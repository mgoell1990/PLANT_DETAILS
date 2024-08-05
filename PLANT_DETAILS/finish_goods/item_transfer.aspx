<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback ="true"  MasterPageFile="~/Site.Master" CodeBehind="item_transfer.aspx.vb" Inherits="PLANT_DETAILS.item_transfer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
    if ( window.history.replaceState ) {
        window.history.replaceState( null, null, window.location.href );
    }
</script>
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif; "  >
            <asp:Panel ID="Panel1" runat="server" BorderColor="#4686F0" BorderStyle="Double" Font-Names="Times New Roman" Font-Size="Small" Width="1000px" style="text-align: left" CssClass="brds">
              <asp:Label ID="Label270" runat="server" BackColor="Blue" Font-Bold="True" Font-Size="Large" ForeColor="White" Text="ITEM STOCK TRANSFER CUM ADJUST" Width="100%" CssClass="brds"></asp:Label>
              <br />
               <div runat ="server" style=" height: 280px; " >
              <div style =" float :left; width :443px; height: 280px; " runat ="server" >
                  <asp:Label ID="Label288" runat="server" Font-Bold="True" ForeColor="Blue" Text="Trans. Type:-" Width="100px"></asp:Label>
                  <asp:DropDownList ID="DropDownList5" runat="server" Width="150px" AutoPostBack="True">
                      <asp:ListItem>Select</asp:ListItem>
                      <asp:ListItem>Transfer</asp:ListItem>
                      <asp:ListItem>Phy. Adjust</asp:ListItem>
                  </asp:DropDownList>
                   <br />
                  <asp:Panel ID="Panel3" runat="server" Visible="False">
                        <asp:Label ID="Label291" runat="server" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Red">FROM:-</asp:Label>
                  <br />
                  <asp:Label ID="Label274" runat="server" Text="Mat Group:-" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <asp:DropDownList ID="DropDownList2" runat="server" Width="250px" AutoPostBack="True">
                  </asp:DropDownList>
                  <br />
              <asp:Label ID="Label271" runat="server" Text="Mat Code:-" Width="100px" Font-Bold="True" ForeColor="Blue"></asp:Label>
              <asp:DropDownList ID="DropDownList1" runat="server" Width="250px" AutoPostBack="True">
              </asp:DropDownList>
              <br />
                 
                  <asp:Label ID="Label272" runat="server" Font-Bold="True" ForeColor="Blue" Text="F.R. Qty:-" Width="100px"></asp:Label>
                  <asp:TextBox ID="TextBox1" runat="server" Width="150px" BackColor="Blue" Enabled="False" ForeColor="White">0.000</asp:TextBox>
                  <asp:Label ID="Label276" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <br />
                  <asp:Label ID="Label275" runat="server" Font-Bold="True" ForeColor="Blue" Text="B.S.R. Qty:-" Width="100px"></asp:Label>
                  <asp:TextBox ID="TextBox50" runat="server" Width="150px" BackColor="Blue" Enabled="False" ForeColor="White">0.000</asp:TextBox>
                  <asp:Label ID="Label277" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <br />
                  <asp:Label ID="Label273" runat="server" Font-Bold="True" ForeColor="Blue" Text="Date:-" Width="100px"></asp:Label>
                  <asp:TextBox ID="TextBox49" runat="server" Font-Bold="True" ForeColor="#FF0066" TabIndex="2" ToolTip="DD-MM-YYYY" Width="150px" AutoCompleteType="Disabled"></asp:TextBox>
                  <cc1:CalendarExtender ID="TextBox49_CalendarExtender" runat="server" BehaviorID="TextBox49_CalendarExtender" CssClass="red" Enabled="True" Format="dd-MM-yyyy" TargetControlID="TextBox49" />
                  <asp:Label ID="Label278" runat="server" Font-Size="Smaller" ForeColor="Blue" Text="Date Format(DD-MM-YYYY)"></asp:Label>
                
                  <br />
                  <asp:Label ID="Label293" runat="server" Font-Bold="True" ForeColor="Blue" Text="Type Of Qty.:-" Width="100px"></asp:Label>
                  <asp:DropDownList ID="DropDownList6" runat="server" Width="150px">
                      <asp:ListItem>Select</asp:ListItem>
                      <asp:ListItem>Finishing Room</asp:ListItem>
                  </asp:DropDownList>
                
                  <br />
                  <asp:Label ID="Label289" runat="server" Font-Bold="True" ForeColor="Blue" Text="Qty:-" Width="100px"></asp:Label>
                  <asp:TextBox ID="TextBox56" runat="server" Width="150px"></asp:TextBox>
                  <asp:Label ID="Label290" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                  <br />
                  <br />
                  <asp:Label ID="Label279" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                  <br />
                        <asp:Button ID="Button33" runat="server" Text="CANCEL" Width="120px" CssClass="bottomstyle" Font-Size="Small" ViewStateMode="Disabled" />
                  <asp:Button ID="Button32" runat="server" Text="TRANSFER" Width="120px" CssClass="bottomstyle" Font-Size="Small" ViewStateMode="Disabled" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
                  <asp:Button ID="Button1" runat="server" Text="PRINT" Width="120px" CssClass="bottomstyle" Font-Size="Small" ViewStateMode="Disabled" />
                  </asp:Panel>
                  <br />
                
                 
                  <br />
                
                   <br />
                  </div>
                   <div style ="float :right; width :500px; height: 279px;" runat ="server" >

                       <asp:Panel ID="Panel2" runat="server" Height="268px" Visible="False">
                             <br />
                             <asp:Label ID="Label292" runat="server" Font-Bold="True" Font-Size="Medium" Font-Underline="True" ForeColor="Red">TO:-</asp:Label>
                       <br />
                       <asp:Label ID="Label281" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code:-" Width="100px"></asp:Label>
                       <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="250px">
                       </asp:DropDownList>
                       <br />
                       <asp:Label ID="Label284" runat="server" Font-Bold="True" ForeColor="Blue" Text="F.R. Qty:-" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox54" runat="server" Width="150px" BackColor="Blue" Enabled="False" ForeColor="White">0.000</asp:TextBox>
                       <asp:Label ID="Label285" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                       <br />
                       <asp:Label ID="Label286" runat="server" Font-Bold="True" ForeColor="Blue" Text="B.S.R. Qty:-" Width="100px"></asp:Label>
                       <asp:TextBox ID="TextBox55" runat="server" Width="150px" BackColor="Blue" Enabled="False" ForeColor="White">0.000</asp:TextBox>
                       <asp:Label ID="Label287" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                       </asp:Panel>
 </div>
                   
              </div>
          </asp:Panel>





            </div> 
        </center> 
</asp:Content>
