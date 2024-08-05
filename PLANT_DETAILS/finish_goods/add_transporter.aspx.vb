Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class add_transporter
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("FINISHGOODS").ConnectionString)
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
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("despatchAccess")) Or Session("despatchAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        dt.Clear()
        da = New SqlDataAdapter("select D_TYPE  + INV_NO AS INV_NO ,SO_NO,INV_DATE,TRANS_WO,TRANS_SLNO,TRANS_NAME,TRUCK_NO,TOTAL_WEIGHT from DESPATCH where INV_STATUS ='Pending' and TRUCK_NO ='" & TextBox2.Text & "' ORDER BY INV_NO ", conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
        conn.Close()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim STR1 As String = ""
                Dim FLAG As Boolean
                FLAG = False
                If working_date.Month > 3 Then
                    STR1 = working_date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = STR1 & (STR1 + 1)
                ElseIf working_date.Month <= 3 Then
                    STR1 = working_date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = (STR1 - 1) & STR1
                End If
                Dim PROV_PRICE_FOR_TRANSPORTER As Decimal = 0.0
                Dim RCVD_QTY_TRANSPORTER As Decimal = 0.0
                Dim I As Integer = 0
                For Each row As GridViewRow In GridView1.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                        If chkRow.Checked Then
                            RCVD_QTY_TRANSPORTER = RCVD_QTY_TRANSPORTER + CDec(row.Cells(8).Text)
                            FLAG = True
                        End If
                    End If
                Next
                For Each row As GridViewRow In GridView1.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("CheckBox1"), CheckBox)
                        If chkRow.Checked Then
                            ''INSERT grid view transporter charge
                            conn.Open()
                            Dim w_qty, w_complite, w_unit_price, W_discount, mat_price, wct_price As Decimal
                            Dim WO_NAME, WO_AMD, AMD_DATE As New String("")
                            Dim WO_AU As String = ""
                            Dim WO_SUPL_ID As String = ""
                            Dim MCqq As New SqlCommand
                            Dim des_date As Date = Today.Date
                            MCqq.CommandText = "select MAX(WO_AMD) AS WO_AMD ,MAX(AMD_DATE) AS AMD_DATE, sum(W_MATERIAL_COST) as W_MATERIAL_COST,MAX(SUPL_ID) as SUPL_ID, sum(W_QTY) as W_QTY,sum(W_COMPLITED) as W_COMPLITED,sum(W_TOLERANCE) as W_TOLERANCE,sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & row.Cells(4).Text & "' and w_slno=" & row.Cells(5).Text & " and AMD_DATE < ='" & des_date.Year & "-" & des_date.Month & "-" & des_date.Day & "'"
                            MCqq.Connection = conn
                            dr = MCqq.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                w_qty = dr.Item("W_QTY")
                                w_complite = dr.Item("W_COMPLITED")
                                w_unit_price = dr.Item("W_UNIT_PRICE")
                                W_discount = dr.Item("W_DISCOUNT")
                                mat_price = dr.Item("W_MATERIAL_COST")
                                WO_NAME = dr.Item("W_NAME")
                                WO_AU = dr.Item("W_AU")
                                WO_SUPL_ID = dr.Item("SUPL_ID")
                                WO_AMD = dr.Item("WO_AMD")
                                AMD_DATE = dr.Item("AMD_DATE")
                                dr.Close()
                            End If
                            conn.Close()

                            ''insert inv_print

                            Dim QUARY1 As String = "Insert Into INV_PRINT(F_YEAR,INV_NO,PRINT_ORIGN,PRINT_TRANS,PRINT_ASSAE)values(@F_YEAR,@INV_NO,@PRINT_ORIGN,@PRINT_TRANS,@PRINT_ASSAE)"
                            Dim scmd As New SqlCommand(QUARY1, conn_trans, myTrans)
                            scmd.Parameters.AddWithValue("@INV_NO", row.Cells(2).Text)
                            scmd.Parameters.AddWithValue("@PRINT_ORIGN", "ORIGINAL")
                            scmd.Parameters.AddWithValue("@PRINT_TRANS", "DUPLICATE")
                            scmd.Parameters.AddWithValue("@PRINT_ASSAE", "TRIPLICATE")
                            scmd.Parameters.AddWithValue("@F_YEAR", STR1)
                            scmd.ExecuteReader()
                            scmd.Dispose()


                            ''calculate work amount
                            Dim base_value, discount_value, mat_rate, unit_price, total_price As Decimal

                            total_price = 0
                            unit_price = 0
                            base_value = 0
                            discount_value = 0
                            mat_rate = 0
                            base_value = w_unit_price
                            discount_value = (base_value * W_discount) / 100
                            wct_price = (((base_value - discount_value) + mat_rate) * wct_price) / 100
                            PROV_PRICE_FOR_TRANSPORTER = FormatNumber(base_value - discount_value, 2)
                            unit_price = PROV_PRICE_FOR_TRANSPORTER / RCVD_QTY_TRANSPORTER
                            total_price = FormatNumber(unit_price * CDec(row.Cells(8).Text), 2)


                            Dim Query As String = "Insert Into mb_book(unit_price,Entry_Date,mb_no,mb_date,po_no,supl_id,wo_slno,w_name,w_au,from_date,to_date,work_qty,rqd_qty,bal_qty,note,mb_by,ra_no,prov_amt,pen_amt,sgst,cgst,igst,cess,sgst_liab,cgst_liab,igst_liab,cess_liab,it_amt,pay_ind,fiscal_year,mat_rate,mb_clear,amd_no,amd_date,u_price_trailor)VALUES(@unit_price,@Entry_Date,@mb_no,@mb_date,@po_no,@supl_id,@wo_slno,@w_name,@w_au,@from_date,@to_date,@work_qty,@rqd_qty,@bal_qty,@note,@mb_by,@ra_no,@prov_amt,@pen_amt,@sgst,@cgst,@igst,@cess,@sgst_liab,@cgst_liab,@igst_liab,@cess_liab,@it_amt,@pay_ind,@fiscal_year,@mat_rate,@mb_clear,@amd_no,@amd_date,@u_price_trailor)"
                            Dim cmd As New SqlCommand(Query, conn_trans, myTrans)
                            cmd.Parameters.AddWithValue("@mb_no", row.Cells(2).Text)
                            cmd.Parameters.AddWithValue("@mb_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@po_no", row.Cells(4).Text)
                            cmd.Parameters.AddWithValue("@supl_id", WO_SUPL_ID)
                            cmd.Parameters.AddWithValue("@wo_slno", row.Cells(5).Text)
                            cmd.Parameters.AddWithValue("@w_name", WO_NAME)
                            cmd.Parameters.AddWithValue("@w_au", WO_AU)
                            cmd.Parameters.AddWithValue("@from_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@to_date", Date.ParseExact(working_date, "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@work_qty", CDec(row.Cells(8).Text))
                            cmd.Parameters.AddWithValue("@rqd_qty", CDec(row.Cells(8).Text))
                            cmd.Parameters.AddWithValue("@bal_qty", w_complite)
                            cmd.Parameters.AddWithValue("@note", "")
                            cmd.Parameters.AddWithValue("@mb_by", Session("userName"))
                            cmd.Parameters.AddWithValue("@ra_no", "")
                            cmd.Parameters.AddWithValue("@prov_amt", CDec(total_price))
                            cmd.Parameters.AddWithValue("@pen_amt", 0.0)
                            cmd.Parameters.AddWithValue("@sgst", 0)
                            cmd.Parameters.AddWithValue("@cgst", 0)
                            cmd.Parameters.AddWithValue("@igst", 0)
                            cmd.Parameters.AddWithValue("@cess", 0)
                            cmd.Parameters.AddWithValue("@sgst_liab", 0)
                            cmd.Parameters.AddWithValue("@cgst_liab", 0)
                            cmd.Parameters.AddWithValue("@igst_liab", 0)
                            cmd.Parameters.AddWithValue("@cess_liab", 0)
                            cmd.Parameters.AddWithValue("@it_amt", 0)
                            cmd.Parameters.AddWithValue("@pay_ind", "")
                            cmd.Parameters.AddWithValue("@fiscal_year", STR1)
                            cmd.Parameters.AddWithValue("@mat_rate", 0)
                            cmd.Parameters.AddWithValue("@mb_clear", "I.R. CLEAR")
                            cmd.Parameters.AddWithValue("@AMD_NO", WO_AMD)
                            cmd.Parameters.AddWithValue("@AMD_DATE", Date.ParseExact(CDate(AMD_DATE), "dd-MM-yyyy", provider))
                            cmd.Parameters.AddWithValue("@u_price_trailor", unit_price)
                            cmd.Parameters.AddWithValue("@unit_price", w_unit_price)
                            cmd.Parameters.AddWithValue("@Entry_Date", Now)
                            cmd.ExecuteReader()
                            cmd.Dispose()



                            ''SEARCH AC HEAD
                            conn.Open()
                            Dim TRANS_PROV, TRANS_PUR As String
                            TRANS_PROV = ""
                            TRANS_PUR = ""
                            Dim MC5 As New SqlCommand
                            MC5.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & row.Cells(4).Text & "') and work_type=(select distinct wo_type from wo_order where po_no='" & row.Cells(4).Text & "' and w_slno='" & row.Cells(5).Text & "')"
                            MC5.Connection = conn
                            dr = MC5.ExecuteReader
                            If dr.HasRows Then
                                dr.Read()
                                TRANS_PROV = dr.Item("PROV_HEAD")
                                TRANS_PUR = dr.Item("PUR_HEAD")
                                dr.Close()
                                conn.Close()
                            Else
                                conn.Close()
                            End If
                            ''INSERT TRANSPORTER LEDGER PROV AND PUR
                            save_ledger(row.Cells(4).Text, row.Cells(2).Text, WO_SUPL_ID, TRANS_PUR, "Dr", total_price, "PUR")
                            save_ledger(row.Cells(4).Text, row.Cells(2).Text, WO_SUPL_ID, TRANS_PROV, "Cr", total_price, "PROV")
                            ''UPDATE TRANSPORTER

                            If FLAG = True Then

                                QUARY1 = "update WO_ORDER set W_COMPLITED =W_COMPLITED + 1 where PO_NO ='" & row.Cells(4).Text & "' AND W_SLNO= '" & row.Cells(5).Text & "'"
                                Dim TRANS As New SqlCommand(QUARY1, conn_trans, myTrans)
                                TRANS.ExecuteReader()
                                TRANS.Dispose()

                                FLAG = False
                            End If


                            ''UPDATE DESPATCH
                            QUARY1 = "update DESPATCH set INV_STATUS ='ACTIVE' where D_TYPE + CONVERT(VARCHAR(15), INV_NO)  ='" & row.Cells(2).Text & "'"
                            Dim despatch As New SqlCommand(QUARY1, conn_trans, myTrans)
                            despatch.ExecuteReader()
                            despatch.Dispose()

                        End If
                    End If
                Next
                'CLEAR
                conn.Open()
                dt.Clear()
                da = New SqlDataAdapter("select * from DESPATCH WITH(NOLOCK) where INV_STATUS ='Pending' and TRUCK_NO ='" & TextBox2.Text & "' ORDER BY INV_NO ", conn)
                da.Fill(dt)
                conn.Close()
                GridView1.DataSource = dt
                GridView1.DataBind()
                conn.Close()

                myTrans.Commit()
                Label3.Text = "Trailor updated successfully. Please create invoice."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label3.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub
    Protected Sub save_ledger(so_no As String, inv_no As String, dt_id As String, ac_head As String, ac_term As String, price As Decimal, post_ind As String)
        Dim working_date As Date

        working_date = Today.Date
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
            conn.Open()
            Dim cmd As New SqlCommand
            Dim Query As String = "Insert Into LEDGER(PO_NO,GARN_NO_MB_NO,SUPL_ID,FISCAL_YEAR,PERIOD,EFECTIVE_DATE,ENTRY_DATE,AC_NO,AMOUNT_DR,AMOUNT_CR,POST_INDICATION,PAYMENT_INDICATION)VALUES(@PO_NO,@GARN_NO_MB_NO,@SUPL_ID,@FISCAL_YEAR,@PERIOD,@EFECTIVE_DATE,@ENTRY_DATE,@AC_NO,@AMOUNT_DR,@AMOUNT_CR,@POST_INDICATION,@PAYMENT_INDICATION)"
            cmd = New SqlCommand(Query, conn)
            cmd.Parameters.AddWithValue("@PO_NO", so_no)
            cmd.Parameters.AddWithValue("@GARN_NO_MB_NO", inv_no)
            cmd.Parameters.AddWithValue("@SUPL_ID", dt_id)
            cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
            cmd.Parameters.AddWithValue("@PERIOD", qtr1)
            cmd.Parameters.AddWithValue("@EFECTIVE_DATE", working_date)
            cmd.Parameters.AddWithValue("@ENTRY_DATE", Now)
            cmd.Parameters.AddWithValue("@AC_NO", ac_head)
            cmd.Parameters.AddWithValue("@AMOUNT_DR", dr_value)
            cmd.Parameters.AddWithValue("@AMOUNT_CR", cr_value)
            cmd.Parameters.AddWithValue("@POST_INDICATION", post_ind)
            cmd.Parameters.AddWithValue("@PAYMENT_INDICATION", "")
            cmd.ExecuteReader()
            cmd.Dispose()
            conn.Close()
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class