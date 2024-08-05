<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="way_bill.aspx.vb" Inherits="PLANT_DETAILS.way_bill" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True"  ForeColor="#800040" style="text-align: center" Width="100%" Font-Size="XX-Large"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; line-height: 26px;"  >
           <asp:Panel ID="Panel3" runat="server" BorderColor="Lime" BorderStyle="Double" Visible="False" Width="400px" style="text-align: left">
              <asp:Label ID="Label317" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="White" style="text-align: center" Text="UPDATE WAYBILL NO" Width="100%"></asp:Label>
              <br />
              <asp:Label ID="Label310" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Invoice No" Width="120px"></asp:Label>
              <asp:TextBox ID="TextBox70" runat="server" Width="150px"></asp:TextBox>
              <br />
              <asp:Label ID="Label313" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Financial Year" Width="120px"></asp:Label>
              <asp:TextBox ID="TextBox73" runat="server" Width="150px"></asp:TextBox>
              <br />
              <asp:Label ID="Label292" runat="server" Text="Way Bill No." Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
              <asp:TextBox ID="TextBox58" runat="server" Width="150px"></asp:TextBox>
              <br />
              <asp:Label ID="Label318" runat="server" Font-Names="Times New Roman" ForeColor="Red"></asp:Label>
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="Button40" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CANCEL" Width="65px" />
              <asp:Button ID="Button41" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="SAVE" Width="65px" />
              <br />
          </asp:Panel>
             </div>
        </center>
</asp:Content>
