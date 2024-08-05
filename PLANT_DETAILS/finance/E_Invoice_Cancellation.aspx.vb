Imports System.Net
Imports System.Data.SqlClient
Imports System.Globalization
Public Class E_Invoice_Cancellation
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim dr As SqlDataReader
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim goAheadFlag As Boolean = True
    Dim da As New SqlDataAdapter
    Dim ds As New DataSet()
    Dim mycommand As New SqlCommand
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ''ADD FISCAL YEAR IN DROPDOWNLIST
            conn.Open()
            da = New SqlDataAdapter("SELECT FY FROM FISCAL_YEAR ORDER BY FY DESC", conn)
            da.Fill(ds, "FISCAL_YEAR")
            DropDownList17.DataSource = ds.Tables("FISCAL_YEAR")
            DropDownList17.DataValueField = "FY"
            DropDownList17.DataBind()
            DropDownList17.Items.Insert(0, "Select")
            conn.Close()
        End If
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button47_Click(sender As Object, e As EventArgs) Handles Button47.Click
        Dim my_gst_code As New String("")
        'search company profile compair gst code
        conn.Open()
        mycommand.CommandText = "select * from comp_profile"
        mycommand.Connection = conn
        dr = mycommand.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            my_gst_code = dr.Item("c_gst_no")
            dr.Close()
        End If
        conn.Close()

        Dim logicClassObj = New EinvoiceLogicClassEY
        Dim AuthErrorData As List(Of AuthenticationErrorDetailsClassEY) = logicClassObj.EinvoiceAuthentication(DropDownList10.SelectedValue, TextBox43.Text)
        If (AuthErrorData.Item(0).status = "1") Then

            Dim EinvCancellationErrorData As List(Of EinvoiceCancellationErrorDetailsClassEY) = logicClassObj.CancelEInvoice(AuthErrorData.Item(0).Idtoken, TextBox4.Text, my_gst_code, DropDownList1.SelectedValue, TextBox3.Text)
            If (EinvCancellationErrorData.Item(0).status = "1") Then
                'TextBox6.Text = EinvCancellationErrorData.Item(0).IRN
                conn.Open()
                Dim sqlQuery As String = ""
                sqlQuery = "update DESPATCH set INV_STATUS ='CANCELLED',INV_CANCELLATION_DATE='" & CDate(EinvCancellationErrorData.Item(0).CancelDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) & "' where IRN_NO  ='" & EinvCancellationErrorData.Item(0).IRN & "'"
                Dim despatch As New SqlCommand(sqlQuery, conn)
                despatch.ExecuteReader()
                despatch.Dispose()
                conn.Close()
                goAheadFlag = True
                Label552.Visible = True
                Label552.Text = "IRN Cancelled Successfully."
            ElseIf (EinvCancellationErrorData.Item(0).status = "2") Then
                Label552.Visible = True
                Label553.Visible = True
                Label552.Text = EinvCancellationErrorData.Item(0).errorCode
                Label553.Text = EinvCancellationErrorData.Item(0).errorMessage
                goAheadFlag = False

            End If

        ElseIf (AuthErrorData.Item(0).status = "2") Then

            Label552.Visible = True
            Label553.Visible = True
            Label552.Text = AuthErrorData.Item(0).errorCode
            Label553.Text = AuthErrorData.Item(0).errorMessage
            goAheadFlag = False

        Else
            goAheadFlag = False
            Label552.Visible = True
            Label552.Text = "There is some response error in E-invoice Authentication."
        End If
    End Sub

    Protected Sub DropDownList17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList17.SelectedIndexChanged
        If (DropDownList17.SelectedValue <> "Select") Then
            conn.Open()
            da = New SqlDataAdapter("select (D_TYPE+INV_NO) As invoice_no from DESPATCH where IRN_NO is not null and FISCAL_YEAR='" & DropDownList17.SelectedValue & "' AND (INV_STATUS='' OR INV_STATUS='ACTIVE') order by invoice_no", conn)
            da.Fill(ds, "DESPATCH")
            DropDownList10.DataSource = ds.Tables("DESPATCH")
            DropDownList10.DataValueField = "invoice_no"
            DropDownList10.DataBind()
            DropDownList10.Items.Insert(0, "Select")
            DropDownList10.SelectedValue = "Select"
            conn.Close()
        Else
            DropDownList10.Items.Clear()
            DropDownList10.DataBind()
        End If
    End Sub

    Protected Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged
        If (DropDownList10.SelectedValue <> "Select") Then
            conn.Open()
            Dim mc1 As New SqlCommand
            mc1.CommandText = "select * from DESPATCH LEFT JOIN dater ON DESPATCH.PARTY_CODE=dater.d_code LEFT JOIN SUPL ON DESPATCH.PARTY_CODE=SUPL.SUPL_ID where D_TYPE+INV_NO='" & DropDownList10.Text & "' AND FISCAL_YEAR='" & DropDownList17.Text & "'"
            mc1.Connection = conn
            dr = mc1.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                TextBox4.Text = dr.Item("IRN_NO")
                If (dr.Item("INV_STATUS") = "") Then
                    TextBox28.Text = "ACTIVE"
                Else
                    TextBox28.Text = dr.Item("INV_STATUS")
                End If
                TextBox43.Text = dr.Item("PARTY_CODE")


                If (IsDBNull(dr.Item("d_name"))) Then
                    TextBox1.Text = dr.Item("SUPL_NAME")
                Else
                    TextBox1.Text = dr.Item("d_name")
                End If
                TextBox2.Text = dr.Item("TOTAL_AMT")
                dr.Close()
            Else
                dr.Close()
            End If
            conn.Close()
        Else
            DropDownList10.Items.Clear()
            DropDownList10.DataBind()
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        If (DropDownList1.Text = "1") Then
            TextBox3.Text = "Duplicate IRN"
        ElseIf (DropDownList1.Text = "2") Then
            TextBox3.Text = "Wrong data entry"
        ElseIf (DropDownList1.Text = "3") Then
            TextBox3.Text = "Order Cancelled"
        ElseIf (DropDownList1.Text = "4") Then
            TextBox3.Text = "Others"
        End If
    End Sub
End Class