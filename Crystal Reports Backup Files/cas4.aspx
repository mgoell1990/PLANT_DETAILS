<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="cas4.aspx.vb" Inherits="PLANT_DETAILS.cas4" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; line-height: 20px;"  >
<asp:Panel ID="Panel1" runat="server" CssClass="brds" Width="500px" BorderColor="Blue" BorderStyle="Ridge">
    <asp:Label ID="Label2" runat="server" Text="CAS4" style="text-align: center" BackColor="Blue" CssClass="brds" Font-Bold="True" Font-Size="X-Large"  ForeColor="White" Width="100%"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Mat. Group" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" Width="250px" AutoPostBack="True">
    </asp:DropDownList>
    <br />
    <asp:Label ID="Label4" runat="server" Text="CAS4 Value" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Width="150px"></asp:TextBox>
    <br />
    <asp:Label ID="Label5" runat="server" Text="Eff. Date" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server" Width="150px"></asp:TextBox>
    <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox2_CalendarExtender" TargetControlID="TextBox2" />
    <br />
    <asp:Label ID="Label6" runat="server" ForeColor="#660033"></asp:Label>
    <br />
    <div runat ="server" style ="float :right ">
        <asp:Button ID="Button1" runat="server" Text="SAVE" CssClass="bottomstyle" Font-Size="Small" Width="80px"></asp:Button>
        <asp:Button ID="Button2" runat="server" Text="CANCEL" CssClass="bottomstyle" Font-Size="Small" Width="80px"></asp:Button>
</div>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="100%">
        <Columns>
            <asp:BoundField DataField="MAT_GROUP" HeaderText="Mat. Group">
            <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="cost_value" HeaderText="CAS4 Value" />
            <asp:BoundField DataField="EFECTIVE_DATE" HeaderText="Effective Date">
            <ItemStyle Width="150px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <br />
</asp:Panel>




</div>
        </center>
</asp:Content>
