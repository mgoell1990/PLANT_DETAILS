Imports System.Globalization
Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataSet
Imports System.Data.SqlTypes
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Public Class rm_rej_mat
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If

        If ((IsDBNull(Session("adminAccess")) Or Session("adminAccess") = "") And (IsDBNull(Session("rawMaterialAccess")) Or Session("rawMaterialAccess") = "")) Then

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), Guid.NewGuid().ToString(), "alert('Sorry permission denied!!');window.location.href='../Default.aspx';", True)

        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        Dim dt As New DataTable
        da = New SqlDataAdapter("SELECT (convert(varchar(30),PO_RCD_MAT .CRR_NO) +' , ' + convert(varchar(30),PO_RCD_MAT.MAT_SLNO )) AS CRR_NO1,PO_RCD_MAT.CRR_NO,PO_RCD_MAT.PO_NO,PO_RCD_MAT.MAT_CODE,MATERIAL.MAT_NAME,PO_RCD_MAT.MAT_SLNO,PO_RCD_MAT.MAT_CHALAN_QTY,PO_RCD_MAT.MAT_RCD_QTY,PO_RCD_MAT.MAT_REJ_QTY ,(PO_RCD_MAT.MAT_RCD_QTY-PO_RCD_MAT.MAT_REJ_QTY) AS ACCPT_QTY FROM PO_RCD_MAT JOIN MATERIAL ON PO_RCD_MAT .MAT_CODE =MATERIAL .MAT_CODE WHERE PO_RCD_MAT .CRR_NO ='" & TextBox1.Text.Substring(0, TextBox1.Text.IndexOf(",") - 1) & "' AND RET_STATUS IS NULL AND MAT_REJ_QTY >0 ORDER BY PO_RCD_MAT.MAT_SLNO", conn)
        da.Fill(dt)
        conn.Close()
        GridView1.DataSource = dt
        GridView1.DataBind()
        Label3.Text = ""
    End Sub
    Protected Sub prv(ByVal sender As Object, ByVal e As EventArgs)
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try
                'Database updation entry
                Dim gvRow As GridViewRow = CType(CType(sender, Control).Parent.Parent, GridViewRow)
                Dim index As Integer = gvRow.RowIndex
                Dim btn As LinkButton = CType(sender, LinkButton)
                Dim inv As String = btn.CommandName
                Dim cond As String = btn.Text
                Dim crr_no, mat_slno As String
                crr_no = ""
                mat_slno = ""
                conn.Open()
                Dim mc1 As New SqlCommand
                mc1.CommandText = "select (convert(varchar(30),PO_RCD_MAT .CRR_NO) +' , ' + convert(varchar(30),PO_RCD_MAT.MAT_SLNO )) AS CRR_NO from PO_RCD_MAT where CRR_NO ='" & inv.Substring(0, inv.IndexOf(",") - 1) & "' and MAT_SLNO =" & inv.Substring(inv.IndexOf(",") + 1)
                mc1.Connection = conn
                dr = mc1.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    crr_no = dr.Item("CRR_NO")
                    dr.Close()
                Else
                    dr.Close()
                End If
                conn.Close()
                If crr_no <> "" Then
                    mat_slno = crr_no.Substring(crr_no.IndexOf(",") + 1)
                    crr_no = crr_no.Substring(0, crr_no.IndexOf(",") - 1)
                    'RETURN REF NO GENERATE
                    Dim REFF_NO As String = ""
                    conn.Open()
                    mc1.CommandText = "SELECT (case when MAX(RET_STATUS) IS null then 0 else MAX(RET_STATUS) end) AS CRR_REFF FROM PO_RCD_MAT"
                    mc1.Connection = conn
                    dr = mc1.ExecuteReader
                    If dr.HasRows Then
                        dr.Read()
                        REFF_NO = dr.Item("CRR_REFF")
                        dr.Close()
                    Else
                        dr.Close()
                    End If
                    conn.Close()
                    If REFF_NO = "0" Then
                        REFF_NO = "000001"
                    Else
                        str = REFF_NO + 1
                        If str.Length = 1 Then
                            str = "00000" & (REFF_NO + 1)
                        ElseIf str.Length = 2 Then
                            str = "0000" & (REFF_NO + 1)
                        ElseIf str.Length = 3 Then
                            str = "000" & (REFF_NO + 1)
                        ElseIf str.Length = 4 Then
                            str = "00" & (REFF_NO + 1)
                        ElseIf str.Length = 5 Then
                            str = "0" & (REFF_NO + 1)
                        End If
                        REFF_NO = str
                    End If

                    ''update po_rcd_mat
                    Dim query As String = "update PO_RCD_MAT set RET_STATUS=@RET_STATUS , RET_USER=@RET_USER where MAT_SLNO='" & inv.Substring(inv.IndexOf(",") + 1) & "' and CRR_NO='" & inv.Substring(0, inv.IndexOf(",") - 1) & "'"
                    Dim cmd As New SqlCommand(query, conn_trans, myTrans)
                    cmd.Parameters.AddWithValue("@RET_STATUS", REFF_NO)
                    cmd.Parameters.AddWithValue("@RET_USER", Session("userName"))
                    cmd.ExecuteReader()
                    cmd.Dispose()

                Else
                    Return
                End If

                myTrans.Commit()
                Label3.Text = "Material rejection note generated successfully."
                TextBox1.Text = ""
                dt.Clear()
                GridView1.DataSource = dt
                GridView1.DataBind()
            Catch ee As Exception
                ' Roll back the transaction. 
                myTrans.Rollback()
                conn.Close()
                conn_trans.Close()
                Label3.Text = "There was some error. Please contact EDP."
            Finally
                conn.Close()
                conn_trans.Close()
            End Try

        End Using


    End Sub


End Class