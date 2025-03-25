Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.IO
Imports ClosedXML.Excel



Public Class report3
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt, dTable As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("userName") = "" Then
            '    '' Response.Redirect("~/Account/Login")
            '    '' Return
            'End If
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim quary As String = ""
        Dim quary1 As String = ""
        Dim TERM As String = ""
        Dim po_no As String = ""
        Dim po_type As String = ""
        Dim pay_type As String = ""
        Dim dt1 As New DataTable
        Dim crystalReport As New ReportDocument
        quary = "select * from voucher where token_no=" & TextBox1.Text
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = quary
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            TERM = dr.Item("voucher_type")
            pay_type = dr.Item("PAY_TYPE")

            If (pay_type = "Current") Then
                po_no = dr.Item("PO_NO")
            End If

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        '''''''''''''''''''''
        conn.Open()
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt1)
        conn.Close()
        Dim mc2 As New SqlCommand
        quary1 = "select PO_TYPE from ORDER_DETAILS WHERE SO_NO='" & po_no & "'"
        conn.Open()
        'Dim mc1 As New SqlCommand
        mc2.CommandText = quary1
        mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            po_type = dr.Item("PO_TYPE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If (po_type = "FREIGHT INWARD" Or po_type = "FREIGHT OUTWARD") Then

            If TERM = "" Then
                TextBox1.Text = ""
                TextBox1.Focus()
                Return
            End If
            quary1 = "select SUPL_ID,'N/A' as GARN_NO_MB_NO,AC_NO,ac_description, sum(AMOUNT_DR) AS AMOUNT_DR, sum(AMOUNT_CR) AS AMOUNT_CR, VOUCHER_NO from LEDGER join ACDIC on LEDGER.AC_NO=ACDIC.ac_code where VOUCHER_NO='" & TextBox1.Text & "' and GARN_NO_MB_NO <> 'BANK' group by AC_NO, SUPL_ID, ac_description, JURNAL_LINE_NO, VOUCHER_NO order by JURNAL_LINE_NO"
            If TERM = "C.P.V" Or TERM = "B.P.V" Or TERM = "RCD" Or TERM = "C.B.V" Then
                crystalReport.Load(Server.MapPath("~/print_rpt/voucher_transporter1.rpt"))
            Else
                crystalReport.Load(Server.MapPath("~/print_rpt/je_pass.rpt"))
            End If
            conn.Open()
            Dim dt As New DataTable
            da = New SqlDataAdapter(quary1, conn)
            da.Fill(dt)
            conn.Close()
            crystalReport.SetDataSource(dt)
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
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


        Else
            If TERM = "" Then
                TextBox1.Text = ""
                TextBox1.Focus()
                Return
            End If
            If TERM = "C.P.V" Or TERM = "B.P.V" Or TERM = "RCD" Or TERM = "C.B.V" Then
                crystalReport.Load(Server.MapPath("~/print_rpt/bill_pass.rpt"))
            Else
                crystalReport.Load(Server.MapPath("~/print_rpt/je_pass.rpt"))
            End If
            conn.Open()
            Dim dt As New DataTable
            da = New SqlDataAdapter(quary, conn)
            da.Fill(dt)
            conn.Close()
            crystalReport.SetDataSource(dt)
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
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

    Protected Sub Button61_Click(sender As Object, e As EventArgs) Handles Button61.Click
        Dim from_date, to_date As Date
        from_date = CDate(TextBox33.Text)
        to_date = CDate(TextBox34.Text)
        conn.Open()
        dt.Clear()
        'da = New SqlDataAdapter("select inv_data .bill_id ,inv_data .post_date ,inv_data .inv_no ,inv_data .inv_date ,inv_data .inv_amount ,inv_data .emp_id ,inv_data .po_no ,SUPL .SUPL_NAME ,ORDER_DETAILS .PO_TYPE  from inv_data join ORDER_DETAILS ON inv_data .po_no =ORDER_DETAILS .SO_NO JOIN SUPL ON ORDER_DETAILS .PARTY_CODE =SUPL .SUPL_ID WHERE inv_data .v_ind IS NULL and inv_data .post_date between ' " & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & " ' ORDER BY bill_id", conn)
        da = New SqlDataAdapter("select inv_data .bill_id ,inv_data .post_date ,inv_data .inv_no ,inv_data .inv_date ,inv_data .inv_amount ,inv_data .emp_id ,inv_data .po_no ,SUPL .SUPL_NAME ,ORDER_DETAILS .PO_TYPE from inv_data LEFT join ORDER_DETAILS ON inv_data .po_no =ORDER_DETAILS .SO_NO LEFT JOIN SUPL ON ORDER_DETAILS .PARTY_CODE =SUPL .SUPL_ID WHERE inv_data .inv_date between ' " & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND ORDER_TO='Other' ORDER BY bill_id", conn)
        da.Fill(dt)
        conn.Close()
        GridView5.DataSource = dt
        GridView5.DataBind()

        Dim I As Integer = 0
        For I = 0 To GridView5.Rows.Count - 1
            Dim ORDER_TYPE, PO_TYPE, GARN_MB_DATE, PAYMENT_DATE, CHEQUE_NO As New String("")
            ''Getting last GARN/MB Date

            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "SELECT ORDER_TYPE,PO_TYPE FROM ORDER_DETAILS WHERE SO_NO ='" & GridView5.Rows(I).Cells(2).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                ORDER_TYPE = dr.Item("ORDER_TYPE")
                PO_TYPE = dr.Item("PO_TYPE")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            ''Checking type of order
            If (ORDER_TYPE = "Purchase Order") Then

                conn.Open()
                mc1.CommandText = "Select FORMAT(max(GARN_DATE),'dd-MM-yyyy') As GARN_DATE from PO_RCD_MAT where PO_NO='" & GridView5.Rows(I).Cells(2).Text & "' and INV_NO='" & GridView5.Rows(I).Cells(5).Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows = True Then
                    dr.Read()
                    If IsDBNull(dr.Item("GARN_DATE")) Then
                        GARN_MB_DATE = ""
                    Else
                        GARN_MB_DATE = dr.Item("GARN_DATE")
                    End If

                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

                GridView5.Rows(I).Cells(7).Text = GARN_MB_DATE
            ElseIf (ORDER_TYPE = "Work Order") Then

                conn.Open()
                mc1.CommandText = "select FORMAT(max(mb_date),'dd-MM-yyyy') As GARN_DATE from mb_book where PO_NO='" & GridView5.Rows(I).Cells(2).Text & "' and INV_NO='" & GridView5.Rows(I).Cells(5).Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows = True Then
                    dr.Read()
                    If IsDBNull(dr.Item("GARN_DATE")) Then
                        GARN_MB_DATE = ""
                    Else
                        GARN_MB_DATE = dr.Item("GARN_DATE")
                    End If
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

                GridView5.Rows(I).Cells(7).Text = GARN_MB_DATE
            ElseIf (ORDER_TYPE = "Rate Contract") Then
                If (PO_TYPE = "STORE MATERIAL" Or PO_TYPE = "STORE MATERIAL(IMP)" Or PO_TYPE = "RAW MATERIAL" Or PO_TYPE = "RAW MATERIAL(IMP)" Or PO_TYPE = "COAL PURCHASE") Then
                    conn.Open()
                    mc1.CommandText = "Select FORMAT(max(GARN_DATE),'dd-MM-yyyy') As GARN_DATE from PO_RCD_MAT where PO_NO='" & GridView5.Rows(I).Cells(2).Text & "' and INV_NO='" & GridView5.Rows(I).Cells(5).Text & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows = True Then
                        dr.Read()
                        If IsDBNull(dr.Item("GARN_DATE")) Then
                            GARN_MB_DATE = ""
                        Else
                            GARN_MB_DATE = dr.Item("GARN_DATE")
                        End If
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()

                    GridView5.Rows(I).Cells(7).Text = GARN_MB_DATE
                Else
                    conn.Open()
                    mc1.CommandText = "select FORMAT(max(mb_date),'dd-MM-yyyy') As GARN_DATE from mb_book where PO_NO='" & GridView5.Rows(I).Cells(2).Text & "' and INV_NO='" & GridView5.Rows(I).Cells(5).Text & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows = True Then
                        dr.Read()
                        If IsDBNull(dr.Item("GARN_DATE")) Then
                            GARN_MB_DATE = ""
                        Else
                            GARN_MB_DATE = dr.Item("GARN_DATE")
                        End If
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()

                    GridView5.Rows(I).Cells(7).Text = GARN_MB_DATE
                End If
            End If

            conn.Open()
            mc1.CommandText = "SELECT convert(varchar(30),CONVERT(varchar,CHEQUE_DATE,105) , 105) As PAYMENT_DATE, CHEQUE_NO FROM VOUCHER WHERE BILL_TRACK='" & GridView5.Rows(I).Cells(0).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr.Item("PAYMENT_DATE")) Then
                    PAYMENT_DATE = ""
                    CHEQUE_NO = ""
                Else
                    PAYMENT_DATE = dr.Item("PAYMENT_DATE")
                    CHEQUE_NO = dr.Item("CHEQUE_NO")
                End If

                dr.Close()
            Else
                PAYMENT_DATE = ""
                CHEQUE_NO = ""
                dr.Close()
            End If
            conn.Close()

            GridView5.Rows(I).Cells(8).Text = PAYMENT_DATE
            GridView5.Rows(I).Cells(9).Text = CHEQUE_NO
        Next
    End Sub

    Protected Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        If GridView5.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(10) {New DataColumn("bill_id"), New DataColumn("post_date"), New DataColumn("po_no"), New DataColumn("PO_TYPE"), New DataColumn("SUPL_NAME"), New DataColumn("inv_no"), New DataColumn("inv_date"), New DataColumn("GARN_MB_DATE"), New DataColumn("PAYMENT_DATE"), New DataColumn("CHEQUE_NO"), New DataColumn("inv_amount", GetType(Decimal))})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView5.Rows.Count - 1
            dt2.Rows.Add(GridView5.Rows(i).Cells(0).Text, GridView5.Rows(i).Cells(1).Text, GridView5.Rows(i).Cells(2).Text, GridView5.Rows(i).Cells(3).Text, GridView5.Rows(i).Cells(4).Text, GridView5.Rows(i).Cells(5).Text, GridView5.Rows(i).Cells(6).Text, GridView5.Rows(i).Cells(7).Text, GridView5.Rows(i).Cells(8).Text, GridView5.Rows(i).Cells(9).Text, CDec(GridView5.Rows(i).Cells(10).Text))
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/bill_track.rpt"))
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

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            MultiView2.ActiveViewIndex = -1
            Return

        ElseIf DropDownList9.SelectedValue = "Adv. Voucher" Then

            MultiView2.ActiveViewIndex = 0

        ElseIf DropDownList9.SelectedValue = "Bank Book" Then

            MultiView2.ActiveViewIndex = 1

        ElseIf DropDownList9.SelectedValue = "Bill Forwarding Memo" Then

            MultiView2.ActiveViewIndex = 2

        ElseIf DropDownList9.SelectedValue = "Bank Gaurantee" Then

            MultiView2.ActiveViewIndex = 3

        ElseIf DropDownList9.SelectedValue = "Bill Track" Then

            MultiView2.ActiveViewIndex = 4

        ElseIf DropDownList9.SelectedValue = "General Ledger" Then

            MultiView2.ActiveViewIndex = 5

            conn.Open()
            Dim ds5 As New DataSet
            da = New SqlDataAdapter("select distinct (ac_code + ' , ' + ac_description ) as ac_code from ACDIC  ORDER BY AC_CODE", conn)
            da.Fill(ds5, "ACDIC")
            conn.Close()
            DropDownList2.DataSource = ds5.Tables("ACDIC")
            DropDownList2.DataValueField = "AC_CODE"
            DropDownList2.DataBind()
            ds5.Tables.Clear()
            DropDownList2.Items.Insert(0, "Select")
            DropDownList2.Items.Insert(1, "All")
            DropDownList2.SelectedValue = "Select"

        ElseIf DropDownList9.SelectedValue = "ITC" Then

            MultiView2.ActiveViewIndex = 6

        ElseIf DropDownList9.SelectedValue = "JE Entry" Then

            MultiView2.ActiveViewIndex = 7

        ElseIf DropDownList9.SelectedValue = "Link Sheet" Then

            MultiView2.ActiveViewIndex = 8

        ElseIf DropDownList9.SelectedValue = "Party Ledger" Then

            MultiView2.ActiveViewIndex = 9

            'retrieve account codes
            conn.Open()
            Dim ds5 As New DataSet
            da = New SqlDataAdapter("select distinct (ac_code + ' , ' + ac_description ) as ac_code from ACDIC  ORDER BY AC_CODE", conn)
            da.Fill(ds5, "ACDIC")
            conn.Close()
            DropDownList1.DataSource = ds5.Tables("ACDIC")
            DropDownList1.DataValueField = "AC_CODE"
            DropDownList1.DataBind()
            ds5.Tables.Clear()
            DropDownList1.Items.Insert(0, "All")
            ds5.Clear()

            'retrieve party codes
            conn.Open()
            da = New SqlDataAdapter("select distinct (supl_id + ' , ' + supl_name ) as supl_details from SUPL UNION select distinct (d_code + ' , ' + d_name ) as supl_details from dater ORDER BY supl_details", conn)
            da.Fill(ds5, "ACDIC")
            conn.Close()
            DropDownList3.DataSource = ds5.Tables("ACDIC")
            DropDownList3.DataValueField = "supl_details"
            DropDownList3.DataBind()
            ds5.Tables.Clear()
            DropDownList3.Items.Insert(0, "All")
            ds5.Clear()

        ElseIf DropDownList9.SelectedValue = "Pending RCM" Then

            MultiView2.ActiveViewIndex = 10

        ElseIf DropDownList9.SelectedValue = "RCM Tax Invoice" Then

            MultiView2.ActiveViewIndex = 11

        ElseIf DropDownList9.SelectedValue = "RIOC" Then

            MultiView2.ActiveViewIndex = 12

        ElseIf DropDownList9.SelectedValue = "Shedule" Then

            MultiView2.ActiveViewIndex = 13

        ElseIf DropDownList9.SelectedValue = "Trial Report" Then

            MultiView2.ActiveViewIndex = 14

        ElseIf DropDownList9.SelectedValue = "Voucher" Then

            MultiView2.ActiveViewIndex = 15

        ElseIf DropDownList9.SelectedValue = "Ledger Entry" Then

            MultiView2.ActiveViewIndex = 16

        ElseIf DropDownList9.SelectedValue = "AGING" Then

            MultiView2.ActiveViewIndex = 17

            conn.Open()
            Dim ds5 As New DataSet
            da = New SqlDataAdapter("select distinct (ac_code + ' , ' + ac_description ) as ac_code from ACDIC  ORDER BY AC_CODE", conn)
            da.Fill(ds5, "ACDIC")
            conn.Close()
            DropDownList4.DataSource = ds5.Tables("ACDIC")
            DropDownList4.DataValueField = "AC_CODE"
            DropDownList4.DataBind()
            ds5.Tables.Clear()
            DropDownList4.Items.Insert(0, "Select")
            DropDownList4.Items.Insert(1, "All")
            DropDownList4.SelectedValue = "Select"

        ElseIf DropDownList9.SelectedValue = "Trial Report(Merged A/c Code)" Then

            MultiView2.ActiveViewIndex = 18
        ElseIf DropDownList9.SelectedValue = "Pending GARN" Then

            MultiView2.ActiveViewIndex = 19
            TextBox28.Text = ""
            TextBox29.Text = ""
        ElseIf DropDownList9.SelectedValue = "Asset Register" Then

            MultiView2.ActiveViewIndex = 20
        ElseIf DropDownList9.SelectedValue = "Pending Payment" Then

            MultiView2.ActiveViewIndex = 21

        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Return
        ElseIf TextBox3.Text = "" Then
            TextBox3.Focus()
            Return
        ElseIf IsDate(TextBox3.Text) = False Then
            TextBox3.Text = ""
            TextBox3.Focus()
            Return
        End If

        Dim from_date, to_date As Date
        from_date = CDate(TextBox2.Text)
        to_date = CDate(TextBox3.Text)

        Dim FY As String = ""
        If from_date.Month > 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = FY & (FY + 1)
        ElseIf from_date.Month <= 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = (FY - 1) & FY
        End If

        conn.Open()
        dt.Clear()

        Dim quary As String = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2))

            INSERT INTO @TT
            SELECT ac_code, ac_desc,sum(ac_dr) AS AMOUNT_DR,sum(ac_cr) AS AMOUNT_CR FROM ob_trial WHERE ac_fy ='" & FY & "' group by ac_code, ac_desc having (SUM(ac_dr) - SUM(ac_cr) <>0) order by ac_code

            INSERT INTO @TT
            SELECT AC_NO,ac_description,(case when SUM(AMOUNT_DR) IS NULL THEN '0.00' else SUM(AMOUNT_DR) end) AS AMOUNT_DR,
            (case when SUM(AMOUNT_CR) IS NULL THEN '0.00' else SUM(AMOUNT_CR) end) AS AMOUNT_CR FROM LEDGER join ACDIC on 
            LEDGER.AC_NO=ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' 
            AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND (PAYMENT_INDICATION  <>'X') group by AC_NO, ac_description order by AC_NO
            
            SELECT AC_NO, ac_description,(case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR, 
            (case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR FROM @TT GROUP BY AC_NO,ac_description 
            HAVING (SUM(AMOUNT_DR) - SUM(AMOUNT_CR) <>0) ORDER BY AC_NO"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()


        Dim total_dr, total_cr As New Decimal(0)
        Dim I As Integer = 0
        For I = 0 To GridView1.Rows.Count - 1
            ''Calculating total Debit and Credit amount
            total_dr = total_dr + CDec(GridView1.Rows(I).Cells(2).Text)
            total_cr = total_cr + CDec(GridView1.Rows(I).Cells(3).Text)
        Next
        Dim dRow As DataRow
        dRow = dt.NewRow
        dRow.Item(0) = "Total"
        dt.Rows.Add(dRow)

        dt.AcceptChanges()
        GridView1.DataSource = dt
        GridView1.DataBind()

        'calculateTrialAmount(GridView1.Rows.Count - 1)
        GridView1.Rows(GridView1.Rows.Count - 1).Cells(2).Text = total_dr
        GridView1.Rows(GridView1.Rows.Count - 1).Cells(3).Text = total_cr
        GridView1.Rows(GridView1.Rows.Count - 1).Font.Bold = True

    End Sub

    Protected Sub calculateTrialAmount(count)
        Dim from_date, to_date As Date
        from_date = CDate(TextBox2.Text)
        to_date = CDate(TextBox3.Text)

        Dim FY As String = ""
        If from_date.Month > 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = FY & (FY + 1)
        ElseIf from_date.Month <= 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = (FY - 1) & FY
        End If


        Dim I As Integer = 0
        For I = 0 To count - 1
            Dim OP_CR, OP_DR, CURRENT_DR, CURRENT_CR As String
            OP_CR = 0
            OP_DR = 0

            '''''''''''''''''''''''''''
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "SELECT ( case when SUM(ac_dr) IS NULL THEN '0.00' else SUM(ac_dr) end) AS DR,( case when SUM(ac_cr) IS NULL THEN '0.00' else SUM(ac_cr) end) AS CR FROM ob_trial WHERE ac_code ='" & GridView1.Rows(I).Cells(0).Text & "' AND ac_fy ='" & FY & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                OP_DR = dr.Item("DR")
                OP_CR = dr.Item("CR")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            '''''''''''''''''''''''''''
            CURRENT_DR = 0
            CURRENT_CR = 0
            conn.Open()
            mc1.CommandText = "SELECT ( case when SUM(AMOUNT_DR) IS NULL THEN '0.00' else SUM(AMOUNT_DR) end) AS DR,( case when SUM(AMOUNT_CR) IS NULL THEN '0.00' else SUM(AMOUNT_CR) end) AS CR FROM LEDGER WHERE AC_NO ='" & GridView1.Rows(I).Cells(0).Text & "' AND EFECTIVE_DATE <= '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND FISCAL_YEAR='" & FY & "' AND (PAYMENT_INDICATION  <>'X')"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                CURRENT_CR = dr.Item("CR")
                CURRENT_DR = dr.Item("DR")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            If (CDec(OP_DR) + CDec(CURRENT_DR) - (CDec(OP_CR) + CDec(CURRENT_CR))) = 0 Then
                GridView1.Rows(I).Cells(2).Text = "0.00"
                GridView1.Rows(I).Cells(3).Text = "0.00"
            ElseIf (CDec(OP_DR) + CDec(CURRENT_DR) - (CDec(OP_CR) + CDec(CURRENT_CR))) > 0 Then
                GridView1.Rows(I).Cells(2).Text = (CDec(OP_DR) + CDec(CURRENT_DR) - (CDec(OP_CR) + CDec(CURRENT_CR)))
                GridView1.Rows(I).Cells(3).Text = "0.00"

            ElseIf (CDec(OP_DR) + CDec(CURRENT_DR) - (CDec(OP_CR) + CDec(CURRENT_CR))) < 0 Then
                GridView1.Rows(I).Cells(2).Text = "0.00"
                GridView1.Rows(I).Cells(3).Text = (CDec(OP_CR) + CDec(CURRENT_CR) - (CDec(OP_DR) + CDec(CURRENT_DR)))
            End If

        Next
    End Sub
    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If IsDate(TextBox6.Text) = False Then
            TextBox6.Text = ""
            TextBox6.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox21.Text)
        to_date = CDate(TextBox6.Text)

        Dim STR1 As String = ""
        If from_date.Month > 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf from_date.Month <= 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        'conn.Open()
        'dt.Clear()

        'Dim quary As String = "DECLARE @TT TABLE(AC_NO VARCHAR(30),SUPL_ID VARCHAR(30),SUPL_NAME VARCHAR(100),AC_NAME VARCHAR(250),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2))
        '    INSERT INTO @TT
        '    SELECT LEDGER .AC_NO,LEDGER.SUPL_ID,MAX(SUPL.SUPL_NAME) AS SUPL_NAME  ,MAX(ACDIC .ac_description) AS AC_NAME , 
        '    SUM(LEDGER .AMOUNT_DR) AS AMOUNT_DR, SUM(LEDGER.AMOUNT_CR) AS AMOUNT_CR
        '    FROM LEDGER JOIN ACDIC ON LEDGER .AC_NO =ACDIC .ac_code JOIN SUPL ON LEDGER.SUPL_ID =SUPL.SUPL_ID 
        '    WHERE LEDGER .AC_NO in (select distinct ac_code  from ACDIC where ac_ledg <> 0 ) and (LEDGER .SUPL_ID <>'' or LEDGER .SUPL_ID is null) AND LEDGER .EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'
        '    AND (PAYMENT_INDICATION  <>'X') GROUP BY  LEDGER .AC_NO,LEDGER .SUPL_ID 
        '    UNION 
        '    SELECT LEDGER .AC_NO,LEDGER.SUPL_ID,MAX(dater.d_name) AS SUPL_NAME  ,MAX(ACDIC .ac_description) AS AC_NAME , 
        '    SUM(LEDGER .AMOUNT_DR) AS AMOUNT_DR, SUM(LEDGER.AMOUNT_CR) AS AMOUNT_CR
        '    FROM LEDGER JOIN ACDIC ON LEDGER .AC_NO =ACDIC .ac_code JOIN dater ON LEDGER.SUPL_ID =dater.d_code 
        '    WHERE LEDGER .AC_NO in (select distinct ac_code  from ACDIC where ac_ledg <> 0 ) and (LEDGER .SUPL_ID <>'' or LEDGER .SUPL_ID is null) AND LEDGER .EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'
        '    AND (PAYMENT_INDICATION  <>'X') GROUP BY  LEDGER .AC_NO,LEDGER .SUPL_ID ORDER BY LEDGER .AC_NO	

        '    INSERT INTO @TT
        '    select o1.ac_no as AC_NO,o1.supl_id,(CASE WHEN o1.supl_id like 'D%' THEN d1.d_name ELSE s1.SUPL_NAME end) as SUPL_NAME,ac_description AS AC_NAME,debit AS AMOUNT_DR, credit AS AMOUNT_CR
        '    from ob_party_ledger o1 join ACDIC a1 on o1.ac_no=a1.ac_code left join SUPL s1 on o1.supl_id=s1.SUPL_ID left join dater d1 on o1.supl_id=d1.d_code where fiscal_year= " & STR1 & "

        '    DECLARE @TT1 TABLE(AC_NO VARCHAR(30),SUPL_ID VARCHAR(30),SUPL_NAME VARCHAR(100),AC_NAME VARCHAR(250),AMOUNT DECIMAL(16,2),AMOUNT_TYPE VARCHAR(5))
        '    INSERT INTO @TT1
        '    SELECT AC_NO,SUPL_ID,SUPL_NAME,AC_NAME,(CASE WHEN( SUM(AMOUNT_DR) - SUM(AMOUNT_CR)) > 0 THEN (SUM(AMOUNT_DR) -SUM(AMOUNT_CR)) ELSE (SUM(AMOUNT_CR) -SUM(AMOUNT_DR)) END) AS AMOUNT, (CASE WHEN( SUM(AMOUNT_DR) -SUM(AMOUNT_CR)) > 0 THEN 'DR' ELSE 'CR' END) AS AMOUNT_TYPE FROM @TT WHERE AMOUNT_DR <> 0 or AMOUNT_CR <> 0 group by AC_NO,SUPL_ID,SUPL_NAME,AC_NAME ORDER BY AC_NO
        '    SELECT * FROM @TT1 WHERE AMOUNT > 0 ORDER BY AC_NO"


        'da = New SqlDataAdapter(quary, conn)
        'da.SelectCommand.CommandTimeout = 100
        'da.Fill(dt)
        'conn.Close()
        'GridView3.DataSource = dt
        'GridView3.DataBind()

        '''''''''''''''''''''''''''''

        Dim spName As String = "GetScheduleReport"
        'Dim constr As String = ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
        Using conn
            Using cmd As New SqlCommand(spName, conn)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@from_date", from_date)
                cmd.Parameters.AddWithValue("@to_date", to_date)
                cmd.Parameters.AddWithValue("@fiscal_year", STR1)
                Using sda As New SqlDataAdapter(cmd)
                    Using dt As New DataTable()
                        sda.Fill(dt)
                        GridView3.DataSource = dt
                        GridView3.DataBind()
                    End Using
                End Using
            End Using
        End Using

        '''''''''''''''''''''''''''''
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If GridView3.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(6) {New DataColumn("AC_NO"), New DataColumn("SUPL_ID"), New DataColumn("SUPL_NAME"), New DataColumn("AC_NAME"), New DataColumn("AMOUNT", GetType(Decimal)), New DataColumn("AMOUNT_TYPE"), New DataColumn("S_DATE")})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView3.Rows.Count - 1
            dt2.Rows.Add(GridView3.Rows(i).Cells(0).Text, GridView3.Rows(i).Cells(2).Text, GridView3.Rows(i).Cells(3).Text, GridView3.Rows(i).Cells(1).Text, CDec(GridView3.Rows(i).Cells(4).Text), GridView3.Rows(i).Cells(5).Text, TextBox6.Text)
        Next


        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/shedule_report.rpt"))
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

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If IsDate(TextBox4.Text) = False Then
            TextBox4.Text = ""
            TextBox4.Focus()
            Return
        ElseIf IsDate(TextBox5.Text) = False Then
            TextBox5.Text = ""
            TextBox5.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox4.Text)
        to_date = CDate(TextBox5.Text)


        ''''''''''''''''''''''''''''''''''
        conn.Open()
        dt.Clear()
        'Dim quary As String = "SELECT L1.VOUCHER_NO,V1.CVB_NO, V1.CVB_DATE, V1.CHEQUE_NO, V1.SUPL_ID, V1.SUPL_NAME, L1.AC_NO, A1.ac_description, L1.AMOUNT_DR, L1.AMOUNT_CR" &
        '    " FROM (LEDGER L1 JOIN VOUCHER V1 ON L1.VOUCHER_NO=V1.TOKEN_NO) JOIN ACDIC A1 ON L1.AC_NO=A1.ac_code WHERE (L1.GARN_NO_MB_NO='PAYMENT' OR L1.GARN_NO_MB_NO='RCD' OR (L1.GARN_NO_MB_NO LIKE 'RV%' AND L1.AMOUNT_CR >0) OR (L1.GARN_NO_MB_NO LIKE 'PV%' AND L1.POST_INDICATION ='ADV PAY')) " &
        '    " AND V1.CHEQUE_NO <> '' AND L1.EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (L1.AMOUNT_CR <> L1.AMOUNT_DR) ORDER BY L1.EFECTIVE_DATE"
        'Dim quary As String = "SELECT distinct L1.VOUCHER_NO,V1.CVB_NO, V1.CVB_DATE, V1.CHEQUE_NO, L1.SUPL_ID, (CASE WHEN L1.supl_id like 'D%' THEN d1.d_name ELSE s1.SUPL_NAME end) as SUPL_NAME, L1.AC_NO, A1.ac_description" &
        '    " FROM (LEDGER L1 JOIN VOUCHER V1 ON L1.VOUCHER_NO=V1.TOKEN_NO) JOIN ACDIC A1 ON L1.AC_NO=A1.ac_code left join supl s1 on s1.SUPL_ID=L1.SUPL_ID left join dater d1 on d1.d_code=L1.SUPL_ID WHERE (L1.GARN_NO_MB_NO='PAYMENT' OR L1.GARN_NO_MB_NO='RCD' OR (L1.GARN_NO_MB_NO LIKE 'RV%' AND L1.AMOUNT_CR >0) OR (L1.GARN_NO_MB_NO LIKE 'PV%' AND L1.POST_INDICATION ='ADV PAY')) " &
        '    " AND V1.CHEQUE_NO <> '' AND L1.EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (L1.AMOUNT_CR <> L1.AMOUNT_DR) ORDER BY L1.VOUCHER_NO"
        Dim quary As String = "SELECT distinct L1.VOUCHER_NO,V1.CVB_NO, V1.CVB_DATE, V1.CHEQUE_NO, L1.SUPL_ID, V1.SUPL_NAME, L1.AC_NO, A1.ac_description" &
            " FROM (LEDGER L1 JOIN VOUCHER V1 ON L1.VOUCHER_NO=V1.TOKEN_NO) JOIN ACDIC A1 ON L1.AC_NO=A1.ac_code left join supl s1 on s1.SUPL_ID=L1.SUPL_ID left join dater d1 on d1.d_code=L1.SUPL_ID WHERE (L1.GARN_NO_MB_NO='PAYMENT' OR L1.GARN_NO_MB_NO='RCD' OR (L1.GARN_NO_MB_NO LIKE 'RV%' AND L1.AMOUNT_CR >0) OR (L1.GARN_NO_MB_NO LIKE 'PV%' AND L1.POST_INDICATION ='ADV PAY')) " &
            " AND V1.CHEQUE_NO <> '' AND L1.EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (L1.AMOUNT_CR <> L1.AMOUNT_DR) ORDER BY L1.VOUCHER_NO"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        ''''''''''''''''''''''''''''''''''
        GridView2.DataSource = dt
        GridView2.DataBind()

        calculateBankBookAmount(GridView2.Rows.Count)


        'METHOD TO CALCULATE DEBIT AND CREDIT AMOUNT OF DIFFERENT HEADS
        Dim total_dr, total_cr As New Decimal(0)
        Dim I As Integer = 0
        For I = 0 To GridView2.Rows.Count - 1
            ''Calculating total Debit and Credit amount
            total_dr = total_dr + CDec(GridView2.Rows(I).Cells(8).Text)
            total_cr = total_cr + CDec(GridView2.Rows(I).Cells(9).Text)
        Next
        Dim dRow As DataRow
        dRow = dt.NewRow
        dRow.Item(7) = "Total"
        dt.Rows.Add(dRow)

        dt.AcceptChanges()
        GridView2.DataSource = dt
        GridView2.DataBind()

        GridView2.Rows(GridView2.Rows.Count - 1).Cells(8).Text = total_dr
        GridView2.Rows(GridView2.Rows.Count - 1).Cells(9).Text = total_cr
        GridView2.Rows(GridView2.Rows.Count - 1).Font.Bold = True


        calculateBankBookAmount(GridView2.Rows.Count - 1)

    End Sub


    Protected Sub calculateBankBookAmount(count)
        Dim SUM_DEBIT, SUM_CREDIT As New Decimal(0)
        Dim BILL_ID As New String("")
        Dim I1 As Integer = 0
        For I1 = 0 To count - 1

            ''''''''''''''''''''''''''''''''
            ''Checking bill track id
            BILL_ID = ""
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select BILL_TRACK_ID from ledger where VOUCHER_NO='" & GridView2.Rows(I1).Cells(0).Text & "' and SUPL_ID='" & GridView2.Rows(I1).Cells(4).Text & "' and AC_NO='" & GridView2.Rows(I1).Cells(6).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr.Item("BILL_TRACK_ID")) Then
                    'conn.Open()
                    'Dim mc1 As New SqlCommand
                    mc1.CommandText = "select sum(AMOUNT_DR) As SUM_DEBIT,sum(AMOUNT_CR) As SUM_CREDIT from ledger where VOUCHER_NO='" & GridView2.Rows(I1).Cells(0).Text & "' and SUPL_ID='" & GridView2.Rows(I1).Cells(4).Text & "' and AC_NO='" & GridView2.Rows(I1).Cells(6).Text & "' and (GARN_NO_MB_NO='PAYMENT' or GARN_NO_MB_NO='PSC_AT_BILL_PASS' OR GARN_NO_MB_NO='RCD' OR (GARN_NO_MB_NO LIKE 'RV%' AND AMOUNT_CR >0) OR (GARN_NO_MB_NO LIKE 'PV%' AND POST_INDICATION ='ADV PAY')) group by VOUCHER_NO, supl_id,ac_no"
                    'mc1.Connection = conn
                    'dr = mc1.ExecuteReader
                    'If dr.HasRows = True Then
                    '    dr.Read()
                    '    ''Calculating total Debit and Credit amount
                    '    SUM_DEBIT = dr.Item("SUM_DEBIT")
                    '    SUM_CREDIT = dr.Item("SUM_CREDIT")
                    '    dr.Close()
                    'Else
                    '    dr.Close()
                    'End If
                    'conn.Close()
                Else
                    BILL_ID = dr.Item("BILL_TRACK_ID")
                    If (BILL_ID = "") Then
                        mc1.CommandText = "select sum(AMOUNT_DR) As SUM_DEBIT,sum(AMOUNT_CR) As SUM_CREDIT from ledger where VOUCHER_NO='" & GridView2.Rows(I1).Cells(0).Text & "' and SUPL_ID='" & GridView2.Rows(I1).Cells(4).Text & "' and AC_NO='" & GridView2.Rows(I1).Cells(6).Text & "' and (GARN_NO_MB_NO='PAYMENT' or GARN_NO_MB_NO='PSC_AT_BILL_PASS' OR GARN_NO_MB_NO='RCD' OR (GARN_NO_MB_NO LIKE 'RV%' AND AMOUNT_CR >0) OR (GARN_NO_MB_NO LIKE 'PV%' AND POST_INDICATION ='ADV PAY')) group by VOUCHER_NO, supl_id,ac_no"
                    Else
                        'conn.Open()
                        'Dim mc1 As New SqlCommand
                        mc1.CommandText = "select sum(AMOUNT_DR) As SUM_DEBIT,sum(AMOUNT_CR) As SUM_CREDIT from ledger where VOUCHER_NO='" & GridView2.Rows(I1).Cells(0).Text & "' and SUPL_ID='" & GridView2.Rows(I1).Cells(4).Text & "' and (GARN_NO_MB_NO='PAYMENT' or GARN_NO_MB_NO='PSC_AT_BILL_PASS' OR GARN_NO_MB_NO='RCD' OR (GARN_NO_MB_NO LIKE 'RV%' AND AMOUNT_CR >0) OR (GARN_NO_MB_NO LIKE 'PV%' AND POST_INDICATION ='ADV PAY')) group by VOUCHER_NO, supl_id"
                        'mc1.Connection = conn
                        'dr = mc1.ExecuteReader
                        'If dr.HasRows = True Then
                        '    dr.Read()
                        '    ''Calculating total Debit and Credit amount
                        '    SUM_DEBIT = dr.Item("SUM_DEBIT")
                        '    SUM_CREDIT = dr.Item("SUM_CREDIT")
                        '    dr.Close()
                        'Else
                        '    dr.Close()
                        'End If
                        'conn.Close()
                    End If
                End If
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            ''''''''''''''''''''''''''''''''
            conn.Open()
            'Dim mc1 As New SqlCommand
            'mc1.CommandText = "select AMOUNT_DR As SUM_DEBIT,AMOUNT_CR As SUM_CREDIT from ledger where VOUCHER_NO='" & GridView2.Rows(I1).Cells(0).Text & "' and SUPL_ID='" & GridView2.Rows(I1).Cells(4).Text & "' and AC_NO='" & GridView2.Rows(I1).Cells(6).Text & "' and (GARN_NO_MB_NO='PAYMENT' or GARN_NO_MB_NO='PSC_AT_BILL_PASS' OR GARN_NO_MB_NO='RCD' OR (GARN_NO_MB_NO LIKE 'RV%' AND AMOUNT_CR >0) OR (GARN_NO_MB_NO LIKE 'PV%' AND POST_INDICATION ='ADV PAY')) group by VOUCHER_NO, supl_id"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                ''Calculating total Debit and Credit amount
                SUM_DEBIT = dr.Item("SUM_DEBIT")
                SUM_CREDIT = dr.Item("SUM_CREDIT")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()


            If (SUM_DEBIT > SUM_CREDIT) Then
                GridView2.Rows(I1).Cells(8).Text = SUM_DEBIT - SUM_CREDIT
                GridView2.Rows(I1).Cells(9).Text = "0.00"
            Else
                GridView2.Rows(I1).Cells(8).Text = "0.00"
                GridView2.Rows(I1).Cells(9).Text = SUM_CREDIT - SUM_DEBIT
            End If

        Next
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If GridView1.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(4) {New DataColumn("ac_no"), New DataColumn("ac_name"), New DataColumn("dr", GetType(Decimal)), New DataColumn("cr", GetType(Decimal)), New DataColumn("duration")})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView1.Rows.Count - 1
            dt2.Rows.Add(GridView1.Rows(i).Cells(0).Text, GridView1.Rows(i).Cells(1).Text, GridView1.Rows(i).Cells(2).Text, GridView1.Rows(i).Cells(3).Text, TextBox2.Text + " to " + TextBox3.Text)
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/trial.rpt"))
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

    Protected Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click

        Dim conn1 As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)

        Dim quary As String = ""
        Dim TERM As String = ""
        Dim crystalReport As New ReportDocument
        quary = "SELECT * FROM SALE_RCD_VOUCHAR WHERE VOUCHER_TYPE + VOUCHER_NO like '" & TextBox35.Text & "'"
        conn1.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = quary
        mc1.Connection = conn1
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            TERM = dr.Item("voucher_type")
            dr.Close()
        Else
            dr.Close()
        End If
        conn1.Close()
        If TERM = "" Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        End If

        crystalReport.Load(Server.MapPath("~/print_rpt/gst_rcd_goods.rpt"))

        conn1.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(quary, conn1)
        da.Fill(dt)
        conn1.Close()
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
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
        Dim quary As String = ""
        Dim TERM As String = ""
        Dim crystalReport As New ReportDocument
        quary = "SELECT ('') as INV_TO , *  FROM RCM_INV WHERE D_TYPE + INV_NO like '" & TextBox7.Text & "' AND FISCAL_YEAR ='" & TextBox36.Text & "'"


        crystalReport.Load(Server.MapPath("~/print_rpt/gst_rcm_inv.rpt"))
        Dim I As Integer = 0
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(quary, conn)
        I = da.Fill(dt)
        conn.Close()
        'MsgBox(I)
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
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

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If GridView4.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(8) {New DataColumn("ac_no"), New DataColumn("ac_name"), New DataColumn("po_no"), New DataColumn("garn_no"), New DataColumn("voucher_no"), New DataColumn("date", GetType(String)), New DataColumn("dr", GetType(Decimal)), New DataColumn("cr", GetType(Decimal)), New DataColumn("duration")})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView4.Rows.Count - 1
            dt2.Rows.Add(GridView4.Rows(i).Cells(0).Text, GridView4.Rows(i).Cells(1).Text, GridView4.Rows(i).Cells(2).Text, GridView4.Rows(i).Cells(3).Text, GridView4.Rows(i).Cells(4).Text, GridView4.Rows(i).Cells(5).Text, CDec(GridView4.Rows(i).Cells(6).Text), CDec(GridView4.Rows(i).Cells(7).Text), TextBox8.Text + " to " + TextBox9.Text)
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/general_ledger.rpt"))
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

    Public Sub ExportExcel(ByVal filename As String, ByVal gv As GridView)
        gv.AllowPaging = False
        gv.AllowSorting = False
        gv.EditIndex = -1
        Response.ClearContent()
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        gv.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub

    Protected Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If TextBox10.Text = "" Then
            TextBox10.Focus()
            Return
        ElseIf IsDate(TextBox10.Text) = False Then
            TextBox10.Text = ""
            TextBox10.Focus()
            Return
        ElseIf TextBox11.Text = "" Then
            TextBox11.Focus()
            Return
        ElseIf IsDate(TextBox11.Text) = False Then
            TextBox11.Text = ""
            TextBox11.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox10.Text)
        to_date = CDate(TextBox11.Text)

        Dim STR1 As String = ""
        If from_date.Month > 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf from_date.Month <= 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If


        Dim quary As String
        quary = ""

        If (DropDownList1.Text <> "All" And DropDownList3.Text <> "All") Then
            conn.Open()
            dt.Clear()

            'SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy', 'en-us') As Entry_Date, LEDGER.SUPL_ID AS SUPL_ID, 
            'LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND PAYMENT_INDICATION <>'X' ORDER BY LEDGER.SUPL_ID,LEDGER.ENTRY_DATE
            quary = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),PO_NO VARCHAR(30),GARN VARCHAR(250),VOUCHER_NO VARCHAR(30),INVOICE_NO VARCHAR(50),SUPL_ID VARCHAR(30),EFECTIVE_DATE DATE,ENTRY_DATE DATETIME,DR DECIMAL(16,2),CR DECIMAL(16,2))
            
            INSERT INTO @TT
            SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, CONVERT(varchar, LEDGER.VOUCHER_NO) As VOUCHER_NO, 
            LEDGER.INVOICE_NO AS INVOICE_NO,LEDGER.SUPL_ID AS SUPL_ID,CONVERT(date, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy'),103) As EFECTIVE_DATE,LEDGER.ENTRY_DATE, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR AS CR 
            FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND 
            LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND PAYMENT_INDICATION <>'X' ORDER BY LEDGER.AC_NO,LEDGER.ENTRY_DATE

            INSERT INTO @TT
            SELECT ac_no AS AC_NO, ac_description, '' AS PO_NO, 'OPENING' AS GARN,'' AS VOUCHER_NO,'' AS INVOICE_NO ,SUPL_ID, CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS EFECTIVE_DATE, 
            CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS ENTRY_DATE,debit AS DR, credit AS CR FROM ob_party_ledger join ACDIC on ob_party_ledger.ac_no =ACDIC.ac_code 
            where ac_code='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1).Trim & "' AND SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND (debit>0 OR credit>0) AND fiscal_year=" & STR1 & "
            
            SELECT * FROM @TT ORDER BY AC_NO, ENTRY_DATE"


        ElseIf (DropDownList1.Text = "All" And DropDownList3.Text <> "All") Then
            conn.Open()
            dt.Clear()
            quary = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),PO_NO VARCHAR(30),GARN VARCHAR(250),VOUCHER_NO VARCHAR(30),INVOICE_NO VARCHAR(50),SUPL_ID VARCHAR(30),EFECTIVE_DATE DATE,ENTRY_DATE DATETIME,DR DECIMAL(16,2),CR DECIMAL(16,2))
            
            INSERT INTO @TT
            SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, CONVERT(varchar, LEDGER.VOUCHER_NO) As VOUCHER_NO, 
            LEDGER.INVOICE_NO AS INVOICE_NO,LEDGER.SUPL_ID AS SUPL_ID,CONVERT(date, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy'),103) As EFECTIVE_DATE,LEDGER.ENTRY_DATE, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR AS CR 
            FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND 
            EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND PAYMENT_INDICATION <>'X' ORDER BY LEDGER.AC_NO,LEDGER.ENTRY_DATE

            INSERT INTO @TT
            SELECT ac_no AS AC_NO, ac_description, '' AS PO_NO, 'OPENING' AS GARN,'' AS VOUCHER_NO,'' AS INVOICE_NO, SUPL_ID, CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS EFECTIVE_DATE, 
            CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS ENTRY_DATE,debit AS DR, credit AS CR FROM ob_party_ledger join ACDIC on ob_party_ledger.ac_no =ACDIC.ac_code 
            where SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND (debit>0 OR credit>0) AND fiscal_year=" & STR1 & "
            
            SELECT * FROM @TT ORDER BY AC_NO, ENTRY_DATE"
        ElseIf (DropDownList1.Text <> "All" And DropDownList3.Text = "All") Then
            conn.Open()
            dt.Clear()
            quary = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),PO_NO VARCHAR(30),GARN VARCHAR(250),VOUCHER_NO VARCHAR(30),INVOICE_NO VARCHAR(50),SUPL_ID VARCHAR(30),EFECTIVE_DATE DATE,ENTRY_DATE DATETIME,DR DECIMAL(16,2),CR DECIMAL(16,2))
            
            INSERT INTO @TT
            SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, CONVERT(varchar, LEDGER.VOUCHER_NO) As VOUCHER_NO, 
            LEDGER.INVOICE_NO AS INVOICE_NO,LEDGER.SUPL_ID AS SUPL_ID,CONVERT(date, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy'),103) As EFECTIVE_DATE,LEDGER.ENTRY_DATE, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR AS CR 
            FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND 
            EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND PAYMENT_INDICATION <>'X' ORDER BY LEDGER.AC_NO,LEDGER.ENTRY_DATE

            INSERT INTO @TT
            SELECT ac_no AS AC_NO, ac_description, '' AS PO_NO, 'OPENING' AS GARN,'' AS VOUCHER_NO,'' AS INVOICE_NO, SUPL_ID, CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS EFECTIVE_DATE, 
            CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS ENTRY_DATE,debit AS DR, credit AS CR FROM ob_party_ledger join ACDIC on ob_party_ledger.ac_no =ACDIC.ac_code 
            where ac_code='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1).Trim & "' AND (debit>0 OR credit>0) AND fiscal_year=" & STR1 & "
            
            SELECT * FROM @TT ORDER BY AC_NO,SUPL_ID, ENTRY_DATE"

        ElseIf (DropDownList1.Text = "All" And DropDownList3.Text = "All") Then
            conn.Open()
            dt.Clear()
            quary = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),PO_NO VARCHAR(30),GARN VARCHAR(250),VOUCHER_NO VARCHAR(30),INVOICE_NO VARCHAR(50),SUPL_ID VARCHAR(30),EFECTIVE_DATE DATE,ENTRY_DATE DATETIME,DR DECIMAL(16,2),CR DECIMAL(16,2))
            
            INSERT INTO @TT
            SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, CONVERT(varchar, LEDGER.VOUCHER_NO) As VOUCHER_NO, 
            LEDGER.INVOICE_NO AS INVOICE_NO,LEDGER.SUPL_ID AS SUPL_ID,CONVERT(date, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy'),103) As EFECTIVE_DATE,LEDGER.ENTRY_DATE, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR AS CR 
            FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' 
            AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND PAYMENT_INDICATION <>'X' ORDER BY LEDGER.AC_NO,LEDGER.ENTRY_DATE

            INSERT INTO @TT
            SELECT ac_no AS AC_NO, ac_description, '' AS PO_NO, 'OPENING' AS GARN,'' AS VOUCHER_NO,'' AS INVOICE_NO, SUPL_ID, CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS EFECTIVE_DATE, 
            CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS ENTRY_DATE,debit AS DR, credit AS CR FROM ob_party_ledger join ACDIC on ob_party_ledger.ac_no =ACDIC.ac_code 
            where (debit>0 OR credit>0) AND fiscal_year=" & STR1 & "
            
            SELECT * FROM @TT ORDER BY AC_NO,SUPL_ID, ENTRY_DATE"
        End If

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView6.DataSource = dt
        GridView6.DataBind()

        Dim total_dr, total_cr As New Decimal(0)
        Dim I As Integer = 0
        For I = 0 To GridView6.Rows.Count - 1
            If (GridView6.Rows(I).Cells(3).Text = "OPENING") Then
                GridView6.Rows(I).Font.Bold = True
            End If
            ''Calculating total Debit and Credit amount
            total_dr = total_dr + CDec(GridView6.Rows(I).Cells(8).Text)
            total_cr = total_cr + CDec(GridView6.Rows(I).Cells(9).Text)
        Next
        Dim dRow1 As DataRow
        dRow1 = dt.NewRow
        dRow1.Item(0) = "Total"
        dt.Rows.Add(dRow1)

        dt.AcceptChanges()

        GridView6.DataSource = dt
        GridView6.DataBind()

        GridView6.Rows(GridView6.Rows.Count - 1).Cells(8).Text = total_dr
        GridView6.Rows(GridView6.Rows.Count - 1).Cells(9).Text = total_cr
        GridView6.Rows(GridView6.Rows.Count - 1).Font.Bold = True

        ''''''''''============================'''''''''''

    End Sub



    Protected Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If GridView4.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("GL")
        dt3.Columns.Add(New DataColumn("A/C. No", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/C. Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("PO No", GetType(String)))
        dt3.Columns.Add(New DataColumn("GARN No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Voucher No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Amount DR", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Amount CR", GetType(Decimal)))


        For Me.count = 0 To GridView4.Rows.Count - 1
            dt3.Rows.Add(GridView4.Rows(count).Cells(0).Text, GridView4.Rows(count).Cells(1).Text, GridView4.Rows(count).Cells(2).Text, GridView4.Rows(count).Cells(3).Text, GridView4.Rows(count).Cells(4).Text, GridView4.Rows(count).Cells(5).Text, CDec(GridView4.Rows(count).Cells(6).Text), CDec(GridView4.Rows(count).Cells(7).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            'Dim workbook = New ExcelFile
            'Dim worksheet = workbook.Worksheets.Add("Styles and Formatting")
            'worksheet.Range("FirstCell:LastCell").Merge()


            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=General_Ledger.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If GridView1.Rows.Count < 0 Then
            Return
        End If


        '''''''''''''''''''''''''''''
        Dim from_date, to_date As Date
        from_date = CDate(TextBox2.Text)
        to_date = CDate(TextBox3.Text)

        Dim FY As String = ""
        If from_date.Month > 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = FY & (FY + 1)
        ElseIf from_date.Month <= 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = (FY - 1) & FY
        End If

        Using cmd As New SqlCommand("DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2))

            INSERT INTO @TT
            SELECT ac_code, ac_desc,ac_dr AS AMOUNT_DR,ac_cr AS AMOUNT_CR FROM ob_trial WHERE ac_fy ='" & FY & "' order by ac_code

            INSERT INTO @TT
            SELECT AC_NO,ac_description,(case when SUM(AMOUNT_DR) IS NULL THEN '0.00' else SUM(AMOUNT_DR) end) AS AMOUNT_DR,
            (case when SUM(AMOUNT_CR) IS NULL THEN '0.00' else SUM(AMOUNT_CR) end) AS AMOUNT_CR FROM LEDGER join ACDIC on 
            LEDGER.AC_NO=ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' 
            AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND (PAYMENT_INDICATION  <>'X') group by AC_NO, ac_description order by AC_NO
            
            SELECT AC_NO, ac_description,(case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR, 
            (case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR FROM @TT GROUP BY AC_NO,ac_description 
            HAVING (SUM(AMOUNT_DR) - SUM(AMOUNT_CR) <> 0) ORDER BY AC_NO")



            Using sda As New SqlDataAdapter()
                cmd.Connection = conn
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    Using wb As New XLWorkbook()
                        wb.Worksheets.Add(dt, "Customers")

                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        Response.AddHeader("content-disposition", "attachment;filename=Trial_New.xlsx")

                        Using MyMemoryStream As New MemoryStream()
                            wb.SaveAs(MyMemoryStream)
                            MyMemoryStream.WriteTo(Response.OutputStream)
                            Response.Flush()
                            Response.End()
                        End Using
                    End Using
                End Using
            End Using
        End Using


        '''''''''''''''''''''''''''''
    End Sub

    Protected Sub ExportExcel()

        '     Dim from_date, to_date As Date
        '     from_date = CDate(TextBox2.Text)
        '     to_date = CDate(TextBox3.Text)

        '     Dim FY As String = ""
        '     If from_date.Month > 3 Then
        '         FY = from_date.Year
        '         FY = FY.Trim.Substring(2)
        '         FY = FY & (FY + 1)
        '     ElseIf from_date.Month <= 3 Then
        '         FY = from_date.Year
        '         FY = FY.Trim.Substring(2)
        '         FY = (FY - 1) & FY
        '     End If


        '     Using con As New SqlConnection(conn)
        '         Using cmd As New SqlCommand("DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2))

        '         INSERT INTO @TT
        '         SELECT ac_code, ac_desc,ac_dr AS AMOUNT_DR,ac_cr AS AMOUNT_CR FROM ob_trial WHERE ac_fy ='" & FY & "' order by ac_code

        '         INSERT INTO @TT
        '      SELECT AC_NO,ac_description,(case when SUM(AMOUNT_DR) IS NULL THEN '0.00' else SUM(AMOUNT_DR) end) AS AMOUNT_DR,
        '(case when SUM(AMOUNT_CR) IS NULL THEN '0.00' else SUM(AMOUNT_CR) end) AS AMOUNT_CR FROM LEDGER join ACDIC on 
        'LEDGER.AC_NO=ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' 
        '         AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
        'AND (PAYMENT_INDICATION  <>'X') group by AC_NO, ac_description order by AC_NO

        '         SELECT AC_NO, ac_description,(case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR, 
        '         (case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR FROM @TT GROUP BY AC_NO,ac_description 
        '         HAVING (SUM(AMOUNT_DR) - SUM(AMOUNT_CR) <> 0) ORDER BY AC_NO")

        '             Using sda As New SqlDataAdapter()
        '                 cmd.Connection = con
        '                 sda.SelectCommand = cmd
        '                 Using dt As New DataTable()
        '                     sda.Fill(dt)
        '                     Using wb As New XLWorkbook()
        '                         wb.Worksheets.Add(dt, "Customers")

        '                         Response.Clear()
        '                         Response.Buffer = True
        '                         Response.Charset = ""
        '                         Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        '                         Response.AddHeader("content-disposition", "attachment;filename=Trial_New.xlsx")

        '                         Using MyMemoryStream As New MemoryStream()
        '                             wb.SaveAs(MyMemoryStream)
        '                             MyMemoryStream.WriteTo(Response.OutputStream)
        '                             Response.Flush()
        '                             Response.End()
        '                         End Using
        '                     End Using
        '                 End Using
        '             End Using
        '         End Using
        '     End Using
    End Sub

    Protected Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If GridView3.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Schedule")
        dt3.Columns.Add(New DataColumn("A/C. No", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/C. Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Party Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Party Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Balance Amt", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Amount Type", GetType(String)))

        For Me.count = 0 To GridView3.Rows.Count - 1
            dt3.Rows.Add(GridView3.Rows(count).Cells(0).Text, GridView3.Rows(count).Cells(1).Text, GridView3.Rows(count).Cells(2).Text, GridView3.Rows(count).Cells(3).Text, CDec(GridView3.Rows(count).Cells(4).Text), GridView3.Rows(count).Cells(5).Text)
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Schedule.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If GridView2.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Bank_Book")
        dt3.Columns.Add(New DataColumn("Voucher No", GetType(String)))
        dt3.Columns.Add(New DataColumn("C.B.V. No", GetType(String)))
        dt3.Columns.Add(New DataColumn("C.B.V. Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Check No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Party Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Party Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/C. No", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/C. Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Debit Amt", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Credit Amt", GetType(Decimal)))

        For Me.count = 0 To GridView2.Rows.Count - 1
            dt3.Rows.Add(GridView2.Rows(count).Cells(0).Text, GridView2.Rows(count).Cells(1).Text, GridView2.Rows(count).Cells(2).Text, GridView2.Rows(count).Cells(3).Text, GridView2.Rows(count).Cells(4).Text, GridView2.Rows(count).Cells(5).Text, GridView2.Rows(count).Cells(6).Text, GridView2.Rows(count).Cells(7).Text, CDec(GridView2.Rows(count).Cells(8).Text), CDec(GridView2.Rows(count).Cells(9).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Bank_Book.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If TextBox12.Text = "" Then
            TextBox12.Focus()
            Return
        ElseIf IsDate(TextBox12.Text) = False Then
            TextBox12.Text = ""
            TextBox12.Focus()
            Return
        ElseIf TextBox13.Text = "" Then
            TextBox13.Focus()
            Return
        ElseIf IsDate(TextBox13.Text) = False Then
            TextBox13.Text = ""
            TextBox13.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox12.Text)
        to_date = CDate(TextBox13.Text)

        Dim STR1 As String = ""
        If from_date.Month > 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf from_date.Month <= 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        ''Modified RIOC Report
        'conn.Open()
        'dt.Clear()
        'da = New SqlDataAdapter("SELECT * FROM MATERIAL WHERE MAT_CODE LIKE '100%' ORDER BY MAT_CODE", conn)
        'da.Fill(dt)
        'conn.Close()
        'GridView7.DataSource = dt
        'GridView7.DataBind()

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("DECLARE @TT TABLE(MAT_CODE VARCHAR(30),MAT_NAME VARCHAR(250),OPEN_QTY DECIMAL(16,3),OPEN_VALUE DECIMAL(16,3),RCD_QTY DECIMAL(16,3),RCD_VALUE DECIMAL(16,3),ISSUE_QTY DECIMAL(16,3),ISSUE_VALUE DECIMAL(16,3),MISC_SALE_QTY DECIMAL(16,3),MISC_SALE_VALUE DECIMAL(16,3),CLOSING_QTY DECIMAL(16,3),CLOSING_VALUE DECIMAL(16,3))

            INSERT INTO @TT
            SELECT MAT_CODE,MAT_NAME,OPEN_STOCK As OPEN_QTY,0 As OPEN_VALUE,0 As RCD_QTY,0 As RCD_VALUE,0 As ISSUE_QTY,0 As ISSUE_VALUE,0 As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE FROM MATERIAL WHERE MAT_CODE LIKE '100%' ORDER BY MAT_CODE

            INSERT INTO @TT
            Select MAT_DETAILS.MAT_CODE,MAT_NAME,(SUM(MAT_QTY) - SUM(ISSUE_QTY)) As OPEN_QTY,0 As OPEN_VALUE,0 As RCD_QTY,0 As RCD_VALUE,0 As ISSUE_QTY,0 As ISSUE_VALUE,0 As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE FROM MAT_DETAILS join MATERIAL on MAT_DETAILS.MAT_CODE=MATERIAL.MAT_CODE WHERE MAT_DETAILS.MAT_CODE LIKE '100%'  AND LINE_DATE < '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' GROUP BY MAT_DETAILS.MAT_CODE,MAT_NAME ORDER BY MAT_CODE

            INSERT INTO @TT
            SELECT MAT_CODE,MAT_NAME,0 As OPEN_QTY,(ac_dr-ac_cr) As OPEN_VALUE,0 As RCD_QTY,0 As RCD_VALUE,0 As ISSUE_QTY,0 As ISSUE_VALUE,0 As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE from ob_trial join MATERIAL on ob_trial.ac_code=MATERIAL.AC_PUR WHERE ac_code in(select AC_PUR from MATERIAL where MAT_CODE LIKE '100%') AND ac_fy='" & STR1 & "'
            
            INSERT INTO @TT
            SELECT MAT_DETAILS.MAT_CODE,MAT_NAME,0 As OPEN_QTY,0 As OPEN_VALUE,SUM(MAT_QTY) AS RCD_QTY, 0 AS RCD_VALUE,0 As ISSUE_QTY,0 As ISSUE_VALUE,0 As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE FROM MAT_DETAILS join MATERIAL on MAT_DETAILS.MAT_CODE=MATERIAL.MAT_CODE WHERE MAT_DETAILS.MAT_CODE LIKE '100%' and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (LINE_TYPE='R' or LINE_TYPE='A') GROUP BY MAT_DETAILS.MAT_CODE,MAT_NAME ORDER BY MAT_CODE

            INSERT INTO @TT
            SELECT MAT_CODE,MAT_NAME,0 As OPEN_QTY,0 As OPEN_VALUE,0 As RCD_QTY, SUM(AMOUNT_DR) - SUM(AMOUNT_CR) As RCD_VALUE,0 As ISSUE_QTY,0 As ISSUE_VALUE,0 As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE from LEDGER JOIN MATERIAL ON LEDGER.AC_NO=MATERIAL.AC_PUR where MAT_CODE LIKE '100%' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' GROUP BY MATERIAL.MAT_CODE,MAT_NAME ORDER BY MAT_CODE

            INSERT INTO @TT
            SELECT MAT_DETAILS.MAT_CODE,MAT_NAME,0 As OPEN_QTY,0 As OPEN_VALUE,0 AS RCD_QTY, 0 AS RCD_VALUE,SUM(ISSUE_QTY) As ISSUE_QTY,0 As ISSUE_VALUE,0 As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE FROM MAT_DETAILS join MATERIAL on MAT_DETAILS.MAT_CODE=MATERIAL.MAT_CODE WHERE MAT_DETAILS.MAT_CODE LIKE '100%' and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (LINE_TYPE='I' or LINE_TYPE='A') GROUP BY MAT_DETAILS.MAT_CODE,MAT_NAME ORDER BY MAT_CODE

            INSERT INTO @TT
            SELECT MAT_CODE,MAT_NAME,0 As OPEN_QTY,0 As OPEN_VALUE,0 As RCD_QTY, 0 As RCD_VALUE,0 As ISSUE_QTY, SUM(AMOUNT_CR)-SUM(AMOUNT_DR) As ISSUE_VALUE,0 As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE from LEDGER JOIN MATERIAL ON LEDGER.AC_NO=MATERIAL.AC_ISSUE where MAT_CODE LIKE '100%' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and POST_INDICATION <>'STOCK TRANSFOR' GROUP BY MATERIAL.MAT_CODE,MAT_NAME ORDER BY MAT_CODE

            INSERT INTO @TT
            SELECT MAT_DETAILS.MAT_CODE,MAT_NAME,0 As OPEN_QTY,0 As OPEN_VALUE,0 AS RCD_QTY, 0 AS RCD_VALUE,0 As ISSUE_QTY,0 As ISSUE_VALUE,SUM(ISSUE_QTY) As MISC_SALE_QTY,0 As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE FROM MAT_DETAILS join MATERIAL on MAT_DETAILS.MAT_CODE=MATERIAL.MAT_CODE WHERE MAT_DETAILS.MAT_CODE LIKE '100%' and LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND LINE_TYPE='S' GROUP BY MAT_DETAILS.MAT_CODE,MAT_NAME ORDER BY MAT_CODE

            INSERT INTO @TT
            SELECT MAT_CODE,MAT_NAME,0 As OPEN_QTY,0 As OPEN_VALUE,0 As RCD_QTY, 0 As RCD_VALUE,0 As ISSUE_QTY, 0 As ISSUE_VALUE,0 As MISC_SALE_QTY,SUM(AMOUNT_CR)-SUM(AMOUNT_DR) As MISC_SALE_VALUE,0 As CLOSING_QTY,0 As CLOSING_VALUE from LEDGER JOIN MATERIAL ON LEDGER.AC_NO=MATERIAL.AC_ISSUE where MAT_CODE LIKE '100%' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND POST_INDICATION='STOCK TRANSFOR' GROUP BY MATERIAL.MAT_CODE,MAT_NAME ORDER BY MAT_CODE

            SELECT MAT_CODE,MAT_NAME,SUM(OPEN_QTY) AS OPEN_QTY,SUM(OPEN_VALUE) AS OPEN_VALUE,SUM(RCD_QTY) AS RCD_QTY,SUM(RCD_VALUE) AS RCD_VALUE,SUM(ISSUE_QTY) AS ISSUE_QTY,SUM(ISSUE_VALUE) AS ISSUE_VALUE,SUM(MISC_SALE_QTY) AS MISC_SALE_QTY,SUM(MISC_SALE_VALUE) AS MISC_SALE_VALUE,SUM(OPEN_QTY+RCD_QTY-ISSUE_QTY-MISC_SALE_QTY) AS CLOSING_QTY,(case when (SUM(OPEN_QTY+RCD_QTY-ISSUE_QTY-MISC_SALE_QTY)>0) THEN SUM(OPEN_VALUE+RCD_VALUE-ISSUE_VALUE-MISC_SALE_VALUE) else 0.00 end) AS CLOSING_VALUE FROM @TT GROUP BY MAT_CODE,MAT_NAME ORDER BY MAT_CODE", conn)
        da.Fill(dt)
        conn.Close()
        GridView7.DataSource = dt
        GridView7.DataBind()



    End Sub

    Protected Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If GridView7.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("RIOC")
        dt3.Columns.Add(New DataColumn("Mat Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Mat Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Opening Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Opening Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("RCD Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Rcd Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Issue Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("issue Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Misc. Sale Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Misc. Sale Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Closing Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Closing Value", GetType(Decimal)))

        For Me.count = 0 To GridView7.Rows.Count - 1
            dt3.Rows.Add(GridView7.Rows(count).Cells(0).Text, GridView7.Rows(count).Cells(1).Text, GridView7.Rows(count).Cells(2).Text, GridView7.Rows(count).Cells(3).Text, CDec(GridView7.Rows(count).Cells(4).Text), GridView7.Rows(count).Cells(5).Text, GridView7.Rows(count).Cells(6).Text, CDec(GridView7.Rows(count).Cells(7).Text), CDec(GridView7.Rows(count).Cells(8).Text), CDec(GridView7.Rows(count).Cells(9).Text), CDec(GridView7.Rows(count).Cells(10).Text), CDec(GridView7.Rows(count).Cells(11).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=RIOC.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If GridView6.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("PARTY_LEDGER")
        dt3.Columns.Add(New DataColumn("A/C NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/C Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("PO NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("GARN No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Voucher No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Invoice No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Supplier", GetType(String)))
        dt3.Columns.Add(New DataColumn("Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Debit", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Credit", GetType(Decimal)))

        For Me.count = 0 To GridView6.Rows.Count - 1
            dt3.Rows.Add(GridView6.Rows(count).Cells(0).Text, GridView6.Rows(count).Cells(1).Text, GridView6.Rows(count).Cells(2).Text, GridView6.Rows(count).Cells(3).Text, GridView6.Rows(count).Cells(4).Text, GridView6.Rows(count).Cells(5).Text, GridView6.Rows(count).Cells(6).Text, GridView6.Rows(count).Cells(7).Text, CDec(GridView6.Rows(count).Cells(8).Text), CDec(GridView6.Rows(count).Cells(9).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=PARTY_LEDGER.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        If TextBox14.Text = "" Then
            TextBox14.Focus()
            Return
        ElseIf IsDate(TextBox14.Text) = False Then
            TextBox14.Text = ""
            TextBox14.Focus()
            Return
        ElseIf TextBox15.Text = "" Then
            TextBox15.Focus()
            Return
        ElseIf IsDate(TextBox15.Text) = False Then
            TextBox15.Text = ""
            TextBox15.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox14.Text)
        to_date = CDate(TextBox15.Text)

        conn.Open()
        dt.Clear()
        Dim quary As String = "select m1.mb_no, m1. wo_slno, m1.w_name, m1.w_au,996511 as sac_code, CAST((round((m1.valuation_amt/work_qty),2)) as decimal(18,3)) AS UNIT_PRICE, m1.work_qty, m1.valuation_amt As prov_amt, CAST((round((m1.cgst*100/valuation_amt),1)) as decimal(18,3)) AS CGST, m1.cgst_liab, CAST((round((m1.sgst*100/valuation_amt),1)) as decimal(18,3)) AS SGST, m1.sgst_liab, CAST((round((m1.igst*100/valuation_amt),1)) as decimal(18,3)) AS IGST, m1.igst_liab, CAST((round((m1.cess*100/valuation_amt),1)) as decimal(18,3)) AS CESS, m1.cess_liab,(valuation_amt + m1.sgst_liab + m1.cgst_liab + m1.igst_liab + m1.cess_liab) as TOTAL_VAL from mb_book m1 where (mb_no like 'dc%' or mb_no like 'os%' or mb_no like 'rcrr%' or mb_no like 'scrr%') and RCM_P is null and (sgst>0 or cgst>0 or igst>0 or cess>0) and v_ind='V' and mb_date between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by mb_no"
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView8.DataSource = dt
        GridView8.DataBind()
    End Sub

    Protected Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If GridView8.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("Pending RCM")
        dt3.Columns.Add(New DataColumn("CRR No", GetType(String)))
        dt3.Columns.Add(New DataColumn("WO SLNo", GetType(String)))
        dt3.Columns.Add(New DataColumn("Work Desc.", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/U", GetType(String)))
        dt3.Columns.Add(New DataColumn("SAC Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Unit Rate", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Work Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Taxable Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("CGST", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("CGST Amt", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("SGST", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("SGST Amt", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("IGST", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("IGST Amt", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("CESS", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("CESS Amt", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Total Value", GetType(Decimal)))

        For Me.count = 0 To GridView8.Rows.Count - 1
            dt3.Rows.Add(GridView8.Rows(count).Cells(0).Text, GridView8.Rows(count).Cells(1).Text, GridView8.Rows(count).Cells(2).Text, GridView8.Rows(count).Cells(3).Text, GridView8.Rows(count).Cells(4).Text, CDec(GridView8.Rows(count).Cells(5).Text), CDec(GridView8.Rows(count).Cells(6).Text), CDec(GridView8.Rows(count).Cells(7).Text), CDec(GridView8.Rows(count).Cells(8).Text), CDec(GridView8.Rows(count).Cells(9).Text), CDec(GridView8.Rows(count).Cells(10).Text), CDec(GridView8.Rows(count).Cells(11).Text), CDec(GridView8.Rows(count).Cells(12).Text), CDec(GridView8.Rows(count).Cells(13).Text), CDec(GridView8.Rows(count).Cells(14).Text), CDec(GridView8.Rows(count).Cells(15).Text), CDec(GridView8.Rows(count).Cells(16).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Pending_RCM.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        If IsDate(TextBox16.Text) = False Then
            TextBox16.Text = ""
            TextBox16.Focus()
            Return
        ElseIf IsDate(TextBox17.Text) = False Then
            TextBox17.Text = ""
            TextBox17.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox16.Text)
        to_date = CDate(TextBox17.Text)


        conn.Open()
        dt.Clear()
        Dim quary As String = "select * from LEDGER where REVERSAL_INDICATOR='Normal' and EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by Journal_ID, VOUCHER_NO, SUPL_ID"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()

        GridView9.DataSource = dt
        GridView9.DataBind()


    End Sub

    Protected Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If GridView9.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("RIOC")

        dt3.Columns.Add(New DataColumn("Journal ID", GetType(String)))
        dt3.Columns.Add(New DataColumn("Voucher No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Supl ID", GetType(String)))
        dt3.Columns.Add(New DataColumn("Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("AC Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Debit", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Credit", GetType(Decimal)))

        For Me.count = 0 To GridView9.Rows.Count - 1
            dt3.Rows.Add(GridView9.Rows(count).Cells(0).Text, GridView9.Rows(count).Cells(1).Text, GridView9.Rows(count).Cells(2).Text, GridView9.Rows(count).Cells(3).Text, GridView9.Rows(count).Cells(4).Text, CDec(GridView9.Rows(count).Cells(5).Text), CDec(GridView9.Rows(count).Cells(6).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=JE.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Dim quary As String = ""
        Dim quary1 As String = ""
        Dim TERM As String = ""
        Dim po_no As String = ""
        Dim po_type As String = ""
        Dim pay_type As String = ""
        Dim dt1 As New DataTable
        Dim crystalReport As New ReportDocument

        quary1 = "select * from VOUCHER where TOKEN_NO='" & TextBox18.Text & "'"
        crystalReport.Load(Server.MapPath("~/print_rpt/bill_forwarding.rpt"))
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(quary1, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
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

    Protected Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        If IsDate(TextBox19.Text) = False Then
            TextBox19.Text = ""
            TextBox19.Focus()
            Return
        ElseIf TextBox19.Text = "" Then
            TextBox19.Focus()
            Return
        ElseIf IsDate(TextBox20.Text) = False Then
            TextBox20.Text = ""
            TextBox20.Focus()
            Return
        ElseIf TextBox20.Text = "" Then
            TextBox20.Focus()
            Return
        End If

        Dim from_date, to_date As Date
        Dim quary, party_code As String
        from_date = CDate(TextBox19.Text)
        to_date = CDate(TextBox20.Text)

        conn.Open()
        dt.Clear()

        If (TextBox37.Text = "") Then
            quary = "select * from BANK_GUARANTEE where BG_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by BG_NO"
        Else
            Dim dd25Text As String() = TextBox37.Text.Split(",")
            party_code = dd25Text(0).Trim
            quary = "select * from BANK_GUARANTEE where PARTY_CODE='" & party_code & "' AND BG_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by BG_NO"
        End If


        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()

        GridView10.DataSource = dt
        GridView10.DataBind()
    End Sub

    Protected Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click, Button33.Click
        If GridView10.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("BG")

        dt3.Columns.Add(New DataColumn("BG NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("BG DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("ORIGINAL BG NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("ORIGINAL_BG_DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("PARTY Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("PARTY Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("ORDER NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("ACTUAL ORDER NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("ORDER DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("IOC NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("IOC DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("RETURN BG IOC NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("RETURN BG IOC DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("DEPOSIT TYPE", GetType(String)))
        dt3.Columns.Add(New DataColumn("BG TYPE", GetType(String)))
        dt3.Columns.Add(New DataColumn("BG LOCATION", GetType(String)))
        dt3.Columns.Add(New DataColumn("ISSUING BANK NAME", GetType(String)))
        dt3.Columns.Add(New DataColumn("ISSUING BANK BRANCH", GetType(String)))
        dt3.Columns.Add(New DataColumn("BG AMOUNT", GetType(String)))
        dt3.Columns.Add(New DataColumn("BG VALIDITY", GetType(String)))
        dt3.Columns.Add(New DataColumn("CONFIRMATION LETTER NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("CONFIRMATION LETTER DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("COMPANY CONFIRMATION LETTER NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("COMPANY CONFIRMATION LETTER DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("FISCAL YEAR", GetType(String)))
        dt3.Columns.Add(New DataColumn("EMPLOYEE NAME", GetType(String)))
        dt3.Columns.Add(New DataColumn("ENTRY DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("BG STATUS", GetType(String)))


        For Me.count = 0 To GridView10.Rows.Count - 1
            dt3.Rows.Add(GridView10.Rows(count).Cells(0).Text, GridView10.Rows(count).Cells(1).Text, GridView10.Rows(count).Cells(2).Text, GridView10.Rows(count).Cells(3).Text, GridView10.Rows(count).Cells(4).Text, GridView10.Rows(count).Cells(5).Text, GridView10.Rows(count).Cells(6).Text, GridView10.Rows(count).Cells(7).Text, GridView10.Rows(count).Cells(8).Text, GridView10.Rows(count).Cells(9).Text, GridView10.Rows(count).Cells(10).Text, GridView10.Rows(count).Cells(11).Text, GridView10.Rows(count).Cells(12).Text, GridView10.Rows(count).Cells(13).Text, GridView10.Rows(count).Cells(14).Text, GridView10.Rows(count).Cells(15).Text, GridView10.Rows(count).Cells(16).Text, GridView10.Rows(count).Cells(17).Text, GridView10.Rows(count).Cells(18).Text, GridView10.Rows(count).Cells(19).Text, GridView10.Rows(count).Cells(20).Text, GridView10.Rows(count).Cells(21).Text, GridView10.Rows(count).Cells(22).Text, GridView10.Rows(count).Cells(23).Text, GridView10.Rows(count).Cells(24).Text, GridView10.Rows(count).Cells(25).Text, GridView10.Rows(count).Cells(26).Text, GridView10.Rows(count).Cells(27).Text)
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=BG_REPORT.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged
        If DropDownList10.SelectedValue = "Select" Then
            DropDownList10.Focus()

        ElseIf DropDownList10.SelectedValue = "BG Report" Then
            MultiView1.ActiveViewIndex = 0

        ElseIf DropDownList10.SelectedValue = "BG Expiry Report" Then
            MultiView1.ActiveViewIndex = 1
        End If
        GridView10.DataSource = New List(Of String)
        GridView10.DataBind()
    End Sub

    Protected Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If DropDownList11.SelectedValue = "Select" Then
            DropDownList11.Focus()
            Return
        End If

        Dim from_date, to_date As Date
        Dim quary As String
        from_date = Now
        to_date = from_date.AddMonths(CInt(DropDownList11.SelectedItem.Text))

        conn.Open()
        dt.Clear()

        quary = "select * from BANK_GUARANTEE where BG_VALIDITY between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' order by BG_NO"


        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()

        GridView10.DataSource = dt
        GridView10.DataBind()
    End Sub


    Protected Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        Dim conn1 As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)

        If IsDate(TextBox22.Text) = False Then
            TextBox22.Text = ""
            TextBox22.Focus()
            Return
        ElseIf IsDate(TextBox23.Text) = False Then
            TextBox23.Text = ""
            TextBox23.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox22.Text)
        to_date = CDate(TextBox23.Text)

        Dim FY As String = ""
        If from_date.Month > 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = FY & (FY + 1)
        ElseIf from_date.Month <= 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = (FY - 1) & FY
        End If

        Dim quary As String = ""
        Dim TERM As String = ""
        Dim crystalReport As New ReportDocument

        quary = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),OP_DEBIT DECIMAL(16,2),OP_CREDIT DECIMAL(16,2),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2),Account_group VARCHAR(200),ac_linking VARCHAR(200),P_IND INT)
            INSERT INTO @TT
            SELECT AC_NO,ac_description,0.00 AS OP_DEBIT,0.00 AS OP_CREDIT,(case when SUM(AMOUNT_DR) IS NULL THEN '0.00' else SUM(AMOUNT_DR) end) AS AMOUNT_DR,
            (case when SUM(AMOUNT_CR) IS NULL THEN '0.00' else SUM(AMOUNT_CR) end) AS AMOUNT_CR, Account_group,ac_linking, P_IND FROM LEDGER join ACDIC on 
            LEDGER.AC_NO=ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "'
            AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND (PAYMENT_INDICATION  <>'X') group by AC_NO, ac_description, Account_group,ac_linking, P_IND order by AC_NO
            
             
            INSERT INTO @TT
            SELECT ob_trial.ac_code AS AC_NO, ac_desc AS ac_description, ac_dr AS OP_DEBIT, ac_cr AS OP_CREDIT,0.00 AS AMOUNT_DR,0.00 AS AMOUNT_CR, Account_group,ac_linking, P_IND 
            FROM ob_trial JOIN ACDIC ON ob_trial.ac_code=ACDIC.ac_code
            where (ac_dr>0 OR ac_cr>0) AND ac_fy=" & FY & "
            
            SELECT '" & TextBox22.Text & " To " & TextBox23.Text & "' as duration,'" & FY & "' as fiscal_year,AC_NO,ac_description,
            SUM(OP_DEBIT) AS OB_DEBIT,SUM(OP_CREDIT) AS OB_CREDIT,SUM(AMOUNT_DR) AS AMOUNT_DR,SUM(AMOUNT_CR) AS AMOUNT_CR,Account_group,ac_linking,P_IND FROM @TT group by AC_NO, ac_description, Account_group,ac_linking,P_IND ORDER BY P_IND"




        conn1.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = quary
        mc1.Connection = conn1
        dr = mc1.ExecuteReader

        conn1.Close()


        crystalReport.Load(Server.MapPath("~/print_rpt/LinkSheet.rpt"))

        conn1.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(quary, conn1)
        da.Fill(dt)
        conn1.Close()
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
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

    Protected Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        If GridView5.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("BG")

        dt3.Columns.Add(New DataColumn("REG. NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("REG DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("ORDER NO", GetType(String)))
        dt3.Columns.Add(New DataColumn("ORDER TYPE", GetType(String)))
        dt3.Columns.Add(New DataColumn("PARTY NAME", GetType(String)))
        dt3.Columns.Add(New DataColumn("INV. NO.", GetType(String)))
        dt3.Columns.Add(New DataColumn("INV. DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("GARN/MB DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("PAYMENT DATE", GetType(String)))
        dt3.Columns.Add(New DataColumn("CHEQUE NO.", GetType(String)))
        dt3.Columns.Add(New DataColumn("AMOUNT", GetType(Double)))



        For Me.count = 0 To GridView5.Rows.Count - 1
            dt3.Rows.Add(GridView5.Rows(count).Cells(0).Text, GridView5.Rows(count).Cells(1).Text, GridView5.Rows(count).Cells(2).Text, GridView5.Rows(count).Cells(3).Text, GridView5.Rows(count).Cells(4).Text, GridView5.Rows(count).Cells(5).Text, GridView5.Rows(count).Cells(6).Text, GridView5.Rows(count).Cells(7).Text, GridView5.Rows(count).Cells(8).Text, GridView5.Rows(count).Cells(9).Text, CDec(GridView5.Rows(count).Cells(10).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Bill_Track_Report.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Protected Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
        Dim quary As String = ""
        Dim crystalReport As New ReportDocument

        'quary = "Select * from ledger where garn_no_mb_no='" & TextBox24.Text & "'"
        quary = "Select * From ledger left Join SUPL On ledger.SUPL_ID=SUPL.SUPL_ID Join ACDIC On ledger.AC_NO=ACDIC.ac_code Where GARN_NO_MB_NO ='" & TextBox24.Text & "' or GARN_NO_MB_NO='" & "CHA" & TextBox24.Text & "'"

        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = quary
        mc1.Connection = conn
        dr = mc1.ExecuteReader

        conn.Close()

        crystalReport.Load(Server.MapPath("~/print_rpt/view_ledger_entry.rpt"))

        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
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

        '''''''''''''''''''''''''''
        'Dim quary As String = ""
        'Dim TERM As String = ""
        'Dim crystalReport As New ReportDocument
        'quary = "Select * from ledger where garn_no_mb_no='" & TextBox24.Text & "'"


        'crystalReport.Load(Server.MapPath("~/print_rpt/ledger_entry.rpt"))
        'Dim I As Integer = 0
        'conn.Open()
        'Dim dt As New DataTable
        'da = New SqlDataAdapter(quary, conn)
        'I = da.Fill(dt)
        'conn.Close()
        'MsgBox(I)
        'crystalReport.SetDataSource(dt)
        'crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/REPORT.pdf"))
        'Dim url As String = "REPORT.aspx"
        'Dim sb As New StringBuilder()
        'sb.Append("<script type = 'text/javascript'>")
        'sb.Append("window.open('")
        'sb.Append(url)
        'sb.Append("');")
        'sb.Append("</script>")
        'ClientScript.RegisterStartupScript(Me.[GetType](), "script", sb.ToString())
        'crystalReport.Close()
        'crystalReport.Dispose()


    End Sub

    Protected Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        If DropDownList4.Text = "Select" Then
            DropDownList4.Focus()
            Return
        ElseIf TextBox25.Text = "" Then
            TextBox25.Focus()
            Return
        ElseIf IsDate(TextBox25.Text) = False Then
            TextBox25.Text = ""
            TextBox25.Focus()
            Return
        End If

        Dim from_date, to_date As Date

        from_date = CDate("2016-03-01")
        to_date = CDate(TextBox25.Text)

        conn.Open()
        dt.Clear()


        Dim quary As String = "DECLARE @TT TABLE(AC_NO VARCHAR(30),SUPL_ID VARCHAR(30),SUPL_NAME VARCHAR(100),ac_description VARCHAR(250),invoice_no VARCHAR(250),FISCAL_YEAR VARCHAR(250),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2))
            INSERT INTO @TT
            select AC_NO,LEDGER.SUPL_ID,SUPL_NAME,ac_description,INVOICE_NO,FISCAL_YEAR,AMOUNT_DR,AMOUNT_CR from ledger join SUPL on LEDGER.SUPL_ID=SUPL.SUPL_ID join ACDIC on LEDGER.AC_NO=ACDIC.ac_code where AC_NO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION  <>'X') order by SUPL_ID,EFECTIVE_DATE


            DECLARE @TT1 TABLE(SUPL_ID VARCHAR(250),invoice_no VARCHAR(250),FISCAL_YEAR VARCHAR(250),efective_date VARCHAR(250))
            INSERT INTO @TT1
            select distinct SUPL_ID,INVOICE_NO,FISCAL_YEAR,EFECTIVE_DATE from ledger where AC_NO='" & DropDownList4.Text.Substring(0, DropDownList4.Text.IndexOf(",") - 1).Trim & "' and EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION  <>'X') order by INVOICE_NO,EFECTIVE_DATE


            SELECT AC_NO,ac_description,T1.SUPL_ID,SUPL_NAME,t1.invoice_no,max(t2.efective_date) As efective_date,(SELECT dbo.fnc_FiscalYear(max(t2.efective_date))) As FISCAL_YEAR,sum(AMOUNT_DR) As AMOUNT_DR,sum(AMOUNT_CR) As AMOUNT_CR FROM @TT t1 join @tt1 t2 on t1.invoice_no=t2.invoice_no AND T1.FISCAL_YEAR=T2.FISCAL_YEAR AND T1.SUPL_ID=T2.SUPL_ID WHERE AMOUNT_DR <> 0 or AMOUNT_CR <> 0 group by AC_NO,T1.SUPL_ID,SUPL_NAME,ac_description,t1.invoice_no having sum(AMOUNT_CR)-sum(AMOUNT_DR) <> 0 ORDER BY AC_NO,SUPL_ID"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView11.DataSource = dt
        GridView11.DataBind()

    End Sub

    Protected Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        If TextBox26.Text = "" Then
            TextBox26.Focus()
            Return
        ElseIf IsDate(TextBox26.Text) = False Then
            TextBox26.Text = ""
            TextBox26.Focus()
            Return
        ElseIf TextBox27.Text = "" Then
            TextBox27.Focus()
            Return
        ElseIf IsDate(TextBox27.Text) = False Then
            TextBox27.Text = ""
            TextBox27.Focus()
            Return
        End If

        Dim from_date, to_date As Date
        from_date = CDate(TextBox26.Text)
        to_date = CDate(TextBox27.Text)

        Dim FY As String = ""
        If from_date.Month > 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = FY & (FY + 1)
        ElseIf from_date.Month <= 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = (FY - 1) & FY
        End If

        conn.Open()
        dt.Clear()

        Dim quary As String = "DECLARE @TT TABLE(AC_NO VARCHAR(30),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2))

            INSERT INTO @TT
            
			SELECT ac_code, sum(ac_dr) AS AMOUNT_DR,sum(ac_cr) AS AMOUNT_CR FROM ob_trial WHERE ac_fy ='" & FY & "' group by ac_code, ac_desc having (SUM(ac_dr) - SUM(ac_cr) <>0) order by ac_code

            INSERT INTO @TT
	        SELECT AC_NO,(case when SUM(AMOUNT_DR) IS NULL THEN '0.00' else SUM(AMOUNT_DR) end) AS AMOUNT_DR,
			(case when SUM(AMOUNT_CR) IS NULL THEN '0.00' else SUM(AMOUNT_CR) end) AS AMOUNT_CR FROM LEDGER join ACDIC on 
			LEDGER.AC_NO=ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'
			AND (PAYMENT_INDICATION  <>'X') group by AC_NO, ac_description order by AC_NO
			
            SELECT LEFT(T1.AC_NO,5) AS AC_NO, A1.ac_description,(case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR, 
            (case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR FROM @TT T1 JOIN ACDIC_OLD A1 ON LEFT(T1.AC_NO,5)=A1.ac_code GROUP BY LEFT(T1.AC_NO,5),A1.ac_description 
            HAVING (SUM(AMOUNT_DR) - SUM(AMOUNT_CR) <>0) ORDER BY LEFT(T1.AC_NO,5)"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView12.DataSource = dt
        GridView12.DataBind()


        Dim total_dr, total_cr As New Decimal(0)
        Dim I As Integer = 0
        For I = 0 To GridView12.Rows.Count - 1
            ''Calculating total Debit and Credit amount
            total_dr = total_dr + CDec(GridView12.Rows(I).Cells(2).Text)
            total_cr = total_cr + CDec(GridView12.Rows(I).Cells(3).Text)
        Next
        Dim dRow As DataRow
        dRow = dt.NewRow
        dRow.Item(0) = "Total"
        dt.Rows.Add(dRow)

        dt.AcceptChanges()
        GridView12.DataSource = dt
        GridView12.DataBind()

        'calculateTrialAmount(GridView12.Rows.Count - 1)
        GridView12.Rows(GridView12.Rows.Count - 1).Cells(2).Text = total_dr
        GridView12.Rows(GridView12.Rows.Count - 1).Cells(3).Text = total_cr
        GridView12.Rows(GridView12.Rows.Count - 1).Font.Bold = True
    End Sub

    Protected Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        If GridView12.Rows.Count < 0 Then
            Return
        End If


        '''''''''''''''''''''''''''''
        Dim from_date, to_date As Date
        from_date = CDate(TextBox26.Text)
        to_date = CDate(TextBox27.Text)

        Dim FY As String = ""
        If from_date.Month > 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = FY & (FY + 1)
        ElseIf from_date.Month <= 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = (FY - 1) & FY
        End If

        Using cmd As New SqlCommand("DECLARE @TT TABLE(AC_NO VARCHAR(30),AMOUNT_DR DECIMAL(16,2),AMOUNT_CR DECIMAL(16,2))

            INSERT INTO @TT
            
			SELECT ac_code, sum(ac_dr) AS AMOUNT_DR,sum(ac_cr) AS AMOUNT_CR FROM ob_trial WHERE ac_fy ='" & FY & "' group by ac_code, ac_desc having (SUM(ac_dr) - SUM(ac_cr) <>0) order by ac_code

            INSERT INTO @TT
	        SELECT AC_NO,(case when SUM(AMOUNT_DR) IS NULL THEN '0.00' else SUM(AMOUNT_DR) end) AS AMOUNT_DR,
			(case when SUM(AMOUNT_CR) IS NULL THEN '0.00' else SUM(AMOUNT_CR) end) AS AMOUNT_CR FROM LEDGER join ACDIC on 
			LEDGER.AC_NO=ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'
			AND (PAYMENT_INDICATION  <>'X') group by AC_NO, ac_description order by AC_NO
			
            SELECT LEFT(T1.AC_NO,5) AS AC_NO, A1.ac_description,(case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR, 
            (case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR FROM @TT T1 JOIN ACDIC_OLD A1 ON LEFT(T1.AC_NO,5)=A1.ac_code GROUP BY LEFT(T1.AC_NO,5),A1.ac_description 
            HAVING (SUM(AMOUNT_DR) - SUM(AMOUNT_CR) <>0) ORDER BY LEFT(T1.AC_NO,5)")


            Using sda As New SqlDataAdapter()
                cmd.Connection = conn
                sda.SelectCommand = cmd
                Using dt As New DataTable()
                    sda.Fill(dt)
                    Using wb As New XLWorkbook()
                        wb.Worksheets.Add(dt, "Trial Report")

                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        Response.AddHeader("content-disposition", "attachment;filename=Trial_Report_Merged_codes.xlsx")

                        Using MyMemoryStream As New MemoryStream()
                            wb.SaveAs(MyMemoryStream)
                            MyMemoryStream.WriteTo(Response.OutputStream)
                            Response.Flush()
                            Response.End()
                        End Using
                    End Using
                End Using
            End Using
        End Using
    End Sub

    Protected Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        If GridView12.Rows.Count = 0 Then
            Return
        End If
        Dim dt2 As New DataTable
        dt2.Columns.AddRange(New DataColumn(4) {New DataColumn("ac_no"), New DataColumn("ac_name"), New DataColumn("dr", GetType(Decimal)), New DataColumn("cr", GetType(Decimal)), New DataColumn("duration")})
        ''insert datatable value
        Dim i As Integer = 0
        i = 0
        For i = 0 To GridView12.Rows.Count - 1
            dt2.Rows.Add(GridView12.Rows(i).Cells(0).Text, GridView12.Rows(i).Cells(1).Text, GridView12.Rows(i).Cells(2).Text, GridView12.Rows(i).Cells(3).Text, TextBox26.Text + " to " + TextBox27.Text)
        Next
        Dim crystalReport As New ReportDocument
        crystalReport.Load(Server.MapPath("~/print_rpt/trial.rpt"))
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

    End Sub

    Protected Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        If TextBox28.Text = "" Then
            TextBox28.Focus()
            Return
        ElseIf IsDate(TextBox28.Text) = False Then
            TextBox28.Text = ""
            TextBox28.Focus()
            Return
        ElseIf TextBox29.Text = "" Then
            TextBox29.Focus()
            Return
        ElseIf IsDate(TextBox29.Text) = False Then
            TextBox29.Text = ""
            TextBox29.Focus()
            Return
        End If

        Dim from_date, to_date As Date
        from_date = CDate(TextBox28.Text)
        to_date = CDate(TextBox29.Text)

        Dim FY As String = ""
        If from_date.Month > 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = FY & (FY + 1)
        ElseIf from_date.Month <= 3 Then
            FY = from_date.Year
            FY = FY.Trim.Substring(2)
            FY = (FY - 1) & FY
        End If

        conn.Open()
        dt.Clear()

        Dim quary As String = "select CRR_NO,CRR_DATE,PO_RCD_MAT.SUPL_ID,SUPL_NAME,PO_RCD_MAT.MAT_CODE,MATERIAL.MAT_NAME,MAT_CHALAN_QTY,MAT_RCD_QTY,GARN_NO,GARN_NOTE from PO_RCD_MAT JOIN SUPL ON PO_RCD_MAT.SUPL_ID=SUPL.SUPL_ID JOIN MATERIAL ON PO_RCD_MAT.MAT_CODE=MATERIAL.MAT_CODE WHERE CRR_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND GARN_NO='PENDING' ORDER BY CRR_NO"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView13.DataSource = dt
        GridView13.DataBind()


    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If DropDownList5.SelectedValue = "Asset Register" Then

            MultiView3.ActiveViewIndex = 0
            Button53.Visible = True
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select * from AssetMaster", conn)
            da.Fill(dt)
            conn.Close()
            GridView15.DataSource = dt
            GridView15.DataBind()
        ElseIf DropDownList5.SelectedValue = "Depreciation Entry" Then

            MultiView3.ActiveViewIndex = 1
            Button53.Visible = False

        End If
    End Sub

    Protected Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click

        If (DropDownList6.SelectedValue = "Select") Then
        Else
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("DECLARE @TT TABLE([AssetCode] VARCHAR(30),[FISCALYEAR] VARCHAR(10),Quarter1 VARCHAR(10),
            CummDeprBeforeQ1 decimal(16,2),DeprValueQ1 DECIMAL(16,2),Quarter2 VARCHAR(10),CummDeprBeforeQ2 decimal(16,2),DeprValueQ2 DECIMAL(16,2),
            Quarter3 VARCHAR(10),CummDeprBeforeQ3 decimal(16,2),DeprValueQ3 DECIMAL(16,2),Quarter4 VARCHAR(10),CummDeprBeforeQ4 decimal(16,2),DeprValueQ4 DECIMAL(16,2))
            INSERT INTO @TT
            SELECT *,'Q2' as Quarter2, 0 as CummDeprBeforeQ2, 0 as DeprValueQ2,'Q3' as Quarter3, 0 as CummDeprBeforeQ3, 0 as DeprValueQ3,'Q4' as Quarter4, 0 as CummDeprBeforeQ4, 0 as DeprValueQ4
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ1,
                [DepreciationValue] as DeprValueQ1
                FROM AssetDepreciation where FiscalYear=" + DropDownList6.SelectedValue + " and Quarter='Q1'
            ) AS SourceTable PIVOT(SUM([DeprValueQ1]) FOR [Quarter] IN([Q1])) AS PivotTable order by AssetCode

            INSERT INTO @TT
            SELECT *,'Q3' as Quarter3, 0 as CummDeprBeforeQ3, 0 as DeprValueQ3,'Q4' as Quarter4, 0 as CummDeprBeforeQ4, 0 as DeprValueQ4
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1, 0 as CummDeprBeforeQ1, 0 as DeprValueQ1,
		        'Q2' as Quarter2,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ2,
                [DepreciationValue] as DeprValueQ2
                FROM AssetDepreciation where FiscalYear=" + DropDownList6.SelectedValue + " and Quarter='Q2'
            ) AS SourceTable PIVOT(SUM([DeprValueQ2]) FOR [Quarter] IN([Q2])) AS PivotTable order by AssetCode

            INSERT INTO @TT
            SELECT *,'Q4' as Quarter4, 0 as CummDeprBeforeQ4, 0 as DeprValueQ4
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1, 0 as CummDeprBeforeQ1, 0 as DeprValueQ1,
		        'Q2' as Quarter2, 0 as CummDeprBeforeQ2, 0 as DeprValueQ2,
		        'Q3' as Quarter3,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ3,
                [DepreciationValue] as DeprValueQ3
                FROM AssetDepreciation where FiscalYear=" + DropDownList6.SelectedValue + " and Quarter='Q3'
            ) AS SourceTable PIVOT(SUM([DeprValueQ3]) FOR [Quarter] IN([Q3])) AS PivotTable order by AssetCode

            INSERT INTO @TT
            SELECT *
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1, 0 as CummDeprBeforeQ1, 0 as DeprValueQ1,
		        'Q2' as Quarter2, 0 as CummDeprBeforeQ2, 0 as DeprValueQ2,
		        'Q3' as Quarter3, 0 as CummDeprBeforeQ3, 0 as DeprValueQ3,
		        'Q4' as Quarter4,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ4,
                [DepreciationValue] as DeprValueQ4
                FROM AssetDepreciation where FiscalYear=" + DropDownList6.SelectedValue + " and Quarter='Q4'
            ) AS SourceTable PIVOT(SUM([DeprValueQ4]) FOR [Quarter] IN([Q4])) AS PivotTable order by AssetCode

            select A1.AssetCode,Max(A1.AccountCode) as AccountCode,A1.AssetName,Max(A1.DateOfCommisioning) as DateOfCommisioning,Max(A1.PhysicalQuantity) as PhysicalQuantity,Max(A1.PhysicalLocation) as PhysicalLocation,
            Max(A1.DepreciationPercentage) as DepreciationPercentage,Max(A1.GrossBlock) as GrossBlock,Max(A1.CummulativeDepriciation) as CummulativeDepriciation,max(T1.FISCALYEAR) as fiscalYear,
            Max(T1.Quarter1) as Quarter1,sum(T1.CummDeprBeforeQ1) as CummDeprBeforeQ1,sum(T1.DeprValueQ1) as DeprValueQ1,max(T1.Quarter2) as Quarter2,sum(T1.CummDeprBeforeQ2) as CummDeprBeforeQ2,sum(T1.DeprValueQ2) as DeprValueQ2,
            max(T1.Quarter3) as Quarter3,sum(T1.CummDeprBeforeQ3) as CummDeprBeforeQ3,sum(T1.DeprValueQ3) as DeprValueQ3,max(T1.Quarter4) as Quarter4,sum(T1.CummDeprBeforeQ4) as CummDeprBeforeQ4,sum(T1.DeprValueQ4) as DeprValueQ4,max(A1.Remarks) as Remarks from AssetMaster A1 join @TT T1 on A1.AssetCode=T1.AssetCode group by A1.AssetCode,A1.AssetName order by AccountCode", conn)
            da.Fill(dt)
            conn.Close()
            GridView14.DataSource = dt
            GridView14.DataBind()

        End If



    End Sub

    Protected Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        If GridView15.Rows.Count > 0 Then
            Try
                ''GridView15.Columns(0).Visible = False
                Response.ClearContent()
                Response.Buffer = True
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "AssetRegister.xlsx"))
                Response.ContentEncoding = Encoding.UTF8
                Response.ContentType = "application/ms-excel"
                Dim sw As New StringWriter()
                Dim htw As New HtmlTextWriter(sw)
                GridView15.RenderControl(htw)
                Response.Write(sw.ToString())
                Response.[End]()

            Catch ex As Exception
            Finally

            End Try
        End If
    End Sub

    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        If GridView14.Rows.Count > 0 Then
            Try
                ''GridView14.Columns(0).Visible = False
                Response.ClearContent()
                Response.Buffer = True
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "AssetDepreciation_" + DropDownList6.Text + ".xlsx"))
                Response.ContentEncoding = Encoding.UTF8
                Response.ContentType = "application/ms-excel"
                Dim sw As New StringWriter()
                Dim htw As New HtmlTextWriter(sw)
                GridView14.RenderControl(htw)
                Response.Write(sw.ToString())
                Response.[End]()

            Catch ex As Exception
            Finally

            End Try
        End If
    End Sub

    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        If TextBox8.Text = "" Then
            TextBox8.Focus()
            Return
        ElseIf IsDate(TextBox8.Text) = False Then
            TextBox8.Text = ""
            TextBox8.Focus()
            Return
        ElseIf TextBox9.Text = "" Then
            TextBox9.Focus()
            Return
        ElseIf IsDate(TextBox9.Text) = False Then
            TextBox9.Text = ""
            TextBox9.Focus()
            Return
        End If
        Dim from_date, to_date As Date

        from_date = CDate(TextBox8.Text)
        to_date = CDate(TextBox9.Text)

        Dim STR1 As String = ""
        If from_date.Month > 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf from_date.Month <= 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If


        If DropDownList2.Text = "All" Then
            conn.Open()
            dt.Clear()

            Dim quary As String = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),PO_NO VARCHAR(30),GARN VARCHAR(250),VOUCHER_NO VARCHAR(30),EFECTIVE_DATE DATE,DR DECIMAL(16,2),CR DECIMAL(16,2))
            INSERT INTO @TT
            SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, CONVERT(varchar, LEDGER.VOUCHER_NO) As VOUCHER_NO, 
            CONVERT(date, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy'),103) As EFECTIVE_DATE, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR AS CR 
            FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND PAYMENT_INDICATION <>'X' ORDER BY LEDGER.AC_NO ,LEDGER.EFECTIVE_DATE 

            INSERT INTO @TT
            SELECT ac_code AS AC_NO, ac_desc AS ac_description, '' AS PO_NO, 'OPENING' AS GARN,'' AS VOUCHER_NO, CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS EFECTIVE_DATE, 
            ac_dr AS DR, ac_cr AS CR FROM ob_trial where (ac_dr>0 OR ac_cr>0) AND ac_fy=" & STR1 & "
            
            SELECT * FROM @TT ORDER BY AC_NO, EFECTIVE_DATE"
            da = New SqlDataAdapter(quary, conn)
            da.Fill(dt)
            conn.Close()
            GridView4.DataSource = dt
            GridView4.DataBind()
        Else
            conn.Open()
            dt.Clear()
            Dim quary As String = "DECLARE @TT TABLE(AC_NO VARCHAR(30),ac_description VARCHAR(250),PO_NO VARCHAR(30),GARN VARCHAR(250),VOUCHER_NO VARCHAR(30),EFECTIVE_DATE DATE,DR DECIMAL(16,2),CR DECIMAL(16,2))
            INSERT INTO @TT
            SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, CONVERT(varchar, LEDGER.VOUCHER_NO) As VOUCHER_NO, 
            CONVERT(date, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy'),103) As EFECTIVE_DATE, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR AS CR 
            FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO='" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1).Trim & "' 
            AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
            AND PAYMENT_INDICATION <>'X' ORDER BY LEDGER.AC_NO ,LEDGER.EFECTIVE_DATE 
            INSERT INTO @TT
            SELECT ac_code AS AC_NO, ac_desc AS ac_description, '' AS PO_NO, 'OPENING' AS GARN,'' AS VOUCHER_NO, CONVERT(DATE, '01-04-20' + '" & Left(STR1, 2) & "', 103) AS EFECTIVE_DATE, 
            ac_dr AS DR, ac_cr AS CR FROM ob_trial 
            where ac_code='" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1).Trim & "' AND (ac_dr>0 OR ac_cr>0) AND ac_fy=" & STR1 & "
            
            SELECT * FROM @TT ORDER BY AC_NO, EFECTIVE_DATE"
            da = New SqlDataAdapter(quary, conn)
            da.Fill(dt)
            conn.Close()
            GridView4.DataSource = dt
            GridView4.DataBind()
        End If

        Dim total_dr, total_cr As New Decimal(0)
        Dim I As Integer = 0
        For I = 0 To GridView4.Rows.Count - 1
            ''Calculating total Debit and Credit amount
            total_dr = total_dr + CDec(GridView4.Rows(I).Cells(6).Text)
            total_cr = total_cr + CDec(GridView4.Rows(I).Cells(7).Text)
        Next
        Dim dRow1 As DataRow
        dRow1 = dt.NewRow
        dRow1.Item(0) = "Total"
        dt.Rows.Add(dRow1)

        dt.AcceptChanges()

        GridView4.DataSource = dt
        GridView4.DataBind()

        GridView4.Rows(GridView4.Rows.Count - 1).Cells(6).Text = total_dr
        GridView4.Rows(GridView4.Rows.Count - 1).Cells(7).Text = total_cr
        GridView4.Rows(GridView4.Rows.Count - 1).Font.Bold = True

        ''''''''''''''''''''''''''''''''''''''''
    End Sub
End Class
