Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Public Class Reports
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim dt As New DataTable
    Dim da As New SqlDataAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub DropDownList9_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList9.SelectedIndexChanged
        If DropDownList9.SelectedValue = "Active Work Orders" Then
            MultiView1.ActiveViewIndex = 0

            Dim quary_trans As String = "DECLARE @TT TABLE(PO_NO VARCHAR(30),W_SLNO VARCHAR(5),W_END_DATE DATE)

			INSERT INTO @TT
			select PO_NO,W_SLNO,MAX(W_END_DATE) from WO_ORDER join ORDER_DETAILS on WO_ORDER.PO_NO=ORDER_DETAILS.SO_NO join SUPL on WO_ORDER.SUPL_ID=SUPL.SUPL_ID where W_END_DATE> GETDATE() GROUP BY PO_NO,W_SLNO order by PO_NO,W_SLNO 

			SELECT ROW_NUMBER() OVER(ORDER BY SO_ACTUAL) ROW_NO,*
			FROM (
			SELECT o1.SO_NO,O1.SO_ACTUAL,O1.SO_ACTUAL_DATE,W1.SUPL_ID,S1.SUPL_NAME,W1.W_SLNO,W1.W_NAME,W1.W_AU,SUM(W1.W_QTY) AS W_QTY,SUM(W1.W_COMPLITED) AS W_COMPLITED,MAX(T1.W_END_DATE) AS W_END_DATE FROM WO_ORDER W1 JOIN @TT T1 ON W1.PO_NO=T1.PO_NO AND W1.W_SLNO=T1.W_SLNO join ORDER_DETAILS O1 on W1.PO_NO=O1.SO_NO join SUPL S1 on W1.SUPL_ID=S1.SUPL_ID GROUP BY o1.SO_NO,O1.SO_ACTUAL,O1.SO_ACTUAL_DATE,W1.SUPL_ID,S1.SUPL_NAME,W1.W_SLNO,W1.W_NAME,W1.W_AU
			UNION
			select SO_NO,SO_ACTUAL,SO_ACTUAL_DATE,SUPL.SUPL_ID,SUPL_NAME,'' as W_SLNO,'' as w_name,'' as w_au,ORD_AMOUNT,sum(prov_amt) as w_completed,max(VALID_DATE) as W_END_DATE from ORDER_DETAILS join RATE_CONTRACT on ORDER_DETAILS.SO_NO=RATE_CONTRACT.PO_NO join SUPL on ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID join mb_book on ORDER_DETAILS.SO_NO=mb_book.po_no where SO_NO in (select PO_NO from RATE_CONTRACT where VALID_DATE>'2025-07-01') and PO_TYPE<>'STORE MATERIAL'  group by SO_NO,SO_ACTUAL,SO_ACTUAL_DATE,SUPL.SUPL_ID,SUPL_NAME,ORD_AMOUNT
			) a"

            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter(quary_trans, conn)
            da.Fill(dt)
            conn.Close()
            GridView10.DataSource = dt
            GridView10.DataBind()

        End If
    End Sub

    Protected Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        If GridView10.Rows.Count > 0 Then
            Try

                Dim dt As DataTable = New DataTable()
                For j As Integer = 0 To GridView10.Columns.Count - 1
                    dt.Columns.Add(GridView10.Columns(j).HeaderText)
                Next
                For i As Integer = 0 To GridView10.Rows.Count - 1
                    Dim dr As DataRow = dt.NewRow()
                    For j As Integer = 0 To GridView10.Columns.Count - 1
                        If (GridView10.Rows(i).Cells(j).Text <> "") Then
                            dr(GridView10.Columns(j).HeaderText) = GridView10.Rows(i).Cells(j).Text
                        End If

                    Next
                    dt.Rows.Add(dr)
                Next

                Using wb As XLWorkbook = New XLWorkbook()
                    wb.Worksheets.Add(dt, "Active Work Orders Report")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=ActiveWorkOrders.xlsx")
                    Using MyMemoryStream As MemoryStream = New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using

            Catch ex As Exception
            Finally

            End Try
        End If
    End Sub
End Class