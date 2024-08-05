Public Class SiteMaster
    Inherits MasterPage

    Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Dim _antiXsrfTokenValue As String

    ''Protected Sub Page_Init(sender As Object, e As System.EventArgs)
    ' The code below helps to protect against XSRF attacks
    'Dim requestCookie As HttpCookie = Request.Cookies(AntiXsrfTokenKey)
    'Dim requestCookieGuidValue As Guid
    'If ((Not requestCookie Is Nothing) AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue)) Then
    '    ' Use the Anti-XSRF token from the cookie
    '    _antiXsrfTokenValue = requestCookie.Value
    '    Page.ViewStateUserKey = _antiXsrfTokenValue
    'Else
    '    ' Generate a new Anti-XSRF token and save to the cookie
    '    _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
    '    Page.ViewStateUserKey = _antiXsrfTokenValue

    '    Dim responseCookie As HttpCookie = New HttpCookie(AntiXsrfTokenKey) With {.HttpOnly = True, .Value = _antiXsrfTokenValue}
    '    If (FormsAuthentication.RequireSSL And Request.IsSecureConnection) Then
    '        responseCookie.Secure = True
    '    End If
    '    Response.Cookies.Set(responseCookie)
    'End If

    'AddHandler Page.PreLoad, AddressOf master_Page_PreLoad


    ''End Sub

    Private Sub master_Page_PreLoad(sender As Object, e As System.EventArgs)
        If (Not IsPostBack) Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.Session("userName"), String.Empty)
        Else
            ' Validate the Anti-XSRF token
            If (Not DirectCast(ViewState(AntiXsrfTokenKey), String) = _antiXsrfTokenValue _
        Or Not DirectCast(ViewState(AntiXsrfUserNameKey), String) = If(Context.Session("userName"), String.Empty)) Then
                'Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            'Label1.Text = DateTime.Now.ToString("hh:mm:ss tt")
            'Label2.Text = DateTime.Now.ToString("dd-MM-yyyy")

            If (Session("userName") = "") Then
                logOutNew.Visible = False
                loginLinkNew.Visible = True
                registerLinkNew.Visible = True
                txtUserName.Visible = False
                'Response.Redirect("~/Account/Login_new")
            Else
                logOutNew.Visible = True
                registerLinkNew.Visible = False
                loginLinkNew.Visible = False
                txtUserName.Visible = True
                txtUserName.Text = "Welcome " + Session("userName")
            End If


        End If
        'txtUserName.Text = Session("userName")

    End Sub


    'Protected Sub GetTime(sender As Object, e As EventArgs) Handles Timer1.Tick
    '    Label1.Text = DateTime.Now.ToString("hh:mm:ss tt")
    '    Label2.Text = DateTime.Now.ToString("dd-MM-yyyy")
    'End Sub


    Protected Sub logOutNew_Click1(sender As Object, e As EventArgs) Handles logOutNew.Click
        Session.RemoveAll()
        Response.RedirectPermanent("~/Account/Login_new")
    End Sub
End Class