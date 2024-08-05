Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.IO
Imports ClosedXML.Excel

Public Class fg_report
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

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim ds5 As New DataSet
        'conn.Open()
        'dt.Clear()
        'da = New SqlDataAdapter("select ITEM_CODE ,ITEM_NAME from F_ITEM where item_au <> 'Activity' order by item_code", conn)
        'da.Fill(dt)
        'conn.Close()
        'GridView_fdata.DataSource = dt
        'GridView_fdata.DataBind()
        'count = 0
        'Dim DATE_FROM, DATE_TO As Date
        'DATE_FROM = CDate(date1.Text)
        'DATE_TO = CDate(date2.Text)
        'For Me.count = 0 To GridView_fdata.Rows.Count - 1
        '    conn.Open()
        '    Dim o_f_stock, o_b_stock, p_f_qty, t_b_qty, sale_qty, sale_mt, s_value, cl_f_qty, cl_b_qty, sgst_amt, cgst_amt, igst_amt, cess_amt As Decimal
        '    o_f_stock = 0.0
        '    o_b_stock = 0.0
        '    p_f_qty = 0.0
        '    t_b_qty = 0.0
        '    sale_qty = 0.0
        '    cl_b_qty = 0.0
        '    cl_f_qty = 0.0
        '    sale_mt = 0.0
        '    s_value = 0.0
        '    sgst_amt = 0.0
        '    cgst_amt = 0.0
        '    igst_amt = 0.0
        '    cess_amt = 0.0
        '    Dim mc As New SqlCommand
        '    mc.CommandText = "select " &
        '    "(select (case when SUM(ITEM_F_QTY ) is null then '0.00' else  SUM(ITEM_F_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_CONTROL.PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "')+ (SELECT MAX(ITEM_OPEN_F_STOCK) from F_ITEM where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "') - " &
        '    " (select (case when SUM(ITEM_B_QTY ) is null then '0.00' else  SUM(ITEM_B_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "') as op_f_qty, " &
        '    "(select (case when SUM(ITEM_B_QTY ) is null then '0.00' else  SUM(ITEM_B_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_CONTROL.PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "')+ (select  max(ITEM_OPEN_B_STOCK)  from F_ITEM  where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "') - " &
        '    " (select (case when SUM(ITEM_I_QTY ) is null then '0.00' else  SUM(ITEM_I_QTY) end) from PROD_CONTROL where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "') as op_b_qty, " &
        '    "(select (case when SUM(item_f_qty) is null then '0.00' else  SUM(item_f_qty) end) from PROD_CONTROL  where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_CONTROL.PROD_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') as prod_f_qty, " &
        '    "(select (case when SUM(ITEM_B_QTY ) is null then '0.00' else  SUM(ITEM_B_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') as b_trans, " &
        '    "(select (case when SUM(ITEM_I_QTY ) is null then '0.00' else  SUM(ITEM_I_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') as SALE, " &
        '    "((((select (case when SUM(ITEM_F_QTY ) is null then '0.00' else  SUM(ITEM_F_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_CONTROL.PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "')+ (SELECT MAX(ITEM_OPEN_F_STOCK) from F_ITEM where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "') - " &
        '    "(select (case when SUM(ITEM_B_QTY ) is null then '0.00' else  SUM(ITEM_B_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "'))+ " &
        '    "(select (case when SUM(item_f_qty) is null then '0.00' else  SUM(item_f_qty) end) from PROD_CONTROL  where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_CONTROL.PROD_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "'))- " &
        '    "(select (case when SUM(ITEM_B_QTY ) is null then '0.00' else  SUM(ITEM_B_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "')) as f_qty, " &
        '    "((((select (case when SUM(ITEM_B_QTY ) is null then '0.00' else  SUM(ITEM_B_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_CONTROL.PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "')+ (select  max(ITEM_OPEN_B_STOCK)  from F_ITEM  where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "') - " &
        '    "(select (case when SUM(ITEM_I_QTY ) is null then '0.00' else  SUM(ITEM_I_QTY) end) from PROD_CONTROL where ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE <'" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "')) + " &
        '    "(select (case when SUM(ITEM_B_QTY ) is null then '0.00' else  SUM(ITEM_B_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') )- " &
        '    "(select (case when SUM(ITEM_I_QTY ) is null then '0.00' else  SUM(ITEM_I_QTY) end) from PROD_CONTROL where PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and PROD_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "')) b_qty , " &
        '    "(select (case when SUM(total_weight) is null then '0.000' else SUM(total_weight) end) from DESPATCH join PROD_CONTROL on DESPATCH .D_TYPE +DESPATCH .INV_NO  =PROD_CONTROL .INV_NO  where PROD_CONTROL.ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' and DESPATCH .INV_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "' and INV_STATUS <> 'cancelled') as sale_mt , " &
        '    "(select (case when  SUM(TAX_DESC .total_amount) is null then '0.000' else SUM(TAX_DESC .total_amount) end)  from TAX_DESC join DESPATCH on TAX_DESC .inv_no =DESPATCH .INV_NO and TAX_DESC .D_TYPE =DESPATCH .D_TYPE and TAX_DESC .FISCAL_YEAR =DESPATCH .FISCAL_YEAR JOIN PROD_CONTROL ON (DESPATCH .D_TYPE +DESPATCH .INV_NO )=PROD_CONTROL .INV_NO  " &
        '     " where (TAX_DESC.tax_slno =2 or TAX_DESC.tax_slno =4) and DESPATCH .INV_DATE between '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' and '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "' and DESPATCH .INV_STATUS <> 'cancelled' AND PROD_CONTROL .ITEM_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' ) AS s_value ," &
        '    "(SELECT (case when  SUM(DESPATCH .SGST_AMT ) is null then '0.000' else SUM(DESPATCH .SGST_AMT ) end) FROM DESPATCH WHERE DESPATCH .INV_STATUS <> 'CANCELLED' AND DESPATCH .P_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' AND  DESPATCH.INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') AS sgst_amt,  " &
        '    "(SELECT (case when  SUM(DESPATCH .CGST_AMT ) is null then '0.000' else SUM(DESPATCH .CGST_AMT ) end) FROM DESPATCH WHERE DESPATCH .INV_STATUS <> 'CANCELLED' AND DESPATCH .P_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' AND  DESPATCH.INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') AS cgst_amt,  " &
        '    "(SELECT (case when  SUM(DESPATCH .IGST_AMT ) is null then '0.000' else SUM(DESPATCH .IGST_AMT ) end) FROM DESPATCH WHERE DESPATCH .INV_STATUS <> 'CANCELLED' AND DESPATCH .P_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' AND  DESPATCH.INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') AS igst_amt,  " &
        '    "(SELECT (case when  SUM(DESPATCH .CESS_AMT ) is null then '0.000' else SUM(DESPATCH .CESS_AMT ) end) FROM DESPATCH WHERE DESPATCH .INV_STATUS <> 'CANCELLED' AND DESPATCH .P_CODE ='" & GridView_fdata.Rows(count).Cells(1).Text & "' AND  DESPATCH.INV_DATE BETWEEN '" & DATE_FROM.Year & "-" & DATE_FROM.Month & "-" & DATE_FROM.Day & "' AND '" & DATE_TO.Year & "-" & DATE_TO.Month & "-" & DATE_TO.Day & "') AS cess_amt  "

        '    mc.Connection = conn
        '    dr = mc.ExecuteReader
        '    If dr.HasRows Then
        '        dr.Read()
        '        o_f_stock = dr.Item("op_f_qty")
        '        o_b_stock = dr.Item("op_b_qty")
        '        p_f_qty = dr.Item("prod_f_qty")
        '        t_b_qty = dr.Item("b_trans")
        '        sale_qty = dr.Item("SALE")
        '        cl_f_qty = dr.Item("f_qty")
        '        cl_b_qty = dr.Item("b_qty")
        '        sale_mt = dr.Item("sale_mt")
        '        s_value = dr.Item("s_value")
        '        sgst_amt = dr.Item("sgst_amt")
        '        cgst_amt = dr.Item("cgst_amt")
        '        igst_amt = dr.Item("igst_amt")
        '        cess_amt = dr.Item("cess_amt")
        '        dr.Close()
        '    Else
        '        conn.Close()
        '    End If
        '    conn.Close()
        '    GridView_fdata.Rows(count).Cells(0).Text = count + 1
        '    GridView_fdata.Rows(count).Cells(3).Text = o_f_stock
        '    GridView_fdata.Rows(count).Cells(4).Text = o_b_stock
        '    GridView_fdata.Rows(count).Cells(5).Text = p_f_qty
        '    GridView_fdata.Rows(count).Cells(6).Text = t_b_qty
        '    GridView_fdata.Rows(count).Cells(7).Text = sale_qty
        '    GridView_fdata.Rows(count).Cells(8).Text = sale_mt
        '    GridView_fdata.Rows(count).Cells(9).Text = s_value
        '    GridView_fdata.Rows(count).Cells(10).Text = sgst_amt
        '    GridView_fdata.Rows(count).Cells(11).Text = cgst_amt
        '    GridView_fdata.Rows(count).Cells(12).Text = igst_amt
        '    GridView_fdata.Rows(count).Cells(13).Text = cess_amt
        '    GridView_fdata.Rows(count).Cells(14).Text = cl_f_qty
        '    GridView_fdata.Rows(count).Cells(15).Text = cl_b_qty
        'Next

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If IsDate(date1.Text) = False Then
            date1.Text = ""
            date1.Focus()
            Return
        ElseIf IsDate(date2.Text) = False Then
            date2.Text = ""
            date2.Focus()
            Return
        End If

        Dim from_date, to_date As Date
        from_date = CDate(date1.Text)
        to_date = CDate(date2.Text)

        Dim STR1 As String = ""
        If from_date.Month > 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = STR1 & (STR1 + 1)
        ElseIf from_date.Month <= 3 Then
            STR1 = from_date.Year
            STR1 = STR1.Trim.Substring(2)
            STR1 = (STR1 - 1) & STR1
        End If

        conn.Open()
        dt.Clear()

        'Dim quary As String = "DECLARE @TT TABLE(ITEM_CODE VARCHAR(30),ITEM_NAME VARCHAR(30),ITEM_OPEN_F_STOCK DECIMAL(16,3),ITEM_OPEN_B_STOCK DECIMAL(16,3),PROD_F_QTY DECIMAL(16,3),TRANS_B_QTY DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_MT DECIMAL(16,3),SALES_VALUE DECIMAL(16,3),SGST_AMT DECIMAL(16,3),CGST_AMT DECIMAL(16,3),IGST_AMT DECIMAL(16,3),CESS_AMT DECIMAL(16,3))
        '    INSERT INTO @TT
        '    select ITEM_CODE ,ITEM_NAME, ITEM_OPEN_F_STOCK, ITEM_OPEN_B_STOCK,'0.00' AS PROD_F_QTY, '0.00' AS TRANS_B_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT from F_ITEM where item_au <> 'Activity' order by item_code
        '    INSERT INTO @TT
        '    select F_ITEM.ITEM_CODE,ITEM_NAME,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end)-(case when SUM(ITEM_B_QTY ) is null then '0.00' else SUM(ITEM_B_QTY) end) as ITEM_OPEN_F_STOCK,
        '    (case when SUM(ITEM_B_QTY ) is null then '0.00' else SUM(ITEM_B_QTY) end)-(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as ITEM_OPEN_B_STOCK, 
        '    '0.00' AS PROD_F_QTY, '0.00' AS TRANS_B_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        '    from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE <'" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and item_au <> 'Activity' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        '    INSERT INTO @TT
        '    select F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_F_STOCK, '0.00' AS ITEM_OPEN_B_STOCK,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as PROD_F_QTY,
        '    (case when SUM(ITEM_B_QTY ) is null then '0.00' else SUM(ITEM_B_QTY) end) as TRANS_B_QTY,
        '    (case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as SALES_QTY,
        '    '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        '    from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        '    INSERT INTO @TT
        '    select ITEM_CODE, P_DESC AS ITEM_NAME,'0.00' AS ITEM_OPEN_F_STOCK, '0.00' AS ITEM_OPEN_B_STOCK,'0.00' as PROD_F_QTY, '0.00' as TRANS_B_QTY,'0.00' as SALES_QTY, 
        '    (case when SUM(total_weight) is null then '0.000' else SUM(total_weight) end) AS SALES_MT, (case when SUM(TAXABLE_VALUE) is null then '0.000' else SUM(TAXABLE_VALUE) end) AS SALES_VALUE,
        '    (case when SUM(SGST_AMT) is null then '0.000' else SUM(SGST_AMT) end) AS SGST_AMT, (case when SUM(CGST_AMT) is null then '0.000' else SUM(CGST_AMT) end) AS CGST_AMT,
        '    (case when SUM(IGST_AMT) is null then '0.000' else SUM(IGST_AMT) end) AS IGST_AMT, (case when SUM(CESS_AMT) is null then '0.000' else SUM(CESS_AMT) end) AS CESS_AMT
        '    from PROD_CONTROL join DESPATCH on DESPATCH .D_TYPE +DESPATCH .INV_NO =PROD_CONTROL.INV_NO and DESPATCH.FISCAL_YEAR=PROD_CONTROL.FISCAL_YEAR where DESPATCH .INV_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and INV_STATUS <> 'cancelled' GROUP BY ITEM_CODE, P_DESC order by ITEM_CODE

        '    SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo,ITEM_CODE, ITEM_NAME,SUM(ITEM_OPEN_F_STOCK) AS ITEM_OPEN_F_STOCK,SUM(ITEM_OPEN_B_STOCK) AS ITEM_OPEN_B_STOCK,SUM(PROD_F_QTY) AS PROD_F_QTY,SUM(TRANS_B_QTY) AS TRANS_B_QTY,SUM(SALES_QTY) AS SALES_QTY,SUM(SALES_MT) AS SALES_MT,SUM(SALES_VALUE) AS SALES_VALUE,SUM(SGST_AMT) AS SGST_AMT,
        '    SUM(CGST_AMT) AS CGST_AMT,SUM(IGST_AMT) AS IGST_AMT,SUM(CESS_AMT) AS CESS_AMT,(SUM(ITEM_OPEN_F_STOCK)+SUM(PROD_F_QTY)-SUM(TRANS_B_QTY)) AS CLOSING_F_STOCK,(SUM(ITEM_OPEN_B_STOCK)+SUM(TRANS_B_QTY)-SUM(SALES_QTY)) AS CLOSING_B_STOCK FROM @TT GROUP BY ITEM_CODE, ITEM_NAME ORDER BY ITEM_CODE"

        '     Dim quary As String = "DECLARE @TT TABLE(ITEM_CODE VARCHAR(30),ITEM_NAME VARCHAR(30),ITEM_OPEN_STOCK DECIMAL(16,3),PROD_F_QTY DECIMAL(16,3),TRANSFER_QTY DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_MT DECIMAL(16,3),SALES_VALUE DECIMAL(16,3),SGST_AMT DECIMAL(16,3),CGST_AMT DECIMAL(16,3),IGST_AMT DECIMAL(16,3),CESS_AMT DECIMAL(16,3))
        '         INSERT INTO @TT
        '         select ITEM_CODE ,ITEM_NAME, (ITEM_OPEN_F_STOCK + ITEM_OPEN_B_STOCK) As ITEM_OPEN_STOCK,'0.00' AS PROD_F_QTY, '0.00' AS TRANSFER_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT from F_ITEM where item_au <> 'Activity' order by item_code

        '         INSERT INTO @TT
        '         select F_ITEM.ITEM_CODE,ITEM_NAME,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end)-(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as ITEM_OPEN_STOCK, 
        '         '0.00' AS PROD_F_QTY, '0.00' AS TRANSFER_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        '         from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE <'" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and item_au <> 'Activity' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        '         INSERT INTO @TT
        '         select F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as PROD_F_QTY,
        '         '0.00' as TRANSFER_QTY,(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as SALES_QTY,
        '         '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        '         from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO<>'Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        'INSERT INTO @TT
        '         select F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,'0.00' as PROD_F_QTY,
        '         (case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as TRANSFER_QTY,'0.00' as SALES_QTY,'0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        '         from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO='Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        '         INSERT INTO @TT
        '         select ITEM_CODE, P_DESC AS ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK, '0.00' as PROD_F_QTY, '0.00' as TRANSFER_QTY,'0.00' as SALES_QTY, 
        '         (case when SUM(total_weight) is null then '0.000' else SUM(total_weight) end) AS SALES_MT, (case when SUM(TAXABLE_VALUE) is null then '0.000' else SUM(TAXABLE_VALUE) end) AS SALES_VALUE,
        '         (case when SUM(SGST_AMT) is null then '0.000' else SUM(SGST_AMT) end) AS SGST_AMT, (case when SUM(CGST_AMT) is null then '0.000' else SUM(CGST_AMT) end) AS CGST_AMT,
        '         (case when SUM(IGST_AMT) is null then '0.000' else SUM(IGST_AMT) end) AS IGST_AMT, (case when SUM(CESS_AMT) is null then '0.000' else SUM(CESS_AMT) end) AS CESS_AMT
        '         from PROD_CONTROL join DESPATCH on DESPATCH .D_TYPE +DESPATCH .INV_NO =PROD_CONTROL.INV_NO and DESPATCH.FISCAL_YEAR=PROD_CONTROL.FISCAL_YEAR where DESPATCH .INV_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and INV_STATUS <> 'cancelled' GROUP BY ITEM_CODE, P_DESC order by ITEM_CODE

        '         SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo,ITEM_CODE, ITEM_NAME,SUM(ITEM_OPEN_STOCK) AS ITEM_OPEN_STOCK,SUM(PROD_F_QTY) AS PROD_F_QTY,SUM(TRANSFER_QTY) AS TRANSFER_QTY,SUM(SALES_QTY) AS SALES_QTY,SUM(SALES_MT) AS SALES_MT,SUM(SALES_VALUE) AS SALES_VALUE,SUM(SGST_AMT) AS SGST_AMT,
        '         SUM(CGST_AMT) AS CGST_AMT,SUM(IGST_AMT) AS IGST_AMT,SUM(CESS_AMT) AS CESS_AMT,(SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) AS CLOSING_F_STOCK FROM @TT GROUP BY ITEM_CODE, ITEM_NAME ORDER BY ITEM_CODE"

        Dim quary As String = "DECLARE @TT TABLE(ITEM_CODE VARCHAR(30),ITEM_NAME VARCHAR(60),ITEM_OPEN_STOCK DECIMAL(16,3),PROD_F_QTY DECIMAL(16,3),TRANSFER_QTY DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_MT DECIMAL(16,3),SALES_VALUE DECIMAL(16,3),SGST_AMT DECIMAL(16,3),CGST_AMT DECIMAL(16,3),IGST_AMT DECIMAL(16,3),CESS_AMT DECIMAL(16,3))
            INSERT INTO @TT
            select ITEM_CODE ,ITEM_NAME, (ITEM_OPEN_F_STOCK + ITEM_OPEN_B_STOCK) As ITEM_OPEN_STOCK,'0.00' AS PROD_F_QTY, '0.00' AS TRANSFER_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT from F_ITEM where item_au <> 'Activity' order by item_code

            INSERT INTO @TT
            select F_ITEM.ITEM_CODE,ITEM_NAME,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end)-(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as ITEM_OPEN_STOCK, 
            '0.00' AS PROD_F_QTY, '0.00' AS TRANSFER_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
            from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE <'" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and item_au <> 'Activity' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

            INSERT INTO @TT
            select F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as PROD_F_QTY,
            '0.00' as TRANSFER_QTY,(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as SALES_QTY,
            '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
            from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO<>'Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

			INSERT INTO @TT
            select F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,'0.00' as PROD_F_QTY,
            (case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as TRANSFER_QTY,'0.00' as SALES_QTY,'0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
            from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO='Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

            INSERT INTO @TT
            select ITEM_CODE, P_DESC AS ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK, '0.00' as PROD_F_QTY, '0.00' as TRANSFER_QTY,'0.00' as SALES_QTY, 
            (case when SUM(total_weight) is null then '0.000' else SUM(total_weight) end) AS SALES_MT, (case when SUM(TAXABLE_VALUE) is null then '0.000' else SUM(TAXABLE_VALUE) end) AS SALES_VALUE,
            (case when SUM(SGST_AMT) is null then '0.000' else SUM(SGST_AMT) end) AS SGST_AMT, (case when SUM(CGST_AMT) is null then '0.000' else SUM(CGST_AMT) end) AS CGST_AMT,
            (case when SUM(IGST_AMT) is null then '0.000' else SUM(IGST_AMT) end) AS IGST_AMT, (case when SUM(CESS_AMT) is null then '0.000' else SUM(CESS_AMT) end) AS CESS_AMT
            from PROD_CONTROL join DESPATCH on DESPATCH .D_TYPE +DESPATCH .INV_NO =PROD_CONTROL.INV_NO and DESPATCH.FISCAL_YEAR=PROD_CONTROL.FISCAL_YEAR where DESPATCH .INV_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and INV_STATUS <> 'cancelled' GROUP BY ITEM_CODE, P_DESC order by ITEM_CODE

             SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo,t1.ITEM_CODE, t1.ITEM_NAME,SUM(ITEM_OPEN_STOCK) AS ITEM_OPEN_STOCK,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(ITEM_OPEN_STOCK)*max(f1.ITEM_WEIGHT))/1000 ELSE SUM(ITEM_OPEN_STOCK) END) As open_mt,SUM(PROD_F_QTY) AS PROD_F_QTY,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(PROD_F_QTY) * max(f1.ITEM_WEIGHT))/1000 ELSE SUM(PROD_F_QTY) END) As prod_mt,SUM(TRANSFER_QTY) AS TRANSFER_QTY,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(TRANSFER_QTY) * max(f1.ITEM_WEIGHT))/1000 ELSE SUM(TRANSFER_QTY) END) As trans_mt,SUM(SALES_QTY) AS SALES_QTY,SUM(SALES_MT) AS SALES_MT,SUM(SALES_VALUE) AS SALES_VALUE,SUM(SGST_AMT) AS SGST_AMT,
            SUM(CGST_AMT) AS CGST_AMT,SUM(IGST_AMT) AS IGST_AMT,SUM(CESS_AMT) AS CESS_AMT,(SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) AS CLOSING_F_STOCK,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN ((SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) * max(f1.ITEM_WEIGHT))/1000 ELSE (SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) END) As closing_mt,qual_desc FROM @TT t1 join F_ITEM f1 on t1.ITEM_CODE=f1.ITEM_CODE
            JOIN qual_group q1 on f1.ITEM_TYPE=q1.qual_code where qual_desc <> 'REJ. SILICA' GROUP BY t1.ITEM_CODE, t1.ITEM_NAME,qual_desc ORDER BY ITEM_CODE"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView_fdata.DataSource = dt
        GridView_fdata.DataBind()


        ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'ExportExcel("Stock_Report", GridView_fdata)
        Dim dt3 As DataTable = New DataTable("Stock")
        dt3.Columns.Add(New DataColumn("Sl. No", GetType(String)))
        dt3.Columns.Add(New DataColumn("Item Code", GetType(String)))
        dt3.Columns.Add(New DataColumn("Item Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Opening Stock", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Opening Stock(Mt)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Production Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Production(Mt)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Transfer Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Transfer(Mt)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Sale(Qty)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Sale(Mt)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Sales Value", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("SGST", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("CGST", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("IGST", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("CESS", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Closing Stock", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Closing Stock(Mt)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("QUALITY", GetType(String)))
        For Me.count = 0 To GridView_fdata.Rows.Count - 1

            dt3.Rows.Add(GridView_fdata.Rows(count).Cells(0).Text, GridView_fdata.Rows(count).Cells(1).Text, GridView_fdata.Rows(count).Cells(2).Text, CDec(GridView_fdata.Rows(count).Cells(3).Text), CDec(GridView_fdata.Rows(count).Cells(4).Text), CDec(GridView_fdata.Rows(count).Cells(5).Text), CDec(GridView_fdata.Rows(count).Cells(6).Text), CDec(GridView_fdata.Rows(count).Cells(7).Text), CDec(GridView_fdata.Rows(count).Cells(8).Text), CDec(GridView_fdata.Rows(count).Cells(9).Text), CDec(GridView_fdata.Rows(count).Cells(10).Text), CDec(GridView_fdata.Rows(count).Cells(11).Text), CDec(GridView_fdata.Rows(count).Cells(12).Text), CDec(GridView_fdata.Rows(count).Cells(13).Text), CDec(GridView_fdata.Rows(count).Cells(14).Text), CDec(GridView_fdata.Rows(count).Cells(15).Text), CDec(GridView_fdata.Rows(count).Cells(16).Text), CDec(GridView_fdata.Rows(count).Cells(17).Text), GridView_fdata.Rows(count).Cells(18).Text)
        Next

        Using wb As New XLWorkbook()
            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=Stock_Report.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub

    Public Sub ExportExcel(ByVal filename As String, ByVal gv As GridView)
        gv.AllowPaging = False
        gv.AllowSorting = False
        gv.EditIndex = -1
        Response.ClearContent()
        'Response.AddHeader("content-disposition", "attachment; filename=" & filename & ".xlsx")
        'Response.ContentType = "application/vnd.ms-excel"
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xls")
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        gv.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()

    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' Verifies that the control is rendered
    End Sub

End Class