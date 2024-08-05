Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class WebForm1
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
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            dt.Clear()
            conn.Open()
            da = New SqlDataAdapter("SELECT DISTINCT BE_NO FROM BE_DA WHERE BE_COND = 'PENDING'", conn)
            da.Fill(dt)
            TextBox202.DataSource = dt
            TextBox202.DataValueField = "BE_NO"
            TextBox202.DataBind()
            TextBox202.Items.Add("Select")
            TextBox202.SelectedValue = "Select"
            conn.Close()

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            Return
        ElseIf DropDownList2.SelectedValue = "Material Charge" Then
           
            'SEARCH DATA
            Dim cond As String = ""
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "' AND BE_COND='PENDING') select 'yes' AS COND else select 'no' AS COND"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                cond = dr.Item("COND")
                dr.Close()
                conn.Close()
            End If
            conn.Close()
            If cond = "yes" Then
                Dim mat_value, update_date As String
                mat_value = ""
                update_date = ""
                conn.Open()
                mc1.CommandText = "SELECT  MAT_PRICE,MP_UPDATE_DATE FROM BE_DA WHERE BE_NO='" & TextBox202.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    mat_value = dr.Item("MAT_PRICE")
                    update_date = dr.Item("MP_UPDATE_DATE")
                    dr.Close()
                    conn.Close()
                End If
                conn.Close()
                MAT_CHARGE_TEXTBOX.Text = mat_value
                TextBox2.Text = update_date
                Panel1.Visible = True
                Panel2.Visible = False
                Panel3.Visible = False
                Panel4.Visible = False
                Panel5.Visible = False
                Panel6.Visible = False
                Panel7.Visible = False
                MAT_CHARGE_TEXTBOX.Focus()
            Else
                Return
            End If
            Return
        ElseIf DropDownList2.SelectedValue = "Insurance Charge" Then
           
            'search data
            Dim cond As String = ""
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "' AND BE_COND='PENDING') select 'yes' AS COND else select 'no' AS COND"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                cond = dr.Item("COND")
                dr.Close()
                conn.Close()
            End If
            conn.Close()
            If cond = "yes" Then
                Panel1.Visible = False
                Panel2.Visible = True
                Panel3.Visible = False
                Panel4.Visible = False
                Panel5.Visible = False
                Panel6.Visible = False
                Panel7.Visible = False
                Dim sea_insu, insu_charge, insu_date As String
                sea_insu = ""
                insu_date = ""
                insu_charge = ""
                conn.Open()
                mc1.CommandText = "SELECT INSURANCE,SEA_INSURANCE,INSU_UPDATE_DATE FROM BE_DA WHERE BE_NO='" & TextBox202.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    sea_insu = dr.Item("INSURANCE")
                    insu_charge = dr.Item("SEA_INSURANCE")
                    insu_date = dr.Item("INSU_UPDATE_DATE")
                    dr.Close()
                    conn.Close()
                End If
                conn.Close()
                be_insurance_sea_TextBox18.Text = sea_insu
                be_insu_TextBox13.Text = insu_charge
                TextBox3.Text = insu_date
                be_insurance_sea_TextBox18.Focus()
            Else

                Return
            End If
            Return
        ElseIf DropDownList2.SelectedValue = "Custom Duty" Then
            'SEARCH DATA 
            Dim cond As String = ""
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "' AND BE_COND='PENDING') select 'yes' AS COND else select 'no' AS COND"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                cond = dr.Item("COND")
                dr.Close()
                conn.Close()
            End If
            conn.Close()
            If cond = "yes" Then
                Panel1.Visible = False
                Panel2.Visible = False
                Panel3.Visible = True
                Panel4.Visible = False
                Panel5.Visible = False
                Panel6.Visible = False
                Panel7.Visible = False
                Dim custom, cenvat_charge, cust_date As String
                cenvat_charge = ""
                custom = ""
                cust_date = ""
                conn.Open()
                mc1.CommandText = "SELECT MAT_CUSTOM,MAT_ADD_CUSTOM,CUSTOM_DATE FROM BE_DA WHERE BE_NO='" & TextBox202.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    custom = dr.Item("MAT_CUSTOM")
                    cenvat_charge = dr.Item("MAT_ADD_CUSTOM")
                    cust_date = dr.Item("CUSTOM_DATE")
                    dr.Close()
                    conn.Close()
                End If
                conn.Close()
                BE_CUST_TextBox183.Text = custom
                BE_CENVAT_TextBox1.Text = cenvat_charge
                TextBox4.Text = cust_date
                BE_CUST_TextBox183.Focus()
            Else

                Return
            End If
            Return
        ElseIf DropDownList2.SelectedValue = "Freight Charge" Then
            'SEARCH DATA 
            Dim cond As String = ""
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "' AND BE_COND='PENDING') select 'yes' AS COND else select 'no' AS COND"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                cond = dr.Item("COND")
                dr.Close()
                conn.Close()
            End If
            conn.Close()
            If cond = "yes" Then
                Panel1.Visible = False
                Panel2.Visible = False
                Panel3.Visible = False
                Panel4.Visible = True
                Panel5.Visible = False
                Panel6.Visible = False
                Panel7.Visible = False
                be_ocenfreight_TextBox12.Focus()
                Dim freight, freight_date As String
                freight = ""
                freight_date = ""
                conn.Open()
                mc1.CommandText = "SELECT OCEAN_FREIGHT,OF_UPDATE_DATE FROM BE_DA WHERE BE_NO='" & TextBox202.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    freight = dr.Item("OCEAN_FREIGHT")
                    freight_date = dr.Item("OF_UPDATE_DATE")
                    dr.Close()
                    conn.Close()
                End If
                conn.Close()
                be_ocenfreight_TextBox12.Text = freight
                TextBox5.Text = freight_date
            Else

                Return
            End If
            Return
        ElseIf DropDownList2.SelectedValue = "Statutory Charges" Then
            'SEARCH DATA 
            Dim cond As String = ""
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "' AND BE_COND='PENDING') select 'yes' AS COND else select 'no' AS COND"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                cond = dr.Item("COND")
                dr.Close()
                conn.Close()
            End If
            conn.Close()
            If cond = "yes" Then
                Panel1.Visible = False
                Panel2.Visible = False
                Panel3.Visible = False
                Panel4.Visible = False
                Panel5.Visible = True
                Panel6.Visible = False
                Panel7.Visible = False
                be_sat_TextBox184.Focus()

                Dim THC, CONT_CLEEN, CONT_MONITOR, DO_PRICE, SURVAY_FEES, DOCU_FEES, BROKER_FEES, REPAIR_FEES, ONCAR_INLAND_FEES, PROSSES_FEES, BL_FEES, DESTU_FEES, SAT_UPDATE_DATE As New String("")
                conn.Open()
                mc1.CommandText = "SELECT * FROM BE_DA WHERE BE_NO='" & TextBox202.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    THC = dr.Item("THC")
                    CONT_CLEEN = dr.Item("CONT_CLEEN")
                    CONT_MONITOR = dr.Item("CONT_MONITOR")
                    DO_PRICE = dr.Item("DO_PRICE")
                    SURVAY_FEES = dr.Item("SURVAY_FEES")
                    DOCU_FEES = dr.Item("DOCU_FEES")
                    BROKER_FEES = dr.Item("BROKER_FEES")
                    REPAIR_FEES = dr.Item("REPAIR_FEES")
                    ONCAR_INLAND_FEES = dr.Item("ONCAR_INLAND_FEES")
                    PROSSES_FEES = dr.Item("PROSSES_FEES")
                    BL_FEES = dr.Item("BL_FEES")
                    DESTU_FEES = dr.Item("DESTU_FEES")
                    SAT_UPDATE_DATE = dr.Item("SAT_UPDATE_DATE")
                    dr.Close()
                    conn.Close()
                End If
                conn.Close()
                be_sat_TextBox184.Text = THC
                TextBox185.Text = CONT_CLEEN
                TextBox186.Text = CONT_MONITOR
                TextBox187.Text = DO_PRICE
                TextBox192.Text = SURVAY_FEES
                TextBox194.Text = DOCU_FEES
                TextBox188.Text = BROKER_FEES
                TextBox189.Text = REPAIR_FEES
                TextBox190.Text = ONCAR_INLAND_FEES
                TextBox191.Text = PROSSES_FEES
                TextBox193.Text = BL_FEES
                TextBox195.Text = DESTU_FEES
                TextBox6.Text = SAT_UPDATE_DATE

            Else

                Return
            End If
            Return

        ElseIf DropDownList2.SelectedValue = "Preview" Then
            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel5.Visible = False
            Panel6.Visible = True
            Panel7.Visible = True
            'search data
            Dim mc1 As New SqlCommand




            Dim mat_value, update_date As String
            mat_value = ""
            update_date = ""
            conn.Open()
            mc1.CommandText = "SELECT  * FROM BE_DA WHERE BE_NO='" & TextBox202.Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox1.Text = dr.Item("MAT_PRICE")
                TextBox7.Text = dr.Item("MP_UPDATE_DATE")
                TextBox8.Text = dr.Item("INSURANCE")
                TextBox9.Text = dr.Item("SEA_INSURANCE")
                TextBox10.Text = dr.Item("INSU_UPDATE_DATE")
                TextBox11.Text = dr.Item("MAT_CUSTOM")
                TextBox12.Text = dr.Item("MAT_ADD_CUSTOM")
                TextBox13.Text = dr.Item("CUSTOM_DATE")
                TextBox14.Text = dr.Item("OCEAN_FREIGHT")
                TextBox15.Text = dr.Item("OF_UPDATE_DATE")
                TextBox16.Text = dr.Item("THC")
                TextBox18.Text = dr.Item("CONT_CLEEN")
                TextBox20.Text = dr.Item("CONT_MONITOR")
                TextBox22.Text = dr.Item("DO_PRICE")
                TextBox24.Text = dr.Item("SURVAY_FEES")
                TextBox26.Text = dr.Item("DOCU_FEES")
                TextBox17.Text = dr.Item("BROKER_FEES")
                TextBox19.Text = dr.Item("REPAIR_FEES")
                TextBox21.Text = dr.Item("ONCAR_INLAND_FEES")
                TextBox23.Text = dr.Item("PROSSES_FEES")
                TextBox25.Text = dr.Item("BL_FEES")
                TextBox27.Text = dr.Item("DESTU_FEES")
                TextBox28.Text = dr.Item("SAT_UPDATE_DATE")
                TextBox29.Text = dr.Item("BE_QTY")
                TextBox30.Text = dr.Item("RCVD_QTY")
                dr.Close()
                conn.Close()
            End If
            conn.Close()
            TextBox32.Text = CDec(TextBox29.Text) - CDec(TextBox30.Text)
            TextBox31.Text = TextBox32.Text
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MAT_CHARGE_TEXTBOX.Text = "" Then
            MAT_CHARGE_TEXTBOX.Focus()
            Return
        ElseIf IsNumeric(MAT_CHARGE_TEXTBOX.Text) = False Then
            MAT_CHARGE_TEXTBOX.Text = ""
            MAT_CHARGE_TEXTBOX.Focus()
            Return
        End If
        Dim cond As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "') select 'yes' AS COND else select 'no' AS COND"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            cond = dr.Item("COND")
            dr.Close()
            conn.Close()
        End If
        conn.Close()
        If cond = "no" Then
            Return
        End If
        Dim quary As String
        conn.Open()
        quary = ""
        quary = "UPDATE BE_DA SET MAT_PRICE =" & CDec(MAT_CHARGE_TEXTBOX.Text) & " , MP_UPDATE_DATE=@MP_UPDATE_DATE WHERE BE_NO ='" & TextBox202.Text & "'AND PO_NO ='" & TextBox196.Text & "' AND MAT_SLNO ='" & TextBox197.Text & "'"
        Dim cmd2 As New SqlCommand(quary, conn)
        cmd2.Parameters.AddWithValue("@MP_UPDATE_DATE", Date.ParseExact(Today.Date, "dd-MM-yyyy", provider))
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()




    End Sub

    Protected Sub TextBox202_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TextBox202.SelectedIndexChanged
        If TextBox202.SelectedValue = "Select" Then
            Return
        End If
        conn.Open()
        Dim mc As New SqlCommand
        mc.CommandText = "select " & _
        " (select BE_DATE  from BE_DA where BE_NO ='" & TextBox202.Text & "') as be_date," & _
        " (select BL_NO  from BE_DA where BE_NO ='" & TextBox202.Text & "') as bl_no," & _
        " (select BL_DATE  from BE_DA where BE_NO ='" & TextBox202.Text & "') as bl_date," & _
        " (select INV_NO  from BE_DA where BE_NO ='" & TextBox202.Text & "') as inv_no," & _
        " (select INV_DATE  from BE_DA where BE_NO ='" & TextBox202.Text & "') as inv_date," & _
        " (select PO_NO  from BE_DA where BE_NO ='" & TextBox202.Text & "') as po_no," & _
        " (SELECT top 1 (SUPL.SUPL_ID + ' , ' + SUPL.SUPL_NAME) AS SUPL_NAME FROM ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS .PARTY_CODE =SUPL.SUPL_ID WHERE ORDER_DETAILS.SO_NO=(select PO_NO  from BE_DA where BE_NO ='" & TextBox202.Text & "')) as supl_name," & _
        " (select MAT_SLNO  from BE_DA where BE_NO ='" & TextBox202.Text & "') as mat_slno," & _
        " (SELECT (PO_ORD_MAT .MAT_CODE + ' , ' + PO_ORD_MAT .MAT_NAME) FROM PO_ORD_MAT JOIN MATERIAL  ON PO_ORD_MAT .MAT_CODE  =MATERIAL .MAT_CODE WHERE PO_ORD_MAT .MAT_SLNO =(select MAT_SLNO from BE_DA where BE_NO ='" & TextBox202.Text & "') and po_ord_mat.po_no=(select PO_NO  from BE_DA where BE_NO ='" & TextBox202.Text & "')) as mat_name," & _
        " (SELECT MATERIAL .MAT_AU  FROM PO_ORD_MAT JOIN MATERIAL  ON PO_ORD_MAT .MAT_CODE  =MATERIAL .MAT_CODE WHERE PO_ORD_MAT .MAT_SLNO =(select MAT_SLNO from BE_DA where BE_NO ='" & TextBox202.Text & "') and po_ord_mat.po_no=(select PO_NO  from BE_DA where BE_NO ='" & TextBox202.Text & "')) as mat_au," & _
        " (select BE_QTY  from BE_DA where BE_NO ='" & TextBox202.Text & "') as be_qty," & _
        " (select RCVD_QTY  from BE_DA where BE_NO ='" & TextBox202.Text & "') as rcd_qty," & _
        " (select TRANS_MODE  from BE_DA where BE_NO ='" & TextBox202.Text & "') as trans_mode," & _
        " (select SHIP_FLIGHT   from BE_DA where BE_NO ='" & TextBox202.Text & "') as ship_name," & _
        " (select CHA_ORDER  from BE_DA where BE_NO ='" & TextBox202.Text & "') as cha_wo," & _
        " (select top 1 (SUPL.SUPL_ID + ' , ' + SUPL.SUPL_NAME) from SUPL JOIN WO_ORDER ON WO_ORDER.SUPL_ID=SUPL.SUPL_ID WHERE WO_ORDER.PO_NO =(select CHA_ORDER from BE_DA where BE_NO ='" & TextBox202.Text & "')) as cha_name," & _
        " (select CHA_SLNO  from BE_DA where BE_NO ='" & TextBox202.Text & "') as cha_slno," & _
        " (select W_NAME FROM WO_ORDER WHERE PO_NO =(select CHA_ORDER  from BE_DA  where BE_NO ='" & TextBox202.Text & "') AND W_SLNO=(select CHA_SLNO  from BE_DA where BE_NO ='" & TextBox202.Text & "')) as cha_w_name"

        mc.Connection = conn
        dr = mc.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            be_date_TextBox4.Text = dr.Item("be_date")
            be_bl_no_TextBox6.Text = dr.Item("bl_no")
            be_bl_no_date_TextBox7.Text = dr.Item("bl_date")
            TextBox181.Text = dr.Item("inv_no")
            TextBox182.Text = dr.Item("inv_date")
            TextBox196.Text = dr.Item("po_no")
            TextBox177.Text = dr.Item("supl_name")
            TextBox197.Text = dr.Item("mat_slno")
            TextBox178.Text = dr.Item("mat_name")
            TextBox183.Text = dr.Item("mat_au")
            be_quantity_TextBox5.Text = dr.Item("be_qty")
            TextBox200.Text = dr.Item("rcd_qty")
            TextBox201.Text = dr.Item("trans_mode")
            be_ship_flight_name_TextBox8.Text = dr.Item("ship_name")
            TextBox198.Text = dr.Item("cha_wo")
            TextBox179.Text = dr.Item("cha_name")
            TextBox199.Text = dr.Item("cha_slno")
            TextBox180.Text = dr.Item("cha_w_name")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()



    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If be_insurance_sea_TextBox18.Text = "" Then
            be_insurance_sea_TextBox18.Focus()
            Return
        ElseIf IsNumeric(be_insurance_sea_TextBox18.Text) = False Then
            be_insurance_sea_TextBox18.Text = ""
            be_insurance_sea_TextBox18.Focus()
            Return
        ElseIf be_insu_TextBox13.Text = "" Then
            be_insu_TextBox13.Focus()
            Return
        ElseIf IsNumeric(be_insu_TextBox13.Text) = False Then
            be_insu_TextBox13.Text = ""
            be_insu_TextBox13.Focus()
            Return
        End If

        Dim cond As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "') select 'yes' AS COND else select 'no' AS COND"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            cond = dr.Item("COND")
            dr.Close()
            conn.Close()
        End If
        conn.Close()
        If cond = "no" Then
            Return
        End If
        Dim quary As String
        conn.Open()
        quary = ""
        quary = "UPDATE BE_DA SET SEA_INSURANCE  =" & CDec(be_insurance_sea_TextBox18.Text) & ",INSURANCE=" & CDec(be_insu_TextBox13.Text) & ",INSU_UPDATE_DATE=@INSU_UPDATE_DATE WHERE BE_NO ='" & TextBox202.Text & "'AND PO_NO ='" & TextBox196.Text & "' AND MAT_SLNO ='" & TextBox197.Text & "'"
        Dim cmd2 As New SqlCommand(quary, conn)
        cmd2.Parameters.AddWithValue("@INSU_UPDATE_DATE", Date.ParseExact(Today.Date, "dd-MM-yyyy", provider))
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()




    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If BE_CUST_TextBox183.Text = "" Then
            BE_CUST_TextBox183.Focus()
            Return
        ElseIf IsNumeric(BE_CUST_TextBox183.Text) = False Then
            BE_CUST_TextBox183.Text = ""
            BE_CUST_TextBox183.Focus()
            Return
        ElseIf BE_CENVAT_TextBox1.Text = "" Then
            BE_CENVAT_TextBox1.Focus()
            Return
        ElseIf IsNumeric(BE_CENVAT_TextBox1.Text) = False Then
            BE_CENVAT_TextBox1.Text = ""
            BE_CENVAT_TextBox1.Focus()
            Return
        End If

        Dim cond As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "') select 'yes' AS COND else select 'no' AS COND"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            cond = dr.Item("COND")
            dr.Close()
            conn.Close()
        End If
        conn.Close()
        If cond = "no" Then
            Return
        End If
        Dim quary As String
        conn.Open()
        quary = ""
        quary = "UPDATE BE_DA SET MAT_CUSTOM  =" & CDec(BE_CUST_TextBox183.Text) & ",MAT_ADD_CUSTOM=" & CDec(BE_CENVAT_TextBox1.Text) & ",CUSTOM_DATE=@CUSTOM_DATE WHERE BE_NO ='" & TextBox202.Text & "'AND PO_NO ='" & TextBox196.Text & "' AND MAT_SLNO ='" & TextBox197.Text & "'"
        Dim cmd2 As New SqlCommand(quary, conn)
        cmd2.Parameters.AddWithValue("@CUSTOM_DATE", Date.ParseExact(Today.Date, "dd-MM-yyyy", provider))
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If be_ocenfreight_TextBox12.Text = "" Then
            be_ocenfreight_TextBox12.Focus()
            Return
        ElseIf IsNumeric(be_ocenfreight_TextBox12.Text) = False Then
            be_ocenfreight_TextBox12.Text = ""
            be_ocenfreight_TextBox12.Focus()
            Return
        End If
        Dim cond As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "') select 'yes' AS COND else select 'no' AS COND"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            cond = dr.Item("COND")
            dr.Close()
            conn.Close()
        End If
        conn.Close()
        If cond = "no" Then
            Return
        End If
        Dim quary As String
        conn.Open()
        quary = ""
        quary = "UPDATE BE_DA SET OCEAN_FREIGHT =" & CDec(be_ocenfreight_TextBox12.Text) & " , OF_UPDATE_DATE=@OF_UPDATE_DATE WHERE BE_NO ='" & TextBox202.Text & "'AND PO_NO ='" & TextBox196.Text & "' AND MAT_SLNO ='" & TextBox197.Text & "'"
        Dim cmd2 As New SqlCommand(quary, conn)
        cmd2.Parameters.AddWithValue("@OF_UPDATE_DATE", Date.ParseExact(Today.Date, "dd-MM-yyyy", provider))
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
    End Sub

    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'thc
        If be_sat_TextBox184.Text = "" Then
            be_sat_TextBox184.Focus()
            Return
        ElseIf IsNumeric(be_sat_TextBox184.Text) = False Then
            be_sat_TextBox184.Text = ""
            be_sat_TextBox184.Focus()
            Return
            'container cleen
        ElseIf TextBox185.Text = "" Then
            TextBox185.Focus()
            Return
        ElseIf IsNumeric(TextBox185.Text) = False Then
            TextBox185.Text = ""
            TextBox185.Focus()
            Return

            'container moniter
        ElseIf TextBox186.Text = "" Then
            TextBox186.Focus()
            Return
        ElseIf IsNumeric(TextBox186.Text) = False Then
            TextBox186.Text = ""
            TextBox186.Focus()
            Return

            'do charge
        ElseIf TextBox187.Text = "" Then
            TextBox187.Focus()
            Return
        ElseIf IsNumeric(TextBox187.Text) = False Then
            TextBox187.Text = ""
            TextBox187.Focus()
            Return

            'survay fees
        ElseIf TextBox192.Text = "" Then
            TextBox192.Focus()
            Return
        ElseIf IsNumeric(TextBox192.Text) = False Then
            TextBox192.Text = ""
            TextBox192.Focus()
            Return

            'document fees
        ElseIf TextBox194.Text = "" Then
            TextBox194.Focus()
            Return
        ElseIf IsNumeric(TextBox194.Text) = False Then
            TextBox194.Text = ""
            TextBox194.Focus()
            Return

            'brokeage charges
        ElseIf TextBox188.Text = "" Then
            TextBox188.Focus()
            Return
        ElseIf IsNumeric(TextBox188.Text) = False Then
            TextBox188.Text = ""
            TextBox188.Focus()
            Return

            'repair fees
        ElseIf TextBox189.Text = "" Then
            TextBox189.Focus()
            Return
        ElseIf IsNumeric(TextBox189.Text) = False Then
            TextBox189.Text = ""
            TextBox189.Focus()
            Return

            'on carriage
        ElseIf TextBox190.Text = "" Then
            TextBox190.Focus()
            Return
        ElseIf IsNumeric(TextBox190.Text) = False Then
            TextBox190.Text = ""
            TextBox190.Focus()
            Return

            'pross charge
        ElseIf TextBox191.Text = "" Then
            TextBox191.Focus()
            Return
        ElseIf IsNumeric(TextBox191.Text) = False Then
            TextBox191.Text = ""
            TextBox191.Focus()
            Return

            'bl fees
        ElseIf TextBox193.Text = "" Then
            TextBox193.Focus()
            Return
        ElseIf IsNumeric(TextBox193.Text) = False Then
            TextBox193.Text = ""
            TextBox193.Focus()
            Return

            'destu charge
        ElseIf TextBox195.Text = "" Then
            TextBox195.Focus()
            Return
        ElseIf IsNumeric(TextBox195.Text) = False Then
            TextBox195.Text = ""
            TextBox195.Focus()
            Return
        End If





        Dim cond As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "if exists(select * from BE_DA where BE_NO='" & TextBox202.Text & "') select 'yes' AS COND else select 'no' AS COND"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            cond = dr.Item("COND")
            dr.Close()
            conn.Close()
        End If
        conn.Close()
        If cond = "no" Then
            Return
        End If
        Dim quary As String
        conn.Open()
        quary = ""
        quary = "UPDATE BE_DA SET THC=@THC,CONT_CLEEN=@CONT_CLEEN,CONT_MONITOR=@CONT_MONITOR, DO_PRICE=@DO_PRICE, SURVAY_FEES=@SURVAY_FEES, DOCU_FEES=@DOCU_FEES, BROKER_FEES=@BROKER_FEES, REPAIR_FEES=@REPAIR_FEES,ONCAR_INLAND_FEES=@ONCAR_INLAND_FEES,PROSSES_FEES=@PROSSES_FEES,BL_FEES=@BL_FEES, DESTU_FEES=@DESTU_FEES,SAT_UPDATE_DATE=@SAT_UPDATE_DATE WHERE BE_NO ='" & TextBox202.Text & "'AND PO_NO ='" & TextBox196.Text & "' AND MAT_SLNO ='" & TextBox197.Text & "'"
        Dim cmd2 As New SqlCommand(quary, conn)
        cmd2.Parameters.AddWithValue("@THC", CDec(be_sat_TextBox184.Text))
        cmd2.Parameters.AddWithValue("@CONT_CLEEN", CDec(TextBox185.Text))
        cmd2.Parameters.AddWithValue("@CONT_MONITOR", CDec(TextBox186.Text))
        cmd2.Parameters.AddWithValue("@DO_PRICE", CDec(TextBox187.Text))
        cmd2.Parameters.AddWithValue("@SURVAY_FEES", CDec(TextBox192.Text))
        cmd2.Parameters.AddWithValue("@DOCU_FEES", CDec(TextBox194.Text))
        cmd2.Parameters.AddWithValue("@BROKER_FEES", CDec(TextBox188.Text))
        cmd2.Parameters.AddWithValue("@REPAIR_FEES", CDec(TextBox189.Text))
        cmd2.Parameters.AddWithValue("@ONCAR_INLAND_FEES", CDec(TextBox190.Text))
        cmd2.Parameters.AddWithValue("@PROSSES_FEES", CDec(TextBox191.Text))
        cmd2.Parameters.AddWithValue("@BL_FEES", CDec(TextBox193.Text))
        cmd2.Parameters.AddWithValue("@DESTU_FEES", CDec(TextBox195.Text))
        cmd2.Parameters.AddWithValue("@SAT_UPDATE_DATE", Date.ParseExact(Today.Date, "dd-MM-yyyy", provider))
        cmd2.ExecuteReader()
        cmd2.Dispose()
        conn.Close()
    End Sub

    Protected Sub BE_SAVE_Click(sender As Object, e As EventArgs) Handles BE_SAVE.Click
        'CALCULATE UNIT PRICE AND UNIT CENVAT
        Dim TOTAL_PRICE, TOTAL_CENVAT, UNIT_VALUE, UNIT_CENVAT As New Decimal(0.0)
        TOTAL_PRICE = CDec(TextBox1.Text) + CDec(TextBox8.Text) + CDec(TextBox9.Text) + CDec(TextBox11.Text) + CDec(TextBox12.Text) + CDec(TextBox14.Text) + CDec(TextBox16.Text) + CDec(TextBox18.Text) + CDec(TextBox20.Text) + CDec(TextBox22.Text) + CDec(TextBox24.Text) + CDec(TextBox26.Text) + CDec(TextBox17.Text) + CDec(TextBox19.Text) + CDec(TextBox21.Text) + CDec(TextBox23.Text) + CDec(TextBox25.Text) + CDec(TextBox27.Text)
        TOTAL_CENVAT = CDec(TextBox12.Text)
        UNIT_VALUE = TOTAL_PRICE / CDec(be_quantity_TextBox5.Text)
        UNIT_CENVAT = TOTAL_CENVAT / CDec(be_quantity_TextBox5.Text)
        'UPDATE UNIT PRICE AND DA_COND







    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        'thc
        If be_sat_TextBox184.Text = "" Then
            be_sat_TextBox184.Focus()
            Return
        ElseIf IsNumeric(be_sat_TextBox184.Text) = False Then
            be_sat_TextBox184.Text = ""
            be_sat_TextBox184.Focus()
            Return
            'be_qty

        ElseIf TextBox31.Text = "" Then
            TextBox31.Focus()
            Return
        ElseIf IsNumeric(TextBox31.Text) = False Then
            TextBox31.Text = ""
            TextBox31.Focus()
            Return

            'container cleen
        ElseIf TextBox185.Text = "" Then
            TextBox185.Focus()
            Return
        ElseIf IsNumeric(TextBox185.Text) = False Then
            TextBox185.Text = ""
            TextBox185.Focus()
            Return

            'container moniter
        ElseIf TextBox186.Text = "" Then
            TextBox186.Focus()
            Return
        ElseIf IsNumeric(TextBox186.Text) = False Then
            TextBox186.Text = ""
            TextBox186.Focus()
            Return

            'do charge
        ElseIf TextBox187.Text = "" Then
            TextBox187.Focus()
            Return
        ElseIf IsNumeric(TextBox187.Text) = False Then
            TextBox187.Text = ""
            TextBox187.Focus()
            Return

            'survay fees
        ElseIf TextBox192.Text = "" Then
            TextBox192.Focus()
            Return
        ElseIf IsNumeric(TextBox192.Text) = False Then
            TextBox192.Text = ""
            TextBox192.Focus()
            Return

            'document fees
        ElseIf TextBox194.Text = "" Then
            TextBox194.Focus()
            Return
        ElseIf IsNumeric(TextBox194.Text) = False Then
            TextBox194.Text = ""
            TextBox194.Focus()
            Return

            'brokeage charges
        ElseIf TextBox188.Text = "" Then
            TextBox188.Focus()
            Return
        ElseIf IsNumeric(TextBox188.Text) = False Then
            TextBox188.Text = ""
            TextBox188.Focus()
            Return

            'repair fees
        ElseIf TextBox189.Text = "" Then
            TextBox189.Focus()
            Return
        ElseIf IsNumeric(TextBox189.Text) = False Then
            TextBox189.Text = ""
            TextBox189.Focus()
            Return

            'on carriage
        ElseIf TextBox190.Text = "" Then
            TextBox190.Focus()
            Return
        ElseIf IsNumeric(TextBox190.Text) = False Then
            TextBox190.Text = ""
            TextBox190.Focus()
            Return

            'pross charge
        ElseIf TextBox191.Text = "" Then
            TextBox191.Focus()
            Return
        ElseIf IsNumeric(TextBox191.Text) = False Then
            TextBox191.Text = ""
            TextBox191.Focus()
            Return

            'bl fees
        ElseIf TextBox193.Text = "" Then
            TextBox193.Focus()
            Return
        ElseIf IsNumeric(TextBox193.Text) = False Then
            TextBox193.Text = ""
            TextBox193.Focus()
            Return

            'destu charge
        ElseIf TextBox195.Text = "" Then
            TextBox195.Focus()
            Return
        ElseIf IsNumeric(TextBox195.Text) = False Then
            TextBox195.Text = ""
            TextBox195.Focus()
            Return
        End If
        TextBox203.Text = CDec(be_sat_TextBox184.Text) + CDec(TextBox185.Text) + CDec(TextBox186.Text) + CDec(TextBox187.Text) + CDec(TextBox192.Text) + CDec(TextBox194.Text) + CDec(TextBox188.Text) + CDec(TextBox189.Text) + CDec(TextBox190.Text) + CDec(TextBox191.Text) + CDec(TextBox193.Text) + CDec(TextBox195.Text)
    End Sub
End Class