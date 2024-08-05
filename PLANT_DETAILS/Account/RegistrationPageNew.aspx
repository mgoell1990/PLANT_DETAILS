<%@ Page Title="Log in" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrationPageNew.aspx.vb" Inherits="PLANT_DETAILS.RegistrationPageNew" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>


    <h2>User registration page</h2>

    <ol>
        <li>
            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="UserName" Width="175px"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
        </li>
        <li>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Password" Width="175px"></asp:Label>
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" Width="120px" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
        </li>
        <li>
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Secret Question 1" Width="175px"></asp:Label>
            <asp:DropDownList ID="ddlSecretQuestion1" runat="server" Width="120px">
                <asp:ListItem>What was your childhood nickname?</asp:ListItem>
                <asp:ListItem>What is the name of your childhood friend?</asp:ListItem>
                <asp:ListItem>What is your mother maiden name?</asp:ListItem>
                <asp:ListItem>What is your favorite movie?</asp:ListItem>
                <asp:ListItem>What is your favorite sport?</asp:ListItem>
                <asp:ListItem>What is your favorite food?</asp:ListItem>
                <asp:ListItem>What is your birth year?</asp:ListItem>
                
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
        </li>
        <li>
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Secret Answer 1" Width="175px"></asp:Label>
            <asp:TextBox runat="server" ID="txtSecretAnswer1" TextMode="Password" Width="120px" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSecretAnswer1" CssClass="field-validation-error" ErrorMessage="The password field is required." />
        </li>
        <li>
            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Secret Question 2" Width="175px"></asp:Label>
            <asp:DropDownList ID="ddlSecretQuestion2" runat="server" Width="120px">
                <asp:ListItem>What was your first car?</asp:ListItem>
                <asp:ListItem>What is your astrological sign?</asp:ListItem>
                <asp:ListItem>What city were you born in?</asp:ListItem>
                <asp:ListItem>What was your first car?</asp:ListItem>
                <asp:ListItem>What is your favorite vacation destination?</asp:ListItem>
                <asp:ListItem>What was your school name?</asp:ListItem>
                <asp:ListItem>What is your favorite color?</asp:ListItem>
                
            </asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
        </li>
        <li>
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Secret Answer 2" Width="175px"></asp:Label>
            <asp:TextBox runat="server" ID="txtSecretAnswer2" TextMode="Password" Width="120px" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSecretAnswer2" CssClass="field-validation-error" ErrorMessage="The password field is required." />
        </li>

        <li>

            <asp:Label runat="server" CssClass="checkbox" ID="txtLoginError" Font-Bold="True" ForeColor="Red"></asp:Label>
        </li>
    </ol>
    <asp:Button ID="btnRegister" runat="server" Text="Register" />
    

</asp:Content>

