Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class manage_order
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim myTrans As SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ''amd_Panel.Visible = True
        End If

        TextBox27_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("purchaseAccess")) Or Session("purchaseAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            DropDownList4.Items.Clear()
            DropDownList2.Items.Clear()
            DropDownList57.Items.Clear()
            Return
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select distinct so_no from ORDER_DETAILS where ORDER_TYPE='" & DropDownList3.SelectedValue & "' ORDER BY SO_NO", conn)
        da.Fill(dt)
        DropDownList4.Items.Clear()
        DropDownList4.DataSource = dt
        DropDownList4.DataValueField = "so_no"
        DropDownList4.DataBind()
        DropDownList4.Items.Insert(0, "Select")
        DropDownList4.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        ''order type search
        Dim order_type As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE from ORDER_DETAILS where SO_NO = '" & DropDownList4.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            dr.Close()
        End If
        conn.Close()
        'quary
        Dim quary As String = ""
        If order_type = "Purchase Order" Then
            quary = "SELECT DISTINCT MAT_SLNO AS SL_NO FROM PO_ORD_MAT WHERE PO_NO ='" & DropDownList4.SelectedValue & "' ORDER BY MAT_SLNO"
        ElseIf order_type = "Sale Order" Then
            quary = "SELECT DISTINCT ITEM_SLNO AS SL_NO FROM SO_MAT_ORDER WHERE SO_NO='" & DropDownList4.SelectedValue & "' ORDER BY ITEM_SLNO"
        ElseIf order_type = "Work Order" Then
            quary = "SELECT DISTINCT W_SLNO AS SL_NO from WO_ORDER WHERE PO_NO ='" & DropDownList4.SelectedValue & "' ORDER BY W_SLNO"
        ElseIf order_type = "Rate Contract" Then
            quary = "SELECT DISTINCT MAT_SLNO AS SL_NO FROM PO_ORD_MAT WHERE PO_NO ='" & DropDownList4.SelectedValue & "' ORDER BY MAT_SLNO"
        End If

        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        DropDownList2.Items.Clear()
        DropDownList2.DataSource = dt
        DropDownList2.DataValueField = "SL_NO"
        DropDownList2.DataBind()
        DropDownList2.Items.Insert(0, "Select")
        DropDownList2.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Dim order_type As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE from ORDER_DETAILS where SO_NO = '" & DropDownList4.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            dr.Close()
        End If
        conn.Close()
        Dim quary As String = ""
        If order_type = "Purchase Order" Then
            quary = "SELECT distinct MAT_CODE AS SL_NO FROM PO_ORD_MAT WHERE PO_NO ='" & DropDownList4.SelectedValue & "' and MAT_SLNO=" & DropDownList2.SelectedValue
        ElseIf order_type = "Sale Order" Then
            quary = "SELECT DISTINCT ITEM_CODE AS SL_NO FROM SO_MAT_ORDER WHERE SO_NO='" & DropDownList4.SelectedValue & "' and ITEM_SLNO=" & DropDownList2.SelectedValue
        ElseIf order_type = "Work Order" Then
            quary = "SELECT DISTINCT W_NAME AS SL_NO from WO_ORDER WHERE PO_NO ='" & DropDownList4.SelectedValue & "' and W_SLNO=" & DropDownList2.SelectedValue
        ElseIf order_type = "Rate Contract" Then
            quary = "SELECT distinct MAT_CODE AS SL_NO FROM PO_ORD_MAT WHERE PO_NO ='" & DropDownList4.SelectedValue & "' and MAT_SLNO=" & DropDownList2.SelectedValue
        End If
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter(quary, conn)
        count = da.Fill(dt)
        conn.Close()
        DropDownList57.Items.Clear()
        DropDownList57.DataSource = dt
        DropDownList57.DataValueField = "SL_NO"
        DropDownList57.DataBind()
        DropDownList57.Items.Insert(0, "Select")
        DropDownList57.SelectedValue = "Select"
    End Sub

    Protected Sub DropDownList57_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList57.SelectedIndexChanged
        ''order type search
        Dim QUARY As String = ""

        Dim order_type, PO_TYPE As New String("")
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select ORDER_TYPE,PO_TYPE from ORDER_DETAILS where SO_NO = '" & DropDownList4.SelectedValue & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            PO_TYPE = dr.Item("PO_TYPE")
            dr.Close()
        End If
        conn.Close()
        If order_type = "Purchase Order" Or order_type = "Rate Contract" Then
            QUARY = "select MAX(MATERIAL.MAT_NAME) AS MAT_NAME, MAX(MAT_DESC) AS MAT_DESC, MAX(MATERIAL.MAT_AU) AS AU, SUM(MAT_QTY) as QTY,sum(MAT_UNIT_RATE) AS UNIT_RATE, SUM(MAT_PACK) AS PACK,MAX(PF_TYPE) AS PF_TYPE,SUM(MAT_DISCOUNT) AS DISC,MAX(DISC_TYPE) AS DISC_TYPE,SUM(SGST) AS SGST, SUM(CGST) AS CGST,SUM(IGST) AS IGST, SUM(CESS) AS CESS, SUM(MAT_FREIGHT_PU) AS FREIGHT,MAX(FREIGHT_TYPE) AS F_TYPE , SUM(MAT_QTY_RCVD) AS RCVD, MAX(MAT_DELIVERY) AS DELV_DATE from PO_ORD_MAT JOIN MATERIAL ON PO_ORD_MAT .MAT_CODE =MATERIAL.MAT_CODE  where PO_NO='" & DropDownList4.SelectedValue & "' and MAT_SLNO=" & DropDownList2.SelectedValue
            ''TEXTBOX DATA FILL
            conn.Open()
            mc1.CommandText = QUARY
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox1.Text = dr.Item("QTY")
                Label499.Text = dr.Item("AU")
                Label500.Text = dr.Item("AU")
                TextBox2.Text = dr.Item("UNIT_RATE")
                Label622.Text = dr.Item("MAT_NAME")
                TextBox747.Text = dr.Item("DISC")
                Label487.Text = dr.Item("DISC_TYPE")
                Label498.Text = dr.Item("DISC_TYPE")
                TextBox748.Text = dr.Item("PACK")
                Label488.Text = dr.Item("PF_TYPE")
                Label497.Text = dr.Item("PF_TYPE")
                TextBox749.Text = dr.Item("SGST")
                TextBox750.Text = dr.Item("CGST")
                TextBox3.Text = dr.Item("IGST")
                TextBox4.Text = dr.Item("CESS")
                TextBox751.Text = dr.Item("FREIGHT")
                Label491.Text = dr.Item("F_TYPE")
                Label494.Text = dr.Item("F_TYPE")
                TextBox744.Text = dr.Item("DELV_DATE")
                TextBox746.Text = dr.Item("DELV_DATE")
                Label503.Text = dr.Item("MAT_DESC")
                Label622.Text = dr.Item("MAT_NAME")
                dr.Close()
            End If
            conn.Close()
            ''GRID VIEW DATA FILL
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select * from PO_ORD_MAT JOIN MATERIAL ON PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where PO_ORD_MAT.PO_NO='" & DropDownList4.SelectedValue & "' and PO_ORD_MAT.MAT_SLNO=" & DropDownList2.SelectedValue & " ORDER BY AMD_NO ", conn)
            count = da.Fill(dt)
            conn.Close()
            GridView212.DataSource = dt
            GridView212.DataBind()



            MultiView1.ActiveViewIndex = 0
        ElseIf order_type = "Sale Order" Then

            If (PO_TYPE = "RAW MATERIAL") Then
                QUARY = "SELECT MAX(ITEM_VOCAB) AS ITEM_VOCAB, MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE, MAX(ITEM_SGST) AS SGST, MAX(ITEM_CGST) AS CGST, MAX(ITEM_IGST) AS IGST, MAX(ITEM_CESS) AS CESS,MAX(MAT_AU) AS ITEM_AU,SUM(ITEM_QTY) AS ITEM_QTY,SUM(ITEM_UNIT_RATE) AS ITEM_UNIT_RATE,SUM(ITEM_PACK) AS ITEM_PACK,MAX(PACK_TYPE) AS PACK_TYPE,SUM(ITEM_DISCOUNT) AS ITEM_DISCOUNT,MAX(DISC_TYPE) AS DISC_TYPE,SUM(ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX,SUM(ITEM_TCS) AS ITEM_TCS,MAX(ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE,SUM(ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU,MAX(ITEM_DELIVERY) AS ITEM_DELIVERY,SUM(ITEM_S_TAX) AS ITEM_S_TAX,MAX(ITEM_DETAILS) AS ITEM_DETAILS FROM SO_MAT_ORDER JOIN MATERIAL ON SO_MAT_ORDER .ITEM_CODE =MATERIAL .MAT_CODE WHERE SO_MAT_ORDER .SO_NO ='" & DropDownList4.SelectedValue & "' AND SO_MAT_ORDER .ITEM_SLNO ='" & DropDownList2.SelectedValue & "' AND SO_MAT_ORDER .ITEM_CODE ='" & DropDownList57.SelectedValue & "'"

            ElseIf (PO_TYPE = "MISCELLANEOUS") Then
                QUARY = "SELECT MAX(ITEM_VOCAB) AS ITEM_VOCAB, MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE, MAX(ITEM_SGST) AS SGST, MAX(ITEM_CGST) AS CGST, MAX(ITEM_IGST) AS IGST, MAX(ITEM_CESS) AS CESS,MAX(MAT_AU) AS ITEM_AU,SUM(ITEM_QTY) AS ITEM_QTY,SUM(ITEM_UNIT_RATE) AS ITEM_UNIT_RATE,SUM(ITEM_PACK) AS ITEM_PACK,MAX(PACK_TYPE) AS PACK_TYPE,SUM(ITEM_DISCOUNT) AS ITEM_DISCOUNT,MAX(DISC_TYPE) AS DISC_TYPE,SUM(ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX,SUM(ITEM_TCS) AS ITEM_TCS,MAX(ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE,SUM(ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU,MAX(ITEM_DELIVERY) AS ITEM_DELIVERY,SUM(ITEM_S_TAX) AS ITEM_S_TAX,MAX(ITEM_DETAILS) AS ITEM_DETAILS FROM SO_MAT_ORDER JOIN MATERIAL ON SO_MAT_ORDER .ITEM_CODE =MATERIAL .MAT_CODE WHERE SO_MAT_ORDER .SO_NO ='" & DropDownList4.SelectedValue & "' AND SO_MAT_ORDER .ITEM_SLNO ='" & DropDownList2.SelectedValue & "' AND SO_MAT_ORDER .ITEM_CODE ='" & DropDownList57.SelectedValue & "'"

            Else
                QUARY = "SELECT MAX(ITEM_VOCAB) AS ITEM_VOCAB, MAX(SO_MAT_ORDER.ITEM_CODE) AS ITEM_CODE, MAX(ITEM_SGST) AS SGST, MAX(ITEM_CGST) AS CGST, MAX(ITEM_IGST) AS IGST, MAX(ITEM_CESS) AS CESS,MAX(ITEM_AU) AS ITEM_AU,SUM(ITEM_QTY) AS ITEM_QTY,SUM(ITEM_UNIT_RATE) AS ITEM_UNIT_RATE,SUM(ITEM_PACK) AS ITEM_PACK,MAX(PACK_TYPE) AS PACK_TYPE,SUM(ITEM_DISCOUNT) AS ITEM_DISCOUNT,MAX(DISC_TYPE) AS DISC_TYPE,SUM(ITEM_TERMINAL_TAX) AS ITEM_TERMINAL_TAX,SUM(ITEM_TCS) AS ITEM_TCS,MAX(ITEM_FREIGHT_TYPE) AS ITEM_FREIGHT_TYPE,SUM(ITEM_FREIGHT_PU) AS ITEM_FREIGHT_PU,MAX(ITEM_DELIVERY) AS ITEM_DELIVERY,SUM(ITEM_S_TAX) AS ITEM_S_TAX,MAX(ITEM_DETAILS) AS ITEM_DETAILS FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER .ITEM_CODE =F_ITEM .ITEM_CODE WHERE SO_MAT_ORDER .SO_NO ='" & DropDownList4.SelectedValue & "' AND SO_MAT_ORDER .ITEM_SLNO ='" & DropDownList2.SelectedValue & "' AND SO_MAT_ORDER .ITEM_CODE ='" & DropDownList57.SelectedValue & "'"
            End If

            conn.Open()
            mc1.CommandText = QUARY
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox757.Text = dr.Item("ITEM_QTY")
                Label506.Text = dr.Item("ITEM_AU")
                Label523.Text = dr.Item("ITEM_AU")
                TextBox758.Text = dr.Item("ITEM_UNIT_RATE")
                TextBox759.Text = dr.Item("ITEM_DISCOUNT")
                Label510.Text = dr.Item("DISC_TYPE")
                Label527.Text = dr.Item("DISC_TYPE")
                TextBox760.Text = dr.Item("ITEM_PACK")
                Label512.Text = dr.Item("PACK_TYPE")
                Label529.Text = dr.Item("PACK_TYPE")
                TextBox761.Text = dr.Item("SGST")
                TextBox762.Text = dr.Item("CGST")
                TextBox10.Text = dr.Item("IGST")
                TextBox11.Text = dr.Item("CESS")
                TextBox805.Text = dr.Item("ITEM_TERMINAL_TAX")
                TextBox806.Text = dr.Item("ITEM_TCS")
                TextBox763.Text = dr.Item("ITEM_FREIGHT_PU")
                Label518.Text = dr.Item("ITEM_FREIGHT_TYPE")
                Label535.Text = dr.Item("ITEM_FREIGHT_TYPE")
                TextBox764.Text = dr.Item("ITEM_DELIVERY")
                TextBox772.Text = dr.Item("ITEM_DELIVERY")
                Label538.Text = dr.Item("ITEM_DETAILS")
                Label622.Text = dr.Item("ITEM_VOCAB")
                dr.Close()
            End If
            conn.Close()
            ''GRID VIEW DATA FILL
            conn.Open()
            dt.Clear()

            If (PO_TYPE = "RAW MATERIAL") Then
                da = New SqlDataAdapter("SELECT *,MAT_NAME AS ITEM_NAME,ORD_AU AS ITEM_AU FROM SO_MAT_ORDER JOIN MATERIAL ON SO_MAT_ORDER.ITEM_CODE= MATERIAL.MAT_CODE WHERE SO_MAT_ORDER.SO_NO='" & DropDownList4.SelectedValue & "' AND SO_MAT_ORDER.ITEM_SLNO=" & DropDownList2.SelectedValue & " AND SO_MAT_ORDER.ITEM_CODE='" & DropDownList57.SelectedValue & "'", conn)
            Else
                da = New SqlDataAdapter("SELECT * FROM SO_MAT_ORDER JOIN F_ITEM ON SO_MAT_ORDER.ITEM_CODE= F_ITEM.ITEM_CODE WHERE SO_MAT_ORDER.SO_NO='" & DropDownList4.SelectedValue & "' AND SO_MAT_ORDER.ITEM_SLNO=" & DropDownList2.SelectedValue & " AND SO_MAT_ORDER.ITEM_CODE='" & DropDownList57.SelectedValue & "'", conn)
            End If

            count = da.Fill(dt)
            conn.Close()
            GridView213.DataSource = dt
            GridView213.DataBind()

            MultiView1.ActiveViewIndex = 1

        ElseIf order_type = "Work Order" Then
            QUARY = "select MAX(SUPL_ID) AS SUPL_ID,MAX(WO_TYPE) AS WO_TYPE,SUM(W_QTY) AS W_QTY, MAX(W_AU) AS W_AU, SUM(W_UNIT_PRICE) AS W_UNIT_PRICE, SUM(W_DISCOUNT) AS W_DISCOUNT, SUM(SGST) AS SGST, SUM(CGST) AS CGST, SUM(IGST) AS IGST, SUM(CESS) AS CESS, MAX(W_END_DATE) AS W_END_DATE,MAX(W_NAME) AS W_NAME FROM WO_ORDER WHERE W_SLNO=" & DropDownList2.SelectedValue & " AND PO_NO='" & DropDownList4.SelectedValue & "'"
            ''TEXTBOX DATA FILL
            conn.Open()
            mc1.CommandText = QUARY
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox773.Text = dr.Item("W_QTY")
                Label542.Text = dr.Item("W_AU")
                Label559.Text = dr.Item("W_AU")
                TextBox774.Text = dr.Item("W_UNIT_PRICE")
                TextBox776.Text = dr.Item("W_DISCOUNT")
                TextBox778.Text = dr.Item("SGST")
                TextBox779.Text = dr.Item("CGST")
                TextBox14.Text = dr.Item("IGST")
                TextBox15.Text = dr.Item("CESS")
                TextBox780.Text = dr.Item("W_END_DATE")
                TextBox788.Text = dr.Item("W_END_DATE")
                Label574.Text = dr.Item("W_NAME")
                Label29.Text = dr.Item("WO_TYPE")
                Label31.Text = dr.Item("SUPL_ID")

                dr.Close()
            End If
            conn.Close()
            ''GRID VIEW DATA FILL
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT * FROM WO_ORDER WHERE PO_NO ='" & DropDownList4.SelectedValue & "' AND W_SLNO =" & DropDownList2.SelectedValue, conn)
            count = da.Fill(dt)
            conn.Close()
            GridView214.DataSource = dt
            GridView214.DataBind()
            Label622.Text = ""
            ''Panel39.Visible = False

            MultiView1.ActiveViewIndex = 2

        End If
    End Sub



    Protected Sub Button63_Click(sender As Object, e As EventArgs) Handles Button63.Click
        If TextBox745.Text = "" Or IsNumeric(TextBox745.Text) = False Then
            TextBox745.Text = ""
            TextBox745.Focus()
            Return
        ElseIf TextBox5.Text = "" Or IsNumeric(TextBox5.Text) = False Then
            TextBox5.Text = ""
            TextBox5.Focus()
            Return
        ElseIf TextBox746.Text = "" Or IsDate(TextBox746.Text) = False Then
            TextBox746.Text = ""
            TextBox746.Focus()
            Return
        ElseIf CDate(TextBox744.Text) > CDate(TextBox746.Text) Then
            TextBox746.Text = ""
            TextBox746.Focus()
            Return
        End If


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                ''insert new value row

                Dim QUARY3 As String = ""
                QUARY3 = "Insert Into PO_ORD_MAT(PO_NO, MAT_SLNO,MAT_CODE,MAT_NAME,MAT_QTY,MAT_UNIT_RATE,MAT_PACK,MAT_DISCOUNT,SGST,CGST,IGST,CESS,MAT_FREIGHT_PU,MAT_DELIVERY,MAT_QTY_RCVD,MAT_STATUS,AMD_NO,AMD_DATE,MAT_DESC,PF_TYPE,DISC_TYPE,FREIGHT_TYPE)values(@PO_NO,@MAT_SLNO,@MAT_CODE,@MAT_NAME,@MAT_QTY,@MAT_UNIT_RATE,@MAT_PACK,@MAT_DISCOUNT,@SGST,@CGST,@IGST,@CESS,@MAT_FREIGHT_PU,@MAT_DELIVERY,@MAT_QTY_RCVD,@MAT_STATUS,@AMD_NO,@AMD_DATE,@MAT_DESC,@PF_TYPE,@DISC_TYPE,@FREIGHT_TYPE)"
                Dim cmd3 As New SqlCommand(QUARY3, conn_trans, myTrans)
                cmd3.Parameters.AddWithValue("@PO_NO", DropDownList4.SelectedValue)
                cmd3.Parameters.AddWithValue("@MAT_SLNO", DropDownList2.SelectedValue)
                cmd3.Parameters.AddWithValue("@MAT_CODE", DropDownList57.Text)
                cmd3.Parameters.AddWithValue("@MAT_NAME", Label622.Text)
                cmd3.Parameters.AddWithValue("@MAT_QTY", CDec(TextBox745.Text))
                cmd3.Parameters.AddWithValue("@MAT_UNIT_RATE", CDec(TextBox5.Text))
                cmd3.Parameters.AddWithValue("@MAT_PACK", CDec(TextBox753.Text))
                cmd3.Parameters.AddWithValue("@MAT_DISCOUNT", CDec(TextBox752.Text))
                cmd3.Parameters.AddWithValue("@MAT_FREIGHT_PU", CDec(TextBox756.Text))
                cmd3.Parameters.AddWithValue("@SGST", CDec(TextBox6.Text))
                cmd3.Parameters.AddWithValue("@CGST", CDec(TextBox7.Text))
                cmd3.Parameters.AddWithValue("@IGST", CDec(TextBox8.Text))
                cmd3.Parameters.AddWithValue("@CESS", CDec(TextBox9.Text))
                cmd3.Parameters.AddWithValue("@MAT_DELIVERY", Date.ParseExact(CDate(TextBox746.Text), "dd-MM-yyyy", provider))
                cmd3.Parameters.AddWithValue("@MAT_QTY_RCVD", 0)
                cmd3.Parameters.AddWithValue("@MAT_STATUS", "Pending")
                cmd3.Parameters.AddWithValue("@AMD_NO", TextBox809.Text)
                cmd3.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox810.Text), "dd-MM-yyyy", provider))
                cmd3.Parameters.AddWithValue("@MAT_DESC", Label503.Text)
                cmd3.Parameters.AddWithValue("@PF_TYPE", Label497.Text)
                cmd3.Parameters.AddWithValue("@DISC_TYPE", Label498.Text)
                cmd3.Parameters.AddWithValue("@FREIGHT_TYPE", Label494.Text)
                cmd3.ExecuteReader()
                cmd3.Dispose()



                'count = 0
                'conn.Open()
                'dt.Clear()
                'da = New SqlDataAdapter("select * from PO_ORD_MAT JOIN MATERIAL ON PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where PO_ORD_MAT.PO_NO='" & DropDownList4.SelectedValue & "' and PO_ORD_MAT.MAT_SLNO=" & DropDownList2.SelectedValue & " ORDER BY AMD_NO", conn)
                ''da.Fill(dt)
                'da.Fill(dt)
                'conn.Close()




                ''TDS CHECK TDS ON GST CRITERIA
                Dim base_value, TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0.00)
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "DECLARE @TT TABLE(PO_NO VARCHAR(30),base_value DECIMAL(16,3))
                INSERT INTO @TT
                select PO_NO,sum(MAT_QTY)*sum(MAT_UNIT_RATE) As base_value from PO_ORD_MAT WITH(NOLOCK) where PO_NO='" & DropDownList4.SelectedValue & "' group by PO_NO,MAT_SLNO

                SELECT PO_NO,SUM(base_value) as new_base_value FROM @TT group by PO_NO"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    base_value = dr.Item("new_base_value")
                    dr.Close()
                End If
                conn.Close()

                If base_value >= 250000 Then
                    If CDec(TextBox3.Text) > 0 Then
                        TDS_SGST = 0.00
                        TDS_CGST = 0.00
                        TDS_IGST = 2.0

                    Else
                        TDS_SGST = 1.0
                        TDS_CGST = 1.0
                        TDS_IGST = 0.00
                    End If

                    mycommand = New SqlCommand("update PO_ORD_MAT set TDS_CGST=" & TDS_CGST & ", TDS_SGST=" & TDS_SGST & ",TDS_IGST=" & TDS_IGST & " where PO_NO='" & DropDownList4.SelectedValue & "'", conn_trans, myTrans)
                    mycommand.ExecuteReader()
                    mycommand.Dispose()

                ElseIf base_value < 250000 Then

                    TDS_SGST = 0.00
                    TDS_CGST = 0.00
                    TDS_IGST = 0.00

                    mycommand = New SqlCommand("update PO_ORD_MAT set TDS_CGST=" & TDS_CGST & ", TDS_SGST=" & TDS_SGST & ",TDS_IGST=" & TDS_IGST & " where PO_NO='" & DropDownList4.SelectedValue & "'", conn_trans, myTrans)
                    mycommand.ExecuteReader()
                    mycommand.Dispose()

                End If

                myTrans.Commit()
                conn_trans.Close()
                Label33.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn_trans.Close()
                conn.Close()
                Label33.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try


        End Using

        Dim ds As New DataSet()
        conn.Open()
        Dim cmd As New SqlCommand("select * from PO_ORD_MAT JOIN MATERIAL ON PO_ORD_MAT.MAT_CODE=MATERIAL.MAT_CODE where PO_ORD_MAT.PO_NO='" & DropDownList4.SelectedValue & "' and PO_ORD_MAT.MAT_SLNO=" & DropDownList2.SelectedValue & " ORDER BY AMD_NO", conn)
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(ds)
        conn.Close()

        GridView212.DataSource = ds
        GridView212.DataBind()

    End Sub

    Protected Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        If TextBox809.Text = "" Then
            TextBox809.Text = ""
            TextBox809.Focus()
            Return
        ElseIf TextBox810.Text = "" Or IsDate(TextBox810.Text) = False Then
            TextBox810.Text = ""
            TextBox810.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Return
        ElseIf DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Return
        ElseIf DropDownList4.SelectedValue = "Select" Then
            DropDownList4.Focus()
            Return
        ElseIf DropDownList57.SelectedValue = "Select" Then
            DropDownList57.Focus()
            Return
        ElseIf TextBox765.Text = "" Or IsNumeric(TextBox765.Text) = False Then
            TextBox765.Text = ""
            TextBox765.Focus()
            Return
        ElseIf TextBox766.Text = "" Or IsNumeric(TextBox766.Text) = False Then
            TextBox766.Text = ""
            TextBox766.Focus()
            Return
        ElseIf TextBox767.Text = "" Or IsNumeric(TextBox767.Text) = False Then
            TextBox767.Text = ""
            TextBox767.Focus()
            Return
        ElseIf TextBox768.Text = "" Or IsNumeric(TextBox768.Text) = False Then
            TextBox768.Text = ""
            TextBox768.Focus()
            Return
        ElseIf TextBox769.Text = "" Or IsNumeric(TextBox769.Text) = False Then
            TextBox769.Text = ""
            TextBox769.Focus()
            Return
        ElseIf TextBox770.Text = "" Or IsNumeric(TextBox770.Text) = False Then
            TextBox770.Text = ""
            TextBox770.Focus()
            Return
        ElseIf TextBox807.Text = "" Or IsNumeric(TextBox807.Text) = False Then
            TextBox807.Text = ""
            TextBox807.Focus()
            Return
        ElseIf TextBox808.Text = "" Or IsNumeric(TextBox808.Text) = False Then
            TextBox808.Text = ""
            TextBox808.Focus()
            Return
        ElseIf TextBox771.Text = "" Or IsNumeric(TextBox771.Text) = False Then
            TextBox771.Text = ""
            TextBox771.Focus()
            Return
        ElseIf IsDate(TextBox772.Text) = False Then
            TextBox772.Text = ""
            TextBox772.Focus()
            Return
        End If
        ''quantity wise weight search
        Dim weight As Decimal = 0
        If CDec(TextBox765.Text) > 0 Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select ITEM_WEIGHT from F_ITEM where ITEM_CODE = '" & DropDownList57.SelectedValue & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                weight = dr.Item("ITEM_WEIGHT")
                dr.Close()
            End If
            conn.Close()
            weight = (CDec(TextBox765.Text) * weight) / 1000
        Else
            weight = 0.0
        End If


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry

                Dim QUARY1 As String = ""
                QUARY1 = "Insert Into SO_MAT_ORDER(PACK_TYPE,DISC_TYPE,SO_NO,ITEM_VOCAB,ITEM_SLNO,ITEM_CODE,ORD_AU,ITEM_QTY,ITEM_MT,ITEM_UNIT_RATE,ITEM_PACK,ITEM_DISCOUNT,ITEM_SGST,ITEM_CGST,ITEM_IGST,ITEM_CESS,ITEM_TERMINAL_TAX,ITEM_TCS,ITEM_FREIGHT_TYPE,ITEM_FREIGHT_PU,ITEM_DELIVERY,ITEM_QTY_SEND,ITEM_S_TAX, ITEM_STATUS,AMD_NO,AMD_DATE,ITEM_DETAILS)values(@PACK_TYPE,@DISC_TYPE,@SO_NO,@ITEM_VOCAB,@ITEM_SLNO,@ITEM_CODE,@ORD_AU,@ITEM_QTY,@ITEM_MT,@ITEM_UNIT_RATE,@ITEM_PACK,@ITEM_DISCOUNT,@ITEM_SGST,@ITEM_CGST,@ITEM_IGST,@ITEM_CESS,@ITEM_TERMINAL_TAX,@ITEM_TCS,@ITEM_FREIGHT_TYPE,@ITEM_FREIGHT_PU,@ITEM_DELIVERY,@ITEM_QTY_SEND,@ITEM_S_TAX, @ITEM_STATUS,@AMD_NO,@AMD_DATE,@ITEM_DETAILS)"
                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@SO_NO", DropDownList4.SelectedValue)
                cmd1.Parameters.AddWithValue("@ITEM_VOCAB", Label622.Text)
                cmd1.Parameters.AddWithValue("@ITEM_SLNO", DropDownList2.SelectedValue)
                cmd1.Parameters.AddWithValue("@ITEM_CODE", DropDownList57.SelectedValue)
                cmd1.Parameters.AddWithValue("@ORD_AU", Label506.Text)
                cmd1.Parameters.AddWithValue("@ITEM_QTY", CDec(TextBox765.Text))
                cmd1.Parameters.AddWithValue("@ITEM_MT", weight)
                cmd1.Parameters.AddWithValue("@ITEM_UNIT_RATE", CDec(TextBox766.Text))
                cmd1.Parameters.AddWithValue("@ITEM_PACK", CDec(TextBox768.Text))
                cmd1.Parameters.AddWithValue("@PACK_TYPE", Label529.Text)
                cmd1.Parameters.AddWithValue("@ITEM_DISCOUNT", CDec(TextBox767.Text))
                cmd1.Parameters.AddWithValue("@DISC_TYPE", Label527.Text)
                cmd1.Parameters.AddWithValue("@ITEM_SGST", CDec(TextBox769.Text))
                cmd1.Parameters.AddWithValue("@ITEM_CGST", CDec(TextBox770.Text))
                cmd1.Parameters.AddWithValue("@ITEM_IGST", CDec(TextBox12.Text))
                cmd1.Parameters.AddWithValue("@ITEM_CESS", CDec(TextBox13.Text))
                cmd1.Parameters.AddWithValue("@ITEM_TERMINAL_TAX", CDec(TextBox807.Text))
                cmd1.Parameters.AddWithValue("@ITEM_TCS", 0)
                cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_TYPE", Label535.Text)
                cmd1.Parameters.AddWithValue("@ITEM_FREIGHT_PU", CDec(TextBox771.Text))
                cmd1.Parameters.AddWithValue("@ITEM_DELIVERY", Date.ParseExact(TextBox772.Text, "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@ITEM_QTY_SEND", 0.0)
                cmd1.Parameters.AddWithValue("@ITEM_S_TAX", 0.0)
                cmd1.Parameters.AddWithValue("@ITEM_STATUS", "PENDING")
                cmd1.Parameters.AddWithValue("@AMD_NO", TextBox809.Text)
                cmd1.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox810.Text), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@ITEM_DETAILS", Label538.Text)
                cmd1.ExecuteReader()
                cmd1.Dispose()

                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("SELECT * FROM SO_MAT_ORDER WITH(NOLOCK) JOIN F_ITEM ON SO_MAT_ORDER.ITEM_CODE= F_ITEM.ITEM_CODE WHERE SO_MAT_ORDER.SO_NO='" & DropDownList4.SelectedValue & "' AND SO_MAT_ORDER.ITEM_SLNO=" & DropDownList2.SelectedValue & " AND SO_MAT_ORDER.ITEM_CODE='" & DropDownList57.SelectedValue & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                GridView213.DataSource = dt
                GridView213.DataBind()

                myTrans.Commit()

                Label33.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label33.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub

    Protected Sub Button69_Click(sender As Object, e As EventArgs) Handles Button69.Click

        Dim AMD_DATE As Date
        AMD_DATE = CDate(TextBox810.Text)
        Dim goAheadFlag As Boolean = True
        ''CHECK IF AMENDMENT WITH SAME DATE IS PRESENT OR NOT

        'Dim MC As New SqlCommand
        'conn.Open()
        'MC.CommandText = "SELECT AMD_DATE FROM WO_ORDER WHERE PO_NO='" & DropDownList4.SelectedValue & "' AND W_SLNO='" & DropDownList2.SelectedValue & "' AND AMD_DATE='" & AMD_DATE.Year & "-" & AMD_DATE.Month & "-" & AMD_DATE.Day & "'"
        'MC.Connection = conn
        'dr = MC.ExecuteReader
        'If dr.HasRows Then
        '    dr.Read()
        '    If IsDBNull(dr.Item("AMD_DATE")) Then
        '        AMD_DATE = ""
        '    Else
        '        AMD_DATE = dr.Item("AMD_DATE")
        '    End If

        '    dr.Close()
        'Else
        '    conn.Close()
        'End If
        'conn.Close()

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim MC As New SqlCommand
                conn.Open()
                MC.CommandText = "SELECT AMD_DATE FROM WO_ORDER WHERE PO_NO='" & DropDownList4.SelectedValue & "' AND W_SLNO='" & DropDownList2.SelectedValue & "' AND AMD_DATE='" & AMD_DATE.Year & "-" & AMD_DATE.Month & "-" & AMD_DATE.Day & "'"
                MC.Connection = conn
                dr = MC.ExecuteReader

                If dr.HasRows Then
                    dr.Read()
                    If IsDBNull(dr.Item("AMD_DATE")) Then
                        goAheadFlag = True
                    Else
                        goAheadFlag = False
                        Label622.Text = "Amendment with same date already exist."
                    End If
                    dr.Close()

                Else
                    goAheadFlag = True
                End If

                If (goAheadFlag) Then
                    Label622.Text = ""

                    'conn.Open()
                    Dim query As String = "Insert Into WO_ORDER(AMD_ENTRY_DATE,AMD_DATE,WO_TYPE,PO_NO, SUPL_ID, W_SLNO,W_NAME,W_AU,W_UNIT_PRICE,W_QTY,W_DISCOUNT,SGST,CGST,IGST,CESS,W_END_DATE,W_COMPLITED,W_STATUS,WO_AMD) values (@AMD_ENTRY_DATE,@AMD_DATE,@WO_TYPE,@PO_NO, @SUPL_ID,@W_SLNO,@W_NAME,@W_AU,@W_UNIT_PRICE,@W_QTY,@W_DISCOUNT,@SGST,@CGST,@IGST,@CESS,@W_END_DATE,@W_COMPLITED,@W_STATUS,@WO_AMD)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@PO_NO", DropDownList4.SelectedValue)
                    cmd.Parameters.AddWithValue("@WO_TYPE", Label29.Text)
                    cmd.Parameters.AddWithValue("@SUPL_ID", Label31.Text)
                    cmd.Parameters.AddWithValue("@W_SLNO", DropDownList2.SelectedValue)
                    cmd.Parameters.AddWithValue("@W_NAME", Label574.Text)
                    cmd.Parameters.AddWithValue("@W_AU", Label542.Text)
                    cmd.Parameters.AddWithValue("@W_UNIT_PRICE", CDec(TextBox782.Text))
                    cmd.Parameters.AddWithValue("@W_QTY", CDec(TextBox781.Text))
                    cmd.Parameters.AddWithValue("@W_DISCOUNT", CDec(TextBox784.Text))
                    cmd.Parameters.AddWithValue("@SGST", CDec(TextBox785.Text))
                    cmd.Parameters.AddWithValue("@CGST", CDec(TextBox786.Text))
                    cmd.Parameters.AddWithValue("@IGST", CDec(TextBox787.Text))
                    cmd.Parameters.AddWithValue("@CESS", CDec(TextBox16.Text))
                    cmd.Parameters.AddWithValue("@W_END_DATE", Date.ParseExact(CDate(TextBox788.Text), "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@W_COMPLITED", 0.0)
                    cmd.Parameters.AddWithValue("@W_STATUS", "PENDING")
                    cmd.Parameters.AddWithValue("@WO_AMD", TextBox809.Text)
                    cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(TextBox810.Text), "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@AMD_ENTRY_DATE", Now)
                    cmd.ExecuteReader()
                    cmd.Dispose()


                    ''TDS CHECK TDS ON GST CRITERIA
                    Dim base_value, TDS_SGST, TDS_CGST, TDS_IGST As New Decimal(0.00)
                    'conn.Open()
                    Dim mc1 As New SqlCommand
                    mc1.CommandText = "DECLARE @TT TABLE(PO_NO VARCHAR(30),base_value DECIMAL(16,3))
                        INSERT INTO @TT
                        select PO_NO,sum(W_QTY)*sum(W_UNIT_PRICE) As base_value from WO_ORDER WITH(NOLOCK) where PO_NO='" & DropDownList4.SelectedValue & "' group by PO_NO,W_SLNO

                        SELECT PO_NO,SUM(base_value) as new_base_value FROM @TT group by PO_NO"
                    mc1.Connection = conn

                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        base_value = dr.Item("new_base_value")
                        dr.Close()
                    End If
                    conn.Close()

                    If base_value >= 250000 Then
                        If CDec(TextBox14.Text) > 0 Then
                            TDS_SGST = 0.00
                            TDS_CGST = 0.00
                            TDS_IGST = 2.0

                        Else
                            TDS_SGST = 1.0
                            TDS_CGST = 1.0
                            TDS_IGST = 0.00
                        End If

                        mycommand = New SqlCommand("update WO_ORDER set TDS_CGST=" & TDS_CGST & ", TDS_SGST=" & TDS_SGST & ",TDS_IGST=" & TDS_IGST & " where PO_NO='" & DropDownList4.SelectedValue & "'", conn_trans, myTrans)
                        mycommand.ExecuteReader()
                        mycommand.Dispose()


                    ElseIf base_value < 250000 Then

                        TDS_SGST = 0.00
                        TDS_CGST = 0.00
                        TDS_IGST = 0.00


                        mycommand = New SqlCommand("update WO_ORDER set TDS_CGST=" & TDS_CGST & ", TDS_SGST=" & TDS_SGST & ",TDS_IGST=" & TDS_IGST & " where PO_NO='" & DropDownList4.SelectedValue & "'", conn_trans, myTrans)
                        mycommand.ExecuteReader()
                        mycommand.Dispose()

                    End If

                    myTrans.Commit()

                    conn.Open()
                    dt.Clear()
                    da = New SqlDataAdapter("SELECT * FROM WO_ORDER WITH(NOLOCK) WHERE PO_NO ='" & DropDownList4.SelectedValue & "' AND W_SLNO =" & DropDownList2.SelectedValue, conn)
                    count = da.Fill(dt)
                    conn.Close()
                    GridView214.DataSource = dt
                    GridView214.DataBind()
                    Label33.Text = "All records are written to database."
                Else
                End If

            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label33.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using




    End Sub

End Class