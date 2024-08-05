<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="print_invoice.aspx.vb" Inherits="PLANT_DETAILS.print_invoice" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $("[id$=txtContactsSearch]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/inv_no")%>',
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
            <asp:Label ID="Label1" runat="server" Text="Print Invoice" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>



    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-6 text-center">

                <asp:Panel ID="Panel4" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman">
                    <div class="row align-items-center m-1">

                        <div class="col-3 text-start">
                            <asp:Label ID="Label311" runat="server" Font-Bold="True" Text="Invoice No" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:TextBox CssClass="form-control" ID="txtContactsSearch" runat="server"></asp:TextBox>
                        </div>


                    </div>
                    <div class="row align-items-center m-1">

                        <div class="col-3 text-start">
                            <asp:Label ID="Label312" runat="server" Font-Bold="True" Text="Financial Year" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:TextBox CssClass="form-control" ID="TextBox72" runat="server"></asp:TextBox>
                        </div>


                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-3 text-start">
                        </div>

                        <div class="col text-end">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" CssClass="radio" AutoPostBack="false" RepeatDirection="Horizontal" ForeColor="Blue">
                                <asp:ListItem>Despatch</asp:ListItem>
                                <asp:ListItem>Service</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="row align-items-center m-1 mt-2">

                        <div class="col-3 text-start">
                        </div>
                        <div class="col text-start">
                            <asp:Button ID="Button38" runat="server" Font-Bold="True" Text="Print Extra Copy" CssClass="btn btn-primary" />
                            <asp:Button ID="Button39" runat="server" Font-Bold="True" Text="View Pending Print" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                        </div>

                    </div>

                    <div class="row align-items-center m-1">

                        <asp:Panel ID="Panel45" runat="server" ScrollBars="Auto">
                            <%--<asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="Gray"
                                BorderStyle="Groove" BorderWidth="1px"
                                CellPadding="3" ForeColor="Black" GridLines="Both" Style="text-align: center; width: 100%; height: 75px; font-size: medium" class="table table-bordered text-center">
                                <RowStyle Height="35px" />
                                <FooterStyle BackColor="#cccc99" />
                                <HeaderStyle BackColor="#296DA9" Font-Bold="true" ForeColor="white" />
                                <RowStyle BackColor="#f7f7de" />
                                <SelectedRowStyle BackColor="#ce5d5a" Font-Bold="true" ForeColor="white" />
                                <SortedAscendingCellStyle BackColor="#fbfbf2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#eaead3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>--%>

                            <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="3px" CellPadding="4" EmptyDataText="No More Invoice To Print" Font-Names="Times New Roman" Font-Size="Smaller" Visible="False">
                                <Columns>
                                    <asp:BoundField DataField="INV_NO" HeaderText="INVOICE NO">
                                        <ItemStyle Font-Bold="True" ForeColor="Blue" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="ORIGINAL">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Button1" ForeColor="BLUE" runat="server" Text='<%#Eval("PRINT_ORIGN")%>' CommandName='<%#Eval("INV_NO")%>' OnClick="ORIGINAL" />
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Blue" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DUPLICATE">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Button2" ForeColor="BLUE" runat="server" Text='<%#Eval("PRINT_TRANS")%>' CommandName='<%#Eval("INV_NO")%>' OnClick="DUPLICATE" />
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Blue" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TRIPLICATE">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Button3" ForeColor="BLUE" runat="server" Text='<%#Eval("PRINT_ASSAE")%>' CommandName='<%#Eval("INV_NO")%>' OnClick="TRIPLICATE" />
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Blue" Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="Blue" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Blue" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                            </asp:GridView>
                        </asp:Panel>

                    </div>


                </asp:Panel>
            </div>

        </div>
    </div>

</asp:Content>
