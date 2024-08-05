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
Public Class rm_rej_print
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim da1 As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

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
        Dim name As String = ""
        Dim name1 As String = ""
        'Dim QUARY As String = "select * from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO where PO_RCD_MAT .RET_STATUS = '"
        Dim QUARY As String = "select  MAX(ORDER_DETAILS. SO_DATE) AS SO_DATE ,MAX(PO_RCD_MAT . BE_NO) AS BE_NO ,PO_RCD_MAT . TRANS_SLNO ,PO_RCD_MAT . MAT_SLNO , MAX(PO_RCD_MAT .CRR_NO) AS CRR_NO ,MAX(PO_RCD_MAT .CRR_DATE) AS CRR_DATE ,MAX(PO_RCD_MAT .PO_NO) AS PO_NO ,MAX(ORDER_DETAILS .SO_ACTUAL_DATE) AS SO_ACTUAL_DATE ,MAX(PO_RCD_MAT .SUPL_ID) AS SUPL_ID ,MAX(PO_RCD_MAT .TRANS_WO_NO) AS TRANS_WO_NO ,MAX(PO_RCD_MAT .TRUCK_NO) AS TRUCK_NO ,MAX(PO_RCD_MAT .MAT_CODE) AS MAT_CODE , " &
            "MAX(PO_RCD_MAT .CHLN_NO) AS CHLN_NO ,MAX(PO_RCD_MAT .CHLN_DATE) AS CHLN_DATE ,MAX(PO_RCD_MAT .MAT_CHALAN_QTY) AS MAT_CHALAN_QTY ,MAX(PO_RCD_MAT .MAT_RCD_QTY) AS MAT_RCD_QTY ,MAX(PO_RCD_MAT .MAT_REJ_QTY) AS MAT_REJ_QTY ,MAX(PO_RCD_MAT .MAT_BAL_QTY) AS MAT_BAL_QTY ,MAX(PO_RCD_MAT .MAT_EXCE) AS MAT_EXCE ,MAX(PO_RCD_MAT .RET_STATUS) RET_STATUS ,MAX(PO_RCD_MAT .GARN_DATE) AS GARN_DATE ,MAX(PO_RCD_MAT .CRR_EMP) AS CRR_EMP , " &
            "MAX(PO_RCD_MAT .INSP_EMP ) AS INSP_EMP , MAX (PO_RCD_MAT .INSP_NOTE ) AS INSP_NOTE , MAX (PO_RCD_MAT .MAT_RATE) AS MAT_RATE , MAX (PO_RCD_MAT .GARN_NOTE ) AS GARN_NOTE , MAX (MATERIAL .MAT_NAME ) AS MAT_NAME , MAX (MATERIAL.MAT_AU ) AS MAT_AU , MAX (SUPL.SUPL_NAME ) AS SUPL_NAME , MAX (SUPL.SUPL_AT ) AS SUPL_AT , " &
            "MAX (SUPL.SUPL_PO ) AS SUPL_PO , MAX (SUPL.SUPL_DIST ) AS SUPL_DIST , MAX (SUPL.SUPL_STATE ) AS SUPL_STATE , MAX (SUPL.SUPL_COUNTRY ) AS SUPL_COUNTRY , MAX (ORDER_DETAILS .SO_TYPE ) AS SO_TYPE , MAX (ORDER_DETAILS .PO_TYPE ) AS PO_TYPE , SUM (PO_ORD_MAT .MAT_QTY ) AS MAT_QTY , MAX (ORDER_DETAILS .PAYMENT_MODE ) AS PAYMENT_MODE , MAX (ORDER_DETAILS .MODE_OF_DESPATCH ) AS MODE_OF_DESPATCH , " &
            "MAX (ORDER_DETAILS .INDENTOR ) AS INDENTOR , MAX (PO_RCD_MAT .RET_USER ) AS RET_USER , MAX (PO_RCD_MAT .INSP_DATE ) AS INSP_DATE , MAX (ORDER_DETAILS .SO_ACTUAL ) AS SO_ACTUAL , MAX (PO_RCD_MAT .GARN_NO ) AS GARN_NO " &
            "from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO  " &
            " where PO_RCD_MAT .RET_STATUS ='"


        For Each row As GridViewRow In GridView1.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                If chkRow.Checked Then

                    If name = "" Then
                        name = row.Cells(1).Text & "'"
                    Else
                        name = name & " or RET_STATUS = '" & row.Cells(1).Text & "'"
                    End If

                End If
            End If
        Next

        If name <> "" Then
            conn.Open()
            Dim crystalReport As New ReportDocument
            Dim dt As New DataTable
            Dim PO_QUARY As String = QUARY & name & " GROUP BY PO_RCD_MAT .MAT_SLNO,PO_RCD_MAT . TRANS_SLNO " & " ORDER BY PO_RCD_MAT.MAT_SLNO"
            da = New SqlDataAdapter(PO_QUARY, conn)
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

        Else
            Label3.Text = "Please Select Min One Record"
        End If

    End Sub
End Class