Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Partial Class LDM
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
    Dim working_data As Date = Today.Date
    Public test As Decimal = 50
    Public test1 As Decimal = 100

    Public VAR_IPT_BUDGET_UPTO, VAR_IPT_ACTUAL_UPTO, VAR_IPT_CPLY, VAR_BONUS_PENALTY_BUDGET_UPTO, VAR_BONUS_PENALTY_ACTUAL_UPTO, VAR_BONUS_PENALTY_CPLY, VAR_IC_BUDGET_UPTO, VAR_IC_ACTUAL_UPTO, VAR_IC_CPLY, VAR_GS_BUDGET_UPTO, VAR_GS_ACTUAL_UPTO, VAR_GS_CPLY As New Decimal(0.00)
    Public VAR_OR_BUDGET_UPTO, VAR_OR_ACTUAL_UPTO, VAR_OR_CPLY, VAR_PWB_BUDGET_UPTO, VAR_PWB_ACTUAL_UPTO, VAR_PWB_CPLY, VAR_TOTAL_INCOME_BUDGET_UPTO, VAR_TOTAL_INCOME_ACTUAL_UPTO, VAR_TOTAL_INCOME_CPLY, VAR_ACR_DEP_BUDGET_UPTO, VAR_ACR_DEP_ACTUAL_UPTO, VAR_ACR_DEP_CPLY As New Decimal(0.00)
    Public VAR_RAW_MATERIAL_BUDGET_UPTO, VAR_RAW_MATERIAL_ACTUAL_UPTO, VAR_RAW_MATERIAL_CPLY, VAR_SALARY_WAGES_BUDGET_UPTO, VAR_SALARY_WAGES_ACTUAL_UPTO, VAR_SALARY_WAGES_CPLY, VAR_STORES_SPARES_BUDGET_UPTO, VAR_STORES_SPARES_ACTUAL_UPTO, VAR_STORES_SPARES_CPLY As New Decimal(0.00)
    Public VAR_POWER_FUEL_BUDGET_UPTO, VAR_POWER_FUEL_ACTUAL_UPTO, VAR_POWER_FUEL_CPLY, VAR_REPAIR_MAINTENANCE_BUDGET_UPTO, VAR_REPAIR_MAINTENANCE_ACTUAL_UPTO, VAR_REPAIR_MAINTENANCE_CPLY, VAR_FREIGHT_OUT_BUDGET_UPTO, VAR_FREIGHT_OUT_ACTUAL_UPTO, VAR_FREIGHT_OUT_CPLY, VAR_OVERHEAD_EXP_BUDGET_UPTO As New Decimal(0.00)
    Public VAR_OVERHEAD_EXP_ACTUAL_UPTO, VAR_OVERHEAD_EXP_CPLY, VAR_HO_EXP_BUDGET_UPTO, VAR_HO_EXP_ACTUAL_UPTO, VAR_HO_EXP_CPLY, VAR_IGST_IPT_BUDGET_UPTO, VAR_IGST_IPT_ACTUAL_UPTO, VAR_IGST_IPT_CPLY, VAR_GROSS_EXP_BUDGET_UPTO, VAR_GROSS_EXP_ACTUAL_UPTO, VAR_GROSS_EXP_CPLY As New Decimal(0.00)
    Public VAR_LESS_TRANS_BUDGET_UPTO, VAR_LESS_TRANS_ACTUAL_UPTO, VAR_LESS_TRANS_CPLY, VAR_NET_EXP_BUDGET_UPTO, VAR_NET_EXP_ACTUAL_UPTO, VAR_NET_EXP_CPLY, VAR_GROSS_MARGIN_BUDGET_UPTO, VAR_GROSS_MARGIN_ACTUAL_UPTO, VAR_GROSS_MARGIN_CPLY, VAR_DEPRECIATION_BUDGET_UPTO, VAR_DEPRECIATION_ACTUAL_UPTO, VAR_DEPRECIATION_CPLY As New Decimal(0.00)
    Public VAR_INTEREST_CASH_CREDIT_BUDGET_UPTO, VAR_INTEREST_CASH_CREDIT_ACTUAL_UPTO, VAR_INTEREST_CASH_CREDIT_CPLY, VAR_NET_PROFIT_BUDGET_UPTO, VAR_NET_PROFIT_ACTUAL_UPTO, VAR_NET_PROFIT_CPLY As New Decimal(0.00)
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub



    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select Year" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select Month" Then
            DropDownList2.Focus()
            Return
        End If

        Dim localDate = Date.Now
        If DropDownList1.SelectedValue < Today.Date.Year Then
            conn.Open()
            Dim MC6 As New SqlCommand
            MC6.CommandText = "select * from LDM where LDM_YEAR = '" & DropDownList1.SelectedValue & "' AND LDM_MONTH = '" & DropDownList2.SelectedValue & "'"
            MC6.Connection = conn
            dr = MC6.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                IPT_BUDGET.Text = dr.Item("IPT_BUDGET")
                IPT_ACTUAL.Text = dr.Item("IPT_ACTUAL")
                IPT_CPLY.Text = dr.Item("IPT_CPLY")
                BONUS_PENALTY_BUDGET.Text = dr.Item("BONUS_PENALTY_BUDGET")
                BONUS_PENALTY_ACTUAL.Text = dr.Item("BONUS_PENALTY_ACTUAL")
                BONUS_PENALTY_CPLY.Text = dr.Item("BONUS_PENALTY_CPLY")
                IC_BUDGET.Text = dr.Item("IC_BUDGET")
                IC_ACTUAL.Text = dr.Item("IC_ACTUAL")
                IC_CPLY.Text = dr.Item("IC_CPLY")
                GS_BUDGET.Text = dr.Item("GS_BUDGET")
                GS_ACTUAL.Text = dr.Item("GS_ACTUAL")
                GS_CPLY.Text = dr.Item("GS_CPLY")
                OR_BUDGET.Text = dr.Item("OR_BUDGET")
                OR_ACTUAL.Text = dr.Item("OR_ACTUAL")
                OR_CPLY.Text = dr.Item("OR_CPLY")
                PWB_BUDGET.Text = dr.Item("PWB_BUDGET")
                PWB_ACTUAL.Text = dr.Item("PWB_ACTUAL")
                PWB_CPLY.Text = dr.Item("PWB_CPLY")
                TOTAL_INCOME_BUDGET.Text = dr.Item("TOTAL_INCOME_BUDGET")
                TOTAL_INCOME_ACTUAL.Text = dr.Item("TOTAL_INCOME_ACTUAL")
                TOTAL_INCOME_CPLY.Text = dr.Item("TOTAL_INCOME_CPLY")
                ACR_DEP_BUDGET.Text = dr.Item("ACR_DEP_BUDGET")
                ACR_DEP_ACTUAL.Text = dr.Item("ACR_DEP_ACTUAL")
                ACR_DEP_CPLY.Text = dr.Item("ACR_DEP_CPLY")
                RAW_MATERIAL_BUDGET.Text = dr.Item("RAW_MATERIAL_BUDGET")
                RAW_MATERIAL_ACTUAL.Text = dr.Item("RAW_MATERIAL_ACTUAL")
                RAW_MATERIAL_CPLY.Text = dr.Item("RAW_MATERIAL_CPLY")
                SALARY_WAGES_BUDGET.Text = dr.Item("SALARY_WAGES_BUDGET")
                SALARY_WAGES_ACTUAL.Text = dr.Item("SALARY_WAGES_ACTUAL")
                SALARY_WAGES_CPLY.Text = dr.Item("SALARY_WAGES_CPLY")
                STORES_SPARES_BUDGET.Text = dr.Item("STORES_SPARES_BUDGET")
                STORES_SPARES_ACTUAL.Text = dr.Item("STORES_SPARES_ACTUAL")
                STORES_SPARES_CPLY.Text = dr.Item("STORES_SPARES_CPLY")
                POWER_FUEL_BUDGET.Text = dr.Item("POWER_FUEL_BUDGET")
                POWER_FUEL_ACTUAL.Text = dr.Item("POWER_FUEL_ACTUAL")
                POWER_FUEL_CPLY.Text = dr.Item("POWER_FUEL_CPLY")
                REPAIR_MAINTENANCE_BUDGET.Text = dr.Item("R_M_BUDGET")
                REPAIR_MAINTENANCE_ACTUAL.Text = dr.Item("R_M_ACTUAL")
                REPAIR_MAINTENANCE_CPLY.Text = dr.Item("R_M_CPLY")
                FREIGHT_OUT_BUDGET.Text = dr.Item("F_O_BUDGET")
                FREIGHT_OUT_ACTUAL.Text = dr.Item("F_O_ACTUAL")
                FREIGHT_OUT_CPLY.Text = dr.Item("F_O_CPLY")
                OVERHEAD_EXP_BUDGET.Text = dr.Item("S_A_O_E_BUDGET")
                OVERHEAD_EXP_ACTUAL.Text = dr.Item("S_A_O_E_ACTUAL")
                OVERHEAD_EXP_CPLY.Text = dr.Item("S_A_O_E_CPLY")
                HO_EXP_BUDGET.Text = dr.Item("S_CMO_HO_E_BUDGET")
                HO_EXP_ACTUAL.Text = dr.Item("S_CMO_HO_E_ACTUAL")
                HO_EXP_CPLY.Text = dr.Item("S_CMO_HO_E_CPLY")
                IGST_IPT_BUDGET.Text = dr.Item("GST_IPT_BUDGET")
                IGST_IPT_ACTUAL.Text = dr.Item("GST_IPT_ACTUAL")
                IGST_IPT_CPLY.Text = dr.Item("GST_IPT_CPLY")
                GROSS_EXP_BUDGET.Text = dr.Item("G_E_BUDGET")
                GROSS_EXP_ACTUAL.Text = dr.Item("G_E_ACTUAL")
                GROSS_EXP_CPLY.Text = dr.Item("G_E_CPLY")
                LESS_TRANS_BUDGET.Text = dr.Item("LT_AC_BUDGET")
                LESS_TRANS_ACTUAL.Text = dr.Item("LT_AC_ACTUAL")
                LESS_TRANS_CPLY.Text = dr.Item("LT_AC_CPLY")
                NET_EXP_BUDGET.Text = dr.Item("N_E_BUDGET")
                NET_EXP_ACTUAL.Text = dr.Item("N_E_ACTUAL")
                NET_EXP_CPLY.Text = dr.Item("N_E_CPLY")
                GROSS_MARGIN_BUDGET.Text = dr.Item("G_M_BUDGET")
                GROSS_MARGIN_ACTUAL.Text = dr.Item("G_M_ACTUAL")
                GROSS_MARGIN_CPLY.Text = dr.Item("G_M_CPLY")
                DEPRECIATION_BUDGET.Text = dr.Item("DEP_BUDGET")
                DEPRECIATION_ACTUAL.Text = dr.Item("DEP_ACTUAL")
                DEPRECIATION_CPLY.Text = dr.Item("DEP_CPLY")
                INTEREST_CASH_CREDIT_BUDGET.Text = dr.Item("I_CC_BUDGET")
                INTEREST_CASH_CREDIT_ACTUAL.Text = dr.Item("I_CC_ACTUAL")
                INTEREST_CASH_CREDIT_CPLY.Text = dr.Item("I_CC_CPLY")
                NET_PROFIT_BUDGET.Text = dr.Item("N_P_BUDGET")
                NET_PROFIT_ACTUAL.Text = dr.Item("N_P_ACTUAL")
                NET_PROFIT_CPLY.Text = dr.Item("N_P_CPLY")
                conn.Close()
            Else
                conn.Close()
            End If
            conn.Close()
        End If
        If DropDownList1.SelectedValue = "Select Year" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select Month" Then
            DropDownList2.Focus()
            Return
        End If

        If DropDownList1.SelectedValue < Today.Date.Year Then
            conn.Open()
            Dim MC7 As New SqlCommand
            MC7.CommandText = "select * from LDM where LDM_YEAR = '" & DropDownList1.SelectedValue - 1 & "' AND LDM_MONTH = '" & DropDownList2.SelectedValue & "'"
            MC7.Connection = conn
            dr = MC7.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                IPT_CPLY_UPTO.Text = dr.Item("IPT_CPLY")

                BONUS_PENALTY_CPLY_UPTO.Text = dr.Item("BONUS_PENALTY_CPLY")

                IC_CPLY_UPTO.Text = dr.Item("IC_CPLY")

                GS_CPLY_UPTO.Text = dr.Item("GS_CPLY")

                OR_CPLY_UPTO.Text = dr.Item("OR_CPLY")

                PWB_CPLY_UPTO.Text = dr.Item("PWB_CPLY")

                TOTAL_INCOME_CPLY_UPTO.Text = dr.Item("TOTAL_INCOME_CPLY")

                ACR_DEP_CPLY_UPTO.Text = dr.Item("ACR_DEP_CPLY")

                RAW_MATERIAL_CPLY_UPTO.Text = dr.Item("RAW_MATERIAL_CPLY")

                SALARY_WAGES_CPLY_UPTO.Text = dr.Item("SALARY_WAGES_CPLY")

                STORES_SPARES_CPLY_UPTO.Text = dr.Item("STORES_SPARES_CPLY")

                POWER_FUEL_CPLY_UPTO.Text = dr.Item("POWER_FUEL_CPLY")

                REPAIR_MAINTENANCE_CPLY_UPTO.Text = dr.Item("R_M_CPLY")

                FREIGHT_OUT_CPLY_UPTO.Text = dr.Item("F_O_CPLY")

                OVERHEAD_EXP_CPLY_UPTO.Text = dr.Item("S_A_O_E_CPLY")

                HO_EXP_CPLY_UPTO.Text = dr.Item("S_CMO_HO_E_CPLY")

                IGST_IPT_CPLY_UPTO.Text = dr.Item("GST_IPT_CPLY")

                GROSS_EXP_CPLY_UPTO.Text = dr.Item("G_E_CPLY")

                LESS_TRANS_CPLY_UPTO.Text = dr.Item("LT_AC_CPLY")

                NET_EXP_CPLY_UPTO.Text = dr.Item("N_E_CPLY")

                GROSS_MARGIN_CPLY_UPTO.Text = dr.Item("G_M_CPLY")

                DEPRECIATION_CPLY_UPTO.Text = dr.Item("DEP_CPLY")

                INTEREST_CASH_CREDIT_CPLY_UPTO.Text = dr.Item("I_CC_CPLY")

                NET_PROFIT_CPLY_UPTO.Text = dr.Item("N_P_CPLY")
                conn.Close()
            Else
                conn.Close()
            End If
            conn.Close()
        End If
        If DropDownList1.SelectedValue = "Select Year" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select Month" Then
            DropDownList2.Focus()
            Return
        End If

        If DropDownList1.SelectedValue < Today.Date.Year Then
            conn.Open()
            Dim MC8 As New SqlCommand
            MC8.CommandText = "select * from LDM where LDM_YEAR = '" & 2015 & "' AND LDM_MONTH = '" & DropDownList2.SelectedValue & "'"
            MC8.Connection = conn
            dr = MC8.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                IPT_BUDGET_UPTO.Text = 0
                IPT_ACTUAL_UPTO.Text = 0
                IPT_CPLY_UPTO.Text = 0
                BONUS_PENALTY_BUDGET_UPTO.Text = 0
                BONUS_PENALTY_ACTUAL_UPTO.Text = 0
                BONUS_PENALTY_CPLY_UPTO.Text = 0
                IC_BUDGET_UPTO.Text = 0
                IC_ACTUAL_UPTO.Text = 0
                IC_CPLY_UPTO.Text = 0
                GS_BUDGET_UPTO.Text = 0
                GS_ACTUAL_UPTO.Text = 0
                GS_CPLY_UPTO.Text = 0
                OR_BUDGET_UPTO.Text = 0
                OR_ACTUAL_UPTO.Text = 0
                OR_CPLY_UPTO.Text = 0
                PWB_BUDGET_UPTO.Text = 0
                PWB_ACTUAL_UPTO.Text = 0
                PWB_CPLY_UPTO.Text = 0
                TOTAL_INCOME_BUDGET_UPTO.Text = 0
                TOTAL_INCOME_ACTUAL_UPTO.Text = 0
                TOTAL_INCOME_CPLY_UPTO.Text = 0
                ACR_DEP_BUDGET_UPTO.Text = 0
                ACR_DEP_ACTUAL_UPTO.Text = 0
                ACR_DEP_CPLY_UPTO.Text = 0
                RAW_MATERIAL_BUDGET_UPTO.Text = 0
                RAW_MATERIAL_ACTUAL_UPTO.Text = 0
                RAW_MATERIAL_CPLY_UPTO.Text = 0
                SALARY_WAGES_BUDGET_UPTO.Text = 0
                SALARY_WAGES_ACTUAL_UPTO.Text = 0
                SALARY_WAGES_CPLY_UPTO.Text = 0
                STORES_SPARES_BUDGET_UPTO.Text = 0
                STORES_SPARES_ACTUAL_UPTO.Text = 0
                STORES_SPARES_CPLY_UPTO.Text = 0
                POWER_FUEL_BUDGET_UPTO.Text = 0
                POWER_FUEL_ACTUAL_UPTO.Text = 0
                POWER_FUEL_CPLY_UPTO.Text = 0
                REPAIR_MAINTENANCE_BUDGET_UPTO.Text = 0
                REPAIR_MAINTENANCE_ACTUAL_UPTO.Text = 0
                REPAIR_MAINTENANCE_CPLY_UPTO.Text = 0
                FREIGHT_OUT_BUDGET_UPTO.Text = 0
                FREIGHT_OUT_ACTUAL_UPTO.Text = 0
                FREIGHT_OUT_CPLY_UPTO.Text = 0
                OVERHEAD_EXP_BUDGET_UPTO.Text = 0
                OVERHEAD_EXP_ACTUAL_UPTO.Text = 0
                OVERHEAD_EXP_CPLY_UPTO.Text = 0
                HO_EXP_BUDGET_UPTO.Text = 0
                HO_EXP_ACTUAL_UPTO.Text = 0
                HO_EXP_CPLY_UPTO.Text = 0
                IGST_IPT_BUDGET_UPTO.Text = 0
                IGST_IPT_ACTUAL_UPTO.Text = 0
                IGST_IPT_CPLY_UPTO.Text = 0
                GROSS_EXP_BUDGET_UPTO.Text = 0
                GROSS_EXP_ACTUAL.Text = 0
                GROSS_EXP_CPLY_UPTO.Text = 0
                LESS_TRANS_BUDGET_UPTO.Text = 0
                LESS_TRANS_ACTUAL_UPTO.Text = 0
                LESS_TRANS_CPLY_UPTO.Text = 0
                NET_EXP_BUDGET_UPTO.Text = 0
                NET_EXP_ACTUAL_UPTO.Text = 0
                NET_EXP_CPLY_UPTO.Text = 0
                GROSS_MARGIN_BUDGET_UPTO.Text = 0
                GROSS_MARGIN_ACTUAL_UPTO.Text = 0
                GROSS_MARGIN_CPLY_UPTO.Text = 0
                DEPRECIATION_BUDGET_UPTO.Text = 0
                DEPRECIATION_ACTUAL_UPTO.Text = 0
                DEPRECIATION_CPLY_UPTO.Text = 0
                INTEREST_CASH_CREDIT_BUDGET_UPTO.Text = 0
                INTEREST_CASH_CREDIT_ACTUAL_UPTO.Text = 0
                INTEREST_CASH_CREDIT_CPLY_UPTO.Text = 0
                NET_PROFIT_BUDGET_UPTO.Text = 0
                NET_PROFIT_ACTUAL_UPTO.Text = 0
                NET_PROFIT_CPLY_UPTO.Text = 0
                conn.Close()
            Else
                conn.Close()
            End If
            conn.Close()
        End If
    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        If DropDownList1.SelectedValue = "Select Year" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select Month" Then
            DropDownList2.Focus()
            Return
        End If
        If DropDownList1.SelectedValue < Today.Date.Year Then
            conn.Open()
            Dim MC6 As New SqlCommand
            MC6.CommandText = "select * from LDM where LDM_YEAR = '" & DropDownList1.SelectedValue & "' AND LDM_MONTH = '" & DropDownList2.SelectedValue & "'"
            MC6.Connection = conn
            dr = MC6.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                IPT_BUDGET.Text = dr.Item("IPT_BUDGET")
                IPT_ACTUAL.Text = dr.Item("IPT_ACTUAL")
                IPT_CPLY.Text = dr.Item("IPT_CPLY")
                BONUS_PENALTY_BUDGET.Text = dr.Item("BONUS_PENALTY_BUDGET")
                BONUS_PENALTY_ACTUAL.Text = dr.Item("BONUS_PENALTY_ACTUAL")
                BONUS_PENALTY_CPLY.Text = dr.Item("BONUS_PENALTY_CPLY")
                IC_BUDGET.Text = dr.Item("IC_BUDGET")
                IC_ACTUAL.Text = dr.Item("IC_ACTUAL")
                IC_CPLY.Text = dr.Item("IC_CPLY")
                GS_BUDGET.Text = dr.Item("GS_BUDGET")
                GS_ACTUAL.Text = dr.Item("GS_ACTUAL")
                GS_CPLY.Text = dr.Item("GS_CPLY")
                OR_BUDGET.Text = dr.Item("OR_BUDGET")
                OR_ACTUAL.Text = dr.Item("OR_ACTUAL")
                OR_CPLY.Text = dr.Item("OR_CPLY")
                PWB_BUDGET.Text = dr.Item("PWB_BUDGET")
                PWB_ACTUAL.Text = dr.Item("PWB_ACTUAL")
                PWB_CPLY.Text = dr.Item("PWB_CPLY")
                TOTAL_INCOME_BUDGET.Text = dr.Item("TOTAL_INCOME_BUDGET")
                TOTAL_INCOME_ACTUAL.Text = dr.Item("TOTAL_INCOME_ACTUAL")
                TOTAL_INCOME_CPLY.Text = dr.Item("TOTAL_INCOME_CPLY")
                ACR_DEP_BUDGET.Text = dr.Item("ACR_DEP_BUDGET")
                ACR_DEP_ACTUAL.Text = dr.Item("ACR_DEP_ACTUAL")
                ACR_DEP_CPLY.Text = dr.Item("ACR_DEP_CPLY")
                RAW_MATERIAL_BUDGET.Text = dr.Item("RAW_MATERIAL_BUDGET")
                RAW_MATERIAL_ACTUAL.Text = dr.Item("RAW_MATERIAL_ACTUAL")
                RAW_MATERIAL_CPLY.Text = dr.Item("RAW_MATERIAL_CPLY")
                SALARY_WAGES_BUDGET.Text = dr.Item("SALARY_WAGES_BUDGET")
                SALARY_WAGES_ACTUAL.Text = dr.Item("SALARY_WAGES_ACTUAL")
                SALARY_WAGES_CPLY.Text = dr.Item("SALARY_WAGES_CPLY")
                STORES_SPARES_BUDGET.Text = dr.Item("STORES_SPARES_BUDGET")
                STORES_SPARES_ACTUAL.Text = dr.Item("STORES_SPARES_ACTUAL")
                STORES_SPARES_CPLY.Text = dr.Item("STORES_SPARES_CPLY")
                POWER_FUEL_BUDGET.Text = dr.Item("POWER_FUEL_BUDGET")
                POWER_FUEL_ACTUAL.Text = dr.Item("POWER_FUEL_ACTUAL")
                POWER_FUEL_CPLY.Text = dr.Item("POWER_FUEL_CPLY")
                REPAIR_MAINTENANCE_BUDGET.Text = dr.Item("R_M_BUDGET")
                REPAIR_MAINTENANCE_ACTUAL.Text = dr.Item("R_M_ACTUAL")
                REPAIR_MAINTENANCE_CPLY.Text = dr.Item("R_M_CPLY")
                FREIGHT_OUT_BUDGET.Text = dr.Item("F_O_BUDGET")
                FREIGHT_OUT_ACTUAL.Text = dr.Item("F_O_ACTUAL")
                FREIGHT_OUT_CPLY.Text = dr.Item("F_O_CPLY")
                OVERHEAD_EXP_BUDGET.Text = dr.Item("S_A_O_E_BUDGET")
                OVERHEAD_EXP_ACTUAL.Text = dr.Item("S_A_O_E_ACTUAL")
                OVERHEAD_EXP_CPLY.Text = dr.Item("S_A_O_E_CPLY")
                HO_EXP_BUDGET.Text = dr.Item("S_CMO_HO_E_BUDGET")
                HO_EXP_ACTUAL.Text = dr.Item("S_CMO_HO_E_ACTUAL")
                HO_EXP_CPLY.Text = dr.Item("S_CMO_HO_E_CPLY")
                IGST_IPT_BUDGET.Text = dr.Item("GST_IPT_BUDGET")
                IGST_IPT_ACTUAL.Text = dr.Item("GST_IPT_ACTUAL")
                IGST_IPT_CPLY.Text = dr.Item("GST_IPT_CPLY")
                GROSS_EXP_BUDGET.Text = dr.Item("G_E_BUDGET")
                GROSS_EXP_ACTUAL.Text = dr.Item("G_E_ACTUAL")
                GROSS_EXP_CPLY.Text = dr.Item("G_E_CPLY")
                LESS_TRANS_BUDGET.Text = dr.Item("LT_AC_BUDGET")
                LESS_TRANS_ACTUAL.Text = dr.Item("LT_AC_ACTUAL")
                LESS_TRANS_CPLY.Text = dr.Item("LT_AC_CPLY")
                NET_EXP_BUDGET.Text = dr.Item("N_E_BUDGET")
                NET_EXP_ACTUAL.Text = dr.Item("N_E_ACTUAL")
                NET_EXP_CPLY.Text = dr.Item("N_E_CPLY")
                GROSS_MARGIN_BUDGET.Text = dr.Item("G_M_BUDGET")
                GROSS_MARGIN_ACTUAL.Text = dr.Item("G_M_ACTUAL")
                GROSS_MARGIN_CPLY.Text = dr.Item("G_M_CPLY")
                DEPRECIATION_BUDGET.Text = dr.Item("DEP_BUDGET")
                DEPRECIATION_ACTUAL.Text = dr.Item("DEP_ACTUAL")
                DEPRECIATION_CPLY.Text = dr.Item("DEP_CPLY")
                INTEREST_CASH_CREDIT_BUDGET.Text = dr.Item("I_CC_BUDGET")
                INTEREST_CASH_CREDIT_ACTUAL.Text = dr.Item("I_CC_ACTUAL")
                INTEREST_CASH_CREDIT_CPLY.Text = dr.Item("I_CC_CPLY")
                NET_PROFIT_BUDGET.Text = dr.Item("N_P_BUDGET")
                NET_PROFIT_ACTUAL.Text = dr.Item("N_P_ACTUAL")
                NET_PROFIT_CPLY.Text = dr.Item("N_P_CPLY")
                conn.Close()
            Else
                conn.Close()
            End If
        End If
        If DropDownList1.SelectedValue = "Select Year" Then
            DropDownList1.Focus()
            Return
        ElseIf DropDownList2.SelectedValue = "Select Month" Then
            DropDownList2.Focus()
            Return
        End If
        conn.Open()
        Dim MC7 As New SqlCommand
        MC7.CommandText = "select * from LDM where LDM_YEAR = '" & DropDownList1.SelectedValue - 1 & "' AND LDM_MONTH = '" & DropDownList2.SelectedValue & "'"
        MC7.Connection = conn
        dr = MC7.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            IPT_CPLY_UPTO.Text = dr.Item("IPT_CPLY")

            BONUS_PENALTY_CPLY_UPTO.Text = dr.Item("BONUS_PENALTY_CPLY")

            IC_CPLY_UPTO.Text = dr.Item("IC_CPLY")

            GS_CPLY_UPTO.Text = dr.Item("GS_CPLY")

            OR_CPLY_UPTO.Text = dr.Item("OR_CPLY")

            PWB_CPLY_UPTO.Text = dr.Item("PWB_CPLY")

            TOTAL_INCOME_CPLY_UPTO.Text = dr.Item("TOTAL_INCOME_CPLY")

            ACR_DEP_CPLY_UPTO.Text = dr.Item("ACR_DEP_CPLY")

            RAW_MATERIAL_CPLY_UPTO.Text = dr.Item("RAW_MATERIAL_CPLY")

            SALARY_WAGES_CPLY_UPTO.Text = dr.Item("SALARY_WAGES_CPLY")

            STORES_SPARES_CPLY_UPTO.Text = dr.Item("STORES_SPARES_CPLY")

            POWER_FUEL_CPLY_UPTO.Text = dr.Item("POWER_FUEL_CPLY")

            REPAIR_MAINTENANCE_CPLY_UPTO.Text = dr.Item("R_M_CPLY")

            FREIGHT_OUT_CPLY_UPTO.Text = dr.Item("F_O_CPLY")

            OVERHEAD_EXP_CPLY_UPTO.Text = dr.Item("S_A_O_E_CPLY")

            HO_EXP_CPLY_UPTO.Text = dr.Item("S_CMO_HO_E_CPLY")

            IGST_IPT_CPLY_UPTO.Text = dr.Item("GST_IPT_CPLY")

            GROSS_EXP_CPLY_UPTO.Text = dr.Item("G_E_CPLY")

            LESS_TRANS_CPLY_UPTO.Text = dr.Item("LT_AC_CPLY")

            NET_EXP_CPLY_UPTO.Text = dr.Item("N_E_CPLY")

            GROSS_MARGIN_CPLY_UPTO.Text = dr.Item("G_M_CPLY")

            DEPRECIATION_CPLY_UPTO.Text = dr.Item("DEP_CPLY")

            INTEREST_CASH_CREDIT_CPLY_UPTO.Text = dr.Item("I_CC_CPLY")

            NET_PROFIT_CPLY_UPTO.Text = dr.Item("N_P_CPLY")
            conn.Close()
        Else
            conn.Close()
        End If

    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DropDownList2.SelectedValue = "Select Month" Then
            DropDownList2.Focus()
            Return
        ElseIf DropDownList1.SelectedValue = "Select Year" Then
            DropDownList1.Focus()
            Return
        End If
        Dim MonthNumber As New String("")
        ''MonthNumber = Month(DateValue("01 " & DropDownList2.SelectedItem.Text & " " & DropDownList1.SelectedValue))
        MonthNumber = Month(DateValue("01-" & DropDownList2.SelectedItem.Text & "-" & DropDownList1.SelectedValue)).ToString()
        Dim currentWorkingDate As Date = CDate("01-" & MonthNumber & "-" & DropDownList1.SelectedValue)
        Dim previousYearWorkingDate As Date = CDate("01-" & MonthNumber & "-" & DropDownList1.SelectedValue - 1)

        Dim currentFiscalYear, previousYearFiscalYear As New String("")
        If currentWorkingDate.Month > 3 Then
            currentFiscalYear = currentWorkingDate.Year
            currentFiscalYear = currentFiscalYear.Trim.Substring(2)
            currentFiscalYear = currentFiscalYear & (currentFiscalYear + 1)
        ElseIf currentWorkingDate.Month <= 3 Then
            currentFiscalYear = currentWorkingDate.Year
            currentFiscalYear = currentFiscalYear.Trim.Substring(2)
            currentFiscalYear = (currentFiscalYear - 1) & currentFiscalYear
        End If

        If previousYearWorkingDate.Month > 3 Then
            previousYearFiscalYear = previousYearWorkingDate.Year
            previousYearFiscalYear = previousYearFiscalYear.Trim.Substring(2)
            previousYearFiscalYear = previousYearFiscalYear & (previousYearFiscalYear + 1)
        ElseIf previousYearWorkingDate.Month <= 3 Then
            previousYearFiscalYear = previousYearWorkingDate.Year
            previousYearFiscalYear = previousYearFiscalYear.Trim.Substring(2)
            previousYearFiscalYear = (previousYearFiscalYear - 1) & previousYearFiscalYear
        End If

        Try
            conn.Open()
            Dim MC6 As New SqlCommand
            MC6.CommandText = "select * from LDM where LDM_YEAR = '" & DropDownList1.Text & "' AND LDM_MONTH = '" & DropDownList2.SelectedItem.Text & "'"
            MC6.Connection = conn
            dr = MC6.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                IPT_BUDGET.Text = dr.Item("IPT_BUDGET")
                IPT_ACTUAL.Text = dr.Item("IPT_ACTUAL")
                'IPT_CPLY.Text = dr.Item("IPT_CPLY")
                BONUS_PENALTY_BUDGET.Text = dr.Item("BONUS_PENALTY_BUDGET")
                BONUS_PENALTY_ACTUAL.Text = dr.Item("BONUS_PENALTY_ACTUAL")
                'BONUS_PENALTY_CPLY.Text = dr.Item("BONUS_PENALTY_CPLY")
                IC_BUDGET.Text = dr.Item("IC_BUDGET")
                IC_ACTUAL.Text = dr.Item("IC_ACTUAL")
                'IC_CPLY.Text = dr.Item("IC_CPLY")
                GS_BUDGET.Text = dr.Item("GS_BUDGET")
                GS_ACTUAL.Text = dr.Item("GS_ACTUAL")
                'GS_CPLY.Text = dr.Item("GS_CPLY")
                OR_BUDGET.Text = dr.Item("OR_BUDGET")
                OR_ACTUAL.Text = dr.Item("OR_ACTUAL")
                'OR_CPLY.Text = dr.Item("OR_CPLY")
                PWB_BUDGET.Text = dr.Item("PWB_BUDGET")
                PWB_ACTUAL.Text = dr.Item("PWB_ACTUAL")
                'PWB_CPLY.Text = dr.Item("PWB_CPLY")
                TOTAL_INCOME_BUDGET.Text = dr.Item("TOTAL_INCOME_BUDGET")
                TOTAL_INCOME_ACTUAL.Text = dr.Item("TOTAL_INCOME_ACTUAL")
                'TOTAL_INCOME_CPLY.Text = dr.Item("TOTAL_INCOME_CPLY")
                ACR_DEP_BUDGET.Text = dr.Item("ACR_DEP_BUDGET")
                ACR_DEP_ACTUAL.Text = dr.Item("ACR_DEP_ACTUAL")
                'ACR_DEP_CPLY.Text = dr.Item("ACR_DEP_CPLY")
                RAW_MATERIAL_BUDGET.Text = dr.Item("RAW_MATERIAL_BUDGET")
                RAW_MATERIAL_ACTUAL.Text = dr.Item("RAW_MATERIAL_ACTUAL")
                'RAW_MATERIAL_CPLY.Text = dr.Item("RAW_MATERIAL_CPLY")
                SALARY_WAGES_BUDGET.Text = dr.Item("SALARY_WAGES_BUDGET")
                SALARY_WAGES_ACTUAL.Text = dr.Item("SALARY_WAGES_ACTUAL")
                'SALARY_WAGES_CPLY.Text = dr.Item("SALARY_WAGES_CPLY")
                STORES_SPARES_BUDGET.Text = dr.Item("STORES_SPARES_BUDGET")
                STORES_SPARES_ACTUAL.Text = dr.Item("STORES_SPARES_ACTUAL")
                'STORES_SPARES_CPLY.Text = dr.Item("STORES_SPARES_CPLY")
                POWER_FUEL_BUDGET.Text = dr.Item("POWER_FUEL_BUDGET")
                POWER_FUEL_ACTUAL.Text = dr.Item("POWER_FUEL_ACTUAL")
                'POWER_FUEL_CPLY.Text = dr.Item("POWER_FUEL_CPLY")
                REPAIR_MAINTENANCE_BUDGET.Text = dr.Item("REPAIR_MAINTENANCE_BUDGET")
                REPAIR_MAINTENANCE_ACTUAL.Text = dr.Item("REPAIR_MAINTENANCE_ACTUAL")
                'REPAIR_MAINTENANCE_CPLY.Text = dr.Item("REPAIR_MAINTENANCE_CPLY")
                FREIGHT_OUT_BUDGET.Text = dr.Item("FREIGHT_OUT_BUDGET")
                FREIGHT_OUT_ACTUAL.Text = dr.Item("FREIGHT_OUT_ACTUAL")
                'FREIGHT_OUT_CPLY.Text = dr.Item("FREIGHT_OUT_CPLY")
                OVERHEAD_EXP_BUDGET.Text = dr.Item("OVERHEAD_EXP_BUDGET")
                OVERHEAD_EXP_ACTUAL.Text = dr.Item("OVERHEAD_EXP_ACTUAL")
                'OVERHEAD_EXP_CPLY.Text = dr.Item("OVERHEAD_EXP_CPLY")
                HO_EXP_BUDGET.Text = dr.Item("HO_EXP_BUDGET")
                HO_EXP_ACTUAL.Text = dr.Item("HO_EXP_ACTUAL")
                'HO_EXP_CPLY.Text = dr.Item("HO_EXP_CPLY")
                IGST_IPT_BUDGET.Text = dr.Item("IGST_IPT_BUDGET")
                IGST_IPT_ACTUAL.Text = dr.Item("IGST_IPT_ACTUAL")
                'IGST_IPT_CPLY.Text = dr.Item("IGST_IPT_CPLY")
                GROSS_EXP_BUDGET.Text = dr.Item("GROSS_EXP_BUDGET")
                GROSS_EXP_ACTUAL.Text = dr.Item("GROSS_EXP_ACTUAL")
                'GROSS_EXP_CPLY.Text = dr.Item("GROSS_EXP_CPLY")
                LESS_TRANS_BUDGET.Text = dr.Item("LESS_TRANS_BUDGET")
                LESS_TRANS_ACTUAL.Text = dr.Item("LESS_TRANS_ACTUAL")
                'LESS_TRANS_CPLY.Text = dr.Item("LESS_TRANS_CPLY")
                NET_EXP_BUDGET.Text = dr.Item("NET_EXP_BUDGET")
                NET_EXP_ACTUAL.Text = dr.Item("NET_EXP_ACTUAL")
                'NET_EXP_CPLY.Text = dr.Item("NET_EXP_CPLY")
                GROSS_MARGIN_BUDGET.Text = dr.Item("GROSS_MARGIN_BUDGET")
                GROSS_MARGIN_ACTUAL.Text = dr.Item("GROSS_MARGIN_ACTUAL")
                'GROSS_MARGIN_CPLY.Text = dr.Item("GROSS_MARGIN_CPLY")
                DEPRECIATION_BUDGET.Text = dr.Item("DEPRECIATION_BUDGET")
                DEPRECIATION_ACTUAL.Text = dr.Item("DEPRECIATION_ACTUAL")
                'DEPRECIATION_CPLY.Text = dr.Item("DEPRECIATION_CPLY")
                INTEREST_CASH_CREDIT_BUDGET.Text = dr.Item("INTEREST_CASH_CREDIT_BUDGET")
                INTEREST_CASH_CREDIT_ACTUAL.Text = dr.Item("INTEREST_CASH_CREDIT_ACTUAL")
                'INTEREST_CASH_CREDIT_CPLY.Text = dr.Item("INTEREST_CASH_CREDIT_CPLY")
                NET_PROFIT_BUDGET.Text = dr.Item("NET_PROFIT_BUDGET")
                NET_PROFIT_ACTUAL.Text = dr.Item("NET_PROFIT_ACTUAL")
                'NET_PROFIT_CPLY.Text = dr.Item("NET_PROFIT_CPLY")
                conn.Close()
            Else

                IPT_BUDGET.Text = "0.00"
                IPT_ACTUAL.Text = "0.00"

                BONUS_PENALTY_BUDGET.Text = "0.00"
                BONUS_PENALTY_ACTUAL.Text = "0.00"

                IC_BUDGET.Text = "0.00"
                IC_ACTUAL.Text = "0.00"

                GS_BUDGET.Text = "0.00"
                GS_ACTUAL.Text = "0.00"

                OR_BUDGET.Text = "0.00"
                OR_ACTUAL.Text = "0.00"

                PWB_BUDGET.Text = "0.00"
                PWB_ACTUAL.Text = "0.00"

                TOTAL_INCOME_BUDGET.Text = "0.00"
                TOTAL_INCOME_ACTUAL.Text = "0.00"

                ACR_DEP_BUDGET.Text = "0.00"
                ACR_DEP_ACTUAL.Text = "0.00"

                RAW_MATERIAL_BUDGET.Text = "0.00"
                RAW_MATERIAL_ACTUAL.Text = "0.00"

                SALARY_WAGES_BUDGET.Text = "0.00"
                SALARY_WAGES_ACTUAL.Text = "0.00"

                STORES_SPARES_BUDGET.Text = "0.00"
                STORES_SPARES_ACTUAL.Text = "0.00"

                POWER_FUEL_BUDGET.Text = "0.00"
                POWER_FUEL_ACTUAL.Text = "0.00"

                REPAIR_MAINTENANCE_BUDGET.Text = "0.00"
                REPAIR_MAINTENANCE_ACTUAL.Text = "0.00"

                FREIGHT_OUT_BUDGET.Text = "0.00"
                FREIGHT_OUT_ACTUAL.Text = "0.00"

                OVERHEAD_EXP_BUDGET.Text = "0.00"
                OVERHEAD_EXP_ACTUAL.Text = "0.00"

                HO_EXP_BUDGET.Text = "0.00"
                HO_EXP_ACTUAL.Text = "0.00"

                IGST_IPT_BUDGET.Text = "0.00"
                IGST_IPT_ACTUAL.Text = "0.00"

                GROSS_EXP_BUDGET.Text = "0.00"
                GROSS_EXP_ACTUAL.Text = "0.00"

                LESS_TRANS_BUDGET.Text = "0.00"
                LESS_TRANS_ACTUAL.Text = "0.00"

                NET_EXP_BUDGET.Text = "0.00"
                NET_EXP_ACTUAL.Text = "0.00"

                GROSS_MARGIN_BUDGET.Text = "0.00"
                GROSS_MARGIN_ACTUAL.Text = "0.00"

                DEPRECIATION_BUDGET.Text = "0.00"
                DEPRECIATION_ACTUAL.Text = "0.00"

                INTEREST_CASH_CREDIT_BUDGET.Text = "0.00"
                INTEREST_CASH_CREDIT_ACTUAL.Text = "0.00"

                NET_PROFIT_BUDGET.Text = "0.00"
                NET_PROFIT_ACTUAL.Text = "0.00"


                conn.Close()
            End If

            ''Calculate current Year CPLY data
            conn.Open()
            Dim MC7 As New SqlCommand
            MC7.CommandText = "select * from LDM where LDM_YEAR = '" & DropDownList1.SelectedValue - 1 & "' AND LDM_MONTH = '" & DropDownList2.SelectedItem.Text & "'"
            MC7.Connection = conn
            dr = MC7.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                IPT_CPLY.Text = dr.Item("IPT_ACTUAL")
                BONUS_PENALTY_CPLY.Text = dr.Item("BONUS_PENALTY_ACTUAL")
                IC_CPLY.Text = dr.Item("IC_ACTUAL")
                GS_CPLY.Text = dr.Item("GS_ACTUAL")
                OR_CPLY.Text = dr.Item("OR_ACTUAL")
                PWB_CPLY.Text = dr.Item("PWB_ACTUAL")
                TOTAL_INCOME_CPLY.Text = dr.Item("TOTAL_INCOME_ACTUAL")
                ACR_DEP_CPLY.Text = dr.Item("ACR_DEP_ACTUAL")
                RAW_MATERIAL_CPLY.Text = dr.Item("RAW_MATERIAL_ACTUAL")
                SALARY_WAGES_CPLY.Text = dr.Item("SALARY_WAGES_ACTUAL")
                STORES_SPARES_CPLY.Text = dr.Item("STORES_SPARES_ACTUAL")
                POWER_FUEL_CPLY.Text = dr.Item("POWER_FUEL_ACTUAL")
                REPAIR_MAINTENANCE_CPLY.Text = dr.Item("REPAIR_MAINTENANCE_ACTUAL")
                FREIGHT_OUT_CPLY.Text = dr.Item("FREIGHT_OUT_ACTUAL")
                OVERHEAD_EXP_CPLY.Text = dr.Item("OVERHEAD_EXP_ACTUAL")
                HO_EXP_CPLY.Text = dr.Item("HO_EXP_ACTUAL")
                IGST_IPT_CPLY.Text = dr.Item("IGST_IPT_ACTUAL")
                GROSS_EXP_CPLY.Text = dr.Item("GROSS_EXP_ACTUAL")
                LESS_TRANS_CPLY.Text = dr.Item("LESS_TRANS_ACTUAL")
                NET_EXP_CPLY.Text = dr.Item("NET_EXP_ACTUAL")
                GROSS_MARGIN_CPLY.Text = dr.Item("GROSS_MARGIN_ACTUAL")
                DEPRECIATION_CPLY.Text = dr.Item("DEPRECIATION_ACTUAL")
                INTEREST_CASH_CREDIT_CPLY.Text = dr.Item("INTEREST_CASH_CREDIT_ACTUAL")
                NET_PROFIT_CPLY.Text = dr.Item("NET_PROFIT_ACTUAL")
                conn.Close()
            Else
                conn.Close()
            End If



            ''Calculate Last Year Upto the month CPLY data
            conn.Open()
            ''Dim MC7 As New SqlCommand
            MC7.CommandText = "SELECT SUM(IPT_ACTUAL) AS IPT_ACTUAL_UPTO,SUM(BONUS_PENALTY_ACTUAL) AS BONUS_PENALTY_ACTUAL_UPTO,
	        SUM(IC_ACTUAL) AS IC_ACTUAL_UPTO,SUM(GS_ACTUAL) AS GS_ACTUAL_UPTO,SUM(OR_ACTUAL) AS OR_ACTUAL_UPTO,
	        SUM(PWB_ACTUAL) AS PWB_ACTUAL_UPTO,SUM(TOTAL_INCOME_ACTUAL) AS TOTAL_INCOME_ACTUAL_UPTO,
	        SUM(ACR_DEP_ACTUAL) AS ACR_DEP_ACTUAL_UPTO,SUM(RAW_MATERIAL_ACTUAL) AS RAW_MATERIAL_ACTUAL_UPTO,
	        SUM(SALARY_WAGES_ACTUAL) AS SALARY_WAGES_ACTUAL_UPTO,SUM(STORES_SPARES_ACTUAL) AS STORES_SPARES_ACTUAL_UPTO,
	        SUM(POWER_FUEL_ACTUAL) AS POWER_FUEL_ACTUAL_UPTO,SUM(REPAIR_MAINTENANCE_ACTUAL) AS REPAIR_MAINTENANCE_ACTUAL_UPTO,
	        SUM(FREIGHT_OUT_ACTUAL) AS FREIGHT_OUT_ACTUAL_UPTO,SUM(OVERHEAD_EXP_ACTUAL) AS OVERHEAD_EXP_ACTUAL_UPTO,
	        SUM(HO_EXP_ACTUAL) AS HO_EXP_ACTUAL_UPTO,SUM(IGST_IPT_ACTUAL) AS IGST_IPT_ACTUAL_UPTO,
	        SUM(GROSS_EXP_ACTUAL) AS GROSS_EXP_ACTUAL_UPTO,SUM(LESS_TRANS_ACTUAL) AS LESS_TRANS_ACTUAL_UPTO,
	        SUM(NET_EXP_ACTUAL) AS NET_EXP_ACTUAL_UPTO,SUM(GROSS_MARGIN_ACTUAL) AS GROSS_MARGIN_ACTUAL_UPTO,
	        SUM(DEPRECIATION_ACTUAL) AS DEPRECIATION_ACTUAL_UPTO,SUM(INTEREST_CASH_CREDIT_ACTUAL) AS INTEREST_CASH_CREDIT_ACTUAL_UPTO,
	        SUM(NET_PROFIT_ACTUAL) AS NET_PROFIT_ACTUAL_UPTO
            FROM LDM WHERE MONTH_SL_NO<=" & DropDownList2.SelectedValue & " AND FISCAL_YEAR='" & previousYearFiscalYear & "'"
            MC7.Connection = conn
            dr = MC7.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                IPT_CPLY_UPTO.Text = dr.Item("IPT_ACTUAL_UPTO")
                BONUS_PENALTY_CPLY_UPTO.Text = dr.Item("BONUS_PENALTY_ACTUAL_UPTO")
                IC_CPLY_UPTO.Text = dr.Item("IC_ACTUAL_UPTO")
                GS_CPLY_UPTO.Text = dr.Item("GS_ACTUAL_UPTO")
                OR_CPLY_UPTO.Text = dr.Item("OR_ACTUAL_UPTO")
                PWB_CPLY_UPTO.Text = dr.Item("PWB_ACTUAL_UPTO")
                TOTAL_INCOME_CPLY_UPTO.Text = dr.Item("TOTAL_INCOME_ACTUAL_UPTO")
                ACR_DEP_CPLY_UPTO.Text = dr.Item("ACR_DEP_ACTUAL_UPTO")
                RAW_MATERIAL_CPLY_UPTO.Text = dr.Item("RAW_MATERIAL_ACTUAL_UPTO")
                SALARY_WAGES_CPLY_UPTO.Text = dr.Item("SALARY_WAGES_ACTUAL_UPTO")
                STORES_SPARES_CPLY_UPTO.Text = dr.Item("STORES_SPARES_ACTUAL_UPTO")
                POWER_FUEL_CPLY_UPTO.Text = dr.Item("POWER_FUEL_ACTUAL_UPTO")
                REPAIR_MAINTENANCE_CPLY_UPTO.Text = dr.Item("REPAIR_MAINTENANCE_ACTUAL_UPTO")
                FREIGHT_OUT_CPLY_UPTO.Text = dr.Item("FREIGHT_OUT_ACTUAL_UPTO")
                OVERHEAD_EXP_CPLY_UPTO.Text = dr.Item("OVERHEAD_EXP_ACTUAL_UPTO")
                HO_EXP_CPLY_UPTO.Text = dr.Item("HO_EXP_ACTUAL_UPTO")
                IGST_IPT_CPLY_UPTO.Text = dr.Item("IGST_IPT_ACTUAL_UPTO")
                GROSS_EXP_CPLY_UPTO.Text = dr.Item("GROSS_EXP_ACTUAL_UPTO")
                LESS_TRANS_CPLY_UPTO.Text = dr.Item("LESS_TRANS_ACTUAL_UPTO")
                NET_EXP_CPLY_UPTO.Text = dr.Item("NET_EXP_ACTUAL_UPTO")
                GROSS_MARGIN_CPLY_UPTO.Text = dr.Item("GROSS_MARGIN_ACTUAL_UPTO")
                DEPRECIATION_CPLY_UPTO.Text = dr.Item("DEPRECIATION_ACTUAL_UPTO")
                INTEREST_CASH_CREDIT_CPLY_UPTO.Text = dr.Item("INTEREST_CASH_CREDIT_ACTUAL_UPTO")
                NET_PROFIT_CPLY_UPTO.Text = dr.Item("NET_PROFIT_ACTUAL_UPTO")
                conn.Close()
            Else
                conn.Close()
            End If


            'CALCULATING UPTO THE MONTH BUDGET AND ACTUAL VALUES

            conn.Open()
            'Dim MC7 As New SqlCommand
            MC7.CommandText = "SELECT SUM(IPT_BUDGET) AS IPT_BUDGET_UPTO,SUM(IPT_ACTUAL) AS IPT_ACTUAL_UPTO,SUM(BONUS_PENALTY_BUDGET) AS BONUS_PENALTY_BUDGET_UPTO,SUM(BONUS_PENALTY_ACTUAL) AS BONUS_PENALTY_ACTUAL_UPTO,
	        SUM(IC_BUDGET) AS IC_BUDGET_UPTO,SUM(IC_ACTUAL) AS IC_ACTUAL_UPTO,SUM(GS_BUDGET) AS GS_BUDGET_UPTO,SUM(GS_ACTUAL) AS GS_ACTUAL_UPTO,SUM(OR_BUDGET) AS OR_BUDGET_UPTO,SUM(OR_ACTUAL) AS OR_ACTUAL_UPTO,
	        SUM(PWB_BUDGET) AS PWB_BUDGET_UPTO,SUM(PWB_ACTUAL) AS PWB_ACTUAL_UPTO,SUM(TOTAL_INCOME_BUDGET) AS TOTAL_INCOME_BUDGET_UPTO,SUM(TOTAL_INCOME_ACTUAL) AS TOTAL_INCOME_ACTUAL_UPTO,
	        SUM(ACR_DEP_BUDGET) AS ACR_DEP_BUDGET_UPTO,SUM(ACR_DEP_ACTUAL) AS ACR_DEP_ACTUAL_UPTO,SUM(RAW_MATERIAL_BUDGET) AS RAW_MATERIAL_BUDGET_UPTO,SUM(RAW_MATERIAL_ACTUAL) AS RAW_MATERIAL_ACTUAL_UPTO,
	        SUM(SALARY_WAGES_BUDGET) AS SALARY_WAGES_BUDGET_UPTO,SUM(SALARY_WAGES_ACTUAL) AS SALARY_WAGES_ACTUAL_UPTO,SUM(STORES_SPARES_BUDGET) AS STORES_SPARES_BUDGET_UPTO,SUM(STORES_SPARES_ACTUAL) AS STORES_SPARES_ACTUAL_UPTO,
	        SUM(POWER_FUEL_BUDGET) AS POWER_FUEL_BUDGET_UPTO,SUM(POWER_FUEL_ACTUAL) AS POWER_FUEL_ACTUAL_UPTO,SUM(REPAIR_MAINTENANCE_BUDGET) AS REPAIR_MAINTENANCE_BUDGET_UPTO,SUM(REPAIR_MAINTENANCE_ACTUAL) AS REPAIR_MAINTENANCE_ACTUAL_UPTO,
	        SUM(FREIGHT_OUT_BUDGET) AS FREIGHT_OUT_BUDGET_UPTO,SUM(FREIGHT_OUT_ACTUAL) AS FREIGHT_OUT_ACTUAL_UPTO,SUM(OVERHEAD_EXP_BUDGET) AS OVERHEAD_EXP_BUDGET_UPTO,SUM(OVERHEAD_EXP_ACTUAL) AS OVERHEAD_EXP_ACTUAL_UPTO,
	        SUM(HO_EXP_BUDGET) AS HO_EXP_BUDGET_UPTO,SUM(HO_EXP_ACTUAL) AS HO_EXP_ACTUAL_UPTO,SUM(IGST_IPT_BUDGET) AS IGST_IPT_BUDGET_UPTO,SUM(IGST_IPT_ACTUAL) AS IGST_IPT_ACTUAL_UPTO,
	        SUM(GROSS_EXP_BUDGET) AS GROSS_EXP_BUDGET_UPTO,SUM(GROSS_EXP_ACTUAL) AS GROSS_EXP_ACTUAL_UPTO,SUM(LESS_TRANS_BUDGET) AS LESS_TRANS_BUDGET_UPTO,SUM(LESS_TRANS_ACTUAL) AS LESS_TRANS_ACTUAL_UPTO,
	        SUM(NET_EXP_BUDGET) AS NET_EXP_BUDGET_UPTO,SUM(NET_EXP_ACTUAL) AS NET_EXP_ACTUAL_UPTO,SUM(GROSS_MARGIN_BUDGET) AS GROSS_MARGIN_BUDGET_UPTO,SUM(GROSS_MARGIN_ACTUAL) AS GROSS_MARGIN_ACTUAL_UPTO,
	        SUM(DEPRECIATION_BUDGET) AS DEPRECIATION_BUDGET_UPTO,SUM(DEPRECIATION_ACTUAL) AS DEPRECIATION_ACTUAL_UPTO,SUM(INTEREST_CASH_CREDIT_BUDGET) AS INTEREST_CASH_CREDIT_BUDGET_UPTO,SUM(INTEREST_CASH_CREDIT_ACTUAL) AS INTEREST_CASH_CREDIT_ACTUAL_UPTO,
	        SUM(NET_PROFIT_BUDGET) AS NET_PROFIT_BUDGET_UPTO,SUM(NET_PROFIT_ACTUAL) AS NET_PROFIT_ACTUAL_UPTO
            FROM LDM WHERE MONTH_SL_NO<=" & DropDownList2.SelectedValue & " AND FISCAL_YEAR='" & currentFiscalYear & "'"
            MC7.Connection = conn
            dr = MC7.ExecuteReader
            If dr.HasRows Then
                dr.Read()

                'ASSIGNING VALUES FOR UPTO THE MONTH BUDGET TEXTBOX
                IPT_BUDGET_UPTO.Text = dr.Item("IPT_BUDGET_UPTO")
                BONUS_PENALTY_BUDGET_UPTO.Text = dr.Item("BONUS_PENALTY_BUDGET_UPTO")
                IC_BUDGET_UPTO.Text = dr.Item("IC_BUDGET_UPTO")
                GS_BUDGET_UPTO.Text = dr.Item("GS_BUDGET_UPTO")
                OR_BUDGET_UPTO.Text = dr.Item("OR_BUDGET_UPTO")
                PWB_BUDGET_UPTO.Text = dr.Item("PWB_BUDGET_UPTO")
                TOTAL_INCOME_BUDGET_UPTO.Text = dr.Item("TOTAL_INCOME_BUDGET_UPTO")
                ACR_DEP_BUDGET_UPTO.Text = dr.Item("ACR_DEP_BUDGET_UPTO")
                RAW_MATERIAL_BUDGET_UPTO.Text = dr.Item("RAW_MATERIAL_BUDGET_UPTO")
                SALARY_WAGES_BUDGET_UPTO.Text = dr.Item("SALARY_WAGES_BUDGET_UPTO")
                STORES_SPARES_BUDGET_UPTO.Text = dr.Item("STORES_SPARES_BUDGET_UPTO")
                POWER_FUEL_BUDGET_UPTO.Text = dr.Item("POWER_FUEL_BUDGET_UPTO")
                REPAIR_MAINTENANCE_BUDGET_UPTO.Text = dr.Item("REPAIR_MAINTENANCE_BUDGET_UPTO")
                FREIGHT_OUT_BUDGET_UPTO.Text = dr.Item("FREIGHT_OUT_BUDGET_UPTO")
                OVERHEAD_EXP_BUDGET_UPTO.Text = dr.Item("OVERHEAD_EXP_BUDGET_UPTO")
                HO_EXP_BUDGET_UPTO.Text = dr.Item("HO_EXP_BUDGET_UPTO")
                IGST_IPT_BUDGET_UPTO.Text = dr.Item("IGST_IPT_BUDGET_UPTO")
                GROSS_EXP_BUDGET_UPTO.Text = dr.Item("GROSS_EXP_BUDGET_UPTO")
                LESS_TRANS_BUDGET_UPTO.Text = dr.Item("LESS_TRANS_BUDGET_UPTO")
                NET_EXP_BUDGET_UPTO.Text = dr.Item("NET_EXP_BUDGET_UPTO")
                GROSS_MARGIN_BUDGET_UPTO.Text = dr.Item("GROSS_MARGIN_BUDGET_UPTO")
                DEPRECIATION_BUDGET_UPTO.Text = dr.Item("DEPRECIATION_BUDGET_UPTO")
                INTEREST_CASH_CREDIT_BUDGET_UPTO.Text = dr.Item("INTEREST_CASH_CREDIT_BUDGET_UPTO")
                NET_PROFIT_BUDGET_UPTO.Text = dr.Item("NET_PROFIT_BUDGET_UPTO")


                'ASSIGNING VALUES FOR UPTO THE MONTH BUDGET VARIABLES
                VAR_IPT_BUDGET_UPTO = dr.Item("IPT_BUDGET_UPTO")
                VAR_BONUS_PENALTY_BUDGET_UPTO = dr.Item("BONUS_PENALTY_BUDGET_UPTO")
                VAR_IC_BUDGET_UPTO = dr.Item("IC_BUDGET_UPTO")
                VAR_GS_BUDGET_UPTO = dr.Item("GS_BUDGET_UPTO")
                VAR_OR_BUDGET_UPTO = dr.Item("OR_BUDGET_UPTO")
                VAR_PWB_BUDGET_UPTO = dr.Item("PWB_BUDGET_UPTO")
                VAR_TOTAL_INCOME_BUDGET_UPTO = dr.Item("TOTAL_INCOME_BUDGET_UPTO")
                VAR_ACR_DEP_BUDGET_UPTO = dr.Item("ACR_DEP_BUDGET_UPTO")
                VAR_RAW_MATERIAL_BUDGET_UPTO = dr.Item("RAW_MATERIAL_BUDGET_UPTO")
                VAR_SALARY_WAGES_BUDGET_UPTO = dr.Item("SALARY_WAGES_BUDGET_UPTO")
                VAR_STORES_SPARES_BUDGET_UPTO = dr.Item("STORES_SPARES_BUDGET_UPTO")
                VAR_POWER_FUEL_BUDGET_UPTO = dr.Item("POWER_FUEL_BUDGET_UPTO")
                VAR_REPAIR_MAINTENANCE_BUDGET_UPTO = dr.Item("REPAIR_MAINTENANCE_BUDGET_UPTO")
                VAR_FREIGHT_OUT_BUDGET_UPTO = dr.Item("FREIGHT_OUT_BUDGET_UPTO")
                VAR_OVERHEAD_EXP_BUDGET_UPTO = dr.Item("OVERHEAD_EXP_BUDGET_UPTO")
                VAR_HO_EXP_BUDGET_UPTO = dr.Item("HO_EXP_BUDGET_UPTO")
                VAR_IGST_IPT_BUDGET_UPTO = dr.Item("IGST_IPT_BUDGET_UPTO")
                VAR_GROSS_EXP_BUDGET_UPTO = dr.Item("GROSS_EXP_BUDGET_UPTO")
                VAR_LESS_TRANS_BUDGET_UPTO = dr.Item("LESS_TRANS_BUDGET_UPTO")
                VAR_NET_EXP_BUDGET_UPTO = dr.Item("NET_EXP_BUDGET_UPTO")
                VAR_GROSS_MARGIN_BUDGET_UPTO = dr.Item("GROSS_MARGIN_BUDGET_UPTO")
                VAR_DEPRECIATION_BUDGET_UPTO = dr.Item("DEPRECIATION_BUDGET_UPTO")
                VAR_INTEREST_CASH_CREDIT_BUDGET_UPTO = dr.Item("INTEREST_CASH_CREDIT_BUDGET_UPTO")
                VAR_NET_PROFIT_BUDGET_UPTO = dr.Item("NET_PROFIT_BUDGET_UPTO")


                'ASSIGNING VALUES FOR UPTO THE MONTH ACTUAL
                IPT_ACTUAL_UPTO.Text = dr.Item("IPT_ACTUAL_UPTO")
                BONUS_PENALTY_ACTUAL_UPTO.Text = dr.Item("BONUS_PENALTY_ACTUAL_UPTO")
                IC_ACTUAL_UPTO.Text = dr.Item("IC_ACTUAL_UPTO")
                GS_ACTUAL_UPTO.Text = dr.Item("GS_ACTUAL_UPTO")
                OR_ACTUAL_UPTO.Text = dr.Item("OR_ACTUAL_UPTO")
                PWB_ACTUAL_UPTO.Text = dr.Item("PWB_ACTUAL_UPTO")
                TOTAL_INCOME_ACTUAL_UPTO.Text = dr.Item("TOTAL_INCOME_ACTUAL_UPTO")
                ACR_DEP_ACTUAL_UPTO.Text = dr.Item("ACR_DEP_ACTUAL_UPTO")
                RAW_MATERIAL_ACTUAL_UPTO.Text = dr.Item("RAW_MATERIAL_ACTUAL_UPTO")
                SALARY_WAGES_ACTUAL_UPTO.Text = dr.Item("SALARY_WAGES_ACTUAL_UPTO")
                STORES_SPARES_ACTUAL_UPTO.Text = dr.Item("STORES_SPARES_ACTUAL_UPTO")
                POWER_FUEL_ACTUAL_UPTO.Text = dr.Item("POWER_FUEL_ACTUAL_UPTO")
                REPAIR_MAINTENANCE_ACTUAL_UPTO.Text = dr.Item("REPAIR_MAINTENANCE_ACTUAL_UPTO")
                FREIGHT_OUT_ACTUAL_UPTO.Text = dr.Item("FREIGHT_OUT_ACTUAL_UPTO")
                OVERHEAD_EXP_ACTUAL_UPTO.Text = dr.Item("OVERHEAD_EXP_ACTUAL_UPTO")
                HO_EXP_ACTUAL_UPTO.Text = dr.Item("HO_EXP_ACTUAL_UPTO")
                IGST_IPT_ACTUAL_UPTO.Text = dr.Item("IGST_IPT_ACTUAL_UPTO")
                GROSS_EXP_ACTUAL_UPTO.Text = dr.Item("GROSS_EXP_ACTUAL_UPTO")
                LESS_TRANS_ACTUAL_UPTO.Text = dr.Item("LESS_TRANS_ACTUAL_UPTO")
                NET_EXP_ACTUAL_UPTO.Text = dr.Item("NET_EXP_ACTUAL_UPTO")
                GROSS_MARGIN_ACTUAL_UPTO.Text = dr.Item("GROSS_MARGIN_ACTUAL_UPTO")
                DEPRECIATION_ACTUAL_UPTO.Text = dr.Item("DEPRECIATION_ACTUAL_UPTO")
                INTEREST_CASH_CREDIT_ACTUAL_UPTO.Text = dr.Item("INTEREST_CASH_CREDIT_ACTUAL_UPTO")
                NET_PROFIT_ACTUAL_UPTO.Text = dr.Item("NET_PROFIT_ACTUAL_UPTO")


                'ASSIGNING VALUES FOR UPTO THE MONTH ACTUAL VARIABLES
                VAR_IPT_ACTUAL_UPTO = dr.Item("IPT_ACTUAL_UPTO")
                VAR_BONUS_PENALTY_ACTUAL_UPTO = dr.Item("BONUS_PENALTY_ACTUAL_UPTO")
                VAR_IC_ACTUAL_UPTO = dr.Item("IC_ACTUAL_UPTO")
                VAR_GS_ACTUAL_UPTO = dr.Item("GS_ACTUAL_UPTO")
                VAR_OR_ACTUAL_UPTO = dr.Item("OR_ACTUAL_UPTO")
                VAR_PWB_ACTUAL_UPTO = dr.Item("PWB_ACTUAL_UPTO")
                VAR_TOTAL_INCOME_ACTUAL_UPTO = dr.Item("TOTAL_INCOME_ACTUAL_UPTO")
                VAR_ACR_DEP_ACTUAL_UPTO = dr.Item("ACR_DEP_ACTUAL_UPTO")
                VAR_RAW_MATERIAL_ACTUAL_UPTO = dr.Item("RAW_MATERIAL_ACTUAL_UPTO")
                VAR_SALARY_WAGES_ACTUAL_UPTO = dr.Item("SALARY_WAGES_ACTUAL_UPTO")
                VAR_STORES_SPARES_ACTUAL_UPTO = dr.Item("STORES_SPARES_ACTUAL_UPTO")
                VAR_POWER_FUEL_ACTUAL_UPTO = dr.Item("POWER_FUEL_ACTUAL_UPTO")
                VAR_REPAIR_MAINTENANCE_ACTUAL_UPTO = dr.Item("REPAIR_MAINTENANCE_ACTUAL_UPTO")
                VAR_FREIGHT_OUT_ACTUAL_UPTO = dr.Item("FREIGHT_OUT_ACTUAL_UPTO")
                VAR_OVERHEAD_EXP_ACTUAL_UPTO = dr.Item("OVERHEAD_EXP_ACTUAL_UPTO")
                VAR_HO_EXP_ACTUAL_UPTO = dr.Item("HO_EXP_ACTUAL_UPTO")
                VAR_IGST_IPT_ACTUAL_UPTO = dr.Item("IGST_IPT_ACTUAL_UPTO")
                VAR_GROSS_EXP_ACTUAL_UPTO = dr.Item("GROSS_EXP_ACTUAL_UPTO")
                VAR_LESS_TRANS_ACTUAL_UPTO = dr.Item("LESS_TRANS_ACTUAL_UPTO")
                VAR_NET_EXP_ACTUAL_UPTO = dr.Item("NET_EXP_ACTUAL_UPTO")
                VAR_GROSS_MARGIN_ACTUAL_UPTO = dr.Item("GROSS_MARGIN_ACTUAL_UPTO")
                VAR_DEPRECIATION_ACTUAL_UPTO = dr.Item("DEPRECIATION_ACTUAL_UPTO")
                VAR_INTEREST_CASH_CREDIT_ACTUAL_UPTO = dr.Item("INTEREST_CASH_CREDIT_ACTUAL_UPTO")
                VAR_NET_PROFIT_ACTUAL_UPTO = dr.Item("NET_PROFIT_ACTUAL_UPTO")
                conn.Close()
            Else
                conn.Close()
            End If
        Catch ee As Exception

            Label2.Visible = True
            Label2.Text = "There was some Error, please contact EDP."
        Finally

        End Try






    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'conn.Open()
        'Dim sqlOrder As New SqlCommand()
        'sqlOrder.Connection = conn
        'sqlOrder.CommandText = "INSERT INTO LDM([IPT_BUDGET], [IPT_ACTUAL], [IPT_CPLY]) VALUES('" & TextBox29.Text & "','" & TextBox30.Text & "','" & TextBox22.Text & "') wherewhere LDM_YEAR = '" & DropDownList1.SelectedValue & "' AND LDM_MONTH = '" & DropDownList2.SelectedValue & "'"
        'conn.Close()
    End Sub


End Class
