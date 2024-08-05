<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="ac_head.aspx.vb" Inherits="PLANT_DETAILS.ac_head" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script>

        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox1]").autocomplete({
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
                    $("[id$=TextBox3]").val(i.item.AC_NAME);
                    $("[id$=TextBox4]").val(i.item.AC_TYPE);
                    $("[id$=TextBox2]").val(i.item.AC_GROUP);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox3]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head_name")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[1],
                                    AC_CODE: item.split('^')[0],
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
                    $("[id$=TextBox1]").val(i.item.AC_CODE);
                    $("[id$=TextBox4]").val(i.item.AC_TYPE);
                    $("[id$=TextBox2]").val(i.item.AC_GROUP);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox4]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head_type")%>',
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
            $("[id$=TextBox2]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head_group")%>',
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
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />


    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Add/Update Account Head" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center">
                    <div class="col-7 text-center">
                        <asp:Panel ID="Panel16" runat="server" BorderColor="Blue" BorderStyle="Groove">
                            <div class="row">
                                <div class="col m-1">
                                    <div class="row align-items-center">
                                        <div class="col-5 text-end">
                                            <asp:Label ID="Label2" runat="server" Text="Account Head"></asp:Label>
                                        </div>
                                        <div class="col-6 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                                        </div>


                                    </div>

                                    <div class="row align-items-center mt-0">
                                        <div class="col-5 text-end">
                                            <asp:Label ID="Label6" runat="server" Text="Account Head Desc."></asp:Label>
                                        </div>
                                        <div class="col-6 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox3" runat="server"></asp:TextBox>
                                        </div>


                                    </div>

                                    <div class="row align-items-center mt-1">
                                        <div class="col-5 text-end">
                                            <asp:Label ID="Label5" runat="server" Text="Account Head Type"></asp:Label>
                                        </div>
                                        <div class="col-6 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox4" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row align-items-center mt-1">

                                        <div class="col-5 text-end">
                                            <asp:Label ID="Label4" runat="server" Text="Account Head Group"></asp:Label>
                                        </div>
                                        <div class="col-6 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                                        </div>

                                    </div>


                                    <div class="row align-items-center mt-2">
                                        <div class="col-5 text-end">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="True">Admin Password : </asp:Label>
                                        </div>
                                        <div class="col-6 text-center">
                                            <asp:TextBox class="form-control" ID="TextBox114" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                        
                                    </div>

                                    <div class="row align-items-center mt-3">
                                        <div class="col-5 text-end">
                                        </div>
                                        <div class="col-6 text-end">
                                            <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-primary fw-bold" Font-Size="Small" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                            <asp:Button ID="Button2" runat="server" class="btn btn-danger fw-bold" Text="Cancel" Font-Size="Small" />
                                            <asp:Button ID="Button3" runat="server" class="btn btn-danger fw-bold" Text="Close" Font-Size="Small" />
                                        </div>
                                        
                                    </div>

                                    <div class="row align-items-center mt-2">
                                        <div class="col text-start">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>


            </div>
        </div>
    </div>


</asp:Content>
