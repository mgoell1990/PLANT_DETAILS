<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="inv_entry.aspx.vb" Inherits="PLANT_DETAILS.inv_entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" Style="text-align: center" ForeColor="Blue"></asp:Label>
        </div>
    </section>
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script>

        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=pay_supl_code_TextBox98]").autocomplete({
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

    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>


    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Invoice Entry" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center">
                    <div class="col-10 text-center">
                        <asp:Panel ID="Panel16" runat="server" BorderColor="Blue" BorderStyle="Groove">

                            <div class="row align-items-center mt-2">
                                <div class="col-2 text-end">
                                    <asp:Label ID="Label71" runat="server" Text="Bill Tracking Id" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox ID="TextBox28" class="form-control" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-6 text-end">
                                </div>

                            </div>

                            <div class="row align-items-center mt-0">
                                <div class="col-2 text-end">
                                    <asp:Label ID="Label8" runat="server" Text="Inv Type" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:DropDownList ID="DropDownList3" class="form-select" runat="server" AutoPostBack="True">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Regular</asp:ListItem>
                                        <asp:ListItem>Miscellaneous</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-6 text-end">
                                </div>

                            </div>

                            <asp:MultiView ID="MultiView1" runat="server">

                                <%--=====VIEW 1 Regular invoive START=====--%>
                                <asp:View ID="View1" runat="server">
                                    <div class="row align-items-center mt-0">
                                        <div class="col-2 text-end">
                                            <asp:Label ID="Label98" runat="server" Text="Inv for" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start">
                                            <asp:DropDownList ID="DropDownList17" class="form-select" runat="server" AutoPostBack="True">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Supplier</asp:ListItem>
                                                <asp:ListItem>Contractor</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-6 text-end">
                                        </div>

                                    </div>

                                    <div class="row align-items-center mt-1">
                                        <div class="col-2 text-end">
                                            <asp:Label ID="Label2" runat="server" Text="Order No" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start">
                                            <asp:DropDownList ID="DropDownList10" class="form-select" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-1 text-end">
                                            <asp:Label ID="Label89" runat="server" Text="Party" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-5 text-start">
                                            <asp:TextBox ID="TextBox42" class="form-control" runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row align-items-center mt-1">

                                        <div class="col-2 text-end">
                                            <asp:Label ID="Label3" runat="server" Text="Advance Voucher No" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start">
                                            <asp:DropDownList ID="DropDownList1" class="form-select" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-1 text-end">
                                            <asp:Label ID="Label4" runat="server" Text="Amount" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:TextBox ID="TextBox1" runat="server" class="form-control" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-end">
                                            <asp:Label ID="Label7" runat="server" Text="Party Type" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:TextBox ID="TextBox3" runat="server" class="form-control" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                        </div>


                                    </div>

                                    <div class="row align-items-center mt-1">

                                        <div class="col-2 text-end">
                                            <asp:Label ID="Label5" runat="server" Text="IOC No Available" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start">
                                            <asp:DropDownList ID="DropDownList2" class="form-select" runat="server" AutoPostBack="True">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>YES</asp:ListItem>
                                                <asp:ListItem>NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-1 text-end">
                                            <asp:Label ID="Label6" runat="server" Text="IOC No" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-5 text-start">
                                            <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </asp:View>
                                <%--=====VIEW 2 Miscellaneous invoive START=====--%>
                                <asp:View ID="View2" runat="server">

                                    <div class="row align-items-center mt-1">
                                        <div class="col-2 text-end">
                                            <asp:Label ID="Label636" runat="server" ForeColor="Blue" Text="Supl Code"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start">
                                            <asp:TextBox class="form-control" ID="pay_supl_code_TextBox98" runat="server" TabIndex="12"></asp:TextBox>
                                        </div>
                                        <div class="col-1 text-end">
                                            <asp:Label ID="Label9" runat="server" Text="PO Number" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-5 text-start">
                                            <asp:TextBox ID="TextBox4" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                </asp:View>
                            </asp:MultiView>




                            <div class="row align-items-center mt-1">

                                <div class="col-2 text-end">
                                    <asp:Label ID="Label68" runat="server" Text="Supplier Invoice No" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox ID="TextBox26" class="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-end">
                                </div>
                                <div class="col-5 text-start">
                                </div>

                            </div>

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-end">
                                    <asp:Label ID="Label69" runat="server" Text="Inv Date" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox ID="TextBox27" runat="server" class="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox27_CalendarExtender" runat="server" CssClass="red" Format="dd-MM-yyyy" Enabled="True" TargetControlID="TextBox27"></asp:CalendarExtender>
                                </div>
                                <div class="col-1 text-end">
                                    <asp:Label ID="Label72" runat="server" Text="Amt" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-5 text-start">
                                    <asp:TextBox ID="TextBox30" runat="server" class="form-control"></asp:TextBox>
                                </div>

                            </div>



                            <div class="row align-items-center mt-3">
                                <div class="col-2 text-end">
                                </div>
                                <div class="col-4 text-end">
                                    <asp:Button ID="Button12" runat="server" Text="SAVE" class="btn btn-primary fw-bold" Font-Size="Small" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                </div>
                                <div class="col-4 text-start">
                                    <asp:Button ID="Button47" runat="server" class="btn btn-danger fw-bold" Text="CANCEL" Font-Size="Small" />
                                </div>
                                <div class="col-2 text-start">
                                </div>

                            </div>

                            <div class="row align-items-center mt-2">
                                <div class="col-2 text-end">
                                </div>
                                <div class="col-10 text-center">
                                    <asp:Label ID="Label552" runat="server" ForeColor="Red"></asp:Label>
                                </div>

                                <div class="col-2 text-start">
                                </div>

                            </div>

                        </asp:Panel>
                    </div>
                </div>

                <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Visible="False">
                </asp:Panel>
            </div>
        </div>
    </div>



</asp:Content>
