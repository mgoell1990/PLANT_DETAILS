Imports System.Globalization
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net

Imports System.Security.Cryptography
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Security
Imports Org.BouncyCastle.Crypto.Parameters

Imports Newtonsoft
Imports Newtonsoft.Json.Linq
Imports System.IdentityModel.Tokens.Jwt


Public Class OutsourceMatDespatch
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

    Protected Sub BINDGRID()
        GridView2.DataSource = DirectCast(ViewState("despatch"), DataTable)
        GridView2.DataBind()
    End Sub

    Protected Sub BINDGRID2()
        FormView1.DataSource = DirectCast(ViewState("form_view"), DataTable)
        FormView1.DataBind()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            MultiView1.ActiveViewIndex = 0
        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

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


        TextBox125.Text = ""
        TextBox126.Text = ""
        TextBox75.Text = ""
        TextBox62.Text = ""
        TextBox174.Text = ""
        If po_type = "OUTSOURCED ITEMS" Then
            Dim dt7 As New DataTable()
            dt7.Columns.AddRange(New DataColumn(8) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("UNIT_WEIGHT"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
            ViewState("despatch") = dt7
            BINDGRID()
            ''SEARCH SALE ORDER VENDER DETAILS
            conn.Open()
            mc1.CommandText = "select dater.d_code,dater.d_name,dater.supl_loc from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox125.Text = dr.Item("d_code")
                TextBox126.Text = dr.Item("d_name")
                'dater_type = dr.Item("d_name")
                dr.Close()
            End If
            conn.Close()

            conn.Open()
            mc1.CommandText = "select dater.d_code as consinee_code,dater.d_name as consinee_name from dater join order_details on order_details.CONSIGN_CODE=dater.d_code where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox18.Text = dr.Item("consinee_code")
                TextBox19.Text = dr.Item("consinee_name")
                dr.Close()
            End If
            conn.Close()

            TextBox124.Text = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim
            '' SERACH TRANSPORTATION DETAILS
            If DELIVERY_TERM = "By SRU" Then
                DropDownList6.Enabled = True
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("SELECT DISTINCT (WO_ORDER .PO_NO + ' , ' + SUPL.SUPL_NAME) AS PO_NO FROM ORDER_DETAILS JOIN WO_ORDER ON ORDER_DETAILS .SO_NO =WO_ORDER .PO_NO JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID WHERE ORDER_DETAILS .PO_TYPE ='FREIGHT OUTWARD' AND WO_ORDER.W_STATUS='PENDING' and WO_ORDER.W_END_DATE >'" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "' AND WO_ORDER.WO_TYPE='FREIGHT OUTWARD' ORDER BY PO_NO", conn)
                'da = New SqlDataAdapter("SELECT DISTINCT (WO_ORDER .PO_NO + ' , ' + SUPL.SUPL_NAME) AS PO_NO FROM ORDER_DETAILS JOIN WO_ORDER ON ORDER_DETAILS .SO_NO =WO_ORDER .PO_NO JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID WHERE ORDER_DETAILS .PO_TYPE ='FREIGHT OUTWARD' and WO_ORDER.W_END_DATE >'" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "' AND WO_ORDER.WO_TYPE='FREIGHT OUTWARD' ORDER BY PO_NO", conn)
                da.Fill(dt)
                conn.Close()
                DropDownList6.Items.Clear()
                DropDownList6.DataSource = dt
                DropDownList6.DataValueField = "PO_NO"
                DropDownList6.DataBind()
                DropDownList6.Items.Insert(0, "Select")
                DropDownList6.Items.Insert(1, "PARTY")
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
            Dim FINANCE_ARRANGE As String = ""
            conn.Open()
            mc1.CommandText = "select  ORDER_DETAILS . PAYMENT_MODE  FROM  ORDER_DETAILS   WHERE SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
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
                da = New SqlDataAdapter("SELECT VOUCHER_NO FROM SALE_RCD_VOUCHAR WHERE SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "' AND VOUCHER_STATUS='PENDING'", conn)
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

            ''SEARCH LINE NO DETAILS
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct convert(varchar(15),ITEM_SLNO) + ' , ' + ITEM_VOCAB AS ITEM_SLNO from SO_MAT_ORDER where item_status='PENDING' and ORD_AU <>'Activity' and ORD_AU <>'Service/Mt' and SO_NO='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList4.Items.Clear()
            DropDownList4.DataSource = dt
            DropDownList4.DataValueField = "ITEM_SLNO"
            DropDownList4.DataBind()
            DropDownList4.Items.Insert(0, "Select")
            DropDownList4.SelectedValue = "Select"

            MultiView1.ActiveViewIndex = 1
        End If
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
            DropDownList27.Items.Insert(0, "Select")
            DropDownList27.SelectedValue = "Select"
            Return
        End If
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
        If MAT_TYPE = "OUTSOURCED ITEMS" Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT ITEM_CODE AS ITEM_CODE from SO_MAT_ORDER where item_status='PENDING' and SO_MAT_ORDER.SO_NO='" & TextBox124.Text & "' and SO_MAT_ORDER.ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList5.Items.Clear()
            DropDownList5.DataSource = dt
            DropDownList5.DataValueField = "ITEM_CODE"
            DropDownList5.DataBind()
            DropDownList5.Items.Insert(0, "Select")
            DropDownList5.SelectedValue = "Select"
        End If

        Dim FINANCE_ARRANGE As String = ""
        conn.Open()
        mc1.CommandText = "select  ORDER_DETAILS . PAYMENT_MODE  FROM  ORDER_DETAILS   WHERE SO_NO ='" & TextBox124.Text & "'"
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
            da = New SqlDataAdapter("SELECT VOUCHER_TYPE + VOUCHER_NO AS VOUCHER_NO FROM SALE_RCD_VOUCHAR WHERE SO_NO ='" & TextBox124.Text & "' AND VOUCHER_STATUS='PENDING' AND ITEM_SLNO ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' ", conn)
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
        If MAT_TYPE = "OUTSOURCED ITEMS" Then
            conn.Open()
            dt.Clear()
            'da = New SqlDataAdapter("select MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE ,MAX(outsource_F_ITEM .ITEM_NAME) AS ITEM_NAME,MAX(outsource_F_ITEM .ITEM_AU) AS ITEM_AU,MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT,SUM(SO_MAT_ORDER .ITEM_QTY) AS ITEM_QTY,(SUM(SO_MAT_ORDER.ITEM_QTY)-SUM(SO_MAT_ORDER.ITEM_QTY_SEND)) AS ITEM_BAL_QTY,MAX(outsource_F_ITEM .ITEM_B_STOCK) AS ITEM_B_STOCK,convert(decimal(10,3) ,((MAX(outsource_F_ITEM .ITEM_B_STOCK)*MAX(SO_MAT_ORDER.ITEM_WEIGHT)) / 1000)) AS ITEM_B_STOCK_MT FROM SO_MAT_ORDER JOIN outsource_F_ITEM ON SO_MAT_ORDER .ITEM_CODE = outsource_F_ITEM .ITEM_CODE where SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND SO_MAT_ORDER.SO_NO='" & TextBox124.Text & "' AND SO_MAT_ORDER.ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "'", conn)
            da = New SqlDataAdapter("select MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE ,MAX(outsource_F_ITEM .ITEM_NAME) AS ITEM_NAME,MAX(outsource_F_ITEM .ITEM_AU) AS ITEM_AU,MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT,SUM(SO_MAT_ORDER .ITEM_QTY) AS ITEM_QTY,(SUM(SO_MAT_ORDER.ITEM_QTY)-SUM(SO_MAT_ORDER.ITEM_QTY_SEND)) AS ITEM_BAL_QTY,MAX(outsource_F_ITEM .ITEM_F_STOCK) AS ITEM_F_STOCK,convert(decimal(10,3) ,((MAX(outsource_F_ITEM .ITEM_F_STOCK) * MAX(SO_MAT_ORDER.ITEM_WEIGHT)) / 1000)) AS ITEM_F_STOCK_MT FROM SO_MAT_ORDER JOIN outsource_F_ITEM ON SO_MAT_ORDER .ITEM_CODE = outsource_F_ITEM .ITEM_CODE where SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND SO_MAT_ORDER.SO_NO='" & TextBox124.Text & "' AND SO_MAT_ORDER.ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "'", conn)
            da.Fill(dt)
            conn.Close()
            ViewState("form_view") = dt
            BINDGRID2()
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select ITEM_AU,ITEM_NAME from outsource_F_ITEM where ITEM_CODE='" & DropDownList5.Text & "' "
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
        ElseIf TextBox54.Text = "" Then
            TextBox54.Focus()
            Return
        ElseIf TextBox54.Text = 0 Then
            TextBox54.Text = ""
            TextBox54.Focus()
            Return
        ElseIf IsNumeric(TextBox54.Text) = False Then
            TextBox54.Focus()
            Return
        ElseIf TextBox56.Text = "" Then
            TextBox56.Focus()
            Return
        ElseIf IsNumeric(TextBox56.Text) = False Then
            TextBox56.Focus()
            TextBox56.Text = ""
            Return
        ElseIf IsNumeric(TextBox7.Text) = False Then
            TextBox7.Focus()
            TextBox7.Text = ""
            Label308.Text = "Please enter correct transporter weight in MTS."
            Return
        ElseIf TextBox7.Text = "" Then
            TextBox7.Focus()
            Label308.Text = "Please enter transporter weight in MTS."
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
        Dim FINANCE_ARRANGE As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select  ORDER_DETAILS.PAYMENT_MODE FROM ORDER_DETAILS WHERE SO_NO ='" & TextBox124.Text.Trim & "'"
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
        ''details search
        'Dim pcs_qty, total_weight, ass_price, cas4_ass_price As Decimal
        Dim pcs_qty, total_weight, ass_price As Decimal
        pcs_qty = 0
        total_weight = 0
        ass_price = 0.0
        'cas4_ass_price = 0.0
        Dim stock_qty As Decimal = 0.0
        Dim item_qty, unit_rate, discount, pack_forwd, item_cess, item_tcs, terminal_tax, cgst, sgst, igst, qty_send, freight_rate, UNIT_WEIGHT As Decimal
        Dim freight_type As String = ""
        Dim ord_au As String = ""
        Dim DISC_TYPE As String = ""
        Dim PACK_TYPE, ITEM_TYPE As New String("")
        Dim ED_TYPE As String = ""

        unit_rate = 0
        discount = 0
        pack_forwd = 0
        item_cess = 0
        item_tcs = 0
        terminal_tax = 0
        cgst = 0
        sgst = 0
        igst = 0
        qty_send = 0
        item_qty = 0
        UNIT_WEIGHT = 0.0
        freight_rate = 0
        conn.Open()
        mc1.CommandText = "select ITEM_WEIGHT,ITEM_F_STOCK from outsource_F_ITEM where ITEM_CODE='" & DropDownList5.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            stock_qty = dr.Item("ITEM_F_STOCK")
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
        mc.CommandText = "select MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT,SUM(ITEM_CGST) AS ITEM_CGST,SUM(ITEM_SGST) AS ITEM_SGST,SUM(ITEM_IGST) AS ITEM_IGST, SUM(ITEM_CESS) AS ITEM_CESS,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU , " &
                             " MAX(CHPTR_HEADING.TAX_VALUE) AS TAX_VALUE ,MAX(CHPTR_HEADING.ED_SESS) AS ED_SESS ,MAX(CHPTR_HEADING.SHED_CESS) AS SHED_CESS, Max(outsource_F_ITEM.ITEM_TYPE) As ITEM_TYPE FROM SO_MAT_ORDER JOIN outsource_F_ITEM ON SO_MAT_ORDER .ITEM_CODE =outsource_F_ITEM .ITEM_CODE JOIN CHPTR_HEADING on outsource_F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and SO_MAT_ORDER .SO_NO='" & TextBox124.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            ord_au = dr.Item("ORD_AU")
            UNIT_WEIGHT = dr.Item("ITEM_WEIGHT")
            unit_rate = dr.Item("ITEM_UNIT_RATE")
            discount = dr.Item("ITEM_DISCOUNT")
            pack_forwd = dr.Item("ITEM_PACK")
            cgst = dr.Item("ITEM_CGST")
            sgst = dr.Item("ITEM_SGST")
            igst = dr.Item("ITEM_IGST")
            item_cess = dr.Item("ITEM_CESS")
            terminal_tax = dr.Item("ITEM_TERMINAL_TAX")
            item_tcs = dr.Item("ITEM_TCS")
            qty_send = dr.Item("ITEM_QTY_SEND")
            item_qty = dr.Item("ITEM_QTY")
            freight_type = dr.Item("ITEM_FREIGHT_TYPE")
            freight_rate = dr.Item("ITEM_FREIGHT_PU")
            'If (IsDBNull(dr.Item("COST_VALUE"))) Then
            '    cas4_price = 0.00
            'Else
            '    cas4_price = dr.Item("COST_VALUE")
            'End If

            DISC_TYPE = dr.Item("DISC_TYPE")
            PACK_TYPE = dr.Item("PACK_TYPE")
            ITEM_TYPE = dr.Item("ITEM_TYPE")
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
            'cas4_ass_price = total_weight * cas4_price
        ElseIf (ord_au = "Set" Or ord_au = "SET") Then
            pcs_qty = CInt(TextBox54.Text)
            total_weight = FormatNumber((CDec(TextBox54.Text) * UNIT_WEIGHT) / 1000, 3)
            ass_price = pcs_qty * unit_rate
            'cas4_ass_price = total_weight * cas4_price
        ElseIf (ord_au = "Mt" Or ord_au = "MT" Or ord_au = "MTS") Then
            pcs_qty = 0
            total_weight = FormatNumber((CDec(TextBox54.Text) * UNIT_WEIGHT) / 1000, 3)
            ass_price = total_weight * unit_rate
            'cas4_ass_price = total_weight * cas4_price
        End If

        If (DropDownList6.SelectedItem.Text <> "PARTY" And DropDownList6.SelectedItem.Text <> "N/A") Then
            ''Checking the transporter balance QTY
            Dim WO_Qty, Work_completed As Decimal
            Dim W_AU As New String("")
            WO_Qty = 0
            Work_completed = 0
            conn.Open()
            mc1.CommandText = "select MAX(W_AU) As W_AU, sum(W_QTY) As WO_Qty, sum(W_COMPLITED) As Work_completed from WO_ORDER where PO_NO='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' and W_SLNO='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim & "' group by PO_NO,W_SLNO"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                WO_Qty = dr.Item("WO_Qty")
                Work_completed = dr.Item("Work_completed")
                W_AU = dr.Item("W_AU")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            If (W_AU = "Vehicle") Then
                If WO_Qty = Work_completed Then
                    TextBox54.Focus()
                    Label308.Text = "There is No trailor balance left in trandportation contract. Order balance is " & WO_Qty - Work_completed
                    Return
                Else
                    Label308.Text = ""
                End If
            Else
                If WO_Qty < Work_completed + FormatNumber(CInt(TextBox7.Text), 3) Then
                    TextBox54.Focus()
                    Label308.Text = "Despatch Qty is Higher Than Transporter Balance Qty which is. " & WO_Qty - Work_completed
                    Return
                Else
                    Label308.Text = ""
                End If
            End If

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

            If ITEM_TYPE = "Other" Then

                TextBox38.Text = FormatNumber((CDec(TextBox38.Text) + ass_price), 2)
            Else
                'TextBox38.Text = FormatNumber((CDec(TextBox38.Text) + cas4_ass_price), 2)
                TextBox38.Text = FormatNumber((CDec(TextBox38.Text) + ass_price), 2)
            End If


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

        If PARTY_TYPE = "Other" Then
            ''Taxable Value
            TextBox38.Text = CDec(ASS_V) + CDec(TextBox39.Text) + CDec(TextBox41.Text) - CDec(TextBox37.Text)
            ''T TAX VALUE
            TextBox43.Text = FormatNumber(CInt((CDec(TextBox38.Text) * terminal_tax) / 100), 2)

            'cgst value
            TextBox40.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * cgst) / 100), 2)
            'sgst value
            TextBox42.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * sgst) / 100), 2)
            'igst value
            TextBox44.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * igst) / 100), 2)
            'cess value
            TextBox21.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * item_cess) / 100), 2)

            ''TCS VALUE
            'TextBox66.Text = FormatNumber((((CDec(ASS_V) - CDec(TextBox37.Text)) + CDec(TextBox40.Text) + CDec(TextBox21.Text) + CDec(TextBox41.Text) + CDec(TextBox44.Text)) * item_tcs) / 100, 2)
            TextBox66.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox21.Text)) * item_tcs) / 100), 2)



        ElseIf PARTY_TYPE = "I.P.T." Then

            If ITEM_TYPE = "Other" Then
                ''TAXABLE VALUE
                TextBox38.Text = ass_price + CDec(TextBox39.Text) + CDec(TextBox41.Text) - CDec(TextBox37.Text)
                ''T TAX VALUE
                TextBox43.Text = FormatNumber(CInt((CDec(TextBox38.Text) * terminal_tax) / 100), 2)

                'cgst value
                TextBox40.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * cgst) / 100), 2)
                'sgst value
                TextBox42.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * sgst) / 100), 2)
                'igst value
                TextBox44.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * igst) / 100), 2)
                'cess value
                TextBox21.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * item_cess) / 100), 2)

                ''TCS VALUE
                TextBox66.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox21.Text)) * item_tcs) / 100), 2)
            Else
                ''TAXABLE VALUE
                'TextBox38.Text = cas4_ass_price + CDec(TextBox39.Text) + CDec(TextBox41.Text) - CDec(TextBox37.Text)
                TextBox38.Text = ass_price + CDec(TextBox39.Text) + CDec(TextBox41.Text) - CDec(TextBox37.Text)
                ''T TAX VALUE
                TextBox43.Text = FormatNumber(CInt((CDec(TextBox38.Text) * terminal_tax) / 100), 2)

                'cgst value
                TextBox40.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * cgst) / 100), 2)
                'sgst value
                TextBox42.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * sgst) / 100), 2)
                'igst value
                TextBox44.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * igst) / 100), 2)
                'cess value
                TextBox21.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text)) * item_cess) / 100), 2)

                ''TCS VALUE
                TextBox66.Text = FormatNumber(CInt(((CDec(TextBox38.Text) + CDec(TextBox43.Text) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox21.Text)) * item_tcs) / 100), 2)
            End If



        End If


        ''TOTAL VALUE
        TextBox45.Text = CDec(ASS_V) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox21.Text) - CDec(TextBox37.Text) + CDec(TextBox39.Text) + CDec(TextBox41.Text) + CDec(TextBox43.Text) + CDec(TextBox66.Text)


        If FINANCE_ARRANGE = "ADVANCE" Then
            If DropDownList1.SelectedValue = "Select" Then
                DropDownList1.Focus()
                Return
            End If
            If CDec(TextBox9.Text) < CDec(TextBox45.Text) Then

                DropDownList1.Focus()
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
                TextBox45.Text = "0.00"
                TextBox21.Text = "0.00"
                Label308.Text = "Despatch value cannot be more than balance amt."
                Return
            End If

        ElseIf FINANCE_ARRANGE = "CREDIT" Then

        ElseIf FINANCE_ARRANGE = "BG" Then

        End If


        Button36.Enabled = True
    End Sub

    Protected Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim dt7 As New DataTable()
        dt7.Columns.AddRange(New DataColumn(8) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("UNIT_WEIGHT"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
        ViewState("despatch") = dt7
        BINDGRID()
        TextBox55.Text = ""
        TextBox69.Text = ""
        DropDownList7.SelectedValue = "N/A"
        TextBox37.Text = "0.00"
        TextBox38.Text = "0.00"
        TextBox39.Text = "0.00"
        TextBox40.Text = "0.00"
        TextBox41.Text = "0.00"
        TextBox42.Text = "0.00"
        TextBox43.Text = "0.00"
        TextBox44.Text = "0.00"
        TextBox66.Text = "0.00"
        TextBox45.Text = "0.00"
        TextBox21.Text = "0.00"
        TextBox54.Text = ""
        TextBox56.Text = ""
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

    Protected Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date
                working_date = Today.Date
                ''working_date = CDate(TextBox178.Text)
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


                Dim gst_code, my_gst_code, COMM, DIVISION As New String("")
                conn.Open()
                mycommand.CommandText = "select dater.d_code,dater.d_name,dater.gst_code from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & TextBox124.Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    gst_code = dr.Item("gst_code")
                    dr.Close()
                End If
                conn.Close()

                ''SAVE DESPATCH
                ''sale order date excise comodity
                Dim so_date As Date
                Dim SO_ACTUAL_DATE As Date
                Dim PARTY_CODE As String = ""
                Dim CONSIGN_CODE As String = ""
                Dim excise_comodity As String = ""
                Dim chptr_heading As String = ""
                Dim mode_of_despatch As String = ""
                Dim mode_of_payment As String = ""
                Dim qual_name As String = ""
                Dim qual_desc As String = ""
                Dim FINANCE_ARRANGE As String = ""
                Dim PLACE_OF_SUPPLY As String = ""
                Dim ORDER_TO As String = ""

                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.DESTINATION,ORDER_DETAILS.CONSIGN_CODE,ORDER_DETAILS.SO_ACTUAL_DATE, ORDER_DETAILS . PAYMENT_MODE , ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PARTY_CODE,qual_group.qual_name,qual_group.qual_desc , ORDER_DETAILS.PAYMENT_TERM,ORDER_DETAILS.MODE_OF_DESPATCH,outsource_F_ITEM .ITEM_CHPTR,CHPTR_HEADING.CHPT_NAME FROM SO_MAT_ORDER JOIN ORDER_DETAILS ON SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO JOIN outsource_F_ITEM ON SO_MAT_ORDER .ITEM_CODE =outsource_F_ITEM .ITEM_CODE JOIN CHPTR_HEADING ON outsource_F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE join qual_group on outsource_F_ITEM .ITEM_TYPE =qual_group.qual_code  WHERE SO_MAT_ORDER.ITEM_SLNO  ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' AND SO_MAT_ORDER .SO_NO ='" & TextBox124.Text.Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORDER_TO = dr.Item("ORDER_TO")
                    excise_comodity = dr.Item("CHPT_NAME")
                    chptr_heading = dr.Item("ITEM_CHPTR")
                    so_date = dr.Item("SO_DATE")
                    FINANCE_ARRANGE = dr.Item("PAYMENT_MODE")
                    PARTY_CODE = dr.Item("PARTY_CODE")
                    CONSIGN_CODE = dr.Item("CONSIGN_CODE")
                    mode_of_despatch = dr.Item("MODE_OF_DESPATCH")
                    mode_of_payment = dr.Item("PAYMENT_TERM")
                    qual_name = dr.Item("qual_name")
                    qual_desc = dr.Item("qual_desc")
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
                    inv_rule = "(Under Rule 1 of Tax Invoice Credit and Debit Note Rules)"
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
                        TextBox65.Text = prefixFY & "000001"
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox65.ReadOnly = True
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
                        TextBox65.Text = str
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox65.ReadOnly = True
                    End If
                Else
                    If CInt(inv_no) = 0 Then
                        TextBox65.Text = "0000001"
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox65.ReadOnly = True
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
                        TextBox65.Text = str
                        TextBox177.Text = inv_for
                        TextBox177.ReadOnly = True
                        TextBox65.ReadOnly = True
                    End If
                End If


                ''DESPATCH NO GENERATE
                Dim transporter, trans_sl_no As New String("")
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

                Dim weight As Decimal = 0.0
                Dim i As Integer = 0
                Dim ass_price As Decimal = 0.0
                For i = 0 To GridView2.Rows.Count - 1
                    weight = CDec(GridView2.Rows(i).Cells(6).Text) + weight
                    ass_price = ass_price + CDec(GridView2.Rows(i).Cells(8).Text)
                Next

                ''item assessable value calculatation
                Dim cas4_price, item_qty, unit_rate, discount, IGST, CGST, SGST, item_cess, pack_forwd, item_tcs, terminal_tax, qty_send, freight_rate As Decimal
                Dim freight_type, pack_type, actual_so, DISC_TYPE As New String("")
                conn.Open()
                Dim mc As New SqlCommand
                mc.CommandText = "select MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT,SUM(ITEM_CGST) AS ITEM_CGST,SUM(ITEM_SGST) AS ITEM_SGST,SUM(ITEM_IGST) AS ITEM_IGST, SUM(ITEM_CESS) AS ITEM_CESS,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU , " &
                            " (select CAS_4 .COST_VALUE  from outsource_F_ITEM join CAS_4 on outsource_F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where ITEM_CODE ='" & DropDownList5.Text & "' and CAS_4 .EFECTIVE_DATE =(select max(CAS_4 .EFECTIVE_DATE)  from CAS_4 join outsource_F_ITEM on outsource_F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where outsource_F_ITEM .ITEM_CODE ='" & DropDownList5.Text & "' and CAS_4 .EFECTIVE_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "')) as COST_VALUE,  " &
                            " (select SO_ACTUAL  from ORDER_DETAILS where SO_NO ='" & TextBox124.Text & "') as actual_so ,   " &
                            "  MAX(CHPTR_HEADING.TAX_VALUE) AS TAX_VALUE ,MAX(CHPTR_HEADING.ED_SESS) AS ED_SESS ,MAX(CHPTR_HEADING.SHED_CESS) AS SHED_CESS FROM SO_MAT_ORDER JOIN outsource_F_ITEM ON SO_MAT_ORDER .ITEM_CODE =outsource_F_ITEM .ITEM_CODE JOIN CHPTR_HEADING on outsource_F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and SO_MAT_ORDER .SO_NO='" & TextBox124.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList5.Text & "' AND AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "'"
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
                    If (IsDBNull(dr.Item("COST_VALUE"))) Then
                        cas4_price = 0.00
                    Else
                        cas4_price = dr.Item("COST_VALUE")
                    End If

                    pack_type = dr.Item("PACK_TYPE")
                    DISC_TYPE = dr.Item("DISC_TYPE")
                    actual_so = dr.Item("actual_so")
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
                mc1.CommandText = "select d_name + ' , ' + add_1 + ' , ' + add_2 + ' , ' + ecc_no + ' , ' + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select PARTY_CODE  from ORDER_DETAILS where SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "')"
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
                mc1.CommandText = "select d_name + ' , ' + add_1 + ' , ' + add_2 + ' , ' + ecc_no + ' , ' + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select CONSIGN_CODE  from ORDER_DETAILS where SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "')"
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
                If (Label289.Text = "Pcs" Or Label289.Text = "PCS") Then
                    total_pcs = CInt(TextBox54.Text)
                Else
                    total_pcs = 0
                End If
                '' calculation

                Dim price_unit, total_rate_unit, taxable_rate_unit, total_amt As New Decimal(0.0)
                price_unit = FormatNumber((ass_price + CDec(TextBox41.Text)) / CDec(TextBox54.Text), 4)
                total_rate_unit = FormatNumber((ass_price + CDec(TextBox41.Text) + CDec(TextBox39.Text)) / CDec(TextBox54.Text), 4)
                taxable_rate_unit = FormatNumber((ass_price + CDec(TextBox41.Text) + CDec(TextBox39.Text) - CDec(TextBox37.Text)) / CDec(TextBox54.Text), 4)
                total_amt = CDec(TextBox45.Text)

                Dim TAXABLE_VALUE As Decimal = 0
                TAXABLE_VALUE = CDec(TextBox38.Text)
                Dim ADV_AMT, ADV_GST, NET_PAY As New Decimal(0.0)
                If FINANCE_ARRANGE = "ADVANCE" Then
                    ADV_AMT = ass_price - CDec(TextBox37.Text) + CDec(TextBox66.Text) + CDec(TextBox43.Text)
                    ADV_GST = CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox21.Text)
                    NET_PAY = 0.0
                Else
                    ADV_AMT = 0.0
                    ADV_GST = 0.0
                    NET_PAY = CDec(TextBox45.Text)
                End If

                'conn.Open()
                Dim QUARY1 As String = ""
                QUARY1 = "Insert Into DESPATCH(MATERIAL_TYPE,TRANSPORTER_WEIGHT,QUALITY,INV_STATUS,SO_NO ,SO_DATE ,PO_NO ,PO_DATE ,AMD_NO ,AMD_DATE ,TRANS_WO ,TRANS_SLNO ,TRANS_NAME ,TRUCK_NO ,PARTY_CODE ,CONSIGN_CODE ,MAT_VOCAB ,MAT_SLNO ,INV_NO,INV_DATE,D_TYPE,CHPTR_HEAD ,FISCAL_YEAR,INV_ISSUE ,PLACE_OF_SUPPLY ,BILL_PARTY_ADD ,CON_PARTY_ADD ,B_STATE ,B_STATE_CODE ,C_STATE ,C_STATE_CODE ,BILL_PARTY_GST_N ,CON_PARTY_GST_N ,NEGOTIATING_BRANCH ,PAYING_AUTH ,RR_NO ,RR_DATE ,TOTAL_WEIGHT ,ACC_UNIT ,PURITY ,TC_NO ,FINANCE_ARRENGE ,MILL_CODE ,DA_NO ,CONTRACT_NO ,RCD_VOUCHER_NO ,RCD_VOUCHER_DATE ,ROUT_CARD_NO ,DESPATCH_TYPE ,TAX_REVERS_CHARGE ,RLY_INV_NO ,RLY_INV_DATE ,FRT_WT_AMT ,TOTAL_PCS,P_CODE ,P_DESC ,D1 ,D2 ,D3 ,D4 ,BASE_PRICE ,PACK_PRICE ,PACK_TYPE ,QLTY_PRICE ,SEC_PRICE ,TOTAL_TDC ,UNIT_PRICE ,SY_MARGIN ,PPM_FRT ,FRT_TYPE ,RLY_ROAD_FRT ,TOTAL_RATE_UNIT ,REBATE_UNIT ,REBATE_TYPE ,TAXABLE_RATE_UNIT ,TOTAL_QTY ,TAXABLE_VALUE ,CGST_RATE ,CGST_AMT ,SGST_RATE ,SGST_AMT ,IGST_RATE ,IGST_AMT ,CESS_RATE ,CESS_AMT ,TERM_RATE ,TERM_AMT ,TOTAL_AMT ,LESS_LOAD_AMT ,TOTAL_BAG ,ADVANCE_PAID ,GST_PAID_ADV ,NET_PAY ,NOTIFICATION_TEXT ,COMM ,DIV_ADD ,INV_TYPE ,INV_RULE ,FORM_NAME,EMP_ID, TCS_TAX, TCS_AMT)values(@MATERIAL_TYPE,@TRANSPORTER_WEIGHT,@QUALITY,@INV_STATUS,@SO_NO ,@SO_DATE ,@PO_NO ,@PO_DATE ,@AMD_NO ,@AMD_DATE ,@TRANS_WO ,@TRANS_SLNO ,@TRANS_NAME ,@TRUCK_NO ,@PARTY_CODE ,@CONSIGN_CODE ,@MAT_VOCAB ,@MAT_SLNO ,@INV_NO,@INV_DATE,@D_TYPE,@CHPTR_HEAD ,@FISCAL_YEAR,@INV_ISSUE ,@PLACE_OF_SUPPLY ,@BILL_PARTY_ADD ,@CON_PARTY_ADD ,@B_STATE ,@B_STATE_CODE ,@C_STATE ,@C_STATE_CODE ,@BILL_PARTY_GST_N ,@CON_PARTY_GST_N ,@NEGOTIATING_BRANCH ,@PAYING_AUTH ,@RR_NO ,@RR_DATE ,@TOTAL_WEIGHT ,@ACC_UNIT ,@PURITY ,@TC_NO ,@FINANCE_ARRENGE ,@MILL_CODE ,@DA_NO ,@CONTRACT_NO ,@RCD_VOUCHER_NO ,@RCD_VOUCHER_DATE ,@ROUT_CARD_NO ,@DESPATCH_TYPE ,@TAX_REVERS_CHARGE ,@RLY_INV_NO ,@RLY_INV_DATE ,@FRT_WT_AMT ,@TOTAL_PCS,@P_CODE ,@P_DESC ,@D1 ,@D2 ,@D3 ,@D4 ,@BASE_PRICE ,@PACK_PRICE ,@PACK_TYPE ,@QLTY_PRICE ,@SEC_PRICE ,@TOTAL_TDC ,@UNIT_PRICE ,@SY_MARGIN ,@PPM_FRT ,@FRT_TYPE ,@RLY_ROAD_FRT ,@TOTAL_RATE_UNIT ,@REBATE_UNIT ,@REBATE_TYPE ,@TAXABLE_RATE_UNIT ,@TOTAL_QTY ,@TAXABLE_VALUE ,@CGST_RATE ,@CGST_AMT ,@SGST_RATE ,@SGST_AMT ,@IGST_RATE ,@IGST_AMT ,@CESS_RATE ,@CESS_AMT ,@TERM_RATE ,@TERM_AMT ,@TOTAL_AMT ,@LESS_LOAD_AMT ,@TOTAL_BAG ,@ADVANCE_PAID ,@GST_PAID_ADV ,@NET_PAY ,@NOTIFICATION_TEXT ,@COMM ,@DIV_ADD ,@INV_TYPE ,@INV_RULE ,@FORM_NAME,@EMP_ID, @TCS_TAX, @TCS_AMT)"
                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@SO_NO", TextBox124.Text)
                cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@PO_NO", actual_so)
                cmd1.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(SO_ACTUAL_DATE.Day & "-" & SO_ACTUAL_DATE.Month & "-" & SO_ACTUAL_DATE.Year), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@AMD_NO", TextBox67.Text)
                cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(TextBox68.Text, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@TRANS_WO", transporter)
                cmd1.Parameters.AddWithValue("@TRANS_SLNO", trans_sl_no)
                cmd1.Parameters.AddWithValue("@TRANS_NAME", TextBox62.Text)
                cmd1.Parameters.AddWithValue("@TRUCK_NO", TextBox55.Text)
                cmd1.Parameters.AddWithValue("@ED_COMDT", excise_comodity)
                cmd1.Parameters.AddWithValue("@PARTY_CODE", PARTY_CODE)
                cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CONSIGN_CODE)
                cmd1.Parameters.AddWithValue("@MAT_VOCAB", TextBox53.Text)
                cmd1.Parameters.AddWithValue("@MAT_SLNO", DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim)
                cmd1.Parameters.AddWithValue("@INV_NO", TextBox65.Text)
                cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@CHPTR_HEAD", chptr_heading)
                cmd1.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
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
                cmd1.Parameters.AddWithValue("@RR_NO", TextBox69.Text)
                cmd1.Parameters.AddWithValue("@RR_DATE", TextBox5.Text)
                cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT ", weight)
                cmd1.Parameters.AddWithValue("@D_TYPE", inv_for)
                cmd1.Parameters.AddWithValue("@ACC_UNIT", Label289.Text)
                cmd1.Parameters.AddWithValue("@PURITY", 0)
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
                cmd1.Parameters.AddWithValue("@FRT_WT_AMT", 0)
                cmd1.Parameters.AddWithValue("@TOTAL_PCS", total_pcs)
                cmd1.Parameters.AddWithValue("@P_CODE ", DropDownList5.SelectedValue)
                cmd1.Parameters.AddWithValue("@P_DESC", Label467.Text)
                cmd1.Parameters.AddWithValue("@D1", "")
                cmd1.Parameters.AddWithValue("@D2", "")
                cmd1.Parameters.AddWithValue("@D3", "")
                cmd1.Parameters.AddWithValue("@D4", "")
                cmd1.Parameters.AddWithValue("@PACK_TYPE", pack_type)
                cmd1.Parameters.AddWithValue("@FRT_TYPE", freight_type)
                cmd1.Parameters.AddWithValue("@REBATE_TYPE ", DISC_TYPE)
                cmd1.Parameters.AddWithValue("@TOTAL_QTY", CDec(TextBox54.Text))
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
                cmd1.Parameters.AddWithValue("@CGST_AMT", CDec(TextBox42.Text))
                cmd1.Parameters.AddWithValue("@SGST_RATE", SGST)
                cmd1.Parameters.AddWithValue("@SGST_AMT", CDec(TextBox40.Text))
                cmd1.Parameters.AddWithValue("@IGST_RATE", IGST)
                cmd1.Parameters.AddWithValue("@IGST_AMT", CDec(TextBox44.Text))
                cmd1.Parameters.AddWithValue("@CESS_RATE", item_cess)
                cmd1.Parameters.AddWithValue("@CESS_AMT", CDec(TextBox21.Text))
                cmd1.Parameters.AddWithValue("@TERM_RATE", terminal_tax)
                cmd1.Parameters.AddWithValue("@TERM_AMT", CDec(TextBox43.Text))
                cmd1.Parameters.AddWithValue("@TOTAL_AMT", total_amt)
                cmd1.Parameters.AddWithValue("@LESS_LOAD_AMT", 0.0)
                cmd1.Parameters.AddWithValue("@ADVANCE_PAID", ADV_AMT)
                cmd1.Parameters.AddWithValue("@GST_PAID_ADV", ADV_GST)
                cmd1.Parameters.AddWithValue("@NET_PAY", NET_PAY)
                cmd1.Parameters.AddWithValue("@COMM", COMM)
                cmd1.Parameters.AddWithValue("@DIV_ADD", DIVISION)
                cmd1.Parameters.AddWithValue("@INV_TYPE", inv_type)
                cmd1.Parameters.AddWithValue("@INV_RULE", inv_rule)
                cmd1.Parameters.AddWithValue("@FORM_NAME", DropDownList7.Text)
                cmd1.Parameters.AddWithValue("@TOTAL_BAG", TextBox56.Text)
                cmd1.Parameters.AddWithValue("@NOTIFICATION_TEXT", TextBox64.Text)
                cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                cmd1.Parameters.AddWithValue("@INV_STATUS", "ACTIVE")
                cmd1.Parameters.AddWithValue("@TCS_TAX", 1)
                cmd1.Parameters.AddWithValue("@TCS_AMT", TextBox66.Text)
                cmd1.Parameters.AddWithValue("@QUALITY", qual_desc)
                cmd1.Parameters.AddWithValue("@TRANSPORTER_WEIGHT", TextBox7.Text)
                cmd1.Parameters.AddWithValue("@MATERIAL_TYPE", "Outsourced Finished Goods")
                cmd1.ExecuteReader()
                cmd1.Dispose()
                'conn.Close()

                ''update sale order update outsource_F_ITEM
                Dim lp As Integer = 0
                For lp = 0 To GridView2.Rows.Count - 1
                    Dim sendqty As Decimal = 0
                    If (GridView2.Rows(lp).Cells(3).Text = "Pcs" Or GridView2.Rows(lp).Cells(3).Text = "PCS") Then
                        sendqty = CInt(GridView2.Rows(lp).Cells(4).Text)
                    ElseIf (GridView2.Rows(lp).Cells(3).Text = "Set" Or GridView2.Rows(lp).Cells(3).Text = "SET") Then
                        sendqty = CInt(GridView2.Rows(lp).Cells(4).Text)
                    Else
                        sendqty = CDec(GridView2.Rows(lp).Cells(6).Text)
                    End If

                    'Update outsource_F_ITEM
                    QUARY1 = "update outsource_F_ITEM set ITEM_F_STOCK =ITEM_F_STOCK - " & sendqty & " ,ITEM_LAST_DESPATCH = @ITEM_LAST_DESPATCH where ITEM_CODE ='" & GridView2.Rows(lp).Cells(1).Text & "'"
                    Dim cmd2 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmd2.Parameters.AddWithValue("@ITEM_LAST_DESPATCH", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                    cmd2.ExecuteReader()
                    cmd2.Dispose()

                    ''update so order
                    QUARY1 = "update SO_MAT_ORDER set ITEM_QTY_SEND =ITEM_QTY_SEND + " & sendqty & " where SO_NO ='" & TextBox124.Text & "' and ITEM_SLNO ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and ITEM_CODE ='" & GridView2.Rows(lp).Cells(1).Text & "' AND AMD_NO='" & TextBox67.Text & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()


                    ''insert into prod control
                    conn.Open()
                    Dim mcc As New SqlCommand
                    Dim fr_stock, bsr_stock As Decimal
                    fr_stock = 0
                    bsr_stock = 0
                    mcc.CommandText = "select (case when (ITEM_F_STOCK) is null then '0' else (ITEM_F_STOCK) end ) as fr_stock ,(case when (ITEM_B_STOCK) is null then '0' else (ITEM_B_STOCK) end ) as bsr_stock from outsource_F_ITEM WITH(NOLOCK) where ITEM_CODE='" & GridView2.Rows(lp).Cells(1).Text & "' "
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
                    'conn.Open()
                    QUARY1 = "Insert Into OUTSOURCED_PROD_CONTROL(ENTRY_DATE,FISCAL_YEAR,INV_NO,ITEM_CODE,PROD_DATE,ITEM_F_QTY,ITEM_B_QTY,ITEM_I_QTY,ITEM_I_SO,ITEM_F_STOCK,ITEM_B_STOCK,ITEM_I_TOTAL)values(@ENTRY_DATE,@FISCAL_YEAR,@INV_NO,@ITEM_CODE,@PROD_DATE,@ITEM_F_QTY,@ITEM_B_QTY,@ITEM_I_QTY,@ITEM_I_SO,@ITEM_F_STOCK,@ITEM_B_STOCK,@ITEM_I_TOTAL)"
                    cmd1 = New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@ITEM_CODE", GridView2.Rows(lp).Cells(1).Text)
                    cmd1.Parameters.AddWithValue("@PROD_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@ITEM_F_QTY", 0.0)
                    cmd1.Parameters.AddWithValue("@ITEM_B_QTY", 0.0)
                    cmd1.Parameters.AddWithValue("@ITEM_I_QTY", sendqty)
                    cmd1.Parameters.AddWithValue("@ITEM_I_SO", TextBox124.Text)
                    cmd1.Parameters.AddWithValue("@INV_NO", inv_for & TextBox65.Text)
                    cmd1.Parameters.AddWithValue("@ITEM_F_STOCK", fr_stock)
                    cmd1.Parameters.AddWithValue("@ITEM_B_STOCK", bsr_stock)
                    cmd1.Parameters.AddWithValue("@ITEM_I_TOTAL", 0.0)
                    cmd1.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()
                    'conn.Close


                    ''LINE NO
                    Dim LINO_SL As Integer = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select LINE_NO from MAT_DETAILS WHERE FISCAL_YEAR =" & CInt(STR1) & " AND MAT_CODE ='" & GridView2.Rows(lp).Cells(1).Text & "' AND LINE_NO <> 0", conn)
                    LINO_SL = da.Fill(ds, "MAT_DETAILS")
                    conn.Close()


                    Dim Query As String = "Insert Into MAT_DETAILS(MAT_SL_NO,ENTRY_DATE,MAT_QTY,LINE_DATE,ISSUE_NO,LINE_NO,FISCAL_YEAR,LINE_TYPE,MAT_CODE,RQD_QTY,ISSUE_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,PURPOSE,COST_CODE,AUTH_BY,ISSUE_TYPE,RQD_DATE)VALUES(@MAT_SL_NO,@ENTRY_DATE,@MAT_QTY,@LINE_DATE,@ISSUE_NO,@LINE_NO,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@RQD_QTY,@ISSUE_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@PURPOSE,@COST_CODE,@AUTH_BY,@ISSUE_TYPE,@RQD_DATE)"
                    Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@ISSUE_NO", inv_for & TextBox65.Text)
                    cmd.Parameters.AddWithValue("@ISSUE_TYPE", ORDER_TO)
                    cmd.Parameters.AddWithValue("@LINE_NO", LINO_SL + 1)
                    cmd.Parameters.AddWithValue("@LINE_DATE", working_date)
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
                    cmd.Parameters.AddWithValue("@LINE_TYPE", "S")
                    cmd.Parameters.AddWithValue("@RQD_DATE", working_date.Date)
                    cmd.Parameters.AddWithValue("@MAT_CODE", GridView2.Rows(lp).Cells(1).Text)
                    cmd.Parameters.AddWithValue("@MAT_QTY", 0.0)
                    cmd.Parameters.AddWithValue("@RQD_QTY", sendqty)
                    cmd.Parameters.AddWithValue("@ISSUE_QTY", sendqty)
                    cmd.Parameters.AddWithValue("@MAT_BALANCE", fr_stock)
                    cmd.Parameters.AddWithValue("@UNIT_PRICE", unit_rate)
                    cmd.Parameters.AddWithValue("@TOTAL_PRICE", TAXABLE_VALUE)
                    cmd.Parameters.AddWithValue("@PURPOSE", "SALE")
                    cmd.Parameters.AddWithValue("@COST_CODE", TextBox125.Text)
                    cmd.Parameters.AddWithValue("@AUTH_BY", Session("userName"))
                    cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd.Parameters.AddWithValue("@MAT_SL_NO", DropDownList4.SelectedValue)
                    cmd.ExecuteReader()
                    cmd.Dispose()
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
                MC5.CommandText = "select work_group.TCS_OUTGOING,work_group.gst_expenditure,ORDER_DETAILS .SO_NO ,ORDER_DETAILS .ORDER_TO, dater.stock_ac_head,dater.iuca_head,work_group.adv_pay,work_group.lcgst,work_group .lsgst,work_group .ligst ,work_group .lcess,work_group.cgst,work_group.sgst,work_group.igst,work_group.cess, work_group.ed_head,work_group.vat_head,work_group.cst_head,work_group.freight_head,work_group.term_tax,work_group.tds_head,work_group.pack_head from ORDER_DETAILS join DATER on ORDER_DETAILS .PARTY_CODE=DATER.d_code JOIN work_group on ORDER_DETAILS.ORDER_TYPE =work_group.work_name and  ORDER_DETAILS.PO_TYPE  =work_group.work_type and ORDER_DETAILS.ORDER_TO  =work_group.d_type WHERE ORDER_DETAILS.SO_NO='" & TextBox124.Text & "'"
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORDER_TO = dr.Item("ORDER_TO")
                    STOCK_HEAD = "61998"
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

                If ORDER_TO = "I.P.T." Then
                    If FINANCE_ARRANGE = "Book Adjustment" Or FINANCE_ARRANGE = "CREDIT" Then
                        'save_ledger(TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox41.Text) + CDec(TextBox21.Text) - CDec(TextBox37.Text), "STOCK TRANSFOR")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox41.Text) - CDec(TextBox37.Text), "STOCK TRANSFOR")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, IUCA, "Dr", CDec(TextBox45.Text), "IUCA")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, FREIGHT, "Cr", CDec(TextBox39.Text), "FREIGHT")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lcgst, "Cr", CDec(TextBox42.Text), "CGST_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lsgst, "Cr", CDec(TextBox40.Text), "SGST_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, ligst, "Cr", CDec(TextBox44.Text), "IGST_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lcess, "Cr", CDec(TextBox21.Text), "CESS_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, TERMINAL, "Cr", CDec(TextBox43.Text), "TERMINAL TAX")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, TCS, "Cr", CDec(TextBox66.Text), "TCS_OUTPUT")

                    End If

                ElseIf ORDER_TO = "Other" Then

                    If FINANCE_ARRANGE = "ADVANCE" Then
                        'save_ledger(TextBox124.Text, inv_for & CStr(DESPATCH_TYPE) & TextBox65.Text, TextBox125.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox41.Text) + CDec(TextBox21.Text) - CDec(TextBox37.Text), "STOCK TRANSFOR")
                        'save_ledger(TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox40.Text) + CDec(TextBox42.Text) + CDec(TextBox44.Text) + CDec(TextBox41.Text) + CDec(TextBox21.Text) - CDec(TextBox37.Text), "STOCK TRANSFOR")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox41.Text) - CDec(TextBox37.Text), "STOCK TRANSFOR")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, adv_pay_head, "Dr", CDec(TextBox45.Text), "ADV_PAY")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, FREIGHT, "Cr", CDec(TextBox39.Text), "FREIGHT")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lcgst, "Cr", CDec(TextBox42.Text), "CGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lsgst, "Cr", CDec(TextBox40.Text), "SGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, ligst, "Cr", CDec(TextBox44.Text), "IGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lcess, "Cr", CDec(TextBox21.Text), "CESS_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, TERMINAL, "Cr", CDec(TextBox43.Text), "TERMINAL TAX")
                        save_ledger(DropDownList1.SelectedValue, TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, TCS, "Cr", CDec(TextBox66.Text), "TCS_OUTPUT")

                    ElseIf FINANCE_ARRANGE = "BG" Then

                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, STOCK_HEAD, "Cr", CDec(ass_price) + CDec(TextBox41.Text) - CDec(TextBox37.Text), "STOCK TRANSFOR")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, "62203", "Dr", CDec(TextBox45.Text), "SUND_DEBTOR_OTHER")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, FREIGHT, "Cr", CDec(TextBox39.Text), "FREIGHT")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lcgst, "Cr", CDec(TextBox42.Text), "CGST_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lsgst, "Cr", CDec(TextBox40.Text), "SGST_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, ligst, "Cr", CDec(TextBox44.Text), "IGST_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, lcess, "Cr", CDec(TextBox21.Text), "CESS_PAYABLE")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, TERMINAL, "Cr", CDec(TextBox43.Text), "TERMINAL TAX")
                        save_ledger("", TextBox124.Text, inv_for & TextBox65.Text, TextBox125.Text, TCS, "Cr", CDec(TextBox66.Text), "TCS_OUTPUT")

                    End If

                End If

                'UPDATE SALE_RCD_VOUCHER
                If DropDownList1.SelectedValue <> "N/A" Then

                    QUARY1 = ""
                    QUARY1 = "UPDATE SALE_RCD_VOUCHAR SET BAL_AMT =BAL_AMT- " & CDec(TextBox45.Text) & ",CGST_BAL =CGST_BAL - " & CDec(TextBox42.Text) & ",SGST_BAL =SGST_BAL- " & CDec(TextBox40.Text) & ",IGST_BAL =IGST_BAL- " & CDec(TextBox44.Text) & ",CESS_BAL =CESS_BAL- " & CDec(TextBox21.Text) & ",TT_TAX_BAL=TT_TAX_BAL - " & CDec(TextBox43.Text) & " WHERE VOUCHER_TYPE + VOUCHER_NO ='" & DropDownList1.Text & "' AND SO_NO ='" & TextBox124.Text & "' AND ITEM_SLNO ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()

                End If


                If transporter = "N/A" Or transporter = "PARTY" Then
                    ''insert inv_print

                    QUARY1 = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
                    Dim scmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    scmd1.Parameters.AddWithValue("@INV_NO", inv_for & TextBox65.Text)
                    scmd1.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
                    scmd1.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
                    scmd1.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
                    scmd1.Parameters.AddWithValue("@F_YEAR", STR1)
                    scmd1.ExecuteReader()
                    scmd1.Dispose()

                Else

                    Dim W_AU As New String("")

                    conn.Open()
                    mc1.CommandText = "select MAX(W_AU) As W_AU from WO_ORDER where PO_NO='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' and W_SLNO='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        W_AU = dr.Item("W_AU")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()

                    If (W_AU = "Vehicle") Then
                        'update despatch if A/U is Vehicle

                        QUARY1 = "update DESPATCH set INV_STATUS ='Pending' where INV_NO  ='" & TextBox65.Text & "'"
                        Dim despatch As New SqlCommand(QUARY1, conn_trans, myTrans)
                        despatch.ExecuteReader()
                        despatch.Dispose()

                    ElseIf (W_AU = "Mt" Or W_AU = "MT" Or W_AU = "MTS") Then

                        Dim PROV_PRICE_FOR_TRANSPORTER As Decimal = 0.0
                        Dim RCVD_QTY_TRANSPORTER As Decimal = 0.0
                        i = 0
                        For i = 0 To GridView2.Rows.Count - 1
                            'RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(GridView2.Rows(i).Cells(6).Text)
                            RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(TextBox7.Text)
                        Next

                        ''UPDATE WO_ORDER
                        mycommand = New SqlCommand("update WO_ORDER set W_COMPLITED =W_COMPLITED + " & RCVD_QTY_TRANSPORTER & " where PO_NO='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1) & "' AND W_SLNO='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "'  and AMD_DATE =(select max(AMD_DATE) from WO_ORDER where PO_NO ='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1) & "' and W_SLNO = '" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1) & "' and AMD_DATE < ='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "')", conn_trans, myTrans)
                        mycommand.ExecuteNonQuery()


                        ''INSERT MB_BOOK FOR TRANSPORTER
                        conn.Open()
                        Dim w_qty, w_complite, w_unit_price, W_discount, mat_price As Decimal
                        Dim WO_NAME, WO_AMD, AMD_DATE As New String("")
                        Dim WO_AU As String = ""
                        Dim WO_SUPL_ID As String = ""
                        Dim MCqq As New SqlCommand
                        Dim des_date As Date = Today.Date
                        MCqq.CommandText = "select MAX(WO_AMD) AS WO_AMD ,MAX(AMD_DATE) AS AMD_DATE, sum(W_MATERIAL_COST) as W_MATERIAL_COST,MAX(SUPL_ID) as SUPL_ID, sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER WITH(NOLOCK) where PO_NO = '" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' and w_slno=" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim & " and AMD_DATE < ='" & des_date.Year & "-" & des_date.Month & "-" & des_date.Day & "'"
                        MCqq.Connection = conn
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
                        conn.Close()

                        'update despatch
                        'TRANSPORTER AU WISE ENTRY

                        'QUARY1 = "update DESPATCH set INV_STATUS ='' where INV_NO  ='" & TextBox65.Text & "'"
                        'Dim despatch As New SqlCommand(QUARY1, conn_trans, myTrans)
                        'despatch.ExecuteReader()
                        'despatch.Dispose()

                        ''insert inv_print

                        QUARY1 = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
                        Dim scmd As New SqlCommand(QUARY1, conn_trans, myTrans)
                        scmd.Parameters.AddWithValue("@INV_NO", inv_for & TextBox65.Text)
                        scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
                        scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
                        scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
                        scmd.Parameters.AddWithValue("@F_YEAR", STR1)
                        scmd.ExecuteReader()
                        scmd.Dispose()


                        ''calculate work amount
                        Dim base_value, discount_value, mat_rate, balance As Decimal
                        base_value = 0
                        discount_value = 0
                        mat_rate = 0
                        balance = 0
                        base_value = w_unit_price * RCVD_QTY_TRANSPORTER
                        discount_value = (base_value * W_discount) / 100
                        PROV_PRICE_FOR_TRANSPORTER = FormatNumber(base_value - discount_value, 2)


                        If (Label289.Text = "Pcs" Or Label289.Text = "PCS") Then
                            balance = w_qty - w_complite - CInt(TextBox54.Text)

                        Else
                            balance = w_qty - w_complite - weight
                        End If

                        balance = w_qty - w_complite

                        Dim Query As String = "Insert Into mb_book(unit_price,Entry_Date,mb_no,mb_date,po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,mb_by,ra_no,prov_amt,pen_amt,sgst,cgst,igst,cess,sgst_liab,cgst_liab,igst_liab,cess_liab,it_amt,pay_ind,fiscal_year,mat_rate,mb_clear,amd_no,amd_date)VALUES(@unit_price,@Entry_Date,@mb_no,@mb_date,@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@mb_by,@ra_no,@prov_amt,@pen_amt,@sgst,@cgst,@igst,@cess,@sgst_liab,@cgst_liab,@igst_liab,@cess_liab,@it_amt,@pay_ind,@fiscal_year,@mat_rate,@mb_clear,@amd_no,@amd_date)"
                        Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@mb_no", inv_for & TextBox65.Text)
                        cmd.Parameters.AddWithValue("@mb_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@po_no", DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim)
                        cmd.Parameters.AddWithValue("@supl_id", WO_SUPL_ID)
                        cmd.Parameters.AddWithValue("@wo_slno", DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim)
                        cmd.Parameters.AddWithValue("@w_name", WO_NAME)
                        cmd.Parameters.AddWithValue("@w_au", WO_AU)
                        cmd.Parameters.AddWithValue("@from_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@to_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@work_qty", RCVD_QTY_TRANSPORTER)
                        cmd.Parameters.AddWithValue("@rqd_qty", RCVD_QTY_TRANSPORTER)
                        cmd.Parameters.AddWithValue("@bal_qty", balance)
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



                        ''SEARCH AC HEAD
                        conn.Open()
                        Dim TRANS_PROV, TRANS_PUR As String
                        TRANS_PROV = ""
                        TRANS_PUR = ""
                        MCqq.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "') and work_type=(select distinct wo_type from wo_order WITH(NOLOCK) where po_no='" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim & "' and w_slno='" & DropDownList27.Text.Substring(0, DropDownList27.Text.IndexOf(",") - 1).Trim & "')"
                        MCqq.Connection = conn
                        dr = MCqq.ExecuteReader
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
                        save_ledger("", DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim, inv_for & TextBox65.Text, WO_SUPL_ID, TRANS_PUR, "Dr", PROV_PRICE_FOR_TRANSPORTER, "PUR")
                        save_ledger("", DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1).Trim, inv_for & TextBox65.Text, WO_SUPL_ID, TRANS_PROV, "Cr", PROV_PRICE_FOR_TRANSPORTER, "PROV")
                    End If


                End If

                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(8) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("UNIT_WEIGHT"), New DataColumn("TOTAL_WEIGHT"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
                ViewState("despatch") = dt7
                BINDGRID()
                TextBox55.Text = ""
                TextBox69.Text = ""
                DropDownList7.SelectedValue = "N/A"
                TextBox37.Text = "0.00"
                TextBox38.Text = "0.00"
                TextBox39.Text = "0.00"
                TextBox40.Text = "0.00"
                TextBox41.Text = "0.00"
                TextBox42.Text = "0.00"
                TextBox43.Text = "0.00"
                TextBox44.Text = "0.00"
                TextBox66.Text = "0.00"
                TextBox45.Text = "0.00"
                TextBox21.Text = "0.00"
                TextBox54.Text = ""
                TextBox56.Text = ""

                Dim tcsFlag As String
                If (CDec(TextBox66.Text) > 0) Then
                    tcsFlag = "Y"
                Else
                    tcsFlag = "N"
                End If

                'myTrans.Commit()
                'Label308.Text = "All records are written to database."

                ''===========================Generate E-Invoice Start=======================''

                'If gst_code = my_gst_code Then
                '    'e-invoice is not required
                'Else
                '    Dim logicclassobj = New EinvoiceLogicClass
                '    Dim autherrordata As List(Of AuthenticationErrorDetailsClass) = logicclassobj.EinvoiceAuthentication(TextBox177.Text + TextBox65.Text, TextBox125.Text)
                '    If (autherrordata.Item(0).status = "1") Then

                '        Dim einverrordata As List(Of EinvoiceErrorDetailsClass) = logicclassobj.GenerateEInvoice(autherrordata.Item(0).client_id, autherrordata.Item(0).client_secret, autherrordata.Item(0).gst_no, autherrordata.Item(0).user_name, autherrordata.Item(0).AuthToken, autherrordata.Item(0).Sek, autherrordata.Item(0).appKey, autherrordata.Item(0).systemInvoiceNo, autherrordata.Item(0).buyerPartyCode, "no")
                '        If (einverrordata.Item(0).status = "1") Then
                '            TextBox6.Text = einverrordata.Item(0).IRN

                '            Dim sqlquery As String = ""
                '            sqlquery = "update despatch set irn_no ='" & einverrordata.Item(0).IRN & "',qr_code ='" & einverrordata.Item(0).QRCode & "' where d_type+inv_no  ='" & TextBox177.Text + TextBox65.Text & "' and fiscal_year='" & STR1 & "'"
                '            Dim despatch As New SqlCommand(sqlquery, conn_trans, myTrans)
                '            despatch.ExecuteReader()
                '            despatch.Dispose()

                '            goAheadFlag = True
                '        ElseIf (einverrordata.Item(0).status = "2") Then
                '            Label31.Visible = True
                '            Label42.Visible = True
                '            txtEinvoiceErrorCode.Visible = True
                '            txtEinvoiceErrorMessage.Visible = True
                '            txtEinvoiceErrorCode.Text = einverrordata.Item(0).errorCode
                '            txtEinvoiceErrorMessage.Text = einverrordata.Item(0).errorMessage
                '            goAheadFlag = False
                '            Label308.Text = "there is some response error in e-invoice generation."
                '        End If

                '    ElseIf (autherrordata.Item(0).status = "2") Then

                '        Label31.Visible = True
                '        Label42.Visible = True
                '        txtEinvoiceErrorCode.Visible = True
                '        txtEinvoiceErrorMessage.Visible = True
                '        txtEinvoiceErrorCode.Text = autherrordata.Item(0).errorCode
                '        txtEinvoiceErrorMessage.Text = autherrordata.Item(0).errorMessage
                '        goAheadFlag = False
                '        Label308.Text = "there is some response error in e-invoice authentication."
                '    Else
                '        goAheadFlag = False
                '        Label308.Text = "there is some response error in e-invoice authentication."
                '    End If


                'End If

                'If (goAheadFlag = True) Then
                '    myTrans.Commit()
                '    Label308.Text = "all records are written to database."
                '    Label31.Visible = False
                '    Label42.Visible = False
                '    txtEinvoiceErrorCode.Visible = False
                '    txtEinvoiceErrorMessage.Visible = False
                'Else
                '    myTrans.Rollback()
                '    conn.Close()
                '    conn_trans.Close()
                '    TextBox65.Text = ""
                '    TextBox177.Text = ""
                '    TextBox6.Text = ""

                'End If

                ''===========================Generate E-Invoice End=======================''

                ''===========================Generate E-Invoice Through EY Start=======================''
                If (TextBox125.Text <> "D8888") Then
                    If gst_code = my_gst_code Then
                        ''Only E-way bill is required
                        Dim logicClassObj = New EinvoiceLogicClassEY
                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox177.Text + TextBox65.Text, TextBox125.Text)
                        If (AuthErrorData.Item(0).status = "1") Then

                            Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEwayBillOnly(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox177.Text + TextBox65.Text, TextBox125.Text, TextBox18.Text, "NO", tcsFlag)
                            If (EinvErrorData.Item(0).status = "1") Then

                                TextBox8.Text = EinvErrorData.Item(0).EwbNo
                                TextBox20.Text = EinvErrorData.Item(0).EwbValidTill

                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set EWB_NO ='" & EinvErrorData.Item(0).EwbNo & "',EWB_DATE ='" & EinvErrorData.Item(0).EwbDt & "',EWB_VALIDITY ='" & EinvErrorData.Item(0).EwbValidTill & "', EWB_STATUS ='ACTIVE' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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
                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox177.Text + TextBox65.Text, TextBox125.Text)
                        If (AuthErrorData.Item(0).status = "1") Then
                            Dim authIdToken As String = AuthErrorData.Item(0).Idtoken
                            Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox177.Text + TextBox65.Text, TextBox125.Text, TextBox18.Text, "NO", tcsFlag, "INV")
                            If (EinvErrorData.Item(0).status = "1") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN
                                TextBox8.Text = EinvErrorData.Item(0).EwbNo
                                TextBox20.Text = EinvErrorData.Item(0).EwbValidTill


                                '================SENDING DATA TO EY PORTAL START==================='
                                'Dim result
                                'If (Label289.Text = "Service") Then
                                '    If (BILL_PARTY_GST = "") Then
                                '        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "NO", dr.Item("INV_DATE"))
                                '    Else
                                '        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "YES", dr.Item("INV_DATE"))
                                '    End If

                                'Else
                                '    If (BILL_PARTY_GST = "") Then
                                '        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "NO", dr.Item("INV_DATE"))
                                '    Else
                                '        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "YES", dr.Item("INV_DATE"))
                                '    End If

                                'End If

                                Dim result
                                If (BILL_PARTY_GST = "") Then
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox125.Text, TextBox18.Text, "NO", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                Else
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox125.Text, TextBox18.Text, "NO", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                End If

                                '================SENDING DATA TO EY PORTAL END==================='
                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "',EWB_NO ='" & EinvErrorData.Item(0).EwbNo & "',EWB_DATE ='" & EinvErrorData.Item(0).EwbDt & "',EWB_VALIDITY ='" & EinvErrorData.Item(0).EwbValidTill & "', EWB_STATUS ='ACTIVE', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox125.Text, TextBox18.Text, "NO", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                Else
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox125.Text, TextBox18.Text, "NO", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                End If

                                '================SENDING DATA TO EY PORTAL END==================='

                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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
                    TextBox65.Text = ""
                    TextBox177.Text = ""
                    TextBox6.Text = ""

                End If

                ''===========================Generate E-Invoice Through EY End=======================''


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                TextBox65.Text = ""
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

        End If
    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Or DropDownList1.SelectedValue = "N/A" Then
            DropDownList1.Focus()
            TextBox9.Text = 0.0
            TextBox176.Text = ""
            Return
        End If
        conn.Open()
        mycommand.CommandText = "SELECT * FROM SALE_RCD_VOUCHAR WHERE VOUCHER_TYPE + VOUCHER_NO ='" & DropDownList1.SelectedValue & "'"
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

    Public Function GenerateJsonData()
        Dim working_date As Date = Today.Date
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

        Dim basic As New EinvoiceModelClass()
        basic.Version = "1.03"
        basic.TranDtls.TaxSch = "GST"
        basic.TranDtls.SupTyp = "B2B"
        'basic.tranDtls.TaxSch = New String() {"Small", "Medium", "Large"}
        basic.TranDtls.RegRev = "N"
        basic.TranDtls.IgstOnIntra = "N"

        basic.DocDtls.Typ = "INV"
        basic.DocDtls.No = TextBox177.Text + TextBox65.Text
        'basic.DocDtls.Dt = working_date.Day + "/" + working_date.Month + "/" + working_date.Year
        basic.DocDtls.Dt = working_date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        'basic.DocDtls.Dt = "19/09/2020"

        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from comp_profile"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            basic.SellerDtls.Gstin = dr.Item("c_gst_no")
            basic.SellerDtls.LglNm = dr.Item("c_name")
            basic.SellerDtls.Addr1 = dr.Item("c_add")
            basic.SellerDtls.Addr2 = dr.Item("c_add1")
            basic.SellerDtls.Loc = dr.Item("c_city")
            basic.SellerDtls.Pin = Convert.ToInt32(dr.Item("c_pin"))
            basic.SellerDtls.Stcd = dr.Item("c_state_code")
            basic.SellerDtls.Ph = dr.Item("c_contact_no")
            basic.SellerDtls.Em = dr.Item("c_email")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "select * from dater where d_code='" & TextBox125.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            basic.BuyerDtls.Gstin = dr.Item("gst_code")
            basic.BuyerDtls.LglNm = dr.Item("d_name")
            basic.BuyerDtls.Pos = dr.Item("d_state_code")
            basic.BuyerDtls.Addr1 = dr.Item("add_1")
            basic.BuyerDtls.Addr2 = dr.Item("add_2")
            basic.BuyerDtls.Loc = dr.Item("d_city")
            basic.BuyerDtls.Pin = dr.Item("d_pin")
            basic.BuyerDtls.Stcd = dr.Item("d_state_code")
            basic.BuyerDtls.Ph = dr.Item("d_contact")
            basic.BuyerDtls.Em = dr.Item("d_email")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim itemDetails As List(Of ItemDetails) = New List(Of ItemDetails)()

        conn.Open()
        mc1.CommandText = "select * from despatch with(nolock) where d_type+inv_no='" & TextBox177.Text + TextBox65.Text & "' and FISCAL_YEAR='" & STR1 & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            Dim isService As String
            If (dr.Item("ACC_UNIT") = "Service") Then
                isService = "Y"
            Else
                isService = "N"
            End If

            Dim gstRate As Decimal
            If (CDec(dr.Item("IGST_RATE")) = 0) Then
                gstRate = CDec(dr.Item("CGST_RATE")) + CDec(dr.Item("SGST_RATE"))
            Else
                gstRate = dr.Item("IGST_RATE")
            End If
            itemDetails.Add(New ItemDetails With {.SlNo = dr.Item("MAT_SLNO"), .PrdDesc = dr.Item("P_DESC"), .IsServc = isService,
                            .HsnCd = dr.Item("CHPTR_HEAD"), .Qty = dr.Item("TOTAL_QTY"), .Unit = dr.Item("ACC_UNIT"),
                            .UnitPrice = Math.Round(CDec(dr.Item("TAXABLE_VALUE")) / CDec(dr.Item("TOTAL_QTY")), 3), .TotAmt = Math.Round(dr.Item("TAXABLE_VALUE"), 2),
                            .Discount = Math.Round(0, 2), .AssAmt = Math.Round(dr.Item("TAXABLE_VALUE"), 2), .GstRt = gstRate, .IgstAmt = Math.Round(dr.Item("IGST_AMT"), 2),
                            .CgstAmt = Math.Round(dr.Item("CGST_AMT"), 2), .SgstAmt = Math.Round(dr.Item("SGST_AMT"), 2), .TotItemVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT"))), 2),
                            .OrdLineRef = dr.Item("MAT_SLNO")})

            basic.ItemList = itemDetails

            basic.ValDtls.AssVal = Math.Round(dr.Item("TAXABLE_VALUE"), 2)
            basic.ValDtls.CgstVal = Math.Round(dr.Item("CGST_AMT"), 2)
            basic.ValDtls.SgstVal = Math.Round(dr.Item("SGST_AMT"), 2)
            basic.ValDtls.IgstVal = Math.Round(dr.Item("IGST_AMT"), 2)
            basic.ValDtls.Discount = 0.00
            basic.ValDtls.RndOffAmt = 0.00
            basic.ValDtls.TotInvVal = Math.Round((CDec(dr.Item("TAXABLE_VALUE")) + CDec(dr.Item("IGST_AMT")) + CDec(dr.Item("CGST_AMT")) + CDec(dr.Item("SGST_AMT"))), 2)
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim output As String = Json.JsonConvert.SerializeObject(basic)
        Return output

    End Function

    Overridable Sub AuthenticationErrorMethod(ErrorType As String, ErrorCode As String, ErrorMessage As String)
        If (ErrorType = "1") Then
            Label31.Visible = True
            Label42.Visible = True
            txtEinvoiceErrorCode.Visible = True
            txtEinvoiceErrorMessage.Visible = True
            txtEinvoiceErrorCode.Text = ErrorCode
            txtEinvoiceErrorMessage.Text = ErrorMessage
            goAheadFlag = False
            Label308.Text = "There was some response error in E-invoice Authentication."
        Else
            goAheadFlag = False
            Label308.Text = "There was some response error in E-invoice Authentication."
        End If


    End Sub

    Overridable Sub EinvoiceErrorMethod(ErrorType As String, ErrorCode As String, ErrorMessage As String)
        If (ErrorType = "1") Then
            Label31.Visible = True
            Label42.Visible = True
            txtEinvoiceErrorCode.Visible = True
            txtEinvoiceErrorMessage.Visible = True
            txtEinvoiceErrorCode.Text = ErrorCode
            txtEinvoiceErrorMessage.Text = ErrorMessage
            goAheadFlag = False
            Label308.Text = "There is some response error in E-Invoice generation."
        ElseIf (ErrorType = "2") Then
            goAheadFlag = False
            Label308.Text = "There is some response error in E-Invoice generation."
        ElseIf (ErrorType = "3") Then
            goAheadFlag = True
            TextBox6.Text = ErrorMessage
        End If

    End Sub


End Class