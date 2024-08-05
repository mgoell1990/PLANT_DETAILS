Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Public Class Raw_Opening_Correction
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt, dTable As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim working_date As Date = Today.Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim I As Integer = 0
        For I = 0 To GridView6.Rows.Count - 1

            Dim CHA_CRR As String
            CHA_CRR = "CHA" & GridView6.Rows(I).Cells(4).Text
            conn.Open()
            mycommand = New SqlCommand("DELETE from ledger where (GARN_NO_MB_NO='" & GridView6.Rows(I).Cells(4).Text & "' OR GARN_NO_MB_NO='" & CHA_CRR & "') and Journal_ID is not null", conn)
            mycommand.ExecuteNonQuery()
            conn.Close()

            Dim mc1 As New SqlCommand
            Dim po_no, MAT_SLNO, cha_wo, cha_wo_sl, CHA_SUPL_ID, SUPL_ID, GARN_NO, EFFECTIVE_DATE As String
            po_no = GridView6.Rows(I).Cells(1).Text
            MAT_SLNO = GridView6.Rows(I).Cells(3).Text
            GARN_NO = GridView6.Rows(I).Cells(4).Text
            EFFECTIVE_DATE = GridView6.Rows(I).Cells(6).Text
            cha_wo = ""
            cha_wo_sl = ""
            SUPL_ID = ""
            CHA_SUPL_ID = ""
            conn.Open()
            mc1.CommandText = "Select PO_RCD_MAT.SUPL_ID,PO_RCD_MAT .TRANS_WO_NO ,PO_RCD_MAT .TRANS_SLNO ,BE_DETAILS .CHA_ORDER ,BE_DETAILS .CHA_SLNO  from PO_RCD_MAT join BE_DETAILS On PO_RCD_MAT .BE_NO =BE_DETAILS .BE_NO And PO_RCD_MAT .PO_NO =BE_DETAILS .PO_NO And PO_RCD_MAT .MAT_SLNO =BE_DETAILS .MAT_SLNO  where PO_RCD_MAT .CRR_NO  ='" & GridView6.Rows(I).Cells(0).Text & "' AND PO_RCD_MAT.MAT_SLNO=" & GridView6.Rows(I).Cells(3).Text
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                cha_wo = dr.Item("CHA_ORDER")
                cha_wo_sl = dr.Item("CHA_SLNO")
                SUPL_ID = dr.Item("SUPL_ID")
                dr.Close()
            Else
                conn.Close()
            End If
            conn.Close()

            conn.Open()
            mycommand.CommandText = "SELECT PARTY_CODE FROM ORDER_DETAILS WHERE SO_NO ='" & cha_wo & "'"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                CHA_SUPL_ID = dr.Item("PARTY_CODE")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            conn.Close()

            Dim PARTY_QTY As Decimal = 0.0
            Dim w_tolerance As Decimal = 0.0
            Dim w_qty, w_complite, PRICE, final_price, ENTRY_TAX, w_unit_price, W_discount, DISCOUNT_VALUE, CUSTOM_DUTY, IGST, BE_QTY, STATUTORY_CHARGE, INSURANCE As New Decimal(0)

            Dim CHA_VALUE, CHA_RCD, cenvat_value, mat_value, PURCHASE_VALUE, MAT_CUSTOM_DUTY, loss_on_ed_value, wt_var_value, mat_qty As New Decimal(0)
            cenvat_value = 0
            mat_value = 0
            PURCHASE_VALUE = 0
            loss_on_ed_value = 0
            wt_var_value = 0

            CHA_VALUE = 0.0
            CHA_RCD = 0
            ENTRY_TAX = 0

            conn.Open()

            'mc1.CommandText = "select MAT_CHALAN_QTY from PO_RCD_MAT where garn_no='" & GridView6.Rows(I).Cells(4).Text & "'"
            mc1.CommandText = "select Chalan_Qty from foren_temp_correction where Garn_number='" & GridView6.Rows(I).Cells(4).Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                'mat_qty = dr.Item("MAT_CHALAN_QTY")
                mat_qty = dr.Item("Chalan_Qty")
                dr.Close()
            Else
                conn.Close()
            End If
            conn.Close()



            'mat_qty = CDec(GridView6.Rows(I).Cells(10).Text) - (CDec(GridView6.Rows(I).Cells(11).Text) + CDec(GridView6.Rows(I).Cells(13).Text))
            Dim UNIT_PRICE, UNIT_CENVAT As Decimal
            conn.Open()
            mc1.CommandText = "SELECT * FROM BE_DETAILS JOIN PO_ORD_MAT ON BE_DETAILS .PO_NO =PO_ORD_MAT .PO_NO AND BE_DETAILS .MAT_SLNO =PO_ORD_MAT .MAT_SLNO WHERE BE_DETAILS .BE_NO ='" & GridView6.Rows(I).Cells(7).Text & "' AND BE_DETAILS .PO_NO ='" & GridView6.Rows(I).Cells(1).Text & "' AND BE_DETAILS .MAT_SLNO =" & GridView6.Rows(I).Cells(3).Text
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                UNIT_PRICE = dr.Item("UNIT_PRICE")
                UNIT_CENVAT = dr.Item("UNIT_CENVAT")
                CUSTOM_DUTY = dr.Item("TOTAL_CUSTOM_DUTY")
                IGST = dr.Item("IGST")
                BE_QTY = dr.Item("BE_QTY")
                STATUTORY_CHARGE = dr.Item("SAT_CHARGE")
                INSURANCE = dr.Item("INSURANCE")
                dr.Close()
            Else
                conn.Close()
            End If
            conn.Close()
            'unit_value = FormatNumber(UNIT_PRICE, 2)
            mat_value = FormatNumber(((UNIT_PRICE) * CDec(mat_qty)), 2)
            cenvat_value = (UNIT_CENVAT * CDec(mat_qty))

            ''cha calculation
            Dim CHA_PRICE, CHA_DISCOUNT, CHA_FINAL_PRICE As Decimal
            CHA_PRICE = 0
            CHA_DISCOUNT = 0
            CHA_FINAL_PRICE = 0
            Dim cha_name, cha_au As String
            cha_name = ""
            cha_au = ""
            Dim MC As New SqlCommand
            conn.Open()
            MC.CommandText = "select sum(W_UNIT_PRICE) as W_UNIT_PRICE,sum(W_DISCOUNT) as W_DISCOUNT,max(W_NAME) as W_NAME,max(W_AU) as W_AU from WO_ORDER where PO_NO = '" & cha_wo & "' AND W_SLNO='" & cha_wo_sl & "' and AMD_DATE < =(SELECT DISTINCT CHLN_DATE FROM PO_RCD_MAT WHERE CRR_NO='" & GridView6.Rows(I).Cells(0).Text & "')"
            MC.Connection = conn
            dr = MC.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                CHA_PRICE = dr.Item("W_UNIT_PRICE")
                CHA_DISCOUNT = dr.Item("W_DISCOUNT")
                cha_name = dr.Item("W_NAME")
                cha_au = dr.Item("W_AU")
                dr.Close()
            Else
                conn.Close()
            End If
            conn.Close()


            ''GETTING PURCHASE AND SIT HEAD
            Dim PURCHASE As String = ""
            conn.Open()
            Dim MCc As New SqlCommand
            MCc.CommandText = "select AC_PUR from MATERIAL where MAT_CODE = '" & GridView6.Rows(I).Cells(8).Text & "'"
            MCc.Connection = conn
            dr = MCc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                PURCHASE = dr.Item("AC_PUR")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            ''ledger posting material transit
            Dim SIT_HEAD, cenvat_head, PROV_CHA As New String("")
            conn.Open()
            mycommand.CommandText = "select * from work_group where work_type = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & po_no & "')"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                SIT_HEAD = dr.Item("PROV_HEAD")
                cenvat_head = dr.Item("igst")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If
            conn.Close()

            conn.Open()
            mycommand.CommandText = "select * from work_group where work_name = (SELECT PO_TYPE FROM ORDER_DETAILS WHERE SO_NO='" & cha_wo & "') and work_type=(select distinct wo_type from wo_order where po_no='" & cha_wo & "' and w_slno='" & cha_wo_sl & "')"
            mycommand.Connection = conn
            dr = mycommand.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                PROV_CHA = dr.Item("PROV_HEAD")
                dr.Close()
                conn.Close()
            Else
                conn.Close()
            End If

            Dim MAT_CHA_VALUE, MAT_STATUTORY_CHARGE, MAT_INSURANCE_VALUE, MAT_IGST_VALUE As New Decimal(0)

            CHA_DISCOUNT = (CHA_PRICE * CHA_DISCOUNT) / 100
            CHA_FINAL_PRICE = (CHA_PRICE - CHA_DISCOUNT) * CDec(mat_qty)
            CHA_VALUE = FormatNumber(CHA_FINAL_PRICE, 2)
            CHA_RCD = FormatNumber((CHA_PRICE - CHA_DISCOUNT) * CDec(mat_qty), 2)
            MAT_CUSTOM_DUTY = FormatNumber((CUSTOM_DUTY / BE_QTY) * CDec(mat_qty), 2)
            MAT_STATUTORY_CHARGE = FormatNumber((STATUTORY_CHARGE / BE_QTY) * CDec(mat_qty), 2)

            MAT_CHA_VALUE = FormatNumber(CHA_VALUE, 2)
            MAT_IGST_VALUE = FormatNumber((IGST / BE_QTY) * CDec(mat_qty), 2)

            MAT_INSURANCE_VALUE = FormatNumber((INSURANCE / BE_QTY) * CDec(mat_qty), 2)

            PURCHASE_VALUE = mat_value + MAT_CUSTOM_DUTY + MAT_STATUTORY_CHARGE + MAT_CHA_VALUE + MAT_INSURANCE_VALUE



        Next
    End Sub

    Protected Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        conn.Open()
        dt.Clear()

        Dim total_row As Integer



        'da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE BE_NO <> 'N/A' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' ORDER BY GARN_NO", conn)
        da = New SqlDataAdapter("select MAX(LINE_NO) as line_no,MAT_CODE from MAT_DETAILS where LINE_DATE <'2018-04-01' and FISCAL_YEAR=1718 and MAT_CODE like '100%' group by MAT_CODE order by MAT_CODE", conn)
        'da = New SqlDataAdapter("SELECT * FROM PO_RCD_MAT WHERE BE_NO <> 'N/A' AND GARN_DATE BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and GARN_NO <> 'RGARN1819001098' ORDER BY GARN_NO", conn)
        da.Fill(dt)
        total_row = dt.Rows.Count
        conn.Close()
        GridView6.DataSource = dt
        GridView6.DataBind()
    End Sub
End Class