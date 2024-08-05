Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class f_goods
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
        If Not IsPostBack Then

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        If DropDownList26.Text = "" Then
            DropDownList26.Focus()
            Return
        End If
        If DropDownList26.Text.IndexOf(",") <> 14 Then
            DropDownList26.Text = ""
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
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, DELIVERY_TERM, dater_code As String
        order_type = ""
        po_type = ""
        SUPL_ID = ""
        SUPL_NAME = ""
        SO_DATE = ""
        freight_term = ""
        ORDER_TO = ""
        DELIVERY_TERM = ""
        dater_code = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select  ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.DELIVERY_TERM, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
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

        TextBox96.Text = ""
        TextBox97.Text = ""
        TextBox98.Text = ""
        TextBox99.Text = ""
        TextBox175.Text = ""
        TextBox125.Text = ""
        TextBox126.Text = ""
        TextBox75.Text = ""
        TextBox62.Text = ""
        TextBox174.Text = ""
        If order_type = "Sale Order" Then
            If po_type = "RAW MATERIAL" Or po_type = "STORE MATERIAL" Or po_type = "MISCELLANEOUS" Then
                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(7) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
                ViewState("despatch_raw") = dt7
                BINDGRID_RAW()
                ''SEARCH SALE ORDER VENDER DETAILS
                conn.Open()
                mc1.CommandText = "select dater.d_tax ,dater.d_code,dater.d_name from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox96.Text = dr.Item("d_code")
                    TextBox97.Text = dr.Item("d_name")
                    Label398.Text = dr.Item("d_tax")
                    dr.Close()
                End If
                conn.Close()
                TextBox123.Text = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim
                '' SERACH TRANSPORTATION DETAILS
                If DELIVERY_TERM = "By SRU" Then
                    DropDownList9.Enabled = True
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("SELECT DISTINCT (WO_ORDER .PO_NO + ' , ' + SUPL.SUPL_NAME)  AS PO_NO FROM ORDER_DETAILS JOIN WO_ORDER ON ORDER_DETAILS .SO_NO =WO_ORDER .PO_NO JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID WHERE ORDER_DETAILS .PO_TYPE ='FREIGHT OUTWARD' AND WO_ORDER.W_STATUS='PENDING' and WO_ORDER.W_END_DATE >'" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "' AND WO_ORDER.WO_TYPE='FREIGHT OUTWARD' ORDER BY PO_NO", conn)
                    da.Fill(dt)
                    conn.Close()
                    DropDownList9.Items.Clear()
                    DropDownList9.DataSource = dt
                    DropDownList9.DataValueField = "PO_NO"
                    DropDownList9.DataBind()
                    DropDownList9.Items.Add("Select")
                    DropDownList9.Items.Add("PARTY")
                    DropDownList9.SelectedValue = "Select"
                    DropDownList28.Enabled = True
                    DropDownList28.Items.Clear()
                Else
                    DropDownList9.Items.Clear()
                    DropDownList9.Items.Add("N/A")
                    DropDownList9.SelectedValue = "N/A"
                    DropDownList9.Enabled = False
                    DropDownList28.Items.Clear()
                    DropDownList28.Items.Add("N/A")
                    DropDownList28.SelectedValue = "N/A"
                    DropDownList28.Enabled = False
                End If
                ''SEARCH LINE NO DETAILS
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select distinct ITEM_SLNO from SO_MAT_ORDER where item_status='PENDING' and SO_NO='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'", conn)
                da.Fill(dt)
                conn.Close()
                DropDownList11.Items.Clear()
                DropDownList11.DataSource = dt
                DropDownList11.DataValueField = "ITEM_SLNO"
                DropDownList11.DataBind()
                DropDownList11.Items.Add("Select")
                DropDownList11.SelectedValue = "Select"
                Panel8.Visible = False
                Panel5.Visible = True
            ElseIf po_type = "FINISH GOODS" Then
                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(8) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("UNIT_WEIGHT"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
                ViewState("despatch") = dt7
                BINDGRID()
                ''SEARCH SALE ORDER VENDER DETAILS
                conn.Open()
                mc1.CommandText = "select dater.d_tax ,dater.d_code,dater.d_name from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox125.Text = dr.Item("d_code")
                    TextBox126.Text = dr.Item("d_name")
                    Label303.Text = dr.Item("d_tax")
                    dr.Close()
                End If
                conn.Close()
                TextBox124.Text = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim
                '' SERACH TRANSPORTATION DETAILS
                If DELIVERY_TERM = "By SRU" Then
                    DropDownList6.Enabled = True
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("SELECT DISTINCT (WO_ORDER .PO_NO + ' , ' + SUPL.SUPL_NAME) AS PO_NO FROM ORDER_DETAILS JOIN WO_ORDER ON ORDER_DETAILS .SO_NO =WO_ORDER .PO_NO JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID WHERE ORDER_DETAILS .PO_TYPE ='FREIGHT OUTWARD' AND WO_ORDER.W_STATUS='PENDING' and WO_ORDER.W_END_DATE >'" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "'AND WO_ORDER.WO_TYPE='FREIGHT OUTWARD' ORDER BY PO_NO", conn)
                    da.Fill(dt)
                    conn.Close()
                    DropDownList6.Items.Clear()
                    DropDownList6.DataSource = dt
                    DropDownList6.DataValueField = "PO_NO"
                    DropDownList6.DataBind()
                    DropDownList6.Items.Add("Select")
                    DropDownList6.Items.Add("PARTY")
                    DropDownList6.SelectedValue = "Select"
                    DropDownList27.Enabled = True
                    DropDownList27.Items.Clear()

                Else
                    DropDownList6.Items.Add("N/A")
                    DropDownList6.SelectedValue = "N/A"
                    DropDownList6.Enabled = False
                    DropDownList27.Items.Add("N/A")
                    DropDownList27.SelectedValue = "N/A"
                    DropDownList27.Enabled = False
                End If
                ''SEARCH LINE NO DETAILS
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select distinct (STR(ITEM_SLNO) + ' , ' + ITEM_VOCAB) AS ITEM_SLNO from SO_MAT_ORDER where item_status='PENDING' and ORD_AU <>'Activity' and ORD_AU <>'Service/Mt' and SO_NO='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'", conn)
                da.Fill(dt)
                conn.Close()
                DropDownList4.Items.Clear()
                DropDownList4.DataSource = dt
                DropDownList4.DataValueField = "ITEM_SLNO"
                DropDownList4.DataBind()
                DropDownList4.Items.Add("Select")
                DropDownList4.SelectedValue = "Select"
                Panel8.Visible = False
                Panel2.Visible = True
            End If
        End If
    End Sub
    Protected Sub BINDGRID()
        GridView2.DataSource = DirectCast(ViewState("despatch"), DataTable)
        GridView2.DataBind()
    End Sub
    Protected Sub BINDGRID_RAW()
        GridView4.DataSource = DirectCast(ViewState("despatch_raw"), DataTable)
        GridView4.DataBind()
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        If DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            Return
        ElseIf DropDownList6.SelectedValue = "PARTY" Then
            DropDownList27.Items.Clear()
            DropDownList27.Items.Add("N/A")
            DropDownList27.SelectedValue = "N/A"
            TextBox75.Text = ""
            TextBox174.Text = ""
            TextBox62.Text = TextBox126.Text
            Return
        ElseIf DropDownList6.SelectedValue <> "" Or DropDownList6.SelectedValue <> "PARTY" Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select supl.supl_id,supl.supl_name  from ORDER_DETAILS join supl on ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID  WHERE ORDER_DETAILS.SO_NO = '" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox75.Text = dr.Item("supl_id")
                TextBox62.Text = dr.Item("supl_name")
                dr.Close()
            End If
            conn.Close()
            ''work sl no put
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct (STR(W_SLNO) + ' , ' + W_NAME) AS W_SLNO from wo_order where po_no='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' AND WO_TYPE='FREIGHT OUTWARD'", conn)
            da.Fill(dt)
            DropDownList27.Items.Clear()
            DropDownList27.DataSource = dt
            DropDownList27.DataValueField = "w_slno"
            DropDownList27.DataBind()
            conn.Close()
            DropDownList27.Items.Add("Select")
            DropDownList27.SelectedValue = "Select"
            Return
        End If
    End Sub


    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        Dim working_date As Date
        working_date = Today.Date
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        End If
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select distinct ITEM_VOCAB from SO_MAT_ORDER where ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and SO_NO='" & TextBox124.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox53.Text = dr.Item("ITEM_VOCAB")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        ''SELECT AMD NO AND DATE
        conn.Open()
        Dim mc3 As New SqlCommand
        '' mc3.CommandText = "SELECT AMD_NO , AMD_DATE FROM SO_MAT_ORDER WHERE AMD_DATE=(SELECT MAX(AMD_DATE) FROM SO_MAT_ORDER WHERE ITEM_SLNO=" & DropDownList4.SelectedValue & " AND SO_NO='" & TextBox124.Text & "') AND SO_NO='" & TextBox124.Text & "' AND ITEM_SLNO=" & DropDownList4.SelectedValue
        mc3.CommandText = "SELECT MAX(AMD_NO) AS AMD_NO , MAX(AMD_DATE) AS AMD_DATE FROM SO_MAT_ORDER WHERE AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "' AND SO_NO='" & TextBox124.Text & "' AND ITEM_SLNO=" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim
        mc3.Connection = conn
        dr = mc3.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox67.Text = dr.Item("AMD_NO")
            TextBox68.Text = dr.Item("AMD_DATE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()




        ''SEARCH MATERIAL
        Dim MAT_TYPE As String = ""
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select PO_TYPE from ORDER_DETAILS where SO_NO='" & TextBox124.Text & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            MAT_TYPE = dr.Item("PO_TYPE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If MAT_TYPE = "FINISH GOODS" Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT ITEM_CODE  AS ITEM_CODE from SO_MAT_ORDER where item_status='PENDING' and SO_MAT_ORDER.SO_NO='" & TextBox124.Text & "' and SO_MAT_ORDER.ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList5.Items.Clear()
            DropDownList5.DataSource = dt
            DropDownList5.DataValueField = "ITEM_CODE"
            DropDownList5.DataBind()
            DropDownList5.Items.Add("Select")
            DropDownList5.SelectedValue = "Select"
        End If
    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(7) {New DataColumn("ITEM_CODE"), New DataColumn("ITEM_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_WEIGHT"), New DataColumn("ITEM_QTY"), New DataColumn("ITEM_BAL_QTY"), New DataColumn("ITEM_B_STOCK"), New DataColumn("ITEM_B_STOCK_MT")})
            ViewState("form_view") = dt2
            BINDGRID2()
            Return
        End If
        ''SEARCH MATERIAL
        Dim MAT_TYPE As String = ""
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select PO_TYPE from ORDER_DETAILS where SO_NO='" & TextBox124.Text & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            MAT_TYPE = dr.Item("PO_TYPE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If MAT_TYPE = "FINISH GOODS" Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE ,MAX(F_ITEM .ITEM_NAME) AS ITEM_NAME,MAX(F_ITEM .ITEM_AU) AS ITEM_AU,MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT,SUM(SO_MAT_ORDER .ITEM_QTY) AS ITEM_QTY,(SUM(SO_MAT_ORDER.ITEM_QTY)-SUM(SO_MAT_ORDER.ITEM_QTY_SEND)) AS ITEM_BAL_QTY,MAX(F_ITEM .ITEM_B_STOCK) AS ITEM_B_STOCK,convert(decimal(10,3) ,((MAX(F_ITEM .ITEM_B_STOCK)*MAX(SO_MAT_ORDER.ITEM_WEIGHT)) / 1000)) AS ITEM_B_STOCK_MT FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE = F_ITEM .ITEM_CODE where SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND SO_MAT_ORDER.SO_NO='" & TextBox124.Text & "' AND SO_MAT_ORDER.ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "'", conn)
            da.Fill(dt)
            conn.Close()
            ViewState("form_view") = dt
            BINDGRID2()
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select ITEM_AU,ITEM_NAME from F_ITEM where ITEM_CODE='" & DropDownList5.Text & "' "
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label289.Text = dr.Item("ITEM_AU")
                Label467.Text = dr.Item("ITEM_NAME")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            FormView1.Visible = True

        End If
    End Sub
    Protected Sub BINDGRID2()
        FormView1.DataSource = DirectCast(ViewState("form_view"), DataTable)
        FormView1.DataBind()
        FormView2.DataSource = DirectCast(ViewState("form_view"), DataTable)
        FormView2.DataBind()

    End Sub

    Protected Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim dt7 As New DataTable()
        dt7.Columns.AddRange(New DataColumn(8) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("UNIT_WEIGHT"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
        ViewState("despatch") = dt7
        BINDGRID()
        TextBox37.Text = "0.00"
        TextBox38.Text = "0.00"
        TextBox39.Text = "0.00"
        TextBox40.Text = "0.00"
        TextBox41.Text = "0.00"
        TextBox42.Text = "0.00"
        TextBox43.Text = "0.00"
        TextBox44.Text = "0.00"
        TextBox66.Text = "0.00"
        TextBox20.Text = "0.00"
        TextBox45.Text = "0.00"
        TextBox21.Text = "0.00"
    End Sub

    Protected Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Dim working_date As Date
       
        working_date = Today.Date
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        ElseIf TextBox55.Text = "" Then
            TextBox55.Focus()
            Return
        ElseIf IsNumeric(TextBox54.Text) = False Then
            TextBox54.Focus()
            Return

        End If
        If (Label289.Text = "Pcs" Or Label289.Text = "PCS") Then
            Dim output As Integer
            If Integer.TryParse(TextBox54.Text, output) = False Then
                TextBox54.Focus()
                Label308.Text = "Don't Enter Decimal Value In Qty"
                Return
            End If
        End If
        Dim COUNTER As Integer = 0
        For COUNTER = 0 To GridView2.Rows.Count - 1
            If DropDownList5.Text = GridView2.Rows(COUNTER).Cells(1).Text Then
                DropDownList5.Focus()
                Return
            End If
        Next
        ''SEARCH MATERIAL
        Dim MAT_TYPE As String = ""
        Dim PARTY_TYPE As String = ""
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select PO_TYPE,ORDER_TO from ORDER_DETAILS where SO_NO='" & TextBox124.Text & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            MAT_TYPE = dr.Item("PO_TYPE")
            PARTY_TYPE = dr.Item("ORDER_TO")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If MAT_TYPE = "FINISH GOODS" Then
            ''details search
            Dim pcs_qty, total_weight, ass_price, cas4_ass_parice As Decimal
            pcs_qty = 0
            total_weight = 0
            ass_price = 0.0
            cas4_ass_parice = 0.0
            Dim stock_qty As Decimal = 0.0
            Dim cas4_price, item_qty, unit_rate, discount, excise_duty, excise_cess, excise_hcess, pack_forwd, item_cst, item_tcs, terminal_tax, basic_ed, sess_ed, hsess_ed, qty_send, freight_rate, UNIT_WEIGHT As Decimal
            Dim freight_type As String = ""
            Dim ord_au As String = ""
            Dim DISC_TYPE As String = ""
            Dim PACK_TYPE As String = ""
            Dim ED_TYPE As String = ""
            cas4_price = 0
            unit_rate = 0
            discount = 0
            pack_forwd = 0
            item_cst = 0
            item_tcs = 0
            terminal_tax = 0
            basic_ed = 0
            sess_ed = 0
            hsess_ed = 0
            qty_send = 0
            item_qty = 0
            UNIT_WEIGHT = 0.0
            freight_rate = 0
            excise_duty = 0.0
            excise_cess = 0.0
            excise_hcess = 0.0
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select ITEM_WEIGHT,ITEM_B_STOCK from F_ITEM where ITEM_CODE='" & DropDownList5.Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                stock_qty = dr.Item("ITEM_B_STOCK")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            If stock_qty < CDec(TextBox54.Text) Then
                TextBox54.Focus()
                Label308.Text = "Qty is Higher Than Stock Qty"
                Return
            Else
                Label308.Text = ""
            End If
            ''item assessable value calculatation
            conn.Open()
            mc.CommandText = "select MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT, SUM(ITEM_EXCISE_DUTY) AS ITEM_EXCISE_DUTY,SUM(ITEM_SESS_DUTY) AS ITEM_SESS_DUTY,SUM(ITEM_HSESS_DUTY) AS ITEM_HSESS_DUTY,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE,MAX(SO_MAT_ORDER.ED_TYPE) AS ED_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK ,MAX(SO_MAT_ORDER .ITEM_TAXTYPE) AS ITEM_TAXTYPE,SUM(SO_MAT_ORDER .ITEM_CST) AS ITEM_CST ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU , " &
                             " (select CAS_4 .COST_VALUE  from F_ITEM join CAS_4 on F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where ITEM_CODE ='" & DropDownList5.Text & "' and CAS_4 .EFECTIVE_DATE =(select max(CAS_4 .EFECTIVE_DATE)  from CAS_4 join F_ITEM on F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where F_ITEM .ITEM_CODE ='" & DropDownList5.Text & "' and CAS_4 .EFECTIVE_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "')) as COST_VALUE, " &
                             " MAX(CHPTR_HEADING.TAX_VALUE) AS TAX_VALUE ,MAX(CHPTR_HEADING.ED_SESS) AS ED_SESS ,MAX(CHPTR_HEADING.SHED_CESS) AS SHED_CESS   FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE JOIN CHPTR_HEADING on F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and SO_MAT_ORDER .SO_NO='" & TextBox124.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "'"
            mc.Connection = conn
            dr = mc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                ord_au = dr.Item("ORD_AU")
                UNIT_WEIGHT = dr.Item("ITEM_WEIGHT")
                unit_rate = dr.Item("ITEM_UNIT_RATE")
                discount = dr.Item("ITEM_DISCOUNT")
                pack_forwd = dr.Item("ITEM_PACK")
                basic_ed = dr.Item("TAX_VALUE")
                sess_ed = dr.Item("ED_SESS")
                hsess_ed = dr.Item("SHED_CESS")
                Label303.Text = dr.Item("ITEM_TAXTYPE")
                item_cst = dr.Item("ITEM_CST")
                terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
                item_tcs = dr.Item("ITEM_TCS")
                qty_send = dr.Item("ITEM_QTY_SEND")
                item_qty = dr.Item("ITEM_QTY")
                freight_type = dr.Item("ITEM_FREIGHT_TYPE")
                freight_rate = dr.Item("ITEM_FREIGHT_PU")
                cas4_price = dr.Item("COST_VALUE")
                DISC_TYPE = dr.Item("DISC_TYPE")
                PACK_TYPE = dr.Item("PACK_TYPE")
                ED_TYPE = dr.Item("ED_TYPE")
                excise_duty = dr.Item("ITEM_EXCISE_DUTY")
                excise_cess = dr.Item("ITEM_SESS_DUTY")
                excise_hcess = dr.Item("ITEM_HSESS_DUTY")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            If (item_qty - qty_send) < CDec(TextBox54.Text) Then

                TextBox54.Focus()
                Label308.Text = "Qty is Higher Than Order Bal Qty"
                Return
            Else
                Label308.Text = ""
            End If
            If (ord_au = "Pcs" Or ord_au = "PCS") Then
                pcs_qty = CInt(TextBox54.Text)
                total_weight = FormatNumber((CInt(TextBox54.Text) * UNIT_WEIGHT) / 1000, 3)
                ass_price = CInt(TextBox54.Text) * unit_rate
                cas4_ass_parice = total_weight * cas4_price
            ElseIf ord_au = "Set" Then
                pcs_qty = CInt(TextBox54.Text)
                total_weight = FormatNumber((CDec(TextBox54.Text) * UNIT_WEIGHT) / 1000, 3)
                ass_price = total_weight * unit_rate
                cas4_ass_parice = total_weight * cas4_price
            ElseIf (ord_au = "Mt" Or ord_au = "MT" Or ord_au = "MTS") Then
                pcs_qty = 0
                total_weight = FormatNumber((CDec(TextBox54.Text) * UNIT_WEIGHT) / 1000, 3)
                ass_price = total_weight * unit_rate
                cas4_ass_parice = total_weight * cas4_price
            End If
            Dim row_count As Integer = GridView2.Rows.Count + 1
            Dim dt As DataTable = DirectCast(ViewState("despatch"), DataTable)
            dt.Rows.Add(row_count, DropDownList5.Text, Label467.Text, Label289.Text, pcs_qty, UNIT_WEIGHT, total_weight, TextBox56.Text & " " & TextBox57.Text, FormatNumber(ass_price, 2))
            ViewState("despatch") = dt
            BINDGRID()
            Dim weight As Decimal = 0.0
            Dim i As Integer = 0
            For i = 0 To GridView2.Rows.Count - 1
                weight = CDec(GridView2.Rows(i).Cells(6).Text) + weight
            Next
            ''tax calculatation

            Dim CKNT As Integer = 0
            Dim ASS_V As Decimal = 0.0
            For CKNT = 0 To GridView2.Rows.Count - 1
                ASS_V = ASS_V + CDec(GridView2.Rows(CKNT).Cells(8).Text)
            Next
            ''TERM TYPE DATA
            'ASS VALUE
            If PARTY_TYPE = "Other" Then
                TextBox38.Text = 0.0
            ElseIf PARTY_TYPE = "I.P.T." Then
                TextBox38.Text = FormatNumber((CDec(TextBox38.Text) + cas4_ass_parice), 2)
            End If

            ''DISCOUNT
            If DISC_TYPE = "PERCENTAGE" Then
                TextBox37.Text = FormatNumber(((CDec(ASS_V) * discount) / 100), 2)
            ElseIf DISC_TYPE = "PER UNIT" Then
                TextBox37.Text = FormatNumber(((CDec(TextBox54.Text) * discount)) + CDec(TextBox37.Text), 2)
            ElseIf DISC_TYPE = "PER MT" Then
                TextBox37.Text = FormatNumber(((CDec(total_weight) * discount)) + CDec(TextBox37.Text), 2)
            End If
            ''PACKING AND FORWD
            If PACK_TYPE = "PERCENTAGE" Then
                TextBox41.Text = FormatNumber(((CDec(ASS_V) * pack_forwd) / 100), 2)
            ElseIf PACK_TYPE = "PER UNIT" Then
                TextBox41.Text = FormatNumber(((CDec(TextBox54.Text) * pack_forwd)) + CDec(TextBox41.Text), 2)
            ElseIf PACK_TYPE = "PER MT" Then
                TextBox41.Text = FormatNumber(((CDec(total_weight) * pack_forwd)) + CDec(TextBox41.Text), 2)
            End If
            ''FREIGHT
            If DropDownList6.Text <> "PARTY" Then
                If freight_type = "PERCENTAGE" Then
                    TextBox39.Text = FormatNumber((((CDec(ASS_V) * freight_rate) / 100)), 2)
                ElseIf freight_type = "PER UNIT" Then
                    TextBox39.Text = FormatNumber(((CDec(TextBox54.Text) * freight_rate)) + CDec(TextBox39.Text), 2)
                ElseIf freight_type = "PER MT" Then
                    TextBox39.Text = FormatNumber(((total_weight * freight_rate)) + CDec(TextBox39.Text), 2)
                End If
            ElseIf DropDownList6.Text = "PARTY" Then
                TextBox39.Text = "0.00"
            End If






            ''BED
            If PARTY_TYPE = "Other" Then
                If ED_TYPE = "PERCENTAGE" Then
                    TextBox40.Text = FormatNumber(((CDec(ASS_V) * excise_duty) / 100), 2)
                    'CESS
                    TextBox42.Text = FormatNumber(CInt(CDec(TextBox42.Text) + (CDec(TextBox40.Text) * excise_cess) / 100), 2)
                    'HCESS
                    TextBox44.Text = FormatNumber(CInt(CDec(TextBox44.Text) + (CDec(TextBox40.Text) * excise_hcess) / 100), 2)
                ElseIf ED_TYPE = "PER UNIT" Then
                    TextBox40.Text = FormatNumber(((CDec(TextBox54.Text) * excise_duty)) + CDec(TextBox40.Text), 2)
                    'CESS
                    TextBox42.Text = "0.00"
                    'HCESS
                    TextBox44.Text = "0.00"
                ElseIf ED_TYPE = "PER MT" Then
                    TextBox40.Text = FormatNumber(((CDec(total_weight) * excise_duty)) + CDec(TextBox40.Text), 2)
                    'CESS
                    TextBox42.Text = "0.00"
                    'HCESS
                    TextBox44.Text = "0.00"
                End If
            ElseIf PARTY_TYPE = "I.P.T." Then
                If CheckBox1.Checked = True Then


                    If working_date < "2015-06-12" Then
                        If ED_TYPE = "PERCENTAGE" Then
                            TextBox40.Text = FormatNumber(CInt(((cas4_ass_parice + CDec(TextBox39.Text) + CDec(TextBox41.Text)) * basic_ed) / 100) + CDec(TextBox40.Text), 2)
                            'CESS
                            TextBox42.Text = FormatNumber(CInt((CDec(TextBox40.Text) * sess_ed) / 100), 2)
                            'HCESS
                            TextBox44.Text = FormatNumber(CInt((CDec(TextBox40.Text) * hsess_ed) / 100), 2)
                        ElseIf ED_TYPE = "PER UNIT" Then
                            TextBox40.Text = FormatNumber(((CDec(TextBox54.Text) * excise_duty)) + CDec(TextBox40.Text), 2)
                            'CESS
                            TextBox42.Text = "0.00"
                            'HCESS
                            TextBox44.Text = "0.00"
                        ElseIf ED_TYPE = "PER MT" Then
                            TextBox40.Text = FormatNumber(((CDec(total_weight) * excise_duty)) + CDec(TextBox40.Text), 2)
                            'CESS
                            TextBox42.Text = "0.00"
                            'HCESS
                            TextBox44.Text = "0.00"
                        End If
                    Else
                        If ED_TYPE = "PERCENTAGE" Then
                            TextBox40.Text = FormatNumber(CInt(((cas4_ass_parice) * basic_ed) / 100) + CDec(TextBox40.Text), 2)
                            'CESS
                            TextBox42.Text = FormatNumber(CInt(((CDec(TextBox40.Text)) * sess_ed) / 100), 2)
                            'HCESS
                            TextBox44.Text = FormatNumber(CInt((CDec(TextBox40.Text) * hsess_ed) / 100), 2)
                        ElseIf ED_TYPE = "PER UNIT" Then
                            TextBox40.Text = FormatNumber(((CDec(TextBox54.Text) * excise_duty)) + CDec(TextBox40.Text), 2)
                            'CESS
                            TextBox42.Text = "0.00"
                            'HCESS
                            TextBox44.Text = "0.00"
                        ElseIf ED_TYPE = "PER MT" Then
                            TextBox40.Text = FormatNumber(((CDec(total_weight) * excise_duty)) + CDec(TextBox40.Text), 2)
                            'CESS
                            TextBox42.Text = "0.00"
                            'HCESS
                            TextBox44.Text = "0.00"
                        End If
                    End If
                Else
                    TextBox40.Text = "0.00"
                    TextBox42.Text = "0.00"
                    'HCESS
                    TextBox44.Text = "0.00"
                End If
            End If
            ''VAT/CST VALUE
            TextBox20.Text = FormatNumber(((((CDec(ASS_V) - CDec(TextBox37.Text)) + CDec(TextBox40.Text) + CDec(TextBox41.Text) + CDec(TextBox44.Text)) * item_cst) / 100), 2)
            ''TCS VALUE
            TextBox66.Text = FormatNumber((((CDec(ASS_V) - CDec(TextBox37.Text)) + CDec(TextBox40.Text) + CDec(TextBox41.Text) + CDec(TextBox44.Text) + CDec(TextBox20.Text)) * item_tcs) / 100, 2)
            ''T TAX VALUE
            TextBox43.Text = FormatNumber((((CDec(ASS_V) - CDec(TextBox37.Text)) + CDec(TextBox39.Text) + CDec(TextBox40.Text) + CDec(TextBox41.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox20.Text) + CDec(TextBox66.Text)) * terminal_tax) / 100, 2)


            ''ROUNDOFF
            TextBox21.Text = CInt((CDec(ASS_V) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox20.Text) + CDec(TextBox39.Text) + CDec(TextBox41.Text) + CDec(TextBox43.Text) + CDec(TextBox66.Text)) - CDec(TextBox37.Text)) - ((CDec(ASS_V) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox20.Text) + CDec(TextBox39.Text) + CDec(TextBox41.Text) + CDec(TextBox43.Text) + CDec(TextBox66.Text)) - CDec(TextBox37.Text))
            ''TOTAL VALUE
            TextBox45.Text = FormatNumber(CInt((CDec(ASS_V) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox20.Text) + CDec(TextBox39.Text) + CDec(TextBox41.Text) + CDec(TextBox43.Text) + CDec(TextBox66.Text)) - CDec(TextBox37.Text)), 2)

        End If
        Button36.Enabled = True
    End Sub

    Protected Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Dim working_date As Date
        working_date = Today.Date
        If GridView2.Rows.Count = 0 Then
            Label308.Text = "Please add Material first"
            Return
        ElseIf DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            Return
        ElseIf DropDownList27.SelectedValue = "Select" Or DropDownList27.Text = "" Then
            DropDownList27.Focus()
            Return
        End If
        ''INVOICE NO GENERATE
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
        conn.Open()
        Dim inv_no As String = ""
        Dim mc_c As New SqlCommand
        mc_c.CommandText = "SELECT (CASE WHEN MAX(INV_NO) IS NULL THEN 0 ELSE MAX(INV_NO) END) as inv_no  FROM DESPATCH WHERE D_TYPE='D' AND FISCAL_YEAR =" & STR1
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
            TextBox65.ReadOnly = False
            If TextBox65.Text = "" Then
                TextBox65.Focus()
                Label308.Text = "Please enter Invoice No First"
                Return
            ElseIf IsNumeric(TextBox65.Text) = False Then
                TextBox65.Focus()
                Label308.Text = "Please enter Invoice No In Numeric value"
                Return
            End If
        Else
            str = CInt(inv_no) + 1
            If str.Length = 1 Then
                str = "0000" & CInt(inv_no) + 1
            ElseIf str.Length = 2 Then
                str = "000" & CInt(inv_no) + 1
            ElseIf str.Length = 3 Then
                str = "00" & CInt(inv_no) + 1
            ElseIf str.Length = 4 Then
                str = "0" & CInt(inv_no) + 1
            ElseIf str.Length = 5 Then
                str = CInt(inv_no) + 1

            End If
            TextBox65.Text = str
            TextBox65.ReadOnly = True
        End If
        ''DESPATCH NO GENERATE
        Dim transporter, trans_sl_no As String
        If DropDownList6.SelectedValue = "PARTY" Or DropDownList6.SelectedValue = "N/A" Then
            transporter = DropDownList6.Text.Trim
        Else
            transporter = DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim
        End If
        If DropDownList27.SelectedValue <> "N/A" Then
            trans_sl_no = DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim
        Else
            trans_sl_no = DropDownList27.Text.Trim
        End If
        ''SAVE DESPATCH
        ''sale order date excise comodity
        Dim so_date As Date
        Dim PARTY_CODE As String = ""
        Dim excise_comodity As String = ""
        Dim chptr_heading As String = ""
        Dim mode_of_despatch As String = ""
        Dim mode_of_payment As String = ""
        Dim qual_name As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PARTY_CODE,qual_group.qual_name , ORDER_DETAILS.PAYMENT_TERM,ORDER_DETAILS.MODE_OF_DESPATCH,F_ITEM .ITEM_CHPTR,CHPTR_HEADING.CHPT_NAME FROM SO_MAT_ORDER JOIN ORDER_DETAILS ON SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE JOIN CHPTR_HEADING ON F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE join qual_group on F_ITEM .ITEM_TYPE =qual_group.qual_code  WHERE SO_MAT_ORDER.ITEM_SLNO  ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' AND SO_MAT_ORDER .SO_NO ='" & TextBox124.Text.Trim & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            excise_comodity = dr.Item("CHPT_NAME")
            chptr_heading = dr.Item("ITEM_CHPTR")
            so_date = dr.Item("SO_DATE")
            PARTY_CODE = dr.Item("PARTY_CODE")
            mode_of_despatch = dr.Item("MODE_OF_DESPATCH")
            mode_of_payment = dr.Item("PAYMENT_TERM")
            qual_name = dr.Item("qual_name")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim weight As Decimal = 0.0
        Dim i As Integer = 0
        Dim ass_price As Decimal = 0.0
        For i = 0 To GridView2.Rows.Count - 1
            weight = CDec(GridView2.Rows(i).Cells(6).Text) + weight
            ass_price = ass_price + CDec(GridView2.Rows(i).Cells(8).Text)
        Next
        ''SAVE ITEM
        Dim i1 As Integer = 0
        For i1 = 0 To GridView2.Rows.Count - 1
            MAT_SAVE(TextBox65.Text, TextBox124.Text, CInt(GridView2.Rows(i1).Cells(0).Text), GridView2.Rows(i1).Cells(1).Text, GridView2.Rows(i1).Cells(2).Text, GridView2.Rows(i1).Cells(3).Text, GridView2.Rows(i1).Cells(7).Text, CInt(GridView2.Rows(i1).Cells(4).Text), CDec(GridView2.Rows(i1).Cells(5).Text), CDec(GridView2.Rows(i1).Cells(6).Text), CDec(GridView2.Rows(i1).Cells(8).Text), STR1, "D")
        Next

        ''item assessable value calculatation
        Dim cas4_price, item_qty, unit_rate, discount, pack_forwd, item_cst, item_tcs, terminal_tax, basic_ed, sess_ed, hsess_ed, qty_send, freight_rate As Decimal
        Dim freight_type, pack_type, actual_so As String
        actual_so = ""
        freight_type = ""
        pack_type = ""
        cas4_price = 0
        unit_rate = 0
        discount = 0
        pack_forwd = 0
        item_cst = 0
        item_tcs = 0
        terminal_tax = 0
        basic_ed = 0
        sess_ed = 0
        hsess_ed = 0
        qty_send = 0
        item_qty = 0
        freight_rate = 0
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT, SUM(ITEM_EXCISE_DUTY) AS ITEM_EXCISE_DUTY,SUM(ITEM_SESS_DUTY) AS ITEM_SESS_DUTY,SUM(ITEM_HSESS_DUTY) AS ITEM_HSESS_DUTY,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE,MAX(SO_MAT_ORDER.ED_TYPE) AS ED_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK ,MAX(SO_MAT_ORDER .ITEM_TAXTYPE) AS ITEM_TAXTYPE,SUM(SO_MAT_ORDER .ITEM_CST) AS ITEM_CST ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU , " &
                            " (select CAS_4 .COST_VALUE  from F_ITEM join CAS_4 on F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where ITEM_CODE ='" & DropDownList5.Text & "' and CAS_4 .EFECTIVE_DATE =(select max(CAS_4 .EFECTIVE_DATE)  from CAS_4 join F_ITEM on F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where F_ITEM .ITEM_CODE ='" & DropDownList5.Text & "' and CAS_4 .EFECTIVE_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "')) as COST_VALUE,  " &
                            " (select SO_ACTUAL  from ORDER_DETAILS where SO_NO ='" & TextBox124.Text & "') as actual_so ,   " &
                            "  MAX(CHPTR_HEADING.TAX_VALUE) AS TAX_VALUE ,MAX(CHPTR_HEADING.ED_SESS) AS ED_SESS ,MAX(CHPTR_HEADING.SHED_CESS) AS SHED_CESS   FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE JOIN CHPTR_HEADING on F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and SO_MAT_ORDER .SO_NO='" & TextBox124.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            unit_rate = dr.Item("ITEM_UNIT_RATE")
            discount = dr.Item("ITEM_DISCOUNT")
            pack_forwd = dr.Item("ITEM_PACK")
            basic_ed = dr.Item("TAX_VALUE")
            sess_ed = dr.Item("ED_SESS")
            hsess_ed = dr.Item("SHED_CESS")
            terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
            item_tcs = dr.Item("ITEM_TCS")
            qty_send = dr.Item("ITEM_QTY_SEND")
            item_qty = dr.Item("ITEM_QTY")
            freight_type = dr.Item("ITEM_FREIGHT_TYPE")
            freight_rate = dr.Item("ITEM_FREIGHT_PU")
            cas4_price = dr.Item("COST_VALUE")
            pack_type = dr.Item("PACK_TYPE")
            actual_so = dr.Item("actual_so")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If freight_type = "PERCNTAGE" Then
            freight_type = freight_rate & " %"
        Else
            freight_type = freight_rate & " /Mt"
        End If
        If pack_type = "PERCNTAGE" Then
            pack_type = pack_forwd & " %"
        Else
            pack_type = pack_forwd & " /Mt"
        End If
        ''data save for tax_desc
        ''assessable value Assble Val(As Per CAS 4)
        If TextBox38.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 1, "Assble Val(As Per CAS 4)", "", CDec(TextBox38.Text), STR1, "D")
        End If

        If TextBox38.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 2, "Assessable Value", "", ass_price, STR1, "D")
        End If

        'discount
        If TextBox37.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 3, "Discount", discount & " %", CDec(TextBox37.Text), STR1, "D")
        End If

        ''packing forwording
        If TextBox41.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 4, "Pack & Forwd", pack_forwd, CDec(TextBox41.Text), STR1, "D")
        End If

        ''basic ed
        If TextBox40.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 5, "Basic Excise Duty", basic_ed & " %", CDec(TextBox40.Text), STR1, "D")
        End If

        ''cess
        If TextBox42.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 6, "Cess", sess_ed & " %", CDec(TextBox42.Text), STR1, "D")
        End If

        ''hs cess
        If TextBox44.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 7, "Hs Cess", hsess_ed & " %", CDec(TextBox44.Text), STR1, "D")
        End If

        ''vat/cst
        If TextBox20.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 8, Label303.Text, item_cst & " %", CDec(TextBox20.Text), STR1, "D")
        End If

        ''terminal tax
        If TextBox43.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 9, "Terminal Tax", terminal_tax & " %", CDec(TextBox43.Text), STR1, "D")
        End If

        ''tcs 
        If TextBox66.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 10, "T.C.S ", item_tcs & " %", CDec(TextBox66.Text), STR1, "D")
        End If

        ''freight
        If TextBox39.Text > 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 11, "Freight", freight_type, CDec(TextBox39.Text), STR1, "D")
        End If

        ''roundoff
        If CDec(TextBox21.Text) <> 0 Then
            TAX_SAVE(TextBox65.Text, TextBox124.Text, 12, "Round Off", "", CDec(TextBox21.Text), STR1, "D")
        End If

        ''grand total
        TAX_SAVE(TextBox65.Text, TextBox124.Text, 13, "Grand Total", "", CDec(TextBox45.Text), STR1, "D")
        ''despatch save
        Dim inv_status As String = ""
        If transporter = "N/A" Or transporter = "PARTY" Then
            inv_status = ""
        Else
            inv_status = "Pending"
        End If



        conn.Open()
        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into DESPATCH(actual_so,D_TYPE,TRANS_SLNO,SO_NO,SO_DATE,AMD_NO,AMD_DATE,TRANS_WO,TRANS_NAME,TRUCK_NO,ED_COMDT,PARTY_CODE,CONSIGN_CODE,MAT_VOCAB,MAT_SLNO,INV_NO,INV_DATE,CHPTR_HEAD,FISCAL_YEAR,INV_ISSUE,DEBIT_ENTRY_NO,MODE_OF_DESPATCH,MODE_OF_PAYMENT,QUALITY,FORM_NAME,WAY_BILL,ED_VALUE,TOTAL_WEIGHT,ORD_UNIT_VALUE,CAS_4_UNIT_VALUE,DIFF_ED_VALUE,PAY_VALUE,INV_STATUS,LR_NO,NOTIFICATION_,EMP_ID1,EMP_ID2)values(@actual_so,@D_TYPE,@TRANS_SLNO,@SO_NO,@SO_DATE,@AMD_NO,@AMD_DATE,@TRANS_WO,@TRANS_NAME,@TRUCK_NO,@ED_COMDT,@PARTY_CODE,@CONSIGN_CODE,@MAT_VOCAB,@MAT_SLNO,@INV_NO,@INV_DATE,@CHPTR_HEAD,@FISCAL_YEAR,@INV_ISSUE,@DEBIT_ENTRY_NO,@MODE_OF_DESPATCH,@MODE_OF_PAYMENT,@QUALITY,@FORM_NAME,@WAY_BILL,@ED_VALUE,@TOTAL_WEIGHT,@ORD_UNIT_VALUE,@CAS_4_UNIT_VALUE,@DIFF_ED_VALUE,@PAY_VALUE,@INV_STATUS,@LR_NO,@NOTIFICATION_,@EMP_ID1,@EMP_ID2)"
        Dim cmd1 As New SqlCommand(QUARY1, conn)
        cmd1.Parameters.AddWithValue("@SO_NO", TextBox124.Text)
        cmd1.Parameters.AddWithValue("@actual_so", actual_so)
        cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@AMD_NO", TextBox67.Text)
        cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(TextBox68.Text, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@TRANS_WO", transporter)
        cmd1.Parameters.AddWithValue("@TRANS_SLNO", trans_sl_no)
        cmd1.Parameters.AddWithValue("@TRANS_NAME", TextBox62.Text)
        cmd1.Parameters.AddWithValue("@TRUCK_NO", TextBox55.Text)
        cmd1.Parameters.AddWithValue("@ED_COMDT", excise_comodity)
        cmd1.Parameters.AddWithValue("@PARTY_CODE", PARTY_CODE)
        cmd1.Parameters.AddWithValue("@CONSIGN_CODE", PARTY_CODE)
        cmd1.Parameters.AddWithValue("@MAT_VOCAB", TextBox53.Text)
        cmd1.Parameters.AddWithValue("@MAT_SLNO", DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim)
        cmd1.Parameters.AddWithValue("@INV_NO", TextBox65.Text)
        cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@CHPTR_HEAD", chptr_heading)
        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
        cmd1.Parameters.AddWithValue("@INV_ISSUE", Now)
        cmd1.Parameters.AddWithValue("@DEBIT_ENTRY_NO", TextBox63.Text)
        cmd1.Parameters.AddWithValue("@MODE_OF_DESPATCH", mode_of_despatch)
        cmd1.Parameters.AddWithValue("@MODE_OF_PAYMENT", mode_of_payment)
        cmd1.Parameters.AddWithValue("@QUALITY", qual_name)
        cmd1.Parameters.AddWithValue("@FORM_NAME", DropDownList7.SelectedValue)
        cmd1.Parameters.AddWithValue("@WAY_BILL", "")
        cmd1.Parameters.AddWithValue("@ED_VALUE", CInt(TextBox40.Text) + CInt(TextBox42.Text) + CInt(TextBox44.Text))
        cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT", weight)
        cmd1.Parameters.AddWithValue("@ORD_UNIT_VALUE", unit_rate)
        cmd1.Parameters.AddWithValue("@CAS_4_UNIT_VALUE", cas4_price)
        cmd1.Parameters.AddWithValue("@DIFF_ED_VALUE", 0)
        cmd1.Parameters.AddWithValue("@PAY_VALUE", CDec(TextBox45.Text))
        cmd1.Parameters.AddWithValue("@INV_STATUS", inv_status)
        cmd1.Parameters.AddWithValue("@LR_NO", TextBox69.Text)
        cmd1.Parameters.AddWithValue("@NOTIFICATION_ ", TextBox64.Text)
        cmd1.Parameters.AddWithValue("@D_TYPE", "D")
        cmd1.Parameters.AddWithValue("@EMP_ID1", Session("userName"))
        cmd1.Parameters.AddWithValue("@EMP_ID2", Session("userName"))
        cmd1.ExecuteReader()
        cmd1.Dispose()
        conn.Close()

        ''update sale order update f_item
        Dim lp As Integer = 0
        For lp = 0 To GridView2.Rows.Count - 1
            Dim sendqty As Decimal = 0
            If (GridView2.Rows(lp).Cells(3).Text = "Pcs" Or GridView2.Rows(lp).Cells(3).Text = "PCS") Then
                sendqty = CInt(GridView2.Rows(lp).Cells(4).Text)
            Else
                sendqty = CDec(GridView2.Rows(lp).Cells(6).Text)
            End If
            conn.Open()
            QUARY1 = ""
            QUARY1 = "update F_ITEM set ITEM_B_STOCK =ITEM_B_STOCK - " & sendqty & " ,ITEM_LAST_DESPATCH = @ITEM_LAST_DESPATCH where ITEM_CODE ='" & GridView2.Rows(lp).Cells(1).Text & "'"
            Dim cmd2 As New SqlCommand(QUARY1, conn)
            cmd2.Parameters.AddWithValue("@ITEM_LAST_DESPATCH", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
            cmd2.ExecuteReader()
            cmd2.Dispose()
            conn.Close()
            ''update so order
            conn.Open()
            QUARY1 = ""
            QUARY1 = "update SO_MAT_ORDER set ITEM_QTY_SEND =ITEM_QTY_SEND + " & sendqty & " where SO_NO ='" & TextBox124.Text & "' and ITEM_SLNO ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and ITEM_CODE ='" & GridView2.Rows(lp).Cells(1).Text & "' AND AMD_NO='" & TextBox67.Text & "'"
            Dim cmdd As New SqlCommand(QUARY1, conn)
            cmdd.ExecuteReader()
            cmdd.Dispose()
            conn.Close()


            ''insert prod controle
            conn.Open()
            Dim mcc As New SqlCommand
            Dim fr_stock, bsr_stock As Decimal
            fr_stock = 0
            bsr_stock = 0
            mcc.CommandText = "select (case when (ITEM_F_STOCK) is null then '0' else (ITEM_F_STOCK) end ) as fr_stock ,(case when (ITEM_B_STOCK) is null then '0' else (ITEM_B_STOCK) end ) as bsr_stock from F_ITEM where ITEM_CODE='" & GridView2.Rows(lp).Cells(1).Text & "' "
            mcc.Connection = conn
            dr = mcc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                fr_stock = dr.Item("fr_stock")
                bsr_stock = dr.Item("bsr_stock")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            conn.Open()
            QUARY1 = "Insert Into PROD_CONTROL(FISCAL_YEAR,INV_NO,ITEM_CODE,PROD_DATE,ITEM_F_QTY,ITEM_B_QTY,ITEM_I_QTY,ITEM_I_SO,ITEM_F_STOCK,ITEM_B_STOCK,ITEM_I_TOTAL)values(@FISCAL_YEAR,@INV_NO,@ITEM_CODE,@PROD_DATE,@ITEM_F_QTY,@ITEM_B_QTY,@ITEM_I_QTY,@ITEM_I_SO,@ITEM_F_STOCK,@ITEM_B_STOCK,@ITEM_I_TOTAL)"
            cmd1 = New SqlCommand(QUARY1, conn)
            cmd1.Parameters.AddWithValue("@ITEM_CODE", GridView2.Rows(lp).Cells(1).Text)
            cmd1.Parameters.AddWithValue("@PROD_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
            cmd1.Parameters.AddWithValue("@ITEM_F_QTY", 0.0)
            cmd1.Parameters.AddWithValue("@ITEM_B_QTY", 0.0)
            cmd1.Parameters.AddWithValue("@ITEM_I_QTY", sendqty)
            cmd1.Parameters.AddWithValue("@ITEM_I_SO", TextBox124.Text)
            cmd1.Parameters.AddWithValue("@INV_NO", "D" & TextBox65.Text)
            cmd1.Parameters.AddWithValue("@ITEM_F_STOCK", fr_stock)
            cmd1.Parameters.AddWithValue("@ITEM_B_STOCK", bsr_stock)
            cmd1.Parameters.AddWithValue("@ITEM_I_TOTAL", 0.0)
            cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd1.ExecuteReader()
            cmd1.Dispose()
            conn.Close()
        Next


        ''search ac head
        Dim IUCA As String = ""
        Dim FREIGHT As String = ""
        Dim EXCISE_HEAD As String = ""
        Dim ED_IPT As String = ""
        Dim VAT As String = ""
        Dim CST_HEAD As String = ""
        Dim TERMINAL As String = ""
        Dim TCS As String = ""
        Dim STOCK_HEAD As String = ""
        Dim PACK_HEAD As String = ""
        Dim DATER_TYPE As String = ""
        conn.Open()
        Dim MC5 As New SqlCommand
        MC5.CommandText = "select ORDER_DETAILS .SO_NO, DATER.D_TAX, dater.stock_ac_head,dater.iuca_head,work_group.ed_liab, work_group.ed_head,work_group.vat_head,work_group.cst_head,work_group.freight_head,work_group.term_tax,work_group.tds_head,work_group.pack_head from ORDER_DETAILS join DATER on ORDER_DETAILS .PARTY_CODE=DATER.d_code JOIN work_group on ORDER_DETAILS.ORDER_TYPE =work_group.work_name and  ORDER_DETAILS.PO_TYPE  =work_group.work_type and ORDER_DETAILS.ORDER_TO  =work_group.d_type WHERE ORDER_DETAILS.SO_NO='" & TextBox124.Text & "'"
        MC5.Connection = conn
        dr = MC5.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            STOCK_HEAD = dr.Item("stock_ac_head")
            IUCA = dr.Item("iuca_head")
            FREIGHT = dr.Item("freight_head")
            EXCISE_HEAD = dr.Item("ed_liab")
            ED_IPT = dr.Item("ed_head")
            VAT = dr.Item("vat_head")
            CST_HEAD = dr.Item("cst_head")
            TERMINAL = dr.Item("term_tax")
            TCS = dr.Item("tds_head")
            PACK_HEAD = dr.Item("pack_head")
            DATER_TYPE = dr.Item("D_TAX")
            dr.Close()
            conn.Close()
        Else
            conn.Close()
        End If
        ''insert ledger
        save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox41.Text), "STOCK TRANSFOR")
        save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, IUCA, "Dr", CDec(TextBox45.Text) - CDec(TextBox21.Text), "IUCA")
        save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, FREIGHT, "Cr", CDec(TextBox39.Text), "FREIGHT")
        save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, ED_IPT, "Dr", CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text), "ED_IPT")
        save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, EXCISE_HEAD, "Cr", CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text), "EXCISE")
        If DATER_TYPE = "VAT" Then
            save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, VAT, "Cr", CDec(TextBox20.Text), "VAT")
        Else
            save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, CST_HEAD, "Cr", CDec(TextBox20.Text), "C.S.T.")
        End If
        save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, TERMINAL, "Cr", CDec(TextBox43.Text), "TERMINAL TAX")
        save_ledger(TextBox124.Text, "D" & TextBox65.Text, TextBox125.Text, TCS, "Cr", CDec(TextBox66.Text), "TCS TAX")

        '' save_ledger(TextBox124.Text, TextBox65.Text, TextBox125.Text, PACK_HEAD, "Cr", CDec(TextBox41.Text), "PACK CHARGE")












        If transporter = "N/A" Or transporter = "PARTY" Then
            ''insert inv_print
            conn.Open()
            QUARY1 = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
            Dim scmd As New SqlCommand(QUARY1, conn)
            scmd.Parameters.AddWithValue("@INV_NO", "D" & TextBox65.Text)
            scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
            scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
            scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
            scmd.Parameters.AddWithValue("@F_YEAR", STR1)
            scmd.ExecuteReader()
            scmd.Dispose()
            conn.Close()
        Else
            ''INSERT MB_BOOK FOR TRANSPORTER
            conn.Open()
            Dim w_qty, w_complite, w_unit_price, W_discount, mat_price, wct_price As Decimal
            Dim WO_NAME As String = ""
            Dim WO_AU As String = ""
            Dim WO_SUPL_ID As String = ""
            Dim MCqq As New SqlCommand
            Dim des_date As Date = Today.Date
            MCqq.CommandText = "select sum(W_MATERIAL_COST) as W_MATERIAL_COST,MAX(SUPL_ID) as SUPL_ID, sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_STAX) as W_STAX,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,sum(W_WCTAX) as W_WCTAX,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' and w_slno=" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim & " and AMD_DATE < ='" & des_date.Year & "-" & des_date.Month & "-" & des_date.Day & "'"
            MCqq.Connection = conn
            dr = MCqq.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                w_qty = dr.Item("W_QTY")
                w_complite = dr.Item("W_COMPLITED")
                w_unit_price = dr.Item("W_UNIT_PRICE")
                W_discount = dr.Item("W_DISCOUNT")
                mat_price = dr.Item("W_MATERIAL_COST")
                wct_price = dr.Item("W_WCTAX")
                WO_NAME = dr.Item("W_NAME")
                WO_AU = dr.Item("W_AU")
                WO_SUPL_ID = dr.Item("SUPL_ID")
                dr.Close()
            End If
            conn.Close()






            'TRANSPORTER AU WISE ENTRY
            If (WO_AU = "Mt" Or WO_AU = "MT" Or WO_AU = "MTS") Then
                'update despatch
                conn.Open()
                QUARY1 = ""
                QUARY1 = "update DESPATCH set INV_STATUS ='' where INV_NO  ='" & TextBox65.Text & "'"
                Dim despatch As New SqlCommand(QUARY1, conn)
                despatch.ExecuteReader()
                despatch.Dispose()
                conn.Close()
                ''insert inv_print
                conn.Open()
                QUARY1 = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
                Dim scmd As New SqlCommand(QUARY1, conn)
                scmd.Parameters.AddWithValue("@INV_NO", "D" & TextBox65.Text)
                scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
                scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
                scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
                scmd.Parameters.AddWithValue("@F_YEAR", STR1)
                scmd.ExecuteReader()
                scmd.Dispose()
                conn.Close()
                ''service data search
                Dim PROV_PRICE_FOR_TRANSPORTER As Decimal = 0.0
                Dim RCVD_QTY_TRANSPORTER As Decimal = 0.0
                i = 0
                For i = 0 To GridView2.Rows.Count - 1
                    RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(GridView2.Rows(i).Cells(6).Text)
                Next
                Dim st_recever, st_provider As Decimal
                st_provider = 0
                st_recever = 0
                conn.Open()
                mc1.CommandText = "select * from s_tax_liability where taxable_service  = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "')"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    st_provider = dr.Item("service_provider")
                    st_recever = dr.Item("service_receiver")
                    dr.Close()
                End If
                conn.Close()

                ''calculate work amount
                Dim base_value, discount_value, st_value_p, st_value_r, mat_rate As Decimal
                base_value = 0
                discount_value = 0
                st_value_p = 0
                st_value_r = 0
                mat_rate = 0
                base_value = w_unit_price * RCVD_QTY_TRANSPORTER
                discount_value = (base_value * W_discount) / 100
                st_value_p = ((base_value - discount_value) * st_provider) / 100
                st_value_r = ((base_value - discount_value) * st_recever) / 100
                wct_price = (((base_value - discount_value) + mat_rate) * wct_price) / 100
                PROV_PRICE_FOR_TRANSPORTER = FormatNumber(base_value - discount_value, 2)


                conn.Open()
                Dim cmd12 As New SqlCommand
                Dim Query12 As String = "Insert Into mb_book(unit_price,Entry_Date,mb_clear,mat_rate,mb_no,mb_date,po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,ra_no,total_amt,pen_amt,st_amt_r,st_amt_p,wct_amt,it_amt,pay_ind,fiscal_year,mb_by)VALUES(@unit_price,@Entry_Date,@mb_clear,@mat_rate,@mb_no,@mb_date,@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@ra_no,@total_amt,@pen_amt,@st_amt_r,@st_amt_p,@wct_amt,@it_amt,@pay_ind,@fiscal_year,@mb_by)"
                cmd12 = New SqlCommand(Query12, conn)
                cmd12.Parameters.AddWithValue("@mb_no", "D" & TextBox65.Text)
                cmd12.Parameters.AddWithValue("@mb_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                cmd12.Parameters.AddWithValue("@po_no", DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim)
                cmd12.Parameters.AddWithValue("@supl_id", WO_SUPL_ID)
                cmd12.Parameters.AddWithValue("@wo_slno", DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim)
                cmd12.Parameters.AddWithValue("@w_name", WO_NAME)
                cmd12.Parameters.AddWithValue("@w_au", WO_AU)
                cmd12.Parameters.AddWithValue("@from_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                cmd12.Parameters.AddWithValue("@to_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                cmd12.Parameters.AddWithValue("@work_qty", RCVD_QTY_TRANSPORTER)
                cmd12.Parameters.AddWithValue("@rqd_qty", RCVD_QTY_TRANSPORTER)
                cmd12.Parameters.AddWithValue("@bal_qty", w_complite)
                cmd12.Parameters.AddWithValue("@note", "")
                cmd12.Parameters.AddWithValue("@ra_no", "")
                cmd12.Parameters.AddWithValue("@total_amt", PROV_PRICE_FOR_TRANSPORTER)
                cmd12.Parameters.AddWithValue("@pen_amt", 0.0)
                cmd12.Parameters.AddWithValue("@st_amt_r", st_value_r)
                cmd12.Parameters.AddWithValue("@st_amt_p", st_value_p)
                cmd12.Parameters.AddWithValue("@mat_rate", 0)
                cmd12.Parameters.AddWithValue("@wct_amt", wct_price)
                cmd12.Parameters.AddWithValue("@it_amt", 0)
                cmd12.Parameters.AddWithValue("@pay_ind", "")
                cmd12.Parameters.AddWithValue("@fiscal_year", STR1)
                cmd12.Parameters.AddWithValue("@mb_clear", "I.R. CLEAR")
                cmd12.Parameters.AddWithValue("@mb_by", Session("userName"))
                cmd12.Parameters.AddWithValue("@unit_price", w_unit_price)
                cmd12.Parameters.AddWithValue("@Entry_Date", Now)
                cmd12.ExecuteReader()
                cmd12.Dispose()
                conn.Close()

                ''SEARCH AC HEAD
                conn.Open()
                Dim TRANS_PROV, TRANS_PUR As String
                TRANS_PROV = ""
                TRANS_PUR = ""
                MC5.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "') and work_type=(select distinct wo_type from wo_order where po_no='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' and w_slno='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim & "')"
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TRANS_PROV = dr.Item("PROV_HEAD")
                    TRANS_PUR = dr.Item("PUR_HEAD")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                ''INSERT TRANSPORTER LEDGER PROV AND PUR
                save_ledger(DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim, "D" & TextBox65.Text, WO_SUPL_ID, TRANS_PUR, "Dr", PROV_PRICE_FOR_TRANSPORTER, "PUR")
                save_ledger(DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim, "D" & TextBox65.Text, WO_SUPL_ID, TRANS_PROV, "Cr", PROV_PRICE_FOR_TRANSPORTER, "PROV")
            End If
        End If


        ''clear data
        Dim dt7 As New DataTable()
        dt7.Columns.AddRange(New DataColumn(8) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("UNIT_WEIGHT"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
        ViewState("despatch") = dt7
        BINDGRID()
        TextBox37.Text = "0.00"
        TextBox38.Text = "0.00"
        TextBox39.Text = "0.00"
        TextBox40.Text = "0.00"
        TextBox41.Text = "0.00"
        TextBox42.Text = "0.00"
        TextBox43.Text = "0.00"
        TextBox44.Text = "0.00"
        TextBox66.Text = "0.00"
        TextBox20.Text = "0.00"
        TextBox45.Text = "0.00"
        TextBox21.Text = "0.00"
        Button36.Enabled = False

      

















    End Sub
    Protected Sub save_ledger(so_no As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
        Dim working_date As Date
       
        working_date = Today.Date
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
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PO_NO", so_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", inv_no)
            cmd.Parameters.AddWithValue("@SUPL_ID", dt_id)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End If
    End Sub
    Protected Sub MAT_SAVE(ITEM_INV_NO As String, SO_NO As String, ITEM_SLNO As Integer, MAT_CODE As String, MAT_NAME As String, MAT_AU As String, MAT_DETAILS As String, MAT_QTY_PCS As Decimal, UNIT_WEIGHT As Decimal, TOTAL_WEIGHT As Decimal, ORD_ASS_VALUE As Decimal, FISCAL_YEAR As Integer, D_TYPE As String)
        If MAT_DETAILS = "&nbsp;" Then
            MAT_DETAILS = ""
        End If

        conn.Open()
        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into INV_MAT(D_TYPE,ITEM_INV_NO,SO_NO,ITEM_SLNO,MAT_CODE,MAT_NAME,MAT_AU,MAT_DETAILS,MAT_QTY_PCS,UNIT_WEIGHT,TOTAL_WEIGHT,ORD_ASS_VALUE,FISCAL_YEAR)values(@D_TYPE,@ITEM_INV_NO,@SO_NO,@ITEM_SLNO,@MAT_CODE,@MAT_NAME,@MAT_AU,@MAT_DETAILS,@MAT_QTY_PCS,@UNIT_WEIGHT,@TOTAL_WEIGHT,@ORD_ASS_VALUE,@FISCAL_YEAR)"
        Dim cmd1 As New SqlCommand(QUARY1, conn)
        cmd1.Parameters.AddWithValue("@ITEM_INV_NO", ITEM_INV_NO)
        cmd1.Parameters.AddWithValue("@SO_NO", SO_NO)
        cmd1.Parameters.AddWithValue("@ITEM_SLNO", ITEM_SLNO)
        cmd1.Parameters.AddWithValue("@MAT_CODE", MAT_CODE)
        cmd1.Parameters.AddWithValue("@MAT_NAME", MAT_NAME)
        cmd1.Parameters.AddWithValue("@MAT_AU", MAT_AU)
        cmd1.Parameters.AddWithValue("@MAT_DETAILS", MAT_DETAILS)
        cmd1.Parameters.AddWithValue("@MAT_QTY_PCS", MAT_QTY_PCS)
        cmd1.Parameters.AddWithValue("@UNIT_WEIGHT", UNIT_WEIGHT)
        cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT", TOTAL_WEIGHT)
        cmd1.Parameters.AddWithValue("@ORD_ASS_VALUE", ORD_ASS_VALUE)
        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", FISCAL_YEAR)
        cmd1.Parameters.AddWithValue("@D_TYPE", D_TYPE)
        cmd1.ExecuteReader()
        cmd1.Dispose()
        conn.Close()
    End Sub
    Protected Sub TAX_SAVE(INV_NO As String, SO_NO As String, TAX_SLNO As Integer, TAX_DESC As String, TAX_VALUE As String, TOTAL_AMOUNT As Decimal, FISCAL_YEAR As Integer, D_TYPE As String)
        conn.Open()
        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into TAX_DESC(D_TYPE,inv_no,so_no,tax_slno,tax_desc,tax_value,total_amount,FISCAL_YEAR)values(@D_TYPE,@inv_no,@so_no,@tax_slno,@tax_desc,@tax_value,@total_amount,@FISCAL_YEAR)"
        Dim cmd1 As New SqlCommand(QUARY1, conn)
        cmd1.Parameters.AddWithValue("@inv_no", INV_NO)
        cmd1.Parameters.AddWithValue("@so_no", SO_NO)
        cmd1.Parameters.AddWithValue("@tax_slno", TAX_SLNO)
        cmd1.Parameters.AddWithValue("@tax_desc", TAX_DESC)
        cmd1.Parameters.AddWithValue("@tax_value", TAX_VALUE)
        cmd1.Parameters.AddWithValue("@total_amount", TOTAL_AMOUNT)
        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", FISCAL_YEAR)
        cmd1.Parameters.AddWithValue("@D_TYPE", D_TYPE)
        cmd1.ExecuteReader()
        cmd1.Dispose()
        conn.Close()
    End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
     



        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            Return
        ElseIf DropDownList9.SelectedValue = "PARTY" Then
            DropDownList28.Items.Clear()
            DropDownList28.Items.Add("N/A")
            DropDownList28.SelectedValue = "N/A"
            TextBox98.Text = ""
            TextBox175.Text = ""
            TextBox99.Text = TextBox97.Text
            Return
        ElseIf DropDownList9.SelectedValue <> "" Or DropDownList9.SelectedValue <> "PARTY" Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select supl.supl_id,supl.supl_name  from ORDER_DETAILS join supl on ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID  WHERE ORDER_DETAILS.SO_NO = '" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox98.Text = dr.Item("supl_id")
                TextBox99.Text = dr.Item("supl_name")
                dr.Close()
            End If
            conn.Close()
            ''work sl no put
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct (STR(W_SLNO) + ' , ' + W_NAME) AS W_SLNO from wo_order where po_no='" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim & "' AND WO_TYPE='FREIGHT OUTWARD'", conn)
            da.Fill(dt)
            DropDownList28.Items.Clear()
            DropDownList28.DataSource = dt
            DropDownList28.DataValueField = "w_slno"
            DropDownList28.DataBind()
            conn.Close()
            DropDownList28.Items.Add("Select")
            DropDownList28.SelectedValue = "Select"
            Return
        End If








    End Sub

    Protected Sub DropDownList28_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList28.SelectedIndexChanged
        If DropDownList28.SelectedValue = "Select" Then
            DropDownList28.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select W_NAME FROM WO_ORDER WHERE PO_NO ='" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim & "' AND W_SLNO='" & DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1).Trim & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox175.Text = dr.Item("W_NAME")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList11.SelectedIndexChanged
        If DropDownList11.SelectedValue = "Select" Then
            DropDownList11.Focus()
            Return
        End If
        ''SEARCH ITEM CODE
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select (SO_MAT_ORDER.ITEM_CODE + ' , ' + MATERIAL.MAT_NAME) AS ITEM_CODE from SO_MAT_ORDER JOIN MATERIAL ON SO_MAT_ORDER.ITEM_CODE=MATERIAL.MAT_CODE  where SO_MAT_ORDER.item_status='PENDING' and SO_MAT_ORDER.SO_NO='" & TextBox123.Text & "' and SO_MAT_ORDER.ITEM_SLNO='" & DropDownList11.SelectedValue & "' ", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList12.Items.Clear()
        DropDownList12.DataSource = dt
        DropDownList12.DataValueField = "ITEM_CODE"
        DropDownList12.DataBind()
        DropDownList12.Items.Add("Select")
        DropDownList12.SelectedValue = "Select"
        ''SELECT AMD NO AND DATE
        conn.Open()
        Dim mc3 As New SqlCommand
        mc3.CommandText = "SELECT AMD_NO , AMD_DATE FROM SO_MAT_ORDER WHERE AMD_DATE=(SELECT MAX(AMD_DATE) FROM SO_MAT_ORDER WHERE ITEM_SLNO=" & DropDownList11.SelectedValue & " AND SO_NO='" & TextBox123.Text & "') AND SO_NO='" & TextBox123.Text & "' AND ITEM_SLNO=" & DropDownList11.SelectedValue
        mc3.Connection = conn
        dr = mc3.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox105.Text = dr.Item("AMD_NO")
            TextBox106.Text = dr.Item("AMD_DATE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        ''SEARCH VOCAB NO
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select distinct ITEM_VOCAB from SO_MAT_ORDER where ITEM_SLNO='" & DropDownList11.SelectedValue & "' and SO_NO='" & TextBox123.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox104.Text = dr.Item("ITEM_VOCAB")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList12.SelectedIndexChanged
        If DropDownList12.SelectedValue = "Select" Then
            DropDownList12.Focus()
            Return
        End If


        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select SO_MAT_ORDER.ITEM_CODE , (MATERIAL.MAT_NAME) AS ITEM_NAME,(MATERIAL.MAT_AU) AS ITEM_AU, ('....') AS ITEM_WEIGHT,SO_MAT_ORDER .ITEM_QTY,(SO_MAT_ORDER.ITEM_QTY-SO_MAT_ORDER.ITEM_QTY_SEND) AS ITEM_BAL_QTY,(MATERIAL.MAT_STOCK) AS ITEM_B_STOCK,('....') AS ITEM_B_STOCK_MT FROM SO_MAT_ORDER JOIN MATERIAL ON SO_MAT_ORDER .ITEM_CODE = MATERIAL.MAT_CODE where SO_MAT_ORDER .ITEM_CODE='" & DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim & "' AND SO_MAT_ORDER.SO_NO='" & TextBox123.Text & "' AND SO_MAT_ORDER.ITEM_SLNO='" & DropDownList11.SelectedValue & "'", conn)
        da.Fill(dt)
        conn.Close()
        ViewState("form_view") = dt
        BINDGRID2()
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select MAT_AU from MATERIAL where MAT_CODE='" & DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label384.Text = dr.Item("MAT_AU")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        FormView2.Visible = True
    End Sub

    Protected Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        Dim dt7 As New DataTable()
        dt7.Columns.AddRange(New DataColumn(7) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
        ViewState("despatch_raw") = dt7
        BINDGRID_RAW()
        TextBox111.Text = 0.0
        TextBox112.Text = 0.0
        TextBox113.Text = 0.0
        TextBox114.Text = 0.0
        TextBox115.Text = 0.0
        TextBox116.Text = 0.0
        TextBox117.Text = 0.0
        TextBox118.Text = 0.0
        TextBox119.Text = 0.0
        TextBox120.Text = 0.0
        TextBox121.Text = 0.0
        TextBox122.Text = 0.0
    End Sub

    Protected Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        If TextBox100.Text = "" Then
            TextBox100.Focus()
            Return
        ElseIf DropDownList11.SelectedValue = "Select" Then
            DropDownList11.Focus()
            Return
        ElseIf DropDownList12.SelectedValue = "Select" Then
            DropDownList12.Focus()
            Return
        ElseIf IsNumeric(TextBox107.Text) = False Then
            TextBox107.Focus()
            Return
        ElseIf IsNumeric(TextBox110.Text) = False Then
            TextBox110.Focus()
            Return
        End If
        Dim COUNTER As Integer = 0
        For COUNTER = 0 To GridView4.Rows.Count - 1
            If DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1).Trim = GridView4.Rows(COUNTER).Cells(1).Text Then
                DropDownList12.Focus()
                Return
            End If
        Next

        ''details search
        Dim pcs_qty, total_weight, ass_price As Decimal
        pcs_qty = 0
        total_weight = 0
        ass_price = 0.0
        Dim UNIT_WEIGHT As Decimal = 0.0
        Dim stock_qty As Decimal = 0.0
        Dim cas4_price, item_qty, unit_rate, discount, pack_forwd, item_cst, item_tcs, terminal_tax, basic_ed, sess_ed, hsess_ed, qty_send, freight_rate As Decimal
        Dim freight_type As String = ""
        Dim ord_au As String = ""
        cas4_price = 0
        unit_rate = 0
        discount = 0
        pack_forwd = 0
        item_cst = 0
        item_tcs = 0
        terminal_tax = 0
        basic_ed = 0
        sess_ed = 0
        hsess_ed = 0
        qty_send = 0
        item_qty = 0
        freight_rate = 0
        ''add data 
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select MAT_STOCK from MATERIAL where MAT_CODE='" & DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            stock_qty = dr.Item("MAT_STOCK")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If stock_qty < CDec(TextBox107.Text) Then
            TextBox54.Focus()
            Label388.Text = "Qty is Higher Than Stock Qty"
            Return
        Else
            Label388.Text = ""
        End If
        ''item assessable value calculatation
        Dim mc As New SqlCommand
        conn.Open()
        mc.CommandText = "select SO_MAT_ORDER.ORD_AU, SO_MAT_ORDER.ITEM_QTY, SO_MAT_ORDER.ITEM_UNIT_RATE ,SO_MAT_ORDER .ITEM_DISCOUNT,SO_MAT_ORDER .ITEM_PACK ,SO_MAT_ORDER .ITEM_TAXTYPE,SO_MAT_ORDER .ITEM_CST ,SO_MAT_ORDER .ITEM_QTY_SEND ,SO_MAT_ORDER .ITEM_TCS ,SO_MAT_ORDER .ITEM_TERMINAL_TAX , SO_MAT_ORDER .ITEM_FREIGHT_TYPE ,SO_MAT_ORDER .ITEM_FREIGHT_PU ,SO_MAT_ORDER.ITEM_EXCISE_DUTY ,SO_MAT_ORDER.ITEM_SESS_DUTY ,SO_MAT_ORDER.ITEM_HSESS_DUTY FROM SO_MAT_ORDER  where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList11.SelectedValue & "' and SO_MAT_ORDER .SO_NO='" & TextBox123.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            ord_au = dr.Item("ORD_AU")
            unit_rate = dr.Item("ITEM_UNIT_RATE")
            discount = dr.Item("ITEM_DISCOUNT")
            pack_forwd = dr.Item("ITEM_PACK")
            basic_ed = dr.Item("ITEM_EXCISE_DUTY")
            sess_ed = dr.Item("ITEM_SESS_DUTY")
            hsess_ed = dr.Item("ITEM_HSESS_DUTY")
            Label398.Text = dr.Item("ITEM_TAXTYPE")
            item_cst = dr.Item("ITEM_CST")
            terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
            item_tcs = dr.Item("ITEM_TCS")
            qty_send = dr.Item("ITEM_QTY_SEND")
            item_qty = dr.Item("ITEM_QTY")
            freight_type = dr.Item("ITEM_FREIGHT_TYPE")
            freight_rate = dr.Item("ITEM_FREIGHT_PU")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        cas4_price = unit_rate
        If (item_qty - qty_send) < CDec(TextBox107.Text) Then
            TextBox107.Focus()
            Label388.Text = "Qty is Higher Than Order Bal Qty"
            Return
        Else
            Label388.Text = ""
        End If
        ass_price = CDec(TextBox107.Text) * unit_rate
        Dim row_count As Integer = GridView4.Rows.Count + 1
        Dim dt As DataTable = DirectCast(ViewState("despatch_raw"), DataTable)
        dt.Rows.Add(row_count, DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim, DropDownList12.Text.Substring((DropDownList12.Text.IndexOf(",") + 2)), Label384.Text, TextBox107.Text, TextBox110.Text, TextBox108.Text, FormatNumber(ass_price, 2))
        ViewState("despatch_raw") = dt
        BINDGRID_RAW()
        Dim weight As Decimal = 0.0
        Dim i As Integer = 0
        For i = 0 To GridView4.Rows.Count - 1
            weight = CDec(GridView4.Rows(i).Cells(5).Text) + weight
        Next
        ''tax calculatation
        TextBox112.Text = FormatNumber((CDec(TextBox112.Text) + ass_price), 2)
        If freight_type = "PERCENTAGE" Then
            TextBox113.Text = FormatNumber((((CDec(TextBox112.Text) * freight_rate) / 100)), 2)
        Else
            TextBox113.Text = FormatNumber(((weight * freight_rate)), 2)
        End If
        TextBox115.Text = FormatNumber(CDec(TextBox115.Text) + ((CDec(TextBox112.Text) * pack_forwd) / 100), 2)
        TextBox111.Text = FormatNumber(((CDec(TextBox112.Text) * discount) / 100), 2)
        TextBox114.Text = FormatNumber(CInt((((cas4_price * CDec(TextBox107.Text)) + CDec(TextBox115.Text)) * basic_ed) / 100), 2)
        TextBox116.Text = FormatNumber(CInt(CDec(TextBox116.Text) + (CDec(TextBox114.Text) * sess_ed) / 100), 2)
        TextBox118.Text = FormatNumber(CInt(CDec(TextBox118.Text) + (CDec(TextBox114.Text) * hsess_ed) / 100), 2)
        TextBox120.Text = FormatNumber(((((CDec(TextBox112.Text) - CDec(TextBox111.Text)) + CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text)) * item_cst) / 100), 2)
        TextBox121.Text = ((CDec(TextBox112.Text) + CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text) + CDec(TextBox120.Text) + CDec(TextBox113.Text) + CDec(TextBox115.Text) + CDec(TextBox117.Text) + CDec(TextBox119.Text)) - CDec(TextBox111.Text)) - CInt((CDec(TextBox112.Text) + CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text) + CDec(TextBox120.Text) + CDec(TextBox113.Text) + CDec(TextBox115.Text) + CDec(TextBox117.Text) + CDec(TextBox119.Text)) - CDec(TextBox111.Text))
        TextBox122.Text = FormatNumber(CInt((CDec(TextBox112.Text) + CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text) + CDec(TextBox120.Text) + CDec(TextBox113.Text) + CDec(TextBox115.Text) + CDec(TextBox117.Text) + CDec(TextBox119.Text)) - CDec(TextBox111.Text)), 2)
    End Sub

    Protected Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        Dim working_date As Date
      
        working_date = Today.Date
        If GridView4.Rows.Count = 0 Then
            Label388.Text = "Please add Material first"
            Return
        End If
        ''INVOICE NO GENERATE
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
        conn.Open()
        Dim inv_no As String = ""
        Dim mc_c As New SqlCommand
        mc_c.CommandText = "SELECT (CASE WHEN MAX(INV_NO) IS NULL THEN 0 ELSE MAX(INV_NO) END) as inv_no  FROM DESPATCH WHERE D_TYPE='D' AND FISCAL_YEAR =" & STR1
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
            TextBox65.ReadOnly = False
            If TextBox95.Text = "" Then
                TextBox95.Focus()
                Label388.Text = "Please enter Invoice No First"
                Return
            ElseIf IsNumeric(TextBox95.Text) = False Then
                TextBox95.Focus()
                Label388.Text = "Please enter Invoice No In Numeric value"
                Return
            End If


        Else
            str = CInt(inv_no) + 1
            If str.Length = 1 Then
                str = "0000" & CInt(inv_no) + 1
            ElseIf str.Length = 2 Then
                str = "000" & CInt(inv_no) + 1
            ElseIf str.Length = 3 Then
                str = "00" & CInt(inv_no) + 1
            ElseIf str.Length = 4 Then
                str = "0" & CInt(inv_no) + 1
            ElseIf str.Length = 5 Then
                str = CInt(inv_no) + 1

            End If
            TextBox95.Text = str
            TextBox95.ReadOnly = True
        End If
        ''DESPATCH NO GENERATE

        ''SAVE DESPATCH
        conn.Open()
        dt.Clear()
        count = 0
        da = New SqlDataAdapter("select INV_NO from DESPATCH where FISCAL_YEAR=" & CInt(STR1), conn)
        count = da.Fill(dt)
        conn.Close()
        If count = 0 Then
            count = 1
        End If
        ''sale order date excise comodity
        Dim so_date As Date
        Dim ORDER_TO As String = ""
        Dim PARTY_CODE As String = ""
        Dim excise_comodity As String = ""
        Dim chptr_heading As String = ""
        Dim mode_of_despatch As String = ""
        Dim mode_of_payment As String = ""
        Dim qual_name As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select MATERIAL .CHPTR_HEAD, ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PARTY_CODE, ORDER_DETAILS.PAYMENT_TERM,ORDER_DETAILS.MODE_OF_DESPATCH FROM SO_MAT_ORDER JOIN ORDER_DETAILS ON SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO JOIN material ON SO_MAT_ORDER .ITEM_CODE =MATERIAL.MAT_CODE WHERE SO_MAT_ORDER.ITEM_SLNO  ='" & DropDownList11.SelectedValue & "' AND SO_MAT_ORDER .SO_NO ='" & TextBox123.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            chptr_heading = dr.Item("CHPTR_HEAD")
            so_date = dr.Item("SO_DATE")
            PARTY_CODE = dr.Item("PARTY_CODE")
            mode_of_despatch = dr.Item("MODE_OF_DESPATCH")
            mode_of_payment = dr.Item("PAYMENT_TERM")
            ORDER_TO = dr.Item("ORDER_TO")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim weight As Decimal = 0.0
        Dim i As Integer = 0
        For i = 0 To GridView4.Rows.Count - 1
            weight = CDec(GridView4.Rows(i).Cells(5).Text) + weight
        Next


        ''SAVE ITEM
        Dim i1 As Integer = 0
        For i1 = 0 To GridView4.Rows.Count - 1
            MAT_SAVE(TextBox95.Text, TextBox123.Text, CInt(GridView4.Rows(i1).Cells(0).Text), GridView4.Rows(i1).Cells(1).Text, GridView4.Rows(i1).Cells(2).Text, GridView4.Rows(i1).Cells(3).Text, GridView4.Rows(i1).Cells(6).Text, CInt(GridView4.Rows(i1).Cells(4).Text), 0, CDec(GridView4.Rows(i1).Cells(5).Text), CDec(GridView4.Rows(i1).Cells(7).Text), STR1, "D")
        Next

        ''item assessable value calculatation
        Dim cas4_price, item_qty, unit_rate, discount, pack_forwd, item_cst, item_tcs, terminal_tax, basic_ed, sess_ed, hsess_ed, qty_send, freight_rate As Decimal
        Dim freight_type As String = ""
        cas4_price = 0
        unit_rate = 0
        discount = 0
        pack_forwd = 0
        item_cst = 0
        item_tcs = 0
        terminal_tax = 0
        basic_ed = 0
        sess_ed = 0
        hsess_ed = 0
        qty_send = 0
        item_qty = 0
        freight_rate = 0
        conn.Open()
        Dim mc As New SqlCommand
        '' mc.CommandText = "select SO_MAT_ORDER.ITEM_QTY, SO_MAT_ORDER.ITEM_UNIT_RATE ,SO_MAT_ORDER .ITEM_DISCOUNT,SO_MAT_ORDER .ITEM_PACK ,SO_MAT_ORDER .ITEM_TAXTYPE,SO_MAT_ORDER .ITEM_CST ,SO_MAT_ORDER .ITEM_QTY_SEND ,SO_MAT_ORDER .ITEM_TCS ,SO_MAT_ORDER .ITEM_TERMINAL_TAX , SO_MAT_ORDER .ITEM_FREIGHT_TYPE ,SO_MAT_ORDER .ITEM_FREIGHT_PU ,CAS_4 .COST_VALUE,CHPTR_HEADING.TAX_VALUE ,CHPTR_HEADING.ED_SESS ,CHPTR_HEADING.SHED_CESS   FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE JOIN CAS_4 ON F_ITEM .ITEM_TYPE =CAS_4 .MAT_GROUP join CHPTR_HEADING on F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList11.SelectedValue & "' and SO_MAT_ORDER .SO_NO='" & TextBox123.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1) & "'"
        mc.CommandText = "select *  FROM SO_MAT_ORDER where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList11.SelectedValue & "' and SO_MAT_ORDER .SO_NO='" & TextBox123.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1).Trim & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            unit_rate = dr.Item("ITEM_UNIT_RATE")
            discount = dr.Item("ITEM_DISCOUNT")
            pack_forwd = dr.Item("ITEM_PACK")
            basic_ed = dr.Item("ITEM_EXCISE_DUTY")
            sess_ed = dr.Item("ITEM_SESS_DUTY")
            hsess_ed = dr.Item("ITEM_HSESS_DUTY")
            Label303.Text = dr.Item("ITEM_TAXTYPE")
            item_cst = dr.Item("ITEM_CST")
            terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
            item_tcs = dr.Item("ITEM_TCS")
            qty_send = dr.Item("ITEM_QTY_SEND")
            item_qty = dr.Item("ITEM_QTY")
            freight_type = dr.Item("ITEM_FREIGHT_TYPE")
            freight_rate = dr.Item("ITEM_FREIGHT_PU")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If freight_type = "PERCNTAGE" Then
            freight_type = freight_rate & " %"
        Else
            freight_type = freight_rate & " /Mt"
        End If
        ''data save for tax_desc
        ''assessable value
        If TextBox112.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 1, "Assessable Value", "", CDec(TextBox112.Text), STR1, "D")
        End If
        'discount
        If TextBox111.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 2, "Discount", discount & " %", CDec(TextBox111.Text), STR1, "D")
        End If
        ''packing forwording
        If TextBox115.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 3, "Pack & Forwd", pack_forwd & " %", CDec(TextBox115.Text), STR1, "D")
        End If
        ''basic ed
        If TextBox114.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 4, "Basic Excise Duty", basic_ed & " %", CDec(TextBox114.Text), STR1, "D")
        End If
        ''cess
        If TextBox116.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 5, "Cess", sess_ed & " %", CDec(TextBox116.Text), STR1, "D")
        End If
        ''hs cess
        If TextBox118.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 6, "Hs Cess", hsess_ed & " %", CDec(TextBox118.Text), STR1, "D")
        End If
        ''vat/cst
        If TextBox120.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 7, Label303.Text, item_cst & " %", CDec(TextBox120.Text), STR1, "D")
        End If
        ''terminal tax
        If TextBox117.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 8, "Terminal Tax", terminal_tax & " %", CDec(TextBox117.Text), STR1, "D")
        End If
        ''tcs 
        If TextBox119.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 9, "T.C.S ", item_tcs & " %", CDec(TextBox119.Text), STR1, "D")
        End If
        ''freight
        If TextBox113.Text > 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 10, "Freight", freight_type, CDec(TextBox113.Text), STR1, "D")
        End If
        ''roundoff
        If CDec(TextBox121.Text) <> 0 Then
            TAX_SAVE(TextBox95.Text, TextBox123.Text, 11, "Round Off", "", CDec(TextBox121.Text), STR1, "D")
        End If
        ''grand total
        TAX_SAVE(TextBox95.Text, TextBox123.Text, 12, "Grand Total", "", CDec(TextBox122.Text), STR1, "D")
        ''despatch save
        conn.Open()
        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into DESPATCH(D_TYPE,SO_NO,SO_DATE,AMD_NO,AMD_DATE,TRANS_WO,TRANS_NAME,TRUCK_NO,ED_COMDT,PARTY_CODE,CONSIGN_CODE,MAT_VOCAB,MAT_SLNO,INV_NO,INV_DATE,CHPTR_HEAD,FISCAL_YEAR,INV_ISSUE,DEBIT_ENTRY_NO,MODE_OF_DESPATCH,MODE_OF_PAYMENT,QUALITY,FORM_NAME,WAY_BILL,ED_VALUE,TOTAL_WEIGHT,ORD_UNIT_VALUE,CAS_4_UNIT_VALUE,DIFF_ED_VALUE,PAY_VALUE,INV_STATUS,LR_NO,NOTIFICATION_,EMP_ID1,EMP_ID2)values(@D_TYPE,@SO_NO,@SO_DATE,@AMD_NO,@AMD_DATE,@TRANS_WO,@TRANS_NAME,@TRUCK_NO,@ED_COMDT,@PARTY_CODE,@CONSIGN_CODE,@MAT_VOCAB,@MAT_SLNO,@INV_NO,@INV_DATE,@CHPTR_HEAD,@FISCAL_YEAR,@INV_ISSUE,@DEBIT_ENTRY_NO,@MODE_OF_DESPATCH,@MODE_OF_PAYMENT,@QUALITY,@FORM_NAME,@WAY_BILL,@ED_VALUE,@TOTAL_WEIGHT,@ORD_UNIT_VALUE,@CAS_4_UNIT_VALUE,@DIFF_ED_VALUE,@PAY_VALUE,@INV_STATUS,@LR_NO,@NOTIFICATION_,@EMP_ID1,@EMP_ID2)"
        Dim cmd1 As New SqlCommand(QUARY1, conn)
        cmd1.Parameters.AddWithValue("@SO_NO", TextBox123.Text)
        cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@AMD_NO", TextBox105.Text)
        cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(TextBox106.Text, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@TRANS_WO", DropDownList6.SelectedValue)
        cmd1.Parameters.AddWithValue("@TRANS_NAME", TextBox62.Text)
        cmd1.Parameters.AddWithValue("@TRUCK_NO", TextBox100.Text)
        cmd1.Parameters.AddWithValue("@ED_COMDT", excise_comodity)
        cmd1.Parameters.AddWithValue("@PARTY_CODE", PARTY_CODE)
        cmd1.Parameters.AddWithValue("@CONSIGN_CODE", PARTY_CODE)
        cmd1.Parameters.AddWithValue("@MAT_VOCAB", TextBox104.Text)
        cmd1.Parameters.AddWithValue("@MAT_SLNO", DropDownList11.SelectedValue)
        cmd1.Parameters.AddWithValue("@INV_NO", TextBox95.Text)
        cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@CHPTR_HEAD", chptr_heading)
        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
        cmd1.Parameters.AddWithValue("@INV_ISSUE", Now)
        cmd1.Parameters.AddWithValue("@DEBIT_ENTRY_NO", TextBox101.Text)
        cmd1.Parameters.AddWithValue("@MODE_OF_DESPATCH", mode_of_despatch)
        cmd1.Parameters.AddWithValue("@MODE_OF_PAYMENT", mode_of_payment)
        cmd1.Parameters.AddWithValue("@QUALITY", qual_name)
        cmd1.Parameters.AddWithValue("@FORM_NAME", DropDownList7.SelectedValue)
        cmd1.Parameters.AddWithValue("@WAY_BILL", "")
        cmd1.Parameters.AddWithValue("@ED_VALUE", CInt(TextBox114.Text) + CInt(TextBox116.Text) + CInt(TextBox118.Text))
        cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT", weight)
        cmd1.Parameters.AddWithValue("@ORD_UNIT_VALUE", unit_rate)
        cmd1.Parameters.AddWithValue("@CAS_4_UNIT_VALUE", cas4_price)
        cmd1.Parameters.AddWithValue("@DIFF_ED_VALUE", 0)
        cmd1.Parameters.AddWithValue("@PAY_VALUE", CDec(TextBox122.Text))
        cmd1.Parameters.AddWithValue("@INV_STATUS", "")
        cmd1.Parameters.AddWithValue("@LR_NO", TextBox102.Text)
        cmd1.Parameters.AddWithValue("@NOTIFICATION_ ", TextBox103.Text)
        cmd1.Parameters.AddWithValue("@D_TYPE", "D")
        cmd1.Parameters.AddWithValue("@EMP_ID1", Session("userName"))
        cmd1.Parameters.AddWithValue("@EMP_ID2", Session("userName"))
        cmd1.ExecuteReader()
        cmd1.Dispose()
        conn.Close()
        ''insert inv_print
        conn.Open()
        QUARY1 = "Insert Into INV_PRINT(INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
        Dim scmd As New SqlCommand(QUARY1, conn)
        scmd.Parameters.AddWithValue("@INV_NO", "D" & TextBox95.Text)
        scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
        scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
        scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
        scmd.ExecuteReader()
        scmd.Dispose()
        conn.Close()
        ''update sale order update f_item
        Dim lp As Integer = 0
        For lp = 0 To GridView4.Rows.Count - 1
            conn.Open()
            QUARY1 = ""
            QUARY1 = "update MATERIAL set MAT_STOCK =MAT_STOCK - ((" & CDec(GridView4.Rows(lp).Cells(4).Text) & ")),LAST_ISSUE_DATE = @ITEM_LAST_DESPATCH,LAST_TRANS_DATE=@ITEM_LAST_DESPATCH where MAT_CODE ='" & GridView4.Rows(lp).Cells(1).Text & "'"
            Dim cmd2 As New SqlCommand(QUARY1, conn)
            cmd2.Parameters.AddWithValue("@ITEM_LAST_DESPATCH", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
            cmd2.ExecuteReader()
            cmd2.Dispose()
            conn.Close()
            ''UPDATE TRANSPORTER
            If DropDownList9.SelectedValue <> "N/A" Then
                conn.Open()
                QUARY1 = ""
                QUARY1 = "update WO_ORDER set W_COMPLITED =W_COMPLITED + (" & FormatNumber(CDec(GridView4.Rows(lp).Cells(5).Text), 3) & ") where PO_NO ='" & DropDownList9.Text & "'"
                Dim TRANS As New SqlCommand(QUARY1, conn)
                TRANS.ExecuteReader()
                TRANS.Dispose()
                conn.Close()
            End If
            ''update so order
            conn.Open()
            QUARY1 = ""
            QUARY1 = "update SO_MAT_ORDER set ITEM_QTY_SEND =(ITEM_QTY_SEND + (" & CDec(GridView4.Rows(lp).Cells(4).Text) & ")) where SO_NO ='" & TextBox123.Text & "' and ITEM_SLNO ='" & DropDownList11.SelectedValue & "' and ITEM_CODE ='" & GridView4.Rows(lp).Cells(1).Text & "' AND AMD_NO='" & TextBox105.Text & "'"
            Dim cmdd As New SqlCommand(QUARY1, conn)
            cmdd.ExecuteReader()
            cmdd.Dispose()
            conn.Close()
            ''INSERT MAT_DETAILS
            conn.Open()
            Dim mcc As New SqlCommand
            Dim mat_stock As Decimal = 0
            mcc.CommandText = "SELECT MAT_STOCK FROM MATERIAL WHERE MAT_CODE='" & GridView4.Rows(lp).Cells(1).Text & "'"
            mcc.Connection = conn
            dr = mcc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                mat_stock = dr.Item("MAT_STOCK")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            ''LINE NO
            Dim LINO_SL As Integer = 0
            conn.Open()
            ds.Clear()
            da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & GridView4.Rows(lp).Cells(1).Text & "' AND LINE_NO <> 0", conn)
            LINO_SL = da.Fill(ds, "MAT_DETAILS")
            conn.Close()
            ''MATERIAL DETAILS SEARCH IN MATERIAL TABLE

            ''MATERIAL DETAILS ADD
            conn.Open()
            Dim Query As String = "Insert Into MAT_DETAILS(MAT_SL_NO,MAT_QTY,LINE_DATE,ISSUE_NO,LINE_NO,FISCAL_YEAR,LINE_TYPE,MAT_CODE,RQD_QTY,ISSUE_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,PURPOSE,COST_CODE,AUTH_BY,ISSUE_TYPE,RQD_DATE)VALUES(@MAT_SL_NO,@MAT_QTY,@LINE_DATE,@ISSUE_NO,@LINE_NO,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@RQD_QTY,@ISSUE_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@PURPOSE,@COST_CODE,@AUTH_BY,@ISSUE_TYPE,@RQD_DATE)"
            Dim cmd As New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@ISSUE_NO", "D" & TextBox95.Text)
            cmd.Parameters.AddWithValue("@ISSUE_TYPE", ORDER_TO)
            cmd.Parameters.AddWithValue("@LINE_NO", LINO_SL + 1)
            cmd.Parameters.AddWithValue("@LINE_DATE", working_date)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
            cmd.Parameters.AddWithValue("@LINE_TYPE", "S")
            cmd.Parameters.AddWithValue("@RQD_DATE", working_date.Date)
            cmd.Parameters.AddWithValue("@MAT_CODE", GridView4.Rows(lp).Cells(1).Text)
            cmd.Parameters.AddWithValue("@MAT_QTY", 0.0)
            cmd.Parameters.AddWithValue("@RQD_QTY", CDec(GridView4.Rows(lp).Cells(4).Text))
            cmd.Parameters.AddWithValue("@ISSUE_QTY", CDec(GridView4.Rows(lp).Cells(4).Text))
            cmd.Parameters.AddWithValue("@MAT_BALANCE", mat_stock)
            cmd.Parameters.AddWithValue("@UNIT_PRICE", unit_rate)
            cmd.Parameters.AddWithValue("@TOTAL_PRICE", CDec(GridView4.Rows(lp).Cells(7).Text))
            cmd.Parameters.AddWithValue("@PURPOSE", "SALE")
            cmd.Parameters.AddWithValue("@COST_CODE", TextBox96.Text)
            cmd.Parameters.AddWithValue("@AUTH_BY", Session("userName"))
            cmd.Parameters.AddWithValue("@MAT_SL_NO", DropDownList11.SelectedValue)
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        Next
        ''search ac head
        Dim IUCA As String = ""
        Dim FREIGHT As String = ""
        Dim EXCISE_HEAD As String = ""
        Dim VAT As String = ""
        Dim CST_HEAD As String = ""
        Dim TERMINAL As String = ""
        Dim TCS As String = ""
        Dim STOCK_HEAD As String = ""
        Dim PACK_HEAD As String = ""
        Dim DATER_TYPE As String = ""
        conn.Open()
        Dim MC5 As New SqlCommand
        MC5.CommandText = "select ORDER_DETAILS .SO_NO, DATER.D_TAX, dater.stock_ac_head , " &
                          "dater.iuca_head,work_group.ed_liab ,work_group.vat_head,work_group.cst_head,work_group.freight_head,work_group.term_tax , " &
                          "work_group.tds_head,work_group.pack_head from ORDER_DETAILS join DATER on ORDER_DETAILS .PARTY_CODE=DATER.d_code JOIN work_group on ORDER_DETAILS.ORDER_TYPE =work_group.work_name and  ORDER_DETAILS.PO_TYPE  =work_group.work_type and ORDER_DETAILS.ORDER_TO  =work_group.d_type WHERE ORDER_DETAILS.SO_NO='" & TextBox123.Text & "'"
        MC5.Connection = conn
        dr = MC5.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            STOCK_HEAD = dr.Item("stock_ac_head")
            IUCA = dr.Item("iuca_head")
            FREIGHT = dr.Item("freight_head")
            EXCISE_HEAD = dr.Item("ed_liab")
            VAT = dr.Item("vat_head")
            CST_HEAD = dr.Item("cst_head")
            TERMINAL = dr.Item("term_tax")
            TCS = dr.Item("tds_head")
            PACK_HEAD = dr.Item("pack_head")
            DATER_TYPE = dr.Item("D_TAX")
            dr.Close()
            conn.Close()
        Else
            conn.Close()
        End If

        ''insert ledger
        save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, STOCK_HEAD, "Cr", CDec(TextBox112.Text), "STOCK TRANSFOR")
        save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, IUCA, "Dr", CDec(TextBox121.Text) + CDec(TextBox122.Text), "IUCA")

        save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, FREIGHT, "Cr", CDec(TextBox113.Text), "FREIGHT")
        save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, EXCISE_HEAD, "Cr", CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text), "EXCISE")
        If DATER_TYPE = "VAT" Then
            save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, VAT, "Cr", CDec(TextBox120.Text), "VAT")
        Else
            save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, CST_HEAD, "Cr", CDec(TextBox120.Text), "CST")
        End If
        save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, TERMINAL, "Cr", CDec(TextBox117.Text), "TERMINAL TAX")
        save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, TCS, "Cr", CDec(TextBox119.Text), "TCS TAX")
        save_ledger(TextBox123.Text, "D" & TextBox95.Text, TextBox96.Text, PACK_HEAD, "Cr", CDec(TextBox115.Text), "PACK CHARGE")






        If DropDownList28.SelectedValue <> "N/A" Or DropDownList28.SelectedValue <> "PARTY" Then
            ''INSERT MB_BOOK FOR TRANSPORTER
            conn.Open()
            Dim w_qty, w_complite, w_unit_price, W_discount, mat_price, wct_price As Decimal
            Dim WO_NAME As String = ""
            Dim WO_AU As String = ""
            Dim WO_SUPL_ID As String = ""
            Dim MCqq As New SqlCommand
            MCqq.CommandText = "select * from WO_ORDER where PO_NO = '" & DropDownList9.SelectedValue & "' and w_slno='" & DropDownList28.SelectedValue & "'"
            MCqq.Connection = conn
            dr = MCqq.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                w_qty = dr.Item("W_QTY")
                w_complite = dr.Item("W_COMPLITED")
                w_unit_price = dr.Item("W_UNIT_PRICE")
                W_discount = dr.Item("W_DISCOUNT")
                mat_price = dr.Item("W_MATERIAL_COST")
                wct_price = dr.Item("W_WCTAX")
                WO_NAME = dr.Item("W_NAME")
                WO_AU = dr.Item("W_AU")
                WO_SUPL_ID = dr.Item("SUPL_ID")
                dr.Close()
            End If
            conn.Close()
            ''service data search
            Dim PROV_PRICE_FOR_TRANSPORTER As Decimal = 0.0
            Dim RCVD_QTY_TRANSPORTER As Decimal = 0.0
            i = 0
            For i = 0 To GridView4.Rows.Count - 1
                RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(GridView4.Rows(i).Cells(5).Text)
            Next
            Dim st_recever, st_provider As Decimal
            st_provider = 0
            st_recever = 0
            conn.Open()
            mc1.CommandText = "select * from s_tax_liability where taxable_service  = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList9.SelectedValue & "')"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                st_provider = dr.Item("service_provider")
                st_recever = dr.Item("service_receiver")
                dr.Close()
            End If
            conn.Close()
            ''calculate work amount
            Dim base_value, discount_value, st_value_p, st_value_r, mat_rate As Decimal
            base_value = 0
            discount_value = 0
            st_value_p = 0
            st_value_r = 0
            mat_rate = 0
            base_value = w_unit_price * RCVD_QTY_TRANSPORTER
            discount_value = (base_value * W_discount) / 100
            st_value_p = ((base_value - discount_value) * st_provider) / 100
            st_value_r = ((base_value - discount_value) * st_recever) / 100
            wct_price = (((base_value - discount_value) + mat_rate) * wct_price) / 100
            PROV_PRICE_FOR_TRANSPORTER = FormatNumber(base_value - discount_value, 2)
            conn.Open()
            Dim cmd12 As New SqlCommand
            Dim Query12 As String = "Insert Into mb_book(unit_price,Entry_Date,mb_clear,mat_rate,mb_no,mb_date,po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,ra_no,total_amt,pen_amt,st_amt_r,st_amt_p,wct_amt,it_amt,pay_ind,fiscal_year,mb_by)VALUES(@unit_price,@Entry_Date,@mb_clear,@mat_rate,@mb_no,@mb_date,@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@ra_no,@total_amt,@pen_amt,@st_amt_r,@st_amt_p,@wct_amt,@it_amt,@pay_ind,@fiscal_year,@mb_by)"
            cmd12 = New SqlCommand(Query12, conn)
            cmd12.Parameters.AddWithValue("@mb_no", "D" & TextBox95.Text)
            cmd12.Parameters.AddWithValue("@mb_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
            cmd12.Parameters.AddWithValue("@po_no", DropDownList9.SelectedValue)
            cmd12.Parameters.AddWithValue("@supl_id", WO_SUPL_ID)
            cmd12.Parameters.AddWithValue("@wo_slno", DropDownList28.SelectedValue)
            cmd12.Parameters.AddWithValue("@w_name", WO_NAME)
            cmd12.Parameters.AddWithValue("@w_au", WO_AU)
            cmd12.Parameters.AddWithValue("@from_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
            cmd12.Parameters.AddWithValue("@to_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
            cmd12.Parameters.AddWithValue("@work_qty", RCVD_QTY_TRANSPORTER)
            cmd12.Parameters.AddWithValue("@rqd_qty", RCVD_QTY_TRANSPORTER)
            cmd12.Parameters.AddWithValue("@bal_qty", w_complite)
            cmd12.Parameters.AddWithValue("@note", "")
            cmd12.Parameters.AddWithValue("@ra_no", "")
            cmd12.Parameters.AddWithValue("@total_amt", PROV_PRICE_FOR_TRANSPORTER)
            cmd12.Parameters.AddWithValue("@pen_amt", 0.0)
            cmd12.Parameters.AddWithValue("@st_amt_r", st_value_r)
            cmd12.Parameters.AddWithValue("@st_amt_p", st_value_p)
            cmd12.Parameters.AddWithValue("@mat_rate", 0)
            cmd12.Parameters.AddWithValue("@wct_amt", wct_price)
            cmd12.Parameters.AddWithValue("@it_amt", 0)
            cmd12.Parameters.AddWithValue("@pay_ind", "")
            cmd12.Parameters.AddWithValue("@fiscal_year", STR1)
            cmd12.Parameters.AddWithValue("@mb_clear", "I.R. CLEAR")
            cmd12.Parameters.AddWithValue("@mb_by", Session("userName"))
            cmd12.Parameters.AddWithValue("@unit_price", w_unit_price)
            cmd12.Parameters.AddWithValue("@Entry_Date", Now)
            cmd12.ExecuteReader()
            cmd12.Dispose()
            conn.Close()


            ''SEARCH AC HEAD
            conn.Open()
            Dim TRANS_PROV, TRANS_PUR As String
            TRANS_PROV = ""
            TRANS_PUR = ""
            MC5.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList9.SelectedValue & "') and work_type=(select wo_type from wo_order where po_no='" & DropDownList9.SelectedValue & "' and w_slno='" & DropDownList28.SelectedValue & "')"
            MC5.Connection = conn
            dr = MC5.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TRANS_PROV = dr.Item("PROV_HEAD")
                TRANS_PUR = dr.Item("PUR_HEAD")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            ''INSERT TRANSPORTER LEDGER PROV AND PUR
            save_ledger(DropDownList9.SelectedValue, "D" & TextBox95.Text, WO_SUPL_ID, TRANS_PUR, "Dr", PROV_PRICE_FOR_TRANSPORTER, "PUR")
            save_ledger(DropDownList9.SelectedValue, "D" & TextBox95.Text, WO_SUPL_ID, TRANS_PUR, "Cr", PROV_PRICE_FOR_TRANSPORTER, "PROV")
        End If
        ''clear data
        Dim dt7 As New DataTable()
        dt7.Columns.AddRange(New DataColumn(8) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("UNIT_WEIGHT"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
        ViewState("despatch_raw") = dt7
        BINDGRID_RAW()
        TextBox111.Text = "0.00"
        TextBox112.Text = "0.00"
        TextBox113.Text = "0.00"
        TextBox114.Text = "0.00"
        TextBox115.Text = "0.00"
        TextBox116.Text = "0.00"
        TextBox117.Text = "0.00"
        TextBox118.Text = "0.00"
        TextBox119.Text = "0.00"
        TextBox120.Text = "0.00"
        TextBox122.Text = "0.00"
        TextBox121.Text = "0.00"
        Label406.Text = "Invoice No Generated"
    End Sub

  
    Protected Sub DropDownList27_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList27.SelectedIndexChanged
        If DropDownList27.SelectedValue = "Select" Then
            DropDownList27.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select W_NAME FROM WO_ORDER WHERE PO_NO ='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' AND W_SLNO='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "'"
        '' mc1.CommandText = "select W_NAME FROM WO_ORDER WHERE PO_NO ='w1617000001' AND W_SLNO='1'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox174.Text = dr.Item("W_NAME")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel2.Visible = False
        Panel8.Visible = True
    End Sub
End Class