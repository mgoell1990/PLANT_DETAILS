<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback ="true" CodeBehind="GSTR_1.aspx.vb" Inherits="PLANT_DETAILS.GSTR_1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
        <asp:Panel ID="Panel3" runat="server" BorderColor="Lime" BorderStyle="Double" style="text-align: left" Width="99%" CssClass="brds" Font-Names="Times New Roman">
               <asp:Label ID="Label37" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :30px;" Text="GSTR-1" Width="100%" Height="30px" CssClass="brds"></asp:Label>
       <center>
        <table border="0">
            <tr>
                <td>
                    <h3><strong><span >From:</span></strong>&ensp;&ensp;&ensp;
                        <asp:TextBox ID="TextBox1" runat="server" Width="180px" AutoCompleteType="Disabled"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
                    </h3>
                </td>
                <td>
                    <h3><strong><span>To:</span></strong>&ensp;&ensp;&ensp;
                        <asp:TextBox ID="TextBox2" runat="server" Width="180px" AutoCompleteType="Disabled"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox2" />
                    </h3>
                </td>
                <td>
                    <%--<asp:Button ID="Button1" runat="server" BackColor="#99FF66" BorderColor="Black" BorderStyle="Double" BorderWidth="1px" Font-Bold="True" Font-Size="X-Large" ForeColor="White" Height="50px" Text="Show" Width="180px" />--%>
                    <asp:Button ID="Button1" runat="server" Font-Bold="True" Text="B2B" Width="85px" CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                    <asp:Button ID="Button17" runat="server" CssClass="bottomstyle" Font-Bold="True" OnClientClick="this.disabled='true'; this.value='Please Wait...'" Text="B2C" UseSubmitBehavior="false" Width="85px" />
                    <asp:Button ID="Button16" runat="server" Text="Excel B2B" CssClass="bottomstyle" Width="85px" />
                    <asp:Button ID="Button18" runat="server" Text="Excel B2C" CssClass="bottomstyle" Width="85px" />
                </td>
            </tr>
        </table>
           </center>
<br />        

        <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Width="100%">
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="200%" RowStyle-HorizontalAlign="Center">
                  <AlternatingRowStyle BackColor="#DCDCDC" />
                  <Columns>
                      <asp:BoundField DataField="supplier_gst" HeaderText="Supplier GST" />
                      <asp:BoundField DataField="flag" HeaderText="Flag" />
                      <asp:BoundField DataField="party_gst" HeaderText="Party GST" />
                      <asp:BoundField DataField="receiver_name" HeaderText="Receiver Name" />
                      <asp:BoundField DataField="e_commerce_gstin" HeaderText="E Commerce GSTIN" />
                      <asp:BoundField DataField="return_period" HeaderText="Return Period" />
                      <asp:BoundField DataField="control_object" HeaderText="Control Object" />
                      <asp:BoundField DataField="invoice_number" HeaderText="Invoice Number" />
                      <asp:BoundField DataField="prefix" HeaderText="Prefix" />
                      <asp:BoundField DataField="suffix" HeaderText="Suffix" />
                      <asp:BoundField DataField="number" HeaderText="Number" />
                      <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" />
                      <asp:BoundField DataField="invoice_amt" HeaderText="Invoice Amt." />
                      <asp:BoundField DataField="export_type" HeaderText="Export Type" />
                      <asp:BoundField DataField="place_of_spply" HeaderText="Place Of Supply" />
                      <asp:BoundField DataField="reverse_charge" HeaderText="Reverse Charge" />
                      <asp:BoundField DataField="extensible_header_1" HeaderText="Extensible Header 1" />
                      <asp:BoundField DataField="extensible_header_2" HeaderText="Extensible Header 2" />
                      <asp:BoundField DataField="extensible_header_3" HeaderText="Extensible Header 3" />
                      <asp:BoundField DataField="extensible_header_4" HeaderText="Extensible Header 4" />
                      <asp:BoundField DataField="extensible_header_5" HeaderText="Extensible Header 5" />
                      <asp:BoundField DataField="extensible_header_6" HeaderText="Extensible Header 6" />
                      <asp:BoundField DataField="item_number" HeaderText="Item Number" />
                      <asp:BoundField DataField="nil_type" HeaderText="Nill Type" />
                      <asp:BoundField DataField="hsn" HeaderText="HSN Code" />
                      <asp:BoundField DataField="CHPT_NAME" HeaderText="Description" />
                      <asp:BoundField DataField="goods_services" HeaderText="Goods/Services" />
                      <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Quantity" />
                      <asp:BoundField DataField="UQC" HeaderText="UQC" />
                      <asp:BoundField DataField="TAXABLE_VALUE" HeaderText="Taxable Base" />
                      <asp:BoundField DataField="IGST_RATE" HeaderText="IGST Rate" />
                      <asp:BoundField DataField="IGST_AMT" HeaderText="IGST Amt" />
                      <asp:BoundField DataField="CGST_RATE" HeaderText="CGST Rate" />
                      <asp:BoundField DataField="CGST_AMT" HeaderText="CGST Amt" />
                      <asp:BoundField DataField="SGST_RATE" HeaderText="SGST Rate" />
                      <asp:BoundField DataField="SGST_AMT" HeaderText="SGST Amt" />
                      <asp:BoundField DataField="CESS_RATE" HeaderText="CESS Rate" />
                      <asp:BoundField DataField="CESS_AMT" HeaderText="CESS Amt" />
                      
                  </Columns>
                  
              </asp:GridView>
        </asp:Panel>

            <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="200%" RowStyle-HorizontalAlign="Center">
                  <AlternatingRowStyle BackColor="#DCDCDC" />
                  <Columns>
                      <asp:BoundField DataField="supplier_gst" HeaderText="Supplier GST" />
                      <asp:BoundField DataField="flag" HeaderText="Flag" />
                      <asp:BoundField DataField="e_commerce_gstin" HeaderText="E Commerce GSTIN" />
                      <asp:BoundField DataField="receiver_name" HeaderText="Receiver Name" />
                      <asp:BoundField DataField="receiver_state" HeaderText="Receiver State" />
                      <asp:BoundField DataField="return_period" HeaderText="Return Period" />
                      <asp:BoundField DataField="control_object" HeaderText="Control Object" />
                      <asp:BoundField DataField="invoice_number" HeaderText="Invoice Number" />
                      <asp:BoundField DataField="prefix" HeaderText="Prefix" />
                      <asp:BoundField DataField="suffix" HeaderText="Suffix" />
                      <asp:BoundField DataField="number" HeaderText="Number" />
                      <asp:BoundField DataField="invoice_date" HeaderText="Invoice Date" />
                      <asp:BoundField DataField="invoice_amt" HeaderText="Invoice Amt." />
                      <asp:BoundField DataField="place_of_spply" HeaderText="Place Of Supply" />
                      <asp:BoundField DataField="extensible_header_1" HeaderText="Extensible Header 1" />
                      <asp:BoundField DataField="extensible_header_2" HeaderText="Extensible Header 2" />
                      <asp:BoundField DataField="extensible_header_3" HeaderText="Extensible Header 3" />
                      <asp:BoundField DataField="extensible_header_4" HeaderText="Extensible Header 4" />
                      <asp:BoundField DataField="extensible_header_5" HeaderText="Extensible Header 5" />
                      <asp:BoundField DataField="extensible_header_6" HeaderText="Extensible Header 6" />
                      <asp:BoundField DataField="item_number" HeaderText="Item Number" />
                      <asp:BoundField DataField="nil_type" HeaderText="Nill Type" />
                      <asp:BoundField DataField="hsn" HeaderText="HSN Code" />
                      <asp:BoundField DataField="CHPT_NAME" HeaderText="Description" />
                      <asp:BoundField DataField="goods_services" HeaderText="Goods/Services" />
                      <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Quantity" />
                      <asp:BoundField DataField="UQC" HeaderText="UQC" />
                      <asp:BoundField DataField="TAXABLE_VALUE" HeaderText="Taxable Base" />
                      <asp:BoundField DataField="IGST_RATE" HeaderText="IGST Rate" />
                      <asp:BoundField DataField="IGST_AMT" HeaderText="IGST Amt" />
                      <asp:BoundField DataField="CGST_RATE" HeaderText="CGST Rate" />
                      <asp:BoundField DataField="CGST_AMT" HeaderText="CGST Amt" />
                      <asp:BoundField DataField="SGST_RATE" HeaderText="SGST Rate" />
                      <asp:BoundField DataField="SGST_AMT" HeaderText="SGST Amt" />
                      <asp:BoundField DataField="CESS_RATE" HeaderText="CESS Rate" />
                      <asp:BoundField DataField="CESS_AMT" HeaderText="CESS Amt" />
                      
                  </Columns>
                  
              </asp:GridView>
        </asp:Panel>
            </asp:Panel>
           </center> 
</asp:Content>







