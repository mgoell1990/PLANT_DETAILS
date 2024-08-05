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
Public Class budget
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
            If Session("userName") = "" Then
                '' Response.Redirect("~/Account/Login")
                '' Return
            End If
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf TextBox4.Text = "" Or IsDate(TextBox4.Text) = False Then
            TextBox4.Focus()
            Return
        ElseIf TextBox5.Text = "" Or IsDate(TextBox5.Text) = False Then
            TextBox5.Focus()
            Return
        ElseIf TextBox6.Text = "" Or IsNumeric(TextBox6.Text) = False Then
            TextBox6.Focus()
            Return
        End If
        Dim working_date As Date
        working_date = Today.Date
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
        'check table
        conn.Open()
        count = 0
        ds.Tables.Clear()
        da = New SqlDataAdapter("select  ITEM_CODE from b_details WHERE b_code LIKE '" & TextBox1.Text & "'", conn)
        count = da.Fill(ds, "b_code")
        conn.Close()
        If count > 0 Then
            'save
            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into F_ITEM(b_code,fiscal_year,efect_date,end_date,f_value,e_value,c_value,u_name) values (@b_code,@fiscal_year,@efect_date,@end_date,@f_value,@e_value,@c_value,@u_name)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@b_code", TextBox1.Text)
            cmd.Parameters.AddWithValue("@fiscal_year", STR1)
            cmd.Parameters.AddWithValue("@efect_date", Date.ParseExact(CDate(TextBox4.Text), "dd-MM-yyyy", provider))
            cmd.Parameters.AddWithValue("@end_date", Date.ParseExact(CDate(TextBox5.Text), "dd-MM-yyyy", provider))
            cmd.Parameters.AddWithValue("@f_value", CDec(TextBox6.Text))
            cmd.Parameters.AddWithValue("@e_value", 0)
            cmd.Parameters.AddWithValue("@c_value", 0)
            cmd.Parameters.AddWithValue("@u_name", Session("userName"))
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        Else
            'not availabel

        End If
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub
End Class