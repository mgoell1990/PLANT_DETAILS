<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="order_update.aspx.vb" Inherits="PLANT_DETAILS.order_update" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
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
          <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; line-height :20px;"  >
             
               <asp:Panel ID="Panel7" runat="server" BorderColor="Red"  BorderStyle="Groove" Font-Names="Times New Roman" Height="107px" style="text-align: left" Width="550px" CssClass="brds">
        <asp:Label ID="Label343" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="NEW ORDER" Width="100%" CssClass="brds"></asp:Label>
        <br />
        <asp:Label ID="Label344" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order Type" Width="110px"></asp:Label>
        <asp:DropDownList ID="DropDownList24" runat="server" AutoPostBack="True" Width="150px">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>Purchase Order</asp:ListItem>
            <asp:ListItem>Sale Order</asp:ListItem>
            <asp:ListItem>Work Order</asp:ListItem>
            <asp:ListItem>Rate Contract</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label340" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Despatch Type:-" Visible="False"></asp:Label>
        <asp:DropDownList ID="DropDownList23" runat="server" Visible="False" Width="150px">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>Through D.O.</asp:ListItem>
            <asp:ListItem>Direct</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="Label345" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order For" Width="110px"></asp:Label>
        <asp:DropDownList ID="DropDownList25" runat="server" Width="150px">
        </asp:DropDownList>
        <br />
       <div runat ="server" style ="float :right ">
        <asp:Button ID="Button43" runat="server" Font-Bold="True" Text="NEXT" Width="80px" CssClass="bottomstyle"  />
        <asp:Button ID="Button44" runat="server" Font-Bold="True" Text="CANCEL" Width="80px" CssClass="bottomstyle" />
           </div>
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
              <asp:Panel ID="Panel1" runat="server" BorderColor="#4686F0" BorderStyle="Double" Font-Names="Times New Roman" style="margin-right: 17px; text-align: left;" Width="1000px" Visible="False" CssClass="brds">
                  <asp:Label ID="Label270" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; " Text="SALES ORDER DETAILS:" Width="100%" CssClass="brds"></asp:Label>
                 <div style ="text-align :left; width: 708px; margin :auto; font-family: 'times New Roman', Times, serif; ">
                      <asp:Label ID="Label341" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Order No:-" Width="200px"></asp:Label>
                      <asp:TextBox ID="TextBox82" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                      <asp:Label ID="Label342" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Date:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="Delvdate6" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                      <asp:Label ID="Label346" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <br />
                      <asp:Label ID="Label5" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Actual Order No:-" Width="200px"></asp:Label>
                      <asp:TextBox ID="TextBox3" runat="server" Width="150px"></asp:TextBox>
                      <asp:Label ID="Label268" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Date:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="Delvdate1" runat="server" Width="120px"></asp:TextBox>
                      <cc1:CalendarExtender ID="Delvdate1_CalendarExtender" runat="server" BehaviorID="Delvdate1_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate1" />
              <br style="font-size: 0px" />
                      <asp:Label ID="Label6" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Quotation No:-" Width="200px"></asp:Label>
                      <asp:TextBox ID="TextBox4" runat="server" Width="150px">NA</asp:TextBox>
                      <asp:Label ID="Label7" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Date:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="Delvdate2" runat="server" Width="120px">NA</asp:TextBox>
                      <cc1:CalendarExtender ID="Delvdate2_CalendarExtender" runat="server" BehaviorID="Delvdate2_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate2" />
              <br />
                      <asp:Label ID="Label220" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="LOI No              :-" Width="200px"></asp:Label>
                      <asp:TextBox ID="TextBox6" runat="server" Width="150px">NA</asp:TextBox>
                      <asp:Label ID="Label229" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Date:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="Delvdate3" runat="server" TabIndex="5" Width="120px">NA</asp:TextBox>

                      <cc1:CalendarExtender ID="Delvdate3_CalendarExtender" runat="server" BehaviorID="Delvdate3_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate3" />

                  <br />
                      <asp:Label ID="Label218" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Enquiry No       :-" Width="200px"></asp:Label>
                      <asp:TextBox ID="TextBox54" runat="server" TabIndex="6" Width="150px">NA</asp:TextBox>
                      <asp:Label ID="Label219" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Date:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox55" runat="server" AutoCompleteType="Disabled" TabIndex="7" Width="120px">NA</asp:TextBox>
                      <cc1:CalendarExtender ID="TextBox55_CalendarExtender" runat="server" BehaviorID="TextBox55_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox55" />
                  <br />
                      <asp:Label ID="Label222" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Indent No   :-" Width="200px"></asp:Label>
                      <asp:TextBox ID="TextBox56" runat="server" TabIndex="8" Width="150px">NA</asp:TextBox>
                      <asp:Label ID="Label223" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Date:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox57" runat="server" AutoCompleteType="Disabled" TabIndex="9" Width="120px">NA</asp:TextBox>
                      <cc1:CalendarExtender ID="TextBox57_CalendarExtender" runat="server" BehaviorID="TextBox57_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox57" />
              <br />
                      <asp:Label ID="Label247" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Currency:-" Width="200px"></asp:Label>
                      <asp:TextBox ID="TextBox8" runat="server" TabIndex="10" Width="150px">INR</asp:TextBox>
                      <asp:Label ID="Label248" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Value:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="Delvdate4" runat="server" TabIndex="11" Width="120px">1</asp:TextBox>
                  <br />
                      <asp:Label ID="Label337" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Whether IPT:-" Width="200px"></asp:Label>
                      <asp:DropDownList ID="DropDownList20" runat="server" TabIndex="12" Width="151px">
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>I.P.T.</asp:ListItem>
                          <asp:ListItem>Other</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label212" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Pur Grp File:-" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox48" runat="server" TabIndex="13" Width="120px"></asp:TextBox>
                   <br />
                      <asp:Panel ID="Panel46" runat="server">
                          <asp:Label ID="Label230" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Party Code:-" Width="200px"></asp:Label>
                          <script type="text/javascript">

                        $(function () {
                            $("[id$=TextBox12]").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        url: '<%=ResolveUrl("~/Service.asmx/supl")%>',
                         data: "{ 'prefix': '" + request.term + "'}",
                         dataType: "json",
                         type: "POST",
                         contentType: "application/json; charset=utf-8",
                         success: function (data) {
                             response($.map(data.d, function (item) {
                                 return {
                                     label: item.split('^')[0],
                                     val: item.split('^')[1]
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
                     $("[id$=TextBox81]").val(i.item.label);
                 },
                 minLength: 1
             });
         });
    </script>
                          <asp:TextBox ID="TextBox12" runat="server" cssclass="textboxclass" TabIndex="14" Width="380px"></asp:TextBox>
                  <br />
                          <asp:Label ID="Label338" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Consign Code:-" Width="200px"></asp:Label>
                          <script type="text/javascript">

     $(function () {
         $("[id$=TextBox81]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/Service.asmx/supl")%>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('^')[0],
                                 val: item.split('^')[1]
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
                 $("[id$=HiddenField1]").val(i.item.val);
             },
             minLength: 1
         });
     });
    </script>
                          <asp:TextBox ID="TextBox81" runat="server" cssclass="textboxclass" TabIndex="15" Width="380px"></asp:TextBox>
                      </asp:Panel>
                      <asp:Panel ID="Panel47" runat="server">
                          <asp:Label ID="Label2" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Party Code:-" Width="200px"></asp:Label>
                          <script type="text/javascript">

                        $(function () {
                            $("[id$=TextBox7]").autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        url: '<%=ResolveUrl("~/Service.asmx/dater")%>',
                                        data: "{ 'prefix': '" + request.term + "'}",
                                        dataType: "json",
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.split('^')[0],
                                                    val: item.split('^')[1]
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
                                    $("[id$=TextBox9]").val(i.item.label);
                                },
                                minLength: 1
                            });
                        });
    </script>
                          <asp:TextBox ID="TextBox7" runat="server" cssclass="textboxclass" TabIndex="16" Width="380px"></asp:TextBox>
                  <br />
                          <asp:Label ID="Label3" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Consign Code:-" Width="200px"></asp:Label>
                          <script type="text/javascript">

     $(function () {
         $("[id$=TextBox9]").autocomplete({
             source: function (request, response) {
                 $.ajax({
                     url: '<%=ResolveUrl("~/Service.asmx/dater")%>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('^')[0],
                                 val: item.split('^')[1]
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
                 $("[id$=HiddenField1]").val(i.item.val);
             },
             minLength: 1
         });
     });
    </script>
                          <asp:TextBox ID="TextBox9" runat="server" cssclass="textboxclass" TabIndex="17" Width="380px"></asp:TextBox>
                      </asp:Panel>
                      <asp:HiddenField ID="HiddenField1" runat="server" />
              <br />
                     </div>
                  <br />
  <asp:Label ID="Label350" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="Red" Text="&lt;marquee&gt;Please do not Use &quot; ' &quot; (single quotation mark) in any field &lt;/marquee&gt;" Font-Size="Large"></asp:Label>
              <br />
                  <asp:Label ID="Label249" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="Terms &amp; Condition:" Width="100%"></asp:Label>
              <br />
              <br />
                  <asp:Label ID="Label250" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Payment Mode:-" Width="350px"></asp:Label>
                  <asp:DropDownList ID="payterm" runat="server" Height="20px" TabIndex="16" Width="150px">
                      <asp:ListItem>Select</asp:ListItem>
                      <asp:ListItem>E.Payment</asp:ListItem>
                      <asp:ListItem>Cheque</asp:ListItem>
                      <asp:ListItem>Demand Draft</asp:ListItem>
                      <asp:ListItem>Book Adjustment</asp:ListItem>
                  </asp:DropDownList>
              <br />
                  <asp:Label ID="Label253" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Payment Term:-" Width="350px"></asp:Label>
                  <asp:DropDownList ID="paymode" runat="server" Height="20px" TabIndex="18" Width="150px">
                      <asp:ListItem>100% Against GRN Within 30 Days</asp:ListItem>
                      <asp:ListItem>Against Running Bill</asp:ListItem>
                      <asp:ListItem>Advance Payment</asp:ListItem>
                  </asp:DropDownList>
                  <asp:Label ID="Label267" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Paying Agency:-" Width="150px"></asp:Label>
                  <asp:DropDownList ID="pay_agency" runat="server" TabIndex="19" Width="150px">
                      <asp:ListItem>SRU, Bhilai</asp:ListItem>
                      <asp:ListItem></asp:ListItem>
                      <asp:ListItem></asp:ListItem>
                  </asp:DropDownList>
                  <asp:Panel ID="Panel12" runat="server">
                      <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="L.D. Applicability:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="ldapplicable" runat="server" Height="20px" TabIndex="21" Width="150px">
                          <asp:ListItem>Applicable</asp:ListItem>
                          <asp:ListItem>Not Applicable</asp:ListItem>
                      </asp:DropDownList>
                  </asp:Panel>
                  <asp:Panel ID="Panel13" runat="server">
                      <asp:Label ID="Label264" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Delivery Terms:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="delvterm" runat="server" Height="20px" TabIndex="23" Width="150px">
                          <asp:ListItem>N/A</asp:ListItem>
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>By Customer</asp:ListItem>
                          <asp:ListItem>By SRU</asp:ListItem>
                          <asp:ListItem>Transporter Godown</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Mode of Despatch:-" Width="150px"></asp:Label>
                      <asp:DropDownList ID="despatch_mode" runat="server" Height="20px" TabIndex="24" Width="150px">
                          <asp:ListItem>By Road</asp:ListItem>
                          <asp:ListItem>By Train</asp:ListItem>
                          <asp:ListItem>By Ship</asp:ListItem>
                          <asp:ListItem>By Air</asp:ListItem>
                          <asp:ListItem>By Hand</asp:ListItem>
                          <asp:ListItem>By Post</asp:ListItem>
                      </asp:DropDownList>
                  </asp:Panel>
                  <asp:Panel ID="Panel14" runat="server">
                      <asp:Label ID="Label251" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Origin Station:-" Width="350px"></asp:Label>
                      <asp:TextBox ID="destinatationTextBox" runat="server" TabIndex="26" Width="150px"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel15" runat="server">
                      <asp:Label ID="Label252" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Freight Term:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="freightterm" runat="server" TabIndex="28" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Paid</asp:ListItem>
                          <asp:ListItem>Extra</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label271" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="S. Tax On Freight:-" Width="150px"></asp:Label>
                      <asp:TextBox ID="tax_on_freightTextBox" runat="server" TabIndex="29" Width="150px">0.00</asp:TextBox>
                      <asp:Label ID="Label286" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="%"></asp:Label>
                  </asp:Panel>
                  <asp:Panel ID="Panel10" runat="server">
                      <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Insurance Term:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="insurance" runat="server" AutoPostBack="True" Height="20px" TabIndex="31" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>Party Cost</asp:ListItem>
                          <asp:ListItem>Company Cost</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Insurance Rate:-" Width="150px"></asp:Label>
                      <asp:TextBox ID="insupercent_TextBox" runat="server" BackColor="SeaGreen" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px" ReadOnly="True" TabIndex="32" Width="150px">0.00</asp:TextBox>
                      <asp:DropDownList ID="insurancetype" runat="server" TabIndex="33">
                          <asp:ListItem>PERCENTAGE</asp:ListItem>
                          <asp:ListItem>AMOUNT</asp:ListItem>
                      </asp:DropDownList>
                  </asp:Panel>
                  <asp:Panel ID="Panel16" runat="server">
                      <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Misc. Charge:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="misccharg_ComboBox" runat="server" AutoPostBack="True" Height="20px" TabIndex="35" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label272" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="S. Tax On MisChrg:-" Width="150px"></asp:Label>
                      <asp:TextBox ID="misc_tax_ComboBox4" runat="server" TabIndex="36" Width="150px">0.00</asp:TextBox>
                      %
                  </asp:Panel>
                  <asp:Panel ID="Panel17" runat="server">
                      <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Inspection Term:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="inspeterm_ComboBox" runat="server" Height="20px" TabIndex="38" Width="150px">
                          <asp:ListItem>N/A</asp:ListItem>
                          <asp:ListItem>By Vendor</asp:ListItem>
                          <asp:ListItem>By TC/GC</asp:ListItem>
                          <asp:ListItem>By Third Party</asp:ListItem>
                          <asp:ListItem>By SRU Bhilai</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label282" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Third Party Inspection:-" Width="150px"></asp:Label>
                      <asp:DropDownList ID="third_party_insp" runat="server" Height="20px" TabIndex="39" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                  </asp:Panel>
                  <asp:Panel ID="Panel18" runat="server">
                      <asp:Label ID="Label256" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Inespection/Testing/Quality Plan :-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="insp_test_ComboBox4" runat="server" Height="20px" TabIndex="41" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:TextBox ID="insp_test_TextBox25" runat="server" cssclass="mymultitextboxclass" TabIndex="42" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel19" runat="server">
                      <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="PVC Clause:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="pvc_ComboBox" runat="server" Height="20px" TabIndex="43" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:TextBox ID="pvc_TextBox26" runat="server" cssclass="mymultitextboxclass" TabIndex="44" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel20" runat="server">
                      <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Penality Clause:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="bonus_ComboBox" runat="server" Height="20px" TabIndex="45" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:TextBox ID="bonus_TextBox27" runat="server" cssclass="mymultitextboxclass" TabIndex="46" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel21" runat="server">
                      <asp:Label ID="Label257" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Performance Evalutation Clause:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="performance_ComboBox0" runat="server" Height="20px" TabIndex="47" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:TextBox ID="performance_TextBox28" runat="server" cssclass="mymultitextboxclass" TabIndex="48" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel22" runat="server">
                      <asp:Label ID="Label258" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Performance Guarantee:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="per_gurrenty_ComboBox1" runat="server" Height="20px" TabIndex="49" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:TextBox ID="per_gurrenty_TextBox29" runat="server" cssclass="mymultitextboxclass" TabIndex="50" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel23" runat="server">
                      <asp:Label ID="Label259" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Security Deposit:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="sd_ComboBox2" runat="server" AutoPostBack="True" Height="20px" TabIndex="51" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>CASH</asp:ListItem>
                          <asp:ListItem>BANK GUARANTEE</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label284" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="S.D. Percentage:-" Width="150px"></asp:Label>
                      <asp:TextBox ID="sd_TextBox46" runat="server" TabIndex="52" Width="150px">0.00</asp:TextBox>
                      <asp:Label ID="Label285" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="%"></asp:Label>
                  </asp:Panel>
                  <asp:Panel ID="Panel24" runat="server">
                      <asp:Label ID="Label260" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Special Gurantee / Warranty Clause :-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="sp_gur_ComboBox3" runat="server" Height="20px" TabIndex="54" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:TextBox ID="sp_gur_TextBox31" runat="server" cssclass="mymultitextboxclass" TabIndex="55" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel25" runat="server">
                      <asp:Label ID="Label261" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Matching Part Details:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="match_ComboBox2" runat="server" Height="20px" TabIndex="56" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                  </asp:Panel>
                  <asp:Panel ID="Panel26" runat="server">
                      <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Quantity Variation Clause:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="quantity_ComboBox" runat="server" Height="20px" TabIndex="58" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                  </asp:Panel>
                  <asp:Panel ID="Panel27" runat="server">
                      <asp:Label ID="Label262" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Special Terms Related To Supply Of Medicines:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="medicine_ComboBox0" runat="server" Height="20px" TabIndex="60" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                  </asp:Panel>
                  <asp:Panel ID="Panel28" runat="server">
                      <asp:Label ID="spl_del_Label263" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Special Instruction:-" Width="350px"></asp:Label>
                      <asp:DropDownList ID="spl_del_ComboBox1" runat="server" Height="20px" TabIndex="62" Width="150px">
                          <asp:ListItem>Not Applicable</asp:ListItem>
                          <asp:ListItem>Applicable</asp:ListItem>
                      </asp:DropDownList>
                      <br />
                      <asp:TextBox ID="spl_del_TextBox35" runat="server" cssclass="mymultitextboxclass" TabIndex="63" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel29" runat="server">
                      <asp:Label ID="Label239" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Documents to be submitted along with material supply" Width="350px"></asp:Label>
                      <asp:TextBox ID="doc_sub_m_supl_TextBox49" runat="server" cssclass="mymultitextboxclass" TabIndex="64" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel30" runat="server">
                      <asp:Label ID="Label240" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Documents to be submitted along with bill of payment" Width="350px"></asp:Label>
                      <asp:TextBox ID="doc_bill_pay_TextBox50" runat="server" cssclass="mymultitextboxclass" TabIndex="65" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Panel ID="Panel31" runat="server">
                      <asp:Label ID="Label241" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" style="text-align: left" Text="Invoicing Party / payment to be made to" Width="350px"></asp:Label>
                      <asp:TextBox ID="inv_party_TextBox51" runat="server" cssclass="mymultitextboxclass" TabIndex="66" TextMode="MultiLine"></asp:TextBox>
                  </asp:Panel>
                  <asp:Label ID="Label242" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="ANNEXURE:" Width="100%"></asp:Label>
              <br />
                  <asp:Label ID="Label243" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="General terms and conditions" Width="200px"></asp:Label>
              <br />
                  <asp:TextBox ID="general_term_TextBox52" runat="server" cssclass="notetextboxclass" TabIndex="67" TextMode="MultiLine"></asp:TextBox>
              <br />
                  <asp:Label ID="Label244" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Blue" style="text-align: left" Text="NOTE:-"></asp:Label>
              <br />
                  <asp:TextBox ID="note_TextBox53" runat="server" cssclass="notetextboxclass" TabIndex="68" TextMode="MultiLine"></asp:TextBox>
              <br />
              <br />
                  <asp:Panel ID="Panel2" runat="server" Height="51px" style="text-align: center">
                      <asp:Button ID="Button33" runat="server" CssClass="bottomstyle" Font-Bold="True" Font-Size="Medium" TabIndex="69" Text="SAVE" Width="150px" />
                      <asp:Button ID="Button32" runat="server" CssClass="bottomstyle" Font-Bold="True" Font-Size="Medium" TabIndex="70" Text="CANCEL" Width="150px" />
                  </asp:Panel>
              </asp:Panel>
               </div> 
        </center> 
</asp:Content>
