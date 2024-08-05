<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="PLANT_DETAILS._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <center>
     <div runat ="server" style ="border: 5px groove #0000FF; border-radius :100px;" >
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/SAIL_Logo_svg.png" Height="592px" Width="751px"></asp:Image>
 </div>
     </center>
</asp:Content>
