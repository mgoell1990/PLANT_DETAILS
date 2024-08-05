<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="d_work.aspx.vb" Inherits="PLANT_DETAILS.d_work" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
     <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">
            <asp:Panel ID="Panel1" runat="server" BorderColor="#4686F0" BorderStyle="Groove" Width="800px" style="text-align: left" Font-Names="Times New Roman" Font-Size="Small" Visible="False">
                 <asp:Label ID="Label16" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :30px;"  Text="DAILY WORK DETAILS"   Width="794px"></asp:Label>
                 <br />
                 <br />
                 <br />
                 <asp:Label ID="Label2" runat="server" Text="Work Order No" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" AutoPostBack="True">
                 </asp:DropDownList>
                 <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Blue" Text="Supl Name" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox12" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="290px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Sl No" Width="100px"></asp:Label>
                 <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="100px">
                 </asp:DropDownList>
                 <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Desc" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox3" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="290px"></asp:TextBox>
                 <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Blue" Text="A / U" Width="60px"></asp:Label>
                 <asp:TextBox ID="TextBox5" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                 <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
                 <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Text="To" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                 <cc1:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox2" />
                 &nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Blue" Text="EXP Date" Width="60px"></asp:Label>
                 <asp:TextBox ID="TextBox10" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
                 <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Blue" Text="Bal Qty" Width="60px"></asp:Label>
                 <asp:TextBox ID="TextBox11" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label8" runat="server" Text="Worked Unit" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:TextBox ID="TextBox4" runat="server" Width="100px"></asp:TextBox>
                 <asp:Label ID="Label9" runat="server" Text="Rqd Unit" Width="70px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:TextBox ID="TextBox6" runat="server" Width="100px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label15" runat="server" Font-Bold="True" ForeColor="Blue" Text="Deptt" Width="100px"></asp:Label>
                 <asp:DropDownList ID="DropDownList3" runat="server" Width="300px">
                 </asp:DropDownList>
                 <br />
                 <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Blue" Text="Note" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox8" runat="server" Width="300px"></asp:TextBox>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="Button49" runat="server" Font-Bold="True" ForeColor="Blue" Text="ADD" Width="80px" CssClass="bottomstyle" />
                 <asp:Button ID="Button50" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" CssClass="bottomstyle" />
                 <asp:Button ID="Button51" runat="server" Font-Bold="True" ForeColor="Blue" Text="CANCEL" Width="80px" CssClass="bottomstyle" />
                 <asp:Panel ID="Panel8" runat="server" BackColor="#CCFFFF" style="text-align: left" Visible="False">
                     <div style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label46" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                         <asp:TextBox ID="TextBox32" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                         <asp:Label ID="Label47" runat="server" ForeColor="Red"></asp:Label>
                         <br />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                         <asp:Button ID="Button60" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" />
                     </div>
                 </asp:Panel>
                 <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" Width="100%">
                 </asp:GridView>
             </asp:Panel>
            </div>
            </center> 
</asp:Content>
