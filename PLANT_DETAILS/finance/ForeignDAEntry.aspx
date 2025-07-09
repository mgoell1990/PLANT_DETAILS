<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="ForeignDAEntry.aspx.vb" Inherits="PLANT_DETAILS.WebForm1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; line-height: 20px;" >
            <br />
 <asp:Panel ID="BE_Panel" runat="server" BorderColor="Red" BorderStyle="Double" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="800px" CssClass="brds">
                 <asp:Label ID="Label470" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="FOREIGN MATERIAL DA  ENTRY" Width="100%" CssClass="brds"></asp:Label>
        <br />
    
                 <asp:Label ID="Label471" runat="server" Text="B.E No." Width="120px"></asp:Label>
                 <asp:DropDownList ID="TextBox202" runat="server" AutoPostBack="True" Width="124px">
                 </asp:DropDownList>






                 <asp:Label ID="Label472" runat="server" Text="Date" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_date_TextBox4" runat="server" Width="100px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label473" runat="server" Text="B.L. No" Width="120px" ></asp:Label>
                 <asp:TextBox ID="be_bl_no_TextBox6" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <asp:Label ID="Label474" runat="server" Text="Date" Width="120px" ></asp:Label>
                 <asp:TextBox ID="be_bl_no_date_TextBox7" runat="server" Width="100px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label502" runat="server" Text="Inv. No." Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox181" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <asp:Label ID="Label503" runat="server" Text="Date" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox182" runat="server" Width="100px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label476" runat="server" Text="Purchase Order No." Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox196" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <asp:Label ID="Label498" runat="server" Text="Supl. Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox177" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="300px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label477" runat="server" Text="Material SlNo" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox197" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <asp:Label ID="Label499" runat="server" Text="Mat. Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox178" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="300px"></asp:TextBox>
                 <asp:TextBox ID="TextBox183" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="80px"></asp:TextBox>
                  <br />
                 <asp:Label ID="Label481" runat="server" Text=" BE Quantity" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_quantity_TextBox5" runat="server" BackColor="White" Enabled="False" ForeColor="Blue" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label520" runat="server" Text="Rcd. Qty." Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox200" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label478" runat="server" Text="Transport Mode" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox201" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <asp:Label ID="Label479" runat="server" Text="Ship / Flight Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_ship_flight_name_TextBox8" runat="server" Width="300px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 
                 <br />
                 <asp:Label ID="Label480" runat="server" Text="CHA Contract" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox198" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <asp:Label ID="Label496" runat="server" Text="Cont. Name" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox179" runat="server" BackColor="#99FF33" Enabled="False" Font-Size="9pt" ForeColor="#CC0000" Width="300px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label22" runat="server" Text="CHA Work SlNo" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox199" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                 <asp:Label ID="Label497" runat="server" Text="Job Details" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox180" runat="server" BackColor="#99FF33" Enabled="False" ForeColor="#CC0000" Width="300px"></asp:TextBox>
               
                 <br />
                 <asp:Label ID="Label529" runat="server" BackColor="#00CCFF" style="text-align: left" Width="100%">DA ENTRY</asp:Label>
                 <br />
                 <br />
                 <asp:Label ID="Label530" runat="server" Text="DA ENTRY" Width="120px"></asp:Label>
                 <asp:DropDownList ID="DropDownList2" runat="server" Width="120px" AutoPostBack="True">
                     <asp:ListItem>Select</asp:ListItem>
                     <asp:ListItem>Material Charge</asp:ListItem>
                     <asp:ListItem>Insurance Charge</asp:ListItem>
                     <asp:ListItem>Custom Duty</asp:ListItem>
                     <asp:ListItem>Freight Charge</asp:ListItem>
                     <asp:ListItem>Statutory Charges</asp:ListItem>
                     <asp:ListItem>Preview</asp:ListItem>
                 </asp:DropDownList>
               <asp:Panel ID="Panel6" runat="server" Visible="False">
                 <asp:Label ID="Label33" runat="server" BackColor="#00CCFF" style="text-align: left" Text="MATERIAL QUANTITY" Width="100%"></asp:Label>
                  <div runat ="server" style ="float :right ">
                       <asp:Label ID="Label35" runat="server" Width="120px">Rcvd. Quantity</asp:Label>
                 <asp:TextBox ID="TextBox30" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                      <br />
                        <asp:Label ID="Label37" runat="server" Width="120px">Bal. Quantity</asp:Label>
                 <asp:TextBox ID="TextBox32" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                   </div>
                    <br />
                 <asp:Label ID="Label34" runat="server" Width="120px">BE Quantity</asp:Label>
                 <asp:TextBox ID="TextBox29" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                     <br />
                 <asp:Label ID="Label36" runat="server" Width="120px">DA Quantity</asp:Label>
                 <asp:TextBox ID="TextBox31" runat="server" Width="120px"></asp:TextBox>
                 
             
                 <br />
                






                 <asp:Label ID="Label3" runat="server" BackColor="#00CCFF" style="text-align: left" Text="MATERIAL CHARGES" Width="100%"></asp:Label>
                 <br />
                 <br />
                 <asp:Label ID="Label4" runat="server" Width="120px">Material Value</asp:Label>
                 <asp:TextBox ID="TextBox1" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label5" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox7" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                 <br />
 <br />
              
      <asp:Label ID="Label6" runat="server" BackColor="#00CCFF" style="text-align: left" Text="INSURANCE CHARGES" Width="100%"></asp:Label>
              
                  <br />
                 <br />
                 <asp:Label ID="Label8" runat="server" Width="120px">Insurance(Sea)</asp:Label>
                 <asp:TextBox ID="TextBox8" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <br />
                     <asp:Label ID="Label9" runat="server" Text="Insurance" Width="120px"></asp:Label>
                     <asp:TextBox ID="TextBox9" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                  <div runat ="server" style ="float :right;">
                 <asp:Label ID="Label10" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox10" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                 <br />

         <asp:Label ID="Label11" runat="server" BackColor="#00CCFF" style="text-align: left" Text="CUSTOM DUTY" Width="100%"></asp:Label>
                  
                     <br />
                     <asp:Label ID="Label12" runat="server" Text="Custom Duty" Width="120px"></asp:Label>
                     <asp:TextBox ID="TextBox11" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                       <br />
                     <asp:Label ID="Label13" runat="server" Text="Add. Duty" Width="120px"></asp:Label>
                     <asp:TextBox ID="TextBox12" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                  <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label14" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox13" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                   CENVATABLE<br />

          <asp:Label ID="Label15" runat="server" BackColor="#00CCFF" style="text-align: left" Text="FREIGHT CHARGES" Width="100%"></asp:Label>
               <br />
                     <asp:Label ID="Label16" runat="server" Text="Ocean Freight" Width="120px"></asp:Label>
                     <asp:TextBox ID="TextBox14" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                  <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label17" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox15" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                    
                 <br />
                      <asp:Label ID="Label18" runat="server" BackColor="#00CCFF" style="text-align: left" Text="STATUTORY CHARGES" Width="100%"></asp:Label>
                   <br />
                 <asp:Label ID="Label19" runat="server" Text="T.H. Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox16" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <asp:Label ID="Label20" runat="server" Text="Brokerage Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox17" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <br />
                 <asp:Label ID="Label21" runat="server" Text="Container Cln./Wsh." Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox18" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <asp:Label ID="Label23" runat="server" Text="Repair Fecilitation" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox19" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <br />
                 <asp:Label ID="Label24" runat="server" Text="Container Monitering" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox20" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <asp:Label ID="Label25" runat="server" Text="Oncarriage/Inland" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox21" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <br />
                 <asp:Label ID="Label26" runat="server" Text="D.O. Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox22" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <asp:Label ID="Label27" runat="server" Text="Processing Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox23" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <br />
                 <asp:Label ID="Label28" runat="server" Text="Survey Fees" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox24" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <asp:Label ID="Label29" runat="server" Text="BL Fees" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox25" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <br />
                 <asp:Label ID="Label30" runat="server" Text="Documentation Fees" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox26" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                 <asp:Label ID="Label31" runat="server" Text="Destuffing Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox27" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue">0.0000</asp:TextBox>
                  <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label32" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox28" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                 <br />
                 </asp:Panel>
                 <br />
           




                 <asp:Panel ID="Panel1" runat="server" Visible="False">
                 <asp:Label ID="Label505" runat="server" BackColor="#00CCFF" style="text-align: left" Text="MATERIAL CHARGES" Width="100%"></asp:Label>
                 <br />
                 <div runat ="server" style ="float :right ">
                 <asp:Button ID="Button1" runat="server" Text="SAVE" CssClass="bottomstyle"></asp:Button>
                 </div>
                  <br />
                 <br />
                 <asp:Label ID="Label482" runat="server" Width="120px">Material Value</asp:Label>
                 <asp:TextBox ID="MAT_CHARGE_TEXTBOX" runat="server" Width="120px"></asp:TextBox>
                <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label528" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox2" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                 <br />
                 </asp:Panel>
               <asp:Panel ID="Panel2" runat="server" Visible="False">
                     <asp:Label ID="Label521" runat="server" BackColor="#00CCFF" style="text-align: left" Text="INSURANCE CHARGES" Width="100%"></asp:Label>
                 <br />
                 <div runat ="server" style ="float :right ">
                 <asp:Button ID="Button2" runat="server" Text="SAVE" CssClass="bottomstyle"></asp:Button>
                 </div>
                  <br />
                 <br />
                 <asp:Label ID="Label483" runat="server" Width="120px">Insurance(Sea)</asp:Label>
                 <asp:TextBox ID="be_insurance_sea_TextBox18" runat="server" Width="120px"></asp:TextBox>
                 <br />
                     <asp:Label ID="Label487" runat="server" Text="Insurance" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_insu_TextBox13" runat="server" Width="120px"></asp:TextBox>
                  <div runat ="server" style ="float :right;">
                 <asp:Label ID="Label527" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox3" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                 <br />
               </asp:Panel>
             
               
                <asp:Panel ID="Panel3" runat="server" Visible="False">
                     <asp:Label ID="Label522" runat="server" BackColor="#00CCFF" style="text-align: left" Text="CUSTOM DUTY" Width="100%"></asp:Label>
                    <br />
                 <div runat ="server" style ="float :right ">
                 <asp:Button ID="Button3" runat="server" Text="SAVE" CssClass="bottomstyle"></asp:Button>
                 </div>
                     <br />
                     <asp:Label ID="Label504" runat="server" Text="Custom Duty" Width="120px"></asp:Label>
                     <asp:TextBox ID="BE_CUST_TextBox183" runat="server" Width="120px"></asp:TextBox>
                       <br />
                     <asp:Label ID="Label2" runat="server" Text="Add. Duty" Width="120px"></asp:Label>
                     <asp:TextBox ID="BE_CENVAT_TextBox1" runat="server" Width="120px" Height="17px"></asp:TextBox>
                  <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label526" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox4" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                     (CENVAT)<br />
                </asp:Panel>
                 <asp:Panel ID="Panel4" runat="server" Visible="False">
                     <asp:Label ID="Label523" runat="server" BackColor="#00CCFF" style="text-align: left" Text="FREIGHT CHARGES" Width="100%"></asp:Label>
                       <br />
                 <div runat ="server" style ="float :right ">
                 <asp:Button ID="Button4" runat="server" Text="SAVE" CssClass="bottomstyle"></asp:Button>
                 </div>
                     <br />
                     <asp:Label ID="Label485" runat="server" Text="Ocean Freight" Width="120px"></asp:Label>
                     <asp:TextBox ID="be_ocenfreight_TextBox12" runat="server" Width="120px"></asp:TextBox>
                  <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label525" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox5" runat="server" Width="120px" BackColor="White" Enabled="False" ForeColor="Blue"></asp:TextBox>
                </div>
                    
                 <br />
                 </asp:Panel>
                <asp:Panel ID="Panel5" runat="server" Visible="False">
                     <asp:Label ID="Label507" runat="server" BackColor="#00CCFF" style="text-align: left" Text="STATUTORY CHARGES" Width="100%"></asp:Label>
                   <br />
                 <div runat ="server" style ="float :right ">
                 <asp:Button ID="Button5" runat="server" Text="SAVE" CssClass="bottomstyle"></asp:Button>
                 </div>
                 <br />
                 <asp:Label ID="Label508" runat="server" Text="T.H. Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="be_sat_TextBox184" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label512" runat="server" Text="Brokerage Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox188" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label509" runat="server" Text="Container Cln./Wsh." Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox185" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label513" runat="server" Text="Repair Fecilitation" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox189" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label510" runat="server" Text="Container Monitering" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox186" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label514" runat="server" Text="Oncarriage/Inland" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox190" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label511" runat="server" Text="D.O. Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox187" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label515" runat="server" Text="Processing Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox191" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label516" runat="server" Text="Survey Fees" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox192" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label517" runat="server" Text="BL Fees" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox193" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label518" runat="server" Text="Documentation Fees" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox194" runat="server" Width="120px"></asp:TextBox>
                 <asp:Label ID="Label519" runat="server" Text="Destuffing Charges" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox195" runat="server" Width="120px"></asp:TextBox>
                  <div runat ="server" style ="float :right ">
                 <asp:Label ID="Label7" runat="server" Width="120px">Last Update Date</asp:Label>
                 <asp:TextBox ID="TextBox6" runat="server" Width="120px"></asp:TextBox>
                </div>
                     <br />
                     <asp:Label ID="Label531" runat="server" Text="Total" Width="120px"></asp:Label>
                     <asp:TextBox ID="TextBox203" runat="server" BackColor="White" Enabled="False" ForeColor="Blue" Width="120px"></asp:TextBox>
                     <asp:LinkButton ID="LinkButton1" runat="server">TOTAL</asp:LinkButton>
                 <br />
                </asp:Panel>
     <asp:Panel ID="Panel7" runat="server" Visible="False">
          <asp:Label ID="Label524" runat="server" BackColor="#00CCFF" ForeColor="#00CCFF" style="text-align: center" Text="STATUTORY CHARGES" Width="100%"></asp:Label>
                 <br />
                 <asp:Button ID="BE_CLOSE" runat="server" CssClass="bottomstyle" Text="CLOSE" Width="90px" />
                         <asp:Button ID="BE_SAVE" runat="server" CssClass="bottomstyle" Text="SAVE" Width="90px" />
                         <asp:Button ID="BE_CANCEL" runat="server" CssClass="bottomstyle" Text="CANCEL" Width="90px" />
                 &nbsp;<br /> <asp:Label ID="Label501" runat="server" ForeColor="Red"></asp:Label>
     </asp:Panel>
        </asp:Panel>
           
 </div> 
        </center> 
</asp:Content>

