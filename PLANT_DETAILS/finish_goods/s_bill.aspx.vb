Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports System.Net

Public Class s_bill1
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
    Dim goAheadFlag As Boolean = True
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
    End Sub

    Protected Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        If DropDownList29.Text = "" Then
            DropDownList29.Focus()
            Return
        End If
        If DropDownList29.Text.IndexOf(",") <> 14 Then
            DropDownList29.Text = ""
            DropDownList29.Focus()
            Return
        End If
        conn.Open()
        count = 0
        Dim dt1 As New DataTable()
        da = New SqlDataAdapter("SELECT SO_NO  FROM ORDER_DETAILS  WHERE SO_NO ='" & DropDownList29.Text.Substring(0, DropDownList29.Text.IndexOf(",") - 1).Trim & "' AND SO_STATUS ='DRAFT'", conn)
        count = da.Fill(dt1)
        conn.Close()
        If count = 0 Then
            DropDownList29.Text = ""
            DropDownList29.Focus()
            Return
        End If
        Dim working_date As Date = Today.Date
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select dater.d_code,dater.d_name from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & DropDownList29.Text.Substring(0, DropDownList29.Text.IndexOf(",") - 1).Trim & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox180.Text = dr.Item("d_code")
            TextBox181.Text = dr.Item("d_name")
            dr.Close()
        End If
        conn.Close()
        TextBox179.Text = DropDownList29.Text.Substring(0, DropDownList29.Text.IndexOf(",") - 1).Trim
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct (STR(ITEM_SLNO) + ' , ' + ITEM_VOCAB) AS ITEM_SLNO from SO_MAT_ORDER where item_status='PENDING' and (ORD_AU ='Activity' OR ORD_AU ='Service/Mt') and SO_NO='" & DropDownList29.Text.Substring(0, DropDownList29.Text.IndexOf(",") - 1).Trim & "'", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList30.Items.Clear()
        DropDownList30.DataSource = dt
        DropDownList30.DataValueField = "ITEM_SLNO"
        DropDownList30.DataBind()
        DropDownList30.Items.Insert(0, "Select")
        DropDownList30.SelectedValue = "Select"
        Panel9.Visible = False
        Panel10.Visible = True
        Dim dt7 As New DataTable()
        dt7.Columns.AddRange(New DataColumn(7) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("ITEM_UNIT_RATE"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
        ViewState("SERVICE") = dt7
        BINDGRID_SER()

        Dim FINANCE_ARRANGE As String = ""
        conn.Open()
        mc1.CommandText = "select  ORDER_DETAILS . PAYMENT_MODE  FROM  ORDER_DETAILS   WHERE SO_NO ='" & DropDownList29.Text.Substring(0, DropDownList29.Text.IndexOf(",") - 1).Trim & "'"
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
            da = New SqlDataAdapter("SELECT VOUCHER_NO FROM SALE_RCD_VOUCHAR WHERE SO_NO ='" & DropDownList29.Text.Substring(0, DropDownList29.Text.IndexOf(",") - 1).Trim & "' AND VOUCHER_STATUS='PENDING'", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.Items.Clear()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "VOUCHER_NO"
            DropDownList1.DataBind()
            DropDownList1.Items.Add("Select")
            DropDownList1.SelectedValue = "Select"
        Else
            DropDownList1.Items.Clear()
            DropDownList1.Items.Add("N/A")
            DropDownList1.SelectedValue = "N/A"
        End If
    End Sub



    Protected Sub DropDownList30_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList30.SelectedIndexChanged
        Dim working_date As Date

        working_date = Today.Date
        If DropDownList30.SelectedValue = "Select" Then
            DropDownList30.Focus()
            Return
        End If
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select distinct ITEM_VOCAB from SO_MAT_ORDER where ITEM_SLNO='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "' and SO_NO='" & TextBox179.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox183.Text = dr.Item("ITEM_VOCAB")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        ''SELECT AMD NO AND DATE
        conn.Open()
        Dim mc3 As New SqlCommand
        mc3.CommandText = "SELECT MAX(AMD_NO) AS AMD_NO , MAX(AMD_DATE) AS AMD_DATE FROM SO_MAT_ORDER WHERE AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "' AND SO_NO='" & TextBox179.Text & "' AND ITEM_SLNO=" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim
        mc3.Connection = conn
        dr = mc3.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox184.Text = dr.Item("AMD_NO")
            TextBox185.Text = dr.Item("AMD_DATE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select DISTINCT ITEM_CODE  AS ITEM_CODE from SO_MAT_ORDER where item_status='PENDING' and SO_MAT_ORDER.SO_NO='" & TextBox179.Text & "' and SO_MAT_ORDER.ITEM_SLNO='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "' ", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList31.Items.Clear()
        DropDownList31.DataSource = dt
        DropDownList31.DataValueField = "ITEM_CODE"
        DropDownList31.DataBind()
        DropDownList31.Items.Insert(0, "Select")
        DropDownList31.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList31_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList31.SelectedIndexChanged
        If DropDownList31.SelectedValue = "Select" Then
            DropDownList31.Focus()
            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(4) {New DataColumn("ITEM_CODE"), New DataColumn("ITEM_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY"), New DataColumn("ITEM_BAL_QTY")})
            ViewState("form_view1") = dt2
            BINDGRID3()
            Return
        End If
        ''SEARCH MATERIAL
        Dim MAT_TYPE As String = ""
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select PO_TYPE from ORDER_DETAILS where SO_NO='" & TextBox179.Text & "'"
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

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE ,MAX(F_ITEM .ITEM_NAME) AS ITEM_NAME,MAX(F_ITEM .ITEM_AU) AS ITEM_AU,MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT,SUM(SO_MAT_ORDER .ITEM_QTY) AS ITEM_QTY,(SUM(SO_MAT_ORDER.ITEM_QTY)-SUM(SO_MAT_ORDER.ITEM_QTY_SEND)) AS ITEM_BAL_QTY,MAX(F_ITEM .ITEM_B_STOCK) AS ITEM_B_STOCK,convert(decimal(10,3) ,((MAX(F_ITEM .ITEM_B_STOCK)*MAX(SO_MAT_ORDER.ITEM_WEIGHT)) / 1000)) AS ITEM_B_STOCK_MT FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE = F_ITEM .ITEM_CODE where SO_MAT_ORDER .ITEM_CODE='" & DropDownList31.Text & "' AND SO_MAT_ORDER.SO_NO='" & TextBox179.Text & "' AND SO_MAT_ORDER.ITEM_SLNO='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "'", conn)
        da.Fill(dt)
        conn.Close()
        ViewState("form_view1") = dt
        BINDGRID3()
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ITEM_AU,ITEM_NAME from F_ITEM where ITEM_CODE='" & DropDownList31.Text & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label486.Text = dr.Item("ITEM_AU")
            Label493.Text = dr.Item("ITEM_NAME")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        FormView3.Visible = True
    End Sub
    Protected Sub BINDGRID3()
        FormView3.DataSource = DirectCast(ViewState("form_view1"), DataTable)
        FormView3.DataBind()
    End Sub



    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        Dim working_date As Date

        working_date = Today.Date
        If DropDownList30.SelectedValue = "Select" Then
            DropDownList30.Focus()
            Return
        ElseIf DropDownList31.SelectedValue = "Select" Then
            DropDownList31.Focus()
            Return
        ElseIf IsNumeric(TextBox186.Text) = False Or TextBox186.Text = "" Then
            TextBox186.Text = ""
            TextBox186.Focus()
            Return


        End If
        Dim COUNTER As Integer = 0
        For COUNTER = 0 To GridView5.Rows.Count - 1
            If DropDownList31.Text = GridView5.Rows(COUNTER).Cells(1).Text Then
                DropDownList31.Focus()
                Return
            End If
        Next
        ''SEARCH MATERIAL
        Dim PARTY_TYPE As String = ""
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select PO_TYPE,ORDER_TO from ORDER_DETAILS where SO_NO='" & TextBox179.Text & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            PARTY_TYPE = dr.Item("ORDER_TO")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        'calculation
        Dim unit_price, qty_send, ord_qty As Decimal
        unit_price = 0.0
        qty_send = 0.0
        ord_qty = 0.0
        conn.Open()
        mc.CommandText = "select sum(ITEM_UNIT_RATE) as ITEM_UNIT_RATE,sum(ITEM_QTY ) as ITEM_QTY ,sum(ITEM_QTY_SEND) as ITEM_QTY_SEND from SO_MAT_ORDER where SO_NO ='" & TextBox179.Text & "' and ITEM_CODE ='" & DropDownList31.SelectedValue & "' and ITEM_SLNO ='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            unit_price = dr.Item("ITEM_UNIT_RATE")
            qty_send = dr.Item("ITEM_QTY_SEND")
            ord_qty = dr.Item("ITEM_QTY")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If (ord_qty - qty_send) < CDec(TextBox186.Text) Then
            TextBox186.Focus()
            Return
        Else
        End If

        Dim ass_value As Decimal = 0.0
        ass_value = FormatNumber(unit_price * CDec(TextBox186.Text), 2)


        Dim row_count As Integer = GridView5.Rows.Count + 1
        Dim dt As DataTable = DirectCast(ViewState("SERVICE"), DataTable)
        dt.Rows.Add(row_count, DropDownList31.Text, Label493.Text, Label486.Text, TextBox186.Text, unit_price, TextBox187.Text, ass_value)
        ViewState("SERVICE") = dt
        BINDGRID_SER()
        Dim ass_total, cas4_price, item_qty, unit_rate, discount, IGST, CGST, SGST, IGST_AMOUNT, CGST_AMOUNT, SGST_AMOUNT, CESS, CESS_AMOUNT, pack_forwd, item_tcs, terminal_tax, freight_rate As New Decimal(0)

        For COUNTER = 0 To GridView5.Rows.Count - 1
            ass_total = ass_total + GridView5.Rows(COUNTER).Cells(7).Text
        Next
        'select service_provider ,service_receiver  from s_tax_liability where taxable_service ='Activity' and w_e_f =(select max(w_e_f)  from s_tax_liability where taxable_service ='Activity')

        conn.Open()
        mc.CommandText = "select SUM(ITEM_CGST) AS ITEM_CGST,SUM(ITEM_SGST) AS ITEM_SGST,SUM(ITEM_IGST) AS ITEM_IGST, SUM(ITEM_CESS) AS ITEM_CESS,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU 
                            FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE JOIN CHPTR_HEADING on F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "' and SO_MAT_ORDER .SO_NO='" & TextBox179.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList31.SelectedValue & "' AND AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "'"
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            CGST = dr.Item("ITEM_CGST")
            SGST = dr.Item("ITEM_SGST")
            IGST = dr.Item("ITEM_IGST")
            CESS = dr.Item("ITEM_CESS")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        'calculation service tax
        CGST_AMOUNT = FormatNumber((ass_total * CGST) / 100, 0)
        SGST_AMOUNT = FormatNumber((ass_total * SGST) / 100, 0)
        IGST_AMOUNT = FormatNumber((ass_total * IGST) / 100, 0)
        CESS_AMOUNT = FormatNumber((ass_total * CESS) / 100, 0)
        TextBox1.Text = FormatNumber(ass_total, 2)
        TextBox188.Text = CGST_AMOUNT
        TextBox190.Text = SGST_AMOUNT
        TextBox193.Text = IGST_AMOUNT
        TextBox194.Text = CESS_AMOUNT

        ''TCS VALUE
        TextBox66.Text = FormatNumber((((ass_total + CGST_AMOUNT + SGST_AMOUNT + IGST_AMOUNT + CESS_AMOUNT) * item_tcs) / 100), 2)

        TextBox195.Text = ass_total + CGST_AMOUNT + SGST_AMOUNT + IGST_AMOUNT + CESS_AMOUNT + CDec(TextBox66.Text)
        ''roundoff
        'TextBox191.Text = CInt(ass_total + sp_total + sb_total + KK_TOTAL) - (ass_total + sp_total + sb_total + KK_TOTAL)
        ''total
        'TextBox192.Text = FormatNumber(CInt(ass_total + sp_total + sb_total + KK_TOTAL), 2)


    End Sub
    Protected Sub BINDGRID_SER()
        GridView5.DataSource = DirectCast(ViewState("SERVICE"), DataTable)
        GridView5.DataBind()
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

    Protected Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                Dim working_date As Date
                working_date = Today.Date

                If GridView5.Rows.Count = 0 Then
                    Return
                End If

                Dim gst_code, my_gst_code, COMM, DIVISION As New String("")
                conn.Open()
                mycommand.CommandText = "select dater.d_code,dater.d_name,dater.gst_code from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & TextBox179.Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    gst_code = dr.Item("gst_code")
                    dr.Close()
                End If
                conn.Close()

                Dim ORDER_TO, FINANCE_ARRANGE, PARTY_CODE, CONSIGN_CODE, chptr_heading, PLACE_OF_SUPPLY, qual_desc As New String("")
                Dim so_date, SO_ACTUAL_DATE As New Date
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.DESTINATION,ORDER_DETAILS.CONSIGN_CODE,ORDER_DETAILS.SO_ACTUAL_DATE, ORDER_DETAILS . PAYMENT_MODE , ORDER_DETAILS.SO_DATE,ORDER_DETAILS.PARTY_CODE,qual_group.qual_name,qual_group.qual_desc , ORDER_DETAILS.PAYMENT_TERM,ORDER_DETAILS.MODE_OF_DESPATCH,F_ITEM .ITEM_CHPTR,CHPTR_HEADING.CHPT_NAME FROM SO_MAT_ORDER JOIN ORDER_DETAILS ON SO_MAT_ORDER .SO_NO =ORDER_DETAILS .SO_NO JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE JOIN CHPTR_HEADING ON F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE join qual_group on F_ITEM .ITEM_TYPE =qual_group.qual_code  WHERE SO_MAT_ORDER.ITEM_SLNO  ='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "' AND SO_MAT_ORDER .SO_NO ='" & TextBox179.Text.Trim & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORDER_TO = dr.Item("ORDER_TO")
                    chptr_heading = dr.Item("ITEM_CHPTR")
                    so_date = dr.Item("SO_DATE")
                    FINANCE_ARRANGE = dr.Item("PAYMENT_MODE")
                    PARTY_CODE = dr.Item("PARTY_CODE")
                    CONSIGN_CODE = dr.Item("CONSIGN_CODE")
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

                Dim cas4_price, item_qty, unit_rate, discount, IGST, CGST, SGST, item_cess, pack_forwd, item_tcs, terminal_tax, qty_send, freight_rate As Decimal
                Dim freight_type, pack_type, actual_so, DISC_TYPE As New String("")
                conn.Open()
                Dim mc As New SqlCommand
                mc.CommandText = "select MAX(SO_MAT_ORDER.ITEM_WEIGHT) AS ITEM_WEIGHT,SUM(ITEM_CGST) AS ITEM_CGST,SUM(ITEM_SGST) AS ITEM_SGST,SUM(ITEM_IGST) AS ITEM_IGST, SUM(ITEM_CESS) AS ITEM_CESS,MAX(SO_MAT_ORDER.DISC_TYPE) AS DISC_TYPE,MAX(SO_MAT_ORDER.PACK_TYPE) AS PACK_TYPE ,MAX(SO_MAT_ORDER.ORD_AU) AS ORD_AU, SUM(SO_MAT_ORDER.ITEM_QTY) AS ITEM_QTY, SUM(SO_MAT_ORDER.ITEM_UNIT_RATE) AS ITEM_UNIT_RATE ,SUM(SO_MAT_ORDER .ITEM_DISCOUNT) AS ITEM_DISCOUNT,SUM(SO_MAT_ORDER .ITEM_PACK) AS ITEM_PACK ,SUM(SO_MAT_ORDER .ITEM_QTY_SEND) AS ITEM_QTY_SEND ,SUM(SO_MAT_ORDER .ITEM_TCS) AS ITEM_TCS ,SUM(SO_MAT_ORDER .ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX , MAX(SO_MAT_ORDER .ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE ,SUM(SO_MAT_ORDER .ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU , " &
                            " (select CAS_4 .COST_VALUE  from F_ITEM join CAS_4 on F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where ITEM_CODE ='" & DropDownList31.SelectedValue & "' and CAS_4 .EFECTIVE_DATE =(select max(CAS_4 .EFECTIVE_DATE)  from CAS_4 join F_ITEM on F_ITEM . ITEM_TYPE =CAS_4.MAT_GROUP where F_ITEM .ITEM_CODE ='" & DropDownList31.SelectedValue & "' and CAS_4 .EFECTIVE_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "')) as COST_VALUE,  " &
                            " (select SO_ACTUAL  from ORDER_DETAILS where SO_NO ='" & TextBox179.Text & "') as actual_so ,   " &
                            "  MAX(CHPTR_HEADING.TAX_VALUE) AS TAX_VALUE ,MAX(CHPTR_HEADING.ED_SESS) AS ED_SESS ,MAX(CHPTR_HEADING.SHED_CESS) AS SHED_CESS FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE JOIN CHPTR_HEADING on F_ITEM .ITEM_CHPTR =CHPTR_HEADING .CHPT_CODE where SO_MAT_ORDER .ITEM_SLNO='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "' and SO_MAT_ORDER .SO_NO='" & TextBox179.Text & "' and SO_MAT_ORDER .ITEM_CODE='" & DropDownList31.SelectedValue & "' AND AMD_DATE <='" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "'"
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


                Dim BILL_PARTY_ADD, SHIP_PARTY_ADD, BILL_PARTY_state_code, BILL_PARTY_state, SHIP_PARTY_state_code, SHIP_PARTY_state, BILL_PARTY_GST, BILL_PARTY_CODE, SHIP_PARTY_GST, SHIP_PARTY_CODE As New String("")
                'BILL TO PARTY ADDRESS SEARCH
                conn.Open()
                mc1.CommandText = "select d_name + ' , ' + add_1 + ' , ' + add_2 + ' , ' + ecc_no + ' , ' + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select PARTY_CODE  from ORDER_DETAILS where SO_NO ='" & TextBox179.Text & "')"
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
                mc1.CommandText = "select d_name + ' , ' + add_1 + ' , ' + add_2 + ' , ' + ecc_no + ' , ' + tin_no as party_details, d_state ,d_state_code ,gst_code,d_code from dater where d_code =(select CONSIGN_CODE  from ORDER_DETAILS where SO_NO ='" & TextBox179.Text & "')"
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

                Dim ass_price As Decimal = 0.00
                For COUNTER = 0 To GridView5.Rows.Count - 1
                    ass_price = ass_price + GridView5.Rows(COUNTER).Cells(7).Text
                Next

                '' calculation

                Dim TAXABLE_VALUE As Decimal = 0
                TAXABLE_VALUE = CDec(TextBox1.Text)
                Dim ADV_AMT, ADV_GST, NET_PAY As New Decimal(0.0)
                If FINANCE_ARRANGE = "ADVANCE" Then
                    ADV_AMT = ass_price + CDec(TextBox66.Text)
                    ADV_GST = CDec(TextBox188.Text) + CDec(TextBox190.Text) + CDec(TextBox193.Text) + CDec(TextBox194.Text)
                    NET_PAY = 0.0
                Else
                    ADV_AMT = 0.0
                    ADV_GST = 0.0
                    NET_PAY = CDec(TextBox195.Text)
                End If


                Dim QUARY1 As String = ""
                QUARY1 = "Insert Into DESPATCH(TRANSPORTER_WEIGHT,QUALITY,INV_STATUS,SO_NO ,SO_DATE ,PO_NO ,PO_DATE ,AMD_NO ,AMD_DATE ,TRANS_WO ,TRANS_SLNO ,TRANS_NAME ,TRUCK_NO ,PARTY_CODE ,CONSIGN_CODE ,MAT_VOCAB ,MAT_SLNO ,INV_NO,INV_DATE,D_TYPE,CHPTR_HEAD ,FISCAL_YEAR,INV_ISSUE ,PLACE_OF_SUPPLY ,BILL_PARTY_ADD ,CON_PARTY_ADD ,B_STATE ,B_STATE_CODE ,C_STATE ,C_STATE_CODE ,BILL_PARTY_GST_N ,CON_PARTY_GST_N ,NEGOTIATING_BRANCH ,PAYING_AUTH ,RR_NO ,RR_DATE ,TOTAL_WEIGHT ,ACC_UNIT ,PURITY ,TC_NO ,FINANCE_ARRENGE ,MILL_CODE ,DA_NO ,CONTRACT_NO ,RCD_VOUCHER_NO ,RCD_VOUCHER_DATE ,ROUT_CARD_NO ,DESPATCH_TYPE ,TAX_REVERS_CHARGE ,RLY_INV_NO ,RLY_INV_DATE ,FRT_WT_AMT ,TOTAL_PCS,P_CODE ,P_DESC ,D1 ,D2 ,D3 ,D4 ,BASE_PRICE ,PACK_PRICE ,PACK_TYPE ,QLTY_PRICE ,SEC_PRICE ,TOTAL_TDC ,UNIT_PRICE ,SY_MARGIN ,PPM_FRT ,FRT_TYPE ,RLY_ROAD_FRT ,TOTAL_RATE_UNIT ,REBATE_UNIT ,REBATE_TYPE ,TAXABLE_RATE_UNIT ,TOTAL_QTY ,TAXABLE_VALUE ,CGST_RATE ,CGST_AMT ,SGST_RATE ,SGST_AMT ,IGST_RATE ,IGST_AMT ,CESS_RATE ,CESS_AMT ,TERM_RATE ,TERM_AMT ,TOTAL_AMT ,LESS_LOAD_AMT ,TOTAL_BAG ,ADVANCE_PAID ,GST_PAID_ADV ,NET_PAY ,NOTIFICATION_TEXT ,COMM ,DIV_ADD ,INV_TYPE ,INV_RULE ,FORM_NAME,EMP_ID, TCS_TAX, TCS_AMT)values(@TRANSPORTER_WEIGHT,@QUALITY,@INV_STATUS,@SO_NO ,@SO_DATE ,@PO_NO ,@PO_DATE ,@AMD_NO ,@AMD_DATE ,@TRANS_WO ,@TRANS_SLNO ,@TRANS_NAME ,@TRUCK_NO ,@PARTY_CODE ,@CONSIGN_CODE ,@MAT_VOCAB ,@MAT_SLNO ,@INV_NO,@INV_DATE,@D_TYPE,@CHPTR_HEAD ,@FISCAL_YEAR,@INV_ISSUE ,@PLACE_OF_SUPPLY ,@BILL_PARTY_ADD ,@CON_PARTY_ADD ,@B_STATE ,@B_STATE_CODE ,@C_STATE ,@C_STATE_CODE ,@BILL_PARTY_GST_N ,@CON_PARTY_GST_N ,@NEGOTIATING_BRANCH ,@PAYING_AUTH ,@RR_NO ,@RR_DATE ,@TOTAL_WEIGHT ,@ACC_UNIT ,@PURITY ,@TC_NO ,@FINANCE_ARRENGE ,@MILL_CODE ,@DA_NO ,@CONTRACT_NO ,@RCD_VOUCHER_NO ,@RCD_VOUCHER_DATE ,@ROUT_CARD_NO ,@DESPATCH_TYPE ,@TAX_REVERS_CHARGE ,@RLY_INV_NO ,@RLY_INV_DATE ,@FRT_WT_AMT ,@TOTAL_PCS,@P_CODE ,@P_DESC ,@D1 ,@D2 ,@D3 ,@D4 ,@BASE_PRICE ,@PACK_PRICE ,@PACK_TYPE ,@QLTY_PRICE ,@SEC_PRICE ,@TOTAL_TDC ,@UNIT_PRICE ,@SY_MARGIN ,@PPM_FRT ,@FRT_TYPE ,@RLY_ROAD_FRT ,@TOTAL_RATE_UNIT ,@REBATE_UNIT ,@REBATE_TYPE ,@TAXABLE_RATE_UNIT ,@TOTAL_QTY ,@TAXABLE_VALUE ,@CGST_RATE ,@CGST_AMT ,@SGST_RATE ,@SGST_AMT ,@IGST_RATE ,@IGST_AMT ,@CESS_RATE ,@CESS_AMT ,@TERM_RATE ,@TERM_AMT ,@TOTAL_AMT ,@LESS_LOAD_AMT ,@TOTAL_BAG ,@ADVANCE_PAID ,@GST_PAID_ADV ,@NET_PAY ,@NOTIFICATION_TEXT ,@COMM ,@DIV_ADD ,@INV_TYPE ,@INV_RULE ,@FORM_NAME,@EMP_ID, @TCS_TAX, @TCS_AMT)"
                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@SO_NO", TextBox179.Text)
                cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@PO_NO", actual_so)
                cmd1.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(SO_ACTUAL_DATE.Day & "-" & SO_ACTUAL_DATE.Month & "-" & SO_ACTUAL_DATE.Year), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@AMD_NO", TextBox184.Text)
                cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(TextBox185.Text, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@TRANS_WO", "N/A")
                cmd1.Parameters.AddWithValue("@TRANS_SLNO", "N/A")
                cmd1.Parameters.AddWithValue("@TRANS_NAME", "N/A")
                cmd1.Parameters.AddWithValue("@TRUCK_NO", "N/A")
                cmd1.Parameters.AddWithValue("@PARTY_CODE", PARTY_CODE)
                cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CONSIGN_CODE)
                cmd1.Parameters.AddWithValue("@MAT_VOCAB", TextBox183.Text)
                cmd1.Parameters.AddWithValue("@MAT_SLNO", DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim)
                cmd1.Parameters.AddWithValue("@D_TYPE", inv_for)
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
                cmd1.Parameters.AddWithValue("@RR_NO", "")
                cmd1.Parameters.AddWithValue("@RR_DATE", "")
                cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT ", CDec(TextBox186.Text))
                cmd1.Parameters.AddWithValue("@ACC_UNIT", "Activity")
                cmd1.Parameters.AddWithValue("@PURITY", 0.00)
                cmd1.Parameters.AddWithValue("@TC_NO", "")
                cmd1.Parameters.AddWithValue("@FINANCE_ARRENGE", FINANCE_ARRANGE)
                cmd1.Parameters.AddWithValue("@MILL_CODE", "")
                cmd1.Parameters.AddWithValue("@DA_NO", "")
                cmd1.Parameters.AddWithValue("@CONTRACT_NO", "")
                cmd1.Parameters.AddWithValue("@RCD_VOUCHER_NO", "")
                cmd1.Parameters.AddWithValue("@RCD_VOUCHER_DATE", "")
                cmd1.Parameters.AddWithValue("@ROUT_CARD_NO", "")
                cmd1.Parameters.AddWithValue("@DESPATCH_TYPE", "")
                cmd1.Parameters.AddWithValue("@TAX_REVERS_CHARGE", "NO")
                cmd1.Parameters.AddWithValue("@RLY_INV_NO", "")
                cmd1.Parameters.AddWithValue("@RLY_INV_DATE", "")
                cmd1.Parameters.AddWithValue("@FRT_WT_AMT", 0.00)
                cmd1.Parameters.AddWithValue("@TOTAL_PCS", 0)
                cmd1.Parameters.AddWithValue("@P_CODE ", DropDownList31.SelectedValue)
                cmd1.Parameters.AddWithValue("@P_DESC", Label493.Text)
                cmd1.Parameters.AddWithValue("@D1", "")
                cmd1.Parameters.AddWithValue("@D2", "")
                cmd1.Parameters.AddWithValue("@D3", "")
                cmd1.Parameters.AddWithValue("@D4", "")
                cmd1.Parameters.AddWithValue("@PACK_TYPE", pack_type)
                cmd1.Parameters.AddWithValue("@FRT_TYPE", freight_type)
                cmd1.Parameters.AddWithValue("@REBATE_TYPE ", DISC_TYPE)
                cmd1.Parameters.AddWithValue("@TOTAL_QTY", CDec(TextBox186.Text))
                cmd1.Parameters.AddWithValue("@BASE_PRICE", unit_rate)
                cmd1.Parameters.AddWithValue("@PACK_PRICE", pack_forwd)
                cmd1.Parameters.AddWithValue("@QLTY_PRICE", 0.0)
                cmd1.Parameters.AddWithValue("@SEC_PRICE", 0.0)
                cmd1.Parameters.AddWithValue("@TOTAL_TDC", 0.0)
                cmd1.Parameters.AddWithValue("@UNIT_PRICE", unit_rate)
                cmd1.Parameters.AddWithValue("@SY_MARGIN", 0.0)
                cmd1.Parameters.AddWithValue("@PPM_FRT", 0.0)
                cmd1.Parameters.AddWithValue("@RLY_ROAD_FRT", freight_rate)
                cmd1.Parameters.AddWithValue("@TOTAL_RATE_UNIT", unit_rate)
                cmd1.Parameters.AddWithValue("@REBATE_UNIT", discount)
                cmd1.Parameters.AddWithValue("@TAXABLE_RATE_UNIT", unit_rate)
                cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", ass_price)
                cmd1.Parameters.AddWithValue("@CGST_RATE", CGST)
                cmd1.Parameters.AddWithValue("@CGST_AMT", CDec(TextBox188.Text))
                cmd1.Parameters.AddWithValue("@SGST_RATE", SGST)
                cmd1.Parameters.AddWithValue("@SGST_AMT", CDec(TextBox190.Text))
                cmd1.Parameters.AddWithValue("@IGST_RATE", IGST)
                cmd1.Parameters.AddWithValue("@IGST_AMT", CDec(TextBox193.Text))
                cmd1.Parameters.AddWithValue("@CESS_RATE", item_cess)
                cmd1.Parameters.AddWithValue("@CESS_AMT", CDec(TextBox194.Text))
                cmd1.Parameters.AddWithValue("@TERM_RATE", terminal_tax)
                cmd1.Parameters.AddWithValue("@TERM_AMT", 0.00)
                cmd1.Parameters.AddWithValue("@TOTAL_AMT", CDec(TextBox195.Text))
                cmd1.Parameters.AddWithValue("@LESS_LOAD_AMT", 0.0)
                cmd1.Parameters.AddWithValue("@ADVANCE_PAID", ADV_AMT)
                cmd1.Parameters.AddWithValue("@GST_PAID_ADV", ADV_GST)
                cmd1.Parameters.AddWithValue("@NET_PAY", CDec(TextBox195.Text))
                cmd1.Parameters.AddWithValue("@COMM", COMM)
                cmd1.Parameters.AddWithValue("@DIV_ADD", DIVISION)
                cmd1.Parameters.AddWithValue("@INV_TYPE", inv_type)
                cmd1.Parameters.AddWithValue("@INV_RULE", inv_rule)
                cmd1.Parameters.AddWithValue("@FORM_NAME", "")
                cmd1.Parameters.AddWithValue("@TOTAL_BAG", 0)
                cmd1.Parameters.AddWithValue("@NOTIFICATION_TEXT", TextBox182.Text)
                cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                cmd1.Parameters.AddWithValue("@INV_STATUS", "ACTIVE")
                cmd1.Parameters.AddWithValue("@TCS_TAX", 0.00)
                cmd1.Parameters.AddWithValue("@TCS_AMT", 0.00)
                cmd1.Parameters.AddWithValue("@QUALITY", qual_desc)
                cmd1.Parameters.AddWithValue("@TRANSPORTER_WEIGHT", 0.00)
                cmd1.ExecuteReader()
                cmd1.Dispose()
                'conn.Close()

                ''update sale order update f_item
                Dim lp As Integer = 0
                For lp = 0 To GridView5.Rows.Count - 1
                    Dim sendqty As Decimal = CDec(GridView5.Rows(lp).Cells(4).Text)


                    'Update F_item
                    QUARY1 = "update F_ITEM set ITEM_F_STOCK =ITEM_F_STOCK - " & sendqty & " ,ITEM_LAST_DESPATCH = @ITEM_LAST_DESPATCH where ITEM_CODE ='" & GridView5.Rows(lp).Cells(1).Text & "'"
                    Dim cmd2 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmd2.Parameters.AddWithValue("@ITEM_LAST_DESPATCH", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                    cmd2.ExecuteReader()
                    cmd2.Dispose()


                    ''update so order
                    QUARY1 = "update SO_MAT_ORDER set ITEM_QTY_SEND =ITEM_QTY_SEND + " & sendqty & " where SO_NO ='" & TextBox179.Text & "' and ITEM_SLNO ='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "' and ITEM_CODE ='" & GridView5.Rows(lp).Cells(1).Text & "' AND AMD_NO='" & TextBox184.Text & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()

                Next

                ''search ac head
                Dim IUCA As String = ""
                Dim CGST_HEAD As String = ""
                Dim IGST_HEAD As String = ""
                Dim TCS As String = ""
                Dim JOB_WORK As String = ""
                Dim CESS_HEAD As String = ""
                Dim SGST_HEAD As String = ""

                Dim lcgst, lsgst, ligst, lcess, adv_pay_head, gst_exp As New String("")
                conn.Open()
                Dim MC5 As New SqlCommand
                MC5.CommandText = "select work_group.TCS_OUTGOING,work_group.gst_expenditure,ORDER_DETAILS .SO_NO ,ORDER_DETAILS .ORDER_TO,dater .JOB_WORK , dater.stock_ac_head,dater.iuca_head,work_group.adv_pay,work_group.lcgst,work_group .lsgst,work_group .ligst ,work_group .lcess,work_group.cgst,work_group.sgst,work_group.igst,work_group.cess, work_group.ed_head,work_group.vat_head,work_group.cst_head,work_group.freight_head,work_group.term_tax,work_group.tds_head,work_group.pack_head from ORDER_DETAILS join DATER on ORDER_DETAILS .PARTY_CODE=DATER.d_code JOIN work_group on ORDER_DETAILS.ORDER_TYPE =work_group.work_name and  ORDER_DETAILS.PO_TYPE  =work_group.work_type and ORDER_DETAILS.ORDER_TO  =work_group.d_type WHERE ORDER_DETAILS.SO_NO='" & TextBox179.Text & "'"
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORDER_TO = dr.Item("ORDER_TO")
                    JOB_WORK = dr.Item("JOB_WORK")
                    IUCA = dr.Item("iuca_head")
                    CGST_HEAD = dr.Item("CGST")
                    IGST_HEAD = dr.Item("IGST")
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
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, JOB_WORK, "Cr", CDec(ass_price), "JOB_WORK")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, IUCA, "Dr", CDec(TextBox195.Text), "IUCA")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lcgst, "Cr", CDec(TextBox188.Text), "CGST_PAYABLE")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lsgst, "Cr", CDec(TextBox190.Text), "SGST_PAYABLE")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, ligst, "Cr", CDec(TextBox193.Text), "IGST_PAYABLE")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lcess, "Cr", CDec(TextBox194.Text), "CESS_PAYABLE")

                    End If

                ElseIf ORDER_TO = "Other" Then
                    If FINANCE_ARRANGE = "ADVANCE" Then

                        save_ledger(DropDownList1.SelectedValue, TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, JOB_WORK, "Cr", CDec(ass_price), "JOB_WORK")
                        save_ledger(DropDownList1.SelectedValue, TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, adv_pay_head, "Dr", CDec(TextBox195.Text), "ADV_PAY")
                        save_ledger(DropDownList1.SelectedValue, TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lcgst, "Cr", CDec(TextBox188.Text), "CGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lsgst, "Cr", CDec(TextBox190.Text), "SGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, ligst, "Cr", CDec(TextBox193.Text), "IGST_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lcess, "Cr", CDec(TextBox194.Text), "CESS_PAYABLE")
                        save_ledger(DropDownList1.SelectedValue, TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, TCS, "Cr", CDec(TextBox66.Text), "TCS_OUTPUT")

                    ElseIf FINANCE_ARRANGE = "BG" Then

                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, JOB_WORK, "Cr", CDec(ass_price), "JOB_WORK")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, "62203", "Dr", CDec(TextBox195.Text), "SUND_DEBTOR_OTHER")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lcgst, "Cr", CDec(TextBox188.Text), "CGST_PAYABLE")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lsgst, "Cr", CDec(TextBox190.Text), "SGST_PAYABLE")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, ligst, "Cr", CDec(TextBox193.Text), "IGST_PAYABLE")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, lcess, "Cr", CDec(TextBox194.Text), "CESS_PAYABLE")
                        save_ledger("", TextBox179.Text, inv_for & TextBox65.Text, TextBox180.Text, TCS, "Cr", CDec(TextBox66.Text), "TCS_OUTPUT")

                    End If

                End If

                'UPDATE SALE_RCD_VOUCHER
                If DropDownList1.SelectedValue <> "N/A" Then

                    QUARY1 = ""
                    QUARY1 = "UPDATE SALE_RCD_VOUCHAR SET BAL_AMT =BAL_AMT- " & CDec(TextBox195.Text) & ",CGST_BAL =CGST_BAL - " & CDec(TextBox188.Text) & ",SGST_BAL =SGST_BAL- " & CDec(TextBox190.Text) & ",IGST_BAL =IGST_BAL- " & CDec(TextBox193.Text) & ",CESS_BAL =CESS_BAL- " & CDec(TextBox194.Text) & " WHERE VOUCHER_TYPE + VOUCHER_NO ='" & DropDownList1.Text & "' AND SO_NO ='" & TextBox179.Text & "' AND ITEM_SLNO ='" & DropDownList30.Text.Substring(0, DropDownList30.Text.IndexOf(",") - 1).Trim & "'"
                    Dim cmdd As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmdd.ExecuteReader()
                    cmdd.Dispose()

                End If



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


                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(7) {New DataColumn("mat_sl_no"), New DataColumn("ITEM_CODE"), New DataColumn("MAT_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_QTY_PCS"), New DataColumn("ITEM_UNIT_RATE"), New DataColumn("Packing Details"), New DataColumn("ASS_VALUE")})
                ViewState("SERVICE") = dt
                BINDGRID_SER()
                TextBox1.Text = "0.00"
                TextBox188.Text = "0.00"
                TextBox190.Text = "0.00"
                TextBox193.Text = "0.00"
                TextBox194.Text = "0.00"
                TextBox66.Text = "0.00"
                TextBox195.Text = "0.00"

                'myTrans.Commit()
                'Label308.Text = "All records are written to database."

                Dim tcsFlag As String
                If (CDec(TextBox66.Text) > 0) Then
                    tcsFlag = "Y"
                Else
                    tcsFlag = "N"
                End If

                ''===========================Generate E-Invoice Start=======================''

                'If gst_code = my_gst_code Then
                '    'e-invoice is not required
                'Else
                '    Dim logicclassobj = New EinvoiceLogicClass
                '    Dim autherrordata As List(Of AuthenticationErrorDetailsClass) = logicclassobj.EinvoiceAuthentication(TextBox177.Text + TextBox65.Text, TextBox180.Text)
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

                If gst_code = my_gst_code Then
                    ''E-Invoice is not required
                Else
                    Dim logicClassObj = New EinvoiceLogicClassEY
                    Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox177.Text + TextBox65.Text, TextBox180.Text)
                    If (AuthErrorData.Item(0).status = "1") Then
                        Dim authIdToken As String = AuthErrorData.Item(0).Idtoken
                        Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox177.Text + TextBox65.Text, TextBox180.Text, TextBox180.Text, "YES", tcsFlag, "INV")


                        If (EinvErrorData.Item(0).status = "1") Then
                            TextBox6.Text = EinvErrorData.Item(0).IRN
                            '================SENDING DATA TO EY PORTAL START==================='

                            Dim result
                            If (BILL_PARTY_GST = "") Then
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox180.Text, TextBox180.Text, "YES", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            Else
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox180.Text, TextBox180.Text, "YES", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            End If

                            '================SENDING DATA TO EY PORTAL END==================='
                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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

                            '================SENDING DATA TO EY PORTAL START==================='

                            Dim result
                            If (BILL_PARTY_GST = "") Then
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox180.Text, TextBox180.Text, "YES", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            Else
                                result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox177.Text + TextBox65.Text, TextBox180.Text, TextBox180.Text, "YES", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                            End If

                            '================SENDING DATA TO EY PORTAL END==================='

                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage
                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox177.Text + TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            goAheadFlag = True

                            'Label308.Text = "There is error in E-way bill generation, please generate E-way bill alone with above IRN."
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
                    myTrans.Commit()
                    Label308.Text = "All records are written to database."
                    Label31.Visible = False
                    Label42.Visible = False
                    txtEinvoiceErrorCode.Visible = False
                    txtEinvoiceErrorMessage.Visible = False
                Else
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    TextBox65.Text = ""
                    TextBox177.Text = ""
                    TextBox6.Text = ""

                End If

                ''===========================Generate E-Invoice Through EY End=======================''

                '''''''''''''''''''''''''''''''''''''''''


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
End Class