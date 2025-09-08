Imports System.Globalization
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO

Public Class order_balance
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
        da = New SqlDataAdapter("select distinct (so_no+' , '+so_actual) as so_no from order_details where order_type='" & DropDownList50.SelectedValue & "' and FINANCE_YEAR='" & DropDownList1.SelectedValue & "' order by so_no", conn)
        da.Fill(dt)
        DropDownList10.Items.Clear()
        DropDownList10.DataSource = dt
        DropDownList10.DataValueField = "so_no"
        DropDownList10.DataBind()
        conn.Close()
        DropDownList10.Items.Add("Select")
        DropDownList10.SelectedValue = "Select"
    End Sub

    Protected Sub Button64_Click(sender As Object, e As EventArgs) Handles Button64.Click
        Try
            Panel14.Visible = True
            If (DropDownList50.SelectedValue = "Purchase Order") Then
                conn.Open()
                dt.Clear()

                da = New SqlDataAdapter("DECLARE @tolerance DECIMAL(6,2) = 0;
            set @tolerance = (select TOLERANCE from ORDER_DETAILS where SO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "')
            DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),UNIT_PRICE DECIMAL(16,2),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,MAT_SLNO as W_SLNO,PO_ORD_MAT.MAT_NAME as W_NAME,MAX(MATERIAL.MAT_AU) AS W_AU,SUM(MAT_UNIT_RATE) AS UNIT_PRICE,MIN(AMD_DATE) AS W_START_DATE,MAX(MAT_DELIVERY) AS W_END_DATE,SUM(MAT_QTY) AS W_QTY from PO_ORD_MAT join MATERIAL on PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,MAT_SLNO,PO_ORD_MAT.MAT_NAME ORDER BY MAT_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            
            select PO_NO,MAT_SLNO as W_SLNO,sum((case when TRANS_WO_NO = 'N/A' THEN (case when @tolerance=0 THEN (MAT_RCD_QTY- MAT_REJ_QTY-MAT_EXCE) else (case when (MAT_CHALAN_QTY-MAT_RCD_QTY)<=(MAT_CHALAN_QTY*0.005) THEN (MAT_CHALAN_QTY- MAT_REJ_QTY-MAT_EXCE) else (MAT_RCD_QTY- MAT_REJ_QTY-MAT_EXCE) end) end) else MAT_CHALAN_QTY end)) AS W_COMPLETED from PO_RCD_MAT where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' and GARN_NO<>'PENDING' group by PO_NO,MAT_SLNO

            SELECT T1.PO_NO,T1.W_SLNO,'' as vocabNo,T1.W_NAME,T1.W_AU,t1.UNIT_PRICE,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)
                da.Fill(dt)
                conn.Close()
                GridView4.DataSource = dt
                GridView4.DataBind()
                Dim i As Integer = 0
                For i = 0 To GridView4.Rows.Count - 1
                    GridView4.Rows(i).Cells(10).Text = CDec(GridView4.Rows(i).Cells(8).Text) - CDec(GridView4.Rows(i).Cells(9).Text)
                Next
                Dim mc1 As New SqlCommand
                conn.Open()
                mc1.CommandText = "select supl.supl_name from supl join ORDER_DETAILS on ORDER_DETAILS.PARTY_CODE=supl.supl_id where ORDER_DETAILS.SO_NO  = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    Label57.Text = dr.Item("supl_name")
                    dr.Close()
                End If
                conn.Close()
            ElseIf (DropDownList50.SelectedValue = "Work Order") Then
                conn.Open()
                dt.Clear()

                da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(500),W_AU VARCHAR(30),UNIT_PRICE DECIMAL(16,2),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,W_SLNO,W_NAME,MAX(W_AU) AS W_AU,SUM(W_UNIT_PRICE) AS UNIT_PRICE,MIN(W_START_DATE) AS W_START_DATE,MAX(W_END_DATE) AS W_END_DATE,SUM(W_QTY) AS W_QTY from WO_ORDER where WO_ORDER.PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,W_SLNO,W_NAME ORDER BY W_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            select PO_NO,WO_SLNO,SUM(work_qty) AS W_COMPLETED from mb_book where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY po_no,wo_slno

            SELECT T1.PO_NO,T1.W_SLNO,'' as vocabNo,T1.W_NAME,T1.W_AU,t1.UNIT_PRICE,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO", conn)
                da.Fill(dt)
                conn.Close()
                GridView4.DataSource = dt
                GridView4.DataBind()
                Dim i As Integer = 0
                For i = 0 To GridView4.Rows.Count - 1
                    GridView4.Rows(i).Cells(10).Text = CDec(GridView4.Rows(i).Cells(8).Text) - CDec(GridView4.Rows(i).Cells(9).Text)
                Next
                Dim mc1 As New SqlCommand
                conn.Open()
                mc1.CommandText = "select supl.supl_name from supl join wo_order on wo_order.supl_id=supl.supl_id where wo_order.po_no  = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    Label57.Text = dr.Item("supl_name")
                    dr.Close()
                End If
                conn.Close()
            ElseIf (DropDownList50.SelectedValue = "Rate Contract") Then
                Dim PO_TYPE, order_amt, prov_value As New String("")
                Dim mc1 As New SqlCommand
                conn.Open()
                mc1.CommandText = "select * from ORDER_DETAILS where SO_NO = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    PO_TYPE = dr.Item("PO_TYPE")
                    dr.Close()
                End If
                conn.Close()

                conn.Open()
                dt.Clear()

                If (PO_TYPE = "STORE MATERIAL" Or PO_TYPE = "Others") Then

                    da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),vocabNo VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),UNIT_PRICE DECIMAL(16,2),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3),W_COMPLETED DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,MAT_SLNO,'' as vocabNo,MAT_NAME,MAX(MAT_AU) AS W_AU,SUM(MAT_UNIT_RATE) AS UNIT_PRICE,MIN(AMD_DATE) AS W_START_DATE,MAX(MAT_DELIVERY) AS W_END_DATE,SUM(MAT_QTY) AS W_QTY,SUM(MAT_QTY_RCVD) AS W_COMPLETED from PO_ORD_MAT where PO_ORD_MAT.PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,MAT_SLNO,MAT_NAME ORDER BY MAT_SLNO
			select  * from @TT", conn)

                ElseIf (PO_TYPE = "Maint Work" Or PO_TYPE = "Works Contract") Then

                    da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(500),W_AU VARCHAR(30),UNIT_PRICE DECIMAL(16,2),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,W_SLNO,W_NAME,MAX(W_AU) AS W_AU,SUM(W_UNIT_PRICE) AS UNIT_PRICE,MIN(W_START_DATE) AS W_START_DATE,MAX(W_END_DATE) AS W_END_DATE,SUM(W_QTY) AS W_QTY from WO_ORDER where WO_ORDER.PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,W_SLNO,W_NAME ORDER BY W_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            select PO_NO,WO_SLNO,SUM(work_qty) AS W_COMPLETED from mb_book where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY po_no,wo_slno

            SELECT T1.PO_NO,T1.W_SLNO,'' as vocabNo,T1.W_NAME,T1.W_AU,t1.UNIT_PRICE,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO", conn)

                End If

                da.Fill(dt)
                conn.Close()
                GridView4.DataSource = dt
                GridView4.DataBind()
                Dim i As Integer = 0
                For i = 0 To GridView4.Rows.Count - 1
                    GridView4.Rows(i).Cells(10).Text = CDec(GridView4.Rows(i).Cells(8).Text) - CDec(GridView4.Rows(i).Cells(9).Text)
                Next

                'conn.Open()
                'mc1.CommandText = "select supl.supl_name from supl join RATE_CONTRACT on wo_order.supl_id=supl.supl_id where wo_order.po_no  = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
                'mc1.Connection = conn
                'dr = mc1.ExecuteReader
                'If dr.HasRows Then
                '    dr.Read()
                '    Label57.Text = dr.Item("supl_name")
                '    dr.Close()
                'End If
                'conn.Close()



                conn.Open()
                Dim mc2 As New SqlCommand
                If (PO_TYPE = "STORE MATERIAL" Or PO_TYPE = "Others") Then
                    mc2.CommandText = "select T1.PO_NO,t1.ORD_AMOUNT,t2.prov_value,t3.SUPL_NAME from RATE_CONTRACT T1 INNER JOIN (select SUPL_ID,PO_NO,sum(PROV_VALUE) as prov_value from PO_RCD_MAT where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' group by SUPL_ID,PO_NO) as T2
			        ON T1.PO_NO = T2.PO_NO join SUPL T3 on t2.SUPL_ID=T3.SUPL_ID"

                ElseIf (PO_TYPE = "Maint Work" Or PO_TYPE = "Works Contract") Then

                    mc2.CommandText = "select T1.PO_NO,t1.ORD_AMOUNT,t2.prov_value,t3.SUPL_NAME from RATE_CONTRACT T1 INNER JOIN (select SUPL_ID,PO_NO,sum(prov_amt) as prov_value from mb_book where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' group by SUPL_ID,PO_NO) as T2
			        ON T1.PO_NO = T2.PO_NO join SUPL T3 on t2.SUPL_ID=T3.SUPL_ID"
                End If

                mc2.Connection = conn
                dr = mc2.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_amt = dr.Item("ORD_AMOUNT")
                    prov_value = dr.Item("prov_value")
                    Label57.Text = dr.Item("SUPL_NAME")
                    dr.Close()
                End If
                conn.Close()




                Label3.Visible = True
                Label5.Visible = True


                Label3.Text = "Rate Contract Value = " + order_amt + " , "
                Label5.Text = "Executed Value = " + prov_value


            ElseIf (DropDownList50.SelectedValue = "Sale Order") Then

                conn.Open()
                dt.Clear()

                da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),vocabNo VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),UNIT_PRICE DECIMAL(16,2),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select SO_NO as PO_NO,ITEM_SLNO as W_SLNO,ITEM_VOCAB,F_ITEM.ITEM_NAME as W_NAME,MAX(ORD_AU) AS W_AU,SUM(ITEM_UNIT_RATE) AS UNIT_PRICE,MIN(AMD_DATE) AS W_START_DATE,MAX(ITEM_DELIVERY) AS W_END_DATE,SUM(ITEM_QTY) AS W_QTY from SO_MAT_ORDER join F_ITEM on SO_MAT_ORDER.ITEM_CODE=F_ITEM.ITEM_CODE where SO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY SO_NO,ITEM_SLNO,ITEM_VOCAB,ITEM_NAME ORDER BY ITEM_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            select SO_NO as PO_NO,MAT_SLNO as WO_SLNO,SUM(TOTAL_QTY) AS W_COMPLETED from DESPATCH where SO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' and INV_STATUS<>'CANCELLED' GROUP BY SO_NO,MAT_SLNO

            SELECT T1.PO_NO,T1.W_SLNO,t1.vocabNo,T1.W_NAME,T1.W_AU,t1.UNIT_PRICE,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)
                da.Fill(dt)
                conn.Close()
                GridView4.DataSource = dt
                GridView4.DataBind()
                Dim i As Integer = 0
                For i = 0 To GridView4.Rows.Count - 1
                    GridView4.Rows(i).Cells(10).Text = CDec(GridView4.Rows(i).Cells(8).Text) - CDec(GridView4.Rows(i).Cells(9).Text)
                Next

                Dim mc1 As New SqlCommand
                conn.Open()
                mc1.CommandText = "select dater.d_name as supl_name from dater join ORDER_DETAILS on ORDER_DETAILS.PARTY_CODE=d_code where ORDER_DETAILS.SO_NO  ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    Label57.Text = dr.Item("supl_name")
                    dr.Close()
                End If
                conn.Close()

            End If

        Catch ee As Exception
            conn.Close()

        Finally
            conn.Close()
        End Try
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
            Panel24.Visible = True
            Panel1.Visible = False

        ElseIf (DropDownList4.SelectedValue = "Work Order") Then
            conn.Open()
            dt.Clear()

            da = New SqlDataAdapter("
            DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(250),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,W_SLNO,W_NAME,MAX(W_AU) AS W_AU,MIN(W_START_DATE) AS W_START_DATE,MAX(W_END_DATE) AS W_END_DATE,SUM(W_QTY) AS W_QTY from WO_ORDER where WO_ORDER.PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,W_SLNO,W_NAME ORDER BY W_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            select PO_NO,WO_SLNO,SUM(work_qty) AS W_COMPLETED from mb_book where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY po_no,wo_slno

            SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO", conn)
            da.Fill(dt)
            conn.Close()
            GridView4.DataSource = dt
            GridView4.DataBind()
            Dim i As Integer = 0
            For i = 0 To GridView4.Rows.Count - 1
                GridView4.Rows(i).Cells(8).Text = CDec(GridView4.Rows(i).Cells(6).Text) - CDec(GridView4.Rows(i).Cells(7).Text)
            Next
            Dim mc1 As New SqlCommand
            conn.Open()
            mc1.CommandText = "select supl.supl_name from supl join wo_order on wo_order.supl_id=supl.supl_id where wo_order.po_no  = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label57.Text = dr.Item("supl_name")
                dr.Close()
            End If
            conn.Close()
        ElseIf (DropDownList4.SelectedValue = "Rate Contract") Then

            conn.Open()
            dt.Clear()

            If (Left(DropDownList10.SelectedValue, 3) = "R06") Then

                da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,W_SLNO,W_NAME,MAX(W_AU) AS W_AU,MIN(W_START_DATE) AS W_START_DATE,MAX(W_END_DATE) AS W_END_DATE,SUM(W_QTY) AS W_QTY from WO_ORDER where WO_ORDER.PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,W_SLNO,W_NAME ORDER BY W_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            select PO_NO,WO_SLNO,SUM(work_qty) AS W_COMPLETED from mb_book where PO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY po_no,wo_slno

            SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO", conn)

            Else

                da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),W_NAME VARCHAR(150),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,3))
            INSERT INTO @TT
            select PO_NO,MAT_SLNO as W_SLNO,PO_ORD_MAT.MAT_NAME as W_NAME,MAX(MATERIAL.MAT_AU) AS W_AU,MIN(AMD_DATE) AS W_START_DATE,MAX(MAT_DELIVERY) AS W_END_DATE,SUM(MAT_QTY) AS W_QTY from PO_ORD_MAT join MATERIAL on PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where PO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY PO_NO,MAT_SLNO,PO_ORD_MAT.MAT_NAME ORDER BY MAT_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED VARCHAR(30))
            INSERT INTO @TT1
            select COST_CODE as PO_NO,MAT_SL_NO as WO_SLNO,SUM(MAT_QTY) AS W_COMPLETED from MAT_DETAILS where COST_CODE='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' GROUP BY COST_CODE,MAT_SL_NO

            SELECT T1.PO_NO,T1.W_SLNO,T1.W_NAME,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,T1.W_QTY,ISNULL(T2.W_COMPLETED, 0.000) As W_COMPLETED FROM @TT T1 LEFT JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)

            End If

            da.Fill(dt)
            conn.Close()
            GridView4.DataSource = dt
            GridView4.DataBind()
            Dim i As Integer = 0
            For i = 0 To GridView4.Rows.Count - 1
                GridView4.Rows(i).Cells(8).Text = CDec(GridView4.Rows(i).Cells(6).Text) - CDec(GridView4.Rows(i).Cells(7).Text)
            Next
            Dim mc1 As New SqlCommand
            conn.Open()
            mc1.CommandText = "select supl.supl_name from supl join wo_order on wo_order.supl_id=supl.supl_id where wo_order.po_no  = '" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                Label57.Text = dr.Item("supl_name")
                dr.Close()
            End If
            conn.Close()

        ElseIf (DropDownList4.SelectedValue = "Sale Order") Then

            conn.Open()
            dt.Clear()

            da = New SqlDataAdapter("DECLARE @TT TABLE(PO_NO VARCHAR(60),W_SLNO VARCHAR(30),f_name VARCHAR(150),W_NAME VARCHAR(150),W_weight DECIMAL(16,2),W_AU VARCHAR(30),W_START_DATE VARCHAR(30),W_END_DATE VARCHAR(30),W_QTY DECIMAL(16,2))
            INSERT INTO @TT
            select SO_NO as PO_NO,ITEM_SLNO as W_SLNO,F_ITEM.ITEM_CODE as f_name,F_ITEM.ITEM_NAME as W_NAME,F_ITEM.ITEM_WEIGHT,MAX(ORD_AU) AS W_AU,MIN(AMD_DATE) AS W_START_DATE,MAX(ITEM_DELIVERY) AS W_END_DATE,SUM(ITEM_QTY) AS W_QTY from SO_MAT_ORDER join F_ITEM on SO_MAT_ORDER.ITEM_CODE=F_ITEM.ITEM_CODE GROUP BY SO_NO,ITEM_SLNO,ITEM_NAME,F_ITEM.ITEM_CODE,F_ITEM.ITEM_WEIGHT ORDER BY ITEM_SLNO

            DECLARE @TT1 TABLE(PO_NO VARCHAR(50),PARTY_CODE VARCHAR(30),W_SLNO VARCHAR(30),W_COMPLETED DECIMAL(16,2))
            INSERT INTO @TT1
            select SO_NO as PO_NO,PARTY_CODE,MAT_SLNO as W_SLNO,SUM(TOTAL_QTY) AS W_COMPLETED from DESPATCH where INV_STATUS='ACTIVE' GROUP BY SO_NO,MAT_SLNO, PARTY_CODE order by PO_NO

            SELECT o1.SO_ACTUAL AS ACTUAL_PO_NO,o1.SO_ACTUAL_DATE,T1.PO_NO,o1.PARTY_CODE,D1.d_name AS SUPL_NAME,T1.W_SLNO AS W_SLNO,t1.f_name,T1.W_NAME AS MAT_NAME,T1.W_weight,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE,sum(T1.W_QTY) As Order_Qty, (case when t1.W_AU='PCS' THEN (sum(T1.W_QTY)*T1.W_weight/1000) else sum(T1.W_QTY) end) as ORDER_QTY_MT,sum(T2.W_COMPLETED) As Completed_Qty, (case when t1.W_AU='PCS' THEN (sum(T2.W_COMPLETED)*T1.W_weight/1000) else sum(T2.W_COMPLETED) end) as completed_mt FROM @TT T1 JOIN @TT1 T2 ON T1.PO_NO=T2.PO_NO AND T1.W_SLNO=T2.W_SLNO join ORDER_DETAILS O1 on T1.PO_NO=O1.SO_NO join dater D1 on T2.PARTY_CODE=D1.d_code where (O1.SO_ACTUAL_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & " ' and ' " & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "') group by o1.SO_ACTUAL,o1.SO_ACTUAL_DATE,T1.PO_NO,T1.W_SLNO,o1.PARTY_CODE,D1.d_name,t1.f_name,T1.W_NAME,T1.W_weight,T1.W_AU,T1.W_START_DATE,T1.W_END_DATE having (sum(T1.W_QTY)-sum(T2.W_COMPLETED))<>0 order by t1.PO_NO,convert(int,T1.W_SLNO)", conn)
            da.Fill(dt)
            conn.Close()
            GridView3.DataSource = dt
            GridView3.DataBind()
            Dim i As Integer = 0
            For i = 0 To GridView3.Rows.Count - 1
                GridView3.Rows(i).Cells(14).Text = CDec(GridView3.Rows(i).Cells(10).Text) - CDec(GridView3.Rows(i).Cells(12).Text)
                GridView3.Rows(i).Cells(15).Text = CDec(GridView3.Rows(i).Cells(11).Text) - CDec(GridView3.Rows(i).Cells(13).Text)
            Next

            Panel24.Visible = False
            Panel1.Visible = True

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

    Protected Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If GridView4.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("BG")

        dt3.Columns.Add(New DataColumn("Party Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Order No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Order SL No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Description", GetType(String)))
        dt3.Columns.Add(New DataColumn("A/U", GetType(String)))
        dt3.Columns.Add(New DataColumn("Unit Price", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Start Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("End Date", GetType(String)))
        dt3.Columns.Add(New DataColumn("Order Qty.", GetType(String)))
        dt3.Columns.Add(New DataColumn("Completed", GetType(String)))
        dt3.Columns.Add(New DataColumn("Bal. Qty.", GetType(String)))



        For Me.count = 0 To GridView4.Rows.Count - 1
            dt3.Rows.Add(Label57.Text, GridView4.Rows(count).Cells(0).Text, GridView4.Rows(count).Cells(1).Text, GridView4.Rows(count).Cells(2).Text, GridView4.Rows(count).Cells(3).Text, GridView4.Rows(count).Cells(4).Text, GridView4.Rows(count).Cells(5).Text, GridView4.Rows(count).Cells(6).Text, GridView4.Rows(count).Cells(7).Text, GridView4.Rows(count).Cells(8).Text, GridView4.Rows(count).Cells(9).Text)
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=OrderBalance_" + GridView4.Rows(0).Cells(0).Text + ".xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub
End Class