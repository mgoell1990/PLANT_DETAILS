<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="debtor.aspx.vb" Inherits="PLANT_DETAILS.debtor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            $("[id$=TextBox124]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/DEB_DETAILS")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    d_name: item.split('^')[1],
                                    add_1: item.split('^')[2],
                                    add_2: item.split('^')[3],
                                    d_range: item.split('^')[4],
                                    d_city: item.split('^')[5],
                                    d_coll: item.split('^')[6],
                                    ecc_no: item.split('^')[7],
                                    tin_no: item.split('^')[8],
                                    stock_ac_head: item.split('^')[9],
                                    iuca_head: item.split('^')[10],
                                    supl_loc: item.split('^')[11],
                                    JOB_WORK: item.split('^')[12],
                                    deb_loc: item.split('^')[13],
                                    gst_code: item.split('^')[14],
                                    d_state: item.split('^')[15],
                                    d_state_code: item.split('^')[16],
                                    d_pin: item.split('^')[17],
                                    data_clr: item.split('^')[18]
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
                    $("[id$=TextBox125]").val(i.item.d_name);
                    $("[id$=TextBox127]").val(i.item.add_1);
                    $("[id$=TextBox129]").val(i.item.add_2);
                    $("[id$=TextBox147]").val(i.item.d_range);
                    $("[id$=TextBox148]").val(i.item.d_city);
                    $("[id$=TextBox142]").val(i.item.d_coll);
                    $("[id$=TextBox144]").val(i.item.ecc_no);
                    $("[id$=TextBox143]").val(i.item.tin_no);
                    $("[id$=TextBox146]").val(i.item.stock_ac_head);
                    $("[id$=TextBox145]").val(i.item.iuca_head);
                    $("[id$=DropDownList1]").val(i.item.supl_loc);
                    $("[id$=TextBox149]").val(i.item.JOB_WORK);
                    $("[id$=SUPLDropDownList18]").val(i.item.deb_loc);
                    $("[id$=TextBox1]").val(i.item.gst_code);
                    $("[id$=TextBox2]").val(i.item.d_state);
                    $("[id$=TextBox3]").val(i.item.d_state_code);
                    $("[id$=ERR_LABLE0]").val(i.item.d_name);
                    $("[id$=TextBox150]").val(i.item.d_pin);
                },
                minLength: 1
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox125]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/DEB_DETAILS")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[1],
                                    d_name: item.split('^')[0],
                                    add_1: item.split('^')[2],
                                    add_2: item.split('^')[3],
                                    d_range: item.split('^')[4],
                                    d_city: item.split('^')[5],
                                    d_coll: item.split('^')[6],
                                    ecc_no: item.split('^')[7],
                                    tin_no: item.split('^')[8],
                                    stock_ac_head: item.split('^')[9],
                                    iuca_head: item.split('^')[10],
                                    supl_loc: item.split('^')[11],
                                    JOB_WORK: item.split('^')[12],
                                    deb_loc: item.split('^')[13],
                                    gst_code: item.split('^')[14],
                                    d_state: item.split('^')[15],
                                    d_state_code: item.split('^')[16],
                                    d_pin: item.split('^')[17],
                                    data_clr: item.split('^')[18]
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
                    $("[id$=TextBox124]").val(i.item.d_name);
                    $("[id$=TextBox127]").val(i.item.add_1);
                    $("[id$=TextBox129]").val(i.item.add_2);
                    $("[id$=TextBox147]").val(i.item.d_range);
                    $("[id$=TextBox148]").val(i.item.d_city);
                    $("[id$=TextBox142]").val(i.item.d_coll);
                    $("[id$=TextBox144]").val(i.item.ecc_no);
                    $("[id$=TextBox143]").val(i.item.tin_no);
                    $("[id$=TextBox146]").val(i.item.stock_ac_head);
                    $("[id$=TextBox145]").val(i.item.iuca_head);
                    $("[id$=DropDownList1]").val(i.item.supl_loc);
                    $("[id$=TextBox149]").val(i.item.JOB_WORK);
                    $("[id$=SUPLDropDownList18]").val(i.item.deb_loc);
                    $("[id$=TextBox1]").val(i.item.gst_code);
                    $("[id$=TextBox2]").val(i.item.d_state);
                    $("[id$=TextBox3]").val(i.item.d_state_code);
                    $("[id$=ERR_LABLE0]").val(i.item.d_name);
                    $("[id$=TextBox150]").val(i.item.d_pin);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox145]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head_new")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    AC_NAME: item.split('^')[1],
                                    AC_TYPE: item.split('^')[2],
                                    AC_GROUP: item.split('^')[3]
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
                    $("[id$=TextBox145]").val(i.item.label);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox146]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head_new")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    AC_NAME: item.split('^')[1],
                                    AC_TYPE: item.split('^')[2],
                                    AC_GROUP: item.split('^')[3]
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
            $("[id$=TextBox149]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head_new")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    AC_NAME: item.split('^')[1],
                                    AC_TYPE: item.split('^')[2],
                                    AC_GROUP: item.split('^')[3]
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

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Add/Update Customer Details" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-2 justify-content-center text-center">
            <div class="col-10" style="border: 5px groove #FF0066; float: left; text-align: left;">
                <div class="row">
                    <div class="col-8" style="border-right: 5px groove #FF0066;">
                        <div class="row align-items-center mt-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label366" runat="server" ForeColor="Blue" Text="Customer Code"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox class="form-control" ID="TextBox124" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label367" runat="server" ForeColor="Blue" Text="Company Name"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox class="form-control" ID="TextBox125" runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label369" runat="server" ForeColor="Blue" Text="Customer Loc."></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:DropDownList class="form-select" ID="SUPLDropDownList18" runat="server" Font-Names="Times New Roman">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Within State</asp:ListItem>
                                    <asp:ListItem>Out Side State</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>



                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label397" runat="server" ForeColor="Blue" Text="Party Type"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" Font-Names="Times New Roman">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>IPT</asp:ListItem>
                                    <asp:ListItem>OTHER</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>



                    </div>

                    <div class="col-4 align-top">

                        <div class="row mt-2 align-items-center">
                            <div class="col-5 text-end">
                                <asp:Label ID="Label5" runat="server" Font-Bold="True">Password : </asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox class="form-control" ID="TextBox114" runat="server" TextMode="Password"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-5 text-end">
                            </div>
                            <div class="col-7 text-start mt-2">
                                <asp:Button ID="Button49" runat="server" class="btn btn-primary fw-bold" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                <asp:Button ID="Button50" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col text-start">
                                <asp:Label ID="ERR_LABLE0" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row align-items-center justify-content-center mt-1" style="background-color: #4686F0">
                    <div class="col-7 text-center">
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Address Details"></asp:Label>
                    </div>

                    <div class="col-5 text-center">
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Bank Details"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-8" style="border-right: 5px groove #FF0066;">
                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label373" runat="server" ForeColor="Blue" Text="Address 1"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox127" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label375" runat="server" ForeColor="Blue" Text="Address 2"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox129" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label395" runat="server" ForeColor="Blue" Text="Range"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox147" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="GST NO."></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label400" runat="server" ForeColor="Blue" Text="Pin Code"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox150" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label396" runat="server" ForeColor="Blue" Text="City"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox148" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="State"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end g-0">
                                <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="State Code"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox3" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="col-4 align-top">

                        <div class="row mt-1 align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label399" runat="server" ForeColor="Blue" Text="IUCA Code"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox145" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label394" runat="server" ForeColor="Blue" Text="S.T. Code"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox146" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label398" runat="server" ForeColor="Blue" Text="Job Code" ></asp:Label> 
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox149" runat="server" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center justify-content-center mt-1" style="background-color: #4686F0">
                            <div class="col text-center">
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Registration Details"></asp:Label>
                            </div>

                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label389" runat="server" ForeColor="Blue" Text="D.Coll" ></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox142" runat="server" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label390" runat="server" ForeColor="Blue" Text="TIN No" ></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox143" runat="server" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label391" runat="server" ForeColor="Blue" Text="ECC No" ></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox144" runat="server" ></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col text-start">
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>

</asp:Content>
