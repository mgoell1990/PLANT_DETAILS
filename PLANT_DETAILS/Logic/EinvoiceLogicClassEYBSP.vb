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
Imports Oracle.ManagedDataAccess.Client

Public Class EinvoiceLogicClassEYBSP


    'Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    'Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim oradb As String = "Data Source = (DESCRIPTION = (ADDRESS=(COMMUNITY = tcp.world)(PROTOCOL=TCP)(HOST=10.145.1.42)(PORT=1521)) (CONNECT_DATA=(SERVICE_NAME=oraprod1)(SERVICE=SHARED))); User Id=einvoice;Password=sailbsp987;"
    Dim conn As New OracleConnection(oradb)

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
    Dim working_date As Date = Today.Date
    Dim goAheadFlag As Boolean = True

    Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEYBSP) = New List(Of EinvoiceErrorDetailsClassEYBSP)()

    Public Function EinvoiceAuthentication()
        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEYBSP) = New List(Of AuthenticationErrorDetailsClassEYBSP)()
        Dim AuthErrorDataObj As New AuthenticationErrorDetailsClassEYBSP
        Try


            Dim user_name As New String("P000118")
            Dim password As New String("YJncNjgRLmtfPn22")
            Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateAccessToken.do")
            Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
            requestObjPost.Method = "POST"
            requestObjPost.ContentType = "application/json"
            requestObjPost.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(user_name + ":" + password)))


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
                        AuthErrorData.Add(New AuthenticationErrorDetailsClassEYBSP With {.status = "1", .Idtoken = authenticationResponseData("resp")("id_token"),
                            .Access_token = authenticationResponseData("resp")("access_token"), .Refresh_token = authenticationResponseData("resp")("refresh_token"),
                            .token_type = authenticationResponseData("resp")("token_type"), .Expires_in = authenticationResponseData("resp")("expires_in"), .errorCode = "", .errorMessage = ""})


                        Return AuthErrorData
                    Else
                        AuthErrorData.Add(New AuthenticationErrorDetailsClassEYBSP With {.status = "2", .Idtoken = "", .Access_token = "",
                            .Refresh_token = "", .token_type = "", .Expires_in = "", .errorCode = "", .errorMessage = authenticationResponseData("resp")})

                        Return AuthErrorData
                    End If

                Catch ex As Exception
                    AuthErrorData.Add(New AuthenticationErrorDetailsClassEYBSP With {.status = "3", .Idtoken = "", .Access_token = "",
                            .Refresh_token = "", .token_type = "", .Expires_in = "", .errorCode = "", .errorMessage = "There Is some technical error in Authentication"})

                    Return AuthErrorData
                End Try

            End Using


        Catch ee As Exception

            AuthErrorData.Add(New AuthenticationErrorDetailsClassEYBSP With {.status = "3", .Idtoken = "", .Access_token = "",
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


    Public Function GenerateEInvoice(idtoken As String, payloadId As String, payloadData As String)

        Dim strposturltest = String.Format("https://eapi.eydigigst.in/eybusinessapi-0.0.2-SNAPSHOT/api/generateEinvoice.do")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("idtoken", idtoken)
        requestObjPost.Headers.Add("payloadId", payloadId)
        Dim sha As SHA1 = SHA1.Create()
        'sha = SHA1.Create()
        requestObjPost.Headers.Add("checksum", Convert.ToBase64String(sha.ComputeHash(Encoding.Default.GetBytes(payloadData))))
        requestObjPost.Headers.Add("companyCode", "1000")


        Dim jsonDataForEinvoice As String = GenerateJsonData()
        'Dim eInvoiceRequestData As String = payloadData


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

                If (eInvoiceEncryptedResponseData("hdr")("status") = "S") Then

                    EinvErrorData.Add(New EinvoiceErrorDetailsClassEYBSP With {.status = "1", .IRN = eInvoiceEncryptedResponseData("resp")("Irn"), .QRCode = eInvoiceEncryptedResponseData("resp")("SignedQRCode"), .DocType = eInvoiceEncryptedResponseData("resp")("DocType"), .DocNum = eInvoiceEncryptedResponseData("resp")("DocNum"), .DocDate = eInvoiceEncryptedResponseData("resp")("DocDate"), .suppGstin = eInvoiceEncryptedResponseData("resp")("SellerGstin"), .AckNo = eInvoiceEncryptedResponseData("resp")("AckNo"), .AckDt = eInvoiceEncryptedResponseData("resp")("AckDt"), .errorCode = "", .errorMessage = ""})
                    Return EinvErrorData


                Else

                    EinvErrorData.Add(New EinvoiceErrorDetailsClassEYBSP With {.status = "2", .IRN = "", .QRCode = "", .errorCode = eInvoiceEncryptedResponseData("resp")("errorCode"), .errorMessage = eInvoiceEncryptedResponseData("resp")("errorMessage")})
                    Return EinvErrorData


                End If


            Catch ex As Exception
                EinvErrorData.Add(New EinvoiceErrorDetailsClassEYBSP With {.status = "2", .IRN = "", .QRCode = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvErrorData
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

    Public Function GenerateJsonData()
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

        Dim basic As New EinvoiceModelClassEY()



        conn.Open()

        Dim sql As String = "select * from EInv_Invoice"
        Dim cmd As New OracleCommand(sql, conn)
        cmd.CommandType = CommandType.Text

        Dim dr As OracleDataReader = cmd.ExecuteReader()
        If dr.HasRows Then
            dr.Read()
            basic.req.docType = dr.Item("docType")
            basic.req.docNo = dr.Item("docNo")
            basic.req.docDate = CDate(dr.Item("docDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
            basic.req.reverseCharge = dr.Item("reverseCharge")
            basic.req.suppGstin = dr.Item("suppGstin")
            basic.req.supTradeName = dr.Item("supTradeName")
            basic.req.supLegalName = dr.Item("supLegalName")
            basic.req.supBuildingNo = dr.Item("supBuildingNo")
            basic.req.supBuildingName = dr.Item("supBuildingName")
            basic.req.supLocation = dr.Item("supLocation")
            basic.req.supPincode = Convert.ToInt32(dr.Item("supPincode"))
            basic.req.supStateCode = dr.Item("supStateCode")
            'basic.req.supPhone = dr.Item("supPhone")
            'basic.req.supEmail = dr.Item("supEmail")


            If (dr.Item("isService") = "Y") Then

                basic.req.custGstin = dr.Item("custGstin")
                basic.req.custOrSupName = dr.Item("custOrSupName")
                basic.req.custOrSupAddr1 = dr.Item("custOrSupAddr1")
                basic.req.custOrSupAddr2 = dr.Item("custOrSupAddr2")
                basic.req.custOrSupAddr4 = dr.Item("custOrSupAddr4")
                basic.req.custPincode = dr.Item("custPincode")
                basic.req.billToState = dr.Item("billToState")
                basic.req.pos = dr.Item("pos")
                'basic.req.custPhone = dr.Item("custPhone")
                'basic.req.custEmail = dr.Item("custEmail")

                Dim itemDetails As List(Of ItemDetailsEY) = New List(Of ItemDetailsEY)()


                itemDetails.Add(New ItemDetailsEY With {.itemNo = dr.Item("itemNo"), .supplyType = dr.Item("supplyType"), .itemDesc = dr.Item("itemDesc"), .isService = dr.Item("isService"),
                                .hsnsacCode = dr.Item("hsnsacCode"), .unitPrice = Math.Round(dr.Item("invAssessableAmt"), 3), .itemAmt = Math.Round(dr.Item("invAssessableAmt"), 2),
                                .taxableVal = Math.Round(dr.Item("invAssessableAmt"), 2), .igstRt = Math.Round(dr.Item("igstRt"), 3), .igstAmt = Math.Round(dr.Item("igstAmt"), 2),
                                .cgstRt = Math.Round(dr.Item("cgstRt"), 3), .cgstAmt = Math.Round(dr.Item("cgstAmt"), 2), .sgstRt = Math.Round(dr.Item("sgstRt"), 2), .sgstAmt = Math.Round(dr.Item("sgstAmt"), 2), .otherValues = Math.Round(dr.Item("otherValues"), 2), .totalItemAmt = Math.Round(dr.Item("docAmt"), 2)})

                basic.req.lineItems = itemDetails

                basic.req.invAssessableAmt = Math.Round(dr.Item("invAssessableAmt"), 3)
                basic.req.invCgstAmt = Math.Round(dr.Item("invCgstAmt"), 2)
                basic.req.invSgstAmt = Math.Round(dr.Item("invSgstAmt"), 2)
                basic.req.invIgstAmt = Math.Round(dr.Item("invIgstAmt"), 2)

                basic.req.roundOff = 0.00
                basic.req.docAmt = Math.Round(dr.Item("docAmt"), 2)

            Else

                basic.req.custGstin = dr.Item("custGstin")
                basic.req.custOrSupName = dr.Item("custOrSupName")
                basic.req.custOrSupAddr1 = dr.Item("custOrSupAddr1")
                basic.req.custOrSupAddr2 = dr.Item("custOrSupAddr2")
                basic.req.custOrSupAddr4 = dr.Item("custOrSupAddr4")
                basic.req.custPincode = dr.Item("custPincode")
                basic.req.billToState = dr.Item("billToState")
                basic.req.pos = dr.Item("pos")
                'basic.req.custPhone = dr.Item("custPhone")
                'basic.req.custEmail = dr.Item("custEmail")

                Dim itemDetails As List(Of ItemDetailsEY) = New List(Of ItemDetailsEY)()

                itemDetails.Add(New ItemDetailsEY With {.itemNo = dr.Item("itemNo"), .supplyType = dr.Item("supplyType"), .itemDesc = dr.Item("itemDesc"), .isService = dr.Item("isService"),
                                .hsnsacCode = dr.Item("hsnsacCode"), .itemQty = dr.Item("itemQty"), .itemUqc = dr.Item("itemUqc"),
                                .unitPrice = Math.Round(dr.Item("invAssessableAmt"), 3), .itemAmt = Math.Round(dr.Item("invAssessableAmt"), 2),
                                .taxableVal = Math.Round(dr.Item("invAssessableAmt"), 2), .igstRt = Math.Round(dr.Item("igstRt"), 2), .igstAmt = Math.Round(dr.Item("igstAmt"), 2),
                                .cgstRt = Math.Round(dr.Item("cgstRt"), 2), .cgstAmt = Math.Round(dr.Item("cgstAmt"), 2), .sgstRt = Math.Round(dr.Item("sgstRt"), 2), .sgstAmt = Math.Round(dr.Item("sgstAmt"), 2), .otherValues = Math.Round(dr.Item("otherValues"), 2), .totalItemAmt = Math.Round(dr.Item("docAmt"), 2)})

                basic.req.lineItems = itemDetails

                basic.req.invAssessableAmt = Math.Round(dr.Item("invAssessableAmt"), 3)
                basic.req.invCgstAmt = Math.Round(dr.Item("invCgstAmt"), 2)
                basic.req.invSgstAmt = Math.Round(dr.Item("invSgstAmt"), 2)
                basic.req.invIgstAmt = Math.Round(dr.Item("invIgstAmt"), 2)

                basic.req.roundOff = 0.00
                basic.req.docAmt = Math.Round(dr.Item("docAmt"), 2)


            End If
            dr.Close()
        Else
            dr.Close()
        End If
        cmd.Dispose()
        conn.Close()



        Dim output As String = Json.JsonConvert.SerializeObject(basic)
        Return output

    End Function

End Class

Public Class AuthenticationErrorDetailsClassEYBSP

    Public Property status As String
    Public Property Idtoken As String
    Public Property Access_token As String
    Public Property Refresh_token As String
    Public Property token_type As String
    Public Property Expires_in As String


    Public Property errorCode As String
    Public Property errorMessage As String

End Class

Public Class EinvoiceErrorDetailsClassEYBSP

    Public Property status As String
    Public Property IRN As String
    Public Property QRCode As String
    Public Property errorCode As String
    Public Property errorMessage As String

    Public Property DocType As String
    Public Property DocNum As String
    Public Property DocDate As Date
    Public Property suppGstin As String

    Public Property AckNo As String
    Public Property AckDt As Date


End Class
