<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="add_order.aspx.vb" Inherits="PLANT_DETAILS.add_order" %>

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
            $("[id$=TextBox832]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/po_no_search")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0]
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
            $("[id$=TextBox45]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/Outsource_F_ITEM")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox48]").val(i.item.ITEM_AU);
                    $("[id$=HiddenField2]").val(i.item.ITEM_AU);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=po_matcodecombo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODS")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox816]").val(i.item.ITEM_AU);
                    $("[id$=HiddenField2]").val(i.item.ITEM_AU);
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
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODACT")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox48]").val(i.item.ITEM_AU);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=po_matcodecombo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODS")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox816]").val(i.item.ITEM_AU);
                    $("[id$=HiddenField2]").val(i.item.ITEM_AU);
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
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODMT")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox816]").val(i.item.ITEM_AU);
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
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODACT")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox816]").val(i.item.ITEM_AU);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox25]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    MAT_AU: item.split('^')[2]
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
                    $("[id$=TextBox26]").val(i.item.MAT_AU);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox40]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/Outsource_F_ITEM")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    MAT_AU: item.split('^')[2]
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
                    $("[id$=TextBox41]").val(i.item.MAT_AU);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=DropDownList12]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    mat_au: item.split('^')[2],
                                    mat_rate: item.split('^')[3]
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
                    $("[id$= TextBox79]").val(i.item.mat_au);
                    $("[id$= TextBox77]").val(i.item.mat_rate);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=po_matcodecombo0]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    MAT_AU: item.split('^')[2]
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
                    $("[id$=TextBox815]").val(i.item.MAT_AU);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=po_matcodecombo1]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    MAT_AU: item.split('^')[2]
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
                    $("[id$=TextBox821]").val(i.item.MAT_AU);
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
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODMT")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox816]").val(i.item.ITEM_AU);
                    $("[id$=HiddenField1]").val(i.item.ITEM_AU);
                },
                minLength: 1
            });
        });
    </script>
    <script type="text/javascript">

        $(function () {
            $("[id$=po_matcodecombo]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODS")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox816]").val(i.item.ITEM_AU);
                    $("[id$=HiddenField1]").val(i.item.ITEM_AU);
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
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODACT")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_WEIGHT: item.split('^')[3]
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
                    $("[id$=TextBox816]").val(i.item.ITEM_AU);
                    $("[id$=HiddenField1]").val(i.item.ITEM_AU);
                },
                minLength: 1
            });
        });
    </script>

    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <%--<asp:Button runat="server" ID="btnSample" ClientIDMode="Static" Text="" Style="display: none;" OnClick="btnSample_Click" />
    <asp:Button runat="server" ID="chkPartyCodePO" ClientIDMode="Static" Text="" Style="display: none;" OnClick="chkPartyCodePO_Click" />--%>
    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label270" runat="server" Text="Add Order Details" Font-Bold="True" Font-Size="Larger"></asp:Label>
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
                                    <div class="col-4 text-end">
                                        <asp:Label ID="Label348" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-"></asp:Label>
                                    </div>
                                    <div class="col-8 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox832" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="row justify-content-center align-items-center mt-2 mb-2">
                                    <div class="col-4 text-start">
                                    </div>
                                    <div class="col-8 text-start">
                                        <asp:Button ID="Button45" runat="server" class="btn btn-primary fw-bold" Text="Proceed" />
                                        <asp:Button ID="Button46" runat="server" class="btn btn-primary fw-bold" Text="Cancel" />
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2 mb-2">
                                    <div class="col text-start">
                                        <asp:Label ID="Label678" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </asp:View>
                    <%--=====VIEW 2 Purchase Order START=====--%>
                    <asp:View ID="View2" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label108" runat="server" Font-Bold="True" ForeColor="White" Text="Material Details"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label387" runat="server" Font-Bold="True" ForeColor="Blue" Text="P.O. / Ref. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox102" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label388" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox103" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label439" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label390" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox105" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label391" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox106" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-5 text-start">
                                    </div>
                                    <div class="col-7 text-end">
                                        <asp:Button ID="Button49" runat="server" class="btn btn-primary fw-bold" Text="Save" />
                                        <asp:Button ID="Button60" runat="server" class="btn btn-primary fw-bold" Text="Submit" />
                                        <asp:Button ID="Button48" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-1 mb-1">
                                    <div class="col text-start">
                                        <asp:Label ID="Label107" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label384" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">Material Details</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label628" runat="server" ForeColor="Blue" Text="Mat. SLNo"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList58" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label352" runat="server" ForeColor="Blue" Text="Material Code &amp; Name"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="po_matcodecombo0" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox815" runat="server" BackColor="Red" ReadOnly="true" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label629" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label353" runat="server" ForeColor="Blue" Text="Mat Qty"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_matqty_text0" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label37" runat="server" ForeColor="Blue" Text="Disc"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_tradedisText1" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DISCOUNT_typeComboBox" runat="server" AutoPostBack="True">
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label354" runat="server" ForeColor="Blue" Text="Unit Rate"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_unitrateText" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label36" runat="server" ForeColor="Blue" Text="P &amp; F "></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_pfCombo1" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="PF_typeComboBox3" runat="server" AutoPostBack="True">
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label668" runat="server" ForeColor="Blue" Text="CGST %"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox833" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label356" runat="server" ForeColor="Blue" Text="Freight"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_frightText1" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="po_ed_typeComboBox2" runat="server" AutoPostBack="True">
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label669" runat="server" ForeColor="Blue" Text="SGST %"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox834" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label672" runat="server" ForeColor="Blue" Text="Total Wt. (Mt)" Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_frightText2" runat="server" Visible="False">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label670" ForeColor="Blue" runat="server" Text="IGST %"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox835" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label674" ForeColor="Blue" runat="server" Text="Cess %"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox837" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col text-end">
                                        <asp:Button ID="Button76" runat="server" Font-Bold="True" Text="Delete" CssClass="btn btn-danger" />
                                        <asp:Button ID="Button75" runat="server" Font-Bold="True" Text="Correction" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label671" ForeColor="Blue" runat="server" Text="Analytical Charge /Mt"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox836" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label42" runat="server" ForeColor="Blue" Text="Delivery Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="Delvdate7" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="Delvdate7" />
                                    </div>
                                    <div class="col-2 text-start g-0">
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label2" ForeColor="Blue" runat="server" Text="HSN Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox57" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        
                                    </div>
                                    <div class="col-2 text-start">
                                        
                                    </div>
                                    <div class="col-2 text-start g-0">
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label442" runat="server" ForeColor="Blue" Text="Material Details"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox733" runat="server" BorderColor="Lime" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button47" runat="server" Font-Bold="True" Text="ADD" CssClass="btn btn-primary" />
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col text-start">
                                        <asp:Panel ID="Panel12" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Style="text-align: center" Width="200%">
                                                <Columns>
                                                    <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat SLNo" />
                                                    <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                                    <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                                                    <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                    <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                                                    <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Rate" />
                                                    <asp:BoundField DataField="DISC_TYPE" HeaderText="Disc. Type" />
                                                    <asp:BoundField DataField="MAT_DISCOUNT" HeaderText="Discount" />
                                                    <asp:BoundField DataField="PF_TYPE" HeaderText="P&amp;F Type" />
                                                    <asp:BoundField DataField="MAT_PACK" HeaderText="P &amp; F" />
                                                    <asp:BoundField DataField="FREIGHT_TYPE" HeaderText="Freight Type" />
                                                    <asp:BoundField DataField="MAT_FREIGHT_PU" HeaderText="Freight" />
                                                    <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                    <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                    <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                    <asp:BoundField DataField="ANAL_TAX" HeaderText="Analytical Tax" />
                                                    <asp:BoundField DataField="Cess" HeaderText="Cess" />
                                                    <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                                                    <asp:BoundField DataField="MAT_DESC" HeaderText="Description" />
                                                    <asp:BoundField DataField="MAT_QTY_RCVD" HeaderText="Qty. Rcvd." />
                                                    <asp:BoundField DataField="TOTAL_WT" HeaderText="Total Wt." />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="row mt-1">
                                    <div class="col ms-1 text-start">
                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label43" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Disc Val"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox83" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label49" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Assble Val"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox18" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label44" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Freight"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox13" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label500" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="CGST"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox19" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label45" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="P&amp;F"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox14" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label673" runat="server" Font-Bold="True" Text="SGST"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox20" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label46" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Analytical Val"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox15" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label260" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="IGST"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox21" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Cess Val"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox10" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label281" runat="server" Font-Bold="True" ForeColor="Red" Style="text-align: left" Text="TOTAL VAL"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox22" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 3 Sale Order START=====--%>
                    <asp:View ID="View3" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label303" runat="server" Font-Bold="True" ForeColor="White" Text="MISC. SALES &amp; STOCK TRANSFER"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label426" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. / Ref. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox723" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label427" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox724" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label428" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox725" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label429" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox726" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label310" runat="server" Font-Bold="True" ForeColor="Blue" Text="Type Of Material"></asp:Label>
                                    </div>
                                    <div class="col-3 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox727" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>

                                </div>



                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label318" runat="server" Font-Bold="True" ForeColor="Blue" Text="Chapter Heading"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList14" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label311" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Group"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList11" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col text-start g-0">
                                        <asp:Label ID="Label438" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label333" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">Material Details</asp:Label>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label312" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="DropDownList12" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label630" runat="server" ForeColor="Red"></asp:Label>
                                    </div>

                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label319" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. Line No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox74" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label320" runat="server" Font-Bold="True" ForeColor="Blue" Text="VOCAB No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox75" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label335" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/U"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox79" runat="server" BackColor="Red" ReadOnly="True" ForeColor="White"></asp:TextBox>
                                    </div>

                                </div>



                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label322" runat="server" Font-Bold="True" ForeColor="Blue" Text="Ord. Qty"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox76" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label324" runat="server" Font-Bold="True" ForeColor="Blue" Text="P &amp; F "></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_pfCombo0" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-end g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList63" runat="server">
                                            <asp:ListItem>N/A</asp:ListItem>
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label323" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Rate"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox77" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label325" runat="server" Font-Bold="True" ForeColor="Blue" Text="Disc"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_tradedisText0" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList62" runat="server">
                                            <asp:ListItem>N/A</asp:ListItem>
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label330" runat="server" Font-Bold="True" ForeColor="Blue" Text="Terminal Tax"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox78" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:Label ID="Label327" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight"></asp:Label>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_frightText0" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList1" runat="server">
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">

                                    <div class="col-2 text-start ">
                                        <asp:Label ID="Label664" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST"></asp:Label>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="sgst_textbox" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:Label ID="Label665" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST"></asp:Label>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="cgst_textbox" runat="server">0.00</asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">

                                    <div class="col-2 text-start ">
                                        <asp:Label ID="Label666" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="igst_textbox" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:Label ID="Label667" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS"></asp:Label>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="cess_textbox" runat="server">0.00</asp:TextBox>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label336" runat="server" Font-Bold="True" ForeColor="Blue" Text="T.C.S."></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox80" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label331" runat="server" Font-Bold="True" ForeColor="Blue" Text="Delivery Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="Delvdate5" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="Delvdate7_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate5" />
                                    </div>


                                </div>


                                <div class="row align-items-center mb-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label431" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat. Details"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox728" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col text-end">
                                        <asp:Button ID="Button2" runat="server" Font-Bold="True" Font-Names="Times New Roman" Text="Save" CssClass="btn btn-success" />
                                        <asp:Button ID="Button58" runat="server" Font-Bold="True" Text="Submit" CssClass="btn btn-success" />
                                        <asp:Button ID="Button5" runat="server" Font-Bold="True" Font-Names="Times New Roman" Text="Cancel" CssClass="btn btn-danger" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 4 Purchase Order RAW MATERIAL(IMP) START=====--%>
                    <asp:View ID="View4" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label634" runat="server" Font-Bold="True" ForeColor="White" Text="RAW MATERIAL(IMP)"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label635" runat="server" Font-Bold="True" ForeColor="Blue" Text="P.O. / Ref. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox817" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label636" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox818" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label637" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label638" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox819" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label639" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox820" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-5 text-start">
                                    </div>
                                    <div class="col-7 text-end">
                                        <asp:Button ID="Button78" runat="server" class="btn btn-primary fw-bold" Text="Save" />
                                        <asp:Button ID="Button77" runat="server" class="btn btn-primary fw-bold" Text="Submit" />
                                        <asp:Button ID="Button79" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>

                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label641" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">Material Details</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label642" runat="server" ForeColor="Blue" Text="Mat. SLNo"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList60" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label643" runat="server" ForeColor="Blue" Text="Material Code &amp; Name"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="po_matcodecombo1" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox821" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label644" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label645" runat="server" ForeColor="Blue" Text="Mat Qty"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_matqty_text1" runat="server">0</asp:TextBox>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label647" runat="server" ForeColor="Blue" Text="Unit Rate"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_unitrateText0" runat="server">0</asp:TextBox>
                                    </div>

                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label651" runat="server" ForeColor="Blue" Text="Delivery Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="Delvdate8" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Delvdate8_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate8_CalendarExtender" TargetControlID="Delvdate8" />
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label14" runat="server" ForeColor="Blue" Text="HSN Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox58" runat="server"></asp:TextBox>
                                    </div>

                                </div>


                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label653" runat="server" ForeColor="Blue" Text="Material Details"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox822" runat="server" BorderColor="Lime" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button82" runat="server" Font-Bold="True" Text="Add" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button81" runat="server" Font-Bold="True" Text="Correction" CssClass="btn btn-info" />
                                        <asp:Button ID="Button80" runat="server" Font-Bold="True" Text="Delete" CssClass="btn btn-danger" />
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col text-start">
                                        <asp:GridView ID="GridView216" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Style="text-align: center" >
                                            <Columns>
                                                <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat SLNo" />
                                                <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                                <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                                                <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Rate" />
                                                <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="MAT_DESC" HeaderText="Description" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="row mt-1">
                                    <div class="col ms-1 text-start">


                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label655" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Accessible Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox824" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label662" runat="server" Font-Bold="True" ForeColor="Red" Style="text-align: left" Text="Total Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox831" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 5 Purchase Order OUTSOURCED ITEMS(IMP) START=====--%>
                    <asp:View ID="View5" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label48" runat="server" Font-Bold="True" ForeColor="White" Text="Purchase Order for OUTSOURCED ITEMS(IMP)"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label55" runat="server" Font-Bold="True" ForeColor="Blue" Text="P.O. / Ref. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox3" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label63" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox4" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label64" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label65" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox38" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label66" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox39" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-5 text-start">
                                    </div>
                                    <div class="col-7 text-end">
                                        <asp:Button ID="Button18" runat="server" class="btn btn-primary fw-bold" Text="Save" />
                                        <asp:Button ID="Button17" runat="server" class="btn btn-primary fw-bold" Text="Submit" />
                                        <asp:Button ID="Button19" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>

                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label114" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">Material Details</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label68" runat="server" Text="Mat. SLNo"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList7" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label69" runat="server" ForeColor="Blue" Text="Material Code &amp; Name"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox40" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox41" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label70" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label71" runat="server" ForeColor="Blue" Text="Mat Qty"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox43" runat="server">0</asp:TextBox>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label72" runat="server" ForeColor="Blue" Text="Unit Rate"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox52" runat="server">0</asp:TextBox>
                                    </div>

                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label73" runat="server" ForeColor="Blue" Text="Delivery Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox53" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate8_CalendarExtender" TargetControlID="TextBox53" />
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label21" runat="server" ForeColor="Blue" Text="HSN Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox61" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label74" runat="server" ForeColor="Blue" Text="Material Details"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox54" runat="server" BorderColor="Lime" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button22" runat="server" Font-Bold="True" Text="Add" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button21" runat="server" Font-Bold="True" Text="Correction" CssClass="btn btn-info" />
                                        <asp:Button ID="Button20" runat="server" Font-Bold="True" Text="Delete" CssClass="btn btn-danger" />
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col text-start">
                                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Style="text-align: center" >
                                            <Columns>
                                                <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat SLNo" />
                                                <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                                <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                                                <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Rate" />
                                                <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="MAT_DESC" HeaderText="Description" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="row mt-1">
                                    <div class="col ms-1 text-start">


                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label75" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Accessible Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox55" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label76" runat="server" Font-Bold="True" ForeColor="Red" Style="text-align: left" Text="Total Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox56" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 6 Purchase Order STORE ITEMS(IMP) START=====--%>
                    <asp:View ID="View6" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="White" Text="Purchase Order for STORE MATERIAL(IMP)"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="Blue" Text="P.O. / Ref. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox16" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox17" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label11" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox23" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox24" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-5 text-start">
                                    </div>
                                    <div class="col-7 text-end">
                                        <asp:Button ID="Button6" runat="server" class="btn btn-primary fw-bold" Text="Save" />
                                        <asp:Button ID="Button4" runat="server" class="btn btn-primary fw-bold" Text="Submit" />
                                        <asp:Button ID="Button7" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>

                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label113" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">Material Details</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label15" runat="server" Text="Mat. SLNo"></asp:Label>
                                    </div>
                                    <div class="col-4 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label16" runat="server" ForeColor="Blue" Text="Material Code &amp; Name"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox25" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox26" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label17" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label18" runat="server" ForeColor="Blue" Text="Mat Qty"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox27" runat="server">0</asp:TextBox>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label19" runat="server" ForeColor="Blue" Text="Unit Rate"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox28" runat="server">0</asp:TextBox>
                                    </div>

                                </div>
                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label20" runat="server" ForeColor="Blue" Text="Delivery Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox29" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox29_CalendarExtender" TargetControlID="TextBox29" />
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label22" runat="server" ForeColor="Blue" Text="HSN Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox66" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label121" runat="server" ForeColor="Blue" Text="Material Details"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox30" runat="server" BorderColor="Lime" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button10" runat="server" Font-Bold="True" Text="Add" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button9" runat="server" Font-Bold="True" Text="Correction" CssClass="btn btn-info" />
                                        <asp:Button ID="Button8" runat="server" Font-Bold="True" Text="Delete" CssClass="btn btn-danger" />
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col text-start">
                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Style="text-align: center" >
                                            <Columns>
                                                <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat SLNo" />
                                                <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                                <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                                                <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Rate" />
                                                <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                                                <asp:BoundField DataField="MAT_DESC" HeaderText="Description" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="row mt-1">
                                    <div class="col ms-1 text-start">


                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label122" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Accessible Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox31" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label123" runat="server" Font-Bold="True" ForeColor="Red" Style="text-align: left" Text="Total Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox32" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 7 Rate Contract(Service) START=====--%>
                    <asp:View ID="View7" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label398" runat="server" Font-Bold="True" ForeColor="White" Text="NEW WORK ORDER"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label412" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label413" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label441" runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label415" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox128" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label416" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox129" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label423" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">ORDER DETAILS</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label425" runat="server" ForeColor="Blue" Text="Order Type"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox722" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label440" runat="server" ForeColor="Blue" Text="Taxable Service"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList46" runat="server">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label399" runat="server" ForeColor="Blue" Text="Location"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox641" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label400" runat="server" ForeColor="Blue" Text="Due Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox651" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox651_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" runat="server" BehaviorID="TextBox651_CalendarExtender" TargetControlID="TextBox651" />
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label401" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox661" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox661_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox661_CalendarExtender" TargetControlID="TextBox661" />
                                    </div>

                                </div>
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label273" runat="server" ForeColor="Blue" Text="Tolerance %"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox671" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label23" runat="server" ForeColor="Blue" Text="HSN Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox67" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label663" runat="server" ForeColor="Blue" Text="Work Type"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList61" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Service Work</asp:ListItem>
                                            <asp:ListItem>Material Supply</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label409" runat="server" ForeColor="Blue" Text="Description of Job"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox621" runat="server" EnableViewState="False" TabIndex="14" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button53" runat="server" class="btn btn-primary fw-bold" Text="Save" />
                                        <asp:Button ID="Button59" runat="server" class="btn btn-primary fw-bold" Text="Submit" />
                                        <asp:Button ID="Button54" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                        <asp:Button ID="Button1" runat="server" class="btn btn-danger fw-bold" Text="Close" />
                                    </div>
                                </div>

                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label424" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">WORK DETAILS</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label403" runat="server" ForeColor="Blue" Text="Ordered Qty"></asp:Label>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label404" runat="server" ForeColor="Blue" Text="A/U"></asp:Label>

                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label405" runat="server" ForeColor="Blue" Text="Rate/Unit"></asp:Label>

                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label406" runat="server" ForeColor="Blue" Text="Mat. Cost"></asp:Label>

                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label407" runat="server" ForeColor="Blue" Text="Discount %"></asp:Label>

                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label255" runat="server" ForeColor="Blue" Text="SGST %"></asp:Label>

                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label675" runat="server" ForeColor="Blue" Text="CGST %"></asp:Label>

                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label676" runat="server" ForeColor="Blue" Text="IGST %"></asp:Label>

                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:Label ID="Label677" runat="server" ForeColor="Blue" Text="CESS %"></asp:Label>
                                    </div>
                                    <div class="col-1 text-end g-1">
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox561" runat="server" TabIndex="15"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox571" runat="server" TabIndex="16"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox581" runat="server" TabIndex="17"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox591" runat="server" TabIndex="18"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox601" runat="server" TabIndex="19"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox611" runat="server" TabIndex="20"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox838" runat="server" TabIndex="21"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox839" runat="server" TabIndex="22"></asp:TextBox>
                                    </div>
                                    <div class="col text-start g-1">
                                        <asp:TextBox class="form-control" ID="TextBox840" runat="server" TabIndex="23"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-end g-1">
                                        <asp:Button ID="Button50" runat="server" class="btn btn-primary fw-bold" Text="Add" />
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col text-start" style="overflow: scroll">
                                        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="200%">
                                            <Columns>
                                                <asp:BoundField DataField="W_SLNO" HeaderText="SLNo" />
                                                <asp:BoundField DataField="TAX_TYPE" HeaderText="Taxable Service" />
                                                <asp:BoundField DataField="W_NAME" HeaderText="Desc. Of Job" />
                                                <asp:BoundField DataField="W_QTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="W_AU" HeaderText="A/U" />
                                                <asp:BoundField DataField="W_UNIT_PRICE" HeaderText="Unit Price" />
                                                <asp:BoundField DataField="W_MATERIAL_COST" HeaderText="Mat. Charge" />
                                                <asp:BoundField DataField="W_AREA" HeaderText="Location" />
                                                <asp:BoundField DataField="W_START_DATE" HeaderText="Strat Date" />
                                                <asp:BoundField DataField="W_END_DATE" HeaderText="End Date" />
                                                <asp:BoundField DataField="W_TOLERANCE" HeaderText="Tolerance" />
                                                <asp:BoundField DataField="W_DISCOUNT" HeaderText="Discount" />
                                                <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                <asp:BoundField DataField="CESS" HeaderText="CESS" />
                                                <asp:BoundField DataField="wo_type" HeaderText="Work Type" />
                                                <asp:BoundField DataField="t_value" HeaderText="Total" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="row mt-1">
                                    <div class="col ms-1 text-start">

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label274" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Accessible Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox681" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label275" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="SGST"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox691" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label276" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="CGST"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox701" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="IGST"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox11" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="CESS"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox12" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label277" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Round Off"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox711" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-2 text-start">
                                            </div>
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-2 text-end">
                                                <asp:Label ID="Label278" runat="server" Font-Bold="True" ForeColor="Red" Style="text-align: left" Text="Total Value"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox721" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 8 Rate Contract Initial Screen START=====--%>
                    <asp:View ID="View8" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label309" runat="server" Font-Bold="True" ForeColor="White" Text="RATE CONTRACT"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label433" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox729" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label434" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox730" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label679" runat="server" Text="Label"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label435" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox731" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label436" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox732" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label437" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center">RATE CONTRACT DETAILS</asp:Label>
                                    </div>
                                </div>


                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label307" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amount"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox84" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox85" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox85_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" runat="server" BehaviorID="TextBox85_CalendarExtender" TargetControlID="TextBox85" />
                                    </div>

                                </div>


                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                    </div>
                                    <div class="col-5 text-start">
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button56" runat="server" class="btn btn-primary fw-bold" Text="Save" />
                                        <asp:Button ID="Button57" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 9 FINISHED PRODUCTS START=====--%>
                    <asp:View ID="View9" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label246" runat="server" Font-Bold="True" ForeColor="White" Text="FINISHED PRODUCTS"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label359" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. / Ref. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox86" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label360" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox87" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label362" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox90" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label363" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox88" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label370" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">MATERIAL DETAILS</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label300" runat="server" ForeColor="Blue" Text="System Line No" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox64" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label78" runat="server" ForeColor="Blue" Text="S.O. Line No" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox70" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label289" runat="server" ForeColor="Blue" Text="Ord. Unit" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList8" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>PCS</asp:ListItem>
                                            <asp:ListItem>MTS</asp:ListItem>
                                            <asp:ListItem>Activity</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label432" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label287" runat="server" ForeColor="Blue" Text="VOCAB No" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox59" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label293" runat="server" ForeColor="Blue" Text="Disc" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_tradedisText" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList48" runat="server">
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label288" runat="server" ForeColor="Blue" Text="Ord. Qty" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox60" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label292" runat="server" ForeColor="Blue" Text="P &amp; F " Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_pfCombo" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList47" runat="server">
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label291" runat="server" Text="Unit Rate" ForeColor="Blue" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox62" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label41" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_frightText" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList9" runat="server">
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label52" runat="server" Font-Bold="True" ForeColor="Blue" Text="TCS (%)"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="po_taxTextBox" runat="server">0.00</asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label79" runat="server" Font-Bold="True" ForeColor="Blue" Text="Set Name"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox71" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label295" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST (%)"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="sgstPercentage" runat="server">0.00</asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CGST (%)"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="cgstPercentage" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="igstPercentage" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cess"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox9" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label301" runat="server" Font-Bold="True" ForeColor="Blue" Text="Terminal Tax"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox65" runat="server">0.00</asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label298" runat="server" Font-Bold="True" ForeColor="Blue" Text="Delivery Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="Delvdate" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Delvdate_CalendarExtender" runat="server" BehaviorID="Delvdate_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate" />
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label67" runat="server" Font-Bold="True" ForeColor="Blue" Text="HSN Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox68" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        
                                    </div>
                                    <div class="col-2 text-start">
                                        
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                    </div>
                                    <div class="col-5 text-start">
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button39" runat="server" class="btn btn-primary fw-bold" Text="Add Item" />
                                        <asp:Button ID="Button55" runat="server" class="btn btn-primary fw-bold" Text="Submit" />
                                        <asp:Button ID="Button40" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>


                                <div class="row align-items-center mt-1">
                                    <div class="col text-center g-0">
                                        <asp:Panel ID="Panel5" runat="server" Visible="False">
                                            <asp:Label ID="Label302" runat="server" BackColor="Blue" Font-Bold="True" ForeColor="White" Style="text-align: center" Text="INDIVIDUAL ITEM DETAILS AS PER SALE ORDER" Width="100%"></asp:Label>

                                            <div class="row align-items-center mt-1 ms-1">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label32" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Code"></asp:Label>
                                                </div>
                                                <div class="col-4 text-start">
                                                    <asp:TextBox class="form-control" ID="po_matcodecombo" runat="server" BorderStyle="Double" Visible="False"></asp:TextBox>
                                                    <asp:TextBox class="form-control" ID="TextBox1" runat="server" BorderStyle="Double" Visible="False"></asp:TextBox>
                                                    <asp:TextBox class="form-control" ID="TextBox2" runat="server" BorderStyle="Double" Visible="False"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="row align-items-center mt-1 ms-1">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label631" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Accounting Unit"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label632" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Ord. Unit"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label633" runat="server" ForeColor="Blue" Font-Bold="True" Text="Unit Weight (Kg)"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row align-items-center mt-1 ms-1">
                                                <div class="col-2 text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox816" runat="server" BorderStyle="Double" ReadOnly="True" ForeColor="Red"></asp:TextBox>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:TextBox class="form-control" ID="po_matqty_text" runat="server" BorderStyle="Double" ReadOnly="True" ForeColor="Red">0</asp:TextBox>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:TextBox class="form-control" ID="po_unitWEIGHTText" runat="server" BorderStyle="Double" ForeColor="Red">0.000</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row align-items-center justify-content-center ms-1">
                                                <div class="col text-start">
                                                    <asp:Label ID="Label299" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Description"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row align-items-center ms-1">
                                                <div class="col-6 text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox63" runat="server" BorderStyle="Double" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div class="col text-end">
                                                    <asp:Button ID="Button3" runat="server" BorderColor="Lime" CssClass="btn btn-primary" Font-Bold="True" Text="Add" />
                                                    <asp:Button ID="Button41" runat="server" BorderColor="Lime" CssClass="btn btn-primary" Text="Calculate" Visible="False" />
                                                    <asp:Button ID="Button42" runat="server" BorderColor="Lime" CssClass="btn btn-success" Text="Save and Go to another Vocab" />
                                                </div>
                                            </div>
                                            <div class="row align-items-center m-1">
                                                <div class="col text-center" style="overflow: scroll">
                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered border-2 table-responsive text-center" ShowHeaderWhenEmpty="True" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="SlNo" HeaderText="SlNo"></asp:BoundField>
                                                            <asp:BoundField DataField="Mat Code" HeaderText="Item Code"></asp:BoundField>
                                                            <asp:BoundField DataField="Mat Name" HeaderText="Item Name" />
                                                            <asp:BoundField DataField="A/U" HeaderText="A/U"></asp:BoundField>
                                                            <asp:BoundField DataField="Qty" HeaderText="Item Unit Qty "></asp:BoundField>
                                                            <asp:BoundField DataField="Unit Weight" HeaderText="Unit Weight"></asp:BoundField>
                                                            <asp:BoundField DataField="Mat Ord. Qty" HeaderText="Item Ord. Qty"></asp:BoundField>
                                                            <asp:BoundField DataField="ORD_QTY_MT" HeaderText="Ord Qty (Mt)"></asp:BoundField>
                                                            <asp:BoundField DataField="Unit Price" HeaderText="Unit Price(Mt)"></asp:BoundField>
                                                            <asp:BoundField DataField="Mat Desc" HeaderText="Item Description"></asp:BoundField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=====VIEW 10 Sale Order(Outsourced Items) START=====--%>
                    <asp:View ID="View10" runat="server">
                        <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                            <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                                <div class="row align-items-center justify-content-center" style="background-color: Blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" Text="OUTSOURCED FINISHED PRODUCTS"></asp:Label>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. / Ref. No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="soNoOutsourced" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="soDateOutsourced" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label680" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox5" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name"></asp:Label>
                                    </div>
                                    <div class="col-7 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center mt-1" style="background-color: blue">
                                    <div class="col text-center">
                                        <asp:Label ID="Label27" runat="server" Font-Bold="True" ForeColor="White" Style="text-align: center" Width="100%">MATERIAL DETAILS</asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label28" runat="server" Text="S.O. Line No" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox7" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label30" runat="server" Text="Ord. Unit" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList3" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>PCS</asp:ListItem>
                                            <asp:ListItem>MTS</asp:ListItem>
                                            <asp:ListItem>SET</asp:ListItem>
                                            <asp:ListItem>Activity</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label33" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label34" runat="server" Text="VOCAB No" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox8" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label35" runat="server" ForeColor="Blue" Text="Disc" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox33" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList4" runat="server">
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label38" runat="server" Text="Ord. Qty" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox34" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label39" runat="server" ForeColor="Blue" Text="P &amp; F " Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox35" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList5" runat="server">
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label40" runat="server" Text="Unit Rate" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox36" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label47" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox37" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList6" runat="server">
                                            <asp:ListItem>PERCENTAGE</asp:ListItem>
                                            <asp:ListItem>PER UNIT</asp:ListItem>
                                            <asp:ListItem>PER MT</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label106" runat="server" Font-Bold="True" ForeColor="Blue" Text="TCS (%)"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox89" runat="server">0.00</asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                    </div>
                                    <div class="col-2 text-start">
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label50" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST (%)"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="sgstPercentageOutsourced" runat="server">0.00</asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label51" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CGST (%)"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="cgstPercentageOutsourced" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>
                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="igstPercentageOutsourced" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label54" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cess"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox42" runat="server">0.00</asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label105" runat="server" Font-Bold="True" ForeColor="Blue" Text="Terminal Tax"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox82" runat="server">0.00</asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label56" runat="server" Font-Bold="True" ForeColor="Blue" Text="Delivery Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox44" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="Delvdate_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate" />
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label77" runat="server" Font-Bold="True" ForeColor="Blue" Text="HSN Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox69" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="col-2 text-start">
                                        
                                    </div>
                                    <div class="col-2 text-start">
                                        
                                    </div>
                                    <div class="col text-start">
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                    </div>
                                    <div class="col-5 text-start">
                                    </div>
                                    <div class="col text-end ">
                                        <asp:Button ID="Button11" runat="server" class="btn btn-primary fw-bold" Text="Add Item" />
                                        <asp:Button ID="Button13" runat="server" class="btn btn-primary fw-bold" Text="Submit" />
                                        <asp:Button ID="Button12" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>


                                <div class="row align-items-center mt-1">
                                    <div class="col text-center g-0">
                                        <asp:Panel ID="Panel13" runat="server" Visible="False">
                                            <asp:Label ID="Label57" runat="server" BackColor="Blue" Font-Bold="True" ForeColor="White" Style="text-align: center" Text="INDIVIDUAL ITEM DETAILS AS PER SALE ORDER" Width="100%"></asp:Label>

                                            <div class="row align-items-center mt-1 ms-1">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label58" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Code"></asp:Label>
                                                </div>
                                                <div class="col-4 text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox45" runat="server" BorderStyle="Double" Visible="False"></asp:TextBox>
                                                    <asp:TextBox class="form-control" ID="TextBox46" runat="server" BorderStyle="Double" Visible="False"></asp:TextBox>
                                                    <asp:TextBox class="form-control" ID="TextBox47" runat="server" BorderStyle="Double" Visible="False"></asp:TextBox>
                                                </div>

                                            </div>

                                            <div class="row align-items-center mt-1 ms-1">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label59" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Accounting Unit"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label60" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Ord. Unit"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label61" runat="server" ForeColor="Blue" Font-Bold="True" Text="Unit Weight (Kg)"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row align-items-center mt-1 ms-1">
                                                <div class="col-2 text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox48" runat="server" BorderStyle="Double" ReadOnly="True" ForeColor="Red"></asp:TextBox>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox49" runat="server" BorderStyle="Double" ReadOnly="True" ForeColor="Red">0</asp:TextBox>
                                                </div>
                                                <div class="col-2 text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox50" runat="server" BorderStyle="Double" ForeColor="Red">0.000</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row align-items-center justify-content-center ms-1">
                                                <div class="col text-start">
                                                    <asp:Label ID="Label62" runat="server" ForeColor="Blue" Font-Bold="True" Text="Item Description"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row align-items-center ms-1">
                                                <div class="col-6 text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox51" runat="server" BorderStyle="Double" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div class="col text-end">
                                                    <asp:Button ID="Button14" runat="server" BorderColor="Lime" CssClass="btn btn-primary" Font-Bold="True" Text="Add" />
                                                    <asp:Button ID="Button15" runat="server" BorderColor="Lime" CssClass="btn btn-primary" Text="Calculate" Visible="False" />
                                                    <asp:Button ID="Button16" runat="server" BorderColor="Lime" CssClass="btn btn-success" Text="Save and Go to another Vocab" />
                                                </div>
                                            </div>
                                            <div class="row align-items-center m-1">
                                                <div class="col text-center" style="overflow: scroll">
                                                    <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered border-2 table-responsive text-center" ShowHeaderWhenEmpty="True" Width="100%">
                                                        <Columns>

                                                            <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat SLNo" />
                                                            <asp:BoundField DataField="MAT_CODE" HeaderText="Mat Code" />
                                                            <asp:BoundField DataField="MAT_NAME" HeaderText="Mat Name" />
                                                            <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                            <asp:BoundField DataField="MAT_QTY" HeaderText="Qty" />
                                                            <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Rate" />
                                                            <asp:BoundField DataField="DISC_TYPE" HeaderText="Disc. Type" />
                                                            <asp:BoundField DataField="MAT_DISCOUNT" HeaderText="Discount" />
                                                            <asp:BoundField DataField="PF_TYPE" HeaderText="P&amp;F Type" />
                                                            <asp:BoundField DataField="MAT_PACK" HeaderText="P &amp; F" />
                                                            <asp:BoundField DataField="FREIGHT_TYPE" HeaderText="Freight Type" />
                                                            <asp:BoundField DataField="MAT_FREIGHT_PU" HeaderText="Freight" />
                                                            <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                            <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                            <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                            <asp:BoundField DataField="ANAL_TAX" HeaderText="Analytical Tax" />
                                                            <asp:BoundField DataField="Cess" HeaderText="Cess" />
                                                            <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                                                            <asp:BoundField DataField="MAT_DESC" HeaderText="Description" />
                                                            <asp:BoundField DataField="MAT_QTY_RCVD" HeaderText="Qty. Rcvd." />
                                                            <asp:BoundField DataField="TOTAL_WT" HeaderText="Total Wt.(MT)" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
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













































     <%--==================OUTSOURCE ITEM SALE ORDER START===========================--%>


    <%--<center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
            
           

            
           
            
            <asp:Panel ID="Panel14" runat="server" BorderColor="Blue" BorderStyle="Ridge" BorderWidth="2px" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="1000px"  Visible="False">
                <asp:Label ID="Label77" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="OUTSOURCHED FINISHED PRODUCTS SALE" Width="100%"  Font-Size="Large"></asp:Label>
        <br />
        <br />
                <asp:Label ID="Label78" runat="server" Font-Bold="True" ForeColor="Blue" Text="S.O. / Ref. No" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox57" runat="server" BackColor="Red" ForeColor="White" ></asp:TextBox>
                <asp:Label ID="Label79" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox58" runat="server" BackColor="Red" ForeColor="White" ></asp:TextBox>
        <br />
                <asp:Label ID="Label80" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Code" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox61" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" ></asp:TextBox>
                <asp:Label ID="Label81" runat="server" Font-Bold="True" ForeColor="Blue" Text="Name" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox66" runat="server" BackColor="#6600FF" ForeColor="White" ReadOnly="True" ></asp:TextBox>
        <br />
                <br />
                <asp:Label ID="Label82" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Width="100%" >MATERIAL DETAILS</asp:Label>
        <br />
        <br />
       
        <br />
                <asp:Label ID="Label83" runat="server" Text="S.O. Line No"  Font-Bold="True"></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox67" runat="server" ></asp:TextBox>
                 <asp:Label ID="Label84" runat="server" Text="Ord. Unit"  Font-Bold="True"></asp:Label>
                <asp:DropDownList class="form-select" ID="DropDownList10" runat="server"  AutoPostBack="True">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>PCS</asp:ListItem>
                    <asp:ListItem>MTS</asp:ListItem>
                    <asp:ListItem>Activity</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label85" runat="server" ForeColor="Red"></asp:Label>
        <br />
                <asp:Label ID="Label86" runat="server" Text="VOCAB No"  Font-Bold="True"></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox68" runat="server" ></asp:TextBox>
                 <asp:Label ID="Label87" runat="server"  ForeColor="Blue" Text="Disc"  Font-Bold="True"></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox69" runat="server" >0.00</asp:TextBox>
                <asp:DropDownList class="form-select" ID="DropDownList13" runat="server" >
                    <asp:ListItem>PERCENTAGE</asp:ListItem>
                    <asp:ListItem>PER UNIT</asp:ListItem>
                    <asp:ListItem>PER MT</asp:ListItem>
                </asp:DropDownList>
        
         
       
         <br />
                <asp:Label ID="Label88" runat="server" Text="Ord. Qty"  Font-Bold="True"></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox70" runat="server" ></asp:TextBox>
                <asp:Label ID="Label89" runat="server"  ForeColor="Blue" Text="P &amp; F "  Font-Bold="True"></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox71" runat="server" >0.00</asp:TextBox>
                <asp:DropDownList class="form-select" ID="DropDownList15" runat="server" >
                    <asp:ListItem>PERCENTAGE</asp:ListItem>
                    <asp:ListItem>PER UNIT</asp:ListItem>
                    <asp:ListItem>PER MT</asp:ListItem>
                </asp:DropDownList>
        <br />
                <asp:Label ID="Label90" runat="server" Text="Unit Rate"  Font-Bold="True"></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox72" runat="server" ></asp:TextBox>
                



                
                <asp:Label ID="Label91" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox73" runat="server" >0.00</asp:TextBox>
                <asp:DropDownList class="form-select" ID="DropDownList16" runat="server" >
                    <asp:ListItem>PERCENTAGE</asp:ListItem>
                    <asp:ListItem>PER UNIT</asp:ListItem>
                    <asp:ListItem>PER MT</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label92" runat="server" Font-Bold="True" ForeColor="Blue" Text="TCS" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox81" runat="server" >0.00</asp:TextBox>
                %<br />
                <asp:Label ID="Label93" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST" ></asp:Label>
                <asp:TextBox class="form-control" ID="sgstPercentageOutSourcedSale" runat="server" >0.00</asp:TextBox>
                %  <asp:Label ID="Label94" runat="server" Font-Bold="False" Font-Names="Times New Roman"  ForeColor="Blue" Text="CGST" ></asp:Label>
                <asp:TextBox class="form-control" ID="cgstPercentageOutSourcedSale" runat="server" >0.00</asp:TextBox>
                %<br />
                   <asp:Label ID="Label95" runat="server" Font-Bold="False" ForeColor="Blue" Text="IGST" ></asp:Label>
                   <asp:TextBox class="form-control" ID="igstPercentageOutSourcedSale" runat="server" >0.00</asp:TextBox>
                %
                <asp:Label ID="Label96" runat="server" Font-Bold="True" Text="Cess" ></asp:Label>
                <asp:TextBox class="form-control" ID="cessPercentageOutSourcedSale" runat="server" >0.00</asp:TextBox>
                %<br />
                          <asp:Label ID="Label97" runat="server" Font-Bold="True" Text="Terminal Tax" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox93" runat="server" >0.00</asp:TextBox>
                % <asp:Label ID="Label98" runat="server" Font-Bold="True" ForeColor="Blue" Text="Delivery Date" ></asp:Label>
                <asp:TextBox class="form-control" ID="TextBox94" runat="server" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" BehaviorID="Delvdate_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="Delvdate" />
               <br /> 
                          <br /><asp:Panel ID="Panel15" runat="server" Height="30px" style="text-align: center">
                    <asp:Button ID="Button23" runat="server" CssClass="bottomstyle" Font-Bold="True" Text="ADD ITEM"  />
                    <asp:Button ID="Button24" runat="server" CssClass="bottomstyle" Font-Bold="True" Text="CANCEL"  />
                    <asp:Button ID="Button25" runat="server" CssClass="bottomstyle" Font-Bold="True" Text="SUBMIT"  />
                </asp:Panel>
                <asp:Panel ID="Panel16" runat="server" Visible="False" Width="100%">
                    <asp:Label ID="Label99" runat="server" BackColor="Blue" Font-Bold="True"  ForeColor="White" style="text-align: center" Text="INDIVIDUAL ITEM DETAILS AS PER SALE ORDER" Width="100%" ></asp:Label>
            <br />
                    <div style="height: 205px">
                        <div runat="server" style="float :left; width: 518px; ">
                            <div style="background-color: #4686F0; width: 498px; color: #FFFFFF;">
                                <asp:Label ID="Label100" runat="server" BackColor="#4686F0" Font-Bold="True"  ForeColor="White" Text="Item Code" ></asp:Label>
                            </div>
                            
                            <asp:TextBox class="form-control" ID="TextBox95" runat="server" BorderStyle="Double"  Width="500px" Visible="False"></asp:TextBox>
                                         
                              <asp:TextBox class="form-control" ID="TextBox96" runat="server" BorderStyle="Double"  Width="500px" Visible="False"></asp:TextBox>
                                                                 
                            <asp:TextBox class="form-control" ID="TextBox97" runat="server" BorderStyle="Double"  Width="500px" Visible="False"></asp:TextBox>
                             <br />
                            <asp:Label ID="Label101" runat="server" Text="Item Accounting Unit" ></asp:Label>
                            <asp:Label ID="Label102" runat="server" Text="Item Ord. Unit " ></asp:Label>
                            <asp:Label ID="Label103" runat="server" Text="Unit Weight (Kg)" ></asp:Label>
                    <br />
                            <asp:TextBox class="form-control" ID="TextBox98" runat="server" BorderStyle="Double" ReadOnly="True" ForeColor="Red" ></asp:TextBox>
                            <asp:TextBox class="form-control" ID="TextBox99" runat="server" BorderStyle="Double" ReadOnly="True" ForeColor="Red" >0</asp:TextBox>
                            <asp:TextBox class="form-control" ID="TextBox100" runat="server" BorderStyle="Double" ForeColor="Red" >0.000</asp:TextBox>
            <br />
                            <asp:Label ID="Label104" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" Text="Item Description" Width="498px"></asp:Label>
            <br />
                            <asp:TextBox class="form-control" ID="TextBox101" runat="server" BorderStyle="Double" CssClass="MATtextboxclass" Height="82px" TextMode="MultiLine" Width="498px"></asp:TextBox>
                        </div>
                        <div runat="server" style="float :right; ">
                <br />
                <br />
                <br />
                <br />
                <br />
                            <asp:Button ID="Button26" runat="server" BorderColor="Lime" CssClass="bottomstyle" Font-Bold="True" Text="ADD" />
                            <asp:Button ID="Button27" runat="server" BorderColor="Lime" CssClass="bottomstyle"  Text="CALCULATE" Visible="False" />
                            <asp:Button ID="Button28" runat="server" BorderColor="Lime" CssClass="bottomstyle"  Text="SAVE AND GOTO ANOTHER VOCAB" />
                        </div>
                    </div>
                    <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False"  HorizontalAlign="Center" ShowHeaderWhenEmpty="True" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="SlNo" HeaderText="SlNo">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Code" HeaderText="Item Code">
                            <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Name" HeaderText="Item Name" />
                            <asp:BoundField DataField="A/U" HeaderText="A/U">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Qty" HeaderText="Item Unit Qty ">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit Weight" HeaderText="Unit Weight">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Ord. Qty" HeaderText="Item Ord. Qty">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ORD_QTY_MT" HeaderText="Ord Qty (Mt)" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit Price" HeaderText="Unit Price(Mt)">
                            <ItemStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Mat Desc" HeaderText="Item Description">
                            <ItemStyle  />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </asp:Panel>            
            
            
             
            
            
            
            
            
             </div> 
    </center>--%>




</asp:Content>
