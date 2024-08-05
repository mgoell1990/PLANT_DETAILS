Imports System.Globalization
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Net
Imports System.Net.Mail
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp
Imports iTextSharp.text.pdf.parser
Imports Org.BouncyCastle.Utilities.Encoders
Imports System.IO
Imports CrystalDecisions.Shared

Public Class WebForm4
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
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
        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            Return
        ElseIf DropDownList10.SelectedValue = "Select" Then
            DropDownList10.Focus()
            Return
        End If

        Dim crystalReport As New ReportDocument
        Dim dt2 As New DataTable
        Dim PO_QUARY As String = "Select *,'" & DropDownList9.SelectedValue & "' AS MONTH,'" & DropDownList10.SelectedValue & "' AS YEAR,(QTR+' '+STREET+' '+SECTOR) AS ADDRESS from emp_master where STATUS='WORKING' ORDER BY BILL_NO,DEPARTMENT"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt2)
        crystalReport.Load(Server.MapPath("~/print_rpt/emp_payslip.rpt"))
        crystalReport.SetDataSource(dt2)

        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
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


        ''==================For downloading individual payslips==================''



        'If DropDownList9.SelectedValue = "Select" Then
        '    DropDownList9.Focus()
        '    Return
        'ElseIf DropDownList10.SelectedValue = "Select" Then
        '    DropDownList10.Focus()
        '    Return
        'End If


        'conn.Open()
        'Dim mc1 As New SqlCommand
        'Dim dr1 As SqlDataReader
        'mc1.CommandText = "Select PERNO from emp_master where STATUS='WORKING'"
        'mc1.Connection = conn
        'dr1 = mc1.ExecuteReader
        'If dr1.HasRows = True Then
        '    'dr1.Read()
        '    While dr1.Read()
        '        If IsDBNull(dr1("PERNO")) Then
        '            'sum_sesbf_amt = dr1("PERNO")
        '        Else
        '            'dr.Close()
        '            Dim pers_no As New String("")
        '            pers_no = dr1("PERNO")
        '            Dim crystalReport As New ReportDocument
        '            Dim dt2 As New DataTable
        '            Dim PO_QUARY As String = "Select *,'" & DropDownList9.SelectedValue & "' AS MONTH,'" & DropDownList10.SelectedValue & "' AS YEAR,(QTR+' '+STREET+' '+SECTOR) AS ADdr1ESS from emp_master where PERNO='" & pers_no & "' ORDER BY BILL_NO,DEPARTMENT"
        '            Dim da1 As New SqlDataAdapter(PO_QUARY, conn)
        '            'da = New SqlDataAdapter(PO_QUARY, conn)
        '            da1.Fill(dt2)
        '            crystalReport.Load(Server.MapPath("~/print_rpt/emp_payslip.rpt"))
        '            crystalReport.SetDataSource(dt2)


        '            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "C:\Pay\" + dr1("PERNO") + "_payslip.pdf")

        '            crystalReport.Close()
        '            crystalReport.Dispose()

        '        End If

        '    End While
        '    dr1.Close()
        'Else
        '    dr1.Close()
        'End If
        'conn.Close()


        ''====================================================================''

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If DropDownList13.SelectedValue = "Select" Then
            DropDownList13.Focus()
            Return
        ElseIf DropDownList14.SelectedValue = "Select" Then
            DropDownList14.Focus()
            Return
        End If

        '''''''''''''''''''''''''''''''''''''''''
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range
        Dim rCnt As Integer
        Dim cCnt As Integer
        Dim Obj1, Obj2, Obj3 As Object
        Dim Month, Year, value, header, PERSNO As New String("")
        Month = DropDownList13.SelectedValue
        Year = DropDownList14.SelectedValue
        xlApp = New Excel.Application
        'xlWorkBook = xlApp.Workbooks.Open("C:\Users\Administrator\Desktop\srupaybill_201807.xls")
        xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(FileUpload1.FileName))
        xlWorkSheet = xlWorkBook.Worksheets(1)

        range = xlWorkSheet.UsedRange

        For rCnt = 2 To range.Rows.Count
            For cCnt = 2 To range.Columns.Count
                Obj1 = CType(range.Cells(1, cCnt), Excel.Range)
                header = Obj1.value
                Obj2 = CType(range.Cells(rCnt, 3), Excel.Range)
                PERSNO = Obj2.value
                Obj3 = CType(range.Cells(rCnt, cCnt), Excel.Range)
                value = Obj3.value
                'If (header = "ITAX") Then
                '    checkColumnHeader_payment(header, PERSNO, CInt(CType(range.Cells(rCnt, cCnt), Excel.Range).Value) + CInt(CType(range.Cells(rCnt, cCnt + 1), Excel.Range).Value), Month, Year)
                'Else
                '    checkColumnHeader_payment(header, PERSNO, value, Month, Year)
                'End If

                checkColumnHeader_payment(header, PERSNO, value, Month, Year)

                If (header = "SESBF") Then
                    Dim interest, pr_ind, month_difference As New Integer()
                    Dim interest_rate As New Decimal()
                    Dim dateOfSuperannuation As New Date()
                    interest = 0
                    pr_ind = 0
                    interest_rate = 0

                    ''Getting interest rate of SESBF
                    conn.Open()
                    Dim mc1 As New SqlCommand
                    mc1.CommandText = "select interest_rate from SESBF_INTEREST"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows = True Then
                        'dr.Read()
                        While dr.Read()
                            If Not IsDBNull(dr("interest_rate")) Then

                                interest_rate = CDec(dr("interest_rate"))

                            End If
                        End While

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()

                    ''Getting Date of Superannuation
                    conn.Open()
                    mc1.CommandText = "select DOS from emp_master where PERNO='" & PERSNO & "'"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows = True Then
                        'dr.Read()
                        While dr.Read()
                            If Not IsDBNull(dr("DOS")) Then

                                dateOfSuperannuation = dr("DOS")

                            End If
                        End While

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                    If Month = "APRIL" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-04-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 10) / 12)
                        End If

                        pr_ind = 2
                        emp_sesbf_deduction("MAY", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "MAY" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-05-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 9) / 12)
                        End If

                        pr_ind = 3
                        emp_sesbf_deduction("JUNE", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "JUNE" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-06-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((value * CDec(interest_rate) / 100) * 8) / 12)
                        End If

                        pr_ind = 4
                        emp_sesbf_deduction("JULY", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "JULY" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-07-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 7) / 12)
                        End If

                        pr_ind = 5
                        emp_sesbf_deduction("AUGUST", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "AUGUST" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-08-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 6) / 12)
                        End If

                        pr_ind = 6
                        emp_sesbf_deduction("SEPTEMBER", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "SEPTEMBER" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-09-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 5) / 12)
                        End If

                        pr_ind = 7
                        emp_sesbf_deduction("OCTOBER", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "OCTOBER" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-10-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 4) / 12)
                        End If

                        pr_ind = 8
                        emp_sesbf_deduction("NOVEMBER", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "NOVEMBER" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-11-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 3) / 12)
                        End If

                        pr_ind = 9
                        emp_sesbf_deduction("DECEMBER", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "DECEMBER" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-12-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 2) / 12)
                        End If

                        pr_ind = 10
                        emp_sesbf_deduction("JANUARY", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "JANUARY" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-01-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 1) / 12)
                        End If

                        pr_ind = 11
                        emp_sesbf_deduction("FEBRUARY", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "FEBRUARY" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-02-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 0) / 12)
                        End If

                        pr_ind = 12
                        emp_sesbf_deduction("MARCH", Year, PERSNO, value, interest, pr_ind)

                    ElseIf Month = "MARCH" Then
                        month_difference = DateDiff(DateInterval.Month, CDate(Year & "-03-01"), dateOfSuperannuation)
                        If (month_difference < 12) Then
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * month_difference) / 12)
                        Else
                            interest = CInt(((CInt(value) * CDec(interest_rate) / 100) * 11) / 12)
                        End If

                        pr_ind = 1
                        emp_sesbf_deduction("APRIL", Year, PERSNO, value, interest, pr_ind)
                    End If


                End If

            Next
        Next

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
        '''''''''''''''''''''''''''''''''''''''''
    End Sub

    Protected Sub emp_sesbf_deduction(month As String, year As String, pers_no As String, amount As String, interest As Integer, pr_ind As Integer)

        If CInt(amount) <> 0 Then
            ''Getting Fiscal Year
            Dim fiscal_year As New String("")
            If (month = "JANUARY" Or month = "FEBRUARY" Or month = "MARCH") Then
                fiscal_year = year.Trim.Substring(2)
                fiscal_year = (fiscal_year - 1) & fiscal_year
            Else
                fiscal_year = year.Trim.Substring(2)
                fiscal_year = fiscal_year & (fiscal_year + 1)
            End If

            ''Getting sum of priciple and interest upto the month
            Dim sum_sesbf_amt, sum_sesbf_interest As New Integer()
            sum_sesbf_amt = 0
            sum_sesbf_interest = 0
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select sum(SESBF_AMOUNT) As sum_sesbf_amt,sum(INTEREST) As sum_sesbf_int_amt from SESBF where PERS_NO='" & pers_no & "' and FY='" & fiscal_year & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                'dr.Read()
                While dr.Read()
                    If Not IsDBNull(dr("sum_sesbf_amt")) Then
                        sum_sesbf_amt = dr("sum_sesbf_amt")
                    End If

                    If Not IsDBNull(dr("sum_sesbf_int_amt")) Then
                        sum_sesbf_interest = dr("sum_sesbf_int_amt")
                    End If
                End While

                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()


            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into SESBF(PERS_NO,MONTH,YEAR,SESBF_AMOUNT,PLBS,INTEREST,AMT_CLOSING_BAL,INT_CLOSING_BAL,PR_IND,FY) VALUES(@PERS_NO,@MONTH,@YEAR,@SESBF_AMOUNT,@PLBS,@INTEREST,@AMT_CLOSING_BAL,@INT_CLOSING_BAL,@PR_IND,@FY)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PERS_NO", pers_no)
            cmd.Parameters.AddWithValue("@MONTH", month)
            cmd.Parameters.AddWithValue("@YEAR", year)
            cmd.Parameters.AddWithValue("@SESBF_AMOUNT", amount)
            cmd.Parameters.AddWithValue("@PLBS", 0.00)
            cmd.Parameters.AddWithValue("@INTEREST", interest)
            cmd.Parameters.AddWithValue("@AMT_CLOSING_BAL", sum_sesbf_amt + CInt(amount))
            cmd.Parameters.AddWithValue("@INT_CLOSING_BAL", sum_sesbf_interest + CInt(interest))
            cmd.Parameters.AddWithValue("@PR_IND", pr_ind)
            cmd.Parameters.AddWithValue("@FY", fiscal_year)
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
            ''
        End If
    End Sub

    Private Sub checkColumnHeader_payment(header As String, PERSNO_VALUE As String, value As String, MONTH_VALUE As String, YEAR_VALUE As String)
        Select Case header
            Case "BASIC+SI"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "DUTY PAY", value, 1)
            Case "PERS_PAY"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "PP", value, 2)
            Case "DA"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "DA", value, 3)
            Case "ACT_PAY"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "ACTING PAYMENT", value, 34)
            Case "HRA"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "HRA", value, 4)
            Case "FUEL_SUB"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "FUEL SUB", value, 9)
            Case "CANTEEN"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CANTEEN ALL", value, 10)
            Case "NIGHT_ALL"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "NIGHT ALL", value, 11)
            Case "CPF_PC"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CPF", value, 1)
            Case "VPF"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "VPF", value, 3)
            Case "SESBF"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "SESBF", value, 2)
            Case "COOP"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "COOP", value, 11)
            Case "CLUB"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CLUB", value, 20)
            Case "HRENT"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "HRENT", value, 5)
            Case "ELEC"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "ELEC", value, 6)
            Case "CONS"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CONS", value, 7)
            Case "CPF_LN_PR"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CPF LOAN PRIN", value, 8)
            Case "CPF_LN_INT"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CPF LOAN INT", value, 38)
            Case "FEST_REC"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "FEST REC", value, 20)
            Case "CAF_TAX"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "PERKS TAXABLE", value, 18)
            Case "CAF_NTAX"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "PERKS NONTAXABLE", value * (-1), 29)
            Case "ITAX"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "ITAX", value, 4)
            Case "SURCH"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "SURCHARGE", value, 4)
            Case "CESS"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CESS", value, 5)
            Case "CESS_ADDL"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "CESS_ADDL", value, 36)
            Case "VCHR AMT"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "MEAL VOUCHER", value * (-1), 21)












            Case "ATS"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "NS ALL", value, 8)
            Case "PTAX"
                emp_deduction(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "PTAX", value, 37)
            Case "INCENTIVE"
                emp_payment(MONTH_VALUE, YEAR_VALUE, PERSNO_VALUE, "INCENTIVE", value, 35)


            Case Else

        End Select
    End Sub

    Private Sub checkColumnHeader_recoveries(header As String, PERSNO As String, value As String, Month As String, Year As String)
        Select Case header
            Case "ADJUSTABLE ADVANCE"
                emp_payment(Month, Year, PERSNO, "ADJUSTABLE ADVANCE", value, 15)
            Case "C C A"
                emp_payment(Month, Year, PERSNO, "C C A", value, 7)
            Case "COST OF MOBILE"
                emp_deduction(Month, Year, PERSNO, "MOBILE COST", value * (-1), 32)
            Case "DA ADJUSTMENT"
                emp_payment(Month, Year, PERSNO, "DA ADJMT", value, 6)
            Case "DUST ALLOWANCE"
                emp_payment(Month, Year, PERSNO, "DUST ALLOWANCE", value, 15)
            Case "ELECTRICITY CHARGES"
                emp_deduction(Month, Year, PERSNO, "ELECTRICITY CHARGES", value * (-1), 13)
            Case "EMPLOYEE EXPENSES"
                emp_deduction(Month, Year, PERSNO, "EMPLOYEE EXPENSES", value * (-1), 17)
            Case "EXTRA WAGES"
                emp_payment(Month, Year, PERSNO, "EXTRA WAGES", value, 15)
            Case "FESTIVAL ADVANCE SANCTION"
                emp_deduction(Month, Year, PERSNO, "FESTIVAL ADV", value * (-1), 21)
            Case "FIXED QUARTER CHARGES"
                emp_deduction(Month, Year, PERSNO, "FIXED QUARTER CHARGES", value * (-1), 10)
            Case "GSLI"
                emp_deduction(Month, Year, PERSNO, "GSLI", value * (-1), 10)
            Case "HOUSE RENT ARREAR"
                emp_deduction(Month, Year, PERSNO, "HOUSE RENT ARREAR", value * (-1), 10)
            Case "INCIDENTAL EXPENSES(NIGHT SHIFT)"
                emp_deduction(Month, Year, PERSNO, "INCIDENTAL EXPENSES", value * (-1), 10)
            Case "L T E"
                emp_deduction(Month, Year, PERSNO, "LTE NONTAXABLE", value * (-1), 14)
            Case "LTE TAXABLE"
                emp_payment(Month, Year, PERSNO, "LTE TAXABLE", value, 16)
            Case "N P A"
                emp_payment(Month, Year, PERSNO, "NPA", value, 14)
            Case "O A"
                emp_deduction(Month, Year, PERSNO, "OA", value * (-1), 28)
            Case "OTHER ALLOWANCE"
                emp_payment(Month, Year, PERSNO, "OTHER ALLOWANCE", value, 20)
            Case "PAY ADJUSTMENT"
                emp_payment(Month, Year, PERSNO, "PAY ADJUSTMENT", value, 5)
            Case "PM CARES FUND"
                emp_deduction(Month, Year, PERSNO, "PM CARES FUND", value * (-1), 15)
            Case "INT REC AUTOLN"
                emp_deduction(Month, Year, PERSNO, "CAR LOAN INT", value * (-1), 18)
            Case "PRIN REC AUTOLN"
                emp_deduction(Month, Year, PERSNO, "CAR LOAN PRIN", value * (-1), 19)
            Case "SPL ALL ADJ"
                emp_payment(Month, Year, PERSNO, "SPL ALL ADJ", value, 11)
            Case "TRANSPORT EXPENSES(CYCLE ALLOWANCE)"
                emp_payment(Month, Year, PERSNO, "TRANS EXP", value, 31)
            Case "WASHING / MESSING ALLOWANCE"
                emp_payment(Month, Year, PERSNO, "WASH/MESS ALL", value, 11)
            Case "WATER CHARGES"
                emp_deduction(Month, Year, PERSNO, "WATER CHARGES", value * (-1), 12)
            Case "HOUSE MAINTENANCE"
                emp_deduction(Month, Year, PERSNO, "HOUSE MAINTENANCE", value * (-1), 27)
            Case "LOCAL CONVEYANCE"
                emp_deduction(Month, Year, PERSNO, "LOCAL CONVEYANCE", value * (-1), 27)






            Case "SAL ADV."
                emp_deduction(Month, Year, PERSNO, "SAL ADV.", value * (-1), 15)
            Case "HOUSE RENT ADJ"
                emp_deduction(Month, Year, PERSNO, "HOUSE RENT ADJ", value * (-1), 9)



            Case "MED REIMB"
                emp_deduction(Month, Year, PERSNO, "MED REIMB", value * (-1), 16)





            Case "ATS PAY"
                emp_payment(Month, Year, PERSNO, "ATS PAY", value, 22)
            Case "LTC ENCASH"
                emp_payment(Month, Year, PERSNO, "LTC ENCASH", value, 23)
            Case "LTC/LLTC ENCASH"
                emp_payment(Month, Year, PERSNO, "LTC/LLTC ENCASH", value, 23)

            Case "BASIC ADJ"
                emp_payment(Month, Year, PERSNO, "BASIC ADJ", value, 12)
            Case "CM RELIEF FUND"
                emp_deduction(Month, Year, PERSNO, "CM RELIEF FUND", value * (-1), 34)
            Case "DA ADJ"
                emp_payment(Month, Year, PERSNO, "DA ADJ", value, 14)


            Case "GUEST HOUSE CHARGES"
                emp_deduction(Month, Year, PERSNO, "GUEST HOUSE CHARGES", value * (-1), 22)
            Case "GRP INS"
                emp_deduction(Month, Year, PERSNO, "GRP INS", value * (-1), 23)
            Case "HINDI AWD"
                emp_deduction(Month, Year, PERSNO, "HINDI AWD", value * (-1), 24)
            Case "HBL PRIN"
                emp_deduction(Month, Year, PERSNO, "HBL PRIN", value * (-1), 25)
            Case "HBL INT"
                emp_deduction(Month, Year, PERSNO, "HBL INT", value * (-1), 26)


            Case "MOBILE BILL"
                emp_deduction(Month, Year, PERSNO, "MOBILE BILL", value * (-1), 34)



            Case "NON-EX PERKS"
                emp_payment(Month, Year, PERSNO, "NON-EX PERKS", value, 17)
            Case "PERKS ADJ"
                emp_payment(Month, Year, PERSNO, "PERKS ADJ", value, 19)
            Case "OUTSTN TOUR ADJ"
                emp_deduction(Month, Year, PERSNO, "OUTSTN TOUR ADJ", value * (-1), 30)

            Case "UNIF.ALLOWANCE"
                emp_deduction(Month, Year, PERSNO, "UNIF.ALLOWANCE", value * (-1), 31)

            Case "NX PERKS ADJ"
                emp_payment(Month, Year, PERSNO, "NX PERKS ADJ", value, 13)
            Case "FAMLY WELFARE"
                emp_deduction(Month, Year, PERSNO, "FAMLY WELFARE", value * (-1), 33)
            Case "CANTEEN ALLOWANCE"
                emp_payment(Month, Year, PERSNO, "CANTEEN ALLOWANCE", value, 16)

            Case "UNION MEMBERSHIP"
                emp_deduction(Month, Year, PERSNO, "UNION MEMBERSHIP", value * (-1), 34)
            Case "INC ADJ"
                emp_payment(Month, Year, PERSNO, "INC ADJ", value, 17)

            Case Else

        End Select
    End Sub


    Protected Sub emp_payment(month As String, year As String, pers_no As String, payment_type As String, amount As String, pr_ind As Integer)

        If CInt(amount) <> 0 Then
            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into emp_payment(MONTH,YEAR,PERS_NO,PAYMENT_TYPE,AMOUNT,PR_IND) VALUES(@MONTH,@YEAR,@PERS_NO,@PAYMENT_TYPE,@AMOUNT,@PR_IND)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@MONTH", month)
            cmd.Parameters.AddWithValue("@YEAR", year)
            cmd.Parameters.AddWithValue("@PERS_NO", pers_no)
            cmd.Parameters.AddWithValue("@PAYMENT_TYPE", payment_type)
            cmd.Parameters.AddWithValue("@AMOUNT", amount)
            cmd.Parameters.AddWithValue("@PR_IND", pr_ind)
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
            ''
        End If
    End Sub

    Protected Sub emp_deduction(month As String, year As String, pers_no As String, payment_type As String, amount As String, pr_ind As Integer)

        If CInt(amount) <> 0 Then

            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into emp_deductions(MONTH,YEAR,PERS_NO,DEDUCTION_TYPE,AMOUNT,PR_IND) VALUES(@MONTH,@YEAR,@PERS_NO,@DEDUCTION_TYPE,@AMOUNT,@PR_IND)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@MONTH", month)
            cmd.Parameters.AddWithValue("@YEAR", year)
            cmd.Parameters.AddWithValue("@PERS_NO", pers_no)
            cmd.Parameters.AddWithValue("@DEDUCTION_TYPE", payment_type)
            cmd.Parameters.AddWithValue("@AMOUNT", amount)
            cmd.Parameters.AddWithValue("@PR_IND", pr_ind)
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
            ''
        End If
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If DropDownList11.SelectedValue = "Select" Then
            DropDownList11.Focus()
            Return
        ElseIf DropDownList12.SelectedValue = "Select" Then
            DropDownList12.Focus()
            Return
        End If

        '''''''''''''''''''''''''''''''''''''''''
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range
        Dim rCnt As Integer
        Dim Obj1, Obj2, Obj3 As Object
        Dim Month, Year, value, header, PERSNO As New String("")
        Month = DropDownList11.SelectedValue
        Year = DropDownList12.SelectedValue
        xlApp = New Excel.Application
        'xlWorkBook = xlApp.Workbooks.Open("C:\Users\Administrator\Desktop\srupayrec_cds_201807.xls")
        xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(FileUpload2.FileName))
        xlWorkSheet = xlWorkBook.Worksheets(1)

        range = xlWorkSheet.UsedRange

        For rCnt = 2 To range.Rows.Count
            'For cCnt = 2 To range.Columns.Count
            '    Obj1 = CType(range.Cells(1, cCnt), Excel.Range)
            '    header = Obj1.value
            '    Obj2 = CType(range.Cells(rCnt, 1), Excel.Range)
            '    PERSNO = Obj2.value
            '    Obj3 = CType(range.Cells(rCnt, cCnt), Excel.Range)
            '    value = Obj3.value

            '    checkColumnHeader_recoveries(header, PERSNO, value, Month, Year)
            'Next

            Obj1 = CType(range.Cells(rCnt, 6), Excel.Range)
            header = Obj1.value
            Obj2 = CType(range.Cells(rCnt, 3), Excel.Range)
            PERSNO = Obj2.value
            Obj3 = CType(range.Cells(rCnt, 7), Excel.Range)
            value = Obj3.value

            checkColumnHeader_recoveries(header, PERSNO, value, Month, Year)
        Next

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

        '''''''''''''''''''''''''''''''''''''''''
    End Sub




    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If DropDownList22.SelectedValue = "PAYSLIP" Then
            If DropDownList15.SelectedValue = "Select" Then
                DropDownList15.Focus()
                Return
            ElseIf DropDownList18.SelectedValue = "Select" Then
                DropDownList18.Focus()
                Return
            End If
        ElseIf DropDownList22.SelectedValue = "SESBF" Then
            If DropDownList24.SelectedValue = "Select" Then
                DropDownList24.Focus()
                Return

            End If
        End If


        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select PERNO from emp_master order by PERNO"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            'dr.Read()
            While dr.Read()
                If Not IsDBNull(dr("PERNO")) Then

                    If DropDownList22.SelectedValue = "PAYSLIP" Then
                        downloadPayslip(dr("PERNO"), DropDownList15.SelectedValue, DropDownList18.SelectedValue)
                    ElseIf DropDownList22.SelectedValue = "SESBF" Then
                        downloadSESBFslip(dr("PERNO"), DropDownList24.SelectedValue)
                    ElseIf DropDownList22.SelectedValue = "PF SLIP" Then
                        SplipPFSlip(dr("PERNO"), DropDownList24.SelectedValue)
                    End If

                End If
            End While

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Private Sub downloadPayslip(pno As String, month As String, year As String)
        Dim outputStream As Stream = Nothing
        Dim reader As PdfReader = New PdfReader("D:/Payslips/" & year & "/" & month & "/" & month & "_payslip.pdf")
        If (PdfReader.unethicalreading) Then

        End If

        Dim stamper As PdfStamper = Nothing
        Dim parser As PdfReaderContentParser = New PdfReaderContentParser(reader)

        Dim strategy As New SimpleTextExtractionStrategy
        Dim text As New String("")
        Dim sb As New StringBuilder()
        Dim currentPage As New Integer
        For currentPage = 1 To reader.NumberOfPages()
            strategy = parser.ProcessContent(currentPage, strategy)

            If (strategy.GetResultantText().Contains(pno)) Then

                Dim doc As Document = Nothing
                Dim doc_new As Document = Nothing
                Dim pdfCpy As PdfCopy = Nothing
                Dim page As PdfImportedPage = Nothing
                Try

                    doc = New Document(reader.GetPageSizeWithRotation(1))
                    pdfCpy = New PdfCopy(doc, New IO.FileStream("D:/Payslips/" + year + "/" + month + "/" + month + "_" + year + "_" + pno + ".pdf", IO.FileMode.Create))
                    doc.Open()
                    page = pdfCpy.GetImportedPage(reader, currentPage)
                    pdfCpy.AddPage(page)

                    doc.Close()
                    reader.Close()
                Catch ex As Exception
                    Throw ex
                End Try

                Exit For

            End If
        Next

        ''''''''''''''''''''''''''

    End Sub


    Private Sub downloadSESBFslip(pno As String, fiscal_year As String)
        Dim outputStream As Stream = Nothing
        Dim reader As PdfReader = New PdfReader("D:/SESBF/" + fiscal_year + "/SESBF_" + fiscal_year + ".pdf")
        If (PdfReader.unethicalreading) Then

        End If

        Dim stamper As PdfStamper = Nothing
        Dim parser As PdfReaderContentParser = New PdfReaderContentParser(reader)

        Dim strategy As New SimpleTextExtractionStrategy
        Dim text As New String("")
        Dim sb As New StringBuilder()
        Dim currentPage As New Integer
        For currentPage = 1 To reader.NumberOfPages()
            strategy = parser.ProcessContent(currentPage, strategy)

            If (strategy.GetResultantText().Contains(pno)) Then

                Dim doc As Document = Nothing
                Dim doc_new As Document = Nothing
                Dim pdfCpy As PdfCopy = Nothing
                Dim page As PdfImportedPage = Nothing
                Try

                    doc = New Document(reader.GetPageSizeWithRotation(1))
                    pdfCpy = New PdfCopy(doc, New IO.FileStream("D:/SESBF/" + fiscal_year + "/SESBFSLIP_" + pno + "_" + fiscal_year + ".pdf", IO.FileMode.Create))
                    doc.Open()
                    page = pdfCpy.GetImportedPage(reader, currentPage)
                    pdfCpy.AddPage(page)

                    doc.Close()
                    reader.Close()
                Catch ex As Exception
                    Throw ex
                End Try

                Exit For

            End If
        Next

    End Sub

    Private Sub SplipPFSlip(pno As String, fiscal_year As String)
        Dim outputStream As Stream = Nothing
        Dim reader As PdfReader = New PdfReader("D:/PFSlip/" + fiscal_year + "/PF_SLIP_" + fiscal_year + ".pdf")
        If (PdfReader.unethicalreading) Then

        End If

        Dim stamper As PdfStamper = Nothing
        Dim parser As PdfReaderContentParser = New PdfReaderContentParser(reader)

        Dim strategy As New SimpleTextExtractionStrategy
        Dim text As New String("")
        Dim sb As New StringBuilder()
        Dim currentPage As New Integer
        For currentPage = 1 To reader.NumberOfPages()
            strategy = parser.ProcessContent(currentPage, strategy)

            If (strategy.GetResultantText().Contains(Right(pno, 5))) Then

                Dim doc As Document = Nothing
                Dim doc_new As Document = Nothing
                Dim pdfCpy As PdfCopy = Nothing
                Dim page As PdfImportedPage = Nothing
                Try

                    doc = New Document(reader.GetPageSizeWithRotation(1))
                    pdfCpy = New PdfCopy(doc, New IO.FileStream("D:/PFSlip/" + fiscal_year + "/PF_SLIP_" + pno + "_" + fiscal_year + ".pdf", IO.FileMode.Create))
                    doc.Open()
                    page = pdfCpy.GetImportedPage(reader, currentPage)
                    pdfCpy.AddPage(page)

                    doc.Close()
                    reader.Close()
                Catch ex As Exception
                    Throw ex
                End Try

                Exit For

            End If
        Next

        ''''''''''''''''''''''''''

    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click


        If (DropDownList23.SelectedValue = "PAYSLIP") Then
            If DropDownList16.SelectedValue = "Select" Then
                DropDownList16.Focus()
                Return
            ElseIf DropDownList17.SelectedValue = "Select" Then
                DropDownList17.Focus()
                Return
            End If
        Else
            If DropDownList27.SelectedValue = "Select" Then
                DropDownList27.Focus()
                Return
            End If
        End If


        conn.Open()
        Dim mc1 As New SqlCommand

        If (DropDownList23.SelectedValue = "PAYSLIP" Or DropDownList23.SelectedValue = "SESBF") Then
            mc1.CommandText = "select PERNO, EMAIL from emp_master where status='working' ORDER BY PERNO"
        ElseIf DropDownList23.SelectedValue = "FORM 16" Then
            mc1.CommandText = "select PAN, EMAIL from emp_master where status='working' ORDER BY PERNO"
        ElseIf (DropDownList23.SelectedValue = "PF SLIP") Then
            mc1.CommandText = "select PERNO, EMAIL from emp_master where status='working' ORDER BY PERNO"
        End If

        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            'dr.Read()
            While dr.Read()
                If Not IsDBNull(dr("EMAIL")) Then

                    If DropDownList23.SelectedValue = "PAYSLIP" Then
                        SendPAYSLIPEmail(dr("PERNO"), DropDownList16.SelectedValue, DropDownList17.SelectedValue, dr("EMAIL"))
                    ElseIf DropDownList23.SelectedValue = "SESBF" Then
                        SendSESBFPDFEmail(dr("PERNO"), DropDownList27.SelectedValue, dr("EMAIL"))
                    ElseIf DropDownList23.SelectedValue = "FORM 16" Then
                        If Not IsDBNull(dr("PAN")) Then
                            SendForm16PDFEmail(dr("PAN"), DropDownList27.SelectedValue, dr("EMAIL"))
                        End If

                    ElseIf DropDownList23.SelectedValue = "PF SLIP" Then
                        SendPFSlipEmail(dr("PERNO"), DropDownList27.SelectedValue, dr("EMAIL"))
                    End If

                End If
            End While

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Private Sub SendPAYSLIPEmail(pno As String, month As String, year As String, email_id As String)
        Using sw As New StringWriter()
            Using hw As New HtmlTextWriter(sw)
                Dim companyName As String = "ASPSnippets"
                Dim sb As New StringBuilder()
                Using memoryStream As New MemoryStream()

                    Dim bytes As Byte() = memoryStream.ToArray()
                    memoryStream.Close()
                    Dim mm As New MailMessage("srubhilaiedp@gmail.com", email_id)
                    mm.Subject = "Payslip for the month of " + month + " " + year

                    sb.AppendLine("Hi<br>")
                    sb.AppendLine("Please find your payslip for the month of " + month + " " + year + ".<br>")
                    sb.AppendLine("Request you all to please check the final payslip amount with the amount being credited in your account. If there is any discrepancy, please let us know.<br>")
                    sb.AppendLine("This is a system generated email, please do not revert.<br>")
                    sb.AppendLine("<br><br>")

                    sb.AppendLine("Regards,<br><br>")
                    sb.AppendLine("Mayank Kumar Goyal<br>")
                    sb.AppendLine("Deputy Manager(Systems)<br>")
                    sb.AppendLine("SRU Bhilai<br>")

                    mm.Body = sb.ToString()

                    mm.Attachments.Add(New Attachment("D:/Payslips/" + year + "/" + month + "/" + month + "_" + year + "_" + pno + ".pdf"))
                    mm.IsBodyHtml = True
                    Dim smtp As New SmtpClient()
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential()
                    NetworkCred.UserName = "srubhilaiedp@gmail.com"
                    NetworkCred.Password = "aomhrucarouzssnw"
                    smtp.UseDefaultCredentials = True
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    smtp.Send(mm)
                End Using
            End Using
        End Using
    End Sub

    Private Sub SendForm16PDFEmail(pan As String, fiscal_year As String, email_id As String)
        Using sw As New StringWriter()
            Using hw As New HtmlTextWriter(sw)
                Dim companyName As String = "ASPSnippets"
                Dim sb As New StringBuilder()
                Using memoryStream As New MemoryStream()

                    Dim bytes As Byte() = memoryStream.ToArray()
                    memoryStream.Close()
                    Dim mm As New MailMessage("srubhilaiedp@gmail.com", email_id)
                    mm.Subject = "Form 16 for the FY " + fiscal_year

                    sb.AppendLine("Dear Sir/Madam,<br>")
                    sb.AppendLine("Please find your Form 16 for the FY " + fiscal_year + ".<br>")
                    sb.AppendLine("This is a system generated email, please do not revert.<br>")
                    sb.AppendLine("<br><br>")

                    sb.AppendLine("Regards,<br><br>")
                    sb.AppendLine("Mayank Kumar Goyal<br>")
                    sb.AppendLine("Manager(Systems/F&A)<br>")
                    sb.AppendLine("SRU Bhilai<br>")

                    mm.Body = sb.ToString()

                    mm.Attachments.Add(New Attachment("D:/Form-16/" + fiscal_year + "/PartA/" + pan + "_2024-25.pdf"))
                    mm.Attachments.Add(New Attachment("D:/Form-16/" + fiscal_year + "/PartB/" + pan + "_PARTB_2024-25.pdf"))
                    mm.IsBodyHtml = True
                    Dim smtp As New SmtpClient()
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential()
                    NetworkCred.UserName = "srubhilaiedp@gmail.com"
                    NetworkCred.Password = "aomhrucarouzssnw"
                    smtp.UseDefaultCredentials = True
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    smtp.Send(mm)
                End Using
            End Using
        End Using
    End Sub

    Private Sub SendSESBFPDFEmail(pno As String, fiscal_year As String, email_id As String)
        Using sw As New StringWriter()
            Using hw As New HtmlTextWriter(sw)
                Dim companyName As String = "ASPSnippets"
                Dim sb As New StringBuilder()
                Using memoryStream As New MemoryStream()

                    Dim bytes As Byte() = memoryStream.ToArray()
                    memoryStream.Close()
                    Dim mm As New MailMessage("srubhilaiedp@gmail.com", email_id)
                    mm.Subject = "SESBF statement for the FY " + fiscal_year

                    sb.AppendLine("Hi<br>")
                    sb.AppendLine("Please find your SESBF statement for the FY " + fiscal_year + ".<br>")
                    sb.AppendLine("This is a system generated email, please do not revert.<br>")
                    sb.AppendLine("<br><br>")

                    sb.AppendLine("Regards,<br><br>")
                    sb.AppendLine("Mayank Kumar Goyal<br>")
                    sb.AppendLine("Manager(Systems/F&A)<br>")
                    sb.AppendLine("SRU Bhilai<br>")

                    mm.Body = sb.ToString()

                    mm.Attachments.Add(New Attachment("D:/SESBF/" + fiscal_year + "/SESBFSLIP_" + pno + "_" + fiscal_year + ".pdf"))
                    mm.IsBodyHtml = True
                    Dim smtp As New SmtpClient()
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential()
                    NetworkCred.UserName = "srubhilaiedp@gmail.com"
                    NetworkCred.Password = "aomhrucarouzssnw"
                    smtp.UseDefaultCredentials = True
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    smtp.Send(mm)
                End Using
            End Using
        End Using
    End Sub

    Private Sub SendPFSlipEmail(pno As String, fiscal_year As String, email_id As String)
        Using sw As New StringWriter()
            Using hw As New HtmlTextWriter(sw)
                Dim companyName As String = "ASPSnippets"
                Dim sb As New StringBuilder()
                Using memoryStream As New MemoryStream()

                    If System.IO.File.Exists("D:/PFSlip/" + fiscal_year + "/PF_SLIP_" + pno + "_" + fiscal_year + ".pdf") Then
                        Dim bytes As Byte() = memoryStream.ToArray()
                        memoryStream.Close()
                        Dim mm As New MailMessage("srubhilaiedp@gmail.com", email_id)
                        mm.Subject = "PF statement for the FY " + fiscal_year

                        sb.AppendLine("Respected Sir/Madam,<br>")
                        sb.AppendLine("<br>")
                        sb.AppendLine("Please find your PF statement for the FY " + fiscal_year + ".<br>")
                        sb.AppendLine("This is a system generated email, please do not revert.<br>")
                        sb.AppendLine("<br><br>")

                        sb.AppendLine("Regards,<br><br>")
                        sb.AppendLine("Mayank Kumar Goyal<br>")
                        sb.AppendLine("Manager(Systems/F&A)<br>")
                        sb.AppendLine("SRU Bhilai<br>")

                        mm.Body = sb.ToString()

                        mm.Attachments.Add(New Attachment("D:/PFSlip/" + fiscal_year + "/PF_SLIP_" + pno + "_" + fiscal_year + ".pdf"))
                        mm.IsBodyHtml = True
                        Dim smtp As New SmtpClient()
                        smtp.Host = "smtp.gmail.com"
                        smtp.EnableSsl = True
                        Dim NetworkCred As New NetworkCredential()
                        NetworkCred.UserName = "srubhilaiedp@gmail.com"
                        NetworkCred.Password = "aomhrucarouzssnw"
                        smtp.UseDefaultCredentials = True
                        smtp.Credentials = NetworkCred
                        smtp.Port = 587
                        smtp.Send(mm)

                    End If


                End Using
            End Using
        End Using
    End Sub

    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If DropDownList23.SelectedValue = "PAYSLIP" Then
            If DropDownList16.SelectedValue = "Select" Then
                DropDownList16.Focus()
                Return
            ElseIf DropDownList17.SelectedValue = "Select" Then
                DropDownList17.Focus()
                Return
            ElseIf TextBox1.Text = "" Then
                TextBox1.Focus()
                Return
            End If
        ElseIf (DropDownList23.SelectedValue = "SESBF" Or DropDownList23.SelectedValue = "FORM 16") Then
            If DropDownList27.SelectedValue = "Select" Then
                DropDownList27.Focus()
                Return
            ElseIf TextBox4.Text = "" Then
                TextBox4.Focus()
                Return
            End If
        End If



        conn.Open()
        Dim mc1 As New SqlCommand

        If DropDownList23.SelectedValue = "PAYSLIP" Then
            mc1.CommandText = "select PERNO, EMAIL from emp_master where PERNO='" & TextBox1.Text & "' and status='working' ORDER BY PERNO"
        ElseIf DropDownList23.SelectedValue = "SESBF" Then
            mc1.CommandText = "select PERNO, EMAIL from emp_master where PERNO='" & TextBox4.Text & "' and status='working' ORDER BY PERNO"
        ElseIf DropDownList23.SelectedValue = "FORM 16" Then
            mc1.CommandText = "select PAN, EMAIL from emp_master where PERNO='" & TextBox4.Text & "' and status='working' ORDER BY PERNO"
        ElseIf DropDownList23.SelectedValue = "PF SLIP" Then
            mc1.CommandText = "select PERNO, EMAIL from emp_master where PERNO='" & TextBox4.Text & "' and status='working' ORDER BY PERNO"
        End If

        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows = True Then
            'dr.Read()
            While dr.Read()
                If Not IsDBNull(dr("EMAIL")) Then

                    If DropDownList23.SelectedValue = "PAYSLIP" Then
                        SendPAYSLIPEmail(dr("PERNO"), DropDownList16.SelectedValue, DropDownList17.SelectedValue, dr("EMAIL"))
                    ElseIf DropDownList23.SelectedValue = "SESBF" Then
                        SendSESBFPDFEmail(dr("PERNO"), DropDownList27.SelectedValue, dr("EMAIL"))
                    ElseIf DropDownList23.SelectedValue = "FORM 16" Then
                        SendForm16PDFEmail(dr("PAN"), DropDownList27.SelectedValue, dr("EMAIL"))
                    ElseIf DropDownList23.SelectedValue = "PF SLIP" Then
                        SendPFSlipEmail(dr("PERNO"), DropDownList27.SelectedValue, dr("EMAIL"))
                    End If

                End If
            End While

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Panel3.Visible = False
            Panel10.Visible = False
            Panel2.Visible = False
            Panel1.Visible = False
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False

            Return
        ElseIf DropDownList1.SelectedValue = "Payments" Then
            Panel10.Visible = True
            Panel2.Visible = False
            Panel1.Visible = False
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Recoveries" Then
            Panel10.Visible = False
            Panel2.Visible = True
            Panel1.Visible = False
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Attendance" Then
            Panel10.Visible = False
            Panel2.Visible = False
            Panel6.Visible = True
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = False
            Panel1.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Leave Balance" Then
            Panel10.Visible = False
            Panel2.Visible = False
            Panel1.Visible = True
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Cumulatives" Then
            Panel10.Visible = False
            Panel2.Visible = False
            Panel1.Visible = False
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = True
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Savings" Then
            Panel10.Visible = False
            Panel2.Visible = False
            Panel1.Visible = False
            Panel4.Visible = True
            Panel11.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Generate Document" Then
            Panel10.Visible = False
            Panel2.Visible = False
            Panel1.Visible = False
            Panel4.Visible = False
            Panel11.Visible = True
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Split Document" Then
            Panel10.Visible = False
            Panel2.Visible = False
            Panel1.Visible = False
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = True
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = False


        ElseIf DropDownList1.SelectedValue = "Email Document" Then
            Panel10.Visible = False
            Panel2.Visible = False
            Panel1.Visible = False
            Panel4.Visible = False
            Panel11.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel3.Visible = False
            Panel8.Visible = True


        End If
    End Sub

    Protected Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        If TextBox2.Text = "" Then
            TextBox2.Text = ""
            TextBox2.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Return
        ElseIf DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Return
        End If

        '''''''''''''''''''''''''''''''''''''''''
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range
        Dim rCnt As Integer
        Dim cCnt As Integer
        Dim Obj1, Obj2 As Object
        Dim Month, Year As New String("")
        Month = DropDownList2.SelectedValue
        Year = DropDownList3.SelectedValue
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(FileUpload3.FileName))
        xlWorkSheet = xlWorkBook.Worksheets(1)

        range = xlWorkSheet.UsedRange

        For rCnt = 2 To range.Rows.Count
            Dim value, header, pers_no, duty, w_off, cl, C_NHDAY, FL, EL, HPL, COM, ABS, COFF, OTHER As New String("")
            For cCnt = 1 To range.Columns.Count

                Obj1 = CType(range.Cells(1, cCnt), Excel.Range)
                header = Obj1.value

                Obj2 = CType(range.Cells(rCnt, cCnt), Excel.Range)
                value = Obj2.value

                If (header = "PERNO") Then
                    pers_no = value
                ElseIf (header = "DUTY") Then
                    duty = value
                ElseIf (header = "W_OFF") Then
                    w_off = value
                ElseIf (header = "CL") Then
                    cl = value
                ElseIf (header = "C_NHDAY") Then
                    C_NHDAY = value
                ElseIf (header = "FL") Then
                    FL = value
                ElseIf (header = "EL") Then
                    EL = value
                ElseIf (header = "HPL") Then
                    HPL = value
                ElseIf (header = "COM") Then
                    COM = value
                ElseIf (header = "ABS") Then
                    ABS = value
                ElseIf (header = "COFF") Then
                    COFF = value
                ElseIf (header = "Other") Then
                    OTHER = value
                End If

            Next

            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into emp_attendance(PERNO,ATTENDANCE_MONTH,MONTH,YEAR,DUTY,WOFF,CL,CH_NH,FL,EL,HPL,COM,ABS_EOL,Other,COFF) VALUES(@PERNO,@ATTENDANCE_MONTH,@MONTH,@YEAR,@DUTY,@WOFF,@CL,@CH_NH,@FL,@EL,@HPL,@COM,@ABS_EOL,@Other,@COFF)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PERNO", pers_no)
            cmd.Parameters.AddWithValue("@ATTENDANCE_MONTH", TextBox2.Text)
            cmd.Parameters.AddWithValue("@MONTH", Month)
            cmd.Parameters.AddWithValue("@YEAR", Year)
            cmd.Parameters.AddWithValue("@DUTY", duty)
            cmd.Parameters.AddWithValue("@WOFF", w_off)
            cmd.Parameters.AddWithValue("@CL", cl)
            cmd.Parameters.AddWithValue("@CH_NH", C_NHDAY)
            cmd.Parameters.AddWithValue("@FL", FL)
            cmd.Parameters.AddWithValue("@EL", EL)
            cmd.Parameters.AddWithValue("@HPL", HPL)
            cmd.Parameters.AddWithValue("@COM", COM)
            cmd.Parameters.AddWithValue("@ABS_EOL", ABS)
            cmd.Parameters.AddWithValue("@Other", OTHER)
            cmd.Parameters.AddWithValue("@COFF", COFF)
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        Next

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
    End Sub


    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If TextBox3.Text = "" Then
            TextBox3.Text = ""
            TextBox3.Focus()
            Return
        ElseIf DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        End If
        Dim ifPresentFlag As New Boolean()
        '''''''''''''''''''''''''''''''''''''''''
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range
        Dim rCnt As Integer
        Dim cCnt As Integer
        Dim Obj1, Obj2 As Object
        Dim Month, Year As New String("")
        Month = DropDownList4.SelectedValue
        Year = DropDownList5.SelectedValue
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(FileUpload4.FileName))
        xlWorkSheet = xlWorkBook.Worksheets(1)

        range = xlWorkSheet.UsedRange

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select PERNO from emp_master where STATUS='working'", conn)
        da.Fill(dt)
        conn.Close()

        For rCnt = 2 To range.Rows.Count
            ifPresentFlag = False
            Dim value, header, pers_no, cl, FL, EL, HPL As New String("")
            For cCnt = 1 To range.Columns.Count

                Obj1 = CType(range.Cells(1, cCnt), Excel.Range)
                header = Obj1.value

                Obj2 = CType(range.Cells(rCnt, cCnt), Excel.Range)
                value = Obj2.value

                If (header = "UNIT_PERNO") Then
                    pers_no = value
                ElseIf (header = "CL_BAL") Then
                    cl = value
                ElseIf (header = "FL_BAL") Then
                    FL = value
                ElseIf (header = "EL_BAL") Then
                    EL = value
                ElseIf (header = "HPL_BAL") Then
                    HPL = value
                End If

            Next

            For Each dRow As DataRow In dt.Rows
                For index As Integer = 0 To dt.Columns.Count - 1
                    If (Convert.ToString(dRow(index)).Contains(pers_no)) Then
                        ifPresentFlag = True

                    End If

                Next
            Next

            If (ifPresentFlag) Then

                conn.Open()
                Dim cmd As New SqlCommand
                Dim Query As String = "Insert Into emp_leave_balance(PERNO,LEAVE_BAL_MONTH,MONTH,YEAR,CL,EL,FL,HPL) VALUES(@PERNO,@LEAVE_BAL_MONTH,@MONTH,@YEAR,@CL,@EL,@FL,@HPL)"
                cmd = New SqlCommand(Query, conn)
                cmd.Parameters.AddWithValue("@PERNO", pers_no)
                cmd.Parameters.AddWithValue("@LEAVE_BAL_MONTH", TextBox3.Text)
                cmd.Parameters.AddWithValue("@MONTH", Month)
                cmd.Parameters.AddWithValue("@YEAR", Year)
                cmd.Parameters.AddWithValue("@CL", cl)
                cmd.Parameters.AddWithValue("@EL", EL)
                cmd.Parameters.AddWithValue("@FL", FL)
                cmd.Parameters.AddWithValue("@HPL", HPL)
                cmd.ExecuteReader()
                cmd.Dispose()
                conn.Close()

            End If

        Next

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
    End Sub


    Protected Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If DropDownList6.SelectedValue = "Select" Then
            DropDownList6.Focus()
            Return
        ElseIf DropDownList7.SelectedValue = "Select" Then
            DropDownList7.Focus()
            Return
        End If

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range
        Dim rCnt As Integer
        Dim cCnt As Integer
        Dim Obj1, Obj2, Obj3 As Object
        Dim Month, Year As New String("")
        Month = DropDownList6.SelectedValue
        Year = DropDownList7.SelectedValue
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(FileUpload5.FileName))
        xlWorkSheet = xlWorkBook.Worksheets(1)

        range = xlWorkSheet.UsedRange

        For rCnt = 2 To range.Rows.Count
            Dim value, header, pers_no, head, amount, pr_ind As New String("")
            For cCnt = 1 To range.Columns.Count

                Obj1 = CType(range.Cells(1, cCnt), Excel.Range)
                header = Obj1.value
                Obj2 = CType(range.Cells(rCnt, 3), Excel.Range)
                pers_no = Obj2.value
                Obj3 = CType(range.Cells(rCnt, cCnt), Excel.Range)
                value = Obj3.value

                checkColumnHeader_cummulative_savings(header, pers_no, value, Month, Year)
            Next


        Next








        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
    End Sub

    Private Sub checkColumnHeader_cummulative_savings(header As String, PERSNO As String, value As String, Month As String, Year As String)
        Select Case header

            ''==============================Cummulatives============================''

            Case "GROSS"
                emp_cummulatives(Month, Year, PERSNO, "GROSS", value)
            Case "CPF"
                emp_cummulatives(Month, Year, PERSNO, "CPF", value)
            Case "VPF"
                emp_cummulatives(Month, Year, PERSNO, "VPF", value)
            Case "EPS"
                emp_cummulatives(Month, Year, PERSNO, "EPS", value)
            Case "LIC"
                emp_cummulatives(Month, Year, PERSNO, "GSLI", value)
            Case "ITAX"
                emp_cummulatives(Month, Year, PERSNO, "ITAX", value)
            Case "SURCH"
                emp_cummulatives(Month, Year, PERSNO, "SURCH", value)
            Case "CESS"
                emp_cummulatives(Month, Year, PERSNO, "CESS", value)
            Case "CESSADDL"
                emp_cummulatives(Month, Year, PERSNO, "CESS ADDL", value)
            Case "SESBF"
                emp_cummulatives(Month, Year, PERSNO, "SESBF", value)
            Case "HAPERKS"
                emp_cummulatives(Month, Year, PERSNO, "HAPERKS", value)
            Case "OTH_PERKS"
                emp_cummulatives(Month, Year, PERSNO, "OTH PERKS", value)
            Case "PPF"
                emp_cummulatives(Month, Year, PERSNO, "PPF", value)
            Case "NSC"
                emp_cummulatives(Month, Year, PERSNO, "NSC", value)
            Case "CPFI"
                emp_cummulatives(Month, Year, PERSNO, "CPF LOAN INT BAL", value)
            Case "CPFL"
                emp_cummulatives(Month, Year, PERSNO, "CPF LOAN PRIN BAL", value)
            Case "CL"
                emp_cummulatives(Month, Year, PERSNO, "CAR LOAN BAL", value)
            Case "FL"
                emp_cummulatives(Month, Year, PERSNO, "FEST LOAN BAL", value)



            ''==============================Savings============================''

            Case "DIRSAV"
                emp_savings(Month, Year, PERSNO, "DIRSAV", value)
            Case "S80CCC"
                emp_savings(Month, Year, PERSNO, "S80CCC", value)
            Case "S80U"
                emp_savings(Month, Year, PERSNO, "S80U", value)
            Case "S80E"
                emp_savings(Month, Year, PERSNO, "S80E", value)
            Case "S80D"
                emp_savings(Month, Year, PERSNO, "S80D", value)
            Case "S80DD"
                emp_savings(Month, Year, PERSNO, "S80DD", value)
            Case "S80DDB"
                emp_savings(Month, Year, PERSNO, "S80DDB", value)
            Case "S80TTA"
                emp_savings(Month, Year, PERSNO, "S80TTA", value)
            Case "HOUSE_EXEMPT"
                emp_savings(Month, Year, PERSNO, "HBL ACCR INT", value)
            Case "S80G"
                emp_savings(Month, Year, PERSNO, "S80G", value)
            Case "S80CCD"
                emp_savings(Month, Year, PERSNO, "S80CCD", value)

            Case Else

        End Select
    End Sub


    Protected Sub emp_cummulatives(month As String, year As String, pers_no As String, head As String, amount As String)


        If CInt(amount) <> 0 Then
            Dim CummHashTable As New Hashtable
            CummHashTable.Add("GROSS", "1")
            CummHashTable.Add("CPF", "2")
            CummHashTable.Add("VPF", "3")
            CummHashTable.Add("EPS", "4")
            CummHashTable.Add("GSLI", "5")
            CummHashTable.Add("ITAX", "6")
            CummHashTable.Add("SURCH", "7")
            CummHashTable.Add("CESS", "8")
            CummHashTable.Add("CESS ADDL", "9")
            CummHashTable.Add("SESBF", "10")
            CummHashTable.Add("HAPERKS", "11")
            CummHashTable.Add("OTH PERKS", "12")
            CummHashTable.Add("PPF", "13")
            CummHashTable.Add("NSC", "14")
            CummHashTable.Add("CPF LOAN PRIN BAL", "15")
            CummHashTable.Add("CPF LOAN INT BAL", "16")
            CummHashTable.Add("FEST LOAN BAL", "17")
            CummHashTable.Add("CAR LOAN BAL", "18")

            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into emp_cumulative(PERNO,HEAD, AMOUNT,MONTH,YEAR,PR_IND) VALUES(@PERNO,@HEAD,@AMOUNT,@MONTH,@YEAR,@PR_IND)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PERNO", pers_no)
            cmd.Parameters.AddWithValue("@HEAD", head)
            cmd.Parameters.AddWithValue("@AMOUNT", amount)
            cmd.Parameters.AddWithValue("@MONTH", month)
            cmd.Parameters.AddWithValue("@YEAR", year)
            cmd.Parameters.AddWithValue("@PR_IND", CummHashTable.Item(head))

            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()

        End If
    End Sub


    Protected Sub emp_savings(month As String, year As String, pers_no As String, head As String, amount As String)



        If CInt(amount) <> 0 Then
            Dim SavingsHashTable As New Hashtable
            SavingsHashTable.Add("DIRSAV", "1")
            SavingsHashTable.Add("S80CCC", "2")
            SavingsHashTable.Add("S80U", "3")
            SavingsHashTable.Add("S80E", "4")
            SavingsHashTable.Add("S80D", "5")
            SavingsHashTable.Add("S80DD", "6")
            SavingsHashTable.Add("S80DDB", "7")
            SavingsHashTable.Add("S80TTA", "8")
            SavingsHashTable.Add("HBL ACCR INT", "9")
            SavingsHashTable.Add("S80G", "10")
            SavingsHashTable.Add("S80CCD", "11")

            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into emp_savings(PERNO,HEAD, AMOUNT,MONTH,YEAR,PR_IND) VALUES(@PERNO,@HEAD,@AMOUNT,@MONTH,@YEAR,@PR_IND)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PERNO", pers_no)
            cmd.Parameters.AddWithValue("@HEAD", head)
            cmd.Parameters.AddWithValue("@AMOUNT", amount)
            cmd.Parameters.AddWithValue("@MONTH", month)
            cmd.Parameters.AddWithValue("@YEAR", year)
            cmd.Parameters.AddWithValue("@PR_IND", SavingsHashTable.Item(head))

            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End If
    End Sub



    Protected Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If DropDownList8.SelectedValue = "Select" Then
            DropDownList8.Focus()
            Return
        ElseIf DropDownList19.SelectedValue = "Select" Then
            DropDownList19.Focus()
            Return
        End If


        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim range As Excel.Range
        Dim rCnt As Integer
        Dim Obj1, Obj2, Obj3 As Object
        Dim Month, Year, value, header, PERSNO As New String("")
        Month = DropDownList8.SelectedValue
        Year = DropDownList19.SelectedValue
        xlApp = New Excel.Application

        xlWorkBook = xlApp.Workbooks.Open(Server.MapPath(FileUpload6.FileName))
        xlWorkSheet = xlWorkBook.Worksheets(1)

        range = xlWorkSheet.UsedRange

        For rCnt = 2 To range.Rows.Count

            Obj1 = CType(range.Cells(rCnt, 3), Excel.Range)
            PERSNO = Obj1.value

            Obj2 = CType(range.Cells(rCnt, 4), Excel.Range)
            header = Obj2.value

            Obj3 = CType(range.Cells(rCnt, 5), Excel.Range)
            value = Obj3.value

            checkColumnHeader_cummulative_savings(header, PERSNO, value, Month, Year)
        Next

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)


    End Sub



    Protected Sub DropDownList21_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList21.SelectedIndexChanged

        If DropDownList21.SelectedValue = "PAYSLIP" Then
            MultiView1.ActiveViewIndex = 0
        ElseIf DropDownList21.SelectedValue = "SESBF" Then
            MultiView1.ActiveViewIndex = 1
        End If
    End Sub

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If DropDownList20.SelectedValue = "Select" Then
            DropDownList20.Focus()
            Return
        End If
        Dim crystalReport As New ReportDocument
        Dim dt2 As New DataTable
        Dim PO_QUARY As String = "SELECT '" & DropDownList20.SelectedValue & "' as fiscal_year,* FROM emp_master where STATUS='WORKING' ORDER BY BILL_NO,DEPARTMENT"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt2)
        crystalReport.Load(Server.MapPath("~/print_rpt/SESBF.rpt"))
        crystalReport.SetDataSource(dt2)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/reports/report.pdf"))
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

    Protected Sub DropDownList22_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList22.SelectedIndexChanged
        If DropDownList22.SelectedValue = "PAYSLIP" Then
            MultiView2.ActiveViewIndex = 0
        ElseIf DropDownList22.SelectedValue = "SESBF" Then
            MultiView2.ActiveViewIndex = 1
        ElseIf DropDownList22.SelectedValue = "PF SLIP" Then
            MultiView2.ActiveViewIndex = 1
        End If
    End Sub

    Protected Sub DropDownList23_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList23.SelectedIndexChanged
        If DropDownList23.SelectedValue = "PAYSLIP" Then
            MultiView3.ActiveViewIndex = 0
        ElseIf DropDownList23.SelectedValue = "SESBF" Then
            MultiView3.ActiveViewIndex = 1
        ElseIf DropDownList23.SelectedValue = "FORM 16" Then
            MultiView3.ActiveViewIndex = 1
        ElseIf DropDownList23.SelectedValue = "PF SLIP" Then
            MultiView3.ActiveViewIndex = 1
        End If
    End Sub
End Class