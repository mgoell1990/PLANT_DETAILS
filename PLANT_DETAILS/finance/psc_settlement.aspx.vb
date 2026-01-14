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

Public Class psc_settlement
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
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
            If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

                Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

            Else

                'retrieve account codes
                conn.Open()
                Dim ds5 As New DataSet
                da = New SqlDataAdapter("select distinct (ac_code + ' , ' + ac_description ) as ac_code from ACDIC WHERE ac_code='61603' OR ac_code='61899' ORDER BY AC_CODE", conn)
                da.Fill(ds5, "ACDIC")
                conn.Close()
                DropDownList1.DataSource = ds5.Tables("ACDIC")
                DropDownList1.DataValueField = "AC_CODE"
                DropDownList1.DataBind()
                ds5.Tables.Clear()
                'DropDownList1.Items.Insert(0, "All")
                ds5.Clear()

                'retrieve party codes
                conn.Open()
                'Dim ds5 As New DataSet
                'da = New SqlDataAdapter("select distinct (supl_id + ' , ' + supl_name ) as supl_details from SUPL ORDER BY supl_details", conn)
                da = New SqlDataAdapter("select distinct (supl_id + ' , ' + supl_name ) as supl_details from SUPL UNION select distinct (d_code + ' , ' + d_name ) as supl_details from dater ORDER BY supl_details", conn)
                da.Fill(ds5, "ACDIC")
                conn.Close()
                DropDownList3.DataSource = ds5.Tables("ACDIC")
                DropDownList3.DataValueField = "supl_details"
                DropDownList3.DataBind()
                DropDownList3.Items.Insert(0, "All")
                ds5.Tables.Clear()
            End If
        End If



    End Sub

    Protected Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Label33.Text = "Please select effective date."
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Label33.Text = "Invalid date format"
            Return
        ElseIf TextBox10.Text = "" Then
            TextBox10.Focus()
            Label33.Text = "Please select TO Date."
            Return
        ElseIf IsDate(TextBox10.Text) = False Then
            TextBox10.Text = ""
            TextBox10.Focus()
            Label33.Text = "Invalid date format"
            Return
        ElseIf TextBox11.Text = "" Then
            TextBox11.Focus()
            Label33.Text = "Please select FROM Date."
            Return
        ElseIf IsDate(TextBox11.Text) = False Then
            TextBox11.Text = ""
            TextBox11.Focus()
            Label33.Text = "Invalid date format"
            Return
        Else
            Label33.Text = ""
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

        conn.Open()
        dt.Clear()

        Dim total_row As Integer

        'If (DropDownList1.Text = "All" And DropDownList3.Text = "All") Then
        '    da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy', 'en-us') As Entry_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' or PAYMENT_INDICATION <>'settled') ORDER BY LEDGER.AC_NO ,LEDGER.ENTRY_DATE ", conn)

        'ElseIf (DropDownList1.Text <> "All" And DropDownList3.Text <> "All") Then
        '    da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy', 'en-us') As Entry_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' or PAYMENT_INDICATION <>'settled') ORDER BY LEDGER.SUPL_ID,LEDGER.ENTRY_DATE", conn)

        'ElseIf (DropDownList1.Text <> "All" And DropDownList3.Text = "All") Then
        '    da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy', 'en-us') As Entry_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' or PAYMENT_INDICATION <>'settled') ORDER BY LEDGER.SUPL_ID,LEDGER.ENTRY_DATE", conn)

        'ElseIf (DropDownList1.Text = "All" And DropDownList3.Text <> "All") Then
        '    da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, FORMAT(LEDGER.EFECTIVE_DATE, 'dd-MM-yyyy', 'en-us') As Entry_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' or PAYMENT_INDICATION <>'settled') ORDER BY LEDGER.AC_NO,LEDGER.SUPL_ID,LEDGER.ENTRY_DATE", conn)
        'End If

        If (DropDownList1.Text = "All" And DropDownList3.Text = "All") Then

            If (DropDownList2.Text = "Raw Material") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'RGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Store") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'SGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Contracts") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'MB%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            End If

        ElseIf (DropDownList1.Text <> "All" And DropDownList3.Text <> "All") Then

            If (DropDownList2.Text = "Raw Material") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'RGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Store") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'SGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Contracts") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'MB%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            End If

        ElseIf (DropDownList1.Text <> "All" And DropDownList3.Text = "All") Then

            If (DropDownList2.Text = "Raw Material") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'RGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Store") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'SGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Contracts") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'MB%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Transport") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE AC_NO ='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and (GARN_NO_MB_NO LIKE 'RCRR%' OR GARN_NO_MB_NO LIKE 'SCRR%' OR GARN_NO_MB_NO LIKE 'OS%' OR GARN_NO_MB_NO LIKE 'DC%') and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            End If

        ElseIf (DropDownList1.Text = "All" And DropDownList3.Text <> "All") Then

            If (DropDownList2.Text = "Raw Material") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'RGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Store") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'SGARN%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            ElseIf (DropDownList2.Text = "Contracts") Then
                da = New SqlDataAdapter("SELECT LEDGER.AC_NO, ACDIC .ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code WHERE LEDGER.SUPL_ID='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "' AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (PAYMENT_INDICATION <>'X' and PAYMENT_INDICATION <>'S') and GARN_NO_MB_NO LIKE 'MB%' and POST_INDICATION NOT LIKE '%PSC_Adj%' ORDER BY LEDGER.Efective_Date, LEDGER.SUPL_ID", conn)
            End If

        End If

        da.Fill(dt)
        total_row = dt.Rows.Count
        conn.Close()
        GridView6.DataSource = dt
        GridView6.DataBind()

        Dim total_dr, total_cr As New Decimal(0)
        Dim I1 As Integer = 0
        For I1 = 0 To GridView6.Rows.Count - 1
            ''Calculating total Debit and Credit amount
            total_dr = total_dr + CDec(GridView6.Rows(I1).Cells(7).Text)
            total_cr = total_cr + CDec(GridView6.Rows(I1).Cells(8).Text)
        Next
        Dim dRow1 As DataRow
        dRow1 = dt.NewRow
        dRow1.Item(0) = "Total"
        dt.Rows.Add(dRow1)

        dt.AcceptChanges()
        GridView6.DataSource = dt
        GridView6.DataBind()

        GridView6.Rows(GridView6.Rows.Count - 1).Cells(7).Text = total_dr
        GridView6.Rows(GridView6.Rows.Count - 1).Cells(8).Text = total_cr
        GridView6.Rows(GridView6.Rows.Count - 1).Font.Bold = True
        ''''''''''============================'''''''''''

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Label33.Text = "Please select effective date."
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Label33.Text = "Invalid date format"
            Return
        ElseIf TextBox10.Text = "" Then
            TextBox10.Focus()
            Label33.Text = "Please select TO Date."
            Return
        ElseIf IsDate(TextBox10.Text) = False Then
            TextBox10.Text = ""
            TextBox10.Focus()
            Label33.Text = "Invalid date format"
            Return
        ElseIf TextBox11.Text = "" Then
            TextBox11.Focus()
            Label33.Text = "Please select FROM Date."
            Return
        ElseIf IsDate(TextBox11.Text) = False Then
            TextBox11.Text = ""
            TextBox11.Focus()
            Label33.Text = "Invalid date format"
            Return
        Else
            Label33.Text = ""
        End If

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                If (GridView6.Rows.Count = 0) Then
                    Label33.Text = "Please select data first."
                Else
                    Dim flag As New Boolean
                    flag = False
                    Dim amount_dr, amount_cr, MAT_STOCK, MAT_CODE, MAT_AVG, MAT_VALUE, NEW_MAT_AVG_PRICE, MAT_AVG_FROM_MATERIAL, MAT_STOCK_FROM_MATERIAL As New Decimal(0)
                    Dim garn_no_mb_no, garn_date As New String("")
                    Dim AC_PUR, AVG_PRICE As New String("")
                    Dim I As Integer = 0
                    For I = 0 To GridView6.Rows.Count - 2
                        amount_dr = 0
                        amount_cr = 0

                        ''Getting PSC values
                        garn_no_mb_no = GridView6.Rows(I).Cells(3).Text
                        garn_date = GridView6.Rows(I).Cells(6).Text
                        amount_dr = CDec(GridView6.Rows(I).Cells(7).Text)
                        amount_cr = CDec(GridView6.Rows(I).Cells(8).Text)

                        Dim PSC_SETTLEMENT_DATE As New Date
                        PSC_SETTLEMENT_DATE = CDate(TextBox2.Text)
                        Dim STR1 As String = ""
                        If PSC_SETTLEMENT_DATE.Month > 3 Then
                            STR1 = PSC_SETTLEMENT_DATE.Year
                            STR1 = STR1.Trim.Substring(2)
                            STR1 = STR1 & (STR1 + 1)
                        ElseIf PSC_SETTLEMENT_DATE.Month <= 3 Then
                            STR1 = PSC_SETTLEMENT_DATE.Year
                            STR1 = STR1.Trim.Substring(2)
                            STR1 = (STR1 - 1) & STR1
                        End If

                        If (DropDownList2.Text = "Raw Material") Then

                            ''Getting PUR HEAD for raw material
                            Dim MC As New SqlCommand
                            conn.Open()
                            MC.CommandText = "select distinct(AC_PUR), m1.MAT_AVG, m1.MAT_STOCK, m1.MAT_CODE from ledger l1 join PO_RCD_MAT p1 on l1.GARN_NO_MB_NO=p1.GARN_NO JOIN MATERIAL m1 ON p1.MAT_CODE=m1.MAT_CODE where p1.GARN_NO='" & garn_no_mb_no & "' and l1.POST_INDICATION='PSC'"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                AC_PUR = dr.Item("AC_PUR")
                                MAT_AVG_FROM_MATERIAL = dr.Item("MAT_AVG")
                                MAT_STOCK_FROM_MATERIAL = dr.Item("MAT_STOCK")
                                MAT_CODE = dr.Item("MAT_CODE")
                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If


                            ''''''''''''''''''''''''''''''

                            ''GETTING AVG PRICE AS PER LAST LINE NUMBER
                            conn.Open()
                            MC.CommandText = "SELECT LINE_NO, AVG_PRICE, MAT_BALANCE FROM MAT_DETAILS WHERE MAT_CODE='" & MAT_CODE & "' AND FISCAL_YEAR='" & STR1 & "' AND LINE_NO IN (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE='" & MAT_CODE & "' AND FISCAL_YEAR='" & STR1 & "' AND (LINE_TYPE like 'R' OR LINE_TYPE like 'I' OR LINE_TYPE like 'A' OR LINE_TYPE like 'PA'))"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()

                                If IsDBNull(dr.Item("LINE_NO")) Then
                                    MAT_AVG = MAT_AVG_FROM_MATERIAL
                                    'MAT_STOCK = MAT_STOCK_FROM_MATERIAL
                                Else
                                    MAT_AVG = dr.Item("AVG_PRICE")
                                    'MAT_STOCK = dr.Item("MAT_BALANCE")
                                End If

                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If

                            ''GETTING MAT BALANCE AS PER LAST LINE NUMBER
                            conn.Open()
                            MC.CommandText = "SELECT sum(MAT_QTY) as MAT_RCD_QTY FROM MAT_DETAILS WHERE MAT_CODE='" & MAT_CODE & "' AND FISCAL_YEAR='" & STR1 & "' AND ISSUE_TYPE='PURCHASE'"

                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()

                                If IsDBNull(dr.Item("MAT_RCD_QTY")) Then
                                    'MAT_AVG = MAT_AVG_FROM_MATERIAL
                                    MAT_STOCK = MAT_STOCK_FROM_MATERIAL
                                Else
                                    'MAT_AVG = dr.Item("AVG_PRICE")
                                    MAT_STOCK = dr.Item("MAT_RCD_QTY")
                                End If

                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If

                            ''''''''''''''''''''''''''''''
                            MAT_VALUE = MAT_AVG * MAT_STOCK

                        ElseIf (DropDownList2.Text = "Store") Then

                            ''Getting PUR HEAD for stores
                            Dim MC As New SqlCommand
                            conn.Open()
                            MC.CommandText = "select distinct(AC_CON) from ledger l1 join PO_RCD_MAT p1 on l1.GARN_NO_MB_NO=p1.GARN_NO JOIN MATERIAL m1 ON p1.MAT_CODE=m1.MAT_CODE where p1.GARN_NO='" & garn_no_mb_no & "' and l1.POST_INDICATION='PSC'"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                AC_PUR = dr.Item("AC_CON")
                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If

                        ElseIf (DropDownList2.Text = "Contracts") Then

                            ''Getting PUR HEAD for contracts
                            Dim MC As New SqlCommand
                            conn.Open()
                            MC.CommandText = "select distinct(pur_head) from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & GridView6.Rows(I).Cells(2).Text & "') and work_type=(select MAX(wo_type) from wo_order where po_no='" & GridView6.Rows(I).Cells(2).Text & "')"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                AC_PUR = dr.Item("pur_head")
                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If

                        ElseIf (DropDownList2.Text = "Transport") Then

                            If (Left(GridView6.Rows(I).Cells(3).Text, 2) = "DC" Or Left(GridView6.Rows(I).Cells(3).Text, 2) = "OS") Then
                                AC_PUR = "84001"
                            ElseIf (Left(GridView6.Rows(I).Cells(3).Text, 2) = "SC") Then
                                AC_PUR = "61601"
                            ElseIf (Left(GridView6.Rows(I).Cells(3).Text, 2) = "RC") Then
                                ''Getting PUR HEAD for RAW MATERIAL TRANSPORT CASES
                                conn.Open()
                                Dim MCc As New SqlCommand
                                MCc.CommandText = "select AC_PUR from MATERIAL where MAT_CODE IN (SELECT MAT_CODE FROM PO_RCD_MAT WHERE CRR_NO='" & GridView6.Rows(I).Cells(3).Text & "')"
                                MCc.Connection = conn
                                dr = MCc.ExecuteReader
                                If dr.HasRows Then
                                    dr.Read()
                                    AC_PUR = dr.Item("AC_PUR")
                                    dr.Close()
                                    conn.Close()
                                Else
                                    conn.Close()
                                End If
                            End If



                        End If


                        If (flag = False) Then

                            ''Generating PSC settlement number
                            conn.Open()
                            ds.Clear()
                            'da = New SqlDataAdapter("SELECT distinct(ISSUE_NO) FROM MAT_DETAILS WHERE ISSUE_NO LIKE '%PSC%' and FISCAL_YEAR=" & STR1, conn)
                            da = New SqlDataAdapter("SELECT distinct(PO_NO) FROM LEDGER WHERE PO_NO LIKE 'PSC%' and FISCAL_YEAR=" & STR1, conn)
                            count = da.Fill(dt)
                            conn.Close()

                            If count = 0 Then
                                TextBox1.Text = "PSC" & STR1 & "000001"
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
                                TextBox1.Text = "PSC" & STR1 & str
                            End If
                            flag = True

                        End If



                        ''Save ledger
                        If (amount_dr > 0 And amount_cr = 0) Then

                            ''''Updating the average price of the material only in case of raw materials
                            If (DropDownList2.Text = "Raw Material") Then

                                If (MAT_VALUE = 0) Then
                                    NEW_MAT_AVG_PRICE = Decimal.Round((MAT_AVG + amount_dr), 3, MidpointRounding.AwayFromZero)
                                Else
                                    NEW_MAT_AVG_PRICE = Decimal.Round(((MAT_VALUE + amount_dr) / MAT_STOCK), 3, MidpointRounding.AwayFromZero)
                                End If
                                ''UPDATING MATERIAL AVG PRICE
                                conn.Open()
                                mycommand = New SqlCommand("UPDATE MATERIAL SET MAT_AVG='" & NEW_MAT_AVG_PRICE & "' WHERE MAT_CODE='" & MAT_CODE & "'", conn)
                                mycommand.ExecuteNonQuery()
                                conn.Close()

                            End If

                            save_ledger(TextBox1.Text, garn_no_mb_no, "", garn_date, TextBox2.Text, AC_PUR, "Dr", amount_dr, "PSC_Adj", "", 1, "")
                            save_ledger(TextBox1.Text, garn_no_mb_no, "", garn_date, TextBox2.Text, DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1).Trim, "Cr", amount_dr, "PSC_Adj", "", 2, "")

                        ElseIf (amount_dr = 0 And amount_cr > 0) Then

                            ''''Updating the average price of the material only in case of raw materials
                            If (DropDownList2.Text = "Raw Material") Then

                                If (MAT_VALUE = 0) Then
                                    NEW_MAT_AVG_PRICE = Decimal.Round((MAT_AVG - amount_cr), 3, MidpointRounding.AwayFromZero)
                                Else
                                    NEW_MAT_AVG_PRICE = Decimal.Round(((MAT_VALUE - amount_cr) / MAT_STOCK), 3, MidpointRounding.AwayFromZero)
                                End If
                                ''UPDATING MATERIAL AVG PRICE
                                conn.Open()
                                mycommand = New SqlCommand("UPDATE MATERIAL SET MAT_AVG='" & NEW_MAT_AVG_PRICE & "' WHERE MAT_CODE='" & MAT_CODE & "'", conn)
                                mycommand.ExecuteNonQuery()
                                conn.Close()
                            End If

                            save_ledger(TextBox1.Text, garn_no_mb_no, "", garn_date, TextBox2.Text, DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1).Trim, "Dr", amount_cr, "PSC_Adj", "", 1, "")
                            save_ledger(TextBox1.Text, garn_no_mb_no, "", garn_date, TextBox2.Text, AC_PUR, "Cr", amount_cr, "PSC_Adj", "", 2, "")

                        End If






                        If (DropDownList2.Text = "Raw Material") Then

                            ''''''''''''''''''''''''''''=========================='''''''''''''''''''''''''''''''''

                            ''ADJUSTMENT LINE NO
                            Dim LINE_NO As Integer
                            conn.Open()
                            Dim MC As New SqlCommand
                            MC.CommandText = "select max(LINE_NO) AS LINE_NO from MAT_DETAILS where MAT_CODE= '" & MAT_CODE & "' and FISCAL_YEAR= " & STR1
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

                            Dim month1 As Integer = 0
                            month1 = PSC_SETTLEMENT_DATE.Month
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

                            cmd1.Parameters.AddWithValue("@ISSUE_NO", TextBox1.Text)
                            cmd1.Parameters.AddWithValue("@LINE_NO", LINE_NO + 1)
                            cmd1.Parameters.AddWithValue("@ISSUE_TYPE", "PSC ADJUSTMENT")
                            cmd1.Parameters.AddWithValue("@LINE_DATE", PSC_SETTLEMENT_DATE)
                            cmd1.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
                            cmd1.Parameters.AddWithValue("@LINE_TYPE", "PA")
                            cmd1.Parameters.AddWithValue("@MAT_CODE", MAT_CODE)
                            cmd1.Parameters.AddWithValue("@MAT_QTY", 0)
                            cmd1.Parameters.AddWithValue("@RQD_QTY", 0)
                            cmd1.Parameters.AddWithValue("@ISSUE_QTY", 0)
                            cmd1.Parameters.AddWithValue("@MAT_BALANCE", MAT_STOCK_FROM_MATERIAL)
                            cmd1.Parameters.AddWithValue("@UNIT_PRICE", NEW_MAT_AVG_PRICE)
                            cmd1.Parameters.AddWithValue("@TOTAL_PRICE", 0)
                            cmd1.Parameters.AddWithValue("@DEPT_CODE", "RM")
                            cmd1.Parameters.AddWithValue("@PURPOSE", "FOR PSC ADJ.")
                            cmd1.Parameters.AddWithValue("@COST_CODE", "070000")
                            cmd1.Parameters.AddWithValue("@AUTH_BY", Session("userName"))
                            cmd1.Parameters.AddWithValue("@POST_TYPE", "AUTH")
                            cmd1.Parameters.AddWithValue("@REMARKS", garn_no_mb_no)
                            cmd1.Parameters.AddWithValue("@RQD_DATE", PSC_SETTLEMENT_DATE)
                            cmd1.Parameters.AddWithValue("@QTR", qtr1)
                            cmd1.Parameters.AddWithValue("@AVG_PRICE", NEW_MAT_AVG_PRICE)
                            cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
                            cmd1.ExecuteReader()
                            cmd1.Dispose()
                            conn.Close()

                            ''''''''''''''''''''''''''''=========================='''''''''''''''''''''''''''''''''
                        End If

                        ''UPDATING LEDGER
                        conn.Open()
                        mycommand = New SqlCommand("UPDATE LEDGER SET PAYMENT_INDICATION='S' WHERE GARN_NO_MB_NO='" & garn_no_mb_no & "' AND AC_NO='" & DropDownList1.Text.Substring(0, DropDownList1.Text.IndexOf(",") - 1).Trim & "' AND POST_INDICATION='PSC'", conn)
                        mycommand.ExecuteNonQuery()
                        conn.Close()

                    Next

                    Dim dt2 As New DataTable()
                    dt2.Columns.AddRange(New DataColumn(8) {New DataColumn("A/c No"), New DataColumn("A/c Name"), New DataColumn("PO NO"), New DataColumn("GARN NO"), New DataColumn("Voucher No"), New DataColumn("Supplier"), New DataColumn("Date"), New DataColumn("DR"), New DataColumn("CR")})
                    'ViewState("mat2") = dt2
                    GridView6.DataSource = dt2
                    GridView6.DataBind()
                End If

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label33.Visible = True
                Label33.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try
        End Using

    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If (DropDownList4.Text = "PSC Settlement") Then
            MultiView1.ActiveViewIndex = 0
        ElseIf (DropDownList4.Text = "Issue Revise") Then
            MultiView1.ActiveViewIndex = 1
        ElseIf (DropDownList4.Text = "Issue Adj. Entry") Then
            MultiView1.ActiveViewIndex = 2
        ElseIf (DropDownList4.Text = "Raw Mat. Avg. Correction") Then
            MultiView1.ActiveViewIndex = 3
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox5.Text = "" Then
            TextBox5.Focus()
            Label18.Text = "Please select TO Date."
            Return
        ElseIf IsDate(TextBox5.Text) = False Then
            TextBox5.Text = ""
            TextBox5.Focus()
            Label18.Text = "Invalid date format"
            Return
        ElseIf TextBox6.Text = "" Then
            TextBox6.Focus()
            Label18.Text = "Please select FROM Date."
            Return
        ElseIf IsDate(TextBox6.Text) = False Then
            TextBox6.Text = ""
            TextBox6.Focus()
            Label18.Text = "Invalid date format"
            Return
        Else
            Label18.Text = ""
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox5.Text)
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

        conn.Open()
        dt.Clear()

        Dim total_row As Integer

        'da = New SqlDataAdapter("SELECT MAT_DETAILS.MAT_CODE,LEDGER.AC_NO, ACDIC.ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.ENTRY_DATE As Entry_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code JOIN MAT_DETAILS ON LEDGER.GARN_NO_MB_NO=MAT_DETAILS.ISSUE_NO WHERE (GARN_NO_MB_NO like 'ri%' or GARN_NO_MB_NO like 'adj%') and EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND POST_INDICATION <> 'Material Issue Return' AND POST_INDICATION <> 'Material Con Return' order by GARN_NO_MB_NO,Entry_Date, LEDGER.AC_NO", conn)
        da = New SqlDataAdapter("SELECT MAT_DETAILS.MAT_CODE,LEDGER.AC_NO, ACDIC.ac_description, LEDGER.PO_NO, LEDGER.GARN_NO_MB_NO AS GARN, LEDGER.VOUCHER_NO, LEDGER.EFECTIVE_DATE As Efective_Date, LEDGER.ENTRY_DATE As Entry_Date, LEDGER.SUPL_ID AS SUPL_ID, LEDGER.AMOUNT_DR AS DR, LEDGER.AMOUNT_CR  AS CR FROM LEDGER join ACDIC on LEDGER.AC_NO =ACDIC.ac_code JOIN MAT_DETAILS ON LEDGER.GARN_NO_MB_NO=MAT_DETAILS.ISSUE_NO WHERE (GARN_NO_MB_NO like 'ri%') and EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND POST_INDICATION <> 'Material Issue Return' AND POST_INDICATION <> 'Material Con Return' order by GARN_NO_MB_NO,Entry_Date, LEDGER.AC_NO", conn)

        da.Fill(dt)
        total_row = dt.Rows.Count
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()


        ''''''''''============================'''''''''''

    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If TextBox5.Text = "" Then
            TextBox5.Focus()
            Label18.Text = "Please select TO Date."
            Return
        ElseIf IsDate(TextBox5.Text) = False Then
            TextBox5.Text = ""
            TextBox5.Focus()
            Label18.Text = "Invalid date format"
            Return
        ElseIf TextBox6.Text = "" Then
            TextBox6.Focus()
            Label18.Text = "Please select FROM Date."
            Return
        ElseIf IsDate(TextBox6.Text) = False Then
            TextBox6.Text = ""
            TextBox6.Focus()
            Label18.Text = "Invalid date format"
            Return
        Else
            Label18.Text = ""
        End If

        Dim from_date, to_date As Date
        from_date = CDate(TextBox5.Text)
        to_date = CDate(TextBox6.Text)

        If (GridView1.Rows.Count = 0) Then
            Label33.Text = "Please select data first."
        Else
            Dim flag As New Boolean
            flag = False
            Dim amount_dr, amount_cr, ORIGINAL_OPENING_STOCK, OPENING_VALUE, CUMMULATIVE, OPENING_MAT_STOCK, CUMM_MAT_BALANCE, MAT_QTY, ISSUE_QTY, CUMM_RCD_VALUE, CUMM_ISSUE_VALUE, NEW_ISSUE_VALUE, MAT_CODE, MAT_AVG, MAT_VALUE, NEW_MAT_AVG_PRICE, MAT_AVG_FROM_MATERIAL, MAT_STOCK_FROM_MATERIAL As New Decimal(0)
            Dim garn_no_mb_no, garn_date As New String("")
            Dim AC_PUR, AVG_PRICE, AC_ISSUE As New String("")
            Dim I As Integer = 0
            For I = 0 To GridView1.Rows.Count - 1
                amount_dr = 0
                amount_cr = 0

                ''Getting PSC values
                MAT_CODE = GridView1.Rows(I).Cells(0).Text
                garn_no_mb_no = GridView1.Rows(I).Cells(4).Text
                garn_date = GridView1.Rows(I).Cells(6).Text
                amount_dr = CDec(GridView1.Rows(I).Cells(8).Text)
                amount_cr = CDec(GridView1.Rows(I).Cells(9).Text)

                Dim PSC_SETTLEMENT_DATE As New Date
                PSC_SETTLEMENT_DATE = CDate(TextBox5.Text)
                Dim STR1 As String = ""
                If PSC_SETTLEMENT_DATE.Month > 3 Then
                    STR1 = PSC_SETTLEMENT_DATE.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = STR1 & (STR1 + 1)
                ElseIf PSC_SETTLEMENT_DATE.Month <= 3 Then
                    STR1 = PSC_SETTLEMENT_DATE.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = (STR1 - 1) & STR1
                End If

                ''Getting PUR HEAD for raw material
                Dim MC As New SqlCommand
                conn.Open()
                MC.CommandText = "select open_stock,AC_PUR,AC_ISSUE from MATERIAL where MAT_CODE='" & MAT_CODE & "'"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    ORIGINAL_OPENING_STOCK = dr.Item("open_stock")
                    AC_PUR = dr.Item("AC_PUR")
                    AC_ISSUE = dr.Item("AC_ISSUE")

                    dr.Close()
                    conn.Close()
                Else
                    conn.Close()
                End If

                ''GETTING upto Mat Stock
                conn.Open()
                MC.CommandText = "Select (SUM(MAT_QTY) - SUM(ISSUE_QTY)) As CUMMULATIVE FROM MAT_DETAILS WHERE MAT_CODE= '" & GridView1.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR < '" & STR1 & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows = True Then
                    dr.Read()
                    If IsDBNull(dr.Item("CUMMULATIVE")) Then
                        CUMMULATIVE = 0
                    Else
                        CUMMULATIVE = dr.Item("CUMMULATIVE")
                    End If
                    'CUMMULATIVE = dr.Item("CUMMULATIVE")
                    If (CUMMULATIVE = 0) Then
                        OPENING_MAT_STOCK = ORIGINAL_OPENING_STOCK
                    ElseIf ((ORIGINAL_OPENING_STOCK + CUMMULATIVE) = 0) Then
                        OPENING_MAT_STOCK = 0.00
                    Else
                        OPENING_MAT_STOCK = Format(ORIGINAL_OPENING_STOCK + CUMMULATIVE, "#.000")
                    End If
                    dr.Close()
                Else
                    OPENING_MAT_STOCK = ORIGINAL_OPENING_STOCK
                    dr.Close()
                End If
                conn.Close()

                ''''''''''''''''''''''''''''''

                ''CALCULATING OPENING VALUE
                conn.Open()
                'mc1.CommandText = "SELECT TOP 1 UNIT_PRICE FROM MAT_DETAILS WHERE MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' AND LINE_DATE < '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' ORDER BY MAT_CODE,LINE_NO DESC"
                MC.CommandText = "select (ac_dr-ac_cr) AS OPENING_VALUE from ob_trial WHERE ac_code=(select AC_PUR from MATERIAL where MAT_CODE='" & GridView1.Rows(I).Cells(0).Text & "') AND ac_fy=" & STR1
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows = True Then
                    dr.Read()
                    OPENING_VALUE = dr.Item("OPENING_VALUE")
                    dr.Close()
                Else
                    OPENING_VALUE = 0.00
                    dr.Close()
                End If
                conn.Close()

                ''CALCULATING TOTAL RCD QTY
                conn.Open()
                MC.CommandText = "SELECT (SUM(MAT_QTY) - SUM(ISSUE_QTY)) AS MAT_BALANCE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE <= '" & GridView1.Rows(I).Cells(6).Text & "' AND (LINE_TYPE='R' or LINE_TYPE='I' or LINE_TYPE='A') and MAT_CODE='" & GridView1.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows = True Then
                    dr.Read()
                    If IsDBNull(dr.Item("MAT_BALANCE")) Then
                        CUMM_MAT_BALANCE = 0.00
                    Else
                        CUMM_MAT_BALANCE = dr.Item("MAT_BALANCE")
                    End If


                    CUMM_MAT_BALANCE = Format(CUMM_MAT_BALANCE, "#.000")

                    dr.Close()
                Else
                    CUMM_MAT_BALANCE = 0.00
                    dr.Close()
                End If
                conn.Close()

                ''CALCULATING CUMM RCD VALUATION
                conn.Open()
                MC.CommandText = "select (case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR_PURCHASE,(case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR_PURCHASE from LEDGER where AC_NO=(select AC_PUR from MATERIAL where MAT_CODE='" & GridView1.Rows(I).Cells(0).Text & "') AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & GridView1.Rows(I).Cells(6).Text & "'"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    If IsDBNull(dr.Item("AMOUNT_DR_PURCHASE") And dr.Item("AMOUNT_CR_PURCHASE")) Then
                        CUMM_RCD_VALUE = 0.00
                    Else
                        CUMM_RCD_VALUE = dr.Item("AMOUNT_DR_PURCHASE") - dr.Item("AMOUNT_CR_PURCHASE")
                    End If

                    dr.Close()
                    conn.Close()
                Else
                    CUMM_RCD_VALUE = 0.00
                    conn.Close()
                End If

                ''CALCULATING CUMM ISSUE VALUATION
                conn.Open()
                MC.CommandText = "select (case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR_PURCHASE,(case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR_PURCHASE from LEDGER where AC_NO=(select AC_ISSUE from MATERIAL where MAT_CODE='" & GridView1.Rows(I).Cells(0).Text & "') AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & GridView1.Rows(I).Cells(6).Text & "'"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    If IsDBNull(dr.Item("AMOUNT_DR_PURCHASE") And dr.Item("AMOUNT_CR_PURCHASE")) Then
                        CUMM_ISSUE_VALUE = 0.00
                    Else
                        CUMM_ISSUE_VALUE = dr.Item("AMOUNT_CR_PURCHASE") - dr.Item("AMOUNT_DR_PURCHASE")
                    End If

                    dr.Close()
                    conn.Close()
                Else
                    CUMM_ISSUE_VALUE = 0.00
                    conn.Close()
                End If

                ''''''''''''''''''''''''''''''
                If ((OPENING_MAT_STOCK + CUMM_MAT_BALANCE) > 0) Then
                    MAT_AVG = Format((OPENING_VALUE + CUMM_RCD_VALUE - CUMM_ISSUE_VALUE) / (OPENING_MAT_STOCK + CUMM_MAT_BALANCE), "#.000")
                Else
                    MAT_AVG = 0.00
                End If


                ''CALCULATING PARTICULAR ISSUE QTY
                conn.Open()
                'MC.CommandText = "select SUM(MAT_QTY) AS MAT_QTY, SUM(ISSUE_QTY) AS ISSUE_QTY from MAT_DETAILS where ISSUE_NO='" & GridView1.Rows(I).Cells(4).Text & "'"
                MC.CommandText = "select SUM(ISSUE_QTY) AS ISSUE_QTY from MAT_DETAILS where ISSUE_NO='" & GridView1.Rows(I).Cells(4).Text & "'"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows = True Then
                    dr.Read()
                    If IsDBNull(dr.Item("ISSUE_QTY")) Then
                        'MAT_QTY = 0.00
                        ISSUE_QTY = 0.00
                    Else
                        'MAT_QTY = dr.Item("MAT_QTY")
                        ISSUE_QTY = dr.Item("ISSUE_QTY")
                    End If

                    'If (MAT_QTY > 0) Then
                    '    ISSUE_QTY = Format(MAT_QTY, "#.000")
                    'Else
                    '    ISSUE_QTY = Format(ISSUE_QTY, "#.000")
                    'End If

                    ISSUE_QTY = Format(ISSUE_QTY, "#.000")

                    dr.Close()
                Else
                    ISSUE_QTY = 0.00
                    dr.Close()
                End If
                conn.Close()

                NEW_ISSUE_VALUE = Format(ISSUE_QTY * MAT_AVG, "#.000")

                ''Save ledger
                If (amount_dr > 0 And amount_cr = 0) Then

                    ''''Updating the average price of the material only in case of raw materials
                    ''UPDATING MATERIAL AVG PRICE
                    conn.Open()
                    mycommand = New SqlCommand("UPDATE LEDGER SET REVERSAL_INDICATOR='Issue_Revised',AMOUNT_DR='" & NEW_ISSUE_VALUE & "' WHERE GARN_NO_MB_NO='" & garn_no_mb_no & "' AND AC_NO='" & GridView1.Rows(I).Cells(1).Text & "' AND AMOUNT_DR=" & amount_dr & " AND AMOUNT_CR=" & amount_cr, conn)
                    mycommand.ExecuteNonQuery()
                    conn.Close()

                ElseIf (amount_dr = 0 And amount_cr > 0) Then

                    ''''Updating the average price of the material only in case of raw materials
                    ''UPDATING MATERIAL AVG PRICE
                    conn.Open()
                    mycommand = New SqlCommand("UPDATE LEDGER SET REVERSAL_INDICATOR='Issue_Revised',AMOUNT_CR='" & NEW_ISSUE_VALUE & "' WHERE GARN_NO_MB_NO='" & garn_no_mb_no & "' AND AC_NO='" & GridView1.Rows(I).Cells(1).Text & "' AND AMOUNT_DR=" & amount_dr & " AND AMOUNT_CR=" & amount_cr, conn)
                    mycommand.ExecuteNonQuery()
                    conn.Close()

                End If
            Next

            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(8) {New DataColumn("A/c No"), New DataColumn("A/c Name"), New DataColumn("PO NO"), New DataColumn("GARN NO"), New DataColumn("Voucher No"), New DataColumn("Supplier"), New DataColumn("Date"), New DataColumn("DR"), New DataColumn("CR")})
            'ViewState("mat2") = dt2
            GridView1.DataSource = dt2
            GridView1.DataBind()
        End If
    End Sub


    Protected Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT * FROM MATERIAL WHERE MAT_CODE LIKE '100%' ORDER BY MAT_CODE", conn)
        da.Fill(dt)
        conn.Close()
        GridView7.DataSource = dt
        GridView7.DataBind()

        Dim from_date, to_date As Date
        from_date = CDate(TextBox9.Text)
        to_date = CDate(TextBox12.Text)

        Dim PSC_SETTLEMENT_DATE As New Date
        PSC_SETTLEMENT_DATE = CDate(TextBox7.Text)
        Dim STR1 As String = ""
        If PSC_SETTLEMENT_DATE.Month > 3 Then
            STR1 = PSC_SETTLEMENT_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf PSC_SETTLEMENT_DATE.Month <= 3 Then
            STR1 = PSC_SETTLEMENT_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        Dim I As Integer = 0
        For I = 0 To GridView7.Rows.Count - 1
            Dim OPENING_STOCK, OP_AVG, CUMMULATIVE, OPENING_STOCK_VALUE, MAT_AVG_FROM_MATERIAL, MAT_STOCK_FROM_MATERIAL As Decimal
            Dim RCD_QTY, ISSUE_QTY, RCD_VALUE, ISSUE_VALUE As Decimal
            OPENING_STOCK = 0
            OPENING_STOCK_VALUE = 0

            ''CALCULATING OPENING QTY
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "Select OPEN_STOCK As op_stock, MAT_AVG AS OP_AVG,MAT_STOCK FROM MATERIAL WHERE MAT_CODE= '" & GridView7.Rows(I).Cells(0).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                OPENING_STOCK = dr.Item("op_stock")
                OP_AVG = dr.Item("OP_AVG")
                MAT_AVG_FROM_MATERIAL = OP_AVG
                MAT_STOCK_FROM_MATERIAL = dr.Item("MAT_STOCK")
                dr.Close()
            Else
                GridView7.Rows(I).Cells(2).Text = "0.000"
                dr.Close()
            End If
            conn.Close()

            conn.Open()
            'mc1.CommandText = "Select (SUM(MAT_QTY) - SUM(ISSUE_QTY)) As CUMMULATIVE FROM MAT_DETAILS WHERE MAT_CODE= '" & GridView7.Rows(I).Cells(0).Text & "' AND LINE_DATE < '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.CommandText = "Select (SUM(MAT_QTY) - SUM(ISSUE_QTY)) As CUMMULATIVE FROM MAT_DETAILS WHERE MAT_CODE= '" & GridView7.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR < '" & STR1 & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr.Item("CUMMULATIVE")) Then
                    CUMMULATIVE = 0
                Else
                    CUMMULATIVE = dr.Item("CUMMULATIVE")
                End If
                'CUMMULATIVE = dr.Item("CUMMULATIVE")
                If (CUMMULATIVE = 0) Then
                    GridView7.Rows(I).Cells(2).Text = OPENING_STOCK
                ElseIf ((OPENING_STOCK + CUMMULATIVE) = 0) Then
                    GridView7.Rows(I).Cells(2).Text = "0.000"
                Else
                    GridView7.Rows(I).Cells(2).Text = Format(OPENING_STOCK + CUMMULATIVE, "#.000")
                End If
                dr.Close()
            Else
                GridView7.Rows(I).Cells(2).Text = OPENING_STOCK
                dr.Close()
            End If
            conn.Close()

            ''CALCULATING OPENING VALUE
            conn.Open()
            'mc1.CommandText = "SELECT TOP 1 UNIT_PRICE FROM MAT_DETAILS WHERE MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' AND LINE_DATE < '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' ORDER BY MAT_CODE,LINE_NO DESC"
            mc1.CommandText = "select (ac_dr-ac_cr) AS OPENING_VALUE from ob_trial WHERE ac_code=(select AC_PUR from MATERIAL where MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "') AND ac_fy=" & STR1
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                GridView7.Rows(I).Cells(3).Text = dr.Item("OPENING_VALUE")

                dr.Close()
            Else
                GridView7.Rows(I).Cells(3).Text = "0.000"

                dr.Close()
            End If
            conn.Close()

            ''GETTING PURCHASE AND ISSUE CODE FROM MATERIAL
            Dim PUR_VALUE, ISSUE_AVG, PSC_PRICE As New Decimal(0)
            Dim AC_PUR, AC_ISSUE As New String("")
            conn.Open()
            'mc1.CommandText = "SELECT LINE_NO,AVG_PRICE, MAT_BALANCE FROM MAT_DETAILS WHERE MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR='" & STR1 & "' AND LINE_NO IN (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE<='" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "')"
            mc1.CommandText = "select AC_PUR, AC_ISSUE from material where MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                AC_PUR = dr.Item("AC_PUR")
                AC_ISSUE = dr.Item("AC_ISSUE")

                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            '''''''''''''''''''''''''''''''''''''''
            ''CALCULATING TOTAL RCD QTY
            conn.Open()
            'mc1.CommandText = "SELECT SUM(MAT_QTY) AS RCD_QTY, SUM(TOTAL_PRICE) AS RCD_VALUE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND LINE_TYPE='R' and MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.CommandText = "SELECT SUM(MAT_QTY) AS RCD_QTY, SUM(TOTAL_PRICE) AS RCD_VALUE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE <= '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (LINE_TYPE='R' or LINE_TYPE='A') and MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                RCD_QTY = dr.Item("RCD_QTY")
                RCD_VALUE = dr.Item("RCD_VALUE")
                If (RCD_QTY = 0) Then
                    GridView7.Rows(I).Cells(4).Text = "0.000"
                Else
                    GridView7.Rows(I).Cells(4).Text = Format(RCD_QTY, "#.000")
                End If

                dr.Close()
            Else
                GridView7.Rows(I).Cells(4).Text = "0.000"
                GridView7.Rows(I).Cells(5).Text = "0.000"
                dr.Close()
            End If
            conn.Close()

            ''CALCULATING TOTAL RCD VALUATION
            conn.Open()
            'mc1.CommandText = "SELECT LINE_NO,AVG_PRICE, MAT_BALANCE FROM MAT_DETAILS WHERE MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR='" & STR1 & "' AND LINE_NO IN (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE<='" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "')"
            ''mc1.CommandText = "SELECT sum(TOTAL_PRICE) AS TOTAL_PRICE FROM MAT_DETAILS WHERE MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR='" & STR1 & "' AND ISSUE_TYPE='PURCHASE'"
            mc1.CommandText = "select (case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR_PURCHASE,(case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR_PURCHASE from LEDGER where AC_NO=(select AC_PUR from MATERIAL where MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "') AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                If IsDBNull(dr.Item("AMOUNT_DR_PURCHASE") And dr.Item("AMOUNT_CR_PURCHASE")) Then
                    PUR_VALUE = 0
                Else
                    PUR_VALUE = dr.Item("AMOUNT_DR_PURCHASE") - dr.Item("AMOUNT_CR_PURCHASE")
                End If

                If (PUR_VALUE = 0) Then
                    GridView7.Rows(I).Cells(5).Text = "0.000"
                Else
                    GridView7.Rows(I).Cells(5).Text = Format(PUR_VALUE, "#.000")

                End If


                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If

            'Ideal issue Avg price
            If (CDec(GridView7.Rows(I).Cells(2).Text) + CDec(GridView7.Rows(I).Cells(4).Text) > 0) Then
                ISSUE_AVG = Format((CDec(GridView7.Rows(I).Cells(3).Text) + CDec(GridView7.Rows(I).Cells(5).Text)) / (CDec(GridView7.Rows(I).Cells(2).Text) + CDec(GridView7.Rows(I).Cells(4).Text)), "#.000")
            End If

            ''CALCULATING TOTAL ISSUE QTY & ISSUE VALUATION
            conn.Open()
            mc1.CommandText = "SELECT SUM(ISSUE_QTY) AS ISSUE_QTY, SUM(TOTAL_PRICE) AS ISSUE_VALUE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE <= '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (LINE_TYPE='I' or LINE_TYPE='A') and MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                ISSUE_QTY = dr.Item("ISSUE_QTY")
                ISSUE_VALUE = dr.Item("ISSUE_VALUE")
                If (ISSUE_QTY = 0) Then
                    GridView7.Rows(I).Cells(6).Text = "0.000"
                    GridView7.Rows(I).Cells(8).Text = Format(ISSUE_QTY * ISSUE_AVG, "#.00")
                Else
                    GridView7.Rows(I).Cells(6).Text = Format(ISSUE_QTY, "#.000")
                    GridView7.Rows(I).Cells(8).Text = Format(ISSUE_QTY * ISSUE_AVG, "#.00")
                End If

                dr.Close()
            Else
                GridView7.Rows(I).Cells(6).Text = "0.000"
                GridView7.Rows(I).Cells(8).Text = "0.00"
                dr.Close()
            End If
            conn.Close()

            ''Issue value new
            conn.Open()
            mc1.CommandText = "select (case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR_ISSUE,(case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR_ISSUE from LEDGER where AC_NO=(select AC_ISSUE from MATERIAL where MAT_CODE='" & GridView7.Rows(I).Cells(0).Text & "') AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and POST_INDICATION <>'STOCK TRANSFOR'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                If IsDBNull(dr.Item("AMOUNT_DR_ISSUE") And dr.Item("AMOUNT_CR_ISSUE")) Then
                    ISSUE_VALUE = 0
                Else
                    ISSUE_VALUE = dr.Item("AMOUNT_CR_ISSUE") - dr.Item("AMOUNT_DR_ISSUE")
                End If

                If (ISSUE_VALUE = 0) Then
                    GridView7.Rows(I).Cells(7).Text = "0.000"
                Else
                    GridView7.Rows(I).Cells(7).Text = Format(ISSUE_VALUE, "#.000")

                End If


                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If

            GridView7.Rows(I).Cells(9).Text = CDec(GridView7.Rows(I).Cells(8).Text) - CDec(GridView7.Rows(I).Cells(7).Text)
        Next
    End Sub


    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim amount_dr, amount_cr, ORIGINAL_OPENING_STOCK, OPENING_VALUE, CUMMULATIVE, OPENING_MAT_STOCK, CUMM_MAT_BALANCE, MAT_QTY, ISSUE_QTY, CUMM_RCD_VALUE, CUMM_ISSUE_VALUE, NEW_ISSUE_VALUE, MAT_CODE, MAT_AVG, MAT_VALUE, NEW_MAT_AVG_PRICE, MAT_AVG_FROM_MATERIAL, MAT_STOCK_FROM_MATERIAL As New Decimal(0)
        Dim garn_no_mb_no, issue_adj_date As New String("")
        Dim AC_CON, AVG_PRICE, AC_ISSUE As New String("")

        Dim PSC_SETTLEMENT_DATE As New Date
        PSC_SETTLEMENT_DATE = CDate(TextBox7.Text)
        Dim STR1 As String = ""
        If PSC_SETTLEMENT_DATE.Month > 3 Then
            STR1 = PSC_SETTLEMENT_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf PSC_SETTLEMENT_DATE.Month <= 3 Then
            STR1 = PSC_SETTLEMENT_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        conn.Open()
        ds.Clear()
        'da = New SqlDataAdapter("SELECT distinct(ISSUE_NO) FROM MAT_DETAILS WHERE ISSUE_NO LIKE '%PSC%' and FISCAL_YEAR=" & STR1, conn)
        da = New SqlDataAdapter("SELECT distinct(PO_NO) FROM LEDGER WHERE PO_NO LIKE 'ISSUEADJ%' and FISCAL_YEAR=" & STR1, conn)
        count = da.Fill(dt)
        conn.Close()

        If count = 0 Then
            TextBox8.Text = "ISSUEADJ" & STR1 & "000001"
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
            TextBox8.Text = "ISSUEADJ" & STR1 & str
        End If



        issue_adj_date = TextBox7.Text
        Dim I As Integer = 0
        For I = 0 To GridView7.Rows.Count - 1
            ''Getting PUR HEAD for raw material
            MAT_CODE = GridView7.Rows(I).Cells(0).Text
            Dim MC As New SqlCommand
            conn.Open()
            MC.CommandText = "select AC_ISSUE, AC_CON from MATERIAL where MAT_CODE='" & MAT_CODE & "'"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                AC_CON = dr.Item("AC_CON")
                AC_ISSUE = dr.Item("AC_ISSUE")

                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If

            If (CDec(GridView7.Rows(I).Cells(9).Text) > 0) Then

                save_ledger_Issue_Adj(TextBox8.Text, "", "", issue_adj_date, issue_adj_date, AC_ISSUE, "Cr", CDec(GridView7.Rows(I).Cells(9).Text), "ISSUE_REVISE", "", 1, "")
                save_ledger_Issue_Adj(TextBox8.Text, "", "", issue_adj_date, issue_adj_date, AC_CON, "Dr", CDec(GridView7.Rows(I).Cells(9).Text), "ISSUE_REVISE", "", 2, "")

            ElseIf (CDec(GridView7.Rows(I).Cells(9).Text) < 0) Then

                save_ledger_Issue_Adj(TextBox8.Text, "", "", CDate(issue_adj_date).Date, issue_adj_date, AC_ISSUE, "Dr", CDec(GridView7.Rows(I).Cells(9).Text) * (-1), "ISSUE_REVISE", "", 1, "")
                save_ledger_Issue_Adj(TextBox8.Text, "", "", CDate(issue_adj_date).Date, issue_adj_date, AC_CON, "Cr", CDec(GridView7.Rows(I).Cells(9).Text) * (-1), "ISSUE_REVISE", "", 2, "")

            End If
        Next
    End Sub

    Protected Sub save_ledger(so_no As String, garn_mb As String, inv_no As String, garn_date As String, effective_date As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, token_no As String, line_no As Integer, PAY_IND As String)

        Dim working_date As Date

        working_date = CDate(effective_date)
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
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date)
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

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If TextBox15.Text = "" Then
            TextBox15.Focus()
            Return
        ElseIf IsDate(TextBox15.Text) = False Then
            TextBox15.Text = ""
            TextBox15.Focus()
            Return
        ElseIf TextBox16.Text = "" Then
            TextBox16.Focus()
            Return
        ElseIf IsDate(TextBox16.Text) = False Then
            TextBox16.Text = ""
            TextBox16.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox15.Text)
        to_date = CDate(TextBox16.Text)

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
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT * FROM MATERIAL WHERE MAT_CODE LIKE '100%' ORDER BY MAT_CODE", conn)
        da.Fill(dt)
        conn.Close()
        GridView2.DataSource = dt
        GridView2.DataBind()

        '''''''''''''''''''''''''''
        Dim I As Integer = 0
        For I = 0 To GridView2.Rows.Count - 1
            Dim OPENING_STOCK, OP_AVG, CUMMULATIVE, OPENING_STOCK_VALUE, MAT_AVG_FROM_MATERIAL, MAT_STOCK_FROM_MATERIAL As Decimal
            Dim RCD_QTY, ISSUE_QTY, RCD_VALUE, ISSUE_VALUE, MISC_SALE_QTY, MISC_SALE_VALUE As Decimal
            OPENING_STOCK = 0
            OPENING_STOCK_VALUE = 0

            ''CALCULATING OPENING QTY
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "Select OPEN_STOCK As op_stock, MAT_AVG AS OP_AVG,MAT_STOCK FROM MATERIAL WHERE MAT_CODE= '" & GridView2.Rows(I).Cells(0).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                OPENING_STOCK = dr.Item("op_stock")
                OP_AVG = dr.Item("OP_AVG")
                MAT_AVG_FROM_MATERIAL = OP_AVG
                MAT_STOCK_FROM_MATERIAL = dr.Item("MAT_STOCK")
                dr.Close()
            Else
                GridView2.Rows(I).Cells(2).Text = "0.000"
                dr.Close()
            End If
            conn.Close()

            conn.Open()
            'mc1.CommandText = "Select (SUM(MAT_QTY) - SUM(ISSUE_QTY)) As CUMMULATIVE FROM MAT_DETAILS WHERE MAT_CODE= '" & GridView2.Rows(I).Cells(0).Text & "' AND LINE_DATE < '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.CommandText = "Select (SUM(MAT_QTY) - SUM(ISSUE_QTY)) As CUMMULATIVE FROM MAT_DETAILS WHERE MAT_CODE= '" & GridView2.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR < '" & STR1 & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr.Item("CUMMULATIVE")) Then
                    CUMMULATIVE = 0
                Else
                    CUMMULATIVE = dr.Item("CUMMULATIVE")
                End If
                'CUMMULATIVE = dr.Item("CUMMULATIVE")
                If (CUMMULATIVE = 0) Then
                    GridView2.Rows(I).Cells(2).Text = OPENING_STOCK
                ElseIf ((OPENING_STOCK + CUMMULATIVE) = 0) Then
                    GridView2.Rows(I).Cells(2).Text = "0.000"
                Else
                    GridView2.Rows(I).Cells(2).Text = Format(OPENING_STOCK + CUMMULATIVE, "#.000")
                End If
                dr.Close()
            Else
                GridView2.Rows(I).Cells(2).Text = OPENING_STOCK
                dr.Close()
            End If
            conn.Close()

            ''CALCULATING OPENING VALUE
            conn.Open()
            'mc1.CommandText = "SELECT TOP 1 UNIT_PRICE FROM MAT_DETAILS WHERE MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "' AND LINE_DATE < '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' ORDER BY MAT_CODE,LINE_NO DESC"
            mc1.CommandText = "select (ac_dr-ac_cr) AS OPENING_VALUE from ob_trial WHERE ac_code=(select AC_PUR from MATERIAL where MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "') AND ac_fy=" & STR1
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                GridView2.Rows(I).Cells(3).Text = dr.Item("OPENING_VALUE")

                dr.Close()
            Else
                GridView2.Rows(I).Cells(3).Text = "0.000"

                dr.Close()
            End If
            conn.Close()


            '''''''''''''''''''''''''''''''''''''''
            ''GETTING PURCHASE AND ISSUE CODE FROM MATERIAL
            Dim PUR_VALUE, ISSUE_AVG, PSC_PRICE As New Decimal(0)
            Dim AC_PUR, AC_ISSUE As New String("")
            conn.Open()
            'mc1.CommandText = "SELECT LINE_NO,AVG_PRICE, MAT_BALANCE FROM MAT_DETAILS WHERE MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR='" & STR1 & "' AND LINE_NO IN (SELECT MAX(LINE_NO) FROM MAT_DETAILS WHERE MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE<='" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "')"
            mc1.CommandText = "select AC_PUR, AC_ISSUE from material where MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                AC_PUR = dr.Item("AC_PUR")
                AC_ISSUE = dr.Item("AC_ISSUE")

                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            '''''''''''''''''''''''''''''''''''''''
            ''CALCULATING TOTAL RCD QTY
            conn.Open()
            'mc1.CommandText = "SELECT SUM(MAT_QTY) AS RCD_QTY, SUM(TOTAL_PRICE) AS RCD_VALUE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND LINE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND LINE_TYPE='R' and MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.CommandText = "SELECT SUM(MAT_QTY) AS RCD_QTY, SUM(TOTAL_PRICE) AS RCD_VALUE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE <= '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (LINE_TYPE='R' or LINE_TYPE='A') and MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                RCD_QTY = dr.Item("RCD_QTY")
                RCD_VALUE = dr.Item("RCD_VALUE")
                If (RCD_QTY = 0) Then
                    GridView2.Rows(I).Cells(4).Text = "0.000"
                Else
                    GridView2.Rows(I).Cells(4).Text = Format(RCD_QTY, "#.000")
                End If

                dr.Close()
            Else
                GridView2.Rows(I).Cells(4).Text = "0.000"
                GridView2.Rows(I).Cells(5).Text = "0.000"
                dr.Close()
            End If
            conn.Close()

            ''CALCULATING TOTAL RCD VALUATION
            conn.Open()

            mc1.CommandText = "select (case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR_PURCHASE,(case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR_PURCHASE from LEDGER where AC_NO=(select AC_PUR from MATERIAL where MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "') AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                If IsDBNull(dr.Item("AMOUNT_DR_PURCHASE") And dr.Item("AMOUNT_CR_PURCHASE")) Then
                    PUR_VALUE = 0
                Else
                    PUR_VALUE = dr.Item("AMOUNT_DR_PURCHASE") - dr.Item("AMOUNT_CR_PURCHASE")
                End If

                If (PUR_VALUE = 0) Then
                    GridView2.Rows(I).Cells(5).Text = "0.000"
                Else
                    GridView2.Rows(I).Cells(5).Text = Format(PUR_VALUE, "#.000")

                End If


                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If


            If (CDec(GridView2.Rows(I).Cells(2).Text) + CDec(GridView2.Rows(I).Cells(4).Text) > 0) Then
                ISSUE_AVG = Format((CDec(GridView2.Rows(I).Cells(3).Text) + CDec(GridView2.Rows(I).Cells(5).Text)) / (CDec(GridView2.Rows(I).Cells(2).Text) + CDec(GridView2.Rows(I).Cells(4).Text)), "#.000")
            End If


            ''CALCULATING TOTAL ISSUE QTY & ISSUE VALUATION
            conn.Open()
            mc1.CommandText = "SELECT SUM(ISSUE_QTY) AS ISSUE_QTY, SUM(TOTAL_PRICE) AS ISSUE_VALUE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE <= '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND (LINE_TYPE='I' or LINE_TYPE='A') and MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                ISSUE_QTY = dr.Item("ISSUE_QTY")
                ISSUE_VALUE = dr.Item("ISSUE_VALUE")
                If (ISSUE_QTY = 0) Then
                    GridView2.Rows(I).Cells(6).Text = "0.000"
                Else
                    GridView2.Rows(I).Cells(6).Text = Format(ISSUE_QTY, "#.000")
                End If

                dr.Close()
            Else
                GridView2.Rows(I).Cells(6).Text = "0.000"
                'GridView2.Rows(I).Cells(7).Text = "0.000"
                dr.Close()
            End If
            conn.Close()

            ''Issue value new
            conn.Open()

            mc1.CommandText = "select (case when (SUM(AMOUNT_DR)>SUM(AMOUNT_CR)) THEN SUM(AMOUNT_DR)-SUM(AMOUNT_CR) else '0.00' end) AS AMOUNT_DR_ISSUE,(case when (SUM(AMOUNT_CR)>SUM(AMOUNT_DR)) THEN SUM(AMOUNT_CR)-SUM(AMOUNT_DR) else '0.00' end) AS AMOUNT_CR_ISSUE from LEDGER where AC_NO=(select AC_ISSUE from MATERIAL where MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "') AND EFECTIVE_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and POST_INDICATION <>'STOCK TRANSFOR'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                If IsDBNull(dr.Item("AMOUNT_DR_ISSUE") And dr.Item("AMOUNT_CR_ISSUE")) Then
                    ISSUE_VALUE = 0
                Else
                    ISSUE_VALUE = dr.Item("AMOUNT_CR_ISSUE") - dr.Item("AMOUNT_DR_ISSUE")
                End If

                If (ISSUE_VALUE = 0) Then
                    GridView2.Rows(I).Cells(7).Text = "0.000"
                Else
                    GridView2.Rows(I).Cells(7).Text = Format(ISSUE_VALUE, "#.000")

                End If


                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If

            ''CALCULATING MISC. SALE QTY & MISC. SALE VALUATION
            conn.Open()
            mc1.CommandText = "SELECT SUM(ISSUE_QTY) AS MISC_SALE_QTY, SUM(TOTAL_PRICE) AS MISC_SALE_VALUE FROM MAT_DETAILS WHERE MAT_CODE LIKE '100%' AND FISCAL_YEAR='" & STR1 & "' AND LINE_DATE <= '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND LINE_TYPE='S' and MAT_CODE='" & GridView2.Rows(I).Cells(0).Text & "' GROUP BY MAT_CODE ORDER BY MAT_CODE"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                MISC_SALE_QTY = dr.Item("MISC_SALE_QTY")
                MISC_SALE_VALUE = dr.Item("MISC_SALE_VALUE")
                If (MISC_SALE_QTY = 0) Then
                    GridView2.Rows(I).Cells(8).Text = "0.000"
                Else
                    GridView2.Rows(I).Cells(8).Text = Format(MISC_SALE_QTY, "#.000")
                End If

                If (MISC_SALE_VALUE = 0) Then
                    GridView2.Rows(I).Cells(9).Text = "0.000"
                Else
                    GridView2.Rows(I).Cells(9).Text = Format(MISC_SALE_VALUE, "#.000")
                    'GridView2.Rows(I).Cells(9).Text = Format(MISC_SALE_QTY * MAT_AVG, "#.000")
                End If

                dr.Close()
            Else
                GridView2.Rows(I).Cells(8).Text = "0.000"
                GridView2.Rows(I).Cells(9).Text = "0.000"
                dr.Close()
            End If
            conn.Close()

            ''CALCULATING TOTAL CLOSING QTY & CLOSING VALUATION

            GridView2.Rows(I).Cells(10).Text = CDec(GridView2.Rows(I).Cells(2).Text) + CDec(GridView2.Rows(I).Cells(4).Text) - CDec(GridView2.Rows(I).Cells(6).Text) - CDec(GridView2.Rows(I).Cells(8).Text)
            If (CDec(GridView2.Rows(I).Cells(10).Text)) = 0 Then
                GridView2.Rows(I).Cells(11).Text = "0.000"
            Else
                GridView2.Rows(I).Cells(11).Text = CDec(GridView2.Rows(I).Cells(3).Text) + CDec(GridView2.Rows(I).Cells(5).Text) - CDec(GridView2.Rows(I).Cells(7).Text) - CDec(GridView2.Rows(I).Cells(9).Text)
            End If

        Next
        '''''''''''''''''''''''''''
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        Dim MAT_CODE, OPEN_QTY, OPEN_VALUE, RCD_QTY, RCD_VALUE, ISSUE_QTY, ISSUE_VALUE, MISC_SALE_QTY, MISC_SALE_VALUE, MAT_AVG_PRICE As New Decimal(0)
        Dim I As Integer = 0
        For I = 0 To GridView2.Rows.Count - 1

            MAT_CODE = GridView2.Rows(I).Cells(0).Text
            OPEN_QTY = CDec(GridView2.Rows(I).Cells(2).Text)
            OPEN_VALUE = CDec(GridView2.Rows(I).Cells(3).Text)
            RCD_QTY = CDec(GridView2.Rows(I).Cells(4).Text)
            RCD_VALUE = CDec(GridView2.Rows(I).Cells(5).Text)
            ISSUE_QTY = CDec(GridView2.Rows(I).Cells(6).Text)
            ISSUE_VALUE = CDec(GridView2.Rows(I).Cells(7).Text)
            MISC_SALE_QTY = CDec(GridView2.Rows(I).Cells(8).Text)
            MISC_SALE_VALUE = CDec(GridView2.Rows(I).Cells(9).Text)

            If ((OPEN_QTY + RCD_QTY) > 0) Then
                If ((OPEN_QTY + RCD_QTY - ISSUE_QTY) = 0) Then
                    'Ideal issue Avg price
                    MAT_AVG_PRICE = Format((OPEN_VALUE + RCD_VALUE) / (OPEN_QTY + RCD_QTY), "#.00")
                    ''UPDATING MATERIAL AVERAGE PRICE
                    conn.Open()
                    mycommand = New SqlCommand("UPDATE MATERIAL SET MAT_AVG='" & MAT_AVG_PRICE & "' WHERE MAT_CODE='" & MAT_CODE & "'", conn)
                    mycommand.ExecuteNonQuery()
                    conn.Close()
                ElseIf ((OPEN_QTY + RCD_QTY - ISSUE_QTY - MISC_SALE_QTY) > 0) Then
                    'Ideal issue Avg price
                    MAT_AVG_PRICE = Format((OPEN_VALUE + RCD_VALUE - ISSUE_VALUE - MISC_SALE_VALUE) / (OPEN_QTY + RCD_QTY - ISSUE_QTY - MISC_SALE_QTY), "#.00")
                    ''UPDATING MATERIAL AVERAGE PRICE
                    conn.Open()
                    mycommand = New SqlCommand("UPDATE MATERIAL SET MAT_AVG='" & MAT_AVG_PRICE & "' WHERE MAT_CODE='" & MAT_CODE & "'", conn)
                    mycommand.ExecuteNonQuery()
                    conn.Close()
                End If

            End If

        Next
    End Sub


    Protected Sub save_ledger_Issue_Adj(so_no As String, garn_mb As String, inv_no As String, garn_date As String, effective_date As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, token_no As String, line_no As Integer, PAY_IND As String)

        Dim working_date As Date

        working_date = CDate(garn_date)
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

End Class
