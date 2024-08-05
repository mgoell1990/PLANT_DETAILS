Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class p_indent
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
    Protected Sub BINDGRID1()
        GridView1.DataSource = DirectCast(ViewState("mat"), DataTable)
        GridView1.DataBind()
        GridView3.DataSource = DirectCast(ViewState("mat"), DataTable)
        GridView3.DataBind()
    End Sub
    Protected Sub BINDGRID2()
      
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("userName") = "" Then
            '    Response.Redirect("~/Account/Login")
            '    Return
            'End If
            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(3) {New DataColumn("SlNo"), New DataColumn("S_CODE"), New DataColumn("S_NAME"), New DataColumn("S_ADD")})
            ViewState("supl") = dt2

            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(15) {New DataColumn("SlNo"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("MAT_QTY"), New DataColumn("CON1"), New DataColumn("CON2"), New DataColumn("CON3"), New DataColumn("OB"), New DataColumn("MAT_STOCK"), New DataColumn("LP"), New DataColumn("LPO"), New DataColumn("LD"), New DataColumn("UNIT_PRICE"), New DataColumn("TOTAL_PRICE"), New DataColumn("D_DATE")})
            ViewState("mat") = dt1
            Me.BINDGRID1()
            Me.BINDGRID2()

         
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox3.Text = "" Then
            TextBox3.Focus()
            Return
        ElseIf TextBox4.Text = "" Then
            TextBox4.Focus()
            Return
        ElseIf TextBox5.Text = "" Then
            TextBox5.Focus()
            Return
        ElseIf IsNumeric(TextBox5.Text) = False Then
            TextBox5.Focus()
            TextBox5.Text = ""

            Return
        ElseIf TextBox6.Text = "" Then
            TextBox6.Focus()
            Return
        ElseIf IsNumeric(TextBox6.Text) = False Then
            TextBox6.Text = ""
            TextBox6.Focus()
            Return
        ElseIf TextBox7.Text = "" Then
            TextBox7.Focus()
            Return
        ElseIf IsDate(TextBox7.Text) = False Then
            TextBox7.Text = ""
            TextBox7.Focus()
            Return
        ElseIf TextBox3.Text.IndexOf(",") <> 10 Then
            TextBox3.Text = ""
            TextBox3.Focus()
            Return
        End If
       
        'validation
        count = 0
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select MAT_AU from MATERIAL where MAT_CODE = '" & TextBox3.Text.Substring(0, (TextBox3.Text.IndexOf(",") - 1)) & "'", conn)
        count = da.Fill(dt)
        conn.Close()
        If count = 0 Then
            TextBox3.Text = ""
            TextBox3.Focus()
            Return
        End If
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
        GridView1.Columns(5).HeaderText = "LC. " & CInt(STR1) - 303
        GridView1.Columns(6).HeaderText = "LC. " & CInt(STR1) - 202
        GridView1.Columns(7).HeaderText = "LC. " & CInt(STR1) - 101
        'search material details
        Dim mat_code, mat_name, au As String
        mat_code = ""
        mat_name = ""
        au = ""
        mat_code = TextBox3.Text.Substring(0, TextBox3.Text.IndexOf(",") - 1)
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select MAT_AU,MAT_NAME from MATERIAL where MAT_CODE = '" & mat_code & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            au = dr.Item("MAT_AU")
            mat_name = dr.Item("MAT_NAME")
            dr.Close()
        End If
        conn.Close()
        Dim con1, con2, con3, OB, STOCK, L_PRICE As Decimal
        Dim LP, LD As String
        LP = "NA"
        LD = "NA"
        conn.Open()
        mc1.CommandText = "select " & _
        "(select (case when SUM(issue_qty) IS null then '0.00' else SUM(issue_qty) end) from MAT_DETAILS WHERE MAT_CODE ='" & mat_code & "' AND FISCAL_YEAR ='" & CInt(STR1) - 303 & "') as 'con1', " & _
        "(select (case when SUM(issue_qty) IS null then '0.00' else SUM(issue_qty) end) from MAT_DETAILS WHERE MAT_CODE ='" & mat_code & "' AND FISCAL_YEAR ='" & CInt(STR1) - 202 & "') as 'con2', " & _
        "(select (case when SUM(issue_qty) IS null then '0.00' else SUM(issue_qty) end) from MAT_DETAILS WHERE MAT_CODE ='" & mat_code & "' AND FISCAL_YEAR ='" & CInt(STR1) - 101 & "') as 'con3' , " & _
         "(select (case when SUM(MAT_QTY) IS null then '0.00' else SUM(MAT_QTY) end) from PO_ORD_MAT WHERE MAT_CODE ='" & mat_code & "' AND MAT_STATUS ='PENDING') AS OB , " & _
        "(select (case when max(MAT_STOCK) IS null then '0.00' else max(MAT_STOCK) end) from MATERIAL WHERE MAT_CODE ='" & mat_code & "') AS STOCK , " & _
        "(SELECT (case when max(UNIT_PRICE)  is null then '0.00' else max(UNIT_PRICE)  end)  FROM MAT_DETAILS WHERE ISSUE_TYPE ='PURCHASE' AND MAT_CODE ='" & mat_code & "' AND LINE_NO =(SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & mat_code & "' AND ISSUE_TYPE ='PURCHASE')) AS L_PRICE , " & _
        "(SELECT (case when max(ORDER_DETAILS .SO_ACTUAL)  IS null then 'N/A' else max(ORDER_DETAILS .SO_ACTUAL)  end) FROM MAT_DETAILS join ORDER_DETAILS on MAT_DETAILS .COST_CODE =ORDER_DETAILS .SO_NO  WHERE MAT_DETAILS.ISSUE_TYPE ='PURCHASE' AND MAT_DETAILS.MAT_CODE ='" & mat_code & "' AND MAT_DETAILS.LINE_NO =(SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & mat_code & "' AND ISSUE_TYPE ='PURCHASE')) AS LP , " & _
        "(SELECT (case when max(ORDER_DETAILS .SO_ACTUAL_DATE)  IS null then '' else max(ORDER_DETAILS .SO_ACTUAL_DATE)  end) AS LD   FROM MAT_DETAILS join ORDER_DETAILS on MAT_DETAILS .COST_CODE =ORDER_DETAILS .SO_NO  WHERE MAT_DETAILS.ISSUE_TYPE ='PURCHASE' AND MAT_DETAILS.MAT_CODE ='" & mat_code & "' AND MAT_DETAILS.LINE_NO =(SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE ='" & mat_code & "' AND ISSUE_TYPE ='PURCHASE')) AS LD  "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            con1 = dr.Item("con1")
            con2 = dr.Item("con2")
            con3 = dr.Item("con3")
            OB = dr.Item("OB")
            STOCK = dr.Item("STOCK")
            L_PRICE = dr.Item("L_PRICE")
            LP = dr.Item("LP")
            LD = dr.Item("LD")
            dr.Close()
        End If
        conn.Close()
        If LD = "01-01-1900" Then
            LD = "N/A"
        End If
        count = (GridView1.Rows.Count + 1)
        Dim dt_table As DataTable = DirectCast(ViewState("mat"), DataTable)
        dt_table.Rows.Add(count, mat_code, mat_name, au, CDec(TextBox5.Text), con1, con2, con3, OB, STOCK, L_PRICE, LP, LD, CDec(TextBox6.Text), CDec(TextBox5.Text) * CDec(TextBox6.Text), TextBox7.Text)
        ViewState("mat") = dt_table
        Me.BINDGRID1()












    End Sub
End Class