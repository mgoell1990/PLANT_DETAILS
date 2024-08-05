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

Public Class EinvoiceLogicClass


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
    Dim working_date As Date = Today.Date
    Dim goAheadFlag As Boolean = True

    Dim EinvErrorData As List(Of EinvoiceErrorDetailsClass) = New List(Of EinvoiceErrorDetailsClass)()

    Public Function EinvoiceAuthentication(systemInvoiceNo As String, buyerPartyCode As String)
        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClass) = New List(Of AuthenticationErrorDetailsClass)()
        Dim AuthErrorDataObj As New AuthenticationErrorDetailsClass
        Try
            'Dim user_name As New String("BSP_einv")
            'Dim password As New String("bsp@1234")
            'Dim client_id As New String("AAACS22TXP0QQNS")
            'Dim client_secret As New String("qQ1CwXiIU5pbBM89tTfF")
            'Dim strposturltest = String.Format("https://einv-apisandbox.nic.in/eivital/v1.03/auth")
            'Dim public_key As New String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArxd93uLDs8HTPqcSPpxZrf0Dc29r3iPp0a8filjAyeX4RAH6lWm9qFt26CcE8ESYtmo1sVtswvs7VH4Bjg/FDlRpd+MnAlXuxChij8/vjyAwE71ucMrmZhxM8rOSfPML8fniZ8trr3I4R2o4xWh6no/xTUtZ02/yUEXbphw3DEuefzHEQnEF+quGji9pvGnPO6Krmnri9H4WPY0ysPQQQd82bUZCk9XdhSZcW/am8wBulYokITRMVHlbRXqu1pOFmQMO5oSpyZU3pXbsx+OxIOc4EDX0WMa9aH4+snt18WAXVGwF2B4fmBk7AtmkFzrTmbpmyVqA3KO2IjzMZPw0hQIDAQAB")
            'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)

            Dim user_name As New String("API_BSP_DIRECT")
            Dim password As New String("bsp@SAIL1")

            Dim client_id As New String("AAACS19TXPMEIQI")
            Dim client_secret As New String("52SAsqWCTlunMvR91Eot")
            Dim strposturlProduction = String.Format("https://api.einvoice1.gst.gov.in/eivital/v1.03/auth")
            Dim public_key As New String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjo1FvyiKcQ9hDR2+vH0+O2XazuLbo2bPfRiiUnpaPhE3ly+Pwh05gvEuzo2UhUIDg98cX4E0vbfWOF1po2wWTBxb8jMY1nAJ8fz1xyHc1Wa7KZ0CeTvAGeifkMux7c22pMu6pBGJN8f3q7MnIW/uSJloJF6+x4DZcgvnDUlgZD3Pcoi3GJF1THbWQi5pDQ8U9hZsSJfpsuGKnz41QRsKs7Dz7qmcKT2WwN3ULWikgCzywfuuREWb4TVE2p3e9WuoDNPUziLZFeUfMP0NqYsiGVYHs1tVI25G42AwIVJoIxOWys8Zym9AMaIBV6EMVOtQUBbNIZufix/TwqTlxNPQVwIDAQAB")
            Dim requestObjPost As WebRequest = WebRequest.Create(strposturlProduction)


            Dim gst_no As New String("22AAACS7062F1ZO")

            Dim appKey As Byte() = generateAppKey()
            Dim appKeyString = appKey.ToString()
            'Dim proxyObject As New WebProxy("210.212.151.3")
            'GlobalProxySelection.Select = proxyObject






            requestObjPost.Method = "POST"
            requestObjPost.ContentType = "application/json"
            requestObjPost.Headers.Add("client_id", client_id)
            requestObjPost.Headers.Add("client_secret", client_secret)

            Dim flag As New String("false")


            ''Test URL
            'Dim authenticationRequestData As String = "{""data"": {""UserName"": ""BSP_einv"",""Password"": """ + EncryptPasswordUsingAsymmetricEncryption(password, public_key) + """,""AppKey"": """ + EncryptAppkeyUsingAsymmetricEncryption(appKey, public_key) + """,""ForceRefreshAccessToken"": false}}"
            Dim authenticationRequestData As String = "{""data"": {""UserName"": """ + user_name + """,""Password"": """ + EncryptPasswordUsingAsymmetricEncryption(password, public_key) + """,""AppKey"": """ + EncryptAppkeyUsingAsymmetricEncryption(appKey, public_key) + """,""ForceRefreshAccessToken"": false}}"



            Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
                streamWriter.Write(authenticationRequestData)
                streamWriter.Flush()
                streamWriter.Close()

                Try
                    Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                    Dim txtResponse As New String("")
                    Dim authenticationResponseData
                    Using streamreader As New StreamReader(httpResponse.GetResponseStream())


                        txtResponse = streamreader.ReadToEnd()
                        'authenticationResponseData = Json.Linq.JObject.Parse(streamreader.ReadToEnd())
                        authenticationResponseData = Json.Linq.JObject.Parse(txtResponse)


                    End Using

                    If (authenticationResponseData("Status") = 0) Then


                        AuthErrorData.Add(New AuthenticationErrorDetailsClass With {.status = "2", .client_id = "", .client_secret = "",
                            .gst_no = "", .user_name = "", .AuthToken = "", .Sek = "", .appKey = Encoding.UTF8.GetBytes(""), .systemInvoiceNo = "", .buyerPartyCode = "", .errorCode = authenticationResponseData("ErrorDetails")(0)("ErrorCode"), .errorMessage = authenticationResponseData("ErrorDetails")(0)("ErrorMessage")})

                        Return AuthErrorData

                    Else
                        AuthErrorData.Add(New AuthenticationErrorDetailsClass With {.status = "1", .client_id = client_id, .client_secret = client_secret,
                            .gst_no = gst_no, .user_name = user_name, .AuthToken = authenticationResponseData("Data")("AuthToken"),
                            .Sek = authenticationResponseData("Data")("Sek"), .appKey = appKey,
                            .systemInvoiceNo = systemInvoiceNo, .buyerPartyCode = buyerPartyCode, .errorCode = "", .errorMessage = ""})


                        Return AuthErrorData
                        'GenerateEInvoice(client_id, client_secret, gst_no, user_name, authenticationResponseData("Data")("AuthToken"), authenticationResponseData("Data")("Sek"), appKey, systemInvoiceNo, buyerPartyCode)
                    End If

                Catch ex As Exception
                    AuthErrorData.Add(New AuthenticationErrorDetailsClass With {.status = "3", .client_id = "", .client_secret = "",
                            .gst_no = "", .user_name = "", .AuthToken = "", .Sek = "", .appKey = Encoding.UTF8.GetBytes(""), .systemInvoiceNo = "", .buyerPartyCode = "", .errorCode = "", .errorMessage = "There is some technical error in Authentication"})

                    Return AuthErrorData
                End Try

            End Using


        Catch ee As Exception

            AuthErrorData.Add(New AuthenticationErrorDetailsClass With {.status = "3", .client_id = "", .client_secret = "",
                            .gst_no = "", .user_name = "", .AuthToken = "", .Sek = "", .appKey = Encoding.UTF8.GetBytes(""), .systemInvoiceNo = "", .buyerPartyCode = "", .errorCode = "", .errorMessage = "There is some technical error in Authentication"})

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


    Public Function GenerateEInvoice(client_id As String, client_secret As String, Gstin As String, user_name As String, AuthToken As String, Sek As String, appKey() As Byte, systemInvoiceNo As String, buyerPartyCode As String, isServiceFlag As String)

        Dim strposturltest = String.Format("https://einv-apisandbox.nic.in/eicore/v1.03/Invoice")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("client_id", client_id)
        requestObjPost.Headers.Add("client_secret", client_secret)
        requestObjPost.Headers.Add("Gstin", Gstin)
        requestObjPost.Headers.Add("user_name", user_name)
        requestObjPost.Headers.Add("AuthToken", AuthToken)

        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim decryptedSek = DecryptSymmetricKey(Sek, appKey)

        Dim jsonDataForEinvoice As String = GenerateJsonData(systemInvoiceNo, buyerPartyCode, isServiceFlag)
        Dim eInvoiceRequestData As String = "{""Data"": """ + EncryptDataUsingSymmetricKey(jsonDataForEinvoice, decryptedSek) + """}"
        'Dim eInvoiceRequestData As String = "{""Data"": """ + EncryptDataUsingSymmetricKey(TextBox178.Text, decryptedSek) + """}"


        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(eInvoiceRequestData)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData, eInvoiceDecryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (eInvoiceEncryptedResponseData("Status") = 0) Then

                    EinvErrorData.Add(New EinvoiceErrorDetailsClass With {.status = "2", .IRN = "", .QRCode = "", .errorCode = eInvoiceEncryptedResponseData("ErrorDetails")(0)("ErrorCode"), .errorMessage = eInvoiceEncryptedResponseData("ErrorDetails")(0)("ErrorMessage")})
                    Return EinvErrorData

                Else

                    eInvoiceDecryptedResponseData = JObject.Parse(DecryptResponseUsingSymmetricKey(eInvoiceEncryptedResponseData("Data"), decryptedSek))
                    'TextBox6.Text = eInvoiceDecryptedResponseData("Irn")
                    'Dim decodedQRCode As String = DecodeSignedQRCode(eInvoiceDecryptedResponseData("SignedQRCode"))


                    EinvErrorData.Add(New EinvoiceErrorDetailsClass With {.status = "1", .IRN = eInvoiceDecryptedResponseData("Irn"), .QRCode = eInvoiceDecryptedResponseData("SignedQRCode"), .errorCode = "", .errorMessage = ""})
                    Return EinvErrorData

                End If


            Catch ex As Exception
                EinvErrorData.Add(New EinvoiceErrorDetailsClass With {.status = "2", .IRN = "", .QRCode = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
                Return EinvErrorData
            End Try

        End Using

    End Function

    Public Function CancelEInvoice(client_id As String, client_secret As String, Gstin As String, user_name As String, AuthToken As String, Sek As String, appKey() As Byte, IRN_No As String)
        Dim strposturltest = String.Format("https://einv-apisandbox.nic.in/eicore/v1.03/Invoice/Cancel")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"
        requestObjPost.Headers.Add("client_id", client_id)
        requestObjPost.Headers.Add("client_secret", client_secret)
        requestObjPost.Headers.Add("Gstin", Gstin)
        requestObjPost.Headers.Add("user_name", user_name)
        requestObjPost.Headers.Add("AuthToken", AuthToken)

        'ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim decryptedSek = DecryptSymmetricKey(Sek, appKey)

        Dim jsonDataForEinvoiceCancel As String = "{""Irn"":""" + IRN_No + """,""CnlRsn"":""2"",""CnlRem"" : ""Data entry mistake""}"
        Dim eInvoiceRequestData As String = "{""Data"": """ + EncryptDataUsingSymmetricKey(jsonDataForEinvoiceCancel, decryptedSek) + """}"



        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(eInvoiceRequestData)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim eInvoiceEncryptedResponseData, eInvoiceDecryptedResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())

                    txtResponse = streamreader.ReadToEnd()
                    eInvoiceEncryptedResponseData = JObject.Parse(txtResponse)

                End Using

                If (eInvoiceEncryptedResponseData("Status") = 1) Then

                    eInvoiceDecryptedResponseData = JObject.Parse(DecryptResponseUsingSymmetricKey(eInvoiceEncryptedResponseData("Data"), decryptedSek))
                    EinvErrorData.Add(New EinvoiceErrorDetailsClass With {.status = "1", .IRN = eInvoiceDecryptedResponseData("Irn"), .QRCode = eInvoiceDecryptedResponseData("CancelDate"), .errorCode = "", .errorMessage = ""})
                    Return EinvErrorData

                Else

                    EinvErrorData.Add(New EinvoiceErrorDetailsClass With {.status = "2", .IRN = "", .QRCode = "", .errorCode = eInvoiceEncryptedResponseData("ErrorDetails")(0)("ErrorCode"), .errorMessage = eInvoiceEncryptedResponseData("ErrorDetails")(0)("ErrorMessage")})
                    Return EinvErrorData

                End If


            Catch ex As Exception
                EinvErrorData.Add(New EinvoiceErrorDetailsClass With {.status = "2", .IRN = "", .QRCode = "", .errorCode = "", .errorMessage = "There is some technical error in E-Invoice generation"})
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

    Public Function GenerateJsonData(systemInvoiceNo As String, buyerPartyCode As String, isServiceFlag As String)
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

        Dim basic As New EinvoiceModelClass()
        basic.Version = "1.03"
        basic.TranDtls.TaxSch = "GST"
        basic.TranDtls.SupTyp = "B2B"
        basic.TranDtls.RegRev = "N"
        basic.TranDtls.IgstOnIntra = "N"

        basic.DocDtls.Typ = "INV"
        basic.DocDtls.No = systemInvoiceNo
        basic.DocDtls.Dt = working_date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)


        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            basic.SellerDtls.Gstin = dr.Item("c_gst_no")
            basic.SellerDtls.LglNm = dr.Item("c_name")
            basic.SellerDtls.Addr1 = dr.Item("c_add")
            basic.SellerDtls.Addr2 = dr.Item("c_add1")
            basic.SellerDtls.Loc = dr.Item("c_city")
            basic.SellerDtls.Pin = Convert.ToInt32(dr.Item("c_pin"))
            basic.SellerDtls.Stcd = dr.Item("c_state_code")
            basic.SellerDtls.Ph = dr.Item("c_contact_no")
            basic.SellerDtls.Em = dr.Item("c_email")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If (isServiceFlag = "YES") Then
            'When invoice type is Service(LD/PENALTY)
            conn.Open()
            mc1.CommandText = "select * from SUPL where SUPL_ID='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                basic.BuyerDtls.Gstin = dr.Item("SUPL_GST_NO")
                basic.BuyerDtls.LglNm = dr.Item("SUPL_NAME")
                basic.BuyerDtls.Pos = dr.Item("SUPL_STATE_CODE")
                basic.BuyerDtls.Addr1 = dr.Item("SUPL_AT")
                basic.BuyerDtls.Addr2 = dr.Item("SUPL_PO")
                basic.BuyerDtls.Loc = dr.Item("SUPL_DIST")
                basic.BuyerDtls.Pin = dr.Item("SUPL_PIN")
                basic.BuyerDtls.Stcd = dr.Item("SUPL_STATE_CODE")
                basic.BuyerDtls.Ph = dr.Item("SUPL_MOB1")
                basic.BuyerDtls.Em = dr.Item("SUPL_EMAIL")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            Dim itemDetails As List(Of ItemDetails) = New List(Of ItemDetails)()

            conn.Open()
            mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                Dim isService As String
                If (dr.Item("ACC_UNIT") = "Service") Then
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
                itemDetails.Add(New ItemDetails With {.SlNo = "1", .PrdDesc = dr.Item("P_DESC"), .IsServc = isService,
                                .HsnCd = dr.Item("CHPTR_HEAD"), .UnitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .TotAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                .Discount = Math.Round(0, 2), .AssAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .GstRt = gstRate, .IgstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                .CgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .SgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .OthChrg = Math.Round(dr.Item("TCS_AMT"), 2), .TotItemVal = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2),
                                .OrdLineRef = dr.Item("MAT_SLNO")})

                basic.ItemList = itemDetails

                basic.ValDtls.AssVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
                basic.ValDtls.CgstVal = Math.Round(dr.Item("CGST_AMT"), 2)
                basic.ValDtls.SgstVal = Math.Round(dr.Item("SGST_AMT"), 2)
                basic.ValDtls.IgstVal = Math.Round(dr.Item("IGST_AMT"), 2)
                basic.ValDtls.Discount = 0.00
                basic.ValDtls.OthChrg = 0.00
                basic.ValDtls.RndOffAmt = 0.00
                basic.ValDtls.TotInvVal = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

        Else
            'When invoice type is Goods
            conn.Open()
            mc1.CommandText = "select * from dater where d_code='" & buyerPartyCode & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                basic.BuyerDtls.Gstin = dr.Item("gst_code")
                basic.BuyerDtls.LglNm = dr.Item("d_name")
                basic.BuyerDtls.Pos = dr.Item("d_state_code")
                basic.BuyerDtls.Addr1 = dr.Item("add_1")
                basic.BuyerDtls.Addr2 = dr.Item("add_2")
                basic.BuyerDtls.Loc = dr.Item("d_city")
                basic.BuyerDtls.Pin = dr.Item("d_pin")
                basic.BuyerDtls.Stcd = dr.Item("d_state_code")
                basic.BuyerDtls.Ph = dr.Item("d_contact")
                basic.BuyerDtls.Em = dr.Item("d_email")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            Dim itemDetails As List(Of ItemDetails) = New List(Of ItemDetails)()

            conn.Open()
            mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & systemInvoiceNo & "' and FISCAL_YEAR='" & STR1 & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                Dim isService As String
                If (dr.Item("ACC_UNIT") = "Service") Then
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
                itemDetails.Add(New ItemDetails With {.SlNo = dr.Item("MAT_SLNO"), .PrdDesc = dr.Item("P_DESC"), .IsServc = isService,
                                .HsnCd = dr.Item("CHPTR_HEAD"), .Qty = dr.Item("TOTAL_QTY"), .Unit = dr.Item("ACC_UNIT"),
                                .UnitPrice = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) / CDec(dr.Item("TOTAL_QTY")), 3), .TotAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2),
                                .Discount = Math.Round(0, 2), .AssAmt = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2), .GstRt = gstRate, .IgstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                                .CgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .SgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .OthChrg = Math.Round(dr.Item("TCS_AMT"), 2), .TotItemVal = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2),
                                .OrdLineRef = dr.Item("MAT_SLNO")})

                basic.ItemList = itemDetails

                basic.ValDtls.AssVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))), 2)
                basic.ValDtls.CgstVal = Math.Round(dr.Item("CGST_AMT"), 2)
                basic.ValDtls.SgstVal = Math.Round(dr.Item("SGST_AMT"), 2)
                basic.ValDtls.IgstVal = Math.Round(dr.Item("IGST_AMT"), 2)
                basic.ValDtls.Discount = 0.00
                basic.ValDtls.OthChrg = 0.00
                basic.ValDtls.RndOffAmt = 0.00
                basic.ValDtls.TotInvVal = Math.Round(((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("TERM_AMT"))) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT")) + CDec(dr.Item("TCS_AMT"))), 2)
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

        End If




        Dim output As String = Json.JsonConvert.SerializeObject(basic)
        Return output

    End Function

End Class

Public Class AuthenticationErrorDetailsClass

    Public Property status As String
    Public Property client_id As String
    Public Property client_secret As String
    Public Property gst_no As String

    Public Property user_name As String

    Public Property AuthToken As String
    Public Property Sek As String

    Public Property appKey As Byte()

    Public Property systemInvoiceNo As String
    Public Property buyerPartyCode As String
    Public Property errorCode As String
    Public Property errorMessage As String

End Class

Public Class EinvoiceErrorDetailsClass

    Public Property status As String
    Public Property IRN As String
    Public Property QRCode As String
    Public Property errorCode As String
    Public Property errorMessage As String

End Class





