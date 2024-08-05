<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="s_bill.aspx.vb" Inherits="PLANT_DETAILS.s_bill1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
           <asp:Panel ID="Panel9" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" Height="146px" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label470" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="SERVICE/ACTIVITY ORDER SELECTION" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label471" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-" Width="80px"></asp:Label>
             
                            <script type="text/javascript">
                  $(function () {
                      $("[id$=DropDownList29]").autocomplete({
                          source: function (request, response) {
                              $.ajax({
                                  url: '<%=ResolveUrl("~/Service.asmx/S_SO")%>',
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
              
              
              
              
              
               <asp:TextBox ID="DropDownList29" runat="server" Width="300px" Font-Names="Times New Roman"></asp:TextBox>
              <br />
              <asp:Label ID="Label472" runat="server" Text="&lt;marquee&gt;Purchase/Sales/Work Order Codes will be &quot;P&quot; for Purchase, &quot;S&quot; for Sales, &quot;W&quot; for Work Order and then &quot;01&quot; for Store Material &quot;02&quot; for Raw Material &quot;04&quot; for Miscellaneous  &quot;05&quot; for Finished Goods&lt;/marquee&gt;" ForeColor="Red"></asp:Label>
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <br />
              &nbsp;<asp:Button ID="Button47" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="ADD" Width="130px" CssClass="bottomstyle" />
              <asp:Button ID="Button48" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CANCEL" Width="130px" CssClass="bottomstyle" />
              <br style="text-align: center" />
          </asp:Panel>
            <asp:Panel ID="Panel10" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" style="text-align: left" Visible="False" CssClass="brds" Width="1000px">
                <asp:Label ID="Label473" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="White" style="text-align: center" Text="SERVICE / ACTIVITY BILLING" Width="100%" CssClass="brds"></asp:Label>
                <br />
                 <div runat ="server" style="border-bottom: 10px double #008000; width: 100%;" >
                     <asp:Label ID="Label30" runat="server" Text="IRN No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox6" runat="server" Width="450px" ReadOnly="True"></asp:TextBox>
                   <br />
                     <asp:Label ID="Label299" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Invoice No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox177" runat="server" Width="75px"></asp:TextBox>
                   <asp:TextBox ID="TextBox65" runat="server" Width="75px"></asp:TextBox>
                <asp:Label ID="Label476" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                <br />
                <asp:Label ID="Label477" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Order No." Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox179" runat="server" BackColor="Red" ForeColor="White" Width="150px" ReadOnly="True"></asp:TextBox>
                <asp:Label ID="Label478" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Party Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox180" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="TextBox181" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                     <br />
                       <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Rcd Voucher No" Width="120px"></asp:Label>
                       <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="152px">
                       </asp:DropDownList>
                       <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Bal. Amount" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox9" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                       <br />
                       <asp:Label ID="Label468" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Rcd Voucher Date" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox176" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                <br />
                <asp:Label ID="Label479" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Notification" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox182" runat="server" Cssclass="notetextboxclass" TextMode="MultiLine" Width="382px"></asp:TextBox>
                </div>
                
                <br />
                 <div style ="float :right; width :350px; height: 188px;" runat ="server" >
                         
                         <asp:FormView ID="FormView3" runat="server" Font-Names="Times New Roman" Width="100%" Height="186px" BorderColor="Red" BorderStyle="Double" Visible="False">
                             <ItemTemplate>
                                 <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF;"> <%# Eval("ITEM_CODE")%> </h3>
                                 <br />
                                 <asp:Label ID="Label355" runat="server" Font-Bold="True" Text="Work Desc" Width="100"></asp:Label>
                                 <asp:Label ID="Label356" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label357" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                 <br />
                                 <asp:Label ID="Label358" runat="server" Font-Bold="True" Text="Work A/U" Width="100"></asp:Label>
                                 <asp:Label ID="Label359" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label360" runat="server" Text='<%# Eval("ITEM_AU")%>' Width="120"></asp:Label>
                                  <br />
                                 <asp:Label ID="Label365" runat="server" Font-Bold="True" Text="Ord. Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label366" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label367" runat="server" Text='<%# Eval("ITEM_QTY")%>' Width="120"></asp:Label>
                                 <br />
                                 <asp:Label ID="Label368" runat="server" Font-Bold="True" Text="Ord. Bal Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label369" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label370" runat="server" Text='<%# Eval("ITEM_BAL_QTY")%>' Width="120"></asp:Label>
                                 <br />
                             </ItemTemplate>
                         </asp:FormView>
                         </div> 
                <br />
                <asp:Label ID="Label480" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList30" runat="server" AutoPostBack="True" Width="150px">
                </asp:DropDownList>
                <asp:Label ID="Label481" runat="server" Font-Bold="True" ForeColor="Blue" Text="Vocab No" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox183" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="147px"></asp:TextBox>
                <br />
                <asp:Label ID="Label482" runat="server" Font-Bold="True" ForeColor="Blue" Text="Ammed No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox184" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                <asp:Label ID="Label483" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox185" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="147px"></asp:TextBox>
                <br />
                <asp:Label ID="Label484" runat="server" Font-Bold="True" ForeColor="Blue" Text="Activity Code" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList31" runat="server" AutoPostBack="True" Width="150px">
                </asp:DropDownList>
                <asp:Label ID="Label493" runat="server" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label485" runat="server" Font-Bold="True" ForeColor="Blue" Text="Activity Qty" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox186" runat="server" Width="150px"></asp:TextBox>
                <asp:Label ID="Label486" runat="server" Font-Bold="True" ForeColor="Blue" Visible="False" Width="65px"></asp:Label>
                <br />
                <asp:Label ID="Label487" runat="server" Font-Bold="True" ForeColor="Blue" Text="Activity Details" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox187" runat="server" Width="300px"></asp:TextBox>
                <br />
                <asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="Red" Text="Error Code : " Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="txtEinvoiceErrorCode" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label42" runat="server" Font-Bold="True" ForeColor="Red" Text="Error Message : " Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="txtEinvoiceErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                <br />
                <br />
                <asp:Button ID="Button49" runat="server" Text="CANCEL" Width="75px" CssClass="bottomstyle" Font-Size="Small" />
                <asp:Button ID="Button50" runat="server" Text="ADD" Width="75px" CssClass="bottomstyle" Font-Size="Small" />
                <asp:Button ID="Button51" runat="server" Text="SAVE" Width="75px" CssClass="bottomstyle" Font-Size="Small" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                <br />
                <br />
                <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" BorderColor="#00CC00" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Names="Times New Roman" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="100%">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="mat_sl_no" HeaderText="Sl No" />
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Job Code" />
                        <asp:BoundField DataField="MAT_NAME" HeaderText="Job Name" />
                        <asp:BoundField DataField="ITEM_AU" HeaderText="A/U" />
                        <asp:BoundField DataField="ITEM_QTY_PCS" HeaderText="Job Unit" />
                        <asp:BoundField DataField="ITEM_UNIT_RATE" HeaderText="Unit Price" />
                        <asp:BoundField DataField="Packing Details" HeaderText="Job Details" />
                        <asp:BoundField DataField="ASS_VALUE" HeaderText="Total Base Value" />
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
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Taxable Amount" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label496" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Red" style="text-align: left" Text="TOTAL VAL" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox195" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                <br />
                <asp:Label ID="Label488" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="CGST Amount" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox188" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="Label490" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="SGST Amount" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox190" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
                <br />
                <asp:Label ID="Label494" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" Text="IGST Amount" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox193" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                <br />
                <asp:Label ID="Label495" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" Text="CESS Amount" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox194" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
                <br />
                <asp:Label ID="Label305" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="TCS Val" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox66" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
                <br />
            </asp:Panel>
            
            
            
            
            
             </div> 
        </center> 

</asp:Content>
