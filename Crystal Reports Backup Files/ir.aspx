<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="ir.aspx.vb" Inherits="PLANT_DETAILS.ir" %>
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
    <asp:Panel ID="Panel3" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Width="1000px">
             <asp:Label ID="Label37" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center;line-height :30px;" Text="I.R. CLEARANCE" Height ="30px" Width="100%"></asp:Label>
             <asp:Panel ID="Panel4" runat="server" BackColor="#CCFFFF" Height="64px" HorizontalAlign="Left">
                 <br />
                    <asp:Label ID="Label38" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Order No" Width="100px"></asp:Label>
             <asp:DropDownList ID="DropDownList6" runat="server" Width="125px" AutoPostBack="True">
             </asp:DropDownList>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label40" runat="server" Font-Bold="True" ForeColor="Blue" Text="M.B. No" Width="100px"></asp:Label>
                 <asp:DropDownList ID="DropDownList8" runat="server" Width="125px" AutoPostBack="True">
                 </asp:DropDownList>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="Button57" runat="server" BackColor="Transparent" Font-Bold="True" ForeColor="Blue" Text="CLEAR" Width="90px" />
                 <asp:Button ID="Button58" runat="server" BackColor="Transparent" Font-Bold="True" ForeColor="Blue" Text="CLOSE" Width="90px" />
             </asp:Panel>
            
                  <asp:Panel ID="Panel6" runat="server" style="text-align: left" Visible="False" BackColor="#CCFFFF">
                  <div style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="Label42" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                 <asp:TextBox ID="TextBox30" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                 <asp:Label ID="Label43" runat="server" ForeColor="Red"></asp:Label>
                 <br />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                 <asp:Button ID="Button56" runat="server" Text="GO" Width="120px" Font-Bold="True" ForeColor="Blue" />
             </div>
                  </asp:Panel>
             
            
             <asp:Panel ID="Panel5" runat="server" BackColor="#CCFFFF">
                 <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%">
                     <Columns>
                         <asp:BoundField DataField="ra_no" HeaderText="R.A. No" />
                         <asp:BoundField DataField="wo_slno" HeaderText="W.O. SLNo" />
                         <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                         <asp:BoundField DataField="w_au" HeaderText="Acc.Unit" />
                         <asp:BoundField DataField="from_date" HeaderText="From Date" />
                         <asp:BoundField DataField="to_date" HeaderText="To Date" />
                         <asp:BoundField DataField="work_qty" HeaderText="Work Qty" />
                         <asp:BoundField DataField="rqd_qty" HeaderText="Rqd. Qty." />
                         <asp:BoundField DataField="bal_qty" HeaderText="Bal. Qty" />
                         <asp:BoundField DataField="total_amt" HeaderText="Base Value" />
                     </Columns>
                 </asp:GridView>
             </asp:Panel>
         </asp:Panel>
        </div>
                    </center>
    
</asp:Content>
