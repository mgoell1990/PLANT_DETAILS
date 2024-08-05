<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="RAW_MAT_CORR.aspx.vb" Inherits="PLANT_DETAILS.RAW_MAT_CORR" %>
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
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
       <div runat ="server" style ="min-height :600px;">
            <asp:Panel ID="Panel12" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px"></asp:DropDownList>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                <br />
                 <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="VIEW"></asp:Button>
               </asp:Panel>
           <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Width="100%" Font-Names="Times New Roman">
               <asp:GridView ID="GridView210" runat="server" AutoGenerateColumns="False" Width="100%">
                   <Columns>
                       <asp:BoundField DataField="ISSUE_NO" HeaderText="Ref. No" />
                       <asp:BoundField DataField="LINE_NO" HeaderText="Line No" />
                       <asp:BoundField DataField="LINE_DATE" HeaderText="Line Date" />
                       <asp:BoundField DataField="LINE_TYPE" HeaderText="Type" />
                       <asp:BoundField DataField="MAT_CODE" HeaderText="Mat. Code" />
                       <asp:BoundField DataField="MAT_QTY" HeaderText="Pur. Qty" />
                       <asp:BoundField DataField="ISSUE_QTY" HeaderText="Issue Qty" />
                       <asp:BoundField DataField="COST_CODE" HeaderText="Pur/Issue To" />
                       <asp:TemplateField HeaderText="Unit Rate"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Disc. Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Pf. Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="ED. Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="VAT/CST"></asp:TemplateField>
                       <asp:TemplateField HeaderText="ANL. Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="LD. Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Pen. Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Freight Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Local Freight"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Entry Tax"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Trans. Charge"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Mat Val"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Loss In Trans."></asp:TemplateField>
                       <asp:TemplateField HeaderText="Wt. Var. Val."></asp:TemplateField>
                       <asp:TemplateField HeaderText="Loss On ED"></asp:TemplateField>
                       <asp:TemplateField HeaderText="Trans. Shrot."></asp:TemplateField>
                       <asp:TemplateField HeaderText="Trans. Penality"></asp:TemplateField>
                   </Columns>
               </asp:GridView>
              
              
                </asp:Panel>
           </div> 
        </center> 
</asp:Content>
