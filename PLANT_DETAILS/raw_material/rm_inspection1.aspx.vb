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

Public Class rm_inspection1
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)

    Dim dt As New DataTable
    Dim count As Integer
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Dim da As New SqlDataAdapter

    Dim str As String
    Dim result As Integer
    Dim provider As CultureInfo = CultureInfo.InvariantCulture


    Protected Sub BINDGRID()
        GridView1.DataSource = DirectCast(ViewState("INSP"), DataTable)
        GridView1.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Dim dt7 As New DataTable()
            dt7.Columns.AddRange(New DataColumn(5) {New DataColumn("SR No."), New DataColumn("Unit"), New DataColumn("Characteristics"), New DataColumn("Gauranted Specification As Per PO"), New DataColumn("Rebate Clause Value As Per PO"), New DataColumn("Test Result At SRU Lab")})
            ViewState("INSP") = dt7
            BINDGRID()

            conn.Open()
            Dim ds5 As New DataSet
            da = New SqlDataAdapter("select distinct PO_RCD_MAT.CRR_NO from  PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO  where PO_RCD_MAT.INSP_EMP is null AND (ORDER_DETAILS.PO_TYPE='RAW MATERIAL' or ORDER_DETAILS .PO_TYPE ='RAW MATERIAL(IMP)') ORDER BY PO_RCD_MAT.CRR_NO", conn)
            da.Fill(ds5, "PO_RCD_MAT")
            DropDownList3.DataSource = ds5.Tables("PO_RCD_MAT")
            DropDownList3.DataValueField = "CRR_NO"
            DropDownList3.DataBind()
            conn.Close()
            DropDownList3.Items.Add("Select")
            DropDownList3.SelectedValue = "Select"
            DropDownList3.Enabled = True

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If

    End Sub



    Protected Sub Button50_Click(sender As Object, e As EventArgs) Handles Button50.Click
        If properties_DropDownList.Text = "Select" Then
            Label2.Visible = True
            properties_DropDownList.Focus()
            Label2.Text = "Please Select Characteristics"
            Return
        ElseIf TextBox1.Text = "" Then
            Label2.Visible = True
            TextBox1.Focus()
            TextBox1.Text = ""
            Label2.Text = "Please Enter Specification as per PO"
            Return
        ElseIf TextBox2.Text = "" Then
            Label2.Visible = True
            TextBox2.Focus()
            TextBox2.Text = ""
            Label2.Text = "Please Enter Acceptable limit"
            Return
        ElseIf TextBox3.Text = "" Then
            Label2.Visible = True
            TextBox3.Focus()
            TextBox3.Text = ""
            Label2.Text = "Please Enter Test result obtained at SRU Lab"
            Return
        End If

        ''insert grid view
        'count = (GridView1.Rows.Count + 1)
        count = GridView1.Rows.Count + 1
        Dim dt7 As DataTable = DirectCast(ViewState("INSP"), DataTable)
        dt7.Rows.Add(count, "%", properties_DropDownList.SelectedValue, TextBox1.Text, TextBox2.Text, TextBox3.Text)
        ViewState("INSP") = dt7
        Me.BINDGRID()


        ''calulation
        TextBox1.Text = 0.0
        TextBox2.Text = 0.0
        TextBox3.Text = 0.0

        'Dim i As Integer = 0
        'For i = 0 To GridView1.Rows.Count - 1
        '    Dim total, net, discount, SGST, CGST, IGST, CESS As New Decimal(0)
        '    Dim mat_cost As Decimal = 0
        '    net = CDec(GridView1.Rows(i).Cells(3).Text) * CDec(GridView1.Rows(i).Cells(5).Text)
        '    mat_cost = CDec(GridView1.Rows(i).Cells(3).Text) * CDec(GridView1.Rows(i).Cells(6).Text)
        '    discount = (net * CDec(GridView1.Rows(i).Cells(11).Text)) / 100
        '    SGST = ((net + mat_cost - discount) * CDec(GridView1.Rows(i).Cells(12).Text)) / 100
        '    CGST = ((net + mat_cost - discount) * CDec(GridView1.Rows(i).Cells(13).Text)) / 100
        '    IGST = ((net + mat_cost - discount) * CDec(GridView1.Rows(i).Cells(14).Text)) / 100
        '    CESS = ((net + mat_cost - discount) * CDec(GridView1.Rows(i).Cells(15).Text)) / 100

        '    total = (net - discount) + SGST + CGST + IGST + CESS + mat_cost
        '    total = FormatNumber(total, 2)
        '    GridView1.Rows(i).Cells(17).Text = total

        '    TextBox681.Text = FormatNumber(net + mat_cost + CDec(TextBox681.Text), 3)

        '    TextBox691.Text = FormatNumber((SGST + CDec(TextBox691.Text)), 3)
        '    TextBox701.Text = FormatNumber(CGST + CDec(TextBox701.Text), 3)
        '    TextBox11.Text = FormatNumber((IGST + CDec(TextBox11.Text)), 3)
        '    TextBox12.Text = FormatNumber(CESS + CDec(TextBox12.Text), 3)

        '    TextBox711.Text = FormatNumber(total - FormatNumber(total, 0), 3)
        '    TextBox721.Text = FormatNumber(FormatNumber(total, 0) + CDec(TextBox721.Text), 2)


        'Next
        Label2.Text = ""
        Label2.Visible = False
    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        If DropDownList3.SelectedValue = "Select" Then
            DropDownList3.Focus()
            Return
        End If
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from  PO_RCD_MAT JOIN SUPL ON PO_RCD_MAT.SUPL_ID=SUPL.SUPL_ID JOIN MATERIAL ON PO_RCD_MAT.MAT_CODE=MATERIAL.MAT_CODE where CRR_NO='" & DropDownList3.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            TextBox287.Text = dr.Item("CHLN_NO")
            TextBox282.Text = dr.Item("PO_NO")
            TextBox4.Text = dr.Item("MAT_SLNO")
            TextBox283.Text = dr.Item("SUPL_NAME")
            TextBox278.Text = dr.Item("MAT_NAME")
            TextBox284.Text = dr.Item("MAT_AU")
            TextBox280.Text = dr.Item("MAT_RCD_QTY")
            'TextBox285.Text = dr.Item("MAT_REJ_QTY")

            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cmd As New SqlCommand()
        'Dim strImageName As String = txtName.Text.ToString()
        If FileUpload1.PostedFile IsNot Nothing AndAlso
           FileUpload1.PostedFile.FileName <> "" Then

            Dim imageSize As Byte() = New Byte(FileUpload1.PostedFile.ContentLength - 1) {}

            Dim uploadedImage__1 As HttpPostedFile =
                                 FileUpload1.PostedFile

            uploadedImage__1.InputStream.Read(imageSize, 0,
                  CInt(FileUpload1.PostedFile.ContentLength))

            ' Create SQL Connection 
            'Dim con As New SqlConnection()
            'con.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString

            ' Create SQL Command 


            cmd.CommandText = "INSERT INTO LAB_TEST_REPORT_IMAGES (CRR_NO,IMAGE) VALUES (@CRR_NO,@IMAGE)"
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn

            Dim CRR_NO As New SqlParameter("@CRR_NO", SqlDbType.VarChar, 50)
            'ImageName.Value = strImageName.ToString()
            CRR_NO.Value = DropDownList3.SelectedValue
            cmd.Parameters.Add(CRR_NO)

            Dim UploadedImage__2 As New SqlParameter("@IMAGE", SqlDbType.Image, imageSize.Length)
            UploadedImage__2.Value = imageSize
            cmd.Parameters.Add(UploadedImage__2)
            conn.Open()
            Dim result As Integer = cmd.ExecuteNonQuery()
            conn.Close()
            'FileUpload1.SaveAs(Server.MapPath(FileUpload1.FileName))

            If result > 0 Then
                lblMessage.Text = "File Uploaded"
            End If
            GridView1.DataBind()
        End If


        ''''''''''''''''''''''''''''''''''''''''''''''
        ''Display uploaded picture
        Response.Redirect("~/raw_material/rm_inspection1.aspx")
    End Sub

    Protected Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Using sda As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM LAB_TEST_REPORT_IMAGES where CRR_NO='" & DropDownList3.SelectedValue & "'", conn)
            Dim dt As DataTable = New DataTable()
            sda.Fill(dt)
            gvImages.DataSource = dt
            gvImages.DataBind()
        End Using
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dr As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim imageUrl As String = "data:image/jpg;base64," & Convert.ToBase64String(CType(dr("Image"), Byte()))
            CType(e.Row.FindControl("Image1"), Image).ImageUrl = imageUrl

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class