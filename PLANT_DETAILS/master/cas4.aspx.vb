Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class cas4
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

            ''SEARCH F MATERIAL PO AS PER BAL
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("SELECT (qual_code + ' , ' + qual_name) AS Q_CODE  FROM qual_group ORDER BY qual_code", conn)
            da.Fill(dt)
            DropDownList1.Items.Clear()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "Q_CODE"
            DropDownList1.DataBind()
            DropDownList1.Items.Add("Select")
            DropDownList1.SelectedValue = "Select"
            conn.Close()
            Dim dt1 As New DataTable
            dt1.Columns.AddRange(New DataColumn(2) {New DataColumn("MAT_GROUP"), New DataColumn("COST_VALUE"), New DataColumn("EFECTIVE_DATE")})
            GridView1.DataSource = dt1
            GridView1.DataBind()

        End If

        If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select * from CAS_4 WHERE MAT_GROUP ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "'", conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
        conn.Close()
        Label6.Text = ""
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or IsNumeric(TextBox1.Text) = False Or CDec(TextBox1.Text) = 0 Then
            TextBox1.Focus()
        ElseIf TextBox2.Text = "" Or IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        ElseIf DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Return
        End If

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry

                Dim cmd As New SqlCommand
                Dim Query As String = "Insert Into CAS_4 (MAT_GROUP ,COST_VALUE ,EFECTIVE_DATE,ENTRY_DATE,EMP_NAME) VALUES (@MAT_GROUP ,@COST_VALUE ,@EFECTIVE_DATE,@ENTRY_DATE,@EMP_NAME)"
                cmd = New SqlCommand(Query, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@MAT_GROUP", DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1))
                cmd.Parameters.AddWithValue("@COST_VALUE", TextBox1.Text)
                cmd.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                cmd.Parameters.AddWithValue("@EMP_NAME", Session("userName"))
                cmd.ExecuteReader()
                cmd.Dispose()

                TextBox1.Text = ""
                TextBox2.Text = ""
                Label6.Text = "DATA SAVED"
                If DropDownList1.SelectedValue = "Select" Then
                    Return
                End If
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select * from CAS_4 WHERE MAT_GROUP ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "'", conn)
                da.Fill(dt)
                'conn.Close()
                GridView1.DataSource = dt
                GridView1.DataBind()
                conn.Close()
                myTrans.Commit()
                conn_trans.Close()
                Label6.Text = "All records are written to database."
                result = True
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

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DropDownList1.SelectedValue = "Select"
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
End Class