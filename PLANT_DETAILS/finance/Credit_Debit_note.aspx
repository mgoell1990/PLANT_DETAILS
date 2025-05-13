<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Credit_Debit_note.aspx.vb" Inherits="PLANT_DETAILS.Credit_Debit_note1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=txtContactsSearch]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/inv_no")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],

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
                },
                minLength: 1
            });
        });
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label94" runat="server" Text="Credit/Debit Note" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="row align-items-center mt-1">
        <div class="col-5 text-end">
            <asp:Label ID="Label53" runat="server" Text="Select"></asp:Label>

        </div>
        <div class="col-3 text-end">
            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-select" Width="150px" AutoPostBack="True">
                <asp:ListItem>Select</asp:ListItem>
                <asp:ListItem>Credit Note</asp:ListItem>
                <asp:ListItem>Debit Note</asp:ListItem>
                <asp:ListItem>Print</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-2 text-end">
            <asp:Label ID="Label1" runat="server" Text="Invoice Date"></asp:Label>

        </div>
        <div class="col-2 text-end">
            <asp:TextBox ID="txtInvoiceDate" class="form-control" runat="server"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="txtInvoiceDate" />
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">

                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div class="row justify-content-center">
                            <div class="col-12 justify-content-center m-1" style="border-color: Blue; border-style: groove">

                                <div class="row  align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label629" runat="server" ForeColor="Blue" Text="IRN No"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start ">
                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Label ID="Label455" runat="server" ForeColor="Red"></asp:Label>
                                    </div>

                                </div>

                                <div class="row  align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label451" runat="server" ForeColor="Blue" Text="CREDIT/DEBIT NOTE NO."></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:TextBox class="form-control" ID="TextBox65" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row  align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label449" runat="server" ForeColor="Blue" Text="Fiscal Year"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:DropDownList ID="DropDownList3" class="form-select" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                </div>


                                <div class="row  align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label507" runat="server" ForeColor="Blue" Text="Invoice No."></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">

                                        <asp:DropDownList ID="DropDownList4" class="form-select" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <div class="row  align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label453" runat="server" ForeColor="Blue" Text="Invoice Type"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="True" Style="text-align: left"></asp:Label>
                                    </div>

                                </div>

                                <div class="row  align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label639" runat="server" ForeColor="Blue" Text="A/c Unit"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:Label ID="Label320" runat="server" Font-Bold="True" Style="text-align: left"></asp:Label>
                                    </div>



                                </div>

                                <div class="row  align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label510" runat="server" ForeColor="Blue" Text="Taxable Amount"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:TextBox class="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                                    </div>

                                </div>

                                <div class="row align-items-center mt-1">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label318" runat="server" ForeColor="Blue" Text="Notification"></asp:Label>
                                    </div>
                                    <div class="col-6 text-start ">
                                        <asp:TextBox class="form-control" ID="TextBox66" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button1" runat="server" Text="Add" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                        <asp:Button ID="Button2" runat="server" Text="Save" CssClass="btn btn-danger" />
                                    </div>
                                </div>

                                <div class="row align-items-center mt-2">
                                    <div class="col" style="overflow: scroll">
                                        <asp:GridView ID="GridView2" Width="100%" CssClass="table table-bordered table-condensed table-responsive text-center" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True">
                                            <Columns>
                                                <asp:BoundField DataField="PO_NO" HeaderText="PO NO" />
                                                <asp:BoundField DataField="GARN_NO_MB_NO" HeaderText="INVOICE NO" />
                                                <asp:BoundField DataField="SUPL_ID" HeaderText="Supl Code" />
                                                <asp:BoundField DataField="FISCAL_YEAR" HeaderText="FISCAL_YEAR" />
                                                <asp:BoundField DataField="PERIOD" HeaderText="QUARTER" />
                                                <asp:BoundField DataField="EFECTIVE_DATE" HeaderText="EFECTIVE DATE" />
                                                <asp:BoundField DataField="AC_NO" HeaderText="A/C Head" />
                                                <asp:BoundField DataField="AMOUNT_DR" HeaderText="Amount Debit" />
                                                <asp:BoundField DataField="AMOUNT_CR" HeaderText="Amount Credit" />
                                                <asp:BoundField DataField="POST_INDICATION" HeaderText="POST_INDICATION" />

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>


                                <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" Width="100%">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="115%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Invoice No"></asp:TemplateField>
                                            <asp:BoundField DataField="SO_NO" HeaderText="PO No" />
                                            <asp:BoundField DataField="SO_Date" HeaderText="PO Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="PO_NO" HeaderText="Actual PO No" />
                                            <asp:BoundField DataField="PO_Date" HeaderText="Actual PO Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="PARTY_CODE" HeaderText="Party Code" />
                                            <asp:BoundField DataField="CONSIGN_CODE" HeaderText="Consignee Code" />
                                            <asp:BoundField DataField="D_NAME" HeaderText="Party Name" />
                                            <asp:BoundField DataField="MAT_SLNO" HeaderText="Mat Sl. No" />
                                            <asp:BoundField DataField="P_CODE" HeaderText="Mat. Code" />
                                            <asp:BoundField DataField="P_DESC" HeaderText="Mat Name" />
                                            <asp:TemplateField HeaderText="TAXABLE VALUE"></asp:TemplateField>
                                            <asp:BoundField DataField="CGST_RATE" HeaderText="CGST" />
                                            <asp:BoundField DataField="CGST_AMT" HeaderText="CGST AMT" />
                                            <asp:BoundField DataField="SGST_RATE" HeaderText="SGST" />
                                            <asp:BoundField DataField="SGST_AMT" HeaderText="SGST AMT" />
                                            <asp:BoundField DataField="IGST_RATE" HeaderText="IGST" />
                                            <asp:BoundField DataField="IGST_AMT" HeaderText="IGST AMT" />
                                            <asp:BoundField DataField="TOTAL_AMT" HeaderText="TOTAL AMOUNT" />

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                                <div class="row  align-items-center">
                                    <div class="col text-start">
                                        <asp:Label ID="Label3" runat="server" ForeColor="#FF3300" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                                    </div>
                                </div>

                                <div class="row  align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label317" runat="server" Font-Bold="True" ForeColor="Red" Text="Error Code : " Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:Label ID="txtEinvoiceErrorCode" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>

                                </div>

                                <div class="row  align-items-center">
                                    <div class="col-2 text-start">
                                        <asp:Label ID="Label42" runat="server" Font-Bold="True" ForeColor="Red" Text="Error Message : " Visible="False"></asp:Label>
                                    </div>
                                    <div class="col-2 text-start ">
                                        <asp:Label ID="txtEinvoiceErrorMessage" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <asp:Panel ID="Panel1" runat="server" Style="text-align: left">
                            <div class="row justify-content-center">
                                <div class="col-6 justify-content-center m-1" style="border-color: Blue; border-style: groove">
                                    <div class="row align-items-center justify-content-center" style="background-color: #4686F0">
                                        <div class="col text-center">
                                            <asp:Label ID="Label314" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="White" Style="text-align: center" Text="PRINT CREDIT/DEBIT INVOICE"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="row  align-items-center mt-1">
                                        <div class="col-2 text-start">
                                            <asp:Label ID="Label311" runat="server" Font-Bold="True" Text="Invoice No" Font-Names="Times New Roman" ForeColor="Blue"></asp:Label>
                                        </div>
                                        <div class="col-6 text-start ">
                                            <asp:TextBox class="form-control" ID="txtInvSearch" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col text-start">
                                            <asp:Label ID="Label29" runat="server" ForeColor="Red"></asp:Label>
                                        </div>

                                    </div>

                                   

                                    <div class="row  align-items-center mt-1">
                                        <div class="col-2 text-start">
                                        </div>
                                        <div class="col-6 text-start">
                                            <asp:Button ID="Button38" CssClass="btn btn-success" runat="server" Font-Bold="True" Text="Extra Copy" />
                                            <asp:Button ID="Button39" CssClass="btn btn-success" runat="server" Font-Bold="True" Text="Pending Print" />
                                        </div>

                                    </div>


                                    <div class="row  align-items-center mt-1">
                                        <div class="col text-start">
                                            <asp:GridView ID="GridView3" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="Groove" BorderWidth="3px" CellPadding="4" EmptyDataText="No More Invoice To Print" Font-Names="Times New Roman" Font-Size="Smaller" Visible="False">
                                                <Columns>
                                                    <asp:BoundField DataField="INV_NO" HeaderText="INVOICE NO">
                                                        <ItemStyle Font-Bold="True" ForeColor="Blue" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="ORIGINAL">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Button1" ForeColor="BLUE" runat="server" Text='<%#Eval("PRINT_ORIGN")%>' CommandName='<%#Eval("INV_NO")%>' OnClick="ORIGINAL" />
                                                        </ItemTemplate>
                                                        <ItemStyle ForeColor="Blue" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DUPLICATE">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Button2" ForeColor="BLUE" runat="server" Text='<%#Eval("PRINT_TRANS")%>' CommandName='<%#Eval("INV_NO")%>' OnClick="DUPLICATE" />
                                                        </ItemTemplate>
                                                        <ItemStyle ForeColor="Blue" Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TRIPLICATE">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Button3" ForeColor="BLUE" runat="server" Text='<%#Eval("PRINT_ASSAE")%>' CommandName='<%#Eval("INV_NO")%>' OnClick="TRIPLICATE" />
                                                        </ItemTemplate>
                                                        <ItemStyle ForeColor="Blue" Width="100px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                <RowStyle BackColor="White" ForeColor="Blue" />
                                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Blue" />
                                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                            </asp:GridView>
                                        </div>

                                    </div>



                                </div>
                            </div>
                        </asp:Panel>

                    </asp:View>
                </asp:MultiView>

            </div>
        </div>
    </div>


</asp:Content>



























