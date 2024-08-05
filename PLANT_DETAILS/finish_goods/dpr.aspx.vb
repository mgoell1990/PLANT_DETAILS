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
Public Class dpr1
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
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

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct (F_ITEM.ITEM_TYPE + ' , ' + qual_group.qual_name) AS group_name FROM qual_group join F_ITEM  on F_ITEM.ITEM_TYPE=qual_group.qual_code ORDER BY group_name ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "group_name"
            DropDownList2.DataBind()
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM where ITEM_TYPE='" & DropDownList2.Text.Substring(0, (DropDownList2.Text.IndexOf(",") - 1)).Trim & "' ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "ITEM_CODE"
            DropDownList1.DataBind()
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT F_ITEM .ITEM_CODE,F_ITEM.ITEM_NAME,F_ITEM.ITEM_DRAW,F_ITEM.ITEM_AU,F_ITEM.ITEM_WEIGHT,F_ITEM.ITEM_F_STOCK,F_ITEM.ITEM_B_STOCK,F_ITEM.ITEM_LAST_PROD,F_ITEM.ITEM_LAST_DESPATCH ,(select(case when SUM(ITEM_F_QTY) is null then '0.0000' else SUM(ITEM_F_QTY) end) from PROD_CONTROL where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' and PROD_DATE >='" & working_date.Year & "-" & working_date.Month & "- 01') as cmul_qty from F_ITEM where F_ITEM.ITEM_CODE ='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "'", conn)
            da.Fill(dt)
            conn.Close()
            DetailsView1.DataSource = dt
            DetailsView1.DataBind()
            DetailsView1.Rows(5).Cells(0).Text = "Current Stock (" & DetailsView1.Rows(3).Cells(1).Text & ")"
            DetailsView1.Rows(6).Cells(1).Text = FormatNumber(CDec(DetailsView1.Rows(4).Cells(1).Text) * CDec(DetailsView1.Rows(5).Cells(1).Text) / 1000, 3)

            Dim mc1 As New SqlCommand
            conn.Open()
            mc1.CommandText = "select QUAL_DESC from QUAL_GROUP where QUAL_CODE='" & DropDownList2.Text.Substring(0, 3).Trim & "' "
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                DetailsView1.Rows(10).Cells(1).Text = dr.Item("QUAL_DESC")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            conn.Open()

            mc1.CommandText = "select ITEM_AU from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label276.Text = dr.Item("ITEM_AU")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
            If (Label276.Text = "Pcs" Or Label276.Text = "PCS") Then
                TextBox1.Text = 0
                ''TextBox50.Text = 0
            Else
                TextBox1.Text = "0.0000"
                ''TextBox50.Text = "0.0000"
            End If
            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(5) {New DataColumn("ITEM_CODE"), New DataColumn("ITEM_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_PROD_DATE"), New DataColumn("ITEM_QTY"), New DataColumn("fr_mt")})
            ViewState("mat1") = dt2
            Me.BINDGRID1()
            ''Panel1.Visible = True
        End If
        TextBox49_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub BINDGRID1()
        GridView1.DataSource = DirectCast(ViewState("mat1"), DataTable)
        GridView1.DataBind()
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Dim working_date As Date

        working_date = Today.Date
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM where ITEM_TYPE='" & DropDownList2.Text.Substring(0, (DropDownList2.Text.IndexOf(",") - 1)).Trim & "' ", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList1.DataSource = dt
        DropDownList1.DataValueField = "ITEM_CODE"
        DropDownList1.DataBind()
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT F_ITEM .ITEM_CODE,F_ITEM.ITEM_NAME,F_ITEM.ITEM_DRAW,F_ITEM.ITEM_AU,F_ITEM.ITEM_WEIGHT,F_ITEM.ITEM_F_STOCK,F_ITEM.ITEM_B_STOCK,F_ITEM.ITEM_LAST_PROD,F_ITEM.ITEM_LAST_DESPATCH ,(select(case when SUM(ITEM_F_QTY) is null then '0.0000' else SUM(ITEM_F_QTY) end)  from PROD_CONTROL where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' and PROD_DATE >='" & working_date.Year & "-" & working_date.Month & "- 01') as cmul_qty from F_ITEM where F_ITEM.ITEM_CODE ='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "'", conn)
        da.Fill(dt)
        conn.Close()
        DetailsView1.DataSource = dt
        DetailsView1.DataBind()
        'DetailsView1.Rows(5).Cells(0).Text = "Cur Stock FR (" & DetailsView1.Rows(3).Cells(1).Text & ")"
        'DetailsView1.Rows(6).Cells(0).Text = "Cur Stock BSR (" & DetailsView1.Rows(3).Cells(1).Text & ")"
        'DetailsView1.Rows(8).Cells(1).Text = FormatNumber((CDec(DetailsView1.Rows(4).Cells(1).Text) * CDec(DetailsView1.Rows(6).Cells(1).Text)) / 1000, 4)
        'DetailsView1.Rows(7).Cells(1).Text = FormatNumber((CDec(DetailsView1.Rows(4).Cells(1).Text) * CDec(DetailsView1.Rows(5).Cells(1).Text)) / 1000, 4)
        DetailsView1.Rows(5).Cells(0).Text = "Current Stock (" & DetailsView1.Rows(3).Cells(1).Text & ")"
        DetailsView1.Rows(6).Cells(1).Text = FormatNumber(CDec(DetailsView1.Rows(4).Cells(1).Text) * CDec(DetailsView1.Rows(5).Cells(1).Text) / 1000, 3)
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ITEM_AU from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label276.Text = dr.Item("ITEM_AU")
            ''Label277.Text = dr.Item("ITEM_AU")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If (Label276.Text = "Pcs" Or Label276.Text = "PCS") Then
            TextBox1.Text = 0
            ''TextBox50.Text = 0
        Else
            TextBox1.Text = "0.0000"
            ''TextBox50.Text = "0.0000"
        End If

        conn.Open()
        mc1.CommandText = "select QUAL_DESC from QUAL_GROUP where QUAL_CODE='" & DropDownList2.Text.Substring(0, 3).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            DetailsView1.Rows(10).Cells(1).Text = dr.Item("QUAL_DESC")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim working_date As Date

        working_date = Today.Date
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT F_ITEM .ITEM_CODE,F_ITEM.ITEM_NAME,F_ITEM.ITEM_DRAW,F_ITEM.ITEM_AU,F_ITEM.ITEM_WEIGHT,F_ITEM.ITEM_F_STOCK,F_ITEM.ITEM_B_STOCK,F_ITEM.ITEM_LAST_PROD,F_ITEM.ITEM_LAST_DESPATCH ,(select(case when SUM(ITEM_F_QTY) is null then '0.0000' else SUM(ITEM_F_QTY) end) from PROD_CONTROL where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' and PROD_DATE >='" & working_date.Year & "-" & working_date.Month & "- 01') as cmul_qty from F_ITEM where F_ITEM.ITEM_CODE ='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "'", conn)
        da.Fill(dt)
        conn.Close()
        DetailsView1.DataSource = dt
        DetailsView1.DataBind()
        'DetailsView1.Rows(5).Cells(0).Text = "Cur Stock FR (" & DetailsView1.Rows(3).Cells(1).Text & ")"
        ''DetailsView1.Rows(6).Cells(0).Text = "Cur Stock BSR (" & DetailsView1.Rows(3).Cells(1).Text & ")"
        'DetailsView1.Rows(7).Cells(1).Text = FormatNumber((CDec(DetailsView1.Rows(4).Cells(1).Text) * CDec(DetailsView1.Rows(5).Cells(1).Text)) / 1000, 4)
        ''DetailsView1.Rows(8).Cells(1).Text = FormatNumber((CDec(DetailsView1.Rows(4).Cells(1).Text) * CDec(DetailsView1.Rows(6).Cells(1).Text)) / 1000, 4)

        DetailsView1.Rows(5).Cells(0).Text = "Current Stock (" & DetailsView1.Rows(3).Cells(1).Text & ")"
        DetailsView1.Rows(6).Cells(1).Text = FormatNumber(CDec(DetailsView1.Rows(4).Cells(1).Text) * CDec(DetailsView1.Rows(5).Cells(1).Text) / 1000, 3)
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ITEM_AU from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label276.Text = dr.Item("ITEM_AU")
            ''Label277.Text = dr.Item("ITEM_AU")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If (Label276.Text = "Pcs" Or Label276.Text = "PCS") Then
            TextBox1.Text = 0
            ''TextBox50.Text = 0
        Else
            TextBox1.Text = "0.0000"
            ''TextBox50.Text = "0.0000"
        End If

        conn.Open()
        mc1.CommandText = "select QUAL_DESC from QUAL_GROUP where QUAL_CODE='" & DropDownList2.Text.Substring(0, 3).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            DetailsView1.Rows(10).Cells(1).Text = dr.Item("QUAL_DESC")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click


        If IsDate(TextBox49.Text) = False Then
            TextBox49.Focus()
            Label279.Text = "Please Select Date"
            Return
        ElseIf IsNumeric(TextBox1.Text) = False Then
            TextBox1.Focus()
            Label279.Text = "Please Enter Numaric Value In FR Qty"
            Return

        ElseIf CDec(TextBox1.Text = 0) Then
            TextBox1.Focus()
            Label279.Text = "Please Enter Some Value In Production Qty"
            Return

        ElseIf (Label276.Text = "Pcs" Or Label276.Text = "PCS") Then
            Dim output As Integer
            If Integer.TryParse(TextBox1.Text, output) = False Then
                TextBox1.Focus()
                Label279.Text = "Don't Enter Decimal Value In FR Qty"
                Return

            End If
        ElseIf CDec(TextBox1.Text) < 0 Then
            TextBox1.Focus()
            Label279.Text = "Please Enter Positive Value In FR Qty"
            Return

        End If
        Dim date_diff As Integer = 0
        date_diff = DateDiff(DateInterval.Day, CDate(TextBox49.Text), Today.Date)

        'If date_diff > 3 Or date_diff < 0 Then
        '    TextBox49.Focus()
        '    TextBox49.Text = ""
        '    Label279.Text = "Please Select Date"
        '    Return
        'End If

        Dim unit_weight As Decimal = 0.0
        Dim item_au As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            unit_weight = dr.Item("ITEM_WEIGHT")
            item_au = dr.Item("ITEM_AU")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        Dim FR_MT, BSR_MT As Decimal
        FR_MT = 0.0
        BSR_MT = 0.0
        If (item_au = "Pcs" Or item_au = "PCS") Then
            FR_MT = FormatNumber(CDec((TextBox1.Text) * unit_weight) / 1000, 3)
            ''BSR_MT = FormatNumber(CDec((TextBox50.Text) * unit_weight) / 1000, 3)
        ElseIf (item_au = "Mt" Or item_au = "MT" Or item_au = "MTS") Then
            FR_MT = FormatNumber(CDec(TextBox1.Text), 3)
            ''BSR_MT = FormatNumber(CDec(TextBox50.Text), 3)
        End If
        Label279.Text = ""
        Dim dt As DataTable = DirectCast(ViewState("mat1"), DataTable)
        dt.Rows.Add(DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim, DropDownList1.Text.Substring((DropDownList1.Text.IndexOf(",") + 2)), item_au, TextBox49.Text, TextBox1.Text, FR_MT)
        ViewState("mat1") = dt
        Me.BINDGRID1()

    End Sub

    Protected Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim fr_stock, bsr_stock As Decimal
                fr_stock = 0
                bsr_stock = 0
                If GridView1.Rows.Count = 0 Then
                    Label279.Text = "Please Add Some Data First"
                    Return
                End If
                Dim i As Integer = 0
                working_date = CDate(TextBox49.Text)
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
                For i = 0 To GridView1.Rows.Count - 1
                    ''finish stock,bsr stock
                    conn.Open()
                    Dim mc1 As New SqlCommand

                    mc1.CommandText = "select (case when (ITEM_F_STOCK) is null then '0' else (ITEM_F_STOCK) end ) as fr_stock ,(case when (ITEM_B_STOCK) is null then '0' else (ITEM_B_STOCK) end ) as bsr_stock from F_ITEM where ITEM_CODE='" & GridView1.Rows(i).Cells(0).Text & "' "
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        fr_stock = dr.Item("fr_stock")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                    'conn.Open()
                    Dim QUARY1 As String = ""
                    QUARY1 = "Insert Into PROD_CONTROL(QUALITY,ENTRY_DATE,fiscal_year,name_user,ITEM_CODE,PROD_DATE,ITEM_F_QTY,ITEM_I_QTY,ITEM_I_SO,ITEM_F_STOCK,ITEM_I_TOTAL)values(@QUALITY,@ENTRY_DATE,@fiscal_year,@name_user,@ITEM_CODE,@PROD_DATE,@ITEM_F_QTY,@ITEM_I_QTY,@ITEM_I_SO,@ITEM_F_STOCK,@ITEM_I_TOTAL)"
                    Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmd1.Parameters.AddWithValue("@ITEM_CODE", GridView1.Rows(i).Cells(0).Text)
                    cmd1.Parameters.AddWithValue("@PROD_DATE", Date.ParseExact(GridView1.Rows(i).Cells(3).Text, "dd-MM-yyyy", provider))
                    cmd1.Parameters.AddWithValue("@ITEM_F_QTY", CDec(GridView1.Rows(i).Cells(4).Text))
                    cmd1.Parameters.AddWithValue("@ITEM_I_QTY", 0.0)
                    cmd1.Parameters.AddWithValue("@ITEM_I_SO", "")
                    cmd1.Parameters.AddWithValue("@ITEM_F_STOCK", (fr_stock + CDec(GridView1.Rows(i).Cells(4).Text)))
                    cmd1.Parameters.AddWithValue("@ITEM_I_TOTAL", 0.0)
                    cmd1.Parameters.AddWithValue("@name_user", Session("userName"))
                    cmd1.Parameters.AddWithValue("@fiscal_year", STR1)
                    cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
                    cmd1.Parameters.AddWithValue("@QUALITY", DetailsView1.Rows(10).Cells(1).Text)
                    cmd1.ExecuteReader()
                    cmd1.Dispose()
                    'conn.Close()
                    ''update f_item
                    'conn.Open()
                    QUARY1 = ""
                    QUARY1 = "update F_ITEM set ITEM_F_STOCK=@ITEM_F_STOCK,ITEM_LAST_PROD=@ITEM_LAST_PROD where ITEM_CODE ='" & GridView1.Rows(i).Cells(0).Text & "'"
                    Dim cmd2 As New SqlCommand(QUARY1, conn_trans, myTrans)
                    cmd2.Parameters.AddWithValue("@ITEM_F_STOCK", (fr_stock + CDec(GridView1.Rows(i).Cells(4).Text)))
                    cmd2.Parameters.AddWithValue("@ITEM_LAST_PROD", Date.ParseExact(GridView1.Rows(i).Cells(3).Text, "dd-MM-yyyy", provider))
                    cmd2.ExecuteReader()
                    cmd2.Dispose()
                    'conn.Close()
                Next
                Dim dt2 As New DataTable()
                dt2.Columns.AddRange(New DataColumn(5) {New DataColumn("ITEM_CODE"), New DataColumn("ITEM_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_PROD_DATE"), New DataColumn("ITEM_QTY"), New DataColumn("fr_mt")})
                ViewState("mat1") = dt2
                Me.BINDGRID1()

                myTrans.Commit()
                Label279.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label279.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub

    Protected Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim dt2 As New DataTable()
        dt2.Columns.AddRange(New DataColumn(7) {New DataColumn("ITEM_CODE"), New DataColumn("ITEM_NAME"), New DataColumn("ITEM_AU"), New DataColumn("ITEM_PROD_DATE"), New DataColumn("ITEM_QTY"), New DataColumn("bsr_qty"), New DataColumn("fr_mt"), New DataColumn("bsr_mt")})
        ViewState("mat1") = dt2
        Me.BINDGRID1()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox49.Text = "" Then
            TextBox49.Focus()
            Return
        ElseIf IsDate(TextBox49.Text) = False Then
            TextBox49.Focus()
            Return
        End If
        Dim dpr_date As Date
        dpr_date = CDate(TextBox49.Text)
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        Dim PO_QUARY As String = "select distinct PROD_CONTROL .ITEM_CODE,qual_group.qual_name ,F_ITEM .ITEM_NAME ,F_ITEM .ITEM_AU , " & _
            " '" & dpr_date & "'  AS P_DATE , 'PRODUCTION & DESPATCH REPORT' AS R_TYPE, " _
            & " '" & dpr_date & "'  AS P_DATE_TO , " _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY))else '0.000' end) as F_QTY, " _
 & " CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY*F_ITEM .ITEM_WEIGHT)/1000)  as F_MT," _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_B_QTY))else '0.000' end) AS B_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_B_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS B_MT ," _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_I_QTY))else '0.000' end) AS INV_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_I_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS INV_MT" _
 & " from PROD_CONTROL join F_ITEM on PROD_CONTROL .ITEM_CODE =F_ITEM .ITEM_CODE" _
 & " join qual_group ON F_ITEM .ITEM_TYPE =qual_group .qual_code" _
 & " where PROD_CONTROL .PROD_DATE = '" & dpr_date.Year & "-" & dpr_date.Month & "-" & dpr_date.Day & "'" _
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