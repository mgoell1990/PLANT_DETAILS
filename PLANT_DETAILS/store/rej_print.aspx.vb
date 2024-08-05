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
Public Class rej_print
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

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("storeAccess")) Or Session("storeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("SELECT PO_RCD_MAT.RET_STATUS,PO_RCD_MAT.CRR_NO,PO_RCD_MAT.PO_NO,PO_RCD_MAT.MAT_CODE,MATERIAL.MAT_NAME,PO_RCD_MAT.MAT_SLNO,PO_RCD_MAT.MAT_CHALAN_QTY,PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_REJ_QTY ,(PO_RCD_MAT.MAT_RCD_QTY-PO_RCD_MAT.MAT_REJ_QTY) AS ACCPT_QTY FROM PO_RCD_MAT JOIN MATERIAL ON PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE WHERE PO_RCD_MAT .CRR_NO ='" & TextBox1.Text.Substring(0, TextBox1.Text.IndexOf(",") - 1) & "' AND RET_STATUS IS NOT NULL AND MAT_REJ_QTY >0 ORDER BY PO_RCD_MAT.RET_STATUS", conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub
   

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim name As String = ""
        'Dim QUARY As String = "select * from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO where PO_RCD_MAT .RET_STATUS = '"
        'For Each row As GridViewRow In GridView1.Rows
        '    If row.RowType = DataControlRowType.DataRow Then
        '        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
        '        If chkRow.Checked Then

        '            If name = "" Then
        '                name = row.Cells(1).Text & "'"
        '            Else
        '                name = name & " or RET_STATUS = '" & row.Cells(1).Text & "'"
        '            End If

        '        Else
        '            Label3.Text = "Please Select Min One Record"
        '        End If
        '    End If
        'Next
        'If name <> "" Then
        '    conn.Open()
        '    Dim crystalReport As New ReportDocument
        '    Dim dt As New DataTable
        '    Dim PO_QUARY As String = QUARY & name
        '    da = New SqlDataAdapter(PO_QUARY, conn)
        '    da.Fill(dt)
        '    conn.Close()
        '    crystalReport.Load(Server.MapPath("~/print_rpt/rej_return.rpt"))
        '    crystalReport.SetDataSource(dt)
        '    crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
        '    Dim url As String = "REPORT.aspx"
        '    Dim sb As New StringBuilder()
        '    sb.Append("<script type = 'text/javascript'>")
        '    sb.Append("window.open('")
        '    sb.Append(url)
        '    sb.Append("');")
        '    sb.Append("</script>")
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
        '    crystalReport.Close()
        '    crystalReport.Dispose()
        '    Label3.Text = ""
        'Else
        '    Label3.Text = "Please Select Min One Record"
        'End If

        Dim rejectionList As String = ""
        Dim rejectionFlag As Boolean = False
        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                If chkRow.Checked Then
                    rejectionFlag = True

                    If rejectionList = "" Then
                        rejectionList = "RET_STATUS = '" & row.Cells(1).Text & "'"
                    Else
                        rejectionList = rejectionList & " or RET_STATUS = '" & row.Cells(1).Text & "'"
                    End If

                Else
                    Label3.Text = "Please Select Min One Record"
                End If
            End If
        Next

        If (rejectionFlag) Then
            Dim QUARY As String = "DECLARE @TT TABLE(PO_NO VARCHAR(30),MAT_SLNO VARCHAR(30),MAT_QTY VARCHAR(30))
                    INSERT INTO @TT
                    SELECT MAX(PO_NO) AS PO_NO,MAX(MAT_SLNO) AS MAT_SLNO,SUM(MAT_QTY) AS MAT_QTY FROM PO_ORD_MAT WHERE PO_NO IN (SELECT PO_NO FROM PO_RCD_MAT WHERE (" & rejectionList & ")) and MAT_SLNO in (SELECT MAT_SLNO FROM PO_RCD_MAT WHERE (" & rejectionList & ")) GROUP BY MAT_SLNO

                    select SO_ACTUAL,PO_RCD_MAT.MAT_SLNO , (PO_RCD_MAT .CRR_NO) AS CRR_NO ,(PO_RCD_MAT .CRR_DATE) AS CRR_DATE ,(PO_RCD_MAT .PO_NO) AS PO_NO ,(ORDER_DETAILS .SO_ACTUAL_DATE) AS SO_DATE ,(PO_RCD_MAT .SUPL_ID) AS SUPL_ID ,(PO_RCD_MAT .TRANS_WO_NO) AS TRANS_WO_NO ,(PO_RCD_MAT .TRUCK_NO) AS TRUCK_NO ,(PO_RCD_MAT .MAT_CODE) AS MAT_CODE , 
                    (PO_RCD_MAT .CHLN_NO) AS CHLN_NO ,(PO_RCD_MAT .CHLN_DATE) AS CHLN_DATE ,(PO_RCD_MAT .MAT_CHALAN_QTY) AS MAT_CHALAN_QTY ,(PO_RCD_MAT .MAT_RCD_QTY) AS MAT_RCD_QTY ,(PO_RCD_MAT .MAT_REJ_QTY) AS MAT_REJ_QTY ,(PO_RCD_MAT .MAT_BAL_QTY) AS MAT_BAL_QTY ,(PO_RCD_MAT .MAT_EXCE) AS MAT_EXCE ,(PO_RCD_MAT .RET_STATUS) RET_STATUS ,(PO_RCD_MAT .GARN_DATE) AS GARN_DATE ,(PO_RCD_MAT .CRR_EMP) AS CRR_EMP , 
                    (PO_RCD_MAT .INSP_EMP ) AS INSP_EMP , (PO_RCD_MAT .INSP_NOTE ) AS INSP_NOTE , (PO_RCD_MAT .MAT_RATE) AS MAT_RATE , (PO_RCD_MAT .GARN_NOTE ) AS GARN_NOTE ,(MATERIAL .MAT_NAME ) AS MAT_NAME , (MATERIAL.MAT_AU ) AS MAT_AU , (SUPL.SUPL_NAME ) AS SUPL_NAME , (SUPL.SUPL_AT ) AS SUPL_AT , 
                    (SUPL.SUPL_PO ) AS SUPL_PO , (SUPL.SUPL_DIST ) AS SUPL_DIST , (SUPL.SUPL_STATE ) AS SUPL_STATE , (SUPL.SUPL_COUNTRY ) AS SUPL_COUNTRY , (ORDER_DETAILS .SO_TYPE ) AS SO_TYPE , (ORDER_DETAILS .PO_TYPE ) AS PO_TYPE , T1 .MAT_QTY AS MAT_QTY , (ORDER_DETAILS .PAYMENT_MODE ) AS PAYMENT_MODE , (ORDER_DETAILS .MODE_OF_DESPATCH ) AS MODE_OF_DESPATCH , 
                    (ORDER_DETAILS .INDENTOR ) AS INDENTOR , (MAT_DETAILS .LINE_NO ) AS LINE_NO , (MAT_DETAILS .MAT_BALANCE ) AS MAT_BALANCE , (MAT_DETAILS .UNIT_PRICE ) AS UNIT_PRICE , (MAT_DETAILS .TOTAL_PRICE ) AS TOTAL_PRICE , (PO_RCD_MAT .RET_USER ) AS RET_USER , (PO_RCD_MAT .INSP_DATE ) AS INSP_DATE , (ORDER_DETAILS .SO_ACTUAL_DATE ) AS SO_ACTUAL_DATE , (PO_RCD_MAT .GARN_NO ) AS GARN_NO 
                    from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join @TT T1 on PO_RCD_MAT .PO_NO =T1 .PO_NO and PO_RCD_MAT .MAT_SLNO =T1 .MAT_SLNO 
                    LEFT join MAT_DETAILS ON PO_RCD_MAT.GARN_NO=MAT_DETAILS.ISSUE_NO and MAT_DETAILS .MAT_CODE =PO_RCD_MAT .MAT_CODE and MAT_DETAILS .MAT_SL_NO =PO_RCD_MAT .MAT_SLNO where (" & rejectionList & ") ORDER BY PO_RCD_MAT.MAT_SLNO"

            conn.Open()
            Dim crystalReport As New ReportDocument
            Dim dt As New DataTable
            da = New SqlDataAdapter(QUARY, conn)
            da.Fill(dt)
            conn.Close()
            crystalReport.Load(Server.MapPath("~/print_rpt/rej_return.rpt"))
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
            Label3.Text = ""

        End If

    End Sub
End Class