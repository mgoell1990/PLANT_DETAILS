<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="new_order.aspx.vb" Inherits="PLANT_DETAILS.new_order" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js"></script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox12]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/supl")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1]
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
                    $("[id$=TextBox12]").val(i.item.label);
                    $("[id$=TextBox81]").val(i.item.label);
                    document.getElementById("chkPartyCodePO").click();
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox81]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/supl")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1]
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
                    $("[id$=HiddenField1]").val(i.item.val);
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
                        url: '<%=ResolveUrl("~/Service.asmx/dater")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1]

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
                    
                    $("[id$=TextBox9]").val(i.item.label);
                    $("[id$=TextBox7]").val(i.item.label);
                    document.getElementById("btnSample").click();
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox9]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/dater")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1]
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
                    $("[id$=HiddenField1]").val(i.item.val);
                },
                minLength: 1
            });
        });
    </script>
    <asp:Button runat="server" ID="btnSample" ClientIDMode="Static" Text="" style="display:none;" OnClick="btnSample_Click" />
    <asp:Button runat="server" ID="chkPartyCodePO" ClientIDMode="Static" Text="" style="display:none;" OnClick="chkPartyCodePO_Click" />
    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label270" runat="server" Text="Generate New Order" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 GARN START=====--%>
                    <asp:View ID="View1" runat="server">

                        <div class="row justify-content-center mt-1">
                            <div class="col-7 justify-content-center" style="border-style: Groove; border-color: #FFFF66">

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-3 text-end">
                                        <asp:Label ID="Label344" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order Type"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList24" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Purchase Order</asp:ListItem>
                                            <asp:ListItem>Sale Order</asp:ListItem>
                                            <asp:ListItem>Work Order</asp:ListItem>
                                            <asp:ListItem>Rate Contract</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row justify-content-center align-items-center mt-1">
                                    <div class="col-3 text-end">
                                        <asp:Label ID="Label340" runat="server" Font-Bold="True" ForeColor="Blue" Text="Despatch Type:-" Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList23" runat="server" Visible="False">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Through D.O.</asp:ListItem>
                                            <asp:ListItem>Direct</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row justify-content-center align-items-center mt-1">
                                    <div class="col-3 text-end">
                                        <asp:Label ID="Label345" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order For"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList25" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                </div>



                                <div class="row justify-content-center align-items-center mt-2 mb-2">
                                    <div class="col-3 text-start">
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:Button ID="Button43" runat="server" class="btn btn-primary fw-bold" Text="Proceed" />
                                        <asp:Button ID="Button44" runat="server" class="btn btn-primary fw-bold" Text="Cancel" />

                                    </div>
                                </div>


                            </div>
                        </div>
                    </asp:View>
                    <%--=====VIEW 2 START=====--%>
                    <asp:View ID="View2" runat="server">

                        <div class="row">
                            <div class="col" style="border: 3px; border-style: Double; border-color: #00CC00">
                                <div class="row">

                                    <div class="col ms-1 text-start">
                                        <div class="row align-items-center mt-1">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label341" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Order No:-"></asp:Label>
                                            </div>
                                            <div class="col-3 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox82" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                            <div class="col-2 text-start g-0">
                                                <asp:Label ID="Label342" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Date:-"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="Delvdate6" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                <asp:Label ID="Label346" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Actual Order No:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox class="form-control" ID="TextBox3" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label268" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Date:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:TextBox class="form-control" ID="Delvdate1" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="Delvdate1_CalendarExtender" runat="server" BehaviorID="Delvdate1_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate1" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Quotation No:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox class="form-control" ID="TextBox4" runat="server">NA</asp:TextBox>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Date:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:TextBox class="form-control" ID="Delvdate2" runat="server">NA</asp:TextBox>
                                                <cc1:CalendarExtender ID="Delvdate2_CalendarExtender" runat="server" BehaviorID="Delvdate2_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate2" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label220" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="LOI No:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox class="form-control" ID="TextBox6" runat="server">NA</asp:TextBox>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label229" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Date:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:TextBox class="form-control" ID="Delvdate3" runat="server" TabIndex="5">NA</asp:TextBox>
                                                <cc1:CalendarExtender ID="Delvdate3_CalendarExtender" runat="server" BehaviorID="Delvdate3_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate3" />
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label218" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Enquiry No:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox class="form-control" ID="TextBox54" runat="server" TabIndex="6">NA</asp:TextBox>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label219" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Date:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:TextBox class="form-control" ID="TextBox55" runat="server" AutoCompleteType="Disabled" TabIndex="7">NA</asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBox55_CalendarExtender" runat="server" BehaviorID="TextBox55_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox55" />
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label222" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Indent No:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox class="form-control" ID="TextBox56" runat="server" TabIndex="8">NA</asp:TextBox>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label223" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Date:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:TextBox class="form-control" ID="TextBox57" runat="server" AutoCompleteType="Disabled" TabIndex="9">NA</asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBox57_CalendarExtender" runat="server" BehaviorID="TextBox57_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox57" />
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label247" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Currency:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox class="form-control" ID="TextBox8" runat="server" TabIndex="10">INR</asp:TextBox>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label248" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Value:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:TextBox class="form-control" ID="Delvdate4" runat="server" TabIndex="11">1</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label337" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Whether IPT:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:DropDownList class="form-select" ID="DropDownList20" runat="server" TabIndex="12">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>I.P.T.</asp:ListItem>
                                                    <asp:ListItem>Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label212" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Pur Grp File:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:TextBox class="form-control" ID="TextBox48" runat="server" TabIndex="13"></asp:TextBox>
                                            </div>
                                        </div>
                                        <asp:Panel ID="Panel46" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label230" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Party Code:-"></asp:Label>
                                                </div>
                                                <div class="col-7">
                                                    <asp:TextBox class="form-control" ID="TextBox12" runat="server" TabIndex="14"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label338" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Consign Code:-"></asp:Label>
                                                </div>
                                                <div class="col-7">
                                                    <asp:TextBox class="form-control" ID="TextBox81" runat="server" TabIndex="15"></asp:TextBox>
                                                </div>

                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel47" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Party Code:-"></asp:Label>
                                                </div>
                                                <div class="col-7">
                                                    <asp:TextBox class="form-control" ID="TextBox7" runat="server" TabIndex="16"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Consign Code:-"></asp:Label>
                                                </div>
                                                <div class="col-7">
                                                    <asp:TextBox class="form-control" ID="TextBox9" runat="server" TabIndex="17"></asp:TextBox>
                                                </div>

                                            </div>
                                        </asp:Panel>

                                        <div class="row align-items-center">
                                            <div class="col">
                                                <asp:Label ID="Label350" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="Red" Text="&lt;marquee&gt;Please do not Use &quot; ' &quot; (single quotation mark) in any field &lt;/marquee&gt;" Font-Size="Large"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" Text="Terms &amp; Conditions"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label250" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Payment Mode:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:DropDownList class="form-select" ID="payterm" runat="server" TabIndex="16">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>E.Payment</asp:ListItem>
                                                    <asp:ListItem>Cheque</asp:ListItem>
                                                    <asp:ListItem>Demand Draft</asp:ListItem>
                                                    <asp:ListItem>Book Adjustment</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label253" runat="server" Font-Bold="True" ForeColor="Blue" Text="Payment Term:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:DropDownList class="form-select" ID="paymode" runat="server" TabIndex="18">
                                                    <asp:ListItem>100% Against GRN Within 30 Days</asp:ListItem>
                                                    <asp:ListItem>90-10% Against GRN Within 30 Days</asp:ListItem>
                                                    <asp:ListItem>Against Running Bill</asp:ListItem>
                                                    <asp:ListItem>Advance Payment</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-2 g-0">
                                                <asp:Label ID="Label267" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Paying Agency:-"></asp:Label>
                                            </div>
                                            <div class="col-2">
                                                <asp:DropDownList class="form-select" ID="pay_agency" runat="server" TabIndex="19">
                                                    <asp:ListItem>SRU, Bhilai</asp:ListItem>
                                                    <asp:ListItem>Customer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <asp:Panel ID="Panel12" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="L.D. Applicability:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="ldapplicable" runat="server" TabIndex="21">
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel13" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label264" runat="server" Font-Bold="True" ForeColor="Blue" Text="Delivery Terms:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="delvterm" runat="server" TabIndex="23">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>N/A</asp:ListItem>
                                                        <asp:ListItem>By Customer</asp:ListItem>
                                                        <asp:ListItem>By SRU</asp:ListItem>
                                                        <asp:ListItem>Transporter Godown</asp:ListItem>
                                                        <asp:ListItem>By Supplier(Door Delivery)</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Mode of Despatch:-"></asp:Label>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList class="form-select" ID="despatch_mode" runat="server" TabIndex="24">
                                                        <asp:ListItem>By Road</asp:ListItem>
                                                        <asp:ListItem>By Train</asp:ListItem>
                                                        <asp:ListItem>By Ship</asp:ListItem>
                                                        <asp:ListItem>By Air</asp:ListItem>
                                                        <asp:ListItem>By Hand</asp:ListItem>
                                                        <asp:ListItem>By Post</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label251" runat="server" Font-Bold="True" ForeColor="Blue" Text="Origin Station:-"></asp:Label>
                                            </div>
                                            <div class="col-3">
                                                <asp:TextBox class="form-control" ID="destinatationTextBox" runat="server" TabIndex="26"></asp:TextBox>
                                            </div>

                                        </div>

                                        <asp:Panel ID="Panel15" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label252" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight Term:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="freightterm" runat="server" TabIndex="28">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Paid</asp:ListItem>
                                                        <asp:ListItem>Extra</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:Label ID="Label271" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="ITC Status"></asp:Label>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList class="form-select" ID="txt_ITC_Status" runat="server" TabIndex="19">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                        <asp:ListItem>No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="Panel10" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Insurance Term:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="insurance" runat="server" AutoPostBack="True" TabIndex="31">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem>Party Cost</asp:ListItem>
                                                        <asp:ListItem>Company Cost</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Insurance Rate:-"></asp:Label>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox class="form-control" ID="insupercent_TextBox" runat="server" BackColor="SeaGreen" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px" ReadOnly="True" TabIndex="32">0.00</asp:TextBox>
                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:DropDownList class="form-select" ID="insurancetype" runat="server" TabIndex="33">
                                                        <asp:ListItem>PERCENTAGE</asp:ListItem>
                                                        <asp:ListItem>AMOUNT</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="Panel16" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Misc. Charge:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="misccharg_ComboBox" runat="server" AutoPostBack="True" TabIndex="35">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:Label ID="Label272" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="S. Tax On MisChrg:-"></asp:Label>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox class="form-control" ID="misc_tax_ComboBox4" runat="server" TabIndex="36">0.00</asp:TextBox>
                                                    %
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel17" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label30" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Inspection Term:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="inspeterm_ComboBox" runat="server" TabIndex="38">
                                                        <asp:ListItem>N/A</asp:ListItem>
                                                        <asp:ListItem>By Vendor</asp:ListItem>
                                                        <asp:ListItem>By TC/GC</asp:ListItem>
                                                        <asp:ListItem>By Third Party</asp:ListItem>
                                                        <asp:ListItem>By SRU Bhilai</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:Label ID="Label282" runat="server" Font-Bold="True" ForeColor="Blue" Text="Third Party Inspection:-"></asp:Label>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList class="form-select" ID="third_party_insp" runat="server" TabIndex="39">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="Panel18" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Tolerance Percentage:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:TextBox class="form-control" ID="TextBox1" runat="server" TabIndex="52"></asp:TextBox>
                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:Label ID="Label256" runat="server" Font-Bold="True" ForeColor="Blue" Text="Inespection/Testing/Quality Plan :-"></asp:Label>
                                                </div>
                                                <div class="col-2">
                                                    <asp:DropDownList class="form-select" ID="insp_test_ComboBox4" runat="server" TabIndex="41">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="insp_test_TextBox25" runat="server" TabIndex="42" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel19" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="PVC Clause:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="pvc_ComboBox" runat="server" TabIndex="43">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="pvc_TextBox26" runat="server" CssClass="form-control" TabIndex="44" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel20" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Penality Clause:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="bonus_ComboBox" runat="server" TabIndex="45">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="bonus_TextBox27" runat="server" CssClass="form-control" TabIndex="46" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel21" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label257" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Performance Evalutation Clause:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="performance_ComboBox0" runat="server" TabIndex="47">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="performance_TextBox28" runat="server" CssClass="form-control" TabIndex="48" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="Panel22" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label258" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Performance Guarantee:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="per_gurrenty_ComboBox1" runat="server" TabIndex="49">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="per_gurrenty_TextBox29" runat="server" CssClass="form-control" TabIndex="50" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel23" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label259" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Security Deposit:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="sd_ComboBox2" runat="server" AutoPostBack="True" TabIndex="51">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>E-PAYMENT</asp:ListItem>
                                                        <asp:ListItem>CASH</asp:ListItem>
                                                        <asp:ListItem>BANK GUARANTEE</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                                <div class="col-2 g-0">
                                                    <asp:Label ID="Label284" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.D. Percentage:-"></asp:Label>
                                                </div>
                                                <div class="col-2">
                                                    <asp:TextBox class="form-control" ID="sd_TextBox46" runat="server" TabIndex="52">0.00</asp:TextBox>
                                                </div>
                                                <div class="col-1 g-0 text-start">
                                                    <asp:Label ID="Label285" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="%"></asp:Label>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="Panel24" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label260" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Special Gurantee / Warranty Clause :-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="sp_gur_ComboBox3" runat="server" TabIndex="54">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>

                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="sp_gur_TextBox31" runat="server" CssClass="form-control" TabIndex="55" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel25" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label261" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Matching Part Details:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="match_ComboBox2" runat="server" TabIndex="56">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>
                                        </asp:Panel>


                                        <asp:Panel ID="Panel26" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label22" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Quantity Variation Clause:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="quantity_ComboBox" runat="server" TabIndex="58">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>
                                        </asp:Panel>


                                        <asp:Panel ID="Panel27" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label262" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Special Terms Related To Supply Of Medicines:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="medicine_ComboBox0" runat="server" TabIndex="60">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel28" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="spl_del_Label263" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Special Instruction:-"></asp:Label>
                                                </div>
                                                <div class="col-3">
                                                    <asp:DropDownList class="form-select" ID="spl_del_ComboBox1" runat="server" TabIndex="62">
                                                        <asp:ListItem>Not Applicable</asp:ListItem>
                                                        <asp:ListItem>Applicable</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-2 g-0">
                                                </div>
                                                <div class="col-2">
                                                </div>

                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="spl_del_TextBox35" runat="server" CssClass="form-control" TabIndex="63" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                        </asp:Panel>

                                        <asp:Panel ID="Panel29" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label239" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Documents to be submitted along with material supply"></asp:Label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox class="form-control" ID="doc_sub_m_supl_TextBox49" runat="server" CssClass="form-control" TabIndex="64" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel30" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label240" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Documents to be submitted along with bill of payment"></asp:Label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox class="form-control" ID="doc_bill_pay_TextBox50" runat="server" CssClass="form-control" TabIndex="65" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="Panel31" runat="server">
                                            <div class="row align-items-center">
                                                <div class="col-2 g-0 text-start">
                                                    <asp:Label ID="Label241" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Invoicing Party / payment to be made to"></asp:Label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox class="form-control" ID="inv_party_TextBox51" runat="server" CssClass="form-control" TabIndex="66" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label243" runat="server" Font-Bold="True" ForeColor="Blue" Text="General terms and conditions"></asp:Label>
                                            </div>
                                            <div class="col">
                                                <asp:TextBox class="form-control" ID="general_term_TextBox52" runat="server" CssClass="form-control" TabIndex="67" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 g-0 text-start">
                                                <asp:Label ID="Label244" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Blue" Style="text-align: left" Text="Note:-"></asp:Label>
                                            </div>
                                            <div class="col">
                                                <asp:TextBox class="form-control" ID="note_TextBox53" runat="server" CssClass="form-control" TabIndex="68" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                                <div class="row align-items-center mt-1">
                                    <div class="col text-center">
                                        <asp:Button ID="Button33" runat="server" Text="Save" CssClass="btn btn-success fw-bold" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                        <asp:Button ID="Button32" runat="server" Text="Cancel" CssClass="btn btn-danger fw-bold" />
                                        <%--<asp:Button ID="btnSample" runat="server" Text="Button" CssClass="btn btn-danger fw-bold" OnClick="btnSample_Click"/>--%>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">

                                    <div class="col text-center">
                                        <asp:Label ID="Label351" runat="server" Text="Label" Visible="False"></asp:Label>
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
