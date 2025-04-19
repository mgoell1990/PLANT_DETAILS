Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class add_order
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim myTrans As SqlTransaction
    Dim SOACUNIT As String
    Protected Sub BINDGRID1()
        GridView1.DataSource = DirectCast(ViewState("mat"), DataTable)
        GridView1.DataBind()
    End Sub
    Protected Sub BINDGRIDpur()
        GridView3.DataSource = DirectCast(ViewState("purchase"), DataTable)
        GridView3.DataBind()
    End Sub
    Protected Sub BINDGRIDFGN()
        GridView216.DataSource = DirectCast(ViewState("foreign"), DataTable)
        GridView216.DataBind()
    End Sub
    Protected Sub BINDGRIDFGN_STORE_IMP()
        GridView2.DataSource = DirectCast(ViewState("foreign_store"), DataTable)
        GridView2.DataBind()
    End Sub
    Protected Sub BINDGRIDWORK()
        GridView4.DataSource = DirectCast(ViewState("WORK"), DataTable)
        GridView4.DataBind()
    End Sub

    Protected Sub BINDGRIDOUTSOURCED()
        GridView5.DataSource = DirectCast(ViewState("OutSourcedFG"), DataTable)
        GridView5.DataBind()
    End Sub

    Protected Sub BINDGRIDFGN_OUTSOURCED()
        GridView6.DataSource = DirectCast(ViewState("foreign_outsourced"), DataTable)
        GridView6.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            MultiView1.ActiveViewIndex = 0
            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(9) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Qty"), New DataColumn("Unit Weight"), New DataColumn("Mat Ord. Qty"), New DataColumn("ORD_QTY_MT"), New DataColumn("Unit Price"), New DataColumn("Mat Desc")})
            ViewState("mat") = dt1
            Me.BINDGRID1()

            Dim dt_fgn As New DataTable()
            dt_fgn.Columns.AddRange(New DataColumn(6) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DESC")})
            ViewState("foreign") = dt_fgn
            Me.BINDGRIDFGN()

            Dim dt_fgn_store As New DataTable()
            dt_fgn_store.Columns.AddRange(New DataColumn(6) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DESC")})
            ViewState("foreign_store") = dt_fgn_store
            Me.BINDGRIDFGN_STORE_IMP()

            Dim dtOutsourced As New DataTable()
            dtOutsourced.Columns.AddRange(New DataColumn(20) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("CGST"), New DataColumn("SGST"), New DataColumn("IGST"), New DataColumn("ANAL_TAX"), New DataColumn("Cess"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC"), New DataColumn("MAT_QTY_RCVD"), New DataColumn("TOTAL_WT")})
            ViewState("OutSourcedFG") = dtOutsourced
            Me.BINDGRIDOUTSOURCED()

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select SO_NO from ORDER_DETAILS where SO_STATUS='DRAFT' OR SO_STATUS='RC'  ORDER BY SO_NO", conn)
            da.Fill(dt)

            conn.Close()
            '' 





            MultiView1.ActiveViewIndex = 0
            TextBox832.Text = DirectCast(Session("val"), String)
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("purchaseAccess")) Or Session("purchaseAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

        SOACUNIT = TextBox816.Text

    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Label439.Text = ""
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, dater_code As String
        order_type = ""
        po_type = ""
        SUPL_ID = ""
        SUPL_NAME = ""
        SO_DATE = ""
        freight_term = ""
        ORDER_TO = ""
        dater_code = ""
        If TextBox832.Text = "" Then
            Label678.Visible = True
            Label678.Text = "Please enter order number first"
        Else
            Label678.Visible = False
            Label678.Text = ""
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select  ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_ACTUAL_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                order_type = dr.Item("ORDER_TYPE")
                po_type = dr.Item("PO_TYPE")
                SO_DATE = dr.Item("SO_ACTUAL_DATE")
                freight_term = dr.Item("FREIGHT_TERM")
                ORDER_TO = dr.Item("ORDER_TO")
                dr.Close()
            End If
            conn.Close()
            If order_type = "Purchase Order" Then
                ''search supplier
                If po_type = "RAW MATERIAL" Or po_type = "STORE MATERIAL" Then
                    TextBox733.Text = ""
                    Dim gst_code As String = ""
                    Dim dt_raw As New DataTable()
                    dt_raw.Columns.AddRange(New DataColumn(20) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("CGST"), New DataColumn("SGST"), New DataColumn("IGST"), New DataColumn("ANAL_TAX"), New DataColumn("Cess"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC"), New DataColumn("MAT_QTY_RCVD"), New DataColumn("TOTAL_WT")})
                    ViewState("purchase") = dt_raw
                    Me.BINDGRIDpur()
                    Label384.Text = po_type.ToUpper & " INFORMATION"

                    MultiView1.ActiveViewIndex = 1
                    conn.Open()
                    mc1.CommandText = "select supl.SUPL_GST_NO,supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox105.Text = dr.Item("supl_id")
                        TextBox106.Text = dr.Item("supl_name")
                        gst_code = dr.Item("SUPL_GST_NO")
                        dr.Close()
                    End If
                    conn.Close()
                    TextBox102.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                    TextBox103.Text = SO_DATE
                    If freight_term = "Extra" Then
                        ''Freight
                        Label356.Text = "Freight"
                        po_frightText1.ReadOnly = False
                        po_frightText1.BackColor = Drawing.Color.White
                        po_frightText1.ForeColor = Drawing.Color.Black
                    ElseIf freight_term = "Paid" Or freight_term = "Not Applicable" Then
                        Label356.Text = "Freight"
                        po_frightText1.ReadOnly = True
                        po_frightText1.Text = 0.0
                        po_frightText1.BackColor = Drawing.Color.Red
                        po_frightText1.ForeColor = Drawing.Color.White
                    End If


                    'search company profile compair gst code
                    Dim my_gst_code As String = ""
                    conn.Open()
                    mc1.CommandText = "select * from comp_profile"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        my_gst_code = dr.Item("c_gst_no")
                        dr.Close()
                    End If
                    conn.Close()
                    If gst_code = my_gst_code Then
                        TextBox833.Enabled = False
                        TextBox834.Enabled = False
                        TextBox835.Enabled = False
                        cess_textbox.Enabled = False
                        sgstPercentage.Text = "0.00"
                        cgstPercentage.Text = "0.00"
                        igstPercentage.Text = "0.00"
                    Else
                        cgstPercentage.Enabled = True
                        igstPercentage.Enabled = True
                        sgstPercentage.Enabled = True
                        cess_textbox.Enabled = True
                        sgstPercentage.Text = "0.00"
                        cgstPercentage.Text = "0.00"
                        igstPercentage.Text = "0.00"
                    End If

                    TextBox83.Text = 0.0
                    TextBox13.Text = 0.0
                    TextBox14.Text = 0.0
                    TextBox15.Text = 0.0
                    TextBox18.Text = 0.0
                    TextBox19.Text = 0.0
                    TextBox20.Text = 0.0
                    TextBox21.Text = 0.0
                    TextBox22.Text = 0.0
                    conn.Open()
                    Dim dt_table As DataTable = DirectCast(ViewState("purchase"), DataTable)
                    da = New SqlDataAdapter("select *  from po_ord_mat JOIN MATERIAL ON PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where po_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "' ORDER BY MAT_SLNO", conn)
                    da.Fill(dt_table)
                    conn.Close()
                    ViewState("purchase") = dt_table
                    Me.BINDGRIDpur()
                    ''CALCULATION
                    If GridView3.Rows.Count > 0 Then
                        Dim I As Integer = 0
                        For I = 0 To GridView3.Rows.Count - 1
                            Dim basic_price, packing_charge, discount, net_price, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, ANAL_VAL, priceincl, Fright, total_invoice, landed_cost As Decimal
                            ''Basic Price Count
                            basic_price = CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(5).Text)
                            ''Discount
                            If GridView3.Rows(I).Cells(6).Text = "PERCENTAGE" Then
                                discount = (CDec(basic_price * CDec(GridView3.Rows(I).Cells(7).Text)) / 100)
                            ElseIf GridView3.Rows(I).Cells(6).Text = "PER UNIT" Then
                                discount = CDec(GridView3.Rows(I).Cells(7).Text) * CDec(GridView3.Rows(I).Cells(4).Text)
                            ElseIf GridView3.Rows(I).Cells(6).Text = "PER MT" Then
                                discount = CDec(GridView3.Rows(I).Cells(7).Text) * CDec(GridView3.Rows(I).Cells(19).Text)
                            End If
                            ''Pack& forwd
                            If GridView3.Rows(I).Cells(8).Text = "PERCENTAGE" Then
                                packing_charge = ((basic_price - discount) * CDec(GridView3.Rows(I).Cells(9).Text)) / 100
                            ElseIf GridView3.Rows(I).Cells(8).Text = "PER UNIT" Then
                                packing_charge = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                            ElseIf GridView3.Rows(I).Cells(8).Text = "PER MT" Then
                                packing_charge = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                            End If
                            '' Fright calculatation
                            If GridView3.Rows(I).Cells(10).Text = "PERCENTAGE" Then
                                Fright = (basic_price * CDec(GridView3.Rows(I).Cells(15).Text)) / 100
                            ElseIf GridView3.Rows(I).Cells(10).Text = "PER UNIT" Then
                                Fright = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                            ElseIf GridView3.Rows(I).Cells(10).Text = "PER MT" Then
                                Fright = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                            End If
                            ''Net price
                            net_price = (basic_price - discount) + packing_charge
                            ''GST on cash
                            CGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(12).Text))) / 100
                            SGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(13).Text))) / 100
                            IGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(14).Text))) / 100
                            ''ANAL_VAL calculatation Freight Rs
                            ANAL_VAL = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                            CESS_VAL = ((net_price + Fright) * CDec(GridView3.Rows(I).Cells(16).Text)) / 100
                            priceincl = net_price + Fright + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL

                            ''total ORDER value
                            total_invoice = priceincl
                            landed_cost = net_price + CESS_VAL + Fright
                            TextBox83.Text = FormatNumber((CDec(TextBox83.Text) + discount), 2)
                            TextBox13.Text = FormatNumber((CDec(TextBox13.Text) + Fright), 2)
                            TextBox14.Text = FormatNumber((CDec(TextBox14.Text) + packing_charge), 2)
                            TextBox18.Text = FormatNumber(net_price + CDec(TextBox18.Text), 2)
                            TextBox20.Text = FormatNumber(SGST_VAL + CDec(TextBox20.Text), 2)
                            TextBox19.Text = FormatNumber((CGST_VAL + CDec(TextBox19.Text)), 2)
                            TextBox21.Text = FormatNumber((IGST_VAL + CDec(TextBox21.Text)), 2)
                            TextBox15.Text = FormatNumber((ANAL_VAL + CDec(TextBox15.Text)), 2)
                            TextBox10.Text = FormatNumber((CESS_VAL + CDec(TextBox10.Text)), 2)
                            ''
                        Next
                        TextBox22.Text = FormatNumber(CDec(TextBox18.Text) + CDec(TextBox19.Text) + CDec(TextBox20.Text) + CDec(TextBox21.Text) + CDec(TextBox13.Text) + CDec(TextBox15.Text) + CDec(TextBox10.Text), 2)
                        '' TextBox21.Text = FormatNumber(FormatNumber(CDec(TextBox18.Text) + CDec(TextBox19.Text) + CDec(TextBox20.Text) + CDec(TextBox21.Text) + CDec(TextBox13.Text) + CDec(TextBox15.Text), 0) - FormatNumber(CDec(TextBox18.Text) + CDec(TextBox19.Text) + CDec(TextBox20.Text) + CDec(TextBox21.Text) + CDec(TextBox13.Text) + CDec(TextBox15.Text), 2), 2)
                    End If

                ElseIf po_type = "RAW MATERIAL(IMP)" Then

                    TextBox822.Text = ""
                    Dim dt_fgn As New DataTable()
                    dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
                    ViewState("foreign") = dt_fgn
                    Me.BINDGRIDFGN()
                    Label384.Text = po_type.ToUpper & " INFORMATION"

                    MultiView1.ActiveViewIndex = 3
                    TextBox824.Text = "0.00"
                    TextBox831.Text = "0.00"
                    conn.Open()
                    Dim dt_mat As DataTable = DirectCast(ViewState("foreign"), DataTable)
                    da = New SqlDataAdapter("select *  from po_ord_mat JOIN MATERIAL ON PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where po_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "' ORDER BY MAT_SLNO", conn)
                    da.Fill(dt_mat)
                    conn.Close()
                    ViewState("foreign") = dt_mat
                    Me.BINDGRIDFGN()
                    conn.Open()
                    mc1.CommandText = "select supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox819.Text = dr.Item("supl_id")
                        TextBox820.Text = dr.Item("supl_name")
                        dr.Close()
                    End If
                    conn.Close()
                    TextBox817.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                    TextBox818.Text = SO_DATE
                    DropDownList60.Items.Clear()
                    For I As Integer = 0 To GridView216.Rows.Count - 1
                        DropDownList60.Items.Add(GridView216.Rows(I).Cells(0).Text)
                    Next
                    DropDownList60.Items.Add("Select")
                    DropDownList60.SelectedValue = "Select"
                    TextBox831.Text = 0.0
                    TextBox824.Text = 0.0

                    If GridView216.Rows.Count > 0 Then
                        Dim I As Integer = 0
                        For I = 0 To GridView216.Rows.Count - 1
                            Dim basic_price As Decimal
                            ''Basic Price Count
                            basic_price = CDec(GridView216.Rows(I).Cells(4).Text) * CDec(GridView216.Rows(I).Cells(5).Text)
                            TextBox824.Text = basic_price = CDec(TextBox824.Text)
                            TextBox831.Text = basic_price + CDec(TextBox831.Text)
                        Next
                    End If
                    po_matcodecombo1.Text = ""

                    ''''''''''''''''
                ElseIf po_type = "STORE MATERIAL(IMP)" Then

                    TextBox30.Text = ""
                    Dim dt_fgn As New DataTable()
                    dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
                    ViewState("foreign_store") = dt_fgn
                    Me.BINDGRIDFGN_STORE_IMP()
                    Label333.Text = po_type.ToUpper & " INFORMATION"

                    MultiView1.ActiveViewIndex = 5
                    TextBox31.Text = "0.00"
                    TextBox32.Text = "0.00"
                    conn.Open()
                    Dim dt_mat As DataTable = DirectCast(ViewState("foreign_store"), DataTable)
                    da = New SqlDataAdapter("select *  from po_ord_mat JOIN MATERIAL ON PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where po_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "' ORDER BY MAT_SLNO", conn)
                    da.Fill(dt_mat)
                    conn.Close()
                    ViewState("foreign_store") = dt_mat
                    Me.BINDGRIDFGN_STORE_IMP()
                    conn.Open()
                    mc1.CommandText = "select supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox23.Text = dr.Item("supl_id")
                        TextBox24.Text = dr.Item("supl_name")
                        dr.Close()
                    End If
                    conn.Close()
                    TextBox16.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                    TextBox17.Text = SO_DATE
                    DropDownList2.Items.Clear()
                    For I As Integer = 0 To GridView2.Rows.Count - 1
                        DropDownList2.Items.Add(GridView2.Rows(I).Cells(0).Text)
                    Next
                    DropDownList2.Items.Add("Select")
                    DropDownList2.SelectedValue = "Select"
                    TextBox32.Text = 0.0
                    TextBox31.Text = 0.0

                    If GridView2.Rows.Count > 0 Then
                        Dim I As Integer = 0
                        For I = 0 To GridView2.Rows.Count - 1
                            Dim basic_price As Decimal
                            ''Basic Price Count
                            basic_price = CDec(GridView2.Rows(I).Cells(4).Text) * CDec(GridView2.Rows(I).Cells(5).Text)
                            TextBox31.Text = basic_price = CDec(TextBox31.Text)
                            TextBox32.Text = basic_price + CDec(TextBox32.Text)
                        Next
                    End If
                    TextBox25.Text = ""
                    ''''''''''''''''

                ElseIf (po_type = "OUTSOURCED ITEMS") Then

                    MultiView1.ActiveViewIndex = 9
                    conn.Open()
                    mc1.CommandText = "select supl.SUPL_GST_NO,supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox5.Text = dr.Item("supl_id")
                        TextBox6.Text = dr.Item("supl_name")
                        dr.Close()
                    End If
                    conn.Close()

                    Dim gst_code, my_gst_code As New String("")
                    conn.Open()
                    mc1.CommandText = "select dater.d_code,dater.d_name,dater.gst_code from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox5.Text = dr.Item("d_code")
                        TextBox6.Text = dr.Item("d_name")
                        gst_code = dr.Item("gst_code")
                        dr.Close()
                    End If
                    conn.Close()
                    'search company profile compair gst code
                    conn.Open()
                    mc1.CommandText = "select * from comp_profile"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        my_gst_code = dr.Item("c_gst_no")
                        dr.Close()
                    End If
                    conn.Close()
                    If gst_code = my_gst_code Then
                        cgstPercentageOutsourced.Enabled = False
                        igstPercentageOutsourced.Enabled = False
                        sgstPercentageOutsourced.Enabled = False
                        TextBox42.Enabled = False
                        sgstPercentageOutsourced.Text = "0.00"
                        cgstPercentageOutsourced.Text = "0.00"
                        igstPercentageOutsourced.Text = "0.00"

                    Else
                        cgstPercentage.Enabled = True
                        igstPercentage.Enabled = True
                        sgstPercentage.Enabled = True
                        TextBox9.Enabled = True
                        sgstPercentage.Text = "0.00"
                        cgstPercentage.Text = "0.00"
                        igstPercentage.Text = "0.00"

                    End If
                    soDateOutsourced.Text = SO_DATE
                    soNoOutsourced.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)


                ElseIf po_type = "OUTSOURCED ITEMS(IMP)" Then

                    TextBox822.Text = ""
                    Dim dt_fgn As New DataTable()
                    dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
                    ViewState("foreign_outsourced") = dt_fgn
                    Me.BINDGRIDFGN_OUTSOURCED()
                    Label384.Text = po_type.ToUpper & " INFORMATION"


                    MultiView1.ActiveViewIndex = 4
                    TextBox55.Text = "0.00"
                    TextBox56.Text = "0.00"
                    conn.Open()
                    Dim dt_mat As DataTable = DirectCast(ViewState("foreign_outsourced"), DataTable)
                    da = New SqlDataAdapter("select * from po_ord_mat JOIN Outsource_F_ITEM ON PO_ORD_MAT.MAT_CODE=Outsource_F_ITEM.ITEM_CODE where po_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "' ORDER BY MAT_SLNO", conn)
                    da.Fill(dt_mat)
                    conn.Close()
                    ViewState("foreign_outsourced") = dt_mat
                    Me.BINDGRIDFGN_OUTSOURCED()
                    conn.Open()
                    mc1.CommandText = "select supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox38.Text = dr.Item("supl_id")
                        TextBox39.Text = dr.Item("supl_name")
                        dr.Close()
                    End If
                    conn.Close()
                    TextBox3.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                    TextBox4.Text = SO_DATE
                    DropDownList7.Items.Clear()
                    For I As Integer = 0 To GridView6.Rows.Count - 1
                        DropDownList7.Items.Add(GridView6.Rows(I).Cells(0).Text)
                    Next
                    DropDownList7.Items.Insert(0, "Select")
                    DropDownList7.SelectedValue = "Select"

                    If GridView6.Rows.Count > 0 Then
                        Dim I As Integer = 0
                        For I = 0 To GridView6.Rows.Count - 1
                            Dim basic_price As Decimal
                            ''Basic Price Count
                            basic_price = CDec(GridView6.Rows(I).Cells(4).Text) * CDec(GridView6.Rows(I).Cells(5).Text)
                            TextBox55.Text = basic_price = CDec(TextBox55.Text)
                            TextBox56.Text = basic_price + CDec(TextBox56.Text)
                        Next
                    End If
                    po_matcodecombo1.Text = ""
                End If
                ''DropDownList58.Items.Clear()
                'For I As Integer = 0 To GridView3.Rows.Count - 1
                '    DropDownList58.Items.Add(GridView3.Rows(I).Cells(0).Text)
                'Next
                'DropDownList58.Items.Add("Select")
                'DropDownList58.SelectedValue = "Select"

                '''''''''''''''''''''

                '''''''''''''''''''''

            ElseIf order_type = "Rate Contract" Then
                Label384.Text = po_type.ToUpper & " INFORMATION"

                ''SEARCH RATE CONTRACT VALUE TABLE
                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select PO_NO from RATE_CONTRACT WHERE PO_NO ='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    conn.Open()
                    Dim mcq As New SqlCommand
                    mcq.CommandText = "select supl.supl_tax,supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mcq.Connection = conn
                    dr = mcq.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox731.Text = dr.Item("supl_id")
                        TextBox732.Text = dr.Item("supl_name")
                        dr.Close()
                    End If
                    conn.Close()
                    TextBox729.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                    TextBox730.Text = SO_DATE

                    MultiView1.ActiveViewIndex = 7
                Else
                    Dim VALID_DATE As New Date
                    conn.Open()
                    Dim mcq As New SqlCommand
                    mcq.CommandText = "select VALID_DATE from RATE_CONTRACT WHERE PO_NO ='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mcq.Connection = conn
                    dr = mcq.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        VALID_DATE = dr.Item("VALID_DATE")
                        dr.Close()
                    End If
                    conn.Close()
                    If (Today < VALID_DATE) Then
                        Label678.Visible = False
                        If po_type = "RAW MATERIAL" Or po_type = "STORE MATERIAL" Then
                            Dim dt_rate As New DataTable()
                            ''dt_raw.Columns.AddRange(New DataColumn(17) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("MAT_EXCISE"), New DataColumn("MAT_EXCISE_DUTY"), New DataColumn("MAT_TAXTYPE"), New DataColumn("MAT_CST"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
                            dt_rate.Columns.AddRange(New DataColumn(20) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("CGST"), New DataColumn("SGST"), New DataColumn("IGST"), New DataColumn("ANAL_TAX"), New DataColumn("Cess"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC"), New DataColumn("MAT_QTY_RCVD"), New DataColumn("TOTAL_WT")})
                            ViewState("purchase") = dt_rate
                            Me.BINDGRIDpur()

                            MultiView1.ActiveViewIndex = 1
                            conn.Open()
                            mc1.CommandText = "select supl.supl_tax ,supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                            mc1.Connection = conn
                            dr = mc1.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                TextBox105.Text = dr.Item("supl_id")
                                TextBox106.Text = dr.Item("supl_name")
                                dr.Close()
                            End If
                            conn.Close()
                            TextBox102.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                            TextBox103.Text = SO_DATE
                            If freight_term = "Extra" Then
                                ''Freight %
                                Label356.Text = "Freight"
                                po_frightText1.ReadOnly = False
                                po_frightText1.BackColor = Drawing.Color.White
                                po_frightText1.ForeColor = Drawing.Color.Black
                            ElseIf freight_term = "Paid" Or freight_term = "Not Applicable" Then
                                Label356.Text = "Freight"
                                po_frightText1.ReadOnly = True
                                po_frightText1.Text = 0.0
                                po_frightText1.BackColor = Drawing.Color.Red
                                po_frightText1.ForeColor = Drawing.Color.White
                            End If
                        Else

                            Dim dt_wo As New DataTable()
                            dt_wo.Columns.AddRange(New DataColumn(17) {New DataColumn("W_SLNO"), New DataColumn("TAX_TYPE"), New DataColumn("W_NAME"), New DataColumn("W_QTY"), New DataColumn("W_AU"), New DataColumn("W_UNIT_PRICE"), New DataColumn("W_MATERIAL_COST"), New DataColumn("W_AREA"), New DataColumn("W_START_DATE"), New DataColumn("W_END_DATE"), New DataColumn("W_TOLERANCE"), New DataColumn("W_DISCOUNT"), New DataColumn("SGST"), New DataColumn("CGST"), New DataColumn("IGST"), New DataColumn("CESS"), New DataColumn("wo_type"), New DataColumn("t_value")})
                            ViewState("WORK") = dt_wo
                            Me.BINDGRIDWORK()
                            Label398.Text = "NEW WORK ORDER FOR " & po_type.ToUpper

                            MultiView1.ActiveViewIndex = 6
                            conn.Open()
                            Dim mcz As New SqlCommand
                            mcz.CommandText = "select supl.supl_tax,supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                            mcz.Connection = conn
                            dr = mcz.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                TextBox128.Text = dr.Item("supl_id")
                                TextBox129.Text = dr.Item("supl_name")
                                dr.Close()
                            End If
                            conn.Close()
                            TextBox722.Text = po_type
                            TextBox126.Text = SO_DATE
                            TextBox125.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                            ''search taxable service
                            conn.Open()
                            dt.Clear()
                            da = New SqlDataAdapter("select * from  wo_dic where taxable_service='" & po_type & "'", conn)
                            da.Fill(dt)
                            conn.Close()
                            DropDownList46.Items.Clear()
                            DropDownList46.DataSource = dt
                            DropDownList46.DataValueField = "work_desc"
                            DropDownList46.DataBind()
                            DropDownList46.Items.Add("Select")
                            DropDownList46.SelectedValue = "Select"
                            TextBox681.Text = 0.0
                            TextBox691.Text = 0.0
                            TextBox701.Text = 0.0
                            TextBox711.Text = 0.0
                            TextBox721.Text = 0.0
                            ''search work name
                        End If
                        DropDownList58.Items.Clear()
                        For I As Integer = 0 To GridView3.Rows.Count - 1
                            DropDownList58.Items.Add(GridView3.Rows(I).Cells(0).Text)
                        Next
                        DropDownList58.Items.Add("Select")
                        DropDownList58.SelectedValue = "Select"
                    Else
                        Label678.Visible = True
                        Label678.Text = "Rate Contract validity expired."
                    End If

                End If

            ElseIf order_type = "Sale Order" Then
                If po_type = "RAW MATERIAL" Or po_type = "STORE MATERIAL" Or po_type = "MISCELLANEOUS" Then

                    'GST CODE SEARCH
                    Dim gst_code, my_gst_code As New String("")
                    conn.Open()
                    mc1.CommandText = "select dater.gst_code, dater.d_code,dater.d_name from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox725.Text = dr.Item("d_code")
                        TextBox726.Text = dr.Item("d_name")
                        gst_code = dr.Item("gst_code")
                        dr.Close()
                    End If
                    conn.Close()

                    'search company profile compair gst code
                    conn.Open()
                    mc1.CommandText = "select * from comp_profile"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        my_gst_code = dr.Item("c_gst_no")
                        dr.Close()
                    End If
                    conn.Close()
                    If gst_code = my_gst_code Then
                        cgst_textbox.Enabled = False
                        sgst_textbox.Enabled = False
                        igst_textbox.Enabled = False
                        cess_textbox.Enabled = False
                        sgstPercentage.Text = "0.00"
                        cgstPercentage.Text = "0.00"
                        igstPercentage.Text = "0.00"
                        TextBox78.Text = "0.00"
                    Else
                        cgstPercentage.Enabled = True
                        igstPercentage.Enabled = True
                        sgstPercentage.Enabled = True
                        cess_textbox.Enabled = True
                        sgstPercentage.Text = "0.00"
                        cgstPercentage.Text = "0.00"
                        igstPercentage.Text = "0.00"
                        TextBox78.Text = "0.1"
                    End If

                    TextBox723.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                    TextBox724.Text = SO_DATE

                    ''search mat group
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("select (group_code + ' , ' + group_name) as group_details from BIN_GROUP where GROUP_TYPE='" & po_type & "'", conn)
                    da.Fill(dt)
                    conn.Close()
                    TextBox727.Text = po_type
                    ''GROUP SEARCHDropDownList10.Items.Clear()
                    DropDownList11.DataSource = dt
                    DropDownList11.DataValueField = "group_details"
                    DropDownList11.DataBind()
                    DropDownList11.Items.Insert(0, "Select")
                    DropDownList11.SelectedValue = "Select"
                    If ORDER_TO = "I.P.T." Then
                        TextBox77.BackColor = Drawing.Color.White
                        TextBox77.ForeColor = Drawing.Color.Black
                        TextBox77.Enabled = True
                    ElseIf ORDER_TO = "Other" Then
                        TextBox77.BackColor = Drawing.Color.White
                        TextBox77.ForeColor = Drawing.Color.Black
                        TextBox77.Enabled = True
                    End If

                    MultiView1.ActiveViewIndex = 2
                    If freight_term = "Extra" Then
                        ''Freight %
                        po_frightText0.ReadOnly = False
                        po_frightText0.BackColor = Drawing.Color.White
                        po_frightText0.ForeColor = Drawing.Color.Black
                    ElseIf freight_term = "Paid" Or freight_term = "Not Applicable" Then
                        po_frightText0.ReadOnly = True
                        po_frightText0.Text = 0.0
                        po_frightText0.BackColor = Drawing.Color.Red
                        po_frightText0.ForeColor = Drawing.Color.White
                    End If
                ElseIf po_type = "FINISH GOODS" Then

                    MultiView1.ActiveViewIndex = 8
                    'GST CODE SEARCH

                    Dim gst_code, my_gst_code As New String("")
                    conn.Open()
                    mc1.CommandText = "select dater.d_code,dater.d_name,dater.gst_code from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        TextBox90.Text = dr.Item("d_code")
                        TextBox88.Text = dr.Item("d_name")
                        gst_code = dr.Item("gst_code")
                        dr.Close()
                    End If
                    conn.Close()
                    'search company profile compair gst code
                    conn.Open()
                    mc1.CommandText = "select * from comp_profile"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        my_gst_code = dr.Item("c_gst_no")
                        dr.Close()
                    End If
                    conn.Close()
                    If gst_code = my_gst_code Then

                        sgstPercentage.Text = "0.00"
                        cgstPercentage.Text = "0.00"
                        igstPercentage.Text = "0.00"
                        TextBox65.Text = "0.00"
                        cgstPercentage.ReadOnly = True
                        igstPercentage.ReadOnly = True
                        sgstPercentage.ReadOnly = True
                        TextBox9.ReadOnly = True
                    Else
                        sgstPercentage.Text = "0.00"
                        cgstPercentage.Text = "0.00"
                        igstPercentage.Text = "0.00"
                        TextBox65.Text = "0.1"
                        cgstPercentage.ReadOnly = False
                        igstPercentage.ReadOnly = False
                        sgstPercentage.ReadOnly = False
                        TextBox9.ReadOnly = False
                    End If
                    TextBox87.Text = SO_DATE
                    TextBox86.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)

                ElseIf po_type = "OUTSOURCED ITEMS" Then

                    'MultiView1.ActiveViewIndex = 9
                    ''GST CODE SEARCH

                    'Dim gst_code, my_gst_code As New String("")
                    'conn.Open()
                    'mc1.CommandText = "select dater.d_code,dater.d_name,dater.gst_code from dater join order_details on order_details.PARTY_CODE=dater.d_code where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                    'mc1.Connection = conn
                    'dr = mc1.ExecuteReader
                    'If dr.HasRows Then
                    '    dr.Read()
                    '    TextBox5.Text = dr.Item("d_code")
                    '    TextBox6.Text = dr.Item("d_name")
                    '    gst_code = dr.Item("gst_code")
                    '    dr.Close()
                    'End If
                    'conn.Close()
                    ''search company profile compair gst code
                    'conn.Open()
                    'mc1.CommandText = "select * from comp_profile"
                    'mc1.Connection = conn
                    'dr = mc1.ExecuteReader
                    'If dr.HasRows Then
                    '    dr.Read()
                    '    my_gst_code = dr.Item("c_gst_no")
                    '    dr.Close()
                    'End If
                    'conn.Close()
                    'If gst_code = my_gst_code Then
                    '    cgstPercentageOutSourcedSale.Enabled = False
                    '    sgstPercentageOutSourcedSale.Enabled = False
                    '    igstPercentageOutSourcedSale.Enabled = False

                    '    cessPercentageOutSourcedSale.Enabled = False
                    '    sgstPercentageOutSourcedSale.Text = "0.00"
                    '    cgstPercentageOutSourcedSale.Text = "0.00"
                    '    igstPercentageOutSourcedSale.Text = "0.00"
                    '    TextBox93.Text = "0.00"
                    'Else
                    '    cgstPercentage.Enabled = True
                    '    igstPercentage.Enabled = True
                    '    sgstPercentage.Enabled = True
                    '    cessPercentageOutSourcedSale.Enabled = True
                    '    sgstPercentage.Text = "0.00"
                    '    cgstPercentage.Text = "0.00"
                    '    igstPercentage.Text = "0.00"
                    '    TextBox93.Text = "0.1"
                    'End If
                    'soDateOutsourced.Text = SO_DATE
                    'soNoOutsourced.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                End If
            ElseIf order_type = "Work Order" Then
                Dim dt_wo As New DataTable()
                dt_wo.Columns.AddRange(New DataColumn(17) {New DataColumn("W_SLNO"), New DataColumn("TAX_TYPE"), New DataColumn("W_NAME"), New DataColumn("W_QTY"), New DataColumn("W_AU"), New DataColumn("W_UNIT_PRICE"), New DataColumn("W_MATERIAL_COST"), New DataColumn("W_AREA"), New DataColumn("W_START_DATE"), New DataColumn("W_END_DATE"), New DataColumn("W_TOLERANCE"), New DataColumn("W_DISCOUNT"), New DataColumn("SGST"), New DataColumn("CGST"), New DataColumn("IGST"), New DataColumn("CESS"), New DataColumn("wo_type"), New DataColumn("t_value")})
                ViewState("WORK") = dt_wo
                Me.BINDGRIDWORK()
                Label398.Text = "NEW WORK ORDER FOR " & po_type.ToUpper

                MultiView1.ActiveViewIndex = 6
                conn.Open()
                Dim mcz As New SqlCommand
                mcz.CommandText = "select supl.supl_tax,supl.supl_id,supl.supl_name from supl join order_details on order_details.PARTY_CODE=supl.supl_id where order_details.so_no='" & TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1) & "'"
                mcz.Connection = conn
                dr = mcz.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox128.Text = dr.Item("supl_id")
                    TextBox129.Text = dr.Item("supl_name")
                    dr.Close()
                End If
                conn.Close()
                TextBox722.Text = po_type
                TextBox126.Text = SO_DATE
                TextBox125.Text = TextBox832.Text.Substring(0, TextBox832.Text.IndexOf(",") - 1)
                ''search taxable service
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select * from wo_dic where taxable_service='" & po_type & "'", conn)
                da.Fill(dt)
                conn.Close()
                DropDownList46.Items.Clear()
                DropDownList46.DataSource = dt
                DropDownList46.DataValueField = "work_desc"
                DropDownList46.DataBind()
                DropDownList46.Items.Add("Select")
                DropDownList46.SelectedValue = "Select"
                TextBox681.Text = 0.0
                TextBox691.Text = 0.0
                TextBox701.Text = 0.0
                TextBox711.Text = 0.0
                TextBox721.Text = 0.0
                ''search work name
                conn.Open()
                Dim dt_wo1 As DataTable = DirectCast(ViewState("WORK"), DataTable)
                da = New SqlDataAdapter("select * FROM WO_ORDER where PO_NO='" & TextBox125.Text & "'", conn)
                da.Fill(dt_wo1)
                conn.Close()
                ViewState("WORK") = dt_wo1
                Me.BINDGRIDWORK()
                If GridView4.Rows.Count > 0 Then
                    Dim i As Integer = 0
                    For i = 0 To GridView4.Rows.Count - 1
                        Dim total, net, discount, SGST, CGST, IGST, CESS As New Decimal(0)
                        Dim mat_cost As Decimal = 0
                        net = CDec(GridView4.Rows(i).Cells(3).Text) * CDec(GridView4.Rows(i).Cells(5).Text)
                        mat_cost = CDec(GridView4.Rows(i).Cells(3).Text) * CDec(GridView4.Rows(i).Cells(6).Text)
                        discount = (net * CDec(GridView4.Rows(i).Cells(11).Text)) / 100
                        SGST = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(12).Text)) / 100
                        CGST = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(13).Text)) / 100
                        IGST = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(14).Text)) / 100
                        CESS = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(15).Text)) / 100

                        total = (net - discount) + SGST + CGST + IGST + CESS + mat_cost
                        total = FormatNumber(total, 2)
                        GridView4.Rows(i).Cells(17).Text = total
                        TextBox681.Text = FormatNumber(net + mat_cost + CDec(TextBox681.Text), 3)
                        TextBox691.Text = FormatNumber((SGST + CDec(TextBox691.Text)), 3)
                        TextBox701.Text = FormatNumber(CGST + CDec(TextBox701.Text), 3)
                        TextBox11.Text = FormatNumber((IGST + CDec(TextBox11.Text)), 3)
                        TextBox12.Text = FormatNumber(CESS + CDec(TextBox12.Text), 3)
                        TextBox711.Text = FormatNumber(total - FormatNumber(total, 0), 3)
                        TextBox721.Text = FormatNumber(FormatNumber(total, 0) + CDec(TextBox721.Text), 2)
                    Next
                End If

            End If
        End If


    End Sub

    Protected Sub Button81_Click(sender As Object, e As EventArgs) Handles Button81.Click
        If po_matcodecombo1.Text = "" Then
            po_matcodecombo1.Focus()
            Return
        ElseIf po_matqty_text1.Text = "" Or IsNumeric(po_matqty_text1.Text) = False Then
            po_matqty_text1.Focus()
            Return
        ElseIf po_unitrateText0.Text = "" Or IsNumeric(po_unitrateText0.Text) = False Then
            po_unitrateText0.Focus()
            Return
        ElseIf Delvdate8.Text = "" Or IsDate(Delvdate8.Text) = False Then
            Delvdate8.Focus()
            Return
        ElseIf po_matcodecombo1.Text.IndexOf(",") <> 10 Then
            po_matcodecombo1.Text = ""
            po_matcodecombo1.Focus()
            Return
        ElseIf po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1)).Length <> 9 Or IsNumeric(po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1))) = False Then
            po_matcodecombo1.Focus()
            po_matcodecombo1.Text = ""
            Return
        End If
        For I As Integer = 0 To DirectCast(ViewState("foreign"), DataTable).Rows.Count - 1
            If DirectCast(ViewState("foreign"), DataTable).Rows(I)(0) = DropDownList60.SelectedValue Then
                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1)) & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    Label637.Text = "Please Enter Correct Material Code"
                    Return
                End If
                Dim AU As String = ""
                Dim MAT_NAME As String = ""
                conn.Open()
                mycommand.CommandText = "select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1)) & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    AU = dr.Item("MAT_AU")
                    MAT_NAME = dr.Item("MAT_NAME")
                    dr.Close()
                End If
                conn.Close()
                DirectCast(ViewState("foreign"), DataTable).Rows(I)(1) = po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1))
                DirectCast(ViewState("foreign"), DataTable).Rows(I)(2) = MAT_NAME
                DirectCast(ViewState("foreign"), DataTable).Rows(I)(3) = AU
                DirectCast(ViewState("foreign"), DataTable).Rows(I)(4) = po_matqty_text1.Text
                DirectCast(ViewState("foreign"), DataTable).Rows(I)(5) = po_unitrateText0.Text
                DirectCast(ViewState("foreign"), DataTable).Rows(I)(6) = Delvdate8.Text
                DirectCast(ViewState("foreign"), DataTable).Rows(I)(7) = TextBox822.Text
            End If
        Next
        Me.BINDGRIDFGN()
        TextBox824.Text = 0.0
        TextBox831.Text = 0.0
        If GridView216.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView216.Rows.Count - 1
                Dim basic_price As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView216.Rows(I).Cells(4).Text) * CDec(GridView216.Rows(I).Cells(5).Text)
                TextBox824.Text = basic_price + CDec(TextBox824.Text)
                TextBox831.Text = basic_price + CDec(TextBox831.Text)
            Next
        End If
        po_matcodecombo1.Text = ""
    End Sub

    Protected Sub Button60_Click(sender As Object, e As EventArgs) Handles Button60.Click
        Dim order_type As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox102.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")

            dr.Close()
        End If
        conn.Close()
        If order_type = "Rate Contract" Then
            Label439.Text = "This is a rate contract can't be submited"
            Return
        End If
        ''CHECK MATERIAL AVAILABILITY

        count = 0
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select PO_NO from PO_ORD_MAT WHERE PO_NO ='" & TextBox102.Text & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If count = 0 Then
            Label439.Text = "Please add material first"
            Return
        Else
            ''update order details
            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry
                    'conn.Open()
                    'Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox102.Text & "'", conn)
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE', Finance_approved='No' where SO_NO ='" & TextBox102.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()

                    Label439.Text = "Order Submited"
                    myTrans.Commit()
                    conn_trans.Close()
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    Label439.Text = "There was some Error, please contact EDP."
                Finally
                    conn.Close()
                    conn_trans.Close()
                End Try

            End Using

        End If
    End Sub

    Protected Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                ''DELETE EXITING DATA
                Dim order_type As String = ""
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox102.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")

                    dr.Close()
                End If
                conn.Close()


                If order_type <> "Rate Contract" Then

                    mycommand = New SqlCommand("DELETE FROM PO_ORD_MAT WHERE PO_NO ='" & TextBox102.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()

                    'Database updation entry
                    ''DATA SAVE IN PO_ORD_MAT
                    Dim ENTRY_TAX As Decimal = 0
                    Dim TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0)

                    For I = 0 To GridView3.Rows.Count - 1
                        Dim po_slno As String = TextBox102.Text
                        Dim sno As Integer = GridView3.Rows(I).Cells(0).Text
                        Dim matcode As String = GridView3.Rows(I).Cells(1).Text
                        Dim matname As String = GridView3.Rows(I).Cells(2).Text
                        Dim matqty As Decimal = GridView3.Rows(I).Cells(4).Text
                        Dim urate As Decimal = GridView3.Rows(I).Cells(5).Text
                        Dim disc_type As String = GridView3.Rows(I).Cells(6).Text
                        Dim disc As Decimal = CDec(GridView3.Rows(I).Cells(7).Text)
                        Dim pf_type As String = GridView3.Rows(I).Cells(8).Text
                        Dim pf As Decimal = CDec(GridView3.Rows(I).Cells(9).Text)
                        Dim freight_type As String = GridView3.Rows(I).Cells(10).Text
                        Dim fritax As Decimal = GridView3.Rows(I).Cells(11).Text
                        Dim CGST As String = GridView3.Rows(I).Cells(12).Text
                        Dim SGST As String = GridView3.Rows(I).Cells(13).Text
                        Dim IGST As String = GridView3.Rows(I).Cells(14).Text
                        Dim ANAL As Decimal = GridView3.Rows(I).Cells(15).Text
                        Dim CESS As Decimal = GridView3.Rows(I).Cells(16).Text


                        Dim ddate As String = CDate(GridView3.Rows(I).Cells(17).Text.Trim)
                        Dim mat_details As String = GridView3.Rows(I).Cells(18).Text.Trim
                        Dim mat_qty_rcvd As Decimal = CDec(GridView3.Rows(I).Cells(19).Text.Trim)
                        Dim TOTAL_WT As Decimal = CDec(GridView3.Rows(I).Cells(20).Text.Trim)
                        If mat_details = "&nbsp;" Then
                            mat_details = ""
                        End If

                        ''TDS CHECK TDS ON GST CRITERIA
                        If CDec(TextBox18.Text) >= 250000 Then
                            If CDec(TextBox835.Text) > 0 Then
                                TDS_SGST = 0.00
                                TDS_CGST = 0.00
                                TDS_IGST = 2.0

                            Else
                                TDS_SGST = 1.0
                                TDS_CGST = 1.0
                                TDS_IGST = 0.00
                            End If
                        End If

                        'conn.Open()
                        Dim query As String = "Insert Into PO_ORD_MAT(HSN_CODE,PO_NO,MAT_SLNO,MAT_CODE,MAT_NAME,MAT_DESC,MAT_QTY,MAT_QTY_RCVD,MAT_UNIT_RATE,MAT_DISCOUNT,DISC_TYPE,MAT_PACK,PF_TYPE,MAT_FREIGHT_PU,FREIGHT_TYPE,SGST,CGST,IGST,CESS,ANAL_TAX,MAT_STATUS,AMD_NO,AMD_DATE,MAT_DELIVERY,TOTAL_WT,TDS_SGST,TDS_CGST,TDS_IGST) values (@HSN_CODE,@PO_NO,@MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_DESC,@MAT_QTY,@MAT_QTY_RCVD,@MAT_UNIT_RATE,@MAT_DISCOUNT,@DISC_TYPE,@MAT_PACK,@PF_TYPE,@MAT_FREIGHT_PU,@FREIGHT_TYPE,@SGST,@CGST,@IGST,@CESS,@ANAL_TAX,@MAT_STATUS,@AMD_NO,@AMD_DATE,@MAT_DELIVERY,@TOTAL_WT,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                        cmd.Parameters.AddWithValue("@MAT_SLNO", sno)
                        cmd.Parameters.AddWithValue("@MAT_CODE", matcode)
                        cmd.Parameters.AddWithValue("@MAT_NAME", matname)
                        cmd.Parameters.AddWithValue("@MAT_DESC", mat_details)
                        cmd.Parameters.AddWithValue("@MAT_QTY", matqty)
                        cmd.Parameters.AddWithValue("@MAT_QTY_RCVD", mat_qty_rcvd)
                        cmd.Parameters.AddWithValue("@MAT_UNIT_RATE", urate)
                        cmd.Parameters.AddWithValue("@PF_TYPE", pf_type)
                        cmd.Parameters.AddWithValue("@MAT_PACK", pf)
                        cmd.Parameters.AddWithValue("@DISC_TYPE", disc_type)
                        cmd.Parameters.AddWithValue("@MAT_DISCOUNT", disc)
                        cmd.Parameters.AddWithValue("@FREIGHT_TYPE", freight_type)
                        cmd.Parameters.AddWithValue("@MAT_FREIGHT_PU", fritax)
                        cmd.Parameters.AddWithValue("@CGST", CGST)
                        cmd.Parameters.AddWithValue("@SGST", SGST)
                        cmd.Parameters.AddWithValue("@IGST", IGST)
                        cmd.Parameters.AddWithValue("@CESS", CESS)
                        cmd.Parameters.AddWithValue("@ANAL_TAX", ANAL)
                        cmd.Parameters.AddWithValue("@MAT_STATUS", "Pending")
                        cmd.Parameters.AddWithValue("@AMD_NO", "0")
                        cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox103.Text), "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@MAT_DELIVERY", Date.ParseExact(ddate, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@TOTAL_WT", TOTAL_WT)
                        cmd.Parameters.AddWithValue("@TDS_SGST", TDS_SGST)
                        cmd.Parameters.AddWithValue("@TDS_CGST", TDS_CGST)
                        cmd.Parameters.AddWithValue("@TDS_IGST", TDS_IGST)
                        cmd.Parameters.AddWithValue("@HSN_CODE", TextBox57.Text)

                        cmd.ExecuteReader()
                        cmd.Dispose()

                    Next
                    ''update order details
                    'conn_trans.Open()
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=" & GridView3.Rows.Count & " , FULL_VALUE= " & CDec(TextBox22.Text) & ", PO_BASE_VALUE= " & CDec(TextBox18.Text) & " where SO_NO ='" & TextBox102.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()


                    Dim dt_raw As New DataTable()
                    dt_raw.Columns.AddRange(New DataColumn(20) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("CGST"), New DataColumn("SGST"), New DataColumn("IGST"), New DataColumn("ANAL_TAX"), New DataColumn("Cess"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC"), New DataColumn("MAT_QTY_RCVD"), New DataColumn("TOTAL_WT")})
                    ViewState("purchase") = dt_raw
                    Me.BINDGRIDpur()

                    myTrans.Commit()
                    conn_trans.Close()
                    Label439.Text = "Data Saved Succsessfully"

                ElseIf order_type = "Rate Contract" Then

                    Dim ORD_AMT As New Decimal(0)
                    Dim UPTO_AMT As New Decimal(0)
                    conn.Open()
                    'Dim mc1 As New SqlCommand
                    mc1.CommandText = "SELECT ORD_AMOUNT FROM RATE_CONTRACT WHERE PO_NO= '" & TextBox102.Text & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        ORD_AMT = dr.Item("ORD_AMOUNT")
                        dr.Close()
                    End If
                    conn.Close()
                    conn.Open()
                    'Dim mc1 As New SqlCommand
                    'mc1.CommandText = "SELECT SUM(T_VALUE) AS T_VALUE FROM WO_ORDER where PO_NO= '" & TextBox125.Text & "'"
                    ''mc1.CommandText = "select sum(NET_AMT) AS T_VALUE from VOUCHER where PO_NO='" & TextBox102.Text & "'"
                    mc1.CommandText = "select sum(PROV_VALUE) AS T_VALUE from PO_RCD_MAT where PO_NO='" & TextBox102.Text & "'"
                    'mc1.CommandText = "select sum(TOTAL_PRICE) AS T_VALUE from MAT_DETAILS where COST_CODE='" & TextBox125.Text & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        If IsDBNull(dr.Item("T_VALUE")) Then
                            UPTO_AMT = 0
                        Else
                            UPTO_AMT = dr.Item("T_VALUE")
                        End If

                        dr.Close()
                    End If
                    conn.Close()
                    If ((ORD_AMT - UPTO_AMT) >= CDec(TextBox18.Text)) Then

                        mycommand = New SqlCommand("update ORDER_DETAILS set SO_STATUS='RCM' where SO_NO ='" & TextBox102.Text & "'", conn_trans, myTrans)
                        mycommand.ExecuteReader()
                        mycommand.Dispose()

                        'Database updation entry
                        ''DATA SAVE IN PO_ORD_MAT
                        Dim ENTRY_TAX As Decimal = 0
                        Dim TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0)

                        For I = 0 To GridView3.Rows.Count - 1
                            Dim po_slno As String = TextBox102.Text
                            Dim sno As Integer = GridView3.Rows(I).Cells(0).Text
                            Dim matcode As String = GridView3.Rows(I).Cells(1).Text
                            Dim matname As String = GridView3.Rows(I).Cells(2).Text
                            Dim matqty As Decimal = GridView3.Rows(I).Cells(4).Text
                            Dim urate As Decimal = GridView3.Rows(I).Cells(5).Text
                            Dim disc_type As String = GridView3.Rows(I).Cells(6).Text
                            Dim disc As Decimal = CDec(GridView3.Rows(I).Cells(7).Text)
                            Dim pf_type As String = GridView3.Rows(I).Cells(8).Text
                            Dim pf As Decimal = CDec(GridView3.Rows(I).Cells(9).Text)
                            Dim freight_type As String = GridView3.Rows(I).Cells(10).Text
                            Dim fritax As Decimal = GridView3.Rows(I).Cells(11).Text
                            Dim CGST As String = GridView3.Rows(I).Cells(12).Text
                            Dim SGST As String = GridView3.Rows(I).Cells(13).Text
                            Dim IGST As String = GridView3.Rows(I).Cells(14).Text
                            Dim ANAL As Decimal = GridView3.Rows(I).Cells(15).Text
                            Dim CESS As Decimal = GridView3.Rows(I).Cells(16).Text
                            Dim ddate As String = CDate(GridView3.Rows(I).Cells(17).Text.Trim)
                            Dim mat_details As String = GridView3.Rows(I).Cells(18).Text.Trim
                            Dim mat_qty_rcvd As Decimal = CDec(GridView3.Rows(I).Cells(19).Text.Trim)
                            Dim TOTAL_WT As Decimal = CDec(GridView3.Rows(I).Cells(20).Text.Trim)
                            If mat_details = "&nbsp;" Then
                                mat_details = ""
                            End If

                            ''TDS CHECK TDS ON GST CRITERIA
                            If CDec(TextBox18.Text) >= 250000 Then
                                If CDec(TextBox835.Text) > 0 Then
                                    TDS_SGST = 0.00
                                    TDS_CGST = 0.00
                                    TDS_IGST = 2.0

                                Else
                                    TDS_SGST = 1.0
                                    TDS_CGST = 1.0
                                    TDS_IGST = 0.00
                                End If
                            End If

                            'conn.Open()
                            Dim query As String = "Insert Into PO_ORD_MAT(HSN_CODE,PO_NO,MAT_SLNO,MAT_CODE,MAT_NAME,MAT_DESC,MAT_QTY,MAT_QTY_RCVD,MAT_UNIT_RATE,MAT_DISCOUNT,DISC_TYPE,MAT_PACK,PF_TYPE,MAT_FREIGHT_PU,FREIGHT_TYPE,SGST,CGST,IGST,CESS,ANAL_TAX,MAT_STATUS,AMD_NO,AMD_DATE,MAT_DELIVERY,TOTAL_WT,TDS_SGST,TDS_CGST,TDS_IGST) values (@HSN_CODE,@PO_NO,@MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_DESC,@MAT_QTY,@MAT_QTY_RCVD,@MAT_UNIT_RATE,@MAT_DISCOUNT,@DISC_TYPE,@MAT_PACK,@PF_TYPE,@MAT_FREIGHT_PU,@FREIGHT_TYPE,@SGST,@CGST,@IGST,@CESS,@ANAL_TAX,@MAT_STATUS,@AMD_NO,@AMD_DATE,@MAT_DELIVERY,@TOTAL_WT,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                            Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                            cmd.Parameters.AddWithValue("@MAT_SLNO", sno)
                            cmd.Parameters.AddWithValue("@MAT_CODE", matcode)
                            cmd.Parameters.AddWithValue("@MAT_NAME", matname)
                            cmd.Parameters.AddWithValue("@MAT_DESC", mat_details)
                            cmd.Parameters.AddWithValue("@MAT_QTY", matqty)
                            cmd.Parameters.AddWithValue("@MAT_QTY_RCVD", mat_qty_rcvd)
                            cmd.Parameters.AddWithValue("@MAT_UNIT_RATE", urate)
                            cmd.Parameters.AddWithValue("@PF_TYPE", pf_type)
                            cmd.Parameters.AddWithValue("@MAT_PACK", pf)
                            cmd.Parameters.AddWithValue("@DISC_TYPE", disc_type)
                            cmd.Parameters.AddWithValue("@MAT_DISCOUNT", disc)
                            cmd.Parameters.AddWithValue("@FREIGHT_TYPE", freight_type)
                            cmd.Parameters.AddWithValue("@MAT_FREIGHT_PU", fritax)
                            cmd.Parameters.AddWithValue("@CGST", CGST)
                            cmd.Parameters.AddWithValue("@SGST", SGST)
                            cmd.Parameters.AddWithValue("@IGST", IGST)
                            cmd.Parameters.AddWithValue("@CESS", CESS)
                            cmd.Parameters.AddWithValue("@ANAL_TAX", ANAL)
                            cmd.Parameters.AddWithValue("@MAT_STATUS", "Pending")
                            cmd.Parameters.AddWithValue("@AMD_NO", "0")
                            cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox103.Text), "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@MAT_DELIVERY", Date.ParseExact(ddate, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@TOTAL_WT", TOTAL_WT)
                            cmd.Parameters.AddWithValue("@TDS_SGST", TDS_SGST)
                            cmd.Parameters.AddWithValue("@TDS_CGST", TDS_CGST)
                            cmd.Parameters.AddWithValue("@TDS_IGST", TDS_IGST)
                            cmd.Parameters.AddWithValue("@HSN_CODE", TextBox57.Text)

                            cmd.ExecuteReader()
                            cmd.Dispose()

                        Next
                        ''update order details
                        'conn_trans.Open()
                        Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=NO_OF_ITEM+" & GridView3.Rows.Count & " , FULL_VALUE= FULL_VALUE + " & CDec(TextBox22.Text) & ", PO_BASE_VALUE= PO_BASE_VALUE+ " & CDec(TextBox18.Text) & " where SO_NO ='" & TextBox102.Text & "'", conn_trans, myTrans)
                        cmd2.ExecuteReader()
                        cmd2.Dispose()


                        Dim dt_raw As New DataTable()
                        dt_raw.Columns.AddRange(New DataColumn(20) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("CGST"), New DataColumn("SGST"), New DataColumn("IGST"), New DataColumn("ANAL_TAX"), New DataColumn("Cess"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC"), New DataColumn("MAT_QTY_RCVD"), New DataColumn("TOTAL_WT")})
                        ViewState("purchase") = dt_raw
                        Me.BINDGRIDpur()

                        myTrans.Commit()
                        conn_trans.Close()
                        Label439.Text = "Data Saved Succsessfully"

                    Else

                        'Button59.Enabled = False
                        Label439.Text = "Total amount is exceeding the rate contract balance amount Rs " & CDec(TextBox18.Text) - (ORD_AMT - UPTO_AMT)
                        Label439.ForeColor = Drawing.Color.Red

                    End If



                End If


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label439.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try


        End Using

    End Sub

    Protected Sub Button48_Click(sender As Object, e As EventArgs) Handles Button48.Click
        Dim dt_raw As New DataTable()
        dt_raw.Columns.AddRange(New DataColumn(20) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("CGST"), New DataColumn("SGST"), New DataColumn("IGST"), New DataColumn("ANAL_TAX"), New DataColumn("Cess"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC"), New DataColumn("MAT_QTY_RCVD"), New DataColumn("TOTAL_WT")})
        ViewState("purchase") = dt_raw
        Me.BINDGRIDpur()
        TextBox83.Text = "0.00"
        TextBox13.Text = "0.00"
        TextBox14.Text = "0.00"
        TextBox18.Text = "0.00"
        TextBox20.Text = "0.00"
        TextBox19.Text = "0.00"
        TextBox21.Text = "0.00"
        TextBox15.Text = "0.00"
        TextBox10.Text = "0.00"
        TextBox22.Text = "0.00"

        MultiView1.ActiveViewIndex = 0

    End Sub

    Protected Sub Button76_Click(sender As Object, e As EventArgs) Handles Button76.Click
        For I As Integer = 0 To DirectCast(ViewState("purchase"), DataTable).Rows.Count - 1
            If DirectCast(ViewState("purchase"), DataTable).Rows(I)(0) = DropDownList58.SelectedValue Then
                DirectCast(ViewState("purchase"), DataTable).Rows(I).Delete()
                DirectCast(ViewState("purchase"), DataTable).AcceptChanges()
                For J As Integer = 0 To DirectCast(ViewState("purchase"), DataTable).Rows.Count - 1
                    DirectCast(ViewState("purchase"), DataTable).Rows(J)(0) = J + 1
                Next
                Me.BINDGRIDpur()
                DropDownList58.Items.Clear()
                For K As Integer = 0 To GridView3.Rows.Count - 1
                    DropDownList58.Items.Add(GridView3.Rows(K).Cells(0).Text)
                Next
                DropDownList58.Items.Add("Select")
                DropDownList58.SelectedValue = "Select"
                Return
            End If

        Next
        If GridView3.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView3.Rows.Count - 1
                Dim basic_price, packing_charge, discount, net_price, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, ANAL_VAL, priceincl, Fright, total_invoice, landed_cost As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(5).Text)
                ''Discount
                If GridView3.Rows(I).Cells(6).Text = "PERCENTAGE" Then
                    discount = (CDec(basic_price * CDec(GridView3.Rows(I).Cells(7).Text)) / 100)
                ElseIf GridView3.Rows(I).Cells(6).Text = "PER UNIT" Then
                    discount = CDec(GridView3.Rows(I).Cells(7).Text) * CDec(GridView3.Rows(I).Cells(4).Text)
                ElseIf GridView3.Rows(I).Cells(6).Text = "PER MT" Then
                    discount = CDec(GridView3.Rows(I).Cells(7).Text) * CDec(GridView3.Rows(I).Cells(19).Text)
                End If
                ''Pack& forwd
                If GridView3.Rows(I).Cells(8).Text = "PERCENTAGE" Then
                    packing_charge = ((basic_price - discount) * CDec(GridView3.Rows(I).Cells(9).Text)) / 100
                ElseIf GridView3.Rows(I).Cells(8).Text = "PER UNIT" Then
                    packing_charge = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                ElseIf GridView3.Rows(I).Cells(8).Text = "PER MT" Then
                    packing_charge = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                End If
                '' Fright calculatation
                If GridView3.Rows(I).Cells(10).Text = "PERCENTAGE" Then
                    Fright = (basic_price * CDec(GridView3.Rows(I).Cells(15).Text)) / 100
                ElseIf GridView3.Rows(I).Cells(10).Text = "PER UNIT" Then
                    Fright = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                ElseIf GridView3.Rows(I).Cells(10).Text = "PER MT" Then
                    Fright = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                End If
                ''Net price
                net_price = (basic_price - discount) + packing_charge
                ''GST on cash
                CGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(12).Text))) / 100
                SGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(13).Text))) / 100
                IGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(14).Text))) / 100
                ''ANAL_VAL calculatation Freight Rs
                ANAL_VAL = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                CESS_VAL = ((net_price + Fright) * CDec(GridView3.Rows(I).Cells(16).Text)) / 100
                priceincl = net_price + Fright + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL

                ''total ORDER value
                total_invoice = priceincl
                landed_cost = net_price + CESS_VAL + Fright
                TextBox83.Text = FormatNumber((CDec(TextBox83.Text) + discount), 2)
                TextBox13.Text = FormatNumber((CDec(TextBox13.Text) + Fright), 2)
                TextBox14.Text = FormatNumber((CDec(TextBox14.Text) + packing_charge), 2)
                TextBox18.Text = FormatNumber(net_price + CDec(TextBox18.Text), 2)
                TextBox20.Text = FormatNumber(SGST_VAL + CDec(TextBox20.Text), 2)
                TextBox19.Text = FormatNumber((CGST_VAL + CDec(TextBox19.Text)), 2)
                TextBox21.Text = FormatNumber((IGST_VAL + CDec(TextBox21.Text)), 2)
                TextBox15.Text = FormatNumber((ANAL_VAL + CDec(TextBox15.Text)), 2)
                TextBox10.Text = FormatNumber((CESS_VAL + CDec(TextBox10.Text)), 2)
                ''
            Next
            TextBox22.Text = FormatNumber(CDec(TextBox18.Text) + CDec(TextBox19.Text) + CDec(TextBox20.Text) + CDec(TextBox21.Text) + CDec(TextBox13.Text) + CDec(TextBox15.Text) + CDec(TextBox10.Text), 2)
        End If
    End Sub

    Protected Sub Button75_Click(sender As Object, e As EventArgs) Handles Button75.Click
        If po_matqty_text0.Text = "" Or IsNumeric(po_matqty_text0.Text) = False Then
            po_matcodecombo0.Focus()
            Return
        ElseIf po_unitrateText.Text = "" Or IsNumeric(po_unitrateText.Text) = False Then
            po_unitrateText.Focus()
            Return
        ElseIf po_pfCombo1.Text = "" Or IsNumeric(po_pfCombo1.Text) = False Then
            po_pfCombo1.Focus()
            Return
        ElseIf po_tradedisText1.Text = "" Or IsNumeric(po_tradedisText1.Text) = False Then
            po_tradedisText1.Focus()
            Return
        ElseIf po_frightText1.Text = "" Or IsNumeric(po_frightText1.Text) = False Then
            po_frightText1.Focus()
            Return
        ElseIf TextBox833.Text = "" Or IsNumeric(TextBox833.Text) = False Then
            TextBox833.Focus()
            Return
        ElseIf TextBox834.Text = "" Or IsNumeric(TextBox834.Text) = False Then
            TextBox834.Focus()
            Return
        ElseIf TextBox835.Text = "" Or IsNumeric(TextBox835.Text) = False Then
            TextBox835.Focus()
            Return
        ElseIf TextBox836.Text = "" Or IsNumeric(TextBox836.Text) = False Then
            TextBox836.Focus()
            Return
        ElseIf TextBox837.Text = "" Or IsNumeric(TextBox837.Text) = False Then
            TextBox837.Focus()
            Return
        ElseIf Delvdate7.Text = "" Or IsDate(Delvdate7.Text) = False Then
            Delvdate7.Focus()
            Return
        ElseIf po_matcodecombo0.Text.IndexOf(",") <> 10 Then
            po_matcodecombo0.Focus()
            Return
        ElseIf po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1)).Length <> 9 Or IsNumeric(po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1))) = False Then
            Label439.Text = "Please Enter Correct Material Code"
            Return
        End If
        ''validation
        Dim po_type As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select PO_TYPE from ORDER_DETAILS WHERE SO_NO = '" & TextBox102.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            po_type = dr.Item("PO_TYPE")
            dr.Close()
        End If
        conn.Close()
        If po_type = "STORE MATERIAL" Then
            If po_matcodecombo0.Text.Substring(0, 1) <> "0" Then
                Label439.Text = "Please Enter Correct Material Code"
                Return
            End If
        ElseIf po_type = "RAW MATERIAL" Then
            If po_matcodecombo0.Text.Substring(0, 1) <> "1" Then
                Label439.Text = "Please Enter Correct Material Code"
                Return
            End If
        End If
        TextBox83.Text = 0.0
        TextBox13.Text = 0.0
        TextBox14.Text = 0.0
        TextBox15.Text = 0.0
        TextBox18.Text = 0.0
        TextBox19.Text = 0.0
        TextBox20.Text = 0.0
        TextBox21.Text = 0.0
        TextBox22.Text = 0.0
        For I As Integer = 0 To DirectCast(ViewState("purchase"), DataTable).Rows.Count - 1
            'If DirectCast(ViewState("purchase"), DataTable).Rows(I)(0) = DropDownList58.SelectedValue And DirectCast(ViewState("purchase"), DataTable).Rows(I)(18) = 0 Then
            If DirectCast(ViewState("purchase"), DataTable).Rows(I)(0) = DropDownList58.SelectedValue Then
                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1)) & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    Label439.Text = "Please Enter Correct Material Code"
                    Return
                End If
                Dim AU As String = ""
                Dim MAT_NAME As String = ""
                conn.Open()
                mycommand.CommandText = "select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1)) & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    AU = dr.Item("MAT_AU")
                    MAT_NAME = dr.Item("MAT_NAME")
                    dr.Close()
                End If
                conn.Close()
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(1) = po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1))
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(2) = MAT_NAME
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(3) = AU
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(4) = po_matqty_text0.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(5) = po_unitrateText.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(6) = DISCOUNT_typeComboBox.SelectedValue
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(7) = po_tradedisText1.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(8) = PF_typeComboBox3.SelectedValue
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(9) = po_pfCombo1.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(10) = po_ed_typeComboBox2.SelectedValue
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(11) = po_frightText1.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(12) = TextBox833.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(13) = TextBox834.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(14) = TextBox835.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(15) = TextBox836.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(16) = TextBox837.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(17) = Delvdate7.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(18) = TextBox733.Text
                DirectCast(ViewState("purchase"), DataTable).Rows(I)(20) = po_frightText2.Text
            End If
        Next
        Me.BINDGRIDpur()
        If GridView3.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView3.Rows.Count - 1
                Dim basic_price, packing_charge, discount, net_price, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, ANAL_VAL, priceincl, Fright, total_invoice, landed_cost As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(5).Text)
                ''Discount
                If GridView3.Rows(I).Cells(6).Text = "PERCENTAGE" Then
                    discount = (CDec(basic_price * CDec(GridView3.Rows(I).Cells(7).Text)) / 100)
                ElseIf GridView3.Rows(I).Cells(6).Text = "PER UNIT" Then
                    discount = CDec(GridView3.Rows(I).Cells(7).Text) * CDec(GridView3.Rows(I).Cells(4).Text)
                ElseIf GridView3.Rows(I).Cells(6).Text = "PER MT" Then
                    discount = CDec(GridView3.Rows(I).Cells(7).Text) * CDec(GridView3.Rows(I).Cells(19).Text)
                End If
                ''Pack& forwd
                If GridView3.Rows(I).Cells(8).Text = "PERCENTAGE" Then
                    packing_charge = ((basic_price - discount) * CDec(GridView3.Rows(I).Cells(9).Text)) / 100
                ElseIf GridView3.Rows(I).Cells(8).Text = "PER UNIT" Then
                    packing_charge = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                ElseIf GridView3.Rows(I).Cells(8).Text = "PER MT" Then
                    packing_charge = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                End If
                '' Fright calculatation
                If GridView3.Rows(I).Cells(10).Text = "PERCENTAGE" Then
                    Fright = (basic_price * CDec(GridView3.Rows(I).Cells(11).Text)) / 100
                ElseIf GridView3.Rows(I).Cells(10).Text = "PER UNIT" Then
                    Fright = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(11).Text))
                ElseIf GridView3.Rows(I).Cells(10).Text = "PER MT" Then
                    Fright = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(11).Text))
                End If
                ''Net price
                net_price = (basic_price - discount) + packing_charge
                ''GST on cash
                CGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(12).Text))) / 100
                SGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(13).Text))) / 100
                IGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(14).Text))) / 100
                ''ANAL_VAL calculatation Freight Rs
                ANAL_VAL = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                CESS_VAL = ((net_price + Fright) * CDec(GridView3.Rows(I).Cells(16).Text)) / 100
                priceincl = net_price + Fright + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL

                ''total ORDER value
                total_invoice = priceincl
                landed_cost = net_price + CESS_VAL + Fright
                TextBox83.Text = FormatNumber((CDec(TextBox83.Text) + discount), 2)
                TextBox13.Text = FormatNumber((CDec(TextBox13.Text) + Fright), 2)
                TextBox14.Text = FormatNumber((CDec(TextBox14.Text) + packing_charge), 2)
                TextBox18.Text = FormatNumber(net_price + CDec(TextBox18.Text), 2)
                TextBox20.Text = FormatNumber(SGST_VAL + CDec(TextBox20.Text), 2)
                TextBox19.Text = FormatNumber((CGST_VAL + CDec(TextBox19.Text)), 2)
                TextBox21.Text = FormatNumber((IGST_VAL + CDec(TextBox21.Text)), 2)
                TextBox15.Text = FormatNumber((ANAL_VAL + CDec(TextBox15.Text)), 2)
                TextBox10.Text = FormatNumber((CESS_VAL + CDec(TextBox10.Text)), 2)
                ''
            Next
            TextBox22.Text = FormatNumber(CDec(TextBox18.Text) + CDec(TextBox19.Text) + CDec(TextBox20.Text) + CDec(TextBox21.Text) + CDec(TextBox13.Text) + CDec(TextBox15.Text) + CDec(TextBox10.Text), 2)
        End If
    End Sub

    Protected Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        If po_matqty_text0.Text = "" Or IsNumeric(po_matqty_text0.Text) = False Then
            po_matqty_text0.Focus()
            Return
        ElseIf po_unitrateText.Text = "" Or IsNumeric(po_unitrateText.Text) = False Then
            po_unitrateText.Focus()
            Return
        ElseIf po_pfCombo1.Text = "" Or IsNumeric(po_pfCombo1.Text) = False Then
            po_pfCombo1.Focus()
            Return
        ElseIf po_tradedisText1.Text = "" Or IsNumeric(po_tradedisText1.Text) = False Then
            po_tradedisText1.Focus()
            Return
        ElseIf po_frightText1.Text = "" Or IsNumeric(po_frightText1.Text) = False Then
            po_frightText1.Focus()
            Return

        ElseIf TextBox833.Text = "" Or IsNumeric(TextBox833.Text) = False Then
            TextBox833.Focus()
            Return
        ElseIf TextBox834.Text = "" Or IsNumeric(TextBox834.Text) = False Then
            TextBox834.Focus()
            Return
        ElseIf TextBox835.Text = "" Or IsNumeric(TextBox835.Text) = False Then
            TextBox835.Focus()
            Return
        ElseIf TextBox836.Text = "" Or IsNumeric(TextBox836.Text) = False Then
            TextBox836.Focus()
            Return
        ElseIf TextBox837.Text = "" Or IsNumeric(TextBox837.Text) = False Then
            TextBox837.Focus()
            Return
        ElseIf Delvdate7.Text = "" Or IsDate(Delvdate7.Text) = False Then
            Delvdate7.Focus()
            Return
        ElseIf po_matcodecombo0.Text.IndexOf(",") <> 10 Then
            Label629.Text = "Please Enter Correct Material details"
            po_matcodecombo0.Text = ""
            po_matcodecombo0.Focus()
            Return

        End If

        'If ((TextBox105.Text <> "D3002") And ((CDec(TextBox833.Text) + CDec(TextBox834.Text) + CDec(TextBox835.Text)) = 0)) Then
        '    Label439.Text = "GST percentage cannot be left blank."
        '    Return
        'End If
        count = 0
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select MAT_AU from MATERIAL where MAT_CODE = '" & po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1)) & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1)).Length <> 9 Or IsNumeric(po_matcodecombo0.Text.Substring(0, (po_matcodecombo0.Text.IndexOf(",") - 1))) = False Or count = 0 Then
            Label629.Text = "Please Enter Correct Material Details"
            po_matcodecombo0.Text = ""
            po_matcodecombo0.Focus()
            Return
        End If
        ''validation
        Dim po_type As String = ""
        conn.Open()
        mycommand.CommandText = "select PO_TYPE from ORDER_DETAILS WHERE SO_NO = '" & TextBox102.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            po_type = dr.Item("PO_TYPE")
            dr.Close()
        End If
        conn.Close()

        If po_type = "STORE MATERIAL" Then
            If po_matcodecombo0.Text.Substring(0, 1) <> "0" Then
                Label629.Text = "Please Enter Correct Material Code"
                po_matcodecombo0.Text = ""
                po_matcodecombo0.Focus()
                Return
            End If
        ElseIf po_type = "RAW MATERIAL" Then
            If po_matcodecombo0.Text.Substring(0, 1) <> "1" Then
                Label629.Text = "Please Enter Correct Material Code"
                po_matcodecombo0.Text = ""
                po_matcodecombo0.Focus()
                Return
            End If
        End If


        ''DELETE EXITING DATA
        Dim order_type As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox102.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")

            dr.Close()
        End If
        conn.Close()


        If order_type <> "Rate Contract" Then
            count = (GridView3.Rows.Count + 1)
        Else
            count = 0
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select PO_NO from PO_ORD_MAT WHERE PO_NO ='" & TextBox102.Text & "'", conn)
            count = da.Fill(dt)
            conn.Close()
            count = count + (GridView3.Rows.Count + 1)
        End If
        ''CALCULATATION

        Dim mat_name, mat_code, AU As New String("")
        mat_name = ""
        mat_code = po_matcodecombo0.Text.Substring(0, po_matcodecombo0.Text.IndexOf(",") - 1)
        conn.Open()
        mc1.CommandText = "select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & mat_code & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            AU = dr.Item("MAT_AU")
            mat_name = dr.Item("MAT_NAME")
            dr.Close()
        End If
        conn.Close()

        Dim dt_table As DataTable = DirectCast(ViewState("purchase"), DataTable)
        dt_table.Rows.Add(count, mat_code, mat_name, AU, po_matqty_text0.Text, po_unitrateText.Text, DISCOUNT_typeComboBox.SelectedValue, po_tradedisText1.Text, PF_typeComboBox3.SelectedValue, po_pfCombo1.Text, po_ed_typeComboBox2.SelectedValue, po_frightText1.Text, TextBox833.Text, TextBox834.Text, TextBox835.Text, TextBox836.Text, TextBox837.Text, Delvdate7.Text, TextBox733.Text, "0.000", po_frightText2.Text)
        ViewState("purchase") = dt_table
        Me.BINDGRIDpur()
        DropDownList58.Items.Clear()
        For I As Integer = 0 To GridView3.Rows.Count - 1
            DropDownList58.Items.Add(GridView3.Rows(I).Cells(0).Text)
        Next
        DropDownList58.Items.Add("Select")
        DropDownList58.SelectedValue = "Select"
        TextBox83.Text = 0.0
        TextBox13.Text = 0.0
        TextBox14.Text = 0.0
        TextBox15.Text = 0.0
        TextBox18.Text = 0.0
        TextBox19.Text = 0.0
        TextBox20.Text = 0.0
        TextBox21.Text = 0.0
        TextBox22.Text = 0.0
        If GridView3.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView3.Rows.Count - 1
                Dim basic_price, packing_charge, discount, net_price, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, ANAL_VAL, priceincl, Fright, total_invoice, landed_cost As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(5).Text)
                ''Discount
                If GridView3.Rows(I).Cells(6).Text = "PERCENTAGE" Then
                    discount = (CDec(basic_price * CDec(GridView3.Rows(I).Cells(7).Text)) / 100)
                ElseIf GridView3.Rows(I).Cells(6).Text = "PER UNIT" Then
                    discount = CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(7).Text)
                ElseIf GridView3.Rows(I).Cells(6).Text = "PER MT" Then
                    discount = CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(7).Text)
                End If
                ''Pack& forwd
                If GridView3.Rows(I).Cells(8).Text = "PERCENTAGE" Then
                    packing_charge = ((basic_price - discount) * CDec(GridView3.Rows(I).Cells(9).Text)) / 100
                ElseIf GridView3.Rows(I).Cells(8).Text = "PER UNIT" Then
                    packing_charge = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                ElseIf GridView3.Rows(I).Cells(8).Text = "PER MT" Then
                    packing_charge = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(9).Text))
                End If
                '' Fright calculatation
                If GridView3.Rows(I).Cells(10).Text = "PERCENTAGE" Then
                    Fright = (basic_price * CDec(GridView3.Rows(I).Cells(11).Text)) / 100
                ElseIf GridView3.Rows(I).Cells(10).Text = "PER UNIT" Then
                    Fright = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(11).Text))
                ElseIf GridView3.Rows(I).Cells(10).Text = "PER MT" Then
                    Fright = (CDec(GridView3.Rows(I).Cells(19).Text) * CDec(GridView3.Rows(I).Cells(11).Text))
                End If
                ''Net price
                net_price = (basic_price - discount) + packing_charge
                ''GST on cash
                CGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(12).Text))) / 100
                SGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(13).Text))) / 100
                IGST_VAL = (((net_price + Fright) * CDec(GridView3.Rows(I).Cells(14).Text))) / 100
                ''ANAL_VAL calculatation Freight Rs
                ANAL_VAL = (CDec(GridView3.Rows(I).Cells(4).Text) * CDec(GridView3.Rows(I).Cells(15).Text))
                CESS_VAL = ((net_price + Fright) * CDec(GridView3.Rows(I).Cells(16).Text)) / 100
                priceincl = net_price + Fright + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL

                ''total ORDER value
                total_invoice = priceincl
                landed_cost = net_price + CESS_VAL + Fright
                TextBox83.Text = FormatNumber((CDec(TextBox83.Text) + discount), 2)
                TextBox13.Text = FormatNumber((CDec(TextBox13.Text) + Fright), 2)
                TextBox14.Text = FormatNumber((CDec(TextBox14.Text) + packing_charge), 2)
                TextBox18.Text = FormatNumber(net_price + CDec(TextBox18.Text), 2)
                TextBox20.Text = FormatNumber(SGST_VAL + CDec(TextBox20.Text), 2)
                TextBox19.Text = FormatNumber((CGST_VAL + CDec(TextBox19.Text)), 2)
                TextBox21.Text = FormatNumber((IGST_VAL + CDec(TextBox21.Text)), 2)
                TextBox15.Text = FormatNumber((ANAL_VAL + CDec(TextBox15.Text)), 2)
                TextBox10.Text = FormatNumber((CESS_VAL + CDec(TextBox10.Text)), 2)
                ''
            Next
            TextBox22.Text = FormatNumber(CDec(TextBox18.Text) + CDec(TextBox19.Text) + CDec(TextBox20.Text) + CDec(TextBox21.Text) + CDec(TextBox13.Text) + CDec(TextBox15.Text) + CDec(TextBox10.Text), 2)
        End If
        po_matcodecombo0.Text = ""
        Label629.Text = ""
    End Sub

    Protected Sub Button58_Click(sender As Object, e As EventArgs) Handles Button58.Click
        ''CHECK MATERIAL AVAILABILITY
        count = 0
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select SO_NO from SO_MAT_ORDER WHERE SO_NO ='" & TextBox723.Text & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If count = 0 Then
            Label438.Text = "Please add material first"
            Return
        Else
            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry
                    ''update order details
                    'conn_trans.Open()
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox723.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()

                    Label438.Text = "Order Submited"
                    myTrans.Commit()
                    conn_trans.Close()
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn_trans.Close()
                    Label438.Text = "There was some Error, please contact EDP."
                Finally

                    conn_trans.Close()
                End Try

            End Using

        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label630.Text = ""
        If TextBox74.Text = "" Then
            TextBox74.Focus()
            Return
        ElseIf TextBox75.Text = "" Then
            TextBox75.Focus()
            Return
        ElseIf TextBox76.Text = "" Or IsNumeric(TextBox76.Text) = False Then
            TextBox76.Focus()
            Return
        ElseIf TextBox77.Text = "" Or IsNumeric(TextBox77.Text) = False Then
            TextBox77.Focus()
            Return
        ElseIf po_pfCombo0.Text = "" Or IsNumeric(po_pfCombo0.Text) = False Then
            po_pfCombo0.Focus()
            Return
        ElseIf po_tradedisText0.Text = "" Or IsNumeric(po_tradedisText0.Text) = False Then
            po_tradedisText0.Focus()
            Return
        ElseIf po_frightText0.Text = "" Or IsNumeric(po_frightText0.Text) = False Then
            po_frightText0.Focus()
            Return
        ElseIf po_frightText0.Text = "" Or IsNumeric(po_frightText0.Text) = False Then
            po_frightText0.Focus()
            Return
        ElseIf TextBox78.Text = "" Or IsNumeric(TextBox78.Text) = False Then
            TextBox78.Focus()
            Return
        ElseIf IsDate(Delvdate5.Text) = False Then
            Delvdate5.Focus()
            Return
        ElseIf TextBox80.Text = "" Or IsNumeric(TextBox80.Text) = False Then
            TextBox80.Focus()
            Return
        ElseIf DropDownList12.Text.IndexOf(",") <> 10 Then
            Label630.Text = "Please Enter Correct Material details"
            DropDownList12.Text = ""
            DropDownList12.Focus()
            Return
        End If


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry

                Dim dt1 As New DataTable
                count = 0
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select MAT_AU from MATERIAL where MAT_CODE = '" & DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)) & "'", conn)
                count = da.Fill(dt1)
                conn.Close()
                If DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1)).Length <> 9 Or IsNumeric(DropDownList12.Text.Substring(0, (DropDownList12.Text.IndexOf(",") - 1))) = False Or count = 0 Then
                    Label630.Text = "Please Enter Correct Material Details"
                    DropDownList12.Text = ""
                    DropDownList12.Focus()
                    Return
                End If
                count = 0
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select * from SO_MAT_ORDER where SO_NO ='" & TextBox723.Text & "' and ITEM_SLNO ='" & TextBox74.Text & "'", conn)
                count = da.Fill(dt1)
                conn.Close()
                If count > 0 Then
                    Label630.Text = "Please Enter Correct Material Details"
                    TextBox74.Text = ""
                    TextBox74.Focus()
                    Return
                End If


                ''SAVE
                Dim SALE_TYPE As String = ""
                If cgst_textbox.Enabled = False And sgst_textbox.Enabled = False And igst_textbox.Enabled = False Then
                    SALE_TYPE = "D"
                Else
                    SALE_TYPE = "T"
                End If


                Dim QUARY1 As String = ""
                QUARY1 = "Insert Into SO_MAT_ORDER(SO_NO ,ITEM_VOCAB ,ITEM_SLNO ,ITEM_CODE ,ORD_AU ,ITEM_QTY ,ITEM_MT ,ITEM_UNIT_RATE ,ITEM_PACK ,PACK_TYPE ,ITEM_DISCOUNT ,DISC_TYPE ,ITEM_CGST ,ITEM_SGST ,ITEM_IGST ,ITEM_TERMINAL_TAX ,ITEM_TCS ,ITEM_FREIGHT_PU ,ITEM_FREIGHT_TYPE ,ITEM_DELIVERY,ITEM_QTY_SEND,ITEM_S_TAX,ITEM_STATUS,AMD_NO,AMD_DATE,ITEM_DETAILS,ITEM_WEIGHT,SALE_TYPE,ITEM_CESS)values(@SO_NO ,@ITEM_VOCAB ,@ITEM_SLNO ,@ITEM_CODE ,@ORD_AU ,@ITEM_QTY ,@ITEM_MT ,@ITEM_UNIT_RATE ,@ITEM_PACK ,@PACK_TYPE ,@ITEM_DISCOUNT ,@DISC_TYPE ,@ITEM_CGST ,@ITEM_SGST ,@ITEM_IGST ,@ITEM_TERMINAL_TAX ,@ITEM_TCS ,@ITEM_FREIGHT_PU ,@ITEM_FREIGHT_TYPE ,@ITEM_DELIVERY,@ITEM_QTY_SEND,@ITEM_S_TAX,@ITEM_STATUS,@AMD_NO,@AMD_DATE,@ITEM_DETAILS,@ITEM_WEIGHT,@SALE_TYPE,@ITEM_CESS)"
                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@SO_NO", TextBox723.Text)
                cmd1.Parameters.AddWithValue("@ITEM_VOCAB", TextBox75.Text)
                cmd1.Parameters.AddWithValue("@ITEM_SLNO", TextBox74.Text)
                cmd1.Parameters.AddWithValue("@ITEM_CODE", DropDownList12.Text.Substring(0, DropDownList12.Text.IndexOf(",") - 1))
                cmd1.Parameters.AddWithValue("@ORD_AU", TextBox79.Text)
                cmd1.Parameters.AddWithValue("@ITEM_QTY", CDec(TextBox76.Text))
                cmd1.Parameters.AddWithValue("@ITEM_MT", 0.0)
                cmd1.Parameters.AddWithValue("@ITEM_UNIT_RATE", CDec(TextBox77.Text))
                cmd1.Parameters.AddWithValue("@ITEM_PACK", CDec(po_pfCombo0.Text))
                cmd1.Parameters.AddWithValue("@PACK_TYPE", DropDownList63.SelectedValue)
                cmd1.Parameters.AddWithValue("@ITEM_DISCOUNT", CDec(po_tradedisText0.Text))
                cmd1.Parameters.AddWithValue("@DISC_TYPE", DropDownList62.SelectedValue)
                cmd1.Parameters.AddWithValue("@ITEM_CGST", CDec(cgst_textbox.Text))
                cmd1.Parameters.AddWithValue("@ITEM_SGST", CDec(sgst_textbox.Text))
                cmd1.Parameters.AddWithValue("@ITEM_IGST", CDec(igst_textbox.Text))
                cmd1.Parameters.AddWithValue("@ITEM_TERMINAL_TAX", CDec(TextBox78.Text))
                cmd1.Parameters.AddWithValue("@ITEM_TCS", CDec(TextBox80.Text))
                cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_TYPE", DropDownList1.Text)
                cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_PU", CDec(po_frightText0.Text))
                cmd1.Parameters.AddWithValue("@ITEM_DELIVERY", Date.ParseExact(Delvdate5.Text, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@ITEM_QTY_SEND", 0.0)
                cmd1.Parameters.AddWithValue("@ITEM_S_TAX", 0.0)
                cmd1.Parameters.AddWithValue("@ITEM_STATUS", "PENDING")
                cmd1.Parameters.AddWithValue("@ITEM_WEIGHT", 0.0)
                cmd1.Parameters.AddWithValue("@AMD_NO", "0")
                cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(TextBox724.Text, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@ITEM_DETAILS", TextBox728.Text)
                cmd1.Parameters.AddWithValue("@SALE_TYPE", SALE_TYPE)
                cmd1.Parameters.AddWithValue("@ITEM_CESS", CDec(cess_textbox.Text))
                cmd1.ExecuteReader()
                cmd1.Dispose()

                TextBox74.Text = ""
                myTrans.Commit()
                conn_trans.Close()
                Label630.Text = "Data Saved Succsessfully"

            Catch ee As Exception
                'Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                Label630.Text = "There was some Error, please contact EDP."

            Finally
                conn_trans.Close()
            End Try

        End Using


    End Sub



    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub Button77_Click(sender As Object, e As EventArgs) Handles Button77.Click
        Dim order_type As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox817.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")

            dr.Close()
        End If
        conn.Close()
        If order_type = "Rate Contract" Then
            Label637.Text = "This is a rate contract can't be submited"
            Return
        End If
        ''CHECK MATERIAL AVAILABILITY

        count = 0
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select PO_NO from PO_ORD_MAT WHERE PO_NO ='" & TextBox817.Text & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If count = 0 Then
            Label637.Text = "Please add material first"
            Return
        Else
            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry

                    ''update order details
                    'conn_trans.Open()
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox817.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()

                    Label637.Text = "Order Submited"
                    myTrans.Commit()
                    conn_trans.Close()
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn_trans.Close()
                    Label637.Text = "There was some Error, please contact EDP."
                Finally
                    conn_trans.Close()
                End Try

            End Using

        End If
    End Sub

    Protected Sub Button78_Click(sender As Object, e As EventArgs) Handles Button78.Click


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                ''DELETE EXITING DATA
                Dim order_type As String = ""
                conn.Open()
                Dim mc1 As New SqlCommand("select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox102.Text & "'", conn)
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")

                    dr.Close()
                End If
                conn.Close()

                If order_type <> "Rate Contract" Then

                    mycommand = New SqlCommand("DELETE FROM PO_ORD_MAT WHERE PO_NO ='" & TextBox102.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()

                ElseIf order_type = "Rate Contract" Then

                    mycommand = New SqlCommand("update ORDER_DETAILS set SO_STATUS='RC' where SO_NO ='" & TextBox102.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteReader()
                    mycommand.Dispose()

                End If

                ''DATA SAVE IN PO_ORD_MAT
                Dim ENTRY_TAX As Decimal = 1

                For I = 0 To GridView216.Rows.Count - 1
                    Dim po_slno As String = TextBox817.Text
                    Dim sno As Integer = GridView216.Rows(I).Cells(0).Text
                    Dim matcode As String = GridView216.Rows(I).Cells(1).Text
                    Dim matname As String = GridView216.Rows(I).Cells(2).Text
                    Dim matqty As String = GridView216.Rows(I).Cells(4).Text
                    Dim urate As String = GridView216.Rows(I).Cells(5).Text
                    Dim ddate As String = CDate(GridView216.Rows(I).Cells(6).Text.Trim)
                    Dim mat_details As String = GridView216.Rows(I).Cells(7).Text.Trim
                    If mat_details = "&nbsp;" Then
                        mat_details = ""
                    End If



                    Dim query As String = "Insert Into PO_ORD_MAT(HSN_CODE,MAT_DESC,PO_NO, MAT_SLNO,MAT_CODE,MAT_NAME,MAT_QTY,MAT_UNIT_RATE,PF_TYPE,MAT_PACK,DISC_TYPE,MAT_DISCOUNT,SGST,CGST,IGST,CESS,ANAL_TAX,FREIGHT_TYPE,MAT_FREIGHT_PU,MAT_DELIVERY,MAT_QTY_RCVD,MAT_STATUS,AMD_NO,AMD_DATE,TOTAL_WT,TDS_SGST,TDS_CGST,TDS_IGST) values (@HSN_CODE,@MAT_DESC,@PO_NO, @MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_QTY,@MAT_UNIT_RATE,@PF_TYPE,@MAT_PACK,@DISC_TYPE,@MAT_DISCOUNT,@SGST,@CGST,@IGST,@CESS,@ANAL_TAX,@FREIGHT_TYPE,@MAT_FREIGHT_PU,@MAT_DELIVERY,@MAT_QTY_RCVD,@MAT_STATUS,@AMD_NO,@AMD_DATE,@TOTAL_WT,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                    cmd.Parameters.AddWithValue("@MAT_SLNO", sno)
                    cmd.Parameters.AddWithValue("@MAT_CODE", matcode)
                    cmd.Parameters.AddWithValue("@MAT_NAME", matname)
                    cmd.Parameters.AddWithValue("@MAT_QTY", matqty)
                    cmd.Parameters.AddWithValue("@MAT_UNIT_RATE", urate)
                    cmd.Parameters.AddWithValue("@PF_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_PACK", 0)
                    cmd.Parameters.AddWithValue("@DISC_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_DISCOUNT", 0)
                    cmd.Parameters.AddWithValue("@CGST", 0)
                    cmd.Parameters.AddWithValue("@SGST", 0)
                    cmd.Parameters.AddWithValue("@IGST", 0)
                    cmd.Parameters.AddWithValue("@CESS", 0)
                    cmd.Parameters.AddWithValue("@ANAL_TAX", 0)
                    cmd.Parameters.AddWithValue("@FREIGHT_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_FREIGHT_PU", 0)
                    cmd.Parameters.AddWithValue("@MAT_DELIVERY", Date.ParseExact(ddate, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@MAT_QTY_RCVD", 0.0)
                    cmd.Parameters.AddWithValue("@MAT_STATUS", "Pending")
                    cmd.Parameters.AddWithValue("@MAT_DESC", mat_details)
                    cmd.Parameters.AddWithValue("@AMD_NO", "0")
                    cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox818.Text), "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@TOTAL_WT", 0)
                    cmd.Parameters.AddWithValue("@TDS_SGST", 0.00)
                    cmd.Parameters.AddWithValue("@TDS_CGST", 0.00)
                    cmd.Parameters.AddWithValue("@TDS_IGST", 0.00)
                    cmd.Parameters.AddWithValue("@HSN_CODE", TextBox58.Text)
                    cmd.ExecuteReader()
                    cmd.Dispose()

                Next
                ''update order details

                Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=" & GridView216.Rows.Count & " , FULL_VALUE= " & CDec(TextBox824.Text) & ", PO_BASE_VALUE= " & CDec(TextBox824.Text) & " where SO_NO ='" & TextBox817.Text & "'", conn_trans, myTrans)
                cmd2.ExecuteReader()
                cmd2.Dispose()



                Dim dt_fgn As New DataTable()
                dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
                ViewState("foreign") = dt_fgn
                Me.BINDGRIDFGN()
                myTrans.Commit()
                conn_trans.Close()
                Label637.Text = "Data Saved Succsessfully"
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                Label637.Text = "There was some Error, please contact EDP."
            Finally
                conn_trans.Close()
            End Try



        End Using


    End Sub

    Protected Sub Button79_Click(sender As Object, e As EventArgs) Handles Button79.Click
        Dim dt_fgn As New DataTable()
        dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
        ViewState("foreign") = dt_fgn
        Me.BINDGRIDFGN()

        MultiView1.ActiveViewIndex = 0
    End Sub



    Protected Sub Button82_Click(sender As Object, e As EventArgs) Handles Button82.Click
        If po_matcodecombo1.Text = "" Then
            po_matcodecombo1.Focus()
            Return
        ElseIf po_matqty_text1.Text = "" Or IsNumeric(po_matqty_text1.Text) = False Then
            po_matqty_text1.Focus()
            Return
        ElseIf po_unitrateText0.Text = "" Or IsNumeric(po_unitrateText0.Text) = False Then
            po_unitrateText0.Focus()
            Return
        ElseIf Delvdate8.Text = "" Or IsDate(Delvdate8.Text) = False Then
            Delvdate8.Focus()
            Return
        ElseIf po_matcodecombo1.Text.IndexOf(",") <> 10 Then
            po_matcodecombo1.Text = ""
            po_matcodecombo1.Focus()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select MAT_AU from MATERIAL where MAT_CODE = '" & po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1)) & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1)).Length <> 9 Or IsNumeric(po_matcodecombo1.Text.Substring(0, (po_matcodecombo1.Text.IndexOf(",") - 1))) = False Or count = 0 Then
            po_matcodecombo1.Text = ""
            po_matcodecombo1.Focus()
            Return
        End If
        ''validation
        Dim po_type As String = ""
        conn.Open()
        mycommand.CommandText = "select PO_TYPE from ORDER_DETAILS WHERE SO_NO = '" & TextBox817.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            po_type = dr.Item("PO_TYPE")
            dr.Close()
        End If
        conn.Close()
        ''SERIAL NO SEARCH
        count = (GridView216.Rows.Count + 1)
        ''CALCULATATION
        Dim AU As String
        AU = "N/A"
        Dim mat_name, mat_code As String
        mat_name = ""
        mat_code = po_matcodecombo1.Text.Substring(0, po_matcodecombo1.Text.IndexOf(",") - 1)
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & mat_code & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            AU = dr.Item("MAT_AU")
            mat_name = dr.Item("MAT_NAME")
            dr.Close()
        End If
        conn.Close()
        Dim dt_table As DataTable = DirectCast(ViewState("foreign"), DataTable)
        dt_table.Rows.Add(count, mat_code, mat_name, AU, po_matqty_text1.Text, po_unitrateText0.Text, Delvdate8.Text, TextBox822.Text)
        ViewState("foreign") = dt_table
        Me.BINDGRIDFGN()
        DropDownList60.Items.Clear()
        For I As Integer = 0 To GridView216.Rows.Count - 1
            DropDownList60.Items.Add(GridView216.Rows(I).Cells(0).Text)
        Next
        DropDownList60.Items.Add("Select")
        DropDownList60.SelectedValue = "Select"
        TextBox831.Text = 0.0
        TextBox824.Text = 0.0

        If GridView216.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView216.Rows.Count - 1
                Dim basic_price As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView216.Rows(I).Cells(4).Text) * CDec(GridView216.Rows(I).Cells(5).Text)
                TextBox824.Text = basic_price + CDec(TextBox824.Text)
                TextBox831.Text = basic_price + CDec(TextBox831.Text)
            Next
        End If
        po_matcodecombo1.Text = ""
    End Sub

    Protected Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                ''CHECK MATERIAL AVAILABILITY
                Dim ORD_TYPE As New String("")
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "SELECT ORDER_TYPE FROM ORDER_DETAILS where SO_NO='" & TextBox125.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORD_TYPE = dr.Item("ORDER_TYPE")
                    dr.Close()
                End If
                conn.Close()
                If (ORD_TYPE = "Rate Contract") Then
                    count = 0
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("select PO_NO from WO_ORDER WHERE PO_NO ='" & TextBox125.Text & "'", conn)
                    count = da.Fill(dt)
                    conn.Close()
                    If count = 0 Then
                        Label441.Text = "Please Add Some Work First"
                        Return
                    Else
                        ''update order details

                        Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='RCW' where SO_NO ='" & TextBox125.Text & "'", conn_trans, myTrans)
                        cmd2.ExecuteReader()
                        cmd2.Dispose()

                        Label441.Text = "Order Submited"
                    End If
                Else
                    count = 0
                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("select PO_NO from WO_ORDER WHERE PO_NO ='" & TextBox125.Text & "'", conn)
                    count = da.Fill(dt)
                    conn.Close()
                    If count = 0 Then
                        Label441.Text = "Please Add Some Work First"
                        Return
                    Else
                        ''update order details

                        Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox125.Text & "'", conn_trans, myTrans)
                        cmd2.ExecuteReader()
                        cmd2.Dispose()

                        Label441.Text = "Order Submited"
                    End If
                End If

                myTrans.Commit()
                conn_trans.Close()

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label441.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        If GridView4.Rows.Count = 0 Then
            TextBox641.Focus()
            Label441.Text = "Please add Data first"
            Return
        End If

        'Dim myTrans As SqlTransaction
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                ''CHECKING THE UPPER CAP LIMIT OF THE RATE CONTRACT
                Dim ORD_AMT As New Decimal(0)
                Dim UPTO_AMT As New Decimal(0)
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "SELECT ORD_AMOUNT FROM RATE_CONTRACT WHERE PO_NO= '" & TextBox125.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORD_AMT = dr.Item("ORD_AMOUNT")
                    dr.Close()
                End If
                conn.Close()
                conn.Open()
                'Dim mc1 As New SqlCommand
                'mc1.CommandText = "SELECT SUM(T_VALUE) AS T_VALUE FROM WO_ORDER where PO_NO= '" & TextBox125.Text & "'"
                ''mc1.CommandText = "select sum(NET_AMT) AS T_VALUE from VOUCHER where PO_NO='" & TextBox125.Text & "'"
                mc1.CommandText = "select sum(prov_amt) AS T_VALUE from mb_book where PO_NO='" & TextBox125.Text & "'"
                'mc1.CommandText = "select sum(TOTAL_PRICE) AS T_VALUE from MAT_DETAILS where COST_CODE='" & TextBox125.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If IsDBNull(dr.Item("T_VALUE")) Then
                        UPTO_AMT = 0
                    Else
                        UPTO_AMT = dr.Item("T_VALUE")
                    End If

                    dr.Close()
                End If
                conn.Close()

                Dim order_type As String = ""
                conn.Open()
                'Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox125.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")

                    dr.Close()
                End If
                conn.Close()

                Dim TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0)

                If (order_type <> "Rate Contract") Then

                    mycommand = New SqlCommand("DELETE FROM WO_ORDER WHERE PO_NO ='" & TextBox125.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()


                    Dim I As Integer
                    ''  Try
                    For I = 0 To GridView4.Rows.Count - 1
                        Dim po_slno As String = TextBox125.Text
                        Dim sno As String = GridView4.Rows(I).Cells(0).Text
                        Dim wo_type As String = GridView4.Rows(I).Cells(1).Text
                        Dim WORK_NAME As String = GridView4.Rows(I).Cells(2).Text
                        Dim QUANTITY As String = GridView4.Rows(I).Cells(3).Text
                        Dim A_U As String = GridView4.Rows(I).Cells(4).Text
                        Dim urate As String = GridView4.Rows(I).Cells(5).Text
                        Dim USASE_MAT As String = GridView4.Rows(I).Cells(6).Text
                        Dim location As String = GridView4.Rows(I).Cells(7).Text
                        Dim start_date As String = GridView4.Rows(I).Cells(8).Text
                        Dim end_date As String = GridView4.Rows(I).Cells(9).Text
                        Dim tolerance As String = GridView4.Rows(I).Cells(10).Text
                        Dim DISCOUNT As String = GridView4.Rows(I).Cells(11).Text
                        Dim sgst As String = GridView4.Rows(I).Cells(12).Text
                        Dim cgst As String = GridView4.Rows(I).Cells(13).Text
                        Dim igst As String = GridView4.Rows(I).Cells(14).Text
                        Dim cess As Decimal = GridView4.Rows(I).Cells(15).Text
                        Dim WO_FOR As String = GridView4.Rows(I).Cells(16).Text
                        Dim t_value As Decimal = GridView4.Rows(I).Cells(17).Text

                        ''TDS CHECK TDS ON GST CRITERIA
                        If CDec(TextBox681.Text) >= 250000 Then
                            If TextBox839.Text = "0.00" Then
                                TDS_SGST = 1.0
                                TDS_CGST = 1.0
                                TDS_IGST = 0.00
                            Else
                                TDS_SGST = 0.00
                                TDS_CGST = 0.00
                                TDS_IGST = 2.0
                            End If
                        End If


                        Dim query As String = "Insert Into WO_ORDER(sgst,cgst,igst,cess,t_value,WO_FOR,AMD_DATE,WO_TYPE,PO_NO, SUPL_ID, W_SLNO,W_NAME,W_AU,W_UNIT_PRICE,W_QTY,W_DISCOUNT,W_MATERIAL_COST,W_TOLERANCE,W_AREA,W_START_DATE,W_END_DATE,W_COMPLITED,W_STATUS,WO_AMD,TDS_SGST,TDS_CGST,TDS_IGST) values (@sgst,@cgst,@igst,@cess,@t_value,@WO_FOR,@AMD_DATE,@WO_TYPE,@PO_NO, @SUPL_ID,@W_SLNO,@W_NAME,@W_AU,@W_UNIT_PRICE,@W_QTY,@W_DISCOUNT,@W_MATERIAL_COST,@W_TOLERANCE,@W_AREA,@W_START_DATE,@W_END_DATE,@W_COMPLITED,@W_STATUS,@WO_AMD,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                        cmd.Parameters.AddWithValue("@WO_TYPE", wo_type)
                        cmd.Parameters.AddWithValue("@SUPL_ID", TextBox128.Text)
                        cmd.Parameters.AddWithValue("@W_SLNO", sno)
                        cmd.Parameters.AddWithValue("@W_NAME", WORK_NAME)
                        cmd.Parameters.AddWithValue("@W_AU", A_U)
                        cmd.Parameters.AddWithValue("@W_UNIT_PRICE", urate)
                        cmd.Parameters.AddWithValue("@W_QTY", QUANTITY)
                        cmd.Parameters.AddWithValue("@W_DISCOUNT", DISCOUNT)
                        cmd.Parameters.AddWithValue("@W_MATERIAL_COST", USASE_MAT)
                        cmd.Parameters.AddWithValue("@W_TOLERANCE", tolerance)
                        cmd.Parameters.AddWithValue("@W_AREA", location)
                        cmd.Parameters.AddWithValue("@W_START_DATE", DateValue(start_date))
                        cmd.Parameters.AddWithValue("@W_END_DATE", DateValue(end_date))
                        cmd.Parameters.AddWithValue("@W_COMPLITED", 0.0)
                        cmd.Parameters.AddWithValue("@W_STATUS", "PENDING")
                        cmd.Parameters.AddWithValue("@WO_AMD", "NA")
                        cmd.Parameters.AddWithValue("@AMD_DATE", DateValue(TextBox126.Text))
                        cmd.Parameters.AddWithValue("@sgst", sgst)
                        cmd.Parameters.AddWithValue("@cgst", cgst)
                        cmd.Parameters.AddWithValue("@igst", igst)
                        cmd.Parameters.AddWithValue("@cess", cess)
                        cmd.Parameters.AddWithValue("@WO_FOR", WO_FOR)
                        cmd.Parameters.AddWithValue("@t_value", t_value)
                        cmd.Parameters.AddWithValue("@TDS_SGST", TDS_SGST)
                        cmd.Parameters.AddWithValue("@TDS_CGST", TDS_CGST)
                        cmd.Parameters.AddWithValue("@TDS_IGST", TDS_IGST)
                        cmd.ExecuteReader()
                        cmd.Dispose()

                    Next
                    ''update order details

                    'Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=" & GridView4.Rows.Count & " , FULL_VALUE= " & CDec(TextBox721.Text) & ", PO_BASE_VALUE= " & CDec(TextBox681.Text) & " where SO_NO ='" & TextBox125.Text & "'", conn_trans, myTrans)
                    'cmd2.ExecuteReader()
                    'cmd2.Dispose()

                    Dim dt_wo As New DataTable()
                    dt_wo.Columns.AddRange(New DataColumn(17) {New DataColumn("W_SLNO"), New DataColumn("TAX_TYPE"), New DataColumn("W_NAME"), New DataColumn("W_QTY"), New DataColumn("W_AU"), New DataColumn("W_UNIT_PRICE"), New DataColumn("W_MATERIAL_COST"), New DataColumn("W_AREA"), New DataColumn("W_START_DATE"), New DataColumn("W_END_DATE"), New DataColumn("W_TOLERANCE"), New DataColumn("W_DISCOUNT"), New DataColumn("SGST"), New DataColumn("CGST"), New DataColumn("IGST"), New DataColumn("CESS"), New DataColumn("wo_type"), New DataColumn("t_value")})
                    ViewState("WORK") = dt_wo
                    Me.BINDGRIDWORK()
                    Label441.Text = "Order Saved"
                    Button59.Enabled = True
                    myTrans.Commit()
                    conn_trans.Close()

                Else
                    If ((ORD_AMT - UPTO_AMT) >= CDec(TextBox681.Text)) Then

                        Button59.Enabled = True
                        mycommand = New SqlCommand("update ORDER_DETAILS set SO_STATUS='RCW',NO_OF_ITEM=" & GridView4.Rows.Count & " where SO_NO ='" & TextBox125.Text & "'", conn_trans, myTrans)
                        mycommand.ExecuteReader()
                        mycommand.Dispose()

                        Dim I As Integer
                        ''  Try
                        For I = 0 To GridView4.Rows.Count - 1
                            Dim po_slno As String = TextBox125.Text
                            Dim sno As String = GridView4.Rows(I).Cells(0).Text
                            Dim wo_type As String = GridView4.Rows(I).Cells(1).Text
                            Dim WORK_NAME As String = GridView4.Rows(I).Cells(2).Text
                            Dim QUANTITY As String = GridView4.Rows(I).Cells(3).Text
                            Dim A_U As String = GridView4.Rows(I).Cells(4).Text
                            Dim urate As String = GridView4.Rows(I).Cells(5).Text
                            Dim USASE_MAT As String = GridView4.Rows(I).Cells(6).Text
                            Dim location As String = GridView4.Rows(I).Cells(7).Text
                            Dim start_date As String = GridView4.Rows(I).Cells(8).Text
                            Dim end_date As String = GridView4.Rows(I).Cells(9).Text
                            Dim tolerance As String = GridView4.Rows(I).Cells(10).Text
                            Dim DISCOUNT As String = GridView4.Rows(I).Cells(11).Text
                            Dim sgst As String = GridView4.Rows(I).Cells(12).Text
                            Dim cgst As String = GridView4.Rows(I).Cells(13).Text
                            Dim igst As String = GridView4.Rows(I).Cells(14).Text
                            Dim cess As Decimal = GridView4.Rows(I).Cells(15).Text
                            Dim WO_FOR As String = GridView4.Rows(I).Cells(16).Text
                            Dim t_value As Decimal = GridView4.Rows(I).Cells(17).Text

                            ''TDS CHECK TDS ON GST CRITERIA
                            If ORD_AMT >= 250000 Then
                                If CDec(TextBox839.Text) > 0 Then
                                    TDS_SGST = 0.00
                                    TDS_CGST = 0.00
                                    TDS_IGST = 2.0

                                Else
                                    TDS_SGST = 1.0
                                    TDS_CGST = 1.0
                                    TDS_IGST = 0.00
                                End If
                            End If

                            Dim query As String = "Insert Into WO_ORDER(HSN_CODE,sgst,cgst,igst,cess,t_value,WO_FOR,AMD_DATE,WO_TYPE,PO_NO, SUPL_ID, W_SLNO,W_NAME,W_AU,W_UNIT_PRICE,W_QTY,W_DISCOUNT,W_MATERIAL_COST,W_TOLERANCE,W_AREA,W_START_DATE,W_END_DATE,W_COMPLITED,W_STATUS,WO_AMD,TDS_SGST,TDS_CGST,TDS_IGST) values (@HSN_CODE,@sgst,@cgst,@igst,@cess,@t_value,@WO_FOR,@AMD_DATE,@WO_TYPE,@PO_NO, @SUPL_ID,@W_SLNO,@W_NAME,@W_AU,@W_UNIT_PRICE,@W_QTY,@W_DISCOUNT,@W_MATERIAL_COST,@W_TOLERANCE,@W_AREA,@W_START_DATE,@W_END_DATE,@W_COMPLITED,@W_STATUS,@WO_AMD,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                            Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                            cmd.Parameters.AddWithValue("@WO_TYPE", wo_type)
                            cmd.Parameters.AddWithValue("@SUPL_ID", TextBox128.Text)
                            cmd.Parameters.AddWithValue("@W_SLNO", sno)
                            cmd.Parameters.AddWithValue("@W_NAME", WORK_NAME)
                            cmd.Parameters.AddWithValue("@W_AU", A_U)
                            cmd.Parameters.AddWithValue("@W_UNIT_PRICE", urate)
                            cmd.Parameters.AddWithValue("@W_QTY", QUANTITY)
                            cmd.Parameters.AddWithValue("@W_DISCOUNT", DISCOUNT)
                            cmd.Parameters.AddWithValue("@W_MATERIAL_COST", USASE_MAT)
                            cmd.Parameters.AddWithValue("@W_TOLERANCE", tolerance)
                            cmd.Parameters.AddWithValue("@W_AREA", location)
                            cmd.Parameters.AddWithValue("@W_START_DATE", DateValue(start_date))
                            cmd.Parameters.AddWithValue("@W_END_DATE", DateValue(end_date))
                            cmd.Parameters.AddWithValue("@W_COMPLITED", 0.0)
                            cmd.Parameters.AddWithValue("@W_STATUS", "PENDING")
                            cmd.Parameters.AddWithValue("@WO_AMD", "NA")
                            cmd.Parameters.AddWithValue("@AMD_DATE", DateValue(TextBox126.Text))
                            cmd.Parameters.AddWithValue("@sgst", sgst)
                            cmd.Parameters.AddWithValue("@cgst", cgst)
                            cmd.Parameters.AddWithValue("@igst", igst)
                            cmd.Parameters.AddWithValue("@cess", cess)
                            cmd.Parameters.AddWithValue("@WO_FOR", WO_FOR)
                            cmd.Parameters.AddWithValue("@t_value", t_value)
                            cmd.Parameters.AddWithValue("@TDS_SGST", TDS_SGST)
                            cmd.Parameters.AddWithValue("@TDS_CGST", TDS_CGST)
                            cmd.Parameters.AddWithValue("@TDS_IGST", TDS_IGST)
                            cmd.Parameters.AddWithValue("@HSN_CODE", TextBox67.Text)
                            cmd.ExecuteReader()
                            cmd.Dispose()

                        Next
                        ''update order details

                        'Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=" & GridView4.Rows.Count & " where SO_NO ='" & TextBox125.Text & "'", conn_trans, myTrans)
                        'cmd2.ExecuteReader()
                        'cmd2.Dispose()

                        Dim dt_wo As New DataTable()
                        dt_wo.Columns.AddRange(New DataColumn(17) {New DataColumn("W_SLNO"), New DataColumn("TAX_TYPE"), New DataColumn("W_NAME"), New DataColumn("W_QTY"), New DataColumn("W_AU"), New DataColumn("W_UNIT_PRICE"), New DataColumn("W_MATERIAL_COST"), New DataColumn("W_AREA"), New DataColumn("W_START_DATE"), New DataColumn("W_END_DATE"), New DataColumn("W_TOLERANCE"), New DataColumn("W_DISCOUNT"), New DataColumn("SGST"), New DataColumn("CGST"), New DataColumn("IGST"), New DataColumn("CESS"), New DataColumn("wo_type"), New DataColumn("t_value")})
                        ViewState("WORK") = dt_wo
                        Me.BINDGRIDWORK()

                        myTrans.Commit()
                        conn_trans.Close()
                        Label441.Text = "Order Saved Succsessfully"

                    Else

                        Button59.Enabled = False
                        Label441.Text = "Total amount is exceeding the rate contract balance amount Rs " & (ORD_AMT - UPTO_AMT)
                        Label441.ForeColor = Drawing.Color.Red
                        myTrans.Rollback()
                        conn.Close()
                        conn_trans.Close()

                    End If
                End If

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label441.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using



    End Sub

    Protected Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click
        Dim dt_wo As New DataTable()
        dt_wo.Columns.AddRange(New DataColumn(17) {New DataColumn("W_SLNO"), New DataColumn("TAX_TYPE"), New DataColumn("W_NAME"), New DataColumn("W_QTY"), New DataColumn("W_AU"), New DataColumn("W_UNIT_PRICE"), New DataColumn("W_MATERIAL_COST"), New DataColumn("W_AREA"), New DataColumn("W_START_DATE"), New DataColumn("W_END_DATE"), New DataColumn("W_TOLERANCE"), New DataColumn("W_DISCOUNT"), New DataColumn("SGST"), New DataColumn("CGST"), New DataColumn("IGST"), New DataColumn("CESS"), New DataColumn("wo_type"), New DataColumn("t_value")})
        ViewState("WORK") = dt_wo
        Me.BINDGRIDWORK()
        TextBox681.Text = 0.0
        TextBox691.Text = 0.0
        TextBox701.Text = 0.0
        TextBox711.Text = 0.0
        TextBox721.Text = 0.0

    End Sub

    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        If TextBox621.Text = "" Then
            TextBox621.Focus()
            Label441.Text = "Please Enter Desc. Of Job"
            Return
        ElseIf DropDownList46.SelectedValue = "Select" Then
            DropDownList46.Focus()
            Label441.Text = "Please Select Taxable Service"
            Return
        ElseIf TextBox641.Text = "" Then
            TextBox641.Focus()
            Label441.Text = "Please Enter Job Location"
            Return
        ElseIf TextBox561.Text = "" Or IsNumeric(TextBox561.Text) = False Then
            TextBox561.Focus()
            Label441.Text = "Please Enter Order Unit"
            Return
        ElseIf TextBox581.Text = "" Or IsNumeric(TextBox581.Text) = False Then
            TextBox581.Focus()
            Label441.Text = "Please Enter Service Charge P/U"
            Return
        ElseIf TextBox651.Text = "" Or IsDate(TextBox651.Text) = False Then
            TextBox651.Focus()
            TextBox651.Text = ""
            Label441.Text = "Please Enter Job Start Date"
            Return
        ElseIf TextBox661.Text = "" Or IsDate(TextBox661.Text) = False Then
            TextBox661.Focus()
            TextBox661.Text = ""
            Label441.Text = "Please Enter Job end Date"
            Return
        ElseIf TextBox571.Text = "" Then
            TextBox571.Focus()
            Label441.Text = "Please Enter A/U"
            Return
        ElseIf TextBox591.Text = "" Or IsNumeric(TextBox591.Text) = False Then
            TextBox591.Focus()
            Label441.Text = "Please Enter Any Mat. Cost Other wise 0 "
            Return
        ElseIf TextBox601.Text = "" Or IsNumeric(TextBox601.Text) = False Then
            TextBox601.Focus()
            Label441.Text = "Please Enter Discount % Other wise 0 "
            Return
        ElseIf TextBox611.Text = "" Or IsNumeric(TextBox611.Text) = False Then
            TextBox611.Focus()
            Label441.Text = "Please Enter SGST % Other wise 0 "
            Return
        ElseIf TextBox838.Text = "" Or IsNumeric(TextBox838.Text) = False Then
            TextBox838.Focus()
            Label441.Text = "Please Enter CGST % Other wise 0 "
            Return
        ElseIf TextBox839.Text = "" Or IsNumeric(TextBox839.Text) = False Then
            TextBox839.Focus()
            Label441.Text = "Please Enter IGST % Other wise 0 "
            Return
        ElseIf TextBox840.Text = "" Or IsNumeric(TextBox840.Text) = False Then
            TextBox840.Focus()
            Label441.Text = "Please Enter CESS % Other wise 0 "
            Return
        ElseIf TextBox671.Text = "" Or IsNumeric(TextBox671.Text) = False Then
            TextBox671.Focus()
            Label441.Text = "Please Enter Tolerance % Other wise 0 "
            Return
        ElseIf DropDownList61.Text = "Select" Then
            DropDownList61.Focus()
            Label441.Text = "Please Select Work Type"
            Return
        End If

        Dim W_SLNO As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "SELECT MAX(W_SLNO) As W_SLNO FROM WO_ORDER WHERE PO_NO= '" & TextBox125.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If IsDBNull(dr.Item("W_SLNO")) Then
                W_SLNO = "0"
            Else
                W_SLNO = dr.Item("W_SLNO")
            End If

            dr.Close()
        End If
        conn.Close()

        ''insert grid view
        'count = (GridView4.Rows.Count + 1)
        count = GridView4.Rows.Count + CDec(W_SLNO) + 1
        Dim dt_wo As DataTable = DirectCast(ViewState("WORK"), DataTable)
        'dt_wo.Rows.Add(count, DropDownList46.SelectedValue, TextBox621.Text, TextBox561.Text, TextBox571.Text, TextBox581.Text, TextBox591.Text, TextBox641.Text, TextBox651.Text, TextBox661.Text, TextBox671.Text, TextBox601.Text, TextBox611.Text, TextBox838.Text, TextBox839.Text, TextBox840.Text, DropDownList61.Text, TextBox721.Text)
        dt_wo.Rows.Add(count, DropDownList46.SelectedValue, TextBox621.Text, TextBox561.Text, TextBox571.Text, TextBox581.Text, TextBox591.Text, TextBox641.Text, TextBox651.Text, TextBox661.Text, TextBox671.Text, TextBox601.Text, TextBox611.Text, TextBox838.Text, TextBox839.Text, TextBox840.Text, DropDownList61.Text, TextBox721.Text)
        ViewState("WORK") = dt_wo
        Me.BINDGRIDWORK()
        ''calulation
        TextBox681.Text = 0.0
        TextBox691.Text = 0.0
        TextBox701.Text = 0.0
        TextBox11.Text = 0.0
        TextBox12.Text = 0.0
        TextBox711.Text = 0.0
        TextBox721.Text = 0.0
        Dim i As Integer = 0
        For i = 0 To GridView4.Rows.Count - 1
            Dim total, net, discount, SGST, CGST, IGST, CESS As New Decimal(0)
            Dim mat_cost As Decimal = 0
            net = CDec(GridView4.Rows(i).Cells(3).Text) * CDec(GridView4.Rows(i).Cells(5).Text)
            mat_cost = CDec(GridView4.Rows(i).Cells(3).Text) * CDec(GridView4.Rows(i).Cells(6).Text)
            discount = (net * CDec(GridView4.Rows(i).Cells(11).Text)) / 100
            SGST = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(12).Text)) / 100
            CGST = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(13).Text)) / 100
            IGST = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(14).Text)) / 100
            CESS = ((net + mat_cost - discount) * CDec(GridView4.Rows(i).Cells(15).Text)) / 100

            total = (net - discount) + SGST + CGST + IGST + CESS + mat_cost
            total = FormatNumber(total, 2)
            GridView4.Rows(i).Cells(17).Text = total

            TextBox681.Text = FormatNumber(net + mat_cost + CDec(TextBox681.Text), 3)

            TextBox691.Text = FormatNumber((SGST + CDec(TextBox691.Text)), 3)
            TextBox701.Text = FormatNumber(CGST + CDec(TextBox701.Text), 3)
            TextBox11.Text = FormatNumber((IGST + CDec(TextBox11.Text)), 3)
            TextBox12.Text = FormatNumber(CESS + CDec(TextBox12.Text), 3)

            TextBox711.Text = FormatNumber(total - FormatNumber(total, 0), 3)
            TextBox721.Text = FormatNumber(FormatNumber(total, 0) + CDec(TextBox721.Text), 2)

        Next
        Label441.Text = ""
    End Sub

    Protected Sub Button56_Click(sender As Object, e As EventArgs) Handles Button56.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry

                If TextBox84.Text = "" Or IsNumeric(TextBox84.Text) = False Then
                    TextBox84.Focus()
                    Return
                ElseIf IsDate(TextBox85.Text) = False Then
                    TextBox85.Focus()
                    Return
                End If
                ''SAVE DATA
                'conn_trans.Open()
                Dim query As String = "Insert Into RATE_CONTRACT(PO_NO,PO_DATE,ORD_AMOUNT,CLR_AMOUNT,VALID_DATE) values (@PO_NO,@PO_DATE,@ORD_AMOUNT,@CLR_AMOUNT,@VALID_DATE)"
                Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@PO_NO", TextBox729.Text)
                cmd.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(TextBox730.Text), "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@ORD_AMOUNT", CDec(TextBox84.Text))
                cmd.Parameters.AddWithValue("@CLR_AMOUNT", 0.0)
                cmd.Parameters.AddWithValue("@VALID_DATE", Date.ParseExact(CDate(TextBox85.Text), "dd-MM-yyyy", provider))
                cmd.ExecuteReader()
                cmd.Dispose()


                Dim cmd2 As New SqlCommand("update ORDER_DETAILS set PO_BASE_VALUE= " & CDec(TextBox84.Text) & ",FULL_VALUE= " & CDec(TextBox84.Text) & " where SO_NO ='" & TextBox729.Text & "'", conn_trans, myTrans)
                cmd2.ExecuteReader()
                cmd2.Dispose()

                myTrans.Commit()
                conn_trans.Close()
                Label679.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                Label679.Text = "There was some Error, please contact EDP."
            Finally

                conn_trans.Close()
            End Try

        End Using


    End Sub

    Protected Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click

        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If TextBox64.Text = "" Or IsNumeric(TextBox64.Text) = False Then
            TextBox64.Focus()
            Return
        ElseIf DropDownList8.SelectedValue = "Select" Then
            DropDownList8.Focus()
            Return
        ElseIf TextBox59.Text = "" Then
            TextBox59.Focus()
            Return
        ElseIf TextBox60.Text = "" Or IsNumeric(TextBox60.Text) = False Then
            TextBox60.Focus()
            Return
        ElseIf TextBox62.Text = "" Or IsNumeric(TextBox62.Text) = False Then
            TextBox62.Focus()
            Return
        ElseIf cgstPercentage.Text = "" Or IsNumeric(cgstPercentage.Text) = False Then
            cgstPercentage.Focus()
            Return
        ElseIf igstPercentage.Text = "" Or IsNumeric(igstPercentage.Text) = False Then
            igstPercentage.Focus()
            Return
        ElseIf po_pfCombo.Text = "" Or IsNumeric(po_pfCombo.Text) = False Then
            po_pfCombo.Focus()
            Return
        ElseIf po_taxTextBox.Text = "" Or IsNumeric(po_taxTextBox.Text) = False Then
            po_taxTextBox.Focus()
            Return
        ElseIf Delvdate.Text = "" Or IsDate(Delvdate.Text) = False Then
            Delvdate.Focus()
            Return
        ElseIf TextBox65.Text = "" Or IsNumeric(TextBox65.Text) = False Then
            TextBox65.Focus()
            Return
        ElseIf po_tradedisText.Text = "" Or IsNumeric(po_tradedisText.Text) = False Then
            po_tradedisText.Focus()
            Return
        ElseIf sgstPercentage.Text = "" Or IsNumeric(sgstPercentage.Text) = False Then
            sgstPercentage.Focus()
            Return
        ElseIf po_frightText.Text = "" Or IsNumeric(po_frightText.Text) = False Then
            po_frightText.Focus()
            Return
        ElseIf TextBox9.Text = "" Or IsNumeric(TextBox9.Text) = False Then
            TextBox9.Focus()
            Return
        End If
        Dim date_diff As Integer
        date_diff = DateDiff(DateInterval.Day, Today.Date, CDate(Delvdate.Text))
        If date_diff < 0 Then
            Delvdate.Focus()
            Delvdate.Text = ""
            Return
        End If

        If ((TextBox90.Text <> "D8888") And (TextBox90.Text <> "D3002") And ((CDec(sgstPercentage.Text) + CDec(cgstPercentage.Text) + CDec(igstPercentage.Text)) = 0)) Then
            Label432.Text = "GST percentage cannot be left blank."
            Return
        End If

        'sono slno check
        conn.Open()
        count = 0
        ds.Tables.Clear()
        da = New SqlDataAdapter("select * from SO_MAT_ORDER where SO_NO ='" & TextBox86.Text & "' and ITEM_SLNO =" & TextBox64.Text, conn)
        count = da.Fill(ds, "SO_MAT_ORDER")
        conn.Close()
        If count > 0 Then
            TextBox64.Text = ""
            TextBox64.Focus()
            Return
        End If



        Button3.Enabled = True
        po_frightText.ReadOnly = True
        sgstPercentage.ReadOnly = True
        po_tradedisText.ReadOnly = True
        TextBox65.ReadOnly = True
        TextBox62.ReadOnly = True
        Delvdate.ReadOnly = True
        po_taxTextBox.ReadOnly = True
        po_pfCombo.ReadOnly = True
        TextBox60.ReadOnly = True
        TextBox59.ReadOnly = True
        TextBox64.ReadOnly = True
        Panel5.Visible = True
        ''Panel4.Visible = False
        If DropDownList8.SelectedValue = "Set" Then
            po_matqty_text.Text = "0"
            po_unitWEIGHTText.Text = "0"
            po_matqty_text.ReadOnly = False
            po_unitWEIGHTText.ReadOnly = False
            GridView1.Columns(8).HeaderText = "Unit Price(Mt)"
        ElseIf (DropDownList8.SelectedValue = "Pcs" Or DropDownList8.SelectedValue = "PCS") Then
            po_matqty_text.Text = "1"
            po_matqty_text.ReadOnly = False
            po_unitWEIGHTText.ReadOnly = False
            GridView1.Columns(8).HeaderText = "Unit Price(Pcs)"
            po_matcodecombo.Visible = True
        ElseIf (DropDownList8.SelectedValue = "Mt" Or DropDownList8.SelectedValue = "MT" Or DropDownList8.SelectedValue = "MTS") Then
            po_matqty_text.Text = "1"
            po_matqty_text.ReadOnly = False
            po_unitWEIGHTText.ReadOnly = False
            GridView1.Columns(8).HeaderText = "Unit Price(Mt)"
            TextBox1.Visible = True
        ElseIf DropDownList8.SelectedValue = "Activity" Then
            po_matqty_text.Text = "1"
            GridView1.Columns(8).HeaderText = "Unit Price/Srv"
            TextBox2.Visible = True
        End If
        ''select f_item
        Dim item_au As String = ""
        If DropDownList8.SelectedValue = "Set" Or DropDownList8.SelectedValue = "Pcs" Or DropDownList8.SelectedValue = "PCS" Then
            item_au = "PCS"
        ElseIf (DropDownList8.SelectedValue = "Mt" Or DropDownList8.SelectedValue = "MT" Or DropDownList8.SelectedValue = "MTS") Then
            item_au = "MTS"
        ElseIf DropDownList8.SelectedValue = "Activity" Then
            item_au = "Activity"
        End If
    End Sub

    Protected Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click

        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                ''CHECK MATERIAL AVAILABILITY
                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select SO_NO from SO_MAT_ORDER WHERE SO_NO ='" & TextBox86.Text & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    Label432.Text = "Please add material first"
                    Return
                Else
                    ''update order details

                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox86.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()

                End If
                myTrans.Commit()
                conn_trans.Close()
                Label432.Text = "Order Submited Succsessfully"
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                Label432.Text = "There was some Error, please contact EDP."
            Finally
                conn_trans.Close()
            End Try

        End Using


    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim mat_code, mat_name, ITEM_AU As New String("")

        If DropDownList8.SelectedValue = "Select" Then
            DropDownList8.Focus()
            Return
        ElseIf (DropDownList8.SelectedValue = "Pcs" Or DropDownList8.SelectedValue = "PCS") Then
            If po_matcodecombo.Text = "" Then
                po_matcodecombo.Focus()
                Return
            Else
                ITEM_AU = TextBox816.Text
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  ITEM_CODE from F_ITEM WHERE ITEM_CODE LIKE '" & po_matcodecombo.Text.Substring(0, po_matcodecombo.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(ds, "F_ITEM")
                conn.Close()
                If count = 0 Then
                    po_matcodecombo.Focus()
                    po_matcodecombo.Text = ""
                    Return
                Else
                    mat_code = po_matcodecombo.Text.Substring(0, po_matcodecombo.Text.IndexOf(",") - 1)
                    mat_name = po_matcodecombo.Text.Substring(po_matcodecombo.Text.IndexOf(",") + 2)
                End If
            End If
        ElseIf (DropDownList8.SelectedValue = "Mt" Or DropDownList8.SelectedValue = "MT" Or DropDownList8.SelectedValue = "MTS") Then
            If TextBox1.Text = "" Then
                TextBox1.Focus()
                Return
            Else
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  ITEM_CODE from F_ITEM WHERE ITEM_CODE LIKE '" & TextBox1.Text.Substring(0, TextBox1.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(ds, "F_ITEM")
                conn.Close()
                If count = 0 Then
                    TextBox1.Focus()
                    TextBox1.Text = ""
                    Return
                Else
                    mat_code = TextBox1.Text.Substring(0, TextBox1.Text.IndexOf(",") - 1)
                    mat_name = TextBox1.Text.Substring(TextBox1.Text.IndexOf(",") + 2)
                End If
            End If
        ElseIf DropDownList8.SelectedValue = "Activity" Then
            If TextBox2.Text = "" Then
                TextBox2.Focus()
                Return
            Else
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  ITEM_CODE from F_ITEM WHERE ITEM_CODE LIKE '" & TextBox2.Text.Substring(0, TextBox2.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(ds, "F_ITEM")
                conn.Close()
                If count = 0 Then
                    TextBox2.Focus()
                    TextBox2.Text = ""
                    Return
                Else
                    mat_code = TextBox2.Text.Substring(0, TextBox2.Text.IndexOf(",") - 1)
                    mat_name = TextBox2.Text.Substring(TextBox2.Text.IndexOf(",") + 2)
                End If
            End If
        End If
        If po_unitWEIGHTText.Text = "" Then
            po_unitWEIGHTText.Focus()
            Return
        ElseIf IsNumeric(po_unitWEIGHTText.Text) = False Then
            po_unitWEIGHTText.Text = ""
            po_unitWEIGHTText.Focus()
            Return
        End If
        count = GridView1.Rows.Count + 1
        Dim dt_x As DataTable = DirectCast(ViewState("mat"), DataTable)
        dt_x.Rows.Add(TextBox64.Text, mat_code, mat_name, HiddenField1.Value, po_matqty_text.Text, po_unitWEIGHTText.Text, FormatNumber(CDec(po_matqty_text.Text) * CDec(TextBox60.Text), 3), FormatNumber((((CDec(po_matqty_text.Text) * CDec(TextBox60.Text)) * CDec(po_unitWEIGHTText.Text))) / 1000, 3), TextBox62.Text, TextBox63.Text)
        ViewState("mat") = dt_x
        Me.BINDGRID1()
        po_matcodecombo.Focus()
    End Sub

    Protected Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        If (DropDownList8.SelectedValue = "Mt" Or DropDownList8.SelectedValue = "MT" Or DropDownList8.SelectedValue = "MTS") Then
            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).Cells(8).Text = FormatNumber(CDec(TextBox62.Text), 3)
            Next
        ElseIf DropDownList8.SelectedValue = "Set" Then
            Dim i As Integer = 0
            Dim total_weight As Decimal = 0
            For i = 0 To GridView1.Rows.Count - 1
                total_weight = total_weight + CDec(GridView1.Rows(i).Cells(7).Text)
            Next
            Dim total_price As Decimal = 0
            total_price = CDec(TextBox60.Text) * CDec(TextBox62.Text)
            Dim unit_price As Decimal = 0
            unit_price = total_price / total_weight
            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).Cells(8).Text = FormatNumber(unit_price, 3)
            Next
        ElseIf (DropDownList8.SelectedValue = "Pcs" Or DropDownList8.SelectedValue = "PCS") Then
            For i = 0 To GridView1.Rows.Count - 1
                GridView1.Rows(i).Cells(8).Text = FormatNumber(CDec(TextBox62.Text), 3)
            Next
        End If
    End Sub

    Protected Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        Panel5.Visible = False
        Button3.Enabled = False
        po_frightText.ReadOnly = False
        sgstPercentage.ReadOnly = False
        po_tradedisText.ReadOnly = False
        TextBox65.ReadOnly = False
        TextBox62.ReadOnly = False
        Delvdate.ReadOnly = False
        po_taxTextBox.ReadOnly = False
        po_pfCombo.ReadOnly = False
        TextBox60.ReadOnly = False
        TextBox59.ReadOnly = False
        TextBox64.ReadOnly = False
        Panel5.Visible = False
        TextBox64.Focus()
        ''Panel4.Visible = True

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            'Dim price_unit, total_rate_unit, taxable_rate_unit, total_amt, ass_price, discount_unit_rate As New Decimal(0.0)

            'Dim DISC_TYPE, PACK_TYPE, FREIGHT_TYPE As New String("")
            'DISC_TYPE = DropDownList48.SelectedValue
            'PACK_TYPE = DropDownList47.SelectedValue
            'FREIGHT_TYPE = DropDownList9.SelectedValue

            'ass_price = CDec(TextBox60.Text) * CDec(TextBox62.Text)

            '''DISCOUNT
            'If DISC_TYPE = "PERCENTAGE" Then
            '    discount_unit_rate = FormatNumber(((ass_price * CDec(po_tradedisText.Text)) / 100), 2)
            'ElseIf DISC_TYPE = "PER UNIT" Then
            '    discount_unit_rate = FormatNumber(((CDec(TextBox54.Text) * discount)) + CDec(TextBox37.Text), 2)
            'ElseIf DISC_TYPE = "PER MT" Then
            '    discount_unit_rate = FormatNumber(((CDec(total_weight) * discount)) + CDec(TextBox37.Text), 2)
            'End If
            '''PACKING AND FORWD
            'If PACK_TYPE = "PERCENTAGE" Then
            '    TextBox41.Text = FormatNumber(((CDec(ASS_V) * pack_forwd) / 100), 2)
            'ElseIf PACK_TYPE = "PER UNIT" Then
            '    TextBox41.Text = FormatNumber(((CDec(TextBox54.Text) * pack_forwd)) + CDec(TextBox41.Text), 2)
            'ElseIf PACK_TYPE = "PER MT" Then
            '    TextBox41.Text = FormatNumber(((CDec(total_weight) * pack_forwd)) + CDec(TextBox41.Text), 2)
            'End If
            '''FREIGHT
            'If DropDownList6.Text <> "PARTY" Then
            '    If FREIGHT_TYPE = "PERCENTAGE" Then
            '        TextBox39.Text = FormatNumber((((CDec(ASS_V) * freight_rate) / 100)), 2)
            '    ElseIf FREIGHT_TYPE = "PER UNIT" Then
            '        TextBox39.Text = FormatNumber(((CDec(TextBox54.Text) * freight_rate)) + CDec(TextBox39.Text), 2)
            '    ElseIf FREIGHT_TYPE = "PER MT" Then
            '        TextBox39.Text = FormatNumber(((total_weight * freight_rate)) + CDec(TextBox39.Text), 2)
            '    End If
            'ElseIf DropDownList6.Text = "PARTY" Then
            '    TextBox39.Text = "0.00"
            'End If

            'taxable_rate_unit = FormatNumber((ass_price + CDec(TextBox41.Text) + CDec(TextBox39.Text) - CDec(TextBox37.Text)) / CDec(TextBox54.Text), 4)

            Try
                'Database updation entry
                Dim SALE_TYPE As String = ""
                If sgstPercentage.Enabled = False And cgstPercentage.Enabled = False And igstPercentage.Enabled = False Then
                    SALE_TYPE = "D"
                Else
                    SALE_TYPE = "T"
                End If


                Dim I As Integer
                For I = 0 To GridView1.Rows.Count - 1
                    Dim DETAIL As String = ""
                    If GridView1.Rows(I).Cells(9).Text = "&nbsp;" Then
                        DETAIL = ""
                    Else
                        DETAIL = GridView1.Rows(I).Cells(9).Text
                    End If
                    'conn.Open()
                    Dim QUARY1 As String = ""
                    QUARY1 = "Insert Into SO_MAT_ORDER(PURPOSE,CUSTOMER_SLNO,HSN_CODE,SO_NO ,ITEM_VOCAB ,ITEM_SLNO ,ITEM_CODE ,ORD_AU ,ITEM_QTY ,ITEM_MT ,ITEM_UNIT_RATE ,ITEM_PACK ,PACK_TYPE ,ITEM_DISCOUNT ,DISC_TYPE ,ITEM_CGST ,ITEM_SGST ,ITEM_IGST ,ITEM_TERMINAL_TAX ,ITEM_TCS ,ITEM_FREIGHT_PU ,ITEM_FREIGHT_TYPE ,ITEM_DELIVERY,ITEM_QTY_SEND,ITEM_S_TAX,ITEM_STATUS,AMD_NO,AMD_DATE,ITEM_DETAILS,ITEM_WEIGHT,SALE_TYPE,ITEM_CESS)values(@PURPOSE,@CUSTOMER_SLNO,@HSN_CODE,@SO_NO ,@ITEM_VOCAB ,@ITEM_SLNO ,@ITEM_CODE ,@ORD_AU ,@ITEM_QTY ,@ITEM_MT ,@ITEM_UNIT_RATE ,@ITEM_PACK ,@PACK_TYPE ,@ITEM_DISCOUNT ,@DISC_TYPE ,@ITEM_CGST ,@ITEM_SGST ,@ITEM_IGST ,@ITEM_TERMINAL_TAX ,@ITEM_TCS ,@ITEM_FREIGHT_PU ,@ITEM_FREIGHT_TYPE ,@ITEM_DELIVERY,@ITEM_QTY_SEND,@ITEM_S_TAX,@ITEM_STATUS,@AMD_NO,@AMD_DATE,@ITEM_DETAILS,@ITEM_WEIGHT,@SALE_TYPE,@ITEM_CESS)"
                    Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@SO_NO", TextBox86.Text)
                    cmd1.Parameters.AddWithValue("@ITEM_VOCAB", TextBox59.Text)
                    cmd1.Parameters.AddWithValue("@ITEM_SLNO", TextBox64.Text)
                    cmd1.Parameters.AddWithValue("@ITEM_CODE", GridView1.Rows(I).Cells(1).Text)
                    cmd1.Parameters.AddWithValue("@ORD_AU", DropDownList8.SelectedValue)
                    cmd1.Parameters.AddWithValue("@ITEM_QTY", CDec(GridView1.Rows(I).Cells(6).Text))
                    cmd1.Parameters.AddWithValue("@ITEM_MT", CDec(GridView1.Rows(I).Cells(7).Text))
                    cmd1.Parameters.AddWithValue("@ITEM_UNIT_RATE", CDec(GridView1.Rows(I).Cells(8).Text))
                    cmd1.Parameters.AddWithValue("@ITEM_PACK", CDec(po_pfCombo.Text))
                    cmd1.Parameters.AddWithValue("@PACK_TYPE", DropDownList47.SelectedValue)
                    cmd1.Parameters.AddWithValue("@ITEM_DISCOUNT", CDec(po_tradedisText.Text))
                    cmd1.Parameters.AddWithValue("@DISC_TYPE", DropDownList48.SelectedValue)
                    cmd1.Parameters.AddWithValue("@ITEM_CGST", CDec(cgstPercentage.Text))
                    cmd1.Parameters.AddWithValue("@ITEM_SGST", CDec(sgstPercentage.Text))
                    cmd1.Parameters.AddWithValue("@ITEM_IGST", CDec(igstPercentage.Text))
                    cmd1.Parameters.AddWithValue("@ITEM_TERMINAL_TAX", CDec(TextBox65.Text))
                    cmd1.Parameters.AddWithValue("@ITEM_TCS", CDec(po_taxTextBox.Text))
                    cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_TYPE", DropDownList9.Text)
                    cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_PU", CDec(po_frightText.Text))
                    cmd1.Parameters.AddWithValue("@ITEM_DELIVERY", Date.ParseExact(Delvdate.Text, "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@ITEM_QTY_SEND", 0.0)
                    cmd1.Parameters.AddWithValue("@ITEM_S_TAX", 0.0)
                    cmd1.Parameters.AddWithValue("@ITEM_STATUS", "PENDING")
                    cmd1.Parameters.AddWithValue("@ITEM_WEIGHT", CDec(GridView1.Rows(I).Cells(5).Text))
                    cmd1.Parameters.AddWithValue("@AMD_NO", "0")
                    cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(TextBox87.Text, "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@ITEM_DETAILS", DETAIL)
                    cmd1.Parameters.AddWithValue("@SALE_TYPE", SALE_TYPE)
                    cmd1.Parameters.AddWithValue("@ITEM_CESS", CDec(TextBox9.Text))
                    cmd1.Parameters.AddWithValue("@HSN_CODE", TextBox68.Text)
                    cmd1.Parameters.AddWithValue("@CUSTOMER_SLNO", TextBox70.Text)
                    cmd1.Parameters.AddWithValue("@PURPOSE", TextBox71.Text)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()

                Next
                Dim dt1 As New DataTable()
                dt1.Columns.AddRange(New DataColumn(9) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Qty"), New DataColumn("Unit Weight"), New DataColumn("Mat Ord. Qty"), New DataColumn("ORD_QTY_MT"), New DataColumn("Unit Price"), New DataColumn("Mat Desc")})
                ViewState("mat") = dt1
                Me.BINDGRID1()
                myTrans.Commit()
                conn_trans.Close()
                Label432.Text = "Data Saved Succsessfully"
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                Label432.Text = "There was some Error, please contact EDP."
            Finally
                conn_trans.Close()
            End Try



        End Using

    End Sub

    Protected Sub DropDownList8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList8.SelectedIndexChanged
        If DropDownList8.SelectedValue = "Select" Then
            DropDownList8.Focus()
            Return
        ElseIf (DropDownList8.SelectedValue = "Pcs" Or DropDownList8.SelectedValue = "PCS") Then
            po_unitWEIGHTText.ReadOnly = False
            po_unitWEIGHTText.Text = ""
            po_matcodecombo.Visible = True
            TextBox1.Visible = False
            TextBox2.Visible = False
        ElseIf (DropDownList8.SelectedValue = "Mt" Or DropDownList8.SelectedValue = "MT" Or DropDownList8.SelectedValue = "MTS") Then
            po_unitWEIGHTText.ReadOnly = True
            po_unitWEIGHTText.Text = 1000.0
            TextBox1.Visible = True
            po_matcodecombo.Visible = False
            TextBox2.Visible = False
        ElseIf DropDownList8.SelectedValue = "Activity" Then
            po_unitWEIGHTText.ReadOnly = True
            po_unitWEIGHTText.Text = 0.0
            TextBox2.Visible = True
            po_matcodecombo.Visible = False
            TextBox1.Visible = False
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt_wo As New DataTable()
        dt_wo.Columns.AddRange(New DataColumn(17) {New DataColumn("W_SLNO"), New DataColumn("TAX_TYPE"), New DataColumn("W_NAME"), New DataColumn("W_QTY"), New DataColumn("W_AU"), New DataColumn("W_UNIT_PRICE"), New DataColumn("W_MATERIAL_COST"), New DataColumn("W_AREA"), New DataColumn("W_START_DATE"), New DataColumn("W_END_DATE"), New DataColumn("W_TOLERANCE"), New DataColumn("W_DISCOUNT"), New DataColumn("SGST"), New DataColumn("CGST"), New DataColumn("IGST"), New DataColumn("CESS"), New DataColumn("wo_type"), New DataColumn("t_value")})
        ViewState("WORK") = dt_wo
        Me.BINDGRIDWORK()
        TextBox681.Text = 0.0
        TextBox691.Text = 0.0
        TextBox701.Text = 0.0
        TextBox711.Text = 0.0
        TextBox721.Text = 0.0

        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub DropDownList61_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList61.SelectedIndexChanged
        If DropDownList61.SelectedValue = "Select" Then
            DropDownList61.Focus()
            Return
        ElseIf DropDownList61.SelectedValue = "Service Work" Then
            TextBox581.Enabled = True
            TextBox591.Enabled = True
            TextBox581.BackColor = System.Drawing.Color.White
            TextBox581.ForeColor = Drawing.Color.Black
            TextBox581.Text = ""
        ElseIf DropDownList61.SelectedValue = "Material Supply" Then
            TextBox581.Enabled = False
            TextBox591.Enabled = True
            TextBox581.Text = 0.0
            TextBox581.BackColor = System.Drawing.ColorTranslator.FromHtml("#6600FF")
            TextBox581.ForeColor = Drawing.Color.White
        End If
    End Sub

    Protected Sub po_ed_typeComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles po_ed_typeComboBox2.SelectedIndexChanged
        If po_frightText2.Visible = False Then
            If po_ed_typeComboBox2.SelectedValue = "PER UNIT" Then
                Label672.Visible = False
                po_frightText2.Visible = False
                po_frightText2.Text = 0.0
            ElseIf po_ed_typeComboBox2.SelectedValue = "PER MT" Then
                If (TextBox815.Text = "Mt" Or TextBox815.Text = "MT" Or TextBox815.Text = "MTS") Then
                    Label672.Visible = False
                    po_frightText2.Visible = False
                    po_frightText2.Text = 0.0
                Else
                    Label672.Visible = True
                    po_frightText2.Visible = True
                End If
            ElseIf po_ed_typeComboBox2.SelectedValue = "PERCENTAGE" Then
                Label672.Visible = False
                po_frightText2.Visible = False
                po_frightText2.Text = 0.0
            End If
        ElseIf po_ed_typeComboBox2.SelectedValue = "PER MT" Or DISCOUNT_typeComboBox.SelectedValue = "PER MT" Or PF_typeComboBox3.SelectedValue = "PER MT" Then
            Label672.Visible = True
            po_frightText2.Visible = True
        Else
            Label672.Visible = False
            po_frightText2.Visible = False
            po_frightText2.Text = 0.0
        End If
    End Sub

    Protected Sub DISCOUNT_typeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DISCOUNT_typeComboBox.SelectedIndexChanged
        If po_frightText2.Visible = False Then
            If DISCOUNT_typeComboBox.SelectedValue = "PER UNIT" Then
                Label672.Visible = False
                po_frightText2.Visible = False
                po_frightText2.Text = 0.0
            ElseIf DISCOUNT_typeComboBox.SelectedValue = "PER MT" Then
                If (TextBox815.Text = "Mt" Or TextBox815.Text = "MT" Or TextBox815.Text = "MTS") Then
                    Label672.Visible = False
                    po_frightText2.Visible = False
                    po_frightText2.Text = 0.0
                Else
                    Label672.Visible = True
                    po_frightText2.Visible = True
                End If
            ElseIf DISCOUNT_typeComboBox.SelectedValue = "PERCENTAGE" Then
                Label672.Visible = False
                po_frightText2.Visible = False
                po_frightText2.Text = 0.0
            End If
        ElseIf po_ed_typeComboBox2.SelectedValue = "PER MT" Or DISCOUNT_typeComboBox.SelectedValue = "PER MT" Or PF_typeComboBox3.SelectedValue = "PER MT" Then
            Label672.Visible = True
            po_frightText2.Visible = True
        Else
            Label672.Visible = False
            po_frightText2.Visible = False
            po_frightText2.Text = 0.0
        End If
    End Sub

    Protected Sub PF_typeComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PF_typeComboBox3.SelectedIndexChanged
        If po_frightText2.Visible = False Then
            If PF_typeComboBox3.SelectedValue = "PER UNIT" Then
                Label672.Visible = False
                po_frightText2.Visible = False
                po_frightText2.Text = 0.0
            ElseIf PF_typeComboBox3.SelectedValue = "PER MT" Then
                If (TextBox815.Text = "Mt" Or TextBox815.Text = "MT" Or TextBox815.Text = "MTS") Then
                    Label672.Visible = False
                    po_frightText2.Visible = False
                    po_frightText2.Text = 0.0
                Else
                    Label672.Visible = True
                    po_frightText2.Visible = True
                End If
            ElseIf PF_typeComboBox3.SelectedValue = "PERCENTAGE" Then
                Label672.Visible = False
                po_frightText2.Visible = False
                po_frightText2.Text = 0.0
            End If
        ElseIf po_ed_typeComboBox2.SelectedValue = "PER MT" Or DISCOUNT_typeComboBox.SelectedValue = "PER MT" Or PF_typeComboBox3.SelectedValue = "PER MT" Then
            Label672.Visible = True
            po_frightText2.Visible = True
        Else
            Label672.Visible = False
            po_frightText2.Visible = False
            po_frightText2.Text = 0.0
        End If
    End Sub

    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TextBox25.Text = "" Then
            TextBox25.Focus()
            Return
        ElseIf TextBox27.Text = "" Or IsNumeric(TextBox27.Text) = False Then
            TextBox27.Focus()
            Return
        ElseIf TextBox28.Text = "" Or IsNumeric(TextBox28.Text) = False Then
            TextBox28.Focus()
            Return
        ElseIf TextBox29.Text = "" Or IsDate(TextBox29.Text) = False Then
            TextBox29.Focus()
            Return
        ElseIf TextBox25.Text.IndexOf(",") <> 10 Then
            TextBox25.Text = ""
            TextBox25.Focus()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select MAT_AU from MATERIAL where MAT_CODE = '" & TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1)) & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1)).Length <> 9 Or IsNumeric(TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1))) = False Or count = 0 Then
            TextBox25.Text = ""
            TextBox25.Focus()
            Return
        End If
        ''validation
        Dim po_type As String = ""
        conn.Open()
        mycommand.CommandText = "select PO_TYPE from ORDER_DETAILS WHERE SO_NO = '" & TextBox16.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            po_type = dr.Item("PO_TYPE")
            dr.Close()
        End If
        conn.Close()
        ''SERIAL NO SEARCH
        count = (GridView2.Rows.Count + 1)
        ''CALCULATATION
        Dim AU As String
        AU = "N/A"
        Dim mat_name, mat_code As String
        mat_name = ""
        mat_code = TextBox25.Text.Substring(0, TextBox25.Text.IndexOf(",") - 1)
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & mat_code & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            AU = dr.Item("MAT_AU")
            mat_name = dr.Item("MAT_NAME")
            dr.Close()
        End If
        conn.Close()
        Dim dt_table As DataTable = DirectCast(ViewState("foreign_store"), DataTable)
        dt_table.Rows.Add(count, mat_code, mat_name, AU, TextBox27.Text, TextBox28.Text, TextBox29.Text, TextBox30.Text)
        ViewState("foreign_store") = dt_table
        Me.BINDGRIDFGN_STORE_IMP()
        DropDownList2.Items.Clear()
        For I As Integer = 0 To GridView2.Rows.Count - 1
            DropDownList2.Items.Add(GridView2.Rows(I).Cells(0).Text)
        Next
        DropDownList2.Items.Add("Select")
        DropDownList2.SelectedValue = "Select"
        TextBox32.Text = 0.0
        TextBox31.Text = 0.0

        If GridView2.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView2.Rows.Count - 1
                Dim basic_price As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView2.Rows(I).Cells(4).Text) * CDec(GridView2.Rows(I).Cells(5).Text)
                TextBox31.Text = basic_price + CDec(TextBox31.Text)
                TextBox32.Text = basic_price + CDec(TextBox32.Text)
            Next
        End If
        TextBox25.Text = ""
    End Sub

    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If TextBox25.Text = "" Then
            TextBox25.Focus()
            Return
        ElseIf TextBox27.Text = "" Or IsNumeric(TextBox27.Text) = False Then
            TextBox27.Focus()
            Return
        ElseIf TextBox28.Text = "" Or IsNumeric(TextBox28.Text) = False Then
            TextBox28.Focus()
            Return
        ElseIf TextBox29.Text = "" Or IsDate(TextBox29.Text) = False Then
            TextBox29.Focus()
            Return
        ElseIf TextBox25.Text.IndexOf(",") <> 10 Then
            TextBox25.Text = ""
            TextBox25.Focus()
            Return
        ElseIf TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1)).Length <> 9 Or IsNumeric(TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1))) = False Then
            TextBox25.Focus()
            TextBox25.Text = ""
            Return
        End If
        For I As Integer = 0 To DirectCast(ViewState("foreign_store"), DataTable).Rows.Count - 1
            If DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(0) = DropDownList2.SelectedValue Then
                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1)) & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    Label11.Text = "Please Enter Correct Material Code"
                    Return
                End If
                Dim AU As String = ""
                Dim MAT_NAME As String = ""
                conn.Open()
                mycommand.CommandText = "select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1)) & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    AU = dr.Item("MAT_AU")
                    MAT_NAME = dr.Item("MAT_NAME")
                    dr.Close()
                End If
                conn.Close()
                DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(1) = TextBox25.Text.Substring(0, (TextBox25.Text.IndexOf(",") - 1))
                DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(2) = MAT_NAME
                DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(3) = AU
                DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(4) = TextBox27.Text
                DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(5) = TextBox28.Text
                DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(6) = TextBox29.Text
                DirectCast(ViewState("foreign_store"), DataTable).Rows(I)(7) = TextBox30.Text
            End If
        Next
        Me.BINDGRIDFGN_STORE_IMP()
        TextBox31.Text = 0.0
        TextBox32.Text = 0.0
        If GridView2.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView2.Rows.Count - 1
                Dim basic_price As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView2.Rows(I).Cells(4).Text) * CDec(GridView2.Rows(I).Cells(5).Text)
                TextBox31.Text = basic_price + CDec(TextBox31.Text)
                TextBox32.Text = basic_price + CDec(TextBox32.Text)
            Next
        End If
        TextBox25.Text = ""
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim dt_fgn As New DataTable()
        dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
        ViewState("foreign_store") = dt_fgn
        Me.BINDGRIDFGN_STORE_IMP()
        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                ''DELETE EXITING DATA
                Dim order_type As String = ""
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox16.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")

                    dr.Close()
                End If
                conn.Close()


                If order_type <> "Rate Contract" Then

                    mycommand = New SqlCommand("DELETE FROM PO_ORD_MAT WHERE PO_NO ='" & TextBox16.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()

                ElseIf order_type = "Rate Contract" Then

                    mycommand = New SqlCommand("update ORDER_DETAILS set SO_STATUS='RC' where SO_NO ='" & TextBox16.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteReader()
                    mycommand.Dispose()

                End If

                ''DATA SAVE IN PO_ORD_MAT
                Dim ENTRY_TAX As Decimal = 1
                For I = 0 To GridView2.Rows.Count - 1
                    Dim po_slno As String = TextBox16.Text
                    Dim sno As Integer = GridView2.Rows(I).Cells(0).Text
                    Dim matcode As String = GridView2.Rows(I).Cells(1).Text
                    Dim matname As String = GridView2.Rows(I).Cells(2).Text
                    Dim matqty As String = GridView2.Rows(I).Cells(4).Text
                    Dim urate As String = GridView2.Rows(I).Cells(5).Text
                    Dim ddate As String = CDate(GridView2.Rows(I).Cells(6).Text.Trim)
                    Dim mat_details As String = GridView2.Rows(I).Cells(7).Text.Trim
                    If mat_details = "&nbsp;" Then
                        mat_details = ""
                    End If

                    Dim query As String = "Insert Into PO_ORD_MAT(HSN_CODE,MAT_DESC,PO_NO, MAT_SLNO,MAT_CODE,MAT_NAME,MAT_QTY,MAT_UNIT_RATE,PF_TYPE,MAT_PACK,DISC_TYPE,MAT_DISCOUNT,SGST,CGST,IGST,CESS,ANAL_TAX,FREIGHT_TYPE,MAT_FREIGHT_PU,MAT_DELIVERY,MAT_QTY_RCVD,MAT_STATUS,AMD_NO,AMD_DATE,TOTAL_WT,TDS_SGST,TDS_CGST,TDS_IGST) values (@HSN_CODE,@MAT_DESC,@PO_NO, @MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_QTY,@MAT_UNIT_RATE,@PF_TYPE,@MAT_PACK,@DISC_TYPE,@MAT_DISCOUNT,@SGST,@CGST,@IGST,@CESS,@ANAL_TAX,@FREIGHT_TYPE,@MAT_FREIGHT_PU,@MAT_DELIVERY,@MAT_QTY_RCVD,@MAT_STATUS,@AMD_NO,@AMD_DATE,@TOTAL_WT,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                    cmd.Parameters.AddWithValue("@MAT_SLNO", sno)
                    cmd.Parameters.AddWithValue("@MAT_CODE", matcode)
                    cmd.Parameters.AddWithValue("@MAT_NAME", matname)
                    cmd.Parameters.AddWithValue("@MAT_QTY", matqty)
                    cmd.Parameters.AddWithValue("@MAT_UNIT_RATE", urate)
                    cmd.Parameters.AddWithValue("@PF_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_PACK", 0)
                    cmd.Parameters.AddWithValue("@DISC_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_DISCOUNT", 0)
                    cmd.Parameters.AddWithValue("@CGST", 0)
                    cmd.Parameters.AddWithValue("@SGST", 0)
                    cmd.Parameters.AddWithValue("@IGST", 0)
                    cmd.Parameters.AddWithValue("@CESS", 0)
                    cmd.Parameters.AddWithValue("@ANAL_TAX", 0)
                    cmd.Parameters.AddWithValue("@FREIGHT_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_FREIGHT_PU", 0)
                    cmd.Parameters.AddWithValue("@MAT_DELIVERY", Date.ParseExact(ddate, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@MAT_QTY_RCVD", 0.0)
                    cmd.Parameters.AddWithValue("@MAT_STATUS", "Pending")
                    cmd.Parameters.AddWithValue("@MAT_DESC", mat_details)
                    cmd.Parameters.AddWithValue("@AMD_NO", "0")
                    cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox17.Text), "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@TOTAL_WT", 0)
                    cmd.Parameters.AddWithValue("@TDS_SGST", 0.00)
                    cmd.Parameters.AddWithValue("@TDS_CGST", 0.00)
                    cmd.Parameters.AddWithValue("@TDS_IGST", 0.00)
                    cmd.Parameters.AddWithValue("@HSN_CODE", TextBox66.Text)
                    cmd.ExecuteReader()
                    cmd.Dispose()

                Next
                ''update order details

                Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=" & GridView2.Rows.Count & " , FULL_VALUE= " & CDec(TextBox31.Text) & ", PO_BASE_VALUE= " & CDec(TextBox31.Text) & " where SO_NO ='" & TextBox16.Text & "'", conn_trans, myTrans)
                cmd2.ExecuteReader()
                cmd2.Dispose()


                Dim dt_fgn As New DataTable()
                dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
                ViewState("foreign_store") = dt_fgn
                Me.BINDGRIDFGN_STORE_IMP()
                myTrans.Commit()
                conn_trans.Close()

                Label11.Text = "Marerial Added Succesfully"
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label11.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try


        End Using


    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim order_type As String = ""
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox16.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")

                    dr.Close()
                End If
                conn.Close()
                If order_type = "Rate Contract" Then
                    Label11.Text = "This is a rate contract can't be submited"
                    Return
                End If
                ''CHECK MATERIAL AVAILABILITY

                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select PO_NO from PO_ORD_MAT WHERE PO_NO ='" & TextBox16.Text & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    Label11.Text = "Please add material first"
                    Return
                Else
                    ''update order details
                    'conn_trans.Open()
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox16.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()
                    Label11.Text = "Order Submited"
                End If

                myTrans.Commit()
                conn_trans.Close()
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label11.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                ''DELETE EXITING DATA
                Dim order_type, PO_TYPE As New String("")
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_TYPE,PO_TYPE from ORDER_DETAILS WHERE SO_NO = '" & soNoOutsourced.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")
                    PO_TYPE = dr.Item("PO_TYPE")
                    dr.Close()
                End If
                conn.Close()

                If order_type = "Purchase Order" Then

                    'Database updation entry
                    ''DATA SAVE IN PO_ORD_MAT
                    Dim ENTRY_TAX As Decimal = 0
                    Dim TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0)

                    For I = 0 To GridView5.Rows.Count - 1
                        Dim po_slno As String = soNoOutsourced.Text
                        Dim sno As Integer = GridView5.Rows(I).Cells(0).Text
                        Dim matcode As String = GridView5.Rows(I).Cells(1).Text
                        Dim matname As String = GridView5.Rows(I).Cells(2).Text
                        Dim MAT_AU As String = GridView5.Rows(I).Cells(3).Text
                        Dim matqty As Decimal = GridView5.Rows(I).Cells(4).Text

                        Dim urate As Decimal = GridView5.Rows(I).Cells(5).Text
                        Dim disc_type As String = GridView5.Rows(I).Cells(6).Text
                        Dim disc As Decimal = CDec(GridView5.Rows(I).Cells(7).Text)
                        Dim pf_type As String = GridView5.Rows(I).Cells(8).Text
                        Dim pf As Decimal = CDec(GridView5.Rows(I).Cells(9).Text)
                        Dim freight_type As String = GridView5.Rows(I).Cells(10).Text
                        Dim fritax As Decimal = GridView5.Rows(I).Cells(11).Text

                        Dim CGST As String = GridView5.Rows(I).Cells(12).Text
                        Dim SGST As String = GridView5.Rows(I).Cells(13).Text
                        Dim IGST As String = GridView5.Rows(I).Cells(14).Text
                        Dim ANAL As Decimal = GridView5.Rows(I).Cells(15).Text
                        Dim CESS As Decimal = GridView5.Rows(I).Cells(16).Text


                        Dim ddate As String = CDate(GridView5.Rows(I).Cells(17).Text.Trim)
                        Dim mat_details As String = GridView5.Rows(I).Cells(18).Text.Trim
                        Dim mat_qty_rcvd As Decimal = CDec(GridView5.Rows(I).Cells(19).Text.Trim)
                        Dim TOTAL_WT As Decimal = CDec(GridView5.Rows(I).Cells(20).Text.Trim)
                        If mat_details = "&nbsp;" Then
                            mat_details = ""
                        End If

                        ''TDS CHECK TDS ON GST CRITERIA
                        If CDec(TextBox18.Text) >= 250000 Then
                            If CDec(TextBox835.Text) > 0 Then
                                TDS_SGST = 0.00
                                TDS_CGST = 0.00
                                TDS_IGST = 2.0

                            Else
                                TDS_SGST = 1.0
                                TDS_CGST = 1.0
                                TDS_IGST = 0.00
                            End If
                        End If

                        'conn.Open()
                        Dim query As String = "Insert Into PO_ORD_MAT(PO_NO,MAT_SLNO,MAT_CODE,MAT_NAME,MAT_DESC,MAT_QTY,MAT_QTY_RCVD,MAT_UNIT_RATE,MAT_DISCOUNT,DISC_TYPE,MAT_PACK,PF_TYPE,MAT_FREIGHT_PU,FREIGHT_TYPE,SGST,CGST,IGST,CESS,ANAL_TAX,MAT_STATUS,AMD_NO,AMD_DATE,MAT_DELIVERY,TOTAL_WT,TDS_SGST,TDS_CGST,TDS_IGST) values (@PO_NO,@MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_DESC,@MAT_QTY,@MAT_QTY_RCVD,@MAT_UNIT_RATE,@MAT_DISCOUNT,@DISC_TYPE,@MAT_PACK,@PF_TYPE,@MAT_FREIGHT_PU,@FREIGHT_TYPE,@SGST,@CGST,@IGST,@CESS,@ANAL_TAX,@MAT_STATUS,@AMD_NO,@AMD_DATE,@MAT_DELIVERY,@TOTAL_WT,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                        Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                        cmd.Parameters.AddWithValue("@MAT_SLNO", sno)
                        cmd.Parameters.AddWithValue("@MAT_CODE", matcode)
                        cmd.Parameters.AddWithValue("@MAT_NAME", matname)
                        cmd.Parameters.AddWithValue("@MAT_DESC", mat_details)
                        cmd.Parameters.AddWithValue("@MAT_QTY", matqty)
                        cmd.Parameters.AddWithValue("@MAT_QTY_RCVD", mat_qty_rcvd)
                        cmd.Parameters.AddWithValue("@MAT_UNIT_RATE", urate)
                        cmd.Parameters.AddWithValue("@PF_TYPE", pf_type)
                        cmd.Parameters.AddWithValue("@MAT_PACK", pf)
                        cmd.Parameters.AddWithValue("@DISC_TYPE", disc_type)
                        cmd.Parameters.AddWithValue("@MAT_DISCOUNT", disc)
                        cmd.Parameters.AddWithValue("@FREIGHT_TYPE", freight_type)
                        cmd.Parameters.AddWithValue("@MAT_FREIGHT_PU", fritax)
                        cmd.Parameters.AddWithValue("@CGST", CGST)
                        cmd.Parameters.AddWithValue("@SGST", SGST)
                        cmd.Parameters.AddWithValue("@IGST", IGST)
                        cmd.Parameters.AddWithValue("@CESS", CESS)
                        cmd.Parameters.AddWithValue("@ANAL_TAX", ANAL)
                        cmd.Parameters.AddWithValue("@MAT_STATUS", "Pending")
                        cmd.Parameters.AddWithValue("@AMD_NO", "0")
                        cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(soDateOutsourced.Text), "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@MAT_DELIVERY", Date.ParseExact(ddate, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@TOTAL_WT", TOTAL_WT)
                        cmd.Parameters.AddWithValue("@TDS_SGST", TDS_SGST)
                        cmd.Parameters.AddWithValue("@TDS_CGST", TDS_CGST)
                        cmd.Parameters.AddWithValue("@TDS_IGST", TDS_IGST)

                        cmd.ExecuteReader()
                        cmd.Dispose()

                    Next
                    ''update order details
                    'conn_trans.Open()
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=" & GridView5.Rows.Count & " where SO_NO ='" & soNoOutsourced.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()


                    Dim dtOutsourced As New DataTable()
                    dtOutsourced.Columns.AddRange(New DataColumn(20) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("DISC_TYPE"), New DataColumn("MAT_DISCOUNT"), New DataColumn("PF_TYPE"), New DataColumn("MAT_PACK"), New DataColumn("FREIGHT_TYPE"), New DataColumn("MAT_FREIGHT_PU"), New DataColumn("CGST"), New DataColumn("SGST"), New DataColumn("IGST"), New DataColumn("ANAL_TAX"), New DataColumn("Cess"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC"), New DataColumn("MAT_QTY_RCVD"), New DataColumn("TOTAL_WT")})
                    ViewState("OutSourcedFG") = dtOutsourced
                    Me.BINDGRIDOUTSOURCED()

                    myTrans.Commit()
                    conn_trans.Close()
                    Label680.Text = "Data Saved Succsessfully"


                ElseIf (PO_TYPE = "OUTSOURCED ITEMS") Then

                    Panel13.Visible = True

                    Using conn_trans
                        ''conn_trans.Open()
                        ''myTrans = conn_trans.BeginTransaction()

                        Try
                            'Database updation entry
                            Dim SALE_TYPE As String = ""
                            If sgstPercentageOutsourced.Enabled = False And cgstPercentageOutsourced.Enabled = False And igstPercentageOutsourced.Enabled = False Then
                                SALE_TYPE = "D"
                            Else
                                SALE_TYPE = "T"
                            End If


                            Dim I As Integer
                            For I = 0 To GridView5.Rows.Count - 1

                                'conn.Open()
                                Dim QUARY1 As String = ""
                                QUARY1 = "Insert Into SO_MAT_ORDER(HSN_CODE,SO_NO ,ITEM_VOCAB ,ITEM_SLNO ,ITEM_CODE ,ORD_AU ,ITEM_QTY ,ITEM_MT ,ITEM_UNIT_RATE ,ITEM_PACK ,PACK_TYPE ,ITEM_DISCOUNT ,DISC_TYPE ,ITEM_CGST ,ITEM_SGST ,ITEM_IGST ,ITEM_TERMINAL_TAX ,ITEM_TCS ,ITEM_FREIGHT_PU ,ITEM_FREIGHT_TYPE ,ITEM_DELIVERY,ITEM_QTY_SEND,ITEM_S_TAX,ITEM_STATUS,AMD_NO,AMD_DATE,ITEM_DETAILS,ITEM_WEIGHT,SALE_TYPE,ITEM_CESS)values(@HSN_CODE,@SO_NO ,@ITEM_VOCAB ,@ITEM_SLNO ,@ITEM_CODE ,@ORD_AU ,@ITEM_QTY ,@ITEM_MT ,@ITEM_UNIT_RATE ,@ITEM_PACK ,@PACK_TYPE ,@ITEM_DISCOUNT ,@DISC_TYPE ,@ITEM_CGST ,@ITEM_SGST ,@ITEM_IGST ,@ITEM_TERMINAL_TAX ,@ITEM_TCS ,@ITEM_FREIGHT_PU ,@ITEM_FREIGHT_TYPE ,@ITEM_DELIVERY,@ITEM_QTY_SEND,@ITEM_S_TAX,@ITEM_STATUS,@AMD_NO,@AMD_DATE,@ITEM_DETAILS,@ITEM_WEIGHT,@SALE_TYPE,@ITEM_CESS)"
                                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                                cmd1.Parameters.AddWithValue("@SO_NO", soNoOutsourced.Text)
                                cmd1.Parameters.AddWithValue("@ITEM_VOCAB", TextBox8.Text)
                                cmd1.Parameters.AddWithValue("@ITEM_SLNO", TextBox7.Text)
                                cmd1.Parameters.AddWithValue("@ITEM_CODE", GridView5.Rows(I).Cells(1).Text)
                                cmd1.Parameters.AddWithValue("@ORD_AU", DropDownList3.SelectedValue)
                                cmd1.Parameters.AddWithValue("@ITEM_QTY", CDec(GridView5.Rows(I).Cells(4).Text))
                                cmd1.Parameters.AddWithValue("@ITEM_MT", CDec(GridView5.Rows(I).Cells(20).Text))
                                cmd1.Parameters.AddWithValue("@ITEM_UNIT_RATE", CDec(GridView5.Rows(I).Cells(5).Text))
                                cmd1.Parameters.AddWithValue("@ITEM_PACK", CDec(TextBox35.Text))
                                cmd1.Parameters.AddWithValue("@PACK_TYPE", DropDownList5.SelectedValue)
                                cmd1.Parameters.AddWithValue("@ITEM_DISCOUNT", CDec(TextBox33.Text))
                                cmd1.Parameters.AddWithValue("@DISC_TYPE", DropDownList4.SelectedValue)
                                cmd1.Parameters.AddWithValue("@ITEM_CGST", CDec(cgstPercentageOutsourced.Text))
                                cmd1.Parameters.AddWithValue("@ITEM_SGST", CDec(sgstPercentageOutsourced.Text))
                                cmd1.Parameters.AddWithValue("@ITEM_IGST", CDec(igstPercentageOutsourced.Text))
                                cmd1.Parameters.AddWithValue("@ITEM_TERMINAL_TAX", CDec(TextBox82.Text))
                                cmd1.Parameters.AddWithValue("@ITEM_TCS", CDec(TextBox89.Text))
                                cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_TYPE", DropDownList6.Text)
                                cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_PU", CDec(TextBox37.Text))
                                cmd1.Parameters.AddWithValue("@ITEM_DELIVERY", Date.ParseExact(TextBox44.Text, "dd-MM-yyyy", provider))
                                cmd1.Parameters.AddWithValue("@ITEM_QTY_SEND", 0.0)
                                cmd1.Parameters.AddWithValue("@ITEM_S_TAX", 0.0)
                                cmd1.Parameters.AddWithValue("@ITEM_STATUS", "PENDING")
                                cmd1.Parameters.AddWithValue("@ITEM_WEIGHT", CDec(TextBox50.Text))
                                cmd1.Parameters.AddWithValue("@AMD_NO", "0")
                                cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(soDateOutsourced.Text, "dd-MM-yyyy", provider))
                                cmd1.Parameters.AddWithValue("@ITEM_DETAILS", TextBox51.Text)
                                cmd1.Parameters.AddWithValue("@SALE_TYPE", SALE_TYPE)
                                cmd1.Parameters.AddWithValue("@ITEM_CESS", CDec(TextBox42.Text))
                                cmd1.Parameters.AddWithValue("@HSN_CODE", TextBox68.Text)
                                cmd1.ExecuteReader()
                                cmd1.Dispose()

                            Next
                            Dim dt1 As New DataTable()
                            dt1.Columns.AddRange(New DataColumn(9) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Qty"), New DataColumn("Unit Weight"), New DataColumn("Mat Ord. Qty"), New DataColumn("ORD_QTY_MT"), New DataColumn("Unit Price"), New DataColumn("Mat Desc")})
                            ViewState("mat") = dt1
                            Me.BINDGRID1()
                            myTrans.Commit()
                            conn_trans.Close()
                            Label680.Text = "Data Saved Succsessfully"
                        Catch ee As Exception
                            ' Roll back the transaction. 
                            myTrans.Rollback()
                            conn_trans.Close()
                            Label680.Text = "There was some Error, please contact EDP."
                        Finally
                            conn_trans.Close()
                        End Try



                    End Using

                End If


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label680.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
                Panel13.Visible = False
            End Try


        End Using
    End Sub

    Protected Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If TextBox7.Text = "" Or IsNumeric(TextBox7.Text) = False Then
            TextBox7.Focus()
            Return
        ElseIf DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Return
        ElseIf TextBox8.Text = "" Then
            TextBox8.Focus()
            Return
        ElseIf TextBox34.Text = "" Or IsNumeric(TextBox34.Text) = False Then
            TextBox34.Focus()
            Return
        ElseIf TextBox36.Text = "" Or IsNumeric(TextBox36.Text) = False Then
            TextBox36.Focus()
            Return
        ElseIf cgstPercentageOutsourced.Text = "" Or IsNumeric(cgstPercentageOutsourced.Text) = False Then
            cgstPercentageOutsourced.Focus()
            Return
        ElseIf igstPercentageOutsourced.Text = "" Or IsNumeric(igstPercentageOutsourced.Text) = False Then
            igstPercentageOutsourced.Focus()
            Return
        ElseIf TextBox35.Text = "" Or IsNumeric(TextBox35.Text) = False Then
            TextBox35.Focus()
            Return
        ElseIf TextBox44.Text = "" Or IsDate(TextBox44.Text) = False Then
            TextBox44.Focus()
            Return
        ElseIf TextBox33.Text = "" Or IsNumeric(TextBox33.Text) = False Then
            TextBox33.Focus()
            Return
        ElseIf sgstPercentageOutsourced.Text = "" Or IsNumeric(sgstPercentageOutsourced.Text) = False Then
            sgstPercentageOutsourced.Focus()
            Return
        ElseIf TextBox37.Text = "" Or IsNumeric(TextBox37.Text) = False Then
            TextBox37.Focus()
            Return
        ElseIf TextBox42.Text = "" Or IsNumeric(TextBox42.Text) = False Then
            TextBox42.Focus()
            Return
        End If
        Dim date_diff As Integer
        date_diff = DateDiff(DateInterval.Day, Today.Date, CDate(TextBox44.Text))
        If date_diff < 0 Then
            TextBox44.Focus()
            TextBox44.Text = ""
            Return
        End If

        If ((TextBox5.Text <> "D8888") And (TextBox5.Text <> "D3002") And ((CDec(sgstPercentageOutsourced.Text) + CDec(cgstPercentageOutsourced.Text) + CDec(igstPercentageOutsourced.Text)) = 0)) Then
            Label432.Text = "GST percentage cannot be left blank."
            Return
        End If

        'sono slno check
        conn.Open()
        count = 0
        ds.Tables.Clear()
        da = New SqlDataAdapter("select * from SO_MAT_ORDER where SO_NO ='" & TextBox86.Text & "' and ITEM_SLNO =" & TextBox7.Text, conn)
        count = da.Fill(ds, "SO_MAT_ORDER")
        conn.Close()
        If count > 0 Then
            TextBox7.Text = ""
            TextBox7.Focus()
            Return
        End If


        Button3.Enabled = True
        TextBox37.ReadOnly = True
        sgstPercentageOutsourced.ReadOnly = True
        TextBox33.ReadOnly = True
        TextBox65.ReadOnly = True
        TextBox36.ReadOnly = True
        TextBox44.Enabled = False
        po_taxTextBox.ReadOnly = True
        TextBox35.ReadOnly = True
        TextBox34.ReadOnly = True
        TextBox8.ReadOnly = True
        TextBox7.ReadOnly = True
        Panel13.Visible = True

        If DropDownList3.SelectedValue = "Set" Then
            TextBox49.Text = "0"
            TextBox50.Text = "0"
            TextBox49.ReadOnly = False
            TextBox50.ReadOnly = False

        ElseIf (DropDownList3.SelectedValue = "Pcs" Or DropDownList3.SelectedValue = "PCS") Then
            TextBox49.Text = "1"
            TextBox49.ReadOnly = False
            TextBox50.ReadOnly = False

        ElseIf (DropDownList3.SelectedValue = "Mt" Or DropDownList3.SelectedValue = "MT" Or DropDownList3.SelectedValue = "MTS") Then
            TextBox49.Text = "1"
            TextBox49.ReadOnly = False
            TextBox50.ReadOnly = False

        ElseIf DropDownList3.SelectedValue = "Activity" Then
            TextBox49.Text = "1"

        End If
        ''select f_item
        Dim item_au As String = ""
        If DropDownList3.SelectedValue = "Set" Or DropDownList3.SelectedValue = "Pcs" Or DropDownList3.SelectedValue = "PCS" Then
            item_au = "PCS"
        ElseIf (DropDownList3.SelectedValue = "Mt" Or DropDownList3.SelectedValue = "MT" Or DropDownList3.SelectedValue = "MTS") Then
            item_au = "MTS"
        ElseIf DropDownList3.SelectedValue = "Activity" Then
            item_au = "Activity"
        End If
    End Sub

    Protected Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        If TextBox40.Text = "" Then
            TextBox40.Focus()
            Return
        ElseIf TextBox43.Text = "" Or IsNumeric(TextBox43.Text) = False Then
            TextBox43.Focus()
            Return
        ElseIf TextBox52.Text = "" Or IsNumeric(TextBox52.Text) = False Then
            TextBox52.Focus()
            Return
        ElseIf TextBox53.Text = "" Or IsDate(TextBox53.Text) = False Then
            TextBox53.Focus()
            Return

        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select ITEM_AU from Outsource_F_ITEM where ITEM_CODE = '" & TextBox40.Text.Substring(0, (TextBox40.Text.IndexOf(",") - 1)) & "'", conn)
        count = da.Fill(dt)
        conn.Close()

        ''validation
        Dim po_type As String = ""
        conn.Open()
        mycommand.CommandText = "select PO_TYPE from ORDER_DETAILS WHERE SO_NO = '" & TextBox3.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            po_type = dr.Item("PO_TYPE")
            dr.Close()
        End If
        conn.Close()
        ''SERIAL NO SEARCH
        count = (GridView6.Rows.Count + 1)
        ''CALCULATATION
        Dim AU As String
        AU = "N/A"
        Dim mat_name, mat_code As String
        mat_name = ""
        mat_code = TextBox40.Text.Substring(0, TextBox40.Text.IndexOf(",") - 1)
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ITEM_AU, ITEM_NAME from Outsource_F_ITEM where ITEM_CODE = '" & mat_code & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            AU = dr.Item("ITEM_AU")
            mat_name = dr.Item("ITEM_NAME")
            dr.Close()
        End If
        conn.Close()
        Dim dt_table As DataTable = DirectCast(ViewState("foreign_outsourced"), DataTable)
        dt_table.Rows.Add(count, mat_code, mat_name, AU, TextBox43.Text, TextBox52.Text, TextBox53.Text, TextBox54.Text)
        ViewState("foreign_outsourced") = dt_table
        Me.BINDGRIDFGN_OUTSOURCED()
        DropDownList7.Items.Clear()
        For I As Integer = 0 To GridView6.Rows.Count - 1
            DropDownList7.Items.Add(GridView6.Rows(I).Cells(0).Text)
        Next
        DropDownList7.Items.Add("Select")
        DropDownList7.SelectedValue = "Select"

        If GridView6.Rows.Count > 0 Then
            Dim I As Integer = 0
            For I = 0 To GridView6.Rows.Count - 1
                Dim basic_price As Decimal
                ''Basic Price Count
                basic_price = CDec(GridView6.Rows(I).Cells(4).Text) * CDec(GridView6.Rows(I).Cells(5).Text)
                TextBox55.Text = basic_price + CDec(TextBox55.Text)
                TextBox56.Text = basic_price + CDec(TextBox56.Text)
            Next
        End If
        TextBox40.Text = ""
    End Sub

    Protected Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                ''DELETE EXITING DATA
                Dim order_type As String = ""
                conn.Open()
                Dim mc1 As New SqlCommand("select ORDER_TYPE from ORDER_DETAILS WHERE SO_NO = '" & TextBox3.Text & "'", conn)
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")
                    dr.Close()
                End If
                conn.Close()

                If order_type <> "Rate Contract" Then

                    mycommand = New SqlCommand("DELETE FROM PO_ORD_MAT WHERE PO_NO ='" & TextBox3.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()

                ElseIf order_type = "Rate Contract" Then

                    mycommand = New SqlCommand("update ORDER_DETAILS set SO_STATUS='RC' where SO_NO ='" & TextBox3.Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteReader()
                    mycommand.Dispose()

                End If

                ''DATA SAVE IN PO_ORD_MAT
                Dim ENTRY_TAX As Decimal = 1

                For I = 0 To GridView6.Rows.Count - 1
                    Dim po_slno As String = TextBox3.Text
                    Dim sno As Integer = GridView6.Rows(I).Cells(0).Text
                    Dim matcode As String = GridView6.Rows(I).Cells(1).Text
                    Dim matname As String = GridView6.Rows(I).Cells(2).Text
                    Dim mat_au As String = GridView6.Rows(I).Cells(3).Text
                    Dim matqty As String = GridView6.Rows(I).Cells(4).Text
                    Dim urate As String = GridView6.Rows(I).Cells(5).Text
                    Dim ddate As String = CDate(GridView6.Rows(I).Cells(6).Text.Trim)
                    Dim mat_details As String = GridView6.Rows(I).Cells(7).Text.Trim
                    If mat_details = "&nbsp;" Then
                        mat_details = ""
                    End If


                    Dim query As String = "Insert Into PO_ORD_MAT(HSN_CODE,MAT_AU,MAT_DESC,PO_NO, MAT_SLNO,MAT_CODE,MAT_NAME,MAT_QTY,MAT_UNIT_RATE,PF_TYPE,MAT_PACK,DISC_TYPE,MAT_DISCOUNT,SGST,CGST,IGST,CESS,ANAL_TAX,FREIGHT_TYPE,MAT_FREIGHT_PU,MAT_DELIVERY,MAT_QTY_RCVD,MAT_STATUS,AMD_NO,AMD_DATE,TOTAL_WT,TDS_SGST,TDS_CGST,TDS_IGST) values (@HSN_CODE,@MAT_AU,@MAT_DESC,@PO_NO, @MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_QTY,@MAT_UNIT_RATE,@PF_TYPE,@MAT_PACK,@DISC_TYPE,@MAT_DISCOUNT,@SGST,@CGST,@IGST,@CESS,@ANAL_TAX,@FREIGHT_TYPE,@MAT_FREIGHT_PU,@MAT_DELIVERY,@MAT_QTY_RCVD,@MAT_STATUS,@AMD_NO,@AMD_DATE,@TOTAL_WT,@TDS_SGST,@TDS_CGST,@TDS_IGST)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@PO_NO", po_slno)
                    cmd.Parameters.AddWithValue("@MAT_SLNO", sno)
                    cmd.Parameters.AddWithValue("@MAT_CODE", matcode)
                    cmd.Parameters.AddWithValue("@MAT_NAME", matname)
                    cmd.Parameters.AddWithValue("@MAT_AU", mat_au)
                    cmd.Parameters.AddWithValue("@MAT_QTY", matqty)
                    cmd.Parameters.AddWithValue("@MAT_UNIT_RATE", urate)
                    cmd.Parameters.AddWithValue("@PF_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_PACK", 0)
                    cmd.Parameters.AddWithValue("@DISC_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_DISCOUNT", 0)
                    cmd.Parameters.AddWithValue("@CGST", 0)
                    cmd.Parameters.AddWithValue("@SGST", 0)
                    cmd.Parameters.AddWithValue("@IGST", 0)
                    cmd.Parameters.AddWithValue("@CESS", 0)
                    cmd.Parameters.AddWithValue("@ANAL_TAX", 0)
                    cmd.Parameters.AddWithValue("@FREIGHT_TYPE", "")
                    cmd.Parameters.AddWithValue("@MAT_FREIGHT_PU", 0)
                    cmd.Parameters.AddWithValue("@MAT_DELIVERY", Date.ParseExact(ddate, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@MAT_QTY_RCVD", 0.0)
                    cmd.Parameters.AddWithValue("@MAT_STATUS", "Pending")
                    cmd.Parameters.AddWithValue("@MAT_DESC", mat_details)
                    cmd.Parameters.AddWithValue("@AMD_NO", "0")
                    cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox4.Text), "dd-MM-yyyy", provider))
                    If (mat_au = "MTS") Then
                        cmd.Parameters.AddWithValue("@TOTAL_WT", matqty)
                    Else
                        cmd.Parameters.AddWithValue("@TOTAL_WT", 0)
                    End If


                    cmd.Parameters.AddWithValue("@TDS_SGST", 0.00)
                    cmd.Parameters.AddWithValue("@TDS_CGST", 0.00)
                    cmd.Parameters.AddWithValue("@TDS_IGST", 0.00)
                    cmd.Parameters.AddWithValue("@HSN_CODE", TextBox61.Text)
                    cmd.ExecuteReader()
                    cmd.Dispose()

                Next
                ''update order details

                Dim cmd2 As New SqlCommand("update ORDER_DETAILS set NO_OF_ITEM=" & GridView6.Rows.Count & " , FULL_VALUE= " & CDec(TextBox56.Text) & ", PO_BASE_VALUE= " & CDec(TextBox56.Text) & " where SO_NO ='" & TextBox3.Text & "'", conn_trans, myTrans)
                cmd2.ExecuteReader()
                cmd2.Dispose()



                Dim dt_fgn As New DataTable()
                dt_fgn.Columns.AddRange(New DataColumn(7) {New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("MAT_UNIT_RATE"), New DataColumn("MAT_DELIVERY"), New DataColumn("MAT_DESC")})
                ViewState("foreign_outsourced") = dt_fgn
                Me.BINDGRIDFGN_OUTSOURCED()
                myTrans.Commit()
                conn_trans.Close()
                Label64.Text = "Data Saved Succsessfully"
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                Label64.Text = "There was some Error, please contact EDP."
            Finally
                conn_trans.Close()
            End Try

        End Using
    End Sub

    Protected Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim order_type As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE from ORDER_DETAILS WHERE SO_NO = '" & TextBox3.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")

            dr.Close()
        End If
        conn.Close()
        If order_type = "Rate Contract" Then
            Label64.Text = "This is a rate contract can't be submited"
            Return
        End If
        ''CHECK MATERIAL AVAILABILITY

        count = 0
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select PO_NO from PO_ORD_MAT WHERE PO_NO ='" & TextBox3.Text & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If count = 0 Then
            Label64.Text = "Please add material first"
            Return
        Else
            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry

                    ''update order details
                    'conn_trans.Open()
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox3.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()

                    Label64.Text = "Order Submited"
                    myTrans.Commit()
                    conn_trans.Close()
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn_trans.Close()
                    Label64.Text = "There was some Error, please contact EDP."
                Finally
                    conn_trans.Close()
                End Try

            End Using

        End If
    End Sub

    Protected Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim mat_code As String = ""
        Dim mat_name As String = ""
        If DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Return
        ElseIf (DropDownList3.SelectedValue = "Pcs" Or DropDownList3.SelectedValue = "PCS") Then
            If TextBox45.Text = "" Then
                TextBox45.Focus()
                Return
            Else
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select ITEM_CODE from Outsource_F_ITEM WHERE ITEM_CODE LIKE '" & TextBox45.Text.Substring(0, TextBox45.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(ds, "Outsource_F_ITEM")
                conn.Close()
                If count = 0 Then
                    TextBox45.Focus()
                    TextBox45.Text = ""
                    Return
                Else
                    mat_code = TextBox45.Text.Substring(0, TextBox45.Text.IndexOf(",") - 1)
                    mat_name = TextBox45.Text.Substring(TextBox45.Text.IndexOf(",") + 2)
                End If
            End If
        ElseIf (DropDownList3.SelectedValue = "Mt" Or DropDownList3.SelectedValue = "MT" Or DropDownList3.SelectedValue = "MTS") Then
            If TextBox45.Text = "" Then
                TextBox45.Focus()
                Return
            Else
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select  ITEM_CODE from Outsource_F_ITEM WHERE ITEM_CODE LIKE '" & TextBox45.Text.Substring(0, TextBox45.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(ds, "Outsource_F_ITEM")
                conn.Close()
                If count = 0 Then
                    TextBox45.Focus()
                    TextBox45.Text = ""
                    Return
                Else
                    mat_code = TextBox45.Text.Substring(0, TextBox45.Text.IndexOf(",") - 1)
                    mat_name = TextBox45.Text.Substring(TextBox45.Text.IndexOf(",") + 2)
                End If
            End If
        ElseIf DropDownList3.SelectedValue = "Activity" Then
            If TextBox47.Text = "" Then
                TextBox47.Focus()
                Return
            Else
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select ITEM_CODE from Outsource_F_ITEM WHERE ITEM_CODE LIKE '" & TextBox47.Text.Substring(0, TextBox47.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(ds, "Outsource_F_ITEM")
                conn.Close()
                If count = 0 Then
                    TextBox47.Focus()
                    TextBox47.Text = ""
                    Return
                Else
                    mat_code = TextBox47.Text.Substring(0, TextBox47.Text.IndexOf(",") - 1)
                    mat_name = TextBox47.Text.Substring(TextBox47.Text.IndexOf(",") + 2)
                End If
            End If
        End If
        If TextBox50.Text = "" Then
            TextBox50.Focus()
            Return
        ElseIf IsNumeric(TextBox50.Text) = False Then
            TextBox50.Text = ""
            TextBox50.Focus()
            Return
        End If
        count = GridView5.Rows.Count + 1



        Dim dt_x As DataTable = DirectCast(ViewState("OutSourcedFG"), DataTable)
        dt_x.Rows.Add(TextBox7.Text, mat_code, mat_name, HiddenField2.Value, TextBox34.Text, TextBox36.Text, DropDownList4.SelectedValue, TextBox33.Text, DropDownList5.SelectedValue, TextBox35.Text, DropDownList6.SelectedValue, TextBox37.Text, cgstPercentageOutsourced.Text, sgstPercentageOutsourced.Text, igstPercentageOutsourced.Text, "0.00", "0.00", TextBox44.Text, TextBox51.Text, "0.00", FormatNumber((CDec(TextBox50.Text) * CDec(TextBox34.Text)) / 1000, 3))
        ViewState("OutSourcedFG") = dt_x
        Me.BINDGRIDOUTSOURCED()
        TextBox45.Focus()
    End Sub


End Class