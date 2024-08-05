<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostback ="true"  AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="print_order.aspx.vb" Inherits="PLANT_DETAILS.print_order" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif; height: 8px;">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="Content/Site.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <center>
        <div runat ="server" style ="min-height :600px;">

            <asp:Panel ID="Panel32" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" style="text-align: left" Width="353px" CssClass="brds">
                <asp:Label ID="Label444" runat="server" BackColor="#0000CC" Font-Bold="True" ForeColor="White" style="text-align: center" Text="PRINT ORDER" Width="100%" Font-Size="Medium"></asp:Label>
              <br />
                <asp:Label ID="Label447" runat="server" ForeColor="Blue" Text="Report For" Width="90px"></asp:Label>
                <asp:DropDownList ID="DropDownList51" runat="server" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Full Order</asp:ListItem>
                    <asp:ListItem>Order Balance</asp:ListItem>
                    <asp:ListItem>Amendment</asp:ListItem>
                    <asp:ListItem>Short Close</asp:ListItem>
                    <asp:ListItem>Completed</asp:ListItem>
                </asp:DropDownList>
              <br />
                <asp:Label ID="Label446" runat="server" ForeColor="Blue" Text="Order Type" Width="90px"></asp:Label>
                <asp:DropDownList ID="DropDownList50" runat="server" AutoPostBack="True" Width="125px">
                </asp:DropDownList>
              <br />
                <asp:Label ID="Label445" runat="server" ForeColor="Blue" Text="Order No" Width="90px"></asp:Label>
                <asp:DropDownList ID="DropDownList49" runat="server" Width="125px">
                </asp:DropDownList>
              <br />
              <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button61" runat="server" Font-Bold="True" Text="CANCEL" CssClass="bottomstyle" Width="100px" />
                <asp:Button ID="Button62" runat="server" Font-Bold="True" Text="PREVIEW" CssClass="bottomstyle" Width="100px" />
              <br />
            </asp:Panel>
            <br />
            <asp:Button ID="Button63" runat="server" Text="Button" Visible="False" />
            <br />

            <br />

        </div>
    </center>
</asp:Content>
