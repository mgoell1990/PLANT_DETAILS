<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="s_report.aspx.vb" Inherits="PLANT_DETAILS.s_report" %>

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
            <asp:Label ID="Label1" runat="server" Text="Store Material Report Section" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mt-2 mb-1" style="border: 3px; border-style: Groove; border-color: #FF3399">
        <div class="row mt-2 justify-content-center text-center">
            <div class="col-5 text-center">
                <asp:Panel ID="Panel9" runat="server">
                    <div class="row align-items-center">
                        <div class="col-5 text-end">
                            <asp:Label ID="Label49" runat="server" Font-Bold="True" ForeColor="Blue" Text="Search Type"></asp:Label>
                        </div>
                        <div class="col text-center">
                            <asp:DropDownList CssClass="form-select" ID="DropDownList9" runat="server" AutoPostBack="True">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Stock Position</asp:ListItem>
                                <asp:ListItem>Issue</asp:ListItem>
                                <asp:ListItem>Group-Wise Issue</asp:ListItem>
                                <asp:ListItem>Mat. Transaction</asp:ListItem>
                                <asp:ListItem>CRR</asp:ListItem>
                                <asp:ListItem>Non-Moving Items</asp:ListItem>
                                <asp:ListItem>Insured Items</asp:ListItem>
                                <asp:ListItem>Store Inventory Detail</asp:ListItem>
                                <asp:ListItem>View Materials Group Wise</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="row mt-2 justify-content-center align-items-center">
            <div class="col text-center">
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=============View 1 Stated==============--%>
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="STORE STOCK POSITION" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label454" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Group"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="REPORTDropDownList2" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label438" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="REPORTDropDownList3" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label439" runat="server" Font-Bold="True" ForeColor="Blue" Text="From Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="REPORTTextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="REPORTTextBox1_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="REPORTTextBox1"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label440" runat="server" Font-Bold="True" ForeColor="Blue" Text="To Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="REPORTTextBox2" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="REPORTTextBox2_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="REPORTTextBox2"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button29" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button30" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button31" runat="server" CssClass="btn btn-success" Text="Print" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel16" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="GridView10" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowHeaderWhenEmpty="True">
                                                <Columns>

                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" ItemStyle-CssClass="text-start" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                    <asp:BoundField DataField="MAT_STOCK" HeaderText="MAT STOCK" />
                                                    <asp:BoundField DataField="MAT_AVG" HeaderText="AVG. UNIT PRICE" />

                                                </Columns>
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                                <RowStyle BackColor="#f5f5f5" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=============View 2 Stated==============--%>
                    <asp:View ID="View2" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="STORE ISSUE" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Type"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="ddl_issue_type" runat="server" AutoPostBack="True">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>To Deptt</asp:ListItem>
                                                    <asp:ListItem>I.P.T. Issue</asp:ListItem>
                                                    <asp:ListItem>Individual</asp:ListItem>
                                                    <asp:ListItem>All Issue</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList5" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue No."></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="ddl_issue_no" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Text="From Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox1"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Text="To Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox2" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox2"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button2" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-success" Text="Print" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel10" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView1" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>

                                                    <asp:BoundField DataField="ROW_NO" HeaderText="Sl No" />
                                                    <asp:BoundField DataField="ISSUE_NO" HeaderText="Issue No" />
                                                    <asp:BoundField DataField="LINE_DATE" HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                                    <asp:BoundField DataField="LINE_NO" HeaderText="Line No" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                                                    <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                    <asp:BoundField DataField="ISSUE_QTY" HeaderText="Issue Qty" />
                                                    <asp:BoundField DataField="TOTAL_PRICE" HeaderText="Total Price" />
                                                    <asp:BoundField DataField="RQD_BY" HeaderText="REQ. By" />
                                                    <asp:BoundField DataField="RQD_DATE" HeaderText="REQ. Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="AUTH_BY" HeaderText="Auth. By" />
                                                    <asp:BoundField DataField="AUTH_DATE" HeaderText="Auth. Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="ISSUE_BY" HeaderText="Issue By" />
                                                    <asp:BoundField DataField="ISSUE_DATE" HeaderText="Issue Date" DataFormatString="{0:dd/MM/yyyy}" />

                                                </Columns>
                                                <AlternatingRowStyle BackColor="White" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                                <RowStyle BackColor="#f5f5f5" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:View>

                    <%--=============View 3 Stated==============--%>
                    <asp:View ID="View3" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Group Wise Store Issue" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Group"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="ddl_mat_group" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="ddl_mat_code" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Blue" Text="From Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox3" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox3"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="Blue" Text="To Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox4" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox4"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button5" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button6" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView2" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                    <asp:BoundField DataField="MAT_QTY" HeaderText="ISSUE QTY" />
                                                    <asp:BoundField DataField="Total_Value" HeaderText="TOTAL PRICE" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=============View 4 Stated==============--%>
                    <asp:View ID="View4" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Store Material Transaction" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Group"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList2" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label459" runat="server" Font-Bold="True" ForeColor="Blue" Text="Trans. Type"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList21" runat="server">
                                                    <asp:ListItem>All</asp:ListItem>
                                                    <asp:ListItem>Purchase</asp:ListItem>
                                                    <asp:ListItem>Issue</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="True" ForeColor="Blue" Text="From Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox5" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox5"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label16" runat="server" Font-Bold="True" ForeColor="Blue" Text="To Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox6" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox6"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button7" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button8" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button9" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView3" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="LINE_NO" HeaderText="LINE NO" />
                                                    <asp:BoundField DataField="LINE_DATE" HeaderText="LINE DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="LINE_TYPE" HeaderText="ISSUE/RECEIPT" />
                                                    <asp:BoundField DataField="ISSUE_NO" HeaderText="ISSUE/GARN NO" />
                                                    <asp:BoundField DataField="COST_CODE" HeaderText="COST CENTER" />
                                                    <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                    <asp:BoundField DataField="MAT_QTY" HeaderText="QTY" />
                                                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="UNIT PRICE" />
                                                    <asp:BoundField DataField="Total_PRICE" HeaderText="TOTAL PRICE" />
                                                    <asp:BoundField DataField="MAT_BALANCE" HeaderText="STOCK BALANCE" />
                                                    <asp:BoundField DataField="PURPOSE" HeaderText="PURPOSE" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=============View 5 CRR Stated==============--%>
                    <asp:View ID="View5" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="CRR" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Group"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList3" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList4" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label21" runat="server" Font-Bold="True" ForeColor="Blue" Text="From Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox7" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox7"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label22" runat="server" Font-Bold="True" ForeColor="Blue" Text="To Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox8" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox8"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button10" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button11" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button12" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView4" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="CRR_NO" HeaderText="CRR NO" />
                                                    <asp:BoundField DataField="CRR_DATE" HeaderText="CRR DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="SUPL_NAME" HeaderText="SUPL NAME" />
                                                    <asp:BoundField DataField="CHLN_NO" HeaderText="CHLN NO" />
                                                    <asp:BoundField DataField="CHLN_DATE" HeaderText="CHLN DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="TRUCK_NO" HeaderText="TRUCK NO" />
                                                    <asp:BoundField DataField="MAT_CHALAN_QTY" HeaderText="CHLN QTY" />
                                                    <asp:BoundField DataField="MAT_RCD_QTY" HeaderText="RCD QTY" />
                                                    <asp:BoundField DataField="BE_NO" HeaderText="BE NO" />
                                                    <asp:BoundField DataField="BE_DATE" HeaderText="BE DATE" />
                                                    <asp:BoundField DataField="BL_NO" HeaderText="BL NO" />
                                                    <asp:BoundField DataField="BL_DATE" HeaderText="BL DATE" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>


                    <%--=============View 6 Non-Moving Items Stated==============--%>
                    <asp:View ID="View6" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Store Non-Moving Items" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label56" runat="server" ForeColor="Blue" Text="Duration"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList10" runat="server">
                                                    <asp:ListItem>1</asp:ListItem>
                                                    <asp:ListItem>2</asp:ListItem>
                                                    <asp:ListItem>3</asp:ListItem>
                                                    <asp:ListItem Selected="True">5</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label37" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox19" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender16" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox19" />
                                            </div>
                                        </div>


                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button13" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button14" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button15" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>



                                    </div>

                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView5" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="MAT_LASTPUR_DATE" HeaderText="LAST PURCHASE DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="LAST_ISSUE_DATE" HeaderText="LAST ISSUE DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="ISSUE_FY" HeaderText="FISCAL YEAR" />
                                                    <asp:BoundField DataField="MAT_STOCK" HeaderText="STOCK" />
                                                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="UNIT PRICE" />
                                                    <asp:BoundField DataField="Value" HeaderText="VALUE" />
                                                    <asp:BoundField DataField="PURPOSE" HeaderText="PURPOSE" />
                                                    <asp:BoundField DataField="MATERIAL_TYPE" HeaderText="MATERIAL TYPE" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=============View 7 Stated==============--%>
                    <asp:View ID="View7" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Store Stock Position Group-Wise" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Group"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList6" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList7" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label27" runat="server" Font-Bold="True" ForeColor="Blue" Text="From Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox9" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="REPORTTextBox1"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="Blue" Text="To Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox10" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender10" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="REPORTTextBox2"></cc1:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button16" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button17" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button18" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView6" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                    <asp:BoundField DataField="MAT_STOCK" HeaderText="MAT STOCK" />
                                                    <asp:BoundField DataField="MAT_AVG" HeaderText="AVG. UNIT PRICE" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>


                    <%--=============View 8 Store Inventory Details Stated==============--%>
                    <asp:View ID="View8" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Store Inventory Detail" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="As on Date"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox11" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender11" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox11" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button19" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button20" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button21" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel8" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView7" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="LINE_NO" HeaderText="LINE NO" />
                                                    <asp:BoundField DataField="LINE_DATE" HeaderText="LINE DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="FISCAL_YEAR" HeaderText="FISCAL YEAR" />
                                                    <asp:BoundField DataField="MAT_BALANCE" HeaderText="STOCK" />
                                                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="UNIT PRICE" />
                                                    <asp:BoundField DataField="TOTAL_VALUE" HeaderText="VALUE" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=============View 9 Store Insured Items Details Stated==============--%>
                    <asp:View ID="View9" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col-5 text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Store Insured Items" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Button ID="Button22" runat="server" Text="Download" CssClass="btn btn-primary" />
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView8" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ROW_NO" HeaderText="SL NO" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT NAME" />
                                                    <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                    <asp:BoundField DataField="MAT_LASTPUR_DATE" HeaderText="LAST PURCHASE DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="LAST_ISSUE_DATE" HeaderText="LAST ISSUE DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="MAT_STOCK" HeaderText="STOCK" />
                                                    <asp:BoundField DataField="MAT_AVG" HeaderText="UNIT PRICE" />
                                                    <asp:BoundField DataField="Value" HeaderText="VALUE" />
                                                    <asp:BoundField DataField="PURPOSE" HeaderText="PURPOSE" />
                                                    <asp:BoundField DataField="MATERIAL_TYPE" HeaderText="MATERIAL TYPE" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
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
