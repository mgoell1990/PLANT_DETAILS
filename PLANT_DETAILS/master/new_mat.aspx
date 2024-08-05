<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="new_mat.aspx.vb" Inherits="PLANT_DETAILS.new_mat" %>

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


    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Add New Material" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col text-center">

                <div class="row justify-content-center">
                    <div class="col-7 text-center">
                        <asp:Panel ID="Panel16" runat="server" BorderColor="Blue" BorderStyle="Groove">
                            <div class="row">
                                <div class="col m-3">
                                    <div class="row align-items-center">
                                        <div class="col-2 text-start g-0">
                                            <asp:Label ID="Label40" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Type"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList4" runat="server" AutoPostBack="True">
                                                <asp:ListItem>Store Material</asp:ListItem>
                                                <asp:ListItem>Raw Material</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label46" runat="server" Font-Bold="True" ForeColor="Blue" Text="Pur Head"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList6" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-2 text-start g-0">
                                            <asp:Label ID="Label41" runat="server" Font-Bold="True" ForeColor="Blue" Text="Sub Group"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList5" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label47" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Head"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList7" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-2 text-start g-0">
                                            <asp:Label ID="Label45" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                                        </div>
                                        <div class="col-1 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox7" runat="server" BackColor="#660033" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-3 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox8" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label48" runat="server" Font-Bold="True" ForeColor="Blue" Text="Con Head"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList8" runat="server">
                                            </asp:DropDownList>
                                        </div>

                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-2 text-start g-0">
                                            <asp:Label ID="Label42" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Name"></asp:Label>
                                        </div>
                                        <div class="col text-start  g-0">
                                            <asp:TextBox class="form-control" ID="TextBox4" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-2 text-start g-0">
                                            <asp:Label ID="Label43" runat="server" Font-Bold="True" ForeColor="Blue" Text="Drawing No"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox5" runat="server" Font-Bold="False"></asp:TextBox>
                                        </div>
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Chptr Head"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:DropDownList class="form-select" ID="DropDownList1" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-2 text-start g-0">
                                            <asp:Label ID="Label44" runat="server" Font-Bold="True" ForeColor="Blue" Text="A / U"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox6" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-2 text-start">
                                        </div>
                                        <div class="col-4 text-start">
                                        </div>
                                    </div>

                                    <div class="row align-items-center">
                                        <div class="col-2 text-start g-0">
                                            <asp:Label ID="Label352" runat="server" Font-Bold="True" ForeColor="Blue" Text="Location"></asp:Label>
                                        </div>
                                        <div class="col-4 text-start g-0">
                                            <asp:TextBox class="form-control" ID="TextBox114" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-2 text-start">
                                        </div>
                                        <div class="col-4 text-start">
                                        </div>
                                    </div>                                   


                                    <div class="row align-items-center mt-2">
                                        <div class="col-8 text-end">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="True">Admin Password : </asp:Label>
                                        </div>
                                        <div class="col-4 text-center g-0">
                                            <asp:TextBox class="form-control" ID="TextBox1" runat="server"  TextMode="Password"></asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="row align-items-center mt-2">
                                        <div class="col-8 text-end">
                                        </div>
                                        <div class="col text-start g-0">
                                            <asp:Button ID="Button35" runat="server" Text="Save" class="btn btn-primary fw-bold" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                            <asp:Button ID="Button38" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                                        </div>

                                    </div>

                                    <div class="row align-items-center mt-2">
                                        <div class="col text-start">
                                            <asp:Label ID="mat_err_label" runat="server" Font-Bold="True" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>


            </div>
        </div>
    </div>


</asp:Content>
