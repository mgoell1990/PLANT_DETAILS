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
Imports ZXing
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class print_invoice
    Inherits System.Web.UI.Page
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
        If Not IsPostBack Then
            RadioButtonList1.SelectedIndex = 0
        End If
    End Sub
    Protected Sub ORIGINAL(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim inv As String = btn.CommandName
        Dim cond As String = btn.Text
        ''PRINT
        PRINT(inv, "ORIGINAL FOR RECEIPIENT")
        ''UPDATE
        conn.Open()
        Dim QUARY1 As String = "update INV_PRINT set PRINT_ORIGN=@PRINT_ORIGN WHERE INV_NO ='" & inv & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn)
        cmd2.Parameters.AddWithValue("@PRINT_ORIGN", "")
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
        Dim origin, duplicate, triplicate As String
        origin = ""
        duplicate = ""
        triplicate = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from INV_PRINT WHERE INV_NO='" & inv & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            origin = dr.Item("PRINT_ORIGN")
            duplicate = dr.Item("PRINT_TRANS")
            triplicate = dr.Item("PRINT_ASSAE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If origin = "" And duplicate = "" And triplicate = "" Then
            ''DELETE
            conn.Open()
            mycommand = New SqlCommand("DELETE FROM INV_PRINT WHERE INV_NO ='" & inv & "'", conn)
            mycommand.ExecuteNonQuery()
            conn.Close()

        End If
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from INV_PRINT", conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
    End Sub
    Protected Sub DUPLICATE(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim inv As String = btn.CommandName
        Dim cond As String = btn.Text
        ''PRINT
        PRINT(inv, "DUPLICATE FOR TRANSPORTER")
        ''UPDATE
        conn.Open()
        Dim QUARY1 As String = "update INV_PRINT set PRINT_TRANS=@PRINT_TRANS WHERE INV_NO ='" & inv & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn)
        cmd2.Parameters.AddWithValue("@PRINT_TRANS", "")
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
        Dim origin, duplicate, triplicate As String
        origin = ""
        duplicate = ""
        triplicate = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from INV_PRINT WHERE INV_NO='" & inv & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            origin = dr.Item("PRINT_ORIGN")
            duplicate = dr.Item("PRINT_TRANS")
            triplicate = dr.Item("PRINT_ASSAE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If origin = "" And duplicate = "" And triplicate = "" Then
            ''DELETE
            conn.Open()
            mycommand = New SqlCommand("DELETE FROM INV_PRINT WHERE INV_NO ='" & inv & "'", conn)
            mycommand.ExecuteNonQuery()
            conn.Close()
        End If
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from INV_PRINT", conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()

    End Sub
    Protected Sub PRINT(INV_NO As String, INV_TYPE As String)
        Dim inv_for As String = ""
        Dim QRCodeData As String = ""
        Dim IRN_NO As String = ""


        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        Dim PO_QUARY As String = "SELECT (SELECT '" & INV_TYPE & "' AS INV_FOR) AS INV_FOR, * FROM DESPATCH where D_TYPE+ CONVERT(VARCHAR(15), INV_NO) ='" & INV_NO & "' AND FISCAL_YEAR=" & TextBox72.Text
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)

        conn.Open()
        mycommand.CommandText = "select * from DESPATCH WHERE D_TYPE+ CONVERT(VARCHAR(15), INV_NO)='" & INV_NO & "' AND FISCAL_YEAR=" & TextBox72.Text
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_for = dr.Item("INV_TYPE")
            If (IsDBNull(dr.Item("IRN_NO"))) Then
                IRN_NO = ""
            Else
                IRN_NO = dr.Item("IRN_NO")
                QRCodeData = dr.Item("QR_CODE")
                ''Add the Barcode column to the DataSet
                dt.Columns.Add(New DataColumn("QR_CODE_INPUT", GetType(Byte())))

                For Each dr As DataRow In dt.Rows

                    dr("QR_CODE_INPUT") = GetQRCodeData(QRCodeData)

                Next
            End If
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim INV_NAME As String = ""
        If inv_for = "Delivery Challan(Within same GSTIN)" Then
            'INV_NAME = "gst_invoice.rpt"
            INV_NAME = "gst_invoice_ti_new.rpt"
        Else
            If IRN_NO = "" Then
                'INV_NAME = "gst_invoice_ti.rpt"
                INV_NAME = "gst_invoice_ti_new.rpt"
            Else
                INV_NAME = "gst_invoice_ti_QRCode.rpt"
            End If

        End If

        crystalReport.Load(Server.MapPath("~/print_rpt/" & INV_NAME))
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
    Protected Sub TRIPLICATE(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim inv As String = btn.CommandName
        Dim cond As String = btn.Text
        ''PRINT
        PRINT(inv, "TRIPLICATE FOR SUPPLIER")
        ''UPDATE
        conn.Open()
        Dim QUARY1 As String = "update INV_PRINT set PRINT_ASSAE=@PRINT_ASSAE WHERE INV_NO ='" & inv & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn)
        cmd2.Parameters.AddWithValue("@PRINT_ASSAE", "")
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
        Dim origin, duplicate, triplicate As String
        origin = ""
        duplicate = ""
        triplicate = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from INV_PRINT WHERE INV_NO='" & inv & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            origin = dr.Item("PRINT_ORIGN")
            duplicate = dr.Item("PRINT_TRANS")
            triplicate = dr.Item("PRINT_ASSAE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If origin = "" And duplicate = "" And triplicate = "" Then
            ''DELETE
            conn.Open()
            mycommand = New SqlCommand("DELETE FROM INV_PRINT WHERE INV_NO ='" & inv & "'", conn)
            mycommand.ExecuteNonQuery()
            conn.Close()
        End If
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from INV_PRINT", conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
    End Sub

    Public Function GetQRCodeData(inputString As String)

        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE

        writer.Options.Height = 10
        writer.Options.Width = 10
        Dim result = writer.Write(inputString)
        Dim barcodeBitmap = New Bitmap(result)

        Dim bytes As Byte()
        Using memory As New MemoryStream()
            barcodeBitmap.Save(memory, ImageFormat.Jpeg)
            bytes = memory.ToArray()
        End Using

        Return bytes

    End Function

    Protected Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        If txtContactsSearch.Text = "" Then
            txtContactsSearch.Focus()
            Return
        ElseIf IsNumeric(TextBox72.Text) = False Then
            TextBox72.Focus()
            Return
        End If
        count = 0
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from DESPATCH WHERE D_TYPE+ CONVERT(VARCHAR(15), INV_NO)='" & txtContactsSearch.Text & "' AND FISCAL_YEAR=" & TextBox72.Text, conn)
        count = da.Fill(dt)
        conn.Close()
        If count = 0 Then
            txtContactsSearch.Focus()
            Return
        End If
        conn.Open()

        Dim crystalReport As New ReportDocument
        Dim dt2 As New DataTable
        Dim PO_QUARY As String = "select (SELECT 'EXTRA COPY' AS INV_FOR ) AS INV_FOR, * from DESPATCH where D_TYPE+ CONVERT(VARCHAR(15), INV_NO) ='" & txtContactsSearch.Text & "' and FISCAL_YEAR =" & TextBox72.Text
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt2)

        Dim inv_for As String = ""
        Dim IRN_NO As String = ""
        Dim QRCodeData As String = ""
        mycommand.CommandText = "select * from DESPATCH WHERE D_TYPE+ CONVERT(VARCHAR(15), INV_NO)='" & txtContactsSearch.Text & "' AND FISCAL_YEAR=" & TextBox72.Text
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_for = dr.Item("INV_TYPE")
            If (IsDBNull(dr.Item("QR_CODE"))) Then
                IRN_NO = ""
                QRCodeData = ""
            Else
                If (IsDBNull(dr.Item("IRN_NO"))) Then
                    IRN_NO = ""
                Else
                    IRN_NO = dr.Item("IRN_NO")
                End If

                QRCodeData = dr.Item("QR_CODE")
                ''Add the Barcode column to the DataSet
                dt2.Columns.Add(New DataColumn("QR_CODE_INPUT", GetType(Byte())))

                For Each dr As DataRow In dt2.Rows

                    dr("QR_CODE_INPUT") = GetQRCodeData(QRCodeData)

                Next
            End If

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim INV_NAME As String = ""
        If inv_for = "Delivery Challan(Within same GSTIN)" Then
            'INV_NAME = "gst_invoice.rpt"
            INV_NAME = "gst_invoice_ti_new.rpt"
        Else
            If QRCodeData = "" Then
                'INV_NAME = "gst_invoice_ti.rpt"
                INV_NAME = "gst_invoice_ti_new.rpt"
            Else
                INV_NAME = "gst_invoice_ti_QRCode.rpt"
            End If

        End If

        crystalReport.Load(Server.MapPath("~/print_rpt/" & INV_NAME))
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

    Protected Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If TextBox72.Text = "" Or IsNumeric(TextBox72.Text) = False Then
            TextBox72.Focus()
            Return
        End If
        Dim QUARY As String = ""


        If RadioButtonList1.SelectedIndex = 0 Then
            QUARY = "select * from INV_PRINT WHERE INV_NO LIKE 'OS15%' or INV_NO LIKE 'DC15%' ORDER BY INV_NO"
        Else
            QUARY = "select * from INV_PRINT WHERE INV_NO LIKE 'RC%' ORDER BY INV_NO"
        End If

        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(QUARY, conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
        GridView3.Visible = True
    End Sub


End Class