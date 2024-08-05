Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports System.Net

Public Class mis_sale
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
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
    Dim goAheadFlag As Boolean = True
    Dim partialSuccess As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MultiView1.ActiveViewIndex = 0

            TextBox123.Attributes.Add("readonly", "readonly")
            TextBox96.Attributes.Add("readonly", "readonly")
            TextBox97.Attributes.Add("readonly", "readonly")
            TextBox1.Attributes.Add("readonly", "readonly")
            TextBox2.Attributes.Add("readonly", "readonly")
            TextBox98.Attributes.Add("readonly", "readonly")
            TextBox99.Attributes.Add("readonly", "readonly")
            TextBox175.Attributes.Add("readonly", "readonly")
            TextBox9.Attributes.Add("readonly", "readonly")
            TextBox176.Attributes.Add("readonly", "readonly")
            TextBox104.Attributes.Add("readonly", "readonly")
            TextBox105.Attributes.Add("readonly", "readonly")
            TextBox106.Attributes.Add("readonly", "readonly")
            TextBox111.Attributes.Add("readonly", "readonly")
            TextBox112.Attributes.Add("readonly", "readonly")
            TextBox113.Attributes.Add("readonly", "readonly")
            TextBox114.Attributes.Add("readonly", "readonly")
            TextBox115.Attributes.Add("readonly", "readonly")
            TextBox116.Attributes.Add("readonly", "readonly")
            TextBox117.Attributes.Add("readonly", "readonly")
            TextBox118.Attributes.Add("readonly", "readonly")
            TextBox119.Attributes.Add("readonly", "readonly")
            TextBox120.Attributes.Add("readonly", "readonly")
            TextBox122.Attributes.Add("readonly", "readonly")
            TextBox6.Attributes.Add("readonly", "readonly")
            TextBox8.Attributes.Add("readonly", "readonly")
            TextBox20.Attributes.Add("readonly", "readonly")
            TextBox177.Attributes.Add("readonly", "readonly")
            TextBox95.Attributes.Add("readonly", "readonly")

        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub
    Protected Sub BINDGRID_RAW()
        GridView4.DataSource = DirectCast(ViewState("despatch_raw"), DataTable)
        GridView4.DataBind()
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
        End If
        Dim COUNTER As Integer = 0
        For COUNTER = 0 To GridView4.Rows.Count - 1
            If DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1).Trim = GridView4.Rows(COUNTER).Cells(1).Text Then
                DropDownList12.Focus()
                Return
            End If
        Next


        Dim FINANCE_ARRANGE As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select  ORDER_DETAILS . PAYMENT_MODE  FROM  ORDER_DETAILS   WHERE SO_NO ='" & TextBox123.Text.Trim & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            FINANCE_ARRANGE = dr.Item("PAYMENT_MODE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If FINANCE_ARRANGE = "ADVANCE" Then
            If DropDownList1.SelectedValue = "Select" Then
                DropDownList1.Focus()
                Return
            End If
        ElseIf FINANCE_ARRANGE = "CREDIT" Then

        End If


        ''details search
        Dim pcs_qty, total_weight, ass_price As Decimal
        pcs_qty = 0
        total_weight = 0
        ass_price = 0.0
        Dim UNIT_WEIGHT As Decimal = 0.0
        Dim stock_qty As Decimal = 0.0
        Dim item_qty, unit_rate, discount, pack_forwd, item_cst, item_tcs, terminal_tax, basic_ed, sess_ed, hsess_ed, qty_send, freight_rate As Decimal
        Dim freight_type, ed_type, pack_type, disc_type As String
        freight_type = ""
        ed_type = ""
        pack_type = ""
        disc_type = ""
        Dim ord_au As String = ""
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
            '' TextBox54.Focus()
            Label308.Text = "Qty is Higher Than Stock Qty"
            Return
        Else
            Label308.Text = ""
        End If
        Dim CGST, SGST, IGST, CESS As New Decimal(0.0)
        ''item assessable value calculatation
        Dim mc As New SqlCommand
        conn.Open()
        mc.CommandText = "select MAX(MATERIAL .MAT_AU ) as ORD_AU, SUM(ITEM_SGST) AS ITEM_SGST,SUM(ITEM_CESS) AS ITEM_CESS,SUM(ITEM_CGST) AS ITEM_CGST,SUM(ITEM_IGST) AS ITEM_IGST,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK  ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU  FROM SO_MAT_ORDER JOIN MATERIAL  ON SO_MAT_ORDER .ITEM_CODE =MATERIAL .MAT_CODE JOIN CHPTR_HEADING on (MATERIAL .CHPTR_HEAD = CHPTR_HEADING .CHPT_CODE) where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList11.Text & "' and SO_MAT_ORDER .SO_NO='" & TextBox123.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1) & "' AND AMD_DATE <='" & Today.Year & "-" & Today.Month & "-" & Today.Day & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            ord_au = dr.Item("ORD_AU")
            unit_rate = dr.Item("ITEM_UNIT_RATE")
            discount = dr.Item("ITEM_DISCOUNT")
            disc_type = dr.Item("DISC_TYPE")
            pack_forwd = dr.Item("ITEM_PACK")
            pack_type = dr.Item("PACK_TYPE")
            terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
            item_tcs = dr.Item("ITEM_TCS")
            qty_send = dr.Item("ITEM_QTY_SEND")
            item_qty = dr.Item("ITEM_QTY")
            freight_type = dr.Item("ITEM_FREIGHT_TYPE")
            freight_rate = dr.Item("ITEM_FREIGHT_PU")
            CGST = dr.Item("ITEM_CGST")
            SGST = dr.Item("ITEM_SGST")
            IGST = dr.Item("ITEM_IGST")
            CESS = dr.Item("ITEM_CESS")

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()



        If (item_qty - qty_send) < CDec(TextBox107.Text) Then
            TextBox107.Focus()
            Label308.Text = "Qty is Higher Than Order Bal Qty"
            Return
        Else
            Label308.Text = ""
        End If
        ass_price = CDec(TextBox107.Text) * unit_rate
        Dim row_count As Integer = GridView4.Rows.Count + 1
        Dim dt As DataTable = DirectCast(ViewState("despatch_raw"), DataTable)
        dt.Rows.Add(row_count, DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim, DropDownList12.Text.Substring((DropDownList12.Text.IndexOf(",") + 2)), Label384.Text, TextBox107.Text, FormatNumber(ass_price, 2))
        ViewState("despatch_raw") = dt
        BINDGRID_RAW()
        Dim weight As Decimal = CDec(TextBox107.Text)

        'freight calculation
        TextBox112.Text = FormatNumber(ass_price, 2)
        If freight_type = "N/A" Then
            TextBox113.Text = "0.00"
        ElseIf freight_type = "PERCENTAGE" Then
            TextBox113.Text = FormatNumber((((CDec(TextBox112.Text) * freight_rate) / 100)), 2)
        ElseIf freight_type = "PER UNIT" Then
            TextBox113.Text = FormatNumber((CDec(TextBox107.Text) * freight_rate), 2)
        ElseIf freight_type = "PER MT" Then
            TextBox113.Text = FormatNumber(((weight * freight_rate)), 2)
        End If
        'disc calculation
        If disc_type = "N/A" Then
            TextBox111.Text = "0.00"
        ElseIf disc_type = "PERCENTAGE" Then
            TextBox111.Text = FormatNumber(((CDec(TextBox112.Text) * discount) / 100), 2)
        ElseIf disc_type = "PER UNIT" Then
            TextBox111.Text = FormatNumber((CDec(TextBox107.Text) * discount), 2)
        ElseIf disc_type = "PER MT" Then
            TextBox111.Text = FormatNumber((weight * discount), 2)
        End If
        'pack calculation
        If pack_type = "N/A" Then
            TextBox115.Text = "0.00"
        ElseIf pack_type = "PERCENTAGE" Then
            TextBox115.Text = FormatNumber(CDec(TextBox115.Text) + ((CDec(TextBox112.Text) * pack_forwd) / 100), 2)
        ElseIf pack_type = "PER UNIT" Then
            TextBox115.Text = FormatNumber((CDec(TextBox107.Text) * pack_forwd), 2)
        ElseIf pack_type = "PER MT" Then
            TextBox115.Text = FormatNumber((weight * pack_forwd), 2)
        End If


        ''TERMINAL TAX
        Dim Base_value As New Decimal(0.00)
        Base_value = ((unit_rate * CDec(TextBox107.Text)) - CDec(TextBox111.Text) + CDec(TextBox113.Text) + CDec(TextBox115.Text))
        TextBox117.Text = FormatNumber(CDec(((Base_value * terminal_tax) / 100)), 2)
        TextBox112.Text = Base_value
        TextBox114.Text = FormatNumber(((CDec(TextBox112.Text) + CDec(TextBox117.Text)) * SGST) / 100, 2)
        TextBox116.Text = FormatNumber(((CDec(TextBox112.Text) + CDec(TextBox117.Text)) * CGST / 100), 2)
        TextBox118.Text = FormatNumber(((CDec(TextBox112.Text) + CDec(TextBox117.Text)) * IGST) / 100, 2)
        TextBox120.Text = FormatNumber(((CDec(TextBox112.Text) + CDec(TextBox117.Text)) * CESS / 100), 2)


        TextBox119.Text = FormatNumber(((CDec(TextBox112.Text) + CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text) + CDec(TextBox120.Text) + CDec(TextBox117.Text)) * item_tcs) / 100, 2)
        TextBox122.Text = FormatNumber((CDec(TextBox112.Text) + CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text) + CDec(TextBox120.Text) + CDec(TextBox117.Text) + CDec(TextBox119.Text)), 2)

        If FINANCE_ARRANGE = "ADVANCE" Then
            If DropDownList1.SelectedValue = "Select" Then
                DropDownList1.Focus()
                Return
            End If
            If CDec(TextBox9.Text) < CDec(TextBox122.Text) Then

                DropDownList1.Focus()
                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(5) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("ASS_VALUE")})
                ViewState("despatch_raw") = dt7
                BINDGRID_RAW()
                TextBox111.Text = "0.00"
                TextBox113.Text = "0.00"
                TextBox115.Text = "0.00"
                TextBox117.Text = "0.00"
                TextBox119.Text = "0.00"
                TextBox112.Text = "0.00"
                TextBox114.Text = "0.00"
                TextBox116.Text = "0.00"
                TextBox118.Text = "0.00"
                TextBox120.Text = "0.00"
                TextBox122.Text = "0.00"
                Label308.Text = "Despatch value cannot be more than balance amt."
                Return
            End If

        ElseIf FINANCE_ARRANGE = "CREDIT" Then

        ElseIf FINANCE_ARRANGE = "BG" Then

        End If

        Button44.Enabled = True
    End Sub

    Protected Sub DropDownList11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList11.SelectedIndexChanged
        If DropDownList11.SelectedValue = "Select" Then
            DropDownList11.Focus()
            Return
        End If
        ''SEARCH ITEM CODE
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select DISTINCT (SO_MAT_ORDER.ITEM_CODE + ' , ' + MATERIAL.MAT_NAME) AS ITEM_CODE from SO_MAT_ORDER JOIN MATERIAL ON SO_MAT_ORDER.ITEM_CODE=MATERIAL.MAT_CODE  where SO_MAT_ORDER.item_status='PENDING' and SO_MAT_ORDER.SO_NO='" & TextBox123.Text & "' and SO_MAT_ORDER.ITEM_SLNO='" & DropDownList11.SelectedValue & "' ", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList12.Items.Clear()
        DropDownList12.DataSource = dt
        DropDownList12.DataValueField = "ITEM_CODE"
        DropDownList12.DataBind()
        DropDownList12.Items.Insert(0, "Select")
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
        mc1.CommandText = "select distinct ITEM_VOCAB AS ITEM_VOCAB from SO_MAT_ORDER where ITEM_SLNO='" & DropDownList11.SelectedValue & "' and SO_NO='" & TextBox123.Text & "'"
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

        Dim FINANCE_ARRANGE As String = ""
        conn.Open()
        mc1.CommandText = "select  ORDER_DETAILS . PAYMENT_MODE  FROM  ORDER_DETAILS   WHERE SO_NO ='" & TextBox123.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            FINANCE_ARRANGE = dr.Item("PAYMENT_MODE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If FINANCE_ARRANGE = "ADVANCE" Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT VOUCHER_TYPE + VOUCHER_NO AS VOUCHER_NO FROM SALE_RCD_VOUCHAR WHERE SO_NO ='" & TextBox123.Text & "' AND VOUCHER_STATUS='PENDING' AND ITEM_SLNO ='" & DropDownList11.Text & "' ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.Items.Clear()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "VOUCHER_NO"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            DropDownList1.SelectedValue = "Select"
        Else
            DropDownList1.Items.Clear()
            DropDownList1.Items.Add("N/A")
            DropDownList1.SelectedValue = "N/A"
        End If
    End Sub

    Protected Sub DropDownList12_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList12.SelectedIndexChanged
        If DropDownList12.SelectedValue = "Select" Then
            DropDownList12.Focus()
            Return
        End If


        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE ,MAX(MATERIAL.MAT_NAME) AS ITEM_NAME,MAX(MATERIAL.MAT_AU) AS ITEM_AU, ('....') AS ITEM_WEIGHT,SUM(SO_MAT_ORDER .ITEM_QTY) As ITEM_QTY,(SUM(SO_MAT_ORDER.ITEM_QTY)-SUM(SO_MAT_ORDER.ITEM_QTY_SEND)) AS ITEM_BAL_QTY,MAX(MATERIAL.MAT_STOCK) AS ITEM_B_STOCK,('....') AS ITEM_B_STOCK_MT FROM SO_MAT_ORDER JOIN MATERIAL ON SO_MAT_ORDER .ITEM_CODE = MATERIAL.MAT_CODE where SO_MAT_ORDER .ITEM_CODE='" & DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim & "' AND SO_MAT_ORDER.SO_NO='" & TextBox123.Text & "' AND SO_MAT_ORDER.ITEM_SLNO='" & DropDownList11.SelectedValue & "'", conn)
        'da = New SqlDataAdapter("select MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE ,MAX(F_ITEM .ITEM_NAME) AS ITEM_NAME,MAX(F_ITEM .ITEM_AU) AS ITEM_AU,M_WEIGHT) AS ITEM_WEIGHT,SUM(SO_MAT_ORDER .ITEM_QTY) AS ITEM_QTY,(SUM(SO_MAT_ORDER.ITEM_QTY)-SUM(SO_MAT_ORDER.ITEM_QTY_SEND)) AS ITEM_BAL_QTY,MAX(F_ITEM .ITEM_B_STOC AS ITEM_B_STOCK,convert( AS ITEM_B_STOCK_MT FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE = F_ITEM .ITEM_CODE where SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND SO_MAT_ORDER.SO_NO='" & TextBox124.Text & "' AND SO_MAT_ORDER.ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "'", conn)
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
    Protected Sub BINDGRID2()
        FormView2.DataSource = DirectCast(ViewState("form_view"), DataTable)
        FormView2.DataBind()
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
        If order_type = "Sale Order" Then
            If po_type = "RAW MATERIAL" Or po_type = "STORE MATERIAL" Or po_type = "MISCELLANEOUS" Then
                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(5) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("ASS_VALUE")})
                ViewState("despatch_raw") = dt7
                BINDGRID_RAW()
                ''SEARCH SALE ORDER VENDER DETAILS
                conn.Open()
                mc1.CommandText = "select  dater.d_code,dater.d_name from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox96.Text = dr.Item("d_code")
                    TextBox97.Text = dr.Item("d_name")
                    dr.Close()
                End If
                conn.Close()

                conn.Open()
                mc1.CommandText = "select dater.d_code as consinee_code,dater.d_name as consinee_name from dater join order_details on order_details.CONSIGN_CODE=dater.d_code where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox1.Text = dr.Item("consinee_code")
                    TextBox2.Text = dr.Item("consinee_name")
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
                    ''DropDownList9.Enabled = False
                    DropDownList9.Attributes.Add("disabled", "disabled")
                    DropDownList28.Items.Clear()
                    DropDownList28.Items.Add("N/A")
                    DropDownList28.SelectedValue = "N/A"
                    'DropDownList28.Enabled = False
                    DropDownList28.Attributes.Add("disabled", "disabled")
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
                DropDownList11.Items.Insert(0, "Select")
                DropDownList11.SelectedValue = "Select"
                MultiView1.ActiveViewIndex = 1

            End If
        End If
    End Sub

    Public Function fiscalYearSum(inputString As String)
        Dim sum As Integer = 0
        For Each ch As Char In inputString
            Dim i As Integer
            If Integer.TryParse(ch, i) Then
                sum += i
            End If
        Next

        If (sum > 9) Then
            Return fiscalYearSum(sum.ToString())
        Else
            Return sum
        End If

    End Function
    Protected Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date
                working_date = Today.Date
                If GridView4.Rows.Count = 0 Then
                    Label308.Text = "Please add Material first"
                    Return
                End If

                Dim gst_code, my_gst_code, COMM, DIVISION As New String("")
                conn.Open()
                mycommand.CommandText = "select dater.d_code,dater.d_name,dater.gst_code from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & TextBox123.Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    gst_code = dr.Item("gst_code")
                    dr.Close()
                End If
                conn.Close()

                ''sale order date excise comodity
                Dim so_date As Date
                Dim SO_ACTUAL_DATE As Date
                Dim PARTY_CODE As String = ""
                Dim CONSIGN_CODE As String = ""
                Dim excise_comodity As String = ""
                Dim chptr_heading As String = ""
                Dim mode_of_despatch As String = ""
                Dim mode_of_payment As String = ""
                Dim FINANCE_ARRANGE As String = ""
                Dim PLACE_OF_SUPPLY As String = ""
                Dim ORDER_TO As String = ""
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.DESTINATION,ORDER_DETAILS.CONSIGN_CODE,ORDER_DETAILS.SO_ACTUAL_DATE, ORDER_DETAILS . PAYMENT_MODE , ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PARTY_CODE, ORDER_DETAILS.PAYMENT_TERM,ORDER_DETAILS.MODE_OF_DESPATCH ,MATERIAL .CHPTR_HEAD, CHPTR_HEADING.CHPT_NAME FROM SO_MAT_ORDER JOIN ORDER_DETAILS ON SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO JOIN MATERIAL ON SO_MAT_ORDER .ITEM_CODE =MATERIAL .MAT_CODE JOIN CHPTR_HEADING ON MATERIAL  .CHPTR_HEAD  =CHPTR_HEADING .CHPT_CODE  WHERE SO_MAT_ORDER.ITEM_SLNO  ='" & DropDownList11.Text & "' AND SO_MAT_ORDER .SO_NO ='" & TextBox123.Text.Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORDER_TO = dr.Item("ORDER_TO")
                    excise_comodity = dr.Item("CHPT_NAME")
                    chptr_heading = dr.Item("CHPTR_HEAD")
                    so_date = dr.Item("SO_DATE")
                    FINANCE_ARRANGE = dr.Item("PAYMENT_MODE")
                    PARTY_CODE = dr.Item("PARTY_CODE")
                    CONSIGN_CODE = dr.Item("CONSIGN_CODE")
                    mode_of_despatch = dr.Item("MODE_OF_DESPATCH")
                    mode_of_payment = dr.Item("PAYMENT_TERM")
                    SO_ACTUAL_DATE = dr.Item("SO_ACTUAL_DATE")
                    PLACE_OF_SUPPLY = dr.Item("DESTINATION")

                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
                Dim DESPATCH_TYPE As Integer = 0
                If ORDER_TO = "I.P.T." Then
                    DESPATCH_TYPE = 4
                ElseIf ORDER_TO = "Other" Then
                    DESPATCH_TYPE = 1
                End If

                'search company profile compair gst code
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
                'invoice type
                Dim inv_type, inv_rule, inv_for As String
                If gst_code = my_gst_code Then
                    inv_type = "Delivery Challan(Within same GSTIN)"
                    inv_rule = "(Under Rule 10 of Tax Invoice Credit and Debit Note Rules)"
                    inv_for = "DC15" & CStr(DESPATCH_TYPE)
                Else
                    inv_type = "Tax Invoice"
                    inv_rule = "(Under Rule 1of Tax Invoice Credit and Debit Note Rules)"
                    inv_for = "OS15" & CStr(DESPATCH_TYPE)
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

                Dim prefixFY = fiscalYearSum(STR1)

                conn.Open()
                Dim inv_no As String = ""
                Dim mc_c As New SqlCommand
                mc_c.CommandText = "SELECT (CASE WHEN MAX(INV_NO) IS NULL THEN 0 ELSE MAX(INV_NO) END) as inv_no FROM DESPATCH WHERE D_TYPE LIKE'" & inv_for & "%' AND FISCAL_YEAR =" & STR1
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


                If (Left(inv_for, 2) = "DC") Then
                    If CInt(inv_no) = 0 Then
                        TextBox95.Text = prefixFY & "000001"
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox95.ReadOnly = True
                    Else
                        str = CInt(inv_no) + 1
                        If str.Length = 1 Then
                            str = prefixFY & "00000" & CInt(inv_no) + 1
                        ElseIf str.Length = 2 Then
                            str = prefixFY & "0000" & CInt(inv_no) + 1
                        ElseIf str.Length = 3 Then
                            str = prefixFY & "000" & CInt(inv_no) + 1
                        ElseIf str.Length = 4 Then
                            str = prefixFY & "00" & CInt(inv_no) + 1
                        ElseIf str.Length = 5 Then
                            str = prefixFY & "0" & CInt(inv_no) + 1
                        ElseIf str.Length = 6 Then
                            str = prefixFY & CInt(inv_no) + 1
                        ElseIf str.Length = 7 Then
                            str = CInt(inv_no) + 1
                        End If
                        TextBox95.Text = str
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox95.ReadOnly = True
                    End If
                Else
                    If CInt(inv_no) = 0 Then
                        TextBox95.Text = "0000001"
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox95.ReadOnly = True
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
                        TextBox95.Text = str
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox95.ReadOnly = True
                    End If
                End If




                Dim transporter, trans_sl_no As New String("")
                If DropDownList9.SelectedValue = "PARTY" Or DropDownList9.SelectedValue = "N/A" Then
                    transporter = DropDownList9.Text.Trim
                Else
                    transporter = DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim
                End If
                If DropDownList28.SelectedValue <> "N/A" Then
                    trans_sl_no = DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1).Trim
                Else
                    trans_sl_no = DropDownList28.Text.Trim
                End If




                Dim weight As Decimal = 0.0
                Dim i As Integer = 0
                Dim ass_price As Decimal = 0.0
                If (Label384.Text = "Pcs" Or Label384.Text = "PCS" Or Label384.Text = "NOS" Or Label384.Text = "SET" Or Label384.Text = "LOT") Then
                    For i = 0 To GridView4.Rows.Count - 1
                        weight = 0.00
                        ass_price = ass_price + CDec(GridView4.Rows(i).Cells(5).Text)
                    Next
                Else
                    For i = 0 To GridView4.Rows.Count - 1
                        weight = CDec(GridView4.Rows(i).Cells(4).Text) + weight
                        ass_price = ass_price + CDec(GridView4.Rows(i).Cells(5).Text)
                    Next
                End If

                ''item assessable value calculatation
                Dim item_qty, unit_rate, discount, IGST, CGST, SGST, item_cess, pack_forwd, item_tcs, terminal_tax, qty_send, freight_rate As Decimal
                Dim freight_type, pack_type, actual_so, DISC_TYPE As New String("")
                conn.Open()
                Dim mc As New SqlCommand
                mc.CommandText = "select MAX(ORDER_DETAILS .SO_ACTUAL ) AS SO_ACTUAL, MAX(MATERIAL .MAT_AU ) as ORD_AU, SUM(ITEM_SGST) AS ITEM_SGST,SUM(ITEM_CESS) AS ITEM_CESS,SUM(ITEM_CGST) AS ITEM_CGST,SUM(ITEM_IGST) AS ITEM_IGST,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK  ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU  FROM SO_MAT_ORDER JOIN MATERIAL  ON SO_MAT_ORDER .ITEM_CODE =MATERIAL .MAT_CODE  JOIN CHPTR_HEADING on MATERIAL .CHPTR_HEAD  =CHPTR_HEADING .CHPT_CODE JOIN ORDER_DETAILS ON SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO  where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList11.Text & "' and SO_MAT_ORDER .SO_NO='" & TextBox123.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1) & "' AND AMD_DATE <='" & Today.Year & "-" & Today.Month & "-" & Today.Day & "'"
                mc.Connection = conn
                dr = mc.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    CGST = dr.Item("ITEM_CGST")
                    SGST = dr.Item("ITEM_SGST")
                    IGST = dr.Item("ITEM_IGST")
                    item_cess = dr.Item("ITEM_CESS")
                    unit_rate = dr.Item("ITEM_UNIT_RATE")
                    discount = dr.Item("ITEM_DISCOUNT")
                    pack_forwd = dr.Item("ITEM_PACK")
                    terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
                    item_tcs = dr.Item("ITEM_TCS")
                    qty_send = dr.Item("ITEM_QTY_SEND")
                    item_qty = dr.Item("ITEM_QTY")
                    freight_type = dr.Item("ITEM_FREIGHT_TYPE")
                    freight_rate = dr.Item("ITEM_FREIGHT_PU")
                    pack_type = dr.Item("PACK_TYPE")
                    DISC_TYPE = dr.Item("DISC_TYPE")
                    actual_so = dr.Item("SO_ACTUAL")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()


                If freight_type = "PERCENTAGE" Then
                    freight_type = "/%"
                ElseIf freight_type = "PER UNIT" Then
                    freight_type = "/Unit"
                ElseIf freight_type = "PER MT" Then
                    freight_type = "/Mt"
                End If

                If pack_type = "PERCENTAGE" Then
                    pack_type = " %"
                ElseIf pack_type = "PER MT" Then
                    pack_type = "/Mt"
                ElseIf pack_type = "PER UNIT" Then
                    pack_type = "/Unit"
                End If

                If DISC_TYPE = "PERCENTAGE" Then
                    DISC_TYPE = " %"
                ElseIf DISC_TYPE = "PER MT" Then
                    DISC_TYPE = "/Mt"
                ElseIf DISC_TYPE = "PER UNIT" Then
                    DISC_TYPE = "/Unit"
                End If

                Dim BILL_PARTY_ADD, SHIP_PARTY_ADD, BILL_PARTY_state_code, BILL_PARTY_state, SHIP_PARTY_state_code, SHIP_PARTY_state, BILL_PARTY_GST, BILL_PARTY_CODE, SHIP_PARTY_GST, SHIP_PARTY_CODE As New String("")
                'BILL TO PARTY ADDRESS SEARCH
                conn.Open()
                'mc1.CommandText = "select d_name + ' , ' + add_1 + ' , ' + add_2 + ' , ' + ecc_no + ' , ' + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select PARTY_CODE  from ORDER_DETAILS where SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "')"
                mc1.CommandText = "select d_name + ' , ' + CHAR(10) + add_1 + ' , ' + CHAR(10) + add_2 + ' , ' + CHAR(10) + ecc_no + ' , ' + CHAR(10) + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select PARTY_CODE  from ORDER_DETAILS where SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "')"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    BILL_PARTY_CODE = dr.Item("d_code")
                    BILL_PARTY_ADD = dr.Item("party_details")
                    BILL_PARTY_GST = dr.Item("gst_code")
                    BILL_PARTY_state = dr.Item("d_state")
                    BILL_PARTY_state_code = dr.Item("d_state_code")
                    dr.Close()
                End If
                conn.Close()
                'SHIP TO PARTY ADDRESS SEARCH
                conn.Open()
                'mc1.CommandText = "select d_name + ' , ' + add_1 + ' , ' + add_2 + ' , ' + ecc_no + ' , ' + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select CONSIGN_CODE  from ORDER_DETAILS where SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "')"
                mc1.CommandText = "select d_name + ' , ' + CHAR(10) + add_1 + ' , ' + CHAR(10) + add_2 + ' , ' + CHAR(10) + ecc_no + ' , ' + CHAR(10) + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select PARTY_CODE  from ORDER_DETAILS where SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "')"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    SHIP_PARTY_CODE = dr.Item("d_code")
                    SHIP_PARTY_ADD = dr.Item("party_details")
                    SHIP_PARTY_GST = dr.Item("gst_code")
                    SHIP_PARTY_state = dr.Item("d_state")
                    SHIP_PARTY_state_code = dr.Item("d_state_code")
                    dr.Close()
                End If
                conn.Close()
                'TOTAL PCS CALCULATION
                Dim total_pcs As Integer = 0
                If (Label384.Text = "Pcs" Or Label384.Text = "PCS") Then
                    total_pcs = CInt(TextBox107.Text)
                Else
                    total_pcs = 0
                End If
                '' calculation

                Dim price_unit, total_rate_unit, taxable_rate_unit, total_amt As New Decimal(0.0)
                price_unit = FormatNumber((ass_price + CDec(TextBox115.Text)) / CDec(TextBox107.Text), 4)
                total_rate_unit = FormatNumber((ass_price + CDec(TextBox115.Text) + CDec(TextBox113.Text)) / CDec(TextBox107.Text), 4)
                taxable_rate_unit = FormatNumber((ass_price + CDec(TextBox115.Text) + CDec(TextBox113.Text) - CDec(TextBox111.Text)) / CDec(TextBox107.Text), 4)
                total_amt = CDec(TextBox122.Text)

                Dim TAXABLE_VALUE As Decimal = 0
                TAXABLE_VALUE = CDec(TextBox112.Text)
                Dim ADV_AMT, ADV_GST, NET_PAY As New Decimal(0.0)
                If FINANCE_ARRANGE = "ADVANCE" Then
                    ADV_AMT = ass_price - CDec(TextBox111.Text) + CDec(TextBox117.Text) + CDec(TextBox119.Text)
                    ADV_GST = CDec(TextBox114.Text) + CDec(TextBox116.Text) + CDec(TextBox118.Text) + CDec(TextBox120.Text)
                    NET_PAY = 0.0
                Else
                    ADV_AMT = 0.0
                    ADV_GST = 0.0
                    NET_PAY = CDec(TextBox122.Text)
                End If


                'conn.Open()
                Dim QUARY1 As String = ""
                QUARY1 = "Insert Into DESPATCH(TCS_TAX,TCS_AMT,INV_STATUS,SO_NO ,SO_DATE ,PO_NO ,PO_DATE ,AMD_NO ,AMD_DATE ,TRANS_WO ,TRANS_SLNO ,TRANS_NAME ,TRUCK_NO ,PARTY_CODE ,
                    CONSIGN_CODE ,MAT_VOCAB ,MAT_SLNO ,INV_NO,INV_DATE,D_TYPE,CHPTR_HEAD ,FISCAL_YEAR,INV_ISSUE ,PLACE_OF_SUPPLY ,BILL_PARTY_ADD ,CON_PARTY_ADD ,B_STATE ,
                    B_STATE_CODE ,C_STATE ,C_STATE_CODE ,BILL_PARTY_GST_N ,CON_PARTY_GST_N ,NEGOTIATING_BRANCH ,PAYING_AUTH ,RR_NO ,RR_DATE ,TOTAL_WEIGHT ,ACC_UNIT ,
                    PURITY ,TC_NO ,FINANCE_ARRENGE ,MILL_CODE ,DA_NO ,CONTRACT_NO ,RCD_VOUCHER_NO ,RCD_VOUCHER_DATE ,ROUT_CARD_NO ,DESPATCH_TYPE ,TAX_REVERS_CHARGE ,RLY_INV_NO ,
                    RLY_INV_DATE ,FRT_WT_AMT ,TOTAL_PCS,P_CODE ,P_DESC ,D1 ,D2 ,D3 ,D4 ,BASE_PRICE ,PACK_PRICE ,PACK_TYPE ,QLTY_PRICE ,SEC_PRICE ,TOTAL_TDC ,UNIT_PRICE ,
                    SY_MARGIN ,PPM_FRT ,FRT_TYPE ,RLY_ROAD_FRT ,TOTAL_RATE_UNIT ,REBATE_UNIT ,REBATE_TYPE ,TAXABLE_RATE_UNIT ,TOTAL_QTY ,TAXABLE_VALUE ,CGST_RATE ,CGST_AMT ,
                    SGST_RATE ,SGST_AMT ,IGST_RATE ,IGST_AMT ,CESS_RATE ,CESS_AMT ,TERM_RATE ,TERM_AMT ,TOTAL_AMT ,LESS_LOAD_AMT ,TOTAL_BAG ,ADVANCE_PAID ,GST_PAID_ADV ,NET_PAY ,
                    NOTIFICATION_TEXT ,COMM ,DIV_ADD ,INV_TYPE ,INV_RULE ,FORM_NAME,EMP_ID)values(@TCS_TAX,@TCS_AMT,@INV_STATUS,@SO_NO ,@SO_DATE ,@PO_NO ,@PO_DATE ,@AMD_NO ,
                    @AMD_DATE ,@TRANS_WO ,@TRANS_SLNO ,@TRANS_NAME ,@TRUCK_NO ,@PARTY_CODE ,@CONSIGN_CODE ,@MAT_VOCAB ,@MAT_SLNO ,@INV_NO,@INV_DATE,@D_TYPE,@CHPTR_HEAD ,@FISCAL_YEAR,
                    @INV_ISSUE ,@PLACE_OF_SUPPLY ,@BILL_PARTY_ADD ,@CON_PARTY_ADD ,@B_STATE ,@B_STATE_CODE ,@C_STATE ,@C_STATE_CODE ,@BILL_PARTY_GST_N ,@CON_PARTY_GST_N ,@NEGOTIATING_BRANCH,
                    @PAYING_AUTH ,@RR_NO ,@RR_DATE ,@TOTAL_WEIGHT ,@ACC_UNIT ,@PURITY ,@TC_NO ,@FINANCE_ARRENGE ,@MILL_CODE ,@DA_NO ,@CONTRACT_NO ,@RCD_VOUCHER_NO ,@RCD_VOUCHER_DATE ,
                    @ROUT_CARD_NO ,@DESPATCH_TYPE ,@TAX_REVERS_CHARGE ,@RLY_INV_NO ,@RLY_INV_DATE ,@FRT_WT_AMT ,@TOTAL_PCS,@P_CODE ,@P_DESC ,@D1 ,@D2 ,@D3 ,@D4 ,@BASE_PRICE ,@PACK_PRICE ,
                    @PACK_TYPE ,@QLTY_PRICE ,@SEC_PRICE ,@TOTAL_TDC ,@UNIT_PRICE ,@SY_MARGIN ,@PPM_FRT ,@FRT_TYPE ,@RLY_ROAD_FRT ,@TOTAL_RATE_UNIT ,@REBATE_UNIT ,@REBATE_TYPE ,@TAXABLE_RATE_UNIT ,
                    @TOTAL_QTY ,@TAXABLE_VALUE ,@CGST_RATE ,@CGST_AMT ,@SGST_RATE ,@SGST_AMT ,@IGST_RATE ,@IGST_AMT ,@CESS_RATE ,@CESS_AMT ,@TERM_RATE ,@TERM_AMT ,@TOTAL_AMT ,@LESS_LOAD_AMT ,@TOTAL_BAG ,
                    @ADVANCE_PAID ,@GST_PAID_ADV ,@NET_PAY ,@NOTIFICATION_TEXT ,@COMM ,@DIV_ADD ,@INV_TYPE ,@INV_RULE ,@FORM_NAME,@EMP_ID)"
                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@SO_NO", TextBox123.Text)
                cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@PO_NO", actual_so)
                cmd1.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(SO_ACTUAL_DATE.Day & "-" & SO_ACTUAL_DATE.Month & "-" & SO_ACTUAL_DATE.Year), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@AMD_NO", TextBox105.Text)
                cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(TextBox106.Text, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@TRANS_WO", transporter)
                cmd1.Parameters.AddWithValue("@TRANS_SLNO", trans_sl_no)
                cmd1.Parameters.AddWithValue("@TRANS_NAME", TextBox99.Text)
                cmd1.Parameters.AddWithValue("@TRUCK_NO", TextBox100.Text)
                cmd1.Parameters.AddWithValue("@ED_COMDT", excise_comodity)
                cmd1.Parameters.AddWithValue("@PARTY_CODE", PARTY_CODE)
                cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CONSIGN_CODE)
                cmd1.Parameters.AddWithValue("@MAT_VOCAB", TextBox104.Text)
                cmd1.Parameters.AddWithValue("@MAT_SLNO", DropDownList11.Text)
                cmd1.Parameters.AddWithValue("@INV_NO", TextBox95.Text)
                cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@CHPTR_HEAD", chptr_heading)
                cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                cmd1.Parameters.AddWithValue("@INV_ISSUE", Now)
                cmd1.Parameters.AddWithValue("@PLACE_OF_SUPPLY", PLACE_OF_SUPPLY)
                cmd1.Parameters.AddWithValue("@BILL_PARTY_ADD", BILL_PARTY_ADD)
                cmd1.Parameters.AddWithValue("@CON_PARTY_ADD", SHIP_PARTY_ADD)
                cmd1.Parameters.AddWithValue("@B_STATE", BILL_PARTY_state)
                cmd1.Parameters.AddWithValue("@B_STATE_CODE", BILL_PARTY_state_code)
                cmd1.Parameters.AddWithValue("@C_STATE", SHIP_PARTY_state)
                cmd1.Parameters.AddWithValue("@C_STATE_CODE", SHIP_PARTY_state_code)
                cmd1.Parameters.AddWithValue("@BILL_PARTY_GST_N", BILL_PARTY_GST)
                cmd1.Parameters.AddWithValue("@CON_PARTY_GST_N", SHIP_PARTY_GST)
                cmd1.Parameters.AddWithValue("@NEGOTIATING_BRANCH", "")
                cmd1.Parameters.AddWithValue("@PAYING_AUTH", "")
                cmd1.Parameters.AddWithValue("@RR_NO", "")
                cmd1.Parameters.AddWithValue("@RR_DATE", "")
                cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT ", weight)
                cmd1.Parameters.AddWithValue("@D_TYPE", inv_for)
                cmd1.Parameters.AddWithValue("@ACC_UNIT", Label384.Text)
                cmd1.Parameters.AddWithValue("@PURITY", 0.0)
                cmd1.Parameters.AddWithValue("@TC_NO", "")
                cmd1.Parameters.AddWithValue("@FINANCE_ARRENGE", FINANCE_ARRANGE)
                cmd1.Parameters.AddWithValue("@MILL_CODE", "")
                cmd1.Parameters.AddWithValue("@DA_NO", "")
                cmd1.Parameters.AddWithValue("@CONTRACT_NO", "")
                cmd1.Parameters.AddWithValue("@RCD_VOUCHER_NO", DropDownList1.SelectedValue)
                cmd1.Parameters.AddWithValue("@RCD_VOUCHER_DATE", TextBox176.Text)
                cmd1.Parameters.AddWithValue("@ROUT_CARD_NO", "")
                cmd1.Parameters.AddWithValue("@DESPATCH_TYPE", "BY ROAD")
                cmd1.Parameters.AddWithValue("@TAX_REVERS_CHARGE", "YES")
                cmd1.Parameters.AddWithValue("@RLY_INV_NO", "")
                cmd1.Parameters.AddWithValue("@RLY_INV_DATE", "")
                cmd1.Parameters.AddWithValue("@FRT_WT_AMT", 0.0)
                cmd1.Parameters.AddWithValue("@TOTAL_PCS", total_pcs)
                cmd1.Parameters.AddWithValue("@P_CODE ", DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1).Trim)
                cmd1.Parameters.AddWithValue("@P_DESC", DropDownList12.Text.Substring(DropDownList12.Text.IndexOf(",") + 1).Trim)
                cmd1.Parameters.AddWithValue("@D1", "")
                cmd1.Parameters.AddWithValue("@D2", "")
                cmd1.Parameters.AddWithValue("@D3", "")
                cmd1.Parameters.AddWithValue("@D4", "")
                cmd1.Parameters.AddWithValue("@PACK_TYPE", pack_type)
                cmd1.Parameters.AddWithValue("@FRT_TYPE", freight_type)
                cmd1.Parameters.AddWithValue("@REBATE_TYPE ", DISC_TYPE)
                cmd1.Parameters.AddWithValue("@TOTAL_QTY", CDec(TextBox107.Text))
                cmd1.Parameters.AddWithValue("@BASE_PRICE", unit_rate)
                cmd1.Parameters.AddWithValue("@PACK_PRICE", pack_forwd)
                cmd1.Parameters.AddWithValue("@QLTY_PRICE", 0.0)
                cmd1.Parameters.AddWithValue("@SEC_PRICE", 0.0)
                cmd1.Parameters.AddWithValue("@TOTAL_TDC", 0.0)
                cmd1.Parameters.AddWithValue("@UNIT_PRICE", price_unit)
                cmd1.Parameters.AddWithValue("@SY_MARGIN", 0.0)
                cmd1.Parameters.AddWithValue("@PPM_FRT", 0.0)
                cmd1.Parameters.AddWithValue("@RLY_ROAD_FRT", freight_rate)
                cmd1.Parameters.AddWithValue("@TOTAL_RATE_UNIT", total_rate_unit)
                cmd1.Parameters.AddWithValue("@REBATE_UNIT", discount)
                cmd1.Parameters.AddWithValue("@TAXABLE_RATE_UNIT", taxable_rate_unit)
                cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", TAXABLE_VALUE)
                cmd1.Parameters.AddWithValue("@CGST_RATE", CGST)
                cmd1.Parameters.AddWithValue("@CGST_AMT", CDec(TextBox116.Text))
                cmd1.Parameters.AddWithValue("@SGST_RATE", SGST)
                cmd1.Parameters.AddWithValue("@SGST_AMT", CDec(TextBox114.Text))
                cmd1.Parameters.AddWithValue("@IGST_RATE", IGST)
                cmd1.Parameters.AddWithValue("@IGST_AMT", CDec(TextBox118.Text))
                cmd1.Parameters.AddWithValue("@CESS_RATE", item_cess)
                cmd1.Parameters.AddWithValue("@CESS_AMT", CDec(TextBox120.Text))
                cmd1.Parameters.AddWithValue("@TERM_RATE", terminal_tax)
                cmd1.Parameters.AddWithValue("@TERM_AMT", CDec(TextBox117.Text))
                cmd1.Parameters.AddWithValue("@TOTAL_AMT", total_amt)
                cmd1.Parameters.AddWithValue("@LESS_LOAD_AMT", 0.0)
                cmd1.Parameters.AddWithValue("@ADVANCE_PAID", ADV_AMT)
                cmd1.Parameters.AddWithValue("@GST_PAID_ADV", ADV_GST)
                cmd1.Parameters.AddWithValue("@NET_PAY", NET_PAY)
                cmd1.Parameters.AddWithValue("@COMM", COMM)
                cmd1.Parameters.AddWithValue("@DIV_ADD", DIVISION)
                cmd1.Parameters.AddWithValue("@INV_TYPE", inv_type)
                cmd1.Parameters.AddWithValue("@INV_RULE", inv_rule)
                cmd1.Parameters.AddWithValue("@FORM_NAME", DropDownList10.Text)
                cmd1.Parameters.AddWithValue("@TOTAL_BAG", 0)
                cmd1.Parameters.AddWithValue("@NOTIFICATION_TEXT", TextBox103.Text)
                cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                cmd1.Parameters.AddWithValue("@INV_STATUS", "ACTIVE")
                cmd1.Parameters.AddWithValue("@TCS_TAX", item_tcs)
                cmd1.Parameters.AddWithValue("@TCS_AMT", CDec(TextBox119.Text))
                cmd1.ExecuteReader()
                cmd1.Dispose()
                'conn.Close()
                ''update sale order update f_item
                Dim lp As Integer = 0
                For lp = 0 To GridView4.Rows.Count - 1
                    'material avg price
                    Dim conn1 As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
                    Dim STOCK_QTY, AVG_PRICE As Decimal
                    conn1.Open()
                    mc.CommandText = "select * from MATERIAL where MAT_CODE = '" & GridView4.Rows(lp).Cells(1).Text & "'"
                    mc.Connection = conn1
                    dr = mc.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        STOCK_QTY = dr.Item("MAT_STOCK")
                        AVG_PRICE = dr.Item("MAT_AVG")
                        dr.Close()
                        conn1.Close()
                    Else
                        conn1.Close()
                    End If

                    Dim NEW_AVG_PRICE, NEW_UNIT_RATE, NEW_MAT_VALUE As Decimal
                    NEW_MAT_VALUE = CDec(GridView4.Rows(lp).Cells(5).Text)
                    NEW_UNIT_RATE = CDec(FormatNumber(NEW_MAT_VALUE / CDec(GridView4.Rows(lp).Cells(4).Text), 3))
                    If ((STOCK_QTY - CDec(GridView4.Rows(lp).Cells(4).Text)) = 0) Then
                        NEW_AVG_PRICE = AVG_PRICE
                    Else
                        NEW_AVG_PRICE = FormatNumber(((STOCK_QTY * AVG_PRICE) - NEW_MAT_VALUE) / (STOCK_QTY - CDec(GridView4.Rows(lp).Cells(4).Text)), 3)
                    End If

                    'conn1.Open()
                    QUARY1 = ""
                    QUARY1 = "update MATERIAL set MAT_STOCK =MAT_STOCK - ((" & CDec(GridView4.Rows(lp).Cells(4).Text) & ")),LAST_ISSUE_DATE = @ITEM_LAST_DESPATCH,LAST_TRANS_DATE=@ITEM_LAST_DESPATCH ,MAT_AVG= @MAT_AVG where MAT_CODE ='" & GridView4.Rows(lp).Cells(1).Text & "'"
                    Dim cmd2 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmd2.Parameters.AddWithValue("@ITEM_LAST_DESPATCH", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                    cmd2.Parameters.AddWithValue("@MAT_AVG", NEW_AVG_PRICE)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()
                    'conn1.Close()
                    ''UPDATE TRANSPORTER
                    If (DropDownList9.SelectedValue <> "N/A" And DropDownList9.SelectedValue <> "PARTY") Then
                        'conn.Open()
                        QUARY1 = ""
                        'QUARY1 = "update WO_ORDER set W_COMPLITED =W_COMPLITED + (" & FormatNumber(CDec(GridView4.Rows(lp).Cells(4).Text), 3) & ") where PO_NO ='" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1) & "'"
                        QUARY1 = "update WO_ORDER set W_COMPLITED =W_COMPLITED + " & FormatNumber(CDec(GridView4.Rows(lp).Cells(4).Text), 3) & " where PO_NO='" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1) & "' AND W_SLNO='" & DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1) & "'  and AMD_DATE =(select max(AMD_DATE) from WO_ORDER where PO_NO ='" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1) & "' and W_SLNO = '" & DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1) & "' and AMD_DATE < ='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "')"
                        Dim TRANS As New SqlCommand(QUARY1, conn_trans, myTrans)
                        TRANS.ExecuteReader()
                        TRANS.Dispose()
                        'conn.Close()
                    End If
                    ''update so order
                    'conn.Open()
                    QUARY1 = ""
                    QUARY1 = "update SO_MAT_ORDER set ITEM_QTY_SEND =(ITEM_QTY_SEND + (" & CDec(GridView4.Rows(lp).Cells(4).Text) & ")) where SO_NO ='" & TextBox123.Text & "' and ITEM_SLNO ='" & DropDownList11.SelectedValue & "' and ITEM_CODE ='" & GridView4.Rows(lp).Cells(1).Text & "' AND AMD_NO='" & TextBox105.Text & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()
                    'conn.Close()
                    ''INSERT MAT_DETAILS
                    conn1.Open()
                    Dim mcc As New SqlCommand
                    Dim mat_stock As Decimal = 0
                    mcc.CommandText = "SELECT MAT_STOCK FROM MATERIAL WITH(NOLOCK) WHERE MAT_CODE='" & GridView4.Rows(lp).Cells(1).Text & "'"
                    mcc.Connection = conn1
                    dr = mcc.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        mat_stock = dr.Item("MAT_STOCK")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn1.Close()
                    ''LINE NO
                    Dim LINO_SL As Integer = 0
                    conn1.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & GridView4.Rows(lp).Cells(1).Text & "' AND LINE_NO <> 0", conn1)
                    LINO_SL = da.Fill(ds, "MAT_DETAILS")
                    conn1.Close()
                    'MATERIAL DETAILS SEARCH IN MATERIAL TABLE

                    'MATERIAL DETAILS ADD
                    'conn1.Open()
                    Dim Query As String = "Insert Into MAT_DETAILS(MAT_SL_NO,ENTRY_DATE,MAT_QTY,LINE_DATE,ISSUE_NO,LINE_NO,FISCAL_YEAR,LINE_TYPE,MAT_CODE,RQD_QTY,ISSUE_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,PURPOSE,COST_CODE,AUTH_BY,ISSUE_TYPE,RQD_DATE)VALUES(@MAT_SL_NO,@ENTRY_DATE,@MAT_QTY,@LINE_DATE,@ISSUE_NO,@LINE_NO,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@RQD_QTY,@ISSUE_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@PURPOSE,@COST_CODE,@AUTH_BY,@ISSUE_TYPE,@RQD_DATE)"
                    Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@ISSUE_NO", inv_for & TextBox95.Text)
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
                    cmd.Parameters.AddWithValue("@TOTAL_PRICE", CDec(GridView4.Rows(lp).Cells(5).Text))
                    cmd.Parameters.AddWithValue("@PURPOSE", "SALE")
                    cmd.Parameters.AddWithValue("@COST_CODE", TextBox96.Text)
                    cmd.Parameters.AddWithValue("@AUTH_BY", Session("userName"))
                    cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd.Parameters.AddWithValue("@MAT_SL_NO", DropDownList11.SelectedValue)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn1.Close()
                Next

                ''search ac head
                Dim IUCA As String = ""
                Dim FREIGHT As String = ""
                Dim CGST_HEAD As String = ""
                Dim IGST_HEAD As String = ""
                Dim VAT As String = ""
                Dim CST_HEAD As String = ""
                Dim TERMINAL As String = ""
                Dim TCS As String = ""
                Dim STOCK_HEAD As String = ""
                Dim CESS_HEAD As String = ""
                Dim SGST_HEAD As String = ""

                Dim lcgst, lsgst, ligst, lcess, adv_pay_head, gst_exp As New String("")
                conn.Open()
                Dim MC5 As New SqlCommand
                MC5.CommandText = "select work_group.TCS_OUTGOING,work_group.gst_expenditure,ORDER_DETAILS .SO_NO ,ORDER_DETAILS .ORDER_TO, dater.stock_ac_head,dater.iuca_head,work_group.adv_pay,work_group.lcgst,work_group .lsgst,work_group .ligst ,work_group .lcess,work_group.cgst,work_group.sgst,work_group.igst,work_group.cess, work_group.ed_head,work_group.vat_head,work_group.cst_head,work_group.freight_head,work_group.term_tax,work_group.tds_head,work_group.pack_head from ORDER_DETAILS join DATER on ORDER_DETAILS .PARTY_CODE=DATER.d_code JOIN work_group on ORDER_DETAILS.ORDER_TYPE =work_group.work_name and  ORDER_DETAILS.PO_TYPE  =work_group.work_type and ORDER_DETAILS.ORDER_TO  =work_group.d_type WHERE ORDER_DETAILS.SO_NO='" & TextBox123.Text & "'"
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORDER_TO = dr.Item("ORDER_TO")
                    IUCA = dr.Item("iuca_head")
                    FREIGHT = dr.Item("freight_head")
                    CGST_HEAD = dr.Item("CGST")
                    IGST_HEAD = dr.Item("IGST")
                    VAT = dr.Item("vat_head")
                    CST_HEAD = dr.Item("cst_head")
                    TERMINAL = dr.Item("term_tax")
                    TCS = dr.Item("TCS_OUTGOING")
                    CESS_HEAD = dr.Item("CESS")
                    SGST_HEAD = dr.Item("SGST")
                    lcgst = dr.Item("lcgst")
                    lsgst = dr.Item("lsgst")
                    ligst = dr.Item("ligst")
                    lcess = dr.Item("lcess")
                    adv_pay_head = dr.Item("adv_pay")
                    gst_exp = dr.Item("gst_expenditure")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                conn.Open()
                MC5.CommandText = "SELECT AC_ISSUE FROM MATERIAL WITH(NOLOCK) WHERE MAT_CODE LIKE '" & DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Trim & "'"
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    STOCK_HEAD = dr.Item("AC_ISSUE")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If

                If ORDER_TO = "I.P.T." Then
                    If FINANCE_ARRANGE = "Book Adjustment" Or FINANCE_ARRANGE = "CREDIT" Then
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox115.Text) - CDec(TextBox111.Text), "STOCK TRANSFOR")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, IUCA, "Dr", CDec(TextBox122.Text), "IUCA")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, FREIGHT, "Cr", CDec(TextBox113.Text), "FREIGHT")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lsgst, "Cr", CDec(TextBox114.Text), "SGST_PAYABLE")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcgst, "Cr", CDec(TextBox116.Text), "CGST_PAYABLE")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, ligst, "Cr", CDec(TextBox118.Text), "IGST_PAYABLE")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcess, "Cr", CDec(TextBox120.Text), "CESS_PAYABLE")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TERMINAL, "Cr", CDec(TextBox117.Text), "TERMINAL TAX")
                        save_ledger("", TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TCS, "Cr", CDec(TextBox119.Text), "TCS_OUTPUT")

                    End If

                ElseIf ORDER_TO = "Other" Then
                    If FINANCE_ARRANGE = "ADVANCE" Then
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox115.Text) - CDec(TextBox111.Text), "STOCK TRANSFOR")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, adv_pay_head, "Dr", CDec(TextBox122.Text), "ADV_PAY")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, FREIGHT, "Cr", CDec(TextBox113.Text), "FREIGHT")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lsgst, "Cr", CDec(TextBox114.Text), "SGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcgst, "Cr", CDec(TextBox116.Text), "CGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, ligst, "Cr", CDec(TextBox118.Text), "IGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcess, "Cr", CDec(TextBox120.Text), "CESS_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TERMINAL, "Cr", CDec(TextBox117.Text), "TERMINAL TAX")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TCS, "Cr", CDec(TextBox119.Text), "TCS_OUTPUT")

                    ElseIf FINANCE_ARRANGE = "RETURNABLE BASIS" Then

                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox115.Text) - CDec(TextBox111.Text), "STOCK TRANSFOR")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, "62203", "Dr", CDec(TextBox122.Text), "SUND_DEBTOR_OTHER")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, FREIGHT, "Cr", CDec(TextBox113.Text), "FREIGHT")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lsgst, "Cr", CDec(TextBox114.Text), "SGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcgst, "Cr", CDec(TextBox116.Text), "CGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, ligst, "Cr", CDec(TextBox118.Text), "IGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcess, "Cr", CDec(TextBox120.Text), "CESS_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TERMINAL, "Cr", CDec(TextBox117.Text), "TERMINAL TAX")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TCS, "Cr", CDec(TextBox119.Text), "TCS_OUTPUT")

                    ElseIf FINANCE_ARRANGE = "BUY BACK" Then
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox115.Text) - CDec(TextBox111.Text), "STOCK TRANSFOR")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, FREIGHT, "Cr", CDec(TextBox113.Text), "FREIGHT")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lsgst, "Cr", CDec(TextBox114.Text), "SGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcgst, "Cr", CDec(TextBox116.Text), "CGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, ligst, "Cr", CDec(TextBox118.Text), "IGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, lcess, "Cr", CDec(TextBox120.Text), "CESS_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TERMINAL, "Cr", CDec(TextBox117.Text), "TERMINAL TAX")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, TCS, "Cr", CDec(TextBox119.Text), "TCS_OUTPUT")
                        save_ledger(DropDownList1.SelectedValue, TextBox123.Text, inv_for & TextBox95.Text, TextBox96.Text, adv_pay_head, "Dr", CDec(TextBox122.Text), "BUY_BACK")
                    End If

                End If

                'UPDATE SALE_RCD_VOUCHER
                If DropDownList1.SelectedValue <> "N/A" Then
                    'conn.Open()
                    QUARY1 = ""
                    QUARY1 = "UPDATE SALE_RCD_VOUCHAR SET BAL_AMT =BAL_AMT- " & CDec(TextBox122.Text) & ",CGST_BAL =CGST_BAL - " & CDec(TextBox116.Text) & ",SGST_BAL =SGST_BAL- " & CDec(TextBox114.Text) & ",IGST_BAL =IGST_BAL- " & CDec(TextBox118.Text) & ",CESS_BAL =CESS_BAL- " & CDec(TextBox120.Text) & ",TT_TAX_BAL=TT_TAX_BAL - " & CDec(TextBox117.Text) & " WHERE VOUCHER_TYPE + VOUCHER_NO ='" & DropDownList1.Text & "' AND SO_NO ='" & TextBox123.Text & "' AND ITEM_SLNO ='" & DropDownList11.Text & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()
                    'conn.Close()
                End If
                If transporter = "N/A" Or transporter = "PARTY" Then
                    ''insert inv_print
                    'conn.Open()
                    QUARY1 = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
                    Dim scmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    scmd1.Parameters.AddWithValue("@INV_NO", inv_for & TextBox95.Text)
                    scmd1.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
                    scmd1.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
                    scmd1.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
                    scmd1.Parameters.AddWithValue("@F_YEAR", STR1)
                    scmd1.ExecuteReader()
                    scmd1.Dispose()
                    'conn.Close()
                Else
                    ''INSERT MB_BOOK FOR TRANSPORTER
                    Dim conn1 As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
                    conn1.Open()
                    Dim w_qty, w_complite, w_unit_price, W_discount, mat_price As Decimal
                    Dim WO_NAME, WO_AMD, AMD_DATE As New String("")
                    Dim WO_AU As String = ""
                    Dim WO_SUPL_ID As String = ""
                    Dim MCqq As New SqlCommand
                    Dim des_date As Date = Today.Date
                    MCqq.CommandText = "select MAX(WO_AMD) AS WO_AMD ,MAX(AMD_DATE) AS AMD_DATE, sum(W_MATERIAL_COST) as W_MATERIAL_COST,MAX(SUPL_ID) as SUPL_ID, sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER WITH(NOLOCK) where PO_NO = '" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim & "' and w_slno=" & DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1).Trim & " and AMD_DATE < ='" & des_date.Year & "-" & des_date.Month & "-" & des_date.Day & "'"
                    MCqq.Connection = conn1
                    dr = MCqq.ExecuteReader
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
                    conn1.Close()

                    'TRANSPORTER AU WISE ENTRY
                    If (WO_AU = "Mt" Or WO_AU = "MT" Or WO_AU = "MTS") Then
                        'update despatch

                        QUARY1 = ""
                        QUARY1 = "update DESPATCH set INV_STATUS ='' where INV_NO  ='" & TextBox95.Text & "'"
                        Dim despatch As New SqlCommand(QUARY1, conn_trans, myTrans)
                        despatch.ExecuteReader()
                        despatch.Dispose()

                        ''insert inv_print

                        QUARY1 = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
                        Dim scmd As New SqlCommand(QUARY1, conn_trans, myTrans)
                        scmd.Parameters.AddWithValue("@INV_NO", inv_for & TextBox95.Text)
                        scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
                        scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
                        scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
                        scmd.Parameters.AddWithValue("@F_YEAR", STR1)
                        scmd.ExecuteReader()
                        scmd.Dispose()

                        ''service data search
                        Dim PROV_PRICE_FOR_TRANSPORTER As Decimal = 0.0
                        Dim RCVD_QTY_TRANSPORTER As Decimal = 0.0
                        i = 0
                        For i = 0 To GridView4.Rows.Count - 1
                            RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(GridView4.Rows(i).Cells(4).Text)
                        Next

                        ''calculate work amount
                        Dim base_value, discount_value, mat_rate As Decimal
                        base_value = 0
                        discount_value = 0
                        mat_rate = 0
                        base_value = w_unit_price * RCVD_QTY_TRANSPORTER
                        discount_value = (base_value * W_discount) / 100
                        PROV_PRICE_FOR_TRANSPORTER = FormatNumber(base_value - discount_value, 2)


                        'conn.Open()
                        Dim Query As String = "Insert Into mb_book(unit_price,Entry_Date,mb_no,mb_date,po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,mb_by,ra_no,prov_amt,pen_amt,sgst,cgst,igst,cess,sgst_liab,cgst_liab,igst_liab,cess_liab,it_amt,pay_ind,fiscal_year,mat_rate,mb_clear,amd_no,amd_date)VALUES(@unit_price,@Entry_Date,@mb_no,@mb_date,@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@mb_by,@ra_no,@prov_amt,@pen_amt,@sgst,@cgst,@igst,@cess,@sgst_liab,@cgst_liab,@igst_liab,@cess_liab,@it_amt,@pay_ind,@fiscal_year,@mat_rate,@mb_clear,@amd_no,@amd_date)"
                        Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@mb_no", inv_for & TextBox95.Text)
                        cmd.Parameters.AddWithValue("@mb_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@po_no", DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim)
                        cmd.Parameters.AddWithValue("@supl_id", WO_SUPL_ID)
                        cmd.Parameters.AddWithValue("@wo_slno", DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1).Trim)
                        cmd.Parameters.AddWithValue("@w_name", WO_NAME)
                        cmd.Parameters.AddWithValue("@w_au", WO_AU)
                        cmd.Parameters.AddWithValue("@from_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@to_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@work_qty", RCVD_QTY_TRANSPORTER)
                        cmd.Parameters.AddWithValue("@rqd_qty", RCVD_QTY_TRANSPORTER)
                        cmd.Parameters.AddWithValue("@bal_qty", w_complite)
                        cmd.Parameters.AddWithValue("@note", "")
                        cmd.Parameters.AddWithValue("@mb_by", Session("userName"))
                        cmd.Parameters.AddWithValue("@ra_no", "")
                        cmd.Parameters.AddWithValue("@prov_amt", Math.Round(PROV_PRICE_FOR_TRANSPORTER, 2))
                        cmd.Parameters.AddWithValue("@pen_amt", 0.0)
                        cmd.Parameters.AddWithValue("@sgst", 0)
                        cmd.Parameters.AddWithValue("@cgst", 0)
                        cmd.Parameters.AddWithValue("@igst", 0)
                        cmd.Parameters.AddWithValue("@cess", 0)
                        cmd.Parameters.AddWithValue("@sgst_liab", 0)
                        cmd.Parameters.AddWithValue("@cgst_liab", 0)
                        cmd.Parameters.AddWithValue("@igst_liab", 0)
                        cmd.Parameters.AddWithValue("@cess_liab", 0)
                        cmd.Parameters.AddWithValue("@it_amt", 0)
                        cmd.Parameters.AddWithValue("@pay_ind", "")
                        cmd.Parameters.AddWithValue("@fiscal_year", STR1)
                        cmd.Parameters.AddWithValue("@mat_rate", 0)
                        cmd.Parameters.AddWithValue("@mb_clear", "I.R. CLEAR")
                        cmd.Parameters.AddWithValue("@AMD_NO", WO_AMD)
                        cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(AMD_DATE), "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@unit_price", w_unit_price)
                        cmd.Parameters.AddWithValue("@Entry_Date", Now)
                        cmd.ExecuteReader()
                        cmd.Dispose()
                        'conn.Close()

                        ''SEARCH AC HEAD
                        conn.Open()
                        Dim TRANS_PROV, TRANS_PUR As String
                        TRANS_PROV = ""
                        TRANS_PUR = ""
                        mycommand.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim & "') and work_type=(select distinct wo_type from wo_order WITH(NOLOCK) where po_no='" & DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim & "' and w_slno='" & DropDownList28.Text.Substring(0, DropDownList28.Text.IndexOf(",") - 1).Trim & "')"
                        mycommand.Connection = conn
                        dr = mycommand.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            TRANS_PROV = dr.Item("PROV_HEAD")
                            TRANS_PUR = dr.Item("PUR_HEAD")
                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If
                        'INSERT TRANSPORTER LEDGER PROV AND PUR
                        save_ledger("", DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim, inv_for & TextBox95.Text, WO_SUPL_ID, TRANS_PUR, "Dr", PROV_PRICE_FOR_TRANSPORTER, "PUR")
                        save_ledger("", DropDownList9.Text.Substring(0, DropDownList9.Text.IndexOf(",") - 1).Trim, inv_for & TextBox95.Text, WO_SUPL_ID, TRANS_PROV, "Cr", PROV_PRICE_FOR_TRANSPORTER, "PROV")
                    End If
                End If

                ''clear data
                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(5) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("ASS_VALUE")})
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

                'myTrans.Commit()
                'Label406.Text = "All records are written to database."

                Dim tcsFlag As String
                If (CDec(TextBox119.Text) > 0) Then
                    tcsFlag = "Y"
                Else
                    tcsFlag = "N"
                End If

                ''===========================Generate E-Invoice Start=======================''

                'If gst_code = my_gst_code Then
                '    ''E-Invoice is not required
                'Else
                '    Dim logicClassObj = New EinvoiceLogicClass
                '    Dim AuthErrorData As List(Of AuthenticationErrorDetailsClass) = logicClassObj.EinvoiceAuthentication(TextBox177.Text + TextBox95.Text, TextBox96.Text)
                '    If (AuthErrorData.Item(0).status = "1") Then

                '        Dim EinvErrorData As List(Of EinvoiceErrorDetailsClass) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).client_id, AuthErrorData.Item(0).client_secret, AuthErrorData.Item(0).gst_no, AuthErrorData.Item(0).user_name, AuthErrorData.Item(0).AuthToken, AuthErrorData.Item(0).Sek, AuthErrorData.Item(0).appKey, AuthErrorData.Item(0).systemInvoiceNo, AuthErrorData.Item(0).buyerPartyCode)
                '        If (EinvErrorData.Item(0).status = "1") Then
                '            TextBox6.Text = EinvErrorData.Item(0).IRN

                '            Dim sqlQuery As String = ""
                '            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox95.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                '            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                '            despatch.ExecuteReader()
                '            despatch.Dispose()

                '            goAheadFlag = True
                '        ElseIf (EinvErrorData.Item(0).status = "2") Then
                '            Label31.Visible = True
                '            Label42.Visible = True
                '            txtEinvoiceErrorCode.Visible = True
                '            txtEinvoiceErrorMessage.Visible = True
                '            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorCode
                '            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                '            goAheadFlag = False
                '            Label388.Text = "There is some response error in E-Invoice generation."
                '        End If

                '    ElseIf (AuthErrorData.Item(0).status = "2") Then

                '        Label31.Visible = True
                '        Label42.Visible = True
                '        txtEinvoiceErrorCode.Visible = True
                '        txtEinvoiceErrorMessage.Visible = True
                '        txtEinvoiceErrorCode.Text = AuthErrorData.Item(0).errorCode
                '        txtEinvoiceErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                '        goAheadFlag = False
                '        Label388.Text = "There is some response error in E-invoice Authentication."
                '    Else
                '        goAheadFlag = False
                '        Label388.Text = "There is some response error in E-invoice Authentication."
                '    End If


                '    'Correct E-invoice authentication
                '    'EinvoiceAuthentication()

                'End If

                'If (goAheadFlag = True) Then
                '    myTrans.Commit()
                '    Label388.Text = "All records are written to database."
                '    Label31.Visible = False
                '    Label42.Visible = False
                '    txtEinvoiceErrorCode.Visible = False
                '    txtEinvoiceErrorMessage.Visible = False
                'Else
                '    myTrans.Rollback()
                '    conn.Close()
                '    conn_trans.Close()
                '    TextBox95.Text = ""
                '    TextBox177.Text = ""
                '    TextBox6.Text = ""

                'End If

                ''===========================Generate E-Invoice End=======================''

                ''===========================Generate E-Invoice Through EY Start=======================''

                If gst_code = my_gst_code Then
                    ''Only E-way bill is required
                    Dim logicClassObj = New EinvoiceLogicClassEY
                    Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox177.Text + TextBox95.Text, TextBox96.Text)
                    If (AuthErrorData.Item(0).status = "1") Then

                        'Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEwayBillOnly(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", tcsFlag)
                        Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEwayBillOnly(AuthErrorData.Item(0).Idtoken, Guid.NewGuid().ToString(), "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", tcsFlag)
                        If (EinvErrorData.Item(0).status = "1") Then

                            TextBox8.Text = EinvErrorData.Item(0).EwbNo
                            TextBox20.Text = EinvErrorData.Item(0).EwbValidTill

                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set EWB_NO ='" & EinvErrorData.Item(0).EwbNo & "',EWB_DATE ='" & EinvErrorData.Item(0).EwbDt & "',EWB_VALIDITY ='" & EinvErrorData.Item(0).EwbValidTill & "', EWB_STATUS ='ACTIVE' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox95.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            goAheadFlag = True

                        ElseIf (EinvErrorData.Item(0).status = "2") Then
                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorCode
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                            goAheadFlag = False
                            Label308.Text = "There is some response error in E-Invoice generation."

                        End If

                    ElseIf (AuthErrorData.Item(0).status = "2") Then

                        Label31.Visible = True
                        Label42.Visible = True
                        txtEinvoiceErrorCode.Visible = True
                        txtEinvoiceErrorMessage.Visible = True
                        txtEinvoiceErrorCode.Text = AuthErrorData.Item(0).errorCode
                        txtEinvoiceErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                        goAheadFlag = False
                        Label308.Text = "There is some response error in E-invoice Authentication."
                    Else
                        goAheadFlag = False
                        Label308.Text = "There is some response error in E-invoice Authentication."
                    End If
                Else
                    Dim logicClassObj = New EinvoiceLogicClassEY
                    Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox177.Text + TextBox95.Text, TextBox96.Text)
                    If (AuthErrorData.Item(0).status = "1") Then


                        Dim authIdToken As String = AuthErrorData.Item(0).Idtoken
                        'Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", tcsFlag, "INV")
                        Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, Guid.NewGuid().ToString(), "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", tcsFlag, "INV")

                        If (EinvErrorData.Item(0).status = "1") Then
                            TextBox6.Text = EinvErrorData.Item(0).IRN
                            TextBox8.Text = EinvErrorData.Item(0).EwbNo
                            TextBox20.Text = EinvErrorData.Item(0).EwbValidTill

                            '================SENDING DATA TO EY PORTAL START==================='

                            Dim result
                            If (BILL_PARTY_GST = "") Then
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            Else
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            End If

                            '================SENDING DATA TO EY PORTAL END==================='
                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "',EWB_NO ='" & EinvErrorData.Item(0).EwbNo & "',EWB_DATE ='" & EinvErrorData.Item(0).EwbDt & "',EWB_VALIDITY ='" & EinvErrorData.Item(0).EwbValidTill & "', EWB_STATUS ='ACTIVE', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox95.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            goAheadFlag = True

                        ElseIf (EinvErrorData.Item(0).status = "2") Then
                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorCode
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                            goAheadFlag = False
                            Label308.Text = "There is some response error in E-Invoice generation."

                        ElseIf (EinvErrorData.Item(0).status = "3") Then
                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorfield
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errordesc
                            goAheadFlag = False
                            Label308.Text = "There is some response error in E-Invoice generation."
                        ElseIf (EinvErrorData.Item(0).status = "4") Then
                            TextBox6.Text = EinvErrorData.Item(0).IRN
                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage

                            '================SENDING DATA TO EY PORTAL START==================='

                            Dim result
                            If (BILL_PARTY_GST = "") Then
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            Else
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox95.Text, TextBox96.Text, TextBox1.Text, "NO", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            End If

                            '================SENDING DATA TO EY PORTAL END==================='

                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox95.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            goAheadFlag = True
                            partialSuccess = True
                            Label308.Text = "There is error in E-way bill generation, please generate E-way bill alone with above IRN."
                        End If

                    ElseIf (AuthErrorData.Item(0).status = "2") Then

                        Label31.Visible = True
                        Label42.Visible = True
                        txtEinvoiceErrorCode.Visible = True
                        txtEinvoiceErrorMessage.Visible = True
                        txtEinvoiceErrorCode.Text = AuthErrorData.Item(0).errorCode
                        txtEinvoiceErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                        goAheadFlag = False
                        Label308.Text = "There is some response error in E-invoice Authentication."
                    Else
                        goAheadFlag = False
                        Label308.Text = "There is some response error in E-invoice Authentication."
                    End If

                End If

                If (goAheadFlag = True) Then

                    If (partialSuccess = True) Then
                        myTrans.Commit()

                    Else
                        myTrans.Commit()
                        Label31.Visible = False
                        Label42.Visible = False
                        txtEinvoiceErrorCode.Visible = False
                        txtEinvoiceErrorMessage.Visible = False
                    End If
                Else
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    TextBox95.Text = ""
                    TextBox177.Text = ""
                    TextBox6.Text = ""

                End If

                ''===========================Generate E-Invoice Through EY End=======================''
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                TextBox95.Text = ""
                TextBox177.Text = ""
                Label308.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub
    Protected Sub save_ledger(RCD_VOUCHER_NO As String, so_no As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
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
            'conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(AGING_FLAG,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@AGING_FLAG,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn_trans, myTrans)
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
            cmd.Parameters.AddWithValue("@AGING_FLAG", RCD_VOUCHER_NO)
            cmd.ExecuteReader()
            cmd.Dispose()
            'conn.Close()
        End If
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

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Or DropDownList1.SelectedValue = "N/A" Then
            DropDownList1.Focus()
            TextBox9.Text = 0.0
            TextBox176.Text = ""
            Return
        End If
        conn.Open()
        mycommand.CommandText = "SELECT * FROM SALE_RCD_VOUCHAR WHERE VOUCHER_TYPE + VOUCHER_NO ='" & DropDownList1.SelectedValue & "' and SO_NO='" & TextBox123.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox9.Text = dr.Item("BAL_AMT")
            TextBox176.Text = dr.Item("VOUCHER_DATE")
            dr.Close()
        End If
        conn.Close()
    End Sub


End Class