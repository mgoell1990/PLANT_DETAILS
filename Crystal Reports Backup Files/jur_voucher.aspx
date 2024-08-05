<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="jur_voucher.aspx.vb" Inherits="PLANT_DETAILS.jur_voucher" %>
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
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px;">
            <asp:Panel ID="rcd_Panel0" runat="server" BorderColor="Blue" BorderStyle="Groove" Width="735px" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left" CssClass="brds">
                <asp:Panel ID="Panel15" runat="server" BackColor="#4686F0" Height="54px" style="text-align: left">
                    <asp:Label ID="Label456" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height:40px" Text="EXTERNAL JOURNAL VOUCHER" Height ="40px" Width="100%"></asp:Label>
                </asp:Panel>
            <br />
                <asp:Label ID="Label457" runat="server" ForeColor="Blue" Text="Token No" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox61" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White"></asp:TextBox>
            <br />
                <asp:Label ID="Label458" runat="server" ForeColor="Blue" Text="Section Sl No" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox62" runat="server" Width="120px"></asp:TextBox>
                <asp:Label ID="Label459" runat="server" ForeColor="Blue" Text="Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox63" runat="server" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox63_CalendarExtender" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox63">
                </asp:CalendarExtender>
                <br />
                <asp:Label ID="Label653" runat="server" ForeColor="Blue" Text="Voucher No" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox181" runat="server" Width="120px"></asp:TextBox>
                <asp:Label ID="Label654" runat="server" ForeColor="Blue" Text="Date" Width="60px"></asp:Label>
                <asp:TextBox ID="TextBox182" runat="server" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox182_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox182" />
            <br />
                <asp:Label ID="Label469" runat="server" ForeColor="Blue" Text="Type of JV" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList28" runat="server" Width="124px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Normal</asp:ListItem>
                    <asp:ListItem>To Be Reversed</asp:ListItem>
                </asp:DropDownList>
            <br />
                <asp:Label ID="Label460" runat="server" ForeColor="Blue" Text="Supl Code" Width="100px"></asp:Label>
                                 <script type="text/javascript">
                                     $(function () {
                                         $("[id$=DropDownList25]").autocomplete({
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
                
                
                
                
                
                
                 <asp:TextBox ID="DropDownList25" runat="server" Width="292px"></asp:TextBox>
                <br />
                <asp:Label ID="Label544" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox94" runat="server" Width="292px"></asp:TextBox>
            <br />
                <asp:Panel ID="Panel16" runat="server" BackColor="#8ECD87" Height="53px">
                    <asp:Label ID="Label466" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/C Head" Width="250px"></asp:Label>
                    <asp:Label ID="Label467" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amount" Width="100px"></asp:Label>
                    &nbsp;<asp:Label ID="Label512" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amt Type" Width="80px"></asp:Label>
                    &nbsp;&nbsp;&nbsp;<br />
                     <script type="text/javascript">
                  $(function () {
                      $("[id$=DropDownList26]").autocomplete({
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


                   <asp:TextBox ID="DropDownList26" runat="server" Width="250px"></asp:TextBox>
                    <asp:TextBox ID="TextBox64" runat="server" Width="100px"></asp:TextBox>
                    <asp:DropDownList ID="catgory_DropDownList0" runat="server" Width="80px">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Dr</asp:ListItem>
                        <asp:ListItem>Cr</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="Button22" runat="server" Font-Bold="True" ForeColor="Blue" Text="ADD" Width="80px" CssClass="bottomstyle" />
                    <asp:Button ID="Button23" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" CssClass="bottomstyle" />
                    <asp:Button ID="Button24" runat="server" Font-Bold="True" ForeColor="Blue" Text="CANCEL" Width="80px" CssClass="bottomstyle" />
                </asp:Panel>
                <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="735px">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
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
                <asp:Label ID="Label468" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label558" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Text="&lt;marquee&gt;If Once  Generated It can't be changed&lt;/marquee&gt;"></asp:Label>
            </asp:Panel>




            </div>
        </center>
</asp:Content>

