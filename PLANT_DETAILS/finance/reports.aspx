<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="reports.aspx.vb" Inherits="PLANT_DETAILS.report3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <%--<link href="../Content/red.css" rel="stylesheet" type="text/css" />--%>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox37]").autocomplete({
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
            $("[id$=TextBox35]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/sale_voucher_no")%>',
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
            $("[id$=TextBox18]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/voucher_no")%>',
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
            $("[id$=TextBox7]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/rcm_inv_no")%>',
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
            $("[id$=TextBox1]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/voucher_no")%>',
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
            $("[id$=TextBox1]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/voucher_no")%>',
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
            <asp:Label ID="Label1" runat="server" Text="Finance Reports" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mt-2 mb-1">
        <div class="row mt-2 justify-content-center text-center">
            <div class="col-6 text-center">
                <asp:Panel ID="Panel22" runat="server">
                    <div class="row align-items-center">
                        <div class="col-4 text-end">
                            <asp:Label ID="Label49" runat="server" ForeColor="Blue" Text="Search For"></asp:Label>
                        </div>
                        <div class="col text-center">
                            <asp:DropDownList class="form-select" ID="DropDownList9" runat="server" AutoPostBack="True">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Adv. Voucher</asp:ListItem>
                                <asp:ListItem>Bank Book</asp:ListItem>
                                <asp:ListItem>Bill Forwarding Memo</asp:ListItem>
                                <asp:ListItem>Bank Gaurantee</asp:ListItem>
                                <asp:ListItem>Bill Track</asp:ListItem>
                                <asp:ListItem>General Ledger</asp:ListItem>
                                <asp:ListItem>ITC</asp:ListItem>
                                <asp:ListItem>JE Entry</asp:ListItem>
                                <asp:ListItem>Link Sheet</asp:ListItem>
                                <asp:ListItem>Party Ledger</asp:ListItem>
                                <asp:ListItem>Pending RCM</asp:ListItem>
                                <asp:ListItem>RCM Tax Invoice</asp:ListItem>
                                <asp:ListItem>RIOC</asp:ListItem>
                                <asp:ListItem>Shedule</asp:ListItem>
                                <asp:ListItem>Trial Report</asp:ListItem>
                                <asp:ListItem>Trial Report(Merged A/c Code)</asp:ListItem>
                                <asp:ListItem>Voucher</asp:ListItem>
                                <asp:ListItem>Ledger Entry</asp:ListItem>
                                <asp:ListItem>AGING</asp:ListItem>
                                <asp:ListItem>Pending GARN</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="row mt-2 justify-content-center align-items-center">
            <div class="col text-center">
                <asp:MultiView ID="MultiView2" runat="server">

                    <%--=============View 3 Advance Voucher Stated==============--%>
                    <asp:View ID="View3" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="ADVANCE VOUCHER"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1 mb-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label53" runat="server" Text="Voucher No" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox35" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button66" runat="server" CssClass="btn btn-primary" Text="View" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=============View 4 Bank Book Stated==============--%>
                    <asp:View ID="View4" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label67" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Bank Book" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-3 text-end">
                                                <asp:Label ID="Label6" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox4" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox4" />
                                            </div>

                                            <div class="col-1 text-end">
                                                <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox5" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox5" />

                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" Text="Go" />
                                                <asp:Button ID="Button16" runat="server" Text="Excel" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button5" runat="server" CssClass="btn btn-success" Text="Print" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel24" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView2" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No." />
                                                    <asp:BoundField DataField="CVB_NO" HeaderText="C.B.V. No." />
                                                    <asp:BoundField DataField="CVB_DATE" HeaderText="C.B.V. Date" />
                                                    <asp:BoundField DataField="CHEQUE_NO" HeaderText="Check No." />
                                                    <asp:BoundField DataField="SUPL_ID" HeaderText="Party Code" />
                                                    <asp:BoundField DataField="SUPL_NAME" HeaderText="Party Name" />
                                                    <asp:BoundField DataField="AC_NO" HeaderText="A/C No." />
                                                    <asp:BoundField DataField="ac_description" HeaderText="A/C Name" />
                                                    <asp:TemplateField HeaderText="Debit Amount"></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit Amount"></asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:View>

                    <%--=============View 5 Bill Forwarding Memo Stated==============--%>
                    <asp:View ID="View5" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label73" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Bill Forwarding Memo" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center m-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label35" runat="server" Text="Voucher No" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox18" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button28" runat="server" CssClass="btn btn-primary" Text="View" />
                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 6 BANK GAURANTEE START=====--%>
                    <asp:View ID="View6" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label78" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Bank Guarantee" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label56" runat="server" ForeColor="Blue" Text="Report Type"></asp:Label>

                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList10" runat="server" AutoPostBack="True">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>BG Report</asp:ListItem>
                                                    <asp:ListItem>BG Expiry Report</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="View1" runat="server">
                                                <div class="row align-items-center">
                                                    <div class="col-5 text-end">
                                                        <asp:Label ID="Label39" runat="server" Text="Party Code" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox37" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-5 text-end">
                                                        <asp:Label ID="Label37" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox19" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender16" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox19" />
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-5 text-end">
                                                        <asp:Label ID="Label38" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox20" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender17" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox20" />
                                                    </div>
                                                    <div class="col text-start">
                                                        <asp:Button ID="Button29" runat="server" CssClass="btn btn-primary" Text="Go" />
                                                        <asp:Button ID="Button30" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                        <asp:Button ID="Button31" runat="server" CssClass="btn btn-success" Text="Print" />
                                                    </div>
                                                </div>

                                            </asp:View>
                                            <asp:View ID="View2" runat="server">

                                                <div class="row align-items-center">
                                                    <div class="col-5 text-end">
                                                        <asp:Label ID="Label57" runat="server" Text="Expiring in next" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList11" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                            <asp:ListItem>5</asp:ListItem>
                                                            <asp:ListItem>6</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-1 text-start g-0">
                                                        <asp:Label ID="Label41" runat="server" Text="Month" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col text-start">
                                                        <asp:Button ID="Button32" runat="server" CssClass="btn btn-primary" Text="Go" />
                                                        <asp:Button ID="Button33" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                        <asp:Button ID="Button34" runat="server" CssClass="btn btn-success" Text="Print" />
                                                    </div>
                                                </div>

                                            </asp:View>
                                        </asp:MultiView>

                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel26" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView10" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="BG_NO" HeaderText="BG NO" />
                                                    <asp:BoundField DataField="BG_DATE" HeaderText="BG DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="ORIGINAL_BG_NO" HeaderText="ORIGINAL BG NO" />
                                                    <asp:BoundField DataField="ORIGINAL_BG_DATE" HeaderText="ORIGINAL BG DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="PARTY_CODE" HeaderText="PARTY Code" />
                                                    <asp:BoundField DataField="PARTY_NAME" HeaderText="PARTY Name" />
                                                    <asp:BoundField DataField="ORDER_NO" HeaderText="ORDER NO" />
                                                    <asp:BoundField DataField="ACTUAL_ORDER_NO" HeaderText="ACTUAL ORDER NO" />
                                                    <asp:BoundField DataField="ORDER_DATE" HeaderText="ORDER DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="IOC_NO" HeaderText="IOC NO" />
                                                    <asp:BoundField DataField="IOC_DATE" HeaderText="IOC DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="RETURN_BG_IOC_NO" HeaderText="RETURN BG IOC NO" />
                                                    <asp:BoundField DataField="RETURN_BG_IOC_DATE" HeaderText="RETURN BG IOC DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="DEPOSIT_TYPE" HeaderText="DEPOSIT TYPE" />
                                                    <asp:BoundField DataField="BG_TYPE" HeaderText="BG TYPE" />
                                                    <asp:BoundField DataField="BG_LOCATION" HeaderText="BG LOCATION" />
                                                    <asp:BoundField DataField="ISSUING_BANK_NAME" HeaderText="ISSUING BANK NAME" />
                                                    <asp:BoundField DataField="ISSUING_BANK_BRANCH" HeaderText="ISSUING BANK BRANCH" />
                                                    <asp:BoundField DataField="BG_AMOUNT" HeaderText="BG AMOUNT" />
                                                    <asp:BoundField DataField="BG_VALIDITY" HeaderText="BG VALIDITY" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="CONF_LETTER_NO" HeaderText="CONFIRMATION LETTER NO" />
                                                    <asp:BoundField DataField="CONF_LETTER_DATE" HeaderText="CONFIRMATION LETTER DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="COMPANY_CONF_LETTER_NO" HeaderText="COMPANY CONFIRMATION LETTER NO" />
                                                    <asp:BoundField DataField="COMPANY_CONF_LETTER_DATE" HeaderText="COMPANY CONFIRMATION LETTER DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="FISCAL_YEAR" HeaderText="FISCAL YEAR" />
                                                    <asp:BoundField DataField="EMP_NAME" HeaderText="EMPLOYEE NAME" />
                                                    <asp:BoundField DataField="ENTRY_DATE" HeaderText="ENTRY DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="BG_STATUS" HeaderText="BG STATUS" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 7 BILL TRACK START=====--%>
                    <asp:View ID="View7" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Bill Track" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-3 text-end">
                                                <asp:Label ID="Label50" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox33" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBox33_CalendarExtender" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox33" />
                                            </div>

                                            <div class="col-1 text-end">
                                                <asp:Label ID="Label51" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox34" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBox34_CalendarExtender" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox34" />

                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button61" runat="server" CssClass="btn btn-primary" Text="Go" />
                                                <asp:Button ID="Button21" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button65" runat="server" CssClass="btn btn-success" Text="Print" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView5" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="bill_id" HeaderText="Reg. No" />
                                                    <asp:BoundField DataField="post_date" HeaderText="Reg. Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="po_no" HeaderText="Order No" />
                                                    <asp:BoundField DataField="PO_TYPE" HeaderText="Order Type" />
                                                    <asp:BoundField DataField="SUPL_NAME" HeaderText="Party Name" />
                                                    <asp:BoundField DataField="inv_no" HeaderText="Inv. No" />
                                                    <asp:BoundField DataField="inv_date" HeaderText="Inv. Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:TemplateField HeaderText="GARN/MB Date"></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date"></asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque No."></asp:TemplateField>
                                                    <asp:BoundField DataField="inv_amount" HeaderText="Invoice Amount" />

                                                </Columns>

                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:View>


                    <%--=====VIEW 8 GENERAL LEDGER START=====--%>
                    <asp:View ID="View8" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="General Ledger" Font-Underline="True"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label11" runat="server" Text="A/c No" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label12" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox8" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox8" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label13" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox9" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox9" />
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button9" runat="server" CssClass="btn btn-primary" Text="Go" />
                                        <asp:Button ID="Button13" runat="server" Text="Download" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button10" runat="server" CssClass="btn btn-success" Text="Print" />
                                        
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel28" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView4" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EnableRowVirtualization="True">
                                                <Columns>
                                                    <asp:BoundField DataField="AC_NO" HeaderText="A/c No" />
                                                    <asp:BoundField DataField="ac_description" HeaderText="A/c Name" />
                                                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" />
                                                    <asp:BoundField DataField="GARN" HeaderText="GARN NO" />
                                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No" />
                                                    <asp:BoundField DataField="EFECTIVE_DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="DR" HeaderText="DR" />
                                                    <asp:BoundField DataField="CR" HeaderText="CR" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 9 ITC START=====--%>
                    <asp:View ID="View9" runat="server">
                    </asp:View>

                    <%--=====VIEW 10 JE ENTRY START=====--%>
                    <asp:View ID="View10" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label91" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="External JE Entry" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label31" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox16" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender14" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox16" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label32" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox17" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender15" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox17" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button25" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button26" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button27" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel29" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView9" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="Journal_ID" HeaderText="Journal ID" />
                                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No" />
                                                    <asp:BoundField DataField="SUPL_ID" HeaderText="Supl ID" />
                                                    <asp:BoundField DataField="EFECTIVE_DATE" HeaderText="Effective Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="AC_NO" HeaderText="AC Code" />
                                                    <asp:BoundField DataField="AMOUNT_DR" HeaderText="Debit" />
                                                    <asp:BoundField DataField="AMOUNT_CR" HeaderText="Credit" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 11 LINK SHEET START=====--%>
                    <asp:View ID="View11" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Link Sheet" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label43" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox22" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender19" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox22" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center mb-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label44" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox23" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender20" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox23" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button37" runat="server" CssClass="btn btn-primary" Text="Print" />
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 12 PARTY LEDGER START=====--%>
                    <asp:View ID="View12" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Party Ledger" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label5" runat="server" Text="A/c No" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList1" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label16" runat="server" Text="Party Code" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList3" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label14" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox10" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender10" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox10" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label15" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox11" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender11" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox11" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button11" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button20" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button12" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel6" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView6" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="AC_NO" HeaderText="A/c No" />
                                                    <asp:BoundField DataField="ac_description" HeaderText="A/c Name" />
                                                    <asp:BoundField DataField="PO_NO" HeaderText="PO NO" />
                                                    <asp:BoundField DataField="GARN" HeaderText="GARN NO" />
                                                    <asp:BoundField DataField="VOUCHER_NO" HeaderText="Voucher No" />
                                                    <asp:BoundField DataField="INVOICE_NO" HeaderText="Invoice No" />
                                                    <asp:BoundField DataField="SUPL_ID" HeaderText="Supplier" />
                                                    <asp:BoundField DataField="EFECTIVE_DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="DR" HeaderText="DR" />
                                                    <asp:BoundField DataField="CR" HeaderText="CR" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 13 PENDING RCM START=====--%>
                    <asp:View ID="View13" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Pending RCM vouchers" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label26" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox14" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender12" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox14" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label29" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox15" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender13" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox15" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button22" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button23" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button24" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel7" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView8" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="mb_no" HeaderText="MB No" />
                                                    <asp:BoundField DataField="wo_slno" HeaderText="Wo. SLNO" />
                                                    <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                                                    <asp:BoundField DataField="w_au" HeaderText="A / U" />
                                                    <asp:BoundField DataField="sac_code" HeaderText="SAC Code" />
                                                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Rate" />
                                                    <asp:BoundField DataField="work_qty" HeaderText="Work Qty" />
                                                    <asp:BoundField DataField="prov_amt" HeaderText="Taxable Value" />
                                                    <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                    <asp:BoundField DataField="cgst_liab" HeaderText="CGST Amt." />
                                                    <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                    <asp:BoundField DataField="sgst_liab" HeaderText="SGST Amt." />
                                                    <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                    <asp:BoundField DataField="igst_liab" HeaderText="IGST Amt" />
                                                    <asp:BoundField DataField="CESS" HeaderText="CESS" />
                                                    <asp:BoundField DataField="cess_liab" HeaderText="CESS Amt." />
                                                    <asp:BoundField DataField="TOTAL_VAL" HeaderText="Total value of Goods" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 14 RCM TAX INVOICE START=====--%>
                    <asp:View ID="View14" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="RCM Print" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1 mb-1">
                                            <div class="col-3 text-end">
                                                <asp:Label ID="Label10" runat="server" Text="RCM Invoice No" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox7" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>

                                            <div class="col-1 text-end">
                                                <asp:Label ID="Label55" runat="server" Font-Names="Times New Roman" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox36" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button8" runat="server" CssClass="btn btn-primary" Text="View" />
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>

                    </asp:View>

                    <%--=====VIEW 15 RIOC START=====--%>
                    <asp:View ID="View15" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="RIOC" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label27" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox12" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender8" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox12" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label28" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox13" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender9" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox13" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button17" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button19" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button18" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView7" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>

                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT Code" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT Name" />
                                                    <asp:BoundField DataField="OPEN_QTY" HeaderText="OPEN QTY" />
                                                    <asp:BoundField DataField="OPEN_VALUE" HeaderText="OPENING VALUE" />
                                                    <asp:BoundField DataField="RCD_QTY" HeaderText="RCD QTY" />
                                                    <asp:BoundField DataField="RCD_VALUE" HeaderText="RCD VALUE" />
                                                    <asp:BoundField DataField="ISSUE_QTY" HeaderText="ISSUE QTY" />
                                                    <asp:BoundField DataField="ISSUE_VALUE" HeaderText="ISSUE VALUE" />
                                                    <asp:BoundField DataField="MISC_SALE_QTY" HeaderText="MISC SALE QTY" />
                                                    <asp:BoundField DataField="MISC_SALE_VALUE" HeaderText="MISC SALE VALUE" />
                                                    <asp:BoundField DataField="CLOSING_QTY" HeaderText="CLOSING QTY" />
                                                    <asp:BoundField DataField="CLOSING_VALUE" HeaderText="CLOSING VALUE" />

                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 16 SCHEDULE START=====--%>
                    <asp:View ID="View16" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Schedule" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-3 text-end">
                                                <asp:Label ID="Label40" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox21" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender18" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox21" />
                                            </div>

                                            <div class="col-1 text-end">
                                                <asp:Label ID="Label9" runat="server" ForeColor="Blue" Style="text-align: left" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox6" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox6" />

                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary" Text="Go" />
                                                <asp:Button ID="Button15" runat="server" Text="Excel" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button7" runat="server" CssClass="btn btn-success" Text="Print" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel8" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView3" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="AC_NO" HeaderText="Ac. Code" />
                                                    <asp:BoundField DataField="AC_NAME" HeaderText="Ac. Name" />
                                                    <asp:BoundField DataField="SUPL_ID" HeaderText="Party Code" />
                                                    <asp:BoundField DataField="SUPL_NAME" HeaderText="Party Name" />
                                                    <asp:BoundField DataField="AMOUNT" HeaderText="Balance Amount" />
                                                    <asp:BoundField DataField="AMOUNT_TYPE" HeaderText="Amount Type" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:View>

                    <%--=====VIEW 17 TRIAL REPORT START=====--%>
                    <asp:View ID="View17" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Trial Report" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label3" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox2" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox2" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox3" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox3" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button14" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel4" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView1" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="AC_NO" HeaderText="A/c No" />
                                                    <asp:BoundField DataField="ac_description" HeaderText="A/c Name" />
                                                    <asp:BoundField DataField="AMOUNT_DR" HeaderText="Closing Dr" />
                                                    <asp:BoundField DataField="AMOUNT_CR" HeaderText="Closing Cr" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 18 VOUCHER START=====--%>
                    <asp:View ID="View18" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Normal Vourcher Report"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1 mb-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label2" runat="server" Text="Voucher No" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="View" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 19 LEDGER ENTRY START=====--%>
                    <asp:View ID="View19" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Ledger Entry"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1 mb-1">
                                    <div class="col-5 text-end">
                                        <asp:Label ID="Label46" runat="server" Text="Voucher No" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox24" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button35" runat="server" CssClass="btn btn-primary" Text="View" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 20 EGING START=====--%>
                    <asp:View ID="View20" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Aging Report" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label54" runat="server" Text="A/c No" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList4" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label58" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox25" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender21" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox8" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button36" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button38" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button39" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView11" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="AC_NO" HeaderText="A/c No" />
                                                    <asp:BoundField DataField="ac_description" HeaderText="A/c Name" />
                                                    <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL ID" />
                                                    <asp:BoundField DataField="SUPL_NAME" HeaderText="SUPL NAME" />
                                                    <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE No" />
                                                    <asp:BoundField DataField="EFECTIVE_DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="FISCAL_YEAR" HeaderText="FISCAL YEAR" />
                                                    <asp:BoundField DataField="AMOUNT_DR" HeaderText="DEBIT" />
                                                    <asp:BoundField DataField="AMOUNT_CR" HeaderText="CREDIT" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 21 TRIAL REPORT(MERGED A/C CODE) START=====--%>
                    <asp:View ID="View21" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Trial Report (Merged A/C Code)" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label60" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox26" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender22" runat="server" BehaviorID="TextBox26_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox26" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label61" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox27" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender23" runat="server" BehaviorID="TextBox27_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox27" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button40" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button41" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button42" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView12" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="AC_NO" HeaderText="A/c No" />
                                                    <asp:BoundField DataField="ac_description" HeaderText="A/c Name" />
                                                    <asp:BoundField DataField="AMOUNT_DR" HeaderText="Closing Dr" />
                                                    <asp:BoundField DataField="AMOUNT_CR" HeaderText="Closing Cr" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 22 PENDING GARN REPORT START=====--%>
                    <asp:View ID="View22" runat="server">
                        <div class="row justify-content-center align-items-center">
                            <div class="col text-center" style="background-color: #AAEEFF; border-color: #AAEEFF; border-style: Groove">
                                <div class="row justify-content-center">
                                    <div class="col text-center">
                                        <div class="row align-items-center">
                                            <div class="col text-center">
                                                <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Pending GARN Report" Font-Underline="True"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label63" runat="server" ForeColor="Blue" Style="text-align: left" Text="Date Between"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox28" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender24" runat="server" BehaviorID="TextBox26_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox28" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-end">
                                                <asp:Label ID="Label64" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox29" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender25" runat="server" BehaviorID="TextBox27_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox29" />
                                            </div>

                                            <div class="col text-start">
                                                <asp:Button ID="Button43" runat="server" CssClass="btn btn-primary" Text="Search" />
                                                <asp:Button ID="Button44" runat="server" Text="Download" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button45" runat="server" CssClass="btn btn-success" Text="Print"></asp:Button>
                                            </div>
                                        </div>


                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col g-0">
                                        <asp:Panel ID="Panel10" runat="server" ScrollBars="Auto" Width="100%">
                                            <asp:GridView ID="GridView13" CssClass="table table-bordered border-2 table-responsive text-center" Style="font-size: 15px" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True">
                                                <Columns>
                                                    <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
                                                    <asp:BoundField DataField="CRR_DATE" HeaderText="CRR DATE" DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL. ID" />
                                                    <asp:BoundField DataField="SUPL_NAME" HeaderText="SUPL. NAME" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="MAT. CODE" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="MAT. NAME" />
                                                    <asp:BoundField DataField="MAT_CHALAN_QTY" HeaderText="CHLN QTY." />
                                                    <asp:BoundField DataField="MAT_RCD_QTY" HeaderText="RCD QTY" />
                                                    <asp:BoundField DataField="GARN_NO" HeaderText="GARN STATUS" />
                                                    <asp:BoundField DataField="GARN_NOTE" HeaderText="REASON" />
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
