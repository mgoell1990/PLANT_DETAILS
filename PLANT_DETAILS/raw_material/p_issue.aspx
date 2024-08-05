<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="p_issue.aspx.vb" Inherits="PLANT_DETAILS.p_issue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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
            <div runat ="server" style ="float :right ">
                <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <cc1:calendarextender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox2" />
            </div>
             <asp:Panel ID="Panel2" runat="server" BorderColor="Red" BorderStyle="Groove" BorderWidth="5px" Font-Names="Times New Roman" Font-Size="X-Small" Height="345px" style="text-align: left" Width="705px">
                <div id="fastdivision1" aria-orientation="vertical" aria-selected="undefined" style="width: 700px; height: 348px;">
                    <div style="border-right: 5px groove #FF0066; float: left; text-align: left; width: 450px; height: 347px; font-size: small;">
                        <asp:Label ID="Label435" runat="server" Font-Bold="True" Font-Size="X-Large" style="line-height :30px; text-align: center;" ForeColor="White" Height="30px" Text="REQUISITION" Width="100%" BackColor="#4686F0"></asp:Label>
                 <br />
                        &nbsp;
                        <asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Vaucher No" Width="110px"></asp:Label>
                        <asp:TextBox ID="issue_no" runat="server" BorderStyle="Solid" Enabled="False" Width="150px"></asp:TextBox>
                <br />
                        &nbsp;
                        <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code" Width="110px"></asp:Label>
                        <script type="text/javascript">

     $(function () {
         $("[id$=DropDownList3]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/Service.asmx/P_R_ISSUE")%>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('^')[0],
                                 au: item.split('^')[1],
                                 STOCK: item.split('^')[3],
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
                 $("[id$=TextBox168]").val(i.item.au);
                 $("[id$=TextBox167]").val(i.item.STOCK);
             },
             minLength: 1
         });
     });
    </script>
                        <asp:TextBox ID="DropDownList3" runat="server" Width="260px" Font-Names="Times New Roman"></asp:TextBox>
                 <br />
                        &nbsp;&nbsp;<asp:Label ID="Label426" runat="server" Font-Bold="True" ForeColor="Blue" Text="Qty" Width="110px"></asp:Label>
                        <asp:TextBox ID="TextBox163" runat="server" Width="95px" Font-Names="Times New Roman"></asp:TextBox>
                 <br />
                        &nbsp;&nbsp;<asp:Label ID="Label425" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Type" Width="110px"></asp:Label>
                        <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" Width="120px" Font-Names="Times New Roman">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>To Deptt</asp:ListItem>
                            <asp:ListItem>To Contractor</asp:ListItem>
                        </asp:DropDownList>
                 <br />
                        &nbsp;&nbsp;<asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cost Center" Width="110px"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" Width="260px" Font-Names="Times New Roman">
                        </asp:DropDownList>
                 <br />
                        &nbsp;
                        <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Purpose" Width="110px"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" Height="58px" TextMode="MultiLine" Width="260px" Font-Names="Times New Roman"></asp:TextBox>
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
                       <br />
                 <br />
                 <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                        <br />
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
