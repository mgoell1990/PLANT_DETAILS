Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net

Public Class FOREIGN_MAT_CORRECTION
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt, dTable As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim working_date As Date = Today.Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then

        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Label33.Text = "Please select effective date."
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Label33.Text = "Invalid date format"
            Return
        ElseIf TextBox10.Text = "" Then
            TextBox10.Focus()
            Label33.Text = "Please select TO Date."
            Return
        ElseIf IsDate(TextBox10.Text) = False Then
            TextBox10.Text = ""
            TextBox10.Focus()
            Label33.Text = "Invalid date format"
            Return
        ElseIf TextBox11.Text = "" Then
            TextBox11.Focus()
            Label33.Text = "Please select FROM Date."
            Return
        ElseIf IsDate(TextBox11.Text) = False Then
            TextBox11.Text = ""
            TextBox11.Focus()
            Label33.Text = "Invalid date format"
            Return
        Else
            Label33.Text = ""
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox10.Text)
        to_date = CDate(TextBox11.Text)

        Dim STR1 As String = ""
        If from_date.Month > 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf from_date.Month <= 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        conn.Open()
        dt.Clear()

        Dim total_row As Integer



        'da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE BE_NO <> 'N/A' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' ORDER BY GARN_NO", conn)
        If (TextBox1.Text = "") Then
            'da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE BE_NO <> 'N/A' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND garn_no='RGARN1819001828' ORDER BY GARN_NO", conn)
            'da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE BE_NO <> 'N/A' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' ORDER BY GARN_NO", conn)
            da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE GARN_NO IN (SELECT Garn_number FROM foren_temp_correction)", conn)
        Else
            da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE BE_NO <> 'N/A' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND BE_NO='" & TextBox1.Text & "' ORDER BY GARN_NO", conn)
        End If

        'da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE BE_NO <> 'N/A' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and GARN_NO <> 'RGARN1819001098' ORDER BY GARN_NO", conn)
        da.Fill(dt)
        total_row = dt.Rows.Count
        conn.Close()
        GridView6.DataSource = dt
        GridView6.DataBind()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                Dim I As Integer = 0
                For I = 0 To GridView6.Rows.Count - 1

                    Dim CHA_CRR As String
                    CHA_CRR = "CHA" & GridView6.Rows(I).Cells(4).Text
                    conn.Open()
                    mycommand = New SqlCommand("DELETE from ledger where (GARN_NO_MB_NO='" & GridView6.Rows(I).Cells(4).Text & "') and Journal_ID is not null", conn)
                    mycommand.ExecuteNonQuery()
                    conn.Close()

                    Dim mc1 As New SqlCommand
                    Dim po_no, MAT_SLNO, cha_wo, cha_wo_sl, CHA_SUPL_ID, SUPL_ID, GARN_NO, EFFECTIVE_DATE As String
                    po_no = GridView6.Rows(I).Cells(1).Text
                    MAT_SLNO = GridView6.Rows(I).Cells(3).Text
                    GARN_NO = GridView6.Rows(I).Cells(4).Text
                    EFFECTIVE_DATE = GridView6.Rows(I).Cells(6).Text
                    cha_wo = ""
                    cha_wo_sl = ""
                    SUPL_ID = ""
                    CHA_SUPL_ID = ""
                    conn.Open()
                    mc1.CommandText = "Select PO_RCD_MAT.SUPL_ID,PO_RCD_MAT .TRANS_WO_NO ,PO_RCD_MAT .TRANS_SLNO ,BE_DETAILS .CHA_ORDER ,BE_DETAILS .CHA_SLNO  from PO_RCD_MAT join BE_DETAILS On PO_RCD_MAT .BE_NO =BE_DETAILS .BE_NO And PO_RCD_MAT .PO_NO =BE_DETAILS .PO_NO And PO_RCD_MAT .MAT_SLNO =BE_DETAILS .MAT_SLNO  where PO_RCD_MAT .CRR_NO  ='" & GridView6.Rows(I).Cells(0).Text & "' AND PO_RCD_MAT.MAT_SLNO=" & GridView6.Rows(I).Cells(3).Text
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        cha_wo = dr.Item("CHA_ORDER")
                        cha_wo_sl = dr.Item("CHA_SLNO")
                        SUPL_ID = dr.Item("SUPL_ID")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()

                    conn.Open()
                    mycommand.CommandText = "SELECT PARTY_CODE FROM ORDER_DETAILS WHERE SO_NO ='" & cha_wo & "'"
                    mycommand.Connection = conn
                    dr = mycommand.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        CHA_SUPL_ID = dr.Item("PARTY_CODE")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()

                    Dim PARTY_QTY As Decimal = 0.0
                    Dim w_tolerance As Decimal = 0.0
                    Dim w_qty, w_complite, PRICE, final_price, ENTRY_TAX, w_unit_price, W_discount, DISCOUNT_VALUE, CUSTOM_DUTY, IGST, BE_QTY, STATUTORY_CHARGE, INSURANCE, TRANS_CHARGE As New Decimal(0)

                    Dim CHA_VALUE, CHA_RCD, cenvat_value, mat_value, PURCHASE_VALUE, MAT_CUSTOM_DUTY, loss_on_ed_value, wt_var_value, mat_qty As New Decimal(0)
                    cenvat_value = 0
                    mat_value = 0
                    PURCHASE_VALUE = 0
                    loss_on_ed_value = 0
                    wt_var_value = 0

                    CHA_VALUE = 0.0
                    CHA_RCD = 0
                    ENTRY_TAX = 0

                    conn.Open()

                    'mc1.CommandText = "select MAT_CHALAN_QTY from PO_RCD_MAT where garn_no='" & GridView6.Rows(I).Cells(4).Text & "'"
                    mc1.CommandText = "select Chalan_Qty from foren_temp_correction where Garn_number='" & GridView6.Rows(I).Cells(4).Text & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        'mat_qty = dr.Item("MAT_CHALAN_QTY")
                        mat_qty = dr.Item("Chalan_Qty")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()

                    'mat_qty = CDec(GridView6.Rows(I).Cells(10).Text) - (CDec(GridView6.Rows(I).Cells(11).Text) + CDec(GridView6.Rows(I).Cells(13).Text))
                    Dim UNIT_PRICE, UNIT_CENVAT As Decimal
                    conn.Open()
                    mc1.CommandText = "SELECT * FROM BE_DETAILS JOIN PO_ORD_MAT ON BE_DETAILS .PO_NO =PO_ORD_MAT .PO_NO AND BE_DETAILS .MAT_SLNO =PO_ORD_MAT .MAT_SLNO WHERE BE_DETAILS .BE_NO ='" & GridView6.Rows(I).Cells(7).Text & "' AND BE_DETAILS .PO_NO ='" & GridView6.Rows(I).Cells(1).Text & "' AND BE_DETAILS .MAT_SLNO =" & GridView6.Rows(I).Cells(3).Text
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        UNIT_PRICE = dr.Item("UNIT_PRICE")
                        UNIT_CENVAT = dr.Item("UNIT_CENVAT")
                        CUSTOM_DUTY = dr.Item("TOTAL_CUSTOM_DUTY")
                        IGST = dr.Item("IGST")
                        BE_QTY = dr.Item("BE_QTY")
                        STATUTORY_CHARGE = dr.Item("SAT_CHARGE")
                        INSURANCE = dr.Item("INSURANCE")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                    'unit_value = FormatNumber(UNIT_PRICE, 2)
                    mat_value = FormatNumber(((UNIT_PRICE) * CDec(mat_qty)), 2)
                    cenvat_value = (UNIT_CENVAT * CDec(mat_qty))


                    ''GETTING PURCHASE AND SIT HEAD
                    Dim PURCHASE As String = ""
                    conn.Open()
                    Dim MCc As New SqlCommand
                    MCc.CommandText = "select AC_PUR from MATERIAL where MAT_CODE = '" & GridView6.Rows(I).Cells(8).Text & "'"
                    MCc.Connection = conn
                    dr = MCc.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        PURCHASE = dr.Item("AC_PUR")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If

                    ''ledger posting material transit
                    Dim SIT_HEAD, cenvat_head, PROV_CHA As New String("")
                    conn.Open()
                    mycommand.CommandText = "select * from work_group where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & po_no & "')"
                    mycommand.Connection = conn
                    dr = mycommand.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        SIT_HEAD = dr.Item("PROV_HEAD")
                        cenvat_head = dr.Item("igst")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()

                    conn.Open()
                    mycommand.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & cha_wo & "') and work_type=(select distinct wo_type from wo_order where po_no='" & cha_wo & "' and w_slno='" & cha_wo_sl & "')"
                    mycommand.Connection = conn
                    dr = mycommand.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        PROV_CHA = dr.Item("PROV_HEAD")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If


                    Dim MAT_STATUTORY_CHARGE, MAT_INSURANCE_VALUE, MAT_IGST_VALUE As New Decimal(0)

                    MAT_CUSTOM_DUTY = FormatNumber((CUSTOM_DUTY / BE_QTY) * CDec(mat_qty), 2)
                    MAT_STATUTORY_CHARGE = FormatNumber((STATUTORY_CHARGE / BE_QTY) * CDec(mat_qty), 2)


                    MAT_IGST_VALUE = FormatNumber((IGST / BE_QTY) * CDec(mat_qty), 2)

                    MAT_INSURANCE_VALUE = FormatNumber((INSURANCE / BE_QTY) * CDec(mat_qty), 2)

                    PURCHASE_VALUE = mat_value + MAT_CUSTOM_DUTY + MAT_STATUTORY_CHARGE + MAT_INSURANCE_VALUE

                    ''SAVE LEDGER PURCHASE
                    LEDGER_SAVE_PUR(po_no, MAT_SLNO, SUPL_ID, GARN_NO, PURCHASE, "Dr", FormatNumber(PURCHASE_VALUE, 2), "PUR", 1, "", EFFECTIVE_DATE, "FOREIGN MAT. UPDATED", GridView6.Rows(I).Cells(7).Text)
                    'sit credit
                    LEDGER_SAVE_PUR(po_no, MAT_SLNO, SUPL_ID, GARN_NO, SIT_HEAD, "Cr", FormatNumber(mat_value, 2), "PROV. CRED. FOR RM(FOR.)", 3, "", EFFECTIVE_DATE, "FOREIGN MAT. UPDATED", GridView6.Rows(I).Cells(7).Text)
                    'CUSTOM DUTY credit
                    LEDGER_SAVE_PUR(po_no, MAT_SLNO, SUPL_ID, GARN_NO, "86201", "Cr", FormatNumber(MAT_CUSTOM_DUTY + MAT_IGST_VALUE, 2), "CUSTOM DUTY", 4, "", EFFECTIVE_DATE, "FOREIGN MAT. UPDATED", GridView6.Rows(I).Cells(7).Text)
                    'STATUTORY CHARGES credit
                    LEDGER_SAVE_PUR(po_no, MAT_SLNO, SUPL_ID, GARN_NO, "51746", "Cr", FormatNumber(MAT_STATUTORY_CHARGE, 2), "STATUTORY CHARGES", 5, "", EFFECTIVE_DATE, "FOREIGN MAT. UPDATED", GridView6.Rows(I).Cells(7).Text)
                    'INSURANCE credit
                    LEDGER_SAVE_PUR(po_no, MAT_SLNO, SUPL_ID, GARN_NO, "51207", "Cr", FormatNumber(MAT_INSURANCE_VALUE, 2), "INSURANCE", 6, "", EFFECTIVE_DATE, "FOREIGN MAT. UPDATED", GridView6.Rows(I).Cells(7).Text)
                    ''IGST
                    LEDGER_SAVE_PUR(po_no, 0, "", GARN_NO, cenvat_head, "Dr", MAT_IGST_VALUE, "IGST", 2, "", EFFECTIVE_DATE, "FOREIGN MAT. UPDATED", GridView6.Rows(I).Cells(7).Text)


                    conn.Open()
                    mycommand.CommandText = "select * from PO_RCD_MAT where GARN_NO ='" & GARN_NO & "'"
                    mycommand.Connection = conn
                    dr = mycommand.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TRANS_CHARGE = dr.Item("TRANS_CHARGE")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If


                    Dim cmd As New SqlCommand
                    Dim Query_1 As String = "update PO_RCD_MAT set UNIT_RATE=@UNIT_RATE,MAT_RATE=@MAT_RATE,PROV_VALUE=@PROV_VALUE,PARTY_PAY=@PARTY_PAY WHERE GARN_NO ='" & GARN_NO & "'"
                    cmd = New SqlCommand(Query_1, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@UNIT_RATE", UNIT_PRICE)
                    cmd.Parameters.AddWithValue("@MAT_RATE", mat_value)
                    cmd.Parameters.AddWithValue("@PROV_VALUE", mat_value - TRANS_CHARGE)
                    cmd.Parameters.AddWithValue("@PARTY_PAY", mat_value)
                    cmd.ExecuteReader()
                    cmd.Dispose()


                    Query_1 = "update MAT_DETAILS set UNIT_PRICE=@UNIT_PRICE,TOTAL_PRICE=@TOTAL_PRICE WHERE ISSUE_NO ='" & GARN_NO & "'"
                    cmd = New SqlCommand(Query_1, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@UNIT_PRICE", UNIT_PRICE)
                    cmd.Parameters.AddWithValue("@TOTAL_PRICE", mat_value)
                    cmd.ExecuteReader()
                    cmd.Dispose()

                Next

                myTrans.Commit()

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()

            Finally
                conn.Close()
                conn_trans.Close()
            End Try
        End Using
    End Sub



    Protected Sub LEDGER_SAVE_PUR(PO_NO As String, MAT_SLNO As Integer, SUPL_ID As String, GARN_NO As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, J_LINE_NO As Integer, PAY_IND As String, EFFECTIVE_DATE As String, rever_ind As String, BE_NO As String)
        Dim working_date As Date
        If EFFECTIVE_DATE = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(EFFECTIVE_DATE) = False Then
            TextBox2.Focus()
            Return
        End If
        'working_date = CDate(TextBox2.Text)
        working_date = CDate(EFFECTIVE_DATE)
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

            conn.Open()
            Dim QUARY1 As String
            QUARY1 = "Insert Into LEDGER(BE_NO,REVERSAL_INDICATOR,Journal_ID,JURNAL_LINE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@BE_NO,@REVERSAL_INDICATOR,@Journal_ID,@JURNAL_LINE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            Dim cmd1 As New SqlCommand(QUARY1, conn)
            cmd1.Parameters.AddWithValue("@PO_NO", PO_NO)
            cmd1.Parameters.AddWithValue("@Journal_ID", MAT_SLNO)
            cmd1.Parameters.AddWithValue("@GARN_NO_MB_NO", GARN_NO)
            cmd1.Parameters.AddWithValue("@SUPL_ID", SUPL_ID)
            cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd1.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd1.Parameters.AddWithValue("@EFECTIVE_DATE", working_date)
            cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd1.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd1.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd1.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd1.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd1.Parameters.AddWithValue("@PAYMENT_INDICATION", PAY_IND)
            cmd1.Parameters.AddWithValue("@JURNAL_LINE_NO", J_LINE_NO)
            cmd1.Parameters.AddWithValue("@REVERSAL_INDICATOR", rever_ind)
            cmd1.Parameters.AddWithValue("@BE_NO", BE_NO)
            cmd1.ExecuteReader()
            cmd1.Dispose()
            conn.Close()
        End If
    End Sub
End Class