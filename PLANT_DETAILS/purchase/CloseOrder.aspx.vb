Imports System.Globalization
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Public Class CloseOrder
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

            ''search chptr code
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select distinct order_type  from order_details order by order_type", conn)
            da.Fill(dt)
            DropDownList50.Items.Clear()
            DropDownList50.DataSource = dt
            DropDownList50.DataValueField = "order_type"
            DropDownList50.DataBind()
            conn.Close()
            DropDownList50.Items.Insert(0, "Select")
            DropDownList50.SelectedValue = "Select"
            ''search mat group
            ''Panel32.Visible = True
        End If

        'If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("purchaseAccess")) Or Session("purchaseAccess") = "")) Then

        '    Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        'End If
    End Sub



    Protected Sub DropDownList50_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList50.SelectedIndexChanged
        If DropDownList50.SelectedValue = "Select" Then
            DropDownList50.Focus()
            Return
        End If

        ''ADD FISCAL YEAR IN DROPDOWNLIST
        conn.Open()
        da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
        da.Fill(ds, "FISCAL_YEAR")
        DropDownList1.DataSource = ds.Tables("FISCAL_YEAR")
        DropDownList1.DataValueField = "FY"
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, "Select")
        conn.Close()
    End Sub

    Private Function PO_QUARY() As Object
        Throw New NotImplementedException
    End Function

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()

            DropDownList10.Items.Clear()
            DropDownList10.DataBind()
            Return
        End If

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct (so_no+' , '+so_actual) as so_no from order_details where order_type='" & DropDownList50.SelectedValue & "' and FINANCE_YEAR='" & DropDownList1.SelectedValue & "' and SO_STATUS in('PENDING','RCM','RC','DRAFT','RCW','CLEAR','ACTIVE') order by so_no", conn)
        da.Fill(dt)
        DropDownList10.Items.Clear()
        DropDownList10.DataSource = dt
        DropDownList10.DataValueField = "so_no"
        DropDownList10.DataBind()
        conn.Close()
        DropDownList10.Items.Insert(0, "Select")
        DropDownList10.SelectedValue = "Select"
    End Sub

    Protected Sub Button64_Click(sender As Object, e As EventArgs) Handles Button64.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()
            Try
                If (DropDownList3.Text = "Close") Then
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='Closed', Finance_approved='No' where SO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()
                    lblErrorText.Visible = "True"
                    lblErrorText.Text = "Order closed successfully."
                    myTrans.Commit()
                    conn_trans.Close()
                ElseIf (DropDownList3.Text = "Short Close") Then
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='Short Closed', Finance_approved='No' where SO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()
                    lblErrorText.Visible = "True"
                    lblErrorText.Text = "Order short closed successfully."
                    myTrans.Commit()
                    conn_trans.Close()
                End If

                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select distinct (so_no+' , '+so_actual) as so_no from order_details where order_type='" & DropDownList50.SelectedValue & "' and FINANCE_YEAR='" & DropDownList1.SelectedValue & "' and SO_STATUS in('PENDING','RCM','RC','DRAFT','RCW','CLEAR','ACTIVE') order by so_no", conn)
                da.Fill(dt)
                DropDownList10.Items.Clear()
                DropDownList10.DataSource = dt
                DropDownList10.DataValueField = "so_no"
                DropDownList10.DataBind()
                conn.Close()
                DropDownList10.Items.Insert(0, "Select")
                DropDownList10.SelectedValue = "Select"


            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                lblErrorText.Text = "There was some Error, please contact EDP."
            Finally
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim from_date, to_date As Date
        from_date = CDate(TextBox1.Text)
        to_date = CDate(TextBox2.Text)
        ''Panel3.Visible = True
        If (DropDownList4.SelectedValue = "Purchase Order") Then
            conn.Open()
            dt.Clear()

            da = New SqlDataAdapter("DECLARE @TT TABLE(ACTUAL_PO_NO VARCHAR(100),PO_NO VARCHAR(60),SUPL_NAME VARCHAR(100),MAT_CODE VARCHAR(60),W_SLNO VARCHAR(30),W_AU VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select SO_ACTUAL,PO_NO,SUPL_NAME,PO_ORD_MAT.MAT_CODE,MAT_SLNO as W_SLNO,MAX(MATERIAL.MAT_AU) AS W_AU, SUM(MAT_QTY) AS W_QTY from PO_ORD_MAT join MATERIAL on PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE join ORDER_DETAILS on PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID where ORDER_DETAILS.SO_ACTUAL_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' GROUP BY SO_ACTUAL,PO_NO,MAT_SLNO,PO_ORD_MAT.MAT_CODE,SUPL_NAME ORDER BY PO_NO,MAT_SLNO,MAT_CODE

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),MAT_CODE VARCHAR(60),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            
            select PO_NO,MAT_CODE ,MAT_SLNO as W_SLNO,sum((case when TRANS_WO_NO = 'N/A' THEN (case when (MAT_CHALAN_QTY-MAT_RCD_QTY)<=(MAT_CHALAN_QTY*0.005) THEN MAT_CHALAN_QTY- MAT_REJ_QTY-MAT_EXCE else MAT_RCD_QTY-MAT_REJ_QTY-MAT_EXCE end) else (MAT_CHALAN_QTY - MAT_REJ_QTY-MAT_EXCE) end)) AS W_COMPLETED from PO_RCD_MAT join ORDER_DETAILS on PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO where GARN_NO<>'PENDING' and ORDER_DETAILS.SO_ACTUAL_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' group by PO_NO,MAT_SLNO,MAT_CODE

            SELECT T1.ACTUAL_PO_NO,T1.PO_NO,T1.SUPL_NAME,T1.W_SLNO,M1.MAT_NAME,T1.W_AU,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED,(T1.W_QTY-ISNULL(T2.W_COMPLETED, 0.000)) AS ORDER_BALANCE FROM @TT T1 LEFT JOIN @TT1 T2 ON (T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO and t1.MAT_CODE=T2.MAT_CODE) join MATERIAL M1 on t1.MAT_CODE=M1.MAT_CODE order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)
            da.Fill(dt)
            conn.Close()
            GridView1.DataSource = dt
            GridView1.DataBind()

        ElseIf (DropDownList4.SelectedValue = "Work Order") Then
            'conn.Open()
            'dt.Clear()

            'da = New SqlDataAdapter("
            'DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(250),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            'INSERT INTO @TT
            'select PO_NO,W_SLNO,W_NAME,MAX(W_AU) AS W_AU,MIN(W_START_DATE) AS W_START_DATE,MAX(W_END_DATE) AS W_END_DATE,SUM(W_QTY) AS W_QTY from WO_ORDER where WO_ORDER.PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,W_SLNO,W_NAME ORDER BY W_SLNO

            'DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            'INSERT INTO @TT1
            'select PO_NO,WO_SLNO,SUM(work_qty) AS W_COMPLETED from mb_book where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY po_no,wo_slno

            'SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO", conn)
            'da.Fill(dt)
            'conn.Close()
            'GridView4.DataSource = dt
            'GridView4.DataBind()
            'Dim i As Integer = 0
            'For i = 0 To GridView4.Rows.Count - 1
            '    GridView4.Rows(i).Cells(8).Text = CDec(GridView4.Rows(i).Cells(6).Text) - CDec(GridView4.Rows(i).Cells(7).Text)
            'Next
            'Dim mc1 As New SqlCommand
            'conn.Open()
            'mc1.CommandText = "select supl.supl_name from supl join wo_order on wo_order.supl_id=supl.supl_id where wo_order.po_no  = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            'mc1.Connection = conn
            'dr = mc1.ExecuteReader
            'If dr.HasRows Then
            '    dr.Read()
            '    Label57.Text = dr.Item("supl_name")
            '    dr.Close()
            'End If
            'conn.Close()
        ElseIf (DropDownList4.SelectedValue = "Rate Contract") Then

            'conn.Open()
            'dt.Clear()

            'If (Left(DropDownList10.SelectedValue, 3) = "R06") Then

            '    da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            'INSERT INTO @TT
            'select PO_NO,W_SLNO,W_NAME,MAX(W_AU) AS W_AU,MIN(W_START_DATE) AS W_START_DATE,MAX(W_END_DATE) AS W_END_DATE,SUM(W_QTY) AS W_QTY from WO_ORDER where WO_ORDER.PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,W_SLNO,W_NAME ORDER BY W_SLNO

            'DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            'INSERT INTO @TT1
            'select PO_NO,WO_SLNO,SUM(work_qty) AS W_COMPLETED from mb_book where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY po_no,wo_slno

            'SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO", conn)

            'Else

            '    da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            'INSERT INTO @TT
            'select PO_NO,MAT_SLNO as W_SLNO,PO_ORD_MAT.MAT_NAME as W_NAME,MAX(MATERIAL.MAT_AU) AS W_AU,MIN(AMD_DATE) AS W_START_DATE,MAX(MAT_DELIVERY) AS W_END_DATE,SUM(MAT_QTY) AS W_QTY from PO_ORD_MAT join MATERIAL on PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,MAT_SLNO,PO_ORD_MAT.MAT_NAME ORDER BY MAT_SLNO

            'DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            'INSERT INTO @TT1
            'select COST_CODE as PO_NO,MAT_SL_NO as WO_SLNO,SUM(MAT_QTY) AS W_COMPLETED from MAT_DETAILS where COST_CODE='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY COST_CODE,MAT_SL_NO

            'SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)

            'End If

            'da.Fill(dt)
            'conn.Close()
            'GridView4.DataSource = dt
            'GridView4.DataBind()
            'Dim i As Integer = 0
            'For i = 0 To GridView4.Rows.Count - 1
            '    GridView4.Rows(i).Cells(8).Text = CDec(GridView4.Rows(i).Cells(6).Text) - CDec(GridView4.Rows(i).Cells(7).Text)
            'Next
            'Dim mc1 As New SqlCommand
            'conn.Open()
            'mc1.CommandText = "select supl.supl_name from supl join wo_order on wo_order.supl_id=supl.supl_id where wo_order.po_no  = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            'mc1.Connection = conn
            'dr = mc1.ExecuteReader
            'If dr.HasRows Then
            '    dr.Read()
            '    Label57.Text = dr.Item("supl_name")
            '    dr.Close()
            'End If
            'conn.Close()

        ElseIf (DropDownList4.SelectedValue = "Sale Order") Then

            'conn.Open()
            'dt.Clear()

            'da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            'INSERT INTO @TT
            'select SO_NO as PO_NO,ITEM_SLNO as W_SLNO,F_ITEM.ITEM_NAME as W_NAME,MAX(ORD_AU) AS W_AU,MIN(AMD_DATE) AS W_START_DATE,MAX(ITEM_DELIVERY) AS W_END_DATE,SUM(ITEM_QTY) AS W_QTY from SO_MAT_ORDER join F_ITEM on SO_MAT_ORDER.ITEM_CODE=F_ITEM.ITEM_CODE where SO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY SO_NO,ITEM_SLNO,ITEM_NAME ORDER BY ITEM_SLNO

            'DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            'INSERT INTO @TT1
            'select SO_NO as PO_NO,MAT_SLNO as WO_SLNO,SUM(TOTAL_QTY) AS W_COMPLETED from DESPATCH where SO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'  and INV_STATUS<>'CANCELLED' GROUP BY SO_NO,MAT_SLNO

            'SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)
            'da.Fill(dt)
            'conn.Close()
            'GridView4.DataSource = dt
            'GridView4.DataBind()
            'Dim i As Integer = 0
            'For i = 0 To GridView4.Rows.Count - 1
            '    GridView4.Rows(i).Cells(8).Text = CDec(GridView4.Rows(i).Cells(6).Text) - CDec(GridView4.Rows(i).Cells(7).Text)
            'Next

            'Dim mc1 As New SqlCommand
            'conn.Open()
            'mc1.CommandText = "select dater.d_name as supl_name from dater join ORDER_DETAILS on ORDER_DETAILS.PARTY_CODE=d_code where ORDER_DETAILS.SO_NO  ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            'mc1.Connection = conn
            'dr = mc1.ExecuteReader
            'If dr.HasRows Then
            '    dr.Read()
            '    Label57.Text = dr.Item("supl_name")
            '    dr.Close()
            'End If
            'conn.Close()

        End If
    End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Select" Then
            DropDownList9.Focus()
            MultiView1.ActiveViewIndex = -1
            Return

        ElseIf DropDownList9.SelectedValue = "PO Number" Then

            MultiView1.ActiveViewIndex = 0

        ElseIf DropDownList9.SelectedValue = "Date" Then

            MultiView1.ActiveViewIndex = 1

        ElseIf DropDownList9.SelectedValue = "By Material Code" Then

            MultiView1.ActiveViewIndex = 2

        End If
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel6.Visible = True
        conn.Open()
        dt.Clear()

        da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,MAT_SLNO as W_SLNO,PO_ORD_MAT.MAT_NAME as W_NAME,MAX(MATERIAL.MAT_AU) AS W_AU,MIN(AMD_DATE) AS W_START_DATE,MAX(MAT_DELIVERY) AS W_END_DATE,SUM(MAT_QTY) AS W_QTY from PO_ORD_MAT join MATERIAL on PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where PO_NO ='" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,MAT_SLNO,PO_ORD_MAT.MAT_NAME ORDER BY MAT_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            
            select PO_NO,MAT_SLNO as W_SLNO,sum((case when TRANS_WO_NO = 'N/A' THEN (case when (MAT_CHALAN_QTY-MAT_RCD_QTY)<=(MAT_CHALAN_QTY*0.005) THEN (MAT_CHALAN_QTY- MAT_REJ_QTY-MAT_EXCE) else (MAT_RCD_QTY- MAT_REJ_QTY-MAT_EXCE) end) else MAT_CHALAN_QTY end)) AS W_COMPLETED from PO_RCD_MAT where PO_NO='" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "' and GARN_NO<>'PENDING' group by PO_NO,MAT_SLNO

            SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)

        da.Fill(dt)
        conn.Close()
        GridView2.DataSource = dt
        GridView2.DataBind()
        Dim i As Integer = 0
        For i = 0 To GridView2.Rows.Count - 1
            GridView2.Rows(i).Cells(8).Text = CDec(GridView2.Rows(i).Cells(6).Text) - CDec(GridView2.Rows(i).Cells(7).Text)
        Next
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select supl.supl_name from supl join ORDER_DETAILS on ORDER_DETAILS.PARTY_CODE=supl.supl_id where ORDER_DETAILS.SO_NO  = '" & DropDownList2.Text.Substring(0, DropDownList2.Text.IndexOf(",") - 1) & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Label14.Text = dr.Item("supl_name")
            dr.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        ''Panel6.Visible = False

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("SELECT DISTINCT (PO_NO+' , '+SO_ACTUAL) AS PO_NO FROM PO_ORD_MAT JOIN ORDER_DETAILS ON PO_ORD_MAT.PO_NO=ORDER_DETAILS.SO_NO WHERE MAT_CODE='" & TextBox115.Text.Substring(0, TextBox115.Text.IndexOf(",") - 1).Trim & "' ORDER BY PO_NO", conn)
        da.Fill(dt)
        conn.Close()
        DropDownList2.Items.Clear()
        DropDownList2.DataSource = dt
        DropDownList2.DataValueField = "PO_NO"
        DropDownList2.DataBind()
        DropDownList2.Items.Insert(0, "Select")
        DropDownList2.SelectedValue = "Select"

    End Sub

    Protected Sub Button68_Click(sender As Object, e As EventArgs) Handles Button68.Click
        If DropDownList50.SelectedValue = "Select" Then
            DropDownList50.Focus()
            Return
        ElseIf DropDownList1.SelectedValue = "Select" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList10.SelectedValue = "Select" Then
            DropDownList10.Focus()
            Return
        End If
        conn.Open()
        Dim crystalReport As New ReportDocument
        Dim dt As New DataTable
        Dim PO_QUARY As String = "select * from ORDER_DETAILS join EmpLoginDetails on EmpLoginDetails .EMP_ID =ORDER_DETAILS .EMP_ID where ORDER_DETAILS.SO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1).Trim & "'"
        da = New SqlDataAdapter(PO_QUARY, conn)
        da.Fill(dt)
        conn.Close()
        If DropDownList50.SelectedValue = "Sale Order" Then
            If Left(DropDownList10.SelectedValue, 3) = "S05" Then
                crystalReport.Load(Server.MapPath("~/print_rpt/sale_order1.rpt"))

            Else
                crystalReport.Load(Server.MapPath("~/print_rpt/sale_order_misc.rpt"))
            End If

        ElseIf DropDownList50.SelectedValue = "Purchase Order" Or DropDownList50.SelectedValue = "Rate Contract" Then
            crystalReport.Load(Server.MapPath("~/print_rpt/po_store.rpt"))

        ElseIf DropDownList50.SelectedValue = "Work Order" Then
            crystalReport.Load(Server.MapPath("~/print_rpt/w_order.rpt"))

        End If
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