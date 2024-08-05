<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="p_indent.aspx.vb" Inherits="PLANT_DETAILS.p_indent" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
            <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" Width="900px" CssClass="brds">
                <asp:Label ID="Label3" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :30px;" Text="INDENT" Width="100%" CssClass="brds" Height="30px"></asp:Label>
                <br />
                <asp:Label ID="Label4" runat="server" Text="Indent No" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label5" runat="server" Text="Indent Date" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <br />
               
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="100%" Font-Names="Times New Roman">
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                        <HeaderTemplate>
                            Material Details
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label8" runat="server" Text="Mat Code" Font-Names="Times New Roman" ForeColor="Blue" Width="80px"></asp:Label>
              <script type="text/javascript">
               $(function () {
                   $("[id$=TextBox3]").autocomplete({
                 source: function (request, response) {
                    $.ajax({
                 url: '<%=ResolveUrl("~/Service.asmx/INDENT")%>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('^')[0],
                                 val: item.split('^')[1],
                                 au: item.split('^')[2],
                                 mat_avg: item.split('^')[4],
                                 mat_stock: item.split('^')[3],
                                 mat_loca: item.split('^')[5],
                                 line_no: item.split('^')[6]
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
                 $("[id$=TextBox4]").val(i.item.au);
                 $("[id$=TextBox8]").val(i.item.mat_stock);
                 $("[id$=TextBox9]").val(i.item.mat_avg);
             },
             minLength: 1
         });
     });
    </script>
                            
                            
                            
                            
                            
                             <asp:TextBox ID="TextBox3" runat="server" Width="322px" Font-Names="Times New Roman"></asp:TextBox>
                            <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" ForeColor="Blue" Text="Unit" Width="80px"></asp:Label>
                            <asp:TextBox ID="TextBox4" runat="server" Width="120px" BackColor="Red" Enabled="False" Font-Names="Times New Roman" ForeColor="White"></asp:TextBox>
                            <div runat ="server" style ="float :right">
                              <asp:Button ID="Button1" runat="server" Text="ADD" CssClass="bottomstyle" Width="80px" />
                                <asp:Button ID="Button2" runat="server" Text="CANCEL" CssClass="bottomstyle" Width="80px"></asp:Button>
                            </div>
                            <br />
                            <asp:Label ID="Label13" runat="server" Font-Names="Times New Roman" ForeColor="Blue" Text="Cur. Stock" Width="80px"></asp:Label>
                            <asp:TextBox ID="TextBox8" runat="server" BackColor="Red" Enabled="False" Font-Names="Times New Roman" ForeColor="White" Width="120px"></asp:TextBox>
                            <asp:Label ID="Label14" runat="server" Font-Names="Times New Roman" ForeColor="Blue" Text="Cur. Price" Width="68px"></asp:Label>
                            <asp:TextBox ID="TextBox9" runat="server" BackColor="Red" Enabled="False" Font-Names="Times New Roman" ForeColor="White" Width="120px"></asp:TextBox>
                            <br />
                            <asp:Label ID="Label9" runat="server" Text="Mat Qty." Font-Names="Times New Roman" ForeColor="Blue" Width="80px"></asp:Label>
                            <asp:TextBox ID="TextBox5" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                            <asp:Label ID="Label10" runat="server" Text="Unit Price" Font-Names="Times New Roman" ForeColor="Blue" Width="70px"></asp:Label>
                            <asp:TextBox ID="TextBox6" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                            <asp:Label ID="Label11" runat="server" Text="Delivery Date" Font-Names="Times New Roman" ForeColor="Blue" Width="81px"></asp:Label>
                            <asp:TextBox ID="TextBox7" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                            
                            <cc1:CalendarExtender ID="TextBox7_CalendarExtender" runat="server" CssClass="red" Format="dd-MM-yyyy" BehaviorID="TextBox7_CalendarExtender" TargetControlID="TextBox7" />
                            
                            <br />
                            <br />
                            <asp:Panel ID="Panel12" runat="server" ScrollBars="Auto" Width="880px" BorderColor="Black" BorderStyle="Outset">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="text-align: center" Width="200%">
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNo" />
                                        <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                        <asp:BoundField DataField="MAT_NAME" HeaderText="Material Description" />
                                        <asp:BoundField DataField="MAT_AU" HeaderText="Unit" />
                                        <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                                        <asp:BoundField DataField="CON1" />
                                        <asp:BoundField DataField="CON2" />
                                        <asp:BoundField DataField="CON3" />
                                        <asp:BoundField DataField="OB" HeaderText="Order Balance" />
                                        <asp:BoundField DataField="MAT_STOCK" HeaderText="Current Stock" />
                                        <asp:BoundField DataField="LP" HeaderText="Last Pur. Price" />
                                        <asp:BoundField DataField="LPO" HeaderText="Last PO No" />
                                        <asp:BoundField DataField="LD" HeaderText="Last PO Date" />
                                        <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" />
                                        <asp:BoundField DataField="TOTAL_PRICE" HeaderText="Total Price" />
                                        <asp:BoundField DataField="D_DATE" HeaderText="Delivery Date" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                        <HeaderTemplate>
                            Suggested Tender
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label15" runat="server" Text="Supl Code"></asp:Label>
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                            <br />
                            <asp:Panel ID="Panel14" runat="server" BorderColor="Black" BorderStyle="Outset" ScrollBars="Auto" Width="880px">
                                <asp:GridView ID="GridView3" runat="server" ShowHeaderWhenEmpty="True" style="text-align: center" Width="200%">
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNo" />
                                        <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                        <asp:BoundField DataField="MAT_NAME" HeaderText="Material Description" />
                                        <asp:BoundField DataField="MAT_AU" HeaderText="Unit" />
                                        <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                                        <asp:BoundField DataField="CON1" />
                                        <asp:BoundField DataField="CON2" />
                                        <asp:BoundField DataField="CON3" />
                                        <asp:BoundField DataField="OB" HeaderText="Order Balance" />
                                        <asp:BoundField DataField="MAT_STOCK" HeaderText="Current Stock" />
                                        <asp:BoundField DataField="LP" HeaderText="Last Pur. Price" />
                                        <asp:BoundField DataField="LPO" HeaderText="Last PO No" />
                                        <asp:BoundField DataField="LD" HeaderText="Last PO Date" />
                                        <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" />
                                        <asp:BoundField DataField="TOTAL_PRICE" HeaderText="Total Price" />
                                        <asp:BoundField DataField="D_DATE" HeaderText="Delivery Date" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                            <br />
                           
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                        <HeaderTemplate>
                            Terms And Condition
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="Label6" runat="server" Text="Proprietary Item"></asp:Label>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="Label7" runat="server" Text="Proprietary Certificate Attached"></asp:Label>
                            <asp:DropDownList ID="DropDownList2" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                        <HeaderTemplate>
                            Budget Clearance
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Panel ID="Panel13" runat="server" BorderColor="Black" BorderStyle="Outset" ScrollBars="Auto" Width="880px">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="text-align: center" Width="200%">
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNo">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="S_CODE" HeaderText="Supl Code">
                                        <HeaderStyle Width="100px" />
                                        <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="S_NAME" HeaderText="Supl Name" />
                                        <asp:BoundField DataField="S_ADD" HeaderText="Supl Add" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel5">
                        <HeaderTemplate>
                            Purchase Deptt.
                        </HeaderTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            
            </asp:Panel>
        </div> 
        </center> 
</asp:Content>
