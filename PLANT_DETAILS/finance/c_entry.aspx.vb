Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class c_entry
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

            Dim DT3 As New DataTable
            DT3.Columns.AddRange(New DataColumn(6) {New DataColumn("TOKEN NO"), New DataColumn("SUPL NAME"), New DataColumn("A/C Head"), New DataColumn("A/C_Description"), New DataColumn("AMOUNT"), New DataColumn("CHEQUE NO"), New DataColumn("CHEQUE DATE")})
            ViewState("CHEQUE_ENTRY") = DT3
            Me.BINDGRID2()
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT VOUCHER .TOKEN_NO from VOUCHER join LEDGER on VOUCHER .TOKEN_NO =LEDGER .VOUCHER_NO  where LEDGER .POST_INDICATION='BANK' AND VOUCHER .CHEQUE_NO IS NULL AND VOUCHER .VOUCHER_TYPE ='B.P.V' order by TOKEN_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList34.Items.Clear()
            DropDownList34.DataSource = dt
            DropDownList34.DataValueField = "TOKEN_NO"
            DropDownList34.DataBind()
            DropDownList34.Items.Insert(0, "Select")
            DropDownList34.SelectedValue = "Select"
        End If
        TextBox88_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        Dim I As Int16 = 0
        For I = 0 To GridView7.Rows.Count - 1
            If GridView7.Rows(I).Cells(0).Text = DropDownList34.SelectedValue Then
                GridView7.Rows(I).Cells(6).Text = TextBox87.Text
                GridView7.Rows(I).Cells(7).Text = TextBox88.Text
            End If
        Next
        Label649.Text = ""
    End Sub

    Protected Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date
                working_date = CDate(TextBox88.Text)
                If GridView7.Rows.Count = 0 Then
                    Label541.Text = "Please Add Data First"
                    Return
                End If

                '''''''''''''''''''''''''''''''''
                ''Checking Cheque entry date and Freeze date
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

                If (Date.ParseExact(TextBox88.Text, "dd-MM-yyyy", Nothing) <= CDate(Block_DATE)) Then
                    Label541.Visible = True
                    Label541.Text = "Cheque entry before " & Block_DATE & " has been freezed."

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
                    Dim I As Integer
                    For I = 0 To GridView7.Rows.Count - 1
                        If GridView7.Rows(I).Cells(6).Text <> "&nbsp;" Then
                            ''DATA SAVE 

                            Dim BPV_NO As String = ""
                            Dim str_temp As String = ""
                            Dim count_cbv_no As Decimal
                            count_cbv_no = 0
                            conn.Open()
                            ds.Clear()
                            da = New SqlDataAdapter("SELECT CVB_NO FROM VOUCHER WITH(NOLOCK) WHERE VOUCHER_TYPE ='B.P.V' AND CVB_NO <>'' AND FISCAL_YEAR=" & STR1, conn)
                            count_cbv_no = da.Fill(dt)
                            conn.Close()
                            If CInt(count_cbv_no) = 0 Then
                                BPV_NO = "BPV" + STR1 + "00001"
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
                                BPV_NO = "BPV" + STR1 + str_temp
                            End If
                            GridView7.Rows(I).Cells(5).Text = BPV_NO
                            Label649.Text = BPV_NO

                            ''UPDATE VOUCHER

                            'mycommand = New SqlCommand("update VOUCHER set BANK_NAME='S.B.I. SECTOR 1 BHILAI', CHEQUE_NO='" & TextBox87.Text & "', CHEQUE_DATE='" & Date.ParseExact(CDate(TextBox88.Text), "yyyy-MM-dd", provider) & "', CVB_NO='" & BPV_NO & "' , CVB_DATE='" & Date.ParseExact(TextBox88.Text, "yyyy-MM-dd", provider) & "' WHERE TOKEN_NO='" & GridView7.Rows(I).Cells(0).Text & "'", conn_trans, myTrans)
                            'mycommand = New SqlCommand("update VOUCHER set BANK_NAME='S.B.I. SECTOR 1 BHILAI', CHEQUE_NO='" & TextBox87.Text & "', CHEQUE_DATE='" & TextBox88.Text & "' WHERE TOKEN_NO='" & GridView7.Rows(I).Cells(0).Text & "'", conn_trans, myTrans)
                            'mycommand.ExecuteNonQuery()

                            mycommand = New SqlCommand("update VOUCHER set BANK_NAME=@BANK_NAME,CHEQUE_NO=@CHEQUE_NO,CHEQUE_DATE=@CHEQUE_DATE,CVB_NO=@CVB_NO,CVB_DATE=@CVB_DATE WHERE TOKEN_NO='" & GridView7.Rows(I).Cells(0).Text & "'", conn_trans, myTrans)
                            mycommand.Parameters.AddWithValue("@BANK_NAME", "S.B.I. SECTOR 1 BHILAI")
                            mycommand.Parameters.AddWithValue("@CHEQUE_NO", TextBox87.Text)
                            mycommand.Parameters.AddWithValue("@CHEQUE_DATE", Date.ParseExact(CDate(TextBox88.Text), "dd-MM-yyyy", provider))
                            mycommand.Parameters.AddWithValue("@CVB_NO", BPV_NO)
                            mycommand.Parameters.AddWithValue("@CVB_DATE", Date.ParseExact(CDate(TextBox88.Text), "dd-MM-yyyy", provider))
                            mycommand.ExecuteNonQuery()
                            mycommand.Dispose()

                            ''UPDATE LEDGER PROBLEM
                            Dim EF_DATE As Date = CDate(GridView7.Rows(I).Cells(7).Text)

                            mycommand = New SqlCommand("update LEDGER set PAYMENT_INDICATION=@PAYMENT_INDICATION,EFECTIVE_DATE=@EFECTIVE_DATE,FISCAL_YEAR=@FISCAL_YEAR WHERE VOUCHER_NO='" & GridView7.Rows(I).Cells(0).Text & "' AND PAYMENT_INDICATION='X'", conn_trans, myTrans)
                            mycommand.Parameters.AddWithValue("@PAYMENT_INDICATION", "OK")
                            mycommand.Parameters.AddWithValue("@EFECTIVE_DATE", Date.ParseExact(CDate(GridView7.Rows(I).Cells(7).Text), "dd-MM-yyyy", provider))
                            mycommand.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                            mycommand.ExecuteNonQuery()
                            mycommand.Dispose()


                            ''UPDATE BILL TRACK ID STATUS
                            Dim query2 As String = "update inv_data set PaymentStatus=@PaymentStatus, PaymentDate=@PaymentDate WHERE VoucherNo='" & GridView7.Rows(I).Cells(0).Text & "'"
                            Dim cmd2 As New SqlCommand(query2, conn_trans, myTrans)
                            cmd2.Parameters.AddWithValue("@PaymentStatus", "Payment Completed")
                            cmd2.Parameters.AddWithValue("@PaymentDate", Date.ParseExact(CDate(GridView7.Rows(I).Cells(7).Text), "dd-MM-yyyy", provider))
                            cmd2.ExecuteReader()
                            cmd2.Dispose()


                        Else
                            Label541.Text = "Please Add Cheque No And Date"
                            Return
                        End If
                    Next

                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("select DISTINCT VOUCHER .TOKEN_NO from VOUCHER WITH(NOLOCK) join LEDGER WITH(NOLOCK) on VOUCHER .TOKEN_NO =LEDGER .VOUCHER_NO  where LEDGER .POST_INDICATION='BANK' AND VOUCHER .CHEQUE_NO IS NULL AND VOUCHER .VOUCHER_TYPE ='B.P.V' order by TOKEN_NO", conn)
                    da.Fill(dt)
                    conn.Close()
                    DropDownList34.Items.Clear()
                    DropDownList34.DataSource = dt
                    DropDownList34.DataValueField = "TOKEN_NO"
                    DropDownList34.DataBind()
                    DropDownList34.Items.Add("Select")
                    DropDownList34.SelectedValue = "Select"
                    Dim DT3 As New DataTable
                    DT3.Columns.AddRange(New DataColumn(6) {New DataColumn("TOKEN NO"), New DataColumn("SUPL NAME"), New DataColumn("A/C Head"), New DataColumn("A/C_Description"), New DataColumn("AMOUNT"), New DataColumn("CHEQUE NO"), New DataColumn("CHEQUE DATE")})
                    ViewState("CPV_ENTRY") = DT3
                    Me.BINDGRID2()

                    myTrans.Commit()
                    Label541.Text = "All records are written to database."
                End If


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label541.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub
    Protected Sub BINDGRID2()
        GridView7.DataSource = DirectCast(ViewState("CHEQUE_ENTRY"), DataTable)
        GridView7.DataBind()
    End Sub

    Protected Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Dim DT3 As New DataTable
        DT3.Columns.AddRange(New DataColumn(6) {New DataColumn("TOKEN NO"), New DataColumn("SUPL NAME"), New DataColumn("A/C Head"), New DataColumn("A/C_Description"), New DataColumn("AMOUNT"), New DataColumn("CHEQUE NO"), New DataColumn("CHEQUE DATE")})
        ViewState("CHEQUE_ENTRY") = DT3
        Me.BINDGRID2()
        ''rcd_Panel4.Visible = False
    End Sub

    Protected Sub DropDownList34_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList34.SelectedIndexChanged
        If DropDownList34.SelectedValue = "Select" Then
            DropDownList34.Focus()
            Label532.Text = ""
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select VOUCHER .TOKEN_NO ,VOUCHER.SUPL_ID ,LEDGER.AC_NO ,ACDIC.ac_description ,(LEDGER.AMOUNT_CR +LEDGER.AMOUNT_DR) as AMOUNT_CR from VOUCHER join LEDGER on VOUCHER .TOKEN_NO =LEDGER .VOUCHER_NO JOIN ACDIC ON LEDGER.AC_NO =ACDIC .ac_code where LEDGER .POST_INDICATION='BANK' AND VOUCHER .CHEQUE_NO IS NULL AND VOUCHER .VOUCHER_TYPE ='B.P.V' AND VOUCHER.TOKEN_NO=" & DropDownList34.SelectedValue, conn)
        da.Fill(dt)
        GridView7.DataSource = dt
        GridView7.DataBind()
        conn.Close()


        If (Left((GridView7.Rows(0).Cells(1).Text), 1) = "S") Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "SELECT SUPL.SUPL_NAME FROM VOUCHER JOIN SUPL ON VOUCHER.SUPL_ID=SUPL.SUPL_ID WHERE VOUCHER .TOKEN_NO ='" & DropDownList34.SelectedValue & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                Label532.Text = dr.Item("SUPL_NAME")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "SELECT dater.d_name FROM VOUCHER JOIN dater ON VOUCHER.SUPL_ID=dater.d_code WHERE VOUCHER .TOKEN_NO ='" & DropDownList34.SelectedValue & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows = True Then
                dr.Read()
                Label532.Text = dr.Item("d_name")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        End If

    End Sub
End Class