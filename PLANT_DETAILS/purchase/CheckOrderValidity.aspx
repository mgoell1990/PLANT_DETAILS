<%@ Page Title="" Language="vb" MaintainScrollPositionOnPostback="true" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CheckOrderValidity.aspx.vb" Inherits="PLANT_DETAILS.CheckOrderValidity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />


    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Check Order Validity" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>



    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-6 text-center" style="border-style: Groove; border-color: Blue; border-width: 3px">

                <div class="row align-items-center mt-1">

                    <div class="col-3 text-start">
                        <asp:Label ID="Label6" runat="server" ForeColor="Blue" Text="Date Between"></asp:Label>
                    </div>
                    <div class="col-3">
                        <asp:TextBox class="form-control" ID="TextBox4" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="TextBox33_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox4" />
                    </div>

                    <div class="col-1 text-start">
                        <asp:Label ID="Label7" runat="server" ForeColor="Blue" Text="To"></asp:Label>
                    </div>
                    <div class="col-3">
                        <asp:TextBox class="form-control" ID="TextBox5" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" BehaviorID="TextBox34_CalendarExtender" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox5" />
                    </div>

                </div>



                <div class="row align-items-center mt-2 mb-1">

                    <div class="col-3 text-start">
                    </div>
                    <div class="col text-start">
                        <asp:Button ID="Button4" runat="server" Font-Bold="True" Text="Search" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                        <asp:Button ID="Button16" runat="server" Font-Bold="True" Text="Download" CssClass="btn btn-primary" />
                        <asp:Button ID="Button5" runat="server" Font-Bold="True" Text="Print" CssClass="btn btn-primary" />

                    </div>

                </div>
                
            </div>

        </div>

        <div class="row align-items-center mt-2">

            <div class="col text-start">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered border-2 table-responsive text-center" ShowHeaderWhenEmpty="False" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="SO_NO" HeaderText="Order No." />
                        <asp:BoundField DataField="SO_DATE" HeaderText="Order Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="SO_ACTUAL" HeaderText="Actual Order No." />
                        <asp:BoundField DataField="SO_ACTUAL_DATE" HeaderText="Actual Order Date" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="SUPL_ID" HeaderText="Party Code" />
                        <asp:BoundField DataField="SUPL_NAME" HeaderText="Party Name" />
                        <asp:BoundField DataField="order_validity" HeaderText="Order Validity" DataFormatString="{0:dd/MM/yyyy}" />
                    </Columns>
                </asp:GridView>

            </div>

        </div>
    </div>






</asp:Content>

