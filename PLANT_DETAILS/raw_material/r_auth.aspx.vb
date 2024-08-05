Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class r_auth
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

            Dim ds5 As New DataSet
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT (MAT_DETAILS.ISSUE_NO + ' , ' + MATERIAL .MAT_NAME) AS ISSUE_NO  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code 
                                    WHERE DEPT_CODE ='RM' AND POST_TYPE IS NULL and cost.mat_type='RAW MATERIAL' order by ISSUE_NO", conn)

            da.Fill(dt)
            conn.Close()
            DropDownList11.Items.Clear()
            DropDownList11.DataSource = dt
            DropDownList11.DataValueField = "ISSUE_NO"
            DropDownList11.DataBind()
            DropDownList11.Items.Insert(0, "Select")
            DropDownList11.SelectedValue = "Select"


            TextBox31.Attributes.Add("readonly", "readonly")
            TextBox172.Attributes.Add("readonly", "readonly")
            TextBox173.Attributes.Add("readonly", "readonly")
            TextBox1.Attributes.Add("readonly", "readonly")
            TextBox3.Attributes.Add("readonly", "readonly")
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
        ''search issue no
        If TextBox31.Text = "" Then
            ISSUE_ERR_LABEL.Text = "Please Enter Issue No"
            ISSUE_ERR_LABEL.Visible = True
            TextBox31.Text = ""
            TextBox31.Focus()
            Return
        ElseIf TextBox172.Text = "" Then
            ISSUE_ERR_LABEL.Text = "Please Enter Issue No"
            ISSUE_ERR_LABEL.Visible = True
            TextBox172.Text = ""
            TextBox172.Focus()
            Return
        ElseIf TextBox173.Text = "" Then
            ISSUE_ERR_LABEL.Text = "Please Enter Issue No"
            ISSUE_ERR_LABEL.Visible = True
            TextBox173.Text = ""
            TextBox173.Focus()
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
                da = New SqlDataAdapter("select * from MAT_DETAILS where ISSUE_NO ='" & DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim & "' AND DEPT_CODE ='RM' AND POST_TYPE IS NULL", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    ISSUE_ERR_LABEL.Text = "Please Enter Issue No"
                    ISSUE_ERR_LABEL.Visible = True
                    DropDownList11.SelectedValue = "Select"
                    DropDownList11.Focus()
                    Return
                End If
                If IsNumeric(TextBox163.Text) = False Then
                    ISSUE_ERR_LABEL.Text = "Please Enter Numeric Value"
                    ISSUE_ERR_LABEL.Visible = True
                    TextBox163.Text = ""
                    TextBox163.Focus()
                    Return
                End If


                If RadioButtonList1.SelectedIndex = 0 Then
                    'conn.Open()
                    Dim Query As String = "UPDATE MAT_DETAILS SET AUTH_DATE=@AUTH_DATE,POST_TYPE=@POST_TYPE, RQD_QTY=@RQD_QTY,AUTH_BY=@AUTH_BY WHERE ISSUE_NO ='" & DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim & "'"
                    Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@RQD_QTY", CDec(TextBox163.Text))
                    cmd.Parameters.AddWithValue("@POST_TYPE", "AUTH")
                    cmd.Parameters.AddWithValue("@AUTH_BY", Session("userName"))
                    cmd.Parameters.AddWithValue("@AUTH_DATE", Now)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()

                    ISSUE_ERR_LABEL.Text = ""
                    ISSUE_ERR_LABEL.Visible = False

                ElseIf RadioButtonList1.SelectedIndex = 1 Then
                    'conn.Open()
                    Dim Query As String = "UPDATE MAT_DETAILS SET AUTH_DATE=@AUTH_DATE,POST_TYPE=@POST_TYPE,AUTH_BY=@AUTH_BY WHERE ISSUE_NO ='" & DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim & "'"
                    Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@RQD_QTY", CDec(TextBox163.Text))
                    cmd.Parameters.AddWithValue("@POST_TYPE", "CANCEL")
                    cmd.Parameters.AddWithValue("@AUTH_BY", Session("userName"))
                    cmd.Parameters.AddWithValue("@AUTH_DATE", Now)
                    cmd.ExecuteReader()
                    cmd.Dispose()
                    'conn.Close()

                    ISSUE_ERR_LABEL.Text = ""
                    ISSUE_ERR_LABEL.Visible = False
                End If

                Dim ds5 As New DataSet
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("SELECT (MAT_DETAILS.ISSUE_NO + ' , ' + MATERIAL .MAT_NAME) AS ISSUE_NO  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL JOIN MAT_DETAILS WITH(NOLOCK) ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code 
                                    WHERE DEPT_CODE ='RM' AND POST_TYPE IS NULL and cost.mat_type='RAW MATERIAL'", conn)
                da.Fill(dt)
                conn.Close()
                DropDownList11.Items.Clear()
                DropDownList11.DataSource = dt
                DropDownList11.DataValueField = "ISSUE_NO"
                DropDownList11.DataBind()
                DropDownList11.Items.Insert(0, "Select")
                DropDownList11.SelectedValue = "Select"

                TextBox2.Text = ""
                TextBox163.Text = ""
                TextBox166.Text = ""
                TextBox167.Text = ""
                TextBox168.Text = ""
                TextBox170.Text = ""
                DropDownList11.SelectedValue = "Select"
                TextBox172.Text = ""
                ISSUE_ERR_LABEL.Text = ""
                TextBox1.Text = ""
                TextBox3.Text = ""
                TextBox31.Text = ""
                TextBox173.Text = ""

                myTrans.Commit()
                ISSUE_ERR_LABEL.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                ISSUE_ERR_LABEL.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using



    End Sub

    Protected Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        TextBox2.Text = ""
        TextBox163.Text = ""
        TextBox166.Text = ""
        TextBox167.Text = ""
        TextBox168.Text = ""
        TextBox170.Text = ""
        DropDownList11.SelectedValue = "Select"
        TextBox172.Text = ""
        ISSUE_ERR_LABEL.Text = ""
    End Sub

    Protected Sub DropDownList11_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList11.SelectedIndexChanged
        If (DropDownList11.Text <> "Select") Then
            Dim MC As New SqlCommand
            conn.Open()
            MC.CommandText = "Select MAT_DETAILS.ISSUE_NO ,(CAST(MATERIAL .MAT_CODE As VARCHAR(30)) + ' , ' + MATERIAL .MAT_NAME) AS MAT_CODE  ,MATERIAL .MAT_AU ,MAT_STOCK ,MATERIAL .MAT_AVG ,MATERIAL .MAT_LOCATION ,MAT_DETAILS .RQD_QTY ,MAT_DETAILS .ISSUE_TYPE ,(cost .cost_code + ' , ' + cost .cost_centre ) AS COST_CENT,MAT_DETAILS .PURPOSE,MAT_DETAILS.RQD_BY,MAT_DETAILS.RQD_DATE FROM MATERIAL JOIN MAT_DETAILS ON MATERIAL .MAT_CODE =MAT_DETAILS .MAT_CODE JOIN cost ON MAT_DETAILS .COST_CODE =cost .cost_code  WHERE 
                MAT_DETAILS .ISSUE_NO like '" & DropDownList11.Text.Substring(0, DropDownList11.Text.IndexOf(",") - 1).Trim & "' AND DEPT_CODE ='RM' AND POST_TYPE IS NULL and cost.mat_type='RAW MATERIAL'"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox31.Text = dr.Item("MAT_CODE")
                TextBox168.Text = dr.Item("MAT_AU")
                TextBox167.Text = dr.Item("MAT_STOCK")
                TextBox166.Text = dr.Item("MAT_AVG")
                TextBox170.Text = dr.Item("MAT_LOCATION")
                TextBox163.Text = dr.Item("RQD_QTY")
                TextBox172.Text = dr.Item("ISSUE_TYPE")
                TextBox173.Text = dr.Item("COST_CENT")
                TextBox2.Text = dr.Item("PURPOSE")
                TextBox1.Text = dr.Item("RQD_BY")
                TextBox3.Text = dr.Item("RQD_DATE")

                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If



        End If
    End Sub
End Class