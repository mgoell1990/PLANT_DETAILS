<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="mat_garn.aspx.vb" Inherits="PLANT_DETAILS.mat_garn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .checkbox {
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Store Material GARN" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label21" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox ID="TextBox2" class="form-control" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox2" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center">
                    <div class="col-5 justify-content-center">
                        <asp:Panel ID="Panel16" runat="server" BorderColor="#FFFF66" BorderStyle="Groove">
                            <div class="row justify-content-center align-items-center mt-2">
                                <div class="col-3 text-start">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Material Type"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList ID="type_DropDown" runat="server" AutoPostBack="True" CssClass="form-select">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row justify-content-center align-items-center mt-1">
                                <div class="col-3 text-start">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="form-select">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="row justify-content-center align-items-center mt-1">
                                <div class="col-3 text-start">
                                    <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="GARN No"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList ID="search_DropDown" runat="server" AutoPostBack="True" CssClass="form-select">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="row justify-content-center align-items-center mt-2 mb-2">
                                <div class="col-3 text-start">
                                </div>
                                <div class="col-6 text-start">
                                    <asp:Button ID="new_button" runat="server" class="btn btn-primary fw-bold" Text="NEW" />
                                    <asp:Button ID="view_button" runat="server" class="btn btn-primary fw-bold" Text="VIEW" />

                                </div>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 GARN START=====--%>
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center">
                            <div class="col-11">
                                <div class="row" style="border: 3px; border-style: Groove; border-color: #FF3399">
                                    <%--=================Left Panel ================================--%>
                                    <div class="col-3 text-start" style="border-right: 3px; border-right-style: groove; border-right-color: #FF3399">
                                        <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="GOODS ACCEPTANCE AND RECEIPT NOTE"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label22" runat="server" ForeColor="Blue" Text="GARN No"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:TextBox ID="GARN_NO_TextBox" runat="server" class="form-control" BackColor="Red" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label23" runat="server" ForeColor="Blue" Text="CRR No"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList ID="garn_crrnoDropDownList" CssClass="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label24" runat="server" ForeColor="Blue" Text="MAT SLNo"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList ID="DropDownList6" CssClass="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center mt-2" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Details"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label27" runat="server" ForeColor="Blue" Text="P.O. No"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label398" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label28" runat="server" ForeColor="Blue" Text="AMD No"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label422" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label395" runat="server" ForeColor="Blue" Text="SUPL Name"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label396" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label391" runat="server" ForeColor="Blue" Text="MAT Code"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label392" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label393" runat="server" ForeColor="Blue" Text="MAT Name"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label394" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label423" runat="server" ForeColor="Blue" Text="Transporter"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label424" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label461" runat="server" ForeColor="Blue" Text="Work Sl No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label462" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col text-center">
                                                <asp:Button ID="Button44" runat="server" CssClass="btn btn-primary" Text="ADD" />
                                                <asp:Button ID="Button5" runat="server" CssClass="btn btn-success" Text="SAVE" Enabled="False" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-danger" Text="CLOSE" />

                                            </div>

                                        </div>
                                    </div>
                                    <%--=================Right Panel ================================--%>
                                    <div class="col-9">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col-6">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label29" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="AS PER ORDER"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-center">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="AS PER CHALLAN"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-8 text-start">
                                                        <asp:TextBox ID="TextBox120" runat="server" CssClass="form-control" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox147" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Size="Medium" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox124" runat="server" CssClass="form-control" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Size="Medium" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox149" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label468" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Size="Medium" Text="Packing"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox121" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Size="Medium" Text="Packing"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox151" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label467" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Size="Medium" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox122" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label40" runat="server" Font-Bold="True" Font-Size="Medium" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox153" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Size="Medium" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox123" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Size="Medium" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox155" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label43" runat="server" Font-Bold="True" Font-Size="Medium" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox137" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label44" runat="server" Font-Bold="True" Font-Size="Medium" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox159" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label45" runat="server" Font-Bold="True" Font-Size="Medium" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label46" runat="server" Font-Bold="True" Font-Size="Medium" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label47" runat="server" Font-Bold="True" Font-Size="Medium" Text="Analytical Charge P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label48" runat="server" Font-Bold="True" Font-Size="Medium" Text="Analytical Charge P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox161" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Medium" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox125" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Medium" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox157" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label466" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Medium" Text="LD %"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox142" CssClass="form-control" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Medium" Text="LD (Rs.)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox143" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                                                        <asp:Label ID="Label49" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label419" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Visible="False"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label50" runat="server" Font-Bold="True" Font-Size="Medium" Text="Local Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox160" CssClass="form-control" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label51" runat="server" Font-Bold="True" Font-Size="Medium" Text="Penality P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox162" runat="server" CssClass="form-control" Font-Names="Times New Roman">0.00</asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <div class="form-check">
                                                    <asp:CheckBox ID="CheckBox1" class="form-check-input" runat="server" Text=""></asp:CheckBox>
                                                    <label class="form-check-label text-primary" for="CheckBox1">Percentage</label>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1" style="background-color: #4686F0">
                                            <div class="col">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label52" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Other Charges"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label465" runat="server" Font-Bold="True" Font-Size="Medium" Text="Trans. Penality"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox175" CssClass="form-control" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label389" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remarks For Penality:-"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox144" CssClass="form-control" TextMode="MultiLine" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label390" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remarks For Modification:-"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="TextBox145" runat="server" CssClass="form-control" TextMode="MultiLine" Font-Names="Times New Roman"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-2">
                                                        <asp:Label ID="Label451" runat="server" Font-Bold="True" Font-Size="Medium" Text="GARN Comments:-"></asp:Label>
                                                    </div>
                                                    <div class="col-10">
                                                        <asp:TextBox ID="TextBox173" CssClass="form-control" TextMode="MultiLine" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <asp:Label ID="GARN_ERR_LABLE" runat="server" ForeColor="Red" Text="Label" Visible="False" Font-Bold="True"></asp:Label>
                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center" style="border-bottom: 3px; border-left: 3px; border-right: 3px; border-style: Groove; border-color: #FF3399;">
                                    <div class="col text-center">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label53" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Material Details"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col" style="overflow: scroll">
                                                <asp:GridView ID="GridView2" Width="300%" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
                                                        <asp:BoundField DataField="PO_NO" HeaderText="PO No" />
                                                        <asp:BoundField DataField="AMD_NO" HeaderText="AMD No" />
                                                        <asp:BoundField DataField="MAT_SLNO" HeaderText="MAT SLNo" />
                                                        <asp:BoundField DataField="MAT_CODE" HeaderText="MAT. Code" />
                                                        <asp:BoundField DataField="MAT_NAME" HeaderText="MAT. Name" />
                                                        <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                        <asp:BoundField DataField="MAT_QTY" HeaderText="ORD. Qty." />
                                                        <asp:BoundField DataField="CHLN_NO" HeaderText="CHLN No" />
                                                        <asp:BoundField DataField="MAT_CHALAN_QTY" HeaderText="CHLN. Qty." />
                                                        <asp:BoundField DataField="MAT_RCD_QTY" HeaderText="RCVD. Qty." />
                                                        <asp:BoundField DataField="MAT_REJ_QTY" HeaderText="REJ. Qty." />
                                                        <asp:TemplateField HeaderText="Accept Qty."></asp:TemplateField>
                                                        <asp:BoundField DataField="MAT_EXCE" HeaderText="Excess Qty." />
                                                        <asp:BoundField DataField="MAT_BAL_QTY" HeaderText="BAL. Qty." />
                                                        <asp:TemplateField HeaderText="Unit Rate"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Discount"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="P&amp;F"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Freight"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CESS"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ANAL Charge"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Local Fright"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Penality"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="L.D. Charge"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mat Value"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Loss GST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Wt Var Val"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Transport Charge"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shortage Amount"></asp:TemplateField>
                                                        <asp:BoundField DataField="INSP_NOTE" HeaderText="Remarks" />
                                                        <asp:BoundField DataField="TOTAL_MT" HeaderText="Total Mt" />
                                                    </Columns>
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                                    <RowStyle BackColor="#f5f5f5" />

                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>
                        </div>
                    </asp:View>
                    <%--=====VIEW 2 Foreign GARN Start=====--%>
                    <asp:View ID="View2" runat="server">
                        <div class="row justify-content-center">
                            <div class="col-10 text-center">
                                <asp:Panel ID="imp_Panel1" runat="server" BackColor="Transparent" BorderColor="#FF3300" BorderStyle="Dotted" Font-Names="Times New Roman" Style="text-align: left" Visible="False">
                                    <div class="row ms-1">
                                        <%--=================Left Panel ================================--%>
                                        <div class="col-5 text-start">

                                            <div class="row align-items-center mt-1">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label525" runat="server" ForeColor="Blue" Text="GARN No"></asp:Label>

                                                </div>
                                                <div class="col-8">
                                                    <asp:TextBox ID="imp_garn_no_TextBox1" runat="server" class="form-control" BackColor="Red" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label526" runat="server" ForeColor="Blue" Text="CRR No"></asp:Label>

                                                </div>
                                                <div class="col-8">
                                                    <asp:DropDownList ID="imp_crr_no_DropDownList1" runat="server" AutoPostBack="True" CssClass="form-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label527" runat="server" ForeColor="Blue" Text="MAT SL No"></asp:Label>

                                                </div>
                                                <div class="col-8">

                                                    <asp:DropDownList ID="imp_mat_slno_DropDownList2" runat="server" CssClass="form-select" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="row align-items-center mt-1">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label535" runat="server" ForeColor="Blue" Text="PO No"></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                    <asp:Label ID="Label536" runat="server" ForeColor="Blue"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label537" runat="server" ForeColor="Blue" Text="AMD No"></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                    <asp:Label ID="Label538" runat="server" ForeColor="Blue"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label15" runat="server" ForeColor="Blue" Text="SUPL Name"></asp:Label>

                                                </div>
                                                <div class="col-8">
                                                    <asp:Label ID="Label16" runat="server" ForeColor="Blue"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label17" runat="server" ForeColor="Blue" Text="SUPL Id"></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                    <asp:Label ID="Label18" runat="server" ForeColor="Blue"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label528" runat="server" ForeColor="Blue" Text="BE No"></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                    <asp:Label ID="Label529" runat="server" ForeColor="#CC0000"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label530" runat="server" ForeColor="Blue" Text="Transporter"></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                    <asp:Label ID="Label13" runat="server" ForeColor="#CC0000"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label531" runat="server" ForeColor="Blue" Text="Work Sl No"></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                    <asp:Label ID="Label14" runat="server" ForeColor="#CC0000"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label532" runat="server" ForeColor="Blue" Text="CHA WO."></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                </div>
                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label533" runat="server" ForeColor="Blue" Text="Job Type"></asp:Label>
                                                </div>
                                                <div class="col-8">
                                                </div>
                                            </div>

                                            <div class="row align-items-center mt-2">
                                                <div class="col text-start">
                                                    <asp:Button ID="Button52" runat="server" Text="Add All" CssClass="btn btn-primary" />
                                                    <asp:Button ID="Button50" runat="server" Text="ADD Single Item" CssClass="btn btn-primary" />
                                                    <asp:Button ID="Button51" runat="server" Text="SAVE" Width="80px" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                    <asp:Button ID="Button49" runat="server" Text="CLOSE" Width="80px" CssClass="btn btn-danger" />
                                                </div>

                                            </div>

                                        </div>

                                        <%--=================Right Panel ================================--%>
                                        <div class="col-7 text-start">

                                            <div class="row align-items-center">
                                                <div class="col text-start me-1">

                                                    <asp:Panel ID="Panel4" runat="server" CssClass="w-100">
                                                        <asp:FormView ID="imp_FormView1" CssClass="w-100" runat="server" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" Font-Names="Times New Roman" ForeColor="Black" GridLines="Both" Height="240px">
                                                            <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                            <FooterStyle BackColor="#CCCCCC" />
                                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                            <ItemTemplate>
                                                                <h3 style="font-family: 'Times New Roman', Times, serif; font-size: xx-large; color: #0000FF; margin-top: 0px;"><%# Eval("MAT_NAME")%> </h3>
                                                                <br />
                                                                <asp:Label ID="Label506" runat="server" ForeColor="Blue" Text='<%# Eval("SUPL_NAME")%>'></asp:Label>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="Label507" runat="server" Font-Bold="True" Text="BE No" Width="100"></asp:Label>
                                                                <asp:Label ID="Label508" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label509" runat="server" Text='<%# Eval("BE_NO")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label510" runat="server" Font-Bold="True" Text="Mat. Code" Width="100"></asp:Label>
                                                                <asp:Label ID="Label511" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label512" runat="server" Text='<%# Eval("MAT_CODE")%>' Width="120"></asp:Label>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="Label513" runat="server" Font-Bold="True" Text="PO. No" Width="100"></asp:Label>
                                                                <asp:Label ID="Label514" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label515" runat="server" Text='<%# Eval("PO_NO")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="AMD. No" Width="100"></asp:Label>
                                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("amd_no")%>' Width="120"></asp:Label>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="Label516" runat="server" Font-Bold="True" Text="Trans WO." Width="100"></asp:Label>
                                                                <asp:Label ID="Label517" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label518" runat="server" Text='<%# Eval("TRANS_WO_NO")%>' Width="120"></asp:Label>

                                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="CHA. WO." Width="100"></asp:Label>
                                                                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label7" runat="server" Text='<%# Eval("CHA_ORDER")%>' Width="120"></asp:Label>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="Label519" runat="server" Font-Bold="True" Text="BE Qty" Width="100"></asp:Label>
                                                                <asp:Label ID="Label520" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label521" runat="server" Text='<%# Eval("BE_QTY")%>' Width="120"></asp:Label>
                                                                <asp:Label ID="Label522" runat="server" Font-Bold="True" Text="BE Bal. Qty" Width="100"></asp:Label>
                                                                <asp:Label ID="Label523" runat="server" Font-Bold="True" Text=":"></asp:Label>
                                                                <asp:Label ID="Label524" runat="server" Text='<%# Eval("BE_BAL")%>' Width="120"></asp:Label>
                                                                <br />
                                                            </ItemTemplate>
                                                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                            <RowStyle BackColor="White" />
                                                        </asp:FormView>

                                                    </asp:Panel>

                                                </div>

                                            </div>

                                            <div class="row align-items-center mt-2">
                                                <div class="col-3 text-start">
                                                    <asp:Label ID="Label30" runat="server" ForeColor="Blue" Text="Trans Penalty"></asp:Label>
                                                </div>
                                                <div class="col-9 text-start">
                                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server">0</asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col-3 text-start">
                                                    <asp:Label ID="Label34" runat="server" ForeColor="Blue" Text="Remarks"></asp:Label>
                                                </div>
                                                <div class="col-9 text-start">
                                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row align-items-center">
                                                <div class="col text-start">
                                                    <asp:Label ID="Label534" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                </div>

                                            </div>

                                        </div>
                                        

                                    </div>
                                    <div class="row align-items-center">
                                            <div class="col m-3" style="overflow: scroll">
                                            <asp:Panel ID="imp_Panel11" runat="server" Style="text-align: left">
                                                <asp:GridView ID="imp_GridView3" Width="200%" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
                                                
                                                    <Columns>
                                                        <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
                                                        <asp:BoundField DataField="PO_NO" HeaderText="PO No" />
                                                        <asp:BoundField DataField="AMD_NO" HeaderText="AMD No" />
                                                        <asp:BoundField DataField="MAT_SLNO" HeaderText="MAT SLNo" />
                                                        <asp:BoundField DataField="MAT_CODE" HeaderText="MAT. Code" />
                                                        <asp:BoundField DataField="MAT_NAME" HeaderText="MAT. Name" />
                                                        <asp:BoundField DataField="MAT_AU" HeaderText="A/U" />
                                                        <asp:BoundField DataField="MAT_QTY" HeaderText="ORD. Qty." />
                                                        <asp:BoundField DataField="CHLN_NO" HeaderText="CHLN No" />
                                                        <asp:BoundField DataField="MAT_CHALAN_QTY" HeaderText="CHLN. Qty." />
                                                        <asp:BoundField DataField="MAT_RCD_QTY" HeaderText="RCVD. Qty." />
                                                        <asp:BoundField DataField="MAT_REJ_QTY" HeaderText="REJ. Qty." />
                                                        <asp:TemplateField HeaderText="Accept Qty."></asp:TemplateField>
                                                        <asp:BoundField DataField="MAT_EXCE" HeaderText="Excess Qty." />
                                                        <asp:BoundField DataField="MAT_BAL_QTY" HeaderText="BAL. Qty." />
                                                        <asp:TemplateField HeaderText="Unit Rate"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CENVAT"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mat Value"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CHA Charge"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Transport Charge"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shortage Amount"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Loss On ED"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Wt Var Val"></asp:TemplateField>
                                                        <asp:BoundField DataField="INSP_NOTE" HeaderText="Remarks" />
                                                        <asp:BoundField DataField="BE_NO" HeaderText="BE NO" />
                                                        <asp:TemplateField HeaderText="Sit Value"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Custom Duty"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Statutory Charges"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insurance"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BE Excess"></asp:TemplateField>
                                                        <%--<asp:BoundField DataField="TOTAL_MT" HeaderText="Gross Weight" />--%>

                                                    </Columns>
                                                    <%--<FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                    <HeaderStyle BackColor="#990000" Font-Bold="True" Font-Size="XX-Small" ForeColor="#FFFFCC" />
                                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />--%>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>

                                </asp:Panel>
                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>


            </div>
        </div>
    </div>


</asp:Content>
