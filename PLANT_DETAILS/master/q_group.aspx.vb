Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class q_group
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim myTrans As SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
        End If

        If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Label6.Text = "Please enter Quality Code"
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            Label6.Text = "Please enter Account Quality Name"
            Return
        ElseIf TextBox3.Text = "" Then
            TextBox3.Focus()
            Label6.Text = "Please enter Account Quality Desc."
            Return
        End If

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  qual_code from qual_group WHERE qual_code LIKE '" & TextBox1.Text & "'", conn)
                count = da.Fill(ds, "qual_group")
                conn.Close()
                If count > 0 Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Label6.Text = "This Quality Code Already Exists"
                    Return
                End If
                'conn.Open()
                Dim cmd As New SqlCommand
                Dim Query As String = "Insert Into qual_group(qual_code,qual_desc ,qual_name)values(@qual_code,@qual_desc ,@qual_name)"
                cmd = New SqlCommand(Query, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@qual_code", TextBox1.Text)
                cmd.Parameters.AddWithValue("@qual_name", TextBox2.Text)
                cmd.Parameters.AddWithValue("@qual_desc", TextBox3.Text)
                cmd.ExecuteReader()
                cmd.Dispose()


                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""

                myTrans.Commit()
                conn_trans.Close()

                Label6.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label6.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using



    End Sub

End Class