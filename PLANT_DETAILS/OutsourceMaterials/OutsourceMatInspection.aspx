<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OutsourceMatInspection.aspx.vb" Inherits="PLANT_DETAILS.OutsourceMatInspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Outsourced Material Inspection" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>



    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10 text-center">

                <asp:Panel ID="Panel3" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Visible="False">
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="Blue" Text="CRR"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList class="form-select" ID="CRR_DropDownList" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:Label ID="Label36" runat="server" Font-Bold="True" ForeColor="Blue" Text="PO No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox class="form-control" ID="po_no_TextBox" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-3">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>
                        </div>

                    </div>
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label29" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat SL No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList class="form-select" ID="MATCODE_DropDownList" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label505" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat. Name"></asp:Label>
                        </div>
                        <div class="col-4">
                            <asp:TextBox class="form-control" ID="TextBox184" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman"></asp:TextBox>
                        </div>
                        <div class="col-1 text-end">
                            <asp:Label ID="Label32" runat="server" Font-Bold="True" ForeColor="Blue" Text="A/U"></asp:Label>

                        </div>
                        <div class="col-1 text-end">
                            <asp:TextBox class="form-control" ID="AU_GRANTextBox" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman"></asp:TextBox>
                        </div>

                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label31" runat="server" ForeColor="Blue" Text="Received Qty"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox class="form-control" ID="RCVDQTY_TextBox" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True" Font-Names="Times New Roman"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label33" runat="server" ForeColor="Blue" Text="Qty to be Rejected"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox class="form-control" ID="GARN_REJQTYTextBox" runat="server"  Font-Names="Times New Roman"></asp:TextBox>
                        </div>

                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label35" runat="server" Font-Bold="True" ForeColor="Blue" Text="Note" ></asp:Label>
                        </div>
                        <div class="col-10">
                            <asp:TextBox ID="NOTE_TextBox" runat="server" CssClass="form-control" TextMode="MultiLine" Font-Names="Times New Roman"></asp:TextBox>
                        </div>

                    </div>


                    <div class="row align-items-center m-1 mt-2">

                        <div class="col-2 text-start">
                        </div>
                        <div class="col-3">
                        </div>
                        <div class="col-7 text-end">
                            <asp:Button ID="Button26" runat="server" Font-Bold="True" Text="Add" CssClass="btn btn-primary" />
                            <asp:Button ID="Button28" runat="server" Font-Bold="True" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                            <asp:Button ID="Button27" runat="server" Font-Bold="True" Text="Cancel" CssClass="btn btn-danger" />
                        </div>

                    </div>

                    <div class="row align-items-center m-1">

                        <asp:Panel ID="Panel45" runat="server" ScrollBars="Auto">
                            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="Gray"
                                BorderStyle="Groove" BorderWidth="1px"
                                CellPadding="3" ForeColor="Black" GridLines="Both" Style="text-align: center; width: 100%; height: 75px; font-size: medium" class="table table-bordered text-center">
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


                </asp:Panel>
            </div>

        </div>
    </div>



</asp:Content>
