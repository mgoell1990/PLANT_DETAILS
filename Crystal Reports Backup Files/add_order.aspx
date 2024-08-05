<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostback ="true"  AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="add_order.aspx.vb" Inherits="PLANT_DETAILS.add_order" %>
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
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
            <asp:Panel ID="Panel8" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" Height="171px" style="text-align: left" Width="389px" CssClass="brds">
         <asp:Label ID="Label347" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="ORDER WISE MATERIAL/WORK  ADD" Width="100%" Font-Size="Medium" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label348" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox832" runat="server" Width="250px"></asp:TextBox>
              <script type="text/javascript">

                  $(function () {
                      $("[id$=TextBox832]").autocomplete({
                          source: function (request, response) {
                              $.ajax({
                                  url: '<%=ResolveUrl("~/Service.asmx/po_no_search")%>',
                           data: "{ 'prefix': '" + request.term + "'}",
                           dataType: "json",
                           type: "POST",
                           contentType: "application/json; charset=utf-8",
                           success: function (data) {
                               response($.map(data.d, function (item) {
                                   return {
                                       label: item.split('^')[0]
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

                <br />
              <br />
              <asp:Label ID="Label349" runat="server" ForeColor="Red" Text="&lt;marquee&gt;Purchase/Sales/Work Order Codes will be &quot;P&quot; for Purchase, &quot;S&quot; for Sales, &quot;W&quot; for Work Order and then &quot;01&quot; for Store Material &quot;02&quot; for Raw Material &quot;04&quot; for Miscellaneous  &quot;05&quot; for Finished Goods&lt;/marquee&gt;" Font-Size="Small"></asp:Label>
              <br />
              <br />
                <div style="text-align: center">
                     <asp:Button ID="Button45" runat="server" Font-Bold="True" Font-Names="Times New Roman" Text="ADD" Width="130px" CssClass="bottomstyle" />
         <asp:Button ID="Button46" runat="server" Font-Bold="True" Font-Names="Times New Roman" Text="CANCEL" Width="130px" CssClass="bottomstyle" />
                </div>
            
              <br />
          </asp:Panel>
            <br />
            
             <asp:Panel ID="panel_mat" runat="server" BorderColor="Blue" BorderStyle="Ridge" BorderWidth="2px" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="1000px" CssClass="brds" Font-Size="Medium">
                 <asp:Label ID="Label351" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="MATERIAL INFORMATION" Width="100%" Height="29px" Font-Size="X-Large" CssClass="brds"></asp:Label>
                 <div style="float :right; height: 140px;  margin-top: 36px;">
                     <asp:Button ID="Button60" runat="server" Font-Bold="True" Text="SUBMIT" Width="100px" CssClass="bottomstyle" Font-Size="Small" />
                    <br />
                      <asp:Button ID="Button49" runat="server" Font-Bold="True" Text="SAVE" Width="100px" CssClass="bottomstyle" Font-Size="Small" />
                    <br />
                      <asp:Button ID="Button48" runat="server" Font-Bold="True" Text="CANCEL" Width="100px" CssClass="bottomstyle" Font-Size="Small" />
                 </div>

                  <div style="float :left; height: 140px;  margin-top: 36px;">
                 &nbsp;<asp:Label ID="Label387" runat="server" Font-Bold="True" ForeColor="Blue" Text="P.O. / Ref. No" Width="110px"></asp:Label>
                      <asp:TextBox ID="TextBox102" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                      <asp:Label ID="Label388" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                      <asp:TextBox ID="TextBox103" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
                      <asp:Label ID="Label439" runat="server" ForeColor="Red"></asp:Label>
                      <br />
                 &nbsp;<asp:Label ID="Label390" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code" Width="110px"></asp:Label>
                      <asp:TextBox ID="TextBox105" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                      <asp:Label ID="Label391" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name" Width="65px"></asp:Label>
                      <asp:TextBox ID="TextBox106" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                      <br />
                      <br />
                      &nbsp;</div>
        <br />&nbsp;&nbsp;
        <br />
                 <asp:Label ID="Label384" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Width="100%" Font-Size="Medium">MATERIAL DETAILS</asp:Label>
        <br />
                  <div runat ="server" style ="float :right ">
                    <br />
                       <br />
                       <br />
                       <br />
                       <asp:Button ID="Button76" runat="server" Font-Bold="True" Text="DELETE" Width="100px" CssClass="bottomstyle" Font-Size="Small" />
               <br />
                      <asp:Button ID="Button75" runat="server" Font-Bold="True" Text="CORECTION" Width="100px" CssClass="bottomstyle" Font-Size="Small" />
                       </div>



                 <asp:Label ID="Label628" runat="server" Text="Mat. SLNo" Width="160px"></asp:Label>
                 <asp:DropDownList ID="DropDownList58" runat="server" Width="160px">
                 </asp:DropDownList>
        <br />
                 <asp:Label ID="Label352" runat="server"  ForeColor="Blue" Text="Material Code &amp; Name" Width="160px"></asp:Label>
                 <script type="text/javascript">

           $(function () {
               $("[id$=po_matcodecombo0]").autocomplete({
                   source: function (request, response) {
                       $.ajax({
                           url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                                        data: "{ 'prefix': '" + request.term + "'}",
                                        dataType: "json",
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.split('^')[0],
                                                    val: item.split('^')[1],
                                                    MAT_AU: item.split('^')[2]
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
                                    $("[id$=TextBox815]").val(i.item.MAT_AU);
                                },
                                minLength: 1
                            });
                        });
    </script>
                 <asp:TextBox ID="po_matcodecombo0" runat="server" cssclass="textboxclass1" Width="420px"></asp:TextBox>
                 <asp:TextBox ID="TextBox815" runat="server" BackColor="Red" Enabled="False" ForeColor="White" Width="110px"></asp:TextBox>
                 <asp:Label ID="Label629" runat="server" ForeColor="Red"></asp:Label>
        <br />
                 <asp:Label ID="Label353" runat="server"  ForeColor="Blue" Text="Mat Qty" Width="160px"></asp:Label>
                 <asp:TextBox ID="po_matqty_text0" runat="server" Width="160px">0</asp:TextBox>
                 <asp:Label ID="Label37" runat="server"  ForeColor="Blue" Text="Disc" Width="100px"></asp:Label>
                 <asp:TextBox ID="po_tradedisText1" runat="server" Width="150px">0</asp:TextBox>
                 <asp:DropDownList ID="DISCOUNT_typeComboBox" runat="server" Width="110px">
                     <asp:ListItem>N/A</asp:ListItem>
                     <asp:ListItem>PERCENTAGE</asp:ListItem>
                     <asp:ListItem>PER UNIT</asp:ListItem>
                 </asp:DropDownList>
        <br />
                 <asp:Label ID="Label354" runat="server"  ForeColor="Blue" Text="Unit Rate" Width="160px"></asp:Label>
                 <asp:TextBox ID="po_unitrateText" runat="server" Width="160px">0</asp:TextBox>
                 <asp:Label ID="Label36" runat="server"  ForeColor="Blue" Text="P &amp; F " Width="100px"></asp:Label>
                 <asp:TextBox ID="po_pfCombo1" runat="server" Width="150px">0</asp:TextBox>
                 <asp:DropDownList ID="PF_typeComboBox3" runat="server" Width="110px">
                     <asp:ListItem>N/A</asp:ListItem>
                     <asp:ListItem>PERCENTAGE</asp:ListItem>
                     <asp:ListItem>PER UNIT</asp:ListItem>
                 </asp:DropDownList>
        <br />
                 <asp:Label ID="Label355" runat="server"  ForeColor="Blue" Text="Vat" Width="160px"></asp:Label>
                 <asp:TextBox ID="po_taxTextBox1" runat="server" Width="160px">0</asp:TextBox>
                 <asp:Label ID="Label39" runat="server"  ForeColor="Blue" Text="Excise" Width="100px"></asp:Label>
                 <asp:TextBox ID="po_excComboBox1" runat="server" Width="150px">0</asp:TextBox>
                 <asp:DropDownList ID="po_ed_typeComboBox1" runat="server" Width="110px">
                     <asp:ListItem>N/A</asp:ListItem>
                     <asp:ListItem>PERCENTAGE</asp:ListItem>
                     <asp:ListItem>PER UNIT</asp:ListItem>
                 </asp:DropDownList>
               




                 
                <br />
                 <asp:Label ID="Label42" runat="server"  ForeColor="Blue" Text="Delivery Date" Width="160px"></asp:Label>
                 <asp:TextBox ID="Delvdate7" runat="server" Width="160px"></asp:TextBox>
                 <cc1:CalendarExtender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="Delvdate7" />
                 <asp:Label ID="Label356" runat="server"  ForeColor="Blue" Text="Freight" Width="100px"></asp:Label>
                 <asp:TextBox ID="po_frightText1" runat="server" Width="150px">0</asp:TextBox>
                 <asp:DropDownList ID="po_ed_typeComboBox2" runat="server" Width="110px">
                     <asp:ListItem>N/A</asp:ListItem>
                     <asp:ListItem>PERCENTAGE</asp:ListItem>
                     <asp:ListItem>PER UNIT</asp:ListItem>
                 </asp:DropDownList>
                 
        <br />
                 <asp:Label ID="Label442" runat="server" BackColor="#00CC66" Text="Material Details:-" Width="100%"></asp:Label>
        <br />
                 <asp:TextBox ID="TextBox733" runat="server" BorderColor="Lime" Height="80px" TextMode="MultiLine" Width="600px"></asp:TextBox>
                 <div runat ="server" style ="float :right ">
                 <asp:Button ID="Button47" runat="server" Font-Bold="True" Text="ADD" Width="100px" CssClass="bottomstyle" Font-Size="Small" />
        </div>
        <br />
                 <br />
                 <asp:Panel ID="Panel12" runat="server" ScrollBars="Auto" Width="993px">
                 <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="text-align: center" Width="200%">
                     <Columns>
                         <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat SLNo" />
                         <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                         <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                         <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                         <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                         <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Rate" />
                         <asp:BoundField DataField="DISC_TYPE" HeaderText="Disc. Type" />
                         <asp:BoundField DataField="MAT_DISCOUNT" HeaderText="Discount" />
                         <asp:BoundField DataField="PF_TYPE" HeaderText="P&amp;F Type" />
                         <asp:BoundField DataField="MAT_PACK" HeaderText="P &amp; F" />
                         <asp:BoundField DataField="MAT_EXCISE" HeaderText="ED. Type" />
                         <asp:BoundField DataField="MAT_EXCISE_DUTY" HeaderText="Excise Duty" />
                         <asp:BoundField DataField="MAT_TAXTYPE" HeaderText="TAX Type" />
                         <asp:BoundField DataField="MAT_CST" HeaderText="VAT/C.S.T." />
                         <asp:BoundField DataField="FREIGHT_TYPE" HeaderText="Freight Type" />
                         <asp:BoundField DataField="MAT_FREIGHT_PU" HeaderText="Freight" />
                         <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                         <asp:BoundField DataField="MAT_DESC" HeaderText="Description" />
                     </Columns>
                 </asp:GridView>
                      </asp:Panel>
                
                
        <br />
                 <div runat ="server" style ="float:right; width: 303px;">
                <asp:Label ID="Label49" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Assble Val" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox18" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
        <br />
                 <asp:Label ID="Label50" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Excise Duty" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox19" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                <br />
                      <asp:Label ID="Label279" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="VAT/C.S.T. " Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox20" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                     <br />
                      <asp:Label ID="Label280" runat="server" Font-Bold="True" ForeColor="Blue" style="text-align: left" Text="Round Off" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox21" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                <br />
                      <asp:Label ID="Label281" runat="server" Font-Bold="True"  ForeColor="Red" style="text-align: left" Text="TOTAL VAL" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox22" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      </div>
                 <asp:Label ID="Label43" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Disc Val" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox83" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="121px">0.00</asp:TextBox>
                 <br />
                 <asp:Label ID="Label44" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Freight" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox13" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="121px">0.00</asp:TextBox>
                 
        <br />
                 <asp:Label ID="Label45" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="P&amp;F Val" Width="120px"></asp:Label>
                 <asp:TextBox ID="TextBox14" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="121px">0.00</asp:TextBox>
                
        <br />
                 <asp:Label ID="Label46" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="E.Tax Val" Width="120px"></asp:Label>
                 
                 <asp:TextBox ID="TextBox15" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="121px">0.00</asp:TextBox>
          <br />
                 <br />
                 <br />
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel6" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="1000px" CssClass="brds">
                <asp:Label ID="Label303" runat="server" BackColor="Blue" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="MISC. SALES &amp; STOCK TRANSFER" Width="100%" Font-Size="XX-Large" CssClass="brds"></asp:Label>
              <br />
               <br />
                &nbsp;<asp:Label ID="Label426" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. / Ref. No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox723" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label427" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox724" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
              <br />
                &nbsp;<asp:Label ID="Label428" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox725" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label429" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox726" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
              <br />
                &nbsp;<asp:Label ID="Label310" runat="server" Font-Bold="True" ForeColor="Blue" Text="Type Of Material" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox727" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="230px"></asp:TextBox>
              <br />
                &nbsp;<asp:Label ID="Label318" runat="server" Font-Bold="True" ForeColor="Blue" Text="Chapter Heading" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList14" runat="server" Width="230px">
                </asp:DropDownList>
              <br />
                &nbsp;<asp:Label ID="Label311" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Group" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList11" runat="server" Width="230px">
                </asp:DropDownList>
                <asp:Label ID="Label438" runat="server" ForeColor="Red"></asp:Label>
              <br />
                <asp:Label ID="Label333" runat="server" BackColor="Blue" BorderColor="#CC0000" BorderStyle="Double"  ForeColor="White" style="text-align: center" Text="MATERIAL DETAILS" Width="100%"></asp:Label>
                <div runat="server" style="float :left; width: 788px;">
                    &nbsp;<br />&nbsp;<asp:Label ID="Label312" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code" Width="110px"></asp:Label>
                    <script type="text/javascript">

                          $(function () {
                              $("[id$=DropDownList12]").autocomplete({
                                  source: function (request, response) {
                                      $.ajax({
                                          url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                           data: "{ 'prefix': '" + request.term + "'}",
                           dataType: "json",
                           type: "POST",
                           contentType: "application/json; charset=utf-8",
                           success: function (data) {
                               response($.map(data.d, function (item) {
                                   return {
                                       label: item.split('^')[0],
                                       val: item.split('^')[1],
                                       mat_au: item.split('^')[2],
                                       mat_rate: item .split('^')[3]
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
                    $("[id$= TextBox79]").val(i.item.mat_au);
                    $("[id$= TextBox77]").val(i.item.mat_rate);
                   },
                   minLength: 1
               });
           });
    </script>
                    <asp:TextBox ID="DropDownList12" runat="server" cssclass="textboxclass1" Width="420px"></asp:TextBox>
                    <asp:Label ID="Label630" runat="server" ForeColor="Red"></asp:Label>
              <br />
                    &nbsp;<asp:Label ID="Label319" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. Line No" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox74" runat="server" Width="120px"></asp:TextBox>
              <br />
                    &nbsp;<asp:Label ID="Label320" runat="server" Font-Bold="True" ForeColor="Blue" Text="VOCAB No" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox75" runat="server" Width="120px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="Label335" runat="server" Text="A/U" Width="96px"></asp:Label>
                    <asp:TextBox ID="TextBox79" runat="server" BackColor="Red" Enabled="False" ForeColor="White" Width="150px"></asp:TextBox>
              <br /> &nbsp;<asp:Label ID="Label322" runat="server" Font-Bold="True" ForeColor="Blue" Text="Ord. Qty" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox76" runat="server" Width="120px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label323" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Rate" Width="96px"></asp:Label>
                    <asp:TextBox ID="TextBox77" runat="server" Width="150px"></asp:TextBox>
              <br />
                    &nbsp;<asp:Label ID="Label324" runat="server" Font-Bold="True" ForeColor="Blue" Text="P &amp; F " Width="110px"></asp:Label>
                    <asp:TextBox ID="po_pfCombo0" runat="server" Width="120px">0.00</asp:TextBox>
                    %&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label325" runat="server" Font-Bold="True" ForeColor="Blue" Text="Disc" Width="96px"></asp:Label>
                    <asp:TextBox ID="po_tradedisText0" runat="server" Width="150px">0.00</asp:TextBox>
                    %<br /> &nbsp;<asp:Label ID="Label326" runat="server" Font-Bold="True" ForeColor="Blue" Text="ED Type" Width="110px"></asp:Label>
                    <asp:DropDownList ID="po_ed_typeComboBox0" runat="server" Width="120px">
                        <asp:ListItem>Percentege</asp:ListItem>
                        <asp:ListItem>Amount</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="Label327" runat="server" Font-Bold="True" ForeColor="Blue" Text="Excise" Width="96px"></asp:Label>
                    <asp:TextBox ID="po_excComboBox0" runat="server" Width="150px">0.00</asp:TextBox>
              <br />
                    &nbsp;<asp:Label ID="Label328" runat="server" Font-Bold="True" ForeColor="Blue" Text="Vat" Width="110px"></asp:Label>
                    <asp:TextBox ID="po_taxTextBox0" runat="server" Width="120px">0</asp:TextBox>
                    %&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label329" runat="server" Font-Bold="True"  ForeColor="Blue" Text="Freight/Mt" Width="96px"></asp:Label>
                    <asp:TextBox ID="po_frightText0" runat="server" Width="150px">0.00</asp:TextBox>
                    <asp:Label ID="Label430" runat="server"></asp:Label>
              <br />
                    &nbsp;<asp:Label ID="Label330" runat="server" Font-Bold="True" ForeColor="Blue" Text="Terminal Tax" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox78" runat="server" Width="120px">0.00</asp:TextBox>
                    %&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label331" runat="server" Font-Bold="True"  ForeColor="Blue" Text="Delivery Date" Width="96px"></asp:Label>
                    <asp:TextBox ID="Delvdate5" runat="server" Width="150px"></asp:TextBox>
                
                    <cc1:CalendarExtender ID="Delvdate5_CalendarExtender" runat="server" BehaviorID="Delvdate5_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate5" />
                
              <br />
                    &nbsp;<asp:Label ID="Label336" runat="server" Font-Bold="True" ForeColor="Blue" Text="T.C.S." Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox80" runat="server" Width="120px">0.00</asp:TextBox>
                    %<br /> &nbsp;<asp:Label ID="Label431" runat="server" Text="Mat. Details" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox728" runat="server" Width="300px"></asp:TextBox>
                  <br />
                </div>
                <div runat="server" style="float : right; height: 245px;">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Button ID="Button58" runat="server" Font-Bold="True" Text="SUBMIT" Width="100px" CssClass="bottomstyle" />
                    <br />
                    <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Names="Times New Roman" Text="SAVE" Width="100px" CssClass="bottomstyle" />
                    <br />
                    <asp:Button ID="Button4" runat="server" Font-Bold="True" Font-Names="Times New Roman" Text="ADD" Width="100px" CssClass="bottomstyle" />
                    <br />
                    <asp:Button ID="Button5" runat="server" Font-Bold="True" Font-Names="Times New Roman" Text="CANCEL" Width="100px" CssClass="bottomstyle" />
                </div>
             
                <br />
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Font-Bold="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%" Font-Size="XX-Small">
                    <Columns>
                        <asp:BoundField DataField="SlNo" HeaderText="SlNo">
                        </asp:BoundField>
                        <asp:BoundField DataField="v_no" HeaderText="VOCAB No" />
                        <asp:BoundField DataField="Mat Code" HeaderText="Item Code">
                        </asp:BoundField>
                        <asp:BoundField DataField="Mat Name" HeaderText="Item Name" />
                        <asp:BoundField DataField="A/U" HeaderText="A/U">
                        </asp:BoundField>
                        <asp:BoundField DataField="ORD_QTY_MT" HeaderText="Ord Qty">
                        </asp:BoundField>
                        <asp:BoundField DataField="Unit Price" HeaderText="Unit Price">
                        </asp:BoundField>
                        <asp:BoundField DataField="P_F" HeaderText="P &amp; F">
                        </asp:BoundField>
                        <asp:BoundField DataField="disc" HeaderText="Discount">
                        </asp:BoundField>
                        <asp:BoundField DataField="excise_type" HeaderText="Excise Type">
                        </asp:BoundField>
                        <asp:BoundField DataField="excise" HeaderText="Excise">
                        </asp:BoundField>
                        <asp:BoundField DataField="tax_type" HeaderText="Tax Type">
                        </asp:BoundField>
                        <asp:BoundField DataField="sale_tax" HeaderText="Sale Tax">
                        </asp:BoundField>
                        <asp:BoundField DataField="t_tax" HeaderText="T. Tax">
                        </asp:BoundField>
                        <asp:BoundField DataField="tcs" HeaderText="T.C.S.">
                        </asp:BoundField>
                        <asp:BoundField DataField="freight_type" HeaderText="Freight Type">
                        </asp:BoundField>
                        <asp:BoundField DataField="freight" HeaderText="Freight">
                        </asp:BoundField>
                        <asp:BoundField DataField="delv_date" HeaderText="Delv. Date">
                        </asp:BoundField>
                        <asp:BoundField DataField="Mat Desc" HeaderText="Item Description">
                        <HeaderStyle Width="250px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
             
              <br />
            </asp:Panel>
            <asp:Panel ID="panel_mat0" runat="server" BackColor="Silver" BorderColor="Blue" BorderStyle="Ridge" BorderWidth="2px" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="1000px" CssClass="brds">
                <asp:Label ID="Label634" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="FOREIGN MATERIAL INFORMATION" Width="100%" Font-Size="X-Large" CssClass="brds"></asp:Label>
                <div style="float :right; height: 126px; margin-top: 36px;">
                    <asp:Button ID="Button77" runat="server" Font-Bold="True" Text="SUBMIT" Width="100px" CssClass="bottomstyle" />
                   <br />
                     <asp:Button ID="Button78" runat="server" Font-Bold="True" Text="SAVE" Width="100px" CssClass="bottomstyle" />
                  <br />
                      <asp:Button ID="Button79" runat="server" Font-Bold="True" Text="CANCEL" Width="100px" CssClass="bottomstyle" />
                </div>



                <div runat ="server" style ="float:left ;" >
                    <br />
                    <br />
                &nbsp;<asp:Label ID="Label635" runat="server" Font-Bold="True" ForeColor="Blue" Text="P.O. / Ref. No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox817" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label636" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox818" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
                <asp:Label ID="Label637" runat="server" ForeColor="Red"></asp:Label>
        <br />
                    <br />
                &nbsp;<asp:Label ID="Label638" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox819" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                     <asp:Label ID="Label639" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox820" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
      </div>
                 <br />
                &nbsp;<br /><asp:Label ID="Label641" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Width="100%" Font-Size="Medium">MATERIAL DETAILS</asp:Label>
        <br />

                <div runat ="server" style ="float:right ">
                 <asp:Button ID="Button80" runat="server" Font-Bold="True" Text="DELETE" Width="100px" CssClass="bottomstyle" />
                    <br />
                     <asp:Button ID="Button81" runat="server" Font-Bold="True" Text="CORECTION" Width="100px" CssClass="bottomstyle" />
                </div>









                <asp:Label ID="Label642" runat="server" Text="Mat. SLNo" Width="160px"></asp:Label>
                <asp:DropDownList ID="DropDownList60" runat="server" Width="160px">
                </asp:DropDownList>
        <br />
                <asp:Label ID="Label643" runat="server"  ForeColor="Blue" Text="Material Code &amp; Name" Width="160px"></asp:Label>
                <script type="text/javascript">

           $(function () {
               $("[id$=po_matcodecombo1]").autocomplete({
                   source: function (request, response) {
                       $.ajax({
                           url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                                        data: "{ 'prefix': '" + request.term + "'}",
                                        dataType: "json",
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        success: function (data) {
                                            response($.map(data.d, function (item) {
                                                return {
                                                    label: item.split('^')[0],
                                                    val: item.split('^')[1],
                                                    MAT_AU: item.split('^')[2]
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
                                    $("[id$=TextBox821]").val(i.item.MAT_AU);
                                },
                                minLength: 1
                            });
                        });
    </script>
                <asp:TextBox ID="po_matcodecombo1" runat="server" cssclass="textboxclass1" Width="420px"></asp:TextBox>
                <asp:TextBox ID="TextBox821" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="110px"></asp:TextBox>
                <asp:Label ID="Label644" runat="server" ForeColor="Red"></asp:Label>
        <br />
                <asp:Label ID="Label645" runat="server"  ForeColor="Blue" Text="Mat Qty" Width="160px"></asp:Label>
                <asp:TextBox ID="po_matqty_text1" runat="server" Width="160px">0</asp:TextBox>
        <br />
                <asp:Label ID="Label647" runat="server"  ForeColor="Blue" Text="Unit Rate" Width="160px"></asp:Label>
                <asp:TextBox ID="po_unitrateText0" runat="server" Width="160px">0</asp:TextBox>
                
                
        <br />
                <asp:Label ID="Label651" runat="server"  ForeColor="Blue" Text="Delivery Date" Width="160px"></asp:Label>
                <asp:TextBox ID="Delvdate8" runat="server" Width="160px"></asp:TextBox>
               
               
                <cc1:CalendarExtender ID="Delvdate8_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate8_CalendarExtender" TargetControlID="Delvdate8" />
               
               
        <br />
                <asp:Label ID="Label653" runat="server" BackColor="#00CC66" Text="Material Details:-" Width="100%"></asp:Label>
        <br />
                
                <asp:TextBox ID="TextBox822" runat="server" BorderColor="Lime"  Height="80px" TextMode="MultiLine" Width="600px"></asp:TextBox>
               <div runat ="server" style ="float :right;">
                     <asp:Button ID="Button82" runat="server" Font-Bold="True" Text="ADD" Width="100px" CssClass="bottomstyle" />
                </div>
        
        <br />
                <asp:GridView ID="GridView216" runat="server" AutoGenerateColumns="False"  HorizontalAlign="Center" ShowHeaderWhenEmpty="True" style="text-align: center" Width="998px" Font-Size="Small">
                    <Columns>
                        <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat SLNo" />
                        <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                        <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                        <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                        <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                        <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Rate" />
                        <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                        <asp:BoundField DataField="MAT_DESC" HeaderText="Description" />
                    </Columns>
                </asp:GridView>
                &nbsp;
        <br />
                <div runat ="server" style ="float :right ">
                <asp:Label ID="Label655" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Assble Val" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox824" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
        <br />
                <asp:Label ID="Label662" runat="server" Font-Bold="True"  ForeColor="Red" style="text-align: left" Text="TOTAL VAL" Width="80px"></asp:Label>
                <asp:TextBox ID="TextBox831" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
       </div>
                     <br />
                 <br />
                 <br />
 <br />

            </asp:Panel>
            <asp:Panel ID="Panel9" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Bold="False" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="1000px" CssClass="brds">
                <asp:Label ID="Label398" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="NEW WORK ORDER" Width="100%" Font-Size="X-Large" CssClass="brds" Height="40px"></asp:Label>
        <br />
        <br />
                &nbsp;<asp:Label ID="Label412" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label413" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
        <br />
                &nbsp;<asp:Label ID="Label415" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox128" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label416" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox129" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
        <br />
                &nbsp;<br />
                <asp:Label ID="Label423" runat="server" BackColor="Blue" BorderColor="#CC0000" BorderStyle="Double" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="ORDER DETAILS" Width="100%" Font-Size="Medium"></asp:Label>
        <br />
        <br />
                <div runat="server" style="float :left ">
                    <asp:Label ID="Label425" runat="server" ForeColor="Blue" Text="Order Type" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox722" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="265px"></asp:TextBox>
                    <asp:Label ID="Label441" runat="server"></asp:Label>
        <br />
                    <asp:Label ID="Label440" runat="server" ForeColor="Blue" Text="Taxable Service" Width="110px"></asp:Label>
                    <asp:DropDownList ID="DropDownList46" runat="server" Width="265px">
                    </asp:DropDownList>
        <br />
                    <asp:Label ID="Label399" runat="server"  ForeColor="Blue" Text="Location" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox641" runat="server" Width="265px"></asp:TextBox>
        <br />
                    <asp:Label ID="Label400" runat="server"  ForeColor="Blue" Text="Due Date" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox651" runat="server" Width="120px"></asp:TextBox>
                    <cc1:CalendarExtender ID="TextBox651_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" runat="server" BehaviorID="TextBox651_CalendarExtender" TargetControlID="TextBox651" />
                    <asp:Label ID="Label401" runat="server"  ForeColor="Blue" Text="To"></asp:Label>
                    <asp:TextBox ID="TextBox661" runat="server" Width="120px"></asp:TextBox>
                    <cc1:CalendarExtender ID="TextBox661_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox661_CalendarExtender" TargetControlID="TextBox661" />
        <br />
                    <asp:Label ID="Label273" runat="server"  ForeColor="Blue" Text="Tolerance %" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox671" runat="server" Width="120px"></asp:TextBox>
        <br />
                    <asp:Label ID="Label409" runat="server"  ForeColor="Blue" Text="Description of Job" Width="110px"></asp:Label>
                    <asp:TextBox ID="TextBox621" runat="server" EnableViewState="False" Height="59px" TabIndex="14" TextMode="MultiLine" Width="265px"></asp:TextBox>
                </div>
                <div runat="server" style="float :right; height: 181px; width: 205px; text-align: right;">
            <br />
                    <asp:Button ID="Button59" runat="server" Font-Bold="True" Text="SUBMIT" Width="100px" CssClass="bottomstyle" />
        <br />
                    <asp:Button ID="Button53" runat="server" Font-Bold="True" Text="SAVE" Width="100px" CssClass="bottomstyle" />
        <br />
                    <asp:Button ID="Button54" runat="server" Font-Bold="True" Text="CANCEL" Width="100px" CssClass="bottomstyle" />
                </div>
                <asp:Label ID="Label424" runat="server" BackColor="Blue" BorderColor="Red" BorderStyle="Groove" Font-Bold="True" ForeColor="White" style="text-align: center" Text="WORK DETAILS" Width="100%" Font-Size="Medium"></asp:Label>
        <br />
                <asp:Label ID="Label403" runat="server"  ForeColor="Blue" Text="Ordered Qty" Width="80px"></asp:Label>
                &nbsp;<asp:Label ID="Label404" runat="server"  ForeColor="Blue" Text="A/U" Width="80px"></asp:Label>
                <asp:Label ID="Label405" runat="server"  ForeColor="Blue" Text="Rate/Unit" Width="80px"></asp:Label>
                <asp:Label ID="Label406" runat="server"  ForeColor="Blue" Text="Mat. Cost" Width="80px"></asp:Label>
                <asp:Label ID="Label407" runat="server"  ForeColor="Blue" Text="Discount %" Width="80px"></asp:Label>
                <asp:Label ID="Label255" runat="server"  ForeColor="Blue" Text="WC Tax %" Width="80px"></asp:Label>
        <br />
                <asp:TextBox ID="TextBox561" runat="server" TabIndex="15" Width="80px"></asp:TextBox>
                <asp:TextBox ID="TextBox571" runat="server" TabIndex="16" Width="80px"></asp:TextBox>
                <asp:TextBox ID="TextBox581" runat="server" TabIndex="17" Width="80px"></asp:TextBox>
                <asp:TextBox ID="TextBox591" runat="server" TabIndex="18" Width="80px"></asp:TextBox>
                <asp:TextBox ID="TextBox601" runat="server" TabIndex="19" Width="80px"></asp:TextBox>
                <asp:TextBox ID="TextBox611" runat="server" TabIndex="20" Width="80px"></asp:TextBox>
                <div class ="pull-right " >
                     <asp:Button ID="Button50" runat="server" Text="ADD" CssClass="bottomstyle" Width="100px" />
                </div>
               
        <br />
                   <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="993px">
                     
                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True" Width="200%">
                    <Columns>
                        <asp:BoundField DataField="W_SLNO" HeaderText="SLNo" />
                        <asp:BoundField DataField="WO_TYPE" HeaderText="Taxable Service" />
                        <asp:BoundField DataField="W_NAME" HeaderText="Desc. Of Job" />
                        <asp:BoundField DataField="W_QTY" HeaderText="Qty" />
                        <asp:BoundField DataField="W_AU" HeaderText="A/U" />
                        <asp:BoundField DataField="W_UNIT_PRICE" HeaderText="Unit Price" />
                        <asp:BoundField DataField="W_MATERIAL_COST" HeaderText="Mat. Charge" />
                        <asp:BoundField DataField="W_AREA" HeaderText="Location" />
                        <asp:BoundField DataField="W_START_DATE" HeaderText="Strat Date" />
                        <asp:BoundField DataField="W_END_DATE" HeaderText="End Date" />
                        <asp:BoundField DataField="W_TOLERANCE" HeaderText="Tolerance" />
                        <asp:BoundField DataField="W_DISCOUNT" HeaderText="Discount" />
                        <asp:BoundField DataField="W_STAX" HeaderText="S. Tax" />
                        <asp:BoundField DataField="W_WCTAX" HeaderText="WC. Tax" />
                    </Columns>
                </asp:GridView>
                         </asp:Panel>
               <div class="pull-right " >

               
                <asp:Label ID="Label274" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Assble Val" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox681" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
        <br />
               
                <asp:Label ID="Label275" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Service Tax" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox691" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
        <br />
               
                <asp:Label ID="Label276" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="W C Tax" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox701" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
        <br />
                
                <asp:Label ID="Label277" runat="server" Font-Bold="True"  ForeColor="Blue" style="text-align: left" Text="Round Off" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox711" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
        <br />
               
                <asp:Label ID="Label278" runat="server" Font-Bold="True"  ForeColor="Red" style="text-align: left" Text="TOTAL VAL" Width="120px"></asp:Label>
                <asp:TextBox ID="TextBox721" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                   </div>
        <br />
        <br />
                <br />
                <br />
                <br />
                <br />
                       
        <br />
            </asp:Panel>
            <asp:Panel ID="Panel11" runat="server" BorderColor="#006666" BorderStyle="Groove" Font-Names="Times New Roman" style="text-align: left" Width="666px" CssClass="brds">
                <asp:Label ID="Label309" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="RATE CONTRACT" Width="100%" CssClass="brds" Font-Size="Large"></asp:Label>
        <br />
        <br />
                <asp:Label ID="Label433" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox729" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label434" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox730" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="100px"></asp:TextBox>
        <br />
               <asp:Label ID="Label435" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox731" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label436" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox732" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
        <br />
        <br />
                <asp:Label ID="Label437" runat="server" BackColor="Blue" BorderColor="Red" BorderStyle="Groove" Font-Bold="True" ForeColor="White" style="text-align: center" Text="RATE CONTRACT DETAILS" Width="660px"></asp:Label>
        <br />
        <br />
                <asp:Label ID="Label307" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amount"></asp:Label>
                <asp:TextBox ID="TextBox84" runat="server"></asp:TextBox>
                <asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                <asp:TextBox ID="TextBox85" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="TextBox85_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" runat="server" BehaviorID="TextBox85_CalendarExtender" TargetControlID="TextBox85" />
           <br />
        <br />
        <br />
               
                <asp:Button ID="Button56" runat="server" Font-Bold="True" Text="SAVE" Width="100px" CssClass="bottomstyle" />
                <asp:Button ID="Button57" runat="server" Font-Bold="True" Text="CANCEL" Width="100px" CssClass="bottomstyle" />
        <br />
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" BorderColor="Blue" BorderStyle="Ridge" BorderWidth="2px" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="1000px" CssClass="brds" Visible="False">
                <asp:Label ID="Label246" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="FINISHED PRODUCTS" Width="100%" CssClass="brds" Font-Size="Large"></asp:Label>
        <br />
        <br />
                &nbsp;<asp:Label ID="Label359" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. / Ref. No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox86" runat="server" BackColor="Red" ForeColor="White" Width="160px"></asp:TextBox>
                <asp:Label ID="Label360" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox87" runat="server" BackColor="Red" ForeColor="White" Width="100px"></asp:TextBox>
        <br />
                &nbsp;<asp:Label ID="Label362" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox90" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="160px"></asp:TextBox>
                <asp:Label ID="Label363" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox88" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
        <br />
                &nbsp;<asp:Label ID="Label368" runat="server" Font-Bold="True" ForeColor="Blue" Text="Chapter Heading" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList32" runat="server" Width="230px">
                </asp:DropDownList>
        <br />
                <asp:Label ID="Label370" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Width="100%" Font-Size="Medium">MATERIAL DETAILS</asp:Label>
        <br />
        <br />
       
        <br />
                <asp:Label ID="Label300" runat="server" Text="S.O. Line No" Width="96px"></asp:Label>
                <asp:TextBox ID="TextBox64" runat="server" Width="120px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label289" runat="server" Text="Ord. Unit" Width="96px"></asp:Label>
                <asp:DropDownList ID="DropDownList8" runat="server" Width="120px" AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>Pcs</asp:ListItem>
                    <asp:ListItem>Mtn</asp:ListItem>
                    <asp:ListItem>Activity</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label432" runat="server" ForeColor="Red"></asp:Label>
        <br />
                <asp:Label ID="Label287" runat="server" Text="VOCAB No" Width="96px"></asp:Label>
                <asp:TextBox ID="TextBox59" runat="server" Width="120px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label293" runat="server"  ForeColor="Blue" Text="Disc" Width="96px"></asp:Label>
                <asp:TextBox ID="po_tradedisText" runat="server" Width="120px">0.00</asp:TextBox>
                <asp:DropDownList ID="DropDownList48" runat="server" Width="110px">
                    <asp:ListItem>N/A</asp:ListItem>
                    <asp:ListItem>PERCENTAGE</asp:ListItem>
                    <asp:ListItem>PER UNIT</asp:ListItem>
                    <asp:ListItem>PER MT</asp:ListItem>
                </asp:DropDownList>
        
         
       
         <br />
                <asp:Label ID="Label288" runat="server" Text="Ord. Qty" Width="96px"></asp:Label>
                <asp:TextBox ID="TextBox60" runat="server" Width="120px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label292" runat="server"  ForeColor="Blue" Text="P &amp; F " Width="96px"></asp:Label>
                <asp:TextBox ID="po_pfCombo" runat="server" Width="120px">0.00</asp:TextBox>
                <asp:DropDownList ID="DropDownList47" runat="server" Width="110px">
                    <asp:ListItem>N/A</asp:ListItem>
                    <asp:ListItem>PERCENTAGE</asp:ListItem>
                    <asp:ListItem>PER UNIT</asp:ListItem>
                    <asp:ListItem>PER MT</asp:ListItem>
                </asp:DropDownList>
        <br />
                <asp:Label ID="Label291" runat="server" Text="Unit Rate" Width="96px"></asp:Label>
                <asp:TextBox ID="TextBox62" runat="server" Width="120px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:Label ID="Label295" runat="server"  ForeColor="Blue" Text="Excise" Width="96px"></asp:Label>
                <asp:TextBox ID="po_excComboBox" runat="server" Width="120px">0.00</asp:TextBox>
                <asp:DropDownList ID="po_ed_typeComboBox" runat="server" Width="110px">
                    <asp:ListItem>PERCENTAGE</asp:ListItem>
                    <asp:ListItem>PER UNIT</asp:ListItem>
                    <asp:ListItem>PER MT</asp:ListItem>
                </asp:DropDownList>
        <br />
                <asp:Label ID="Label52" runat="server"  ForeColor="Blue" Text="Vat" Width="96px"></asp:Label>
                <asp:TextBox ID="po_taxTextBox" runat="server" Width="120px">0</asp:TextBox>
                %&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label41" runat="server"  ForeColor="Blue" Text="Freight/Mt" Width="96px"></asp:Label>
                <asp:TextBox ID="po_frightText" runat="server" Width="120px">0.00</asp:TextBox>
                <asp:DropDownList ID="DropDownList9" runat="server" Width="110px">
                    <asp:ListItem>N/A</asp:ListItem>
                    <asp:ListItem>PERCENTAGE</asp:ListItem>
                    <asp:ListItem>PER UNIT</asp:ListItem>
                    <asp:ListItem>PER MT</asp:ListItem>
                </asp:DropDownList>
        <br />
                <asp:Label ID="Label301" runat="server" Text="Terminal Tax" Width="96px"></asp:Label>
                <asp:TextBox ID="TextBox65" runat="server" Width="120px">0.00</asp:TextBox>
                %&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label298" runat="server"  ForeColor="Blue" Text="Delivery Date" Width="96px"></asp:Label>
                <asp:TextBox ID="Delvdate" runat="server" Width="120px"></asp:TextBox>
                <cc1:CalendarExtender ID="Delvdate_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate_CalendarExtender" TargetControlID="Delvdate" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /><asp:Panel ID="Panel4" runat="server" Height="30px" style="text-align: center">
                    <asp:Button ID="Button39" runat="server" CssClass="bottomstyle" Font-Bold="True" Text="ADD ITEM" Width="100px" />
                    &nbsp;<asp:Button ID="Button40" runat="server" CssClass="bottomstyle" Font-Bold="True" Text="CANCEL" Width="100px" />
                    &nbsp;<asp:Button ID="Button55" runat="server" CssClass="bottomstyle" Font-Bold="True" Text="SUBMIT" Width="100px" />
                </asp:Panel>
                <asp:Panel ID="Panel5" runat="server" Visible="False" Width="100%">
                    <asp:Label ID="Label302" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="INDIVIDUAL ITEM DETAILS AS PER SALE ORDER" Width="100%" Font-Size="Medium"></asp:Label>
            <br />
                    <div style="height: 205px">
                        <div runat="server" style="float :left; width: 518px; ">
                            <div style="background-color: #4686F0; width: 498px; color: #FFFFFF;">
                                <asp:Label ID="Label32" runat="server" BackColor="#4686F0" Font-Bold="True"  ForeColor="White" Text="Item Code" Width="250px"></asp:Label>
                            </div>
                            <script type="text/javascript">

                         $(function () {
                             $("[id$=po_matcodecombo]").autocomplete({
                                 source: function (request, response) {
                                     $.ajax({
                                         url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODS")%>',
                     data: "{ 'prefix': '" + request.term + "'}",
                     dataType: "json",
                     type: "POST",
                     contentType: "application/json; charset=utf-8",
                     success: function (data) {
                         response($.map(data.d, function (item) {
                             return {
                                 label: item.split('^')[0],
                                 val: item.split('^')[1],
                                 ITEM_AU: item.split('^')[2],
                                 ITEM_WEIGHT: item.split('^')[3]
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
                 $("[id$=TextBox816]").val(i.item.ITEM_AU);
                 $("[id$=HiddenField2]").val(i.item.ITEM_AU);
             },
             minLength: 1
         });
     });
    </script>
                            <asp:TextBox ID="po_matcodecombo" runat="server" BorderStyle="Double" CssClass="textboxclass2" Width="500px" Visible="False"></asp:TextBox>
                                         <script type="text/javascript">

                                             $(function () {
                                                 $("[id$=TextBox1]").autocomplete({
                                                     source: function (request, response) {
                                                         $.ajax({
                                                             url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODMT")%>',
                                         data: "{ 'prefix': '" + request.term + "'}",
                                         dataType: "json",
                                         type: "POST",
                                         contentType: "application/json; charset=utf-8",
                                         success: function (data) {
                                             response($.map(data.d, function (item) {
                                                 return {
                                                     label: item.split('^')[0],
                                                     val: item.split('^')[1],
                                                     ITEM_AU: item.split('^')[2],
                                                     ITEM_WEIGHT: item.split('^')[3]
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
                                     $("[id$=TextBox816]").val(i.item.ITEM_AU);
                                 },
                                 minLength: 1
                             });
                         });
    </script>
                              <asp:TextBox ID="TextBox1" runat="server" BorderStyle="Double" CssClass="textboxclass2" Width="500px" Visible="False"></asp:TextBox>
                                                                 <script type="text/javascript">

                                                                     $(function () {
                                                                         $("[id$=TextBox2]").autocomplete({
                                                                             source: function (request, response) {
                                                                                 $.ajax({
                                                                                     url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODACT")%>',
                                                             data: "{ 'prefix': '" + request.term + "'}",
                                                             dataType: "json",
                                                             type: "POST",
                                                             contentType: "application/json; charset=utf-8",
                                                             success: function (data) {
                                                                 response($.map(data.d, function (item) {
                                                                     return {
                                                                         label: item.split('^')[0],
                                                                         val: item.split('^')[1],
                                                                         ITEM_AU: item.split('^')[2],
                                                                         ITEM_WEIGHT: item.split('^')[3]
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
                                                         $("[id$=TextBox816]").val(i.item.ITEM_AU);
                                                     },
                                                     minLength: 1
                                                 });
                                             });
    </script>
                            <asp:TextBox ID="TextBox2" runat="server" BorderStyle="Double" CssClass="textboxclass2" Width="500px" Visible="False"></asp:TextBox>
                             <br />
                            <asp:Label ID="Label631" runat="server" Text="Item Accounting Unit" Width="159px"></asp:Label>
                            <asp:Label ID="Label632" runat="server" Text="Item Ord. Unit " Width="159px"></asp:Label>
                            <asp:Label ID="Label633" runat="server" Text="Unit Weight (Kg)" Width="159px"></asp:Label>
                    <br />
                            <asp:TextBox ID="TextBox816" runat="server" BorderStyle="Double" Enabled="False" ForeColor="Red" Width="159px"></asp:TextBox>
                            <asp:TextBox ID="po_matqty_text" runat="server" BorderStyle="Double" Enabled="False" ForeColor="Red" Width="159px">0</asp:TextBox>
                            <asp:TextBox ID="po_unitWEIGHTText" runat="server" BorderStyle="Double" ForeColor="Red" Width="159px">0.000</asp:TextBox>
            <br />
                            <asp:Label ID="Label299" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" Text="Item Description" Width="498px"></asp:Label>
            <br />
                            <asp:TextBox ID="TextBox63" runat="server" BorderStyle="Double" CssClass="MATtextboxclass" Height="82px" TextMode="MultiLine" Width="498px"></asp:TextBox>
                        </div>
                        <div runat="server" style="float :right; ">
                <br />
                <br />
                <br />
                <br />
                <br />
                            <asp:Button ID="Button3" runat="server" BorderColor="Lime" CssClass="bottomstyle" Font-Bold="True" Text="ADD" />
                            &nbsp;<asp:Button ID="Button41" runat="server" BorderColor="Lime" CssClass="bottomstyle"  Text="CALCULATE" Visible="False" />
                            &nbsp;<asp:Button ID="Button42" runat="server" BorderColor="Lime" CssClass="bottomstyle"  Text="SAVE AND GOTO ANOTHER VOCAB" />
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SlNo" HeaderText="SlNo">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Code" HeaderText="Item Code">
                            <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Name" HeaderText="Item Name" />
                            <asp:BoundField DataField="A/U" HeaderText="A/U">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Qty" HeaderText="Item Unit Qty ">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit Weight" HeaderText="Unit Weight">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Ord. Qty" HeaderText="Item Ord. Qty">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORD_QTY_MT" HeaderText="Ord Qty (Mt)" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit Price" HeaderText="Unit Price(Mt)">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Desc" HeaderText="Item Description">
                            <ItemStyle Width="250px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>
            <br />

            
            
            
             
            
            
            
            
            
             </div> 
    </center>
















     


</asp:Content>
