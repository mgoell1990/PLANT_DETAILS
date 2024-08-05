<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="supl.aspx.vb" Inherits="PLANT_DETAILS.supl" %>
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
          <asp:Panel ID="Panel11" runat="server" BorderColor="#FF3399" BorderStyle="Groove" BorderWidth="5px" Font-Size="X-Small" Height="411px" Width="700px">
         <div id="fastdivision0" aria-orientation="vertical" aria-selected="undefined" style=" width: 700px;  height: 150px; ">
             <div style="border-right: 5px groove #FF0066; float:left; text-align: left; width: 535px; height: 150px;">
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label351" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="#CC0000" Text="SUPPLIER"></asp:Label>
                 <br />
                 &nbsp;<asp:Label ID="Label307" runat="server" Font-Size="Small" ForeColor="Blue" Text="Supl Code" Width="100px"></asp:Label>
                 <script type="text/javascript">
                          $(function () {
                              $("[id$=TextBox84]").autocomplete({
                          source: function (request, response) {
                              $.ajax({
                                  url: '<%=ResolveUrl("~/Service.asmx/SUPL_DETAILS")%>',
                           data: "{ 'prefix': '" + request.term + "'}",
                           dataType: "json",
                           type: "POST",
                           contentType: "application/json; charset=utf-8",
                           success: function (data) {
                               response($.map(data.d, function (item) {
                                   return {
                                       label: item.split('^')[0],
                                       SUPL_NAME: item.split('^')[1],
                                       SUPL_CONTACT_PERSON: item.split('^')[2],
                                       SUPL_AT: item.split('^')[3],
                                       SUPL_PO: item.split('^')[4],
                                       SUPL_DIST: item.split('^')[5],
                                       SUPL_PIN: item.split('^')[6],
                                       SUPL_STATE: item.split('^')[7],
                                       SUPL_COUNTRY: item.split('^')[8],
                                       SUPL_MOB1: item.split('^')[9],
                                       SUPL_MOB2: item.split('^')[10],
                                       SUPL_LAND: item.split('^')[11],
                                       SUPL_FAX: item.split('^')[12],
                                       SUPL_EMAIL: item.split('^')[13],
                                       SUPL_WEB: item.split('^')[14],
                                       SUPL_PAN: item.split('^')[15],
                                       SUPL_TIN: item.split('^')[16],
                                       SUPL_ST_NO: item.split('^')[17],
                                       SUPL_BANK: item.split('^')[18],
                                       SUPL_ACOUNT_NO: item.split('^')[19],
                                       SUPL_IFSC: item.split('^')[20],
                                       SUPL_TYPE: item.split('^')[21]
                                      
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
                       $("[id$=TextBox85]").val(i.item.SUPL_NAME);
                       $("[id$=TextBox86]").val(i.item.SUPL_CONTACT_PERSON);
                       $("[id$=SUPLDropDownList17]").val(i.item.SUPL_TYPE);
                       $("[id$=Textbox99]").val(i.item.SUPL_AT);
                       $("[id$=TextBox100]").val(i.item.SUPL_PO);
                       $("[id$=TextBox101]").val(i.item.SUPL_DIST);
                       $("[id$=TextBox102]").val(i.item.SUPL_PIN);
                       $("[id$=TextBox103]").val(i.item.SUPL_STATE);
                       $("[id$=TextBox104]").val(i.item.SUPL_COUNTRY);
                       $("[id$=TextBox93]").val(i.item.SUPL_MOB1);
                       $("[id$=TextBox94]").val(i.item.SUPL_MOB2);
                       $("[id$=TextBox95]").val(i.item.SUPL_LAND);
                       $("[id$=TextBox105]").val(i.item.SUPL_FAX);
                       $("[id$=TextBox106]").val(i.item.SUPL_EMAIL);
                       $("[id$=TextBox107]").val(i.item.SUPL_WEB);
                       $("[id$=TextBox108]").val(i.item.SUPL_BANK);
                       $("[id$=TextBox109]").val(i.item.SUPL_ACOUNT_NO);
                       $("[id$=TextBox110]").val(i.item.SUPL_IFSC);
                       $("[id$=TextBox111]").val(i.item.SUPL_PAN);
                       $("[id$=TextBox112]").val(i.item.SUPL_TIN);
                       $("[id$=TextBox113]").val(i.item.SUPL_ST_NO);
                       
                   },
                   minLength: 1
               });
           });
    </script>
                 
                 
                  <asp:TextBox ID="TextBox84" runat="server" Font-Size="Small" Width="125px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label308" runat="server" Font-Size="Small" ForeColor="Blue" Text="Company Name" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox85" runat="server" Font-Size="Small" Width="200px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label309" runat="server" Font-Size="Small" ForeColor="Blue" Text="Contact Person" Width="100px"></asp:Label>
                 <asp:TextBox ID="TextBox86" runat="server" Font-Size="Small" Width="200px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label310" runat="server" Font-Size="Small" ForeColor="Blue" Text="Supl Type" Width="100px"></asp:Label>
                 <asp:DropDownList ID="SUPLDropDownList17" runat="server" AutoPostBack="True" Font-Size="Small" Width="125px">
                     <asp:ListItem>Select</asp:ListItem>
                     <asp:ListItem>Within  State</asp:ListItem>
                     <asp:ListItem>Out Side State</asp:ListItem>
                     <asp:ListItem>Foreign</asp:ListItem>
                 </asp:DropDownList>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             </div>
             <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="float:left; border-bottom-width: medium;margin-left:0px; margin-top :0px; width: 155px; font-family: 'Times New Roman', Times, serif; font-size: small; height: 147px; line-height: 25px; text-align: center;">
         <br />
         <br />
                 <asp:Button ID="Button42" runat="server" Font-Size="X-Small" ForeColor="Blue" Text="SAVE" Width="100px" CssClass="bottomstyle" />
         <br />
                 <asp:Button ID="Button43" runat="server" Font-Size="X-Small" ForeColor="Blue" Text="CANCEL" Width="100px" CssClass="bottomstyle" />
             </div>
         </div>
         <asp:Panel ID="Panel12" runat="server" BackColor="#4686F0" ForeColor="White" Height="20px">
             <asp:Label ID="Label323" runat="server" Font-Bold="True" Font-Size="Medium" Text="ADDRESS DETAILS"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label333" runat="server" Font-Bold="True" Font-Size="Medium" Text="CONTACT DETAILS"></asp:Label>
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label334" runat="server" Font-Bold="True" Font-Size="Medium" Text="BANK DETAILS"></asp:Label>
         </asp:Panel>
         <div id="SECONDdivision" aria-orientation="vertical" aria-selected="undefined" style=" width: 700px;  height: 237px; ">
             <div style="float:left; border-right-color: #FF0066; border-right-style: groove; border-right-width: 5px; text-align: left; width: 435px; height: 198px; border-bottom-style: double; border-bottom-width: medium; border-bottom-color: #FF0066; font-size: small;">

           <br />
                 &nbsp;<asp:Label ID="Label335" runat="server" ForeColor="Blue" Text="At" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox99" runat="server" Width="125px"></asp:TextBox>
                 &nbsp;<asp:Label ID="Label324" runat="server" ForeColor="Blue" Text="MOB No" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox93" runat="server" Width="125px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label336" runat="server" ForeColor="Blue" Text="Po" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox100" runat="server" Width="125px"></asp:TextBox>
                 &nbsp;<asp:Label ID="Label325" runat="server" ForeColor="Blue" Text="MOB No" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox94" runat="server" Width="125px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label337" runat="server" ForeColor="Blue" Text="Dist" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox101" runat="server" Width="125px"></asp:TextBox>
                 &nbsp;<asp:Label ID="Label326" runat="server" ForeColor="Blue" Text="Office No" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox95" runat="server" Width="125px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label338" runat="server" ForeColor="Blue" Text="PIN" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox102" runat="server" Width="125px"></asp:TextBox>
                 &nbsp;<asp:Label ID="Label341" runat="server" ForeColor="Blue" Text="FAX No" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox105" runat="server" Width="125px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label339" runat="server" ForeColor="Blue" Text="State" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox103" runat="server" Width="125px"></asp:TextBox>
                 &nbsp;<asp:Label ID="Label342" runat="server" ForeColor="Blue" Text="Email Id" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox106" runat="server" Width="125px"></asp:TextBox>
           <br />
                 &nbsp;<asp:Label ID="Label340" runat="server" ForeColor="Blue" Text="Country" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox104" runat="server" Width="125px"></asp:TextBox>
                 &nbsp;<asp:Label ID="Label343" runat="server" ForeColor="Blue" Text="Web Site" Width="70px"></asp:Label>
                 <asp:TextBox ID="TextBox107" runat="server" Width="125px"></asp:TextBox>
                 <br />
                 <br />
                 <asp:Label ID="ERR_LABLE" runat="server" Font-Size="Medium" ForeColor="Red" Text="err" Visible="False"></asp:Label>
             </div>
             <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="border-bottom: medium double #FF0066; float:left; margin-left:0px; margin-top :0px; width: 255px; font-family: 'Times New Roman', Times, serif; font-size: small; height: 198px; text-align: left;">
                 &nbsp;<asp:Label ID="Label344" runat="server" ForeColor="Blue" Text="Bank Name" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox108" runat="server" Width="130px"></asp:TextBox>
         <br />
                 &nbsp;<asp:Label ID="Label345" runat="server" ForeColor="Blue" Text="AC No" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox109" runat="server" Width="130px"></asp:TextBox>
         <br />
                 &nbsp;<asp:Label ID="Label346" runat="server" ForeColor="Blue" Text="IFSC Code" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox110" runat="server" Width="130px"></asp:TextBox>
                 <asp:Panel ID="Panel13" runat="server" BackColor="#4686F0" ForeColor="White" Height="23px">
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label350" runat="server" Font-Bold="True" Font-Size="Medium" Text="REG. DETAILS"></asp:Label>
                 </asp:Panel>
                 &nbsp;<asp:Label ID="Label347" runat="server" ForeColor="Blue" Text="PAN No" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox111" runat="server" Width="130px"></asp:TextBox>
         <br />
                 &nbsp;<asp:Label ID="Label348" runat="server" ForeColor="Blue" Text="TIN No" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox112" runat="server" Width="130px"></asp:TextBox>
         <br />
                 &nbsp;<asp:Label ID="Label349" runat="server" ForeColor="Blue" Text="Service Reg" Width="80px"></asp:Label>
                 <asp:TextBox ID="TextBox113" runat="server" Width="130px"></asp:TextBox>
         <br />
             </div>
         </div>
    </asp:Panel>
            
            
             </div> 
        </center> 
</asp:Content>
