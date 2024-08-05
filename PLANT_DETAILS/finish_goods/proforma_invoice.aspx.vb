Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine

Public Class proforma_invoice1
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            type_DropDown.AutoPostBack = False
            Panel1.Visible = False
            search_Label.Visible = True
            type_DropDown.Visible = True
            search_DropDown.Visible = True
            new_button.Visible = True
            view_button.Visible = True
            type_DropDown.Items.Clear()
            search_DropDown.Items.Clear()
            search_Label.Text = "PROFORMA INVOICE"
            type_DropDown.Items.Add("FINISH GOODS")
            conn.Open()
            da = New SqlDataAdapter("select distinct INVOICE_NO from proforma_invoice where INVOICE_NO like 'PINV%' ORDER BY INVOICE_NO", conn)
            da.Fill(ds, "proforma_invoice")
            search_DropDown.DataSource = ds.Tables("proforma_invoice")
            search_DropDown.DataValueField = "INVOICE_NO"
            search_DropDown.DataBind()
            conn.Close()
            Panel16.Visible = True
            Dim dt As New DataTable()
            dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Quantity"), New DataColumn("Unit Price"), New DataColumn("Base Value"), New DataColumn("Disc. Type"), New DataColumn("Discount"), New DataColumn("Packing Type"), New DataColumn("Packing/Forwarding"), New DataColumn("Freight Type"), New DataColumn("Freight"), New DataColumn("Taxable Value"), New DataColumn("Mode Of Trans."), New DataColumn("CGST"), New DataColumn("CGST AMT"), New DataColumn("SGST"), New DataColumn("SGST AMT"), New DataColumn("IGST"), New DataColumn("IGST AMT"), New DataColumn("Total Value"), New DataColumn("Unit Weight(Kg)")})
            ViewState("mat") = dt
            crr_gridview.Font.Size = 8
            Me.BINDGRID1()
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub BINDGRID1()
        crr_gridview.DataSource = DirectCast(ViewState("mat"), DataTable)
        crr_gridview.DataBind()

    End Sub
    Protected Sub new_button_Click(sender As Object, e As EventArgs) Handles new_button.Click
        Panel16.Visible = False
        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select distinct (SO_MAT_ORDER.SO_NO +' , '+ ORDER_DETAILS .SO_ACTUAL) AS PO_NO from SO_MAT_ORDER JOIN ORDER_DETAILS ON SO_MAT_ORDER.SO_NO=ORDER_DETAILS.SO_NO where SO_MAT_ORDER.ITEM_STATUS='PENDING' AND (ORDER_DETAILS.SO_STATUS='ACTIVE' OR ORDER_DETAILS.SO_STATUS='DRAFT') and ORDER_DETAILS.PO_TYPE='" & type_DropDown.SelectedValue & "' ORDER BY PO_NO", conn)
        da.Fill(ds5, "SO_MAT_ORDER")
        conn.Close()
        pono_DropDownList.DataSource = ds5.Tables("SO_MAT_ORDER")
        pono_DropDownList.DataValueField = "PO_NO"
        pono_DropDownList.DataBind()
        ds5.Tables.Clear()
        pono_DropDownList.Items.Insert(0, "Select")
        pono_DropDownList.SelectedValue = "Select"
        pono_DropDownList.Enabled = True
        Panel1.Visible = True
    End Sub

    Protected Sub pono_DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles pono_DropDownList.SelectedIndexChanged
        If pono_DropDownList.SelectedValue = "Select" Then
            pono_DropDownList.Focus()
            suplnameTextBox.Text = ""
            Return
        End If
        Dim working_date As Date
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        End If
        working_date = CDate(TextBox2.Text)

        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select DISTINCT (CONVERT(varchar(10), ITEM_SLNO) + ' , ' + F_ITEM.ITEM_NAME) AS MAT_SLNO from SO_MAT_ORDER join F_ITEM on SO_MAT_ORDER.ITEM_CODE=F_ITEM.ITEM_CODE where SO_NO='" & pono_DropDownList.Text.Substring(0, pono_DropDownList.Text.IndexOf(",") - 1) & "' and ITEM_STATUS='PENDING'", conn)
        da.Fill(ds5, "SO_MAT_ORDER")
        mat_sl_noDropDownList.DataSource = ds5.Tables("SO_MAT_ORDER")
        mat_sl_noDropDownList.DataValueField = "MAT_SLNO"
        mat_sl_noDropDownList.DataBind()
        Dim mc1 As New SqlCommand
        str = ""
        Dim mode_trans As String = ""
        mc1.CommandText = "SELECT ORDER_DETAILS.DELIVERY_TERM,ORDER_DETAILS.MODE_OF_DESPATCH,DATER .D_NAME FROM ORDER_DETAILS JOIN DATER ON ORDER_DETAILS .PARTY_CODE =DATER.D_CODE WHERE ORDER_DETAILS.SO_NO='" & pono_DropDownList.Text.Substring(0, pono_DropDownList.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            str = dr.Item("DELIVERY_TERM")
            suplnameTextBox.Text = dr.Item("D_NAME")
            mode_trans = dr.Item("MODE_OF_DESPATCH")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()

        mat_sl_noDropDownList.Items.Insert(0, "Select")
        mat_sl_noDropDownList.SelectedValue = "Select"
        txt_mode_of_trans.Text = mode_trans
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
        mc1.CommandText = "SELECT F_ITEM .ITEM_NAME, SO_MAT_ORDER.ORD_AU FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE  =F_ITEM .ITEM_CODE WHERE SO_MAT_ORDER .ITEM_SLNO =" & mat_sl_noDropDownList.Text.Substring(0, mat_sl_noDropDownList.Text.IndexOf(",") - 1) & "and SO_MAT_ORDER.SO_NO='" & pono_DropDownList.Text.Substring(0, pono_DropDownList.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            matnameTextBox.Text = dr.Item("ITEM_NAME")
            au_TextBox.Text = dr.Item("ORD_AU")

            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
        crr_add_Button.Enabled = True
    End Sub

    Protected Sub crr_add_Button_Click(sender As Object, e As EventArgs) Handles crr_add_Button.Click

        Label467.Text = ""
        Label467.Visible = False
        Dim po_type As String = ""
        Dim mc1 As New SqlCommand

        Dim proforma_date As Date = TextBox2.Text
        Dim disc_type, pf_type, freight_type As String
        Dim MAT_QTY, CGST, SGST, IGST As New Decimal(0)
        disc_type = ""
        pf_type = ""
        freight_type = ""

        MAT_QTY = CDec(txt_mat_qty.Text)
        Dim frieght_unit_price, TOTAL_FREIGHT, unit_price, Mat_Weight, mat_discount, mat_packing As Decimal
        Dim mat_code As String = ""

        conn.Open()
        Dim mc2 As New SqlCommand
        mc2.CommandText = "SELECT MAX(SO_MAT_ORDER.ITEM_CGST) As CGST,MAX(SO_MAT_ORDER.ITEM_SGST) As SGST,MAX(SO_MAT_ORDER.ITEM_IGST) As IGST,MAX(F_ITEM.ITEM_WEIGHT) As Mat_Weight,MAX(SO_MAT_ORDER.ITEM_CODE) as mat_code,SUM(ITEM_FREIGHT_PU) AS MAT_FREIGHT_PU, MAX(ITEM_FREIGHT_TYPE) AS FREIGHT_TYPE,SUM(ITEM_DISCOUNT) AS MAT_DISCOUNT ,MAX(DISC_TYPE) AS DISC_TYPE, SUM(ITEM_PACK) AS MAT_PACK, MAX(PACK_TYPE) AS PACKING_TYPE, MAX(AMD_NO) as AMD_NO,SUM(ITEM_QTY) AS MAT_QTY, SUM(ITEM_UNIT_RATE ) AS MAT_UNIT_RATE FROM SO_MAT_ORDER join F_ITEM on SO_MAT_ORDER.ITEM_CODE=F_ITEM.ITEM_CODE WHERE SO_NO='" & pono_DropDownList.Text.Substring(0, pono_DropDownList.Text.IndexOf(",") - 1) & "' AND ITEM_SLNO=" & mat_sl_noDropDownList.Text.Substring(0, mat_sl_noDropDownList.Text.IndexOf(",") - 1) & " and AMD_DATE < ='" & proforma_date.Year & "-" & proforma_date.Month & "-" & proforma_date.Day & "'"
        mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            mat_code = dr.Item("MAT_CODE")
            frieght_unit_price = dr.Item("MAT_FREIGHT_PU")
            freight_type = dr.Item("FREIGHT_TYPE")
            mat_discount = dr.Item("MAT_DISCOUNT")
            disc_type = dr.Item("DISC_TYPE")
            mat_packing = dr.Item("MAT_PACK")
            pf_type = dr.Item("PACKING_TYPE")
            unit_price = FormatNumber(dr.Item("MAT_UNIT_RATE"), 2)
            Mat_Weight = dr.Item("Mat_Weight")
            CGST = dr.Item("CGST")
            SGST = dr.Item("SGST")
            IGST = dr.Item("IGST")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()

        Dim MAT_BASE_PRICE, DISCOUNT_VALUE, PACKING_VALUE, MAT_FINAL_PRICE, MAT_TOTAL_VALUE, CGST_VALUE, SGST_VALUE, IGST_VALUE As Decimal

        MAT_BASE_PRICE = FormatNumber(unit_price * MAT_QTY, 2)

        'Calculating Freight
        If (freight_type = "PERCENTAGE") Then
            TOTAL_FREIGHT = FormatNumber((frieght_unit_price * unit_price * MAT_QTY) / 100, 2)

        ElseIf (freight_type = "PER UNIT") Then
            TOTAL_FREIGHT = FormatNumber(frieght_unit_price * MAT_QTY, 2)

        ElseIf (freight_type = "PER MT") Then
            TOTAL_FREIGHT = FormatNumber((frieght_unit_price * MAT_QTY * Mat_Weight) / 1000, 2)

        End If

        'Calculating Discount
        If (disc_type = "PERCENTAGE") Then
            DISCOUNT_VALUE = FormatNumber((unit_price * mat_discount * MAT_QTY) / 100, 2)

        ElseIf (disc_type = "PER UNIT") Then
            DISCOUNT_VALUE = FormatNumber(mat_discount * MAT_QTY, 2)

        ElseIf (disc_type = "PER MT") Then
            DISCOUNT_VALUE = FormatNumber((mat_discount * MAT_QTY * Mat_Weight) / 1000, 2)

        End If

        'Calculating Packing
        If (pf_type = "PERCENTAGE") Then
            PACKING_VALUE = FormatNumber((unit_price * mat_packing * MAT_QTY) / 100, 2)

        ElseIf (pf_type = "PER UNIT") Then
            PACKING_VALUE = FormatNumber(mat_packing * MAT_QTY, 2)

        ElseIf (pf_type = "PER MT") Then
            PACKING_VALUE = FormatNumber((mat_packing * MAT_QTY * Mat_Weight) / 1000, 2)

        End If


        MAT_FINAL_PRICE = FormatNumber((MAT_BASE_PRICE - DISCOUNT_VALUE + TOTAL_FREIGHT + PACKING_VALUE), 2)
        CGST_VALUE = FormatNumber(MAT_FINAL_PRICE * CGST / 100, 2)
        SGST_VALUE = FormatNumber(MAT_FINAL_PRICE * SGST / 100, 2)
        IGST_VALUE = FormatNumber(MAT_FINAL_PRICE * IGST / 100, 2)

        MAT_TOTAL_VALUE = MAT_FINAL_PRICE + CGST_VALUE + SGST_VALUE + IGST_VALUE

        Dim dt As DataTable = DirectCast(ViewState("mat"), DataTable)
        dt.Rows.Add(mat_sl_noDropDownList.Text.Substring(0, mat_sl_noDropDownList.Text.IndexOf(",") - 1), mat_code, matnameTextBox.Text, au_TextBox.Text, txt_mat_qty.Text, unit_price, MAT_BASE_PRICE, disc_type, DISCOUNT_VALUE, pf_type, PACKING_VALUE, freight_type, TOTAL_FREIGHT, MAT_FINAL_PRICE, txt_mode_of_trans.Text, CGST, CGST_VALUE, SGST, SGST_VALUE, IGST, IGST_VALUE, MAT_TOTAL_VALUE, Mat_Weight)
        'dt.Columns.AddRange(New DataColumn(10) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Quantity"), New DataColumn("Unit Price"), New DataColumn("Base Value"), New DataColumn("Discount"), New DataColumn("Packing/Forwarding"), New DataColumn("Freight"), New DataColumn("Total Value")})
        ViewState("mat") = dt
        Me.BINDGRID1()
        txt_invoice_no.Text = ""
    End Sub

    Protected Sub crr_save_Button_Click(sender As Object, e As EventArgs) Handles crr_save_Button.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                If crr_gridview.Rows.Count = 0 Then
                    Return
                End If
                Dim working_date As Date
                If TextBox2.Text = "" Then
                    TextBox2.Focus()
                    Return
                ElseIf IsDate(TextBox2.Text) = False Then
                    TextBox2.Focus()
                    Return
                End If
                working_date = CDate(TextBox2.Text)
                Dim STR1 As String = ""
                If working_date.Date.Month > 3 Then
                    STR1 = working_date.Date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = STR1 & (STR1 + 1)
                ElseIf working_date.Date.Month <= 3 Then
                    STR1 = working_date.Date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = (STR1 - 1) & STR1
                End If

                '''''''''''''''''''''''''''''''''

                ''CRR TYPE
                Dim CRR_TYPE As New String("PINV")

                conn.Open()
                da = New SqlDataAdapter("select distinct INVOICE_NO from proforma_invoice", conn)
                count = da.Fill(ds, "PO_RCD_MAT")
                conn.Close()
                If count = 0 Then
                    txt_invoice_no.Text = CRR_TYPE & STR1 & "000001"
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
                    txt_invoice_no.Text = CRR_TYPE & STR1 & str
                End If


                Dim mc1 As New SqlCommand
                Dim I As Integer
                Dim INVOICE_NO As String = txt_invoice_no.Text
                Dim INVOICE_DATE As Date = working_date.Date
                Dim po_no As String = pono_DropDownList.Text.Substring(0, pono_DropDownList.Text.IndexOf(",") - 1)
                Dim PARTY_CODE As New String("")
                conn.Open()
                Dim mc2 As New SqlCommand
                mc2.CommandText = "SELECT PARTY_CODE FROM ORDER_DETAILS WHERE SO_NO='" & pono_DropDownList.Text.Substring(0, pono_DropDownList.Text.IndexOf(",") - 1) & "'"
                mc2.Connection = conn
                dr = mc2.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    PARTY_CODE = dr.Item("PARTY_CODE")

                    dr.Close()
                Else
                    conn.Close()
                End If
                conn.Close()

                For I = 0 To crr_gridview.Rows.Count - 1
                    Dim MAT_SLNO As Integer = crr_gridview.Rows(I).Cells(0).Text
                    Dim MAT_CODE As String = crr_gridview.Rows(I).Cells(1).Text
                    Dim MAT_NAME As String = crr_gridview.Rows(I).Cells(2).Text
                    Dim MAT_AU As String = crr_gridview.Rows(I).Cells(3).Text
                    Dim MAT_QTY As String = CDec(crr_gridview.Rows(I).Cells(4).Text)
                    Dim UNIT_PRICE As String = CDec(crr_gridview.Rows(I).Cells(5).Text)
                    Dim BASE_VALUE As String = CDec(crr_gridview.Rows(I).Cells(6).Text)
                    Dim DISC_TYPE As String = crr_gridview.Rows(I).Cells(7).Text
                    Dim DISC_VALUE As String = CDec(crr_gridview.Rows(I).Cells(8).Text)
                    Dim PACK_TYPE As String = crr_gridview.Rows(I).Cells(9).Text
                    Dim PACK_VALUE As String = CDec(crr_gridview.Rows(I).Cells(10).Text)
                    Dim FREIGHT_TYPE As String = crr_gridview.Rows(I).Cells(11).Text
                    Dim FREIGHT_VALUE As String = CDec(crr_gridview.Rows(I).Cells(12).Text)
                    Dim TAXABLE_VALUE As Decimal = CDec(crr_gridview.Rows(I).Cells(13).Text)
                    Dim MODE_OF_TRANSPORT As String = crr_gridview.Rows(I).Cells(14).Text
                    Dim CGST As Decimal = CDec(crr_gridview.Rows(I).Cells(15).Text)
                    Dim CGST_VALUE As Decimal = CDec(crr_gridview.Rows(I).Cells(16).Text)
                    Dim SGST As Decimal = CDec(crr_gridview.Rows(I).Cells(17).Text)
                    Dim SGST_VALUE As Decimal = CDec(crr_gridview.Rows(I).Cells(18).Text)
                    Dim IGST As Decimal = CDec(crr_gridview.Rows(I).Cells(19).Text)
                    Dim IGST_VALUE As Decimal = CDec(crr_gridview.Rows(I).Cells(20).Text)
                    Dim TOTAL_VALUE As String = CDec(crr_gridview.Rows(I).Cells(21).Text)
                    Dim UNIT_WEIGHT As String = CDec(crr_gridview.Rows(I).Cells(22).Text)


                    Dim query As String = "Insert Into proforma_invoice(TAXABLE_VALUE,SO_NO,CGST,CGST_AMT,SGST,SGST_AMT,IGST,IGST_AMT,PARTY_CODE,INVOICE_NO,INVOICE_DATE,MAT_SLNO,MAT_CODE,MAT_NAME,MAT_AU,MAT_QTY,UNIT_PRICE,BASE_VALUE,DISC_TYPE,DISCOUNT_AMT,PACKING_TYPE,PACKING_AMT,FREIGHT_TYPE,FREIGHT_AMT,MODE_OF_TRANSPORT,TOTAL_VALUE,UNIT_WEIGHT,EMP_NAME)values (@TAXABLE_VALUE,@SO_NO,@CGST,@CGST_AMT,@SGST,@SGST_AMT,@IGST,@IGST_AMT,@PARTY_CODE,@INVOICE_NO,@INVOICE_DATE,@MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_AU,@MAT_QTY,@UNIT_PRICE,@BASE_VALUE,@DISC_TYPE,@DISCOUNT_AMT,@PACKING_TYPE,@PACKING_AMT,@FREIGHT_TYPE,@FREIGHT_AMT,@MODE_OF_TRANSPORT,@TOTAL_VALUE,@UNIT_WEIGHT,@EMP_NAME)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@INVOICE_NO", INVOICE_NO)
                    cmd.Parameters.AddWithValue("@INVOICE_DATE", Date.ParseExact(INVOICE_DATE, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@MAT_SLNO", MAT_SLNO)
                    cmd.Parameters.AddWithValue("@MAT_CODE", MAT_CODE)
                    cmd.Parameters.AddWithValue("@MAT_NAME", MAT_NAME)
                    cmd.Parameters.AddWithValue("@MAT_AU", MAT_AU)
                    cmd.Parameters.AddWithValue("@MAT_QTY", MAT_QTY)
                    cmd.Parameters.AddWithValue("@UNIT_PRICE", UNIT_PRICE)
                    cmd.Parameters.AddWithValue("@BASE_VALUE", BASE_VALUE)
                    cmd.Parameters.AddWithValue("@DISC_TYPE", DISC_TYPE)
                    cmd.Parameters.AddWithValue("@DISCOUNT_AMT", DISC_VALUE)
                    cmd.Parameters.AddWithValue("@PACKING_TYPE", PACK_TYPE)
                    cmd.Parameters.AddWithValue("@PACKING_AMT", PACK_VALUE)
                    cmd.Parameters.AddWithValue("@FREIGHT_TYPE", FREIGHT_TYPE)
                    cmd.Parameters.AddWithValue("@FREIGHT_AMT", FREIGHT_VALUE)
                    cmd.Parameters.AddWithValue("@MODE_OF_TRANSPORT", MODE_OF_TRANSPORT)
                    cmd.Parameters.AddWithValue("@TOTAL_VALUE", TOTAL_VALUE)
                    cmd.Parameters.AddWithValue("@UNIT_WEIGHT", UNIT_WEIGHT)
                    cmd.Parameters.AddWithValue("@EMP_NAME", Session("userName"))
                    cmd.Parameters.AddWithValue("@PARTY_CODE", PARTY_CODE)
                    cmd.Parameters.AddWithValue("@SO_NO", pono_DropDownList.Text.Substring(0, pono_DropDownList.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@CGST", CGST)
                    cmd.Parameters.AddWithValue("@CGST_AMT", CGST_VALUE)
                    cmd.Parameters.AddWithValue("@SGST", SGST)
                    cmd.Parameters.AddWithValue("@SGST_AMT", SGST_VALUE)
                    cmd.Parameters.AddWithValue("@IGST", IGST)
                    cmd.Parameters.AddWithValue("@IGST_AMT", IGST_VALUE)
                    cmd.Parameters.AddWithValue("@TAXABLE_VALUE", TAXABLE_VALUE)

                    cmd.ExecuteReader()
                    cmd.Dispose()


                    '''''''''''''''''''''''''''''''''

                Next
                ''UPDATE TRANSPORTER

                Dim dt As New DataTable()

                dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Quantity"), New DataColumn("Unit Price"), New DataColumn("Base Value"), New DataColumn("Disc. Type"), New DataColumn("Discount"), New DataColumn("Packing Type"), New DataColumn("Packing/Forwarding"), New DataColumn("Freight Type"), New DataColumn("Freight"), New DataColumn("Taxable Value"), New DataColumn("Mode Of Trans."), New DataColumn("CGST"), New DataColumn("CGST AMT"), New DataColumn("SGST"), New DataColumn("SGST AMT"), New DataColumn("IGST"), New DataColumn("IGST AMT"), New DataColumn("Total Value"), New DataColumn("Unit Weight(Kg)")})
                ViewState("mat") = dt
                Me.BINDGRID1()

                Label467.Visible = True
                Label467.Text = "Data Saved Successfully."

                myTrans.Commit()
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label467.Visible = True
                Label467.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub crr_cancel_Button_Click(sender As Object, e As EventArgs) Handles crr_cancel_Button.Click
        Dim dt As New DataTable()

        dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Quantity"), New DataColumn("Unit Price"), New DataColumn("Base Value"), New DataColumn("Disc. Type"), New DataColumn("Discount"), New DataColumn("Packing Type"), New DataColumn("Packing/Forwarding"), New DataColumn("Freight Type"), New DataColumn("Freight"), New DataColumn("Taxable Value"), New DataColumn("Mode Of Trans."), New DataColumn("CGST"), New DataColumn("CGST AMT"), New DataColumn("SGST"), New DataColumn("SGST AMT"), New DataColumn("IGST"), New DataColumn("IGST AMT"), New DataColumn("Total Value"), New DataColumn("Unit Weight(Kg)")})
        ViewState("mat") = dt
        Me.BINDGRID1()
        crr_gridview.Font.Size = 8
        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select distinct (PO_ORD_MAT.PO_NO +' , '+ ORDER_DETAILS .SO_ACTUAL) AS PO_NO from  PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_ORD_MAT.MAT_STATUS='PENDING' AND (ORDER_DETAILS.SO_STATUS='ACTIVE' OR ORDER_DETAILS.SO_STATUS='RC') and ORDER_DETAILS.PO_TYPE='" & type_DropDown.SelectedValue & "' ORDER BY PO_NO", conn)
        da.Fill(ds5, "PO_ORD_MAT")
        conn.Close()
        pono_DropDownList.DataSource = ds5.Tables("PO_ORD_MAT")
        pono_DropDownList.DataValueField = "PO_NO"
        pono_DropDownList.DataBind()
        ds5.Tables.Clear()
        pono_DropDownList.Enabled = True
        pono_DropDownList.Items.Add("Select")
        pono_DropDownList.SelectedValue = "Select"
        pono_DropDownList.Enabled = True
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim dt As New DataTable()

        'dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Quantity"), New DataColumn("Unit Price"), New DataColumn("Base Value"), New DataColumn("Disc. Type"), New DataColumn("Discount"), New DataColumn("Packing Type"), New DataColumn("Packing/Forwarding"), New DataColumn("Freight Type"), New DataColumn("Freight"), New DataColumn("Taxable Value"), New DataColumn("Mode Of Trans."), New DataColumn("CGST"), New DataColumn("CGST AMT"), New DataColumn("SGST"), New DataColumn("SGST AMT"), New DataColumn("IGST"), New DataColumn("IGST AMT"), New DataColumn("Total Value"), New DataColumn("Unit Weight(Kg)")})
        'ViewState("mat") = dt
        'Me.BINDGRID1()

        type_DropDown.AutoPostBack = False
        Panel1.Visible = False
        search_Label.Visible = True
        type_DropDown.Visible = True
        search_DropDown.Visible = True
        new_button.Visible = True
        view_button.Visible = True
        type_DropDown.Items.Clear()
        search_DropDown.Items.Clear()
        search_Label.Text = "PROFORMA INVOICE"
        type_DropDown.Items.Add("FINISH GOODS")
        conn.Open()
        da = New SqlDataAdapter("select distinct INVOICE_NO from proforma_invoice where INVOICE_NO like 'PINV%' ORDER BY INVOICE_NO", conn)
        da.Fill(ds, "proforma_invoice")
        search_DropDown.DataSource = ds.Tables("proforma_invoice")
        search_DropDown.DataValueField = "INVOICE_NO"
        search_DropDown.DataBind()
        conn.Close()
        Panel16.Visible = True
        Dim dt As New DataTable()
        dt.Columns.AddRange(New DataColumn(22) {New DataColumn("SlNo"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Quantity"), New DataColumn("Unit Price"), New DataColumn("Base Value"), New DataColumn("Disc. Type"), New DataColumn("Discount"), New DataColumn("Packing Type"), New DataColumn("Packing/Forwarding"), New DataColumn("Freight Type"), New DataColumn("Freight"), New DataColumn("Taxable Value"), New DataColumn("Mode Of Trans."), New DataColumn("CGST"), New DataColumn("CGST AMT"), New DataColumn("SGST"), New DataColumn("SGST AMT"), New DataColumn("IGST"), New DataColumn("IGST AMT"), New DataColumn("Total Value"), New DataColumn("Unit Weight(Kg)")})
        ViewState("mat") = dt
        crr_gridview.Font.Size = 8
        Me.BINDGRID1()

    End Sub

    Protected Sub view_button_Click(sender As Object, e As EventArgs) Handles view_button.Click
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable

        Dim PO_QUARY As String = "select * from proforma_invoice join F_ITEM on proforma_invoice.MAT_CODE=F_ITEM.ITEM_CODE where INVOICE_NO ='" & search_DropDown.SelectedValue & "'"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.Load(Server.MapPath("~/print_rpt/proforma_inv.rpt"))
        crystalReport.SetDataSource(dt)

        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

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

        ''''''''''''''''''''''''''''''''''''''''''''''''



        ''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub
End Class