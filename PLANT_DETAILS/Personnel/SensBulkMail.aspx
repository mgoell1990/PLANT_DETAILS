<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SensBulkMail.aspx.vb" Inherits="PLANT_DETAILS.SensBulkMail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Emp. Type&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList16" runat="server" AutoPostBack="True" Width="125px">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>Executives</asp:ListItem>
            <asp:ListItem>Non-Executives</asp:ListItem>
            
        </asp:DropDownList>
        <br />
    </div>
        <asp:TextBox ID="TextBox2" runat="server" Height="94px" TextMode="MultiLine" Width="559px"></asp:TextBox>
    
        <br />
        <br />
        Personal No&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
    
        &nbsp;&nbsp;&nbsp;&nbsp;
        
        <br />
        <br />
    
        <asp:Button ID="Button6" runat="server" Text="Send Individual Mail" />
    
    &nbsp;&nbsp;&nbsp;
    
        <asp:Button ID="Button5" runat="server" Text="Send Bulk Mail" />
    
    </form>
</body>
</html>
