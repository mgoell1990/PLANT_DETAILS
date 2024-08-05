Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Public Class Store_PO_Approval
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
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            conn.Open()
            DropDownList10.Items.Clear()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct ORDER_DETAILS.SO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL as SEARCH_PO FROM ORDER_DETAILS where PO_TYPE='STORE MATERIAL' and SO_STATUS='ACTIVE' and FINANCE_approved='No' ORDER BY SEARCH_PO", conn)
            da.Fill(dt)
            DropDownList10.DataSource = dt
            DropDownList10.DataValueField = "SEARCH_PO"
            DropDownList10.DataBind()
            conn.Close()
            DropDownList10.Items.Add("Select")
            DropDownList10.SelectedValue = ("Select")
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub

    Protected Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If DropDownList10.Text = "Select" Then
            DropDownList10.Focus()
            Label552.Text = "Please select Purchase Order"
            Return

        ElseIf TextBox114.Text <> "123321" Then
            TextBox114.Text = ""
            TextBox114.Focus()
            Label552.Text = "Incorrect Admin Password"
            Return
        ElseIf TextBox114.Text = "123321" Then

            conn.Open()
            Dim cmd11 As New SqlCommand
            Dim Query11 As String = "update order_details set finance_approved ='Yes' where SO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            cmd11 = New SqlCommand(Query11, conn)
            cmd11.ExecuteReader()
            cmd11.Dispose()
            conn.Close()

            Label552.Text = "Purchase order approved."

            conn.Open()
            DropDownList10.Items.Clear()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct ORDER_DETAILS.SO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL as SEARCH_PO FROM ORDER_DETAILS where PO_TYPE='STORE MATERIAL' and SO_STATUS='ACTIVE' and FINANCE_approved='No' ORDER BY SEARCH_PO", conn)
            da.Fill(dt)
            DropDownList10.DataSource = dt
            DropDownList10.DataValueField = "SEARCH_PO"
            DropDownList10.DataBind()
            conn.Close()
            DropDownList10.Items.Add("Select")
            DropDownList10.SelectedValue = ("Select")

            TextBox42.Text = ""
        End If
    End Sub

    Protected Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged

        If (DropDownList10.Text = "Select") Then
            TextBox42.Text = ""
        Else
            conn.Open()
            Dim SUPL_NAME As String = ""
            Dim mc As New SqlCommand
            mc.CommandText = "select SUPL.SUPL_NAME,SUPL.SUPL_ID from ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID where ORDER_DETAILS.so_no ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            mc.Connection = conn
            dr = mc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                SUPL_NAME = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
                dr.Close()
            End If
            conn.Close()
            TextBox42.Text = SUPL_NAME
        End If

    End Sub
End Class