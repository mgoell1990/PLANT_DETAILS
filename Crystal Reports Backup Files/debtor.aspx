<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"    MasterPageFile="~/Site.Master" CodeBehind="debtor.aspx.vb" Inherits="PLANT_DETAILS.debtor" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; line-height: 26px;"  >
           
            <asp:Panel ID="Panel16" runat="server" BorderColor="#FF3399" BorderStyle="Groove" BorderWidth="5px" Font-Size="X-Small" Width="700px" Font-Names="Times New Roman">
         <div id="fastdivision1" aria-orientation="vertical" aria-selected="undefined" style=" width: 700px;  height: 143px; ">
             <div style="border-right: 5px groove #FF0066; float:left; text-align: left; width: 535px; height: 142px;">
                 <asp:Label ID="Label365" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#CC0000" Text="DEBTORS" style="text-align: center" Width="100%"></asp:Label>
                 <br />
                 &nbsp;<asp:Label ID="Label366" runat="server" Font-Size="Small" ForeColor="Blue" Text="Customer Code" Width="100px"></asp:Label>
                                  <script type="text/javascript">
                                      $(function () {
                                          $("[id$=TextBox124]").autocomplete({
                                              source: function (request, response) {
                                                  $.ajax({
                                                      url: '<%=ResolveUrl("~/Service.asmx/DEB_DETAILS")%>',
                                  data: "{ 'prefix': '" + request.term + "'}",
                                  dataType: "json",
                                  type: "POST",
                                  contentType: "application/json; charset=utf-8",
                                  success: function (data) {
                                      response($.map(data.d, function (item) {
                                          return {
                                              label: item.split('^')[0],
                                              d_name: item.split('^')[1],
                                              add_1: item.split('^')[2],
                                              add_2: item.split('^')[3],
                                              d_range: item.split('^')[5],
                                              d_division: item.split('^')[6],
                                              d_coll: item.split('^')[7],
                                              ecc_no: item.split('^')[8],
                                              tin_no: item.split('^')[9],
                                              stock_ac_head: item.split('^')[10],
                                              iuca_head: item.split('^')[11],
                                              supl_loc: item.split('^')[12],
                                              JOB_WORK: item.split('^')[13],
                                              deb_loc: item.split('^')[14],
                                              data_clr: item.split('^')[15]
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
                                      $("[id$=TextBox125]").val(i.item.d_name);
                                      $("[id$=TextBox127]").val(i.item.add_1);
                                      $("[id$=TextBox129]").val(i.item.add_2);
                                      $("[id$=TextBox147]").val(i.item.d_range);
                                      $("[id$=TextBox148]").val(i.item.d_division);
                                      $("[id$=TextBox142]").val(i.item.d_coll);
                                      $("[id$=TextBox144]").val(i.item.ecc_no);
                                      $("[id$=TextBox143]").val(i.item.tin_no);
                                      $("[id$=TextBox146]").val(i.item.stock_ac_head);
                                      $("[id$=TextBox145]").val(i.item.iuca_head);
                                      $("[id$=DropDownList1]").val(i.item.supl_loc);
                                      $("[id$=TextBox149]").val(i.item.JOB_WORK);
                                      $("[id$=SUPLDropDownList18]").val(i.item.deb_loc);
                                      $("[id$=ERR_LABLE0]").val(i.item.d_name);
                                  },
                                  minLength: 1
                              });
                          });
    </script>
                  <asp:TextBox ID="TextBox124" runat="server" Font-Size="Small" Width="125px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label367" runat="server" Font-Size="Small" ForeColor="Blue" Text="Company Name" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox125" runat="server" Font-Size="Small" Width="200px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label369" runat="server" Font-Size="Small" ForeColor="Blue" Text="Customer Loc." Width="100px"></asp:Label>
                 <asp:DropDownList ID="SUPLDropDownList18" runat="server" Font-Names="Times New Roman" Font-Size="Small" Width="125px">
                     <asp:ListItem>Select</asp:ListItem>
                     <asp:ListItem>Within  State</asp:ListItem>
                     <asp:ListItem>Out Side State</asp:ListItem>
                 </asp:DropDownList>
                 &nbsp;<asp:Label ID="Label397" runat="server" Font-Size="Small" ForeColor="Blue" Text="Customer Type" Width="95px"></asp:Label>
                 <asp:DropDownList ID="DropDownList1" runat="server" Width="125px" Font-Names="Times New Roman" Font-Size="Small">
                     <asp:ListItem>Select</asp:ListItem>
                     <asp:ListItem>IPT</asp:ListItem>
                     <asp:ListItem>OTHER</asp:ListItem>
                 </asp:DropDownList>
             </div>
             <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="float:left; border-bottom-width: medium;margin-left:0px; margin-top :0px; width: 155px; font-family: 'Times New Roman', Times, serif; font-size: small; height: 139px; line-height: 25px; text-align: center;">
         <br />
         <br />
                 <asp:Button ID="Button49" runat="server" Font-Size="X-Small" ForeColor="Blue" Text="SAVE" Width="100px" CssClass="bottomstyle" />
         <br />
                 <asp:Button ID="Button50" runat="server" Font-Size="X-Small" ForeColor="Blue" Text="CANCEL" Width="100px" CssClass="bottomstyle" />
             </div>
         </div>
         <asp:Panel ID="Panel17" runat="server" BackColor="#4686F0" ForeColor="White" Height="20px">
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label ID="Label370" runat="server" Font-Bold="True" Font-Size="Medium" Text="ADDRESS DETAILS"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="Label372" runat="server" Font-Bold="True" Font-Size="Medium" Text="ACC. HEAD DETAILS"></asp:Label>
         </asp:Panel>
         <div id="SECONDdivision0" aria-orientation="vertical" aria-selected="undefined" style=" width: 700px;  height: 247px; ">
             <div style="border-right: 5px groove #FF0066; border-bottom: medium double #FF0066; float:left; text-align: left; width: 435px; height: 244px; font-size: small;">

                 &nbsp;
                 <asp:Label ID="Label373" runat="server" ForeColor="Blue" Text="Add 1" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox127" runat="server" Width="300px"></asp:TextBox>
                 &nbsp;<br />&nbsp;&nbsp;<asp:Label ID="Label375" runat="server" ForeColor="Blue" Text="Add 2" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox129" runat="server" Width="300px"></asp:TextBox>
                 &nbsp;&nbsp;<br /> &nbsp;
                 <asp:Label ID="Label395" runat="server" ForeColor="Blue" Text="Range" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox147" runat="server" Width="300px"></asp:TextBox>
                 <br />
                 &nbsp;
                 <asp:Label ID="Label396" runat="server" ForeColor="Blue" Text="Division" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox148" runat="server" Width="300px"></asp:TextBox>
                 <br />
                 &nbsp;&nbsp;<asp:Label ID="Label382" runat="server" ForeColor="Blue" Text="Email Id" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox136" runat="server" Width="300px"></asp:TextBox>
                 <br />
                 &nbsp;&nbsp;
                 <br />
                 <br />
                 &nbsp;
                 <asp:Label ID="ERR_LABLE0" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
             </div>
             <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="border-bottom: medium double #FF0066; float:left; margin-left:0px; margin-top :0px; width: 255px; font-family: 'Times New Roman', Times, serif; font-size: small; height: 243px; text-align: left;">
                 &nbsp;<asp:Label ID="Label399" runat="server" ForeColor="Blue" Text="IUCA Code" Width="80px"></asp:Label>
                 <script type="text/javascript">

                     $(function () {
                         $("[id$=TextBox145]").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: '<%=ResolveUrl("~/Service.asmx/ac_head_new")%>',
                                      data: "{ 'prefix': '" + request.term + "'}",
                                      dataType: "json",
                                      type: "POST",
                                      contentType: "application/json; charset=utf-8",
                                      success: function (data) {
                                          response($.map(data.d, function (item) {
                                              return {
                                                  label: item.split('^')[0],
                                                  AC_NAME: item.split('^')[1],
                                                  AC_TYPE: item.split('^')[2],
                                                  AC_GROUP: item.split('^')[3]
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
                                 $("[id$=TextBox145]").val(i.item.label);
                              },
                              minLength: 1
                          });
                      });
    </script>
                 <asp:TextBox ID="TextBox145" runat="server"  Width="130px"></asp:TextBox>

         <br />
                 &nbsp;<asp:Label ID="Label394" runat="server" ForeColor="Blue" Text="S.T. Code" Width="80px"></asp:Label>
                 <script type="text/javascript">

                     $(function () {
                         $("[id$=TextBox146]").autocomplete({
                             source: function (request, response) {
                                 $.ajax({
                                     url: '<%=ResolveUrl("~/Service.asmx/ac_head_new")%>',
                                                 data: "{ 'prefix': '" + request.term + "'}",
                                                 dataType: "json",
                                                 type: "POST",
                                                 contentType: "application/json; charset=utf-8",
                                                 success: function (data) {
                                                     response($.map(data.d, function (item) {
                                                         return {
                                                             label: item.split('^')[0],
                                                             AC_NAME: item.split('^')[1],
                                                             AC_TYPE: item.split('^')[2],
                                                             AC_GROUP: item.split('^')[3]
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
                 <asp:TextBox ID="TextBox146" runat="server" Width="130px"></asp:TextBox>
                 <br />




                 &nbsp;<asp:Label ID="Label398" runat="server" ForeColor="Blue" Text="Job Code" Width="80px"></asp:Label>
                
                  <script type="text/javascript">

                      $(function () {
                          $("[id$=TextBox149]").autocomplete({
                              source: function (request, response) {
                                  $.ajax({
                                      url: '<%=ResolveUrl("~/Service.asmx/ac_head_new")%>',
                                     data: "{ 'prefix': '" + request.term + "'}",
                                     dataType: "json",
                                     type: "POST",
                                     contentType: "application/json; charset=utf-8",
                                     success: function (data) {
                                         response($.map(data.d, function (item) {
                                             return {
                                                 label: item.split('^')[0],
                                                 AC_NAME: item.split('^')[1],
                                                 AC_TYPE: item.split('^')[2],
                                                 AC_GROUP: item.split('^')[3]
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
                 
                  <asp:TextBox ID="TextBox149" runat="server" Width="130px"></asp:TextBox>
                 &nbsp;<br />&nbsp;&nbsp;<asp:Panel ID="Panel18" runat="server" BackColor="#4686F0" ForeColor="White" Height="23px">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label388" runat="server" Font-Bold="True" Font-Size="Medium" Text="REG. DETAILS"></asp:Label>
                 </asp:Panel>
                 &nbsp;<asp:Label ID="Label389" runat="server" ForeColor="Blue" Text="D.Coll" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox142" runat="server" Width="130px"></asp:TextBox>
         <br />
                 &nbsp;<asp:Label ID="Label390" runat="server" ForeColor="Blue" Text="TIN No" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox143" runat="server" Width="130px"></asp:TextBox>
         <br />
                 &nbsp;<asp:Label ID="Label391" runat="server" ForeColor="Blue" Text="ECC No" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox144" runat="server" Width="130px"></asp:TextBox>
         <br />
             </div>
         </div>
    </asp:Panel>
            
             </div>
        </center>
</asp:Content>
