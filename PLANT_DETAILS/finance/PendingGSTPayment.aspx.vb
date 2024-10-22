Imports System.Globalization
Imports System.Data.SqlClient
Public Class PendingGSTPayment
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim sqlDataReader As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim working_date As Date = Today.Date
    Dim PARTY_PAYMENT As New Decimal(0)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(6) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR"), New DataColumn("POST_INDICATION"), New DataColumn("TAXABLE_VALUE")})
            ViewState("mat1") = dt1
            Me.BINDGRID1()
            Dim DT00 As New DataTable()
            ds.Clear()
            da = New SqlDataAdapter("select DISTINCT CAST(BillTrackID AS INTEGER) AS TOKEN_NO from PO_RCD_MAT where GST_STATUS='PENDING' AND BillTrackID IS NOT NULL
                                    UNION
                                    select DISTINCT CAST(BillTrackID AS INTEGER) AS TOKEN_NO from mb_book where GST_STATUS='PENDING' AND BillTrackID IS NOT NULL ORDER BY CAST(BillTrackID AS INTEGER)", conn)
            da.Fill(DT00)
            conn.Close()
            DropDownList15.Items.Clear()
            DropDownList15.DataSource = DT00
            DropDownList15.DataValueField = "TOKEN_NO"
            DropDownList15.DataBind()
            DropDownList15.Items.Insert(0, "Select")
            DropDownList15.SelectedValue = ("Select")
            Button10.Enabled = True
            Dim DT As DataTable = DirectCast(ViewState("mat1"), DataTable)
            TextBox169.ReadOnly = True
            TextBox170.ReadOnly = True


            ''DropDownList15.Items.Clear()
            garn_dropdown.Items.Clear()
            TextBox31.Text = ""
            TextBox40.Text = ""
            TextBox34.Text = ""
            TextBox169.Text = ""
            TextBox170.Text = ""
            TextBox35.Text = ""
            TextBox36.Text = 0.0
            TextBox55.Text = ""
            TextBox53.Text = ""
            TextBox1.Text = DateTime.Now.ToString("dd-MM-yyyy")
            TextBox32.Text = DateTime.Now.ToString("dd-MM-yyyy")
            TextBox41.Text = DateTime.Now.ToString("dd-MM-yyyy")
        End If
        CalendarExtender1.EndDate = Now.Date
        TextBox32_CalendarExtender.EndDate = Now.Date
        TextBox41_CalendarExtender.EndDate = Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub BINDGRID1()
        GridView5.DataSource = DirectCast(ViewState("mat1"), DataTable)
        GridView5.DataBind()
    End Sub



    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If DropDownList15.SelectedValue = "Select" Then
            DropDownList15.Focus()
            Dim dt5 As New DataTable()
            dt5.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
            ViewState("mat1") = dt5
            Me.BINDGRID1()
            Return
        End If
        ''SEARCH
        'Dim dt1 As New DataTable()
        'dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
        'ViewState("mat1") = dt1
        'Me.BINDGRID1()
        conn.Open()
        Dim QUARY As String = ""
        QUARY = "SELECT PARTY_AMT.GARN_MB_NO AS GARN_NO_MB_NO ,PARTY_AMT.AC_CODE AS AC_NO ,ACDIC.AC_DESCRIPTION, PARTY_AMT.AMOUNT_DR,PARTY_AMT.AMOUNT_CR FROM PARTY_AMT JOIN ACDIC ON PARTY_AMT.AC_CODE =ACDIC.AC_CODE WHERE PARTY_AMT.TOKEN_NO ='" & DropDownList15.SelectedValue & "' AND PARTY_AMT.POST_TYPE ='SUND' ORDER BY AMOUNT_DR"
        da = New SqlDataAdapter(QUARY, conn)
        Dim dt2 As DataTable = DirectCast(ViewState("mat1"), DataTable)
        da.Fill(dt2)
        conn.Close()
        'ViewState("mat1") = dt2
        'BINDGRID1()
        garn_dropdown.Items.Clear()
        garn_dropdown.DataSource = dt2
        garn_dropdown.DataValueField = "GARN_NO_MB_NO"
        garn_dropdown.DataBind()
        dt2.Clear()
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * FROM inv_data WHERE BILL_ID=" & DropDownList15.SelectedValue
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            TextBox55.Text = dr.Item("PO_NO")
            TextBox169.Text = dr.Item("inv_no")
            TextBox170.Text = dr.Item("inv_date")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim PO_TYPE As New String("")
        conn.Open()
        ''Dim mc1 As New SqlCommand
        mc1.CommandText = "SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()

            PO_TYPE = dr.Item("PO_TYPE")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        If (PO_TYPE = "FREIGHT INWARD" Or PO_TYPE = "FREIGHT OUTWARD") Then
            mc1.CommandText = "DECLARE @TT TABLE(Prov_Value DECIMAL(16,2))
                            INSERT INTO @TT
                            select (case when sum(valuation_amt)>0 then sum(valuation_amt) else 0 end) AS Prov_Value from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                            select sum(Prov_Value) as Prov_Value from @TT"
            garn_dropdown.Items.Clear()
            garn_dropdown.Items.Insert(0, "All")
        Else
            mc1.CommandText = "DECLARE @TT TABLE(Prov_Value DECIMAL(16,2))
                            INSERT INTO @TT
                            select (case when sum(valuation_amt)>0 then sum(valuation_amt) else 0 end) AS Prov_Value from mb_book where mb_no='" & garn_dropdown.SelectedValue & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                            UNION
                            select (case when sum(PROV_VALUE)>0 then sum(PROV_VALUE) else 0 end) AS Prov_Value from PO_RCD_MAT where GARN_NO='" & garn_dropdown.SelectedValue & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                            select sum(Prov_Value) as Prov_Value from @TT"
        End If

        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            TextBox36.Text = dr.Item("Prov_Value")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc2 As New SqlCommand
        mc2.CommandText = "select (SUPL_ID + ' , ' +SUPL_NAME ) AS SUPL_DETAIL,SUPL_STATE_CODE from SUPL join ORDER_DETAILS on ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID JOIN inv_data ON inv_data .PO_NO = ORDER_DETAILS .SO_NO WHERE inv_data.BILL_ID=" & DropDownList15.SelectedValue
        mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            TextBox35.Text = dr.Item("SUPL_DETAIL")
            If (IsDBNull(dr.Item("SUPL_STATE_CODE"))) Then
            Else
                If (dr.Item("SUPL_STATE_CODE") = "22") Then
                    TextBox4.Text = 0
                    TextBox4.ReadOnly = True
                    TextBox2.ReadOnly = False
                    TextBox3.ReadOnly = False
                Else
                    TextBox2.Text = 0
                    TextBox3.Text = 0
                    TextBox2.ReadOnly = True
                    TextBox3.ReadOnly = True
                    TextBox4.ReadOnly = False
                End If
            End If
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Button13.Enabled = True

        Label624.Text = ""
    End Sub

    Protected Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If TextBox31.Text = "" Then
            TextBox31.Focus()
            Label447.Visible = True
            Label447.Text = "Please Enter Sec SlNo"
            Return
        ElseIf IsDate(TextBox32.Text) = False Then
            TextBox32.Focus()
            Label447.Visible = True
            Label447.Text = "Please Enter Sec Date"
            Return
        ElseIf TextBox40.Text = "" Then
            TextBox40.Focus()
            Label447.Visible = True
            Label447.Text = "Please Enter Je No"
            Return
        ElseIf IsDate(TextBox41.Text) = False Then
            TextBox41.Focus()
            Label447.Visible = True
            Label447.Text = "Please Enter Je Date"
            Return
        ElseIf GridView5.Rows.Count = 0 Then
            Label447.Visible = True
            Label447.Text = "Please Enter Data first"
            Return
        ElseIf IsNumeric(TextBox36.Text) = False Then
            Label447.Visible = True
            Label447.Text = "Please Enter Integer In Net Amount Value"
            Return
        ElseIf CDec(TextBox36.Text) = 0 Then
            Label447.Visible = True
            Label447.Text = "Please Enter Net Amount Higher Than 0"
            Return
        ElseIf TextBox169.Text = "" Then
            TextBox169.Focus()
            Label447.Visible = True
            Label447.Text = "Invoice No. Not Blank"
            Return
        ElseIf IsDate(TextBox170.Text) = False Then
            TextBox170.Focus()
            Label447.Visible = True
            Label447.Text = "Invoice Date Not Blank"
            Return
        End If

        Panel6.Visible = True


    End Sub

    Protected Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim dt1 As New DataTable()
        dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
        ViewState("mat1") = dt1
        Me.BINDGRID1()
    End Sub


    Protected Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click
        Dim strarr As String() = TextBox35.Text.Split(","c)
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date
                working_date = CDate(TextBox1.Text)

                '''''''''''''''''''''''''''''''''
                ''Checking Journal voucher entry date and Freeze date
                Dim Block_DATE As String = ""
                conn.Open()
                Dim MC_new As New SqlCommand
                MC_new.CommandText = "SELECT Block_date_finance_JE FROM Date_Freeze"
                MC_new.Connection = conn
                dr = MC_new.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    Block_DATE = dr.Item("Block_date_finance_JE")
                    dr.Close()
                End If
                conn.Close()

                If (CDate(TextBox1.Text) <= CDate(Block_DATE)) Then
                    Label447.Visible = True
                    Label447.Text = "Payment voucher entry before " & Block_DATE & " has been freezed."

                Else
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
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                    If password = TextBox174.Text Then



                        Dim cr, dr As Decimal
                        cr = 0
                        dr = 0
                        Dim i As Integer

                        If debitTextBox.Text <> crTextBox10.Text Then
                            Label447.Text = "Debit amount is Not matching the Credit amount"
                            Return
                        End If
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
                        TextBox53.Text = STR1 & count + 1


                        Dim query As String = "INSERT INTO VOUCHER (SUPL_NAME,REF_DATE,PO_NO,INV_NO,TOKEN_NO,TOKEN_DATE,JE_NO,JE_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,GARN_MB_BOOK,BILL_TRACK)VALUES(@SUPL_NAME,@REF_DATE,@PO_NO,@INV_NO,@TOKEN_NO,@TOKEN_DATE,@JE_NO,@JE_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@GARN_MB_BOOK,@BILL_TRACK)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@TOKEN_NO", TextBox53.Text)
                        cmd.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@JE_NO", TextBox40.Text)
                        cmd.Parameters.AddWithValue("@JE_DATE", Date.ParseExact(TextBox41.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@SEC_NO", TextBox31.Text)
                        cmd.Parameters.AddWithValue("@SEC_DATE", Date.ParseExact(TextBox32.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@VOUCHER_TYPE", "B.P.V")
                        cmd.Parameters.AddWithValue("@PAY_TYPE", "Current")
                        cmd.Parameters.AddWithValue("@NET_AMT", CDec(txtTotalGST.Text))
                        cmd.Parameters.AddWithValue("@PARTICULAR", TextBox34.Text)
                        cmd.Parameters.AddWithValue("@SUPL_ID", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                        cmd.Parameters.AddWithValue("@SUPL_NAME", strarr(1))
                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd.Parameters.AddWithValue("@GARN_MB_BOOK", "")
                        cmd.Parameters.AddWithValue("@BILL_TRACK", DropDownList15.SelectedValue)
                        cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                        cmd.Parameters.AddWithValue("@PO_NO", TextBox55.Text)
                        cmd.Parameters.AddWithValue("@INV_NO", TextBox169.Text)
                        cmd.Parameters.AddWithValue("@REF_DATE", Date.ParseExact(CDate(TextBox170.Text), "dd-MM-yyyy", provider))
                        cmd.ExecuteReader()
                        cmd.Dispose()


                        '''''''''''''''''''''''''''''''''
                        ''search po type
                        Dim order_type, po_type, quary_ord_type, payment_type, IUCA_HEAD, TEST As String
                        order_type = ""
                        po_type = ""
                        quary_ord_type = ""
                        payment_type = ""
                        IUCA_HEAD = ""
                        TEST = ""

                        conn.Open()
                        Dim mct1 As New SqlCommand
                        TEST = TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1).Trim
                        mct1.CommandText = "select IUCA_HEAD from SUPL WHERE SUPL_ID = '" & TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1).Trim & "'"
                        mct1.Connection = conn
                        sqlDataReader = mct1.ExecuteReader
                        If sqlDataReader.HasRows Then
                            sqlDataReader.Read()
                            If IsDBNull(sqlDataReader.Item("IUCA_HEAD")) Then

                            Else
                                IUCA_HEAD = sqlDataReader.Item("IUCA_HEAD")
                            End If

                            sqlDataReader.Close()
                        End If
                        conn.Close()

                        conn.Open()
                        Dim mct As New SqlCommand
                        mct.CommandText = "select ORDER_DETAILS.PAYMENT_MODE, ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_ACTUAL_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & TextBox55.Text & "'"
                        mct.Connection = conn
                        sqlDataReader = mct.ExecuteReader
                        If sqlDataReader.HasRows Then
                            sqlDataReader.Read()
                            order_type = sqlDataReader.Item("ORDER_TYPE")
                            po_type = sqlDataReader.Item("PO_TYPE")
                            payment_type = sqlDataReader.Item("PAYMENT_MODE")
                            sqlDataReader.Close()
                        End If
                        conn.Close()

                        For i = 0 To GridView5.Rows.Count - 1
                            ''LEDGER ENTRIES
                            query = "Insert Into LEDGER(AGING_FLAG,PO_NO,BILL_TRACK_ID,GARN_NO_MB_NO,INVOICE_NO,POST_INDICATION,VOUCHER_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,PAYMENT_INDICATION)VALUES(@AGING_FLAG,@PO_NO,@BILL_TRACK_ID,@GARN_NO_MB_NO,@INVOICE_NO,@POST_INDICATION,@VOUCHER_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@PAYMENT_INDICATION)"
                            cmd = New SqlCommand(query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                            cmd.Parameters.AddWithValue("@Journal_ID", TextBox31.Text)
                            cmd.Parameters.AddWithValue("@SUPL_ID", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", GridView5.Rows(i).Cells(0).Text)
                            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
                            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                            cmd.Parameters.AddWithValue("@AC_NO", GridView5.Rows(i).Cells(1).Text)
                            cmd.Parameters.AddWithValue("@AMOUNT_DR", CDec(GridView5.Rows(i).Cells(3).Text))
                            cmd.Parameters.AddWithValue("@AMOUNT_CR", CDec(GridView5.Rows(i).Cells(4).Text))
                            cmd.Parameters.AddWithValue("@POST_INDICATION", GridView5.Rows(i).Cells(5).Text)
                            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                            cmd.Parameters.AddWithValue("@INVOICE_NO", TextBox169.Text)
                            cmd.Parameters.AddWithValue("@BILL_TRACK_ID", DropDownList15.SelectedValue)
                            cmd.Parameters.AddWithValue("@PO_NO", TextBox55.Text)
                            cmd.Parameters.AddWithValue("@AGING_FLAG", TextBox169.Text)
                            cmd.ExecuteReader()
                            cmd.Dispose()
                        Next




                        If order_type = "Purchase Order" Then

                            quary_ord_type = "SELECT DISTINCT work_group.psc_head, work_group.adv_pay, work_group.bank, work_group.sund_head ,ACDIC.ac_description  FROM ORDER_DETAILS JOIN inv_data ON ORDER_DETAILS .SO_NO =inv_data .po_no JOIN work_group ON ORDER_DETAILS .PO_TYPE   =work_group .work_type JOIN ACDIC ON work_group .sund_head =ACDIC .ac_code WHERE inv_data .bill_id  =" & DropDownList15.SelectedValue

                        ElseIf order_type = "Work Order" Then

                            quary_ord_type = "SELECT DISTINCT work_group.psc_head, work_group.adv_pay, work_group.bank, work_group.sund_head ,ACDIC.ac_description  FROM WO_ORDER JOIN inv_data ON WO_ORDER .PO_NO =inv_data .po_no JOIN work_group ON WO_ORDER .WO_TYPE  =work_group .work_type JOIN ACDIC ON work_group .sund_head =ACDIC .ac_code WHERE inv_data .bill_id  =" & DropDownList15.SelectedValue

                        ElseIf order_type = "Rate Contract" Then

                            If (po_type = "STORE MATERIAL") Then
                                quary_ord_type = "SELECT DISTINCT work_group.psc_head, work_group.adv_pay, work_group.bank, work_group.sund_head ,ACDIC.ac_description  FROM ORDER_DETAILS JOIN inv_data ON ORDER_DETAILS .SO_NO =inv_data .po_no JOIN work_group ON ORDER_DETAILS .PO_TYPE  =work_group .work_type JOIN ACDIC ON work_group .sund_head =ACDIC .ac_code WHERE inv_data .bill_id  =" & DropDownList15.SelectedValue
                            Else
                                quary_ord_type = "SELECT DISTINCT work_group.psc_head, work_group.adv_pay, work_group.bank, work_group.sund_head ,ACDIC.ac_description  FROM WO_ORDER JOIN inv_data ON WO_ORDER .PO_NO =inv_data .po_no JOIN work_group ON WO_ORDER .WO_TYPE  =work_group .work_type JOIN ACDIC ON work_group .sund_head =ACDIC .ac_code WHERE inv_data .bill_id  =" & DropDownList15.SelectedValue
                            End If

                        End If
                        Dim ac_head, ac_descrip, adv_head, bank_head, psc_head As String
                        ac_head = ""
                        ac_descrip = ""
                        adv_head = ""
                        bank_head = ""
                        psc_head = ""
                        conn.Open()
                        Dim mc1 As New SqlCommand
                        mc1.CommandText = quary_ord_type
                        mc1.Connection = conn
                        sqlDataReader = mc1.ExecuteReader
                        If sqlDataReader.HasRows = True Then
                            sqlDataReader.Read()
                            ac_head = sqlDataReader.Item("sund_head")
                            ac_descrip = sqlDataReader.Item("ac_description")
                            adv_head = sqlDataReader.Item("adv_pay")
                            bank_head = sqlDataReader.Item("bank")
                            psc_head = sqlDataReader.Item("psc_head")
                            sqlDataReader.Close()
                        Else
                            sqlDataReader.Close()
                        End If
                        conn.Close()

                        Dim PSC_PRICE As New Decimal(0)

                        PSC_PRICE = (txtTotalGST.Text - CDec(debitTextBox.Text))
                        If PSC_PRICE > 0 Then
                            ledger_billpass(TextBox55.Text, "PSC_AT_BILL_PASS", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), psc_head, "Dr", PSC_PRICE, "PSC", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)
                        ElseIf PSC_PRICE < 0 Then
                            Dim DIFF As Decimal = 0.0
                            DIFF = PSC_PRICE * (-1)
                            ledger_billpass(TextBox55.Text, "PSC_AT_BILL_PASS", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), psc_head, "Cr", DIFF, "PSC", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)
                        End If


                        ''SUND DEBIT
                        ledger_billpass(TextBox55.Text, "PAYMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), ac_head, "Dr", CDec(txtTotalGST.Text), "SUND", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)
                        ''BANK CREDIT
                        ledger_billpass(TextBox55.Text, "BANK", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), bank_head, "Cr", txtTotalGST.Text, "BANK", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)

                        Dim cmd11 As New SqlCommand
                        Dim Query11 As String = ""
                        If (Left(TextBox55.Text, 1) = "W") Then
                            ''Query11 = "UPDATE mb_book SET GST_STATUS='PAID', GST_PAYMENT_DATE=CONVERT(DATETIME,'" & Now & "',103),GST_PAYMENT_VOUCHER_NO='" & TextBox53.Text & "' WHERE mb_no='" & garn_dropdown.SelectedValue & "'"
                            Query11 = "UPDATE mb_book SET GST_STATUS='PAID', GST_PAYMENT_DATE=CONVERT(DATETIME,'" & Now & "',103),GST_PAYMENT_VOUCHER_NO='" & TextBox53.Text & "' where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'"
                        Else
                            Query11 = "UPDATE PO_RCD_MAT SET GST_STATUS='PAID', GST_PAYMENT_DATE=CONVERT(DATETIME,'" & Now & "',103),GST_PAYMENT_VOUCHER_NO='" & TextBox53.Text & "' WHERE GARN_NO='" & garn_dropdown.SelectedValue & "'"
                        End If

                        cmd11 = New SqlCommand(Query11, conn_trans, myTrans)
                        cmd11.ExecuteReader()
                        cmd11.Dispose()


                        If (DropDownList3.SelectedValue = "Yes") Then

                            Query11 = "UPDATE Taxable_Values SET GST_STATUS='PAID', GST_PAYMENT_DATE=CONVERT(DATETIME,'" & Now & "',103),GST_PAYMENT_VOUCHER_NO='" & TextBox53.Text & "' where INVOICE_NO='" & TextBox169.Text & "' and GST_STATUS='PENDING'"
                            cmd11 = New SqlCommand(Query11, conn_trans, myTrans)
                            cmd11.ExecuteReader()
                            cmd11.Dispose()
                        End If

                        myTrans.Commit()
                        Label624.Visible = True
                        Label624.Text = "All records are written to database."
                        Label447.Text = ""
                        Panel6.Visible = False

                        Dim DT00 As New DataTable()
                        ds.Clear()
                        da = New SqlDataAdapter("select DISTINCT CAST(BillTrackID AS INTEGER) AS TOKEN_NO from PO_RCD_MAT where GST_STATUS='PENDING' AND BillTrackID IS NOT NULL
                                    UNION
                                    select DISTINCT CAST(BillTrackID AS INTEGER) AS TOKEN_NO from mb_book where GST_STATUS='PENDING' AND BillTrackID IS NOT NULL ORDER BY CAST(BillTrackID AS INTEGER)", conn)
                        da.Fill(DT00)
                        conn.Close()
                        DropDownList15.Items.Clear()
                        DropDownList15.DataSource = DT00
                        DropDownList15.DataValueField = "TOKEN_NO"
                        DropDownList15.DataBind()
                        DropDownList15.Items.Insert(0, "Select")
                        DropDownList15.SelectedValue = ("Select")

                    Else
                        Label623.Text = "Auth. Failed"
                        TextBox174.Text = ""
                        Return
                    End If
                End If
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                TextBox53.Text = ""
                Label447.Visible = True
                Label447.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub


    Protected Sub ledger_billpass(so_no As String, garn_mb As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, token_no As String, line_no As Integer, PAY_IND As String, voucher As String)
        Dim working_date As Date
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox1.Text) = False Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        End If
        working_date = CDate(TextBox1.Text)
        If price > 0 Then
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

            Dim aging_flag As New String("")
            Dim mc1 As New SqlCommand
            conn.Open()
            mc1.CommandText = "select inv_no,AGING_FLAG from inv_data WITH(NOLOCK) where bill_id='" & token_no & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (IsDBNull(dr.Item("AGING_FLAG"))) Then
                    aging_flag = dr.Item("inv_no")
                Else
                    aging_flag = dr.Item("AGING_FLAG")
                End If

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            Dim dr_value, cr_value As Decimal
            dr_value = 0.0
            cr_value = 0.0
            If ac_term = "Dr" Then
                dr_value = price
                cr_value = 0.0
            ElseIf ac_term = "Cr" Then
                dr_value = 0.0
                cr_value = price
            End If

            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(AGING_FLAG,VOUCHER_NO,JURNAL_LINE_NO,BILL_TRACK_ID,INVOICE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@AGING_FLAG,@VOUCHER_NO,@JURNAL_LINE_NO,@BILL_TRACK_ID,@INVOICE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn_trans, myTrans)
            cmd.Parameters.AddWithValue("@PO_NO", so_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", garn_mb)
            cmd.Parameters.AddWithValue("@SUPL_ID", dt_id)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@INVOICE_NO", inv_no)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", PAY_IND)
            cmd.Parameters.AddWithValue("@BILL_TRACK_ID", token_no)
            cmd.Parameters.AddWithValue("@JURNAL_LINE_NO", line_no)
            cmd.Parameters.AddWithValue("@VOUCHER_NO", voucher)
            cmd.Parameters.AddWithValue("@AGING_FLAG", aging_flag)
            cmd.ExecuteReader()
            cmd.Dispose()

            ''
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim mc1 As New SqlCommand
        Dim MC6 As New SqlCommand
        ''SEARCH AC HEAD
        Dim SGST_HEAD, CGST_HEAD, IGST_HEAD, SGST_DESC, CGST_DESC, IGST_DESC, PROV_HEAD, SUND_HEAD As New String("")
        Dim TAXABLE_AMOUNT, CGST_VALUE, SGST_VALUE, IGST_VALUE As New Decimal(0)

        Dim order_type, PO_TYPE, SO_STATUS As New String("")

        conn.Open()
        'Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE,PO_TYPE,SO_STATUS  from ORDER_DETAILS WHERE SO_NO = '" & TextBox55.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            PO_TYPE = dr.Item("PO_TYPE")
            SO_STATUS = dr.Item("SO_STATUS")
            dr.Close()
        End If
        ''conn.Close()
        If order_type = "Rate Contract" And SO_STATUS = "RCW" Then

            MC6.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "') and work_type = (select top 1 WO_TYPE from WO_ORDER where PO_NO = '" & TextBox55.Text & "')"

        ElseIf order_type = "Work Order" And (PO_TYPE <> "FREIGHT INWARD" And PO_TYPE <> "FREIGHT OUTWARD") Then 'And (PO_TYPE <> "FREIGHT INWARD" And PO_TYPE <> "FREIGHT OUTWARD") Then

            MC6.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "') and work_type = (select top 1 WO_TYPE from WO_ORDER where PO_NO = '" & TextBox55.Text & "')"

        ElseIf order_type = "Purchase Order" Then

            MC6.CommandText = "select * from work_group where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "')  AND work_name=(SELECT ORDER_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "')"

        ElseIf order_type = "Rate Contract" And (PO_TYPE = "STORE MATERIAL" Or PO_TYPE = "RAW MATERIAL") Then

            ''MC6.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "') and work_type = (select top 1 WO_TYPE from WO_ORDER where PO_NO = '" & TextBox55.Text & "')"
            MC6.CommandText = "select * from work_group where work_name = (select top 1 ORDER_TYPE from ORDER_DETAILS where SO_NO = '" & TextBox55.Text & "') and work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "')"

        ElseIf order_type = "Work Order" And (PO_TYPE = "FREIGHT INWARD" Or PO_TYPE = "FREIGHT OUTWARD") Then

            MC6.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "') and work_type = (select top 1 WO_TYPE from WO_ORDER where PO_NO = '" & TextBox55.Text & "')"

        End If

        MC6.Connection = conn
        dr = MC6.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            PROV_HEAD = dr.Item("prov_head")
            SUND_HEAD = dr.Item("sund_head")
            If (DropDownList3.SelectedValue = "Yes") Then
                If (PO_TYPE = "FREIGHT INWARD" Or PO_TYPE = "FREIGHT OUTWARD") Then
                    IGST_HEAD = dr.Item("IGST_WITHOUT_RCM")
                    CGST_HEAD = dr.Item("CGST_WITHOUT_RCM")
                    SGST_HEAD = dr.Item("SGST_WITHOUT_RCM")
                    CGST_DESC = "CGST RECEIVABLE FINAL-INPUT"
                    SGST_DESC = "SGST RECEIVABLE FINAL-INPUT"
                    IGST_DESC = "IGST RECEIVABLE FINAL-INPUT"
                Else
                    IGST_HEAD = dr.Item("igst")
                    CGST_HEAD = dr.Item("cgst")
                    SGST_HEAD = dr.Item("sgst")
                    CGST_DESC = "CGST RECEIVABLE FINAL-INPUT"
                    SGST_DESC = "SGST RECEIVABLE FINAL-INPUT"
                    IGST_DESC = "IGST RECEIVABLE FINAL-INPUT"
                End If

            Else
                IGST_HEAD = "84811"
                CGST_HEAD = "84811"
                SGST_HEAD = "84811"
                CGST_DESC = "GST EXPENSES"
                SGST_DESC = "GST EXPENSES"
                IGST_DESC = "GST EXPENSES"

            End If
        End If


        'Dim dt As DataTable = DirectCast(ViewState("mat1"), DataTable)
        'dt.Rows.Add(mat_sl_noDropDownList.Text, mat_id, matnameTextBox.Text, au_TextBox.Text, crr_TextBox.Text, chalan_TextBox.Text, chalandate_TextBox.Text, be_TextBox.Text, bedate_TextBox.Text, bl_noTextBox.Text, bldate_TextBox.Text, qty, chalan_qty_TextBox.Text, rcv_qty_TextBox.Text, CDec(CDec(chalan_qty_TextBox.Text) - CDec(rcv_qty_TextBox.Text)), exqty, sqty, NO_OF_BAG, BAG_WEIGHT, Math.Round(TRANSPORTATION_VALUE, 3), tole_value, AMD_NO, Actual_Material_Weight.Text)
        'ViewState("mat1") = dt
        'Me.BINDGRID1()


        CGST_VALUE = CInt((CDec(TextBox36.Text) * CDec(TextBox2.Text)) / 100)
        SGST_VALUE = CInt((CDec(TextBox36.Text) * CDec(TextBox3.Text)) / 100)
        IGST_VALUE = CInt((CDec(TextBox36.Text) * CDec(TextBox4.Text)) / 100)
        PARTY_PAYMENT = CGST_VALUE + SGST_VALUE + IGST_VALUE

        TextBox5.Text = PARTY_PAYMENT

        Dim dt1 As DataTable = DirectCast(ViewState("mat1"), DataTable)
        ''dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR"), New DataColumn("POST_INDICATION")})
        ''ViewState("mat1") = dt1
        ''Me.BINDGRID1()
        ''dt1.Clear()

        If (PO_TYPE = "FREIGHT INWARD" Or PO_TYPE = "FREIGHT OUTWARD") Then

            ''dt.Clear()
            If (CDec(TextBox4.Text) > 0) Then
                If (IGST_HEAD = "84811") Then
                    da = New SqlDataAdapter("select mb_no as GARN_NO_MB_NO,'" + IGST_HEAD + "' as AC_NO,'" + IGST_DESC + "' as ac_description,igst as AMOUNT_DR,0 as AMOUNT_CR, 'IGST_EXPENSES' as POST_INDICATION,'0' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                                            union
                                            select mb_no as GARN_NO_MB_NO,'" + SUND_HEAD + "' as AC_NO,'SUNDRY CREDITOR' as ac_description,0 as AMOUNT_DR,igst as AMOUNT_CR, 'SUND' as POST_INDICATION,'" + TextBox36.Text + "' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'", conn)
                Else
                    da = New SqlDataAdapter("select mb_no as GARN_NO_MB_NO,'" + IGST_HEAD + "' as AC_NO,'" + IGST_DESC + "' as ac_description,igst as AMOUNT_DR,0 as AMOUNT_CR, 'IGST' as POST_INDICATION,'0' as TAXABLE_VALUE  from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                                            union
                                            select mb_no as GARN_NO_MB_NO,'" + SUND_HEAD + "' as AC_NO,'SUNDRY CREDITOR' as ac_description,0 as AMOUNT_DR,igst as AMOUNT_CR, 'SUND' as POST_INDICATION,'" + TextBox36.Text + "' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'", conn)
                End If

            Else
                If (CGST_HEAD = "84811") Then
                    da = New SqlDataAdapter("select mb_no as GARN_NO_MB_NO,'" + CGST_HEAD + "' as AC_NO,'" + CGST_DESC + "' as ac_description,cgst as AMOUNT_DR,0 as AMOUNT_CR, 'CGST_EXPENSES' as POST_INDICATION,'0' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                                            union
                                            select mb_no as GARN_NO_MB_NO,'" + SGST_HEAD + "' as AC_NO,'" + SGST_DESC + "' as ac_description,sgst as AMOUNT_DR,0 as AMOUNT_CR, 'SGST_EXPENSES' as POST_INDICATION,'0' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                                            union
                                            select mb_no as GARN_NO_MB_NO,'" + SUND_HEAD + "' as AC_NO,'SUNDRY CREDITOR' as ac_description,0 as AMOUNT_DR,(cgst+sgst) as AMOUNT_CR, 'SUND' as POST_INDICATION,'" + TextBox36.Text + "' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'", conn)
                Else
                    da = New SqlDataAdapter("select mb_no as GARN_NO_MB_NO,'" + CGST_HEAD + "' as AC_NO,'" + CGST_DESC + "' as ac_description,cgst as AMOUNT_DR,0 as AMOUNT_CR, 'CGST' as POST_INDICATION,'0' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                                            union
                                            select mb_no as GARN_NO_MB_NO,'" + SGST_HEAD + "' as AC_NO,'" + SGST_DESC + "' as ac_description,sgst as AMOUNT_DR,0 as AMOUNT_CR, 'SGST' as POST_INDICATION,'0' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'
                                            union
                                            select mb_no as GARN_NO_MB_NO,'" + SUND_HEAD + "' as AC_NO,'SUNDRY CREDITOR' as ac_description,0 as AMOUNT_DR,(cgst+sgst) as AMOUNT_CR, 'SUND' as POST_INDICATION,'" + TextBox36.Text + "' as TAXABLE_VALUE from mb_book where inv_no='" & TextBox169.Text & "' and v_ind ='V' AND PO_NO='" & TextBox55.Text & "' and GST_STATUS='PENDING'", conn)
                End If

            End If



            ''da.Fill(dt1)
            dt1.Rows.Add(da)
            ViewState("mat1") = dt1
            Me.BINDGRID1()

        Else
            If (CDec(TextBox4.Text) > 0) Then
                If (IGST_HEAD = "84811") Then
                    dt1.Rows.Add(garn_dropdown.SelectedValue, IGST_HEAD, IGST_DESC, IGST_VALUE, 0, "IGST_EXPENSES", "0")
                Else
                    dt1.Rows.Add(garn_dropdown.SelectedValue, IGST_HEAD, IGST_DESC, IGST_VALUE, 0, "IGST", "0")
                End If

            Else
                If (CGST_HEAD = "84811") Then
                    dt1.Rows.Add(garn_dropdown.SelectedValue, CGST_HEAD, CGST_DESC, CGST_VALUE, 0, "CGST_EXPENSES", "0")
                    dt1.Rows.Add(garn_dropdown.SelectedValue, SGST_HEAD, SGST_DESC, SGST_VALUE, 0, "SGST_EXPENSES", "0")
                Else
                    dt1.Rows.Add(garn_dropdown.SelectedValue, CGST_HEAD, CGST_DESC, CGST_VALUE, 0, "CGST", "0")
                    dt1.Rows.Add(garn_dropdown.SelectedValue, SGST_HEAD, SGST_DESC, SGST_VALUE, 0, "SGST", "0")
                End If

            End If
            dt1.Rows.Add(garn_dropdown.SelectedValue, SUND_HEAD, "SUNDRY CREDITOR", 0, PARTY_PAYMENT, "SUND", TextBox36.Text)

            ViewState("mat1") = dt1
            Me.BINDGRID1()
        End If

        conn.Close()

        Dim i As Integer
        Dim drr, crr, totalTaxableValue, totalGST As New Decimal(0)

        For i = 0 To GridView5.Rows.Count - 1
            drr = drr + CDec(GridView5.Rows(i).Cells(3).Text)
            crr = crr + CDec(GridView5.Rows(i).Cells(4).Text)
            totalTaxableValue = totalTaxableValue + CDec(GridView5.Rows(i).Cells(6).Text)
            totalGST = totalGST + CDec(GridView5.Rows(i).Cells(4).Text)
        Next
        debitTextBox.Text = FormatNumber(drr, 2)
        crTextBox10.Text = FormatNumber(crr, 2)
        txtTotalTaxableValue.Text = FormatNumber(totalTaxableValue, 2)
        txtTotalGST.Text = FormatNumber(totalGST, 2)
    End Sub


End Class