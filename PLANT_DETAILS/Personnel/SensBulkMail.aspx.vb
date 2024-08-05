Imports System.Globalization
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Net
Imports System.Net.Mail
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp
Imports iTextSharp.text.pdf.parser
Imports Org.BouncyCastle.Utilities.Encoders
Imports System.IO
Public Class SensBulkMail
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If DropDownList16.SelectedValue = "Select" Then
            DropDownList16.Focus()
            Return

        ElseIf TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        End If

        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select PERNO, EMAIL from emp_master where PERNO='" & TextBox1.Text & "' and status='working' ORDER BY PERNO"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            'dr.Read()
            While dr.Read()
                If Not IsDBNull(dr("EMAIL")) Then

                    SendPDFEmail(dr("PERNO"), dr("EMAIL"))
                End If
            End While

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()


    End Sub

    Private Sub SendPDFEmail(pno As String, email_id As String)
        Using sw As New StringWriter()
            Using hw As New HtmlTextWriter(sw)
                Dim companyName As String = "ASPSnippets"
                Dim sb As New StringBuilder()
                Using memoryStream As New MemoryStream()

                    Dim bytes As Byte() = memoryStream.ToArray()
                    memoryStream.Close()
                    Dim mm As New MailMessage("srubhilaiedp@gmail.com", email_id)
                    mm.Subject = "Process flow document for Attendance and Leave Management portal"

                    sb.AppendLine("Dear Sir,<br><br>")

                    sb.AppendLine(TextBox2.Text)
                    sb.AppendLine("<br><br>")
                    sb.AppendLine("Regards,<br><br>")
                    sb.AppendLine("OA<br>")
                    sb.AppendLine("SRU(Bhilai)<br>")


                    mm.Body = sb.ToString()

                    'mm.Attachments.Add(New Attachment("D:/OA_FORMAT/" + year + "/" + month + "/" + month + "_" + year + "_" + pno + ".pdf"))
                    mm.Attachments.Add(New Attachment("D:/OA_FORMAT/Format.xls"))
                    mm.IsBodyHtml = True
                    Dim smtp As New SmtpClient()
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential()
                    NetworkCred.UserName = "oasrubhilai@gmail.com"
                    NetworkCred.Password = "bam*490006"
                    'NetworkCred.UserName = "srubhilaiedp@gmail.com"
                    'NetworkCred.Password = "srubhilaiedp@1"

                    smtp.UseDefaultCredentials = True
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    smtp.Send(mm)
                End Using
            End Using
        End Using
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If DropDownList16.SelectedValue = "Select" Then
            DropDownList16.Focus()
            Return

        End If

        If (DropDownList16.SelectedValue = "Executives") Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select PERNO, EMAIL from emp_master where status='working' and GRADE like 'E%' ORDER BY PERNO"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                'dr.Read()
                While dr.Read()
                    If Not IsDBNull(dr("EMAIL")) Then

                        SendPDFEmail(dr("PERNO"), dr("EMAIL"))
                    End If
                End While

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        ElseIf (DropDownList16.SelectedValue = "Non-Executives") Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select PERNO, EMAIL from emp_master where status='working' and GRADE not like 'E%' ORDER BY PERNO"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                'dr.Read()
                While dr.Read()
                    If Not IsDBNull(dr("EMAIL")) Then

                        SendPDFEmail(dr("PERNO"), dr("EMAIL"))
                    End If
                End While

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        End If

    End Sub
End Class