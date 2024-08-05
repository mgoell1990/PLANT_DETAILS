<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Email.aspx.vb" Inherits="PLANT_DETAILS.Email" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label49" runat="server" Text="Month"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList9" runat="server" AutoPostBack="True" Width="125px">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>JANUARY</asp:ListItem>
            <asp:ListItem>FEBRUARY</asp:ListItem>
            <asp:ListItem>MARCH</asp:ListItem>
            <asp:ListItem>APRIL</asp:ListItem>
            <asp:ListItem>MAY</asp:ListItem>
            <asp:ListItem>JUNE</asp:ListItem>
            <asp:ListItem>JULY</asp:ListItem>
            <asp:ListItem>AUGUST</asp:ListItem>
            <asp:ListItem>SEPTEMBER</asp:ListItem>
            <asp:ListItem>OCTOBER</asp:ListItem>
            <asp:ListItem>NOVEMBER</asp:ListItem>
            <asp:ListItem>DECEMBER</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
    
        <asp:Button ID="Button1" runat="server" Text="Split PDF" />
    
        <br />
        <br />
    
        <asp:Label ID="Label50" runat="server" Text="Month"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList10" runat="server" AutoPostBack="True" Width="125px">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>JANUARY</asp:ListItem>
            <asp:ListItem>FEBRUARY</asp:ListItem>
            <asp:ListItem>MARCH</asp:ListItem>
            <asp:ListItem>APRIL</asp:ListItem>
            <asp:ListItem>MAY</asp:ListItem>
            <asp:ListItem>JUNE</asp:ListItem>
            <asp:ListItem>JULY</asp:ListItem>
            <asp:ListItem>AUGUST</asp:ListItem>
            <asp:ListItem>SEPTEMBER</asp:ListItem>
            <asp:ListItem>OCTOBER</asp:ListItem>
            <asp:ListItem>NOVEMBER</asp:ListItem>
            <asp:ListItem>DECEMBER</asp:ListItem>
        </asp:DropDownList>
        &nbsp;<asp:Label ID="Label51" runat="server" Text="Year"></asp:Label>
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True" Width="125px">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>2018</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
    
        <asp:Button ID="Button2" runat="server" Text="Send PDF" />
    
        <br />
        <br />
        <br />
        <asp:Label ID="Label57" runat="server" Text="Invoice Data"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" Height="77px" TextMode="MultiLine" Width="350px"></asp:TextBox>
    
        <br />
        <asp:Label ID="Label58" runat="server" Text="IRN Number"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" Width="500px"></asp:TextBox>
    
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="API GET Testing" />
        <asp:Button ID="Button4" runat="server" Text="API POST Testing" />
    
        <asp:Button ID="Button5" runat="server" Text="Decoding Testing" />
    
        <asp:Button ID="Button6" runat="server" Text="Print Invoice" />
    
        <asp:Button ID="Button7" runat="server" Text="Create Json" />
    
        <asp:Button ID="Button8" runat="server" Text="Cancel IRN" />
    
        <br />
        <br />
        <asp:Image ID="imgQRCode" runat="server" Height="300px" Width="300px" />
    
        <br />
        <br />
        <asp:Label ID="Label53" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label52" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label56" runat="server" Text="IRN number : "></asp:Label>
        <asp:Label ID="Label54" runat="server"></asp:Label>
        <br />
        <asp:Label ID="Label55" runat="server" Text="Signed QR Code : "></asp:Label>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
