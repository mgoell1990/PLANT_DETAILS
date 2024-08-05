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
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Web.UI
Public Class rm_report
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
            REPORTTextBox1.Text = Today.Date
            REPORTTextBox2.Text = Today.Date
        End If
    End Sub
    Protected Sub REPORTDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles REPORTDropDownList.SelectedIndexChanged
        If REPORTDropDownList.SelectedValue <> "Material Transaction" Then
            DropDownList20.Visible = False
            Label457.Visible = False
        End If
        If REPORTDropDownList.SelectedValue = "Select" Then
            REPORTDropDownList.Focus()
            Return
        ElseIf REPORTDropDownList.SelectedValue = "Stock" Then
            Label454.Text = "Mat Group"
            Label438.Text = "Mat Code"
            Label439.Visible = False
            Label440.Visible = False
            REPORTTextBox1.Visible = False
            REPORTTextBox2.Visible = False
            dt.Clear()
            conn.Open()
            REPORTDropDownList2.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '1%' order by GROUP_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList2.Items.Clear()
            REPORTDropDownList2.DataSource = dt
            REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
            REPORTDropDownList2.DataBind()
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.Items.Insert(1, "All Material")
            REPORTDropDownList2.SelectedValue = "Select"
            conn.Close()
        ElseIf REPORTDropDownList.SelectedValue = "Consumption" Then
            Label454.Text = "Mat Group"
            Label438.Text = "Mat Code"
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "Date"
            Label440.Text = "And"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
            dt.Clear()
            conn.Open()
            REPORTDropDownList2.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '1%' order by GROUP_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList2.DataSource = dt
            REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
            REPORTDropDownList2.DataBind()
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.SelectedValue = "Select"
            conn.Close()
        ElseIf REPORTDropDownList.SelectedValue = "Material Transaction" Then
            Label454.Text = "Mat Group"
            Label438.Text = "Mat Code"
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "From Date"
            Label440.Text = "To Date"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
            dt.Clear()
            conn.Open()
            REPORTDropDownList2.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '1%' order by GROUP_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList2.DataSource = dt
            REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
            REPORTDropDownList2.DataBind()
            'REPORTDropDownList2.Items.Add("All Group")
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.SelectedValue = "Select"
            conn.Close()
            DropDownList20.Items.Clear()
            DropDownList20.Items.Insert(0, "Select")
            DropDownList20.Items.Insert(1, "Purchase")
            DropDownList20.Items.Insert(2, "Issue")
            DropDownList20.Items.Insert(3, "All")
            DropDownList20.Visible = True
            Label457.Visible = True
            Label457.Text = "Transaction Type"
            DropDownList19.Visible = False
            Label458.Visible = False
            REPORTDropDownList2.Visible = True
            REPORTDropDownList3.Visible = True
            Label454.Visible = True
            Label438.Visible = True
        ElseIf REPORTDropDownList.SelectedValue = "Daily Report" Then
            Label454.Text = "Mat Group"
            Label438.Text = "Mat Code"
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "From Date"
            Label440.Text = "To Date"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
            dt.Clear()
            conn.Open()
            REPORTDropDownList2.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '1%' order by GROUP_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList2.DataSource = dt
            REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
            REPORTDropDownList2.DataBind()
            'REPORTDropDownList2.Items.Add("All Group")
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.SelectedValue = "Select"
            conn.Close()
            DropDownList20.Visible = False
            Label457.Visible = False
            Label457.Text = "Type"
            DropDownList19.Visible = False
            Label454.Visible = True
            Label438.Visible = True
            Label458.Visible = False
            REPORTDropDownList2.Visible = True
            REPORTDropDownList3.Visible = True

        ElseIf REPORTDropDownList.SelectedValue = "CRR" Then

            Label454.Text = "Mat Group"
            Label438.Text = "Mat Code"
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "From Date"
            Label440.Text = "To Date"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
            dt.Clear()
            conn.Open()
            REPORTDropDownList2.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '1%' order by GROUP_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList2.DataSource = dt
            REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
            REPORTDropDownList2.DataBind()
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.SelectedValue = "Select"
            conn.Close()
            DropDownList20.Visible = False
            Label457.Visible = False
            Label457.Text = "Type"
            DropDownList19.Visible = False
            Label454.Visible = True
            Label438.Visible = True
            Label458.Visible = False
            REPORTDropDownList2.Visible = True
            REPORTDropDownList3.Visible = True

        ElseIf REPORTDropDownList.SelectedValue = "Issue" Then
            REPORTDropDownList2.Items.Clear()
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.Items.Insert(1, "To Deptt")
            ''REPORTDropDownList2.Items.Add("To Contractor")
            REPORTDropDownList2.Items.Insert(2, "I.P.T. Issue")
            REPORTDropDownList2.Items.Insert(3, "Individual")
            REPORTDropDownList2.Items.Insert(4, "All Issue")
            Label454.Text = "Issue Type"
            Label438.Text = "Issue No"
            Label439.Text = "Date"
            Label440.Text = "And"
            Label439.Visible = True
            Label440.Visible = True
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
        ElseIf REPORTDropDownList.SelectedValue = "Transporter Wise" Then
            REPORTDropDownList3.Visible = False
            dt.Clear()
            conn.Open()
            REPORTDropDownList2.Items.Clear()
            da = New SqlDataAdapter("select distinct (SUPL_ID +' , '+ SUPL_NAME +' , '+ SO_ACTUAL +' , '+ SO_NO) As supl_details from ORDER_DETAILS join SUPL on ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID where PO_TYPE like '%frei%' order by supl_details", conn)
            da.Fill(dt)
            REPORTDropDownList2.DataSource = dt
            REPORTDropDownList2.DataValueField = "supl_details"
            REPORTDropDownList2.DataBind()
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.SelectedValue = "Select"
            conn.Close()
            Label454.Text = "Transportor"
            Label439.Text = "Date"
            Label440.Text = "And"
            Label438.Visible = False
            REPORTDropDownList3.Visible = False
            Label439.Visible = True
            Label440.Visible = True
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
        End If

    End Sub

    Protected Sub REPORTDropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles REPORTDropDownList2.SelectedIndexChanged
        If REPORTDropDownList2.SelectedValue = "Select" Then
            REPORTDropDownList2.Focus()
            Return
        ElseIf REPORTDropDownList2.SelectedValue = "All Material" And REPORTDropDownList.SelectedValue = "Stock" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL where MAT_CODE like '100%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And REPORTDropDownList.SelectedValue = "Stock" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue = "All Issue" And REPORTDropDownList.SelectedValue = "Issue" Then
            conn.Open()
            da = New SqlDataAdapter("select distinct issue_no from  mat_details where issue_no like'ISSUE%' ORDER BY issue_no", conn)
            da.Fill(ds, "mat_details")
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = ds.Tables("mat_details")
            REPORTDropDownList3.DataValueField = "issue_no"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue = "I.P.T. Issue" And REPORTDropDownList.SelectedValue = "Issue" Then
            conn.Open()
            da = New SqlDataAdapter("select distinct issue_no from  mat_details where ISSUE_TYPE='I.P.T.' or ISSUE_TYPE='Other' ORDER BY issue_no", conn)
            da.Fill(ds, "mat_details")
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = ds.Tables("mat_details")
            REPORTDropDownList3.DataValueField = "issue_no"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.SelectedValue = "Select"

        ElseIf REPORTDropDownList2.SelectedValue = "Individual" And REPORTDropDownList.SelectedValue = "Issue" Then
            conn.Open()
            da = New SqlDataAdapter("select distinct issue_no from  mat_details where issue_no like'RI%' ORDER BY issue_no", conn)
            da.Fill(ds, "mat_details")
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = ds.Tables("mat_details")
            REPORTDropDownList3.DataValueField = "issue_no"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.SelectedValue = "Select"
            Label439.Visible = False
            Label440.Visible = False
            REPORTTextBox1.Visible = False
            REPORTTextBox2.Visible = False



        ElseIf REPORTDropDownList2.SelectedValue = "To Contractor" And REPORTDropDownList.SelectedValue = "Issue" Then
            conn.Open()
            da = New SqlDataAdapter("select distinct SUPL.SUPL_NAME from supl join ORDER_DETAILS on SUPL.SUPL_ID =ORDER_DETAILS .PARTY_CODE join MAT_DETAILS on ORDER_DETAILS .SO_NO =MAT_DETAILS .COST_CODE where MAT_DETAILS .ISSUE_TYPE ='To Contractor'", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "SUPL_NAME"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue = "To Deptt" And REPORTDropDownList.SelectedValue = "Issue" Then
            conn.Open()
            da = New SqlDataAdapter("select distinct cost.cost_centre from cost join MAT_DETAILS on cost.cost_code =MAT_DETAILS .COST_CODE where MAT_DETAILS .ISSUE_TYPE ='To Deptt'", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "cost_centre"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList.SelectedValue = "Material Transaction" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All Material")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList.SelectedValue = "Material Transaction" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All Material")
            REPORTDropDownList3.SelectedValue = "Select"

        ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList.SelectedValue = "Daily Report" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All Material")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList.SelectedValue = "Daily Report" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All Material")
            REPORTDropDownList3.SelectedValue = "Select"


        ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList.SelectedValue = "CRR" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All Material")
            REPORTDropDownList3.SelectedValue = "Select"
        ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList.SelectedValue = "CRR" Then
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All Material")
            REPORTDropDownList3.SelectedValue = "Select"

        End If
    End Sub

    Protected Sub REPORTDropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles REPORTDropDownList3.SelectedIndexChanged

    End Sub

    Protected Sub REPORTButton45_Click(sender As Object, e As EventArgs) Handles REPORTButton45.Click
        Dim working_date As Date
        working_date = Today.Date
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
        Dim crystalReport As New ReportDocument
        Dim quary As String = ""
        Dim from_date As Date = Date.ParseExact(CDate(REPORTTextBox1.Text), "dd-MM-yyyy", provider)
        Dim to_date As Date = Date.ParseExact(CDate(REPORTTextBox2.Text), "dd-MM-yyyy", provider)
        If REPORTDropDownList.SelectedValue = "Stock" Then
            If REPORTDropDownList2.SelectedValue = "All Material" And REPORTDropDownList3.SelectedValue <> "All" Then
                quary = "SELECT (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS LINE_NO, MATERIAL.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MATERIAL.MAT_DRAW,MATERIAL.MAT_AVG,MATERIAL.MAT_STOCK,MATERIAL.MAT_LAST_RATE,MATERIAL.MAT_LASTPUR_DATE,MATERIAL.OPEN_STOCK,MATERIAL.OPEN_AVG_PRICE,MATERIAL.RE_ORDER_LABEL,MATERIAL.ORDER_STOP_IND,MATERIAL.LAST_ISSUE_DATE,MATERIAL.LAST_TRANS_DATE,(SELECT SUM(ISSUE_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_COMP,(SELECT SUM(MAT_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_PUR  FROM MATERIAL  WHERE MATERIAL .MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/stockreport.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue = "All Material" And REPORTDropDownList3.SelectedValue = "All" Then
                quary = "SELECT * FROM MATERIAL WHERE MAT_STOCK > 0 and MAT_CODE like '100%'"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/stock_total.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And REPORTDropDownList3.SelectedValue = "All" Then
                quary = "SELECT * FROM MATERIAL where mat_code like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' AND MAT_STOCK > 0"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/stock_total.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And REPORTDropDownList3.SelectedValue <> "All" Then

                quary = "SELECT (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS LINE_NO, MATERIAL.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MATERIAL.MAT_DRAW,MATERIAL.MAT_AVG,MATERIAL.MAT_STOCK,MATERIAL.MAT_LAST_RATE,MATERIAL.MAT_LASTPUR_DATE,MATERIAL.OPEN_STOCK,MATERIAL.OPEN_AVG_PRICE,MATERIAL.RE_ORDER_LABEL,MATERIAL.ORDER_STOP_IND,MATERIAL.LAST_ISSUE_DATE,MATERIAL.LAST_TRANS_DATE,(SELECT SUM(ISSUE_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_COMP,(SELECT SUM(MAT_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_PUR  FROM MATERIAL  WHERE MATERIAL .MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/stockreport.rpt"))
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

            End If
        ElseIf REPORTDropDownList.SelectedValue = "Issue" Then
            If REPORTDropDownList2.SelectedValue = "All Issue" And REPORTDropDownList3.SelectedValue <> "All" Then
                conn.Open()
                Dim mc As New SqlCommand
                Dim issue_type As String = ""
                mc.CommandText = "select issue_type from MAT_DETAILS where ISSUE_NO='" & REPORTDropDownList3.SelectedValue & "'"
                mc.Connection = conn
                dr = mc.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    issue_type = dr.Item("issue_type")
                    dr.Close()
                End If
                conn.Close()
                Dim PO_QUARY As String = ""
                If issue_type = "To Contractor" Then
                    PO_QUARY = "select material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select SUPL.SUPL_NAME from ORDER_DETAILS join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID where ORDER_DETAILS .SO_NO =(select cost_code from MAT_DETAILS where ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "')) as COST_CENTRE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "'"
                ElseIf issue_type = "To Deptt" Then
                    PO_QUARY = "select material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select cost.cost_centre  from MAT_DETAILS  join cost on MAT_DETAILS.COST_CODE   =cost.cost_code  where MAT_DETAILS .ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "') as cost_centre from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "'"
                End If
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(PO_QUARY, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/issue.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue = "Individual" And REPORTDropDownList3.SelectedValue <> "Select" Then
                ''all
                quary = "select material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select cost.cost_centre  from MAT_DETAILS  join cost on MAT_DETAILS.COST_CODE   =cost.cost_code  where MAT_DETAILS .ISSUE_NO ='" & REPORTDropDownList3.Text & "') as cost_centre from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & REPORTDropDownList3.Text & "'"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/issue_ind.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue = "To Contractor" And REPORTDropDownList3.SelectedValue <> "Select" Then
                ''all
                quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join ORDER_DETAILS  on MAT_DETAILS .COST_CODE =ORDER_DETAILS .SO_NO join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID   where MAT_DETAILS .ISSUE_TYPE ='To Contractor' and supl.SUPL_NAME ='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue = "To Deptt" And REPORTDropDownList3.SelectedValue <> "Select" Then
                ''all
                quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(cost.cost_centre ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join cost on MAT_DETAILS .COST_CODE =cost.cost_code  where MAT_DETAILS .ISSUE_TYPE ='To Deptt' and cost .cost_centre ='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue = "I.P.T. Issue" And REPORTDropDownList3.SelectedValue <> "Select" Then
                ''all
                '' quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join PO_ORDER  on MAT_DETAILS .COST_CODE =PO_ORDER .PO_NO join SUPL on PO_ORDER .SUPPLIER_CODE =SUPL.SUPL_ID   where MAT_DETAILS .ISSUE_TYPE ='To Contractor' and supl.SUPL_NAME ='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
                quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type FROM MAT_DETAILS JOIN SUPL ON MAT_DETAILS .COST_CODE =SUPL.SUPL_ID JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE  WHERE ISSUE_NO='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
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

            ElseIf REPORTDropDownList2.SelectedValue = "All Issue" And REPORTDropDownList3.SelectedValue = "All" Then
                quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,('All Issue Report' ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE where LINE_TYPE ='i' and  LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and ISSUE_NO like 'RI%' order by ISSUE_NO"
                conn.Open()
                Dim dt As New DataTable
                da = New SqlDataAdapter(quary, conn)
                da.Fill(dt)
                conn.Close()
                crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
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

            End If
        ElseIf REPORTDropDownList.SelectedValue = "Material Transaction" Then

            Dim quary_trans As String = "select MAT_DETAILS .UNIT_PRICE,MAT_DETAILS .TOTAL_PRICE,MAT_DETAILS .LINE_NO,MAT_DETAILS .cost_code ,MAT_DETAILS .LINE_DATE ,MAT_DETAILS .LINE_TYPE ,MAT_DETAILS .ISSUE_NO ,(MAT_DETAILS .MAT_QTY +MAT_DETAILS .ISSUE_QTY ) as mat_qty,mat_details.MAT_BALANCE,mat_details.mat_code,material.mat_name,material.MAT_AU,('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "' ) AS REPORT_FROM from mat_details join material on mat_details.mat_code=material.mat_code where LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
            If REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
                quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
            ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue = "All Material" Then
                quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%'"
            ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
                quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
            End If
            If DropDownList20.SelectedValue = "Purchase" Then
                quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='R'"
            ElseIf DropDownList20.SelectedValue = "Issue" Then
                quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I'"
            End If
            quary_trans = quary_trans & " order by LINE_NO"
            conn.Open()
            Dim dt As New DataTable
            da = New SqlDataAdapter(quary_trans, conn)
            da.Fill(dt)
            conn.Close()
            crystalReport.Load(Server.MapPath("~/print_rpt/trans_report.rpt"))
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

        ElseIf REPORTDropDownList.SelectedValue = "Daily Report" Then
            Dim f_date, t_date, month_date As Date
            f_date = CDate(REPORTTextBox1.Text)
            t_date = CDate(REPORTTextBox2.Text)
            month_date = f_date.Year & "-" & f_date.Month & "- 01"
            Dim dt1 As New DataTable()
            If REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select mat_code,mat_name from material where mat_code ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' order by mat_code", conn)
                da.Fill(dt1)
                conn.Close()
            ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue = "All Material" Then
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select mat_code,mat_name from material where mat_code like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by mat_code", conn)
                da.Fill(dt1)
                conn.Close()
            ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select mat_code,mat_name from material where mat_code ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' order by mat_code", conn)
                da.Fill(dt1)
                conn.Close()
            End If
            Dim dt10 As New DataTable()
            dt10.Columns.AddRange(New DataColumn(11) {New DataColumn("mat_code"), New DataColumn("mat_name"), New DataColumn("ser_date"), New DataColumn("month_ob"), New DataColumn("month_rcpt"), New DataColumn("month_issue"), New DataColumn("ob"), New DataColumn("receipt"), New DataColumn("issue"), New DataColumn("closing"), New DataColumn("stock"), New DataColumn("po_bal")})
            For Me.count = 0 To dt1.Rows.Count - 1
                Dim quary_trans, ser_date As New String("")
                Dim month_ob, month_rcpt, month_issue, ob, receipt, issue, closing, stock, po_bal As New Decimal(0.0)
                quary_trans = "select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date , " &
                    " (select (case when MAX(MAT_BALANCE) IS null then (select OPEN_STOCK  from MATERIAL where MAT_CODE ='" & dt1.Rows(count)(0) & "' ) else MAX(mat_balance) end) from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_NO =(select max(LINE_NO)  from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_DATE < '" & month_date.Year & "-" & month_date.Month & "-" & month_date.Day & "'))  as month_ob, " &
                    " (select (case when sum(MAT_QTY) IS null then '0.00' else sum(MAT_QTY) end) from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_DATE between '" & month_date.Year & "-" & month_date.Month & "-" & month_date.Day & "' and '" & f_date.Year & "-" & f_date.Month & "-" & f_date.Day & "') as month_rcpt," &
                    " (select (case when sum(ISSUE_QTY ) IS null then '0.00' else sum(ISSUE_QTY ) end) from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_DATE between '" & month_date.Year & "-" & month_date.Month & "-" & month_date.Day & "' and '" & f_date.Year & "-" & f_date.Month & "-" & f_date.Day & "') as month_issue," &
                    " (select (case when MAX(MAT_BALANCE) IS null then (select OPEN_STOCK  from MATERIAL where MAT_CODE ='" & dt1.Rows(count)(0) & "' ) else MAX(mat_balance) end) from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_NO =(select max(LINE_NO)  from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_DATE < '" & f_date.Year & "-" & f_date.Month & "-" & f_date.Day & "'))  as ob," &
                    " (select (case when sum(MAT_QTY) IS null then '0.00' else sum(MAT_QTY) end) from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_DATE between '" & f_date.Year & "-" & f_date.Month & "-" & f_date.Day & "' and '" & t_date.Year & "-" & t_date.Month & "-" & t_date.Day & "') as receipt," &
                    " (select (case when sum(ISSUE_QTY ) IS null then '0.00' else sum(ISSUE_QTY ) end) from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_DATE between '" & f_date.Year & "-" & f_date.Month & "-" & f_date.Day & "' and '" & t_date.Year & "-" & t_date.Month & "-" & t_date.Day & "') as issue," &
                    " (select (case when MAX(MAT_BALANCE) IS null then (select OPEN_STOCK  from MATERIAL where MAT_CODE ='" & dt1.Rows(count)(0) & "' ) else MAX(mat_balance) end) from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_NO =(select max(LINE_NO)  from MAT_DETAILS where MAT_CODE ='" & dt1.Rows(count)(0) & "' and LINE_DATE <= '" & t_date.Year & "-" & t_date.Month & "-" & t_date.Day & "')) as closing," &
                    " (select ( case when  MAT_STOCK IS null then 0.00 else MAT_STOCK end) from material where MAT_CODE ='" & dt1.Rows(count)(0) & "' ) as stock," &
                    " (select (case when  sum(MAT_QTY) IS null  then 0.00 else sum(MAT_QTY)-sum(MAT_QTY_RCVD) end)  from PO_ORD_MAT where MAT_CODE ='" & dt1.Rows(count)(0) & "' and MAT_STATUS ='PENDING') AS po_bal"
                Dim mc As New SqlCommand
                conn.Open()
                mc.CommandText = quary_trans
                mc.Connection = conn
                dr = mc.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ser_date = dr.Item("ser_date")
                    month_ob = dr.Item("month_ob")
                    month_rcpt = dr.Item("month_rcpt")
                    month_issue = dr.Item("month_issue")
                    ob = dr.Item("ob")
                    receipt = dr.Item("receipt")
                    issue = dr.Item("issue")
                    closing = dr.Item("closing")
                    stock = dr.Item("stock")
                    po_bal = dr.Item("po_bal")
                    dr.Close()
                Else
                    conn.Close()
                End If
                conn.Close()
                dt10.Rows.Add(dt1.Rows(count)(0), dt1.Rows(count)(1), ser_date, month_ob, month_rcpt, month_issue, ob, receipt, issue, closing, stock, po_bal)

            Next
            crystalReport.Load(Server.MapPath("~/print_rpt/rm_daily_rpt.rpt"))
            crystalReport.SetDataSource(dt10)
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

        ElseIf REPORTDropDownList.SelectedValue = "CRR" Then
            Dim s_date, e_date As Date
            s_date = CDate(REPORTTextBox1.Text)
            e_date = CDate(REPORTTextBox2.Text)
            Dim dt1 As New DataTable()
            If REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE, PO_RCD_MAT .TRANS_SHORT from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'rcrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' and po_rcd_mat.mat_code ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' order by PO_RCD_MAT .CRR_NO ", conn)
                da.Fill(dt1)
                conn.Close()
            ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue = "All Material" Then
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE, PO_RCD_MAT .TRANS_SHORT  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'rcrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' and po_rcd_mat.mat_code like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by PO_RCD_MAT .CRR_NO ", conn)
                da.Fill(dt1)
                conn.Close()
            ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
                conn.Open()
                dt1.Clear()
                da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE, PO_RCD_MAT .TRANS_SHORT  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'rcrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' and po_rcd_mat.mat_code ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' order by PO_RCD_MAT .CRR_NO ", conn)
                da.Fill(dt1)
                conn.Close()
            End If
            crystalReport.Load(Server.MapPath("~/print_rpt/CRR_REPORT.rpt"))
            crystalReport.SetDataSource(dt1)
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

        ElseIf REPORTDropDownList.SelectedValue = "Transporter Wise" Then
            Dim s_date, e_date As Date
            s_date = CDate(REPORTTextBox1.Text)
            e_date = CDate(REPORTTextBox2.Text)
            Dim partyDetailsArray() As String
            partyDetailsArray = (REPORTDropDownList2.SelectedValue).Split(",")
            'partyDetailsArray(0).Trim
            Dim dt1 As New DataTable()

            conn.Open()
            dt1.Clear()
            da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE, PO_RCD_MAT .TRANS_SHORT  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'rcrr%' and TRANS_WO_NO='" & partyDetailsArray(3).Trim & "' and CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' AND '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' order by PO_RCD_MAT .CRR_NO", conn)
            da.Fill(dt1)
            conn.Close()

            crystalReport.Load(Server.MapPath("~/print_rpt/CRR_REPORT.rpt"))
            crystalReport.SetDataSource(dt1)
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

        End If
    End Sub
End Class