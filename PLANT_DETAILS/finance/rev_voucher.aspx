<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="rev_voucher.aspx.vb" Inherits="PLANT_DETAILS.rev_voucher" %>

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
            <asp:Label ID="Label94" runat="server" Text="Reversal Journal Voucher" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox1" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:Panel ID="Panel30" runat="server">
                    <div class="row justify-content-center">
                        <div class="col-12 justify-content-center m-1" style="border-color: Blue; border-style: groove">

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label471" runat="server" ForeColor="Blue" Text="New Token No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox65" runat="server" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col text-start ">
                                    <asp:Label ID="Label479" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label480" runat="server" ForeColor="Blue" Text="Section Sl No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox66" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label481" runat="server" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox67" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox67_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox67"></asp:CalendarExtender>
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label550" runat="server" ForeColor="Blue" Text="Previous Token No"></asp:Label>
                                </div>
                                <div class="col-2 text-start ">
                                    <asp:DropDownList class="form-select" ID="DropDownList39" runat="server">
                                    </asp:DropDownList>
                                </div>
                                
                                <div class="col-1 text-start">
                                </div>
                                <div class="col-2 text-start">
                                </div>

                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label551" runat="server" Font-Bold="False" ForeColor="Blue" Text="Narration"></asp:Label>
                                </div>
                                <div class="col-5 text-start ">
                                    <asp:TextBox class="form-control" ID="TextBox95" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>


                                <div class="col text-end">
                                    <asp:Button ID="Button44" runat="server" Text="Add" CssClass="btn btn-primary" />
                                    <asp:Button ID="Button25" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                    <asp:Button ID="Button45" runat="server" Text="Cancel" CssClass="btn btn-danger" />

                                </div>
                            </div>

                            <div class="row align-items-center mt-2">
                                <div class="col" style="overflow: scroll">
                                    <asp:GridView ID="GridView3" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <asp:BoundField DataField="TOKEN_NO" HeaderText="Token No" />
                                            <asp:BoundField DataField="SEC_NO" HeaderText="Sec Sl No" />
                                            <asp:BoundField DataField="SEC_DATE" HeaderText="Sec Date" />
                                            <asp:BoundField DataField="SUPL_ID" HeaderText="Supl Code" />
                                            <asp:BoundField DataField="AC_NO" HeaderText="A/C Head" />
                                            <asp:BoundField DataField="ac_description" HeaderText="A/C Description" />
                                            <asp:BoundField DataField="AMOUNT_DR" HeaderText="Amount Debit" />
                                            <asp:BoundField DataField="AMOUNT_CR" HeaderText="Amount Credit" />
                                            <asp:BoundField DataField="BE_NO" HeaderText="BE NO" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>




                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>

