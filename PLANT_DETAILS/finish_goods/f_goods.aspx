<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="f_goods.aspx.vb" Inherits="PLANT_DETAILS.f_goods" %>
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
            <asp:Panel ID="Panel2" runat="server" BorderColor="#00CC00" BorderStyle="Double" Width="1000px" Font-Names="Times New Roman" style="text-align: left" CssClass="brds" Visible="False">
              <asp:Label ID="Label280" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Overline="False" Font-Size="X-Large" ForeColor="White" style="text-align: center ; line-height :40px" Text="INVOICING OF FINISH GOODS" Height ="40px" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style="border-bottom: 10px double #008000; height: 270px; width: 100%;" >
 
                &nbsp;<asp:Label ID="Label299" runat="server" Text="Invoice No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox65" runat="server" Width="150px"></asp:TextBox>
                   &nbsp;<br />
                   &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="S.O. No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox124" runat="server" BackColor="Red" ForeColor="White" Width="150px"></asp:TextBox>
                   <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Buyer Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label287" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Transporter" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Width="152px">
                   </asp:DropDownList>
                   &nbsp;<asp:Label ID="Label296" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Transp. Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox75" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <asp:TextBox ID="TextBox62" runat="server" BackColor="Red" ForeColor="White" Width="300px" Enabled="False"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label463" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="WO Sl No" Width="120px"></asp:Label>
                  <asp:DropDownList ID="DropDownList27" runat="server" AutoPostBack="True" Width="152px"></asp:DropDownList>
                   &nbsp;<asp:Label ID="Label464" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox174" runat="server" BackColor="Red" CssClass="worktextboxclass" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="432px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label288" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Truck No" Width="120px"></asp:Label>
                  
                   
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
                   <br />
                   &nbsp;<asp:Label ID="Label293" runat="server" Text="Form to Receive" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:DropDownList ID="DropDownList7" runat="server" Width="152px" Font-Names="Times New Roman">
                       <asp:ListItem>N/A</asp:ListItem>
                       <asp:ListItem>F Form</asp:ListItem>
                       <asp:ListItem>C Form</asp:ListItem>
                   </asp:DropDownList>
                   <br />
                   &nbsp;<asp:Label ID="Label297" runat="server" Text="Debit Entry No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox63" runat="server" Width="150px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label309" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="LR No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox69" runat="server" Width="150px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label298" runat="server" Text="Notification" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox64" runat="server" Cssclass="notetextboxclass" TextMode="MultiLine" Width="382px"></asp:TextBox>
                   <br />
                   <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" ForeColor="Blue" Height="40px" Text="EXCISEABLE" Width="149px" />
               </div> 
               <div style ="float :right; width :500px; height: 188px; margin-left: 0px;" runat ="server" >
                         
                         <asp:FormView ID="FormView1" runat="server" Font-Names="Times New Roman" Width="100%" Height="102%" BorderColor="Red" BorderStyle="Double">
                             <ItemTemplate>
                                 <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF;"> <%# Eval("ITEM_CODE")%> </h3>
                                <h1 >===============================================</h1>
                                  <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Item Desc" Width="100"></asp:Label>
                                 <asp:Label ID="Label15" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label2" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                 <br />
                                 <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Item A/U" Width="100"></asp:Label>
                                 <asp:Label ID="Label16" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label4" runat="server" Text='<%# Eval("ITEM_AU")%>' Width="120"></asp:Label>
                                 <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Item U/W" Width="100"></asp:Label>
                                 <asp:Label ID="Label17" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label6" runat="server" Text='<%# Eval("ITEM_WEIGHT")%>'></asp:Label>
                                 <asp:Label ID="Label23" runat="server" Text="(Kg)"></asp:Label>
                                  <br />
                                 <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Ord. Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label18" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label8" runat="server" Text='<%# Eval("ITEM_QTY")%>' Width="120"></asp:Label>
                                 <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Ord. Bal Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label19" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label10" runat="server" Text='<%# Eval("ITEM_BAL_QTY")%>' Width="120"></asp:Label>
                                 <br />
                                 <asp:Label ID="Label11" runat="server" Font-Bold="True" Text="Unit In Stock" Width="100"></asp:Label>
                                 <asp:Label ID="Label20" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label12" runat="server" Text='<%# Eval("ITEM_B_STOCK")%>' Width="120"></asp:Label>
                                 <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="Unit In Stock" Width="100"></asp:Label>
                                 <asp:Label ID="Label21" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label14" runat="server" Text='<%# Eval("ITEM_B_STOCK_MT")%>'></asp:Label>
                                 <asp:Label ID="Label22" runat="server" Text="(Mt)"></asp:Label>
                                 <br />
                             </ItemTemplate>
                         </asp:FormView>
                         </div> 
               <br />
                   <asp:Label ID="Label283" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No" Width="110px"></asp:Label>
              <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="152px">
              </asp:DropDownList>
              &nbsp;<asp:Label ID="Label284" runat="server" Font-Bold="True" ForeColor="Blue" Text="Vocab No" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox53" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="147px"></asp:TextBox>
                <br />
                <asp:Label ID="Label306" runat="server" Font-Bold="True" ForeColor="Blue" Text="Ammed No" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox67" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="150px"></asp:TextBox>
                <asp:Label ID="Label307" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox68" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Width="147px"></asp:TextBox>
                <br />
                <asp:Label ID="Label285" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Code" Width="110px"></asp:Label>
                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Width="152px">
                </asp:DropDownList>
                <asp:Label ID="Label467" runat="server" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label286" runat="server" Font-Bold="True" ForeColor="Blue" Text="Despatch Qty." Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox54" runat="server" Width="150px"></asp:TextBox>
                <asp:Label ID="Label289" runat="server" Font-Bold="True" ForeColor="Blue" Width="65px"></asp:Label>
                <br />
                <asp:Label ID="Label290" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Details" Width="110px"></asp:Label>
                <asp:TextBox ID="TextBox56" runat="server" Width="150px"></asp:TextBox>
                <asp:Label ID="Label291" runat="server" Font-Bold="True" ForeColor="Blue" Text="Lot No" Width="65px"></asp:Label>
                <asp:TextBox ID="TextBox57" runat="server" Width="147px"></asp:TextBox>
                <br />
                &nbsp;<asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
              &nbsp;&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button1" runat="server" Text="NEW" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
                <asp:Button ID="Button37" runat="server" Text="CANCEL" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button35" runat="server" Text="ADD" Width="75px" Font-Size="Small" CssClass="bottomstyle" />
              <asp:Button ID="Button36" runat="server" Text="SAVE" Width="75px" Font-Size="Small" CssClass="bottomstyle" Enabled="False" />
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
              <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Assble Val(As Per CAS 4)" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox38" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
               <br />
              <asp:Label ID="Label44" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Freight" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox39" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label50" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="B.E.D" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox40" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label45" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="P&amp;F Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox41" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label300" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Cess" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox42" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label46" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="T.Tax Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox43" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label301" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="HS Cess" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox44" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label305" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="TCS Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox66" runat="server" Width="144px" Enabled="False">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label303" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="VAT/C.S.T. " Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox20" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label304" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Round Off" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox21" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label302" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Red" style="text-align: left" Text="TOTAL VAL" Width="130px"></asp:Label>
              <asp:TextBox ID="TextBox45" runat="server" Width="120px" Enabled="False">0.00</asp:TextBox>
              <br />
          </asp:Panel>
            <asp:Panel ID="Panel5" runat="server" BorderColor="#00CC00" BorderStyle="Double" Width="1000px" Font-Names="Times New Roman" Visible="False" style="text-align: left" CssClass="brds">
              <asp:Label ID="Label344" runat="server" BackColor="Blue" Font-Bold="True" Font-Names="Times New Roman" Font-Overline="False" Font-Size="X-Large" ForeColor="White" style="text-align: center; line-height :40px" Text="INVOICING OF MISC. / STOCK TRANSFOR" Height ="40px" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style="border-bottom: 10px double #008000; height: 269px; width: 100%;" >
 
                &nbsp;<asp:Label ID="Label345" runat="server" Text="Invoice No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox95" runat="server" Width="150px"></asp:TextBox>
                   &nbsp;<asp:Label ID="Label406" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <br />
                    &nbsp;<asp:Label ID="Label346" runat="server" Text="S.O. No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox123" runat="server" BackColor="Red" ForeColor="White" Width="150px"></asp:TextBox>
                   <asp:Label ID="Label347" runat="server" Text="Buyer Name" Width="100px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox96" runat="server" ReadOnly="True" BackColor="Red" ForeColor="White"></asp:TextBox>
                   <asp:TextBox ID="TextBox97" runat="server" Width="300px" ReadOnly="True" BackColor="Red" ForeColor="White"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label348" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Transporter" Width="120px" Font-Names="Times New Roman"></asp:Label>
                    <asp:DropDownList ID="DropDownList9" runat="server" Width="154px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:Label ID="Label349" runat="server" Text="Transp. Name" Width="100px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox98" runat="server" ReadOnly="True" BackColor="Red" ForeColor="White"></asp:TextBox>
                   <asp:TextBox ID="TextBox99" runat="server" Width="300px" ReadOnly="True" BackColor="Red" ForeColor="White"></asp:TextBox>
                    <br />
                    &nbsp;<asp:Label ID="Label465" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="WO Sl No" Width="120px"></asp:Label>
                   <asp:DropDownList ID="DropDownList28" runat="server" AutoPostBack="True" Width="154px">
                   </asp:DropDownList>
                   <asp:Label ID="Label466" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Name" Width="100px"></asp:Label>
                   <asp:TextBox ID="TextBox175" runat="server" BackColor="Red" CssClass="worktextboxclass" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="370px"></asp:TextBox>
                   &nbsp;<br />
                   &nbsp;<asp:Label ID="Label350" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="Truck No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox100" runat="server" Width="150px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label351" runat="server" Text="Form to Receive" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:DropDownList ID="DropDownList10" runat="server" Width="150px" Font-Names="Times New Roman">
                       <asp:ListItem>N/A</asp:ListItem>
                       <asp:ListItem>F Form</asp:ListItem>
                       <asp:ListItem>C Form</asp:ListItem>
                   </asp:DropDownList>
                   <br />
                   &nbsp;<asp:Label ID="Label352" runat="server" Text="Debit Entry No" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox101" runat="server" Width="150px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label353" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue" Text="LR No" Width="120px"></asp:Label>
                   <asp:TextBox ID="TextBox102" runat="server" Width="150px"></asp:TextBox>
                   <br />
                   &nbsp;<asp:Label ID="Label354" runat="server" Text="Notification" Width="120px" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox103" runat="server" Cssclass="notetextboxclass" TextMode="MultiLine" Width="382px"></asp:TextBox>
               </div> 
               <div style ="float :right; width :500px; height: 188px; margin-left: 0px;" runat ="server" >
                         
                         <asp:FormView ID="FormView2" runat="server" Font-Names="Times New Roman" Width="100%" Height="102%" BorderColor="Red" BorderStyle="Double">
                             <ItemTemplate>
                                 <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF;"> <%# Eval("ITEM_CODE")%> </h3>
                                <h1 >===============================================</h1>
                                  <asp:Label ID="Label355" runat="server" Font-Bold="True" Text="Item Desc" Width="100"></asp:Label>
                                 <asp:Label ID="Label356" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label357" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                 <br />
                                 <asp:Label ID="Label358" runat="server" Font-Bold="True" Text="Item A/U" Width="100"></asp:Label>
                                 <asp:Label ID="Label359" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label360" runat="server" Text='<%# Eval("ITEM_AU")%>' Width="120"></asp:Label>
                                <asp:Label ID="Label371" runat="server" Font-Bold="True" Text="Unit In Stock" Width="100"></asp:Label>
                                 <asp:Label ID="Label372" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label373" runat="server" Text='<%# Eval("ITEM_B_STOCK")%>' Width="120"></asp:Label>
                                  <br />
                                 <asp:Label ID="Label365" runat="server" Font-Bold="True" Text="Ord. Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label366" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label367" runat="server" Text='<%# Eval("ITEM_QTY")%>' Width="120"></asp:Label>
                                 <asp:Label ID="Label368" runat="server" Font-Bold="True" Text="Ord. Bal Qty" Width="100"></asp:Label>
                                 <asp:Label ID="Label369" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                 <asp:Label ID="Label370" runat="server" Text='<%# Eval("ITEM_BAL_QTY")%>' Width="120"></asp:Label>
                                 <br />
                             </ItemTemplate>
                         </asp:FormView>
                         </div> 
               <br />
                   <asp:Label ID="Label378" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No" Width="110px"></asp:Label>
              <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True" Width="150px">
              </asp:DropDownList>
              <asp:Label ID="Label379" runat="server" Font-Bold="True" ForeColor="Blue" Text="Vocab No" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox104" runat="server" Width="147px" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
              <br />
              <asp:Label ID="Label380" runat="server" Font-Bold="True" ForeColor="Blue" Text="Ammed No" Width="110px"></asp:Label>
              <asp:TextBox ID="TextBox105" runat="server" Width="150px" ReadOnly="True" BackColor="Red" ForeColor="White"></asp:TextBox>
              <asp:Label ID="Label381" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox106" runat="server" Width="147px" ReadOnly="True" BackColor="Red" ForeColor="White"></asp:TextBox>
              <br />
              <asp:Label ID="Label382" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Code" Width="110px"></asp:Label>
              <asp:DropDownList ID="DropDownList12" runat="server" AutoPostBack="True" Width="150px">
              </asp:DropDownList>
              <br />
              <asp:Label ID="Label383" runat="server" Font-Bold="True" ForeColor="Blue" Text="Despatch Unit" Width="110px"></asp:Label>
              <asp:TextBox ID="TextBox107" runat="server" Width="150px"></asp:TextBox>
              <asp:Label ID="Label384" runat="server" Font-Bold="True" ForeColor="Blue" Width="65px"></asp:Label>
              <br />
              <asp:Label ID="Label385" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Details" Width="110px"></asp:Label>
              <asp:TextBox ID="TextBox108" runat="server" Width="300px"></asp:TextBox>
              <br />
              <asp:Label ID="Label387" runat="server" Font-Bold="True" ForeColor="Blue" Text="Net Weight(Mt)" Width="110px"></asp:Label>
              <asp:TextBox ID="TextBox110" runat="server" Width="150px"></asp:TextBox>
              <br />
              &nbsp;<asp:Label ID="Label388" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
              &nbsp;&nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button42" runat="server" Text="CANCEL" Width="75px" CssClass="bottomstyle" Font-Size="Small" />
              <asp:Button ID="Button43" runat="server" Text="ADD" Width="75px" CssClass="bottomstyle" Font-Size="Small" />
              <asp:Button ID="Button44" runat="server" Text="SAVE" Width="75px" CssClass="bottomstyle" Font-Size="Small" />
              <br />
              <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Times New Roman" ShowHeaderWhenEmpty="True" Width="100%" BackColor="White">
                  <Columns>
                      <asp:BoundField DataField="mat_sl_no" HeaderText="Sl No" />
                      <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                      <asp:BoundField DataField="MAT_NAME" HeaderText="Item Name" />
                      <asp:BoundField DataField="ITEM_AU" HeaderText="A / U" />
                      <asp:BoundField DataField="ITEM_QTY_PCS" HeaderText="Item Qty" />
                      <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Item Qty (Mt)" />
                      <asp:BoundField DataField="Packing Details" HeaderText="Packing Details" />
                      <asp:BoundField DataField="ASS_VALUE" HeaderText="Assessable Value" />
                  </Columns>
                  <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                  <HeaderStyle BackColor="#990000" BorderColor="Blue" BorderStyle="Double" BorderWidth="2px" Font-Bold="True" ForeColor="#FFFFCC" />
                  <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                  <RowStyle BackColor="White" BorderColor="Lime" BorderStyle="Ridge" ForeColor="#330099" />
                  <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                  <SortedAscendingCellStyle BackColor="#FEFCEB" />
                  <SortedAscendingHeaderStyle BackColor="#AF0101" />
                  <SortedDescendingCellStyle BackColor="#F6F0C0" />
                  <SortedDescendingHeaderStyle BackColor="#7E0000" />
              </asp:GridView>
              <asp:Label ID="Label389" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Disc Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox111" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label390" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Assble Val" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox112" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
               <br />
              <asp:Label ID="Label391" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Freight" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox113" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
              <asp:Label ID="Label392" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="B.E.D" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox114" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label393" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="P&amp;F Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox115" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Label ID="Label394" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Cess" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox116" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label395" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="T.Tax Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox117" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
              <asp:Label ID="Label396" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="HS Cess" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox118" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
              <br />
              <asp:Label ID="Label397" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="TCS Val" Width="65px"></asp:Label>
              <asp:TextBox ID="TextBox119" runat="server" ReadOnly="True" Width="144px">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label398" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="VAT/C.S.T. " Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox120" runat="server" Width="120px" ReadOnly="True">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label399" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Blue" style="text-align: left" Text="Round Off" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox121" runat="server" Width="120px" ReadOnly="True">0.00</asp:TextBox>
              &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label400" runat="server" Font-Bold="True" Font-Size="Smaller" ForeColor="Red" style="text-align: left" Text="TOTAL VAL" Width="80px"></asp:Label>
              <asp:TextBox ID="TextBox122" runat="server" ReadOnly="True" Width="120px">0.00</asp:TextBox>
              <br />
          </asp:Panel>
            <asp:Panel ID="Panel8" runat="server" BorderColor="Red" BorderStyle="Groove" Font-Names="Times New Roman" Height="146px" style="text-align: left" Width="416px" CssClass="brds">
              <asp:Label ID="Label401" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" style="text-align: center" Text="SALE ORDER SELECTION" Width="100%" CssClass="brds"></asp:Label>
              <br />
              <br />
              <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-" Width="80px"></asp:Label>
             
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







            </div> 
        </center> 

</asp:Content>
