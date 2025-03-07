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
Imports ClosedXML.Excel

Public Class s_report
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
        'MultiView1.ActiveViewIndex = 0
    End Sub

    'Protected Sub REPORTDropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles REPORTDropDownList.SelectedIndexChanged
    '    If REPORTDropDownList.SelectedValue <> "Mat Trans" Then
    '        DropDownList20.Visible = False
    '        Label457.Visible = False
    '    End If
    '    If REPORTDropDownList.SelectedValue = "Select" Then
    '        REPORTDropDownList.Focus()
    '        Return
    '    ElseIf REPORTDropDownList.SelectedValue = "Stock" Then
    '        Label454.Text = "Mat Group"
    '        Label438.Text = "Mat Code"
    '        Label454.Visible = True
    '        Label438.Visible = True
    '        Label439.Visible = False
    '        Label440.Visible = False
    '        REPORTTextBox1.Visible = False
    '        REPORTTextBox2.Visible = False
    '        REPORTDropDownList2.Visible = True
    '        dt.Clear()
    '        conn.Open()
    '        REPORTDropDownList2.Items.Clear()
    '        da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
    '        da.Fill(dt)
    '        REPORTDropDownList2.Items.Clear()
    '        REPORTDropDownList2.DataSource = dt
    '        REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
    '        REPORTDropDownList2.DataBind()
    '        REPORTDropDownList2.Items.Add("Select")
    '        REPORTDropDownList2.Items.Add("All Material")
    '        REPORTDropDownList2.SelectedValue = "Select"
    '        conn.Close()
    '    ElseIf REPORTDropDownList.SelectedValue = "Total Issue" Then
    '        Label454.Text = "Mat Group"
    '        Label438.Text = "Mat Code"
    '        Label454.Visible = True
    '        Label438.Visible = True
    '        Label439.Visible = True
    '        Label440.Visible = True
    '        Label439.Text = "Date"
    '        Label440.Text = "And"
    '        REPORTTextBox1.Visible = True
    '        REPORTTextBox2.Visible = True
    '        REPORTDropDownList2.Visible = True
    '        dt.Clear()
    '        conn.Open()
    '        REPORTDropDownList2.Items.Clear()
    '        da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
    '        da.Fill(dt)
    '        REPORTDropDownList2.DataSource = dt
    '        REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
    '        REPORTDropDownList2.DataBind()
    '        REPORTDropDownList2.Items.Add("Select")
    '        REPORTDropDownList2.Items.Add("All Group")
    '        REPORTDropDownList2.SelectedValue = "Select"
    '        conn.Close()
    '    ElseIf REPORTDropDownList.SelectedValue = "Mat Trans" Then
    '        Label454.Text = "Mat Group"
    '        Label438.Text = "Mat Code"
    '        Label454.Visible = True
    '        Label438.Visible = True
    '        Label439.Visible = True
    '        Label440.Visible = True
    '        REPORTDropDownList2.Visible = True
    '        Label439.Text = "Date"
    '        Label440.Text = "And"
    '        REPORTTextBox1.Visible = True
    '        REPORTTextBox2.Visible = True
    '        dt.Clear()
    '        conn.Open()
    '        REPORTDropDownList2.Items.Clear()
    '        da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
    '        da.Fill(dt)
    '        REPORTDropDownList2.DataSource = dt
    '        REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
    '        REPORTDropDownList2.DataBind()
    '        REPORTDropDownList2.Items.Add("All Group")
    '        REPORTDropDownList2.Items.Add("Select")
    '        REPORTDropDownList2.SelectedValue = "Select"
    '        conn.Close()
    '        DropDownList20.Items.Clear()
    '        DropDownList20.Items.Add("Select")
    '        DropDownList20.Items.Add("Purchase")
    '        DropDownList20.Items.Add("Issue")
    '        DropDownList20.Items.Add("All")
    '        DropDownList20.Visible = True
    '        Label457.Visible = True
    '        Label457.Text = "Type"
    '        DropDownList19.Visible = False
    '        Label458.Visible = False
    '    ElseIf REPORTDropDownList.SelectedValue = "CRR" Then
    '        Label454.Text = "Mat Group"
    '        Label438.Text = "Mat Code"
    '        Label439.Visible = True
    '        Label440.Visible = True
    '        Label439.Text = "Date"
    '        Label440.Text = "And"
    '        REPORTTextBox1.Visible = True
    '        REPORTTextBox2.Visible = True
    '        dt.Clear()
    '        conn.Open()
    '        REPORTDropDownList2.Items.Clear()
    '        da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
    '        da.Fill(dt)
    '        REPORTDropDownList2.DataSource = dt
    '        REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
    '        REPORTDropDownList2.DataBind()
    '        REPORTDropDownList2.Items.Add("All Group")
    '        REPORTDropDownList2.Items.Add("Select")
    '        REPORTDropDownList2.SelectedValue = "Select"
    '        conn.Close()
    '        DropDownList20.Visible = False
    '        Label457.Visible = False
    '        Label457.Text = "Type"
    '        DropDownList19.Visible = False
    '        Label454.Visible = True
    '        Label438.Visible = True
    '        Label458.Visible = False
    '        REPORTDropDownList2.Visible = True
    '        REPORTDropDownList3.Visible = True

    '    ElseIf REPORTDropDownList.SelectedValue = "Issue" Then
    '        REPORTDropDownList2.Visible = True
    '        REPORTDropDownList2.Items.Clear()
    '        REPORTDropDownList2.Items.Add("Select")
    '        REPORTDropDownList2.Items.Add("To Deptt")
    '        REPORTDropDownList2.Items.Add("To Contractor")
    '        REPORTDropDownList2.Items.Add("I.P.T. Issue")
    '        REPORTDropDownList2.Items.Add("Individual")
    '        REPORTDropDownList2.Items.Add("All Issue")
    '        Label454.Text = "Issue Type"
    '        Label438.Text = "Issue No"
    '        Label439.Text = "Date"
    '        Label440.Text = "And"
    '        Label439.Visible = True
    '        Label440.Visible = True
    '        Label454.Visible = True
    '        Label438.Visible = True
    '        REPORTTextBox1.Visible = True
    '        REPORTTextBox2.Visible = True

    '    ElseIf REPORTDropDownList.SelectedValue = "Non-Moving Item" Then

    '        REPORTDropDownList2.Visible = False
    '        REPORTDropDownList3.Visible = False
    '        REPORTTextBox1.Visible = True
    '        REPORTTextBox2.Visible = False
    '        DropDownList19.Visible = False
    '        DropDownList20.Visible = False
    '        Label454.Visible = False
    '        Label438.Visible = False
    '        Label440.Visible = False
    '        Label439.Visible = True
    '        Label439.Text = "Date"
    '        REPORTTextBox1.Text = ""

    '    End If
    'End Sub

    Protected Sub REPORTDropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles REPORTDropDownList2.SelectedIndexChanged
        If REPORTDropDownList2.SelectedValue = "Select" Then
            REPORTDropDownList2.Focus()
            Return
        ElseIf REPORTDropDownList2.SelectedValue = "All Material" And DropDownList9.SelectedValue = "Stock Position" Then
            REPORTDropDownList3.Visible = True
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL where MAT_CODE not like '100%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()

            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All")
            REPORTDropDownList3.SelectedValue = "Select"
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "From"
            Label440.Text = "To"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
        ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And DropDownList9.SelectedValue = "Stock Position" Then
            REPORTDropDownList3.Visible = True
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
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "From"
            Label440.Text = "To"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
            'ElseIf REPORTDropDownList2.SelectedValue = "All Issue" And REPORTDropDownList.SelectedValue = "Issue" Then
            '    REPORTDropDownList3.Visible = True
            '    conn.Open()
            '    da = New SqlDataAdapter("select distinct issue_no from  mat_details where issue_no like'SI%' ORDER BY issue_no", conn)
            '    da.Fill(ds, "mat_details")
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = ds.Tables("mat_details")
            '    REPORTDropDownList3.DataValueField = "issue_no"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("All")
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            '    Label439.Visible = True
            '    Label440.Visible = True
            '    Label439.Text = "Date"
            '    Label440.Text = "And"
            '    REPORTTextBox1.Visible = True
            '    REPORTTextBox2.Visible = True
            'ElseIf REPORTDropDownList2.SelectedValue = "Individual" And REPORTDropDownList.SelectedValue = "Issue" Then
            '    REPORTDropDownList3.Visible = True
            '    conn.Open()
            '    da = New SqlDataAdapter("select distinct issue_no from  mat_details where issue_no like'SI%' ORDER BY issue_no", conn)
            '    da.Fill(ds, "mat_details")
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = ds.Tables("mat_details")
            '    REPORTDropDownList3.DataValueField = "issue_no"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            '    Label439.Visible = False
            '    Label440.Visible = False
            '    REPORTTextBox1.Visible = False
            '    REPORTTextBox2.Visible = False


            'ElseIf REPORTDropDownList2.SelectedValue = "I.P.T. Issue" And REPORTDropDownList.SelectedValue = "Issue" Then
            '    REPORTDropDownList3.Visible = True
            '    conn.Open()
            '    da = New SqlDataAdapter("select distinct issue_no from  mat_details where ISSUE_TYPE='I.P.T.' or ISSUE_TYPE='Other' ORDER BY issue_no", conn)
            '    da.Fill(ds, "mat_details")
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = ds.Tables("mat_details")
            '    REPORTDropDownList3.DataValueField = "issue_no"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            '    Label439.Visible = True
            '    Label440.Visible = True
            '    Label439.Text = "Date"
            '    Label440.Text = "And"
            '    REPORTTextBox1.Visible = True
            '    REPORTTextBox2.Visible = True
            'ElseIf REPORTDropDownList2.SelectedValue = "To Contractor" And REPORTDropDownList.SelectedValue = "Issue" Then
            '    REPORTDropDownList3.Visible = True
            '    conn.Open()
            '    da = New SqlDataAdapter("select distinct SUPL.SUPL_NAME from supl join ORDER_DETAILS on SUPL.SUPL_ID =ORDER_DETAILS .PARTY_CODE join MAT_DETAILS on ORDER_DETAILS .SO_NO =MAT_DETAILS .COST_CODE where MAT_DETAILS .ISSUE_TYPE ='To Contractor'", conn)
            '    da.Fill(dt)
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = dt
            '    REPORTDropDownList3.DataValueField = "SUPL_NAME"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            '    Label439.Visible = True
            '    Label440.Visible = True
            '    Label439.Text = "Date"
            '    Label440.Text = "And"
            '    REPORTTextBox1.Visible = True
            '    REPORTTextBox2.Visible = True
            'ElseIf REPORTDropDownList2.SelectedValue = "To Deptt" And REPORTDropDownList.SelectedValue = "Issue" Then
            '    REPORTDropDownList3.Visible = True
            '    conn.Open()
            '    da = New SqlDataAdapter("select distinct cost.cost_centre from cost join MAT_DETAILS on cost.cost_code =MAT_DETAILS .COST_CODE where MAT_DETAILS .ISSUE_TYPE ='To Deptt'", conn)
            '    da.Fill(dt)
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = dt
            '    REPORTDropDownList3.DataValueField = "cost_centre"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            '    Label439.Visible = True
            '    Label440.Visible = True
            '    Label439.Text = "Date"
            '    Label440.Text = "And"
            '    REPORTTextBox1.Visible = True
            '    REPORTTextBox2.Visible = True
            'ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList.SelectedValue = "Mat Trans" Then
            '    REPORTDropDownList3.Visible = True
            '    dt.Clear()
            '    conn.Open()
            '    da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL order by MAT_CODE", conn)
            '    da.Fill(dt)
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = dt
            '    REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("All Material")
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            '    Label439.Visible = True
            '    Label440.Visible = True
            '    Label439.Text = "Date"
            '    Label440.Text = "And"
            '    REPORTTextBox1.Visible = True
            '    REPORTTextBox2.Visible = True
            'ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList.SelectedValue = "Mat Trans" Then
            '    REPORTDropDownList3.Visible = True
            '    dt.Clear()
            '    conn.Open()
            '    da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            '    da.Fill(dt)
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = dt
            '    REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("All Material")
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            '    Label439.Visible = True
            '    Label440.Visible = True
            '    Label439.Text = "Date"
            '    Label440.Text = "And"
            '    REPORTTextBox1.Visible = True
            '    REPORTTextBox2.Visible = True

            'ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList.SelectedValue = "CRR" Then
            '    REPORTDropDownList3.Visible = True
            '    dt.Clear()
            '    conn.Open()
            '    da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            '    da.Fill(dt)
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = dt
            '    REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("All Material")
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"
            'ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList.SelectedValue = "CRR" Then
            '    REPORTDropDownList3.Visible = True
            '    dt.Clear()
            '    conn.Open()
            '    da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL order by MAT_CODE", conn)
            '    da.Fill(dt)
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = dt
            '    REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("All Material")
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.SelectedValue = "Select"


            'ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList.SelectedValue = "Total Issue" Then
            '    REPORTDropDownList3.Visible = True
            '    Label438.Visible = True
            '    REPORTDropDownList3.Visible = True

            '    dt.Clear()
            '    conn.Open()
            '    da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            '    da.Fill(dt)
            '    REPORTDropDownList3.Items.Clear()
            '    REPORTDropDownList3.DataSource = dt
            '    REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            '    REPORTDropDownList3.DataBind()
            '    conn.Close()
            '    REPORTDropDownList3.Items.Add("Select")
            '    REPORTDropDownList3.Items.Add("All Material")
            '    REPORTDropDownList3.SelectedValue = "Select"

            'ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList.SelectedValue = "Total Issue" Then

            '    Label438.Visible = False
            '    REPORTDropDownList3.Visible = False

            'dt.Clear()
            'conn.Open()
            'da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL where MAT_CODE not like '100%' order by MAT_CODE", conn)
            'da.Fill(dt)
            'REPORTDropDownList3.Items.Clear()
            'REPORTDropDownList3.DataSource = dt
            'REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            'REPORTDropDownList3.DataBind()
            'conn.Close()
            'REPORTDropDownList3.Items.Add("Select")
            'REPORTDropDownList3.SelectedValue = "Select"

        End If
    End Sub


    'Protected Sub REPORTButton45_Click(sender As Object, e As EventArgs) Handles REPORTButton45.Click
    '    Dim working_date As Date
    '    working_date = Today.Date
    '    Dim STR1 As String = ""
    '    If working_date.Month > 3 Then
    '        STR1 = working_date.Year
    '        STR1 = STR1.Trim.Substring(2)
    '        STR1 = STR1 & (STR1 + 1)
    '    ElseIf working_date.Month <= 3 Then
    '        STR1 = working_date.Year
    '        STR1 = STR1.Trim.Substring(2)
    '        STR1 = (STR1 - 1) & STR1
    '    End If
    '    Dim crystalReport As New ReportDocument
    '    Dim quary As String = ""
    '    Dim from_date As Date = Date.ParseExact(CDate(REPORTTextBox1.Text), "dd-MM-yyyy", provider)
    '    Dim to_date As Date = Date.ParseExact(CDate(REPORTTextBox2.Text), "dd-MM-yyyy", provider)
    '    If REPORTDropDownList.SelectedValue = "Stock" Then
    '        If REPORTDropDownList2.SelectedValue = "All Material" And REPORTDropDownList3.SelectedValue <> "All" Then
    '            quary = "SELECT (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS LINE_NO, MATERIAL.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MATERIAL.MAT_DRAW,MATERIAL.MAT_AVG,MATERIAL.MAT_STOCK,MATERIAL.MAT_LAST_RATE,MATERIAL.MAT_LASTPUR_DATE,MATERIAL.OPEN_STOCK,MATERIAL.OPEN_AVG_PRICE,MATERIAL.RE_ORDER_LABEL,MATERIAL.ORDER_STOP_IND,MATERIAL.LAST_ISSUE_DATE,MATERIAL.LAST_TRANS_DATE,(SELECT SUM(ISSUE_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_COMP,(SELECT SUM(MAT_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_PUR  FROM MATERIAL  WHERE MATERIAL .MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/stockreport.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()
    '        ElseIf REPORTDropDownList2.SelectedValue = "All Material" And REPORTDropDownList3.SelectedValue = "All" Then
    '            quary = "SELECT * FROM MATERIAL WHERE MAT_STOCK > 0"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/stock_total.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()
    '        ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And REPORTDropDownList3.SelectedValue = "All" Then
    '            quary = "SELECT * FROM MATERIAL where mat_code like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' AND MAT_STOCK > 0"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/stock_total.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()
    '        ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And REPORTDropDownList3.SelectedValue <> "All" Then

    '            quary = "SELECT (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS LINE_NO, MATERIAL.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MATERIAL.MAT_DRAW,MATERIAL.MAT_AVG,MATERIAL.MAT_STOCK,MATERIAL.MAT_LAST_RATE,MATERIAL.MAT_LASTPUR_DATE,MATERIAL.OPEN_STOCK,MATERIAL.OPEN_AVG_PRICE,MATERIAL.RE_ORDER_LABEL,MATERIAL.ORDER_STOP_IND,MATERIAL.LAST_ISSUE_DATE,MATERIAL.LAST_TRANS_DATE,(SELECT SUM(ISSUE_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_COMP,(SELECT SUM(MAT_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_PUR  FROM MATERIAL  WHERE MATERIAL .MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/stockreport.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()
    '        End If
    '    ElseIf REPORTDropDownList.SelectedValue = "Issue" Then
    '        If REPORTDropDownList2.SelectedValue = "All Issue" And REPORTDropDownList3.SelectedValue <> "All" Then
    '            conn.Open()
    '            Dim mc As New SqlCommand
    '            Dim issue_type As String = ""
    '            mc.CommandText = "select issue_type from MAT_DETAILS where ISSUE_NO='" & REPORTDropDownList3.SelectedValue & "'"
    '            mc.Connection = conn
    '            dr = mc.ExecuteReader
    '            If dr.HasRows Then
    '                dr.Read()
    '                issue_type = dr.Item("issue_type")
    '                dr.Close()
    '            End If
    '            conn.Close()
    '            Dim PO_QUARY As String = ""
    '            If issue_type = "To Contractor" Then
    '                PO_QUARY = "select material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select SUPL.SUPL_NAME from ORDER_DETAILS join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID where ORDER_DETAILS .SO_NO =(select cost_code from MAT_DETAILS where ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "')) as COST_CENTRE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "'"
    '            ElseIf issue_type = "To Deptt" Then
    '                PO_QUARY = "select material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select cost.cost_centre  from MAT_DETAILS  join cost on MAT_DETAILS.COST_CODE   =cost.cost_code  where MAT_DETAILS .ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "') as cost_centre from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & REPORTDropDownList3.SelectedValue & "'"
    '            End If
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(PO_QUARY, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/issue_ind.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()

    '        ElseIf REPORTDropDownList2.SelectedValue = "To Contractor" And REPORTDropDownList3.SelectedValue <> "Select" Then
    '            ''all
    '            quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join ORDER_DETAILS  on MAT_DETAILS .COST_CODE =ORDER_DETAILS .SO_NO join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID   where MAT_DETAILS .ISSUE_TYPE ='To Contractor' and supl.SUPL_NAME ='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()
    '        ElseIf REPORTDropDownList2.SelectedValue = "To Deptt" And REPORTDropDownList3.SelectedValue <> "Select" Then
    '            ''all
    '            quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(cost.cost_centre ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join cost on MAT_DETAILS .COST_CODE =cost.cost_code  where MAT_DETAILS .ISSUE_TYPE ='To Deptt' and cost .cost_centre ='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()

    '        ElseIf REPORTDropDownList2.SelectedValue = "Individual" And REPORTDropDownList3.SelectedValue <> "Select" Then
    '            ''all
    '            quary = "select material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select cost.cost_centre  from MAT_DETAILS  join cost on MAT_DETAILS.COST_CODE   =cost.cost_code  where MAT_DETAILS .ISSUE_NO ='" & REPORTDropDownList3.Text & "') as cost_centre from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & REPORTDropDownList3.Text & "'"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/issue_ind.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()

    '        ElseIf REPORTDropDownList2.SelectedValue = "I.P.T. Issue" And REPORTDropDownList3.SelectedValue <> "Select" Then
    '            ''all
    '            '' quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join PO_ORDER  on MAT_DETAILS .COST_CODE =PO_ORDER .PO_NO join SUPL on PO_ORDER .SUPPLIER_CODE =SUPL.SUPL_ID   where MAT_DETAILS .ISSUE_TYPE ='To Contractor' and supl.SUPL_NAME ='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
    '            quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type FROM MAT_DETAILS JOIN SUPL ON MAT_DETAILS .COST_CODE =SUPL.SUPL_ID JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE  WHERE ISSUE_NO='" & REPORTDropDownList3.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()

    '        ElseIf REPORTDropDownList2.SelectedValue = "All Issue" And REPORTDropDownList3.SelectedValue = "All" Then
    '            quary = "select MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,('All Issue Report' ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE where LINE_TYPE ='i' and  LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
    '            conn.Open()
    '            Dim dt As New DataTable
    '            da = New SqlDataAdapter(quary, conn)
    '            da.Fill(dt)
    '            conn.Close()
    '            crystalReport.Load(Server.MapPath("~/print_rpt/all_issue.rpt"))
    '            crystalReport.SetDataSource(dt)
    '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))

    '            Dim url As String = "REPORT.aspx"
    '            Dim sb As New StringBuilder()
    '            sb.Append("<script type = 'text/javascript'>")
    '            sb.Append("window.open('")
    '            sb.Append(url)
    '            sb.Append("');")
    '            sb.Append("</script>")
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '            crystalReport.Close()
    '            crystalReport.Dispose()

    '        End If
    '    ElseIf REPORTDropDownList.SelectedValue = "Mat Trans" Then
    '        Dim quary_trans As String = "select MAT_DETAILS .UNIT_PRICE,MAT_DETAILS .TOTAL_PRICE,MAT_DETAILS .LINE_NO,MAT_DETAILS .cost_code ,MAT_DETAILS .LINE_DATE ,MAT_DETAILS .LINE_TYPE ,MAT_DETAILS .ISSUE_NO ,(MAT_DETAILS .MAT_QTY +MAT_DETAILS .ISSUE_QTY ) as mat_qty,mat_details.MAT_BALANCE,mat_details.mat_code,material.mat_name,material.MAT_AU,('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "' ) AS REPORT_FROM from mat_details join material on mat_details.mat_code=material.mat_code where LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
    '        If REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
    '            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
    '        ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue = "All Material" Then
    '            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%'"
    '        ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
    '            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"
    '        End If
    '        If DropDownList20.SelectedValue = "Purchase" Then
    '            quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='R'"
    '        ElseIf DropDownList20.SelectedValue = "Issue" Then
    '            quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I'"
    '        End If
    '        quary_trans = quary_trans & " order by LINE_NO"
    '        conn.Open()
    '        Dim dt As New DataTable
    '        da = New SqlDataAdapter(quary_trans, conn)
    '        da.Fill(dt)
    '        conn.Close()
    '        crystalReport.Load(Server.MapPath("~/print_rpt/trans_report.rpt"))
    '        crystalReport.SetDataSource(dt)
    '        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
    '        Dim url As String = "REPORT.aspx"
    '        Dim sb As New StringBuilder()
    '        sb.Append("<script type = 'text/javascript'>")
    '        sb.Append("window.open('")
    '        sb.Append(url)
    '        sb.Append("');")
    '        sb.Append("</script>")
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '        crystalReport.Close()
    '        crystalReport.Dispose()

    '    ElseIf REPORTDropDownList.SelectedValue = "Total Issue" Then

    '        Dim quary_trans As String = "select mat_details.mat_code, material.mat_name, material.MAT_AU, sum(MAT_DETAILS .ISSUE_QTY ) as mat_qty,('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "' ) AS duration from mat_details join material on mat_details.mat_code=material.mat_code where LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"

    '        If REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then

    '            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"

    '        ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue = "All Material" Then

    '            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%'"

    '            'ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then

    '            '    quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"

    '        End If

    '        quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I' group by mat_details.MAT_CODE,material.mat_name,material.MAT_AU"

    '        'If DropDownList20.SelectedValue = "Purchase" Then
    '        '    quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='R'"
    '        'ElseIf DropDownList20.SelectedValue = "Issue" Then
    '        '    quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I'"
    '        'End If
    '        quary_trans = quary_trans & " order by mat_details.MAT_CODE"
    '        conn.Open()
    '        Dim dt As New DataTable
    '        da = New SqlDataAdapter(quary_trans, conn)
    '        da.Fill(dt)
    '        conn.Close()
    '        crystalReport.Load(Server.MapPath("~/print_rpt/total_issue_store.rpt"))
    '        crystalReport.SetDataSource(dt)
    '        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
    '        Dim url As String = "REPORT.aspx"
    '        Dim sb As New StringBuilder()
    '        sb.Append("<script type = 'text/javascript'>")
    '        sb.Append("window.open('")
    '        sb.Append(url)
    '        sb.Append("');")
    '        sb.Append("</script>")
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '        crystalReport.Close()
    '        crystalReport.Dispose()

    '    ElseIf REPORTDropDownList.SelectedValue = "CRR" Then
    '        Dim s_date, e_date As Date
    '        s_date = CDate(REPORTTextBox1.Text)
    '        e_date = CDate(REPORTTextBox2.Text)
    '        Dim dt1 As New DataTable()
    '        If REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
    '            conn.Open()
    '            dt1.Clear()
    '            da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'scrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' and po_rcd_mat.mat_code ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' order by PO_RCD_MAT .CRR_NO ", conn)
    '            da.Fill(dt1)
    '            conn.Close()
    '        ElseIf REPORTDropDownList2.SelectedValue <> "All Group" And REPORTDropDownList3.SelectedValue = "All Material" Then
    '            conn.Open()
    '            dt1.Clear()
    '            da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'scrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' and po_rcd_mat.mat_code like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' order by PO_RCD_MAT .CRR_NO ", conn)
    '            da.Fill(dt1)
    '            conn.Close()
    '        ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList3.SelectedValue <> "All Material" Then
    '            conn.Open()
    '            dt1.Clear()
    '            da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'scrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' and po_rcd_mat.mat_code ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' order by PO_RCD_MAT .CRR_NO ", conn)
    '            da.Fill(dt1)
    '            conn.Close()
    '        ElseIf REPORTDropDownList2.SelectedValue = "All Group" And REPORTDropDownList3.SelectedValue = "All Material" Then
    '            conn.Open()
    '            dt1.Clear()
    '            da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'scrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' order by PO_RCD_MAT .CRR_NO ", conn)
    '            da.Fill(dt1)
    '            conn.Close()
    '        End If
    '        crystalReport.Load(Server.MapPath("~/print_rpt/CRR_REPORT.rpt"))
    '        crystalReport.SetDataSource(dt1)
    '        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
    '        Dim url As String = "REPORT.aspx"
    '        Dim sb As New StringBuilder()
    '        sb.Append("<script type = 'text/javascript'>")
    '        sb.Append("window.open('")
    '        sb.Append(url)
    '        sb.Append("');")
    '        sb.Append("</script>")
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '        crystalReport.Close()
    '        crystalReport.Dispose()

    '    ElseIf REPORTDropDownList.SelectedValue = "Non-Moving Item" Then
    '        Dim s_date, target_date As Date
    '        s_date = CDate(REPORTTextBox1.Text)
    '        target_date = CDate(REPORTTextBox1.Text).AddYears(-5)
    '        Dim dt1 As New DataTable()

    '        conn.Open()
    '        dt1.Clear()
    '        'da = New SqlDataAdapter("select ('" & REPORTTextBox1.Text & " To " & REPORTTextBox2.Text & "') as ser_date, PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'scrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "' and po_rcd_mat.mat_code ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' order by PO_RCD_MAT .CRR_NO ", conn)
    '        da = New SqlDataAdapter("select *,'" & s_date.Day & "-" & s_date.Month & "-" & s_date.Year & "' AS SEARCH_DATE from MATERIAL where MAT_CODE not like '100%' and LAST_TRANS_DATE < '" & target_date.Year & "-" & target_date.Month & "-" & target_date.Day & "' AND MAT_STOCK > 0 order by MAT_CODE", conn)
    '        da.Fill(dt1)
    '        conn.Close()

    '        crystalReport.Load(Server.MapPath("~/print_rpt/non_moving_stores.rpt"))
    '        crystalReport.SetDataSource(dt1)
    '        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
    '        Dim url As String = "REPORT.aspx"
    '        Dim sb As New StringBuilder()
    '        sb.Append("<script type = 'text/javascript'>")
    '        sb.Append("window.open('")
    '        sb.Append(url)
    '        sb.Append("');")
    '        sb.Append("</script>")
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
    '        crystalReport.Close()
    '        crystalReport.Dispose()
    '    End If

    'End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Stock Position" Then
            MultiView1.ActiveViewIndex = 0

            dt.Clear()
            conn.Open()
            REPORTDropDownList2.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList2.Items.Clear()
            REPORTDropDownList2.DataSource = dt
            REPORTDropDownList2.DataValueField = "GROUP_DETAIL"
            REPORTDropDownList2.DataBind()
            REPORTDropDownList2.Items.Insert(0, "Select")
            REPORTDropDownList2.Items.Insert(1, "All Material")
            REPORTDropDownList2.SelectedValue = "Select"
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "Issue" Then
            MultiView1.ActiveViewIndex = 1
        ElseIf DropDownList9.SelectedValue = "Group-Wise Issue" Then
            MultiView1.ActiveViewIndex = 2

            dt.Clear()
            conn.Open()
            ddl_mat_group.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
            da.Fill(dt)
            ddl_mat_group.DataSource = dt
            ddl_mat_group.DataValueField = "GROUP_DETAIL"
            ddl_mat_group.DataBind()
            ddl_mat_group.Items.Insert(0, "Select")
            ddl_mat_group.Items.Insert(1, "All Group")
            ddl_mat_group.SelectedValue = "Select"
            conn.Close()
        ElseIf DropDownList9.SelectedValue = "Mat. Transaction" Then
            MultiView1.ActiveViewIndex = 3

            dt.Clear()
            conn.Open()
            DropDownList1.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
            da.Fill(dt)
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "GROUP_DETAIL"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            DropDownList1.Items.Insert(1, "All Group")
            DropDownList1.SelectedValue = "Select"
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "CRR" Then
            MultiView1.ActiveViewIndex = 4

            dt.Clear()
            conn.Open()
            DropDownList3.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
            da.Fill(dt)
            DropDownList3.DataSource = dt
            DropDownList3.DataValueField = "GROUP_DETAIL"
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "Select")
            DropDownList3.Items.Insert(1, "All Group")
            DropDownList3.SelectedValue = "Select"
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "Non-Moving Items" Then
            MultiView1.ActiveViewIndex = 5

        ElseIf DropDownList9.SelectedValue = "View Materials Group Wise" Then
            MultiView1.ActiveViewIndex = 6

            dt.Clear()
            conn.Open()
            DropDownList6.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
            da.Fill(dt)
            DropDownList6.Items.Clear()
            DropDownList6.DataSource = dt
            DropDownList6.DataValueField = "GROUP_DETAIL"
            DropDownList6.DataBind()
            DropDownList6.Items.Insert(0, "Select")
            DropDownList6.Items.Insert(1, "All Material")
            DropDownList6.SelectedValue = "Select"
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "Store Inventory Detail" Then
            MultiView1.ActiveViewIndex = 7

            dt.Clear()
            conn.Open()
            DropDownList6.Items.Clear()
            da = New SqlDataAdapter("select ( GROUP_CODE + ' , ' + GROUP_NAME) AS GROUP_DETAIL from BIN_GROUP WHERE GROUP_CODE LIKE '0%' order by GROUP_CODE", conn)
            da.Fill(dt)
            DropDownList6.Items.Clear()
            DropDownList6.DataSource = dt
            DropDownList6.DataValueField = "GROUP_DETAIL"
            DropDownList6.DataBind()
            DropDownList6.Items.Insert(0, "Select")
            DropDownList6.Items.Insert(1, "All Material")
            DropDownList6.SelectedValue = "Select"
            conn.Close()

        End If

    End Sub

    Protected Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
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
        If REPORTDropDownList2.SelectedValue = "All Material" And REPORTDropDownList3.SelectedValue <> "All" Then
            quary = "SELECT ROW_NUMBER() OVER (ORDER BY MAT_CODE) AS ROW_NO,(SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS LINE_NO, MATERIAL.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MATERIAL.MAT_DRAW,MATERIAL.MAT_AVG,MATERIAL.MAT_STOCK,MATERIAL.MAT_LAST_RATE,MATERIAL.MAT_LASTPUR_DATE,MATERIAL.OPEN_STOCK,MATERIAL.OPEN_AVG_PRICE,MATERIAL.RE_ORDER_LABEL,MATERIAL.ORDER_STOP_IND,MATERIAL.LAST_ISSUE_DATE,MATERIAL.LAST_TRANS_DATE,(SELECT SUM(ISSUE_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_COMP,(SELECT SUM(MAT_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_PUR  FROM MATERIAL  WHERE MATERIAL .MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"

        ElseIf REPORTDropDownList2.SelectedValue = "All Material" And REPORTDropDownList3.SelectedValue = "All" Then
            quary = "SELECT ROW_NUMBER() OVER (ORDER BY MAT_CODE) AS ROW_NO,* FROM MATERIAL WHERE MAT_STOCK > 0"

        ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And REPORTDropDownList3.SelectedValue = "All" Then
            quary = "SELECT ROW_NUMBER() OVER (ORDER BY MAT_CODE) AS ROW_NO,* FROM MATERIAL where mat_code like '" & REPORTDropDownList2.Text.Substring(0, REPORTDropDownList2.Text.IndexOf(",") - 1) & "%' AND MAT_STOCK > 0"

        ElseIf REPORTDropDownList2.SelectedValue <> "All Material" And REPORTDropDownList3.SelectedValue <> "All" Then

            quary = "SELECT ROW_NUMBER() OVER (ORDER BY MAT_CODE) AS ROW_NO,(SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS LINE_NO, MATERIAL.MAT_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MATERIAL.MAT_DRAW,MATERIAL.MAT_AVG,MATERIAL.MAT_STOCK,MATERIAL.MAT_LAST_RATE,MATERIAL.MAT_LASTPUR_DATE,MATERIAL.OPEN_STOCK,MATERIAL.OPEN_AVG_PRICE,MATERIAL.RE_ORDER_LABEL,MATERIAL.ORDER_STOP_IND,MATERIAL.LAST_ISSUE_DATE,MATERIAL.LAST_TRANS_DATE,(SELECT SUM(ISSUE_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_COMP,(SELECT SUM(MAT_QTY) FROM MAT_DETAILS WHERE MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "' AND FISCAL_YEAR=" & STR1 & ")AS CUR_PUR  FROM MATERIAL  WHERE MATERIAL .MAT_CODE ='" & REPORTDropDownList3.Text.Substring(0, REPORTDropDownList3.Text.IndexOf(",") - 1) & "'"

        End If

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView10.DataSource = dt
        GridView10.DataBind()
    End Sub

    Protected Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        If GridView10.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(4) {New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_STOCK", GetType(Decimal)), New DataColumn("MAT_AVG", GetType(Decimal))})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView10.Rows.Count - 1
            dt2.Rows.Add(GridView10.Rows(i).Cells(1).Text, GridView10.Rows(i).Cells(2).Text, GridView10.Rows(i).Cells(3).Text, CDec(GridView10.Rows(i).Cells(4).Text), CDec(GridView10.Rows(i).Cells(5).Text))
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/stock_total.rpt"))
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

        Dim dt3 As DataTable = New DataTable("Stock Position")
        dt3.Columns.Add(New DataColumn("Sl No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/U", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Stock", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Mat Avg. Price", GetType(Decimal)))


        For Me.count = 0 To GridView10.Rows.Count - 1
            dt3.Rows.Add(GridView10.Rows(count).Cells(0).Text, GridView10.Rows(count).Cells(1).Text, GridView10.Rows(count).Cells(2).Text, GridView10.Rows(count).Cells(3).Text, CDec(GridView10.Rows(count).Cells(4).Text), CDec(GridView10.Rows(count).Cells(5).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Stores_Stock_Position.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub



    Protected Sub ddl_issue_type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_issue_type.SelectedIndexChanged
        If ddl_issue_type.SelectedValue = "All Issue" Then
            ddl_issue_no.Visible = True

            ddl_issue_no.Items.Insert(0, "Select")
            ddl_issue_no.Items.Insert(1, "All")
            ddl_issue_no.SelectedValue = "All"
            Label23.Visible = False
            DropDownList5.Visible = False
            TextBox1.Visible = True
            TextBox2.Visible = True

        ElseIf ddl_issue_type.SelectedValue = "Individual" Then
            Label23.Visible = True
            DropDownList5.Visible = True

            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList5.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList5.DataValueField = "FY"
            DropDownList5.DataBind()
            DropDownList5.Items.Insert(0, "Select")
            conn.Close()

            TextBox1.Visible = False
            TextBox2.Visible = False

        ElseIf ddl_issue_type.SelectedValue = "I.P.T. Issue" Then
            Label23.Visible = False
            DropDownList5.Visible = False
            ddl_issue_no.Visible = True
            conn.Open()
            da = New SqlDataAdapter("select distinct issue_no from mat_details where (ISSUE_TYPE='I.P.T.') and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') ORDER BY issue_no", conn)
            da.Fill(ds, "mat_details")
            ddl_issue_no.Items.Clear()
            ddl_issue_no.DataSource = ds.Tables("mat_details")
            ddl_issue_no.DataValueField = "issue_no"
            ddl_issue_no.DataBind()
            conn.Close()
            ddl_issue_no.Items.Insert(0, "Select")
            ddl_issue_no.Items.Insert(1, "All")
            ddl_issue_no.SelectedValue = "Select"
            TextBox1.Visible = True
            TextBox2.Visible = True

        ElseIf ddl_issue_type.SelectedValue = "To Contractor" Then
            Label23.Visible = False
            DropDownList5.Visible = False
            conn.Open()
            da = New SqlDataAdapter("select distinct SUPL.SUPL_NAME from supl join ORDER_DETAILS on SUPL.SUPL_ID =ORDER_DETAILS .PARTY_CODE join MAT_DETAILS on ORDER_DETAILS .SO_NO =MAT_DETAILS .COST_CODE where MAT_DETAILS .ISSUE_TYPE ='To Contractor' and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001')", conn)
            da.Fill(dt)
            ddl_issue_no.Items.Clear()
            ddl_issue_no.DataSource = dt
            ddl_issue_no.DataValueField = "SUPL_NAME"
            ddl_issue_no.DataBind()
            conn.Close()
            ddl_issue_no.Items.Insert(0, "Select")
            ddl_issue_no.SelectedValue = "Select"
            TextBox1.Visible = True
            TextBox2.Visible = True

        ElseIf ddl_issue_type.SelectedValue = "To Deptt" Then
            Label23.Visible = False
            DropDownList5.Visible = False

            conn.Open()
            da = New SqlDataAdapter("select distinct cost.cost_centre from cost join MAT_DETAILS on cost.cost_code =MAT_DETAILS .COST_CODE where MAT_DETAILS .ISSUE_TYPE ='To Deptt'", conn)
            da.Fill(dt)
            ddl_issue_no.Items.Clear()
            ddl_issue_no.DataSource = dt
            ddl_issue_no.DataValueField = "cost_centre"
            ddl_issue_no.DataBind()
            conn.Close()
            ddl_issue_no.Items.Insert(0, "Select")
            ddl_issue_no.Items.Insert(1, "All")
            ddl_issue_no.SelectedValue = "Select"
            TextBox1.Visible = True
            TextBox2.Visible = True
        End If


        dt.Clear()
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

        If ddl_issue_type.SelectedValue = "Select" Then
            ddl_issue_type.Focus()
            Return
        ElseIf ddl_issue_no.SelectedValue = "Select" Then
            ddl_issue_no.Focus()
            Return
        End If

        Dim from_date, to_date As Date

        If (TextBox1.Visible = True) Then
            If TextBox1.Text = "" Or IsDate(TextBox1.Text) = False Then
                TextBox1.Text = ""
                TextBox1.Focus()
                Return
            ElseIf TextBox2.Text = "" Or IsDate(TextBox2.Text) = False Then
                TextBox2.Text = ""
                TextBox2.Focus()
                Return
            End If

            from_date = Date.ParseExact(TextBox1.Text, "dd-MM-yyyy", provider)
            to_date = Date.ParseExact(TextBox2.Text, "dd-MM-yyyy", provider)

        End If


        Dim crystalReport As New ReportDocument
        Dim quary As String = ""


        If ddl_issue_type.SelectedValue = "All Issue" And ddl_issue_no.SelectedValue <> "All" Then
            conn.Open()
            Dim mc As New SqlCommand
            Dim issue_type As String = ""
            mc.CommandText = "select issue_type from MAT_DETAILS where ISSUE_NO='" & ddl_issue_no.SelectedValue & "'"
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
                quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select SUPL.SUPL_NAME from ORDER_DETAILS join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID where ORDER_DETAILS .SO_NO =(select cost_code from MAT_DETAILS where ISSUE_NO ='" & ddl_issue_no.SelectedValue & "')) as COST_CENTRE, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & ddl_issue_no.SelectedValue & "'"
            ElseIf issue_type = "To Deptt" Then
                quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select cost.cost_centre from MAT_DETAILS join cost on MAT_DETAILS.COST_CODE   =cost.cost_code where MAT_DETAILS .ISSUE_NO ='" & ddl_issue_no.SelectedValue & "') as cost_centre, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & ddl_issue_no.SelectedValue & "'"
            End If


        ElseIf ddl_issue_type.SelectedValue = "To Contractor" And ddl_issue_no.SelectedValue <> "Select" Then
            ''all
            quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join ORDER_DETAILS  on MAT_DETAILS .COST_CODE =ORDER_DETAILS .SO_NO join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID where MAT_DETAILS .ISSUE_TYPE ='To Contractor' and supl.SUPL_NAME ='" & ddl_issue_no.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') order by ISSUE_NO"

        ElseIf ddl_issue_type.SelectedValue = "To Deptt" And ddl_issue_no.SelectedValue <> "Select" Then
            ''all
            If (ddl_issue_no.SelectedValue = "All") Then
                quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(cost.cost_centre ) as issue_type, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join cost on MAT_DETAILS .COST_CODE =cost.cost_code where MAT_DETAILS .ISSUE_TYPE ='To Deptt' and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') order by ISSUE_NO"
            Else
                quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(cost.cost_centre ) as issue_type, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join cost on MAT_DETAILS .COST_CODE =cost.cost_code where MAT_DETAILS .ISSUE_TYPE ='To Deptt' and cost .cost_centre ='" & ddl_issue_no.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') order by ISSUE_NO"
            End If


        ElseIf ddl_issue_type.SelectedValue = "Individual" And ddl_issue_no.SelectedValue <> "Select" Then
            ''all
            quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,material.mat_name,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.RQD_BY,RQD_DATE,MAT_DETAILS.ISSUE_BY,ISSUE_DATE,MAT_DETAILS.AUTH_BY,AUTH_DATE,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, cost.cost_centre from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE join cost on MAT_DETAILS.COST_CODE =cost.cost_code where mat_details.ISSUE_NO='" & ddl_issue_no.Text & "'"

        ElseIf ddl_issue_type.SelectedValue = "I.P.T. Issue" And ddl_issue_no.SelectedValue <> "Select" Then
            ''all
            If (ddl_issue_no.SelectedValue = "All") Then
                quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(dater.d_name  ) as issue_type, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE FROM MAT_DETAILS JOIN dater ON MAT_DETAILS .COST_CODE =dater.d_code JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE WHERE (MAT_DETAILS.ISSUE_TYPE='I.P.T.' or MAT_DETAILS.ISSUE_TYPE='Other') and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
            Else
                quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(dater.d_name  ) as issue_type, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE FROM MAT_DETAILS JOIN dater ON MAT_DETAILS .COST_CODE =dater.d_code JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE  WHERE ISSUE_NO='" & ddl_issue_no.SelectedValue & "' order by ISSUE_NO"
            End If

        ElseIf ddl_issue_type.SelectedValue = "All Issue" And ddl_issue_no.SelectedValue = "All" Then
            quary = "select ROW_NUMBER() OVER (ORDER BY ISSUE_NO) AS ROW_NO,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,('All Issue Report' ) as issue_type, RQD_BY, RQD_DATE, AUTH_BY, AUTH_DATE, ISSUE_BY, ISSUE_DATE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE where (LINE_TYPE ='I' or LINE_TYPE ='A') and  LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') order by ISSUE_NO"

        End If

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
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


        If ddl_issue_type.SelectedValue = "All Issue" And ddl_issue_no.SelectedValue <> "All" Then

            Dim from_date As Date = Date.ParseExact(CDate(TextBox1.Text), "dd-MM-yyyy", provider)
            Dim to_date As Date = Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider)
            conn.Open()
            Dim mc As New SqlCommand
            Dim issue_type As String = ""
            mc.CommandText = "select issue_type from MAT_DETAILS where ISSUE_NO='" & ddl_issue_no.SelectedValue & "'"
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
                PO_QUARY = "select material.mat_name, POST_TYPE,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select SUPL.SUPL_NAME from ORDER_DETAILS join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID where ORDER_DETAILS .SO_NO =(select cost_code from MAT_DETAILS where ISSUE_NO ='" & ddl_issue_no.SelectedValue & "')) as COST_CENTRE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & ddl_issue_no.SelectedValue & "'"
            ElseIf issue_type = "To Deptt" Then
                PO_QUARY = "select material.mat_name, POST_TYPE,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.ISSUE_BY,MAT_DETAILS.AUTH_BY,MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select cost.cost_centre  from MAT_DETAILS  join cost on MAT_DETAILS.COST_CODE   =cost.cost_code  where MAT_DETAILS .ISSUE_NO ='" & ddl_issue_no.SelectedValue & "') as cost_centre from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & ddl_issue_no.SelectedValue & "'"
            End If
            conn.Open()
            Dim dt As New DataTable
            da = New SqlDataAdapter(PO_QUARY, conn)
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

        ElseIf ddl_issue_type.SelectedValue = "To Contractor" And ddl_issue_no.SelectedValue <> "Select" Then
            Dim from_date As Date = Date.ParseExact(CDate(TextBox1.Text), "dd-MM-yyyy", provider)
            Dim to_date As Date = Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider)
            ''all
            quary = "select MAT_DETAILS.ISSUE_NO, POST_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(SUPL.SUPL_NAME  ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join ORDER_DETAILS  on MAT_DETAILS .COST_CODE =ORDER_DETAILS .SO_NO join SUPL on ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID   where MAT_DETAILS .ISSUE_TYPE ='To Contractor' and supl.SUPL_NAME ='" & ddl_issue_no.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
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
        ElseIf ddl_issue_type.SelectedValue = "To Deptt" And ddl_issue_no.SelectedValue <> "Select" Then

            Dim from_date As Date = Date.ParseExact(CDate(TextBox1.Text), "dd-MM-yyyy", provider)
            Dim to_date As Date = Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider)
            ''all
            If (ddl_issue_no.SelectedValue = "All") Then
                quary = "select '" & TextBox1.Text + " to " + TextBox2.Text & "' As Duration,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(cost.cost_centre ) as issue_type, POST_TYPE from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join cost on MAT_DETAILS .COST_CODE =cost.cost_code  where MAT_DETAILS .ISSUE_TYPE ='To Deptt' and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
            Else
                quary = "select '" & TextBox1.Text + " to " + TextBox2.Text & "' As Duration,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(cost.cost_centre ) as issue_type, POST_TYPE  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE join cost on MAT_DETAILS .COST_CODE =cost.cost_code  where MAT_DETAILS .ISSUE_TYPE ='To Deptt' and cost .cost_centre ='" & ddl_issue_no.SelectedValue & "'and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
            End If

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

        ElseIf ddl_issue_type.SelectedValue = "Individual" And ddl_issue_no.SelectedValue <> "Select" Then

            ''all
            quary = "select material.mat_name, POST_TYPE,MATERIAL.MAT_AU, MAT_DETAILS.ISSUE_NO,MAT_DETAILS.ISSUE_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MAT_DETAILS.MAT_CODE,MAT_DETAILS.RQD_QTY,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.MAT_BALANCE,MAT_DETAILS.UNIT_PRICE,MAT_DETAILS.TOTAL_PRICE,MAT_DETAILS.PURPOSE,MAT_DETAILS.COST_CODE,MAT_DETAILS.RQD_BY,MAT_DETAILS.ISSUE_BY, ISSUE_DATE,MAT_DETAILS.AUTH_BY, AUTH_DATE, MAT_DETAILS.REMARKS,MAT_DETAILS.RQD_DATE, (select DISTINCT cost.cost_centre  from MAT_DETAILS  join cost on MAT_DETAILS.COST_CODE   =cost.cost_code  where MAT_DETAILS .ISSUE_NO ='" & ddl_issue_no.Text & "') as cost_centre from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE  where mat_details.ISSUE_NO ='" & ddl_issue_no.Text & "'"
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

        ElseIf ddl_issue_type.SelectedValue = "I.P.T. Issue" And ddl_issue_no.SelectedValue <> "Select" Then

            Dim from_date As Date = Date.ParseExact(CDate(TextBox1.Text), "dd-MM-yyyy", provider)
            Dim to_date As Date = Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider)
            ''all
            If (ddl_issue_no.SelectedValue = "All") Then
                'quary = "select '" & TextBox1.Text + " to " + TextBox2.Text & "' As Duration,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(dater.d_name) as issue_type FROM MAT_DETAILS JOIN dater ON MAT_DETAILS .COST_CODE =dater.d_code JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE WHERE (MAT_DETAILS.ISSUE_TYPE='I.P.T.' or MAT_DETAILS.ISSUE_TYPE='Other') and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
                quary = "select '" & TextBox1.Text + " to " + TextBox2.Text & "' As Duration, POST_TYPE,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,'" & ddl_issue_type.SelectedValue & "' as issue_type FROM MAT_DETAILS JOIN dater ON MAT_DETAILS .COST_CODE =dater.d_code JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE WHERE (MAT_DETAILS.ISSUE_TYPE='I.P.T.' or MAT_DETAILS.ISSUE_TYPE='Other') and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
            Else
                'quary = "select '" & TextBox1.Text + " to " + TextBox2.Text & "' As Duration,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,(dater.d_name) as issue_type FROM MAT_DETAILS JOIN dater ON MAT_DETAILS .COST_CODE =dater.d_code JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE  WHERE ISSUE_NO='" & ddl_issue_no.SelectedValue & "' order by ISSUE_NO"
                quary = "select '" & TextBox1.Text + " to " + TextBox2.Text & "' As Duration, POST_TYPE,MAT_DETAILS.ISSUE_NO,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,'" & ddl_issue_type.SelectedValue & "' as issue_type FROM MAT_DETAILS JOIN dater ON MAT_DETAILS .COST_CODE =dater.d_code JOIN MATERIAL ON MAT_DETAILS.MAT_CODE =MATERIAL.MAT_CODE  WHERE ISSUE_NO='" & ddl_issue_no.SelectedValue & "' order by ISSUE_NO"
            End If

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

        ElseIf ddl_issue_type.SelectedValue = "All Issue" And ddl_issue_no.SelectedValue = "All" Then

            Dim from_date As Date = Date.ParseExact(CDate(TextBox1.Text), "dd-MM-yyyy", provider)
            Dim to_date As Date = Date.ParseExact(CDate(TextBox2.Text), "dd-MM-yyyy", provider)

            quary = "select MAT_DETAILS.ISSUE_NO, POST_TYPE,MAT_DETAILS.LINE_NO,MAT_DETAILS.LINE_DATE,MATERIAL.MAT_CODE,MAT_DETAILS.ISSUE_QTY,MAT_DETAILS.COST_CODE,MATERIAL.MAT_NAME,MATERIAL.MAT_AU,MAT_DETAILS.TOTAL_PRICE,('All Issue Report' ) as issue_type  from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL.MAT_CODE where LINE_TYPE ='i' and  LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by ISSUE_NO"
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
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If GridView1.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Store Issue")

        dt3.Columns.Add(New DataColumn("Sl No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Line No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/U", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Issue Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("REQ. By", GetType(String)))
        dt3.Columns.Add(New DataColumn("REQ. Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Auth. By", GetType(String)))
        dt3.Columns.Add(New DataColumn("Auth. Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue  By", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue  Date", GetType(String)))


        For Me.count = 0 To GridView1.Rows.Count - 1
            dt3.Rows.Add(GridView1.Rows(count).Cells(0).Text, GridView1.Rows(count).Cells(1).Text, GridView1.Rows(count).Cells(2).Text, GridView1.Rows(count).Cells(3).Text, GridView1.Rows(count).Cells(4).Text, GridView1.Rows(count).Cells(5).Text, GridView1.Rows(count).Cells(6).Text, CDec(GridView1.Rows(count).Cells(7).Text), CDec(GridView1.Rows(count).Cells(8).Text), GridView1.Rows(count).Cells(9).Text, GridView1.Rows(count).Cells(10).Text, GridView1.Rows(count).Cells(11).Text, GridView1.Rows(count).Cells(12).Text, GridView1.Rows(count).Cells(13).Text, GridView1.Rows(count).Cells(14).Text)
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Stores_Issue_Report.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub ddl_mat_group_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_mat_group.SelectedIndexChanged
        If ddl_mat_group.SelectedValue <> "All Group" Then

            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & ddl_mat_group.Text.Substring(0, ddl_mat_group.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            ddl_mat_code.Items.Clear()
            ddl_mat_code.DataSource = dt
            ddl_mat_code.DataValueField = "MAT_DETAIL"
            ddl_mat_code.DataBind()
            conn.Close()
            ddl_mat_code.Items.Insert(0, "Select")
            ddl_mat_code.Items.Insert(1, "All Material")
            ddl_mat_code.SelectedValue = "Select"

        ElseIf ddl_mat_group.SelectedValue = "All Group" Then

            ddl_mat_code.Items.Insert(0, "All Material")
            'dt.Clear()
            'conn.Open()
            'da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL where MAT_CODE not like '100%' order by MAT_CODE", conn)
            'da.Fill(dt)
            'ddl_mat_code.Items.Clear()
            'ddl_mat_code.DataSource = dt
            'ddl_mat_code.DataValueField = "MAT_DETAIL"
            'ddl_mat_code.DataBind()
            'conn.Close()
            'ddl_mat_code.Items.Add("Select")
            'ddl_mat_code.SelectedValue = "Select"

        End If
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim from_date As Date = Date.ParseExact(CDate(TextBox3.Text), "dd-MM-yyyy", provider)
        Dim to_date As Date = Date.ParseExact(CDate(TextBox4.Text), "dd-MM-yyyy", provider)

        Dim quary_trans As String = "select ROW_NUMBER() OVER (ORDER BY mat_details.mat_code) AS ROW_NO,mat_details.mat_code, material.mat_name, material.MAT_AU, sum(MAT_DETAILS .ISSUE_QTY) as mat_qty,('" & TextBox3.Text & " To " & TextBox4.Text & "' ) AS duration, Sum(MAT_DETAILS.TOTAL_PRICE) As Total_Value from mat_details join material on mat_details.mat_code=material.mat_code where LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
        If ddl_mat_group.SelectedValue <> "All Group" And ddl_mat_code.SelectedValue <> "All Material" Then

            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & ddl_mat_code.Text.Substring(0, ddl_mat_code.Text.IndexOf(",") - 1) & "'"

        ElseIf ddl_mat_group.SelectedValue <> "All Group" And ddl_mat_code.SelectedValue = "All Material" Then

            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE like '" & ddl_mat_group.Text.Substring(0, ddl_mat_group.Text.IndexOf(",") - 1) & "%'"

        End If

        quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I' group by mat_details.MAT_CODE,material.mat_name,material.MAT_AU"


        quary_trans = quary_trans & " order by mat_details.MAT_CODE"
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary_trans, conn)
        da.Fill(dt)
        conn.Close()
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim from_date As Date = Date.ParseExact(CDate(TextBox3.Text), "dd-MM-yyyy", provider)
        Dim to_date As Date = Date.ParseExact(CDate(TextBox4.Text), "dd-MM-yyyy", provider)

        Dim crystalReport As New ReportDocument
        Dim quary_trans As String = "select mat_details.mat_code, material.mat_name, material.MAT_AU, sum(MAT_DETAILS .ISSUE_QTY ) as mat_qty,('" & TextBox3.Text & " To " & TextBox4.Text & "' ) AS duration, Sum(MAT_DETAILS.TOTAL_PRICE) As Total_Value from mat_details join material on mat_details.mat_code=material.mat_code where LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
        If ddl_mat_group.SelectedValue <> "All Group" And ddl_mat_code.SelectedValue <> "All Material" Then

            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & ddl_mat_code.Text.Substring(0, ddl_mat_code.Text.IndexOf(",") - 1) & "'"

        ElseIf ddl_mat_group.SelectedValue <> "All Group" And ddl_mat_code.SelectedValue = "All Material" Then

            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE like '" & ddl_mat_group.Text.Substring(0, ddl_mat_group.Text.IndexOf(",") - 1) & "%'"

        End If

        quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I' group by mat_details.MAT_CODE,material.mat_name,material.MAT_AU"


        quary_trans = quary_trans & " order by mat_details.MAT_CODE"
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(quary_trans, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.Load(Server.MapPath("~/print_rpt/total_issue_store.rpt"))
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
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If GridView2.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Group Wise Issue")

        dt3.Columns.Add(New DataColumn("Sl No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/U", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Total Price", GetType(Decimal)))


        For Me.count = 0 To GridView2.Rows.Count - 1
            dt3.Rows.Add(GridView2.Rows(count).Cells(0).Text, GridView2.Rows(count).Cells(1).Text, GridView2.Rows(count).Cells(2).Text, GridView2.Rows(count).Cells(3).Text, CDec(GridView2.Rows(count).Cells(4).Text), CDec(GridView2.Rows(count).Cells(5).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Stores_Issue_GroupWise.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue <> "All Group" Then

            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "MAT_DETAIL"
            DropDownList2.DataBind()
            conn.Close()
            DropDownList2.Items.Insert(0, "Select")
            DropDownList2.Items.Insert(1, "All Material")
            DropDownList2.SelectedValue = "Select"

        ElseIf DropDownList1.SelectedValue = "All Group" Then

            DropDownList2.Items.Insert(0, "All Material")

        End If
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim from_date As Date = Date.ParseExact(CDate(TextBox5.Text), "dd-MM-yyyy", provider)
        Dim to_date As Date = Date.ParseExact(CDate(TextBox6.Text), "dd-MM-yyyy", provider)

        Dim quary_trans As String = "select ROW_NUMBER() OVER (ORDER BY mat_details.mat_code) AS ROW_NO,MAT_DETAILS .UNIT_PRICE,MAT_DETAILS .TOTAL_PRICE,MAT_DETAILS .LINE_NO,MAT_DETAILS .cost_code ,MAT_DETAILS .LINE_DATE ,MAT_DETAILS .LINE_TYPE ,MAT_DETAILS .ISSUE_NO ,(MAT_DETAILS .MAT_QTY +MAT_DETAILS .ISSUE_QTY ) as mat_qty,mat_details.MAT_BALANCE,mat_details.mat_code,material.mat_name,material.MAT_AU,('" & TextBox5.Text & " To " & TextBox6.Text & "' ) AS REPORT_FROM,MAT_DETAILS.fiscal_year from mat_details join material on mat_details.mat_code=material.mat_code where LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
        If DropDownList1.SelectedValue <> "All Group" And DropDownList2.SelectedValue <> "All Material" Then
            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList1.SelectedValue <> "All Group" And DropDownList2.SelectedValue = "All Material" Then
            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE like '" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "%'"

        ElseIf DropDownList1.SelectedValue = "All Group" And DropDownList2.SelectedValue <> "All Material" Then
            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList1.SelectedValue = "All Group" And DropDownList2.SelectedValue = "All Material" Then
            quary_trans = quary_trans
        End If

        If DropDownList21.SelectedValue = "Purchase" Then
            quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='R' and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') "
        ElseIf DropDownList21.SelectedValue = "Issue" Then
            quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I' and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') "
        ElseIf DropDownList21.SelectedValue = "All" Then
            quary_trans = quary_trans & " and (MAT_DETAILS.MAT_CODE like '0%' and MAT_DETAILS.MAT_CODE<>'097000001') "
        End If

        quary_trans = quary_trans & " order by mat_details.mat_code,MAT_DETAILS.fiscal_year,LINE_NO"

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary_trans, conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
    End Sub

    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim from_date As Date = Date.ParseExact(CDate(TextBox5.Text), "dd-MM-yyyy", provider)
        Dim to_date As Date = Date.ParseExact(CDate(TextBox6.Text), "dd-MM-yyyy", provider)

        Dim crystalReport As New ReportDocument
        Dim quary_trans As String = "select MAT_DETAILS .UNIT_PRICE,MAT_DETAILS .TOTAL_PRICE,MAT_DETAILS .LINE_NO,MAT_DETAILS .cost_code ,MAT_DETAILS .LINE_DATE ,MAT_DETAILS .LINE_TYPE ,MAT_DETAILS .ISSUE_NO ,(MAT_DETAILS .MAT_QTY +MAT_DETAILS .ISSUE_QTY ) as mat_qty,mat_details.MAT_BALANCE,mat_details.mat_code,material.mat_name,material.MAT_AU,('" & TextBox5.Text & " To " & TextBox6.Text & "' ) AS REPORT_FROM from mat_details join material on mat_details.mat_code=material.mat_code where LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
        If DropDownList1.SelectedValue <> "All Group" And DropDownList2.SelectedValue <> "All Material" Then
            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList1.SelectedValue <> "All Group" And DropDownList2.SelectedValue = "All Material" Then
            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE like '" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "%'"

        ElseIf DropDownList1.SelectedValue = "All Group" And DropDownList2.SelectedValue <> "All Material" Then
            quary_trans = quary_trans & " and MAT_DETAILS .MAT_CODE = '" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList1.SelectedValue = "All Group" And DropDownList2.SelectedValue = "All Material" Then
            quary_trans = quary_trans
        End If

        If DropDownList21.SelectedValue = "Purchase" Then
            quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='R'"
        ElseIf DropDownList21.SelectedValue = "Issue" Then
            quary_trans = quary_trans & " and MAT_DETAILS.LINE_TYPE ='I'"
        ElseIf DropDownList21.SelectedValue = "All" Then
            quary_trans = quary_trans
        End If

        quary_trans = quary_trans & " order by mat_details.mat_code,LINE_NO"

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
    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If GridView3.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Store Mat Transaction")

        dt3.Columns.Add(New DataColumn("Sl No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Line No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Line Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue/Receipt", GetType(String)))
        dt3.Columns.Add(New DataColumn("Issue/Garn No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Cost Center", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/U", GetType(String)))
        dt3.Columns.Add(New DataColumn("Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Unit Price", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Total Price", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Stock Balance", GetType(Decimal)))

        For Me.count = 0 To GridView3.Rows.Count - 1
            dt3.Rows.Add(GridView3.Rows(count).Cells(0).Text, GridView3.Rows(count).Cells(1).Text, GridView3.Rows(count).Cells(2).Text, GridView3.Rows(count).Cells(3).Text, GridView3.Rows(count).Cells(4).Text, GridView3.Rows(count).Cells(5).Text, GridView3.Rows(count).Cells(6).Text, GridView3.Rows(count).Cells(7).Text, GridView3.Rows(count).Cells(8).Text, CDec(GridView3.Rows(count).Cells(9).Text), CDec(GridView3.Rows(count).Cells(10).Text), CDec(GridView3.Rows(count).Cells(11).Text), CDec(GridView3.Rows(count).Cells(12).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Stores_Mat_Transaction.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim crystalReport As New ReportDocument

        Dim s_date, e_date As Date
        s_date = CDate(TextBox7.Text)
        e_date = CDate(TextBox8.Text)
        Dim dt As New DataTable()

        Dim quary_crr As String = "select ('" & TextBox7.Text & " To " & TextBox8.Text & "') as ser_date, PO_RCD_MAT .MAT_CODE , PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'scrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "'"

        If DropDownList3.SelectedValue <> "All Group" And DropDownList4.SelectedValue <> "All Material" Then

            quary_crr = quary_crr & " and po_rcd_mat.mat_code ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList3.SelectedValue <> "All Group" And DropDownList4.SelectedValue = "All Material" Then

            quary_crr = quary_crr & " and po_rcd_mat.mat_code like '" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "%'"

        ElseIf DropDownList3.SelectedValue = "All Group" And DropDownList4.SelectedValue <> "All Material" Then

            quary_crr = quary_crr & " and po_rcd_mat.mat_code ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList3.SelectedValue = "All Group" And DropDownList4.SelectedValue = "All Material" Then

            quary_crr = quary_crr

        End If
        quary_crr = quary_crr & " order by po_rcd_mat.crr_no"

        conn.Open()
        da = New SqlDataAdapter(quary_crr, conn)
        da.Fill(dt)
        conn.Close()

        crystalReport.Load(Server.MapPath("~/print_rpt/CRR_REPORT.rpt"))
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
    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If DropDownList3.SelectedValue <> "All Group" Then
            DropDownList4.Visible = True
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            DropDownList4.Items.Clear()
            DropDownList4.DataSource = dt
            DropDownList4.DataValueField = "MAT_DETAIL"
            DropDownList4.DataBind()
            conn.Close()
            DropDownList4.Items.Insert(0, "Select")
            DropDownList4.Items.Insert(1, "All Material")
            DropDownList4.SelectedValue = "Select"
        ElseIf DropDownList3.SelectedValue = "All Group" Then

            DropDownList4.Items.Insert(0, "All Material")
        End If
    End Sub

    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim s_date, e_date As Date
        s_date = CDate(TextBox7.Text)
        e_date = CDate(TextBox8.Text)
        Dim dt As New DataTable()

        Dim quary_crr As String = "select ROW_NUMBER() OVER (ORDER BY po_rcd_mat.crr_no) AS ROW_NO,('" & TextBox7.Text & " To " & TextBox8.Text & "') as ser_date, PO_RCD_MAT .MAT_CODE ,PO_RCD_MAT .CRR_NO ,PO_RCD_MAT .CRR_DATE ,SUPL .SUPL_NAME ,PO_RCD_MAT .CHLN_NO ,PO_RCD_MAT .CHLN_DATE ,PO_RCD_MAT .TRUCK_NO ,MATERIAL .MAT_NAME ,PO_RCD_MAT .MAT_CHALAN_QTY ,MAT_RCD_QTY ,PO_RCD_MAT .BE_NO ,PO_RCD_MAT .BE_DATE ,PO_RCD_MAT.BL_NO ,PO_RCD_MAT .BL_DATE  from PO_RCD_MAT join SUPL on PO_RCD_MAT .SUPL_ID =SUPL.SUPL_ID join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE where PO_RCD_MAT .CRR_NO  like 'scrr%' and PO_RCD_MAT .CRR_DATE between '" & s_date.Year & "-" & s_date.Month & "-" & s_date.Day & "' and '" & e_date.Year & "-" & e_date.Month & "-" & e_date.Day & "'"

        If DropDownList3.SelectedValue <> "All Group" And DropDownList4.SelectedValue <> "All Material" Then

            quary_crr = quary_crr & " and po_rcd_mat.mat_code ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList3.SelectedValue <> "All Group" And DropDownList4.SelectedValue = "All Material" Then

            quary_crr = quary_crr & " and po_rcd_mat.mat_code like '" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "%'"

        ElseIf DropDownList3.SelectedValue = "All Group" And DropDownList4.SelectedValue <> "All Material" Then

            quary_crr = quary_crr & " and po_rcd_mat.mat_code ='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1) & "'"

        ElseIf DropDownList3.SelectedValue = "All Group" And DropDownList4.SelectedValue = "All Material" Then

            quary_crr = quary_crr

        End If
        quary_crr = quary_crr & " order by po_rcd_mat.crr_no"

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary_crr, conn)
        da.Fill(dt)
        conn.Close()
        GridView4.DataSource = dt
        GridView4.DataBind()
    End Sub

    Protected Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If GridView4.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Store CRR")

        dt3.Columns.Add(New DataColumn("Sl No", GetType(String)))
        dt3.Columns.Add(New DataColumn("CRR No", GetType(String)))
        dt3.Columns.Add(New DataColumn("CRR Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Supl Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Chalan No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Chalan Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Truck No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Chln. Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Rcd. Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("BE No", GetType(String)))
        dt3.Columns.Add(New DataColumn("BE Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("BL No", GetType(String)))
        dt3.Columns.Add(New DataColumn("BL Date", GetType(String)))

        For Me.count = 0 To GridView4.Rows.Count - 1
            dt3.Rows.Add(GridView4.Rows(count).Cells(0).Text, GridView4.Rows(count).Cells(1).Text, GridView4.Rows(count).Cells(2).Text, GridView4.Rows(count).Cells(3).Text, GridView4.Rows(count).Cells(4).Text, GridView4.Rows(count).Cells(5).Text, GridView4.Rows(count).Cells(6).Text, GridView4.Rows(count).Cells(7).Text, GridView4.Rows(count).Cells(8).Text, CDec(GridView4.Rows(count).Cells(9).Text), CDec(GridView4.Rows(count).Cells(10).Text), GridView4.Rows(count).Cells(11).Text, GridView4.Rows(count).Cells(12).Text, GridView4.Rows(count).Cells(13).Text, GridView4.Rows(count).Cells(14).Text)
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Stores_CRR_Transaction.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim s_date, target_date As Date
        Dim duration As Int32
        duration = DropDownList10.SelectedValue
        s_date = CDate(TextBox19.Text)
        target_date = CDate(TextBox19.Text).AddYears(-1 * (duration))
        Dim dt As New DataTable()

        conn.Open()
        dt.Clear()
        'da = New SqlDataAdapter("select *,'" & s_date.Day & "-" & s_date.Month & "-" & s_date.Year & "' AS SEARCH_DATE from MATERIAL where MAT_CODE not like '100%' and LAST_TRANS_DATE < '" & target_date.Year & "-" & target_date.Month & "-" & target_date.Day & "' AND MAT_STOCK > 0 order by MAT_CODE", conn)
        'da = New SqlDataAdapter("select ROW_NUMBER() OVER (ORDER BY MAT_CODE) AS ROW_NO,*,'" & s_date.Day & "-" & s_date.Month & "-" & s_date.Year & "' AS SEARCH_DATE,(MAT_AVG*MAT_STOCK) AS MAT_VALUE, (SELECT dbo.fnc_FiscalYear(LAST_ISSUE_DATE)) As issue_fy from MATERIAL where MAT_CODE not like '100%' and LAST_ISSUE_DATE < '" & target_date.Year & "-" & target_date.Month & "-" & target_date.Day & "' AND MAT_STOCK > 0 order by MAT_CODE", conn)
        da = New SqlDataAdapter("DECLARE @Upto_date varchar(60) = '" & target_date.Year & "-" & target_date.Month & "-" & target_date.Day & "';
				DECLARE @TT TABLE(MAT_CODE VARCHAR(30),LINE_DATE Date,FISCAL_YEAR Varchar(20),LINE_NO Varchar(20),MAT_BALANCE DECIMAL(16,2),UNIT_PRICE DECIMAL(16,2),TOTAL_VALUE DECIMAL(16,2))

				INSERT INTO @TT
				SELECT E.MAT_CODE,E.LINE_DATE ,E.FISCAL_YEAR, E.LINE_NO, E.MAT_BALANCE,E.UNIT_PRICE,(E.MAT_BALANCE*E.UNIT_PRICE) AS TOTAL_VALUE FROM

				MAT_DETAILS E

				JOIN

				(
		
					SELECT T1.MAT_CODE, T1.MAX_YEAR, MAX(T2.LINE_NO) AS MAX_LINE_NO

					FROM
						(SELECT MAT_CODE, MAX(FISCAL_YEAR) AS MAX_YEAR FROM MAT_DETAILS where MAT_CODE not like '100%' AND LINE_DATE<=@Upto_date GROUP BY MAT_CODE) T1
					JOIN 
					MAT_DETAILS T2
				ON
		
					(T1.MAT_CODE = T2.MAT_CODE AND T1.MAX_YEAR = T2.FISCAL_YEAR) where LINE_DATE<=@Upto_date GROUP BY T1.MAT_CODE, T1.MAX_YEAR
				) R

				ON (E.MAT_CODE = R.MAT_CODE AND E.FISCAL_YEAR = R.MAX_YEAR AND E.LINE_NO = R.MAX_LINE_NO) WHERE E.MAT_BALANCE>0 and e.MAT_CODE not like '100%' AND E.LINE_DATE<=@Upto_date ORDER BY MAT_CODE


				INSERT INTO @TT
				SELECT E.MAT_CODE,'2016-04-01' As LINE_DATE ,'1617' as FISCAL_YEAR, '0' as LINE_NO, E.OPEN_STOCK As MAT_BALANCE,E.OPEN_AVG_PRICE As UNIT_PRICE,(E.OPEN_STOCK*E.OPEN_AVG_PRICE) AS TOTAL_VALUE FROM MATERIAL E where e.MAT_CODE not like '100%' and e.MAT_CODE not in (select distinct MAT_CODE from MAT_DETAILS where MAT_CODE not like '100%' and LINE_DATE<=@Upto_date) and e.OPEN_STOCK>0 ORDER BY MAT_CODE


				select ROW_NUMBER() OVER (ORDER BY t1.MAT_CODE) AS ROW_NO,t1.MAT_CODE,m1.MAT_NAME,m1.MAT_LASTPUR_DATE,m1.LAST_ISSUE_DATE,(SELECT dbo.fnc_FiscalYear(m1.LAST_ISSUE_DATE)) As ISSUE_FY,t1.MAT_BALANCE As MAT_STOCK,t1.UNIT_PRICE,(t1.MAT_BALANCE*t1.UNIT_PRICE) As Value, M1.PURPOSE, M1.REMARKS from @TT t1 join MATERIAL m1 on t1.MAT_CODE=m1.MAT_CODE where t1.MAT_BALANCE>0 and m1.LAST_ISSUE_DATE<@Upto_date order by t1.MAT_CODE", conn)
        da.Fill(dt)
        conn.Close()
        GridView5.DataSource = dt
        GridView5.DataBind()
    End Sub

    Protected Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If GridView5.Rows.Count < 0 Then
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
        dt3.Columns.Add(New DataColumn("Unit Price", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Purpose", GetType(String)))
        dt3.Columns.Add(New DataColumn("Remarks", GetType(String)))
        For Me.count = 0 To GridView5.Rows.Count - 1
            dt3.Rows.Add(GridView5.Rows(count).Cells(0).Text, GridView5.Rows(count).Cells(1).Text, GridView5.Rows(count).Cells(2).Text, CDate(GridView5.Rows(count).Cells(3).Text), CDate(GridView5.Rows(count).Cells(4).Text), GridView5.Rows(count).Cells(5).Text, CDec(GridView5.Rows(count).Cells(6).Text), CDec(GridView5.Rows(count).Cells(7).Text), CDec(GridView5.Rows(count).Cells(8).Text), GridView5.Rows(count).Cells(9).Text, GridView5.Rows(count).Cells(10).Text)
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

    Protected Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If GridView5.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(9) {New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_LASTPUR_DATE", GetType(String)), New DataColumn("LAST_ISSUE_DATE", GetType(String)), New DataColumn("ISSUE_FY"), New DataColumn("MAT_STOCK", GetType(Decimal)), New DataColumn("UNIT_PRICE", GetType(Decimal)), New DataColumn("MAT_VALUE", GetType(Decimal)), New DataColumn("SEARCH_DATE"), New DataColumn("DURATION")})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView5.Rows.Count - 1
            dt2.Rows.Add(GridView5.Rows(i).Cells(1).Text, GridView5.Rows(i).Cells(2).Text, GridView5.Rows(i).Cells(3).Text, GridView5.Rows(i).Cells(4).Text, GridView5.Rows(i).Cells(5).Text, GridView5.Rows(i).Cells(6).Text, GridView5.Rows(i).Cells(7).Text, GridView5.Rows(i).Cells(8).Text, TextBox19.Text, DropDownList10.SelectedValue)
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

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        ddl_issue_no.Visible = True
        conn.Open()
        da = New SqlDataAdapter("select distinct issue_no from  mat_details where issue_no like'SI%' and fiscal_year='" & DropDownList5.SelectedValue & "' ORDER BY issue_no", conn)
        da.Fill(ds, "mat_details")
        ddl_issue_no.Items.Clear()
        ddl_issue_no.DataSource = ds.Tables("mat_details")
        ddl_issue_no.DataValueField = "issue_no"
        ddl_issue_no.DataBind()
        conn.Close()
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        If DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            Return
        ElseIf DropDownList6.SelectedValue = "All Material" And DropDownList9.SelectedValue = "Stock Position" Then
            REPORTDropDownList3.Visible = True
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL where MAT_CODE not like '100%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()

            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All")
            REPORTDropDownList3.SelectedValue = "Select"
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "Date"
            Label440.Text = "And"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
        ElseIf DropDownList6.SelectedValue <> "All Material" And DropDownList9.SelectedValue = "Stock Position" Then
            REPORTDropDownList3.Visible = True
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("select ( MAT_CODE + ' , ' + MAT_NAME) AS MAT_DETAIL from MATERIAL WHERE MAT_CODE LIKE '" & DropDownList6.Text.Substring(0, DropDownList6.Text.IndexOf(",") - 1) & "%' order by MAT_CODE", conn)
            da.Fill(dt)
            REPORTDropDownList3.Items.Clear()
            REPORTDropDownList3.DataSource = dt
            REPORTDropDownList3.DataValueField = "MAT_DETAIL"
            REPORTDropDownList3.DataBind()
            conn.Close()
            REPORTDropDownList3.Items.Insert(0, "Select")
            REPORTDropDownList3.Items.Insert(1, "All")
            REPORTDropDownList3.SelectedValue = "Select"
            Label439.Visible = True
            Label440.Visible = True
            Label439.Text = "Date"
            Label440.Text = "And"
            REPORTTextBox1.Visible = True
            REPORTTextBox2.Visible = True
        End If
    End Sub

    Protected Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        Dim target_date As Date

        target_date = CDate(TextBox11.Text)
        Dim dt As New DataTable()
        conn.Open()
        dt.Clear()

        da = New SqlDataAdapter("DECLARE @Upto_date varchar(60) = '" & target_date.Year & "-" & target_date.Month & "-" & target_date.Day & "';
				DECLARE @TT TABLE(MAT_CODE VARCHAR(30),LINE_DATE Date,FISCAL_YEAR Varchar(20),LINE_NO Varchar(20),MAT_BALANCE Varchar(20),UNIT_PRICE DECIMAL(16,2),TOTAL_VALUE DECIMAL(16,2))

                INSERT INTO @TT
                SELECT E.MAT_CODE,E.LINE_DATE ,E.FISCAL_YEAR, E.LINE_NO, E.MAT_BALANCE,E.AVG_PRICE as UNIT_PRICE,(E.MAT_BALANCE*E.AVG_PRICE) AS TOTAL_VALUE FROM

                MAT_DETAILS E

                JOIN

                (		
                    SELECT T1.MAT_CODE, T1.MAX_YEAR, MAX(T2.LINE_NO) AS MAX_LINE_NO

                    FROM
                    (SELECT MAT_CODE, MAX(FISCAL_YEAR) AS MAX_YEAR FROM MAT_DETAILS where MAT_CODE not like '100%' AND LINE_DATE<=@Upto_date GROUP BY MAT_CODE) T1
                    JOIN 
                    MAT_DETAILS T2
                    ON
		
                (T1.MAT_CODE = T2.MAT_CODE AND T1.MAX_YEAR = T2.FISCAL_YEAR)
		        where LINE_DATE<=@Upto_date
                GROUP BY T1.MAT_CODE, T1.MAX_YEAR
                ) R

                ON (E.MAT_CODE = R.MAT_CODE AND E.FISCAL_YEAR = R.MAX_YEAR AND E.LINE_NO = R.MAX_LINE_NO) WHERE E.MAT_BALANCE>0 and e.MAT_CODE not like '100%' AND E.LINE_DATE<=@Upto_date ORDER BY MAT_CODE


                INSERT INTO @TT
                SELECT E.MAT_CODE,'2016-04-01' As LINE_DATE ,'1617' as FISCAL_YEAR, '0' as LINE_NO, E.OPEN_STOCK As MAT_BALANCE,E.OPEN_AVG_PRICE As UNIT_PRICE,(E.OPEN_STOCK*E.OPEN_AVG_PRICE) AS TOTAL_VALUE FROM MATERIAL E where e.MAT_CODE not like '100%' and e.MAT_CODE not in (select distinct MAT_CODE from MAT_DETAILS where MAT_CODE not like '100%' and LINE_DATE<=@Upto_date) and e.OPEN_STOCK>0 ORDER BY MAT_CODE
                select ROW_NUMBER() OVER (ORDER BY t1.MAT_CODE) AS ROW_NO,T1.MAT_CODE,M1.MAT_NAME,T1.LINE_NO,T1.LINE_DATE,T1.FISCAL_YEAR,T1.MAT_BALANCE,T1.UNIT_PRICE,T1.TOTAL_VALUE from @TT T1 JOIN MATERIAL M1 ON T1.MAT_CODE=M1.MAT_CODE WHERE T1.MAT_CODE<'090000001' order by T1.MAT_CODE", conn)
        da.Fill(dt)
        conn.Close()
        GridView7.DataSource = dt
        GridView7.DataBind()
    End Sub

    Protected Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If GridView7.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Store Inventory Detail")
        dt3.Columns.Add(New DataColumn("Sl No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Line No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Line Date", GetType(Date)))
        dt3.Columns.Add(New DataColumn("Fiscal Year", GetType(String)))
        dt3.Columns.Add(New DataColumn("Stock", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Unit Price", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Total Value", GetType(Decimal)))


        For Me.count = 0 To GridView7.Rows.Count - 1
            dt3.Rows.Add(GridView7.Rows(count).Cells(0).Text, GridView7.Rows(count).Cells(1).Text, GridView7.Rows(count).Cells(2).Text, GridView7.Rows(count).Cells(3).Text, CDate(GridView7.Rows(count).Cells(4).Text), GridView7.Rows(count).Cells(5).Text, CDec(GridView7.Rows(count).Cells(6).Text), CDec(GridView7.Rows(count).Cells(7).Text), CDec(GridView7.Rows(count).Cells(8).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=StoreInventoryDetail_" & CDate(TextBox11.Text).Day & "-" & CDate(TextBox11.Text).Month & "-" & CDate(TextBox11.Text).Year & ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub
End Class