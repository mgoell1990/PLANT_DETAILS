﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="raw_bal.aspx.vb" Inherits="PLANT_DETAILS.raw_bal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif;">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
     <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">
             <asp:Panel ID="Panel12" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px"></asp:DropDownList>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
                <br />
                 <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" Text="VIEW"></asp:Button>
               </asp:Panel>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ISSUE_NO" HeaderText="ISSUE_NO" />
                    <asp:BoundField DataField="LINE_TYPE" HeaderText="LINE_TYPE" />
                    <asp:BoundField DataField="LINE_NO" HeaderText="LINE_NO" />
                    <asp:BoundField DataField="LINE_DATE" HeaderText="LINE_DATE" />
                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT_CODE" />
                    <asp:BoundField DataField="MAT_QTY" HeaderText="MAT_QTY" />
                    <asp:BoundField DataField="ISSUE_QTY" HeaderText="ISSUE_QTY" />
                    <asp:TemplateField HeaderText="UNIT PRICE"></asp:TemplateField>
                    <asp:TemplateField HeaderText="TOTAL PRICE"></asp:TemplateField>
                    <asp:BoundField DataField="AC_PUR" HeaderText="AC_PUR" />
                    <asp:BoundField DataField="AC_ISSUE" HeaderText="AC_ISSUE" />
                    <asp:BoundField DataField="AC_CON" HeaderText="AC_CON" />
                    <asp:TemplateField HeaderText="AVG_PRICE"></asp:TemplateField>
                    <asp:TemplateField HeaderText="BAL STOCK"></asp:TemplateField>
                </Columns>

            </asp:GridView>
            </div> 
         </center> 
</asp:Content>