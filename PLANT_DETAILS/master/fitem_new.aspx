<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="fitem_new.aspx.vb" Inherits="PLANT_DETAILS.fitem_new" %>

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
            $("[id$=TextBox1]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/FIN_GOODS_NEW")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    ITEM_NAME: item.split('^')[1],
                                    ITEM_AU: item.split('^')[2],
                                    ITEM_DRAW: item.split('^')[3],
                                    ITEM_TYPE: item.split('^')[4],
                                    ITEM_CHPT: item.split('^')[5],
                                    ITEM_STATUS: item.split('^')[6],
                                    ITEM_WEIGHT: item.split('^')[7]
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
                    $("[id$=TextBox2]").val(i.item.ITEM_NAME);
                    $("[id$=TextBox3]").val(i.item.ITEM_DRAW);
                    $("[id$=DropDownList2]").val(i.item.ITEM_TYPE);
                    $("[id$=DropDownList3]").val(i.item.ITEM_CHPT);
                    $("[id$=DropDownList1]").val(i.item.ITEM_AU);
                    $("[id$=DropDownList4]").val(i.item.ITEM_STATUS);
                    $("[id$=TextBox4]").val(i.item.ITEM_WEIGHT);
                },
                minLength: 1
            });
        });
    </script>



    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Add/Update Finished Goods" Font-Bold="True" Font-Size="Larger"></asp:Label>
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
                                        <div class="col-3 text-end">
                                            <asp:Label ID="Label4" runat="server" Text="Item Code"></asp:Label>
                                        </div>
                                        <div class="col text-start">
                                            <asp:TextBox class="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-3 text-end">
                                            <asp:Label ID="Label5" runat="server" Text="Item Name"></asp:Label>
                                        </div>
                                        <div class="col text-start">
                                            <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-3 text-end">
                                            <asp:Label ID="Label6" runat="server" Text="Item Drawing"></asp:Label>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox3" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label12" runat="server" Text="Item Weight"></asp:Label>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox4" runat="server">0.000</asp:TextBox>
                                        </div>
                                        <div class="col text-start g-0">
                                            Kg.
                                        </div>
                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-3 text-end">
                                            <asp:Label ID="Label7" runat="server" Text="Item AU."></asp:Label>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:DropDownList class="form-select" ID="DropDownList1" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>PCS</asp:ListItem>
                                                <asp:ListItem>MTS</asp:ListItem>
                                                <asp:ListItem>Activity</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label8" runat="server" Text="Item Group"></asp:Label>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:DropDownList class="form-select" ID="DropDownList2" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-3 text-end">
                                            <asp:Label ID="Label9" runat="server" Text="Chapter Heading"></asp:Label>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:DropDownList class="form-select" ID="DropDownList3" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label10" runat="server" Text="Item Status"></asp:Label>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:DropDownList class="form-select" ID="DropDownList4" runat="server">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>ACTIVE</asp:ListItem>
                                                <asp:ListItem>CLOSE</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-3 text-end">
                                        </div>
                                        <div class="col-3 text-start">
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True">Password : </asp:Label>
                                        </div>
                                        <div class="col-3 text-start">
                                            <asp:TextBox class="form-control" ID="TextBox5" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row align-items-center mt-3">
                                        <div class="col-8 text-end">
                                        </div>
                                        <div class="col text-start">
                                            <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-primary fw-bold" Font-Size="Small" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                            <asp:Button ID="Button2" runat="server" class="btn btn-danger fw-bold" Text="Cancel" Font-Size="Small" />
                                            <asp:Button ID="Button3" runat="server" class="btn btn-danger fw-bold" Text="Close" Font-Size="Small" />
                                        </div>

                                    </div>

                                    <div class="row align-items-center mt-2">
                                        <div class="col text-start">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
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
