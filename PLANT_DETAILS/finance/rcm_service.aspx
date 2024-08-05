<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="rcm_service.aspx.vb" Inherits="PLANT_DETAILS.rcm_service" %>

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
            $("[id$=DropDownList26]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/rcm_ser_po")%>',
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
            <asp:Label ID="Label1" runat="server" Text="Tax Invoice (For service under reverse charge)" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label5" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox3" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="TextBox32_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox3" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 GARN START=====--%>
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center">
                            <div class="col-7 justify-content-center" style="border-style: Groove; border-color: Red">

                                <div class="row align-items-center justify-content-center" style="background-color: #296DA9">
                                    <div class="col text-center">
                                        <asp:Label ID="Label625" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="White" Style="text-align: center;" Text="Order Selection"></asp:Label>
                                    </div>
                                </div>
                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label470" runat="server" Font-Bold="True" ForeColor="Blue" Text="Party Type:-"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList31" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Unregistered Party</asp:ListItem>
                                            <asp:ListItem>Registered Party</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-" Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="DropDownList26" runat="server" Font-Names="Times New Roman" Visible="False"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2 mb-2">
                                    <div class="col-3 text-start">
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:Button ID="Button45" runat="server" class="btn btn-primary fw-bold" Text="Add" />
                                        <asp:Button ID="Button46" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                    </div>
                                </div>





                            </div>
                        </div>
                    </asp:View>
                    <%--=====VIEW 2 START=====--%>
                    <asp:View ID="View2" runat="server">
                        <div class="row mt-1">
                            <div class="col" style="border: 2px; border-style: Double; border-color: #4686F0">
                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label30" runat="server" Text="IRN No" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server" ReadOnly="True"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label299" runat="server" Text="RCM Invoice No" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox177" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox65" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="PV Inv. No"></asp:Label>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox1" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox2" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label485" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="OS Inv. No"></asp:Label>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox190" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox191" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="S.O. No/WO No."></asp:Label>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox124" runat="server" BackColor="Red" ForeColor="White"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Supl. Code"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox125" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start ">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Supl. Name"></asp:Label>
                                    </div>
                                    <div class="col text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox126" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>


                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label482" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Party Type"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList33" runat="server">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 text-start ">
                                        <asp:Label ID="Label483" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="State Code"></asp:Label>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox188" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-1 text-start ">
                                        
                                    </div>
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label484" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="State"></asp:Label>
                                    </div>
                                    <div class="col text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox189" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="INV. For"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start g-0">
                                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>RCM For Tax Invoice</asp:ListItem>
                                            <asp:ListItem>RCM For Payment Voucher</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <asp:Panel ID="Panel3" runat="server" Visible="False">
                                    <div class="row align-items-center">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label469" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="INV. No"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList30" runat="server" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="row align-items-center mt-1">
                                    <div class="col text-center" style="border-bottom: 10px double #008000;">

                                        <asp:Panel ID="Panel1" runat="server" Visible="False">

                                            <div class="row align-items-center">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="INV. No"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:TextBox class="form-control" ID="TextBox178" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1 text-start">
                                                    <asp:Label ID="Label473" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="HSN Code"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:DropDownList class="form-select" ID="DropDownList32" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label476" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Work Qty."></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:TextBox class="form-control" ID="TextBox182" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1 text-start">
                                                    <asp:Label ID="Label477" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Unit Rate"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:TextBox class="form-control" ID="TextBox183" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label478" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CGST"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:TextBox class="form-control" ID="TextBox184" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1 text-start">
                                                    <asp:Label ID="Label479" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="SGST"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:TextBox class="form-control" ID="TextBox185" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-2 text-start">
                                                    <asp:Label ID="Label480" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="IGST"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:TextBox class="form-control" ID="TextBox186" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-1 text-start">
                                                    <asp:Label ID="Label481" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CESS"></asp:Label>
                                                </div>
                                                <div class="col-2 text-start g-0">
                                                    <asp:TextBox class="form-control" ID="TextBox187" runat="server"></asp:TextBox>
                                                </div>
                                            </div>


                                        </asp:Panel>

                                    </div>
                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label298" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Notification"></asp:Label>
                                    </div>
                                    <div class="col text-start g-0">
                                        <asp:TextBox class="form-control" ID="TextBox64" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col text-start">
                                        <asp:Label ID="Label308" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label31" runat="server" Text="Error Code : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>
                                    <div class="col text-start g-0">
                                        <asp:Label ID="txtEinvoiceErrorCode" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label42" runat="server" Text="Error Message : " Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>
                                    <div class="col text-start g-0">
                                        <asp:Label ID="txtEinvoiceErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-2">
                                    <div class="col-2 text-start">
                                        
                                    </div>
                                    <div class="col text-start g-0">
                                        <asp:Button ID="Button35" runat="server" Text="Add" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button36" runat="server" Text="Save" CssClass="btn btn-success" Enabled="False" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                        <asp:Button ID="Button1" runat="server" Text="New" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button37" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                        
                                    </div>

                                </div>



                                <div class="row align-items-center mt-1">
                                    <div class="col text-center">
                                        <asp:GridView ID="GridView2" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" CellPadding="2">
                                            <Columns>

                                                <asp:BoundField DataField="mb_no" HeaderText="MB No" />
                                                <asp:BoundField DataField="wo_slno" HeaderText="Wo. SLNO" />
                                                <asp:BoundField DataField="w_name" HeaderText="Work Desc." />
                                                <asp:BoundField DataField="w_au" HeaderText="A / U" />
                                                <asp:BoundField DataField="sac_code" HeaderText="SAC Code" />
                                                <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Rate" />
                                                <asp:BoundField DataField="work_qty" HeaderText="Work Qty" />
                                                <asp:BoundField DataField="prov_amt" HeaderText="Taxable Value" />
                                                <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                <asp:BoundField DataField="cgst_liab" HeaderText="CGST Amt." />
                                                <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                <asp:BoundField DataField="sgst_liab" HeaderText="SGST Amt." />
                                                <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                <asp:BoundField DataField="igst_liab" HeaderText="IGST Amt" />
                                                <asp:BoundField DataField="CESS" HeaderText="CESS" />
                                                <asp:BoundField DataField="cess_liab" HeaderText="CESS Amt." />
                                                <asp:BoundField DataField="TOTAL_VAL" HeaderText="Total value of Goods" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
    </div>


</asp:Content>


