<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="inv_entry.aspx.vb" Inherits="PLANT_DETAILS.inv_entry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
      <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">

            <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" ScrollBars="Auto" Width="760px" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" CssClass="brds">
                <asp:Label ID="Label483" runat="server" BackColor="#4686F0" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Height="40px" style="text-align: center; line-height:40px" Text="INVOICE ENTRY" Width="100%"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label71" runat="server" Text="Bill Tracking Id" Width="90px"></asp:Label>
                <asp:TextBox ID="TextBox28" runat="server" Width="125px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label98" runat="server" Text="Inv for" Width="90px"></asp:Label>
                <asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Supplier</asp:ListItem>
                    <asp:ListItem>Contractor</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Order No" Width="90px"></asp:Label>
                <asp:DropDownList ID="DropDownList10" runat="server" Width="125px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label89" runat="server" Text="Party"></asp:Label>
                <asp:TextBox ID="TextBox42" runat="server" Width="381px" BackColor="#4686F0" ForeColor="White"></asp:TextBox>
                <br />
                <asp:Label ID="Label68" runat="server" Text="Inv No" Width="90px"></asp:Label>
                <asp:TextBox ID="TextBox26" runat="server" Width="125px"></asp:TextBox>
                <asp:Label ID="Label69" runat="server" Text="Inv Date"></asp:Label>
                <asp:TextBox ID="TextBox27" runat="server" Width="125px"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox27_CalendarExtender" runat="server" CssClass="red" Format ="dd-MM-yyyy" Enabled="True" TargetControlID="TextBox27">
                </asp:CalendarExtender>
                <asp:Label ID="Label72" runat="server" Text="Amt"></asp:Label>
                <asp:TextBox ID="TextBox30" runat="server" Width="125px"></asp:TextBox>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button12" runat="server" Text="SAVE" ForeColor="Blue" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                <asp:Button ID="Button47" runat="server" ForeColor="Blue" Text="CANCEL" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                <br />
                <asp:Label ID="Label552" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>



            </div>
        </center>
</asp:Content>
