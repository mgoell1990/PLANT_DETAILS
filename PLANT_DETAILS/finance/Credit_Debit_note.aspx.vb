Imports System.Globalization
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports System.IO
Imports System.Net
Imports ZXing
Imports System.Drawing
Imports System.Drawing.Imaging
Public Class Credit_Debit_note1
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

    Dim goAheadFlag As Boolean = True
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList3.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList3.DataValueField = "FY"
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "Select")
            conn.Close()
        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If DropDownList4.Text = "" Then
            Label3.Text = "Please enter valid Invoice No."
            Return
        ElseIf (CDec(TextBox2.Text) = 0) Then
            Label3.Text = "Please enter taxable value"
            Return
        End If
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from DESPATCH join dater on DESPATCH.PARTY_CODE=dater.d_code where D_TYPE+ CONVERT(VARCHAR(15), INV_NO) ='" & DropDownList4.Text & "' and FISCAL_YEAR =" & DropDownList3.Text, conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()

        GridView1.Rows(0).Cells(0).Text = DropDownList4.Text

        GridView1.Rows(0).Cells(11).Text = TextBox2.Text
        If ((CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(12).Text)) / 100 > 0) Then
            GridView1.Rows(0).Cells(13).Text = Format((CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(12).Text)) / 100, "#.00")
        Else
            GridView1.Rows(0).Cells(13).Text = (CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(12).Text)) / 100
        End If

        If ((CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(14).Text)) / 100 > 0) Then
            GridView1.Rows(0).Cells(15).Text = Format((CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(14).Text)) / 100, "#.00")
        Else
            GridView1.Rows(0).Cells(15).Text = (CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(14).Text)) / 100
        End If

        If ((CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(16).Text)) / 100 > 0) Then
            GridView1.Rows(0).Cells(17).Text = Format((CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(16).Text)) / 100, "#.00")
        Else
            GridView1.Rows(0).Cells(17).Text = (CDec(TextBox2.Text) * CDec(GridView1.Rows(0).Cells(16).Text)) / 100
        End If



        GridView1.Rows(0).Cells(18).Text = CDec(GridView1.Rows(0).Cells(11).Text) + CDec(GridView1.Rows(0).Cells(13).Text) + CDec(GridView1.Rows(0).Cells(15).Text) + CDec(GridView1.Rows(0).Cells(17).Text)


    End Sub


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim working_date As Date
        Dim mc_c1 As New SqlCommand

        'conn.Open()
        'mc_c1.CommandText = "select * from DESPATCH where D_TYPE+ CONVERT(VARCHAR(15), INV_NO) ='" & DropDownList4.Text & "' and FISCAL_YEAR =" & DropDownList3.Text
        'mc_c1.Connection = conn
        'dr = mc_c1.ExecuteReader
        'If dr.HasRows Then
        '    dr.Read()
        '    working_date = dr.Item("INV_DATE")
        '    dr.Close()
        'Else
        '    dr.Close()
        'End If
        'conn.Close()

        'working_date = CDate(txtInvoiceDate.Text)
        working_date = DateTime.Now

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                If GridView1.Rows.Count = 0 Then
                    Return
                End If

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

                Dim inv_type As String = ""
                If (Label7.Text = "Credit Note") Then
                    inv_type = "CN"
                ElseIf (Label7.Text = "Debit Note") Then
                    inv_type = "DN"
                End If

                conn.Open()
                Dim inv_no As String = ""
                Dim mc_c As New SqlCommand
                mc_c.CommandText = "SELECT (CASE WHEN COUNT(DISTINCT DEBIT_CREDIT_NO) IS NULL THEN 0 ELSE COUNT(DISTINCT DEBIT_CREDIT_NO) END) as inv_no FROM CN_DN_DETAILS  WHERE DEBIT_CREDIT_NO LIKE '" & inv_type & "%' AND FISCAL_YEAR =" & STR1
                mc_c.Connection = conn
                dr = mc_c.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    inv_no = dr.Item("inv_no")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()

                ''Display invoice no

                If CInt(inv_no) = 0 Then
                    TextBox65.Text = inv_type + STR1 + "0000001"
                    TextBox65.ReadOnly = True
                Else
                    str = CInt(inv_no) + 1
                    If str.Length = 1 Then
                        str = "000000" & CInt(inv_no) + 1
                    ElseIf str.Length = 2 Then
                        str = "00000" & CInt(inv_no) + 1
                    ElseIf str.Length = 3 Then
                        str = "0000" & CInt(inv_no) + 1
                    ElseIf str.Length = 4 Then
                        str = "000" & CInt(inv_no) + 1
                    ElseIf str.Length = 5 Then
                        str = "00" & CInt(inv_no) + 1
                    ElseIf str.Length = 6 Then
                        str = "0" & CInt(inv_no) + 1
                    ElseIf str.Length = 7 Then
                        str = CInt(inv_no) + 1
                    End If
                    TextBox65.Text = inv_type + STR1 + str
                    TextBox65.ReadOnly = True
                End If

                Dim name As String = ""
                Dim checkedFlag As Boolean
                checkedFlag = False
                Dim QUARY As New String("")

                ''insert inv_print

                QUARY = "Insert Into CN_DN_DETAILS(ORIGINAL_INVOICE_DATE,INV_TYPE,DEBIT_CREDIT_NO,DEBIT_CREDIT_DATE,FISCAL_YEAR,INVOICE_NO,ORDER_NO,ORDER_DATE,
                        ACTUAL_ORDER_NO,ACTUAL_ORDER_DATE,PARTY_CODE,PARTY_NAME,ORDER_SL_NO,MAT_CODE,MAT_NAME,TAXABLE_VALUE,CGST_RATE,CGST_AMT,
                        SGST_RATE,SGST_AMT,IGST_RATE,IGST_AMT,TOTAL_VALUE,NOTIFICATION)values(@ORIGINAL_INVOICE_DATE,@INV_TYPE,@DEBIT_CREDIT_NO,@DEBIT_CREDIT_DATE,@FISCAL_YEAR,@INVOICE_NO,@ORDER_NO,@ORDER_DATE,
                        @ACTUAL_ORDER_NO,@ACTUAL_ORDER_DATE,@PARTY_CODE,@PARTY_NAME,@ORDER_SL_NO,@MAT_CODE,@MAT_NAME,@TAXABLE_VALUE,@CGST_RATE,@CGST_AMT,
                        @SGST_RATE,@SGST_AMT,@IGST_RATE,@IGST_AMT,@TOTAL_VALUE,@NOTIFICATION)"
                Dim scmd As New SqlCommand(QUARY, conn_trans, myTrans)
                scmd.Parameters.AddWithValue("@INV_TYPE", Label7.Text)
                scmd.Parameters.AddWithValue("@DEBIT_CREDIT_NO", TextBox65.Text)
                scmd.Parameters.AddWithValue("@DEBIT_CREDIT_DATE", DateTime.ParseExact(working_date.ToString("dd-MM-yyyy"), "dd-MM-yyyy", CultureInfo.InvariantCulture))
                scmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                scmd.Parameters.AddWithValue("@INVOICE_NO", GridView1.Rows(0).Cells(0).Text)
                scmd.Parameters.AddWithValue("@ORIGINAL_INVOICE_DATE", Date.ParseExact(CDate(txtInvoiceDate.Text), "dd-MM-yyyy", provider))
                scmd.Parameters.AddWithValue("@ORDER_NO", GridView1.Rows(0).Cells(1).Text)
                scmd.Parameters.AddWithValue("@ORDER_DATE", DateTime.ParseExact(GridView1.Rows(0).Cells(2).Text, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                scmd.Parameters.AddWithValue("@ACTUAL_ORDER_NO", GridView1.Rows(0).Cells(3).Text)
                scmd.Parameters.AddWithValue("@ACTUAL_ORDER_DATE", DateTime.ParseExact(GridView1.Rows(0).Cells(4).Text, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                scmd.Parameters.AddWithValue("@PARTY_CODE", GridView1.Rows(0).Cells(5).Text)
                scmd.Parameters.AddWithValue("@PARTY_NAME", GridView1.Rows(0).Cells(7).Text)
                scmd.Parameters.AddWithValue("@ORDER_SL_NO", GridView1.Rows(0).Cells(8).Text)
                scmd.Parameters.AddWithValue("@MAT_CODE", GridView1.Rows(0).Cells(9).Text)
                scmd.Parameters.AddWithValue("@MAT_NAME", GridView1.Rows(0).Cells(10).Text)
                scmd.Parameters.AddWithValue("@TAXABLE_VALUE", GridView1.Rows(0).Cells(11).Text)
                scmd.Parameters.AddWithValue("@CGST_RATE", GridView1.Rows(0).Cells(12).Text)
                scmd.Parameters.AddWithValue("@CGST_AMT", GridView1.Rows(0).Cells(13).Text)
                scmd.Parameters.AddWithValue("@SGST_RATE", GridView1.Rows(0).Cells(14).Text)
                scmd.Parameters.AddWithValue("@SGST_AMT", GridView1.Rows(0).Cells(15).Text)
                scmd.Parameters.AddWithValue("@IGST_RATE", GridView1.Rows(0).Cells(16).Text)
                scmd.Parameters.AddWithValue("@IGST_AMT", GridView1.Rows(0).Cells(17).Text)
                scmd.Parameters.AddWithValue("@TOTAL_VALUE", GridView1.Rows(0).Cells(18).Text)
                scmd.Parameters.AddWithValue("@NOTIFICATION", TextBox66.Text)



                scmd.ExecuteReader()
                scmd.Dispose()


                ''insert inv_print

                QUARY = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
                Dim cmd As New SqlCommand(QUARY, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@INV_NO", TextBox65.Text)
                cmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
                cmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
                cmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
                cmd.Parameters.AddWithValue("@F_YEAR", STR1)
                cmd.ExecuteReader()
                cmd.Dispose()


                Dim gst_code, my_gst_code, COMM As New String("")
                conn.Open()
                mycommand.CommandText = "select * from comp_profile"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    my_gst_code = dr.Item("c_gst_no")
                    dr.Close()
                End If
                conn.Close()

                If (Left(GridView1.Rows(0).Cells(5).Text, 1) = "S") Then
                    conn.Open()
                    mycommand.CommandText = "select * from supl where SUPL_ID ='" & GridView1.Rows(0).Cells(5).Text & "'"
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
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                Else
                    conn.Open()
                    mycommand.CommandText = "select * from dater where D_CODE ='" & GridView1.Rows(0).Cells(5).Text & "'"
                    mycommand.Connection = conn
                    dr = mycommand.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        If IsDBNull(dr.Item("GST_CODE")) Then
                            gst_code = "N/A"
                        Else
                            gst_code = dr.Item("GST_CODE")

                        End If

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                End If


                ''''''''''''''''''''''''''''''''''''''''''''''''''
                ''===========================Generate E-Invoice Through EY Start=======================''
                Dim isServiceFlag As String
                If (Label320.Text = "Service") Then
                    isServiceFlag = "YES"
                Else
                    isServiceFlag = "NO"
                End If

                If gst_code = my_gst_code Then
                    'E-Invoice is not required
                Else
                    If (gst_code = "" Or gst_code = "N/A") Then

                        Dim logicClassObj = New EinvoiceLogicClassEY

                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox65.Text, "")
                        If (AuthErrorData.Item(0).status = "1") Then

                            Dim authIdToken As String = AuthErrorData.Item(0).Idtoken
                            Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY)
                            If (Label7.Text = "Credit Note") Then
                                EinvErrorData = logicClassObj.GenerateB2CInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "CR")
                            Else
                                EinvErrorData = logicClassObj.GenerateB2CInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "DR")
                            End If


                            If (EinvErrorData.Item(0).status = "1") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result
                                If (Label7.Text = "Credit Note") Then
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "CR")
                                Else
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "DR")
                                End If

                                '================SENDING DATA TO EY PORTAL START==================='

                                Dim sqlQuery As String = ""
                                sqlQuery = "update CN_DN_DETAILS set irn_no ='" & EinvErrorData.Item(0).IRN & "', QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where DEBIT_CREDIT_NO  ='" & TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                                Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                                despatch.ExecuteReader()
                                despatch.Dispose()
                                goAheadFlag = True

                            ElseIf (EinvErrorData.Item(0).status = "2") Then
                                Label3.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorCode
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                                goAheadFlag = False
                                Label3.Text = "There is some response error in invoice generation."

                            ElseIf (EinvErrorData.Item(0).status = "3") Then
                                Label3.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorfield
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errordesc
                                goAheadFlag = False
                                Label3.Text = "There is some response error in invoice generation."
                            ElseIf (EinvErrorData.Item(0).status = "4") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN
                                Label3.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result
                                If (Label7.Text = "Credit Note") Then
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "CR")
                                Else
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "NO", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "DR")
                                End If

                                '================SENDING DATA TO EY PORTAL START==================='

                                Dim sqlQuery As String = ""
                                sqlQuery = "update CN_DN_DETAILS set irn_no ='" & EinvErrorData.Item(0).IRN & "', QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result.ToString() & "' where DEBIT_CREDIT_NO  ='" & TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                                Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                                despatch.ExecuteReader()
                                despatch.Dispose()
                                goAheadFlag = True

                                'Label308.Text = "There is error in E-way bill generation, please generate E-way bill alone with above IRN."
                            End If

                        ElseIf (AuthErrorData.Item(0).status = "2") Then

                            Label3.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = AuthErrorData.Item(0).errorCode
                            txtEinvoiceErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                            goAheadFlag = False
                            Label3.Text = "There is some response error in E-invoice Authentication."
                        Else
                            goAheadFlag = False
                            Label3.Text = "There is some response error in E-invoice Authentication."
                        End If

                    Else
                        Dim logicClassObj = New EinvoiceLogicClassEY
                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(TextBox65.Text, "")
                        If (AuthErrorData.Item(0).status = "1") Then


                            Dim authIdToken As String = AuthErrorData.Item(0).Idtoken
                            Dim EinvErrorData As List(Of EinvoiceErrorDetailsClassEY)

                            'Dim result
                            'If (Label7.Text = "Credit Note") Then
                            '    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, "NO", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "CR")
                            'Else
                            '    result = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, "NO", "N", "YES", Date.ParseExact(working_date, "dd-MM-yyyy", provider), "DR")
                            'End If

                            If (Label7.Text = "Credit Note") Then
                                'EinvErrorData = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "CR")
                                EinvErrorData = logicClassObj.GenerateEInvoiceCNDN(AuthErrorData.Item(0).Idtoken, Guid.NewGuid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "CR", Date.ParseExact(CDate(txtInvoiceDate.Text), "dd-MM-yyyy", provider))
                            Else
                                'EinvErrorData = logicClassObj.GenerateEInvoice(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "DR")
                                EinvErrorData = logicClassObj.GenerateEInvoiceCNDN(AuthErrorData.Item(0).Idtoken, Guid.NewGuid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "DR", Date.ParseExact(CDate(txtInvoiceDate.Text), "dd-MM-yyyy", provider))
                            End If


                            If (EinvErrorData.Item(0).status = "1") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result1
                                If (Label7.Text = "Credit Note") Then
                                    result1 = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, Guid.NewGuid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "YES", Date.ParseExact(CDate(txtInvoiceDate.Text), "dd-MM-yyyy", provider), "CR")
                                Else
                                    result1 = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, Guid.NewGuid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "YES", Date.ParseExact(CDate(txtInvoiceDate.Text), "dd-MM-yyyy", provider), "DR")
                                End If

                                '================SENDING DATA TO EY PORTAL START==================='

                                Dim sqlquery As String = ""
                                sqlquery = "update CN_DN_DETAILS set irn_no ='" & EinvErrorData.Item(0).IRN & "', QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result1.ToString() & "' where DEBIT_CREDIT_NO  ='" & TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                                Dim despatch As New SqlCommand(sqlquery, conn_trans, myTrans)
                                despatch.ExecuteReader()
                                despatch.Dispose()
                                goAheadFlag = True

                            ElseIf (EinvErrorData.Item(0).status = "2") Then
                                Label3.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorCode
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                                goAheadFlag = False
                                Label3.Text = "there is some response error in e-invoice generation."

                            ElseIf (EinvErrorData.Item(0).status = "3") Then
                                Label3.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).errorfield
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).errordesc
                                goAheadFlag = False
                                Label3.Text = "there is some response error in e-invoice generation."
                            ElseIf (EinvErrorData.Item(0).status = "4") Then
                                TextBox6.Text = EinvErrorData.Item(0).IRN

                                '================SENDING DATA TO EY PORTAL START==================='
                                Dim result1
                                If (Label7.Text = "Credit Note") Then
                                    result1 = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "YES", Date.ParseExact(CDate(txtInvoiceDate.Text), "dd-MM-yyyy", provider), "CR")
                                Else
                                    result1 = logicClassObj.SubmitGSTR1DataToEYPortal(authIdToken, New Guid().ToString(), "", TextBox65.Text, GridView1.Rows(0).Cells(5).Text, GridView1.Rows(0).Cells(6).Text, isServiceFlag, "N", "YES", Date.ParseExact(CDate(txtInvoiceDate.Text), "dd-MM-yyyy", provider), "DR")
                                End If

                                '================SENDING DATA TO EY PORTAL START==================='

                                Label3.Visible = True
                                Label42.Visible = True
                                txtEinvoiceErrorCode.Visible = True
                                txtEinvoiceErrorMessage.Visible = True
                                txtEinvoiceErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                                txtEinvoiceErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage
                                Dim sqlquery As String = ""
                                sqlquery = "update CN_DN_DETAILS set irn_no ='" & EinvErrorData.Item(0).IRN & "', QR_CODE ='" & EinvErrorData.Item(0).QRCode & "', EY_STATUS ='" & result1.ToString() & "' where DEBIT_CREDIT_NO  ='" & TextBox65.Text & "' AND FISCAL_YEAR='" & STR1 & "'"
                                Dim despatch As New SqlCommand(sqlquery, conn_trans, myTrans)
                                despatch.ExecuteReader()
                                despatch.Dispose()
                                goAheadFlag = True

                                'label308.text = "there is error in e-way bill generation, please generate e-way bill alone with above irn."
                            End If

                        ElseIf (AuthErrorData.Item(0).status = "2") Then

                            Label3.Visible = True
                            Label42.Visible = True
                            txtEinvoiceErrorCode.Visible = True
                            txtEinvoiceErrorMessage.Visible = True
                            txtEinvoiceErrorCode.Text = AuthErrorData.Item(0).errorCode
                            txtEinvoiceErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                            goAheadFlag = False
                            Label3.Text = "There is some response error in E-invoice Authentication."
                        Else
                            goAheadFlag = False
                            Label3.Text = "There is some response error in E-invoice Authentication."
                        End If
                    End If

                End If

                '''===========================Generate E-Invoice Through EY End=======================''
                '''''''''''''''''''''''''''''''''''''''''''''''''''
                If (goAheadFlag = True) Then
                    myTrans.Commit()
                    Label3.Text = "All records are written to database."
                    Label3.Visible = False

                    txtEinvoiceErrorCode.Visible = False
                    txtEinvoiceErrorMessage.Visible = False
                    Button2.Enabled = False
                    dt.Clear()
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                    GridView1.Visible = False
                Else
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    TextBox65.Text = ""
                    TextBox6.Text = ""
                    dt.Clear()
                    GridView1.DataSource = dt
                    GridView1.DataBind()

                End If



            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label3.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub

    ''''''''''''''''''''''===============================''''''''''''''''''''''''
    Protected Sub ORIGINAL(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim inv As String = btn.CommandName
        Dim cond As String = btn.Text
        ''PRINT
        PRINT(inv, "ORIGINAL FOR RECEIPIENT")
        ''UPDATE
        conn.Open()
        Dim QUARY1 As String = "update INV_PRINT set PRINT_ORIGN=@PRINT_ORIGN WHERE INV_NO ='" & inv & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn)
        cmd2.Parameters.AddWithValue("@PRINT_ORIGN", "")
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
        Dim origin, duplicate, triplicate As String
        origin = ""
        duplicate = ""
        triplicate = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from INV_PRINT WHERE INV_NO='" & inv & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            origin = dr.Item("PRINT_ORIGN")
            duplicate = dr.Item("PRINT_TRANS")
            triplicate = dr.Item("PRINT_ASSAE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If origin = "" And duplicate = "" And triplicate = "" Then
            ''DELETE
            conn.Open()
            mycommand = New SqlCommand("DELETE FROM INV_PRINT WHERE INV_NO ='" & inv & "'", conn)
            mycommand.ExecuteNonQuery()
            conn.Close()

        End If
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from INV_PRINT", conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
    End Sub
    Protected Sub DUPLICATE(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim inv As String = btn.CommandName
        Dim cond As String = btn.Text
        ''PRINT
        PRINT(inv, "DUPLICATE FOR TRANSPORTER")
        ''UPDATE
        conn.Open()
        Dim QUARY1 As String = "update INV_PRINT set PRINT_TRANS=@PRINT_TRANS WHERE INV_NO ='" & inv & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn)
        cmd2.Parameters.AddWithValue("@PRINT_TRANS", "")
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
        Dim origin, duplicate, triplicate As String
        origin = ""
        duplicate = ""
        triplicate = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from INV_PRINT WHERE INV_NO='" & inv & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            origin = dr.Item("PRINT_ORIGN")
            duplicate = dr.Item("PRINT_TRANS")
            triplicate = dr.Item("PRINT_ASSAE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If origin = "" And duplicate = "" And triplicate = "" Then
            ''DELETE
            conn.Open()
            mycommand = New SqlCommand("DELETE FROM INV_PRINT WHERE INV_NO ='" & inv & "'", conn)
            mycommand.ExecuteNonQuery()
            conn.Close()
        End If
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from INV_PRINT", conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()

    End Sub
    Protected Sub PRINT(INV_NO As String, INV_TYPE As String)
        Dim inv_for As String = ""

        Dim INV_NAME As String = ""

        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        'Dim PO_QUARY As String = "select '" & INV_TYPE & "' AS INV_TYPE,* from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT .PO_NO =ORDER_DETAILS .so_no join MATERIAL on PO_RCD_MAT .MAT_CODE =MATERIAL.MAT_CODE join SUPL on PO_RCD_MAT .SUPL_ID=SUPL.SUPL_ID join PO_ORD_MAT on PO_RCD_MAT .PO_NO =PO_ORD_MAT .PO_NO and PO_RCD_MAT .MAT_SLNO =PO_ORD_MAT .MAT_SLNO join CN_DN_DETAILS on PO_RCD_MAT.CRR_NO=CN_DN_DETAILS.CRR_NO AND PO_RCD_MAT.MAT_SLNO=CN_DN_DETAILS.PO_SL_NO where CN_DN_DETAILS .INV_NO ='" & INV_NO & "'"
        ''''''''''''''''''''''''''''''''''''''
        Dim DOC_TYPE As String = ""
        conn.Open()
        Dim MC As New SqlCommand
        MC.CommandText = "select INVOICE_NO from CN_DN_DETAILS where INV_NO = '" & txtInvSearch.Text & "'"
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            DOC_TYPE = dr.Item("INVOICE_NO")
            dr.Close()
        End If
        conn.Close()



        Dim dt2 As New DataTable
        Dim PO_QUARY As New String("")
        If (Left(DOC_TYPE, 2) = "SG" Or Left(DOC_TYPE, 2) = "RG") Then
            PO_QUARY = "select CN_DN_DETAILS.INV_TYPE,(SELECT '" & INV_TYPE & "' AS INV_FOR) AS INV_FOR,SO_ACTUAL as PO_NO,SO_ACTUAL_DATE AS PO_DATE,ORDER_DETAILS.SO_NO,ORDER_DETAILS.SO_DATE,'NA' AS AMD_NO,'NA' AS AMD_DATE,PO_RCD_MAT.TRANS_WO_NO AS TRANS_WO,TRANS_SLNO AS TRANS_SLNO,GARN_NO AS INV_NO, GARN_DATE AS INV_DATE,TRUCK_NO,PARTY_CODE, CONSIGN_CODE,MAT_SLNO,
                        MATERIAL.MAT_CODE AS P_CODE,MAT_NAME as P_DESC,SUPL_NAME AS D_NAME, SUPL_AT AS ADD_1, SUPL_PO AS ADD_2, SUPL_DIST AS D_RANGE, SUPL_STATE AS D_STATE, SUPL_PIN AS D_DIVISION, SUPL_GST_NO AS GST_CODE, SUPL_STATE_CODE AS D_STATE_CODE,PAYMENT_TERM AS FINANCE_ARRENGE, CN_DN_DETAILS.QTY AS TOTAL_PCS, CN_DN_DETAILS.CGST_RATE, CN_DN_DETAILS.CGST_AMT,
                        CN_DN_DETAILS.SGST_RATE, CN_DN_DETAILS.SGST_AMT,CN_DN_DETAILS.IGST_RATE, CN_DN_DETAILS.IGST_AMT, CN_DN_DETAILS.BASE_PRICE,CN_DN_DETAILS.TOTAL_VALUE,CN_DN_DETAILS.INV_NO AS DEBIT_CREDIT_NO,CN_DN_DETAILS.INV_DATE as DEBIT_CREDIT_DATE from PO_RCD_MAT join SUPL on PO_RCD_MAT.SUPL_ID=SUPL.SUPL_ID JOIN CN_DN_DETAILS ON PO_RCD_MAT.GARN_NO=CN_DN_DETAILS.INVOICE_NO join ORDER_DETAILS on PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO JOIN MATERIAL ON PO_RCD_MAT.MAT_CODE=MATERIAL.MAT_CODE where CN_DN_DETAILS.INV_NO ='" & txtInvSearch.Text & "'"
        Else
            PO_QUARY = "select (SELECT '" & INV_TYPE & "' AS INV_FOR) AS INV_FOR, * from DESPATCH join dater on DESPATCH.PARTY_CODE=dater.d_code JOIN CN_DN_DETAILS ON (DESPATCH.D_TYPE+ CONVERT(VARCHAR(15), DESPATCH.INV_NO))=CN_DN_DETAILS.INVOICE_NO where D_TYPE+ CONVERT(VARCHAR(15), DESPATCH.INV_NO) ='" & DropDownList4.Text & "' and DESPATCH.FISCAL_YEAR ='" & DropDownList3.Text & "'"
        End If

        ''''''''''''''''''''''''''''''''''''''
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        crystalReport.Load(Server.MapPath("~/print_rpt/Credit_debit_Note.rpt"))
        crystalReport.SetDataSource(dt)
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
    Protected Sub TRIPLICATE(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim inv As String = btn.CommandName
        Dim cond As String = btn.Text
        ''PRINT
        PRINT(inv, "TRIPLICATE FOR SUPPLIER")
        ''UPDATE
        conn.Open()
        Dim QUARY1 As String = "update INV_PRINT set PRINT_ASSAE=@PRINT_ASSAE WHERE INV_NO ='" & inv & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn)
        cmd2.Parameters.AddWithValue("@PRINT_ASSAE", "")
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
        Dim origin, duplicate, triplicate As String
        origin = ""
        duplicate = ""
        triplicate = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from INV_PRINT WHERE INV_NO='" & inv & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            origin = dr.Item("PRINT_ORIGN")
            duplicate = dr.Item("PRINT_TRANS")
            triplicate = dr.Item("PRINT_ASSAE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If origin = "" And duplicate = "" And triplicate = "" Then
            ''DELETE
            conn.Open()
            mycommand = New SqlCommand("DELETE FROM INV_PRINT WHERE INV_NO ='" & inv & "'", conn)
            mycommand.ExecuteNonQuery()
            conn.Close()
        End If
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("select * from INV_PRINT", conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
    End Sub

    Public Function GetQRCodeData(inputString As String)

        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE

        writer.Options.Height = 10
        writer.Options.Width = 10
        Dim result = writer.Write(inputString)
        Dim barcodeBitmap = New Bitmap(result)

        Dim bytes As Byte()
        Using memory As New MemoryStream()
            barcodeBitmap.Save(memory, ImageFormat.Jpeg)
            bytes = memory.ToArray()
        End Using

        Return bytes

    End Function

    Protected Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        If txtInvSearch.Text = "" Then
            txtInvSearch.Focus()
            Return

        End If
        count = 0

        Dim DOC_TYPE As String = ""
        conn.Open()
        Dim MC As New SqlCommand
        MC.CommandText = "select INVOICE_NO from CN_DN_DETAILS where DEBIT_CREDIT_NO = '" & txtInvSearch.Text & "'"
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            DOC_TYPE = dr.Item("INVOICE_NO")
            dr.Close()
        End If
        conn.Close()


        Dim crystalReport As New ReportDocument
        Dim dt2 As New DataTable
        Dim PO_QUARY As New String("")
        If (Left(DOC_TYPE, 2) = "SG" Or Left(DOC_TYPE, 2) = "RG") Then
            PO_QUARY = "select CN_DN_DETAILS.INV_TYPE,(SELECT 'EXTRA COPY' AS INV_FOR) AS INV_FOR,SO_ACTUAL as PO_NO,SO_ACTUAL_DATE AS PO_DATE,ORDER_DETAILS.SO_NO,ORDER_DETAILS.SO_DATE,'NA' AS AMD_NO,'NA' AS AMD_DATE,PO_RCD_MAT.TRANS_WO_NO AS TRANS_WO,TRANS_SLNO AS TRANS_SLNO,GARN_NO AS INV_NO, GARN_DATE AS INV_DATE,TRUCK_NO,PARTY_CODE, CONSIGN_CODE,MAT_SLNO,
            MATERIAL.MAT_CODE AS P_CODE,MAT_NAME as P_DESC,SUPL_NAME AS D_NAME, SUPL_AT AS ADD_1, SUPL_PO AS ADD_2, SUPL_DIST AS D_RANGE, SUPL_STATE AS D_STATE, SUPL_PIN AS D_DIVISION, SUPL_GST_NO AS GST_CODE, SUPL_STATE_CODE AS D_STATE_CODE,PAYMENT_TERM AS FINANCE_ARRENGE, CN_DN_DETAILS.QTY AS TOTAL_PCS, CN_DN_DETAILS.CGST_RATE, CN_DN_DETAILS.CGST_AMT,
            CN_DN_DETAILS.SGST_RATE, CN_DN_DETAILS.SGST_AMT,CN_DN_DETAILS.IGST_RATE, CN_DN_DETAILS.IGST_AMT, CN_DN_DETAILS.BASE_PRICE,CN_DN_DETAILS.TOTAL_VALUE,CN_DN_DETAILS.INV_NO AS DEBIT_CREDIT_NO,CN_DN_DETAILS.INV_DATE as DEBIT_CREDIT_DATE from PO_RCD_MAT join SUPL on PO_RCD_MAT.SUPL_ID=SUPL.SUPL_ID JOIN CN_DN_DETAILS ON PO_RCD_MAT.GARN_NO=CN_DN_DETAILS.INVOICE_NO join ORDER_DETAILS on PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO JOIN MATERIAL ON PO_RCD_MAT.MAT_CODE=MATERIAL.MAT_CODE where CN_DN_DETAILS.INV_NO ='" & txtInvSearch.Text & "'"


        Else
            'PO_QUARY = "select (SELECT 'EXTRA COPY' AS INV_FOR) AS INV_FOR, * from DESPATCH join dater on DESPATCH.PARTY_CODE=dater.d_code JOIN CN_DN_DETAILS ON (DESPATCH.D_TYPE+ CONVERT(VARCHAR(15), DESPATCH.INV_NO))=CN_DN_DETAILS.INVOICE_NO where D_TYPE+ CONVERT(VARCHAR(15), DESPATCH.INV_NO) ='" & TextBox1.Text & "' and DESPATCH.FISCAL_YEAR ='" & TextBox3.Text & "'"
            PO_QUARY = "select QTY AS TOTAL_PCS,ORDER_SL_NO AS MAT_SLNO,MAT_CODE as P_CODE,MAT_name as P_DESC,dater.d_name as D_NAME,dater.add_1 as ADD_1,dater.add_2 as ADD_2,dater.d_division as D_RANGE,dater.d_pin as D_DIVISION,dater.gst_code as GST_CODE,dater.d_state_code as D_STATE_CODE,dater.d_state as D_STATE,dater.d_code as PARTY_CODE,* FROM CN_DN_DETAILS LEFT join dater ON CN_DN_DETAILS.PARTY_CODE=dater.d_code where CN_DN_DETAILS.DEBIT_CREDIT_NO ='" & txtInvSearch.Text & "'"
        End If

        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt2)
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        conn.Open()
        'Dim crystalReport As New ReportDocument
        'Dim dt2 As New DataTable
        'Dim PO_QUARY As String = "select (SELECT 'EXTRA COPY' AS INV_FOR ) AS INV_FOR, * from DESPATCH where D_TYPE+ CONVERT(VARCHAR(15), INV_NO) ='" & txtContactsSearch.Text & "' and FISCAL_YEAR =" & TextBox72.Text
        'da = New SqlDataAdapter(PO_QUARY, conn)
        'da.Fill(dt2)

        Dim inv_for As String = ""
        Dim IRN_NO As String = ""
        Dim QRCodeData As String = ""
        mycommand.CommandText = "select * from CN_DN_DETAILS where DEBIT_CREDIT_NO ='" & txtInvSearch.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_for = dr.Item("INV_TYPE")
            If (IsDBNull(dr.Item("QR_CODE"))) Then
                IRN_NO = ""
                QRCodeData = ""
            Else
                If (IsDBNull(dr.Item("IRN_NO"))) Then
                    IRN_NO = ""
                Else
                    IRN_NO = dr.Item("IRN_NO")
                End If

                QRCodeData = dr.Item("QR_CODE")
                ''Add the Barcode column to the DataSet
                dt2.Columns.Add(New DataColumn("QR_CODE_INPUT", GetType(Byte())))

                For Each dr As DataRow In dt2.Rows

                    dr("QR_CODE_INPUT") = GetQRCodeData(QRCodeData)

                Next
            End If

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim INV_NAME As String = ""
        If inv_for = "Delivery Challan(Within same GSTIN)" Then
            'INV_NAME = "gst_invoice.rpt"
            INV_NAME = "gst_invoice_ti_new.rpt"
        Else
            If QRCodeData = "" Then
                'INV_NAME = "gst_invoice_ti.rpt"
                INV_NAME = "gst_invoice_ti_new.rpt"
            Else
                INV_NAME = "Credit_debit_Note.rpt"
            End If

        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        crystalReport.Load(Server.MapPath("~/print_rpt/Credit_debit_Note.rpt"))
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

    Protected Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click

        Dim QUARY As String = ""
        QUARY = "select * from INV_PRINT WHERE (INV_NO LIKE 'CN%' or INV_NO LIKE 'DN%') ORDER BY INV_NO"

        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter(QUARY, conn)
        da.Fill(dt)
        conn.Close()
        GridView3.DataSource = dt
        GridView3.DataBind()
        GridView3.Visible = True
    End Sub

    Protected Sub GridView3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView3.SelectedIndexChanged

    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedValue = "Credit Note" Then
            MultiView1.ActiveViewIndex = 0
            Label7.Text = "Credit Note"
        ElseIf DropDownList2.SelectedValue = "Debit Note" Then
            MultiView1.ActiveViewIndex = 0
            Label7.Text = "Debit Note"
        ElseIf DropDownList2.SelectedValue = "Print" Then
            MultiView1.ActiveViewIndex = 1
        End If
    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If (DropDownList3.Text <> "Select") Then
            conn.Open()
            da = New SqlDataAdapter("select distinct (D_TYPE+INV_NO) AS INV_NO from DESPATCH where FISCAL_YEAR= '" & DropDownList3.Text & "' AND D_TYPE LIKE 'OS%' ORDER BY INV_NO", conn)
            da.Fill(ds, "DESPATCH")
            DropDownList4.DataSource = ds.Tables("DESPATCH")
            DropDownList4.DataValueField = "INV_NO"
            DropDownList4.DataBind()
            DropDownList4.Items.Insert(0, "Select")
            conn.Close()
        Else
            DropDownList4.Items.Clear()
            DropDownList4.DataBind()
        End If
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If (DropDownList4.SelectedValue <> "Select") Then

            Dim mc1 As New SqlCommand
            conn.Open()
            mc1.CommandText = "select * from DESPATCH where FISCAL_YEAR= '" & DropDownList3.SelectedValue & "' AND D_TYPE+inv_no='" & DropDownList4.SelectedValue & "' ORDER BY INV_NO"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label320.Text = dr.Item("ACC_UNIT")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            DropDownList4.Items.Clear()
            DropDownList4.DataBind()
        End If
    End Sub
End Class