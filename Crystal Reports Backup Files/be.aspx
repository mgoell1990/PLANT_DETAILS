﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="be.aspx.vb" Inherits="PLANT_DETAILS.be" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; line-height: 20px;"  >
 <asp:Panel ID="BE_Panel" runat="server" BorderColor="Red" BorderStyle="Double" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="800px" CssClass="brds">
                 <asp:Label ID="Label470" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="BE ENTRY" Width="100%" CssClass="brds"></asp:Label>
        <br />
    
                 <asp:Label ID="Label471" runat="server" Text="B.E No." Width="120px"></asp:Label>
                 <asp:TextBox ID="be_no_TextBox3" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label472" runat="server" Text="Date" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_date_TextBox4" runat="server" Width="100px"></asp:TextBox>
                 <asp:CalendarExtender ID="be_date_TextBox4_CalendarExtender" runat="server" CssClass ="red" Format ="dd-MM-yyyy" BehaviorID="be_date_TextBox4_CalendarExtender" TargetControlID="be_date_TextBox4" />
                 <br />
                 <asp:Label ID="Label473" runat="server" Text="B.L. No" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_bl_no_TextBox6" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label474" runat="server" Text="Date" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_bl_no_date_TextBox7" runat="server" Width="100px"></asp:TextBox>
                 <asp:CalendarExtender ID="be_bl_no_date_TextBox7_CalendarExtender" runat="server" BehaviorID="be_bl_no_date_TextBox7_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="be_bl_no_date_TextBox7" />
                 <br />
                 <asp:Label ID="Label502" runat="server" Text="Inv. No." Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox181" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label503" runat="server" Text="Date" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox182" runat="server" Width="100px"></asp:TextBox>
                 <asp:CalendarExtender ID="TextBox182_CalendarExtender" runat="server" BehaviorID="TextBox182_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox182" />
                 <br />
                 <asp:Label ID="Label476" runat="server" Text="Purchase Order No." Width="120px"></asp:Label>
                 <asp:DropDownList ID="be_purchase_order_no_DropDownList2" runat="server" AutoPostBack="True" Width="120px">
                 </asp:DropDownList>
                 <asp:Label ID="Label498" runat="server" Text="Supl. Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox177" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="300px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label477" runat="server" Text="Material SlNo" Width="120px"></asp:Label>
                 <asp:DropDownList ID="be_material_slno_DropDownList3" runat="server" AutoPostBack="True" Width="120px">
                 </asp:DropDownList>
                 <asp:Label ID="Label499" runat="server" Text="Mat. Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox178" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="300px"></asp:TextBox>
                 <asp:TextBox ID="TextBox183" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="80px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label475" runat="server" Text="Conv. Rate" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_conv_rate_TextBox9" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label478" runat="server" Text="Transport Mode" Width="120px"></asp:Label>
                 <asp:DropDownList ID="be_transport_mode_DropDownList1" runat="server" Width="120px">
                     <asp:ListItem>N/A</asp:ListItem>
                     <asp:ListItem>By Ship</asp:ListItem>
                     <asp:ListItem>By Air</asp:ListItem>
                 </asp:DropDownList>
                 <br />
                 <asp:Label ID="Label479" runat="server" Text="Ship / Flight Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_ship_flight_name_TextBox8" runat="server" Width="300px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label480" runat="server" Text="CHA Contract" Width="120px"></asp:Label>
                 <asp:DropDownList ID="be_cha_contract_DropDownList4" runat="server" AutoPostBack="True" Width="120px">
                 </asp:DropDownList>
                 <asp:Label ID="Label496" runat="server" Text="Cont. Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox179" runat="server" BackColor="#99FF33" Enabled="False" Font-Size="9pt" ForeColor="#CC0000" Width="300px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label22" runat="server" Text="CHA Work SlNo" Width="120px"></asp:Label>
                 <asp:DropDownList ID="be_cha_work_slno_DropDownList6" runat="server" AutoPostBack="True" Width="120px">
                 </asp:DropDownList>
                 <asp:Label ID="Label497" runat="server" Text="Job Details" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox180" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="300px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label481" runat="server" Text=" BE Quantity" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_quantity_TextBox5" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label482" runat="server" Width="120px">Ocean Freight</asp:Label>
                 <asp:TextBox ID="be_ocean_freight_TextBox10" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label483" runat="server" Width="120px">Insurance(Sea)</asp:Label>
                 <asp:TextBox ID="be_insurance_sea_TextBox18" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label504" runat="server" Text="Sat. Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="BE_SAT_CHARGE_TextBox183" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <div runat="server" style="float :left;">
                     <asp:Label ID="Label485" runat="server" Text="BCD" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_bcd_TextBox12" runat="server" Width="120px"></asp:TextBox>
                     <br />
                     <asp:Label ID="Label487" runat="server" Text="CVD" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_cvd_TextBox13" runat="server" Width="120px"></asp:TextBox>
                     <br />
                     <asp:Label ID="Label500" runat="server" Text="SAD" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_sad_TextBox181" runat="server" Width="120px"></asp:TextBox>
                 </div>
                 <div runat="server" style="float :right; height: 143px; width: 438px; text-align: left;">
                     <asp:Label ID="Label489" runat="server" Text="ED Cess On CVD" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_ed_cess_on_cvd_TextBox14" runat="server" Width="120px"></asp:TextBox>
                     <br />
                     <asp:Label ID="Label491" runat="server" Text="SHE Cess On CVD" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_she_cess_on_cvd_TextBox15" runat="server" Width="120px"></asp:TextBox>
                     <br />
                     <asp:Label ID="Label493" runat="server" Text="Custom Edu. Cess" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_custom_edu_cess_TextBox16" runat="server" Width="120px"></asp:TextBox>
                     <br />
                     <asp:Label ID="Label495" runat="server" Text="Custom SHE Cess" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_custom_she_cess_TextBox17" runat="server" Width="120px"></asp:TextBox>
                     <div style="text-align: right">
                         <asp:Button ID="BE_CLOSE" runat="server" CssClass="bottomstyle" Text="CLOSE" Width="90px" />
                         <asp:Button ID="BE_SAVE" runat="server" CssClass="bottomstyle" Text="SAVE" Width="90px" />
                         <asp:Button ID="BE_CANCEL" runat="server" CssClass="bottomstyle" Text="CANCEL" Width="90px" />
                     </div>
                 </div>
                 <br />
                 <br />
                 <br />
                 <br />
                 <br />
                 &nbsp;<asp:Label ID="Label501" runat="server" ForeColor="Red"></asp:Label>
                 <br />
                 <br />
             </asp:Panel>
            </div>
        </center>
</asp:Content>