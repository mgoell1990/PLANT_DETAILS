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
Public Class p_report
    Inherits System.Web.UI.Page
    ''Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
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

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If date1.Text = "" Then
            date1.Focus()
            Return
        ElseIf IsDate(date1.Text) = False Then
            date1.Focus()
            Return
        ElseIf date2.Text = "" Then
            date2.Focus()
            Return
        ElseIf IsDate(date2.Text) = False Then
        date2.Focus()
        Return
        End If
        Dim dpr_date, dpr_date2 As Date
        dpr_date = CDate(date1.Text)
        dpr_date2 = CDate(date2.Text)
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        Dim PO_QUARY As String = "select distinct PROD_CONTROL .ITEM_CODE,qual_group.qual_name ,F_ITEM .ITEM_NAME ,F_ITEM .ITEM_AU , " & _
            " '" & date1.Text & "'  AS P_DATE , 'PRODUCTION & DESPATCH REPORT' AS R_TYPE, " _
            & " '" & date2.Text & "'  AS P_DATE_TO , " _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY))else '0.000' end) as F_QTY, " _
 & " CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY*F_ITEM .ITEM_WEIGHT)/1000)  as F_MT," _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_B_QTY))else '0.000' end) AS B_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_B_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS B_MT ," _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_I_QTY))else '0.000' end) AS INV_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_I_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS INV_MT" _
 & " from PROD_CONTROL join F_ITEM on PROD_CONTROL .ITEM_CODE =F_ITEM .ITEM_CODE" _
 & " join qual_group ON F_ITEM .ITEM_TYPE =qual_group .qual_code" _
 & " where PROD_CONTROL .PROD_DATE between  '" & dpr_date.Year & "-" & dpr_date.Month & "-" & dpr_date.Day & "' and '" & dpr_date2.Year & "-" & dpr_date2.Month & "-" & dpr_date2.Day & "' " _
 & " group by PROD_CONTROL .ITEM_CODE,F_ITEM .ITEM_NAME,qual_group.qual_name,F_ITEM .ITEM_AU"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.Load(Server.MapPath("~/print_rpt/pr_rpt.rpt"))
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
End Class