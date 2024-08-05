<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="w_report.aspx.vb" Inherits="PLANT_DETAILS.w_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            <asp:Label ID="Label1" runat="server" Text="Contract Report" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center align-items-center mb-1">
                    <div class="col-2 text-end">
                        <asp:Label ID="Label49" runat="server" Font-Bold="true" ForeColor="Blue" Text="Search For"></asp:Label>
                    </div>
                    <div class="col-3 text-start">
                        <asp:DropDownList CssClass="form-select" ID="DropDownList9" runat="server" AutoPostBack="True">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Pending For M.B.</asp:ListItem>
                            <asp:ListItem>Date Wise Work</asp:ListItem>
                            <asp:ListItem>M.B. Details</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 START=====--%>
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center">
                            <div class="col" style="background: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Pending for M.B."></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-end">
                                        <asp:Label ID="Label50" runat="server" ForeColor="Blue" Text="Date Between"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox CssClass="form-control" ID="TextBox33" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox33_CalendarExtender" runat="server" BehaviorID="TextBox33_CalendarExtender" TargetControlID="TextBox33" CssClass="red" Format="dd-MM-yyyy" />
                                    </div>
                                    <div class="col-1 text-end">
                                        <asp:Label ID="Label51" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox CssClass="form-control" ID="TextBox34" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox34_CalendarExtender" runat="server" BehaviorID="TextBox34_CalendarExtender" TargetControlID="TextBox34" CssClass="red" Format="dd-MM-yyyy" />
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:Button ID="Button61" runat="server" Text="Proceed" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button65" runat="server" Text="Print" CssClass="btn btn-success" />
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col g-1">
                                        <asp:GridView ID="GridView5" runat="server" CssClass="table table-bordered border-2 table-responsive text-center" CellPadding="2" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="PO_NO" HeaderText="Work Order No." />
                                                <asp:BoundField DataField="supl_name" HeaderText="Contractor Name" />
                                                <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                                                <asp:BoundField DataField="w_au" HeaderText="Acc. Unit" />
                                                <asp:BoundField DataField="unit_rate" HeaderText="Unit Rate" />
                                                <asp:BoundField DataField="work_qty" HeaderText="Worked Qty." />
                                                <asp:BoundField DataField="total_amt" HeaderText="Total Amt." />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>


                    <%--=====VIEW 2 START=====--%>
                    <asp:View ID="View2" runat="server">
                        <div class="row justify-content-center">
                            <div class="col" style="background: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Date wise work"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-end">
                                        <asp:Label ID="Label52" runat="server" ForeColor="Blue" Text="Date Between" Style="text-align: left"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox CssClass="form-control" ID="TextBox35" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox35_CalendarExtender" runat="server" BehaviorID="TextBox35_CalendarExtender" TargetControlID="TextBox35" CssClass="red" Format="dd-MM-yyyy" />
                                    </div>
                                    <div class="col-1 text-end">
                                        <asp:Label ID="Label53" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox CssClass="form-control" ID="TextBox36" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox36_CalendarExtender" runat="server" BehaviorID="TextBox36_CalendarExtender" TargetControlID="TextBox36" CssClass="red" Format="dd-MM-yyyy" />
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:Button ID="Button62" runat="server" Text="Proceed" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button66" runat="server" Text="Print" CssClass="btn btn-success" />
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col g-1">
                                        <asp:GridView ID="GridView6" runat="server" CssClass="table table-bordered border-2 table-responsive text-center" CellPadding="2" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="PO_NO" HeaderText="Work Order No." />
                                                <asp:BoundField DataField="supl_name" HeaderText="Contractor Name" />
                                                <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                                                <asp:BoundField DataField="w_au" HeaderText="Acc. Unit" />
                                                <asp:BoundField DataField="unit_rate" HeaderText="Unit Rate" />
                                                <asp:BoundField DataField="work_qty" HeaderText="Worked Qty." />
                                                <asp:BoundField DataField="total_amt" HeaderText="Total Amt." />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 3 START=====--%>
                    <asp:View ID="View3" runat="server">
                        <div class="row justify-content-center">
                            <div class="col" style="background: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="M.B. Details"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mb-2">
                                    <div class="col-2 text-end">
                                        <asp:Label ID="Label58" runat="server" ForeColor="Blue" Text="Financial Year"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:DropDownList CssClass="form-select" ID="DropDownList13" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 text-end">
                                        <asp:Label ID="Label54" runat="server" ForeColor="Blue" Style="text-align: left" Text="Order No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:DropDownList CssClass="form-select" ID="DropDownList11" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-1 text-end">
                                        <asp:Label ID="Label55" runat="server" ForeColor="Blue" Text="M.B. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:DropDownList CssClass="form-select" ID="DropDownList12" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Button ID="Button67" runat="server" Text="Print" CssClass="btn btn-success" />
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
