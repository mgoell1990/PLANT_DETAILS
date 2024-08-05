<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="ir.aspx.vb" Inherits="PLANT_DETAILS.ir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="I.R. Clearance" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 GARN START=====--%>
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col-11 justify-content-center" style="border-style: Groove; border-color: Red">

                                <div class="row align-items-center mt-2 mb-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label38" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No"></asp:Label>
                                    </div>
                                    <div class="col-3 text-start">
                                        <asp:DropDownList CssClass="form-select" ID="DropDownList6" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-1 g-0 text-start">
                                        <asp:Label ID="Label40" runat="server" Font-Bold="True" ForeColor="Blue" Text="M.B. No"></asp:Label>
                                    </div>
                                    <div class="col-3 text-start">
                                        <asp:DropDownList CssClass="form-select" ID="DropDownList8" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-3 text-start">
                                        <asp:Button ID="Button57" runat="server" class="btn btn-primary fw-bold" Text="Proceed" />
                                        <asp:Button ID="Button58" runat="server" class="btn btn-primary fw-bold" Text="Close" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>

                                <asp:Panel ID="Panel6" runat="server" Style="text-align: left" Visible="False">
                                    <div class="row justify-content-center" style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                                        <div class="col-5 text-start">

                                            <div class="row align-items-center mt-1">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label46" runat="server" Font-Bold="True" ForeColor="Blue" Text="Enter Password"></asp:Label>
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox CssClass="form-control" ID="TextBox30" runat="server" TextMode="Password"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row align-items-center mt-1">
                                                <div class="col-4 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:Button ID="Button56" runat="server" Font-Bold="True" ForeColor="White" Text="Submit" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                </div>
                                            </div>
                                            <div class="row align-items-center mb-1">
                                                <div class="col-4 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:Label ID="Label43" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="row align-items-center">
                                    <div class="col g-1">
                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered border-2 table-responsive text-center" CellPadding="2" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="ra_no" HeaderText="R.A. No" />
                                                <asp:BoundField DataField="wo_slno" HeaderText="W.O. SLNo" />
                                                <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                                                <asp:BoundField DataField="w_au" HeaderText="Acc.Unit" />
                                                <asp:BoundField DataField="from_date" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="to_date" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="work_qty" HeaderText="Work Qty" />
                                                <asp:BoundField DataField="rqd_qty" HeaderText="Rqd. Qty." />
                                                <asp:BoundField DataField="bal_qty" HeaderText="Bal. Qty" />
                                                <asp:BoundField DataField="prov_amt" HeaderText="Base Value" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>


</asp:Content>
