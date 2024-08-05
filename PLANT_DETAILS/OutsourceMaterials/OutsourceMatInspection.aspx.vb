Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class OutsourceMatInspection
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
    Protected Sub BINDGRID()
        GridView1.DataSource = DirectCast(ViewState("mat1"), DataTable)
        GridView1.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim dt1 As New DataTable()
            dt1.Columns.AddRange(New DataColumn(10) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note")})
            ViewState("mat1") = dt1
            Me.BINDGRID()
            GridView1.Font.Size = 8
            ''load
            conn.Open()
            Dim ds5 As New DataSet
            da = New SqlDataAdapter("select distinct PO_RCD_MAT.CRR_NO from PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO where PO_RCD_MAT.INSP_EMP is null AND (ORDER_DETAILS.PO_TYPE='OUTSOURCED ITEMS' OR ORDER_DETAILS.PO_TYPE='OUTSOURCED ITEMS(IMP)') ORDER BY PO_RCD_MAT.CRR_NO", conn)
            da.Fill(ds5, "PO_RCD_MAT")
            CRR_DropDownList.DataSource = ds5.Tables("PO_RCD_MAT")
            CRR_DropDownList.DataValueField = "CRR_NO"
            CRR_DropDownList.DataBind()
            conn.Close()
            CRR_DropDownList.Items.Insert(0, "Select")
            CRR_DropDownList.SelectedValue = "Select"
            CRR_DropDownList.Enabled = True
            Panel3.Visible = True

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("outSourceAccess")) Or Session("outSourceAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        If CRR_DropDownList.SelectedValue = "Select" Then
            CRR_DropDownList.Focus()
            Return
        ElseIf MATCODE_DropDownList.SelectedValue = "Select" Then
            MATCODE_DropDownList.Focus()
            Return
        ElseIf IsNumeric(GARN_REJQTYTextBox.Text) = False Or GARN_REJQTYTextBox.Text = "" Then
            GARN_REJQTYTextBox.Focus()
            Return
        ElseIf CDec(GARN_REJQTYTextBox.Text) > CDec(RCVDQTY_TextBox.Text) Or CDec(GARN_REJQTYTextBox.Text) < 0 Then
            GARN_REJQTYTextBox.Focus()
            Return
        End If
        For i = 0 To GridView1.Rows.Count - 1
            If MATCODE_DropDownList.SelectedValue = GridView1.Rows(i).Cells(1).Text Then
                MATCODE_DropDownList.Focus()
                Return
            End If
        Next
        Dim MC As New SqlCommand
        Dim tolerance As New Decimal(0.00)
        conn.Open()
        MC.CommandText = "select TOLERANCE from ORDER_DETAILS where SO_NO = '" & po_no_TextBox.Text & "'"
        MC.Connection = conn
        dr = MC.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If IsDBNull(dr.Item("TOLERANCE")) Then
                tolerance = 0
            Else
                tolerance = dr.Item("TOLERANCE")
            End If

            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()

        Dim qty, rqty, total_value, unit_value As Decimal
        Dim unit_price As Decimal = 0.0
        Dim mat_id, transporter As New String("")
        str = ""
        conn.Open()
        Dim mc1 As New SqlCommand
        Dim ITEM_NAME As String = ""
        mc1.CommandText = "select * from PO_ORD_MAT join Outsource_F_ITEM on PO_ORD_MAT .mat_code=Outsource_F_ITEM.ITEM_CODE where PO_ORD_MAT .mat_slno=" & MATCODE_DropDownList.SelectedValue & " and PO_ORD_MAT .po_no='" & po_no_TextBox.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            mat_id = dr.Item("MAT_CODE")
            ITEM_NAME = dr.Item("ITEM_NAME")
            dr.Close()
        End If
        conn.Close()
        conn.Open()
        mc1.CommandText = "select SUM(MAT_QTY) AS MAT_QTY , SUM(MAT_QTY_RCVD) AS MAT_QTY_RCVD FROM PO_ORD_MAT WHERE mat_slno=" & MATCODE_DropDownList.SelectedValue & " and po_no='" & po_no_TextBox.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            qty = dr.Item("MAT_QTY")
            'rqty = dr.Item("MAT_QTY_RCVD")
            dr.Close()
        End If
        conn.Close()


        Dim CRR_RCVD_QTY As Decimal = 0
        'conn.Open()
        'mc1.CommandText = "select (CASE WHEN (MAT_CHALAN_QTY-MAT_RCD_QTY)<(MAT_CHALAN_QTY*0.005) THEN MAT_CHALAN_QTY ELSE MAT_RCD_QTY END) as MAT_RCD_QTY,PO_RCD_MAT.TRANS_WO_NO,PO_RCD_MAT.MAT_EXCE ,Outsource_F_ITEM .ITEM_AU,Outsource_F_ITEM.ITEM_NAME from PO_RCD_MAT join Outsource_F_ITEM on PO_RCD_MAT .MAT_CODE =Outsource_F_ITEM .ITEM_CODE where PO_RCD_MAT .MAT_SLNO =" & MATCODE_DropDownList.SelectedValue & " and PO_RCD_MAT .CRR_NO='" & CRR_DropDownList.SelectedValue & "'"
        'mc1.Connection = conn
        'dr = mc1.ExecuteReader
        'If dr.HasRows Then
        '    dr.Read()
        '    CRR_RCVD_QTY = (dr.Item("MAT_RCD_QTY"))
        '    transporter = (dr.Item("TRANS_WO_NO"))
        '    dr.Close()
        'Else
        '    conn.Close()
        'End If
        'conn.Close()



        ''Calculating Actual received Qty
        conn.Open()
        If (AU_GRANTextBox.Text = "PCS") Then
            mc1.CommandText = "select MAT_RCD_QTY,PO_RCD_MAT.TRANS_WO_NO,PO_RCD_MAT.MAT_EXCE ,Outsource_F_ITEM .ITEM_AU,Outsource_F_ITEM.ITEM_NAME from PO_RCD_MAT join Outsource_F_ITEM on PO_RCD_MAT.MAT_CODE = Outsource_F_ITEM.ITEM_CODE where PO_RCD_MAT.MAT_SLNO =" & MATCODE_DropDownList.SelectedValue & " and PO_RCD_MAT .CRR_NO='" & CRR_DropDownList.SelectedValue & "'"
        Else
            mc1.CommandText = "select (CASE WHEN (MAT_CHALAN_QTY-MAT_RCD_QTY)<(MAT_CHALAN_QTY*0.005) THEN MAT_CHALAN_QTY ELSE MAT_RCD_QTY END) as MAT_RCD_QTY,PO_RCD_MAT.TRANS_WO_NO,PO_RCD_MAT.MAT_EXCE ,Outsource_F_ITEM .ITEM_AU,Outsource_F_ITEM.ITEM_NAME from PO_RCD_MAT join Outsource_F_ITEM on PO_RCD_MAT .MAT_CODE =Outsource_F_ITEM .ITEM_CODE where PO_RCD_MAT .MAT_SLNO =" & MATCODE_DropDownList.SelectedValue & " and PO_RCD_MAT .CRR_NO='" & CRR_DropDownList.SelectedValue & "'"
        End If

        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If (IsDBNull(dr.Item("MAT_RCD_QTY"))) Then
                CRR_RCVD_QTY = 0.00
            Else
                CRR_RCVD_QTY = (dr.Item("MAT_RCD_QTY"))
            End If

            transporter = (dr.Item("TRANS_WO_NO"))
            dr.Close()
        End If
        conn.Close()


        Dim queryString As New String("")
        conn.Open()
        If (transporter = "N/A") Then
            If (tolerance <> 0) Then
                ''TOLERANCE
                If (AU_GRANTextBox.Text = "PCS") Then
                    queryString = "SELECT sum(MAT_RCD_QTY)-sum(MAT_REJ_QTY)-sum(MAT_EXCE) as MAT_QTY_RCVD FROM PO_RCD_MAT where PO_NO='" & po_no_TextBox.Text & "' and MAT_SLNO=" & MATCODE_DropDownList.SelectedValue & " and INSP_EMP is not null"
                Else
                    queryString = "SELECT sum((CASE WHEN (MAT_CHALAN_QTY-MAT_RCD_QTY)<(MAT_CHALAN_QTY*" & tolerance & ") THEN MAT_CHALAN_QTY ELSE MAT_RCD_QTY END))-sum(MAT_REJ_QTY)-sum(MAT_EXCE) as MAT_QTY_RCVD FROM PO_RCD_MAT where PO_NO='" & po_no_TextBox.Text & "' and MAT_SLNO=" & MATCODE_DropDownList.SelectedValue & " and INSP_EMP is not null"
                End If


            Else
                queryString = "SELECT sum(MAT_RCD_QTY)-sum(MAT_REJ_QTY)-sum(MAT_EXCE) as MAT_QTY_RCVD FROM PO_RCD_MAT where PO_NO='" & po_no_TextBox.Text & "' and MAT_SLNO=" & MATCODE_DropDownList.SelectedValue & " and INSP_EMP is not null"
            End If

        Else
            queryString = "select (sum(MAT_CHALAN_QTY)-sum(MAT_REJ_QTY)-sum(MAT_EXCE)) AS MAT_QTY_RCVD from PO_RCD_MAT where PO_NO='" & po_no_TextBox.Text & "' and MAT_SLNO=" & MATCODE_DropDownList.SelectedValue & " and INSP_EMP is not null"
        End If

        mc1.CommandText = queryString
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()

            If IsDBNull(dr.Item("MAT_QTY_RCVD")) Then
                rqty = 0
            Else
                rqty = dr.Item("MAT_QTY_RCVD")
            End If

            dr.Close()
        End If
        conn.Close()

        If (CDec(RCVDQTY_TextBox.Text) - CDec(GARN_REJQTYTextBox.Text)) = 0 Then
            unit_value = 0.0
        Else
            unit_value = FormatNumber(total_value / (CDec(RCVDQTY_TextBox.Text) - CDec(GARN_REJQTYTextBox.Text)), 3)
        End If
        Dim exces_qty, bal_qty As Decimal
        bal_qty = qty - (rqty + CRR_RCVD_QTY - CDec(GARN_REJQTYTextBox.Text))
        If bal_qty >= 0 Then
            exces_qty = 0.0
        Else
            exces_qty = 0.0
        End If
        Dim dt As DataTable = DirectCast(ViewState("mat1"), DataTable)
        dt.Rows.Add(po_no_TextBox.Text, MATCODE_DropDownList.SelectedValue, mat_id, ITEM_NAME, AU_GRANTextBox.Text, RCVDQTY_TextBox.Text, GARN_REJQTYTextBox.Text, (CDec(RCVDQTY_TextBox.Text) - CDec(GARN_REJQTYTextBox.Text)) - exces_qty, exces_qty, bal_qty, NOTE_TextBox.Text)
        ViewState("mat1") = dt
        Me.BINDGRID()
    End Sub

    Protected Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim I As Integer
                For I = 0 To GridView1.Rows.Count - 1
                    Dim insp_note As String = ""
                    If GridView1.Rows(I).Cells(10).Text <> "&nbsp;" Then
                        insp_note = GridView1.Rows(I).Cells(10).Text
                    Else
                        insp_note = ""
                    End If

                    mycommand = New SqlCommand("update PO_RCD_MAT set MAT_REJ_QTY=" & GridView1.Rows(I).Cells(6).Text & " , INSP_EMP='" & Session("userName") & "' , INSP_DATE='" & Today.Year & "-" & Today.Month & "-" & Today.Day & "',MAT_BAL_QTY=" & CDec(GridView1.Rows(I).Cells(9).Text) & ",INSP_NOTE='" & insp_note & "' where MAT_SLNO='" & GridView1.Rows(I).Cells(1).Text & "' and CRR_NO='" & CRR_DropDownList.SelectedValue & "' and po_no='" & po_no_TextBox.Text & "'", conn_trans, myTrans)
                    result = mycommand.ExecuteNonQuery()

                    'UPDATE PO_ORD_MAT
                    If GridView1.Rows(I).Cells(6).Text > 0 Then

                        Dim RCVD_QTY_FOR As Decimal = 0
                        RCVD_QTY_FOR = CDec(GridView1.Rows(I).Cells(6).Text)
                        mycommand = New SqlCommand("update PO_ORD_MAT set MAT_QTY_RCVD=MAT_QTY_RCVD - " & RCVD_QTY_FOR & ",MAT_STATUS='PENDING'  WHERE MAT_SLNO='" & GridView1.Rows(I).Cells(1).Text & "' and PO_NO='" & GridView1.Rows(I).Cells(0).Text & "' AND AMD_NO=(" & "SELECT AMD_NO  FROM PO_RCD_MAT WHERE PO_NO ='" & GridView1.Rows(I).Cells(0).Text & "' AND MAT_SLNO ='" & GridView1.Rows(I).Cells(1).Text & "' AND CRR_NO ='" & CRR_DropDownList.Text & "')", conn_trans, myTrans)
                        mycommand.ExecuteNonQuery()

                    End If

                Next
                Dim dt1 As New DataTable()
                dt1.Columns.AddRange(New DataColumn(10) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note")})
                ViewState("mat1") = dt1
                Me.BINDGRID()

                myTrans.Commit()
                Label2.Text = "All records are written to database."

                conn.Open()
                Dim ds5 As New DataSet
                da = New SqlDataAdapter("select distinct PO_RCD_MAT.CRR_NO from  PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO  where PO_RCD_MAT.INSP_EMP is null AND (ORDER_DETAILS.PO_TYPE='OUTSOURCED ITEMS') ORDER BY PO_RCD_MAT.CRR_NO", conn)
                da.Fill(ds5, "PO_RCD_MAT")
                CRR_DropDownList.DataSource = ds5.Tables("PO_RCD_MAT")
                CRR_DropDownList.DataValueField = "CRR_NO"
                CRR_DropDownList.DataBind()
                conn.Close()
                CRR_DropDownList.Items.Insert(0, "Select")
                CRR_DropDownList.SelectedValue = "Select"
                CRR_DropDownList.Enabled = True
                Panel3.Visible = True
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label2.Text = "There was some Error, please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using

    End Sub

    Protected Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Dim dt1 As New DataTable()
        dt1.Columns.AddRange(New DataColumn(10) {New DataColumn("PO No"), New DataColumn("Mat Sl No"), New DataColumn("Mat Code"), New DataColumn("Mat Name"), New DataColumn("A/U"), New DataColumn("Rcvd Qty"), New DataColumn("Rej Qty"), New DataColumn("Acept Qty"), New DataColumn("Exces Qty"), New DataColumn("Bal Qty"), New DataColumn("Note")})
        ViewState("mat1") = dt1
        Me.BINDGRID()
        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select distinct PO_RCD_MAT.CRR_NO from  PO_RCD_MAT JOIN ORDER_DETAILS ON PO_RCD_MAT.PO_NO=ORDER_DETAILS.SO_NO  where PO_RCD_MAT.INSP_EMP is null AND (ORDER_DETAILS.PO_TYPE='RAW MATERIAL' or ORDER_DETAILS .PO_TYPE ='RAW MATERIAL(IMP)') ORDER BY PO_RCD_MAT.CRR_NO", conn)
        da.Fill(ds5, "PO_RCD_MAT")
        CRR_DropDownList.DataSource = ds5.Tables("PO_RCD_MAT")
        CRR_DropDownList.DataValueField = "CRR_NO"
        CRR_DropDownList.DataBind()
        conn.Close()
        CRR_DropDownList.Items.Insert(0, "Select")
        CRR_DropDownList.SelectedValue = "Select"
        CRR_DropDownList.Enabled = True


    End Sub




    Protected Sub CRR_DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CRR_DropDownList.SelectedIndexChanged
        If CRR_DropDownList.SelectedValue = "Select" Then
            CRR_DropDownList.Focus()
            Return
        End If
        conn.Open()
        Dim ds5 As New DataSet
        da = New SqlDataAdapter("select distinct MAT_SLNO from  PO_RCD_MAT where CRR_NO='" & CRR_DropDownList.Text & "' and GARN_NO='PENDING'", conn)
        da.Fill(ds5, "PO_RCD_MAT")
        MATCODE_DropDownList.DataSource = ds5.Tables("PO_RCD_MAT")
        MATCODE_DropDownList.DataValueField = "MAT_SLNO"
        MATCODE_DropDownList.DataBind()
        MATCODE_DropDownList.Items.Insert(0, "Select")
        MATCODE_DropDownList.SelectedValue = "Select"
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select * from  PO_RCD_MAT where CRR_NO='" & CRR_DropDownList.Text & "'"
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            po_no_TextBox.Text = dr.Item("PO_NO")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
    End Sub

    Protected Sub MATCODE_DropDownList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MATCODE_DropDownList.SelectedIndexChanged
        If MATCODE_DropDownList.SelectedValue = "Select" Then
            MATCODE_DropDownList.Focus()
            Return
        End If
        conn.Open()
        Dim mc1 As New SqlCommand
        mc1.CommandText = "select PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_EXCE , Outsource_F_ITEM.ITEM_AU, Outsource_F_ITEM.ITEM_NAME from PO_RCD_MAT join Outsource_F_ITEM on PO_RCD_MAT .MAT_CODE =Outsource_F_ITEM.ITEM_CODE where PO_RCD_MAT .MAT_SLNO =" & MATCODE_DropDownList.SelectedValue & " and PO_RCD_MAT .CRR_NO='" & CRR_DropDownList.SelectedValue & "' "
        mc1.Connection = conn
        dr = mc1.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            RCVDQTY_TextBox.Text = (dr.Item("MAT_RCD_QTY") - dr.Item("MAT_EXCE"))
            AU_GRANTextBox.Text = dr.Item("ITEM_AU")
            TextBox184.Text = dr.Item("ITEM_NAME")
            dr.Close()
        Else
            conn.Close()
        End If
        conn.Close()
    End Sub

End Class