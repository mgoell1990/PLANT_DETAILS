Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net
Public Class FCM_Service
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
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
    Dim goAheadFlag As Boolean = True
    Dim taxable_price, cgst_amt, sgst_amt, igst_amt, cess_amt, net_pay As New Decimal(0)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox125.Attributes.Add(“readonly”, “readonly”)
        TextBox126.Attributes.Add(“readonly”, “readonly”)
        TextBox188.Attributes.Add(“readonly”, “readonly”)
        TextBox189.Attributes.Add(“readonly”, “readonly”)
        TextBox182.Text = "1"
        If Not IsPostBack Then


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList31_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList31.SelectedIndexChanged
        If DropDownList31.SelectedValue = "Select" Then
            DropDownList31.Focus()
            Return
        ElseIf DropDownList31.SelectedValue = "Unregistered Party" Then
            Label402.Visible = False
            ''DropDownList26.Visible = False
            Return
        ElseIf DropDownList31.SelectedValue = "Registered Party" Then
            'Label402.Visible = True
            'DropDownList26.Visible = True
            'DropDownList26.Focus()
            Return
        End If
    End Sub

    Protected Sub BINDGRID()
        GridView2.DataSource = DirectCast(ViewState("RCM"), DataTable)
        GridView2.DataBind()
    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim dt2 As New DataTable()
        dt2.Columns.AddRange(New DataColumn(16) {New DataColumn("MB No"), New DataColumn("Wo. SLNO"), New DataColumn("Work Desc."), New DataColumn("A/U"), New DataColumn("SAC Code"), New DataColumn("Unit Rate"), New DataColumn("Work Qty"), New DataColumn("Taxable Value"), New DataColumn("CGST"), New DataColumn("CGST Amt."), New DataColumn("SGST"), New DataColumn("SGST Amt."), New DataColumn("IGST"), New DataColumn("IGST Amt."), New DataColumn("CESS"), New DataColumn("CESS Amt."), New DataColumn("Total value of Goods")})
        ViewState("RCM") = dt2
        Me.BINDGRID()
        If DropDownList31.SelectedValue = "Select" Then
            DropDownList31.Focus()
            Return
        ElseIf DropDownList31.SelectedValue = "Unregistered Party" Then

            'TextBox124.ReadOnly = False
            'TextBox124.ForeColor = Drawing.Color.Black
            'TextBox124.BackColor = Drawing.Color.White

            'TextBox125.ReadOnly = False
            'TextBox125.ForeColor = Drawing.Color.Black
            'TextBox125.BackColor = Drawing.Color.White

            'TextBox126.ReadOnly = False
            'TextBox126.ForeColor = Drawing.Color.Black
            'TextBox126.BackColor = Drawing.Color.White

            TextBox188.ReadOnly = False
            TextBox188.ForeColor = Drawing.Color.Black
            TextBox188.BackColor = Drawing.Color.White
            TextBox188.Text = ""

            TextBox189.ReadOnly = False
            TextBox189.ForeColor = Drawing.Color.Black
            TextBox189.BackColor = Drawing.Color.White
            TextBox189.Text = ""
            Panel2.Visible = True
            Panel8.Visible = False
            Panel1.Visible = True


            DropDownList32.Items.Clear()
            DropDownList32.Items.Add("Select")
            DropDownList32.Items.Add("94029090")

            DropDownList32.SelectedValue = "Select"
        ElseIf DropDownList31.SelectedValue = "Registered Party" Then
            Panel1.Visible = False

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

            conn.Open()
            count = 0
            Dim dt1 As New DataTable()
            da = New SqlDataAdapter("SELECT SO_NO  FROM ORDER_DETAILS  WHERE SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'", conn)
            count = da.Fill(dt1)
            conn.Close()
            If count = 0 Then
                DropDownList26.Text = ""
                DropDownList26.Focus()
                Return
            End If

            ''SEARCH SALE ORDER VENDER DETAILS
            conn.Open()
            mycommand.CommandText = "select SUPL_ID,SUPL_NAME,SUPL_STATE_CODE,SUPL_STATE from SUPL join order_details on order_details.PARTY_CODE=SUPL.SUPL_ID where order_details.so_no='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim & "'"
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
            ''TextBox124.Text = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1).Trim

            Panel2.Visible = True
            Panel8.Visible = False
        End If
    End Sub

    Protected Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click

        If (DropDownList26.Text = "") Then
            Label308.Visible = True
            Label308.Text = "Please select Party first"
            DropDownList26.Focus()
            Return
        ElseIf TextBox3.Text = "" Then
            TextBox3.Focus()
            Return
        ElseIf IsDate(TextBox3.Text) = False Then
            TextBox3.Text = ""
            TextBox3.Focus()
            Return
        ElseIf (DropDownList32.Text = "Select") Then
            Label308.Visible = True
            Label308.Text = "Please select HSN Code"
            DropDownList32.Focus()
            Return
        ElseIf (DropDownList33.Text = "Select") Then
            Label308.Visible = True
            Label308.Text = "Please select Invoice Type."
            DropDownList33.Focus()
            Return
        ElseIf (TextBox183.Text = "") Then
            Label308.Visible = True
            Label308.Text = "Please enter taxable unit price"
            TextBox183.Focus()
            Return
        End If

        Label308.Visible = False
        Label308.Text = ""
        'If Panel1.Visible = True Then
        '    If DropDownList32.SelectedValue = "Select" Then
        '        DropDownList32.Focus()
        '        Return
        '    ElseIf TextBox182.Text = "" Then
        '        TextBox182.Focus()
        '        Return
        '    ElseIf CDec(TextBox182.Text) < 0 Then
        '        TextBox182.Text = ""
        '        TextBox182.Focus()
        '        Return
        '    ElseIf TextBox183.Text = "" Then
        '        TextBox183.Focus()
        '        Return
        '    ElseIf CDec(TextBox183.Text) < 0 Then
        '        TextBox183.Text = ""
        '        TextBox183.Focus()
        '        Return
        '    ElseIf TextBox184.Text = "" Then
        '        TextBox184.Focus()
        '        Return
        '    ElseIf TextBox185.Text = "" Then
        '        TextBox185.Focus()
        '        Return
        '    ElseIf TextBox186.Text = "" Then
        '        TextBox186.Focus()
        '        Return
        '    ElseIf TextBox187.Text = "" Then
        '        TextBox187.Focus()
        '        Return
        '    End If
        'End If

        'CALCULATION
        Dim TAX_VAL, TOTAL_VAL, CGST_VAL, SGST_VAL, IGST_VAL, CESS_VAL, UNIT_PRICE, MAT_QTY, CGST, SGST, IGST, CESS As New Decimal(0)
        MAT_QTY = FormatNumber(CDec(TextBox182.Text), 2)
        UNIT_PRICE = FormatNumber(CDec(TextBox183.Text), 2)
        TAX_VAL = FormatNumber(MAT_QTY * UNIT_PRICE, 2)
        CGST = FormatNumber(CDec(TextBox184.Text), 2)
        SGST = FormatNumber(CDec(TextBox185.Text), 2)
        IGST = FormatNumber(CDec(TextBox186.Text), 2)
        CESS = FormatNumber(CDec(TextBox187.Text), 2)
        CGST_VAL = FormatNumber((TAX_VAL * CGST) / 100, 2)
        SGST_VAL = FormatNumber((TAX_VAL * SGST) / 100, 2)
        IGST_VAL = FormatNumber((TAX_VAL * IGST) / 100, 2)
        CESS_VAL = FormatNumber((TAX_VAL * CESS) / 100, 2)
        TOTAL_VAL = FormatNumber(TAX_VAL + CGST_VAL + SGST_VAL + IGST_VAL + CESS_VAL, 2)

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select '" & TextBox125.Text & "' as PartyCode, '" & TextBox126.Text & "' as PartyName, '" & DropDownList32.SelectedValue & "' as SACCode, '" & UNIT_PRICE & "' AS UNIT_PRICE, '" & MAT_QTY & "' as work_qty, '" & TAX_VAL & "' As prov_amt, '" & CGST & "' AS CGST, '" & CGST_VAL & "' as cgst_liab, '" & SGST & "' AS SGST, '" & SGST_VAL & "' as sgst_liab, '" & IGST & "' AS IGST, '" & IGST_VAL & "' as igst_liab, '" & CESS & "' AS CESS, '" & CESS_VAL & "' as cess_liab,'" & TOTAL_VAL & "' as TOTAL_VAL", conn)
        da.Fill(dt)
        conn.Close()
        GridView2.DataSource = dt
        GridView2.DataBind()


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
                Else
                    Label308.Text = ""
                End If
                working_date = CDate(TextBox3.Text)
                'working_date = Today.Date

                Dim DESPATCH_TYPE As Integer = 1


                'invoice type for GST
                Dim inv_type_new, inv_rule_new, inv_for_new, inv_type1_new As String
                'Dim inv_type, inv_rule, inv_for, inv_type1, inv_type_new, inv_rule_new, inv_for_new, inv_type1_new As String

                'inv_type = "Tax Invoice"
                'inv_type1 = "(For Services under Reverse Charge)"
                'inv_rule = "(In case of Supplies from unregistered Suppliers under Section 31(3)(f)of CGST Act 2017 read with Rule 1 of Tax Invoice ,Credit and Debit Note Rules)"
                'inv_for = "RC15"

                'SAVE RCM_INV
                Dim supl_state, supl_state_code, gst_no As New String("")
                Dim SUPL_INV As String = "NA"

                supl_state = TextBox189.Text
                supl_state_code = TextBox188.Text

                Dim gst_code, my_gst_code, COMM, DIVISION As New String("")

                conn.Open()
                mycommand.CommandText = "SELECT * FROM SUPL WHERE SUPL_ID='" & TextBox125.Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()

                    If IsDBNull(dr.Item("SUPL_GST_NO")) Then
                        gst_code = "N/A"
                    Else
                        gst_code = dr.Item("SUPL_GST_NO")

                    End If
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

                'invoice type for GST
                inv_type_new = "Tax Invoice"
                inv_type1_new = "(For Services under Forward Charge)"
                inv_rule_new = "(In case of Supplies from unregistered Suppliers under Section 31(3)(f)of CGST Act 2017 read with Rule 1 of Tax Invoice ,Credit and Debit Note Rules)"
                inv_for_new = "OS15"

                generateFCM(DESPATCH_TYPE, inv_type_new, inv_type1_new, inv_rule_new, inv_for_new, SUPL_INV, STR1, COMM, DIVISION, supl_state, supl_state_code)


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
                    If (gst_code = "" Or gst_code = "N/A") Then

                        Dim logicClassObj = New EinvoiceLogicClassEY

                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox190.Text + TextBox191.Text, TextBox125.Text)
                        If (AuthErrorData.Item(0).status = "1") Then
                            Dim authIdToken As String = AuthErrorData.Item(0).Idtoken
                            Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateB2CInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox190.Text + TextBox191.Text, TextBox125.Text, TextBox125.Text, "YES", "N", "INV")

                            If (EinvErrorData.Item(0).status = "1") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox190.Text + TextBox191.Text, TextBox125.Text, TextBox125.Text, "YES", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                '================SENDING DATA TO EY PORTAL START==================='

                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox190.Text + TextBox191.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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
                                Label308.Text = "There is some response error in invoice generation."

                            ElseIf (EinvErrorData.Item(0).status = "3") Then
                                Label31.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorfield
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errordesc
                                goAheadFlag = False
                                Label308.Text = "There is some response error in invoice generation."
                            ElseIf (EinvErrorData.Item(0).status = "4") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox190.Text + TextBox191.Text, TextBox125.Text, TextBox125.Text, "YES", "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                '================SENDING DATA TO EY PORTAL START==================='

                                Label31.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage
                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox190.Text + TextBox191.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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



                    Else
                        Dim logicClassObj = New EinvoiceLogicClassEY
                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox190.Text + TextBox191.Text, TextBox125.Text)
                        If (AuthErrorData.Item(0).status = "1") Then
                            Dim authIdToken As String = AuthErrorData.Item(0).Idtoken
                            Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY) = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox190.Text + TextBox191.Text, TextBox125.Text, TextBox125.Text, "YES", "N", "INV")

                            If (EinvErrorData.Item(0).status = "1") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox190.Text + TextBox191.Text, TextBox125.Text, TextBox125.Text, "YES", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                '================SENDING DATA TO EY PORTAL START==================='

                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox190.Text + TextBox191.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox190.Text + TextBox191.Text, TextBox125.Text, TextBox125.Text, "YES", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "INV")
                                '================SENDING DATA TO EY PORTAL START==================='

                                Label31.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage
                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set IRN_NO ='" & EinvErrorData.Item(0).IRN & "',QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & TextBox190.Text + TextBox191.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
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


    Protected Sub generateFCM(DESPATCH_TYPE As String, inv_type_new As String, inv_type1_new As String, inv_rule_new As String, inv_for_new As String, SUPL_INV As String, STR1 As String, COMM As String, DIVISION As String, supl_state As String, supl_state_code As String)


        conn.Open()
        Dim inv_no_ld_pen As String = ""
        Dim mc_c_ld_pen As New SqlCommand
        mc_c_ld_pen.CommandText = "SELECT (CASE WHEN MAX(inv_no) IS NULL THEN 0 ELSE MAX(inv_no) END) as inv_no FROM DESPATCH WITH(NOLOCK) WHERE D_TYPE LIKE'" & inv_for_new + DESPATCH_TYPE & "%' AND FISCAL_YEAR =" & STR1
        mc_c_ld_pen.Connection = conn
        dr = mc_c_ld_pen.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_no_ld_pen = dr.Item("inv_no")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim prefixFY = Left(STR1, 2)

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

        actual_so = "NA"
        so_date = Date.ParseExact(working_date, "dd-MM-yyyy", provider)
        SO_ACTUAL_DATE = Date.ParseExact(working_date, "dd-MM-yyyy", provider)
        PLACE_OF_SUPPLY = "NA"

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
        mycommand.CommandText = "SELECT SUPL_AT,SUPL_PO, SUPL_DIST, SUPL_PIN, SUPL_GST_NO FROM SUPL WHERE SUPL_ID='" & TextBox125.Text & "'"
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

        Dim CGST_RATE, SGST_RATE, IGST_RATE As New Decimal(0)
        For i = 0 To GridView2.Rows.Count - 1

            taxable_price = taxable_price + CDec(GridView2.Rows(i).Cells(5).Text)
            cgst_amt = cgst_amt + CDec(GridView2.Rows(i).Cells(7).Text)
            sgst_amt = sgst_amt + CDec(GridView2.Rows(i).Cells(9).Text)
            igst_amt = igst_amt + CDec(GridView2.Rows(i).Cells(11).Text)
            cess_amt = cess_amt + CDec(GridView2.Rows(i).Cells(13).Text)
            net_pay = taxable_price + cgst_amt + sgst_amt + igst_amt + cess_amt


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
        cmd1.Parameters.AddWithValue("@SO_NO", "NA")
        cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(so_date.Day & "-" & so_date.Month & "-" & so_date.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@PO_NO", actual_so)
        cmd1.Parameters.AddWithValue("@PO_DATE", Date.ParseExact(CDate(SO_ACTUAL_DATE.Day & "-" & SO_ACTUAL_DATE.Month & "-" & SO_ACTUAL_DATE.Year), "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@AMD_NO", amd_no)
        cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(amd_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@TRANS_WO", "N/A")
        cmd1.Parameters.AddWithValue("@TRANS_SLNO", "N/A")
        cmd1.Parameters.AddWithValue("@TRANS_NAME", "N/A")
        cmd1.Parameters.AddWithValue("@TRUCK_NO", "N/A")
        cmd1.Parameters.AddWithValue("@ED_COMDT", "")
        cmd1.Parameters.AddWithValue("@PARTY_CODE", party_code)
        cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CONSIGN_CODE)
        cmd1.Parameters.AddWithValue("@MAT_VOCAB", "N/A")
        cmd1.Parameters.AddWithValue("@MAT_SLNO", 0)
        cmd1.Parameters.AddWithValue("@INV_NO", TextBox191.Text)
        cmd1.Parameters.AddWithValue("@INV_DATE", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@CHPTR_HEAD", DropDownList32.Text)
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
        cmd1.Parameters.AddWithValue("@P_DESC", "FCM Invoice for Service B2C")
        cmd1.Parameters.AddWithValue("@D1", "")
        cmd1.Parameters.AddWithValue("@D2", "")
        cmd1.Parameters.AddWithValue("@D3", "")
        cmd1.Parameters.AddWithValue("@D4", "")
        cmd1.Parameters.AddWithValue("@PACK_TYPE", "/ Mt")
        cmd1.Parameters.AddWithValue("@FRT_TYPE", "/ Mt")
        cmd1.Parameters.AddWithValue("@REBATE_TYPE ", "/ Mt")
        cmd1.Parameters.AddWithValue("@TOTAL_QTY", CDec(1))
        cmd1.Parameters.AddWithValue("@BASE_PRICE", 0)
        cmd1.Parameters.AddWithValue("@PACK_PRICE", 0)
        cmd1.Parameters.AddWithValue("@QLTY_PRICE", 0.0)
        cmd1.Parameters.AddWithValue("@SEC_PRICE", 0.0)
        cmd1.Parameters.AddWithValue("@TOTAL_TDC", 0.0)
        cmd1.Parameters.AddWithValue("@UNIT_PRICE", 0)
        cmd1.Parameters.AddWithValue("@SY_MARGIN", 0.0)
        cmd1.Parameters.AddWithValue("@PPM_FRT", 0.0)
        cmd1.Parameters.AddWithValue("@RLY_ROAD_FRT", 0.0)
        cmd1.Parameters.AddWithValue("@TOTAL_RATE_UNIT", 0.0)
        cmd1.Parameters.AddWithValue("@REBATE_UNIT", 0.0)
        cmd1.Parameters.AddWithValue("@TAXABLE_RATE_UNIT", 0)
        cmd1.Parameters.AddWithValue("@TAXABLE_VALUE", taxable_price)
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
        cmd1.Parameters.AddWithValue("@TOTAL_AMT", taxable_price + cgst_amt + sgst_amt + igst_amt + cess_amt)
        cmd1.Parameters.AddWithValue("@LESS_LOAD_AMT", 0.0)
        cmd1.Parameters.AddWithValue("@ADVANCE_PAID", 0)
        cmd1.Parameters.AddWithValue("@GST_PAID_ADV", cgst_amt + sgst_amt + igst_amt + cess_amt)
        cmd1.Parameters.AddWithValue("@NET_PAY", taxable_price)
        cmd1.Parameters.AddWithValue("@COMM", COMM)
        cmd1.Parameters.AddWithValue("@DIV_ADD", DIVISION)
        cmd1.Parameters.AddWithValue("@INV_TYPE", inv_type_new & " " & inv_type1_new)
        cmd1.Parameters.AddWithValue("@INV_RULE", inv_rule_new)
        cmd1.Parameters.AddWithValue("@FORM_NAME", "")
        cmd1.Parameters.AddWithValue("@TOTAL_BAG", 0)
        cmd1.Parameters.AddWithValue("@NOTIFICATION_TEXT", "FCM voucher for Service")
        cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
        cmd1.Parameters.AddWithValue("@INV_STATUS", "ACTIVE")
        cmd1.Parameters.AddWithValue("@BILL_NO", "NA")
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


        ''Update RCM_INV table
        'conn.Open()
        'QUARY = "update RCM_INV set OS_INV_NO='" & TextBox190.Text + TextBox191.Text & "' where (D_TYPE+INV_NO)='" & TextBox177.Text + TextBox65.Text & "'"
        'Dim sc1 As New SqlCommand(QUARY, conn)
        'sc1.ExecuteReader()
        'sc1.Dispose()
        'conn.Close()

        'conn.Open()
        'QUARY = "update RCM_INV set OS_INV_NO='" & TextBox190.Text + TextBox191.Text & "' where (D_TYPE+INV_NO)='" & TextBox1.Text + TextBox2.Text & "'"
        'Dim sc2 As New SqlCommand(QUARY, conn)
        'sc2.ExecuteReader()
        'sc2.Dispose()
        'conn.Close()

        'update RCM_INV set RCM_TYPE='Tax Invoice', OS_INV_NO='OS0002' where (D_TYPE+INV_NO)='PV1510000001'


    End Sub
End Class