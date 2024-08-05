<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="mat_issue.aspx.vb" Inherits="PLANT_DETAILS.mat_issue" %>

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
            $("[id$=DropDownList3]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/ISSUE")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    au: item.split('^')[2],
                                    mat_avg: item.split('^')[4],
                                    mat_stock: item.split('^')[3],
                                    mat_loca: item.split('^')[5],
                                    line_no: item.split('^')[6]
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
                    $("[id$=TextBox168]").val(i.item.au);
                    $("[id$=TextBox167]").val(i.item.mat_stock);
                    $("[id$=TextBox166]").val(i.item.mat_avg);
                    $("[id$=TextBox170]").val(i.item.mat_loca);
                    $("[id$=TextBox169]").val(i.item.line_no);
                    $("[id$=DropDownList7]").val("Select");
                    $("[id$=DropDownList2]").val("Select");
                },
                minLength: 1
            });
        });
    </script>

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Store Requisition" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10" style="border: 5px groove #FF0066; float: left; text-align: left;">
                <div class="row">
                    <div class="col-8" style="border-right: 5px groove #FF0066;">
                        <div class="row align-items-center mt-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Vaucher No"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="issue_no" class="form-control " runat="server" BackColor="#559fe0" ForeColor="White" ReadOnly="True"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                            </div>
                            <div class="col-8 text-start">

                                <asp:TextBox ID="DropDownList3" runat="server" class="form-control " Font-Names="Times New Roman"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label426" runat="server" Font-Bold="True" ForeColor="Blue" Text="Reqd Qty"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox163" class="form-control " runat="server"></asp:TextBox>


                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="row align-items-center">
                                    <div class="col-4 text-end">
                                        <asp:Label ID="Label425" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Type"></asp:Label>
                                    </div>
                                    <div class="col-8 text-start">

                                        <asp:DropDownList ID="DropDownList7" class="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>To Deptt</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>



                                <div class="row align-items-center">
                                    <div class="col-4 text-end">
                                        <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cost Center"></asp:Label>
                                    </div>
                                    <div class="col-8 text-start">
                                        <asp:DropDownList ID="DropDownList2" class="form-select" runat="server" Font-Names="Times New Roman">
                                        </asp:DropDownList>


                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="row align-items-center mb-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Purpose"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox2" runat="server" Height="100 px" class="form-control" TextMode="MultiLine" Font-Names="Times New Roman"></asp:TextBox>


                            </div>
                        </div>
                    </div>

                    <div class="col-4 align-top">

                        <div class="row mt-2 align-items-center">
                            <div class="col-5 text-end">
                                <asp:Label ID="Label429" runat="server" Font-Bold="True" ForeColor="Blue">A/U</asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox168" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>

                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label431" runat="server" Font-Bold="True" ForeColor="Blue" Text="Availble Stock" ></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox167" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label430" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Value" ></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox166" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label432" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No" ></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox169" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label450" runat="server" Font-Bold="True" ForeColor="Blue" Text="Location" ></asp:Label>
                            </div>
                            <div class="col-7 text-start">

                                <asp:TextBox ID="TextBox170" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-5 text-end">
                            </div>
                            <div class="col-7 text-start mt-2">
                                <asp:Button ID="Button45" runat="server" class="btn btn-primary fw-bold" Text="SAVE" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                <asp:Button ID="Button46" runat="server" class="btn btn-danger fw-bold" Text="CANCEL" />
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col text-start">
                                <asp:Label ID="ISSUE_ERR_LABEL" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                            </div>

                        </div>
                    </div>



                </div>
                
            </div>

        </div>
    </div>

</asp:Content>
