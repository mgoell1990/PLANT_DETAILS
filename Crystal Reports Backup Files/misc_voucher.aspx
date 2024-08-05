<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="misc_voucher.aspx.vb" Inherits="PLANT_DETAILS.misc_voucher" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
    <link href="Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px;">

            <asp:Panel ID="pay_panel" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left" Width="805px" CssClass="brds">
                      <asp:Panel ID="Panel39" runat="server" BackColor="#4686F0" Height="54px" style="text-align: center" CssClass="brds">
                          <asp:Label ID="Label625" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :40px" Height ="40px" Text="PAYMENT VOUCHER CUM BILL ADJUSTMENT"></asp:Label>
                      </asp:Panel>
                      <br />
                 <div runat ="server" style ="float :right ">
                      <asp:Button ID="PAY_Button36" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" CssClass="bottomstyle" />    
                      <br />
                      <asp:Button ID="Button60" runat="server" Text="NEW " Width="80px" CssClass="bottomstyle" ForeColor="Blue" />
                     <br />
                     <br />
                     <br />
                      <br />
                     <br />
                     <br />
                     <asp:LinkButton ID="PAY_LinkButton1" runat="server" ForeColor="Red">ADD JOURNAL</asp:LinkButton>
                 </div>
                      <asp:Label ID="Label626" runat="server" ForeColor="Blue" Text="Token No" Width="100px"></asp:Label>
                      <asp:TextBox ID="pay_vou_TextBox76" runat="server" BackColor="#4686F0" ForeColor="White" Width="120px"></asp:TextBox>
                      <asp:Label ID="Label627" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                      <br />
                      <asp:Label ID="Label628" runat="server" ForeColor="Blue" Text="Section Sl No" Width="100px"></asp:Label>
                      <asp:TextBox ID="pay_sec_slno_TextBox77" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="Label629" runat="server" ForeColor="Blue" Text="Date" Width="100px"></asp:Label>
                      <asp:TextBox ID="pay_date_TextBox78" runat="server" Width="85px"></asp:TextBox>
                      <asp:CalendarExtender ID="pay_date_TextBox78_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="pay_date_TextBox78" />
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 
                  
                      <br />
                      <asp:Label ID="Label647" runat="server" ForeColor="Blue" Text="Voucher No" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox179" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="Label648" runat="server" ForeColor="Blue" Text="Voucher Date" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox180" runat="server" Width="85px"></asp:TextBox>
                      <asp:CalendarExtender ID="TextBox180_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox180_CalendarExtender" TargetControlID="TextBox180" />
                      <br />
                      <asp:Label ID="Label630" runat="server" Font-Bold="False" ForeColor="Blue" Text="Voucher Type" Width="100px"></asp:Label>
                      <asp:DropDownList ID="pay_vouch_type_DropDownList36" runat="server" Width="122px">
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>B.P.V</asp:ListItem>
                          <asp:ListItem>C.B.V</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label631" runat="server" Font-Bold="False" ForeColor="Blue" Text="Pay Mode" Width="100px"></asp:Label>
                      <asp:DropDownList ID="pay_mode_DropDownList37" runat="server" Width="87px" AutoPostBack="True">
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>Advance</asp:ListItem>
                          <asp:ListItem>Through Liab.</asp:ListItem>
                          <asp:ListItem>Direct Exp.</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:Label ID="Label632" runat="server" ForeColor="Blue" Text="P.O./WO No" Width="100px"></asp:Label>
                      <asp:TextBox ID="pay_po_wo_TextBox96" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="Label633" runat="server" ForeColor="Blue" Text="GARN /MB No" Width="100px"></asp:Label>
                      <asp:TextBox ID="pay_garn_TextBox97" runat="server" Width="85px"></asp:TextBox>
                      <br />
                      <asp:Label ID="Label634" runat="server" ForeColor="Blue" Text="Inv No." Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox177" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="Label635" runat="server" ForeColor="Blue" Text="Inv. Date" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox178" runat="server" Width="85px"></asp:TextBox>
                      <asp:CalendarExtender ID="TextBox178_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox178_CalendarExtender" TargetControlID="TextBox178" />
                      <br />
                     
                      <asp:Label ID="Label637" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration" Width="100px"></asp:Label>
                      <asp:TextBox ID="pay_narration_TextBox81" runat="server" Width="314px"></asp:TextBox>
                      <br />
                      <asp:Label ID="Label651" runat="server" ForeColor="Blue" Text="Paid To" Width="100px"></asp:Label>
                      <script type="text/javascript">
                          $(function () {
                              $("[id$=pay_paid_TextBox82]").autocomplete({
                                  source: function (request, response) {
                                      $.ajax({
                                          url: '<%=ResolveUrl("~/Service.asmx/supl")%>',
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
                 
                  <asp:TextBox ID="pay_paid_TextBox82" runat="server" Width="314px"></asp:TextBox>
                      <br />
                      <asp:Label ID="Label652" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="DETAILS" Width="100%"></asp:Label>
                      <br />
                  <asp:Label ID="Label636" runat="server" ForeColor="Blue" Text="Supl Code" Width="100px"></asp:Label>
                 <br />
                                                 <script type="text/javascript">
                                                     $(function () {
                                                         $("[id$=pay_supl_code_TextBox98]").autocomplete({
                                                             source: function (request, response) {
                                                                 $.ajax({
                                                                     url: '<%=ResolveUrl("~/Service.asmx/supl")%>',
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
                  <asp:TextBox ID="pay_supl_code_TextBox98" runat="server" Width="300px"></asp:TextBox>
                      <br />
                <asp:Panel ID="PAY_Panel22" runat="server" BackColor="#8ECD87">
                          <asp:Label ID="Label639" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3399" style="text-align: center" Text="PAYMENT ENTRYS" Width="806px"></asp:Label>
                          <br />
                          <div runat ="server" style ="float :right ;">    
                     <asp:Button ID="PAY_Button35" runat="server" Font-Bold="True" ForeColor="Blue" Text="ADD" Width="80px" CssClass="bottomstyle" />
                          <asp:Button ID="PAY_Button37" runat="server" Font-Bold="True" ForeColor="Blue" Text="CANCEL" Width="80px" CssClass="bottomstyle" />
                          </div> 
                          <asp:Label ID="Label640" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/C Head" Width="250px"></asp:Label>
                          <asp:Label ID="Label641" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amount" Width="100px"></asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;<br />
                  
                            <script type="text/javascript">
                                                      $(function () {
                                  $("[id$=pay_ac_head_TextBox95]").autocomplete({
                                      source: function (request, response) {
                                          $.ajax({
                                              url: '<%=ResolveUrl("~/Service.asmx/ac_head")%>',
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
                   
                  
                          <asp:TextBox ID="pay_ac_head_TextBox95" runat="server" Width="250px"></asp:TextBox>
                          <asp:TextBox ID="pay_amount_TextBox89" runat="server" Width="100px"></asp:TextBox>
                     <br />
                     <br />
                    <asp:GridView ID="pay_GridView8" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="806px">
                              <AlternatingRowStyle BackColor="#DCDCDC" />
                              <Columns>
                                  <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL CODE" />
                                  <asp:BoundField DataField="AC_HEAD" HeaderText="A/C HEAD">
                                  <ItemStyle Width="150px" />
                                  </asp:BoundField>
                                  <asp:BoundField HeaderText="A/C_DESCRIPTION" DataField="AC_DESC" />
                                  <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT">
                                  <ItemStyle Width="150px" />
                                  </asp:BoundField>
                              </Columns>
                              <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                              <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                              <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                              <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                              <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                              <SortedAscendingCellStyle BackColor="#F1F1F1" />
                              <SortedAscendingHeaderStyle BackColor="#0000A9" />
                              <SortedDescendingCellStyle BackColor="#CAC9C9" />
                              <SortedDescendingHeaderStyle BackColor="#000065" />
                          </asp:GridView>
                      </asp:Panel>
                      <asp:Panel ID="JNL_Panel23" runat="server" BackColor="#8ECD87" Visible="False">
                          <asp:Label ID="Label642" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3399" style="text-align: center" Text="JOURNAL ENTRYS" Width="806px"></asp:Label>
                          <br />
                          <asp:Label ID="Label643" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/C Head" Width="250px"></asp:Label>
                          <asp:Label ID="Label644" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amount" Width="100px"></asp:Label>
                          &nbsp;<asp:Label ID="Label645" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amt Type" Width="80px"></asp:Label>
                          &nbsp;&nbsp;&nbsp;<br />
                          <script type="text/javascript">
                                       $(function () {
                                  $("[id$=jpay_ac_DropDownList38]").autocomplete({
                                      source: function (request, response) {
                                          $.ajax({
                                              url: '<%=ResolveUrl("~/Service.asmx/ac_head")%>',
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
                          <asp:TextBox ID="jpay_ac_DropDownList38" runat="server" Width="300px"></asp:TextBox>
                          <asp:TextBox ID="jpay_amount_TextBox95" runat="server" Width="100px"></asp:TextBox>
                          <asp:DropDownList ID="jpay_catgory_DropDownList1" runat="server" Width="80px">
                              <asp:ListItem>Select</asp:ListItem>
                              <asp:ListItem>Dr</asp:ListItem>
                              <asp:ListItem>Cr</asp:ListItem>
                          </asp:DropDownList>
                          <div runat ="server" style ="float :right;">
                          <asp:Button ID="JNL_Button38" runat="server" Font-Bold="True" ForeColor="Blue" Text="ADD" Width="80px" CssClass="bottomstyle" />
                          <asp:Button ID="JNL_Button39" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" CssClass="bottomstyle" />
                          <asp:Button ID="JNL_Button40" runat="server" Font-Bold="True" ForeColor="Blue" Text="CANCEL" Width="80px" CssClass="bottomstyle" />
                         </div>
                               <asp:GridView ID="jpay_GridView9" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="806px">
                              <AlternatingRowStyle BackColor="#DCDCDC" />
                              <Columns>
                                  <asp:BoundField DataField="AC_HEAD" HeaderText="AC HEAD">
                                  <ItemStyle Width="150px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="AC_DESC" HeaderText="A/C DESCRIPTION" />
                                  <asp:BoundField DataField="AMOUNT_DR" HeaderText="DEBIT AMOUNT">
                                  <HeaderStyle Width="150px" />
                                  <ItemStyle Width="150px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="AMOUNT_CR" HeaderText="CREDIT AMOUNT">
                                  <HeaderStyle Width="150px" />
                                  <ItemStyle Width="150px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL ID" />
                              </Columns>
                              <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                              <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
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
                   <asp:Label ID="Label638" runat="server" ForeColor="Red"></asp:Label>
                      <br />
                      <asp:Label ID="Label646" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Text="&lt;marquee&gt;If Once Generated It can't be changed&lt;/marquee&gt;"></asp:Label>
                      <br />
                  </asp:Panel>



            </div>
        </center>
</asp:Content>
