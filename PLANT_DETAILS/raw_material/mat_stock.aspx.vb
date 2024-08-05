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
Public Class mat_stock
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

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Return
        ElseIf TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox1.Text) = False Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Return
        End If
        Dim mat_code As String = ""
        If DropDownList1.SelectedValue = "Raw Material" Then
            mat_code = "100"
        ElseIf DropDownList1.SelectedValue = "Store Material" Then
            mat_code = "0"
        End If


        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select * from MATERIAL where MAT_CODE like '" & mat_code & "%' ORDER BY MAT_CODE", conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
        conn.Close()
        Dim FROM_DATE, TO_DATE As Date
        FROM_DATE = CDate(TextBox1.Text)
        TO_DATE = CDate(TextBox2.Text)

        Dim i As Integer = 0
        For i = 0 To GridView1.Rows.Count - 1
            'search open qty and total price
            Dim OPEN_STOCK_QTY, OPEN_UNIT_PRICE, RCD_QTY, RCD_VALUE, ISSUE_QTY, ISSUE_VALUE, CLOSING_QTY, CLOSING_VALUE As Decimal
            conn.Open()
            mycommand.CommandText = "SELECT " & _
                "(SELECT (CASE WHEN SUM(MAT_QTY) IS NULL THEN 0.00 ELSE SUM(MAT_QTY) END) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "'  and LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "')-(SELECT (CASE WHEN SUM(ISSUE_QTY) IS NULL THEN 0.00 ELSE SUM(ISSUE_QTY) END) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "'  and LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "') + (SELECT OPEN_STOCK FROM MATERIAL WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "') AS OPEN_STOCK, " & _
                "(SELECT CAST((SELECT (CASE WHEN (SELECT  SUM(MAT_BALANCE* AVG_PRICE)  FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE <'" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "'  and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' )) IS NOT NULL THEN (SELECT  SUM(MAT_BALANCE* AVG_PRICE)  FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "'  and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE <'" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' )) WHEN (SELECT SUM(MAT_BALANCE* AVG_PRICE) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' )) > 0 THEN  (SELECT SUM(MAT_BALANCE* AVG_PRICE)  FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' )) WHEN (SELECT OPEN_STOCK * OPEN_AVG_PRICE  FROM MATERIAL WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "') >0 THEN  (SELECT  OPEN_STOCK *OPEN_AVG_PRICE FROM MATERIAL WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "') ELSE '0.000' END ) FROM MATERIAL WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "')  AS DECIMAL(16,3))) AS OPEN_VALUE, " & _
                "(select (CASE WHEN SUM( MAT_QTY)  IS NULL THEN 0.00  ELSE SUM(MAT_QTY)  END) from MAT_DETAILS where MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' and LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "') AS RCD_QTY, " & _
                "(select (CASE WHEN SUM( TOTAL_PRICE )  IS NULL THEN 0.00  ELSE SUM(TOTAL_PRICE )  END) from MAT_DETAILS where LINE_TYPE ='R' AND MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' and LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "') AS RCD_VALUE, " & _
                "(select (CASE WHEN SUM( ISSUE_QTY )  IS NULL THEN 0.00  ELSE SUM(ISSUE_QTY )  END) from MAT_DETAILS where MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' and LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "') AS ISSUE_QTY, " & _
                "(select (CASE WHEN SUM( TOTAL_PRICE )  IS NULL THEN 0.00  ELSE SUM(TOTAL_PRICE )  END) from MAT_DETAILS where (LINE_TYPE ='I' OR LINE_TYPE ='S') AND MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' and LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "') AS ISSUE_VALUE, " & _
                "(SELECT (CASE WHEN SUM(MAT_QTY) IS NULL THEN 0.00 ELSE SUM(MAT_QTY) END) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "'  and LINE_DATE <= '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "') -(SELECT (CASE WHEN SUM(ISSUE_QTY) IS NULL THEN 0.00 ELSE SUM(ISSUE_QTY ) END) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "'  and LINE_DATE <= '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "') + (SELECT OPEN_STOCK FROM MATERIAL WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "') AS CLOSING_STOCK, " & _
                "(SELECT CAST((SELECT (CASE WHEN (SELECT  SUM(MAT_BALANCE* AVG_PRICE)  FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "' and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "' )) >=0" & _
                "THEN (SELECT  SUM(MAT_BALANCE* AVG_PRICE)  FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "' and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE BETWEEN '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND '" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "' ))" & _
                "WHEN (SELECT SUM(MAT_BALANCE* AVG_PRICE) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "')) >= 0" & _
                "THEN  (SELECT SUM(MAT_BALANCE* AVG_PRICE)  FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' and LINE_NO = (SELECT MAX(LINE_NO ) FROM MAT_DETAILS WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "' AND LINE_DATE < '" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' ))" & _
                "ELSE (SELECT OPEN_STOCK * OPEN_AVG_PRICE  FROM MATERIAL WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "')" & _
                " END ) FROM MATERIAL WHERE MAT_CODE ='" & GridView1.Rows(i).Cells(0).Text & "')  AS DECIMAL(16,3))) AS CLOSING_VALUE"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                OPEN_STOCK_QTY = dr.Item("OPEN_STOCK")
                OPEN_UNIT_PRICE = dr.Item("OPEN_VALUE")

                RCD_QTY = dr.Item("RCD_QTY")
                RCD_VALUE = dr.Item("RCD_VALUE")

                ISSUE_QTY = dr.Item("ISSUE_QTY")
                ISSUE_VALUE = dr.Item("ISSUE_VALUE")

                CLOSING_QTY = dr.Item("CLOSING_STOCK")
                CLOSING_VALUE = dr.Item("CLOSING_VALUE")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            GridView1.Rows(i).Cells(2).Text = OPEN_STOCK_QTY
            GridView1.Rows(i).Cells(3).Text = OPEN_UNIT_PRICE
            GridView1.Rows(i).Cells(4).Text = RCD_QTY
            GridView1.Rows(i).Cells(5).Text = RCD_VALUE

            GridView1.Rows(i).Cells(6).Text = ISSUE_QTY
            GridView1.Rows(i).Cells(7).Text = ISSUE_VALUE
            GridView1.Rows(i).Cells(8).Text = CLOSING_QTY
            GridView1.Rows(i).Cells(9).Text = CLOSING_VALUE

            RCD_QTY = 0
            RCD_VALUE = 0
            OPEN_STOCK_QTY = 0
            OPEN_UNIT_PRICE = 0
            ISSUE_QTY = 0
            ISSUE_VALUE = 0
            CLOSING_QTY = 0
            CLOSING_VALUE = 0
        Next
    End Sub
End Class