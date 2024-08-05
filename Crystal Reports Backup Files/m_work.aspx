<%@ Page Title="" Language="vb" AutoEventWireup="false"  MaintainScrollPositionOnPostback ="true" MasterPageFile="~/Site.Master" CodeBehind="m_work.aspx.vb" Inherits="PLANT_DETAILS.m_work" %>
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
                <asp:Panel ID="Panel2" runat="server" BorderColor="#4686F0" BorderStyle="Groove" Width="800px" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left" Visible="False">
                 <asp:Label ID="Label17" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :30px;" Text="MESUREMENT OF WORKS" Height="30px" Width="796px"></asp:Label>
                 <br />
                 <br />
                 <asp:Label ID="Label36" runat="server" Font-Bold="True" ForeColor="Blue" Text="MB No" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox28" runat="server" Width="100px" BackColor="Red" ForeColor="White" ReadOnly="True" ></asp:TextBox>
                 <br />
                 <asp:Label ID="Label18" runat="server" Text="Work Order No" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:DropDownList ID="DropDownList4" runat="server" Width="100px" AutoPostBack="True">
                 </asp:DropDownList>
                 <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="Blue" Text="Supl Name" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox13" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="290px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label20" runat="server" Text="Work Sl No" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:DropDownList ID="DropDownList5" runat="server" Width="100px" AutoPostBack="True">
                 </asp:DropDownList>
                 <asp:Label ID="Label21" runat="server" Text="Work Desc" Width="70px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:TextBox ID="TextBox14" runat="server" Width="290px" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                 <asp:Label ID="Label22" runat="server" Text="A / U" Font-Bold="True" ForeColor="Blue" Width="60px"></asp:Label>
                 <asp:TextBox ID="TextBox15" runat="server" Width="100px" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label23" runat="server" Text="Date" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:TextBox ID="TextBox16" runat="server" Width="100px"></asp:TextBox>
      <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"  CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox16">
                 </cc1:CalendarExtender>
              
                 <asp:Label ID="Label24" runat="server" Text="To" Width="70px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                 <asp:TextBox ID="TextBox17" runat="server" Width="100px"></asp:TextBox>
                 <cc1:CalendarExtender ID="TextBox17_CalendarExtender" runat="server" Enabled="True"  CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox17">
                 </cc1:CalendarExtender>
                 &nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="EXP Date" Width="60px"></asp:Label>
                 <asp:TextBox ID="TextBox18" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
                 <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Bal Qty" Width="60px"></asp:Label>
                 <asp:TextBox ID="TextBox19" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label32" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox24" runat="server" Width="100px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Blue" Text="RA Bill" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox25" runat="server" Width="100px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="Blue" Text="Note" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox23" runat="server" Width="280px"></asp:TextBox>
                 &nbsp;&nbsp;<asp:Button ID="Button52" runat="server" Font-Bold="True" ForeColor="Blue" Text="VIEW" Width="80px" />
                 <asp:Button ID="Button55" runat="server" Font-Bold="True" ForeColor="Blue" Text="ADD PENALITY" Width="120px" />
                 <asp:Button ID="Button53" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" />
                 <asp:Button ID="Button54" runat="server" Font-Bold="True" ForeColor="Blue" Text="CANCEL" Width="80px" />
              
                 <asp:Panel ID="Panel7" runat="server" BackColor="#CCFFFF" style="text-align: left" Visible="False">
                     <div style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label44" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                         <asp:TextBox ID="TextBox31" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                         <asp:Label ID="Label45" runat="server" ForeColor="Red"></asp:Label>
                         <br />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                         <asp:Button ID="Button59" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" />
                     </div>
                 </asp:Panel>
                 <asp:GridView ID="GridView2" runat="server" ShowHeaderWhenEmpty="True" Width="100%" AutoGenerateColumns="False">
                     <Columns>
                         <asp:BoundField DataField="wo_slno" HeaderText="Work SlNo" />
                         <asp:BoundField DataField="w_name" HeaderText="Work Desc" />
                         <asp:BoundField DataField="AU" HeaderText="A/U" />
                         <asp:BoundField DataField="from_date" HeaderText="From Date" />
                         <asp:BoundField DataField="to_date" HeaderText="To Date" />
                         <asp:BoundField DataField="work_qty" HeaderText="Worked Unit" />
                         <asp:BoundField DataField="rqd_qty" HeaderText="Rqd Unit" />
                         <asp:TemplateField HeaderText="Penality"></asp:TemplateField>
                         <asp:BoundField DataField="op_qty" HeaderText="Op Qty" />
                         <asp:BoundField DataField="bal_qty" HeaderText="Bal Unit" />
                         <asp:TemplateField HeaderText="Note"></asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </asp:Panel>
            </div>
        </center>
</asp:Content>
