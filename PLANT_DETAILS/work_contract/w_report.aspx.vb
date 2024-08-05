Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class w_report
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("userName") = "" Then
            '    Response.Redirect("~/Account/Login")
            '    Return
            'End If

        End If
    End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            MultiView1.ActiveViewIndex = -1
            Return
        ElseIf DropDownList9.SelectedValue = "Pending For M.B." Then
            MultiView1.ActiveViewIndex = 0
        ElseIf DropDownList9.SelectedValue = "Date Wise Work" Then
            MultiView1.ActiveViewIndex = 1
        ElseIf DropDownList9.SelectedValue = "M.B. Details" Then
            MultiView1.ActiveViewIndex = 2
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT FY from FISCAL_YEAR ORDER BY FY desc", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList13.Items.Clear()
            DropDownList13.DataSource = dt
            DropDownList13.DataValueField = "FY"
            DropDownList13.DataBind()
            DropDownList13.Items.Insert(0, "Select")
            DropDownList13.SelectedValue = "Select"
        End If
    End Sub

    Protected Sub Button61_Click(sender As Object, e As EventArgs) Handles Button61.Click
        Dim from_date, to_date As Date
        from_date = CDate(TextBox33.Text)
        to_date = CDate(TextBox34.Text)
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select * from daily_work join SUPL on daily_work .supl_id =supl.supl_id where daily_work.from_date >='" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and daily_work.to_date <='" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and daily_work.mb_gen_ind ='' order by po_no", conn)
        da.Fill(dt)
        conn.Close()
        GridView5.DataSource = dt
        GridView5.DataBind()
    End Sub

    Protected Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        If GridView5.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(9) {New DataColumn("PO_NO"), New DataColumn("SUPL_NAME"), New DataColumn("W_NAME"), New DataColumn("W_AU"), New DataColumn("W_QTY"), New DataColumn("unit_rate"), New DataColumn("total_amt"), New DataColumn("RPT_FOR"), New DataColumn("RPT_DATE"), New DataColumn("total_value")})
        ''insert datatable value
        Dim i As Integer = 0
        Dim total_value As Decimal = 0
        For i = 0 To GridView5.Rows.Count - 1
            total_value = total_value + CDec(GridView5.Rows(i).Cells(6).Text)
        Next

        i = 0
        For i = 0 To GridView5.Rows.Count - 1
            dt2.Rows.Add(GridView5.Rows(i).Cells(0).Text, GridView5.Rows(i).Cells(1).Text, GridView5.Rows(i).Cells(2).Text, GridView5.Rows(i).Cells(3).Text, GridView5.Rows(i).Cells(5).Text, GridView5.Rows(i).Cells(4).Text, GridView5.Rows(i).Cells(6).Text, "UNPREPAIRED MESUREMENT BOOK", TextBox33.Text & " To " & TextBox34.Text, total_value)
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/daily_work.rpt"))
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

    Protected Sub Button62_Click(sender As Object, e As EventArgs) Handles Button62.Click
        Dim from_date, to_date As Date
        from_date = CDate(TextBox35.Text)
        to_date = CDate(TextBox36.Text)
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select * from daily_work join SUPL on daily_work .supl_id =supl.supl_id where daily_work.from_date >='" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and daily_work.to_date <='" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by po_no", conn)
        da.Fill(dt)
        conn.Close()
        GridView6.DataSource = dt
        GridView6.DataBind()
    End Sub

    Protected Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        If GridView6.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(8) {New DataColumn("PO_NO"), New DataColumn("SUPL_NAME"), New DataColumn("W_NAME"), New DataColumn("W_AU"), New DataColumn("W_QTY"), New DataColumn("unit_rate"), New DataColumn("total_amt"), New DataColumn("RPT_FOR"), New DataColumn("RPT_DATE")})
        ''insert datatable value
        Dim i As Integer = 0
        For i = 0 To GridView6.Rows.Count - 1
            dt2.Rows.Add(GridView6.Rows(i).Cells(0).Text, GridView6.Rows(i).Cells(1).Text, GridView6.Rows(i).Cells(2).Text, GridView6.Rows(i).Cells(3).Text, GridView6.Rows(i).Cells(5).Text, GridView6.Rows(i).Cells(4).Text, GridView6.Rows(i).Cells(6).Text, "", TextBox35.Text & " To " & TextBox36.Text)
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/daily_work.rpt"))
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

    Protected Sub Button67_Click(sender As Object, e As EventArgs) Handles Button67.Click
        Dim dt2 As New DataTable
        conn.Open()
        dt2.Clear()
        'da = New SqlDataAdapter("select * from mb_book join supl on mb_book.supl_id=supl.supl_id join WO_ORDER on mb_book.po_no =WO_ORDER .PO_NO and mb_book .wo_slno =WO_ORDER .W_SLNO join ORDER_DETAILS on mb_book .po_no =ORDER_DETAILS .SO_NO where mb_book .po_no ='" & DropDownList11.SelectedValue & "' and mb_book .mb_no ='" & DropDownList12.SelectedValue & "' AND WO_ORDER .WO_AMD ='NA'", conn)
        da = New SqlDataAdapter("select * from mb_book join daily_work on mb_book.mb_no=daily_work.mb_gen_ind join supl on mb_book.supl_id=supl.supl_id join WO_ORDER on mb_book.po_no =WO_ORDER .PO_NO and mb_book .wo_slno =WO_ORDER .W_SLNO join ORDER_DETAILS on mb_book .po_no =ORDER_DETAILS .SO_NO where mb_book .po_no ='" & DropDownList11.SelectedValue & "' and mb_book .mb_no ='" & DropDownList12.SelectedValue & "' AND WO_ORDER .WO_AMD ='NA'", conn)
        da.Fill(dt2)
        conn.Close()
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/mb_book.rpt"))
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


    Protected Sub DropDownList13_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList13.SelectedIndexChanged
        If DropDownList13.SelectedValue = "Select" Then
            DropDownList13.Focus()
            DropDownList11.Items.Clear()
            DropDownList12.Items.Clear()
            Return
        End If
        conn.Open()
        dt.Clear()
        'da = New SqlDataAdapter("select DISTINCT po_no from mb_book where fiscal_year=" & DropDownList13.SelectedValue & "  ORDER BY po_no", conn)
        da = New SqlDataAdapter("select DISTINCT po_no from mb_book where fiscal_year=" & DropDownList13.SelectedValue & " AND mb_no LIKE 'MB%' ORDER BY po_no", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList11.Items.Clear()
        DropDownList11.DataSource = dt
        DropDownList11.DataValueField = "po_no"
        DropDownList11.DataBind()
        DropDownList11.Items.Insert(0, "Select")
        DropDownList11.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList11.SelectedIndexChanged
        If DropDownList11.SelectedValue = "Select" Then
            DropDownList11.Focus()
            DropDownList12.Items.Clear()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select DISTINCT mb_no from mb_book where fiscal_year=" & DropDownList13.SelectedValue & " and po_no='" & DropDownList11.SelectedValue & "'  ORDER BY mb_no", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList12.Items.Clear()
        DropDownList12.DataSource = dt
        DropDownList12.DataValueField = "mb_no"
        DropDownList12.DataBind()
        DropDownList12.Items.Insert(0, "Select")
        DropDownList12.SelectedValue = "Select"
    End Sub
End Class