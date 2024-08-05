Imports System.Data.SqlClient

Public Class RegistrationPageNew
    Inherits System.Web.UI.Page
    Dim conn_trans As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
    Dim myTrans As SqlTransaction
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Using conn_trans
            conn_trans.Open()
            myTrans = conn_trans.BeginTransaction()

            Try

                Dim QUARY1 As String
                QUARY1 = "Insert Into EmpLoginDetails(EMP_ID,EMP_PASSWORD,SECRET_QUESTION_1,SECRET_ANSWER_1,SECRET_QUESTION_2,SECRET_ANSWER_2,STATUS)VALUES(@EMP_ID,@EMP_PASSWORD,@SECRET_QUESTION_1,@SECRET_ANSWER_1,@SECRET_QUESTION_2,@SECRET_ANSWER_2,@STATUS)"
                Dim cmd1 As New SqlCommand(QUARY1, conn_trans, myTrans)
                cmd1.Parameters.AddWithValue("@EMP_ID", txtUserName.Text)
                cmd1.Parameters.AddWithValue("@EMP_PASSWORD", txtPassword.Text)
                cmd1.Parameters.AddWithValue("@SECRET_QUESTION_1", ddlSecretQuestion1.Text)
                cmd1.Parameters.AddWithValue("@SECRET_ANSWER_1", txtSecretAnswer1.Text)
                cmd1.Parameters.AddWithValue("@SECRET_QUESTION_2", ddlSecretQuestion2.Text)
                cmd1.Parameters.AddWithValue("@SECRET_ANSWER_2", txtSecretAnswer2.Text)
                cmd1.Parameters.AddWithValue("@STATUS", "ACTIVE")
                cmd1.ExecuteReader()
                cmd1.Dispose()

                myTrans.Commit()
                txtLoginError.Text = "User registered successfully, Please contact EDP for your role based permission."
            Catch ex As Exception
                myTrans.Rollback()
                conn_trans.Close()
                txtLoginError.Text = "There is some error, Please contact EDP."
            Finally

                conn_trans.Close()
            End Try
        End Using
    End Sub
End Class