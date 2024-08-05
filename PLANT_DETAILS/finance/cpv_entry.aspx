<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="cpv_entry.aspx.vb" Inherits="PLANT_DETAILS.cpv_entry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
    if ( window.history.replaceState ) {
        window.history.replaceState( null, null, window.location.href );
    }
</script>
    <center>
        <div runat ="server" style ="min-height :600px;">
            <div runat ="server" style ="float :right ">
                <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
            </div>
            <asp:Panel ID="rcd_Panel5" runat="server" BorderColor="Blue" BorderStyle="Groove" Width="805px" Font-Names="Times New Roman" Font-Size="Small" style="text-align: left">
                <asp:Panel ID="Panel23" runat="server" BackColor="#4686F0" Height="54px">
                    <asp:Label ID="Label533" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center" Text="ENTRY FOR CASH PMNT VOUCHER" Width="630px"></asp:Label>
                </asp:Panel>
            <br />
                <asp:Label ID="Label534" runat="server" ForeColor="Blue" Text="Token No" Width="100px"></asp:Label>
                <asp:DropDownList ID="DropDownList38" runat="server" Width="124px" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label535" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#00CC00"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <br />
                <asp:Label ID="Label539" runat="server" ForeColor="Blue" Text="Date" Width="100px"></asp:Label>
                <asp:TextBox ID="TextBox92" runat="server" Width="120px" AutoCompleteType="Disabled"></asp:TextBox>
                <asp:CalendarExtender ID="TextBox92_CalendarExtender" runat="server" Enabled="True" CssClass ="red" Format ="dd-MM-yyyy" TargetControlID="TextBox92">
                </asp:CalendarExtender>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button38" runat="server" Font-Bold="True" ForeColor="Blue" Text="ADD" Width="70px" CssClass="bottomstyle" />
                <asp:Button ID="Button39" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="70px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                <asp:Button ID="Button40" runat="server" Font-Bold="True" ForeColor="Blue" Text="CANCEL" Width="70px" CssClass="bottomstyle" />
                <br />
                <asp:Label ID="Label655" runat="server" ForeColor="Blue" Text="Voucher No" Width="100px"></asp:Label>
                <asp:Label ID="Label656" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Medium" ForeColor="Blue"></asp:Label>

                &nbsp;<asp:GridView ID="GridView9" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ShowHeaderWhenEmpty="True" Width="801px" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:BoundField DataField="TOKEN_NO" HeaderText="TOKEN NO" />
                        <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL CODE" />
                        <asp:BoundField DataField="AC_NO" HeaderText="A/C HEAD" />
                        <asp:BoundField DataField="ac_description" HeaderText="A/C DESCRIPTION" />
                        <asp:BoundField DataField="AMOUNT_CR" HeaderText="AMOUNT" />
                        <asp:TemplateField HeaderText="C.P.V NO"></asp:TemplateField>
                        <asp:TemplateField HeaderText="PAYMENT DATE"></asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                <asp:Label ID="Label537" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label556" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Text="&lt;marquee&gt;If Once C.B.V. Generated It can't be changed&lt;/marquee&gt;"></asp:Label>
            <br />
            </asp:Panel>



            </div>
        </center>
</asp:Content>

