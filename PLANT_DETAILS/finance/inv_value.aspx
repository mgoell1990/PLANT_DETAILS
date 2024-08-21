<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="inv_value.aspx.vb" Inherits="PLANT_DETAILS.inv_value" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function Validate_Checkbox() {

            var chks = document.getElementsByTagName('input');
            var hasChecked = false;
            for (var i = 0; i < chks.length; i++) {
                if (chks[i].checked) {
                    hasChecked = true;
                    break;
                }
            }
            if (hasChecked == false) {
                alert("Please select at least one row!!");

                return false;
            }

            return true;
        }
    </script>

    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox45]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head")%>',
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
            $("[id$=TextBox61]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head")%>',
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
            $("[id$=TextBox59]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ac_head")%>',
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
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label94" runat="server" Text="Invoice Valuation" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:CalendarExtender ID="TextBox32_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:Panel ID="Panel30" runat="server">
                    <div class="row justify-content-center">
                        <div class="col-9 justify-content-center m-1" style="border-color: #FF3399; border-style: groove">

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label571" runat="server" ForeColor="Blue" Text="Token No"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:DropDownList class="form-select" ID="DropDownList40" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label581" runat="server" ForeColor="Blue" Text="Order No"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox171" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>

                                <div class="col-1 text-start">
                                    <asp:Label ID="Label578" runat="server" Font-Bold="False" ForeColor="Blue" Text="Party"></asp:Label>
                                </div>
                                <div class="col-5 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox168" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>


                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label568" runat="server" Font-Bold="False" ForeColor="Blue" Text="Inv. No"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox160" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label569" runat="server" Font-Bold="False" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-5 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox161" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label77" runat="server" Text="Adv. Voucher/IOC No" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox57" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row  align-items-center mt-1">
                                <div class="col text-start">
                                    <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-2 mb-2">
                                <div class="col-2 text-start">
                                </div>
                                <div class="col text-end">
                                    <asp:Button ID="Button55" runat="server" class="btn btn-primary fw-bold" Text="Next" />

                                </div>
                            </div>
                           
                        </div>
                    </div>
                </asp:Panel>
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 MB START=====--%>
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center">
                            <div class="col">
                                <div class="row" style="border: 3px; border-style: Groove; border-color: #FF3399">
                                    <%--=================Left Panel ================================--%>
                                    <div class="col-4 text-start" style="border-right: 3px; border-right-style: groove; border-right-color: #FF3399">
                                        <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label399" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="MB Valuation"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label401" runat="server" Font-Bold="True" ForeColor="Blue" Text="M.B. No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList class="form-select" ID="garn_crrnoDropDownList" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label373" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work SLNo"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList class="form-select" ID="DropDownList6" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-2" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label101" runat="server" Font-Bold="True" ForeColor="White" Text="Details"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label397" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label398" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label421" runat="server" Font-Bold="True" ForeColor="Blue" Text="AMD No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label422" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label395" runat="server" ForeColor="Blue" Text="SUPL Name" Font-Bold="True"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label396" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col text-start">
                                                <asp:Label ID="GARN_ERR_LABLE" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col text-center">
                                                <asp:Button ID="Button49" runat="server" CssClass="btn btn-primary" Text="Add" />
                                                <asp:Button ID="Button52" runat="server" CssClass="btn btn-success" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="Button51" runat="server" CssClass="btn btn-danger" Text="Close" />
                                            </div>
                                        </div>

                                        <asp:Panel ID="Panel37" runat="server" BackColor="#CCFFFF" Visible="False">
                                            <div class="row justify-content-center" style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                                                <div class="col text-start">

                                                    <div class="row align-items-center mt-1">
                                                        <div class="col text-center">
                                                            <asp:Label ID="Label618" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: center" Text="ARE YOU SURE?"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row align-items-center mt-1">
                                                        <div class="col-4 text-start">
                                                            <asp:Label ID="Label42" runat="server" Font-Bold="True" ForeColor="Blue" Text="Enter Password"></asp:Label>
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:TextBox class="form-control" ID="TextBox172" runat="server" TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center mt-1">
                                                        <div class="col-4 text-start">
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:Button ID="Button57" runat="server" Font-Bold="True" ForeColor="White" Text="Submit" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center mb-1">
                                                        <div class="col-4 text-start">
                                                        </div>
                                                        <div class="col-1 text-start">
                                                            <asp:Label ID="Label43" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>



                                    </div>
                                    <%--=================Right Panel ================================--%>
                                    <div class="col-8">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col-6">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label112" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="As per Order"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-center">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label113" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="As per Valuation"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label404" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-8 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox120" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label405" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox147" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label406" runat="server" Font-Bold="True" Text="Discount%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox124" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label117" runat="server" Font-Bold="True" Font-Size="Medium" Text="Discount%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox149" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label408" runat="server" Font-Bold="True" Text="Mat. Rate P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox121" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label562" runat="server" Font-Bold="True" Text="Mat. Rate P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox151" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label563" runat="server" Font-Bold="True" Text="SGST%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox122" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label564" runat="server" Font-Bold="True" Text="SGST%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox153" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label531" runat="server" Font-Bold="True" Text="CGST%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox123" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label565" runat="server" Font-Bold="True" Text="CGST%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox155" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label412" runat="server" Font-Bold="True" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox125" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label566" runat="server" Font-Bold="True" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox157" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label52" runat="server" Font-Bold="True" Text="CESS%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox37" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label53" runat="server" Font-Bold="True" Text="CESS%"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox38" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label416" runat="server" Font-Bold="True" Text="TDS(M-JUNCTION)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox180" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label536" runat="server" Font-Bold="True" Text="T.D.S. %"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox159" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="L.D. Rs"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox2" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label567" runat="server" Font-Bold="True" Text="Penality Rs."></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox143" runat="server"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label626" runat="server" Font-Bold="True" Text="R.C.M."></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:DropDownList class="form-select" ID="DropDownList43" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem Selected="True">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label76" runat="server" Font-Bold="True" Text="ITC"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:DropDownList class="form-select" ID="DropDownList3" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label61" runat="server" Font-Bold="True" Text="Other Deduction(Rs)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox44" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label62" runat="server" Font-Bold="True" Text="Deduction Head"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox45" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Text="GST Payment"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:DropDownList class="form-select" ID="DropDownList4" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col">
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label68" runat="server" Font-Bold="True" Text="TDS SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox51" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label627" runat="server" Font-Bold="True" Text="S.D. %"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox179" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label69" runat="server" Font-Bold="True" Text="TDS CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox52" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label70" runat="server" Font-Bold="True" Text="TDS IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox53" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label82" runat="server" Font-Bold="True" Text="Other Payment"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox60" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label83" runat="server" Font-Bold="True" Text="Payment Head"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox61" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label135" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remarks For Penality:-"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox144" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label136" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remarks For Modification:-"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox145" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col text-start">
                                                <div class="row align-items-center">
                                                    <div class="col text-start">
                                                        <asp:Label ID="Label137" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>


                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center" style="border-bottom: 3px; border-left: 3px; border-right: 3px; border-style: Groove; border-color: #FF3399;">
                                    <div class="col text-center">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label323" runat="server" Font-Bold="True" ForeColor="white" Font-Size="Medium" Text="Job Details"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col" style="overflow: scroll">
                                                <asp:GridView ID="GridView6" Width="300%" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="mb_no" HeaderText="M.B. No" />
                                                        <asp:BoundField DataField="mb_date" HeaderText="M.B. Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="po_no" HeaderText="Order No" />
                                                        <asp:BoundField DataField="wo_slno" HeaderText="Work Sl No" />
                                                        <asp:BoundField DataField="w_name" HeaderText="Job Name" />
                                                        <asp:BoundField DataField="w_au" HeaderText="A/U" />
                                                        <asp:BoundField DataField="from_date" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="to_date" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="work_qty" HeaderText="Work Qty" />
                                                        <asp:BoundField DataField="rqd_qty" HeaderText="Rqd. Qty." />
                                                        <asp:BoundField DataField="bal_qty" HeaderText="Bal. Qty." />
                                                        <asp:BoundField DataField="prov_amt" HeaderText="PROV. S. Charge" />
                                                        <asp:BoundField DataField="mat_rate" HeaderText="PROV. Mat. Rate" />
                                                        <asp:TemplateField HeaderText="Party Payment"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Final Mat. Charge"></asp:TemplateField>
                                                        <asp:BoundField DataField="pen_amt" HeaderText="Penality" />
                                                        <asp:BoundField DataField="sgst" HeaderText="SGST" />
                                                        <asp:BoundField DataField="cgst" HeaderText="CGST" />
                                                        <asp:BoundField DataField="igst" HeaderText="IGST" />
                                                        <asp:BoundField DataField="cess" HeaderText="CESS" />
                                                        <asp:BoundField DataField="sgst_liab" HeaderText="SGST Liab." />
                                                        <asp:BoundField DataField="cgst_liab" HeaderText="CGST Liab." />
                                                        <asp:BoundField DataField="igst_liab" HeaderText="IGST Liab." />
                                                        <asp:BoundField DataField="cess_liab" HeaderText="CESS Liab." />
                                                        <asp:BoundField DataField="it_amt" HeaderText="T.D.S. Tax" />
                                                        <asp:TemplateField HeaderText="S.D."></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                                                        <asp:BoundField DataField="rcm" HeaderText="R.C.M." />
                                                        <asp:TemplateField HeaderText="L.D."></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm CESS"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Other deduction head"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Other deduction"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Other Payment head"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Other Payment"></asp:TemplateField>
                                                        <asp:BoundField HeaderText="SGST%" />
                                                        <asp:BoundField HeaderText="CGST%" />
                                                        <asp:BoundField HeaderText="IGST%" />
                                                        <asp:BoundField HeaderText="CESS%" />
                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </div>
                                </div>


                            </div>
                        </div>
                    </asp:View>
                    <%--=================================== MB View End=====================================--%>

                    <%--=====VIEW 2 GARN VALUATION START=====--%>
                    <asp:View ID="View2" runat="server">
                        <div class="row justify-content-center">
                            <div class="col">
                                <div class="row" style="border: 3px; border-style: Groove; border-color: #FF3399">
                                    <%--=================Left Panel ================================--%>
                                    <div class="col-3 text-start" style="border-right: 3px; border-right-style: groove; border-right-color: #FF3399">
                                        <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="White" Text="Goods Valuation"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label400" runat="server" ForeColor="Blue" Text="GARN No"></asp:Label>

                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList class="form-select" ID="M_garn_crrnoDropDownList" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label596" runat="server" ForeColor="Blue" Text="MAT SLNo"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList class="form-select" ID="M_DropDownList6" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row align-items-center mt-2" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label98" runat="server" Font-Bold="True" ForeColor="White" Text="Details"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label598" runat="server" ForeColor="Blue" Text="P.O. No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="M_Label398" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label599" runat="server" ForeColor="Blue" Text="AMD No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="M_Label422" runat="server" Font-Bold="False" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label600" runat="server" ForeColor="Blue" Text="SUPL Name"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="M_Label396" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label106" runat="server" ForeColor="Blue" Text="MAT Code"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="M_Label392" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label393" runat="server" ForeColor="Blue" Text="MAT Name"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="M_Label394" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label423" runat="server" ForeColor="Blue" Text="Transporter WO"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="M_Label424" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label461" runat="server" ForeColor="Blue" Text="Work Sl No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="M_Label462" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label630" runat="server" ForeColor="Blue" Text="PAN No."></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="lblPANNo" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label92" runat="server" ForeColor="Blue" Text="Order From"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label93" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col text-start">
                                                <asp:Label ID="M_GARN_ERR_LABLE" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col text-center">
                                                <asp:Button ID="M_Button44" runat="server" CssClass="btn btn-primary" Text="Add" />
                                                <asp:Button ID="M_Button5" runat="server" CssClass="btn btn-success" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="M_Button3" runat="server" CssClass="btn btn-danger" Text="Close" />
                                            </div>

                                        </div>

                                        <asp:Panel ID="Panel38" runat="server" BackColor="#CCFFFF" Visible="False">
                                            <div class="row justify-content-center" style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                                                <div class="col text-start">

                                                    <div class="row align-items-center mt-1">

                                                        <div class="col text-center">
                                                            <asp:Label ID="Label95" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: center" Text="ARE YOU SURE?"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row align-items-center mt-1">
                                                        <div class="col-4 text-start">
                                                            <asp:Label ID="Label96" runat="server" Font-Bold="True" ForeColor="Blue" Text="Enter Password"></asp:Label>
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:TextBox class="form-control" ID="TextBox173" runat="server" TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center mt-1">
                                                        <div class="col-4 text-start">
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:Button ID="Button58" runat="server" Font-Bold="True" ForeColor="White" Text="Submit" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center mb-1">
                                                        <div class="col-4 text-start">
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:Label ID="Label621" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                    <%--=================Right Panel ================================--%>
                                    <div class="col-9">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col-6">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label110" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="As per Order"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-center">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label111" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="As per Valuation"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label603" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-8 text-start">
                                                        <asp:TextBox class="form-control" ID="M_TextBox120" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label604" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox147" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label605" runat="server" Font-Bold="True" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox124" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label606" runat="server" Font-Bold="True" Font-Size="Medium" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox149" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="M_Label468" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label607" runat="server" Font-Bold="True" Font-Size="Medium" Text="Packing"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox121" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label409" runat="server" Font-Bold="True" Font-Size="Medium" Text="Packing"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox151" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="M_Label467" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label410" runat="server" Font-Bold="True" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox11" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label411" runat="server" Font-Bold="True" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox12" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox29" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label413" runat="server" Font-Bold="True" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox30" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox31" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label27" runat="server" Font-Bold="True" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox32" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label28" runat="server" Font-Bold="True" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox33" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label49" runat="server" Font-Bold="True" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox34" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label50" runat="server" Font-Bold="True" Font-Size="Medium" Text="Analytical Charge P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox35" runat="server" BackColor="#6F53DF" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label51" runat="server" Font-Bold="True" Font-Size="Medium" Text="Analytical Charge P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox36" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label414" runat="server" Font-Bold="True" Font-Size="Medium" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox125" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label415" runat="server" Font-Bold="True" Font-Size="Medium" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox157" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="M_Label466" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="row ">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label610" runat="server" Font-Bold="True" Font-Size="Medium" Text="LD %"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox142" runat="server" BackColor="#6F53DF" Font-Bold="True" ForeColor="White" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label388" runat="server" Font-Bold="True" Font-Size="Medium" Text="LD (Rs.)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox143" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="M_Label419" runat="server" ForeColor="#CC0000"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label384" runat="server" Font-Bold="True" Text="Local Freight P/U"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox160" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label628" runat="server" Font-Bold="True" Text="R.C.M."></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:DropDownList class="form-select" ID="DropDownList44" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem Selected="True">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label78" runat="server" Font-Bold="True" Text="Other Deduction(Rs)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox58" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label79" runat="server" Font-Bold="True" Text="Deduction Head"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox59" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label21" runat="server" Font-Bold="True" Text="GST Payment"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:DropDownList class="form-select" ID="DropDownList5" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col">
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="S.D. (Rs.)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox3" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label386" runat="server" Font-Bold="True" Text="Penality P/U Mat" Style="height: 15px"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="M_TextBox162" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <div class="row align-items-center">
                                                    <div class="form-check">
                                                        <asp:CheckBox ID="M_CheckBox1" class="form-check-input" runat="server" Text=""></asp:CheckBox>
                                                        <label class="form-check-label text-primary" for="M_CheckBox1">Percentage</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label85" runat="server" Font-Bold="True" Text="TDS(M-JUNCTION)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox64" runat="server" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label629" runat="server" Font-Bold="True" Text="TDS"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="txtTDS" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label71" runat="server" Font-Bold="True" Text="TDS SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox54" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label72" runat="server" Font-Bold="True" Text="TDS CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox55" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label73" runat="server" Font-Bold="True" Text="TDS IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox56" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-5 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label84" runat="server" Font-Bold="True" Text="TCS(%)"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox class="form-control" ID="TextBox62" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row align-items-center mt-1" style="background-color: #4686F0">
                                            <div class="col">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label134" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Other Charges"></asp:Label>
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
                                                        <asp:TextBox ID="M_TextBox144" CssClass="form-control" TextMode="MultiLine" runat="server" Font-Names="Times New Roman"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-4">
                                                        <asp:Label ID="Label390" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remarks For Modification:-"></asp:Label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="M_TextBox145" runat="server" CssClass="form-control" TextMode="MultiLine" Font-Names="Times New Roman"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center" style="border-bottom: 3px; border-left: 3px; border-right: 3px; border-style: Groove; border-color: #FF3399;">
                                    <div class="col text-center">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label139" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Material Details"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col" style="overflow: scroll">
                                                <asp:GridView ID="GridView210" Width="300%" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
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
                                                        <asp:TemplateField HeaderText="SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cess"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Freight"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Local Fright"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Penality"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="L.D. Charge"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Payment"></asp:TemplateField>
                                                        <asp:BoundField DataField="TRANS_SHORT" HeaderText="Shortage Value" />
                                                        <asp:TemplateField HeaderText="Loss On ED"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Wt Var Val"></asp:TemplateField>
                                                        <asp:BoundField DataField="PROV_VALUE" HeaderText="PROV. Value" />
                                                        <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                                                        <asp:BoundField DataField="TRANS_CHARGE" HeaderText="Transport Charge" />
                                                        <asp:BoundField DataField="GARN_NOTE" HeaderText="Remarks" />
                                                        <asp:BoundField DataField="TOTAL_MT" HeaderText="Total Mt." />
                                                        <asp:TemplateField HeaderText="RCM SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RCM CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RCM IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RCM Cess"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SD"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Other deduction"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TCS"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TAXABLE_VALUE"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cess%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS"></asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=================================== GARN View END=====================================--%>

                    <%--=====VIEW 2 GARN VALUATION START=====--%>
                    <asp:View ID="View3" runat="server">
                        <div class="row justify-content-center">
                            <div class="col">
                                <div class="row" style="border: 3px; border-style: Groove; border-color: #FF3399">
                                    <%--=================Left Panel ================================--%>
                                    <div class="col-4 text-start" style="border-right: 3px; border-right-style: groove; border-right-color: #FF3399">
                                        <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label74" runat="server" Font-Bold="True" ForeColor="White" Text="Transportation Valuation"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Text="Crr/Inv No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label622" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work SLNo"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:DropDownList class="form-select" ID="DropDownList41" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:TextBox class="form-control" ID="TextBox174" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox174" />
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:TextBox class="form-control" ID="TextBox175" runat="server"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox175" />
                                            </div>
                                        </div>



                                        <div class="row align-items-center mt-2" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label97" runat="server" Font-Bold="True" ForeColor="White" Text="Details"></asp:Label>
                                            </div>

                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label15" runat="server" ForeColor="Blue" Text="Party Name" Font-Bold="True"></asp:Label>
                                            </div>
                                            <div class="col-8">
                                                <asp:Label ID="Label16" runat="server" ForeColor="#FF0066"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col text-center">
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Add" />
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Calculate" />
                                                <asp:Button ID="Button4" runat="server" CssClass="btn btn-success" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="Button3" runat="server" CssClass="btn btn-danger" Text="Close" />

                                            </div>

                                        </div>

                                        <div class="row align-items-center mt-0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label17" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                            </div>
                                        </div>

                                        <asp:Panel ID="Panel4" CssClass="g-0" runat="server" BackColor="#CCFFFF" Visible="False">
                                            <div class="row justify-content-center" style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                                                <div class="col-11 text-start g-0">

                                                    <div class="row align-items-center mt-1">

                                                        <div class="col text-center">
                                                            <asp:Label ID="Label125" runat="server" Font-Bold="True" ForeColor="Blue" Style="text-align: center" Text="ARE YOU SURE?"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="row align-items-center mt-1">
                                                        <div class="col-5 text-end">
                                                            <asp:Label ID="Label126" runat="server" Font-Bold="True" ForeColor="Blue" Text="Enter Password"></asp:Label>
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:TextBox class="form-control" ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center mt-1">
                                                        <div class="col-5 text-start">
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:Button ID="Button5" runat="server" Font-Bold="True" ForeColor="White" Text="Submit" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                        </div>
                                                    </div>
                                                    <div class="row align-items-center mb-1">
                                                        <div class="col-4 text-start">
                                                        </div>
                                                        <div class="col-1 text-start">
                                                            <asp:Label ID="Label20" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                    <%--=================Right Panel ================================--%>
                                    <div class="col-8">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col-6">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label128" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="As per Order"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-center">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label129" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="As per Valuation"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5 text-start">
                                                        <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-7 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox7" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Size="Medium" Text="Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox8" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label25" runat="server" Font-Bold="True" Text="Discount%"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox9" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Size="Medium" Text="Discount%"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox10" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label138" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label44" runat="server" Font-Bold="True" Font-Size="Medium" Text="Packing"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox5" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label55" runat="server" Font-Bold="True" Font-Size="Medium" Text="Packing"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox26" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label29" runat="server" Font-Bold="True" Text="SGST%"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox13" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label30" runat="server" Font-Bold="True" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox14" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label31" runat="server" Font-Bold="True" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox15" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label32" runat="server" Font-Bold="True" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox16" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label33" runat="server" Font-Bold="True" Text="IGST%"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox17" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label34" runat="server" Font-Bold="True" Text="IGST%"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox18" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label35" runat="server" Font-Bold="True" Text="CESS%"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox19" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label36" runat="server" Font-Bold="True" Text="CESS%"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox20" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label37" runat="server" Font-Bold="True" Text="Penality Rs."></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox21" runat="server" BackColor="#6F53DF" Font-Bold="False" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label38" runat="server" Font-Bold="True" Text="Penality Rs."></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox22" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label75" runat="server" Font-Bold="True" Text="TDS(M-JUNCTION)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox63" runat="server" Font-Bold="False" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label40" runat="server" Font-Bold="True" Text="TDS (%)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox24" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label86" runat="server" Font-Bold="True" Text="TDS SGST(%)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox65" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label87" runat="server" Font-Bold="True" Text="TDS CGST(%)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox66" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label158" runat="server" ForeColor="#CC0000"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row ">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="TDS IGST(%)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox67" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="LD (Rs)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox25" runat="server" Font-Bold="False" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label19" runat="server" ForeColor="#CC0000"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="GST Payment"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList class="form-select" ID="DropDownList7" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col">
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="R.C.M."></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label54" runat="server" Font-Bold="True" Text="S.D. (%)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox4" runat="server" Font-Names="Times New Roman">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label39" runat="server" Font-Bold="True" Text="Total Qty"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox23" runat="server" Font-Bold="False" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label56" runat="server" Font-Bold="True" Text="Total Value"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox39" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label57" runat="server" Font-Bold="True" Text="Total SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox40" runat="server" Font-Bold="False" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label58" runat="server" Font-Bold="True" Text="Total CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox41" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label59" runat="server" Font-Bold="True" Text="Total IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox42" runat="server" Font-Bold="False" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label67" runat="server" Font-Bold="True" Text="GST(LD/PENALTY)"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox50" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label63" runat="server" Font-Bold="True" Text="Total SD"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox46" runat="server" Font-Bold="False" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label64" runat="server" Font-Bold="True" Text="Total IT"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox47" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label60" runat="server" Font-Bold="True" Text="Total LD"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox43" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label65" runat="server" Font-Bold="True" Text="Total Penalty"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox48" runat="server" Font-Bold="False" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label89" runat="server" Font-Bold="True" Text="Total TDS ON SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox68" runat="server" Font-Bold="False" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label90" runat="server" Font-Bold="True" Text="Total TDS ON CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox69" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label91" runat="server" Font-Bold="True" Text="Total TDS ON IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox70" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label66" runat="server" Font-Bold="True" Text="Final Payment"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox49" runat="server" Font-Names="Times New Roman" ReadOnly="True">0.00</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row align-items-center">
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label172" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remarks For Penality:-"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox27" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-6 text-start">
                                                <div class="row align-items-center">
                                                    <div class="col-5">
                                                        <asp:Label ID="Label173" runat="server" Font-Bold="True" Font-Size="Medium" Text="Remarks For Modification:-"></asp:Label>
                                                    </div>
                                                    <div class="col-7">
                                                        <asp:TextBox class="form-control" ID="TextBox28" runat="server" TextMode="MultiLine"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>


                                <div class="row align-items-center justify-content-center" style="border-bottom: 3px; border-left: 3px; border-right: 3px; border-style: Groove; border-color: #FF3399;">
                                    <div class="col text-center">
                                        <div class="row align-items-center" style="background-color: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label174" runat="server" Font-Bold="True" ForeColor="White" Font-Size="Medium" Text="Work Details"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col" style="overflow: scroll">
                                                <asp:GridView ID="GridView1" Width="300%" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                All
                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:BoundField DataField="mb_no" HeaderText="MB No" />
                                                        <asp:BoundField DataField="mb_date" HeaderText="MB Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="po_no" HeaderText="Order No" />
                                                        <asp:BoundField DataField="wo_slno" HeaderText="Work Sl No" />
                                                        <asp:BoundField DataField="w_name" HeaderText="Job Name" />
                                                        <asp:BoundField DataField="w_au" HeaderText="A/U" />
                                                        <asp:BoundField DataField="from_date" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="to_date" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="rqd_qty" HeaderText="Chln. Qty" />
                                                        <asp:BoundField DataField="work_qty" HeaderText="Rcd/Send. Qty." />
                                                        <asp:TemplateField HeaderText="Unit Price" />
                                                        <asp:BoundField DataField="prov_amt" HeaderText="PROV. S. Charge" />
                                                        <asp:TemplateField HeaderText="Party Payment"></asp:TemplateField>
                                                        <asp:BoundField DataField="pen_amt" HeaderText="Penality" />
                                                        <asp:BoundField DataField="sgst" HeaderText="SGST" />
                                                        <asp:BoundField DataField="cgst" HeaderText="CGST" />
                                                        <asp:BoundField DataField="igst" HeaderText="IGST" />
                                                        <asp:BoundField DataField="cess" HeaderText="CESS" />
                                                        <asp:BoundField DataField="sgst_liab" HeaderText="SGST Liab." />
                                                        <asp:BoundField DataField="cgst_liab" HeaderText="CGST Liab." />
                                                        <asp:BoundField DataField="igst_liab" HeaderText="IGST Liab." />
                                                        <asp:BoundField DataField="cess_liab" HeaderText="CESS Liab." />
                                                        <asp:BoundField DataField="it_amt" HeaderText="TDS Tax" />
                                                        <asp:TemplateField HeaderText="S.D."></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Diff. Value"></asp:TemplateField>
                                                        <asp:BoundField DataField="rcm" HeaderText="R.C.M." />
                                                        <asp:TemplateField HeaderText="L.D."></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm IGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rcm CESS"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Final Payment Amount"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cess%"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Taxable Amt."></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS SGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS CGST"></asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS IGST"></asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:View>

                    <%--=================================== Transporter View END=====================================--%>
                </asp:MultiView>
            </div>
        </div>
    </div>


</asp:Content>

