Imports System.Data.SqlClient
Imports System.Net
Imports System.Globalization

Public Class e_invoice
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim goAheadFlag As Boolean = True
    Dim mycommand As New SqlCommand
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim ds As New DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub



    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            MultiView2.ActiveViewIndex = -1
            Return

        ElseIf DropDownList9.SelectedValue = "Cancel E-invoice" Then

            MultiView2.ActiveViewIndex = 0

            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList17.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList17.DataValueField = "FY"
            DropDownList17.DataBind()
            DropDownList17.Items.Insert(0, "Select")
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "Generate E-way bill from IRN" Then

            MultiView2.ActiveViewIndex = 1

            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList3.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList3.DataValueField = "FY"
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "Select")
            conn.Close()


        ElseIf DropDownList9.SelectedValue = "Cancel E-way bill" Then

            MultiView2.ActiveViewIndex = 2
            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList1.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList1.DataValueField = "FY"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "Update E-way bill" Then

            MultiView2.ActiveViewIndex = 3
            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList6.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList6.DataValueField = "FY"
            DropDownList6.DataBind()
            DropDownList6.Items.Insert(0, "Select")
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "Extend E-way bill" Then

            MultiView2.ActiveViewIndex = 4

        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Dim gst_code, my_gst_code As New String("")
            conn.Open()
            mycommand.CommandText = "select * from dater where d_code='" & TextBox32.Text & "'"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                gst_code = dr.Item("gst_code")
                dr.Close()
            End If
            conn.Close()

            'search company profile compair gst code
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

            Try
                If gst_code = my_gst_code Then
                    ''E-Invoice is not required
                Else
                    Dim logicClassObj = New EinvoiceLogicClassEY
                    Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(DropDownList8.SelectedValue, TextBox32.Text)
                    If (AuthErrorData.Item(0).status = "1") Then

                        Dim EinvErrorData As List(Of EinvoiceFromIRNErrorDetailsClassEY) = logicClassObj.GenerateEwayBillFromIRN(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, TextBox26.Text, DropDownList8.SelectedValue, TextBox32.Text, TextBox34.Text)
                        If (EinvErrorData.Item(0).status = "1") Then
                            TextBox36.Text = EinvErrorData.Item(0).EwbNo
                            TextBox46.Text = EinvErrorData.Item(0).EwbValidTill

                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set EWB_NO ='" & EinvErrorData.Item(0).EwbNo & "',EWB_DATE ='" & EinvErrorData.Item(0).EwbDt & "',EWB_VALIDITY ='" & EinvErrorData.Item(0).EwbValidTill & "', EWB_STATUS ='ACTIVE' where IRN_NO ='" & EinvErrorData.Item(0).IRN & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            goAheadFlag = True

                        ElseIf (EinvErrorData.Item(0).status = "2") Then
                            Label70.Visible = True
                            Label73.Visible = True
                            lblEwaybillFromIRNErrorCode.Visible = True
                            lblEwaybillFromIRNErrorMessage.Visible = True
                            lblEwaybillFromIRNErrorCode.Text = EinvErrorData.Item(0).errorCode
                            lblEwaybillFromIRNErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                            goAheadFlag = False
                            Label68.Text = "There is some response error in E-Invoice generation."
                        ElseIf (EinvErrorData.Item(0).status = "3") Then
                            Label70.Visible = True
                            Label73.Visible = True
                            lblEwaybillFromIRNErrorCode.Visible = True
                            lblEwaybillFromIRNErrorMessage.Visible = True
                            lblEwaybillFromIRNErrorCode.Text = EinvErrorData.Item(0).errorfield
                            lblEwaybillFromIRNErrorMessage.Text = EinvErrorData.Item(0).errordesc
                            goAheadFlag = False
                            Label68.Text = "There is some response error in E-Invoice generation."
                        ElseIf (EinvErrorData.Item(0).status = "4") Then
                            Label70.Visible = True
                            Label73.Visible = True
                            lblEwaybillFromIRNErrorCode.Visible = True
                            lblEwaybillFromIRNErrorMessage.Visible = True
                            lblEwaybillFromIRNErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                            lblEwaybillFromIRNErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage
                            goAheadFlag = False
                            Label68.Text = "There is error in E-way bill generation, please generate E-way bill alone with above IRN."

                        End If

                    ElseIf (AuthErrorData.Item(0).status = "2") Then

                        Label70.Visible = True
                        Label73.Visible = True
                        lblEwaybillFromIRNErrorCode.Visible = True
                        lblEwaybillFromIRNErrorMessage.Visible = True
                        lblEwaybillFromIRNErrorCode.Text = AuthErrorData.Item(0).errorCode
                        lblEwaybillFromIRNErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                        goAheadFlag = False
                        Label68.Text = "There is some response error in E-invoice Authentication."
                    Else
                        goAheadFlag = False
                        Label68.Text = "There is some response error in E-invoice Authentication."
                    End If


                End If

                If (goAheadFlag = True) Then
                    myTrans.Commit()
                    Label70.Visible = False
                    Label73.Visible = False
                    lblEwaybillFromIRNErrorCode.Visible = False
                    lblEwaybillFromIRNErrorMessage.Visible = False
                    Label68.Text = "All records are written to database."
                Else
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    TextBox36.Text = ""
                    TextBox46.Text = ""
                    TextBox6.Text = ""

                End If

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                TextBox36.Text = ""
                TextBox46.Text = ""
                Label68.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try
        End Using
    End Sub

    Protected Sub DropDownList8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList8.SelectedIndexChanged
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "SELECT * FROM DESPATCH WHERE D_TYPE+INV_NO='" & DropDownList8.Text & "' AND FISCAL_YEAR='" & DropDownList3.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox26.Text = dr.Item("IRN_NO")
            TextBox27.Text = dr.Item("INV_STATUS")
            TextBox32.Text = dr.Item("PARTY_CODE")
            TextBox34.Text = dr.Item("CONSIGN_CODE")

            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "SELECT * FROM DATER WHERE D_CODE='" & TextBox32.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox33.Text = dr.Item("D_NAME")
            dr.Close()
        End If
        conn.Close()
        conn.Open()
        mc1.CommandText = "SELECT * FROM DATER WHERE D_CODE='" & TextBox34.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox47.Text = dr.Item("D_NAME")
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        Dim eWayBillStatus As New String("")
        conn.Open()
        mycommand.CommandText = "select EWB_STATUS from DESPATCH WHERE IRN_NO='" & TextBox24.Text & "'"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If IsDBNull(dr.Item("EWB_STATUS")) Then
                eWayBillStatus = "INACTIVE"
            Else
                eWayBillStatus = dr.Item("EWB_STATUS")
            End If

            dr.Close()
        End If
        conn.Close()

        If (eWayBillStatus <> "ACTIVE") Then
            Dim my_gst_code As New String("")
            'search company profile compair gst code
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

            Dim logicClassObj = New EinvoiceLogicClassEY
            Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(DropDownList10.SelectedValue, TextBox25.Text)
            If (AuthErrorData.Item(0).status = "1") Then

                Dim EinvCancellationErrorData As List(Of EinvoiceCancellationErrorDetailsClassEY) = logicClassObj.CancelEInvoice(AuthErrorData.Item(0).Idtoken, TextBox24.Text, my_gst_code, DropDownList2.SelectedValue, TextBox31.Text)
                If (EinvCancellationErrorData.Item(0).status = "1") Then
                    'TextBox6.Text = EinvCancellationErrorData.Item(0).IRN
                    conn.Open()
                    Dim sqlQuery As String = ""
                    sqlQuery = "update DESPATCH set INV_STATUS ='CANCELLED',INV_CANCELLATION_DATE='" & CDate(EinvCancellationErrorData.Item(0).CancelDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & "' where IRN_NO  ='" & EinvCancellationErrorData.Item(0).IRN & "'"
                    Dim despatch As New SqlCommand(sqlQuery, conn)
                    despatch.ExecuteReader()
                    despatch.Dispose()
                    conn.Close()
                    goAheadFlag = True
                    Label552.Visible = True
                    Label552.Text = "IRN Cancelled Successfully."
                ElseIf (EinvCancellationErrorData.Item(0).status = "2") Then
                    Label552.Visible = True
                    Label553.Visible = True
                    Label552.Text = EinvCancellationErrorData.Item(0).errorCode
                    Label553.Text = EinvCancellationErrorData.Item(0).errorMessage
                    goAheadFlag = False

                End If

            ElseIf (AuthErrorData.Item(0).status = "2") Then

                Label552.Visible = True
                Label553.Visible = True
                Label552.Text = AuthErrorData.Item(0).errorCode
                Label553.Text = AuthErrorData.Item(0).errorMessage
                goAheadFlag = False

            Else
                goAheadFlag = False
                Label552.Visible = True
                Label552.Text = "There is some response error in E-invoice Authentication."
            End If
        Else
            Label554.Text = "Please cancel E-way bill first"
        End If

    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If (DropDownList3.SelectedValue <> "Select") Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (D_TYPE+INV_NO) as invoice_no from DESPATCH where FISCAL_YEAR='" & DropDownList3.SelectedValue & "' and len(IRN_NO)>0 and (EWB_NO is null OR EWB_STATUS='CANCELLED') order by INV_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList8.Items.Clear()
            DropDownList8.DataSource = dt
            DropDownList8.DataValueField = "invoice_no"
            DropDownList8.DataBind()
            DropDownList8.Items.Insert(0, "Select")
            DropDownList8.SelectedValue = "Select"
        Else
            DropDownList8.Items.Clear()
            DropDownList8.DataBind()
        End If

    End Sub

    Protected Sub DropDownList17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList17.SelectedIndexChanged
        If (DropDownList17.SelectedValue <> "Select") Then
            conn.Open()
            da = New SqlDataAdapter("select (D_TYPE+INV_NO) As invoice_no from DESPATCH where IRN_NO is not null and FISCAL_YEAR='" & DropDownList17.SelectedValue & "' AND (INV_STATUS='' OR INV_STATUS='ACTIVE') order by invoice_no", conn)
            da.Fill(ds, "DESPATCH")
            DropDownList10.DataSource = ds.Tables("DESPATCH")
            DropDownList10.DataValueField = "invoice_no"
            DropDownList10.DataBind()
            DropDownList10.Items.Insert(0, "Select")
            DropDownList10.SelectedValue = "Select"
            conn.Close()
        Else
            DropDownList10.Items.Clear()
            DropDownList10.DataBind()
        End If
    End Sub

    Protected Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged
        If (DropDownList10.SelectedValue <> "Select") Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select * from DESPATCH LEFT JOIN dater ON DESPATCH.PARTY_CODE=dater.d_code LEFT JOIN SUPL ON DESPATCH.PARTY_CODE=SUPL.SUPL_ID where D_TYPE+INV_NO='" & DropDownList10.Text & "' AND FISCAL_YEAR='" & DropDownList17.Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox24.Text = dr.Item("IRN_NO")
                If (dr.Item("INV_STATUS") = "") Then
                    TextBox28.Text = "ACTIVE"
                Else
                    TextBox28.Text = dr.Item("INV_STATUS")
                End If
                TextBox25.Text = dr.Item("PARTY_CODE")
                TextBox35.Text = dr.Item("CONSIGN_CODE")

                If (IsDBNull(dr.Item("d_name"))) Then
                    TextBox29.Text = dr.Item("SUPL_NAME")
                Else
                    TextBox29.Text = dr.Item("d_name")
                End If
                TextBox30.Text = dr.Item("TOTAL_AMT")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            If (Left(TextBox35.Text, 1) = "D") Then
                conn.Open()
                mc1.CommandText = "SELECT * FROM DATER WHERE D_CODE='" & TextBox35.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox48.Text = dr.Item("D_NAME")
                    dr.Close()
                End If
                conn.Close()
            Else
                conn.Open()
                mc1.CommandText = "SELECT * FROM SUPL WHERE SUPL_ID='" & TextBox35.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox48.Text = dr.Item("SUPL_NAME")
                    dr.Close()
                End If
                conn.Close()
            End If

        Else
            DropDownList10.Items.Clear()
            DropDownList10.DataBind()
        End If
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If (DropDownList2.Text = "1") Then
            TextBox31.Text = "Duplicate IRN"
        ElseIf (DropDownList2.Text = "2") Then
            TextBox31.Text = "Wrong data entry"
        ElseIf (DropDownList2.Text = "3") Then
            TextBox31.Text = "Order Cancelled"
        ElseIf (DropDownList2.Text = "4") Then
            TextBox31.Text = "Others"
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If (DropDownList1.SelectedValue <> "Select") Then
            conn.Open()
            da = New SqlDataAdapter("select (D_TYPE+INV_NO) As invoice_no from DESPATCH where EWB_NO is not null and FISCAL_YEAR='" & DropDownList1.SelectedValue & "' AND (EWB_STATUS='' OR EWB_STATUS='ACTIVE') order by invoice_no", conn)
            da.Fill(ds, "DESPATCH")
            DropDownList4.DataSource = ds.Tables("DESPATCH")
            DropDownList4.DataValueField = "invoice_no"
            DropDownList4.DataBind()
            DropDownList4.Items.Insert(0, "Select")
            DropDownList4.SelectedValue = "Select"
            conn.Close()
        Else
            DropDownList4.Items.Clear()
            DropDownList4.DataBind()
        End If
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If (DropDownList4.SelectedValue <> "Select") Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select * from DESPATCH LEFT JOIN dater ON DESPATCH.PARTY_CODE=dater.d_code LEFT JOIN SUPL ON DESPATCH.PARTY_CODE=SUPL.SUPL_ID where D_TYPE+INV_NO='" & DropDownList4.Text & "' AND FISCAL_YEAR='" & DropDownList1.Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                If (IsDBNull(dr.Item("IRN_NO"))) Then
                    TextBox1.Text = ""
                Else
                    TextBox1.Text = dr.Item("IRN_NO")
                End If

                If (dr.Item("INV_STATUS") = "") Then
                    TextBox2.Text = "ACTIVE"
                Else
                    TextBox2.Text = dr.Item("INV_STATUS")
                End If

                If (dr.Item("EWB_STATUS") = "") Then
                    TextBox7.Text = "ACTIVE"
                Else
                    TextBox7.Text = dr.Item("EWB_STATUS")
                End If

                TextBox3.Text = dr.Item("PARTY_CODE")

                If (IsDBNull(dr.Item("d_name"))) Then
                    TextBox4.Text = dr.Item("SUPL_NAME")
                Else
                    TextBox4.Text = dr.Item("d_name")
                End If

                TextBox5.Text = dr.Item("EWB_NO")
                TextBox6.Text = dr.Item("EWB_VALIDITY")
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

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If (DropDownList5.Text = "1") Then
            TextBox8.Text = "Duplicate IRN"
        ElseIf (DropDownList5.Text = "2") Then
            TextBox8.Text = "Wrong data entry"
        ElseIf (DropDownList5.Text = "3") Then
            TextBox8.Text = "Order Cancelled"
        ElseIf (DropDownList5.Text = "4") Then
            TextBox8.Text = "Others"
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim working_date As Date
        working_date = Today.Date
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
        Dim my_gst_code As New String("")
        'search company profile compair gst code
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

        Dim logicClassObj = New EinvoiceLogicClassEY
        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(DropDownList4.SelectedValue, TextBox3.Text)
        If (AuthErrorData.Item(0).status = "1") Then

            Dim EwaybillCancellationErrorData As List(Of EwaybillCancellationErrorDetailsClassEY) = logicClassObj.CancelEwayBill(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, TextBox5.Text, my_gst_code, DropDownList5.SelectedValue)
            If (EwaybillCancellationErrorData.Item(0).status = "1") Then
                'TextBox6.Text = EwaybillCancellationErrorData.Item(0).IRN
                conn.Open()
                Dim sqlQuery As String = ""
                sqlQuery = "update DESPATCH set EWB_STATUS ='CANCELLED',EWB_CANCELLATION_DATE='" & CDate(EwaybillCancellationErrorData.Item(0).CancelDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & "' where D_TYPE+INV_NO  ='" & DropDownList4.SelectedValue & "' AND FISCAL_YEAR='" & STR1 & "'"
                Dim despatch As New SqlCommand(sqlQuery, conn)
                despatch.ExecuteReader()
                despatch.Dispose()
                conn.Close()
                goAheadFlag = True
                Label21.Visible = True
                Label21.Text = "E-waybill Cancelled Successfully."
                Label22.Visible = False
                Label23.Visible = False
                Label24.Visible = False
                Label25.Visible = False

            ElseIf (EwaybillCancellationErrorData.Item(0).status = "2") Then

                Label22.Visible = True
                Label24.Visible = True
                Label23.Visible = True
                Label25.Visible = True
                Label23.Text = EwaybillCancellationErrorData.Item(0).errorCode
                Label25.Text = EwaybillCancellationErrorData.Item(0).errorMessage
                goAheadFlag = False
                Label21.Visible = True
                Label21.Text = "There is some response error in E-way bill cancellation."

            End If

        ElseIf (AuthErrorData.Item(0).status = "2") Then

            Label23.Visible = True
            Label25.Visible = True
            Label23.Text = AuthErrorData.Item(0).errorCode
            Label25.Text = AuthErrorData.Item(0).errorMessage
            goAheadFlag = False

        Else
            goAheadFlag = False
            Label23.Visible = True
            Label23.Text = "There is some response error in E-invoice Authentication."
        End If
    End Sub

    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        If (DropDownList6.SelectedValue <> "Select") Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (D_TYPE+INV_NO) as invoice_no from DESPATCH where FISCAL_YEAR='" & DropDownList6.SelectedValue & "' and len(IRN_NO)>0 and EWB_STATUS='ACTIVE' order by INV_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList7.Items.Clear()
            DropDownList7.DataSource = dt
            DropDownList7.DataValueField = "invoice_no"
            DropDownList7.DataBind()
            DropDownList7.Items.Insert(0, "Select")
            DropDownList7.SelectedValue = "Select"
        Else
            DropDownList7.Items.Clear()
            DropDownList7.DataBind()
        End If
    End Sub

    Protected Sub DropDownList7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList7.SelectedIndexChanged
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "SELECT * FROM DESPATCH WHERE D_TYPE+INV_NO='" & DropDownList7.Text & "' AND FISCAL_YEAR='" & DropDownList6.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox11.Text = dr.Item("IRN_NO")
            TextBox12.Text = dr.Item("INV_STATUS")
            TextBox13.Text = dr.Item("PARTY_CODE")
            TextBox15.Text = dr.Item("CONSIGN_CODE")
            TextBox49.Text = dr.Item("TRUCK_NO")
            TextBox54.Text = dr.Item("EWB_NO")
            TextBox55.Text = dr.Item("EWB_VALIDITY")
            TextBox56.Text = dr.Item("EWB_STATUS")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        mc1.CommandText = "SELECT * FROM DATER WHERE D_CODE='" & TextBox13.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox14.Text = dr.Item("D_NAME")
            dr.Close()
        End If
        conn.Close()
        conn.Open()
        mc1.CommandText = "SELECT * FROM DATER WHERE D_CODE='" & TextBox15.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox16.Text = dr.Item("D_NAME")
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList18_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList18.SelectedIndexChanged
        If (DropDownList18.Text = "1") Then
            TextBox51.Text = "Due to Break Down"
        ElseIf (DropDownList18.Text = "2") Then
            TextBox51.Text = "Due to Transshipment"
        ElseIf (DropDownList18.Text = "3") Then
            TextBox51.Text = "Wrong data entry"
        ElseIf (DropDownList18.Text = "4") Then
            TextBox51.Text = "First Time"
        End If
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'check
        If DropDownList6.Text = "Select" Then
            DropDownList6.Focus()
            Label35.Text = "Please select fiscal year."
            Return
        ElseIf DropDownList7.Text = "Select" Then
            DropDownList7.Focus()
            Label35.Text = "Please select invoice number."
            Return
        ElseIf TextBox50.Text = "" Then
            TextBox50.Focus()
            Label35.Text = "Please enter new truck number."
            Return
        ElseIf TextBox50.Text = TextBox49.Text Then
            TextBox50.Focus()
            TextBox50.Text = ""
            Label35.Text = "New truck number cannot be same as old truck number."
            Return
        ElseIf DropDownList18.Text = "Select" Then
            DropDownList18.Focus()
            Label35.Text = "Please select updation reason."
            Return
        ElseIf DropDownList19.Text = "Select" Then
            DropDownList19.Focus()
            Label35.Text = "Please select state from which vehicle update is required."
            Return
        Else
            Label35.Text = ""

            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Dim gst_code, my_gst_code As New String("")
                conn.Open()
                mycommand.CommandText = "select * from dater where d_code='" & TextBox13.Text & "'"
                mycommand.Connection = conn
                dr = mycommand.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    gst_code = dr.Item("gst_code")
                    dr.Close()
                End If
                conn.Close()

                'search company profile compair gst code
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

                Try
                    If gst_code = my_gst_code Then
                        ''E-Invoice is not required
                    Else
                        Dim logicClassObj = New EinvoiceLogicClassEY
                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(DropDownList8.SelectedValue, TextBox32.Text)
                        If (AuthErrorData.Item(0).status = "1") Then

                            Dim EinvErrorData As List(Of EinvoiceFromIRNErrorDetailsClassEY) = logicClassObj.UpdatePartBOfEwayBill(AuthErrorData.Item(0).Idtoken, AuthErrorData.Item(0).Access_token, my_gst_code, TextBox54.Text, TextBox50.Text, TextBox53.Text, DropDownList19.Text.Substring(0, DropDownList19.Text.IndexOf(",") - 1).Trim, DropDownList18.Text, TextBox51.Text)
                            If (EinvErrorData.Item(0).status = "1") Then
                                TextBox36.Text = EinvErrorData.Item(0).EwbNo
                                TextBox46.Text = EinvErrorData.Item(0).EwbValidTill

                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set EWB_NO ='" & EinvErrorData.Item(0).EwbNo & "',EWB_DATE ='" & EinvErrorData.Item(0).EwbDt & "',EWB_VALIDITY ='" & EinvErrorData.Item(0).EwbValidTill & "', EWB_STATUS ='ACTIVE' where IRN_NO ='" & EinvErrorData.Item(0).IRN & "'"
                                Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                                despatch.ExecuteReader()
                                despatch.Dispose()
                                goAheadFlag = True

                            ElseIf (EinvErrorData.Item(0).status = "2") Then
                                Label70.Visible = True
                                Label73.Visible = True
                                lblEwaybillFromIRNErrorCode.Visible = True
                                lblEwaybillFromIRNErrorMessage.Visible = True
                                lblEwaybillFromIRNErrorCode.Text = EinvErrorData.Item(0).errorCode
                                lblEwaybillFromIRNErrorMessage.Text = EinvErrorData.Item(0).errorMessage
                                goAheadFlag = False
                                Label68.Text = "There is some response error in E-Invoice generation."
                            ElseIf (EinvErrorData.Item(0).status = "3") Then
                                Label70.Visible = True
                                Label73.Visible = True
                                lblEwaybillFromIRNErrorCode.Visible = True
                                lblEwaybillFromIRNErrorMessage.Visible = True
                                lblEwaybillFromIRNErrorCode.Text = EinvErrorData.Item(0).errorfield
                                lblEwaybillFromIRNErrorMessage.Text = EinvErrorData.Item(0).errordesc
                                goAheadFlag = False
                                Label68.Text = "There is some response error in E-Invoice generation."
                            ElseIf (EinvErrorData.Item(0).status = "4") Then
                                Label70.Visible = True
                                Label73.Visible = True
                                lblEwaybillFromIRNErrorCode.Visible = True
                                lblEwaybillFromIRNErrorMessage.Visible = True
                                lblEwaybillFromIRNErrorCode.Text = EinvErrorData.Item(0).infoErrorCode
                                lblEwaybillFromIRNErrorMessage.Text = EinvErrorData.Item(0).infoErrorMessage
                                goAheadFlag = False
                                Label68.Text = "There is error in E-way bill generation, please generate E-way bill alone with above IRN."

                            End If

                        ElseIf (AuthErrorData.Item(0).status = "2") Then

                            Label70.Visible = True
                            Label73.Visible = True
                            lblEwaybillFromIRNErrorCode.Visible = True
                            lblEwaybillFromIRNErrorMessage.Visible = True
                            lblEwaybillFromIRNErrorCode.Text = AuthErrorData.Item(0).errorCode
                            lblEwaybillFromIRNErrorMessage.Text = AuthErrorData.Item(0).errorMessage
                            goAheadFlag = False
                            Label68.Text = "There is some response error in E-invoice Authentication."
                        Else
                            goAheadFlag = False
                            Label68.Text = "There is some response error in E-invoice Authentication."
                        End If


                    End If

                    If (goAheadFlag = True) Then
                        myTrans.Commit()
                        Label70.Visible = False
                        Label73.Visible = False
                        lblEwaybillFromIRNErrorCode.Visible = False
                        lblEwaybillFromIRNErrorMessage.Visible = False
                        Label68.Text = "All records are written to database."
                    Else
                        myTrans.Rollback()
                        conn.Close()
                        conn_trans.Close()
                        TextBox36.Text = ""
                        TextBox46.Text = ""
                        TextBox6.Text = ""

                    End If

                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    TextBox36.Text = ""
                    TextBox46.Text = ""
                    Label68.Text = "There was some Error, please contact EDP."
                Finally
                    conn.Close()
                    conn_trans.Close()
                End Try
            End Using
        End If
    End Sub
End Class