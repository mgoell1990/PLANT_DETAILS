<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Store_Other_Reports.aspx.vb" Inherits="PLANT_DETAILS.Store_Other_Reports" %>

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
           
             <asp:Panel ID="Panel9" runat="server" Font-Names="Times New Roman" Width="100%" style="text-align: left">
             <asp:Label ID="Label48" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center;line-height :30px;" Text="REPORT" Height ="30px" Width="100%"></asp:Label>
          <br />
                    <br />
                    <asp:Label ID="Label49" runat="server" ForeColor="Blue" Text="Search For"></asp:Label>
                    <asp:DropDownList ID="DropDownList9" runat="server" Width="125px" AutoPostBack="True">
                        <asp:ListItem>Non-Moving Items</asp:ListItem>
                        
                    </asp:DropDownList>


            </asp:Panel>


            <br />

                    <asp:Panel ID="Panel15" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left" Visible="True" Font-Bold="True">
                    
                    <br />
                     <asp:MultiView ID="MultiView1" runat="server">
                       <asp:View ID="View1" runat="server">
                            <div runat ="server" style="text-align: center">
                     <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="NON-MOVING ITEMS" Width="40%"></asp:Label>
                    </div>
                     <br />
                     <asp:Label ID="Label56" runat="server" ForeColor="Blue" Text="Duration" Width="125px"></asp:Label>
                    <asp:DropDownList ID="DropDownList10" runat="server" Width="125px">
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                    </asp:DropDownList>
                           <br />
                           <div runat ="server" style ="float :left ">
                            <asp:Label ID="Label37" runat="server" ForeColor="Blue" style="text-align: left" Text="Date" Width="125px"></asp:Label>
                            <asp:TextBox ID="TextBox19" runat="server" Width="170px" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender16" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox19" />
                            <br />
                                </div>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="Button29" runat="server" CssClass="bottomstyle" Text="GO" Width="80px" />
                     <asp:Button ID="Button30" runat="server" Text="EXCEL" CssClass="bottomstyle" Width="80px" />
                     <asp:Button ID="Button31" runat="server" CssClass="bottomstyle" Text="PRINT" Width="80px" />
                     <br />
                     <br />
                     <br />
                           <asp:Panel ID="Panel16" runat="server" ScrollBars="Auto" Width="100%">
                     <asp:GridView ID="GridView10" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowHeaderWhenEmpty="True" Width="100%">
                         <Columns>
                             
                             <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                             <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                             <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                             <asp:BoundField DataField="MAT_LASTPUR_DATE" HeaderText="LAST PURCHASE DATE" DataFormatString="{0:dd/MM/yyyy}"/>
                             <asp:BoundField DataField="LAST_ISSUE_DATE" HeaderText="LAST ISSUE DATE" DataFormatString="{0:dd/MM/yyyy}"/>
                             <asp:BoundField DataField="ISSUE_FY" HeaderText="FISCAL YEAR" />
                             <asp:BoundField DataField="MAT_STOCK" HeaderText="STOCK" />
                             <asp:BoundField DataField="MAT_VALUE" HeaderText="VALUE" />
                             
                             
                         </Columns>
                     </asp:GridView>
                        </asp:Panel>
                            
                       </asp:View>
                       <asp:View ID="View2" runat="server">
                           
                       </asp:View>
                    </asp:MultiView>
                     <br />    
                    
                    
                 </asp:Panel>
                        
                 

            </div>
        </center>
</asp:Content>

