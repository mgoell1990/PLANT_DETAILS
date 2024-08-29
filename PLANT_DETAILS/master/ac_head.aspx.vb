Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration

Public Class ac_head
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

        End If

        If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Label7.Text = "Please enter Account Head"
            Return
        ElseIf TextBox3.Text = "" Then
            TextBox3.Focus()
            Label7.Text = "Please enter Account Head Desc."
            Return
        ElseIf TextBox4.Text = "" Then
            TextBox4.Focus()
            Label7.Text = "Please enter Account Head Type"
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            Label7.Text = "Please enter Account Head Group"
            Return
        ElseIf TextBox114.Text = "" Then
            TextBox114.Focus()
            Label7.Text = "Please enter Admin Password"
            Return
        ElseIf TextBox114.Text <> "123456987" Then
            TextBox114.Text = ""
            TextBox114.Focus()
            Label7.Text = "Incorrect Admin Password"
            Return
        ElseIf TextBox114.Text = "123456987" Then
            'validatation
            conn.Open()
            count = 0
            ds.Tables.Clear()
            da = New SqlDataAdapter("select ac_code from ACDIC WHERE ac_code LIKE '" & TextBox1.Text & "'", conn)
            count = da.Fill(ds, "ACDIC")
            conn.Close()
            If count > 0 Then
                TextBox1.Text = ""
                TextBox1.Focus()
                Label7.Text = "This Account Head Already Exists"
                Return
            End If

            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try

                    'Database updation entry
                    Dim cmd As New SqlCommand
                    Dim Query As String = "Insert Into ACDIC(ac_code,ac_description,ac_type,Account_group) values (@ac_code,@ac_description,@ac_type,@Account_group)"
                    cmd = New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@ac_code", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@ac_description", TextBox3.Text)
                    cmd.Parameters.AddWithValue("@ac_type", TextBox4.Text)
                    cmd.Parameters.AddWithValue("@Account_group", TextBox2.Text)
                    cmd.ExecuteReader()
                    cmd.Dispose()

                    myTrans.Commit()
                    conn_trans.Close()

                    Label7.Text = "Data saved successfully."

                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn_trans.Close()
                    Label7.Text = "There was some Error!!!"
                Finally
                    conn_trans.Close()
                End Try

                conn_trans.Close()

            End Using



            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

            'Label7.Text = "Data Saved"
        End If

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label7.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Label7.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Response.Redirect("~/Default")
    End Sub
End Class