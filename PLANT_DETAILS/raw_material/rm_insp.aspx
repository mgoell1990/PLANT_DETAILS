<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="rm_insp.aspx.vb" Inherits="PLANT_DETAILS.rm_insp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <%--<link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />--%>



    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $("[id$=be_TextBox]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/BE")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    bedate: item.split('^')[1],
                                    blno: item.split('^')[2],
                                    bldate: item.split('^')[3],
                                    shipname: item.split('^')[4]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=bedate_TextBox]").val(i.item.bedate);
                    $("[id$=bl_noTextBox]").val(i.item.blno);
                    $("[id$=bldate_TextBox]").val(i.item.bldate);
                    $("[id$=shipnameTextBox]").val(i.item.shipname);
                },
                minLength: 1
            });
        });
    </script>

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Raw Material Inspection" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>



    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10 text-center">

                <asp:Panel ID="Panel1" runat="server" BorderColor="Blue" BorderStyle="Groove" Font-Names="Times New Roman" Visible="False">
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label465" runat="server" ForeColor="Blue" Text="CRR No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="CRR_DropDownList" CssClass="form-select" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1">
                            <asp:Label ID="Label3" runat="server" ForeColor="Blue" Text="PO No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="po_no_TextBox" runat="server" CssClass="form-control" BackColor="#4686F0" ForeColor="White" ></asp:TextBox>
                        </div>
                        <div class="col-3">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </div>

                    </div>
                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label22" runat="server" ForeColor="Blue" Text="Mat SL No"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="MATCODE_DropDownList" CssClass="form-select" runat="server" AutoPostBack="True" Font-Names="Times New Roman">
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 g-0 text-end">
                            <asp:Label ID="Label24" runat="server" ForeColor="Blue" Text="Mat. Name"></asp:Label>
                        </div>
                        <div class="col-4">
                            <asp:TextBox ID="TextBox184" CssClass="form-control" runat="server" BackColor="#4686F0" ForeColor="White" ></asp:TextBox>
                        </div>
                        <div class="col-1 text-end">
                            <asp:Label ID="Label6" runat="server" ForeColor="Blue" Text="A/U"></asp:Label>

                        </div>
                        <div class="col-1 text-end">
                            <asp:TextBox ID="AU_GRANTextBox" CssClass="form-control" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Names="Times New Roman" ForeColor="White" ></asp:TextBox>
                        </div>

                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label31" runat="server" ForeColor="Blue" Text="Received Qty" ></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="RCVDQTY_TextBox" runat="server" CssClass="form-control" BackColor="Red" ForeColor="White"  Font-Names="Times New Roman"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label33" runat="server" ForeColor="Blue" Text="Qty to be Rejected"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:TextBox ID="GARN_REJQTYTextBox" runat="server" CssClass="form-control" Font-Names="Times New Roman"></asp:TextBox>
                        </div>

                    </div>


                    <div class="row align-items-center m-1">

                        <div class="col-2 text-start">
                            <asp:Label ID="Label35" runat="server" ForeColor="Blue" Text="Note"></asp:Label>
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
                                CellPadding="3" ForeColor="Black" GridLines="Both" Style="text-align: center; width: 100%; height: 75px; font-size:medium" class="table table-bordered text-center">
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
