<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="r_issue.aspx.vb" Inherits="PLANT_DETAILS.r_issue" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
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

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Raw Material Issue" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label23" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="Delvdate7_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="Delvdate7_CalendarExtender" TargetControlID="TextBox1" />
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10" style="border: 5px groove #FF0066; float: left; text-align: left;">
                <div class="row">
                    <div class="col-8" style="border-right: 5px groove #FF0066;">
                        <div class="row align-items-center mt-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Voucher No"></asp:Label>

                            </div>
                            <div class="col-8 text-start">
                                <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True" class="form-select">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>

                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="Textbox31" class="form-control " runat="server" Font-Names="Times New Roman"  BackColor="White"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label426" runat="server" Font-Bold="True" ForeColor="Blue" Text="Required Quantity"></asp:Label>

                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox163" class="form-control " runat="server"   BackColor="White" Font-Names="Times New Roman"></asp:TextBox>

                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label451" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Quantity"></asp:Label>

                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox3" class="form-control " runat="server" Font-Names="Times New Roman"></asp:TextBox>

                            </div>
                        </div>                

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label423" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Type"></asp:Label>
                            </div>
                            <div class="col-8 text-start">

                                <asp:TextBox ID="TextBox172" runat="server" class="form-control "  BackColor="White"></asp:TextBox>

                            </div>
                        </div>



                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cost Center"></asp:Label>
                            </div>
                            <div class="col-8 text-start">

                                <asp:TextBox ID="TextBox173" runat="server" class="form-control "  BackColor="White"></asp:TextBox>

                            </div>
                        </div>


                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Requisition By"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox4" runat="server" class="form-control "  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Requisition Date"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox5" runat="server" class="form-control "  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Blue" Text="Authorization By"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox12" runat="server" class="form-control "  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label15" runat="server" Font-Bold="True" ForeColor="Blue" Text="Authorization Date"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox ID="TextBox13" runat="server" class="form-control "  BackColor="White"></asp:TextBox>
                            </div>
                        </div>


                        <div class="row align-items-center mb-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Purpose"></asp:Label>
                            </div>
                            <div class="col-8 text-start">

                                <asp:TextBox ID="TextBox2" runat="server" Font-Names="Times New Roman" class="form-control " TextMode="MultiLine"  BackColor="White"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="col-4 align-top">

                        <div class="row mt-2 align-items-center">
                            <div class="col-5 text-end">
                                <asp:Label ID="Label17" runat="server" Font-Bold="True" ForeColor="Blue">A/C Unit</asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox168" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>

                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="Blue" Text="Availble Stock"></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox167" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Value"></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox166" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label20" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No"></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox ID="TextBox169" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label450" runat="server" Font-Bold="True" ForeColor="Blue" Text="Location"></asp:Label>
                            </div>
                            <div class="col-7 text-start">

                                <asp:TextBox ID="TextBox170" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-5 text-end">
                            </div>
                            <div class="col-7 text-start mt-2">
                                <asp:Button ID="Button45" runat="server" class="btn btn-primary fw-bold" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                <asp:Button ID="Button46" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col text-start">
                                <asp:Label ID="ISSUE_ERR_LABEL" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </div>

                        </div>
                    </div>



                </div>

            </div>

        </div>
    </div>


    
</asp:Content>
