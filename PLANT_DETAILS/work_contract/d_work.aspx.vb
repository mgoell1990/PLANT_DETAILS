Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class d_work
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

            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(11) {New DataColumn("WO No"), New DataColumn("Supl Id"), New DataColumn("WO Sl No"), New DataColumn("Work Name"), New DataColumn("A/U"), New DataColumn("Working Date"), New DataColumn("To"), New DataColumn("Deptt"), New DataColumn("Rqd Qty"), New DataColumn("Worked Qty"), New DataColumn("Bal Qty"), New DataColumn("Note")})
            ViewState("d_w") = dt1
            Me.BINDGRID()

            ''SEARCH DEPTT
            dt.Clear()
            conn.Open()
            DropDownList3.Items.Clear()
            da = New SqlDataAdapter("select ( cost_code + ' , ' + cost_centre) AS COST_DETAIL from COST order by COST_CODE", conn)
            da.Fill(dt)
            DropDownList3.DataSource = dt
            DropDownList3.DataValueField = "COST_DETAIL"
            DropDownList3.DataBind()

            conn.Close()
            DropDownList3.Items.Insert(0, "Select")
            DropDownList3.SelectedValue = "Select"

            MultiView1.ActiveViewIndex = 0

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("contractAccess")) Or Session("contractAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

        TextBox1_CalendarExtender.EndDate = DateTime.Now.Date
        TextBox2_CalendarExtender.EndDate = DateTime.Now.Date
    End Sub



    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Return
        End If
        If TextBox2.Text <> "" And IsDate(TextBox2.Text) Then
            Dim mc1 As New SqlCommand
            Dim FROM_DATE, TO_DATE As Date
            FROM_DATE = CDate(TextBox1.Text)
            TO_DATE = CDate(TextBox2.Text)
            conn.Open()
            mc1.CommandText = "select MAX(W_NAME) AS W_NAME,MAX(W_AU) AS W_AU,MAX(W_END_DATE) AS W_END_DATE, (SUM(W_QTY) -SUM(W_COMPLITED)) as bal from WO_ORDER where PO_NO = '" & DropDownList1.Text & "' and w_slno=" & DropDownList2.SelectedValue & " AND AMD_DATE <='" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "'"

            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox3.Text = dr.Item("W_NAME")
                TextBox5.Text = dr.Item("W_AU")
                TextBox10.Text = dr.Item("W_END_DATE")
                TextBox11.Text = dr.Item("bal")
                dr.Close()
            Else
                conn.Close()
                Return
            End If
            conn.Close()
        End If
    End Sub

    Protected Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        ''condition
        If DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Return
        ElseIf TextBox1.Text = "" Or IsDate(TextBox1.Text) = False Then
            TextBox1.Focus()
            Return
        ElseIf TextBox2.Text = "" Or IsDate(TextBox2.Text) = False Then
            TextBox2.Focus()
            Return
        ElseIf TextBox4.Text = "" Or IsNumeric(TextBox4.Text) = False Then
            TextBox4.Focus()
            Return
        ElseIf TextBox6.Text = "" Or IsNumeric(TextBox6.Text) = False Then
            TextBox6.Focus()
            Return
        ElseIf DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Return
        ElseIf CDec(TextBox4.Text) > CDec(TextBox11.Text) Then
            TextBox4.Focus()
            Label403.Text = "Worked Qty is greater than balance Qty."
            Return
        End If
        Dim FROM_DATE, TO_DATE, EXP_DATE As Date
        FROM_DATE = CDate(TextBox1.Text)
        TO_DATE = CDate(TextBox2.Text)
        EXP_DATE = CDate(TextBox10.Text)

        If FROM_DATE > TO_DATE Then
            TextBox1.Focus()
            Label403.Text = "From date cannot be greater than to date"
            Return
        Else
            Label403.Text = ""
        End If

        If EXP_DATE < FROM_DATE Then
            TextBox1.Focus()
            Return
        ElseIf EXP_DATE < TO_DATE Then
            TextBox2.Focus()
            Return
        End If

        Dim I As Integer = 0
        For I = 0 To GridView1.Rows.Count - 1
            If DropDownList2.SelectedValue = GridView1.Rows(I).Cells(2).Text And DropDownList1.Text = GridView1.Rows(I).Cells(0).Text Then
                DropDownList2.Focus()
                Return
            End If
        Next

        Dim dt9 As DataTable = DirectCast(ViewState("d_w"), DataTable)
        dt9.Rows.Add(DropDownList1.Text, TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1), DropDownList2.SelectedValue, TextBox3.Text, TextBox5.Text, TextBox1.Text, TextBox2.Text, DropDownList3.Text.Substring(0, DropDownList3.Text.IndexOf(",") - 1), TextBox6.Text, TextBox4.Text, TextBox11.Text - TextBox4.Text, TextBox8.Text)
        ViewState("d_w") = dt9
        Me.BINDGRID()
    End Sub
    Protected Sub BINDGRID()
        GridView1.DataSource = DirectCast(ViewState("d_w"), DataTable)
        GridView1.DataBind()
    End Sub

    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        If GridView1.Rows.Count = 0 Then
            Return
        End If
        Panel8.Visible = True
    End Sub

    Protected Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Dim dt1 As New DataTable()
        dt1.Columns.AddRange(New DataColumn(12) {New DataColumn("WO No"), New DataColumn("Supl Id"), New DataColumn("WO Sl No"), New DataColumn("Work Name"), New DataColumn("A/U"), New DataColumn("Working Date"), New DataColumn("To"), New DataColumn("Deptt"), New DataColumn("Rqd Qty"), New DataColumn("Worked Qty"), New DataColumn("Bal Qty"), New DataColumn("Penality"), New DataColumn("Note")})
        ViewState("d_w") = dt1
        Me.BINDGRID()
        Panel8.Visible = False
    End Sub

    Protected Sub Button60_Click(sender As Object, e As EventArgs) Handles Button60.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                If TextBox32.Text = "" Then
                    TextBox32.Focus()
                    Return
                End If
                Dim password As String = ""
                conn.Open()
                Dim MC As New SqlCommand
                MC.CommandText = "select emp_password from EmpLoginDetails where (emp_id = '" & Session("userName") & "' or emp_name = '" & Session("userName") & "')"
                MC.Connection = conn
                dr = MC.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    password = dr.Item("emp_password")
                    dr.Close()
                End If
                conn.Close()

                ''verify
                If password = TextBox32.Text Then
                    ''save data in daily work
                    Dim i As Integer = 0
                    For i = 0 To GridView1.Rows.Count - 1
                        Dim UPDATE_DATE As Date
                        UPDATE_DATE = CDate(GridView1.Rows(i).Cells(5).Text)
                        Dim podate As Date
                        Dim mc1 As New SqlCommand
                        conn.Open()
                        mc1.CommandText = "select SO_ACTUAL_DATE from ORDER_DETAILS where so_no='" & DropDownList1.Text & "'"
                        mc1.Connection = conn
                        dr = mc1.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            podate = dr.Item("SO_ACTUAL_DATE")
                            dr.Close()
                        Else
                            conn.Close()
                        End If
                        conn.Close()
                        If podate > UPDATE_DATE Then
                            Label47.Visible = True
                            Label47.Text = "Please check Work Order date"
                            Return
                        Else
                            Label47.Text = ""
                        End If


                        ''work order wise data search
                        conn.Open()
                        Dim w_qty, w_complite, w_unit_price, W_discount As Decimal
                        MC.CommandText = "select (SELECT SUM(W_QTY) FROM WO_ORDER WITH(NOLOCK) WHERE PO_NO='" & GridView1.Rows(i).Cells(0).Text & "' AND W_SLNO =" & GridView1.Rows(i).Cells(2).Text & ") AS W_QTY,(SELECT SUM(W_COMPLITED) FROM WO_ORDER WITH(NOLOCK) WHERE PO_NO='" & GridView1.Rows(i).Cells(0).Text & "' AND W_SLNO =" & GridView1.Rows(i).Cells(2).Text & ") AS W_COMPLITED,SUM(W_UNIT_PRICE) AS W_UNIT_PRICE,SUM(W_DISCOUNT) AS W_DISCOUNT from WO_ORDER WITH(NOLOCK) where PO_NO = '" & GridView1.Rows(i).Cells(0).Text & "' and w_slno=" & GridView1.Rows(i).Cells(2).Text & " AND AMD_DATE <= (SELECT MAX(AMD_DATE) FROM WO_ORDER WITH(NOLOCK) WHERE AMD_DATE <= '" & UPDATE_DATE.Year & "-" & UPDATE_DATE.Month & "-" & UPDATE_DATE.Day & "' AND PO_NO ='" & GridView1.Rows(i).Cells(0).Text & "' AND W_SLNO = " & GridView1.Rows(i).Cells(2).Text & ")"
                        MC.Connection = conn
                        dr = MC.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            w_qty = dr.Item("W_QTY")
                            w_complite = dr.Item("W_COMPLITED")
                            w_unit_price = dr.Item("W_UNIT_PRICE")
                            W_discount = dr.Item("W_DISCOUNT")
                            dr.Close()
                        End If
                        conn.Close()
                        ''service data search
                        Dim cgst, sgst, igst, cess As New Decimal(0)
                        Dim gst_liab As String = ""
                        conn.Open()
                        mc1.CommandText = "select CGST, SGST, IGST, CESS from WO_ORDER WITH(NOLOCK) where PO_NO='" & DropDownList1.Text & "' AND W_SLNO=" & DropDownList2.SelectedValue & ""
                        mc1.Connection = conn
                        dr = mc1.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            cgst = dr.Item("cgst")
                            sgst = dr.Item("sgst")
                            igst = dr.Item("igst")
                            cess = dr.Item("cess")
                            'gst_liab = dr.Item("gst_liab")
                            dr.Close()
                        End If
                        conn.Close()
                        ''calculate work amount
                        Dim base_value, discount_value, cgst_value, sgst_value, igst_value, cess_value As New Decimal(0)

                        base_value = w_unit_price * CDec(GridView1.Rows(i).Cells(9).Text)
                        discount_value = (base_value * W_discount) / 100
                        cgst_value = ((base_value - discount_value) * cgst) / 100
                        sgst_value = ((base_value - discount_value) * sgst) / 100
                        igst_value = ((base_value - discount_value) * igst) / 100
                        cess_value = ((base_value - discount_value) * cess) / 100

                        Dim Query As String = "Insert Into daily_work(po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,deptt,unit_rate,total_amt,mb_gen_ind,pre_by,gst_liab,cgst,sgst,igst,cess)VALUES(@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@deptt,@unit_rate,@total_amt,@mb_gen_ind,@pre_by,@gst_liab,@cgst,@sgst,@igst,@cess)"
                        Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                        cmd.Parameters.AddWithValue("@po_no", GridView1.Rows(i).Cells(0).Text)
                        cmd.Parameters.AddWithValue("@supl_id", GridView1.Rows(i).Cells(1).Text)
                        cmd.Parameters.AddWithValue("@wo_slno", GridView1.Rows(i).Cells(2).Text)
                        cmd.Parameters.AddWithValue("@w_name", GridView1.Rows(i).Cells(3).Text)
                        cmd.Parameters.AddWithValue("@w_au", GridView1.Rows(i).Cells(4).Text)
                        cmd.Parameters.AddWithValue("@from_date", Date.ParseExact(GridView1.Rows(i).Cells(5).Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@to_date", Date.ParseExact(GridView1.Rows(i).Cells(6).Text, "dd-MM-yyyy", provider))
                        cmd.Parameters.AddWithValue("@deptt", GridView1.Rows(i).Cells(7).Text)
                        cmd.Parameters.AddWithValue("@rqd_qty", CDec(GridView1.Rows(i).Cells(8).Text))
                        cmd.Parameters.AddWithValue("@work_qty", GridView1.Rows(i).Cells(9).Text)
                        cmd.Parameters.AddWithValue("@bal_qty", CDec(GridView1.Rows(i).Cells(10).Text))
                        cmd.Parameters.AddWithValue("@note", GridView1.Rows(i).Cells(11).Text)
                        cmd.Parameters.AddWithValue("@unit_rate", w_unit_price)
                        cmd.Parameters.AddWithValue("@total_amt", (base_value - discount_value))
                        cmd.Parameters.AddWithValue("@cgst", cgst_value)
                        cmd.Parameters.AddWithValue("@sgst", sgst_value)
                        cmd.Parameters.AddWithValue("@igst", igst_value)
                        cmd.Parameters.AddWithValue("@cess", cess_value)
                        cmd.Parameters.AddWithValue("@gst_liab", gst_liab)
                        cmd.Parameters.AddWithValue("@mb_gen_ind", "")
                        cmd.Parameters.AddWithValue("@pre_by", Session("userName"))
                        cmd.ExecuteReader()
                        cmd.Dispose()

                        ''UPDATE WORK ORDER
                        str = ""
                        If GridView1.Rows(i).Cells(10).Text <= 0 Then
                            str = "CLEAR"
                        Else
                            str = "PENDING"
                        End If
                        ''SEARCH QUANTITY
                        Dim WORK_DONE As Decimal
                        WORK_DONE = 0
                        conn.Open()
                        MC.CommandText = "SELECT SUM(W_COMPLITED) AS W_COMPLITED FROM WO_ORDER WITH(NOLOCK) WHERE PO_NO='" & GridView1.Rows(i).Cells(0).Text & "' AND W_SLNO ='" & GridView1.Rows(i).Cells(2).Text & "' AND AMD_DATE =(SELECT MAX(AMD_DATE) FROM WO_ORDER WITH(NOLOCK) WHERE PO_NO='" & GridView1.Rows(i).Cells(0).Text & "' AND W_SLNO ='" & GridView1.Rows(i).Cells(2).Text & "' AND AMD_DATE <='" & UPDATE_DATE.Year & "-" & UPDATE_DATE.Month & "-" & UPDATE_DATE.Day & "' ) "
                        MC.Connection = conn
                        dr = MC.ExecuteReader
                        If dr.HasRows Then
                            dr.Read()
                            WORK_DONE = dr.Item("W_COMPLITED")
                            dr.Close()
                        End If
                        conn.Close()

                        mycommand = New SqlCommand("update WO_ORDER set W_COMPLITED=W_COMPLITED+" & CDec(GridView1.Rows(i).Cells(9).Text) & ",W_STATUS='" & str & "'  WHERE W_SLNO='" & GridView1.Rows(i).Cells(2).Text & "' and PO_NO='" & GridView1.Rows(i).Cells(0).Text & "' AND AMD_DATE = (SELECT MAX(AMD_DATE) FROM WO_ORDER WHERE W_SLNO='" & GridView1.Rows(i).Cells(2).Text & "' and PO_NO='" & GridView1.Rows(i).Cells(0).Text & "' AND  AMD_DATE <= '" & UPDATE_DATE.Year & "-" & UPDATE_DATE.Month & "-" & UPDATE_DATE.Day & "' ) ", conn_trans, myTrans)
                        mycommand.ExecuteNonQuery()

                    Next
                    ''refresh
                    Dim dt1 As New DataTable()
                    dt1.Columns.AddRange(New DataColumn(12) {New DataColumn("WO No"), New DataColumn("Supl Id"), New DataColumn("WO Sl No"), New DataColumn("Work Name"), New DataColumn("A/U"), New DataColumn("Working Date"), New DataColumn("To"), New DataColumn("Deptt"), New DataColumn("Rqd Qty"), New DataColumn("Worked Qty"), New DataColumn("Bal Qty"), New DataColumn("Penality"), New DataColumn("Note")})
                    ViewState("d_w") = dt1
                    Me.BINDGRID()
                    Panel8.Visible = False

                    myTrans.Commit()
                    Label403.Text = "All records are written to database."
                Else
                    Label47.Text = "Password mismatch. Please try again"
                End If

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label403.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub

    Protected Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, dater_code, so_status As String
        order_type = ""
        po_type = ""
        SUPL_ID = ""
        SUPL_NAME = ""
        SO_DATE = ""
        freight_term = ""
        ORDER_TO = ""
        dater_code = ""
        so_status = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select  ORDER_DETAILS.ORDER_TO,ORDER_DETAILS.SO_STATUS , ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_ACTUAL_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            po_type = dr.Item("PO_TYPE")
            SO_DATE = dr.Item("SO_ACTUAL_DATE")
            freight_term = dr.Item("FREIGHT_TERM")
            ORDER_TO = dr.Item("ORDER_TO")
            so_status = dr.Item("SO_STATUS")
            dr.Close()
        End If
        conn.Close()

        If order_type = "Work Order" Or so_status = "RCW" Then
            DropDownList1.Text = DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1)


            'panel_mat.Visible = False
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select DISTINCT W_SLNO  from WO_ORDER where PO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1) & "' AND W_STATUS='PENDING' order by w_slno", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = dt
            DropDownList2.DataValueField = "W_SLNO"
            DropDownList2.DataBind()
            DropDownList2.Items.Add("Select")
            DropDownList2.SelectedValue = "Select"
            conn.Open()
            mc1.CommandText = "select (SUPL.SUPL_ID + ' , ' + supl.SUPL_NAME ) as supl_detail from SUPL join ORDER_DETAILS on SUPL.SUPL_ID =ORDER_DETAILS .PARTY_CODE where ORDER_DETAILS .SO_NO ='" & DropDownList26.Text.Substring(0, DropDownList26.Text.IndexOf(",") - 1) & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox12.Text = dr.Item("supl_detail")
                dr.Close()
            Else
                conn.Close()
                Return
            End If
            conn.Close()
            MultiView1.ActiveViewIndex = 1
        End If

    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim dt1 As New DataTable()
        dt1.Columns.AddRange(New DataColumn(12) {New DataColumn("WO No"), New DataColumn("Supl Id"), New DataColumn("WO Sl No"), New DataColumn("Work Name"), New DataColumn("A/U"), New DataColumn("Working Date"), New DataColumn("To"), New DataColumn("Deptt"), New DataColumn("Rqd Qty"), New DataColumn("Worked Qty"), New DataColumn("Bal Qty"), New DataColumn("Penality"), New DataColumn("Note")})
        ViewState("d_w") = dt1
        Me.BINDGRID()
        Panel8.Visible = False
        MultiView1.ActiveViewIndex = 0

    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Try
            If (TextBox2.Text <> "") Then
                Dim mc1 As New SqlCommand
                Dim FROM_DATE, TO_DATE As Date
                FROM_DATE = CDate(TextBox1.Text)
                TO_DATE = CDate(TextBox2.Text)
                conn.Open()
                mc1.CommandText = "select MAX(W_NAME) AS W_NAME,MAX(W_AU) AS W_AU,MAX(W_END_DATE) AS W_END_DATE, (SUM(W_QTY) -SUM(W_COMPLITED)) as bal from WO_ORDER where PO_NO = '" & DropDownList1.Text & "' and w_slno=" & DropDownList2.SelectedValue & " AND AMD_DATE <='" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "'"

                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    TextBox3.Text = dr.Item("W_NAME")
                    TextBox5.Text = dr.Item("W_AU")
                    TextBox10.Text = dr.Item("W_END_DATE")
                    TextBox11.Text = dr.Item("bal")
                    dr.Close()
                Else
                    conn.Close()
                    Return
                End If
                conn.Close()
            End If

        Catch ee As Exception

            Label403.Visible = True
            Label403.Text = "There was some Error in Order Amendment."
        Finally

        End Try
    End Sub
End Class