<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="rm_report.aspx.vb" Inherits="PLANT_DETAILS.rm_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
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
            <asp:Label ID="Label1" runat="server" Text="Raw Material Report Section" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center">
                    <div class="col-6">

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label453" runat="server" Font-Bold="True" ForeColor="Blue" Text="Search Type"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:DropDownList CssClass="form-select" ID="REPORTDropDownList" runat="server" AutoPostBack="True">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Stock</asp:ListItem>
                                    <asp:ListItem>Issue</asp:ListItem>
                                    <asp:ListItem>Material Transaction</asp:ListItem>
                                    <asp:ListItem>Daily Report</asp:ListItem>
                                    <asp:ListItem>CRR</asp:ListItem>
                                    <asp:ListItem>Transporter Wise</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label454" runat="server" Font-Bold="True" ForeColor="Blue" Text="Label"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:DropDownList CssClass="form-select" ID="REPORTDropDownList2" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label438" runat="server" Font-Bold="True" ForeColor="Blue" Text="Label"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:DropDownList CssClass="form-select" ID="REPORTDropDownList3" runat="server">
                                </asp:DropDownList>
                            </div>

                        </div>

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label439" runat="server" Font-Bold="True" ForeColor="Blue" Text="Label"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:TextBox CssClass="form-control" ID="REPORTTextBox1" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="REPORTTextBox1_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="REPORTTextBox1"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label440" runat="server" Font-Bold="True" ForeColor="Blue" Text="Label"></asp:Label>

                            </div>
                            <div class="col-6 text-start">
                                <asp:TextBox CssClass="form-control" ID="REPORTTextBox2" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="REPORTTextBox2_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="REPORTTextBox2"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label457" runat="server" Font-Bold="True" ForeColor="Blue" Text="Label" Visible="False"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:DropDownList CssClass="form-select" ID="DropDownList20" runat="server" Visible="False">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row justify-content-center align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label458" runat="server" Font-Bold="True" ForeColor="Blue" Text="Label" Visible="False"></asp:Label>
                            </div>
                            <div class="col-6 text-start">
                                <asp:DropDownList CssClass="form-select" ID="DropDownList19" runat="server" Visible="False">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row justify-content-center align-items-center mt-2">
                            <div class="col-3 text-start">
                            </div>
                            <div class="col-6 text-start">
                                <asp:Button ID="REPORTButton45" runat="server" Text="Preview" CssClass="btn btn-primary fw-bold"></asp:Button>
                                <asp:Button ID="REPORTButton46" runat="server" Text="Cancel" CssClass="btn btn-primary fw-bold" />
                            </div>
                        </div>

                    </div>
                </div>
                
            </div>
        </div>
    </div>

















    
    
</asp:Content>
