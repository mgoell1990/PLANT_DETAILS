<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="fg_report_quality.aspx.vb" Inherits="PLANT_DETAILS.fg_report_quality" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Finished Goods Report(Quality-Wise)" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col-6">
                <div class="row align-items-center">
                    <div class="col-5 text-end">
                        <asp:Label ID="Label1" runat="server" Text="From" Font-Bold="True" ForeColor="blue" />
                    </div>
                    <div class="col-4 text-start">
                        <asp:TextBox CssClass="form-control" ID="date1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox1_CalendarExtender" TargetControlID="date1" />
                    </div>
                </div>

                <div class="row align-items-center">
                    <div class="col-5 text-end">
                        <asp:Label ID="Label4" runat="server" Text="To" Font-Bold="True" ForeColor="blue" />
                    </div>
                    <div class="col-4 text-start">
                        <asp:TextBox CssClass="form-control" ID="date2" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox2_CalendarExtender" TargetControlID="date2" />
                    </div>

                </div>

                <div class="row align-items-center mt-1">
                    <div class="col-5 text-end">
                    </div>

                    <div class="col-4 text-start">
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"></asp:Button>
                        <asp:Button ID="Button2" runat="server" Text="Download" CssClass="btn btn-primary"></asp:Button>
                    </div>
                </div>

            </div>
        </div>

        <div class="row align-items-center mt-1">
            <div class="col text-center" style="overflow: scroll">

                <asp:GridView ID="GridView_fdata" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" CellPadding="2" ShowHeaderWhenEmpty="false">
                    <Columns>
                        <asp:BoundField DataField="ItemNo" HeaderText="Sl. No" />
                        <asp:BoundField DataField="qual_desc" HeaderText="Item Name" />
                        <asp:BoundField DataField="Open_Stock" HeaderText="Opening Stock" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="Open_Stock_Mt" HeaderText="Opening Stock(Mt)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PROD_QTY" HeaderText="Production Qty" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="PROD_QTY_Mt" HeaderText="Production(Mt)" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="SALES_QTY" HeaderText="Sales Qty" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="SALES_QTY_Mt" HeaderText="Sales Mt." ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="CLOSING_QTY" HeaderText="Closing Stock" ItemStyle-HorizontalAlign="Right" />
                        <asp:TemplateField HeaderText="Closing Stock(Mt)" ItemStyle-HorizontalAlign="Right"></asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
   
</asp:Content>
