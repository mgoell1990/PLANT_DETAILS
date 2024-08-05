Imports System.Data.SqlClient
Imports System.Web

Public Class Login_new
    Inherits System.Web.UI.Page
    Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim dr As SqlDataReader
    Dim mycommand As New SqlCommand
    Dim ds As New DataSet()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub loginBtn_Click(sender As Object, e As EventArgs) Handles loginBtnNew.Click
        Dim masterAccess, purchaseAccess, storeAccess, rawMaterialAccess, contractAccess, despatchAccess,
            financeAccess, adminAccess, outSourceAccess, isRequisitioner, isAuthorizer, isIssuer, userName As New String("")
        conn.Open()
        Dim mc2 As New SqlCommand
        mc2.CommandText = "select * FROM EmpLoginDetails WITH(NOLOCK) WHERE EMP_ID='" & txtUserName.Text & "' AND EMP_PASSWORD='" & txtPassword.Text & "'"
        mc2.Connection = conn
        dr = mc2.ExecuteReader
        If dr.HasRows Then
            dr.Read()
            If IsDBNull(dr.Item("MASTER")) Then
                masterAccess = ""
            Else
                masterAccess = dr.Item("MASTER")
            End If

            If IsDBNull(dr.Item("PURCHASE")) Then
                purchaseAccess = ""
            Else
                purchaseAccess = dr.Item("PURCHASE")
            End If

            If IsDBNull(dr.Item("STORE")) Then
                storeAccess = ""
            Else
                storeAccess = dr.Item("STORE")
            End If

            If IsDBNull(dr.Item("RAW_MATERIAL")) Then
                rawMaterialAccess = ""
            Else
                rawMaterialAccess = dr.Item("RAW_MATERIAL")
            End If

            If IsDBNull(dr.Item("CONTRACT")) Then
                contractAccess = ""
            Else
                contractAccess = dr.Item("CONTRACT")
            End If


            If IsDBNull(dr.Item("DESPATCH")) Then
                despatchAccess = ""
            Else
                despatchAccess = dr.Item("DESPATCH")
            End If


            If IsDBNull(dr.Item("FINANCE")) Then
                financeAccess = ""
            Else
                financeAccess = dr.Item("FINANCE")
            End If


            If IsDBNull(dr.Item("ADMIN")) Then
                adminAccess = ""
            Else
                adminAccess = dr.Item("ADMIN")
            End If

            If IsDBNull(dr.Item("OUTSOURCED_ITEMS")) Then
                outSourceAccess = ""
            Else
                outSourceAccess = dr.Item("OUTSOURCED_ITEMS")
            End If

            If IsDBNull(dr.Item("REQUISITIONER")) Then
                isRequisitioner = ""
            Else
                isRequisitioner = dr.Item("REQUISITIONER")
            End If

            If IsDBNull(dr.Item("AUTHORIZER")) Then
                isAuthorizer = ""
            Else
                isAuthorizer = dr.Item("AUTHORIZER")
            End If

            If IsDBNull(dr.Item("ISSUER")) Then
                isIssuer = ""
            Else
                isIssuer = dr.Item("ISSUER")
            End If

            If IsDBNull(dr.Item("EMP_NAME")) Then
                Session("userName") = txtUserName.Text
            Else
                Session("userName") = dr.Item("EMP_NAME")
            End If


            If (masterAccess = "YES") Then
                Session("masterAccess") = masterAccess
            Else
                Session("masterAccess") = ""
            End If

            If (purchaseAccess = "YES") Then
                Session("purchaseAccess") = purchaseAccess
            Else
                Session("purchaseAccess") = ""
            End If

            If (storeAccess = "YES") Then
                Session("storeAccess") = storeAccess
            Else
                Session("storeAccess") = ""
            End If

            If (rawMaterialAccess = "YES") Then
                Session("rawMaterialAccess") = rawMaterialAccess
            Else
                Session("rawMaterialAccess") = ""
            End If

            If (contractAccess = "YES") Then
                Session("contractAccess") = contractAccess
            Else
                Session("contractAccess") = ""
            End If

            If (despatchAccess = "YES") Then
                Session("despatchAccess") = despatchAccess
            Else
                Session("despatchAccess") = ""
            End If

            If (financeAccess = "YES") Then
                Session("financeAccess") = financeAccess
            Else
                Session("financeAccess") = ""
            End If

            If (adminAccess = "YES") Then
                Session("adminAccess") = adminAccess
            Else
                Session("adminAccess") = ""
            End If

            If (outSourceAccess = "YES") Then
                Session("outSourceAccess") = outSourceAccess
            Else
                Session("outSourceAccess") = ""
            End If

            If (isRequisitioner = "YES") Then
                Session("isRequisitioner") = isRequisitioner
            Else
                Session("isRequisitioner") = ""
            End If

            If (isAuthorizer = "YES") Then
                Session("isAuthorizer") = isAuthorizer
            Else
                Session("isAuthorizer") = ""
            End If

            If (isIssuer = "YES") Then
                Session("isIssuer") = isIssuer
            Else
                Session("isIssuer") = ""
            End If


            dr.Close()
            Response.Redirect("~/Default.aspx")


        Else
            txtLoginError.Text = "Login Error, Please try again!!"

        End If
        conn.Close()

    End Sub


End Class