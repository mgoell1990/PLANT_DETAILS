<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="mat_stock.aspx.vb" Inherits="PLANT_DETAILS.mat_stock" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif;">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
     .0
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
     <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; text-align: left;">
              <br />
            <div runat ="server" style ="float :left " >
                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Type" Width="80px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="120px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Store Material</asp:ListItem>
                    <asp:ListItem>Raw Material</asp:ListItem>
                </asp:DropDownList>
                <br />
              <asp:Label ID="Label3" runat="server" Text="Date" Font-Bold="True" ForeColor="Blue" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox1" runat="server" Width="120px"></asp:TextBox>
                <cc1:calendarextender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox1" />
              <br />
              <asp:Label ID="Label4" runat="server" Text="To" Font-Bold="True" ForeColor="Blue" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox2" runat="server" Width="120px"></asp:TextBox>
                <cc1:calendarextender ID="Calendarextender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox2" />
                </div>
              <asp:Button ID="Button1" runat="server" Text="VIEW" />
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                    <asp:TemplateField HeaderText="OPEN QTY"></asp:TemplateField>
                    <asp:TemplateField HeaderText="OPEN VALUE"></asp:TemplateField>
                    <asp:TemplateField HeaderText="RCD QTY" ></asp:TemplateField>
                    <asp:TemplateField HeaderText="RCD VALUE"></asp:TemplateField>
                    <asp:TemplateField HeaderText="ISSUE_QTY" ></asp:TemplateField>
                    <asp:TemplateField HeaderText="ISSUE VALUE"></asp:TemplateField>
                    <asp:TemplateField HeaderText="CLOSING STOCK"></asp:TemplateField>
                    <asp:TemplateField HeaderText="CLOSING PRICE"></asp:TemplateField>

                </Columns>

            </asp:GridView>
            </div> 
         </center> 
</asp:Content>
