Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class gst_trans
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

        End If

        CalendarExtender3.EndDate = DateTime.Now.Date
        TextBox57_CalendarExtender.EndDate = DateTime.Now.Date
        TextBox83_CalendarExtender.EndDate = DateTime.Now.Date
        CalendarExtender1.EndDate = DateTime.Now.Date
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
                If TextBox18.Text = "" Then
                    TextBox18.Text = ""
                    TextBox18.Focus()
                    Return
                ElseIf IsDate(TextBox18.Text) = False Then
                    TextBox18.Text = ""
                    TextBox18.Focus()
                    Return
                ElseIf DropDownList23.SelectedValue = "Select" Then
                    DropDownList23.Focus()
                    Label455.Text = "Please Select Inst Type"
                    Return
                ElseIf TextBox56.Text = "" Then
                    TextBox56.Focus()
                    Label455.Text = "Please Fill Sec. No"
                    Return
                ElseIf TextBox82.Text = "" Then
                    TextBox82.Focus()
                    Label455.Text = "Please Fill Inst No"
                    Return
                ElseIf TextBox83.Text = "" Or IsDate(TextBox83.Text) = False Then
                    TextBox83.Focus()
                    Label455.Text = "Please Select Inst Date"
                    Return
                ElseIf TextBox57.Text = "" Or IsDate(TextBox57.Text) = False Then
                    TextBox57.Focus()
                    Label455.Text = "Please Select Sec. Date"
                    Return
                ElseIf TextBox19.Text = "" Then
                    TextBox19.Focus()
                    Label455.Text = "Please Fill IOC No"
                    Return
                ElseIf TextBox84.Text = "" Then
                    TextBox84.Focus()
                    Label455.Text = "Please Fill Drawn On"
                    Return
                ElseIf TextBox58.Text = "" Then
                    TextBox58.Focus()
                    Label455.Text = "Please Fill Narration"
                    Return
                ElseIf TextBox60.Text = "" Or IsNumeric(TextBox60.Text) = False Then
                    TextBox60.Focus()
                    Label455.Text = "Please Enter Amount Or Numeric Value"
                    Return
                ElseIf DropDownList26.SelectedValue = "Select" Then
                    DropDownList26.Focus()
                    Label455.Text = "Please Select Sale Order No"
                    Return
                ElseIf DropDownList27.SelectedValue = "Select" Then
                    DropDownList27.Focus()
                    Label455.Text = "Please Select Item Details"
                    Return
                End If

                '''''''''''''''''''''''''''''''''
                ''Checking Adv receipt voucher entry date and Freeze date
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

                If (CDate(TextBox18.Text) <= CDate(Block_DATE)) Then
                    Label455.Visible = True
                    Label455.Text = "Advance receipt voucher entry before " & Block_DATE & " has been freezed."

                Else
                    Dim STR1 As String = ""
                    If CDate(TextBox18.Text).Month > 3 Then
                        STR1 = CDate(TextBox18.Text).Year
                        STR1 = STR1.Trim.Substring(2)
                        STR1 = STR1 & (STR1 + 1)
                    ElseIf CDate(TextBox18.Text).Month <= 3 Then
                        STR1 = CDate(TextBox18.Text).Year
                        STR1 = STR1.Trim.Substring(2)
                        STR1 = (STR1 - 1) & STR1
                    End If


                    'search order_details for sale type
                    Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, DELIVERY_TERM, dater_code As New String("")
                    conn.Open()
                    Dim mc1 As New SqlCommand
                    mc1.CommandText = "select ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.DELIVERY_TERM, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList26.Text & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        order_type = dr.Item("ORDER_TYPE")
                        po_type = dr.Item("PO_TYPE")
                        SO_DATE = dr.Item("SO_DATE")
                        freight_term = dr.Item("FREIGHT_TERM")
                        ORDER_TO = dr.Item("ORDER_TO")
                        DELIVERY_TERM = dr.Item("DELIVERY_TERM")
                        dr.Close()
                    End If
                    conn.Close()
                    count = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select DISTINCT TOKEN_NO FROM VOUCHER WHERE FISCAL_YEAR=" & STR1, conn)
                    count = da.Fill(dt)
                    conn.Close()
                    TextBox59.Text = STR1 & count + 1
                    ''SEARCH CVB NO
                    count = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("SELECT DISTINCT CVB_NO FROM VOUCHER WHERE VOUCHER_TYPE ='RCD' AND FISCAL_YEAR=" & STR1, conn)
                    count = da.Fill(dt)
                    conn.Close()
                    TextBox93.Text = "RCD" & STR1 & count + 1
                    ''SEARCH RCD VOUCHER NO
                    Dim RCV_TYPE As Integer = 0
                    If ORDER_TO = "I.P.T." Then
                        RCV_TYPE = 4
                    ElseIf ORDER_TO = "Other" Then
                        RCV_TYPE = 1
                    End If

                    conn.Open()
                    Dim inv_no As String = ""
                    Dim mc_c As New SqlCommand
                    mc_c.CommandText = "SELECT (CASE WHEN MAX(VOUCHER_NO) IS NULL THEN 0 ELSE MAX(VOUCHER_NO) END) as VOUCHER_NO  FROM SALE_RCD_VOUCHAR  WHERE VOUCHER_TYPE LIKE 'RV%' AND FISCAL_YEAR =" & STR1
                    mc_c.Connection = conn
                    dr = mc_c.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        inv_no = dr.Item("VOUCHER_NO")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()


                    If CInt(inv_no) = 0 Then
                        TextBox94.Text = "0000001"

                    Else
                        str = CInt(inv_no) + 1
                        If str.Length = 1 Then
                            str = "000000" & CInt(inv_no) + 1
                        ElseIf str.Length = 2 Then
                            str = "00000" & CInt(inv_no) + 1
                        ElseIf str.Length = 3 Then
                            str = "0000" & CInt(inv_no) + 1
                        ElseIf str.Length = 4 Then
                            str = "000" & CInt(inv_no) + 1
                        ElseIf str.Length = 5 Then
                            str = "00" & CInt(inv_no) + 1
                        ElseIf str.Length = 6 Then
                            str = "0" & CInt(inv_no) + 1
                        ElseIf str.Length = 7 Then
                            str = CInt(inv_no) + 1
                        End If
                        TextBox94.Text = str
                    End If



                    Dim HSN_CODE As String = ""
                    Dim quary As String
                    If order_type = "Sale Order" And po_type = "FINISH GOODS" Then
                        quary = "SELECT ITEM_CHPTR AS HSN_CODE FROM F_ITEM WHERE ITEM_CODE =(SELECT DISTINCT ITEM_CODE  FROM SO_MAT_ORDER WHERE SO_NO ='" & DropDownList26.SelectedValue & "' AND ITEM_SLNO ='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "')"
                    Else
                        quary = "SELECT CHPTR_HEAD AS HSN_CODE FROM MATERIAL WHERE MAT_CODE =(SELECT DISTINCT ITEM_CODE  FROM SO_MAT_ORDER WHERE SO_NO ='" & DropDownList26.SelectedValue & "' AND ITEM_SLNO ='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "')"
                    End If

                    conn.Open()
                    mc1.CommandText = quary
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        HSN_CODE = dr.Item("HSN_CODE")
                        dr.Close()
                    End If
                    conn.Close()

                    'search account head
                    'search order_details for sale type
                    Dim adv_head, cgst_head, igst_head, sgst_head, cess_head, terminal_head, bank_head As New String("")
                    conn.Open()
                    mycommand.CommandText = "select ORDER_DETAILS .SO_NO, dater.stock_ac_head,dater.iuca_head,work_group.ed_liab,work_group.bank, work_group.ed_head,work_group.vat_head,work_group.cst_head,work_group.freight_head,work_group.term_tax,work_group.tds_head,work_group.pack_head,work_group .adv_pay,work_group .cgst,work_group .sgst,work_group .igst,work_group .cess from ORDER_DETAILS join DATER on ORDER_DETAILS .PARTY_CODE=DATER.d_code JOIN work_group on ORDER_DETAILS.ORDER_TYPE =work_group.work_name and  ORDER_DETAILS.PO_TYPE  =work_group.work_type and ORDER_DETAILS.ORDER_TO  =work_group.d_type WHERE ORDER_DETAILS.SO_NO='" & DropDownList26.Text & "'"
                    mycommand.Connection = conn
                    dr = mycommand.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        adv_head = dr.Item("adv_pay")
                        cgst_head = dr.Item("cgst")
                        sgst_head = dr.Item("sgst")
                        igst_head = dr.Item("igst")
                        cess_head = dr.Item("cess")
                        terminal_head = dr.Item("term_tax")
                        bank_head = dr.Item("bank")
                        dr.Close()
                    End If
                    conn.Close()
                    'save ledger

                    save_ledger("RV15" & CStr(RCV_TYPE) & TextBox94.Text, TextBox59.Text, DropDownList26.Text, "RV15" & CStr(RCV_TYPE) & TextBox94.Text, DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1), adv_head, "Cr", CDec(TextBox95.Text) + CDec(TextBox96.Text) + CDec(TextBox97.Text) + CDec(TextBox98.Text) + CDec(TextBox99.Text) + CDec(TextBox100.Text) + CDec(TextBox102.Text), "ADV PAY")
                    save_ledger("RV15" & CStr(RCV_TYPE) & TextBox94.Text, TextBox59.Text, DropDownList26.Text, "RV15" & CStr(RCV_TYPE) & TextBox94.Text, DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1), bank_head, "Dr", CDec(TextBox101.Text), "BANK")

                    Dim BILL_PARTY_NAME, BILL_PARTY_ADD, SHIP_PARTY_ADD, BILL_PARTY_state_code, BILL_PARTY_state, SHIP_PARTY_state_code, SHIP_PARTY_state, BILL_PARTY_GST, BILL_PARTY_CODE, SHIP_PARTY_GST, SHIP_PARTY_CODE As New String("")
                    'BILL TO PARTY ADDRESS SEARCH
                    conn.Open()
                    mc1.CommandText = "select d_name,d_name + ' , ' + add_1 + ' , ' + add_2 + ' , ' + ecc_no + ' , ' + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select PARTY_CODE from ORDER_DETAILS where SO_NO ='" & DropDownList26.Text & "')"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        BILL_PARTY_NAME = dr.Item("d_name")
                        BILL_PARTY_CODE = dr.Item("d_code")
                        BILL_PARTY_ADD = dr.Item("party_details")
                        BILL_PARTY_GST = dr.Item("gst_code")
                        BILL_PARTY_state = dr.Item("d_state")
                        BILL_PARTY_state_code = dr.Item("d_state_code")
                        dr.Close()
                    End If
                    conn.Close()

                    'search gst rate
                    Dim cgst, igst, sgst, cess, tcs_rate, terminal_tax As New Decimal(0.0)
                    conn.Open()
                    mc1.CommandText = "select sum(ITEM_CGST) as ITEM_CGST ,sum(ITEM_SGST) as ITEM_SGST ,sum(ITEM_IGST) as ITEM_IGST ,sum(ITEM_CESS) as ITEM_CESS ,sum(ITEM_TERMINAL_TAX) as ITEM_TERMINAL_TAX,sum(ITEM_TCS) AS ITEM_TCS from SO_MAT_ORDER where SO_NO ='" & DropDownList26.SelectedValue & "' and ITEM_SLNO ='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        cgst = dr.Item("ITEM_CGST")
                        sgst = dr.Item("ITEM_SGST")
                        igst = dr.Item("ITEM_IGST")
                        cess = dr.Item("ITEM_CESS")
                        tcs_rate = dr.Item("ITEM_TCS")
                        terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
                        dr.Close()
                    End If
                    conn.Close()


                    'save SALE_RCD_VOUCHAR

                    Dim cmd As New SqlCommand
                    Dim Query As String = "Insert Into SALE_RCD_VOUCHAR(TCS_RATE,TCS_AMT,EMP_ID,FISCAL_YEAR,VOUCHER_NO ,VOUCHER_TYPE ,VOUCHER_DATE ,TOKEN_NO,RCD_PARTY_CODE ,RCD_PARTY_NAME ,RCD_PARTY_ADD ,GST_NO ,STATE_NAME,STATE_CODE,NEG_BRANCH_DEPTT,VOUCHER_AMT ,MODE_OF_PAY ,INST_NO ,TAX_PAY_REVER_CHR ,SO_NO,ITEM_SLNO ,HSN_CODE ,PROD_CODE,PROD_DESC,TAX_VALUE,CENTRAL_TAX_RATE,CENTRAL_TAX_AMT,STATE_TAX_RATE ,STATE_TAX_AMT,INT_TAX_RATE,INT_TAX_AMT ,CESS_TAX_RATE,CESS_TAX_AMT,TT_TAX_RATE,TT_TAX_AMT ,TOTAL_AMT,BAL_AMT,CGST_BAL,SGST_BAL,IGST_BAL,CESS_BAL,VOUCHER_STATUS)VALUES(@TCS_RATE,@TCS_AMT,@EMP_ID,@FISCAL_YEAR,@VOUCHER_NO ,@VOUCHER_TYPE ,@VOUCHER_DATE ,@TOKEN_NO,@RCD_PARTY_CODE ,@RCD_PARTY_NAME ,@RCD_PARTY_ADD ,@GST_NO ,@STATE_NAME,@STATE_CODE,@NEG_BRANCH_DEPTT,@VOUCHER_AMT ,@MODE_OF_PAY ,@INST_NO ,@TAX_PAY_REVER_CHR ,@SO_NO,@ITEM_SLNO ,@HSN_CODE ,@PROD_CODE,@PROD_DESC,@TAX_VALUE,@CENTRAL_TAX_RATE,@CENTRAL_TAX_AMT,@STATE_TAX_RATE ,@STATE_TAX_AMT,@INT_TAX_RATE,@INT_TAX_AMT ,@CESS_TAX_RATE,@CESS_TAX_AMT,@TT_TAX_RATE,@TT_TAX_AMT ,@TOTAL_AMT,@BAL_AMT,@CGST_BAL,@SGST_BAL,@IGST_BAL,@CESS_BAL,@VOUCHER_STATUS)"
                    cmd = New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox94.Text)
                    cmd.Parameters.AddWithValue("@VOUCHER_TYPE", "RV15" & CStr(RCV_TYPE))
                    cmd.Parameters.AddWithValue("@VOUCHER_DATE", CDate(TextBox18.Text))
                    cmd.Parameters.AddWithValue("@TOKEN_NO", TextBox59.Text)
                    cmd.Parameters.AddWithValue("@RCD_PARTY_CODE", DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@RCD_PARTY_NAME", DropDownList28.Text.Substring(DropDownList28.Text.IndexOf(",") + 1))
                    cmd.Parameters.AddWithValue("@RCD_PARTY_ADD", BILL_PARTY_ADD)
                    cmd.Parameters.AddWithValue("@GST_NO", BILL_PARTY_GST)
                    cmd.Parameters.AddWithValue("@STATE_NAME", BILL_PARTY_state)
                    cmd.Parameters.AddWithValue("@STATE_CODE", BILL_PARTY_state_code)
                    cmd.Parameters.AddWithValue("@NEG_BRANCH_DEPTT", "")
                    cmd.Parameters.AddWithValue("@VOUCHER_AMT", CDec(TextBox101.Text))
                    cmd.Parameters.AddWithValue("@MODE_OF_PAY", DropDownList23.Text)
                    cmd.Parameters.AddWithValue("@INST_NO", TextBox82.Text)
                    cmd.Parameters.AddWithValue("@TAX_PAY_REVER_CHR", DropDownList29.Text)
                    cmd.Parameters.AddWithValue("@SO_NO", DropDownList26.Text)
                    cmd.Parameters.AddWithValue("@ITEM_SLNO", DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@HSN_CODE", HSN_CODE)
                    cmd.Parameters.AddWithValue("@PROD_CODE", DropDownList22.Text.Substring(0, DropDownList22.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@PROD_DESC", DropDownList22.Text.Substring(DropDownList22.Text.IndexOf(",") + 1))
                    cmd.Parameters.AddWithValue("@TAX_VALUE", CDec(TextBox60.Text))
                    cmd.Parameters.AddWithValue("@CENTRAL_TAX_RATE", cgst)
                    cmd.Parameters.AddWithValue("@CENTRAL_TAX_AMT", CDec(TextBox96.Text))
                    cmd.Parameters.AddWithValue("@STATE_TAX_RATE", sgst)
                    cmd.Parameters.AddWithValue("@STATE_TAX_AMT", CDec(TextBox97.Text))
                    cmd.Parameters.AddWithValue("@INT_TAX_RATE", igst)
                    cmd.Parameters.AddWithValue("@INT_TAX_AMT", CDec(TextBox98.Text))
                    cmd.Parameters.AddWithValue("@CESS_TAX_RATE", cess)
                    cmd.Parameters.AddWithValue("@CESS_TAX_AMT", CDec(TextBox99.Text))
                    cmd.Parameters.AddWithValue("@TT_TAX_RATE", terminal_tax)
                    cmd.Parameters.AddWithValue("@TT_TAX_AMT", CDec(TextBox100.Text))
                    cmd.Parameters.AddWithValue("@TOTAL_AMT", CDec(TextBox101.Text))
                    cmd.Parameters.AddWithValue("@BAL_AMT", CDec(TextBox101.Text))
                    cmd.Parameters.AddWithValue("@CGST_BAL", CDec(TextBox96.Text))
                    cmd.Parameters.AddWithValue("@SGST_BAL", CDec(TextBox97.Text))
                    cmd.Parameters.AddWithValue("@IGST_BAL", CDec(TextBox98.Text))
                    cmd.Parameters.AddWithValue("@CESS_BAL", CDec(TextBox99.Text))
                    cmd.Parameters.AddWithValue("@VOUCHER_STATUS", "PENDING")
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                    cmd.Parameters.AddWithValue("@TCS_RATE", tcs_rate)
                    cmd.Parameters.AddWithValue("@TCS_AMT", CDec(TextBox102.Text))
                    cmd.ExecuteReader()
                    cmd.Dispose()

                    'INSERT VOUCHER FISCAL_YEAR

                    Query = "INSERT INTO VOUCHER (INV_NO,BANK_NAME,SUPL_NAME,TOKEN_NO,TOKEN_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,CVB_NO,CVB_DATE,CHEQUE_NO,CHEQUE_DATE)VALUES(@INV_NO,@BANK_NAME,@SUPL_NAME,@TOKEN_NO,@TOKEN_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@CVB_NO,@CVB_DATE,@CHEQUE_NO,@CHEQUE_DATE)"
                    Dim cmd1 As New SqlCommand(Query, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@TOKEN_NO", TextBox59.Text)
                    cmd1.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(CDate(TextBox18.Text), "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@SEC_NO", TextBox56.Text)
                    cmd1.Parameters.AddWithValue("@SEC_DATE", TextBox57.Text)
                    cmd1.Parameters.AddWithValue("@VOUCHER_TYPE", "RCD")
                    cmd1.Parameters.AddWithValue("@PAY_TYPE", DropDownList23.Text)
                    cmd1.Parameters.AddWithValue("@NET_AMT", CDec(TextBox101.Text))
                    cmd1.Parameters.AddWithValue("@PARTICULAR", TextBox58.Text)
                    cmd1.Parameters.AddWithValue("@SUPL_ID", BILL_PARTY_CODE)
                    cmd1.Parameters.AddWithValue("@SUPL_NAME", BILL_PARTY_NAME)
                    cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd1.Parameters.AddWithValue("@CVB_NO", TextBox93.Text)
                    cmd1.Parameters.AddWithValue("@CVB_DATE", Date.ParseExact(CDate(TextBox18.Text), "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@BANK_NAME", TextBox84.Text)
                    cmd1.Parameters.AddWithValue("@CHEQUE_NO", TextBox82.Text)
                    cmd1.Parameters.AddWithValue("@CHEQUE_DATE", Date.ParseExact(CDate(TextBox83.Text), "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                    cmd1.Parameters.AddWithValue("@INV_NO", "N/A")

                    cmd1.ExecuteReader()
                    cmd1.Dispose()

                End If

                myTrans.Commit()
                Label542.Visible = True
                Label542.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label542.Visible = True
                Label542.ForeColor = Drawing.Color.Red
                Label542.Text = "There was some Error, please contact EDP."
                TextBox94.Text = ""
                TextBox59.Text = ""
                TextBox93.Text = ""
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using




    End Sub
    Protected Sub save_ledger(PAYMENT_VOUCHER_NO As String, VOUCHER_NO As String, so_no As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
        Dim working_date As Date

        working_date = CDate(TextBox18.Text)
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
            dr_value = 0
            cr_value = 0
            If ac_term = "Dr" Then
                dr_value = price
                cr_value = 0.0
            ElseIf ac_term = "Cr" Then
                dr_value = 0.0
                cr_value = price
            End If

            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(AGING_FLAG,VOUCHER_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@AGING_FLAG,@VOUCHER_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn_trans, myTrans)
            cmd.Parameters.AddWithValue("@VOUCHER_NO", VOUCHER_NO)
            cmd.Parameters.AddWithValue("@PO_NO", so_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", PAYMENT_VOUCHER_NO)
            cmd.Parameters.AddWithValue("@SUPL_ID", dt_id)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.Parameters.AddWithValue("@AGING_FLAG", inv_no)
            cmd.ExecuteReader()
            cmd.Dispose()

        End If
    End Sub
    Protected Sub DropDownList26_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList26.SelectedIndexChanged
        If DropDownList26.SelectedValue = "Select" Then
            DropDownList26.Focus()
            Return
        End If

        'search order_details for sale type
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, DELIVERY_TERM, dater_code As New String("")
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select  ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.DELIVERY_TERM, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList26.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            po_type = dr.Item("PO_TYPE")
            SO_DATE = dr.Item("SO_DATE")
            freight_term = dr.Item("FREIGHT_TERM")
            ORDER_TO = dr.Item("ORDER_TO")
            DELIVERY_TERM = dr.Item("DELIVERY_TERM")
            dr.Close()
        End If
        conn.Close()
        Dim quary As String
        If order_type = "Sale Order" And po_type = "FINISH GOODS" Then
            quary = "select distinct (convert( varchar(10),SO_MAT_ORDER . ITEM_SLNO ) + ' , ' + CONVERT(varchar(250), F_ITEM.ITEM_NAME )) as p_details  from SO_MAT_ORDER join F_ITEM  on SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE   where SO_NO ='" & DropDownList26.SelectedValue & "' AND SO_MAT_ORDER .ITEM_STATUS ='PENDING' ORDER BY p_details"
        Else
            quary = "select distinct (convert( varchar(10),SO_MAT_ORDER . ITEM_SLNO ) + ' , ' + CONVERT(varchar(250), MATERIAL.MAT_NAME)) as p_details  from SO_MAT_ORDER join MATERIAL on SO_MAT_ORDER .ITEM_CODE =MATERIAL.MAT_CODE  where SO_NO ='" & DropDownList26.SelectedValue & "' AND SO_MAT_ORDER .ITEM_STATUS ='PENDING' ORDER BY p_details"
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        DropDownList27.Items.Clear()
        DropDownList27.DataSource = dt
        DropDownList27.DataValueField = "p_details"
        DropDownList27.DataBind()
        DropDownList27.Items.Insert(0, "Select")
        DropDownList27.SelectedValue = "Select"

        'DABTERS DETAILS
        conn.Open()
        mc1.CommandText = "select dater.d_code,dater.d_name from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & DropDownList26.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            DropDownList28.Text = dr.Item("d_code") & " , " & dr.Item("d_name")
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList27_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList27.SelectedIndexChanged
        If DropDownList27.SelectedValue = "Select" Then
            DropDownList27.Focus()
            Return
        ElseIf TextBox60.Text = "" Then
            TextBox60.Focus()
            Label455.Text = "Please Enter Amount "
            DropDownList27.SelectedValue = "Select"
            Return
        ElseIf IsNumeric(TextBox60.Text) = False Then
            TextBox60.Focus()
            Label455.Text = "Please Enter Numeric Value"
            DropDownList27.SelectedValue = "Select"
            Return
        ElseIf DropDownList26.SelectedValue = "Select" Then
            DropDownList26.Focus()
            DropDownList27.SelectedValue = "Select"
            Return
        End If

        'search gst rate
        Dim cgst, igst, sgst, cess, terminal_tax, TCS As New Decimal(0.0)
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select sum(ITEM_TCS) as ITEM_TCS,sum(ITEM_CGST) as ITEM_CGST ,sum(ITEM_SGST) as ITEM_SGST ,sum(ITEM_IGST) as ITEM_IGST ,sum(ITEM_CESS) as ITEM_CESS ,sum(ITEM_TERMINAL_TAX) as ITEM_TERMINAL_TAX  from SO_MAT_ORDER where SO_NO ='" & DropDownList26.SelectedValue & "' and ITEM_SLNO ='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            cgst = dr.Item("ITEM_CGST")
            sgst = dr.Item("ITEM_SGST")
            igst = dr.Item("ITEM_IGST")
            cess = dr.Item("ITEM_CESS")
            terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
            TCS = dr.Item("ITEM_TCS")
            dr.Close()
        End If
        conn.Close()
        'PRODUST DETAILS
        'DABTERS DETAILS
        'search order_details for sale type
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, DELIVERY_TERM, dater_code As New String("")
        conn.Open()
        mc1.CommandText = "select  ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.DELIVERY_TERM, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList26.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            po_type = dr.Item("PO_TYPE")
            SO_DATE = dr.Item("SO_DATE")
            freight_term = dr.Item("FREIGHT_TERM")
            ORDER_TO = dr.Item("ORDER_TO")
            DELIVERY_TERM = dr.Item("DELIVERY_TERM")
            dr.Close()
        End If
        conn.Close()
        Dim quary As String
        If order_type = "Sale Order" And po_type = "FINISH GOODS" Then
            quary = "SELECT ITEM_CODE + ' , ' + ITEM_NAME AS ITEM_DETAILS  FROM F_ITEM WHERE ITEM_CODE =(SELECT MAX(ITEM_CODE)  FROM SO_MAT_ORDER WHERE ITEM_SLNO ='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "' AND SO_NO ='" & DropDownList26.SelectedValue & "')"
        Else
            quary = "select convert( varchar(10),MATERIAL .MAT_CODE ) + ' , ' + CONVERT(varchar(250), MATERIAL .MAT_NAME) as ITEM_DETAILS  from SO_MAT_ORDER join MATERIAL on SO_MAT_ORDER .ITEM_CODE =MATERIAL.MAT_CODE  where SO_NO ='" & DropDownList26.SelectedValue & "' AND SO_MAT_ORDER .ITEM_SLNO  ='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "' "
        End If

        conn.Open()
        mc1.CommandText = quary
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            DropDownList22.Text = dr.Item("ITEM_DETAILS")
            dr.Close()
        End If
        conn.Close()
        'calculation
        TextBox95.Text = FormatNumber(CDec(TextBox60.Text), 2)
        TextBox100.Text = FormatNumber(((CDec(TextBox95.Text)) * terminal_tax) / 100, 2)

        TextBox96.Text = FormatNumber(((CDec(TextBox95.Text) + CDec(TextBox100.Text)) * cgst) / 100, 2)
        TextBox97.Text = FormatNumber(((CDec(TextBox95.Text) + CDec(TextBox100.Text)) * sgst) / 100, 2)
        TextBox98.Text = FormatNumber(((CDec(TextBox95.Text) + CDec(TextBox100.Text)) * igst) / 100, 2)
        TextBox99.Text = FormatNumber(((CDec(TextBox95.Text) + CDec(TextBox100.Text)) * cess) / 100, 2)
        TextBox102.Text = FormatNumber(((CDec(TextBox95.Text) + CDec(TextBox100.Text) + CDec(TextBox96.Text) + CDec(TextBox97.Text) + CDec(TextBox98.Text) + CDec(TextBox99.Text)) * TCS) / 100, 2)


        TextBox101.Text = FormatNumber(CDec(TextBox95.Text) + CDec(TextBox96.Text) + CDec(TextBox97.Text) + CDec(TextBox98.Text) + CDec(TextBox99.Text) + CDec(TextBox100.Text) + CDec(TextBox102.Text), 2)
    End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            Return
        ElseIf DropDownList9.SelectedValue = "Advance Receipt Voucher (For Supply Of Goods)" Then
            rcd_Panel.Visible = True
            Panel1.Visible = False
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT SO_MAT_ORDER .SO_NO  from SO_MAT_ORDER join ORDER_DETAILS on SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO where SO_MAT_ORDER .ITEM_STATUS ='PENDING' AND ORDER_DETAILS . PAYMENT_MODE ='ADVANCE' ORDER BY SO_MAT_ORDER .SO_NO ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList26.Items.Clear()
            DropDownList26.DataSource = dt
            DropDownList26.DataValueField = "SO_NO"
            DropDownList26.DataBind()
            DropDownList26.Items.Insert(0, "Select")
            DropDownList26.SelectedValue = "Select"
        ElseIf DropDownList9.SelectedValue = "Advance Receipt Voucher (For Services)" Then
            rcd_Panel.Visible = True
            Panel1.Visible = False
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT SO_MAT_ORDER .SO_NO  from SO_MAT_ORDER join ORDER_DETAILS on SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO where SO_MAT_ORDER .ITEM_STATUS ='PENDING' AND ORDER_DETAILS . PAYMENT_MODE ='ADVANCE' ORDER BY SO_MAT_ORDER .SO_NO ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList26.Items.Clear()
            DropDownList26.DataSource = dt
            DropDownList26.DataValueField = "SO_NO"
            DropDownList26.DataBind()
            DropDownList26.Items.Insert(0, "Select")
            DropDownList26.SelectedValue = "Select"
        ElseIf DropDownList9.SelectedValue = "Advance Payment Voucher for Goods" Then
            rcd_Panel.Visible = False
            Panel1.Visible = True
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT PO_ORD_MAT.PO_NO from PO_ORD_MAT  join ORDER_DETAILS on PO_ORD_MAT.PO_NO=ORDER_DETAILS .SO_NO where PO_ORD_MAT.MAT_STATUS  ='PENDING' AND ORDER_DETAILS.PAYMENT_MODE ='Advance Payment' ORDER BY PO_ORD_MAT .PO_NO ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList3.Items.Clear()
            DropDownList3.DataSource = dt
            DropDownList3.DataValueField = "PO_NO"
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "Select")
            DropDownList3.SelectedValue = "Select"
        ElseIf DropDownList9.SelectedValue = "Advance Payment Voucher For Services" Then

            rcd_Panel.Visible = False
            Panel1.Visible = True
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT WO_ORDER.PO_NO from WO_ORDER join ORDER_DETAILS on WO_ORDER.PO_NO=ORDER_DETAILS .SO_NO where WO_ORDER.W_STATUS  ='PENDING' AND ORDER_DETAILS.PAYMENT_MODE ='Advance Payment' ORDER BY WO_ORDER.PO_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList3.Items.Clear()
            DropDownList3.DataSource = dt
            DropDownList3.DataValueField = "PO_NO"
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "Select")
            DropDownList3.SelectedValue = "Select"

        ElseIf DropDownList9.SelectedValue = "Refund Advance Voucher" Then
        ElseIf DropDownList9.SelectedValue = "Advance Payment Voucher Refund" Then
        End If
    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Return
        End If

        'search order_details for sale type
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, DELIVERY_TERM, dater_code As New String("")
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.DELIVERY_TERM, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList3.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            po_type = dr.Item("PO_TYPE")
            SO_DATE = dr.Item("SO_DATE")
            freight_term = dr.Item("FREIGHT_TERM")
            ORDER_TO = dr.Item("ORDER_TO")
            DELIVERY_TERM = dr.Item("DELIVERY_TERM")
            dr.Close()
        End If
        conn.Close()
        Dim quary As String = ""
        If order_type = "Purchase Order" Then

            quary = "select distinct (convert( varchar(10),PO_ORD_MAT  . MAT_SLNO  ) + ' , ' + CONVERT(varchar(250), MATERIAL .MAT_NAME)) as p_details from PO_ORD_MAT join MATERIAL on PO_ORD_MAT .MAT_CODE =MATERIAL.MAT_CODE where PO_ORD_MAT .PO_NO  ='" & DropDownList3.SelectedValue & "' AND PO_ORD_MAT .MAT_STATUS  ='PENDING' ORDER BY p_details"
        ElseIf order_type = "Work Order" Then
            quary = "select distinct (convert( varchar(10),WO_ORDER.W_SLNO  ) + ' , ' + CONVERT(varchar(250), WO_ORDER.W_NAME)) as p_details from WO_ORDER where WO_ORDER .PO_NO  ='" & DropDownList3.SelectedValue & "' AND WO_ORDER.W_STATUS  ='PENDING' ORDER BY p_details"
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        DropDownList4.Items.Clear()
        DropDownList4.DataSource = dt
        DropDownList4.DataValueField = "p_details"
        DropDownList4.DataBind()
        DropDownList4.Items.Insert(0, "Select")
        DropDownList4.SelectedValue = "Select"

        'DABTERS DETAILS
        conn.Open()
        mc1.CommandText = "select SUPL.SUPL_ID,SUPL.SUPL_NAME from SUPL join order_details on order_details.PARTY_CODE=SUPL.SUPL_ID  where order_details.so_no='" & DropDownList3.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox12.Text = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        ElseIf TextBox8.Text = "" Then
            TextBox8.Focus()
            Label6.Text = "Please Enter Amount "
            DropDownList4.SelectedValue = "Select"
            Return
        ElseIf IsNumeric(TextBox8.Text) = False Then
            TextBox8.Focus()
            Label6.Text = "Please Enter Numeric Value"
            DropDownList4.SelectedValue = "Select"
            Return
        ElseIf DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            DropDownList4.SelectedValue = "Select"
            Return
        ElseIf TextBox22.Text = "" Or IsNumeric(TextBox22.Text) = False Then
            TextBox22.Focus()
            Label6.Text = "Please Enter Valid TDS Amount "
            DropDownList4.SelectedValue = "Select"
            Return
        End If

        'search order_details for sale type
        Dim mc1 As New SqlCommand
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, DELIVERY_TERM, dater_code As New String("")
        conn.Open()
        mc1.CommandText = "select  ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.DELIVERY_TERM, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList3.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            po_type = dr.Item("PO_TYPE")
            SO_DATE = dr.Item("SO_DATE")
            freight_term = dr.Item("FREIGHT_TERM")
            ORDER_TO = dr.Item("ORDER_TO")
            DELIVERY_TERM = dr.Item("DELIVERY_TERM")
            dr.Close()
        End If
        conn.Close()

        'search gst rate
        Dim cgst, igst, sgst, cess As New Decimal(0.0)
        conn.Open()

        If order_type = "Purchase Order" Then
            mc1.CommandText = "select sum(CGST) as CGST ,sum(SGST) as SGST ,sum(IGST) as IGST ,sum(CESS) as CESS  from PO_ORD_MAT  where PO_NO ='" & DropDownList3.SelectedValue & "' and MAT_SLNO  ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"
        ElseIf order_type = "Work Order" Then
            mc1.CommandText = "select sum(CGST) as CGST ,sum(SGST) as SGST ,sum(IGST) as IGST ,sum(CESS) as CESS  from WO_ORDER where PO_NO ='" & DropDownList3.SelectedValue & "' and W_SLNO  ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"
        End If


        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            cgst = dr.Item("CGST")
            sgst = dr.Item("SGST")
            igst = dr.Item("IGST")
            cess = dr.Item("CESS")
            dr.Close()
        End If
        conn.Close()



        Dim quary As New String("")
        If order_type = "Purchase Order" Then
            quary = "select convert( varchar(10),PO_ORD_MAT .MAT_CODE   ) + ' , ' + CONVERT(varchar(250), MATERIAL .MAT_NAME) as p_details  from PO_ORD_MAT  join MATERIAL on PO_ORD_MAT .MAT_CODE  =MATERIAL.MAT_CODE  where PO_ORD_MAT .PO_NO  ='" & DropDownList3.SelectedValue & "' AND PO_ORD_MAT .MAT_SLNO   ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"
        ElseIf order_type = "Work Order" Then
            quary = "select convert(varchar(10), WO_ORDER.W_SLNO) + ' , ' + CONVERT(varchar(250), WO_ORDER.W_NAME) as p_details from WO_ORDER where PO_NO  ='" & DropDownList3.SelectedValue & "' AND W_SLNO ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"
        End If


        conn.Open()
        mc1.CommandText = quary
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox11.Text = dr.Item("p_details")
            dr.Close()
        End If
        conn.Close()
        'calculation
        TextBox13.Text = FormatNumber(CDec(TextBox8.Text), 2)
        TextBox14.Text = FormatNumber(CInt((CDec(TextBox13.Text) * cgst) / 100), 2)
        TextBox15.Text = FormatNumber(CInt((CDec(TextBox13.Text) * sgst) / 100), 2)
        TextBox16.Text = FormatNumber(CInt((CDec(TextBox13.Text) * igst) / 100), 2)
        TextBox17.Text = FormatNumber(CInt((CDec(TextBox13.Text) * cess) / 100), 2)
        TextBox20.Text = FormatNumber(CDec(TextBox13.Text) + CDec(TextBox14.Text) + CDec(TextBox15.Text) + CDec(TextBox16.Text) + CDec(TextBox17.Text) - CDec(TextBox22.Text), 2)
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                If TextBox18.Text = "" Then
                    TextBox18.Text = ""
                    TextBox18.Focus()
                    Return
                ElseIf IsDate(TextBox18.Text) = False Then
                    TextBox18.Text = ""
                    TextBox18.Focus()
                    Return
                ElseIf DropDownList1.SelectedValue = "Select" Then
                    DropDownList1.Focus()
                    Label6.Text = "Please Select Inst Type"
                    Return
                ElseIf TextBox4.Text = "" Then
                    TextBox4.Focus()
                    Label6.Text = "Please Fill Sec. No"
                    Return
                ElseIf TextBox6.Text = "" Then
                    TextBox6.Focus()
                    Label6.Text = "Please Fill Inst No"
                    Return
                ElseIf TextBox7.Text = "" Or IsDate(TextBox7.Text) = False Then
                    TextBox7.Focus()
                    Label6.Text = "Please Select Inst Date"
                    Return
                ElseIf TextBox5.Text = "" Or IsDate(TextBox5.Text) = False Then
                    TextBox5.Focus()
                    Label6.Text = "Please Select Sec. Date"
                    Return
                ElseIf TextBox21.Text = "" Then
                    TextBox21.Focus()
                    Label6.Text = "Please Fill IOC No"
                    Return
                ElseIf TextBox9.Text = "" Then
                    TextBox9.Focus()
                    Label6.Text = "Please Fill Drawn On"
                    Return
                ElseIf TextBox10.Text = "" Then
                    TextBox10.Focus()
                    Label6.Text = "Please Fill Narration"
                    Return
                ElseIf TextBox8.Text = "" Or IsNumeric(TextBox8.Text) = False Then
                    TextBox8.Focus()
                    Label6.Text = "Please Enter Amount Or Numeric Value"
                    Return
                ElseIf DropDownList3.SelectedValue = "Select" Then
                    DropDownList3.Focus()
                    Label6.Text = "Please Select Sale Order No"
                    Return
                ElseIf DropDownList4.SelectedValue = "Select" Then
                    DropDownList4.Focus()
                    Label6.Text = "Please Select Item Details"
                    Return
                End If


                '''''''''''''''''''''''''''''''''
                ''Checking Adv payment voucher entry date and Freeze date
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

                If (CDate(TextBox18.Text) <= CDate(Block_DATE)) Then
                    Label29.Visible = True
                    Label29.Text = "Advance payment voucher entry before " & Block_DATE & " has been freezed."

                Else
                    Dim STR1 As String = ""
                    If CDate(TextBox18.Text).Month > 3 Then
                        STR1 = CDate(TextBox18.Text).Year
                        STR1 = STR1.Trim.Substring(2)
                        STR1 = STR1 & (STR1 + 1)
                    ElseIf CDate(TextBox18.Text).Month <= 3 Then
                        STR1 = CDate(TextBox18.Text).Year
                        STR1 = STR1.Trim.Substring(2)
                        STR1 = (STR1 - 1) & STR1
                    End If

                    'search order_details for sale type
                    Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, DELIVERY_TERM, dater_code As New String("")
                    conn.Open()
                    Dim mc1 As New SqlCommand
                    mc1.CommandText = "select ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.DELIVERY_TERM, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList3.Text & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        order_type = dr.Item("ORDER_TYPE")
                        po_type = dr.Item("PO_TYPE")
                        SO_DATE = dr.Item("SO_DATE")
                        freight_term = dr.Item("FREIGHT_TERM")
                        ORDER_TO = dr.Item("ORDER_TO")
                        DELIVERY_TERM = dr.Item("DELIVERY_TERM")
                        dr.Close()
                    End If
                    conn.Close()
                    count = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select DISTINCT TOKEN_NO FROM VOUCHER WHERE FISCAL_YEAR=" & STR1, conn)
                    count = da.Fill(dt)
                    conn.Close()
                    TextBox2.Text = STR1 & count + 1

                    ''SEARCH RCD VOUCHER NO
                    Dim RCV_TYPE As Integer = 0
                    If ORDER_TO = "I.P.T." Then
                        RCV_TYPE = 4
                    ElseIf ORDER_TO = "Other" Then
                        RCV_TYPE = 1
                    End If



                    conn.Open()
                    Dim inv_no As String = ""
                    Dim mc_c As New SqlCommand
                    mc_c.CommandText = "SELECT (CASE WHEN MAX(VOUCHER_NO) IS NULL THEN 0 ELSE MAX(VOUCHER_NO) END) as VOUCHER_NO  FROM SALE_RCD_VOUCHAR  WHERE VOUCHER_TYPE LIKE 'PV%' AND FISCAL_YEAR =" & STR1
                    mc_c.Connection = conn
                    dr = mc_c.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        inv_no = dr.Item("VOUCHER_NO")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()


                    If CInt(inv_no) = 0 Then
                        TextBox1.Text = "0000001"

                    Else
                        str = CInt(inv_no) + 1
                        If str.Length = 1 Then
                            str = "000000" & CInt(inv_no) + 1
                        ElseIf str.Length = 2 Then
                            str = "00000" & CInt(inv_no) + 1
                        ElseIf str.Length = 3 Then
                            str = "0000" & CInt(inv_no) + 1
                        ElseIf str.Length = 4 Then
                            str = "000" & CInt(inv_no) + 1
                        ElseIf str.Length = 5 Then
                            str = "00" & CInt(inv_no) + 1
                        ElseIf str.Length = 6 Then
                            str = "0" & CInt(inv_no) + 1
                        ElseIf str.Length = 7 Then
                            str = CInt(inv_no) + 1
                        End If
                        TextBox1.Text = str
                    End If



                    Dim HSN_CODE As String = ""
                    Dim quary As New String("")
                    If order_type = "Purchase Order" Then
                        quary = "select CHPTR_HEAD from MATERIAL where MAT_CODE =(SELECT DISTINCT MAT_CODE FROM PO_ORD_MAT WHERE PO_NO ='" & DropDownList3.SelectedValue & "' AND MAT_SLNO  ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "')"
                    ElseIf order_type = "Work Order" Then
                        quary = "select '9984' as CHPTR_HEAD"
                    End If



                    conn.Open()
                    mc1.CommandText = quary
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        HSN_CODE = dr.Item("CHPTR_HEAD")
                        dr.Close()
                    End If
                    conn.Close()

                    'search account head
                    'search order_details for sale type
                    Dim adv_head, cgst_head, igst_head, sgst_head, cess_head, bank_head As New String("")
                    conn.Open()
                    mycommand.CommandText = "select ORDER_DETAILS .SO_NO, work_group.bank,work_group .adv_pay from ORDER_DETAILS JOIN work_group on ORDER_DETAILS.ORDER_TYPE =work_group.work_name and  ORDER_DETAILS.PO_TYPE  =work_group.work_type and ORDER_DETAILS.ORDER_TO  =work_group.d_type WHERE ORDER_DETAILS.SO_NO='" & DropDownList3.Text & "'"
                    mycommand.Connection = conn
                    dr = mycommand.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        adv_head = dr.Item("adv_pay")
                        bank_head = dr.Item("bank")
                        dr.Close()
                    End If
                    conn.Close()

                    'save ledger
                    save_ledger("PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox2.Text, DropDownList3.Text, "PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1), adv_head, "Dr", FormatNumber(CDec(TextBox13.Text) + CDec(TextBox14.Text) + CDec(TextBox15.Text) + CDec(TextBox16.Text) + CDec(TextBox17.Text), 2), "Advance")
                    save_ledger("PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox2.Text, DropDownList3.Text, "PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1), adv_head, "Cr", CDec(TextBox20.Text), "Advance")
                    save_ledger("PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox2.Text, DropDownList3.Text, "PAYMENT", TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1), adv_head, "Dr", CDec(TextBox20.Text), "ADV PAY")
                    save_ledger("PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox2.Text, DropDownList3.Text, "BANK", TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1), bank_head, "Cr", CDec(TextBox20.Text), "BANK")
                    save_ledger("PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox2.Text, DropDownList3.Text, "PV15" & CStr(RCV_TYPE) & TextBox1.Text, TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1), "51710", "Cr", CDec(TextBox22.Text), "TDS_194(O)")


                    Dim BILL_PARTY_ADD, SHIP_PARTY_ADD, BILL_PARTY_state_code, BILL_PARTY_state, SHIP_PARTY_state_code, SHIP_PARTY_state, BILL_PARTY_GST, BILL_PARTY_CODE, SHIP_PARTY_GST, SHIP_PARTY_CODE As New String("")
                    'BILL TO PARTY ADDRESS SEARCH
                    conn.Open()
                    mc1.CommandText = "select SUPL_NAME  + ' , ' + SUPL_AT + ' , ' + SUPL_PO + ' , ' + SUPL_DIST  as party_details, SUPL_STATE ,SUPL_state_code ,SUPL_GST_NO,SUPL_ID from SUPL where SUPL_ID =(select PARTY_CODE from ORDER_DETAILS where SO_NO ='" & DropDownList3.Text & "')"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        BILL_PARTY_CODE = dr.Item("SUPL_ID")
                        BILL_PARTY_ADD = dr.Item("party_details")
                        BILL_PARTY_GST = dr.Item("SUPL_GST_NO")
                        BILL_PARTY_state = dr.Item("SUPL_STATE")
                        BILL_PARTY_state_code = dr.Item("SUPL_state_code")
                        dr.Close()
                    End If
                    conn.Close()



                    'search gst rate
                    Dim cgst, igst, sgst, cess As New Decimal(0.0)
                    conn.Open()

                    If order_type = "Purchase Order" Then
                        mc1.CommandText = "select sum(CGST) as CGST ,sum(SGST) as SGST ,sum(IGST) as IGST ,sum(CESS) as CESS  from PO_ORD_MAT  where PO_NO ='" & DropDownList3.SelectedValue & "' and MAT_SLNO  ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"
                    ElseIf order_type = "Work Order" Then
                        mc1.CommandText = "select sum(CGST) as CGST ,sum(SGST) as SGST ,sum(IGST) as IGST ,sum(CESS) as CESS  from WO_ORDER where PO_NO ='" & DropDownList3.SelectedValue & "' and W_SLNO  ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"
                    End If


                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        cgst = dr.Item("CGST")
                        sgst = dr.Item("SGST")
                        igst = dr.Item("IGST")
                        cess = dr.Item("CESS")
                        dr.Close()
                    End If
                    conn.Close()


                    'save SALE_RCD_VOUCHAR

                    Dim cmd As New SqlCommand
                    Dim Query As String = "Insert Into SALE_RCD_VOUCHAR(EMP_ID,FISCAL_YEAR,VOUCHER_NO ,VOUCHER_TYPE ,VOUCHER_DATE ,TOKEN_NO,RCD_PARTY_CODE ,RCD_PARTY_NAME ,RCD_PARTY_ADD ,GST_NO ,STATE_NAME,STATE_CODE,NEG_BRANCH_DEPTT,VOUCHER_AMT ,MODE_OF_PAY ,INST_NO ,TAX_PAY_REVER_CHR ,SO_NO,ITEM_SLNO ,HSN_CODE ,PROD_CODE,PROD_DESC,TAX_VALUE,CENTRAL_TAX_RATE,CENTRAL_TAX_AMT,STATE_TAX_RATE ,STATE_TAX_AMT,INT_TAX_RATE,INT_TAX_AMT ,CESS_TAX_RATE,CESS_TAX_AMT,TT_TAX_RATE,TT_TAX_AMT ,TOTAL_AMT,BAL_AMT,CGST_BAL,SGST_BAL,IGST_BAL,CESS_BAL,VOUCHER_STATUS)VALUES(@EMP_ID,@FISCAL_YEAR,@VOUCHER_NO ,@VOUCHER_TYPE ,@VOUCHER_DATE ,@TOKEN_NO,@RCD_PARTY_CODE ,@RCD_PARTY_NAME ,@RCD_PARTY_ADD ,@GST_NO ,@STATE_NAME,@STATE_CODE,@NEG_BRANCH_DEPTT,@VOUCHER_AMT ,@MODE_OF_PAY ,@INST_NO ,@TAX_PAY_REVER_CHR ,@SO_NO,@ITEM_SLNO ,@HSN_CODE ,@PROD_CODE,@PROD_DESC,@TAX_VALUE,@CENTRAL_TAX_RATE,@CENTRAL_TAX_AMT,@STATE_TAX_RATE ,@STATE_TAX_AMT,@INT_TAX_RATE,@INT_TAX_AMT ,@CESS_TAX_RATE,@CESS_TAX_AMT,@TT_TAX_RATE,@TT_TAX_AMT ,@TOTAL_AMT,@BAL_AMT,@CGST_BAL,@SGST_BAL,@IGST_BAL,@CESS_BAL,@VOUCHER_STATUS)"
                    cmd = New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox1.Text)
                    cmd.Parameters.AddWithValue("@VOUCHER_TYPE", "PV15" & CStr(RCV_TYPE))
                    cmd.Parameters.AddWithValue("@VOUCHER_DATE", CDate(TextBox18.Text))
                    cmd.Parameters.AddWithValue("@TOKEN_NO", TextBox2.Text)
                    cmd.Parameters.AddWithValue("@RCD_PARTY_CODE", TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@RCD_PARTY_NAME", TextBox12.Text.Substring(TextBox12.Text.IndexOf(",") + 1))
                    cmd.Parameters.AddWithValue("@RCD_PARTY_ADD", BILL_PARTY_ADD)
                    cmd.Parameters.AddWithValue("@GST_NO", BILL_PARTY_GST)
                    cmd.Parameters.AddWithValue("@STATE_NAME", BILL_PARTY_state)
                    cmd.Parameters.AddWithValue("@STATE_CODE", BILL_PARTY_state_code)
                    cmd.Parameters.AddWithValue("@NEG_BRANCH_DEPTT", "")
                    cmd.Parameters.AddWithValue("@VOUCHER_AMT", CDec(TextBox20.Text))
                    cmd.Parameters.AddWithValue("@MODE_OF_PAY", DropDownList1.Text)
                    cmd.Parameters.AddWithValue("@INST_NO", TextBox6.Text)
                    cmd.Parameters.AddWithValue("@TAX_PAY_REVER_CHR", DropDownList2.Text)
                    cmd.Parameters.AddWithValue("@SO_NO", DropDownList3.Text)
                    cmd.Parameters.AddWithValue("@ITEM_SLNO", DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@HSN_CODE", HSN_CODE)
                    cmd.Parameters.AddWithValue("@PROD_CODE", TextBox11.Text.Substring(0, TextBox11.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@PROD_DESC", TextBox11.Text.Substring(TextBox11.Text.IndexOf(",") + 1))
                    cmd.Parameters.AddWithValue("@TAX_VALUE", CDec(TextBox8.Text))
                    cmd.Parameters.AddWithValue("@CENTRAL_TAX_RATE", cgst)
                    cmd.Parameters.AddWithValue("@CENTRAL_TAX_AMT", CDec(TextBox14.Text))
                    cmd.Parameters.AddWithValue("@STATE_TAX_RATE", sgst)
                    cmd.Parameters.AddWithValue("@STATE_TAX_AMT", CDec(TextBox15.Text))
                    cmd.Parameters.AddWithValue("@INT_TAX_RATE", igst)
                    cmd.Parameters.AddWithValue("@INT_TAX_AMT", CDec(TextBox16.Text))
                    cmd.Parameters.AddWithValue("@CESS_TAX_RATE", cess)
                    cmd.Parameters.AddWithValue("@CESS_TAX_AMT", CDec(TextBox17.Text))
                    cmd.Parameters.AddWithValue("@TT_TAX_RATE", 0)
                    cmd.Parameters.AddWithValue("@TT_TAX_AMT", 0)
                    cmd.Parameters.AddWithValue("@TOTAL_AMT", CDec(TextBox20.Text))
                    cmd.Parameters.AddWithValue("@BAL_AMT", CDec(TextBox20.Text))
                    cmd.Parameters.AddWithValue("@CGST_BAL", CDec(TextBox14.Text))
                    cmd.Parameters.AddWithValue("@SGST_BAL", CDec(TextBox15.Text))
                    cmd.Parameters.AddWithValue("@IGST_BAL", CDec(TextBox16.Text))
                    cmd.Parameters.AddWithValue("@CESS_BAL", CDec(TextBox17.Text))
                    cmd.Parameters.AddWithValue("@VOUCHER_STATUS", "PENDING")
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                    cmd.ExecuteReader()
                    cmd.Dispose()

                    'INSERT VOUCHER FISCAL_YEAR

                    Dim BPV_NO As String = ""
                    Dim str_temp As String = ""
                    Dim count_cbv_no As Decimal
                    count_cbv_no = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("SELECT CVB_NO FROM VOUCHER WITH(NOLOCK) WHERE VOUCHER_TYPE ='B.P.V' AND CVB_NO <>'' AND FISCAL_YEAR=" & STR1, conn)
                    count_cbv_no = da.Fill(dt)
                    conn.Close()
                    If CInt(count_cbv_no) = 0 Then
                        BPV_NO = "BPV" + STR1 + "00001"
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
                        BPV_NO = "BPV" + STR1 + str_temp
                    End If
                    '''''''''''''''''''''
                    TextBox3.Text = BPV_NO
                    TextBox3.ReadOnly = True


                    Query = "INSERT INTO VOUCHER (PO_NO,BANK_NAME,TOKEN_NO,TOKEN_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,CVB_NO,CVB_DATE,CHEQUE_NO,CHEQUE_DATE,SUPL_NAME)VALUES(@PO_NO,@BANK_NAME,@TOKEN_NO,@TOKEN_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@CVB_NO,@CVB_DATE,@CHEQUE_NO,@CHEQUE_DATE,@SUPL_NAME)"
                    Dim cmd1 As New SqlCommand(Query, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@TOKEN_NO", TextBox2.Text)
                    cmd1.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(CDate(TextBox18.Text), "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@SEC_NO", TextBox4.Text)
                    cmd1.Parameters.AddWithValue("@SEC_DATE", TextBox5.Text)
                    cmd1.Parameters.AddWithValue("@VOUCHER_TYPE", "B.P.V")
                    cmd1.Parameters.AddWithValue("@PAY_TYPE", DropDownList1.Text)
                    cmd1.Parameters.AddWithValue("@NET_AMT", CDec(TextBox20.Text))
                    cmd1.Parameters.AddWithValue("@PARTICULAR", TextBox10.Text)
                    cmd1.Parameters.AddWithValue("@SUPL_ID", BILL_PARTY_CODE)
                    cmd1.Parameters.AddWithValue("@SUPL_NAME", TextBox12.Text.Substring(TextBox12.Text.IndexOf(",") + 1))
                    cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd1.Parameters.AddWithValue("@CVB_NO", BPV_NO)
                    cmd1.Parameters.AddWithValue("@CVB_DATE", Date.ParseExact(CDate(TextBox18.Text), "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@BANK_NAME", TextBox9.Text)
                    cmd1.Parameters.AddWithValue("@CHEQUE_NO", TextBox6.Text)
                    cmd1.Parameters.AddWithValue("@CHEQUE_DATE", Date.ParseExact(CDate(TextBox7.Text), "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                    cmd1.Parameters.AddWithValue("@PO_NO", DropDownList3.Text)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()

                End If

                myTrans.Commit()

                Label6.Visible = True
                Label6.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label6.Visible = True
                Label6.ForeColor = Drawing.Color.Red
                Label6.Text = "There was some Error, please contact EDP."
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub


End Class