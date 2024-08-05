<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="crr.aspx.vb" Inherits="PLANT_DETAILS.crr" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            <asp:Label ID="Label1" runat="server" Text="Store Material CRR" Font-Bold="True" Font-Size="Larger"></asp:Label>
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
                        <asp:Panel ID="Panel16" runat="server" BorderColor="#FFFF66" BorderStyle="Groove">
                            <div class="row justify-content-center align-items-center mt-2">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label459" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Type"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList ID="type_DropDown" runat="server" AutoPostBack="True" CssClass="form-select">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row justify-content-center align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="form-select">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="row justify-content-center align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label460" runat="server" Font-Bold="True" Font-Names="Times New Roman" ForeColor="Blue" Text="CRR No"></asp:Label>
                                </div>
                                <div class="col-6 text-start">
                                    <asp:DropDownList ID="search_DropDown" runat="server" AutoPostBack="True" CssClass="form-select">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="row justify-content-center align-items-center mt-2 mb-2">
                                <div class="col-2 text-start">
                                </div>
                                <div class="col-6 text-start">
                                    <asp:Button ID="new_button" runat="server" class="btn btn-primary fw-bold" Text="NEW" />
                                    <asp:Button ID="view_button" runat="server" class="btn btn-primary fw-bold" Text="VIEW" />

                                </div>
                            </div>

                        </asp:Panel>
                    </div>
                </div>

                <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Visible="False">
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label465" runat="server" ForeColor="Blue" Text="CRR No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="crr_TextBox" class="form-control" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-7 text-start">
                            <asp:Label ID="ERRLabel" runat="server" ForeColor="Red" Text="Label" Visible="False" Font-Bold="True"></asp:Label>
                        </div>
                    </div>
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label22" runat="server" ForeColor="Blue" Text="PO No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="pono_DropDownList" CssClass="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label24" runat="server" ForeColor="Blue" Text="Supplier"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox ID="suplnameTextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Transporter"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="trans_DropDownList" CssClass="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label10" runat="server" ForeColor="Blue" Text="Trans Name"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox ID="transnameTextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="WO SL No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="DropDownList21" CssClass="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end g-0">
                            <asp:Label ID="Label11" runat="server" ForeColor="Blue" Text="Work Name"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox ID="TextBox174" CssClass="form-control" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>

                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label25" runat="server" ForeColor="Blue" Text="Truck No."></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="lr_rr_noTextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label26" runat="server" ForeColor="Blue" Text="Ship Name"></asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:TextBox ID="shipnameTextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>

                        </div>
                    </div>



                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="Mat SLNo"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="mat_sl_noDropDownList" CssClass="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label12" runat="server" ForeColor="Blue" Text="Mat Name"></asp:Label>
                        </div>
                        <div class="col-4">
                            <asp:TextBox ID="matnameTextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Enabled="False" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>

                        </div>
                        <div class="col-1 text-end">
                            <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="A/U"></asp:Label>

                        </div>
                        <div class="col-1 text-end">
                            <asp:TextBox ID="au_TextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label13" runat="server" ForeColor="Blue" Text="BE No."></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="be_TextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman" ForeColor="Black">N/A</asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label18" runat="server" ForeColor="Blue" Text="BE Date"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="bedate_TextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Enabled="False" Font-Names="Times New Roman" ForeColor="White">N/A</asp:TextBox>

                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label6" runat="server" ForeColor="Blue" Text="BL No."></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="bl_noTextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman" ForeColor="Black">N/A</asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label14" runat="server" ForeColor="Blue" Text="BL Date"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="bldate_TextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Enabled="False" Font-Names="Times New Roman" ForeColor="White">N/A</asp:TextBox>

                        </div>
                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="Chalan No."></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="chalan_TextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman" ForeColor="Black"></asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label15" runat="server" ForeColor="Blue" Text="Chalan Date"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="chalandate_TextBox" CssClass="form-control" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="chalandate_TextBox_CalendarExtender" runat="server" BehaviorID="chalandate_TextBox_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="chalandate_TextBox" />
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label8" runat="server" ForeColor="Blue" Text="Chalan Qty."></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="chalan_qty_TextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman" ForeColor="Black"></asp:TextBox>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label16" runat="server" ForeColor="Blue" Text="Rcvd Qty"></asp:Label>
                        </div>
                        <div class="col-2">
                            <asp:TextBox ID="rcv_qty_TextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="No of Bags:-"></asp:Label>
                        </div>
                        <div class="col-1">
                            <asp:TextBox ID="no_of_bagTextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman">0</asp:TextBox>
                        </div>
                        <div class="col-1 g-0">
                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue" Text="Total Bag Weight(Mt):-"></asp:Label>
                        </div>
                        <div class="col-1">

                            <asp:TextBox ID="bag_weightTextBox" CssClass="form-control" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                        </div>
                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label9" runat="server" ForeColor="Blue" Text="Total Weight(MT)"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="Actual_Material_Weight" CssClass="form-control" runat="server" Font-Names="Times New Roman" ForeColor="Black">0</asp:TextBox>
                        </div>
                        <div class="col-7 text-end">
                            <asp:Button ID="crr_add_Button" runat="server" Text="ADD" CssClass="btn btn-primary" Font-Size="Small" Width="85px" />
                        </div>

                    </div>

                    <div class="row align-items-center m-1">

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


</asp:Content>
<%--============================================--%>

