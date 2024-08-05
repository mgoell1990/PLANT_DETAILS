<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="rcm_goods.aspx.vb" Inherits="PLANT_DETAILS.rcm" %>
<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
           

            
            <asp:Panel ID="Panel2" runat="server" BorderColor="#00CC00" BorderStyle="Double" Width="1000px" Font-Names="Times New Roman" style="text-align: left" CssClass="brds" Visible="False">
              <asp:Label ID="Label280" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Overline="False" Font-Size="X-Large" ForeColor="White" style="text-align: center ; line-height :40px" Text="TAX INVOICE (FOR GOODS UNDER REVERSE CHARGE)" Height ="40px" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style="border-bottom: 10px double #008000; width: 100%;" >
 
                &nbsp;<asp:Label ID="Label299" runat="server" Text="Invoice No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox177" runat="server" Width="75px"></asp:TextBox>
                   <asp:TextBox ID="TextBox65" runat="server" Width="75px"></asp:TextBox>
                   &nbsp;<br />
                   &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="S.O. No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox124" runat="server" BackColor="Red" ForeColor="White" Width="150px"></asp:TextBox>
                   <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. Details" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
        <a href="rcm.aspx">rcm.aspx</a>           <br />
                   &nbsp;<asp:Label ID="Label482" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Party Type" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList33" runat="server" Width="152px">
                       <asp:ListItem>Select</asp:ListItem>
                       <asp:ListItem>I.P.T.</asp:ListItem>
                       <asp:ListItem>Other</asp:ListItem>
                   </asp:DropDownList>
                   <asp:Label ID="Label483" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. S.Code" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox188" runat="server"></asp:TextBox>
                   <asp:Label ID="Label484" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. State" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox189" runat="server"></asp:TextBox>
                   <br />
                    &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. For" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList2" runat="server" Width="152px" AutoPostBack="True">
                       <asp:ListItem>Select</asp:ListItem>
                       <asp:ListItem>RCM For Tax Invoice</asp:ListItem>
                       <asp:ListItem>RCM For Payment Voucher</asp:ListItem>
                   </asp:DropDownList>
                   <asp:Panel ID="Panel3" runat="server" Visible="False">
                         &nbsp;<asp:Label ID="Label469" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. No" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList30" runat="server" Width="152px">
                   </asp:DropDownList>
                   &nbsp;<br /> 
                   </asp:Panel>
                 <asp:Panel ID="Panel1" runat="server" Visible="False">
                       &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. No" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox178" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label472" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Inv. Date" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox179" runat="server"></asp:TextBox>
                      <asp:CalendarExtender ID="TextBox83_CalendarExtender" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox179">
                </asp:CalendarExtender>
                  
                       <br />
                       &nbsp;<asp:Label ID="Label473" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="HSN Code" Width="120px"></asp:Label>
                       <asp:DropDownList ID="DropDownList32" runat="server" Width="152px">
                       </asp:DropDownList>
                       <br />
                       &nbsp;<asp:Label ID="Label474" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Mat. Desc." Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox180" runat="server" Width="380px"></asp:TextBox>
                       <asp:Label ID="Label475" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Mat. AU." Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox181" runat="server"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label476" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Mat. Qty." Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox182" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label477" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Unit Rate" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox183" runat="server" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label478" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="CGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox184" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label479" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="SGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox185" runat="server" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label480" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="IGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox186" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label481" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="CESS" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox187" runat="server" Width="150px"></asp:TextBox>
                   </asp:Panel> 
                   </div> 
                <asp:Label ID="Label298" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Notification" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox64" runat="server" Cssclass="notetextboxclass" TextMode="MultiLine" Width="382px"></asp:TextBox>
               <br />
              
                <br />
                &nbsp;<asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
              &nbsp;&nbsp;<br />
                <asp:Button ID="Button1" runat="server" Text="NEW" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
                <asp:Button ID="Button37" runat="server" Text="CANCEL" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button35" runat="server" Text="ADD" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button36" runat="server" Text="SAVE" Width="75px" Font-Size="Small" CssClass="bottomstyle" Enabled="False" />
              <br />
              <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderColor="#00CC00" BorderStyle="Double" BorderWidth="5px" CellPadding="3" Font-Names="Times New Roman" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="100%">
                  <AlternatingRowStyle BackColor="#DCDCDC" />
                  <Columns>
                      <asp:BoundField DataField="GARN_NO" HeaderText="GARN No" />
                      <asp:BoundField DataField="MAT_SLNO" HeaderText="MAT. SLNO" />
                      <asp:BoundField DataField="MAT_CODE" HeaderText="MAT. CODE" />
                      <asp:BoundField DataField="MAT_NAME" HeaderText="MAT. DESC." />
                      <asp:BoundField DataField="MAT_AU" HeaderText="A / U" />
                      <asp:BoundField DataField="CHPTR_HEAD" HeaderText="HSN Code" />
                      <asp:BoundField DataField="UNIT_RATE" HeaderText="Unit Rate" />


                      <asp:BoundField DataField="MAT_QTY" HeaderText="Mat Qty" />
                      <asp:BoundField DataField="TAX_VAL" HeaderText="Taxable Value" />
                      <asp:BoundField DataField="CGST_P" HeaderText="CGST" />
                      <asp:BoundField DataField="CGST" HeaderText="CGST Amt." />

                     <asp:BoundField DataField="SGST_P" HeaderText="CGST" />

                      <asp:BoundField DataField="SGST" HeaderText="SGST Amt." />
                      <asp:BoundField DataField="IGST_P" HeaderText="IGST" />
                      <asp:BoundField DataField="IGST" HeaderText="IGST Amt" />
                      <asp:BoundField DataField="CESS_P" HeaderText="CESS" />
                      <asp:BoundField DataField="CESS" HeaderText="CESS Amt." />
                      <asp:BoundField DataField="TOTAL_VAL" HeaderText="Total value of Goods" />
                  </Columns>
                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                  <HeaderStyle BackColor="#FF0066" BorderColor="Blue" BorderStyle="Double" BorderWidth="2px" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                  <RowStyle BackColor="#EEEEEE" BorderColor="Lime" BorderStyle="Ridge" ForeColor="Black" />
                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                  <SortedDescendingHeaderStyle BackColor="#000065" />
              </asp:GridView>
             
          </asp:Panel>
           <br />


            
            <asp:Panel ID="Panel8" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" Height="146px" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label401" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="ORDER SELECTION" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label470" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Type:-" Width="80px"></asp:Label>
             
                            <asp:DropDownList ID="DropDownList31" runat="server" AutoPostBack="True" Width="300px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Unregistered Party</asp:ListItem>
                                <asp:ListItem>Registered Party</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-" Width="80px" Visible="False"></asp:Label>
             
                            <script type="text/javascript">
                                $(function () {
                                    $("[id$=DropDownList26]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/Service.asmx/rcm_po")%>',
                                  data: "{ 'prefix': '" + request.term + "'}",
                                  dataType: "json",
                                  type: "POST",
                                  contentType: "application/json; charset=utf-8",
                                  success: function (data) {
                                      response($.map(data.d, function (item) {
                                          return {
                                              label: item.split('^')[0],

                                          }
                                      }))
                                  },
                                  error: function (response) {
                                      alert(response.responseText);
                                  },
                                  failure: function (response) {
                                      alert(response.responseText);
                                  }
                              });
                          },
                          select: function (e, i) {
                          },
                          minLength: 1
                      });
                  });
    </script>
              
              
              
              
              
               <asp:TextBox ID="DropDownList26" runat="server" Width="300px" Font-Names="Times New Roman" Font-Size="Small" Visible="False"></asp:TextBox>
              <br />
              <asp:Label ID="Label403" runat="server" Text="&lt;marquee&gt;Purchase/Sales/Work Order Codes will be &quot;P&quot; for Purchase, &quot;S&quot; for Sales, &quot;W&quot; for Work Order and then &quot;01&quot; for Store Material &quot;02&quot; for Raw Material &quot;04&quot; for Miscellaneous  &quot;05&quot; for Finished Goods&lt;/marquee&gt;" ForeColor="Red"></asp:Label>
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <br />
              &nbsp;<asp:Button ID="Button45" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="ADD" Width="130px" CssClass="bottomstyle" />
              <asp:Button ID="Button46" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CANCEL" Width="130px" CssClass="bottomstyle" />
              <br />
          </asp:Panel>


            
             </div> 
        </center> 
            </asp:Content>--%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
           

            
            <asp:Panel ID="Panel2" runat="server" BorderColor="#00CC00" BorderStyle="Double" Width="1000px" Font-Names="Times New Roman" style="text-align: left" CssClass="brds" Visible="False">
              <asp:Label ID="Label280" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Overline="False" Font-Size="X-Large" ForeColor="White" style="text-align: center ; line-height :40px" Text="TAX INVOICE (FOR SERVICE UNDER REVERSE CHARGE)" Height ="40px" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style="border-bottom: 10px double #008000; width: 100%;" >
 
                &nbsp;<asp:Label ID="Label299" runat="server" Text="RCM Invoice No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox177" runat="server" Width="75px"></asp:TextBox>
                   <asp:TextBox ID="TextBox65" runat="server" Width="75px"></asp:TextBox>
                   <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="PV Invoice No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox1" runat="server" Width="75px"></asp:TextBox>
                   <asp:TextBox ID="TextBox2" runat="server" Width="75px"></asp:TextBox>

                   <asp:Label ID="Label485" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="OS Invoice No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox190" runat="server" Width="75px"></asp:TextBox>
                   <asp:TextBox ID="TextBox191" runat="server" Width="75px"></asp:TextBox>
                   &nbsp;<br />
                   &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="S.O. No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox124" runat="server" BackColor="Red" ForeColor="White" Width="150px"></asp:TextBox>
                   <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. Details" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label482" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Party Type" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList33" runat="server" Width="152px">
                       <asp:ListItem>Select</asp:ListItem>
                       <asp:ListItem>I.P.T.</asp:ListItem>
                       <asp:ListItem>Other</asp:ListItem>
                   </asp:DropDownList>
                   <asp:Label ID="Label483" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. S.Code" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox188" runat="server"></asp:TextBox>
                   <asp:Label ID="Label484" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. State" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox189" runat="server"></asp:TextBox>
                   <br />
                    &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. For" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList2" runat="server" Width="152px" AutoPostBack="True">
                       <asp:ListItem>Select</asp:ListItem>
                       <asp:ListItem>RCM For Tax Invoice</asp:ListItem>
                       <asp:ListItem>RCM For Payment Voucher</asp:ListItem>
                   </asp:DropDownList>
                   <asp:Panel ID="Panel3" runat="server" Visible="False">
                         &nbsp;<asp:Label ID="Label469" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. No" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList30" runat="server" Width="152px">
                   </asp:DropDownList>
                   &nbsp;<br /> 
                   </asp:Panel>
                 <asp:Panel ID="Panel1" runat="server" Visible="False">
                       &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. No" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox178" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label472" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Inv. Date" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox179" runat="server"></asp:TextBox>
                      <asp:CalendarExtender ID="TextBox83_CalendarExtender" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox179">
                </asp:CalendarExtender>
                  
                       <br />
                       &nbsp;<asp:Label ID="Label473" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="HSN Code" Width="120px"></asp:Label>
                       <asp:DropDownList ID="DropDownList32" runat="server" Width="152px">
                       </asp:DropDownList>
                       <br />
                       &nbsp;<asp:Label ID="Label474" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Work Desc." Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox180" runat="server" Width="380px"></asp:TextBox>
                       <asp:Label ID="Label475" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Work AU." Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox181" runat="server"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label476" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Work Qty." Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox182" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label477" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Unit Rate" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox183" runat="server" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label478" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="CGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox184" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label479" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="SGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox185" runat="server" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label480" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="IGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox186" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label481" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="CESS" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox187" runat="server" Width="150px"></asp:TextBox>
                   </asp:Panel> 
                   </div> 
                <asp:Label ID="Label298" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Notification" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox64" runat="server" Cssclass="notetextboxclass" TextMode="MultiLine" Width="382px"></asp:TextBox>
               <br />
              
                <br />
                &nbsp;<asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
              &nbsp;&nbsp;<br />
                <asp:Button ID="Button1" runat="server" Text="NEW" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
                <asp:Button ID="Button37" runat="server" Text="CANCEL" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button35" runat="server" Text="ADD" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button36" runat="server" Text="SAVE" Width="75px" Font-Size="Small" CssClass="bottomstyle" Enabled="False" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
              <br />
              <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderColor="#00CC00" BorderStyle="Double" BorderWidth="5px" CellPadding="3" Font-Names="Times New Roman" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="100%">
                  <AlternatingRowStyle BackColor="#DCDCDC" />
                  <Columns>
                      <asp:BoundField DataField="GARN_NO" HeaderText="GARN No" />
                      <asp:BoundField DataField="MAT_SLNO" HeaderText="MAT. SLNO" />
                      <asp:BoundField DataField="MAT_CODE" HeaderText="MAT. CODE" />
                      <asp:BoundField DataField="MAT_NAME" HeaderText="MAT. DESC." />
                      <asp:BoundField DataField="MAT_AU" HeaderText="A / U" />
                      <asp:BoundField DataField="CHPTR_HEAD" HeaderText="HSN Code" />
                      <asp:BoundField DataField="UNIT_RATE" HeaderText="Unit Rate" />
                      <asp:BoundField DataField="MAT_RCD_QTY" HeaderText="Mat Qty" />
                      <asp:BoundField DataField="MAT_RATE" HeaderText="Taxable Value" />
                      <asp:BoundField DataField="CGST_P" HeaderText="CGST" />
                      <asp:BoundField DataField="RCM_CGST" HeaderText="CGST Amt." />
                      <asp:BoundField DataField="SGST_P" HeaderText="CGST" />
                      <asp:BoundField DataField="RCM_SGST" HeaderText="SGST Amt." />
                      <asp:BoundField DataField="IGST_P" HeaderText="IGST" />
                      <asp:BoundField DataField="RCM_IGST" HeaderText="IGST Amt" />
                      <asp:BoundField DataField="CESS_P" HeaderText="CESS" />
                      <asp:BoundField DataField="RCM_CESS" HeaderText="CESS Amt." />
                      <asp:BoundField DataField="TOTAL_VAL" HeaderText="Total value of Goods" />
                  </Columns>
                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                  <HeaderStyle BackColor="#FF0066" BorderColor="Blue" BorderStyle="Double" BorderWidth="2px" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                  <RowStyle BackColor="#EEEEEE" BorderColor="Lime" BorderStyle="Ridge" ForeColor="Black" />
                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                  <SortedDescendingHeaderStyle BackColor="#000065" />
              </asp:GridView>
             
          </asp:Panel>
           <br />


            
            <asp:Panel ID="Panel8" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" Height="146px" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label401" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="ORDER SELECTION" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label470" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Type:-" Width="80px"></asp:Label>
             
                            <asp:DropDownList ID="DropDownList31" runat="server" AutoPostBack="True" Width="300px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Unregistered Party</asp:ListItem>
                                <asp:ListItem>Registered Party</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-" Width="80px" Visible="False"></asp:Label>
             
                            <script type="text/javascript">
                                $(function () {
                                    $("[id$=DropDownList26]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/Service.asmx/rcm_po")%>',
                                  data: "{ 'prefix': '" + request.term + "'}",
                                  dataType: "json",
                                  type: "POST",
                                  contentType: "application/json; charset=utf-8",
                                  success: function (data) {
                                      response($.map(data.d, function (item) {
                                          return {
                                              label: item.split('^')[0],

                                          }
                                      }))
                                  },
                                  error: function (response) {
                                      alert(response.responseText);
                                  },
                                  failure: function (response) {
                                      alert(response.responseText);
                                  }
                              });
                          },
                          select: function (e, i) {
                          },
                          minLength: 1
                      });
                  });
    </script>
              
              
              
              
              
               <asp:TextBox ID="DropDownList26" runat="server" Width="300px" Font-Names="Times New Roman" Font-Size="Small" Visible="False"></asp:TextBox>
              <br />
              <asp:Label ID="Label403" runat="server" Text="&lt;marquee&gt;Purchase/Sales/Work Order Codes will be &quot;P&quot; for Purchase, &quot;S&quot; for Sales, &quot;W&quot; for Work Order and then &quot;01&quot; for Store Material &quot;02&quot; for Raw Material &quot;04&quot; for Miscellaneous  &quot;05&quot; for Finished Goods&lt;/marquee&gt;" ForeColor="Red"></asp:Label>
              <br />
              
              <br />
              &nbsp;<asp:Button ID="Button45" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="ADD" Width="130px" CssClass="bottomstyle" />
              <asp:Button ID="Button46" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CANCEL" Width="130px" CssClass="bottomstyle" />
              <br />
          </asp:Panel>


            
             </div> 
        </center> 
            </asp:Content>


