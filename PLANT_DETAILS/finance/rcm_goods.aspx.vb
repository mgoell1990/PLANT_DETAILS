Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class rcm
    'Inherits System.Web.UI.Page
    'Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    'Dim count As Integer
    'Dim dr As SqlDataReader
    'Dim mycommand As New SqlCommand
    'Dim ds As New DataSet()
    'Dim da As New SqlDataAdapter
    'Dim dt As New DataTable
    'Dim str As String
    'Dim result As Integer
    'Dim provider As CultureInfo = CultureInfo.InvariantCulture
    'Dim working_date As Date = Today.Date
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    'End Sub
    'Protected Sub BINDGRID()
    '    GridView2.DataSource = DirectCast(ViewState("RCM"), DataTable)
    '    GridView2.DataBind()
    'End Sub
    'Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
    '    Dim dt2 As New DataTable()
    '    dt2.Columns.AddRange(New DataColumn(17) {New DataColumn("GARN_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("CHPTR_HEAD"), New DataColumn("UNIT_RATE"), New DataColumn("MAT_QTY"), New DataColumn("TAX_VAL"), New DataColumn("CGST_P"), New DataColumn("CGST"), New DataColumn("SGST_P"), New DataColumn("SGST"), New DataColumn("IGST_P"), New DataColumn("IGST"), New DataColumn("CESS_P"), New DataColumn("CESS"), New DataColumn("TOTAL_VAL")})
    '    ViewState("RCM") = dt2
    '    Me.BINDGRID()
    '    If DropDownList31.SelectedValue = "Select" Then
    '        DropDownList31.Focus()
    '        Return
    '    ElseIf DropDownList31.SelectedValue = "Unregistered Party" Then
    '        TextBox124.Text = "N/A"
    '        TextBox125.Text = "N/A"
    '        TextBox126.ReadOnly = False
    '        TextBox125.ForeColor = Drawing.Color.Black
    '        TextBox126.BackColor = Drawing.Color.White
    '        TextBox126.Text = ""

    '        TextBox188.ReadOnly = False
    '        TextBox188.ForeColor = Drawing.Color.Black
    '        TextBox188.BackColor = Drawing.Color.White
    '        TextBox188.Text = ""

    '        TextBox189.ReadOnly = False
    '        TextBox189.ForeColor = Drawing.Color.Black
    '        TextBox189.BackColor = Drawing.Color.White
    '        TextBox189.Text = ""
    '        Panel2.Visible = True
    '        Panel8.Visible = False
    '        Panel1.Visible = True
    '        Panel3.Visible = False
    '        conn.Open()
    '        dt.Clear()
    '        da = New SqlDataAdapter("select (CHPT_CODE + ' , ' + CHPT_NAME ) as chpt from CHPTR_HEADING order by CHPT_CODE", conn)
    '        da.Fill(dt)
    '        conn.Close()
    '        DropDownList32.Items.Clear()
    '        DropDownList32.DataSource = dt
    '        DropDownList32.DataValueField = "chpt"
    '        DropDownList32.DataBind()
    '        DropDownList32.Items.Add("Select")
    '        DropDownList32.SelectedValue = "Select"
    '    ElseIf DropDownList31.SelectedValue = "Registered Party" Then
    '        Panel1.Visible = False
    '        Panel3.Visible = True
    '        TextBox126.ReadOnly = True
    '        TextBox125.ForeColor = Drawing.Color.White
    '        TextBox126.BackColor = Drawing.Color.Red
    '        TextBox126.Text = ""

    '        TextBox188.ReadOnly = True
    '        TextBox188.ForeColor = Drawing.Color.White
    '        TextBox188.BackColor = Drawing.Color.Red
    '        TextBox188.Text = ""

    '        TextBox189.ReadOnly = True
    '        TextBox189.ForeColor = Drawing.Color.White
    '        TextBox189.BackColor = Drawing.Color.Red
    '        TextBox189.Text = ""
    '        If DropDownList26.Text = "" Then
    '            DropDownList26.Focus()
    '            Return
    '        End If
    '        'If DropDownList26.Text.IndexOf(",") <> 13 Then
    '        '    DropDownList26.Text = ""
    '        '    DropDownList26.Focus()
    '        '    Return
    '        'End If
    '        conn.Open()
    '        count = 0
    '        Dim dt1 As New DataTable()
    '        da = New SqlDataAdapter("SELECT SO_NO  FROM ORDER_DETAILS  WHERE SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'", conn)
    '        count = da.Fill(dt1)
    '        conn.Close()
    '        If count = 0 Then
    '            DropDownList26.Text = ""
    '            DropDownList26.Focus()
    '            Return
    '        End If
    '        Dim working_date As Date

    '        working_date = Today.Date
    '        ''SEARCH SALE ORDER VENDER DETAILS
    '        conn.Open()
    '        mycommand.CommandText = "select SUPL_ID,SUPL_NAME,SUPL_STATE_CODE,SUPL_STATE from SUPL join order_details on order_details.PARTY_CODE=SUPL.SUPL_ID where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
    '        mycommand.Connection = conn
    '        dr = mycommand.ExecuteReader
    '        If dr.HasRows Then
    '            dr.Read()
    '            TextBox125.Text = dr.Item("SUPL_ID")
    '            TextBox126.Text = dr.Item("SUPL_NAME")
    '            TextBox188.Text = dr.Item("SUPL_STATE_CODE")
    '            TextBox189.Text = dr.Item("SUPL_STATE")
    '            dr.Close()
    '        End If
    '        conn.Close()
    '        TextBox124.Text = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim
    '        conn.Open()
    '        dt.Clear()
    '        da = New SqlDataAdapter("select DISTINCT PO_RCD_MAT .INV_NO  from PO_RCD_MAT JOIN MATERIAL ON PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE where PO_RCD_MAT .INV_NO <> '' AND PO_RCD_MAT .RCM_P is null AND PO_RCD_MAT .PO_NO='" & TextBox124.Text & "'", conn)
    '        da.Fill(dt)
    '        conn.Close()
    '        DropDownList30.Items.Clear()
    '        DropDownList30.DataSource = dt
    '        DropDownList30.DataValueField = "INV_NO"
    '        DropDownList30.DataBind()
    '        DropDownList30.Items.Add("Select")
    '        DropDownList30.SelectedValue = "Select"
    '        Panel2.Visible = True
    '        Panel8.Visible = False
    '    End If

    'End Sub

    'Protected Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
    '    If Panel1.Visible = True Then
    '        If DropDownList2.SelectedValue = "RCM For Tax Invoice" Then
    '            'CALCULATION
    '            Dim TAX_VAL, TOTAL_VAL, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, UNIT_PRICE, MAT_QTY, CGST, SGST, IGST, CESS As New Decimal(0)
    '            MAT_QTY = FormatNumber(CDec(TextBox182.Text), 2)
    '            UNIT_PRICE = FormatNumber(CDec(TextBox183.Text), 2)
    '            TAX_VAL = FormatNumber(MAT_QTY * UNIT_PRICE, 2)
    '            CGST = FormatNumber(CDec(TextBox184.Text), 2)
    '            SGST = FormatNumber(CDec(TextBox185.Text), 2)
    '            IGST = FormatNumber(CDec(TextBox186.Text), 2)
    '            CESS = FormatNumber(CDec(TextBox187.Text), 2)
    '            CGST_VAL = FormatNumber((TAX_VAL * CGST) / 100, 2)
    '            SGST_VAL = FormatNumber((TAX_VAL * SGST) / 100, 2)
    '            IGST_VAL = FormatNumber((TAX_VAL * IGST) / 100, 2)
    '            CESS_VAL = FormatNumber((TAX_VAL * CESS) / 100, 2)
    '            TOTAL_VAL = FormatNumber(TAX_VAL + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL, 2)
    '            Dim dt9 As DataTable = DirectCast(ViewState("RCM"), DataTable)
    '            dt9.Rows.Add("N/A", "N/A", "N/A", TextBox180.Text, TextBox181.Text, DropDownList32.Text.Substring(0, DropDownList32.Text.LastIndexOf(",") - 1), UNIT_PRICE, MAT_QTY, TAX_VAL, CGST, CGST_VAL, SGST, SGST_VAL, IGST, IGST_VAL, CESS, CESS_VAL, TOTAL_VAL)
    '            ViewState("RCM") = dt9
    '            Me.BINDGRID()
    '        ElseIf DropDownList2.SelectedValue = "RCM For Payment Voucher" Then
    '            'CALCULATION
    '            Dim TAX_VAL, TOTAL_VAL, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, UNIT_PRICE, MAT_QTY, CGST, SGST, IGST, CESS As New Decimal(0)
    '            MAT_QTY = FormatNumber(CDec(TextBox182.Text), 2)
    '            UNIT_PRICE = FormatNumber(0.0, 2)
    '            TAX_VAL = FormatNumber(MAT_QTY, 2)
    '            CGST = FormatNumber(CDec(TextBox184.Text), 2)
    '            SGST = FormatNumber(CDec(TextBox185.Text), 2)
    '            IGST = FormatNumber(CDec(TextBox186.Text), 2)
    '            CESS = FormatNumber(CDec(TextBox187.Text), 2)
    '            CGST_VAL = FormatNumber((TAX_VAL * CGST) / 100, 2)
    '            SGST_VAL = FormatNumber((TAX_VAL * SGST) / 100, 2)
    '            IGST_VAL = FormatNumber((TAX_VAL * IGST) / 100, 2)
    '            CESS_VAL = FormatNumber((TAX_VAL * CESS) / 100, 2)
    '            TOTAL_VAL = FormatNumber(TAX_VAL + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL, 2)
    '            Dim dt9 As DataTable = DirectCast(ViewState("RCM"), DataTable)
    '            dt9.Rows.Add("N/A", "N/A", "N/A", TextBox180.Text, TextBox181.Text, DropDownList32.Text.Substring(0, DropDownList32.Text.LastIndexOf(",") - 1), "0.00", "0.00", TAX_VAL, CGST, CGST_VAL, SGST, SGST_VAL, IGST, IGST_VAL, CESS, CESS_VAL, TOTAL_VAL)
    '            ViewState("RCM") = dt9
    '            Me.BINDGRID()



    '        End If




    '    ElseIf Panel3.Visible = True Then
    '        If DropDownList2.SelectedValue = "RCM For Tax Invoice" Then
    '            conn.Open()
    '            dt.Clear()
    '            da = New SqlDataAdapter("select PO_RCD_MAT .GARN_NO ,PO_RCD_MAT .MAT_SLNO,PO_RCD_MAT .UNIT_RATE,PO_RCD_MAT .CGST ,PO_RCD_MAT .SGST,PO_RCD_MAT .IGST,PO_RCD_MAT .CESS  ,PO_RCD_MAT .MAT_CODE ,MATERIAL.MAT_NAME ,MATERIAL.MAT_AU ,MATERIAL.CHPTR_HEAD,PO_RCD_MAT .INV_NO,'0.00' AS MAT_QTY,'0.00' AS TAX_VAL,'0.00' AS CGST_P,'0.00' AS SGST_P,'0.00' AS IGST_P,'0.00' AS CESS_P,'0.00' AS TOTAL_VAL  from PO_RCD_MAT JOIN MATERIAL ON PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE where PO_RCD_MAT.RCM='Yes' and PO_RCD_MAT .INV_NO='" & DropDownList30.SelectedValue & "' and PO_RCD_MAT .RCM_P is null AND PO_RCD_MAT .PO_NO='" & TextBox124.Text & "'", conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            GridView2.DataSource = dt
    '            GridView2.DataBind()
    '            Dim I As Integer = 0
    '            For I = 0 To GridView2.Rows.Count - 1
    '                'MATERIAL QTY

    '                Dim TRANS As String = ""
    '                Dim RCVD_QTY, CHLN_QTY, REJ_QTY, EXCES_QTY, ACCEPT_QTY As New Decimal(0)
    '                conn.Open()
    '                mycommand.CommandText = "SELECT * FROM PO_RCD_MAT WHERE GARN_NO ='" & GridView2.Rows(I).Cells(0).Text & "' AND MAT_SLNO ='" & GridView2.Rows(I).Cells(1).Text & "' AND INV_NO ='" & DropDownList30.SelectedValue & "'"
    '                mycommand.Connection = conn
    '                dr = mycommand.ExecuteReader
    '                If dr.HasRows Then
    '                    dr.Read()
    '                    TRANS = dr.Item("TRANS_WO_NO")
    '                    CHLN_QTY = dr.Item("MAT_CHALAN_QTY")
    '                    RCVD_QTY = dr.Item("MAT_RCD_QTY")
    '                    REJ_QTY = dr.Item("MAT_REJ_QTY")
    '                    EXCES_QTY = dr.Item("MAT_EXCE")
    '                    dr.Close()
    '                End If
    '                conn.Close()

    '                If TRANS = "N/A" Then
    '                    ACCEPT_QTY = 0
    '                    ACCEPT_QTY = RCVD_QTY - (REJ_QTY + EXCES_QTY)
    '                    GridView2.Rows(I).Cells(7).Text = ACCEPT_QTY
    '                Else
    '                    ACCEPT_QTY = 0
    '                    ACCEPT_QTY = CHLN_QTY - (REJ_QTY + EXCES_QTY)
    '                    GridView2.Rows(I).Cells(7).Text = ACCEPT_QTY
    '                End If

    '                'TAXABLE VALUE
    '                Dim PROV_VALUE, TRANS_SHORT, PENALITY, WT_VAR As New Decimal(0)
    '                conn.Open()
    '                mycommand.CommandText = "SELECT * FROM PO_RCD_MAT WHERE GARN_NO ='" & GridView2.Rows(I).Cells(0).Text & "' AND MAT_SLNO ='" & GridView2.Rows(I).Cells(1).Text & "' AND INV_NO ='" & DropDownList30.SelectedValue & "'"
    '                mycommand.Connection = conn
    '                dr = mycommand.ExecuteReader
    '                If dr.HasRows Then
    '                    dr.Read()
    '                    PROV_VALUE = dr.Item("PROV_VALUE")
    '                    TRANS_SHORT = dr.Item("TRANS_SHORT")
    '                    PENALITY = dr.Item("PENALITY_CHARGE")
    '                    WT_VAR = dr.Item("LOSS_TRANSPORT")
    '                    dr.Close()
    '                End If
    '                conn.Close()
    '                GridView2.Rows(I).Cells(8).Text = PROV_VALUE + TRANS_SHORT + PENALITY - WT_VAR
    '                'CGST
    '                'SGST
    '                'IGST
    '                'CESS

    '                Dim SGST, CGST, IGST, CESS As New Decimal(0)
    '                conn.Open()
    '                mycommand.CommandText = "SELECT * FROM GARN_PRICE WHERE CRR_NO ='" & GridView2.Rows(I).Cells(0).Text & "' AND MAT_SLNO ='" & GridView2.Rows(I).Cells(1).Text & "'"
    '                mycommand.Connection = conn
    '                dr = mycommand.ExecuteReader
    '                If dr.HasRows Then
    '                    dr.Read()
    '                    SGST = dr.Item("SGST")
    '                    CGST = dr.Item("CGST")
    '                    IGST = dr.Item("IGST")
    '                    CESS = dr.Item("CESS")
    '                    dr.Close()
    '                End If
    '                conn.Close()
    '                GridView2.Rows(I).Cells(11).Text = SGST
    '                GridView2.Rows(I).Cells(9).Text = CGST
    '                GridView2.Rows(I).Cells(13).Text = IGST
    '                GridView2.Rows(I).Cells(15).Text = CESS

    '                'TOTAL VALUE
    '                GridView2.Rows(I).Cells(17).Text = CDec(GridView2.Rows(I).Cells(8).Text) + CDec(GridView2.Rows(I).Cells(10).Text) + CDec(GridView2.Rows(I).Cells(12).Text) + CDec(GridView2.Rows(I).Cells(14).Text) + CDec(GridView2.Rows(I).Cells(16).Text)
    '            Next
    '        ElseIf DropDownList2.SelectedValue = "RCM For Payment Voucher" Then

    '            conn.Open()
    '            dt.Clear()
    '            da = New SqlDataAdapter("select PO_RCD_MAT .GARN_NO ,PO_RCD_MAT .MAT_SLNO,'0.00' as UNIT_RATE,PO_RCD_MAT .rcm_CGST AS CGST ,PO_RCD_MAT .RCM_SGST AS SGST,PO_RCD_MAT .RCM_IGST AS IGST,PO_RCD_MAT .RCM_CESS AS CESS  ,PO_RCD_MAT .MAT_CODE ,MATERIAL.MAT_NAME ,MATERIAL.MAT_AU ,MATERIAL.CHPTR_HEAD,PO_RCD_MAT .INV_NO,'0.00' AS MAT_QTY,LD_CHARGE + PENALITY_CHARGE AS TAX_VAL,'0.00' AS CGST_P,'0.00' AS SGST_P,'0.00' AS IGST_P,'0.00' AS CESS_P,'0.00' AS TOTAL_VAL  from PO_RCD_MAT JOIN MATERIAL ON PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE where PO_RCD_MAT .INV_NO='" & DropDownList30.SelectedValue & "' and PO_RCD_MAT .RCM_F IS NULL AND PO_RCD_MAT .PO_NO='" & TextBox124.Text & "'", conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            GridView2.DataSource = dt
    '            GridView2.DataBind()
    '            Dim I As Integer = 0
    '            For I = 0 To GridView2.Rows.Count - 1
    '                'MATERIAL QTY

    '                Dim TRANS As String = ""
    '                Dim RCVD_QTY, CHLN_QTY, REJ_QTY, EXCES_QTY, ACCEPT_QTY As New Decimal(0)
    '                conn.Open()
    '                mycommand.CommandText = "SELECT * FROM PO_RCD_MAT WHERE GARN_NO ='" & GridView2.Rows(I).Cells(0).Text & "' AND MAT_SLNO ='" & GridView2.Rows(I).Cells(1).Text & "' AND INV_NO ='" & DropDownList30.SelectedValue & "'"
    '                mycommand.Connection = conn
    '                dr = mycommand.ExecuteReader
    '                If dr.HasRows Then
    '                    dr.Read()
    '                    TRANS = dr.Item("TRANS_WO_NO")
    '                    CHLN_QTY = dr.Item("MAT_CHALAN_QTY")
    '                    RCVD_QTY = dr.Item("MAT_RCD_QTY")
    '                    REJ_QTY = dr.Item("MAT_REJ_QTY")
    '                    EXCES_QTY = dr.Item("MAT_EXCE")
    '                    dr.Close()
    '                End If
    '                conn.Close()

    '                If TRANS = "N/A" Then
    '                    ACCEPT_QTY = 0
    '                    ACCEPT_QTY = RCVD_QTY - (REJ_QTY + EXCES_QTY)
    '                    GridView2.Rows(I).Cells(7).Text = "0.00"
    '                Else
    '                    ACCEPT_QTY = 0
    '                    ACCEPT_QTY = CHLN_QTY - (REJ_QTY + EXCES_QTY)
    '                    GridView2.Rows(I).Cells(7).Text = "0.00"
    '                End If

    '                'TAXABLE VALUE
    '                Dim PENALITY, LD_CHARGE As New Decimal(0)
    '                conn.Open()
    '                mycommand.CommandText = "SELECT PENALITY_CHARGE,LD_CHARGE  FROM PO_RCD_MAT WHERE GARN_NO ='" & GridView2.Rows(I).Cells(0).Text & "' AND MAT_SLNO ='" & GridView2.Rows(I).Cells(1).Text & "' AND INV_NO ='" & DropDownList30.SelectedValue & "'"
    '                mycommand.Connection = conn
    '                dr = mycommand.ExecuteReader
    '                If dr.HasRows Then
    '                    dr.Read()
    '                    PENALITY = dr.Item("PENALITY_CHARGE")
    '                    LD_CHARGE = dr.Item("LD_CHARGE")
    '                    dr.Close()
    '                End If
    '                conn.Close()
    '                '  GridView2.Rows(I).Cells(8).Text = PENALITY - LD_CHARGE
    '                'CGST
    '                'SGST
    '                'IGST
    '                'CESS

    '                Dim SGST, CGST, IGST, CESS As New Decimal(0)
    '                conn.Open()
    '                mycommand.CommandText = "SELECT * FROM GARN_PRICE WHERE CRR_NO ='" & GridView2.Rows(I).Cells(0).Text & "' AND MAT_SLNO ='" & GridView2.Rows(I).Cells(1).Text & "'"
    '                mycommand.Connection = conn
    '                dr = mycommand.ExecuteReader
    '                If dr.HasRows Then
    '                    dr.Read()
    '                    SGST = dr.Item("SGST")
    '                    CGST = dr.Item("CGST")
    '                    IGST = dr.Item("IGST")
    '                    CESS = dr.Item("CESS")
    '                    dr.Close()
    '                End If
    '                conn.Close()
    '                GridView2.Rows(I).Cells(11).Text = SGST
    '                GridView2.Rows(I).Cells(9).Text = CGST
    '                GridView2.Rows(I).Cells(13).Text = IGST
    '                GridView2.Rows(I).Cells(15).Text = CESS

    '                'TOTAL VALUE
    '                GridView2.Rows(I).Cells(17).Text = CDec(GridView2.Rows(I).Cells(8).Text) + CDec(GridView2.Rows(I).Cells(10).Text) + CDec(GridView2.Rows(I).Cells(12).Text) + CDec(GridView2.Rows(I).Cells(14).Text) + CDec(GridView2.Rows(I).Cells(16).Text)
    '            Next
    '        End If




    '    End If








    '    Button36.Enabled = True
    'End Sub

    'Protected Sub DropDownList31_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList31.SelectedIndexChanged
    '    If DropDownList31.SelectedValue = "Select" Then
    '        DropDownList31.Focus()
    '        Return
    '    ElseIf DropDownList31.SelectedValue = "Unregistered Party" Then
    '        Label402.Visible = False
    '        DropDownList26.Visible = False
    '        Return
    '    ElseIf DropDownList31.SelectedValue = "Registered Party" Then
    '        Label402.Visible = True
    '        DropDownList26.Visible = True
    '        DropDownList26.Focus()
    '        Return
    '    End If
    'End Sub

    'Protected Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
    '    Dim DESPATCH_TYPE As Integer = 0
    '    If DropDownList33.SelectedValue = "I.P.T." Then
    '        DESPATCH_TYPE = 4
    '    ElseIf DropDownList33.SelectedValue = "Other" Then
    '        DESPATCH_TYPE = 1
    '    End If

    '    'search company profile compair gst code
    '    Dim gst_code, my_gst_code, COMM, DIVISION As New String("")
    '    conn.Open()
    '    mycommand.CommandText = "select * from comp_profile"
    '    mycommand.Connection = conn
    '    dr = mycommand.ExecuteReader
    '    If dr.HasRows Then
    '        dr.Read()
    '        my_gst_code = dr.Item("c_gst_no")
    '        COMM = dr.Item("c_comm")
    '        DIVISION = dr.Item("c_division")
    '        dr.Close()
    '    End If
    '    conn.Close()
    '    'invoice type
    '    Dim inv_type, inv_rule, inv_for, inv_type1 As String
    '    inv_type = "Tax Invoice"
    '    inv_type1 = "(For Goods under Reverse Charge)"
    '    inv_rule = "(In case of Supplies from unregistered Suppliers under Section 31(3)(f)of CGST Act 2017 read with Rule 1 of Tax Invoice ,Credit and Debit Note Rules)"
    '    inv_for = "RC15"




    '    ''INVOICE NO GENERATE
    '    Dim STR1 As String = ""
    '    If working_date.Month > 3 Then
    '        STR1 = working_date.Year
    '        STR1 = STR1.Trim.Substring(2)
    '        STR1 = STR1 & (STR1 + 1)
    '    ElseIf working_date.Month <= 3 Then
    '        STR1 = working_date.Year
    '        STR1 = STR1.Trim.Substring(2)
    '        STR1 = (STR1 - 1) & STR1
    '    End If
    '    conn.Open()
    '    Dim inv_no As String = ""
    '    Dim mc_c As New SqlCommand
    '    mc_c.CommandText = "SELECT (CASE WHEN MAX(INV_NO) IS NULL THEN 0 ELSE MAX(INV_NO) END) as inv_no  FROM RCM_INV  WHERE D_TYPE LIKE'" & inv_for & "%' AND FISCAL_YEAR =" & STR1
    '    mc_c.Connection = conn
    '    dr = mc_c.ExecuteReader
    '    If dr.HasRows Then
    '        dr.Read()
    '        inv_no = dr.Item("inv_no")
    '        dr.Close()
    '    Else
    '        dr.Close()
    '    End If
    '    conn.Close()
    '    If CInt(inv_no) = 0 Then
    '        TextBox65.Text = "0000001"
    '        TextBox177.Text = inv_for & CStr(DESPATCH_TYPE)
    '        TextBox177.ReadOnly = True
    '        TextBox65.ReadOnly = True
    '    Else
    '        str = CInt(inv_no) + 1
    '        If str.Length = 1 Then
    '            str = "000000" & CInt(inv_no) + 1
    '        ElseIf str.Length = 2 Then
    '            str = "00000" & CInt(inv_no) + 1
    '        ElseIf str.Length = 3 Then
    '            str = "0000" & CInt(inv_no) + 1
    '        ElseIf str.Length = 4 Then
    '            str = "000" & CInt(inv_no) + 1
    '        ElseIf str.Length = 5 Then
    '            str = "00" & CInt(inv_no) + 1
    '        ElseIf str.Length = 6 Then
    '            str = "0" & CInt(inv_no) + 1
    '        ElseIf str.Length = 7 Then
    '            str = CInt(inv_no) + 1
    '        End If
    '        TextBox65.Text = str
    '        TextBox177.Text = inv_for & CStr(DESPATCH_TYPE)
    '        TextBox177.ReadOnly = True
    '        TextBox65.ReadOnly = True
    '    End If









    '    'SAVE RCM_INV
    '    Dim supl_state, supl_state_code, gst_no As New String("")
    '    Dim SUPL_INV As String = ""
    '    If Panel1.Visible = True Then
    '        SUPL_INV = TextBox178.Text
    '        supl_state = TextBox189.Text
    '        supl_state_code = TextBox188.Text
    '    ElseIf Panel3.Visible = True Then
    '        SUPL_INV = DropDownList30.SelectedValue
    '        supl_state = TextBox189.Text
    '        supl_state_code = TextBox188.Text
    '    End If








    '    Dim so_date, SO_ACTUAL_DATE, amd_date As Date
    '    Dim actual_so, amd_no, party_code, CONSIGN_CODE, PLACE_OF_SUPPLY As New String("")
    '    Dim taxable_rate_unit, cgst_amt, sgst_amt, igst_amt, cess_amt, net_pay As Decimal

    '    If DropDownList31.SelectedValue = "Unregistered Party" Then
    '        so_date = Today.Date
    '        SO_ACTUAL_DATE = Today.Date
    '    ElseIf DropDownList31.SelectedValue = "Registered Party" Then
    '        'search order_details
    '        conn.Open()
    '        mycommand.CommandText = "select SO_ACTUAL ,SO_DATE ,SO_ACTUAL_DATE,DESTINATION from ORDER_DETAILS where SO_NO ='" & TextBox124.Text & "'"
    '        mycommand.Connection = conn
    '        dr = mycommand.ExecuteReader
    '        If dr.HasRows Then
    '            dr.Read()
    '            actual_so = dr.Item("SO_ACTUAL")
    '            so_date = dr.Item("SO_DATE")
    '            SO_ACTUAL_DATE = dr.Item("SO_ACTUAL_DATE")
    '            PLACE_OF_SUPPLY = dr.Item("DESTINATION")
    '            dr.Close()
    '        Else
    '            dr.Close()
    '        End If
    '        conn.Close()
    '        amd_no = "N/A"
    '        amd_date = Today.Date
    '        party_code = TextBox125.Text
    '        CONSIGN_CODE = TextBox125.Text
    '    End If



    '    Dim i As Integer = 0
    '    For i = 0 To GridView2.Rows.Count - 1
    '        conn.Open()
    '        Dim Query As String = "Insert Into RCM_INV(EMP_ID,INV_NO,INV_DATE,PO_NO,PO_DATE,SUPL_INV,RCM_BASIS,SUPL_STATE,SUPL_STATE_CODE,SUPL_DETAILS,HSN_CODE,ITEM_SLNO,ITEM_CODE,ITEM_DESC,ITEM_AU,ITEM_QTY,UNIT_RATE,TAXABLE_VALUE,CGST,CGST_AMT,SGST,SGST_AMT,IGST,IGST_AMT,CESS,CESS_AMT,TOTAL_VALUE,ADV_PAID,TAX_RCM,NET_PAY,COMMISSION,DIVISION,NOTIFICATION_DESC,INV_FOR,INV_TYPE,INV_RULE,PARTY_TYPE,GARN_NO,FISCAL_YEAR,D_TYPE)VALUES(@EMP_ID,@INV_NO,@INV_DATE,@PO_NO,@PO_DATE,@SUPL_INV,@RCM_BASIS,@SUPL_STATE,@SUPL_STATE_CODE,@SUPL_DETAILS,@HSN_CODE,@ITEM_SLNO,@ITEM_CODE,@ITEM_DESC,@ITEM_AU,@ITEM_QTY,@UNIT_RATE,@TAXABLE_VALUE,@CGST,@CGST_AMT,@SGST,@SGST_AMT,@IGST,@IGST_AMT,@CESS,@CESS_AMT,@TOTAL_VALUE,@ADV_PAID,@TAX_RCM,@NET_PAY,@COMMISSION,@DIVISION,@NOTIFICATION_DESC,@INV_FOR,@INV_TYPE,@INV_RULE,@PARTY_TYPE,@GARN_NO,@FISCAL_YEAR,@D_TYPE)"
    '        Dim cmd As New SqlCommand(Query, conn)
    '        cmd.Parameters.AddWithValue("@INV_NO", TextBox65.Text)
    '        cmd.Parameters.AddWithValue("@D_TYPE", TextBox177.Text)
    '        cmd.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(CDate(working_date), "dd-MM-yyyy", provider))
    '        cmd.Parameters.AddWithValue("@PO_NO", TextBox124.Text)
    '        cmd.Parameters.AddWithValue("@PO_DATE", "")
    '        cmd.Parameters.AddWithValue("@SUPL_INV", SUPL_INV)
    '        cmd.Parameters.AddWithValue("@RCM_BASIS", "Yes")
    '        cmd.Parameters.AddWithValue("@SUPL_STATE", supl_state)
    '        cmd.Parameters.AddWithValue("@SUPL_STATE_CODE", supl_state_code)
    '        cmd.Parameters.AddWithValue("@SUPL_DETAILS", TextBox126.Text)
    '        cmd.Parameters.AddWithValue("@HSN_CODE", GridView2.Rows(i).Cells(5).Text)
    '        cmd.Parameters.AddWithValue("@ITEM_SLNO", GridView2.Rows(i).Cells(1).Text)
    '        cmd.Parameters.AddWithValue("@ITEM_CODE", GridView2.Rows(i).Cells(2).Text)
    '        cmd.Parameters.AddWithValue("@ITEM_DESC", GridView2.Rows(i).Cells(3).Text)
    '        cmd.Parameters.AddWithValue("@ITEM_AU", GridView2.Rows(i).Cells(4).Text)
    '        cmd.Parameters.AddWithValue("@ITEM_QTY", CDec(GridView2.Rows(i).Cells(7).Text))
    '        cmd.Parameters.AddWithValue("@UNIT_RATE", CDec(GridView2.Rows(i).Cells(6).Text))
    '        cmd.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(GridView2.Rows(i).Cells(8).Text))
    '        cmd.Parameters.AddWithValue("@CGST", CDec(GridView2.Rows(i).Cells(9).Text))
    '        cmd.Parameters.AddWithValue("@CGST_AMT", CDec(GridView2.Rows(i).Cells(10).Text))
    '        cmd.Parameters.AddWithValue("@SGST", CDec(GridView2.Rows(i).Cells(11).Text))
    '        cmd.Parameters.AddWithValue("@SGST_AMT", CDec(GridView2.Rows(i).Cells(12).Text))
    '        cmd.Parameters.AddWithValue("@IGST", CDec(GridView2.Rows(i).Cells(13).Text))
    '        cmd.Parameters.AddWithValue("@IGST_AMT", CDec(GridView2.Rows(i).Cells(14).Text))
    '        cmd.Parameters.AddWithValue("@CESS", CDec(GridView2.Rows(i).Cells(15).Text))
    '        cmd.Parameters.AddWithValue("@CESS_AMT", CDec(GridView2.Rows(i).Cells(16).Text))
    '        cmd.Parameters.AddWithValue("@TOTAL_VALUE", CDec(GridView2.Rows(i).Cells(17).Text))
    '        cmd.Parameters.AddWithValue("@ADV_PAID", CDec(GridView2.Rows(i).Cells(8).Text))
    '        cmd.Parameters.AddWithValue("@TAX_RCM", 0)
    '        cmd.Parameters.AddWithValue("@NET_PAY", CDec(GridView2.Rows(i).Cells(17).Text) - CDec(GridView2.Rows(i).Cells(8).Text))
    '        cmd.Parameters.AddWithValue("@COMMISSION", COMM)
    '        cmd.Parameters.AddWithValue("@DIVISION", DIVISION)
    '        cmd.Parameters.AddWithValue("@NOTIFICATION_DESC", TextBox64.Text)
    '        cmd.Parameters.AddWithValue("@INV_FOR", inv_type1)
    '        cmd.Parameters.AddWithValue("@INV_TYPE", inv_type)
    '        cmd.Parameters.AddWithValue("@INV_RULE", inv_rule)
    '        cmd.Parameters.AddWithValue("@PARTY_TYPE", DropDownList31.Text)
    '        cmd.Parameters.AddWithValue("@GARN_NO", GridView2.Rows(i).Cells(0).Text)
    '        cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
    '        cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))

    '        cmd.ExecuteReader()
    '        cmd.Dispose()
    '        conn.Close()
    '        taxable_rate_unit = taxable_rate_unit + CDec(GridView2.Rows(i).Cells(8).Text)
    '        net_pay = net_pay + CDec(GridView2.Rows(i).Cells(10).Text) + CDec(GridView2.Rows(i).Cells(12).Text) + CDec(GridView2.Rows(i).Cells(14).Text) + CDec(GridView2.Rows(i).Cells(16).Text)

    '        cgst_amt = cgst_amt + CDec(GridView2.Rows(i).Cells(10).Text)
    '        sgst_amt = sgst_amt + CDec(GridView2.Rows(i).Cells(12).Text)
    '        igst_amt = igst_amt + CDec(GridView2.Rows(i).Cells(14).Text)
    '        cess_amt = cess_amt + CDec(GridView2.Rows(i).Cells(16).Text)
    '    Next




    '    If DropDownList2.SelectedValue = "RCM For Payment Voucher" Then
    '        'save despatch



    '        conn.Open()
    '        Dim QUARY As String = ""
    '        QUARY = "Insert Into DESPATCH(INV_STATUS,SO_NO ,SO_DATE ,PO_NO ,PO_DATE ,AMD_NO ,AMD_DATE ,TRANS_WO ,TRANS_SLNO ,TRANS_NAME ,TRUCK_NO ,PARTY_CODE ,CONSIGN_CODE ,MAT_VOCAB ,MAT_SLNO ,INV_NO,INV_DATE,D_TYPE,CHPTR_HEAD ,FISCAL_YEAR,INV_ISSUE ,PLACE_OF_SUPPLY ,BILL_PARTY_ADD ,CON_PARTY_ADD ,B_STATE ,B_STATE_CODE ,C_STATE ,C_STATE_CODE ,BILL_PARTY_GST_N ,CON_PARTY_GST_N ,NEGOTIATING_BRANCH ,PAYING_AUTH ,RR_NO ,RR_DATE ,TOTAL_WEIGHT ,ACC_UNIT ,PURITY ,TC_NO ,FINANCE_ARRENGE ,MILL_CODE ,DA_NO ,CONTRACT_NO ,RCD_VOUCHER_NO ,RCD_VOUCHER_DATE ,ROUT_CARD_NO ,DESPATCH_TYPE ,TAX_REVERS_CHARGE ,RLY_INV_NO ,RLY_INV_DATE ,FRT_WT_AMT ,TOTAL_PCS,P_CODE ,P_DESC ,D1 ,D2 ,D3 ,D4 ,BASE_PRICE ,PACK_PRICE ,PACK_TYPE ,QLTY_PRICE ,SEC_PRICE ,TOTAL_TDC ,UNIT_PRICE ,SY_MARGIN ,PPM_FRT ,FRT_TYPE ,RLY_ROAD_FRT ,TOTAL_RATE_UNIT ,REBATE_UNIT ,REBATE_TYPE ,TAXABLE_RATE_UNIT ,TOTAL_QTY ,TAXABLE_VALUE ,CGST_RATE ,CGST_AMT ,SGST_RATE ,SGST_AMT ,IGST_RATE ,IGST_AMT ,CESS_RATE ,CESS_AMT ,TERM_RATE ,TERM_AMT ,TOTAL_AMT ,LESS_LOAD_AMT ,TOTAL_BAG ,ADVANCE_PAID ,GST_PAID_ADV ,NET_PAY ,NOTIFICATION_TEXT ,COMM ,DIV_ADD ,INV_TYPE ,INV_RULE ,FORM_NAME,EMP_ID)values(@INV_STATUS,@SO_NO ,@SO_DATE ,@PO_NO ,@PO_DATE ,@AMD_NO ,@AMD_DATE ,@TRANS_WO ,@TRANS_SLNO ,@TRANS_NAME ,@TRUCK_NO ,@PARTY_CODE ,@CONSIGN_CODE ,@MAT_VOCAB ,@MAT_SLNO ,@INV_NO,@INV_DATE,@D_TYPE,@CHPTR_HEAD ,@FISCAL_YEAR,@INV_ISSUE ,@PLACE_OF_SUPPLY ,@BILL_PARTY_ADD ,@CON_PARTY_ADD ,@B_STATE ,@B_STATE_CODE ,@C_STATE ,@C_STATE_CODE ,@BILL_PARTY_GST_N ,@CON_PARTY_GST_N ,@NEGOTIATING_BRANCH ,@PAYING_AUTH ,@RR_NO ,@RR_DATE ,@TOTAL_WEIGHT ,@ACC_UNIT ,@PURITY ,@TC_NO ,@FINANCE_ARRENGE ,@MILL_CODE ,@DA_NO ,@CONTRACT_NO ,@RCD_VOUCHER_NO ,@RCD_VOUCHER_DATE ,@ROUT_CARD_NO ,@DESPATCH_TYPE ,@TAX_REVERS_CHARGE ,@RLY_INV_NO ,@RLY_INV_DATE ,@FRT_WT_AMT ,@TOTAL_PCS,@P_CODE ,@P_DESC ,@D1 ,@D2 ,@D3 ,@D4 ,@BASE_PRICE ,@PACK_PRICE ,@PACK_TYPE ,@QLTY_PRICE ,@SEC_PRICE ,@TOTAL_TDC ,@UNIT_PRICE ,@SY_MARGIN ,@PPM_FRT ,@FRT_TYPE ,@RLY_ROAD_FRT ,@TOTAL_RATE_UNIT ,@REBATE_UNIT ,@REBATE_TYPE ,@TAXABLE_RATE_UNIT ,@TOTAL_QTY ,@TAXABLE_VALUE ,@CGST_RATE ,@CGST_AMT ,@SGST_RATE ,@SGST_AMT ,@IGST_RATE ,@IGST_AMT ,@CESS_RATE ,@CESS_AMT ,@TERM_RATE ,@TERM_AMT ,@TOTAL_AMT ,@LESS_LOAD_AMT ,@TOTAL_BAG ,@ADVANCE_PAID ,@GST_PAID_ADV ,@NET_PAY ,@NOTIFICATION_TEXT ,@COMM ,@DIV_ADD ,@INV_TYPE ,@INV_RULE ,@FORM_NAME,@EMP_ID)"
    '        Dim cmd1 As New SqlCommand(QUARY, conn)
    '        cmd1.Parameters.AddWithValue("@SO_NO", TextBox124.Text)
    '        cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
    '        cmd1.Parameters.AddWithValue("@PO_NO", actual_so)
    '        cmd1.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(SO_ACTUAL_DATE.Day & "-" & SO_ACTUAL_DATE.Month & "-" & SO_ACTUAL_DATE.Year), "dd-MM-yyyy", provider))
    '        cmd1.Parameters.AddWithValue("@AMD_NO", amd_no)
    '        cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(amd_date, "dd-MM-yyyy", provider))
    '        cmd1.Parameters.AddWithValue("@TRANS_WO", "N/A")
    '        cmd1.Parameters.AddWithValue("@TRANS_SLNO", "N/A")
    '        cmd1.Parameters.AddWithValue("@TRANS_NAME", "N/A")
    '        cmd1.Parameters.AddWithValue("@TRUCK_NO", "N/A")
    '        cmd1.Parameters.AddWithValue("@ED_COMDT", "Agreeing To Tolerate An Act")
    '        cmd1.Parameters.AddWithValue("@PARTY_CODE", party_code)
    '        cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CONSIGN_CODE)
    '        cmd1.Parameters.AddWithValue("@MAT_VOCAB", "N/A")
    '        cmd1.Parameters.AddWithValue("@MAT_SLNO", 0)
    '        cmd1.Parameters.AddWithValue("@INV_NO", TextBox65.Text)
    '        cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
    '        cmd1.Parameters.AddWithValue("@CHPTR_HEAD", "999794")
    '        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
    '        cmd1.Parameters.AddWithValue("@INV_ISSUE", Now)
    '        cmd1.Parameters.AddWithValue("@PLACE_OF_SUPPLY", PLACE_OF_SUPPLY)
    '        cmd1.Parameters.AddWithValue("@BILL_PARTY_ADD", TextBox126.Text)
    '        cmd1.Parameters.AddWithValue("@CON_PARTY_ADD", TextBox126.Text)
    '        cmd1.Parameters.AddWithValue("@B_STATE", supl_state)
    '        cmd1.Parameters.AddWithValue("@B_STATE_CODE", supl_state_code)
    '        cmd1.Parameters.AddWithValue("@C_STATE", supl_state)
    '        cmd1.Parameters.AddWithValue("@C_STATE_CODE", supl_state_code)
    '        cmd1.Parameters.AddWithValue("@BILL_PARTY_GST_N", "N/A")
    '        cmd1.Parameters.AddWithValue("@CON_PARTY_GST_N", "N/A")
    '        cmd1.Parameters.AddWithValue("@NEGOTIATING_BRANCH", "")
    '        cmd1.Parameters.AddWithValue("@PAYING_AUTH", "")
    '        cmd1.Parameters.AddWithValue("@RR_NO", "")
    '        cmd1.Parameters.AddWithValue("@RR_DATE", "")
    '        cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT ", 0)
    '        cmd1.Parameters.AddWithValue("@D_TYPE", inv_for & CStr(DESPATCH_TYPE))
    '        cmd1.Parameters.AddWithValue("@ACC_UNIT", "Service")
    '        cmd1.Parameters.AddWithValue("@PURITY", CDec(0))
    '        cmd1.Parameters.AddWithValue("@TC_NO", "")
    '        cmd1.Parameters.AddWithValue("@FINANCE_ARRENGE", "N/A")
    '        cmd1.Parameters.AddWithValue("@MILL_CODE", "")
    '        cmd1.Parameters.AddWithValue("@DA_NO", "")
    '        cmd1.Parameters.AddWithValue("@CONTRACT_NO", "")
    '        cmd1.Parameters.AddWithValue("@RCD_VOUCHER_NO", "N/A")
    '        cmd1.Parameters.AddWithValue("@RCD_VOUCHER_DATE", "N/A")
    '        cmd1.Parameters.AddWithValue("@ROUT_CARD_NO", "N/A")
    '        cmd1.Parameters.AddWithValue("@DESPATCH_TYPE", "")
    '        cmd1.Parameters.AddWithValue("@TAX_REVERS_CHARGE", "YES")
    '        cmd1.Parameters.AddWithValue("@RLY_INV_NO", "N/A")
    '        cmd1.Parameters.AddWithValue("@RLY_INV_DATE", "N/A")
    '        cmd1.Parameters.AddWithValue("@FRT_WT_AMT", CDec(0.0))
    '        cmd1.Parameters.AddWithValue("@TOTAL_PCS", 0)
    '        cmd1.Parameters.AddWithValue("@P_CODE ", "")
    '        cmd1.Parameters.AddWithValue("@P_DESC", "L.D. / PENALITY")
    '        cmd1.Parameters.AddWithValue("@D1", "")
    '        cmd1.Parameters.AddWithValue("@D2", "")
    '        cmd1.Parameters.AddWithValue("@D3", "")
    '        cmd1.Parameters.AddWithValue("@D4", "")
    '        cmd1.Parameters.AddWithValue("@PACK_TYPE", "/ Mt")
    '        cmd1.Parameters.AddWithValue("@FRT_TYPE", "/ Mt")
    '        cmd1.Parameters.AddWithValue("@REBATE_TYPE ", "/ Mt")
    '        cmd1.Parameters.AddWithValue("@TOTAL_QTY", CDec(1))
    '        cmd1.Parameters.AddWithValue("@BASE_PRICE", 0)
    '        cmd1.Parameters.AddWithValue("@PACK_PRICE", 0)
    '        cmd1.Parameters.AddWithValue("@QLTY_PRICE", 0.0)
    '        cmd1.Parameters.AddWithValue("@SEC_PRICE", 0.0)
    '        cmd1.Parameters.AddWithValue("@TOTAL_TDC", 0.0)
    '        cmd1.Parameters.AddWithValue("@UNIT_PRICE", 0)
    '        cmd1.Parameters.AddWithValue("@SY_MARGIN", 0.0)
    '        cmd1.Parameters.AddWithValue("@PPM_FRT", 0.0)
    '        cmd1.Parameters.AddWithValue("@RLY_ROAD_FRT", 0.0)
    '        cmd1.Parameters.AddWithValue("@TOTAL_RATE_UNIT", 0.0)
    '        cmd1.Parameters.AddWithValue("@REBATE_UNIT", 0.0)
    '        cmd1.Parameters.AddWithValue("@TAXABLE_RATE_UNIT", taxable_rate_unit)
    '        cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", taxable_rate_unit)
    '        cmd1.Parameters.AddWithValue("@CGST_RATE", 0.0)
    '        cmd1.Parameters.AddWithValue("@CGST_AMT", cgst_amt)
    '        cmd1.Parameters.AddWithValue("@SGST_RATE", 0)
    '        cmd1.Parameters.AddWithValue("@SGST_AMT", sgst_amt)
    '        cmd1.Parameters.AddWithValue("@IGST_RATE", 0)
    '        cmd1.Parameters.AddWithValue("@IGST_AMT", igst_amt)
    '        cmd1.Parameters.AddWithValue("@CESS_RATE", 0)
    '        cmd1.Parameters.AddWithValue("@CESS_AMT", cess_amt)
    '        cmd1.Parameters.AddWithValue("@TERM_RATE", 0)
    '        cmd1.Parameters.AddWithValue("@TERM_AMT", 0)
    '        cmd1.Parameters.AddWithValue("@TOTAL_AMT", taxable_rate_unit + cgst_amt + sgst_amt + igst_amt + cess_amt)
    '        cmd1.Parameters.AddWithValue("@LESS_LOAD_AMT", 0.0)
    '        cmd1.Parameters.AddWithValue("@ADVANCE_PAID", taxable_rate_unit)
    '        cmd1.Parameters.AddWithValue("@GST_PAID_ADV", 0)
    '        cmd1.Parameters.AddWithValue("@NET_PAY", net_pay)
    '        cmd1.Parameters.AddWithValue("@COMM", COMM)
    '        cmd1.Parameters.AddWithValue("@DIV_ADD", DIVISION)
    '        cmd1.Parameters.AddWithValue("@INV_TYPE", inv_type & " " & inv_type1)
    '        cmd1.Parameters.AddWithValue("@INV_RULE", inv_rule)
    '        cmd1.Parameters.AddWithValue("@FORM_NAME", "")
    '        cmd1.Parameters.AddWithValue("@TOTAL_BAG", 0)
    '        cmd1.Parameters.AddWithValue("@NOTIFICATION_TEXT", TextBox64.Text)
    '        cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
    '        cmd1.Parameters.AddWithValue("@INV_STATUS", "ACTIVE")
    '        cmd1.ExecuteReader()
    '        cmd1.Dispose()
    '        conn.Close()

    '        'save print invoice
    '        ''insert inv_print
    '        conn.Open()
    '        QUARY = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
    '        Dim scmd As New SqlCommand(QUARY, conn)
    '        scmd.Parameters.AddWithValue("@INV_NO", inv_for & CStr(DESPATCH_TYPE) & TextBox65.Text)
    '        scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
    '        scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
    '        scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
    '        scmd.Parameters.AddWithValue("@F_YEAR", STR1)
    '        scmd.ExecuteReader()
    '        scmd.Dispose()
    '        conn.Close()


    '    ElseIf DropDownList2.SelectedValue = "RCM For Tax Invoice" Then

    '    End If




    '    Dim QUARY1 As String = ""
    '    If DropDownList31.SelectedValue = "Unregistered Party" Then

    '    ElseIf DropDownList31.SelectedValue = "Registered Party" Then
    '        Dim SUPL_INVOICENO As String = ""
    '        If Panel1.Visible = True Then
    '            SUPL_INVOICENO = TextBox178.Text
    '        ElseIf Panel3.Visible = True Then
    '            SUPL_INVOICENO = DropDownList30.SelectedValue
    '        End If
    '        Dim N As Integer = 0
    '        For N = 0 To GridView2.Rows.Count - 1


    '            If DropDownList2.SelectedValue = "RCM For Payment Voucher" Then

    '                'UPDATE PO_RCD_MAT RCM_F
    '                conn.Open()
    '                QUARY1 = ""
    '                QUARY1 = "UPDATE PO_RCD_MAT SET RCM_F ='P' WHERE GARN_NO ='" & GridView2.Rows(N).Cells(0).Text & "' AND INV_NO ='" & SUPL_INVOICENO & "' AND MAT_SLNO ='" & GridView2.Rows(N).Cells(1).Text & "' AND PO_NO ='" & TextBox124.Text & "'"
    '                Dim cmdd As New SqlCommand(QUARY1, conn)
    '                cmdd.ExecuteReader()
    '                cmdd.Dispose()
    '                conn.Close()

    '            ElseIf DropDownList2.SelectedValue = "RCM For Tax Invoice" Then

    '                'UPDATE PO_RCD_MAT RCM_F
    '                conn.Open()
    '                QUARY1 = ""
    '                QUARY1 = "UPDATE PO_RCD_MAT SET RCM_P ='P' WHERE GARN_NO ='" & GridView2.Rows(N).Cells(0).Text & "' AND INV_NO ='" & SUPL_INVOICENO & "' AND MAT_SLNO ='" & GridView2.Rows(N).Cells(1).Text & "' AND PO_NO ='" & TextBox124.Text & "'"
    '                Dim cmdd As New SqlCommand(QUARY1, conn)
    '                cmdd.ExecuteReader()
    '                cmdd.Dispose()
    '                conn.Close()
    '            End If
    '        Next

    '    End If

    '    Button36.Enabled = False
    'End Sub


    'Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
    '    If DropDownList2.SelectedValue = "Select" Then
    '        DropDownList2.Focus()
    '        Panel1.Visible = False
    '        Return
    '    End If
    '    If DropDownList31.SelectedValue = "Unregistered Party" Then
    '        If DropDownList2.SelectedValue = "RCM For Tax Invoice" Then
    '            Label477.Visible = True
    '            TextBox183.Visible = True
    '            Label476.Text = "Mat. Qty."
    '        ElseIf DropDownList2.SelectedValue = "RCM For Payment Voucher" Then
    '            Label477.Visible = False
    '            TextBox183.Visible = False
    '            Label476.Text = "Taxable Value"
    '        End If
    '        Panel3.Visible = False
    '        Panel1.Visible = True
    '    ElseIf DropDownList31.SelectedValue = "Registered Party" Then
    '        Panel1.Visible = False
    '        Panel3.Visible = True
    '    End If

    'End Sub

    'Protected Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
    '    Dim dt2 As New DataTable()
    '    dt2.Columns.AddRange(New DataColumn(17) {New DataColumn("GARN_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("CHPTR_HEAD"), New DataColumn("UNIT_RATE"), New DataColumn("MAT_QTY"), New DataColumn("TAX_VAL"), New DataColumn("CGST_P"), New DataColumn("CGST"), New DataColumn("SGST_P"), New DataColumn("SGST"), New DataColumn("IGST_P"), New DataColumn("IGST"), New DataColumn("CESS_P"), New DataColumn("CESS"), New DataColumn("TOTAL_VAL")})
    '    ViewState("RCM") = dt2
    '    Me.BINDGRID()
    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Panel2.Visible = False
    '    Panel8.Visible = True
    '    Dim dt2 As New DataTable()
    '    dt2.Columns.AddRange(New DataColumn(17) {New DataColumn("GARN_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("CHPTR_HEAD"), New DataColumn("UNIT_RATE"), New DataColumn("MAT_QTY"), New DataColumn("TAX_VAL"), New DataColumn("CGST_P"), New DataColumn("CGST"), New DataColumn("SGST_P"), New DataColumn("SGST"), New DataColumn("IGST_P"), New DataColumn("IGST"), New DataColumn("CESS_P"), New DataColumn("CESS"), New DataColumn("TOTAL_VAL")})
    '    ViewState("RCM") = dt2
    '    Me.BINDGRID()
    '    Panel8.Visible = True
    '    Panel2.Visible = False
    'End Sub

    'Protected Sub save_ledger(so_no As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
    '    Dim working_date As Date

    '    working_date = Today.Date
    '    If price > 0 Then
    '        Dim STR1 As String = ""
    '        If working_date.Month > 3 Then
    '            STR1 = working_date.Year
    '            STR1 = STR1.Trim.Substring(2)
    '            STR1 = STR1 & (STR1 + 1)
    '        ElseIf working_date.Month <= 3 Then
    '            STR1 = working_date.Year
    '            STR1 = STR1.Trim.Substring(2)
    '            STR1 = (STR1 - 1) & STR1
    '        End If
    '        Dim month1 As Integer
    '        month1 = working_date.Month
    '        Dim qtr1 As String = ""
    '        If month1 = 4 Or month1 = 5 Or month1 = 6 Then
    '            qtr1 = "Q1"
    '        ElseIf month1 = 7 Or month1 = 8 Or month1 = 9 Then
    '            qtr1 = "Q2"
    '        ElseIf month1 = 10 Or month1 = 11 Or month1 = 12 Then
    '            qtr1 = "Q3"
    '        ElseIf month1 = 1 Or month1 = 2 Or month1 = 3 Then
    '            qtr1 = "Q4"
    '        End If
    '        Dim dr_value, cr_value As Decimal
    '        dr_value = 0
    '        cr_value = 0
    '        If ac_term = "Dr" Then
    '            dr_value = price
    '            cr_value = 0.0
    '        ElseIf ac_term = "Cr" Then
    '            dr_value = 0.0
    '            cr_value = price
    '        End If
    '        conn.Open()
    '        Dim cmd As New SqlCommand
    '        Dim Query As String = "Insert Into LEDGER(PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
    '        cmd = New SqlCommand(Query, conn)
    '        cmd.Parameters.AddWithValue("@PO_NO", so_no)
    '        cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", inv_no)
    '        cmd.Parameters.AddWithValue("@SUPL_ID", dt_id)
    '        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
    '        cmd.Parameters.AddWithValue("@PERIOD", qtr1)
    '        cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date)
    '        cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
    '        cmd.Parameters.AddWithValue("@AC_NO", ac_head)
    '        cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
    '        cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
    '        cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
    '        cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
    '        cmd.ExecuteReader()
    '        cmd.Dispose()
    '        conn.Close()
    '    End If
    'End Sub




    ''''''''''''''''''======================='''''''''''''''''''''''''''
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
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
    Dim taxable_rate_unit, cgst_amt, sgst_amt, igst_amt, cess_amt, net_pay As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub BINDGRID()
        GridView2.DataSource = DirectCast(ViewState("RCM"), DataTable)
        GridView2.DataBind()
    End Sub
    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim dt2 As New DataTable()
        dt2.Columns.AddRange(New DataColumn(17) {New DataColumn("GARN_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("CHPTR_HEAD"), New DataColumn("UNIT_RATE"), New DataColumn("MAT_QTY"), New DataColumn("TAX_VAL"), New DataColumn("CGST_P"), New DataColumn("CGST"), New DataColumn("SGST_P"), New DataColumn("SGST"), New DataColumn("IGST_P"), New DataColumn("IGST"), New DataColumn("CESS_P"), New DataColumn("CESS"), New DataColumn("TOTAL_VAL")})
        ViewState("RCM") = dt2
        Me.BINDGRID()
        If DropDownList31.SelectedValue = "Select" Then
            DropDownList31.Focus()
            Return
        ElseIf DropDownList31.SelectedValue = "Unregistered Party" Then
            TextBox124.Text = "N/A"
            TextBox125.Text = "N/A"
            TextBox126.ReadOnly = False
            TextBox125.ForeColor = Drawing.Color.Black
            TextBox126.BackColor = Drawing.Color.White
            TextBox126.Text = ""

            TextBox188.ReadOnly = False
            TextBox188.ForeColor = Drawing.Color.Black
            TextBox188.BackColor = Drawing.Color.White
            TextBox188.Text = ""

            TextBox189.ReadOnly = False
            TextBox189.ForeColor = Drawing.Color.Black
            TextBox189.BackColor = Drawing.Color.White
            TextBox189.Text = ""
            Panel2.Visible = True
            Panel8.Visible = False
            Panel1.Visible = True
            Panel3.Visible = False
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (CHPT_CODE + ' , ' + CHPT_NAME ) as chpt from CHPTR_HEADING order by CHPT_CODE", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList32.Items.Clear()
            DropDownList32.DataSource = dt
            DropDownList32.DataValueField = "chpt"
            DropDownList32.DataBind()
            DropDownList32.Items.Add("Select")
            DropDownList32.SelectedValue = "Select"
        ElseIf DropDownList31.SelectedValue = "Registered Party" Then
            Panel1.Visible = False
            Panel3.Visible = True
            TextBox126.ReadOnly = True
            TextBox125.ForeColor = Drawing.Color.White
            TextBox126.BackColor = Drawing.Color.Red
            TextBox126.Text = ""

            TextBox188.ReadOnly = True
            TextBox188.ForeColor = Drawing.Color.White
            TextBox188.BackColor = Drawing.Color.Red
            TextBox188.Text = ""

            TextBox189.ReadOnly = True
            TextBox189.ForeColor = Drawing.Color.White
            TextBox189.BackColor = Drawing.Color.Red
            TextBox189.Text = ""
            If DropDownList26.Text = "" Then
                DropDownList26.Focus()
                Return
            End If

            conn.Open()
            count = 0
            Dim dt1 As New DataTable()
            da = New SqlDataAdapter("SELECT SO_NO  FROM ORDER_DETAILS  WHERE SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'", conn)
            count = da.Fill(dt1)
            conn.Close()
            If count = 0 Then
                DropDownList26.Text = ""
                DropDownList26.Focus()
                Return
            End If
            Dim working_date As Date

            working_date = Today.Date
            ''SEARCH SALE ORDER VENDER DETAILS
            conn.Open()
            mycommand.CommandText = "select SUPL_ID,SUPL_NAME,SUPL_STATE_CODE,SUPL_STATE from SUPL join order_details on order_details.PARTY_CODE=SUPL.SUPL_ID where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox125.Text = dr.Item("SUPL_ID")
                TextBox126.Text = dr.Item("SUPL_NAME")
                TextBox188.Text = dr.Item("SUPL_STATE_CODE")
                TextBox189.Text = dr.Item("SUPL_STATE")
                dr.Close()
            End If
            conn.Close()
            TextBox124.Text = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim

            Panel2.Visible = True
            Panel8.Visible = False
        End If

    End Sub

    Protected Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        If Panel1.Visible = True Then
            If DropDownList2.SelectedValue = "RCM For Tax Invoice" Then
                'CALCULATION
                Dim TAX_VAL, TOTAL_VAL, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, UNIT_PRICE, MAT_QTY, CGST, SGST, IGST, CESS As New Decimal(0)
                MAT_QTY = FormatNumber(CDec(TextBox182.Text), 2)
                UNIT_PRICE = FormatNumber(CDec(TextBox183.Text), 2)
                TAX_VAL = FormatNumber(MAT_QTY * UNIT_PRICE, 2)
                CGST = FormatNumber(CDec(TextBox184.Text), 2)
                SGST = FormatNumber(CDec(TextBox185.Text), 2)
                IGST = FormatNumber(CDec(TextBox186.Text), 2)
                CESS = FormatNumber(CDec(TextBox187.Text), 2)
                CGST_VAL = FormatNumber((TAX_VAL * CGST) / 100, 2)
                SGST_VAL = FormatNumber((TAX_VAL * SGST) / 100, 2)
                IGST_VAL = FormatNumber((TAX_VAL * IGST) / 100, 2)
                CESS_VAL = FormatNumber((TAX_VAL * CESS) / 100, 2)
                TOTAL_VAL = FormatNumber(TAX_VAL + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL, 2)
                Dim dt9 As DataTable = DirectCast(ViewState("RCM"), DataTable)
                dt9.Rows.Add("N/A", "N/A", "N/A", TextBox180.Text, TextBox181.Text, DropDownList32.Text.Substring(0, DropDownList32.Text.LastIndexOf(",") - 1), UNIT_PRICE, MAT_QTY, TAX_VAL, CGST, CGST_VAL, SGST, SGST_VAL, IGST, IGST_VAL, CESS, CESS_VAL, TOTAL_VAL)
                ViewState("RCM") = dt9
                Me.BINDGRID()
            ElseIf DropDownList2.SelectedValue = "RCM For Payment Voucher" Then
                'CALCULATION
                Dim TAX_VAL, TOTAL_VAL, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, UNIT_PRICE, MAT_QTY, CGST, SGST, IGST, CESS As New Decimal(0)
                MAT_QTY = FormatNumber(CDec(TextBox182.Text), 2)
                UNIT_PRICE = FormatNumber(0.0, 2)
                TAX_VAL = FormatNumber(MAT_QTY, 2)
                CGST = FormatNumber(CDec(TextBox184.Text), 2)
                SGST = FormatNumber(CDec(TextBox185.Text), 2)
                IGST = FormatNumber(CDec(TextBox186.Text), 2)
                CESS = FormatNumber(CDec(TextBox187.Text), 2)
                CGST_VAL = FormatNumber((TAX_VAL * CGST) / 100, 2)
                SGST_VAL = FormatNumber((TAX_VAL * SGST) / 100, 2)
                IGST_VAL = FormatNumber((TAX_VAL * IGST) / 100, 2)
                CESS_VAL = FormatNumber((TAX_VAL * CESS) / 100, 2)
                TOTAL_VAL = FormatNumber(TAX_VAL + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL, 2)
                Dim dt9 As DataTable = DirectCast(ViewState("RCM"), DataTable)
                dt9.Rows.Add("N/A", "N/A", "N/A", TextBox180.Text, TextBox181.Text, DropDownList32.Text.Substring(0, DropDownList32.Text.LastIndexOf(",") - 1), "0.00", "0.00", TAX_VAL, CGST, CGST_VAL, SGST, SGST_VAL, IGST, IGST_VAL, CESS, CESS_VAL, TOTAL_VAL)
                ViewState("RCM") = dt9
                Me.BINDGRID()



            End If

        ElseIf Panel3.Visible = True Then
            If DropDownList2.SelectedValue = "RCM For Tax Invoice" Then
                conn.Open()
                dt.Clear()
                'da = New SqlDataAdapter("select m1.mb_no, m1. wo_slno, m1.w_name, m1.w_au,999794 as sac_code, g1.UNIT_PRICE, m1.work_qty, m1.prov_amt, g1.CGST, m1.cgst_liab, g1.SGST, m1.sgst_liab, g1.IGST, m1.igst_liab, g1.CESS, m1.cess_liab,(prov_amt + m1.sgst_liab + m1.cgst_liab + m1.igst_liab + m1.cess_liab) as TOTAL_VAL from mb_book m1 join GARN_PRICE g1 on (m1.mb_no = g1. CRR_NO AND m1.po_no=g1.PO_NO) where m1. po_no='" & TextBox124.Text & "' and v_ind='V' and inv_no='" & DropDownList30.SelectedValue & "' and RCM_P is null", conn)
                da = New SqlDataAdapter("select GARN_NO, p1.MAT_SLNO, p1.MAT_CODE, m1.MAT_NAME, m1.MAT_AU, 936448 AS CHPTR_HEAD, p1.UNIT_RATE, p1.MAT_RCD_QTY, p1.MAT_RATE, g1.CGST as CGST_P, p1.RCM_CGST, g1.SGST as SGST_P, p1.RCM_SGST, g1.IGST as IGST_P, p1.RCM_IGST, g1.CESS as CESS_P, p1.RCM_CESS, (p1.MAT_RATE + RCM_CGST + RCM_SGST + RCM_IGST + RCM_CESS) AS TOTAL_VAL from (PO_RCD_MAT p1 join MATERIAL m1 on p1.MAT_CODE = m1.MAT_CODE) join GARN_PRICE g1 on (p1.CRR_NO = g1.CRR_NO and p1.MAT_SLNO=g1.MAT_SLNO) where (p1.RCM_SGST >0 or p1.RCM_IGST >0 OR p1.RCM_CGST >0 OR p1.RCM_CESS >0) and p1.po_no='" & TextBox124.Text & "'  and p1.inv_no='" & DropDownList30.SelectedValue & "' and v_ind='V' and RCM_P is null  order by GARN_NO", conn)
                da.Fill(dt)
                conn.Close()
                GridView2.DataSource = dt
                GridView2.DataBind()

            ElseIf DropDownList2.SelectedValue = "RCM For Payment Voucher" Then

                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select m1.mb_no, m1. wo_slno, m1.w_name, m1.w_au,999794 as sac_code, g1.UNIT_PRICE, m1.work_qty, (m1.pen_amt + m1.ld) as prov_amt, g1.CGST, m1.rcm_cgst as cgst_liab, g1.SGST, m1.rcm_sgst as sgst_liab, g1.IGST, m1.rcm_igst as igst_liab, g1.CESS, m1.rcm_cess as cess_liab, (m1.pen_amt + m1.ld + m1.rcm_cgst + m1.rcm_sgst + m1.rcm_igst + m1.rcm_cess) as TOTAL_VAL from mb_book m1 join GARN_PRICE g1 on (m1.mb_no = g1. CRR_NO AND m1.po_no=g1.PO_NO) where m1. po_no='" & TextBox124.Text & "' and v_ind='V' and inv_no='" & DropDownList30.SelectedValue & "' AND (pen_amt > 0 or ld > 0) and RCM_F is null", conn)
                da.Fill(dt)
                conn.Close()
                GridView2.DataSource = dt
                GridView2.DataBind()

            End If

        End If


        Button36.Enabled = True
    End Sub

    Protected Sub DropDownList31_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList31.SelectedIndexChanged
        If DropDownList31.SelectedValue = "Select" Then
            DropDownList31.Focus()
            Return
        ElseIf DropDownList31.SelectedValue = "Unregistered Party" Then
            Label402.Visible = False
            DropDownList26.Visible = False
            Return
        ElseIf DropDownList31.SelectedValue = "Registered Party" Then
            Label402.Visible = True
            DropDownList26.Visible = True
            DropDownList26.Focus()
            Return
        End If
    End Sub

    Protected Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Dim DESPATCH_TYPE As Integer = 0
        If DropDownList33.SelectedValue = "I.P.T." Then
            DESPATCH_TYPE = 4
        ElseIf DropDownList33.SelectedValue = "Other" Then
            DESPATCH_TYPE = 1
        End If

        'invoice type for GST
        Dim inv_type, inv_rule, inv_for, inv_type1, inv_type_new, inv_rule_new, inv_for_new, inv_type1_new As String

        inv_type = "Tax Invoice"
        inv_type1 = "(For Services under Reverse Charge)"
        inv_rule = "(In case of Supplies from unregistered Suppliers under Section 31(3)(f)of CGST Act 2017 read with Rule 1 of Tax Invoice ,Credit and Debit Note Rules)"
        inv_for = "RC15"

        'SAVE RCM_INV
        Dim supl_state, supl_state_code, gst_no As New String("")
        Dim SUPL_INV As String = ""
        If Panel1.Visible = True Then
            SUPL_INV = TextBox178.Text
            supl_state = TextBox189.Text
            supl_state_code = TextBox188.Text
        ElseIf Panel3.Visible = True Then
            SUPL_INV = DropDownList30.SelectedValue
            supl_state = TextBox189.Text
            supl_state_code = TextBox188.Text
        End If

        Dim gst_code, my_gst_code, COMM, DIVISION As New String("")
        conn.Open()
        mycommand.CommandText = "select * from comp_profile"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            my_gst_code = dr.Item("c_gst_no")
            COMM = dr.Item("c_comm")
            DIVISION = dr.Item("c_division")
            dr.Close()
        End If
        conn.Close()

        'Inv No generation for GST (Forward Charge)
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

        generateRCM("rc", DESPATCH_TYPE, inv_type, inv_type1, inv_rule, inv_for, SUPL_INV, STR1, COMM, DIVISION)
        generateRCM("pv", DESPATCH_TYPE, "Payment Voucher", inv_type1, inv_rule, "PV15", SUPL_INV, STR1, COMM, DIVISION)



        If DropDownList2.SelectedValue = "RCM For Payment Voucher" Then

            'Inv No generation for LD & Penalty(Forward Charge)
            'invoice type for GST
            inv_type_new = "Tax Invoice"
            inv_type1_new = "(For Services under Reverse Charge)"
            inv_rule_new = "(In case of Supplies from unregistered Suppliers under Section 31(3)(f)of CGST Act 2017 read with Rule 1 of Tax Invoice ,Credit and Debit Note Rules)"
            inv_for_new = "OS15"

            generateFCM(DESPATCH_TYPE, inv_type_new, inv_type1_new, inv_rule_new, inv_for_new, SUPL_INV, STR1, COMM, DIVISION, supl_state, supl_state_code)



        End If




        Dim QUARY1 As String = ""
        If DropDownList31.SelectedValue = "Unregistered Party" Then

        ElseIf DropDownList31.SelectedValue = "Registered Party" Then

            Dim N As Integer = 0
            For N = 0 To GridView2.Rows.Count - 1


                If DropDownList2.SelectedValue = "RCM For Payment Voucher" Then

                    'UPDATE PO_RCD_MAT RCM_F
                    conn.Open()
                    QUARY1 = ""
                    QUARY1 = "UPDATE mb_book SET RCM_F ='P' WHERE mb_no ='" & GridView2.Rows(N).Cells(0).Text & "' AND INV_NO ='" & DropDownList30.SelectedValue & "' AND WO_SLNO ='" & GridView2.Rows(N).Cells(1).Text & "' AND PO_NO ='" & TextBox124.Text & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()
                    conn.Close()

                ElseIf DropDownList2.SelectedValue = "RCM For Tax Invoice" Then

                    'UPDATE PO_RCD_MAT RCM_F
                    conn.Open()
                    QUARY1 = ""
                    QUARY1 = "UPDATE mb_book SET RCM_P ='P' WHERE mb_no ='" & GridView2.Rows(N).Cells(0).Text & "' AND INV_NO ='" & DropDownList30.SelectedValue & "' AND WO_SLNO ='" & GridView2.Rows(N).Cells(1).Text & "' AND PO_NO ='" & TextBox124.Text & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()
                    conn.Close()
                End If
            Next

        End If

        Button36.Enabled = False
        GridView2.Visible = False

    End Sub

    Protected Sub generateRCM(type As String, DESPATCH_TYPE As String, inv_type As String, inv_type1 As String, inv_rule As String, inv_for As String, SUPL_INV As String, STR1 As String, COMM As String, DIVISION As String)



        conn.Open()
        Dim inv_no As String = ""
        Dim final_inv_no As String = ""
        Dim final_inv_type As String = ""

        Dim mc_c As New SqlCommand
        mc_c.CommandText = "SELECT (CASE WHEN MAX(INV_NO) IS NULL THEN 0 ELSE MAX(INV_NO) END) as inv_no  FROM RCM_INV  WHERE D_TYPE LIKE'" & inv_for & "%' AND FISCAL_YEAR =" & STR1
        mc_c.Connection = conn
        dr = mc_c.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_no = dr.Item("inv_no")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If CInt(inv_no) = 0 Then

            If type = "rc" Then
                TextBox65.Text = "0000001"
                TextBox177.Text = inv_for & CStr(DESPATCH_TYPE)
                TextBox177.ReadOnly = True
                TextBox65.ReadOnly = True
                final_inv_no = "0000001"
                final_inv_type = inv_for & CStr(DESPATCH_TYPE)
            ElseIf type = "pv" Then
                TextBox2.Text = "0000001"
                TextBox1.Text = inv_for & CStr(DESPATCH_TYPE)
                TextBox1.ReadOnly = True
                TextBox2.ReadOnly = True
                final_inv_no = "0000001"
                final_inv_type = inv_for & CStr(DESPATCH_TYPE)
            End If

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

            If type = "rc" Then
                TextBox65.Text = str
                TextBox177.Text = inv_for & CStr(DESPATCH_TYPE)
                TextBox177.ReadOnly = True
                TextBox65.ReadOnly = True
                final_inv_no = str
                final_inv_type = inv_for & CStr(DESPATCH_TYPE)
            ElseIf type = "pv" Then
                TextBox2.Text = str
                TextBox1.Text = inv_for & CStr(DESPATCH_TYPE)
                TextBox1.ReadOnly = True
                TextBox2.ReadOnly = True
                final_inv_no = str
                final_inv_type = inv_for & CStr(DESPATCH_TYPE)
            End If

        End If







        Dim i As Integer = 0
        For i = 0 To GridView2.Rows.Count - 1
            conn.Open()
            Dim Query As String = "Insert Into RCM_INV(RCM_TYPE,EMP_ID,INV_NO,INV_DATE,PO_NO,PO_DATE,SUPL_INV,RCM_BASIS,SUPL_STATE,SUPL_STATE_CODE,SUPL_DETAILS,HSN_CODE,ITEM_SLNO,ITEM_CODE,ITEM_DESC,ITEM_AU,ITEM_QTY,UNIT_RATE,TAXABLE_VALUE,CGST,CGST_AMT,SGST,SGST_AMT,IGST,IGST_AMT,CESS,CESS_AMT,TOTAL_VALUE,ADV_PAID,TAX_RCM,NET_PAY,COMMISSION,DIVISION,NOTIFICATION_DESC,INV_FOR,INV_TYPE,INV_RULE,PARTY_TYPE,GARN_NO,FISCAL_YEAR,D_TYPE)VALUES(@RCM_TYPE,@EMP_ID,@INV_NO,@INV_DATE,@PO_NO,@PO_DATE,@SUPL_INV,@RCM_BASIS,@SUPL_STATE,@SUPL_STATE_CODE,@SUPL_DETAILS,@HSN_CODE,@ITEM_SLNO,@ITEM_CODE,@ITEM_DESC,@ITEM_AU,@ITEM_QTY,@UNIT_RATE,@TAXABLE_VALUE,@CGST,@CGST_AMT,@SGST,@SGST_AMT,@IGST,@IGST_AMT,@CESS,@CESS_AMT,@TOTAL_VALUE,@ADV_PAID,@TAX_RCM,@NET_PAY,@COMMISSION,@DIVISION,@NOTIFICATION_DESC,@INV_FOR,@INV_TYPE,@INV_RULE,@PARTY_TYPE,@GARN_NO,@FISCAL_YEAR,@D_TYPE)"
            Dim cmd As New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@INV_NO", final_inv_no)
            cmd.Parameters.AddWithValue("@D_TYPE", final_inv_type)
            cmd.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(CDate(working_date), "dd-MM-yyyy", provider))
            cmd.Parameters.AddWithValue("@PO_NO", TextBox124.Text)
            cmd.Parameters.AddWithValue("@PO_DATE", "")
            cmd.Parameters.AddWithValue("@SUPL_INV", SUPL_INV)
            cmd.Parameters.AddWithValue("@RCM_BASIS", "Yes")
            cmd.Parameters.AddWithValue("@SUPL_STATE", TextBox189.Text)
            cmd.Parameters.AddWithValue("@SUPL_STATE_CODE", TextBox188.Text)
            cmd.Parameters.AddWithValue("@SUPL_DETAILS", TextBox126.Text)
            cmd.Parameters.AddWithValue("@HSN_CODE", GridView2.Rows(i).Cells(4).Text)
            cmd.Parameters.AddWithValue("@ITEM_SLNO", GridView2.Rows(i).Cells(1).Text)
            cmd.Parameters.AddWithValue("@ITEM_CODE", "NA")
            cmd.Parameters.AddWithValue("@ITEM_DESC", GridView2.Rows(i).Cells(2).Text)
            cmd.Parameters.AddWithValue("@ITEM_AU", GridView2.Rows(i).Cells(3).Text)
            cmd.Parameters.AddWithValue("@ITEM_QTY", CDec(GridView2.Rows(i).Cells(6).Text))
            cmd.Parameters.AddWithValue("@UNIT_RATE", CDec(GridView2.Rows(i).Cells(5).Text))
            cmd.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(GridView2.Rows(i).Cells(7).Text))
            cmd.Parameters.AddWithValue("@CGST", CDec(GridView2.Rows(i).Cells(8).Text))
            cmd.Parameters.AddWithValue("@CGST_AMT", CDec(GridView2.Rows(i).Cells(9).Text))
            cmd.Parameters.AddWithValue("@SGST", CDec(GridView2.Rows(i).Cells(10).Text))
            cmd.Parameters.AddWithValue("@SGST_AMT", CDec(GridView2.Rows(i).Cells(11).Text))
            cmd.Parameters.AddWithValue("@IGST", CDec(GridView2.Rows(i).Cells(12).Text))
            cmd.Parameters.AddWithValue("@IGST_AMT", CDec(GridView2.Rows(i).Cells(13).Text))
            cmd.Parameters.AddWithValue("@CESS", CDec(GridView2.Rows(i).Cells(14).Text))
            cmd.Parameters.AddWithValue("@CESS_AMT", CDec(GridView2.Rows(i).Cells(15).Text))
            cmd.Parameters.AddWithValue("@TOTAL_VALUE", CDec(GridView2.Rows(i).Cells(16).Text))
            cmd.Parameters.AddWithValue("@ADV_PAID", 0)
            cmd.Parameters.AddWithValue("@TAX_RCM", 0)
            cmd.Parameters.AddWithValue("@NET_PAY", CDec(CDec(GridView2.Rows(i).Cells(9).Text) + CDec(GridView2.Rows(i).Cells(11).Text) + CDec(GridView2.Rows(i).Cells(13).Text) + CDec(GridView2.Rows(i).Cells(15).Text)))
            cmd.Parameters.AddWithValue("@COMMISSION", COMM)
            cmd.Parameters.AddWithValue("@DIVISION", DIVISION)
            cmd.Parameters.AddWithValue("@NOTIFICATION_DESC", TextBox64.Text)
            cmd.Parameters.AddWithValue("@INV_FOR", inv_type1)
            cmd.Parameters.AddWithValue("@INV_TYPE", inv_type)
            cmd.Parameters.AddWithValue("@INV_RULE", inv_rule)
            cmd.Parameters.AddWithValue("@PARTY_TYPE", DropDownList31.Text)
            cmd.Parameters.AddWithValue("@GARN_NO", GridView2.Rows(i).Cells(0).Text)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
            cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
            cmd.Parameters.AddWithValue("@RCM_TYPE", DropDownList2.SelectedValue)

            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
            taxable_rate_unit = taxable_rate_unit + CDec(GridView2.Rows(i).Cells(7).Text)
            cgst_amt = cgst_amt + CDec(GridView2.Rows(i).Cells(9).Text)
            sgst_amt = sgst_amt + CDec(GridView2.Rows(i).Cells(11).Text)
            igst_amt = igst_amt + CDec(GridView2.Rows(i).Cells(13).Text)
            cess_amt = cess_amt + CDec(GridView2.Rows(i).Cells(15).Text)
            net_pay = net_pay + cgst_amt + sgst_amt + igst_amt + cess_amt

        Next



    End Sub

    Protected Sub generateFCM(DESPATCH_TYPE As String, inv_type_new As String, inv_type1_new As String, inv_rule_new As String, inv_for_new As String, SUPL_INV As String, STR1 As String, COMM As String, DIVISION As String, supl_state As String, supl_state_code As String)

        conn.Open()
        Dim inv_no_ld_pen As String = ""
        Dim mc_c_ld_pen As New SqlCommand
        mc_c_ld_pen.CommandText = "SELECT (CASE WHEN MAX(inv_no) IS NULL THEN 0 ELSE MAX(inv_no) END) as inv_no  FROM DESPATCH  WHERE D_TYPE LIKE'" & inv_for_new & "%' AND FISCAL_YEAR =" & STR1
        mc_c_ld_pen.Connection = conn
        dr = mc_c_ld_pen.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_no_ld_pen = dr.Item("inv_no")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim prefixFY = Left(STR1, 2)

        If CInt(inv_no_ld_pen) = 0 Then
            TextBox191.Text = prefixFY & "00001"
            TextBox190.Text = inv_for_new & CStr(DESPATCH_TYPE)
            TextBox190.ReadOnly = True
            TextBox191.ReadOnly = True
        Else
            str = CInt(inv_no_ld_pen) + 1
            If str.Length = 1 Then
                str = prefixFY & "0000" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 2 Then
                str = prefixFY & "000" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 3 Then
                str = prefixFY & "00" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 4 Then
                str = prefixFY & "0" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 5 Then
                str = prefixFY & CInt(inv_no_ld_pen) + 1
            End If
            TextBox191.Text = str
            TextBox190.Text = inv_for_new & CStr(DESPATCH_TYPE)
            TextBox190.ReadOnly = True
            TextBox191.ReadOnly = True
        End If

        Dim so_date, SO_ACTUAL_DATE, amd_date As Date
        Dim actual_so, amd_no, party_code, CONSIGN_CODE, PLACE_OF_SUPPLY As New String("")


        If DropDownList31.SelectedValue = "Unregistered Party" Then
            so_date = Today.Date
            SO_ACTUAL_DATE = Today.Date
        ElseIf DropDownList31.SelectedValue = "Registered Party" Then
            'search order_details
            conn.Open()
            mycommand.CommandText = "select SO_ACTUAL ,SO_DATE ,SO_ACTUAL_DATE,DESTINATION from ORDER_DETAILS where SO_NO ='" & TextBox124.Text & "'"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                actual_so = dr.Item("SO_ACTUAL")
                so_date = dr.Item("SO_DATE")
                SO_ACTUAL_DATE = dr.Item("SO_ACTUAL_DATE")
                PLACE_OF_SUPPLY = dr.Item("DESTINATION")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            amd_no = "N/A"
            amd_date = Today.Date
            party_code = TextBox125.Text
            CONSIGN_CODE = TextBox125.Text
        End If


        'save despatch

        conn.Open()
        Dim QUARY As String = ""
        QUARY = "Insert Into DESPATCH(TCS_AMT,INV_STATUS,SO_NO ,SO_DATE ,PO_NO ,PO_DATE ,AMD_NO ,AMD_DATE ,TRANS_WO ,TRANS_SLNO ,TRANS_NAME ,TRUCK_NO ,PARTY_CODE ,CONSIGN_CODE ,MAT_VOCAB ,MAT_SLNO ,INV_NO,INV_DATE,D_TYPE,CHPTR_HEAD ,FISCAL_YEAR,INV_ISSUE ,PLACE_OF_SUPPLY ,BILL_PARTY_ADD ,CON_PARTY_ADD ,B_STATE ,B_STATE_CODE ,C_STATE ,C_STATE_CODE ,BILL_PARTY_GST_N ,CON_PARTY_GST_N ,NEGOTIATING_BRANCH ,PAYING_AUTH ,RR_NO ,RR_DATE ,TOTAL_WEIGHT ,ACC_UNIT ,PURITY ,TC_NO ,FINANCE_ARRENGE ,MILL_CODE ,DA_NO ,CONTRACT_NO ,RCD_VOUCHER_NO ,RCD_VOUCHER_DATE ,ROUT_CARD_NO ,DESPATCH_TYPE ,TAX_REVERS_CHARGE ,RLY_INV_NO ,RLY_INV_DATE ,FRT_WT_AMT ,TOTAL_PCS,P_CODE ,P_DESC ,D1 ,D2 ,D3 ,D4 ,BASE_PRICE ,PACK_PRICE ,PACK_TYPE ,QLTY_PRICE ,SEC_PRICE ,TOTAL_TDC ,UNIT_PRICE ,SY_MARGIN ,PPM_FRT ,FRT_TYPE ,RLY_ROAD_FRT ,TOTAL_RATE_UNIT ,REBATE_UNIT ,REBATE_TYPE ,TAXABLE_RATE_UNIT ,TOTAL_QTY ,TAXABLE_VALUE ,CGST_RATE ,CGST_AMT ,SGST_RATE ,SGST_AMT ,IGST_RATE ,IGST_AMT ,CESS_RATE ,CESS_AMT ,TERM_RATE ,TERM_AMT ,TOTAL_AMT ,LESS_LOAD_AMT ,TOTAL_BAG ,ADVANCE_PAID ,GST_PAID_ADV ,NET_PAY ,NOTIFICATION_TEXT ,COMM ,DIV_ADD ,INV_TYPE ,INV_RULE ,FORM_NAME,EMP_ID)values(@TCS_AMT,@INV_STATUS,@SO_NO ,@SO_DATE ,@PO_NO ,@PO_DATE ,@AMD_NO ,@AMD_DATE ,@TRANS_WO ,@TRANS_SLNO ,@TRANS_NAME ,@TRUCK_NO ,@PARTY_CODE ,@CONSIGN_CODE ,@MAT_VOCAB ,@MAT_SLNO ,@INV_NO,@INV_DATE,@D_TYPE,@CHPTR_HEAD ,@FISCAL_YEAR,@INV_ISSUE ,@PLACE_OF_SUPPLY ,@BILL_PARTY_ADD ,@CON_PARTY_ADD ,@B_STATE ,@B_STATE_CODE ,@C_STATE ,@C_STATE_CODE ,@BILL_PARTY_GST_N ,@CON_PARTY_GST_N ,@NEGOTIATING_BRANCH ,@PAYING_AUTH ,@RR_NO ,@RR_DATE ,@TOTAL_WEIGHT ,@ACC_UNIT ,@PURITY ,@TC_NO ,@FINANCE_ARRENGE ,@MILL_CODE ,@DA_NO ,@CONTRACT_NO ,@RCD_VOUCHER_NO ,@RCD_VOUCHER_DATE ,@ROUT_CARD_NO ,@DESPATCH_TYPE ,@TAX_REVERS_CHARGE ,@RLY_INV_NO ,@RLY_INV_DATE ,@FRT_WT_AMT ,@TOTAL_PCS,@P_CODE ,@P_DESC ,@D1 ,@D2 ,@D3 ,@D4 ,@BASE_PRICE ,@PACK_PRICE ,@PACK_TYPE ,@QLTY_PRICE ,@SEC_PRICE ,@TOTAL_TDC ,@UNIT_PRICE ,@SY_MARGIN ,@PPM_FRT ,@FRT_TYPE ,@RLY_ROAD_FRT ,@TOTAL_RATE_UNIT ,@REBATE_UNIT ,@REBATE_TYPE ,@TAXABLE_RATE_UNIT ,@TOTAL_QTY ,@TAXABLE_VALUE ,@CGST_RATE ,@CGST_AMT ,@SGST_RATE ,@SGST_AMT ,@IGST_RATE ,@IGST_AMT ,@CESS_RATE ,@CESS_AMT ,@TERM_RATE ,@TERM_AMT ,@TOTAL_AMT ,@LESS_LOAD_AMT ,@TOTAL_BAG ,@ADVANCE_PAID ,@GST_PAID_ADV ,@NET_PAY ,@NOTIFICATION_TEXT ,@COMM ,@DIV_ADD ,@INV_TYPE ,@INV_RULE ,@FORM_NAME,@EMP_ID)"
        Dim cmd1 As New SqlCommand(QUARY, conn)
        cmd1.Parameters.AddWithValue("@SO_NO", TextBox124.Text)
        cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@PO_NO", actual_so)
        cmd1.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(SO_ACTUAL_DATE.Day & "-" & SO_ACTUAL_DATE.Month & "-" & SO_ACTUAL_DATE.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@AMD_NO", amd_no)
        cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(amd_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@TRANS_WO", "N/A")
        cmd1.Parameters.AddWithValue("@TRANS_SLNO", "N/A")
        cmd1.Parameters.AddWithValue("@TRANS_NAME", "N/A")
        cmd1.Parameters.AddWithValue("@TRUCK_NO", "N/A")
        cmd1.Parameters.AddWithValue("@ED_COMDT", "Agreeing To Tolerate An Act")
        cmd1.Parameters.AddWithValue("@PARTY_CODE", party_code)
        cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CONSIGN_CODE)
        cmd1.Parameters.AddWithValue("@MAT_VOCAB", "N/A")
        cmd1.Parameters.AddWithValue("@MAT_SLNO", 0)
        cmd1.Parameters.AddWithValue("@INV_NO", TextBox191.Text)
        cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@CHPTR_HEAD", "999794")
        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
        cmd1.Parameters.AddWithValue("@INV_ISSUE", Now)
        cmd1.Parameters.AddWithValue("@PLACE_OF_SUPPLY", PLACE_OF_SUPPLY)
        cmd1.Parameters.AddWithValue("@BILL_PARTY_ADD", TextBox126.Text)
        cmd1.Parameters.AddWithValue("@CON_PARTY_ADD", TextBox126.Text)
        cmd1.Parameters.AddWithValue("@B_STATE", supl_state)
        cmd1.Parameters.AddWithValue("@B_STATE_CODE", supl_state_code)
        cmd1.Parameters.AddWithValue("@C_STATE", supl_state)
        cmd1.Parameters.AddWithValue("@C_STATE_CODE", supl_state_code)
        cmd1.Parameters.AddWithValue("@BILL_PARTY_GST_N", "N/A")
        cmd1.Parameters.AddWithValue("@CON_PARTY_GST_N", "N/A")
        cmd1.Parameters.AddWithValue("@NEGOTIATING_BRANCH", "")
        cmd1.Parameters.AddWithValue("@PAYING_AUTH", "")
        cmd1.Parameters.AddWithValue("@RR_NO", "")
        cmd1.Parameters.AddWithValue("@RR_DATE", "")
        cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT ", 0)
        cmd1.Parameters.AddWithValue("@D_TYPE", TextBox190.Text)
        cmd1.Parameters.AddWithValue("@ACC_UNIT", "Service")
        cmd1.Parameters.AddWithValue("@PURITY", CDec(0))
        cmd1.Parameters.AddWithValue("@TC_NO", "")
        cmd1.Parameters.AddWithValue("@FINANCE_ARRENGE", "N/A")
        cmd1.Parameters.AddWithValue("@MILL_CODE", "")
        cmd1.Parameters.AddWithValue("@DA_NO", "")
        cmd1.Parameters.AddWithValue("@CONTRACT_NO", "")
        cmd1.Parameters.AddWithValue("@RCD_VOUCHER_NO", "N/A")
        cmd1.Parameters.AddWithValue("@RCD_VOUCHER_DATE", "N/A")
        cmd1.Parameters.AddWithValue("@ROUT_CARD_NO", "N/A")
        cmd1.Parameters.AddWithValue("@DESPATCH_TYPE", "")
        cmd1.Parameters.AddWithValue("@TAX_REVERS_CHARGE", "YES")
        cmd1.Parameters.AddWithValue("@RLY_INV_NO", "N/A")
        cmd1.Parameters.AddWithValue("@RLY_INV_DATE", "N/A")
        cmd1.Parameters.AddWithValue("@FRT_WT_AMT", CDec(0.0))
        cmd1.Parameters.AddWithValue("@TOTAL_PCS", 0)
        cmd1.Parameters.AddWithValue("@P_CODE ", "")
        cmd1.Parameters.AddWithValue("@P_DESC", "L.D. / PENALITY")
        cmd1.Parameters.AddWithValue("@D1", "")
        cmd1.Parameters.AddWithValue("@D2", "")
        cmd1.Parameters.AddWithValue("@D3", "")
        cmd1.Parameters.AddWithValue("@D4", "")
        cmd1.Parameters.AddWithValue("@PACK_TYPE", "/ Mt")
        cmd1.Parameters.AddWithValue("@FRT_TYPE", "/ Mt")
        cmd1.Parameters.AddWithValue("@REBATE_TYPE ", "/ Mt")
        cmd1.Parameters.AddWithValue("@TOTAL_QTY", CDec(1))
        cmd1.Parameters.AddWithValue("@BASE_PRICE", 0)
        cmd1.Parameters.AddWithValue("@PACK_PRICE", 0)
        cmd1.Parameters.AddWithValue("@QLTY_PRICE", 0.0)
        cmd1.Parameters.AddWithValue("@SEC_PRICE", 0.0)
        cmd1.Parameters.AddWithValue("@TOTAL_TDC", 0.0)
        cmd1.Parameters.AddWithValue("@UNIT_PRICE", 0)
        cmd1.Parameters.AddWithValue("@SY_MARGIN", 0.0)
        cmd1.Parameters.AddWithValue("@PPM_FRT", 0.0)
        cmd1.Parameters.AddWithValue("@RLY_ROAD_FRT", 0.0)
        cmd1.Parameters.AddWithValue("@TOTAL_RATE_UNIT", 0.0)
        cmd1.Parameters.AddWithValue("@REBATE_UNIT", 0.0)
        cmd1.Parameters.AddWithValue("@TAXABLE_RATE_UNIT", 0)
        cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", taxable_rate_unit)
        cmd1.Parameters.AddWithValue("@CGST_RATE", 0.0)
        cmd1.Parameters.AddWithValue("@CGST_AMT", cgst_amt)
        cmd1.Parameters.AddWithValue("@SGST_RATE", 0)
        cmd1.Parameters.AddWithValue("@SGST_AMT", sgst_amt)
        cmd1.Parameters.AddWithValue("@IGST_RATE", 0)
        cmd1.Parameters.AddWithValue("@IGST_AMT", igst_amt)
        cmd1.Parameters.AddWithValue("@CESS_RATE", 0)
        cmd1.Parameters.AddWithValue("@CESS_AMT", cess_amt)
        cmd1.Parameters.AddWithValue("@TERM_RATE", 0)
        cmd1.Parameters.AddWithValue("@TERM_AMT", 0)
        cmd1.Parameters.AddWithValue("@TOTAL_AMT", taxable_rate_unit + cgst_amt + sgst_amt + igst_amt + cess_amt)
        cmd1.Parameters.AddWithValue("@LESS_LOAD_AMT", 0.0)
        cmd1.Parameters.AddWithValue("@ADVANCE_PAID", 0)
        cmd1.Parameters.AddWithValue("@GST_PAID_ADV", 0)
        cmd1.Parameters.AddWithValue("@NET_PAY", taxable_rate_unit)
        cmd1.Parameters.AddWithValue("@COMM", COMM)
        cmd1.Parameters.AddWithValue("@DIV_ADD", DIVISION)
        cmd1.Parameters.AddWithValue("@INV_TYPE", inv_type_new & " " & inv_type1_new)
        cmd1.Parameters.AddWithValue("@INV_RULE", inv_rule_new)
        cmd1.Parameters.AddWithValue("@FORM_NAME", "")
        cmd1.Parameters.AddWithValue("@TOTAL_BAG", 0)
        cmd1.Parameters.AddWithValue("@NOTIFICATION_TEXT", TextBox64.Text)
        cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
        cmd1.Parameters.AddWithValue("@INV_STATUS", "ACTIVE")
        cmd1.Parameters.AddWithValue("@TCS_AMT", 0)
        cmd1.ExecuteReader()
        cmd1.Dispose()
        conn.Close()

        'save print invoice
        ''insert inv_print
        conn.Open()
        QUARY = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
        Dim scmd As New SqlCommand(QUARY, conn)
        scmd.Parameters.AddWithValue("@INV_NO", inv_for_new & CStr(DESPATCH_TYPE) & TextBox65.Text)
        scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
        scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
        scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
        scmd.Parameters.AddWithValue("@F_YEAR", STR1)
        scmd.ExecuteReader()
        scmd.Dispose()
        conn.Close()

        ''Update RCM_INV table
        conn.Open()
        QUARY = "update RCM_INV set OS_INV_NO='" & TextBox190.Text + TextBox191.Text & "' where (D_TYPE+INV_NO)='" & TextBox177.Text + TextBox65.Text & "'"
        Dim sc1 As New SqlCommand(QUARY, conn)
        sc1.ExecuteReader()
        sc1.Dispose()
        conn.Close()

        conn.Open()
        QUARY = "update RCM_INV set OS_INV_NO='" & TextBox190.Text + TextBox191.Text & "' where (D_TYPE+INV_NO)='" & TextBox1.Text + TextBox2.Text & "'"
        Dim sc2 As New SqlCommand(QUARY, conn)
        sc2.ExecuteReader()
        sc2.Dispose()
        conn.Close()

        'update RCM_INV set RCM_TYPE='RCM For TAX Invoice', OS_INV_NO='OS0002' where (D_TYPE+INV_NO)='PV1510000001'


    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Panel1.Visible = False
            Return
        End If
        If DropDownList31.SelectedValue = "Registered Party" Then
            If DropDownList2.SelectedValue = "RCM For Tax Invoice" Then

                Panel1.Visible = False
                Panel3.Visible = True
                '''''''''''==========================================='''''''''''''''''''''''
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("SELECT DISTINCT INV_NO FROM PO_RCD_MAT WHERE  po_no='" & TextBox124.Text & "' AND v_ind IS NOT NULL and  RCM_P is null AND (PO_RCD_MAT.RCM_SGST >0 or PO_RCD_MAT.RCM_IGST >0 OR PO_RCD_MAT.RCM_CGST >0 OR PO_RCD_MAT.RCM_CESS >0)", conn)
                da.Fill(dt)
                conn.Close()

                DropDownList30.Items.Clear()
                DropDownList30.DataSource = dt
                DropDownList30.DataValueField = "INV_NO"
                DropDownList30.DataBind()
                DropDownList30.Items.Add("Select")
                DropDownList30.SelectedValue = "Select"


                '''''''''''==========================================='''''''''''''''''''''''

            ElseIf DropDownList2.SelectedValue = "RCM For Payment Voucher" Then

                Panel1.Visible = False
                Panel3.Visible = True
                '''''''''''==========================================='''''''''''''''''''''''
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("SELECT DISTINCT INV_NO FROM mb_book WHERE  po_no='" & TextBox124.Text & "' AND (pen_amt > 0 or ld > 0) and v_ind='v' and  RCM_F is null", conn)
                da.Fill(dt)
                conn.Close()

                DropDownList30.Items.Clear()
                DropDownList30.DataSource = dt
                DropDownList30.DataValueField = "INV_NO"
                DropDownList30.DataBind()
                DropDownList30.Items.Add("Select")
                DropDownList30.SelectedValue = "Select"


                '''''''''''==========================================='''''''''''''''''''''''
            End If

        ElseIf DropDownList31.SelectedValue = "Unregistered Party" Then
            Label477.Visible = True
            TextBox183.Visible = True
            Label476.Text = "Mat. Qty."

        End If

    End Sub

    Protected Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim dt2 As New DataTable()
        dt2.Columns.AddRange(New DataColumn(17) {New DataColumn("GARN_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("CHPTR_HEAD"), New DataColumn("UNIT_RATE"), New DataColumn("MAT_QTY"), New DataColumn("TAX_VAL"), New DataColumn("CGST_P"), New DataColumn("CGST"), New DataColumn("SGST_P"), New DataColumn("SGST"), New DataColumn("IGST_P"), New DataColumn("IGST"), New DataColumn("CESS_P"), New DataColumn("CESS"), New DataColumn("TOTAL_VAL")})
        ViewState("RCM") = dt2
        Me.BINDGRID()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel2.Visible = False
        Panel8.Visible = True
        Dim dt2 As New DataTable()
        dt2.Columns.AddRange(New DataColumn(17) {New DataColumn("GARN_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("CHPTR_HEAD"), New DataColumn("UNIT_RATE"), New DataColumn("MAT_QTY"), New DataColumn("TAX_VAL"), New DataColumn("CGST_P"), New DataColumn("CGST"), New DataColumn("SGST_P"), New DataColumn("SGST"), New DataColumn("IGST_P"), New DataColumn("IGST"), New DataColumn("CESS_P"), New DataColumn("CESS"), New DataColumn("TOTAL_VAL")})
        ViewState("RCM") = dt2
        Me.BINDGRID()
        Panel8.Visible = True
        Panel2.Visible = False
    End Sub


    ''''''''''''''''''======================='''''''''''''''''''''''''''
End Class