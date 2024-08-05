Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class GSTR_1
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim dt As New DataTable
    Dim da As New SqlDataAdapter
    Dim count As Integer

    Protected Sub BINDGRID()
        GridView2.DataSource = DirectCast(ViewState("GSTR1"), DataTable)
        GridView2.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dt1 As New DataTable()
            'dt1.Columns.AddRange(New DataColumn(5) {New DataColumn("SR No."), New DataColumn("Unit"), New DataColumn("Characteristics"), New DataColumn("Gauranted Specification As Per PO"), New DataColumn("Rebate Clause Value As Per PO"), New DataColumn("Test Result At SRU Lab")})
            ViewState("GSTR1") = dt1
            BINDGRID()
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsDate(TextBox1.Text) = False Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        Dim month_year As New String("")
        from_date = CDate(TextBox1.Text)
        to_date = CDate(TextBox2.Text)
        month_year = from_date.Month.ToString("D2") & from_date.Year

        conn.Open()
        dt.Clear()
        'Dim quary As String = "select (select c_gst_no from comp_profile) as supplier_gst,'N' as flag, BILL_PARTY_GST_N as party_gst,(CASE WHEN d2.d_code is null THEN s1.SUPL_NAME ELSE d2.d_name end) as receiver_name, '' as e_commerce_gstin,'" & month_year & "' as return_period, 'CGSRU' AS control_object, D_TYPE+INV_NO as invoice_number, LEFT((D_TYPE+INV_NO), 2) as prefix, '' as suffix, RIGHT((D_TYPE+INV_NO), 10) as number, FORMAT(INV_DATE, 'dd-MM-yyyy', 'en-us') AS invoice_date,
        '    TOTAL_AMT as invoice_amt, 'R' as export_type, B_STATE_CODE+'-'+B_STATE as place_of_spply, 'N' as reverse_charge, '' as extensible_header_1, '' as extensible_header_2, '' as extensible_header_3, '' as extensible_header_4, '' as extensible_header_5, '' as extensible_header_6, '1' as item_number,'NA' as nil_type, CHPTR_HEAD as hsn, c1.CHPT_NAME,(CASE WHEN d1.CHPTR_HEAD ='999794' THEN 'S' ELSE 'G' end) as goods_services, TOTAL_WEIGHT,
        '    (CASE WHEN d1.TOTAL_WEIGHT =0.00 THEN 'OTH-OTHERS' ELSE 'MTS-METRIC TON' end) as UQC, (TAXABLE_VALUE+TERM_AMT) as TAXABLE_VALUE, IGST_RATE, IGST_AMT, CGST_RATE,CGST_AMT,SGST_RATE,SGST_AMT,CESS_RATE,CESS_AMT from DESPATCH d1 join order_details o1 on d1.SO_NO=o1.SO_NO left join SUPL s1 on d1.PARTY_CODE=s1.SUPL_ID left join dater d2 on d1.PARTY_CODE=d2.d_code 
        '    left join CHPTR_HEADING c1 on d1.CHPTR_HEAD= c1.CHPT_CODE where INV_DATE between ' " & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & " ' and D_TYPE <> 'DC154' and (o1.PO_TYPE<>'FREIGHT INWARD' and o1.PO_TYPE<>'FREIGHT OUTWARD') and d1.INV_STATUS<>'CANCELLED' order by D_TYPE,INV_NO,party_gst"

        Dim quary As String = "select (select c_gst_no from comp_profile) as supplier_gst,'N' as flag, BILL_PARTY_GST_N as party_gst,(CASE WHEN d2.d_code is null THEN s1.SUPL_NAME ELSE d2.d_name end) as receiver_name, '' as e_commerce_gstin,'" & month_year & "' as return_period, 'CGSRU' AS control_object, D_TYPE+INV_NO as invoice_number, LEFT((D_TYPE+INV_NO), 2) as prefix, '' as suffix, RIGHT((D_TYPE+INV_NO), 10) as number, FORMAT(INV_DATE, 'dd-MM-yyyy', 'en-us') AS invoice_date,
            TOTAL_AMT as invoice_amt, 'R' as export_type, B_STATE_CODE+'-'+B_STATE as place_of_spply, 'N' as reverse_charge, '' as extensible_header_1, '' as extensible_header_2, '' as extensible_header_3, '' as extensible_header_4, '' as extensible_header_5, '' as extensible_header_6, '1' as item_number,'NA' as nil_type, CHPTR_HEAD as hsn, c1.CHPT_NAME,(CASE WHEN d1.CHPTR_HEAD ='999794' THEN 'S' ELSE 'G' end) as goods_services, TOTAL_WEIGHT,
            (CASE WHEN d1.TOTAL_WEIGHT =0.00 THEN 'OTH-OTHERS' ELSE 'MTS-METRIC TON' end) as UQC, (TAXABLE_VALUE+TERM_AMT) as TAXABLE_VALUE, IGST_RATE, IGST_AMT, CGST_RATE,CGST_AMT,SGST_RATE,SGST_AMT,CESS_RATE,CESS_AMT from DESPATCH d1 join order_details o1 on d1.SO_NO=o1.SO_NO left join SUPL s1 on d1.PARTY_CODE=s1.SUPL_ID left join dater d2 on d1.PARTY_CODE=d2.d_code 
            left join CHPTR_HEADING c1 on d1.CHPTR_HEAD= c1.CHPT_CODE where INV_DATE between ' " & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & " ' and D_TYPE <> 'DC154' and (o1.PO_TYPE<>'FREIGHT INWARD' and o1.PO_TYPE<>'FREIGHT OUTWARD') and d1.INV_STATUS<>'CANCELLED' order by D_TYPE,INV_NO,party_gst"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        ''''''''''''''''''''''''''''''''''
        Panel1.Visible = False
        Panel11.Visible = True
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If GridView2.Rows.Count < 0 Then
            Return
        End If

        Dim dt1 As DataTable = New DataTable("GSTR_1_B2B")
        dt1.Columns.Add(New DataColumn("GSTIN/UIN of Supplier", GetType(String)))
        dt1.Columns.Add(New DataColumn("Flag", GetType(String)))
        dt1.Columns.Add(New DataColumn("GSTIN/UIN of Receiver", GetType(String)))
        dt1.Columns.Add(New DataColumn("Receiver Name", GetType(String)))
        dt1.Columns.Add(New DataColumn("E-Commerce GSTIN", GetType(String)))
        dt1.Columns.Add(New DataColumn("Return Period", GetType(String)))
        dt1.Columns.Add(New DataColumn("Control Object", GetType(String)))
        dt1.Columns.Add(New DataColumn("Invoice Number", GetType(String)))
        dt1.Columns.Add(New DataColumn("Prefix", GetType(String)))
        dt1.Columns.Add(New DataColumn("Suffix", GetType(String)))
        dt1.Columns.Add(New DataColumn("Number", GetType(String)))
        dt1.Columns.Add(New DataColumn("Invoice Date", GetType(String)))
        dt1.Columns.Add(New DataColumn("Invoice Amount(INR)", GetType(String)))
        dt1.Columns.Add(New DataColumn("Export Type", GetType(String)))
        dt1.Columns.Add(New DataColumn("Place of Supply", GetType(String)))
        dt1.Columns.Add(New DataColumn("Reverse Charge", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 1", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 2", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 3", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 4", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 5", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 6", GetType(String)))
        dt1.Columns.Add(New DataColumn("Item Number", GetType(String)))
        dt1.Columns.Add(New DataColumn("Nil Type", GetType(String)))
        dt1.Columns.Add(New DataColumn("HSN", GetType(String)))
        dt1.Columns.Add(New DataColumn("Description", GetType(String)))
        dt1.Columns.Add(New DataColumn("Goods/Services", GetType(String)))
        dt1.Columns.Add(New DataColumn("Quantity", GetType(String)))
        dt1.Columns.Add(New DataColumn("UQC", GetType(String)))
        dt1.Columns.Add(New DataColumn("Taxable Base(INR)", GetType(String)))
        dt1.Columns.Add(New DataColumn("IGST Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("IGST Amt", GetType(String)))
        dt1.Columns.Add(New DataColumn("CGST Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("CGST Amt", GetType(String)))
        dt1.Columns.Add(New DataColumn("SGST Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("SGST Amt", GetType(String)))
        dt1.Columns.Add(New DataColumn("CESS Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("CESS Amt", GetType(String)))

        For Me.count = 0 To GridView2.Rows.Count - 1
            dt1.Rows.Add(GridView2.Rows(count).Cells(0).Text, GridView2.Rows(count).Cells(1).Text, GridView2.Rows(count).Cells(2).Text, GridView2.Rows(count).Cells(3).Text, GridView2.Rows(count).Cells(4).Text, GridView2.Rows(count).Cells(5).Text, GridView2.Rows(count).Cells(6).Text, GridView2.Rows(count).Cells(7).Text, GridView2.Rows(count).Cells(8).Text, GridView2.Rows(count).Cells(9).Text, GridView2.Rows(count).Cells(10).Text, GridView2.Rows(count).Cells(11).Text, GridView2.Rows(count).Cells(12).Text, GridView2.Rows(count).Cells(13).Text, GridView2.Rows(count).Cells(14).Text, GridView2.Rows(count).Cells(15).Text, GridView2.Rows(count).Cells(16).Text, GridView2.Rows(count).Cells(17).Text, GridView2.Rows(count).Cells(18).Text, GridView2.Rows(count).Cells(19).Text, GridView2.Rows(count).Cells(20).Text, GridView2.Rows(count).Cells(21).Text, GridView2.Rows(count).Cells(22).Text, GridView2.Rows(count).Cells(23).Text, GridView2.Rows(count).Cells(24).Text, GridView2.Rows(count).Cells(25).Text, GridView2.Rows(count).Cells(26).Text, GridView2.Rows(count).Cells(27).Text, GridView2.Rows(count).Cells(28).Text, GridView2.Rows(count).Cells(29).Text, GridView2.Rows(count).Cells(30).Text, GridView2.Rows(count).Cells(31).Text, GridView2.Rows(count).Cells(32).Text, GridView2.Rows(count).Cells(33).Text, GridView2.Rows(count).Cells(34).Text, GridView2.Rows(count).Cells(35).Text, GridView2.Rows(count).Cells(36).Text, GridView2.Rows(count).Cells(37).Text)
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt1)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=GSTR1_B2B_" & CDate(TextBox1.Text).Month.ToString("D2") & CDate(TextBox1.Text).Year & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If IsDate(TextBox1.Text) = False Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        Dim month_year As New String("")
        from_date = CDate(TextBox1.Text)
        to_date = CDate(TextBox2.Text)
        month_year = from_date.Month.ToString("D2") & from_date.Year

        conn.Open()
        dt.Clear()
        Dim quary As String = "select (select c_gst_no from comp_profile) as supplier_gst,'N' as flag, '' as e_commerce_gstin,(CASE WHEN d2.d_code is null THEN s1.SUPL_NAME ELSE d2.d_name end) as receiver_name, '22-CHHATTISGARH' as receiver_state,'" & month_year & "' as return_period, 'CGSRU' AS control_object, D_TYPE+INV_NO as invoice_number, LEFT((D_TYPE+INV_NO), 2) as prefix, '' as suffix, RIGHT((D_TYPE+INV_NO), 10) as number, FORMAT(INV_DATE, 'dd-MM-yyyy', 'en-us') AS invoice_date,
            TOTAL_AMT as invoice_amt, B_STATE_CODE+'-'+B_STATE as place_of_spply, '' as extensible_header_1, '' as extensible_header_2, '' as extensible_header_3, '' as extensible_header_4, '' as extensible_header_5, '' as extensible_header_6, '1' as item_number,'NA' as nil_type, CHPTR_HEAD as hsn, c1.CHPT_NAME,(CASE WHEN d1.CHPTR_HEAD ='999794' THEN 'S' ELSE 'G' end) as goods_services,'' As TOTAL_WEIGHT, 
            (CASE WHEN d1.TOTAL_WEIGHT =0.00 THEN 'OTH-OTHERS' ELSE 'MTS-METRIC TON' end) as UQC, TAXABLE_VALUE, IGST_RATE, IGST_AMT, CGST_RATE,CGST_AMT,SGST_RATE,SGST_AMT,CESS_RATE,CESS_AMT from DESPATCH d1 join order_details o1 on d1.SO_NO=o1.SO_NO left join SUPL s1 on d1.PARTY_CODE=s1.SUPL_ID left join dater d2 on d1.PARTY_CODE=d2.d_code 
            left join CHPTR_HEADING c1 on d1.CHPTR_HEAD= c1.CHPT_CODE where INV_DATE between ' " & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & " ' and D_TYPE <> 'DC154' and (o1.PO_TYPE='FREIGHT INWARD' or o1.PO_TYPE='FREIGHT OUTWARD') and d1.INV_STATUS<>'CANCELLED'
            union
			select (select c_gst_no from comp_profile) as supplier_gst,'N' as flag, '' as e_commerce_gstin,(CASE WHEN d2.d_code is null THEN s1.SUPL_NAME ELSE d2.d_name end) as receiver_name, '22-CHHATTISGARH' as receiver_state,'" & month_year & "' as return_period, 'CGSRU' AS control_object, D_TYPE+INV_NO as invoice_number, LEFT((D_TYPE+INV_NO), 2) as prefix, '' as suffix, RIGHT((D_TYPE+INV_NO), 10) as number, FORMAT(INV_DATE, 'dd-MM-yyyy', 'en-us') AS invoice_date,
            TOTAL_AMT as invoice_amt, B_STATE_CODE+'-'+B_STATE as place_of_spply, '' as extensible_header_1, '' as extensible_header_2, '' as extensible_header_3, '' as extensible_header_4, '' as extensible_header_5, '' as extensible_header_6, '1' as item_number,'NA' as nil_type, CHPTR_HEAD as hsn, c1.CHPT_NAME,(CASE WHEN d1.CHPTR_HEAD ='999794' THEN 'S' ELSE 'G' end) as goods_services,'' As TOTAL_WEIGHT, 
            (CASE WHEN d1.TOTAL_WEIGHT =0.00 THEN 'OTH-OTHERS' ELSE 'MTS-METRIC TON' end) as UQC, TAXABLE_VALUE, IGST_RATE, IGST_AMT, CGST_RATE,CGST_AMT,SGST_RATE,SGST_AMT,CESS_RATE,CESS_AMT from DESPATCH d1 left join order_details o1 on d1.SO_NO=o1.SO_NO left join SUPL s1 on d1.PARTY_CODE=s1.SUPL_ID left join dater d2 on d1.PARTY_CODE=d2.d_code 
            left join CHPTR_HEADING c1 on d1.CHPTR_HEAD= c1.CHPT_CODE where INV_DATE between ' " & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & " ' and D_TYPE <> 'DC154' and d1.P_DESC='EMD FOREFEIT' and d1.INV_STATUS<>'CANCELLED' order by invoice_number"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        ''''''''''''''''''''''''''''''''''
        Panel11.Visible = False
        Panel1.Visible = True
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If GridView1.Rows.Count < 0 Then
            Return
        End If

        Dim dt1 As DataTable = New DataTable("GSTR_1_B2C")
        dt1.Columns.Add(New DataColumn("GSTIN/UIN of Supplier", GetType(String)))
        dt1.Columns.Add(New DataColumn("Flag", GetType(String)))
        dt1.Columns.Add(New DataColumn("E-Commerce GSTIN", GetType(String)))
        dt1.Columns.Add(New DataColumn("Receiver Name", GetType(String)))
        dt1.Columns.Add(New DataColumn("Receiver State", GetType(String)))
        dt1.Columns.Add(New DataColumn("Return Period", GetType(String)))
        dt1.Columns.Add(New DataColumn("Control Object", GetType(String)))
        dt1.Columns.Add(New DataColumn("Invoice Number", GetType(String)))
        dt1.Columns.Add(New DataColumn("Prefix", GetType(String)))
        dt1.Columns.Add(New DataColumn("Suffix", GetType(String)))
        dt1.Columns.Add(New DataColumn("Number", GetType(String)))
        dt1.Columns.Add(New DataColumn("Invoice Date", GetType(String)))
        dt1.Columns.Add(New DataColumn("Invoice Amount(INR)", GetType(String)))
        dt1.Columns.Add(New DataColumn("Place of Supply", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 1", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 2", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 3", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 4", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 5", GetType(String)))
        dt1.Columns.Add(New DataColumn("Extensible Header Field 6", GetType(String)))
        dt1.Columns.Add(New DataColumn("Item Number", GetType(String)))
        dt1.Columns.Add(New DataColumn("Nil Type", GetType(String)))
        dt1.Columns.Add(New DataColumn("HSN", GetType(String)))
        dt1.Columns.Add(New DataColumn("Description", GetType(String)))
        dt1.Columns.Add(New DataColumn("Goods/Services", GetType(String)))
        dt1.Columns.Add(New DataColumn("Quantity", GetType(String)))
        dt1.Columns.Add(New DataColumn("UQC", GetType(String)))
        dt1.Columns.Add(New DataColumn("Taxable Base(INR)", GetType(String)))
        dt1.Columns.Add(New DataColumn("IGST Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("IGST Amt", GetType(String)))
        dt1.Columns.Add(New DataColumn("CGST Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("CGST Amt", GetType(String)))
        dt1.Columns.Add(New DataColumn("SGST Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("SGST Amt", GetType(String)))
        dt1.Columns.Add(New DataColumn("CESS Rate", GetType(String)))
        dt1.Columns.Add(New DataColumn("CESS Amt", GetType(String)))

        For Me.count = 0 To GridView1.Rows.Count - 1
            dt1.Rows.Add(GridView1.Rows(count).Cells(0).Text, GridView1.Rows(count).Cells(1).Text, GridView1.Rows(count).Cells(2).Text, GridView1.Rows(count).Cells(3).Text, GridView1.Rows(count).Cells(4).Text, GridView1.Rows(count).Cells(5).Text, GridView1.Rows(count).Cells(6).Text, GridView1.Rows(count).Cells(7).Text, GridView1.Rows(count).Cells(8).Text, GridView1.Rows(count).Cells(9).Text, GridView1.Rows(count).Cells(10).Text, GridView1.Rows(count).Cells(11).Text, GridView1.Rows(count).Cells(12).Text, GridView1.Rows(count).Cells(13).Text, GridView1.Rows(count).Cells(14).Text, GridView1.Rows(count).Cells(15).Text, GridView1.Rows(count).Cells(16).Text, GridView1.Rows(count).Cells(17).Text, GridView1.Rows(count).Cells(18).Text, GridView1.Rows(count).Cells(19).Text, GridView1.Rows(count).Cells(20).Text, GridView1.Rows(count).Cells(21).Text, GridView1.Rows(count).Cells(22).Text, GridView1.Rows(count).Cells(23).Text, GridView1.Rows(count).Cells(24).Text, GridView1.Rows(count).Cells(25).Text, GridView1.Rows(count).Cells(26).Text, GridView1.Rows(count).Cells(27).Text, GridView1.Rows(count).Cells(28).Text, GridView1.Rows(count).Cells(29).Text, GridView1.Rows(count).Cells(30).Text, GridView1.Rows(count).Cells(31).Text, GridView1.Rows(count).Cells(32).Text, GridView1.Rows(count).Cells(33).Text, GridView1.Rows(count).Cells(34).Text, GridView1.Rows(count).Cells(35).Text)
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt1)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=GSTR1_B2C_" & CDate(TextBox1.Text).Month.ToString("D2") & CDate(TextBox1.Text).Year & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub
End Class