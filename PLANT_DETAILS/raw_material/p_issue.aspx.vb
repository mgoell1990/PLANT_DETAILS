Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class p_issue
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
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
            da = New SqlDataAdapter("select ( cost_code + ' , ' + cost_centre) AS COST_DETAIL from COST order by COST_CODE", conn)
            da.Fill(dt)
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "COST_DETAIL"
            DropDownList2.DataBind()
            conn.Close()
            DropDownList2.Items.Add("Select")
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
        If TextBox2.Text = "" Then
            TextBox2.Focus()
            Return
        ElseIf IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        End If
        working_date = CDate(TextBox2.Text)
        If DropDownList3.Text = "" Then
            ISSUE_ERR_LABEL.Text = "Please select material code"
            DropDownList3.Focus()
            Return
        ElseIf IsNumeric(TextBox167.Text) = False Then
            DropDownList3.Text = ""
            DropDownList3.Focus()
            ISSUE_ERR_LABEL.Text = "Please select proper material code"
            Return
        ElseIf TextBox163.Text = "" Then
            TextBox163.Focus()
            ISSUE_ERR_LABEL.Text = "Please enter qty "
            Return
        ElseIf IsNumeric(TextBox163.Text) = False Then
            TextBox163.Text = ""
            TextBox163.Focus()
            ISSUE_ERR_LABEL.Text = "Please enter numeric value in qty "
            Return
        ElseIf TextBox163.Text = 0 Or TextBox163.Text < 0 Then
            TextBox163.Text = ""
            TextBox163.Focus()
            ISSUE_ERR_LABEL.Text = "Please enter qty greater than 0 "

        ElseIf TextBox163.Text > TextBox167.Text Then
            TextBox163.Focus()
            ISSUE_ERR_LABEL.Text = "Issue qty is greater than available stock"
            Return
        ElseIf DropDownList7.SelectedValue = "Select" Then
            DropDownList7.Focus()
            ISSUE_ERR_LABEL.Text = "Please select issue type "
            Return
        ElseIf DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            ISSUE_ERR_LABEL.Text = "Please select cost center"
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            ISSUE_ERR_LABEL.Text = "Please enter purpose "
            Return

        End If

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
        ''ISSUE TYPE
        Dim CRR_TYPE As String = ""

        CRR_TYPE = "PI" & STR1
        conn.Open()
        count = 0
        ds.Tables.Clear()
        da = New SqlDataAdapter("select distinct p_i_no from p_issue WHERE p_i_no LIKE '" & CRR_TYPE & "%' and F_YEAR=" & STR1, conn)
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
        conn.Open()
        Dim Query As String = "Insert Into p_issue(F_YEAR,p_i_no,mat_code,i_date,issue_qty,issue_type,cost_code,qtr,purpose,issued_by,p_i_status)VALUES(@f_year,@p_i_no,@mat_code,@i_date,@issue_qty,@issue_type,@cost_code,@qtr,@purpose,@issued_by,@p_i_status)"
        Dim cmd As New SqlCommand(Query, conn)
        cmd.Parameters.AddWithValue("@p_i_no", issue_no.Text)
        cmd.Parameters.AddWithValue("@mat_code", DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1))
        cmd.Parameters.AddWithValue("@i_date", working_date.Date)
        cmd.Parameters.AddWithValue("@issue_qty", CDec(TextBox163.Text))
        cmd.Parameters.AddWithValue("@issue_type", DropDownList7.SelectedValue)
        cmd.Parameters.AddWithValue("@cost_code", DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1))
        cmd.Parameters.AddWithValue("@qtr", qtr)
        cmd.Parameters.AddWithValue("@purpose", TextBox2.Text)
        cmd.Parameters.AddWithValue("@issued_by", Session("userName"))
        cmd.Parameters.AddWithValue("@p_i_status", "P")
        cmd.Parameters.AddWithValue("@f_year", CInt(STR1))
        cmd.ExecuteReader()
        cmd.Dispose()
        conn.Close()
        ISSUE_ERR_LABEL.Text = "Data Saved"
        ISSUE_ERR_LABEL.Visible = True
        TextBox167.Text = CDec(TextBox167.Text) - CDec(TextBox163.Text)
        TextBox2.Text = ""
    End Sub

End Class