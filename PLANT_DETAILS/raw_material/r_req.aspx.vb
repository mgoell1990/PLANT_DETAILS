Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class r_req
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
            TextBox168.Attributes.Add("readonly", "readonly")
            TextBox167.Attributes.Add("readonly", "readonly")
            TextBox166.Attributes.Add("readonly", "readonly")
            TextBox169.Attributes.Add("readonly", "readonly")
            TextBox170.Attributes.Add("readonly", "readonly")
            issue_no.Attributes.Add("readonly", "readonly")
            Delvdate7_CalendarExtender.EndDate = DateTime.Now.Date
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub


    Protected Sub DropDownList7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList7.SelectedIndexChanged

        If DropDownList7.SelectedValue = "To Deptt" Then
            Label24.Text = "Cost Center"
            dt.Clear()
            conn.Open()
            DropDownList2.Items.Clear()
            da = New SqlDataAdapter("select (cost_code+' , '+cost_centre) as cost_center from cost where mat_type='RAW MATERIAL' order by cost_center", conn)
            da.Fill(dt)
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "cost_center"
            DropDownList2.DataBind()
            conn.Close()
            DropDownList2.Items.Insert(0, "Select")
            DropDownList2.SelectedValue = "Select"
        ElseIf DropDownList7.SelectedValue = "To Contractor" Then
            Label24.Text = "W.O. No"
            dt.Clear()
            conn.Open()
            DropDownList2.Items.Clear()
            da = New SqlDataAdapter("select (ORDER_DETAILS .so_no + ' , ' + SUPL.SUPL_NAME) as wo_data  from ORDER_DETAILS join SUPL on ORDER_DETAILS .PARTY_CODE=SUPL.SUPL_ID  where so_no like 'W%'", conn)
            da.Fill(dt)
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "wo_data"
            DropDownList2.DataBind()
            conn.Close()
            DropDownList2.Items.Add("Select")
            DropDownList2.SelectedValue = "Select"

        End If
    End Sub


    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim working_date As Date
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf IsDate(TextBox1.Text) = False Then
            TextBox1.Focus()
            Return
        End If
        working_date = CDate(TextBox1.Text)
        ''save
        If IsNumeric(TextBox163.Text) = False Then
            ISSUE_ERR_LABEL.Text = "Please Enter Numeric Value"
            ISSUE_ERR_LABEL.Visible = True
            TextBox163.Text = ""
            TextBox163.Focus()
            Return
        ElseIf TextBox167.Text = "" Or IsNumeric(TextBox167.Text) = False Then
            ISSUE_ERR_LABEL.Text = "Enter Material Code"
            ISSUE_ERR_LABEL.Visible = True
            DropDownList3.Text = ""
            DropDownList3.Focus()
            Return
        ElseIf TextBox163.Text = "" Or IsNumeric(TextBox163.Text) = False Then
            ISSUE_ERR_LABEL.Text = "Enter Material Code"
            ISSUE_ERR_LABEL.Visible = True
            DropDownList3.Text = ""
            DropDownList3.Focus()
            Return
        ElseIf TextBox163.Text = "" Or IsNumeric(TextBox163.Text) = False Then
            ISSUE_ERR_LABEL.Text = "Enter Material Code"
            ISSUE_ERR_LABEL.Visible = True
            DropDownList3.Text = ""
            DropDownList3.Focus()
            Return
        ElseIf DropDownList7.SelectedValue = "Select" Then
            ISSUE_ERR_LABEL.Text = "Please Select Cost Centre"
            ISSUE_ERR_LABEL.Visible = True
            DropDownList7.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select" Then
            ISSUE_ERR_LABEL.Text = "Please Select Cost Centre"
            ISSUE_ERR_LABEL.Visible = True
            DropDownList2.Focus()
            Return
        ElseIf TextBox2.Text = "" Then
            ISSUE_ERR_LABEL.Text = "Please Enter Issue Purpose"
            ISSUE_ERR_LABEL.Visible = True
            TextBox2.Focus()
            Return
        End If

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select * from material where mat_code ='" & DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1) & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    ISSUE_ERR_LABEL.Text = "Please Enter Material Code"
                    ISSUE_ERR_LABEL.Visible = True
                    DropDownList3.Text = ""
                    DropDownList3.Focus()
                    Return
                End If
                ''Issue no generate
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
                ''ISSUE TYPE
                Dim CRR_TYPE As String = ""

                CRR_TYPE = "RI" & STR1
                conn.Open()
                count = 0
                ds.Tables.Clear()
                da = New SqlDataAdapter("select distinct ISSUE_NO from MAT_DETAILS WHERE ISSUE_NO LIKE '" & CRR_TYPE & "%' and FISCAL_YEAR=" & STR1, conn)
                count = da.Fill(ds, "MAT_DETAILS")
                conn.Close()
                If count = 0 Then

                    issue_no.Text = (CRR_TYPE & "0000001")
                Else
                    str = count + 1
                    If str.Length = 1 Then
                        str = "000000" & (count + 1)
                    ElseIf str.Length = 2 Then
                        str = "00000" & (count + 1)
                    ElseIf str.Length = 3 Then
                        str = "0000" & (count + 1)
                    ElseIf str.Length = 4 Then
                        str = "000" & (count + 1)
                    ElseIf str.Length = 5 Then
                        str = "00" & (count + 1)
                    End If
                    issue_no.Text = (CRR_TYPE & str)
                End If
                'conn.Open()
                Dim Query As String = "Insert Into MAT_DETAILS(DEPT_CODE,RQD_BY,ISSUE_NO,LINE_NO,FISCAL_YEAR,LINE_TYPE,MAT_CODE,RQD_QTY,ISSUE_QTY,MAT_BALANCE,UNIT_PRICE,TOTAL_PRICE,PURPOSE,COST_CODE,ISSUE_TYPE,RQD_DATE)VALUES(@DEPT_CODE,@RQD_BY,@ISSUE_NO,@LINE_NO,@FISCAL_YEAR,@LINE_TYPE,@MAT_CODE,@RQD_QTY,@ISSUE_QTY,@MAT_BALANCE,@UNIT_PRICE,@TOTAL_PRICE,@PURPOSE,@COST_CODE,@ISSUE_TYPE,@RQD_DATE)"
                Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                cmd.Parameters.AddWithValue("@ISSUE_NO", issue_no.Text)
                cmd.Parameters.AddWithValue("@ISSUE_TYPE", DropDownList7.SelectedValue)
                cmd.Parameters.AddWithValue("@LINE_NO", 0)
                cmd.Parameters.AddWithValue("@FISCAL_YEAR", CInt(STR1))
                cmd.Parameters.AddWithValue("@LINE_TYPE", "I")
                cmd.Parameters.AddWithValue("@RQD_DATE", working_date.Date)
                cmd.Parameters.AddWithValue("@MAT_CODE", DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1))
                cmd.Parameters.AddWithValue("@RQD_QTY", CDec(TextBox163.Text))
                cmd.Parameters.AddWithValue("@ISSUE_QTY", 0)
                cmd.Parameters.AddWithValue("@MAT_BALANCE", 0)
                cmd.Parameters.AddWithValue("@UNIT_PRICE", 0)
                cmd.Parameters.AddWithValue("@TOTAL_PRICE", 0)
                cmd.Parameters.AddWithValue("@PURPOSE", TextBox2.Text)
                cmd.Parameters.AddWithValue("@COST_CODE", DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1))
                cmd.Parameters.AddWithValue("@RQD_BY", Session("userName"))
                cmd.Parameters.AddWithValue("@DEPT_CODE", "RM")
                cmd.ExecuteReader()
                cmd.Dispose()
                'conn.Close()


                myTrans.Commit()
                ISSUE_ERR_LABEL.Visible = True
                ISSUE_ERR_LABEL.Text = "All records are written to database."
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

    Protected Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        issue_no.Text = ""
        TextBox163.Text = ""
        TextBox166.Text = ""
        TextBox167.Text = ""
        TextBox168.Text = ""
        TextBox169.Text = ""
        TextBox170.Text = ""
        TextBox2.Text = ""
        DropDownList3.Text = ""
        DropDownList7.SelectedValue = "Select"
        DropDownList2.SelectedValue = "Select"

    End Sub
End Class