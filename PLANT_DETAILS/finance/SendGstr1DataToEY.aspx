<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="SendGstr1DataToEY.aspx.vb" Inherits="PLANT_DETAILS.SendGstr1DataToEY" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">
           
             <br />
                    <asp:Label ID="Label49" runat="server" ForeColor="Blue" Text="Search For"></asp:Label>
                    <asp:DropDownList ID="DropDownList9" runat="server" Width="125px" AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>GSTR-1 Data</asp:ListItem>
                        <asp:ListItem>GSTR-3B Data</asp:ListItem>
                        
                    </asp:DropDownList>
            <asp:MultiView ID="MultiView2" runat="server">

                     <%--=====VIEW 3 ADVANCE VOUCHER START=====--%>
                     <asp:View ID="View1" runat="server">
            <asp:Panel ID="Panel2" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label401" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="Send GSTR-1 Data" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
                <asp:Label ID="Label403" runat="server" Font-Bold="True" ForeColor="Blue" Text="Fiscal Year:-" Width="80px"></asp:Label>
               <asp:DropDownList ID="DropDownList27" runat="server" AutoPostBack="True">
                   <asp:ListItem>2122</asp:ListItem>
                   <asp:ListItem>2223</asp:ListItem>
                   <asp:ListItem>2324</asp:ListItem>
                   <asp:ListItem>2425</asp:ListItem>
                   <asp:ListItem>2526</asp:ListItem>
               </asp:DropDownList>
                <br />
              <br />
                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Blue" Text="Invoice Type:-" Width="80px"></asp:Label>
               <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True">
                   <asp:ListItem>Select</asp:ListItem>
                   <asp:ListItem>Credit/Debit Note</asp:ListItem>
                   <asp:ListItem>FG Invoice</asp:ListItem>
               </asp:DropDownList>
               <br />
              <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Invoice No:-" Width="80px"></asp:Label>
             
                            <script type="text/javascript">
                                $(function () {
                                    $("[id$=DropDownList26]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/Service.asmx/wo_no_search")%>',
                                                data: "{ 'prefix': '" + request.term + "'}",
                                                dataType: "json",
                                                type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                success: function (data) {
                                                    response($.map(data.d, function (item) {
                                                        return {
                                                            label: item.split('^')[0],

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
              
              
              
              
              
               <asp:DropDownList ID="DropDownList28" runat="server" Width="200px" AutoPostBack="True">
                   
               </asp:DropDownList>
               <br />
               
               <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Blue" Text="Original invoice date:-" Width="80px"></asp:Label>
               <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                <br />
               
               <asp:Label ID="Label404" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code:-" Width="80px"></asp:Label>
               <asp:Label ID="Label405" runat="server" Text="Label"></asp:Label>
               <br />
               <asp:Label ID="Label406" runat="server" Font-Bold="True" ForeColor="Blue" Text="Consig. Code:-" Width="80px"></asp:Label>
               <asp:Label ID="Label407" runat="server" Text="Label"></asp:Label>
              <br />
               <asp:Label ID="Label408" runat="server" Font-Bold="True" ForeColor="Blue" Text="Product Code:-" Width="80px"></asp:Label>
               <asp:Label ID="Label409" runat="server" Text="Label"></asp:Label>
             <br />
               <asp:Label ID="Label410" runat="server" Font-Bold="True" ForeColor="Blue" Text="Prod. Desc.:-" Width="80px"></asp:Label>
               <asp:Label ID="Label411" runat="server" Text="Label"></asp:Label>
               <br />
               <asp:Label ID="lblErrorMsg" runat="server" Text="Label"></asp:Label>
            <br />
              <br />
               <div runat ="server" style ="width :100%; text-align: center;">
 <asp:Button ID="Button45" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Send Single Data" Width="130px" CssClass="bottomstyle" />
                   <asp:Button ID="Button46" runat="server" CssClass="bottomstyle" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Send Bulk Data" Width="130px" />
               </div>
          </asp:Panel>
                </asp:View>
            <br />
            <asp:View ID="View2" runat="server">
            <asp:Panel ID="Panel1" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label2" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="Send GSTR-3B Data" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Invoice No:-" Width="80px"></asp:Label>
             
                            <script type="text/javascript">
                                $(function () {
                                    $("[id$=DropDownList26]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/Service.asmx/wo_no_search")%>',
                                                data: "{ 'prefix': '" + request.term + "'}",
                                                dataType: "json",
                                                type: "POST",
                                                contentType: "application/json; charset=utf-8",
                                                success: function (data) {
                                                    response($.map(data.d, function (item) {
                                                        return {
                                                            label: item.split('^')[0],

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
              
              
              
              
              
               <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" AutoPostBack="True">
                   
               </asp:DropDownList>
                <br />
                <asp:Label ID="Label412" runat="server" Font-Bold="True" ForeColor="Blue" Text="From Date:-" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" CssClass="red" Format ="dd-MM-yyyy" Enabled="True" TargetControlID="TextBox1"/>
                <br />
                <asp:Label ID="Label413" runat="server" Font-Bold="True" ForeColor="Blue" Text="To Date:-" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Format ="dd-MM-yyyy" Enabled="True" TargetControlID="TextBox2"/>
               <br />
               <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Fiscal Year:-" Width="80px"></asp:Label>
               <asp:DropDownList ID="DropDownList2" runat="server">
                   <asp:ListItem>2122</asp:ListItem>
                   <asp:ListItem>2223</asp:ListItem>
               </asp:DropDownList>
               <br />
               <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code:-" Width="80px"></asp:Label>
               <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
               <br />
               <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Blue" Text="Consig. Code:-" Width="80px"></asp:Label>
               <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
              <br />
               <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="Blue" Text="Product Code:-" Width="80px"></asp:Label>
               <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
             <br />
               <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="Blue" Text="Prod. Desc.:-" Width="80px"></asp:Label>
               <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
               <br />
               <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
            <br />
              <br />
               <div runat ="server" style ="width :100%; text-align: center;">
                    <asp:Button ID="Button1" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Send Single Data" Width="130px" CssClass="bottomstyle" />
                    <asp:Button ID="Button2" runat="server" CssClass="bottomstyle" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Send Bulk Data" Width="130px" />
               </div>
          </asp:Panel>
                </asp:View>
            
            </asp:MultiView>
            
            
            
 </div>
            </center>
</asp:Content>

