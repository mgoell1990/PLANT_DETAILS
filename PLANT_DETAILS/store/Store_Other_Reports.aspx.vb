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
Imports ClosedXML.Excel
Imports System.IO

Public Class Store_Other_Reports
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
    Dim crystalReport As New ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MultiView1.ActiveViewIndex = 0

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("storeAccess")) Or Session("storeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click

        Dim s_date, target_date As Date
        Dim duration As Int32
        duration = DropDownList10.SelectedValue
        s_date = CDate(TextBox19.Text)
        target_date = CDate(TextBox19.Text).AddYears(-1 * (duration))
        Dim dt As New DataTable()

        conn.Open()
        dt.Clear()
        'da = New SqlDataAdapter("select *,'" & s_date.Day & "-" & s_date.Month & "-" & s_date.Year & "' AS SEARCH_DATE from MATERIAL where MAT_CODE not like '100%' and LAST_TRANS_DATE < '" & target_date.Year & "-" & target_date.Month & "-" & target_date.Day & "' AND MAT_STOCK > 0 order by MAT_CODE", conn)
        da = New SqlDataAdapter("select ROW_NUMBER() OVER (ORDER BY MAT_CODE) AS ROW_NO,*,'" & s_date.Day & "-" & s_date.Month & "-" & s_date.Year & "' AS SEARCH_DATE,(MAT_AVG*MAT_STOCK) AS MAT_VALUE, (SELECT dbo.fnc_FiscalYear(LAST_ISSUE_DATE)) As issue_fy from MATERIAL where MAT_CODE not like '100%' and LAST_ISSUE_DATE < '" & target_date.Year & "-" & target_date.Month & "-" & target_date.Day & "' AND MAT_STOCK > 0 order by MAT_CODE", conn)
        da.Fill(dt)
        conn.Close()
        GridView10.DataSource = dt
        GridView10.DataBind()


    End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Non-Moving Items" Then
            MultiView1.ActiveViewIndex = 0
        End If
    End Sub

    Protected Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click

        If GridView10.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(8) {New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_LASTPUR_DATE", GetType(String)), New DataColumn("LAST_ISSUE_DATE", GetType(String)), New DataColumn("ISSUE_FY"), New DataColumn("MAT_STOCK", GetType(Decimal)), New DataColumn("MAT_VALUE", GetType(Decimal)), New DataColumn("SEARCH_DATE"), New DataColumn("DURATION")})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView10.Rows.Count - 1
            dt2.Rows.Add(GridView10.Rows(i).Cells(1).Text, GridView10.Rows(i).Cells(2).Text, GridView10.Rows(i).Cells(3).Text, GridView10.Rows(i).Cells(4).Text, GridView10.Rows(i).Cells(5).Text, GridView10.Rows(i).Cells(6).Text, GridView10.Rows(i).Cells(7).Text, TextBox19.Text, DropDownList10.SelectedValue)
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/non_moving_stores.rpt"))
        crystalReport.SetDataSource(dt2)
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
    End Sub

    Protected Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If GridView10.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Non_Moving_Items")
        dt3.Columns.Add(New DataColumn("Sl No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Last Purchase Date", GetType(Date)))
        dt3.Columns.Add(New DataColumn("Last Issue Date", GetType(Date)))
        dt3.Columns.Add(New DataColumn("Fiscal Year", GetType(String)))
        dt3.Columns.Add(New DataColumn("Stock", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Value", GetType(Decimal)))

        For Me.count = 0 To GridView10.Rows.Count - 1
            dt3.Rows.Add(GridView10.Rows(count).Cells(0).Text, GridView10.Rows(count).Cells(1).Text, GridView10.Rows(count).Cells(2).Text, GridView10.Rows(count).Cells(3).Text, GridView10.Rows(count).Cells(4).Text, GridView10.Rows(count).Cells(5).Text, CDec(GridView10.Rows(count).Cells(6).Text), CDec(GridView10.Rows(count).Cells(7).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Non_Moving_Items_Stores.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub
End Class