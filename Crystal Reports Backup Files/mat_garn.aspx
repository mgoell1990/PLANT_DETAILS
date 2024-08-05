<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="mat_garn.aspx.vb" Inherits="PLANT_DETAILS.mat_garn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .checkbox {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
        <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">      
   <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>

    <center>
        <div runat ="server" style ="min-height :600px;">
            <asp:Panel ID="garn_panel" runat="server" BorderColor="#FF3399" BorderStyle="Groove" BorderWidth="5px" CssClass="brds" Font-Names="Times New Roman" Font-Size="X-Small" Width="910px">
                <div id="fastdivision0" aria-orientation="vertical" aria-selected="undefined" style="   height: 424px; ">
                    <div style="border-right: 5px groove #FF0066; float:left; text-align: left; width: 285px; height: 424px; line-height: 20px; font-size: small;">
                        <asp:Panel ID="Panel7" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px" style="text-align: center" Width="100%">
                            <asp:Label ID="Label399" runat="server" Font-Bold="True" Font-Size="Medium" Text="GOODS ACCEPTANCE AND RECEIPT NOTE"></asp:Label>
                        </asp:Panel>
           <br />
           <br />
                        &nbsp;<asp:Label ID="Label400" runat="server" Font-Bold="True" ForeColor="Blue" Text="GARN NO" Width="120px"></asp:Label>
                        <asp:TextBox ID="GARN_NO_TextBox" runat="server" BackColor="Red" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px"></asp:TextBox>
                        &nbsp;<asp:Label ID="Label401" runat="server" Font-Bold="True" ForeColor="Blue" Text="CRR No" Width="120px"></asp:Label>
                        <asp:DropDownList ID="garn_crrnoDropDownList" runat="server" AutoPostBack="True" Font-Names="Times New Roman" Width="120px">
                        </asp:DropDownList>
                 <br />
                        &nbsp;<asp:Label ID="Label373" runat="server" Font-Bold="True" ForeColor="Blue" Text="MAT SLNo" Width="120px"></asp:Label>
                        <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Font-Names="Times New Roman" Width="120px">
                        </asp:DropDownList>
           <br />
                        &nbsp;<asp:Panel ID="Panel12" runat="server" BackColor="#4686F0" style="text-align: center">
                            <asp:Label ID="Label420" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="DETAILS"></asp:Label>
                        </asp:Panel>
                 <br />
                        <asp:Label ID="Label397" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="P.O. No" Width="80px"></asp:Label>
                        <asp:Label ID="Label398" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                 <br />
                        <asp:Label ID="Label421" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="AMD No" Width="80px"></asp:Label>
                        <asp:Label ID="Label422" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                 <br />
                        <asp:Label ID="Label395" runat="server" Font-Size="Small" ForeColor="Blue" Text="SUPL Name" Width="80px"></asp:Label>
                        <asp:Label ID="Label396" runat="server" Font-Size="XX-Small" ForeColor="#FF0066"></asp:Label>
           <br />
                        <asp:Label ID="Label391" runat="server" Font-Size="Small" ForeColor="Blue" Text="MAT Code" Width="80px"></asp:Label>
                        <asp:Label ID="Label392" runat="server" Font-Size="XX-Small" ForeColor="#FF0066"></asp:Label>
           <br />
                        <asp:Label ID="Label393" runat="server" Font-Size="Small" ForeColor="Blue" Text="MAT Name" Width="80px"></asp:Label>
                        <asp:Label ID="Label394" runat="server" Font-Size="XX-Small" ForeColor="#FF0066"></asp:Label>
                 <br />
                        <asp:Label ID="Label423" runat="server" Font-Size="Small" ForeColor="Blue" Text="Transporter" Width="80px"></asp:Label>
                        <asp:Label ID="Label424" runat="server" Font-Size="Small" ForeColor="#FF0066"></asp:Label>
                 <br />
                        <asp:Label ID="Label461" runat="server" ForeColor="Blue" Text="Work Sl No" Width="80px"></asp:Label>
                        <asp:Label ID="Label462" runat="server" ForeColor="#FF0066"></asp:Label>
           <br />
                        <asp:Label ID="GARN_ERR_LABLE" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Height="16px" Visible="False" Width="280px"></asp:Label>
                 <br />
                        <asp:Button ID="Button44" runat="server" CssClass="bottomstyle" Text="ADD" Width="80px" />
                        <asp:Button ID="Button3" runat="server" CssClass="bottomstyle" Text="Button" Width="80px" />
                        <asp:Button ID="Button5" runat="server" CssClass="bottomstyle" Text="SAVE" Width="80px" />
                    </div>
                    <div runat ="server" style ="float :right; width: 619px; text-align: left;">
                        <asp:Panel ID="Panel8" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px" Width="100%" CssClass="brds">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label402" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER ORDER"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label403" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER CHALAN"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
                        &nbsp;<br />
                        &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox120" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label405" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox147" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                 <br />
                        &nbsp;<asp:Label ID="Label406" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox124" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label407" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox149" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                        <asp:Label ID="Label468" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
         <br />
                        &nbsp;<asp:Label ID="Label408" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox121" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label409" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox151" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                        <asp:Label ID="Label467" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
         <br />
                        &nbsp;<asp:Label ID="Label410" runat="server" Font-Bold="True" ForeColor="Blue" Text="Excise " Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox122" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label411" runat="server" Font-Bold="True" ForeColor="Blue" Text="Excise " Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox153" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                        <asp:Label ID="Label469" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
         <br />
                        &nbsp;<asp:Label ID="Label412" runat="server" Font-Bold="True" ForeColor="Blue" Text="VAT %" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox123" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label413" runat="server" Font-Bold="True" ForeColor="Blue" Text="VAT %" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox155" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
         <br />
                        &nbsp;<asp:Label ID="Label414" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox125" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label415" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox157" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                        <asp:Label ID="Label466" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
         <br />
                        &nbsp;<asp:Label ID="Label416" runat="server" Font-Bold="True" ForeColor="Blue" Text="Entry Tax %" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox137" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label417" runat="server" Font-Bold="True" ForeColor="Blue" Text="Entry Tax %" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox159" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
         <br />
                        &nbsp;<asp:Label ID="Label387" runat="server" Font-Bold="True" ForeColor="Blue" Text="Late Delivery %" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox142" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label388" runat="server" Font-Bold="True" ForeColor="Blue" Text="Late Delivery %" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox143" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                        <asp:Label ID="Label419" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Visible="False"></asp:Label>
                        &nbsp;<asp:Label ID="Label384" runat="server" Font-Bold="True" ForeColor="Blue" Text="Local Freight" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox160" runat="server" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
                        &nbsp;<asp:Label ID="Label385" runat="server" Font-Bold="True" ForeColor="Blue" Text="Analytical Charge P/U" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox161" runat="server" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
                        &nbsp;<br />&nbsp;<asp:Label ID="Label386" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality P/U Mat" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox162" runat="server" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
                        <asp:CheckBox ID="CheckBox1" runat="server" ForeColor="Blue" Text="Percentege" Width="200px" CssClass="checkbox" />
                 <br />
                        <asp:Label ID="Label418" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" style="text-align: center" Text="OTHER CHARGE" Width="100%" CssClass="brds"></asp:Label>
                 <br />
                        &nbsp;<asp:Label ID="Label465" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality For Ttans" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox175" runat="server" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
                 <br />
                 <br />
                        <asp:Label ID="Label389" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue" Text="Remarks For Penality :-" Width="280px"></asp:Label>
                        &nbsp;<asp:Label ID="Label390" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Small" ForeColor="Blue" Text="Remarks For Modify :-"></asp:Label>
                 <br />
                        <asp:TextBox ID="TextBox144" runat="server" Font-Names="Times New Roman" Width="295px"></asp:TextBox>
                        <asp:TextBox ID="TextBox145" runat="server" Font-Names="Times New Roman" Width="300px"></asp:TextBox>
                 <br />
                 <br />
                        <asp:Label ID="Label451" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue" Text="Any Comments For GARN :-"></asp:Label>
                 <br />
                        <asp:TextBox ID="TextBox173" runat="server" Font-Names="Times New Roman" Width="600px"></asp:TextBox>
                    </div>
                </div>
                <asp:Panel ID="Panel10" runat="server" BackColor="#4686F0" ForeColor="White" Height="25px" style="text-align: center" Width="100%">
                    <asp:Label ID="Label323" runat="server" Font-Bold="True" Font-Size="Medium" Text="MATERIAL DETAILS"></asp:Label>
                    &nbsp;</asp:Panel>
                <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Width="903px">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="200%" Font-Size="Small">
                      <Columns>
                     <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
                     <asp:BoundField DataField="PO_NO" HeaderText="PO No" />
                     <asp:BoundField DataField="AMD_NO" HeaderText="AMD No" />
                     <asp:BoundField DataField="MAT_SLNO" HeaderText="MAT SLNo" />
                     <asp:BoundField DataField="MAT_CODE" HeaderText="MAT. Code" />
                     <asp:BoundField DataField="MAT_NAME" HeaderText="MAT. Name" >
                          </asp:BoundField>
                     <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                     <asp:BoundField DataField="MAT_QTY" HeaderText="ORD. Qty." />
                     <asp:BoundField DataField="CHLN_NO" HeaderText="CHLN No" />
                     <asp:BoundField DataField="MAT_CHALAN_QTY" HeaderText="CHLN. Qty." />
                     <asp:BoundField DataField="MAT_RCD_QTY" HeaderText="RCVD. Qty." />
                     <asp:BoundField DataField="MAT_REJ_QTY" HeaderText="REJ. Qty." />
                     <asp:TemplateField HeaderText="Accept Qty."></asp:TemplateField>
                     <asp:BoundField DataField="MAT_EXCE" HeaderText="Excess Qty." />
                     <asp:BoundField DataField="MAT_BAL_QTY" HeaderText="BAL. Qty." />
                     <asp:TemplateField HeaderText="Unit Rate"></asp:TemplateField>
                     <asp:templatefield HeaderText="Discount"></asp:templatefield>
                     <asp:TemplateField HeaderText="P&amp;F"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Excise Duty"></asp:TemplateField>
                     <asp:TemplateField HeaderText="VAT/CST"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Freight"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Local Fright"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Penality"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Entry Tax"></asp:TemplateField>
                     <asp:TemplateField HeaderText="L.D. Charge"></asp:TemplateField>
                     <asp:templatefield HeaderText="Mat Value"></asp:templatefield>
                     <asp:TemplateField HeaderText="Transport Charge"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Shortage Amount"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Loss On ED"></asp:TemplateField>
                     <asp:TemplateField HeaderText="Wt Var Val"></asp:TemplateField>
                     <asp:BoundField DataField="INSP_NOTE" HeaderText="Remarks" />
                        </Columns>
                     </asp:GridView>
                </asp:Panel>
        
         
        <br />
            <br />
                </asp:Panel> 
        </div>
    </center>
</asp:Content>
 