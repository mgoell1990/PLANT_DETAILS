<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true"  CodeBehind="inv_value.aspx.vb" Inherits="PLANT_DETAILS.inv_value" %>
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
            <asp:Panel ID="Panel30" runat="server" BorderColor="Fuchsia" BorderStyle="Groove" Font-Size="Small" style="text-align: left" Width="800px" Font-Names="Times New Roman">
                  <asp:Panel ID="Panel31" runat="server" BackColor="#4686F0" Height="60px">
                      <asp:Label ID="Label570" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :40px" Text="BILL PASS" Height ="40px" Width="100%"></asp:Label>
                  </asp:Panel>
                  <br />
                  <asp:Label ID="Label571" runat="server" ForeColor="Blue" Text="Token No" Width="100px"></asp:Label>
                  <asp:DropDownList ID="DropDownList40" runat="server" AutoPostBack="True" Width="120px">
                  </asp:DropDownList>
                  &nbsp;<br />
                   
                    <asp:Label ID="Label581" runat="server" ForeColor="Blue" Text="Order No" Width="100px"></asp:Label>
                  <asp:TextBox ID="TextBox171" runat="server" Width="120px" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                  &nbsp;<asp:Label ID="Label578" runat="server" Font-Bold="False" ForeColor="Blue" Text="Party" Width="110px"></asp:Label>
                  <asp:TextBox ID="TextBox168" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True" Width="300px"></asp:TextBox>
                  &nbsp;&nbsp;&nbsp;
                   <br />
                <asp:Label ID="Label568" runat="server" Font-Bold="False" ForeColor="Blue" Text="Inv. No" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox160" runat="server" Width="120px" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                      &nbsp;<asp:Label ID="Label569" runat="server" Font-Bold="False" ForeColor="Blue" Text="Date" Width="110px"></asp:Label>
                      <asp:TextBox ID="TextBox161" runat="server" Width="80px" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                   <br />
                    
                  <br />
                  <div runat ="server" style ="float :right ">
 <asp:Button ID="Button54" runat="server" Text="CANCEL" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                  <asp:Button ID="Button56" runat="server" Text="SUBMIT" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                  <asp:Button ID="Button55" runat="server" Text="NEXT" Width="80px" CssClass="bottomstyle" Font-Size="Small" />
                  </div><br />
                  <asp:Label ID="Label593" runat="server" Font-Bold="True" Text="&lt;marquee&gt;If Once  Generated It can't be changed&lt;/marquee&gt;" ForeColor="Blue" Font-Size="Medium"></asp:Label>
                  <br />
              </asp:Panel>


            <asp:Panel ID="mb_panel" runat="server" BorderColor="#FF3399" BorderStyle="Groove" BorderWidth="5px" Font-Names="Times New Roman" Font-Size="X-Small" Width="907px" Visible="False" CssClass="brds">
              <div id="fastdivision0" aria-orientation="vertical" aria-selected="undefined" style=" width: 905px;  height: 424px; ">
                  <div style="border-right: 5px groove #FF0066; float:left; text-align: left; width: 285px; height: 424px; line-height: 20px; font-size: small;">
                      <asp:Panel ID="Panel26" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px" style="text-align: center" Width="100%">
                          <asp:Label ID="Label399" runat="server" Font-Bold="True" Font-Size="Medium" Text="MESUREMENT OF WORKS VALUATION"></asp:Label>
                      </asp:Panel>
                      <br />
                      &nbsp;<asp:Label ID="Label401" runat="server" Font-Bold="True" ForeColor="Blue" Text="M.B. No" Width="80px"></asp:Label>
                      <asp:DropDownList ID="garn_crrnoDropDownList" runat="server" AutoPostBack="True" Width="120px">
                      </asp:DropDownList>
                      <br />
                      &nbsp;<asp:Label ID="Label373" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work SLNo" Width="80px"></asp:Label>
                      <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Width="120px">
                      </asp:DropDownList>
           <br />
                      &nbsp;<br /> <asp:Panel ID="Panel12" runat="server" BackColor="#4686F0" style="text-align: center">
                          <asp:Label ID="Label420" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="DETAILS"></asp:Label>
                      </asp:Panel>
                 <br />
                      <asp:Label ID="Label397" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Order No" Width="80px"></asp:Label>
                      <asp:Label ID="Label398" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                 <br />
                      <asp:Label ID="Label421" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="AMD No" Width="80px"></asp:Label>
                      <asp:Label ID="Label422" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                 <br />
                      <asp:Label ID="Label395" runat="server" Font-Size="Small" ForeColor="Blue" Text="SUPL Name" Width="80px"></asp:Label>
                      <asp:Label ID="Label396" runat="server" Font-Size="Small" ForeColor="#FF0066"></asp:Label>
           <br />
                    <br />
                      <asp:Label ID="GARN_ERR_LABLE" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Height="16px" Visible="False" Width="280px"></asp:Label>
                 <br />
                      <asp:Button ID="Button49" runat="server" Text="ADD" Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="Button50" runat="server" Text="Button" Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="Button51" runat="server" Text="CLOSE" Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="Button52" runat="server" Text="SAVE" Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                      <br />
                      <asp:Panel ID="Panel37" runat="server" BackColor="#CCFFFF" Height="134px" Visible="False" Width="276px">
                          <asp:Label ID="Label618" runat="server" BorderColor="Red" BorderStyle="Double" Font-Bold="True" ForeColor="Blue" style="text-align: center" Text="ARE YOU SURE?" Width="100%"></asp:Label>
                          <br />
                          <br />
                          <asp:Label ID="Label42" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                          <asp:TextBox ID="TextBox172" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                          <br />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="Button57" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" />
                          <br />
                          <asp:Label ID="Label43" runat="server" ForeColor="Red"></asp:Label>
                      </asp:Panel>
                  </div>
                  <div runat ="server"  aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="float:left; border-bottom-width: medium;  width: 613px; font-family: 'Times New Roman', Times, serif; font-size: small; height: 422px; line-height: 10px; text-align: left;">
                      <asp:Panel ID="Panel27" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px" Width="100%">
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label402" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER ORDER"></asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label403" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER VALUATION"></asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
                      &nbsp;<br />
                      &nbsp;<asp:Label ID="Label404" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox120" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label405" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox147" runat="server" Width="120px"></asp:TextBox>
                 <br />
                      &nbsp;<asp:Label ID="Label406" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox124" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label407" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox149" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label408" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat. Rate P/U" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox121" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label562" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat. Rate P/U" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox151" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label563" runat="server" Font-Bold="True" ForeColor="Blue" Text="Service Tax(P) %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox122" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label564" runat="server" Font-Bold="True" ForeColor="Blue" Text="Service Tax(P) %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox153" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label531" runat="server" Font-Bold="True" ForeColor="Blue" Text="Service Tax(R) %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox123" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label565" runat="server" Font-Bold="True" ForeColor="Blue" Text="Service Tax(R) %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox155" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label412" runat="server" Font-Bold="True" ForeColor="Blue" Text="W.C. Tax %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox125" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label566" runat="server" Font-Bold="True" ForeColor="Blue" Text="W.C. Tax %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox157" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label416" runat="server" Font-Bold="True" ForeColor="Blue" Text="T.D.S. %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox137" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label536" runat="server" Font-Bold="True" ForeColor="Blue" Text="T.D.S. %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox159" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label387" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality Rs." Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox142" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label567" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality Rs." Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox143" runat="server" Width="120px"></asp:TextBox>
                 <br />
                 <br />
                      <asp:Panel ID="Panel28" runat="server" BackColor="#4686F0" ForeColor="White" Height="30px" style="text-align: center" Width="100%">
                        <br /> 
                          <asp:Label ID="Label418" runat="server" Font-Bold="True" Font-Size="Medium" Text="REMARKS"></asp:Label>
                      </asp:Panel>
                      &nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label389" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue" Text="Remarks For Penality :-" Width="280px"></asp:Label>
                      &nbsp;<asp:Label ID="Label390" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Small" ForeColor="Blue" Text="Remarks For Modify :-"></asp:Label>
                 <br />
         <br />
                      <asp:TextBox ID="TextBox144" runat="server" Height="91px" TextMode="MultiLine" Width="295px"></asp:TextBox>
                      <asp:TextBox ID="TextBox145" runat="server" Height="91px"  TextMode="MultiLine" Width="300px"></asp:TextBox>
                 <br />
                 <br />
                  </div>
              </div>
              <asp:Panel ID="Panel10" runat="server" BackColor="#4686F0" ForeColor="White" Height="25px" style="text-align: center" Width="100%">
                  <asp:Label ID="Label323" runat="server" Font-Bold="True" Font-Size="Medium" Text="WORK DETAILS"></asp:Label>
                  &nbsp;</asp:Panel>
              <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Width="900px">
                  <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Font-Size="Small" ShowHeaderWhenEmpty="True" Width="200%">
                      <Columns>
                          <asp:BoundField DataField="mb_no" HeaderText="M.B. No" />
                          <asp:BoundField DataField="mb_date" HeaderText="M.B. Date" />
                          <asp:BoundField DataField="po_no" HeaderText="Order No" />
                          <asp:BoundField DataField="wo_slno" HeaderText="Work Sl No" />
                          <asp:BoundField DataField="w_name" HeaderText="Job Name" />
                          <asp:BoundField DataField="w_au" HeaderText="A/U" />
                          <asp:BoundField DataField="from_date" HeaderText="From Date" />
                          <asp:BoundField DataField="to_date" HeaderText="To Date" />
                          <asp:BoundField DataField="work_qty" HeaderText="Work Qty" />
                          <asp:BoundField DataField="rqd_qty" HeaderText="Rqd. Qty." />
                          <asp:BoundField DataField="bal_qty" HeaderText="Bal. Qty." />
                          <asp:BoundField DataField="total_amt" HeaderText="PROV. S. Charge" />
                          <asp:BoundField DataField="mat_rate" HeaderText="PROV. Mat. Rate" />
                          <asp:TemplateField HeaderText="Party Payment"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Final Mat. Charge"></asp:TemplateField>
                          <asp:BoundField DataField="pen_amt" HeaderText="Penality" />
                          <asp:BoundField DataField="st_amt_p" HeaderText="Service Tax(P)" />
                          <asp:BoundField DataField="st_amt_r" HeaderText="Service Tax(R)" />
                          <asp:BoundField DataField="wct_amt" HeaderText="W.C. Tax" />
                          <asp:BoundField DataField="it_amt" HeaderText="T.D.S. Tax" />
                          <asp:TemplateField HeaderText="S.D."></asp:TemplateField>
                          <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                      </Columns>
                      <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                      <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="XX-Small" ForeColor="#FFFFCC" />
                      <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                      <RowStyle BackColor="White" ForeColor="#330099" />
                      <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                      <SortedAscendingCellStyle BackColor="#FEFCEB" />
                      <SortedAscendingHeaderStyle BackColor="#AF0101" />
                      <SortedDescendingCellStyle BackColor="#F6F0C0" />
                      <SortedDescendingHeaderStyle BackColor="#7E0000" />
                  </asp:GridView>
        </asp:Panel>
        </asp:Panel>

            <asp:Panel ID="garn_value" runat="server" BorderColor="#FF3399" BorderStyle="Groove" BorderWidth="5px" Font-Names="Times New Roman" Font-Size="X-Small" Width="910px" Visible="False" CssClass="brds">
              <div id="fastdivision3" aria-orientation="vertical" aria-selected="undefined" style=" width: 905px;  height: 488px; ">
                  <div style="border-right: 5px groove #FF0066; float:left; text-align: left; width: 285px; height: 485px; line-height: 20px; font-size: small;">
                      <asp:Panel ID="Panel32" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px" style="text-align: center" Width="280px">
                          <asp:Label ID="Label594" runat="server" Font-Bold="True" Font-Size="Medium" Text="GOODS VALUATION"></asp:Label>
                      </asp:Panel>
                <br />
                <br />
                      &nbsp;<asp:Label ID="Label400" runat="server" Font-Bold="True" ForeColor="Blue" Text="GARN NO" Width="120px"></asp:Label>
                      <asp:DropDownList ID="M_garn_crrnoDropDownList" runat="server" AutoPostBack="True" Width="120px">
                      </asp:DropDownList>
                      <br />
                      &nbsp;<asp:Label ID="Label596" runat="server" Font-Bold="True" ForeColor="Blue" Text="MAT SLNo" Width="120px"></asp:Label>
                      <asp:DropDownList ID="M_DropDownList6" runat="server" AutoPostBack="True" Width="120px">
                      </asp:DropDownList>
                <br />
                      &nbsp;<asp:Panel ID="Panel33" runat="server" BackColor="#4686F0" style="text-align: center">
                          <asp:Label ID="Label597" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="DETAILS"></asp:Label>
                      </asp:Panel>
                <br />
                      <asp:Label ID="Label598" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="P.O. No" Width="80px"></asp:Label>
                      <asp:Label ID="M_Label398" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                <br />
                      <asp:Label ID="Label599" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="AMD No" Width="80px"></asp:Label>
                      <asp:Label ID="M_Label422" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                <br />
                      <asp:Label ID="Label600" runat="server" Font-Size="Small" ForeColor="Blue" Text="SUPL Name" Width="80px"></asp:Label>
                      <asp:Label ID="M_Label396" runat="server" Font-Size="Small" ForeColor="#FF0066"></asp:Label>
                <br />
                      <asp:Label ID="Label391" runat="server" Font-Size="Small" ForeColor="Blue" Text="MAT Code" Width="80px"></asp:Label>
                      <asp:Label ID="M_Label392" runat="server" Font-Size="Small" ForeColor="#FF0066"></asp:Label>
                <br />
                      <asp:Label ID="Label393" runat="server" Font-Size="Small" ForeColor="Blue" Text="MAT Name" Width="80px"></asp:Label>
                      <asp:Label ID="M_Label394" runat="server" Font-Size="Small" ForeColor="#FF0066"></asp:Label>
                <br />
                      <asp:Label ID="Label423" runat="server" Font-Size="Small" ForeColor="Blue" Text="Transporter" Width="80px"></asp:Label>
                      <asp:Label ID="M_Label424" runat="server" Font-Size="Small" ForeColor="#FF0066"></asp:Label>
                <br />
                      <asp:Label ID="Label461" runat="server" ForeColor="Blue" Text="Work Sl No" Width="80px"></asp:Label>
                      <asp:Label ID="M_Label462" runat="server" ForeColor="#FF0066"></asp:Label>
                <br />
                      <asp:Label ID="M_GARN_ERR_LABLE" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Height="16px" Visible="False" Width="280px"></asp:Label>
                <br />
                      <asp:Button ID="M_Button44" runat="server" Text="ADD" Font-Size="Smaller" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="M_Button2" runat="server" Text="Button" Font-Size="Smaller" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="M_Button3" runat="server" Text="CLOSE" Font-Size="Smaller" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="M_Button5" runat="server" Text="SAVE" Font-Size="Smaller" Width="60px" CssClass="bottomstyle" />
                      <asp:Panel ID="Panel38" runat="server" BackColor="#CCFFFF" Height="114px" Width="100%" Visible="False">
                          <asp:Label ID="Label619" runat="server" BorderColor="Red" BorderStyle="Double" Font-Bold="True" ForeColor="Blue" style="text-align: center" Text="ARE YOU SURE?" Width="100%"></asp:Label>
                          <br />
                          <asp:Label ID="Label620" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                          <asp:TextBox ID="TextBox173" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <br />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="Button58" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" />
                          <br />
                          <asp:Label ID="Label621" runat="server" ForeColor="Red"></asp:Label>
                      </asp:Panel>
                  </div>
                  <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="horizontal" style="float:left; border-bottom-width: medium; width: 615px; font-family: 'Times New Roman', Times, serif; height: 488px; text-align: left; font-size: small;">
                      <asp:Panel ID="Panel34" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px">
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Label ID="Label601" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER ORDER"></asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label602" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER VALUATION"></asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
                      &nbsp;<br /> &nbsp;<asp:Label ID="Label603" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox120" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label604" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox147" runat="server" Width="120px"></asp:TextBox>
                <br />
                      &nbsp;<asp:Label ID="Label605" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox124" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label606" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox149" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="M_Label468" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                <br />
                      &nbsp;<asp:Label ID="Label607" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox121" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label409" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox151" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="M_Label467" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                <br />
                      &nbsp;<asp:Label ID="Label410" runat="server" Font-Bold="True" ForeColor="Blue" Text="Excise " Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox122" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label411" runat="server" Font-Bold="True" ForeColor="Blue" Text="Excise " Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox153" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="M_Label469" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                <br />
                      &nbsp;<asp:Label ID="Label608" runat="server" Font-Bold="True" ForeColor="Blue" Text="VAT %" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox123" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label413" runat="server" Font-Bold="True" ForeColor="Blue" Text="VAT %" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox155" runat="server" Width="120px"></asp:TextBox>
                <br />
                      &nbsp;<asp:Label ID="Label414" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox125" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label415" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox157" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="M_Label466" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                <br />
                      &nbsp;<asp:Label ID="Label609" runat="server" Font-Bold="True" ForeColor="Blue" Text="Entry Tax %" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox137" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label417" runat="server" Font-Bold="True" ForeColor="Blue" Text="Entry Tax %" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox159" runat="server" Width="120px"></asp:TextBox>
                <br />
                      &nbsp;<asp:Label ID="Label610" runat="server" Font-Bold="True" ForeColor="Blue" Text="Late Delivery %" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox142" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label388" runat="server" Font-Bold="True" ForeColor="Blue" Text="Late Delivery %" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox143" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="M_Label419" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Visible="False"></asp:Label>
                      &nbsp;<br /> <asp:Label ID="Label384" runat="server" Font-Bold="True" ForeColor="Blue" Text="Local Freight P/U" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox160" runat="server" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label385" runat="server" Font-Bold="True" ForeColor="Blue" Text="Analytical Charge P/U" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox161" runat="server" Width="120px">0.00</asp:TextBox>
                      &nbsp;<br /> <asp:Label ID="Label386" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality P/U Mat" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox162" runat="server" Width="120px">0.00</asp:TextBox>
                      <asp:CheckBox ID="M_CheckBox1" runat="server" Font-Names="Times New Roman" ForeColor="Blue" Text="Percentege" Width="130px" />
                      <asp:CheckBox ID="M_CheckBox2" runat="server" Font-Names="Times New Roman" ForeColor="Blue" Text="Modvat Applicable" Font-Size="Smaller" Width="130px" />
                      <br />
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <br />
                      <br />
                      <asp:Label ID="Label611" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="OTHER CHARGE" BackColor="#4686F0" style="text-align: center" Width="100%"></asp:Label>
                      <br />
                      <asp:Label ID="Label612" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue" Text="Remarks For Penality :-" Width="280px"></asp:Label>
                      &nbsp;<asp:Label ID="Label613" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Small" ForeColor="Blue" Text="Remarks For Modify :-"></asp:Label>
                <br />
                      <asp:TextBox ID="M_TextBox144" runat="server" Height="35px" Width="295px"></asp:TextBox>
                      <asp:TextBox ID="M_TextBox145" runat="server" Height="34px" Width="295px"></asp:TextBox>
                <br />
                      <asp:Label ID="Label614" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue" Text="Any Comments For GARN :-"></asp:Label>
                <br />
                      <asp:TextBox ID="M_TextBox173" runat="server" CssClass="multitextboxclass" Height="30px" Width="600px"></asp:TextBox>
                  </div>
              </div>
              <asp:Panel ID="Panel35" runat="server" BackColor="#4686F0" ForeColor="White" Height="25px" style="text-align: center" Width="900px">
                  <asp:Label ID="Label615" runat="server" Font-Bold="True" Font-Size="Medium" Text="MATERIAL DETAILS"></asp:Label>
                  &nbsp;</asp:Panel>
              <asp:Panel ID="Panel36" runat="server" ScrollBars="Auto" Width="900px">
                  <asp:GridView ID="GridView210" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Font-Size="Small" ShowHeaderWhenEmpty="True" Width="200%">
                      <Columns>
                          <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
                          <asp:BoundField DataField="PO_NO" HeaderText="PO No" />
                          <asp:BoundField DataField="AMD_NO" HeaderText="AMD No" />
                          <asp:BoundField DataField="MAT_SLNO" HeaderText="MAT SLNo" />
                          <asp:BoundField DataField="MAT_CODE" HeaderText="MAT. Code" />
                          <asp:BoundField DataField="MAT_NAME" HeaderText="MAT. Name" />
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
                          <asp:templatefield HeaderText="Party Payment"></asp:templatefield>
                          <asp:BoundField DataField="TRANS_SHORT" HeaderText="Shortage Value" />
                          <asp:TemplateField HeaderText="Loss On ED"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Wt Var Val"></asp:TemplateField>
                          <asp:BoundField DataField="PROV_VALUE" HeaderText="PROV. Value" />
                          <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                          <asp:BoundField DataField="TRANS_CHARGE" HeaderText="Transport Charge" />
                          <asp:BoundField DataField="GARN_NOTE" HeaderText="Remarks" />
                      </Columns>
                      <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                      <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="XX-Small" ForeColor="#FFFFCC" />
                      <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                      <RowStyle BackColor="White" ForeColor="#330099" />
                      <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                      <SortedAscendingCellStyle BackColor="#FEFCEB" />
                      <SortedAscendingHeaderStyle BackColor="#AF0101" />
                      <SortedDescendingCellStyle BackColor="#F6F0C0" />
                      <SortedDescendingHeaderStyle BackColor="#7E0000" />
                  </asp:GridView>
              </asp:Panel>
          </asp:Panel>

            </div>
        </center>
</asp:Content>

