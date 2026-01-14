Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class rcd_voucher
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
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim DT9 As New DataTable
            DT9.Columns.AddRange(New DataColumn(7) {New DataColumn("supl_id"), New DataColumn("inst_type"), New DataColumn("inst_no"), New DataColumn("inst_date"), New DataColumn("ac_code"), New DataColumn("amount"), New DataColumn("drawn_on"), New DataColumn("nar")})
            ViewState("rcd") = DT9
            Me.BINDGRID14()

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct order_type  from order_details order by order_type", conn)
            da.Fill(dt)
            DropDownList50.Items.Clear()
            DropDownList50.DataSource = dt
            DropDownList50.DataValueField = "order_type"
            DropDownList50.DataBind()
            conn.Close()
            DropDownList50.Items.Insert(0, "Select")
            DropDownList50.SelectedValue = "Select"

        End If

        CalendarExtender1.EndDate = DateTime.Now.Date
        TextBox57_CalendarExtender.EndDate = DateTime.Now.Date
        TextBox83_CalendarExtender.EndDate = DateTime.Now.Date
        CalendarExtender2.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub

    Protected Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date
                If TextBox1.Text = "" Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                ElseIf IsDate(TextBox1.Text) = False Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                ElseIf TextBox56.Text = "" Then
                    TextBox56.Focus()
                    Label455.Text = "Please Enter Section No"
                    Return
                ElseIf TextBox57.Text = "" Or IsDate(TextBox57.Text) = False Then
                    TextBox57.Focus()
                    Label455.Text = "Please Select Date"
                    Return
                End If

                '''''''''''''''''''''''''''''''''
                ''Checking RCD voucher entry date and Freeze date
                Dim Block_DATE As String = ""
                conn.Open()
                Dim MC_new As New SqlCommand
                MC_new.CommandText = "SELECT Block_date_finance FROM Date_Freeze"
                MC_new.Connection = conn
                dr = MC_new.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    Block_DATE = dr.Item("Block_date_finance")
                    dr.Close()
                End If
                conn.Close()

                If (CDate(TextBox1.Text) <= CDate(Block_DATE)) Then
                    Label455.Visible = True
                    Label455.Text = "RCD voucher entry before " & Block_DATE & " has been freezed."

                Else
                    working_date = CDate(TextBox1.Text)
                    ''SERCH TOKEN NO
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
                    Dim month1 As Integer
                    month1 = working_date.Month
                    Dim qtr1 As String = ""
                    If month1 = 4 Or month1 = 5 Or month1 = 6 Then
                        qtr1 = "Q1"
                    ElseIf month1 = 7 Or month1 = 8 Or month1 = 9 Then
                        qtr1 = "Q2"
                    ElseIf month1 = 10 Or month1 = 11 Or month1 = 12 Then
                        qtr1 = "Q3"
                    ElseIf month1 = 1 Or month1 = 2 Or month1 = 3 Then
                        qtr1 = "Q4"
                    End If
                    count = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select DISTINCT TOKEN_NO FROM VOUCHER WHERE FISCAL_YEAR=" & STR1, conn)
                    count = da.Fill(dt)
                    conn.Close()
                    TextBox59.Text = STR1 & count + 1

                    Dim BPV_NO As String = ""
                    Dim str_temp As String = ""
                    Dim count_cbv_no As Decimal
                    count_cbv_no = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("SELECT CVB_NO FROM VOUCHER WHERE VOUCHER_TYPE ='RCD' AND CVB_NO <>'' AND FISCAL_YEAR=" & STR1, conn)
                    count_cbv_no = da.Fill(dt)
                    conn.Close()
                    If CInt(count_cbv_no) = 0 Then
                        BPV_NO = "RCD" + STR1 + "00001"
                    Else
                        str_temp = count_cbv_no
                        If str_temp.Length = 1 Then
                            str_temp = "0000" & (CInt(count_cbv_no) + 1)
                        ElseIf str_temp.Length = 2 Then
                            str_temp = "000" & (CInt(count_cbv_no) + 1)
                        ElseIf str_temp.Length = 3 Then
                            str_temp = "00" & (CInt(count_cbv_no) + 1)
                        ElseIf str_temp.Length = 4 Then
                            str_temp = "0" & (CInt(count_cbv_no) + 1)
                        Else
                            str_temp = CInt(count_cbv_no) + 1
                        End If
                        BPV_NO = "RCD" + STR1 + str_temp
                    End If

                    TextBox93.Text = BPV_NO
                    ''save rcd voucher
                    If GridView211.Rows.Count > 1 Then
                        Label455.Text = "Please add One RCD Document Only"
                        Return
                    End If
                    Dim i As Integer = 0
                    For i = 0 To GridView211.Rows.Count - 1
                        Dim supl_name As String = ""
                        conn.Open()
                        Dim MC1 As New SqlCommand
                        'MC1.CommandText = "select * from supl where supl_id='" & GridView211.Rows(i).Cells(0).Text & "'"
                        MC1.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & GridView211.Rows(i).Cells(0).Text & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & GridView211.Rows(i).Cells(0).Text & "'"
                        MC1.Connection = conn
                        dr = MC1.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            supl_name = dr.Item("supl_name")
                            dr.Close()
                        Else
                            conn.Close()
                        End If
                        conn.Close()

                        Dim query As String = "INSERT INTO VOUCHER (INV_NO,REF_DATE,BANK_NAME,SUPL_NAME,TOKEN_NO,TOKEN_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,
                                        PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,CVB_NO,CVB_DATE,CHEQUE_NO,CHEQUE_DATE)VALUES
                                        (@INV_NO,@REF_DATE,@BANK_NAME,@SUPL_NAME,@TOKEN_NO,@TOKEN_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@CVB_NO,@CVB_DATE,@CHEQUE_NO,@CHEQUE_DATE)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@TOKEN_NO", TextBox59.Text)
                        cmd.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@SEC_NO", TextBox56.Text)
                        'cmd.Parameters.AddWithValue("@SEC_DATE", Date.ParseExact(TextBox57.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@SEC_DATE", TextBox57.Text)
                        cmd.Parameters.AddWithValue("@VOUCHER_TYPE", "RCD")
                        cmd.Parameters.AddWithValue("@PAY_TYPE", GridView211.Rows(i).Cells(1).Text)
                        cmd.Parameters.AddWithValue("@NET_AMT", CDec(GridView211.Rows(i).Cells(5).Text))
                        cmd.Parameters.AddWithValue("@PARTICULAR", GridView211.Rows(i).Cells(7).Text)
                        cmd.Parameters.AddWithValue("@SUPL_ID", GridView211.Rows(i).Cells(0).Text)
                        cmd.Parameters.AddWithValue("@SUPL_NAME", supl_name)
                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd.Parameters.AddWithValue("@CVB_NO", TextBox93.Text)
                        cmd.Parameters.AddWithValue("@CVB_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@BANK_NAME", GridView211.Rows(i).Cells(6).Text)
                        cmd.Parameters.AddWithValue("@CHEQUE_NO", GridView211.Rows(i).Cells(2).Text)
                        cmd.Parameters.AddWithValue("@CHEQUE_DATE", Date.ParseExact(CDate(GridView211.Rows(i).Cells(3).Text), "dd-MM-yyyy", provider))

                        cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                        cmd.Parameters.AddWithValue("@INV_NO", TextBox2.Text)
                        'cmd.Parameters.AddWithValue("@REF_DATE", Date.ParseExact(TextBox3.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@REF_DATE", TextBox3.Text)
                        cmd.ExecuteReader()
                        cmd.Dispose()


                        ''SAVE LEDGER PARTY CREDIT
                        query = "Insert Into LEDGER(PO_NO,AGING_FLAG_NEW,AGING_FLAG,INVOICE_NO,GARN_NO_MB_NO,VOUCHER_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION,Journal_ID)VALUES(@PO_NO,@AGING_FLAG_NEW,@AGING_FLAG,@INVOICE_NO,@GARN_NO_MB_NO,@VOUCHER_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION,@Journal_ID)"
                        cmd = New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox59.Text)
                        cmd.Parameters.AddWithValue("@Journal_ID", TextBox56.Text)
                        cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", "RCD")
                        cmd.Parameters.AddWithValue("@SUPL_ID", GridView211.Rows(i).Cells(0).Text)
                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd.Parameters.AddWithValue("@PERIOD", qtr1)
                        cmd.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(TextBox1.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                        cmd.Parameters.AddWithValue("@AC_NO", GridView211.Rows(i).Cells(4).Text)
                        cmd.Parameters.AddWithValue("@AMOUNT_DR", 0.0)
                        cmd.Parameters.AddWithValue("@AMOUNT_CR", CDec(GridView211.Rows(i).Cells(5).Text))
                        cmd.Parameters.AddWithValue("@POST_INDICATION", "PAYMENT RCD")
                        cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                        cmd.Parameters.AddWithValue("@INVOICE_NO", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@AGING_FLAG", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@AGING_FLAG_NEW", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@PO_NO", DropDownList49.Text.Substring(0, DropDownList49.Text.IndexOf(",") - 1).Trim())
                        cmd.ExecuteReader()
                        cmd.Dispose()

                    Next
                    Dim net_amount As Decimal = 0
                    For i = 0 To GridView211.Rows.Count - 1
                        net_amount = net_amount + GridView211.Rows(i).Cells(5).Text
                    Next
                    ''SAVE LEDGER BANK DEBIT

                    For i = 0 To GridView211.Rows.Count - 1
                        Dim query1 As String = "Insert Into LEDGER(PO_NO,AGING_FLAG_NEW,AGING_FLAG,INVOICE_NO,GARN_NO_MB_NO,VOUCHER_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION,Journal_ID)VALUES(@PO_NO,@AGING_FLAG_NEW,@AGING_FLAG,@INVOICE_NO,@GARN_NO_MB_NO,@VOUCHER_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION,@Journal_ID)"
                        Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                        cmd1.Parameters.AddWithValue("@VOUCHER_NO", TextBox59.Text)
                        cmd1.Parameters.AddWithValue("@Journal_ID", TextBox56.Text)
                        cmd1.Parameters.AddWithValue("@GARN_NO_MB_NO", "BANK")
                        cmd1.Parameters.AddWithValue("@SUPL_ID", GridView211.Rows(i).Cells(0).Text)
                        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd1.Parameters.AddWithValue("@PERIOD", qtr1)
                        cmd1.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(TextBox1.Text, "dd-MM-yyyy", provider))
                        cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
                        cmd1.Parameters.AddWithValue("@AC_NO", "50514")
                        cmd1.Parameters.AddWithValue("@AMOUNT_DR", CDec(net_amount))
                        cmd1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                        cmd1.Parameters.AddWithValue("@POST_INDICATION", "BANK RCD")
                        cmd1.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                        cmd1.Parameters.AddWithValue("@INVOICE_NO", TextBox2.Text)
                        cmd1.Parameters.AddWithValue("@AGING_FLAG", TextBox2.Text)
                        cmd1.Parameters.AddWithValue("@AGING_FLAG_NEW", TextBox2.Text)
                        cmd1.Parameters.AddWithValue("@PO_NO", DropDownList49.Text.Substring(0, DropDownList49.Text.IndexOf(",") - 1).Trim())
                        cmd1.ExecuteReader()
                        cmd1.Dispose()
                    Next



                    Dim DT9 As New DataTable
                    DT9.Columns.AddRange(New DataColumn(7) {New DataColumn("supl_id"), New DataColumn("inst_type"), New DataColumn("inst_no"), New DataColumn("inst_date"), New DataColumn("ac_code"), New DataColumn("amount"), New DataColumn("drawn_on"), New DataColumn("nar")})
                    ViewState("rcd") = DT9
                    Me.BINDGRID14()

                    DropDownList22.Text = ""
                    DropDownList23.SelectedValue = "Select"

                    myTrans.Commit()
                    Label455.Visible = True
                    Label455.ForeColor = Drawing.Color.Red
                    Label455.Text = "Token No & B.P.V No Generated"
                End If


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label455.Visible = True
                Label455.ForeColor = Drawing.Color.Red
                Label455.Text = "There was some Error, please contact EDP."
                TextBox59.Text = ""
                TextBox93.Text = ""
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using



    End Sub

    Protected Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If DropDownList22.Text = "" Then
            DropDownList22.Focus()
            Label455.Text = "Please Select Supplier"
            Return
        ElseIf DropDownList23.SelectedValue = "Select" Then
            DropDownList23.Focus()
            Label455.Text = "Please Select Inst Type"
            Return
        ElseIf TextBox82.Text = "" Then
            TextBox82.Focus()
            Label455.Text = "Please Fill Inst No"
            Return
        ElseIf TextBox83.Text = "" Or IsDate(TextBox83.Text) = False Then
            TextBox83.Focus()
            Label455.Text = "Please Select Inst Date"
            Return
        ElseIf TextBox84.Text = "" Then
            TextBox84.Focus()
            Label455.Text = "Please Fill Drawn On"
            Return
        ElseIf TextBox58.Text = "" Then
            TextBox58.Focus()
            Label455.Text = "Please Fill Narration"
            Return
        ElseIf DropDownList24.Text = "" Then
            DropDownList24.Focus()
            Label455.Text = "Please Select A/C Head"
            Return
        ElseIf TextBox60.Text = "" Or IsNumeric(TextBox60.Text) = False Then
            TextBox60.Focus()
            Label455.Text = "Please Enter Amount Or Numeric Value"
            Return
        End If

        Dim supl_id, supl_name As String
        supl_id = ""
        supl_name = ""

        count = 0
        'conn.Open()
        'Dim dt5 As New DataTable()
        'da = New SqlDataAdapter("select * from supl where SUPL_ID='" & DropDownList22.Text.Substring(0, DropDownList22.Text.IndexOf(",") - 1) & "'", conn)
        'count = da.Fill(dt5)
        'conn.Close()
        'If count = 0 Then

        '    DropDownList22.Text = ""
        '    DropDownList22.Focus()
        '    Return
        'End If

        If (Left(DropDownList22.Text.Substring(0, DropDownList22.Text.IndexOf(",") - 1), 1) = "D") Then
            conn.Open()
            Dim MC1 As New SqlCommand
            MC1.CommandText = "select * from dater where d_code='" & DropDownList22.Text.Substring(0, DropDownList22.Text.IndexOf(",") - 1) & "'"
            MC1.Connection = conn
            dr = MC1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                supl_id = dr.Item("d_code")
                supl_name = dr.Item("d_name")
                dr.Close()
            Else
                conn.Close()
            End If
            conn.Close()

        Else
            conn.Open()
            Dim MC1 As New SqlCommand
            MC1.CommandText = "select * from supl where supl_id='" & DropDownList22.Text.Substring(0, DropDownList22.Text.IndexOf(",") - 1) & "'"

            MC1.Connection = conn
            dr = MC1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                supl_id = dr.Item("SUPL_ID")
                supl_name = dr.Item("SUPL_NAME")
                dr.Close()
            Else
                conn.Close()
            End If
            conn.Close()
        End If

        count = 0
        conn.Open()
        Dim dt1 As New DataTable()
        da = New SqlDataAdapter("select * from acdic where ac_code='" & DropDownList24.Text.Substring(0, DropDownList24.Text.IndexOf(",") - 1) & "'", conn)
        count = da.Fill(dt1)
        conn.Close()
        If count = 0 Then

            DropDownList24.Text = ""
            DropDownList24.Focus()
            Return
        End If
        Dim ac_head, ac_desc As String
        ac_desc = ""
        ac_head = ""
        conn.Open()
        Dim MC As New SqlCommand
        MC.CommandText = "select * from acdic where ac_code='" & DropDownList24.Text.Substring(0, DropDownList24.Text.IndexOf(",") - 1) & "'"
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            ac_head = dr.Item("ac_code")
            ac_desc = dr.Item("ac_description")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
        Dim dt10 As DataTable = DirectCast(ViewState("rcd"), DataTable)
        dt10.Rows.Add(supl_id, DropDownList23.Text, TextBox82.Text, TextBox83.Text, ac_head, TextBox60.Text, TextBox84.Text, TextBox58.Text)
        ViewState("rcd") = dt10
        BINDGRID14()

        DropDownList22.Text = ""
        DropDownList23.SelectedValue = "Select"
        TextBox82.Text = ""
        TextBox83.Text = ""
        TextBox84.Text = ""
        TextBox58.Text = ""
        DropDownList24.Text = ""
        TextBox60.Text = ""
    End Sub
    Protected Sub BINDGRID14()
        GridView211.DataSource = DirectCast(ViewState("rcd"), DataTable)
        GridView211.DataBind()
    End Sub

    Protected Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click

    End Sub

    Protected Sub DropDownList50_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList50.SelectedIndexChanged
        If DropDownList50.SelectedValue = "Select" Then
            DropDownList50.Focus()
            Return
        End If

        ''ADD FISCAL YEAR IN DROPDOWNLIST
        conn.Open()
        da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
        da.Fill(ds, "FISCAL_YEAR")
        DropDownList1.DataSource = ds.Tables("FISCAL_YEAR")
        DropDownList1.DataValueField = "FY"
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, "Select")
        conn.Close()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()

            DropDownList49.Items.Clear()
            DropDownList49.DataBind()
            Return
        End If

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct (so_no +' , '+ so_actual) as so_no from order_details where order_type='" & DropDownList50.SelectedValue & "' and FINANCE_YEAR='" & DropDownList1.SelectedValue & "' order by so_no", conn)
        da.Fill(dt)
        DropDownList49.Items.Clear()
        DropDownList49.DataSource = dt
        DropDownList49.DataValueField = "so_no"
        DropDownList49.DataBind()
        conn.Close()
        DropDownList49.Items.Insert(0, "Select")
        DropDownList49.Items.Insert(1, "NA , NA")
        DropDownList49.SelectedValue = "Select"
    End Sub
End Class