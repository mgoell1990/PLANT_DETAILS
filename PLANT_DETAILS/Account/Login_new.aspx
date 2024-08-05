<%@ Page Title="Log in" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login_new.aspx.vb" Inherits="PLANT_DETAILS.Login_new" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../bootstrap/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%--<hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>


    <h2>Use a local account to log in.</h2>
    <legend>Log in Form</legend>
    <ol>
        <li>
            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="UserName" Width="110px"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
        </li>
        <li>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Password" Width="110px"></asp:Label>
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" Width="120px" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
        </li>
        <li>
            <asp:CheckBox runat="server" ID="RememberMe" />
            <asp:Label ID="Label2" runat="server" Font-Bold="True" CssClass="checkbox" Font-Size="Small" ForeColor="Blue" Text="Remember me?" Width="150px"></asp:Label>
        </li>
        <li>

            <asp:Label runat="server" CssClass="checkbox" ID="txtLoginError" Font-Bold="True" ForeColor="Red"></asp:Label>
        </li>
    </ol>
    <asp:Button ID="loginBtnNew" runat="server" Text="LOG IN" />
    <p>
        <a id="registerLinkNew" runat="server" href="~/Account/RegistrationPageNew" style="font-size: 15px">Register</a>
        if you don't have an account.
    </p>--%>



    <%--=======================================================--%>
    <script src="js/jquery.min.js"></script>
    <script src="js/popper.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/main.js"></script>
    <section class="ftco-section">
        <div class="container">

            <div class="row justify-content-center">
                <div class="col-md-12 col-lg-10">
                    <div class="wrap d-md-flex m-1">
                        <div class="img" style="background-image: url(../Images/SAIL_Logo_svg.png);">
                        </div>
                        <div class="login-wrap p-4 p-md-5">
                            <div class="d-flex">
                                <div class="w-100">
                                    <h3 class="mb-4">Log In</h3>
                                </div>
                            </div>

                            <div class="form-group mb-3">
                                <label class="label" for="name">Username</label>
                                <%--<input type="text" ID="txtUserName" class="form-control" placeholder="Username" required>--%>
                                <asp:TextBox type="text" ID="txtUserName" class="form-control" placeholder="Username" runat="server" required></asp:TextBox>
                            </div>
                            <div class="form-group mb-3">
                                <label class="label" for="password">Password</label>
                                <%--<input type="password" class="form-control" placeholder="Password" required>--%>
                                <asp:TextBox runat="server" type="password" ID="txtPassword" class="form-control" placeholder="Password" required />
                            </div>
                            <div class="form-group">
                                <%--<button type="submit" class="form-control btn btn-primary rounded submit px-3">Sign In</button>--%>
                                <asp:Button ID="loginBtnNew" runat="server" class="form-control btn btn-primary rounded submit px-3" Text="LOG IN" />
                            </div>
                            <div class="form-group d-md-flex">
                                <div class="w-50 text-left">
                                    <label class="checkbox-wrap checkbox-primary mb-0">
                                        Remember Me
									 
                                                <input type="checkbox" checked>
                                        <span class="checkmark"></span>
                                    </label>
                                </div>
                                <div class="w-50 text-md-right">
                                    <a href="#">Forgot Password</a>
                                </div>
                            </div>

                            <p class="text-center">Not a member? <a data-toggle="tab" href="#signup">Sign Up</a></p>
                            <asp:Label runat="server" CssClass="checkbox" ID="txtLoginError" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>





</asp:Content>
