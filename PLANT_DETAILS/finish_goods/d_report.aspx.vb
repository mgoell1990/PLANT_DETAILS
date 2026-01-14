Imports System.Globalization
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Web.UI
Imports ClosedXML.Excel
Public Class d_report
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
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (qual_code +' , '+qual_name) as qual_name from qual_group order by qual_code ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "qual_name"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            DropDownList1.Items.Insert(1, "All")
            DropDownList1.SelectedValue = "Select"
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox1.Text) = False Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Return
        End If
        Dim DATE_FROM, DATE_TO As Date
        Dim t_qty, t_weight, t_CAS4, t_ttax, t_tcs, t_sgst, t_cgst, t_igst, t_total_value As New Decimal(0)
        DATE_FROM = CDate(TextBox1.Text)
        DATE_TO = CDate(TextBox2.Text)
        If DropDownList1.Text = "" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList2.Text = "" Then
            DropDownList2.Focus()
            Return
        End If


        'search type
        Dim ds5 As New DataSet
        conn.Open()
        dt.Clear()
        If DropDownList1.Text <> "All" And DropDownList2.Text <> "All" Then

            da = New SqlDataAdapter("SELECT (DESPATCH .D_TYPE + DESPATCH .INV_NO) AS INV_NO, CONVERT(varchar, FORMAT(DESPATCH .INV_DATE, 'dd-MM-yyyy', 'en-us')) As INV_DATE ,ORDER_DETAILS .SO_ACTUAL,DESPATCH .FISCAL_YEAR  ,CONVERT(varchar, FORMAT(ORDER_DETAILS .SO_ACTUAL_DATE, 'dd-MM-yyyy', 'en-us')) As SO_ACTUAL_DATE , ORDER_DETAILS .PARTY_CODE, DESPATCH .P_CODE , DESPATCH .P_DESC,HSN_CODE , DESPATCH.TOTAL_PCS,DESPATCH .TRUCK_NO, DESPATCH .TAXABLE_VALUE AS CAS4, (cast(DESPATCH .PACK_PRICE as varchar) + DESPATCH .PACK_TYPE) AS PACKING , (cast(DESPATCH .RLY_ROAD_FRT as varchar) + DESPATCH .FRT_TYPE) AS FREIGHT,DESPATCH .TERM_AMT,DESPATCH .TCS_AMT,DESPATCH .TOTAL_WEIGHT , DESPATCH .BASE_PRICE ,DESPATCH .TRANS_NAME,qual_group .qual_desc AS QUAL_DESC,DESPATCH .TOTAL_AMT AS TOTAL_VALUE,DESPATCH .SGST_AMT,DESPATCH .CGST_AMT,DESPATCH .IGST_AMT, DESPATCH.MAT_SLNO  from (DESPATCH join ORDER_DETAILS on DESPATCH .SO_NO =ORDER_DETAILS .SO_NO) join SO_MAT_ORDER on DESPATCH.SO_NO=SO_MAT_ORDER.SO_NO and DESPATCH.MAT_SLNO=SO_MAT_ORDER.ITEM_SLNO join qual_group on SUBSTRING(DESPATCH .P_CODE, 1, 3) =qual_group .qual_code WHERE DESPATCH .INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "' AND (DESPATCH .INV_STATUS ='' or DESPATCH .INV_STATUS ='ACTIVE') and DESPATCH .P_CODE = '" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "'  and SO_MAT_ORDER.AMD_NO='0' ORDER BY INV_NO", conn)

        ElseIf DropDownList1.Text = "All" And DropDownList2.Text <> "All" Then

            da = New SqlDataAdapter("SELECT (DESPATCH .D_TYPE + DESPATCH .INV_NO) AS INV_NO, CONVERT(varchar, FORMAT(DESPATCH .INV_DATE, 'dd-MM-yyyy', 'en-us')) As INV_DATE ,ORDER_DETAILS .SO_ACTUAL,DESPATCH .FISCAL_YEAR  ,CONVERT(varchar, FORMAT(ORDER_DETAILS .SO_ACTUAL_DATE, 'dd-MM-yyyy', 'en-us')) As SO_ACTUAL_DATE , ORDER_DETAILS .PARTY_CODE, DESPATCH .P_CODE , DESPATCH .P_DESC,HSN_CODE , DESPATCH.TOTAL_PCS,DESPATCH .TRUCK_NO, DESPATCH .TAXABLE_VALUE AS CAS4, (cast(DESPATCH .PACK_PRICE as varchar) + DESPATCH .PACK_TYPE) AS PACKING , (cast(DESPATCH .RLY_ROAD_FRT as varchar) + DESPATCH .FRT_TYPE) AS FREIGHT,DESPATCH .TERM_AMT,DESPATCH .TCS_AMT,DESPATCH .TOTAL_WEIGHT , DESPATCH .BASE_PRICE ,DESPATCH .TRANS_NAME,qual_group .qual_desc AS QUAL_DESC,DESPATCH .TOTAL_AMT AS TOTAL_VALUE,DESPATCH .SGST_AMT,DESPATCH .CGST_AMT,DESPATCH .IGST_AMT, DESPATCH.MAT_SLNO  from (DESPATCH join ORDER_DETAILS on DESPATCH .SO_NO =ORDER_DETAILS .SO_NO) join SO_MAT_ORDER on DESPATCH.SO_NO=SO_MAT_ORDER.SO_NO and DESPATCH.MAT_SLNO=SO_MAT_ORDER.ITEM_SLNO join qual_group on SUBSTRING(DESPATCH .P_CODE, 1, 3) =qual_group .qual_code WHERE DESPATCH .INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "' AND (DESPATCH .INV_STATUS ='' or DESPATCH .INV_STATUS ='ACTIVE') and DESPATCH .P_CODE = '" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "'  and SO_MAT_ORDER.AMD_NO='0' ORDER BY INV_NO", conn)

        ElseIf DropDownList1.Text <> "All" And DropDownList2.Text = "All" Then

            da = New SqlDataAdapter("SELECT (DESPATCH .D_TYPE + DESPATCH .INV_NO) AS INV_NO, CONVERT(varchar, FORMAT(DESPATCH .INV_DATE, 'dd-MM-yyyy', 'en-us')) As INV_DATE ,ORDER_DETAILS .SO_ACTUAL,DESPATCH .FISCAL_YEAR  ,CONVERT(varchar, FORMAT(ORDER_DETAILS .SO_ACTUAL_DATE, 'dd-MM-yyyy', 'en-us')) As SO_ACTUAL_DATE , ORDER_DETAILS .PARTY_CODE, DESPATCH .P_CODE , DESPATCH .P_DESC,HSN_CODE , DESPATCH.TOTAL_PCS,DESPATCH .TRUCK_NO, DESPATCH .TAXABLE_VALUE AS CAS4, (cast(DESPATCH .PACK_PRICE as varchar) + DESPATCH .PACK_TYPE) AS PACKING , (cast(DESPATCH .RLY_ROAD_FRT as varchar) + DESPATCH .FRT_TYPE) AS FREIGHT,DESPATCH .TERM_AMT,DESPATCH .TCS_AMT,DESPATCH .TOTAL_WEIGHT , DESPATCH .BASE_PRICE ,DESPATCH .TRANS_NAME,qual_group .qual_desc AS QUAL_DESC,DESPATCH .TOTAL_AMT AS TOTAL_VALUE,DESPATCH .SGST_AMT,DESPATCH .CGST_AMT,DESPATCH .IGST_AMT, DESPATCH.MAT_SLNO  from (DESPATCH join ORDER_DETAILS on DESPATCH .SO_NO =ORDER_DETAILS .SO_NO) join SO_MAT_ORDER on DESPATCH.SO_NO=SO_MAT_ORDER.SO_NO and DESPATCH.MAT_SLNO=SO_MAT_ORDER.ITEM_SLNO join qual_group on SUBSTRING(DESPATCH .P_CODE, 1, 3) =qual_group .qual_code WHERE DESPATCH .INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "' AND (DESPATCH .INV_STATUS ='' or DESPATCH .INV_STATUS ='ACTIVE') and DESPATCH .P_CODE like '" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "%'  and SO_MAT_ORDER.AMD_NO='0' ORDER BY INV_NO", conn)
        ElseIf DropDownList1.Text = "All" And DropDownList2.Text = "All" Then

            da = New SqlDataAdapter("SELECT (DESPATCH .D_TYPE + DESPATCH .INV_NO) AS INV_NO, CONVERT(varchar, FORMAT(DESPATCH .INV_DATE, 'dd-MM-yyyy', 'en-us')) As INV_DATE ,ORDER_DETAILS .SO_ACTUAL,DESPATCH .FISCAL_YEAR  ,CONVERT(varchar, FORMAT(ORDER_DETAILS .SO_ACTUAL_DATE, 'dd-MM-yyyy', 'en-us')) As SO_ACTUAL_DATE , ORDER_DETAILS .PARTY_CODE, DESPATCH .P_CODE , DESPATCH .P_DESC,HSN_CODE , DESPATCH.TOTAL_PCS,DESPATCH .TRUCK_NO, DESPATCH .TAXABLE_VALUE AS CAS4, (cast(DESPATCH .PACK_PRICE as varchar) + DESPATCH .PACK_TYPE) AS PACKING , (cast(DESPATCH .RLY_ROAD_FRT as varchar) + DESPATCH .FRT_TYPE) AS FREIGHT,DESPATCH .TERM_AMT,DESPATCH .TCS_AMT,DESPATCH .TOTAL_WEIGHT , DESPATCH .BASE_PRICE ,DESPATCH .TRANS_NAME,qual_group .qual_desc AS QUAL_DESC,DESPATCH .TOTAL_AMT AS TOTAL_VALUE,DESPATCH .SGST_AMT,DESPATCH .CGST_AMT,DESPATCH .IGST_AMT, DESPATCH.MAT_SLNO  from (DESPATCH join ORDER_DETAILS on DESPATCH .SO_NO =ORDER_DETAILS .SO_NO) join SO_MAT_ORDER on DESPATCH.SO_NO=SO_MAT_ORDER.SO_NO and DESPATCH.MAT_SLNO=SO_MAT_ORDER.ITEM_SLNO join qual_group on SUBSTRING(DESPATCH .P_CODE, 1, 3) =qual_group .qual_code WHERE DESPATCH .INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "' AND (DESPATCH .INV_STATUS ='' or DESPATCH .INV_STATUS ='ACTIVE')  and SO_MAT_ORDER.AMD_NO='0' ORDER BY INV_NO", conn)

        End If

        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
        count = 0

        For Me.count = 0 To GridView1.Rows.Count - 1
            'Calculating total values of griditem
            t_qty = t_qty + CDec(GridView1.Rows(count).Cells(8).Text)
            t_weight = t_weight + CDec(GridView1.Rows(count).Cells(9).Text)
            t_CAS4 = t_CAS4 + CDec(GridView1.Rows(count).Cells(14).Text)
            t_ttax = t_ttax + CDec(GridView1.Rows(count).Cells(16).Text)
            t_tcs = t_tcs + CDec(GridView1.Rows(count).Cells(17).Text)
            t_sgst = t_sgst + CDec(GridView1.Rows(count).Cells(19).Text)
            t_cgst = t_cgst + CDec(GridView1.Rows(count).Cells(20).Text)
            t_igst = t_igst + CDec(GridView1.Rows(count).Cells(21).Text)
            t_total_value = t_total_value + CDec(GridView1.Rows(count).Cells(22).Text)



        Next

        Dim dRow As DataRow
        dRow = dt.NewRow
        dRow.Item(0) = "Total"
        dRow.Item(9) = t_qty
        dRow.Item(16) = t_weight
        dRow.Item(11) = t_CAS4

        dRow.Item(20) = t_total_value
        dt.Rows.Add(dRow)

        ''dt.Rows.Add("Total", "", "", "", "", "", "", "", t_qty, t_weight, "", "", "", t_total_value, "", "", "", "", "", "", "", "", "", "", "", "", "")
        dt.AcceptChanges()
        GridView1.DataSource = dt
        GridView1.DataBind()

        'For Me.count = 0 To GridView1.Rows.Count - 2
        '    conn.Open()
        '    Dim BASE_VALUE As Decimal = 0

        '    Dim mc As New SqlCommand
        '    mc.CommandText = "select (case when DESPATCH.ACC_UNIT='SET' THEN (DESPATCH.TOTAL_QTY * DESPATCH.BASE_PRICE) ELSE(CASE when DESPATCH.TOTAL_PCS = 0 then (DESPATCH.TOTAL_WEIGHT * DESPATCH.BASE_PRICE) else (DESPATCH.TOTAL_PCS*DESPATCH.BASE_PRICE) end) end) AS BASE_VALUE from DESPATCH where DESPATCH .D_TYPE + DESPATCH .INV_NO  ='" & GridView1.Rows(count).Cells(0).Text & "' AND DESPATCH.FISCAL_YEAR='" & GridView1.Rows(count).Cells(24).Text & "'"
        '    mc.Connection = conn
        '    dr = mc.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()
        '        BASE_VALUE = dr.Item("BASE_VALUE")
        '        dr.Close()
        '    Else
        '        conn.Close()
        '    End If
        '    conn.Close()
        '    GridView1.Rows(count).Cells(13).Text = BASE_VALUE
        'Next
        GridView1.Rows(GridView1.Rows.Count - 1).Font.Bold = True

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'If GridView1.Rows.Count < 0 Then
        '    Return
        'End If

        'Dim dt3 As DataTable = New DataTable(GridView1.Rows(0).Cells(23).Text)
        'dt3.Columns.Add(New DataColumn("Inv. No", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Inv. Date", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Act. SO", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Act. SO Date", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Mat. SLNo", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Party Code", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Item Code", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Item Name", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Qty Pcs", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("Total Weight", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("Base Price", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Tran. Name", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Truck No", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Base Value", GetType(String)))
        'dt3.Columns.Add(New DataColumn("CAS4", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("Packing", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Term Tax", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("TCS", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("Freight", GetType(String)))
        'dt3.Columns.Add(New DataColumn("SGST", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("CGST", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("IGST", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("Total Value", GetType(Decimal)))
        'dt3.Columns.Add(New DataColumn("Qual. Grp", GetType(String)))
        'dt3.Columns.Add(New DataColumn("Fiscal Year", GetType(String)))
        'dt3.Columns.Add(New DataColumn("HSN Code", GetType(String)))
        'For Me.count = 0 To GridView1.Rows.Count - 2
        '    Dim trans_name As String = ""
        '    If GridView1.Rows(count).Cells(11).Text <> "&nbsp;" Then
        '        trans_name = GridView1.Rows(count).Cells(11).Text
        '    End If
        '    dt3.Rows.Add(GridView1.Rows(count).Cells(0).Text, GridView1.Rows(count).Cells(1).Text, GridView1.Rows(count).Cells(2).Text, GridView1.Rows(count).Cells(3).Text, GridView1.Rows(count).Cells(4).Text, GridView1.Rows(count).Cells(5).Text, GridView1.Rows(count).Cells(6).Text, GridView1.Rows(count).Cells(7).Text, GridView1.Rows(count).Cells(8).Text, GridView1.Rows(count).Cells(9).Text, GridView1.Rows(count).Cells(10).Text, trans_name, GridView1.Rows(count).Cells(12).Text, GridView1.Rows(count).Cells(13).Text, GridView1.Rows(count).Cells(14).Text, GridView1.Rows(count).Cells(15).Text, GridView1.Rows(count).Cells(16).Text, GridView1.Rows(count).Cells(17).Text, GridView1.Rows(count).Cells(18).Text, GridView1.Rows(count).Cells(19).Text, GridView1.Rows(count).Cells(20).Text, GridView1.Rows(count).Cells(21).Text, GridView1.Rows(count).Cells(22).Text, GridView1.Rows(count).Cells(23).Text, GridView1.Rows(count).Cells(24).Text, GridView1.Rows(count).Cells(25).Text)
        'Next

        'Using wb As New XLWorkbook()

        '    wb.Worksheets.Add(dt3)
        '    'Export the Excel file.
        '    Response.Clear()
        '    Response.Buffer = True
        '    Response.Charset = ""
        '    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        '    Response.AddHeader("content-disposition", "attachment;filename=SaleReport.xlsx")
        '    Using MyMemoryStream As New MemoryStream()
        '        wb.SaveAs(MyMemoryStream)
        '        MyMemoryStream.WriteTo(Response.OutputStream)
        '        Response.Flush()
        '        Response.End()
        '    End Using
        'End Using

        If GridView1.Rows.Count > 0 Then
            Try

                Dim dt As DataTable = New DataTable()
                For j As Integer = 0 To GridView1.Columns.Count - 1
                    dt.Columns.Add(GridView1.Columns(j).HeaderText)
                Next
                For i As Integer = 0 To GridView1.Rows.Count - 1
                    Dim dr As DataRow = dt.NewRow()
                    For j As Integer = 0 To GridView1.Columns.Count - 1
                        If (GridView1.Rows(i).Cells(j).Text <> "") Then
                            dr(GridView1.Columns(j).HeaderText) = GridView1.Rows(i).Cells(j).Text
                        End If

                    Next
                    dt.Rows.Add(dr)
                Next

                Using wb As XLWorkbook = New XLWorkbook()
                    wb.Worksheets.Add(dt, "Sale Report")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=SaleReport.xlsx")
                    Using MyMemoryStream As MemoryStream = New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using

            Catch ex As Exception
            Finally

            End Try
        End If

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList1.SelectedValue = "All" Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (ITEM_CODE + ' , '+ ITEM_NAME ) as ITEM_NAME from F_ITEM ORDER BY ITEM_CODE ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "ITEM_NAME"
            DropDownList2.DataBind()
            DropDownList2.Items.Insert(0, "Select")
            DropDownList2.Items.Insert(1, "All")
            DropDownList2.SelectedValue = "Select"
        ElseIf DropDownList1.SelectedValue <> "Select" And DropDownList1.SelectedValue <> "All" Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (ITEM_CODE + ' , '+ ITEM_NAME ) as ITEM_NAME from F_ITEM where ITEM_TYPE ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' ORDER BY ITEM_CODE ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "ITEM_NAME"
            DropDownList2.DataBind()
            DropDownList2.Items.Insert(0, "Select")
            DropDownList2.Items.Insert(1, "All")
            DropDownList2.SelectedValue = "Select"
        End If

    End Sub







End Class