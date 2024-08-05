Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class update_order
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

            conn.Open()
            da = New SqlDataAdapter("select ORDER_DETAILS.SO_NO,ORDER_DETAILS.SO_ACTUAL ,ORDER_DETAILS.SO_ACTUAL_DATE ,SUPL .SUPL_NAME   from ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS .PARTY_CODE =SUPL .SUPL_ID  where ORDER_DETAILS .SO_STATUS ='draft' ORDER BY SO_NO", conn)
            da.Fill(dt)
            conn.Close()
            GridView1.DataSource = dt
            GridView1.DataBind()
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("purchaseAccess")) Or Session("purchaseAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub
    Protected Sub prv(ByVal sender As Object, ByVal e As EventArgs)
        Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
        Dim index As Integer = gvRow.RowIndex
        Dim btn As LinkButton = CType(sender, LinkButton)
        Dim inv As String = btn.CommandName
        Dim cond As String = btn.Text
        Panel1.Visible = True
        Panel700.Visible = False

        '' search po_type
        Dim order_type, po_type, SUPL_NAME, SUPL_ID, SO_DATE, freight_term, ORDER_TO, dater_code As String
        order_type = ""
        po_type = ""
        SUPL_ID = ""
        SUPL_NAME = ""
        SO_DATE = ""
        freight_term = ""
        ORDER_TO = ""
        dater_code = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select  ORDER_DETAILS.ORDER_TO, ORDER_DETAILS.FREIGHT_TERM, ORDER_DETAILS.SO_ACTUAL_DATE,ORDER_DETAILS.PO_TYPE,ORDER_DETAILS.ORDER_TYPE  from ORDER_DETAILS WHERE ORDER_DETAILS.SO_NO = '" & inv & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            order_type = dr.Item("ORDER_TYPE")
            po_type = dr.Item("PO_TYPE")
            SO_DATE = dr.Item("SO_ACTUAL_DATE")
            freight_term = dr.Item("FREIGHT_TERM")
            ORDER_TO = dr.Item("ORDER_TO")
            dr.Close()
        End If
        conn.Close()











        If order_type = "Purchase Order" Then
            Label270.Text = "PURCHASE ORDER FOR " & po_type.ToUpper
            Panel47.Visible = False
            Panel46.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("100% Against GRN Within 30 Days")
            paymode.Items.Add("Advance Payment")
        ElseIf order_type = "Sale Order" Then
            Label270.Text = "SALE ORDER FOR " & po_type.ToUpper
            Panel46.Visible = False
            Panel47.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("100% Against GRN Within 30 Days")
            paymode.Items.Add("Advance Payment")
        ElseIf order_type = "Work Order" Then
            Label270.Text = "WORK ORDER FOR " & po_type.ToUpper
            Panel47.Visible = False
            Panel46.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("Against Running Bill")
            paymode.Items.Add("Advance Payment")
        ElseIf order_type = "Rate Contract" Then
            Label270.Text = "RATE CONTRACT FOR " & po_type.ToUpper
            Panel47.Visible = False
            Panel46.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("100% Against GRN Within 30 Days")
            paymode.Items.Add("Against Running Bill")
            paymode.Items.Add("Advance Payment")
        End If
        If order_type = "Purchase Order" Then
            Panel16.Visible = False
            Panel21.Visible = False
            Panel25.Visible = False
            Panel27.Visible = False
            Panel31.Visible = False
            Label251.Text = "Origin Station"
        ElseIf order_type = "Sale Order" Then
            Label251.Text = "Destination Station"
            Panel16.Visible = False
            Panel18.Visible = False
            Panel21.Visible = False
            Panel20.Visible = False
            Panel22.Visible = False
            Panel24.Visible = False
            Panel25.Visible = False
            Panel27.Visible = False
            Panel28.Visible = False
        ElseIf order_type = "Work Order" Then
            Panel12.Visible = False
            Panel13.Visible = False
            Panel15.Visible = False
            Panel16.Visible = False
            Panel17.Visible = False
            Panel18.Visible = False
            Panel21.Visible = False
            Panel22.Visible = False
            Panel24.Visible = False
            Panel25.Visible = False
            Panel27.Visible = False
            Panel31.Visible = False
        ElseIf order_type = "Rate Contract" Then
        End If
        conn.Open()
        Dim mcz As New SqlCommand
        mcz.CommandText = "select * from ORDER_DETAILS WHERE SO_NO='" & inv & "'"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox82.Text = dr.Item("SO_NO")
            Delvdate6.Text = dr.Item("SO_DATE")
            TextBox3.Text = dr.Item("SO_ACTUAL")
            Delvdate1.Text = dr.Item("SO_ACTUAL_DATE")
            TextBox4.Text = dr.Item("QUOT_NO")
            Delvdate2.Text = dr.Item("QUOT_DATE")
            TextBox6.Text = dr.Item("LOI_NAME")
            Delvdate3.Text = dr.Item("LOI_DATE")
            TextBox54.Text = dr.Item("INQUARY_NO")
            TextBox55.Text = dr.Item("INQUARY_DATE")
            TextBox56.Text = dr.Item("INDENT_NO")
            TextBox57.Text = dr.Item("INDENT_DATE")
            TextBox8.Text = dr.Item("CURRENCY")
            Delvdate4.Text = dr.Item("CURRENCY_VALUE")
            DropDownList20.Text = dr.Item("ORDER_TO")
            TextBox48.Text = dr.Item("PURGRP_FILE")
            payterm.Text = dr.Item("PAYMENT_TERM")
            paymode.Text = dr.Item("PAYMENT_MODE")
            ldapplicable.Text = dr.Item("LD")
            pay_agency.Text = dr.Item("PAYING_AGENCY")
            delvterm.Text = dr.Item("DELIVERY_TERM")
            despatch_mode.Text = dr.Item("MODE_OF_DESPATCH")
            destinatationTextBox.Text = dr.Item("DESTINATION")
            freightterm.Text = dr.Item("FREIGHT_TERM")
            tax_on_freightTextBox.Text = dr.Item("S_TAX_ON_FREIGHT")
            insurance.Text = dr.Item("INSU_TERM")
            insurancetype.Text = dr.Item("INSU_TYPE")
            insupercent_TextBox.Text = dr.Item("INSU_TAX")
            misccharg_ComboBox.Text = dr.Item("MISC_CHARGE")
            misc_tax_ComboBox4.Text = dr.Item("ST_MISC")
            inspeterm_ComboBox.Text = dr.Item("INSP_TERM")
            third_party_insp.Text = dr.Item("THIRD_PARTY_INSP")
            insp_test_ComboBox4.Text = dr.Item("INSP_TEST_QULITY")
            pvc_ComboBox.Text = dr.Item("PVC_CLAUSE")
            bonus_ComboBox.Text = dr.Item("B_P_CLAUSE")
            performance_ComboBox0.Text = dr.Item("PERFORMANCE_CLOUSE")
            per_gurrenty_ComboBox1.Text = dr.Item("PERFORMANCE_GURRENTEE")
            sd_ComboBox2.Text = dr.Item("SD_DIPOSITE")
            sd_TextBox46.Text = dr.Item("SD_AMOUNT")
            sp_gur_ComboBox3.Text = dr.Item("SPECIAL_GURRENTEE")
            match_ComboBox2.Text = dr.Item("MATCHING_PARTS")
            quantity_ComboBox.Text = dr.Item("QTY_VERIATATION")
            medicine_ComboBox0.Text = dr.Item("MEDECINE_TERM")
            spl_del_ComboBox1.Text = dr.Item("SPECIAL_DEL_PACK_TERM")
            doc_sub_m_supl_TextBox49.Text = dr.Item("DOC_SUPPLY")
            doc_bill_pay_TextBox50.Text = dr.Item("DOC_PAYMENT")
            inv_party_TextBox51.Text = dr.Item("INVOICING_PARTY")
            general_term_TextBox52.Text = dr.Item("GENERAL_TERM")
            note_TextBox53.Text = dr.Item("NOTE")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc4 As New SqlCommand
        mcz.CommandText = "SELECT (SUPL.SUPL_ID + ' , ' + SUPL.SUPL_NAME ) AS SUPL_ID  FROM SUPL JOIN ORDER_DETAILS ON SUPL .SUPL_ID =ORDER_DETAILS .PARTY_CODE  WHERE ORDER_DETAILS.SO_NO='" & inv & "'"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox12.Text = dr.Item("SUPL_ID")
            TextBox7.Text = dr.Item("SUPL_ID")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc5 As New SqlCommand
        mcz.CommandText = "SELECT (SUPL.SUPL_ID + ' , ' + SUPL.SUPL_NAME ) AS SUPL_ID  FROM SUPL JOIN ORDER_DETAILS ON SUPL .SUPL_ID =ORDER_DETAILS .PARTY_CODE  WHERE ORDER_DETAILS.SO_NO='" & inv & "'"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            TextBox81.Text = dr.Item("SUPL_ID")
            TextBox9.Text = dr.Item("SUPL_ID")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc6 As New SqlCommand
        mcz.CommandText = "SELECT term_desc  FROM order_terms WHERE so_no ='" & inv & "' AND term_slno =10"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            insp_test_TextBox25.Text = dr.Item("term_desc")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc7 As New SqlCommand
        mcz.CommandText = "SELECT term_desc  FROM order_terms WHERE so_no ='" & inv & "' AND term_slno =11"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            pvc_TextBox26.Text = dr.Item("term_desc")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc8 As New SqlCommand
        mcz.CommandText = "SELECT term_desc  FROM order_terms WHERE so_no ='" & inv & "' AND term_slno =12"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            bonus_TextBox27.Text = dr.Item("term_desc")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc9 As New SqlCommand
        mcz.CommandText = "SELECT term_desc  FROM order_terms WHERE so_no ='" & inv & "' AND term_slno =13"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            performance_TextBox28.Text = dr.Item("term_desc")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc10 As New SqlCommand
        mcz.CommandText = "SELECT term_desc  FROM order_terms WHERE so_no ='" & inv & "' AND term_slno =14"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            per_gurrenty_TextBox29.Text = dr.Item("term_desc")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc11 As New SqlCommand
        mcz.CommandText = "SELECT term_desc  FROM order_terms WHERE so_no ='" & inv & "' AND term_slno =16"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            sp_gur_TextBox31.Text = dr.Item("term_desc")
            dr.Close()
        End If
        conn.Close()

        conn.Open()
        Dim mc12 As New SqlCommand
        mcz.CommandText = "SELECT term_desc  FROM order_terms WHERE so_no ='" & inv & "' AND term_slno =20"
        mcz.Connection = conn
        dr = mcz.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            spl_del_TextBox35.Text = dr.Item("term_desc")
            dr.Close()
        End If
        conn.Close()

       









    End Sub

    Protected Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If TextBox3.Text = "" Then
            TextBox3.Focus()
            Return
        ElseIf IsDate(Delvdate1.Text) = False Then
            Delvdate1.Focus()
            Return
        ElseIf TextBox4.Text = "" Then
            TextBox4.Focus()
            Return
        ElseIf Delvdate2.Text = "" Then
            Delvdate2.Focus()
            Return
        ElseIf TextBox6.Text = "" Then
            TextBox6.Focus()
            Return
        ElseIf Delvdate3.Text = "" Then
            Delvdate3.Focus()
            Return
        ElseIf TextBox54.Text = "" Then
            TextBox54.Focus()
            Return
        ElseIf TextBox55.Text = "" Then
            TextBox55.Focus()
            Return
        ElseIf TextBox56.Text = "" Then
            TextBox56.Focus()
            Return
        ElseIf TextBox57.Text = "" Then
            TextBox57.Focus()
            Return
        ElseIf TextBox8.Text = "" Then
            TextBox8.Focus()
            Return
        ElseIf Delvdate4.Text = "" Then
            Delvdate4.Focus()
            Return

        ElseIf DropDownList20.SelectedValue = "Select" Then
            DropDownList20.Focus()
            Return
        ElseIf delvterm.SelectedValue = "Select" Then
            delvterm.Focus()
            Return
        ElseIf destinatationTextBox.Text = "" Then
            destinatationTextBox.Focus()
            Return
        ElseIf insurance.SelectedValue = "Select" Then
            insurance.Focus()
            Return
        ElseIf payterm.SelectedValue = "Select" Then
            payterm.Focus()
            Return
        ElseIf IsNumeric(tax_on_freightTextBox.Text) = False Then
            tax_on_freightTextBox.Focus()
            Return
        ElseIf IsNumeric(misc_tax_ComboBox4.Text) = False Then
            misc_tax_ComboBox4.Focus()
            Return
        ElseIf IsNumeric(sd_TextBox46.Text) = False Then
            sd_TextBox46.Focus()
            Return
        End If


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim PARTY_CODE, PARTY_NAME, CON_CODE, CON_NAME As String
                PARTY_CODE = ""
                PARTY_NAME = ""
                CON_CODE = ""
                CON_NAME = ""
                If Panel46.Visible = True Then
                    If TextBox12.Text.IndexOf(",") <> 6 Then
                        TextBox12.Text = ""
                        TextBox12.Focus()
                        Return
                    ElseIf TextBox81.Text.IndexOf(",") <> 6 Then
                        TextBox81.Text = ""
                        TextBox81.Focus()
                        Return
                    End If
                    PARTY_CODE = TextBox12.Text.Substring(0, TextBox12.Text.IndexOf(",") - 1)
                    CON_CODE = TextBox81.Text.Substring(0, TextBox81.Text.IndexOf(",") - 1)
                ElseIf Panel47.Visible = True Then
                    If TextBox7.Text.IndexOf(",") <> 6 Then
                        TextBox7.Text = ""
                        TextBox7.Focus()
                        Return
                    ElseIf TextBox9.Text.IndexOf(",") <> 6 Then
                        TextBox9.Text = ""
                        TextBox9.Focus()
                        Return
                    End If
                    PARTY_CODE = TextBox7.Text.Substring(0, TextBox7.Text.IndexOf(",") - 1)
                    CON_CODE = TextBox9.Text.Substring(0, TextBox9.Text.IndexOf(",") - 1)
                End If
                ''SALE ORDER SAVE
                'conn.Open()
                Dim QUARY1 As String = "UPDATE ORDER_DETAILS SET " &
            "SO_DATE= @SO_DATE ,SO_ACTUAL= @SO_ACTUAL ,SO_ACTUAL_DATE= @SO_ACTUAL_DATE  ,ORDER_TO= @ORDER_TO ,QUOT_NO= @QUOT_NO , " &
            "QUOT_DATE= @QUOT_DATE ,LOI_NAME= @LOI_NAME ,LOI_DATE= @LOI_DATE ,INDENT_NO= @INDENT_NO ,INDENT_DATE= @INDENT_DATE , " &
            "INQUARY_NO= @INQUARY_NO ,INQUARY_DATE= @INQUARY_DATE ,CURRENCY= @CURRENCY ,CURRENCY_VALUE= @CURRENCY_VALUE , " &
            "PARTY_CODE= @PARTY_CODE ,CONSIGN_CODE= @CONSIGN_CODE ,PURGRP_FILE= @PURGRP_FILE ,PAYMENT_TERM= @PAYMENT_TERM , " &
            "PAYMENT_MODE= @PAYMENT_MODE ,PAYING_AGENCY= @PAYING_AGENCY ,LD= @LD ,DELIVERY_TERM= @DELIVERY_TERM , " &
            "MODE_OF_DESPATCH= @MODE_OF_DESPATCH ,DESTINATION= @DESTINATION ,FREIGHT_TERM= @FREIGHT_TERM ,S_TAX_ON_FREIGHT= @S_TAX_ON_FREIGHT , " &
            "INSU_TERM= @INSU_TERM ,INSU_TAX= @INSU_TAX ,INSU_TYPE= @INSU_TYPE ,MISC_CHARGE= @MISC_CHARGE ,ST_MISC= @ST_MISC , " &
            "INSP_TERM= @INSP_TERM ,THIRD_PARTY_INSP= @THIRD_PARTY_INSP ,INSP_TEST_QULITY= @INSP_TEST_QULITY ,PVC_CLAUSE= @PVC_CLAUSE , " &
            "B_P_CLAUSE= @B_P_CLAUSE ,PERFORMANCE_CLOUSE= @PERFORMANCE_CLOUSE ,PERFORMANCE_GURRENTEE= @PERFORMANCE_GURRENTEE , " &
            "SD_DIPOSITE= @SD_DIPOSITE ,SD_AMOUNT= @SD_AMOUNT ,SPECIAL_GURRENTEE= @SPECIAL_GURRENTEE ,MATCHING_PARTS= @MATCHING_PARTS , " &
            "QTY_VERIATATION= @QTY_VERIATATION ,MEDECINE_TERM= @MEDECINE_TERM ,SPECIAL_DEL_PACK_TERM= @SPECIAL_DEL_PACK_TERM , " &
             "DOC_SUPPLY= @DOC_SUPPLY ,DOC_PAYMENT= @DOC_PAYMENT ,INVOICING_PARTY= @INVOICING_PARTY ,GENERAL_TERM= @GENERAL_TERM , " &
             "NOTE= @NOTE ,EMP_ID= @EMP_ID ,SO_STATUS= @SO_STATUS ,SO_TYPE= @SO_TYPE " &
             "WHERE SO_NO='" & TextBox82.Text & "' "

                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(Delvdate6.Text), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@SO_ACTUAL", TextBox3.Text)
                cmd1.Parameters.AddWithValue("@SO_ACTUAL_DATE", Date.ParseExact(CDate(Delvdate1.Text), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@ORDER_TO", DropDownList20.SelectedValue)
                cmd1.Parameters.AddWithValue("@QUOT_NO", TextBox4.Text)
                cmd1.Parameters.AddWithValue("@QUOT_DATE", Delvdate2.Text)
                cmd1.Parameters.AddWithValue("@LOI_NAME", TextBox6.Text)
                cmd1.Parameters.AddWithValue("@LOI_DATE", Delvdate3.Text)
                cmd1.Parameters.AddWithValue("@INDENT_NO", TextBox56.Text)
                cmd1.Parameters.AddWithValue("@INDENT_DATE", TextBox57.Text)
                cmd1.Parameters.AddWithValue("@INQUARY_NO", TextBox54.Text)
                cmd1.Parameters.AddWithValue("@INQUARY_DATE", TextBox55.Text)
                cmd1.Parameters.AddWithValue("@CURRENCY", TextBox8.Text)
                cmd1.Parameters.AddWithValue("@CURRENCY_VALUE", Delvdate4.Text)
                cmd1.Parameters.AddWithValue("@PARTY_CODE", PARTY_CODE)
                cmd1.Parameters.AddWithValue("@CONSIGN_CODE", CON_CODE)
                cmd1.Parameters.AddWithValue("@PURGRP_FILE", TextBox48.Text)
                cmd1.Parameters.AddWithValue("@PAYMENT_TERM", payterm.Text)
                cmd1.Parameters.AddWithValue("@PAYMENT_MODE", paymode.Text)
                cmd1.Parameters.AddWithValue("@PAYING_AGENCY", pay_agency.Text)
                cmd1.Parameters.AddWithValue("@LD", ldapplicable.Text)
                cmd1.Parameters.AddWithValue("@DELIVERY_TERM", delvterm.Text)
                cmd1.Parameters.AddWithValue("@MODE_OF_DESPATCH", despatch_mode.Text)
                cmd1.Parameters.AddWithValue("@DESTINATION", destinatationTextBox.Text)
                cmd1.Parameters.AddWithValue("@FREIGHT_TERM", freightterm.Text)
                cmd1.Parameters.AddWithValue("@S_TAX_ON_FREIGHT", tax_on_freightTextBox.Text)
                cmd1.Parameters.AddWithValue("@INSU_TERM", insurance.Text)
                cmd1.Parameters.AddWithValue("@INSU_TAX", insupercent_TextBox.Text)
                cmd1.Parameters.AddWithValue("@INSU_TYPE", insurancetype.Text)
                cmd1.Parameters.AddWithValue("@MISC_CHARGE", misccharg_ComboBox.Text)
                cmd1.Parameters.AddWithValue("@ST_MISC", misc_tax_ComboBox4.Text)
                cmd1.Parameters.AddWithValue("@INSP_TERM", inspeterm_ComboBox.Text)
                cmd1.Parameters.AddWithValue("@THIRD_PARTY_INSP", third_party_insp.Text)
                cmd1.Parameters.AddWithValue("@INSP_TEST_QULITY", insp_test_ComboBox4.Text)
                cmd1.Parameters.AddWithValue("@PVC_CLAUSE", pvc_ComboBox.Text)
                cmd1.Parameters.AddWithValue("@B_P_CLAUSE", bonus_ComboBox.Text)
                cmd1.Parameters.AddWithValue("@PERFORMANCE_CLOUSE", performance_ComboBox0.Text)
                cmd1.Parameters.AddWithValue("@PERFORMANCE_GURRENTEE", per_gurrenty_ComboBox1.Text)
                cmd1.Parameters.AddWithValue("@SD_DIPOSITE", sd_ComboBox2.Text)
                cmd1.Parameters.AddWithValue("@SD_AMOUNT", sd_TextBox46.Text)
                cmd1.Parameters.AddWithValue("@SPECIAL_GURRENTEE", sp_gur_ComboBox3.Text)
                cmd1.Parameters.AddWithValue("@MATCHING_PARTS", match_ComboBox2.Text)
                cmd1.Parameters.AddWithValue("@QTY_VERIATATION", quantity_ComboBox.Text)
                cmd1.Parameters.AddWithValue("@MEDECINE_TERM", medicine_ComboBox0.Text)
                cmd1.Parameters.AddWithValue("@SPECIAL_DEL_PACK_TERM", spl_del_ComboBox1.Text)
                cmd1.Parameters.AddWithValue("@DOC_SUPPLY", doc_sub_m_supl_TextBox49.Text)
                cmd1.Parameters.AddWithValue("@DOC_PAYMENT", doc_bill_pay_TextBox50.Text)
                cmd1.Parameters.AddWithValue("@INVOICING_PARTY", inv_party_TextBox51.Text)
                cmd1.Parameters.AddWithValue("@GENERAL_TERM", general_term_TextBox52.Text)
                cmd1.Parameters.AddWithValue("@NOTE", note_TextBox53.Text)
                cmd1.Parameters.AddWithValue("@EMP_ID", Session("userName"))
                cmd1.Parameters.AddWithValue("@SO_TYPE", "")
                cmd1.Parameters.AddWithValue("@SO_STATUS", "DRAFT")
                cmd1.ExecuteReader()
                cmd1.Dispose()
                'conn.Close()
                ''save po terms

                If insp_test_TextBox25.Text <> "" Then
                    save_terms(TextBox82.Text, 10, Label256.Text, insp_test_TextBox25.Text)
                End If
                If pvc_TextBox26.Text <> "" Then
                    save_terms(TextBox82.Text, 11, Label20.Text, pvc_TextBox26.Text)
                End If
                If bonus_TextBox27.Text <> "" Then
                    save_terms(TextBox82.Text, 12, Label19.Text, bonus_TextBox27.Text)
                End If
                If performance_TextBox28.Text <> "" Then
                    save_terms(TextBox82.Text, 13, Label257.Text, performance_TextBox28.Text)
                End If
                If per_gurrenty_TextBox29.Text <> "" Then
                    save_terms(TextBox82.Text, 14, Label258.Text, per_gurrenty_TextBox29.Text)
                End If

                If sp_gur_TextBox31.Text <> "" Then
                    save_terms(TextBox82.Text, 16, Label260.Text, sp_gur_TextBox31.Text)
                End If
                If spl_del_TextBox35.Text <> "" Then
                    save_terms(TextBox82.Text, 20, spl_del_Label263.Text, spl_del_TextBox35.Text)
                End If

                myTrans.Commit()

                Label6.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label6.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using



    End Sub
    Protected Sub save_terms(so_no As String, term_slno As Integer, term_type As String, term_desc As String)
        ''DELETE FROM ORDER_TERM
        'conn.Open()
        mycommand = New SqlCommand("DELETE FROM order_terms WHERE SO_NO ='" & so_no & "' AND term_slno=" & term_slno, conn_trans, myTrans)
        mycommand.ExecuteNonQuery()
        'conn.Close()
        ''SAVE ORDER_TERM
        'conn.Open()
        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into order_terms(so_no,term_slno,term_type,term_desc) values (@so_no,@term_slno,@term_type,@term_desc)"
        Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
        cmd1.Parameters.AddWithValue("@so_no", so_no)
        cmd1.Parameters.AddWithValue("@term_slno", term_slno)
        cmd1.Parameters.AddWithValue("@term_type", term_type)
        cmd1.Parameters.AddWithValue("@term_desc", term_desc)
        cmd1.ExecuteReader()
        'cmd1.Dispose()
        'conn.Close()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        conn.Open()
        Dim mc12 As New SqlCommand
        mc12.CommandText = "SELECT (SO_NO + ' , ' + SO_ACTUAL ) AS SO_NO FROM ORDER_DETAILS WHERE SO_NO ='" & TextBox82.Text & "'"
        mc12.Connection = conn
        dr = mc12.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            Session("val") = dr.Item("SO_NO")
            dr.Close()
        End If
        conn.Close()
        Response.Redirect("/purchase/add_order.aspx")
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim order_type As String = ""
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select ORDER_TYPE  from ORDER_DETAILS WHERE SO_NO = '" & TextBox82.Text & "'"
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    order_type = dr.Item("ORDER_TYPE")

                    dr.Close()
                End If
                conn.Close()
                If order_type = "Rate Contract" Then
                    Label439.Text = "This is a rate contract can't be submited"
                    Return
                End If
                ''CHECK MATERIAL AVAILABILITY

                count = 0
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select PO_NO from PO_ORD_MAT WHERE PO_NO ='" & TextBox82.Text & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    Label439.Text = "Please add material first"
                    Return
                Else
                    ''update order details
                    'conn.Open()
                    Dim cmd2 As New SqlCommand("update ORDER_DETAILS set SO_STATUS='ACTIVE' where SO_NO ='" & TextBox82.Text & "'", conn_trans, myTrans)
                    cmd2.ExecuteReader()
                    cmd2.Dispose()
                    'conn.Close()
                    Label439.Text = "Order Submited"
                End If

                myTrans.Commit()

                Label6.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label6.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub
End Class