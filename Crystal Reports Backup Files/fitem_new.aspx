<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true"  MasterPageFile="~/Site.Master" CodeBehind="fitem_new.aspx.vb" Inherits="PLANT_DETAILS.fitem_new" %>
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
         <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" CssClass="brds" Width="500px"> 
             <div class="auto-style1">
                 <asp:Label ID="Label3" runat="server" BackColor="Blue" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center" Text="FINISH GOODS" Width="100%"></asp:Label>
                 <br />
                 <asp:Label ID="Label4" runat="server" Text="Item Code" Width="110px"></asp:Label>
                 <asp:TextBox ID="TextBox1" runat="server" Width="130px"></asp:TextBox>
                   <script type="text/javascript">
                          $(function () {
                      $("[id$=TextBox1]").autocomplete({
                          source: function (request, response) {
                              $.ajax({
                                  url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODS_NEW")%>',
                           data: "{ 'prefix': '" + request.term + "'}",
                           dataType: "json",
                           type: "POST",
                           contentType: "application/json; charset=utf-8",
                           success: function (data) {
                               response($.map(data.d, function (item) {
                                   return {
                                       label: item.split('^')[0],
                                       ITEM_NAME: item.split('^')[1],
                                       ITEM_AU: item.split('^')[2],
                                       ITEM_DRAW: item.split('^')[3],
                                       ITEM_TYPE: item.split('^')[4],
                                       ITEM_CHPT: item.split('^')[5],
                                      ITEM_STATUS: item.split('^')[6]
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
                       $("[id$=TextBox2]").val(i.item.ITEM_NAME);
                       $("[id$=TextBox3]").val(i.item.ITEM_DRAW);
                       $("[id$=DropDownList2]").val(i.item.ITEM_TYPE);
                       $("[id$=DropDownList3]").val(i.item.ITEM_CHPT);
                       $("[id$=DropDownList1]").val(i.item.ITEM_AU);
                       $("[id$=DropDownList4]").val(i.item.ITEM_STATUS);
                   },
                   minLength: 1
               });
           });
    </script>








                 <br />
                 <asp:Label ID="Label5" runat="server" Text="Item Name" Width="110px"></asp:Label>
                 <asp:TextBox ID="TextBox2" runat="server" Width="364px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label6" runat="server" Text="Item Drawing" Width="110px"></asp:Label>
                 <asp:TextBox ID="TextBox3" runat="server" Width="364px"></asp:TextBox>
                 <br />
                 <asp:Label ID="Label12" runat="server" Text="Item Weight" Width="110px"></asp:Label>
                 <asp:TextBox ID="TextBox4" runat="server" Width="130px">0.000</asp:TextBox>
                 Kg.<br />
                 <asp:Label ID="Label7" runat="server" Text="Item AU." Width="110px"></asp:Label>
                 <asp:DropDownList ID="DropDownList1" runat="server" Width="130px">
                     <asp:ListItem>Select</asp:ListItem>
                     <asp:ListItem>Pcs</asp:ListItem>
                     <asp:ListItem>Mt</asp:ListItem>
                     <asp:ListItem>Activity</asp:ListItem>
                 </asp:DropDownList>
                 <asp:Label ID="Label8" runat="server" Text="Item Group" Width="100px"></asp:Label>
                 <asp:DropDownList ID="DropDownList2" runat="server" Width="130px">
                 </asp:DropDownList>
                 <br />
                 <asp:Label ID="Label9" runat="server" Text="Chapter Heading" Width="110px"></asp:Label>
                 <asp:DropDownList ID="DropDownList3" runat="server" Width="130px">
                 </asp:DropDownList>
                 <asp:Label ID="Label10" runat="server" Text="Item Status" Width="100px"></asp:Label>
                 <asp:DropDownList ID="DropDownList4" runat="server" Width="130px">
                     <asp:ListItem>Select</asp:ListItem>
                     <asp:ListItem>ACTIVE</asp:ListItem>
                     <asp:ListItem>CLOSE</asp:ListItem>
                 </asp:DropDownList>
                 <br />
                   <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                   <br />
                 <div runat ="server" style ="float :right; text-align: left; width: 308px;">
                     <asp:Button ID="Button1" runat="server" Text="SAVE" CssClass="bottomstyle" Width="90px"></asp:Button>
                     <asp:Button ID="Button2" runat="server" Text="CANCEL" CssClass="bottomstyle" Width="90px"></asp:Button>
                     <asp:Button ID="Button3" runat="server" Text="CLOSE" CssClass="bottomstyle" Width="90px"></asp:Button>
                 </div>
               
                 <br />
                 <br />
             </div>
             </asp:Panel>
            </div>
        </center>
       </asp:Content>
