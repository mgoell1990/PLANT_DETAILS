Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class bin_card_rm
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("userName") = "" Then
            '    Response.Redirect("~/Account/Login")
            '    Return
            'End If
        End If
    End Sub

    Protected Sub BINDGRID()
        imp_FormView1.DataSource = DirectCast(ViewState("form_view"), DataTable)
        imp_FormView1.DataBind()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select * from material where mat_code ='" & TextBox115.Text.Substring(0, TextBox115.Text.IndexOf(",") - 1) & "'", conn)
        da.Fill(dt)
        conn.Close()

        ViewState("form_view") = dt
        BINDGRID()
    End Sub
End Class