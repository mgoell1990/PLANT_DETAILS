Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class be
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ''SEARCH F MATERIAL PO AS PER BAL
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("SELECT DISTINCT (PO_ORD_MAT.PO_NO +' , '+ ORDER_DETAILS.so_actual) AS PO_NO FROM PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT .PO_NO =ORDER_DETAILS .SO_NO WHERE (ORDER_DETAILS .PO_TYPE ='RAW MATERIAL(IMP)' OR ORDER_DETAILS .PO_TYPE ='STORE MATERIAL(IMP)' OR ORDER_DETAILS .PO_TYPE ='OUTSOURCED ITEMS(IMP)') AND PO_ORD_MAT .MAT_STATUS ='PENDING'", conn)

            da.Fill(dt)
            be_purchase_order_no_DropDownList2.DataSource = dt
            be_purchase_order_no_DropDownList2.DataValueField = "PO_NO"
            be_purchase_order_no_DropDownList2.DataBind()
            be_purchase_order_no_DropDownList2.Items.Add("Select")
            be_purchase_order_no_DropDownList2.SelectedValue = "Select"
            conn.Close()
            ''SEARCH CHA WORK ORDER
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select distinct (WO_ORDER.PO_NO +' , '+ ORDER_DETAILS.so_actual) AS PO_NO from WO_ORDER JOIN ORDER_DETAILS ON WO_ORDER.PO_NO=ORDER_DETAILS.SO_NO where WO_TYPE ='CHA CHARGES' and W_STATUS ='PENDING'", conn)
            da.Fill(dt)
            be_cha_contract_DropDownList4.DataSource = dt
            be_cha_contract_DropDownList4.DataValueField = "PO_NO"
            be_cha_contract_DropDownList4.DataBind()
            be_cha_contract_DropDownList4.Items.Add("Select")
            be_cha_contract_DropDownList4.SelectedValue = "Select"
            conn.Close()

            'Dim dt2 As New DataTable()
            'dt2.Columns.AddRange(New DataColumn(19) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("BE QTY"), New DataColumn("OCEAN FREIGHT"), New DataColumn("INSURANCE"), New DataColumn("STAT. CHARGES"), New DataColumn("BCD"), New DataColumn("CVD"), New DataColumn("SAD"), New DataColumn("ED ON CVD"), New DataColumn("SHE ON CVD"), New DataColumn("CUST. ED. CESS"), New DataColumn("CUST. SHE CESS"), New DataColumn("UNIT PRICE"), New DataColumn("UNIT CENVAT"), New DataColumn("PARTY AMOUNT"), New DataColumn("IGST"), New DataColumn("CESS"), New DataColumn("SOCIAL WELFARE SURCHARGE"), New DataColumn("TOTAL CUSTOM DUTY")})
            'ViewState("mat2") = dt2
            'Me.BINDGRID2()
        End If

        If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    'Protected Sub BINDGRID2()
    '    GridView2.DataSource = DirectCast(ViewState("mat2"), DataTable)
    '    GridView2.DataBind()
    'End Sub

    Protected Sub be_purchase_order_no_DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles be_purchase_order_no_DropDownList2.SelectedIndexChanged
        If be_purchase_order_no_DropDownList2.SelectedValue = "Select" Then
            be_purchase_order_no_DropDownList2.Focus()
            Return
        End If
        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select DISTINCT MAT_SLNO from  PO_ORD_MAT where PO_NO='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "' and MAT_STATUS='PENDING'", conn)
        da.Fill(dt)
        be_material_slno_DropDownList3.Items.Clear()
        be_material_slno_DropDownList3.DataSource = dt
        be_material_slno_DropDownList3.DataValueField = "MAT_SLNO"
        be_material_slno_DropDownList3.DataBind()
        be_material_slno_DropDownList3.Items.Add("Select")
        be_material_slno_DropDownList3.SelectedValue = "Select"
        Dim mc1 As New SqlCommand
        str = ""
        mc1.CommandText = "SELECT (SUPL.SUPL_ID + ' , ' + SUPL.SUPL_NAME) AS SUPL_NAME FROM ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID WHERE ORDER_DETAILS.SO_NO='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox177.Text = dr.Item("SUPL_NAME")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
        be_material_slno_DropDownList3.Focus()
    End Sub

    Protected Sub be_material_slno_DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles be_material_slno_DropDownList3.SelectedIndexChanged
        If be_material_slno_DropDownList3.SelectedValue = "Select" Then
            be_material_slno_DropDownList3.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        ''mc1.CommandText = "SELECT PO_ORD_MAT .MAT_CODE ,PO_ORD_MAT .MAT_NAME ,MATERIAL .MAT_AU  FROM PO_ORD_MAT JOIN MATERIAL ON PO_ORD_MAT .MAT_CODE  =MATERIAL .MAT_CODE WHERE PO_ORD_MAT .MAT_SLNO =" & be_material_slno_DropDownList3.Text & "and po_ord_mat.po_no='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "'"
        mc1.CommandText = "DECLARE @TT TABLE(MAT_CODE VARCHAR(30), MAT_NAME VARCHAR(100), MAT_AU VARCHAR(20))

                            INSERT INTO @TT
                            SELECT PO_ORD_MAT .MAT_CODE ,PO_ORD_MAT .MAT_NAME ,MATERIAL .MAT_AU FROM PO_ORD_MAT JOIN MATERIAL ON PO_ORD_MAT .MAT_CODE  =MATERIAL .MAT_CODE WHERE PO_ORD_MAT .MAT_SLNO =" & be_material_slno_DropDownList3.Text & " and po_ord_mat.po_no='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "'

                            INSERT INTO @TT
                            SELECT PO_ORD_MAT .MAT_CODE ,PO_ORD_MAT .MAT_NAME ,Outsource_F_ITEM .ITEM_AU AS MAT_AU FROM PO_ORD_MAT JOIN Outsource_F_ITEM ON PO_ORD_MAT .MAT_CODE = Outsource_F_ITEM.ITEM_CODE WHERE PO_ORD_MAT .MAT_SLNO =" & be_material_slno_DropDownList3.Text & " and po_ord_mat.po_no='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "'
                            SELECT * FROM @TT"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox178.Text = dr.Item("MAT_NAME")
            TextBox183.Text = dr.Item("MAT_AU")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
    End Sub

    Protected Sub be_cha_contract_DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles be_cha_contract_DropDownList4.SelectedIndexChanged
        If be_cha_contract_DropDownList4.SelectedValue = "Select" Then
            be_cha_contract_DropDownList4.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select (SUPL.SUPL_ID + ' , ' + SUPL.SUPL_NAME) AS SUPL_NAME from SUPL JOIN WO_ORDER ON WO_ORDER.SUPL_ID=SUPL.SUPL_ID WHERE WO_ORDER.PO_NO ='" & be_cha_contract_DropDownList4.Text.Substring(0, be_cha_contract_DropDownList4.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox179.Text = dr.Item("SUPL_NAME")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
        ''work sl no put
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct w_slno from wo_order where po_no='" & be_cha_contract_DropDownList4.Text.Substring(0, be_cha_contract_DropDownList4.Text.IndexOf(",") - 1) & "' AND WO_TYPE='CHA CHARGES'", conn)
        da.Fill(dt)
        be_cha_work_slno_DropDownList6.Items.Clear()
        be_cha_work_slno_DropDownList6.DataSource = dt
        be_cha_work_slno_DropDownList6.DataValueField = "w_slno"
        be_cha_work_slno_DropDownList6.DataBind()
        conn.Close()
        be_cha_work_slno_DropDownList6.Items.Add("Select")
        be_cha_work_slno_DropDownList6.SelectedValue = "Select"
        be_cha_work_slno_DropDownList6.Focus()
    End Sub
    Protected Sub be_cha_work_slno_DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles be_cha_work_slno_DropDownList6.SelectedIndexChanged
        If be_cha_work_slno_DropDownList6.SelectedValue = "Select" Then
            be_cha_work_slno_DropDownList6.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select W_NAME FROM WO_ORDER WHERE PO_NO ='" & be_cha_contract_DropDownList4.Text.Substring(0, be_cha_contract_DropDownList4.Text.IndexOf(",") - 1) & "' AND W_SLNO='" & be_cha_work_slno_DropDownList6.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox180.Text = dr.Item("W_NAME")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
    End Sub
    Protected Sub BE_SAVE_Click(sender As Object, e As EventArgs) Handles BE_SAVE.Click
        If TextBox1.Text = "" Or IsDate(TextBox1.Text) = False Then
            TextBox1.Focus()
            Return
        ElseIf be_no_TextBox3.Text = "" Then
            be_no_TextBox3.Focus()
            Return
        ElseIf be_date_TextBox4.Text = "" Or IsDate(be_date_TextBox4.Text) = False Then
            be_date_TextBox4.Focus()
            Return
        ElseIf be_bl_no_TextBox6.Text = "" Then
            be_bl_no_TextBox6.Focus()
            Return
        ElseIf be_bl_no_date_TextBox7.Text = "" Or IsDate(be_bl_no_date_TextBox7.Text) = False Then
            be_bl_no_date_TextBox7.Focus()
            Return
        ElseIf be_conv_rate_TextBox9.Text = "" Or IsNumeric(be_conv_rate_TextBox9.Text) = False Then
            be_conv_rate_TextBox9.Focus()
            Return
        ElseIf be_purchase_order_no_DropDownList2.SelectedValue = "Select" Then
            be_purchase_order_no_DropDownList2.Focus()
            Return
        ElseIf be_material_slno_DropDownList3.SelectedValue = "Select" Then
            be_purchase_order_no_DropDownList2.Focus()
            Return
        ElseIf be_transport_mode_DropDownList1.SelectedValue = "Select" Then
            be_transport_mode_DropDownList1.Focus()
            Return
        ElseIf be_ship_flight_name_TextBox8.Text = "" Then
            be_ship_flight_name_TextBox8.Focus()
            Return
        ElseIf be_cha_contract_DropDownList4.SelectedValue = "Select" Then
            be_cha_contract_DropDownList4.Focus()
            Return
        ElseIf be_cha_work_slno_DropDownList6.SelectedValue = "Select" Then
            be_cha_work_slno_DropDownList6.Focus()
            Return
        ElseIf be_quantity_TextBox5.Text = "" Or IsNumeric(be_quantity_TextBox5.Text) = False Then
            be_quantity_TextBox5.Focus()
            Return
        ElseIf CDec(be_quantity_TextBox5.Text) = 0 Then
            be_quantity_TextBox5.Focus()
            be_quantity_TextBox5.Text = ""
            Return
        ElseIf be_ocean_freight_TextBox10.Text = "" Or IsNumeric(be_ocean_freight_TextBox10.Text) = False Then
            be_ocean_freight_TextBox10.Focus()
            Return
        ElseIf be_insurance_sea_TextBox18.Text = "" Or IsNumeric(be_insurance_sea_TextBox18.Text) = False Then
            be_insurance_sea_TextBox18.Focus()
            Return
        ElseIf be_bcd_TextBox12.Text = "" Or IsNumeric(be_bcd_TextBox12.Text) = False Then
            be_bcd_TextBox12.Focus()
            Return
        ElseIf be_cvd_TextBox13.Text = "" Or IsNumeric(be_cvd_TextBox13.Text) = False Then
            be_cvd_TextBox13.Focus()
            Return
        ElseIf be_ed_cess_on_cvd_TextBox14.Text = "" Or IsNumeric(be_ed_cess_on_cvd_TextBox14.Text) = False Then
            be_ed_cess_on_cvd_TextBox14.Focus()
            Return
        ElseIf be_she_cess_on_cvd_TextBox15.Text = "" Or IsNumeric(be_she_cess_on_cvd_TextBox15.Text) = False Then
            be_she_cess_on_cvd_TextBox15.Focus()
            Return
        ElseIf BE_SAT_CHARGE_TextBox183.Text = "" Or IsNumeric(BE_SAT_CHARGE_TextBox183.Text) = False Then
            BE_SAT_CHARGE_TextBox183.Focus()
            Return
        ElseIf be_custom_edu_cess_TextBox16.Text = "" Or IsNumeric(be_custom_edu_cess_TextBox16.Text) = False Then
            be_custom_edu_cess_TextBox16.Focus()
            Return

        ElseIf be_sad_TextBox181.Text = "" Or IsNumeric(be_sad_TextBox181.Text) = False Then
            be_sad_TextBox181.Focus()
            Return
        End If

        '''''''''''''''''''''''''''''''''

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                '''''''''''''''''''''''''''''''''''''''''''''
                ''po validation 
                dt.Clear()
                count = 0
                conn.Open()
                da = New SqlDataAdapter("SELECT BE_NO FROM BE_DETAILS WHERE BE_NO = '" & be_no_TextBox3.Text & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count > 0 Then
                    Label501.Text = "BE NO ALREDY EXISTS"
                    be_no_TextBox3.Focus()
                    be_no_TextBox3.Text = ""
                    Return
                End If
                ''po validation 
                Dim podate As Date
                Dim mc1 As New SqlCommand
                conn.Open()
                mc1.CommandText = "select SO_ACTUAL_DATE from ORDER_DETAILS where so_no='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    podate = dr.Item("SO_ACTUAL_DATE")
                    dr.Close()
                Else
                    conn.Close()
                End If
                conn.Close()
                Dim BE_DATE As Date = be_date_TextBox4.Text
                If podate > BE_DATE Then
                    Label501.Visible = True
                    Label501.Text = "Please check Purchase order date"
                    Return
                Else
                    Label501.Visible = False
                End If
                Dim disc_type, pf_type, freight_type As String
                disc_type = ""
                pf_type = ""
                freight_type = ""
                Dim qty, rqty, tolerance As Decimal
                tolerance = 0
                Dim unit_price As Decimal
                Dim AMD_NO As String = ""
                Dim mat_id As String = ""
                str = ""
                ''search material details
                conn.Open()
                Dim mc2 As New SqlCommand
                mc2.CommandText = "SELECT  MAX(MAT_CODE) as mat_code,MAX(AMD_NO) as AMD_NO, SUM(MAT_UNIT_RATE ) AS MAT_UNIT_RATE FROM PO_ORD_MAT WHERE PO_NO='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "' AND MAT_SLNO=" & be_material_slno_DropDownList3.SelectedValue & " and AMD_DATE < ='" & BE_DATE.Year & "-" & BE_DATE.Month & "-" & BE_DATE.Day & "'"
                mc2.Connection = conn
                dr = mc2.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    AMD_NO = dr.Item("AMD_NO")
                    mat_id = dr.Item("MAT_CODE")
                    unit_price = dr.Item("MAT_UNIT_RATE")
                    dr.Close()
                Else
                    conn.Close()
                End If
                conn.Close()

                conn.Open()
                mc1.CommandText = "select SUM(BE_QTY) AS MAT_QTY_RCVD FROM BE_DETAILS WHERE mat_slno=" & be_material_slno_DropDownList3.SelectedValue & " and po_no='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If IsDBNull(dr.Item("MAT_QTY_RCVD")) Then
                        rqty = 0.00
                    Else
                        rqty = dr.Item("MAT_QTY_RCVD")
                    End If

                    dr.Close()
                End If
                conn.Close()

                conn.Open()
                mc1.CommandText = "select SUM(MAT_QTY) AS MAT_QTY , SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD FROM PO_ORD_MAT WHERE mat_slno=" & be_material_slno_DropDownList3.SelectedValue & " and po_no='" & be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1) & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    qty = dr.Item("MAT_QTY")
                    'rqty = dr.Item("MAT_QTY_RCVD")
                    dr.Close()
                End If
                conn.Close()
                If (qty - rqty) < be_quantity_TextBox5.Text Then
                    be_quantity_TextBox5.Focus()
                    be_quantity_TextBox5.Text = ""
                    Label501.Visible = True
                    Label501.Text = "BE quantity is greater tha Purchase order balance quantity. PO balance is " + (qty - rqty) + "MT."
                    Return
                End If
                ''search cha details
                Dim w_qty, w_complite, w_unit_price, W_discount As Decimal
                Dim MC As New SqlCommand
                Dim w_name, w_au As String
                conn.Open()
                mc2.CommandText = "select sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_STAX) as W_STAX,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,sum(W_WCTAX) as W_WCTAX,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & be_cha_contract_DropDownList4.Text.Substring(0, be_cha_contract_DropDownList4.Text.IndexOf(",") - 1) & "' AND W_SLNO ='" & be_cha_work_slno_DropDownList6.SelectedValue & "'  and AMD_DATE < ='" & BE_DATE.Year & "-" & BE_DATE.Month & "-" & BE_DATE.Day & "'"
                mc2.Connection = conn
                dr = mc2.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    w_qty = dr.Item("W_QTY")
                    w_complite = dr.Item("W_COMPLITED")
                    w_unit_price = dr.Item("W_UNIT_PRICE")
                    W_discount = dr.Item("W_DISCOUNT")
                    w_name = dr.Item("W_NAME")
                    w_au = dr.Item("W_AU")
                    dr.Close()

                End If
                conn.Close()
                If w_qty < CDec(be_quantity_TextBox5.Text) Then
                    Label501.Visible = True
                    Label501.Text = "CHA Job qty. less than be qty"
                    Return
                End If
                ''cha unit value
                Dim cha_price, cha_disc As Decimal
                cha_disc = (w_unit_price * W_discount) / 100
                cha_price = FormatNumber(w_unit_price - cha_disc, 4)

                ''calculation
                Dim con_rate, be_qty, ocn_freight, total_value, sea_insu, sat_charg, bcd, cvd, sad, igst, cess, ed_cvd, she_cvd, cust_edu, cust_she As New Decimal(0)
                be_qty = CDec(be_quantity_TextBox5.Text)
                sat_charg = CDec(BE_SAT_CHARGE_TextBox183.Text)
                con_rate = unit_price * CDec(be_conv_rate_TextBox9.Text)
                ocn_freight = CDec(be_ocean_freight_TextBox10.Text)
                sea_insu = CDec(be_insurance_sea_TextBox18.Text)
                bcd = CDec(be_bcd_TextBox12.Text)
                cvd = CDec(be_cvd_TextBox13.Text)
                sad = CDec(be_sad_TextBox181.Text)
                igst = CDec(be_igst_TextBox.Text)
                cess = CDec(be_cess_TextBox.Text)

                ed_cvd = CDec(be_ed_cess_on_cvd_TextBox14.Text)
                she_cvd = CDec(be_she_cess_on_cvd_TextBox15.Text)
                cust_edu = CDec(be_custom_edu_cess_TextBox16.Text)
                cust_she = CDec(be_custom_she_cess_TextBox17.Text)

                ''material unit value
                Dim cenvat_credit, custom_duty, UNIT_VALUE, UNIT_CENVAT As Decimal
                cenvat_credit = cvd + sad + ed_cvd + she_cvd + igst + cess
                custom_duty = bcd + cvd + sad + ed_cvd + she_cvd + cust_edu + cust_she + igst + cess
                ''total_value = custom_duty + (con_rate * be_qty) + CDec(BE_SAT_CHARGE_TextBox183.Text) + sea_insu
                total_value = con_rate * be_qty
                UNIT_VALUE = FormatNumber(total_value / be_qty, 6)
                UNIT_CENVAT = FormatNumber(cenvat_credit / be_qty, 6)



                ''SAVE BE_TABLE
                'conn_trans.Open()
                Dim cmd As New SqlCommand
                Dim Query As String = "Insert Into BE_DETAILS(IGST_PERCENTAGE,CHA_RCD_QTY,TOTAL_CUSTOM_DUTY,SOCIAL_WEL_SURCHARGE,IGST,CESS,EMP_ID,UNIT_CENVAT,UNIT_PRICE,BE_NO,BE_DATE,BL_NO,BL_DATE,INV_NO,INV_DATE,PO_NO,MAT_SLNO,CONV_RATE,TRANS_MODE,SHIP_FLIGHT,CHA_ORDER,CHA_SLNO,BE_QTY,RCVD_QTY,OCEAN_FREIGHT,INSURANCE,SAT_CHARGE,BCD,CVD,SAD,ED_ON_CVD,SHE_ON_CVD,CUST_EDU_CESS,CUST_SHE_CESS,BE_STATUS,PARTY_AMT)" &
                         "VALUES(@IGST_PERCENTAGE,@CHA_RCD_QTY,@TOTAL_CUSTOM_DUTY,@SOCIAL_WEL_SURCHARGE,@IGST,@CESS,@EMP_ID,@UNIT_CENVAT,@UNIT_PRICE,@BE_NO,@BE_DATE,@BL_NO,@BL_DATE,@INV_NO,@INV_DATE,@PO_NO,@MAT_SLNO,@CONV_RATE,@TRANS_MODE,@SHIP_FLIGHT,@CHA_ORDER,@CHA_SLNO,@BE_QTY,@RCVD_QTY,@OCEAN_FREIGHT,@INSURANCE,@SAT_CHARGE,@BCD,@CVD,@SAD,@ED_ON_CVD,@SHE_ON_CVD,@CUST_EDU_CESS,@CUST_SHE_CESS,@BE_STATUS,@PARTY_AMT)"
                cmd = New SqlCommand(Query, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@BE_NO", be_no_TextBox3.Text)
                cmd.Parameters.AddWithValue("@BE_DATE", Date.ParseExact(be_date_TextBox4.Text, "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@BL_NO", be_bl_no_TextBox6.Text)
                cmd.Parameters.AddWithValue("@BL_DATE", Date.ParseExact(be_bl_no_date_TextBox7.Text, "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@INV_NO", TextBox181.Text)
                cmd.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(TextBox182.Text, "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@PO_NO", be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1))
                cmd.Parameters.AddWithValue("@MAT_SLNO", be_material_slno_DropDownList3.SelectedValue)
                cmd.Parameters.AddWithValue("@CONV_RATE", be_conv_rate_TextBox9.Text)
                cmd.Parameters.AddWithValue("@TRANS_MODE", be_transport_mode_DropDownList1.SelectedValue)
                cmd.Parameters.AddWithValue("@SHIP_FLIGHT", be_ship_flight_name_TextBox8.Text)
                cmd.Parameters.AddWithValue("@CHA_ORDER", be_cha_contract_DropDownList4.Text.Substring(0, be_cha_contract_DropDownList4.Text.IndexOf(",") - 1))
                cmd.Parameters.AddWithValue("@CHA_SLNO", be_cha_work_slno_DropDownList6.SelectedValue)
                cmd.Parameters.AddWithValue("@BE_QTY", be_quantity_TextBox5.Text)
                cmd.Parameters.AddWithValue("@RCVD_QTY", 0.0)
                cmd.Parameters.AddWithValue("@OCEAN_FREIGHT", be_ocean_freight_TextBox10.Text)
                cmd.Parameters.AddWithValue("@INSURANCE", be_insurance_sea_TextBox18.Text)
                cmd.Parameters.AddWithValue("@SAT_CHARGE", BE_SAT_CHARGE_TextBox183.Text)
                cmd.Parameters.AddWithValue("@BCD", be_bcd_TextBox12.Text)
                cmd.Parameters.AddWithValue("@CVD", be_cvd_TextBox13.Text)
                cmd.Parameters.AddWithValue("@SAD", be_sad_TextBox181.Text)
                cmd.Parameters.AddWithValue("@IGST", be_igst_TextBox.Text)
                cmd.Parameters.AddWithValue("@CESS", be_cess_TextBox.Text)
                cmd.Parameters.AddWithValue("@ED_ON_CVD", be_ed_cess_on_cvd_TextBox14.Text)
                cmd.Parameters.AddWithValue("@SHE_ON_CVD", be_she_cess_on_cvd_TextBox15.Text)
                cmd.Parameters.AddWithValue("@CUST_EDU_CESS", 0)
                cmd.Parameters.AddWithValue("@SOCIAL_WEL_SURCHARGE", be_custom_edu_cess_TextBox16.Text)
                cmd.Parameters.AddWithValue("@CUST_SHE_CESS", be_custom_she_cess_TextBox17.Text)
                cmd.Parameters.AddWithValue("@UNIT_PRICE", UNIT_VALUE)
                cmd.Parameters.AddWithValue("@UNIT_CENVAT", UNIT_CENVAT)
                cmd.Parameters.AddWithValue("@BE_STATUS", "PENDING")
                cmd.Parameters.AddWithValue("@PARTY_AMT", (con_rate * be_qty))
                cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                cmd.Parameters.AddWithValue("@TOTAL_CUSTOM_DUTY", custom_duty - igst)
                cmd.Parameters.AddWithValue("@CHA_RCD_QTY", 0)
                cmd.Parameters.AddWithValue("@IGST_PERCENTAGE", CDec(be_igst_percentage.Text))
                cmd.ExecuteReader()
                cmd.Dispose()


                ''PROV FOR MATERIAL TRANSIT 
                'ledger_post(be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1), be_no_TextBox3.Text, "", sit_head, "Dr", total_value, "RMT")
                ''PARTY AMT
                'ledger_post(be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1), be_no_TextBox3.Text, TextBox177.Text.Substring(0, TextBox177.Text.IndexOf(",") - 1), party_prov_head, "Cr", (con_rate * be_qty), "PROV")
                ''CUSTOM DUTY
                'ledger_post(be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1), be_no_TextBox3.Text, "", custom_head, "Cr", custom_duty, "CD")
                ''SAT CHARGE
                'ledger_post(be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1), be_no_TextBox3.Text, "", sat_head, "Cr", CDec(BE_SAT_CHARGE_TextBox183.Text), "STAT")
                ''insurance CHARGE
                'ledger_post(be_purchase_order_no_DropDownList2.Text.Substring(0, be_purchase_order_no_DropDownList2.Text.IndexOf(",") - 1), be_no_TextBox3.Text, "", INSU_HEAD, "Cr", sea_insu, "INSU")

                be_no_TextBox3.Text = ""
                be_date_TextBox4.Text = ""
                be_bl_no_date_TextBox7.Text = ""
                be_bl_no_date_TextBox7.Text = ""
                TextBox181.Text = ""
                TextBox182.Text = ""
                be_conv_rate_TextBox9.Text = ""
                be_ship_flight_name_TextBox8.Text = ""
                be_quantity_TextBox5.Text = ""
                be_ocean_freight_TextBox10.Text = ""
                be_insurance_sea_TextBox18.Text = ""
                BE_SAT_CHARGE_TextBox183.Text = ""
                be_bcd_TextBox12.Text = ""
                be_cvd_TextBox13.Text = ""
                be_sad_TextBox181.Text = ""
                be_ed_cess_on_cvd_TextBox14.Text = ""
                be_she_cess_on_cvd_TextBox15.Text = ""
                be_custom_edu_cess_TextBox16.Text = ""
                be_custom_she_cess_TextBox17.Text = ""
                ''total
                ''update po_ord_mat
                ''update wo_order for cha

                '''''''''''''''''''''''''''''''''''''''''''''
                myTrans.Commit()
                Label501.Visible = True
                Label501.Text = "Data saved successfully."
                result = True
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label501.Visible = True
                Label501.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using




    End Sub
    Protected Sub ledger_post(po_no As String, be_no As String, supl_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
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
            cmd.Parameters.AddWithValue("@PO_NO", po_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", be_no)
            cmd.Parameters.AddWithValue("@SUPL_ID", supl_id)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", CDate(TextBox1.Text))
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

End Class