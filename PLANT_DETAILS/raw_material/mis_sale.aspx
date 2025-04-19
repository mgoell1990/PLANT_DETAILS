<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="mis_sale.aspx.vb" Inherits="PLANT_DETAILS.mis_sale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=DropDownList26]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/S_SO")%>',
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
            <asp:Label ID="Label1" runat="server" Text="Miscellaneous Sale" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="row mt-2 justify-content-center text-center">
                    <div class="col text-center">
                        <asp:MultiView ID="MultiView1" runat="server">

                            <%--=====VIEW 1 GARN START=====--%>
                            <asp:View ID="View1" runat="server">
                                <div class="row justify-content-center">
                                    <div class="col-7 justify-content-center" style="border-style: Groove; border-color: #FFFF66">

                                        <div class="row justify-content-center align-items-center mt-2">
                                            <div class="col-3 text-start">
                                                <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-"></asp:Label>
                                            </div>
                                            <div class="col-6 text-start">
                                                <asp:TextBox class="form-control" ID="DropDownList26" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row justify-content-center align-items-center mt-1">
                                            <div class="col">
                                                <asp:Label ID="Label403" runat="server" Text="&lt;marquee&gt;Purchase/Sales/Work Order Codes will be &quot;P&quot; for Purchase, &quot;S&quot; for Sales, &quot;W&quot; for Work Order and then &quot;01&quot; for Store Material &quot;02&quot; for Raw Material &quot;04&quot; for Miscellaneous  &quot;05&quot; for Finished Goods&lt;/marquee&gt;" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="row justify-content-center align-items-center mt-2 mb-2">
                                            <div class="col-3 text-start">
                                            </div>
                                            <div class="col-6 text-start">
                                                <asp:Button ID="Button45" runat="server" class="btn btn-primary fw-bold" Text="Add" />
                                                <asp:Button ID="Button46" runat="server" class="btn btn-primary fw-bold" Text="Cancel" />

                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </asp:View>
                            <%--=====VIEW 2 START=====--%>
                            <asp:View ID="View2" runat="server">
                                <div class="row">
                                    <div class="col" style="border: 3px; border-style: Double; border-color: #00CC00">
                                        <div class="row" style="border-bottom: 10px double #008000;">

                                            <div class="col ms-1 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label30" runat="server" Text="IRN No" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 text-start g-0">
                                                        <asp:Label ID="Label48" runat="server" Text="E-Way Bill No" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox8" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 text-start">
                                                        <asp:Label ID="Label52" runat="server" Text="Validity" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox20" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label345" runat="server" Text="Invoice No" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2">
                                                        <asp:TextBox class="form-control" ID="TextBox177" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2">
                                                        <asp:TextBox class="form-control" ID="TextBox95" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:Label ID="Label406" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label346" runat="server" Text="S.O. No" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>

                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control fw-bold" ID="TextBox123" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label347" runat="server" Text="Buyer Name" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-1 text-start">
                                                        <asp:TextBox class="form-control fw-bold" ID="TextBox96" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control fw-bold" ID="TextBox97" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                    </div>
                                                    <div class="col-2 text-start">
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Consignee Name"></asp:Label>
                                                    </div>
                                                    <div class="col-1 text-start">
                                                        <asp:TextBox class="form-control fw-bold" ID="TextBox1" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control fw-bold" ID="TextBox2" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label348" runat="server" Font-Bold="True" ForeColor="Blue" Text="Transporter" Font-Names="Times New Roman"></asp:Label>

                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList9" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label349" runat="server" Text="Transp. Name" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-1 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox98" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>

                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox99" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label465" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="WO Sl No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList28" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label466" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Name"></asp:Label>
                                                    </div>
                                                    <div class="col-7 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox175" runat="server" BackColor="#559fe0" ForeColor="White" Font-Bold="False"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label350" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Truck No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox100" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label351" runat="server" Text="Form to Receive" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList10" runat="server" Font-Names="Times New Roman">
                                                            <asp:ListItem>N/A</asp:ListItem>
                                                            <asp:ListItem>F Form</asp:ListItem>
                                                            <asp:ListItem>C Form</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label352" runat="server" Text="Debit Entry" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox101" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label353" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="LR No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox102" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 text-start g-0">
                                                        <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Rcd Voucher"></asp:Label>

                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Bal. Amount"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control fw-bold" ID="TextBox9" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label468" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Rcd Voucher Date"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control fw-bold" ID="TextBox176" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                    </div>
                                                    <div class="col-7 text-start">
                                                    </div>
                                                </div>


                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label354" runat="server" Text="Notification" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-11 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox103" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 ms-1 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label378" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList11" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label379" runat="server" Font-Bold="True" ForeColor="Blue" Text="Vocab No"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox104" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label380" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amd. No"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox105" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label381" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox106" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label382" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Code"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList12" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:Label ID="Label467" runat="server" Visible="False"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label383" runat="server" Font-Bold="True" ForeColor="Blue" Text="Despatch Quantity"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox107" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:Label ID="Label384" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center mt-2">
                                                    <div class="col-2 text-end">
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:Button ID="Button43" runat="server" Text="Add" CssClass="btn btn-primary fw-bold" />
                                                        <asp:Button ID="Button44" runat="server" Text="Save" CssClass="btn btn-success fw-bold" Enabled="False" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                        <asp:Button ID="Button42" runat="server" Text="Cancel" CssClass="btn btn-danger fw-bold" />
                                                    </div>
                                                    <div class="col-2 text-start">
                                                    </div>

                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-2 text-end">
                                                    </div>
                                                    <div class="col text-start">
                                                        <asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label31" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                    </div>
                                                    <div class="col-10 text-start">
                                                        <asp:Label ID="txtEinvoiceErrorCode" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                    </div>

                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label42" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                    </div>
                                                    <div class="col-10 text-start">
                                                        <asp:Label ID="txtEinvoiceErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col h-100 text-start">
                                                <div class="row h-100 align-items-center mt-1">
                                                    <div class="col h-100 g-0 text-start">
                                                        <asp:FormView ID="FormView2" CssClass="h-100 w-100" runat="server" Font-Names="Times New Roman" BorderColor="Red" BorderStyle="Double">
                                                            <ItemTemplate>
                                                                <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF;"><%# Eval("ITEM_CODE")%> </h3>
                                                                <div style="border-bottom: 10px double #000000;"></div>
                                                                <br />
                                                                <asp:Label ID="Label355" runat="server" Font-Bold="True" Text="Item Desc" Width="100"></asp:Label>
                                                                <asp:Label ID="Label356" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label357" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label358" runat="server" Font-Bold="True" Text="Item A/U" Width="100"></asp:Label>
                                                                <asp:Label ID="Label359" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label360" runat="server" Text='<%# Eval("ITEM_AU")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label371" runat="server" Font-Bold="True" Text="Unit In Stock" Width="100"></asp:Label>
                                                                <asp:Label ID="Label372" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label373" runat="server" Text='<%# Eval("ITEM_B_STOCK")%>' Width="120"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label365" runat="server" Font-Bold="True" Text="Ord. Qty" Width="100"></asp:Label>
                                                                <asp:Label ID="Label366" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label367" runat="server" Text='<%# Eval("ITEM_QTY")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label368" runat="server" Font-Bold="True" Text="Ord. Bal Qty" Width="100"></asp:Label>
                                                                <asp:Label ID="Label369" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label370" runat="server" Text='<%# Eval("ITEM_BAL_QTY")%>' Width="120"></asp:Label>
                                                                <br />
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:FormView>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row mt-1">
                                            <div class="col g-0">
                                                <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Names="Times New Roman" ShowHeaderWhenEmpty="True" Width="100%" BackColor="White">
                                                    <Columns>
                                                        <asp:BoundField DataField="mat_sl_no" HeaderText="Sl No" />
                                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="MAT_NAME" HeaderText="Item Name" />
                                                        <asp:BoundField DataField="ITEM_AU" HeaderText="A / U" />
                                                        <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Item Qty (Mt)" />
                                                        <asp:BoundField DataField="ASS_VALUE" HeaderText="Assessable Value" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                    <HeaderStyle BackColor="#990000" BorderColor="Blue" BorderStyle="Double" BorderWidth="2px" Font-Bold="True" ForeColor="#FFFFCC" />
                                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="White" BorderColor="Lime" BorderStyle="Ridge" ForeColor="#330099" />
                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="row mt-1">
                                            <div class="col ms-1 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label389" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount(Rs.)"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox111" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label390" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Taxable Value"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox112" runat="server">0.00</asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label391" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox113" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label392" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox114" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label393" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="P&amp;F"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox115" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label394" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox116" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label395" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Terminal Tax"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox117" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label396" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox118" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label397" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="TCS"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox119" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label398" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox120" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                    </div>
                                                    <div class="col-2 text-start">
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label400" runat="server" Font-Bold="True" ForeColor="Red" Style="text-align: left" Text="Total Value"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox122" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
