Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class inv_entry
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
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

            TextBox27_CalendarExtender.EndDate = DateTime.Now.Date
        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("financeAccess")) Or Session("financeAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub DropDownList17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList17.SelectedIndexChanged
        If DropDownList17.SelectedValue = "Select" Then
            DropDownList17.Focus()
            Return
        ElseIf DropDownList17.SelectedValue = "Supplier" Then
            conn.Open()
            DropDownList10.Items.Clear()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct PO_RCD_MAT.PO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL  as SEARCH_PO FROM PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT .PO_NO =ORDER_DETAILS .SO_NO where PO_RCD_MAT.GARN_NO<>'Pending' and (SO_STATUS='RC' OR SO_STATUS='DRAFT' OR SO_STATUS='RCW' OR SO_STATUS='RCM' or SO_STATUS='ACTIVE') AND PO_RCD_MAT.v_ind is null ORDER BY SEARCH_PO", conn)
            da.Fill(dt)
            DropDownList10.DataSource = dt
            DropDownList10.DataValueField = "SEARCH_PO"
            DropDownList10.DataBind()
            conn.Close()
            DropDownList10.Items.Add("Select")
            DropDownList10.SelectedValue = ("Select")
        ElseIf DropDownList17.SelectedValue = "Contractor" Then
            conn.Open()
            DropDownList10.Items.Clear()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct MB_BOOK.PO_NO + ' , ' + ORDER_DETAILS .SO_ACTUAL as SEARCH_PO FROM mb_book JOIN ORDER_DETAILS ON mb_book .po_no =ORDER_DETAILS .SO_NO WHERE mb_book.v_ind is null and (SO_STATUS='RCW' OR SO_STATUS='ACTIVE') ORDER BY SEARCH_PO", conn)
            da.Fill(dt)
            DropDownList10.DataSource = dt
            DropDownList10.DataValueField = "SEARCH_PO"
            DropDownList10.DataBind()
            conn.Close()
            DropDownList10.Items.Add("Select")
            DropDownList10.SelectedValue = ("Select")
        End If
    End Sub

    Protected Sub DropDownList10_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList10.SelectedIndexChanged
        If DropDownList17.SelectedValue = "Select" Then
            DropDownList17.Focus()
            Return
        ElseIf DropDownList17.SelectedValue = "Supplier" Then
            conn.Open()
            Dim SUPL_NAME As String = ""
            Dim mc As New SqlCommand
            mc.CommandText = "select SUPL.SUPL_NAME,SUPL.SUPL_ID from ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID where ORDER_DETAILS.so_no ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            mc.Connection = conn
            dr = mc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                SUPL_NAME = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
                dr.Close()
            End If
            conn.Close()
            TextBox42.Text = SUPL_NAME
        ElseIf DropDownList17.SelectedValue = "Transporter" Then
            conn.Open()
            Dim SUPL_NAME As String = ""
            Dim mc As New SqlCommand
            mc.CommandText = "select SUPL.SUPL_NAME,SUPL.SUPL_ID from ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID where ORDER_DETAILS.so_no ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            mc.Connection = conn
            dr = mc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                SUPL_NAME = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
                dr.Close()
            End If
            conn.Close()
            TextBox42.Text = SUPL_NAME
        ElseIf DropDownList17.SelectedValue = "Contractor" Then
            conn.Open()
            Dim SUPL_NAME As String = ""
            Dim mc As New SqlCommand
            mc.CommandText = "select SUPL.SUPL_NAME,SUPL.SUPL_ID from ORDER_DETAILS JOIN SUPL ON ORDER_DETAILS.PARTY_CODE=SUPL.SUPL_ID where ORDER_DETAILS.so_no ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "'"
            mc.Connection = conn
            dr = mc.ExecuteReader
            If dr.HasRows Then
                dr.Read()
                SUPL_NAME = dr.Item("SUPL_ID") & " , " & dr.Item("SUPL_NAME")
                dr.Close()
            End If
            conn.Close()
            TextBox42.Text = SUPL_NAME
        End If

        Dim FINANCE_ARRANGE As String = ""
        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "select  ORDER_DETAILS . PAYMENT_MODE FROM ORDER_DETAILS WHERE SO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1).Trim & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            FINANCE_ARRANGE = dr.Item("PAYMENT_MODE")
            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()
        If (FINANCE_ARRANGE = "ADVANCE" Or FINANCE_ARRANGE = "Advance Payment") Then
            conn.Open()
            dt.Clear()
            da = New SqlDataAdapter("SELECT distinct (VOUCHER_TYPE + VOUCHER_NO) AS VOUCHER_NO FROM SALE_RCD_VOUCHAR WHERE SO_NO ='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1).Trim & "' AND VOUCHER_STATUS='PENDING'", conn)
            da.Fill(dt)
            conn.Close()
            DropDownList1.Items.Clear()
            DropDownList1.DataSource = dt
            DropDownList1.DataValueField = "VOUCHER_NO"
            DropDownList1.DataBind()
            DropDownList1.Items.Insert(0, "Select")
            DropDownList1.SelectedValue = "Select"
            TextBox2.Text = "N/A"
            TextBox2.ReadOnly = True
            DropDownList2.Enabled = False
        Else

            DropDownList1.Items.Clear()
            DropDownList1.Items.Add("N/A")
            DropDownList1.SelectedValue = "N/A"
            TextBox1.Text = ""
            DropDownList2.Enabled = True

        End If
    End Sub

    Protected Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        If (Len(TextBox26.Text) > 16) Then
            Label552.Text = "Invoice number cannot be more than 16 characters."
        ElseIf TextBox26.Text.Contains(" ") Then
            Label552.Text = "Space is not allowed in supplier's invoice number"
        ElseIf TextBox26.Text.Contains(".") Then
            Label552.Text = "Please enter invoice number in correct format"
        Else
            Label552.Text = ""
            Using conn_trans
                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()

                Try
                    'Database updation entry
                    Dim working_date As Date
                    working_date = Today.Date
                    If DropDownList10.SelectedValue = "Select" Then
                        DropDownList10.Focus()
                        Label552.Text = "Please Select Order No."
                        Return
                    ElseIf DropDownList1.SelectedValue = "Select" Then
                        DropDownList1.Focus()
                        Label552.Text = "Please Select Advance Voucher No."
                        Return
                    ElseIf TextBox26.Text = "" Then
                        Label552.Text = "Please enter invoice No."
                        TextBox26.Focus()
                        Return
                    ElseIf IsDate(TextBox27.Text) = False Then
                        Label552.Text = "Please Select a valid Date format."
                        TextBox27.Focus()
                        Return
                    ElseIf IsNumeric(TextBox30.Text) = False Then
                        Label552.Text = "Please enter valid amount."
                        TextBox30.Focus()
                        Return
                    ElseIf (DropDownList1.SelectedValue = "N/A") Then
                        If TextBox2.Text = "" Then
                            TextBox2.Focus()
                            Label552.Text = "Please enter IOC No."
                            Return
                        End If

                    End If
                    conn.Open()
                    dt.Clear()
                    count = 0
                    da = New SqlDataAdapter("Select distinct bill_id from inv_data", conn)
                    count = da.Fill(dt)
                    conn.Close()
                    TextBox28.Text = count + 1

                    Dim query As String = "INSERT INTO inv_data (AGING_FLAG,bill_id,po_no,inv_no,inv_date,post_date,inv_amount,emp_id)VALUES(@AGING_FLAG,@bill_id,@po_no,@inv_no,@inv_date,@post_date,@inv_amount,@emp_id)"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@bill_id", TextBox28.Text)
                    cmd.Parameters.AddWithValue("@po_no", DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1))
                    cmd.Parameters.AddWithValue("@inv_no", TextBox26.Text)
                    cmd.Parameters.AddWithValue("@inv_date", Date.ParseExact(TextBox27.Text, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@post_date", Date.ParseExact(working_date.Date, "dd-MM-yyyy", provider))
                    cmd.Parameters.AddWithValue("@inv_amount", TextBox30.Text)
                    cmd.Parameters.AddWithValue("@emp_id", Session("userName"))
                    If (DropDownList1.SelectedValue <> "Select" And DropDownList1.SelectedValue <> "N/A") Then
                        cmd.Parameters.AddWithValue("@AGING_FLAG", DropDownList1.SelectedValue)
                    ElseIf (TextBox2.Text <> "N/A") Then
                        cmd.Parameters.AddWithValue("@AGING_FLAG", TextBox2.Text)
                    Else
                        cmd.Parameters.AddWithValue("@AGING_FLAG", TextBox26.Text)
                    End If

                    cmd.ExecuteReader()
                    cmd.Dispose()

                    TextBox26.Text = ""
                    TextBox27.Text = ""
                    TextBox30.Text = ""
                    myTrans.Commit()
                    Label552.Text = "All records are written To database."
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    TextBox28.Text = ""
                    Label552.Text = "There was some Error, please contact EDP."
                Finally
                    conn.Close()
                    conn_trans.Close()
                End Try

            End Using
        End If


    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged


        Dim mc1 As New SqlCommand
        conn.Open()
        mc1.CommandText = "Select SUM(TOTAL_AMT) As TOTAL_AMT from SALE_RCD_VOUCHAR WHERE VOUCHER_TYPE+VOUCHER_NO='" & DropDownList1.SelectedValue & "' AND SO_NO='" & DropDownList10.Text.Substring(0, DropDownList10.Text.IndexOf(",") - 1) & "' AND VOUCHER_STATUS='PENDING'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (IsDBNull(dr.Item("TOTAL_AMT"))) Then
                TextBox1.Text = 0.00
            Else
                TextBox1.Text = dr.Item("TOTAL_AMT")
            End If

            dr.Close()
        Else
            dr.Close()
        End If
        conn.Close()

    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged

        If DropDownList2.SelectedValue = "Select" Then
            DropDownList2.Focus()
            Label552.Text = "Please select if IOC number is available or not."
            Return

        ElseIf DropDownList2.SelectedValue = "YES" Then
            TextBox2.ReadOnly = False
            TextBox2.Text = ""
        ElseIf DropDownList2.SelectedValue = "NO" Then
            TextBox2.ReadOnly = True
            TextBox2.Text = "N/A"

        End If
    End Sub
End Class