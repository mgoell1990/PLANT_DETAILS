Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.Drawing

Public Class jur_voucher
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim count As Integer
    Dim dr1 As SqlDataReader
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

            Dim DT4 As New DataTable
            DT4.Columns.AddRange(New DataColumn(6) {New DataColumn("AC HEAD"), New DataColumn("A/C DESCRIPTION"), New DataColumn("SUPL CODE"), New DataColumn("DEBIT AMOUNT"), New DataColumn("CREDIT AMOUNT"), New DataColumn("BE/BL NO"), New DataColumn("INVOICE NO")})
            ViewState("EXTERNAL_JV") = DT4
            Me.BINDGRID4()

            Panel1.Visible = False
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

        CalendarExtender1.EndDate = DateTime.Now.Date
        TextBox63_CalendarExtender.EndDate = DateTime.Now.Date
        TextBox182_CalendarExtender.EndDate = DateTime.Now.Date
    End Sub
    Protected Sub BINDGRID4()
        GridView2.DataSource = DirectCast(ViewState("EXTERNAL_JV"), DataTable)
        GridView2.DataBind()
    End Sub
    Protected Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click

        If DropDownList10.SelectedValue = "HO DA" Then
            If DropDownList29.Text = "" Then
                Label468.Text = "Please Select BE No."
                Return
            Else
                Label468.Text = ""
            End If
        Else
            Label468.Text = ""
        End If


        If TextBox62.Text = "" Then
            TextBox62.Focus()
            Label468.Text = "Please Enter Section No"
            Return
        ElseIf TextBox63.Text = "" Or IsDate(TextBox63.Text) = False Then
            TextBox63.Focus()
            Label468.Text = "Please Select Date"
            Return
        ElseIf DropDownList28.SelectedValue = "Select" Then
            DropDownList28.Focus()
            Label468.Text = "Please Select Type Of JV"
            Return
        ElseIf DropDownList25.Text = "" Then
            DropDownList25.Focus()
            Label468.Text = "Please Enter Supplier"
            Return
        ElseIf DropDownList26.Text = "" Then
            DropDownList26.Focus()
            Label468.Text = "Please Enter A/C Head"
            Return
        ElseIf TextBox64.Text = "" Or IsNumeric(TextBox64.Text) = False Then
            TextBox64.Focus()
            Label468.Text = "Please Enter Amount Or Numeric Value"
            Return
        ElseIf catgory_DropDownList0.SelectedValue = "Select" Then
            catgory_DropDownList0.Focus()
            Label468.Text = "Please Select Amt Type"
            Return
        ElseIf (TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox2.Text = "") Then
            Label468.Text = "GST percentage cannot be left blank"
            Label468.ForeColor = Color.Red
            Return
        ElseIf TextBox177.Text = "" Then
            TextBox177.Focus()
            Label468.Text = "Please Enter Invoice No"
            Return
        End If


        Dim amount_dr As Decimal = 0.0
        Dim amount_cr As Decimal = 0.0
        If catgory_DropDownList0.SelectedValue = "Dr" Then
            amount_dr = CDec(TextBox64.Text)
            amount_cr = 0.0
        ElseIf catgory_DropDownList0.SelectedValue = "Cr" Then
            amount_cr = CDec(TextBox64.Text)
            amount_dr = 0.0
        End If
        Dim supl_name As String = ""
        If DropDownList25.Text = "N/A" Then
            supl_name = DropDownList25.Text
        Else
            supl_name = DropDownList25.Text.Substring(0, (DropDownList25.Text.IndexOf(",") - 1))
        End If
        Dim dt1 As DataTable = DirectCast(ViewState("EXTERNAL_JV"), DataTable)

        Dim m As Match = Regex.Match(DropDownList29.Text, ",")
        If (m.Success) Then
            dt1.Rows.Add(DropDownList26.Text.Substring(0, (DropDownList26.Text.IndexOf(",") - 1)), DropDownList26.Text.Substring((DropDownList26.Text.IndexOf(",") + 1)), supl_name, amount_dr, amount_cr, DropDownList29.Text.Substring(0, (DropDownList29.Text.IndexOf(",") - 1)), TextBox177.Text)
        Else
            dt1.Rows.Add(DropDownList26.Text.Substring(0, (DropDownList26.Text.IndexOf(",") - 1)), DropDownList26.Text.Substring((DropDownList26.Text.IndexOf(",") + 1)), supl_name, amount_dr, amount_cr, DropDownList29.Text, TextBox177.Text)
        End If

        ViewState("EXTERNAL_JV") = dt1
        BINDGRID4()
        Label468.Text = ""

        'Dim gstHead As New String("")
        'gstHead = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim

        'If ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And (txtTaxableAmount.Text = "0" Or txtTaxableAmount.Text = "")) Then
        '    txtTaxableAmount.Focus()
        '    Label468.Text = "Please enter taxable amount in case of GST"
        '    Return
        'Else

        '    If ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And (TextBox5.Text = "" Or TextBox6.Text = "" Or TextBox2.Text = "")) Then
        '        Label468.Text = "Please enter valid GST percentage"
        '        Return
        '    ElseIf ((gstHead = "64811" Or gstHead = "64812" Or gstHead = "64813" Or gstHead = "64821" Or gstHead = "64822" Or gstHead = "64823") And ((CDec(TextBox5.Text) + CDec(TextBox6.Text) + CDec(TextBox2.Text)) = 0)) Then
        '        Label468.Text = "Please enter valid GST percentage"
        '        Return
        '    ElseIf ((gstHead = "64811" Or gstHead = "64821") And (CDec(TextBox2.Text) = 0)) Then
        '        Label468.Text = "Please enter valid IGST percentage only"
        '        Return
        '    ElseIf ((gstHead = "64812" Or gstHead = "64822") And (CDec(TextBox5.Text) = 0)) Then
        '        Label468.Text = "Please enter valid CGST percentage"
        '        Return
        '    ElseIf ((gstHead = "64813" Or gstHead = "64823") And (CDec(TextBox6.Text) = 0)) Then
        '        Label468.Text = "Please enter valid SGST percentage"
        '        Return
        '    Else
        '        ''add data to grid view
        '        Dim amount_dr As Decimal = 0.0
        '        Dim amount_cr As Decimal = 0.0
        '        If catgory_DropDownList0.SelectedValue = "Dr" Then
        '            amount_dr = CDec(TextBox64.Text)
        '            amount_cr = 0.0
        '        ElseIf catgory_DropDownList0.SelectedValue = "Cr" Then
        '            amount_cr = CDec(TextBox64.Text)
        '            amount_dr = 0.0
        '        End If
        '        Dim supl_name As String = ""
        '        If DropDownList25.Text = "N/A" Then
        '            supl_name = DropDownList25.Text
        '        Else
        '            supl_name = DropDownList25.Text.Substring(0, (DropDownList25.Text.IndexOf(",") - 1))
        '        End If
        '        Dim dt1 As DataTable = DirectCast(ViewState("EXTERNAL_JV"), DataTable)

        '        Dim m As Match = Regex.Match(DropDownList29.Text, ",")
        '        If (m.Success) Then
        '            dt1.Rows.Add(DropDownList26.Text.Substring(0, (DropDownList26.Text.IndexOf(",") - 1)), DropDownList26.Text.Substring((DropDownList26.Text.IndexOf(",") + 1)), supl_name, amount_dr, amount_cr, DropDownList29.Text.Substring(0, (DropDownList29.Text.IndexOf(",") - 1)))
        '        Else
        '            dt1.Rows.Add(DropDownList26.Text.Substring(0, (DropDownList26.Text.IndexOf(",") - 1)), DropDownList26.Text.Substring((DropDownList26.Text.IndexOf(",") + 1)), supl_name, amount_dr, amount_cr, DropDownList29.Text)
        '        End If

        '        ViewState("EXTERNAL_JV") = dt1
        '        BINDGRID4()
        '        Label468.Text = ""
        '    End If
        'End If


    End Sub

    Protected Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date

                If DropDownList10.SelectedValue = "Select" Then
                    DropDownList10.Focus()
                    Label468.Text = "Please Select Voucher Type first."
                    Return
                ElseIf DropDownList10.SelectedValue = "HO DA" Then
                    If DropDownList29.Text = "" Then
                        Label468.Text = "Please enter BE No."
                        Return
                    Else
                        Label468.Text = ""
                    End If
                Else
                    Label468.Text = ""
                End If

                If TextBox1.Text = "" Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                ElseIf IsDate(TextBox1.Text) = False Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                ElseIf TextBox62.Text = "" Then
                    TextBox62.Focus()
                    Label468.Text = "Please Enter Section No"
                    Return
                ElseIf TextBox63.Text = "" Or IsDate(TextBox63.Text) = False Then
                    TextBox63.Focus()
                    Label468.Text = "Please Select Date"
                    Return
                ElseIf DropDownList28.SelectedValue = "Select" Then
                    DropDownList28.Focus()
                    Label468.Text = "Please Select Type Of JV"
                    Return
                ElseIf GridView2.Rows.Count = 0 Then
                    Label468.Text = "Please Add Data First"
                    Return
                End If

                working_date = CDate(TextBox1.Text)

                '''''''''''''''''''''''''''''''''
                ''Checking Journal voucher entry date and Freeze date
                Dim Block_DATE As String = ""
                conn.Open()
                Dim MC_new As New SqlCommand
                MC_new.CommandText = "SELECT Block_date_finance_JE FROM Date_Freeze"
                MC_new.Connection = conn
                dr1 = MC_new.ExecuteReader
                If dr1.HasRows Then
                    dr1.Read()
                    Block_DATE = dr1.Item("Block_date_finance_JE")
                    dr1.Close()
                End If
                conn.Close()

                If (CDate(TextBox1.Text) <= CDate(Block_DATE)) Then
                    Label468.Visible = True
                    Label468.Text = "Journal voucher entry before " & Block_DATE & " has been freezed."

                Else
                    Dim cr, dr As Decimal
                    cr = 0
                    dr = 0
                    Dim i As Integer
                    For i = 0 To GridView2.Rows.Count - 1
                        cr = GridView2.Rows(i).Cells(4).Text + cr
                        dr = GridView2.Rows(i).Cells(3).Text + dr
                    Next
                    If dr <> cr Then
                        Label468.Text = "Debit amount is Not matching the Credit amount"
                        Return
                    End If
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
                    ''token no generate
                    count = 0
                    conn.Open()
                    ds.Clear()
                    da = New SqlDataAdapter("select DISTINCT TOKEN_NO FROM VOUCHER WHERE FISCAL_YEAR=" & STR1, conn)
                    count = da.Fill(dt)
                    conn.Close()
                    TextBox61.Text = STR1 & count + 1
                    ''save Journal voucher

                    Dim query As String = "INSERT INTO VOUCHER (INV_NO,JE_DATE,JE_NO,TOKEN_NO,TOKEN_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,CVB_NO,CVB_DATE,CHEQUE_NO,CHEQUE_DATE)VALUES(@INV_NO,@JE_DATE,@JE_NO,@TOKEN_NO,@TOKEN_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@CVB_NO,@CVB_DATE,@CHEQUE_NO,@CHEQUE_DATE)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@TOKEN_NO", TextBox61.Text)
                    cmd.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@SEC_NO", TextBox62.Text)
                    cmd.Parameters.AddWithValue("@SEC_DATE", Date.ParseExact(TextBox63.Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@JE_NO", TextBox181.Text)
                    cmd.Parameters.AddWithValue("@JE_DATE", Date.ParseExact(TextBox182.Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@VOUCHER_TYPE", DropDownList28.SelectedValue)
                    cmd.Parameters.AddWithValue("@PAY_TYPE", "")
                    cmd.Parameters.AddWithValue("@NET_AMT", 0.0)
                    cmd.Parameters.AddWithValue("@PARTICULAR", TextBox94.Text)
                    cmd.Parameters.AddWithValue("@SUPL_ID", "")
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@CVB_NO", "")
                    cmd.Parameters.AddWithValue("@CVB_DATE", "")
                    cmd.Parameters.AddWithValue("@CHEQUE_NO", "")
                    cmd.Parameters.AddWithValue("@CHEQUE_DATE", "")
                    cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                    cmd.Parameters.AddWithValue("@INV_NO", TextBox177.Text)
                    cmd.ExecuteReader()
                    cmd.Dispose()


                    For i = 0 To GridView2.Rows.Count - 1
                        ''SAVE LEDGER PARTY CREDIT

                        query = "Insert Into LEDGER(AGING_FLAG,AGING_FLAG_NEW,INVOICE_NO,POST_INDICATION,BE_NO,VOUCHER_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,REVERSAL_INDICATOR,PAYMENT_INDICATION,Journal_ID)VALUES(@AGING_FLAG,@AGING_FLAG_NEW,@INVOICE_NO,@POST_INDICATION,@BE_NO,@VOUCHER_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@REVERSAL_INDICATOR,@PAYMENT_INDICATION,@Journal_ID)"
                        cmd = New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox61.Text)
                        cmd.Parameters.AddWithValue("@Journal_ID", TextBox62.Text)
                        cmd.Parameters.AddWithValue("@SUPL_ID", GridView2.Rows(i).Cells(2).Text)
                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd.Parameters.AddWithValue("@PERIOD", qtr1)
                        cmd.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                        cmd.Parameters.AddWithValue("@AC_NO", GridView2.Rows(i).Cells(0).Text)
                        cmd.Parameters.AddWithValue("@AMOUNT_DR", CDec(GridView2.Rows(i).Cells(3).Text))
                        cmd.Parameters.AddWithValue("@AMOUNT_CR", CDec(GridView2.Rows(i).Cells(4).Text))
                        cmd.Parameters.AddWithValue("@REVERSAL_INDICATOR", DropDownList28.SelectedValue)
                        cmd.Parameters.AddWithValue("@POST_INDICATION", "")
                        cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")

                        If DropDownList10.SelectedValue = "HO DA" Then
                            cmd.Parameters.AddWithValue("@BE_NO", GridView2.Rows(i).Cells(5).Text)
                        Else
                            cmd.Parameters.AddWithValue("@BE_NO", "")
                        End If
                        cmd.Parameters.AddWithValue("@INVOICE_NO", GridView2.Rows(i).Cells(6).Text)
                        cmd.Parameters.AddWithValue("@AGING_FLAG", GridView2.Rows(i).Cells(6).Text)
                        cmd.Parameters.AddWithValue("@AGING_FLAG_NEW", GridView2.Rows(i).Cells(6).Text)
                        cmd.ExecuteReader()
                        cmd.Dispose()

                    Next

                    myTrans.Commit()
                    Label468.Text = "All records are written to database."

                End If


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                TextBox61.Text = ""
                Label468.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()


                Dim DT4 As New DataTable
                DT4.Columns.AddRange(New DataColumn(6) {New DataColumn("AC HEAD"), New DataColumn("A/C DESCRIPTION"), New DataColumn("SUPL CODE"), New DataColumn("DEBIT AMOUNT"), New DataColumn("CREDIT AMOUNT"), New DataColumn("BE/BL NO"), New DataColumn("INVOICE NO")})
                ViewState("EXTERNAL_JV") = DT4
                Me.BINDGRID4()

            End Try

        End Using



    End Sub

    Protected Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim DT4 As New DataTable
        DT4.Columns.AddRange(New DataColumn(6) {New DataColumn("AC HEAD"), New DataColumn("A/C DESCRIPTION"), New DataColumn("SUPL CODE"), New DataColumn("DEBIT AMOUNT"), New DataColumn("CREDIT AMOUNT"), New DataColumn("BE/BL NO"), New DataColumn("INVOICE NO")})
        ViewState("EXTERNAL_JV") = DT4
        Me.BINDGRID4()
        TextBox62.Text = ""
        TextBox63.Text = ""
        TextBox181.Text = ""
        TextBox182.Text = ""
        DropDownList28.SelectedValue = "Select"
        DropDownList25.Text = ""
        TextBox94.Text = ""
        DropDownList26.Text = ""
        TextBox64.Text = ""
        catgory_DropDownList0.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged
        If DropDownList10.SelectedValue = "HO DA" Then
            Panel1.Visible = True

        ElseIf DropDownList10.SelectedValue = "Other" Then
            Panel1.Visible = False
        End If
    End Sub
End Class