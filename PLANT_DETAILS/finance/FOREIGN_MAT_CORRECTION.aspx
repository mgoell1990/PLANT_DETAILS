<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FOREIGN_MAT_CORRECTION.aspx.vb" Inherits="PLANT_DETAILS.FOREIGN_MAT_CORRECTION" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                    <asp:TextBox ID="TextBox1" runat="server" Width="125px"></asp:TextBox>
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
                             
                    <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" />
                    <asp:BoundField DataField="CRR_DATE" HeaderText="CRR DATE" DataFormatString="{0:yyyy-MM-dd}"/>
                    <asp:BoundField DataField="MAT_SLNO" HeaderText="MAT SL No" />
                    <asp:BoundField DataField="GARN_NO" HeaderText="GARN NO" />
                    <asp:BoundField DataField="SUPL_ID" HeaderText="Supplier" />
                    <asp:BoundField DataField="GARN_Date" HeaderText="EFFECTIVE Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="BE_NO" HeaderText="BE NO" />
                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                             
                    </Columns>
                    </asp:GridView>

                    </asp:Panel>
               
            

                 
          </div>
        </center>
</asp:Content>
