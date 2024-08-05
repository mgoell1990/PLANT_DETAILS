<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="dpr.aspx.vb" Inherits="PLANT_DETAILS.dpr1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<style type="text/css">
        .auto-style1 {
            height: 280px;
        }
    </style>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label1" runat="server" Text="Daily Production Entry" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center">
            <div class="col-11 text-center" style="border: 3px; border-style: Double; border-color: #4686F0">

                        <div class="row" style="border-bottom: 3px; border-bottom-style: groove; border-bottom-color: #FF3399">
                            <%--=================Left Panel ================================--%>
                            <div class="col-6 text-start" style="border-right: 3px; border-right-style: groove; border-right-color: #FF3399;">

                                <div class="row align-items-center mt-1">
                                    <div class="col-4 text-start">
                                        <asp:Label ID="Label274" runat="server" Text="Mat Group:-" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-4 text-start">
                                        <asp:Label ID="Label271" runat="server" Text="Mat Code:-" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-8">
                                        <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row align-items-center">
                                    <div class="col-4 text-start">
                                        <asp:Label ID="Label273" runat="server" Text="Date:-" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:TextBox class="form-control" ID="TextBox49" runat="server" Font-Bold="True" ForeColor="#FF0066" TabIndex="2" ToolTip="DD-MM-YYYY" AutoCompleteType="Disabled"></asp:TextBox>
                                        <cc1:CalendarExtender ID="TextBox49_CalendarExtender" runat="server" CssClass="red" Enabled="True" Format="dd-MM-yyyy" BehaviorID="TextBox49_CalendarExtender" TargetControlID="TextBox49" />
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label278" runat="server" Text="(DD-MM-YYYY)" ForeColor="Blue" Font-Size="Small"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center">
                                    <div class="col-4 text-start">
                                        <asp:Label ID="Label272" runat="server" Font-Bold="True" ForeColor="Blue" Text="Production Qty:-"></asp:Label>
                                    </div>
                                    <div class="col-5">
                                        <asp:TextBox class="form-control" ID="TextBox1" runat="server">0.000</asp:TextBox>
                                    </div>
                                    <div class="col-3 text-start g-0">
                                        <asp:Label ID="Label276" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                    </div>
                                </div>

                                <div class="row align-items-center mt-2">
                                    <div class="col text-start">
                                        <asp:Label ID="Label279" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>



                                <div class="row align-items-center mt-2">
                                    <div class="col-4 text-start">
                                        
                                    </div>
                                    <div class="col text-start">
                                        <asp:Button ID="Button34" runat="server" CssClass="btn btn-primary fw-bold" Text="Add" />
                                        <asp:Button ID="Button32" runat="server" CssClass="btn btn-success fw-bold" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                        <asp:Button ID="Button33" runat="server" CssClass="btn btn-danger fw-bold" Text="Cancel" />
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success fw-bold" Text="Print" UseSubmitBehavior="false" />

                                    </div>

                                </div>
                            </div>
                            <%--=================Right Panel ================================--%>
                            <div class="col-6">
                                <div class="row">
                                    <div class="col g-0 text-start mb-1">
                                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" Width="100%" CssClass="DataWebControlStyle" BackColor="White" ForeColor="black">
                                            <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                            <FieldHeaderStyle CssClass="HeaderStyle" />
                                            <Fields>
                                                <asp:BoundField DataField="ITEM_CODE" HeaderText="Mat Code" SortExpression="ITEM_CODE">
                                                    <HeaderStyle />
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ITEM_NAME" HeaderText="Mat Name" SortExpression="ITEM_NAME" />
                                                <asp:BoundField DataField="ITEM_DRAW" HeaderText="Mat draw" SortExpression="ITEM_DRAW" />
                                                <asp:BoundField DataField="ITEM_AU" HeaderText="Mat AU" SortExpression="ITEM_AU" />
                                                <asp:BoundField DataField="ITEM_WEIGHT" HeaderText="Mat Unit Weight(Kg)" SortExpression="ITEM_WEIGHT" />
                                                <asp:BoundField DataField="ITEM_F_STOCK" HeaderText="Current Stock" SortExpression="ITEM_F_STOCK" />
                                                <asp:TemplateField HeaderText="Current Stock Weight (Mt)">
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ITEM_LAST_PROD" HeaderText="Last Production(Dt)" SortExpression="ITEM_LAST_PROD" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="ITEM_LAST_DESPATCH" HeaderText="Last Despatch(Dt)" SortExpression="ITEM_LAST_DESPATCH" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="cmul_qty" HeaderText="Cummulative Of This Month(Mt)" />
                                                <asp:TemplateField HeaderText="Quality">
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                            </Fields>
                                            <RowStyle CssClass="RowStyle" />
                                        </asp:DetailsView>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <div class="row align-items-center mt-1">
                            <div class="col g-0">
                                <asp:Panel ID="Panel16" runat="server" ScrollBars="Auto">
                                    <asp:GridView ID="GridView1" Style="font-size: 15px" CssClass="table table-bordered border-2 table-responsive text-center" runat="server" AutoGenerateColumns="False" CellPadding="2" ShowHeaderWhenEmpty="false">
                                        <Columns>
                                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Mat Code" />
                                            <asp:BoundField DataField="ITEM_NAME" HeaderText="Mat Name" />
                                            <asp:BoundField DataField="ITEM_AU" HeaderText="Mat AU" />
                                            <asp:BoundField DataField="ITEM_PROD_DATE" HeaderText="Date" />
                                            <asp:BoundField DataField="ITEM_QTY" HeaderText="Production Qty" />
                                            <asp:BoundField DataField="fr_mt" HeaderText="Production (Mt)" />
                                        </Columns>
                                        <AlternatingRowStyle BackColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="true" Font-Size="Medium" ForeColor="White" />
                                        <RowStyle BackColor="#f5f5f5" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>

                        </div>
            </div>
        </div>
    </div>






















<%--
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
        <script>
            if (window.history.replaceState) {
                window.history.replaceState(null, null, window.location.href);
            }
</script>

        

        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
            <asp:Panel ID="Panel1" runat="server" BorderColor="#4686F0" BorderStyle="Double" Font-Names="Times New Roman" Font-Size="Small" Width="1000px" style="text-align: left" CssClass="brds">
              
               <div runat ="server" style="border-bottom: 10px double #008000; " class="auto-style1" >
              <div style ="border-right: 10px double #008000; float :left; width :439px; height: 280px; " runat ="server" >
                  
                  
                  
                  
                  </div>
                   <div style ="float :right; width :550px; height: 279px;" runat ="server" >

                       

                  </div>
                   
              </div>
 <asp:GridView ID="" runat="server"  CssClass="DataWebControlStyle" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                 <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                                <RowStyle CssClass="RowStyle" BackColor="White" ForeColor="#330099" />
                                 <HeaderStyle CssClass="HeaderStyle" BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                 <Columns>
                                     <asp:BoundField DataField="ITEM_CODE" HeaderText="Mat Code" />
                                     <asp:BoundField DataField="ITEM_NAME" HeaderText="Mat Name" />
                                     <asp:BoundField DataField="ITEM_AU" HeaderText="Mat AU" />
                                     <asp:BoundField DataField="ITEM_PROD_DATE" HeaderText="Date" />
                                     <asp:BoundField DataField="ITEM_QTY" HeaderText="Production Qty" />
                                     <asp:BoundField DataField="fr_mt" HeaderText="Production (Mt)" />
                                 </Columns>
                                 <FooterStyle CssClass="FooterStyle" BackColor="#FFFFCC" ForeColor="#330099" />
                                 <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                 <PagerStyle CssClass="PagerRowStyle" BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                  <PagerSettings PageButtonCount="5" />
                                 <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                 <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                 <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                 <SortedDescendingHeaderStyle BackColor="#7E0000" />
              </asp:GridView>
          </asp:Panel>





            </div> 
        </center>--%>
</asp:Content>
