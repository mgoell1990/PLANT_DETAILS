<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="order_balance.aspx.vb" Inherits="PLANT_DETAILS.order_balance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox115]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/r_material")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            OnSuccess
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0]
                                }
                            }

                            ))
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

        function OnSuccess() {
            alert("Function called");
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Order Balance" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mt-2 mb-1">
        <div class="row mt-2 justify-content-center text-center">
            <div class="col-5 text-center">
                <asp:Panel ID="Panel22" runat="server">
                    <div class="row align-items-center">
                        <div class="col-5 text-end">
                            <asp:Label ID="Label49" runat="server" ForeColor="Blue" Text="Search By"></asp:Label>
                        </div>
                        <div class="col text-center">
                            <asp:DropDownList CssClass="form-select" ID="DropDownList9" runat="server" AutoPostBack="True">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>PO Number</asp:ListItem>
                                <asp:ListItem>Date</asp:ListItem>
                                <asp:ListItem>By Material Code</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="row mt-2 justify-content-center align-items-center">
            <div class="col text-center">
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 SEARCH BY PO NUMBER START=====--%>
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col-5 text-center" style="border-color: Blue; border-style: Groove">

                                <div class="row align-items-center mt-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label446" runat="server" ForeColor="Blue" Text="Order Type"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList50" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row align-items-center mt-1 mb-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label445" runat="server" ForeColor="Blue" Text="Order No"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList10" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1 mb-1">
                                    <div class="col-5 text-end">
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button64" runat="server" Text="Search" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button68" runat="server" Text="Print" CssClass="btn btn-success" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center align-items-center mt-1">
                            <div class="col text-center g-0">

                                <asp:Panel ID="Panel14" runat="server" BackColor="#AAEEFF" Style="text-align: left" Visible="False" BorderColor="#AAEEFF" BorderStyle="Groove">

                                    <div class="row align-items-start mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label447" runat="server" Font-Bold="True" ForeColor="#000099">Party Name</asp:Label>
                                        </div>
                                        <div class="col text-start">
                                            <asp:Label ID="Label57" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row align-items-center mt-1 mb-1">
                                        <div class="col text-center">
                                            <asp:Panel ID="Panel26" runat="server" ScrollBars="Auto" Width="100%">
                                                <asp:GridView ID="GridView4" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                    <Columns>
                                                        <asp:BoundField DataField="PO_NO" HeaderText="Order No" />
                                                        <asp:BoundField DataField="W_SLNO" HeaderText="W.O. SLNo" />
                                                        <asp:BoundField DataField="W_NAME" HeaderText="Work Name" />
                                                        <asp:BoundField DataField="W_AU" HeaderText="Acc.Unit" />
                                                        <asp:BoundField DataField="W_START_DATE" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="W_END_DATE" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="W_QTY" HeaderText="Order Qty." />
                                                        <asp:BoundField DataField="W_COMPLETED" HeaderText="Completed" />
                                                        <asp:TemplateField HeaderText="Bal. Qty."></asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>

                                    </div>

                                </asp:Panel>



                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 2 SEARCH BY DATE START=====--%>
                    <asp:View ID="View2" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col-5 text-center" style="border-color: Blue; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">


                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="From"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
                                            </div>
                                        </div>
                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label6" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox2" />

                                            </div>

                                        </div>
                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="Order Type"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList4" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Purchase Order</asp:ListItem>
                                                    <%--<asp:ListItem>Work Order</asp:ListItem>
                                                    <asp:ListItem>Rate Contract</asp:ListItem>
                                                    <asp:ListItem>Sale Order</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="row align-items-center mt-1 mb-1">
                                            <div class="col-5 text-end">
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Print" />
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col g-0">
                                <asp:Panel ID="Panel24" runat="server" ScrollBars="Auto" Width="100%">
                                    <asp:GridView ID="GridView1" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField DataField="ACTUAL_PO_NO" HeaderText="Actual Order No" />
                                            <asp:BoundField DataField="PO_NO" HeaderText="System Order No" />
                                            <asp:BoundField DataField="SUPL_NAME" HeaderText="Party Name" />
                                            <asp:BoundField DataField="W_SLNO" HeaderText="Order Sl. No." />
                                            <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                                            <asp:BoundField DataField="W_AU" HeaderText="Acc.Unit" />
                                            <asp:BoundField DataField="W_QTY" HeaderText="Order Qty." />
                                            <asp:BoundField DataField="W_COMPLETED" HeaderText="Completed Qty." />
                                            <asp:BoundField DataField="ORDER_BALANCE" HeaderText="Balance Qty." />
                                        </Columns>

                                    </asp:GridView>
                                </asp:Panel>
                            </div>

                        </div>

                    </asp:View>

                    <%--=====VIEW 3 SEARCH BY MATERIAL CODE START=====--%>
                    <asp:View ID="View3" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col-6 text-center" style="border-color: Blue; border-style: Groove">
                                <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                    <div class="col text-center">
                                        <asp:Label ID="Label625" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="White" Style="text-align: center;" Text="RM Order balance by material code"></asp:Label>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label10" runat="server" ForeColor="Blue" Text="Material Code"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox115" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-5 text-end">
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button5" runat="server" Text="Search PO" CssClass="btn btn-primary" />
                                    </div>

                                </div>

                                <div class="row align-items-center mt-1 mb-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label11" runat="server" ForeColor="Blue" Text="Select PO"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1 mb-1">
                                    <div class="col-5 text-end">
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button3" runat="server" Text="Search" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row justify-content-center align-items-center mt-1">
                            <div class="col text-center g-0">

                                <asp:Panel ID="Panel6" runat="server" BackColor="#AAEEFF" Style="text-align: left" Visible="False" BorderColor="#AAEEFF" BorderStyle="Groove">

                                    <div class="row align-items-start mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label15" runat="server" Font-Bold="True" ForeColor="#000099">Party Name</asp:Label>
                                        </div>
                                        <div class="col text-start">
                                            <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="#000099"></asp:Label>
                                        </div>
                                    </div>


                                    <div class="row align-items-center mt-1 mb-1">
                                        <div class="col text-center">
                                            <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Width="100%">
                                                <asp:GridView ID="GridView2" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                    <Columns>
                                                        <asp:BoundField DataField="PO_NO" HeaderText="Order No" />
                                                        <asp:BoundField DataField="W_SLNO" HeaderText="W.O. SLNo" />
                                                        <asp:BoundField DataField="W_NAME" HeaderText="Work Name" />
                                                        <asp:BoundField DataField="W_AU" HeaderText="Acc.Unit" />
                                                        <asp:BoundField DataField="W_START_DATE" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="W_END_DATE" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="W_QTY" HeaderText="Order Qty." />
                                                        <asp:BoundField DataField="W_COMPLETED" HeaderText="Completed" />
                                                        <asp:TemplateField HeaderText="Bal. Qty."></asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>

                                    </div>

                                </asp:Panel>



                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>





















</asp:Content>
