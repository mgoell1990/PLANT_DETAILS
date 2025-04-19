Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class r_issue
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Delvdate7_CalendarExtender.EndDate = DateTime.Now.Date

            Dim ds5 As New DataSet
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT (MAT_DETAILS.ISSUE_NO + ' , ' + MATERIAL .MAT_NAME) AS ISSUE_NO  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code 
                                    WHERE DEPT_CODE ='RM' AND POST_TYPE ='AUTH' AND LINE_DATE IS NULL order by ISSUE_NO", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList11.Items.Clear()
            DropDownList11.DataSource = dt
            DropDownList11.DataValueField = "ISSUE_NO"
            DropDownList11.DataBind()
            DropDownList11.Items.Insert(0, "Select")
            DropDownList11.SelectedValue = "Select"

            Textbox31.Attributes.Add("readonly", "readonly")
            TextBox163.Attributes.Add("readonly", "readonly")
            TextBox172.Attributes.Add("readonly", "readonly")
            TextBox173.Attributes.Add("readonly", "readonly")
            TextBox4.Attributes.Add("readonly", "readonly")
            TextBox5.Attributes.Add("readonly", "readonly")
            TextBox12.Attributes.Add("readonly", "readonly")
            TextBox13.Attributes.Add("readonly", "readonly")
            TextBox2.Attributes.Add("readonly", "readonly")
            TextBox167.Attributes.Add("readonly", "readonly")
            TextBox166.Attributes.Add("readonly", "readonly")
            TextBox168.Attributes.Add("readonly", "readonly")
            TextBox169.Attributes.Add("readonly", "readonly")
            TextBox170.Attributes.Add("readonly", "readonly")
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click



        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                Dim working_date As Date
                If TextBox1.Text = "" Then
                    TextBox1.Focus()
                    Return
                ElseIf IsDate(TextBox1.Text) = False Then
                    TextBox1.Focus()
                    Return
                End If
                working_date = CDate(TextBox1.Text)
                ''UPDATE ISSUE
                If IsNumeric(TextBox163.Text) = False Then
                    ISSUE_ERR_LABEL.Text = "Please Enter Numeric Value"
                    ISSUE_ERR_LABEL.Visible = True
                    TextBox163.Text = ""
                    TextBox163.Focus()
                    Return
                ElseIf CDec(TextBox163.Text) < CDec(TextBox3.Text) Then
                    ISSUE_ERR_LABEL.Text = "Required Qty is lower than Issue Qty"
                    ISSUE_ERR_LABEL.Visible = True
                    TextBox3.Focus()
                    Return
                ElseIf CDec(TextBox167.Text) < CDec(TextBox3.Text) Then
                    ISSUE_ERR_LABEL.Text = "Stock Qty is lower than Issue Qty"
                    ISSUE_ERR_LABEL.Visible = True
                    TextBox3.Focus()
                    Return
                ElseIf CDec(TextBox3.Text) > CDec(TextBox163.Text) Then
                    If TextBox2.Text = "" Then
                        ISSUE_ERR_LABEL.Text = "Please Enter Min Issue Qty Remarks"
                        ISSUE_ERR_LABEL.Visible = True
                        TextBox2.Focus()
                        Return
                    End If
                End If

                'Database updation entry
                If (DropDownList11.Text = "Select") Then
                    ISSUE_ERR_LABEL.Text = "Please select requisition first."
                Else
                    ISSUE_ERR_LABEL.Text = ""

                    ''Checking Issue date and Freeze date
                    Dim Block_DATE As String = ""
                    conn.Open()
                    Dim MC_new As New SqlCommand
                    MC_new.CommandText = "SELECT Block_date FROM Date_Freeze"
                    MC_new.Connection = conn
                    dr = MC_new.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        Block_DATE = dr.Item("Block_date")
                        dr.Close()
                    End If
                    conn.Close()

                    If (CDate(TextBox1.Text) <= CDate(Block_DATE)) Then
                        ISSUE_ERR_LABEL.Visible = True
                        ISSUE_ERR_LABEL.Text = "Issue before " & Block_DATE & " has been freezed."

                    Else

                        Dim STR1 As String = ""
                        If CDate(TextBox5.Text).Date.Month > 3 Then
                            STR1 = Today.Year
                            STR1 = STR1.Trim.Substring(2)
                            STR1 = STR1 & (STR1 + 1)
                        ElseIf CDate(TextBox5.Text).Date.Month <= 3 Then
                            STR1 = Today.Year
                            STR1 = STR1.Trim.Substring(2)
                            STR1 = (STR1 - 1) & STR1
                        End If

                        conn.Open()
                        Dim max_line As Integer
                        max_line = 0
                        Dim MC5 As New SqlCommand
                        MC5.CommandText = "select (CASE WHEN count(line_no) IS NULL THEN '0' ELSE count(line_no) END) AS line_no from MAT_DETAILS where MAT_CODE ='" & Textbox31.Text.Substring(0, Textbox31.Text.IndexOf(",") - 1).Trim & "' and FISCAL_YEAR =" & CInt(STR1) & " and LINE_NO <> 0"
                        MC5.Connection = conn
                        dr = MC5.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            max_line = dr.Item("line_no")
                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If

                        TextBox169.Text = max_line + 1

                        Dim month As Integer
                        month = working_date.Date.Month
                        Dim qtr As String = ""
                        If month = 4 Or month = 5 Or month = 6 Then
                            qtr = "Q1"
                        ElseIf month = 7 Or month = 8 Or month = 9 Then
                            qtr = "Q2"
                        ElseIf month = 10 Or month = 11 Or month = 12 Then
                            qtr = "Q3"
                        ElseIf month = 1 Or month = 2 Or month = 3 Then
                            qtr = "Q4"
                        End If

                        'conn.Open()
                        Dim Query As String = "UPDATE MAT_DETAILS SET ENTRY_DATE=@ENTRY_DATE, AVG_PRICE=@AVG_PRICE, LINE_NO=@LINE_NO,LINE_DATE=@LINE_DATE,ISSUE_QTY=@ISSUE_QTY,MAT_BALANCE=@MAT_BALANCE,UNIT_PRICE=@UNIT_PRICE,TOTAL_PRICE=@TOTAL_PRICE,ISSUE_BY=@ISSUE_BY ,REMARKS=@REMARKS,QTR=@QTR , MAT_QTY=@MAT_QTY WHERE ISSUE_NO ='" & DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim & "'"
                        Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@LINE_NO", max_line + 1)
                        cmd.Parameters.AddWithValue("@REMARKS", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@LINE_DATE", working_date.Date.Date)
                        cmd.Parameters.AddWithValue("@MAT_QTY", 0)
                        cmd.Parameters.AddWithValue("@ISSUE_QTY", CDec(TextBox3.Text))
                        cmd.Parameters.AddWithValue("@MAT_BALANCE", CDec(TextBox167.Text) - CDec(TextBox3.Text))
                        cmd.Parameters.AddWithValue("@UNIT_PRICE", CDec(TextBox166.Text))
                        cmd.Parameters.AddWithValue("@TOTAL_PRICE", CDec(FormatNumber(CDec(TextBox3.Text) * CDec(TextBox166.Text), 2)))
                        cmd.Parameters.AddWithValue("@QTR", qtr)
                        cmd.Parameters.AddWithValue("@ISSUE_BY", Session("userName"))
                        cmd.Parameters.AddWithValue("@AVG_PRICE", CDec(TextBox166.Text))
                        cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
                        cmd.ExecuteReader()
                        cmd.Dispose()
                        'conn.Close()

                        ''UPDATE MATERIAL
                        TextBox167.Text = CDec(TextBox167.Text) - CDec(TextBox3.Text)
                        'conn.Open()
                        Query = "UPDATE MATERIAL SET MAT_STOCK=@MAT_STOCK,LAST_ISSUE_DATE=@LAST_ISSUE_DATE,LAST_TRANS_DATE=@LAST_TRANS_DATE WHERE MAT_CODE ='" & Textbox31.Text.Substring(0, Textbox31.Text.IndexOf(",") - 1) & "'"
                        cmd = New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@MAT_STOCK", CDec(TextBox167.Text))
                        cmd.Parameters.AddWithValue("@LAST_ISSUE_DATE", Date.ParseExact(working_date.Date.Date, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@LAST_TRANS_DATE", Date.ParseExact(working_date.Date.Date, "dd-MM-yyyy", provider))
                        cmd.ExecuteReader()
                        cmd.Dispose()
                        'conn.Close()

                        ''save ledger
                        conn.Open()
                        Dim issue_head, con_head As String
                        issue_head = ""
                        con_head = ""
                        ''Dim MC5 As New SqlCommand
                        MC5.CommandText = "select AC_ISSUE,AC_CON from material WITH(NOLOCK) where MAT_CODE ='" & Textbox31.Text.Substring(0, Textbox31.Text.IndexOf(",") - 1) & "'"
                        MC5.Connection = conn
                        dr = MC5.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            issue_head = dr.Item("AC_ISSUE")
                            con_head = dr.Item("AC_CON")
                            dr.Close()
                            conn.Close()
                        Else
                            conn.Close()
                        End If

                        ''UPDATE LEDGER
                        save_ledger(DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim, issue_head, "Cr", CDec(FormatNumber(CDec(TextBox3.Text) * CDec(TextBox166.Text), 2)), "Material Issue")
                        save_ledger(DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim, con_head, "Dr", CDec(FormatNumber(CDec(TextBox3.Text) * CDec(TextBox166.Text), 2)), "Material Con")

                        Textbox31.Text = ""
                        TextBox3.Text = ""
                        TextBox2.Text = ""
                        TextBox163.Text = ""
                        TextBox166.Text = ""
                        TextBox167.Text = ""
                        TextBox168.Text = ""
                        TextBox169.Text = ""
                        TextBox170.Text = ""
                        TextBox172.Text = ""
                        TextBox173.Text = ""
                        ISSUE_ERR_LABEL.Text = ""
                        TextBox4.Text = ""
                        TextBox5.Text = ""

                        Dim ds5 As New DataSet
                        conn.Open()
                        dt.Clear()
                        da = New SqlDataAdapter("SELECT (MAT_DETAILS.ISSUE_NO + ' , ' + MATERIAL .MAT_NAME) AS ISSUE_NO  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL WITH(NOLOCK) JOIN MAT_DETAILS WITH(NOLOCK) ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code 
                                    WHERE DEPT_CODE ='RM' AND POST_TYPE ='AUTH' AND LINE_DATE IS NULL order by ISSUE_NO", conn)
                        da.Fill(dt)
                        conn.Close()
                        DropDownList11.Items.Clear()
                        DropDownList11.DataSource = dt
                        DropDownList11.DataValueField = "ISSUE_NO"
                        DropDownList11.DataBind()
                        DropDownList11.Items.Insert(0, "Select")
                        DropDownList11.SelectedValue = "Select"
                    End If

                    myTrans.Commit()
                    ISSUE_ERR_LABEL.Visible = True
                    ISSUE_ERR_LABEL.Text = "All records are written to database."
                End If


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                ISSUE_ERR_LABEL.Visible = True
                ISSUE_ERR_LABEL.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub
    Protected Sub save_ledger(issue_no As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
        Dim working_date As Date
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox1.Text) = False Then
            TextBox1.Focus()
            Return
        End If
        working_date = CDate(TextBox1.Text)
        If price > 0 Then
            Dim STR1 As String = ""
            If working_date.Date.Month > 3 Then
                STR1 = working_date.Date.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf working_date.Date.Month <= 3 Then
                STR1 = working_date.Date.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If

            Dim month1 As Integer
            month1 = working_date.Date.Month

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

            'conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn_trans, myTrans)
            cmd.Parameters.AddWithValue("@PO_NO", "")
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", issue_no)
            cmd.Parameters.AddWithValue("@SUPL_ID", "")
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date.Date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.ExecuteReader()
            cmd.Dispose()
            'conn.Close()
        End If
    End Sub

    Protected Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click

        Textbox31.Text = ""
        TextBox3.Text = ""
        TextBox2.Text = ""
        TextBox163.Text = ""
        TextBox166.Text = ""
        TextBox167.Text = ""
        TextBox168.Text = ""
        TextBox169.Text = ""
        TextBox170.Text = ""
        DropDownList11.SelectedValue = "Select"
        TextBox172.Text = ""
        TextBox173.Text = ""
        ISSUE_ERR_LABEL.Text = ""
    End Sub

    Protected Sub DropDownList11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList11.SelectedIndexChanged

        If (DropDownList11.Text <> "Select") Then
            Dim MC As New SqlCommand
            conn.Open()
            MC.CommandText = "Select MAT_DETAILS.ISSUE_NO ,(CAST(MATERIAL .MAT_CODE As VARCHAR(30)) + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code  WHERE 
                MAT_DETAILS .ISSUE_NO like '" & DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim & "' AND DEPT_CODE ='RM' AND POST_TYPE ='AUTH' AND LINE_DATE IS NULL"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Textbox31.Text = dr.Item("MAT_CODE")
                TextBox168.Text = dr.Item("MAT_AU")
                TextBox167.Text = dr.Item("MAT_STOCK")
                TextBox166.Text = dr.Item("MAT_AVG")
                TextBox170.Text = dr.Item("MAT_LOCATION")
                TextBox163.Text = dr.Item("RQD_QTY")
                TextBox3.Text = dr.Item("RQD_QTY")
                TextBox172.Text = dr.Item("ISSUE_TYPE")
                TextBox173.Text = dr.Item("COST_CENT")
                TextBox2.Text = dr.Item("PURPOSE")
                TextBox4.Text = dr.Item("RQD_BY")
                TextBox5.Text = dr.Item("RQD_DATE")

                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If

            Dim STR1 As String = ""
            If CDate(TextBox5.Text).Date.Month > 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = STR1 & (STR1 + 1)
            ElseIf CDate(TextBox5.Text).Date.Month <= 3 Then
                STR1 = Today.Year
                STR1 = STR1.Trim.Substring(2)
                STR1 = (STR1 - 1) & STR1
            End If


            'Dim dr As SqlDataReader
            conn.Open()
            Dim max_line As Integer
            max_line = 0
            Dim MC5 As New SqlCommand
            MC5.CommandText = "select (CASE WHEN count(line_no) IS NULL THEN '0' ELSE count(line_no) END) AS line_no from MAT_DETAILS where MAT_CODE ='" & Textbox31.Text.Substring(0, Textbox31.Text.IndexOf(",") - 1).Trim & "' and FISCAL_YEAR =" & CInt(STR1) & " and LINE_NO <> 0"
            MC5.Connection = conn
            dr = MC5.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                max_line = dr.Item("line_no")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            TextBox169.Text = max_line + 1

        End If



    End Sub
End Class