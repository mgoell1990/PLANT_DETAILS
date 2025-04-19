<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="jur_voucher.aspx.vb" Inherits="PLANT_DETAILS.jur_voucher" %>

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
            $("[id$=DropDownList29]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/GET_BE_BL_NUMBER")%>',
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
            SetAutoCompleteSuplCode();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetAutoCompleteSuplCode();
                }
            });
        };

        function SetAutoCompleteSuplCode() {
            $("[id$=DropDownList25]").autocomplete({
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
                                };
                            }))
                        }
                    });
                }
            });
        }


        <%--$(function () {
            $("[id$=DropDownList25]").autocomplete({
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
        });--%>
    </script>

    <script type="text/javascript">
        <%--$(function () {
            $("[id$=DropDownList26]").autocomplete({
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
        });--%>

         $(function () {
            SetAutoCompleteAccountCode();
        });

        //On UpdatePanel Refresh.
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    SetAutoCompleteAccountCode();
                }
            });
        };

        function SetAutoCompleteAccountCode() {
            $("[id$=DropDownList26]").autocomplete({
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
                                };
                            }))
                        }
                    });
                }
            });
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label94" runat="server" Text="Journal Voucher" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">


        <div class="col-5 text-end">
            <asp:Label ID="Label4" runat="server" Text="Select Type" Font-Names="Times New Roman"></asp:Label>
        </div>
        <div class="col-2 text-end">
            <asp:DropDownList class="form-select" ID="DropDownList10" runat="server" AutoPostBack="True">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>HO DA</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-3 text-end">
            <asp:Label ID="Label2" runat="server" Text="Date" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row mt-1 justify-content-center text-center">
                    <div class="col text-center">
                        <asp:Panel ID="Panel30" runat="server">
                            <div class="row justify-content-center">
                                <div class="col-12 justify-content-center m-1" style="border-color: Blue; border-style: groove">

                                    <div class="row align-items-center mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label457" runat="server" ForeColor="Blue" Text="Token No"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox61" runat="server" BackColor="#4686F0" ForeColor="White"></asp:TextBox>
                                        </div>
                                        <div class="col text-start">
                                        </div>
                                    </div>

                                    <div class="row  align-items-center mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label6" runat="server" ForeColor="Blue" Text="Taxable Amount"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start g-0">
                                            <asp:TextBox class="form-control" ID="txtTaxableAmount" runat="server">0</asp:TextBox>
                                        </div>
                                        <div class="col-1 text-start">
                                            <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="CGST"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start ">
                                            <asp:TextBox class="form-control" ID="TextBox5" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-start g-0">
                                            <asp:Label ID="Label8" runat="server" ForeColor="Blue" Text="SGST"></asp:Label>
                                        </div>

                                        <div class="col-1 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox6" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-start">
                                            <asp:Label ID="Label9" runat="server" ForeColor="Blue" Text="IGST"></asp:Label>
                                        </div>
                                        <div class="col-1 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <asp:Panel ID="Panel1" runat="server">
                                        <div class="row  align-items-center mt-1">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="Select BE No"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start g-0">
                                                <asp:TextBox class="form-control" ID="DropDownList29" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-1 text-start">
                                                <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="Payment %"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList2" runat="server" AutoPostBack="True">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>90</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </asp:Panel>



                                    <div class="row  align-items-center mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label458" runat="server" ForeColor="Blue" Text="Section Sl No"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox62" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-start">
                                            <asp:Label ID="Label459" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox63" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextBox63_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox63"></asp:CalendarExtender>
                                        </div>

                                    </div>


                                    <div class="row  align-items-center mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label653" runat="server" ForeColor="Blue" Text="Voucher No"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox181" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-start">
                                            <asp:Label ID="Label654" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox182" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                            <asp:CalendarExtender ID="TextBox182_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox182" />
                                        </div>
                                    </div>

                                    <div class="row  align-items-center mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label469" runat="server" ForeColor="Blue" Text="Type of JV"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList28" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Normal</asp:ListItem>
                                                <asp:ListItem>Payroll</asp:ListItem>
                                                <asp:ListItem>To Be Reversed</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-1 text-start">
                                            <asp:Label ID="Label634" runat="server" ForeColor="Blue" Text="Invoice No."></asp:Label>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox177" runat="server" TabIndex="8"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row  align-items-center">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label460" runat="server" ForeColor="Blue" Text="Supl Code"></asp:Label>
                                        </div>
                                        <div class="col-6 text-start g-0">
                                            <asp:TextBox class="form-control" ID="DropDownList25" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row  align-items-center">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label544" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration"></asp:Label>
                                        </div>
                                        <div class="col-6 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox94" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row align-items-center mt-1" style="background-color: #4686F0">
                                        <div class="col">
                                            <asp:Label ID="Label112" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Amount Details"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row  align-items-center mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label466" runat="server" ForeColor="Blue" Text="A/C Head"></asp:Label>
                                        </div>
                                        <div class="col-3 text-start g-0">
                                            <asp:TextBox class="form-control" ID="DropDownList26" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-1 text-start">
                                            <asp:Label ID="Label467" runat="server" ForeColor="Blue" Text="Amount"></asp:Label>
                                        </div>
                                        <div class="col-1 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox64" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-start">
                                            <asp:Label ID="Label512" runat="server" ForeColor="Blue" Text="Amt Type"></asp:Label>
                                        </div>
                                        <div class="col-1 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="catgory_DropDownList0" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Dr</asp:ListItem>
                                                <asp:ListItem>Cr</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col text-start">
                                            <asp:Button ID="Button22" runat="server" Text="Add" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                                            <asp:Button ID="Button23" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                            <asp:Button ID="Button24" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                        </div>
                                    </div>

                                    <div class="row align-items-center mt-2">
                                        <div class="col" style="overflow: scroll">
                                            <asp:GridView ID="GridView2" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="true">
                                            </asp:GridView>
                                        </div>
                                    </div>




                                    <div class="row  align-items-center mt-1">
                                        <div class="col text-start">
                                            <asp:Label ID="Label468" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>

