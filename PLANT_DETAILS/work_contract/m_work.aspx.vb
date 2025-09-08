Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class m_work
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

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT DISTINCT PO_NO FROM daily_work WHERE mb_gen_ind =''", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList4.Items.Clear()
            DropDownList4.DataSource = dt
            DropDownList4.DataValueField = "PO_NO"
            DropDownList4.DataBind()
            DropDownList4.Items.Insert(0, "Select")
            DropDownList4.SelectedValue = "Select"
            Panel7.Visible = False
            MultiView1.ActiveViewIndex = 0

            Dim dt2 As New DataTable()
            dt2.Columns.AddRange(New DataColumn(8) {New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty"), New DataColumn("op_qty"), New DataColumn("AU")})
            ViewState("m_w") = dt2
            Me.BINDGRID2()
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("contractAccess")) Or Session("contractAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

        CalendarExtender1.EndDate = DateTime.Now.Date
        CalendarExtender2.EndDate = DateTime.Now.Date
        TextBox17_CalendarExtender.EndDate = DateTime.Now.Date
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        If DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT DISTINCT WO_SLNO FROM daily_work WHERE po_no ='" & DropDownList4.SelectedValue & "' AND mb_gen_ind ='' ORDER BY WO_SLNO", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList5.Items.Clear()
        DropDownList5.DataSource = dt
        DropDownList5.DataValueField = "WO_SLNO"
        DropDownList5.DataBind()
        DropDownList5.Items.Insert(0, "Select")
        DropDownList5.SelectedValue = "Select"
        ''SUPL SEARCH
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select (SUPL.SUPL_ID + ' , ' + supl.SUPL_NAME ) as supl_detail from SUPL join ORDER_DETAILS on SUPL.SUPL_ID =ORDER_DETAILS .PARTY_CODE where ORDER_DETAILS .SO_NO ='" & DropDownList4.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox13.Text = dr.Item("supl_detail")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        If DropDownList5.SelectedValue = "Select" Then
            DropDownList5.Focus()
            Return
        End If
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select MAX(W_NAME) AS W_NAME,MAX(W_AU) AS W_AU,MAX(W_END_DATE) AS W_END_DATE, (SUM(W_QTY) - SUM(W_COMPLITED)) as bal  from WO_ORDER where PO_NO ='" & DropDownList4.SelectedValue & "' and W_SLNO =" & DropDownList5.SelectedValue
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox14.Text = dr.Item("W_NAME")
            TextBox15.Text = dr.Item("W_AU")
            TextBox18.Text = dr.Item("W_END_DATE")
            TextBox19.Text = dr.Item("bal")
            dr.Close()
        Else
            conn.Close()
            Return
        End If
        conn.Close()
    End Sub

    Protected Sub Button52_Click(sender As Object, e As EventArgs) Handles Button52.Click
        Dim I1 As Integer = 0
        For I1 = 0 To GridView2.Rows.Count - 1
            If DropDownList5.SelectedValue = GridView2.Rows(I1).Cells(0).Text Then
                DropDownList5.Focus()
                Return
            End If
        Next

        ''GRID VIEW FILLING
        Dim FROM_DATE, TO_DATE As Date
        FROM_DATE = Date.ParseExact(TextBox16.Text, "dd-MM-yyyy", provider)
        'FROM_DATE = String.Format("{0:dd/mm/YYYY}", TextBox16.Text)
        TO_DATE = Date.ParseExact(TextBox17.Text, "dd-MM-yyyy", provider)
        'TO_DATE = String.Format("{0:dd/mm/YYYY}", TextBox17.Text)
        count = 0
        conn.Open()
        Dim dt9 As DataTable = DirectCast(ViewState("m_w"), DataTable)
        da = New SqlDataAdapter("SELECT MAX(wo_slno ) as wo_slno ,MAX(w_name) as w_name,MAX(w_au) as AU,CAST(MIN(from_date) AS DATE) as from_date,MAX(to_date) as to_date,SUM(work_qty) as work_qty,SUM(rqd_qty) as rqd_qty,MIN(bal_qty) as bal_qty ,(SUM(work_qty)+MIN(bal_qty)) as op_qty FROM daily_work WHERE po_no ='" & DropDownList4.SelectedValue & "' AND wo_slno =" & DropDownList5.SelectedValue & " AND from_date  >='" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND to_date <='" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "'  AND mb_gen_ind =''", conn)
        da.Fill(dt9)
        conn.Close()
        ViewState("m_w") = dt9
        BINDGRID2()
        For I1 = 0 To GridView2.Rows.Count - 1
            GridView2.Rows(I1).Cells(7).Text = 0.0
            GridView2.Rows(I1).Cells(10).Text = TextBox23.Text
        Next
        DropDownList4.Enabled = False

    End Sub
    Protected Sub BINDGRID2()
        GridView2.DataSource = DirectCast(ViewState("m_w"), DataTable)
        GridView2.DataBind()
    End Sub

    Protected Sub Button55_Click(sender As Object, e As EventArgs) Handles Button55.Click
        Dim i As Integer = 0
        For i = 0 To GridView2.Rows.Count - 1
            If GridView2.Rows(i).Cells(0).Text = DropDownList5.Text Then
                GridView2.Rows(i).Cells(7).Text = TextBox24.Text
                GridView2.Rows(i).Cells(10).Text = TextBox23.Text
            End If
        Next
    End Sub

    Protected Sub Button53_Click(sender As Object, e As EventArgs) Handles Button53.Click
        ''save m book
        If GridView2.Rows.Count = 0 Then
            DropDownList4.Focus()
            Return
        ElseIf TextBox24.Text = "" Or IsNumeric(TextBox24.Text) = False Then
            TextBox24.Focus()
            Return
        ElseIf TextBox25.Text = "" Then
            TextBox25.Focus()
            Return
        End If
        Panel7.Visible = True
    End Sub

    Protected Sub Button54_Click(sender As Object, e As EventArgs) Handles Button54.Click
        Dim dt2 As New DataTable()
        dt2.Columns.AddRange(New DataColumn(6) {New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty")})
        ViewState("m_w") = dt2
        Me.BINDGRID2()
        DropDownList4.Enabled = True
        Panel7.Visible = False
    End Sub

    Protected Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date
                working_date = CDate(TextBox1.Text)
                If TextBox1.Text = "" Or IsDate(TextBox1.Text) = False Then
                    TextBox1.Focus()
                    Label45.Visible = True
                    Label45.Text = "Please Enter MB date."
                    Return
                ElseIf TextBox31.Text = "" Then
                    TextBox31.Focus()
                    Return
                End If


                '''''''''''''''''''''''''''''''''
                ''Checking GARN date and Freeze date
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
                    Label45.Visible = True
                    Label45.Text = "MB before " & Block_DATE & " has been freezed."

                Else
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
                    If password = TextBox31.Text Then
                        ''mb no generated
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
                        count = 0
                        conn.Open()
                        ds.Clear()
                        da = New SqlDataAdapter("select DISTINCT mb_no FROM mb_book WHERE MB_NO LIKE 'MB%' AND fiscal_year=" & STR1, conn)
                        count = da.Fill(dt)
                        conn.Close()
                        TextBox28.Text = "MB" & STR1 & +(count + 1)
                        ''update daily work
                        Dim FROM_DATE, TO_DATE As Date
                        FROM_DATE = Date.ParseExact(TextBox16.Text, "dd-MM-yyyy", provider)
                        TO_DATE = Date.ParseExact(TextBox17.Text, "dd-MM-yyyy", provider)
                        ''save mb book
                        Dim i As Integer = 0
                        For i = 0 To GridView2.Rows.Count - 1
                            ''CHECK
                            Dim podate As Date
                            Dim mc1 As New SqlCommand
                            conn.Open()
                            mc1.CommandText = "select SO_ACTUAL_DATE from ORDER_DETAILS where so_no='" & DropDownList4.SelectedValue & "'"
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
                            Dim WORK_DATE As Date = CDate(GridView2.Rows(i).Cells(3).Text)
                            If podate > WORK_DATE Then
                                Label45.Visible = True
                                Label45.Text = "Please check Work Order date"
                                Return
                            Else
                                Label45.Text = ""
                            End If

                            Dim supl_type As String = ""
                            conn.Open()
                            Dim mc11 As New SqlCommand
                            mc11.CommandText = "select * from SUPL where SUPL_ID ='" & TextBox13.Text.Substring(0, TextBox13.Text.IndexOf(",") - 1) & "'"
                            mc11.Connection = conn
                            dr = mc11.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                supl_type = dr.Item("SUPL_TYPE")
                                dr.Close()
                            Else
                                conn.Close()
                            End If
                            conn.Close()
                            If supl_type = "" Then
                                Label45.Visible = True
                                Label45.Text = "Please check Supplier Details"
                                Return
                            Else
                                Label45.Text = ""
                            End If

                            ''ACTUAL VALUE

                            mycommand = New SqlCommand("update daily_work set mb_gen_ind='" & TextBox28.Text & "'  WHERE po_no ='" & DropDownList4.SelectedValue & "' AND wo_slno =" & GridView2.Rows(i).Cells(0).Text & " AND from_date  >='" & FROM_DATE.Year & "-" & FROM_DATE.Month & "-" & FROM_DATE.Day & "' AND to_date <='" & TO_DATE.Year & "-" & TO_DATE.Month & "-" & TO_DATE.Day & "'  AND mb_gen_ind =''", conn_trans, myTrans)
                            mycommand.ExecuteNonQuery()

                            ''SEARCH WORK ORDER DATA
                            conn.Open()
                            Dim w_qty, w_complite, w_unit_price, W_discount, mat_price As Decimal
                            Dim TO_SEARCH_DATE As Date
                            TO_SEARCH_DATE = CDate(GridView2.Rows(i).Cells(3).Text)
                            'MC.CommandText = "select (SELECT SUM(W_QTY) FROM WO_ORDER WHERE PO_NO='" & DropDownList4.SelectedValue & "' AND W_SLNO =" & GridView2.Rows(i).Cells(0).Text & ") AS W_QTY,(SELECT SUM(W_COMPLITED) FROM WO_ORDER WHERE PO_NO='" & DropDownList4.SelectedValue & "' AND W_SLNO =" & GridView2.Rows(i).Cells(0).Text & ") AS W_COMPLITED,SUM(W_UNIT_PRICE) AS W_UNIT_PRICE,SUM(W_DISCOUNT) AS W_DISCOUNT,SUM(W_MATERIAL_COST) AS W_MATERIAL_COST from WO_ORDER where PO_NO = '" & DropDownList4.SelectedValue & "' and w_slno=" & GridView2.Rows(i).Cells(0).Text & " AND AMD_DATE <='" & TO_SEARCH_DATE.Year & "-" & TO_SEARCH_DATE.Month & "-" & TO_SEARCH_DATE.Day & "'"
                            MC.CommandText = "select SUM(W_QTY) AS W_QTY,SUM(W_COMPLITED) AS W_COMPLITED,SUM(W_UNIT_PRICE) AS W_UNIT_PRICE,SUM(W_DISCOUNT) AS W_DISCOUNT,SUM(W_MATERIAL_COST) AS W_MATERIAL_COST from WO_ORDER where PO_NO = '" & DropDownList4.SelectedValue & "' and w_slno=" & GridView2.Rows(i).Cells(0).Text & " AND AMD_DATE <='" & TO_SEARCH_DATE.Year & "-" & TO_SEARCH_DATE.Month & "-" & TO_SEARCH_DATE.Day & "'"
                            MC.Connection = conn
                            dr = MC.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                w_qty = dr.Item("W_QTY")
                                w_complite = dr.Item("W_COMPLITED")
                                w_unit_price = dr.Item("W_UNIT_PRICE")
                                W_discount = dr.Item("W_DISCOUNT")
                                mat_price = dr.Item("W_MATERIAL_COST")
                                dr.Close()
                            End If
                            conn.Close()
                            ''service data search
                            Dim cgst, sgst, igst, cess As New Decimal(0)
                            Dim gst_liab As String = ""
                            conn.Open()
                            mc1.CommandText = "select * from s_tax_liability where taxable_service  = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList4.SelectedValue & "')"
                            mc1.Connection = conn
                            dr = mc1.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                cgst = dr.Item("cgst")
                                sgst = dr.Item("sgst")
                                igst = dr.Item("igst")
                                cess = dr.Item("cess")
                                gst_liab = dr.Item("gst_liab")
                                dr.Close()
                            End If
                            conn.Close()
                            ''calculate work amount
                            Dim base_value, discount_value, mat_rate, cgst_value, sgst_value, igst_value, cess_value As New Decimal(0)

                            base_value = w_unit_price * CDec(GridView2.Rows(i).Cells(5).Text)
                            mat_rate = mat_price * CDec(GridView2.Rows(i).Cells(5).Text)
                            discount_value = (base_value * W_discount) / 100
                            cgst_value = ((base_value - discount_value + mat_rate) * cgst) / 100
                            sgst_value = ((base_value - discount_value + mat_rate) * sgst) / 100
                            igst_value = ((base_value - discount_value + mat_rate) * igst) / 100
                            cess_value = ((base_value - discount_value + mat_rate) * cess) / 100

                            Dim cgstp, sgstp, igstp, cessp, cgstl, sgstl, igstl, cessl As New Decimal(0)

                            If supl_type = "Within State" Then
                                If gst_liab = "P" Then
                                    cgstp = cgst_value
                                    sgstp = sgst_value
                                    igstp = 0
                                    cessp = cess_value
                                    cgstl = 0
                                    sgstl = 0
                                    igstl = 0
                                    cessl = 0
                                ElseIf gst_liab = "R" Then
                                    cgstp = 0
                                    sgstp = 0
                                    igstp = 0
                                    cessp = 0
                                    cgstl = cgst_value
                                    sgstl = sgst_value
                                    igstl = 0
                                    cessl = cess_value
                                End If

                            Else
                                If gst_liab = "P" Then
                                    cgstp = 0
                                    sgstp = 0
                                    igstp = igst_value
                                    cessp = cess_value
                                    cgstl = 0
                                    sgstl = 0
                                    igstl = 0
                                    cessl = 0
                                ElseIf gst_liab = "R" Then
                                    cgstp = 0
                                    sgstp = 0
                                    igstp = 0
                                    cessp = 0
                                    cgstl = 0
                                    sgstl = 0
                                    igstl = igst_value
                                    cessl = cess_value
                                End If

                            End If
                            Dim F_YEAR As Integer = CInt(STR1)

                            Dim Query As String = "Insert Into mb_book(unit_price,Entry_Date,mb_no, mb_date, po_no, supl_id, wo_slno, w_name,w_au,from_date, to_date, work_qty, rqd_qty, bal_qty, note, ra_no, prov_amt, pen_amt, sgst, cgst, igst, cess, sgst_liab,cgst_liab, igst_liab,cess_liab, mat_rate, it_amt, pay_ind, fiscal_year,mb_by )VALUES(@unit_price,@Entry_Date,@mb_no, @mb_date, @po_no, @supl_id, @wo_slno, @w_name,@w_au,@from_date, @to_date, @work_qty, @rqd_qty, @bal_qty, @note, @ra_no, @prov_amt, @pen_amt, @sgst, @cgst, @igst, @cess, @sgst_liab,@cgst_liab, @igst_liab,@cess_liab, @mat_rate, @it_amt, @pay_ind, @fiscal_year,@mb_by)"
                            Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@mb_no", TextBox28.Text)
                            cmd.Parameters.AddWithValue("@mb_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@po_no", DropDownList4.SelectedValue)
                            cmd.Parameters.AddWithValue("@supl_id", TextBox13.Text.Substring(0, TextBox13.Text.IndexOf(",") - 1))
                            cmd.Parameters.AddWithValue("@wo_slno", GridView2.Rows(i).Cells(0).Text)
                            cmd.Parameters.AddWithValue("@w_name", GridView2.Rows(i).Cells(1).Text)
                            cmd.Parameters.AddWithValue("@w_au", GridView2.Rows(i).Cells(2).Text)
                            cmd.Parameters.AddWithValue("@from_date", Date.ParseExact(CDate(GridView2.Rows(i).Cells(3).Text), "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@to_date", Date.ParseExact(CDate(GridView2.Rows(i).Cells(4).Text), "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@work_qty", GridView2.Rows(i).Cells(5).Text)
                            cmd.Parameters.AddWithValue("@rqd_qty", GridView2.Rows(i).Cells(6).Text)
                            cmd.Parameters.AddWithValue("@bal_qty", GridView2.Rows(i).Cells(9).Text)
                            cmd.Parameters.AddWithValue("@note", GridView2.Rows(i).Cells(10).Text)
                            cmd.Parameters.AddWithValue("@ra_no", TextBox25.Text)
                            cmd.Parameters.AddWithValue("@prov_amt", Math.Round((base_value - discount_value), 2))
                            cmd.Parameters.AddWithValue("@pen_amt", CDec(GridView2.Rows(i).Cells(7).Text))
                            cmd.Parameters.AddWithValue("@sgst", sgstp)
                            cmd.Parameters.AddWithValue("@cgst", cgstp)
                            cmd.Parameters.AddWithValue("@igst", igstp)
                            cmd.Parameters.AddWithValue("@cess", cessp)
                            cmd.Parameters.AddWithValue("@sgst_liab", sgstl)
                            cmd.Parameters.AddWithValue("@cgst_liab", cgstl)
                            cmd.Parameters.AddWithValue("@igst_liab", igstl)
                            cmd.Parameters.AddWithValue("@cess_liab", cessl)
                            cmd.Parameters.AddWithValue("@mat_rate", mat_rate)
                            cmd.Parameters.AddWithValue("@it_amt", 0)
                            cmd.Parameters.AddWithValue("@pay_ind", "")
                            cmd.Parameters.AddWithValue("@fiscal_year", F_YEAR)
                            cmd.Parameters.AddWithValue("@mb_by", Session("userName"))
                            cmd.Parameters.AddWithValue("@unit_price", w_unit_price)
                            cmd.Parameters.AddWithValue("@Entry_Date", Now)
                            cmd.ExecuteReader()
                            cmd.Dispose()


                            ''search PARTY TYPE
                            Dim PROV_HEAD, EXPND_HEAD, PARTY_TYPE As New String("")
                            conn.Open()
                            Dim MC5 As New SqlCommand
                            MC5.CommandText = "select PARTY_TYPE from SUPL where SUPL_ID='" & TextBox13.Text.Substring(0, TextBox13.Text.IndexOf(",") - 1) & "'"
                            MC5.Connection = conn
                            dr = MC5.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                PARTY_TYPE = dr.Item("PARTY_TYPE")
                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If

                            conn.Open()
                            MC5.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & DropDownList4.SelectedValue & "') and work_type=(select MAX(wo_type) from wo_order where po_no='" & DropDownList4.SelectedValue & "' and w_slno='" & GridView2.Rows(i).Cells(0).Text & "')"
                            MC5.Connection = conn
                            dr = MC5.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                PROV_HEAD = dr.Item("prov_head")
                                EXPND_HEAD = dr.Item("pur_head")
                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If


                            If (PARTY_TYPE = "MSME" Or PARTY_TYPE = "SSI") Then
                                PROV_HEAD = "5110C"
                            End If


                            ''add ledger EXPND
                            save_ledger(DropDownList4.SelectedValue, TextBox28.Text, TextBox13.Text.Substring(0, TextBox13.Text.IndexOf(",") - 1), EXPND_HEAD, "Dr", (base_value - discount_value) + mat_rate, "PUR")
                            ''add ledger PROV
                            save_ledger(DropDownList4.SelectedValue, TextBox28.Text, TextBox13.Text.Substring(0, TextBox13.Text.IndexOf(",") - 1), PROV_HEAD, "Cr", (base_value - discount_value) + mat_rate, "PROV")
                        Next
                        Dim dt2 As New DataTable()
                        dt2.Columns.AddRange(New DataColumn(6) {New DataColumn("wo_slno"), New DataColumn("w_name"), New DataColumn("from_date"), New DataColumn("to_date"), New DataColumn("work_qty"), New DataColumn("rqd_qty"), New DataColumn("bal_qty")})
                        ViewState("m_w") = dt2
                        Me.BINDGRID2()
                        DropDownList4.Enabled = True
                        Panel7.Visible = False
                        TextBox16.Text = ""
                        TextBox17.Text = ""
                        TextBox23.Text = ""
                        TextBox24.Text = ""
                        TextBox25.Text = ""
                        ''refresh
                        conn.Open()
                        dt.Clear()
                        da = New SqlDataAdapter("SELECT DISTINCT PO_NO FROM daily_work WITH(NOLOCK) WHERE mb_gen_ind =''", conn)
                        da.Fill(dt)
                        conn.Close()
                        DropDownList4.Items.Clear()
                        DropDownList4.DataSource = dt
                        DropDownList4.DataValueField = "PO_NO"
                        DropDownList4.DataBind()
                        DropDownList4.Items.Add("Select")
                        DropDownList4.SelectedValue = "Select"
                        DropDownList5.Items.Clear()
                        dt2.Dispose()
                    Else
                        Label45.Text = "Password mismatch. Please try again"
                        TextBox31.Text = ""
                        Return
                    End If
                End If

                myTrans.Commit()
                Label3.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                TextBox28.Text = ""
                Label3.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub
    Protected Sub save_ledger(so_no As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
        Dim working_date As Date
        working_date = CDate(TextBox1.Text)
        If price > 0 Then
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

            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(AGING_FLAG_NEW,PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@AGING_FLAG_NEW,@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn_trans, myTrans)
            cmd.Parameters.AddWithValue("@PO_NO", so_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", inv_no)
            cmd.Parameters.AddWithValue("@SUPL_ID", dt_id)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date.Date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.Parameters.AddWithValue("@AGING_FLAG_NEW", inv_no)
            cmd.ExecuteReader()
            cmd.Dispose()

        End If
    End Sub
End Class