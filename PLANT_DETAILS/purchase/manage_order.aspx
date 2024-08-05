<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="manage_order.aspx.vb" Inherits="PLANT_DETAILS.manage_order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js"></script>
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>


    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label475" runat="server" Text="Add Order Amendment" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid mb-2">
        <div class="row mt-1 justify-content-center text-center">
            <div class="col text-center">
                <div class="row justify-content-center mt-1" style="font-family: 'Times New Roman'">
                    <div class="col-10 justify-content-center" style="border-style: Ridge; border-color: Blue; border-width: 2px">

                        <div class="row align-items-center mt-2">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label620" runat="server" Font-Bold="True" ForeColor="Blue" Text="AMD. No"></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:TextBox class="form-control" ID="TextBox809" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-2 text-start">
                                <asp:Label ID="Label621" runat="server" Font-Bold="True" ForeColor="Blue" Text="Effective Date"></asp:Label>
                            </div>
                            <div class="col-2 text-start">
                                <asp:TextBox class="form-control" ID="TextBox810" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:CalendarExtender ID="TextBox27_CalendarExtender" runat="server" CssClass="red" Format="dd-MM-yyyy" Enabled="True" TargetControlID="TextBox810"></asp:CalendarExtender>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label468" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order Type"></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:DropDownList class="form-select" ID="DropDownList3" runat="server" AutoPostBack="True">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Purchase Order</asp:ListItem>
                                    <asp:ListItem>Sale Order</asp:ListItem>
                                    <asp:ListItem>Work Order</asp:ListItem>
                                    <asp:ListItem>Rate Contract</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col text-start">
                                <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label463" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order No"></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:DropDownList class="form-select" ID="DropDownList4" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label466" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order Line No."></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:DropDownList class="form-select" ID="DropDownList2" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-3 text-start">
                                <asp:Label ID="Label476" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat. Code / Work Desc."></asp:Label>
                            </div>
                            <div class="col-3 text-start">
                                <asp:DropDownList class="form-select" ID="DropDownList57" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col text-start">
                                <asp:Label ID="Label622" runat="server"></asp:Label>
                            </div>

                        </div>


                        <div class="row align-items-center mt-1">
                            <div class="col text-start">
                                <asp:MultiView ID="MultiView1" runat="server">

                                    <%--=====VIEW 1 Purchase Order Amendment START=====--%>
                                    <asp:View ID="View1" runat="server">
                                        <div class="row align-items-center">
                                            <%--==================Left Content==========================--%>
                                            <div class="col text-center" style="border-right: 5px groove #808080; border-bottom: 5px groove #808080;">
                                                <div class="row text-white mt-0" style="background: #4686F0">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label467" runat="server" Text="Previous Value" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center mt-1">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label469" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Qty."></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox1" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label499" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label470" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox2" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label501" runat="server" Text="INR"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label477" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox747" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label487" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label478" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing/Forwd."></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox748" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label488" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label479" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox749" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label489" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label480" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox750" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label490" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox3" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label3" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox4" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label5" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label481" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox751" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label491" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label471" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Validity Date"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox744" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label492" runat="server" Text="dd-mm-yyyy"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--==================Right Content==========================--%>

                                            <div class="col text-center" style="border-bottom: 5px groove #808080;">

                                                <div class="row text-white mt-0" style="background: #4686F0">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label34" runat="server" Text="Amended Value" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center mt-1">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label472" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Qty."></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox745" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label500" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label473" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox5" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label502" runat="server" Text="INR"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label482" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox752" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label498" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label483" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing/Forwd."></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox753" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label497" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox6" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label7" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox7" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label9" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox8" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label11" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox9" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label13" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label486" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox756" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label494" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label474" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Validity Date"></asp:Label>
                                                    </div>
                                                    <div class="col-6 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox746" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label493" runat="server" Text="dd-mm-yyyy"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row align-items-center mt-1 mb-1">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Name"></asp:Label>
                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label503" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>
                                            <div class="col text-end">

                                                <asp:Button ID="Button63" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="Button64" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                <asp:Button ID="Button65" runat="server" Text="Close" CssClass="btn btn-danger" />
                                            </div>

                                        </div>



                                        <div class="row text-white mt-0" style="background: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label464" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" Style="text-align: center" Text="Order Details" Width="100%"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col text-center g-1">
                                                <asp:Panel ID="Panel38" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView212" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered border-2 table-responsive text-center" ShowHeaderWhenEmpty="True" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="PO_NO" HeaderText="Order No." />
                                                            <asp:BoundField DataField="AMD_NO" HeaderText="Amd. No." />
                                                            <asp:BoundField DataField="MAT_SLNO" HeaderText="SLNo." />
                                                            <asp:BoundField DataField="MAT_CODE" HeaderText="Mat. Code" />
                                                            <asp:BoundField DataField="MAT_NAME" HeaderText="Mat. Name" />
                                                            <asp:BoundField DataField="MAT_AU" HeaderText="Acc. Unit" />
                                                            <asp:BoundField DataField="MAT_QTY" HeaderText="Qty." />
                                                            <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Price" />
                                                            <asp:BoundField DataField="MAT_PACK" HeaderText="Packing &amp; fowrd." />
                                                            <asp:BoundField DataField="MAT_DISCOUNT" HeaderText="Discount" />
                                                            <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                            <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                            <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                            <asp:BoundField DataField="CESS" HeaderText="CESS" />
                                                            <asp:BoundField DataField="MAT_FREIGHT_PU" HeaderText="Freight" />
                                                            <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField DataField="AMD_DATE" HeaderText="Efective Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>

                                        </div>

                                    </asp:View>

                                    <%--=====VIEW 2 Sale Order Amndment START=====--%>
                                    <asp:View ID="View2" runat="server">
                                        <div class="row align-items-center">
                                            <%--==================Left Content==========================--%>
                                            <div class="col text-center" style="border-right: 5px groove #808080; border-bottom: 5px groove #808080;">
                                                <div class="row text-white mt-0" style="background: #4686F0">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label35" runat="server" Text="Previous Value" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center mt-1">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label505" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Qty."></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox757" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label506" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label507" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox758" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label508" Font-Bold="True" ForeColor="Blue" runat="server" Text="INR"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label509" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox759" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label510" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label511" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing/Forwd."></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox760" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label512" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label513" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox761" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label514" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label515" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox762" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label516" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox10" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label15" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label16" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox11" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label17" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label612" runat="server" Font-Bold="True" ForeColor="Blue" Text="Terminal Tax"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox805" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label613" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label614" runat="server" Font-Bold="True" ForeColor="Blue" Text="TCS"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox806" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label615" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label517" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox763" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label518" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label519" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Validity Date"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox764" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col text-start g-0">
                                                        <asp:Label ID="Label520" runat="server" Font-Bold="True" ForeColor="Blue" Text="dd-mm-yyyy"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                            <%--==================Right Content==========================--%>

                                            <div class="col text-center" style="border-bottom: 5px groove #808080;">

                                                <div class="row text-white mt-0" style="background: #4686F0">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label56" runat="server" Text="Amended Value" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center mt-1">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label522" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Qty."></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox765" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label523" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label524" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox766" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label525" runat="server" Font-Bold="True" ForeColor="Blue" Text="INR"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label526" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox767" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label527" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label528" runat="server" Font-Bold="True" ForeColor="Blue" Text="Packing/Forwd."></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox768" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label529" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label530" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox769" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label531" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label532" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox770" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label533" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox12" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label19" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label20" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox13" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label21" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label616" runat="server" Font-Bold="True" ForeColor="Blue" Text="Terminal Tax"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox807" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label617" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label618" runat="server" Font-Bold="True" ForeColor="Blue" Text="TCS"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox808" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label619" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label534" runat="server" Font-Bold="True" ForeColor="Blue" Text="Freight"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox771" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label535" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label536" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Validity Date"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox772" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col text-start g-0">
                                                        <asp:Label ID="Label537" runat="server" Font-Bold="True" ForeColor="Blue" Text="dd-mm-yyyy"></asp:Label>
                                                    </div>
                                                </div>



                                            </div>

                                        </div>
                                        <div class="row align-items-center mt-1 mb-1">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label623" runat="server" Font-Bold="True" ForeColor="Blue" Text="Item Details"></asp:Label>
                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label538" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>
                                            <div class="col text-end">

                                                <asp:Button ID="Button66" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="Button68" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                <asp:Button ID="Button67" runat="server" Text="Close" CssClass="btn btn-danger" />
                                            </div>
                                        </div>



                                        <div class="row text-white mt-0" style="background: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label36" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" Style="text-align: center" Text="Order Details" Width="100%"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col text-center g-1">
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView213" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered border-2 table-responsive text-center" ShowHeaderWhenEmpty="True" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="SO_NO" HeaderText="Order No." />
                                                            <asp:BoundField DataField="AMD_NO" HeaderText="Amd. No." />
                                                            <asp:BoundField DataField="ITEM_SLNO" HeaderText="SLNo." />
                                                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Mat. Code" />
                                                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Mat. Name" />
                                                            <asp:BoundField DataField="ITEM_AU" HeaderText="Acc. Unit" />
                                                            <asp:BoundField DataField="ITEM_QTY" HeaderText="Qty." />
                                                            <asp:BoundField DataField="ITEM_MT" HeaderText="Qty. (Mt)" />
                                                            <asp:BoundField DataField="ITEM_UNIT_RATE" HeaderText="Unit Price" />
                                                            <asp:BoundField DataField="ITEM_PACK" HeaderText="Packing &amp; fowrd." />
                                                            <asp:BoundField DataField="ITEM_DISCOUNT" HeaderText="Discount" />
                                                            <asp:BoundField DataField="ITEM_SGST" HeaderText="SGST" />
                                                            <asp:BoundField DataField="ITEM_CGST" HeaderText="CGST" />
                                                            <asp:BoundField DataField="ITEM_IGST" HeaderText="SGST" />
                                                            <asp:BoundField DataField="ITEM_CESS" HeaderText="CGST" />
                                                            <asp:BoundField DataField="ITEM_TERMINAL_TAX" HeaderText="Terminal Tax" />
                                                            <asp:BoundField DataField="ITEM_TCS" HeaderText="TCS" />
                                                            <asp:BoundField DataField="ITEM_S_TAX" HeaderText="Service Tax" />
                                                            <asp:BoundField DataField="ITEM_FREIGHT_PU" HeaderText="Freight" />
                                                            <asp:BoundField DataField="ITEM_DELIVERY" HeaderText="Delivery Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField DataField="AMD_DATE" HeaderText="Efective Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </asp:View>

                                    <%--=====VIEW 3 Work Order Amendment START=====--%>
                                    <asp:View ID="View3" runat="server">
                                        <div class="row align-items-center">
                                            <%--==================Left Content==========================--%>
                                            <div class="col text-center" style="border-right: 5px groove #808080; border-bottom: 5px groove #808080;">
                                                <div class="row text-white mt-0" style="background: #4686F0">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label37" runat="server" Text="Previous Value" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center mt-1">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label541" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Qty."></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox773" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label542" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label543" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox774" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label544" Font-Bold="True" ForeColor="Blue" runat="server" Text="INR"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label547" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox776" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label548" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label551" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox778" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label552" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label553" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox779" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label554" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label22" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox14" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label23" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox15" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label25" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>



                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label555" runat="server" Font-Bold="True" ForeColor="Blue" Text="Old Validity Date"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox780" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                                                    </div>
                                                    <div class="col text-start g-0">
                                                        <asp:Label ID="Label556" runat="server" Font-Bold="True" ForeColor="Blue" Text="dd-mm-yyyy"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                            <%--==================Right Content==========================--%>

                                            <div class="col text-center" style="border-bottom: 5px groove #808080;">

                                                <div class="row text-white mt-0" style="background: #4686F0">
                                                    <div class="col text-center">
                                                        <asp:Label ID="Label63" runat="server" Text="Amended Value" Font-Bold="True" Font-Size="Larger"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center mt-1">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label558" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Qty."></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox781" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label559" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label560" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Unit Price"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox782" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label561" runat="server" Font-Bold="True" ForeColor="Blue" Text="INR"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label564" runat="server" Font-Bold="True" ForeColor="Blue" Text="Discount"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox784" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label565" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label566" runat="server" Font-Bold="True" ForeColor="Blue" Text="SGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox785" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label567" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label568" runat="server" Font-Bold="True" ForeColor="Blue" Text="CGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox786" runat="server">0.00</asp:TextBox>
                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label569" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label570" runat="server" Font-Bold="True" ForeColor="Blue" Text="IGST"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox787" runat="server">0.00</asp:TextBox>

                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label571" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="CESS"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox16" runat="server">0.00</asp:TextBox>

                                                    </div>
                                                    <div class="col-2 text-start g-0">
                                                        <asp:Label ID="Label27" Font-Bold="True" ForeColor="Blue" runat="server">%</asp:Label>
                                                    </div>
                                                </div>

                                                <div class="row align-items-center">
                                                    <div class="col-4 text-start">
                                                        <asp:Label ID="Label572" runat="server" Font-Bold="True" ForeColor="Blue" Text="New Validity Date"></asp:Label>
                                                    </div>
                                                    <div class="col-5 text-start">
                                                        <asp:TextBox class="form-control" ID="TextBox788" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col text-start g-0">
                                                        <asp:Label ID="Label573" runat="server" Font-Bold="True" ForeColor="Blue" Text="dd-mm-yyyy"></asp:Label>
                                                    </div>
                                                </div>



                                            </div>

                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label32" runat="server" Font-Bold="True" ForeColor="Blue" Text="Work Description"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:Label ID="Label574" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="Blue" Text="Order Type"></asp:Label>
                                            </div>
                                            <div class="col text-start">
                                                <asp:Label ID="Label29" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1 mb-1">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label30" runat="server" Font-Bold="True" ForeColor="Blue" Text="Supplier ID"></asp:Label>
                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label31" Font-Bold="True" ForeColor="Blue" runat="server"></asp:Label>
                                            </div>
                                            <div class="col text-end">

                                                <asp:Button ID="Button69" runat="server" Text="Save" CssClass="btn btn-success" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                                <asp:Button ID="Button71" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                <asp:Button ID="Button70" runat="server" Text="Close" CssClass="btn btn-danger" />
                                            </div>
                                        </div>



                                        <div class="row text-white mt-0" style="background: #4686F0">
                                            <div class="col text-center">
                                                <asp:Label ID="Label90" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" Style="text-align: center" Text="Order Details" Width="100%"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-1">
                                            <div class="col text-center g-1">
                                                <asp:Panel ID="Panel43" runat="server" ScrollBars="Auto">
                                                    <asp:GridView ID="GridView214" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered border-2 table-responsive text-center" ShowHeaderWhenEmpty="True" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField DataField="PO_NO" HeaderText="Order No." />
                                                            <asp:BoundField DataField="WO_AMD" HeaderText="Amd. No." />
                                                            <asp:BoundField DataField="W_SLNO" HeaderText="SLNo." />
                                                            <asp:BoundField DataField="W_AU" HeaderText="Acc. Unit" />
                                                            <asp:BoundField DataField="W_QTY" HeaderText="Qty." />
                                                            <asp:BoundField DataField="W_UNIT_PRICE" HeaderText="Unit Price" />
                                                            <asp:BoundField DataField="W_DISCOUNT" HeaderText="Discount" />
                                                            <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                            <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                            <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                            <asp:BoundField DataField="CESS" HeaderText="CESS" />
                                                            <asp:BoundField DataField="W_END_DATE" HeaderText="Validity" DataFormatString="{0:dd/MM/yyyy}" />
                                                            <asp:BoundField DataField="AMD_DATE" HeaderText="Effective Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </div>
                                        </div>

                                    </asp:View>
                                </asp:MultiView>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
























    <%--<center>
        <div runat ="server" style ="min-height :600px;">
            <asp:Panel ID="amd_Panel" runat="server" BorderColor="Fuchsia" BorderStyle="Groove" Font-Names="Times New Roman" ForeColor="Blue" style="text-align: left" Width="815px" CssClass="brds">
                
            
                <asp:Panel ID="Panel44" runat="server" Visible="False">
                    <div style="border-bottom: 5px double #0000FF; width: 810px; background-color: #0000FF; height: 228px;">
                        <div runat="server" style="float :left; width: 400px; background-color: #FFFFFF; height: 214px;" class="brds">
                            <asp:Label ID="Label576" runat="server" BackColor="#4686F0" ForeColor="White" Height="27px" style="text-align: center" Text="Previous Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            <br /> <asp:Label ID="Label577" runat="server" Font-Bold="False" Text="Old Qty." ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox789" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label578" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label579" runat="server" Font-Bold="False" Text="Old Unit Price" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox790" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label580" runat="server"  Text="INR"></asp:Label>
                          <br />
                            <asp:Label ID="Label581" runat="server" Text="Discount" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox791" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label582" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label583" runat="server" Text="Packing/ forwd." ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox792" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label584" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label585" runat="server" Text="Excise Duty" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox793" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label586" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label587" runat="server" Text="Vat / C.S.T." ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox794" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label588" runat="server" >PERCENTAGE</asp:Label>
                          <br />
                            <asp:Label ID="Label589" runat="server" Text="Freight" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox795" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label590" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label591" runat="server" Font-Bold="False" Text="Old Validity Date" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox796" runat="server" BackColor="#4686F0" ForeColor="White" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label592" runat="server"  Text="DD-mm-YYYY"></asp:Label>
                          <br />
                        </div>
                        <div runat="server" style="float:right; width: 400px; background-color: #FFFFFF; height: 214px;" class="brds">
                            <asp:Label ID="Label593" runat="server" BackColor="#4686F0" ForeColor="White" Height="28px" style="text-align: center" Text="Amendment Value" Width="100%" CssClass="brds"></asp:Label>
                          <br />
                            <br /> <asp:Label ID="Label594" runat="server" Font-Bold="False" Text="New Qty." ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox797" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label595" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label596" runat="server" Font-Bold="False" Text="New Unit Price" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox798" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label597" runat="server"  Text="INR"></asp:Label>
                          <br />
                            <asp:Label ID="Label598" runat="server" Text="Discount" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox799" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label599" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label600" runat="server" Text="Packing/ forwd." ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox800" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label601" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label602" runat="server" Text="Excise Duty" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox801" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label603" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label604" runat="server" Text="Vat / C.S.T." ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox802" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label605" runat="server" >PERCENTAGE</asp:Label>
                          <br />
                            <asp:Label ID="Label606" runat="server" Text="Freight" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox803" runat="server">0.00</asp:TextBox>
                            <asp:Label ID="Label607" runat="server" ></asp:Label>
                          <br />
                            <asp:Label ID="Label608" runat="server" Font-Bold="False" Text="New Validity Date" ></asp:Label>
                            <asp:TextBox class="form-control" ID="TextBox804" runat="server"></asp:TextBox>
                         
                            <asp:Label ID="Label609" runat="server"  Text="DD-mm-YYYY"></asp:Label>
                          <br />
                        </div>
                    </div>
                    <div runat="server" style="float:left; width :600px; height: 78px;">
                        <asp:Label ID="Label610" runat="server"></asp:Label>
                    </div>
                    <div runat="server" style="float:right ">
                        <asp:Button ID="Button72" runat="server" Text="CLOSE"  CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button73" runat="server" Text="CANCEL"  CssClass="bottomstyle" />
                      <br />
                        <asp:Button ID="Button74" runat="server" Text="SAVE"  CssClass="bottomstyle" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                    </div>
                  <br />
                    <asp:Label ID="Label611" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="Medium" ForeColor="White" style="text-align: center" Text="ORDER STATUS" Width="100%"></asp:Label>
                    <asp:Panel ID="Panel45" runat="server" ScrollBars="Auto" >
                        <asp:GridView ID="GridView215" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="PO_NO" HeaderText="Order No." />
                                <asp:BoundField DataField="AMD_NO" HeaderText="Amd. No." />
                                <asp:BoundField DataField="MAT_SLNO" HeaderText="SLNo." />
                                <asp:BoundField DataField="MAT_CODE" HeaderText="Mat. Code" />
                                <asp:BoundField DataField="MAT_NAME" HeaderText="Mat. Name" />
                                <asp:BoundField DataField="MAT_AU" HeaderText="Acc. Unit" />
                                <asp:BoundField DataField="MAT_QTY" HeaderText="Qty." />
                                <asp:BoundField DataField="MAT_UNIT_RATE" HeaderText="Unit Price" />
                                <asp:BoundField DataField="MAT_PACK" HeaderText="Packing &amp; fowrd." />
                                <asp:BoundField DataField="MAT_DISCOUNT" HeaderText="Discount" />
                                <asp:BoundField DataField="MAT_EXCISE_DUTY" HeaderText="Excise Duty" />
                                <asp:BoundField DataField="MAT_CST" HeaderText="Vat / C.S.T" />
                                <asp:BoundField DataField="MAT_FREIGHT_PU" HeaderText="Freight" />
                                <asp:BoundField DataField="MAT_DELIVERY" HeaderText="Delivery Date" />
                                <asp:BoundField />
                                <asp:BoundField DataField="AMD_DATE" HeaderText="Efective Date" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </asp:Panel>
<br />
        </div>
    </center>--%>
</asp:Content>
