<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="bank_guarantee.aspx.vb" Inherits="PLANT_DETAILS.bank_guarantee" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
    if ( window.history.replaceState ) {
        window.history.replaceState( null, null, window.location.href );
    }
</script>
    <center>
        <div runat ="server" style ="min-height :600px;">
            <div runat ="server" style ="float :right ">
                  <asp:Label ID="Label2" runat="server" Text="Date" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
            </div>
            <asp:Panel ID="rcd_Panel0" runat="server" BorderColor="Blue" BorderStyle="Groove" Width="735px" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left" CssClass="brds">
                <asp:Panel ID="Panel15" runat="server" BackColor="#4686F0" Height="54px" style="text-align: left">
                    <asp:Label ID="Label456" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height:40px" Text="BANK GUARANTEE" Height ="40px" Width="100%"></asp:Label>
                </asp:Panel>
            <br />
                <asp:Label ID="Label457" runat="server" ForeColor="Blue" Text="BG No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox61" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
            <br />
                <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="Original BG No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox4" runat="server" Width="292px"></asp:TextBox>
                <asp:Label ID="Label13" runat="server" ForeColor="Blue" Text="Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox10" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox10">
                </asp:CalendarExtender>
            <br />
                <asp:Label ID="Label460" runat="server" ForeColor="Blue" Text="Supl Code" Width="110px"></asp:Label>
                                 <script type="text/javascript">
                                     $(function () {
                                         $("[id$=DropDownList25]").autocomplete({
                                             source: function (request, response) {
                                                 $.ajax({
                                                     url: '<%=ResolveUrl("~/Service.asmx/supl_and_dater")%>',
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

                <asp:TextBox ID="DropDownList25" runat="server" Width="292px"></asp:TextBox>
                <br />
                <asp:Label ID="Label458" runat="server" ForeColor="Blue" Text="PO/WO No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox62" runat="server" Width="292px"></asp:TextBox>
                <asp:Label ID="Label459" runat="server" ForeColor="Blue" Text="Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox63" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox63_CalendarExtender" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox63">
                </asp:CalendarExtender>

                <script type="text/javascript">

                  $(function () {
                      $("[id$=TextBox62]").autocomplete({
                          source: function (request, response) {
                              $.ajax({
                                  url: '<%=ResolveUrl("~/Service.asmx/order_search")%>',
                           data: "{ 'prefix': '" + request.term + "'}",
                           dataType: "json",
                           type: "POST",
                           contentType: "application/json; charset=utf-8",
                           success: function (data) {
                               response($.map(data.d, function (item) {
                                   return {
                                       label: item.split('^')[0]
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
                <br />
                <asp:Label ID="Label6" runat="server" ForeColor="Blue" Text="IOC No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox5" runat="server" Width="292px"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox6" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox6">
                </asp:CalendarExtender>
                
                <br />
                <asp:Label ID="Label10" runat="server" ForeColor="Blue" Text="Return BG IOC No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server" Width="292px"></asp:TextBox>
                <asp:Label ID="Label11" runat="server" ForeColor="Blue" Text="Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox8" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox8">
                </asp:CalendarExtender>
                
                <br />
                <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="Type of deposit" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList28" runat="server" Width="124px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>SD</asp:ListItem>
                    <asp:ListItem>EMD</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label8" runat="server" ForeColor="Blue" Text="Type of BG" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" Width="124px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>BG</asp:ListItem>
                    <asp:ListItem>PBG</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label9" runat="server" ForeColor="Blue" Text="BG location" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server" Width="124px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>HO</asp:ListItem>
                    <asp:ListItem>SRU Bhilai</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label653" runat="server" ForeColor="Blue" Text="Issuing Bank" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox181" runat="server" Width="120px"></asp:TextBox>
                <asp:Label ID="Label12" runat="server" ForeColor="Blue" Text="Issuing Bank Branch" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox9" runat="server" Width="120px"></asp:TextBox>
                
            <br />
                <asp:Label ID="Label469" runat="server" ForeColor="Blue" Text="Amount" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="120px"></asp:TextBox>            
                <asp:Label ID="Label654" runat="server" ForeColor="Blue" Text="Validity" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox182" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox182_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox182" />
            <br />
                
                <asp:Label ID="Label544" runat="server" Font-Bold="False" ForeColor="Blue" Text="Conf. Letter No." Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox94" runat="server" Width="120px"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="Date" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox3" />
                <br />
                
                <asp:Label ID="Label14" runat="server" Font-Bold="False" ForeColor="Blue" Text="Company Conf. Letter No." Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox11" runat="server" Width="120px"></asp:TextBox>
                <asp:Label ID="Label15" runat="server" ForeColor="Blue" Text="Date" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox12" runat="server" Width="100px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox12" />
            
            <br />
                <asp:Panel ID="Panel16" runat="server" Height="53px">
                    <br />
                    
                   <asp:Button ID="Button22" runat="server" Font-Bold="True" ForeColor="Blue" Text="ADD" Width="80px" CssClass="bottomstyle" />
                   <asp:Button ID="Button23" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                   <asp:Button ID="Button24" runat="server" Font-Bold="True" ForeColor="Blue" Text="CANCEL" Width="80px" CssClass="bottomstyle" />
                </asp:Panel>
                <br />
                <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Width="100%">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Font-Names="Times New Roman" Font-Size="12px" ShowHeaderWhenEmpty="True" CssClass="mGrid" Width="175%">
                  <Columns>
                      <asp:BoundField DataField="ORIGINAL_BG_NO" HeaderText="Original BG No" />
                      <asp:BoundField DataField="ORIGINAL_BG_DATE" HeaderText="Original BG Date" />
                      <asp:BoundField DataField="PARTY_CODE" HeaderText="Party Code" />
                      <asp:BoundField DataField="PARTY_NAME" HeaderText="Party Name" />
                      <asp:BoundField DataField="ORDER_NO" HeaderText="Order No" />
                      <asp:BoundField DataField="ACTUAL_ORDER_NO" HeaderText="Actual Order No" />
                      <asp:BoundField DataField="ORDER_DATE" HeaderText="Order Date" />
                      <asp:BoundField DataField="IOC_NO" HeaderText="IOC No" />
                      <asp:BoundField DataField="IOC_DATE" HeaderText="IOC Date" />
                      <asp:BoundField DataField="RETURN_IOC_NO" HeaderText="Return BG IOC No" />
                      <asp:BoundField DataField="RETURN_IOC_DATE" HeaderText="Return BG IOC Date" />
                      <asp:BoundField DataField="DEPOSIT_TYPE" HeaderText="Deposit Type" />
                      <asp:BoundField DataField="BG_TYPE" HeaderText="BG Type" />
                      <asp:BoundField DataField="BG_LOCATION" HeaderText="BG Location" />
                      <asp:BoundField DataField="ISSUING_BANK_NAME" HeaderText="BANK Name" />
                      <asp:BoundField DataField="ISSUING_BANK_BRANCH" HeaderText="BANK Branch" />
                      <asp:BoundField DataField="BG_AMOUNT" HeaderText="BG Amount" />
                      <asp:BoundField DataField="BG_VALIDITY" HeaderText="BG Validity" />
                      <asp:BoundField DataField="CONF_LETTER_NO" HeaderText="Confirmation Letter No" />
                      <asp:BoundField DataField="CONF_LETTER_DATE" HeaderText="Confirmation Letter Date" />
                      <asp:BoundField DataField="COMPANY_CONF_LETTER_NO" HeaderText="COMPANY Confirmation Letter No" />
                      <asp:BoundField DataField="COMPANY_CONF_LETTER_DATE" HeaderText="COMPANY Confirmation Letter Date" />
                      
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
                <asp:Label ID="Label468" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label558" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Text="&lt;marquee&gt;If Once  Generated It can't be changed&lt;/marquee&gt;"></asp:Label>
            </asp:Panel>




            </div>
        </center>
</asp:Content>


