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
Public Class Store_physical_adjustment
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim ac_pur, ac_issue, ac_con As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (GROUP_NAME + ' , ' + GROUP_CODE) AS BIN_DET from BIN_GROUP WHERE GROUP_TYPE='STORE MATERIAL' order by GROUP_CODE", conn)
            da.Fill(dt)
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "BIN_DET"
            DropDownList2.DataBind()
            conn.Close()
            DropDownList2.Items.Insert(0, "Select")
            DropDownList2.SelectedValue = "Select"

            Panel1.Visible = True

        End If

        TextBox49_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub


    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Return
        End If
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from MATERIAL where MAT_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label276.Text = dr.Item("MAT_AU")
            Label290.Text = dr.Item("MAT_AU")
            TextBox1.Text = dr.Item("MAT_STOCK")
            TextBox2.Text = dr.Item("MAT_AVG")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()


    End Sub



    Protected Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return

        ElseIf DropDownList5.SelectedValue = "Phy. Adjust" Then
            'adjust
            If IsDate(TextBox49.Text) = False Then
                Label279.Text = "Please Select Date"
                Return
            ElseIf TextBox56.Text = "" Then
                Label279.Text = "Please Enter Qty."
                Return
            ElseIf IsNumeric(TextBox56.Text) = False Then
                Label279.Text = "Please Enter Numaric Value In Qty."
                Return

            End If

            'INSERT INTO MAT DETAILS FOR ADJUSTMENT

            Dim item_code1 As String
            item_code1 = DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim

            MAT_DETAILS_ADJUST(item_code1, CDate(TextBox49.Text), CDec(TextBox56.Text), Session("userName"))

            DropDownList5.SelectedValue = "Select"
            Label279.Text = "Stock Adjusted"
        End If



    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Phy. Adjust" Then
            Panel3.Visible = True
        End If
    End Sub

    Protected Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        DropDownList5.SelectedValue = "Select"
        TextBox49.Text = ""
        TextBox56.Text = ""
    End Sub
    Private Sub MAT_DETAILS_ADJUST(ITEM_CODE As String, ADJ_DATE As Date, ADJ_QTY As Decimal, name_user As String)

        Dim NEW_MAT_STOCK As New Decimal(0)
        Dim flag As Boolean
        Dim STR1 As String = ""
        If ADJ_DATE.Month > 3 Then
            STR1 = ADJ_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf ADJ_DATE.Month <= 3 Then
            STR1 = ADJ_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        ''ADJUSTMENT LINE NO
        Dim ADJ_TYPE As String = "ADJ"

        Dim LINE_NO As Integer
        Dim MC As New SqlCommand
        conn.Open()
        MC.CommandText = "select max(LINE_NO) AS LINE_NO from MAT_DETAILS where MAT_CODE= '" & ITEM_CODE & "' and FISCAL_YEAR= " & STR1
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            If IsDBNull(dr.Item("LINE_NO")) Then
                LINE_NO = 0
            Else
                LINE_NO = dr.Item("LINE_NO")
            End If
            dr.Close()
            conn.Close()
        Else
            conn.Close()
        End If

        conn.Open()
        ds.Clear()
        da = New SqlDataAdapter("SELECT distinct(ISSUE_NO) FROM MAT_DETAILS WHERE ISSUE_NO LIKE '%STOREADJ%' and FISCAL_YEAR=" & STR1, conn)
        count = da.Fill(dt)
        conn.Close()

        If count = 0 Then
            Adj_no_TextBox.Text = "STOREADJ" & STR1 & "000001"
        Else
            str = count + 1
            If str.Length = 1 Then
                str = "00000" & (count + 1)
            ElseIf str.Length = 2 Then
                str = "0000" & (count + 1)
            ElseIf str.Length = 3 Then
                str = "000" & (count + 1)
            ElseIf str.Length = 4 Then
                str = "00" & (count + 1)
            ElseIf str.Length = 5 Then
                str = "0" & (count + 1)
            End If
            Adj_no_TextBox.Text = "STOREADJ" & STR1 & str
        End If

        Dim month1 As Integer = 0
        month1 = ADJ_DATE.Month
        Dim qtr1 As String = ""
        If month1 = 4 Or month1 = 5 Or month1 = 6 Then
            qtr1 = "Q1"
        ElseIf month1 = 7 Or month1 = 8 Or month1 = 9 Then
            qtr1 = "Q2"
        ElseIf month1 = 10 Or month1 = 11 Or month1 = 12 Then
            qtr1 = "Q3"
        ElseIf month1 = 1 Or month1 = 2 Or month1 = 3 Then
            qtr1 = "Q4"
        End If

        conn.Open()
        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into MAT_DETAILS(ENTRY_DATE,DEPT_CODE,PURPOSE,AUTH_BY,POST_TYPE,REMARKS,RQD_DATE,RQD_QTY,AVG_PRICE,ISSUE_NO,LINE_NO,LINE_DATE,FISCAL_YEAR,LINE_TYPE,MAT_CODE,MAT_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,QTR,ISSUE_TYPE,COST_CODE,ISSUE_QTY)VALUES(@ENTRY_DATE,@DEPT_CODE,@PURPOSE,@AUTH_BY,@POST_TYPE,@REMARKS,@RQD_DATE,@RQD_QTY,@AVG_PRICE,@ISSUE_NO,@LINE_NO,@LINE_DATE,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@MAT_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@QTR,@ISSUE_TYPE,@COST_CODE,@ISSUE_QTY)"
        Dim cmd1 As New SqlCommand(QUARY1, conn)

        cmd1.Parameters.AddWithValue("@ISSUE_NO", Adj_no_TextBox.Text)
        cmd1.Parameters.AddWithValue("@LINE_NO", LINE_NO + 1)
        cmd1.Parameters.AddWithValue("@ISSUE_TYPE", "ADJUSTMENT")
        cmd1.Parameters.AddWithValue("@LINE_DATE", ADJ_DATE)
        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
        cmd1.Parameters.AddWithValue("@LINE_TYPE", "A")
        cmd1.Parameters.AddWithValue("@MAT_CODE", ITEM_CODE)
        If (ADJ_QTY > 0) Then
            flag = True
            NEW_MAT_STOCK = CDec(TextBox1.Text) + ADJ_QTY
            cmd1.Parameters.AddWithValue("@MAT_QTY", ADJ_QTY)
            cmd1.Parameters.AddWithValue("@RQD_QTY", 0)
            cmd1.Parameters.AddWithValue("@ISSUE_QTY", 0)
            cmd1.Parameters.AddWithValue("@MAT_BALANCE", NEW_MAT_STOCK)
        Else
            flag = False
            ADJ_QTY = ADJ_QTY * (-1)
            NEW_MAT_STOCK = CDec(TextBox1.Text) - ADJ_QTY
            cmd1.Parameters.AddWithValue("@MAT_QTY", 0)
            cmd1.Parameters.AddWithValue("@RQD_QTY", ADJ_QTY)
            cmd1.Parameters.AddWithValue("@ISSUE_QTY", ADJ_QTY)
            cmd1.Parameters.AddWithValue("@MAT_BALANCE", NEW_MAT_STOCK)
        End If


        cmd1.Parameters.AddWithValue("@UNIT_PRICE", CDec(TextBox2.Text))
        cmd1.Parameters.AddWithValue("@TOTAL_PRICE", ADJ_QTY * CDec(TextBox2.Text))
        cmd1.Parameters.AddWithValue("@DEPT_CODE", "SM")
        cmd1.Parameters.AddWithValue("@PURPOSE", "FOR PHYSICAL ADJ.")
        cmd1.Parameters.AddWithValue("@COST_CODE", "070000")
        cmd1.Parameters.AddWithValue("@AUTH_BY", name_user)
        cmd1.Parameters.AddWithValue("@POST_TYPE", "AUTH")
        cmd1.Parameters.AddWithValue("@REMARKS", "For general purpose")
        cmd1.Parameters.AddWithValue("@RQD_DATE", ADJ_DATE)
        cmd1.Parameters.AddWithValue("@QTR", qtr1)
        cmd1.Parameters.AddWithValue("@AVG_PRICE", CDec(TextBox2.Text))
        cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
        cmd1.ExecuteReader()
        cmd1.Dispose()
        conn.Close()

        ''''Updating current stock Qty
        TextBox1.Text = NEW_MAT_STOCK

        'update Material
        conn.Open()
        QUARY1 = ""
        QUARY1 = "update MATERIAL set MAT_STOCK=@MAT_STOCK where MAT_CODE ='" & ITEM_CODE & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn)
        cmd2.Parameters.AddWithValue("@MAT_STOCK", NEW_MAT_STOCK)
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()

        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from MATERIAL where MAT_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label276.Text = dr.Item("MAT_AU")
            Label290.Text = dr.Item("MAT_AU")
            TextBox1.Text = dr.Item("MAT_STOCK")
            TextBox2.Text = dr.Item("MAT_AVG")
            ac_pur = dr.Item("AC_PUR")
            ac_issue = dr.Item("AC_ISSUE")
            ac_con = dr.Item("AC_CON")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        ''Save ledger
        If (flag = True) Then
            save_ledger("Physical Adjustment", Adj_no_TextBox.Text, "", "", ac_issue, "Dr", ADJ_QTY * CDec(TextBox2.Text), "SM_Physical_Adj", "", 1, "")
            save_ledger("Physical Adjustment", Adj_no_TextBox.Text, "", "", ac_con, "Cr", ADJ_QTY * CDec(TextBox2.Text), "SM_Physical_Adj", "", 2, "")
        Else
            save_ledger("Physical Adjustment", Adj_no_TextBox.Text, "", "", ac_con, "Dr", ADJ_QTY * CDec(TextBox2.Text), "SM_Physical_Adj", "", 1, "")
            save_ledger("Physical Adjustment", Adj_no_TextBox.Text, "", "", ac_issue, "Cr", ADJ_QTY * CDec(TextBox2.Text), "SM_Physical_Adj", "", 2, "")
        End If


    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct(MAT_CODE +' , '+ MAT_NAME) as mat_detail from MATERIAL where MAT_CODE not like '100%' and MAT_CODE like '" & DropDownList2.Text.Substring(DropDownList2.Text.IndexOf(",") + 2, 3).Trim & "%' order by mat_detail", conn)
        da.Fill(dt)

        DropDownList1.DataSource = dt
        DropDownList1.DataValueField = "mat_detail"
        DropDownList1.DataBind()
        conn.Close()
        DropDownList1.Items.Insert(0, "Select")
        DropDownList1.SelectedValue = "Select"

    End Sub

    Protected Sub save_ledger(so_no As String, garn_mb As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, token_no As String, line_no As Integer, PAY_IND As String)

        Dim working_date As Date
        If TextBox49.Text = "" Then
            TextBox49.Focus()
            Return
        ElseIf IsDate(TextBox49.Text) = False Then
            TextBox49.Text = ""
            TextBox49.Focus()
            Return
        End If
        working_date = CDate(TextBox49.Text)
        If price > 0 Then
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
            Dim month1 As Integer
            month1 = working_date.Month
            Dim qtr1 As String = ""
            If month1 = 4 Or month1 = 5 Or month1 = 6 Then
                qtr1 = "Q1"
            ElseIf month1 = 7 Or month1 = 8 Or month1 = 9 Then
                qtr1 = "Q2"
            ElseIf month1 = 10 Or month1 = 11 Or month1 = 12 Then
                qtr1 = "Q3"
            ElseIf month1 = 1 Or month1 = 2 Or month1 = 3 Then
                qtr1 = "Q4"
            End If
            Dim dr_value, cr_value As Decimal
            dr_value = 0.0
            cr_value = 0.0
            If ac_term = "Dr" Then
                dr_value = price
                cr_value = 0.0
            ElseIf ac_term = "Cr" Then
                dr_value = 0.0
                cr_value = price
            End If
            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(PAYMENT_INDICATION,JURNAL_LINE_NO,PO_NO,GARN_NO_MB_NO,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION)VALUES(@PAYMENT_INDICATION,@JURNAL_LINE_NO,@PO_NO,@GARN_NO_MB_NO,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PO_NO", so_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", garn_mb)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@JURNAL_LINE_NO", line_no)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
            ''
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox49.Text = "" Then
            TextBox49.Focus()
            Return
        ElseIf IsDate(TextBox49.Text) = False Then
            TextBox49.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        End If
        Dim dpr_date As Date
        dpr_date = CDate(TextBox49.Text)
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        Dim PO_QUARY As String = "select distinct PROD_CONTROL .ITEM_CODE,qual_group.qual_name ,F_ITEM .ITEM_NAME ,F_ITEM .MAT_AU , " &
            " '" & dpr_date & "'  AS P_DATE ," _
            & " '" & dpr_date & "'  AS P_DATE_TO , '" & DropDownList5.SelectedValue & "' AS R_TYPE, " _
 & " (case when f_item.MAT_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY))else '0.000' end) as F_QTY, " _
 & " CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY*F_ITEM .ITEM_WEIGHT)/1000)  as F_MT," _
 & " (case when f_item.MAT_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_B_QTY))else '0.000' end) AS B_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_B_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS B_MT ," _
 & " (case when f_item.MAT_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_I_QTY))else '0.000' end) AS INV_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_I_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS INV_MT" _
 & " from PROD_CONTROL join F_ITEM on PROD_CONTROL .ITEM_CODE =F_ITEM .ITEM_CODE" _
 & " join qual_group ON F_ITEM .ITEM_TYPE =qual_group .qual_code" _
 & " where PROD_CONTROL .PROD_DATE = '" & dpr_date.Year & "-" & dpr_date.Month & "-" & dpr_date.Day & "' and ITEM_I_SO ='" & DropDownList5.SelectedValue & "'" _
 & " group by PROD_CONTROL .ITEM_CODE,F_ITEM .ITEM_NAME,qual_group.qual_name,F_ITEM .MAT_AU"
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