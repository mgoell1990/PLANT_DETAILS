Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class debtor
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

    Protected Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click

        If TextBox124.Text = "" Then
            TextBox124.Focus()
            ERR_LABLE0.Text = "Please Enter Customer Code"
            Return
        ElseIf TextBox125.Text = "" Then
            TextBox125.Focus()
            ERR_LABLE0.Text = "Please Enter Company Name"
            Return
        ElseIf SUPLDropDownList18.SelectedValue = "Select" Then
            SUPLDropDownList18.Focus()
            ERR_LABLE0.Text = "Please Select Customer Loc."
            Return
        ElseIf DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            ERR_LABLE0.Text = "Please Select Customer Type"
            Return
        ElseIf TextBox145.Text = "" Then
            TextBox145.Focus()
            ERR_LABLE0.Text = "Please Enter IUCA Code"
            Return
        ElseIf TextBox146.Text = "" Then
            TextBox146.Focus()
            ERR_LABLE0.Text = "Please Enter S.T. Code"
            Return
        ElseIf TextBox149.Text = "" Then
            TextBox149.Focus()
            ERR_LABLE0.Text = "Please Enter Job Code"
            Return
        ElseIf TextBox148.Text = "" Then
            TextBox148.Focus()
            ERR_LABLE0.Text = "Please Enter City"
            Return
        ElseIf TextBox129.Text = "" Then
            TextBox129.Focus()
            ERR_LABLE0.Text = "Please Enter Address 2 field."
            Return
        ElseIf TextBox150.Text = "" Then
            TextBox150.Focus()
            ERR_LABLE0.Text = "Please Enter valid pin code."
            Return
        End If


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry

                'search acc head
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  ac_code from ACDIC WHERE ac_code LIKE '" & TextBox145.Text & "'", conn)
                count = da.Fill(ds, "ACDIC")
                conn.Close()
                If count = 0 Then
                    TextBox145.Text = ""
                    TextBox145.Focus()
                    ERR_LABLE0.Text = "This IUCA Code Not Exists"
                    Return
                End If


                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  ac_code from ACDIC WHERE ac_code LIKE '" & TextBox146.Text & "'", conn)
                count = da.Fill(ds, "ACDIC")
                conn.Close()
                If count = 0 Then
                    TextBox146.Text = ""
                    TextBox146.Focus()
                    ERR_LABLE0.Text = "This S.T. Code Not Exists"
                    Return
                End If

                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  ac_code from ACDIC WHERE ac_code LIKE '" & TextBox149.Text & "'", conn)
                count = da.Fill(ds, "ACDIC")
                conn.Close()
                If count = 0 Then
                    TextBox149.Text = ""
                    TextBox149.Focus()
                    ERR_LABLE0.Text = "This Job Code Not Exists"
                    Return
                End If
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select * from dater WHERE d_code = '" & TextBox124.Text & "'", conn)
                count = da.Fill(ds, "dater")
                conn.Close()
                'save
                Dim s_tax As String = ""
                If SUPLDropDownList18.SelectedValue = "Within State" Then
                    s_tax = "VAT"
                ElseIf SUPLDropDownList18.SelectedValue = "Out Side State" Then
                    s_tax = "C.S.T."
                End If
                If count = 0 Then

                    'conn.Open()
                    Dim query1 As String = "Insert Into dater(d_pin,d_code ,d_name ,add_1 ,add_2 ,d_range ,d_city ,d_coll ,ecc_no ,tin_no ,stock_ac_head ,iuca_head ,supl_loc ,JOB_WORK,DEB_LOC,gst_code ,d_state,d_state_code) values (@d_pin,@d_code ,@d_name ,@add_1 ,@add_2 ,@d_range ,@d_city ,@d_coll ,@ecc_no ,@tin_no ,@stock_ac_head ,@iuca_head ,@supl_loc ,@JOB_WORK,@DEB_LOC,@gst_code ,@d_state,@d_state_code)"
                    Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@d_code", TextBox124.Text.ToUpper)
                    cmd1.Parameters.AddWithValue("@d_name", TextBox125.Text.ToUpper)
                    cmd1.Parameters.AddWithValue("@add_1", TextBox127.Text)
                    cmd1.Parameters.AddWithValue("@add_2", TextBox129.Text)
                    cmd1.Parameters.AddWithValue("@d_range", TextBox147.Text)
                    cmd1.Parameters.AddWithValue("@d_city", TextBox148.Text)
                    cmd1.Parameters.AddWithValue("@d_coll", TextBox142.Text)
                    cmd1.Parameters.AddWithValue("@ecc_no", TextBox144.Text)
                    cmd1.Parameters.AddWithValue("@tin_no", TextBox143.Text)
                    cmd1.Parameters.AddWithValue("@stock_ac_head", TextBox146.Text)
                    cmd1.Parameters.AddWithValue("@iuca_head", TextBox145.Text)
                    cmd1.Parameters.AddWithValue("@supl_loc", DropDownList1.SelectedValue)
                    cmd1.Parameters.AddWithValue("@JOB_WORK", TextBox149.Text)
                    cmd1.Parameters.AddWithValue("@DEB_LOC", SUPLDropDownList18.SelectedValue)
                    cmd1.Parameters.AddWithValue("@gst_code", TextBox1.Text)
                    cmd1.Parameters.AddWithValue("@d_state", TextBox2.Text)
                    cmd1.Parameters.AddWithValue("@d_state_code", TextBox3.Text)
                    cmd1.Parameters.AddWithValue("@d_pin", TextBox150.Text)
                    'cmd1.Parameters.AddWithValue("@email", TextBox136.Text)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()

                    ERR_LABLE0.Text = "Data Saved"


                Else
                    'update
                    'conn.Open()
                    Dim query1 As String = "update dater set d_pin=@d_pin,d_name=@d_name ,add_1=@add_1 ,add_2=@add_2 ,d_range=@d_range ,d_city=@d_city ,d_coll=@d_coll ,ecc_no=@ecc_no ,tin_no=@tin_no ,stock_ac_head=@stock_ac_head ,iuca_head=@iuca_head ,supl_loc=@supl_loc ,JOB_WORK=@JOB_WORK,DEB_LOC=@DEB_LOC,gst_code=@gst_code,d_state=@d_state,d_state_code=@d_state_code where d_code='" & TextBox124.Text & "'"
                    Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@d_name", TextBox125.Text.ToUpper)
                    cmd1.Parameters.AddWithValue("@add_1", TextBox127.Text)
                    cmd1.Parameters.AddWithValue("@add_2", TextBox129.Text)
                    cmd1.Parameters.AddWithValue("@d_range", TextBox147.Text)
                    cmd1.Parameters.AddWithValue("@d_city", TextBox148.Text)
                    cmd1.Parameters.AddWithValue("@d_coll", TextBox142.Text)
                    cmd1.Parameters.AddWithValue("@ecc_no", TextBox144.Text)
                    cmd1.Parameters.AddWithValue("@tin_no", TextBox143.Text)
                    cmd1.Parameters.AddWithValue("@stock_ac_head", TextBox146.Text)
                    cmd1.Parameters.AddWithValue("@iuca_head", TextBox145.Text)
                    cmd1.Parameters.AddWithValue("@supl_loc", DropDownList1.SelectedValue)
                    cmd1.Parameters.AddWithValue("@JOB_WORK", TextBox149.Text)
                    cmd1.Parameters.AddWithValue("@DEB_LOC", SUPLDropDownList18.SelectedValue)
                    cmd1.Parameters.AddWithValue("@gst_code", TextBox1.Text)
                    cmd1.Parameters.AddWithValue("@d_state", TextBox2.Text)
                    cmd1.Parameters.AddWithValue("@d_state_code", TextBox3.Text)
                    cmd1.Parameters.AddWithValue("@d_pin", TextBox150.Text)
                    'cmd1.Parameters.AddWithValue("@email", TextBox136.Text)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()

                    ERR_LABLE0.Text = "Data Updated"
                End If
                TextBox124.Text = ""
                TextBox125.Text = ""
                TextBox127.Text = ""
                TextBox129.Text = ""
                TextBox142.Text = ""
                TextBox143.Text = ""
                TextBox144.Text = ""
                TextBox145.Text = ""
                TextBox146.Text = ""
                TextBox147.Text = ""
                TextBox148.Text = ""
                TextBox149.Text = ""
                TextBox150.Text = ""

                myTrans.Commit()
                conn_trans.Close()
                ERR_LABLE0.Text = "All records are written to database."

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                ERR_LABLE0.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

            SUPLDropDownList18.SelectedValue = "Select"
            DropDownList1.SelectedValue = "Select"

            conn.Close()

        End Using


    End Sub

    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        TextBox124.Text = ""
        TextBox125.Text = ""
        TextBox127.Text = ""
        TextBox129.Text = ""
        'TextBox136.Text = ""
        TextBox142.Text = ""
        TextBox143.Text = ""
        TextBox144.Text = ""
        TextBox145.Text = ""
        TextBox146.Text = ""
        TextBox147.Text = ""
        TextBox148.Text = ""
        TextBox149.Text = ""
        SUPLDropDownList18.SelectedValue = "Select"
        DropDownList1.SelectedValue = "Select"
    End Sub
End Class