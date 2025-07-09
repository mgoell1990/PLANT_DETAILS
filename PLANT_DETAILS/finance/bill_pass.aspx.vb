Imports System.Globalization
Imports System.Data.SqlClient
Public Class bill_pass1
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

            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
            ViewState("mat1") = dt1
            Me.BINDGRID1()
            DropDownList11.SelectedValue = "Select"
            DropDownList2.SelectedValue = "Select"
            DropDownList15.Items.Clear()
            garn_dropdown.Items.Clear()
            TextBox31.Text = ""
            TextBox32.Text = ""
            TextBox40.Text = ""
            TextBox41.Text = ""
            TextBox34.Text = ""
            TextBox169.Text = ""
            TextBox170.Text = ""
            TextBox35.Text = ""
            TextBox36.Text = 0.0
            TextBox37.Text = 0.0
            TextBox55.Text = ""
            TextBox53.Text = ""
        End If
        CalendarExtender1.EndDate = DateTime.Now.Date
        TextBox32_CalendarExtender.EndDate = DateTime.Now.Date
        TextBox41_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub BINDGRID1()
        GridView5.DataSource = DirectCast(ViewState("mat1"), DataTable)
        GridView5.DataBind()
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedValue = "Current" Then
            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
            ViewState("mat1") = dt1
            Me.BINDGRID1()
            Dim DT00 As New DataTable()
            ds.Clear()
            da = New SqlDataAdapter("select distinct CAST(TOKEN_NO AS INTEGER) as TOKEN_NO from PARTY_AMT WHERE AMOUNT_BAL > 0 and VOUCHER_NO is null order by CAST(TOKEN_NO AS INTEGER)", conn)
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

        ElseIf DropDownList2.SelectedValue = "Old" Then
            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
            ViewState("mat1") = dt1
            Me.BINDGRID1()
            Dim DT00 As New DataTable()
            ds.Clear()
            da = New SqlDataAdapter("select distinct CAST(TOKEN_NO AS INTEGER) as TOKEN_NO from PARTY_AMT WHERE AMOUNT_BAL > 0 and VOUCHER_NO is not null order by CAST(TOKEN_NO AS INTEGER)", conn)
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
            TextBox169.ReadOnly = False
            TextBox170.ReadOnly = False

        End If
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
        Dim dt1 As New DataTable()
        dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
        ViewState("mat1") = dt1
        Me.BINDGRID1()
        conn.Open()
        Dim QUARY As String = ""
        If DropDownList2.SelectedValue = "Current" Then
            QUARY = "SELECT PARTY_AMT.GARN_MB_NO AS GARN_NO_MB_NO ,PARTY_AMT.AC_CODE AS AC_NO ,ACDIC.AC_DESCRIPTION, PARTY_AMT.AMOUNT_DR  ,PARTY_AMT.AMOUNT_CR   FROM PARTY_AMT JOIN ACDIC ON PARTY_AMT.AC_CODE =ACDIC.AC_CODE WHERE PARTY_AMT.TOKEN_NO ='" & DropDownList15.SelectedValue & "' AND PARTY_AMT.POST_TYPE ='SUND' ORDER BY AMOUNT_DR"
        ElseIf DropDownList2.SelectedValue = "Old" Then
            QUARY = "SELECT PARTY_AMT.GARN_MB_NO AS GARN_NO_MB_NO ,PARTY_AMT.AC_CODE AS AC_NO ,ACDIC.AC_DESCRIPTION, PARTY_AMT.AMOUNT_CR AS AMOUNT_DR  ,PARTY_AMT.AMOUNT_BAL AS AMOUNT_CR   FROM PARTY_AMT JOIN ACDIC ON PARTY_AMT.AC_CODE =ACDIC.AC_CODE WHERE PARTY_AMT.TOKEN_NO ='" & DropDownList15.SelectedValue & "' AND GARN_MB_NO='SUND' ORDER BY AMOUNT_DR"
        End If
        da = New SqlDataAdapter(QUARY, conn)
        Dim dt2 As DataTable = DirectCast(ViewState("mat1"), DataTable)
        da.Fill(dt2)
        conn.Close()
        ViewState("mat1") = dt2
        BINDGRID1()
        garn_dropdown.Items.Clear()
        garn_dropdown.DataSource = dt2
        garn_dropdown.DataValueField = "GARN_NO_MB_NO"
        garn_dropdown.DataBind()
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select  * FROM inv_data WHERE BILL_ID=" & DropDownList15.SelectedValue
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
        conn.Open()
        Dim mc2 As New SqlCommand
        mc2.CommandText = "select  (SUPL_ID + ' , ' +SUPL_NAME ) AS SUPL_DETAIL  from SUPL join ORDER_DETAILS on ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID JOIN inv_data ON inv_data .PO_NO = ORDER_DETAILS .SO_NO WHERE inv_data.BILL_ID=" & DropDownList15.SelectedValue
        mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            TextBox35.Text = dr.Item("SUPL_DETAIL")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim i As Integer
        Dim drr, crr As Decimal
        drr = 0
        crr = 0
        For i = 0 To GridView5.Rows.Count - 1
            drr = drr + CDec(GridView5.Rows(i).Cells(3).Text)
            crr = crr + CDec(GridView5.Rows(i).Cells(4).Text)
        Next
        debitTextBox.Text = FormatNumber(drr, 2)
        crTextBox10.Text = FormatNumber(crr, 2)
        TextBox37.Text = FormatNumber((drr - crr), 2)
        Button13.Enabled = True
        If DropDownList2.SelectedValue = "Old" Then
            TextBox169.Text = ""
            TextBox170.Text = ""
        End If
        Label624.Text = ""
    End Sub

    Protected Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Label447.Visible = True
            Label447.Text = "Please Choose Pay Type"
            Return
        ElseIf TextBox31.Text = "" Then
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
        ElseIf DropDownList11.SelectedValue = "Select" Then
            DropDownList11.Focus()
            Label447.Visible = True
            Label447.Text = "Please Choose Voucher Type"
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
        ElseIf (CDec(TextBox36.Text) > CDec(TextBox37.Text)) And (CDec(TextBox36.Text) - CDec(TextBox37.Text)) > 1 Then
            Label447.Visible = True
            Label447.Text = "Amount Is Higher Than Payment Amount"
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
    Protected Sub BINDGRID10()
        GridView10.DataSource = DirectCast(ViewState("GV10"), DataTable)
        GridView10.DataBind()
    End Sub
    Protected Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        If Button43.Text = "Preview" Then
            If garn_dropdown.Text = "" Then
                Return
            End If
            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
            ViewState("GV10") = dt1
            Me.BINDGRID10()
            conn.Open()
            da = New SqlDataAdapter("SELECT LEDGER.GARN_NO_MB_NO ,LEDGER.AC_NO,LEDGER.POST_INDICATION ,ACDIC.AC_DESCRIPTION, LEDGER.AMOUNT_DR  ,LEDGER.AMOUNT_CR   FROM LEDGER JOIN ACDIC ON LEDGER.AC_NO =ACDIC.AC_CODE WHERE LEDGER.GARN_NO_MB_NO ='" & garn_dropdown.Text & "' ORDER BY AMOUNT_CR", conn)
            Dim dt2 As DataTable = DirectCast(ViewState("GV10"), DataTable)
            da.Fill(dt2)
            conn.Close()
            ViewState("GV10") = dt2
            BINDGRID10()
            GridView10.Visible = True
            Button43.Text = "Hide"
        Else
            GridView10.Visible = False
            Button43.Text = "Preview"
        End If
    End Sub

    Protected Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
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
                If TextBox174.Text = "" Then
                    TextBox174.Focus()
                    Return
                End If

                '''''''''''''''''''''''''''''''''
                ''Checking Bill passing date and Freeze date
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
                    Label447.Visible = True
                    Label447.Text = "Bill Passing before " & Block_DATE & " has been freezed."
                Else
                    'working_date = Date.ParseExact(CDate(TextBox170.Text), "dd-MM-yyyy", provider)
                    'Date.ParseExact(CDate(TextBox88.Text), "dd-MM-yyyy", provider)
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
                        dr = mct1.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            If IsDBNull(dr.Item("IUCA_HEAD")) Then

                            Else
                                IUCA_HEAD = dr.Item("IUCA_HEAD")
                            End If

                            dr.Close()
                        End If
                        conn.Close()

                        conn.Open()
                        Dim mct As New SqlCommand
                        mct.CommandText = "select ORDER_DETAILS.PAYMENT_MODE, ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_ACTUAL_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & TextBox55.Text & "'"
                        mct.Connection = conn
                        dr = mct.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            order_type = dr.Item("ORDER_TYPE")
                            po_type = dr.Item("PO_TYPE")
                            payment_type = dr.Item("PAYMENT_MODE")
                            dr.Close()
                        End If
                        conn.Close()


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
                        dr = mc1.ExecuteReader
                        If dr.HasRows = True Then
                            dr.Read()
                            ac_head = dr.Item("sund_head")
                            ac_descrip = dr.Item("ac_description")
                            adv_head = dr.Item("adv_pay")
                            bank_head = dr.Item("bank")
                            psc_head = dr.Item("psc_head")
                            dr.Close()
                        Else
                            dr.Close()
                        End If
                        conn.Close()
                        ''save ledger and voucher table
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
                        Dim month1 As Integer = 0
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
                        'ledger posting
                        Dim SUPL_STRING As String = ""
                        Dim SUPL_NAME As String = ""
                        SUPL_STRING = TextBox35.Text.Substring(0, (TextBox35.Text.IndexOf(",") - 1))
                        SUPL_NAME = TextBox35.Text.Substring((TextBox35.Text.IndexOf(",") + 2))

                        Dim query As String = "INSERT INTO VOUCHER (SUPL_NAME,REF_DATE,PO_NO,INV_NO,TOKEN_NO,TOKEN_DATE,JE_NO,JE_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,GARN_MB_BOOK,BILL_TRACK)VALUES(@SUPL_NAME,@REF_DATE,@PO_NO,@INV_NO,@TOKEN_NO,@TOKEN_DATE,@JE_NO,@JE_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@GARN_MB_BOOK,@BILL_TRACK)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@TOKEN_NO", TextBox53.Text)
                        cmd.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@JE_NO", TextBox40.Text)
                        cmd.Parameters.AddWithValue("@JE_DATE", Date.ParseExact(TextBox41.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@SEC_NO", TextBox31.Text)
                        cmd.Parameters.AddWithValue("@SEC_DATE", Date.ParseExact(TextBox41.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@VOUCHER_TYPE", DropDownList11.SelectedValue)
                        cmd.Parameters.AddWithValue("@PAY_TYPE", DropDownList2.SelectedValue)
                        cmd.Parameters.AddWithValue("@NET_AMT", TextBox36.Text)
                        cmd.Parameters.AddWithValue("@PARTICULAR", TextBox34.Text)
                        cmd.Parameters.AddWithValue("@SUPL_ID", SUPL_STRING)
                        cmd.Parameters.AddWithValue("@SUPL_NAME", SUPL_NAME)
                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd.Parameters.AddWithValue("@GARN_MB_BOOK", "")
                        cmd.Parameters.AddWithValue("@BILL_TRACK", DropDownList15.SelectedValue)
                        cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                        cmd.Parameters.AddWithValue("@PO_NO", TextBox55.Text)
                        cmd.Parameters.AddWithValue("@INV_NO", TextBox169.Text)
                        cmd.Parameters.AddWithValue("@REF_DATE", Date.ParseExact(CDate(TextBox170.Text), "dd-MM-yyyy", provider))

                        cmd.ExecuteReader()
                        cmd.Dispose()

                        ''BALANCE AMOUNT UPDATE
                        If DropDownList2.SelectedValue = "Current" Then

                            query = "UPDATE PARTY_AMT SET AMOUNT_BAL=@AMOUNT_BAL,VOUCHER_NO=@VOUCHER_NO WHERE TOKEN_NO='" & DropDownList15.SelectedValue & "'"
                            cmd = New SqlCommand(query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                            cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                            cmd.ExecuteReader()
                            cmd.Dispose()

                        ElseIf DropDownList2.SelectedValue = "Old" Then

                            query = "UPDATE PARTY_AMT SET AMOUNT_BAL=@AMOUNT_BAL WHERE TOKEN_NO='" & DropDownList15.SelectedValue & "'"
                            cmd = New SqlCommand(query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                            cmd.ExecuteReader()
                            cmd.Dispose()

                            ''insert invoice no

                            query = "INSERT INTO inv_data (bill_id,po_no,inv_no,inv_date,post_date,inv_amount,emp_id)VALUES(@bill_id,@po_no,@inv_no,@inv_date,@post_date,@inv_amount,@emp_id)"
                            cmd = New SqlCommand(query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@bill_id", DropDownList15.SelectedValue)
                            cmd.Parameters.AddWithValue("@po_no", TextBox55.Text)
                            cmd.Parameters.AddWithValue("@inv_no", TextBox169.Text)
                            cmd.Parameters.AddWithValue("@inv_date", Date.ParseExact(TextBox170.Text, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@post_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@inv_amount", TextBox36.Text)
                            cmd.Parameters.AddWithValue("@emp_id", Session("userName"))
                            cmd.ExecuteReader()
                            cmd.Dispose()

                        End If

                        If po_type = "RAW MATERIAL" And payment_type = "Advance Payment" Then

                            ''SUND DEBIT
                            ledger_billpass(TextBox55.Text, "PAYMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), ac_head, "Dr", CDec(TextBox37.Text), "SUND", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)
                            ''BANK CREDIT
                            ledger_billpass(TextBox55.Text, "ADVANCE ADJ.", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), adv_head, "Cr", CDec(TextBox36.Text), "Advance_Adj", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)

                        ElseIf po_type = "STORE MATERIAL" And payment_type = "Advance Payment" Then
                            ''SUND DEBIT
                            ledger_billpass(TextBox55.Text, "PAYMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), ac_head, "Dr", CDec(TextBox37.Text), "SUND", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)
                            ''BANK CREDIT
                            ledger_billpass(TextBox55.Text, "ADVANCE ADJ.", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), adv_head, "Cr", CDec(TextBox36.Text), "Advance_Adj", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)

                        ElseIf po_type = "RAW MATERIAL" And payment_type = "Book Adjustment" Then
                            ''SUND DEBIT
                            ledger_billpass(TextBox55.Text, "SUND ADJUSTMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), ac_head, "Dr", CDec(TextBox37.Text), "SUND", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)
                            ''IUCA CREDIT
                            ledger_billpass(TextBox55.Text, "BOOK ADJUSTMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), IUCA_HEAD, "Cr", CDec(TextBox36.Text), "IUCA", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)

                        ElseIf po_type = "STORE MATERIAL" And payment_type = "Book Adjustment" Then
                            ''SUND DEBIT
                            ledger_billpass(TextBox55.Text, "SUND ADJUSTMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), ac_head, "Dr", CDec(TextBox37.Text), "SUND", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)
                            ''IUCA CREDIT
                            ledger_billpass(TextBox55.Text, "BOOK ADJUSTMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), IUCA_HEAD, "Cr", CDec(TextBox36.Text), "IUCA", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)
                        Else
                            ''SUND DEBIT
                            ledger_billpass(TextBox55.Text, "PAYMENT", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), ac_head, "Dr", CDec(TextBox37.Text), "SUND", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)
                            ''BANK CREDIT
                            ledger_billpass(TextBox55.Text, "BANK", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), bank_head, "Cr", CDec(TextBox36.Text), "BANK", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)
                        End If


                        ''INSERT LEDGER PENDING AMOUNT
                        If CDec(TextBox37.Text) > CDec(TextBox36.Text) Then
                            If payment_type = "Advance Payment" Then
                                ''PSC AT THE TIME OF BILL PASS
                                ledger_billpass(TextBox55.Text, "PSC_AT_BILL_PASS", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), "71801", "Cr", CDec(TextBox37.Text) - CDec(TextBox36.Text), "PSC", DropDownList15.SelectedValue, 10, "OK", TextBox53.Text)
                            Else
                                ''PSC AT THE TIME OF BILL PASS
                                ledger_billpass(TextBox55.Text, "PSC_AT_BILL_PASS", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), "71801", "Cr", CDec(TextBox37.Text) - CDec(TextBox36.Text), "PSC", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)
                            End If


                            If po_type = "RAW MATERIAL" And (payment_type = "Advance Payment" Or payment_type = "Book Adjustment") Then


                                Dim query_11 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query_11, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)

                                If (payment_type = "Advance Payment") Then
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "ADVANCE")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", "64205")
                                Else
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "Book Adjustment")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", IUCA_HEAD)
                                End If

                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()

                            ElseIf po_type = "RAW MATERIAL(IMP)" And payment_type = "Advance Payment" Then

                                Dim query_11 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query_11, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)
                                cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "ADVANCE")
                                cmd_1.Parameters.AddWithValue("@AC_CODE", "64206")
                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()


                            ElseIf po_type = "STORE MATERIAL" And (payment_type = "Advance Payment" Or payment_type = "Book Adjustment") Then


                                Dim query_11 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query_11, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)

                                If (payment_type = "Advance Payment") Then
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "ADVANCE")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", "64201")
                                Else
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "Book Adjustment")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", IUCA_HEAD)
                                End If

                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()

                            Else

                                Dim query_112 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_12 As New SqlCommand(query_112, conn_trans, myTrans)
                                cmd_12.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_12.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_12.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_12.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)
                                cmd_12.Parameters.AddWithValue("@GARN_MB_NO", "BANK")
                                cmd_12.Parameters.AddWithValue("@AC_CODE", "50514")
                                cmd_12.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_12.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_12.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_12.Parameters.AddWithValue("@AMOUNT_BAL", CDec(TextBox37.Text) - CDec(TextBox36.Text))
                                cmd_12.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_12.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_12.ExecuteReader()
                                cmd_12.Dispose()

                            End If
                            ''insert party amount

                        ElseIf CDec(TextBox37.Text) = CDec(TextBox36.Text) Then

                            If po_type = "RAW MATERIAL" And (payment_type = "Advance Payment" Or payment_type = "Book Adjustment") Then
                                ''insert party amount

                                Dim query_11 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query_11, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)

                                If (payment_type = "Advance Payment") Then
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "ADVANCE")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", "64205")
                                Else
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "Book Adjustment")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", IUCA_HEAD)
                                End If

                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()

                            ElseIf po_type = "RAW MATERIAL(IMP)" And payment_type = "Advance Payment" Then

                                Dim query_11 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query_11, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)
                                cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "ADVANCE")
                                cmd_1.Parameters.AddWithValue("@AC_CODE", "64206")
                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()


                            ElseIf po_type = "STORE MATERIAL" And (payment_type = "Advance Payment" Or payment_type = "Book Adjustment") Then
                                ''insert party amount

                                Dim query_11 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query_11, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)

                                If (payment_type = "Advance Payment") Then
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "ADVANCE")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", "64201")
                                Else
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "Book Adjustment")
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", IUCA_HEAD)
                                End If

                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()


                            Else
                                ''insert party amount

                                Dim query_11 As String = "INSERT INTO PARTY_AMT(VOUCHER_NO,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query_11, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1))
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox35.Text.Substring(TextBox35.Text.IndexOf(",") + 2))
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList15.SelectedValue)
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", TextBox55.Text)
                                cmd_1.Parameters.AddWithValue("@GARN_MB_NO", "BANK")
                                cmd_1.Parameters.AddWithValue("@AC_CODE", "50514")
                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(TextBox36.Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", 0.0)
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()

                            End If
                        Else
                            ''PSC AT THE TIME OF BILL PASS
                            ledger_billpass(TextBox55.Text, "PSC_AT_BILL_PASS", TextBox169.Text, TextBox35.Text.Substring(0, TextBox35.Text.IndexOf(",") - 1), "84222", "Dr", CDec(TextBox36.Text) - CDec(TextBox37.Text), "PSC", DropDownList15.SelectedValue, 10, "X", TextBox53.Text)
                        End If

                        ''UPDATE BILL TRACK ID STATUS

                        query = "update inv_data set V_IND ='V' WHERE bill_id='" & DropDownList15.SelectedValue & "'"
                        cmd = New SqlCommand(query, conn_trans, myTrans)
                        cmd.ExecuteReader()
                        cmd.Dispose()


                        ''UPDATE  LEDGER
                        query = "UPDATE LEDGER SET VOUCHER_NO=@VOUCHER_NO WHERE BILL_TRACK_ID='" & DropDownList15.SelectedValue & "'"
                        cmd = New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox53.Text)
                        cmd.ExecuteReader()
                        cmd.Dispose()

                        Button13.Enabled = False
                        Panel6.Visible = False
                        Dim dt1 As New DataTable()
                        dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("GARN_NO_MB_NO"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_CR"), New DataColumn("AMOUNT_DR"), New DataColumn("POST_INDICATION")})
                        ViewState("mat1") = dt1
                        Me.BINDGRID1()
                        ''refresh
                        DropDownList11.SelectedValue = "Select"
                        DropDownList2.SelectedValue = "Select"
                        DropDownList15.Items.Clear()
                        garn_dropdown.Items.Clear()
                        TextBox31.Text = ""
                        TextBox32.Text = ""
                        TextBox40.Text = ""
                        TextBox41.Text = ""
                        TextBox34.Text = ""
                        TextBox169.Text = ""
                        TextBox170.Text = ""
                        TextBox35.Text = ""
                        TextBox36.Text = 0.0
                        TextBox37.Text = 0.0
                        TextBox55.Text = ""
                        crTextBox10.Text = "0.00"
                        debitTextBox.Text = "0.00"
                        TextBox37.Text = "0.00"
                        TextBox36.Text = "0.00"
                    Else
                        Label623.Text = "Auth. Failed"
                        TextBox174.Text = ""
                        Return
                    End If

                End If

                myTrans.Commit()
                Label624.Visible = True
                Label447.Visible = True
                Label624.Text = "Voucher No Generated"
                Label447.Text = "Bill Passed"

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
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

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        If CDec(TextBox36.Text) = CDec(TextBox37.Text) Then
            Return
        ElseIf CDec(TextBox37.Text) = 0 Then
            Return
        ElseIf CDec(TextBox36.Text) - CDec(TextBox37.Text) > 10 Or CDec(TextBox37.Text) - CDec(TextBox36.Text) > 10 Then

            Return
        ElseIf GridView5.Rows.Count = 0 Then

            Return
        End If
        Dim drr, crr As Decimal
        drr = 0
        crr = 0
        If CDec(TextBox36.Text) > CDec(TextBox37.Text) Then
            drr = CDec(TextBox36.Text) - CDec(TextBox37.Text)
            crr = 0

        Else
            crr = CDec(TextBox37.Text) - CDec(TextBox36.Text)
            drr = 0
        End If
        Dim ac_head, ac_descrip As String
        ac_head = ""
        ac_descrip = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        'mc1.CommandText = "select work_group .psc_head ,ACDIC .ac_description  from work_group JOIN ACDIC ON work_group .psc_head =ACDIC .ac_code  where work_group.work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "') AND work_group.work_name =(SELECT ORDER_TYPE  FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "')"
        mc1.CommandText = "select work_group .psc_head ,ACDIC .ac_description  from work_group JOIN ACDIC ON work_group .psc_head =ACDIC .ac_code  where work_group.work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TextBox55.Text & "') AND work_group.work_type =(select distinct WO_TYPE from WO_ORDER where PO_NO='" & TextBox55.Text & "')"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            ac_head = dr.Item("psc_head")
            ac_descrip = dr.Item("ac_description")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim dt1 As DataTable = DirectCast(ViewState("mat1"), DataTable)
        dt1.Rows.Add("PSC", ac_head, ac_descrip, drr, crr, "Adjust For PSC")
        ViewState("mat1") = dt1
        BINDGRID1()
        Dim i As Integer
        drr = 0
        crr = 0
        For i = 0 To CInt(GridView5.Rows.Count) - 1
            drr = drr + CDec(GridView5.Rows(i).Cells(3).Text)
            crr = crr + CDec(GridView5.Rows(i).Cells(4).Text)
        Next
        debitTextBox.Text = FormatNumber(drr, 2)
        crTextBox10.Text = FormatNumber(crr, 2)
        'TextBox37.Text = FormatNumber((drr - crr), 2)
        '' Label447.Visible = False
        '' LinkButton1.Visible = False
    End Sub


End Class