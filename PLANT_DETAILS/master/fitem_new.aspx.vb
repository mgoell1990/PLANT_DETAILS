Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration


Public Class fitem_new
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

            TextBox1.Focus()
            'CHPTR HEAD
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct CHPT_CODE  from CHPTR_HEADING ORDER BY CHPT_CODE", conn)
            da.Fill(dt)
            DropDownList3.Items.Clear()
            DropDownList3.DataSource = dt
            DropDownList3.DataValueField = "CHPT_CODE"
            DropDownList3.DataBind()
            DropDownList3.Items.Insert(0, "Select")
            DropDownList3.SelectedValue = "Select"
            'ITEM GROUP
            dt.Clear()
            da = New SqlDataAdapter("SELECT DISTINCT qual_code  FROM qual_group ORDER BY qual_code", conn)
            da.Fill(dt)
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "qual_code"
            DropDownList2.DataBind()
            DropDownList2.Items.Insert(0, "Select")
            DropDownList2.SelectedValue = "Select"
            conn.Close()

        End If

        If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

 

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Label11.Text = "Please enter Item code"
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            Label11.Text = "Please enter Item name"
            Return
        ElseIf TextBox3.Text = "" Then
            TextBox3.Focus()
            Label11.Text = "Please enter Item drawing "
            Return
        ElseIf TextBox4.Text = "" Or IsNumeric(TextBox4.Text) = False Or CDec(TextBox4.Text) = 0 Then
            TextBox4.Focus()
            Label11.Text = "Please enter Item Weight "
            Return
        ElseIf DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Label11.Text = "Please select Item AU "
            Return
        ElseIf DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Label11.Text = "Please select Item group "
            Return
        ElseIf DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Label11.Text = "Please select chapter heading"
            Return
        ElseIf DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Label11.Text = "Please select item status "
            Return
        ElseIf TextBox5.Text = "" Then
            TextBox5.Focus()
            Label11.Visible = True
            Label11.Text = "Please enter Admin Password"
            Return
        ElseIf TextBox5.Text <> "123456987" Then
            TextBox5.Text = ""
            TextBox5.Focus()
            Label11.Visible = True
            Label11.Text = "Incorrect Admin Password"
            Return
        ElseIf TextBox5.Text = "123456987" Then


            'Using conn_trans
            '    conn_trans.Open()
            '    myTrans = conn_trans.BeginTransaction()

            '    Try
            '        'Database updation entry


            '        myTrans.Commit()

            '        Label6.Text = "All records are written to database."
            '    Catch ee As Exception
            '        ' Roll back the transaction. 
            '        myTrans.Rollback()
            '        conn.Close()
            '        conn_trans.Close()
            '        Label6.Text = "There was some Error, please contact EDP."
            '    Finally
            '        conn.Close()
            '        conn_trans.Close()
            '    End Try

            'End Using

            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry
                    'validatation
                    conn.Open()
                    count = 0
                    ds.Tables.Clear()
                    da = New SqlDataAdapter("select ITEM_CODE from F_ITEM WHERE ITEM_CODE LIKE '" & TextBox1.Text & "'", conn)
                    count = da.Fill(ds, "F_ITEM")
                    conn.Close()
                    If count = 0 Then
                        'Not Exists new
                        'conn.Open()
                        Dim cmd As New SqlCommand
                        Dim Query As String = "Insert Into F_ITEM(ITEM_WEIGHT,ITEM_F_STOCK,ITEM_B_STOCK,ITEM_OPEN_F_STOCK,ITEM_OPEN_B_STOCK,ITEM_CODE,ITEM_NAME,ITEM_DRAW,ITEM_AU,ITEM_TYPE,ITEM_CHPTR,PROD_STOP_IND) values (@ITEM_WEIGHT,@ITEM_F_STOCK,@ITEM_B_STOCK,@ITEM_OPEN_F_STOCK,@ITEM_OPEN_B_STOCK,@ITEM_CODE,@ITEM_NAME,@ITEM_DRAW,@ITEM_AU,@ITEM_TYPE,@ITEM_CHPTR,@PROD_STOP_IND)"
                        cmd = New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@ITEM_CODE", TextBox1.Text)
                        cmd.Parameters.AddWithValue("@ITEM_NAME", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@ITEM_DRAW", TextBox3.Text)
                        cmd.Parameters.AddWithValue("@ITEM_AU", DropDownList1.Text)
                        cmd.Parameters.AddWithValue("@ITEM_TYPE", DropDownList2.Text)
                        cmd.Parameters.AddWithValue("@ITEM_CHPTR", DropDownList3.Text)
                        cmd.Parameters.AddWithValue("@PROD_STOP_IND", DropDownList4.Text)
                        cmd.Parameters.AddWithValue("@ITEM_F_STOCK", 0.0)
                        cmd.Parameters.AddWithValue("@ITEM_B_STOCK", 0.0)
                        cmd.Parameters.AddWithValue("@ITEM_OPEN_F_STOCK", 0.0)
                        cmd.Parameters.AddWithValue("@ITEM_OPEN_B_STOCK", 0.0)
                        cmd.Parameters.AddWithValue("@ITEM_WEIGHT", CDec(TextBox4.Text))
                        cmd.ExecuteReader()
                        cmd.Dispose()


                    Else
                        ' Exists Update
                        'conn.Open()
                        Dim cmd As New SqlCommand
                        Dim Query As String = "UPDATE F_ITEM SET ITEM_WEIGHT=@ITEM_WEIGHT, ITEM_NAME=@ITEM_NAME,ITEM_DRAW=@ITEM_DRAW,ITEM_AU=@ITEM_AU,ITEM_TYPE=@ITEM_TYPE,ITEM_CHPTR=@ITEM_CHPTR,PROD_STOP_IND=@PROD_STOP_IND where item_code='" & TextBox1.Text & "'"
                        cmd = New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@ITEM_NAME", TextBox2.Text)
                        cmd.Parameters.AddWithValue("@ITEM_DRAW", TextBox3.Text)
                        cmd.Parameters.AddWithValue("@ITEM_AU", DropDownList1.Text)
                        cmd.Parameters.AddWithValue("@ITEM_TYPE", DropDownList2.Text)
                        cmd.Parameters.AddWithValue("@ITEM_CHPTR", DropDownList3.Text)
                        cmd.Parameters.AddWithValue("@PROD_STOP_IND", DropDownList4.Text)
                        cmd.Parameters.AddWithValue("@ITEM_WEIGHT", CDec(TextBox4.Text))
                        cmd.ExecuteReader()
                        cmd.Dispose()
                        conn_trans.Close()


                    End If

                    myTrans.Commit()
                    conn_trans.Close()

                    Label11.Text = "Data updated"
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    Label11.Text = "There was some Error, please contact EDP."
                Finally
                    conn.Close()
                    conn_trans.Close()
                End Try

            End Using



        End If

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        DropDownList1.SelectedValue = "Select"
        DropDownList2.SelectedValue = "Select"
        DropDownList3.SelectedValue = "Select"
        DropDownList4.SelectedValue = "Select"
        Response.Redirect("~/Default")
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        DropDownList1.SelectedValue = "Select"
        DropDownList2.SelectedValue = "Select"
        DropDownList3.SelectedValue = "Select"
        DropDownList4.SelectedValue = "Select"

    End Sub
End Class