<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true" MasterPageFile="~/Site.Master" CodeBehind="q_group.aspx.vb" Inherits="PLANT_DETAILS.q_group" %>
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
           <asp:Panel ID="Panel1" runat="server" style="text-align: left" Width="400px" BorderColor="Blue" BorderStyle="Groove" CssClass="brds" Height="250px">
<asp:Label ID="Label2" runat="server" Text="QUALITY GROUP" BackColor="Blue" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center" Width="100%"></asp:Label>
               <br />
               <br />
               <asp:Label ID="Label3" runat="server" Text="Quality Code" Width="80px"></asp:Label>
               <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <script type="text/javascript">
                          $(function () {
                      $("[id$=TextBox1]").autocomplete({
                          source: function (request, response) {
                              $.ajax({
                                  url: '<%=ResolveUrl("~/Service.asmx/Q_GROUP")%>',
                           data: "{ 'prefix': '" + request.term + "'}",
                           dataType: "json",
                           type: "POST",
                           contentType: "application/json; charset=utf-8",
                           success: function (data) {
                               response($.map(data.d, function (item) {
                                   return {
                                       label: item.split('^')[0],
                                       Q_NAME: item.split('^')[1],
                                       Q_DESC: item.split('^')[2],
                                       Q_b: item.split('^')[3]
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
                       $("[id$=TextBox2]").val(i.item.Q_NAME);
                       $("[id$=TextBox3]").val(i.item.Q_DESC);
                   },
                   minLength: 1
               });
           });
    </script>









               <br />
               <asp:Label ID="Label4" runat="server" Text="Quality Name" Width="80px"></asp:Label>
               <asp:TextBox ID="TextBox2" runat="server" Width="300px"></asp:TextBox>
               <br />
               <asp:Label ID="Label5" runat="server" Text="Quality Desc." Width="80px"></asp:Label>
               <asp:TextBox ID="TextBox3" runat="server" Width="300px"></asp:TextBox>
               <br />
               <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
               <br />
               <div runat="server" style="float :right; text-align: right;">
                   <asp:Button ID="Button1" runat="server" CssClass="bottomstyle" Text="SAVE" Width="90px" />
                   <asp:Button ID="Button2" runat="server" CssClass="bottomstyle" Text="CANCEL" Width="90px" />
                   <asp:Button ID="Button3" runat="server" CssClass="bottomstyle" Text="CLOSE" Width="90px" />
               </div>
<br />




           </asp:Panel>
            
            
            
            
             </div>
        </center> 
</asp:Content>
