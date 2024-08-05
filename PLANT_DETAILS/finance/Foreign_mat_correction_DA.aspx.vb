Imports System.Data.SqlClient
Imports System.Globalization

Public Class Foreign_mat_correction_DA
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim dr As SqlDataReader
    Dim ds As New DataSet()
    Dim provider As CultureInfo = CultureInfo.InvariantCulture

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim ds As New DataSet
            conn.Open()
            da = New SqlDataAdapter("select distinct BE_DETAILS.be_no from BE_DETAILS join ho_da on BE_DETAILS.BE_NO=HO_DA.BE_NO where BE_STATUS='pending'", conn)
            da.Fill(ds, "BE_DETAILS")
            DropDownList1.Items.Clear()
            DropDownList1.DataSource = ds.Tables("BE_DETAILS")
            DropDownList1.DataValueField = "be_no"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            conn.Close()

            CalendarExtender1.EndDate = DateTime.Now.Date
            CalendarExtender2.EndDate = DateTime.Now.Date
            TextBox63_CalendarExtender.EndDate = DateTime.Now.Date

            Panel37.Visible = False
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Panel37.Visible = True
    End Sub

    Protected Sub Button57_Click(sender As Object, e As EventArgs) Handles Button57.Click
        If TextBox172.Text = "" Then
            TextBox172.Focus()
            Label43.Visible = True
            Label43.Text = "Please enter Admin Password"
            Return
        ElseIf TextBox172.Text <> "123456987" Then
            TextBox172.Text = ""
            TextBox172.Focus()
            Label43.Visible = True
            Label43.Text = "Incorrect Admin Password"
            Return
        ElseIf TextBox172.Text = "123456987" Then
            Label468.Text = "Data updated Successfully."
            Panel37.Visible = False
            Dim from_date, to_date As Date
            from_date = CDate(TextBox62.Text)
            to_date = CDate(TextBox63.Text)
            Dim MAT_VALUE_LEDGER, MAT_VALUE_DA, MAT_QTY_RCVD, BE_QTY, QTY_TO_BE_CONSIDERED, TOTAL_QTY_RCVD_SO_FAR, MAT_UNIT_PRICE_AS_PER_DA As New Decimal(0.00)
            Dim PO_NO, SUPL_ID, MAT_CODE, PUCHASE_CODE, VOUCHER_NO As New String("")

            ''CALCULATING MATERIAL VALUE AS PER LEDGER
            conn.Open()
            Dim MC As New SqlCommand
            MC.CommandText = "select SUM(AMOUNT_CR) AS LEDGER_MAT_VALUE from LEDGER where BE_NO='" & DropDownList1.SelectedValue & "' and EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and AC_NO='51222' AND GARN_NO_MB_NO LIKE 'RGARN%'"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                MAT_VALUE_LEDGER = dr.Item("LEDGER_MAT_VALUE")
                dr.Close()
            End If
            conn.Close()

            ''GETTING TOTAL BE QTY RECEIVED TO FAR
            conn.Open()
            MC.CommandText = "SELECT SUM(MAT_CHALAN_QTY) AS QTY_RCVD FROM PO_RCD_MAT WHERE BE_NO='" & DropDownList1.SelectedValue & "' AND GARN_DATE <='" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and GARN_NO like 'RGARN%'"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TOTAL_QTY_RCVD_SO_FAR = dr.Item("QTY_RCVD")

                dr.Close()
            End If
            conn.Close()

            ''GETTING PO_NO, SUPL_ID, MAT_CODE
            conn.Open()
            MC.CommandText = "SELECT SUM(MAT_CHALAN_QTY) AS QTY_RCVD, MAX(PO_NO) AS PO_NO, MAX(SUPL_ID) AS SUPL_ID, MAX(MAT_CODE) AS MAT_CODE FROM PO_RCD_MAT WHERE BE_NO='" & DropDownList1.SelectedValue & "' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and GARN_NO like 'RGARN%'"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                MAT_QTY_RCVD = dr.Item("QTY_RCVD")
                PO_NO = dr.Item("PO_NO")
                SUPL_ID = dr.Item("SUPL_ID")
                MAT_CODE = dr.Item("MAT_CODE")
                dr.Close()
            End If
            conn.Close()


            ''GETTING PURCHASE CODE OF MATERIAL
            conn.Open()
            MC.CommandText = "SELECT AC_PUR FROM MATERIAL WHERE MAT_CODE='" & MAT_CODE & "'"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                PUCHASE_CODE = dr.Item("AC_PUR")
                dr.Close()
            End If
            conn.Close()

            ''GETTING UNIT PRICE AS PER DA
            conn.Open()
            MC.CommandText = "select UNIT_PRICE, MAT_QTY from HO_DA where BE_NO='" & DropDownList1.SelectedValue & "'"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                MAT_UNIT_PRICE_AS_PER_DA = dr.Item("UNIT_PRICE")
                BE_QTY = dr.Item("MAT_QTY")
                dr.Close()
            End If
            conn.Close()

            If (TOTAL_QTY_RCVD_SO_FAR <= BE_QTY) Then
                QTY_TO_BE_CONSIDERED = MAT_QTY_RCVD
            Else
                QTY_TO_BE_CONSIDERED = MAT_QTY_RCVD - (TOTAL_QTY_RCVD_SO_FAR - BE_QTY)
            End If

            MAT_VALUE_DA = FormatNumber(QTY_TO_BE_CONSIDERED * MAT_UNIT_PRICE_AS_PER_DA, 2)

            Dim STR1 As String = ""
            Dim working_date As Date

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

            Dim count As Integer
            count = 0
            conn.Open()
            ds.Clear()
            da = New SqlDataAdapter("select DISTINCT TOKEN_NO FROM VOUCHER WHERE FISCAL_YEAR=" & STR1, conn)
            count = da.Fill(dt)
            conn.Close()
            VOUCHER_NO = STR1 & count + 1
            TextBox61.Text = VOUCHER_NO

            If ((MAT_VALUE_DA - MAT_VALUE_LEDGER) <> 0) Then
                conn.Open()
                Dim query As String = "INSERT INTO VOUCHER (JE_DATE,JE_NO,TOKEN_NO,TOKEN_DATE,SEC_NO,SEC_DATE,VOUCHER_TYPE,PAY_TYPE,NET_AMT,PARTICULAR,SUPL_ID,EMP_ID,FISCAL_YEAR,CVB_NO,CVB_DATE,CHEQUE_NO,CHEQUE_DATE)VALUES(@JE_DATE,@JE_NO,@TOKEN_NO,@TOKEN_DATE,@SEC_NO,@SEC_DATE,@VOUCHER_TYPE,@PAY_TYPE,@NET_AMT,@PARTICULAR,@SUPL_ID,@EMP_ID,@FISCAL_YEAR,@CVB_NO,@CVB_DATE,@CHEQUE_NO,@CHEQUE_DATE)"
                Dim cmd As New SqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@TOKEN_NO", VOUCHER_NO)
                cmd.Parameters.AddWithValue("@TOKEN_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", Provider))
                cmd.Parameters.AddWithValue("@SEC_NO", "04")
                cmd.Parameters.AddWithValue("@SEC_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@JE_NO", "04")
                cmd.Parameters.AddWithValue("@JE_DATE", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                cmd.Parameters.AddWithValue("@VOUCHER_TYPE", "Normal")
                cmd.Parameters.AddWithValue("@PAY_TYPE", "")
                cmd.Parameters.AddWithValue("@NET_AMT", 0.0)
                cmd.Parameters.AddWithValue("@PARTICULAR", "BEING ENTRY PASS FOR DIFFERENCE IN EXCHANGE RATE AND BANK CHARGES OF IMPORTED MATERIAL FOR FY " & STR1 & " " & DropDownList28.SelectedValue)
                cmd.Parameters.AddWithValue("@SUPL_ID", "")
                cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                cmd.Parameters.AddWithValue("@CVB_NO", "")
                cmd.Parameters.AddWithValue("@CVB_DATE", "")
                cmd.Parameters.AddWithValue("@CHEQUE_NO", "")
                cmd.Parameters.AddWithValue("@CHEQUE_DATE", "")
                cmd.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                cmd.ExecuteReader()
                cmd.Dispose()
                conn.Close()
            End If

            If (MAT_VALUE_DA < MAT_VALUE_LEDGER) Then

                save_ledger(DropDownList1.SelectedValue, PO_NO, "", "", SUPL_ID, "51222", "Dr", (MAT_VALUE_LEDGER - MAT_VALUE_DA), "PROV. CRED. FOR RM(FOR.)", VOUCHER_NO, 1, "EXCHANGE DIFFERENCE")
                save_ledger(DropDownList1.SelectedValue, PO_NO, "", "", SUPL_ID, PUCHASE_CODE, "Cr", (MAT_VALUE_LEDGER - MAT_VALUE_DA), "PUR", VOUCHER_NO, 2, "EXCHANGE DIFFERENCE")

            ElseIf (MAT_VALUE_DA > MAT_VALUE_LEDGER) Then

                save_ledger(DropDownList1.SelectedValue, PO_NO, "", "", SUPL_ID, PUCHASE_CODE, "Dr", (MAT_VALUE_DA - MAT_VALUE_LEDGER), "PUR", VOUCHER_NO, 1, "EXCHANGE DIFFERENCE")
                save_ledger(DropDownList1.SelectedValue, PO_NO, "", "", SUPL_ID, "51222", "Cr", (MAT_VALUE_DA - MAT_VALUE_LEDGER), "PROV. CRED. FOR RM(FOR.)", VOUCHER_NO, 2, "EXCHANGE DIFFERENCE")

            End If
        End If
    End Sub


    Protected Sub save_ledger(BE_NO As String, PO_NO As String, garn_mb As String, inv_no As String, SUPL_ID As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String, VOUCHER_NO As String, line_no As Integer, PAY_IND As String)

        Dim working_date As Date
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox1.Text) = False Then
            TextBox1.Text = ""
            TextBox1.Focus()
            Return
        End If
        working_date = CDate(TextBox1.Text)
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
            Dim Query As String = "Insert Into LEDGER(BE_NO,REVERSAL_INDICATOR,Journal_ID,JURNAL_LINE_NO,VOUCHER_NO,INVOICE_NO,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@BE_NO,@REVERSAL_INDICATOR,@Journal_ID,@JURNAL_LINE_NO,@VOUCHER_NO,@INVOICE_NO,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@Journal_ID", "04")
            cmd.Parameters.AddWithValue("@PO_NO", PO_NO)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", garn_mb)
            cmd.Parameters.AddWithValue("@SUPL_ID", SUPL_ID)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@INVOICE_NO", inv_no)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", PAY_IND)
            cmd.Parameters.AddWithValue("@VOUCHER_NO", VOUCHER_NO)
            cmd.Parameters.AddWithValue("@JURNAL_LINE_NO", line_no)
            cmd.Parameters.AddWithValue("@REVERSAL_INDICATOR", "Normal")
            cmd.Parameters.AddWithValue("@BE_NO", BE_NO)
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
            ''
        End If
    End Sub

    Protected Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged
        If (DropDownList10.SelectedValue = "Exchange Rate") Then
            MultiView1.ActiveViewIndex = 0
        ElseIf (DropDownList10.SelectedValue = "SIT") Then
            MultiView1.ActiveViewIndex = 1
        ElseIf (DropDownList10.SelectedValue = "Shortage Entry") Then
            MultiView1.ActiveViewIndex = 2
        End If
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim from_date, to_date As Date
        from_date = CDate(TextBox3.Text)
        to_date = CDate(TextBox4.Text)

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("DECLARE @TT TABLE(SUPL_ID VARCHAR(20),SUPL_NAME VARCHAR(100),BE_NO VARCHAR(30),AC_NO VARCHAR(20),AC_DESCRIPTION VARCHAR(100),HO_DA DECIMAL(16,2),RCD_VALUATION_SYSTEM DECIMAL(16,2))
        INSERT INTO @TT

        SELECT L1.SUPL_ID,SUPL_NAME,BE_NO,L1.AC_NO,ac_description,sum(AMOUNT_DR) as HO_DA,0 as RCD_VALUATION_SYSTEM FROM LEDGER L1 join SUPL S1 on L1.SUPL_ID=S1.SUPL_ID join ACDIC on L1.AC_NO=ACDIC.ac_code WHERE (BE_NO is not null and BE_NO <>'') and REVERSAL_INDICATOR='normal' and EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and AC_NO in ('51222','86201','51746') and AMOUNT_DR>0 group by BE_NO,L1.SUPL_ID,SUPL_NAME,AC_NO,ac_description order by BE_NO,AC_NO

        INSERT INTO @TT
        SELECT L1.SUPL_ID,SUPL_NAME,BE_NO,L1.AC_NO,ac_description,0 as HO_DA,sum(AMOUNT_CR) as RCD_VALUATION_SYSTEM FROM LEDGER L1 join SUPL S1 on L1.SUPL_ID=S1.SUPL_ID join ACDIC on L1.AC_NO=ACDIC.ac_code WHERE (BE_NO is not null and BE_NO <>'') and REVERSAL_INDICATOR IS NULL and EFECTIVE_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and AC_NO in ('51222','86201','51746') and AMOUNT_CR>0 group by BE_NO,L1.SUPL_ID,SUPL_NAME,AC_NO,ac_description order by BE_NO,AC_NO
        select SUPL_ID,SUPL_NAME,BE_NO,AC_NO,ac_description,SUM(HO_DA) AS HO_DA,sum(RCD_VALUATION_SYSTEM) as RCD_VALUATION_SYSTEM, (SUM(HO_DA)-SUM(RCD_VALUATION_SYSTEM)) AS DIFFERENCE_VALUE from @TT GROUP BY BE_NO,SUPL_ID,SUPL_NAME,AC_NO,ac_description order by BE_NO,SUPL_ID,AC_NO", conn)

        da.Fill(dt)
        conn.Close()
        GridView6.DataSource = dt
        GridView6.DataBind()
        conn.Close()
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub
End Class