<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="d_work.aspx.vb" Inherits="PLANT_DETAILS.d_work" %>

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
                        url: '<%=ResolveUrl("~/Service.asmx/wo_no_search")%>',
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
            <asp:Label ID="Label1" runat="server" Text="Daily Work Entry" Font-Bold="True" Font-Size="Larger"></asp:Label>
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

                                <div class="row justify-content-center align-items-center mt-2">
                                    <div class="col-3 text-start">
                                        <asp:Label ID="Label402" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No:-"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:TextBox class="form-control" ID="DropDownList26" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row justify-content-center align-items-center mt-2 mb-2">
                                    <div class="col-3 text-start">
                                    </div>
                                    <div class="col-6 text-start">
                                        <asp:Button ID="Button45" runat="server" class="btn btn-primary fw-bold" Text="Proceed" />
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
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label2" runat="server" Text="Order No" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="DropDownList1" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 g-0 text-start">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Blue" Text="Supl Name"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox12" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Sl No"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 text-start g-0">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Blue" Text="A / U"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox5" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Desc"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox3" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
                                    </div>
                                    <div class="col-1 g-0 text-start">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Text="To"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox2" runat="server" AutoCompleteType="Disabled" AutoPostBack="True"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox2" />
                                    </div>
                                    <div class="col-1 g-0 text-start">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Blue" Text="EXP Date"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox10" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col-1 g-0 text-start">
                                        <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Blue" Text="Bal Qty"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox11" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label8" runat="server" Text="Worked Qty" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox4" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-1 g-0 text-start">
                                        <asp:Label ID="Label9" runat="server" Text="Rqd Qty" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start">
                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label15" runat="server" Font-Bold="True" ForeColor="Blue" Text="Deptt"></asp:Label>
                                    </div>
                                    <div class="col-5 text-start">
                                        <asp:DropDownList class="form-select" ID="DropDownList3" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label403" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </div>

                                </div>

                                <div class="row align-items-center">
                                    <div class="col-1 text-start">
                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Blue" Text="Note"></asp:Label>
                                    </div>
                                    <div class="col text-start">
                                        <asp:TextBox class="form-control" ID="TextBox8" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="row align-items-center mt-2 mb-2">
                                    <div class="col-1 text-start">
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button49" runat="server" Font-Bold="True" ForeColor="White" Text="Add" CssClass="btn btn-primary" />
                                        <asp:Button ID="Button50" runat="server" Font-Bold="True" ForeColor="White" Text="Save" CssClass="btn btn-success" />
                                        <asp:Button ID="Button51" runat="server" Font-Bold="True" ForeColor="White" Text="Cancel" CssClass="btn btn-danger" />
                                        <asp:Button ID="Button4" runat="server" Font-Bold="True" ForeColor="White" Text="Close" CssClass="btn btn-danger" />
                                    </div>

                                </div>

                                <asp:Panel ID="Panel8" runat="server" Style="text-align: left" Visible="False">
                                    <div class="row justify-content-center" style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                                        <div class="col-5 text-start">

                                            <div class="row align-items-center mt-1">
                                                <div class="col-4 text-start">
                                                    <asp:Label ID="Label46" runat="server" Font-Bold="True" ForeColor="Blue" Text="Enter Password"></asp:Label>
                                                </div>
                                                <div class="col text-start">
                                                    <asp:TextBox class="form-control" ID="TextBox32" runat="server" TextMode="Password"></asp:TextBox>
                                                </div>
                                                
                                            </div>
                                            <div class="row align-items-center mt-1">
                                                <div class="col-4 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:Button ID="Button60" runat="server" Font-Bold="True" ForeColor="White" Text="Submit" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                </div>
                                            </div>
                                            <div class="row align-items-center mb-1">
                                                <div class="col-4 text-start">
                                                </div>
                                                <div class="col text-start">
                                                    <asp:Label ID="Label47" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>

                                <div class="row align-items-center mt-1">
                                    <div class="col text-center">
                                        <asp:GridView ID="GridView1"  Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" CellPadding="2">
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
