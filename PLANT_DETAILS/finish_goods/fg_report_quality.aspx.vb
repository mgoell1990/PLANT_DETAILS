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
Public Class fg_report_quality
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


        '     Dim quary As String = "DECLARE @TT TABLE(ITEM_TYPE VARCHAR(30),ITEM_CODE VARCHAR(30),ITEM_NAME VARCHAR(60),ITEM_OPEN_STOCK DECIMAL(16,3),PROD_F_QTY DECIMAL(16,3),TRANSFER_QTY DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_MT DECIMAL(16,3),SALES_VALUE DECIMAL(16,3),SGST_AMT DECIMAL(16,3),CGST_AMT DECIMAL(16,3),IGST_AMT DECIMAL(16,3),CESS_AMT DECIMAL(16,3))
        'INSERT INTO @TT
        'select ITEM_TYPE,ITEM_CODE ,ITEM_NAME, (ITEM_OPEN_F_STOCK + ITEM_OPEN_B_STOCK) As ITEM_OPEN_STOCK,'0.00' AS PROD_F_QTY, '0.00' AS TRANSFER_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT from F_ITEM where item_au <> 'Activity' order by item_code

        'INSERT INTO @TT
        'select MAX(ITEM_TYPE) AS ITEM_TYPE,F_ITEM.ITEM_CODE,ITEM_NAME,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as ITEM_OPEN_STOCK, 
        ''0.00' AS PROD_F_QTY, '0.00' AS TRANSFER_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        'from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE <'" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and item_au <> 'Activity' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        'INSERT INTO @TT
        'select MAX(ITEM_TYPE) AS ITEM_TYPE,F_ITEM.ITEM_CODE,ITEM_NAME,-(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as ITEM_OPEN_STOCK, 
        ''0.00' AS PROD_F_QTY, '0.00' AS TRANSFER_QTY, '0.00' AS SALES_QTY, '0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        'from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE <'" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and item_au <> 'Activity' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        'INSERT INTO @TT
        'select MAX(ITEM_TYPE) AS ITEM_TYPE,F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as PROD_F_QTY,
        ''0.00' as TRANSFER_QTY,(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as SALES_QTY,
        ''0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        'from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO<>'Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        'INSERT INTO @TT
        'select MAX(ITEM_TYPE) AS ITEM_TYPE,F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,'0.00' as PROD_F_QTY,
        '(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as TRANSFER_QTY,'0.00' as SALES_QTY,'0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
        'from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO='Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

        'INSERT INTO @TT
        'select MAX(ITEM_TYPE) AS ITEM_TYPE,DESPATCH.P_CODE As ITEM_CODE, P_DESC AS ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK, '0.00' as PROD_F_QTY, '0.00' as TRANSFER_QTY,'0.00' as SALES_QTY, 
        '(case when SUM(total_weight) is null then '0.000' else SUM(total_weight) end) AS SALES_MT, (case when SUM(TAXABLE_VALUE) is null then '0.000' else SUM(TAXABLE_VALUE) end) AS SALES_VALUE,
        '(case when SUM(SGST_AMT) is null then '0.000' else SUM(SGST_AMT) end) AS SGST_AMT, (case when SUM(CGST_AMT) is null then '0.000' else SUM(CGST_AMT) end) AS CGST_AMT,
        '(case when SUM(IGST_AMT) is null then '0.000' else SUM(IGST_AMT) end) AS IGST_AMT, (case when SUM(CESS_AMT) is null then '0.000' else SUM(CESS_AMT) end) AS CESS_AMT
        'from DESPATCH JOIN F_ITEM ON DESPATCH.P_CODE=F_ITEM.ITEM_CODE where DESPATCH .INV_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and INV_STATUS <> 'cancelled' GROUP BY DESPATCH.P_CODE, P_DESC order by ITEM_CODE


        'DECLARE @TT1 TABLE(ITEM_TYPE VARCHAR(30),ITEM_CODE VARCHAR(30),ITEM_NAME VARCHAR(60),ITEM_OPEN_STOCK DECIMAL(16,3),open_mt DECIMAL(16,3),PROD_F_QTY DECIMAL(16,3),prod_mt DECIMAL(16,3),TRANSFER_QTY DECIMAL(16,3),trans_mt DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_MT DECIMAL(16,3),SALES_VALUE DECIMAL(16,3),SGST_AMT DECIMAL(16,3),CGST_AMT DECIMAL(16,3),IGST_AMT DECIMAL(16,3),CESS_AMT DECIMAL(16,3),CLOSING_F_STOCK DECIMAL(16,3),closing_mt DECIMAL(16,3))
        'INSERT INTO @TT1
        'SELECT t1.ITEM_TYPE,t1.ITEM_CODE, t1.ITEM_NAME,SUM(ITEM_OPEN_STOCK) AS ITEM_OPEN_STOCK,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(ITEM_OPEN_STOCK)*max(f1.ITEM_WEIGHT))/1000 ELSE SUM(ITEM_OPEN_STOCK) END) As open_mt,SUM(PROD_F_QTY) AS PROD_F_QTY,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(PROD_F_QTY) * max(f1.ITEM_WEIGHT))/1000 ELSE SUM(PROD_F_QTY) END) As prod_mt,SUM(TRANSFER_QTY) AS TRANSFER_QTY,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(TRANSFER_QTY) * max(f1.ITEM_WEIGHT))/1000 ELSE SUM(TRANSFER_QTY) END) As trans_mt,SUM(SALES_QTY) AS SALES_QTY,SUM(SALES_MT) AS SALES_MT,SUM(SALES_VALUE) AS SALES_VALUE,SUM(SGST_AMT) AS SGST_AMT,
        'SUM(CGST_AMT) AS CGST_AMT,SUM(IGST_AMT) AS IGST_AMT,SUM(CESS_AMT) AS CESS_AMT,(SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) AS CLOSING_F_STOCK,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN ((SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) * max(f1.ITEM_WEIGHT))/1000 ELSE (SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) END) As closing_mt FROM @TT t1 join F_ITEM f1 on t1.ITEM_CODE=f1.ITEM_CODE GROUP BY t1.ITEM_CODE,t1.ITEM_TYPE, t1.ITEM_NAME ORDER BY ITEM_CODE

        'select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo,FG_TYPE,qual_desc,sum(ITEM_OPEN_STOCK) as Open_Stock,sum(open_mt) as Open_Stock_Mt,sum(PROD_F_QTY)+sum(TRANSFER_QTY) as PROD_QTY,sum(prod_mt)+sum(trans_mt) as PROD_QTY_Mt,sum(SALES_QTY) as SALES_QTY,sum(SALES_MT) as SALES_QTY_Mt,sum(CLOSING_F_STOCK) as CLOSING_QTY,sum(closing_mt) as CLOSING_QTY_Mt from @tt1 tt1 JOIN qual_group q1 on tt1.ITEM_TYPE=q1.qual_code where qual_desc <> 'REJ. SILICA' GROUP BY qual_desc,FG_TYPE,FG_IND ORDER BY FG_IND"


        Dim quary As String = "DECLARE @TT TABLE(ITEM_TYPE VARCHAR(30),ITEM_CODE VARCHAR(30),ITEM_NAME VARCHAR(100),ITEM_OPEN_STOCK DECIMAL(16,3),PROD_F_QTY DECIMAL(16,3),TRANSFER_QTY DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_MT DECIMAL(16,3),SALES_VALUE DECIMAL(16,3),SGST_AMT DECIMAL(16,3),CGST_AMT DECIMAL(16,3),IGST_AMT DECIMAL(16,3),CESS_AMT DECIMAL(16,3))
            INSERT INTO @TT
			select MAX(ITEM_TYPE) AS ITEM_TYPE,F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as PROD_F_QTY,
			'0.00' as TRANSFER_QTY,(case when SUM(ITEM_I_QTY ) is null then '0.00' else SUM(ITEM_I_QTY) end) as SALES_QTY,
			'0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
			from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO<>'Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

			INSERT INTO @TT
			select MAX(ITEM_TYPE) AS ITEM_TYPE,F_ITEM.ITEM_CODE,ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK,'0.00' as PROD_F_QTY,
			(case when SUM(ITEM_F_QTY ) is null then '0.00' else SUM(ITEM_F_QTY) end) as TRANSFER_QTY,'0.00' as SALES_QTY,'0.00' AS SALES_MT, '0.00' AS SALES_VALUE, '0.00' AS SGST_AMT, '0.00' AS CGST_AMT, '0.00' AS IGST_AMT, '0.00' AS CESS_AMT
			from F_ITEM join PROD_CONTROL on F_ITEM.ITEM_CODE=PROD_CONTROL.ITEM_CODE where PROD_CONTROL.PROD_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and item_au <> 'Activity' AND ITEM_I_SO='Transfer' group by F_ITEM.ITEM_CODE,F_ITEM.ITEM_NAME order by item_code

			INSERT INTO @TT
			select MAX(ITEM_TYPE) AS ITEM_TYPE,DESPATCH.P_CODE As ITEM_CODE, P_DESC AS ITEM_NAME,'0.00' AS ITEM_OPEN_STOCK, '0.00' as PROD_F_QTY, '0.00' as TRANSFER_QTY,'0.00' as SALES_QTY, 
			(case when SUM(total_weight) is null then '0.000' else SUM(total_weight) end) AS SALES_MT, (case when SUM(TAXABLE_VALUE) is null then '0.000' else SUM(TAXABLE_VALUE) end) AS SALES_VALUE,
			(case when SUM(SGST_AMT) is null then '0.000' else SUM(SGST_AMT) end) AS SGST_AMT, (case when SUM(CGST_AMT) is null then '0.000' else SUM(CGST_AMT) end) AS CGST_AMT,
			(case when SUM(IGST_AMT) is null then '0.000' else SUM(IGST_AMT) end) AS IGST_AMT, (case when SUM(CESS_AMT) is null then '0.000' else SUM(CESS_AMT) end) AS CESS_AMT
			from DESPATCH JOIN F_ITEM ON DESPATCH.P_CODE=F_ITEM.ITEM_CODE where DESPATCH .INV_DATE between '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' and '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' and INV_STATUS <> 'cancelled' GROUP BY DESPATCH.P_CODE, P_DESC order by ITEM_CODE


			DECLARE @TT1 TABLE(ITEM_TYPE VARCHAR(30),ITEM_CODE VARCHAR(30),ITEM_NAME VARCHAR(100),ITEM_OPEN_STOCK DECIMAL(16,3),open_mt DECIMAL(16,3),PROD_F_QTY DECIMAL(16,3),prod_mt DECIMAL(16,3),TRANSFER_QTY DECIMAL(16,3),trans_mt DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_MT DECIMAL(16,3),SALES_VALUE DECIMAL(16,3),SGST_AMT DECIMAL(16,3),CGST_AMT DECIMAL(16,3),IGST_AMT DECIMAL(16,3),CESS_AMT DECIMAL(16,3),CLOSING_F_STOCK DECIMAL(16,3),closing_mt DECIMAL(16,3))
			INSERT INTO @TT1
			SELECT t1.ITEM_TYPE,t1.ITEM_CODE, t1.ITEM_NAME,SUM(ITEM_OPEN_STOCK) AS ITEM_OPEN_STOCK,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(ITEM_OPEN_STOCK)*max(f1.ITEM_WEIGHT))/1000 ELSE SUM(ITEM_OPEN_STOCK) END) As open_mt,SUM(PROD_F_QTY) AS PROD_F_QTY,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(PROD_F_QTY) * max(f1.ITEM_WEIGHT))/1000 ELSE SUM(PROD_F_QTY) END) As prod_mt,SUM(TRANSFER_QTY) AS TRANSFER_QTY,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN (SUM(TRANSFER_QTY) * max(f1.ITEM_WEIGHT))/1000 ELSE SUM(TRANSFER_QTY) END) As trans_mt,SUM(SALES_QTY) AS SALES_QTY,SUM(SALES_MT) AS SALES_MT,SUM(SALES_VALUE) AS SALES_VALUE,SUM(SGST_AMT) AS SGST_AMT,
			SUM(CGST_AMT) AS CGST_AMT,SUM(IGST_AMT) AS IGST_AMT,SUM(CESS_AMT) AS CESS_AMT,(SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) AS CLOSING_F_STOCK,(CASE WHEN max(f1.ITEM_AU)='Pcs' THEN ((SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) * max(f1.ITEM_WEIGHT))/1000 ELSE (SUM(ITEM_OPEN_STOCK)+SUM(PROD_F_QTY)+SUM(TRANSFER_QTY)-SUM(SALES_QTY)) END) As closing_mt FROM @TT t1 join F_ITEM f1 on t1.ITEM_CODE=f1.ITEM_CODE GROUP BY t1.ITEM_CODE,t1.ITEM_TYPE, t1.ITEM_NAME ORDER BY ITEM_CODE
			
			DECLARE @TT2 TABLE(PR_IND int,qual_desc VARCHAR(60),Open_Stock DECIMAL(16,3),Open_Stock_Mt DECIMAL(16,3),PROD_QTY DECIMAL(16,3),PROD_QTY_Mt DECIMAL(16,3),TRANSFER_QTY DECIMAL(16,3),TRANSFER_QTY_Mt DECIMAL(16,3),SALES_QTY DECIMAL(16,3),SALES_QTY_Mt DECIMAL(16,3),CLOSING_QTY DECIMAL(16,3),CLOSING_QTY_Mt DECIMAL(16,3))         
			INSERT INTO @TT2
			select (case when qual_desc='MCH' then 1 when qual_desc='CHM' then 2 when qual_desc='MGT' then 3 when qual_desc='AMC' then 4 when qual_desc='ASC' then 5 when qual_desc='MCB' then 6 when qual_desc='ALUMINO SILICATE' then 7 when qual_desc='MCB BLAST BRIQUETTE' then 8 when qual_desc='SILICA' then 9 when qual_desc='MASSES' then 10 ELSE 11 end) as PR_IND,qual_desc,sum(ITEM_OPEN_STOCK) as Open_Stock,sum(open_mt) as Open_Stock_Mt,sum(PROD_F_QTY) as PROD_QTY,sum(prod_mt) as PROD_QTY_Mt,sum(TRANSFER_QTY) as TRANSFER_QTY,sum(trans_mt) as TRANSFER_QTY_Mt,sum(SALES_QTY) as SALES_QTY,sum(SALES_MT) as SALES_QTY_Mt,sum(CLOSING_F_STOCK) as CLOSING_QTY,sum(closing_mt) as CLOSING_QTY_Mt from @tt1 tt1 JOIN qual_group q1 on tt1.ITEM_TYPE=q1.qual_code where qual_desc <> 'REJ. SILICA' GROUP BY qual_desc,FG_TYPE,FG_IND ORDER BY FG_IND
			
			insert into @TT2
			select (case when FG_TYPE='MCH' then 1 when FG_TYPE='CHM' then 2 when FG_TYPE='MGT' then 3 when FG_TYPE='AMC' then 4 when FG_TYPE='ASC' then 5 when FG_TYPE='MCB' then 6 when FG_TYPE='ALUMINO SILICATE' then 7 when FG_TYPE='MCB BLAST BRIQUETTE' then 8 when FG_TYPE='SILICA' then 9 when FG_TYPE='MASSES' then 10 ELSE 11 end) as PR_IND,FG_TYPE as qual_desc,sum(OPENING_STOCK) as Open_Stock,sum(OPENING_STOCK_MT) as Open_Stock_Mt,0 as PROD_QTY,0 as PROD_QTY_Mt,0 as TRANSFER_QTY,0 as TRANSFER_QTY_Mt,0 as SALES_QTY,0 as SALES_QTY_Mt,0 as CLOSING_QTY,0 as CLOSING_QTY_Mt from OB_FINISHED_GOODS where FISCAL_YEAR='" & STR1 & "' group by FG_TYPE
						
			select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo,qual_desc,sum(Open_Stock) as Open_Stock,sum(Open_Stock_Mt) as Open_Stock_Mt,(sum(PROD_QTY)+sum(TRANSFER_QTY)) as PROD_QTY,(sum(PROD_QTY_Mt)+sum(TRANSFER_QTY_Mt)) as PROD_QTY_Mt, sum(SALES_QTY) as SALES_QTY,sum(SALES_QTY_Mt) as SALES_QTY_Mt,
			(sum(Open_Stock)+sum(PROD_QTY)+sum(TRANSFER_QTY)-sum(SALES_QTY)) as CLOSING_QTY,(sum(Open_Stock_Mt)+sum(PROD_QTY_Mt)+sum(TRANSFER_QTY_Mt)-sum(SALES_QTY_Mt)) as CLOSING_QTY_Mt from @tt2 GROUP BY PR_IND,qual_desc ORDER BY PR_IND"


        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        GridView_fdata.DataSource = dt
        GridView_fdata.DataBind()





        Dim total_opening, total_opening_MT, total_Prod, total_prod_MT, total_desp, total_desp_MT, total_closing, total_closing_MT As New Decimal(0)
        Dim j As Integer = 0
        For j = 0 To GridView_fdata.Rows.Count - 1
            ''Calculating total Debit and Credit amount
            total_opening = total_opening + CDec(GridView_fdata.Rows(j).Cells(2).Text)
            total_opening_MT = total_opening_MT + CDec(GridView_fdata.Rows(j).Cells(3).Text)
            total_Prod = total_Prod + CDec(GridView_fdata.Rows(j).Cells(4).Text)
            total_prod_MT = total_prod_MT + CDec(GridView_fdata.Rows(j).Cells(5).Text)
            total_desp = total_desp + CDec(GridView_fdata.Rows(j).Cells(6).Text)
            total_desp_MT = total_desp_MT + CDec(GridView_fdata.Rows(j).Cells(7).Text)
            total_closing = total_closing + CDec(GridView_fdata.Rows(j).Cells(8).Text)
            ''total_closing_MT = total_closing_MT + CDec(GridView_fdata.Rows(j).Cells(9).Text)
        Next
        Dim dRow1 As DataRow
        dRow1 = dt.NewRow
        dRow1.Item(1) = "Total"
        dt.Rows.Add(dRow1)

        dt.AcceptChanges()

        ''GridView_fdata.DataSource = dt
        GridView_fdata.DataBind()

        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(2).Text = total_opening
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(3).Text = total_opening_MT
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(4).Text = total_Prod
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(5).Text = total_prod_MT
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(6).Text = total_desp
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(7).Text = total_desp_MT
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(8).Text = total_closing
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Cells(9).Text = total_closing_MT
        GridView_fdata.Rows(GridView_fdata.Rows.Count - 1).Font.Bold = True

        Dim i As Integer = 0
        For i = 0 To GridView_fdata.Rows.Count - 1
            GridView_fdata.Rows(i).Cells(9).Text = CDec(GridView_fdata.Rows(i).Cells(3).Text) + CDec(GridView_fdata.Rows(i).Cells(5).Text) - CDec(GridView_fdata.Rows(i).Cells(7).Text)
            ''GridView_fdata.DataBind()
        Next
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If GridView_fdata.Rows.Count < 0 Then
            Return
        End If

        Dim dt3 As DataTable = New DataTable("finish goods stock")
        dt3.Columns.Add(New DataColumn("SL No", GetType(String)))
        'dt3.Columns.Add(New DataColumn("FG Type", GetType(String)))
        dt3.Columns.Add(New DataColumn("Item Name", GetType(String)))
        dt3.Columns.Add(New DataColumn("Opening Stock", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Opening Stock(Mt)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Production Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Production(Mt)", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Sales Qty", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Sales Mt.", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Closing Stock", GetType(Decimal)))
        dt3.Columns.Add(New DataColumn("Closing Stock(Mt)", GetType(Decimal)))

        For Me.count = 0 To GridView_fdata.Rows.Count - 1
            dt3.Rows.Add(GridView_fdata.Rows(count).Cells(0).Text, GridView_fdata.Rows(count).Cells(1).Text, CDec(GridView_fdata.Rows(count).Cells(2).Text), CDec(GridView_fdata.Rows(count).Cells(3).Text), CDec(GridView_fdata.Rows(count).Cells(4).Text), CDec(GridView_fdata.Rows(count).Cells(5).Text), CDec(GridView_fdata.Rows(count).Cells(6).Text), CDec(GridView_fdata.Rows(count).Cells(7).Text), CDec(GridView_fdata.Rows(count).Cells(8).Text), CDec(GridView_fdata.Rows(count).Cells(9).Text))
        Next

        Using wb As New XLWorkbook()

            wb.Worksheets.Add(dt3)
            'Export the Excel file.
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=finish_goods_stock_quality.xlsx")
            Using MyMemoryStream As New MemoryStream()
                wb.SaveAs(MyMemoryStream)
                MyMemoryStream.WriteTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using
        End Using
    End Sub
End Class