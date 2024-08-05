<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="psc_settlement.aspx.vb" Inherits="PLANT_DETAILS.psc_settlement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px;">
            <asp:Label ID="Label4" runat="server" Text="Adjustment Type" ForeColor="Blue" Width="125px"></asp:Label>
                     <asp:DropDownList ID="DropDownList4" runat="server" Width="128px" AutoPostBack="True">
                         <asp:ListItem>Select</asp:ListItem>
                         <asp:ListItem>PSC Settlement</asp:ListItem>
                         <asp:ListItem>Issue Adj. Entry</asp:ListItem>
                         <asp:ListItem>Raw Mat. Avg. Correction</asp:ListItem>
                     </asp:DropDownList>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="View1" runat="server">
                    <asp:Panel ID="Panel7" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left">
                    <div runat ="server" style ="float :right ">
                    <asp:Label ID="Label21" runat="server" Text="Date"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <cc1:calendarextender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox2" />
                    </div>
                    <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="PSC SETTLEMENT" Width="20%"></asp:Label>
                    </div>
                    <br />
                    <br />    
                     
                    <asp:Label ID="Label3" runat="server" ForeColor="Blue" style="text-align: left" Text="Settlement No" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="125px" Font-Names="Times New Roman" Font-Bold="True"></asp:TextBox>
                    <br />     
                    <asp:Label ID="Label5" runat="server" Text="A/c No" ForeColor="Blue" Width="125px"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="128px"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label16" runat="server" Text="Party Code" ForeColor="Blue" Width="125px"></asp:Label>
                    <asp:DropDownList ID="DropDownList3" runat="server" Width="128px"></asp:DropDownList>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Department" ForeColor="Blue" Width="125px"></asp:Label>
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="128px">
                         <asp:ListItem>Raw Material</asp:ListItem>
                         <asp:ListItem>Store</asp:ListItem>
                         <asp:ListItem>Contracts</asp:ListItem>
                        <asp:ListItem>Transport</asp:ListItem>
                        </asp:DropDownList>
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
                    <asp:Button ID="Button20" runat="server" Text="EXCEL" CssClass="bottomstyle" Width="80px" />
                    <asp:Button ID="Button1" runat="server" CssClass="bottomstyle" Text="ADJUST" Width="80px" />     
                    <asp:Button ID="Button12" runat="server" CssClass="bottomstyle" Text="PRINT" Width="80px" />
                    <br />
                    <br />
                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                             
                    <asp:BoundField DataField="AC_NO" HeaderText="A/c No" />
                    <asp:BoundField DataField="ac_description" HeaderText="A/c Name" />
                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" />
                    <asp:BoundField DataField="GARN" HeaderText="GARN NO" />
                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No" />
                    <asp:BoundField DataField="SUPL_ID" HeaderText="Supplier" />
                    <asp:BoundField DataField="Efective_Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="DR" HeaderText="DR" />
                    <asp:BoundField DataField="CR" HeaderText="CR" />
                             
                    </Columns>
                    </asp:GridView>

                    </asp:Panel>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <asp:Panel ID="Panel1" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left">
                    <div runat ="server" style ="float :right ">
                    <asp:Label ID="Label6" runat="server" Text="Date"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <cc1:calendarextender ID="Calendarextender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox3" />
                    </div>
                    <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="ISSUE Revise" Width="20%"></asp:Label>
                    </div>
                    <br />
                    <br />    
                     
                    <asp:Label ID="Label8" runat="server" ForeColor="Blue" style="text-align: left" Text="Issue Revise No" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="125px" Font-Names="Times New Roman" Font-Bold="True"></asp:TextBox>
                    <br />     
                    
                    <asp:Label ID="Label12" runat="server" ForeColor="Blue" style="text-align: left" Text="Date Between" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox5" />
                    <br />
                    <asp:Label ID="Label13" runat="server" ForeColor="Blue" Text="To" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox6" />     
                    <br />
                    <asp:Label ID="Label18" runat="server" ForeColor="Red" Width="275px" ClientIDMode="Predictable"></asp:Label>
                    
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" CssClass="bottomstyle" Text="GO" Width="80px" />
                    <asp:Button ID="Button3" runat="server" Text="EXCEL" CssClass="bottomstyle" Width="80px" />
                    <asp:Button ID="Button4" runat="server" CssClass="bottomstyle" Text="ADJUST" Width="80px" />     
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
                    <Columns>
                    
                    <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />         
                    <asp:BoundField DataField="AC_NO" HeaderText="A/c No" />
                    <asp:BoundField DataField="ac_description" HeaderText="A/c Name" />
                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" />
                    <asp:BoundField DataField="GARN" HeaderText="GARN NO" />
                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No" />
                    <asp:BoundField DataField="Efective_Date" HeaderText="Effective Date" DataFormatString="{0:yyyy-MM-dd}"/>
                    <asp:BoundField DataField="Entry_Date" HeaderText="Entry Date" />
                    <asp:BoundField DataField="DR" HeaderText="DR" />
                    <asp:BoundField DataField="CR" HeaderText="CR" />
                             
                    </Columns>
                    </asp:GridView>

                    </asp:Panel>
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <asp:Panel ID="Panel2" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left">
                    <div runat ="server" style ="float :right ">
                    <asp:Label ID="Label9" runat="server" Text="Date"></asp:Label>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                    <cc1:calendarextender ID="Calendarextender4" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox3" />
                    </div>
                    <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="ISSUE Adjustment JE Entry" Width="31%"></asp:Label>
                    </div>
                    <br />
                    <br />    
                     
                    <asp:Label ID="Label11" runat="server" ForeColor="Blue" style="text-align: left" Text="Issue Revise No" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox8" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="125px" Font-Names="Times New Roman" Font-Bold="True"></asp:TextBox>
                    <br />     
                    
                    <asp:Label ID="Label19" runat="server" ForeColor="Blue" style="text-align: left" Text="Date Between" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox9" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox5" />
                    <br />
                    <asp:Label ID="Label20" runat="server" ForeColor="Blue" Text="To" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox12" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox6" />     
                    <br />
                    <asp:Label ID="Label22" runat="server" ForeColor="Red" Width="275px" ClientIDMode="Predictable"></asp:Label>
                    
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button21" runat="server" CssClass="bottomstyle" Text="GO" Width="80px" />
                    <asp:Button ID="Button8" runat="server" CssClass="bottomstyle" Text="ADJUST" Width="80px" />     
                    <br />
                    <br />
                        <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="MAT_CODE" HeaderText="MAT Code" />
                                <asp:BoundField DataField="MAT_NAME" HeaderText="MAT Name" />
                                <asp:TemplateField HeaderText="OPEN QTY"></asp:TemplateField>
                                <asp:TemplateField HeaderText="OPENING VALUE"></asp:TemplateField>
                                <asp:TemplateField HeaderText="RCD QTY"></asp:TemplateField>
                                <asp:TemplateField HeaderText="RCD VALUE"></asp:TemplateField>
                                <asp:TemplateField HeaderText="ISSUE QTY"></asp:TemplateField>
                                <asp:TemplateField HeaderText="ISSUE VALUE"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Desired Issue Value"></asp:TemplateField>
                                <asp:TemplateField HeaderText="Diff. Issue VALUE"></asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>

                    </asp:Panel>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <asp:Panel ID="Panel3" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left">
                    <div runat ="server" style ="float :right ">
                    <asp:Label ID="Label23" runat="server" Text="Date"></asp:Label>
                    <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                    <cc1:calendarextender ID="Calendarextender7" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox3" />
                    </div>
                    <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="RAW MAT. AVG. CORRECTION" Width="38%"></asp:Label>
                    </div>
                    <br />
                    <br />    
                     
                    <asp:Label ID="Label25" runat="server" ForeColor="Blue" style="text-align: left" Text="Issue Revise No" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox14" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="125px" Font-Names="Times New Roman" Font-Bold="True"></asp:TextBox>
                    <br />     
                    
                    <asp:Label ID="Label26" runat="server" ForeColor="Blue" style="text-align: left" Text="Date Between" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox15" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender8" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox5" />
                    <br />
                    <asp:Label ID="Label27" runat="server" ForeColor="Blue" Text="To" Width="125px"></asp:Label>
                    <asp:TextBox ID="TextBox16" runat="server" Width="125px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender9" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox6" />     
                    <br />
                    <asp:Label ID="Label28" runat="server" ForeColor="Red" Width="275px" ClientIDMode="Predictable"></asp:Label>
                    
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button6" runat="server" CssClass="bottomstyle" Text="GO" Width="80px" />
                    <asp:Button ID="Button7" runat="server" CssClass="bottomstyle" Text="ADJUST MAT. AVG." Width="151px" />     
                    <br />
                    <br />
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="100%">
                         <Columns>
                             
                             <asp:BoundField DataField="MAT_CODE" HeaderText="MAT Code" />
                             <asp:BoundField DataField="MAT_NAME" HeaderText="MAT Name" />
                             <asp:TemplateField HeaderText="OPEN QTY"></asp:TemplateField> 
                             <asp:TemplateField HeaderText="OPENING VALUE"></asp:TemplateField>
                             <asp:TemplateField HeaderText="RCD QTY"></asp:TemplateField> 
                             <asp:TemplateField HeaderText="RCD VALUE"></asp:TemplateField>
                             <asp:TemplateField HeaderText="ISSUE QTY"></asp:TemplateField> 
                             <asp:TemplateField HeaderText="ISSUE VALUE"></asp:TemplateField>
                             <asp:TemplateField HeaderText="MISC SALE QTY"></asp:TemplateField> 
                             <asp:TemplateField HeaderText="MISC SALE VALUE"></asp:TemplateField>
                             <asp:TemplateField HeaderText="CLOSING QTY"></asp:TemplateField> 
                             <asp:TemplateField HeaderText="CLOSING VALUE"></asp:TemplateField>
                             
                         </Columns>
                     </asp:GridView>

                    </asp:Panel>
                </asp:View>
            </asp:MultiView>
            

                 
          </div>
        </center>
</asp:Content>
