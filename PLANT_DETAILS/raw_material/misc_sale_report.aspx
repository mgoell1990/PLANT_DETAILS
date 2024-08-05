<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="misc_sale_report.aspx.vb" Inherits="PLANT_DETAILS.misc_sale_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Raw Material Misc. Sale Report" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center">
                    <div class="col-5">

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label5" runat="server" Text="Item Code"></asp:Label>

                            </div>
                            <div class="col-6 text-start">
                                <asp:DropDownList CssClass="form-select" ID="DropDownList2" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="date2_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox2_CalendarExtender" TargetControlID="TextBox1" />
                            </div>
                        </div>


                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox2_CalendarExtender" TargetControlID="TextBox2" />
                            </div>
                            
                        </div>

                        <div class="row justify-content-center align-items-center mt-2">
                            <div class="col-3 text-start">
                                
                            </div>
                            <div class="col-6 text-start">
                                <asp:Button ID="Button1" runat="server" Text="View" CssClass="btn btn-primary fw-bold"></asp:Button>
                                <asp:Button ID="Button2" runat="server" Text="Download in Excel" CssClass="btn btn-primary fw-bold" />
                            </div>
                        </div>

                    </div>
                </div>
                <div class="row justify-content-center m-2">
                    <div class="col justify-content-center" style="overflow: scroll">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="FALSE" Width="300%">
                            <Columns>
                                <asp:BoundField DataField="INV_NO" HeaderText="INV NO" />
                                <asp:BoundField DataField="INV_DATE" HeaderText="INV DATE" />
                                <asp:BoundField DataField="SO_ACTUAL" HeaderText="ACTUAL SO" />
                                <asp:BoundField DataField="SO_ACTUAL_DATE" HeaderText="SO DATE" />
                                <asp:BoundField DataField="MAT_SLNO" HeaderText="SL NO" />
                                <asp:BoundField DataField="PARTY_CODE" HeaderText="PARTY CODE" />
                                <asp:BoundField DataField="P_CODE" HeaderText="ITEM CODE" />
                                <asp:BoundField DataField="P_DESC" HeaderText="ITEM NAME" />
                                <asp:BoundField DataField="TOTAL_PCS" HeaderText="QTY PCS" />
                                <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="TOTAL WEIGHT" />
                                <asp:BoundField DataField="BASE_PRICE" HeaderText="BASE PRICE" />
                                <asp:BoundField DataField="TRANS_NAME" HeaderText="TRANS NAME" />
                                <asp:BoundField DataField="TRUCK_NO" HeaderText="TRUCK NO" />
                                <asp:TemplateField HeaderText="BASE VALUE"></asp:TemplateField>
                                <asp:BoundField DataField="CAS4" HeaderText="CAS4" />
                                <asp:BoundField DataField="PACKING" HeaderText="PACKING" />
                                <asp:BoundField DataField="TERM_AMT" HeaderText="T TAX" />
                                <asp:BoundField DataField="TCS_AMT" HeaderText="TCS" />
                                <asp:BoundField DataField="FREIGHT" HeaderText="FREIGHT" />
                                <asp:BoundField DataField="SGST_AMT" HeaderText="SGST" />
                                <asp:BoundField DataField="CGST_AMT" HeaderText="CGST" />
                                <asp:BoundField DataField="IGST_AMT" HeaderText="IGST" />
                                <asp:BoundField DataField="TOTAL_VALUE" HeaderText="TOTAL VALUE" />
                                <asp:BoundField DataField="FISCAL_YEAR" HeaderText="F. Year" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

