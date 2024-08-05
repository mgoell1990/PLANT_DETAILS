Imports System.Data.SqlClient

Public Class CheckOrderValidity
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If IsDate(TextBox4.Text) = False Then
            TextBox4.Text = ""
            TextBox4.Focus()
            Return
        ElseIf IsDate(TextBox5.Text) = False Then
            TextBox5.Text = ""
            TextBox5.Focus()
            Return
        End If
        Dim from_date, to_date As Date
        from_date = CDate(TextBox4.Text)
        to_date = CDate(TextBox5.Text)


        ''''''''''''''''''''''''''''''''''
        conn.Open()
        dt.Clear()

        Dim quary As String = "select SO_NO,SO_DATE,SO_ACTUAL,SO_ACTUAL_DATE,SUPL_ID,SUPL_NAME,max(MAT_DELIVERY) as order_validity
        from ORDER_DETAILS join PO_ORD_MAT on ORDER_DETAILS.SO_NO=PO_ORD_MAT.PO_NO join SUPL on 
        ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID group by SO_NO, SO_DATE,SO_ACTUAL,SO_ACTUAL_DATE,SUPL_ID,SUPL_NAME HAVING MAX(SD_REFUND_STATUS) IS NULL AND MAX(MAT_DELIVERY) BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' 
        union
        select SO_NO,SO_DATE,SO_ACTUAL,SO_ACTUAL_DATE,WO_ORDER.SUPL_ID,SUPL_NAME,max(W_END_DATE) as order_validity 
        from ORDER_DETAILS join WO_ORDER on ORDER_DETAILS.SO_NO=WO_ORDER.PO_NO join SUPL on 
        ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID group by SO_NO, SO_DATE,SO_ACTUAL,SO_ACTUAL_DATE,WO_ORDER.SUPL_ID,
        SUPL_NAME HAVING MAX(SD_REFUND_STATUS) IS NULL AND MAX(W_END_DATE) BETWEEN '" & from_date.Year & "-" & from_date.Month & "-" & from_date.Day & "' AND '" & to_date.Year & "-" & to_date.Month & "-" & to_date.Day & "' ORDER BY SO_NO"

        da = New SqlDataAdapter(quary, conn)
        da.Fill(dt)
        conn.Close()
        ''''''''''''''''''''''''''''''''''
        GridView2.DataSource = dt
        GridView2.DataBind()


    End Sub
End Class