<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="ac_head.aspx.vb" Inherits="PLANT_DETAILS.ac_head" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;"  >

<asp:Panel ID="Panel1" runat="server" style="text-align: left" Width="600px" BorderColor="Blue" BorderStyle="Groove" CssClass="brds" Height="300px">
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="XX-Large" Height="26px" style="text-align: center" Text="ACCOUNT HEAD" Width="100%" BackColor="Blue" CssClass="brds" ForeColor="White"></asp:Label>
    <br />
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Account Head" Width="120px"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
     <script type="text/javascript">
         $(function () {
                      $("[id$=TextBox1]").autocomplete({
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
                       $("[id$=TextBox3]").val(i.item.AC_NAME);
                       $("[id$=TextBox4]").val(i.item.AC_TYPE);
                       $("[id$=TextBox2]").val(i.item.AC_GROUP);
                   },
                   minLength: 1
               });
           });
    </script>

    <br />
    <asp:Label ID="Label6" runat="server" Text="Account Head Desc." Width="120px"></asp:Label>
    <asp:TextBox ID="TextBox3" runat="server" Width="300px"></asp:TextBox>
         <script type="text/javascript">
             $(function () {
                 $("[id$=TextBox3]").autocomplete({
                     source: function (request, response) {
                         $.ajax({
                             url: '<%=ResolveUrl("~/Service.asmx/ac_head_name")%>',
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
 <br />
    <asp:Label ID="Label5" runat="server" Text="Account Head Type" Width="120px"></asp:Label>
    <asp:TextBox ID="TextBox4" runat="server" Width="300px"></asp:TextBox>
             <script type="text/javascript">
                 $(function () {
                     $("[id$=TextBox4]").autocomplete({
                         source: function (request, response) {
                             $.ajax({
                                 url: '<%=ResolveUrl("~/Service.asmx/ac_head_type")%>',
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
    <br />
    <asp:Label ID="Label4" runat="server" Text="Account Head Group" Width="120px"></asp:Label>
    <asp:TextBox ID="TextBox2" runat="server" Width="300px"></asp:TextBox>
               <script type="text/javascript">
                   $(function () {
                       $("[id$=TextBox2]").autocomplete({
                           source: function (request, response) {
                               $.ajax({
                                   url: '<%=ResolveUrl("~/Service.asmx/ac_head_group")%>',
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

    <br />
    <br />
    <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    <br />
   <div runat ="server" style ="float :right; text-align: right;">
       <asp:Button ID="Button1" runat="server" Text="SAVE" CssClass="bottomstyle" Width="90px"></asp:Button>
       <asp:Button ID="Button2" runat="server" Text="CANCEL" CssClass="bottomstyle" Width="90px"></asp:Button>
       <asp:Button ID="Button3" runat="server" Text="CLOSE" CssClass="bottomstyle" Width="90px"></asp:Button>
   </div>

</asp:Panel>








            </div>
        </center>
</asp:Content>
