<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="c_entry.aspx.vb" Inherits="PLANT_DETAILS.c_entry" %>

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


    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label94" runat="server" Text="Cheque Entry" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col-10 text-center">
                <asp:Panel ID="Panel30" runat="server">
                    <div class="row justify-content-center">
                        <div class="col-12 justify-content-center m-1" style="border-color: Blue; border-style: groove">

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label514" runat="server" ForeColor="Blue" Text="Token No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:DropDownList CssClass="form-select" ID="DropDownList34" runat="server" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col text-start ">
                                    <asp:Label ID="Label532" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#00CC00"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label520" runat="server" ForeColor="Blue" Text="Inst No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox87" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label521" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox88" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox88_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox88" />
                                </div>

                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label650" runat="server" ForeColor="Blue" Text="BPV No"></asp:Label>
                                </div>
                                <div class="col-5 text-start ">
                                    <asp:Label ID="Label649" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Medium" ForeColor="Blue"></asp:Label>
                                </div>


                                <div class="col text-end">
                                    <asp:Button ID="Button32" runat="server" Text="Add" CssClass="btn btn-primary" />
                                    <asp:Button ID="Button33" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                    <asp:Button ID="Button34" runat="server" Text="Cancel" CssClass="btn btn-danger" />

                                </div>
                            </div>

                            <div class="row align-items-center mt-2">
                                <div class="col" style="overflow: scroll">
                                    <asp:GridView ID="GridView7" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField DataField="TOKEN_NO" HeaderText="TOKEN NO" />
                                            <asp:BoundField DataField="SUPL_ID" HeaderText="SUPL CODE" />
                                            <asp:BoundField DataField="AC_NO" HeaderText="A/C HEAD" />
                                            <asp:BoundField DataField="ac_description" HeaderText="A/C DESCRIPTION" />
                                            <asp:BoundField DataField="AMOUNT_CR" HeaderText="AMOUNT" />
                                            <asp:TemplateField HeaderText="B.P.V NO"></asp:TemplateField>
                                            <asp:TemplateField HeaderText="CHEQUE NO"></asp:TemplateField>
                                            <asp:TemplateField HeaderText="CHEQUE DATE"></asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row align-items-center mt-1">
                                <div class="col text-start">
                                    <asp:Label ID="Label541" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>

