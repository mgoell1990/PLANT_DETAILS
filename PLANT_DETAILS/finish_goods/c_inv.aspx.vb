Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class c_inv
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
            'If Session("userName") = "" Then
            '    Response.Redirect("~/Account/Login")
            '    Return
            'End If
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            TextBox1.Focus()
            Return
        ElseIf TextBox2.Text = "" Or IsNumeric(TextBox2.Text) = False Then
            TextBox2.Text = ""
            TextBox2.Focus()
        End If
        Dim inv_no As String = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select (case when max(DESPATCH.d_type+DESPATCH.inv_no) is null then 'Invoice No Cannot Be Cancelled' else max(DESPATCH.d_type+DESPATCH.inv_no) end) as inv_st from" & _
        " DESPATCH join INV_PRINT on DESPATCH.D_TYPE +DESPATCH.INV_NO =INV_PRINT .INV_NO  WHERE " & _
        " DESPATCH.D_TYPE+DESPATCH.INV_NO ='" & TextBox1.Text & "' AND " & _
        " DESPATCH.FISCAL_YEAR ='" & TextBox2.Text & "' and" & _
        " DESPATCH.INV_STATUS ='' and " & _
        " INV_PRINT .PRINT_ORIGN <>'' and" & _
        " INV_PRINT .PRINT_TRANS <>'' and" & _
        " INV_PRINT .PRINT_ASSAE <>''"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            inv_no = dr.Item("inv_st")
            dr.Close()
        End If
        conn.Close()

        If inv_no = "Invoice No Cannot Be Cancelled" Then
            Label4.Text = inv_no
            Return
        Else
            Label4.Text = "Invoice No Cancelled"
        End If


        Dim mycommand1 As New SqlCommand
        Dim mycommand2 As New SqlCommand
        Dim mycommand3 As New SqlCommand
        Dim mycommand4 As New SqlCommand
        Dim mycommand5 As New SqlCommand
        Dim mycommand6 As New SqlCommand
        Dim mycommand7 As New SqlCommand
        Dim mycommand8 As New SqlCommand
        conn.Open()
        mycommand1 = New SqlCommand("update SO_MAT_ORDER set " & _
                                    " so_mat_order.ITEM_QTY_SEND =(SO_MAT_ORDER .ITEM_QTY_SEND) - PROD_CONTROL .ITEM_I_QTY" & _
                                    " from SO_MAT_ORDER ,DESPATCH ,PROD_CONTROL " & _
                                    " where DESPATCH .D_TYPE +DESPATCH .INV_NO =PROD_CONTROL .INV_NO and" & _
                                    " DESPATCH .SO_NO =SO_MAT_ORDER . SO_NO and" & _
                                    " DESPATCH .MAT_SLNO  =SO_MAT_ORDER . ITEM_SLNO" & _
                                    " DESPATCH .FISCAL_YEAR  =PROD_CONTROL.FISCAL_YEAR" & _
                                    " and PROD_CONTROL .INV_NO ='" & inv_no & "'" & _
                                    " and DESPATCH .FISCAL_YEAR ='" & TextBox2.Text & "' and SO_MAT_ORDER.AMD_NO=DESPATCH.AMD_NO", conn)

        mycommand2 = New SqlCommand("update DESPATCH set INV_STATUS ='CANCELLED' WHERE D_TYPE+INV_NO ='" & inv_no & "' and FISCAL_YEAR ='" & TextBox2.Text & "'", conn)

        mycommand4 = New SqlCommand("UPDATE PROD_CONTROL SET " & _
                                    " PROD_CONTROL.ITEM_I_QTY=0" & _
                                    " from PROD_CONTROL ,DESPATCH " & _
                                    " WHERE DESPATCH .D_TYPE +DESPATCH .INV_NO =PROD_CONTROL.INV_NO and" & _
                                    " DESPATCH .FISCAL_YEAR ='" & TextBox2.Text & "'  and" & _
                                    " PROD_CONTROL.FISCAL_YEAR ='" & TextBox2.Text & "'  and" & _
                                    " PROD_CONTROL.INV_NO ='" & inv_no & "'", conn)

        mycommand3 = New SqlCommand("UPDATE F_ITEM SET F_ITEM.ITEM_B_STOCK =F_ITEM.ITEM_B_STOCK + PROD_CONTROL .ITEM_I_QTY " & _
                                    " FROM F_ITEM ,PROD_CONTROL ,DESPATCH " & _
                                    " WHERE F_ITEM .ITEM_CODE =PROD_CONTROL .ITEM_CODE AND" & _
                                    " DESPATCH .D_TYPE +DESPATCH .INV_NO =PROD_CONTROL.INV_NO and" & _
                                    " DESPATCH .FISCAL_YEAR ='" & TextBox2.Text & "' and " & _
                                    " PROD_CONTROL.FISCAL_YEAR ='" & TextBox2.Text & "'  and" & _
                                    " PROD_CONTROL .INV_NO ='" & inv_no & "'", conn)

        mycommand5 = New SqlCommand("UPDATE WO_ORDER SET WO_ORDER.W_COMPLITED =WO_ORDER .W_COMPLITED -DESPATCH .TOTAL_WEIGHT" & _
                                    " FROM WO_ORDER ,DESPATCH " & _
                                    " WHERE WO_ORDER .PO_NO =DESPATCH .TRANS_WO AND WO_ORDER .W_SLNO =DESPATCH .TRANS_SLNO and " & _
                                    " DESPATCH .FISCAL_YEAR ='" & TextBox2.Text & "' and " & _
                                    " DESPATCH .D_TYPE +DESPATCH .INV_NO ='" & inv_no & "'", conn)

        mycommand6 = New SqlCommand("delete from mb_book where mb_no ='" & inv_no & "' and fiscal_year ='" & TextBox2.Text & "'", conn)

        mycommand7 = New SqlCommand("delete from LEDGER where GARN_NO_MB_NO ='" & inv_no & "' and FISCAL_YEAR ='" & TextBox2.Text & "'", conn)



        mycommand1.ExecuteNonQuery()
        mycommand1.Dispose()

        mycommand2.ExecuteNonQuery()
        mycommand2.Dispose()

        mycommand3.ExecuteNonQuery()
        mycommand3.Dispose()

        mycommand4.ExecuteNonQuery()
        mycommand4.Dispose()

        mycommand5.ExecuteNonQuery()
        mycommand5.Dispose()

        mycommand6.ExecuteNonQuery()
        mycommand6.Dispose()

        mycommand7.ExecuteNonQuery()
        mycommand7.Dispose()

        conn.Close()
    End Sub

End Class