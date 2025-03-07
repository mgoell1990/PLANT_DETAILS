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
Public Class crr
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Panel1.Visible = False
            type_DropDown.AutoPostBack = False
            Panel1.Visible = False
            ''search_Label.Visible = True
            type_DropDown.Visible = True
            search_DropDown.Visible = True
            new_button.Visible = True
            view_button.Visible = True
            type_DropDown.Items.Clear()
            search_DropDown.Items.Clear()
            ''search_Label.Text = "CRR"
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

            Panel16.Visible = True
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("CRR No"), New DataColumn("Chalan No"), New DataColumn("Chalan Date"), New DataColumn("BE No"), New DataColumn("BE Date"), New DataColumn("BL No"), New DataColumn("BL Date"), New DataColumn("Order Qty"), New DataColumn("Chalan Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Short Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("No Of Bag"), New DataColumn("Bag Weight"), New DataColumn("Trans Charge"), New DataColumn("Short Price"), New DataColumn("Amd No"), New DataColumn("Total Mt.")})
            ViewState("mat") = dt
            crr_gridview.Font.Size = 8
            Me.BINDGRID1()

            suplnameTextBox.Attributes.Add("readonly", "readonly")
            transnameTextBox.Attributes.Add("readonly", "readonly")
            TextBox174.Attributes.Add("readonly", "readonly")
            shipnameTextBox.Attributes.Add("readonly", "readonly")
            matnameTextBox.Attributes.Add("readonly", "readonly")
            au_TextBox.Attributes.Add("readonly", "readonly")
            bedate_TextBox.Attributes.Add("readonly", "readonly")
            bldate_TextBox.Attributes.Add("readonly", "readonly")

        End If

        Delvdate7_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("storeAccess")) Or Session("storeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub BINDGRID1()
        crr_gridview.DataSource = DirectCast(ViewState("mat"), DataTable)
        crr_gridview.DataBind()

    End Sub


    Protected Sub pono_DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles pono_DropDownList.SelectedIndexChanged
        Dim working_date As Date
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        End If
        working_date = CDate(TextBox2.Text)
        If pono_DropDownList.SelectedValue = "Select" Then
            pono_DropDownList.Focus()
            suplnameTextBox.Text = ""
            Return
        End If
        conn.Open()

        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select DISTINCT MAT_SLNO from  PO_ORD_MAT where PO_NO='" & pono_DropDownList.Text & "' and MAT_STATUS='PENDING'", conn)
        da.Fill(ds5, "PO_ORD_MAT")
        mat_sl_noDropDownList.DataSource = ds5.Tables("PO_ORD_MAT")
        mat_sl_noDropDownList.DataValueField = "MAT_SLNO"
        mat_sl_noDropDownList.DataBind()
        Dim mc1 As New SqlCommand
        str = ""
        Dim mode_trans As String = ""
        mc1.CommandText = "SELECT ORDER_DETAILS.DELIVERY_TERM,ORDER_DETAILS.MODE_OF_DESPATCH,SUPL .SUPL_NAME FROM ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID WHERE ORDER_DETAILS.SO_NO='" & pono_DropDownList.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            str = dr.Item("DELIVERY_TERM")
            suplnameTextBox.Text = dr.Item("SUPL_NAME")
            mode_trans = dr.Item("MODE_OF_DESPATCH")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
        If str = "By Customer" Or str = "By Supplier(Door Delivery)" Or str = "N/A" Then
            trans_DropDownList.Items.Add("N/A")
            trans_DropDownList.SelectedValue = "N/A"
            DropDownList21.Items.Add("N/A")
            DropDownList21.SelectedValue = "N/A"
            DropDownList21.Enabled = False
            trans_DropDownList.Enabled = False
            transnameTextBox.Text = "N/A"
            TextBox174.Text = "N/A"
            lr_rr_noTextBox.Focus()

        Else
            trans_DropDownList.Enabled = True
            DropDownList21.Enabled = True
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct WO_ORDER .PO_NO + ' , ' + SUPL .SUPL_NAME AS PO_NO FROM ORDER_DETAILS JOIN WO_ORDER ON ORDER_DETAILS .SO_NO =WO_ORDER .PO_NO join SUPL on ORDER_DETAILS.PARTY_CODE =SUPL.SUPL_ID WHERE ORDER_DETAILS .PO_TYPE ='FREIGHT INWARD' AND WO_ORDER.W_STATUS='PENDING' and WO_ORDER.W_END_DATE >'" & working_date.Year & "-" & working_date.Month & "-" & working_date.Day & "' AND WO_ORDER.WO_TYPE='FREIGHT INWARD' ORDER BY PO_NO", conn)
            da.Fill(dt)
            trans_DropDownList.DataSource = dt
            trans_DropDownList.DataValueField = "PO_NO"
            trans_DropDownList.DataBind()
            conn.Close()
            trans_DropDownList.Items.Add("Select")
            trans_DropDownList.SelectedValue = "Select"
            trans_DropDownList.Focus()
            pono_DropDownList.Enabled = False
        End If
        If mode_trans = "By Hand" Then
            lr_rr_noTextBox.Enabled = False
            lr_rr_noTextBox.Text = "N/A"
            trans_DropDownList.Items.Clear()
            trans_DropDownList.Items.Add("N/A")
            trans_DropDownList.SelectedValue = "N/A"
            trans_DropDownList.Enabled = False
        ElseIf mode_trans = "By Road" Then
            lr_rr_noTextBox.Enabled = True
            lr_rr_noTextBox.Text = ""
        ElseIf mode_trans = "By Train" Then
            lr_rr_noTextBox.Enabled = True
            lr_rr_noTextBox.Text = ""
        ElseIf mode_trans = "By Air" Then
            lr_rr_noTextBox.Enabled = True
            lr_rr_noTextBox.Text = ""
        ElseIf mode_trans = "By Ship" Then
            lr_rr_noTextBox.Enabled = True
            lr_rr_noTextBox.Text = "Select"
        ElseIf mode_trans = "By Post" Then
            lr_rr_noTextBox.Enabled = False
            lr_rr_noTextBox.Text = "N/A"
            trans_DropDownList.Items.Clear()
            trans_DropDownList.Items.Add("N/A")
            trans_DropDownList.SelectedValue = "N/A"
            trans_DropDownList.Enabled = False
        End If
        mat_sl_noDropDownList.Items.Add("Select")
        mat_sl_noDropDownList.SelectedValue = "Select"
    End Sub

    Protected Sub trans_DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles trans_DropDownList.SelectedIndexChanged
        If trans_DropDownList.SelectedValue = "Select" Then
            trans_DropDownList.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select SUPL.SUPL_NAME from SUPL JOIN WO_ORDER ON WO_ORDER.SUPL_ID=SUPL.SUPL_ID WHERE WO_ORDER.PO_NO ='" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            transnameTextBox.Text = dr.Item("SUPL_NAME")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
        ''work sl no put
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct (CONVERT(varchar(10), w_slno) + ' , ' + W_NAME) as w_slno from wo_order where po_no='" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1) & "' AND WO_TYPE='FREIGHT INWARD'", conn)
        da.Fill(dt)
        DropDownList21.Items.Clear()
        DropDownList21.DataSource = dt
        DropDownList21.DataValueField = "w_slno"
        DropDownList21.DataBind()
        conn.Close()
        DropDownList21.Items.Add("Select")
        DropDownList21.SelectedValue = "Select"
        DropDownList21.Focus()
    End Sub

    Protected Sub DropDownList21_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList21.SelectedIndexChanged
        If DropDownList21.SelectedValue = "Select" Then
            DropDownList21.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select W_NAME FROM WO_ORDER WHERE PO_NO ='" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1) & "' AND W_SLNO='" & DropDownList21.Text.Substring(0, DropDownList21.Text.IndexOf(",") - 1) & "'"
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

    Protected Sub mat_sl_noDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles mat_sl_noDropDownList.SelectedIndexChanged
        If mat_sl_noDropDownList.SelectedValue = "Select" Then
            mat_sl_noDropDownList.Focus()
            matnameTextBox.Text = ""
            au_TextBox.Text = ""
            Return
        End If

        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "SELECT PO_ORD_MAT .MAT_CODE ,PO_ORD_MAT .MAT_NAME ,MATERIAL .MAT_AU  FROM PO_ORD_MAT JOIN MATERIAL  ON PO_ORD_MAT .MAT_CODE  =MATERIAL .MAT_CODE WHERE PO_ORD_MAT .MAT_SLNO =" & mat_sl_noDropDownList.Text & "and po_ord_mat.po_no='" & pono_DropDownList.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            matnameTextBox.Text = dr.Item("MAT_NAME")
            au_TextBox.Text = dr.Item("MAT_AU")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
        crr_add_Button.Enabled = True
    End Sub

    Protected Sub crr_add_Button_Click(sender As Object, e As EventArgs) Handles crr_add_Button.Click
        If trans_DropDownList.SelectedValue = "Select" Then
            trans_DropDownList.Focus()
            Return
        ElseIf lr_rr_noTextBox.Text = "" Then
            lr_rr_noTextBox.Focus()
            Return
        ElseIf mat_sl_noDropDownList.SelectedValue = "Select" Then
            mat_sl_noDropDownList.Focus()
            Return
        ElseIf be_TextBox.Text = "" Then
            be_TextBox.Focus()
            Return
        ElseIf bedate_TextBox.Text = "" Then
            bedate_TextBox.Focus()
            Return
        ElseIf bl_noTextBox.Text = "" Then
            bl_noTextBox.Focus()
            Return
        ElseIf bldate_TextBox.Text = "" Then
            bldate_TextBox.Focus()
            Return
        ElseIf IsDate(chalandate_TextBox.Text) = False Then
            chalandate_TextBox.Focus()
            Return
        ElseIf chalan_qty_TextBox.Text = "" Or IsNumeric(chalan_qty_TextBox.Text) = False Then
            chalan_qty_TextBox.Focus()
            Return
        ElseIf rcv_qty_TextBox.Text = "" Or IsNumeric(rcv_qty_TextBox.Text) = False Then
            rcv_qty_TextBox.Focus()
            Return
        ElseIf no_of_bagTextBox.Text = "" Or IsNumeric(no_of_bagTextBox.Text) = False Then
            no_of_bagTextBox.Focus()
            Return
        ElseIf bag_weightTextBox.Text = "" Or IsNumeric(bag_weightTextBox.Text) = False Then
            bag_weightTextBox.Focus()
            Return
        ElseIf DropDownList21.SelectedValue = "Select" Then
            DropDownList21.Focus()
            Return
        End If
        If CDec(chalan_qty_TextBox.Text) < CDec(rcv_qty_TextBox.Text) Then
            rcv_qty_TextBox.Focus()
            rcv_qty_TextBox.Text = ""
            ERRLabel.Visible = True
            ERRLabel.Text = "Challan quantity cannot be less than Received quantity."
            Return
        ElseIf Actual_Material_Weight.Text = "" Then
            Actual_Material_Weight.Focus()
            Return
        ElseIf IsNumeric(Actual_Material_Weight.Text) = False Then
            Actual_Material_Weight.Focus()
            Actual_Material_Weight.Text = 0
            Return
        End If

        If (trans_DropDownList.SelectedValue <> "Select" And trans_DropDownList.SelectedValue <> "N/A") And CDec(Actual_Material_Weight.Text) = 0 Then
            ERRLabel.Visible = True
            ERRLabel.Text = "Please enter value for Total MT textbox."
            Return
        End If


        For i = 0 To crr_gridview.Rows.Count - 1
            If mat_sl_noDropDownList.SelectedValue = crr_gridview.Rows(i).Cells(0).Text Then
                mat_sl_noDropDownList.Focus()
                Return
            End If
        Next


        Dim podate As Date
        Dim po_type As String = ""
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select SO_ACTUAL_DATE,PO_TYPE from ORDER_DETAILS where so_no='" & pono_DropDownList.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            podate = dr.Item("SO_ACTUAL_DATE")
            po_type = dr.Item("PO_TYPE")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()


        ''be search
        If po_type = "STORE MATERIAL(IMP)" Then
            conn.Open()
            Dim dt1 As New DataTable()
            da = New SqlDataAdapter("SELECT PO_ORD_MAT .PO_NO  FROM BE_DETAILS JOIN PO_ORD_MAT  ON BE_DETAILS .PO_NO =PO_ORD_MAT.PO_NO AND BE_DETAILS .MAT_SLNO =PO_ORD_MAT .MAT_SLNO  WHERE BE_DETAILS .BE_NO ='" & be_TextBox.Text & "' AND PO_ORD_MAT .PO_NO ='" & pono_DropDownList.Text & "' AND BE_DETAILS .BE_STATUS ='PENDING' AND PO_ORD_MAT .MAT_SLNO =" & mat_sl_noDropDownList.Text, conn)
            count = da.Fill(dt1)
            conn.Close()
            If count = 0 Then
                ERRLabel.Text = "BE No Not Valid"
                ERRLabel.Visible = True
                be_TextBox.Focus()
                Return
            End If
        Else
            bedate_TextBox.Text = "N/A"
            bldate_TextBox.Text = "N/A"
            bl_noTextBox.Text = "N/A"
            shipnameTextBox.Text = "N/A"
            be_TextBox.Text = "N/A"
        End If

        Dim CHLN_DATE As Date = chalandate_TextBox.Text
        If podate > CHLN_DATE Then
            ERRLabel.Visible = True
            ERRLabel.Text = "Please check Purchase order date"
            Return
        Else
            ERRLabel.Visible = False
        End If
        Dim disc_type, pf_type, freight_type As String
        disc_type = ""
        pf_type = ""
        freight_type = ""
        Dim qty, rqty, sqty, eqty, exqty, tolerance, tole_value, MAT_QTY_CONSIDERED_FOR_PAYMENT As Decimal
        tolerance = 0
        Dim FREIGHT, unit_price, ord_packing, ord_discount As Decimal
        Dim AMD_NO As String = ""
        Dim mat_id As String = ""
        str = ""
        conn.Open()
        Dim mc2 As New SqlCommand
        mc2.CommandText = "SELECT SUM(MAT_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(FREIGHT_TYPE) AS FREIGHT_TYPE,MAX(DISC_TYPE) AS DISC_TYPE, MAX(PF_TYPE) AS PF_TYPE, MAX(MAT_CODE) as mat_code, MAX(AMD_NO) as AMD_NO,MAX(AMD_NO) as AMD_NO,SUM(MAT_QTY) AS MAT_QTY, SUM(MAT_UNIT_RATE ) AS MAT_UNIT_RATE,SUM(MAT_PACK) AS MAT_PACK,SUM(MAT_DISCOUNT) AS MAT_DISCOUNT,SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD FROM PO_ORD_MAT WHERE PO_NO='" & pono_DropDownList.Text & "' AND MAT_SLNO=" & mat_sl_noDropDownList.Text & " and AMD_DATE < ='" & CHLN_DATE.Year & "-" & CHLN_DATE.Month & "-" & CHLN_DATE.Day & "'"
        mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            AMD_NO = dr.Item("AMD_NO")
            mat_id = dr.Item("MAT_CODE")
            unit_price = dr.Item("MAT_UNIT_RATE")
            disc_type = dr.Item("DISC_TYPE")
            ord_discount = dr.Item("MAT_DISCOUNT")
            pf_type = dr.Item("PF_TYPE")
            freight_type = dr.Item("FREIGHT_TYPE")
            FREIGHT = dr.Item("MAT_FREIGHT_PU")
            ord_packing = dr.Item("MAT_PACK")

            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
        conn.Open()
        mc1.CommandText = "select SUM(MAT_QTY) AS MAT_QTY , SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD FROM PO_ORD_MAT WHERE mat_slno=" & mat_sl_noDropDownList.Text & " and po_no='" & pono_DropDownList.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            qty = dr.Item("MAT_QTY")
            ''rqty = dr.Item("MAT_QTY_RCVD")
            dr.Close()
        End If
        conn.Close()


        ''''''''''''''''''''''''''''''''''
        ''Calculating Actual received Qty
        conn.Open()

        If trans_DropDownList.SelectedValue <> "N/A" Then
            mc1.CommandText = "select (sum(MAT_RCD_QTY) - sum(MAT_REJ_QTY) - sum(MAT_EXCE)) As MAT_QTY_RCVD, (sum(MAT_CHALAN_QTY) - sum(MAT_REJ_QTY) - sum(MAT_EXCE)) as MAT_QTY_CONSIDERED_FOR_PAYMENT from PO_RCD_MAT where po_no='" & pono_DropDownList.Text & "' and MAT_SLNO='" & mat_sl_noDropDownList.Text & "'"
        Else
            mc1.CommandText = "select (sum(MAT_RCD_QTY) - sum(MAT_REJ_QTY) - sum(MAT_EXCE)) As MAT_QTY_RCVD,sum((CASE WHEN (MAT_CHALAN_QTY-MAT_RCD_QTY)<(MAT_CHALAN_QTY*0.005) THEN MAT_CHALAN_QTY ELSE MAT_RCD_QTY END))-sum(MAT_REJ_QTY)-sum(MAT_EXCE) as MAT_QTY_CONSIDERED_FOR_PAYMENT from PO_RCD_MAT where po_no='" & pono_DropDownList.Text & "' and MAT_SLNO='" & mat_sl_noDropDownList.Text & "'"
        End If

        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (IsDBNull(dr.Item("MAT_QTY_RCVD"))) Then
                rqty = 0.00
            Else
                rqty = dr.Item("MAT_QTY_RCVD")
            End If
            If (IsDBNull(dr.Item("MAT_QTY_CONSIDERED_FOR_PAYMENT"))) Then
                MAT_QTY_CONSIDERED_FOR_PAYMENT = 0.00
            Else
                MAT_QTY_CONSIDERED_FOR_PAYMENT = dr.Item("MAT_QTY_CONSIDERED_FOR_PAYMENT")
            End If
            dr.Close()
        End If
        conn.Close()





        ''''''''''''''''''''''''''''''''''

        Dim w_qty, w_complite, w_tolerance, TRANSPORTATION_VALUE, w_unit_price, W_discount, DISCOUNT_VALUE As Decimal
        Dim MC As New SqlCommand
        Dim w_name, w_au As String
        If trans_DropDownList.SelectedValue <> "N/A" Then
            conn.Open()
            MC.CommandText = "select sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1) & "' AND W_SLNO ='" & DropDownList21.Text.Substring(0, DropDownList21.Text.IndexOf(",") - 1) & "'  and AMD_DATE < ='" & CHLN_DATE.Year & "-" & CHLN_DATE.Month & "-" & CHLN_DATE.Day & "'"
            'MC.CommandText = "select sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1).Trim & "' AND W_SLNO ='" & DropDownList21.Text.Substring(0, DropDownList21.Text.IndexOf(",") - 1).Trim & "'  and AMD_DATE < ='" & CHLN_DATE.Year & "-" & CHLN_DATE.Month & "-" & CHLN_DATE.Day & "'"
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

            End If
            conn.Close()
        End If
        TRANSPORTATION_VALUE = w_unit_price
        DISCOUNT_VALUE = (TRANSPORTATION_VALUE * W_discount) / 100
        TRANSPORTATION_VALUE = (TRANSPORTATION_VALUE - DISCOUNT_VALUE)
        TRANSPORTATION_VALUE = TRANSPORTATION_VALUE * CDec(Actual_Material_Weight.Text)
        eqty = (rqty + CDec(rcv_qty_TextBox.Text)) - qty
        If (qty - rqty) < rcv_qty_TextBox.Text Then
            exqty = CDec(rcv_qty_TextBox.Text) - (qty - rqty)
        Else
            exqty = 0.0
        End If

        If exqty >= CDec(rcv_qty_TextBox.Text) Then
            rcv_qty_TextBox.Text = ""
            rcv_qty_TextBox.Focus()
            ERRLabel.Visible = True
            ERRLabel.Text = "There is no balance left in the purchase order."
            Return
        End If
        ''bal qty as per po term
        Dim BAL_QTY_TERM As String = ""
        conn.Open()
        mc1.CommandText = "select FREIGHT_TERM  from ORDER_DETAILS where SO_NO='" & pono_DropDownList.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            BAL_QTY_TERM = dr.Item("FREIGHT_TERM")
            dr.Close()
        End If
        conn.Close()
        If BAL_QTY_TERM = "SRU Bhilai" Then
            sqty = qty - (rqty + CDec(rcv_qty_TextBox.Text))
        Else
            sqty = qty - (rqty + CDec(chalan_qty_TextBox.Text))
            Dim tole As Decimal = (CDec(chalan_qty_TextBox.Text) * w_tolerance) / 100
        End If
        Dim BAG_WEIGHT, NO_OF_BAG As New Decimal(0.0)
        If CDec(no_of_bagTextBox.Text) * CDec(bag_weightTextBox.Text) > 0 Then
            BAG_WEIGHT = ((CDec(bag_weightTextBox.Text) * 1000) / CDec(no_of_bagTextBox.Text))
            NO_OF_BAG = CDec(no_of_bagTextBox.Text)

        ElseIf CDec(no_of_bagTextBox.Text) * CDec(bag_weightTextBox.Text) <= 0 Then
            BAG_WEIGHT = 0
            NO_OF_BAG = CDec(no_of_bagTextBox.Text)
        End If
        If sqty < 0 Then
            sqty = 0
        End If
        Dim dt As DataTable = DirectCast(ViewState("mat"), DataTable)
        dt.Rows.Add(mat_sl_noDropDownList.Text, mat_id, matnameTextBox.Text, au_TextBox.Text, crr_TextBox.Text, chalan_TextBox.Text, chalandate_TextBox.Text, be_TextBox.Text, bedate_TextBox.Text, bl_noTextBox.Text, bldate_TextBox.Text, qty, chalan_qty_TextBox.Text, rcv_qty_TextBox.Text, CDec(CDec(chalan_qty_TextBox.Text) - CDec(rcv_qty_TextBox.Text)), exqty, sqty, NO_OF_BAG, BAG_WEIGHT, Math.Round(TRANSPORTATION_VALUE, 3), tole_value, AMD_NO, Actual_Material_Weight.Text)
        ViewState("mat") = dt
        Me.BINDGRID1()
        crr_TextBox.Text = ""
    End Sub

    Protected Sub crr_cancel_Button_Click(sender As Object, e As EventArgs) Handles crr_cancel_Button.Click
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("CRR No"), New DataColumn("Chalan No"), New DataColumn("Chalan Date"), New DataColumn("BE No"), New DataColumn("BE Date"), New DataColumn("BL No"), New DataColumn("BL Date"), New DataColumn("Order Qty"), New DataColumn("Chalan Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Short Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("No Of Bag"), New DataColumn("Bag Weight"), New DataColumn("Trans Charge"), New DataColumn("Short Price"), New DataColumn("Amd No"), New DataColumn("Total Mt.")})
        ViewState("mat") = dt
        Me.BINDGRID1()
        crr_gridview.Font.Size = 8
        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select distinct PO_ORD_MAT.PO_NO from  PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_ORD_MAT.MAT_STATUS='PENDING' AND (ORDER_DETAILS.SO_STATUS='ACTIVE' OR ORDER_DETAILS.SO_STATUS='RC') and ORDER_DETAILS.PO_TYPE='STORE MATERIAL' ORDER BY PO_NO", conn)
        da.Fill(ds5, "PO_ORD_MAT")
        conn.Close()
        pono_DropDownList.DataSource = ds5.Tables("PO_ORD_MAT")
        pono_DropDownList.DataValueField = "PO_NO"
        pono_DropDownList.DataBind()
        ds5.Tables.Clear()
        pono_DropDownList.Items.Add("Select")
        pono_DropDownList.SelectedValue = "Select"
        pono_DropDownList.Enabled = True
    End Sub


    Protected Sub crr_save_Button_Click(sender As Object, e As EventArgs) Handles crr_save_Button.Click


        Dim working_date As Date
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        End If
        working_date = CDate(TextBox2.Text)
        If crr_gridview.Rows.Count = 0 Then
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
                ''CRR TYPE
                Dim CRR_TYPE As String = ""
                CRR_TYPE = "SCRR"
                conn.Open()
                da = New SqlDataAdapter("select distinct CRR_NO from PO_RCD_MAT WHERE CRR_NO LIKE '" & CRR_TYPE + STR1 & "%'", conn)
                count = da.Fill(ds, "PO_RCD_MAT")
                conn.Close()
                If count = 0 Then
                    crr_TextBox.Text = CRR_TYPE & STR1 & "000001"
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
                    crr_TextBox.Text = CRR_TYPE & STR1 & str
                End If


                Dim mc1 As New SqlCommand
                Dim suplcode As String = ""
                conn.Open()
                mc1.CommandText = "SELECT PARTY_CODE from ORDER_DETAILS WHERE SO_NO='" & pono_DropDownList.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    suplcode = dr.Item("PARTY_CODE")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If

                Dim I As Integer
                ''  Try
                Dim CRR_NO As String = crr_TextBox.Text
                Dim crr_date As Date = working_date.Date
                Dim po_no As String = pono_DropDownList.Text
                Dim TRANS_WO_NO As String = trans_DropDownList.SelectedValue
                Dim TRANS_SLNO As String = DropDownList21.SelectedValue
                If trans_DropDownList.Text <> "N/A" Then
                    TRANS_WO_NO = trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1)
                    TRANS_SLNO = CInt(DropDownList21.Text.Substring(0, DropDownList21.Text.IndexOf(",") - 1))
                End If
                Dim lr_no As String = lr_rr_noTextBox.Text
                Dim SHIP_NAME As String = shipnameTextBox.Text
                For I = 0 To crr_gridview.Rows.Count - 1
                    Dim MAT_SLNO As Integer = crr_gridview.Rows(I).Cells(0).Text
                    Dim MAT_CODE As String = crr_gridview.Rows(I).Cells(1).Text
                    Dim CHLN_NO As String = crr_gridview.Rows(I).Cells(5).Text
                    Dim CHLN_DATE As String = CDate(crr_gridview.Rows(I).Cells(6).Text)
                    Dim MAT_CHALAN_QTY As String = CDec(crr_gridview.Rows(I).Cells(12).Text) - ((CDec(crr_gridview.Rows(I).Cells(17).Text) * CDec(crr_gridview.Rows(I).Cells(18).Text)) / 1000)
                    Dim MAT_RCD_QTY As String = CDec(crr_gridview.Rows(I).Cells(13).Text) - ((CDec(crr_gridview.Rows(I).Cells(17).Text) * CDec(crr_gridview.Rows(I).Cells(18).Text)) / 1000)
                    Dim MAT_EXCE As String = crr_gridview.Rows(I).Cells(15).Text
                    Dim NO_OF_BAG As String = crr_gridview.Rows(I).Cells(17).Text
                    Dim BAG_WEIGHT As String = crr_gridview.Rows(I).Cells(18).Text
                    Dim BE_NO As String = crr_gridview.Rows(I).Cells(7).Text
                    Dim BE_DATE As String = crr_gridview.Rows(I).Cells(8).Text
                    Dim BL_NO As String = crr_gridview.Rows(I).Cells(9).Text
                    Dim BL_DATE As String = crr_gridview.Rows(I).Cells(10).Text
                    Dim trans_charge As String = crr_gridview.Rows(I).Cells(19).Text
                    Dim trans_fault As String = crr_gridview.Rows(I).Cells(20).Text
                    Dim AMD_NO As String = crr_gridview.Rows(I).Cells(21).Text
                    Dim TOTAL_MT As Decimal = crr_gridview.Rows(I).Cells(22).Text
                    'conn.Open()
                    Dim query As String = "Insert Into PO_RCD_MAT(MAT_NAME,CRR_ENTRY_DATE,TOTAL_MT,FISCAL_YEAR,TRANS_SLNO,PAY_EMP,CRR_NO,CRR_DATE,PO_NO,SUPL_ID,TRANS_WO_NO,TRUCK_NO,MAT_SLNO,MAT_CODE,CHLN_NO,CHLN_DATE,MAT_CHALAN_QTY,MAT_RCD_QTY,MAT_REJ_QTY,MAT_BAL_QTY,MAT_EXCE,NO_OF_BAG,BAG_WEIGHT,GARN_NO,BE_NO,BE_DATE,BL_NO,BL_DATE,SHIP_NAME,CRR_EMP,AMD_NO)values (@MAT_NAME,@CRR_ENTRY_DATE,@TOTAL_MT,@FISCAL_YEAR,@TRANS_SLNO,@PAY_EMP,@CRR_NO, @CRR_DATE,@PO_NO,@SUPL_ID,@TRANS_WO_NO,@TRUCK_NO,@MAT_SLNO,@MAT_CODE,@CHLN_NO,@CHLN_DATE,@MAT_CHALAN_QTY,@MAT_RCD_QTY,@MAT_REJ_QTY,@MAT_BAL_QTY,@MAT_EXCE,@NO_OF_BAG,@BAG_WEIGHT,@GARN_NO,@BE_NO,@BE_DATE,@BL_NO,@BL_DATE,@SHIP_NAME,@CRR_EMP,@AMD_NO)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@CRR_NO", CRR_NO)
                    cmd.Parameters.AddWithValue("@CRR_DATE", crr_date)
                    cmd.Parameters.AddWithValue("@TRANS_WO_NO", TRANS_WO_NO)
                    cmd.Parameters.AddWithValue("@TRANS_SLNO", TRANS_SLNO)
                    cmd.Parameters.AddWithValue("@TRUCK_NO", lr_no)
                    cmd.Parameters.AddWithValue("@SHIP_NAME", SHIP_NAME)
                    cmd.Parameters.AddWithValue("@PO_NO", po_no)
                    cmd.Parameters.AddWithValue("@SUPL_ID", suplcode)
                    cmd.Parameters.AddWithValue("@MAT_SLNO", MAT_SLNO)
                    cmd.Parameters.AddWithValue("@MAT_CODE", MAT_CODE)
                    cmd.Parameters.AddWithValue("@CHLN_NO", CHLN_NO)
                    cmd.Parameters.AddWithValue("@CHLN_DATE", Date.ParseExact(CHLN_DATE, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@MAT_CHALAN_QTY", MAT_CHALAN_QTY)
                    cmd.Parameters.AddWithValue("@MAT_RCD_QTY", MAT_RCD_QTY)
                    cmd.Parameters.AddWithValue("@MAT_REJ_QTY", 0.0)
                    cmd.Parameters.AddWithValue("@MAT_BAL_QTY", 0.0)
                    cmd.Parameters.AddWithValue("@MAT_EXCE", MAT_EXCE)
                    cmd.Parameters.AddWithValue("@GARN_NO", "PENDING")
                    cmd.Parameters.AddWithValue("@NO_OF_BAG", NO_OF_BAG)
                    cmd.Parameters.AddWithValue("@BAG_WEIGHT", BAG_WEIGHT)
                    cmd.Parameters.AddWithValue("@BE_NO", BE_NO)
                    cmd.Parameters.AddWithValue("@BE_DATE", BE_DATE)
                    cmd.Parameters.AddWithValue("@BL_NO", BL_NO)
                    cmd.Parameters.AddWithValue("@BL_DATE", BL_DATE)
                    cmd.Parameters.AddWithValue("@CRR_EMP", Session("userName"))
                    cmd.Parameters.AddWithValue("@AMD_NO", AMD_NO)
                    cmd.Parameters.AddWithValue("@PAY_EMP", "")
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
                    cmd.Parameters.AddWithValue("@TOTAL_MT", TOTAL_MT)
                    cmd.Parameters.AddWithValue("@CRR_ENTRY_DATE", Now)
                    cmd.Parameters.AddWithValue("@MAT_NAME", matnameTextBox.Text)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()

                    'UPDATE PO_ORD_MAT
                    If CDec(crr_gridview.Rows(I).Cells(16).Text) <= 0 Then
                        str = "CLEAR"
                    Else
                        str = "PENDING"
                    End If
                    Dim RCVD_QTY_FOR As Decimal = 0
                    If trans_DropDownList.Text = "N/A" Then
                        RCVD_QTY_FOR = CDec(crr_gridview.Rows(I).Cells(13).Text) - CDec(crr_gridview.Rows(I).Cells(15).Text)
                    Else
                        RCVD_QTY_FOR = CDec(crr_gridview.Rows(I).Cells(12).Text) - CDec(crr_gridview.Rows(I).Cells(15).Text)
                    End If
                    'conn.Open()
                    mycommand = New SqlCommand("update PO_ORD_MAT set MAT_QTY_RCVD=MAT_QTY_RCVD + " & RCVD_QTY_FOR & ",MAT_STATUS='" & str & "'  WHERE MAT_SLNO='" & crr_gridview.Rows(I).Cells(0).Text & "' and PO_NO='" & pono_DropDownList.Text & "' AND AMD_NO='" & crr_gridview.Rows(I).Cells(21).Text & "'", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()
                    'conn.Close()
                Next

                ''UPDATE TRANSPORTER
                If trans_DropDownList.SelectedValue <> "N/A" Then
                    Dim C_QTY, R_QTY, TOTAL_QTY, N_BAG, B_WEIGHT, FAULT_PRICE, TOTAL_TRANSPORTER_WEIGHT As Decimal
                    C_QTY = 0.0
                    R_QTY = 0.0
                    N_BAG = 0.0
                    FAULT_PRICE = 0.0
                    B_WEIGHT = 0.0
                    For I = 0 To crr_gridview.Rows.Count - 1
                        C_QTY = C_QTY + crr_gridview.Rows(I).Cells(12).Text
                        R_QTY = R_QTY + crr_gridview.Rows(I).Cells(13).Text
                        N_BAG = N_BAG + crr_gridview.Rows(I).Cells(17).Text
                        B_WEIGHT = B_WEIGHT + crr_gridview.Rows(I).Cells(18).Text
                        TOTAL_TRANSPORTER_WEIGHT = TOTAL_TRANSPORTER_WEIGHT + crr_gridview.Rows(I).Cells(22).Text
                    Next
                    conn.Open()
                    Dim CHLN_DATE As Date = chalandate_TextBox.Text
                    Dim w_qty, w_complite, w_tolerance, PRICE, w_unit_price, discount, DISCOUNT_VALUE As Decimal
                    Dim MC As New SqlCommand
                    Dim w_name, w_au, WO_SUPL_ID, WO_AMD, AMD_DATE As New String("")
                    MC.CommandText = "select MAX(WO_AMD) AS WO_AMD, MAX(AMD_DATE) AS AMD_DATE ,MAX(SUPL_ID) as SUPL_ID,sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1).Trim & "' AND W_SLNO ='" & DropDownList21.Text.Substring(0, DropDownList21.Text.IndexOf(",") - 1).Trim & "'  and AMD_DATE < ='" & CHLN_DATE.Year & "-" & CHLN_DATE.Month & "-" & CHLN_DATE.Day & "'"
                    MC.Connection = conn
                    dr = MC.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        w_qty = dr.Item("W_QTY")
                        w_complite = dr.Item("W_COMPLITED")
                        w_tolerance = dr.Item("W_TOLERANCE")
                        w_unit_price = dr.Item("W_UNIT_PRICE")
                        discount = dr.Item("W_DISCOUNT")
                        w_name = dr.Item("W_NAME")
                        w_au = dr.Item("W_AU")
                        WO_SUPL_ID = dr.Item("SUPL_ID")
                        WO_AMD = dr.Item("WO_AMD")
                        AMD_DATE = dr.Item("AMD_DATE")
                        dr.Close()
                    Else
                        conn.Close()
                        Return
                    End If
                    conn.Close()

                    PRICE = C_QTY * w_unit_price
                    DISCOUNT_VALUE = (PRICE * discount) / 100
                    PRICE = (PRICE - discount)
                    Dim weight As Decimal
                    weight = N_BAG * B_WEIGHT
                    TOTAL_QTY = R_QTY + weight + w_complite
                    'conn.Open()
                    mycommand = New SqlCommand("update WO_ORDER set W_COMPLITED = W_COMPLITED + " & TOTAL_TRANSPORTER_WEIGHT & " where PO_NO='" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1).Trim & "' AND W_SLNO='" & DropDownList21.Text.Substring(0, DropDownList21.Text.IndexOf(",") - 1).Trim & "'  and AMD_DATE =(select max(AMD_DATE) from WO_ORDER where PO_NO ='" & trans_DropDownList.Text.Substring(0, trans_DropDownList.Text.IndexOf(",") - 1).Trim & "' and W_SLNO = '" & DropDownList21.Text.Substring(0, DropDownList21.Text.IndexOf(",") - 1).Trim & "' and AMD_DATE < ='" & CHLN_DATE.Year & "-" & CHLN_DATE.Month & "-" & CHLN_DATE.Day & "')", conn_trans, myTrans)
                    mycommand.ExecuteNonQuery()
                    'conn.Close()

                    '''''''''''''''''''''''''''''''''''''
                    Dim TOLE_VALUE, TRANS_DIFF, WEIGHT_TO_BE_CONSIDERED As New Decimal(0)
                    TOLE_VALUE = (CDec(crr_gridview.Rows(0).Cells(12).Text) * w_tolerance) / 100
                    ''TRANSPORTATION DIFFRENCE
                    TRANS_DIFF = CDec(crr_gridview.Rows(0).Cells(12).Text) - CDec(crr_gridview.Rows(0).Cells(13).Text)
                    If TOLE_VALUE >= TRANS_DIFF Then
                        WEIGHT_TO_BE_CONSIDERED = C_QTY
                    Else
                        WEIGHT_TO_BE_CONSIDERED = R_QTY

                    End If

                    Dim prov_price_for_transporter, rcvd_qty_transporter, chln_qty_trans, short_amt As New Decimal(0.00)

                    For count = 0 To crr_gridview.Rows.Count - 1

                        prov_price_for_transporter = prov_price_for_transporter + CDec(crr_gridview.Rows(count).Cells(19).Text)
                        chln_qty_trans = chln_qty_trans + CDec(Actual_Material_Weight.Text)
                        rcvd_qty_transporter = rcvd_qty_transporter + CDec(Actual_Material_Weight.Text)
                        'short_amt = short_amt + CDec(crr_gridview.Rows(c).Cells(31).Text)

                    Next

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

                    ''LEDGER POSTING PURCHASE
                    Dim PURCHASE As String = ""
                    Dim cons As String = ""
                    conn.Open()
                    Dim MCc As New SqlCommand
                    MCc.CommandText = "select AC_PUR,AC_CON from MATERIAL where MAT_CODE = '" & crr_gridview.Rows(0).Cells(1).Text & "'"
                    MCc.Connection = conn
                    dr = MCc.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        PURCHASE = dr.Item("AC_PUR")
                        cons = dr.Item("AC_CON")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If
                    ''SAVE LEDGER PURCHASE
                    'conn.Open()
                    Dim Query As String = "Insert Into LEDGER(JURNAL_LINE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@JURNAL_LINE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
                    Dim cmd As New SqlCommand(Query, conn_trans, myTrans)

                    cmd.Parameters.AddWithValue("@PO_NO", TRANS_WO_NO)
                    cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", CRR_NO)
                    cmd.Parameters.AddWithValue("@SUPL_ID", WO_SUPL_ID)
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@PERIOD", qtr1)
                    cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
                    cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd.Parameters.AddWithValue("@AC_NO", PURCHASE)
                    cmd.Parameters.AddWithValue("@AMOUNT_DR", Math.Round(prov_price_for_transporter, 2))
                    cmd.Parameters.AddWithValue("@AMOUNT_CR", 0)
                    cmd.Parameters.AddWithValue("@POST_INDICATION", "PUR")
                    cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                    cmd.Parameters.AddWithValue("@JURNAL_LINE_NO", 1)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()


                    Dim MC5 As New SqlCommand
                    Dim PROV As String = ""
                    conn.Open()
                    MC5.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & TRANS_WO_NO & "') and work_type=(select DISTINCT wo_type from wo_order WITH(NOLOCK) where po_no='" & TRANS_WO_NO & "' and w_slno='" & TRANS_SLNO & "')"
                    MC5.Connection = conn
                    dr = MC5.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        PROV = dr.Item("PROV_HEAD")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If

                    ''INSERT LEDGER prov for transport
                    'conn.Open()
                    Query = "Insert Into LEDGER(JURNAL_LINE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@JURNAL_LINE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
                    cmd = New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@PO_NO", TRANS_WO_NO)
                    cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", CRR_NO)
                    cmd.Parameters.AddWithValue("@SUPL_ID", WO_SUPL_ID)
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@PERIOD", qtr1)
                    cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
                    cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd.Parameters.AddWithValue("@AC_NO", PROV)
                    cmd.Parameters.AddWithValue("@AMOUNT_DR", 0)
                    cmd.Parameters.AddWithValue("@AMOUNT_CR", Math.Round(prov_price_for_transporter, 2))
                    cmd.Parameters.AddWithValue("@POST_INDICATION", "PROV")
                    cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                    cmd.Parameters.AddWithValue("@JURNAL_LINE_NO", 3)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()


                    'conn.Open()
                    Query = "Insert Into mb_book(unit_price,Entry_Date,mb_no,mb_date,po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,mb_by,ra_no,prov_amt,pen_amt,sgst,cgst,igst,cess,sgst_liab,cgst_liab,igst_liab,cess_liab,it_amt,pay_ind,fiscal_year,mat_rate,mb_clear,amd_no,amd_date)VALUES(@unit_price,@Entry_Date,@mb_no,@mb_date,@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@mb_by,@ra_no,@prov_amt,@pen_amt,@sgst,@cgst,@igst,@cess,@sgst_liab,@cgst_liab,@igst_liab,@cess_liab,@it_amt,@pay_ind,@fiscal_year,@mat_rate,@mb_clear,@amd_no,@amd_date)"
                    cmd = New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@mb_no", CRR_NO)
                    cmd.Parameters.AddWithValue("@mb_date", Date.ParseExact(crr_date, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@po_no", TRANS_WO_NO)
                    cmd.Parameters.AddWithValue("@supl_id", WO_SUPL_ID)
                    cmd.Parameters.AddWithValue("@wo_slno", TRANS_SLNO)
                    cmd.Parameters.AddWithValue("@w_name", w_name)
                    cmd.Parameters.AddWithValue("@w_au", w_au)
                    cmd.Parameters.AddWithValue("@from_date", Date.ParseExact(CDate(CHLN_DATE), "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@to_date", Date.ParseExact(CDate(CHLN_DATE), "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@work_qty", CDec(chln_qty_trans))
                    cmd.Parameters.AddWithValue("@rqd_qty", CDec(chln_qty_trans))
                    cmd.Parameters.AddWithValue("@bal_qty", CDec(w_qty) - CDec(TOTAL_TRANSPORTER_WEIGHT))
                    cmd.Parameters.AddWithValue("@note", "")
                    cmd.Parameters.AddWithValue("@mb_by", Session("userName"))
                    cmd.Parameters.AddWithValue("@ra_no", "")
                    cmd.Parameters.AddWithValue("@prov_amt", Math.Round(prov_price_for_transporter, 2))
                    cmd.Parameters.AddWithValue("@pen_amt", 0)
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
                    '''''''''''''''''''''''''''''''''''''
                End If

                ''============================TRANSPORTER CALCULATION END================================''

                ''============================CHA CALCULATION START================================''
                ''CALCULATING CHA PROVISION VALUE
                If (be_TextBox.Text <> "N/A") Then
                    Dim CHA_WO, CHA_SLNO, BE_NO_NEW As String
                    Dim CHA_HEAD As New String("")
                    Dim BE_QTY, CHA_RCD_QTY, CHA_QTY As New Decimal(0)
                    CHA_WO = ""
                    CHA_SLNO = ""
                    BE_NO_NEW = ""



                    Dim MC77 As New SqlCommand
                    conn.Open()
                    MC77.CommandText = "select CHA_ORDER,CHA_SLNO, BE_NO, BE_QTY, CHA_RCD_QTY from BE_DETAILS where BE_NO='" & be_TextBox.Text & "'"
                    MC77.Connection = conn
                    dr = MC77.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        CHA_WO = dr.Item("CHA_ORDER")
                        CHA_SLNO = dr.Item("CHA_SLNO")
                        BE_NO_NEW = dr.Item("BE_NO")
                        BE_QTY = dr.Item("BE_QTY")
                        CHA_RCD_QTY = dr.Item("CHA_RCD_QTY")
                        dr.Close()
                        conn.Close()
                    Else
                        conn.Close()
                    End If


                    conn.Close()

                    ''CALCULATING CHA QTY
                    If (trans_DropDownList.SelectedValue <> "N/A") Then
                        CHA_QTY = CDec(crr_gridview.Rows(0).Cells(12).Text) - ((CDec(crr_gridview.Rows(0).Cells(17).Text) * CDec(crr_gridview.Rows(0).Cells(18).Text)) / 1000)
                    Else
                        CHA_QTY = CDec(crr_gridview.Rows(0).Cells(13).Text) - ((CDec(crr_gridview.Rows(0).Cells(17).Text) * CDec(crr_gridview.Rows(0).Cells(18).Text)) / 1000)
                    End If


                    ''UPDATING BE DETAILS
                    If (CHA_QTY > 0) Then
                        'conn.Open()
                        mycommand = New SqlCommand("update BE_DETAILS set CHA_RCD_QTY=CHA_RCD_QTY + " & CHA_QTY & "  WHERE MAT_SLNO='" & crr_gridview.Rows(0).Cells(0).Text & "' and PO_NO='" & pono_DropDownList.Text & "' AND BE_NO='" & be_TextBox.Text & "'", conn_trans, myTrans)
                        mycommand.ExecuteNonQuery()
                        'conn.Close()
                    End If


                    ''Checking if quantity is exceeding the BE quantity
                    If (BE_QTY >= CHA_RCD_QTY + CHA_QTY) Then
                        CHA_QTY = CHA_QTY
                    Else
                        If ((CHA_RCD_QTY + CHA_QTY - BE_QTY) > CHA_QTY) Then
                            CHA_QTY = 0.00
                        Else
                            CHA_QTY = CHA_QTY - (CHA_RCD_QTY + CHA_QTY - BE_QTY)
                        End If

                    End If

                    Dim MC2 As New SqlCommand
                    Dim CHA_PRICE, CHA_DISCOUNT, CHA_PROV_VALUE As Decimal
                    CHA_PRICE = 0
                    CHA_DISCOUNT = 0
                    CHA_PROV_VALUE = 0
                    conn.Open()
                    MC2.CommandText = "select sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER WITH(NOLOCK) where PO_NO = '" & CHA_WO & "' AND W_SLNO='" & CHA_SLNO & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WITH(NOLOCK) WHERE CRR_NO='" & crr_TextBox.Text & "')"
                    MC2.Connection = conn
                    dr = MC2.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        If (IsDBNull(dr.Item("W_UNIT_PRICE"))) Then
                            CHA_PRICE = 0.00
                        Else
                            CHA_PRICE = dr.Item("W_UNIT_PRICE")
                        End If

                        If (IsDBNull(dr.Item("W_DISCOUNT"))) Then
                            CHA_DISCOUNT = 0.00
                        Else
                            CHA_DISCOUNT = dr.Item("W_DISCOUNT")
                        End If

                        dr.Close()
                    Else
                        conn.Close()
                    End If
                    conn.Close()
                    CHA_DISCOUNT = (CHA_PRICE * CHA_DISCOUNT) / 100
                    CHA_PROV_VALUE = FormatNumber((CHA_PRICE - CHA_DISCOUNT) * CHA_QTY, 2)

                    'CHA ENTRY
                    If CHA_PROV_VALUE > 0 Then

                        conn.Open()
                        Dim MC4 As New SqlCommand
                        MC4.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & CHA_WO & "') and work_type=(select distinct wo_type from wo_order WITH(NOLOCK) where po_no='" & CHA_WO & "' and w_slno='" & CHA_SLNO & "')"
                        MC4.Connection = conn
                        dr = MC4.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            CHA_HEAD = dr.Item("PROV_HEAD")
                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If

                        Dim CHA_SUPL_ID As String = ""
                        conn.Open()
                        MC4.CommandText = "SELECT PARTY_CODE FROM ORDER_DETAILS WHERE SO_NO ='" & CHA_WO & "'"
                        MC4.Connection = conn
                        dr = MC4.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            CHA_SUPL_ID = dr.Item("PARTY_CODE")
                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If
                        conn.Close()

                        ''GETTING MATERIAL PURCHASE HEAD
                        ''LEDGER POSTING PURCHASE
                        Dim PURCHASE_HEAD As String = ""

                        conn.Open()
                        Dim MCc As New SqlCommand
                        MCc.CommandText = "select AC_PUR,AC_CON from MATERIAL where MAT_CODE = '" & crr_gridview.Rows(0).Cells(1).Text & "'"
                        MCc.Connection = conn
                        dr = MCc.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            PURCHASE_HEAD = dr.Item("AC_PUR")
                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If

                        ''INSERT LEDGER prov for CHA
                        LEDGER_SAVE_PUR(po_no, CHA_SLNO, CHA_SUPL_ID, "CHA" & crr_TextBox.Text, PURCHASE_HEAD, "Dr", CHA_PROV_VALUE, "PUR", 1, "", BE_NO_NEW)
                        LEDGER_SAVE_PUR(CHA_WO, CHA_SLNO, CHA_SUPL_ID, "CHA" & crr_TextBox.Text, CHA_HEAD, "Cr", CHA_PROV_VALUE, "PROV. FOR CHA", 7, "", BE_NO_NEW)
                        ''INSERT MB BOOK
                        conn.Open()
                        Dim w_qty, w_complite, w_unit_price, W_discount, mat_price As Decimal
                        Dim WO_NAME As String = ""
                        Dim WO_AU As String = ""
                        Dim WO_SUPL_ID, WO_AMD, AMD_DATE As New String("")

                        MC2.CommandText = "select MAX(WO_AMD) AS WO_AMD ,MAX(AMD_DATE) AS AMD_DATE, MAX(SUPL_ID) as SUPL_ID,sum(W_MATERIAL_COST) as W_MATERIAL_COST, sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER WITH(NOLOCK) where PO_NO = '" & CHA_WO & "' AND W_SLNO='" & CHA_SLNO & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WITH(NOLOCK) WHERE CRR_NO='" & crr_TextBox.Text & "')"
                        MC2.Connection = conn
                        dr = MC2.ExecuteReader
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


                        Dim Query As String = "Insert Into mb_book(unit_price,Entry_Date,mb_no,mb_date,po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,mb_by,ra_no,prov_amt,pen_amt,sgst,cgst,igst,cess,sgst_liab,cgst_liab,igst_liab,cess_liab,it_amt,pay_ind,fiscal_year,mat_rate,mb_clear,amd_no,amd_date)VALUES(@unit_price,@Entry_Date,@mb_no,@mb_date,@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@mb_by,@ra_no,@prov_amt,@pen_amt,@sgst,@cgst,@igst,@cess,@sgst_liab,@cgst_liab,@igst_liab,@cess_liab,@it_amt,@pay_ind,@fiscal_year,@mat_rate,@mb_clear,@amd_no,@amd_date)"
                        Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@mb_no", "CHA" & crr_TextBox.Text)
                        cmd.Parameters.AddWithValue("@mb_date", Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@po_no", CHA_WO)
                        cmd.Parameters.AddWithValue("@supl_id", WO_SUPL_ID)
                        cmd.Parameters.AddWithValue("@wo_slno", CHA_SLNO)
                        cmd.Parameters.AddWithValue("@w_name", WO_NAME)
                        cmd.Parameters.AddWithValue("@w_au", WO_AU)
                        cmd.Parameters.AddWithValue("@from_date", Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@to_date", Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@work_qty", CHA_QTY)
                        cmd.Parameters.AddWithValue("@rqd_qty", CHA_QTY)
                        cmd.Parameters.AddWithValue("@bal_qty", CDec(w_qty) - (CDec(w_complite) + CHA_QTY))
                        cmd.Parameters.AddWithValue("@note", "")
                        cmd.Parameters.AddWithValue("@mb_by", Session("userName"))
                        cmd.Parameters.AddWithValue("@ra_no", "")
                        cmd.Parameters.AddWithValue("@prov_amt", Math.Round(CDec(CHA_PROV_VALUE), 2))
                        cmd.Parameters.AddWithValue("@pen_amt", 0)
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

                        ''update cha work order
                        mycommand = New SqlCommand("update WO_ORDER set W_COMPLITED =" & w_complite + CHA_QTY & " where PO_NO='" & CHA_WO & "' AND W_SLNO='" & CHA_SLNO & "'  and AMD_DATE =(select max(AMD_DATE) from WO_ORDER where PO_NO ='" & CHA_WO & "' and W_SLNO = '" & CHA_SLNO & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & crr_TextBox.Text & "'))", conn_trans, myTrans)
                        mycommand.ExecuteNonQuery()

                    End If
                End If


                Dim dt As New DataTable()
                dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("CRR No"), New DataColumn("Chalan No"), New DataColumn("Chalan Date"), New DataColumn("BE No"), New DataColumn("BE Date"), New DataColumn("BL No"), New DataColumn("BL Date"), New DataColumn("Order Qty"), New DataColumn("Chalan Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Short Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("No Of Bag"), New DataColumn("Bag Weight"), New DataColumn("Trans Charge"), New DataColumn("Short Price"), New DataColumn("Amd No"), New DataColumn("Total Mt.")})
                ViewState("mat") = dt
                Me.BINDGRID1()
                conn.Open()
                Dim ds5 As New DataSet
                da = New SqlDataAdapter("select distinct PO_ORD_MAT.PO_NO from PO_ORD_MAT WITH(NOLOCK) JOIN ORDER_DETAILS ON PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_ORD_MAT.MAT_STATUS='PENDING' AND (ORDER_DETAILS.SO_STATUS='ACTIVE' OR ORDER_DETAILS.SO_STATUS='RC') and ORDER_DETAILS.PO_TYPE='STORE MATERIAL' ORDER BY PO_NO", conn)
                da.Fill(ds5, "PO_ORD_MAT")
                conn.Close()
                pono_DropDownList.DataSource = ds5.Tables("PO_ORD_MAT")
                pono_DropDownList.DataValueField = "PO_NO"
                pono_DropDownList.DataBind()
                ds5.Tables.Clear()
                pono_DropDownList.Items.Add("Select")
                pono_DropDownList.SelectedValue = "Select"
                pono_DropDownList.Enabled = True

                myTrans.Commit()
                ERRLabel.Visible = True
                ERRLabel.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                crr_TextBox.Text = ""
                ERRLabel.Visible = True
                ERRLabel.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

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

            'conn.Open()
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
            'conn.Close()
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("CRR No"), New DataColumn("Chalan No"), New DataColumn("Chalan Date"), New DataColumn("BE No"), New DataColumn("BE Date"), New DataColumn("BL No"), New DataColumn("BL Date"), New DataColumn("Order Qty"), New DataColumn("Chalan Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Short Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("No Of Bag"), New DataColumn("Bag Weight"), New DataColumn("Trans Charge"), New DataColumn("Short Price"), New DataColumn("Amd No"), New DataColumn("Total Mt.")})
        ViewState("mat") = dt
        Me.BINDGRID1()
        crr_gridview.Font.Size = 8
        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select distinct PO_ORD_MAT.PO_NO from  PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_ORD_MAT.MAT_STATUS='PENDING' AND (ORDER_DETAILS.SO_STATUS='ACTIVE' OR ORDER_DETAILS.SO_STATUS='RC') and ORDER_DETAILS.PO_TYPE='STORE MATERIAL' ORDER BY PO_NO", conn)
        da.Fill(ds5, "PO_ORD_MAT")
        conn.Close()
        pono_DropDownList.DataSource = ds5.Tables("PO_ORD_MAT")
        pono_DropDownList.DataValueField = "PO_NO"
        pono_DropDownList.DataBind()
        ds5.Tables.Clear()
        pono_DropDownList.Items.Add("Select")
        pono_DropDownList.SelectedValue = "Select"
        pono_DropDownList.Enabled = True
        lr_rr_noTextBox.Text = ""
        be_TextBox.Text = "N/A"
        bl_noTextBox.Text = "N/A"
        bedate_TextBox.Text = "N/A"
        bldate_TextBox.Text = "N/A"
        suplnameTextBox.Text = ""
        transnameTextBox.Text = ""
        TextBox174.Text = ""
        shipnameTextBox.Text = ""
        matnameTextBox.Text = ""
        au_TextBox.Text = ""
        chalan_qty_TextBox.Text = 0.0
        chalan_TextBox.Text = ""
        rcv_qty_TextBox.Text = 0.0
        no_of_bagTextBox.Text = 0
        bag_weightTextBox.Text = 0.0
        trans_DropDownList.Items.Clear()
        DropDownList21.Items.Clear()
        mat_sl_noDropDownList.Items.Clear()
        crr_TextBox.Text = ""
        Panel1.Visible = False
        Panel16.Visible = True
    End Sub

    Protected Sub new_button_Click(sender As Object, e As EventArgs) Handles new_button.Click

        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select distinct PO_ORD_MAT.PO_NO from  PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_ORD_MAT.MAT_STATUS='PENDING' AND (ORDER_DETAILS.SO_STATUS='ACTIVE' OR ORDER_DETAILS.SO_STATUS='RCM') and ORDER_DETAILS.PO_TYPE='" & type_DropDown.SelectedValue & "' ORDER BY PO_NO", conn)
        'da = New SqlDataAdapter("select distinct PO_ORD_MAT.PO_NO from  PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_ORD_MAT.MAT_STATUS='PENDING' AND (ORDER_DETAILS.SO_STATUS='ACTIVE' OR ORDER_DETAILS.SO_STATUS='RCM') and ORDER_DETAILS.PO_TYPE='" & type_DropDown.SelectedValue & "' and ORDER_DETAILS.FINANCE_approved='Yes' ORDER BY PO_NO", conn)
        da.Fill(ds5, "PO_ORD_MAT")
        conn.Close()
        pono_DropDownList.DataSource = ds5.Tables("PO_ORD_MAT")
        pono_DropDownList.DataValueField = "PO_NO"
        pono_DropDownList.DataBind()
        ds5.Tables.Clear()
        pono_DropDownList.Items.Add("Select")
        pono_DropDownList.SelectedValue = "Select"
        pono_DropDownList.Enabled = True
        Panel1.Visible = True
        Panel16.Visible = False
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("CRR No"), New DataColumn("Chalan No"), New DataColumn("Chalan Date"), New DataColumn("BE No"), New DataColumn("BE Date"), New DataColumn("BL No"), New DataColumn("BL Date"), New DataColumn("Order Qty"), New DataColumn("Chalan Qty"), New DataColumn("Rcvd Qty"), New DataColumn("Short Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("No Of Bag"), New DataColumn("Bag Weight"), New DataColumn("Trans Charge"), New DataColumn("Short Price"), New DataColumn("Amd No"), New DataColumn("Total Mt.")})
        ViewState("mat") = dt
        crr_gridview.Font.Size = 8
        Me.BINDGRID1()
    End Sub

    Protected Sub view_button_Click(sender As Object, e As EventArgs) Handles view_button.Click
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        ''Dim PO_QUARY As String = "select * from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO where PO_RCD_MAT .CRR_NO ='" & search_DropDown.SelectedValue & "'"
        Dim PO_QUARY As String = "select MAX(PO_RCD_MAT . BAG_WEIGHT) AS BAG_WEIGHT ,MAX(PO_RCD_MAT . NO_OF_BAG) AS NO_OF_BAG ,PO_RCD_MAT . MAT_SLNO , MAX(PO_RCD_MAT .CRR_NO) AS CRR_NO ,MAX(PO_RCD_MAT .CRR_DATE) AS CRR_DATE ,MAX(PO_RCD_MAT .PO_NO) AS PO_NO ,MAX(ORDER_DETAILS .SO_ACTUAL_DATE) AS SO_ACTUAL_DATE ,MAX(PO_RCD_MAT .SUPL_ID) AS SUPL_ID ,MAX(PO_RCD_MAT .TRANS_WO_NO) AS TRANS_WO_NO ,MAX(PO_RCD_MAT .TRUCK_NO) AS TRUCK_NO ,MAX(PO_RCD_MAT .MAT_CODE) AS MAT_CODE , " &
        "MAX(PO_RCD_MAT .CHLN_NO) AS CHLN_NO ,MAX(PO_RCD_MAT .CHLN_DATE) AS CHLN_DATE ,MAX(PO_RCD_MAT .MAT_CHALAN_QTY) AS MAT_CHALAN_QTY ,MAX(PO_RCD_MAT .MAT_RCD_QTY) AS MAT_RCD_QTY ,MAX(PO_RCD_MAT .MAT_REJ_QTY) AS MAT_REJ_QTY ,MAX(PO_RCD_MAT .MAT_BAL_QTY) AS MAT_BAL_QTY ,MAX(PO_RCD_MAT .MAT_EXCE) AS MAT_EXCE ,MAX(PO_RCD_MAT .RET_STATUS) RET_STATUS ,MAX(PO_RCD_MAT .GARN_DATE) AS GARN_DATE ,MAX(PO_RCD_MAT .CRR_EMP) AS CRR_EMP , " &
        "MAX(PO_RCD_MAT .INSP_EMP ) AS INSP_EMP , MAX (PO_RCD_MAT .INSP_NOTE ) AS INSP_NOTE , MAX (PO_RCD_MAT .MAT_RATE) AS MAT_RATE , MAX (PO_RCD_MAT .GARN_NOTE ) AS GARN_NOTE , MAX (MATERIAL .MAT_NAME ) AS MAT_NAME , MAX (MATERIAL.MAT_AU ) AS MAT_AU , MAX (SUPL.SUPL_NAME ) AS SUPL_NAME , MAX (SUPL.SUPL_AT ) AS SUPL_AT , " &
        "MAX (SUPL.SUPL_PO ) AS SUPL_PO , MAX (SUPL.SUPL_DIST ) AS SUPL_DIST , MAX (SUPL.SUPL_STATE ) AS SUPL_STATE , MAX (SUPL.SUPL_COUNTRY ) AS SUPL_COUNTRY , MAX (ORDER_DETAILS .SO_TYPE ) AS SO_TYPE , MAX (ORDER_DETAILS .PO_TYPE ) AS PO_TYPE , SUM (PO_ORD_MAT .MAT_QTY ) AS MAT_QTY , MAX (ORDER_DETAILS .PAYMENT_MODE ) AS PAYMENT_MODE , MAX (ORDER_DETAILS .MODE_OF_DESPATCH ) AS MODE_OF_DESPATCH , " &
        "MAX (ORDER_DETAILS .INDENTOR ) AS INDENTOR , MAX (PO_RCD_MAT .RET_USER ) AS RET_USER , MAX (PO_RCD_MAT .INSP_DATE ) AS INSP_DATE , MAX (ORDER_DETAILS .SO_ACTUAL_DATE ) AS SO_ACTUAL_DATE , MAX (PO_RCD_MAT .GARN_NO ) AS GARN_NO " &
        "from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO  " &
        " where PO_RCD_MAT .CRR_NO ='" & search_DropDown.SelectedValue & "'" &
        " GROUP BY PO_RCD_MAT .MAT_SLNO " &
        " ORDER BY PO_RCD_MAT.MAT_SLNO"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.Load(Server.MapPath("~/print_rpt/crrreport.rpt"))
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Reports/report.pdf"))
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


    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If (DropDownList1.SelectedValue <> "Select") Then

            conn.Open()
            da = New SqlDataAdapter("select distinct CRR_NO from PO_RCD_MAT where CRR_NO like 'SCRR%' and crr_date between '" & 2000 + CInt(Left(DropDownList1.SelectedValue, 2)) & "-04-01' and '" & 2000 + CInt(Right(DropDownList1.SelectedValue, 2)) & "-03-31' ORDER BY CRR_NO", conn)
            da.Fill(ds, "PO_RCD_MAT")
            search_DropDown.DataSource = ds.Tables("PO_RCD_MAT")
            search_DropDown.DataValueField = "CRR_NO"
            search_DropDown.DataBind()
            conn.Close()

        Else
            search_DropDown.Items.Clear()
            search_DropDown.DataBind()
        End If
    End Sub


End Class