<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="mat_crr.aspx.vb" Inherits="PLANT_DETAILS.mat_crr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
        <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <%--<link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />--%>

    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $("[id$=be_TextBox]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/BE")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    bedate: item.split('^')[1],
                                    blno: item.split('^')[2],
                                    bldate: item.split('^')[3],
                                    shipname: item.split('^')[4]
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
                    $("[id$=bedate_TextBox]").val(i.item.bedate);
                    $("[id$=bl_noTextBox]").val(i.item.blno);
                    $("[id$=bldate_TextBox]").val(i.item.bldate);
                    $("[id$=shipnameTextBox]").val(i.item.shipname);
                },
                minLength: 1
            });
        });
    </script>

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Raw Material CRR" Font-Bold="True" Font-Size="Larger"></asp:Label>
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


    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center">
                    <div class="col-6 text-center">
                        <asp:Panel ID="Panel2" runat="server" BorderColor="#FFFF66" BorderStyle="Groove">
                            <div class="row justify-content-center align-items-center mt-2">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label459" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Type"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList CssClass="form-select" ID="type_DropDown" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row justify-content-center align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList CssClass="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="row justify-content-center align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label460" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CRR No"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList CssClass="form-select" ID="search_DropDown" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="row justify-content-center align-items-center mt-2 mb-2">
                                <div class="col-2 text-start">
                                </div>
                                <div class="col-6 text-start">
                                    <asp:Button ID="new_button" runat="server" class="btn btn-primary fw-bold" Text="New" />
                                    <asp:Button ID="view_button" runat="server" class="btn btn-primary fw-bold" Text="View" />
                                    <asp:Button ID="Button2" runat="server" Font-Bold="True" Text="CHA CORRECTION" Width="82px" CssClass="bottomstyle" Visible="False" />
                                </div>
                            </div>

                        </asp:Panel>
                    </div>
                </div>

                <asp:Panel ID="Panel3" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Visible="False">
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label465" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="CRR No:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox CssClass="form-control" ID="crr_TextBox" runat="server" BackColor="Red" ForeColor="White"  Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-7">
                            <asp:Label ID="Label467" runat="server" ForeColor="Red" Text="Label" Visible="False" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="PO No:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList CssClass="form-select" ID="pono_DropDownList" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Supplier:"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox CssClass="form-control" ID="suplnameTextBox" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White"  Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">
                        <div class="col-2 text-start">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Transporter"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList CssClass="form-select" ID="trans_DropDownList" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Trans Name:"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox CssClass="form-control" ID="transnameTextBox" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White"  Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label463" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="WO SL No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList CssClass="form-select" ID="DropDownList21" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end g-0">
                            <asp:Label ID="Label464" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Work Name"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox CssClass="form-control" ID="TextBox174" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White"  Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Truck No.:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox CssClass="form-control" ID="lr_rr_noTextBox" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Ship Name:"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox CssClass="form-control" ID="shipnameTextBox" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" ForeColor="White" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                    </div>



                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Mat SLNo:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList CssClass="form-select" ID="mat_sl_noDropDownList" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Mat Name:"></asp:Label>
                        </div>
                        <div class="col-4">
                            <asp:TextBox CssClass="form-control" ID="matnameTextBox" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White"  Style="text-align: justify" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1 text-end">
                            <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="A/U:" Width="40px"></asp:Label>
                        </div>
                        <div class="col-1 text-end">
                            <asp:TextBox CssClass="form-control" ID="au_TextBox" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White"  Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BE No:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox CssClass="form-control" ID="be_TextBox" runat="server" ForeColor="Black" Font-Names="Times New Roman">N/A</asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BE Date:"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox CssClass="form-control" ID="bedate_TextBox" runat="server" BackColor="#4686F0" Enabled="False" ForeColor="White" Font-Names="Times New Roman">N/A</asp:TextBox>
                            <asp:Label ID="ERRLabel" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BL No:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox CssClass="form-control" ID="bl_noTextBox" runat="server" BackColor="#4686F0" Enabled="False" ForeColor="White" Font-Names="Times New Roman">N/A</asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="BL Date:"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox CssClass="form-control" ID="bldate_TextBox" runat="server" BackColor="#4686F0" Enabled="False" ForeColor="White" Font-Names="Times New Roman">N/A</asp:TextBox>
                        </div>
                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Chalan No:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox CssClass="form-control" ID="chalan_TextBox" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Chalan Dt.:"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox CssClass="form-control" ID="chalandate_TextBox" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                            <cc1:CalendarExtender ID="chalandate_TextBox_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="chalandate_TextBox_CalendarExtender" TargetControlID="chalandate_TextBox" />
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Gross Chalan Qty:"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox CssClass="form-control" ID="chalan_qty_TextBox" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Gross Rcvd Qty:"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox CssClass="form-control" ID="rcv_qty_TextBox" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="No of Bags:"></asp:Label>
                        </div>
                        <div class="col-1">
                            <asp:TextBox CssClass="form-control" ID="no_of_bagTextBox" runat="server" Font-Names="Times New Roman">0</asp:TextBox>
                        </div>
                        <div class="col-1 g-0">
                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Total Bag Weight(Mt):"></asp:Label>
                        </div>
                        <div class="col-1">
                            <asp:TextBox CssClass="form-control" ID="bag_weightTextBox" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label466" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Net Rcd Qty(Mt)"></asp:Label>

                        </div>
                        <div class="col-3">
                            <asp:TextBox CssClass="form-control" ID="net_rcd_qty" runat="server" Font-Names="Times New Roman" >0</asp:TextBox>
                        </div>
                        <div class="col-7 text-end">
                            <asp:Label ID="Label22" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                        </div>

                    </div>
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Test Location"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList CssClass="form-select" ID="test_location_DropDownList" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>SRU</asp:ListItem>
                                <asp:ListItem>Outside SRU</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-7 text-end">
                            <asp:Button ID="crr_add_Button" runat="server" Text="Add" CssClass="btn btn-primary" Font-Size="Small" Width="85px" />
                        </div>

                    </div>

                    <div class="row align-items-center m-1 mt-2">

                        <%--<asp:Panel ID="Panel11" runat="server" ScrollBars="Auto">
                            <asp:GridView ID="crr_gridview" runat="server" Font-Size="Small" ShowHeaderWhenEmpty="True" Width="200%" CssClass="mGrid" Font-Names="Times New Roman">
                                <RowStyle Height="25px" />
                            </asp:GridView>
                        </asp:Panel>--%>

                        <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto">
                            <asp:GridView ID="crr_gridview" runat="server" BackColor="White" BorderColor="Gray"
                                BorderStyle="Groove" BorderWidth="1px"
                                CellPadding="3" ForeColor="Black" GridLines="Both" Style="text-align: center; width: 150%; height: 75px" class="table table-bordered text-center">
                                <RowStyle Height="35px" />
                                <FooterStyle BackColor="#cccc99" />
                                <HeaderStyle BackColor="#296DA9" Font-Bold="true" ForeColor="white" />
                                <RowStyle BackColor="#f7f7de" />
                                <SelectedRowStyle BackColor="#ce5d5a" Font-Bold="true" ForeColor="white" />
                                <SortedAscendingCellStyle BackColor="#fbfbf2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#eaead3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </asp:Panel>

                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-5 text-end">
                            <asp:Button ID="crr_save_Button" runat="server" Text="Save" CssClass="btn btn-success fw-bold" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />

                        </div>
                        <div class="col-1 text-center">
                            <asp:Button ID="crr_cancel_Button" runat="server" Font-Bold="True" Text="Cancel" CssClass="btn btn-danger fw-bold" />
                        </div>
                        <div class="col-6 text-start">
                            <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-danger fw-bold"></asp:Button>
                        </div>

                    </div>
                </asp:Panel>
            </div>

        </div>
    </div>



    <%--============================================--%>

</asp:Content>
