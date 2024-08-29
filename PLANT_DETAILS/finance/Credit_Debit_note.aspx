<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Credit_Debit_note.aspx.vb" Inherits="PLANT_DETAILS.Credit_Debit_note1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label321" runat="server" Text="Invoice Date"></asp:Label>
&nbsp;<asp:TextBox ID="txtInvoiceDate" runat="server"></asp:TextBox>
            <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Times New Roman">Select </asp:Label>
        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="txt" Width="150px" AutoPostBack="True">
            <asp:ListItem>Select</asp:ListItem> 
            <asp:ListItem>Credit Note</asp:ListItem>
            <asp:ListItem>Debit Note</asp:ListItem>
            <asp:ListItem>Print</asp:ListItem>
        </asp:DropDownList>
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
        <asp:Panel ID="Panel4" runat="server" BorderColor="#FF33CC" BorderStyle="Groove" style="text-align: left" Width="80%" Font-Size="Small">
            <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Small" Text="IRN No" Width="160px"></asp:Label>
            <asp:TextBox ID="TextBox6" runat="server" ReadOnly="True" Width="500px"></asp:TextBox>
            <br />
           <asp:Label ID="Label5" runat="server" Text="CREDIT/DEBIT NOTE NO." Font-Bold="True" style="text-align: left" Width="160px"></asp:Label>
           <asp:TextBox ID="TextBox65" runat="server" Width="125px" BackColor="Red" ForeColor="White"></asp:TextBox>
           <br />    
           <asp:Label ID="Label9" runat="server" Text="Fiscal Year" Font-Bold="True" style="text-align: left" Width="160px"></asp:Label>
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True">
            </asp:DropDownList>
           <br />    
           <asp:Label ID="Label2" runat="server" Text="Invoice No" Font-Bold="True" style="text-align: left" Width="160px"></asp:Label>
            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True">
            </asp:DropDownList>
             <br />    
           <asp:Label ID="Label6" runat="server" Text="Invoice Type" Font-Bold="True" style="text-align: left" Width="160px"></asp:Label>
           <asp:Label ID="Label7" runat="server" Font-Bold="True" style="text-align: left" Width="100px"></asp:Label>
           
            
            <br />
            <asp:Label ID="Label319" runat="server" Font-Bold="True" style="text-align: left" Text="Document Type" Width="160px"></asp:Label>
            <asp:Label ID="Label320" runat="server" Font-Bold="True" style="text-align: left" Width="100px"></asp:Label>
           
            
            <br />    
           <asp:Label ID="Label8" runat="server" Text="Taxable Amount" Font-Bold="True" style="text-align: left" Width="160px"></asp:Label>
           <asp:TextBox ID="TextBox2" runat="server" Width="150px"></asp:TextBox>
                

            <br />
            <asp:Label ID="Label318" runat="server" Font-Bold="True" style="text-align: left" Text="Notification" Width="160px"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox66" runat="server" Height="68px" TextMode="MultiLine" Width="471px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="Add" CssClass="bottomstyle" Width="80px"></asp:Button>
                 <asp:Button ID="Button2" runat="server" CssClass="bottomstyle" Text="Save" Width="80px" />
                 <br />
                <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Width="100%">
               <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="115%">
                      <Columns>

                          <asp:TemplateField HeaderText="Invoice No"></asp:TemplateField>
                          <asp:BoundField DataField="SO_NO" HeaderText="PO No" />
                          <asp:BoundField DataField="SO_Date" HeaderText="PO Date" DataFormatString="{0:dd/MM/yyyy}" />
                          <asp:BoundField DataField="PO_NO" HeaderText="Actual PO No" />
                          <asp:BoundField DataField="PO_Date" HeaderText="Actual PO Date" DataFormatString="{0:dd/MM/yyyy}" />
                          <asp:BoundField DataField="PARTY_CODE" HeaderText="Party Code" />
                          <asp:BoundField DataField="CONSIGN_CODE" HeaderText="Consignee Code" />
                          <asp:BoundField DataField="D_NAME" HeaderText="Party Name" />
                          <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat Sl. No" />
                          <asp:BoundField DataField="P_CODE" HeaderText="Mat. Code" />
                          <asp:BoundField DataField="P_DESC" HeaderText="Mat Name" />
                          <asp:TemplateField HeaderText="TAXABLE VALUE"></asp:TemplateField>
                          <asp:BoundField DataField="CGST_RATE" HeaderText="CGST" />
                          <asp:BoundField DataField="CGST_AMT" HeaderText="CGST AMT" />
                          <%--<asp:TemplateField HeaderText="CGST AMT"></asp:TemplateField>--%>
                          <asp:BoundField DataField="SGST_RATE" HeaderText="SGST" />
                          <asp:BoundField DataField="SGST_AMT" HeaderText="SGST AMT" />
                          <%--<asp:TemplateField HeaderText="SGST AMT"></asp:TemplateField>--%>
                          <asp:BoundField DataField="IGST_RATE" HeaderText="IGST" />
                          <asp:BoundField DataField="IGST_AMT" HeaderText="IGST AMT" />
                          <%--<asp:TemplateField HeaderText="IGST AMT"></asp:TemplateField>--%>
                          <%--<asp:TemplateField HeaderText="Total VALUE"></asp:TemplateField>--%>
                          <asp:BoundField DataField="TOTAL_AMT" HeaderText="TOTAL AMOUNT" />
                          
                          <%--<asp:TemplateField HeaderText="Taxable Value"></asp:TemplateField>
                          <asp:BoundField DataField="CGST_RATE" HeaderText="CGST" />
                          <asp:TemplateField HeaderText="CGST AMT"></asp:TemplateField>
                          <asp:BoundField DataField="SGST_RATE" HeaderText="SGST" />
                          <asp:TemplateField HeaderText="SGST AMT"></asp:TemplateField>
                          <asp:BoundField DataField="IGST_RATE" HeaderText="IGST" />
                          <asp:TemplateField HeaderText="IGST AMT"></asp:TemplateField>
                          <asp:TemplateField HeaderText="Total Value"></asp:TemplateField>--%>

                      </Columns>
                  </asp:GridView>
                    </asp:Panel>
            <br />
               <asp:Label ID="Label3" runat="server" ForeColor="#FF3300" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>

            <br />
            <asp:Label ID="Label317" runat="server" Font-Bold="True" ForeColor="Red" Text="Error Code : " Visible="False"></asp:Label>
            &nbsp;<asp:Label ID="txtEinvoiceErrorCode" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Label42" runat="server" Font-Bold="True" ForeColor="Red" Text="Error Message : " Visible="False"></asp:Label>
            &nbsp;<asp:Label ID="txtEinvoiceErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>

          </asp:Panel>
            </asp:View>


            <%--==============View 2 Started============--%>

            <asp:View ID="View2" runat="server">
        <asp:Panel ID="Panel1" runat="server" BorderColor="#FF33CC" BorderStyle="Groove" style="text-align: left" Width="500px" Font-Size="Small">
               <asp:Label ID="Label314" runat="server" BackColor="#3366FF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" ForeColor="White" style="text-align: center" Text="PRINT CREDIT/DEBIT INVOICE" Width="100%"></asp:Label>
                <br />
               <br />
               
            <br />
            <br />
               
                   <asp:Label ID="Label311" runat="server" Font-Bold="True" Text="Invoice No" Width="120px" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
             <asp:TextBox ID="txtInvSearch" runat="server" Width="140px"></asp:TextBox>
              <script type="text/javascript">
                  $(function () {
                      $("[id$=txtContactsSearch]").autocomplete({
                          source: function (request, response) {
                              $.ajax({
                                  url: '<%=ResolveUrl("~/Service.asmx/inv_no")%>',
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
                 
                   <asp:Label ID="Label316" runat="server" ForeColor="Red" Text="*"></asp:Label>
                 
                   <br />
                   <br />
                   <asp:Button ID="Button38" runat="server" Font-Bold="True" ForeColor="Blue" Text="PRINT EXTRA COPY" Font-Size="Small" />
                   <asp:Button ID="Button39" runat="server" Font-Bold="True" ForeColor="Blue" Text="VIEW PENDING PRINT" Font-Size="Small" />
                   <br />
                   <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="3px" CellPadding="4" EmptyDataText="No More Invoice To Print" Font-Names="Times New Roman" Font-Size="Smaller" Visible="False"    >
         <Columns>
             <asp:BoundField DataField="INV_NO" HeaderText="INVOICE NO" >
             <ItemStyle Font-Bold="True" ForeColor="Blue" />
             </asp:BoundField>
             <asp:TemplateField HeaderText="ORIGINAL">
             <ItemTemplate>
             <asp:linkButton ID="Button1" ForeColor ="BLUE" runat="server" Text='<%#Eval("PRINT_ORIGN")%>' Commandname ='<%#Eval("INV_NO")%>'   OnClick = "ORIGINAL" />
             </ItemTemplate> 
                 <ItemStyle ForeColor="Blue" Width="100px" />
                 </asp:TemplateField>
             <asp:TemplateField HeaderText="DUPLICATE">
               <ItemTemplate > 
             <asp:linkButton ID="Button2" ForeColor ="BLUE" runat="server" Text='<%#Eval("PRINT_TRANS")%>' Commandname = '<%#Eval("INV_NO")%>'  OnClick = "DUPLICATE" />
             </ItemTemplate> 
                 <ItemStyle ForeColor="Blue" Width="100px" />
                 </asp:TemplateField>
             <asp:TemplateField HeaderText="TRIPLICATE">
              <ItemTemplate > 
             <asp:linkButton ID="Button3" ForeColor ="BLUE" runat="server" Text='<%#Eval("PRINT_ASSAE")%>' Commandname = '<%#Eval("INV_NO")%>'  OnClick = "TRIPLICATE" />
             </ItemTemplate>
                 <ItemStyle ForeColor="Blue" Width="100px" />
             </asp:TemplateField>
         </Columns>
         <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
         <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
         <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
         <RowStyle BackColor="White" ForeColor="Blue" />
         <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Blue" />
         <SortedAscendingCellStyle BackColor="#FEFCEB" />
         <SortedAscendingHeaderStyle BackColor="#AF0101" />
         <SortedDescendingCellStyle BackColor="#F6F0C0" />
         <SortedDescendingHeaderStyle BackColor="#7E0000" />
     </asp:GridView>
          </asp:Panel>
            </asp:View>
            </asp:MultiView> 
        </div> 
        </center>
</asp:Content>
