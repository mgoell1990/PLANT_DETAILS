<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Raw_Opening_Correction.aspx.vb" Inherits="PLANT_DETAILS.Raw_Opening_Correction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" Style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px;">
                     
            
                    <asp:Panel ID="Panel7" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left">
                    <div runat ="server" style ="float :right ">
                    <asp:Label ID="Label21" runat="server" Text="Date"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <cc1:calendarextender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox2" />
                    </div>
                    <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="FOREIGN MATERIAL CORRECTION" Width="30%"></asp:Label>
                    </div>
                    <br />
                    <br />    
                     
                    <asp:Label ID="Label3" runat="server" ForeColor="Blue" style="text-align: left" Text="Settlement No" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="125px" Font-Names="Times New Roman" Font-Bold="True"></asp:TextBox>
                    <br />
                    <asp:Label ID="Label14" runat="server" ForeColor="Blue" style="text-align: left" Text="Date Between" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox10" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender10" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox10" />
                    <br />
                    <asp:Label ID="Label15" runat="server" ForeColor="Blue" Text="To" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox11" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender11" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox11" />     
                    <br />
                    <asp:Label ID="Label33" runat="server" ForeColor="Red" Width="275px" ClientIDMode="Predictable"></asp:Label>
                    
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button11" runat="server" CssClass="bottomstyle" Text="GO" Width="80px" />
                    <asp:Button ID="Button1" runat="server" CssClass="bottomstyle" Text="ADJUST" Width="80px" />     
                    <br />
                    <br />
                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                             
                                     
                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                    <asp:BoundField DataField="line_no" HeaderText="Line NO" />         
                    </Columns>
                    </asp:GridView>

                    </asp:Panel>
               
            

                 
          </div>
        </center>
</asp:Content>
