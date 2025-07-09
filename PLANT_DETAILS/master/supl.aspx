<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="supl.aspx.vb" Inherits="PLANT_DETAILS.supl" %>

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
            $("[id$=TextBox84]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/SUPL_DETAILS")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    SUPL_NAME: item.split('^')[1],
                                    SUPL_CONTACT_PERSON: item.split('^')[2],
                                    SUPL_AT: item.split('^')[3],
                                    SUPL_PO: item.split('^')[4],
                                    SUPL_DIST: item.split('^')[5],
                                    SUPL_PIN: item.split('^')[6],
                                    SUPL_STATE: item.split('^')[7],
                                    SUPL_COUNTRY: item.split('^')[8],
                                    SUPL_MOB1: item.split('^')[9],
                                    SUPL_MOB2: item.split('^')[10],
                                    SUPL_LAND: item.split('^')[11],
                                    SUPL_FAX: item.split('^')[12],
                                    SUPL_EMAIL: item.split('^')[13],
                                    SUPL_WEB: item.split('^')[14],
                                    SUPL_PAN: item.split('^')[15],
                                    SUPL_TIN: item.split('^')[16],
                                    SUPL_ST_NO: item.split('^')[17],
                                    SUPL_BANK: item.split('^')[18],
                                    SUPL_ACOUNT_NO: item.split('^')[19],
                                    SUPL_IFSC: item.split('^')[20],
                                    SUPL_TYPE: item.split('^')[21],
                                    SUPL_GST_NO: item.split('^')[24],
                                    SUPL_STATE_CODE: item.split('^')[25],
                                    PARTY_TYPE: item.split('^')[26],
                                    MSME_NO: item.split('^')[27],
                                    SUPL_STATUS: item.split('^')[28],
                                    SUPL_VALIDITY: item.split('^')[29]

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
                    $("[id$=TextBox85]").val(i.item.SUPL_NAME);
                    $("[id$=TextBox86]").val(i.item.SUPL_CONTACT_PERSON);
                    $("[id$=SUPLDropDownList17]").val(i.item.SUPL_TYPE);
                    $("[id$=TextBox99]").val(i.item.SUPL_AT);
                    $("[id$=TextBox100]").val(i.item.SUPL_PO);
                    $("[id$=TextBox101]").val(i.item.SUPL_DIST);
                    $("[id$=TextBox102]").val(i.item.SUPL_PIN);
                    $("[id$=DropDownList1]").val(i.item.SUPL_STATE);
                    $("[id$=TextBox104]").val(i.item.SUPL_COUNTRY);
                    $("[id$=TextBox93]").val(i.item.SUPL_MOB1);
                    $("[id$=TextBox94]").val(i.item.SUPL_MOB2);
                    $("[id$=TextBox95]").val(i.item.SUPL_LAND);
                    $("[id$=TextBox105]").val(i.item.SUPL_FAX);
                    $("[id$=TextBox106]").val(i.item.SUPL_EMAIL);
                    $("[id$=TextBox107]").val(i.item.SUPL_WEB);
                    $("[id$=TextBox108]").val(i.item.SUPL_BANK);
                    $("[id$=TextBox109]").val(i.item.SUPL_ACOUNT_NO);
                    $("[id$=TextBox110]").val(i.item.SUPL_IFSC);
                    $("[id$=TextBox111]").val(i.item.SUPL_PAN);
                    $("[id$=TextBox112]").val(i.item.SUPL_GST_NO);
                    $("[id$=TextBox113]").val(i.item.SUPL_STATE_CODE);
                    $("[id$=DropDownList2]").val(i.item.PARTY_TYPE);
                    $("[id$=TextBox1]").val(i.item.MSME_NO);
                    $("[id$=DropDownList3]").val(i.item.SUPL_STATUS);
                    $("[id$=TextBox2]").val(i.item.SUPL_VALIDITY);
                },
                minLength: 1
            });
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox85]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/SUPL_DETAILS")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[1],
                                    SUPL_NAME: item.split('^')[0],
                                    SUPL_CONTACT_PERSON: item.split('^')[2],
                                    SUPL_AT: item.split('^')[3],
                                    SUPL_PO: item.split('^')[4],
                                    SUPL_DIST: item.split('^')[5],
                                    SUPL_PIN: item.split('^')[6],
                                    SUPL_STATE: item.split('^')[7],
                                    SUPL_COUNTRY: item.split('^')[8],
                                    SUPL_MOB1: item.split('^')[9],
                                    SUPL_MOB2: item.split('^')[10],
                                    SUPL_LAND: item.split('^')[11],
                                    SUPL_FAX: item.split('^')[12],
                                    SUPL_EMAIL: item.split('^')[13],
                                    SUPL_WEB: item.split('^')[14],
                                    SUPL_PAN: item.split('^')[15],
                                    SUPL_TIN: item.split('^')[16],
                                    SUPL_ST_NO: item.split('^')[17],
                                    SUPL_BANK: item.split('^')[18],
                                    SUPL_ACOUNT_NO: item.split('^')[19],
                                    SUPL_IFSC: item.split('^')[20],
                                    SUPL_TYPE: item.split('^')[21],
                                    SUPL_GST_NO: item.split('^')[24],
                                    SUPL_STATE_CODE: item.split('^')[25],
                                    PARTY_TYPE: item.split('^')[26],
                                    MSME_NO: item.split('^')[27],
                                    SUPL_STATUS: item.split('^')[28],
                                    SUPL_VALIDITY: item.split('^')[29]

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
                    $("[id$=TextBox84]").val(i.item.SUPL_NAME);
                    $("[id$=TextBox86]").val(i.item.SUPL_CONTACT_PERSON);
                    $("[id$=SUPLDropDownList17]").val(i.item.SUPL_TYPE);
                    $("[id$=TextBox99]").val(i.item.SUPL_AT);
                    $("[id$=TextBox100]").val(i.item.SUPL_PO);
                    $("[id$=TextBox101]").val(i.item.SUPL_DIST);
                    $("[id$=TextBox102]").val(i.item.SUPL_PIN);
                    $("[id$=DropDownList1]").val(i.item.SUPL_STATE);
                    $("[id$=TextBox104]").val(i.item.SUPL_COUNTRY);
                    $("[id$=TextBox93]").val(i.item.SUPL_MOB1);
                    $("[id$=TextBox94]").val(i.item.SUPL_MOB2);
                    $("[id$=TextBox95]").val(i.item.SUPL_LAND);
                    $("[id$=TextBox105]").val(i.item.SUPL_FAX);
                    $("[id$=TextBox106]").val(i.item.SUPL_EMAIL);
                    $("[id$=TextBox107]").val(i.item.SUPL_WEB);
                    $("[id$=TextBox108]").val(i.item.SUPL_BANK);
                    $("[id$=TextBox109]").val(i.item.SUPL_ACOUNT_NO);
                    $("[id$=TextBox110]").val(i.item.SUPL_IFSC);
                    $("[id$=TextBox111]").val(i.item.SUPL_PAN);
                    $("[id$=TextBox112]").val(i.item.SUPL_GST_NO);
                    $("[id$=TextBox113]").val(i.item.SUPL_STATE_CODE);
                    $("[id$=DropDownList2]").val(i.item.PARTY_TYPE);
                    $("[id$=TextBox1]").val(i.item.MSME_NO);
                    $("[id$=DropDownList3]").val(i.item.SUPL_STATUS);
                    $("[id$=TextBox2]").val(i.item.SUPL_VALIDITY);

                },
                minLength: 1
            });
        });
    </script>


    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Add/Update Supplier Details" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-2 justify-content-center text-center">
            <div class="col-10" style="border: 5px groove #FF0066; float: left; text-align: left;">
                <div class="row">
                    <div class="col-8" style="border-right: 5px groove #FF0066;">
                        <div class="row align-items-center mt-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label307" runat="server" ForeColor="Blue" Text="Supl Code"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox class="form-control" ID="TextBox84" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label308" runat="server" ForeColor="Blue" Text="Company Name"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox class="form-control" ID="TextBox85" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label309" runat="server" ForeColor="Blue" Text="Contact Person"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox class="form-control" ID="TextBox86" runat="server"></asp:TextBox>
                            </div>
                        </div>



                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label310" runat="server" ForeColor="Blue" Text="Supl Type"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:DropDownList class="form-select" ID="SUPLDropDownList17" runat="server" AutoPostBack="True">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Within State</asp:ListItem>
                                    <asp:ListItem>Out Side State</asp:ListItem>
                                    <asp:ListItem>Foreign</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>



                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Party Type"></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:DropDownList class="form-select" ID="DropDownList2" runat="server" AutoPostBack="True">
                                    <asp:ListItem>NA</asp:ListItem>
                                    <asp:ListItem>MSME</asp:ListItem>
                                    <asp:ListItem>SSI</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </div>

                            <div class="col-2 text-end">
                                <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="MSME NO."></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:TextBox class="form-control" ID="TextBox1" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label9" runat="server" ForeColor="Blue" Text="Status"></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:DropDownList class="form-select" ID="DropDownList3" runat="server" AutoPostBack="True">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>ACTIVE</asp:ListItem>
                                    <asp:ListItem>IN-ACTIVE</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </div>

                            <div class="col-2 text-end">
                                <asp:Label ID="Label10" runat="server" ForeColor="Blue" Text="Validity"></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox2" />
                            </div>
                        </div>



                    </div>

                    <div class="col-4 align-top">

                        <div class="row mt-2 align-items-center">
                            <div class="col-5 text-end">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True">Password : </asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox class="form-control" ID="TextBox114" runat="server" TextMode="Password"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-5 text-end">
                            </div>
                            <div class="col-7 text-start mt-2">
                                <asp:Button ID="Button42" runat="server" class="btn btn-primary fw-bold" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                <asp:Button ID="Button43" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col text-start">
                                <asp:Label ID="ERR_LABLE" runat="server" Font-Size="Medium" ForeColor="Red" Text="err" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="row align-items-center justify-content-center mt-1" style="background-color: #4686F0">
                    <div class="col-4 text-center">
                        <asp:Label ID="Label399" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Address Details"></asp:Label>
                    </div>
                    <div class="col-4 text-center">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Contact Details"></asp:Label>
                    </div>
                    <div class="col-4 text-center">
                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Bank Details"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-8" style="border-right: 5px groove #FF0066;">
                        <div class="row align-items-center mt-1">
                            <div class="col-6 text-end">
                                <div class="row align-items-center">
                                    <div class="col-3 text-end g-0">
                                        <asp:Label ID="Label335" runat="server" ForeColor="Blue" Text="Address 1"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox99" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 text-start">
                                <div class="row align-items-center">
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label324" runat="server" ForeColor="Blue" Text="Mobile No"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox93" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-6 text-end">
                                <div class="row align-items-center">
                                    <div class="col-3 text-end g-0">
                                        <asp:Label ID="Label336" runat="server" ForeColor="Blue" Text="Address 2"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox100" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 text-start">
                                <div class="row align-items-center">
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label325" runat="server" ForeColor="Blue" Text="Mobile No"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox94" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-6 text-end">
                                <div class="row align-items-center">
                                    <div class="col-3 text-end g-0">
                                        <asp:Label ID="Label337" runat="server" ForeColor="Blue" Text="District"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox101" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 text-start">
                                <div class="row align-items-center">
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label326" runat="server" ForeColor="Blue" Text="Office No"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox95" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-6 text-end">
                                <div class="row align-items-center">
                                    <div class="col-3 text-end g-0">
                                        <asp:Label ID="Label338" runat="server" ForeColor="Blue" Text="PIN Code"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox102" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 text-start">
                                <div class="row align-items-center">
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label341" runat="server" ForeColor="Blue" Text="FAX No"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox105" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-6 text-end">
                                <div class="row align-items-center">
                                    <div class="col-3 text-end">
                                        <asp:Label ID="Label339" runat="server" ForeColor="Blue" Text="State"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>ANDHRA PRADESH</asp:ListItem>
                                            <asp:ListItem>ARUNACHAL PRADESH</asp:ListItem>
                                            <asp:ListItem>ASSAM</asp:ListItem>
                                            <asp:ListItem>BIHAR</asp:ListItem>
                                            <asp:ListItem>CHHATTISGARH</asp:ListItem>
                                            <asp:ListItem>GOA</asp:ListItem>
                                            <asp:ListItem>GUJARAT</asp:ListItem>
                                            <asp:ListItem>HARYANA</asp:ListItem>
                                            <asp:ListItem>HIMACHAL PRADESH</asp:ListItem>
                                            <asp:ListItem>JAMMU & KASHMIR</asp:ListItem>
                                            <asp:ListItem>JHARKHAND</asp:ListItem>
                                            <asp:ListItem>KARNATAKA</asp:ListItem>
                                            <asp:ListItem>KERALA</asp:ListItem>
                                            <asp:ListItem>MADHYA PRADESH</asp:ListItem>
                                            <asp:ListItem>MAHARASHTRA</asp:ListItem>
                                            <asp:ListItem>MANIPUR</asp:ListItem>
                                            <asp:ListItem>MEGHALAYA</asp:ListItem>
                                            <asp:ListItem>MIZORAM</asp:ListItem>
                                            <asp:ListItem>NAGALAND</asp:ListItem>
                                            <asp:ListItem>NEW DELHI</asp:ListItem>
                                            <asp:ListItem>ODISHA</asp:ListItem>
                                            <asp:ListItem>PUNJAB</asp:ListItem>
                                            <asp:ListItem>RAJASTHAN</asp:ListItem>
                                            <asp:ListItem>SIKKIM</asp:ListItem>
                                            <asp:ListItem>TAMILNADU</asp:ListItem>
                                            <asp:ListItem>TELANGANA</asp:ListItem>
                                            <asp:ListItem>TRIPURA</asp:ListItem>
                                            <asp:ListItem>UTTARAKHAND</asp:ListItem>
                                            <asp:ListItem>UTTAR PRADESH</asp:ListItem>
                                            <asp:ListItem>WEST BENGAL</asp:ListItem>
                                            <asp:ListItem>OUTSIDE INDIA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 text-start">
                                <div class="row align-items-center">
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label342" runat="server" ForeColor="Blue" Text="Email Id"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox106" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row align-items-center mt-1">
                            <div class="col-6 text-end">
                                <div class="row align-items-center">
                                    <div class="col-3 text-end">
                                        <asp:Label ID="Label340" runat="server" ForeColor="Blue" Text="Country"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox104" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-6 text-start">
                                <div class="row align-items-center">
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label343" runat="server" ForeColor="Blue" Text="Web Site"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox107" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-4 align-top">

                        <div class="row mt-1 align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label344" runat="server" ForeColor="Blue" Text="Bank Name"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox108" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label345" runat="server" ForeColor="Blue" Text="Account No"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox109" runat="server"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label346" runat="server" ForeColor="Blue" Text="IFSC Code"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox110" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center justify-content-center mt-1" style="background-color: #4686F0">
                            <div class="col text-center">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Registration Details"></asp:Label>
                            </div>

                        </div>

                        <div class="row align-items-center mt-1">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label347" runat="server" ForeColor="Blue" Text="PAN No"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox111" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label348" runat="server" ForeColor="Blue" Text="GST No"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox112" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label349" runat="server" ForeColor="Blue" Text="State Code"></asp:Label>
                            </div>
                            <div class="col text-start">
                                <asp:TextBox class="form-control" ID="TextBox113" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col text-start">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>



</asp:Content>
