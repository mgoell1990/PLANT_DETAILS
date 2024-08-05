<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="p_report.aspx.vb" Inherits="PLANT_DETAILS.p_report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Production Report" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col-6">
                <div class="row align-items-center">
                    <div class="col-5 text-end">
                        <asp:Label ID="Label2" runat="server" Text="From" Font-Bold="True" ForeColor="blue" />
                    </div>
                    <div class="col-4 text-start">
                        <asp:TextBox CssClass="form-control" ID="date1" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="date1_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox1_CalendarExtender" TargetControlID="date1" />
                    </div>
                </div>

                <div class="row align-items-center">
                    <div class="col-5 text-end">
                        <asp:Label ID="Label1" runat="server" Text="To" Font-Bold="True" ForeColor="blue" />
                    </div>
                    <div class="col-4 text-start">
                        <asp:TextBox CssClass="form-control" ID="date2" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="date2_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox2_CalendarExtender" TargetControlID="date2" />
                    </div>
                    
                </div>

                <div class="row align-items-center mt-1">
                    <div class="col-5 text-end">
                        
                    </div>
                   
                    <div class="col-4 text-start">
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary"></asp:Button>
                    </div>

                </div>



            </div>
        </div>

        <%--<div class="row align-items-center mt-3">
            <div class="col">
                <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-responsive text-center me-2" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle ForeColor="Blue" Width="30px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RET_STATUS" HeaderText="Rej. Reff No" />
                        <asp:BoundField DataField="CRR_NO" HeaderText="CRR No" />
                        <asp:BoundField DataField="PO_NO" HeaderText="PO No" />
                        <asp:BoundField HeaderText="Mat. Code" DataField="MAT_CODE" />
                        <asp:BoundField HeaderText="Mat. Name" DataField="MAT_NAME" />
                        <asp:BoundField HeaderText="Mat SlNo" DataField="MAT_SLNO" />
                        <asp:BoundField HeaderText="Chln. Qty" DataField="MAT_CHALAN_QTY" />
                        <asp:BoundField HeaderText="Rcvd. Qty" DataField="MAT_RCD_QTY" />
                        <asp:BoundField HeaderText="Qty. Acptd" DataField="ACCPT_QTY" />
                        <asp:BoundField HeaderText="Rej. Qty" DataField="MAT_REJ_QTY" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>

        <div class="row align-items-center">
            <asp:Label ID="Label4" runat="server" ForeColor="#FF3300" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
        </div>--%>
    </div>




   
</asp:Content>
