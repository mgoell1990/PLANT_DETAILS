<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="w_report.aspx.vb" Inherits="PLANT_DETAILS.w_report" %>
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
     <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">
            <asp:Panel ID="Panel9" runat="server" Font-Names="Times New Roman" Width="100%" style="text-align: left">
                    <asp:Label ID="Label48" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center;line-height :30px;" Text="REPORT" Height ="30px" Width="100%"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label49" runat="server" ForeColor="Blue" Text="Search For"></asp:Label>
                    <asp:DropDownList ID="DropDownList9" runat="server" Width="125px" AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Order Balance</asp:ListItem>
                        <asp:ListItem>Pending For M.B.</asp:ListItem>
                        <asp:ListItem>Date Wise Work</asp:ListItem>
                        <asp:ListItem>M.B. Details</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Panel ID="Panel10" runat="server" BackColor="#AAEEFF" style="text-align: center" Visible="False" BorderColor="#AAEEFF" BorderStyle="Groove">
                        <br />
                        <asp:Label ID="Label50" runat="server" ForeColor="Blue" Text="Date Between" Width="125px" style="text-align: left"></asp:Label>
                        <asp:TextBox ID="TextBox33" runat="server" Width="125px"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBox33_CalendarExtender" runat="server" BehaviorID="TextBox33_CalendarExtender" TargetControlID="TextBox33" CssClass ="red" Format ="dd-MM-yyyy" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label51" runat="server" ForeColor="Blue" Text="To" Width="125px"></asp:Label>
                        <asp:TextBox ID="TextBox34" runat="server" Width="125px"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBox34_CalendarExtender" runat="server" BehaviorID="TextBox34_CalendarExtender" TargetControlID="TextBox34" CssClass ="red" Format ="dd-MM-yyyy" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button61" runat="server" Text="GO" Width="80px" CssClass="bottomstyle" />
                        <asp:Button ID="Button65" runat="server" Text="PRINT" Width="80px" CssClass="bottomstyle" />
                        <br />
                        <br />
                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="PO_NO" HeaderText="Work Order No." />
                                <asp:BoundField DataField="supl_name" HeaderText="Contractor Name" />
                                <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                                <asp:BoundField DataField="w_au" HeaderText="Acc. Unit" />
                                <asp:BoundField DataField="unit_rate" HeaderText="Unit Rate" />
                                <asp:BoundField DataField="work_qty" HeaderText="Worked Qty." />
                                <asp:BoundField DataField="total_amt" HeaderText="Total Amt." />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="Panel12" runat="server" BackColor="#AAEEFF" style="text-align: center" Visible="False" BorderColor="#AAEEFF" BorderStyle="Groove">
                        <br />
                        <asp:Label ID="Label52" runat="server" ForeColor="Blue" Text="Date Between" Width="125px" style="text-align: left"></asp:Label>
                        <asp:TextBox ID="TextBox35" runat="server" Width="125px"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBox35_CalendarExtender" runat="server" BehaviorID="TextBox35_CalendarExtender" TargetControlID="TextBox35" CssClass ="red" Format ="dd-MM-yyyy" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label53" runat="server" ForeColor="Blue" Text="To" Width="125px"></asp:Label>
                        <asp:TextBox ID="TextBox36" runat="server" Width="125px"></asp:TextBox>
                        <cc1:CalendarExtender ID="TextBox36_CalendarExtender" runat="server" BehaviorID="TextBox36_CalendarExtender" TargetControlID="TextBox36" CssClass ="red" Format ="dd-MM-yyyy" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button62" runat="server" Text="GO" Width="80px" CssClass="bottomstyle" />
                        <asp:Button ID="Button66" runat="server" Text="PRINT" Width="80px" CssClass="bottomstyle" />
                        <br />
                        <br />
                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="PO_NO" HeaderText="Work Order No." />
                                <asp:BoundField DataField="supl_name" HeaderText="Contractor Name" />
                                <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                                <asp:BoundField DataField="w_au" HeaderText="Acc. Unit" />
                                <asp:BoundField DataField="unit_rate" HeaderText="Unit Rate" />
                                <asp:BoundField DataField="work_qty" HeaderText="Worked Qty." />
                                <asp:BoundField DataField="total_amt" HeaderText="Total Amt." />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="Panel13" runat="server" BackColor="#AAEEFF" style="text-align: center" Visible="False" BorderColor="#AAEEFF" BorderStyle="Groove">
                        <br />
                        <asp:Label ID="Label58" runat="server" ForeColor="Blue" Text="Financial Year" Width="125px"></asp:Label>
                        <asp:DropDownList ID="DropDownList13" runat="server" Width="100px" AutoPostBack="True">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label54" runat="server" ForeColor="Blue" style="text-align: left" Text="Work Order No" Width="125px"></asp:Label>
                        <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True" Width="125px">
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Label ID="Label55" runat="server" ForeColor="Blue" Text="M.B. No" Width="125px"></asp:Label>
                        <asp:DropDownList ID="DropDownList12" runat="server" Width="125px">
                        </asp:DropDownList>
                       <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button67" runat="server" Text="PRINT" Width="80px" CssClass="bottomstyle" />
                        <br />
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="Panel14" runat="server" BackColor="#AAEEFF" style="text-align: left" Visible="False" BorderColor="#AAEEFF" BorderStyle="Groove">
                        <br />
                        <asp:Label ID="Label56" runat="server" ForeColor="Blue" Text="Work Order" Width="125px" style="text-align: left"></asp:Label>
                        <asp:DropDownList ID="DropDownList10" runat="server" Width="125px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button64" runat="server" Text="GO" Width="80px" CssClass="bottomstyle" />
                        <asp:Button ID="Button68" runat="server" Text="PRINT" Width="80px" CssClass="bottomstyle" />
                        <br />
                        <asp:Label ID="Label57" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                        <br />
                        <br />
                        <asp:GridView ID="GridView4" runat="server" Width="100%" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="W_SLNO" HeaderText="W.O. SLNo" />
                                <asp:BoundField DataField="W_NAME" HeaderText="Work Name" />
                                <asp:BoundField DataField="W_AU" HeaderText="Acc.Unit" />
                                <asp:BoundField DataField="W_START_DATE" HeaderText="Start Date" />
                                <asp:BoundField DataField="W_END_DATE" HeaderText="End Date" />
                                <asp:BoundField DataField="W_QTY" HeaderText="Order Qty." />
                                <asp:BoundField DataField="W_COMPLITED" HeaderText="Complicated" />
                                <asp:TemplateField HeaderText="Bal. Qty."></asp:TemplateField>
                            </Columns>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>
                         </asp:Panel>
          </asp:Panel>
        
        </div>
        </center>
    </asp:Content>
