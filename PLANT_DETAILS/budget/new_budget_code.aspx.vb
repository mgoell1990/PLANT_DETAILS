Imports System.Globalization
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Web.UI
Public Class new_budget_code
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("userName") = "" Then
            '    Response.Redirect("~/Account/Login")
            '    Return
            'End If
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf TextBox1.Text.Length > 30 Then
            TextBox1.Focus()
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf TextBox3.Text = "" Then
            TextBox3.Focus()
            Return
        End If
        'check table
        conn.Open()
        count = 0
        ds.Tables.Clear()
        da = New SqlDataAdapter("select  ITEM_CODE from b_details WHERE b_code LIKE '" & TextBox1.Text & "'", conn)
        count = da.Fill(ds, "b_code")
        conn.Close()
        If count = 0 Then
            'save
            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into F_ITEM(b_code,b_name,d_desc) values (@b_code,@b_name,@d_desc)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@b_code", TextBox1.Text)
            cmd.Parameters.AddWithValue("@b_name", TextBox2.Text)
            cmd.Parameters.AddWithValue("@d_desc", TextBox3.Text)
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        Else
            'update
        End If











    End Sub
End Class