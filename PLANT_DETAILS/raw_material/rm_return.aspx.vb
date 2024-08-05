Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class rm_return
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
            DropDownList3.Attributes.Add("readonly", "readonly")
            TextBox163.Attributes.Add("readonly", "readonly")
            TextBox172.Attributes.Add("readonly", "readonly")
            TextBox173.Attributes.Add("readonly", "readonly")
            TextBox9.Attributes.Add("readonly", "readonly")
            TextBox10.Attributes.Add("readonly", "readonly")
            TextBox11.Attributes.Add("readonly", "readonly")
            TextBox12.Attributes.Add("readonly", "readonly")
            TextBox13.Attributes.Add("readonly", "readonly")
            TextBox14.Attributes.Add("readonly", "readonly")
            TextBox2.Attributes.Add("readonly", "readonly")
            TextBox168.Attributes.Add("readonly", "readonly")
            TextBox167.Attributes.Add("readonly", "readonly")
            TextBox166.Attributes.Add("readonly", "readonly")
            TextBox169.Attributes.Add("readonly", "readonly")
            TextBox170.Attributes.Add("readonly", "readonly")
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub save_ledger(issue_no As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)

        If price > 0 Then
            Dim STR1 As String = ""
            If Today.Date.Month > 3 Then
                STR1 = Today.Date.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf Today.Date.Month <= 3 Then
                STR1 = Today.Date.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If
            Dim month1 As Integer
            month1 = Today.Date.Month
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
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", Today.Date.Date)
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


    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        ''UPDATE ISSUE
        If IsNumeric(TextBox3.Text) = False Or CDec(TextBox3.Text) = 0 Then
            ISSUE_ERR_LABEL.Text = "Please Enter Numeric Value"
            ISSUE_ERR_LABEL.Visible = True
            TextBox163.Text = ""
            TextBox163.Focus()
            Return
        ElseIf CDec(TextBox163.Text) < CDec(TextBox3.Text) Then
            ISSUE_ERR_LABEL.Text = "Issue Qty is lower than Return Qty"
            ISSUE_ERR_LABEL.Visible = True
            TextBox3.Focus()
            Return
        ElseIf CDec(TextBox3.Text) > CDec(TextBox163.Text) Then
            If TextBox2.Text = "" Then
                ISSUE_ERR_LABEL.Text = "Please Enter Min Issue Qty Remarks"
                ISSUE_ERR_LABEL.Visible = True
                TextBox2.Focus()
                Return
            End If
        End If
        Dim month As Integer
        month = Today.Date.Month
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
        Dim STR1 As String = ""
        If Today.Date.Month > 3 Then
            STR1 = Today.Date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf Today.Date.Month <= 3 Then
            STR1 = Today.Date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If
        ''CALCULATION OF MATERIAL COST
        Dim TOTAL_PRICE As Decimal
        '' MAT_PRICE = 0.0
        TOTAL_PRICE = 0.0
        TOTAL_PRICE = FormatNumber(CDec(TextBox3.Text) * CDec(TextBox166.Text), 2)
        ''SEARCH MATERIAL STOCK VALUE
        Dim MAT_AVG, STOCK_QTY As New Decimal(0.0)
        conn.Open()
        Dim issue_head, con_head As String
        issue_head = ""
        con_head = ""
        Dim MC5 As New SqlCommand
        MC5.CommandText = "select AC_ISSUE,AC_CON,MAT_AVG ,MAT_STOCK from material where MAT_CODE ='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "'"
        MC5.Connection = conn
        dr = MC5.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            issue_head = dr.Item("AC_ISSUE")
            con_head = dr.Item("AC_CON")
            MAT_AVG = dr.Item("MAT_AVG")
            STOCK_QTY = dr.Item("MAT_STOCK")
            dr.Close()
            conn.Close()
        Else
            conn.Close()
        End If


        Dim NEW_AVG_PRICE As Decimal = 0
        NEW_AVG_PRICE = FormatNumber(((STOCK_QTY * MAT_AVG) + TOTAL_PRICE) / (CDec(TextBox3.Text) + STOCK_QTY), 2)




        TextBox167.Text = CDec(TextBox167.Text) + CDec(TextBox3.Text)


        ''INSERT MAT_DETAILS
        conn.Open()
        Dim Query As String = "Insert Into MAT_DETAILS(ENTRY_DATE,AVG_PRICE,ISSUE_NO,ISSUE_TYPE,LINE_NO,LINE_DATE,FISCAL_YEAR,LINE_TYPE,MAT_CODE,MAT_QTY,RQD_QTY,ISSUE_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,DEPT_CODE,PURPOSE,COST_CODE,ISSUE_BY,AUTH_BY,POST_TYPE,REMARKS,RQD_DATE,QTR,RQD_BY) VALUES (@ENTRY_DATE,@AVG_PRICE,@ISSUE_NO,@ISSUE_TYPE,@LINE_NO,@LINE_DATE,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@MAT_QTY,@RQD_QTY,@ISSUE_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@DEPT_CODE,@PURPOSE,@COST_CODE,@ISSUE_BY,@AUTH_BY,@POST_TYPE,@REMARKS,@RQD_DATE,@QTR,@RQD_BY)"
        Dim cmd As New SqlCommand(Query, conn)
        cmd.Parameters.AddWithValue("@ISSUE_NO", TextBox171.Text)
        cmd.Parameters.AddWithValue("@ISSUE_TYPE", TextBox172.Text)
        cmd.Parameters.AddWithValue("@LINE_NO", CInt(TextBox169.Text))
        cmd.Parameters.AddWithValue("@LINE_DATE", Today.Date.Date)
        cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
        cmd.Parameters.AddWithValue("@QTR", qtr)
        cmd.Parameters.AddWithValue("@LINE_TYPE", "I")
        cmd.Parameters.AddWithValue("@RQD_DATE", Today.Date.Date)
        cmd.Parameters.AddWithValue("@MAT_CODE", DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1))
        cmd.Parameters.AddWithValue("@MAT_QTY", 0)
        cmd.Parameters.AddWithValue("@RQD_QTY", 0)
        cmd.Parameters.AddWithValue("@ISSUE_QTY", CDec(TextBox3.Text) * (-1))
        cmd.Parameters.AddWithValue("@MAT_BALANCE", CDec(TextBox167.Text))
        cmd.Parameters.AddWithValue("@UNIT_PRICE", CDec(TextBox166.Text))
        cmd.Parameters.AddWithValue("@TOTAL_PRICE", TOTAL_PRICE * (-1))
        cmd.Parameters.AddWithValue("@PURPOSE", TextBox2.Text)
        cmd.Parameters.AddWithValue("@COST_CODE", TextBox173.Text.Substring(0, TextBox173.Text.IndexOf(",") - 1))
        cmd.Parameters.AddWithValue("@RQD_BY", Session("userName"))
        cmd.Parameters.AddWithValue("@AUTH_BY", Session("userName"))
        cmd.Parameters.AddWithValue("@ISSUE_BY", Session("userName"))
        cmd.Parameters.AddWithValue("@DEPT_CODE", "RM")
        cmd.Parameters.AddWithValue("@POST_TYPE", "AUTH")
        cmd.Parameters.AddWithValue("@AVG_PRICE", NEW_AVG_PRICE)
        cmd.Parameters.AddWithValue("@REMARKS", "Issue Return")
        cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
        cmd.ExecuteReader()
        cmd.Dispose()
        conn.Close()

        ''UPDATE MATERIAL STOCK

        conn.Open()
        Query = "UPDATE MATERIAL SET MAT_AVG=@MAT_AVG, MAT_STOCK=@MAT_STOCK,LAST_TRANS_DATE=@LAST_TRANS_DATE WHERE MAT_CODE ='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "'"
        cmd = New SqlCommand(Query, conn)
        cmd.Parameters.AddWithValue("@MAT_STOCK", CDec(TextBox167.Text))
        cmd.Parameters.AddWithValue("@MAT_AVG", NEW_AVG_PRICE)
        cmd.Parameters.AddWithValue("@LAST_TRANS_DATE", Date.ParseExact(Today.Date.Date, "dd-MM-yyyy", provider))
        cmd.ExecuteReader()
        cmd.Dispose()
        conn.Close()
        ISSUE_ERR_LABEL.Text = "Data Saved"
        ISSUE_ERR_LABEL.Visible = True
        ''SAVE LEDGER
        save_ledger(TextBox171.Text, issue_head, "Dr", CDec(FormatNumber(TOTAL_PRICE, 2)), "Material Issue")
        save_ledger(TextBox171.Text, con_head, "Cr", CDec(FormatNumber(TOTAL_PRICE, 2)), "Material Con")
        TextBox171.Text = ""
    End Sub
End Class