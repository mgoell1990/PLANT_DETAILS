<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="dpr.aspx.vb" Inherits="PLANT_DETAILS.dpr1" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
            <asp:Panel ID="Panel1" runat="server" BorderColor="#4686F0" BorderStyle="Double" Font-Names="Times New Roman" Font-Size="Small" Width="1000px" style="text-align: left" CssClass="brds">
              <asp:Label ID="Label270" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="DAILY PRODUCTION" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style="border-bottom: 10px double #008000; height: 280px; " >
              <div style ="border-right: 10px double #008000; float :left; width :439px; height: 280px; " runat ="server" >
                  <asp:Label ID="Label274" runat="server" Text="Mat Group:-" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <asp:DropDownList ID="DropDownList2" runat="server" Width="250px" AutoPostBack="True">
                  </asp:DropDownList>
                  <br />
              <asp:Label ID="Label271" runat="server" Text="Mat Code:-" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
              <asp:DropDownList ID="DropDownList1" runat="server" Width="250px" AutoPostBack="True">
              </asp:DropDownList>
              <br />
                  <asp:Label ID="Label273" runat="server" Text="Date:-" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <asp:TextBox ID="TextBox49" runat="server" Font-Bold="True" ForeColor="#FF0066" TabIndex="2" ToolTip="DD-MM-YYYY" Width="150px"></asp:TextBox>
                  <cc1:CalendarExtender ID="TextBox49_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox49_CalendarExtender" TargetControlID="TextBox49" />
                  <asp:Label ID="Label278" runat="server" Text="Date Format(DD-MM-YYYY)" ForeColor="Blue" Font-Size="Smaller"></asp:Label>
                  <br />
                  <asp:Label ID="Label272" runat="server" Font-Bold="True" ForeColor="Blue" Text="F.R. Qty:-" Width="100px"></asp:Label>
                  <asp:TextBox ID="TextBox1" runat="server" Width="150px">0.000</asp:TextBox>
                  <asp:Label ID="Label276" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <br />
                  <asp:Label ID="Label275" runat="server" Font-Bold="True" ForeColor="Blue" Text="B.S.R. Qty:-" Width="100px"></asp:Label>
                  <asp:TextBox ID="TextBox50" runat="server" Width="150px">0.000</asp:TextBox>
                  <asp:Label ID="Label277" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <br />
                  <br />
                  <asp:Label ID="Label279" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <br />
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                  <asp:Button ID="Button33" runat="server" Text="CANCEL" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                  <asp:Button ID="Button34" runat="server" Text="ADD" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                  <asp:Button ID="Button32" runat="server" Text="SAVE" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                  <br />
                  </div>
                   <div style ="float :right; width :550px; height: 279px;" runat ="server" >

                       <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" Width="100%" CssClass="DataWebControlStyle" Height="264px" BackColor="White" Font-Size="Small">
                           <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                           <FieldHeaderStyle CssClass="HeaderStyle"  />
                           <Fields>
                               <asp:BoundField DataField="ITEM_CODE" HeaderText="Mat Code" SortExpression="ITEM_CODE" >
                               <HeaderStyle Width="250px" />
                               <ItemStyle Font-Bold="True" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ITEM_NAME" HeaderText="Mat Name" SortExpression="ITEM_NAME" />
                               <asp:BoundField DataField="ITEM_DRAW" HeaderText="Mat draw" SortExpression="ITEM_DRAW" />
                               <asp:BoundField DataField="ITEM_AU" HeaderText="Mat AU" SortExpression="ITEM_AU" />
                               <asp:BoundField DataField="ITEM_WEIGHT" HeaderText="Mat Unit Weight (Kg)" SortExpression="ITEM_WEIGHT" />
                               <asp:BoundField DataField="ITEM_F_STOCK" HeaderText="Cur FR Stock" SortExpression="ITEM_F_STOCK" />
                               <asp:BoundField DataField="ITEM_B_STOCK" HeaderText="Cur BSR Stock" SortExpression="ITEM_B_STOCK" />
                               <asp:TemplateField HeaderText="Total FRS Weight (Mt)">
                                   <ItemStyle Font-Bold="True" />
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Total BSR Weight (Mt)">
                                   <ItemStyle Font-Bold="True" />
                               </asp:TemplateField>
                               <asp:BoundField DataField="ITEM_LAST_PROD" HeaderText="Last Production(Dt)" SortExpression="ITEM_LAST_PROD" />
                               <asp:BoundField DataField="ITEM_LAST_DESPATCH" HeaderText="Last Despatch(Dt)" SortExpression="ITEM_LAST_DESPATCH" />
                               <asp:BoundField DataField="cmul_qty" HeaderText="Cmul Of This Month(Mt)" />
                           </Fields>
                           <RowStyle CssClass="RowStyle" />
                       </asp:DetailsView>

                  </div>
                   
              </div>
 <asp:GridView ID="GridView1" runat="server"  CssClass="DataWebControlStyle" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                 <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                <RowStyle CssClass="RowStyle" BackColor="White" ForeColor="#330099" />
                                 <HeaderStyle CssClass="HeaderStyle" BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                 <Columns>
                                     <asp:BoundField DataField="ITEM_CODE" HeaderText="Mat Code" />
                                     <asp:BoundField DataField="ITEM_NAME" HeaderText="Mat Name" >
                                     <ItemStyle Width="300px" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="ITEM_AU" HeaderText="Mat AU" />
                                     <asp:BoundField DataField="ITEM_PROD_DATE" HeaderText="Date" />
                                      <asp:BoundField DataField="ITEM_QTY" HeaderText="FR Qty" />
                                     <asp:BoundField DataField="bsr_qty" HeaderText="BSR Qty" />
                                     <asp:BoundField DataField="fr_mt" HeaderText="FR Mt." />
                                     <asp:BoundField DataField="bsr_mt" HeaderText="BSR Mt." />
                                 </Columns>
                                 <FooterStyle CssClass="FooterStyle" BackColor="#FFFFCC" ForeColor="#330099" />
                                 <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                 <PagerStyle CssClass="PagerRowStyle" BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                  <PagerSettings PageButtonCount="5" />
                                 <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                 <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                 <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                 <SortedDescendingHeaderStyle BackColor="#7E0000" />
              </asp:GridView>
          </asp:Panel>





            </div> 
        </center> 
</asp:Content>
