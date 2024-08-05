Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class rev_voucher
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
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

            Dim DT5 As New DataTable
            DT5.Columns.AddRange(New DataColumn(6) {New DataColumn("SEC_NO"), New DataColumn("SEC_DATE"), New DataColumn("SUPL_ID"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR")})
            ViewState("REVERSAL_JV") = DT5
            Me.BINDGRID5()
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT convert(varchar(30),LEDGER.VOUCHER_NO) AS VOUCHER_NO from VOUCHER join LEDGER on VOUCHER .TOKEN_NO=LEDGER.VOUCHER_NO and VOUCHER .VOUCHER_TYPE =LEDGER .REVERSAL_INDICATOR join ACDIC on LEDGER.AC_NO =ACDIC .ac_code WHERE VOUCHER .VOUCHER_TYPE ='To Be Reversed' and PAYMENT_INDICATION='' order by convert(varchar(30),LEDGER.VOUCHER_NO)", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList39.Items.Clear()
            DropDownList39.DataSource = dt
            DropDownList39.DataValueField = "VOUCHER_NO"
            DropDownList39.DataBind()
            DropDownList39.Items.Insert(0, "Select")
            DropDownList39.SelectedValue = "Select"
        End If
        CalendarExtender1.EndDate = DateTime.Now.Date
        TextBox67_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        If DropDownList39.SelectedValue = "Select" Then
            DropDownList39.Focus()
            Return
        End If
        conn.Open()
        Dim dt2 As DataTable = DirectCast(ViewState("REVERSAL_JV"), DataTable)
        da = New SqlDataAdapter("select TOKEN_NO,SEC_NO,SEC_DATE,LEDGER.SUPL_ID,LEDGER.AC_NO,ac_description,AMOUNT_DR,AMOUNT_CR,BE_NO from VOUCHER join LEDGER on VOUCHER .TOKEN_NO=LEDGER.VOUCHER_NO and VOUCHER .VOUCHER_TYPE =LEDGER .REVERSAL_INDICATOR join ACDIC on LEDGER.AC_NO =ACDIC .ac_code  where VOUCHER .TOKEN_NO  ='" & DropDownList39.SelectedValue & "'", conn)
        da.Fill(dt2)
        conn.Close()

        ViewState("REVERSAL_JV") = dt2
        BINDGRID5()

    End Sub
    Protected Sub BINDGRID5()
        GridView3.DataSource = DirectCast(ViewState("REVERSAL_JV"), DataTable)
        GridView3.DataBind()
    End Sub

    Protected Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date

                If TextBox1.Text = "" Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                ElseIf IsDate(TextBox1.Text) = False Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                ElseIf TextBox66.Text = "" Then
                    TextBox66.Focus()
                    Label479.Text = "Please Enter Sec No"
                    Return
                ElseIf TextBox67.Text = "" Then
                    TextBox67.Focus()
                    Label479.Text = "Please Enter Sec Date "
                    Return
                ElseIf GridView3.Rows.Count = 0 Then
                    Label479.Text = "Please Add data first "
                    Return
                End If

                working_date = CDate(TextBox1.Text)

                '''''''''''''''''''''''''''''''''
                ''Checking Reversal voucher entry date and Freeze date
                Dim Block_DATE As String = ""
                conn.Open()
                Dim MC_new As New SqlCommand
                MC_new.CommandText = "SELECT Block_date_finance FROM Date_Freeze"
                MC_new.Connection = conn
                dr = MC_new.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    Block_DATE = dr.Item("Block_date_finance")
                    dr.Close()
                End If
                conn.Close()

                If (CDate(TextBox1.Text) <= CDate(Block_DATE)) Then
                    Label479.Visible = True
                    Label479.Text = "Reversal voucher entry before " & Block_DATE & " has been freezed."

                Else
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
                    TextBox65.Text = STR1 & count + 1
                    ''data save voucher

                    Dim query As String = "INSERT INTO VOUCHER (TOKEN_NO,TOKEN_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,CVB_NO,CVB_DATE,CHEQUE_NO,CHEQUE_DATE)VALUES(@TOKEN_NO,@TOKEN_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@CVB_NO,@CVB_DATE,@CHEQUE_NO,@CHEQUE_DATE)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@TOKEN_NO", TextBox65.Text)
                    cmd.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@SEC_NO", TextBox66.Text)
                    cmd.Parameters.AddWithValue("@SEC_DATE", Date.ParseExact(TextBox67.Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@VOUCHER_TYPE", "REVERSAL")
                    cmd.Parameters.AddWithValue("@PAY_TYPE", "")
                    cmd.Parameters.AddWithValue("@NET_AMT", 0.0)
                    cmd.Parameters.AddWithValue("@PARTICULAR", TextBox95.Text)
                    cmd.Parameters.AddWithValue("@SUPL_ID", "")
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@CVB_NO", "")
                    cmd.Parameters.AddWithValue("@CVB_DATE", "")
                    cmd.Parameters.AddWithValue("@CHEQUE_NO", "")
                    cmd.Parameters.AddWithValue("@CHEQUE_DATE", "")
                    cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                    cmd.ExecuteReader()
                    cmd.Dispose()

                    ''SAVE LEDGER
                    Dim I As Integer = 0
                    For I = 0 To GridView3.Rows.Count - 1
                        ''UPDATE LEDGER

                        mycommand = New SqlCommand("update LEDGER set POST_INDICATION='" & TextBox65.Text & "'  WHERE VOUCHER_NO='" & GridView3.Rows(I).Cells(0).Text & "' AND AC_NO='" & GridView3.Rows(I).Cells(4).Text & "' AND Journal_ID='" & GridView3.Rows(I).Cells(1).Text & "'", conn_trans, myTrans)
                        mycommand.ExecuteNonQuery()

                        ''SAVE LEDGER

                        query = "Insert Into LEDGER(BE_NO,POST_INDICATION,VOUCHER_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,REVERSAL_INDICATOR,PAYMENT_INDICATION,Journal_ID)VALUES(@BE_NO,@POST_INDICATION,@VOUCHER_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@REVERSAL_INDICATOR,@PAYMENT_INDICATION,@Journal_ID)"
                        cmd = New SqlCommand(query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@VOUCHER_NO", TextBox65.Text)
                        cmd.Parameters.AddWithValue("@Journal_ID", TextBox66.Text)
                        cmd.Parameters.AddWithValue("@SUPL_ID", GridView3.Rows(I).Cells(3).Text)
                        cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                        cmd.Parameters.AddWithValue("@PERIOD", qtr1)
                        cmd.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                        cmd.Parameters.AddWithValue("@AC_NO", GridView3.Rows(I).Cells(4).Text)
                        cmd.Parameters.AddWithValue("@AMOUNT_CR", CDec(GridView3.Rows(I).Cells(6).Text))
                        cmd.Parameters.AddWithValue("@AMOUNT_DR", CDec(GridView3.Rows(I).Cells(7).Text))
                        cmd.Parameters.AddWithValue("@REVERSAL_INDICATOR", "REVERSAL ENTRY")
                        cmd.Parameters.AddWithValue("@POST_INDICATION", DropDownList39.SelectedValue)
                        cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
                        cmd.Parameters.AddWithValue("@BE_NO", Convert.ToString(GridView3.Rows(I).Cells(8).Text))
                        cmd.ExecuteReader()
                        cmd.Dispose()


                    Next

                    myTrans.Commit()
                    Label479.Text = "All records are written to database."

                    Dim dt2 As DataTable = DirectCast(ViewState("REVERSAL_JV"), DataTable)
                    dt2.Clear()
                    ViewState("REVERSAL_JV") = dt2
                    BINDGRID5()
                    DropDownList39.Items.Remove(DropDownList39.SelectedValue)

                End If

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label479.Text = "There was some Error, please contact EDP."
                TextBox65.Text = ""
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using



    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim DT5 As New DataTable
        DT5.Columns.AddRange(New DataColumn(6) {New DataColumn("SEC_NO"), New DataColumn("SEC_DATE"), New DataColumn("SUPL_ID"), New DataColumn("AC_NO"), New DataColumn("ac_description"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR")})
        ViewState("REVERSAL_JV") = DT5
        Me.BINDGRID5()
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select DISTINCT LEDGER.VOUCHER_NO from VOUCHER join LEDGER on VOUCHER .TOKEN_NO=LEDGER.VOUCHER_NO and VOUCHER .VOUCHER_TYPE =LEDGER .REVERSAL_INDICATOR join ACDIC on LEDGER.AC_NO =ACDIC .ac_code WHERE LEDGER.POST_INDICATION IS NULL AND VOUCHER .VOUCHER_TYPE ='To Be Reversed'", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList39.Items.Clear()
        DropDownList39.DataSource = dt
        DropDownList39.DataValueField = "VOUCHER_NO"
        DropDownList39.DataBind()
        DropDownList39.Items.Add("Select")
        DropDownList39.SelectedValue = "Select"
    End Sub
End Class