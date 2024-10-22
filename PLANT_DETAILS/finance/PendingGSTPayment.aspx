<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" CodeBehind="PendingGSTPayment.aspx.vb" Inherits="PLANT_DETAILS.PendingGSTPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label94" runat="server" Text="Pending GST Payment" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label1" runat="server" Text="Date"></asp:Label>

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
                        <div class="col-11 justify-content-center m-1" style="border-color: #FF3399; border-style: groove">

                            <div class="row align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label445" runat="server" ForeColor="Blue" Text="Voucher No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox53" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col text-start">
                                    <asp:Label ID="Label624" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label81" runat="server" Font-Bold="False" ForeColor="Blue" Text="Bill track Id"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="DropDownList15" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:Button ID="Button10" runat="server" class="btn btn-primary fw-bold" Text="Add Bill Details" />
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label74" runat="server" Font-Bold="False" ForeColor="Blue" Text="Section Sl. No."></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox31" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label75" runat="server" Font-Bold="False" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox32" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox32_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox32" />
                                </div>
                                <div class="col-1 text-start g-0">
                                    <asp:Label ID="Label78" runat="server" Text="Narration" Font-Bold="False" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox34" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label84" runat="server" Font-Bold="False" ForeColor="Blue" Text="Journal No."></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox40" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label85" runat="server" Text="Date" Font-Bold="False" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox41" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:CalendarExtender ID="TextBox41_CalendarExtender" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox41"></asp:CalendarExtender>
                                </div>
                                <div class="col-1 text-start g-0">
                                    <asp:Label ID="Label79" runat="server" Text="Party" Font-Bold="False" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox35" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label616" runat="server" Font-Bold="False" ForeColor="Blue" Text="Inv. No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox169" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label617" runat="server" Font-Bold="False" ForeColor="Blue" Text="Date"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox170" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                                <div class="col-1 text-start g-0">
                                    <asp:Label ID="Label448" runat="server" Text="Order No" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-4 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox55" runat="server" BackColor="#0099FF" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">

                                <div class="col-2 text-start">
                                    <asp:Label ID="Label446" runat="server" ForeColor="Blue" Text="CRR/GARN No"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:DropDownList class="form-select" ID="garn_dropdown" runat="server">
                                    </asp:DropDownList>
                                </div>
                                
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label76" runat="server" ForeColor="Blue" Text="ITC"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:DropDownList class="form-select" ID="DropDownList3" runat="server">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem Selected="True">Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label80" runat="server" Font-Bold="False" ForeColor="Blue" Text="Taxable Amount"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox36" runat="server">0</asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label2" runat="server" ForeColor="Blue" Text="CGST(%)"></asp:Label>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox2" runat="server">0</asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="SGST(%)"></asp:Label>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox3" runat="server">0</asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="IGST(%)"></asp:Label>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:TextBox class="form-control" ID="TextBox4" runat="server">0</asp:TextBox>
                                </div>
                                
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                    <asp:Label ID="Label5" runat="server" ForeColor="Blue" Text="Total GST(Rs.)"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="TextBox5" runat="server">0</asp:TextBox>
                                </div>
                            </div>

                            <div class="row  align-items-center mt-1">
                                <div class="col-2 text-start">
                                </div>
                                <div class="col-2 text-start g-0">
                                </div>
                                <div class="col-3 text-start">
                                </div>

                                <div class="col-1 text-start g-0">
                                </div>
                                <div class="col-4 text-start">
                                    <asp:Button ID="Button1" runat="server" Text="Add" CssClass="btn btn-success fw-bold" />
                                    <asp:Button ID="Button13" runat="server" Text="Save" CssClass="btn btn-success fw-bold" />
                                    <asp:Button ID="Button18" runat="server" Text="Cancel" CssClass="btn btn-danger fw-bold" />
                                </div>

                            </div>


                            <div class="row  align-items-center mt-1">
                                <div class="col text-start">
                                    <asp:Label ID="Label447" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                </div>
                            </div>



                            <asp:Panel ID="Panel6" runat="server" BackColor="#CCFFFF" Visible="False">
                                <div class="row justify-content-center" style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                                    <div class="col-6 text-start">

                                        <div class="row align-items-center mt-1">
                                            <div class="col-4 text-END">
                                                <asp:Label ID="Label42" runat="server" Font-Bold="True" ForeColor="Blue" Text="Enter Password"></asp:Label>
                                            </div>
                                            <div class="col-6 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox174" runat="server" TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row align-items-center mt-1">
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button59" runat="server" Font-Bold="True" ForeColor="White" Text="Submit" CssClass="btn btn-primary" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                            </div>
                                        </div>
                                        <div class="row align-items-center mb-1">
                                            <div class="col-4 text-start">
                                            </div>
                                            <div class="col-1 text-start">
                                                <asp:Label ID="Label623" runat="server" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>


                            <div class="row align-items-center">
                                <div class="col" style="overflow: scroll">
                                    <asp:GridView ID="GridView5" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="GARN_NO_MB_NO" HeaderText="GARN/MB No" />
                                            <asp:BoundField DataField="AC_NO" HeaderText="A/c Head" />
                                            <asp:BoundField DataField="ac_description" HeaderText="A/c Description" />
                                            <asp:BoundField DataField="AMOUNT_DR" HeaderText="Debit Amount"></asp:BoundField>
                                            <asp:BoundField DataField="AMOUNT_CR" HeaderText="Credit Amount"></asp:BoundField>
                                            <asp:BoundField DataField="POST_INDICATION" HeaderText="Post Indication"></asp:BoundField>
                                            <asp:BoundField DataField="TAXABLE_VALUE" HeaderText="Taxable Value"></asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="row  align-items-center ">
                                <div class="col-3 text-start d-inline-flex align-items-center">
                                    <asp:Label ID="Label6" runat="server" CssClass="w-50 me-2" Font-Bold="True" Text="Total Taxable" ForeColor="Blue"></asp:Label>
                                    <asp:TextBox class="form-control w-50" ID="txtTotalTaxableValue" runat="server" BackColor="#FFFF66" Font-Bold="True" ForeColor="#FF0066" ReadOnly="True">0.00</asp:TextBox>
                                </div>

                                <div class="col-3 text-start d-inline-flex align-items-center">
                                    <asp:Label ID="Label7" runat="server" CssClass="w-50 me-2" Font-Bold="True" Text="Total GST" ForeColor="Blue"></asp:Label>
                                    <asp:TextBox class="form-control w-50" ID="txtTotalGST" runat="server" BackColor="#FFFF66" Font-Bold="True" ForeColor="#FF0066" ReadOnly="True">0.00</asp:TextBox>
                                </div>
                                                                
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label92" runat="server" Font-Bold="True" Text="Debit" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-2 text-start g-0">
                                    <asp:TextBox class="form-control" ID="debitTextBox" runat="server" BackColor="#FFFF66" Font-Bold="True" ForeColor="#FF0066" ReadOnly="True">0.00</asp:TextBox>
                                </div>
                                <div class="col-1 text-start">
                                    <asp:Label ID="Label93" runat="server" Font-Bold="True" Text="Credit" ForeColor="Blue"></asp:Label>
                                </div>
                                <div class="col-2 text-start">
                                    <asp:TextBox class="form-control" ID="crTextBox10" runat="server" BackColor="#FFFF66" Font-Bold="True" ForeColor="#FF0066" ReadOnly="True">0.00</asp:TextBox>
                                </div>
                            </div>

                            


                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>


</asp:Content>

