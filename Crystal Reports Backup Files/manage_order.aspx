<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="manage_order.aspx.vb" Inherits="PLANT_DETAILS.manage_order" %>
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
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
        <div runat ="server" style ="min-height :600px;">
            <asp:Panel ID="amd_Panel" runat="server" BorderColor="Fuchsia" BorderStyle="Groove" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="815px" CssClass="brds">
                <asp:Label ID="Label475" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" style="text-align: center" Text="ORDER AMENDMENT" Width="100%" CssClass="brds"></asp:Label>
            <br />
                <br />
                <asp:Label ID="Label620" runat="server" Font-Bold="True" Height="30px" Text="AMD. No" Width="150px"></asp:Label>
                <asp:TextBox ID="TextBox809" runat="server" Width="150px"></asp:TextBox>
                <asp:Label ID="Label621" runat="server" Font-Bold="True" Text="Effective Date"></asp:Label>
                <asp:TextBox ID="TextBox810" runat="server" Width="120px"></asp:TextBox>
              
              <br />
                <asp:Label ID="Label468" runat="server" Font-Bold="True" Height="30px" Text="Order Type" Width="150px"></asp:Label>
                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Width="150px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Purchase Order</asp:ListItem>
                    <asp:ListItem>Sale Order</asp:ListItem>
                    <asp:ListItem>Work Order</asp:ListItem>
                    <asp:ListItem>Rate Contract</asp:ListItem>
                </asp:DropDownList>
            <br />
                <asp:Label ID="Label463" runat="server" Font-Bold="True" Height="30px" Text="Order No" Width="150px"></asp:Label>
                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="150px">
                </asp:DropDownList>
              
            <br />
                <asp:Label ID="Label466" runat="server" Font-Bold="True" Height="30px" Text="Order Line No." Width="150px"></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="150px">
                </asp:DropDownList>
             
              
            <br />
                <asp:Label ID="Label476" runat="server" Font-Bold="True" Height="30px" Text="Mat. Code / Work Desc." Width="150px"></asp:Label>
                <asp:DropDownList ID="DropDownList57" runat="server" AutoPostBack="True" Width="150px">
                </asp:DropDownList>
                <asp:Label ID="Label622" runat="server"></asp:Label>
              
            <br />
                <asp:Panel ID="Panel39" runat="server" Visible="False">
                    <div style="border-bottom: 5px double #0000FF; background-color: #808080; height: 238px;">
                        <div runat="server" style="float :left; width: 400px; background-color: #FFFFFF; height: 217px;" class="brds">
                            <asp:Label ID="Label467" runat="server" BackColor="#4686F0" ForeColor="White" Height="27px" style="text-align: center" Text="Previous Value" Width="100%" CssClass="brds"></asp:Label>
                    <br />
                            &nbsp;<br />
                            &nbsp;<asp:Label ID="Label469" runat="server" Font-Bold="False" Text="Old Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox1" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label499" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label470" runat="server" Font-Bold="False" Text="Old Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox2" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label501" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label477" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox747" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label487" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label478" runat="server" Text="Packing/ forwd." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox748" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label488" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label479" runat="server" Text="Excise Duty" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox749" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label489" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label480" runat="server" Text="Vat / C.S.T." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox750" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label490" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label481" runat="server" Text="Freight" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox751" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label491" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label471" runat="server" Font-Bold="False" Text="Old Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox744" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label492" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                      <br />
                        </div>
                        <div runat="server" style="float:right; width: 400px; background-color: #FFFFFF; height: 217px;" class="brds">
                            <asp:Label ID="Label628" runat="server" BackColor="#4686F0" ForeColor="White" Height="28px" style="text-align: center" Text="Amendment Value" Width="100%" CssClass="brds"></asp:Label>
                    <br />
                            &nbsp;<br />
                            &nbsp;<asp:Label ID="Label472" runat="server" Font-Bold="False" Text="New Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox745" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label500" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label473" runat="server" Font-Bold="False" Text="New Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox5" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label502" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label482" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox752" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label498" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label483" runat="server" Text="Packing/ forwd." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox753" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label497" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label484" runat="server" Text="Excise Duty" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox754" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label496" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label485" runat="server" Text="Vat / C.S.T." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox755" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label495" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label486" runat="server" Text="Freight" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox756" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label494" runat="server" Font-Size="Smaller"></asp:Label>
                      <br />
                            &nbsp;<asp:Label ID="Label474" runat="server" Font-Bold="False" Text="New Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox746" runat="server"></asp:TextBox>
                     
                            <asp:Label ID="Label493" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                      <br />
                        </div>
                    </div>
                    <div runat="server" style="float:left; width :600px; height: 78px;">
                        <asp:Label ID="Label503" runat="server"></asp:Label>
                    </div>
                    <div runat="server" style="float:right ">
                        <asp:Button ID="Button65" runat="server" Text="CLOSE" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button64" runat="server" Text="CANCEL" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button63" runat="server" Text="SAVE" Width="90px" CssClass="bottomstyle" />
                    </div>
                   <br />
                  
                    <asp:Label ID="Label464" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" style="text-align: center" Text="ORDER STATUS" Width="100%"></asp:Label>
                    <asp:Panel ID="Panel38" runat="server" ScrollBars="Auto" Width="810px">
                        <asp:GridView ID="GridView212" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="PO_NO" HeaderText="Order No." />
                                <asp:BoundField DataField="AMD_NO" HeaderText="Amd. No." />
                                <asp:BoundField DataField="MAT_SLNO" HeaderText="SLNo." />
                                <asp:BoundField DataField="MAT_CODE" HeaderText="Mat. Code" />
                                <asp:BoundField DataField="MAT_NAME" HeaderText="Mat. Name" />
                                <asp:BoundField DataField="MAT_AU" HeaderText="Acc. Unit" />
                                <asp:BoundField DataField="MAT_QTY" HeaderText="Qty." />
                                <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Price" />
                                <asp:BoundField DataField="MAT_PACK" HeaderText="Packing &amp; fowrd." />
                                <asp:BoundField DataField="MAT_DISCOUNT" HeaderText="Discount" />
                                <asp:BoundField DataField="MAT_EXCISE_DUTY" HeaderText="Excise Duty" />
                                <asp:BoundField DataField="MAT_CST" HeaderText="Vat / C.S.T" />
                                <asp:BoundField DataField="MAT_FREIGHT_PU" HeaderText="Freight" />
                                <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                                <asp:BoundField />
                                <asp:BoundField DataField="AMD_DATE" HeaderText="Efective Date" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel40" runat="server" Visible="False">
                    <div style="border-bottom: 5px double #0000FF; background-color: #808080; height: 274px;">
                        <div runat="server" style="float :left; width: 400px; background-color: #FFFFFF; height: 260px;" class="brds">
                            <asp:Label ID="Label504" runat="server" BackColor="#4686F0" ForeColor="White" Height="27px" style="text-align: center" Text="Previous Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            &nbsp;<br /> &nbsp;<asp:Label ID="Label505" runat="server" Font-Bold="False" Text="Old Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox757" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label506" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label507" runat="server" Font-Bold="False" Text="Old Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox758" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label508" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label509" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox759" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label510" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label511" runat="server" Text="Packing/ forwd." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox760" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label512" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label513" runat="server" Text="Excise Duty" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox761" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label514" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label515" runat="server" Text="Vat / C.S.T." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox762" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label516" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label612" runat="server" Text="Terminal Tax" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox805" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label613" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label614" runat="server" Text="TCS" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox806" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label615" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label517" runat="server" Text="Freight" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox763" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label518" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label519" runat="server" Font-Bold="False" Text="Old Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox764" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label520" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                          <br />
                        </div>
                        <div runat="server" style="float:right; width: 400px; background-color: #FFFFFF; height: 260px;" class="brds">
                            <asp:Label ID="Label521" runat="server" BackColor="#4686F0" ForeColor="White" Height="28px" style="text-align: center" Text="Amendment Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            &nbsp;<br /> &nbsp;<asp:Label ID="Label522" runat="server" Font-Bold="False" Text="New Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox765" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label523" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label524" runat="server" Font-Bold="False" Text="New Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox766" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label525" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label526" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox767" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label527" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label528" runat="server" Text="Packing/ forwd." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox768" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label529" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label530" runat="server" Text="Excise Duty" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox769" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label531" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label532" runat="server" Text="Vat / C.S.T." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox770" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label533" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label616" runat="server" Text="Terminal Tax" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox807" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label617" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label618" runat="server" Text="TCS" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox808" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label619" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label534" runat="server" Text="Freight" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox771" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label535" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label536" runat="server" Font-Bold="False" Text="New Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox772" runat="server"></asp:TextBox>
                          
                            <asp:Label ID="Label537" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                          <br />
                        </div>
                    </div>
                    <div runat="server" style="float:left; width :600px; height: 78px;">
                        <asp:Label ID="Label623" runat="server" Font-Bold="True" Text="Item Details:-"></asp:Label>
                      <br />
                        <asp:Label ID="Label538" runat="server"></asp:Label>
                    </div>
                    <div runat="server" style="float:right ">
                        <asp:Button ID="Button66" runat="server" Text="SAVE" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button67" runat="server" Text="CLOSE" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button68" runat="server" Text="CANCEL" Width="90px" CssClass="bottomstyle" />
                    </div>
                  <br />
                    <asp:Label ID="Label539" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" style="text-align: center" Text="ORDER STATUS" Width="100%"></asp:Label>
                    <asp:Panel ID="Panel41" runat="server" ScrollBars="Auto" Width="810px">
                        <asp:GridView ID="GridView213" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="SO_NO" HeaderText="Order No." />
                                <asp:BoundField DataField="AMD_NO" HeaderText="Amd. No." />
                                <asp:BoundField DataField="ITEM_SLNO" HeaderText="SLNo." />
                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Mat. Code" />
                                <asp:BoundField DataField="ITEM_NAME" HeaderText="Mat. Name" />
                                <asp:BoundField DataField="ITEM_AU" HeaderText="Acc. Unit" />
                                <asp:BoundField DataField="ITEM_QTY" HeaderText="Qty." />
                                <asp:BoundField DataField="ITEM_MT" HeaderText="Qty. (Mt)" />
                                <asp:BoundField DataField="ITEM_UNIT_RATE" HeaderText="Unit Price" />
                                <asp:BoundField DataField="ITEM_PACK" HeaderText="Packing &amp; fowrd." />
                                <asp:BoundField DataField="ITEM_DISCOUNT" HeaderText="Discount" />
                                <asp:BoundField DataField="ITEM_EXCISE_DUTY" HeaderText="Excise Duty" />
                                <asp:BoundField DataField="ITEM_CST" HeaderText="Vat / C.S.T" />
                                <asp:BoundField DataField="ITEM_TERMINAL_TAX" HeaderText="Terminal Tax" />
                                <asp:BoundField DataField="ITEM_TCS" HeaderText="TCS" />
                                <asp:BoundField DataField="ITEM_S_TAX" HeaderText="Service Tax" />
                                <asp:BoundField DataField="ITEM_FREIGHT_PU" HeaderText="Freight" />
                                <asp:BoundField DataField="ITEM_DELIVERY" HeaderText="Delivery Date" />
                                <asp:BoundField DataField="AMD_DATE" HeaderText="Efective Date" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel42" runat="server" Visible="False">
                    <div style="border-bottom: 5px double #0000FF; background-color: #808080; height: 322px;">
                        <div runat="server" style="float :left; width: 400px; background-color: #FFFFFF; height: 305px;" class="brds">
                            <asp:Label ID="Label540" runat="server" BackColor="#4686F0" ForeColor="White" Height="27px" style="text-align: center" Text="Previous Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            &nbsp;<br /> &nbsp;<asp:Label ID="Label541" runat="server" Font-Bold="False" Text="Old Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox773" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label542" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label543" runat="server" Font-Bold="False" Text="Old Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox774" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label544" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label545" runat="server" Text="Material Cost." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox775" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                          <br />
                            &nbsp;<asp:Label ID="Label547" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox776" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label548" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label549" runat="server" Text="Tolerance" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox777" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label550" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label551" runat="server" Text="Service Tax" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox778" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label552" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label553" runat="server" Text="W.C. Tax" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox779" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label554" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label555" runat="server" Font-Bold="False" Text="Old Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox780" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label556" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label624" runat="server" Text="Working Area" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox811" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                          <br />
                            &nbsp;<asp:Label ID="Label625" runat="server" Text="Supplier Id" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox812" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                          <br />
                            &nbsp;<asp:Label ID="Label626" runat="server" Text="Work Type" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox813" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                          <br />
                            &nbsp;<asp:Label ID="Label627" runat="server" Text="Start Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox814" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                          <br />
                          <br />
                        </div>
                        <div runat="server" style="float:right; width: 400px; background-color: #FFFFFF; height: 305px;" class="brds">
                            <asp:Label ID="Label557" runat="server" BackColor="#4686F0" ForeColor="White" Height="28px" style="text-align: center" Text="Amendment Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            &nbsp;<br /> &nbsp;<asp:Label ID="Label558" runat="server" Font-Bold="False" Text="New Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox781" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label559" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label560" runat="server" Font-Bold="False" Text="New Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox782" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label561" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label562" runat="server" Text="Material Cost." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox783" runat="server">0.00</asp:TextBox>
                          <br />
                            &nbsp;<asp:Label ID="Label564" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox784" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label565" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label566" runat="server" Text="Tolerance" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox785" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label567" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label568" runat="server" Text="Service Tax" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox786" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label569" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label570" runat="server" Text="W.C. Tax" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox787" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label571" runat="server" Font-Size="Smaller">%</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label572" runat="server" Font-Bold="False" Text="New Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox788" runat="server"></asp:TextBox>
                        
                            <asp:Label ID="Label573" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                          <br />
                        </div>
                    </div>
                    <div runat="server" style="float:left; width :600px; height: 78px;">
                        <asp:Label ID="Label574" runat="server"></asp:Label>
                    </div>
                    <div runat="server" style="float:right ">
                        <asp:Button ID="Button69" runat="server" Text="SAVE" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button70" runat="server" Text="CLOSE" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button71" runat="server" Text="CANCEL" Width="90px" CssClass="bottomstyle" />
                    </div>
                  <br />
                    <asp:Label ID="Label575" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" style="text-align: center" Text="ORDER STATUS" Width="100%"></asp:Label>
                    <asp:Panel ID="Panel43" runat="server" ScrollBars="Auto" Width="810px">
                        <asp:GridView ID="GridView214" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="PO_NO" HeaderText="Order No." />
                                <asp:BoundField DataField="WO_AMD" HeaderText="Amd. No." />
                                <asp:BoundField DataField="W_SLNO" HeaderText="SLNo." />
                                <asp:BoundField DataField="W_AU" HeaderText="Acc. Unit" />
                                <asp:BoundField DataField="W_QTY" HeaderText="Qty." />
                                <asp:BoundField DataField="W_UNIT_PRICE" HeaderText="Unit Price" />
                                <asp:BoundField DataField="W_MATERIAL_COST" HeaderText="Material Cost." />
                                <asp:BoundField DataField="W_DISCOUNT" HeaderText="Discount" />
                                <asp:BoundField DataField="W_TOLERANCE" HeaderText="Tolerance" />
                                <asp:BoundField DataField="W_STAX" HeaderText="Service Tax" />
                                <asp:BoundField DataField="W_WCTAX" HeaderText="W.C. Tax" />
                                <asp:BoundField DataField="W_END_DATE" HeaderText="Job Validity" />
                                <asp:BoundField />
                                <asp:BoundField DataField="AMD_DATE" HeaderText="Effective Date" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="Panel44" runat="server" Visible="False">
                    <div style="border-bottom: 5px double #0000FF; width: 810px; background-color: #0000FF; height: 228px;">
                        <div runat="server" style="float :left; width: 400px; background-color: #FFFFFF; height: 214px;" class="brds">
                            <asp:Label ID="Label576" runat="server" BackColor="#4686F0" ForeColor="White" Height="27px" style="text-align: center" Text="Previous Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            &nbsp;<br /> &nbsp;<asp:Label ID="Label577" runat="server" Font-Bold="False" Text="Old Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox789" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label578" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label579" runat="server" Font-Bold="False" Text="Old Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox790" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label580" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label581" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox791" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label582" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label583" runat="server" Text="Packing/ forwd." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox792" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label584" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label585" runat="server" Text="Excise Duty" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox793" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label586" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label587" runat="server" Text="Vat / C.S.T." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox794" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label588" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label589" runat="server" Text="Freight" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox795" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label590" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label591" runat="server" Font-Bold="False" Text="Old Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox796" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label592" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                          <br />
                        </div>
                        <div runat="server" style="float:right; width: 400px; background-color: #FFFFFF; height: 214px;" class="brds">
                            <asp:Label ID="Label593" runat="server" BackColor="#4686F0" ForeColor="White" Height="28px" style="text-align: center" Text="Amendment Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            &nbsp;<br /> &nbsp;<asp:Label ID="Label594" runat="server" Font-Bold="False" Text="New Qty." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox797" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label595" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label596" runat="server" Font-Bold="False" Text="New Unit Price" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox798" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label597" runat="server" Font-Size="Smaller" Text="INR"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label598" runat="server" Text="Discount" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox799" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label599" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label600" runat="server" Text="Packing/ forwd." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox800" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label601" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label602" runat="server" Text="Excise Duty" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox801" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label603" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label604" runat="server" Text="Vat / C.S.T." Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox802" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label605" runat="server" Font-Size="Smaller">PERCENTAGE</asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label606" runat="server" Text="Freight" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox803" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label607" runat="server" Font-Size="Smaller"></asp:Label>
                          <br />
                            &nbsp;<asp:Label ID="Label608" runat="server" Font-Bold="False" Text="New Validity Date" Width="120px"></asp:Label>
                            <asp:TextBox ID="TextBox804" runat="server"></asp:TextBox>
                         
                            <asp:Label ID="Label609" runat="server" Font-Size="Smaller" Text="DD-mm-YYYY"></asp:Label>
                          <br />
                        </div>
                    </div>
                    <div runat="server" style="float:left; width :600px; height: 78px;">
                        <asp:Label ID="Label610" runat="server"></asp:Label>
                    </div>
                    <div runat="server" style="float:right ">
                        <asp:Button ID="Button72" runat="server" Text="CLOSE" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button73" runat="server" Text="CANCEL" Width="90px" CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button74" runat="server" Text="SAVE" Width="90px" CssClass="bottomstyle" />
                    </div>
                  <br />
                    <asp:Label ID="Label611" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" style="text-align: center" Text="ORDER STATUS" Width="100%"></asp:Label>
                    <asp:Panel ID="Panel45" runat="server" ScrollBars="Auto" Width="810px">
                        <asp:GridView ID="GridView215" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="PO_NO" HeaderText="Order No." />
                                <asp:BoundField DataField="AMD_NO" HeaderText="Amd. No." />
                                <asp:BoundField DataField="MAT_SLNO" HeaderText="SLNo." />
                                <asp:BoundField DataField="MAT_CODE" HeaderText="Mat. Code" />
                                <asp:BoundField DataField="MAT_NAME" HeaderText="Mat. Name" />
                                <asp:BoundField DataField="MAT_AU" HeaderText="Acc. Unit" />
                                <asp:BoundField DataField="MAT_QTY" HeaderText="Qty." />
                                <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Price" />
                                <asp:BoundField DataField="MAT_PACK" HeaderText="Packing &amp; fowrd." />
                                <asp:BoundField DataField="MAT_DISCOUNT" HeaderText="Discount" />
                                <asp:BoundField DataField="MAT_EXCISE_DUTY" HeaderText="Excise Duty" />
                                <asp:BoundField DataField="MAT_CST" HeaderText="Vat / C.S.T" />
                                <asp:BoundField DataField="MAT_FREIGHT_PU" HeaderText="Freight" />
                                <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                                <asp:BoundField />
                                <asp:BoundField DataField="AMD_DATE" HeaderText="Efective Date" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
<br />
        </div>
    </center>
</asp:Content>
