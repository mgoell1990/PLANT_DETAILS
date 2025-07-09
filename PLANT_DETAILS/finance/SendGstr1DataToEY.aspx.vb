Imports System.Globalization
Imports System.Data.SqlClient
Imports System.Net
Public Class SendGstr1DataToEY
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
    Dim provider As CultureInfo = CultureInfo.InvariantCulture

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click


        If (DropDownList3.Text = "Credit/Debit Note") Then
            conn.Open()
            Dim mc1 As New SqlCommand

            mc1.CommandText = "SELECT d_type+inv_no AS INVOICE_NO, * FROM DESPATCH where D_TYPE+INV_NO in (select invoice_no from CN_DN_DETAILS where FISCAL_YEAR='" & DropDownList27.Text & "' and DEBIT_CREDIT_NO='" & DropDownList28.Text & "') and INV_DATE in (select ORIGINAL_INVOICE_DATE from CN_DN_DETAILS where FISCAL_YEAR='" & DropDownList27.Text & "' and DEBIT_CREDIT_NO='" & DropDownList28.Text & "')"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                ''''''''''''''''''''''''''''''''
                Using conn_trans
                    conn_trans.Open()
                    myTrans = conn_trans.BeginTransaction()

                    Try
                        Dim logicClassObj = New EinvoiceLogicClassEY
                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(DropDownList28.Text, Label405.Text)
                        If (AuthErrorData.Item(0).status = "1") Then

                            Dim result
                            If (dr.Item("ACC_UNIT") = "Service") Then
                                If (Left(DropDownList28.Text, 2) = "CN") Then
                                    If (dr.Item("BILL_PARTY_GST_N") = "") Then
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "NO", dr.Item("INV_DATE"), "CR")
                                    Else
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "YES", dr.Item("INV_DATE"), "CR")
                                    End If
                                ElseIf (Left(DropDownList28.Text, 2) = "DN") Then
                                    If (dr.Item("BILL_PARTY_GST_N") = "") Then
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "NO", dr.Item("INV_DATE"), "DR")
                                    Else
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "YES", dr.Item("INV_DATE"), "DR")
                                    End If
                                End If


                            Else
                                If (Left(DropDownList28.Text, 2) = "CN") Then
                                    If (dr.Item("BILL_PARTY_GST_N") = "") Then
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "NO", dr.Item("INV_DATE"), "CR")
                                    Else
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "YES", dr.Item("INV_DATE"), "CR")
                                    End If
                                ElseIf (Left(DropDownList28.Text, 2) = "DN") Then
                                    If (dr.Item("BILL_PARTY_GST_N") = "") Then
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "NO", dr.Item("INV_DATE"), "DR")
                                    Else
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", DropDownList28.Text, dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "YES", dr.Item("INV_DATE"), "DR")
                                    End If
                                End If


                            End If


                            Dim sqlQuery As String = ""
                            sqlQuery = "update CN_DN_DETAILS set EY_STATUS ='" & result.ToString() & "' where DEBIT_CREDIT_NO  ='" & DropDownList28.Text & "' AND FISCAL_YEAR='" & DropDownList27.Text & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            myTrans.Commit()
                            lblErrorMsg.Visible = True
                            lblErrorMsg.Text = DropDownList28.Text + " : " + result.ToString()




                            dt.Clear()
                            da = New SqlDataAdapter("select DEBIT_CREDIT_NO as INVOICE_NO from CN_DN_DETAILS where FISCAL_YEAR='" + DropDownList27.Text + "' order by DEBIT_CREDIT_NO", conn)
                            da.Fill(dt)
                            DropDownList28.Items.Clear()
                            DropDownList28.DataSource = dt
                            DropDownList28.DataValueField = "INVOICE_NO"
                            DropDownList28.DataBind()
                            DropDownList28.Items.Insert(0, "Select")



                        ElseIf (AuthErrorData.Item(0).status = "2") Then

                            lblErrorMsg.Visible = True
                            lblErrorMsg.Text = AuthErrorData.Item(0).errorCode

                        End If
                    Catch ee As Exception
                        ' Roll back the transaction. 
                        myTrans.Rollback()
                        conn_trans.Close()
                    Finally
                        conn_trans.Close()
                    End Try
                End Using
                ''''''''''''''''''''''''''''''''
                dr.Close()

            End If
            conn.Close()
        Else
            conn.Open()
            Dim mc1 As New SqlCommand

            mc1.CommandText = "SELECT d_type+inv_no AS INVOICE_NO, * FROM DESPATCH WHERE INV_DATE > '2021-04-30' AND D_TYPE+INV_NO='" & DropDownList28.Text & "' AND FISCAL_YEAR='" & DropDownList27.Text & "' AND (EY_STATUS IS NULL or EY_STATUS='BadRequest' OR EY_STATUS='OK' OR EY_STATUS='NotFound' OR EY_STATUS='0') ORDER BY INVOICE_NO"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                ''''''''''''''''''''''''''''''''
                Using conn_trans
                    conn_trans.Open()
                    myTrans = conn_trans.BeginTransaction()

                    Try
                        Dim logicClassObj = New EinvoiceLogicClassEY
                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(DropDownList28.Text, Label405.Text)
                        If (AuthErrorData.Item(0).status = "1") Then

                            Dim result
                            If (dr.Item("ACC_UNIT") = "Service") Then
                                If (dr.Item("BILL_PARTY_GST_N") = "") Then
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "NO", dr.Item("INV_DATE"), "INV")
                                Else
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "YES", "N", "YES", dr.Item("INV_DATE"), "INV")
                                End If

                            Else
                                If (dr.Item("BILL_PARTY_GST_N") = "") Then
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "NO", dr.Item("INV_DATE"), "INV")
                                Else
                                    result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr.Item("INVOICE_NO"), dr.Item("PARTY_CODE"), dr.Item("CONSIGN_CODE"), "NO", "N", "YES", dr.Item("INV_DATE"), "INV")
                                End If

                            End If


                            Dim sqlQuery As String = ""
                            sqlQuery = "update DESPATCH set EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & DropDownList28.Text & "' AND FISCAL_YEAR='" & DropDownList27.Text & "'"
                            Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()
                            myTrans.Commit()
                            lblErrorMsg.Text = DropDownList28.Text + " : " + result.ToString()

                            'conn.Open()
                            dt.Clear()
                            da = New SqlDataAdapter("SELECT d_type+inv_no AS INVOICE_NO FROM DESPATCH WHERE INV_DATE > '2021-04-30' AND D_TYPE NOT LIKE 'DC%' AND FISCAL_YEAR='" & DropDownList27.Text & "' AND (EY_STATUS IS NULL or EY_STATUS='BadRequest' OR EY_STATUS='OK' OR EY_STATUS='NotFound' OR EY_STATUS='0') ORDER BY INVOICE_NO", conn)
                            da.Fill(dt)
                            'conn.Close()
                            DropDownList28.Items.Clear()
                            DropDownList28.DataSource = dt
                            DropDownList28.DataValueField = "INVOICE_NO"
                            DropDownList28.DataBind()
                            DropDownList28.Items.Insert(0, "Select")
                            DropDownList28.SelectedValue = "Select"

                        ElseIf (AuthErrorData.Item(0).status = "2") Then

                            lblErrorMsg.Visible = True
                            lblErrorMsg.Text = AuthErrorData.Item(0).errorCode

                        End If
                    Catch ee As Exception
                        ' Roll back the transaction. 
                        myTrans.Rollback()
                        conn_trans.Close()
                    Finally
                        conn_trans.Close()
                    End Try
                End Using
                ''''''''''''''''''''''''''''''''
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
        End If

    End Sub

    Protected Sub DropDownList28_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList28.SelectedIndexChanged

        If (DropDownList3.Text = "Credit/Debit Note") Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select * from DESPATCH where D_TYPE+INV_NO in (select invoice_no from CN_DN_DETAILS where FISCAL_YEAR='" & DropDownList27.Text & "' and DEBIT_CREDIT_NO='" & DropDownList28.Text & "') and INV_DATE in (select ORIGINAL_INVOICE_DATE from CN_DN_DETAILS where FISCAL_YEAR='" & DropDownList27.Text & "' and DEBIT_CREDIT_NO='" & DropDownList28.Text & "')"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label405.Text = dr.Item("PARTY_CODE")
                Label407.Text = dr.Item("CONSIGN_CODE")
                Label409.Text = dr.Item("P_CODE")
                Label411.Text = dr.Item("P_DESC")
                Label15.Text = dr.Item("inv_date")
                dr.Close()
            End If
            conn.Close()

            lblErrorMsg.Visible = True
            lblErrorMsg.Text = ""
        Else
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select * from despatch where d_type+inv_no = '" & DropDownList28.Text & "' and fiscal_year='" & DropDownList27.Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label405.Text = dr.Item("PARTY_CODE")
                Label407.Text = dr.Item("CONSIGN_CODE")
                Label409.Text = dr.Item("P_CODE")
                Label411.Text = dr.Item("P_DESC")
                Label15.Text = dr.Item("inv_date")
                dr.Close()
            End If
            conn.Close()

            lblErrorMsg.Visible = True
            lblErrorMsg.Text = ""
        End If



    End Sub

    Protected Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        Try
            conn.Open()
            Dim mc1 As New SqlCommand

            mc1.CommandText = "SELECT d_type+inv_no AS INVOICE_NO, * FROM DESPATCH WHERE INV_DATE > '2021-04-30' AND D_TYPE NOT LIKE 'DC%' AND (EY_STATUS IS NULL or EY_STATUS='0') ORDER BY INVOICE_NO"

            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                'dr.Read()
                While dr.Read()

                    Using conn_trans
                        conn_trans.Open()
                        myTrans = conn_trans.BeginTransaction()

                        Try
                            Dim logicClassObj = New EinvoiceLogicClassEY
                            Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(dr("INVOICE_NO"), dr("PARTY_CODE"))
                            If (AuthErrorData.Item(0).status = "1") Then
                                Dim result
                                If (dr("ACC_UNIT") = "Service") Then
                                    If (dr("BILL_PARTY_GST_N") = "") Then
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr("INVOICE_NO"), dr("PARTY_CODE"), dr("CONSIGN_CODE"), "YES", "N", "NO", dr("INV_DATE"), "INV")
                                    Else
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr("INVOICE_NO"), dr("PARTY_CODE"), dr("CONSIGN_CODE"), "YES", "N", "YES", dr("INV_DATE"), "INV")
                                    End If

                                Else
                                    If (dr("BILL_PARTY_GST_N") = "") Then
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr("INVOICE_NO"), dr("PARTY_CODE"), dr("CONSIGN_CODE"), "NO", "N", "NO", dr("INV_DATE"), "INV")
                                    Else
                                        result = logicClassObj.SubmitGSTR1DataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr("INVOICE_NO"), dr("PARTY_CODE"), dr("CONSIGN_CODE"), "NO", "N", "YES", dr("INV_DATE"), "INV")
                                    End If

                                End If

                                Dim sqlQuery As String = ""
                                sqlQuery = "update DESPATCH set EY_STATUS ='" & result.ToString() & "' where D_TYPE+INV_NO  ='" & DropDownList28.Text & "' AND FISCAL_YEAR='" & DropDownList27.Text & "'"
                                Dim despatch As New SqlCommand(sqlQuery, conn_trans, myTrans)
                                despatch.ExecuteReader()
                                despatch.Dispose()
                                myTrans.Commit()
                                lblErrorMsg.Text = result.ToString()

                            ElseIf (AuthErrorData.Item(0).status = "2") Then

                                lblErrorMsg.Visible = True
                                lblErrorMsg.Text = AuthErrorData.Item(0).errorCode

                            End If
                        Catch ee As Exception
                            ' Roll back the transaction. 
                            myTrans.Rollback()
                            conn_trans.Close()

                        Finally
                            conn_trans.Close()
                        End Try
                    End Using

                End While

                dr.Close()
            Else
                dr.Close()
            End If
        Catch ee As Exception

            conn.Close()

        Finally
            conn.Close()
        End Try


    End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            MultiView2.ActiveViewIndex = -1
            Return

        ElseIf DropDownList9.SelectedValue = "GSTR-1 Data" Then

            MultiView2.ActiveViewIndex = 0

            DropDownList28.Enabled = True
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT d_type+inv_no AS INVOICE_NO FROM DESPATCH WHERE INV_DATE > '2021-04-30' AND D_TYPE NOT LIKE 'DC%' AND (EY_STATUS IS NULL or EY_STATUS='0') ORDER BY INVOICE_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList28.Items.Clear()
            DropDownList28.DataSource = dt
            DropDownList28.DataValueField = "INVOICE_NO"
            DropDownList28.DataBind()
            DropDownList28.Items.Insert(0, "Select")

            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select * from despatch where d_type+inv_no = '" & DropDownList28.Text & "' and fiscal_year='" & DropDownList27.Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label405.Text = dr.Item("PARTY_CODE")
                Label407.Text = dr.Item("CONSIGN_CODE")
                Label409.Text = dr.Item("P_CODE")
                Label411.Text = dr.Item("P_DESC")
                dr.Close()
            End If
            conn.Close()

        ElseIf DropDownList9.SelectedValue = "GSTR-3B Data" Then

            MultiView2.ActiveViewIndex = 1
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim from_date, to_date As Date

        from_date = CDate(TextBox1.Text)
        to_date = CDate(TextBox2.Text)

        Try
            Using conn ''will make certain that the connection Is properly disposed
                conn.Open()

                Dim mc1 As New SqlCommand
                Using mc1 'will make certain that the command Is properly disposed

                    'mc1.CommandText = "SELECT TOP 1 * FROM TAXABLE_VALUES WHERE VALUATION_DATE > '2021-03-31' AND EY_STATUS IS NULL ORDER BY VALUATION_DATE"
                    'mc1.CommandText = "SELECT * FROM TAXABLE_VALUES WHERE VALUATION_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND EY_STATUS IS NULL ORDER BY VALUATION_DATE"
                    'mc1.CommandText = "SELECT * FROM TAXABLE_VALUES WHERE VALUATION_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND EY_STATUS ='CREATED' ORDER BY VALUATION_DATE"
                    mc1.CommandText = "SELECT DISTINCT INVOICE_NO,INVOICE_DATE,DATA_TYPE,SUPL_CODE,SUM(CGST_AMT) AS CGST_AMT,SUM(SGST_AMT) AS SGST_AMT,SUM(IGST_AMT) AS IGST_AMT,
                                        SUM(RCM_CGST_AMT) AS RCM_CGST_AMT,SUM(RCM_SGST_AMT) AS RCM_SGST_AMT,SUM(RCM_IGST_AMT) AS RCM_IGST_AMT FROM TAXABLE_VALUES 
                                        WHERE VALUATION_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' AND EY_STATUS IS NULL GROUP BY INVOICE_NO,INVOICE_DATE,DATA_TYPE,SUPL_CODE having sum(CGST_AMT+SGST_AMT+IGST_AMT+RCM_CGST_AMT+RCM_SGST_AMT+RCM_IGST_AMT)>0 ORDER BY INVOICE_DATE,INVOICE_NO"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows = True Then
                        Using dr 'will make certain that the reader Is properly disposed
                            While (dr.Read())

                                Using conn_transaction As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
                                    conn_transaction.Open()
                                    myTrans = conn_transaction.BeginTransaction()

                                    Try
                                        Dim logicClassObj = New EinvoiceLogicClassEY
                                        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(dr("INVOICE_NO"), dr("SUPL_CODE"))
                                        If (AuthErrorData.Item(0).status = "1") Then

                                            Dim returnPeriod As String = Convert.ToDateTime(from_date).Month.ToString("D2") & Convert.ToDateTime(from_date).Year
                                            Dim result
                                            If (CDec(dr("CGST_AMT")) + CDec(dr("SGST_AMT")) + CDec(dr("IGST_AMT")) <> 0) Then

                                                result = logicClassObj.SubmitGSTR3BDataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr("INVOICE_NO"), dr("INVOICE_DATE"), dr("DATA_TYPE"), from_date, to_date, returnPeriod, dr("SUPL_CODE"), dr("SUPL_CODE"), "N")

                                            ElseIf (CDec(dr("RCM_CGST_AMT")) + CDec(dr("RCM_SGST_AMT")) + CDec(dr("RCM_IGST_AMT")) <> 0) Then

                                                result = logicClassObj.SubmitGSTR3BDataToEYPortal(AuthErrorData.Item(0).Idtoken, New Guid().ToString(), "", dr("INVOICE_NO"), dr("INVOICE_DATE"), dr("DATA_TYPE"), from_date, to_date, returnPeriod, dr("SUPL_CODE"), dr("SUPL_CODE"), "Y")

                                            End If

                                            Dim sqlQuery As String = ""
                                            sqlQuery = "update TAXABLE_VALUES set EY_STATUS ='" & result.ToString() & "' where INVOICE_NO='" & dr("INVOICE_NO") & "' AND INVOICE_DATE='" & Convert.ToDateTime(dr("INVOICE_DATE")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & "' AND SUPL_CODE='" & dr("SUPL_CODE") & "' and VALUATION_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "'"
                                            'sqlQuery = "update TAXABLE_VALUES set EY_STATUS ='" & result.ToString() & "' where INVOICE_NO='" & dr("INVOICE_NO") & "' AND VALUATION_DATE='" & Convert.ToDateTime(dr("VALUATION_DATE")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & "' AND ENTRY_DATE='" & Convert.ToDateTime(dr("ENTRY_DATE")).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) & "'"
                                            'sqlQuery = "update TAXABLE_VALUES set EY_STATUS =NULL where GARN_CRR_MB_NO='" & dr("GARN_CRR_MB_NO") & "' AND VALUATION_DATE='" & Convert.ToDateTime(dr("VALUATION_DATE")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & "' AND ENTRY_DATE='" & Convert.ToDateTime(dr("ENTRY_DATE")).ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) & "'"
                                            Dim despatch As New SqlCommand(sqlQuery, conn_transaction, myTrans)
                                            despatch.ExecuteReader()
                                            despatch.Dispose()
                                            myTrans.Commit()
                                            lblErrorMsg.Text = result.ToString()

                                        ElseIf (AuthErrorData.Item(0).status = "2") Then

                                            lblErrorMsg.Visible = True
                                            lblErrorMsg.Text = AuthErrorData.Item(0).errorCode

                                        End If
                                    Catch ee As Exception
                                        ' Roll back the transaction. 
                                        myTrans.Rollback()
                                        conn_transaction.Close()
                                    Finally

                                        conn_transaction.Close()
                                    End Try
                                End Using

                            End While
                        End Using
                    End If
                End Using
            End Using
        Catch ee As Exception
            ' Roll back the transaction. 

            conn.Close()

        Finally
            conn.Close()

        End Try

        '''''''''''''''''''''''''''''''''''''

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Protected Sub DropDownList27_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList27.SelectedIndexChanged
        DropDownList28.Enabled = True

    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If (DropDownList3.Text = "Credit/Debit Note") Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DEBIT_CREDIT_NO as INVOICE_NO from CN_DN_DETAILS where FISCAL_YEAR='" + DropDownList27.Text + "' order by DEBIT_CREDIT_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList28.Items.Clear()
            DropDownList28.DataSource = dt
            DropDownList28.DataValueField = "INVOICE_NO"
            DropDownList28.DataBind()
            DropDownList28.Items.Insert(0, "Select")
        Else
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT d_type+inv_no AS INVOICE_NO FROM DESPATCH WHERE INV_DATE > '2021-04-30' and FISCAL_YEAR='" + DropDownList27.Text + "' AND D_TYPE NOT LIKE 'DC%' AND (EY_STATUS IS NULL or EY_STATUS='BadRequest' OR EY_STATUS='OK' OR EY_STATUS='NotFound' OR EY_STATUS='0') ORDER BY INVOICE_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList28.Items.Clear()
            DropDownList28.DataSource = dt
            DropDownList28.DataValueField = "INVOICE_NO"
            DropDownList28.DataBind()
            DropDownList28.Items.Insert(0, "Select")
        End If
    End Sub
End Class