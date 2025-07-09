Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Public Class supl
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    Dim dt As New DataTable
    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture
    Dim myTrans As SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("userName") = "" Then
            '    Response.Redirect("~/Account/Login")
            '    Return
            'End If
        End If

        If ((IsDBNull(Session("masterAccess")) Or Session("masterAccess") = "") And (IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        If TextBox114.Text = "" Then
            TextBox114.Focus()
            ERR_LABLE.Visible = True
            ERR_LABLE.Text = "Please enter Admin Password"
            Return
        ElseIf TextBox114.Text <> "123456987" Then
            TextBox114.Text = ""
            TextBox114.Focus()
            ERR_LABLE.Visible = True
            ERR_LABLE.Text = "Incorrect Admin Password"
            Return
        ElseIf DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            ERR_LABLE.Visible = True
            ERR_LABLE.Text = "Please select supplier status."
            Return
        ElseIf TextBox2.Text = "" Then
            TextBox2.Focus()
            ERR_LABLE.Visible = True
            ERR_LABLE.Text = "Please enter Validity."
            Return
        ElseIf TextBox114.Text = "123456987" Then
            Dim newSupplierCode As New String("")
            If (TextBox84.Text = "") Then
                conn.Open()
                count = 0
                da = New SqlDataAdapter("select * from SUPL WHERE SUPL_ID like 'S" & Left(TextBox85.Text, 1) & "%'", conn)
                count = da.Fill(dt)
                conn.Close()
                If count > 0 Then
                    'newMaterialCode = count.ToString("000000")
                    TextBox84.Text = ("S" + Left(TextBox85.Text, 1) + (count + 1).ToString("000")).ToUpper
                Else
                    TextBox84.Text = ("S" + Left(TextBox85.Text, 1) + "001").ToUpper
                End If
            End If
            Using conn_trans

                conn_trans.Open()
                myTrans = conn_trans.BeginTransaction()
                Try
                    'Database updation entry

                    conn.Open()
                    count = 0
                    ds.Tables.Clear()
                    da = New SqlDataAdapter("select SUPL_ID from SUPL WHERE SUPL_ID = '" & TextBox84.Text & "'", conn)
                    da.Fill(ds)
                    count = ds.Tables(0).Rows.Count
                    conn.Close()
                    If count = 0 Then
                        If TextBox84.Text = "" Then
                            TextBox84.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Id "
                            Return
                        ElseIf TextBox85.Text = "" Then
                            TextBox85.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Name "
                            Return
                        ElseIf TextBox111.Text = "" Then
                            '' TextBox85.Focus()
                            '' ERR_LABLE.Visible = True
                            '' ERR_LABLE.Text = " Fill PAN No "
                            '' Return
                        ElseIf TextBox86.Text = "" Then
                            TextBox86.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Contact Person "
                            Return
                        ElseIf SUPLDropDownList17.SelectedValue = "Select" Then
                            SUPLDropDownList17.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Type "
                            Return
                        End If

                        conn.Open()
                        count = 0
                        da = New SqlDataAdapter("select * from SUPL WHERE SUPL_ID='" & TextBox84.Text & "'", conn)
                        count = da.Fill(dt)
                        conn.Close()
                        If count = 1 Then
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = "Supplier Code Already Exist"
                            TextBox84.Focus()
                            Return
                        End If
                        conn.Open()
                        count = 0
                        da = New SqlDataAdapter("select * from SUPL WHERE SUPL_PAN='" & TextBox111.Text & "'", conn)
                        count = da.Fill(dt)
                        conn.Close()
                        If count = 1 Then
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = "Supplier PAN Already Exist"
                            '' TextBox84.Focus()
                            '' Return
                        End If


                        ''save supplier
                        Dim s_tax As String = ""
                        If SUPLDropDownList17.SelectedValue = "In Side State" Then
                            s_tax = "VAT"
                        ElseIf SUPLDropDownList17.SelectedValue = "Out Side State" Then
                            s_tax = "C.S.T."
                        ElseIf SUPLDropDownList17.SelectedValue = "Foreign" Then
                            s_tax = "Foreign"
                        End If
                        'conn_trans.Open()
                        Dim query1 As String = "Insert Into SUPL(PARTY_TYPE,SUPL_ID , SUPL_NAME , SUPL_CONTACT_PERSON , SUPL_AT , SUPL_PO , SUPL_DIST , SUPL_PIN , SUPL_STATE , SUPL_COUNTRY , SUPL_MOB1 , SUPL_MOB2 , SUPL_LAND , SUPL_FAX , SUPL_EMAIL , SUPL_WEB , SUPL_PAN , SUPL_GST_NO , SUPL_STATE_CODE , SUPL_BANK , SUPL_ACOUNT_NO , SUPL_IFSC , SUPL_TYPE , SUPL_TAX) values (@PARTY_TYPE, @SUPL_ID , @SUPL_NAME , @SUPL_CONTACT_PERSON , @SUPL_AT , @SUPL_PO , @SUPL_DIST , @SUPL_PIN , @SUPL_STATE , @SUPL_COUNTRY , @SUPL_MOB1 , @SUPL_MOB2 , @SUPL_LAND , @SUPL_FAX , @SUPL_EMAIL , @SUPL_WEB , @SUPL_PAN , @SUPL_GST_NO , @SUPL_STATE_CODE , @SUPL_BANK , @SUPL_ACOUNT_NO , @SUPL_IFSC , @SUPL_TYPE , @SUPL_TAX)"
                        Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                        cmd1.Parameters.AddWithValue("@SUPL_ID", TextBox84.Text.ToUpper)
                        cmd1.Parameters.AddWithValue("@SUPL_NAME", TextBox85.Text.ToUpper)
                        cmd1.Parameters.AddWithValue("@SUPL_CONTACT_PERSON", TextBox86.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_AT", TextBox99.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_PO", TextBox100.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_DIST", TextBox101.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_PIN", TextBox102.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_STATE", DropDownList1.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_COUNTRY", TextBox104.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_MOB1", TextBox93.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_MOB2", TextBox94.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_LAND", TextBox95.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_FAX", TextBox105.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_EMAIL", TextBox106.Text.ToLower)
                        cmd1.Parameters.AddWithValue("@SUPL_WEB", TextBox107.Text.ToLower)
                        cmd1.Parameters.AddWithValue("@SUPL_PAN", TextBox111.Text.ToUpper)
                        cmd1.Parameters.AddWithValue("@SUPL_GST_NO", TextBox112.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_STATE_CODE", TextBox113.Text.ToUpper)
                        cmd1.Parameters.AddWithValue("@SUPL_BANK", TextBox108.Text.ToUpper)
                        cmd1.Parameters.AddWithValue("@SUPL_ACOUNT_NO", TextBox109.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_IFSC", TextBox110.Text.ToUpper)
                        cmd1.Parameters.AddWithValue("@SUPL_TYPE", SUPLDropDownList17.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_TAX", s_tax)
                        cmd1.Parameters.AddWithValue("@PARTY_TYPE", DropDownList2.Text)
                        cmd1.ExecuteReader()
                        cmd1.Dispose()
                        'conn_trans.Close()
                        ''TextBox84.Text = ""
                        TextBox85.Text = ""
                        TextBox86.Text = ""
                        TextBox99.Text = ""
                        TextBox100.Text = ""
                        TextBox101.Text = ""
                        TextBox102.Text = ""
                        TextBox104.Text = ""
                        TextBox93.Text = ""
                        TextBox94.Text = ""
                        TextBox95.Text = ""
                        TextBox105.Text = ""
                        TextBox106.Text = ""
                        TextBox107.Text = ""
                        TextBox108.Text = ""
                        TextBox109.Text = ""
                        TextBox110.Text = ""
                        TextBox111.Text = ""
                        TextBox112.Text = ""
                        TextBox113.Text = ""
                        SUPLDropDownList17.SelectedValue = "Select"
                        DropDownList1.SelectedValue = "Select"
                        ERR_LABLE.Visible = True
                        ERR_LABLE.Text = "Data Saved"
                    Else
                        If TextBox84.Text = "" Then
                            TextBox84.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Id "
                            Return
                        ElseIf TextBox85.Text = "" Then
                            TextBox85.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Name "
                            Return
                        ElseIf TextBox111.Text = "" Then
                            ''  TextBox85.Focus()
                            '' ERR_LABLE.Visible = True
                            '' ERR_LABLE.Text = " Fill PAN No "
                            ''  Return

                        ElseIf TextBox86.Text = "" Then
                            TextBox86.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Contact Person "
                            Return
                        ElseIf SUPLDropDownList17.SelectedValue = "Select" Then
                            SUPLDropDownList17.Focus()
                            ERR_LABLE.Visible = True
                            ERR_LABLE.Text = " Fill Supplier Type "
                            Return
                        End If
                        conn.Open()
                        count = 0
                        da = New SqlDataAdapter("select * from SUPL WHERE SUPL_PAN='" & TextBox111.Text & "' AND SUPL_ID <> '" & TextBox84.Text & "'", conn)
                        count = da.Fill(dt)
                        conn.Close()
                        If count = 1 Then
                            ''  ERR_LABLE.Visible = True
                            ''  ERR_LABLE.Text = "Supplier PAN Already Exit "
                            ''  TextBox84.Focus()
                            '' Return
                        End If
                        Dim s_tax As String = ""
                        If SUPLDropDownList17.SelectedValue = "Within State" Then
                            s_tax = "VAT"
                        ElseIf SUPLDropDownList17.SelectedValue = "Out Side State" Then
                            s_tax = "C.S.T."
                        ElseIf SUPLDropDownList17.SelectedValue = "Foreign" Then
                            s_tax = "Foreign"
                        End If
                        'conn_trans.Open()
                        Dim query1 As String = "UPDATE SUPL SET MSME_NO=@MSME_NO,PARTY_TYPE=@PARTY_TYPE,SUPL_NAME=@SUPL_NAME , SUPL_CONTACT_PERSON =@SUPL_CONTACT_PERSON , SUPL_AT=@SUPL_AT , SUPL_PO =@SUPL_PO , SUPL_DIST=@SUPL_DIST , SUPL_PIN =@SUPL_PIN, SUPL_STATE=@SUPL_STATE , SUPL_COUNTRY=@SUPL_COUNTRY , SUPL_MOB1=@SUPL_MOB1 , SUPL_MOB2 =@SUPL_MOB2, SUPL_LAND=@SUPL_LAND , SUPL_FAX =@SUPL_FAX, SUPL_EMAIL=@SUPL_EMAIL , SUPL_WEB=@SUPL_WEB , SUPL_PAN=@SUPL_PAN , SUPL_GST_NO= @SUPL_GST_NO , SUPL_STATE_CODE=@SUPL_STATE_CODE , SUPL_BANK=@SUPL_BANK , SUPL_ACOUNT_NO=@SUPL_ACOUNT_NO , SUPL_IFSC=@SUPL_IFSC , SUPL_TYPE=@SUPL_TYPE , SUPL_TAX=@SUPL_TAX where SUPL_ID='" & TextBox84.Text & "'"
                        Dim cmd1 As New SqlCommand(query1, conn_trans, myTrans)
                        cmd1.Parameters.AddWithValue("@SUPL_NAME", TextBox85.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_CONTACT_PERSON", TextBox86.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_AT", TextBox99.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_PO", TextBox100.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_DIST", TextBox101.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_PIN", TextBox102.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_STATE", DropDownList1.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_COUNTRY", TextBox104.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_MOB1", TextBox93.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_MOB2", TextBox94.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_LAND", TextBox95.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_FAX", TextBox105.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_EMAIL", TextBox106.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_WEB", TextBox107.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_PAN", TextBox111.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_GST_NO", TextBox112.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_STATE_CODE", TextBox113.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_BANK", TextBox108.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_ACOUNT_NO", TextBox109.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_IFSC", TextBox110.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_TYPE", SUPLDropDownList17.Text)
                        cmd1.Parameters.AddWithValue("@SUPL_TAX", s_tax)
                        cmd1.Parameters.AddWithValue("@PARTY_TYPE", DropDownList2.Text)
                        cmd1.Parameters.AddWithValue("@MSME_NO", TextBox1.Text)
                        cmd1.ExecuteReader()
                        cmd1.Dispose()

                        TextBox84.Text = ""
                        TextBox85.Text = ""
                        TextBox86.Text = ""
                        TextBox99.Text = ""
                        TextBox100.Text = ""
                        TextBox101.Text = ""
                        TextBox102.Text = ""
                        'TextBox103.Text = ""
                        TextBox104.Text = ""
                        TextBox93.Text = ""
                        TextBox94.Text = ""
                        TextBox95.Text = ""
                        TextBox105.Text = ""
                        TextBox106.Text = ""
                        TextBox107.Text = ""
                        TextBox108.Text = ""
                        TextBox109.Text = ""
                        TextBox110.Text = ""
                        TextBox111.Text = ""
                        TextBox112.Text = ""
                        TextBox113.Text = ""
                        SUPLDropDownList17.SelectedValue = "Select"
                        DropDownList1.SelectedValue = "Select"

                    End If

                    myTrans.Commit()
                    conn_trans.Close()
                    ERR_LABLE.Visible = True
                    ERR_LABLE.Text = "All records are written to database."
                    result = True
                Catch ee As Exception
                    ' Roll back the transaction. 
                    myTrans.Rollback()
                    conn.Close()
                    conn_trans.Close()
                    ERR_LABLE.Visible = True
                    ERR_LABLE.Text = "There was some Error, please contact EDP."
                Finally
                    conn.Close()
                    conn_trans.Close()
                End Try

            End Using


        End If

    End Sub

    Protected Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        TextBox84.Text = ""
        TextBox85.Text = ""
        TextBox86.Text = ""
        TextBox99.Text = ""
        TextBox100.Text = ""
        TextBox101.Text = ""
        TextBox102.Text = ""
        'TextBox103.Text = ""
        TextBox104.Text = ""
        TextBox93.Text = ""
        TextBox94.Text = ""
        TextBox95.Text = ""
        TextBox105.Text = ""
        TextBox106.Text = ""
        TextBox107.Text = ""
        TextBox108.Text = ""
        TextBox109.Text = ""
        TextBox110.Text = ""
        TextBox111.Text = ""
        TextBox112.Text = ""
        TextBox113.Text = ""
        SUPLDropDownList17.SelectedValue = "Select"
        DropDownList1.SelectedValue = "Select"
    End Sub

    Protected Sub SUPLDropDownList17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SUPLDropDownList17.SelectedIndexChanged
        If SUPLDropDownList17.SelectedValue = "Within State" Then
            'TextBox103.Text = "Chhattisgarh"
            DropDownList1.SelectedValue = "CHHATTISGARH"
            TextBox104.Text = "India"
            'TextBox103.ReadOnly = True

            TextBox104.ReadOnly = True
        ElseIf SUPLDropDownList17.SelectedValue = "Out Side State" Then
            'TextBox103.Text = ""
            DropDownList1.SelectedValue = "SELECT"
            TextBox104.Text = "India"
            'TextBox103.ReadOnly = False
            TextBox104.ReadOnly = True
        ElseIf SUPLDropDownList17.SelectedValue = "Foreign" Then
            DropDownList1.SelectedValue = "OUTSIDE INDIA"
            'TextBox103.Text = ""
            TextBox104.Text = ""
            'TextBox103.ReadOnly = False
            TextBox104.ReadOnly = False
        End If
    End Sub
End Class


'Imports System.Globalization
'Imports Microsoft.VisualBasic
'Imports System
'Imports System.Data
'Imports System.Data.SqlClient
'Imports System.Data.DataSet
'Imports System.Data.SqlTypes
'Imports System.Configuration
'Public Class supl
'    Inherits System.Web.UI.Page
'    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
'    Dim count As Integer
'    Dim dr As SqlDataReader
'    Dim mycommand As New SqlCommand
'    Dim ds As New DataSet()
'    Dim da As New SqlDataAdapter
'    Dim dt As New DataTable
'    Dim str As String
'    Dim result As Integer
'    Dim provider As CultureInfo = CultureInfo.InvariantCulture
'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        If Not IsPostBack Then
'            If Session("userName") = "" Then
'                Response.Redirect("~/Account/Login")
'                Return
'            End If
'        End If
'    End Sub

'    Protected Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
'        If TextBox114.Text = "" Then
'            TextBox114.Focus()
'            ERR_LABLE.Visible = True
'            ERR_LABLE.Text = "Please enter Admin Password"
'            Return
'        ElseIf TextBox114.Text <> "123456987" Then
'            TextBox114.Text = ""
'            TextBox114.Focus()
'            ERR_LABLE.Visible = True
'            ERR_LABLE.Text = "Incorrect Admin Password"
'            Return
'        ElseIf TextBox114.Text = "123456987" Then

'            conn.Open()
'            count = 0
'            ds.Tables.Clear()
'            da = New SqlDataAdapter("select SUPL_ID from SUPL WHERE SUPL_ID = '" & TextBox84.Text & "'", conn)
'            count = da.Fill(ds, "SUPL")
'            conn.Close()
'            If count = 0 Then
'                If TextBox84.Text = "" Then
'                    TextBox84.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Id "
'                    Return
'                ElseIf TextBox85.Text = "" Then
'                    TextBox85.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Name "
'                    Return
'                ElseIf TextBox111.Text = "" Then
'                    '' TextBox85.Focus()
'                    ''  ERR_LABLE.Visible = True
'                    '' ERR_LABLE.Text = " Fill PAN No "
'                    '' Return
'                ElseIf TextBox86.Text = "" Then
'                    TextBox86.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Contact Person "
'                    Return
'                ElseIf SUPLDropDownList17.SelectedValue = "Select" Then
'                    SUPLDropDownList17.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Type "
'                    Return
'                End If
'                conn.Open()
'                count = 0
'                da = New SqlDataAdapter("select * from SUPL WHERE SUPL_ID='" & TextBox84.Text & "'", conn)
'                count = da.Fill(dt)
'                conn.Close()
'                If count = 1 Then
'                    '' ERR_LABLE.Visible = True
'                    '' ERR_LABLE.Text = "Supplier Id Already Exit "
'                    '' TextBox84.Focus()
'                    '' Return
'                End If
'                conn.Open()
'                count = 0
'                da = New SqlDataAdapter("select * from SUPL WHERE SUPL_ID='" & TextBox84.Text & "'", conn)
'                count = da.Fill(dt)
'                conn.Close()
'                If count = 1 Then
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = "Supplier Code Already Exit "
'                    TextBox84.Focus()
'                    Return
'                End If
'                conn.Open()
'                count = 0
'                da = New SqlDataAdapter("select * from SUPL WHERE SUPL_PAN='" & TextBox111.Text & "'", conn)
'                count = da.Fill(dt)
'                conn.Close()
'                If count = 1 Then
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = "Supplier PAN Already Exit "
'                    '' TextBox84.Focus()
'                    '' Return
'                End If
'                ''save supplier
'                Dim s_tax As String = ""
'                If SUPLDropDownList17.SelectedValue = "In Side State" Then
'                    s_tax = "VAT"
'                ElseIf SUPLDropDownList17.SelectedValue = "Out Side State" Then
'                    s_tax = "C.S.T."
'                ElseIf SUPLDropDownList17.SelectedValue = "Foreign" Then
'                    s_tax = "Foreign"
'                End If
'                conn.Open()
'                Dim query1 As String = "Insert Into SUPL(SUPL_ID , SUPL_NAME , SUPL_CONTACT_PERSON , SUPL_AT , SUPL_PO , SUPL_DIST , SUPL_PIN , SUPL_STATE , SUPL_COUNTRY , SUPL_MOB1 , SUPL_MOB2 , SUPL_LAND , SUPL_FAX , SUPL_EMAIL , SUPL_WEB , SUPL_PAN , SUPL_GST_NO , SUPL_STATE_CODE , SUPL_BANK , SUPL_ACOUNT_NO , SUPL_IFSC , SUPL_TYPE , SUPL_TAX) values (@SUPL_ID , @SUPL_NAME , @SUPL_CONTACT_PERSON , @SUPL_AT , @SUPL_PO , @SUPL_DIST , @SUPL_PIN , @SUPL_STATE , @SUPL_COUNTRY , @SUPL_MOB1 , @SUPL_MOB2 , @SUPL_LAND , @SUPL_FAX , @SUPL_EMAIL , @SUPL_WEB , @SUPL_PAN , @SUPL_GST_NO , @SUPL_STATE_CODE , @SUPL_BANK , @SUPL_ACOUNT_NO , @SUPL_IFSC , @SUPL_TYPE , @SUPL_TAX)"
'                Dim cmd1 As New SqlCommand(query1, conn)
'                cmd1.Parameters.AddWithValue("@SUPL_ID", TextBox84.Text.ToUpper)
'                cmd1.Parameters.AddWithValue("@SUPL_NAME", TextBox85.Text.ToUpper)
'                cmd1.Parameters.AddWithValue("@SUPL_CONTACT_PERSON", TextBox86.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_AT", TextBox99.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_PO", TextBox100.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_DIST", TextBox101.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_PIN", TextBox102.Text)
'                'cmd1.Parameters.AddWithValue("@SUPL_STATE", TextBox103.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_STATE", DropDownList1.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_COUNTRY", TextBox104.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_MOB1", TextBox93.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_MOB2", TextBox94.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_LAND", TextBox95.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_FAX", TextBox105.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_EMAIL", TextBox106.Text.ToLower)
'                cmd1.Parameters.AddWithValue("@SUPL_WEB", TextBox107.Text.ToLower)
'                cmd1.Parameters.AddWithValue("@SUPL_PAN", TextBox111.Text.ToUpper)
'                cmd1.Parameters.AddWithValue("@SUPL_GST_NO", TextBox112.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_STATE_CODE", TextBox113.Text.ToUpper)
'                cmd1.Parameters.AddWithValue("@SUPL_BANK", TextBox108.Text.ToUpper)
'                cmd1.Parameters.AddWithValue("@SUPL_ACOUNT_NO", TextBox109.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_IFSC", TextBox110.Text.ToUpper)
'                cmd1.Parameters.AddWithValue("@SUPL_TYPE", SUPLDropDownList17.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_TAX", s_tax)
'                cmd1.ExecuteReader()
'                cmd1.Dispose()
'                conn.Close()
'                TextBox84.Text = ""
'                TextBox85.Text = ""
'                TextBox86.Text = ""
'                TextBox99.Text = ""
'                TextBox100.Text = ""
'                TextBox101.Text = ""
'                TextBox102.Text = ""
'                'TextBox103.Text = ""
'                TextBox104.Text = ""
'                TextBox93.Text = ""
'                TextBox94.Text = ""
'                TextBox95.Text = ""
'                TextBox105.Text = ""
'                TextBox106.Text = ""
'                TextBox107.Text = ""
'                TextBox108.Text = ""
'                TextBox109.Text = ""
'                TextBox110.Text = ""
'                TextBox111.Text = ""
'                TextBox112.Text = ""
'                TextBox113.Text = ""
'                SUPLDropDownList17.SelectedValue = "Select"
'                DropDownList1.SelectedValue = "Select"
'                ERR_LABLE.Visible = True
'                ERR_LABLE.Text = "Data Saved"
'            Else
'                If TextBox84.Text = "" Then
'                    TextBox84.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Id "
'                    Return
'                ElseIf TextBox85.Text = "" Then
'                    TextBox85.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Name "
'                    Return
'                ElseIf TextBox111.Text = "" Then
'                    ''  TextBox85.Focus()
'                    '' ERR_LABLE.Visible = True
'                    '' ERR_LABLE.Text = " Fill PAN No "
'                    ''  Return

'                ElseIf TextBox86.Text = "" Then
'                    TextBox86.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Contact Person "
'                    Return
'                ElseIf SUPLDropDownList17.SelectedValue = "Select" Then
'                    SUPLDropDownList17.Focus()
'                    ERR_LABLE.Visible = True
'                    ERR_LABLE.Text = " Fill Supplier Type "
'                    Return
'                End If
'                conn.Open()
'                count = 0
'                da = New SqlDataAdapter("select * from SUPL WHERE SUPL_PAN='" & TextBox111.Text & "' AND SUPL_ID <> '" & TextBox84.Text & "'", conn)
'                count = da.Fill(dt)
'                conn.Close()
'                If count = 1 Then
'                    ''  ERR_LABLE.Visible = True
'                    ''  ERR_LABLE.Text = "Supplier PAN Already Exit "
'                    ''  TextBox84.Focus()
'                    '' Return
'                End If
'                Dim s_tax As String = ""
'                If SUPLDropDownList17.SelectedValue = "Within State" Then
'                    s_tax = "VAT"
'                ElseIf SUPLDropDownList17.SelectedValue = "Out Side State" Then
'                    s_tax = "C.S.T."
'                ElseIf SUPLDropDownList17.SelectedValue = "Foreign" Then
'                    s_tax = "Foreign"
'                End If
'                conn.Open()
'                Dim query1 As String = "UPDATE  SUPL SET SUPL_NAME=@SUPL_NAME , SUPL_CONTACT_PERSON =@SUPL_CONTACT_PERSON , SUPL_AT=@SUPL_AT , SUPL_PO =@SUPL_PO , SUPL_DIST=@SUPL_DIST , SUPL_PIN =@SUPL_PIN, SUPL_STATE=@SUPL_STATE , SUPL_COUNTRY=@SUPL_COUNTRY , SUPL_MOB1=@SUPL_MOB1 , SUPL_MOB2 =@SUPL_MOB2, SUPL_LAND=@SUPL_LAND , SUPL_FAX =@SUPL_FAX, SUPL_EMAIL=@SUPL_EMAIL , SUPL_WEB=@SUPL_WEB , SUPL_PAN=@SUPL_PAN , SUPL_GST_NO= @SUPL_GST_NO , SUPL_STATE_CODE=@SUPL_STATE_CODE , SUPL_BANK=@SUPL_BANK , SUPL_ACOUNT_NO=@SUPL_ACOUNT_NO , SUPL_IFSC=@SUPL_IFSC , SUPL_TYPE=@SUPL_TYPE , SUPL_TAX=@SUPL_TAX where SUPL_ID='" & TextBox84.Text & "'"
'                Dim cmd1 As New SqlCommand(query1, conn)
'                cmd1.Parameters.AddWithValue("@SUPL_NAME", TextBox85.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_CONTACT_PERSON", TextBox86.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_AT", TextBox99.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_PO", TextBox100.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_DIST", TextBox101.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_PIN", TextBox102.Text)
'                'cmd1.Parameters.AddWithValue("@SUPL_STATE", TextBox103.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_STATE", DropDownList1.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_COUNTRY", TextBox104.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_MOB1", TextBox93.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_MOB2", TextBox94.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_LAND", TextBox95.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_FAX", TextBox105.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_EMAIL", TextBox106.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_WEB", TextBox107.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_PAN", TextBox111.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_GST_NO", TextBox112.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_STATE_CODE", TextBox113.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_BANK", TextBox108.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_ACOUNT_NO", TextBox109.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_IFSC", TextBox110.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_TYPE", SUPLDropDownList17.Text)
'                cmd1.Parameters.AddWithValue("@SUPL_TAX", s_tax)
'                cmd1.ExecuteReader()
'                cmd1.Dispose()
'                conn.Close()
'                TextBox84.Text = ""
'                TextBox85.Text = ""
'                TextBox86.Text = ""
'                TextBox99.Text = ""
'                TextBox100.Text = ""
'                TextBox101.Text = ""
'                TextBox102.Text = ""
'                'TextBox103.Text = ""
'                TextBox104.Text = ""
'                TextBox93.Text = ""
'                TextBox94.Text = ""
'                TextBox95.Text = ""
'                TextBox105.Text = ""
'                TextBox106.Text = ""
'                TextBox107.Text = ""
'                TextBox108.Text = ""
'                TextBox109.Text = ""
'                TextBox110.Text = ""
'                TextBox111.Text = ""
'                TextBox112.Text = ""
'                TextBox113.Text = ""
'                SUPLDropDownList17.SelectedValue = "Select"
'                DropDownList1.SelectedValue = "Select"
'                ERR_LABLE.Visible = True
'                ERR_LABLE.Text = "Data Updated"
'            End If
'        End If

'    End Sub

'    Protected Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
'        TextBox84.Text = ""
'        TextBox85.Text = ""
'        TextBox86.Text = ""
'        TextBox99.Text = ""
'        TextBox100.Text = ""
'        TextBox101.Text = ""
'        TextBox102.Text = ""
'        'TextBox103.Text = ""
'        TextBox104.Text = ""
'        TextBox93.Text = ""
'        TextBox94.Text = ""
'        TextBox95.Text = ""
'        TextBox105.Text = ""
'        TextBox106.Text = ""
'        TextBox107.Text = ""
'        TextBox108.Text = ""
'        TextBox109.Text = ""
'        TextBox110.Text = ""
'        TextBox111.Text = ""
'        TextBox112.Text = ""
'        TextBox113.Text = ""
'        SUPLDropDownList17.SelectedValue = "Select"
'        DropDownList1.SelectedValue = "Select"
'    End Sub

'    Protected Sub SUPLDropDownList17_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SUPLDropDownList17.SelectedIndexChanged
'        If SUPLDropDownList17.SelectedValue = "Within State" Then
'            'TextBox103.Text = "Chhattisgarh"
'            DropDownList1.SelectedValue = "CHHATTISGARH"
'            TextBox104.Text = "India"
'            'TextBox103.ReadOnly = True

'            TextBox104.ReadOnly = True
'        ElseIf SUPLDropDownList17.SelectedValue = "Out Side State" Then
'            'TextBox103.Text = ""
'            DropDownList1.SelectedValue = "SELECT"
'            TextBox104.Text = "India"
'            'TextBox103.ReadOnly = False
'            TextBox104.ReadOnly = True
'        ElseIf SUPLDropDownList17.SelectedValue = "Foreign" Then
'            DropDownList1.SelectedValue = "OUTSIDE INDIA"
'            'TextBox103.Text = ""
'            TextBox104.Text = ""
'            'TextBox103.ReadOnly = False
'            TextBox104.ReadOnly = False
'        End If
'    End Sub
'End Class