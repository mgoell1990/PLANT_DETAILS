<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="gst_trans.aspx.vb" Inherits="PLANT_DETAILS.gst_trans" %>

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
            $("[id$=DropDownList22]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/supl")%>',
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
            <asp:Label ID="Label94" runat="server" Text="Advance Vouchers" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-5 text-end">
            <asp:Label ID="Label53" runat="server" Text="Voucher Type"></asp:Label>

        </div>
        <div class="col-3 text-end">
            <asp:DropDownList class="form-select" ID="DropDownList9" runat="server" AutoPostBack="True">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Advance Receipt Voucher (For Supply Of Goods)</asp:ListItem>
                <%--<asp:ListItem>Advance Receipt Voucher (For Services)</asp:ListItem>--%>
                <asp:ListItem>Advance Payment Voucher for Goods</asp:ListItem>
                <asp:ListItem>Advance Payment Voucher For Services</asp:ListItem>
                <%--<asp:ListItem>Refund Advance Voucher </asp:ListItem>
                        <asp:ListItem>Advance Payment Voucher Refund </asp:ListItem>--%>
            </asp:DropDownList>
        </div>
        <div class="col-2 text-end">
            <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox18" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox18" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:Panel ID="rcd_Panel" runat="server" Visible="False">
                    <div class="row justify-content-center">
                        <div class="col-12 justify-content-center m-1" style="border-color: Blue; border-style: groove">

                            <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                <div class="col text-center">
                                    <asp:Label ID="Label625" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Style="text-align: center;" Text="Advance Receipt Voucher"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label629" runat="server" ForeColor="Blue" Text="Rcpt Voucher No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox94" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col text-start">
                                    <asp:Label ID="Label455" runat="server" ForeColor="Red"></asp:Label>
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label451" runat="server" ForeColor="Blue" Text="Token No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox59" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label543" runat="server" ForeColor="Blue" Text="C.B.V. No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox93" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label542" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Blue" Visible="False"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label449" runat="server" ForeColor="Blue" Text="Section Sl No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox56" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label450" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox57" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox57_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox57"></asp:CalendarExtender>
                                </div>
                                <div class="col text-end">
                                    <asp:Button ID="Button21" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                    <asp:Button ID="Button48" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                </div>
                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label507" runat="server" ForeColor="Blue" Text="Inst Type"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="DropDownList23" runat="server">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>D.D.</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>E Payment</asp:ListItem>
                                        <asp:ListItem>NEFT</asp:ListItem>
                                        <asp:ListItem>RTGS</asp:ListItem>
                                        <asp:ListItem>A/c payee</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label508" runat="server" ForeColor="Blue" Text="Inst No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox82" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label509" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>

                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox83" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox83_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox83"></asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label453" runat="server" ForeColor="Blue" Text="Taxable Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox60" runat="server"></asp:TextBox>
                                </div>


                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label639" runat="server" ForeColor="Blue" Text="Tax Pay On Rev. Charge"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="DropDownList29" runat="server">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label27" runat="server" ForeColor="Blue" Text="IOC No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox19" runat="server"></asp:TextBox>
                                </div>

                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label510" runat="server" ForeColor="Blue" Text="Drawn On"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox84" runat="server"></asp:TextBox>
                                </div>

                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label511" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox58" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label627" runat="server" ForeColor="Blue" Text="Order No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="DropDownList26" runat="server" AutoPostBack="True">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>D.D.</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>E Payment</asp:ListItem>
                                        <asp:ListItem>NEFT</asp:ListItem>
                                        <asp:ListItem>RTGS</asp:ListItem>
                                        <asp:ListItem>A/c payee</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label628" runat="server" ForeColor="Blue" Text="Item Sl No"></asp:Label>
                                </div>
                                <div class="col-3 text-start">
                                    <asp:DropDownList class="form-select" ID="DropDownList27" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label444" runat="server" ForeColor="Blue" Text="Product Details"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="DropDownList22" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label638" runat="server" ForeColor="Blue" Text="Debt Details"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="DropDownList28" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col text-center">
                                    <asp:Label ID="Label630" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Underline="True" ForeColor="#CC0000" Text="AMOUNT DETAILS"></asp:Label>
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label631" runat="server" ForeColor="Blue" Text="Taxable Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox95" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>


                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label632" runat="server" ForeColor="Blue" Text="CGST Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox96" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label633" runat="server" ForeColor="Blue" Text="SGST Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox97" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label634" runat="server" ForeColor="Blue" Text="IGST Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox98" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label635" runat="server" ForeColor="Blue" Text="CESS Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox99" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label636" runat="server" ForeColor="Blue" Text="Terminal Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox100" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label640" runat="server" ForeColor="Blue" Text="TCS Amt"></asp:Label>
                                </div>
                                <div class="col text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox102" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>
                            </div>
                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label637" runat="server" ForeColor="Blue" Text="Total Invoice Amt"></asp:Label>
                                </div>

                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox101" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                            </div>

                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel ID="Panel1" runat="server" Visible="False">
                    <div class="row justify-content-center">
                        <div class="col-12 justify-content-center m-1" style="border-color: Blue; border-style: groove">

                            <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                <div class="col text-center">
                                    <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" Style="text-align: center;" Text="Advance Payment Voucher"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="Pay. Voucher No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox1" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col text-start">
                                    <asp:Label ID="Label29" runat="server" ForeColor="Red"></asp:Label>
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="Token No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox2" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="C.B.V. No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox3" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="Section Sl No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox4" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label8" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox5" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox5"></asp:CalendarExtender>
                                </div>
                                <div class="col text-end">
                                    <asp:Button ID="Button2" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                    <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                </div>
                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label9" runat="server" ForeColor="Blue" Text="Inst Type"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="DropDownList1" runat="server">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>D.D.</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>E Payment</asp:ListItem>
                                        <asp:ListItem>NEFT</asp:ListItem>
                                        <asp:ListItem>RTGS</asp:ListItem>
                                        <asp:ListItem>A/c payee</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label10" runat="server" ForeColor="Blue" Text="Inst No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox6" runat="server"></asp:TextBox>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label11" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>

                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox7" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox7"></asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label12" runat="server" ForeColor="Blue" Text="Taxable Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox8" runat="server"></asp:TextBox>
                                </div>


                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label13" runat="server" ForeColor="Blue" Text="Tax Pay On Rev. Charge"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="DropDownList2" runat="server">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label31" runat="server" ForeColor="Blue" Text="IOC No"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox21" runat="server"></asp:TextBox>
                                </div>

                            </div>

                            <div class="row  align-items-center">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label14" runat="server" ForeColor="Blue" Text="Drawn On"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox9" runat="server"></asp:TextBox>
                                </div>

                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox10" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label32" runat="server" ForeColor="Blue" Text="TDS(Rs.)"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox22" runat="server">0</asp:TextBox>
                                </div>
                            </div>



                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label16" runat="server" ForeColor="Blue" Text="Order No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="DropDownList3" runat="server" AutoPostBack="True">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>D.D.</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>E Payment</asp:ListItem>
                                        <asp:ListItem>NEFT</asp:ListItem>
                                        <asp:ListItem>RTGS</asp:ListItem>
                                        <asp:ListItem>A/c payee</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label17" runat="server" ForeColor="Blue" Text="Item SlNo"></asp:Label>
                                </div>
                                <div class="col-3 text-start">
                                    <asp:DropDownList class="form-select" ID="DropDownList4" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label18" runat="server" ForeColor="Blue" Text="Product Details"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox11" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label19" runat="server" ForeColor="Blue" Text="Party Details"></asp:Label>
                                </div>
                                <div class="col-6 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox12" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col text-center">
                                    <asp:Label ID="Label52" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Underline="True" ForeColor="#CC0000" Text="AMOUNT DETAILS"></asp:Label>
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label21" runat="server" ForeColor="Blue" Text="Taxable Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox13" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>


                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label22" runat="server" ForeColor="Blue" Text="CGST Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox14" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label23" runat="server" ForeColor="Blue" Text="SGST Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox15" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>
                                <div class="col-2 text-start">
                                </div>
                                <div class="col-2 text-start g-0">
                                </div>
                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label24" runat="server" ForeColor="Blue" Text="IGST Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox16" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label25" runat="server" ForeColor="Blue" Text="CESS Amt"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox17" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                                <div class="col-2 text-start">
                                </div>
                                <div class="col text-start g-0">
                                </div>
                            </div>
                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label28" runat="server" ForeColor="Blue" Text="Total Invoice Amt"></asp:Label>
                                </div>

                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox20" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                            </div>

                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>


</asp:Content>
