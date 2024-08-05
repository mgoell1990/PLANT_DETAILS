<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true" CodeBehind="RCD_Voucher_Forefeit.aspx.vb" Inherits="PLANT_DETAILS.RCD_Voucher_Forefeit" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
           
            <div runat ="server" style ="float :right ">
                <asp:Label ID="Label5" runat="server" Text="Date"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                 <asp:CalendarExtender ID="TextBox32_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox3" />
            </div>

            <br />
            <br />
            <asp:Panel ID="Panel2" runat="server" BorderColor="#00CC00" BorderStyle="Double" Width="1000px" Font-Names="Times New Roman" style="text-align: left" CssClass="brds" Visible="False">
              <asp:Label ID="Label280" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Overline="False" Font-Size="X-Large" ForeColor="White" style="text-align: center ; line-height :40px" Text="RCD Voucher Forefeit" Height ="40px" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style="width: 100%;" >
                   &nbsp;<asp:Label ID="Label30" runat="server" Text="IRN No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox6" runat="server" Width="450px" ReadOnly="True"></asp:TextBox>
                   <br /> 
                &nbsp;<asp:Label ID="Label485" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="OS Invoice No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox190" runat="server" Width="75px"></asp:TextBox>
                   <asp:TextBox ID="TextBox191" runat="server" Width="75px"></asp:TextBox>
                   &nbsp;<br />
                   &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Voucher No." Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox124" runat="server" BackColor="Red" ForeColor="White" Width="150px"></asp:TextBox>
                   <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. Details" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label482" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Party Type" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList33" runat="server" Width="152px">
                       <asp:ListItem>Select</asp:ListItem>
                       <asp:ListItem>Other</asp:ListItem>
                   </asp:DropDownList>
                    &nbsp;<asp:Label ID="Label483" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. S.Code" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox188" runat="server"></asp:TextBox>
                   <asp:Label ID="Label484" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. State" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox189" runat="server"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. For" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList2" runat="server" Width="152px" AutoPostBack="True">
                       <asp:ListItem>Select</asp:ListItem>
                       <asp:ListItem>Payment Voucher</asp:ListItem>
                   </asp:DropDownList>
                   <asp:Panel ID="Panel3" runat="server" Visible="False">
                         &nbsp;<asp:Label ID="Label469" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="RCD Voucher No" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList30" runat="server" Width="152px">
                   </asp:DropDownList>
                   
                         <br />
                         &nbsp;<asp:Label ID="Label486" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Amount(Rs.)" Width="120px"></asp:Label>
                         <asp:TextBox ID="TextBox192" runat="server"></asp:TextBox>
                         <br />
                         &nbsp;<asp:Label ID="Label487" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="CGST(%)" Width="120px"></asp:Label>
                         <asp:TextBox ID="TextBox193" runat="server"></asp:TextBox>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="SGST(%)" Width="100px"></asp:Label>
                   
                         <asp:TextBox ID="TextBox194" runat="server"></asp:TextBox>
                         <br />
                         &nbsp;<asp:Label ID="Label488" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="IGST(%)" Width="120px"></asp:Label>
                         <asp:TextBox ID="TextBox195" runat="server"></asp:TextBox>
                   
                   </asp:Panel>
                  
                   </div> 
                <asp:Label ID="Label298" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Notification" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox64" runat="server" Cssclass="notetextboxclass" TextMode="MultiLine" Width="382px"></asp:TextBox>
               <br />
              
                <br />
              &nbsp;<asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
               <br />
                &nbsp;<asp:Label ID="Label31" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="txtEinvoiceErrorCode" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label42" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="txtEinvoiceErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
              
              &nbsp;&nbsp;<br />
                <asp:Button ID="Button1" runat="server" Text="NEW" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
                <asp:Button ID="Button37" runat="server" Text="CANCEL" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button35" runat="server" Text="ADD" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button36" runat="server" Text="SAVE" Width="75px" Font-Size="Small" CssClass="bottomstyle" Enabled="False" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
              <br />
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="3" Font-Names="Times New Roman" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="100%">
                  <AlternatingRowStyle BackColor="#DCDCDC" />
                  <Columns>
                      
                      <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat. SLNO" />
                      <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                      <asp:BoundField DataField="MAT_AU" HeaderText="A / U" />
                      <asp:BoundField DataField="sac_code" HeaderText="SAC Code" />
                      <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Rate" />
                      <asp:BoundField DataField="MAT_CHALAN_QTY" HeaderText="Quantity" />
                      <asp:BoundField DataField="prov_amt" HeaderText="Taxable Amount" />
                      <asp:BoundField DataField="CGST" HeaderText="CGST" />
                      <asp:BoundField DataField="cgst_liab" HeaderText="CGST Amt." />
                      <asp:BoundField DataField="SGST" HeaderText="SGST" />
                      <asp:BoundField DataField="sgst_liab" HeaderText="SGST Amt." />
                      <asp:BoundField DataField="IGST" HeaderText="IGST" />
                      <asp:BoundField DataField="igst_liab" HeaderText="IGST Amt" />
                      <asp:BoundField DataField="CESS" HeaderText="CESS" />
                      <asp:BoundField DataField="cess_liab" HeaderText="CESS Amt." />
                      <asp:TemplateField HeaderText="Total value of Goods"></asp:TemplateField>
                  </Columns>
                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                  <HeaderStyle BackColor="#FF0066" BorderColor="Gray" BorderWidth="1px" Font-Bold="True" ForeColor="White" />
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
            
                <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Voucher No:-" Width="80px"></asp:Label>
             
                            <script type="text/javascript">
                    $(function () {
                        $("[id$=DropDownList26]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/Service.asmx/GetRCDVoucherList")%>',
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
              
              
              
              
              
               <asp:TextBox ID="DropDownList26" runat="server" Width="300px" Font-Names="Times New Roman" Font-Size="Small"></asp:TextBox>
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


