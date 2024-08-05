<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="print_invoice.aspx.vb" Inherits="PLANT_DETAILS.print_invoice" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
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
        <asp:Panel ID="Panel4" runat="server" BorderColor="#FF33CC" BorderStyle="Groove" style="text-align: left" Width="500px" Font-Size="Small">
               <asp:Label ID="Label314" runat="server" BackColor="#3366FF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="White" style="text-align: center" Text="PRINT INVOICE" Width="100%"></asp:Label>
                <br />
               <br />
               <div runat ="server" style ="float :right; width: 122px; text-align: left;">
               
                  <asp:RadioButtonList ID="RadioButtonList1" runat="server" Font-Size="XX-Small" Width="112px">
                      <asp:ListItem>Despatch</asp:ListItem>
                      <asp:ListItem>Service</asp:ListItem>
                  </asp:RadioButtonList>

              </div>
            <br />
            <br />
               
                   <asp:Label ID="Label311" runat="server" Font-Bold="True" Text="Invoice No" Width="120px" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
             <asp:TextBox ID="txtContactsSearch" runat="server" Width="100px"></asp:TextBox>
                 
                   <asp:Label ID="Label316" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 
                   <br />
                   <asp:Label ID="Label312" runat="server" Font-Bold="True" Text="Financial Year" Width="120px" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox72" runat="server" Width="100px"></asp:TextBox>
                   <asp:Label ID="Label315" runat="server" ForeColor="Red" Text="*"></asp:Label>
                   <br />
                   <asp:Button ID="Button38" runat="server" Font-Bold="True" ForeColor="Blue" Text="PRINT EXTRA COPY" Font-Size="Small" />
                   <asp:Button ID="Button39" runat="server" Font-Bold="True" ForeColor="Blue" Text="VIEW PENDING PRINT" Font-Size="Small" />
                   <br />
                   <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="3px" CellPadding="4" EmptyDataText="No More Invoice To Print" Font-Names="Times New Roman" Font-Size="Smaller" Visible="False"    >
         <Columns>
             <asp:BoundField DataField="INV_NO" HeaderText="INVOICE NO" >
             <ItemStyle Font-Bold="True" ForeColor="Blue" />
             </asp:BoundField>
             <asp:TemplateField HeaderText="ORIGINAL">
             <ItemTemplate>
             <asp:linkButton ID="Button1" ForeColor ="BLUE" runat="server" Text='<%#Eval("PRINT_ORIGN")%>' Commandname ='<%#Eval("INV_NO")%>'   OnClick = "ORIGINAL" />
             </ItemTemplate> 
                 <ItemStyle ForeColor="Blue" Width="100px" />
                 </asp:TemplateField>
             <asp:TemplateField HeaderText="DUPLICATE">
               <ItemTemplate > 
             <asp:linkButton ID="Button2" ForeColor ="BLUE" runat="server" Text='<%#Eval("PRINT_TRANS")%>' Commandname = '<%#Eval("INV_NO")%>'  OnClick = "DUPLICATE" />
             </ItemTemplate> 
                 <ItemStyle ForeColor="Blue" Width="100px" />
                 </asp:TemplateField>
             <asp:TemplateField HeaderText="TRIPLICATE">
              <ItemTemplate > 
             <asp:linkButton ID="Button3" ForeColor ="BLUE" runat="server" Text='<%#Eval("PRINT_ASSAE")%>' Commandname = '<%#Eval("INV_NO")%>'  OnClick = "TRIPLICATE" />
             </ItemTemplate>
                 <ItemStyle ForeColor="Blue" Width="100px" />
             </asp:TemplateField>
         </Columns>
         <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
         <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
         <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
         <RowStyle BackColor="White" ForeColor="Blue" />
         <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Blue" />
         <SortedAscendingCellStyle BackColor="#FEFCEB" />
         <SortedAscendingHeaderStyle BackColor="#AF0101" />
         <SortedDescendingCellStyle BackColor="#F6F0C0" />
         <SortedDescendingHeaderStyle BackColor="#7E0000" />
     </asp:GridView>
          </asp:Panel>    
        </div> 
        </center> 
</asp:Content>
