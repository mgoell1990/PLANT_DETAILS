Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Web.UI
Public Class report61
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Clear()
            Dim filePath As String = Server.MapPath("~/Reports/report.pdf")
            Dim client As WebClient = New WebClient()
            Dim buffer As Byte() = client.DownloadData(filePath)
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-length", buffer.Length.ToString())
            Response.BinaryWrite(buffer)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class