<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="print_order.aspx.vb" Inherits="PLANT_DETAILS.print_order" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />


    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Print Order Copy" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>



    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-6 text-center" style="border-style: Groove; border-color: Blue; border-width: 3px">

                
                    <div class="row align-items-center mt-1">

                        <div class="col-4 text-start">
                            <asp:Label ID="Label447" runat="server" ForeColor="Blue" Text="Report For"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:DropDownList class="form-select" ID="DropDownList51" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>Print Order</asp:ListItem>
                                <%--<asp:ListItem>Order Balance</asp:ListItem>
                                <asp:ListItem>Amendment</asp:ListItem>
                                <asp:ListItem>Short Close</asp:ListItem>
                                <asp:ListItem>Completed</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>


                    </div>
                    <div class="row align-items-center">

                        <div class="col-4 text-start">
                            <asp:Label ID="Label446" runat="server" ForeColor="Blue" Text="Order Type"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:DropDownList class="form-select" ID="DropDownList50" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="row align-items-center">

                        <div class="col-4 text-start">
                            <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row align-items-center">
                        <div class="col-4 text-start">
                            <asp:Label ID="Label445" runat="server" ForeColor="Blue" Text="Order No"></asp:Label>
                        </div>
                        <div class="col">
                            <asp:DropDownList class="form-select" ID="DropDownList49" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="row align-items-center mt-2">

                        <div class="col-4 text-start">
                        </div>
                        <div class="col text-start">
                            <asp:Button ID="Button62" runat="server" Font-Bold="True" Text="Print" CssClass="btn btn-primary" />
                            <asp:Button ID="Button61" runat="server" Font-Bold="True" Text="Cancel" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                        </div>

                    </div>



                
            </div>

        </div>
    </div>



</asp:Content>
