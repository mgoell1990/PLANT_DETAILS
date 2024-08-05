
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="WebForm2.aspx.vb" Inherits="PLANT_DETAILS.WebForm2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" Style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px;">
            <div runat ="server" style ="float :right ">
                <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                 <asp:CalendarExtender ID="TextBox32_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
            </div>

            <br />
            <br />
            <asp:Panel ID="Panel30" runat="server" BorderColor="Fuchsia" BorderStyle="Groove" Font-Size="Small" style="text-align: left" Width="800px" Font-Names="Times New Roman" Visible="False">
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
                      <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Width="120px" Height="18px">
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
                      <asp:Panel ID="Panel37" runat="server" BackColor="#CCFFFF" Height="120px" Visible="False" Width="276px">
                          <asp:Label ID="Label618" runat="server" BorderColor="Red" BorderStyle="Double" Font-Bold="True" ForeColor="Blue" style="text-align: center" Text="ARE YOU SURE?" Width="100%"></asp:Label>
                          <br />
                          <br />
                          <asp:Label ID="Label42" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                          <asp:TextBox ID="TextBox172" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                          <br />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="Button57" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
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
                      &nbsp;<asp:Label ID="Label563" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox122" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label564" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox153" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label531" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox123" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label565" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox155" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label412" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox125" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label566" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox157" runat="server" Width="120px"></asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label52" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox37" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox38" runat="server" Width="120px"></asp:TextBox>
                      &nbsp;<br /> 
                      &nbsp;<asp:Label ID="Label416" runat="server" Font-Bold="True" ForeColor="Blue" Text="T.D.S. %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox137" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label536" runat="server" Font-Bold="True" ForeColor="Blue" Text="T.D.S. %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox159" runat="server" Width="120px"></asp:TextBox>
                      <br />
                      &nbsp;<asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="L.D. Rs" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox2" runat="server" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label567" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality Rs." Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox143" runat="server" Width="120px"></asp:TextBox>
                      <br />
                      &nbsp;<asp:Label ID="Label626" runat="server" Font-Bold="True" ForeColor="Blue" Text="R.C.M." Width="120px"></asp:Label>
                      <asp:DropDownList ID="DropDownList43" runat="server" Width="125px">
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>Yes</asp:ListItem>
                          <asp:ListItem Selected="True">No</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label627" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.D. %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox179" runat="server" Width="120px"></asp:TextBox>
                      <br />
                      
                      &nbsp;<asp:Label ID="Label61" runat="server" Font-Bold="True" ForeColor="Blue" Text="Other Deduction(Rs)" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox44" runat="server" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label62" runat="server" Font-Bold="True" ForeColor="Blue" Text="Deduction Head" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox45" runat="server" Width="120px"></asp:TextBox>
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
                <script type="text/javascript">
                    $(function () {
                        $("[id$=TextBox45]").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    url: '<%=ResolveUrl("~/Service.asmx/ac_head")%>',
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
                          <asp:BoundField DataField="prov_amt" HeaderText="PROV. S. Charge" />
                          <asp:BoundField DataField="mat_rate" HeaderText="PROV. Mat. Rate" />
                          <asp:TemplateField HeaderText="Party Payment"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Final Mat. Charge"></asp:TemplateField>
                          <asp:BoundField DataField="pen_amt" HeaderText="Penality" />
                          <asp:BoundField DataField="sgst" HeaderText="SGST" />
                          <asp:BoundField DataField="cgst" HeaderText="CGST" />
                          <asp:BoundField DataField="igst" HeaderText="IGST" />
                          <asp:BoundField DataField="cess" HeaderText="CESS" />
                          <asp:BoundField DataField="sgst_liab" HeaderText="SGST Liab." />
                          <asp:BoundField DataField="cgst_liab" HeaderText="CGST Liab." />
                          <asp:BoundField DataField="igst_liab" HeaderText="IGST Liab." />
                          <asp:BoundField DataField="cess_liab" HeaderText="CESS Liab." />
                          <asp:BoundField DataField="it_amt" HeaderText="T.D.S. Tax" />
                          <asp:TemplateField HeaderText="S.D."></asp:TemplateField>
                          <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                          <asp:BoundField DataField="rcm" HeaderText="R.C.M." />
                          <asp:TemplateField HeaderText="L.D."></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm SGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm CGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm IGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm CESS"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Other deduction"></asp:TemplateField>
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

            <%--=================================== MB Panel END=====================================--%>
            <br />
            <br />
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
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="Button58" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                          <br />
                          <asp:Label ID="Label621" runat="server" ForeColor="Red"></asp:Label>
                      </asp:Panel>
                  </div>
                  <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="horizontal" style="float:left; border-bottom-width: medium; width: 615px; font-family: 'Times New Roman', Times, serif; height: 488px; text-align: left; font-size: xx-small;">
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
                      &nbsp;<asp:Label ID="Label410" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox11" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label411" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST" Width="130px"></asp:Label>
                        <asp:TextBox ID="TextBox12" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
         <br />
                        &nbsp;<asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox29" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label413" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST" Width="130px"></asp:Label>
                        <asp:TextBox ID="TextBox30" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
         <br />
                       &nbsp;<asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox31" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                       <asp:Label ID="Label27" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST" Width="130px"></asp:Label>
                        <asp:TextBox ID="TextBox32" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                       
                        
         <br />
                        &nbsp;<asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox33" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label49" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS" Width="130px"></asp:Label>
                        <asp:TextBox ID="TextBox34" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                        <br />
                        &nbsp;<asp:Label ID="Label50" runat="server" Font-Bold="True" ForeColor="Blue" Text="Analytical Charge P/U" Width="120px"></asp:Label>
                        <asp:TextBox ID="TextBox35" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                        <asp:Label ID="Label51" runat="server" Font-Bold="True" ForeColor="Blue" Text="Analytical Charge P/U" Width="130px"></asp:Label>
                        <asp:TextBox ID="TextBox36" runat="server" Font-Names="Times New Roman" Width="120px"></asp:TextBox>
                <br />
                      &nbsp;<asp:Label ID="Label414" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox125" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label415" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox157" runat="server" Width="120px"></asp:TextBox>
                      <asp:Label ID="M_Label466" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                <br />
                      &nbsp;<asp:Label ID="Label610" runat="server" Font-Bold="True" ForeColor="Blue" Text="Late Delivery %" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox142" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label388" runat="server" Font-Bold="True" ForeColor="Blue" Text="Late Delivery(Rs)" Width="130px"></asp:Label>
                      <asp:TextBox ID="M_TextBox143" runat="server" Width="120px"></asp:TextBox>
                      &nbsp;<asp:Label ID="M_Label419" runat="server" ForeColor="#CC0000"></asp:Label>
                      <br /> 
                      &nbsp;<asp:Label ID="Label384" runat="server" Font-Bold="True" ForeColor="Blue" Text="Local Freight P/U" Width="120px"></asp:Label>
                      <asp:TextBox ID="M_TextBox160" runat="server" Width="120px">0.00</asp:TextBox>
                      &nbsp;<asp:Label ID="Label628" runat="server" Font-Bold="True" ForeColor="Blue" Text="R.C.M." Width="130px"></asp:Label>
                      <asp:DropDownList ID="DropDownList44" runat="server" Width="120px">
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>Yes</asp:ListItem>
                          <asp:ListItem Selected="True">No</asp:ListItem>
                      </asp:DropDownList>
                      <br /> 
                      &nbsp;<asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.D. (Rs.)" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox3" runat="server" Width="120px">0.00</asp:TextBox>
                      <br /> <asp:Label ID="Label386" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality P/U Mat" Width="120px" style="height: 15px"></asp:Label>
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
                          <asp:TemplateField HeaderText="SGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="CGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="IGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Cess"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Freight"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Local Fright"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Penality"></asp:TemplateField>
                          <asp:TemplateField HeaderText="L.D. Charge"></asp:TemplateField>
                          <asp:templatefield HeaderText="Party Payment"></asp:templatefield>
                          <asp:BoundField DataField="TRANS_SHORT" HeaderText="Shortage Value" />
                          <asp:TemplateField HeaderText="Loss On ED"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Wt Var Val"></asp:TemplateField>
                          <asp:BoundField DataField="PROV_VALUE" HeaderText="PROV. Value" />
                          <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                          <asp:BoundField DataField="TRANS_CHARGE" HeaderText="Transport Charge" />
                          <asp:BoundField DataField="GARN_NOTE" HeaderText="Remarks" />
                          <asp:BoundField DataField="TOTAL_MT" HeaderText="Total Mt." />
                          <asp:TemplateField HeaderText="RCM SGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="RCM CGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="RCM IGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="RCM Cess"></asp:TemplateField>
                          <asp:TemplateField HeaderText="SD"></asp:TemplateField>


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

            <%--=================================== GARN Panel END=====================================--%>
            <br />
            <br />
            

            <asp:Panel ID="Panel1" runat="server" BorderColor="#FF3399" BorderStyle="Groove" BorderWidth="5px" Font-Names="Times New Roman" Font-Size="X-Small" Width="907px" CssClass="brds" Visible="True">
              <div id="fastdivision5" aria-orientation="vertical" style=" width: 905px; height: 460px;">
                  <div style="border-right: 5px groove #FF0066; float:left; text-align: left; width: 285px; line-height: 20px; height: 100%; font-size: small;">
                      <asp:Panel ID="Panel2" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px" style="text-align: center">
                          <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Medium" Text="TRANSPORTATION WORKS VALUATION"></asp:Label>
                      </asp:Panel>
                      <br />
                      &nbsp;<asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Text="Crr/Inv No" Width="100px"></asp:Label>
                      <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" Width="120px">
                      </asp:DropDownList>
                      <br />
                      &nbsp;<asp:Label ID="Label622" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work SLNo" Width="100px"></asp:Label>
                      <asp:DropDownList ID="DropDownList41" runat="server" Width="120px" AutoPostBack="True">
                      </asp:DropDownList>
                      
                      <br />
                      &nbsp;<asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox174" runat="server" Width="120px"></asp:TextBox>
                       <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox174" />
                      <br />
                      &nbsp;<asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="Blue" Text="To" Width="100px"></asp:Label>
                      <asp:TextBox ID="TextBox175" runat="server" Width="120px"></asp:TextBox>
                      <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox175" />
                      <br /> 
                      <asp:Panel ID="Panel3" runat="server" BackColor="#4686F0" style="text-align: center">
                          <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="White" Text="DETAILS"></asp:Label>
                      </asp:Panel>
                 <br />
                      <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Order No" Width="80px"></asp:Label>
                      <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                 <br />
                      <asp:Label ID="Label15" runat="server" Font-Size="Small" ForeColor="Blue" Text="SUPL Name" Width="80px"></asp:Label>
                      <asp:Label ID="Label16" runat="server" Font-Size="Small" ForeColor="#FF0066"></asp:Label>
           <br />
                      <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Height="16px" Visible="False" Width="280px"></asp:Label>
                      <br />
                 <br />
                      <div runat ="server" style="text-align: center" >
                      <asp:Button ID="Button2" runat="server" Text="ADD" Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="Button1" runat="server" Text="CAL." Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="Button3" runat="server" Text="CLOSE" Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                      <asp:Button ID="Button4" runat="server" Text="SAVE" Font-Size="Small" Width="60px" CssClass="bottomstyle" />
                 <br />
                      <%--<input id="chkAll" class="cbAll" runat="server" type="checkbox" />--%>
                     </div>
                     
                      <br />
                      <asp:Panel ID="Panel4" runat="server" BackColor="#CCFFFF" Height="134px" Visible="False" Width="276px">
                          <asp:Label ID="Label18" runat="server" BorderColor="Red" BorderStyle="Double" Font-Bold="True" ForeColor="Blue" style="text-align: center" Text="ARE YOU SURE?" Width="100%"></asp:Label>
                          <br />
                          <br />
                          <asp:Label ID="Label19" runat="server" ForeColor="Blue" Text="Auth. Password"></asp:Label>
                          <asp:TextBox ID="TextBox6" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                          <br />
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Button ID="Button5" runat="server" Font-Bold="True" ForeColor="Blue" Text="GO" Width="120px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                          <br />
                          <asp:Label ID="Label20" runat="server" ForeColor="Red"></asp:Label>
                      </asp:Panel>
                  </div>
                  <div runat ="server"  aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="float:left; border-bottom-width: medium;  width: 613px; font-family: 'Times New Roman', Times, serif; font-size: small; line-height: 10px; text-align: left;">
                      <asp:Panel ID="Panel5" runat="server" BackColor="#4686F0" ForeColor="White" Height="39px">
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER ORDER"></asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="Small" Text="AS PER VALUATION"></asp:Label>
                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Panel>
         <br />
                      &nbsp;<asp:Label ID="Label624" runat="server" Font-Bold="True" ForeColor="Blue" Text="Chln. Qty." Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox176" runat="server" Font-Bold="False" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label625" runat="server" Font-Bold="True" ForeColor="Blue" Text="Rcd Qty." Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox177" runat="server" Font-Bold="False" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox7" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Final Unit Price" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox8" runat="server" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox9" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox10" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label44" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing P/U" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox5" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing P/U" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox26" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label29" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox13" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
                      <asp:Label ID="Label30" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox14" runat="server" Font-Names="Times New Roman" Width="120px">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox15" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label32" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox16" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox17" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label34" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox18" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label35" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox19" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label36" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox20" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label37" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality Rs." Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox21" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label38" runat="server" Font-Bold="True" ForeColor="Blue" Text="Penality Rs." Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox22" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      
                      &nbsp;<asp:Label ID="Label41" runat="server" Font-Bold="True" ForeColor="Blue" Text="LD (Rs)" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox25" runat="server" Font-Bold="False" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label40" runat="server" Font-Bold="True" ForeColor="Blue" Text="TDS%" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox24" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      
                      &nbsp;<asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Text="R.C.M." Width="120px"></asp:Label>
                      <asp:DropDownList ID="DropDownList2" runat="server" Width="125px">
                          <asp:ListItem>Select</asp:ListItem>
                          <asp:ListItem>Yes</asp:ListItem>
                          <asp:ListItem>No</asp:ListItem>
                      </asp:DropDownList>
                      <asp:Label ID="Label54" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.D. %" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox4" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label39" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total Qty" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox23" runat="server" Font-Bold="False" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label56" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total Value" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox39" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
         <br />
                      &nbsp;<asp:Label ID="Label57" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total SGST" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox40" runat="server" Font-Bold="False" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label58" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total CGST" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox41" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
        <br />
                      &nbsp;<asp:Label ID="Label59" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total IGST" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox42" runat="server" Font-Bold="False" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label60" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total CESS" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox43" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
        <br />
                      &nbsp;<asp:Label ID="Label63" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total SD" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox46" runat="server" Font-Bold="False" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label64" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total IT" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox47" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
        <br />
                      &nbsp;<asp:Label ID="Label65" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total Penalty" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox48" runat="server" Font-Bold="False" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                      <asp:Label ID="Label66" runat="server" Font-Bold="True" ForeColor="Blue" Text="Total Final Payment" Width="120px"></asp:Label>
                      <asp:TextBox ID="TextBox49" runat="server" Width="120px" Font-Names="Times New Roman">0.00</asp:TextBox>
                     
         <br />
                      &nbsp;<asp:Panel ID="Panel6" runat="server" BackColor="#4686F0" ForeColor="White" Height="30px" style="text-align: center" Width="100%">
                          <br />
                          <asp:Label ID="Label45" runat="server" Font-Bold="True" Font-Size="Medium" Text="REMARKS"></asp:Label>
                      </asp:Panel>
                      &nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label46" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Blue" Text="Remarks For Penality :-" Width="280px"></asp:Label>
                      &nbsp;<asp:Label ID="Label47" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Small" ForeColor="Blue" Text="Remarks For Modify :-"></asp:Label>
                 <br />
         
                      <asp:TextBox ID="TextBox27" runat="server" Height="60px" TextMode="MultiLine" Width="295px"></asp:TextBox>
                      <asp:TextBox ID="TextBox28" runat="server" Height="60px"  TextMode="MultiLine" Width="300px"></asp:TextBox>
                 <br />
                 <br />
                  </div>
              </div>
                
              <asp:Panel ID="Panel7" runat="server" BackColor="#4686F0" ForeColor="White" Height="25px" style="text-align: center" Width="100%">
                  <asp:Label ID="Label48" runat="server" Font-Bold="True" Font-Size="Medium" Text="WORK DETAILS"></asp:Label>
                  &nbsp;</asp:Panel>
                
              <asp:Panel ID="Panel8" runat="server" ScrollBars="Auto" Width="900px" Height="214px">
                  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" Font-Size="Small" ShowHeaderWhenEmpty="True" Width="200%">
                      <Columns>
                          
                          <asp:TemplateField HeaderText="Select"> 
                          <ItemTemplate>
                          <asp:CheckBox ID="CheckBox1" runat="server"/>
                          </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="mb_no" HeaderText="MB No" />
                          <asp:BoundField DataField="mb_date" HeaderText="MB Date" />
                          <asp:BoundField DataField="po_no" HeaderText="Order No" />
                          <asp:BoundField DataField="wo_slno" HeaderText="Work Sl No" />
                          <asp:BoundField DataField="w_name" HeaderText="Job Name" />
                          <asp:BoundField DataField="w_au" HeaderText="A/U" />
                          <asp:BoundField DataField="from_date" HeaderText="From Date" />
                          <asp:BoundField DataField="to_date" HeaderText="To Date" />
                          <asp:BoundField DataField="rqd_qty" HeaderText="Chln. Qty" />
                          <asp:BoundField DataField="work_qty" HeaderText="Rcd/Send. Qty." />
                          <asp:TemplateField HeaderText="Unit Price"/>
                          <asp:BoundField DataField="prov_amt" HeaderText="PROV. S. Charge" />
                          <asp:TemplateField HeaderText="Party Payment"></asp:TemplateField>
                          <asp:BoundField DataField="pen_amt" HeaderText="Penality" />
                          <asp:BoundField DataField="sgst" HeaderText="SGST" />
                          <asp:BoundField DataField="cgst" HeaderText="CGST" />
                          <asp:BoundField DataField="igst" HeaderText="IGST" />
                          <asp:BoundField DataField="cess" HeaderText="CESS" />
                          <asp:BoundField DataField="sgst_liab" HeaderText="SGST Liab." />
                          <asp:BoundField DataField="cgst_liab" HeaderText="CGST Liab." />
                          <asp:BoundField DataField="igst_liab" HeaderText="IGST Liab." />
                          <asp:BoundField DataField="cess_liab" HeaderText="CESS Liab." />
                          <asp:BoundField DataField="it_amt" HeaderText="TDS Tax" />
                          <asp:TemplateField HeaderText="S.D."></asp:TemplateField>
                          <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                          <asp:BoundField DataField="rcm" HeaderText="R.C.M." />
                          <asp:TemplateField HeaderText="L.D."></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm CGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm SGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm IGST"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Rcm CESS"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Final Payment Amount"></asp:TemplateField>

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

            <%--=====================================Transporter Panel END=====================================--%>

         </div>   

        
        <%--<script type="text/javascript">
            $(function SelectAllCheckboxes1(chk) {
                                   
                $('#<%=GridView1.ClientID%>').find("input:CheckBox1").each(function () {
                    $(".CheckBox1").attr("checked", true);
                 });
                  });
    </script>--%>
        </center>
</asp:Content>

