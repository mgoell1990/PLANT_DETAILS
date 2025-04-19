Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net
Public Class RCD_Voucher_Forefeit
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim dt As New DataTable
    Dim da As New SqlDataAdapter
    Dim working_date As Date = Today.Date
    Dim goAheadFlag As Boolean = True
    Dim str As String
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim taxable_rate_unit, cgst_amt, sgst_amt, igst_amt, cess_amt, net_pay As New Decimal(0)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If

        TextBox193.Attributes.Add(“readonly”, “readonly”)
        TextBox194.Attributes.Add(“readonly”, “readonly”)
        TextBox195.Attributes.Add(“readonly”, “readonly”)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub BINDGRID()
        GridView2.DataSource = DirectCast(ViewState("RCM"), DataTable)
        GridView2.DataBind()
    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim dt2 As New DataTable()
        dt2.Columns.AddRange(New DataColumn(17) {New DataColumn("GARN_NO"), New DataColumn("MAT_SLNO"), New DataColumn("MAT_CODE"), New DataColumn("MAT_NAME"), New DataColumn("MAT_AU"), New DataColumn("CHPTR_HEAD"), New DataColumn("UNIT_RATE"), New DataColumn("MAT_QTY"), New DataColumn("TAX_VAL"), New DataColumn("CGST_P"), New DataColumn("CGST"), New DataColumn("SGST_P"), New DataColumn("SGST"), New DataColumn("IGST_P"), New DataColumn("IGST"), New DataColumn("CESS_P"), New DataColumn("CESS"), New DataColumn("TOTAL_VAL")})
        ViewState("RCM") = dt2
        Me.BINDGRID()

        Panel3.Visible = True
        TextBox126.ReadOnly = True
        TextBox125.ForeColor = Drawing.Color.White
        TextBox126.BackColor = Drawing.Color.Red
        TextBox126.Text = ""

        TextBox188.ReadOnly = True
        TextBox188.ForeColor = Drawing.Color.White
        TextBox188.BackColor = Drawing.Color.Red
        TextBox188.Text = ""

        TextBox189.ReadOnly = True
        TextBox189.ForeColor = Drawing.Color.White
        TextBox189.BackColor = Drawing.Color.Red
        TextBox189.Text = ""
        If DropDownList26.Text = "" Then
            DropDownList26.Focus()
            Return
        End If


        ''SEARCH SALE ORDER VENDER DETAILS

        conn.Open()
        mycommand.CommandText = "select SUPL.SUPL_ID,SUPL.SUPL_NAME,SUPL.SUPL_STATE_CODE,SUPL.SUPL_STATE from SUPL join VOUCHER on VOUCHER.SUPL_ID=SUPL.SUPL_ID where VOUCHER.TOKEN_NO='" & DropDownList26.Text & "'
                                union
                                select d_code as SUPL_ID,d_name as SUPL_NAME,d_state_code as SUPL_STATE_CODE,d_state as SUPL_STATE from dater join VOUCHER on VOUCHER.SUPL_ID=dater.d_code where VOUCHER.TOKEN_NO='" & DropDownList26.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox125.Text = dr.Item("SUPL_ID")
            TextBox126.Text = dr.Item("SUPL_NAME")
            TextBox188.Text = dr.Item("SUPL_STATE_CODE")
            TextBox189.Text = dr.Item("SUPL_STATE")
            dr.Close()
        End If
        conn.Close()
        TextBox124.Text = DropDownList26.Text

        If (TextBox188.Text = "22") Then
            TextBox193.Text = "9"
            TextBox194.Text = "9"
            TextBox195.Text = "0"
        Else
            TextBox193.Text = "0"
            TextBox194.Text = "0"
            TextBox195.Text = "18"
        End If

        Panel2.Visible = True
        Panel8.Visible = False
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Panel3.Visible = True
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("Select CVB_NO FROM VOUCHER WHERE TOKEN_NO='" & TextBox124.Text & "' and REFUND_STATUS is null", conn)
        da.Fill(dt)
        conn.Close()

        DropDownList30.Items.Clear()
        DropDownList30.DataSource = dt
        DropDownList30.DataValueField = "CVB_NO"
        DropDownList30.DataBind()
        DropDownList30.Items.Insert(0, "Select")
        DropDownList30.SelectedValue = "Select"
    End Sub

    Protected Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        conn.Open()
        dt.Clear()
        Dim cgst As New Decimal(0.00)
        da = New SqlDataAdapter("select '10' As MAT_SLNO, 'EMD Forefeit' As MAT_NAME, 'Service' As MAT_AU,999794 as sac_code, '" & TextBox192.Text & "' AS UNIT_PRICE, '1' As MAT_CHALAN_QTY, '1' As MAT_RCD_QTY, '" & TextBox192.Text & "' as prov_amt, 
                    '" & TextBox193.Text & "' AS CGST, CAST((round(('" & (CDec(TextBox192.Text) * CDec(TextBox193.Text)) / 100 & "'),2,1)) as decimal(18,2)) as cgst_liab,
					'" & TextBox194.Text & "' AS SGST, CAST((round(('" & (CDec(TextBox192.Text) * CDec(TextBox194.Text)) / 100 & "'),2,1)) as decimal(18,2)) as sgst_liab, 
                    '" & TextBox195.Text & "' AS IGST, CAST((round(('" & (CDec(TextBox192.Text) * CDec(TextBox195.Text)) / 100 & "'),2,1)) as decimal(18,2)) as igst_liab,
					'0.00' AS CESS, '0.00' as cess_liab	from voucher p1 where TOKEN_NO='" & TextBox124.Text & "' and REFUND_STATUS is null", conn)
        da.Fill(dt)
        conn.Close()
        GridView2.DataSource = dt
        GridView2.DataBind()

        GridView2.Rows(GridView2.Rows.Count - 1).Cells(15).Text = CDec(GridView2.Rows(GridView2.Rows.Count - 1).Cells(6).Text) + CDec(GridView2.Rows(GridView2.Rows.Count - 1).Cells(8).Text) + CDec(GridView2.Rows(GridView2.Rows.Count - 1).Cells(10).Text) + CDec(GridView2.Rows(GridView2.Rows.Count - 1).Cells(12).Text) + CDec(GridView2.Rows(GridView2.Rows.Count - 1).Cells(14).Text)
        Button36.Enabled = True
    End Sub

    Protected Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                'Dim working_date As Date
                If TextBox3.Text = "" Then
                    TextBox3.Focus()
                    Return
                ElseIf IsDate(TextBox3.Text) = False Then
                    TextBox3.Text = ""
                    TextBox3.Focus()
                    Return
                ElseIf DropDownList33.SelectedValue = "Select" Then
                    Label308.Text = "Please select party type."
                    DropDownList33.Focus()
                    Return
                Else
                    Label308.Text = ""
                End If
                working_date = CDate(TextBox3.Text)
                'working_date = Today.Date

                Dim DESPATCH_TYPE As Integer = 0
                If DropDownList33.SelectedValue = "I.P.T." Then
                    DESPATCH_TYPE = 4
                ElseIf DropDownList33.SelectedValue = "Other" Then
                    DESPATCH_TYPE = 1
                End If

                'invoice type for GST
                Dim inv_type_new, inv_rule_new, inv_for_new, inv_type1_new As String

                'SAVE RCM_INV
                Dim supl_state, supl_state_code, gst_no As New String("")
                Dim SUPL_INV As String = ""
                If Panel3.Visible = True Then
                    SUPL_INV = DropDownList30.SelectedValue
                    supl_state = TextBox189.Text
                    supl_state_code = TextBox188.Text
                End If

                Dim gst_code, my_gst_code, COMM, DIVISION As New String("")

                conn.Open()
                mycommand.CommandText = "SELECT * FROM SUPL WHERE SUPL_ID='" & TextBox125.Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    gst_code = dr.Item("SUPL_GST_NO")
                    dr.Close()

                End If
                conn.Close()

                conn.Open()
                mycommand.CommandText = "select * from comp_profile"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    my_gst_code = dr.Item("c_gst_no")
                    COMM = dr.Item("c_comm")
                    DIVISION = dr.Item("c_division")
                    dr.Close()
                End If
                conn.Close()

                'Inv No generation for GST (Forward Charge)
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

                If DropDownList2.SelectedValue = "Payment Voucher" Then

                    Dim total_taxable_value, total_cgst, total_sgst, total_igst, total_cess, total_value As New Decimal(0)
                    For i = 0 To GridView2.Rows.Count - 1


                        total_taxable_value = total_taxable_value + CDec(GridView2.Rows(i).Cells(6).Text)
                        total_cgst = total_cgst + CDec(GridView2.Rows(i).Cells(8).Text)
                        total_sgst = total_sgst + CDec(GridView2.Rows(i).Cells(10).Text)
                        total_igst = total_igst + CDec(GridView2.Rows(i).Cells(12).Text)
                        total_cess = total_cess + CDec(GridView2.Rows(i).Cells(14).Text)
                        total_value = total_value + CDec(GridView2.Rows(i).Cells(15).Text)

                    Next

                    'invoice type for GST
                    inv_type_new = "Tax Invoice"
                    inv_type1_new = "(For Services under Forward Charge)"
                    inv_rule_new = "(In case of Supplies from unregistered Suppliers under Section 31(3)(f)of CGST Act 2017 read with Rule 1 of Tax Invoice ,Credit and Debit Note Rules)"
                    inv_for_new = "OS15"

                    ''Save data to despatch table
                    generateFCM(DESPATCH_TYPE, inv_type_new, inv_type1_new, inv_rule_new, inv_for_new, SUPL_INV, STR1, COMM, DIVISION, supl_state, supl_state_code)

                    ''Save data to ledger
                    save_ledger(DropDownList30.SelectedValue, TextBox124.Text, TextBox190.Text & TextBox191.Text, TextBox125.Text, "51404", "Dr", total_value, "EMD DEPOSIT")
                    save_ledger(DropDownList30.SelectedValue, TextBox124.Text, TextBox190.Text & TextBox191.Text, TextBox125.Text, "71304", "Cr", total_taxable_value, "EMD FOREFEIT")
                    save_ledger(DropDownList30.SelectedValue, TextBox124.Text, TextBox190.Text & TextBox191.Text, TextBox125.Text, "54822", "Cr", total_cgst, "CGST_PAYABLE")
                    save_ledger(DropDownList30.SelectedValue, TextBox124.Text, TextBox190.Text & TextBox191.Text, TextBox125.Text, "54823", "Cr", total_sgst, "SGST_PAYABLE")
                    save_ledger(DropDownList30.SelectedValue, TextBox124.Text, TextBox190.Text & TextBox191.Text, TextBox125.Text, "54821", "Cr", total_igst, "IGST_PAYABLE")


                End If


                Dim QUARY1 As String = ""

                Button36.Enabled = False
                GridView2.Visible = False

                'myTrans.Commit()
                'Label388.Text = "All records are written to database."
                ''===========================Generate E-Invoice through NIC Start=======================''

                'If gst_code = my_gst_code Then
                '    ''E-Invoice is not required
                'Else
                '    Dim logicClassObj = New EinvoiceLogicClass
                '    Dim AuthErrorData As List(Of AuthenticationErrorDetailsClass) = logicClassObj.EinvoiceAuthentication(TextBox190.Text + TextBox191.Text, TextBox125.Text)
                '    If (AuthErrorData.Item(0).status = "1") Then

                '        Dim EinvErrorData As List(Of EinvoiceErrorDetailsClass) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).client_id, AuthErrorData.Item(0).client_secret, AuthErrorData.Item(0).gst_no, AuthErrorData.Item(0).user_name, AuthErrorData.Item(0).AuthToken, AuthErrorData.Item(0).Sek, AuthErrorData.Item(0).appKey, AuthErrorData.Item(0).systemInvoiceNo, AuthErrorData.Item(0).buyerPartyCode, "YES")
                '        If (EinvErrorData.Item(0).status = "1") Then
                '            TextBox6.Text = EinvErrorData.Item(0).IRN

                '            Dim sqlQuery As String = ""
                '            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "' where D_TYPE+INV_NO  ='" & TextBox190.Text + TextBox191.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                '            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                '            despatch.ExecuteReader()
                '            despatch.Dispose()

                '            goAheadFlag = True
                '        ElseIf (EinvErrorData.Item(0).status = "2") Then
                '            Label31.Visible = True
                '            Label42.Visible = True
                '            txtEinvoiceErrorCode.Visible = True
                '            txtEinvoiceErrorMessage.Visible = True
                '            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorCode
                '            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                '            goAheadFlag = False
                '            Label388.Text = "There is some response error in E-Invoice generation."
                '        End If

                '    ElseIf (AuthErrorData.Item(0).status = "2") Then

                '        Label31.Visible = True
                '        Label42.Visible = True
                '        txtEinvoiceErrorCode.Visible = True
                '        txtEinvoiceErrorMessage.Visible = True
                '        txtEinvoiceErrorCode.Text = AuthErrorData.Item(0).errorCode
                '        txtEinvoiceErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                '        goAheadFlag = False
                '        Label388.Text = "There is some response error in E-invoice Authentication."
                '    Else
                '        goAheadFlag = False
                '        Label388.Text = "There is some response error in E-invoice Authentication."
                '    End If


                'End If

                'If (goAheadFlag = True) Then
                '    myTrans.Commit()
                '    Label388.Text = "All records are written to database."
                '    Label31.Visible = False
                '    Label42.Visible = False
                '    txtEinvoiceErrorCode.Visible = False
                '    txtEinvoiceErrorMessage.Visible = False
                'Else
                '    myTrans.Rollback()
                '    conn.Close()
                '    conn_trans.Close()
                '    TextBox191.Text = ""
                '    TextBox190.Text = ""
                '    TextBox6.Text = ""

                'End If

                ''===========================Generate E-Invoice through NIC End=======================''


                ''===========================Generate E-Invoice Through EY Start=======================''

                If gst_code = my_gst_code Then
                    'E-Invoice is not required
                Else
                    Dim logicClassObj = New EinvoiceLogicClassEY
                    Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox190.Text + TextBox191.Text, TextBox125.Text)
                    If (AuthErrorData.Item(0).status = "1") Then

                        Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox190.Text + TextBox191.Text, TextBox125.Text, TextBox125.Text, "YES", "N", "INV")

                        If (EinvErrorData.Item(0).status = "1") Then
                            TextBox6.Text = EinvErrorData.Item(0).IRN

                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "' where D_TYPE+INV_NO  ='" & TextBox190.Text + TextBox191.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            goAheadFlag = True

                        ElseIf (EinvErrorData.Item(0).status = "2") Then
                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorCode
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                            goAheadFlag = False
                            Label308.Text = "There is some response error in E-Invoice generation."

                        ElseIf (EinvErrorData.Item(0).status = "3") Then
                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorfield
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errordesc
                            goAheadFlag = False
                            Label308.Text = "There is some response error in E-Invoice generation."
                        ElseIf (EinvErrorData.Item(0).status = "4") Then
                            TextBox6.Text = EinvErrorData.Item(0).IRN
                            Label31.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                            txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage
                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "' where D_TYPE+INV_NO  ='" & TextBox190.Text + TextBox191.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            goAheadFlag = True

                            'Label308.Text = "There is error in E-way bill generation, please generate E-way bill alone with above IRN."
                        End If

                    ElseIf (AuthErrorData.Item(0).status = "2") Then

                        Label31.Visible = True
                        Label42.Visible = True
                        txtEinvoiceErrorCode.Visible = True
                        txtEinvoiceErrorMessage.Visible = True
                        txtEinvoiceErrorCode.Text = AuthErrorData.Item(0).errorCode
                        txtEinvoiceErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                        goAheadFlag = False
                        Label308.Text = "There is some response error in E-invoice Authentication."
                    Else
                        goAheadFlag = False
                        Label308.Text = "There is some response error in E-invoice Authentication."
                    End If


                End If

                If (goAheadFlag = True) Then
                    myTrans.Commit()
                    Label308.Text = "All records are written to database."
                    Label31.Visible = False
                    Label42.Visible = False
                    txtEinvoiceErrorCode.Visible = False
                    txtEinvoiceErrorMessage.Visible = False
                Else
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    TextBox190.Text = ""
                    TextBox191.Text = ""
                    TextBox6.Text = ""

                End If

                ''===========================Generate E-Invoice Through EY End=======================''

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label308.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using
    End Sub

    Protected Sub save_ledger(RCD_VOUCHER_NO As String, so_no As String, inv_no As String, partyCode As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
        Dim working_date As Date

        working_date = Today.Date
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
            dr_value = 0
            cr_value = 0
            If ac_term = "Dr" Then
                dr_value = price
                cr_value = 0.0
            ElseIf ac_term = "Cr" Then
                dr_value = 0.0
                cr_value = price
            End If


            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(AGING_FLAG,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@AGING_FLAG,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn_trans, myTrans)
            cmd.Parameters.AddWithValue("@PO_NO", so_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", inv_no)
            cmd.Parameters.AddWithValue("@SUPL_ID", partyCode)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.Parameters.AddWithValue("@AGING_FLAG", RCD_VOUCHER_NO)
            cmd.ExecuteReader()
            cmd.Dispose()

        End If
    End Sub

    Protected Sub generateFCM(DESPATCH_TYPE As String, inv_type_new As String, inv_type1_new As String, inv_rule_new As String, inv_for_new As String, SUPL_INV As String, STR1 As String, COMM As String, DIVISION As String, supl_state As String, supl_state_code As String)

        ''''''''''''''''''''''''''''''''
        Dim inv_no_ld_pen As String = ""
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "SELECT (CASE WHEN MAX(inv_no) IS NULL THEN 0 ELSE MAX(inv_no) END) as inv_no FROM DESPATCH WITH(NOLOCK) WHERE D_TYPE LIKE'" & inv_for_new + DESPATCH_TYPE & "%' AND FISCAL_YEAR =" & STR1
        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_no_ld_pen = dr.Item("inv_no")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim prefixFY = Left(STR1, 2)

        ''''''''''''''''''''''''''''''''
        If CInt(inv_no_ld_pen) = 0 Then
            TextBox191.Text = prefixFY & "00001"
            TextBox190.Text = inv_for_new & CStr(DESPATCH_TYPE)
            TextBox190.ReadOnly = True
            TextBox191.ReadOnly = True
        Else
            str = CInt(inv_no_ld_pen) + 1
            If str.Length = 1 Then
                str = prefixFY & "0000" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 2 Then
                str = prefixFY & "000" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 3 Then
                str = prefixFY & "00" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 4 Then
                str = prefixFY & "0" & CInt(inv_no_ld_pen) + 1
            ElseIf str.Length = 5 Then
                str = prefixFY & CInt(inv_no_ld_pen) + 1
            End If
            TextBox191.Text = str
            TextBox190.Text = inv_for_new & CStr(DESPATCH_TYPE)
            TextBox190.ReadOnly = True
            TextBox191.ReadOnly = True
        End If

        Dim so_date, SO_ACTUAL_DATE, amd_date As Date
        Dim actual_so, amd_no, party_code, CONSIGN_CODE, PLACE_OF_SUPPLY As New String("")

        so_date = Today.Date
        SO_ACTUAL_DATE = Today.Date
        'search order_details
        'conn.Open()
        'mycommand.CommandText = "select SO_ACTUAL ,SO_DATE ,SO_ACTUAL_DATE,DESTINATION from ORDER_DETAILS WITH(NOLOCK) where SO_NO ='" & TextBox124.Text & "'"
        'mycommand.Connection = conn
        'dr = mycommand.ExecuteReader
        'If dr.HasRows Then
        '    dr.Read()
        '    actual_so = dr.Item("SO_ACTUAL")
        '    so_date = dr.Item("SO_DATE")
        '    SO_ACTUAL_DATE = dr.Item("SO_ACTUAL_DATE")
        '    PLACE_OF_SUPPLY = dr.Item("DESTINATION")
        '    dr.Close()
        'Else
        '    dr.Close()
        'End If
        'conn.Close()


        'Search Supplier details
        Dim SUPL_AT, SUPL_PO, SUPL_DIST, SUPL_PIN, SUPL_GST_NO As New String("")
        conn.Open()
        mycommand.CommandText = "SELECT SUPL_AT,SUPL_PO, SUPL_DIST, SUPL_PIN, SUPL_GST_NO FROM SUPL WHERE SUPL_ID='" & TextBox125.Text & "'
                                 UNION SELECT ADD_1 AS SUPL_AT,ADD_2 AS SUPL_PO, D_CITY AS SUPL_DIST, D_PIN AS SUPL_PIN, GST_CODE AS SUPL_GST_NO FROM DATER WHERE d_code='" & TextBox125.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            SUPL_AT = dr.Item("SUPL_AT")
            SUPL_PO = dr.Item("SUPL_PO")
            SUPL_DIST = dr.Item("SUPL_DIST")
            SUPL_PIN = dr.Item("SUPL_PIN")
            SUPL_GST_NO = dr.Item("SUPL_GST_NO")
            dr.Close()
        Else
            SUPL_AT = ""
            SUPL_PO = ""
            SUPL_DIST = ""
            SUPL_PIN = ""
            SUPL_GST_NO = ""
            dr.Close()
        End If
        conn.Close()


        amd_no = "N/A"
        amd_date = Today.Date
        party_code = TextBox125.Text
        CONSIGN_CODE = TextBox125.Text


        Dim i As Integer = 0

        Dim total_taxable_value, total_cgst, total_sgst, total_igst, total_cess, total_value, CGST_RATE, SGST_RATE, IGST_RATE As New Decimal(0)
        For i = 0 To GridView2.Rows.Count - 1

            taxable_rate_unit = taxable_rate_unit + CDec(GridView2.Rows(i).Cells(4).Text)
            total_taxable_value = total_taxable_value + CDec(GridView2.Rows(i).Cells(6).Text)
            cgst_amt = cgst_amt + CDec(GridView2.Rows(i).Cells(8).Text)
            sgst_amt = sgst_amt + CDec(GridView2.Rows(i).Cells(10).Text)
            igst_amt = igst_amt + CDec(GridView2.Rows(i).Cells(12).Text)
            cess_amt = cess_amt + CDec(GridView2.Rows(i).Cells(14).Text)
            net_pay = net_pay + CDec(GridView2.Rows(i).Cells(15).Text)

        Next

        If (igst_amt > 0) Then
            SGST_RATE = 0.00
            CGST_RATE = 0.00
            IGST_RATE = 18.0
        Else
            SGST_RATE = 9.0
            CGST_RATE = 9.0
            IGST_RATE = 0.00
        End If

        'save despatch


        Dim QUARY As String = ""
        QUARY = "Insert Into DESPATCH(TCS_AMT,BILL_NO,INV_STATUS,SO_NO ,SO_DATE ,PO_NO ,PO_DATE ,AMD_NO ,AMD_DATE ,TRANS_WO ,TRANS_SLNO ,TRANS_NAME ,TRUCK_NO ,PARTY_CODE ,CONSIGN_CODE ,MAT_VOCAB ,MAT_SLNO ,INV_NO,INV_DATE,D_TYPE,CHPTR_HEAD ,FISCAL_YEAR,INV_ISSUE ,PLACE_OF_SUPPLY ,BILL_PARTY_ADD ,CON_PARTY_ADD ,B_STATE ,B_STATE_CODE ,C_STATE ,C_STATE_CODE ,BILL_PARTY_GST_N ,CON_PARTY_GST_N ,NEGOTIATING_BRANCH ,PAYING_AUTH ,RR_NO ,RR_DATE ,TOTAL_WEIGHT ,ACC_UNIT ,PURITY ,TC_NO ,FINANCE_ARRENGE ,MILL_CODE ,DA_NO ,CONTRACT_NO ,RCD_VOUCHER_NO ,RCD_VOUCHER_DATE ,ROUT_CARD_NO ,DESPATCH_TYPE ,TAX_REVERS_CHARGE ,RLY_INV_NO ,RLY_INV_DATE ,FRT_WT_AMT ,TOTAL_PCS,P_CODE ,P_DESC ,D1 ,D2 ,D3 ,D4 ,BASE_PRICE ,PACK_PRICE ,PACK_TYPE ,QLTY_PRICE ,SEC_PRICE ,TOTAL_TDC ,UNIT_PRICE ,SY_MARGIN ,PPM_FRT ,FRT_TYPE ,RLY_ROAD_FRT ,TOTAL_RATE_UNIT ,REBATE_UNIT ,REBATE_TYPE ,TAXABLE_RATE_UNIT ,TOTAL_QTY ,TAXABLE_VALUE ,CGST_RATE ,CGST_AMT ,SGST_RATE ,SGST_AMT ,IGST_RATE ,IGST_AMT ,CESS_RATE ,CESS_AMT ,TERM_RATE ,TERM_AMT ,TOTAL_AMT ,LESS_LOAD_AMT ,TOTAL_BAG ,ADVANCE_PAID ,GST_PAID_ADV ,NET_PAY ,NOTIFICATION_TEXT ,COMM ,DIV_ADD ,INV_TYPE ,INV_RULE ,FORM_NAME,EMP_ID)values(@TCS_AMT,@BILL_NO,@INV_STATUS,@SO_NO ,@SO_DATE ,@PO_NO ,@PO_DATE ,@AMD_NO ,@AMD_DATE ,@TRANS_WO ,@TRANS_SLNO ,@TRANS_NAME ,@TRUCK_NO ,@PARTY_CODE ,@CONSIGN_CODE ,@MAT_VOCAB ,@MAT_SLNO ,@INV_NO,@INV_DATE,@D_TYPE,@CHPTR_HEAD ,@FISCAL_YEAR,@INV_ISSUE ,@PLACE_OF_SUPPLY ,@BILL_PARTY_ADD ,@CON_PARTY_ADD ,@B_STATE ,@B_STATE_CODE ,@C_STATE ,@C_STATE_CODE ,@BILL_PARTY_GST_N ,@CON_PARTY_GST_N ,@NEGOTIATING_BRANCH ,@PAYING_AUTH ,@RR_NO ,@RR_DATE ,@TOTAL_WEIGHT ,@ACC_UNIT ,@PURITY ,@TC_NO ,@FINANCE_ARRENGE ,@MILL_CODE ,@DA_NO ,@CONTRACT_NO ,@RCD_VOUCHER_NO ,@RCD_VOUCHER_DATE ,@ROUT_CARD_NO ,@DESPATCH_TYPE ,@TAX_REVERS_CHARGE ,@RLY_INV_NO ,@RLY_INV_DATE ,@FRT_WT_AMT ,@TOTAL_PCS,@P_CODE ,@P_DESC ,@D1 ,@D2 ,@D3 ,@D4 ,@BASE_PRICE ,@PACK_PRICE ,@PACK_TYPE ,@QLTY_PRICE ,@SEC_PRICE ,@TOTAL_TDC ,@UNIT_PRICE ,@SY_MARGIN ,@PPM_FRT ,@FRT_TYPE ,@RLY_ROAD_FRT ,@TOTAL_RATE_UNIT ,@REBATE_UNIT ,@REBATE_TYPE ,@TAXABLE_RATE_UNIT ,@TOTAL_QTY ,@TAXABLE_VALUE ,@CGST_RATE ,@CGST_AMT ,@SGST_RATE ,@SGST_AMT ,@IGST_RATE ,@IGST_AMT ,@CESS_RATE ,@CESS_AMT ,@TERM_RATE ,@TERM_AMT ,@TOTAL_AMT ,@LESS_LOAD_AMT ,@TOTAL_BAG ,@ADVANCE_PAID ,@GST_PAID_ADV ,@NET_PAY ,@NOTIFICATION_TEXT ,@COMM ,@DIV_ADD ,@INV_TYPE ,@INV_RULE ,@FORM_NAME,@EMP_ID)"
        Dim cmd1 As New SqlCommand(QUARY, conn_trans, myTrans)
        cmd1.Parameters.AddWithValue("@SO_NO", TextBox124.Text)
        cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@PO_NO", actual_so)
        cmd1.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(SO_ACTUAL_DATE.Day & "-" & SO_ACTUAL_DATE.Month & "-" & SO_ACTUAL_DATE.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@AMD_NO", amd_no)
        cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(amd_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@TRANS_WO", "N/A")
        cmd1.Parameters.AddWithValue("@TRANS_SLNO", "N/A")
        cmd1.Parameters.AddWithValue("@TRANS_NAME", "N/A")
        cmd1.Parameters.AddWithValue("@TRUCK_NO", "N/A")
        cmd1.Parameters.AddWithValue("@ED_COMDT", "Agreeing To Tolerate An Act")
        cmd1.Parameters.AddWithValue("@PARTY_CODE", party_code)
        cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CONSIGN_CODE)
        cmd1.Parameters.AddWithValue("@MAT_VOCAB", "N/A")
        cmd1.Parameters.AddWithValue("@MAT_SLNO", 0)
        cmd1.Parameters.AddWithValue("@INV_NO", TextBox191.Text)
        cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@CHPTR_HEAD", "999794")
        cmd1.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
        cmd1.Parameters.AddWithValue("@INV_ISSUE", Now)
        cmd1.Parameters.AddWithValue("@PLACE_OF_SUPPLY", PLACE_OF_SUPPLY)
        cmd1.Parameters.AddWithValue("@BILL_PARTY_ADD", TextBox126.Text + "," + SUPL_AT + "," + SUPL_PO + "," + SUPL_DIST + ", Pin- " + SUPL_PIN)
        cmd1.Parameters.AddWithValue("@CON_PARTY_ADD", TextBox126.Text + "," + SUPL_AT + "," + SUPL_PO + "," + SUPL_DIST + ", Pin- " + SUPL_PIN)
        cmd1.Parameters.AddWithValue("@B_STATE", supl_state)
        cmd1.Parameters.AddWithValue("@B_STATE_CODE", supl_state_code)
        cmd1.Parameters.AddWithValue("@C_STATE", supl_state)
        cmd1.Parameters.AddWithValue("@C_STATE_CODE", supl_state_code)
        cmd1.Parameters.AddWithValue("@BILL_PARTY_GST_N", SUPL_GST_NO)
        cmd1.Parameters.AddWithValue("@CON_PARTY_GST_N", SUPL_GST_NO)
        cmd1.Parameters.AddWithValue("@NEGOTIATING_BRANCH", "")
        cmd1.Parameters.AddWithValue("@PAYING_AUTH", "")
        cmd1.Parameters.AddWithValue("@RR_NO", "")
        cmd1.Parameters.AddWithValue("@RR_DATE", "")
        cmd1.Parameters.AddWithValue("@TOTAL_WEIGHT ", 0)
        cmd1.Parameters.AddWithValue("@D_TYPE", TextBox190.Text)
        cmd1.Parameters.AddWithValue("@ACC_UNIT", "Service")
        cmd1.Parameters.AddWithValue("@PURITY", CDec(0))
        cmd1.Parameters.AddWithValue("@TC_NO", "")
        cmd1.Parameters.AddWithValue("@FINANCE_ARRENGE", "N/A")
        cmd1.Parameters.AddWithValue("@MILL_CODE", "")
        cmd1.Parameters.AddWithValue("@DA_NO", "")
        cmd1.Parameters.AddWithValue("@CONTRACT_NO", "")
        cmd1.Parameters.AddWithValue("@RCD_VOUCHER_NO", "N/A")
        cmd1.Parameters.AddWithValue("@RCD_VOUCHER_DATE", "N/A")
        cmd1.Parameters.AddWithValue("@ROUT_CARD_NO", "N/A")
        cmd1.Parameters.AddWithValue("@DESPATCH_TYPE", "")
        cmd1.Parameters.AddWithValue("@TAX_REVERS_CHARGE", "YES")
        cmd1.Parameters.AddWithValue("@RLY_INV_NO", "N/A")
        cmd1.Parameters.AddWithValue("@RLY_INV_DATE", "N/A")
        cmd1.Parameters.AddWithValue("@FRT_WT_AMT", CDec(0.0))
        cmd1.Parameters.AddWithValue("@TOTAL_PCS", 0)
        cmd1.Parameters.AddWithValue("@P_CODE ", "")
        cmd1.Parameters.AddWithValue("@P_DESC", "E.M.D. FOREFEIT")
        cmd1.Parameters.AddWithValue("@D1", "")
        cmd1.Parameters.AddWithValue("@D2", "")
        cmd1.Parameters.AddWithValue("@D3", "")
        cmd1.Parameters.AddWithValue("@D4", "")
        cmd1.Parameters.AddWithValue("@PACK_TYPE", "/ Mt")
        cmd1.Parameters.AddWithValue("@FRT_TYPE", "/ Mt")
        cmd1.Parameters.AddWithValue("@REBATE_TYPE ", "/ Mt")
        cmd1.Parameters.AddWithValue("@TOTAL_QTY", CDec(1))
        cmd1.Parameters.AddWithValue("@BASE_PRICE", taxable_rate_unit)
        cmd1.Parameters.AddWithValue("@PACK_PRICE", 0)
        cmd1.Parameters.AddWithValue("@QLTY_PRICE", 0.0)
        cmd1.Parameters.AddWithValue("@SEC_PRICE", 0.0)
        cmd1.Parameters.AddWithValue("@TOTAL_TDC", 0.0)
        cmd1.Parameters.AddWithValue("@UNIT_PRICE", taxable_rate_unit)
        cmd1.Parameters.AddWithValue("@SY_MARGIN", 0.0)
        cmd1.Parameters.AddWithValue("@PPM_FRT", 0.0)
        cmd1.Parameters.AddWithValue("@RLY_ROAD_FRT", 0.0)
        cmd1.Parameters.AddWithValue("@TOTAL_RATE_UNIT", taxable_rate_unit)
        cmd1.Parameters.AddWithValue("@REBATE_UNIT", 0.0)
        cmd1.Parameters.AddWithValue("@TAXABLE_RATE_UNIT", taxable_rate_unit)
        cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", total_taxable_value)
        cmd1.Parameters.AddWithValue("@CGST_RATE", CGST_RATE)
        cmd1.Parameters.AddWithValue("@CGST_AMT", cgst_amt)
        cmd1.Parameters.AddWithValue("@SGST_RATE", SGST_RATE)
        cmd1.Parameters.AddWithValue("@SGST_AMT", sgst_amt)
        cmd1.Parameters.AddWithValue("@IGST_RATE", IGST_RATE)
        cmd1.Parameters.AddWithValue("@IGST_AMT", igst_amt)
        cmd1.Parameters.AddWithValue("@CESS_RATE", 0)
        cmd1.Parameters.AddWithValue("@CESS_AMT", cess_amt)
        cmd1.Parameters.AddWithValue("@TERM_RATE", 0)
        cmd1.Parameters.AddWithValue("@TERM_AMT", 0)
        cmd1.Parameters.AddWithValue("@TOTAL_AMT", net_pay)
        cmd1.Parameters.AddWithValue("@LESS_LOAD_AMT", 0.0)
        cmd1.Parameters.AddWithValue("@ADVANCE_PAID", 0)
        cmd1.Parameters.AddWithValue("@GST_PAID_ADV", cgst_amt + sgst_amt + igst_amt + cess_amt)
        cmd1.Parameters.AddWithValue("@NET_PAY", net_pay)
        cmd1.Parameters.AddWithValue("@COMM", COMM)
        cmd1.Parameters.AddWithValue("@DIV_ADD", DIVISION)
        cmd1.Parameters.AddWithValue("@INV_TYPE", inv_type_new & " " & inv_type1_new)
        cmd1.Parameters.AddWithValue("@INV_RULE", inv_rule_new)
        cmd1.Parameters.AddWithValue("@FORM_NAME", "")
        cmd1.Parameters.AddWithValue("@TOTAL_BAG", 0)
        cmd1.Parameters.AddWithValue("@NOTIFICATION_TEXT", TextBox64.Text)
        cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
        cmd1.Parameters.AddWithValue("@INV_STATUS", "ACTIVE")
        cmd1.Parameters.AddWithValue("@BILL_NO", DropDownList30.SelectedValue)
        cmd1.Parameters.AddWithValue("@TCS_AMT", 0)
        cmd1.ExecuteReader()
        cmd1.Dispose()


        ''insert inv_print
        QUARY = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
        Dim scmd As New SqlCommand(QUARY, conn_trans, myTrans)
        scmd.Parameters.AddWithValue("@INV_NO", inv_for_new & CStr(DESPATCH_TYPE) & TextBox191.Text)
        scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
        scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
        scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
        scmd.Parameters.AddWithValue("@F_YEAR", STR1)
        scmd.ExecuteReader()
        scmd.Dispose()


    End Sub
End Class