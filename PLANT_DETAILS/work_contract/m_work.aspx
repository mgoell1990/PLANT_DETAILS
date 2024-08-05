<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="m_work.aspx.vb" Inherits="PLANT_DETAILS.m_work" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    >

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Measurement of Work" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-10 text-end">
            <asp:Label ID="Label2" runat="server" Text="Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox class="form-control" ID="TextBox1" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" CssClass="red" Format="dd-MM-yyyy" TargetControlID="TextBox1"></cc1:CalendarExtender>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <asp:MultiView ID="MultiView1" runat="server">

                    <%--=====VIEW 1 GARN START=====--%>

                    <asp:View ID="View1" runat="server">
                        <div class="row" style="border: 2px; border-style: Double; border-color: #4686F0">
                            <div class="col ">
                                <div class="row">
                                    <div class="col ms-2">



                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label36" runat="server" Font-Bold="True" ForeColor="Blue" Text="MB No"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox28" runat="server" BackColor="Red" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                            <div class="col g-0 text-start">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </div>

                                        </div>


                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label18" runat="server" Text="Order No" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList4" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="Blue" Text="Supl Name"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox13" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" ForeColor="Blue" Text="Sl No"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:DropDownList class="form-select" ID="DropDownList5" runat="server" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-1 text-start g-0">
                                                <asp:Label ID="Label22" runat="server" Font-Bold="True" ForeColor="Blue" Text="A / U"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox15" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label21" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Name"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox14" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label23" runat="server" Font-Bold="True" ForeColor="Blue" Text="From"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox16" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox16" />
                                            </div>
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="To"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox17" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                                <cc1:CalendarExtender ID="TextBox17_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox17" />
                                            </div>
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Validity"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox18" runat="server" BackColor="#4686F0" Font-Bold="True" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Bal Qty"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox19" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label32" runat="server" Text="Penalty" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox24" runat="server"></asp:TextBox>
                                            </div>

                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Blue" Text="RA Bill No."></asp:Label>
                                            </div>
                                            <div class="col-2 text-start">
                                                <asp:TextBox class="form-control" ID="TextBox25" runat="server"></asp:TextBox>
                                            </div>


                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-1 g-0 text-start">
                                                <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="Blue" Text="Note"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:TextBox class="form-control" ID="TextBox23" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="row align-items-center mt-2 mb-2">
                                            <div class="col-1 text-start">
                                            </div>
                                            <div class="col text-start">
                                                <asp:Button ID="Button52" runat="server" Font-Bold="True" ForeColor="White" Text="View" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button55" runat="server" Font-Bold="True" ForeColor="White" Text="Add Penalty" CssClass="btn btn-primary" />
                                                <asp:Button ID="Button53" runat="server" Font-Bold="True" ForeColor="White" Text="Save" CssClass="btn btn-success" />
                                                <asp:Button ID="Button54" runat="server" Font-Bold="True" ForeColor="White" Text="Cancel" CssClass="btn btn-danger" />


                                            </div>

                                        </div>

                                        <asp:Panel ID="Panel7" runat="server" Style="text-align: left" Visible="False">
                                            <div class="row justify-content-center" style="border-top-color: #FF00FF; border-bottom-color: #FF00FF; border-bottom-style: double; border-top-style: double">
                                                <div class="col-5 text-start">

                                                    <div class="row align-items-center mt-1">
                                                        <div class="col-4 text-start">
                                                            <asp:Label ID="Label46" runat="server" Font-Bold="True" ForeColor="Blue" Text="Enter Password"></asp:Label>
                                                        </div>
                                                        <div class="col text-start">
                                                            <asp:TextBox class="form-control" ID="TextBox31" runat="server" TextMode="Password"></asp:TextBox>
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
                                                        <div class="col text-start">
                                                            <asp:Label ID="Label45" runat="server" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </asp:Panel>

                                    </div>
                                </div>
                                <div class="row align-items-center ms-1 me-1">
                                    <div class="col text-center g-0">
                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered border-2 table-responsive text-center" CellPadding="2" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="wo_slno" HeaderText="Work SlNo" />
                                                <asp:BoundField DataField="w_name" HeaderText="Work Desc" />
                                                <asp:BoundField DataField="AU" HeaderText="A/U" />
                                                <asp:BoundField DataField="from_date" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="to_date" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                <asp:BoundField DataField="work_qty" HeaderText="Worked Unit" />
                                                <asp:BoundField DataField="rqd_qty" HeaderText="Rqd Unit" />
                                                <asp:TemplateField HeaderText="Penality"></asp:TemplateField>
                                                <asp:BoundField DataField="op_qty" HeaderText="Op Qty" />
                                                <asp:BoundField DataField="bal_qty" HeaderText="Bal Unit" />
                                                <asp:TemplateField HeaderText="Note"></asp:TemplateField>
                                            </Columns>
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
