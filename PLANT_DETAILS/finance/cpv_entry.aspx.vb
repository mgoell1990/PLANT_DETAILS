Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class cpv_entry
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
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT VOUCHER .TOKEN_NO from VOUCHER join LEDGER on VOUCHER .TOKEN_NO =LEDGER .VOUCHER_NO  where LEDGER .POST_INDICATION='CASH BANK' AND VOUCHER .CVB_DATE  IS NULL AND VOUCHER .VOUCHER_TYPE ='C.B.V'", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList38.Items.Clear()
            DropDownList38.DataSource = dt
            DropDownList38.DataValueField = "TOKEN_NO"
            DropDownList38.DataBind()
            DropDownList38.Items.Add("Select")
            DropDownList38.SelectedValue = "Select"
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

        CalendarExtender1.EndDate = DateTime.Now.Date
        TextBox92_CalendarExtender.EndDate = DateTime.Now.Date
    End Sub

    Protected Sub DropDownList38_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList38.SelectedIndexChanged
        If DropDownList38.SelectedValue = "Select" Then
            DropDownList38.Focus()
            Label535.Text = ""
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select VOUCHER .TOKEN_NO ,VOUCHER.SUPL_ID ,LEDGER.AC_NO ,ACDIC.ac_description ,(LEDGER.AMOUNT_CR +LEDGER.AMOUNT_DR) as AMOUNT_CR from VOUCHER join LEDGER on VOUCHER .TOKEN_NO =LEDGER .VOUCHER_NO JOIN ACDIC ON LEDGER.AC_NO =ACDIC .ac_code where LEDGER .POST_INDICATION='CASH BANK' AND VOUCHER .CHEQUE_NO IS NULL AND VOUCHER .VOUCHER_TYPE ='C.B.V' AND VOUCHER.TOKEN_NO=" & DropDownList38.SelectedValue, conn)
        da.Fill(dt)
        GridView9.DataSource = dt
        GridView9.DataBind()
        conn.Close()


        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "SELECT SUPL.SUPL_NAME,LEDGER.GARN_NO_MB_NO FROM VOUCHER JOIN SUPL ON VOUCHER.SUPL_ID=SUPL.SUPL_ID JOIN LEDGER ON VOUCHER.TOKEN_NO=LEDGER.VOUCHER_NO WHERE VOUCHER .TOKEN_NO ='" & DropDownList38.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            dr.Read()
            Label535.Text = dr.Item("SUPL_NAME")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        Dim I As Int16 = 0
        For I = 0 To GridView9.Rows.Count - 1
            If GridView9.Rows(I).Cells(0).Text = DropDownList38.SelectedValue Then
                GridView9.Rows(I).Cells(6).Text = TextBox92.Text
            End If
        Next
    End Sub

    Protected Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        Dim working_date As Date
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox1.Text) = False Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        End If
        Dim STR1 As String = ""
        working_date = CDate(TextBox1.Text)
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
        Dim TOTAL_AMOUNT As Decimal = 0.0
        Dim I As Integer
        For I = 0 To GridView9.Rows.Count - 1
            If GridView9.Rows(I).Cells(6).Text <> "&nbsp;" Then
                ''DATA SAVE

                'count = 0
                'conn.Open()
                'ds.Clear()
                'da = New SqlDataAdapter("SELECT DISTINCT CVB_NO FROM VOUCHER WHERE VOUCHER_TYPE ='C.B.V' AND FISCAL_YEAR=" & STR1, conn)
                'count = da.Fill(dt)
                'conn.Close()

                Dim BPV_NO As String = ""
                Dim str_temp As String = ""
                Dim count_cbv_no As Decimal
                count_cbv_no = 0
                conn.Open()
                ds.Clear()
                da = New SqlDataAdapter("SELECT CVB_NO FROM VOUCHER WHERE VOUCHER_TYPE ='C.B.V' AND CVB_NO <>'' AND FISCAL_YEAR=" & STR1, conn)
                count_cbv_no = da.Fill(dt)
                conn.Close()
                If CInt(count_cbv_no) = 0 Then
                    BPV_NO = "CPV" + STR1 + "00001"
                Else
                    str_temp = count_cbv_no
                    If str_temp.Length = 1 Then
                        str_temp = "0000" & (CInt(count_cbv_no) + 1)
                    ElseIf str_temp.Length = 2 Then
                        str_temp = "000" & (CInt(count_cbv_no) + 1)
                    ElseIf str_temp.Length = 3 Then
                        str_temp = "00" & (CInt(count_cbv_no) + 1)
                    ElseIf str_temp.Length = 4 Then
                        str_temp = "0" & (CInt(count_cbv_no) + 1)
                    Else
                        str_temp = CInt(count_cbv_no) + 1
                    End If
                    BPV_NO = "CPV" + STR1 + str_temp
                End If

                GridView9.Rows(I).Cells(5).Text = BPV_NO
                Label656.Text = BPV_NO
                ''UPDATE VOUCHER
                conn.Open()
                mycommand = New SqlCommand("update VOUCHER set BANK_NAME='CASH WITH CASHIER', CVB_NO='" & BPV_NO & "' , CVB_DATE=@EFECTIVE_DATE WHERE TOKEN_NO='" & GridView9.Rows(I).Cells(0).Text & "'", conn)
                mycommand.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(TextBox92.Text, "dd-MM-yyyy", provider))

                mycommand.ExecuteNonQuery()
                conn.Close()
                ''UPDATE LEDGER
                conn.Open()
                mycommand = New SqlCommand("update LEDGER set PAYMENT_INDICATION=@PAYMENT_INDICATION , EFECTIVE_DATE=@EFECTIVE_DATE WHERE VOUCHER_NO='" & GridView9.Rows(I).Cells(0).Text & "' AND PAYMENT_INDICATION='X'", conn)
                mycommand.Parameters.AddWithValue("@PAYMENT_INDICATION", "OK")
                mycommand.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(TextBox92.Text, "dd-MM-yyyy", provider))
                mycommand.ExecuteNonQuery()
                conn.Close()
            Else
                Label537.Text = "Please add date first"
            End If
        Next
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select DISTINCT VOUCHER .TOKEN_NO from VOUCHER join LEDGER on VOUCHER .TOKEN_NO =LEDGER .VOUCHER_NO  where LEDGER .POST_INDICATION='CASH BANK' AND VOUCHER .CVB_DATE  IS NULL AND VOUCHER .VOUCHER_TYPE ='C.B.V'", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList38.Items.Clear()
        DropDownList38.DataSource = dt
        DropDownList38.DataValueField = "TOKEN_NO"
        DropDownList38.DataBind()
        DropDownList38.Items.Add("Select")
        DropDownList38.SelectedValue = "Select"
        Dim DT7 As New DataTable
        DT7.Columns.AddRange(New DataColumn(4) {New DataColumn("AC_HEAD"), New DataColumn("AC_DESC"), New DataColumn("AMOUNT_DR"), New DataColumn("AMOUNT_CR"), New DataColumn("SUPL_ID")})
        ViewState("ext_pmt") = DT7
        Me.BINDGRID6()
    End Sub
    Protected Sub BINDGRID6()
        GridView9.DataSource = DirectCast(ViewState("CPV_ENTRY"), DataTable)
        GridView9.DataBind()
    End Sub
End Class