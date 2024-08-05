<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="misc_voucher.aspx.vb" Inherits="PLANT_DETAILS.misc_voucher" %>

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
            $("[id$=pay_paid_TextBox82]").autocomplete({
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
            $("[id$=pay_supl_code_TextBox98]").autocomplete({
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
            $("[id$=pay_ac_head_TextBox95]").autocomplete({
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

    <script type="text/javascript">
        $(function () {
            $("[id$=jpay_ac_DropDownList38]").autocomplete({
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
            <asp:Label ID="Label94" runat="server" Text="Miscellaneous Voucher" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>

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
                                    <asp:Label ID="Label626" runat="server" ForeColor="Blue" Text="Token No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="pay_vou_TextBox76" runat="server" BackColor="#4686F0" ForeColor="White"></asp:TextBox>
                                </div>
                                <div class="col text-start">
                                    <asp:Label ID="Label627" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label653" runat="server" ForeColor="Blue" Text="Taxable Amount"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="txtTaxableAmount" runat="server">0</asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="CGST"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox5" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start g-0">
                                    <asp:Label ID="Label6" runat="server" ForeColor="Blue" Text="SGST"></asp:Label>
                                </div>

                                <div class="col-1 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox6" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label654" runat="server" ForeColor="Blue" Text="IGST"></asp:Label>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox181" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label628" runat="server" ForeColor="Blue" Text="Section Sl No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="pay_sec_slno_TextBox77" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label629" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="pay_date_TextBox78" runat="server" TabIndex="1" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="pay_date_TextBox78_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="pay_date_TextBox78" />
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label647" runat="server" ForeColor="Blue" Text="Voucher No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox179" runat="server" TabIndex="2"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label648" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox180" runat="server" TabIndex="3" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox180_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox180_CalendarExtender" TargetControlID="TextBox180" />
                                </div>

                                <div class="col-1 text-start g-0">
                                    <asp:Label ID="Label630" runat="server" Font-Bold="False" ForeColor="Blue" Text="Voucher Type"></asp:Label>
                                </div>
                                <div class="col-1 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="pay_vouch_type_DropDownList36" runat="server" TabIndex="4">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>B.P.V</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label631" runat="server" Font-Bold="False" ForeColor="Blue" Text="Mode"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:DropDownList class="form-select" ID="pay_mode_DropDownList37" runat="server" AutoPostBack="True" TabIndex="5">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Advance</asp:ListItem>
                                        <asp:ListItem>Through Liab.</asp:ListItem>
                                        <asp:ListItem>Direct Exp.</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label632" runat="server" ForeColor="Blue" Text="P.O./WO No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="pay_po_wo_TextBox96" runat="server" TabIndex="6"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label633" runat="server" ForeColor="Blue" Text="GARN /MB No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="pay_garn_TextBox97" runat="server" TabIndex="7"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label634" runat="server" ForeColor="Blue" Text="Invoice/IOC No."></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox177" runat="server" TabIndex="8"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label635" runat="server" ForeColor="Blue" Text="Inv. Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox178" runat="server" TabIndex="9" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox178_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox178_CalendarExtender" TargetControlID="TextBox178" />
                                </div>
                                <div class="col text-end">
                                    <asp:LinkButton ID="PAY_LinkButton1" runat="server" ForeColor="Red">Add Journal</asp:LinkButton>
                                </div>
                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label637" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="pay_narration_TextBox81" TextMode="MultiLine" runat="server" TabIndex="10"></asp:TextBox>
                                </div>

                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label651" runat="server" ForeColor="Blue" Text="Being Paid To"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="pay_paid_TextBox82" runat="server" TabIndex="11"></asp:TextBox>
                                </div>
                                <div class="col text-end">
                                    <asp:Button ID="PAY_Button36" runat="server" Font-Bold="True" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                    <asp:Button ID="Button60" runat="server" Text="New" CssClass="btn btn-primary" />
                                </div>
                            </div>

                            <div class="row align-items-center mt-1" style="background-color: #4686F0">
                                <div class="col">
                                    <asp:Label ID="Label112" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Details"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label636" runat="server" ForeColor="Blue" Text="Supl Code"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="pay_supl_code_TextBox98" runat="server" TabIndex="12"></asp:TextBox>
                                </div>
                            </div>

                            <asp:Panel ID="PAY_Panel22" runat="server">

                                <div class="row align-items-center mt-1">
                                    <div class="col text-start">
                                        <div class="row  align-items-center mt-1">
                                            <div class="col text-center">
                                                <asp:Label ID="Label639" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Blue" Style="text-align: center" Text="Payment Entry"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row  align-items-center mt-1">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label640" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/C Head"></asp:Label>
                                            </div>
                                            <div class="col-5 text-start g-0">
                                                <asp:TextBox class="form-control" ID="pay_ac_head_TextBox95" runat="server" TabIndex="13"></asp:TextBox>
                                            </div>
                                            <div class="col-1 text-start">
                                                <asp:Label ID="Label641" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amount"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="pay_amount_TextBox89" runat="server" TabIndex="14"></asp:TextBox>
                                            </div>
                                            <div class="col text-end">
                                                <asp:Button ID="PAY_Button35" runat="server" Font-Bold="True" Text="Add" CssClass="btn btn-primary" TabIndex="15" />
                                                <asp:Button ID="PAY_Button37" runat="server" Font-Bold="True" Text="Cancel" CssClass="btn btn-danger" TabIndex="16" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col" style="overflow: scroll">
                                        <asp:GridView ID="pay_GridView8" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL CODE" />
                                                <asp:BoundField DataField="AC_HEAD" HeaderText="A/C HEAD">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="A/C_DESCRIPTION" DataField="AC_DESC" />
                                                <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT">

                                                    <ItemStyle Width="150px" />

                                                </asp:BoundField>
                                                <asp:BoundField DataField="INVOICE_NO" HeaderText="INVOICE NO.">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row  align-items-center mt-1">
                                    <div class="col text-end">
                                        <asp:Label ID="Label3" runat="server" Text="Total"></asp:Label>
                                    </div>
                                    <div class="col-2 text-end">
                                        <asp:TextBox class="form-control" ID="TextBox2" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>


                            <asp:Panel ID="JNL_Panel23" runat="server" Visible="false">

                                <div class="row align-items-center mt-1">
                                    <div class="col text-start">
                                        <div class="row  align-items-center mt-1">
                                            <div class="col text-center">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Blue" Style="text-align: center" Text="JE Entry"></asp:Label>
                                            </div>

                                        </div>
                                        <div class="row  align-items-center mt-1">
                                            <div class="col-1 text-start">
                                                <asp:Label ID="Label643" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/C Head"></asp:Label>
                                            </div>
                                            <div class="col-3 text-start g-0">
                                                <asp:TextBox class="form-control" ID="jpay_ac_DropDownList38" runat="server" TabIndex="17"></asp:TextBox>
                                            </div>
                                            <div class="col-1 text-start g-0 ms-1">
                                                <asp:Label ID="Label644" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amount"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start g-0">
                                                <asp:TextBox class="form-control" ID="jpay_amount_TextBox95" runat="server" TabIndex="18"></asp:TextBox>
                                            </div>
                                            <div class="col-1 text-start g-0 ms-1">
                                                <asp:Label ID="Label645" runat="server" Font-Bold="True" ForeColor="Blue" Text="Type"></asp:Label>
                                            </div>
                                            <div class="col-1 text-start g-0">
                                                <asp:DropDownList class="form-select" ID="jpay_catgory_DropDownList1" runat="server" TabIndex="19">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>Dr</asp:ListItem>
                                                    <asp:ListItem>Cr</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col text-end">
                                                <asp:Button ID="JNL_Button38" runat="server" Font-Bold="True" Text="Add" CssClass="btn btn-primary" TabIndex="20" />
                                                <asp:Button ID="JNL_Button39" runat="server" Font-Bold="True" Text="Save" CssClass="btn btn-success" TabIndex="21" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="JNL_Button40" runat="server" Font-Bold="True" Text="Cancel" CssClass="btn btn-danger" TabIndex="22" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col" style="overflow: scroll">
                                        <asp:GridView ID="jpay_GridView9" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="AC_HEAD" HeaderText="AC HEAD">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AC_DESC" HeaderText="A/C DESCRIPTION" />
                                                <asp:BoundField DataField="AMOUNT_DR" HeaderText="DEBIT AMOUNT">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="AMOUNT_CR" HeaderText="CREDIT AMOUNT">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL ID" />
                                                <asp:BoundField DataField="supl_name" HeaderText="SUPL NAME" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="row  align-items-center mt-1">
                                    <div class="col text-end">
                                        <asp:Label ID="Label4" runat="server" Text="Total" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="col-2 text-end">
                                        <asp:TextBox class="form-control" ID="TextBox3" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-end">
                                        <asp:TextBox class="form-control" ID="TextBox4" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </asp:Panel>


                           


                           

                            <div class="row  align-items-center mt-1">
                                <div class="col text-start">
                                    <asp:Label ID="Label638" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>                         


                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
