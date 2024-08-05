Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class new_mat
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

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT GROUP_TYPE from BIN_GROUP", conn)
            da.Fill(dt)
            DropDownList4.DataSource = dt
            DropDownList4.DataValueField = "GROUP_TYPE"
            DropDownList4.DataBind()
            conn.Close()
            DropDownList4.Items.Insert(0, "Select")
            DropDownList4.SelectedValue = "Select"
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select ac_code from acdic order by ac_code", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList8.DataSource = dt
            DropDownList8.DataValueField = "ac_code"
            DropDownList8.DataBind()
            DropDownList7.DataSource = dt
            DropDownList7.DataValueField = "ac_code"
            DropDownList7.DataBind()
            DropDownList6.DataSource = dt
            DropDownList6.DataValueField = "ac_code"
            DropDownList6.DataBind()
            DropDownList6.Items.Insert(0, "Select")
            DropDownList7.Items.Insert(0, "Select")
            DropDownList8.Items.Insert(0, "Select")
            DropDownList4.SelectedValue = "Select"
            DropDownList5.SelectedValue = "Select"
            DropDownList6.SelectedValue = "Select"
            DropDownList7.SelectedValue = "Select"
            DropDownList8.SelectedValue = "Select"
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT CHPT_CODE FROM CHPTR_HEADING order by CHPT_CODE", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "CHPT_CODE"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            DropDownList1.Items.Add("NA")
            DropDownList1.SelectedValue = "Select"
        End If

        If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select (GROUP_NAME + ' , ' + GROUP_CODE) AS BIN_DET from BIN_GROUP WHERE GROUP_TYPE='" & DropDownList4.SelectedValue & "' order by GROUP_CODE", conn)
        da.Fill(dt)
        DropDownList5.DataSource = dt
        DropDownList5.DataValueField = "BIN_DET"
        DropDownList5.DataBind()
        conn.Close()
        DropDownList5.Items.Insert(0, "Select")
        DropDownList5.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select * from bin_group where GROUP_TYPE='" & DropDownList4.SelectedValue & "' and GROUP_NAME = '" & DropDownList5.Text.Substring(0, DropDownList5.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox7.Text = dr.Item("GROUP_CODE")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
    End Sub

    Protected Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Select Material Type "
            Return
        ElseIf DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Select Material Group "
            Return
        ElseIf TextBox8.Text = "" Then
            TextBox8.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Enter Material Code "
            Return
        ElseIf TextBox5.Text = "" Then
            TextBox5.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Enter material Drawing "
            Return
        ElseIf TextBox6.Text = "" Then
            TextBox6.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Enter Material A/U "
            Return
        ElseIf TextBox4.Text = "" Then
            TextBox4.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Enter Material Name "
            Return
        ElseIf DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Select Material Purchase Ac Head "
            Return
        ElseIf DropDownList7.SelectedValue = "Select" Then
            DropDownList7.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Select Material Issue Ac Head "
            Return
        ElseIf DropDownList8.SelectedValue = "Select" Then
            DropDownList8.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Select Material Con Ac Head "
            Return
        ElseIf TextBox8.Text.Length <> 6 Then
            TextBox8.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Enter 6 Digit Material Code "
            Return
        ElseIf TextBox1.Text = "" Then
            TextBox1.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Please enter Admin Password"
            Return
        ElseIf TextBox1.Text <> "123456987" Then
            TextBox1.Text = ""
            TextBox1.Focus()
            mat_err_label.Visible = True
            mat_err_label.Text = "Incorrect Admin Password"
            Return
        ElseIf TextBox1.Text = "123456987" Then


            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry
                    conn.Open()
                    count = 0
                    da = New SqlDataAdapter("select * from material WHERE MAT_CODE='" & TextBox7.Text & TextBox8.Text & "'", conn)
                    count = da.Fill(dt)
                    conn.Close()
                    If count = 1 Then
                        mat_err_label.Visible = True
                        mat_err_label.Text = "Material Code Already Exit "
                        TextBox8.Focus()
                        Return
                    End If
                    ''save Material ORDER_STOP_IND

                    'conn.Open()
                    Dim query1 As String = "Insert Into MATERIAL(CHPTR_HEAD,ORDER_STOP_IND,RE_ORDER_LABEL,OPEN_STOCK,MAT_CODE , MAT_NAME , MAT_AU , MAT_DRAW , MAT_AVG , MAT_STOCK , MAT_LAST_RATE , MAT_LOCATION , AC_PUR , AC_ISSUE , AC_CON) values (@CHPTR_HEAD,@ORDER_STOP_IND,@RE_ORDER_LABEL,@OPEN_STOCK,@MAT_CODE , @MAT_NAME , @MAT_AU , @MAT_DRAW , @MAT_AVG , @MAT_STOCK , @MAT_LAST_RATE , @MAT_LOCATION , @AC_PUR , @AC_ISSUE , @AC_CON)"
                    Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@MAT_CODE", TextBox7.Text & TextBox8.Text)
                    cmd1.Parameters.AddWithValue("@MAT_NAME", TextBox4.Text)
                    cmd1.Parameters.AddWithValue("@MAT_AU", TextBox6.Text.ToUpper)
                    cmd1.Parameters.AddWithValue("@MAT_DRAW", TextBox5.Text)
                    cmd1.Parameters.AddWithValue("@MAT_AVG", 0)
                    cmd1.Parameters.AddWithValue("@MAT_STOCK", 0)
                    cmd1.Parameters.AddWithValue("@OPEN_STOCK", 0)
                    cmd1.Parameters.AddWithValue("@RE_ORDER_LABEL", 0)
                    cmd1.Parameters.AddWithValue("@OPEN_AVG_PRICE", 0)
                    cmd1.Parameters.AddWithValue("@MAT_LAST_RATE", 0)
                    cmd1.Parameters.AddWithValue("@ORDER_STOP_IND", "OPEN")
                    cmd1.Parameters.AddWithValue("@MAT_LOCATION", TextBox114.Text)
                    cmd1.Parameters.AddWithValue("@AC_PUR", DropDownList6.Text)
                    cmd1.Parameters.AddWithValue("@AC_ISSUE", DropDownList7.Text)
                    cmd1.Parameters.AddWithValue("@AC_CON", DropDownList8.Text)
                    cmd1.Parameters.AddWithValue("@CHPTR_HEAD", DropDownList1.Text)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()

                    mat_err_label.Visible = True

                    DropDownList4.SelectedValue = "Select"
                    DropDownList5.SelectedValue = "Select"
                    DropDownList6.SelectedValue = "Select"
                    DropDownList7.SelectedValue = "Select"
                    DropDownList8.SelectedValue = "Select"
                    TextBox7.Text = ""
                    TextBox8.Text = ""
                    TextBox4.Text = ""
                    TextBox5.Text = ""
                    TextBox6.Text = ""

                    myTrans.Commit()
                    conn_trans.Close()
                    mat_err_label.Text = "Data Saved Succsessfully."
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    mat_err_label.Text = "There was some Error, please contact EDP."
                Finally
                    conn.Close()
                    conn_trans.Close()
                End Try
                'conn.Close()
            End Using

        End If

    End Sub

    Protected Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        mat_err_label.Visible = False
        mat_err_label.Text = ""
        DropDownList4.SelectedValue = "Select"
        DropDownList5.SelectedValue = "Select"
        DropDownList6.SelectedValue = "Select"
        DropDownList7.SelectedValue = "Select"
        DropDownList8.SelectedValue = "Select"
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub

End Class