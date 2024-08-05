<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="mat_rej.aspx.vb" Inherits="PLANT_DETAILS.mat_rej" %>

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
                        url: '<%=ResolveUrl("~/Service.asmx/rej_mat")%>',
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

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Store Rejected Material Return" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10">
                <div class="row align-items-center">
                    <div class="col-4 text-end">
                        <asp:Label ID="Label2" runat="server" Text="CRR No" Font-Bold="True"></asp:Label>
                    </div>
                    <div class="col-4 text-start">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-2 text-start">
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"></asp:Button>
                    </div>
                </div>
                <div class="row align-items-center">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </div>
                <div class="row align-items-center mt-3">
                    <div class="col">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="CRR No.">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Button1" ForeColor="BLUE" runat="server" Text='<%#Eval("CRR_NO")%>' CommandName='<%#Eval("CRR_NO1")%>' OnClick="prv" />
                                    </ItemTemplate>
                                    <ItemStyle ForeColor="Blue" Width="100px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="PO_NO" HeaderText="PO No" />
                                <asp:BoundField HeaderText="Mat. Code" DataField="MAT_CODE" />
                                <asp:BoundField HeaderText="Mat. Name" DataField="MAT_NAME" />
                                <asp:BoundField HeaderText="Mat SlNo" DataField="MAT_SLNO" />
                                <asp:BoundField HeaderText="Chln. Qty" DataField="MAT_CHALAN_QTY" />
                                <asp:BoundField HeaderText="Rcvd. Qty" DataField="MAT_RCD_QTY" />
                                <asp:BoundField HeaderText="Qty. Acptd" DataField="ACCPT_QTY" />
                                <asp:BoundField HeaderText="Rej. Qty" DataField="MAT_REJ_QTY" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
