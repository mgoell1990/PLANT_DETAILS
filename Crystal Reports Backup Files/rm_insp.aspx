<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="rm_insp.aspx.vb" Inherits="PLANT_DETAILS.rm_insp" %>
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
           <br />
           <asp:Panel ID="Panel3" runat="server" BorderColor="Lime" BorderStyle="Double" style="text-align: left" Width="1000px" CssClass="brds" Font-Names="Times New Roman">
               <asp:Label ID="Label37" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :30px;" Text="INSPECTION" Width="100%" Height="30px" CssClass="brds"></asp:Label>
         <br />
         <br />
         <br />
               <asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="Blue" Text="CRR" Width="80px"></asp:Label>
               <asp:DropDownList ID="CRR_DropDownList" runat="server" AutoPostBack="True" Width="120px" Font-Names="Times New Roman">
               </asp:DropDownList>
               <asp:Label ID="Label36" runat="server" Font-Bold="True" ForeColor="Blue" Text="PO No" Width="100px"></asp:Label>
               <asp:TextBox ID="po_no_TextBox" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman"></asp:TextBox>
         <br />
               <asp:Label ID="Label29" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat SL No" Width="80px"></asp:Label>
               <asp:DropDownList ID="MATCODE_DropDownList" runat="server" AutoPostBack="True" Width="120px" Font-Names="Times New Roman">
               </asp:DropDownList>
               <asp:Label ID="Label505" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat. Name" Width="100px"></asp:Label>
               <asp:TextBox ID="TextBox184" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px" Font-Names="Times New Roman"></asp:TextBox>
               <asp:Label ID="Label32" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/U"></asp:Label>
               <asp:TextBox ID="AU_GRANTextBox" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman"></asp:TextBox>
         <br />
               <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="Blue" Text="Rcvd Qty" Width="80px"></asp:Label>
               <asp:TextBox ID="RCVDQTY_TextBox" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
        
         <br />
               <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Blue" Text="Reject Qty" Width="80px"></asp:Label>
               <asp:TextBox ID="GARN_REJQTYTextBox" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
         <br />
               <asp:Label ID="Label35" runat="server" Font-Bold="True" ForeColor="Blue" Text="Note" Width="80px"></asp:Label>
               <asp:TextBox ID="NOTE_TextBox" runat="server" Width="361px" Height="91px" TextMode="MultiLine" Font-Names="Times New Roman"></asp:TextBox>
               <asp:Button ID="Button26" runat="server" Font-Bold="True" Text="ADD" Width="85px" CssClass="bottomstyle" />
               <div runat ="server" style ="float :right; ">
                    <asp:Button ID="Button28" runat="server" Font-Bold="True" Text="SAVE" Width="85px" CssClass="bottomstyle" />
               <asp:Button ID="Button27" runat="server" Font-Bold="True" Text="CANCEL" Width="85px" CssClass="bottomstyle" />
               </div>
              
               <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid" Font-Names="Times New Roman">
                   <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                   <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                   <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                   <RowStyle BackColor="White" ForeColor="#003399" />
                   <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                   <SortedAscendingCellStyle BackColor="#EDF6F6" />
                   <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                   <SortedDescendingCellStyle BackColor="#D6DFDF" />
                   <SortedDescendingHeaderStyle BackColor="#002876" />
               </asp:GridView>
         <br />
           </asp:Panel>

           </div>
           </center> 
</asp:Content>
