Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class new_order
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
            MultiView1.ActiveViewIndex = 0
        End If

        Delvdate1_CalendarExtender.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("purchaseAccess")) Or Session("purchaseAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub
    Protected Sub panel_visible()
        Panel10.Visible = True
        Panel12.Visible = True
        Panel13.Visible = True
        Panel15.Visible = True
        Panel16.Visible = True
        Panel17.Visible = True
        Panel18.Visible = True
        Panel19.Visible = True
        Panel20.Visible = True
        Panel21.Visible = True
        Panel22.Visible = True
        Panel23.Visible = True
        Panel24.Visible = True
        Panel25.Visible = True
        Panel26.Visible = True
        Panel27.Visible = True
        Panel28.Visible = True
        Panel29.Visible = True
        Panel30.Visible = True
        Panel31.Visible = True
    End Sub
    Protected Sub DropDownList24_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList24.SelectedIndexChanged
        If DropDownList24.SelectedValue = "Select" Then
            DropDownList24.Focus()
            Return
        ElseIf DropDownList24.SelectedValue = "Purchase Order" Then
            DropDownList25.Items.Clear()
            DropDownList25.Items.Add("Select")
            DropDownList25.Items.Add("STORE MATERIAL")
            DropDownList25.Items.Add("STORE MATERIAL(IMP)")
            DropDownList25.Items.Add("RAW MATERIAL")
            DropDownList25.Items.Add("RAW MATERIAL(IMP)")
            DropDownList25.Items.Add("COAL PURCHASE")
            DropDownList25.Items.Add("OUTSOURCED ITEMS")
            DropDownList25.Items.Add("OUTSOURCED Items(IPT)")
            DropDownList25.Items.Add("OUTSOURCED ITEMS(FOREIGN)")
            Label340.Visible = False
            DropDownList23.Visible = False
            DropDownList23.SelectedValue = "Direct"
        ElseIf DropDownList24.SelectedValue = "Rate Contract" Then
            DropDownList25.Items.Clear()
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select * from s_tax_liability order by taxable_service", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList25.Items.Clear()
            DropDownList25.DataSource = dt
            DropDownList25.DataValueField = "taxable_service"
            DropDownList25.DataBind()
            DropDownList25.Items.Add("Select")
            DropDownList25.Items.Add("STORE MATERIAL")
            DropDownList25.Items.Add("RAW MATERIAL")
            Label340.Visible = False
            DropDownList23.Visible = False
            DropDownList23.SelectedValue = "Direct"
            DropDownList25.SelectedValue = "Select"
        ElseIf DropDownList24.SelectedValue = "Sale Order" Then
            DropDownList25.Items.Clear()
            DropDownList25.Items.Add("Select")
            DropDownList25.Items.Add("STORE MATERIAL")
            DropDownList25.Items.Add("RAW MATERIAL")
            DropDownList25.Items.Add("MISCELLANEOUS")
            DropDownList25.Items.Add("FINISH GOODS")
            DropDownList25.Items.Add("OUTSOURCED ITEMS")
            Label340.Visible = True
            DropDownList23.Visible = True
            DropDownList23.SelectedValue = "Select"
        ElseIf DropDownList24.SelectedValue = "Work Order" Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("select * from s_tax_liability order by taxable_service", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList25.Items.Clear()
            DropDownList25.DataSource = dt
            DropDownList25.DataValueField = "taxable_service"
            DropDownList25.DataBind()
            DropDownList25.Items.Add("Select")
            DropDownList25.SelectedValue = "Select"

            Label340.Visible = False
            DropDownList23.Visible = False
            DropDownList23.SelectedValue = "Direct"
        End If
    End Sub


    Public Sub btnSample_Click(sender As Object, e As EventArgs)
        If TextBox7.Text.Contains("SAIL/") Then
            payterm.Items.Clear()
            payterm.Items.Insert(0, "Book Adjustment")

            paymode.Items.Clear()
            paymode.Items.Insert(0, "Book Adjustment")

            DropDownList20.Items.Clear()
            DropDownList20.Items.Insert(0, "I.P.T.")

        Else
            payterm.Items.Clear()
            payterm.Items.Insert(0, "E.Payment")
            payterm.Items.Insert(1, "Cheque")
            payterm.Items.Insert(2, "Demand Draft")

            DropDownList20.Items.Clear()
            DropDownList20.Items.Insert(0, "Other")

            If DropDownList24.SelectedValue = "Purchase Order" Then
                paymode.Items.Clear()
                paymode.Items.Insert(0, "100% Against GRN Within 30 Days")
                paymode.Items.Insert(1, "90-10% Against GRN Within 30 Days")
                paymode.Items.Insert(2, "Advance Payment")

            ElseIf DropDownList24.SelectedValue = "Sale Order" Then
                paymode.Items.Clear()
                paymode.Items.Insert(0, "ADVANCE")
                paymode.Items.Insert(1, "BUY BACK")
                paymode.Items.Insert(2, "RETURNABLE BASIS")
            ElseIf DropDownList24.SelectedValue = "Work Order" Then
                paymode.Items.Clear()
                paymode.Items.Insert(0, "Against Running Bill")
                paymode.Items.Insert(1, "Advance Payment")
            ElseIf DropDownList24.SelectedValue = "Rate Contract" Then

                paymode.Items.Clear()
                paymode.Items.Insert(0, "100% Against GRN Within 30 Days")
                paymode.Items.Insert(1, "90-10% Against GRN Within 30 Days")
                paymode.Items.Insert(2, "Against Running Bill")
                paymode.Items.Insert(3, "Advance Payment")
            End If

        End If
    End Sub

    Public Sub chkPartyCodePO_Click(sender As Object, e As EventArgs)
        If TextBox12.Text.Contains("SAIL/") Then
            payterm.Items.Clear()
            payterm.Items.Insert(0, "Book Adjustment")

            paymode.Items.Clear()
            paymode.Items.Insert(0, "Book Adjustment")

            DropDownList20.Items.Clear()
            DropDownList20.Items.Insert(0, "I.P.T.")

        Else
            payterm.Items.Clear()
            payterm.Items.Insert(0, "E.Payment")
            payterm.Items.Insert(1, "Cheque")
            payterm.Items.Insert(2, "Demand Draft")

            DropDownList20.Items.Clear()
            DropDownList20.Items.Insert(0, "Other")

            If DropDownList24.SelectedValue = "Purchase Order" Then
                paymode.Items.Clear()
                paymode.Items.Insert(0, "100% Against GRN Within 30 Days")
                paymode.Items.Insert(1, "90-10% Against GRN Within 30 Days")
                paymode.Items.Insert(2, "Advance Payment")

            ElseIf DropDownList24.SelectedValue = "Sale Order" Then
                paymode.Items.Clear()
                paymode.Items.Insert(0, "ADVANCE")
                paymode.Items.Insert(1, "BUY BACK")
                paymode.Items.Insert(2, "RETURNABLE BASIS")
            ElseIf DropDownList24.SelectedValue = "Work Order" Then
                paymode.Items.Clear()
                paymode.Items.Insert(0, "Against Running Bill")
                paymode.Items.Insert(1, "Advance Payment")
            ElseIf DropDownList24.SelectedValue = "Rate Contract" Then

                paymode.Items.Clear()
                paymode.Items.Insert(0, "100% Against GRN Within 30 Days")
                paymode.Items.Insert(1, "90-10% Against GRN Within 30 Days")
                paymode.Items.Insert(2, "Against Running Bill")
                paymode.Items.Insert(3, "Advance Payment")
            End If

        End If
    End Sub

    Protected Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        If DropDownList23.SelectedValue = "Select" Then
            DropDownList23.Focus()
            Return
        ElseIf DropDownList24.SelectedValue = "Select" Then
            DropDownList24.Focus()
            Return
        ElseIf DropDownList25.SelectedValue = "Select" Then
            DropDownList25.Focus()
            Return
        End If
        If DropDownList24.SelectedValue = "Purchase Order" Then
            Label270.Text = "PURCHASE ORDER FOR " & DropDownList25.SelectedValue.ToUpper
            Panel47.Visible = False
            Panel46.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("100% Against GRN Within 30 Days")
            paymode.Items.Add("90-10% Against GRN Within 30 Days")
            paymode.Items.Add("Advance Payment")
            paymode.Items.Add("Book Adjustment")
        ElseIf DropDownList24.SelectedValue = "Sale Order" Then
            Label270.Text = "SALE ORDER FOR " & DropDownList25.SelectedValue.ToUpper
            Panel46.Visible = False
            Panel47.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("Book Adjustment")
            paymode.Items.Insert(0, "ADVANCE")
            paymode.Items.Insert(1, "BUY BACK")
            paymode.Items.Insert(2, "RETURNABLE BASIS")
        ElseIf DropDownList24.SelectedValue = "Work Order" Then
            Label270.Text = "WORK ORDER FOR " & DropDownList25.SelectedValue.ToUpper
            Panel47.Visible = False
            Panel46.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("Against Running Bill")
            paymode.Items.Add("Advance Payment")
        ElseIf DropDownList24.SelectedValue = "Rate Contract" Then
            Label270.Text = "RATE CONTRACT FOR " & DropDownList25.SelectedValue.ToUpper
            Panel47.Visible = False
            Panel46.Visible = True
            paymode.Items.Clear()
            paymode.Items.Add("100% Against GRN Within 30 Days")
            paymode.Items.Add("90-10% Against GRN Within 30 Days")
            paymode.Items.Add("Against Running Bill")
            paymode.Items.Add("Advance Payment")
        End If
        panel_visible()
        If DropDownList24.SelectedValue = "Purchase Order" Then
            Panel16.Visible = False
            Panel21.Visible = False
            Panel25.Visible = False
            Panel27.Visible = False
            Panel31.Visible = False
            Label251.Text = "Origin Station"
        ElseIf DropDownList24.SelectedValue = "Sale Order" Then
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
        ElseIf DropDownList24.SelectedValue = "Work Order" Then
            Panel12.Visible = False
            Panel13.Visible = False
            Panel15.Visible = True
            Panel16.Visible = False
            Panel17.Visible = False
            Panel18.Visible = False
            Panel21.Visible = False
            Panel22.Visible = False
            Panel24.Visible = False
            Panel25.Visible = False
            Panel27.Visible = False
            Panel31.Visible = False
        ElseIf DropDownList24.SelectedValue = "Rate Contract" Then
        End If

        TextBox3.Focus()
        Delvdate6.Text = Today.Date
        TextBox82.Text = ""
        TextBox3.Text = ""
        Delvdate1.Text = ""
        TextBox12.Text = ""
        TextBox81.Text = ""
        TextBox7.Text = ""
        TextBox9.Text = ""
        destinatationTextBox.Text = ""
        MultiView1.ActiveViewIndex = 1
    End Sub

    Protected Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click

    End Sub

    Protected Sub insurance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles insurance.SelectedIndexChanged
        If insurance.SelectedValue = "Select" Then
            insurance.Focus()
            Return
        ElseIf insurance.SelectedValue = "Party Cost" Or insurance.SelectedValue = "Not Applicable" Then
            insupercent_TextBox.ReadOnly = True
            insupercent_TextBox.Text = "0.00"
            insupercent_TextBox.BackColor = Drawing.Color.SeaGreen
            insupercent_TextBox.Focus()
        Else

            insupercent_TextBox.ReadOnly = False
            insupercent_TextBox.Text = "0.00"
            insupercent_TextBox.BackColor = Drawing.Color.White
            insupercent_TextBox.Focus()
        End If
    End Sub

    Protected Sub misccharg_ComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles misccharg_ComboBox.SelectedIndexChanged
        If misccharg_ComboBox.SelectedValue = "Not Applicable" Then
            misc_tax_ComboBox4.ReadOnly = True
            misc_tax_ComboBox4.Text = "0.00"
            misc_tax_ComboBox4.BackColor = Drawing.Color.SeaGreen
            misc_tax_ComboBox4.Focus()
        Else
            misc_tax_ComboBox4.ReadOnly = False
            misc_tax_ComboBox4.Text = "0.00"
            misc_tax_ComboBox4.BackColor = Drawing.Color.White
            misc_tax_ComboBox4.Focus()
        End If
    End Sub

    Protected Sub sd_ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles sd_ComboBox2.SelectedIndexChanged
        If sd_ComboBox2.SelectedValue = "CASH" Then
            sd_TextBox46.ReadOnly = False
            sd_TextBox46.Focus()
            sd_TextBox46.BackColor = Drawing.Color.White
        ElseIf sd_ComboBox2.SelectedValue = "BANK GUARANTEE" Then
            sd_TextBox46.ReadOnly = True
            sd_ComboBox2.Focus()
            sd_TextBox46.BackColor = Drawing.Color.SeaGreen
        ElseIf sd_ComboBox2.SelectedValue = "Not Applicable" Then
            sd_TextBox46.ReadOnly = True
            sd_ComboBox2.Focus()
            sd_TextBox46.BackColor = Drawing.Color.SeaGreen
        End If
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
        ElseIf destinatationTextBox.Text = "" Then
            destinatationTextBox.Focus()
            Return
        ElseIf insurance.SelectedValue = "Select" Then
            insurance.Focus()
            Return
        ElseIf payterm.SelectedValue = "Select" Then
            payterm.Focus()
            Return
        ElseIf txt_ITC_Status.SelectedValue = "Select" Then
            txt_ITC_Status.Focus()
            Return
        ElseIf IsNumeric(misc_tax_ComboBox4.Text) = False Then
            misc_tax_ComboBox4.Focus()
            Return
        ElseIf IsNumeric(sd_TextBox46.Text) = False Then
            sd_TextBox46.Focus()
            Return
        End If

        If (Panel18.Visible = True) Then
            If TextBox1.Text = "" Then
                TextBox1.Focus()
                Return
            End If
        Else
            TextBox1.Text = "0.00"
        End If

        If (Panel13.Visible = True) Then
            If delvterm.SelectedValue = "Select" Then
                delvterm.Focus()
                Return

            End If

        End If


        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim STR1 As String = ""
                If Today.Date.Month > 3 Then
                    STR1 = Today.Date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = STR1 & (STR1 + 1)
                ElseIf Today.Date.Month <= 3 Then
                    STR1 = Today.Date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = (STR1 - 1) & STR1
                End If
                Dim ORD_NO1 As String = "'"
                Dim ORD_NO2 As String = ""
                If DropDownList24.SelectedValue = "Purchase Order" Then
                    ORD_NO1 = "P"
                ElseIf DropDownList24.SelectedValue = "Sale Order" Then
                    ORD_NO1 = "S"
                ElseIf DropDownList24.SelectedValue = "Work Order" Then
                    ORD_NO1 = "W"
                ElseIf DropDownList24.SelectedValue = "Rate Contract" Then
                    ORD_NO1 = "R"
                End If

                If DropDownList25.SelectedValue = "STORE MATERIAL" Then
                    ORD_NO2 = "01"
                ElseIf DropDownList25.SelectedValue = "STORE MATERIAL(IMP)" Then
                    ORD_NO2 = "01"
                ElseIf DropDownList25.SelectedValue = "RAW MATERIAL" Then
                    ORD_NO2 = "02"
                ElseIf DropDownList25.SelectedValue = "RAW MATERIAL(IMP)" Then
                    ORD_NO2 = "02"
                ElseIf DropDownList25.SelectedValue = "COAL PURCHASE" Then
                    ORD_NO2 = "02"
                ElseIf DropDownList25.SelectedValue = "RATE CONTRACT" Then
                    ORD_NO2 = "03"
                ElseIf DropDownList25.SelectedValue = "MISCELLANEOUS" Then
                    ORD_NO2 = "04"
                ElseIf DropDownList25.SelectedValue = "FINISH GOODS" Then
                    ORD_NO2 = "05"
                Else
                    ORD_NO2 = "06"
                End If


                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select SO_NO from ORDER_DETAILS WHERE SO_NO LIKE '" & ORD_NO1 + ORD_NO2 & "%' AND FINANCE_YEAR='" & STR1 & "'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count = 0 Then
                    TextBox82.Text = ORD_NO1 & ORD_NO2 & STR1 & "000001"
                Else
                    str = count + 1
                    If str.Length = 1 Then
                        str = "00000" & (count + 1)
                    ElseIf str.Length = 2 Then
                        str = "0000" & (count + 1)
                    ElseIf str.Length = 3 Then
                        str = "000" & (count + 1)
                    ElseIf str.Length = 4 Then
                        str = "00" & (count + 1)
                    ElseIf str.Length = 5 Then
                        str = "0" & (count + 1)
                    End If
                    TextBox82.Text = ORD_NO1 & ORD_NO2 & STR1 & str
                End If
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
                Dim QUARY1 As String = ""
                QUARY1 = "Insert Into ORDER_DETAILS(ITC_ELIGIBLE,TOLERANCE,DESTINATION,FINANCE_YEAR,SO_NO,SO_DATE,SO_ACTUAL,SO_ACTUAL_DATE,ORDER_TYPE,ORDER_TO,DESPATCH_TYPE,PO_TYPE,QUOT_NO,QUOT_DATE,LOI_NAME,LOI_DATE,INDENT_NO,INDENT_DATE,INQUARY_NO,INQUARY_DATE,CURRENCY,CURRENCY_VALUE,PARTY_CODE,CONSIGN_CODE,PURGRP_FILE,PAYMENT_TERM,PAYMENT_MODE,PAYING_AGENCY,LD,DELIVERY_TERM,MODE_OF_DESPATCH,FREIGHT_TERM,INSU_TERM,INSU_TAX,INSU_TYPE,MISC_CHARGE,ST_MISC,INSP_TERM,THIRD_PARTY_INSP,INSP_TEST_QULITY,PVC_CLAUSE,B_P_CLAUSE,PERFORMANCE_CLOUSE,PERFORMANCE_GURRENTEE,SD_DIPOSITE,SD_AMOUNT,SPECIAL_GURRENTEE,MATCHING_PARTS,QTY_VERIATATION,MEDECINE_TERM,SPECIAL_DEL_PACK_TERM,DOC_SUPPLY,DOC_PAYMENT,INVOICING_PARTY,GENERAL_TERM,NOTE,EMP_ID,SO_STATUS,SO_TYPE)values(@ITC_ELIGIBLE,@TOLERANCE,@DESTINATION,@FINANCE_YEAR,@SO_NO,@SO_DATE,@SO_ACTUAL,@SO_ACTUAL_DATE,@ORDER_TYPE,@ORDER_TO,@DESPATCH_TYPE,@PO_TYPE,@QUOT_NO,@QUOT_DATE,@LOI_NAME,@LOI_DATE,@INDENT_NO,@INDENT_DATE,@INQUARY_NO,@INQUARY_DATE,@CURRENCY,@CURRENCY_VALUE,@PARTY_CODE,@CONSIGN_CODE,@PURGRP_FILE,@PAYMENT_TERM,@PAYMENT_MODE,@PAYING_AGENCY,@LD,@DELIVERY_TERM,@MODE_OF_DESPATCH,@FREIGHT_TERM,@INSU_TERM,@INSU_TAX,@INSU_TYPE,@MISC_CHARGE,@ST_MISC,@INSP_TERM,@THIRD_PARTY_INSP,@INSP_TEST_QULITY,@PVC_CLAUSE,@B_P_CLAUSE,@PERFORMANCE_CLOUSE,@PERFORMANCE_GURRENTEE,@SD_DIPOSITE,@SD_AMOUNT,@SPECIAL_GURRENTEE,@MATCHING_PARTS,@QTY_VERIATATION,@MEDECINE_TERM,@SPECIAL_DEL_PACK_TERM,@DOC_SUPPLY,@DOC_PAYMENT,@INVOICING_PARTY,@GENERAL_TERM,@NOTE,@EMP_ID,@SO_STATUS,@SO_TYPE)"
                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@SO_NO", TextBox82.Text)
                cmd1.Parameters.AddWithValue("@SO_DATE", Date.ParseExact(CDate(Delvdate6.Text), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@SO_ACTUAL", TextBox3.Text)
                cmd1.Parameters.AddWithValue("@SO_ACTUAL_DATE", Date.ParseExact(CDate(Delvdate1.Text), "dd-MM-yyyy", provider))
                cmd1.Parameters.AddWithValue("@ORDER_TYPE", DropDownList24.SelectedValue)
                cmd1.Parameters.AddWithValue("@ORDER_TO", DropDownList20.SelectedValue)
                cmd1.Parameters.AddWithValue("@DESPATCH_TYPE", DropDownList23.SelectedValue)
                cmd1.Parameters.AddWithValue("@PO_TYPE", DropDownList25.SelectedValue)
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
                cmd1.Parameters.AddWithValue("@ITC_ELIGIBLE", txt_ITC_Status.Text)
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
                cmd1.Parameters.AddWithValue("@FINANCE_YEAR", STR1)
                cmd1.Parameters.AddWithValue("@TOLERANCE", TextBox1.Text)
                cmd1.ExecuteReader()
                cmd1.Dispose()

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
                conn_trans.Close()
                Label351.Visible = True
                Label351.Text = "Data Saved Succsessfully"
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label351.Visible = True
                Label351.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

            conn.Close()

        End Using

    End Sub
    Protected Sub save_terms(so_no As String, term_slno As Integer, term_type As String, term_desc As String)
        conn.Open()
        Dim QUARY1 As String = ""
        QUARY1 = "Insert Into order_terms(so_no,term_slno,term_type,term_desc) values (@so_no,@term_slno,@term_type,@term_desc)"
        Dim cmd1 As New SqlCommand(QUARY1, conn)
        cmd1.Parameters.AddWithValue("@so_no", so_no)
        cmd1.Parameters.AddWithValue("@term_slno", term_slno)
        cmd1.Parameters.AddWithValue("@term_type", term_type)
        cmd1.Parameters.AddWithValue("@term_desc", term_desc)
        cmd1.ExecuteReader()
        cmd1.Dispose()
        conn.Close()
    End Sub

    Protected Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub DropDownList20_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList20.SelectedIndexChanged

    End Sub


End Class