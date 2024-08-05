Imports System.IO
Imports System.Net
Imports System.Net.Mail

Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Security
Imports Org.BouncyCastle.Crypto.Parameters

Imports Newtonsoft
Imports Newtonsoft.Json.Linq


Imports System.IdentityModel.Tokens.Jwt
Imports ZXing
Imports System.Drawing.Imaging
Imports System.Drawing
Imports CrystalDecisions.CrystalReports.Engine

Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports iTextSharp.text
Imports System.Globalization

Public Class Email
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)


    Dim dr As SqlDataReader
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim goAheadFlag As Boolean = True
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        ''''''''''''''''''''''''''''''''''
        'downloadPayslip("711352", "July")
        'ExtractPdfPage("D:/Payslips/" + "July" + ".pdf", 13, "D:/Payslips/July1.pdf")
        'SendPDFEmail(dt)
        'downloadPayslip("711352", "July")
        'CreatePdfandSendMail()
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select PERNO from emp_master where status='working' order by PERNO"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            'dr.Read()
            While dr.Read()
                If Not IsDBNull(dr("PERNO")) Then
                    'cboLabNumber.Items.Add(reader("LABNO"))
                    downloadPayslip(dr("PERNO"), DropDownList9.SelectedValue)
                End If
            End While

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    'Private Sub CreatePdfandSendMail()
    '    Dim reader As PdfReader = Nothing
    '    'Dim page As PdfImportedPage = Nothing
    '    reader = New PdfReader("D:/Payslips/July.pdf")
    '    reader.SelectPages(13)
    '    'Dim doc As New Document()
    '    Dim mStream As MemoryStream = New MemoryStream
    '    'mStream.BeginWrite()
    '    Dim stamper As New PdfStamper(reader, mStream)
    '    Dim bytes As Byte() = mStream.ToArray()
    '    'doc.Open()
    '    'doc.Add(New Paragraph("Paragraph 1"))
    '    'doc.Add((page))



    '    'doc.Close()
    '    'mStream.Position = 0
    '    Dim mm As New MailMessage("srubhilaiedp@gmail.com", "goellbrothers1@gmail.com")
    '    mm.Subject = "Payslip for the month of "
    '    mm.Body = "Hi\n Please find payslip for the month of July 2018.\n SRU Bhilai EDP"
    '    'mm.Attachments.Add(New Attachment(New MemoryStream(bytes), "iTextSharpPDF.pdf"))
    '    mm.Attachments.Add(New Attachment(New MemoryStream(bytes), "iTextSharpPDF.pdf"))
    '    mm.IsBodyHtml = True
    '    Dim smtp As New SmtpClient()
    '    smtp.Host = "smtp.gmail.com"
    '    smtp.EnableSsl = True
    '    Dim NetworkCred As New NetworkCredential()
    '    NetworkCred.UserName = "srubhilaiedp@gmail.com"
    '    NetworkCred.Password = "srubhilaiedp@1"
    '    smtp.UseDefaultCredentials = True
    '    smtp.Credentials = NetworkCred
    '    smtp.Port = 587
    '    smtp.Send(mm)


    'End Sub

    Private Sub downloadPayslip(pno As String, month As String)
        Dim outputStream As Stream = Nothing
        Dim reader As PdfReader = New PdfReader("D:/Payslips/" & month & "/" & month & "_payslip.pdf")
        If (PdfReader.unethicalreading) Then

        End If

        Dim stamper As PdfStamper = Nothing
        Dim parser As PdfReaderContentParser = New PdfReaderContentParser(reader)

        Dim strategy As New SimpleTextExtractionStrategy
        Dim text As New String("")
        Dim sb As New StringBuilder()
        Dim currentPage As New Integer
        For currentPage = 1 To reader.NumberOfPages()
            strategy = parser.ProcessContent(currentPage, strategy)

            If (strategy.GetResultantText().Contains(pno)) Then

                Dim doc As Document = Nothing
                Dim doc_new As Document = Nothing
                Dim pdfCpy As PdfCopy = Nothing
                Dim page As PdfImportedPage = Nothing
                Try

                    doc = New Document(reader.GetPageSizeWithRotation(1))
                    pdfCpy = New PdfCopy(doc, New System.IO.FileStream("D:/Payslips/" + month + "/" + month + "_" + pno + ".pdf", System.IO.FileMode.Create))
                    doc.Open()
                    page = pdfCpy.GetImportedPage(reader, currentPage)
                    pdfCpy.AddPage(page)

                    doc.Close()
                    reader.Close()
                Catch ex As Exception
                    Throw ex
                End Try

                Exit For

            End If
        Next

        ''''''''''''''''''''''''''

    End Sub

    Private Sub SendPDFEmail(pno As String, month As String, year As String, email_id As String)
        Using sw As New StringWriter()
            Using hw As New HtmlTextWriter(sw)
                Dim companyName As String = "ASPSnippets"
                Dim sb As New StringBuilder()
                Using memoryStream As New MemoryStream()

                    Dim bytes As Byte() = memoryStream.ToArray()
                    memoryStream.Close()
                    Dim mm As New MailMessage("srubhilaiedp@gmail.com", email_id)
                    mm.Subject = "Payslip for the month of " + month + " " + year

                    sb.AppendLine("Hi<br>")
                    sb.AppendLine("Please find your payslip for the month of " + month + " " + year + ".<br>")
                    sb.AppendLine("This is a system generated email, please do not revert back.<br>")
                    sb.AppendLine("<br><br>")

                    sb.AppendLine("Regards,<br><br>")
                    sb.AppendLine("Mayank Kumar Goyal<br>")
                    sb.AppendLine("JM(Systems)<br>")
                    sb.AppendLine("EDP<br>")
                    sb.AppendLine("SRU Bhilai<br>")

                    mm.Body = sb.ToString()

                    mm.Attachments.Add(New Attachment("D:/Payslips/" + month + "/" + month + "_" + pno + ".pdf"))
                    mm.IsBodyHtml = True
                    Dim smtp As New SmtpClient()
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential()
                    NetworkCred.UserName = "srubhilaiedp@gmail.com"
                    NetworkCred.Password = "srubhilaiedp@1"
                    smtp.UseDefaultCredentials = True
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    smtp.Send(mm)
                End Using
            End Using
        End Using
    End Sub

    Public Sub ExtractPdfPage(ByVal sourcePdf As String, ByVal pageNumberToExtract As Integer, ByVal outPdf As String)
        'Dim reader As PdfReader = Nothing
        'Dim doc As Document = Nothing
        'Dim doc_new As Document = Nothing
        'Dim pdfCpy As PdfCopy = Nothing
        'Dim page As PdfImportedPage = Nothing
        'Try
        '    reader = New PdfReader(sourcePdf)
        '    doc = New Document(reader.GetPageSizeWithRotation(1))
        '    'pdfCpy = New PdfCopy(doc, New IO.FileStream(outPdf, IO.FileMode.Create))
        '    pdfCpy = New PdfCopy(doc, New IO.FileStream(outPdf, IO.FileMode.Create))
        '    doc.Open()
        '    page = pdfCpy.GetImportedPage(reader, pageNumberToExtract)
        '    pdfCpy.AddPage(page)

        '    doc.Close()
        '    reader.Close()
        'Catch ex As Exception
        '    Throw ex
        'End Try
        'SendPDFEmail(pdfCpy)

        ''''''''''''''''''''''''''''''
        Dim reader As PdfReader = Nothing
        Dim doc As Document = Nothing
        Dim doc_new As Document = Nothing
        Dim pdfCpy As PdfCopy = Nothing
        Dim page As PdfImportedPage = Nothing
        Try
            reader = New PdfReader(sourcePdf)
            doc = New Document(reader.GetPageSizeWithRotation(1))
            'pdfCpy = New PdfCopy(doc, New IO.FileStream(outPdf, IO.FileMode.Create))
            pdfCpy = New PdfCopy(doc, New System.IO.FileStream(outPdf, System.IO.FileMode.Create))
            doc.Open()
            page = pdfCpy.GetImportedPage(reader, pageNumberToExtract)
            pdfCpy.AddPage(page)

            doc.Close()
            reader.Close()
        Catch ex As Exception
            Throw ex
        End Try

        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(2) {New DataColumn("OrderId"), New DataColumn("Product"), New DataColumn("Quantity")})
        dt.Rows.Add(101, "Sun Glasses", 5)
        dt.Rows.Add(102, "Jeans", 2)
        dt.Rows.Add(103, "Trousers", 12)
        dt.Rows.Add(104, "Shirts", 9)
        'SendPDFEmail(doc)

        ''''''''''''''''''''''''''''''
    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select PERNO, EMAIL from emp_master where status='working' ORDER BY PERNO"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            'dr.Read()
            While dr.Read()
                If Not IsDBNull(dr("EMAIL")) Then

                    SendPDFEmail(dr("PERNO"), DropDownList10.SelectedValue, DropDownList11.SelectedValue, dr("EMAIL"))
                End If
            End While

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        ''GET Method
        Dim strgeturltest = String.Format("https://jsonplaceholder.typicode.com/posts/1/comments")
        Dim requestObjGet As WebRequest
        requestObjGet = WebRequest.Create(strgeturltest)

        requestObjGet.Method = "GET"

        Dim responseObjGet As HttpWebResponse
        responseObjGet = requestObjGet.GetResponse()
        Dim strresulttest As New String("")
        Using stream As Stream = responseObjGet.GetResponseStream()
            Dim sr As New StreamReader(stream)
            strresulttest = sr.ReadToEnd()
            sr.Close()
        End Using








    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ''POST Method

        'Dim strposturltest = String.Format("https://jsonplaceholder.typicode.com/posts")
        'Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        'requestObjPost.Method = "POST"
        'requestObjPost.ContentType = "application/json"

        'Dim authenticationRequestData As String = "[{""title"":""SRU"",""body"":""suresh"",""userId"":1}]"



        'Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
        '    streamWriter.Write(authenticationRequestData)
        '    streamWriter.Flush()
        '    streamWriter.Close()

        '    Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

        '    Dim strresulttest As New String("")

        '    Using streamreader As New StreamReader(httpResponse.GetResponseStream())


        '        strresulttest = streamreader.ReadToEnd()
        '        Dim strresulttest1 As New String("")

        '    End Using
        'End Using



        '''''''''''''''''''''''================E-Invoice Portal===============''''''''''''''''''''''''
        Dim password As New String("bsp@1234")
        Dim appKey() As Byte = generateAppKey()
        Dim appKeyString = appKey.ToString()
        Dim public_key As New String("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArxd93uLDs8HTPqcSPpxZrf0Dc29r3iPp0a8filjAyeX4RAH6lWm9qFt26CcE8ESYtmo1sVtswvs7VH4Bjg/FDlRpd+MnAlXuxChij8/vjyAwE71ucMrmZhxM8rOSfPML8fniZ8trr3I4R2o4xWh6no/xTUtZ02/yUEXbphw3DEuefzHEQnEF+quGji9pvGnPO6Krmnri9H4WPY0ysPQQQd82bUZCk9XdhSZcW/am8wBulYokITRMVHlbRXqu1pOFmQMO5oSpyZU3pXbsx+OxIOc4EDX0WMa9aH4+snt18WAXVGwF2B4fmBk7AtmkFzrTmbpmyVqA3KO2IjzMZPw0hQIDAQAB")
        Dim strposturltest = String.Format("https://einv-apisandbox.nic.in/eivital/v1.03/auth")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"

        requestObjPost.Headers.Add("client_id", "AAACS22TXP0QQNS")
        requestObjPost.Headers.Add("client_secret", "qQ1CwXiIU5pbBM89tTfF")

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        Dim flag As New String("false")


        'Correct url
        'Dim postdataCorrect As String = "{""data"": {""UserName"": ""BSP_einv"",""Password"": ""F3FJJH0MgQdXYGRs2YbRO6Nj07dvx5mG/CZZiHfMjcuuQ2v7sZoFarPdc1nvTwn625QIJ523k7d1smePPSzmF3I40mB175xWPRShqgYhFAZqeEWeUg+mD40xnZJJcvPAN6M7Kn626Yho9m2dj5U54qM/17aIVLosONs+VTK7CkY24bQQ2BPsKnEV5XwmMivKCJ1e65JX5EcRmRKstdC7QIBWkH0oYCVMYc9Jqrq6PBgZr369VQgr7qM4xqdYS8alFTvOOUkneBWtQH3vErBtCvHM2mZDkVMkzPAQI0F7x1stY+GAtMOOiW0sGJ+3FfFyhAozLebRt2QFk/5kAWpZ1g=="",""AppKey"": ""E9b1AuO2Hr2LFnIU3AwrnZyLvwiW8AwOf29UWdu73O44opZ8NVTDty4U7dQ+8qcKHID5e6TfE2zF0GEY0zXBLxToLbRAa970+AIl/un4+/1pTlm1yuDFBkRu/Q97pGlJ11JNxj0T2Q18MSE69rrJ7qrtZg56hpna6EdrgIoNwQGlje0C9BHBbcB4Qhkgxd43VuWhrosxduaxD/tgqaLGYL81V5pq2SThn8uZLlPfcDwSefXGr6ikDLW7LMYV6uBdYpgBMqxNiar2Ze7vmYIE8siqzJjEjFZrNwGT7RuNnJ0Cf9yXyCHJWH0KJssHYJ7Gsmsa9tsxzA2Fwh86a0baWw=="",""ForceRefreshAccessToken"": false}}"


        ''Test URL
        Dim authenticationRequestData As String = "{""data"": {""UserName"": ""BSP_einv"",""Password"": """ + EncryptPasswordAsymmetric(password, public_key) + """,""AppKey"": """ + EncryptAppkey(appKey, public_key) + """,""ForceRefreshAccessToken"": false}}"



        Using streamWriter As New StreamWriter(requestObjPost.GetRequestStream())
            streamWriter.Write(authenticationRequestData)
            streamWriter.Flush()
            streamWriter.Close()

            Try
                Dim httpResponse As HttpWebResponse = requestObjPost.GetResponse()

                Dim txtResponse As New String("")
                Dim authenticationResponseData
                Using streamreader As New StreamReader(httpResponse.GetResponseStream())


                    'txtResponse = streamreader.ReadToEnd()
                    authenticationResponseData = Json.Linq.JObject.Parse(streamreader.ReadToEnd())


                End Using

                GenerateEInvoice("AAACS22TXP0QQNS", "qQ1CwXiIU5pbBM89tTfF", "22AAACS7062F1ZO", "BSP_einv", authenticationResponseData("Data")("AuthToken"), authenticationResponseData("Data")("Sek"), appKey)

            Catch ex As Exception
                Label53.Text = "Please try again"
            End Try

        End Using




    End Sub

    Public Sub GenerateEInvoice(client_id As String, client_secret As String, Gstin As String, user_name As String, AuthToken As String, Sek As String, appKey() As Byte)

        Dim strposturltest = String.Format("https://einv-apisandbox.nic.in/eicore/v1.03/Invoice")
        Dim requestObjPost As WebRequest = WebRequest.Create(strposturltest)
        requestObjPost.Method = "POST"
        requestObjPost.ContentType = "application/json"

        requestObjPost.Headers.Add("client_id", client_id)
        requestObjPost.Headers.Add("client_secret", client_secret)
        requestObjPost.Headers.Add("Gstin", Gstin)
        requestObjPost.Headers.Add("user_name", user_name)
        requestObjPost.Headers.Add("AuthToken", AuthToken)


        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Dim eInvJsonRequestData As New String("{
""Version"":""1.01"",
""TranDtls"":{
""TaxSch"":""GST"",
""SupTyp"":""B2B"",
""RegRev"":""Y"",
""EcmGstin"":null,
""IgstOnIntra"":""N""
},
""DocDtls"":{
""Typ"":""INV"",
""No"":""OS1510000051"",
""Dt"":""24/08/2020""
},
""SellerDtls"":{
""Gstin"":""22AAACS7062F1ZO"",
""LglNm"":""STEEL AUTHORITY OF INDIA LTD"",
""TrdNm"":""SAIL REFRACTORY UNIT"",
""Addr1"":""MARODA"",
""Addr2"":""BHILAI"",
""Loc"":""DURG"",
""Pin"":490006,
""Stcd"":""22"",
""Ph"":""9000000000"",
""Em"":""abc@gmail.com""
},
""BuyerDtls"":{
""Gstin"":""21AADCA9414C3Z9"",
""LglNm"":""DALMIACEMENT(BHARAT)LIMITED"",
""TrdNm"":""DALMIACEMENT(BHARAT)LIMITED-REFRACTORYUNIT"",
""Pos"":""21"",
""Addr1"":""RAJGANGPUR,RAJGANGPUR"",
""Addr2"":""RAJGANGPUR"",
""Loc"":""Sundargarh"",
""Pin"":770017,
""Stcd"":""21"",
""Ph"":""91111111111"",
""Em"":""xyz@yahoo.com""
},
""DispDtls"":{
""Nm"":""SAIL REFRACTORY UNIT"",
""Addr1"":""MARODA"",
""Addr2"":""BHILAI"",
""Loc"":""DURG"",
""Pin"":490006,
""Stcd"":""22""
},
""ShipDtls"":{
""Gstin"":""21AADCA9414C3Z9"",
""LglNm"":""DALMIACEMENT(BHARAT)LIMITED"",
""TrdNm"":""DALMIACEMENT(BHARAT)LIMITED-REFRACTORYUNIT"",
""Addr1"":""RAJGANGPUR,RAJGANGPUR"",
""Addr2"":""RAJGANGPUR"",
""Loc"":""Sundargarh"",
""Pin"":770017,
""Stcd"":""21""
},
""ItemList"":[
{
""SlNo"":""10"",
""PrdDesc"":""Mass"",
""IsServc"":""N"",
""HsnCd"":""1001"",
""Barcde"":""123456"",
""Qty"":28,
""FreeQty"":0,
""Unit"":""MTS"",
""UnitPrice"":37800.00,
""TotAmt"":1058400.00,
""Discount"":0,
""PreTaxVal"":0,
""AssAmt"":1058400.00,
""GstRt"":18.0,
""IgstAmt"":190512.00,
""CgstAmt"":0,
""SgstAmt"":0,
""CesRt"":0,
""CesAmt"":0,
""CesNonAdvlAmt"":0,
""StateCesRt"":0,
""StateCesAmt"":0,
""StateCesNonAdvlAmt"":0,
""OthChrg"":0,
""TotItemVal"":1248912.00,
""OrdLineRef"":""10"",
""OrgCntry"":""IN"",
""PrdSlNo"":""10"",
""BchDtls"":{
""Nm"":""Mass"",
""ExpDt"":""11/09/2020"",
""WrDt"":""11/09/2020""
},
""AttribDtls"":[
{
""Nm"":""Ld Gunning Mass"",
""Val"":""10000""
}
]
}
],
""ValDtls"":{
""AssVal"":1058400.00,
""CgstVal"":0,
""SgstVal"":0,
""IgstVal"":190512.00,
""CesVal"":0,
""StCesVal"":0,
""Discount"":0,
""OthChrg"":0,
""RndOffAmt"":0,
""TotInvVal"":1248912.00,
""TotInvValFc"":0
},
""PayDtls"":{
""Nm"":""SRU Bhilai"",
""AccDet"":""5697389713210"",
""Mode"":""Direct Transfer"",
""FinInsBr"":""SBIN11000"",
""PayTerm"":""100"",
""PayInstr"":""Gift"",
""CrTrn"":""test"",
""DirDr"":""test"",
""CrDay"":100,
""PaidAmt"":10000,
""PaymtDue"":5000
},
""RefDtls"":{
""InvRm"":""TEST"",
""DocPerdDtls"":{
""InvStDt"":""01/08/2020"",
""InvEndDt"":""01/09/2020""
},
""PrecDocDtls"":[
{
""InvNo"":""DOC/002"",
""InvDt"":""01/08/2020"",
""OthRefNo"":""123456""
}
],
""ContrDtls"":[
{
""RecAdvRefr"":""Doc/003"",
""RecAdvDt"":""01/08/2020"",
""TendRefr"":""Abc001"",
""ContrRefr"":""Co123"",
""ExtRefr"":""Yo456"",
""ProjRefr"":""Doc-456"",
""PORefr"":""Doc-789"",
""PORefDt"":""01/08/2020""
}
]
},
""AddlDocDtls"":[
{
""Url"":""https://einv-apisandbox.nic.in"",
""Docs"":""TestDoc"",
""Info"":""DocumentTest""
}
],
""ExpDtls"":{
""ShipBNo"":""A-248"",
""ShipBDt"":""01/08/2020"",
""Port"":""INABG1"",
""RefClm"":""N"",
""ForCur"":""AED"",
""CntCode"":""AE"",
""ExpDuty"":null
},
""EwbDtls"":{
""TransId"":""12AWGPV7107B1Z1"",
""TransName"":""XYZEXPORTS"",
""Distance"":100,
""TransDocNo"":""DOC01"",
""TransDocDt"":""18/08/2020"",
""VehNo"":""ka123456"",
""VehType"":""R"",
""TransMode"":""1""
}
}")


        ''Test URL
        Dim decryptedSek = DecryptSymmetricKey(Sek, appKey)



        'Dim eInvoiceRequestData As String = "{""Data"": """ + EncryptUsingSymmetricKey(eInvJsonRequestData, decryptedSek) + """}"
        Dim eInvoiceRequestData As String = "{""Data"": """ + EncryptUsingSymmetricKey(TextBox1.Text, decryptedSek) + """}"




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

                    eInvoiceEncryptedResponseData = Json.Linq.JObject.Parse(txtResponse)

                    Label53.Text = eInvoiceEncryptedResponseData("Status")
                    If (eInvoiceEncryptedResponseData("Status") = "0") Then
                        'Label52.Text = eInvoiceEncryptedResponseData("ErrorDetails")("ErrorMessage")
                        Label52.Text = "There is some error"
                    Else
                        Label52.Text = eInvoiceEncryptedResponseData("Data")

                    End If




                End Using

                'eInvoiceDecryptedResponseData = Json.Linq.JObject.Parse(DecryptUsingSymmetricKey("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"))
                eInvoiceDecryptedResponseData = Json.Linq.JObject.Parse(DecryptUsingSymmetricKey(eInvoiceEncryptedResponseData("Data"), decryptedSek))
                Label54.Text = eInvoiceDecryptedResponseData("Irn")
                'Label1.Text = eInvoiceDecryptedResponseData("SignedQRCode")
                DecodeSignedQRCode(eInvoiceDecryptedResponseData("SignedQRCode"))


            Catch ex As Exception
                Label53.Text = "Please try again"
            End Try

        End Using

    End Sub


    Public Function DecodeSignedInvoice(token As String)


        Dim parts = token.Split(".")
        Dim header = parts(0)
        Dim payload = parts(1)
        Dim Signature = parts(2)
        Dim crypto() As Byte = Convert.FromBase64String(parts(2))
        Dim headerJson = Encoding.UTF8.GetString(Convert.FromBase64String(header))
        Dim headerData = JObject.Parse(headerJson)
        Dim payloadJson = Encoding.UTF8.GetString(Convert.FromBase64String(payload))
        Dim payloadData = JObject.Parse(payloadJson)
        Return headerData.ToString() + payloadData.ToString()



    End Function

    Public Function DecodeSignedQRCode(token As String)


        Try
            Dim handler = New JwtSecurityTokenHandler()
            Dim token1 = handler.ReadJwtToken(token)

            Dim responsePayload = token1.Payload.First().Value
            GenerateQRCode(responsePayload.ToString())
            Label1.Text = responsePayload.ToString()

        Catch ex As Exception
            Label53.Text = "Please try again"

        End Try


    End Function

    Public Function GenerateQRCode(inputString As String)

        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE
        Dim result = writer.Write(inputString)
        Dim path As String = Server.MapPath("~/images/QRImage.jpg")
        Dim barcodeBitmap = New Bitmap(result)

        Using memory As New MemoryStream()
            Using fs As New FileStream(path, FileMode.Create, FileAccess.ReadWrite)
                barcodeBitmap.Save(memory, ImageFormat.Jpeg)
                Dim bytes As Byte() = memory.ToArray()
                fs.Write(bytes, 0, bytes.Length)
            End Using
        End Using
        imgQRCode.Visible = True
        imgQRCode.ImageUrl = "~/images/QRImage.jpg"

    End Function

    Public Function GetQRCodeData(inputString As String)

        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE

        writer.Options.Height = 10
        writer.Options.Width = 10
        Dim result = writer.Write(inputString)
        Dim barcodeBitmap = New Bitmap(result)

        Dim bytes As Byte()
        Using memory As New MemoryStream()
            barcodeBitmap.Save(memory, ImageFormat.Jpeg)
            bytes = memory.ToArray()
        End Using

        Return bytes

    End Function

    Public Function DecryptUsingSymmetricKey(encrytedText As String, sek As String)

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

            ''Dim EK_result As String = Convert.ToBase64String(deCipher)
            Dim jsonStr As String = Encoding.UTF8.GetString(deCipher)
            'Dim dec_string = JsonConvert.DeserializeObject() < Dictionary < String, Object>>(jsonStr)
            Return jsonStr

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


    Public Function generateAppKey() As Byte()
        Dim KEYGEN As Aes = Aes.Create()
        Dim secretKey() As Byte = KEYGEN.Key
        Return secretKey
    End Function


    Public Function EncryptPasswordAsymmetric(data As String, key As String)

        Dim keyBytes() As Byte = Convert.FromBase64String(key)
        Dim asymmetricKeyParameter As AsymmetricKeyParameter = PublicKeyFactory.CreateKey(keyBytes)
        Dim rsaKeyParameters As RsaKeyParameters = asymmetricKeyParameter
        Dim RSAParameters As New RSAParameters()
        RSAParameters.Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned()
        RSAParameters.Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
        Dim RSA As New RSACryptoServiceProvider()
        RSA.ImportParameters(RSAParameters)
        Dim plaintext() As Byte = Encoding.UTF8.GetBytes(data)
        Dim ciphertext() As Byte = RSA.Encrypt(plaintext, False)
        Dim cipherresult As New String(Convert.ToBase64String(ciphertext))
        Return cipherresult
    End Function



    Public Function EncryptAppkey(data() As Byte, key As String)

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


    Public Function EncryptUsingSymmetricKey(text As String, sek As String)

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

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click


        Dim mycommand As New SqlCommand
        Dim da As New SqlDataAdapter
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        'Dim ds As New DataSet()

        Dim inv_for As String = ""
        Dim QRCodeData As String = ""
        mycommand.CommandText = "select * from DESPATCH WHERE D_TYPE+ CONVERT(VARCHAR(15), INV_NO)='OS1510000072' AND FISCAL_YEAR=1920"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_for = dr.Item("INV_TYPE")
            QRCodeData = dr.Item("QR_CODE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim PO_QUARY As String = "select (SELECT 'EXTRA COPY' AS INV_FOR ) AS INV_FOR, * from DESPATCH where D_TYPE+ CONVERT(VARCHAR(15), INV_NO) ='OS1510000072' and FISCAL_YEAR =1920"
        da = New SqlDataAdapter(PO_QUARY, conn)

        da.Fill(dt)

        ''Add the Barcode column to the DataSet
        dt.Columns.Add(New DataColumn("QR_CODE_INPUT", GetType(Byte())))

        For Each dr As DataRow In dt.Rows

            dr("QR_CODE_INPUT") = GetQRCodeData(QRCodeData)

        Next

        conn.Close()
        crystalReport.Load(Server.MapPath("~/print_rpt/gst_invoice_ti_QRCode.rpt"))
        crystalReport.SetDataSource(dt)

        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
        Dim url As String = "REPORT.aspx"
        Dim sb As New StringBuilder()
        sb.Append("<script type = 'text/javascript'>")
        sb.Append("window.open('")
        sb.Append(url)
        sb.Append("');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
        crystalReport.Close()
        crystalReport.Dispose()


    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim basic As New Basic()
        basic.Version = "1.03"
        basic.TranDtls.TaxSch = "GST"
        basic.TranDtls.SupTyp = "B2B"
        'basic.tranDtls.TaxSch = New String() {"Small", "Medium", "Large"}
        basic.TranDtls.RegRev = "N"

        basic.TranDtls.IgstOnIntra = "N"
        basic.DocDtls.Typ = "INV"
        basic.DocDtls.No = "OS1510000093"
        basic.DocDtls.Dt = "24/08/2020"

        basic.SellerDtls.Gstin = "22AAACS7062F1ZO"
        basic.SellerDtls.LglNm = "STEEL AUTHORITY OF INDIA LTD."
        basic.SellerDtls.TrdNm = "STEEL AUTHORITY OF INDIA LTD."
        basic.SellerDtls.Addr1 = "Room No.116,1st Floor,ISPAT BHAVAN"
        basic.SellerDtls.Addr2 = "BHILAI STEEL PLANT, BHILAI, Durg"
        basic.SellerDtls.Loc = "DURG"
        basic.SellerDtls.Pin = 490001
        basic.SellerDtls.Stcd = "22"
        basic.SellerDtls.Ph = "9000000009"
        basic.SellerDtls.Em = "https://www.sail.co.in/"

        basic.BuyerDtls.Gstin = "21AADCA9414C3Z9"
        basic.BuyerDtls.LglNm = "DALMIA CEMENT(BHARAT) LIMITED"
        basic.BuyerDtls.TrdNm = "DALMIA CEMENT(BHARAT) LIMITED - REFRACTORY UNIT"
        basic.BuyerDtls.Pos = "21"
        basic.BuyerDtls.Addr1 = "RAJGANGPUR,,,RAJGANGPUR"
        basic.BuyerDtls.Addr2 = "RAJGANGPUR"
        basic.BuyerDtls.Loc = "Sundargarh"
        basic.BuyerDtls.Pin = 770017
        basic.BuyerDtls.Stcd = "21"
        basic.BuyerDtls.Ph = "9000000017"
        basic.BuyerDtls.Em = "xyz@yahoo.com"



        Dim output As String = Json.JsonConvert.SerializeObject(basic)

    End Sub

    Public Class Basic

        Public Property Version As String
        Public Property TranDtls As New TransanctionDtls()
        Public Property DocDtls As New DocumentsDtls()
        Public Property SellerDtls As New SellerDetails()
        Public Property BuyerDtls As New BuyerDetails()


    End Class

    Public Class TransanctionDtls
        Public Property TaxSch As String
        Public Property SupTyp As String
        Public Property RegRev As String
        'Public Property EcmGstin As String()
        Public Property EcmGstin As String
        Public Property IgstOnIntra As String

    End Class
    Public Class DocumentsDtls
        Public Property Typ As String
        Public Property No As String
        Public Property Dt As String

    End Class

    Public Class SellerDetails
        Public Property Gstin As String
        Public Property LglNm As String
        Public Property TrdNm As String
        Public Property Addr1 As String
        Public Property Addr2 As String
        Public Property Loc As String
        Public Property Pin As Int32
        Public Property Stcd As String
        Public Property Ph As String
        Public Property Em As String


    End Class

    Public Class BuyerDetails
        Public Property Gstin As String
        Public Property LglNm As String
        Public Property TrdNm As String
        Public Property Pos As String
        Public Property Addr1 As String
        Public Property Addr2 As String
        Public Property Loc As String
        Public Property Pin As Int32
        Public Property Stcd As String
        Public Property Ph As String
        Public Property Em As String
    End Class

    Public Class ItemDetails
        Public Property SlNo As String
        Public Property PrdDesc As String
        Public Property IsServc As String
        Public Property HsnCd As String
        Public Property Qty As Decimal
        Public Property Unit As String
        Public Property UnitPrice As Decimal
        Public Property TotAmt As Decimal
        Public Property Discount As Decimal
        Public Property AssAmt As Decimal
        Public Property GstRt As Decimal
        Public Property IgstAmt As String
        Public Property CgstAmt As Decimal
        Public Property SgstAmt As Decimal
        Public Property CesRt As Decimal
        Public Property CesAmt As Decimal
        Public Property TotItemVal As Decimal
        Public Property OrdLineRef As String
        Public Property OrgCntry As String
        Public Property PrdSlNo As String

    End Class

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click


        Dim logicClassObj = New EinvoiceLogicClass
        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClass) = logicClassObj.EinvoiceAuthentication("", "")
        If (AuthErrorData.Item(0).status = "1") Then

            Dim EinvErrorData As List(Of EinvoiceErrorDetailsClass) = logicClassObj.CancelEInvoice(AuthErrorData.Item(0).client_id, AuthErrorData.Item(0).client_secret, AuthErrorData.Item(0).gst_no, AuthErrorData.Item(0).user_name, AuthErrorData.Item(0).AuthToken, AuthErrorData.Item(0).Sek, AuthErrorData.Item(0).appKey, TextBox2.Text)
            If (EinvErrorData.Item(0).status = "1") Then
                'TextBox6.Text = EinvErrorData.Item(0).IRN
                conn.Open()
                Dim sqlQuery As String = ""
                sqlQuery = "update DESPATCH set INV_STATUS ='CANCELLED' where IRN_NO  ='" & EinvErrorData.Item(0).IRN & "'"
                Dim despatch As New SqlCommand(sqlQuery, conn)
                despatch.ExecuteReader()
                despatch.Dispose()
                conn.Close()
                goAheadFlag = True
                Label53.Visible = True
                Label53.Text = "IRN cancelled Successfully."
            ElseIf (EinvErrorData.Item(0).status = "2") Then
                Label53.Visible = True
                Label52.Visible = True
                Label53.Text = EinvErrorData.Item(0).errorCode
                Label52.Text = EinvErrorData.Item(0).errorMessage
                goAheadFlag = False

            End If

        ElseIf (AuthErrorData.Item(0).status = "2") Then

            Label53.Visible = True
            Label52.Visible = True
            Label53.Text = AuthErrorData.Item(0).errorCode
            Label52.Text = AuthErrorData.Item(0).errorMessage
            goAheadFlag = False

        Else
            goAheadFlag = False
            Label53.Text = "There is some response error in E-invoice Authentication."
        End If



    End Sub




    'Private Sub SendPDFEmail(dt As DataTable)
    '    Using sw As New StringWriter()
    '        Using hw As New HtmlTextWriter(sw)
    '            Dim companyName As String = "ASPSnippets"
    '            Dim orderNo As Integer = 2303
    '            Dim sb As New StringBuilder()
    '            sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>")
    '            sb.Append("<tr><td align='center' style='background-color: #18B5F0' colspan = '2'><b>Order Sheet</b></td></tr>")
    '            sb.Append("<tr><td colspan = '2'></td></tr>")
    '            sb.Append("<tr><td><b>Order No:</b>")
    '            sb.Append(orderNo)
    '            sb.Append("</td><td><b>Date: </b>")
    '            sb.Append(DateTime.Now)
    '            sb.Append(" </td></tr>")
    '            sb.Append("<tr><td colspan = '2'><b>Company Name :</b> ")
    '            sb.Append(companyName)
    '            sb.Append("</td></tr>")
    '            sb.Append("</table>")
    '            sb.Append("<br />")
    '            sb.Append("<table border = '1'>")
    '            sb.Append("<tr>")
    '            For Each column As DataColumn In dt.Columns
    '                sb.Append("<th style = 'background-color: #D20B0C;color:#ffffff'>")
    '                sb.Append(column.ColumnName)
    '                sb.Append("</th>")
    '            Next
    '            sb.Append("</tr>")
    '            For Each row As DataRow In dt.Rows
    '                sb.Append("<tr>")
    '                For Each column As DataColumn In dt.Columns
    '                    sb.Append("<td>")
    '                    sb.Append(row(column))
    '                    sb.Append("</td>")
    '                Next
    '                sb.Append("</tr>")
    '            Next
    '            sb.Append("</table>")
    '            Dim sr As New StringReader(sb.ToString())

    '            Dim pdfDoc As New Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
    '            Dim htmlparser As New HTMLWorker(pdfDoc)
    '            Using memoryStream As New MemoryStream()
    '                Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, memoryStream)
    '                pdfDoc.Open()
    '                htmlparser.Parse(sr)
    '                pdfDoc.Close()
    '                Dim bytes As Byte() = memoryStream.ToArray()
    '                memoryStream.Close()

    '                Dim mm As New MailMessage("srubhilaiedp@gmail.com", "goellbrothers1@gmail.com")
    '                mm.Subject = "Payslip for the month of "
    '                mm.Body = "Hi\n Please find payslip for the month of July 2018.\n SRU Bhilai EDP"
    '                mm.Attachments.Add(New Attachment(New MemoryStream(bytes), "iTextSharpPDF.pdf"))
    '                mm.IsBodyHtml = True
    '                Dim smtp As New SmtpClient()
    '                smtp.Host = "smtp.gmail.com"
    '                smtp.EnableSsl = True
    '                Dim NetworkCred As New NetworkCredential()
    '                NetworkCred.UserName = "srubhilaiedp@gmail.com"
    '                NetworkCred.Password = "srubhilaiedp@1"
    '                smtp.UseDefaultCredentials = True
    '                smtp.Credentials = NetworkCred
    '                smtp.Port = 587
    '                smtp.Send(mm)
    '            End Using
    '        End Using
    '    End Using
    'End Sub

End Class