<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="DeliveryChallan.aspx.vb" Inherits="PLANT_DETAILS.DeliveryChallan" %>

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
            $("[id$=TextBox55]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/T_NO")%>',
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
            <asp:Label ID="Label1" runat="server" Text="Invoicing of Finished Goods" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div class="row mt-1 justify-content-center text-center">
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
                                                <div class="row align-items-center mt-1">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label30" runat="server" Font-Bold="True" ForeColor="Blue" Text="IRN No"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 text-start g-0">
                                                        <asp:Label ID="Label48" runat="server" Text="E-Way Bill" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox8" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 text-start">
                                                        <asp:Label ID="Label52" runat="server" Text="Validity" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox20" runat="server" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label299" runat="server" Font-Bold="True" ForeColor="Blue" Text="Invoice No"></asp:Label>
                                                    </div>
                                                    <div class="col-2">
                                                        <asp:TextBox class="form-control" ID="TextBox177" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox65" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-7">
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label404" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox124" runat="server" BackColor="#559fe0" ForeColor="White"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label405" runat="server" Font-Bold="True" ForeColor="Blue" Text="Buyer Name"></asp:Label>
                                                    </div>
                                                    <div class="col-1 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox125" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox126" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                    </div>
                                                    <div class="col-2 text-start">
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label51" runat="server" Font-Bold="True" ForeColor="Blue" Text="Consignee Name"></asp:Label>
                                                    </div>
                                                    <div class="col-1 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox18" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox19" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label287" runat="server" Font-Bold="True" ForeColor="Blue" Text="Transporter"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList6" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label296" runat="server" Font-Bold="True" ForeColor="Blue" Text="Transp. Name"></asp:Label>
                                                    </div>
                                                    <div class="col-1 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox75" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox62" runat="server" BackColor="#559fe0" ReadOnly="True" ForeColor="White"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label463" runat="server" Font-Bold="True" ForeColor="Blue" Text="WO Sl No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList27" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label464" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Name"></asp:Label>
                                                    </div>
                                                    <div class="col-7 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox174" runat="server" BackColor="#559fe0" Font-Bold="False" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label288" runat="server" Font-Bold="True" ForeColor="Blue" Text="Truck No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox55" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label309" runat="server" Font-Bold="True" ForeColor="Blue" Text="RR No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox69" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-1 text-start">
                                                        <asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="Blue" Text="RR Date"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox5" runat="server">N/A</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label293" runat="server" Font-Bold="True" ForeColor="Blue" Text="Form to Receive"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList7" runat="server">
                                                            <asp:ListItem>N/A</asp:ListItem>
                                                            <asp:ListItem>F Form</asp:ListItem>
                                                            <asp:ListItem>C Form</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label32" runat="server" Font-Bold="True" ForeColor="Blue" Text="Rcd Voucher No"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-1 text-start">
                                                        <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Blue" Text="Balance"></asp:Label>
                                                    </div>
                                                    <div class="col-1 text-start g-0">
                                                        <asp:TextBox class="form-control" ID="TextBox9" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>

                                                    <div class="col-1 text-start">
                                                        <asp:Label ID="Label468" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                                    </div>
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox176" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>


                                                <div class="row align-items-center mb-1">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label298" runat="server" Font-Bold="True" ForeColor="Blue" Text="Notification"></asp:Label>
                                                    </div>
                                                    <div class="col-11 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox64" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 ms-1 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label283" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList4" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label284" runat="server" Font-Bold="True" ForeColor="Blue" Text="Vocab No"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox53" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label306" runat="server" Font-Bold="True" ForeColor="Blue" Text="Amd. No"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox67" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label307" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox68" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label285" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Code"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:DropDownList class="form-select" ID="DropDownList5" runat="server" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:Label ID="Label467" runat="server" Visible="False"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label286" runat="server" Font-Bold="True" ForeColor="Blue" Text="Despatch Qty."></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox54" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:Label ID="Label289" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Price."></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="txtUnitPrice" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-6 text-start g-0">
                                                        <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label290" runat="server" Font-Bold="True" ForeColor="Blue" Text="No. of Bags"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox56" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label291" runat="server" Font-Bold="True" ForeColor="Blue" Text="Lot No"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox57" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-2 g-0 text-start">
                                                        <asp:Label ID="Label47" runat="server" Font-Bold="True" ForeColor="Blue" Text="Transp. Wt.(MT)"></asp:Label>
                                                    </div>
                                                    <div class="col-4 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox7" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>




                                                <div class="row align-items-center mt-2">
                                                    <div class="col-2 text-end">
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:Button ID="Button35" runat="server" Text="Add" CssClass="btn btn-primary fw-bold" />
                                                        <asp:Button ID="Button36" runat="server" Text="Save" CssClass="btn btn-success fw-bold" ReadOnly="True" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                        <asp:Button ID="Button37" runat="server" Text="Cancel" CssClass="btn btn-danger fw-bold" />
                                                        <%--<asp:Button ID="Button1" runat="server" Text="NEW"   CssClass="bottomstyle" />--%>
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
                                                        <asp:FormView ID="FormView1" runat="server" Width="100%" Height="100%" BorderColor="Red" BorderStyle="Double">
                                                            <ItemTemplate>
                                                                <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF;"><%# Eval("ITEM_CODE")%> </h3>
                                                                <h1>===============================================</h1>
                                                                <br></br>
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Item Desc" Width="150"></asp:Label>
                                                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("ITEM_NAME")%>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" Text="Unit Price" Width="150"></asp:Label>
                                                                <asp:Label ID="Label27" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label34" runat="server" Text='<%# Eval("ITEM_UNIT_RATE")%>'></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Accounting Unit" Width="150"></asp:Label>
                                                                <asp:Label ID="Label16" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("ITEM_AU")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Unit Weight" Width="100"></asp:Label>
                                                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("ITEM_WEIGHT")%>'></asp:Label>
                                                                <asp:Label ID="Label23" runat="server" Text="(Kg)"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="Ord. Qty" Width="150"></asp:Label>
                                                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label9" runat="server" Text='<%# Eval("ITEM_QTY")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Ord. Bal Qty" Width="100"></asp:Label>
                                                                <asp:Label ID="Label19" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label11" runat="server" Text='<%# Eval("ITEM_BAL_QTY")%>' Width="120"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Text="Unit In Stock" Width="150"></asp:Label>
                                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label13" runat="server" Text='<%# Eval("ITEM_F_STOCK")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="Unit In Stock" Width="100"></asp:Label>
                                                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label22" runat="server" Text='<%# Eval("ITEM_F_STOCK_MT")%>'></asp:Label>
                                                                <asp:Label ID="Label29" runat="server" Text="(Mt)"></asp:Label>
                                                                <br />
                                                            </ItemTemplate>
                                                        </asp:FormView>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row mt-1">
                                            <div class="col g-0">
                                                <%--<asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" Width="100%" BackColor="White">
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
                                        </asp:GridView>--%>
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="2" GridLines="Vertical" ShowHeaderWhenEmpty="True" Width="100%">
                                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                                    <Columns>
                                                        <asp:BoundField DataField="mat_sl_no" HeaderText="Sl No" />
                                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="MAT_NAME" HeaderText="Item Name" />
                                                        <asp:BoundField DataField="ITEM_AU" HeaderText="A / U" />
                                                        <asp:BoundField DataField="ITEM_QTY_PCS" HeaderText="Item Qty (Pcs)" />
                                                        <asp:BoundField DataField="UNIT_WEIGHT" HeaderText="Unit Weight(Kg.)" />
                                                        <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Item Qty (Mt)" />
                                                        <asp:BoundField DataField="Packing Details" HeaderText="Packing Details" />
                                                        <asp:BoundField DataField="ASS_VALUE" HeaderText="Total Base Value" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />

                                                    <HeaderStyle HorizontalAlign="Center" BackColor="#FF0066" BorderColor="Blue" BorderStyle="Double" BorderWidth="2px" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                    <RowStyle HorizontalAlign="Center" BackColor="#EEEEEE" BorderColor="Lime" BorderStyle="Ridge" ForeColor="Black" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" BorderColor="Black" />
                                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#000065" />

                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div class="row mt-1">
                                            <div class="col ms-1 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label43" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount(Rs.)"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox37" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label49" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Taxable Value"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox38" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label44" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox39" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label50" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox40" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label45" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="P&amp;F"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox41" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label300" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox42" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label46" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="Terminal Tax"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox43" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label301" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox44" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-1 g-0 text-start">
                                                        <asp:Label ID="Label305" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="TCS"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox66" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                    </div>
                                                    <div class="col-2 text-end">
                                                        <asp:Label ID="Label304" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: left" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox21" runat="server" ReadOnly="True">0.00</asp:TextBox>
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
                                                        <asp:Label ID="Label302" runat="server" Font-Bold="True" ForeColor="Red" Style="text-align: left" Text="Total Value"></asp:Label>
                                                    </div>
                                                    <div class="col-2 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox45" runat="server" ReadOnly="True">0.00</asp:TextBox>
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


