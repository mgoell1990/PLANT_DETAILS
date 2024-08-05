Imports System.Data.SqlClient
Imports System.Globalization
Imports Tulpep.NotificationWindow


Public Class bank_guarantee
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim count As Integer

    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Protected Sub BINDGRID()
        GridView2.DataSource = DirectCast(ViewState("BG"), DataTable)
        GridView2.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim dt7 As New DataTable()
            dt7.Columns.AddRange(New DataColumn(21) {New DataColumn("ORIGINAL_BG_NO"), New DataColumn("ORIGINAL_BG_DATE"), New DataColumn("PARTY_CODE"), New DataColumn("PARTY_NAME"), New DataColumn("ORDER_NO"), New DataColumn("ACTUAL_ORDER_NO"), New DataColumn("ORDER_DATE"), New DataColumn("IOC_NO"), New DataColumn("IOC_DATE"), New DataColumn("RETURN_IOC_NO"), New DataColumn("RETURN_IOC_DATE"), New DataColumn("DEPOSIT_TYPE"), New DataColumn("BG_TYPE"), New DataColumn("BG_LOCATION"), New DataColumn("ISSUING_BANK_NAME"), New DataColumn("ISSUING_BANK_BRANCH"), New DataColumn("BG_AMOUNT"), New DataColumn("BG_VALIDITY"), New DataColumn("CONF_LETTER_NO"), New DataColumn("CONF_LETTER_DATE"), New DataColumn("COMPANY_CONF_LETTER_NO"), New DataColumn("COMPANY_CONF_LETTER_DATE")})
            ViewState("BG") = dt7
            BINDGRID()
        End If
        CalendarExtender1.EndDate = DateTime.Now.Date
        CalendarExtender5.EndDate = DateTime.Now.Date
        TextBox63_CalendarExtender.EndDate = DateTime.Now.Date
        CalendarExtender3.EndDate = DateTime.Now.Date
        CalendarExtender4.EndDate = DateTime.Now.Date
        TextBox182_CalendarExtender.EndDate = DateTime.Now.Date
        CalendarExtender2.EndDate = DateTime.Now.Date
        CalendarExtender6.EndDate = DateTime.Now.Date

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim working_date As Date

                If TextBox1.Text = "" Or IsDate(TextBox1.Text) = False Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter BG date.','');", True)
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Return
                ElseIf TextBox4.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter BG number.','');", True)
                    TextBox4.Focus()
                    Return
                ElseIf TextBox10.Text = "" Or IsDate(TextBox10.Text) = False Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter original BG date.','');", True)
                    TextBox10.Text = ""
                    TextBox10.Focus()
                    Return
                ElseIf DropDownList25.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter Supl details.','');", True)
                    DropDownList25.Focus()
                    Return
                ElseIf TextBox62.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter PO/WO number.','');", True)
                    TextBox62.Focus()
                    Return
                ElseIf TextBox63.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter PO/WO date.','');", True)
                    TextBox63.Focus()
                    Return
                ElseIf TextBox5.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter IOC number.','');", True)
                    TextBox5.Focus()
                    Return
                ElseIf TextBox6.Text = "" Or IsDate(TextBox6.Text) = False Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter IOC date.','');", True)
                    TextBox6.Text = ""
                    TextBox6.Focus()
                    Return

                ElseIf DropDownList28.SelectedValue = "Select" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please select deposit type.','');", True)
                    DropDownList28.Focus()
                    Return
                ElseIf DropDownList1.SelectedValue = "Select" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please select BG type.','');", True)
                    DropDownList1.Focus()
                    Return
                ElseIf DropDownList2.SelectedValue = "Select" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please select BG location.','');", True)
                    DropDownList2.Focus()
                    Return
                ElseIf TextBox181.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter issuing bank name.','');", True)
                    TextBox181.Focus()
                    Return
                ElseIf TextBox9.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter issuing bank branch.','');", True)
                    TextBox9.Focus()
                    Return
                ElseIf TextBox2.Text = "" Or IsNumeric(TextBox2.Text) = False Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter BG amount.','');", True)
                    TextBox2.Text = ""
                    TextBox2.Focus()
                    Return
                ElseIf TextBox182.Text = "" Or IsDate(TextBox182.Text) = False Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter Return BG validity.','');", True)
                    TextBox182.Text = ""
                    TextBox182.Focus()
                    Return
                ElseIf TextBox94.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter confirmation letter number.','');", True)
                    TextBox94.Focus()
                    Return
                ElseIf TextBox3.Text = "" Or IsDate(TextBox3.Text) = False Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter confirmation letter date.','');", True)
                    TextBox3.Text = ""
                    TextBox3.Focus()
                    Return
                ElseIf TextBox11.Text = "" Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter confirmation letter number.','');", True)
                    TextBox11.Focus()
                    Return
                ElseIf TextBox12.Text = "" Or IsDate(TextBox12.Text) = False Then
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "Window", "window.alert('Please enter confirmation letter date.','');", True)
                    TextBox12.Text = ""
                    TextBox12.Focus()
                    Return

                End If

                working_date = CDate(TextBox1.Text)

                Dim STR1 As String = ""
                If working_date.Date.Month > 3 Then
                    STR1 = working_date.Date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = STR1 & (STR1 + 1)
                ElseIf working_date.Date.Month <= 3 Then
                    STR1 = working_date.Date.Year
                    STR1 = STR1.Trim.Substring(2)
                    STR1 = (STR1 - 1) & STR1
                End If

                Dim BG_TYPE, SYSTEM_BG_NO As New String("")
                BG_TYPE = "BG"
                conn.Open()
                da = New SqlDataAdapter("select distinct BG_NO from BANK_GUARANTEE WITH(NOLOCK) WHERE BG_NO LIKE '" & BG_TYPE & STR1 & "%'", conn)
                count = da.Fill(ds, "BANK_GUARANTEE")
                conn.Close()
                If count = 0 Then
                    SYSTEM_BG_NO = BG_TYPE & STR1 & "000001"
                    TextBox61.Text = SYSTEM_BG_NO
                Else
                    str = count + 1
                    If str.Length = 1 Then
                        str = "00000" & (count + 1)
                    ElseIf str.Length = 2 Then
                        str = "0000" & (count + 1)
                    ElseIf str.Length = 3 Then
                        str = "000" & (count + 1)
                    ElseIf str.Length = 4 Then
                        str = "00" & (count + 1)
                    ElseIf str.Length = 5 Then
                        str = "0" & (count + 1)
                    End If
                    SYSTEM_BG_NO = BG_TYPE & STR1 & str
                    TextBox61.Text = SYSTEM_BG_NO
                End If

                Dim I As Integer
                For I = 0 To GridView2.Rows.Count - 1

                    Dim query As String = "INSERT INTO BANK_GUARANTEE (COMPANY_CONF_LETTER_NO,COMPANY_CONF_LETTER_DATE,BG_STATUS,BG_NO,BG_DATE,ORIGINAL_BG_NO,ORIGINAL_BG_DATE,PARTY_CODE,PARTY_NAME,ORDER_NO,ACTUAL_ORDER_NO,ORDER_DATE,IOC_NO,IOC_DATE,RETURN_BG_IOC_NO,RETURN_BG_IOC_DATE,DEPOSIT_TYPE,BG_TYPE,BG_LOCATION,ISSUING_BANK_NAME,ISSUING_BANK_BRANCH,BG_AMOUNT,BG_VALIDITY,CONF_LETTER_NO,CONF_LETTER_DATE,FISCAL_YEAR,EMP_NAME,ENTRY_DATE)VALUES(@COMPANY_CONF_LETTER_NO,@COMPANY_CONF_LETTER_DATE,@BG_STATUS,@BG_NO,@BG_DATE,@ORIGINAL_BG_NO,@ORIGINAL_BG_DATE,@PARTY_CODE,@PARTY_NAME,@ORDER_NO,@ACTUAL_ORDER_NO,@ORDER_DATE,@IOC_NO,@IOC_DATE,@RETURN_BG_IOC_NO,@RETURN_BG_IOC_DATE,@DEPOSIT_TYPE,@BG_TYPE,@BG_LOCATION,@ISSUING_BANK_NAME,@ISSUING_BANK_BRANCH,@BG_AMOUNT,@BG_VALIDITY,@CONF_LETTER_NO,@CONF_LETTER_DATE,@FISCAL_YEAR,@EMP_NAME,@ENTRY_DATE)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@BG_NO", TextBox61.Text)
                    cmd.Parameters.AddWithValue("@BG_DATE", Date.ParseExact(TextBox1.Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@ORIGINAL_BG_NO", GridView2.Rows(I).Cells(0).Text)
                    cmd.Parameters.AddWithValue("@ORIGINAL_BG_DATE", Date.ParseExact(GridView2.Rows(I).Cells(1).Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@PARTY_CODE", GridView2.Rows(I).Cells(2).Text)
                    cmd.Parameters.AddWithValue("@PARTY_NAME", GridView2.Rows(I).Cells(3).Text)
                    cmd.Parameters.AddWithValue("@ORDER_NO", GridView2.Rows(I).Cells(4).Text)
                    cmd.Parameters.AddWithValue("@ACTUAL_ORDER_NO", GridView2.Rows(I).Cells(5).Text)
                    cmd.Parameters.AddWithValue("@ORDER_DATE", Date.ParseExact(GridView2.Rows(I).Cells(6).Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@IOC_NO", GridView2.Rows(I).Cells(7).Text)
                    cmd.Parameters.AddWithValue("@IOC_DATE", Date.ParseExact(GridView2.Rows(I).Cells(8).Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@RETURN_BG_IOC_NO", GridView2.Rows(I).Cells(9).Text)
                    cmd.Parameters.AddWithValue("@RETURN_BG_IOC_DATE", Date.ParseExact(GridView2.Rows(I).Cells(10).Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@DEPOSIT_TYPE", GridView2.Rows(I).Cells(11).Text)
                    cmd.Parameters.AddWithValue("@BG_TYPE", GridView2.Rows(I).Cells(12).Text)
                    cmd.Parameters.AddWithValue("@BG_LOCATION", GridView2.Rows(I).Cells(13).Text)
                    cmd.Parameters.AddWithValue("@ISSUING_BANK_NAME", GridView2.Rows(I).Cells(14).Text)
                    cmd.Parameters.AddWithValue("@ISSUING_BANK_BRANCH", GridView2.Rows(I).Cells(15).Text)
                    cmd.Parameters.AddWithValue("@BG_AMOUNT", GridView2.Rows(I).Cells(16).Text)
                    cmd.Parameters.AddWithValue("@BG_VALIDITY", Date.ParseExact(GridView2.Rows(I).Cells(17).Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@CONF_LETTER_NO", GridView2.Rows(I).Cells(18).Text)
                    cmd.Parameters.AddWithValue("@CONF_LETTER_DATE", Date.ParseExact(GridView2.Rows(I).Cells(19).Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@FISCAL_YEAR", STR1)
                    cmd.Parameters.AddWithValue("@EMP_NAME", Session("userName"))
                    cmd.Parameters.AddWithValue("@ENTRY_DATE", Date.ParseExact(Now.ToString, "dd-MM-yyyy H:mm:ss", provider))
                    cmd.Parameters.AddWithValue("@BG_STATUS", "ACTIVE")
                    cmd.Parameters.AddWithValue("@COMPANY_CONF_LETTER_NO", GridView2.Rows(I).Cells(20).Text)
                    cmd.Parameters.AddWithValue("@COMPANY_CONF_LETTER_DATE", Date.ParseExact(GridView2.Rows(I).Cells(21).Text, "dd-MM-yyyy", provider))
                    cmd.ExecuteReader()
                    cmd.Dispose()

                Next

                Dim dt7 As New DataTable()
                dt7.Columns.AddRange(New DataColumn(21) {New DataColumn("ORIGINAL_BG_NO"), New DataColumn("ORIGINAL_BG_DATE"), New DataColumn("PARTY_CODE"), New DataColumn("PARTY_NAME"), New DataColumn("ORDER_NO"), New DataColumn("ACTUAL_ORDER_NO"), New DataColumn("ORDER_DATE"), New DataColumn("IOC_NO"), New DataColumn("IOC_DATE"), New DataColumn("RETURN_IOC_NO"), New DataColumn("RETURN_IOC_DATE"), New DataColumn("DEPOSIT_TYPE"), New DataColumn("BG_TYPE"), New DataColumn("BG_LOCATION"), New DataColumn("ISSUING_BANK_NAME"), New DataColumn("ISSUING_BANK_BRANCH"), New DataColumn("BG_AMOUNT"), New DataColumn("BG_VALIDITY"), New DataColumn("CONF_LETTER_NO"), New DataColumn("CONF_LETTER_DATE"), New DataColumn("COMPANY_CONF_LETTER_NO"), New DataColumn("COMPANY_CONF_LETTER_DATE")})
                ViewState("BG") = dt7
                BINDGRID()

                For Each Control In rcd_Panel0.Controls

                    If TypeName(Control) = "TextBox" Then

                        Control.Text = ""

                    End If

                Next

                TextBox61.Text = SYSTEM_BG_NO

                myTrans.Commit()
                Label468.Text = "All records are written to database."
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                TextBox61.Text = ""
                conn.Close()
                conn_trans.Close()
                Label468.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub

    Protected Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        Dim party_code, party_name, order_no, actual_order_no As New String("")
        Dim dd25Text As String() = DropDownList25.Text.Split(",")
        Dim dd62Text As String() = TextBox62.Text.Split(",")

        party_code = dd25Text(0).Trim
        party_name = dd25Text(1).Trim
        order_no = dd62Text(0).Trim
        actual_order_no = dd62Text(1).Trim

        Dim row_count As Integer = GridView2.Rows.Count + 1
        Dim dt As DataTable = DirectCast(ViewState("BG"), DataTable)
        dt.Rows.Add(TextBox4.Text, TextBox10.Text, party_code, party_name, order_no, actual_order_no, TextBox63.Text, TextBox5.Text, TextBox6.Text, TextBox7.Text, TextBox8.Text, DropDownList28.SelectedValue, DropDownList1.SelectedValue, DropDownList2.SelectedValue, TextBox181.Text, TextBox9.Text, TextBox2.Text, TextBox182.Text, TextBox94.Text, TextBox3.Text, TextBox11.Text, TextBox12.Text)
        ViewState("BG") = dt
        BINDGRID()
    End Sub

    Protected Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        Dim objPopUp As New PopupNotifier
        objPopUp.Image = Drawing.Image.FromFile("C:\inetpub\wwwroot\Images\SAIL_Logo_svg.png")
        objPopUp.TitleText = "Hiii"
        objPopUp.ContentText = "This is my first notification!!!"
        objPopUp.Popup()
    End Sub
End Class