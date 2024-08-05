<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="rm_rej_print.aspx.vb" Inherits="PLANT_DETAILS.rm_rej_print" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox1]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/rm_rej_prt")%>',
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
            <asp:Label ID="Label7" runat="server" Text="Rejected Raw Material Print" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10">
                <div class="row align-items-center">
                    <div class="col-4 text-end">
                        <asp:Label ID="Label1" runat="server" Text="CRR No" Font-Bold="True"></asp:Label>
                    </div>
                    <div class="col-4 text-start">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-3 text-start">
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"></asp:Button>
                        <asp:Button ID="Button2" runat="server" Text="Print" CssClass="btn btn-success"></asp:Button>
                    </div>

                </div>



            </div>
        </div>

        <div class="row align-items-center mt-3">
            <div class="col">
                <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle ForeColor="Blue" Width="30px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RET_STATUS" HeaderText="Rej. Reff No" />
                        <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
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

        <div class="row align-items-center">
            <asp:Label ID="Label3" runat="server" ForeColor="#FF3300" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
        </div>
    </div>


</asp:Content>
