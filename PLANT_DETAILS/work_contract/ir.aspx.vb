Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class ir
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT DISTINCT po_no FROM mb_book WHERE mb_clear is null and mb_no like 'mb%'", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList6.Items.Clear()
            DropDownList6.DataSource = dt
            DropDownList6.DataValueField = "po_no"
            DropDownList6.DataBind()
            DropDownList6.Items.Add("Select")
            DropDownList6.SelectedValue = "Select"
            Panel6.Visible = False
            ''Panel3.Visible = True
            MultiView1.ActiveViewIndex = 0
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("contractAccess")) Or Session("contractAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        Panel6.Visible = False
        If DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT DISTINCT mb_no FROM mb_book WHERE mb_clear is null and po_no='" & DropDownList6.SelectedValue & "'", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList8.Items.Clear()
        DropDownList8.DataSource = dt
        DropDownList8.DataValueField = "mb_no"
        DropDownList8.DataBind()
        DropDownList8.Items.Add("Select")
        DropDownList8.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList8.SelectedIndexChanged
        Panel6.Visible = False
        If DropDownList8.SelectedValue = "Select" Then
            DropDownList8.Focus()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT * FROM mb_book WHERE mb_clear is null and po_no='" & DropDownList6.SelectedValue & "' and mb_no='" & DropDownList8.SelectedValue & "'", conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
    End Sub

    Protected Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click
        If GridView3.Rows.Count = 0 Then
            Return
        End If
        Panel6.Visible = True
    End Sub

    Protected Sub Button58_Click(sender As Object, e As EventArgs) Handles Button58.Click
        ''Panel3.Visible = False
        ''MultiView1.ActiveViewIndex = -1
    End Sub

    Protected Sub Button56_Click(sender As Object, e As EventArgs) Handles Button56.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                If TextBox30.Text = "" Then
                    TextBox30.Focus()
                    Return
                End If
                Dim password As String = ""
                conn.Open()
                Dim MC As New SqlCommand
                MC.CommandText = "select emp_password from EmpLoginDetails where (emp_id = '" & Session("userName") & "' or emp_name = '" & Session("userName") & "')"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    password = dr.Item("emp_password")
                    dr.Close()
                End If
                conn.Close()
                If password = TextBox30.Text Then
                    Panel6.Visible = False
                    Label43.Text = ""

                    mycommand = New SqlCommand("update mb_book set mb_clear='I.R. CLEAR'  WHERE po_no ='" & DropDownList6.SelectedValue & "' AND mb_no ='" & DropDownList8.SelectedValue & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()

                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("SELECT * FROM mb_book WITH(NOLOCK) WHERE  po_no='' and mb_no=''", conn)
                    da.Fill(dt)
                    conn.Close()
                    GridView3.DataSource = dt
                    GridView3.DataBind()
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("SELECT DISTINCT po_no FROM mb_book WITH(NOLOCK) WHERE mb_clear is null and mb_no like 'mb%'", conn)
                    da.Fill(dt)
                    conn.Close()
                    DropDownList6.Items.Clear()
                    DropDownList6.DataSource = dt
                    DropDownList6.DataValueField = "po_no"
                    DropDownList6.DataBind()
                    DropDownList6.Items.Add("Select")
                    DropDownList6.SelectedValue = "Select"
                    DropDownList8.Items.Clear()
                    Panel6.Visible = False
                Else
                    Label43.Text = "Password mismatch. Please try again"
                    TextBox30.Text = ""
                    Return
                End If

                myTrans.Commit()
                Label2.Text = "All records are written to database."


                dt.Clear()
                GridView3.DataSource = dt
                GridView3.DataBind()
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label2.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub
End Class