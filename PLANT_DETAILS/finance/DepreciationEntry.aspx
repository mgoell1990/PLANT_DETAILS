<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="DepreciationEntry.aspx.vb" Inherits="PLANT_DETAILS.DepreciationEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label94" runat="server" Text="Depreciation Entry" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:Panel ID="Panel30" runat="server">
                    <div class="row justify-content-center align-items-center">
                        <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                            <div class="row justify-content-center">
                                <div class="col text-center">

                                    <div class="row align-items-center mt-1">
                                        <div class="col-2 text-end ">
                                            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="Voucher No."></asp:Label>
                                        </div>
                                        <div class="col-2 text-end ">
                                            <asp:TextBox class="form-control" ID="TextBox3" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-end ">
                                            <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Fiscal Year"></asp:Label>

                                        </div>
                                        <div class="col-1 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>2425</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-1 text-end">
                                            <asp:Label ID="Label47" runat="server" ForeColor="Blue" Text="Quarter"></asp:Label>

                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:DropDownList class="form-select" ID="DropDownList5" runat="server" >
                                                <asp:ListItem>Select</asp:ListItem>
                                               
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Calculate" />
                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Save" />
                                            <asp:Button ID="Button50" runat="server" Text="Download" CssClass="btn btn-primary" />            
                                        </div>
                                    </div>

                                    <asp:MultiView ID="MultiView1" runat="server">
                                        <asp:View ID="View1" runat="server">

                                            <div class="row align-items-center mt-1">
                                                <div class="col g-0">
                                                    <asp:Panel ID="Panel9" runat="server" ScrollBars="Auto" Width="100%">
                                                        <asp:GridView ID="GridView14" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                            <Columns>
                                                                <asp:BoundField DataField="AssetCode" HeaderText="Asset Code" />
                                                                <asp:BoundField DataField="AccountCode" HeaderText="Account Code" />
                                                                <asp:BoundField DataField="AssetName" HeaderText="Asset Name" />
                                                                <asp:BoundField DataField="DateOfCommisioning" HeaderText="Commisioning Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="PhysicalQuantity" HeaderText="Quantity" />
                                                                <asp:BoundField DataField="PhysicalLocation" HeaderText="Physical Location" />
                                                                <asp:BoundField DataField="DepreciationPercentage" HeaderText="Depreciation Percentage" />
                                                                <asp:BoundField DataField="GrossBlock" HeaderText="Original Value" />
                                                                <asp:BoundField DataField="CummulativeDepriciation" HeaderText="Cummulative Depriciation" />
                                                                <asp:BoundField DataField="FiscalYear" HeaderText="Fiscal Year" />
                                                                <asp:BoundField DataField="Quarter1" HeaderText="Quarter1" />
                                                                <asp:BoundField DataField="CummDeprBeforeQ1" HeaderText="Cumm. Dep. before Q1" />
                                                                <asp:BoundField DataField="DeprValueQ1" HeaderText="Dep. for Q1" />

                                                                <asp:BoundField DataField="Quarter2" HeaderText="Quarter2" />
                                                                <asp:BoundField DataField="CummDeprBeforeQ2" HeaderText="Cumm. Dep. before Q2" />
                                                                <asp:BoundField DataField="DeprValueQ2" HeaderText="Dep. for Q2" />

                                                                <asp:BoundField DataField="Quarter3" HeaderText="Quarter3" />
                                                                <asp:BoundField DataField="CummDeprBeforeQ3" HeaderText="Cumm. Dep. before Q3" />
                                                                <asp:BoundField DataField="DeprValueQ3" HeaderText="Dep. for Q3" />

                                                                <asp:BoundField DataField="Quarter4" HeaderText="Quarter4" />
                                                                <asp:BoundField DataField="CummDeprBeforeQ4" HeaderText="Cumm. Dep. before Q4" />
                                                                <asp:BoundField DataField="DeprValueQ4" HeaderText="Dep. for Q4" />
                                                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />

                                                            </Columns>

                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </div>

                                            </div>

                                        </asp:View>
                                        
                                    </asp:MultiView>

                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>


</asp:Content>


