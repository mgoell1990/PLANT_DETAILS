﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="budget.aspx.vb" Inherits="PLANT_DETAILS.budget" %>
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
            <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" style="text-align: left" Width="700px" Height="280px" CssClass="brds">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" style="text-align: center; line-height :30px;" Text="BUDGET" Width="100%" BackColor="Blue" CssClass="brds" Font-Size="Large" ForeColor="White" Height="30px"></asp:Label>
                <br />
                <br />
                 <br />
                <br />
                <div runat ="server" style ="float :left ">
                    <asp:Label ID="Label4" runat="server" Text="Budget Code" Width="100px" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="125px"></asp:TextBox>
                <br />
                <asp:Label ID="Label5" runat="server" Text="Budget Name" Width="100px" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="250px" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                </div>
                 <div runat ="server" style ="float :right; width: 321px;">
                 <asp:Label ID="Label6" runat="server" Text="Budget Efective From" Width="120px" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                     <cc1:CalendarExtender ID="TextBox4_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" runat="server" BehaviorID="TextBox4_CalendarExtender" TargetControlID="TextBox4" />
                <br />
                <asp:Label ID="Label7" runat="server" Text="Budget Efective To" Width="120px" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                     <cc1:CalendarExtender ID="TextBox5_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" runat="server" BehaviorID="TextBox5_CalendarExtender" TargetControlID="TextBox5" />
                <br />
                <asp:Label ID="Label8" runat="server" Text="Value" Width="120px" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
               </div>
                <br />
              <div runat ="server" style ="float :left; text-align: left; height: 117px; width: 357px;">
                  <br />
              <asp:Label ID="Label2" runat="server" Text="Budget Desc." style="text-align: center" Width="100%" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                    <br />
                    <asp:TextBox ID="TextBox3" runat="server" Height="91px" Width="100%" BackColor="Red" ForeColor="White"></asp:TextBox>
                </div>
                <div runat ="server" style ="float :right; height: 113px;">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="SAVE" CssClass="bottomstyle" Width="100px"></asp:Button>
                    <asp:Button ID="Button2" runat="server" Text="NEW" CssClass="bottomstyle" Width="100px"></asp:Button>
                    <asp:Button ID="Button3" runat="server" Text="CANCEL" CssClass="bottomstyle" Width="100px"></asp:Button>
                </div>
              
            </asp:Panel>
        </div> 
        </center> 
</asp:Content>