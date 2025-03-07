Imports System.Globalization
Imports System.Collections.Generic
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Web.UI
Public Class item_transfer
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
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim qualityType As New String("")
        If Not IsPostBack Then

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct (F_ITEM.ITEM_TYPE + ' , ' + qual_group.qual_name) AS group_name FROM qual_group join F_ITEM  on F_ITEM.ITEM_TYPE=qual_group.qual_code ORDER BY group_name ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "group_name"
            DropDownList2.DataBind()
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM where ITEM_TYPE='" & DropDownList2.Text.Substring(0, (DropDownList2.Text.IndexOf(",") - 1)).Trim & "' ", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "ITEM_CODE"
            DropDownList1.DataBind()
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select * from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label276.Text = dr.Item("ITEM_AU")
                Label277.Text = dr.Item("ITEM_AU")
                Label290.Text = dr.Item("ITEM_AU")
                Label285.Text = dr.Item("ITEM_AU")
                Label287.Text = dr.Item("ITEM_AU")
                TextBox1.Text = dr.Item("ITEM_F_STOCK")
                TextBox50.Text = dr.Item("ITEM_B_STOCK")
                TextBox54.Text = dr.Item("ITEM_F_STOCK")
                TextBox55.Text = dr.Item("ITEM_B_STOCK")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            ''Getting quality group
            conn.Open()
            mc1.CommandText = "select qual_desc from qual_group where qual_code='" & DropDownList2.Text.Substring(0, (DropDownList2.Text.IndexOf(",") - 1)).Trim & "' "
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                qualityType = dr.Item("qual_desc")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()

            conn.Open()
            dt.Clear()
            'da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM where ITEM_TYPE='" & DropDownList2.Text.Substring(0, (DropDownList2.Text.IndexOf(",") - 1)).Trim & "' ", conn)
            da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM join qual_group on F_ITEM.ITEM_TYPE=qual_group.qual_code where qual_desc='" + qualityType + "' order by ITEM_CODE", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList4.DataSource = dt
            DropDownList4.DataValueField = "ITEM_CODE"
            DropDownList4.DataBind()

            Panel1.Visible = True
        End If
        TextBox49_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Dim working_date As Date
        Dim qualityType As New String("")
        working_date = Today.Date
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM where ITEM_TYPE='" & DropDownList2.Text.Substring(0, (DropDownList2.Text.IndexOf(",") - 1)).Trim & "' ", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList1.DataSource = dt
        DropDownList1.DataValueField = "ITEM_CODE"
        DropDownList1.DataBind()
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label276.Text = dr.Item("ITEM_AU")
            Label277.Text = dr.Item("ITEM_AU")
            TextBox1.Text = dr.Item("ITEM_F_STOCK")
            TextBox50.Text = dr.Item("ITEM_B_STOCK")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        ''Getting quality group
        conn.Open()
        mc1.CommandText = "select qual_desc from qual_group where qual_code='" & DropDownList2.Text.Substring(0, (DropDownList2.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            qualityType = dr.Item("qual_desc")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        dt.Clear()

        If (qualityType = "MCB") Then
            da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM join qual_group on F_ITEM.ITEM_TYPE=qual_group.qual_code where (qual_desc='" + qualityType + "' or qual_desc='AMC') order by ITEM_CODE", conn)

        ElseIf (qualityType = "AMC") Then
            da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM join qual_group on F_ITEM.ITEM_TYPE=qual_group.qual_code where (qual_desc='" + qualityType + "' or qual_desc='MCB') order by ITEM_CODE", conn)

        Else
            da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM join qual_group on F_ITEM.ITEM_TYPE=qual_group.qual_code where qual_desc='" + qualityType + "' order by ITEM_CODE", conn)
        End If

        'da = New SqlDataAdapter("select (ITEM_CODE + ' , ' + ITEM_NAME + ' - ' +  convert(varchar,convert(decimal(8,3),ITEM_WEIGHT)) + 'Kg') AS ITEM_CODE from F_ITEM join qual_group on F_ITEM.ITEM_TYPE=qual_group.qual_code order by ITEM_CODE", conn)

        da.Fill(dt)
        conn.Close()
        DropDownList4.DataSource = dt
        DropDownList4.DataValueField = "ITEM_CODE"
        DropDownList4.DataBind()
        conn.Open()
        mc1.CommandText = "select * from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label285.Text = dr.Item("ITEM_AU")
            Label287.Text = dr.Item("ITEM_AU")
            Label290.Text = dr.Item("ITEM_AU")
            TextBox54.Text = dr.Item("ITEM_F_STOCK")
            TextBox55.Text = dr.Item("ITEM_B_STOCK")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Return
        End If
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from F_ITEM where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label276.Text = dr.Item("ITEM_AU")
            Label277.Text = dr.Item("ITEM_AU")
            Label290.Text = dr.Item("ITEM_AU")
            TextBox1.Text = dr.Item("ITEM_F_STOCK")
            TextBox50.Text = dr.Item("ITEM_B_STOCK")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        If DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Transfer" Then
            Button32.Text = "TRANSFER"
            Label291.Visible = True
            Panel2.Visible = True
            Panel3.Visible = True
        ElseIf DropDownList5.SelectedValue = "Phy. Adjust" Then
            Button32.Text = "ADJUST"
            Panel2.Visible = False
            Label291.Visible = False
            Panel3.Visible = True
        End If
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        End If
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from F_ITEM where ITEM_CODE='" & DropDownList4.Text.Substring(0, (DropDownList4.Text.IndexOf(",") - 1)).Trim & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label287.Text = dr.Item("ITEM_AU")
            Label285.Text = dr.Item("ITEM_AU")
            TextBox54.Text = dr.Item("ITEM_F_STOCK")
            TextBox55.Text = dr.Item("ITEM_B_STOCK")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                If DropDownList5.SelectedValue = "Select" Then
                    DropDownList5.Focus()
                    Return
                ElseIf DropDownList5.SelectedValue = "Transfer" Then
                    'transfer
                    If Label276.Text <> Label285.Text Then
                        Label279.Text = "Please Selact Same AU Of Material"
                        Return
                    ElseIf DropDownList1.Text = DropDownList4.Text Then
                        Label279.Text = "Both Are Same Material. Please Select Another"
                        Return
                    ElseIf IsDate(TextBox49.Text) = False Then
                        Label279.Text = "Please Select Date"
                        Return
                    ElseIf DropDownList6.SelectedValue = "Select" Then
                        DropDownList6.Focus()
                        Return
                    ElseIf TextBox56.Text = "" Then
                        Label279.Text = "Please Enter Qty."
                        Return
                    ElseIf IsNumeric(TextBox56.Text) = False Then
                        Label279.Text = "Please Enter Numaric Value In Qty."
                        Return
                    End If
                    If DropDownList6.Text = "Finishing Room" Then
                        If CDec(TextBox1.Text) < CDec(TextBox56.Text) Then
                            Label279.Text = "Qty. Is Higher Than Stock"
                            Return
                        End If
                    ElseIf DropDownList6.Text = "B.S.R." Then
                        If CDec(TextBox50.Text) < CDec(TextBox56.Text) Then
                            Label279.Text = "Qty. Is Higher Than Stock"
                            Return
                        End If
                    End If
                    Dim f_qty, b_qty As Decimal
                    f_qty = 0
                    b_qty = 0
                    If DropDownList6.SelectedValue = "Finishing Room" Then
                        f_qty = CDec(TextBox56.Text)
                        b_qty = 0
                    ElseIf DropDownList6.SelectedValue = "B.S.R." Then
                        b_qty = CDec(TextBox56.Text)
                        f_qty = 0
                    End If

                    Dim fromFGUnitWeight, toFGUnitWeight As Decimal

                    conn.Open()
                    Dim mc1 As New SqlCommand
                    mc1.CommandText = "select * from F_ITEM WITH(NOLOCK) where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        fromFGUnitWeight = dr.Item("ITEM_WEIGHT")

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                    conn.Open()
                    mc1.CommandText = "select * from F_ITEM WITH(NOLOCK) where ITEM_CODE='" & DropDownList4.Text.Substring(0, (DropDownList4.Text.IndexOf(",") - 1)).Trim & "' "
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        toFGUnitWeight = dr.Item("ITEM_WEIGHT")

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()


                    Dim item_code1, item_code2 As String
                    item_code1 = DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim
                    item_code2 = DropDownList4.Text.Substring(0, (DropDownList4.Text.IndexOf(",") - 1)).Trim
                    'save production table FOR TRANSFER
                    ''from
                    PROD_SAVE(fromFGUnitWeight, item_code1, CDate(TextBox49.Text), f_qty * (-1), b_qty * (-1), DropDownList5.SelectedValue, Session("userName"))
                    'to
                    PROD_SAVE(toFGUnitWeight, item_code2, CDate(TextBox49.Text), f_qty, b_qty, DropDownList5.SelectedValue, Session("userName"))
                    conn.Open()
                    ''Dim mc1 As New SqlCommand
                    mc1.CommandText = "select * from F_ITEM WITH(NOLOCK) where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        Label276.Text = dr.Item("ITEM_AU")
                        Label277.Text = dr.Item("ITEM_AU")
                        Label290.Text = dr.Item("ITEM_AU")
                        TextBox1.Text = dr.Item("ITEM_F_STOCK")
                        TextBox50.Text = dr.Item("ITEM_B_STOCK")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                    conn.Open()
                    mc1.CommandText = "select * from F_ITEM WITH(NOLOCK) where ITEM_CODE='" & DropDownList4.Text.Substring(0, (DropDownList4.Text.IndexOf(",") - 1)).Trim & "' "
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        Label287.Text = dr.Item("ITEM_AU")
                        Label285.Text = dr.Item("ITEM_AU")
                        TextBox54.Text = dr.Item("ITEM_F_STOCK")
                        TextBox55.Text = dr.Item("ITEM_B_STOCK")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                    DropDownList5.SelectedValue = "Select"
                    DropDownList6.SelectedValue = "Select"
                    Label279.Text = "stock tranfered"
                ElseIf DropDownList5.SelectedValue = "Phy. Adjust" Then
                    'adjust
                    If IsDate(TextBox49.Text) = False Then
                        Label279.Text = "Please Select Date"
                        Return
                    ElseIf TextBox56.Text = "" Then
                        Label279.Text = "Please Enter Qty."
                        Return
                    ElseIf IsNumeric(TextBox56.Text) = False Then
                        Label279.Text = "Please Enter Numaric Value In Qty."
                        Return

                    End If
                    If DropDownList6.Text = "Finishing Room" Then
                        If CDec(TextBox1.Text) + CDec(TextBox56.Text) < 0 Then
                            Label279.Text = "stock can not be negative"
                            Return
                        End If
                    ElseIf DropDownList6.Text = "B.S.R." Then
                        If CDec(TextBox50.Text) + CDec(TextBox56.Text) < 0 Then
                            Label279.Text = "stock can not be negative"
                            Return
                        End If
                    End If
                    'SAVE PRODUCTION TABLE FOR ADJUSTMENT
                    Dim f_qty, b_qty As Decimal
                    f_qty = 0
                    b_qty = 0
                    If DropDownList6.SelectedValue = "Finishing Room" Then
                        f_qty = CDec(TextBox56.Text)
                        b_qty = 0
                    ElseIf DropDownList6.SelectedValue = "B.S.R." Then
                        b_qty = CDec(TextBox56.Text)
                        f_qty = 0
                    End If


                    Dim item_code1 As String
                    item_code1 = DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim


                    Dim fromFGUnitWeight As Decimal

                    conn.Open()
                    Dim mc1 As New SqlCommand
                    mc1.CommandText = "select * from F_ITEM WITH(NOLOCK) where ITEM_CODE='" & item_code1 & "' "
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        fromFGUnitWeight = dr.Item("ITEM_WEIGHT")

                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()

                    ''''save production table FOR TRANSFER
                    PROD_SAVE(fromFGUnitWeight, item_code1, CDate(TextBox49.Text), f_qty, b_qty, DropDownList5.SelectedValue, Session("userName"))

                    conn.Open()
                    ''Dim mc1 As New SqlCommand
                    mc1.CommandText = "select * from F_ITEM WITH(NOLOCK) where ITEM_CODE='" & DropDownList1.Text.Substring(0, (DropDownList1.Text.IndexOf(",") - 1)).Trim & "' "
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        Label276.Text = dr.Item("ITEM_AU")
                        Label277.Text = dr.Item("ITEM_AU")
                        Label290.Text = dr.Item("ITEM_AU")
                        TextBox1.Text = dr.Item("ITEM_F_STOCK")
                        TextBox50.Text = dr.Item("ITEM_B_STOCK")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                    ''DropDownList5.SelectedValue = "Select"
                    ''DropDownList6.SelectedValue = "Select"

                End If

                myTrans.Commit()
                Label279.Text = "Stock adjusted successfully."

                If DropDownList5.SelectedValue = "Select" Then
                    DropDownList5.Focus()
                    Return
                ElseIf DropDownList5.SelectedValue = "Transfer" Then
                    Button32.Text = "TRANSFER"
                    Label291.Visible = True
                    Panel2.Visible = True
                    Panel3.Visible = True
                ElseIf DropDownList5.SelectedValue = "Phy. Adjust" Then
                    Button32.Text = "ADJUST"
                    Panel2.Visible = False
                    Label291.Visible = False
                    Panel3.Visible = True
                End If

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label279.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Transfer" Then
            Button32.Text = "TRANSFER"
            Label291.Visible = True
            Panel2.Visible = True
            Panel3.Visible = True
        ElseIf DropDownList5.SelectedValue = "Phy. Adjust" Then
            Button32.Text = "ADJUST"
            Panel2.Visible = False
            Label291.Visible = False
            Panel3.Visible = True
        End If
    End Sub

    Protected Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        DropDownList5.SelectedValue = "Select"
        DropDownList6.SelectedValue = "Select"
        TextBox49.Text = ""
        TextBox56.Text = ""
    End Sub
    Private Sub PROD_SAVE(UNIT_WEIGHT As Decimal, ITEM_CODE As String, PROD_DATE As Date, F_QTY As Decimal, B_QTY As Decimal, P_TYPE As String, name_user As String)
        Dim fr_stock, bsr_stock As Decimal
        fr_stock = 0
        bsr_stock = 0
        Dim STR1 As String = ""
        If PROD_DATE.Month > 3 Then
            STR1 = PROD_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf PROD_DATE.Month <= 3 Then
            STR1 = PROD_DATE.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If
        conn.Open()
        Dim mc1 As New SqlCommand

        mc1.CommandText = "select (case when (ITEM_F_STOCK) is null then '0' else (ITEM_F_STOCK) end ) as fr_stock ,(case when (ITEM_B_STOCK) is null then '0' else (ITEM_B_STOCK) end ) as bsr_stock from F_ITEM WITH(NOLOCK) where ITEM_CODE='" & ITEM_CODE & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            fr_stock = dr.Item("fr_stock")
            bsr_stock = dr.Item("bsr_stock")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into PROD_CONTROL(ENTRY_DATE,fiscal_year,name_user,ITEM_CODE,PROD_DATE,ITEM_F_QTY,ITEM_B_QTY,ITEM_I_QTY,ITEM_I_SO,ITEM_F_STOCK,ITEM_B_STOCK,ITEM_I_TOTAL)values(@ENTRY_DATE,@fiscal_year,@name_user,@ITEM_CODE,@PROD_DATE,@ITEM_F_QTY,@ITEM_B_QTY,@ITEM_I_QTY,@ITEM_I_SO,@ITEM_F_STOCK,@ITEM_B_STOCK,@ITEM_I_TOTAL)"
        Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
        cmd1.Parameters.AddWithValue("@ITEM_CODE", ITEM_CODE)
        cmd1.Parameters.AddWithValue("@PROD_DATE", Date.ParseExact(PROD_DATE, "dd-MM-yyyy", provider))
        cmd1.Parameters.AddWithValue("@ITEM_F_QTY", F_QTY)
        cmd1.Parameters.AddWithValue("@ITEM_B_QTY", B_QTY)
        cmd1.Parameters.AddWithValue("@ITEM_I_QTY", 0.0)
        cmd1.Parameters.AddWithValue("@ITEM_I_SO", P_TYPE)
        cmd1.Parameters.AddWithValue("@ITEM_F_STOCK", fr_stock + F_QTY)
        cmd1.Parameters.AddWithValue("@ITEM_B_STOCK", bsr_stock + B_QTY)
        cmd1.Parameters.AddWithValue("@ITEM_I_TOTAL", 0.0)
        cmd1.Parameters.AddWithValue("@name_user", name_user)
        cmd1.Parameters.AddWithValue("@fiscal_year", STR1)
        cmd1.Parameters.AddWithValue("@ENTRY_DATE", Now)
        cmd1.Parameters.AddWithValue("@ITEM_WEIGHT", UNIT_WEIGHT)
        cmd1.ExecuteReader()
        cmd1.Dispose()

        ''update f_item

        QUARY1 = "update F_ITEM set ITEM_F_STOCK=@ITEM_F_STOCK,ITEM_B_STOCK=@ITEM_B_STOCK,ITEM_LAST_PROD=@ITEM_LAST_PROD where ITEM_CODE ='" & ITEM_CODE & "'"
        Dim cmd2 As New SqlCommand(QUARY1, conn_trans, myTrans)
        cmd2.Parameters.AddWithValue("@ITEM_F_STOCK", fr_stock + F_QTY)
        cmd2.Parameters.AddWithValue("@ITEM_B_STOCK", bsr_stock + B_QTY)
        cmd2.Parameters.AddWithValue("@ITEM_LAST_PROD", Date.ParseExact(PROD_DATE, "dd-MM-yyyy", provider))
        cmd2.ExecuteReader()
        cmd2.Dispose()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox49.Text = "" Then
            TextBox49.Focus()
            Return
        ElseIf IsDate(TextBox49.Text) = False Then
            TextBox49.Focus()
            Return
        ElseIf DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        End If
        Dim dpr_date As Date
        dpr_date = CDate(TextBox49.Text)
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        Dim PO_QUARY As String = "select distinct PROD_CONTROL .ITEM_CODE,qual_group.qual_name ,F_ITEM .ITEM_NAME ,F_ITEM .ITEM_AU , " & _
            " '" & dpr_date & "'  AS P_DATE ," _
            & " '" & dpr_date & "'  AS P_DATE_TO , '" & DropDownList5.SelectedValue & "' AS R_TYPE, " _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY))else '0.000' end) as F_QTY, " _
 & " CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_F_QTY*F_ITEM .ITEM_WEIGHT)/1000)  as F_MT," _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_B_QTY))else '0.000' end) AS B_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_B_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS B_MT ," _
 & " (case when f_item.ITEM_AU ='Pcs' then CONVERT(DECIMAL(10,3),sum(PROD_CONTROL.ITEM_I_QTY))else '0.000' end) AS INV_QTY," _
 & " CONVERT(DECIMAL(10,3),SUM(PROD_CONTROL.ITEM_I_QTY*F_ITEM .ITEM_WEIGHT)/1000) AS INV_MT" _
 & " from PROD_CONTROL join F_ITEM on PROD_CONTROL .ITEM_CODE =F_ITEM .ITEM_CODE" _
 & " join qual_group ON F_ITEM .ITEM_TYPE =qual_group .qual_code" _
 & " where PROD_CONTROL .PROD_DATE = '" & dpr_date.Year & "-" & dpr_date.Month & "-" & dpr_date.Day & "' and ITEM_I_SO ='" & DropDownList5.SelectedValue & "'" _
 & " group by PROD_CONTROL .ITEM_CODE,F_ITEM .ITEM_NAME,qual_group.qual_name,F_ITEM .ITEM_AU"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        conn.Close()
        crystalReport.Load(Server.MapPath("~/print_rpt/pr_rpt.rpt"))
        crystalReport.SetDataSource(dt)
        crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Reports/report.pdf"))
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
End Class