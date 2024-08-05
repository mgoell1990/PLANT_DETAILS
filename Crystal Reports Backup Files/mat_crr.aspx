<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="mat_crr.aspx.vb" Inherits="PLANT_DETAILS.mat_crr" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
           <asp:Panel ID="Panel16" runat="server" BorderColor="#FFFF66" BorderStyle="Groove" Height="150px" style="text-align: left" Width="396px" CssClass="brds">
               <asp:Label ID="search_Label" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Names="Times New Roman" Font-Size="XX-Large" ForeColor="White" style="text-align: center; height:30px; line-height :30px; " Width="100%" CssClass="brds"></asp:Label>
             <br />
               <br />
               <asp:Label ID="Label459" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Type" Width="80px"></asp:Label>
               <asp:DropDownList ID="type_DropDown" runat="server" AutoPostBack="True" BackColor="#00FFCC" Height="25px" Width="197px">
               </asp:DropDownList>
             <br />
               <asp:Label ID="Label460" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="No" Width="80px"></asp:Label>
               <asp:DropDownList ID="search_DropDown" runat="server" BackColor="#00FFCC" Height="25px" Width="197px">
               </asp:DropDownList>
             <br />
              
             <br />
              <p style="text-align: center">
               <asp:Button ID="new_button" runat="server" Font-Bold="True" Text="NEW" Width="82px" CssClass="bottomstyle" />
               <asp:Button ID="view_button" runat="server" Font-Bold="True"  Text="VIEW" Width="82px" CssClass="bottomstyle" />
             </p>
                  <br />
           </asp:Panel>
              <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" HorizontalAlign="Center" style="text-align: left" Width="1000px" CssClass="brds">
            <asp:Label ID="Label449" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="X-Large" ForeColor="White" style="text-align: center; line-height :40px;" Text="MATERIAL RECEIPT" Width="100%" CssClass="brds"  Height="40px"></asp:Label>
              <br />
         <br />
         <br />
             <asp:Label ID="Label465" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="CRR No:-" Width="120px"></asp:Label>
             <asp:TextBox ID="crr_TextBox" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="140px" Font-Names="Times New Roman"></asp:TextBox>
            <br />
             <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="PO No:-" Width="120px"></asp:Label>
             <asp:DropDownList ID="pono_DropDownList" runat="server" AutoPostBack="True" Width="142px" Font-Names="Times New Roman">
             </asp:DropDownList>
             <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Supplier:-" Width="110px"></asp:Label>
             <asp:TextBox ID="suplnameTextBox" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="370px" Font-Names="Times New Roman"></asp:TextBox>
            <br />
             <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Transporter:-" Width="120px"></asp:Label>
             <asp:DropDownList ID="trans_DropDownList" runat="server" AutoPostBack="True" Width="142px" Font-Names="Times New Roman">
             </asp:DropDownList>
             <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Trans Name:-" Width="110px"></asp:Label>
             <asp:TextBox ID="transnameTextBox" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="370px" Font-Names="Times New Roman"></asp:TextBox>
            <br />
             <asp:Label ID="Label463" runat="server" Font-Bold="True" ForeColor="Blue" Text="WO SL No" Width="120px"></asp:Label>
             <asp:DropDownList ID="DropDownList21" runat="server" AutoPostBack="True" Width="142px" Font-Names="Times New Roman">
             </asp:DropDownList>
             <asp:Label ID="Label464" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Name" Width="110px"></asp:Label>
             <asp:TextBox ID="TextBox174" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="370px" Font-Names="Times New Roman"></asp:TextBox>
            <br />
             <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Truck No.:-" Width="120px"></asp:Label>
             <asp:TextBox ID="lr_rr_noTextBox" runat="server" Width="140px" Font-Names="Times New Roman"></asp:TextBox>
             <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Ship Name:-" Width="110px"></asp:Label>
             <asp:TextBox ID="shipnameTextBox" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" Width="370px" Font-Names="Times New Roman"></asp:TextBox>
            <br />
             <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Mat SLNo:-" Width="120px"></asp:Label>
             <asp:DropDownList ID="mat_sl_noDropDownList" runat="server" AutoPostBack="True" Width="142px" Font-Names="Times New Roman">
             </asp:DropDownList>
             <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Mat Name:-" Width="110px"></asp:Label>
             <asp:TextBox ID="matnameTextBox" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" style="text-align: justify" Width="370px" Font-Names="Times New Roman"></asp:TextBox>
             <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="A/U:-" Width="40px"></asp:Label>
             <asp:TextBox ID="au_TextBox" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="80px" Font-Names="Times New Roman"></asp:TextBox>
            <br />
           
         
         
             <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BE No:-" Width="120px"></asp:Label>
             <script type="text/javascript">

                 $(function () {
                     $("[id$=be_TextBox]").autocomplete({
                         source: function (request, response) {
                             $.ajax({
                                 url: '<%=ResolveUrl("~/Service.asmx/BE")%>',
                                  data: "{ 'prefix': '" + request.term + "'}",
                                  dataType: "json",
                                  type: "POST",
                                  contentType: "application/json; charset=utf-8",
                                  success: function (data) {
                                      response($.map(data.d, function (item) {
                                          return {
                                              label: item.split('^')[0],
                                              bedate: item.split('^')[1],
                                              blno: item.split('^')[2],
                                              bldate: item.split('^')[3],
                                              shipname: item.split('^')[4]
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
                              $("[id$=bedate_TextBox]").val(i.item.bedate);
                              $("[id$=bl_noTextBox]").val(i.item.blno);
                              $("[id$=bldate_TextBox]").val(i.item.bldate);
                              $("[id$=shipnameTextBox]").val(i.item.shipname);
                          },
                          minLength: 1
                      });
                  });
    </script>
             <asp:TextBox ID="be_TextBox" runat="server" ForeColor="Black" Width="140px" Font-Names="Times New Roman">N/A</asp:TextBox>
             <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BE Date:-" Width="110px"></asp:Label>
             <asp:TextBox ID="bedate_TextBox" runat="server" BackColor="#4686F0" Enabled="False" ForeColor="White" Width="120px" Font-Names="Times New Roman">N/A</asp:TextBox>
            <br />
             <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BL No:-" Width="120px"></asp:Label>
             <asp:TextBox ID="bl_noTextBox" runat="server" BackColor="#4686F0" Enabled="False" ForeColor="White" Width="139px" Font-Names="Times New Roman">N/A</asp:TextBox>
             <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BL Date:-" Width="110px"></asp:Label>
             <asp:TextBox ID="bldate_TextBox" runat="server" BackColor="#4686F0" Enabled="False" ForeColor="White" Width="120px" Font-Names="Times New Roman">N/A</asp:TextBox>
            <br />
             <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Chalan No:-" Width="120px"></asp:Label>
             <asp:TextBox ID="chalan_TextBox" runat="server" Width="139px" Font-Names="Times New Roman"></asp:TextBox>
             <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Chalan Dt.:-" Width="110px"></asp:Label>
             <asp:TextBox ID="chalandate_TextBox" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
            <cc1:CalendarExtender ID="chalandate_TextBox_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="chalandate_TextBox_CalendarExtender" TargetControlID="chalandate_TextBox" />
             <asp:Label ID="ERRLabel" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
            <br />
             <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Chalan Qty:-" Width="120px"></asp:Label>
             <asp:TextBox ID="chalan_qty_TextBox" runat="server" Width="139px" Font-Names="Times New Roman"></asp:TextBox>
             <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Rcvd Qty:-" Width="110px"></asp:Label>
             <asp:TextBox ID="rcv_qty_TextBox" runat="server" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
             <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="No OF Bag:-" Width="80px"></asp:Label>
             <asp:TextBox ID="no_of_bagTextBox" runat="server" Width="100px" Font-Names="Times New Roman">0</asp:TextBox>
             <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Bag Weight(Kg):-" Width="103px"></asp:Label>
             <asp:TextBox ID="bag_weightTextBox" runat="server" Width="100px" Font-Names="Times New Roman">0.00</asp:TextBox>
            <div runat ="server" style ="float :right ;">
                 <asp:Button ID="crr_add_Button" runat="server"  Text="ADD" CssClass="bottomstyle" Font-Size="Small" Width="85px" />
            </div>
                  
             <asp:GridView ID="crr_gridview" runat="server" Font-Size="Small" ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid" Font-Names="Times New Roman">
             </asp:GridView>
                  <div runat="server" style =" float :right;">
                       <asp:Button ID="Button1" runat="server" Font-Bold="True"  Text="CLOSE" Width="85px" CssClass="bottomstyle" Font-Size="Small" />
                       <asp:Button ID="crr_cancel_Button" runat="server" Font-Bold="True"  Text="CANCEL" Width="85px" CssClass="bottomstyle" Font-Size="Small" />
                       <asp:Button ID="crr_save_Button" runat="server" Font-Bold="True" Text="SAVE" Width="85px" CssClass="bottomstyle" Font-Size="Small" />
                  </div>
                 <br />
                  <br />
                  <br />
         </asp:Panel>
       </div>
    </center>
</asp:Content>
