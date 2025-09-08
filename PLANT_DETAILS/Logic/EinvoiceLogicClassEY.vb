Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Imports System.Security.Cryptography
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Security
Imports Org.BouncyCastle.Crypto.Parameters

Imports Newtonsoft
Imports Newtonsoft.Json.Linq
Imports System.IdentityModel.Tokens.Jwt
Imports System.Net.Http
Imports RestSharp

Public Class EinvoiceLogicClassEY

    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture

    Dim goAheadFlag As Boolean = True

    Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = New List(Of EinvoiceErrorDetailsClassEY)()
    Dim EinvFromIRNErrorData As List(Of EinvoiceFromIRNErrorDetailsClassEY) = New List(Of EinvoiceFromIRNErrorDetailsClassEY)()
    Dim EinvCancellationErrorData As List(Of EinvoiceCancellationErrorDetailsClassEY) = New List(Of EinvoiceCancellationErrorDetailsClassEY)()
    Dim EwaybillCancellationErrorData As List(Of EwaybillCancellationErrorDetailsClassEY) = New List(Of EwaybillCancellationErrorDetailsClassEY)()

    Dim transporterWoNo As New String("")
    Dim mc1 As New SqlCommand
    Dim objFinishedGoods As New EinvoiceModelClassEY()
    Dim objFinishedGoodsWithoutTransID As New EinvoiceModelClassWithoutTransporterIdEY()
    Dim objService As New EinvoiceModelClassEYService()
    Dim objB2CInvoiceWithTransId As New B2CInvoiceWithDynamicQRCode()
    Dim objB2CInvoiceWithoutTransId As New B2CInvoiceWithDynamicQRCodeWithoutTransId()
    Dim objGSTR1DataEYB2B As New GSTR1DataModelClassEYB2B()
    Dim objGSTR1DataEYB2C As New GSTR1DataModelClassEYB2C()
    Dim objGSTR3BDataEY As New GSTR3BDataModelClassEY()


    Public Function EinvoiceAuthentication(systemInvoiceNo As String, buyerPartyCode As String)
        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = New List(Of AuthenticationErrorDetailsClassEY)()
        Dim AuthErrorDataObj As New AuthenticationErrorDetailsClassEY
        Try

            '''Testing Credentials
            'Dim user_name_test As New String("P000118")
            'Dim password_test As New String("YJncNjgRLmtfPn22")
            'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateAccessToken.do")
            'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
            'requestObjPost.Method = "POST"
            'requestObjPost.ContentType = "application/json"
            'requestObjPost.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user_name_test + ":" + password_test)))

            '''Production Credentials
            'Dim user_name_Production As New String("P000094")
            'Dim password_Production As New String("JM2Fk4ZUTgWdKqkU")
            'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/generateAccessToken.do")

            Dim user_name_Production As New String("api.sail5440")
            Dim password_Production As New String("Nfu0BvzpwHF@ZF")
            Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/generateAccessToken.do")
            Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
            requestObjPost.Method = "POST"
            requestObjPost.ContentType = "application/json"
            requestObjPost.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user_name_Production + ":" + password_Production)))


            Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
                streamWriter.Write("")
                streamWriter.Flush()
                streamWriter.Close()

                Try
                    Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                    Dim txtResponse As New String("")
                    Dim authenticationResponseData As JObject
                    Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                        txtResponse = streamreader.ReadToEnd()
                        authenticationResponseData = JObject.Parse(txtResponse)

                    End Using

                    If (authenticationResponseData("hdr")("status") = "S") Then
                        AuthErrorData.Add(New AuthenticationErrorDetailsClassEY With {.status = "1", .Idtoken = authenticationResponseData("resp")("id_token"),
                            .Access_token = authenticationResponseData("resp")("access_token"), .Refresh_token = authenticationResponseData("resp")("refresh_token"),
                            .token_type = authenticationResponseData("resp")("token_type"), .Expires_in = authenticationResponseData("resp")("expires_in"), .errorCode = "", .errorMessage = ""})


                        Return AuthErrorData
                    Else
                        AuthErrorData.Add(New AuthenticationErrorDetailsClassEY With {.status = "2", .Idtoken = "", .Access_token = "",
                            .Refresh_token = "", .token_type = "", .Expires_in = "", .errorCode = "", .errorMessage = authenticationResponseData("resp")})

                        Return AuthErrorData
                    End If

                Catch ex As Exception
                    AuthErrorData.Add(New AuthenticationErrorDetailsClassEY With {.status = "3", .Idtoken = "", .Access_token = "",
                            .Refresh_token = "", .token_type = "", .Expires_in = "", .errorCode = "", .errorMessage = "There Is some technical error in Authentication"})

                    Return AuthErrorData
                End Try

            End Using


        Catch ee As Exception

            AuthErrorData.Add(New AuthenticationErrorDetailsClassEY With {.status = "3", .Idtoken = "", .Access_token = "",
                            .Refresh_token = "", .Expires_in = "", .errorCode = "", .errorMessage = "There Is some technical error in Authentication"})

            Return AuthErrorData

        Finally

        End Try

    End Function



    Public Function generateAppKey() As Byte()
        Dim KEYGEN As Aes = Aes.Create()
        Dim secretKey() As Byte = KEYGEN.Key
        Return secretKey
    End Function

    Public Function EncryptPasswordUsingAsymmetricEncryption(dataToEncrypt As String, key As String)

        Dim keyBytes() As Byte = Convert.FromBase64String(key)
        Dim asymmetricKeyParameter As AsymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes)
        Dim rsaKeyParameters As RsaKeyParameters = asymmetricKeyParameter
        Dim RSAParameters As New RSAParameters()
        RSAParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned()
        RSAParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
        Dim RSA As New RSACryptoServiceProvider()
        RSA.ImportParameters(RSAParameters)
        Dim plaintext() As Byte = Encoding.UTF8.GetBytes(dataToEncrypt)
        Dim ciphertext() As Byte = RSA.Encrypt(plaintext, False)
        Dim cipherresult As New String(Convert.ToBase64String(ciphertext))
        Return cipherresult
    End Function

    Public Function EncryptAppkeyUsingAsymmetricEncryption(data() As Byte, key As String)

        Dim keyBytes() As Byte = Convert.FromBase64String(key)
        Dim asymmetricKeyParameter As AsymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes)
        Dim rsaKeyParameters As RsaKeyParameters = asymmetricKeyParameter
        Dim RSAParameters As New RSAParameters()
        RSAParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned()
        RSAParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
        Dim RSA As New RSACryptoServiceProvider()
        RSA.ImportParameters(RSAParameters)
        Dim plaintext() As Byte = data
        Dim ciphertext() As Byte = RSA.Encrypt(plaintext, False)
        Dim cipherresult As New String(Convert.ToBase64String(ciphertext))
        Return cipherresult
    End Function

    Public Function SubmitGSTR3BDataToEYPortal(idtoken As String, payloadId As String, payloadData As String, invoiceNo As String, invoiceDate As Date, dataType As String, fromDate As Date, toDate As Date, returnPeriod As String, buyerPartyCode As String, consigneePartyCode As String, isReverseCharge As String)

        '''''''''''''''''''''''''''''''''''''''''''


        '''''''''''''''''''''''''''''''''''''''''''
        '''Testing Authentication Credentials
        'Dim user_name_test As New String("P000118")
        'Dim password_test As New String("YJncNjgRLmtfPn22")
        'Dim strposturltest = String.Format("https://aspsapapi0opy4ft5hwo.eu2.hana.ondemand.com/aspsapapi-0.0.2-SNAPSHOT/api/saveInwardDocERP240.do")
        'Dim requestObjPost As New RestRequest(Method.POST)
        'Dim client As New RestClient(strposturltest)
        'requestObjPost.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user_name_test + ":" + password_test)))
        'requestObjPost.AddHeader("cache-control", "no-cache")
        'requestObjPost.AddHeader("content-type", "application/json")
        'requestObjPost.AddHeader("idtoken", idtoken)
        'requestObjPost.AddHeader("payloadId", payloadId)


        ''Need to be updated as per cloud migration of EY
        Dim user_name_Production As New String("P000094")
        Dim password_Production As New String("JM2Fk4ZUTgWdKqkU")
        Dim strposturlProduction = String.Format("https://api.eydigigst.com/http/sp0049/inwardrestdata")
        Dim requestObjPost As New RestRequest(Method.POST)
        Dim client As New RestClient(strposturlProduction)
        requestObjPost.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user_name_Production + ":" + password_Production)))
        requestObjPost.AddHeader("cache-control", "no-cache")
        requestObjPost.AddHeader("content-type", "application/json")
        requestObjPost.AddHeader("idtoken", idtoken)
        ''requestObjPost.AddHeader("payloadId", payloadId)
        requestObjPost.AddHeader("payloadId", Guid.NewGuid().ToString())
        Dim sha As SHA1 = SHA1.Create()

        Dim docType, reverseCharge, suppGstin, custGstin, pos, companyCode, itemType As New String("")
        Dim invAssessableAmt, invCgstAmt, invSgstAmt, invIgstAmt, docAmt As New Decimal(0.00)

        reverseCharge = isReverseCharge

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            custGstin = dr.Item("c_gst_no")
            pos = dr.Item("C_STATE_CODE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()


        If (Left(buyerPartyCode, 1) = "S") Then
            conn.Open()
            mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                suppGstin = dr.Item("SUPL_GST_NO")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                suppGstin = dr.Item("gst_code")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        End If

        companyCode = "1000"
        docType = "INV"

        Dim itemDetails As List(Of GSTR3BDataItemDetailsEY) = New List(Of GSTR3BDataItemDetailsEY)()
        Dim ReqDetailsEY As List(Of GSTR3BDataReqDetailsEY) = New List(Of GSTR3BDataReqDetailsEY)()

        'docDate = Convert.ToDateTime(docDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)




        Dim supplyType As New String("")

        Try
            Using conn ''will make certain that the connection Is properly disposed
                conn.Open()

                Dim mc1 As New SqlCommand
                Using mc1 'will make certain that the command Is properly disposed

                    mc1.CommandText = "SELECT * FROM TAXABLE_VALUES WHERE INVOICE_NO='" & invoiceNo & "' and INVOICE_DATE ='" & Convert.ToDateTime(invoiceDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & "' AND SUPL_CODE='" & buyerPartyCode & "' and VALUATION_DATE between '" & fromDate.Year & "-" & fromDate.Month & "-" & fromDate.Day & "' AND '" & toDate.Year & "-" & toDate.Month & "-" & toDate.Day & "' AND EY_STATUS IS NULL ORDER BY SL_NO"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows = True Then
                        Using dr 'will make certain that the reader Is properly disposed
                            While (dr.Read())

                                If (isReverseCharge = "N") Then

                                    If (dr("DATA_TYPE") = "FOREIGN") Then
                                        supplyType = "IMPG"
                                    Else
                                        supplyType = "TAX"
                                    End If
                                    itemDetails.Add(New GSTR3BDataItemDetailsEY With {.itemNo = dr("SL_NO"), .supplyType = supplyType, .plantCode = "CGSRU",
                                        .taxableVal = CDec(dr("TAXABLE_VALUE")), .igstRt = CDec(dr("IGST_PERCENTAGE")), .igstAmt = CDec(dr("IGST_AMT")),
                                        .cgstRt = CDec(dr("CGST_PERCENTAGE")), .cgstAmt = CDec(dr("CGST_AMT")), .sgstRt = CDec(dr("SGST_PERCENTAGE")), .sgstAmt = CDec(dr("SGST_AMT")),
                                        .eligibilityIndicator = "IS", .availableIgst = CDec(dr("IGST_AMT")), .availableCgst = CDec(dr("CGST_AMT")),
                                        .availableSgst = CDec(dr("SGST_AMT")), .availableCess = 0, .otherValues = 0})


                                Else
                                    supplyType = "TAX"
                                    itemDetails.Add(New GSTR3BDataItemDetailsEY With {.itemNo = dr("SL_NO"), .supplyType = supplyType, .plantCode = "CGSRU",
                                        .taxableVal = CDec(dr("TAXABLE_VALUE")), .igstRt = CDec(dr("IGST_PERCENTAGE")), .igstAmt = CDec(dr("RCM_IGST_AMT")),
                                        .cgstRt = CDec(dr("CGST_PERCENTAGE")), .cgstAmt = CDec(dr("RCM_CGST_AMT")), .sgstRt = CDec(dr("SGST_PERCENTAGE")), .sgstAmt = CDec(dr("RCM_SGST_AMT")),
                                        .eligibilityIndicator = "IS", .availableIgst = CDec(dr("RCM_IGST_AMT")), .availableCgst = CDec(dr("RCM_CGST_AMT")),
                                        .availableSgst = CDec(dr("RCM_SGST_AMT")), .availableCess = 0, .otherValues = 0})


                                End If



                            End While
                        End Using
                    End If
                End Using
            End Using
        Catch ee As Exception
            ' Roll back the transaction. 

            conn.Close()

        Finally
            conn.Close()

        End Try


        '''''''''''''''''''''''''''''''''
        ReqDetailsEY.Add(New GSTR3BDataReqDetailsEY With {.docType = docType, .docNo = invoiceNo, .docDate = Convert.ToDateTime(invoiceDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), .reverseCharge = reverseCharge,
                                        .suppGstin = suppGstin, .custGstin = custGstin, .pos = pos, .companyCode = companyCode, .returnPeriod = returnPeriod,
                                        .lineItems = itemDetails})
        '''''''''''''''''''''''''''''''''

        objGSTR3BDataEY.req = ReqDetailsEY


        Dim jsonDataForEinvoice As String = Json.JsonConvert.SerializeObject(objGSTR3BDataEY)

        '''''''''''''''''''''''''''''''''''
        requestObjPost.AddHeader("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoice))))
        requestObjPost.AddHeader("companyCode", "1000")
        requestObjPost.AddHeader("pushType", "2")


        requestObjPost.AddParameter("application/json", jsonDataForEinvoice, ParameterType.RequestBody)
        Dim result As IRestResponse = client.Execute(requestObjPost)
        Return result.StatusCode
        'Return ""


    End Function

    Public Function SubmitGSTR1DataToEYPortal(idtoken As String, payloadId As String, payloadData As String, systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, tcsFlag As String, isB2B As String, invoiceDate As Date, docType As String)

        '''Testing Authentication Credentials
        'Dim user_name_test As New String("P000118")
        'Dim password_test As New String("YJncNjgRLmtfPn22")
        'Dim strposturltest = String.Format("https://api.eydigigst.in/http/sp0049/239/outwardrestdata")
        'Dim requestObjPost As New RestRequest(Method.POST)
        'Dim client As New RestClient(strposturltest)
        'requestObjPost.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user_name_test + ":" + password_test)))
        'requestObjPost.AddHeader("cache-control", "no-cache")
        'requestObjPost.AddHeader("content-type", "application/json")
        'requestObjPost.AddHeader("idtoken", idtoken)
        'requestObjPost.AddHeader("payloadId", payloadId)

        'Dim user_name_Production As New String("P000094")
        'Dim password_Production As New String("JM2Fk4ZUTgWdKqkU")
        'Dim strposturlProduction = String.Format("https://api.eydigigst.com/http/sp0049/239/outwardrestdata")

        Dim user_name_Production As New String("api.sail5440")
        Dim password_Production As New String("Nfu0BvzpwHF@ZF")
        Dim strposturlProduction = String.Format("https://prcpi.it-cpi021-rt.cfapps.in30.hana.ondemand.com/http/sp0049/239/outwardrestdata")

        Dim requestObjPost As New RestRequest(Method.POST)
        Dim client As New RestClient(strposturlProduction)
        requestObjPost.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user_name_Production + ":" + password_Production)))
        requestObjPost.AddHeader("cache-control", "no-cache")
        requestObjPost.AddHeader("content-type", "application/json")
        requestObjPost.AddHeader("idtoken", idtoken)
        'requestObjPost.AddHeader("payloadId", payloadId)
        requestObjPost.AddHeader("payloadId", Guid.NewGuid().ToString())

        Dim sha As SHA1 = SHA1.Create()

        Dim jsonDataForEinvoice As New String("")
        If (docType = "INV") Then
            If (isB2B = "YES") Then
                jsonDataForEinvoice = GenerateJsonDataForGSTR1B2B(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, invoiceDate)
            Else
                jsonDataForEinvoice = GenerateJsonDataForGSTR1B2C(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, invoiceDate)
            End If
        Else
            If (isB2B = "YES") Then
                jsonDataForEinvoice = GenerateJsonDataForGSTR1B2B_CNDN(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, invoiceDate, docType)
            Else
                'jsonDataForEinvoice = GenerateJsonDataForGSTR1B2C(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, invoiceDate)
            End If
        End If


        requestObjPost.AddHeader("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoice))))
        requestObjPost.AddHeader("companyCode", "1000")
        requestObjPost.AddHeader("pushType", "2")


        requestObjPost.AddParameter("application/json", jsonDataForEinvoice, ParameterType.RequestBody)
        Dim result As IRestResponse = client.Execute(requestObjPost)
        Return result.StatusCode


    End Function

    Public Function GenerateEInvoiceCNDN(idtoken As String, payloadId As String, payloadData As String, systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, tcsFlag As String, docType As String, originalInvoiceDate As Date)

        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateEinvoice.do")
        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/v2/generateEinvoice.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/v2/generateEinvoice.do")



        Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/generateEinvoice.do")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", idtoken)
        ''requestObjPost.Headers.Add("payloadId", payloadId)
        requestObjPost.Headers.Add("payloadId", Guid.NewGuid().ToString())
        Dim sha As SHA1 = SHA1.Create()

        Dim jsonDataForEinvoice As String
        If (docType = "INV") Then
            jsonDataForEinvoice = GenerateJsonData(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag)
        Else
            jsonDataForEinvoice = GenerateJsonData_CNDN(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, docType, originalInvoiceDate)
        End If


        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoice))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEinvoice)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (txtResponse.Contains("errorfield")) Then

                    EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "3", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .errorfield = "XML parsing error", .errordesc = "There is some error in XML parsing"})
                    Return EinvErrorData

                Else
                    If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                        If (eInvoiceEncryptedResponseData("resp")("infoErrorCode") = "") Then
                            EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "1", .IRN = eInvoiceEncryptedResponseData("resp")("Irn"), .QRCode = eInvoiceEncryptedResponseData("resp")("SignedQRCode"), .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = ""})
                            Return EinvErrorData
                        Else
                            EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "4", .IRN = eInvoiceEncryptedResponseData("resp")("Irn"), .QRCode = eInvoiceEncryptedResponseData("resp")("SignedQRCode"), .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = "", .infoErrorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .infoErrorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                            Return EinvErrorData
                        End If

                    Else

                        EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                        Return EinvErrorData

                    End If
                End If

            Catch ex As Exception
                EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvErrorData
            End Try

        End Using

    End Function

    Public Function GenerateEInvoice(idtoken As String, payloadId As String, payloadData As String, systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, tcsFlag As String, docType As String)

        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateEinvoice.do")
        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/v2/generateEinvoice.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/v2/generateEinvoice.do")


        Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/generateEinvoice.do")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", idtoken)
        ''requestObjPost.Headers.Add("payloadId", payloadId)
        requestObjPost.Headers.Add("payloadId", Guid.NewGuid().ToString())
        Dim sha As SHA1 = SHA1.Create()

        Dim jsonDataForEinvoice As String
        If (docType = "INV") Then
            jsonDataForEinvoice = GenerateJsonData(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag)
        Else
            ''jsonDataForEinvoice = GenerateJsonData_CNDN(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, docType)
        End If


        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoice))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEinvoice)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (txtResponse.Contains("errorfield")) Then

                    EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "3", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .errorfield = "XML parsing error", .errordesc = "There is some error in XML parsing"})
                    Return EinvErrorData

                Else
                    If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                        If (eInvoiceEncryptedResponseData("resp")("infoErrorCode") = "") Then
                            EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "1", .IRN = eInvoiceEncryptedResponseData("resp")("Irn"), .QRCode = eInvoiceEncryptedResponseData("resp")("SignedQRCode"), .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = ""})
                            Return EinvErrorData
                        Else
                            EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "4", .IRN = eInvoiceEncryptedResponseData("resp")("Irn"), .QRCode = eInvoiceEncryptedResponseData("resp")("SignedQRCode"), .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = "", .infoErrorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .infoErrorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                            Return EinvErrorData
                        End If

                    Else

                        EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                        Return EinvErrorData

                    End If
                End If

            Catch ex As Exception
                EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvErrorData
            End Try

        End Using

    End Function


    Public Function GenerateB2CInvoice(idtoken As String, payloadId As String, payloadData As String, systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, tcsFlag As String, docType As String)

        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateEinvoice.do")
        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/v2/generateEinvoice.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)

        ''Need to change as per cloud migration of EY
        Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/generateB2cDeepLink.do")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", idtoken)
        'requestObjPost.Headers.Add("payloadId", payloadId)
        requestObjPost.Headers.Add("payloadId", Guid.NewGuid().ToString())

        Dim sha As SHA1 = SHA1.Create()
        Dim jsonDataForEinvoice As String
        If (docType = "INV") Then
            jsonDataForEinvoice = GenerateJsonDataForB2C(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, "", tcsFlag)
        Else
            jsonDataForEinvoice = GenerateJsonDataForB2C_CNDN(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, "", tcsFlag, docType)
        End If

        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoice))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEinvoice)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()
                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (txtResponse.Contains("errorfield")) Then

                    EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "3", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .errorfield = "XML parsing error", .errordesc = "There is some error in XML parsing"})
                    Return EinvErrorData

                Else
                    If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                        If (eInvoiceEncryptedResponseData("resp")("infoErrorCode") = "") Then
                            EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "1", .IRN = "", .QRCode = eInvoiceEncryptedResponseData("resp")("url"), .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = ""})
                            Return EinvErrorData
                        Else
                            EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "4", .IRN = "", .QRCode = eInvoiceEncryptedResponseData("resp")("url"), .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .infoErrorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .infoErrorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                            Return EinvErrorData
                        End If

                    Else

                        EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                        Return EinvErrorData

                    End If
                End If

            Catch ex As Exception
                EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvErrorData
            End Try

        End Using

    End Function

    Public Function GenerateEwayBillOnly(idtoken As String, payloadId As String, payloadData As String, systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, tcsFlag As String)

        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateEwayBill.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/generateEwayBill.do")

        Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/generateEwayBill.do")

        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", idtoken)
        'requestObjPost.Headers.Add("payloadId", payloadId)
        requestObjPost.Headers.Add("payloadId", Guid.NewGuid().ToString())
        Dim sha As SHA1 = SHA1.Create()


        Dim jsonDataForEinvoice As String = GenerateJsonData(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag)
        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoice))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEinvoice)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (txtResponse.Contains("errorfield")) Then

                    'EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .errorfield = "XML parsing error", .errordesc = "There is some error in XML parsing"})
                    EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "XML parsing error", .errorMessage = "There is some error in XML parsing"})
                    Return EinvErrorData

                Else
                    If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                        'If (eInvoiceEncryptedResponseData("resp")("infoErrorCode") = "") Then
                        '    EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "1", .IRN = eInvoiceEncryptedResponseData("resp")("Irn"), .QRCode = eInvoiceEncryptedResponseData("resp")("SignedQRCode"), .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = ""})
                        '    Return EinvErrorData
                        'Else
                        '    EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "4", .IRN = eInvoiceEncryptedResponseData("resp")("Irn"), .QRCode = eInvoiceEncryptedResponseData("resp")("SignedQRCode"), .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = "", .infoErrorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .infoErrorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                        '    Return EinvErrorData
                        'End If

                        ''''''''''''''''''''''''''''''''''
                        If (txtResponse.Contains("errorCode") And eInvoiceEncryptedResponseData("resp")("errorCode") <> "") Then
                            EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                            Return EinvErrorData
                        Else
                            If (txtResponse.Contains("infoErrorCode") And eInvoiceEncryptedResponseData("resp")("infoErrorCode") <> "") Then
                                EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage"), .infoErrorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .infoErrorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                                Return EinvErrorData
                            Else
                                EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "1", .IRN = "", .QRCode = "", .EwbNo = eInvoiceEncryptedResponseData("resp")("ewayBillNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("ewayBillDate"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("validUpto"), .errorCode = "", .errorMessage = ""})
                                Return EinvErrorData
                            End If
                        End If
                        ''''''''''''''''''''''''''''''''''
                    Else

                        EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                        Return EinvErrorData

                    End If
                End If

            Catch ex As Exception
                EinvErrorData.Add(New EinvoiceErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvErrorData
            End Try

        End Using

    End Function

    Public Function GenerateEwayBillFromIRN(idtoken As String, payloadId As String, IRN As String, systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String)

        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateEWBByIRN.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/generateEWBByIRN.do")


        Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/generateEWBByIRN.do")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", idtoken)
        'requestObjPost.Headers.Add("payloadId", payloadId)
        requestObjPost.Headers.Add("payloadId", Guid.NewGuid().ToString())
        Dim sha As SHA1 = SHA1.Create()


        Dim jsonDataForEinvoice As String = GenerateJsonDataEwaybillFromIRN(systemInvoiceNo, IRN, buyerPartyCode, consigneePartyCode)
        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoice))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEinvoice)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (txtResponse.Contains("errorfield")) Then

                    EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "3", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .errorfield = "XML parsing error", .errordesc = "There is some error in XML parsing"})
                    Return EinvFromIRNErrorData

                Else
                    If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                        If (txtResponse.Contains("errorCode") And eInvoiceEncryptedResponseData("resp")("errorCode") <> "") Then
                            EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                            Return EinvFromIRNErrorData
                        Else
                            If (txtResponse.Contains("infoErrorCode") And eInvoiceEncryptedResponseData("resp")("infoErrorCode") <> "") Then
                                EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "4", .IRN = IRN, .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .infoErrorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .infoErrorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                                Return EinvFromIRNErrorData
                            Else
                                EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "1", .IRN = IRN, .QRCode = "", .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = ""})
                                Return EinvFromIRNErrorData
                            End If
                        End If

                    Else

                        EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                        Return EinvFromIRNErrorData

                    End If
                End If

            Catch ex As Exception
                EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvFromIRNErrorData
            End Try

        End Using

    End Function

    Public Function UpdatePartBOfEwayBill(idtoken As String, payloadId As String, myGSTCode As String, EWAYBILL_NO As String, vehicleNo As String, fromPlace As String, fromState As String, reasonCode As String, reasonRem As String)

        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/updatePartBEwayBill.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/updatePartBEwayBill.do")

        Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/updatePartBEwayBill.do")

        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", idtoken)
        ''requestObjPost.Headers.Add("payloadId", payloadId)
        requestObjPost.Headers.Add("payloadId", Guid.NewGuid().ToString())
        Dim sha As SHA1 = SHA1.Create()

        Dim jsonDataForEwaybillUpdate As String = "{""req"": [{""gstin"": """ + myGSTCode + """,""ewbNo"": """ + EWAYBILL_NO + """,""vehicleNo"": """ + vehicleNo + """,""vehicleType"": ""R"",""fromPlace"": """ + fromPlace + """,""fromState"": """ + fromState + """,""reasonCode"": """ + reasonCode + """,""reasonRem"": """ + reasonRem + """,""transMode"": ""ROAD""}]}"
        'Dim jsonDataForEwaybillUpdate As String = GenerateJsonDataEwaybillFromIRN(vehicleNo, EWAYBILL_NO, buyerPartyCode, consigneePartyCode)
        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEwaybillUpdate))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEwaybillUpdate)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (txtResponse.Contains("errorfield")) Then

                    EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "3", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .errorfield = "XML parsing error", .errordesc = "There is some error in XML parsing"})
                    Return EinvFromIRNErrorData

                Else
                    If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                        If (txtResponse.Contains("errorCode") And eInvoiceEncryptedResponseData("resp")("errorCode") <> "") Then
                            EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                            Return EinvFromIRNErrorData
                        Else
                            If (txtResponse.Contains("infoErrorCode") And eInvoiceEncryptedResponseData("resp")("infoErrorCode") <> "") Then
                                EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "4", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "", .infoErrorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .infoErrorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                                Return EinvFromIRNErrorData
                            Else
                                EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "1", .IRN = "", .QRCode = "", .EwbNo = eInvoiceEncryptedResponseData("resp")("EwbNo"), .EwbDt = eInvoiceEncryptedResponseData("resp")("EwbDt"), .EwbValidTill = eInvoiceEncryptedResponseData("resp")("EwbValidTill"), .errorCode = "", .errorMessage = ""})
                                Return EinvFromIRNErrorData
                            End If
                        End If

                    Else

                        EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                        Return EinvFromIRNErrorData

                    End If
                End If

            Catch ex As Exception
                EinvFromIRNErrorData.Add(New EinvoiceFromIRNErrorDetailsClassEY With {.status = "2", .IRN = "", .QRCode = "", .EwbNo = "", .EwbDt = "", .EwbValidTill = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvFromIRNErrorData
            End Try

        End Using

    End Function

    Public Function CancelEInvoice(IdToken As String, IRN_No As String, Gstin As String, cnlReason As String, cnlRemarks As String)

        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/cancelEinvoice.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/cancelEinvoice.do")

        Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/cancelEinvoice.do")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", IdToken)

        Dim jsonDataForEinvoiceCancel As String = "{""req"": [{""irn"": """ + IRN_No + """,""cnlRsn"": """ + cnlReason + """,""cnlRem"": """ + cnlRemarks + """,""gstin"": """ + Gstin + """}]}"
        'Dim eInvoiceRequestData As String = "{""Data"": """ + EncryptDataUsingSymmetricKey(jsonDataForEinvoiceCancel, decryptedSek) + """}"


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEinvoiceCancel)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                    If (eInvoiceEncryptedResponseData("resp")(0)("irn") <> "") Then
                        EinvCancellationErrorData.Add(New EinvoiceCancellationErrorDetailsClassEY With {.status = "1", .IRN = eInvoiceEncryptedResponseData("resp")(0)("irn"), .CancelDate = eInvoiceEncryptedResponseData("resp")(0)("cancelDate"), .errorCode = "", .errorMessage = ""})
                        Return EinvCancellationErrorData
                    Else
                        EinvCancellationErrorData.Add(New EinvoiceCancellationErrorDetailsClassEY With {.status = "2", .IRN = "", .CancelDate = "", .errorCode = eInvoiceEncryptedResponseData("resp")(0)("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")(0)("errorMessage")})
                        Return EinvCancellationErrorData
                    End If

                Else

                    EinvCancellationErrorData.Add(New EinvoiceCancellationErrorDetailsClassEY With {.status = "2", .IRN = "", .CancelDate = "", .errorCode = eInvoiceEncryptedResponseData("resp")(0)("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")(0)("errorMessage")})
                    Return EinvCancellationErrorData

                End If

            Catch ex As Exception
                EinvCancellationErrorData.Add(New EinvoiceCancellationErrorDetailsClassEY With {.status = "2", .IRN = "", .CancelDate = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvCancellationErrorData
            End Try

        End Using
    End Function

    Public Function CancelEwayBill(IdToken As String, payloadId As String, EWB_NO As String, Gstin As String, cancelRsnCode As String)
        'Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/cancelEwayBill.do")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'Dim strposturlProduction = String.Format("https://eapi.eydigigst.com/eybusinessapi-0.0.2-SNAPSHOT/api/cancelEwayBill.do")

        Dim strposturlProduction = String.Format("https://eapi.digigstey.com/bcappapi/cancelEwayBill.do")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)

        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", IdToken)
        ''requestObjPost.Headers.Add("payloadId", payloadId)
        requestObjPost.Headers.Add("payloadId", Guid.NewGuid().ToString())
        Dim sha As SHA1 = SHA1.Create()

        Dim jsonDataForEinvoiceCancel As String = "{""req"": [{""gstin"": """ + Gstin + """,""ewbNo"": """ + EWB_NO + """,""cancelRsnCode"": """ + cancelRsnCode + """}]}"
        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(jsonDataForEinvoiceCancel))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(jsonDataForEinvoiceCancel)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using
                '''''''''''''''''''''''''''''''
                If (txtResponse.Contains("errorfield")) Then

                    EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "2", .ewayBillNo = "", .CancelDate = "", .errorCode = "XML parsing error", .errorMessage = "There is some error in XML parsing"})
                    Return EwaybillCancellationErrorData

                Else
                    If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                        If (txtResponse.Contains("errorCode") And eInvoiceEncryptedResponseData("resp")("errorCode") <> "") Then
                            EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "2", .ewayBillNo = "", .CancelDate = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                            Return EwaybillCancellationErrorData
                        Else
                            If (txtResponse.Contains("infoErrorCode") And eInvoiceEncryptedResponseData("resp")("infoErrorCode") <> "") Then
                                EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "2", .ewayBillNo = "", .CancelDate = "", .errorCode = eInvoiceEncryptedResponseData("resp")("infoErrorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("infoErrorMessage")})
                                Return EwaybillCancellationErrorData
                            Else
                                EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "1", .ewayBillNo = eInvoiceEncryptedResponseData("resp")("ewayBillNo"), .CancelDate = eInvoiceEncryptedResponseData("resp")("cancelDate"), .errorCode = "", .errorMessage = ""})
                                Return EwaybillCancellationErrorData
                            End If
                        End If

                    Else

                        EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "2", .ewayBillNo = "", .CancelDate = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                        Return EwaybillCancellationErrorData

                    End If
                End If

                '''''''''''''''''''''''''''''''
                'If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                '    'Dim decodedQRCode As String = DecodeSignedQRCode(eInvoiceDecryptedResponseData("SignedQRCode"))

                '    If (eInvoiceEncryptedResponseData("resp")("irn") <> "") Then
                '        EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "1", .ewayBillNo = eInvoiceEncryptedResponseData("resp")("ewayBillNo"), .CancelDate = eInvoiceEncryptedResponseData("resp")("cancelDate"), .errorCode = "", .errorMessage = ""})
                '        Return EwaybillCancellationErrorData
                '    Else
                '        EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "2", .ewayBillNo = "", .CancelDate = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                '        Return EwaybillCancellationErrorData
                '    End If
                'Else

                '    EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "2", .ewayBillNo = "", .CancelDate = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                '    Return EwaybillCancellationErrorData

                'End If

            Catch ex As Exception
                EwaybillCancellationErrorData.Add(New EwaybillCancellationErrorDetailsClassEY With {.status = "2", .ewayBillNo = "", .CancelDate = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EwaybillCancellationErrorData
            End Try

        End Using
    End Function

    Public Function DecodeSignedQRCode(token As String)

        Dim handler = New JwtSecurityTokenHandler()
        Dim token1 = handler.ReadJwtToken(token)
        Dim responsePayload As String = token1.Payload.First().Value

        Return responsePayload

    End Function

    Public Function DecryptResponseUsingSymmetricKey(encrytedText As String, sek As String)

        ''Decrypting using SEK
        Try

            Dim dataToDecrypt() As Byte = Convert.FromBase64String(encrytedText)
            Dim keyBytes() As Byte = (Convert.FromBase64String(sek))
            Dim tdes As New AesManaged()
            tdes.KeySize = 256
            tdes.BlockSize = 128
            tdes.Key = keyBytes
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.PKCS7
            Dim decrypt__1 As ICryptoTransform = tdes.CreateDecryptor()
            Dim deCipher() As Byte = decrypt__1.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length)
            tdes.Clear()

            Dim strResponse As String = Encoding.UTF8.GetString(deCipher)

            Return strResponse

            ''''''''''''''''''''''''''''''''''''

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Function DecryptSymmetricKey(encryptedText As String, key() As Byte)

        ''Decrypting SEK
        Try

            Dim dataToDecrypt() As Byte = Convert.FromBase64String(encryptedText)
            Dim keyBytes = key
            Dim tdes As New AesManaged()
            tdes.KeySize = 256
            tdes.BlockSize = 128
            tdes.Key = keyBytes
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.PKCS7
            Dim decrypt__1 As ICryptoTransform = tdes.CreateDecryptor()
            Dim deCipher() As Byte = decrypt__1.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length)
            tdes.Clear()
            Dim EK_result As String = Convert.ToBase64String(deCipher)
            Return EK_result

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Function EncryptDataUsingSymmetricKey(text As String, sek As String)

        ''Encrypting using SEK
        Try
            Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(text)

            Dim dataToEncrypt() As Byte = Convert.FromBase64String(Convert.ToBase64String(byt))
            Dim keyBytes() As Byte = (Convert.FromBase64String(sek))
            Dim tdes As AesManaged = New AesManaged()
            tdes.KeySize = 256
            tdes.BlockSize = 128
            tdes.Key = keyBytes
            tdes.Mode = CipherMode.ECB
            tdes.Padding = PaddingMode.PKCS7
            Dim encrypt__1 As ICryptoTransform = tdes.CreateEncryptor()
            Dim deCipher() As Byte = encrypt__1.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length)
            tdes.Clear()
            Dim EK_result As String = Convert.ToBase64String(deCipher)

            ''''''''''''''''''''''''''''''''''''

            Return EK_result

        Catch ex As Exception

            Throw ex
        End Try
    End Function

    Public Function GenerateJsonDataForGSTR1B2B(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String, invoiceDate As Date)

        Dim working_date As Date = invoiceDate
        Dim STR1 As String = ""
        If working_date.Month > 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf working_date.Month <= 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        '''''''''''''''''''''''''''''''''''''''''''
        Dim transporterGSTNo As New String("")
        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            transporterWoNo = dr.Item("TRANS_WO")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (IsDBNull(dr.Item("SUPL_GST_NO"))) Then
                transporterGSTNo = ""
            Else
                transporterGSTNo = dr.Item("SUPL_GST_NO")
            End If
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim docType, docNo, docDate, reverseCharge, suppGstin, supTradeName, supLegalName, supLocation, supStateCode,
            custGstin, custOrSupName, custOrSupAddr1, custOrSupAddr2, custOrSupAddr4, billToState, pos,
            tranType, taxScheme, companyCode, profitCentre1, docCat, returnPeriod, isService, itemType As New String("")
        Dim invAssessableAmt, invCgstAmt, invSgstAmt, invIgstAmt, docAmt As New Decimal(0.00)

        If (Left(systemInvoiceNo, 2) = "DC") Then
            docType = "DLC"
        Else
            docType = "INV"
        End If

        docNo = systemInvoiceNo

        reverseCharge = "N"

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            suppGstin = dr.Item("c_gst_no")
            supTradeName = dr.Item("c_name")
            supLegalName = dr.Item("c_name")
            supLocation = dr.Item("c_city")
            supStateCode = dr.Item("c_state_code")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()



        If (Left(buyerPartyCode, 1) = "S") Then
            conn.Open()
            mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                custGstin = dr.Item("SUPL_GST_NO")
                custOrSupName = dr.Item("SUPL_NAME")
                custOrSupAddr1 = dr.Item("SUPL_AT")
                custOrSupAddr2 = dr.Item("SUPL_PO")
                custOrSupAddr4 = dr.Item("SUPL_DIST")
                billToState = dr.Item("SUPL_STATE_CODE")
                pos = dr.Item("SUPL_STATE_CODE")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                custGstin = dr.Item("gst_code")
                custOrSupName = dr.Item("d_name")
                custOrSupAddr1 = dr.Item("add_1")
                custOrSupAddr2 = dr.Item("add_2")
                custOrSupAddr4 = dr.Item("d_city")
                billToState = dr.Item("d_state_code")
                pos = dr.Item("d_state_code")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        End If

        tranType = "O"

        taxScheme = "GST"
        companyCode = "1000"
        profitCentre1 = "SRU100"
        docCat = "REG"



        Dim itemDetails As List(Of GSTR1DataItemDetailsEYB2B) = New List(Of GSTR1DataItemDetailsEYB2B)()
        Dim ReqDetailsEY As List(Of GSTR1DataReqDetailsEYB2B) = New List(Of GSTR1DataReqDetailsEYB2B)()

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then

            Else

                transporterWoNo = dr.Item("TRANS_WO")
            End If

            If (isServiceFlag = "YES") Then
                isService = "Y"
                itemType = "S"
            Else
                isService = "N"
                itemType = "G"
            End If

            docDate = Convert.ToDateTime(dr.Item("INV_DATE")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            returnPeriod = Convert.ToDateTime(dr.Item("INV_DATE")).Month.ToString("D2") & Convert.ToDateTime(dr.Item("INV_DATE")).Year
            Dim gstRate As Decimal
            If (CDec(dr.Item("IGST_RATE")) = 0) Then
                gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
            Else
                gstRate = dr.Item("IGST_RATE")
            End If
            Dim supplyType As String = ""
            If (Left(systemInvoiceNo, 2) = "DC") Then
                supplyType = "NSY"
            Else
                supplyType = "TAX"
            End If

            itemDetails.Add(New GSTR1DataItemDetailsEYB2B With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                        .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemType = itemType, .itemUqc = dr.Item("ACC_UNIT"), .plantCode = "CGSRU",
                                        .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                        .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                        .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2),
                                        .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})


            invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
            invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
            invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
            invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

            docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)

            '''''''''''''''''''''''''''''''''
            ReqDetailsEY.Add(New GSTR1DataReqDetailsEYB2B With {.docType = docType, .docNo = docNo, .docDate = docDate, .reverseCharge = reverseCharge,
                                        .suppGstin = suppGstin, .supTradeName = supTradeName, .supLegalName = supLegalName, .supLocation = supLocation, .supStateCode = supStateCode,
                                        .custGstin = custGstin, .custOrSupName = custOrSupName, .custOrSupAddr1 = custOrSupAddr1, .custOrSupAddr2 = custOrSupAddr2, .custOrSupAddr4 = custOrSupAddr4,
                                        .billToState = billToState, .pos = pos, .tranType = tranType, .taxScheme = taxScheme, .companyCode = companyCode, .profitCentre1 = profitCentre1, .docCat = docCat, .returnPeriod = returnPeriod,
                                        .invAssessableAmt = invAssessableAmt, .invCgstAmt = invCgstAmt, .invSgstAmt = invSgstAmt, .invIgstAmt = invIgstAmt, .docAmt = docAmt, .lineItems = itemDetails})
            '''''''''''''''''''''''''''''''''
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        objGSTR1DataEYB2B.req = ReqDetailsEY

        ''Return the output
        Dim output As String = Json.JsonConvert.SerializeObject(objGSTR1DataEYB2B)
        Return output

        '''''''''''''''''''''''''''''''''''''''''''
        'If (isServiceFlag = "YES") Then

        '    Dim transporterGSTNo As New String("")
        '    conn.Open()
        '    mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()

        '        transporterWoNo = dr.Item("TRANS_WO")

        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()

        '    conn.Open()
        '    mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()
        '        If (IsDBNull(dr.Item("SUPL_GST_NO"))) Then
        '            transporterGSTNo = ""
        '        Else
        '            transporterGSTNo = dr.Item("SUPL_GST_NO")
        '        End If
        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()
        '    Dim docType, docNo, docDate, reverseCharge, suppGstin, supTradeName, supLegalName, supLocation, supStateCode,
        '    custGstin, custOrSupName, custOrSupAddr1, custOrSupAddr2, custOrSupAddr4, billToState, pos,
        '    tranType, taxScheme, companyCode, profitCentre1, docCat, returnPeriod As New String("")
        '    Dim invAssessableAmt, invCgstAmt, invSgstAmt, invIgstAmt, docAmt As New Decimal(0.00)

        '    If (Left(systemInvoiceNo, 2) = "DC") Then
        '        docType = "DLC"
        '    Else
        '        docType = "INV"
        '    End If

        '    docNo = systemInvoiceNo
        '    docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        '    reverseCharge = "N"

        '    conn.Open()
        '    mc1.CommandText = "select * from comp_profile"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()

        '        suppGstin = dr.Item("c_gst_no")
        '        supTradeName = dr.Item("c_name")
        '        supLegalName = dr.Item("c_name")
        '        supLocation = dr.Item("c_city")
        '        supStateCode = dr.Item("c_state_code")

        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()



        '    If (Left(buyerPartyCode, 1) = "S") Then
        '        conn.Open()
        '        mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
        '        mc1.Connection = conn
        '        dr = mc1.ExecuteReader
        '        If dr.HasRows Then
        '            dr.Read()

        '            custGstin = dr.Item("SUPL_GST_NO")
        '            custOrSupName = dr.Item("SUPL_NAME")
        '            custOrSupAddr1 = dr.Item("SUPL_AT")
        '            custOrSupAddr2 = dr.Item("SUPL_PO")
        '            custOrSupAddr4 = dr.Item("SUPL_DIST")
        '            billToState = dr.Item("SUPL_STATE_CODE")
        '            pos = dr.Item("SUPL_STATE_CODE")

        '            dr.Close()
        '        Else
        '            dr.Close()
        '        End If
        '        conn.Close()
        '    Else
        '        conn.Open()
        '        mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
        '        mc1.Connection = conn
        '        dr = mc1.ExecuteReader
        '        If dr.HasRows Then
        '            dr.Read()

        '            custGstin = dr.Item("gst_code")
        '            custOrSupName = dr.Item("d_name")
        '            custOrSupAddr1 = dr.Item("add_1")
        '            custOrSupAddr2 = dr.Item("add_2")
        '            custOrSupAddr4 = dr.Item("d_city")
        '            billToState = dr.Item("d_state_code")
        '            pos = dr.Item("d_state_code")

        '            dr.Close()
        '        Else
        '            dr.Close()
        '        End If
        '        conn.Close()
        '    End If

        '    tranType = "O"

        '    taxScheme = "GST"
        '    companyCode = "1000"
        '    profitCentre1 = "SRU100"
        '    docCat = "REG"

        '    returnPeriod = working_date.Month.ToString("D2") & working_date.Year

        '    Dim itemDetails As List(Of GSTR1DataItemDetailsEYB2B) = New List(Of GSTR1DataItemDetailsEYB2B)()
        '    Dim ReqDetailsEY As List(Of GSTR1DataReqDetailsEYB2B) = New List(Of GSTR1DataReqDetailsEYB2B)()

        '    conn.Open()
        '    mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()
        '        If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then

        '        Else

        '            transporterWoNo = dr.Item("TRANS_WO")
        '        End If

        '        Dim isService As String = "Y"

        '        Dim gstRate As Decimal
        '        If (CDec(dr.Item("IGST_RATE")) = 0) Then
        '            gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
        '        Else
        '            gstRate = dr.Item("IGST_RATE")
        '        End If
        '        Dim supplyType As String = ""
        '        If (Left(systemInvoiceNo, 2) = "DC") Then
        '            supplyType = "NSY"
        '        Else
        '            supplyType = "TAX"
        '        End If

        '        itemDetails.Add(New GSTR1DataItemDetailsEYB2B With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
        '                                .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemType = "S", .itemUqc = dr.Item("ACC_UNIT"), .plantCode = "CGSRU",
        '                                .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
        '                                .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
        '                                .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2),
        '                                .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})


        '        invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
        '        invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
        '        invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
        '        invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

        '        docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)

        '        '''''''''''''''''''''''''''''''''
        '        ReqDetailsEY.Add(New GSTR1DataReqDetailsEYB2B With {.docType = docType, .docNo = docNo, .docDate = docDate, .reverseCharge = reverseCharge,
        '                                .suppGstin = suppGstin, .supTradeName = supTradeName, .supLegalName = supLegalName, .supLocation = supLocation, .supStateCode = supStateCode,
        '                                .custGstin = custGstin, .custOrSupName = custOrSupName, .custOrSupAddr1 = custOrSupAddr1, .custOrSupAddr2 = custOrSupAddr2, .custOrSupAddr4 = custOrSupAddr4,
        '                                .billToState = billToState, .pos = pos, .tranType = tranType, .taxScheme = taxScheme, .companyCode = companyCode, .profitCentre1 = profitCentre1, .docCat = docCat, .returnPeriod = returnPeriod,
        '                                .invAssessableAmt = invAssessableAmt, .invCgstAmt = invCgstAmt, .invSgstAmt = invSgstAmt, .invIgstAmt = invIgstAmt, .docAmt = docAmt, .lineItems = itemDetails})
        '        '''''''''''''''''''''''''''''''''
        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()

        '    objGSTR1DataEY.req = ReqDetailsEY

        '    ''Return the output
        '    Dim output As String = Json.JsonConvert.SerializeObject(objGSTR1DataEY)
        '    Return output

        'Else

        '    Dim transporterGSTNo As New String("")
        '    conn.Open()
        '    mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()

        '        transporterWoNo = dr.Item("TRANS_WO")

        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()

        '    conn.Open()
        '    mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()
        '        If (IsDBNull(dr.Item("SUPL_GST_NO"))) Then
        '            transporterGSTNo = ""
        '        Else
        '            transporterGSTNo = dr.Item("SUPL_GST_NO")
        '        End If
        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()
        '    Dim docType, docNo, docDate, reverseCharge, suppGstin, supTradeName, supLegalName, supLocation, supStateCode,
        '    custGstin, custOrSupName, custOrSupAddr1, custOrSupAddr2, custOrSupAddr4, billToState, pos,
        '    tranType, taxScheme, companyCode, profitCentre1, docCat, returnPeriod As New String("")
        '    Dim invAssessableAmt, invCgstAmt, invSgstAmt, invIgstAmt, docAmt As New Decimal(0.00)

        '    If (Left(systemInvoiceNo, 2) = "DC") Then
        '        docType = "DLC"
        '    Else
        '        docType = "INV"
        '    End If

        '    docNo = systemInvoiceNo
        '    docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        '    reverseCharge = "N"

        '    conn.Open()
        '    mc1.CommandText = "select * from comp_profile"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()

        '        suppGstin = dr.Item("c_gst_no")
        '        supTradeName = dr.Item("c_name")
        '        supLegalName = dr.Item("c_name")
        '        supLocation = dr.Item("c_city")
        '        supStateCode = dr.Item("c_state_code")

        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()

        '    conn.Open()
        '    mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()

        '        custGstin = dr.Item("gst_code")
        '        custOrSupName = dr.Item("d_name")
        '        custOrSupAddr1 = dr.Item("add_1")
        '        custOrSupAddr2 = dr.Item("add_2")
        '        custOrSupAddr4 = dr.Item("d_city")
        '        billToState = dr.Item("d_state_code")
        '        pos = dr.Item("d_state_code")

        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()

        '    tranType = "O"

        '    taxScheme = "GST"
        '    companyCode = "1000"
        '    profitCentre1 = "SRU100"
        '    docCat = "REG"

        '    returnPeriod = working_date.Month.ToString("D2") & working_date.Year

        '    Dim itemDetails As List(Of GSTR1DataItemDetailsEYB2B) = New List(Of GSTR1DataItemDetailsEYB2B)()
        '    Dim ReqDetailsEY As List(Of GSTR1DataReqDetailsEYB2B) = New List(Of GSTR1DataReqDetailsEYB2B)()
        '    conn.Open()
        '    mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        '    mc1.Connection = conn
        '    dr = mc1.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()
        '        If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then

        '        Else

        '            transporterWoNo = dr.Item("TRANS_WO")
        '        End If

        '        Dim isService As String = "N"

        '        Dim gstRate As Decimal
        '        If (CDec(dr.Item("IGST_RATE")) = 0) Then
        '            gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
        '        Else
        '            gstRate = dr.Item("IGST_RATE")
        '        End If
        '        Dim supplyType As String = ""
        '        If (Left(systemInvoiceNo, 2) = "DC") Then
        '            supplyType = "NSY"
        '        Else
        '            supplyType = "TAX"
        '        End If

        '        itemDetails.Add(New GSTR1DataItemDetailsEYB2B With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
        '                                .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemType = "G", .itemUqc = dr.Item("ACC_UNIT"), .plantCode = "CGSRU",
        '                                .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
        '                                .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
        '                                .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2),
        '                                .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})


        '        invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
        '        invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
        '        invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
        '        invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

        '        docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)

        '        '''''''''''''''''''''''''''''''''
        '        ReqDetailsEY.Add(New GSTR1DataReqDetailsEYB2B With {.docType = docType, .docNo = docNo, .docDate = docDate, .reverseCharge = reverseCharge,
        '                                .suppGstin = suppGstin, .supTradeName = supTradeName, .supLegalName = supLegalName, .supLocation = supLocation, .supStateCode = supStateCode,
        '                                .custGstin = custGstin, .custOrSupName = custOrSupName, .custOrSupAddr1 = custOrSupAddr1, .custOrSupAddr2 = custOrSupAddr2, .custOrSupAddr4 = custOrSupAddr4,
        '                                .billToState = billToState, .pos = pos, .tranType = tranType, .taxScheme = taxScheme, .companyCode = companyCode, .profitCentre1 = profitCentre1, .docCat = docCat, .returnPeriod = returnPeriod,
        '                                .invAssessableAmt = invAssessableAmt, .invCgstAmt = invCgstAmt, .invSgstAmt = invSgstAmt, .invIgstAmt = invIgstAmt, .docAmt = docAmt, .lineItems = itemDetails})
        '        '''''''''''''''''''''''''''''''''
        '        dr.Close()
        '    Else
        '        dr.Close()
        '    End If
        '    conn.Close()

        '    objGSTR1DataEY.req = ReqDetailsEY

        '    ''Return the output
        '    Dim output As String = Json.JsonConvert.SerializeObject(objGSTR1DataEY)
        '    Return output
        'End If

    End Function


    Public Function GenerateJsonDataForGSTR1B2B_CNDN(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String, invoiceDate As Date, docType As String)



        '''''''''''''''''''''''''''''''''''''''''''
        Dim despatchInvNo As New String("")
        Dim DEBIT_CREDIT_DATE As Date
        conn.Open()
        mc1.CommandText = "select * from CN_DN_DETAILS with(nolock) where DEBIT_CREDIT_NO='" & systemInvoiceNo & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            despatchInvNo = dr.Item("INVOICE_NO")
            DEBIT_CREDIT_DATE = dr.Item("DEBIT_CREDIT_DATE")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim working_date As Date = invoiceDate
        Dim STR1 As String = ""
        If working_date.Month > 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf working_date.Month <= 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If


        Dim docNo, docDate, reverseCharge, suppGstin, supTradeName, supLegalName, supLocation, supStateCode,
            custGstin, custOrSupName, custOrSupAddr1, custOrSupAddr2, custOrSupAddr4, billToState, pos,
            tranType, taxScheme, companyCode, profitCentre1, docCat, returnPeriod, isService, itemType As New String("")
        Dim invAssessableAmt, invCgstAmt, invSgstAmt, invIgstAmt, docAmt As New Decimal(0.00)

        docNo = systemInvoiceNo

        reverseCharge = "N"

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            suppGstin = dr.Item("c_gst_no")
            supTradeName = dr.Item("c_name")
            supLegalName = dr.Item("c_name")
            supLocation = dr.Item("c_city")
            supStateCode = dr.Item("c_state_code")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()



        If (Left(buyerPartyCode, 1) = "S") Then
            conn.Open()
            mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                custGstin = dr.Item("SUPL_GST_NO")
                custOrSupName = dr.Item("SUPL_NAME")
                custOrSupAddr1 = dr.Item("SUPL_AT")
                custOrSupAddr2 = dr.Item("SUPL_PO")
                custOrSupAddr4 = dr.Item("SUPL_DIST")
                billToState = dr.Item("SUPL_STATE_CODE")
                pos = dr.Item("SUPL_STATE_CODE")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                custGstin = dr.Item("gst_code")
                custOrSupName = dr.Item("d_name")
                custOrSupAddr1 = dr.Item("add_1")
                custOrSupAddr2 = dr.Item("add_2")
                custOrSupAddr4 = dr.Item("d_city")
                billToState = dr.Item("d_state_code")
                pos = dr.Item("d_state_code")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        End If

        tranType = "O"

        taxScheme = "GST"
        companyCode = "1000"
        profitCentre1 = "SRU100"
        docCat = "REG"

        Dim itemDetails As List(Of GSTR1DataItemDetailsEYB2B) = New List(Of GSTR1DataItemDetailsEYB2B)()
        Dim ReqDetailsEY As List(Of GSTR1DataReqDetailsEYB2B) = New List(Of GSTR1DataReqDetailsEYB2B)()

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & despatchInvNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then

            Else

                transporterWoNo = dr.Item("TRANS_WO")
            End If

            If (isServiceFlag = "YES") Then
                isService = "Y"
                itemType = "S"
            Else
                isService = "N"
                itemType = "G"
            End If

            docDate = DEBIT_CREDIT_DATE.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            returnPeriod = DEBIT_CREDIT_DATE.Month.ToString("D2") & DEBIT_CREDIT_DATE.Year
            Dim gstRate As Decimal
            If (CDec(dr.Item("IGST_RATE")) = 0) Then
                gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
            Else
                gstRate = dr.Item("IGST_RATE")
            End If
            Dim supplyType As String = ""
            If (Left(systemInvoiceNo, 2) = "DC") Then
                supplyType = "NSY"
            Else
                supplyType = "TAX"
            End If

            itemDetails.Add(New GSTR1DataItemDetailsEYB2B With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                        .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemType = itemType, .itemUqc = dr.Item("ACC_UNIT"), .plantCode = "CGSRU",
                                        .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                        .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                        .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2),
                                        .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})


            invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
            invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
            invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
            invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

            docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)

            '''''''''''''''''''''''''''''''''
            ReqDetailsEY.Add(New GSTR1DataReqDetailsEYB2B With {.docType = docType, .docNo = docNo, .docDate = docDate, .reverseCharge = reverseCharge,
                                        .suppGstin = suppGstin, .supTradeName = supTradeName, .supLegalName = supLegalName, .supLocation = supLocation, .supStateCode = supStateCode,
                                        .custGstin = custGstin, .custOrSupName = custOrSupName, .custOrSupAddr1 = custOrSupAddr1, .custOrSupAddr2 = custOrSupAddr2, .custOrSupAddr4 = custOrSupAddr4,
                                        .billToState = billToState, .pos = pos, .tranType = tranType, .taxScheme = taxScheme, .companyCode = companyCode, .profitCentre1 = profitCentre1, .docCat = docCat, .returnPeriod = returnPeriod,
                                        .invAssessableAmt = invAssessableAmt, .invCgstAmt = invCgstAmt, .invSgstAmt = invSgstAmt, .invIgstAmt = invIgstAmt, .docAmt = docAmt, .lineItems = itemDetails})
            '''''''''''''''''''''''''''''''''
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        objGSTR1DataEYB2B.req = ReqDetailsEY

        ''Return the output
        Dim output As String = Json.JsonConvert.SerializeObject(objGSTR1DataEYB2B)
        Return output

        '''''''''''''''''''''''''''''''''''''''''''


    End Function

    Public Function GenerateJsonDataForGSTR1B2C(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String, invoiceDate As Date)
        Dim working_date As Date = invoiceDate
        Dim STR1 As String = ""
        If working_date.Month > 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf working_date.Month <= 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        '''''''''''''''''''''''''''''''''''''''''''
        Dim transporterGSTNo As New String("")
        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            transporterWoNo = dr.Item("TRANS_WO")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (IsDBNull(dr.Item("SUPL_GST_NO"))) Then
                transporterGSTNo = ""
            Else
                transporterGSTNo = dr.Item("SUPL_GST_NO")
            End If
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim docType, docNo, docDate, reverseCharge, suppGstin, supTradeName, supLegalName, supLocation, supStateCode,
            custGstin, custOrSupName, custOrSupAddr1, custOrSupAddr2, custOrSupAddr4, billToState, pos,
            tranType, taxScheme, companyCode, profitCentre1, docCat, returnPeriod, isService, itemType As New String("")
        Dim invAssessableAmt, invCgstAmt, invSgstAmt, invIgstAmt, docAmt As New Decimal(0.00)

        If (Left(systemInvoiceNo, 2) = "DC") Then
            docType = "DLC"
        Else
            docType = "INV"
        End If

        docNo = systemInvoiceNo

        reverseCharge = "N"

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            suppGstin = dr.Item("c_gst_no")
            supTradeName = dr.Item("c_name")
            supLegalName = dr.Item("c_name")
            supLocation = dr.Item("c_city")
            supStateCode = dr.Item("c_state_code")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()



        If (Left(buyerPartyCode, 1) = "S") Then
            conn.Open()
            mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                custGstin = dr.Item("SUPL_GST_NO")
                custOrSupName = dr.Item("SUPL_NAME")
                custOrSupAddr1 = dr.Item("SUPL_AT")
                custOrSupAddr2 = dr.Item("SUPL_PO")
                custOrSupAddr4 = dr.Item("SUPL_DIST")
                billToState = dr.Item("SUPL_STATE_CODE")
                pos = dr.Item("SUPL_STATE_CODE")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        ElseIf (Left(buyerPartyCode, 1) = "P") Then
            conn.Open()
            mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objB2CInvoiceWithoutTransId.req.custGstin = dr.Item("SUPL_GST_NO")
                objB2CInvoiceWithoutTransId.req.custOrSupName = dr.Item("SUPL_NAME")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr1 = dr.Item("SUPL_AT")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr2 = dr.Item("SUPL_PO")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr4 = dr.Item("SUPL_DIST")
                objB2CInvoiceWithoutTransId.req.custPincode = dr.Item("SUPL_PIN")
                objB2CInvoiceWithoutTransId.req.billToState = dr.Item("SUPL_STATE_CODE")
                objB2CInvoiceWithoutTransId.req.pos = dr.Item("SUPL_STATE_CODE")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                custGstin = dr.Item("gst_code")
                custOrSupName = dr.Item("d_name")
                custOrSupAddr1 = dr.Item("add_1")
                custOrSupAddr2 = dr.Item("add_2")
                custOrSupAddr4 = dr.Item("d_city")
                billToState = dr.Item("d_state_code")
                pos = dr.Item("d_state_code")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        End If

        tranType = "O"

        taxScheme = "GST"
        companyCode = "1000"
        profitCentre1 = "SRU100"
        docCat = "REG"



        Dim itemDetails As List(Of GSTR1DataItemDetailsEYB2C) = New List(Of GSTR1DataItemDetailsEYB2C)()
        Dim ReqDetailsEY As List(Of GSTR1DataReqDetailsEYB2C) = New List(Of GSTR1DataReqDetailsEYB2C)()

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then

            Else

                transporterWoNo = dr.Item("TRANS_WO")
            End If

            If (isServiceFlag = "YES") Then
                isService = "Y"
                itemType = "S"
            Else
                isService = "N"
                itemType = "G"
            End If

            docDate = Convert.ToDateTime(dr.Item("INV_DATE")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            returnPeriod = Convert.ToDateTime(dr.Item("INV_DATE")).Month.ToString("D2") & Convert.ToDateTime(dr.Item("INV_DATE")).Year
            Dim gstRate As Decimal
            If (CDec(dr.Item("IGST_RATE")) = 0) Then
                gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
            Else
                gstRate = dr.Item("IGST_RATE")
            End If
            Dim supplyType As String = ""
            If (Left(systemInvoiceNo, 2) = "DC") Then
                supplyType = "NSY"
            Else
                supplyType = "TAX"
            End If

            itemDetails.Add(New GSTR1DataItemDetailsEYB2C With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                        .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemType = itemType, .itemUqc = dr.Item("ACC_UNIT"), .plantCode = "CGSRU",
                                        .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                        .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                        .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2),
                                        .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})


            invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
            invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
            invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
            invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

            docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)

            '''''''''''''''''''''''''''''''''
            ReqDetailsEY.Add(New GSTR1DataReqDetailsEYB2C With {.docType = docType, .docNo = docNo, .docDate = docDate, .reverseCharge = reverseCharge,
                                        .suppGstin = suppGstin, .supTradeName = supTradeName, .supLegalName = supLegalName, .supLocation = supLocation, .supStateCode = supStateCode,
                                        .custOrSupName = custOrSupName, .custOrSupAddr1 = custOrSupAddr1, .custOrSupAddr2 = custOrSupAddr2, .custOrSupAddr4 = custOrSupAddr4,
                                        .billToState = billToState, .pos = pos, .tranType = tranType, .taxScheme = taxScheme, .companyCode = companyCode, .profitCentre1 = profitCentre1, .docCat = docCat, .returnPeriod = returnPeriod,
                                        .invAssessableAmt = invAssessableAmt, .invCgstAmt = invCgstAmt, .invSgstAmt = invSgstAmt, .invIgstAmt = invIgstAmt, .docAmt = docAmt, .lineItems = itemDetails})
            '''''''''''''''''''''''''''''''''
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        objGSTR1DataEYB2C.req = ReqDetailsEY

        ''Return the output
        Dim output As String = Json.JsonConvert.SerializeObject(objGSTR1DataEYB2C)
        Return output

    End Function

    Public Function GenerateJsonData(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String)
        Dim working_date As Date = Today.Date
        Dim STR1 As String = ""
        If working_date.Month > 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf working_date.Month <= 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        If (isServiceFlag = "YES") Then
            If (Left(systemInvoiceNo, 2) = "DC") Then
                objService.req.docType = "DLC"
            Else
                objService.req.docType = "INV"
            End If

            objService.req.docNo = systemInvoiceNo
            objService.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            objService.req.reverseCharge = "N"

            conn.Open()
            mc1.CommandText = "select * from comp_profile"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objService.req.suppGstin = dr.Item("c_gst_no")
                objService.req.supTradeName = dr.Item("c_name")
                objService.req.supLegalName = dr.Item("c_name")
                objService.req.supBuildingNo = dr.Item("c_add")
                objService.req.supBuildingName = dr.Item("c_add1")
                objService.req.supLocation = dr.Item("c_city")
                objService.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
                objService.req.supStateCode = dr.Item("c_state_code")
                'objService.req.supPhone = dr.Item("c_contact_no")
                'objService.req.supEmail = dr.Item("c_email")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            If (Left(buyerPartyCode, 1) = "S") Then
                conn.Open()
                mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objService.req.custGstin = dr.Item("SUPL_GST_NO")
                    objService.req.custOrSupName = dr.Item("SUPL_NAME")
                    objService.req.custOrSupAddr1 = dr.Item("SUPL_AT")
                    objService.req.custOrSupAddr2 = dr.Item("SUPL_PO")
                    objService.req.custOrSupAddr4 = dr.Item("SUPL_DIST")
                    objService.req.custPincode = dr.Item("SUPL_PIN")
                    objService.req.billToState = dr.Item("SUPL_STATE_CODE")
                    objService.req.pos = dr.Item("SUPL_STATE_CODE")
                    'objService.req.custPhone = dr.Item("SUPL_MOB1")
                    'objService.req.custEmail = dr.Item("SUPL_EMAIL")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
            Else
                conn.Open()
                mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objService.req.custGstin = dr.Item("gst_code")
                    objService.req.custOrSupName = dr.Item("d_name")
                    objService.req.custOrSupAddr1 = dr.Item("add_1")
                    objService.req.custOrSupAddr2 = dr.Item("add_2")
                    objService.req.custOrSupAddr4 = dr.Item("d_city")
                    objService.req.custPincode = dr.Item("d_pin")
                    objService.req.billToState = dr.Item("d_state_code")
                    objService.req.pos = dr.Item("d_state_code")

                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
            End If


            objService.req.payloadId = payloadId
            objService.req.tcsFlag = tcsFlag
            If (Left(systemInvoiceNo, 2) = "DC") Then
                objService.req.subsupplyType = "FOU"
            Else
                objService.req.subsupplyType = "TAX"
            End If
            'objService.req.distance = "0"
            objService.req.tranType = "O"
            objService.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
            Dim itemDetails As List(Of ItemDetailsEYService) = New List(Of ItemDetailsEYService)()

            conn.Open()
            mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                Dim isService As String = "Y"

                Dim gstRate As Decimal
                If (CDec(dr.Item("IGST_RATE")) = 0) Then
                    gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
                Else
                    gstRate = dr.Item("IGST_RATE")
                End If

                Dim supplyType As String = ""
                If (Left(systemInvoiceNo, 2) = "DC") Then
                    supplyType = "NSY"
                Else
                    supplyType = "TAX"
                End If

                itemDetails.Add(New ItemDetailsEYService With {.itemNo = "1", .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                .hsnsacCode = dr.Item("CHPTR_HEAD"), .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                                .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})

                objService.req.lineItems = itemDetails

                objService.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
                objService.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
                objService.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
                objService.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

                objService.req.roundOff = 0.00
                objService.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            ''Return the output
            Dim output As String = Json.JsonConvert.SerializeObject(objService)
            Return output
        Else
            Dim transporterGSTNo As New String("")
            conn.Open()
            mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                transporterWoNo = dr.Item("TRANS_WO")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()


            If (Left(buyerPartyCode, 1) = "S") Then
                conn.Open()
                mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If (IsDBNull(dr.Item("SUPL_GST_NO"))) Then
                        transporterGSTNo = ""
                    Else
                        transporterGSTNo = dr.Item("SUPL_GST_NO")
                    End If
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
            Else

                conn.Open()
                mc1.CommandText = "select * from dater where d_code in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If (IsDBNull(dr.Item("SUPL_GST_NO"))) Then
                        transporterGSTNo = ""
                    Else
                        transporterGSTNo = dr.Item("SUPL_GST_NO")
                    End If
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
            End If




            Dim outputJSONData As New String("")

            If (transporterWoNo = "PARTY" Or transporterWoNo = "N/A") Then
                outputJSONData = GetJSONDataWithoutTransId(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, STR1)
            Else

                If (transporterGSTNo = "") Then
                    outputJSONData = GetJSONDataWithoutTransId(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, STR1)
                Else
                    outputJSONData = GetJSONDataWithTransId(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, STR1)
                End If

            End If

            Return outputJSONData


        End If

    End Function

    Public Function GenerateJsonData_CNDN(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String, docType As String, originalInvoiceDate As Date)


        Dim despatchInvNo, fiscalYear, originalInvoicefiscalYear As New String("")
        Dim working_date As Date
        conn.Open()
        mc1.CommandText = "select * from CN_DN_DETAILS with(nolock) where DEBIT_CREDIT_NO='" & systemInvoiceNo & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            despatchInvNo = dr.Item("INVOICE_NO")
            fiscalYear = dr.Item("FISCAL_YEAR")
            working_date = dr.Item("DEBIT_CREDIT_DATE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()


        Dim fiscalYearOfOriginalInvoice As String = ""
        If originalInvoiceDate.Month > 3 Then
            originalInvoicefiscalYear = originalInvoiceDate.Year
            originalInvoicefiscalYear = originalInvoicefiscalYear.Trim.Substring(2)
            originalInvoicefiscalYear = originalInvoicefiscalYear & (originalInvoicefiscalYear + 1)
        ElseIf originalInvoiceDate.Month <= 3 Then
            originalInvoicefiscalYear = originalInvoiceDate.Year
            originalInvoicefiscalYear = originalInvoicefiscalYear.Trim.Substring(2)
            originalInvoicefiscalYear = (originalInvoicefiscalYear - 1) & originalInvoicefiscalYear
        End If


        If (isServiceFlag = "YES") Then
            objService.req.docType = docType

            objService.req.docNo = systemInvoiceNo

            objService.req.reverseCharge = "N"

            conn.Open()
            mc1.CommandText = "select * from comp_profile"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objService.req.suppGstin = dr.Item("c_gst_no")
                objService.req.supTradeName = dr.Item("c_name")
                objService.req.supLegalName = dr.Item("c_name")
                objService.req.supBuildingNo = dr.Item("c_add")
                objService.req.supBuildingName = dr.Item("c_add1")
                objService.req.supLocation = dr.Item("c_city")
                objService.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
                objService.req.supStateCode = dr.Item("c_state_code")
                'objService.req.supPhone = dr.Item("c_contact_no")
                'objService.req.supEmail = dr.Item("c_email")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()



            If (Left(buyerPartyCode, 1) = "S") Then
                conn.Open()
                mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objService.req.custGstin = dr.Item("SUPL_GST_NO")
                    objService.req.custOrSupName = dr.Item("SUPL_NAME")
                    objService.req.custOrSupAddr1 = dr.Item("SUPL_AT")
                    objService.req.custOrSupAddr2 = dr.Item("SUPL_PO")
                    objService.req.custOrSupAddr4 = dr.Item("SUPL_DIST")
                    objService.req.custPincode = dr.Item("SUPL_PIN")
                    objService.req.billToState = dr.Item("SUPL_STATE_CODE")
                    objService.req.pos = dr.Item("SUPL_STATE_CODE")
                    'objService.req.custPhone = dr.Item("SUPL_MOB1")
                    'objService.req.custEmail = dr.Item("SUPL_EMAIL")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
            Else
                conn.Open()
                mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objService.req.custGstin = dr.Item("gst_code")
                    objService.req.custOrSupName = dr.Item("d_name")
                    objService.req.custOrSupAddr1 = dr.Item("add_1")
                    objService.req.custOrSupAddr2 = dr.Item("add_2")
                    objService.req.custOrSupAddr4 = dr.Item("d_city")
                    objService.req.custPincode = dr.Item("d_pin")
                    objService.req.billToState = dr.Item("d_state_code")
                    objService.req.pos = dr.Item("d_state_code")

                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
            End If


            objService.req.payloadId = payloadId
            objService.req.tcsFlag = tcsFlag
            If (Left(systemInvoiceNo, 2) = "DC") Then
                objService.req.subsupplyType = "FOU"
            Else
                objService.req.subsupplyType = "TAX"
            End If
            'objService.req.distance = "0"
            objService.req.tranType = "O"

            Dim itemDetails As List(Of ItemDetailsEYService) = New List(Of ItemDetailsEYService)()

            conn.Open()
            mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & despatchInvNo & "' and FISCAL_YEAR='" & originalInvoicefiscalYear & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()



                objService.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                objService.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
                Dim isService As String = "Y"

                Dim gstRate As Decimal
                If (CDec(dr.Item("IGST_RATE")) = 0) Then
                    gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
                Else
                    gstRate = dr.Item("IGST_RATE")
                End If

                Dim supplyType As String = ""
                If (Left(systemInvoiceNo, 2) = "DC") Then
                    supplyType = "NSY"
                Else
                    supplyType = "TAX"
                End If

                itemDetails.Add(New ItemDetailsEYService With {.itemNo = "1", .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                .hsnsacCode = dr.Item("CHPTR_HEAD"), .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                                .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})

                objService.req.lineItems = itemDetails

                objService.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
                objService.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
                objService.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
                objService.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

                objService.req.roundOff = 0.00
                objService.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            ''Return the output
            Dim output As String = Json.JsonConvert.SerializeObject(objService)
            Return output
        Else


            objFinishedGoodsWithoutTransID.req.docType = docType

            objFinishedGoodsWithoutTransID.req.docNo = systemInvoiceNo
            objFinishedGoodsWithoutTransID.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            objFinishedGoodsWithoutTransID.req.reverseCharge = "N"

            conn.Open()
            mc1.CommandText = "select * from comp_profile"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objFinishedGoodsWithoutTransID.req.suppGstin = dr.Item("c_gst_no")
                objFinishedGoodsWithoutTransID.req.supTradeName = dr.Item("c_name")
                objFinishedGoodsWithoutTransID.req.supLegalName = dr.Item("c_name")
                objFinishedGoodsWithoutTransID.req.supBuildingNo = dr.Item("c_add")
                objFinishedGoodsWithoutTransID.req.supBuildingName = dr.Item("c_add1")
                objFinishedGoodsWithoutTransID.req.supLocation = dr.Item("c_city")
                objFinishedGoodsWithoutTransID.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
                objFinishedGoodsWithoutTransID.req.supStateCode = dr.Item("c_state_code")
                'basic.req.supPhone = dr.Item("c_contact_no")
                'basic.req.supEmail = dr.Item("c_email")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objFinishedGoodsWithoutTransID.req.custGstin = dr.Item("gst_code")
                objFinishedGoodsWithoutTransID.req.custOrSupName = dr.Item("d_name")
                objFinishedGoodsWithoutTransID.req.custOrSupAddr1 = dr.Item("add_1")
                objFinishedGoodsWithoutTransID.req.custOrSupAddr2 = dr.Item("add_2")
                objFinishedGoodsWithoutTransID.req.custOrSupAddr4 = dr.Item("d_city")
                objFinishedGoodsWithoutTransID.req.custPincode = dr.Item("d_pin")
                objFinishedGoodsWithoutTransID.req.billToState = dr.Item("d_state_code")
                objFinishedGoodsWithoutTransID.req.pos = dr.Item("d_state_code")
                'basic.req.custPhone = dr.Item("d_contact")
                'basic.req.custEmail = dr.Item("d_email")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & consigneePartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objFinishedGoodsWithoutTransID.req.shipToState = dr.Item("d_state_code")
                objFinishedGoodsWithoutTransID.req.shipToLegalName = dr.Item("d_name")
                objFinishedGoodsWithoutTransID.req.shipToBuildingNo = dr.Item("add_1")
                objFinishedGoodsWithoutTransID.req.shipToLocation = dr.Item("d_city")
                objFinishedGoodsWithoutTransID.req.shipToPincode = dr.Item("d_pin")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            objFinishedGoodsWithoutTransID.req.tcsFlag = tcsFlag
            objFinishedGoodsWithoutTransID.req.tranType = "O"

            If (Left(systemInvoiceNo, 2) = "DC") Then
                objFinishedGoodsWithoutTransID.req.subsupplyType = "FOU"
            Else
                objFinishedGoodsWithoutTransID.req.subsupplyType = "TAX"
            End If

            objFinishedGoodsWithoutTransID.req.docCat = "REG"
            objFinishedGoodsWithoutTransID.req.transportMode = "ROAD"

            If (objFinishedGoodsWithoutTransID.req.supPincode = objFinishedGoodsWithoutTransID.req.shipToPincode) Then
                objFinishedGoodsWithoutTransID.req.distance = "100"
            Else
                objFinishedGoodsWithoutTransID.req.distance = "0"
            End If


            objFinishedGoodsWithoutTransID.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
            objFinishedGoodsWithoutTransID.req.payloadId = payloadId
            Dim itemDetails As List(Of ItemDetailsWithoutTransporterIdEY) = New List(Of ItemDetailsWithoutTransporterIdEY)()

            conn.Open()
            mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & despatchInvNo & "' and FISCAL_YEAR='" & originalInvoicefiscalYear & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then
                    objFinishedGoodsWithoutTransID.req.transporterName = "Transportation arranged by customer"
                Else
                    objFinishedGoodsWithoutTransID.req.transporterName = dr.Item("TRANS_NAME")
                    transporterWoNo = dr.Item("TRANS_WO")
                End If


                objFinishedGoodsWithoutTransID.req.vehicleNo = dr.Item("TRUCK_NO")
                objFinishedGoodsWithoutTransID.req.vehicleType = "R"
                Dim isService As String = "N"

                Dim gstRate As Decimal
                If (CDec(dr.Item("IGST_RATE")) = 0) Then
                    gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
                Else
                    gstRate = dr.Item("IGST_RATE")
                End If
                Dim supplyType As String = ""
                If (Left(systemInvoiceNo, 2) = "DC") Then
                    supplyType = "NSY"
                Else
                    supplyType = "TAX"
                End If

                itemDetails.Add(New ItemDetailsWithoutTransporterIdEY With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                    .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemUqc = dr.Item("ACC_UNIT"),
                                    .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                    .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                    .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                                    .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})

                objFinishedGoodsWithoutTransID.req.lineItems = itemDetails

                objFinishedGoodsWithoutTransID.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
                objFinishedGoodsWithoutTransID.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
                objFinishedGoodsWithoutTransID.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
                objFinishedGoodsWithoutTransID.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

                objFinishedGoodsWithoutTransID.req.roundOff = 0.00
                objFinishedGoodsWithoutTransID.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            ''Return the output
            Dim output As String = Json.JsonConvert.SerializeObject(objFinishedGoodsWithoutTransID)
            Return output

        End If

    End Function

    Public Function GenerateJsonDataForB2C(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String)

        Dim working_date As Date = Today.Date
        Dim STR1 As String = ""
        If working_date.Month > 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf working_date.Month <= 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        ''''''''''''''''''''''''''''''''''''''''
        Dim transporterGSTNo As New String("")
        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            transporterWoNo = dr.Item("TRANS_WO")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (IsDBNull(dr.Item("SUPL_GST_NO"))) Then
                transporterGSTNo = ""
            Else
                transporterGSTNo = dr.Item("SUPL_GST_NO")
            End If
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim outputJSONData As New String("")

        If (transporterWoNo = "PARTY" Or transporterWoNo = "N/A") Then
            objB2CInvoiceWithoutTransId.req.docType = "INV"

            objB2CInvoiceWithoutTransId.req.docNo = systemInvoiceNo
            objB2CInvoiceWithoutTransId.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            objB2CInvoiceWithoutTransId.req.reverseCharge = "N"

            conn.Open()
            mc1.CommandText = "select * from comp_profile"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objB2CInvoiceWithoutTransId.req.suppGstin = dr.Item("c_gst_no")
                objB2CInvoiceWithoutTransId.req.supTradeName = dr.Item("c_name")
                objB2CInvoiceWithoutTransId.req.supLegalName = dr.Item("c_name")
                objB2CInvoiceWithoutTransId.req.supBuildingNo = dr.Item("c_add")
                objB2CInvoiceWithoutTransId.req.supBuildingName = dr.Item("c_add1")
                objB2CInvoiceWithoutTransId.req.supLocation = dr.Item("c_city")
                objB2CInvoiceWithoutTransId.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
                objB2CInvoiceWithoutTransId.req.supStateCode = dr.Item("c_state_code")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            If (Left(buyerPartyCode, 1) = "S") Then
                conn.Open()
                mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objB2CInvoiceWithoutTransId.req.custGstin = dr.Item("SUPL_GST_NO")
                    objB2CInvoiceWithoutTransId.req.custOrSupName = dr.Item("SUPL_NAME")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr1 = dr.Item("SUPL_AT")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr2 = dr.Item("SUPL_PO")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr4 = dr.Item("SUPL_DIST")
                    objB2CInvoiceWithoutTransId.req.custPincode = dr.Item("SUPL_PIN")
                    objB2CInvoiceWithoutTransId.req.billToState = dr.Item("SUPL_STATE_CODE")
                    objB2CInvoiceWithoutTransId.req.pos = dr.Item("SUPL_STATE_CODE")

                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

            ElseIf (Left(buyerPartyCode, 1) = "P") Then
                conn.Open()
                mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objB2CInvoiceWithoutTransId.req.custGstin = dr.Item("SUPL_GST_NO")
                    objB2CInvoiceWithoutTransId.req.custOrSupName = dr.Item("SUPL_NAME")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr1 = dr.Item("SUPL_AT")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr2 = dr.Item("SUPL_PO")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr4 = dr.Item("SUPL_DIST")
                    objB2CInvoiceWithoutTransId.req.custPincode = dr.Item("SUPL_PIN")
                    objB2CInvoiceWithoutTransId.req.billToState = dr.Item("SUPL_STATE_CODE")
                    objB2CInvoiceWithoutTransId.req.pos = dr.Item("SUPL_STATE_CODE")

                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

            Else
                conn.Open()
                mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objB2CInvoiceWithoutTransId.req.custGstin = dr.Item("gst_code")
                    objB2CInvoiceWithoutTransId.req.custOrSupName = dr.Item("d_name")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr1 = dr.Item("add_1")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr2 = dr.Item("add_2")
                    objB2CInvoiceWithoutTransId.req.custOrSupAddr4 = dr.Item("d_city")
                    objB2CInvoiceWithoutTransId.req.custPincode = dr.Item("d_pin")
                    objB2CInvoiceWithoutTransId.req.billToState = dr.Item("d_state_code")
                    objB2CInvoiceWithoutTransId.req.pos = dr.Item("d_state_code")

                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
            End If



            objB2CInvoiceWithoutTransId.req.tcsFlag = tcsFlag
            objB2CInvoiceWithoutTransId.req.tranType = "O"

            objB2CInvoiceWithoutTransId.req.subsupplyType = "TAX"

            objB2CInvoiceWithoutTransId.req.docCat = "REG"
            objB2CInvoiceWithoutTransId.req.transportMode = "ROAD"

            If (objB2CInvoiceWithoutTransId.req.supPincode = objB2CInvoiceWithoutTransId.req.custPincode) Then
                objB2CInvoiceWithoutTransId.req.distance = "100"
            Else
                objB2CInvoiceWithoutTransId.req.distance = "0"
            End If


            objB2CInvoiceWithoutTransId.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
            objB2CInvoiceWithoutTransId.req.payloadId = payloadId

            objB2CInvoiceWithoutTransId.req.companyCode = "CGSRU"
            Dim itemDetails As List(Of B2CInvoiceWithDynamicQRCodeItemDetailsEYWithoutTransId) = New List(Of B2CInvoiceWithDynamicQRCodeItemDetailsEYWithoutTransId)()

            conn.Open()
            mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then
                    objB2CInvoiceWithoutTransId.req.transporterName = "Transportation arranged by customer"
                Else
                    objB2CInvoiceWithoutTransId.req.transporterName = dr.Item("TRANS_NAME")
                    transporterWoNo = dr.Item("TRANS_WO")
                End If


                'objB2CInvoiceWithoutTransId.req.vehicleNo = dr.Item("TRUCK_NO")
                'objB2CInvoiceWithoutTransId.req.vehicleNo = "CG07MB5783"
                objB2CInvoiceWithoutTransId.req.vehicleType = "R"

                Dim isService As New String("")
                If (isServiceFlag = "YES") Then
                    isService = "Y"
                Else
                    isService = "N"
                End If

                Dim gstRate As Decimal
                If (CDec(dr.Item("IGST_RATE")) = 0) Then
                    gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
                Else
                    gstRate = dr.Item("IGST_RATE")
                End If
                Dim supplyType As String = ""
                If (Left(systemInvoiceNo, 2) = "DC") Then
                    supplyType = "NSY"
                Else
                    supplyType = "TAX"
                End If

                itemDetails.Add(New B2CInvoiceWithDynamicQRCodeItemDetailsEYWithoutTransId With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                    .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemUqc = dr.Item("ACC_UNIT"),
                                    .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                    .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                    .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                                    .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2), .plantCode = "CGSRU"})

                objB2CInvoiceWithoutTransId.req.lineItems = itemDetails

                objB2CInvoiceWithoutTransId.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
                objB2CInvoiceWithoutTransId.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
                objB2CInvoiceWithoutTransId.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
                objB2CInvoiceWithoutTransId.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

                objB2CInvoiceWithoutTransId.req.roundOff = 0.00
                objB2CInvoiceWithoutTransId.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            ''Return the output
            outputJSONData = Json.JsonConvert.SerializeObject(objB2CInvoiceWithoutTransId)

        Else

            If (transporterGSTNo = "") Then
                objB2CInvoiceWithTransId.req.docType = "INV"

                objB2CInvoiceWithTransId.req.docNo = systemInvoiceNo
                objB2CInvoiceWithTransId.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                objB2CInvoiceWithTransId.req.reverseCharge = "N"

                conn.Open()
                mc1.CommandText = "select * from comp_profile"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    objB2CInvoiceWithTransId.req.suppGstin = dr.Item("c_gst_no")
                    objB2CInvoiceWithTransId.req.supTradeName = dr.Item("c_name")
                    objB2CInvoiceWithTransId.req.supLegalName = dr.Item("c_name")
                    objB2CInvoiceWithTransId.req.supBuildingNo = dr.Item("c_add")
                    objB2CInvoiceWithTransId.req.supBuildingName = dr.Item("c_add1")
                    objB2CInvoiceWithTransId.req.supLocation = dr.Item("c_city")
                    objB2CInvoiceWithTransId.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
                    objB2CInvoiceWithTransId.req.supStateCode = dr.Item("c_state_code")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()


                If (Left(buyerPartyCode, 1) = "S") Then
                    conn.Open()
                    mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()

                        objB2CInvoiceWithTransId.req.custGstin = dr.Item("SUPL_GST_NO")
                        objB2CInvoiceWithTransId.req.custOrSupName = dr.Item("SUPL_NAME")
                        objB2CInvoiceWithTransId.req.custOrSupAddr1 = dr.Item("SUPL_AT")
                        objB2CInvoiceWithTransId.req.custOrSupAddr2 = dr.Item("SUPL_PO")
                        objB2CInvoiceWithTransId.req.custOrSupAddr4 = dr.Item("SUPL_DIST")
                        objB2CInvoiceWithTransId.req.custPincode = dr.Item("SUPL_PIN")
                        objB2CInvoiceWithTransId.req.billToState = dr.Item("SUPL_STATE_CODE")
                        objB2CInvoiceWithTransId.req.pos = dr.Item("SUPL_STATE_CODE")

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                Else
                    conn.Open()
                    mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()

                        objB2CInvoiceWithTransId.req.custGstin = dr.Item("gst_code")
                        objB2CInvoiceWithTransId.req.custOrSupName = dr.Item("d_name")
                        objB2CInvoiceWithTransId.req.custOrSupAddr1 = dr.Item("add_1")
                        objB2CInvoiceWithTransId.req.custOrSupAddr2 = dr.Item("add_2")
                        objB2CInvoiceWithTransId.req.custOrSupAddr4 = dr.Item("d_city")
                        objB2CInvoiceWithTransId.req.custPincode = dr.Item("d_pin")
                        objB2CInvoiceWithTransId.req.billToState = dr.Item("d_state_code")
                        objB2CInvoiceWithTransId.req.pos = dr.Item("d_state_code")

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                End If



                objB2CInvoiceWithTransId.req.tcsFlag = tcsFlag
                objB2CInvoiceWithTransId.req.tranType = "O"

                objB2CInvoiceWithTransId.req.subsupplyType = "TAX"

                objB2CInvoiceWithTransId.req.docCat = "REG"
                objB2CInvoiceWithTransId.req.transportMode = "ROAD"
                objB2CInvoiceWithTransId.req.distance = "0"

                If (objB2CInvoiceWithTransId.req.supPincode = objB2CInvoiceWithTransId.req.custPincode) Then
                    objB2CInvoiceWithTransId.req.distance = "100"
                Else
                    objB2CInvoiceWithTransId.req.distance = "0"
                End If

                objB2CInvoiceWithTransId.req.companyCode = "CGSRU"


                objB2CInvoiceWithTransId.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
                objB2CInvoiceWithTransId.req.payloadId = payloadId
                Dim itemDetails As List(Of B2CInvoiceWithDynamicQRCodeItemDetailsEY) = New List(Of B2CInvoiceWithDynamicQRCodeItemDetailsEY)()

                conn.Open()
                mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then
                        objB2CInvoiceWithTransId.req.transporterName = "Transportation arranged by customer"
                    Else
                        objB2CInvoiceWithTransId.req.transporterName = dr.Item("TRANS_NAME")
                        transporterWoNo = dr.Item("TRANS_WO")
                    End If


                    objB2CInvoiceWithTransId.req.vehicleNo = dr.Item("TRUCK_NO")
                    'objB2CInvoice.req.vehicleNo = "CG07MB5783"
                    objB2CInvoiceWithTransId.req.vehicleType = "R"
                    Dim isService As String = "N"

                    Dim gstRate As Decimal
                    If (CDec(dr.Item("IGST_RATE")) = 0) Then
                        gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
                    Else
                        gstRate = dr.Item("IGST_RATE")
                    End If
                    Dim supplyType As String = ""
                    If (Left(systemInvoiceNo, 2) = "DC") Then
                        supplyType = "NSY"
                    Else
                        supplyType = "TAX"
                    End If

                    itemDetails.Add(New B2CInvoiceWithDynamicQRCodeItemDetailsEY With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                            .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemUqc = dr.Item("ACC_UNIT"),
                            .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                            .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                            .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                            .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2), .plantCode = "CGSRU"})

                    objB2CInvoiceWithTransId.req.lineItems = itemDetails

                    objB2CInvoiceWithTransId.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
                    objB2CInvoiceWithTransId.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
                    objB2CInvoiceWithTransId.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
                    objB2CInvoiceWithTransId.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

                    objB2CInvoiceWithTransId.req.roundOff = 0.00
                    objB2CInvoiceWithTransId.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

                ''Getting transporter GST No.
                conn.Open()
                mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    objB2CInvoiceWithTransId.req.transporterID = dr.Item("SUPL_GST_NO")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

                ''Return the output
                outputJSONData = Json.JsonConvert.SerializeObject(objB2CInvoiceWithTransId)

            Else
                outputJSONData = GetJSONDataWithTransId(systemInvoiceNo, buyerPartyCode, consigneePartyCode, isServiceFlag, payloadId, tcsFlag, STR1)
            End If

        End If

        Return outputJSONData
        ''''''''''''''''''''''''''''''''''''''''




    End Function

    Public Function GenerateJsonDataForB2C_CNDN(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String, docType As String)

        Dim working_date As Date

        ''''''''''''''''''''''''''''''''''''''''
        Dim despatchInvNo As New String("")
        conn.Open()
        mc1.CommandText = "select * from CN_DN_DETAILS with(nolock) where DEBIT_CREDIT_NO='" & systemInvoiceNo & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            despatchInvNo = dr.Item("INVOICE_NO")
            working_date = dr.Item("DEBIT_CREDIT_DATE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim STR1 As String = ""
        If working_date.Month > 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf working_date.Month <= 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        Dim outputJSONData As New String("")


        objB2CInvoiceWithoutTransId.req.docType = docType

        objB2CInvoiceWithoutTransId.req.docNo = systemInvoiceNo
        objB2CInvoiceWithoutTransId.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        objB2CInvoiceWithoutTransId.req.reverseCharge = "N"

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            objB2CInvoiceWithoutTransId.req.suppGstin = dr.Item("c_gst_no")
            objB2CInvoiceWithoutTransId.req.supTradeName = dr.Item("c_name")
            objB2CInvoiceWithoutTransId.req.supLegalName = dr.Item("c_name")
            objB2CInvoiceWithoutTransId.req.supBuildingNo = dr.Item("c_add")
            objB2CInvoiceWithoutTransId.req.supBuildingName = dr.Item("c_add1")
            objB2CInvoiceWithoutTransId.req.supLocation = dr.Item("c_city")
            objB2CInvoiceWithoutTransId.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
            objB2CInvoiceWithoutTransId.req.supStateCode = dr.Item("c_state_code")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If (Left(buyerPartyCode, 1) = "S") Then
            conn.Open()
            mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objB2CInvoiceWithoutTransId.req.custGstin = dr.Item("SUPL_GST_NO")
                objB2CInvoiceWithoutTransId.req.custOrSupName = dr.Item("SUPL_NAME")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr1 = dr.Item("SUPL_AT")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr2 = dr.Item("SUPL_PO")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr4 = dr.Item("SUPL_DIST")
                objB2CInvoiceWithoutTransId.req.custPincode = dr.Item("SUPL_PIN")
                objB2CInvoiceWithoutTransId.req.billToState = dr.Item("SUPL_STATE_CODE")
                objB2CInvoiceWithoutTransId.req.pos = dr.Item("SUPL_STATE_CODE")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                objB2CInvoiceWithoutTransId.req.custGstin = dr.Item("gst_code")
                objB2CInvoiceWithoutTransId.req.custOrSupName = dr.Item("d_name")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr1 = dr.Item("add_1")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr2 = dr.Item("add_2")
                objB2CInvoiceWithoutTransId.req.custOrSupAddr4 = dr.Item("d_city")
                objB2CInvoiceWithoutTransId.req.custPincode = dr.Item("d_pin")
                objB2CInvoiceWithoutTransId.req.billToState = dr.Item("d_state_code")
                objB2CInvoiceWithoutTransId.req.pos = dr.Item("d_state_code")

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        End If



        objB2CInvoiceWithoutTransId.req.tcsFlag = tcsFlag
        objB2CInvoiceWithoutTransId.req.tranType = "O"

        objB2CInvoiceWithoutTransId.req.subsupplyType = "TAX"

        objB2CInvoiceWithoutTransId.req.docCat = "REG"
        'objB2CInvoiceWithoutTransId.req.transportMode = "ROAD"

        If (objB2CInvoiceWithoutTransId.req.supPincode = objB2CInvoiceWithoutTransId.req.custPincode) Then
            objB2CInvoiceWithoutTransId.req.distance = "100"
        Else
            objB2CInvoiceWithoutTransId.req.distance = "0"
        End If


        objB2CInvoiceWithoutTransId.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
        objB2CInvoiceWithoutTransId.req.payloadId = payloadId

        objB2CInvoiceWithoutTransId.req.companyCode = "CGSRU"
        Dim itemDetails As List(Of B2CInvoiceWithDynamicQRCodeItemDetailsEYWithoutTransId) = New List(Of B2CInvoiceWithDynamicQRCodeItemDetailsEYWithoutTransId)()

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & despatchInvNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            'If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then
            '    objB2CInvoiceWithoutTransId.req.transporterName = "Transportation arranged by customer"
            'Else
            '    objB2CInvoiceWithoutTransId.req.transporterName = dr.Item("TRANS_NAME")
            '    despatchInvNo = dr.Item("TRANS_WO")
            'End If


            'objB2CInvoiceWithoutTransId.req.vehicleNo = dr.Item("TRUCK_NO")
            'objB2CInvoiceWithoutTransId.req.vehicleType = "R"
            Dim isService As String
            If (isServiceFlag = "YES") Then
                isService = "Y"
            Else
                isService = "N"
            End If

            Dim gstRate As Decimal
            If (CDec(dr.Item("IGST_RATE")) = 0) Then
                gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
            Else
                gstRate = dr.Item("IGST_RATE")
            End If
            Dim supplyType As String = ""
            If (Left(systemInvoiceNo, 2) = "DC") Then
                supplyType = "NSY"
            Else
                supplyType = "TAX"
            End If

            itemDetails.Add(New B2CInvoiceWithDynamicQRCodeItemDetailsEYWithoutTransId With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemUqc = dr.Item("ACC_UNIT"),
                                .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                                .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2), .plantCode = "CGSRU"})

            objB2CInvoiceWithoutTransId.req.lineItems = itemDetails

            objB2CInvoiceWithoutTransId.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
            objB2CInvoiceWithoutTransId.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
            objB2CInvoiceWithoutTransId.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
            objB2CInvoiceWithoutTransId.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

            objB2CInvoiceWithoutTransId.req.roundOff = 0.00
            objB2CInvoiceWithoutTransId.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        ''Return the output
        outputJSONData = Json.JsonConvert.SerializeObject(objB2CInvoiceWithoutTransId)

        Return outputJSONData
        ''''''''''''''''''''''''''''''''''''''''




    End Function

    Public Function GetJSONDataWithTransId(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String, STR1 As String)
        Dim working_date As Date = Today.Date

        'Dim working_date As Date = Convert.ToDateTime("2022-05-27")
        If (Left(systemInvoiceNo, 2) = "DC") Then
            objFinishedGoods.req.docType = "DLC"
        Else
            objFinishedGoods.req.docType = "INV"
        End If

        objFinishedGoods.req.docNo = systemInvoiceNo
        objFinishedGoods.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        objFinishedGoods.req.reverseCharge = "N"

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            objFinishedGoods.req.suppGstin = dr.Item("c_gst_no")
            objFinishedGoods.req.supTradeName = dr.Item("c_name")
            objFinishedGoods.req.supLegalName = dr.Item("c_name")
            objFinishedGoods.req.supBuildingNo = dr.Item("c_add")
            objFinishedGoods.req.supBuildingName = dr.Item("c_add1")
            objFinishedGoods.req.supLocation = dr.Item("c_city")
            objFinishedGoods.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
            objFinishedGoods.req.supStateCode = dr.Item("c_state_code")
            'basic.req.supPhone = dr.Item("c_contact_no")
            'basic.req.supEmail = dr.Item("c_email")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            objFinishedGoods.req.custGstin = dr.Item("gst_code")
            objFinishedGoods.req.custOrSupName = dr.Item("d_name")
            objFinishedGoods.req.custOrSupAddr1 = dr.Item("add_1")
            objFinishedGoods.req.custOrSupAddr2 = dr.Item("add_2")
            objFinishedGoods.req.custOrSupAddr4 = dr.Item("d_city")
            objFinishedGoods.req.custPincode = dr.Item("d_pin")
            objFinishedGoods.req.billToState = dr.Item("d_state_code")
            objFinishedGoods.req.pos = dr.Item("d_state_code")
            'basic.req.custPhone = dr.Item("d_contact")
            'basic.req.custEmail = dr.Item("d_email")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from dater where d_code='" & consigneePartyCode & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            objFinishedGoods.req.shipToState = dr.Item("d_state_code")
            objFinishedGoods.req.shipToLegalName = dr.Item("d_name")
            objFinishedGoods.req.shipToBuildingNo = dr.Item("add_1")
            objFinishedGoods.req.shipToLocation = dr.Item("d_city")
            objFinishedGoods.req.shipToPincode = dr.Item("d_pin")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        objFinishedGoods.req.tcsFlag = tcsFlag
        objFinishedGoods.req.tranType = "O"

        If (Left(systemInvoiceNo, 2) = "DC") Then
            objFinishedGoods.req.subsupplyType = "FOU"
        Else
            objFinishedGoods.req.subsupplyType = "TAX"
        End If

        objFinishedGoods.req.docCat = "REG"
        objFinishedGoods.req.transportMode = "ROAD"

        If (objFinishedGoods.req.supPincode = objFinishedGoods.req.shipToPincode) Then
            objFinishedGoods.req.distance = "100"
        Else
            objFinishedGoods.req.distance = "0"
        End If


        objFinishedGoods.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
        objFinishedGoods.req.payloadId = payloadId
        Dim itemDetails As List(Of ItemDetailsEY) = New List(Of ItemDetailsEY)()

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then
                objFinishedGoods.req.transporterName = "Transportation arranged by customer"
            Else
                objFinishedGoods.req.transporterName = dr.Item("TRANS_NAME")
                transporterWoNo = dr.Item("TRANS_WO")
            End If


            objFinishedGoods.req.vehicleNo = dr.Item("TRUCK_NO")
            objFinishedGoods.req.vehicleType = "R"
            Dim isService As String = "N"

            Dim gstRate As Decimal
            If (CDec(dr.Item("IGST_RATE")) = 0) Then
                gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
            Else
                gstRate = dr.Item("IGST_RATE")
            End If
            Dim supplyType As String = ""
            If (Left(systemInvoiceNo, 2) = "DC") Then
                supplyType = "NSY"
            Else
                supplyType = "TAX"
            End If

            itemDetails.Add(New ItemDetailsEY With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                            .hsnsacCode = dr.Item("CHPTR_HEAD"), .itemQty = dr.Item("TOTAL_QTY"), .itemUqc = dr.Item("ACC_UNIT"),
                            .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                            .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                            .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                            .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})

            objFinishedGoods.req.lineItems = itemDetails

            objFinishedGoods.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
            objFinishedGoods.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
            objFinishedGoods.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
            objFinishedGoods.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

            objFinishedGoods.req.roundOff = 0.00
            objFinishedGoods.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        ''Getting transporter GST No.
        conn.Open()
        mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            objFinishedGoods.req.transporterID = dr.Item("SUPL_GST_NO")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        ''Return the output
        Dim output As String = Json.JsonConvert.SerializeObject(objFinishedGoods)
        Return output
    End Function

    Public Function GetJSONDataWithoutTransId(systemInvoiceNo As String, buyerPartyCode As String, consigneePartyCode As String, isServiceFlag As String, payloadId As String, tcsFlag As String, STR1 As String)
        Dim working_date As Date = Today.Date
        If (Left(systemInvoiceNo, 2) = "DC") Then
            objFinishedGoodsWithoutTransID.req.docType = "DLC"
        Else
            objFinishedGoodsWithoutTransID.req.docType = "INV"
        End If

        objFinishedGoodsWithoutTransID.req.docNo = systemInvoiceNo
        objFinishedGoodsWithoutTransID.req.docDate = working_date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
        objFinishedGoodsWithoutTransID.req.reverseCharge = "N"

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            objFinishedGoodsWithoutTransID.req.suppGstin = dr.Item("c_gst_no")
            objFinishedGoodsWithoutTransID.req.supTradeName = dr.Item("c_name")
            objFinishedGoodsWithoutTransID.req.supLegalName = dr.Item("c_name")
            objFinishedGoodsWithoutTransID.req.supBuildingNo = dr.Item("c_add")
            objFinishedGoodsWithoutTransID.req.supBuildingName = dr.Item("c_add1")
            objFinishedGoodsWithoutTransID.req.supLocation = dr.Item("c_city")
            objFinishedGoodsWithoutTransID.req.supPincode = Convert.ToInt32(dr.Item("c_pin"))
            objFinishedGoodsWithoutTransID.req.supStateCode = dr.Item("c_state_code")
            'basic.req.supPhone = dr.Item("c_contact_no")
            'basic.req.supEmail = dr.Item("c_email")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            objFinishedGoodsWithoutTransID.req.custGstin = dr.Item("gst_code")
            objFinishedGoodsWithoutTransID.req.custOrSupName = dr.Item("d_name")
            objFinishedGoodsWithoutTransID.req.custOrSupAddr1 = dr.Item("add_1")
            objFinishedGoodsWithoutTransID.req.custOrSupAddr2 = dr.Item("add_2")
            objFinishedGoodsWithoutTransID.req.custOrSupAddr4 = dr.Item("d_city")
            objFinishedGoodsWithoutTransID.req.custPincode = dr.Item("d_pin")
            objFinishedGoodsWithoutTransID.req.billToState = dr.Item("d_state_code")
            objFinishedGoodsWithoutTransID.req.pos = dr.Item("d_state_code")
            'basic.req.custPhone = dr.Item("d_contact")
            'basic.req.custEmail = dr.Item("d_email")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from dater where d_code='" & consigneePartyCode & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            objFinishedGoodsWithoutTransID.req.shipToState = dr.Item("d_state_code")
            objFinishedGoodsWithoutTransID.req.shipToLegalName = dr.Item("d_name")
            objFinishedGoodsWithoutTransID.req.shipToBuildingNo = dr.Item("add_1")
            objFinishedGoodsWithoutTransID.req.shipToLocation = dr.Item("d_city")
            objFinishedGoodsWithoutTransID.req.shipToPincode = dr.Item("d_pin")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        objFinishedGoodsWithoutTransID.req.tcsFlag = tcsFlag
        objFinishedGoodsWithoutTransID.req.tranType = "O"

        If (Left(systemInvoiceNo, 2) = "DC") Then
            objFinishedGoodsWithoutTransID.req.subsupplyType = "FOU"
        Else
            objFinishedGoodsWithoutTransID.req.subsupplyType = "TAX"
        End If

        objFinishedGoodsWithoutTransID.req.docCat = "REG"
        objFinishedGoodsWithoutTransID.req.transportMode = "ROAD"

        If (objFinishedGoodsWithoutTransID.req.supPincode = objFinishedGoodsWithoutTransID.req.shipToPincode) Then
            objFinishedGoodsWithoutTransID.req.distance = "100"
        Else
            objFinishedGoodsWithoutTransID.req.distance = "0"
        End If


        objFinishedGoodsWithoutTransID.req.returnPeriod = working_date.Month.ToString("D2") & working_date.Year
        objFinishedGoodsWithoutTransID.req.payloadId = payloadId
        Dim itemDetails As List(Of ItemDetailsWithoutTransporterIdEY) = New List(Of ItemDetailsWithoutTransporterIdEY)()

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (dr.Item("TRANS_NAME") = "" Or dr.Item("TRANS_NAME") = "N/A") Then
                objFinishedGoodsWithoutTransID.req.transporterName = "Transportation arranged by customer"
            Else
                objFinishedGoodsWithoutTransID.req.transporterName = dr.Item("TRANS_NAME")
                transporterWoNo = dr.Item("TRANS_WO")
            End If


            objFinishedGoodsWithoutTransID.req.vehicleNo = dr.Item("TRUCK_NO")
            objFinishedGoodsWithoutTransID.req.vehicleType = "R"
            Dim isService As String = "N"

            Dim gstRate As Decimal
            If (CDec(dr.Item("IGST_RATE")) = 0) Then
                gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
            Else
                gstRate = dr.Item("IGST_RATE")
            End If
            Dim supplyType As String = ""
            If (Left(systemInvoiceNo, 2) = "DC") Then
                supplyType = "NSY"
            Else
                supplyType = "TAX"
            End If

            itemDetails.Add(New ItemDetailsWithoutTransporterIdEY With {.itemNo = dr.Item("MAT_SLNO"), .supplyType = supplyType, .itemDesc = dr.Item("P_DESC"), .isService = isService,
                                    .hsnsacCode = "68159100", .itemQty = dr.Item("TOTAL_QTY"), .itemUqc = dr.Item("ACC_UNIT"),
                                    .unitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .itemAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                    .taxableVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .igstRt = Math.Round(dr.Item("IGST_RATE"), 2), .igstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                    .cgstRt = Math.Round(dr.Item("CGST_RATE"), 2), .cgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .sgstRt = Math.Round(dr.Item("SGST_RATE"), 2), .sgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .otherValues = Math.Round(dr.Item("TCS_AMT"), 2),
                                    .totalItemAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)})

            objFinishedGoodsWithoutTransID.req.lineItems = itemDetails

            objFinishedGoodsWithoutTransID.req.invAssessableAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
            objFinishedGoodsWithoutTransID.req.invCgstAmt = Math.Round(dr.Item("CGST_AMT"), 2)
            objFinishedGoodsWithoutTransID.req.invSgstAmt = Math.Round(dr.Item("SGST_AMT"), 2)
            objFinishedGoodsWithoutTransID.req.invIgstAmt = Math.Round(dr.Item("IGST_AMT"), 2)

            objFinishedGoodsWithoutTransID.req.roundOff = 0.00
            objFinishedGoodsWithoutTransID.req.docAmt = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        ''Return the output
        Dim output As String = Json.JsonConvert.SerializeObject(objFinishedGoodsWithoutTransID)
        Return output
    End Function

    Public Function GenerateJsonDataEwaybillFromIRN(systemInvoiceNo As String, IRN As String, buyerPartyCode As String, consigneePartyCode As String)
        Dim working_date As Date = Today.Date
        Dim STR1 As String = ""
        If working_date.Month > 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf working_date.Month <= 3 Then
            STR1 = working_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        Dim transporterWoNo As New String("")
        Dim mc1 As New SqlCommand
        Dim basic As New EwaybillFromIRNModelClassEY()
        basic.req.irn = IRN

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            basic.req.transName = dr.Item("TRANS_NAME")
            basic.req.transMode = "ROAD"
            basic.req.vehNo = dr.Item("TRUCK_NO")
            basic.req.vehType = "R"
            transporterWoNo = dr.Item("TRANS_WO")
        Else
            dr.Close()
        End If
        conn.Close()

        ''Getting transporter GST No.
        conn.Open()
        mc1.CommandText = "select * from supl where supl_id in(select PARTY_CODE from ORDER_DETAILS where SO_NO='" & transporterWoNo & "')"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            basic.req.transId = dr.Item("SUPL_GST_NO")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            basic.req.gstin = dr.Item("c_gst_no")
            basic.req.suppPincd = Convert.ToInt32(dr.Item("c_pin"))
            basic.req.dispDtls.nm = dr.Item("c_name")
            basic.req.dispDtls.addr1 = dr.Item("c_add")
            basic.req.dispDtls.loc = dr.Item("c_city")
            basic.req.dispDtls.pin = Convert.ToInt32(dr.Item("c_pin"))
            basic.req.dispDtls.stcd = dr.Item("c_state_code")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            basic.req.custPincd = dr.Item("d_pin")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from dater where d_code='" & consigneePartyCode & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            basic.req.shipToPincd = dr.Item("d_pin")
            basic.req.expShipDtls.addr1 = dr.Item("add_1")
            basic.req.expShipDtls.loc = dr.Item("d_city")
            basic.req.expShipDtls.pin = dr.Item("d_pin")
            basic.req.expShipDtls.stcd = dr.Item("d_state_code")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If (basic.req.suppPincd = basic.req.shipToPincd) Then
            basic.req.distance = "100"
        Else
            basic.req.distance = "0"
        End If

        ''Return the output
        Dim output As String = Json.JsonConvert.SerializeObject(basic)
        Return output

    End Function


End Class

Public Class AuthenticationErrorDetailsClassEY

    Public Property status As String
    Public Property Idtoken As String
    Public Property Access_token As String
    Public Property Refresh_token As String
    Public Property token_type As String
    Public Property Expires_in As String


    Public Property errorCode As String
    Public Property errorMessage As String

End Class

Public Class EinvoiceErrorDetailsClassEY

    Public Property status As String
    Public Property IRN As String
    Public Property QRCode As String
    Public Property errorCode As String
    Public Property errorMessage As String
    Public Property EwbNo As String
    Public Property EwbDt As String
    Public Property EwbValidTill As String

    Public Property infoErrorCode As String
    Public Property infoErrorMessage As String

    Public Property errorfield As String
    Public Property errordesc As String

End Class

Public Class EinvoiceCancellationErrorDetailsClassEY

    Public Property status As String
    Public Property IRN As String
    Public Property CancelDate As String
    Public Property errorCode As String
    Public Property errorMessage As String

End Class

Public Class EwaybillCancellationErrorDetailsClassEY

    Public Property status As String
    Public Property ewayBillNo As String
    Public Property CancelDate As String
    Public Property errorCode As String
    Public Property errorMessage As String

    Public Property errorfield As String
    Public Property errordesc As String


End Class

Public Class EinvoiceFromIRNErrorDetailsClassEY

    Public Property status As String
    Public Property IRN As String
    Public Property QRCode As String
    Public Property errorCode As String
    Public Property errorMessage As String
    Public Property EwbNo As String
    Public Property EwbDt As String
    Public Property EwbValidTill As String

    Public Property infoErrorCode As String
    Public Property infoErrorMessage As String

    Public Property errorfield As String
    Public Property errordesc As String

End Class


