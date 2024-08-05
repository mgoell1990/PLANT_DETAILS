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
Imports CrystalDecisions.ReportSource
Public Class print_order
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

            ''search chptr code
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct order_type  from order_details order by order_type", conn)
            da.Fill(dt)
            DropDownList50.Items.Clear()
            DropDownList50.DataSource = dt
            DropDownList50.DataValueField = "order_type"
            DropDownList50.DataBind()
            conn.Close()
            DropDownList50.Items.Add("Select")
            DropDownList50.SelectedValue = "Select"
            ''search mat group
            ''Panel32.Visible = True
        End If

        'If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("purchaseAccess")) Or Session("purchaseAccess") = "")) Then

        '    Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        'End If
    End Sub

    Protected Sub Button62_Click(sender As Object, e As EventArgs) Handles Button62.Click
        If DropDownList49.SelectedValue = "Select" Then
            DropDownList49.Focus()
            Return
        ElseIf DropDownList50.SelectedValue = "Select" Then
            DropDownList50.Focus()
            Return
        End If
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        Dim PO_QUARY As String = "select * from ORDER_DETAILS join EmpLoginDetails on EmpLoginDetails .EMP_ID =ORDER_DETAILS .EMP_ID where ORDER_DETAILS.SO_NO ='" & DropDownList49.Text.Substring(0, DropDownList49.Text.IndexOf(",") - 1).Trim & "'"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        conn.Close()
        If DropDownList50.SelectedValue = "Sale Order" Then
            If Left(DropDownList49.SelectedValue, 3) = "S05" Then
                crystalReport.Load(Server.MapPath("~/print_rpt/sale_order1.rpt"))

            Else
                crystalReport.Load(Server.MapPath("~/print_rpt/sale_order_misc.rpt"))
            End If

        ElseIf DropDownList50.SelectedValue = "Purchase Order" Or DropDownList50.SelectedValue = "Rate Contract" Then
            crystalReport.Load(Server.MapPath("~/print_rpt/po_store.rpt"))

        ElseIf DropDownList50.SelectedValue = "Work Order" Then
            crystalReport.Load(Server.MapPath("~/print_rpt/w_order.rpt"))

        End If
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

    Protected Sub DropDownList50_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList50.SelectedIndexChanged
        If DropDownList50.SelectedValue = "Select" Then
            DropDownList50.Focus()
            Return
        End If

        ''ADD FISCAL YEAR IN DROPDOWNLIST
        conn.Open()
        da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
        da.Fill(ds, "FISCAL_YEAR")
        DropDownList1.DataSource = ds.Tables("FISCAL_YEAR")
        DropDownList1.DataValueField = "FY"
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, "Select")
        conn.Close()
    End Sub



    Private Function PO_QUARY() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()

            DropDownList49.Items.Clear()
            DropDownList49.DataBind()
            Return
        End If

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct (so_no +' , '+ so_actual) as so_no from order_details where order_type='" & DropDownList50.SelectedValue & "' and FINANCE_YEAR='" & DropDownList1.SelectedValue & "' order by so_no", conn)
        da.Fill(dt)
        DropDownList49.Items.Clear()
        DropDownList49.DataSource = dt
        DropDownList49.DataValueField = "so_no"
        DropDownList49.DataBind()
        conn.Close()
        DropDownList49.Items.Add("Select")
        DropDownList49.SelectedValue = "Select"
    End Sub


End Class