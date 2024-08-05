<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="c_inv.aspx.vb" Inherits="PLANT_DETAILS.c_inv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <br />
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True"  ForeColor="#800040" style="text-align: center" Width="100%" Font-Size="XX-Large"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
    if ( window.history.replaceState ) {
        window.history.replaceState( null, null, window.location.href );
    }
</script>
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
           <asp:Panel ID="Panel1" runat="server" style="text-align: left" Width="450px" CssClass="brds">
               <asp:Label ID="Label2" runat="server" Text="Invoice No." Font-Size="Medium" ForeColor="Blue" Width="90px"></asp:Label>
               <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
               <br />
               <asp:Label ID="Label3" runat="server" Text="Fiscal Year" Font-Size="Medium" ForeColor="Blue" Width="90px"></asp:Label>

               <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

               <br />
               <br />
               <asp:Label ID="Label4" runat="server" ForeColor="Red"></asp:Label>
               <br />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="Button2" runat="server" CssClass="bottomstyle" Text="Button" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />
               <br />

           </asp:Panel> 
        
        
        
        
        </div> 
        </center> 
</asp:Content>
