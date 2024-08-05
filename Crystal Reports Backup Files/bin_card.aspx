<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="bin_card.aspx.vb" Inherits="PLANT_DETAILS.bin_card" %>
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
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href ="../Content/Site.css" rel ="stylesheet"  type ="text/css" />
    <center>
        <div runat ="server" style ="min-height :600px;">
            <asp:Panel ID="Panel5" runat="server" BorderColor="#33CC33" BorderStyle="Groove" ScrollBars="Auto" Width="790px" Font-Names="Times New Roman" style="text-align: left" CssClass="brds">
         &nbsp;<asp:Label ID="Label54" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="MATERIAL" Width="786px" CssClass="brds"></asp:Label>
         <br />
         <br />
         <br />
         <asp:Label ID="Label40" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Name" Width="80px"></asp:Label>
                 <script type="text/javascript">

           $(function () {
               $("[id$=TextBox115]").autocomplete({
                   source: function (request, response) {
                       $.ajax({
                           url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                                        data: "{ 'prefix': '" + request.term + "'}",
                                        dataType: "json",
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.split('^')[0],
                                                    val: item.split('^')[1],
                                                    MAT_AU: item.split('^')[2]
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
                 <asp:TextBox ID="TextBox115" runat="server" Width="400px" AutoPostBack="True" ></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="VIEW" CssClass="bottomstyle" />
                <br />
                <br />
                <asp:FormView ID="imp_FormView1" runat="server" CellPadding ="4"  CellSpacing="2" Font-Names="Times New Roman" ForeColor="Black" GridLines="Both" Height="56%" Width="100%" BackColor="#D7D6D7" Font-Size="Medium">
                    <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate>
                        <br />
                        <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF;"><%# Eval("MAT_CODE")%></h3>
                       <br />
                        <br />
                         <asp:Label ID="Label506" runat="server" ForeColor="Blue" Text='<%# Eval("MAT_NAME")%>'></asp:Label>
                        <br />
                        <br />
                         <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text='<%# Eval("MAT_DRAW")%>'></asp:Label>
                        <br />
                        <br />
                        <div runat ="server" style ="float :left;  width :300px;">
                             <asp:Label ID="Label507" runat="server" Font-Bold="True" Text="Mat. Stock" Width="100"></asp:Label>
                        <asp:Label ID="Label508" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label509" runat="server" Text='<%# Eval("MAT_STOCK")%>' ></asp:Label>
                            <br />
                            <br />
                             <asp:Label ID="Label513" runat="server" Font-Bold="True" Text="Mat. Loc." Width="100"></asp:Label>
                        <asp:Label ID="Label514" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label515" runat="server" Text='<%# Eval("MAT_LOCATATION")%>' ></asp:Label>
                       <br />
                       <br />
                       <asp:Label ID="Label519" runat="server" Font-Bold="True" Text="Last Pur." Width="100"></asp:Label>
                        <asp:Label ID="Label520" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label521" runat="server" Text='<%# Eval("MAT_LASTPUR_DATE")%>' ></asp:Label>
                       <br />
                       <br />
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Mat. Price" Width="100"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("MAT_AVG")%>' ></asp:Label>
                       <br />
                       </div>
                        <div runat ="server" style ="float :right; width :300px;">
                              <asp:Label ID="Label510" runat="server" Font-Bold="True" Text="Mat. Au" Width="120"></asp:Label>
                        <asp:Label ID="Label511" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label512" runat="server" Text='<%# Eval("MAT_AU")%>' ></asp:Label>
                       <br />
                       <br />
                         <asp:Label ID="Label516" runat="server" Font-Bold="True" Text="Last Trans." Width="120"></asp:Label>
                        <asp:Label ID="Label517" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label518" runat="server" Text='<%# Eval("LAST_TRANS_DATE")%>' ></asp:Label>
                       <br />
                       <br />
                         <asp:Label ID="Label522" runat="server" Font-Bold="True" Text="Last Issue" Width="120"></asp:Label>
                        <asp:Label ID="Label523" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label524" runat="server" Text='<%# Eval("LAST_ISSUE_DATE")%>' ></asp:Label>
                        <br />
                         <br />
                         <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Last Pur. Price" Width="120"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Text=":"></asp:Label>
                        <asp:Label ID="Label8" runat="server" Text='<%# Eval("MAT_LAST_RATE")%>' ></asp:Label>
                             </div>
                         </ItemTemplate>
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                </asp:FormView>
                <br />
     </asp:Panel>
        </div>
    </center>
</asp:Content>
