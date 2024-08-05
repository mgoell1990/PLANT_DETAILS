Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class RAW_MAT_CORR
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
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
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT top 100 ISSUE_NO ,LINE_NO,LINE_DATE ,LINE_TYPE ,MAT_CODE ,MAT_QTY ,ISSUE_QTY , COST_CODE  FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' and issue_no like 'RGARN002518' ORDER BY MAT_CODE ,LINE_NO ", conn)
        da.Fill(dt)
        conn.Close()
        GridView210.DataSource = dt
        GridView210.DataBind()
        conn.Close()

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click





        Dim row_count As Integer = 0
        For row_count = 0 To GridView210.Rows.Count - 1
            If GridView210.Rows(row_count).Cells(3).Text = "R" Then
                'search po no wise data
                Dim crr_no, po_no, mat_slno, trans_wo, trans_wslno, po_type As New String("")
                conn.Open()
                mycommand.CommandText = "select ORDER_DETAILS .PO_TYPE , PO_RCD_MAT .PO_NO ,PO_RCD_MAT .MAT_SLNO ,PO_RCD_MAT .CRR_NO , PO_RCD_MAT .TRANS_WO_NO ,PO_RCD_MAT .TRANS_SLNO  from MAT_DETAILS join PO_RCD_MAT on MAT_DETAILS .ISSUE_NO =PO_RCD_MAT .GARN_NO and MAT_DETAILS .MAT_CODE =PO_RCD_MAT .MAT_CODE join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .SO_NO where MAT_DETAILS .ISSUE_NO ='" & GridView210.Rows(row_count).Cells(0).Text & "' and MAT_DETAILS .MAT_CODE ='" & GridView210.Rows(row_count).Cells(4).Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    po_type = dr.Item("PO_TYPE")
                    po_no = dr.Item("PO_NO")
                    mat_slno = dr.Item("MAT_SLNO")
                    crr_no = dr.Item("CRR_NO")
                    trans_wo = dr.Item("TRANS_WO_NO")
                    trans_wslno = dr.Item("TRANS_SLNO")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()







                If trans_wo = "N/A" And (po_type = "STORE MATERIAL" Or po_type = "RAW MATERIAL") Then
                    NON_TRANSPORT_IND_GARN(GridView210.Rows(row_count).Cells(0).Text, GridView210.Rows(row_count).Cells(1).Text, GridView210.Rows(row_count).Cells(2).Text, GridView210.Rows(row_count).Cells(3).Text, GridView210.Rows(row_count).Cells(4).Text, GridView210.Rows(row_count).Cells(5).Text, GridView210.Rows(row_count).Cells(7).Text, mat_slno, crr_no, row_count)
                ElseIf trans_wo <> "N/A" And (po_type = "STORE MATERIAL" Or po_type = "RAW MATERIAL") Then
                    IND_GARN(GridView210.Rows(row_count).Cells(0).Text, GridView210.Rows(row_count).Cells(1).Text, GridView210.Rows(row_count).Cells(2).Text, GridView210.Rows(row_count).Cells(3).Text, GridView210.Rows(row_count).Cells(4).Text, GridView210.Rows(row_count).Cells(5).Text, GridView210.Rows(row_count).Cells(7).Text, mat_slno, crr_no, row_count, trans_wo, trans_wslno)
                ElseIf trans_wo <> "N/A" And po_type = "RAW MATERIAL(IMP)" Then

                ElseIf trans_wo = "N/A" And po_type = "RAW MATERIAL(IMP)" Then

                End If

            ElseIf GridView210.Rows(row_count).Cells(3).Text = "I" Then



            End If
        Next


        Return

    End Sub
    Private Sub IND_GARN(REFF As String, LINE_NO As Integer, LINE_DATE As Date, LINE_TYPE As String, MAT_CODE As String, MAT_QTY As Decimal, PO_NO As String, MAT_SLNO As Integer, CRR_NO As String, row_no As Integer, trans_wo As String, trans_slno As Integer)
        Dim TOLE_VALUE, TRANS_DIFF, w_tolerance, mat_exces, short_trans_amt, unit_price, freight, ld_charge, packing_charge, local_freight, discount, analitical_charge, ed_charge, penality, vat, entry, trans_penality As New Decimal(0.0)
        Dim chln_qty, rcvd_qty, loss_qty, tole_qty, accept_qty, mat_rej_qty As New Decimal(0.0)
        Dim base_price, disc_price, pf_price, net_price, ed_price, vat_price, freight_price, local_freight_price, penality_price, et_price, ld_price, mat_price, trans_price, short_price, loss_ed_price, wt_var_price, analitical_price As New Decimal(0.0)
        Dim disc_type, pf_type, ed_type, freight_type, penality_type, MAT_NAME, AMD_NO As New String("")

        conn.Open()
        mycommand.CommandText = "SELECT SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TYPE) AS FREIGHT_TYPE,MAX(DISC_TYPE) AS DISC_TYPE, MAX(PF_TYPE) AS PF_TYPE, MAX(MAT_CODE) as mat_code,MAX(MAT_NAME) AS MAT_NAME,MAX(AMD_NO) as AMD_NO,MAX(MAT_EXCISE) as MAT_EXCISE,SUM(MAT_QTY) AS MAT_QTY, SUM(MAT_UNIT_RATE ) AS MAT_UNIT_RATE,SUM(MAT_PACK) AS MAT_PACK,SUM(MAT_DISCOUNT) AS MAT_DISCOUNT,SUM(MAT_EXCISE_DUTY) AS MAT_EXCISE_DUTY,SUM(MAT_CST) AS MAT_CST,SUM(MAT_ENTRY_TAX) AS MAT_ENTRY_TAX,SUM(MAT_OTHER_TAX) AS MAT_OTHER_TAX,SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD,SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TERM) AS FREIGHT_TYPE FROM PO_ORD_MAT  JOIN ORDER_DETAILS ON PO_ORD_MAT .PO_NO=ORDER_DETAILS .SO_NO WHERE PO_ORD_MAT.PO_NO='" & PO_NO & "' AND PO_ORD_MAT.MAT_SLNO=" & MAT_SLNO & " and PO_ORD_MAT.AMD_DATE < =(SELECT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & CRR_NO & "' AND MAT_SLNO=" & MAT_SLNO & ")"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            unit_price = dr.Item("MAT_UNIT_RATE")
            discount = dr.Item("MAT_DISCOUNT")
            packing_charge = dr.Item("MAT_PACK")
            ed_charge = dr.Item("MAT_EXCISE_DUTY")
            vat = dr.Item("MAT_CST")
            freight = dr.Item("MAT_FREIGHT_PU")
            MAT_CODE = dr.Item("mat_code")
            MAT_NAME = dr.Item("MAT_NAME")
            AMD_NO = dr.Item("amd_no")
            disc_type = dr.Item("DISC_TYPE")
            pf_type = dr.Item("PF_TYPE")
            ed_type = dr.Item("MAT_EXCISE")
            freight_type = dr.Item("FREIGHT_TYPE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        mycommand.Dispose()
        ' search penality
        conn.Open()
        mycommand.CommandText = "select * from GARN_PRICE where CRR_NO ='" & CRR_NO & "'and PO_NO ='" & PO_NO & "' and MAT_SLNO =" & MAT_SLNO
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            penality = dr.Item("PENALITY")
            penality_type = dr.Item("PENALITY_TYPE")
            analitical_charge = dr.Item("ANALITICAL_CHARGE")
            local_freight = dr.Item("LOCAL_FREIGHT")
            ld_charge = dr.Item("LD_CHARGE")
            entry = dr.Item("ENTRY_TAX")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        mycommand.Dispose()

        Dim chln_qty_price, accept_qty_price, party_qty As New Decimal(0.0)
        Dim party_net, party_disc, party_base, party_pf, party_ed, party_vat, party_freight As New Decimal(0.0)
        Dim w_qty, w_complite, PRICE, w_st, w_unit_price, W_discount, DISCOUNT_VALUE, WC_TAX As New Decimal(0.0)
        Dim w_name, w_au As String
        conn.Open()
        mycommand.CommandText = "select  PO_RCD_MAT .MAT_CHALAN_QTY,PO_RCD_MAT .MAT_EXCE ,PO_RCD_MAT .MAT_RCD_QTY ,PO_RCD_MAT .MAT_REJ_QTY from MAT_DETAILS join PO_RCD_MAT on MAT_DETAILS .ISSUE_NO =PO_RCD_MAT .GARN_NO and MAT_DETAILS .MAT_CODE =PO_RCD_MAT .MAT_CODE where MAT_DETAILS .ISSUE_NO ='" & REFF & "' and MAT_DETAILS .MAT_CODE ='" & MAT_CODE & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            chln_qty = dr.Item("MAT_CHALAN_QTY")
            rcvd_qty = dr.Item("MAT_RCD_QTY")
            mat_rej_qty = dr.Item("MAT_REJ_QTY")
            mat_exces = dr.Item("MAT_EXCE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        mycommand.Dispose()
        party_qty = chln_qty - (mat_rej_qty + mat_exces)
        accept_qty = rcvd_qty - (mat_rej_qty + mat_exces)
        ''TRANSPORTER TOLERANCE
        Dim MC As New SqlCommand
        conn.Open()
        MC.CommandText = "select sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_STAX) as W_STAX,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,sum(W_WCTAX) as W_WCTAX,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & trans_wo & "' AND W_SLNO='" & trans_slno & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & CRR_NO & "' AND MAT_SLNO='" & MAT_SLNO & "')"
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            w_qty = dr.Item("W_QTY")
            w_complite = dr.Item("W_COMPLITED")
            w_st = dr.Item("W_STAX")
            w_tolerance = dr.Item("W_TOLERANCE")
            w_unit_price = dr.Item("W_UNIT_PRICE")
            W_discount = dr.Item("W_DISCOUNT")
            WC_TAX = dr.Item("W_WCTAX")
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
        MC.CommandText = "SELECT NO_OF_BAG,BAG_WEIGHT FROM PO_RCD_MAT WHERE CRR_NO='" & CRR_NO & "' AND MAT_SLNO='" & MAT_SLNO & "'"
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
        'swachh bharat cess
        Dim sbc, kkc, s_tax, s_tax_price, sbc_price, kkc_price As New Decimal(0.0)
        conn.Open()
        MC.CommandText = "select * from s_tax_liability where  taxable_service ='FREIGHT INWARD'"
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            kkc = dr.Item("kk_tax")
            sbc = dr.Item("sb_tax")
            s_tax = dr.Item("service_tax")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()


        'krishi kalyana cess

        ''trans freight calculation

        PRICE = w_unit_price
        DISCOUNT_VALUE = (PRICE * W_discount) / 100
        s_tax_price = (PRICE * s_tax) / 100
        sbc_price = (PRICE * sbc) / 100
        kkc_price = (PRICE * kkc) / 100
        PRICE = (PRICE - DISCOUNT_VALUE)
        trans_price = PRICE * (rcvd_qty + ((BAG_WEIGHT * NO_OF_BAG) / 1000))
        trans_price = FormatNumber(trans_price + ((trans_price * sbc) / 100), 2)





        ''BASIC PRICE
        base_price = FormatNumber(unit_price * accept_qty, 2)
        party_base = FormatNumber(unit_price * party_qty, 2)
        'DISCOUNT
        If disc_type = "PERCENTAGE" Then
            disc_price = FormatNumber((base_price * discount) / 100, 2)
            party_disc = FormatNumber((party_base * discount) / 100, 2)
        ElseIf disc_type = "PER UNIT" Then
            disc_price = FormatNumber(accept_qty * discount, 2)
            party_disc = FormatNumber(party_qty * discount, 2)
        ElseIf disc_type = "N/A" Then
            disc_price = 0.0
            party_disc = 0.0
        End If
        ''PACKING AND FORWD
        If pf_type = "PERCENTAGE" Then
            pf_price = FormatNumber(((base_price - disc_price) * packing_charge) / 100, 2)
            party_pf = FormatNumber(((party_base - party_disc) * packing_charge) / 100, 2)
        ElseIf pf_type = "PER UNIT" Then
            pf_price = FormatNumber(accept_qty * packing_charge, 2)
            party_pf = FormatNumber(party_qty * packing_charge, 2)
        ElseIf pf_type = "N/A" Then
            pf_price = 0.0
            party_pf = 0.0
        End If
        ''NET PRICE
        net_price = (base_price - disc_price) + pf_price
        party_net = (party_base - party_disc) + party_pf
        ''EXCISE DUTY
        If ed_type = "PERCENTAGE" Then
            ed_price = FormatNumber((net_price * ed_charge) / 100, 2)
            party_ed = FormatNumber((party_net * ed_charge) / 100, 2)
        ElseIf ed_type = "PER UNIT" Then
            ed_price = FormatNumber(ed_charge * accept_qty, 2)
            party_ed = FormatNumber(ed_charge * party_qty, 2)
        ElseIf ed_type = "N/A" Then
            ed_price = 0.0
            party_ed = 0.0
        End If


        ''freight calculate
        If freight_type = "PERCENTAGE" Then
            freight_price = FormatNumber((base_price * freight) / 100, 2)
            party_freight = FormatNumber((party_base * freight) / 100, 2)
        ElseIf freight_type = "PER UNIT" Then
            freight_price = FormatNumber(accept_qty * freight, 2)
            party_freight = FormatNumber(party_qty * freight, 2)
        ElseIf freight_type = "N/A" Then
            freight_price = 0.0
            party_freight = 0.0
        End If
        ''VAT OR CST
        vat_price = FormatNumber(((party_net + party_ed) * vat) / 100, 2)
        party_vat = FormatNumber(((net_price + ed_price) * vat) / 100, 2)
        ''ANALITICAL PRICE
        analitical_price = FormatNumber(analitical_charge * party_qty, 2)
        ''TOLERANCE
        TOLE_VALUE = (chln_qty * w_tolerance) / 100
        ''TRANSPORTATION DIFFRENCE
        TRANS_DIFF = chln_qty - rcvd_qty
        If TOLE_VALUE > TRANS_DIFF Then
            ''LOSS ON TRANSPORT
            wt_var_price = FormatNumber(((net_price + party_vat + freight_price + analitical_price) / accept_qty) * TRANS_DIFF, 2)
            ''LOSS ON ED
            loss_ed_price = FormatNumber((ed_price / accept_qty) * TRANS_DIFF, 2)
            ''SHORTAGE VALUE
            short_trans_amt = 0
        ElseIf TOLE_VALUE < TRANS_DIFF Then
            If chln_qty <> rcvd_qty Then
                ''LOSS ON TRANSPORT
                wt_var_price = FormatNumber(((net_price + party_vat + freight_price + analitical_price) / accept_qty) * TOLE_VALUE, 2)
                ''LOSS ON ED
                loss_ed_price = FormatNumber((ed_price / party_qty) * TOLE_VALUE, 2)
                ''SHORTAGE VALUE
                short_trans_amt = FormatNumber((((net_price + ed_price + party_vat + freight_price + analitical_price) / accept_qty) * TRANS_DIFF) - (loss_ed_price + wt_var_price), 2)
            End If

        End If
        ''LOCAL FREIGHT
        local_freight_price = local_freight
        ''penality calculate
        If penality_type = True Then
            penality_price = FormatNumber((base_price * penality) / 100, 2)
        ElseIf penality_type = False Then
            penality_price = FormatNumber(penality * accept_qty, 2)
        End If
        ''ld calculate
        ld_price = FormatNumber(((base_price - disc_price) * ld_charge) / 100, 2)
        ''ENTRY TAX
        et_price = FormatNumber(((party_net + party_ed + vat_price + freight_price + local_freight_price + analitical_price + trans_price) * entry) / 100, 2)
        mat_price = FormatNumber((net_price + party_vat + freight_price + local_freight_price + analitical_price + et_price), 2)


        GridView210.Rows(row_no).Cells(8).Text = unit_price
        GridView210.Rows(row_no).Cells(9).Text = disc_price
        GridView210.Rows(row_no).Cells(10).Text = pf_price
        GridView210.Rows(row_no).Cells(11).Text = ed_price
        GridView210.Rows(row_no).Cells(12).Text = vat_price
        GridView210.Rows(row_no).Cells(13).Text = analitical_price
        GridView210.Rows(row_no).Cells(14).Text = ld_price
        GridView210.Rows(row_no).Cells(15).Text = penality_price
        GridView210.Rows(row_no).Cells(16).Text = freight_price
        GridView210.Rows(row_no).Cells(17).Text = local_freight_price
        GridView210.Rows(row_no).Cells(18).Text = et_price
        GridView210.Rows(row_no).Cells(19).Text = trans_price
        GridView210.Rows(row_no).Cells(20).Text = mat_price
        GridView210.Rows(row_no).Cells(21).Text = loss_ed_price + wt_var_price
        GridView210.Rows(row_no).Cells(22).Text = wt_var_price
        GridView210.Rows(row_no).Cells(23).Text = loss_ed_price
        GridView210.Rows(row_no).Cells(24).Text = short_trans_amt
        GridView210.Rows(row_no).Cells(25).Text = trans_penality





    End Sub
    Private Sub NON_TRANSPORT_IND_GARN(REFF As String, LINE_NO As Integer, LINE_DATE As Date, LINE_TYPE As String, MAT_CODE As String, MAT_QTY As Decimal, PO_NO As String, MAT_SLNO As Integer, CRR_NO As String, row_no As Integer)
        Dim TOLE_VALUE, TRANS_DIFF, w_tolerance, mat_exces, short_trans_amt, unit_price, freight, ld_charge, packing_charge, local_freight, discount, analitical_charge, ed_charge, penality, vat, entry As New Decimal(0.0)
        Dim chln_qty, rcvd_qty, loss_qty, tole_qty, accept_qty, mat_rej_qty As New Decimal(0.0)
        Dim base_price, disc_price, pf_price, net_price, ed_price, vat_price, freight_price, local_freight_price, penality_price, et_price, ld_price, mat_price, trans_price, short_price, loss_ed_price, wt_var_price, analitical_price As New Decimal(0.0)
        Dim disc_type, pf_type, ed_type, freight_type, penality_type, MAT_NAME, AMD_NO As New String("")

        conn.Open()
        mycommand.CommandText = "SELECT SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TYPE) AS FREIGHT_TYPE,MAX(DISC_TYPE) AS DISC_TYPE, MAX(PF_TYPE) AS PF_TYPE, MAX(MAT_CODE) as mat_code,MAX(MAT_NAME) AS MAT_NAME,MAX(AMD_NO) as AMD_NO,MAX(MAT_EXCISE) as MAT_EXCISE,SUM(MAT_QTY) AS MAT_QTY, SUM(MAT_UNIT_RATE ) AS MAT_UNIT_RATE,SUM(MAT_PACK) AS MAT_PACK,SUM(MAT_DISCOUNT) AS MAT_DISCOUNT,SUM(MAT_EXCISE_DUTY) AS MAT_EXCISE_DUTY,SUM(MAT_CST) AS MAT_CST,SUM(MAT_ENTRY_TAX) AS MAT_ENTRY_TAX,SUM(MAT_OTHER_TAX) AS MAT_OTHER_TAX,SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD,SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TERM) AS FREIGHT_TYPE FROM PO_ORD_MAT  JOIN ORDER_DETAILS ON PO_ORD_MAT .PO_NO=ORDER_DETAILS .SO_NO WHERE PO_ORD_MAT.PO_NO='" & PO_NO & "' AND PO_ORD_MAT.MAT_SLNO=" & MAT_SLNO & " and PO_ORD_MAT.AMD_DATE < =(SELECT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & CRR_NO & "' AND MAT_SLNO=" & MAT_SLNO & ")"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            unit_price = dr.Item("MAT_UNIT_RATE")
            discount = dr.Item("MAT_DISCOUNT")
            packing_charge = dr.Item("MAT_PACK")
            ed_charge = dr.Item("MAT_EXCISE_DUTY")
            vat = dr.Item("MAT_CST")
            freight = dr.Item("MAT_FREIGHT_PU")
            MAT_CODE = dr.Item("mat_code")
            MAT_NAME = dr.Item("MAT_NAME")
            AMD_NO = dr.Item("amd_no")
            disc_type = dr.Item("DISC_TYPE")
            pf_type = dr.Item("PF_TYPE")
            ed_type = dr.Item("MAT_EXCISE")
            freight_type = dr.Item("FREIGHT_TYPE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        mycommand.Dispose()
        'SEARCH PO_RCD_MAT 

        conn.Open()
        mycommand.CommandText = "select  PO_RCD_MAT .MAT_CHALAN_QTY,PO_RCD_MAT .MAT_EXCE ,PO_RCD_MAT .MAT_RCD_QTY ,PO_RCD_MAT .MAT_REJ_QTY from MAT_DETAILS join PO_RCD_MAT on MAT_DETAILS .ISSUE_NO =PO_RCD_MAT .GARN_NO and MAT_DETAILS .MAT_CODE =PO_RCD_MAT .MAT_CODE where MAT_DETAILS .ISSUE_NO ='" & REFF & "' and MAT_DETAILS .MAT_CODE ='" & MAT_CODE & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            chln_qty = dr.Item("MAT_CHALAN_QTY")
            rcvd_qty = dr.Item("MAT_RCD_QTY")
            mat_rej_qty = dr.Item("MAT_REJ_QTY")
            mat_exces = dr.Item("MAT_EXCE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        mycommand.Dispose()
        ' search penality
        conn.Open()
        mycommand.CommandText = "select * from GARN_PRICE where CRR_NO ='" & CRR_NO & "'and PO_NO ='" & PO_NO & "' and MAT_SLNO =" & MAT_SLNO
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            penality = dr.Item("PENALITY")
            penality_type = dr.Item("PENALITY_TYPE")
            analitical_charge = dr.Item("ANALITICAL_CHARGE")
            local_freight = dr.Item("LOCAL_FREIGHT")
            ld_charge = dr.Item("LD_CHARGE")
            entry = dr.Item("ENTRY_TAX")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        mycommand.Dispose()




        accept_qty = rcvd_qty - (mat_rej_qty + mat_exces)
        ''BASIC PRICE
        base_price = FormatNumber(unit_price * accept_qty, 2)

        'DISCOUNT
        If disc_type = "PERCENTAGE" Then
            disc_price = FormatNumber((base_price * discount) / 100, 2)
        ElseIf disc_type = "PER UNIT" Then
            disc_price = FormatNumber(accept_qty * discount, 2)
        ElseIf disc_type = "N/A" Then
            disc_price = 0.0
        End If
        ''PACKING AND FORWD
        If pf_type = "PERCENTAGE" Then
            pf_price = FormatNumber(((base_price - disc_price) * packing_charge) / 100, 2)
        ElseIf pf_type = "PER UNIT" Then
            pf_price = FormatNumber(accept_qty * packing_charge, 2)
        ElseIf pf_type = "N/A" Then
            pf_price = 0.0
        End If
        ''NET PRICE
        net_price = (base_price - disc_price) + pf_price
        ''EXCISE DUTY
        If ed_type = "PERCENTAGE" Then
            ed_price = FormatNumber((net_price * ed_charge) / 100, 2)
        ElseIf ed_type = "PER UNIT" Then
            ed_price = FormatNumber(ed_charge * accept_qty, 2)
        ElseIf ed_type = "N/A" Then
            ed_price = 0.0
        End If

        ''freight calculate
        If freight_type = "PERCENTAGE" Then
            freight_price = FormatNumber((base_price * freight) / 100, 2)
        ElseIf freight_type = "PER UNIT" Then
            freight_price = FormatNumber(accept_qty * freight, 2)
        ElseIf freight_type = "N/A" Then
            freight_price = 0.0
        End If
        ''VAT OR CST
        vat_price = FormatNumber(((net_price + ed_price) * vat) / 100, 2)
        ''LOCAL FREIGHT
        local_freight_price = local_freight
        ''ANALITICAL PRICE
        analitical_price = analitical_charge * accept_qty
        ''penality calculate
        If penality_type = "1" Then
            penality_price = FormatNumber((base_price * penality) / 100, 2)
        ElseIf penality_type = "0" Then
            penality_price = FormatNumber(penality * accept_qty, 2)
        End If
        ''ld calculate
        ld_price = FormatNumber(((base_price - disc_price) * ld_charge) / 100, 2)
        ''ENTRY TAX
        et_price = FormatNumber(((net_price + ed_price + vat_price + freight_price + local_freight_price + analitical_price) * entry) / 100, 2)
        ''MATERIAL PRICE
        mat_price = FormatNumber(net_price + vat_price + freight_price + local_freight_price + et_price + analitical_price, 2)

        'insert grid view
        GridView210.Rows(row_no).Cells(8).Text = unit_price
        GridView210.Rows(row_no).Cells(9).Text = disc_price
        GridView210.Rows(row_no).Cells(10).Text = pf_price
        GridView210.Rows(row_no).Cells(11).Text = ed_price
        GridView210.Rows(row_no).Cells(12).Text = vat_price
        GridView210.Rows(row_no).Cells(13).Text = analitical_price
        GridView210.Rows(row_no).Cells(14).Text = ld_price
        GridView210.Rows(row_no).Cells(15).Text = penality_price
        GridView210.Rows(row_no).Cells(16).Text = freight_price
        GridView210.Rows(row_no).Cells(17).Text = local_freight_price
        GridView210.Rows(row_no).Cells(18).Text = et_price
        GridView210.Rows(row_no).Cells(19).Text = "0.00"
        GridView210.Rows(row_no).Cells(20).Text = mat_price
        GridView210.Rows(row_no).Cells(21).Text = "0.00"
        GridView210.Rows(row_no).Cells(22).Text = "0.00"
        GridView210.Rows(row_no).Cells(23).Text = "0.00"
        GridView210.Rows(row_no).Cells(24).Text = "0.00"
        GridView210.Rows(row_no).Cells(25).Text = "0.00"
    End Sub
   
    Private Sub MAT_ISSUE(REFF As String, LINE_NO As Integer, LINE_DATE As Date, LINE_TYPE As String, MAT_CODE As String, MAT_QTY As Decimal, PO_NO As String)

    End Sub
End Class