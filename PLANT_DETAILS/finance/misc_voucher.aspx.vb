Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.Drawing

Public Class misc_voucher
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
    'Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim DT8 As New DataTable
            DT8.Columns.AddRange(New DataColumn(4) {New DataColumn("SUPL_ID"), New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT"), New DataColumn("INVOICE_NO")})
            ViewState("ext_pmt1") = DT8
            Me.BINDGRID13()
            Dim DT7 As New DataTable
            DT7.Columns.AddRange(New DataColumn(5) {New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR"), New DataColumn("SUPL_ID"), New DataColumn("SUPL_NAME")})
            ViewState("ext_pmt") = DT7
            Me.BINDGRID12()
            TextBox2.Text = "0.00"
        End If
        CalendarExtender1.EndDate = DateTime.Now.Date
        pay_date_TextBox78_CalendarExtender.EndDate = DateTime.Now.Date
        TextBox180_CalendarExtender.EndDate = DateTime.Now.Date
        TextBox178_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub
    Protected Sub BINDGRID13()
        pay_GridView8.DataSource = DirectCast(ViewState("ext_pmt1"), DataTable)
        pay_GridView8.DataBind()
    End Sub
    Protected Sub BINDGRID12()
        jpay_GridView9.DataSource = DirectCast(ViewState("ext_pmt"), DataTable)
        jpay_GridView9.DataBind()
    End Sub
    Protected Sub pay_mode_DropDownList37_SelectedIndexChanged(sender As Object, e As EventArgs) Handles pay_mode_DropDownList37.SelectedIndexChanged
        If pay_mode_DropDownList37.SelectedValue = "Select" Then
            pay_mode_DropDownList37.Focus()
            pay_mode_DropDownList37.Focus()
            Return
        ElseIf pay_mode_DropDownList37.SelectedValue = "Direct Exp." Then
            pay_supl_code_TextBox98.Text = "N/A"
            pay_supl_code_TextBox98.Enabled = False
            pay_mode_DropDownList37.Focus()
        ElseIf pay_mode_DropDownList37.SelectedValue = "Advance" Then
            pay_supl_code_TextBox98.Text = ""
            pay_supl_code_TextBox98.Enabled = True
            pay_mode_DropDownList37.Focus()
        ElseIf pay_mode_DropDownList37.SelectedValue = "Through Liab." Then
            pay_supl_code_TextBox98.Text = ""
            pay_supl_code_TextBox98.Enabled = True
            pay_mode_DropDownList37.Focus()
        End If
    End Sub

    Protected Sub PAY_Button36_Click(sender As Object, e As EventArgs) Handles PAY_Button36.Click
        Dim working_date As Date = CDate(TextBox1.Text)
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

        Dim taxableFlag As Boolean = False

        For i = 0 To jpay_GridView9.Rows.Count - 1

            Dim acHead As String = jpay_GridView9.Rows(i).Cells(0).Text
            If (acHead = "64811" Or acHead = "64812" Or acHead = "64813" Or acHead = "64821" Or acHead = "64822" Or acHead = "64823") Then
                taxableFlag = True
            End If
        Next

        If (taxableFlag) Then
            ''Checking if same invoice number is present in the system or not
            conn.Open()
            Dim sqlCmd As New SqlCommand
            sqlCmd.CommandText = "SELECT * FROM Taxable_Values WHERE INVOICE_NO='" & TextBox177.Text & "' AND SUPL_CODE='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "' and fiscal_year='" & STR1 & "'"
            sqlCmd.Connection = conn
            dr = sqlCmd.ExecuteReader
            If dr.HasRows Then
                Label627.Text = "There is already an invoice registered with this supplier in this financial year."
                conn.Close()
            Else
                proceedForMiscVoucher()
            End If
        Else
            proceedForMiscVoucher()
        End If



    End Sub

    Protected Sub proceedForMiscVoucher()
        Dim working_date As Date = CDate(TextBox1.Text)
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
        conn.Close()
        If (Len(TextBox177.Text) > 16) Then
            Label627.Text = "Invoice number cannot be more than 16 characters."
        Else
            Label627.Text = ""
            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry
                    If pay_sec_slno_TextBox77.Text = "" Then
                        pay_sec_slno_TextBox77.Focus()
                        Return
                    ElseIf pay_date_TextBox78.Text = "" Then
                        pay_date_TextBox78.Focus()
                        Return
                    ElseIf pay_vouch_type_DropDownList36.SelectedValue = "Select" Then
                        pay_vouch_type_DropDownList36.Focus()
                        Return
                    ElseIf pay_mode_DropDownList37.SelectedValue = "Select" Then
                        pay_mode_DropDownList37.Focus()
                        Return
                    ElseIf pay_po_wo_TextBox96.Text = "" Then
                        pay_po_wo_TextBox96.Focus()
                        Return
                    ElseIf pay_garn_TextBox97.Text = "" Then
                        pay_garn_TextBox97.Focus()
                        Return
                    ElseIf pay_narration_TextBox81.Text = "" Then
                        pay_narration_TextBox81.Focus()
                        Return
                    ElseIf TextBox178.Text = "" Or IsDate(TextBox178.Text) = False Then
                        TextBox178.Text = ""
                        TextBox178.Focus()
                        Return
                    ElseIf pay_GridView8.Rows.Count = 0 Then
                        Return
                    End If
                    'Dim working_date As Date
                    If TextBox1.Text = "" Then
                        TextBox1.Text = ""
                        TextBox1.Focus()
                        Return
                    ElseIf IsDate(TextBox1.Text) = False Then
                        TextBox1.Text = ""
                        TextBox1.Focus()
                        Return
                    End If

                    '''''''''''''''''''''''''''''''''
                    ''Checking Cheque entry date and Freeze date
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
                        Label638.Visible = True
                        Label638.Text = "Miscelaneous voucher entry before " & Block_DATE & " has been freezed."

                    Else

                        'working_date = CDate(TextBox1.Text)
                        ''check supl
                        Dim supl_id, supl_name, bank_supl_id As String
                        supl_id = ""
                        supl_name = ""
                        bank_supl_id = ""
                        If pay_mode_DropDownList37.SelectedValue = "Direct Exp." Then
                            supl_id = "N/A"
                            bank_supl_id = "N/A"
                            supl_name = pay_paid_TextBox82.Text
                        ElseIf pay_mode_DropDownList37.SelectedValue = "Advance" Then
                            count = 0
                            conn.Open()
                            Dim dt1 As New DataTable()
                            da = New SqlDataAdapter("select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "'", conn)
                            count = da.Fill(dt1)
                            conn.Close()
                            If count = 0 Then
                                Label638.Text = "Suppl. Not Found"
                                pay_supl_code_TextBox98.Focus()
                                Return
                            End If
                            conn.Open()
                            Dim MC As New SqlCommand
                            MC.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "'"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()

                                supl_id = dr.Item("supl_id")
                                supl_name = dr.Item("supl_name")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()
                        ElseIf pay_mode_DropDownList37.SelectedValue = "Through Liab." Then
                            count = 0
                            conn.Open()
                            Dim dt1 As New DataTable()
                            da = New SqlDataAdapter("select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "'", conn)
                            count = da.Fill(dt1)
                            conn.Close()
                            If count = 0 Then
                                Label638.Text = "Suppl. Not Found"
                                pay_supl_code_TextBox98.Focus()
                                Return
                            End If
                            conn.Open()
                            Dim MC As New SqlCommand
                            MC.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_paid_TextBox82.Text.Substring(0, pay_paid_TextBox82.Text.IndexOf(",") - 1) & "'"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                supl_id = dr.Item("supl_id")
                                supl_name = dr.Item("supl_name")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()
                        End If
                        ''token no generated

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
                        pay_vou_TextBox76.Text = STR1 & count + 1
                        Label638.Text = "Token No Generated"
                        ''save voucher

                        Dim i As Integer
                        Dim net_pay As Decimal = 0.0
                        For i = 0 To pay_GridView8.Rows.Count - 1
                            net_pay = net_pay + CDec(pay_GridView8.Rows(i).Cells(3).Text)
                        Next


                        Dim query As String = "INSERT INTO VOUCHER (SUPL_NAME,REF_DATE,JE_NO,JE_DATE,INV_NO,TOKEN_NO,TOKEN_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR)VALUES(@SUPL_NAME,@REF_DATE,@JE_NO,@JE_DATE,@INV_NO,@TOKEN_NO,@TOKEN_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@TOKEN_NO", pay_vou_TextBox76.Text)
                        cmd.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@SEC_NO", pay_sec_slno_TextBox77.Text)
                        cmd.Parameters.AddWithValue("@SEC_DATE", Date.ParseExact(pay_date_TextBox78.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@JE_NO", TextBox179.Text)
                        cmd.Parameters.AddWithValue("@JE_DATE", Date.ParseExact(TextBox180.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@VOUCHER_TYPE", pay_vouch_type_DropDownList36.SelectedValue)
                        cmd.Parameters.AddWithValue("@PAY_TYPE", pay_mode_DropDownList37.SelectedValue)
                        cmd.Parameters.AddWithValue("@NET_AMT", net_pay)
                        cmd.Parameters.AddWithValue("@PARTICULAR", pay_narration_TextBox81.Text)
                        cmd.Parameters.AddWithValue("@SUPL_ID", supl_id)
                        cmd.Parameters.AddWithValue("@SUPL_NAME", supl_name)
                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd.Parameters.AddWithValue("@REF_DATE", Date.ParseExact(TextBox178.Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@INV_NO", TextBox177.Text)
                        cmd.Parameters.AddWithValue("@PO_NO", pay_po_wo_TextBox96.Text)
                        cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                        cmd.ExecuteReader()
                        cmd.Dispose()

                        ''SAVE LEDGER
                        For i = 0 To pay_GridView8.Rows.Count - 1
                            ''SUND DEBIT
                            ledger_billpass(pay_po_wo_TextBox96.Text, "PAYMENT", pay_GridView8.Rows(i).Cells(4).Text, pay_GridView8.Rows(i).Cells(0).Text, pay_GridView8.Rows(i).Cells(1).Text, "Dr", CDec(pay_GridView8.Rows(i).Cells(3).Text), pay_mode_DropDownList37.SelectedValue, "", jpay_GridView9.Rows.Count + 1 + i, "X", pay_vou_TextBox76.Text)
                            ''insert party amount

                            If pay_mode_DropDownList37.SelectedValue <> "Direct Exp." Then
                                Dim SUPL_DETAILS As String = ""
                                conn.Open()
                                Dim MC As New SqlCommand
                                'MC.CommandText = "select supl_name from supl where supl_id='" & pay_GridView8.Rows(i).Cells(0).Text & "'"
                                MC.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_GridView8.Rows(i).Cells(0).Text & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_GridView8.Rows(i).Cells(0).Text & "'"
                                MC.Connection = conn
                                dr = MC.ExecuteReader
                                If dr.HasRows Then
                                    dr.Read()
                                    SUPL_DETAILS = dr.Item("supl_name")
                                    dr.Close()
                                Else
                                    conn.Close()
                                End If
                                conn.Close()

                                query = "INSERT INTO PARTY_AMT(VOUCHER_NO,POST_TYPE,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@VOUCHER_NO,@POST_TYPE,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                Dim cmd_1 As New SqlCommand(query, conn_trans, myTrans)
                                cmd_1.Parameters.AddWithValue("@SUPL_CODE", pay_GridView8.Rows(i).Cells(0).Text)
                                cmd_1.Parameters.AddWithValue("@SUPL_NAME", SUPL_DETAILS)
                                cmd_1.Parameters.AddWithValue("@TOKEN_NO", "N/A")
                                cmd_1.Parameters.AddWithValue("@ORDER_NO", pay_po_wo_TextBox96.Text)
                                cmd_1.Parameters.AddWithValue("@GARN_MB_NO", pay_garn_TextBox97.Text)
                                cmd_1.Parameters.AddWithValue("@AC_CODE", pay_GridView8.Rows(i).Cells(0).Text)
                                cmd_1.Parameters.AddWithValue("@AMOUNT_DR", CDec(pay_GridView8.Rows(i).Cells(3).Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_CR", CDec(pay_GridView8.Rows(i).Cells(3).Text))
                                cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                                cmd_1.Parameters.AddWithValue("@POST_TYPE", "SUND")
                                cmd_1.Parameters.AddWithValue("@VOUCHER_NO", pay_vou_TextBox76.Text)
                                cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                cmd_1.ExecuteReader()
                                cmd_1.Dispose()

                            End If

                        Next
                        ''BANK CREDIT
                        If pay_vouch_type_DropDownList36.SelectedValue = "B.P.V" Then
                            ledger_billpass("", "BANK", TextBox177.Text, supl_id, "50514", "Cr", net_pay, "BANK", "", pay_GridView8.Rows.Count + jpay_GridView9.Rows.Count + 1, "X", pay_vou_TextBox76.Text)
                        ElseIf pay_vouch_type_DropDownList36.SelectedValue = "C.B.V" Then
                            ledger_billpass("", "BANK", TextBox177.Text, supl_id, "62701", "Cr", net_pay, "CASH BANK", "", pay_GridView8.Rows.Count + jpay_GridView9.Rows.Count + 1, "X", pay_vou_TextBox76.Text)
                        End If


                        ''insert journal vou
                        Dim CGST_AMT, SGST_AMT, IGST_AMT, RCM_CGST_AMT, RCM_SGST_AMT, RCM_IGST_AMT, TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0)
                        Dim taxableFlag As Boolean = False
                        Dim Query1, gstPartyCode, gstPartyName As New String("")
                        i = 0
                        For i = 0 To jpay_GridView9.Rows.Count - 1

                            Dim acHead As String = jpay_GridView9.Rows(i).Cells(0).Text
                            Query1 = "Insert Into LEDGER(AGING_FLAG_NEW,AGING_FLAG,Journal_ID,VOUCHER_NO,JURNAL_LINE_NO,BILL_TRACK_ID,INVOICE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@AGING_FLAG_NEW,@AGING_FLAG,@Journal_ID,@VOUCHER_NO,@JURNAL_LINE_NO,@BILL_TRACK_ID,@INVOICE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
                            cmd = New SqlCommand(Query1, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@PO_NO", pay_po_wo_TextBox96.Text)
                            cmd.Parameters.AddWithValue("@Journal_ID", "JE")
                            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", pay_garn_TextBox97.Text)
                            cmd.Parameters.AddWithValue("@SUPL_ID", jpay_GridView9.Rows(i).Cells(4).Text)
                            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                            cmd.Parameters.AddWithValue("@INVOICE_NO", TextBox177.Text)
                            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
                            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
                            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                            cmd.Parameters.AddWithValue("@AC_NO", jpay_GridView9.Rows(i).Cells(0).Text)
                            cmd.Parameters.AddWithValue("@AMOUNT_DR", CDec(jpay_GridView9.Rows(i).Cells(2).Text))
                            cmd.Parameters.AddWithValue("@AMOUNT_CR", CDec(jpay_GridView9.Rows(i).Cells(3).Text))
                            cmd.Parameters.AddWithValue("@POST_INDICATION", "")
                            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                            cmd.Parameters.AddWithValue("@BILL_TRACK_ID", "")
                            cmd.Parameters.AddWithValue("@JURNAL_LINE_NO", i + pay_GridView8.Rows.Count)
                            cmd.Parameters.AddWithValue("@VOUCHER_NO", pay_vou_TextBox76.Text)
                            cmd.Parameters.AddWithValue("@AGING_FLAG", TextBox177.Text)
                            cmd.Parameters.AddWithValue("@AGING_FLAG_NEW", TextBox177.Text)
                            cmd.ExecuteReader()
                            cmd.Dispose()

                            If (acHead = "64811" Or acHead = "64812" Or acHead = "64813" Or acHead = "64821" Or acHead = "64822" Or acHead = "64823") Then
                                taxableFlag = True
                                gstPartyCode = jpay_GridView9.Rows(i).Cells(4).Text
                                gstPartyName = jpay_GridView9.Rows(i).Cells(5).Text
                            End If

                            If (acHead = "64811") Then
                                IGST_AMT = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "64812") Then
                                CGST_AMT = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "64813") Then
                                SGST_AMT = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "64821") Then
                                RCM_IGST_AMT = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "64822") Then
                                RCM_CGST_AMT = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "64823") Then
                                RCM_SGST_AMT = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "51787") Then
                                TDS_SGST = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "51788") Then
                                TDS_CGST = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            ElseIf (acHead = "51789") Then
                                TDS_IGST = CDec(jpay_GridView9.Rows(i).Cells(2).Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                            End If

                        Next

                        'Dim supplierName As New String("")
                        If (taxableFlag) Then
                            'supplierCode = pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1)
                            'supplierName = pay_supl_code_TextBox98.Text.Substring(pay_supl_code_TextBox98.Text.IndexOf(","c) + 1).Trim

                            ''INSERT DATA INTO TAXABLE VALUES TABLE
                            Dim splName As String = pay_paid_TextBox82.Text.Substring(pay_paid_TextBox82.Text.IndexOf(","c) + 1).Trim
                            Query1 = "INSERT INTO Taxable_Values (INVOICE_NO,INVOICE_DATE,ENTRY_DATE,RCM_CGST_AMT,RCM_SGST_AMT,RCM_IGST_AMT,RCM_CESS_AMT,GARN_CRR_MB_NO,VALUATION_DATE,DATA_TYPE,SL_NO,SUPL_CODE,SUPL_NAME,TAXABLE_VALUE,FISCAL_YEAR,CGST_PERCENTAGE,SGST_PERCENTAGE,IGST_PERCENTAGE,CESS_PERCENTAGE,CGST_AMT,SGST_AMT,IGST_AMT,CESS_AMT,TAXABLE_LD_PENALTY,CGST_LD_PENALTY,SGST_LD_PENALTY,IGST_LD_PENALTY,CESS_LD_PENALTY,CGST_TDS,SGST_TDS,IGST_TDS,CESS_TDS)VALUES(@INVOICE_NO,@INVOICE_DATE,@ENTRY_DATE,@RCM_CGST_AMT,@RCM_SGST_AMT,@RCM_IGST_AMT,@RCM_CESS_AMT,@GARN_CRR_MB_NO,@VALUATION_DATE,@DATA_TYPE,@SL_NO,@SUPL_CODE,@SUPL_NAME,@TAXABLE_VALUE,@FISCAL_YEAR,@CGST_PERCENTAGE,@SGST_PERCENTAGE,@IGST_PERCENTAGE,@CESS_PERCENTAGE,@CGST_AMT,@SGST_AMT,@IGST_AMT,@CESS_AMT,@TAXABLE_LD_PENALTY,@CGST_LD_PENALTY,@SGST_LD_PENALTY,@IGST_LD_PENALTY,@CESS_LD_PENALTY,@CGST_TDS,@SGST_TDS,@IGST_TDS,@CESS_TDS)"
                            cmd = New SqlCommand(Query1, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@INVOICE_NO", TextBox177.Text)
                            cmd.Parameters.AddWithValue("@INVOICE_DATE", Date.ParseExact(TextBox178.Text, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@GARN_CRR_MB_NO", TextBox177.Text)
                            cmd.Parameters.AddWithValue("@VALUATION_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@DATA_TYPE", "MISCELLANEOUS")
                            cmd.Parameters.AddWithValue("@SL_NO", 1)
                            cmd.Parameters.AddWithValue("@SUPL_CODE", gstPartyCode)
                            cmd.Parameters.AddWithValue("@SUPL_NAME", gstPartyName)
                            cmd.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(txtTaxableAmount.Text))
                            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                            cmd.Parameters.AddWithValue("@SGST_PERCENTAGE", CInt(TextBox6.Text))
                            cmd.Parameters.AddWithValue("@CGST_PERCENTAGE", CInt(TextBox5.Text))
                            cmd.Parameters.AddWithValue("@IGST_PERCENTAGE", CInt(TextBox181.Text))
                            cmd.Parameters.AddWithValue("@CESS_PERCENTAGE", 0)
                            cmd.Parameters.AddWithValue("@SGST_AMT", SGST_AMT)
                            cmd.Parameters.AddWithValue("@CGST_AMT", CGST_AMT)
                            cmd.Parameters.AddWithValue("@IGST_AMT", IGST_AMT)
                            cmd.Parameters.AddWithValue("@CESS_AMT", 0)
                            cmd.Parameters.AddWithValue("@RCM_SGST_AMT", RCM_SGST_AMT)
                            cmd.Parameters.AddWithValue("@RCM_CGST_AMT", RCM_CGST_AMT)
                            cmd.Parameters.AddWithValue("@RCM_IGST_AMT", RCM_IGST_AMT)
                            cmd.Parameters.AddWithValue("@RCM_CESS_AMT", 0)
                            cmd.Parameters.AddWithValue("@TAXABLE_LD_PENALTY", 0)
                            cmd.Parameters.AddWithValue("@SGST_LD_PENALTY", 0)
                            cmd.Parameters.AddWithValue("@CGST_LD_PENALTY", 0)
                            cmd.Parameters.AddWithValue("@IGST_LD_PENALTY", 0)
                            cmd.Parameters.AddWithValue("@CESS_LD_PENALTY", 0)
                            cmd.Parameters.AddWithValue("@SGST_TDS", TDS_SGST)
                            cmd.Parameters.AddWithValue("@CGST_TDS", TDS_CGST)
                            cmd.Parameters.AddWithValue("@IGST_TDS", TDS_IGST)
                            cmd.Parameters.AddWithValue("@CESS_TDS", 0)
                            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                            cmd.ExecuteReader()
                            cmd.Dispose()

                        End If

                        Dim DT8 As New DataTable
                        DT8.Columns.AddRange(New DataColumn(3) {New DataColumn("SUPL_ID"), New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT")})
                        ViewState("ext_pmt1") = DT8
                        Me.BINDGRID13()
                        Dim DT7 As New DataTable
                        DT7.Columns.AddRange(New DataColumn(5) {New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR"), New DataColumn("SUPL_ID"), New DataColumn("SUPL_NAME")})
                        ViewState("ext_pmt") = DT7
                        Me.BINDGRID12()
                        PAY_Button36.Enabled = False

                        myTrans.Commit()
                        Label627.Visible = True
                        Label627.ForeColor = Drawing.Color.Red
                        Label627.Text = "All records are written to database."
                    End If


                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    Label627.Visible = True
                    Label627.ForeColor = Drawing.Color.Red
                    Label627.Text = "There was some Error, please contact EDP."
                    pay_vou_TextBox76.Text = ""
                Finally
                    conn.Close()
                    conn_trans.Close()
                End Try

            End Using
        End If
    End Sub

    Protected Sub ledger_billpass(so_no As String, garn_mb As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, token_no As String, line_no As Integer, PAY_IND As String, voucher As String)
        Dim working_date As Date
        If TextBox1.Text = "" Then
            TextBox1.Text = ""
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
            Dim Query As String = "Insert Into LEDGER(AGING_FLAG_NEW,AGING_FLAG,VOUCHER_NO,JURNAL_LINE_NO,BILL_TRACK_ID,INVOICE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@AGING_FLAG_NEW,@AGING_FLAG,@VOUCHER_NO,@JURNAL_LINE_NO,@BILL_TRACK_ID,@INVOICE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
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
            cmd.Parameters.AddWithValue("@AGING_FLAG", inv_no)
            cmd.Parameters.AddWithValue("@AGING_FLAG_NEW", inv_no)
            cmd.ExecuteReader()
            cmd.Dispose()

            ''
        End If
    End Sub

    Protected Sub Button60_Click(sender As Object, e As EventArgs) Handles Button60.Click
        pay_vou_TextBox76.Text = ""
        pay_sec_slno_TextBox77.Text = ""
        pay_date_TextBox78.Text = ""
        TextBox179.Text = ""
        TextBox180.Text = ""
        pay_vouch_type_DropDownList36.SelectedValue = "B.P.V"
        pay_mode_DropDownList37.SelectedValue = "Select"
        pay_po_wo_TextBox96.Text = "NA"
        pay_garn_TextBox97.Text = "NA"
        TextBox177.Text = "NA"
        TextBox178.Text = ""
        pay_supl_code_TextBox98.Text = ""
        pay_narration_TextBox81.Text = "NA"
        pay_ac_head_TextBox95.Text = ""
        pay_amount_TextBox89.Text = ""
        PAY_Button36.Enabled = True
    End Sub

    Protected Sub PAY_LinkButton1_Click(sender As Object, e As EventArgs) Handles PAY_LinkButton1.Click
        PAY_LinkButton1.Visible = False
        JNL_Panel23.Visible = True
        PAY_Panel22.Visible = False
    End Sub

    Protected Sub PAY_Button35_Click(sender As Object, e As EventArgs) Handles PAY_Button35.Click
        If pay_sec_slno_TextBox77.Text = "" Then
            pay_sec_slno_TextBox77.Focus()
            Return
        ElseIf pay_date_TextBox78.Text = "" Then
            pay_date_TextBox78.Focus()
            Return
        ElseIf pay_vouch_type_DropDownList36.SelectedValue = "Select" Then
            pay_vouch_type_DropDownList36.Focus()
            Return
        ElseIf pay_mode_DropDownList37.SelectedValue = "Select" Then
            pay_mode_DropDownList37.Focus()
            Return
        ElseIf pay_po_wo_TextBox96.Text = "" Then
            pay_po_wo_TextBox96.Focus()
            Return
        ElseIf pay_garn_TextBox97.Text = "" Then
            pay_garn_TextBox97.Focus()
            Return
        ElseIf pay_supl_code_TextBox98.Text = "" Then
            pay_supl_code_TextBox98.Focus()
            Return
        ElseIf pay_narration_TextBox81.Text = "" Then
            pay_narration_TextBox81.Focus()
            Return
        ElseIf pay_ac_head_TextBox95.Text = "" Then
            pay_ac_head_TextBox95.Focus()
            Return
        ElseIf pay_amount_TextBox89.Text = "" Then
            pay_amount_TextBox89.Focus()
            Return
        ElseIf (Len(TextBox177.Text) > 16) Then
            Label627.Text = "Invoice number cannot be more than 16 characters."
            Label627.ForeColor = Color.Red
            Return
        ElseIf TextBox177.Text.Contains(" ") Then
            Label627.Text = "Space is not allowed in invoice number"
            Label627.ForeColor = Color.Red
            Return
        ElseIf (TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox181.Text = "") Then
            Label627.Text = "GST percentage cannot be left blank"
            Label627.ForeColor = Color.Red
            Return
        End If


        Dim gstHead As New String("")
        gstHead = pay_ac_head_TextBox95.Text.Substring(0, pay_ac_head_TextBox95.Text.IndexOf(",") - 1).Trim


        If ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And (txtTaxableAmount.Text = "0" Or txtTaxableAmount.Text = "")) Then
            txtTaxableAmount.Focus()
            Label638.Text = "Please enter taxable amount in case of GST"
            Return
        Else

            If ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And (TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox181.Text = "")) Then
                Label638.Text = "Please enter valid GST percentage"
                Return
            ElseIf ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And ((CDec(TextBox5.Text) + CDec(TextBox6.Text) + CDec(TextBox181.Text)) = 0)) Then
                Label638.Text = "Please enter valid GST percentage"
                Return
            ElseIf ((gstHead = "64811" Or gstHead = "64821") And (CDec(TextBox181.Text) = 0)) Then
                Label638.Text = "Please enter valid IGST percentage only"
                Return
            ElseIf ((gstHead = "64812" Or gstHead = "64822") And (CDec(TextBox5.Text) = 0)) Then
                Label638.Text = "Please enter valid CGST percentage"
                Return
            ElseIf ((gstHead = "64813" Or gstHead = "64823") And (CDec(TextBox6.Text) = 0)) Then
                Label638.Text = "Please enter valid SGST percentage"
                Return
            Else

                Label627.Text = ""
                ''check supl
                Dim supl_id, supl_name As String
                supl_id = ""
                supl_name = ""
                If pay_mode_DropDownList37.SelectedValue = "Direct Exp." Then
                    supl_id = "N/A"
                    supl_name = pay_supl_code_TextBox98.Text
                ElseIf pay_mode_DropDownList37.SelectedValue = "Advance" Then
                    count = 0
                    conn.Open()
                    Dim dt5 As New DataTable()
                    'da = New SqlDataAdapter("select * from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'", conn)
                    da = New SqlDataAdapter("select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'", conn)
                    count = da.Fill(dt5)
                    conn.Close()
                    If count = 0 Then
                        Label638.Text = "Suppl. Not Found"
                        pay_supl_code_TextBox98.Focus()
                        Return
                    End If
                    conn.Open()
                    Dim MC1 As New SqlCommand
                    'MC1.CommandText = "select * from supl where supl_id='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'"
                    MC1.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'"
                    MC1.Connection = conn
                    dr = MC1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        supl_id = dr.Item("supl_id")
                        supl_name = dr.Item("supl_name")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                ElseIf pay_mode_DropDownList37.SelectedValue = "Through Liab." Then
                    count = 0
                    conn.Open()
                    Dim dt5 As New DataTable()
                    'da = New SqlDataAdapter("select * from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select * from dater where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'", conn)
                    da = New SqlDataAdapter("select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'", conn)
                    count = da.Fill(dt5)
                    conn.Close()
                    If count = 0 Then
                        Label638.Text = "Suppl. Not Found"
                        pay_supl_code_TextBox98.Focus()
                        Return
                    End If
                    conn.Open()
                    Dim MC1 As New SqlCommand
                    'MC1.CommandText = "select * from supl where supl_id='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'"
                    MC1.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'"
                    MC1.Connection = conn
                    dr = MC1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        supl_id = dr.Item("supl_id")
                        supl_name = dr.Item("supl_name")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                End If
                count = 0
                conn.Open()
                Dim dt1 As New DataTable()
                da = New SqlDataAdapter("select * from acdic where ac_code='" & pay_ac_head_TextBox95.Text.Substring(0, pay_ac_head_TextBox95.Text.IndexOf(",") - 1).Trim & "'", conn)
                count = da.Fill(dt1)
                conn.Close()
                If count = 0 Then
                    Label638.Text = "AC Head Not Found"
                    pay_ac_head_TextBox95.Focus()
                    Return
                End If
                Dim ac_head, ac_desc As String
                ac_desc = ""
                ac_head = ""
                conn.Open()
                Dim MC As New SqlCommand
                MC.CommandText = "select * from acdic where ac_code='" & pay_ac_head_TextBox95.Text.Substring(0, pay_ac_head_TextBox95.Text.IndexOf(",") - 1).Trim & "'"
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
                Dim dt8 As DataTable = DirectCast(ViewState("ext_pmt1"), DataTable)
                dt8.Rows.Add(supl_id, ac_head, ac_desc, FormatNumber(CDec(pay_amount_TextBox89.Text), 2), TextBox177.Text)
                ViewState("ext_pmt1") = dt8
                BINDGRID13()
                pay_ac_head_TextBox95.Text = ""
                pay_amount_TextBox89.Text = ""
                TextBox2.Text = 0
                For i = 0 To pay_GridView8.Rows.Count - 1
                    TextBox2.Text = CDec(TextBox2.Text) + CDec(pay_GridView8.Rows(i).Cells(3).Text)
                Next
                pay_ac_head_TextBox95.Focus()
            End If
        End If

    End Sub

    Protected Sub PAY_Button37_Click(sender As Object, e As EventArgs) Handles PAY_Button37.Click
        Dim DT8 As New DataTable
        DT8.Columns.AddRange(New DataColumn(3) {New DataColumn("SUPL_ID"), New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT")})
        ViewState("ext_pmt1") = DT8
        Me.BINDGRID13()
        Dim DT7 As New DataTable
        DT7.Columns.AddRange(New DataColumn(5) {New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR"), New DataColumn("SUPL_ID"), New DataColumn("SUPL_NAME")})
        ViewState("ext_pmt") = DT7
        Me.BINDGRID12()
        TextBox2.Text = "0.00"
    End Sub

    Protected Sub JNL_Button38_Click(sender As Object, e As EventArgs) Handles JNL_Button38.Click
        If jpay_ac_DropDownList38.Text = "" Then
            jpay_ac_DropDownList38.Focus()
            Return
        ElseIf jpay_amount_TextBox95.Text = "" Or IsNumeric(jpay_amount_TextBox95.Text) = False Then
            jpay_amount_TextBox95.Focus()
            Return
        ElseIf jpay_catgory_DropDownList1.SelectedValue = "Select" Then
            jpay_catgory_DropDownList1.Focus()
            Return
        ElseIf pay_vouch_type_DropDownList36.SelectedValue = "Select" Then
            pay_vouch_type_DropDownList36.Focus()
            Return
        ElseIf pay_mode_DropDownList37.SelectedValue = "Select" Then
            pay_mode_DropDownList37.Focus()
            Return
        ElseIf (TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox181.Text = "") Then
            Label638.Text = "GST percentage cannot be left blank"
            Return
        End If

        Dim gstHead As New String("")
        gstHead = jpay_ac_DropDownList38.Text.Substring(0, jpay_ac_DropDownList38.Text.IndexOf(",") - 1).Trim


        If ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And (txtTaxableAmount.Text = "0" Or txtTaxableAmount.Text = "")) Then
            txtTaxableAmount.Focus()
            Label638.Text = "Please enter taxable amount in case of GST"
            Return
        Else

            If ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And (TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox181.Text = "")) Then
                Label638.Text = "Please enter valid GST percentage"
                Return
            ElseIf ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And ((CDec(TextBox5.Text) + CDec(TextBox6.Text) + CDec(TextBox181.Text)) = 0)) Then
                Label638.Text = "Please enter valid GST percentage"
                Return
            ElseIf ((gstHead = "64811" Or gstHead = "64821") And (CDec(TextBox181.Text) = 0)) Then
                Label638.Text = "Please enter valid IGST percentage only"
                Return
            ElseIf ((gstHead = "64812" Or gstHead = "64822") And (CDec(TextBox5.Text) = 0)) Then
                Label638.Text = "Please enter valid CGST percentage"
                Return
            ElseIf ((gstHead = "64813" Or gstHead = "64823") And (CDec(TextBox6.Text) = 0)) Then
                Label638.Text = "Please enter valid SGST percentage"
                Return

            Else

                Label638.Text = ""
                ''check supl
                Dim supl_id, supl_name As String
                supl_id = ""
                supl_name = ""
                If pay_mode_DropDownList37.SelectedValue = "Direct Exp." Then
                    supl_id = "N/A"
                    supl_name = pay_supl_code_TextBox98.Text
                ElseIf pay_mode_DropDownList37.SelectedValue = "Advance" Then
                    count = 0
                    conn.Open()
                    Dim dt5 As New DataTable()
                    da = New SqlDataAdapter("select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'", conn)

                    count = da.Fill(dt5)
                    conn.Close()
                    If count = 0 Then
                        Label638.Text = "Suppl. Not Found"
                        pay_supl_code_TextBox98.Focus()
                        Return
                    End If
                    conn.Open()
                    Dim MC1 As New SqlCommand
                    MC1.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'"
                    MC1.Connection = conn
                    dr = MC1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        supl_id = dr.Item("supl_id")
                        supl_name = dr.Item("supl_name")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                ElseIf pay_mode_DropDownList37.SelectedValue = "Through Liab." Then
                    count = 0
                    conn.Open()
                    Dim dt5 As New DataTable()
                    da = New SqlDataAdapter("select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'", conn)
                    count = da.Fill(dt5)
                    conn.Close()
                    If count = 0 Then
                        Label638.Text = "Suppl. Not Found"
                        pay_supl_code_TextBox98.Focus()
                        Return
                    End If
                    conn.Open()
                    Dim MC1 As New SqlCommand
                    MC1.CommandText = "select SUPL_ID,SUPL_NAME from supl where SUPL_ID='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "' union select d_code as SUPL_ID,d_name As SUPL_NAME from dater where d_code='" & pay_supl_code_TextBox98.Text.Substring(0, pay_supl_code_TextBox98.Text.IndexOf(",") - 1) & "'"
                    MC1.Connection = conn
                    dr = MC1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        supl_id = dr.Item("supl_id")
                        supl_name = dr.Item("supl_name")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                End If
                'search ac head details
                count = 0
                conn.Open()
                Dim dt1 As New DataTable()
                da = New SqlDataAdapter("select * from acdic where ac_code='" & jpay_ac_DropDownList38.Text.Substring(0, jpay_ac_DropDownList38.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(dt1)
                conn.Close()
                If count = 0 Then
                    Label638.Text = "AC Head Not Found"
                    jpay_ac_DropDownList38.Focus()
                    Return
                End If
                Dim ac_head, ac_desc As String
                ac_desc = ""
                ac_head = ""
                conn.Open()
                Dim MC As New SqlCommand
                MC.CommandText = "select * from acdic where ac_code='" & jpay_ac_DropDownList38.Text.Substring(0, jpay_ac_DropDownList38.Text.IndexOf(",") - 1) & "'"
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
                Dim dr_amt, cr_amt As Decimal
                dr_amt = 0
                cr_amt = 0
                If jpay_catgory_DropDownList1.SelectedValue = "Dr" Then
                    dr_amt = CDec(jpay_amount_TextBox95.Text)
                    cr_amt = 0
                ElseIf jpay_catgory_DropDownList1.SelectedValue = "Cr" Then
                    cr_amt = CDec(jpay_amount_TextBox95.Text)
                    dr_amt = 0
                End If
                Dim dt7 As DataTable = DirectCast(ViewState("ext_pmt"), DataTable)
                dt7.Rows.Add(ac_head, ac_desc, FormatNumber(dr_amt, 2), FormatNumber(cr_amt, 2), supl_id, supl_name)
                ViewState("ext_pmt") = dt7
                BINDGRID12()
                jpay_ac_DropDownList38.Text = ""
                jpay_amount_TextBox95.Text = ""
                jpay_catgory_DropDownList1.SelectedValue = "Select"
                TextBox3.Text = 0
                TextBox4.Text = 0
                For i = 0 To jpay_GridView9.Rows.Count - 1
                    TextBox3.Text = CDec(TextBox3.Text) + CDec(jpay_GridView9.Rows(i).Cells(2).Text)
                    TextBox4.Text = CDec(TextBox4.Text) + CDec(jpay_GridView9.Rows(i).Cells(3).Text)
                Next
                jpay_ac_DropDownList38.Focus()
            End If

        End If


    End Sub

    Protected Sub JNL_Button39_Click(sender As Object, e As EventArgs) Handles JNL_Button39.Click
        Dim I As Integer
        Dim AMT_DR, AMT_CR As Decimal
        AMT_DR = 0
        AMT_CR = 0
        For I = 0 To jpay_GridView9.Rows.Count - 1
            AMT_DR = CDec(jpay_GridView9.Rows(I).Cells(2).Text) + AMT_DR
            AMT_CR = CDec(jpay_GridView9.Rows(I).Cells(3).Text) + AMT_CR
        Next
        If AMT_CR <> AMT_DR Then
            Label638.Text = "Debit Amount and Credit Amount Not Match"
            Return
        End If

        PAY_LinkButton1.Visible = True
        JNL_Panel23.Visible = False
        PAY_Panel22.Visible = True
        Label638.Text = ""
    End Sub

    Protected Sub JNL_Button40_Click(sender As Object, e As EventArgs) Handles JNL_Button40.Click
        Dim DT7 As New DataTable
        DT7.Columns.AddRange(New DataColumn(5) {New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR"), New DataColumn("SUPL_ID"), New DataColumn("SUPL_NAME")})
        ViewState("ext_pmt") = DT7
        Me.BINDGRID12()
        PAY_LinkButton1.Visible = True
        JNL_Panel23.Visible = False
        PAY_Panel22.Visible = True
        Label638.Text = ""
    End Sub
End Class