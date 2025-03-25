Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class mat_garn
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim mycommandNew As New SqlCommand
    Dim mycommandNew1 As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            type_DropDown.AutoPostBack = False
            MultiView1.ActiveViewIndex = -1

            type_DropDown.Visible = True
            search_DropDown.Visible = True
            new_button.Visible = True
            view_button.Visible = True
            type_DropDown.Items.Clear()
            search_DropDown.Items.Clear()

            type_DropDown.Items.Add("STORE MATERIAL")
            type_DropDown.Items.Add("STORE MATERIAL(IMP)")


            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList1.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList1.DataValueField = "FY"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            conn.Close()

            Dim dt5 As New DataTable()
            dt5.Columns.AddRange(New DataColumn(9) {New DataColumn("MAT_NAME"), New DataColumn("SUPL_NAME"), New DataColumn("BE_NO"), New DataColumn("MAT_CODE"), New DataColumn("PO_NO"), New DataColumn("TRANS_WO_NO"), New DataColumn("BE_QTY"), New DataColumn("BE_BAL"), New DataColumn("amd_no"), New DataColumn("CHA_ORDER")})
            ViewState("IMP") = dt5
            BINDGRID3()
            dt5.Rows.Add("", "", "", "", "", "", "", "", "", "")
            ViewState("IMP") = dt5
            BINDGRID3()
            Dim dt4 As New DataTable()
            dt4.Columns.AddRange(New DataColumn(13) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Ord Qty"), New DataColumn("Chln Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note"), New DataColumn("BE_NO")})
            ViewState("imp_mat") = dt4
            Me.BINDGRID4()
        End If

        Delvdate7_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("storeAccess")) Or Session("storeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub BINDGRID3()
        imp_FormView1.DataSource = DirectCast(ViewState("IMP"), DataTable)
        imp_FormView1.DataBind()
        imp_FormView1.DataSource = DirectCast(ViewState("IMP"), DataTable)
        imp_FormView1.DataBind()
    End Sub


    Protected Sub BINDGRID4()
        imp_GridView3.DataSource = DirectCast(ViewState("imp_mat"), DataTable)
        imp_GridView3.DataBind()
    End Sub

    Protected Sub BINDGRID2()
        GridView2.DataSource = DirectCast(ViewState("mat2"), DataTable)
        GridView2.DataBind()
    End Sub

    Protected Sub LEDGER_SAVE_PUR(PO_NO As String, MAT_SLNO As Integer, SUPL_ID As String, GARN_NO As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, J_LINE_NO As Integer, PAY_IND As String, BE_NO As String)
        Dim working_date As Date
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        End If
        working_date = CDate(TextBox2.Text)
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


            Dim QUARY1 As String
            QUARY1 = "Insert Into LEDGER(BE_NO,Journal_ID,JURNAL_LINE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@BE_NO,@Journal_ID,@JURNAL_LINE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
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
            cmd1.Parameters.AddWithValue("@BE_NO", BE_NO)
            cmd1.ExecuteReader()
            cmd1.Dispose()

        End If
    End Sub

    Protected Sub BINDGRID1()
        GridView2.DataSource = DirectCast(ViewState("mat"), DataTable)
        GridView2.DataBind()

    End Sub

    Protected Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        If TextBox2.Text = "" Or IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            MultiView1.ActiveViewIndex = 0
            GARN_ERR_LABLE.Text = "Please Enter GARN date."
            Return
        ElseIf TextBox143.Text = "" Or IsNumeric(TextBox143.Text) = False Then
            TextBox143.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter L.D. Percentege"
            Return
        ElseIf TextBox147.Text = "" Or IsNumeric(TextBox147.Text) = False Or TextBox147.Text < 0 Then
            TextBox147.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Unit Price"
            Return
        ElseIf TextBox149.Text = "" Or IsNumeric(TextBox149.Text) = False Then
            TextBox149.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Discount Percentege"
            Return
        ElseIf TextBox151.Text = "" Or IsNumeric(TextBox151.Text) = False Then
            TextBox151.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Packing Percentege"
            Return
        ElseIf TextBox153.Text = "" Or IsNumeric(TextBox153.Text) = False Then
            TextBox153.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Excise Duty"
            Return
        ElseIf TextBox155.Text = "" Or IsNumeric(TextBox155.Text) = False Then
            TextBox155.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter VAT/CST Percentege"
            Return
        ElseIf TextBox157.Text = "" Or IsNumeric(TextBox157.Text) = False Then
            TextBox157.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Freight"
            Return
        ElseIf TextBox159.Text = "" Or IsNumeric(TextBox159.Text) = False Then
            TextBox159.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Entry Tax"
            Return
        ElseIf TextBox160.Text = "" Or IsNumeric(TextBox160.Text) = False Then
            TextBox160.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Local Freight"
            Return
        ElseIf TextBox161.Text = "" Or IsNumeric(TextBox161.Text) = False Then
            TextBox161.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Analytical Charge"
            Return
        ElseIf TextBox162.Text = "" Or IsNumeric(TextBox162.Text) = False Then
            TextBox162.Focus()
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Please Enter Penality"
            Return
        ElseIf DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            GARN_ERR_LABLE.Text = "Please Select Mat. SlNo"
            Return
        End If

        '''''''''''''''''''''''''''''''''

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                ''Checking GARN date and CRR date
                Dim Block_DATE As String = ""
                conn.Open()
                Dim MC_new As New SqlCommand
                MC_new.CommandText = "SELECT Block_date FROM Date_Freeze"
                MC_new.Connection = conn
                dr = MC_new.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    Block_DATE = dr.Item("Block_date")
                    dr.Close()
                End If
                conn.Close()

                If (CDate(TextBox2.Text) <= CDate(Block_DATE)) Then
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Garn before " & Block_DATE & " has been freezed."
                Else

                    ''Checking GARN date and CRR date
                    Dim CRR_DATE As String = ""
                    conn.Open()
                    'Dim MC_new As New SqlCommand
                    MC_new.CommandText = "SELECT CRR_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & garn_crrnoDropDownList.SelectedValue & "'"
                    MC_new.Connection = conn
                    dr = MC_new.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        CRR_DATE = dr.Item("CRR_DATE")
                        dr.Close()
                    End If
                    conn.Close()

                    If (CRR_DATE > CDate(TextBox2.Text)) Then
                        GARN_ERR_LABLE.Visible = True
                        GARN_ERR_LABLE.Text = "GARN date cannot be before CRR date."
                        Button5.Enabled = False
                    Else
                        GARN_ERR_LABLE.Visible = False
                        GARN_ERR_LABLE.Text = ""
                        ''delete garn_price
                        'conn.Open()
                        mycommand = New SqlCommand("DELETE FROM GARN_PRICE WHERE CRR_NO ='" & garn_crrnoDropDownList.SelectedValue & "' and po_no ='" & Label398.Text & "' and MAT_SLNO=" & DropDownList6.SelectedValue, conn_trans, myTrans)
                        mycommand.ExecuteNonQuery()
                        'conn.Close()
                        ''save garn_price
                        Dim status As String
                        If CheckBox1.Checked = True Then
                            status = "1"

                        Else
                            status = "0"
                        End If
                        'conn.Open()
                        Dim query As String = "Insert Into GARN_PRICE(CRR_NO,PO_NO,MAT_SLNO,UNIT_PRICE,DISCOUNT,PACKING,FREIGHT,SGST,CGST,IGST,CESS,ANALITICAL_CHARGE,LD_CHARGE,LOCAL_FREIGHT,PENALITY_TYPE,PENALITY,REMARKS_PENALITY,REMARKS_MODIFY,LAST_MODIFY_BY,TRANS_PEN ) values (@CRR_NO,@PO_NO,@MAT_SLNO,@UNIT_PRICE,@DISCOUNT,@PACKING,@FREIGHT,@SGST,@CGST,@IGST,@CESS,@ANALITICAL_CHARGE,@LD_CHARGE,@LOCAL_FREIGHT,@PENALITY_TYPE,@PENALITY,@REMARKS_PENALITY,@REMARKS_MODIFY,@LAST_MODIFY_BY,@TRANS_PEN)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@CRR_NO", garn_crrnoDropDownList.SelectedValue)
                        cmd.Parameters.AddWithValue("@PO_NO", Label398.Text)
                        cmd.Parameters.AddWithValue("@MAT_SLNO", DropDownList6.SelectedValue)
                        cmd.Parameters.AddWithValue("@UNIT_PRICE", TextBox147.Text)
                        cmd.Parameters.AddWithValue("@DISCOUNT", TextBox149.Text)
                        cmd.Parameters.AddWithValue("@PACKING", TextBox151.Text)
                        cmd.Parameters.AddWithValue("@SGST", TextBox153.Text)
                        cmd.Parameters.AddWithValue("@CGST", TextBox155.Text)
                        cmd.Parameters.AddWithValue("@IGST", TextBox159.Text)
                        cmd.Parameters.AddWithValue("@CESS", TextBox4.Text)
                        cmd.Parameters.AddWithValue("@FREIGHT", TextBox157.Text)
                        cmd.Parameters.AddWithValue("@LD_CHARGE", TextBox143.Text)
                        cmd.Parameters.AddWithValue("@LOCAL_FREIGHT", TextBox160.Text)
                        cmd.Parameters.AddWithValue("@PENALITY_TYPE", status)
                        cmd.Parameters.AddWithValue("@PENALITY", TextBox162.Text)
                        cmd.Parameters.AddWithValue("@TRANS_PEN", TextBox175.Text)
                        cmd.Parameters.AddWithValue("@ANALITICAL_CHARGE", TextBox161.Text)
                        cmd.Parameters.AddWithValue("@REMARKS_PENALITY", TextBox144.Text)
                        cmd.Parameters.AddWithValue("@REMARKS_MODIFY", TextBox145.Text)
                        cmd.Parameters.AddWithValue("@LAST_MODIFY_BY", Session("userName"))
                        cmd.ExecuteReader()
                        cmd.Dispose()
                        'conn.Close()

                        Dim basic_price, disc_price, TOLE_VALUE, TRANS_DIFF, w_tolerance, packing_price, short_trans_amt, cess_price, sgst_price, cgst_price, igst_price, ld_price, freight_price, penality_price, analitical_price, WT_VAR, LOSS_GST, TRANS_VALUE_TOTAL, TRANS_SHORT_VALUE As New Decimal(0)
                        Dim unit_price, freight, ld_charge, packing_charge, local_freight, LOCAL_FREIGHT_PRICE, discount, net_price, analitical_charge, ed_charge, penality, sgst, cgst, igst, cess As New Decimal(0)
                        unit_price = TextBox147.Text
                        discount = TextBox149.Text
                        packing_charge = TextBox151.Text
                        sgst = TextBox153.Text
                        cgst = TextBox155.Text
                        freight = TextBox157.Text
                        igst = TextBox159.Text
                        cess = TextBox4.Text
                        ld_charge = TextBox143.Text
                        local_freight = TextBox160.Text
                        analitical_charge = TextBox161.Text
                        penality = TextBox162.Text
                        Dim I As Integer
                        For I = 0 To GridView2.Rows.Count - 1
                            If GridView2.Rows(I).Cells(3).Text = DropDownList6.SelectedValue Then
                                'CALCULATE MATERIAL VALUE
                                Dim ACCEPT_QTY, CHLN_QTY, RCVD_QTY, REJ_QTY, PARTY_QTY, excess_qty, TRANS_QTY, TOTAL_MT, PARTY_AMOUNT, TRANSPORT_ADJUST As New Decimal(0.0)
                                ACCEPT_QTY = CDec(GridView2.Rows(I).Cells(12).Text)
                                CHLN_QTY = CDec(GridView2.Rows(I).Cells(9).Text)
                                RCVD_QTY = CDec(GridView2.Rows(I).Cells(10).Text)
                                REJ_QTY = CDec(GridView2.Rows(I).Cells(11).Text)
                                excess_qty = CDec(GridView2.Rows(I).Cells(13).Text)
                                TOTAL_MT = CDec(GridView2.Rows(I).Cells(33).Text)

                                If Label424.Text = "N/A" Then
                                    PARTY_QTY = ACCEPT_QTY
                                ElseIf Label424.Text <> "N/A" Then
                                    PARTY_QTY = CHLN_QTY - (REJ_QTY + excess_qty)
                                    TRANS_QTY = TOTAL_MT
                                End If
                                ''BASIC PRICE
                                basic_price = FormatNumber(unit_price * PARTY_QTY, 2)
                                ''DISCOUNT
                                If Label468.Text = "PERCENTAGE" Then
                                    disc_price = FormatNumber((basic_price * discount) / 100, 2)
                                ElseIf Label468.Text = "PER UNIT" Then
                                    disc_price = FormatNumber(PARTY_QTY * discount, 2)
                                End If

                                ''PACKING AND FORWD
                                If Label467.Text = "PERCENTAGE" Then
                                    packing_price = FormatNumber(((basic_price - disc_price) * packing_charge) / 100, 2)
                                ElseIf Label467.Text = "PER UNIT" Then
                                    packing_price = FormatNumber(PARTY_QTY * packing_charge, 2)
                                End If
                                ''NET PRICE
                                net_price = (basic_price - disc_price) + packing_price

                                ''freight calculate
                                If Label466.Text = "PERCENTAGE" Then
                                    freight_price = FormatNumber((basic_price * freight) / 100, 2)
                                ElseIf Label466.Text = "PER UNIT" Then
                                    freight_price = FormatNumber(PARTY_QTY * freight, 2)
                                End If

                                ''penality calculate
                                If CheckBox1.Checked = True Then
                                    penality_price = FormatNumber((basic_price * penality) / 100, 2)
                                ElseIf CheckBox1.Checked = False Then
                                    penality_price = FormatNumber(penality * PARTY_QTY, 2)
                                End If
                                ''ld calculate
                                ld_price = ld_charge


                                ''CGST,sgst,igst,cess,anal_charge
                                cgst_price = FormatNumber((((net_price + freight_price)) * cgst) / 100, 2)
                                sgst_price = FormatNumber((((net_price + freight_price)) * sgst) / 100, 2)
                                igst_price = FormatNumber((((net_price + freight_price)) * igst) / 100, 2)
                                cess_price = FormatNumber((((net_price + freight_price)) * cess) / 100, 2)
                                ''LOCAL FREIGHT
                                LOCAL_FREIGHT_PRICE = local_freight
                                ''ANALITICAL PRICE
                                analitical_price = analitical_charge * PARTY_QTY




                                'CALCULATE TRANSPORT
                                If Label424.Text <> "N/A" Then
                                    Dim qty, rqty, sqty, eqty, final_price, exqty, tolerance As New Decimal(0)
                                    Dim w_qty, w_complite, PRICE, w_unit_price, W_discount, DISCOUNT_VALUE As New Decimal(0)
                                    Dim w_name, w_au As New String("")

                                    ''TRANSPORTER TOLERANCE
                                    Dim MC As New SqlCommand
                                    conn.Open()
                                    MC.CommandText = "select sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & Label424.Text & "' AND W_SLNO='" & Label462.Text & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & garn_crrnoDropDownList.SelectedValue & "' AND MAT_SLNO='" & DropDownList6.SelectedValue & "')"
                                    MC.Connection = conn
                                    dr = MC.ExecuteReader
                                    If dr.HasRows Then
                                        dr.Read()
                                        w_qty = dr.Item("W_QTY")
                                        w_complite = dr.Item("W_COMPLITED")
                                        w_tolerance = dr.Item("W_TOLERANCE")
                                        w_unit_price = dr.Item("W_UNIT_PRICE")
                                        W_discount = dr.Item("W_DISCOUNT")
                                        w_name = dr.Item("W_NAME")
                                        w_au = dr.Item("W_AU")
                                        dr.Close()
                                    Else
                                        conn.Close()
                                    End If
                                    conn.Close()
                                    ''BAG WEIGHT CALCULATION
                                    Dim BAG_WEIGHT, NO_OF_BAG As Decimal
                                    conn.Open()
                                    MC.CommandText = "SELECT NO_OF_BAG,BAG_WEIGHT FROM PO_RCD_MAT WHERE CRR_NO='" & garn_crrnoDropDownList.SelectedValue & "' AND MAT_SLNO='" & DropDownList6.SelectedValue & "'"
                                    MC.Connection = conn
                                    dr = MC.ExecuteReader
                                    If dr.HasRows Then
                                        dr.Read()
                                        BAG_WEIGHT = dr.Item("BAG_WEIGHT")
                                        NO_OF_BAG = dr.Item("NO_OF_BAG")
                                        dr.Close()
                                    Else
                                        conn.Close()
                                    End If
                                    conn.Close()
                                    Dim b_weight As Decimal = 0
                                    If BAG_WEIGHT * NO_OF_BAG > 0 Then
                                        b_weight = (BAG_WEIGHT * NO_OF_BAG) / 1000
                                    ElseIf BAG_WEIGHT * NO_OF_BAG <= 0 Then
                                        b_weight = 0
                                    End If

                                    TRANS_QTY = TRANS_QTY + ((BAG_WEIGHT * NO_OF_BAG) / 1000)
                                    PRICE = w_unit_price * TRANS_QTY
                                    DISCOUNT_VALUE = (PRICE * W_discount) / 100
                                    PRICE = (PRICE - DISCOUNT_VALUE)
                                    final_price = PRICE
                                    final_price = FormatNumber(final_price, 2)
                                    TRANS_VALUE_TOTAL = FormatNumber(final_price, 2)
                                    ''TOLERANCE
                                    TOLE_VALUE = (CDec(GridView2.Rows(I).Cells(9).Text) * w_tolerance) / 100
                                    ''TRANSPORTATION DIFFRENCE
                                    TRANS_DIFF = CDec(GridView2.Rows(I).Cells(9).Text) - CDec(GridView2.Rows(I).Cells(10).Text)
                                    If TOLE_VALUE > TRANS_DIFF Then
                                        ''LOSS ON TRANSPORT
                                        WT_VAR = FormatNumber(((net_price + freight_price + LOCAL_FREIGHT_PRICE + analitical_price) / PARTY_QTY) * TRANS_DIFF, 2)
                                        ''LOSS ON GST NOT APPLICABLE
                                        LOSS_GST = FormatNumber(((cgst_price + sgst_price + igst_price + cess_price) / PARTY_QTY) * TRANS_DIFF, 2)
                                        ''SHORTAGE VALUE
                                        TRANS_SHORT_VALUE = 0
                                    Else
                                        If CDec(GridView2.Rows(I).Cells(9).Text) <> CDec(GridView2.Rows(I).Cells(10).Text) Then
                                            ''LOSS ON TRANSPORT
                                            WT_VAR = FormatNumber(((net_price + freight_price + LOCAL_FREIGHT_PRICE + analitical_price) / PARTY_QTY) * TOLE_VALUE, 2)
                                            ''LOSS ON ED
                                            LOSS_GST = FormatNumber(((cgst_price + sgst_price + igst_price + cess_price) / PARTY_QTY) * TOLE_VALUE, 2)
                                            ''SHORTAGE VALUE
                                            TRANS_SHORT_VALUE = FormatNumber(((net_price + freight_price + LOCAL_FREIGHT_PRICE + analitical_price) / PARTY_QTY) * TRANS_DIFF, 2) - (WT_VAR)
                                        End If

                                    End If

                                End If
                                'PUT VALUE
                                ''unit price
                                GridView2.Rows(I).Cells(15).Text = CDec(TextBox147.Text)
                                ''discount
                                GridView2.Rows(I).Cells(16).Text = FormatNumber(disc_price, 2)
                                ''pack & ford
                                GridView2.Rows(I).Cells(17).Text = FormatNumber(packing_price, 2)
                                ''FREIGHT
                                GridView2.Rows(I).Cells(18).Text = FormatNumber(freight_price, 2)
                                ''SGST
                                GridView2.Rows(I).Cells(19).Text = FormatNumber(sgst_price, 2)
                                'CGST
                                GridView2.Rows(I).Cells(20).Text = FormatNumber(cgst_price, 2)
                                ''IGST
                                GridView2.Rows(I).Cells(21).Text = FormatNumber(igst_price, 2)
                                'CESS
                                GridView2.Rows(I).Cells(22).Text = FormatNumber(cess_price, 2)
                                'ANAL
                                GridView2.Rows(I).Cells(23).Text = FormatNumber(analitical_price, 2)
                                'LOCAL FREIGHT
                                GridView2.Rows(I).Cells(24).Text = FormatNumber(LOCAL_FREIGHT_PRICE, 2)
                                ''penality
                                GridView2.Rows(I).Cells(25).Text = FormatNumber(penality_price, 2)
                                'LOSS GST
                                GridView2.Rows(I).Cells(28).Text = FormatNumber(LOSS_GST, 2)
                                'WT VAR
                                GridView2.Rows(I).Cells(29).Text = FormatNumber(WT_VAR, 2)
                                'TRANSPORT CHARGE
                                GridView2.Rows(I).Cells(30).Text = FormatNumber(TRANS_VALUE_TOTAL, 2)
                                'TRANSPORT SHORTAGE
                                GridView2.Rows(I).Cells(31).Text = FormatNumber(TRANS_SHORT_VALUE, 2)
                                ''late delivery
                                GridView2.Rows(I).Cells(26).Text = FormatNumber(ld_price, 2)
                                ''MATERIAL VALUE
                                GridView2.Rows(I).Cells(27).Text = FormatNumber((net_price + LOCAL_FREIGHT_PRICE + analitical_price + freight_price - (penality_price + TRANS_SHORT_VALUE)), 2)
                            End If
                        Next
                        Button5.Enabled = True
                    End If
                End If

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



        '''''''''''''''''''''''''''''''''

    End Sub

    Protected Sub garn_crrnoDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles garn_crrnoDropDownList.SelectedIndexChanged
        If garn_crrnoDropDownList.SelectedValue <> "Select" Then
            conn.Open()
            da = New SqlDataAdapter("select PO_RCD_MAT.MAT_SLNO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .crr_no='" & garn_crrnoDropDownList.Text & "' and PO_RCD_MAT.GARN_NO = 'Pending' AND MAT_RCD_QTY <> MAT_REJ_QTY ORDER BY PO_RCD_MAT.MAT_SLNO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList6.Items.Clear()
            DropDownList6.DataSource = dt
            DropDownList6.DataValueField = "MAT_SLNO"
            DropDownList6.DataBind()
            DropDownList6.Items.Insert(0, "Select")
            DropDownList6.SelectedValue = "Select"
        End If

        ''Adding data to gridview
        ''''''''''''''''''''''''''''

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("WITH group1 AS (
                select PO_RCD_MAT.PO_NO,PO_RCD_MAT.TOTAL_MT,PO_RCD_MAT.CRR_NO,PO_RCD_MAT.CHLN_NO,PO_RCD_MAT.MAT_SLNO,PO_RCD_MAT.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,PO_RCD_MAT.MAT_CHALAN_QTY,PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_REJ_QTY,PO_RCD_MAT.MAT_EXCE,PO_RCD_MAT.MAT_BAL_QTY,PO_RCD_MAT.INSP_NOTE,PO_RCD_MAT.CRR_DATE,PO_RCD_MAT.AMD_NO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .crr_no='" & garn_crrnoDropDownList.SelectedValue & "' and PO_RCD_MAT.GARN_NO = 'Pending' AND MAT_RCD_QTY <> MAT_REJ_QTY),
                group2 AS (
                SELECT PO_ORD_MAT.MAT_SLNO as g2SL_No,SUM(MAT_QTY) as MAT_QTY FROM PO_ORD_MAT join po_rcd_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno WHERE PO_RCD_MAT .crr_no='" & garn_crrnoDropDownList.SelectedValue & "' group by PO_ORD_MAT.MAT_SLNO)
                SELECT * FROM group1 JOIN group2 ON group1.MAT_SLNO = group2.g2SL_No", conn)

        da.Fill(dt)
        conn.Close()
        GridView2.DataSource = dt
        GridView2.DataBind()
        conn.Close()


        ''''''''''''''''''''''''''''
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        If DropDownList6.SelectedValue = "Select " Then
            DropDownList6.Focus()
            Return
        End If

        '''''''''''''''''''''''''''''''''''''''''

        conn.Open()
        Dim supl_tax As String = ""
        Dim mc2 As New SqlCommand
        mc2.CommandText = " Select supl.SUPL_NAME, supl.SUPL_ID, PO_RCD_MAT.TRANS_SLNO, PO_RCD_MAT.PO_NO, PO_RCD_MAT.TRANS_WO_NO From PO_RCD_MAT Join SUPL On PO_RCD_MAT.SUPL_ID =SUPL.SUPL_ID Where PO_RCD_MAT.CRR_NO ='" & garn_crrnoDropDownList.SelectedValue & "' and PO_RCD_MAT.MAT_SLNO='" & DropDownList6.SelectedValue & "'"
      mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label398.Text = dr.Item("po_no")
            Label396.Text = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
            Label424.Text = dr.Item("TRANS_WO_NO")
            Label462.Text = dr.Item("TRANS_SLNO")
            dr.Close()
        End If
        conn.Close()

        Dim i As Integer
        For i = 0 To GridView2.Rows.Count - 1
            GridView2.Rows(i).Cells(12).Text = CDec(GridView2.Rows(i).Cells(10).Text) - (CDec(GridView2.Rows(i).Cells(11).Text) + CDec(GridView2.Rows(i).Cells(13).Text))
        Next
        '''''''''''''''''''''''''''''''''''''''''
        ''LD CALCULATE
        conn.Open()
        Dim mcL As New SqlCommand
        Dim LD_STRING As String = ""
        mcL.CommandText = "select * from ORDER_DETAILS where so_no= '" & Label398.Text & "'"
        mcL.Connection = conn
        dr = mcL.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            LD_STRING = dr.Item("LD")
            dr.Close()
        End If
        conn.Close()
        If LD_STRING = "Applicable" Then
            conn.Open()
            Dim mc1 As New SqlCommand
            Dim delv_date As Date
            mc1.CommandText = "select MAX(MAT_DELIVERY) as delv_date from PO_ORD_MAT where po_no='" & Label398.Text & "' and mat_slno=" & DropDownList6.SelectedValue
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                delv_date = dr.Item("delv_date")
                dr.Close()
            End If
            conn.Close()
            conn.Open()
            Dim crr_date As Date
            mc1.CommandText = "select CRR_DATE  from PO_RCD_MAT where po_no='" & Label398.Text & "' and mat_slno=" & DropDownList6.SelectedValue
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                crr_date = dr.Item("CRR_DATE")
                dr.Close()
            End If
            conn.Close()
            Dim day_check As Integer
            day_check = DateDiff(DateInterval.Day, delv_date, CDate(crr_date))
            Dim LD_COUNT As Decimal
            If day_check > 0 Then
                Label419.Visible = True
                Label419.Text = day_check & " Days Late"
                LD_COUNT = day_check / 30
                If LD_COUNT < 1 Then
                    TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 1 And LD_COUNT < 2 Then
                    TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 2 And LD_COUNT < 3 Then
                    TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 3 And LD_COUNT < 4 Then
                    TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 4 Then
                    TextBox142.Text = "0.00"
                End If
            Else
                TextBox142.Text = "0.00"
                Label419.Visible = False
            End If
        Else
            TextBox142.Text = "0.00"
            Label419.Visible = False
        End If
        ''AS PER ORDER DATA SEARCH
        conn.Open()
        Dim freight_term, freight_type As String
        freight_term = ""
        freight_type = ""
        Dim mc As New SqlCommand
        mc.CommandText = "select FREIGHT_TERM FROM ORDER_DETAILS where so_no='" & Label398.Text & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            freight_term = dr.Item("FREIGHT_TERM")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If freight_term = "Extra As Per Mt." Then
            ''TextBox157.BackColor = Drawing.Color.White
            TextBox157.ReadOnly = False

        ElseIf freight_term = "Extra As %" Then
            ''TextBox157.BackColor = Drawing.Color.White
            TextBox157.ReadOnly = False

        ElseIf freight_term = "Paid" Or freight_term = "Not Applicable" Then
            ''TextBox157.BackColor = Drawing.Color.Aqua
            TextBox157.ReadOnly = True

        End If
        conn.Open()
        mc.CommandText = "SELECT SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TYPE) AS FREIGHT_TYPE,MAX(DISC_TYPE) AS DISC_TYPE, MAX(PF_TYPE) AS PF_TYPE, MAX(MAT_CODE) as mat_code,MAX(MAT_NAME) AS MAT_NAME,MAX(AMD_NO) as AMD_NO,SUM(MAT_QTY) AS MAT_QTY, SUM(MAT_UNIT_RATE ) AS MAT_UNIT_RATE,SUM(MAT_PACK) AS MAT_PACK,SUM(MAT_DISCOUNT) AS MAT_DISCOUNT,SUM(CGST) AS CGST,SUM(SGST) AS SGST,SUM(IGST) AS IGST,SUM(CESS) AS CESS,SUM(ANAL_TAX ) AS ANAL_TAX,SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD,SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TERM) AS FREIGHT_TYPE FROM PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT .PO_NO=ORDER_DETAILS .SO_NO WHERE PO_ORD_MAT.PO_NO='" & Label398.Text & "' AND PO_ORD_MAT.MAT_SLNO=" & DropDownList6.SelectedValue & " and PO_ORD_MAT.AMD_DATE < =(SELECT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & garn_crrnoDropDownList.SelectedValue & "' AND MAT_SLNO=" & DropDownList6.SelectedValue & ")"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox120.Text = dr.Item("MAT_UNIT_RATE")
            TextBox124.Text = dr.Item("MAT_DISCOUNT")
            TextBox121.Text = dr.Item("MAT_PACK")
            TextBox123.Text = dr.Item("CGST")
            TextBox122.Text = dr.Item("SGST")
            TextBox137.Text = dr.Item("IGST")
            TextBox3.Text = dr.Item("CESS")
            TextBox5.Text = dr.Item("ANAL_TAX")
            TextBox125.Text = dr.Item("MAT_FREIGHT_PU")
            Label392.Text = dr.Item("mat_code")
            Label394.Text = dr.Item("MAT_NAME")
            Label422.Text = dr.Item("amd_no")
            Label468.Text = dr.Item("DISC_TYPE")
            Label467.Text = dr.Item("PF_TYPE")
            Label466.Text = dr.Item("FREIGHT_TYPE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        TextBox147.Text = TextBox120.Text
        TextBox149.Text = TextBox124.Text
        TextBox151.Text = TextBox121.Text
        TextBox157.Text = TextBox125.Text
        TextBox143.Text = TextBox142.Text
        TextBox153.Text = TextBox122.Text
        TextBox155.Text = TextBox123.Text
        TextBox159.Text = TextBox137.Text
        TextBox4.Text = TextBox3.Text
        TextBox161.Text = TextBox5.Text
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim working_date As Date
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        End If
        working_date = CDate(TextBox2.Text)
        If GridView2.Rows.Count = 0 Then
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "No Data Added"
            Return
        ElseIf TextBox175.Text = "" Or IsNumeric(TextBox175.Text) = False Then
            TextBox175.Focus()
            GARN_ERR_LABLE.Text = "Penality For Transporter can't be empty or char "
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
        ''GARN TYPE
        Dim CRR_TYPE As String = ""
        If type_DropDown.SelectedValue = "STORE MATERIAL" Then
            CRR_TYPE = "SGARN"
        ElseIf type_DropDown.SelectedValue = "RAW MATERIAL" Then
            CRR_TYPE = "RGARN"
        End If

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                conn.Open()
                da = New SqlDataAdapter("select distinct GARN_NO from PO_RCD_MAT WHERE GARN_NO<>'PENDING' AND GARN_NO LIKE '" & CRR_TYPE + STR1 & "%'", conn)
                count = da.Fill(ds, "PO_RCD_MAT")
                conn.Close()



                If count = 0 Then
                    GARN_NO_TextBox.Text = CRR_TYPE & STR1 & "000001"
                Else
                    str = count + 1
                    If str.Length = 1 Then
                        str = "00000" & (count + 1)
                    ElseIf str.Length = 2 Then
                        str = "0000" & (count + 1)
                    ElseIf str.Length = 3 Then
                        str = "000" & (count + 1)
                    ElseIf str.Length = 4 Then
                        str = "00" & (count + 1)
                    ElseIf str.Length = 5 Then
                        str = "0" & (count + 1)
                    End If
                    GARN_NO_TextBox.Text = CRR_TYPE & STR1 & str
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
                Dim I As Integer
                For I = 0 To GridView2.Rows.Count - 1
                    If IsNumeric(GridView2.Rows(I).Cells(20).Text) = False Then
                        GARN_ERR_LABLE.Visible = True
                        GARN_ERR_LABLE.Text = "Please Add Material SlNo Wise Details"
                        Return
                    Else
                        GARN_ERR_LABLE.Visible = False
                    End If
                Next
                For I = 0 To GridView2.Rows.Count - 1
                    Dim G_DATE As Date = working_date.Date
                    'conn.Open()
                    Dim query As String = "update PO_RCD_MAT set UNIT_RATE=@UNIT_RATE,FISCAL_YEAR=@FISCAL_YEAR, TRANS_CHARGE=@TRANS_CHARGE, GARN_NO=@GARN_NO,GARN_NOTE=@GARN_NOTE,PROV_VALUE=@PROV_VALUE,GARN_DATE=@GARN_DATE,GARN_EMP=@GARN_EMP,TRANS_SHORT=@TRANS_SHORT,GARN_ENTRY_DATE=@GARN_ENTRY_DATE where MAT_SLNO='" & GridView2.Rows(I).Cells(3).Text & "' and CRR_NO='" & garn_crrnoDropDownList.SelectedValue & "'"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@GARN_NO", GARN_NO_TextBox.Text)
                    cmd.Parameters.AddWithValue("@GARN_DATE", Date.ParseExact(G_DATE, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@PROV_VALUE", CDec(GridView2.Rows(I).Cells(27).Text))
                    cmd.Parameters.AddWithValue("@GARN_NOTE", TextBox173.Text)
                    cmd.Parameters.AddWithValue("@TRANS_SHORT", CDec(GridView2.Rows(I).Cells(31).Text))
                    cmd.Parameters.AddWithValue("@TRANS_CHARGE", CDec(GridView2.Rows(I).Cells(30).Text))
                    cmd.Parameters.AddWithValue("@GARN_EMP", Session("userName"))
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@UNIT_RATE", CDec(GridView2.Rows(I).Cells(15).Text))
                    cmd.Parameters.AddWithValue("@GARN_ENTRY_DATE", Now)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()

                    ''UPDATE MATERIAL STOCK AND AVG PRICEING
                    Dim STOCK_QTY, AVG_PRICE As Decimal
                    Dim MC As New SqlCommand
                    conn.Open()
                    MC.CommandText = "select * from MATERIAL WITH(NOLOCK) where MAT_CODE = '" & GridView2.Rows(I).Cells(4).Text & "'"
                    MC.Connection = conn
                    dr = MC.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        STOCK_QTY = dr.Item("MAT_STOCK")
                        AVG_PRICE = dr.Item("MAT_AVG")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If

                    Dim NEW_AVG_PRICE, NEW_UNIT_RATE, NEW_MAT_VALUE As Decimal
                    NEW_MAT_VALUE = (CDec(GridView2.Rows(I).Cells(27).Text) + FormatNumber(CDec(GridView2.Rows(I).Cells(30).Text), 3))
                    NEW_UNIT_RATE = CDec(FormatNumber(NEW_MAT_VALUE / CDec(GridView2.Rows(I).Cells(12).Text), 3))
                    NEW_AVG_PRICE = FormatNumber(((STOCK_QTY * AVG_PRICE) + NEW_MAT_VALUE) / (CDec(GridView2.Rows(I).Cells(12).Text) + STOCK_QTY), 3)
                    'conn.Open()
                    query = "update MATERIAL set MAT_AVG=@MAT_AVG,LAST_TRANS_DATE=@LAST_TRANS_DATE,MAT_STOCK=@MAT_STOCK,MAT_LAST_RATE=@MAT_LAST_RATE,MAT_LASTPUR_DATE=@MAT_LASTPUR_DATE where MAT_CODE='" & GridView2.Rows(I).Cells(4).Text & "'"
                    cmd = New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@MAT_AVG", NEW_AVG_PRICE)
                    cmd.Parameters.AddWithValue("@MAT_STOCK", CDec(GridView2.Rows(I).Cells(12).Text) + STOCK_QTY)
                    cmd.Parameters.AddWithValue("@MAT_LAST_RATE", NEW_UNIT_RATE)
                    cmd.Parameters.AddWithValue("@MAT_LASTPUR_DATE", G_DATE)
                    cmd.Parameters.AddWithValue("@LAST_TRANS_DATE", G_DATE)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()
                    ''LINE BALANCING

                    count = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WITH(NOLOCK) WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & GridView2.Rows(I).Cells(4).Text & "' AND LINE_NO <> 0", conn)
                    count = da.Fill(ds, "MAT_DETAILS")
                    conn.Close()

                    'conn.Open()
                    query = "Insert Into MAT_DETAILS(MAT_SL_NO,ENTRY_DATE,AVG_PRICE,ISSUE_NO,LINE_NO,LINE_DATE,FISCAL_YEAR,LINE_TYPE,MAT_CODE,MAT_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,QTR,ISSUE_TYPE,COST_CODE,ISSUE_QTY)VALUES(@MAT_SL_NO,@ENTRY_DATE,@AVG_PRICE,@ISSUE_NO,@LINE_NO,@LINE_DATE,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@MAT_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@QTR,@ISSUE_TYPE,@COST_CODE,@ISSUE_QTY)"
                    cmd = New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@ISSUE_NO", GARN_NO_TextBox.Text)
                    cmd.Parameters.AddWithValue("@LINE_NO", count + 1)
                    cmd.Parameters.AddWithValue("@ISSUE_TYPE", "PURCHASE")
                    cmd.Parameters.AddWithValue("@LINE_DATE", G_DATE)
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
                    cmd.Parameters.AddWithValue("@LINE_TYPE", "R")
                    cmd.Parameters.AddWithValue("@MAT_CODE", GridView2.Rows(I).Cells(4).Text)
                    cmd.Parameters.AddWithValue("@MAT_QTY", CDec(GridView2.Rows(I).Cells(12).Text))
                    cmd.Parameters.AddWithValue("@MAT_BALANCE", CDec(GridView2.Rows(I).Cells(12).Text) + STOCK_QTY)
                    cmd.Parameters.AddWithValue("@UNIT_PRICE", NEW_UNIT_RATE)
                    cmd.Parameters.AddWithValue("@TOTAL_PRICE", NEW_MAT_VALUE)
                    cmd.Parameters.AddWithValue("@COST_CODE", Label398.Text)
                    cmd.Parameters.AddWithValue("@ISSUE_QTY", 0)
                    cmd.Parameters.AddWithValue("@QTR", qtr1)
                    cmd.Parameters.AddWithValue("@AVG_PRICE", NEW_AVG_PRICE)
                    cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd.Parameters.AddWithValue("@MAT_SL_NO", GridView2.Rows(I).Cells(3).Text)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()


                    Dim PURCHASE As New String("")
                    If (CInt(GridView2.Rows(I).Cells(4).Text.Substring(0, 3)) < 50) Then
                        ''LEDGER POSTING PURCHASE
                        conn.Open()
                        Dim MCc As New SqlCommand
                        MCc.CommandText = "select AC_PUR,AC_CON from MATERIAL WITH(NOLOCK) where MAT_CODE = '" & GridView2.Rows(I).Cells(4).Text & "'"
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
                    Else
                        PURCHASE = "60615"
                    End If


                    ''SAVE LEDGER PURCHASE
                    query = "Insert Into LEDGER(Journal_ID,JURNAL_LINE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@Journal_ID,@JURNAL_LINE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
                    Dim cmd5 As New SqlCommand(query, conn_trans, myTrans)
                    cmd5.Parameters.AddWithValue("@PO_NO", Label398.Text)
                    cmd5.Parameters.AddWithValue("@Journal_ID", GridView2.Rows(I).Cells(3).Text)
                    cmd5.Parameters.AddWithValue("@GARN_NO_MB_NO", GARN_NO_TextBox.Text)
                    cmd5.Parameters.AddWithValue("@SUPL_ID", "")
                    cmd5.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd5.Parameters.AddWithValue("@PERIOD", qtr1)
                    cmd5.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
                    cmd5.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd5.Parameters.AddWithValue("@AC_NO", PURCHASE)
                    cmd5.Parameters.AddWithValue("@AMOUNT_DR", CDec(GridView2.Rows(I).Cells(27).Text))
                    cmd5.Parameters.AddWithValue("@AMOUNT_CR", 0)
                    If (CInt(GridView2.Rows(I).Cells(4).Text.Substring(0, 3)) < 50) Then
                        cmd5.Parameters.AddWithValue("@POST_INDICATION", "PUR")
                    Else
                        cmd5.Parameters.AddWithValue("@POST_INDICATION", "CAPITAL PURCHASE")
                    End If

                    cmd5.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                    cmd5.Parameters.AddWithValue("@JURNAL_LINE_NO", 1)
                    cmd5.ExecuteReader()
                    cmd5.Dispose()
                Next


                Dim PROV_HEAD, PARTY_TYPE As New String("")
                conn.Open()
                Dim MC6 As New SqlCommand
                MC6.CommandText = "select PARTY_TYPE from SUPL where SUPL_ID='" & Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1) & "'"
                MC6.Connection = conn
                dr = MC6.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    PARTY_TYPE = dr.Item("PARTY_TYPE")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If


                ''ledger posting PROV for party
                Dim MC5 As New SqlCommand
                If (PARTY_TYPE = "MSME" Or PARTY_TYPE = "SSI") Then
                    PROV_HEAD = "5110A"
                Else
                    conn.Open()
                    MC5.CommandText = "select * from work_group where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & Label398.Text & "')  AND work_name =(SELECT ORDER_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & Label398.Text & "')"
                    MC5.Connection = conn
                    dr = MC5.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        PROV_HEAD = dr.Item("PROV_HEAD")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                End If


                ''ledger posting PROV for party

                Dim C As Integer
                Dim PROV_PRICE_FOR_PARTY As Decimal = 0.0
                For C = 0 To GridView2.Rows.Count - 1
                    PROV_PRICE_FOR_PARTY = PROV_PRICE_FOR_PARTY + CDec(GridView2.Rows(C).Cells(27).Text)

                Next
                ''INSERT LEDGER PROV SUPL
                'conn.Open()
                Dim QUERY1 As String = "Insert Into LEDGER(JURNAL_LINE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@JURNAL_LINE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
                Dim cmd1 As New SqlCommand(QUERY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@PO_NO", Label398.Text)
                cmd1.Parameters.AddWithValue("@GARN_NO_MB_NO", GARN_NO_TextBox.Text)
                cmd1.Parameters.AddWithValue("@SUPL_ID", Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1))
                cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                cmd1.Parameters.AddWithValue("@PERIOD", qtr1)
                cmd1.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
                cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
                cmd1.Parameters.AddWithValue("@AC_NO", PROV_HEAD)
                cmd1.Parameters.AddWithValue("@AMOUNT_DR", 0)
                cmd1.Parameters.AddWithValue("@AMOUNT_CR", PROV_PRICE_FOR_PARTY)
                cmd1.Parameters.AddWithValue("@POST_INDICATION", "PROV")
                cmd1.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                cmd1.Parameters.AddWithValue("@JURNAL_LINE_NO", 2)
                cmd1.ExecuteReader()
                cmd1.Dispose()
                'conn.Close()


                ''PROV for transport
                C = 0
                ''BAG WEIGHT CALCULATION

                Dim PROV_PRICE_FOR_TRANSPORTER As Decimal = 0.0
                Dim RCVD_QTY_TRANSPORTER As Decimal = 0.0
                Dim chln_qty_trans As Decimal = 0.0
                Dim SHORT_AMT As Decimal = 0.0
                For C = 0 To GridView2.Rows.Count - 1
                    PROV_PRICE_FOR_TRANSPORTER = PROV_PRICE_FOR_TRANSPORTER + CDec(GridView2.Rows(C).Cells(30).Text)
                    Dim BAG_WEIGHT, NO_OF_BAG As Decimal
                    conn.Open()
                    MC5.CommandText = "SELECT NO_OF_BAG,BAG_WEIGHT FROM PO_RCD_MAT WITH(NOLOCK) WHERE CRR_NO='" & GridView2.Rows(C).Cells(0).Text & "' AND MAT_SLNO='" & GridView2.Rows(C).Cells(3).Text & "'"
                    MC5.Connection = conn
                    dr = MC5.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        BAG_WEIGHT = dr.Item("BAG_WEIGHT")
                        NO_OF_BAG = dr.Item("NO_OF_BAG")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                    chln_qty_trans = chln_qty_trans + CDec(GridView2.Rows(C).Cells(9).Text) + (NO_OF_BAG * BAG_WEIGHT)
                    RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(GridView2.Rows(C).Cells(10).Text) + (NO_OF_BAG * BAG_WEIGHT)
                    SHORT_AMT = SHORT_AMT + CDec(GridView2.Rows(C).Cells(31).Text)
                Next

                ''GRID VIEW CLEAR
                Dim dt2 As New DataTable()
                dt2.Columns.AddRange(New DataColumn(12) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Ord Qty"), New DataColumn("Chln Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note")})
                ViewState("mat2") = dt2
                Me.BINDGRID2()
                Button5.Enabled = False

                myTrans.Commit()
                GARN_ERR_LABLE.Visible = True
                GARN_ERR_LABLE.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                GARN_NO_TextBox.Text = ""
                GARN_ERR_LABLE.Visible = True
                GARN_ERR_LABLE.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub


    Protected Sub new_button_Click(sender As Object, e As EventArgs) Handles new_button.Click

        If type_DropDown.SelectedValue = "STORE MATERIAL" Then

            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(12) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Ord Qty"), New DataColumn("Chln Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note")})
            ViewState("mat2") = dt2
            Me.BINDGRID2()
            Dim ds5 As New DataSet
            conn.Open()
            da = New SqlDataAdapter("select distinct PO_RCD_MAT.CRR_NO from PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_RCD_MAT.GARN_NO='PENDING' AND PO_RCD_MAT.MAT_RCD_QTY > PO_RCD_MAT.MAT_REJ_QTY AND PO_RCD_MAT.INSP_EMP IS NOT NULL AND ORDER_DETAILS.PO_TYPE='STORE MATERIAL' ORDER BY CRR_NO", conn)
            da.Fill(ds5, "PO_RCD_MAT")
            garn_crrnoDropDownList.DataSource = ds5.Tables("PO_RCD_MAT")
            garn_crrnoDropDownList.DataValueField = "CRR_NO"
            garn_crrnoDropDownList.DataBind()
            garn_crrnoDropDownList.Items.Insert(0, "Select")
            garn_crrnoDropDownList.SelectedValue = "Select"
            ds5.Tables.Clear()
            Panel16.Visible = False
            imp_Panel1.Visible = False
            MultiView1.ActiveViewIndex = 0

        ElseIf type_DropDown.SelectedValue = "STORE MATERIAL(IMP)" Then

            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(12) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Ord Qty"), New DataColumn("Chln Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note")})
            ViewState("imp_mat") = dt2
            Me.BINDGRID4()
            Dim ds5 As New DataSet
            conn.Open()
            da = New SqlDataAdapter("select distinct PO_RCD_MAT.CRR_NO from PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_RCD_MAT.GARN_NO='PENDING' AND PO_RCD_MAT.MAT_RCD_QTY > PO_RCD_MAT.MAT_REJ_QTY AND PO_RCD_MAT.INSP_EMP IS NOT NULL AND ORDER_DETAILS.PO_TYPE='STORE MATERIAL(IMP)' ORDER BY CRR_NO", conn)
            da.Fill(ds5, "PO_RCD_MAT")
            imp_crr_no_DropDownList1.DataSource = ds5.Tables("PO_RCD_MAT")
            imp_crr_no_DropDownList1.DataValueField = "CRR_NO"
            imp_crr_no_DropDownList1.DataBind()
            imp_crr_no_DropDownList1.Items.Insert(0, "Select")
            imp_crr_no_DropDownList1.SelectedValue = "Select"
            ds5.Tables.Clear()
            Panel16.Visible = False
            MultiView1.ActiveViewIndex = 1
            imp_Panel1.Visible = True

        End If

    End Sub

    Protected Sub view_button_Click(sender As Object, e As EventArgs) Handles view_button.Click

        Dim MAT_SL_NO, PO_QUARY As New String("")
        conn.Open()
        Dim MC1 As New SqlCommand
        MC1.CommandText = "select MAT_SL_NO from MAT_DETAILS where ISSUE_NO='" & search_DropDown.SelectedValue & "'"
        MC1.Connection = conn
        dr = MC1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If IsDBNull(dr.Item("MAT_SL_NO")) Then
                PO_QUARY = "select PO_RCD_MAT . MAT_SLNO , MAX(PO_RCD_MAT .CRR_NO) AS CRR_NO ,MAX(PO_RCD_MAT .CRR_DATE) AS CRR_DATE ,MAX(ORDER_DETAILS .SO_NO) AS PO_NO,MAX(ORDER_DETAILS .SO_DATE) AS SO_DATE, MAX(ORDER_DETAILS .SO_ACTUAL) AS SO_ACTUAL ,MAX(ORDER_DETAILS .SO_ACTUAL_DATE) AS SO_ACTUAL_DATE ,MAX(PO_RCD_MAT .SUPL_ID) AS SUPL_ID ,MAX(PO_RCD_MAT .TRANS_WO_NO) AS TRANS_WO_NO ,MAX(PO_RCD_MAT .TRUCK_NO) AS TRUCK_NO ,MAX(PO_RCD_MAT .MAT_CODE) AS MAT_CODE , " &
                    "MAX(PO_RCD_MAT .CHLN_NO) AS CHLN_NO ,MAX(PO_RCD_MAT .CHLN_DATE) AS CHLN_DATE ,MAX(PO_RCD_MAT .MAT_CHALAN_QTY) AS MAT_CHALAN_QTY ,MAX(PO_RCD_MAT .MAT_RCD_QTY) AS MAT_RCD_QTY ,MAX(PO_RCD_MAT .MAT_REJ_QTY) AS MAT_REJ_QTY ,MAX(PO_RCD_MAT .MAT_BAL_QTY) AS MAT_BAL_QTY ,MAX(PO_RCD_MAT .MAT_EXCE) AS MAT_EXCE ,MAX(PO_RCD_MAT .RET_STATUS) RET_STATUS ,MAX(PO_RCD_MAT .GARN_DATE) AS GARN_DATE ,MAX(PO_RCD_MAT .CRR_EMP) AS CRR_EMP , " &
                    "MAX(PO_RCD_MAT .INSP_EMP ) AS INSP_EMP , MAX (PO_RCD_MAT .INSP_NOTE ) AS INSP_NOTE , MAX (PO_RCD_MAT .MAT_RATE) AS MAT_RATE , MAX (PO_RCD_MAT .GARN_NOTE ) AS GARN_NOTE , MAX (MATERIAL .MAT_NAME ) AS MAT_NAME , MAX (MATERIAL.MAT_AU ) AS MAT_AU , MAX (SUPL.SUPL_NAME ) AS SUPL_NAME , MAX (SUPL.SUPL_AT ) AS SUPL_AT , " &
                    "MAX (SUPL.SUPL_PO ) AS SUPL_PO , MAX (SUPL.SUPL_DIST ) AS SUPL_DIST , MAX (SUPL.SUPL_STATE ) AS SUPL_STATE , MAX (SUPL.SUPL_COUNTRY ) AS SUPL_COUNTRY , MAX (ORDER_DETAILS .SO_TYPE ) AS SO_TYPE , MAX (ORDER_DETAILS .PO_TYPE ) AS PO_TYPE , SUM (PO_ORD_MAT .MAT_QTY ) AS MAT_QTY , MAX (ORDER_DETAILS .PAYMENT_MODE ) AS PAYMENT_MODE , MAX (ORDER_DETAILS .MODE_OF_DESPATCH ) AS MODE_OF_DESPATCH , " &
                    "MAX (ORDER_DETAILS .INDENTOR ) AS INDENTOR , MAX (MAT_DETAILS .LINE_NO ) AS LINE_NO , MAX (MAT_DETAILS .MAT_BALANCE ) AS MAT_BALANCE , MAX (MAT_DETAILS .UNIT_PRICE ) AS UNIT_PRICE , MAX (PO_RCD_MAT .PROV_VALUE ) AS TOTAL_PRICE , MAX (PO_RCD_MAT .RET_USER ) AS RET_USER , MAX (PO_RCD_MAT .INSP_DATE ) AS INSP_DATE , MAX (ORDER_DETAILS .SO_ACTUAL_DATE ) AS SO_ACTUAL_DATE , MAX (PO_RCD_MAT .GARN_NO ) AS GARN_NO " &
                    "from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO  " &
                    "join MAT_DETAILS ON PO_RCD_MAT.GARN_NO=MAT_DETAILS.ISSUE_NO and MAT_DETAILS .MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .GARN_NO ='" & search_DropDown.SelectedValue & "'" &
                    " GROUP BY PO_RCD_MAT .MAT_SLNO ORDER BY PO_RCD_MAT.MAT_SLNO"
            Else
                'PO_QUARY = "select PO_RCD_MAT . MAT_SLNO , (PO_RCD_MAT .CRR_NO) AS CRR_NO ,(PO_RCD_MAT .CRR_DATE) AS CRR_DATE ,(PO_RCD_MAT .PO_NO) AS PO_NO ,(ORDER_DETAILS .SO_ACTUAL_DATE) AS SO_ACTUAL_DATE ,(PO_RCD_MAT .SUPL_ID) AS SUPL_ID ,(PO_RCD_MAT .TRANS_WO_NO) AS TRANS_WO_NO ,(PO_RCD_MAT .TRUCK_NO) AS TRUCK_NO ,(PO_RCD_MAT .MAT_CODE) AS MAT_CODE , 
                '    (PO_RCD_MAT .CHLN_NO) AS CHLN_NO ,(PO_RCD_MAT .CHLN_DATE) AS CHLN_DATE ,(PO_RCD_MAT .MAT_CHALAN_QTY) AS MAT_CHALAN_QTY ,(PO_RCD_MAT .MAT_RCD_QTY) AS MAT_RCD_QTY ,(PO_RCD_MAT .MAT_REJ_QTY) AS MAT_REJ_QTY ,(PO_RCD_MAT .MAT_BAL_QTY) AS MAT_BAL_QTY ,(PO_RCD_MAT .MAT_EXCE) AS MAT_EXCE ,(PO_RCD_MAT .RET_STATUS) RET_STATUS ,(PO_RCD_MAT .GARN_DATE) AS GARN_DATE ,(PO_RCD_MAT .CRR_EMP) AS CRR_EMP , 
                '    (PO_RCD_MAT .INSP_EMP ) AS INSP_EMP , (PO_RCD_MAT .INSP_NOTE ) AS INSP_NOTE , (PO_RCD_MAT .MAT_RATE) AS MAT_RATE , (PO_RCD_MAT .GARN_NOTE ) AS GARN_NOTE ,(MATERIAL .MAT_NAME ) AS MAT_NAME , (MATERIAL.MAT_AU ) AS MAT_AU , (SUPL.SUPL_NAME ) AS SUPL_NAME , (SUPL.SUPL_AT ) AS SUPL_AT , 
                '    (SUPL.SUPL_PO ) AS SUPL_PO , (SUPL.SUPL_DIST ) AS SUPL_DIST , (SUPL.SUPL_STATE ) AS SUPL_STATE , (SUPL.SUPL_COUNTRY ) AS SUPL_COUNTRY , (ORDER_DETAILS .SO_TYPE ) AS SO_TYPE , (ORDER_DETAILS .PO_TYPE ) AS PO_TYPE , PO_ORD_MAT .MAT_QTY AS MAT_QTY , (ORDER_DETAILS .PAYMENT_MODE ) AS PAYMENT_MODE , (ORDER_DETAILS .MODE_OF_DESPATCH ) AS MODE_OF_DESPATCH , 
                '    (ORDER_DETAILS .INDENTOR ) AS INDENTOR , (MAT_DETAILS .LINE_NO ) AS LINE_NO , (MAT_DETAILS .MAT_BALANCE ) AS MAT_BALANCE , (MAT_DETAILS .UNIT_PRICE ) AS UNIT_PRICE , (MAT_DETAILS .TOTAL_PRICE ) AS TOTAL_PRICE , (PO_RCD_MAT .RET_USER ) AS RET_USER , (PO_RCD_MAT .INSP_DATE ) AS INSP_DATE , (ORDER_DETAILS .SO_ACTUAL_DATE ) AS SO_ACTUAL_DATE , (PO_RCD_MAT .GARN_NO ) AS GARN_NO 
                '    from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO 
                '    LEFT join MAT_DETAILS ON PO_RCD_MAT.GARN_NO=MAT_DETAILS.ISSUE_NO and MAT_DETAILS .MAT_CODE =PO_RCD_MAT .MAT_CODE and MAT_DETAILS .MAT_SL_NO =PO_RCD_MAT .MAT_SLNO where PO_RCD_MAT .GARN_NO ='" & search_DropDown.SelectedValue & "' ORDER BY PO_RCD_MAT.MAT_SLNO"

                PO_QUARY = "DECLARE @TT TABLE(PO_NO VARCHAR(30),MAT_SLNO VARCHAR(30),MAT_QTY VARCHAR(30))
                    INSERT INTO @TT
                    SELECT MAX(PO_NO) AS PO_NO,MAX(MAT_SLNO) AS MAT_SLNO,SUM(MAT_QTY) AS MAT_QTY FROM PO_ORD_MAT WHERE PO_NO IN (SELECT PO_NO FROM PO_RCD_MAT WHERE GARN_NO='" & search_DropDown.SelectedValue & "') GROUP BY MAT_SLNO

                    select PO_RCD_MAT.UNIT_RATE AS PO_UNIT_RATE, PO_RCD_MAT . MAT_SLNO , (PO_RCD_MAT .CRR_NO) AS CRR_NO ,(PO_RCD_MAT .CRR_DATE) AS CRR_DATE ,(ORDER_DETAILS .SO_NO) AS PO_NO,(ORDER_DETAILS .SO_DATE) AS SO_DATE, (ORDER_DETAILS .SO_ACTUAL) AS SO_ACTUAL ,(ORDER_DETAILS .SO_ACTUAL_DATE) AS SO_ACTUAL_DATE ,(PO_RCD_MAT .SUPL_ID) AS SUPL_ID ,(PO_RCD_MAT .TRANS_WO_NO) AS TRANS_WO_NO ,(PO_RCD_MAT .TRUCK_NO) AS TRUCK_NO ,(PO_RCD_MAT .MAT_CODE) AS MAT_CODE , 
                    (PO_RCD_MAT .CHLN_NO) AS CHLN_NO ,(PO_RCD_MAT .CHLN_DATE) AS CHLN_DATE ,(PO_RCD_MAT .MAT_CHALAN_QTY) AS MAT_CHALAN_QTY ,(PO_RCD_MAT .MAT_RCD_QTY) AS MAT_RCD_QTY ,(PO_RCD_MAT .MAT_REJ_QTY) AS MAT_REJ_QTY ,(PO_RCD_MAT .MAT_BAL_QTY) AS MAT_BAL_QTY ,(PO_RCD_MAT .MAT_EXCE) AS MAT_EXCE ,(PO_RCD_MAT .RET_STATUS) RET_STATUS ,(PO_RCD_MAT .GARN_DATE) AS GARN_DATE ,(PO_RCD_MAT .CRR_EMP) AS CRR_EMP , 
                    (PO_RCD_MAT .INSP_EMP ) AS INSP_EMP , (PO_RCD_MAT .INSP_NOTE ) AS INSP_NOTE , (PO_RCD_MAT .MAT_RATE) AS MAT_RATE , (PO_RCD_MAT .GARN_NOTE ) AS GARN_NOTE ,(MATERIAL .MAT_NAME ) AS MAT_NAME , (MATERIAL.MAT_AU ) AS MAT_AU , (SUPL.SUPL_NAME ) AS SUPL_NAME , (SUPL.SUPL_AT ) AS SUPL_AT , 
                    (SUPL.SUPL_PO ) AS SUPL_PO , (SUPL.SUPL_DIST ) AS SUPL_DIST , (SUPL.SUPL_STATE ) AS SUPL_STATE , (SUPL.SUPL_COUNTRY ) AS SUPL_COUNTRY , (ORDER_DETAILS .SO_TYPE ) AS SO_TYPE , (ORDER_DETAILS .PO_TYPE ) AS PO_TYPE , T1 .MAT_QTY AS MAT_QTY , (ORDER_DETAILS .PAYMENT_MODE ) AS PAYMENT_MODE , (ORDER_DETAILS .MODE_OF_DESPATCH ) AS MODE_OF_DESPATCH , 
                    (ORDER_DETAILS .INDENTOR ) AS INDENTOR , (MAT_DETAILS .LINE_NO ) AS LINE_NO , (MAT_DETAILS .MAT_BALANCE ) AS MAT_BALANCE , (MAT_DETAILS .UNIT_PRICE ) AS UNIT_PRICE , (PO_RCD_MAT .PROV_VALUE ) AS TOTAL_PRICE , (PO_RCD_MAT .RET_USER ) AS RET_USER , (PO_RCD_MAT .INSP_DATE ) AS INSP_DATE , (ORDER_DETAILS .SO_ACTUAL_DATE ) AS SO_ACTUAL_DATE , (PO_RCD_MAT .GARN_NO ) AS GARN_NO 
                    from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join @TT T1 on PO_RCD_MAT .PO_NO =T1 .PO_NO and PO_RCD_MAT .MAT_SLNO =T1 .MAT_SLNO 
                    LEFT join MAT_DETAILS ON PO_RCD_MAT.GARN_NO=MAT_DETAILS.ISSUE_NO and MAT_DETAILS .MAT_CODE =PO_RCD_MAT .MAT_CODE and MAT_DETAILS .MAT_SL_NO =PO_RCD_MAT .MAT_SLNO where PO_RCD_MAT .GARN_NO ='" & search_DropDown.SelectedValue & "' ORDER BY PO_RCD_MAT.MAT_SLNO"
            End If

            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable



        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.Load(Server.MapPath("~/print_rpt/grn.rpt"))
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
        Dim url As String = "REPORT.aspx"
        Dim sb As New StringBuilder()
        sb.Append("<script type = 'text/javascript'>")
        sb.Append("window.open('")
        sb.Append(url)
        sb.Append("');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
        crystalReport.Close()
        crystalReport.Dispose()
    End Sub


    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MultiView1.ActiveViewIndex = -1
        Panel16.Visible = True
    End Sub

    Protected Sub imp_crr_no_DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles imp_crr_no_DropDownList1.SelectedIndexChanged
        If imp_crr_no_DropDownList1.SelectedValue = "Select" Then
            imp_crr_no_DropDownList1.Focus()
            Dim dt4 As New DataTable()
            dt4.Columns.AddRange(New DataColumn(13) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Ord Qty"), New DataColumn("Chln Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note"), New DataColumn("BE_NO")})
            ViewState("imp_mat") = dt4
            Me.BINDGRID4()
            Panel4.Visible = False
            Return

        Else

            conn.Open()
            da = New SqlDataAdapter("select PO_RCD_MAT.MAT_SLNO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .crr_no='" & imp_crr_no_DropDownList1.Text & "' and PO_RCD_MAT.GARN_NO = 'Pending' ORDER BY PO_RCD_MAT.MAT_SLNO", conn)
            da.Fill(dt)
            conn.Close()
            imp_mat_slno_DropDownList2.Items.Clear()
            imp_mat_slno_DropDownList2.DataSource = dt
            imp_mat_slno_DropDownList2.DataValueField = "MAT_SLNO"
            imp_mat_slno_DropDownList2.DataBind()
            imp_mat_slno_DropDownList2.Items.Insert(0, "Select")
            imp_mat_slno_DropDownList2.SelectedValue = "Select"
            ''grid view
            conn.Open()
            dt.Clear()
            ''da = New SqlDataAdapter("select PO_RCD_MAT .BE_NO, PO_RCD_MAT.PO_NO,PO_RCD_MAT.CRR_NO,PO_RCD_MAT.CHLN_NO,PO_RCD_MAT.MAT_SLNO,PO_RCD_MAT.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,PO_ORD_MAT.MAT_QTY,PO_RCD_MAT.MAT_CHALAN_QTY,PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_REJ_QTY,PO_RCD_MAT.MAT_EXCE,PO_RCD_MAT.MAT_BAL_QTY,PO_RCD_MAT.INSP_NOTE,PO_RCD_MAT.CRR_DATE,PO_RCD_MAT.AMD_NO,PO_RCD_MAT.TOTAL_MT from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .crr_no='" & imp_crr_no_DropDownList1.Text & "' and PO_RCD_MAT.GARN_NO = 'Pending' AND MAT_RCD_QTY <> MAT_REJ_QTY ORDER BY PO_RCD_MAT.MAT_SLNO", conn)
            da = New SqlDataAdapter("WITH group1 AS (
                    select PO_RCD_MAT .BE_NO, PO_RCD_MAT.PO_NO,PO_RCD_MAT.CRR_NO,PO_RCD_MAT.CHLN_NO,PO_RCD_MAT.MAT_SLNO,PO_RCD_MAT.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,PO_RCD_MAT.MAT_CHALAN_QTY,PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_REJ_QTY,PO_RCD_MAT.MAT_EXCE,PO_RCD_MAT.MAT_BAL_QTY,PO_RCD_MAT.INSP_NOTE,PO_RCD_MAT.CRR_DATE,PO_RCD_MAT.AMD_NO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .crr_no='" & imp_crr_no_DropDownList1.Text & "' and PO_RCD_MAT.GARN_NO = 'Pending' AND MAT_RCD_QTY <> MAT_REJ_QTY),
                    group2 AS (
                    SELECT PO_ORD_MAT.MAT_SLNO as g2SL_No,SUM(MAT_QTY) as MAT_QTY FROM PO_ORD_MAT join po_rcd_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno WHERE PO_RCD_MAT .crr_no='" & imp_crr_no_DropDownList1.Text & "' group by PO_ORD_MAT.MAT_SLNO)
                    SELECT * FROM group1 JOIN group2 ON group1.MAT_SLNO = group2.g2SL_No", conn)
            da.Fill(dt)
            conn.Close()
            imp_GridView3.DataSource = dt
            imp_GridView3.DataBind()
            conn.Close()

            conn.Open()
            Dim mc2 As New SqlCommand
            mc2.CommandText = "select distinct be_no  FROM PO_RCD_MAT WHERE crr_no='" & imp_crr_no_DropDownList1.SelectedValue & "'"
            mc2.Connection = conn
            dr = mc2.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label529.Text = dr.Item("be_no")
                dr.Close()
            End If
            conn.Close()

            conn.Open()
            mc2.CommandText = "SELECT SUPL .SUPL_NAME,SUPL.SUPL_ID ,PO_RCD_MAT .TRANS_SLNO, PO_RCD_MAT .PO_NO,PO_RCD_MAT.TRANS_WO_NO  FROM PO_RCD_MAT JOIN SUPL ON PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID WHERE PO_RCD_MAT.CRR_NO ='" & imp_crr_no_DropDownList1.SelectedValue & "'"
            mc2.Connection = conn
            dr = mc2.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label536.Text = dr.Item("po_no")
                Label13.Text = dr.Item("TRANS_WO_NO")
                Label14.Text = dr.Item("TRANS_SLNO")
                Label16.Text = dr.Item("SUPL_NAME")
                Label18.Text = dr.Item("SUPL_ID")
                dr.Close()
            End If
            conn.Close()


        End If

    End Sub

    Protected Sub imp_mat_slno_DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles imp_mat_slno_DropDownList2.SelectedIndexChanged
        If imp_mat_slno_DropDownList2.SelectedValue = "Select" Then
            imp_mat_slno_DropDownList2.Focus()
            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(9) {New DataColumn("MAT_NAME"), New DataColumn("SUPL_NAME"), New DataColumn("BE_NO"), New DataColumn("MAT_CODE"), New DataColumn("PO_NO"), New DataColumn("TRANS_WO_NO"), New DataColumn("BE_QTY"), New DataColumn("BE_BAL"), New DataColumn("amd_no"), New DataColumn("CHA_ORDER")})
            ViewState("IMP") = dt2
            BINDGRID3()
            Dim dt_x As DataTable = DirectCast(ViewState("IMP"), DataTable)
            dt_x.Rows.Add("", "", "", "", "", "", "", "", "", "")
            ViewState("IMP") = dt_x
            BINDGRID3()
            Panel4.Visible = False
            Return
        End If
        conn.Open()
        da = New SqlDataAdapter("select PO_RCD_MAT.amd_no,BE_DETAILS.CHA_ORDER, MATERIAL.MAT_NAME ,SUPL.SUPL_NAME ,BE_DETAILS .BE_NO ,PO_RCD_MAT .PO_NO , PO_RCD_MAT .MAT_CODE ,BE_DETAILS .BE_QTY,PO_RCD_MAT .TRANS_WO_NO ,(BE_DETAILS .BE_QTY -BE_DETAILS .RCVD_QTY ) as BE_BAL from PO_RCD_MAT join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join BE_DETAILS on PO_RCD_MAT .PO_NO = BE_DETAILS .PO_NO and PO_RCD_MAT .MAT_SLNO =BE_DETAILS .MAT_SLNO and PO_RCD_MAT .BE_NO =BE_DETAILS .BE_NO   join supl on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID  WHERE PO_RCD_MAT.CRR_NO ='" & imp_crr_no_DropDownList1.SelectedValue & "' AND PO_RCD_MAT .MAT_SLNO =" & imp_mat_slno_DropDownList2.SelectedValue, conn)
        da.Fill(dt)
        conn.Close()
        ViewState("IMP") = dt
        BINDGRID3()
        Dim mc As New SqlCommand
        conn.Open()
        mc.CommandText = "select AMD_NO from PO_RCD_MAT where CRR_NO ='" & imp_crr_no_DropDownList1.SelectedValue & "' AND MAT_SLNO=" & imp_mat_slno_DropDownList2.SelectedValue
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label538.Text = dr.Item("amd_no")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Panel4.Visible = True
    End Sub

    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        If imp_crr_no_DropDownList1.SelectedValue = "Select" Or imp_crr_no_DropDownList1.SelectedValue = "" Then
            imp_crr_no_DropDownList1.Focus()
            Return
        ElseIf imp_mat_slno_DropDownList2.SelectedValue = "Select" Or imp_mat_slno_DropDownList2.SelectedValue = "" Then
            imp_mat_slno_DropDownList2.Focus()
            Return
        End If

        '''''''''''''''''''''''''''''''''
        ''Checking GARN date and Freeze date
        Dim Block_DATE As String = ""
        conn.Open()
        Dim MC_new As New SqlCommand
        MC_new.CommandText = "SELECT Block_date FROM Date_Freeze"
        MC_new.Connection = conn
        dr = MC_new.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Block_DATE = dr.Item("Block_date")
            dr.Close()
        End If
        conn.Close()

        If (CDate(TextBox2.Text) <= CDate(Block_DATE)) Then
            GARN_ERR_LABLE.Visible = True
            GARN_ERR_LABLE.Text = "Garn before " & Block_DATE & " has been freezed."
            Button51.Enabled = False
        Else
            addAllForeignMaterialToGridview(imp_mat_slno_DropDownList2.Text)
        End If


    End Sub

    Protected Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        conn.Open()
        da = New SqlDataAdapter("select PO_RCD_MAT.MAT_SLNO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .crr_no='" & imp_crr_no_DropDownList1.Text & "' and PO_RCD_MAT.GARN_NO = 'Pending' ORDER BY PO_RCD_MAT.MAT_SLNO", conn)
        da.Fill(dt)
        conn.Close()

        For Each row As DataRow In dt.Rows

            addAllForeignMaterialToGridview(row.Item("MAT_SLNO"))

        Next row

    End Sub

    Public Sub addAllForeignMaterialToGridview(materialSlNo As String)


        Try
            Dim MC_new As New SqlCommand
            ''Checking GARN date and CRR date
            Dim CRR_DATE As String = ""
            conn.Open()
            'Dim MC_new As New SqlCommand
            MC_new.CommandText = "SELECT CRR_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & imp_crr_no_DropDownList1.SelectedValue & "'"
            MC_new.Connection = conn
            dr = MC_new.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                CRR_DATE = dr.Item("CRR_DATE")
                dr.Close()
            End If
            conn.Close()

            If (CRR_DATE > CDate(TextBox2.Text)) Then
                Label534.Visible = True
                Label534.Text = "GARN date cannot be before CRR date."
                Button51.Enabled = False
            Else
                'Label534.Visible = True
                Label534.Text = ""
                'ADD GRID VIEW
                Dim I As Integer = 0
                For I = 0 To imp_GridView3.Rows.Count - 1
                    If imp_GridView3.Rows(I).Cells(3).Text = materialSlNo Then
                        Dim mc1 As New SqlCommand
                        Dim TRANS_WO, TRANS_SLNO, cha_wo, cha_wo_sl As String
                        Dim MAT_RCVD_QTY As New Decimal(0)
                        TRANS_WO = ""
                        TRANS_SLNO = ""
                        cha_wo = ""
                        cha_wo_sl = ""
                        conn.Open()
                        mc1.CommandText = "select PO_RCD_MAT .TRANS_WO_NO ,PO_RCD_MAT .TRANS_SLNO ,BE_DETAILS .CHA_ORDER ,BE_DETAILS .CHA_SLNO  from PO_RCD_MAT join BE_DETAILS on PO_RCD_MAT .BE_NO =BE_DETAILS .BE_NO and PO_RCD_MAT .PO_NO =BE_DETAILS .PO_NO and PO_RCD_MAT .MAT_SLNO =BE_DETAILS .MAT_SLNO  where PO_RCD_MAT .CRR_NO  ='" & imp_GridView3.Rows(I).Cells(0).Text & "' AND PO_RCD_MAT.MAT_SLNO=" & imp_GridView3.Rows(I).Cells(3).Text
                        mc1.Connection = conn
                        dr = mc1.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            TRANS_SLNO = dr.Item("TRANS_SLNO")
                            TRANS_WO = dr.Item("TRANS_WO_NO")
                            cha_wo = dr.Item("CHA_ORDER")
                            cha_wo_sl = dr.Item("CHA_SLNO")
                            dr.Close()
                        Else
                            conn.Close()
                        End If
                        conn.Close()
                        Dim PARTY_QTY As Decimal = 0.0
                        Dim w_tolerance As Decimal = 0.0
                        Dim w_qty, w_complite, PRICE, final_price, ENTRY_TAX, w_unit_price, W_discount, DISCOUNT_VALUE, CUSTOM_DUTY, IGST, BE_QTY, STATUTORY_CHARGE, INSURANCE, BE_RCVD_QTY As New Decimal(0)
                        Dim w_name, w_au As String
                        Dim unit_value, trans_value, tole_value, CHA_VALUE, CHA_RCD, cenvat_value, mat_value, trans_short_value, loss_on_ed_value, wt_var_value As New Decimal(0)
                        cenvat_value = 0
                        mat_value = 0
                        trans_short_value = 0
                        loss_on_ed_value = 0
                        wt_var_value = 0
                        unit_value = 0
                        tole_value = 0
                        CHA_VALUE = 0.0
                        CHA_RCD = 0
                        ENTRY_TAX = 0
                        If TRANS_WO = "N/A" Then
                            imp_GridView3.Rows(I).Cells(12).Text = CDec(imp_GridView3.Rows(I).Cells(10).Text) - (CDec(imp_GridView3.Rows(I).Cells(11).Text) + CDec(imp_GridView3.Rows(I).Cells(13).Text))
                            Dim UNIT_PRICE, UNIT_CENVAT As Decimal
                            conn.Open()
                            mc1.CommandText = "SELECT * FROM BE_DETAILS JOIN PO_ORD_MAT ON BE_DETAILS .PO_NO =PO_ORD_MAT .PO_NO AND BE_DETAILS .MAT_SLNO =PO_ORD_MAT .MAT_SLNO WHERE BE_DETAILS .BE_NO ='" & imp_GridView3.Rows(I).Cells(24).Text & "' AND BE_DETAILS .PO_NO ='" & imp_GridView3.Rows(I).Cells(1).Text & "' AND BE_DETAILS .MAT_SLNO =" & imp_GridView3.Rows(I).Cells(3).Text
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
                                BE_RCVD_QTY = dr.Item("RCVD_QTY")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()

                            MAT_RCVD_QTY = CDec(imp_GridView3.Rows(I).Cells(12).Text)
                            ''Checking if quantity is exceeding the BE quantity

                            If (BE_QTY >= BE_RCVD_QTY + MAT_RCVD_QTY) Then
                                MAT_RCVD_QTY = CDec(imp_GridView3.Rows(I).Cells(12).Text)
                            Else
                                If ((BE_RCVD_QTY + MAT_RCVD_QTY - BE_QTY) > MAT_RCVD_QTY) Then
                                    MAT_RCVD_QTY = 0.00
                                Else
                                    MAT_RCVD_QTY = MAT_RCVD_QTY - (BE_RCVD_QTY + MAT_RCVD_QTY - BE_QTY)
                                End If

                            End If

                            unit_value = FormatNumber(UNIT_PRICE, 2)
                            mat_value = FormatNumber(((unit_value) * MAT_RCVD_QTY), 2)
                            cenvat_value = (UNIT_CENVAT * CDec(imp_GridView3.Rows(I).Cells(12).Text))
                            ''cha calculation
                            Dim CHA_PRICE, CHA_DISCOUNT, CHA_FINAL_PRICE As Decimal
                            CHA_PRICE = 0
                            CHA_DISCOUNT = 0
                            CHA_FINAL_PRICE = 0
                            Dim cha_name, cha_au As String
                            cha_name = ""
                            cha_au = ""
                            Dim MC As New SqlCommand
                            If cha_wo <> "N/A" Then

                                conn.Open()
                                MC.CommandText = "select sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & cha_wo & "' AND W_SLNO='" & cha_wo_sl & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & imp_crr_no_DropDownList1.SelectedValue & "')"
                                MC.Connection = conn
                                dr = MC.ExecuteReader
                                If dr.HasRows Then
                                    dr.Read()
                                    CHA_PRICE = dr.Item("W_UNIT_PRICE")
                                    CHA_DISCOUNT = dr.Item("W_DISCOUNT")
                                    cha_name = dr.Item("W_NAME")
                                    cha_au = dr.Item("W_AU")
                                    dr.Close()
                                Else
                                    conn.Close()
                                End If
                                conn.Close()
                                CHA_DISCOUNT = (CHA_PRICE * CHA_DISCOUNT) / 100

                                CHA_FINAL_PRICE = (CHA_PRICE - CHA_DISCOUNT) * MAT_RCVD_QTY
                                'CHA_FINAL_PRICE = (CHA_PRICE - CHA_DISCOUNT) * CDec(imp_GridView3.Rows(I).Cells(31).Text)
                                CHA_VALUE = FormatNumber(CHA_FINAL_PRICE, 2)
                                CHA_RCD = FormatNumber((CHA_PRICE - CHA_DISCOUNT) * CDec(imp_GridView3.Rows(I).Cells(12).Text), 2)
                            End If

                        Else
                            imp_GridView3.Rows(I).Cells(12).Text = CDec(imp_GridView3.Rows(I).Cells(10).Text) - (CDec(imp_GridView3.Rows(I).Cells(11).Text) + CDec(imp_GridView3.Rows(I).Cells(13).Text))
                            Dim UNIT_PRICE, UNIT_CENVAT As Decimal
                            conn.Open()
                            mc1.CommandText = "SELECT * FROM BE_DETAILS JOIN PO_ORD_MAT ON BE_DETAILS .PO_NO =PO_ORD_MAT .PO_NO AND BE_DETAILS .MAT_SLNO =PO_ORD_MAT .MAT_SLNO WHERE BE_DETAILS .BE_NO ='" & imp_GridView3.Rows(I).Cells(24).Text & "' AND BE_DETAILS .PO_NO ='" & imp_GridView3.Rows(I).Cells(1).Text & "' AND BE_DETAILS .MAT_SLNO =" & imp_GridView3.Rows(I).Cells(3).Text
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
                                BE_RCVD_QTY = dr.Item("RCVD_QTY")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()

                            MAT_RCVD_QTY = CDec(imp_GridView3.Rows(I).Cells(12).Text)
                            ''Checking if quantity is exceeding the BE quantity

                            If (BE_QTY >= BE_RCVD_QTY + MAT_RCVD_QTY) Then
                                MAT_RCVD_QTY = CDec(imp_GridView3.Rows(I).Cells(12).Text)
                            Else
                                If ((BE_RCVD_QTY + MAT_RCVD_QTY - BE_QTY) > MAT_RCVD_QTY) Then
                                    MAT_RCVD_QTY = 0.00
                                Else
                                    MAT_RCVD_QTY = MAT_RCVD_QTY - (BE_RCVD_QTY + MAT_RCVD_QTY - BE_QTY)
                                End If

                            End If

                            unit_value = FormatNumber(UNIT_PRICE, 2)
                            mat_value = FormatNumber(((unit_value) * MAT_RCVD_QTY), 2)
                            cenvat_value = (UNIT_CENVAT * CDec(imp_GridView3.Rows(I).Cells(12).Text))
                            Dim MC As New SqlCommand
                            Dim CHA_PRICE, CHA_DISCOUNT, CHA_FINAL_PRICE As Decimal
                            CHA_PRICE = 0
                            CHA_DISCOUNT = 0
                            CHA_FINAL_PRICE = 0
                            Dim cha_name, cha_au As String
                            cha_name = ""
                            cha_au = ""
                            If cha_wo <> "N/A" Then
                                conn.Open()
                                MC.CommandText = "select sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & cha_wo & "' AND W_SLNO='" & cha_wo_sl & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & imp_crr_no_DropDownList1.SelectedValue & "')"
                                MC.Connection = conn
                                dr = MC.ExecuteReader
                                If dr.HasRows Then
                                    dr.Read()
                                    CHA_PRICE = dr.Item("W_UNIT_PRICE")
                                    CHA_DISCOUNT = dr.Item("W_DISCOUNT")
                                    cha_name = dr.Item("W_NAME")
                                    cha_au = dr.Item("W_AU")
                                    dr.Close()
                                Else
                                    conn.Close()
                                End If
                                conn.Close()

                                CHA_DISCOUNT = (CHA_PRICE * CHA_DISCOUNT) / 100
                                CHA_FINAL_PRICE = (CHA_PRICE - CHA_DISCOUNT) * MAT_RCVD_QTY
                                'CHA_FINAL_PRICE = (CHA_PRICE - CHA_DISCOUNT) * CDec(imp_GridView3.Rows(I).Cells(31).Text)
                                CHA_VALUE = FormatNumber(CHA_FINAL_PRICE, 2)
                                CHA_RCD = FormatNumber((CHA_PRICE - CHA_DISCOUNT) * CDec(imp_GridView3.Rows(I).Cells(12).Text), 2)

                            End If

                            conn.Open()
                            MC.CommandText = "select sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & TRANS_WO & "' AND W_SLNO='" & TRANS_SLNO & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & imp_crr_no_DropDownList1.SelectedValue & "')"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                w_qty = dr.Item("W_QTY")
                                w_complite = dr.Item("W_COMPLITED")
                                w_tolerance = dr.Item("W_TOLERANCE")
                                w_unit_price = dr.Item("W_UNIT_PRICE")
                                W_discount = dr.Item("W_DISCOUNT")
                                w_name = dr.Item("W_NAME")
                                w_au = dr.Item("W_AU")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()
                            ''BAG WEIGHT CALCULATION
                            Dim BAG_WEIGHT, NO_OF_BAG As Decimal
                            conn.Open()
                            MC.CommandText = "SELECT NO_OF_BAG,BAG_WEIGHT FROM PO_RCD_MAT WHERE CRR_NO = '" & imp_GridView3.Rows(I).Cells(0).Text & "' AND MAT_SLNO=" & imp_GridView3.Rows(I).Cells(3).Text
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                BAG_WEIGHT = dr.Item("BAG_WEIGHT")
                                NO_OF_BAG = dr.Item("NO_OF_BAG")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()
                            Dim b_weight As Decimal = 0
                            If BAG_WEIGHT * NO_OF_BAG > 0 Then
                                b_weight = (BAG_WEIGHT * NO_OF_BAG) / 1000
                            ElseIf BAG_WEIGHT * NO_OF_BAG <= 0 Then
                                b_weight = 0
                            End If

                            ''trans tolerance
                            Dim tole_weight, CHLN_QTY, TRANS_DIFF, TRANS_QTY As New Decimal(0)
                            CHLN_QTY = imp_GridView3.Rows(I).Cells(9).Text.Trim
                            tole_weight = (CHLN_QTY * w_tolerance) / 100
                            TRANS_DIFF = CHLN_QTY - CDec(imp_GridView3.Rows(I).Cells(10).Text.Trim)

                            If tole_weight <= TRANS_DIFF Then
                                loss_on_ed_value = FormatNumber(tole_weight * UNIT_CENVAT, 2)
                                wt_var_value = FormatNumber(tole_weight * (((mat_value) - cenvat_value) / CDec(imp_GridView3.Rows(I).Cells(12).Text.Trim)), 3)
                                'trans_short_value = FormatNumber((TRANS_DIFF - tole_weight) * ((mat_value + CHA_RCD) / CDec(imp_GridView3.Rows(I).Cells(12).Text.Trim)), 2)
                                trans_short_value = FormatNumber((TRANS_DIFF) * ((mat_value + CHA_RCD) / CDec(imp_GridView3.Rows(I).Cells(12).Text.Trim)), 2)
                                TRANS_QTY = CDec(imp_GridView3.Rows(I).Cells(31).Text)
                            Else
                                loss_on_ed_value = FormatNumber(TRANS_DIFF * UNIT_CENVAT, 2)
                                wt_var_value = FormatNumber(TRANS_DIFF * ((mat_value - cenvat_value) / CDec(imp_GridView3.Rows(I).Cells(12).Text.Trim)), 3)
                                trans_short_value = FormatNumber(0.0, 2)
                                'TRANS_QTY = CHLN_QTY
                                TRANS_QTY = CDec(imp_GridView3.Rows(I).Cells(31).Text)
                            End If

                            ''trans freight calculation
                            PRICE = w_unit_price
                            DISCOUNT_VALUE = (PRICE * W_discount) / 100
                            PRICE = (PRICE - DISCOUNT_VALUE)
                            final_price = PRICE * (TRANS_QTY + ((BAG_WEIGHT * NO_OF_BAG) / 1000))
                            final_price = FormatNumber(final_price, 2)
                            trans_value = FormatNumber(final_price, 2)

                        End If
                        imp_GridView3.Rows(I).Cells(15).Text = FormatNumber(unit_value, 2)
                        imp_GridView3.Rows(I).Cells(16).Text = FormatNumber(cenvat_value, 2)
                        imp_GridView3.Rows(I).Cells(17).Text = FormatNumber(mat_value, 2)
                        imp_GridView3.Rows(I).Cells(18).Text = FormatNumber(CHA_VALUE, 2)
                        imp_GridView3.Rows(I).Cells(19).Text = FormatNumber(trans_value - trans_short_value, 2)
                        imp_GridView3.Rows(I).Cells(20).Text = FormatNumber(trans_short_value, 2)
                        imp_GridView3.Rows(I).Cells(21).Text = FormatNumber(loss_on_ed_value, 2)
                        imp_GridView3.Rows(I).Cells(22).Text = FormatNumber(wt_var_value, 2)
                        imp_GridView3.Rows(I).Cells(25).Text = FormatNumber(CDec(imp_GridView3.Rows(I).Cells(17).Text) + CDec(imp_GridView3.Rows(I).Cells(16).Text) - (CDec(imp_GridView3.Rows(I).Cells(18).Text)))
                        imp_GridView3.Rows(I).Cells(26).Text = FormatNumber((CUSTOM_DUTY / BE_QTY) * MAT_RCVD_QTY, 2)
                        imp_GridView3.Rows(I).Cells(27).Text = FormatNumber((IGST / BE_QTY) * MAT_RCVD_QTY, 2)
                        imp_GridView3.Rows(I).Cells(28).Text = FormatNumber((STATUTORY_CHARGE / BE_QTY) * MAT_RCVD_QTY, 2)
                        imp_GridView3.Rows(I).Cells(29).Text = FormatNumber((INSURANCE / BE_QTY) * MAT_RCVD_QTY, 2)
                        If (BE_QTY >= BE_RCVD_QTY + CDec(imp_GridView3.Rows(I).Cells(12).Text)) Then
                            imp_GridView3.Rows(I).Cells(30).Text = "0.00"
                        Else
                            imp_GridView3.Rows(I).Cells(30).Text = FormatNumber(BE_RCVD_QTY + CDec(imp_GridView3.Rows(I).Cells(12).Text) - BE_QTY, 2)
                        End If
                    End If
                Next
                Button51.Enabled = True

            End If
        Catch ee As Exception

            conn.Close()
            conn_trans.Close()
            Label534.Visible = True
            Label534.Text = "There was some Error, please contact EDP."
            imp_garn_no_TextBox1.Text = ""
        Finally
            conn.Close()
            conn_trans.Close()
        End Try



    End Sub

    Protected Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Dim working_date As Date
        Dim BAG_WEIGHT, NO_OF_BAG As Decimal
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        End If
        working_date = CDate(TextBox2.Text)

        If imp_GridView3.Rows.Count = 0 Then
            Label534.Visible = True
            Label534.Text = "No Data Added"
            Return
        End If
        If TextBox1.Text = "" Or IsNumeric(TextBox1.Text) = False Then
            TextBox1.Focus()
            Label534.Visible = True
            Label534.Text = "Add Integer Value In Transporter Penalty or 0"
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

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                ''GARN TYPE
                Dim CRR_TYPE As String = ""
                If type_DropDown.SelectedValue = "STORE MATERIAL" Then
                    CRR_TYPE = "SGARN"
                ElseIf type_DropDown.SelectedValue = "RAW MATERIAL" Then
                    CRR_TYPE = "RGARN"
                ElseIf type_DropDown.SelectedValue = "RAW MATERIAL(IMP)" Then
                    CRR_TYPE = "RGARN"
                ElseIf type_DropDown.SelectedValue = "STORE MATERIAL(IMP)" Then
                    CRR_TYPE = "SGARN"
                End If
                count = 0
                conn.Open()
                da = New SqlDataAdapter("select distinct GARN_NO from PO_RCD_MAT WHERE GARN_NO<>'PENDING' AND GARN_NO LIKE '" & CRR_TYPE + STR1 & "%'", conn)
                count = da.Fill(ds, "PO_RCD_MAT")
                conn.Close()
                If count = 0 Then
                    imp_garn_no_TextBox1.Text = CRR_TYPE & STR1 & "000001"

                Else
                    str = count + 1
                    If str.Length = 1 Then
                        str = "00000" & (count + 1)
                    ElseIf str.Length = 2 Then
                        str = "0000" & (count + 1)
                    ElseIf str.Length = 3 Then
                        str = "000" & (count + 1)
                    ElseIf str.Length = 4 Then
                        str = "00" & (count + 1)
                    ElseIf str.Length = 5 Then
                        str = "0" & (count + 1)
                    End If
                    imp_garn_no_TextBox1.Text = CRR_TYPE & STR1 & str

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
                Dim I As Integer
                For I = 0 To imp_GridView3.Rows.Count - 1
                    If IsNumeric(imp_GridView3.Rows(I).Cells(17).Text) = False Then
                        Label534.Visible = True
                        Label534.Text = "Please Add Material SlNo Wise Details"
                        imp_garn_no_TextBox1.Text = ""
                        Return
                    Else
                        Label534.Visible = False
                    End If
                Next
                Dim po_no As String = ""
                conn.Open()
                mycommand.CommandText = "select distinct po_no from PO_RCD_MAT where CRR_NO ='" & imp_crr_no_DropDownList1.SelectedValue & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    po_no = dr.Item("PO_NO")
                    dr.Close()
                Else
                    conn.Close()
                End If
                conn.Close()

                Dim MAT_RCVD_QTY, PURCHASE_VALUE, IGST, CUSTOM_DUTY, STAT_CHARGES, INSURANCE, PARTY_VALUE, BE_QTY, BE_RCVD_QTY As New Decimal(0)

                For I = 0 To imp_GridView3.Rows.Count - 1

                    MAT_RCVD_QTY = CDec(imp_GridView3.Rows(I).Cells(12).Text)
                    ''Checking if quantity is exceeding the BE quantity
                    Dim mc1 As New SqlCommand
                    conn.Open()
                    mc1.CommandText = "SELECT * FROM BE_DETAILS with(NOLOCK) JOIN PO_ORD_MAT with(NOLOCK) ON BE_DETAILS .PO_NO =PO_ORD_MAT .PO_NO AND BE_DETAILS .MAT_SLNO =PO_ORD_MAT .MAT_SLNO WHERE BE_DETAILS .BE_NO ='" & imp_GridView3.Rows(I).Cells(24).Text & "' AND BE_DETAILS .PO_NO ='" & imp_GridView3.Rows(I).Cells(1).Text & "' AND BE_DETAILS .MAT_SLNO =" & imp_GridView3.Rows(I).Cells(3).Text
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()

                        BE_QTY = dr.Item("BE_QTY")
                        BE_RCVD_QTY = dr.Item("RCVD_QTY")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If

                    If (BE_QTY >= BE_RCVD_QTY + MAT_RCVD_QTY) Then
                        MAT_RCVD_QTY = CDec(imp_GridView3.Rows(I).Cells(12).Text)
                    Else
                        If ((BE_RCVD_QTY + MAT_RCVD_QTY - BE_QTY) > MAT_RCVD_QTY) Then
                            MAT_RCVD_QTY = 0.00
                        Else
                            MAT_RCVD_QTY = MAT_RCVD_QTY - (BE_RCVD_QTY + MAT_RCVD_QTY - BE_QTY)
                        End If

                    End If

                    PURCHASE_VALUE = CDec(imp_GridView3.Rows(I).Cells(17).Text) + CDec(imp_GridView3.Rows(I).Cells(26).Text) + CDec(imp_GridView3.Rows(I).Cells(28).Text) + CDec(imp_GridView3.Rows(I).Cells(29).Text)
                    IGST = CDec(imp_GridView3.Rows(I).Cells(27).Text)
                    CUSTOM_DUTY = CDec(imp_GridView3.Rows(I).Cells(26).Text)
                    STAT_CHARGES = CDec(imp_GridView3.Rows(I).Cells(28).Text)
                    INSURANCE = CDec(imp_GridView3.Rows(I).Cells(29).Text)
                    PARTY_VALUE = CDec(imp_GridView3.Rows(I).Cells(17).Text)

                    Dim INV_NO, INV_DATE As String
                    INV_DATE = ""
                    INV_NO = ""
                    conn.Open()
                    mycommandNew1.CommandText = "SELECT BE_DETAILS .BE_NO ,BE_DETAILS .INV_NO ,BE_DETAILS .INV_DATE FROM BE_DETAILS with(NOLOCK) JOIN PO_RCD_MAT with(NOLOCK) ON BE_DETAILS .BE_NO =PO_RCD_MAT .BE_NO AND BE_DETAILS .MAT_SLNO =PO_RCD_MAT .MAT_SLNO WHERE PO_RCD_MAT .CRR_NO ='" & imp_GridView3.Rows(I).Cells(0).Text & "'AND PO_RCD_MAT .MAT_SLNO =" & imp_GridView3.Rows(I).Cells(3).Text
                    mycommandNew1.Connection = conn
                    dr = mycommandNew1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        INV_NO = dr.Item("INV_NO")
                        INV_DATE = dr.Item("INV_DATE")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                    Dim PROV_MAT_VALUE As Decimal = 0
                    PROV_MAT_VALUE = CDec(imp_GridView3.Rows(I).Cells(17).Text)
                    Dim G_DATE As Date = CDate(TextBox2.Text)
                    'conn.Open()
                    Dim query1 As String = "update PO_RCD_MAT set TRANS_CHARGE=@TRANS_CHARGE, GARN_NO=@GARN_NO,GARN_NOTE=@GARN_NOTE,PROV_VALUE=@PROV_VALUE,GARN_DATE=@GARN_DATE,GARN_EMP=@GARN_EMP,TRANS_SHORT=@TRANS_SHORT,UNIT_RATE=@UNIT_RATE, MAT_RATE=@MAT_RATE,DISC_VAL=@DISC_VAL,PF_VALUE=@PF_VALUE,IGST=@IGST,SGST=@SGST,CGST=@CGST,CESS=@CESS,LD_CHARGE=@LD_CHARGE,PENALITY_CHARGE=@PENALITY_CHARGE,FREIGHT_CHARGE=@FREIGHT_CHARGE,LOCAL_FREIGHT=@LOCAL_FREIGHT,LOSS_TRANSPORT=@LOSS_TRANSPORT,LOSS_ON_ED=@LOSS_ON_ED,PARTY_PAY=@PARTY_PAY,DIFF_VALUE=@DIFF_VALUE,V_IND=@V_IND,INV_NO=@INV_NO,INV_DATE=@INV_DATE, PAY_EMP=@PAY_EMP,GARN_ENTRY_DATE=@GARN_ENTRY_DATE where MAT_SLNO='" & imp_GridView3.Rows(I).Cells(3).Text & "' and CRR_NO='" & imp_GridView3.Rows(I).Cells(0).Text & "' AND PO_NO='" & po_no & "'"
                    Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@UNIT_RATE", CDec(imp_GridView3.Rows(I).Cells(15).Text))
                    cmd1.Parameters.AddWithValue("@MAT_RATE", CDec(imp_GridView3.Rows(I).Cells(17).Text))
                    cmd1.Parameters.AddWithValue("@DISC_VAL", 0)
                    cmd1.Parameters.AddWithValue("@PF_VALUE", 0)
                    cmd1.Parameters.AddWithValue("@IGST", CDec(imp_GridView3.Rows(I).Cells(27).Text))
                    cmd1.Parameters.AddWithValue("@CESS", 0)
                    cmd1.Parameters.AddWithValue("@SGST", 0)
                    cmd1.Parameters.AddWithValue("@CGST", 0)
                    cmd1.Parameters.AddWithValue("@LD_CHARGE", 0)
                    cmd1.Parameters.AddWithValue("@PENALITY_CHARGE", 0)
                    cmd1.Parameters.AddWithValue("@FREIGHT_CHARGE", 0)
                    cmd1.Parameters.AddWithValue("@LOCAL_FREIGHT", 0)
                    cmd1.Parameters.AddWithValue("@LOSS_TRANSPORT", CDec(imp_GridView3.Rows(I).Cells(22).Text))
                    cmd1.Parameters.AddWithValue("@LOSS_ON_ED", CDec(imp_GridView3.Rows(I).Cells(21).Text))
                    cmd1.Parameters.AddWithValue("@PARTY_PAY", CDec(imp_GridView3.Rows(I).Cells(17).Text))
                    cmd1.Parameters.AddWithValue("@DIFF_VALUE", 0)
                    cmd1.Parameters.AddWithValue("@V_IND", "V")
                    cmd1.Parameters.AddWithValue("@INV_NO", INV_NO)
                    cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(INV_DATE, "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@PAY_EMP", Session("userName"))
                    cmd1.Parameters.AddWithValue("@GARN_NO", imp_garn_no_TextBox1.Text)
                    cmd1.Parameters.AddWithValue("@GARN_DATE", Date.ParseExact(G_DATE, "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@PROV_VALUE", Math.Round((CDec(imp_GridView3.Rows(I).Cells(17).Text) - CDec(imp_GridView3.Rows(I).Cells(19).Text)), 2))
                    cmd1.Parameters.AddWithValue("@GARN_NOTE", TextBox7.Text)
                    cmd1.Parameters.AddWithValue("@TRANS_SHORT", CDec(imp_GridView3.Rows(I).Cells(20).Text))
                    cmd1.Parameters.AddWithValue("@TRANS_CHARGE", CDec(imp_GridView3.Rows(I).Cells(19).Text))
                    cmd1.Parameters.AddWithValue("@GARN_EMP", Session("userName"))
                    cmd1.Parameters.AddWithValue("@GARN_ENTRY_DATE", Now)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()
                    'conn.Close()
                    If CDec(imp_GridView3.Rows(I).Cells(14).Text) <= 0 Then
                        str = "CLEAR"
                    Else
                        str = "PENDING"
                    End If
                    Dim RCVD_QTY_FOR As Decimal = 0
                    Dim mc As New SqlCommand
                    Dim TRANS_WO, TRANS_SLNO As String
                    TRANS_WO = ""
                    TRANS_SLNO = ""
                    conn.Open()
                    mc.CommandText = "select TRANS_WO_NO ,TRANS_SLNO from PO_RCD_MAT WITH(NOLOCK) where CRR_NO ='" & imp_GridView3.Rows(I).Cells(0).Text & "' and MAT_SLNO=" & imp_GridView3.Rows(I).Cells(3).Text
                    mc.Connection = conn
                    dr = mc.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TRANS_SLNO = dr.Item("TRANS_SLNO")
                        TRANS_WO = dr.Item("TRANS_WO_NO")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()

                    If TRANS_WO = "N/A" Then
                        RCVD_QTY_FOR = CDec(imp_GridView3.Rows(I).Cells(12).Text)
                    Else
                        RCVD_QTY_FOR = CDec(imp_GridView3.Rows(I).Cells(9).Text) - CDec(imp_GridView3.Rows(I).Cells(11).Text)
                    End If
                    'conn.Open()
                    mycommand = New SqlCommand("update BE_DETAILS set RCVD_QTY=RCVD_QTY + " & RCVD_QTY_FOR & "  WHERE MAT_SLNO='" & imp_GridView3.Rows(I).Cells(3).Text & "' and PO_NO='" & imp_GridView3.Rows(I).Cells(1).Text & "' AND BE_NO='" & imp_GridView3.Rows(I).Cells(24).Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()
                    'conn.Close()
                    ''UPDATE MATERIAL STOCK AND AVG PRICEING
                    Dim STOCK_QTY, AVG_PRICE As Decimal
                    conn.Open()
                    mc.CommandText = "select * from MATERIAL with(NOLOCK) where MAT_CODE = '" & imp_GridView3.Rows(I).Cells(4).Text & "'"
                    mc.Connection = conn
                    dr = mc.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        STOCK_QTY = dr.Item("MAT_STOCK")
                        AVG_PRICE = dr.Item("MAT_AVG")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If

                    Dim NEW_AVG_PRICE, NEW_UNIT_RATE, NEW_MAT_VALUE As Decimal
                    NEW_MAT_VALUE = PROV_MAT_VALUE + CUSTOM_DUTY + STAT_CHARGES + INSURANCE + CDec(imp_GridView3.Rows(I).Cells(18).Text) + CDec(imp_GridView3.Rows(I).Cells(19).Text)
                    NEW_UNIT_RATE = CDec(FormatNumber(NEW_MAT_VALUE / CDec(imp_GridView3.Rows(I).Cells(12).Text), 3))
                    NEW_AVG_PRICE = FormatNumber(((STOCK_QTY * AVG_PRICE) + NEW_MAT_VALUE) / (CDec(imp_GridView3.Rows(I).Cells(12).Text) + STOCK_QTY), 3)
                    'conn.Open()
                    Dim Query As String
                    Dim CMD As New SqlCommand
                    Query = "update MATERIAL set MAT_AVG=@MAT_AVG,LAST_TRANS_DATE=@LAST_TRANS_DATE,MAT_STOCK=@MAT_STOCK,MAT_LAST_RATE=@MAT_LAST_RATE,MAT_LASTPUR_DATE=@MAT_LASTPUR_DATE where MAT_CODE='" & imp_GridView3.Rows(I).Cells(4).Text & "'"
                    CMD = New SqlCommand(Query, conn_trans, myTrans)
                    CMD.Parameters.AddWithValue("@MAT_AVG", NEW_AVG_PRICE)
                    CMD.Parameters.AddWithValue("@MAT_STOCK", CDec(imp_GridView3.Rows(I).Cells(12).Text) + CDec(imp_GridView3.Rows(I).Cells(13).Text) + STOCK_QTY)
                    CMD.Parameters.AddWithValue("@MAT_LAST_RATE", NEW_UNIT_RATE)
                    CMD.Parameters.AddWithValue("@MAT_LASTPUR_DATE", G_DATE)
                    CMD.Parameters.AddWithValue("@LAST_TRANS_DATE", G_DATE)
                    CMD.ExecuteReader()
                    CMD.Dispose()
                    'conn.Close()
                    ''LINE BALANCING

                    count = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS with(NOLOCK) WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & imp_GridView3.Rows(I).Cells(4).Text & "' AND LINE_NO <> 0", conn)
                    count = da.Fill(ds, "MAT_DETAILS")
                    conn.Close()

                    'conn.Open()
                    Query = "Insert Into MAT_DETAILS(MAT_SL_NO,ENTRY_DATE,AVG_PRICE,ISSUE_NO,LINE_NO,LINE_DATE,FISCAL_YEAR,LINE_TYPE,MAT_CODE,MAT_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,QTR,ISSUE_TYPE,COST_CODE,ISSUE_QTY)VALUES(@MAT_SL_NO,@ENTRY_DATE,@AVG_PRICE,@ISSUE_NO,@LINE_NO,@LINE_DATE,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@MAT_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@QTR,@ISSUE_TYPE,@COST_CODE,@ISSUE_QTY)"
                    CMD = New SqlCommand(Query, conn_trans, myTrans)
                    CMD.Parameters.AddWithValue("@ISSUE_NO", imp_garn_no_TextBox1.Text)
                    CMD.Parameters.AddWithValue("@LINE_NO", count + 1)
                    CMD.Parameters.AddWithValue("@ISSUE_TYPE", "PURCHASE")
                    CMD.Parameters.AddWithValue("@LINE_DATE", G_DATE)
                    CMD.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
                    CMD.Parameters.AddWithValue("@LINE_TYPE", "R")
                    CMD.Parameters.AddWithValue("@MAT_CODE", imp_GridView3.Rows(I).Cells(4).Text)
                    CMD.Parameters.AddWithValue("@MAT_QTY", MAT_RCVD_QTY + CDec(imp_GridView3.Rows(I).Cells(13).Text))
                    CMD.Parameters.AddWithValue("@MAT_BALANCE", MAT_RCVD_QTY + CDec(imp_GridView3.Rows(I).Cells(13).Text) + STOCK_QTY)
                    CMD.Parameters.AddWithValue("@UNIT_PRICE", NEW_UNIT_RATE)
                    CMD.Parameters.AddWithValue("@TOTAL_PRICE", NEW_MAT_VALUE)
                    CMD.Parameters.AddWithValue("@COST_CODE", imp_GridView3.Rows(I).Cells(1).Text)
                    CMD.Parameters.AddWithValue("@ISSUE_QTY", 0)
                    CMD.Parameters.AddWithValue("@QTR", qtr1)
                    CMD.Parameters.AddWithValue("@AVG_PRICE", NEW_AVG_PRICE)
                    CMD.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    CMD.Parameters.AddWithValue("@MAT_SL_NO", imp_GridView3.Rows(I).Cells(3).Text)
                    CMD.ExecuteReader()
                    CMD.Dispose()
                    'conn.Close()
                    ''LEDGER POSTING PURCHASE
                    Dim PURCHASE As String = ""
                    conn.Open()
                    Dim MCc As New SqlCommand
                    MCc.CommandText = "select AC_PUR from MATERIAL WITH(NOLOCK) where MAT_CODE = '" & imp_GridView3.Rows(I).Cells(4).Text & "'"
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
                    Dim SIT_HEAD As String = ""
                    conn.Open()
                    mycommandNew.CommandText = "select * from work_group with(NOLOCK) where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & po_no & "')"
                    mycommandNew.Connection = conn
                    dr = mycommandNew.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        SIT_HEAD = dr.Item("PROV_HEAD")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()

                    ''SAVE LEDGER PURCHASE
                    LEDGER_SAVE_PUR(po_no, imp_GridView3.Rows(I).Cells(3).Text, Label18.Text, imp_garn_no_TextBox1.Text, PURCHASE, "Dr", FormatNumber(PURCHASE_VALUE, 2), "PUR", 1, "", imp_GridView3.Rows(0).Cells(24).Text)
                    'sit credit
                    LEDGER_SAVE_PUR(po_no, imp_GridView3.Rows(I).Cells(3).Text, Label18.Text, imp_garn_no_TextBox1.Text, SIT_HEAD, "Cr", FormatNumber(PARTY_VALUE, 2), "PROV. CRED. FOR RM(FOR.)", 3, "", imp_GridView3.Rows(0).Cells(24).Text)
                    'CUSTOM DUTY credit
                    LEDGER_SAVE_PUR(po_no, imp_GridView3.Rows(I).Cells(3).Text, Label18.Text, imp_garn_no_TextBox1.Text, "86201", "Cr", FormatNumber(CUSTOM_DUTY + IGST, 2), "CUSTOM DUTY", 4, "", imp_GridView3.Rows(0).Cells(24).Text)
                    'STATUTORY CHARGES credit
                    LEDGER_SAVE_PUR(po_no, imp_GridView3.Rows(I).Cells(3).Text, Label18.Text, imp_garn_no_TextBox1.Text, "51746", "Cr", FormatNumber(STAT_CHARGES, 2), "STATUTORY CHARGES", 5, "", imp_GridView3.Rows(0).Cells(24).Text)
                    'INSURANCE credit
                    LEDGER_SAVE_PUR(po_no, imp_GridView3.Rows(I).Cells(3).Text, Label18.Text, imp_garn_no_TextBox1.Text, "51207", "Cr", FormatNumber(INSURANCE, 2), "INSURANCE", 6, "", imp_GridView3.Rows(0).Cells(24).Text)


                    ''INSERTING DATA INTO TAXABLE VALUES TABLE
                    Dim taxable_value, IGST_PERCENTAGE As New Decimal(0)
                    Dim BE_INVOICE_NO As New String("")
                    conn.Open()
                    Dim MC55 As New SqlCommand
                    MC55.CommandText = "SELECT * FROM BE_DETAILS WITH(NOLOCK) WHERE BE_NO = '" & Label529.Text & "'"
                    MC55.Connection = conn
                    dr = MC55.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        taxable_value = Math.Round(((CDec(dr.Item("PARTY_AMT")) + CDec(dr.Item("TOTAL_CUSTOM_DUTY")) + CDec(dr.Item("INSURANCE"))) / CDec(dr.Item("BE_QTY"))) * CDec(imp_GridView3.Rows(I).Cells(12).Text) + CDec(imp_GridView3.Rows(I).Cells(13).Text), 2)
                        IGST_PERCENTAGE = CDec(dr.Item("IGST_PERCENTAGE"))
                        BE_INVOICE_NO = dr.Item("INV_NO")
                        dr.Close()
                    Else
                        'conn.Close()
                    End If
                    conn.Close()

                    ''INSERT DATA INTO TAXABLE VALUES TABLE
                    query1 = "INSERT INTO Taxable_Values (INVOICE_NO,INVOICE_DATE,ENTRY_DATE,RCM_CGST_AMT,RCM_SGST_AMT,RCM_IGST_AMT,RCM_CESS_AMT,GARN_CRR_MB_NO,VALUATION_DATE,DATA_TYPE,SL_NO,SUPL_CODE,SUPL_NAME,TAXABLE_VALUE,FISCAL_YEAR,CGST_PERCENTAGE,SGST_PERCENTAGE,IGST_PERCENTAGE,CESS_PERCENTAGE,CGST_AMT,SGST_AMT,IGST_AMT,CESS_AMT,TAXABLE_LD_PENALTY,CGST_LD_PENALTY,SGST_LD_PENALTY,IGST_LD_PENALTY,CESS_LD_PENALTY,CGST_TDS,SGST_TDS,IGST_TDS,CESS_TDS)VALUES(@INVOICE_NO,@INVOICE_DATE,@ENTRY_DATE,@RCM_CGST_AMT,@RCM_SGST_AMT,@RCM_IGST_AMT,@RCM_CESS_AMT,@GARN_CRR_MB_NO,@VALUATION_DATE,@DATA_TYPE,@SL_NO,@SUPL_CODE,@SUPL_NAME,@TAXABLE_VALUE,@FISCAL_YEAR,@CGST_PERCENTAGE,@SGST_PERCENTAGE,@IGST_PERCENTAGE,@CESS_PERCENTAGE,@CGST_AMT,@SGST_AMT,@IGST_AMT,@CESS_AMT,@TAXABLE_LD_PENALTY,@CGST_LD_PENALTY,@SGST_LD_PENALTY,@IGST_LD_PENALTY,@CESS_LD_PENALTY,@CGST_TDS,@SGST_TDS,@IGST_TDS,@CESS_TDS)"
                    CMD = New SqlCommand(query1, conn_trans, myTrans)
                    CMD.Parameters.AddWithValue("@INVOICE_NO", BE_INVOICE_NO)
                    CMD.Parameters.AddWithValue("@INVOICE_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                    CMD.Parameters.AddWithValue("@GARN_CRR_MB_NO", imp_garn_no_TextBox1.Text)
                    CMD.Parameters.AddWithValue("@VALUATION_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                    CMD.Parameters.AddWithValue("@DATA_TYPE", "FOREIGN")
                    CMD.Parameters.AddWithValue("@SL_NO", 1)
                    CMD.Parameters.AddWithValue("@SUPL_CODE", Label18.Text)
                    CMD.Parameters.AddWithValue("@SUPL_NAME", Label16.Text)
                    CMD.Parameters.AddWithValue("@TAXABLE_VALUE", taxable_value)
                    CMD.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    CMD.Parameters.AddWithValue("@SGST_PERCENTAGE", 0)
                    CMD.Parameters.AddWithValue("@CGST_PERCENTAGE", 0)
                    CMD.Parameters.AddWithValue("@IGST_PERCENTAGE", IGST_PERCENTAGE)
                    CMD.Parameters.AddWithValue("@CESS_PERCENTAGE", 0)
                    CMD.Parameters.AddWithValue("@SGST_AMT", 0)
                    CMD.Parameters.AddWithValue("@CGST_AMT", 0)
                    CMD.Parameters.AddWithValue("@IGST_AMT", Math.Round((taxable_value * IGST_PERCENTAGE / 100), 2))
                    CMD.Parameters.AddWithValue("@CESS_AMT", 0)
                    CMD.Parameters.AddWithValue("@RCM_SGST_AMT", 0)
                    CMD.Parameters.AddWithValue("@RCM_CGST_AMT", 0)
                    CMD.Parameters.AddWithValue("@RCM_IGST_AMT", 0)
                    CMD.Parameters.AddWithValue("@RCM_CESS_AMT", 0)
                    CMD.Parameters.AddWithValue("@TAXABLE_LD_PENALTY", 0)
                    CMD.Parameters.AddWithValue("@SGST_LD_PENALTY", 0)
                    CMD.Parameters.AddWithValue("@CGST_LD_PENALTY", 0)
                    CMD.Parameters.AddWithValue("@IGST_LD_PENALTY", 0)
                    CMD.Parameters.AddWithValue("@CESS_LD_PENALTY", 0)
                    CMD.Parameters.AddWithValue("@SGST_TDS", 0)
                    CMD.Parameters.AddWithValue("@CGST_TDS", 0)
                    CMD.Parameters.AddWithValue("@IGST_TDS", 0)
                    CMD.Parameters.AddWithValue("@CESS_TDS", 0)
                    CMD.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    CMD.ExecuteReader()
                    CMD.Dispose()

                Next

                Dim C As Integer
                Dim PROV As String = ""
                ''PROV for transport
                C = 0
                Dim PROV_PRICE_FOR_TRANSPORTER As Decimal = 0.0
                Dim PROV_PRICE_FOR_CHA As Decimal = 0.0
                Dim RCVD_QTY_TRANSPORTER As Decimal = 0.0
                Dim RCVD_QTY_CHA As Decimal = 0.0
                Dim SHORT_AMT As Decimal = 0.0
                Dim CENVAT As Decimal = 0


                For C = 0 To imp_GridView3.Rows.Count - 1
                    PROV_PRICE_FOR_TRANSPORTER = PROV_PRICE_FOR_TRANSPORTER + CDec(imp_GridView3.Rows(C).Cells(19).Text)
                    PROV_PRICE_FOR_CHA = PROV_PRICE_FOR_CHA + CDec(imp_GridView3.Rows(C).Cells(18).Text)

                    conn.Open()
                    mycommandNew.CommandText = "SELECT NO_OF_BAG,BAG_WEIGHT FROM PO_RCD_MAT WITH(NOLOCK) WHERE CRR_NO = '" & imp_GridView3.Rows(C).Cells(0).Text & "' AND MAT_SLNO=" & imp_GridView3.Rows(C).Cells(3).Text
                    mycommandNew.Connection = conn
                    dr = mycommandNew.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        BAG_WEIGHT = dr.Item("BAG_WEIGHT")
                        NO_OF_BAG = dr.Item("NO_OF_BAG")
                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                    RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(imp_GridView3.Rows(C).Cells(10).Text) + ((NO_OF_BAG * BAG_WEIGHT) / 1000)
                    RCVD_QTY_CHA = RCVD_QTY_CHA + CDec(imp_GridView3.Rows(C).Cells(9).Text)
                    SHORT_AMT = SHORT_AMT + CDec(imp_GridView3.Rows(C).Cells(20).Text)
                    CENVAT = CENVAT + CDec(imp_GridView3.Rows(C).Cells(16).Text)
                Next
                PROV = ""

                If PROV_PRICE_FOR_TRANSPORTER > 0 Then

                    ''''''''''''''''''''''''''''''''''''''''''''''
                    Dim CHLN_DATE As String = ""
                    conn.Open()
                    Dim MC1 As New SqlCommand
                    MC1.CommandText = "SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WITH(NOLOCK) WHERE CRR_NO='" & imp_crr_no_DropDownList1.SelectedValue & "'"
                    MC1.Connection = conn
                    dr = MC1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        CHLN_DATE = dr.Item("CHLN_DATE")
                        dr.Close()
                    End If
                    conn.Close()

                    conn.Open()
                    Dim w_qty, w_complite, w_unit_price, W_discount, mat_price As Decimal
                    Dim WO_NAME As String = ""
                    Dim WO_AU As String = ""
                    Dim WO_SUPL_ID, WO_AMD, AMD_DATE As New String("")
                    Dim MC As New SqlCommand
                    MC.CommandText = "select MAX(WO_AMD) AS WO_AMD ,MAX(AMD_DATE) AS AMD_DATE,MAX(SUPL_ID) as SUPL_ID, sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_MATERIAL_COST) as W_MATERIAL_COST,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER WITH(NOLOCK) where PO_NO = '" & Label13.Text & "' AND W_SLNO='" & Label14.Text & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WITH(NOLOCK) WHERE CRR_NO='" & imp_crr_no_DropDownList1.SelectedValue & "')"
                    MC.Connection = conn
                    dr = MC.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        w_qty = dr.Item("W_QTY")
                        w_complite = dr.Item("W_COMPLITED")
                        w_unit_price = dr.Item("W_UNIT_PRICE")
                        W_discount = dr.Item("W_DISCOUNT")
                        mat_price = dr.Item("W_MATERIAL_COST")
                        WO_NAME = dr.Item("W_NAME")
                        WO_AU = dr.Item("W_AU")
                        WO_SUPL_ID = dr.Item("SUPL_ID")
                        WO_AMD = dr.Item("WO_AMD")
                        AMD_DATE = dr.Item("AMD_DATE")
                        dr.Close()
                    End If
                    conn.Close()

                    'Checking if valuation is done or not
                    conn.Open()
                    Dim v_ind As String = ""
                    MC.CommandText = "select * FROM mb_book WITH(NOLOCK) WHERE mb_no='" & imp_crr_no_DropDownList1.SelectedValue & "'"
                    MC.Connection = conn
                    dr = MC.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        If IsDBNull(dr.Item("v_ind")) Then
                            v_ind = "N"
                        Else
                            v_ind = dr.Item("v_ind")
                        End If

                        dr.Close()
                    End If
                    conn.Close()

                    'Update MB-Book
                    If (v_ind = "N") Then

                        'conn.Open()
                        Dim Query As String = "update mb_book set pen_amt=@pen_amt where mb_no='" & imp_crr_no_DropDownList1.SelectedValue & "'"
                        Dim cmd As New SqlCommand(Query, conn_trans, myTrans)

                        cmd.Parameters.AddWithValue("@pen_amt", CDec(TextBox1.Text) + SHORT_AMT)
                        cmd.ExecuteReader()
                        cmd.Dispose()
                        'conn.Close()

                    End If

                End If

                For I = 0 To imp_GridView3.Rows.Count - 1
                    ''SAVE LEDGER ENTRY TAX,CENVAT
                    Dim cenvat_head As String
                    cenvat_head = ""
                    conn.Open()
                    mycommandNew1.CommandText = "select * from work_group WITH(NOLOCK) where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WITH(NOLOCK) WHERE SO_NO='" & po_no & "')"
                    mycommandNew1.Connection = conn
                    dr = mycommandNew1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        cenvat_head = dr.Item("igst")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                    LEDGER_SAVE_PUR(po_no, 0, "", imp_garn_no_TextBox1.Text, cenvat_head, "Dr", CDec(imp_GridView3.Rows(I).Cells(27).Text), "IGST", 2, "", imp_GridView3.Rows(0).Cells(24).Text)
                    ''LEDGER_SAVE_PUR(po_no, 0, "", imp_garn_no_TextBox1.Text, entry_tax_head, "Cr", ENTRY_TAX, "ENTRY TAX", 6, "")
                Next


                ''GRID VIEW CLEAR
                Dim dt4 As New DataTable()
                dt4.Columns.AddRange(New DataColumn(13) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Ord Qty"), New DataColumn("Chln Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note"), New DataColumn("BE_NO")})
                ViewState("imp_mat") = dt4
                Me.BINDGRID4()
                Button51.Enabled = False

                myTrans.Commit()
                Label534.Visible = True
                Label534.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label534.Visible = True
                Label534.Text = "There was some Error, please contact EDP."
                imp_garn_no_TextBox1.Text = ""
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using



    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        If (DropDownList1.SelectedValue <> "Select") Then

            conn.Open()
            'da = New SqlDataAdapter("select distinct CRR_NO from PO_RCD_MAT where CRR_NO like 'SCRR%' and crr_date between '" & 2000 + CInt(Left(DropDownList1.SelectedValue, 2)) & "-04-01' and '" & 2000 + CInt(Right(DropDownList1.SelectedValue, 2)) & "-03-31' ORDER BY CRR_NO", conn)

            da = New SqlDataAdapter("select distinct GARN_NO from PO_RCD_MAT where GARN_NO LIKE 'SGARN%' and garn_date between '" & 2000 + CInt(Left(DropDownList1.SelectedValue, 2)) & "-04-01' and '" & 2000 + CInt(Right(DropDownList1.SelectedValue, 2)) & "-03-31' ORDER BY GARN_NO", conn)
            da.Fill(ds, "PO_RCD_MAT")
            search_DropDown.DataSource = ds.Tables("PO_RCD_MAT")
            search_DropDown.DataValueField = "GARN_NO"
            search_DropDown.DataBind()
            conn.Close()

        Else
            search_DropDown.Items.Clear()
            search_DropDown.DataBind()
        End If
    End Sub


End Class