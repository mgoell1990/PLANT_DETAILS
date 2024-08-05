<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="mat_issue_return.aspx.vb" Inherits="PLANT_DETAILS.mat_issue_return" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif;">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
     <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">
            <asp:Panel ID="Panel2" runat="server" BorderColor="Red" BorderStyle="Groove" BorderWidth="5px" Font-Names="Times New Roman" Font-Size="X-Small" Height="354px" style="text-align: left" Width="705px">
                <div id="fastdivision1" aria-orientation="vertical" aria-selected="undefined" style="width: 683px; height: 355px;">
                    <div style="border-right: 5px groove #FF0066; float: left; text-align: left; width: 450px; height: 347px; font-size: small;">
                        <asp:Label ID="Label435" runat="server" Font-Bold="True" Font-Size="X-Large" style="line-height :30px; text-align: center;" ForeColor="White" Height="30px" Text="ISSUE" Width="100%" BackColor="#4686F0"></asp:Label>
                 <br />
                      &nbsp;&nbsp;<br />
                       &nbsp;&nbsp;<asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Vaucher No" Width="110px"></asp:Label>
                        <script type="text/javascript">
                      $(function () {
                     $("[id$=TextBox171]").autocomplete({
                          source: function (request, response) {
                          $.ajax({
                   url: '<%=ResolveUrl("~/Service.asmx/STORE_ISSUE_RET")%>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('^')[0],
                                 val: item.split('^')[1],
                                 au: item.split('^')[2],
                                 mat_stock: item.split('^')[3],
                                 mat_avg: item.split('^')[4],
                                 mat_loca: item.split('^')[5],
                                 dept: item.split('^')[6],
                                 cost_cent: item.split('^')[7],
                                 purpose: item.split('^')[8],
                                 r_qty: item.split('^')[9],
                                 line_no: item.split('^')[10]
                                
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
               $("[id$=DropDownList3]").val(i.item.val);
                 $("[id$=TextBox168]").val(i.item.au);
                 $("[id$=TextBox167]").val(i.item.mat_stock);
                 $("[id$=TextBox166]").val(i.item.mat_avg);
                 $("[id$=TextBox170]").val(i.item.mat_loca);
                 $("[id$=TextBox169]").val(i.item.line_no);
                 $("[id$=TextBox163]").val(i.item.r_qty);
                 $("[id$=TextBox172]").val(i.item.dept);
                 $("[id$=TextBox173]").val(i.item.cost_cent);
                 $("[id$=TextBox2]").val(i.item.purpose);
             },
             minLength: 1
         });
     });
    </script>
                        <asp:TextBox ID="TextBox171" runat="server" Width="260px" BorderStyle="Solid"></asp:TextBox>
                        <br />
                        &nbsp;&nbsp;<asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code" Width="110px"></asp:Label>
                        <asp:TextBox ID="DropDownList3" runat="server" Font-Names="Times New Roman" Width="260px" Enabled="False" BackColor="White" BorderStyle="Solid" ForeColor="Red"></asp:TextBox>
                        <br />
                        &nbsp;&nbsp;<asp:Label ID="Label426" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issued Qty." Width="110px"></asp:Label>
                        <asp:TextBox ID="TextBox163" runat="server" Font-Names="Times New Roman" Width="95px" BorderStyle="Solid" Enabled="False" ForeColor="Red"></asp:TextBox>
                        <br />
&nbsp;&nbsp;<asp:Label ID="Label451" runat="server" Font-Bold="True" ForeColor="Blue" Text="Return Qty" Width="110px"></asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server" BorderStyle="Solid" Font-Names="Times New Roman" Width="95px"></asp:TextBox>
                        <br />
                        &nbsp;&nbsp;<asp:Label ID="Label425" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Type" Width="110px"></asp:Label>
                        <asp:TextBox ID="TextBox172" runat="server" BackColor="White" BorderStyle="Solid" Enabled="False" ForeColor="Red" Width="95px"></asp:TextBox>
                        <br />
                        &nbsp;&nbsp;<asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cost Center" Width="110px"></asp:Label>
                        <asp:TextBox ID="TextBox173" runat="server" BackColor="White" BorderStyle="Solid" Enabled="False" ForeColor="Red" Width="260px"></asp:TextBox>
                        <div runat="server" style=" float :left ;">
                            &nbsp;
                            <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Purpose" Width="110px"></asp:Label>
                        </div>
                        <div runat="server" style=" margin-left :120px;">
                            <asp:TextBox ID="TextBox2" runat="server" BorderStyle="None" Enabled="False" Font-Names="Times New Roman" ForeColor="Red" Height="58px" TextMode="MultiLine" Width="260px"></asp:TextBox>
                        </div>
                        <br />
                        &nbsp;
                        <br />
                        <asp:Label ID="ISSUE_ERR_LABEL" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        <br />
                    </div>
                    <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="float: right; font-family: 'Times New Roman', Times, serif; font-size: small; height: 346px; text-align: left;">
                <br />
                        <asp:Label ID="Label429" runat="server" Font-Bold="True" ForeColor="Blue" Width="85px">A/U</asp:Label>
                        <asp:TextBox ID="TextBox168" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                        <br />
                        <asp:Label ID="Label431" runat="server" Font-Bold="True" ForeColor="Blue" Text="Availble Stock" Width="85px"></asp:Label>
                        <asp:TextBox ID="TextBox167" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                       <br />
                         <asp:Label ID="Label430" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Value" Width="85px"></asp:Label>
                        <asp:TextBox ID="TextBox166" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                       <br />
                         <asp:Label ID="Label432" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No" Width="85px"></asp:Label>
                        <asp:TextBox ID="TextBox169" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                 <br />
                        <asp:Label ID="Label450" runat="server" Font-Bold="True" ForeColor="Blue" Text="Location" Width="85px"></asp:Label>
                        <asp:TextBox ID="TextBox170" runat="server" BackColor="#4686F0" Enabled="False" ForeColor="White" Width="120px" Font-Names="Times New Roman"></asp:TextBox>
                 <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button45" runat="server" CssClass="bottomstyle" Text="SAVE" Width="100px" />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button46" runat="server" Text="CANCEL" Width="100px" CssClass="bottomstyle" />
                 <br />
                 <br />
                    </div>
                </div>
            </asp:Panel>
            </div> 
         </center> 
</asp:Content>
