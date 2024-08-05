Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class raw_bal
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
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select * from MAT_DETAILS join MATERIAL on MAT_DETAILS .MAT_CODE =MATERIAL .MAT_CODE where MAT_DETAILS .MAT_CODE LIKE '1%'  and line_date is not null AND TOTAL_PRICE <> 0 and FISCAL_YEAR =1718 order by MAT_DETAILS .LINE_DATE,MAT_DETAILS.ISSUE_NO ", conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
        conn.Close()

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
     

        Dim I As Integer = 0
        For I = 0 To GridView1.Rows.Count - 1
            If GridView1.Rows(I).Cells(1).Text = "R" Then
                'swachh bharat cess
                Dim TOTAL_PRICE As New Decimal(0.0)
                conn.Open()
                mycommand.CommandText = "SELECT * FROM PO_RCD_MAT WHERE GARN_NO ='" & GridView1.Rows(I).Cells(0).Text & "' AND MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TOTAL_PRICE = dr.Item("PROV_VALUE") + dr.Item("TRANS_CHARGE")
                    dr.Close()
                Else
                    conn.Close()
                End If
                conn.Close()
                GridView1.Rows(I).Cells(7).Text = FormatNumber((TOTAL_PRICE / CDec(GridView1.Rows(I).Cells(5).Text)), 3)
                GridView1.Rows(I).Cells(8).Text = TOTAL_PRICE
              
                'MATERIAL AVG ,STOCK UPDATE
                Dim STOCK_QTY, AVG_PRICE As Decimal
                conn.Open()
                mycommand.CommandText = "select * from MATERIAL where MAT_CODE = '" & GridView1.Rows(I).Cells(4).Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    STOCK_QTY = dr.Item("MAT_STOCK")
                    AVG_PRICE = dr.Item("MAT_AVG")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If



                Dim NEW_AVG_PRICE, NEW_UNIT_RATE, NEW_MAT_VALUE As Decimal
                NEW_MAT_VALUE = CDec(GridView1.Rows(I).Cells(8).Text)
                NEW_UNIT_RATE = CDec(FormatNumber(NEW_MAT_VALUE / CDec(GridView1.Rows(I).Cells(5).Text), 2))
                NEW_AVG_PRICE = FormatNumber(((STOCK_QTY * AVG_PRICE) + NEW_MAT_VALUE) / (CDec(GridView1.Rows(I).Cells(5).Text) + STOCK_QTY), 2)
                conn.Open()
                Dim Query As String
                Dim CMD As New SqlCommand
                Query = "update MATERIAL set MAT_AVG=@MAT_AVG,LAST_TRANS_DATE=@LAST_TRANS_DATE,MAT_STOCK=@MAT_STOCK,MAT_LAST_RATE=@MAT_LAST_RATE,MAT_LASTPUR_DATE=@MAT_LASTPUR_DATE where MAT_CODE='" & GridView1.Rows(I).Cells(4).Text & "'"
                CMD = New SqlCommand(Query, conn)
                CMD.Parameters.AddWithValue("@MAT_AVG", NEW_AVG_PRICE)
                CMD.Parameters.AddWithValue("@MAT_STOCK", CDec(GridView1.Rows(I).Cells(5).Text) + STOCK_QTY)
                CMD.Parameters.AddWithValue("@MAT_LAST_RATE", NEW_UNIT_RATE)
                CMD.Parameters.AddWithValue("@MAT_LASTPUR_DATE", CDate(GridView1.Rows(I).Cells(3).Text))
                CMD.Parameters.AddWithValue("@LAST_TRANS_DATE", CDate(GridView1.Rows(I).Cells(3).Text))
                CMD.ExecuteReader()
                CMD.Dispose()
                conn.Close()
                'UPDATE MAT_DETAILS
                Dim working_date As Date

                working_date = CDate(GridView1.Rows(I).Cells(3).Text)
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
                Dim MAX_LINE As Integer = 0
                ''UPDATE ISSUE
                conn.Open()
                Dim MC5 As New SqlCommand
                MC5.CommandText = "select (CASE WHEN MAX(line_no) IS NULL THEN '0' ELSE MAX(line_no) END) AS line_no from MAT_DETAILS where MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "' and FISCAL_YEAR =" & CInt(STR1)
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    MAX_LINE = dr.Item("line_no")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                MAX_LINE = MAX_LINE + 1
                conn.Open()
                Query = "UPDATE MAT_DETAILS SET AVG_PRICE=@AVG_PRICE, LINE_NO=@LINE_NO, UNIT_PRICE =@UNIT_PRICE ,TOTAL_PRICE =@TOTAL_PRICE ,MAT_BALANCE =@MAT_BALANCE WHERE ISSUE_NO ='" & GridView1.Rows(I).Cells(0).Text & "' AND MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "'"
                ''Query = "UPDATE MAT_DETAILS SET AVG_PRICE=@AVG_PRICE, UNIT_PRICE =@UNIT_PRICE ,TOTAL_PRICE =@TOTAL_PRICE ,MAT_BALANCE =@MAT_BALANCE WHERE ISSUE_NO ='" & GridView1.Rows(I).Cells(0).Text & "' AND MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "'"
                CMD = New SqlCommand(Query, conn)
                CMD.Parameters.AddWithValue("@MAT_BALANCE", CDec(GridView1.Rows(I).Cells(5).Text) + STOCK_QTY)
                CMD.Parameters.AddWithValue("@TOTAL_PRICE", CDec(GridView1.Rows(I).Cells(8).Text))
                CMD.Parameters.AddWithValue("@UNIT_PRICE", NEW_UNIT_RATE)
                CMD.Parameters.AddWithValue("@LINE_NO", MAX_LINE)
                CMD.Parameters.AddWithValue("@AVG_PRICE", NEW_AVG_PRICE)
                CMD.ExecuteReader()
                CMD.Dispose()
                conn.Close()

            ElseIf GridView1.Rows(I).Cells(1).Text = "I" Then

                'ISSUE


                Dim working_date As Date

                working_date = CDate(GridView1.Rows(I).Cells(3).Text)

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


                Dim MAX_LINE As Integer = 0
                ''UPDATE ISSUE
                conn.Open()
                Dim MC5 As New SqlCommand
                MC5.CommandText = "select (CASE WHEN MAX(line_no) IS NULL THEN '0' ELSE MAX(line_no) END) AS line_no from MAT_DETAILS where MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "' and FISCAL_YEAR =" & CInt(STR1)
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    MAX_LINE = dr.Item("line_no")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                MAX_LINE = MAX_LINE + 1


                'MATERIAL AVG ,STOCK UPDATE
                Dim STOCK_QTY, AVG_PRICE As Decimal
                conn.Open()
                mycommand.CommandText = "select * from MATERIAL where MAT_CODE = '" & GridView1.Rows(I).Cells(4).Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    STOCK_QTY = dr.Item("MAT_STOCK")
                    AVG_PRICE = dr.Item("MAT_AVG")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                Dim month As Integer
                month = working_date.Date.Month
                Dim qtr As String = ""
                If month = 4 Or month = 5 Or month = 6 Then
                    qtr = "Q1"
                ElseIf month = 7 Or month = 8 Or month = 9 Then
                    qtr = "Q2"
                ElseIf month = 10 Or month = 11 Or month = 12 Then
                    qtr = "Q3"
                ElseIf month = 1 Or month = 2 Or month = 3 Then
                    qtr = "Q4"
                End If
                conn.Open()
                Dim Query As String = "UPDATE MAT_DETAILS SET AVG_PRICE=@AVG_PRICE, LINE_NO=@LINE_NO,ISSUE_QTY=@ISSUE_QTY,MAT_BALANCE=@MAT_BALANCE,UNIT_PRICE=@UNIT_PRICE,TOTAL_PRICE=@TOTAL_PRICE,ISSUE_BY=@ISSUE_BY ,QTR=@QTR , MAT_QTY=@MAT_QTY WHERE ISSUE_NO ='" & GridView1.Rows(I).Cells(0).Text & "'"
                Dim cmd As New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@LINE_NO", CInt(MAX_LINE))
                cmd.Parameters.AddWithValue("@MAT_QTY", 0)
                cmd.Parameters.AddWithValue("@ISSUE_QTY", CDec(GridView1.Rows(I).Cells(6).Text))
                cmd.Parameters.AddWithValue("@MAT_BALANCE", CDec(STOCK_QTY) - CDec(GridView1.Rows(I).Cells(6).Text))
                cmd.Parameters.AddWithValue("@UNIT_PRICE", CDec(AVG_PRICE))
                cmd.Parameters.AddWithValue("@TOTAL_PRICE", CDec(FormatNumber(CDec(AVG_PRICE) * CDec(GridView1.Rows(I).Cells(6).Text), 2)))
                cmd.Parameters.AddWithValue("@QTR", qtr)
                cmd.Parameters.AddWithValue("@ISSUE_BY", Session("userName"))
                cmd.Parameters.AddWithValue("@AVG_PRICE", CDec(AVG_PRICE))
                cmd.ExecuteReader()
                cmd.Dispose()
                conn.Close()
                ''UPDATE MATERIAL
                conn.Open()
                Query = "UPDATE MATERIAL SET MAT_STOCK=@MAT_STOCK,LAST_ISSUE_DATE=@LAST_ISSUE_DATE,LAST_TRANS_DATE=@LAST_TRANS_DATE WHERE MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "'"
                cmd = New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@MAT_STOCK", CDec(STOCK_QTY) - CDec(GridView1.Rows(I).Cells(6).Text))
                cmd.Parameters.AddWithValue("@LAST_ISSUE_DATE", Date.ParseExact(working_date.Date.Date, "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@LAST_TRANS_DATE", Date.ParseExact(working_date.Date.Date, "dd-MM-yyyy", provider))
                cmd.ExecuteReader()
                cmd.Dispose()
                conn.Close()
                ''save ledger
                conn.Open()
                Dim issue_head, con_head As String
                issue_head = ""
                con_head = ""
                mycommand.CommandText = "select AC_ISSUE,AC_CON from material where MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    issue_head = dr.Item("AC_ISSUE")
                    con_head = dr.Item("AC_CON")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                issue_ledger(CDate(GridView1.Rows(I).Cells(3).Text), GridView1.Rows(I).Cells(0).Text, issue_head, "Cr", CDec(FormatNumber(CDec(GridView1.Rows(I).Cells(6).Text) * CDec(AVG_PRICE), 2)), "Material Issue")
                issue_ledger(CDate(GridView1.Rows(I).Cells(3).Text), GridView1.Rows(I).Cells(0).Text, con_head, "Dr", CDec(FormatNumber(CDec(GridView1.Rows(I).Cells(6).Text) * CDec(AVG_PRICE), 2)), "Material Con")



            ElseIf GridView1.Rows(I).Cells(1).Text = "S" Then

                'SALE


                Dim working_date As Date

                working_date = CDate(GridView1.Rows(I).Cells(3).Text)

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


                Dim MAX_LINE As Integer = 0
                ''UPDATE ISSUE
                conn.Open()
                Dim MC5 As New SqlCommand
                MC5.CommandText = "select (CASE WHEN MAX(line_no) IS NULL THEN '0' ELSE MAX(line_no) END) AS line_no from MAT_DETAILS where MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "' and FISCAL_YEAR =" & CInt(STR1)
                MC5.Connection = conn
                dr = MC5.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    MAX_LINE = dr.Item("line_no")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If
                MAX_LINE = MAX_LINE + 1


                'MATERIAL AVG ,STOCK UPDATE
                Dim STOCK_QTY, AVG_PRICE As Decimal
                conn.Open()
                mycommand.CommandText = "select * from MATERIAL where MAT_CODE = '" & GridView1.Rows(I).Cells(4).Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    STOCK_QTY = dr.Item("MAT_STOCK")
                    AVG_PRICE = dr.Item("MAT_AVG")
                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If

                Dim month As Integer
                month = working_date.Date.Month
                Dim qtr As String = ""
                If month = 4 Or month = 5 Or month = 6 Then
                    qtr = "Q1"
                ElseIf month = 7 Or month = 8 Or month = 9 Then
                    qtr = "Q2"
                ElseIf month = 10 Or month = 11 Or month = 12 Then
                    qtr = "Q3"
                ElseIf month = 1 Or month = 2 Or month = 3 Then
                    qtr = "Q4"
                End If
                conn.Open()
                Dim Query As String = "UPDATE MAT_DETAILS SET AVG_PRICE=@AVG_PRICE, LINE_NO=@LINE_NO,ISSUE_QTY=@ISSUE_QTY,MAT_BALANCE=@MAT_BALANCE,UNIT_PRICE=@UNIT_PRICE,TOTAL_PRICE=@TOTAL_PRICE,ISSUE_BY=@ISSUE_BY ,QTR=@QTR , MAT_QTY=@MAT_QTY WHERE ISSUE_NO ='" & GridView1.Rows(I).Cells(0).Text & "'"
                Dim cmd As New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@LINE_NO", CInt(MAX_LINE))
                cmd.Parameters.AddWithValue("@MAT_QTY", 0)
                cmd.Parameters.AddWithValue("@ISSUE_QTY", CDec(GridView1.Rows(I).Cells(6).Text))
                cmd.Parameters.AddWithValue("@MAT_BALANCE", CDec(STOCK_QTY) - CDec(GridView1.Rows(I).Cells(6).Text))
                cmd.Parameters.AddWithValue("@UNIT_PRICE", CDec(AVG_PRICE))
                cmd.Parameters.AddWithValue("@TOTAL_PRICE", CDec(FormatNumber(CDec(AVG_PRICE) * CDec(GridView1.Rows(I).Cells(6).Text), 2)))
                cmd.Parameters.AddWithValue("@QTR", qtr)
                cmd.Parameters.AddWithValue("@ISSUE_BY", Session("userName"))
                cmd.Parameters.AddWithValue("@AVG_PRICE", CDec(AVG_PRICE))
                cmd.ExecuteReader()
                cmd.Dispose()
                conn.Close()
               
                ''UPDATE MATERIAL
                conn.Open()
                Query = "UPDATE MATERIAL SET MAT_STOCK=@MAT_STOCK,LAST_ISSUE_DATE=@LAST_ISSUE_DATE,LAST_TRANS_DATE=@LAST_TRANS_DATE WHERE MAT_CODE ='" & GridView1.Rows(I).Cells(4).Text & "'"
                Dim cmd1 As New SqlCommand(Query, conn)
                cmd1.Parameters.AddWithValue("@MAT_STOCK", CDec(STOCK_QTY) - CDec(GridView1.Rows(I).Cells(6).Text))
                cmd1.Parameters.AddWithValue("@LAST_ISSUE_DATE", Date.ParseExact(working_date.Date.Date, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@LAST_TRANS_DATE", Date.ParseExact(working_date.Date.Date, "dd-MM-yyyy", provider))
                cmd1.ExecuteReader()
                cmd1.Dispose()
                conn.Close()
               
            End If

        Next
    End Sub

    

    Protected Sub issue_ledger(w_date As Date, issue_no As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
        Dim working_date As Date
        
        working_date = CDate(w_date)
        If price > 0 Then
            Dim STR1 As String = ""
            If working_date.Date.Month > 3 Then
                STR1 = working_date.Date.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf working_date.Date.Month <= 3 Then
                STR1 = working_date.Date.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim month1 As Integer
            month1 = working_date.Date.Month
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
            dr_value = 0
            cr_value = 0
            If ac_term = "Dr" Then
                dr_value = price
                cr_value = 0.0
            ElseIf ac_term = "Cr" Then
                dr_value = 0.0
                cr_value = price
            End If
            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PO_NO", "")
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", issue_no)
            cmd.Parameters.AddWithValue("@SUPL_ID", "")
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date.Date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End If
    End Sub
End Class