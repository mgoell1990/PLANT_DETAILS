<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="rcd_voucher.aspx.vb" Inherits="PLANT_DETAILS.rcd_voucher" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <script type="text/javascript">
        $(function () {
            $("[id$=DropDownList22]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/supl_and_dater")%>',
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

    <script type="text/javascript">
        $(function () {
            $("[id$=DropDownList24]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head")%>',
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

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label94" runat="server" Text="Receipt Voucher" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:Panel ID="Panel30" runat="server">
                    <div class="row justify-content-center">
                        <div class="col-12 justify-content-center m-1" style="border-color: Blue; border-style: groove">

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label451" runat="server" ForeColor="Blue" Text="Token No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox59" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label543" runat="server" ForeColor="Blue" Text="C.B.V. No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox93" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>

                                <div class="col text-start">
                                    <asp:Label ID="Label455" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label449" runat="server" ForeColor="Blue" Text="Section Sl No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox56" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label450" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox57" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox57_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox57"></asp:CalendarExtender>
                                </div>

                            </div>





                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label444" runat="server" ForeColor="Blue" Text="Supl Code"></asp:Label>
                                </div>
                                <div class="col-5 text-start ">
                                    <asp:TextBox class="form-control" ID="DropDownList22" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                </div>
                                <div class="col-2 text-start">
                                </div>

                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label507" runat="server" ForeColor="Blue" Text="Inst Type"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:DropDownList class="form-select" ID="DropDownList23" runat="server">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>D.D.</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Pay Order</asp:ListItem>
                                        <asp:ListItem>E Payment</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label446" runat="server" ForeColor="Blue" Text="Order Type"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:DropDownList class="form-select" ID="DropDownList50" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="Order No."></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:DropDownList class="form-select" ID="DropDownList49" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>



                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label508" runat="server" ForeColor="Blue" Text="Inst No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox82" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label509" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox83" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox83_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox83"></asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="IOC No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox3" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox3"></asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label510" runat="server" ForeColor="Blue" Text="Drawn On"></asp:Label>
                                </div>
                                <div class="col-5 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox84" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label511" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration"></asp:Label>
                                </div>
                                <div class="col-5 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox58" runat="server"></asp:TextBox>
                                </div>
                            </div>



                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label452" runat="server" ForeColor="Blue" Text="A/C Head"></asp:Label>
                                </div>
                                <div class="col-5 text-start ">
                                    <asp:TextBox class="form-control" ID="DropDownList24" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label453" runat="server" Text="Amount" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-1 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox60" runat="server"></asp:TextBox>
                                </div>

                                <div class="col text-end">
                                    <asp:Button ID="Button20" runat="server" Text="Add" CssClass="btn btn-primary" />
                                    <asp:Button ID="Button21" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                    <asp:Button ID="Button48" runat="server" Text="Cancel" CssClass="btn btn-danger" />

                                </div>
                            </div>

                            <div class="row align-items-center mt-2">
                                <div class="col" style="overflow: scroll">
                                    <asp:GridView ID="GridView211" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField HeaderText="Supl. ID" DataField="supl_id"></asp:BoundField>
                                            <asp:BoundField HeaderText="Inst. Type" DataField="inst_type"></asp:BoundField>
                                            <asp:BoundField HeaderText="Inst. No" DataField="inst_no">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Inst. Date" DataField="inst_date"></asp:BoundField>
                                            <asp:BoundField HeaderText="A/C Head" DataField="ac_code"></asp:BoundField>
                                            <asp:BoundField HeaderText="Amount" DataField="amount">
                                                <HeaderStyle />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Drawn On" DataField="drawn_on" />
                                            <asp:BoundField HeaderText="Narration" DataField="nar" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>




                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>

