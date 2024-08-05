<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="E_Invoice_Cancellation.aspx.vb" Inherits="PLANT_DETAILS.E_Invoice_Cancellation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">

            <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" ScrollBars="Auto" Width="760px" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" CssClass="brds">
                <asp:Label ID="Label483" runat="server" BackColor="#4686F0" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Height="40px" style="text-align: center; line-height:40px" Text="INVOICE CANCELLATION" Width="100%"></asp:Label>
                <br />
                <br />
                
                <asp:Label ID="Label98" runat="server" Text="Fiscal Year" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Supplier</asp:ListItem>
                    <asp:ListItem>Contractor</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Invoice No" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList10" runat="server" Width="125px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label8" runat="server" Text="IRN No." Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox4" runat="server" Width="425px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label71" runat="server" Text="Invoice Status" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox28" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Party Code" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox43" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label4" runat="server" Text="Party Name" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="425px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label5" runat="server" Text="Invoice Amount" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="120px"></asp:TextBox>
                 <br />
                <asp:Label ID="Label6" runat="server" Text="Cancellation Reason" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="125px" AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="Label7" runat="server" Text="Cancellation Remarks" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button47" runat="server" ForeColor="Blue" Text="CANCEL INVOICE" Width="150px" CssClass="bottomstyle" Font-Size="Small" />
                <br />
                &nbsp;<asp:Label ID="Label31" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label552" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label42" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label553" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>



            </div>
        </center>
</asp:Content>

