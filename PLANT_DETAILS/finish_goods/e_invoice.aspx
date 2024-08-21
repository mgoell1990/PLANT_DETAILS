<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="e_invoice.aspx.vb" Inherits="PLANT_DETAILS.e_invoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
        <script>
            if (window.history.replaceState) {
                window.history.replaceState(null, null, window.location.href);
            }
</script>
        <%# Eval("ITEM_CODE")%>
        <div runat ="server" style ="min-height :600px;">
           
             <asp:Panel ID="Panel9" runat="server" Font-Names="Times New Roman" Width="100%" style="text-align: left">
             <asp:Label ID="Label53" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center;line-height :30px;" Text="REPORT" Height ="30px" Width="100%"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label54" runat="server" ForeColor="Blue" Text="Search For"></asp:Label>
                    <asp:DropDownList ID="DropDownList9" runat="server" Width="125px" AutoPostBack="True">

                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Cancel E-invoice</asp:ListItem>
                        <asp:ListItem>Generate E-way bill from IRN</asp:ListItem>
                        <asp:ListItem>Cancel E-way bill</asp:ListItem>
                        <asp:ListItem>Update E-way bill</asp:ListItem>
                        <asp:ListItem>Extend E-way bill</asp:ListItem>
                        
                    </asp:DropDownList>

                 <asp:MultiView ID="MultiView2" runat="server">
                     <%--=====VIEW 1 Cancel E-invoice START=====--%>
                     <asp:View ID="View1" runat="server">

                        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">

            <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" ScrollBars="Auto" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" CssClass="brds">
                <asp:Label ID="Label483" runat="server" BackColor="#4686F0" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Height="40px" style="text-align: center; line-height:40px" Text="Cancel E-invoice" Width="100%"></asp:Label>
                <br />
                <br />
                
                <asp:Label ID="Label98" runat="server" Text="Fiscal Year" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="True" Width="125px">
                    
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Invoice No" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList10" runat="server" Width="125px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label8" runat="server" Text="IRN No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox24" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label71" runat="server" Text="Invoice Status" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox28" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Party Code" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox25" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label4" runat="server" Text="Party Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox29" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label72" runat="server" Text="Consignee Code" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox35" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label74" runat="server" Text="Consignee Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox48" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label5" runat="server" Text="Invoice Amount" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox30" runat="server" Width="120px"></asp:TextBox>
                 <br />
                <asp:Label ID="Label55" runat="server" Text="Cancellation Reason" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList2" runat="server" Width="125px" AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="Label57" runat="server" Text="Cancellation Remarks" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox31" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                &nbsp;<asp:Label ID="Label554" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button47" runat="server" ForeColor="Blue" Text="Cancel E-invoice" Width="150px" CssClass="bottomstyle" Font-Size="Small" />
                <br />
                &nbsp;<asp:Label ID="Label58" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label552" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label62" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label553" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>

            </div>

                        
                     </asp:View>

                     <%--=====VIEW 2 GENERATE E-WAY BILL FROM IRN=====--%>
                     <asp:View ID="View2" runat="server">

                        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">

            <asp:Panel ID="Panel4" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" ScrollBars="Auto" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" CssClass="brds">
                <asp:Label ID="Label59" runat="server" BackColor="#4686F0" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Height="40px" style="text-align: center; line-height:40px" Text="Generate E-way bill from IRN" Width="100%"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label60" runat="server" Text="E-Way Bill No" Width="100px" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox36" runat="server" Width="120px" ReadOnly="True"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label75" runat="server" Text="Validity" Width="100px" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox46" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label69" runat="server" Text="Fiscal Year" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Width="125px">
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label61" runat="server" Text="Invoice No" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList8" runat="server" Width="125px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label63" runat="server" Text="IRN No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox26" runat="server" Width="400px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label64" runat="server" Text="Invoice Status" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox27" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label65" runat="server" Text="Party Code" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox32" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label66" runat="server" Text="Party Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox33" runat="server" Width="400px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label67" runat="server" Text="Consignee Code" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox34" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label76" runat="server" Text="Consignee Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox47" runat="server" Width="400px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" ForeColor="Blue" Text="Generate E-way bill" Width="150px" CssClass="bottomstyle" Font-Size="Small" />
                <br />
                &nbsp;<asp:Label ID="Label68" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label70" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="lblEwaybillFromIRNErrorCode" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label73" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="lblEwaybillFromIRNErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>



            </div>

                        
                     </asp:View>

                     <%--=====VIEW 3 ADVANCE VOUCHER START=====--%>
                     <asp:View ID="View3" runat="server">

                        <%--<div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
            <asp:Panel ID="Panel2" runat="server" BorderColor="#00CC00" BorderStyle="Double" Width="1000px" Font-Names="Times New Roman" style="text-align: left" CssClass="brds" Visible="False">
              <asp:Label ID="Label280" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Overline="False" Font-Size="X-Large" ForeColor="White" style="text-align: center ; line-height :40px" Text="INVOICING OF FINISH GOODS" Height ="40px" Width="100%" CssClass="brds"></asp:Label>
              
                <br />
               <div runat ="server" style="border-bottom: 10px double #008000; height:315px; width: 100%;" >
                   <div runat ="server" style="width: 100%;" >
                   &nbsp;<asp:Label ID="Label30" runat="server" Text="IRN No" Width="100px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox6" runat="server" Width="440px" ReadOnly="True"></asp:TextBox>
                   &nbsp;<asp:Label ID="Label48" runat="server" Text="E-Way Bill No" Width="85px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox8" runat="server" Width="130px" ReadOnly="True"></asp:TextBox>
                   &nbsp;<asp:Label ID="Label52" runat="server" Text="Validity" Width="65px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox20" runat="server" Width="140px" ReadOnly="True"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label299" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Invoice No" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox177" runat="server" Width="75px"></asp:TextBox>
                   <asp:TextBox ID="TextBox65" runat="server" Width="75px"></asp:TextBox>
                   &nbsp;<br /> &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="S.O. No" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox124" runat="server" BackColor="Red" ForeColor="White" Width="150px"></asp:TextBox>
                   <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Buyer Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                   <br />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label51" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Consignee Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox18" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox19" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label287" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Transporter" Width="100px"></asp:Label>
                   <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Height="16px" Width="152px">
                   </asp:DropDownList>
                   &nbsp;<asp:Label ID="Label296" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Transp. Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox75" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox62" runat="server" BackColor="Red" Enabled="False" ForeColor="White" Width="300px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label463" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="WO Sl No" Width="100px"></asp:Label>
                   <asp:DropDownList ID="DropDownList27" runat="server" AutoPostBack="True" Width="152px">
                   </asp:DropDownList>
                   &nbsp;<asp:Label ID="Label464" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox174" runat="server" BackColor="Red" CssClass="worktextboxclass" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="432px"></asp:TextBox>
                   <br />
                   <div runat="server" style="width: 541px ; float :left">
                       &nbsp;<asp:Label ID="Label288" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Truck No" Width="100px"></asp:Label>
                       <script type="text/javascript">

                           $(function () {
                               $("[id$=TextBox55]").autocomplete({
                                   source: function (request, response) {
                                       $.ajax({
                                           url: '<%=ResolveUrl("~/Service.asmx/T_NO")%>',
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
                       <asp:TextBox ID="TextBox55" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label309" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="RR No" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox69" runat="server" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label293" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Form to Receive" Width="100px"></asp:Label>
                       <asp:DropDownList ID="DropDownList7" runat="server" Font-Names="Times New Roman" Width="152px">
                           <asp:ListItem>N/A</asp:ListItem>
                           <asp:ListItem>F Form</asp:ListItem>
                           <asp:ListItem>C Form</asp:ListItem>
                       </asp:DropDownList>
                       <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Purity %" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox1" runat="server" Width="150px">0.00</asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label297" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="DA No" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox63" runat="server" Width="150px"></asp:TextBox>
                       <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="TC No" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox2" runat="server" Width="150px">N/A</asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Mill Code" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox3" runat="server" Width="150px">N/A</asp:TextBox>
                       <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Route Card No" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox4" runat="server" Width="150px">N/A</asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Rcd Voucher No" Width="100px"></asp:Label>
                       <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="152px">
                       </asp:DropDownList>
                       <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Bal. Amount" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox9" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label468" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Rcd Voucher Date" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox176" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                       <br />
                       &nbsp;<asp:Label ID="Label298" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Notification" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox64" runat="server" Cssclass="notetextboxclass" TextMode="MultiLine" Width="382px"></asp:TextBox>
                       <br />
                   </div>
                   <div runat="server" style="width: 455px ; float :right; height: 118px;">
                       <asp:Label ID="Label28" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="RR Date" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox5" runat="server" Width="100px">N/A</asp:TextBox>
                       <br />
                       <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Rly. Inv No" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox10" runat="server" Width="100px">N/A</asp:TextBox>
                       <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Date" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox11" runat="server" Width="100px">N/A</asp:TextBox>
                       <br />
                       <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Rly. Frt/ Unit." Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox12" runat="server" Width="100px">0.00</asp:TextBox>
                       <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Rly. Frt Amt." Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox13" runat="server" Width="100px">0.00</asp:TextBox>
                       <br />
                       <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="D1" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox14" runat="server" Width="100px"></asp:TextBox>
                       <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="D2" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox15" runat="server" Width="100px"></asp:TextBox>
                       <br />
                       <asp:Label ID="Label40" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="D3" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox16" runat="server" Width="100px"></asp:TextBox>
                       <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="D4" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox17" runat="server" Width="100px"></asp:TextBox>
                   </div>

                 
               </div> 
                   </div>
                <br />
               <div style ="float :right; width :50%; height: 188px; margin-left: 0px;" runat ="server" >
                         
                         <asp:FormView ID="FormView1" runat="server" Font-Names="Times New Roman" Width="100%" Height="102%" BorderColor="Red" BorderStyle="Double">
                             <ItemTemplate>
                                 <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF;"> <%# Eval("ITEM_CODE")%> </h3>
                                <h1 >===============================================</h1>
                                  <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Item Desc" Width="100"></asp:Label>
                                 <asp:Label ID="Label15" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label3" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                 <br />
                                 <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Item A/U" Width="100"></asp:Label>
                                 <asp:Label ID="Label16" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label5" runat="server" Text='<%# Eval("ITEM_AU")%>' Width="120"></asp:Label>
                                 <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Item U/W" Width="100"></asp:Label>
                                 <asp:Label ID="Label17" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label7" runat="server" Text='<%# Eval("ITEM_WEIGHT")%>'></asp:Label>
                                 <asp:Label ID="Label23" runat="server" Text="(Kg)"></asp:Label>
                                  <br />
                                 <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Ord. Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label18" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label9" runat="server" Text='<%# Eval("ITEM_QTY")%>' Width="120"></asp:Label>
                                 <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Ord. Bal Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label19" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label11" runat="server" Text='<%# Eval("ITEM_BAL_QTY")%>' Width="120"></asp:Label>
                                 <br />
                                 <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Unit In Stock" Width="100"></asp:Label>
                                 <asp:Label ID="Label20" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label13" runat="server" Text='<%# Eval("ITEM_F_STOCK")%>' Width="120"></asp:Label>
                                 <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="Unit In Stock" Width="100"></asp:Label>
                                 <asp:Label ID="Label21" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label22" runat="server" Text='<%# Eval("ITEM_F_STOCK_MT")%>'></asp:Label>
                                 <asp:Label ID="Label29" runat="server" Text="(Mt)"></asp:Label>
                                 <br />
                             </ItemTemplate>
                         </asp:FormView>
               </div> 
               <div style ="float :left; width :50%; margin-left: 0px;" runat ="server" >
                   <asp:Label ID="Label283" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No" Width="120px"></asp:Label>
              <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="152px">
              </asp:DropDownList>
              &nbsp;<asp:Label ID="Label284" runat="server" Font-Bold="True" ForeColor="Blue" Text="Vocab No" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox53" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="147px"></asp:TextBox>
                <br />
                <asp:Label ID="Label306" runat="server" Font-Bold="True" ForeColor="Blue" Text="Ammed No" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox67" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                <asp:Label ID="Label307" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox68" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="147px"></asp:TextBox>
                <br />
                <asp:Label ID="Label285" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Code" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Width="152px">
                </asp:DropDownList>
                <asp:Label ID="Label467" runat="server" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label286" runat="server" Font-Bold="True" ForeColor="Blue" Text="Despatch Qty." Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox54" runat="server" Width="150px"></asp:TextBox>
                <asp:Label ID="Label289" runat="server" Font-Bold="True" ForeColor="Blue" Width="65px"></asp:Label>
                <br />
                <asp:Label ID="Label290" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total Pcs/Bags" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox56" runat="server" Width="150px"></asp:TextBox>
                <asp:Label ID="Label291" runat="server" Font-Bold="True" ForeColor="Blue" Text="Lot No" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox57" runat="server" Width="147px"></asp:TextBox>
                <br />
                <asp:Label ID="Label47" runat="server" Font-Bold="True" ForeColor="Blue" Text="Transporter Wt.(MT)" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server" Width="150px"></asp:TextBox>
                   
                <br />
                &nbsp;<asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label31" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="txtEinvoiceErrorCode" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label42" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="txtEinvoiceErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
              &nbsp;&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="NEW" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
                <asp:Button ID="Button37" runat="server" Text="CANCEL" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button35" runat="server" Text="ADD" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button36" runat="server" Text="SAVE" Width="75px" Font-Size="Small" CssClass="bottomstyle" Enabled="False" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                   </div> 
              <br />
              <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BorderColor="#00CC00" BorderStyle="Double" BorderWidth="5px" CellPadding="3" Font-Names="Times New Roman" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="100%">
                  <AlternatingRowStyle BackColor="#DCDCDC" />
                  <Columns>
                      <asp:BoundField DataField="mat_sl_no" HeaderText="Sl No" />
                      <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                      <asp:BoundField DataField="MAT_NAME" HeaderText="Item Name" />
                      <asp:BoundField DataField="ITEM_AU" HeaderText="A / U" />
                      <asp:BoundField DataField="ITEM_QTY_PCS" HeaderText="Item Qty (Pcs)" />
                      <asp:BoundField DataField="UNIT_WEIGHT" HeaderText="Unit Weight(Kg.)" />
                      <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Item Qty (Mt)" />
                      <asp:BoundField DataField="Packing Details" HeaderText="Packing Details" />
                      <asp:BoundField DataField="ASS_VALUE" HeaderText="Total Base Value" />
                  </Columns>
                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                  <HeaderStyle BackColor="#FF0066" BorderColor="Blue" BorderStyle="Double" BorderWidth="2px" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                  <RowStyle BackColor="#EEEEEE" BorderColor="Lime" BorderStyle="Ridge" ForeColor="Black" />
                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                  <SortedDescendingHeaderStyle BackColor="#000065" />
              </asp:GridView>
              <asp:Label ID="Label43" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Disc Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox37" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Taxable Value" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox38" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
               <br />
              <asp:Label ID="Label44" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Freight" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox39" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label50" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="SGST" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox40" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label45" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="P&amp;F Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox41" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label300" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="CGST" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox42" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label46" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="T.Tax Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox43" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label301" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="IGST" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox44" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label305" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="TCS Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox66" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label304" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Cess" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox21" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="Label302" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Red" style="text-align: left" Text="Net Payable" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox45" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
            </asp:Panel>
            <asp:Panel ID="Panel8" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" Height="146px" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label401" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="SALE ORDER SELECTION" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="IRN No:-" Width="80px"></asp:Label>
             
                            <script type="text/javascript">
                                $(function () {
                                    $("[id$=DropDownList26]").autocomplete({
                                        source: function (request, response) {
                                            $.ajax({
                                                url: '<%=ResolveUrl("~/Service.asmx/S_SO")%>',
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
              
              
              
              
              
               <asp:TextBox ID="DropDownList26" runat="server" Width="300px" Font-Names="Times New Roman" Font-Size="Small"></asp:TextBox>
              <br />
              <asp:Label ID="Label403" runat="server" Text="&lt;marquee&gt;Purchase/Sales/Work Order Codes will be &quot;P&quot; for Purchase, &quot;S&quot; for Sales, &quot;W&quot; for Work Order and then &quot;01&quot; for Store Material &quot;02&quot; for Raw Material &quot;04&quot; for Miscellaneous  &quot;05&quot; for Finished Goods&lt;/marquee&gt;" ForeColor="Red"></asp:Label>
              <br />
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <br />
              &nbsp;<asp:Button ID="Button45" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="ADD" Width="130px" CssClass="bottomstyle" />
              <asp:Button ID="Button46" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CANCEL" Width="130px" CssClass="bottomstyle" />
              <br />
          </asp:Panel>
 </div>--%>

                         <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">

            <asp:Panel ID="Panel2" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" ScrollBars="Auto" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" CssClass="brds">
                <asp:Label ID="Label9" runat="server" BackColor="#4686F0" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Height="40px" style="text-align: center; line-height:40px" Text="Cancel E-way bill" Width="100%"></asp:Label>
                <br />
                <br />
                
                <asp:Label ID="Label10" runat="server" Text="Fiscal Year" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="125px">
                    
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label11" runat="server" Text="Invoice No" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList4" runat="server" Width="125px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label12" runat="server" Text="IRN No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label13" runat="server" Text="Invoice Status" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label18" runat="server" Text="E-way bill Status" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox7" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label14" runat="server" Text="Party Code" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox3" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label15" runat="server" Text="Party Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox4" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label16" runat="server" Text="E-way bill No." Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox5" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label17" runat="server" Text="E-way bill validity" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox6" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label19" runat="server" Text="Cancellation Reason" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList5" runat="server" Width="125px" AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="Label20" runat="server" Text="Cancellation Remarks" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox8" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                &nbsp;<asp:Label ID="Label21" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" ForeColor="Blue" Text="Cancel E-way bill" Width="150px" CssClass="bottomstyle" Font-Size="Small" />
                <br />
                &nbsp;<asp:Label ID="Label22" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label23" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label24" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label25" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>

            </div>

</asp:View>

                     <%--=====VIEW 4 BANK BOOK START=====--%>
                     <asp:View ID="View4" runat="server">

                        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">

            <asp:Panel ID="Panel3" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" ScrollBars="Auto" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" CssClass="brds">
                <asp:Label ID="Label6" runat="server" BackColor="#4686F0" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Height="40px" style="text-align: center; line-height:40px" Text="Update Part-B of E-way bill" Width="100%"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label7" runat="server" Text="E-Way Bill No" Width="100px" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox9" runat="server" Width="120px" ReadOnly="True"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label26" runat="server" Text="Validity" Width="100px" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                <asp:TextBox ID="TextBox10" runat="server" Width="200px" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label27" runat="server" Text="Fiscal Year" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Width="125px">
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label28" runat="server" Text="Invoice No" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList7" runat="server" Width="125px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label29" runat="server" Text="IRN No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox11" runat="server" Width="400px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label561" runat="server" Text="E-way bill No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox54" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label562" runat="server" Text="E-way bill validity" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox55" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="450px"></asp:TextBox>
                <br />
                <asp:Label ID="Label30" runat="server" Text="Invoice Status" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox12" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label563" runat="server" Text="E-way bill Status" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox56" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                <br />
                <asp:Label ID="Label555" runat="server" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Old Truck No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox49" runat="server" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label556" runat="server" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="New Truck No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox50" runat="server" Width="120px"></asp:TextBox>
                <br />
                <asp:Label ID="Label31" runat="server" Text="Party Code" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox13" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label32" runat="server" Text="Party Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox14" runat="server" Width="400px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label33" runat="server" Text="Consignee Code" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox15" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label34" runat="server" Text="Consignee Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox16" runat="server" Width="400px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label557" runat="server" Text="Updation Reason" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList18" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;<asp:Label ID="Label558" runat="server" Text="Updation Remarks" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox51" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                <br />
                <asp:Label ID="Label559" runat="server" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="From State" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList19" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>01 , JAMMU AND KASHMIR</asp:ListItem>
<asp:ListItem>02 , HIMACHAL PRADESH</asp:ListItem>
<asp:ListItem>03 , PUNJAB</asp:ListItem>
<asp:ListItem>04 , CHANDIGARH</asp:ListItem>
<asp:ListItem>05 , UTTARAKHAND</asp:ListItem>
<asp:ListItem>06 , HARYANA</asp:ListItem>
<asp:ListItem>07 , DELHI</asp:ListItem>
<asp:ListItem>08 , RAJASTHAN</asp:ListItem>
<asp:ListItem>09 , UTTAR PRADESH</asp:ListItem>
<asp:ListItem>10 , BIHAR</asp:ListItem>
<asp:ListItem>11 , SIKKIM</asp:ListItem>
<asp:ListItem>12 , ARUNACHAL PRADESH</asp:ListItem>
<asp:ListItem>13 , NAGALAND</asp:ListItem>
<asp:ListItem>14 , MANIPUR</asp:ListItem>
<asp:ListItem>15 , MIZORAM</asp:ListItem>
<asp:ListItem>16 , TRIPURA</asp:ListItem>
<asp:ListItem>17 , MEGHLAYA</asp:ListItem>
<asp:ListItem>18 , ASSAM</asp:ListItem>
<asp:ListItem>19 , WEST BENGAL</asp:ListItem>
<asp:ListItem>20 , JHARKHAND</asp:ListItem>
<asp:ListItem>21 , ODISHA</asp:ListItem>
<asp:ListItem>22 , CHATTISGARH</asp:ListItem>
<asp:ListItem>23 , MADHYA PRADESH</asp:ListItem>
<asp:ListItem>24 , GUJARAT</asp:ListItem>
<asp:ListItem>26 , DADRA AND NAGAR HAVELI</asp:ListItem>
<asp:ListItem>27 , MAHARASHTRA</asp:ListItem>
<asp:ListItem>28 , ANDHRA PRADESH(BEFORE DIVISION)</asp:ListItem>
<asp:ListItem>29 , KARNATAKA</asp:ListItem>
<asp:ListItem>30 , GOA</asp:ListItem>
<asp:ListItem>31 , LAKSHWADEEP</asp:ListItem>
<asp:ListItem>32 , KERALA</asp:ListItem>
<asp:ListItem>33 , TAMIL NADU</asp:ListItem>
<asp:ListItem>34 , PUDUCHERRY</asp:ListItem>
<asp:ListItem>35 , ANDAMAN AND NICOBAR ISLANDS</asp:ListItem>
<asp:ListItem>36 , TELANGANA</asp:ListItem>
<asp:ListItem>37 , ANDHRA PRADESH (NEWLY ADDED)</asp:ListItem>
<asp:ListItem>38 , LADAKH (NEWLY ADDED)</asp:ListItem>


                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label560" runat="server" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="From place" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox53" runat="server" Width="120px"></asp:TextBox>
                <br />
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button3" runat="server" ForeColor="Blue" Text="Update E-way bill" Width="150px" CssClass="bottomstyle" Font-Size="Small" />
                <br />
                &nbsp;<asp:Label ID="Label35" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label36" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label37" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label38" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label39" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>



            </div>

                        
                     </asp:View>

                     <%--=====VIEW 5 Extend E-Way Bill START=====--%>
                     <asp:View ID="View5" runat="server">

                        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">

            <asp:Panel ID="Panel5" runat="server" BorderColor="Blue" BorderStyle="Groove" style="text-align: left" ScrollBars="Auto" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" CssClass="brds">
                <asp:Label ID="Label40" runat="server" BackColor="#4686F0" CssClass="brds" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Height="40px" style="text-align: center; line-height:40px" Text="Extend E-way Bill" Width="100%"></asp:Label>
                <br />
                <br />
                
                <asp:Label ID="Label41" runat="server" Text="Fiscal Year" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True" Width="125px">
                    
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label42" runat="server" Text="Invoice No" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList12" runat="server" Width="125px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;
                <asp:Label ID="Label43" runat="server" Text="IRN No." Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox17" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label44" runat="server" Text="Invoice Status" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox18" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label45" runat="server" Text="Party Code" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox19" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label46" runat="server" Text="Party Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox20" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label47" runat="server" Text="Consignee Code" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox21" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                &nbsp;
                <asp:Label ID="Label48" runat="server" Text="Consignee Name" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox22" runat="server" Width="450px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                <asp:Label ID="Label49" runat="server" Text="Invoice Amount" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox23" runat="server" Width="120px"></asp:TextBox>
                 <br />
                <asp:Label ID="Label50" runat="server" Text="Cancellation Reason" Width="120px"></asp:Label>
                <asp:DropDownList ID="DropDownList13" runat="server" Width="125px" AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="Label51" runat="server" Text="Cancellation Remarks" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox37" runat="server" Width="120px" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                <br />
                &nbsp;<asp:Label ID="Label52" runat="server" ForeColor="Red"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button4" runat="server" ForeColor="Blue" Text="Cancel E-invoice" Width="150px" CssClass="bottomstyle" Font-Size="Small" />
                <br />
                &nbsp;<asp:Label ID="Label56" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label77" runat="server" ForeColor="Red"></asp:Label>
                <br />
                &nbsp;<asp:Label ID="Label78" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                &nbsp;<asp:Label ID="Label79" runat="server" ForeColor="Red"></asp:Label>
                <br />
            </asp:Panel>



            </div>

                        
                     </asp:View>


                     

                     </asp:MultiView>

            </asp:Panel>

            <br />
            </div>
        <%--'''''''''''''''''--%>
         
        </center>

</asp:Content>

