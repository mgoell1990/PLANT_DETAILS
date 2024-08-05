<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="FCM_Service.aspx.vb" Inherits="PLANT_DETAILS.FCM_Service" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
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
              <asp:Label ID="Label280" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Overline="False" Font-Size="X-Large" ForeColor="White" style="text-align: center ; line-height :40px" Text="TAX INVOICE (FOR SERVICE UNDER FORWARD CHARGE)" Height ="40px" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style="border-bottom: 10px double #008000; width: 100%;" >
                &nbsp;<asp:Label ID="Label30" runat="server" Text="IRN No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox6" runat="server" Width="450px" ReadOnly="True"></asp:TextBox>
                   <br /> 
                &nbsp;<asp:Label ID="Label299" runat="server" Text="RCM Invoice No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox177" runat="server" Width="75px" BackColor="Red" ForeColor="White"></asp:TextBox>
                   <asp:TextBox ID="TextBox65" runat="server" Width="75px" BackColor="Red" ForeColor="White"></asp:TextBox>
                   <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="PV Invoice No" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox1" runat="server" Width="75px" BackColor="Red" ForeColor="White"></asp:TextBox>
                   <asp:TextBox ID="TextBox2" runat="server" Width="75px" BackColor="Red" ForeColor="White"></asp:TextBox>

                   <asp:Label ID="Label485" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="OS Invoice No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox190" runat="server" Width="75px" BackColor="Red" ForeColor="White"></asp:TextBox>
                   <asp:TextBox ID="TextBox191" runat="server" Width="75px" BackColor="Red" ForeColor="White"></asp:TextBox>
                   &nbsp;<br />
                   &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Party Name" Width="120px"></asp:Label>
                   <script type="text/javascript">
                                $(function () {
                                    $("[id$=DropDownList26]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/Service.asmx/SUPL_DETAILS")%>',
                                                data: "{ 'prefix': '" + request.term + "'}",
                                                dataType: "json",
                                                type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                success: function (data) {
                                                    response($.map(data.d, function (item) {
                                                        return {
                                                            label: item.split('^')[0] +" , "+ item.split('^')[1],
                                                            SUPL_CODE: item.split('^')[0],
                                                            SUPL_NAME: item.split('^')[1],
                                                            SUPL_CONTACT_PERSON: item.split('^')[2],
                                                            SUPL_AT: item.split('^')[3],
                                                            SUPL_PO: item.split('^')[4],
                                                            SUPL_DIST: item.split('^')[5],
                                                            SUPL_PIN: item.split('^')[6],
                                                            SUPL_STATE: item.split('^')[7],
                                                            SUPL_COUNTRY: item.split('^')[8],
                                                            SUPL_MOB1: item.split('^')[9],
                                                            SUPL_MOB2: item.split('^')[10],
                                                            SUPL_LAND: item.split('^')[11],
                                                            SUPL_FAX: item.split('^')[12],
                                                            SUPL_EMAIL: item.split('^')[13],
                                                            SUPL_WEB: item.split('^')[14],
                                                            SUPL_PAN: item.split('^')[15],
                                                            SUPL_TIN: item.split('^')[16],
                                                            SUPL_ST_NO: item.split('^')[17],
                                                            SUPL_BANK: item.split('^')[18],
                                                            SUPL_ACOUNT_NO: item.split('^')[19],
                                                            SUPL_IFSC: item.split('^')[20],
                                                            SUPL_TYPE: item.split('^')[21],
                                                            SUPL_GST_NO: item.split('^')[24],
                                                            SUPL_STATE_CODE: item.split('^')[25],
                                                            PARTY_TYPE: item.split('^')[26]
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
                                            $("[id$=TextBox125]").val(i.item.SUPL_CODE);
                                            $("[id$=TextBox126]").val(i.item.SUPL_NAME);
                                            $("[id$=TextBox188]").val(i.item.SUPL_STATE_CODE);
                                            $("[id$=TextBox189]").val(i.item.SUPL_STATE);
                                            if (i.item.SUPL_STATE_CODE == 22) {
                                                $("[id$=TextBox184]").val("9");
                                                $("[id$=TextBox185]").val("9");
                                                $("[id$=TextBox186]").val("0");
                                            } else {
                                                $("[id$=TextBox184]").val("0");
                                                $("[id$=TextBox185]").val("0");
                                                $("[id$=TextBox186]").val("18");
                                            }

                                        },
                                        minLength: 1
                                    });
                                });
    </script>

                   <script type="text/javascript">
                $(function () {
                    $("[id$=TextBox85]").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: '<%=ResolveUrl("~/Service.asmx/SUPL_DETAILS")%>',
                                  data: "{ 'prefix': '" + request.term + "'}",
                                  dataType: "json",
                                  type: "POST",
                                  contentType: "application/json; charset=utf-8",
                                  success: function (data) {
                                      response($.map(data.d, function (item) {
                                          return {
                                              label: item.split('^')[1],
                                              SUPL_NAME: item.split('^')[0],
                                              SUPL_CONTACT_PERSON: item.split('^')[2],
                                              SUPL_AT: item.split('^')[3],
                                              SUPL_PO: item.split('^')[4],
                                              SUPL_DIST: item.split('^')[5],
                                              SUPL_PIN: item.split('^')[6],
                                              SUPL_STATE: item.split('^')[7],
                                              SUPL_COUNTRY: item.split('^')[8],
                                              SUPL_MOB1: item.split('^')[9],
                                              SUPL_MOB2: item.split('^')[10],
                                              SUPL_LAND: item.split('^')[11],
                                              SUPL_FAX: item.split('^')[12],
                                              SUPL_EMAIL: item.split('^')[13],
                                              SUPL_WEB: item.split('^')[14],
                                              SUPL_PAN: item.split('^')[15],
                                              SUPL_TIN: item.split('^')[16],
                                              SUPL_ST_NO: item.split('^')[17],
                                              SUPL_BANK: item.split('^')[18],
                                              SUPL_ACOUNT_NO: item.split('^')[19],
                                              SUPL_IFSC: item.split('^')[20],
                                              SUPL_TYPE: item.split('^')[21],
                                              SUPL_GST_NO: item.split('^')[24],
                                              SUPL_STATE_CODE: item.split('^')[25],
                                              PARTY_TYPE: item.split('^')[26]

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
                                      $("[id$=TextBox84]").val(i.item.SUPL_NAME);
                                      $("[id$=TextBox86]").val(i.item.SUPL_CONTACT_PERSON);
                                      $("[id$=SUPLDropDownList17]").val(i.item.SUPL_TYPE);
                                      $("[id$=TextBox99]").val(i.item.SUPL_AT);
                                      $("[id$=TextBox100]").val(i.item.SUPL_PO);
                                      $("[id$=TextBox101]").val(i.item.SUPL_DIST);
                                      $("[id$=TextBox102]").val(i.item.SUPL_PIN);
                                      $("[id$=DropDownList1]").val(i.item.SUPL_STATE);
                                      $("[id$=TextBox104]").val(i.item.SUPL_COUNTRY);
                                      $("[id$=TextBox93]").val(i.item.SUPL_MOB1);
                                      $("[id$=TextBox94]").val(i.item.SUPL_MOB2);
                                      $("[id$=TextBox95]").val(i.item.SUPL_LAND);
                                      $("[id$=TextBox105]").val(i.item.SUPL_FAX);
                                      $("[id$=TextBox106]").val(i.item.SUPL_EMAIL);
                                      $("[id$=TextBox107]").val(i.item.SUPL_WEB);
                                      $("[id$=TextBox108]").val(i.item.SUPL_BANK);
                                      $("[id$=TextBox109]").val(i.item.SUPL_ACOUNT_NO);
                                      $("[id$=TextBox110]").val(i.item.SUPL_IFSC);
                                      $("[id$=TextBox111]").val(i.item.SUPL_PAN);
                                      $("[id$=TextBox112]").val(i.item.SUPL_GST_NO);
                                      $("[id$=TextBox113]").val(i.item.SUPL_STATE_CODE);
                                      $("[id$=DropDownList2]").val(i.item.PARTY_TYPE);

                                  },
                                  minLength: 1
                              });
                          });
    </script>
                   <asp:TextBox ID="DropDownList26" runat="server" Width="310px" Font-Names="Times New Roman" Font-Size="Small"></asp:TextBox>
                   <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. Code" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" Width="50px"></asp:TextBox>
                   <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. Name" Width="85px"></asp:Label>
                   <asp:TextBox ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" Width="300px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label483" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. S.Code" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox188" runat="server" Width="70px"></asp:TextBox>
                   <asp:Label ID="Label484" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Suppl. State" Width="85px"></asp:Label>
                   <asp:TextBox ID="TextBox189" runat="server"></asp:TextBox>
                   <br />
                   
                 <asp:Panel ID="Panel1" runat="server" Visible="False">
                       
                       &nbsp;<asp:Label ID="Label473" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="HSN Code" Width="120px"></asp:Label>
                       <asp:DropDownList ID="DropDownList32" runat="server" Width="161px">
                       </asp:DropDownList>
                       <br />
                       &nbsp;<asp:Label ID="Label482" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="INV. Type" Width="120px"></asp:Label>
                       <asp:DropDownList ID="DropDownList33" runat="server" Width="152px">
                           <asp:ListItem>Select</asp:ListItem>
                           <asp:ListItem>B2C</asp:ListItem>
                       </asp:DropDownList>
                       <br />
                       &nbsp;<asp:Label ID="Label476" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Work Qty." Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox182" runat="server" Width="157px"></asp:TextBox>
                       <asp:Label ID="Label477" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Unit Rate" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox183" runat="server" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label478" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="CGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox184" runat="server" Width="157px"></asp:TextBox>
                       <asp:Label ID="Label479" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="SGST" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox185" runat="server" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label480" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="IGST" Width="120px"></asp:Label>
                       <asp:TextBox ID="TextBox186" runat="server" Width="157px"></asp:TextBox>
                       <asp:Label ID="Label481" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="CESS" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox187" runat="server" Width="150px">0</asp:TextBox>
                     
                   </asp:Panel> 
                   <br/>
                   </div> 
                
              
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

                      <asp:BoundField DataField="PartyCode" HeaderText="Party Code" />
                      <asp:BoundField DataField="PartyName" HeaderText="Party Name" />
                      <asp:BoundField DataField="SACCode" HeaderText="SAC Code" />
                      <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Rate" />
                      <asp:BoundField DataField="work_qty" HeaderText="Work Qty" />
                      <asp:BoundField DataField="prov_amt" HeaderText="Taxable Value" />
                      <asp:BoundField DataField="CGST" HeaderText="CGST" />
                      <asp:BoundField DataField="cgst_liab" HeaderText="CGST Amt." />
                      <asp:BoundField DataField="SGST" HeaderText="SGST" />
                      <asp:BoundField DataField="sgst_liab" HeaderText="SGST Amt." />
                      <asp:BoundField DataField="IGST" HeaderText="IGST" />
                      <asp:BoundField DataField="igst_liab" HeaderText="IGST Amt" />
                      <asp:BoundField DataField="CESS" HeaderText="CESS" />
                      <asp:BoundField DataField="cess_liab" HeaderText="CESS Amt." />
                      <asp:BoundField DataField="TOTAL_VAL" HeaderText="Total value of Goods" />
                      
                    
                  </Columns>
                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                  <HeaderStyle BackColor="#FF0066" BorderColor="Gray" BorderWidth="1px" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                  <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                  <SortedDescendingHeaderStyle BackColor="#000065" />
              </asp:GridView>
             
          </asp:Panel>
           <br />


            
            <asp:Panel ID="Panel8" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" Height="146px" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label401" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="PARTY TYPE" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label470" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Type:-" Width="80px"></asp:Label>
             
                            <asp:DropDownList ID="DropDownList31" runat="server" AutoPostBack="True" Width="300px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Unregistered Party</asp:ListItem>
                                <%--<asp:ListItem>Registered Party</asp:ListItem>--%>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-" Width="80px" Visible="False"></asp:Label>
             
                            
              
              
              
              
              
               
                
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



