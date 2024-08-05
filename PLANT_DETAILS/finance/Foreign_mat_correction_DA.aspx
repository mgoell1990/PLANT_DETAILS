<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Foreign_mat_correction_DA.aspx.vb" Inherits="PLANT_DETAILS.Foreign_mat_correction_DA" %>--%>


<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="Foreign_mat_correction_DA.aspx.vb" Inherits="PLANT_DETAILS.Foreign_mat_correction_DA" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px;">

            <br />
            <div runat ="server" style ="float :right ">
                <asp:Label ID="Label2" runat="server" Text="Date" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
            </div>
            <br />
            <asp:Label ID="Label4" runat="server" Text="Select Type" Font-Names="Times New Roman"></asp:Label>
            <asp:DropDownList ID="DropDownList10" runat="server" Width="125px" AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Exchange Rate</asp:ListItem>
                        <asp:ListItem>SIT</asp:ListItem>
                        <asp:ListItem>Shortage Entry</asp:ListItem>
            </asp:DropDownList>

            

            <asp:MultiView ID="MultiView1" runat="server">

                     <%--=====VIEW 1 EXCHANGE RATE VARIATION START=====--%>
                     <asp:View ID="View1" runat="server">

                        
                     
            <br />
            <br />
            <asp:Panel ID="rcd_Panel0" runat="server" BorderColor="Blue" BorderStyle="Groove" Width="735px" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left" CssClass="brds">
                <asp:Panel ID="Panel15" runat="server" BackColor="#4686F0" Height="54px" style="text-align: left">
                    <asp:Label ID="Label456" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height:40px" Text="Foreign Material Exchange Rate Correction" Height ="40px" Width="100%"></asp:Label>
                </asp:Panel>
            <br />
                <asp:Label ID="Label457" runat="server" ForeColor="Blue" Text="Token No" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox61" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White"></asp:TextBox>
                <br />
                <asp:Panel ID="Panel1" runat="server">
                <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="Select BE No" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="124px"></asp:DropDownList>
                <br />
                <asp:Label ID="Label458" runat="server" ForeColor="Blue" Text="From Date" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox62" runat="server" Width="120px" AutoCompleteType="Disabled"></asp:TextBox>
                
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox62"></asp:CalendarExtender>
                <asp:Label ID="Label459" runat="server" ForeColor="Blue" Text="To Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox63" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox63_CalendarExtender" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox63"></asp:CalendarExtender>
                </asp:Panel>
            
                <asp:Label ID="Label469" runat="server" ForeColor="Blue" Text="Select Quarter" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList28" runat="server" Width="124px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Q1</asp:ListItem>
                    <asp:ListItem>Q2</asp:ListItem>
                    <asp:ListItem>Q3</asp:ListItem>
                    <asp:ListItem>Q4</asp:ListItem>
                </asp:DropDownList>
            <br />
            <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button22" runat="server" Font-Bold="True" ForeColor="Blue" Text="Proceed" Width="80px" CssClass="bottomstyle" />    
                
                <asp:Panel ID="Panel37" runat="server" BorderColor="#0066FF" Height="120px" Visible="False" Width="276px" BorderStyle="Solid">
                          <asp:Label ID="Label618" runat="server" BorderStyle="None" Font-Bold="True" ForeColor="Blue" style="text-align: center" Text="ARE YOU SURE?" Width="100%"></asp:Label>
                          <br />
                          <br />
                          <asp:Label ID="Label42" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                          <asp:TextBox ID="TextBox172" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                          <br />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="Button57" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                          <br />
                          <asp:Label ID="Label43" runat="server" ForeColor="Red"></asp:Label>
                      </asp:Panel>
                
                
                
                
                <asp:Label ID="Label468" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label558" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Text="&lt;marquee&gt;If Once  Generated It can't be changed&lt;/marquee&gt;"></asp:Label>
            </asp:Panel>

                 
                </asp:View>


                <%--=====VIEW 2 SIT START=====--%>
                <asp:View ID="View2" runat="server">
                    <br />
                    <br />
            <asp:Panel ID="Panel2" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left" CssClass="brds">
                <asp:Panel ID="Panel3" runat="server" BackColor="#4686F0" Height="54px" style="text-align: left">
                    <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height:40px" Text="Foreign Material SIT" Height ="40px" Width="100%"></asp:Label>
                </asp:Panel>
            <br />
                <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="Token No" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White"></asp:TextBox>
                <br />
                <asp:Panel ID="Panel4" runat="server">
                <asp:Label ID="Label9" runat="server" ForeColor="Blue" Text="From Date" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" Width="120px" AutoCompleteType="Disabled"></asp:TextBox>
                
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox62"></asp:CalendarExtender>
                <asp:Label ID="Label10" runat="server" ForeColor="Blue" Text="To Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox4" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox63"></asp:CalendarExtender>
                </asp:Panel>
            
                <asp:Label ID="Label11" runat="server" ForeColor="Blue" Text="Select Quarter" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList3" runat="server" Width="124px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Q1</asp:ListItem>
                    <asp:ListItem>Q2</asp:ListItem>
                    <asp:ListItem>Q3</asp:ListItem>
                    <asp:ListItem>Q4</asp:ListItem>
                </asp:DropDownList>
            <br />
            <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Font-Bold="True" ForeColor="Blue" Text="Go" Width="80px" CssClass="bottomstyle" />    
                
                <asp:Button ID="Button58" runat="server" Text="Generate Ledger Entry" />
                
                
                
                
                <asp:Label ID="Label15" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto">
                  <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Font-Size="Small" ShowHeaderWhenEmpty="True" Width="100%">
                      <Columns>
                          <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL ID" />
                          <asp:BoundField DataField="SUPL_NAME" HeaderText="SUPL NAME" />
                          <asp:BoundField DataField="BE_NO" HeaderText="BE No" />
                          <asp:BoundField DataField="AC_NO" HeaderText="A/C CODE" />
                          <asp:BoundField DataField="AC_DESCRIPTION" HeaderText="A/C NAME" />
                          <asp:BoundField DataField="HO_DA" HeaderText="HO_DA_ENTRY" />
                          <asp:BoundField DataField="RCD_VALUATION_SYSTEM" HeaderText="RCD VALUATION SYSTEM" />
                          <asp:BoundField DataField="DIFFERENCE_VALUE" HeaderText="DIFFERENTIAL PURCHASE" />                       

                      </Columns>
                      <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                      <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="XX-Small" ForeColor="#FFFFCC" />
                      <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                      <RowStyle BackColor="White" ForeColor="#330099" />
                      <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                      <SortedAscendingCellStyle BackColor="#FEFCEB" />
                      <SortedAscendingHeaderStyle BackColor="#AF0101" />
                      <SortedDescendingCellStyle BackColor="#F6F0C0" />
                      <SortedDescendingHeaderStyle BackColor="#7E0000" />
                  </asp:GridView>
        </asp:Panel>
                <br />
                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Text="&lt;marquee&gt;If Once  Generated It can't be changed&lt;/marquee&gt;"></asp:Label>
            </asp:Panel>
                </asp:View>


                <%--=====VIEW 3 SHORT CLOSE START=====--%>
                <asp:View ID="View3" runat="server">
                    <br />
                    <br />
            <asp:Panel ID="Panel6" runat="server" BorderColor="Blue" BorderStyle="Groove" Width="735px" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left" CssClass="brds">
                <asp:Panel ID="Panel7" runat="server" BackColor="#4686F0" Height="54px" style="text-align: left">
                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height:40px" Text="Foreign Material Shortage Entry" Height ="40px" Width="100%"></asp:Label>
                </asp:Panel>
            <br />
                <asp:Label ID="Label17" runat="server" ForeColor="Blue" Text="Token No" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox6" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White"></asp:TextBox>
                <br />
                <asp:Panel ID="Panel8" runat="server">
                <asp:Label ID="Label18" runat="server" ForeColor="Blue" Text="Select BE No" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList4" runat="server" Width="124px"></asp:DropDownList>
                <br />
                <asp:Label ID="Label19" runat="server" ForeColor="Blue" Text="From Date" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server" Width="120px" AutoCompleteType="Disabled"></asp:TextBox>
                
                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox62"></asp:CalendarExtender>
                <asp:Label ID="Label20" runat="server" ForeColor="Blue" Text="To Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox8" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox63"></asp:CalendarExtender>
                </asp:Panel>
            
                <asp:Label ID="Label21" runat="server" ForeColor="Blue" Text="Select Quarter" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList5" runat="server" Width="124px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Q1</asp:ListItem>
                    <asp:ListItem>Q2</asp:ListItem>
                    <asp:ListItem>Q3</asp:ListItem>
                    <asp:ListItem>Q4</asp:ListItem>
                </asp:DropDownList>
            <br />
            <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Proceed" Width="80px" CssClass="bottomstyle" />    
                
                <asp:Panel ID="Panel9" runat="server" BorderColor="#0066FF" Height="120px" Visible="False" Width="276px" BorderStyle="Solid">
                          <asp:Label ID="Label22" runat="server" BorderStyle="None" Font-Bold="True" ForeColor="Blue" style="text-align: center" Text="ARE YOU SURE?" Width="100%"></asp:Label>
                          <br />
                          <br />
                          <asp:Label ID="Label23" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                          <asp:TextBox ID="TextBox9" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                          <br />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="Button4" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                          <br />
                          <asp:Label ID="Label24" runat="server" ForeColor="Red"></asp:Label>
                      </asp:Panel>
                
                
                
                
                <asp:Label ID="Label25" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Text="&lt;marquee&gt;If Once  Generated It can't be changed&lt;/marquee&gt;"></asp:Label>
            </asp:Panel>
                </asp:View>
                </asp:MultiView>

            

           </div>
        </center>
</asp:Content>


