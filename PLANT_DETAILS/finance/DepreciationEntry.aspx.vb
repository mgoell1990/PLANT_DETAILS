Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class DepreciationEntry
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim dt As New DataTable
    Dim da As New SqlDataAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If (DropDownList1.SelectedValue = "Select") Then
        Else

            MultiView1.ActiveViewIndex = 0
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("DECLARE @TT TABLE([AssetCode] VARCHAR(30),[FISCALYEAR] VARCHAR(10),Quarter1 VARCHAR(10),
            CummDeprBeforeQ1 decimal(16,2),DeprValueQ1 DECIMAL(16,2),Quarter2 VARCHAR(10),CummDeprBeforeQ2 decimal(16,2),DeprValueQ2 DECIMAL(16,2),
            Quarter3 VARCHAR(10),CummDeprBeforeQ3 decimal(16,2),DeprValueQ3 DECIMAL(16,2),Quarter4 VARCHAR(10),CummDeprBeforeQ4 decimal(16,2),DeprValueQ4 DECIMAL(16,2))
            INSERT INTO @TT
            SELECT *,'Q2' as Quarter2, 0 as CummDeprBeforeQ2, 0 as DeprValueQ2,'Q3' as Quarter3, 0 as CummDeprBeforeQ3, 0 as DeprValueQ3,'Q4' as Quarter4, 0 as CummDeprBeforeQ4, 0 as DeprValueQ4
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ1,
                [DepreciationValue] as DeprValueQ1
                FROM AssetDepreciation where FiscalYear=" + DropDownList1.SelectedValue + " and Quarter='Q1'
            ) AS SourceTable PIVOT(SUM([DeprValueQ1]) FOR [Quarter] IN([Q1])) AS PivotTable order by AssetCode

            INSERT INTO @TT
            SELECT *,'Q3' as Quarter3, 0 as CummDeprBeforeQ3, 0 as DeprValueQ3,'Q4' as Quarter4, 0 as CummDeprBeforeQ4, 0 as DeprValueQ4
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1, 0 as CummDeprBeforeQ1, 0 as DeprValueQ1,
		        'Q2' as Quarter2,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ2,
                [DepreciationValue] as DeprValueQ2
                FROM AssetDepreciation where FiscalYear=" + DropDownList1.SelectedValue + " and Quarter='Q2'
            ) AS SourceTable PIVOT(SUM([DeprValueQ2]) FOR [Quarter] IN([Q2])) AS PivotTable order by AssetCode

            INSERT INTO @TT
            SELECT *,'Q4' as Quarter4, 0 as CummDeprBeforeQ4, 0 as DeprValueQ4
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1, 0 as CummDeprBeforeQ1, 0 as DeprValueQ1,
		        'Q2' as Quarter2, 0 as CummDeprBeforeQ2, 0 as DeprValueQ2,
		        'Q3' as Quarter3,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ3,
                [DepreciationValue] as DeprValueQ3
                FROM AssetDepreciation where FiscalYear=" + DropDownList1.SelectedValue + " and Quarter='Q3'
            ) AS SourceTable PIVOT(SUM([DeprValueQ3]) FOR [Quarter] IN([Q3])) AS PivotTable order by AssetCode

            INSERT INTO @TT
            SELECT *
            FROM
            (
                SELECT [AssetCode],
	            [FISCALYEAR],
		        'Q1' as Quarter1, 0 as CummDeprBeforeQ1, 0 as DeprValueQ1,
		        'Q2' as Quarter2, 0 as CummDeprBeforeQ2, 0 as DeprValueQ2,
		        'Q3' as Quarter3, 0 as CummDeprBeforeQ3, 0 as DeprValueQ3,
		        'Q4' as Quarter4,
		        [Quarter],
		        CummulativeDeprBeforeQuarter as CummDeprBeforeQ4,
                [DepreciationValue] as DeprValueQ4
                FROM AssetDepreciation where FiscalYear=" + DropDownList1.SelectedValue + " and Quarter='Q4'
            ) AS SourceTable PIVOT(SUM([DeprValueQ4]) FOR [Quarter] IN([Q4])) AS PivotTable order by AssetCode

            select A1.AssetCode,Max(A1.AccountCode) as AccountCode,A1.AssetName,Max(A1.DateOfCommisioning) as DateOfCommisioning,Max(A1.PhysicalQuantity) as PhysicalQuantity,Max(A1.PhysicalLocation) as PhysicalLocation,
            Max(A1.DepreciationPercentage) as DepreciationPercentage,Max(A1.GrossBlock) as GrossBlock,Max(A1.CummulativeDepriciation) as CummulativeDepriciation,max(T1.FISCALYEAR) as fiscalYear,
            Max(T1.Quarter1) as Quarter1,sum(T1.CummDeprBeforeQ1) as CummDeprBeforeQ1,sum(T1.DeprValueQ1) as DeprValueQ1,max(T1.Quarter2) as Quarter2,sum(T1.CummDeprBeforeQ2) as CummDeprBeforeQ2,sum(T1.DeprValueQ2) as DeprValueQ2,
            max(T1.Quarter3) as Quarter3,sum(T1.CummDeprBeforeQ3) as CummDeprBeforeQ3,sum(T1.DeprValueQ3) as DeprValueQ3,max(T1.Quarter4) as Quarter4,sum(T1.CummDeprBeforeQ4) as CummDeprBeforeQ4,sum(T1.DeprValueQ4) as DeprValueQ4,max(A1.Remarks) as Remarks from AssetMaster A1 join @TT T1 on A1.AssetCode=T1.AssetCode group by A1.AssetCode,A1.AssetName order by AccountCode", conn)
            da.Fill(dt)
            conn.Close()
            GridView14.DataSource = dt
            GridView14.DataBind()

            Dim depreciationForQ1, depreciationForQ2, depreciationForQ3, depreciationForQ4 As New Decimal(0)

            For L = 0 To GridView14.Rows.Count - 1
                depreciationForQ1 = depreciationForQ1 + CDec(GridView14.Rows(L).Cells(12).Text)
                depreciationForQ2 = depreciationForQ2 + CDec(GridView14.Rows(L).Cells(15).Text)
                depreciationForQ3 = depreciationForQ3 + CDec(GridView14.Rows(L).Cells(18).Text)
                depreciationForQ4 = depreciationForQ4 + CDec(GridView14.Rows(L).Cells(21).Text)
            Next

            If depreciationForQ1 = 0 Then
                DropDownList5.Items.Add(New ListItem("Q1"))
            End If
            If depreciationForQ2 = 0 Then
                DropDownList5.Items.Add(New ListItem("Q2"))
            End If
            If depreciationForQ3 = 0 Then
                DropDownList5.Items.Add(New ListItem("Q3"))
            End If
            If depreciationForQ4 = 0 Then
                DropDownList5.Items.Add(New ListItem("Q4"))
            End If

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim depreciationForQ1, depreciationForQ2, depreciationForQ3, depreciationForQ4 As New Decimal(0)
        If (DropDownList5.SelectedItem.Text = "Q1") Then
            For L = 0 To GridView14.Rows.Count - 1
                GridView14.Rows(L).Cells(11).Text = GridView14.Rows(L).Cells(8).Text

                depreciationForQ1 = (CDec(GridView14.Rows(L).Cells(7).Text) * CDec(GridView14.Rows(L).Cells(6).Text)) / 400
                If (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95 > (CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ1)) Then
                    GridView14.Rows(L).Cells(12).Text = Math.Round(depreciationForQ1, 2)
                Else
                    GridView14.Rows(L).Cells(12).Text = Math.Round(depreciationForQ1 - CDec(((CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ1) - (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95))), 2)
                End If
            Next
        ElseIf (DropDownList5.SelectedItem.Text = "Q2") Then
            For L = 0 To GridView14.Rows.Count - 1
                GridView14.Rows(L).Cells(14).Text = GridView14.Rows(L).Cells(8).Text

                depreciationForQ2 = (CDec(GridView14.Rows(L).Cells(7).Text) * CDec(GridView14.Rows(L).Cells(6).Text)) / 400
                If (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95 > (CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ1)) Then
                    GridView14.Rows(L).Cells(15).Text = Math.Round(depreciationForQ2, 2)
                Else
                    GridView14.Rows(L).Cells(15).Text = Math.Round(depreciationForQ2 - CDec(((CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ2) - (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95))), 2)
                End If
            Next
        ElseIf (DropDownList5.SelectedItem.Text = "Q3") Then
            For L = 0 To GridView14.Rows.Count - 1
                GridView14.Rows(L).Cells(17).Text = GridView14.Rows(L).Cells(8).Text

                depreciationForQ3 = (CDec(GridView14.Rows(L).Cells(7).Text) * CDec(GridView14.Rows(L).Cells(6).Text)) / 400
                If (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95 > (CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ1)) Then
                    GridView14.Rows(L).Cells(18).Text = Math.Round(depreciationForQ3, 2)
                Else
                    GridView14.Rows(L).Cells(18).Text = Math.Round(depreciationForQ3 - CDec(((CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ3) - (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95))), 2)
                End If
            Next
        ElseIf (DropDownList5.SelectedItem.Text = "Q4") Then
            For L = 0 To GridView14.Rows.Count - 1
                GridView14.Rows(L).Cells(20).Text = GridView14.Rows(L).Cells(8).Text

                depreciationForQ4 = (CDec(GridView14.Rows(L).Cells(7).Text) * CDec(GridView14.Rows(L).Cells(6).Text)) / 400
                If (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95 > (CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ1)) Then
                    GridView14.Rows(L).Cells(21).Text = Math.Round(depreciationForQ4, 2)
                Else
                    GridView14.Rows(L).Cells(21).Text = Math.Round(depreciationForQ4 - CDec(((CDec(GridView14.Rows(L).Cells(8).Text) + depreciationForQ4) - (CDec(GridView14.Rows(L).Cells(7).Text) * 0.95))), 2)
                End If
            Next
        End If

    End Sub

    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        If GridView14.Rows.Count > 0 Then
            Try

                Dim dt As DataTable = New DataTable()
                For j As Integer = 0 To GridView14.Columns.Count - 1
                    dt.Columns.Add(GridView14.Columns(j).HeaderText)
                Next
                For i As Integer = 0 To GridView14.Rows.Count - 1
                    Dim dr As DataRow = dt.NewRow()
                    For j As Integer = 0 To GridView14.Columns.Count - 1
                        If (GridView14.Rows(i).Cells(j).Text <> "") Then
                            dr(GridView14.Columns(j).HeaderText) = GridView14.Rows(i).Cells(j).Text
                        End If

                    Next
                    dt.Rows.Add(dr)
                Next

                Using wb As XLWorkbook = New XLWorkbook()
                    wb.Worksheets.Add(dt, "Depreciation Report")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=Depreciation_" + DropDownList1.Text + ".xlsx")
                    Using MyMemoryStream As MemoryStream = New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using


            Catch ex As Exception
                Console.WriteLine(ex.ToString())
            Finally

            End Try
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
End Class