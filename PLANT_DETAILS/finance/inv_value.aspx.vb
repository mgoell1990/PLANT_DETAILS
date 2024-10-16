Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class inv_value
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

            Dim ds5 As New DataSet
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT bill_id  from inv_data where v_ind is null order by bill_id", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList40.Items.Clear()
            DropDownList40.DataSource = dt
            DropDownList40.DataValueField = "bill_id"
            DropDownList40.DataBind()
            DropDownList40.Items.Add("Select")
            DropDownList40.SelectedValue = "Select"
            Panel30.Visible = True
        End If
        TextBox32_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList40_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList40.SelectedIndexChanged
        If DropDownList40.SelectedValue = "Select" Then
            DropDownList40.Focus()
            Return
        End If
        conn.Open()
        Dim MC6 As New SqlCommand
        MC6.CommandText = "select * from inv_data where bill_id = '" & DropDownList40.SelectedValue & "'"
        MC6.Connection = conn
        dr = MC6.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox171.Text = dr.Item("po_no")
            TextBox160.Text = dr.Item("inv_no")
            TextBox161.Text = dr.Item("inv_date")
            If IsDBNull(dr.Item("AGING_FLAG")) Then
                TextBox57.Text = "N/A"
            Else
                TextBox57.Text = dr.Item("AGING_FLAG")
            End If

            dr.Close()
            conn.Close()
        Else
            conn.Close()
        End If
        Dim MC As New SqlCommand
        conn.Open()
        MC.CommandText = "select (SUPL.SUPL_ID + ' , ' + SUPL_NAME ) as supl_details FROM SUPL JOIN ORDER_DETAILS ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID where ORDER_DETAILS.SO_NO = '" & TextBox171.Text & "'"
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox168.Text = dr.Item("supl_details")
            dr.Close()
            conn.Close()
        Else
            conn.Close()
        End If
    End Sub

    Protected Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
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

        Dim RestrictFlag As New String("")

        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select RestrictFlag from RestrictDuplicateInvoice"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            RestrictFlag = dr.Item("RestrictFlag")
            dr.Close()
        End If
        conn.Close()


        If (RestrictFlag = "YES") Then
            ''Checking if same invoice number is present in the system or not
            conn.Open()
            Dim sqlCmd As New SqlCommand
            sqlCmd.CommandText = "SELECT * FROM Taxable_Values WHERE INVOICE_NO='" & TextBox160.Text & "' AND SUPL_CODE='" & TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1).Trim & "' and fiscal_year='" & STR1 & "'"
            sqlCmd.Connection = conn
            dr = sqlCmd.ExecuteReader
            If dr.HasRows Then
                lblErrorMsg.Text = "There is already an invoice registered with this supplier in this financial year."
                conn.Close()
            Else
                ProceedForInvoiceValuation()
            End If
        Else
            ProceedForInvoiceValuation()
        End If

    End Sub

    Protected Sub ProceedForInvoiceValuation()
        Dim mc1 As New SqlCommand
        conn.Close()
        Dim order_type, PO_TYPE, SO_STATUS As String
        order_type = ""
        PO_TYPE = ""
        SO_STATUS = ""
        If TextBox160.Text = "" Then
            TextBox160.Focus()
            Return
        ElseIf IsDate(TextBox161.Text) = False Then
            TextBox161.Focus()
            Return
        End If
        conn.Open()
        'Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE,PO_TYPE,SO_STATUS  from ORDER_DETAILS WHERE SO_NO = '" & TextBox171.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            PO_TYPE = dr.Item("PO_TYPE")
            SO_STATUS = dr.Item("SO_STATUS")
            dr.Close()
        End If
        conn.Close()
        If order_type = "Rate Contract" And SO_STATUS = "RCW" Then
            Dim ds5 As New DataSet
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT DISTINCT MB_NO FROM mb_book WHERE v_ind IS NULL and mb_clear = 'I.R. CLEAR' AND po_no ='" & TextBox171.Text & "' ORDER BY MB_NO", conn)
            da.Fill(dt)
            conn.Close()
            garn_crrnoDropDownList.DataSource = dt
            garn_crrnoDropDownList.DataValueField = "mb_no"
            garn_crrnoDropDownList.DataBind()
            garn_crrnoDropDownList.Items.Insert(0, "Select")
            garn_crrnoDropDownList.SelectedValue = "Select"
            ds5.Tables.Clear()
            Dim DT11 As New DataTable
            DT11.Columns.AddRange(New DataColumn(17) {New DataColumn("mb_no"), New DataColumn("mb_date"), New DataColumn("po_no"), New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("w_au"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty"), New DataColumn("prov_amt"), New DataColumn("pen_amt"), New DataColumn("st_amt_r"), New DataColumn("st_amt_p"), New DataColumn("wct_amt"), New DataColumn("it_amt"), New DataColumn("mat_rate")})
            GridView6.DataSource = DT11
            GridView6.DataBind()
            Panel30.Visible = False
            Panel37.Visible = False
            MultiView1.ActiveViewIndex = 0



            'ElseIf order_type = "Work Order" Then 'And (PO_TYPE <> "FREIGHT INWARD" And PO_TYPE <> "FREIGHT OUTWARD") Then
        ElseIf order_type = "Work Order" And (PO_TYPE <> "FREIGHT INWARD" And PO_TYPE <> "FREIGHT OUTWARD") Then 'And (PO_TYPE <> "FREIGHT INWARD" And PO_TYPE <> "FREIGHT OUTWARD") Then

            Dim ds5 As New DataSet
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT DISTINCT MB_NO FROM mb_book WHERE v_ind IS NULL and mb_clear = 'I.R. CLEAR' AND po_no ='" & TextBox171.Text & "' ORDER BY MB_NO", conn)
            da.Fill(dt)
            conn.Close()
            garn_crrnoDropDownList.DataSource = dt
            garn_crrnoDropDownList.DataValueField = "mb_no"
            garn_crrnoDropDownList.DataBind()
            garn_crrnoDropDownList.Items.Insert(0, "Select")
            garn_crrnoDropDownList.SelectedValue = "Select"
            ds5.Tables.Clear()
            Dim DT11 As New DataTable
            DT11.Columns.AddRange(New DataColumn(17) {New DataColumn("mb_no"), New DataColumn("mb_date"), New DataColumn("po_no"), New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("w_au"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty"), New DataColumn("prov_amt"), New DataColumn("pen_amt"), New DataColumn("st_amt_r"), New DataColumn("st_amt_p"), New DataColumn("wct_amt"), New DataColumn("it_amt"), New DataColumn("mat_rate")})
            GridView6.DataSource = DT11
            GridView6.DataBind()
            Panel30.Visible = False

            Panel37.Visible = False
            MultiView1.ActiveViewIndex = 0

        ElseIf order_type = "Purchase Order" Then
            conn.Open()
            da = New SqlDataAdapter("select DISTINCT GARN_NO from po_rcd_mat WHERE V_IND IS NULL AND GARN_NO <> 'PENDING' AND PO_NO='" & TextBox171.Text & "' ORDER BY GARN_NO", conn)
            da.Fill(dt)
            conn.Close()
            M_garn_crrnoDropDownList.Items.Clear()
            M_garn_crrnoDropDownList.DataSource = dt
            M_garn_crrnoDropDownList.DataValueField = "GARN_NO"
            M_garn_crrnoDropDownList.DataBind()
            M_garn_crrnoDropDownList.Items.Insert(0, "Select")
            M_garn_crrnoDropDownList.SelectedValue = "Select"
            Dim DT10 As New DataTable
            DT10.Columns.AddRange(New DataColumn(18) {New DataColumn("CRR_NO"), New DataColumn("PO_NO"), New DataColumn("AMD_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("CHLN_NO"), New DataColumn("MAT_CHALAN_QTY"), New DataColumn("MAT_RCD_QTY"), New DataColumn("MAT_REJ_QTY"), New DataColumn("MAT_EXCE"), New DataColumn("MAT_BAL_QTY"), New DataColumn("TRANS_SHORT"), New DataColumn("PROV_VALUE"), New DataColumn("TRANS_CHARGE"), New DataColumn("GARN_NOTE"), New DataColumn("TOTAL_MT")})
            GridView210.DataSource = DT10
            GridView210.DataBind()
            Panel30.Visible = False

            Panel38.Visible = False
            MultiView1.ActiveViewIndex = 1

        ElseIf order_type = "Rate Contract" And (PO_TYPE = "STORE MATERIAL" Or PO_TYPE = "RAW MATERIAL") Then
            conn.Open()
            da = New SqlDataAdapter("select DISTINCT GARN_NO from po_rcd_mat WHERE V_IND IS NULL AND GARN_NO <> 'PENDING' AND PO_NO='" & TextBox171.Text & "' ORDER BY GARN_NO", conn)
            da.Fill(dt)
            conn.Close()
            M_garn_crrnoDropDownList.Items.Clear()
            M_garn_crrnoDropDownList.DataSource = dt
            M_garn_crrnoDropDownList.DataValueField = "GARN_NO"
            M_garn_crrnoDropDownList.DataBind()
            M_garn_crrnoDropDownList.Items.Insert(0, "Select")
            M_garn_crrnoDropDownList.SelectedValue = "Select"
            Dim DT10 As New DataTable
            DT10.Columns.AddRange(New DataColumn(18) {New DataColumn("CRR_NO"), New DataColumn("PO_NO"), New DataColumn("AMD_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("CHLN_NO"), New DataColumn("MAT_CHALAN_QTY"), New DataColumn("MAT_RCD_QTY"), New DataColumn("MAT_REJ_QTY"), New DataColumn("MAT_EXCE"), New DataColumn("MAT_BAL_QTY"), New DataColumn("TRANS_SHORT"), New DataColumn("PROV_VALUE"), New DataColumn("TRANS_CHARGE"), New DataColumn("GARN_NOTE"), New DataColumn("TOTAL_MT")})
            GridView210.DataSource = DT10
            GridView210.DataBind()
            Panel30.Visible = False

            MultiView1.ActiveViewIndex = 1


            '' TRANSPORTATION

        ElseIf order_type = "Work Order" And (PO_TYPE = "FREIGHT INWARD" Or PO_TYPE = "FREIGHT OUTWARD") Then
            Dim ds5 As New DataSet

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT DISTINCT MB_NO FROM mb_book WHERE v_ind IS NULL and mb_clear = 'I.R. CLEAR' AND po_no ='" & TextBox171.Text & "' ORDER BY MB_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "mb_no"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            DropDownList1.SelectedValue = "Select"
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct W_SLNO from WO_ORDER where PO_NO ='" & TextBox171.Text & "' ORDER BY W_SLNO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList41.DataSource = dt
            DropDownList41.DataValueField = "W_SLNO"
            DropDownList41.DataBind()
            DropDownList41.Items.Insert(0, "Select")
            DropDownList41.SelectedValue = "Select"
            ds5.Tables.Clear()
            Dim DT11 As New DataTable
            DT11.Columns.AddRange(New DataColumn(17) {New DataColumn("mb_no"), New DataColumn("mb_date"), New DataColumn("po_no"), New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("w_au"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty"), New DataColumn("prov_amt"), New DataColumn("pen_amt"), New DataColumn("st_amt_r"), New DataColumn("st_amt_p"), New DataColumn("wct_amt"), New DataColumn("it_amt"), New DataColumn("mat_rate")})
            ''DT11.Columns.AddRange(New DataColumn(16) {New DataColumn("mb_no"), New DataColumn("mb_date"), New DataColumn("po_no"), New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("w_au"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty"), New DataColumn("pen_amt"), New DataColumn("st_amt_r"), New DataColumn("st_amt_p"), New DataColumn("wct_amt"), New DataColumn("it_amt"), New DataColumn("mat_rate")})
            GridView1.DataSource = DT11
            GridView1.DataBind()
            Panel30.Visible = False

            Panel37.Visible = False

            MultiView1.ActiveViewIndex = 2
            Label12.Text = TextBox171.Text
            Label16.Text = TextBox168.Text

        End If
        Panel37.Visible = False
        Panel38.Visible = False

    End Sub

    Protected Sub garn_crrnoDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles garn_crrnoDropDownList.SelectedIndexChanged
        If garn_crrnoDropDownList.SelectedValue = "Select" Then
            garn_crrnoDropDownList.Focus()
            Return
        End If
        Dim ds5 As New DataSet

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select * from mb_book where mb_no='" & garn_crrnoDropDownList.SelectedValue & "' and v_ind is null AND PO_NO='" & TextBox171.Text & "'", conn)
        da.Fill(dt)
        conn.Close()
        GridView6.DataSource = dt
        GridView6.DataBind()
        DropDownList6.Items.Clear()
        DropDownList6.DataSource = dt
        DropDownList6.DataValueField = "wo_slno"
        DropDownList6.DataBind()
        DropDownList6.Items.Insert(0, "Select")
        DropDownList6.SelectedValue = "Select"
        ''order no search
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select mb_book.po_no,SUPL.SUPL_ID ,SUPL.SUPL_NAME from mb_book join SUPL on mb_book .supl_id =supl.SUPL_ID where mb_book .mb_no='" & garn_crrnoDropDownList.SelectedValue & "'and mb_book .v_ind is null AND MB_BOOK.PO_NO='" & TextBox171.Text & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label398.Text = dr.Item("po_no")
            Label396.Text = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        If DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            Return
        End If
        Dim supl_type As String = ""
        conn.Open()
        mycommand.CommandText = "select * from SUPL where SUPL_ID ='" & TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1).Trim & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            supl_type = dr.Item("SUPL_TYPE")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
        'Dim sgst, cgst, igst, cess As New Decimal(0)
        Dim sgst, cgst, igst, cess, u_price_trailor As New Decimal(0)
        Dim w_unit As String = ""
        conn.Open()
        Dim mc As New SqlCommand
        'mc.CommandText = "SELECT (select pen_amt from mb_book where po_no='" & Label398.Text & "'and wo_slno='" & DropDownList6.SelectedValue & "' and mb_no ='" & garn_crrnoDropDownList.SelectedValue & "') as pen_amt,(select note from mb_book where po_no='" & Label398.Text & "'and wo_slno='" & DropDownList6.SelectedValue & "' and mb_no ='" & garn_crrnoDropDownList.SelectedValue & "') as pen_details, MAX(WO_AMD ) as WO_AMD, SUM(W_UNIT_PRICE ) AS W_UNIT_PRICE,SUM(W_DISCOUNT) AS W_DISCOUNT,SUM(W_MATERIAL_COST) AS W_MATERIAL_COST,max(s_tax_liability.cgst) as cgst,max(s_tax_liability.sgst) as sgst,max(s_tax_liability.igst) as igst,max(s_tax_liability.cess) as cess,(0.000) as tds  FROM wo_order join ORDER_DETAILS on WO_ORDER .PO_NO =ORDER_DETAILS .SO_NO join s_tax_liability on ORDER_DETAILS .PO_TYPE  =s_tax_liability.taxable_service WHERE s_tax_liability .w_e_f =(select max(w_e_f) from s_tax_liability JOIN ORDER_DETAILS ON s_tax_liability .taxable_service =ORDER_DETAILS .PO_TYPE  where ORDER_DETAILS .SO_NO ='" & Label398.Text & "' AND w_e_f <= (select to_date  from mb_book where mb_no ='" & garn_crrnoDropDownList.SelectedValue & "' and po_no ='" & Label398.Text & "' and wo_slno ='" & DropDownList6.SelectedValue & "')) AND  WO_ORDER .PO_NO  ='" & Label398.Text & "' AND WO_ORDER .W_SLNO ='" & DropDownList6.SelectedValue & "' AND AMD_DATE<=(SELECT TO_DATE FROM MB_BOOK WHERE PO_NO='" & Label398.Text & "' AND WO_SLNO='" & DropDownList6.SelectedValue & "' AND MB_NO='" & garn_crrnoDropDownList.SelectedValue & "')"
        mc.CommandText = "SELECT (select u_price_trailor from mb_book where po_no='" & Label398.Text & "'and wo_slno='" & DropDownList6.SelectedValue & "' and mb_no ='" & garn_crrnoDropDownList.SelectedValue & "') as u_price_trailor, (select pen_amt from mb_book where po_no='" & Label398.Text & "'and wo_slno='" & DropDownList6.SelectedValue & "' and mb_no ='" & garn_crrnoDropDownList.SelectedValue & "') as pen_amt,(select note from mb_book where po_no='" & Label398.Text & "'and wo_slno='" & DropDownList6.SelectedValue & "' and mb_no ='" & garn_crrnoDropDownList.SelectedValue & "') as pen_details, MAX(WO_AMD ) as WO_AMD, SUM(W_UNIT_PRICE ) AS W_UNIT_PRICE,SUM(W_DISCOUNT) AS W_DISCOUNT,SUM(W_MATERIAL_COST) AS W_MATERIAL_COST,max(s_tax_liability.cgst) as cgst,max(s_tax_liability.sgst) as sgst,max(s_tax_liability.igst) as igst,max(s_tax_liability.cess) as cess,(0.000) as tds, max(wo_order.W_AU) as W_AU,
                 max(wo_order.TDS_SGST) as TDS_SGST,max(wo_order.TDS_CGST) as TDS_CGST,max(wo_order.TDS_IGST) as TDS_IGST FROM wo_order join ORDER_DETAILS on WO_ORDER .PO_NO =ORDER_DETAILS .SO_NO join s_tax_liability on ORDER_DETAILS .PO_TYPE  =s_tax_liability.taxable_service WHERE s_tax_liability .w_e_f =(select max(w_e_f) from s_tax_liability JOIN ORDER_DETAILS ON s_tax_liability .taxable_service =ORDER_DETAILS .PO_TYPE  where ORDER_DETAILS .SO_NO ='" & Label398.Text & "' AND w_e_f <= (select to_date  from mb_book where mb_no ='" & garn_crrnoDropDownList.SelectedValue & "' and po_no ='" & Label398.Text & "' and wo_slno ='" & DropDownList6.SelectedValue & "')) AND  WO_ORDER .PO_NO  ='" & Label398.Text & "' AND WO_ORDER .W_SLNO ='" & DropDownList6.SelectedValue & "' AND AMD_DATE<=(SELECT TO_DATE FROM MB_BOOK WHERE PO_NO='" & Label398.Text & "' AND WO_SLNO='" & DropDownList6.SelectedValue & "' AND MB_NO='" & garn_crrnoDropDownList.SelectedValue & "')"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox120.Text = dr.Item("W_UNIT_PRICE")
            TextBox124.Text = dr.Item("W_DISCOUNT")
            TextBox121.Text = dr.Item("W_MATERIAL_COST")
            sgst = dr.Item("sgst")
            cgst = dr.Item("cgst")
            igst = dr.Item("igst")
            cess = dr.Item("cess")
            TextBox159.Text = dr.Item("tds")
            Label422.Text = dr.Item("WO_AMD")
            TextBox143.Text = dr.Item("pen_amt")
            TextBox144.Text = dr.Item("pen_details")
            w_unit = dr.Item("W_AU")

            TextBox51.Text = dr.Item("TDS_SGST")
            TextBox52.Text = dr.Item("TDS_CGST")
            TextBox53.Text = dr.Item("TDS_IGST")


            If IsDBNull(dr.Item("u_price_trailor")) Then
                u_price_trailor = TextBox120.Text
            Else
                u_price_trailor = dr.Item("u_price_trailor")
            End If


            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If supl_type = "Within State" Then
            igst = FormatNumber(0, 3)
        Else
            cgst = FormatNumber(0, 3)
            sgst = FormatNumber(0, 3)
        End If
        ''SD CALCULATATION
        Dim sd_order As Decimal = 0.0
        conn.Open()
        Dim MC9 As New SqlCommand
        MC9.CommandText = "select SD_AMOUNT from ORDER_DETAILS where so_no= '" & Label398.Text & "'"
        MC9.Connection = conn
        dr = MC9.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox179.Text = dr.Item("SD_AMOUNT")
            dr.Close()
            conn.Close()
        Else
            conn.Close()
        End If


        TextBox122.Text = sgst
        TextBox123.Text = cgst
        TextBox125.Text = igst
        TextBox37.Text = cess

        If (w_unit = "Vehicle") Then
            TextBox147.Text = u_price_trailor
        Else
            TextBox147.Text = TextBox120.Text
        End If

        TextBox149.Text = TextBox124.Text
        TextBox151.Text = TextBox121.Text
        TextBox153.Text = TextBox122.Text
        TextBox155.Text = TextBox123.Text
        TextBox157.Text = TextBox125.Text
        TextBox38.Text = TextBox37.Text
        ''TextBox159.Text = TextBox137.Text
        'TextBox143.Text = TextBox142.Text
    End Sub

    Protected Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        Dim mc1 As New SqlCommand
        Dim SUPL_TYPE As New String("")

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                conn.Open()
                mc1.CommandText = "select * from supl where supl_id='" & Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1).Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    SUPL_TYPE = dr.Item("SUPL_TYPE")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

                'Database updation entry
                If TextBox1.Text = "" Or IsDate(TextBox1.Text) = False Then
                    TextBox1.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter valuation date."
                    Return
                ElseIf garn_crrnoDropDownList.SelectedValue = "Select" Then
                    garn_crrnoDropDownList.Focus()
                    Return
                ElseIf DropDownList6.SelectedValue = "Select" Or DropDownList6.SelectedValue = "" Then
                    DropDownList6.Focus()
                    Return
                ElseIf TextBox143.Text = "" Or IsNumeric(TextBox143.Text) = False Then
                    TextBox143.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter Penality or 0"
                    Return
                ElseIf TextBox147.Text = "" Or IsNumeric(TextBox147.Text) = False Or TextBox147.Text < 0 Then
                    TextBox147.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter Unit Price"
                    Return
                ElseIf TextBox149.Text = "" Or IsNumeric(TextBox149.Text) = False Then
                    TextBox149.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter Discount Percentege or 0"
                    Return
                ElseIf TextBox151.Text = "" Or IsNumeric(TextBox151.Text) = False Then
                    TextBox151.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter Any Material Rate or 0"
                    Return
                ElseIf TextBox153.Text = "" Or IsNumeric(TextBox153.Text) = False Then
                    TextBox153.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter Service tax by Provider Percentage or 0"
                    Return
                ElseIf TextBox155.Text = "" Or IsNumeric(TextBox155.Text) = False Then
                    TextBox155.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter Service tax by Receiver Percentage or 0"
                    Return
                ElseIf TextBox157.Text = "" Or IsNumeric(TextBox157.Text) = False Then
                    TextBox157.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter W.C. Tax Percentage or 0"
                    Return
                ElseIf TextBox159.Text = "" Or IsNumeric(TextBox159.Text) = False Then
                    TextBox159.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please Enter T.D.S. Percentage or 0"
                    Return
                ElseIf DropDownList43.SelectedValue = "Select" Then
                    DropDownList43.Focus()
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "Please select R.C.M. "
                    Return
                ElseIf (CDec(TextBox180.Text) > 0 And CDec(TextBox159.Text) > 0) Then
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "TDS cannot be filled on both the textboxes."
                    Return
                ElseIf ((CDec(TextBox153.Text) + CDec(TextBox155.Text) + CDec(TextBox157.Text)) <= 0) Then
                    GARN_ERR_LABLE.Visible = True
                    GARN_ERR_LABLE.Text = "GST percentage cannot be 0."
                    Return
                Else
                    GARN_ERR_LABLE.Visible = False

                End If
                GARN_ERR_LABLE.Visible = False

                ''delete garn_price

                mycommand = New SqlCommand("DELETE FROM GARN_PRICE WHERE CRR_NO ='" & garn_crrnoDropDownList.SelectedValue & "' and po_no ='" & Label398.Text & "' and MAT_SLNO=" & DropDownList6.SelectedValue, conn_trans, myTrans)
                mycommand.ExecuteNonQuery()

                ''save garn_price

                Dim query As String = "Insert Into GARN_PRICE(LD_CHARGE,CRR_NO,PO_NO,MAT_SLNO,UNIT_PRICE,DISCOUNT,PACKING,SGST,CGST,IGST,CESS,ENTRY_TAX,PENALITY_TYPE,PENALITY,REMARKS_PENALITY,REMARKS_MODIFY,LAST_MODIFY_BY ) values (@LD_CHARGE,@CRR_NO,@PO_NO,@MAT_SLNO,@UNIT_PRICE,@DISCOUNT,@PACKING,@SGST,@CGST,@IGST,@CESS,@ENTRY_TAX,@PENALITY_TYPE,@PENALITY,@REMARKS_PENALITY,@REMARKS_MODIFY,@LAST_MODIFY_BY)"
                Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@CRR_NO", garn_crrnoDropDownList.SelectedValue)
                cmd.Parameters.AddWithValue("@PO_NO", Label398.Text)
                cmd.Parameters.AddWithValue("@MAT_SLNO", DropDownList6.SelectedValue)
                cmd.Parameters.AddWithValue("@UNIT_PRICE", TextBox147.Text)
                cmd.Parameters.AddWithValue("@DISCOUNT", TextBox149.Text)
                cmd.Parameters.AddWithValue("@PACKING", TextBox151.Text)
                cmd.Parameters.AddWithValue("@SGST", TextBox153.Text)
                cmd.Parameters.AddWithValue("@CGST", TextBox155.Text)
                cmd.Parameters.AddWithValue("@IGST", TextBox157.Text)
                cmd.Parameters.AddWithValue("@CESS", TextBox38.Text)
                cmd.Parameters.AddWithValue("@ENTRY_TAX", TextBox159.Text)
                cmd.Parameters.AddWithValue("@PENALITY_TYPE", "0")
                cmd.Parameters.AddWithValue("@PENALITY", TextBox143.Text)
                cmd.Parameters.AddWithValue("@REMARKS_PENALITY", TextBox144.Text)
                cmd.Parameters.AddWithValue("@REMARKS_MODIFY", TextBox145.Text)
                cmd.Parameters.AddWithValue("@LD_CHARGE", TextBox2.Text)
                cmd.Parameters.AddWithValue("@LAST_MODIFY_BY", Session("userName"))
                cmd.ExecuteReader()
                cmd.Dispose()


                ''put value
                Dim other_payment, basic_price, disc_price, material_price, tds_price, SGST_VALUE, CGST_VALUE, IGST_VALUE, CESS_VALUE As New Decimal(0)
                Dim unit_price, penality, material_rate, discount, tds, SGST, CGST, IGST, CESS, TDS_SGST, TDS_CGST, TDS_IGST As Decimal
                unit_price = TextBox147.Text
                discount = TextBox149.Text
                material_rate = TextBox151.Text
                SGST = TextBox153.Text
                CGST = TextBox155.Text
                IGST = TextBox157.Text
                CESS = TextBox38.Text

                ''TDS FOR CONTRACTS
                If (CDec(TextBox180.Text) > 0) Then
                    tds = TextBox180.Text
                Else
                    tds = TextBox159.Text
                End If

                penality = TextBox143.Text
                Dim sd_order As Decimal = TextBox179.Text

                Dim TAXABLE_AMOUNT, PEN_PRICE, PROV_PRICE, SUND_PRICE, LD_PRICE, SD_PRICE, PSC_PRICE, sgst_price, cgst_price, igst_price, cess_price, sgst_liab, cgst_liab, igst_liab, cess_liab, other_deduction As New Decimal(0)

                ''grid view search
                Dim I As Integer
                For I = 0 To GridView6.Rows.Count - 1
                    If GridView6.Rows(I).Cells(3).Text = DropDownList6.SelectedValue Then
                        basic_price = FormatNumber(unit_price * CDec(GridView6.Rows(I).Cells(8).Text), 2)
                        other_payment = FormatNumber((TextBox60.Text), 2)
                        disc_price = FormatNumber((basic_price * discount) / 100, 2)
                        material_price = FormatNumber(material_rate * CDec(GridView6.Rows(I).Cells(8).Text), 2)


                        'If (DropDownList4.SelectedValue = "Yes") Then
                        '    SGST_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * SGST) / 100)
                        '    CGST_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * CGST) / 100)
                        '    IGST_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * IGST) / 100)
                        '    CESS_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * CESS) / 100)
                        'Else
                        '    SGST_VALUE = 0
                        '    CGST_VALUE = 0
                        '    IGST_VALUE = 0
                        '    CESS_VALUE = 0
                        'End If

                        SGST_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * SGST) / 100)
                        CGST_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * CGST) / 100)
                        IGST_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * IGST) / 100)
                        CESS_VALUE = CInt(((basic_price - disc_price + material_price + other_payment) * CESS) / 100)

                        TDS_SGST = CInt(((basic_price - disc_price + material_price + other_payment) * CDec(TextBox51.Text)) / 100)
                        TDS_CGST = CInt(((basic_price - disc_price + material_price + other_payment) * CDec(TextBox52.Text)) / 100)
                        TDS_IGST = CInt(((basic_price - disc_price + material_price + other_payment) * CDec(TextBox53.Text)) / 100)


                        tds_price = CInt(((basic_price - disc_price + other_payment) * tds) / 100)
                        sd_order = CInt((((basic_price - disc_price) + material_price) * sd_order) / 100)
                        GridView6.Rows(I).Cells(13).Text = basic_price - disc_price + material_price + other_payment
                        GridView6.Rows(I).Cells(14).Text = material_price
                        GridView6.Rows(I).Cells(15).Text = TextBox143.Text
                        GridView6.Rows(I).Cells(28).Text = TextBox2.Text
                        'RCM GST
                        GridView6.Rows(I).Cells(29).Text = "0.00"
                        GridView6.Rows(I).Cells(30).Text = "0.00"
                        GridView6.Rows(I).Cells(31).Text = "0.00"
                        GridView6.Rows(I).Cells(32).Text = "0.00"
                        GridView6.Rows(I).Cells(35).Text = TDS_SGST
                        GridView6.Rows(I).Cells(36).Text = TDS_CGST
                        GridView6.Rows(I).Cells(37).Text = TDS_IGST
                        If DropDownList43.SelectedValue = "Yes" Then
                            GridView6.Rows(I).Cells(16).Text = SGST_VALUE
                            GridView6.Rows(I).Cells(17).Text = CGST_VALUE
                            GridView6.Rows(I).Cells(18).Text = IGST_VALUE
                            GridView6.Rows(I).Cells(19).Text = CESS_VALUE
                            GridView6.Rows(I).Cells(20).Text = SGST_VALUE
                            GridView6.Rows(I).Cells(21).Text = CGST_VALUE
                            GridView6.Rows(I).Cells(22).Text = IGST_VALUE
                            GridView6.Rows(I).Cells(23).Text = CESS_VALUE
                        ElseIf DropDownList43.SelectedValue = "No" Then
                            GridView6.Rows(I).Cells(16).Text = SGST_VALUE
                            GridView6.Rows(I).Cells(17).Text = CGST_VALUE
                            GridView6.Rows(I).Cells(18).Text = IGST_VALUE
                            GridView6.Rows(I).Cells(19).Text = CESS_VALUE
                            GridView6.Rows(I).Cells(20).Text = "0.00"
                            GridView6.Rows(I).Cells(21).Text = "0.00"
                            GridView6.Rows(I).Cells(22).Text = "0.00"
                            GridView6.Rows(I).Cells(23).Text = "0.00"
                        End If


                        SUND_PRICE = SUND_PRICE + (CDec(GridView6.Rows(I).Cells(13).Text) + CDec(GridView6.Rows(I).Cells(14).Text))
                        sgst_price = sgst_price + (CDec(GridView6.Rows(I).Cells(16).Text))
                        cgst_price = cgst_price + (CDec(GridView6.Rows(I).Cells(17).Text))
                        igst_price = igst_price + (CDec(GridView6.Rows(I).Cells(18).Text))
                        cess_price = cess_price + (CDec(GridView6.Rows(I).Cells(19).Text))
                        sgst_liab = sgst_liab + CDec(GridView6.Rows(I).Cells(20).Text)
                        cgst_liab = cgst_liab + (CDec(GridView6.Rows(I).Cells(21).Text))
                        igst_liab = igst_liab + (CDec(GridView6.Rows(I).Cells(22).Text))
                        cess_liab = cess_liab + (CDec(GridView6.Rows(I).Cells(23).Text))



                        GridView6.Rows(I).Cells(24).Text = tds_price
                        GridView6.Rows(I).Cells(25).Text = sd_order
                        '''''''
                        'SUND_PRICE = (PROV_PRICE + sgst_price + cgst_price + igst_price + cess_price) - (LD_PRICE + PEN_PRICE + SD_PRICE + TDS_PRICE + sgst_liab + cgst_liab + igst_liab + cess_liab + RCM_SGST + RCM_CGST + RCM_IGST + RCM_CESS)
                        'SUND_PRICE = FormatNumber(CDec(GridView6.Rows(I).Cells(13).Text) + SGST_VALUE + CGST_VALUE + IGST_VALUE + CESS_VALUE - (CInt(GridView6.Rows(I).Cells(28).Text) + CInt(GridView6.Rows(I).Cells(15).Text) + sd_order + tds_price + CInt(GridView1.Rows(I).Cells(20).Text) + CInt(GridView1.Rows(I).Cells(21).Text) + CInt(GridView1.Rows(I).Cells(22).Text) + CInt(GridView1.Rows(I).Cells(23).Text) + RCM_SGST + RCM_CGST + RCM_IGST + RCM_CESS), 2)
                        '''''''
                        ''GridView6.Rows(I).Cells(26).Text = FormatNumber(((GridView6.Rows(I).Cells(11).Text) + CDec(GridView6.Rows(I).Cells(12).Text)) - (CDec(GridView6.Rows(I).Cells(13).Text) + CDec(GridView6.Rows(I).Cells(14).Text)), 2)
                        ''GridView6.Rows(I).Cells(26).Text = CDec((basic_price) - CDec(GridView6.Rows(I).Cells(11).Text)) + FormatNumber(CInt(CDec((basic_price - disc_price + other_payment + sgst_price + cgst_price + igst_price + cess_price) - (LD_PRICE + PEN_PRICE + SD_PRICE + tds_price + sgst_liab + cgst_liab + igst_liab + cess_liab + other_deduction + TDS_SGST + TDS_CGST + TDS_IGST))) - FormatNumber(CDec((basic_price - disc_price + other_payment + sgst_price + cgst_price + igst_price + cess_price) - (LD_PRICE + PEN_PRICE + SD_PRICE + tds_price + sgst_liab + cgst_liab + igst_liab + cess_liab + other_deduction + TDS_SGST + TDS_CGST + TDS_IGST)), 2), 2)
                        GridView6.Rows(I).Cells(26).Text = CDec((basic_price) - CDec(GridView6.Rows(I).Cells(11).Text))





                        GridView6.Rows(I).Cells(27).Text = DropDownList43.SelectedValue
                        If (TextBox45.Text = "") Then
                            GridView6.Rows(I).Cells(33).Text = ""
                        Else
                            GridView6.Rows(I).Cells(33).Text = TextBox45.Text.Substring(0, TextBox45.Text.IndexOf(",") - 1).Trim()
                        End If

                        GridView6.Rows(I).Cells(34).Text = TextBox44.Text

                        If (TextBox61.Text = "") Then
                            GridView6.Rows(I).Cells(38).Text = ""
                        Else
                            GridView6.Rows(I).Cells(38).Text = TextBox61.Text.Substring(0, TextBox61.Text.IndexOf(",") - 1).Trim()
                        End If

                        GridView6.Rows(I).Cells(39).Text = TextBox60.Text

                        GridView6.Rows(I).Cells(40).Text = TextBox153.Text
                        GridView6.Rows(I).Cells(41).Text = TextBox155.Text
                        GridView6.Rows(I).Cells(42).Text = TextBox157.Text
                        GridView6.Rows(I).Cells(43).Text = TextBox38.Text

                    End If
                Next

                myTrans.Commit()

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                GARN_ERR_LABLE.Visible = True
                GARN_ERR_LABLE.Text = "There was some Error, please contact EDP."

            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub
    Protected Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click

        Panel30.Visible = True
        Panel37.Visible = False
    End Sub

    Protected Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        'check
        If TextBox160.Text = "" Then
            TextBox160.Focus()
            Return
        ElseIf IsDate(TextBox161.Text) = False Then
            TextBox161.Focus()
            Return
        ElseIf GridView6.Rows.Count = 0 Then
            garn_crrnoDropDownList.Focus()
            Return
        End If
        Panel37.Visible = True

    End Sub

    Protected Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click
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
                If TextBox172.Text = "" Then
                    TextBox172.Focus()
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

                '''''''''''''''''''''''''''''''''
                ''Checking Valuation date and Freeze date
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
                    Label43.Visible = True
                    Label43.Text = "Valuation before " & Block_DATE & " has been freezed."
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
                    End If
                    conn.Close()
                    If password = TextBox172.Text Then
                        ''update mb book

                        Dim i As Integer = 0
                        For i = 0 To GridView6.Rows.Count - 1

                            Dim cmd As New SqlCommand
                            Dim Query_1 As String = "update mb_book set TAXABLE_VALUE=@TAXABLE_VALUE,BillTrackID=@BillTrackID,GST_STATUS=@GST_STATUS,Valuation_Date=@Valuation_Date,other_payment=@other_payment,other_deduction=@other_deduction,rcm_sgst=@rcm_sgst,rcm_cgst=@rcm_cgst,rcm_igst=@rcm_igst,rcm_cess=@rcm_cess,ld=@ld, rcm=@rcm, sgst=@sgst,cgst=@cgst ,igst=@igst,cess=@cess,sgst_liab=@sgst_liab,cgst_liab=@cgst_liab,igst_liab=@igst_liab,cess_liab=@cess_liab,inv_no=@inv_no,inv_date=@inv_date,prov_amt=@prov_amt,pen_amt=@pen_amt,it_amt=@it_amt,pay_ind=@pay_ind,v_ind=@v_ind,mat_rate=@mat_rate,valuation_amt=@valuation_amt WHERE po_no ='" & Label398.Text & "' AND wo_slno =" & CDec(GridView6.Rows(i).Cells(3).Text) & " AND mb_no  ='" & GridView6.Rows(i).Cells(0).Text & "'  AND v_ind IS NULL"
                            cmd = New SqlCommand(Query_1, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@inv_no", TextBox160.Text)
                            cmd.Parameters.AddWithValue("@inv_date", Date.ParseExact(TextBox161.Text, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@prov_amt", CDec(GridView6.Rows(i).Cells(13).Text))
                            cmd.Parameters.AddWithValue("@pen_amt", CDec(GridView6.Rows(i).Cells(15).Text))
                            cmd.Parameters.AddWithValue("@sgst", CDec(GridView6.Rows(i).Cells(16).Text))
                            cmd.Parameters.AddWithValue("@cgst", CDec(GridView6.Rows(i).Cells(17).Text))
                            cmd.Parameters.AddWithValue("@igst", CDec(GridView6.Rows(i).Cells(18).Text))
                            cmd.Parameters.AddWithValue("@cess", CDec(GridView6.Rows(i).Cells(19).Text))
                            cmd.Parameters.AddWithValue("@sgst_liab", CDec(GridView6.Rows(i).Cells(20).Text))
                            cmd.Parameters.AddWithValue("@cgst_liab", CDec(GridView6.Rows(i).Cells(21).Text))
                            cmd.Parameters.AddWithValue("@igst_liab", CDec(GridView6.Rows(i).Cells(22).Text))
                            cmd.Parameters.AddWithValue("@cess_liab", CDec(GridView6.Rows(i).Cells(23).Text))
                            cmd.Parameters.AddWithValue("@rcm_sgst", CDec(GridView6.Rows(i).Cells(29).Text))
                            cmd.Parameters.AddWithValue("@rcm_cgst", CDec(GridView6.Rows(i).Cells(30).Text))
                            cmd.Parameters.AddWithValue("@rcm_igst", CDec(GridView6.Rows(i).Cells(31).Text))
                            cmd.Parameters.AddWithValue("@rcm_cess", CDec(GridView6.Rows(i).Cells(32).Text))
                            cmd.Parameters.AddWithValue("@ld", CDec(GridView6.Rows(i).Cells(28).Text))
                            cmd.Parameters.AddWithValue("@it_amt", CDec(GridView6.Rows(i).Cells(24).Text))
                            cmd.Parameters.AddWithValue("@mat_rate", CDec(GridView6.Rows(i).Cells(14).Text))
                            cmd.Parameters.AddWithValue("@pay_ind", "")
                            cmd.Parameters.AddWithValue("@v_ind", "V")
                            cmd.Parameters.AddWithValue("@Valuation_Date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@rcm", GridView6.Rows(i).Cells(27).Text)
                            cmd.Parameters.AddWithValue("@valuation_amt", CDec(GridView6.Rows(i).Cells(13).Text))
                            cmd.Parameters.AddWithValue("@other_deduction", CDec(GridView6.Rows(i).Cells(34).Text))
                            cmd.Parameters.AddWithValue("@other_payment", CDec(GridView6.Rows(i).Cells(39).Text))
                            If (DropDownList4.SelectedValue = "Yes") Then
                                cmd.Parameters.AddWithValue("@GST_STATUS", "PAID")
                            Else
                                cmd.Parameters.AddWithValue("@GST_STATUS", "PENDING")
                            End If
                            cmd.Parameters.AddWithValue("@BillTrackID", DropDownList40.Text)
                            cmd.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(GridView6.Rows(i).Cells(13).Text))
                            cmd.ExecuteReader()
                            cmd.Dispose()



                            If (DropDownList3.SelectedValue = "Yes") Then
                                If ((CDec(GridView6.Rows(i).Cells(40).Text) + CDec(GridView6.Rows(i).Cells(41).Text) + CDec(GridView6.Rows(i).Cells(42).Text)) > 0) Then
                                    ''INSERT DATA INTO TAXABLE VALUES TABLE
                                    Query_1 = "INSERT INTO Taxable_Values (GST_PAYMENT_VOUCHER_NO,GST_PAYMENT_DATE,GST_STATUS,INVOICE_NO,INVOICE_DATE,ENTRY_DATE,RCM_CGST_AMT,RCM_SGST_AMT,RCM_IGST_AMT,RCM_CESS_AMT,GARN_CRR_MB_NO,VALUATION_DATE,DATA_TYPE,SL_NO,SUPL_CODE,SUPL_NAME,TAXABLE_VALUE,FISCAL_YEAR,CGST_PERCENTAGE,SGST_PERCENTAGE,IGST_PERCENTAGE,CESS_PERCENTAGE,CGST_AMT,SGST_AMT,IGST_AMT,CESS_AMT,TAXABLE_LD_PENALTY,CGST_LD_PENALTY,SGST_LD_PENALTY,IGST_LD_PENALTY,CESS_LD_PENALTY,CGST_TDS,SGST_TDS,IGST_TDS,CESS_TDS)VALUES(@GST_PAYMENT_VOUCHER_NO,@GST_PAYMENT_DATE,@GST_STATUS,@INVOICE_NO,@INVOICE_DATE,@ENTRY_DATE,@RCM_CGST_AMT,@RCM_SGST_AMT,@RCM_IGST_AMT,@RCM_CESS_AMT,@GARN_CRR_MB_NO,@VALUATION_DATE,@DATA_TYPE,@SL_NO,@SUPL_CODE,@SUPL_NAME,@TAXABLE_VALUE,@FISCAL_YEAR,@CGST_PERCENTAGE,@SGST_PERCENTAGE,@IGST_PERCENTAGE,@CESS_PERCENTAGE,@CGST_AMT,@SGST_AMT,@IGST_AMT,@CESS_AMT,@TAXABLE_LD_PENALTY,@CGST_LD_PENALTY,@SGST_LD_PENALTY,@IGST_LD_PENALTY,@CESS_LD_PENALTY,@CGST_TDS,@SGST_TDS,@IGST_TDS,@CESS_TDS)"
                                    cmd = New SqlCommand(Query_1, conn_trans, myTrans)
                                    cmd.Parameters.AddWithValue("@INVOICE_NO", TextBox160.Text)
                                    cmd.Parameters.AddWithValue("@INVOICE_DATE", Date.ParseExact(TextBox161.Text, "dd-MM-yyyy", provider))
                                    cmd.Parameters.AddWithValue("@GARN_CRR_MB_NO", GridView6.Rows(i).Cells(0).Text)
                                    cmd.Parameters.AddWithValue("@VALUATION_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                                    cmd.Parameters.AddWithValue("@DATA_TYPE", "CONTRACT")
                                    cmd.Parameters.AddWithValue("@SL_NO", CDec(GridView6.Rows(i).Cells(3).Text))
                                    cmd.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                                    cmd.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                                    cmd.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(GridView6.Rows(i).Cells(13).Text))
                                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                                    cmd.Parameters.AddWithValue("@SGST_PERCENTAGE", CDec(GridView6.Rows(i).Cells(40).Text))
                                    cmd.Parameters.AddWithValue("@CGST_PERCENTAGE", CDec(GridView6.Rows(i).Cells(41).Text))
                                    cmd.Parameters.AddWithValue("@IGST_PERCENTAGE", CDec(GridView6.Rows(i).Cells(42).Text))
                                    cmd.Parameters.AddWithValue("@CESS_PERCENTAGE", CDec(GridView6.Rows(i).Cells(43).Text))
                                    If (DropDownList43.SelectedValue = "Yes") Then
                                        cmd.Parameters.AddWithValue("@SGST_AMT", 0)
                                        cmd.Parameters.AddWithValue("@CGST_AMT", 0)
                                        cmd.Parameters.AddWithValue("@IGST_AMT", 0)
                                        cmd.Parameters.AddWithValue("@CESS_AMT", 0)
                                        cmd.Parameters.AddWithValue("@RCM_SGST_AMT", CDec(GridView6.Rows(i).Cells(20).Text))
                                        cmd.Parameters.AddWithValue("@RCM_CGST_AMT", CDec(GridView6.Rows(i).Cells(21).Text))
                                        cmd.Parameters.AddWithValue("@RCM_IGST_AMT", CDec(GridView6.Rows(i).Cells(22).Text))
                                        cmd.Parameters.AddWithValue("@RCM_CESS_AMT", CDec(GridView6.Rows(i).Cells(23).Text))
                                    Else
                                        cmd.Parameters.AddWithValue("@SGST_AMT", CDec(GridView6.Rows(i).Cells(16).Text))
                                        cmd.Parameters.AddWithValue("@CGST_AMT", CDec(GridView6.Rows(i).Cells(17).Text))
                                        cmd.Parameters.AddWithValue("@IGST_AMT", CDec(GridView6.Rows(i).Cells(18).Text))
                                        cmd.Parameters.AddWithValue("@CESS_AMT", CDec(GridView6.Rows(i).Cells(19).Text))
                                        cmd.Parameters.AddWithValue("@RCM_SGST_AMT", 0)
                                        cmd.Parameters.AddWithValue("@RCM_CGST_AMT", 0)
                                        cmd.Parameters.AddWithValue("@RCM_IGST_AMT", 0)
                                        cmd.Parameters.AddWithValue("@RCM_CESS_AMT", 0)
                                    End If

                                    cmd.Parameters.AddWithValue("@TAXABLE_LD_PENALTY", CDec(GridView6.Rows(i).Cells(15).Text) + CDec(GridView6.Rows(i).Cells(28).Text))
                                    cmd.Parameters.AddWithValue("@SGST_LD_PENALTY", CDec(GridView6.Rows(i).Cells(29).Text))
                                    cmd.Parameters.AddWithValue("@CGST_LD_PENALTY", CDec(GridView6.Rows(i).Cells(30).Text))
                                    cmd.Parameters.AddWithValue("@IGST_LD_PENALTY", CDec(GridView6.Rows(i).Cells(31).Text))
                                    cmd.Parameters.AddWithValue("@CESS_LD_PENALTY", CDec(GridView6.Rows(i).Cells(32).Text))
                                    cmd.Parameters.AddWithValue("@SGST_TDS", CDec(GridView6.Rows(i).Cells(35).Text))
                                    cmd.Parameters.AddWithValue("@CGST_TDS", CDec(GridView6.Rows(i).Cells(36).Text))
                                    cmd.Parameters.AddWithValue("@IGST_TDS", CDec(GridView6.Rows(i).Cells(37).Text))
                                    cmd.Parameters.AddWithValue("@CESS_TDS", 0)
                                    cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                                    If (DropDownList4.SelectedValue = "Yes") Then
                                        cmd.Parameters.AddWithValue("@GST_STATUS", "PAID")
                                        cmd.Parameters.AddWithValue("@GST_PAYMENT_DATE", CDate(DateTime.Now.ToString("dd-MM-yyyy")))
                                        cmd.Parameters.AddWithValue("@GST_PAYMENT_VOUCHER_NO", "")

                                    Else
                                        cmd.Parameters.AddWithValue("@GST_STATUS", "PENDING")
                                        cmd.Parameters.AddWithValue("@GST_PAYMENT_DATE", DBNull.Value)
                                        cmd.Parameters.AddWithValue("@GST_PAYMENT_VOUCHER_NO", DBNull.Value)
                                    End If
                                    cmd.ExecuteReader()
                                    cmd.Dispose()
                                End If
                            End If


                        Next


                        ''SEARCH AC HEAD
                        Dim PROV_HEAD, SUND_HEAD, SGST_HEAD, CGST_HEAD, IGST_HEAD, CESS_HEAD, SGST_LIAB_HEAD, CGST_LIAB_HEAD, IGST_LIAB_HEAD, CESS_LIAB_HEAD, LD_HEAD, TDS_HEAD, SD_HEAD, PSC_HEAD, OTHER_DEDUCTION_HEAD, OTHER_PAYMENT_HEAD As New String("")
                        Dim SGST_LIAB_LD_PENALTY_HEAD, CGST_LIAB_LD_PENALTY_HEAD, IGST_LIAB_LD_PENALTY_HEAD, CESS_LIAB_LD_PENALTY_HEAD, TDS_SGST_HEAD, TDS_CGST_HEAD, TDS_IGST_HEAD As New String("")
                        conn.Open()
                        Dim MC6 As New SqlCommand
                        'MC6.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & Label398.Text & "')"
                        MC6.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & Label398.Text & "') and work_type = (select top 1 WO_TYPE from WO_ORDER where PO_NO = '" & Label398.Text & "')"
                        MC6.Connection = conn
                        dr = MC6.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            PROV_HEAD = dr.Item("prov_head")
                            SUND_HEAD = dr.Item("sund_head")
                            If (DropDownList3.SelectedValue = "Yes") Then
                                If (DropDownList43.SelectedValue = "Yes") Then
                                    IGST_HEAD = "64821"
                                    CGST_HEAD = "64822"
                                    SGST_HEAD = "64823"
                                    CESS_HEAD = "64825"
                                    SGST_LIAB_HEAD = "54813"
                                    CGST_LIAB_HEAD = "54812"
                                    IGST_LIAB_HEAD = "54811"
                                    CESS_LIAB_HEAD = dr.Item("lcess")
                                Else
                                    IGST_HEAD = dr.Item("igst")
                                    CGST_HEAD = dr.Item("cgst")
                                    SGST_HEAD = dr.Item("sgst")
                                    CESS_HEAD = dr.Item("cess")
                                    SGST_LIAB_HEAD = dr.Item("lsgst")
                                    CGST_LIAB_HEAD = dr.Item("lcgst")
                                    IGST_LIAB_HEAD = dr.Item("ligst")
                                    CESS_LIAB_HEAD = dr.Item("lcess")
                                End If
                            Else
                                If (DropDownList43.SelectedValue = "Yes") Then
                                    IGST_HEAD = "84811"
                                    CGST_HEAD = "84811"
                                    SGST_HEAD = "84811"
                                    CESS_HEAD = "84811"
                                    SGST_LIAB_HEAD = "54813"
                                    CGST_LIAB_HEAD = "54812"
                                    IGST_LIAB_HEAD = "54811"
                                    CESS_LIAB_HEAD = dr.Item("lcess")
                                Else
                                    IGST_HEAD = "84811"
                                    CGST_HEAD = "84811"
                                    SGST_HEAD = "84811"
                                    CESS_HEAD = "84811"
                                    SGST_LIAB_HEAD = dr.Item("lsgst")
                                    CGST_LIAB_HEAD = dr.Item("lcgst")
                                    IGST_LIAB_HEAD = dr.Item("ligst")
                                    CESS_LIAB_HEAD = dr.Item("lcess")
                                End If

                            End If


                            LD_HEAD = dr.Item("ld_head")
                            TDS_HEAD = dr.Item("tds_head")
                            SD_HEAD = dr.Item("sd_head")
                            PSC_HEAD = dr.Item("psc_head")
                            SGST_LIAB_LD_PENALTY_HEAD = dr.Item("sgst_liab_ld_pen")
                            CGST_LIAB_LD_PENALTY_HEAD = dr.Item("cgst_liab_ld_pen")
                            IGST_LIAB_LD_PENALTY_HEAD = dr.Item("igst_liab_ld_pen")
                            CESS_LIAB_LD_PENALTY_HEAD = dr.Item("cess_liab_ld_pen")

                            TDS_SGST_HEAD = dr.Item("TDS_SGST")
                            TDS_CGST_HEAD = dr.Item("TDS_CGST")
                            TDS_IGST_HEAD = dr.Item("TDS_IGST")

                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If


                        Dim L As Integer = 0
                        Dim TAXABLE_AMOUNT, TDS_SGST, TDS_CGST, TDS_IGST, PEN_PRICE, PROV_PRICE, SUND_PRICE, LD_PRICE, TDS_PRICE, SD_PRICE, sgst_price, cgst_price, igst_price, cess_price, sgst_liab, cgst_liab, igst_liab, cess_liab, other_deduction, other_payment, PSC_AMOUNT As New Decimal(0)

                        For L = 0 To GridView6.Rows.Count - 1
                            PROV_PRICE = PROV_PRICE + (CDec(GridView6.Rows(L).Cells(11).Text) + CDec(GridView6.Rows(L).Cells(12).Text))
                            SUND_PRICE = SUND_PRICE + (CDec(GridView6.Rows(L).Cells(13).Text) + CDec(GridView6.Rows(L).Cells(14).Text))
                            sgst_price = sgst_price + (CDec(GridView6.Rows(L).Cells(16).Text))
                            cgst_price = cgst_price + (CDec(GridView6.Rows(L).Cells(17).Text))
                            igst_price = igst_price + (CDec(GridView6.Rows(L).Cells(18).Text))
                            cess_price = cess_price + (CDec(GridView6.Rows(L).Cells(19).Text))
                            sgst_liab = sgst_liab + CDec(GridView6.Rows(L).Cells(20).Text)
                            cgst_liab = cgst_liab + (CDec(GridView6.Rows(L).Cells(21).Text))
                            igst_liab = igst_liab + (CDec(GridView6.Rows(L).Cells(22).Text))
                            cess_liab = cess_liab + (CDec(GridView6.Rows(L).Cells(23).Text))
                            'GST ON LD/PENALTY
                            ''SGST_LD_PENALTY = SGST_LD_PENALTY + (CDec(GridView6.Rows(L).Cells(29).Text))
                            ''CGST_LD_PENALTY = CGST_LD_PENALTY + CDec(GridView6.Rows(L).Cells(30).Text)
                            ''IGST_LD_PENALTY = IGST_LD_PENALTY + (CDec(GridView6.Rows(L).Cells(31).Text))
                            ''CESS_LD_PENALTY = CESS_LD_PENALTY + (CDec(GridView6.Rows(L).Cells(32).Text))

                            PEN_PRICE = PEN_PRICE + (CDec(GridView6.Rows(L).Cells(15).Text))
                            LD_PRICE = LD_PRICE + (CDec(GridView6.Rows(L).Cells(28).Text))
                            TDS_PRICE = TDS_PRICE + CDec(GridView6.Rows(L).Cells(24).Text)
                            SD_PRICE = SD_PRICE + CDec(GridView6.Rows(L).Cells(25).Text)
                            ''PSC_PRICE = PSC_PRICE + CDec(GridView6.Rows(L).Cells(26).Text)
                            other_deduction = other_deduction + CDec(GridView6.Rows(L).Cells(34).Text)
                            TDS_SGST = TDS_SGST + CDec(GridView6.Rows(L).Cells(35).Text)
                            TDS_CGST = TDS_CGST + CDec(GridView6.Rows(L).Cells(36).Text)
                            TDS_IGST = TDS_IGST + CDec(GridView6.Rows(L).Cells(37).Text)
                            other_payment = other_payment + CDec(GridView6.Rows(L).Cells(39).Text)
                            TAXABLE_AMOUNT = TAXABLE_AMOUNT + CDec(GridView6.Rows(L).Cells(13).Text)
                            PSC_AMOUNT = PSC_AMOUNT + CDec(GridView6.Rows(L).Cells(26).Text)

                        Next

                        If (DropDownList4.SelectedValue = "Yes") Then
                            SUND_PRICE = (SUND_PRICE + sgst_price + cgst_price + igst_price + cess_price + other_payment) - (LD_PRICE + PEN_PRICE + SD_PRICE + TDS_PRICE + sgst_liab + cgst_liab + igst_liab + cess_liab + other_deduction + TDS_SGST + TDS_CGST + TDS_IGST)
                        Else
                            SUND_PRICE = (SUND_PRICE + other_payment) - (LD_PRICE + PEN_PRICE + SD_PRICE + TDS_PRICE + other_deduction + TDS_SGST + TDS_CGST + TDS_IGST)
                        End If


                        ''PSC_PRICE = PSC_PRICE + (SUND_PRICE - CInt(SUND_PRICE))
                        PSC_AMOUNT = PSC_AMOUNT + (CInt(SUND_PRICE) - SUND_PRICE)
                        SUND_PRICE = CInt(SUND_PRICE)
                        SD_PRICE = CInt(SD_PRICE)


                        save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), PROV_HEAD, "Dr", PROV_PRICE, "PROV", DropDownList40.Text, 1, "")

                        If (DropDownList4.SelectedValue = "Yes") Then
                            If (DropDownList3.SelectedValue = "Yes") Then
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), SGST_HEAD, "Dr", sgst_price, "SGST", DropDownList40.Text, 2, "")
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), CGST_HEAD, "Dr", cgst_price, "CGST", DropDownList40.Text, 2, "")
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), IGST_HEAD, "Dr", igst_price, "IGST", DropDownList40.Text, 2, "")
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), CESS_HEAD, "Dr", cess_price, "CESS", DropDownList40.Text, 2, "")

                            Else
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), SGST_HEAD, "Dr", sgst_price, "SGST_EXPENSES", DropDownList40.Text, 2, "")
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), CGST_HEAD, "Dr", cgst_price, "CGST_EXPENSES", DropDownList40.Text, 2, "")
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), IGST_HEAD, "Dr", igst_price, "IGST_EXPENSES", DropDownList40.Text, 2, "")
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), CESS_HEAD, "Dr", cess_price, "CESS_EXPENSES", DropDownList40.Text, 2, "")
                            End If

                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), SGST_LIAB_HEAD, "Cr", sgst_liab, "SGST_LIAB", DropDownList40.Text, 3, "")
                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), CGST_LIAB_HEAD, "Cr", cgst_liab, "CGST_LIAB", DropDownList40.Text, 3, "")
                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), IGST_LIAB_HEAD, "Cr", igst_liab, "IGST_LIAB", DropDownList40.Text, 3, "")
                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), CESS_LIAB_HEAD, "Cr", cess_liab, "CESS_LIAB", DropDownList40.Text, 3, "")
                            ''RCM LIAB
                        End If


                        save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), SD_HEAD, "Cr", SD_PRICE, "SD", DropDownList40.Text, 5, "")
                        save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), LD_HEAD, "Cr", LD_PRICE + PEN_PRICE, "PENALITY_LD", DropDownList40.Text, 6, "")
                        If (CDec(TextBox180.Text) > 0) Then
                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), "51704", "Cr", TDS_PRICE, "TDS_194(O)", DropDownList40.Text, 7, "")
                        Else
                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), TDS_HEAD, "Cr", TDS_PRICE, "IT", DropDownList40.Text, 7, "")
                        End If

                        save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), SUND_HEAD, "Cr", SUND_PRICE, "SUND", DropDownList40.Text, 12, "")

                        ''TDS ON GST
                        save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), TDS_SGST_HEAD, "Cr", TDS_SGST, "TDS_SGST", DropDownList40.Text, 8, "")
                        save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), TDS_CGST_HEAD, "Cr", TDS_CGST, "TDS_CGST", DropDownList40.Text, 8, "")
                        save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), TDS_IGST_HEAD, "Cr", TDS_IGST, "TDS_IGST", DropDownList40.Text, 8, "")

                        ''psc entry in Ledger
                        If PSC_AMOUNT < 0 Then
                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), PSC_HEAD, "Cr", (PSC_AMOUNT * (-1)), "PSC", DropDownList40.Text, 8, "")
                        Else
                            save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), PSC_HEAD, "Dr", PSC_AMOUNT, "PSC", DropDownList40.Text, 8, "")
                        End If

                        For L = 0 To GridView6.Rows.Count - 1
                            If (GridView6.Rows(L).Cells(33).Text <> "") Then
                                OTHER_DEDUCTION_HEAD = GridView6.Rows(L).Cells(33).Text
                                other_deduction = CDec(GridView6.Rows(L).Cells(34).Text)
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), OTHER_DEDUCTION_HEAD, "Cr", other_deduction, "Other_Deduction", DropDownList40.Text, 12, "")
                            End If
                        Next

                        For L = 0 To GridView6.Rows.Count - 1
                            If (GridView6.Rows(L).Cells(38).Text <> "") Then
                                OTHER_PAYMENT_HEAD = GridView6.Rows(L).Cells(38).Text
                                other_payment = CDec(GridView6.Rows(L).Cells(39).Text)
                                save_ledger("0", Label398.Text, garn_crrnoDropDownList.SelectedValue, TextBox160.Text, Label396.Text.Substring(0, Label396.Text.IndexOf(",") - 1), OTHER_PAYMENT_HEAD, "Dr", other_payment, "Other_Payment", DropDownList40.Text, 12, "")
                            End If
                        Next




                        ''update ledger

                        Dim cmd11 As New SqlCommand
                        Dim Query11 As String = "update LEDGER set PAYMENT_INDICATION ='P' where PO_NO='" & Label398.Text & "' and INVOICE_NO is null and POST_INDICATION ='PROV' AND GARN_NO_MB_NO ='" & garn_crrnoDropDownList.SelectedValue & "'"
                        cmd11 = New SqlCommand(Query11, conn_trans, myTrans)
                        cmd11.ExecuteReader()
                        cmd11.Dispose()

                        ''insert party amount

                        Dim query As String = "INSERT INTO PARTY_AMT (POST_TYPE,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@POST_TYPE,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                        Dim cmd_1 As New SqlCommand(query, conn_trans, myTrans)
                        cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                        cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                        cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList40.SelectedValue)
                        cmd_1.Parameters.AddWithValue("@ORDER_NO", Label398.Text)
                        cmd_1.Parameters.AddWithValue("@GARN_MB_NO", garn_crrnoDropDownList.SelectedValue)
                        cmd_1.Parameters.AddWithValue("@AC_CODE", SUND_HEAD)
                        cmd_1.Parameters.AddWithValue("@AMOUNT_DR", 0.0)
                        cmd_1.Parameters.AddWithValue("@AMOUNT_CR", SUND_PRICE)
                        cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", SUND_PRICE)
                        cmd_1.Parameters.AddWithValue("@POST_TYPE", "SUND")
                        cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                        cmd_1.ExecuteReader()
                        cmd_1.Dispose()

                        If SD_PRICE > 0 Then
                            ''insert party amount

                            query = "INSERT INTO PARTY_AMT (POST_TYPE,IND,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@POST_TYPE,@IND,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                            cmd_1 = New SqlCommand(query, conn_trans, myTrans)
                            cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                            cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                            cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList40.SelectedValue)
                            cmd_1.Parameters.AddWithValue("@ORDER_NO", Label398.Text)
                            cmd_1.Parameters.AddWithValue("@GARN_MB_NO", garn_crrnoDropDownList.SelectedValue)
                            cmd_1.Parameters.AddWithValue("@AC_CODE", SD_HEAD)
                            cmd_1.Parameters.AddWithValue("@AMOUNT_DR", 0.0)
                            cmd_1.Parameters.AddWithValue("@AMOUNT_CR", SD_PRICE)
                            cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", SD_PRICE)
                            cmd_1.Parameters.AddWithValue("@POST_TYPE", "SD")
                            cmd_1.Parameters.AddWithValue("@IND", "P")
                            cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                            cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                            cmd_1.ExecuteReader()
                            cmd_1.Dispose()

                        End If


                        ''REFRESH
                        Dim ds5 As New DataSet
                        conn.Open()
                        dt.Clear()
                        da = New SqlDataAdapter("SELECT DISTINCT MB_NO FROM mb_book WITH(NOLOCK) WHERE v_ind IS NULL and mb_clear is not null AND po_no ='" & TextBox171.Text & "'", conn)
                        da.Fill(dt)
                        conn.Close()
                        garn_crrnoDropDownList.Items.Clear()
                        garn_crrnoDropDownList.DataSource = dt
                        garn_crrnoDropDownList.DataValueField = "mb_no"
                        garn_crrnoDropDownList.DataBind()
                        garn_crrnoDropDownList.Items.Add("Select")
                        garn_crrnoDropDownList.SelectedValue = "Select"
                        ds5.Tables.Clear()
                        DropDownList6.Items.Clear()
                        Panel37.Visible = False
                        ''gridview clear
                        Dim DT11 As New DataTable
                        DT11.Columns.AddRange(New DataColumn(17) {New DataColumn("mb_no"), New DataColumn("mb_date"), New DataColumn("po_no"), New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("w_au"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty"), New DataColumn("prov_amt"), New DataColumn("pen_amt"), New DataColumn("st_amt_r"), New DataColumn("st_amt_p"), New DataColumn("wct_amt"), New DataColumn("it_amt"), New DataColumn("mat_rate")})
                        GridView6.DataSource = DT11
                        GridView6.DataBind()
                    Else
                        Label43.Text = "Auth. Failed"
                        TextBox172.Text = ""
                        Return
                    End If

                End If

                myTrans.Commit()
                GARN_ERR_LABLE.Visible = True
                GARN_ERR_LABLE.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                GARN_ERR_LABLE.Visible = True
                GARN_ERR_LABLE.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub
    Protected Sub save_ledger(ItemSlNo As String, so_no As String, garn_mb As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, token_no As String, line_no As Integer, PAY_IND As String)

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
            Dim Query As String = "Insert Into LEDGER(Journal_ID,AGING_FLAG,JURNAL_LINE_NO,BILL_TRACK_ID,INVOICE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@Journal_ID, @AGING_FLAG,@JURNAL_LINE_NO,@BILL_TRACK_ID,@INVOICE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn_trans, myTrans)
            cmd.Parameters.AddWithValue("@Journal_ID", ItemSlNo)
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
            If (TextBox57.Text = "N/A") Then
                cmd.Parameters.AddWithValue("@AGING_FLAG", inv_no)
            Else
                cmd.Parameters.AddWithValue("@AGING_FLAG", TextBox57.Text)
            End If

            cmd.ExecuteReader()
            cmd.Dispose()

            ''

            ''update ledger
            Dim Query11 As New String("")
            If (TextBox57.Text = "N/A") Then

                Query11 = "update LEDGER set AGING_FLAG ='" & inv_no & "' where PO_NO='" & so_no & "' AND AMOUNT_CR >0 and POST_INDICATION ='PROV' AND GARN_NO_MB_NO ='" & garn_mb & "'"

            Else
                Query11 = "update LEDGER set AGING_FLAG ='" & TextBox57.Text & "' where PO_NO='" & so_no & "'  AND AMOUNT_CR >0 and POST_INDICATION ='PROV' AND GARN_NO_MB_NO ='" & garn_mb & "'"
            End If

            Dim cmd11 As New SqlCommand
            cmd11 = New SqlCommand(Query11, conn_trans, myTrans)
            cmd11.ExecuteReader()
            cmd11.Dispose()


        End If
    End Sub

    Protected Sub M_garn_crrnoDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles M_garn_crrnoDropDownList.SelectedIndexChanged
        If M_garn_crrnoDropDownList.SelectedValue = "Select" Then
            M_garn_crrnoDropDownList.Focus()
            Return
        End If
        conn.Open()
        If (Left(M_garn_crrnoDropDownList.Text, 1) = "O") Then
            da = New SqlDataAdapter("select PO_RCD_MAT.MAT_SLNO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN Outsource_F_ITEM ON Outsource_F_ITEM.ITEM_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .GARN_NO='" & M_garn_crrnoDropDownList.Text & "' and PO_RCD_MAT.V_IND IS NULL", conn)
        Else
            da = New SqlDataAdapter("select PO_RCD_MAT.MAT_SLNO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .GARN_NO='" & M_garn_crrnoDropDownList.Text & "' and PO_RCD_MAT.V_IND IS NULL", conn)
        End If

        da.Fill(dt)
        conn.Close()
        M_DropDownList6.Items.Clear()
        M_DropDownList6.DataSource = dt
        M_DropDownList6.DataValueField = "MAT_SLNO"
        M_DropDownList6.DataBind()
        M_DropDownList6.Items.Insert(0, "Select")
        M_DropDownList6.SelectedValue = "Select"
        M_GARN_ERR_LABLE.Visible = False
        M_GARN_ERR_LABLE.Text = ""
        conn.Open()
        dt.Clear()

        If (Left(M_garn_crrnoDropDownList.Text, 1) = "O") Then
            da = New SqlDataAdapter("select PO_RCD_MAT.TOTAL_MT, PO_RCD_MAT.TRANS_CHARGE, PO_RCD_MAT.TRANS_SHORT,PO_RCD_MAT.PROV_VALUE,PO_RCD_MAT.PO_NO,PO_RCD_MAT.CRR_NO,PO_RCD_MAT.CHLN_NO,PO_RCD_MAT.MAT_SLNO,PO_RCD_MAT.MAT_CODE,Outsource_F_ITEM.ITEM_NAME AS MAT_NAME,Outsource_F_ITEM.ITEM_AU AS MAT_AU,PO_ORD_MAT.MAT_QTY,PO_RCD_MAT.MAT_CHALAN_QTY,PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_REJ_QTY,PO_RCD_MAT.MAT_EXCE,PO_RCD_MAT.MAT_BAL_QTY,PO_RCD_MAT.GARN_NOTE,PO_RCD_MAT.CRR_DATE,PO_RCD_MAT.AMD_NO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN Outsource_F_ITEM ON Outsource_F_ITEM.ITEM_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .GARN_NO='" & M_garn_crrnoDropDownList.SelectedValue & "' and PO_RCD_MAT.GARN_NO <> 'Pending'", conn)
        Else
            da = New SqlDataAdapter("select PO_RCD_MAT.TOTAL_MT, PO_RCD_MAT.TRANS_CHARGE, PO_RCD_MAT.TRANS_SHORT,PO_RCD_MAT.PROV_VALUE,PO_RCD_MAT.PO_NO,PO_RCD_MAT.CRR_NO,PO_RCD_MAT.CHLN_NO,PO_RCD_MAT.MAT_SLNO,PO_RCD_MAT.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,PO_ORD_MAT.MAT_QTY,PO_RCD_MAT.MAT_CHALAN_QTY,PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_REJ_QTY,PO_RCD_MAT.MAT_EXCE,PO_RCD_MAT.MAT_BAL_QTY,PO_RCD_MAT.GARN_NOTE,PO_RCD_MAT.CRR_DATE,PO_RCD_MAT.AMD_NO from po_rcd_mat join po_ord_mat on PO_RCD_MAT .po_no=PO_ORD_MAT .po_no and PO_RCD_MAT .mat_slno=PO_ORD_MAT .mat_slno and PO_ORD_MAT.amd_no=PO_RCD_MAT.amd_no JOIN MATERIAL ON MATERIAL.MAT_CODE =PO_RCD_MAT .MAT_CODE where PO_RCD_MAT .GARN_NO='" & M_garn_crrnoDropDownList.SelectedValue & "' and PO_RCD_MAT.GARN_NO <> 'Pending'", conn)
        End If


        da.Fill(dt)
        conn.Close()
        GridView210.DataSource = dt
        GridView210.DataBind()
        conn.Close()
        conn.Open()
        Dim supl_tax As String = ""

        Dim mc2 As New SqlCommand
        mc2.CommandText = "SELECT SUPL .SUPL_PAN,SUPL .SUPL_NAME,SUPL.SUPL_ID,supl.supl_tax ,PO_RCD_MAT .TRANS_SLNO, PO_RCD_MAT .PO_NO,PO_RCD_MAT.TRANS_WO_NO  FROM PO_RCD_MAT JOIN SUPL ON PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID WHERE PO_RCD_MAT.GARN_NO ='" & M_garn_crrnoDropDownList.SelectedValue & "'"
        mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            M_Label398.Text = dr.Item("po_no")
            M_Label396.Text = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
            supl_tax = dr.Item("SUPL_TAX")
            lblPANNo.Text = dr.Item("SUPL_PAN")
            M_Label424.Text = dr.Item("TRANS_WO_NO")
            M_Label462.Text = dr.Item("TRANS_SLNO")
            dr.Close()
        End If
        conn.Close()


        Dim i As Integer
        For i = 0 To GridView210.Rows.Count - 1
            GridView210.Rows(i).Cells(12).Text = CDec(GridView210.Rows(i).Cells(10).Text) - (CDec(GridView210.Rows(i).Cells(11).Text) + CDec(GridView210.Rows(i).Cells(13).Text))
            conn.Open()
            Dim QTY, RQTY As Decimal
            mc2.CommandText = "select SUM(MAT_QTY) AS MAT_QTY , SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD FROM PO_ORD_MAT WHERE mat_slno=" & GridView210.Rows(i).Cells(3).Text & " and po_no='" & GridView210.Rows(i).Cells(1).Text & "'"
            mc2.Connection = conn
            dr = mc2.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                QTY = dr.Item("MAT_QTY")
                RQTY = dr.Item("MAT_QTY_RCVD")
                dr.Close()
            End If
            conn.Close()
            '' GridView210.Rows(i).Cells(28).Text = RQTY + (RQTY + CDec(GridView210.Rows(i).Cells(12).Text))
        Next
    End Sub

    Protected Sub M_DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles M_DropDownList6.SelectedIndexChanged
        If M_DropDownList6.SelectedValue = "Select" Then
            M_DropDownList6.Focus()
            Return
        End If
        ''LD CALCULATE
        conn.Open()
        Dim mcL As New SqlCommand
        Dim LD_STRING As String = ""
        mcL.CommandText = "select * from ORDER_DETAILS where so_no= '" & M_Label398.Text & "'"
        mcL.Connection = conn
        dr = mcL.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            LD_STRING = dr.Item("LD")
            Label93.Text = dr.Item("ORDER_TO")
            dr.Close()
        End If
        conn.Close()
        If LD_STRING = "Applicable" Then
            conn.Open()
            Dim mc1 As New SqlCommand
            Dim delv_date As Date
            mc1.CommandText = "select MAX(MAT_DELIVERY) as delv_date from PO_ORD_MAT where po_no='" & M_Label398.Text & "' and mat_slno=" & M_DropDownList6.SelectedValue
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
            mc1.CommandText = "select CRR_DATE  from PO_RCD_MAT where po_no='" & M_Label398.Text & "' and mat_slno=" & M_DropDownList6.SelectedValue
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
                M_Label419.Visible = True
                M_Label419.Text = day_check & " Days Late"
                LD_COUNT = day_check / 30
                If LD_COUNT < 1 Then
                    M_TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 1 And LD_COUNT < 2 Then
                    M_TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 2 And LD_COUNT < 3 Then
                    M_TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 3 And LD_COUNT < 4 Then
                    M_TextBox142.Text = "0.00"
                ElseIf LD_COUNT > 4 Then
                    M_TextBox142.Text = "0.00"
                End If
            Else
                M_TextBox142.Text = "0.00"
                M_Label419.Visible = False
            End If
        Else
            M_TextBox142.Text = "0.00"
            M_Label419.Visible = False
        End If
        ''AS PER ORDER DATA SEARCH
        conn.Open()
        Dim freight_term, freight_type As String
        freight_term = ""
        freight_type = ""
        Dim mc As New SqlCommand
        mc.CommandText = "select FREIGHT_TERM FROM ORDER_DETAILS where so_no='" & M_Label398.Text & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            freight_term = dr.Item("FREIGHT_TERM")
            dr.Close()
        Else
            dr.Close()
            conn.Close()
        End If
        conn.Close()
        If freight_term = "Extra As Per Mt." Then
            M_TextBox157.BackColor = Drawing.Color.White
            M_TextBox157.ReadOnly = False
            Label414.Text = "Freight"
            Label415.Text = "Freight"
        ElseIf freight_term = "Extra As %" Then
            M_TextBox157.BackColor = Drawing.Color.White
            M_TextBox157.ReadOnly = False
            Label414.Text = "Freight"
            Label415.Text = "Freight"
        ElseIf freight_term = "Paid" Or freight_term = "Not Applicable" Then
            M_TextBox157.BackColor = Drawing.Color.Aqua
            M_TextBox157.ReadOnly = True
            Label414.Text = "Freight"
            Label415.Text = "Freight"
        End If
        ''SELECT SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TYPE) AS FREIGHT_TYPE,MAX(DISC_TYPE) AS DISC_TYPE, MAX(PF_TYPE) AS PF_TYPE, MAX(MAT_CODE) as mat_code,MAX(MAT_NAME) AS MAT_NAME,MAX(AMD_NO) as AMD_NO,MAX(MAT_EXCISE) as MAT_EXCISE,SUM(MAT_QTY) AS MAT_QTY, SUM(MAT_UNIT_RATE ) AS MAT_UNIT_RATE,SUM(MAT_PACK) AS MAT_PACK,SUM(MAT_DISCOUNT) AS MAT_DISCOUNT,SUM(MAT_EXCISE_DUTY) AS MAT_EXCISE_DUTY,SUM(MAT_CST) AS MAT_CST,SUM(MAT_ENTRY_TAX) AS MAT_ENTRY_TAX,SUM(MAT_OTHER_TAX) AS MAT_OTHER_TAX,SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD,SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TERM) AS FREIGHT_TYPE FROM PO_ORD_MAT  JOIN ORDER_DETAILS ON PO_ORD_MAT .PO_NO=ORDER_DETAILS .SO_NO WHERE PO_ORD_MAT.PO_NO='" & Label398.Text & "' AND PO_ORD_MAT.MAT_SLNO=" & DropDownList6.SelectedValue & " and PO_ORD_MAT.AMD_DATE < =(SELECT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & garn_crrnoDropDownList.SelectedValue & "' AND MAT_SLNO=" & DropDownList6.SelectedValue & ")"
        conn.Open()
        mc.CommandText = "SELECT SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TYPE) AS FREIGHT_TYPE,MAX(DISC_TYPE) AS DISC_TYPE, MAX(PF_TYPE) AS PF_TYPE, MAX(MAT_CODE) as mat_code,MAX(MAT_NAME) AS MAT_NAME,MAX(AMD_NO) as AMD_NO,SUM(MAT_QTY) AS MAT_QTY, SUM(MAT_UNIT_RATE ) AS MAT_UNIT_RATE,SUM(MAT_PACK) AS MAT_PACK,SUM(MAT_DISCOUNT) AS MAT_DISCOUNT,SUM(CGST) AS CGST,SUM(SGST) AS SGST,SUM(IGST) AS IGST,SUM(CESS) AS CESS,SUM(ANAL_TAX ) AS ANAL_TAX,SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD,SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TERM) AS FREIGHT_TYPE,
                max(TDS_SGST) as TDS_SGST,max(TDS_CGST) as TDS_CGST,max(TDS_IGST) as TDS_IGST FROM PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT .PO_NO=ORDER_DETAILS .SO_NO WHERE PO_ORD_MAT.PO_NO='" & M_Label398.Text & "' AND PO_ORD_MAT.MAT_SLNO='" & M_DropDownList6.SelectedValue & "' and PO_ORD_MAT.AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE GARN_NO='" & M_garn_crrnoDropDownList.Text & "' AND MAT_SLNO='" & M_DropDownList6.Text & "')"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            M_TextBox120.Text = dr.Item("MAT_UNIT_RATE")
            M_TextBox124.Text = dr.Item("MAT_DISCOUNT")
            M_TextBox121.Text = dr.Item("MAT_PACK")
            TextBox29.Text = dr.Item("CGST")
            TextBox11.Text = dr.Item("SGST")
            TextBox31.Text = dr.Item("IGST")
            TextBox33.Text = dr.Item("CESS")
            TextBox35.Text = dr.Item("ANAL_TAX")
            M_TextBox125.Text = dr.Item("MAT_FREIGHT_PU")
            M_Label392.Text = dr.Item("mat_code")
            M_Label394.Text = dr.Item("MAT_NAME")
            M_Label422.Text = dr.Item("amd_no")
            M_Label468.Text = dr.Item("DISC_TYPE")
            M_Label467.Text = dr.Item("PF_TYPE")
            M_Label466.Text = dr.Item("FREIGHT_TYPE")
            TextBox54.Text = dr.Item("TDS_SGST")
            TextBox55.Text = dr.Item("TDS_CGST")
            TextBox56.Text = dr.Item("TDS_IGST")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        'Unit price
        M_TextBox147.Text = M_TextBox120.Text
        'discount
        M_TextBox149.Text = M_TextBox124.Text
        'packing
        M_TextBox151.Text = M_TextBox121.Text
        ''freight
        M_TextBox157.Text = M_TextBox125.Text
        'ld
        M_TextBox143.Text = M_TextBox142.Text
        ''sgst
        TextBox12.Text = TextBox11.Text
        'cgst
        TextBox30.Text = TextBox29.Text
        'igst
        TextBox32.Text = TextBox31.Text
        'cess
        TextBox34.Text = TextBox33.Text
        'anal
        TextBox36.Text = TextBox35.Text
    End Sub

    Protected Sub M_Button44_Click(sender As Object, e As EventArgs) Handles M_Button44.Click
        Dim mc1 As New SqlCommand
        Dim SUPL_TYPE As New String("")
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                conn.Open()
                mc1.CommandText = "select * from supl where supl_id='" & M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1).Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    SUPL_TYPE = dr.Item("SUPL_TYPE")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

                'Database updation entry
                If M_garn_crrnoDropDownList.SelectedValue = "Select" Then
                    M_garn_crrnoDropDownList.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Select GARN No"
                    Return
                ElseIf M_DropDownList6.SelectedValue = "Select" Then
                    M_DropDownList6.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Select Material SlNo"
                    Return
                ElseIf DropDownList44.SelectedValue = "Select" Then
                    DropDownList44.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Select RCM Type"
                    Return
                ElseIf M_TextBox143.Text = "" Or IsNumeric(M_TextBox143.Text) = False Then
                    M_TextBox143.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Enter L.D. Percentege"
                    Return
                ElseIf M_TextBox147.Text = "" Or IsNumeric(M_TextBox147.Text) = False Or M_TextBox147.Text < 0 Then
                    M_TextBox147.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Enter Unit Price"
                    Return
                ElseIf M_TextBox149.Text = "" Or IsNumeric(M_TextBox149.Text) = False Then
                    M_TextBox149.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Enter Discount Percentege"
                    Return
                ElseIf M_TextBox151.Text = "" Or IsNumeric(M_TextBox151.Text) = False Then
                    M_TextBox151.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Enter Packing Percentege"
                    Return

                ElseIf M_TextBox157.Text = "" Or IsNumeric(M_TextBox157.Text) = False Then
                    M_TextBox157.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Enter Freight"
                    Return

                ElseIf M_TextBox160.Text = "" Or IsNumeric(M_TextBox160.Text) = False Then
                    M_TextBox160.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Enter Local Freight"
                    Return

                ElseIf M_TextBox162.Text = "" Or IsNumeric(M_TextBox162.Text) = False Then
                    M_TextBox162.Focus()
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "Please Enter Penality"
                    Return
                ElseIf (CDec(TextBox64.Text) > 0 And CDec(txtTDS.Text) > 0) Then
                    M_GARN_ERR_LABLE.Visible = True
                    M_GARN_ERR_LABLE.Text = "TDS cannot be filled on both the textboxes."
                    Return
                    'ElseIf ((CDec(TextBox12.Text) + CDec(TextBox30.Text) + CDec(TextBox32.Text)) <= 0) Then
                    '    M_GARN_ERR_LABLE.Visible = True
                    '    M_GARN_ERR_LABLE.Text = "GST percentage cannot be 0."
                    '    Return
                Else
                    M_GARN_ERR_LABLE.Visible = False
                End If
                ''delete garn_price

                mycommand = New SqlCommand("DELETE FROM GARN_PRICE WHERE CRR_NO ='" & M_garn_crrnoDropDownList.SelectedValue & "' and po_no ='" & M_Label398.Text & "' and MAT_SLNO=" & M_DropDownList6.SelectedValue, conn_trans, myTrans)
                mycommand.ExecuteNonQuery()

                ''save garn_price
                Dim status As String
                If M_CheckBox1.Checked = True Then
                    status = "1"
                Else
                    status = "0"
                End If

                Dim query As String = "Insert Into GARN_PRICE(CRR_NO,PO_NO,MAT_SLNO,UNIT_PRICE,DISCOUNT,PACKING,FREIGHT,SGST,CGST,IGST,CESS,ANALITICAL_CHARGE,LD_CHARGE,LOCAL_FREIGHT,PENALITY_TYPE,PENALITY,REMARKS_PENALITY,REMARKS_MODIFY,LAST_MODIFY_BY ) values (@CRR_NO,@PO_NO,@MAT_SLNO,@UNIT_PRICE,@DISCOUNT,@PACKING,@FREIGHT,@SGST,@CGST,@IGST,@CESS,@ANALITICAL_CHARGE,@LD_CHARGE,@LOCAL_FREIGHT,@PENALITY_TYPE,@PENALITY,@REMARKS_PENALITY,@REMARKS_MODIFY,@LAST_MODIFY_BY)"
                Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@CRR_NO", M_garn_crrnoDropDownList.SelectedValue)
                cmd.Parameters.AddWithValue("@PO_NO", M_Label398.Text)
                cmd.Parameters.AddWithValue("@MAT_SLNO", M_DropDownList6.SelectedValue)
                cmd.Parameters.AddWithValue("@UNIT_PRICE", M_TextBox147.Text)
                cmd.Parameters.AddWithValue("@DISCOUNT", M_TextBox149.Text)
                cmd.Parameters.AddWithValue("@PACKING", M_TextBox151.Text)
                cmd.Parameters.AddWithValue("@SGST", TextBox12.Text)
                cmd.Parameters.AddWithValue("@CGST", TextBox30.Text)
                cmd.Parameters.AddWithValue("@IGST", TextBox32.Text)
                cmd.Parameters.AddWithValue("@CESS", TextBox34.Text)
                cmd.Parameters.AddWithValue("@FREIGHT", M_TextBox157.Text)
                cmd.Parameters.AddWithValue("@LD_CHARGE", M_TextBox143.Text)
                cmd.Parameters.AddWithValue("@LOCAL_FREIGHT", M_TextBox160.Text)
                cmd.Parameters.AddWithValue("@PENALITY_TYPE", status)
                cmd.Parameters.AddWithValue("@PENALITY", M_TextBox162.Text)
                cmd.Parameters.AddWithValue("@ANALITICAL_CHARGE", TextBox36.Text)
                cmd.Parameters.AddWithValue("@REMARKS_PENALITY", M_TextBox144.Text)
                cmd.Parameters.AddWithValue("@REMARKS_MODIFY", M_TextBox145.Text)
                cmd.Parameters.AddWithValue("@LAST_MODIFY_BY", Session("userName"))
                cmd.ExecuteReader()
                cmd.Dispose()

                ''put value
                Dim basic_price, disc_price, TOLE_VALUE, TRANS_DIFF, w_tolerance, packing_price, short_trans_amt, cess_price, sgst_price, cgst_price, igst_price, ld_price, freight_price, penality_price, analitical_price, WT_VAR, LOSS_GST, TRANS_VALUE_TOTAL, TRANS_SHORT_VALUE, TDS_AMOUNT As New Decimal(0)
                Dim unit_price, freight, ld_charge, packing_charge, local_freight, LOCAL_FREIGHT_PRICE, discount, net_price, analitical_charge, ed_charge, penality, sgst, cgst, igst, cess, sd, TDS_SGST, TDS_CGST, TDS_IGST, OTHER_DEDUCTION, tds_percentage As New Decimal(0)
                unit_price = M_TextBox147.Text
                discount = M_TextBox149.Text
                packing_charge = M_TextBox151.Text
                freight = M_TextBox157.Text
                ld_charge = M_TextBox143.Text
                local_freight = M_TextBox160.Text
                analitical_charge = TextBox36.Text
                penality = M_TextBox162.Text
                sgst = TextBox12.Text
                cgst = TextBox30.Text
                igst = TextBox32.Text
                cess = TextBox34.Text
                sd = TextBox3.Text
                OTHER_DEDUCTION = TextBox58.Text
                ''grid view search
                Dim I As Integer
                For I = 0 To GridView210.Rows.Count - 1
                    If GridView210.Rows(I).Cells(3).Text = M_DropDownList6.SelectedValue Then
                        'CALCULATE MATERIAL VALUE
                        Dim TCS_AMOUNT, ACCEPT_QTY, excess_qty, CHLN_QTY, RCVD_QTY, REJ_QTY, PARTY_QTY, TRANS_QTY, TOTAL_MT, PARTY_AMOUNT, TRANSPORT_ADJUST, trans_base_value As New Decimal(0.0)
                        ACCEPT_QTY = CDec(GridView210.Rows(I).Cells(12).Text)
                        CHLN_QTY = CDec(GridView210.Rows(I).Cells(9).Text)
                        RCVD_QTY = CDec(GridView210.Rows(I).Cells(10).Text)
                        REJ_QTY = CDec(GridView210.Rows(I).Cells(11).Text)
                        excess_qty = CDec(GridView210.Rows(I).Cells(13).Text)
                        TOTAL_MT = CDec(GridView210.Rows(I).Cells(34).Text)


                        '''''''''''''''''''''''''''''''''''''''
                        Dim MC As New SqlCommand
                        conn.Open()
                        MC.CommandText = "select tolerance from ORDER_DETAILS where SO_NO= '" & M_Label398.Text & "'"
                        MC.Connection = conn
                        dr = MC.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            If IsDBNull(dr.Item("tolerance")) Then
                                w_tolerance = 0.00
                            Else
                                w_tolerance = dr.Item("tolerance")
                            End If

                            dr.Close()
                        Else
                            conn.Close()
                        End If
                        conn.Close()

                        'w_tolerance = 0.5
                        ''TOLERANCE
                        TOLE_VALUE = (CDec(GridView210.Rows(I).Cells(9).Text) * w_tolerance) / 100
                        ''TRANSPORTATION DIFFRENCE
                        TRANS_DIFF = CDec(GridView210.Rows(I).Cells(9).Text) - CDec(GridView210.Rows(I).Cells(10).Text)

                        '''''''''''''''''''''''''''''''''''''''
                        If M_Label424.Text = "N/A" Then
                            If TOLE_VALUE >= TRANS_DIFF Then
                                PARTY_QTY = CHLN_QTY - (REJ_QTY + excess_qty)
                            Else
                                PARTY_QTY = ACCEPT_QTY
                            End If
                            'PARTY_QTY = ACCEPT_QTY
                        ElseIf M_Label424.Text <> "N/A" Then
                            PARTY_QTY = CHLN_QTY - (REJ_QTY + excess_qty)
                            TRANS_QTY = TOTAL_MT
                        End If
                        ''BASIC PRICE
                        basic_price = FormatNumber(unit_price * PARTY_QTY, 2)
                        ''DISCOUNT
                        If M_Label468.Text = "PERCENTAGE" Then
                            disc_price = FormatNumber((basic_price * discount) / 100, 2)
                        ElseIf M_Label468.Text = "PER UNIT" Then
                            disc_price = FormatNumber(PARTY_QTY * discount, 2)
                        ElseIf M_Label468.Text = "PER MT" Then
                            disc_price = FormatNumber(TOTAL_MT * discount, 2)
                        End If

                        ''PACKING AND FORWD
                        If M_Label467.Text = "PERCENTAGE" Then
                            packing_price = FormatNumber(((basic_price - disc_price) * packing_charge) / 100, 2)
                        ElseIf M_Label467.Text = "PER UNIT" Then
                            packing_price = FormatNumber(PARTY_QTY * packing_charge, 2)
                        End If
                        ''NET PRICE
                        net_price = (basic_price - disc_price) + packing_price
                        ''freight calculate
                        If M_Label466.Text = "PERCENTAGE" Then
                            freight_price = FormatNumber((basic_price * freight) / 100, 2)
                        ElseIf M_Label466.Text = "PER UNIT" Then
                            freight_price = FormatNumber(PARTY_QTY * freight, 2)
                        End If

                        ''penality calculate
                        If M_CheckBox1.Checked = True Then
                            penality_price = FormatNumber((basic_price * penality) / 100, 2)
                        ElseIf M_CheckBox1.Checked = False Then
                            penality_price = FormatNumber(penality * PARTY_QTY, 2)
                        End If
                        ''ld calculate
                        ld_price = ld_charge

                        'If (DropDownList5.SelectedValue = "Yes") Then
                        '    cgst_price = CInt((((net_price + freight_price)) * cgst) / 100)
                        '    sgst_price = CInt((((net_price + freight_price)) * sgst) / 100)
                        '    igst_price = CInt((((net_price + freight_price)) * igst) / 100)
                        '    cess_price = CInt((((net_price + freight_price)) * cess) / 100)
                        'Else
                        '    cgst_price = 0
                        '    sgst_price = 0
                        '    igst_price = 0
                        '    cess_price = 0
                        'End If

                        cgst_price = CInt((((net_price + freight_price)) * cgst) / 100)
                        sgst_price = CInt((((net_price + freight_price)) * sgst) / 100)
                        igst_price = CInt((((net_price + freight_price)) * igst) / 100)
                        cess_price = CInt((((net_price + freight_price)) * cess) / 100)



                        ''TDS ON GST
                        TDS_SGST = CInt(((net_price + freight_price) * CDec(TextBox54.Text)) / 100)
                        TDS_CGST = CInt(((net_price + freight_price) * CDec(TextBox55.Text)) / 100)
                        TDS_IGST = CInt(((net_price + freight_price) * CDec(TextBox56.Text)) / 100)

                        ''TDS FOR RAW MATERIAL & STORES
                        If (CDec(TextBox64.Text) > 0) Then
                            tds_percentage = CDec(TextBox64.Text)
                        Else
                            tds_percentage = CDec(txtTDS.Text)
                        End If
                        TDS_AMOUNT = CInt(((net_price + freight_price) * tds_percentage) / 100)


                        ''===============GST CALCULATION ON LD/PENALTY has been abolished from Oct 2022 but implemented on 22-03-2023=======================''


                        ''LOCAL FREIGHT
                        LOCAL_FREIGHT_PRICE = local_freight
                        ''ANALITICAL PRICE
                        analitical_price = analitical_charge * PARTY_QTY
                        If M_Label424.Text <> "N/A" Then
                            'Dim MC As New SqlCommand
                            conn.Open()
                            MC.CommandText = "select sum(W_TOLERANCE) as W_TOLERANCE from WO_ORDER where PO_NO = '" & M_Label424.Text & "' AND W_SLNO='" & M_Label462.Text & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & GridView210.Rows(I).Cells(0).Text & "' AND MAT_SLNO='" & GridView210.Rows(I).Cells(3).Text & "')"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                w_tolerance = dr.Item("W_TOLERANCE")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()

                            ''TOLERANCE
                            TOLE_VALUE = (CDec(GridView210.Rows(I).Cells(9).Text) * w_tolerance) / 100
                            ''TRANSPORTATION DIFFRENCE
                            TRANS_DIFF = CDec(GridView210.Rows(I).Cells(9).Text) - CDec(GridView210.Rows(I).Cells(10).Text)
                            If TOLE_VALUE > TRANS_DIFF Then
                                ''LOSS ON TRANSPORT
                                WT_VAR = FormatNumber(((net_price + freight_price + LOCAL_FREIGHT_PRICE + analitical_price) / PARTY_QTY) * TRANS_DIFF, 2)
                                ''LOSS ON ED
                                LOSS_GST = FormatNumber(((cgst_price + sgst_price + igst_price + cess_price) / PARTY_QTY) * TRANS_DIFF, 2)
                                ''SHORTAGE VALUE
                                short_trans_amt = 0
                            Else
                                ''LOSS ON TRANSPORT
                                WT_VAR = FormatNumber(((net_price + freight_price + LOCAL_FREIGHT_PRICE + analitical_price) / PARTY_QTY) * TOLE_VALUE, 2)
                                ''LOSS ON ED
                                LOSS_GST = FormatNumber(((cgst_price + sgst_price + igst_price + cess_price) / PARTY_QTY) * TOLE_VALUE, 2)
                                ''SHORTAGE VALUE
                                short_trans_amt = FormatNumber((((net_price + freight_price + LOCAL_FREIGHT_PRICE + cgst_price + sgst_price + igst_price + cess_price + analitical_price) / PARTY_QTY) * TRANS_DIFF) - (LOSS_GST + WT_VAR), 2)
                                trans_base_value = FormatNumber((((net_price + freight_price + LOCAL_FREIGHT_PRICE + analitical_price) / PARTY_QTY) * TRANS_DIFF) - (WT_VAR), 2)
                            End If


                        End If
                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        ''unit price
                        GridView210.Rows(I).Cells(15).Text = CDec(M_TextBox147.Text)
                        ''discount
                        GridView210.Rows(I).Cells(16).Text = disc_price
                        ''pack & ford
                        GridView210.Rows(I).Cells(17).Text = packing_price
                        ''SGST
                        GridView210.Rows(I).Cells(18).Text = sgst_price
                        ''CGST
                        GridView210.Rows(I).Cells(19).Text = cgst_price
                        ''IGST
                        GridView210.Rows(I).Cells(20).Text = igst_price
                        ''CESS
                        GridView210.Rows(I).Cells(21).Text = cess_price
                        ''freight
                        GridView210.Rows(I).Cells(22).Text = freight_price
                        ''local freight
                        GridView210.Rows(I).Cells(23).Text = LOCAL_FREIGHT_PRICE
                        ''penality
                        GridView210.Rows(I).Cells(24).Text = penality_price
                        ''late delivery
                        GridView210.Rows(I).Cells(25).Text = ld_price

                        ''TCS Amount
                        GridView210.Rows(I).Cells(44).Text = FormatNumber((net_price + freight_price + sgst_price + cgst_price + igst_price) * CDec(TextBox62.Text) / 100, 2)
                        TCS_AMOUNT = CDec(GridView210.Rows(I).Cells(44).Text)

                        ''MATERIAL VALUE
                        If DropDownList44.SelectedValue = "Yes" Then
                            GridView210.Rows(I).Cells(26).Text = FormatNumber(CDec(net_price + freight_price + analitical_price + TCS_AMOUNT) - (penality_price + ld_price + sd + TDS_SGST + TDS_CGST + TDS_IGST + OTHER_DEDUCTION + TDS_AMOUNT), 2)
                        ElseIf DropDownList44.SelectedValue = "No" Then

                            If DropDownList5.SelectedValue = "Yes" Then
                                GridView210.Rows(I).Cells(26).Text = FormatNumber(CDec(net_price + cgst_price + sgst_price + igst_price + cess_price + freight_price + analitical_price + TCS_AMOUNT - (penality_price + ld_price + sd + TDS_SGST + TDS_CGST + TDS_IGST + OTHER_DEDUCTION + TDS_AMOUNT)), 2)
                            ElseIf DropDownList5.SelectedValue = "No" Then
                                GridView210.Rows(I).Cells(26).Text = FormatNumber(CDec(net_price + freight_price + analitical_price + TCS_AMOUNT) - (penality_price + ld_price + sd + TDS_SGST + TDS_CGST + TDS_IGST + OTHER_DEDUCTION + TDS_AMOUNT), 2)
                            End If

                        End If

                        GridView210.Rows(I).Cells(29).Text = WT_VAR
                        GridView210.Rows(I).Cells(28).Text = LOSS_GST
                        ''RCM CESS
                        GridView210.Rows(I).Cells(39).Text = sd

                        'DIFF CALCULATION
                        'GridView210.Rows(I).Cells(31).Text = FormatNumber(CDec(net_price + freight_price + analitical_price - (penality_price) - CDec(GridView210.Rows(I).Cells(30).Text) - CDec(GridView210.Rows(I).Cells(27).Text)), 2)
                        GridView210.Rows(I).Cells(31).Text = FormatNumber(CDec(net_price + freight_price + analitical_price - penality_price - CDec(GridView210.Rows(I).Cells(30).Text)), 2)
                        'RCM SGST
                        GridView210.Rows(I).Cells(35).Text = "0.00"
                        ''RCM CGST
                        GridView210.Rows(I).Cells(36).Text = "0.00"
                        ''RCM IGST
                        GridView210.Rows(I).Cells(37).Text = "0.00"
                        ''RCM CESS
                        GridView210.Rows(I).Cells(38).Text = "0.00"

                        ''TDS SGST
                        GridView210.Rows(I).Cells(40).Text = TDS_SGST
                        ''TDS CGST
                        GridView210.Rows(I).Cells(41).Text = TDS_CGST
                        ''TDS IGST
                        GridView210.Rows(I).Cells(42).Text = TDS_IGST
                        ''OTHER DEDUCTION
                        GridView210.Rows(I).Cells(43).Text = TextBox58.Text

                        ''Taxable Value
                        GridView210.Rows(I).Cells(45).Text = net_price + freight_price
                        ''SGST PERCENTAGE
                        GridView210.Rows(I).Cells(46).Text = TextBox12.Text
                        ''CGST PERCENTAGE
                        GridView210.Rows(I).Cells(47).Text = TextBox30.Text
                        ''IGST PERCENTAGE
                        GridView210.Rows(I).Cells(48).Text = TextBox32.Text
                        ''CESS PERCENTAGE
                        GridView210.Rows(I).Cells(49).Text = TextBox34.Text

                        ''TDS AMOUNT
                        GridView210.Rows(I).Cells(50).Text = TDS_AMOUNT


                    End If

                Next

                myTrans.Commit()

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                M_GARN_ERR_LABLE.Visible = True
                M_GARN_ERR_LABLE.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub M_Button3_Click(sender As Object, e As EventArgs) Handles M_Button3.Click

        Panel30.Visible = True
        Panel38.Visible = False
    End Sub

    Protected Sub M_Button5_Click(sender As Object, e As EventArgs) Handles M_Button5.Click
        ''check
        If M_garn_crrnoDropDownList.SelectedValue = "Select" Then
            M_garn_crrnoDropDownList.Focus()
            Return
        ElseIf M_DropDownList6.SelectedValue = "Select" Then
            M_DropDownList6.Focus()
            Return
        ElseIf GridView210.Rows.Count = 0 Then
            M_garn_crrnoDropDownList.Focus()
            Return
        ElseIf DropDownList44.SelectedValue = "Select" Then
            DropDownList44.Focus()
            Return
        End If
        Panel38.Visible = True
    End Sub

    Protected Sub Button58_Click(sender As Object, e As EventArgs) Handles Button58.Click
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
                If TextBox173.Text = "" Then
                    TextBox173.Focus()
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

                '''''''''''''''''''''''''''''''''
                ''Checking Valuation date and Freeze date
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
                    Label621.Visible = True
                    Label621.Text = "Valuation before " & Block_DATE & " has been freezed."
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
                    End If
                    conn.Close()

                    If password = TextBox173.Text Then

                        Dim TDS_HEAD, TCS_HEAD, RCM_SGST_HEAD, RCM_CGST_HEAD, RCM_IGST_HEAD, RCM_CESS_HEAD, prov_head, sund_head, ld_head, short_head, diff_head, cgst_head, sgst_head, igst_head, cess_head, penality_head, sd_head, OTHER_DEDUCTION_HEAD, IUCA_HEAD As New String("")
                        Dim SGST_LIAB_LD_PENALTY, CGST_LIAB_LD_PENALTY, IGST_LIAB_LD_PENALTY, CESS_LIAB_LD_PENALTY, TDS_SGST_HEAD, TDS_CGST_HEAD, TDS_IGST_HEAD As New String("")

                        conn.Open()
                        Dim MC6 As New SqlCommand

                        MC6.CommandText = "select IUCA_HEAD from SUPL where SUPL_ID='" & TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1) & "'"
                        MC6.Connection = conn
                        dr = MC6.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            If IsDBNull(dr.Item("IUCA_HEAD")) Then
                                IUCA_HEAD = ""
                            Else
                                IUCA_HEAD = dr.Item("IUCA_HEAD")
                            End If
                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If



                        conn.Open()
                        'MC6.CommandText = "select * from work_group where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & M_Label398.Text & "')"
                        MC6.CommandText = "select * from work_group where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & M_Label398.Text & "')  AND work_name=(SELECT ORDER_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & M_Label398.Text & "')"
                        MC6.Connection = conn
                        dr = MC6.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            prov_head = dr.Item("prov_head")
                            sund_head = dr.Item("sund_head")
                            ld_head = dr.Item("ld_head")
                            short_head = dr.Item("loss_on_tran_head")
                            penality_head = dr.Item("loss_on_tran_head")
                            diff_head = dr.Item("psc_head")

                            sgst_head = dr.Item("sgst")
                            cgst_head = dr.Item("cgst")
                            igst_head = dr.Item("igst")
                            cess_head = dr.Item("cess")

                            RCM_SGST_HEAD = dr.Item("lsgst")
                            RCM_CGST_HEAD = dr.Item("lcgst")
                            RCM_IGST_HEAD = dr.Item("ligst")
                            RCM_CESS_HEAD = dr.Item("lcess")

                            TCS_HEAD = dr.Item("TCS_INCOMING")
                            TDS_HEAD = dr.Item("tds_head")

                            sd_head = dr.Item("sd_head")
                            SGST_LIAB_LD_PENALTY = dr.Item("sgst_liab_ld_pen")
                            CGST_LIAB_LD_PENALTY = dr.Item("cgst_liab_ld_pen")
                            IGST_LIAB_LD_PENALTY = dr.Item("igst_liab_ld_pen")
                            CESS_LIAB_LD_PENALTY = dr.Item("cess_liab_ld_pen")

                            TDS_SGST_HEAD = dr.Item("TDS_SGST")
                            TDS_CGST_HEAD = dr.Item("TDS_CGST")
                            TDS_IGST_HEAD = dr.Item("TDS_IGST")

                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If


                        ''UPDATE PO_RCD_MAT
                        Dim TAXABLE_AMOUNT, TDS_AMOUNT, TCS_AMOUNT, TDS_SGST, TDS_CGST, TDS_IGST, sgst_LD_Penalty, cgst_LD_Penalty, igst_LD_Penalty, cess_LD_Penalty, prov_price, tole_price, sund_price, ld_price, penality_price, sgst_price, cgst_price, igst_price, cess_price, short_price, diff_price, sd, other_deduction As New Decimal(0)

                        ''VALUE SEARCH
                        Dim I As Integer = 0
                        For I = 0 To GridView210.Rows.Count - 1
                            If GridView210.Rows(I).Cells(16).Text <> "&nbsp;" Then

                                Dim query1 As String = "update PO_RCD_MAT set TAXABLE_VALUE=@TAXABLE_VALUE,BillTrackID=@BillTrackID,GST_STATUS=@GST_STATUS,Valuation_Date=@Valuation_Date,TDS_AMOUNT=@TDS_AMOUNT,TCS_AMOUNT=@TCS_AMOUNT,other_deduction=@other_deduction,RCM=@RCM, RCM_SGST=@RCM_SGST, RCM_CGST=@RCM_CGST,RCM_IGST=@RCM_IGST, RCM_CESS=@RCM_CESS,CESS=@CESS, IGST=@IGST, SGST=@SGST, CGST=@CGST, UNIT_RATE=@UNIT_RATE, MAT_RATE=@MAT_RATE,DISC_VAL=@DISC_VAL,PF_VALUE=@PF_VALUE,LD_CHARGE=@LD_CHARGE,PENALITY_CHARGE=@PENALITY_CHARGE,FREIGHT_CHARGE=@FREIGHT_CHARGE,LOCAL_FREIGHT=@LOCAL_FREIGHT,LOSS_TRANSPORT=@LOSS_TRANSPORT,LOSS_ON_ED=@LOSS_ON_ED,DIFF_VALUE=@DIFF_VALUE,V_IND=@V_IND,INV_NO=@INV_NO,INV_DATE=@INV_DATE, PAY_EMP=@PAY_EMP where MAT_SLNO='" & GridView210.Rows(I).Cells(3).Text & "' and CRR_NO='" & GridView210.Rows(I).Cells(0).Text & "' AND PO_NO='" & M_Label398.Text & "'"
                                Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                                cmd1.Parameters.AddWithValue("@UNIT_RATE", CDec(GridView210.Rows(I).Cells(15).Text))
                                cmd1.Parameters.AddWithValue("@MAT_RATE", CDec(GridView210.Rows(I).Cells(30).Text))
                                cmd1.Parameters.AddWithValue("@DISC_VAL", CDec(GridView210.Rows(I).Cells(16).Text))
                                cmd1.Parameters.AddWithValue("@PF_VALUE", CDec(GridView210.Rows(I).Cells(17).Text))
                                cmd1.Parameters.AddWithValue("@CGST", CDec(GridView210.Rows(I).Cells(19).Text))
                                cmd1.Parameters.AddWithValue("@SGST", CDec(GridView210.Rows(I).Cells(18).Text))
                                cmd1.Parameters.AddWithValue("@IGST", CDec(GridView210.Rows(I).Cells(20).Text))
                                cmd1.Parameters.AddWithValue("@CESS", CDec(GridView210.Rows(I).Cells(21).Text))
                                cmd1.Parameters.AddWithValue("@RCM_SGST", CDec(GridView210.Rows(I).Cells(35).Text))
                                cmd1.Parameters.AddWithValue("@RCM_CGST", CDec(GridView210.Rows(I).Cells(36).Text))
                                cmd1.Parameters.AddWithValue("@RCM_IGST", CDec(GridView210.Rows(I).Cells(37).Text))
                                cmd1.Parameters.AddWithValue("@RCM_CESS", CDec(GridView210.Rows(I).Cells(38).Text))
                                cmd1.Parameters.AddWithValue("@LD_CHARGE", CDec(GridView210.Rows(I).Cells(25).Text))
                                cmd1.Parameters.AddWithValue("@PENALITY_CHARGE", CDec(GridView210.Rows(I).Cells(24).Text))
                                cmd1.Parameters.AddWithValue("@FREIGHT_CHARGE", CDec(GridView210.Rows(I).Cells(22).Text))
                                cmd1.Parameters.AddWithValue("@LOCAL_FREIGHT", CDec(GridView210.Rows(I).Cells(23).Text))
                                cmd1.Parameters.AddWithValue("@LOSS_TRANSPORT", CDec(GridView210.Rows(I).Cells(29).Text))
                                cmd1.Parameters.AddWithValue("@LOSS_ON_ED", CDec(GridView210.Rows(I).Cells(28).Text))
                                cmd1.Parameters.AddWithValue("@DIFF_VALUE", CDec(GridView210.Rows(I).Cells(31).Text))
                                cmd1.Parameters.AddWithValue("@V_IND", "V")
                                cmd1.Parameters.AddWithValue("@Valuation_Date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                cmd1.Parameters.AddWithValue("@INV_NO", TextBox160.Text)
                                cmd1.Parameters.AddWithValue("@RCM", DropDownList44.SelectedValue)
                                cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(TextBox161.Text, "dd-MM-yyyy", provider))
                                cmd1.Parameters.AddWithValue("@PAY_EMP", Session("userName"))
                                cmd1.Parameters.AddWithValue("@other_deduction", CDec(GridView210.Rows(I).Cells(43).Text))
                                cmd1.Parameters.AddWithValue("@TCS_AMOUNT", CDec(GridView210.Rows(I).Cells(44).Text))
                                cmd1.Parameters.AddWithValue("@TDS_AMOUNT", CDec(GridView210.Rows(I).Cells(50).Text))
                                If (DropDownList5.SelectedValue = "Yes") Then
                                    cmd1.Parameters.AddWithValue("@GST_STATUS", "PAID")
                                Else
                                    cmd1.Parameters.AddWithValue("@GST_STATUS", "PENDING")
                                End If
                                cmd1.Parameters.AddWithValue("@BillTrackID", DropDownList40.Text)
                                cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(GridView210.Rows(I).Cells(45).Text))
                                cmd1.ExecuteReader()
                                cmd1.Dispose()

                                If ((CDec(GridView210.Rows(I).Cells(46).Text) + CDec(GridView210.Rows(I).Cells(47).Text) + CDec(GridView210.Rows(I).Cells(48).Text)) > 0) Then

                                    ''INSERT DATA INTO TAXABLE VALUES TABLE
                                    query1 = "INSERT INTO Taxable_Values (GST_STATUS,GST_PAYMENT_DATE,GST_PAYMENT_VOUCHER_NO,INVOICE_NO,INVOICE_DATE,ENTRY_DATE,RCM_CGST_AMT,RCM_SGST_AMT,RCM_IGST_AMT,RCM_CESS_AMT,GARN_CRR_MB_NO,VALUATION_DATE,DATA_TYPE,SL_NO,SUPL_CODE,SUPL_NAME,TAXABLE_VALUE,FISCAL_YEAR,CGST_PERCENTAGE,SGST_PERCENTAGE,IGST_PERCENTAGE,CESS_PERCENTAGE,CGST_AMT,SGST_AMT,IGST_AMT,CESS_AMT,TAXABLE_LD_PENALTY,CGST_LD_PENALTY,SGST_LD_PENALTY,IGST_LD_PENALTY,CESS_LD_PENALTY,CGST_TDS,SGST_TDS,IGST_TDS,CESS_TDS)VALUES(@GST_STATUS,@GST_PAYMENT_DATE,@GST_PAYMENT_VOUCHER_NO,@INVOICE_NO,@INVOICE_DATE,@ENTRY_DATE,@RCM_CGST_AMT,@RCM_SGST_AMT,@RCM_IGST_AMT,@RCM_CESS_AMT,@GARN_CRR_MB_NO,@VALUATION_DATE,@DATA_TYPE,@SL_NO,@SUPL_CODE,@SUPL_NAME,@TAXABLE_VALUE,@FISCAL_YEAR,@CGST_PERCENTAGE,@SGST_PERCENTAGE,@IGST_PERCENTAGE,@CESS_PERCENTAGE,@CGST_AMT,@SGST_AMT,@IGST_AMT,@CESS_AMT,@TAXABLE_LD_PENALTY,@CGST_LD_PENALTY,@SGST_LD_PENALTY,@IGST_LD_PENALTY,@CESS_LD_PENALTY,@CGST_TDS,@SGST_TDS,@IGST_TDS,@CESS_TDS)"
                                    cmd1 = New SqlCommand(query1, conn_trans, myTrans)
                                    cmd1.Parameters.AddWithValue("@INVOICE_NO", TextBox160.Text)
                                    cmd1.Parameters.AddWithValue("@INVOICE_DATE", Date.ParseExact(TextBox161.Text, "dd-MM-yyyy", provider))
                                    cmd1.Parameters.AddWithValue("@GARN_CRR_MB_NO", M_garn_crrnoDropDownList.Text)
                                    cmd1.Parameters.AddWithValue("@VALUATION_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                                    cmd1.Parameters.AddWithValue("@DATA_TYPE", "PURCHASE")
                                    cmd1.Parameters.AddWithValue("@SL_NO", CDec(GridView210.Rows(I).Cells(3).Text))
                                    cmd1.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                                    cmd1.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                                    cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(GridView210.Rows(I).Cells(45).Text))
                                    cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                                    cmd1.Parameters.AddWithValue("@SGST_PERCENTAGE", CDec(GridView210.Rows(I).Cells(46).Text))
                                    cmd1.Parameters.AddWithValue("@CGST_PERCENTAGE", CDec(GridView210.Rows(I).Cells(47).Text))
                                    cmd1.Parameters.AddWithValue("@IGST_PERCENTAGE", CDec(GridView210.Rows(I).Cells(48).Text))
                                    cmd1.Parameters.AddWithValue("@CESS_PERCENTAGE", CDec(GridView210.Rows(I).Cells(49).Text))

                                    If (DropDownList44.SelectedValue = "Yes") Then
                                        cmd1.Parameters.AddWithValue("@SGST_AMT", 0)
                                        cmd1.Parameters.AddWithValue("@CGST_AMT", 0)
                                        cmd1.Parameters.AddWithValue("@IGST_AMT", 0)
                                        cmd1.Parameters.AddWithValue("@CESS_AMT", 0)
                                        cmd1.Parameters.AddWithValue("@RCM_SGST_AMT", CDec(GridView210.Rows(I).Cells(18).Text))
                                        cmd1.Parameters.AddWithValue("@RCM_CGST_AMT", CDec(GridView210.Rows(I).Cells(19).Text))
                                        cmd1.Parameters.AddWithValue("@RCM_IGST_AMT", CDec(GridView210.Rows(I).Cells(20).Text))
                                        cmd1.Parameters.AddWithValue("@RCM_CESS_AMT", CDec(GridView210.Rows(I).Cells(21).Text))
                                    Else
                                        cmd1.Parameters.AddWithValue("@SGST_AMT", CDec(GridView210.Rows(I).Cells(18).Text))
                                        cmd1.Parameters.AddWithValue("@CGST_AMT", CDec(GridView210.Rows(I).Cells(19).Text))
                                        cmd1.Parameters.AddWithValue("@IGST_AMT", CDec(GridView210.Rows(I).Cells(20).Text))
                                        cmd1.Parameters.AddWithValue("@CESS_AMT", CDec(GridView210.Rows(I).Cells(21).Text))
                                        cmd1.Parameters.AddWithValue("@RCM_SGST_AMT", 0)
                                        cmd1.Parameters.AddWithValue("@RCM_CGST_AMT", 0)
                                        cmd1.Parameters.AddWithValue("@RCM_IGST_AMT", 0)
                                        cmd1.Parameters.AddWithValue("@RCM_CESS_AMT", 0)
                                    End If

                                    cmd1.Parameters.AddWithValue("@TAXABLE_LD_PENALTY", CDec(GridView210.Rows(I).Cells(24).Text) + CDec(GridView210.Rows(I).Cells(25).Text))
                                    cmd1.Parameters.AddWithValue("@SGST_LD_PENALTY", CDec(GridView210.Rows(I).Cells(35).Text))
                                    cmd1.Parameters.AddWithValue("@CGST_LD_PENALTY", CDec(GridView210.Rows(I).Cells(36).Text))
                                    cmd1.Parameters.AddWithValue("@IGST_LD_PENALTY", CDec(GridView210.Rows(I).Cells(37).Text))
                                    cmd1.Parameters.AddWithValue("@CESS_LD_PENALTY", CDec(GridView210.Rows(I).Cells(38).Text))
                                    cmd1.Parameters.AddWithValue("@SGST_TDS", CDec(GridView210.Rows(I).Cells(40).Text))
                                    cmd1.Parameters.AddWithValue("@CGST_TDS", CDec(GridView210.Rows(I).Cells(41).Text))
                                    cmd1.Parameters.AddWithValue("@IGST_TDS", CDec(GridView210.Rows(I).Cells(42).Text))
                                    cmd1.Parameters.AddWithValue("@CESS_TDS", 0)
                                    cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
                                    If (DropDownList5.SelectedValue = "Yes") Then
                                        cmd1.Parameters.AddWithValue("@GST_STATUS", "PAID")
                                        cmd1.Parameters.AddWithValue("@GST_PAYMENT_DATE", CDate(DateTime.Now.ToString("dd-MM-yyyy")))
                                        cmd1.Parameters.AddWithValue("@GST_PAYMENT_VOUCHER_NO", "")
                                    Else
                                        cmd1.Parameters.AddWithValue("@GST_STATUS", "PENDING")
                                        cmd1.Parameters.AddWithValue("@GST_PAYMENT_DATE", DBNull.Value)
                                        cmd1.Parameters.AddWithValue("@GST_PAYMENT_VOUCHER_NO", DBNull.Value)
                                    End If
                                    cmd1.ExecuteReader()
                                    cmd1.Dispose()


                                End If


                                prov_price = prov_price + CDec(GridView210.Rows(I).Cells(30).Text)
                                ld_price = ld_price + CDec(GridView210.Rows(I).Cells(25).Text)
                                penality_price = penality_price + CDec(GridView210.Rows(I).Cells(24).Text)
                                sgst_price = sgst_price + CDec(GridView210.Rows(I).Cells(18).Text)
                                cgst_price = cgst_price + CDec(GridView210.Rows(I).Cells(19).Text)
                                igst_price = igst_price + CDec(GridView210.Rows(I).Cells(20).Text)
                                cess_price = cess_price + CDec(GridView210.Rows(I).Cells(21).Text)
                                sgst_LD_Penalty = sgst_LD_Penalty + CDec(GridView210.Rows(I).Cells(35).Text)
                                cgst_LD_Penalty = cgst_LD_Penalty + CDec(GridView210.Rows(I).Cells(36).Text)
                                igst_LD_Penalty = igst_LD_Penalty + CDec(GridView210.Rows(I).Cells(37).Text)
                                cess_LD_Penalty = cess_LD_Penalty + CDec(GridView210.Rows(I).Cells(38).Text)
                                short_price = short_price + CDec(GridView210.Rows(I).Cells(27).Text)
                                diff_price = diff_price + CDec(GridView210.Rows(I).Cells(31).Text)
                                sund_price = sund_price + CDec(GridView210.Rows(I).Cells(26).Text)
                                sd = sd + CDec(GridView210.Rows(I).Cells(39).Text)
                                TDS_SGST = TDS_SGST + CDec(GridView210.Rows(I).Cells(40).Text)
                                TDS_CGST = TDS_CGST + CDec(GridView210.Rows(I).Cells(41).Text)
                                TDS_IGST = TDS_IGST + CDec(GridView210.Rows(I).Cells(42).Text)
                                other_deduction = other_deduction + CDec(GridView210.Rows(I).Cells(43).Text)
                                TCS_AMOUNT = TCS_AMOUNT + CDec(GridView210.Rows(I).Cells(44).Text)
                                TDS_AMOUNT = TDS_AMOUNT + CDec(GridView210.Rows(I).Cells(50).Text)
                                TAXABLE_AMOUNT = TAXABLE_AMOUNT + CDec(GridView210.Rows(I).Cells(45).Text)


                                ''psc calculatation
                                If CDec(GridView210.Rows(I).Cells(31).Text > 0) Then
                                    save_ledger(GridView210.Rows(I).Cells(3).Text, M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), diff_head, "Dr", CDec(GridView210.Rows(I).Cells(31).Text), "PSC", DropDownList40.Text, 7, "")
                                ElseIf CDec(GridView210.Rows(I).Cells(31).Text < 0) Then
                                    Dim DIFF As Decimal = 0.0
                                    DIFF = CDec(GridView210.Rows(I).Cells(31).Text) * (-1)
                                    save_ledger(GridView210.Rows(I).Cells(3).Text, M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), diff_head, "Cr", DIFF, "PSC", DropDownList40.Text, 7, "")
                                End If

                            End If
                        Next

                        sund_price = sund_price



                        ''save ledger
                        save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), prov_head, "Dr", prov_price, "PROV", DropDownList40.Text, 1, "")


                        ''TCS ENTRY
                        save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), TCS_HEAD, "Dr", TCS_AMOUNT, "TCS_INPUT", DropDownList40.Text, 3, "")

                        ''TDS ENTRY FOR RAW MATERIAL & STORES
                        If (CDec(TextBox64.Text) > 0) Then
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), "51704", "Cr", TDS_AMOUNT, "TDS_194(O)", DropDownList40.Text, 4, "")
                        Else
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), TDS_HEAD, "Cr", TDS_AMOUNT, "IT", DropDownList40.Text, 4, "")
                        End If


                        If (DropDownList5.SelectedValue = "Yes") Then
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), sgst_head, "Dr", sgst_price, "sgst", DropDownList40.Text, 2, "")
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), cgst_head, "Dr", cgst_price, "cgst", DropDownList40.Text, 2, "")
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), igst_head, "Dr", igst_price, "igst", DropDownList40.Text, 2, "")
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), cess_head, "Dr", cess_price, "cess", DropDownList40.Text, 2, "")

                            ''RCM LIAB
                            If DropDownList44.SelectedValue = "Yes" Then
                                save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), RCM_SGST_HEAD, "Cr", sgst_price - sgst_LD_Penalty, "sgst_liab", DropDownList40.Text, 2, "")
                                save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), RCM_CGST_HEAD, "Cr", cgst_price - cgst_LD_Penalty, "cgst_liab", DropDownList40.Text, 2, "")
                                save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), RCM_IGST_HEAD, "Cr", igst_price - igst_LD_Penalty, "igst_liab", DropDownList40.Text, 2, "")
                                save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), RCM_CESS_HEAD, "Cr", cess_price - cess_LD_Penalty, "cess_liab", DropDownList40.Text, 2, "")
                            End If
                        End If



                        'save_ledger(M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), SGST_LIAB_LD_PENALTY, "Cr", sgst_LD_Penalty, "sgst_LD_Penalty", DropDownList40.Text, 2, "")
                        'save_ledger(M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), CGST_LIAB_LD_PENALTY, "Cr", cgst_LD_Penalty, "cgst_LD_Penalty", DropDownList40.Text, 2, "")
                        'save_ledger(M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), IGST_LIAB_LD_PENALTY, "Cr", igst_LD_Penalty, "igst_LD_Penalty", DropDownList40.Text, 2, "")
                        'save_ledger(M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), CESS_LIAB_LD_PENALTY, "Cr", cess_LD_Penalty, "cess_LD_Penalty", DropDownList40.Text, 2, "")
                        'save_ledger(M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), short_head, "Dr", short_price, "SHORT ADJUST", DropDownList40.Text, 4, "")
                        save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), ld_head, "Cr", ld_price, "LD", DropDownList40.Text, 5, "")


                        ''Checking whether the order is from IPT or not
                        'If (Label93.Text = "I.P.T.") Then
                        '    save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), IUCA_HEAD, "Cr", sund_price, "IUCA", DropDownList40.Text, 8, "")
                        'Else
                        '    save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), sund_head, "Cr", sund_price, "SUND", DropDownList40.Text, 8, "")
                        'End If


                        ''TDS ON GST
                        save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), TDS_SGST_HEAD, "Cr", TDS_SGST, "TDS_SGST", DropDownList40.Text, 9, "")
                        save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), TDS_CGST_HEAD, "Cr", TDS_CGST, "TDS_CGST", DropDownList40.Text, 9, "")
                        save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), TDS_IGST_HEAD, "Cr", TDS_IGST, "TDS_IGST", DropDownList40.Text, 9, "")

                        If (other_deduction > 0) Then
                            OTHER_DEDUCTION_HEAD = TextBox59.Text.Substring(0, TextBox59.Text.IndexOf(",") - 1).Trim
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), OTHER_DEDUCTION_HEAD, "Cr", other_deduction, "Other_Deduction", DropDownList40.Text, 12, "")
                        End If

                        ''''===============================''''
                        If sd > 0 Then
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), sd_head, "Cr", sd, "SD", DropDownList40.Text, 5, "")
                        End If

                        ''''===============================''''



                        ''update ledger
                        If (Label93.Text = "I.P.T.") Then
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), IUCA_HEAD, "Cr", sund_price, "IUCA", DropDownList40.Text, 8, "")
                        Else
                            save_ledger("0", M_Label398.Text, M_garn_crrnoDropDownList.SelectedValue, TextBox160.Text, M_Label396.Text.Substring(0, M_Label396.Text.IndexOf(",") - 1), sund_head, "Cr", sund_price, "SUND", DropDownList40.Text, 8, "")

                            Dim cmd11 As New SqlCommand
                            Dim Query11 As String = "update LEDGER set PAYMENT_INDICATION ='P' where PO_NO='" & M_Label398.Text & "' and INVOICE_NO is null and POST_INDICATION ='PROV' AND GARN_NO_MB_NO ='" & M_garn_crrnoDropDownList.SelectedValue & "'"
                            cmd11 = New SqlCommand(Query11, conn_trans, myTrans)
                            cmd11.ExecuteReader()
                            cmd11.Dispose()
                        End If




                        ''insert party amount

                        Dim query As String = "INSERT INTO PARTY_AMT(POST_TYPE,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@POST_TYPE,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                        Dim cmd_1 As New SqlCommand(query, conn_trans, myTrans)
                        cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                        cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                        cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList40.SelectedValue)
                        cmd_1.Parameters.AddWithValue("@ORDER_NO", M_Label398.Text)
                        cmd_1.Parameters.AddWithValue("@GARN_MB_NO", M_garn_crrnoDropDownList.SelectedValue)
                        cmd_1.Parameters.AddWithValue("@AC_CODE", sund_head)
                        cmd_1.Parameters.AddWithValue("@AMOUNT_DR", 0.0)
                        cmd_1.Parameters.AddWithValue("@AMOUNT_CR", sund_price)
                        If (Label93.Text = "I.P.T.") Then
                            cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", 0)
                        Else
                            cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", sund_price)
                        End If

                        cmd_1.Parameters.AddWithValue("@POST_TYPE", "SUND")
                        cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                        cmd_1.ExecuteReader()
                        cmd_1.Dispose()

                        ''REFRESH
                        conn.Open()
                        da = New SqlDataAdapter("select DISTINCT GARN_NO from po_rcd_mat WITH(NOLOCK) WHERE V_IND IS NULL AND GARN_NO <> 'PENDING' AND PO_NO='" & TextBox171.Text & "'", conn)
                        da.Fill(dt)
                        conn.Close()

                        M_garn_crrnoDropDownList.Items.Clear()
                        M_garn_crrnoDropDownList.DataSource = dt
                        M_garn_crrnoDropDownList.DataValueField = "GARN_NO"
                        M_garn_crrnoDropDownList.DataBind()
                        M_garn_crrnoDropDownList.Items.Add("Select")
                        M_garn_crrnoDropDownList.SelectedValue = "Select"
                        Panel38.Visible = False
                        ''gridview clear
                        Dim DT10 As New DataTable
                        DT10.Columns.AddRange(New DataColumn(17) {New DataColumn("CRR_NO"), New DataColumn("PO_NO"), New DataColumn("AMD_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("CHLN_NO"), New DataColumn("MAT_CHALAN_QTY"), New DataColumn("MAT_RCD_QTY"), New DataColumn("MAT_REJ_QTY"), New DataColumn("MAT_EXCE"), New DataColumn("MAT_BAL_QTY"), New DataColumn("TRANS_SHORT"), New DataColumn("PROV_VALUE"), New DataColumn("TRANS_CHARGE"), New DataColumn("GARN_NOTE")})
                        GridView210.DataSource = DT10
                        GridView210.DataBind()
                        Label621.Text = ""
                        Label621.Visible = False
                    Else
                        Label621.Visible = True
                        Label621.Text = "Authentication Failed"
                        TextBox173.Text = ""
                        Return
                    End If
                End If

                myTrans.Commit()
                M_DropDownList6.Items.Clear()
                M_GARN_ERR_LABLE.Visible = True
                M_GARN_ERR_LABLE.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                M_GARN_ERR_LABLE.Visible = True
                M_GARN_ERR_LABLE.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim total_price, final_payment_amount, total_sgst, total_cgst, total_igst, total_it, total_penalty, total_ld, gst_on_ld_penalty, total_sd, rcd_qty As New Decimal(0.0)
        Dim flag As Boolean
        flag = False
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                If chkRow.Checked Then
                    flag = True

                    rcd_qty = rcd_qty + CDec(row.Cells(10).Text)
                    total_price = total_price + CDec(row.Cells(13).Text)
                    total_penalty = total_penalty + CDec(row.Cells(14).Text)
                    total_sgst = total_sgst + CDec(row.Cells(15).Text)
                    total_cgst = total_cgst + CDec(row.Cells(16).Text)
                    total_igst = total_igst + CDec(row.Cells(17).Text)
                    total_it = total_it + CDec(row.Cells(23).Text)
                    total_sd = total_sd + CDec(row.Cells(24).Text)
                    total_ld = total_ld + CDec(row.Cells(27).Text)
                    gst_on_ld_penalty = gst_on_ld_penalty + CDec(row.Cells(28).Text) + CDec(row.Cells(29).Text) + CDec(row.Cells(30).Text) + CDec(row.Cells(31).Text)
                    final_payment_amount = final_payment_amount + CDec(row.Cells(32).Text)

                End If
            End If
        Next

        TextBox23.Text = rcd_qty
        TextBox39.Text = total_price
        TextBox40.Text = total_sgst
        TextBox41.Text = total_cgst
        TextBox42.Text = total_igst
        TextBox43.Text = total_ld
        TextBox46.Text = total_sd
        TextBox47.Text = total_it
        TextBox48.Text = total_penalty
        TextBox49.Text = final_payment_amount
        TextBox50.Text = gst_on_ld_penalty

        If (flag = False) Then
            Label20.Text = "Please select atleaset one row"
            Return
        End If


    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Return
        ElseIf (CDec(TextBox63.Text) > 0 And CDec(TextBox24.Text) > 0) Then
            Label17.Text = "TDS cannot be filled on both the textboxes."
            Return
        ElseIf ((CDec(TextBox14.Text) + CDec(TextBox16.Text) + CDec(TextBox18.Text)) <= 0) Then
            Label17.Visible = True
            Label17.Text = "GST percentage cannot be 0.."
            Return
        Else
            Label17.Text = ""
        End If
        Dim mc1 As New SqlCommand
        Dim SUPL_TYPE As New String("")
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                conn.Open()
                mc1.CommandText = "select * from supl where supl_id='" & Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1).Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    SUPL_TYPE = dr.Item("SUPL_TYPE")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
                'Database updation entry
                If DropDownList1.SelectedValue = "Select" Then

                    Dim f_date, to_date As Date
                    f_date = CDate(TextBox174.Text)
                    to_date = CDate(TextBox175.Text)
                    Dim ds5 As New DataSet
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("select * from mb_book where PO_NO='" & TextBox171.Text & "' and wo_slno ='" & DropDownList41.Text & "' and v_ind is null AND mb_date between '" & f_date.Year & "-" & f_date.Month & "-" & f_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by mb_no", conn)
                    da.Fill(dt)
                    conn.Close()
                    GridView1.DataSource = dt
                    GridView1.DataBind()

                    Dim I As Integer
                    For I = 0 To GridView1.Rows.Count - 1
                        If GridView1.Rows(I).Cells(4).Text = DropDownList41.SelectedValue Then

                            mycommand = New SqlCommand("DELETE FROM GARN_PRICE WHERE CRR_NO ='" & DropDownList1.SelectedValue & "' and po_no ='" & Label12.Text & "' and MAT_SLNO=" & DropDownList41.SelectedValue, conn_trans, myTrans)
                            mycommand.ExecuteNonQuery()

                            ''save garn_price
                            Dim query As String = "Insert Into GARN_PRICE(LD_CHARGE,CRR_NO,PO_NO,MAT_SLNO,UNIT_PRICE,DISCOUNT,PACKING,SGST,CGST,IGST,CESS,ENTRY_TAX,PENALITY_TYPE,PENALITY,REMARKS_PENALITY,REMARKS_MODIFY,LAST_MODIFY_BY ) values (@LD_CHARGE,@CRR_NO,@PO_NO,@MAT_SLNO,@UNIT_PRICE,@DISCOUNT,@PACKING,@SGST,@CGST,@IGST,@CESS,@ENTRY_TAX,@PENALITY_TYPE,@PENALITY,@REMARKS_PENALITY,@REMARKS_MODIFY,@LAST_MODIFY_BY)"
                            Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@CRR_NO", DropDownList1.SelectedValue)
                            cmd.Parameters.AddWithValue("@PO_NO", Label12.Text)
                            cmd.Parameters.AddWithValue("@MAT_SLNO", DropDownList41.SelectedValue)
                            cmd.Parameters.AddWithValue("@UNIT_PRICE", TextBox8.Text)
                            cmd.Parameters.AddWithValue("@DISCOUNT", TextBox10.Text)
                            cmd.Parameters.AddWithValue("@PACKING", TextBox26.Text)
                            cmd.Parameters.AddWithValue("@SGST", TextBox14.Text)
                            cmd.Parameters.AddWithValue("@CGST", TextBox16.Text)
                            cmd.Parameters.AddWithValue("@IGST", TextBox18.Text)
                            cmd.Parameters.AddWithValue("@CESS", TextBox20.Text)
                            cmd.Parameters.AddWithValue("@ENTRY_TAX", TextBox24.Text)
                            cmd.Parameters.AddWithValue("@PENALITY_TYPE", "0")
                            cmd.Parameters.AddWithValue("@PENALITY", TextBox22.Text)
                            cmd.Parameters.AddWithValue("@REMARKS_PENALITY", TextBox27.Text)
                            cmd.Parameters.AddWithValue("@REMARKS_MODIFY", TextBox28.Text)
                            cmd.Parameters.AddWithValue("@LD_CHARGE", TextBox25.Text)
                            cmd.Parameters.AddWithValue("@LAST_MODIFY_BY", Session("userName"))
                            cmd.ExecuteReader()
                            cmd.Dispose()

                            ''put value
                            Dim basic_price, disc_price, material_price, tds_price, SGST_VALUE, CGST_VALUE, IGST_VALUE, CESS_VALUE As New Decimal(0)
                            Dim unit_price, penality, mat_packing, discount, tds, SGST, CGST, IGST, CESS, SUND_PRICE, TDS_SGST, TDS_CGST, TDS_IGST As Decimal
                            unit_price = TextBox8.Text
                            discount = TextBox10.Text
                            mat_packing = TextBox26.Text
                            SGST = TextBox14.Text
                            CGST = TextBox16.Text
                            IGST = TextBox18.Text
                            CESS = TextBox20.Text
                            If (CDec(TextBox63.Text) > 0) Then
                                tds = TextBox63.Text
                            Else
                                tds = TextBox24.Text
                            End If
                            penality = TextBox22.Text
                            Dim sd_order As Decimal = TextBox4.Text


                            basic_price = FormatNumber(unit_price * CDec(GridView1.Rows(I).Cells(10).Text), 2)
                            disc_price = FormatNumber((basic_price * discount) / 100, 2)
                            material_price = FormatNumber(mat_packing * CDec(GridView1.Rows(I).Cells(10).Text), 2)

                            SGST_VALUE = FormatNumber(((basic_price - disc_price + material_price) * SGST) / 100, 2)
                            CGST_VALUE = FormatNumber(((basic_price - disc_price + material_price) * CGST) / 100, 2)
                            IGST_VALUE = FormatNumber(((basic_price - disc_price + material_price) * IGST) / 100, 2)
                            CESS_VALUE = FormatNumber(((basic_price - disc_price + material_price) * CESS) / 100, 2)

                            TDS_SGST = FormatNumber(((basic_price - disc_price + material_price) * CDec(TextBox65.Text)) / 100, 2)
                            TDS_CGST = FormatNumber(((basic_price - disc_price + material_price) * CDec(TextBox66.Text)) / 100, 2)
                            TDS_IGST = FormatNumber(((basic_price - disc_price + material_price) * CDec(TextBox67.Text)) / 100, 2)



                            tds_price = FormatNumber(((basic_price - disc_price) * tds) / 100, 2)
                            sd_order = FormatNumber((((basic_price - disc_price) + material_price) * sd_order) / 100, 2)
                            'sd_order = TextBox4.Text
                            'tds_price = TextBox24.Text
                            GridView1.Rows(I).Cells(13).Text = basic_price - disc_price
                            'GridView1.Rows(I).Cells(9).Text = CDec(GridView1.Rows(I).Cells(11).Text) / CDec(GridView1.Rows(I).Cells(8).Text)
                            GridView1.Rows(I).Cells(11).Text = TextBox8.Text
                            GridView1.Rows(I).Cells(14).Text = TextBox22.Text
                            GridView1.Rows(I).Cells(27).Text = TextBox25.Text

                            ''==============================RCM GST CALCULATION================================''
                            GridView1.Rows(I).Cells(28).Text = "0.00"
                            GridView1.Rows(I).Cells(29).Text = "0.00"
                            GridView1.Rows(I).Cells(30).Text = "0.00"
                            GridView1.Rows(I).Cells(31).Text = "0.00"
                            If DropDownList2.SelectedValue = "Yes" Then
                                GridView1.Rows(I).Cells(15).Text = SGST_VALUE
                                GridView1.Rows(I).Cells(16).Text = CGST_VALUE
                                GridView1.Rows(I).Cells(17).Text = IGST_VALUE
                                GridView1.Rows(I).Cells(18).Text = CESS_VALUE
                                GridView1.Rows(I).Cells(19).Text = SGST_VALUE
                                GridView1.Rows(I).Cells(20).Text = CGST_VALUE
                                GridView1.Rows(I).Cells(21).Text = IGST_VALUE
                                GridView1.Rows(I).Cells(22).Text = CESS_VALUE
                            ElseIf DropDownList2.SelectedValue = "No" Then
                                GridView1.Rows(I).Cells(15).Text = SGST_VALUE
                                GridView1.Rows(I).Cells(16).Text = CGST_VALUE
                                GridView1.Rows(I).Cells(17).Text = IGST_VALUE
                                GridView1.Rows(I).Cells(18).Text = CESS_VALUE
                                GridView1.Rows(I).Cells(19).Text = "0.00"
                                GridView1.Rows(I).Cells(20).Text = "0.00"
                                GridView1.Rows(I).Cells(21).Text = "0.00"
                                GridView1.Rows(I).Cells(22).Text = "0.00"
                            End If


                            GridView1.Rows(I).Cells(23).Text = tds_price
                            GridView1.Rows(I).Cells(24).Text = sd_order
                            'GridView1.Rows(I).Cells(25).Text = FormatNumber(CDec(GridView1.Rows(I).Cells(12).Text) - CDec(GridView1.Rows(I).Cells(13).Text), 2)
                            If DropDownList7.SelectedValue = "Yes" Then
                                SUND_PRICE = FormatNumber(CDec(GridView1.Rows(I).Cells(13).Text) + SGST_VALUE + CGST_VALUE + IGST_VALUE + CESS_VALUE - (CDec(TextBox25.Text) + CDec(TextBox22.Text) + sd_order + tds_price + CDec(GridView1.Rows(I).Cells(19).Text) + CDec(GridView1.Rows(I).Cells(20).Text) + CDec(GridView1.Rows(I).Cells(21).Text) + CDec(GridView1.Rows(I).Cells(22).Text) + TDS_SGST + TDS_CGST + TDS_IGST), 2)
                            ElseIf DropDownList7.SelectedValue = "No" Then
                                SUND_PRICE = FormatNumber(CDec(GridView1.Rows(I).Cells(13).Text) - (CDec(TextBox25.Text) + CDec(TextBox22.Text) + sd_order + tds_price + CDec(GridView1.Rows(I).Cells(19).Text) + CDec(GridView1.Rows(I).Cells(20).Text) + CDec(GridView1.Rows(I).Cells(21).Text) + CDec(GridView1.Rows(I).Cells(22).Text) + TDS_SGST + TDS_CGST + TDS_IGST), 2)
                            End If


                            GridView1.Rows(I).Cells(25).Text = (CDec(GridView1.Rows(I).Cells(12).Text) - CDec(GridView1.Rows(I).Cells(13).Text)) + (SUND_PRICE - CInt(SUND_PRICE))
                            GridView1.Rows(I).Cells(26).Text = DropDownList2.SelectedValue
                            GridView1.Rows(I).Cells(32).Text = (SUND_PRICE)

                            ''SGST PERCENTAGE
                            GridView1.Rows(I).Cells(33).Text = TextBox14.Text
                            ''CGST PERCENTAGE
                            GridView1.Rows(I).Cells(34).Text = TextBox16.Text
                            ''IGST PERCENTAGE
                            GridView1.Rows(I).Cells(35).Text = TextBox18.Text
                            ''CESS PERCENTAGE
                            GridView1.Rows(I).Cells(36).Text = TextBox20.Text
                            ''TAXABLE VALUE
                            GridView1.Rows(I).Cells(37).Text = basic_price - disc_price + material_price

                            ''TDS ON GST
                            GridView1.Rows(I).Cells(38).Text = TDS_SGST
                            GridView1.Rows(I).Cells(39).Text = TDS_CGST
                            GridView1.Rows(I).Cells(40).Text = TDS_IGST
                        End If
                    Next

                Else

                    'Dim ds5 As New DataSet
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("select * from mb_book where mb_no='" & DropDownList1.SelectedValue & "' and v_ind is null AND PO_NO='" & TextBox171.Text & "'", conn)
                    da.Fill(dt)
                    conn.Close()
                    GridView1.DataSource = dt
                    GridView1.DataBind()

                    ' Delete from GARN_Price

                    mycommand = New SqlCommand("DELETE FROM GARN_PRICE WHERE CRR_NO ='" & DropDownList1.SelectedValue & "' and po_no ='" & Label12.Text & "' and MAT_SLNO=" & DropDownList41.SelectedValue, conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()

                    ''save garn_price

                    Dim query As String = "Insert Into GARN_PRICE(LD_CHARGE,CRR_NO,PO_NO,MAT_SLNO,UNIT_PRICE,DISCOUNT,PACKING,SGST,CGST,IGST,CESS,ENTRY_TAX,PENALITY_TYPE,PENALITY,REMARKS_PENALITY,REMARKS_MODIFY,LAST_MODIFY_BY ) values (@LD_CHARGE,@CRR_NO,@PO_NO,@MAT_SLNO,@UNIT_PRICE,@DISCOUNT,@PACKING,@SGST,@CGST,@IGST,@CESS,@ENTRY_TAX,@PENALITY_TYPE,@PENALITY,@REMARKS_PENALITY,@REMARKS_MODIFY,@LAST_MODIFY_BY)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@CRR_NO", DropDownList1.SelectedValue)
                    cmd.Parameters.AddWithValue("@PO_NO", Label12.Text)
                    cmd.Parameters.AddWithValue("@MAT_SLNO", DropDownList41.SelectedValue)
                    cmd.Parameters.AddWithValue("@UNIT_PRICE", TextBox8.Text)
                    cmd.Parameters.AddWithValue("@DISCOUNT", TextBox10.Text)
                    cmd.Parameters.AddWithValue("@PACKING", TextBox26.Text)
                    cmd.Parameters.AddWithValue("@SGST", TextBox14.Text)
                    cmd.Parameters.AddWithValue("@CGST", TextBox16.Text)
                    cmd.Parameters.AddWithValue("@IGST", TextBox18.Text)
                    cmd.Parameters.AddWithValue("@CESS", TextBox20.Text)
                    cmd.Parameters.AddWithValue("@ENTRY_TAX", TextBox24.Text)
                    cmd.Parameters.AddWithValue("@PENALITY_TYPE", "0")
                    cmd.Parameters.AddWithValue("@PENALITY", TextBox22.Text)
                    cmd.Parameters.AddWithValue("@REMARKS_PENALITY", TextBox27.Text)
                    cmd.Parameters.AddWithValue("@REMARKS_MODIFY", TextBox28.Text)
                    cmd.Parameters.AddWithValue("@LD_CHARGE", TextBox25.Text)
                    cmd.Parameters.AddWithValue("@LAST_MODIFY_BY", Session("userName"))
                    cmd.ExecuteReader()
                    cmd.Dispose()


                    ''put value
                    Dim basic_price, disc_price, material_price, tds_price, SGST_VALUE, CGST_VALUE, IGST_VALUE, CESS_VALUE As New Decimal(0)
                    Dim net_price, unit_price, penality, mat_packing, discount, tds, SGST, CGST, IGST, CESS, SUND_PRICE, TDS_SGST, TDS_CGST, TDS_IGST As Decimal
                    unit_price = TextBox8.Text
                    discount = TextBox10.Text
                    mat_packing = TextBox26.Text
                    SGST = TextBox14.Text
                    CGST = TextBox16.Text
                    IGST = TextBox18.Text
                    CESS = TextBox20.Text
                    If (CDec(TextBox63.Text) > 0) Then
                        tds = TextBox63.Text
                    Else
                        tds = TextBox24.Text
                    End If

                    penality = TextBox22.Text
                    Dim sd_order As Decimal = TextBox4.Text
                    ''grid view search
                    Dim I As Integer
                    For I = 0 To GridView1.Rows.Count - 1
                        If GridView1.Rows(I).Cells(4).Text = DropDownList41.SelectedValue Then
                            basic_price = FormatNumber(unit_price * CDec(GridView1.Rows(I).Cells(10).Text), 3)
                            disc_price = FormatNumber((basic_price * discount) / 100, 3)
                            material_price = FormatNumber(mat_packing * CDec(GridView1.Rows(I).Cells(10).Text), 3)
                            net_price = (basic_price - disc_price) + mat_packing

                            SGST_VALUE = FormatNumber(((basic_price - disc_price + material_price) * SGST) / 100, 2)
                            CGST_VALUE = FormatNumber(((basic_price - disc_price + material_price) * CGST) / 100, 2)
                            IGST_VALUE = FormatNumber(((basic_price - disc_price + material_price) * IGST) / 100, 2)
                            CESS_VALUE = FormatNumber(((basic_price - disc_price + material_price) * CESS) / 100, 2)

                            TDS_SGST = FormatNumber(((basic_price - disc_price + material_price) * CDec(TextBox65.Text)) / 100, 2)
                            TDS_CGST = FormatNumber(((basic_price - disc_price + material_price) * CDec(TextBox66.Text)) / 100, 2)
                            TDS_IGST = FormatNumber(((basic_price - disc_price + material_price) * CDec(TextBox67.Text)) / 100, 2)



                            tds_price = FormatNumber(((basic_price - disc_price) * tds) / 100, 2)
                            sd_order = FormatNumber((((basic_price - disc_price) + material_price) * sd_order) / 100, 2)
                            'sd_order = TextBox4.Text
                            'tds_price = TextBox24.Text
                            GridView1.Rows(I).Cells(13).Text = basic_price - disc_price
                            'GridView1.Rows(I).Cells(9).Text = CDec(GridView1.Rows(I).Cells(11).Text) / CDec(GridView1.Rows(I).Cells(8).Text)
                            GridView1.Rows(I).Cells(11).Text = TextBox8.Text
                            GridView1.Rows(I).Cells(14).Text = TextBox22.Text
                            GridView1.Rows(I).Cells(27).Text = TextBox25.Text
                            'RCM GST
                            GridView1.Rows(I).Cells(28).Text = "0.00"
                            GridView1.Rows(I).Cells(29).Text = "0.00"
                            GridView1.Rows(I).Cells(30).Text = "0.00"
                            GridView1.Rows(I).Cells(31).Text = "0.00"
                            If DropDownList2.SelectedValue = "Yes" Then
                                GridView1.Rows(I).Cells(15).Text = SGST_VALUE
                                GridView1.Rows(I).Cells(16).Text = CGST_VALUE
                                GridView1.Rows(I).Cells(17).Text = IGST_VALUE
                                GridView1.Rows(I).Cells(18).Text = CESS_VALUE
                                GridView1.Rows(I).Cells(19).Text = SGST_VALUE
                                GridView1.Rows(I).Cells(20).Text = CGST_VALUE
                                GridView1.Rows(I).Cells(21).Text = IGST_VALUE
                                GridView1.Rows(I).Cells(22).Text = CESS_VALUE
                            ElseIf DropDownList2.SelectedValue = "No" Then
                                GridView1.Rows(I).Cells(15).Text = SGST_VALUE
                                GridView1.Rows(I).Cells(16).Text = CGST_VALUE
                                GridView1.Rows(I).Cells(17).Text = IGST_VALUE
                                GridView1.Rows(I).Cells(18).Text = CESS_VALUE
                                GridView1.Rows(I).Cells(19).Text = "0.00"
                                GridView1.Rows(I).Cells(20).Text = "0.00"
                                GridView1.Rows(I).Cells(21).Text = "0.00"
                                GridView1.Rows(I).Cells(22).Text = "0.00"
                            End If


                            GridView1.Rows(I).Cells(23).Text = tds_price
                            GridView1.Rows(I).Cells(24).Text = sd_order
                            ''PSC CALCULATION
                            If DropDownList7.SelectedValue = "Yes" Then
                                SUND_PRICE = FormatNumber(CDec(GridView1.Rows(I).Cells(13).Text) + SGST_VALUE + CGST_VALUE + IGST_VALUE + CESS_VALUE - (CDec(TextBox25.Text) + CDec(TextBox22.Text) + sd_order + tds_price + CDec(GridView1.Rows(I).Cells(19).Text) + CDec(GridView1.Rows(I).Cells(20).Text) + CDec(GridView1.Rows(I).Cells(21).Text) + CDec(GridView1.Rows(I).Cells(22).Text) + TDS_SGST + TDS_CGST + TDS_IGST), 2)
                            ElseIf DropDownList7.SelectedValue = "No" Then
                                SUND_PRICE = FormatNumber(CDec(GridView1.Rows(I).Cells(13).Text) - (CDec(TextBox25.Text) + CDec(TextBox22.Text) + sd_order + tds_price + CDec(GridView1.Rows(I).Cells(19).Text) + CDec(GridView1.Rows(I).Cells(20).Text) + CDec(GridView1.Rows(I).Cells(21).Text) + CDec(GridView1.Rows(I).Cells(22).Text) + TDS_SGST + TDS_CGST + TDS_IGST), 2)
                            End If

                            GridView1.Rows(I).Cells(25).Text = FormatNumber((CDec(GridView1.Rows(I).Cells(12).Text) - CDec(GridView1.Rows(I).Cells(13).Text)) + (SUND_PRICE - CInt(SUND_PRICE)), 2)
                            'GridView210.Rows(I).Cells(31).Text = FormatNumber(CDec(net_price + freight_price + analitical_price - (penality_price) - CDec(GridView210.Rows(I).Cells(30).Text)), 2)
                            GridView1.Rows(I).Cells(26).Text = DropDownList2.SelectedValue
                            GridView1.Rows(I).Cells(32).Text = (SUND_PRICE)

                            ''SGST PERCENTAGE
                            GridView1.Rows(I).Cells(33).Text = TextBox14.Text
                            ''CGST PERCENTAGE
                            GridView1.Rows(I).Cells(34).Text = TextBox16.Text
                            ''IGST PERCENTAGE
                            GridView1.Rows(I).Cells(35).Text = TextBox18.Text
                            ''CESS PERCENTAGE
                            GridView1.Rows(I).Cells(36).Text = TextBox20.Text
                            ''TAXABLE VALUE
                            GridView1.Rows(I).Cells(37).Text = basic_price - disc_price + material_price

                            ''TDS ON GST
                            GridView1.Rows(I).Cells(38).Text = TDS_SGST
                            GridView1.Rows(I).Cells(39).Text = TDS_CGST
                            GridView1.Rows(I).Cells(40).Text = TDS_IGST
                        End If
                    Next

                End If

                myTrans.Commit()
                Label17.Visible = True
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label17.Visible = True
                Label17.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub DropDownList41_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList41.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then

        Else
            Dim supl_type As String = ""
            conn.Open()
            mycommand.CommandText = "select * from SUPL where SUPL_ID ='" & TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1) & "'"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                supl_type = dr.Item("SUPL_TYPE")
                dr.Close()
            Else
                conn.Close()
            End If
            conn.Close()
            'Dim sgst, cgst, igst, cess As New Decimal(0)
            Dim sgst, cgst, igst, cess, u_price_trailor As New Decimal(0)
            Dim w_unit As String = ""
            conn.Open()
            Dim mc As New SqlCommand
            mc.CommandText = "SELECT (select u_price_trailor from mb_book where po_no='" & Label12.Text & "'and wo_slno='" & DropDownList41.SelectedValue & "' and mb_no ='" & DropDownList1.SelectedValue & "') as u_price_trailor, (select pen_amt from mb_book where po_no='" & Label12.Text & "'and wo_slno='" & DropDownList41.SelectedValue & "' and mb_no ='" & DropDownList1.SelectedValue & "') as pen_amt,(select note from mb_book where po_no='" & Label12.Text & "'and wo_slno='" & DropDownList41.SelectedValue & "' and mb_no ='" & DropDownList1.SelectedValue & "') as pen_details, MAX(WO_AMD ) as WO_AMD, SUM(W_UNIT_PRICE ) AS W_UNIT_PRICE,SUM(W_DISCOUNT) AS W_DISCOUNT,SUM(W_MATERIAL_COST) AS W_MATERIAL_COST,max(s_tax_liability.cgst) as cgst,max(s_tax_liability.sgst) as sgst,max(s_tax_liability.igst) as igst,max(s_tax_liability.cess) as cess,(0.000) as tds, max(wo_order.W_AU) as W_AU FROM wo_order join ORDER_DETAILS on WO_ORDER .PO_NO =ORDER_DETAILS .SO_NO join s_tax_liability on ORDER_DETAILS .PO_TYPE  =s_tax_liability.taxable_service WHERE s_tax_liability .w_e_f =(select max(w_e_f) from s_tax_liability JOIN ORDER_DETAILS ON s_tax_liability .taxable_service =ORDER_DETAILS .PO_TYPE  where ORDER_DETAILS .SO_NO ='" & Label12.Text & "' AND w_e_f <= (select to_date  from mb_book where mb_no ='" & DropDownList1.SelectedValue & "' and po_no ='" & Label12.Text & "' and wo_slno ='" & DropDownList41.SelectedValue & "')) AND  WO_ORDER .PO_NO  ='" & Label12.Text & "' AND WO_ORDER .W_SLNO ='" & DropDownList41.SelectedValue & "' AND AMD_DATE<=(SELECT TO_DATE FROM MB_BOOK WHERE PO_NO='" & Label12.Text & "' AND WO_SLNO='" & DropDownList41.SelectedValue & "' AND MB_NO='" & DropDownList1.SelectedValue & "')"
            mc.Connection = conn
            dr = mc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If IsDBNull(dr.Item("W_UNIT_PRICE")) Then
                    Label17.Visible = True
                    Label17.Text = "Please select another work order sl. no."
                Else
                    Label17.Visible = False
                    Label17.Text = ""
                    TextBox7.Text = dr.Item("W_UNIT_PRICE")
                    TextBox9.Text = dr.Item("W_DISCOUNT")
                    sgst = dr.Item("sgst")
                    cgst = dr.Item("cgst")
                    igst = dr.Item("igst")
                    cess = dr.Item("cess")
                    TextBox24.Text = dr.Item("tds")
                    TextBox21.Text = dr.Item("pen_amt")
                    TextBox27.Text = dr.Item("pen_details")
                    w_unit = dr.Item("W_AU")
                End If


                If IsDBNull(dr.Item("u_price_trailor")) Then
                    u_price_trailor = TextBox7.Text
                Else
                    u_price_trailor = dr.Item("u_price_trailor")
                End If

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            If supl_type = "Within State" Then
                igst = FormatNumber(0, 3)
            Else
                cgst = FormatNumber(0, 3)
                sgst = FormatNumber(0, 3)
            End If
            ''SD CALCULATATION
            Dim sd_order As Decimal = 0.0
            conn.Open()
            Dim MC9 As New SqlCommand
            MC9.CommandText = "select SD_AMOUNT from ORDER_DETAILS where so_no= '" & Label12.Text & "'"
            MC9.Connection = conn
            dr = MC9.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox4.Text = dr.Item("SD_AMOUNT")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If


            TextBox13.Text = sgst
            TextBox15.Text = cgst
            TextBox17.Text = igst
            TextBox19.Text = cess

            If (w_unit = "Vehicle") Then
                TextBox8.Text = u_price_trailor
            Else
                TextBox8.Text = TextBox7.Text
            End If

            TextBox10.Text = TextBox9.Text
            TextBox14.Text = TextBox13.Text
            TextBox16.Text = TextBox15.Text
            TextBox18.Text = TextBox17.Text
            TextBox20.Text = TextBox19.Text
            TextBox22.Text = TextBox21.Text
            TextBox151.Text = TextBox121.Text

        End If

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            'DropDownList1.Focus()
            TextBox174.Text = ""
            TextBox175.Text = ""
            TextBox174.Visible = True
            TextBox175.Visible = True

        Else
            DropDownList41.SelectedValue = "Select"
            TextBox174.Visible = False
            TextBox175.Visible = False
        End If
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'check
        If TextBox160.Text = "" Then
            TextBox160.Focus()
            Return
        ElseIf IsDate(TextBox161.Text) = False Then
            TextBox161.Focus()
            Return
        ElseIf GridView1.Rows.Count = 0 Then
            DropDownList41.Focus()
            Return
        End If
        Dim flag As Boolean
        flag = False

        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                If chkRow.Checked Then
                    flag = True
                End If
            End If
        Next

        If (flag = False) Then
            Label17.Visible = True
            Label17.Text = "Please select atleaset one row"
            Return
        Else
            Panel4.Visible = True
            Label17.Visible = False
            Label17.Text = ""
        End If

    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date
                Dim flag As Boolean
                flag = False
                If TextBox1.Text = "" Then
                    TextBox1.Focus()
                    Return
                ElseIf IsDate(TextBox1.Text) = False Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                End If
                working_date = CDate(TextBox1.Text)
                If TextBox6.Text = "" Then
                    TextBox6.Focus()
                    Label17.Visible = True
                    Label17.Text = "Please enter password"
                    Return
                End If

                Label17.Visible = False

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

                '''''''''''''''''''''''''''''''''
                ''Checking Valuation date and Freeze date
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
                    Label20.Visible = True
                    Label20.Text = "Valuation before " & Block_DATE & " has been freezed."
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
                    End If
                    conn.Close()
                    If password = TextBox6.Text Then
                        Dim TAXABLE_AMOUNT, RCM_SGST, RCM_CGST, RCM_IGST, RCM_CESS, PEN_PRICE, PROV_PRICE, SUND_PRICE, LD_PRICE, TDS_PRICE, SD_PRICE, PSC_PRICE, sgst_price, cgst_price, igst_price, cess_price, sgst_liab, cgst_liab, igst_liab, cess_liab, TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0)
                        For Each row As GridViewRow In GridView1.Rows
                            If row.RowType = DataControlRowType.DataRow Then
                                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                                If chkRow.Checked Then
                                    flag = True
                                    Dim AC_PUR As New String("")
                                    If (Left(row.Cells(1).Text, 2) = "OS" Or Left(row.Cells(1).Text, 2) = "DC") Then
                                        AC_PUR = "71012"
                                    Else
                                        ''Getting PUR HEAD for raw material

                                        conn.Open()
                                        MC.CommandText = "SELECT AC_PUR FROM PO_RCD_MAT P1 JOIN MATERIAL M1 ON P1.MAT_CODE=M1.MAT_CODE WHERE CRR_NO='" & row.Cells(1).Text & "'"
                                        MC.Connection = conn
                                        dr = MC.ExecuteReader
                                        If dr.HasRows Then
                                            dr.Read()
                                            AC_PUR = dr.Item("AC_PUR")
                                            dr.Close()
                                            conn.Close()
                                        Else
                                            conn.Close()
                                        End If
                                    End If

                                    'update mb book
                                    Dim cmd As New SqlCommand
                                    Dim Query_1 As String = "update mb_book set GST_STATUS=@GST_STATUS,Valuation_Date=@Valuation_Date,fiscal_year=@fiscal_year,valuation_amt=@valuation_amt,rcm_sgst=@rcm_sgst,rcm_cgst=@rcm_cgst,rcm_igst=@rcm_igst,rcm_cess=@rcm_cess,ld=@ld, rcm=@rcm, sgst=@sgst,cgst=@cgst ,igst=@igst,cess=@cess,sgst_liab=@sgst_liab,cgst_liab=@cgst_liab,igst_liab=@igst_liab,cess_liab=@cess_liab,inv_no=@inv_no,inv_date=@inv_date,prov_amt=@prov_amt,pen_amt=@pen_amt,it_amt=@it_amt,pay_ind=@pay_ind,v_ind=@v_ind,mat_rate=@mat_rate WHERE po_no ='" & Label12.Text & "' AND wo_slno =" & CDec(row.Cells(4).Text) & " AND mb_no  ='" & row.Cells(1).Text & "'  AND v_ind IS NULL"
                                    cmd = New SqlCommand(Query_1, conn_trans, myTrans)
                                    cmd.Parameters.AddWithValue("@inv_no", TextBox160.Text)
                                    cmd.Parameters.AddWithValue("@inv_date", Date.ParseExact(TextBox161.Text, "dd-MM-yyyy", provider))
                                    cmd.Parameters.AddWithValue("@prov_amt", CDec(row.Cells(12).Text))
                                    cmd.Parameters.AddWithValue("@pen_amt", CDec(row.Cells(14).Text))
                                    cmd.Parameters.AddWithValue("@sgst", CDec(row.Cells(15).Text))
                                    cmd.Parameters.AddWithValue("@cgst", CDec(row.Cells(16).Text))
                                    cmd.Parameters.AddWithValue("@igst", CDec(row.Cells(17).Text))
                                    cmd.Parameters.AddWithValue("@cess", CDec(row.Cells(18).Text))
                                    cmd.Parameters.AddWithValue("@sgst_liab", CDec(row.Cells(19).Text))
                                    cmd.Parameters.AddWithValue("@cgst_liab", CDec(row.Cells(20).Text))
                                    cmd.Parameters.AddWithValue("@igst_liab", CDec(row.Cells(21).Text))
                                    cmd.Parameters.AddWithValue("@cess_liab", CDec(row.Cells(22).Text))
                                    cmd.Parameters.AddWithValue("@rcm_sgst", CDec(row.Cells(28).Text))
                                    cmd.Parameters.AddWithValue("@rcm_cgst", CDec(row.Cells(29).Text))
                                    cmd.Parameters.AddWithValue("@rcm_igst", CDec(row.Cells(30).Text))
                                    cmd.Parameters.AddWithValue("@rcm_cess", CDec(row.Cells(31).Text))
                                    cmd.Parameters.AddWithValue("@ld", CDec(row.Cells(27).Text))
                                    cmd.Parameters.AddWithValue("@it_amt", CDec(row.Cells(23).Text))
                                    cmd.Parameters.AddWithValue("@mat_rate", 0)
                                    cmd.Parameters.AddWithValue("@pay_ind", "")
                                    cmd.Parameters.AddWithValue("@v_ind", "V")
                                    cmd.Parameters.AddWithValue("@Valuation_Date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                    cmd.Parameters.AddWithValue("@rcm", row.Cells(26).Text)
                                    cmd.Parameters.AddWithValue("@valuation_amt", CDec(row.Cells(13).Text))
                                    cmd.Parameters.AddWithValue("@fiscal_year", STR1)
                                    If (DropDownList7.SelectedValue = "Yes") Then
                                        cmd.Parameters.AddWithValue("@GST_STATUS", "PAID")
                                    Else
                                        cmd.Parameters.AddWithValue("@GST_STATUS", "PENDING")
                                    End If
                                    cmd.ExecuteReader()
                                    cmd.Dispose()


                                    ''''''''''''''''''''''''''''''''''''''''''''

                                    PROV_PRICE = (CDec(row.Cells(12).Text))
                                    SUND_PRICE = (CDec(row.Cells(13).Text))
                                    sgst_price = (CDec(row.Cells(15).Text))
                                    cgst_price = (CDec(row.Cells(16).Text))
                                    igst_price = (CDec(row.Cells(17).Text))
                                    cess_price = (CDec(row.Cells(18).Text))
                                    sgst_liab = CDec(row.Cells(19).Text)
                                    cgst_liab = (CDec(row.Cells(20).Text))
                                    igst_liab = (CDec(row.Cells(21).Text))
                                    cess_liab = (CDec(row.Cells(22).Text))

                                    'RCM
                                    RCM_SGST = CDec(row.Cells(28).Text)
                                    RCM_CGST = (CDec(row.Cells(29).Text))
                                    RCM_IGST = (CDec(row.Cells(30).Text))
                                    RCM_CESS = (CDec(row.Cells(31).Text))
                                    PEN_PRICE = (CDec(row.Cells(14).Text))
                                    LD_PRICE = (CDec(row.Cells(27).Text))
                                    TDS_PRICE = CDec(row.Cells(23).Text)
                                    SD_PRICE = CDec(row.Cells(24).Text)
                                    PSC_PRICE = CDec(row.Cells(25).Text)

                                    TDS_SGST = CDec(row.Cells(38).Text)
                                    TDS_CGST = CDec(row.Cells(39).Text)
                                    TDS_IGST = CDec(row.Cells(40).Text)
                                    TAXABLE_AMOUNT = CDec(row.Cells(37).Text)

                                    If DropDownList7.SelectedValue = "Yes" Then
                                        SUND_PRICE = (SUND_PRICE + sgst_price + cgst_price + igst_price + cess_price) - (LD_PRICE + PEN_PRICE + SD_PRICE + TDS_PRICE + sgst_liab + cgst_liab + igst_liab + cess_liab + TDS_SGST + TDS_CGST + TDS_IGST)
                                    ElseIf DropDownList7.SelectedValue = "No" Then
                                        SUND_PRICE = (SUND_PRICE) - (LD_PRICE + PEN_PRICE + SD_PRICE + TDS_PRICE + sgst_liab + cgst_liab + igst_liab + cess_liab + TDS_SGST + TDS_CGST + TDS_IGST)
                                    End If


                                    SUND_PRICE = CInt(SUND_PRICE)


                                    ''SEARCH AC HEAD
                                    Dim PROV_HEAD, SUND_HEAD, SGST_HEAD, CGST_HEAD, IGST_HEAD, CESS_HEAD, SGST_LIAB_HEAD, CGST_LIAB_HEAD, IGST_LIAB_HEAD, CESS_LIAB_HEAD, LD_HEAD, TDS_HEAD, SD_HEAD, PSC_HEAD, TDS_SGST_HEAD, TDS_CGST_HEAD, TDS_IGST_HEAD As New String("")
                                    Dim SGST_LIAB_LD_PENALTY, CGST_LIAB_LD_PENALTY, IGST_LIAB_LD_PENALTY, CESS_LIAB_LD_PENALTY, IGST_WITHOUT_RCM, SGST_WITHOUT_RCM, CGST_WITHOUT_RCM, CESS_WITHOUT_RCM As New String("")

                                    conn.Open()
                                    Dim MC6 As New SqlCommand
                                    MC6.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & Label12.Text & "')"
                                    MC6.Connection = conn
                                    dr = MC6.ExecuteReader
                                    If dr.HasRows Then
                                        dr.Read()
                                        PROV_HEAD = dr.Item("prov_head")
                                        SUND_HEAD = dr.Item("sund_head")
                                        SGST_HEAD = dr.Item("sgst")
                                        CGST_HEAD = dr.Item("cgst")
                                        IGST_HEAD = dr.Item("igst")
                                        CESS_HEAD = dr.Item("cess")
                                        SGST_LIAB_HEAD = dr.Item("lsgst")
                                        CGST_LIAB_HEAD = dr.Item("lcgst")
                                        IGST_LIAB_HEAD = dr.Item("ligst")
                                        CESS_LIAB_HEAD = dr.Item("lcess")
                                        LD_HEAD = dr.Item("ld_head")
                                        TDS_HEAD = dr.Item("tds_head")
                                        SD_HEAD = dr.Item("sd_head")
                                        PSC_HEAD = dr.Item("psc_head")
                                        SGST_LIAB_LD_PENALTY = dr.Item("sgst_liab_ld_pen")
                                        CGST_LIAB_LD_PENALTY = dr.Item("cgst_liab_ld_pen")
                                        IGST_LIAB_LD_PENALTY = dr.Item("igst_liab_ld_pen")
                                        CESS_LIAB_LD_PENALTY = dr.Item("cess_liab_ld_pen")

                                        IGST_WITHOUT_RCM = dr.Item("IGST_WITHOUT_RCM")
                                        CGST_WITHOUT_RCM = dr.Item("CGST_WITHOUT_RCM")
                                        SGST_WITHOUT_RCM = dr.Item("SGST_WITHOUT_RCM")

                                        TDS_SGST_HEAD = dr.Item("TDS_SGST")
                                        TDS_CGST_HEAD = dr.Item("TDS_CGST")
                                        TDS_IGST_HEAD = dr.Item("TDS_IGST")

                                        dr.Close()
                                        conn.Close()
                                    Else
                                        conn.Close()
                                    End If
                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), PROV_HEAD, "Dr", PROV_PRICE, "PROV", DropDownList40.Text, 1, "")



                                    If DropDownList2.SelectedValue = "Yes" Then

                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), SGST_HEAD, "Dr", sgst_price, "SGST", DropDownList40.Text, 2, "")
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), CGST_HEAD, "Dr", cgst_price, "CGST", DropDownList40.Text, 2, "")
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), IGST_HEAD, "Dr", igst_price, "IGST", DropDownList40.Text, 2, "")
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), CESS_HEAD, "Dr", cess_price, "CESS", DropDownList40.Text, 2, "")
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), SGST_LIAB_HEAD, "Cr", sgst_liab, "SGST_LIAB", DropDownList40.Text, 3, "")
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), CGST_LIAB_HEAD, "Cr", cgst_liab, "CGST_LIAB", DropDownList40.Text, 3, "")
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), IGST_LIAB_HEAD, "Cr", igst_liab, "IGST_LIAB", DropDownList40.Text, 3, "")
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), CESS_LIAB_HEAD, "Cr", cess_liab, "CESS_LIAB", DropDownList40.Text, 3, "")

                                    Else
                                        If DropDownList7.SelectedValue = "Yes" Then
                                            save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), SGST_WITHOUT_RCM, "Dr", sgst_price, "SGST", DropDownList40.Text, 2, "")
                                            save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), CGST_WITHOUT_RCM, "Dr", cgst_price, "CGST", DropDownList40.Text, 2, "")
                                            save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), IGST_WITHOUT_RCM, "Dr", igst_price, "IGST", DropDownList40.Text, 2, "")
                                        End If



                                    End If
                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), SD_HEAD, "Cr", SD_PRICE, "SD", DropDownList40.Text, 4, "")
                                    'save_ledger(Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), LD_HEAD, "Cr", LD_PRICE + PEN_PRICE, "PENALITY_LD", DropDownList40.Text, 6, "")
                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), LD_HEAD, "Cr", LD_PRICE, "LD", DropDownList40.Text, 5, "")
                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), AC_PUR, "Cr", PEN_PRICE, "SHORTAGE PENALITY", DropDownList40.Text, 6, "")
                                    If (CDec(TextBox63.Text) > 0) Then
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), "51704", "Cr", TDS_PRICE, "TDS_194(O)", DropDownList40.Text, 7, "")
                                    Else
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), TDS_HEAD, "Cr", TDS_PRICE, "IT", DropDownList40.Text, 7, "")
                                    End If

                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), SUND_HEAD, "Cr", SUND_PRICE, "SUND", DropDownList40.Text, 12, "")

                                    ''TDS ON GST
                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), TDS_SGST_HEAD, "Cr", TDS_SGST, "TDS_SGST", DropDownList40.Text, 8, "")
                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), TDS_CGST_HEAD, "Cr", TDS_CGST, "TDS_CGST", DropDownList40.Text, 8, "")
                                    save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), TDS_IGST_HEAD, "Cr", TDS_IGST, "TDS_IGST", DropDownList40.Text, 8, "")

                                    ''psc calculation
                                    If PSC_PRICE > 0 Then
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), PSC_HEAD, "Cr", PSC_PRICE, "PSC", DropDownList40.Text, 8, "")
                                    ElseIf PSC_PRICE < 0 Then
                                        Dim DIFF As Decimal = 0.0
                                        DIFF = PSC_PRICE * (-1)
                                        save_ledger(row.Cells(4).Text, Label12.Text, row.Cells(1).Text, TextBox160.Text, Label16.Text.Substring(0, Label16.Text.IndexOf(",") - 1), PSC_HEAD, "Dr", DIFF, "PSC", DropDownList40.Text, 8, "")
                                    End If
                                    ''update ledger

                                    Dim cmd11 As New SqlCommand
                                    Dim Query11 As String = "update LEDGER set PAYMENT_INDICATION ='P' where PO_NO='" & Label12.Text & "' and INVOICE_NO is null and POST_INDICATION ='PROV' AND GARN_NO_MB_NO ='" & row.Cells(1).Text & "'"
                                    cmd11 = New SqlCommand(Query11, conn_trans, myTrans)
                                    cmd11.ExecuteReader()
                                    cmd11.Dispose()

                                    ''insert party amount

                                    Dim query As String = "INSERT INTO PARTY_AMT (POST_TYPE,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@POST_TYPE,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                    Dim cmd_1 As New SqlCommand(query, conn_trans, myTrans)
                                    cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                                    cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                                    cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList40.SelectedValue)
                                    cmd_1.Parameters.AddWithValue("@ORDER_NO", Label12.Text)
                                    cmd_1.Parameters.AddWithValue("@GARN_MB_NO", row.Cells(1).Text)
                                    cmd_1.Parameters.AddWithValue("@AC_CODE", SUND_HEAD)
                                    cmd_1.Parameters.AddWithValue("@AMOUNT_DR", 0.0)
                                    cmd_1.Parameters.AddWithValue("@AMOUNT_CR", SUND_PRICE)
                                    cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", SUND_PRICE)
                                    cmd_1.Parameters.AddWithValue("@POST_TYPE", "SUND")
                                    cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                    cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                    cmd_1.ExecuteReader()
                                    cmd_1.Dispose()

                                    If SD_PRICE > 0 Then
                                        ''insert party amount

                                        query = "INSERT INTO PARTY_AMT (POST_TYPE,IND,AMOUNT_BAL,SUPL_CODE,SUPL_NAME,TOKEN_NO,ORDER_NO,GARN_MB_NO,AC_CODE,AMOUNT_DR,AMOUNT_CR,POST_DATE,EMP_ID)VALUES(@POST_TYPE,@IND,@AMOUNT_BAL,@SUPL_CODE,@SUPL_NAME,@TOKEN_NO,@ORDER_NO,@GARN_MB_NO,@AC_CODE,@AMOUNT_DR,@AMOUNT_CR,@POST_DATE,@EMP_ID)"
                                        cmd_1 = New SqlCommand(query, conn_trans, myTrans)
                                        cmd_1.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                                        cmd_1.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                                        cmd_1.Parameters.AddWithValue("@TOKEN_NO", DropDownList40.SelectedValue)
                                        cmd_1.Parameters.AddWithValue("@ORDER_NO", Label12.Text)
                                        cmd_1.Parameters.AddWithValue("@GARN_MB_NO", row.Cells(1).Text)
                                        cmd_1.Parameters.AddWithValue("@AC_CODE", SD_HEAD)
                                        cmd_1.Parameters.AddWithValue("@AMOUNT_DR", 0.0)
                                        cmd_1.Parameters.AddWithValue("@AMOUNT_CR", SD_PRICE)
                                        cmd_1.Parameters.AddWithValue("@AMOUNT_BAL", SD_PRICE)
                                        cmd_1.Parameters.AddWithValue("@POST_TYPE", "SD")
                                        cmd_1.Parameters.AddWithValue("@IND", "P")
                                        cmd_1.Parameters.AddWithValue("@POST_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                                        cmd_1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                                        cmd_1.ExecuteReader()
                                        cmd_1.Dispose()

                                    End If



                                    Panel4.Visible = False
                                    ''dropdownlist clear
                                    conn.Open()
                                    dt.Clear()
                                    da = New SqlDataAdapter("SELECT DISTINCT MB_NO FROM mb_book WITH(NOLOCK) WHERE v_ind IS NULL and mb_clear = 'I.R. CLEAR' AND po_no ='" & TextBox171.Text & "' ORDER BY MB_NO", conn)
                                    da.Fill(dt)

                                    conn.Close()
                                    DropDownList1.DataSource = dt
                                    DropDownList1.DataValueField = "mb_no"
                                    DropDownList1.Items.Add("Select")
                                    DropDownList1.Items.FindByText("Select").Selected = True
                                    DropDownList1.DataBind()

                                    ''gridview clear
                                    Dim DT11 As New DataTable
                                    DT11.Columns.AddRange(New DataColumn(17) {New DataColumn("mb_no"), New DataColumn("mb_date"), New DataColumn("po_no"), New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("w_au"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty"), New DataColumn("prov_amt"), New DataColumn("pen_amt"), New DataColumn("st_amt_r"), New DataColumn("st_amt_p"), New DataColumn("wct_amt"), New DataColumn("it_amt"), New DataColumn("mat_rate")})
                                    GridView1.DataSource = DT11
                                    GridView1.DataBind()
                                    TextBox174.Visible = True
                                    TextBox175.Visible = True

                                    If ((CDec(row.Cells(33).Text) + CDec(row.Cells(34).Text) + CDec(row.Cells(35).Text)) > 0) Then

                                        ''INSERT DATA INTO TAXABLE VALUES TABLE
                                        Query_1 = "INSERT INTO Taxable_Values (GST_STATUS,GST_PAYMENT_DATE,GST_PAYMENT_VOUCHER_NO,INVOICE_NO,INVOICE_DATE,ENTRY_DATE,RCM_CGST_AMT,RCM_SGST_AMT,RCM_IGST_AMT,RCM_CESS_AMT,GARN_CRR_MB_NO,VALUATION_DATE,DATA_TYPE,SL_NO,SUPL_CODE,SUPL_NAME,TAXABLE_VALUE,FISCAL_YEAR,CGST_PERCENTAGE,SGST_PERCENTAGE,IGST_PERCENTAGE,CESS_PERCENTAGE,CGST_AMT,SGST_AMT,IGST_AMT,CESS_AMT,TAXABLE_LD_PENALTY,CGST_LD_PENALTY,SGST_LD_PENALTY,IGST_LD_PENALTY,CESS_LD_PENALTY,CGST_TDS,SGST_TDS,IGST_TDS,CESS_TDS)VALUES(@GST_STATUS,@GST_PAYMENT_DATE,@GST_PAYMENT_VOUCHER_NO,@INVOICE_NO,@INVOICE_DATE,@ENTRY_DATE,@RCM_CGST_AMT,@RCM_SGST_AMT,@RCM_IGST_AMT,@RCM_CESS_AMT,@GARN_CRR_MB_NO,@VALUATION_DATE,@DATA_TYPE,@SL_NO,@SUPL_CODE,@SUPL_NAME,@TAXABLE_VALUE,@FISCAL_YEAR,@CGST_PERCENTAGE,@SGST_PERCENTAGE,@IGST_PERCENTAGE,@CESS_PERCENTAGE,@CGST_AMT,@SGST_AMT,@IGST_AMT,@CESS_AMT,@TAXABLE_LD_PENALTY,@CGST_LD_PENALTY,@SGST_LD_PENALTY,@IGST_LD_PENALTY,@CESS_LD_PENALTY,@CGST_TDS,@SGST_TDS,@IGST_TDS,@CESS_TDS)"
                                        cmd = New SqlCommand(Query_1, conn_trans, myTrans)
                                        cmd.Parameters.AddWithValue("@INVOICE_NO", TextBox160.Text)
                                        cmd.Parameters.AddWithValue("@INVOICE_DATE", Date.ParseExact(TextBox161.Text, "dd-MM-yyyy", provider))
                                        cmd.Parameters.AddWithValue("@GARN_CRR_MB_NO", row.Cells(1).Text)
                                        cmd.Parameters.AddWithValue("@VALUATION_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                                        cmd.Parameters.AddWithValue("@DATA_TYPE", "TRANSPORT")
                                        cmd.Parameters.AddWithValue("@SL_NO", CDec(row.Cells(4).Text))
                                        cmd.Parameters.AddWithValue("@SUPL_CODE", TextBox168.Text.Substring(0, TextBox168.Text.IndexOf(",") - 1))
                                        cmd.Parameters.AddWithValue("@SUPL_NAME", TextBox168.Text.Substring(TextBox168.Text.IndexOf(",") + 2))
                                        cmd.Parameters.AddWithValue("@TAXABLE_VALUE", CDec(row.Cells(37).Text))
                                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                                        cmd.Parameters.AddWithValue("@SGST_PERCENTAGE", CDec(row.Cells(33).Text))
                                        cmd.Parameters.AddWithValue("@CGST_PERCENTAGE", CDec(row.Cells(34).Text))
                                        cmd.Parameters.AddWithValue("@IGST_PERCENTAGE", CDec(row.Cells(35).Text))
                                        cmd.Parameters.AddWithValue("@CESS_PERCENTAGE", CDec(row.Cells(36).Text))

                                        If (DropDownList2.SelectedValue = "Yes") Then
                                            cmd.Parameters.AddWithValue("@SGST_AMT", 0)
                                            cmd.Parameters.AddWithValue("@CGST_AMT", 0)
                                            cmd.Parameters.AddWithValue("@IGST_AMT", 0)
                                            cmd.Parameters.AddWithValue("@CESS_AMT", 0)
                                            cmd.Parameters.AddWithValue("@RCM_SGST_AMT", CDec(row.Cells(19).Text))
                                            cmd.Parameters.AddWithValue("@RCM_CGST_AMT", CDec(row.Cells(20).Text))
                                            cmd.Parameters.AddWithValue("@RCM_IGST_AMT", CDec(row.Cells(21).Text))
                                            cmd.Parameters.AddWithValue("@RCM_CESS_AMT", CDec(row.Cells(22).Text))
                                        Else
                                            cmd.Parameters.AddWithValue("@SGST_AMT", CDec(row.Cells(15).Text))
                                            cmd.Parameters.AddWithValue("@CGST_AMT", CDec(row.Cells(16).Text))
                                            cmd.Parameters.AddWithValue("@IGST_AMT", CDec(row.Cells(17).Text))
                                            cmd.Parameters.AddWithValue("@CESS_AMT", CDec(row.Cells(18).Text))
                                            cmd.Parameters.AddWithValue("@RCM_SGST_AMT", 0)
                                            cmd.Parameters.AddWithValue("@RCM_CGST_AMT", 0)
                                            cmd.Parameters.AddWithValue("@RCM_IGST_AMT", 0)
                                            cmd.Parameters.AddWithValue("@RCM_CESS_AMT", 0)
                                        End If

                                        cmd.Parameters.AddWithValue("@TAXABLE_LD_PENALTY", CDec(row.Cells(14).Text) + CDec(row.Cells(27).Text))
                                        cmd.Parameters.AddWithValue("@CGST_LD_PENALTY", CDec(row.Cells(28).Text))
                                        cmd.Parameters.AddWithValue("@SGST_LD_PENALTY", CDec(row.Cells(29).Text))
                                        cmd.Parameters.AddWithValue("@IGST_LD_PENALTY", CDec(row.Cells(30).Text))
                                        cmd.Parameters.AddWithValue("@CESS_LD_PENALTY", CDec(row.Cells(31).Text))
                                        cmd.Parameters.AddWithValue("@SGST_TDS", CDec(row.Cells(38).Text))
                                        cmd.Parameters.AddWithValue("@CGST_TDS", CDec(row.Cells(39).Text))
                                        cmd.Parameters.AddWithValue("@IGST_TDS", CDec(row.Cells(40).Text))
                                        cmd.Parameters.AddWithValue("@CESS_TDS", 0)
                                        cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                                        If (DropDownList7.SelectedValue = "Yes") Then
                                            cmd.Parameters.AddWithValue("@GST_STATUS", "PAID")
                                            cmd.Parameters.AddWithValue("@GST_PAYMENT_DATE", CDate(DateTime.Now.ToString("dd-MM-yyyy")))
                                            cmd.Parameters.AddWithValue("@GST_PAYMENT_VOUCHER_NO", "")
                                        Else
                                            cmd.Parameters.AddWithValue("@GST_STATUS", "PENDING")
                                            cmd.Parameters.AddWithValue("@GST_PAYMENT_DATE", DBNull.Value)
                                            cmd.Parameters.AddWithValue("@GST_PAYMENT_VOUCHER_NO", DBNull.Value)
                                        End If
                                        cmd.ExecuteReader()
                                        cmd.Dispose()

                                        ''''''''''''''''''''''''''''''''''''''''''''
                                    End If
                                End If
                            End If
                        Next

                        If (flag = False) Then
                            Label20.Text = "Please select atleaset one row"
                            Return
                        Else
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        End If
                    Else
                        Label20.Text = "Auth. Failed"
                        TextBox6.Text = ""
                        Return
                    End If
                End If

                myTrans.Commit()
                Label17.Visible = True
                Label17.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label17.Visible = True
                Label17.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedValue = "Yes" Then
            DropDownList7.Items.Clear()
            DropDownList7.Items.Insert(0, "No")
        Else
            DropDownList7.Items.Clear()
            DropDownList7.Items.Insert(0, "Yes")
            DropDownList7.Items.Insert(1, "No")
        End If
    End Sub

    Protected Sub DropDownList44_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList44.SelectedIndexChanged
        If DropDownList44.SelectedValue = "Yes" Then
            DropDownList5.Items.Clear()
            DropDownList5.Items.Insert(0, "No")
        Else
            DropDownList5.Items.Clear()
            DropDownList5.Items.Insert(0, "Yes")
            DropDownList5.Items.Insert(1, "No")
        End If
    End Sub

    Protected Sub DropDownList43_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList43.SelectedIndexChanged
        If DropDownList43.SelectedValue = "Yes" Then
            DropDownList4.Items.Clear()
            DropDownList4.Items.Insert(0, "No")
        Else
            DropDownList4.Items.Clear()
            DropDownList4.Items.Insert(0, "Yes")
            DropDownList4.Items.Insert(1, "No")
        End If
    End Sub

End Class