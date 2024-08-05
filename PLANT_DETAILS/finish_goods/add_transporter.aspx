<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="add_transporter.aspx.vb" Inherits="PLANT_DETAILS.add_transporter" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True"  ForeColor="#800040" style="text-align: center" Width="100%" Font-Size="XX-Large"></asp:Label>
        </div>
    </section>
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
        <asp:Panel ID="Panel1" runat="server" style="text-align: left" Width="700px">
            <asp:Label ID="Label2" runat="server" Text="Truck No"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                      <script type="text/javascript">
                          $(function () {
                              $("[id$=TextBox2]").autocomplete({
                                  source: function (request, response) {
                                      $.ajax({
                                          url: '<%=ResolveUrl("~/Service.asmx/PEND_T_NO")%>',
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
            <asp:Button ID="Button1" runat="server" Text="VIEW" />
            <asp:Button ID="Button2" runat="server" Text="SAVE" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'"/>
          
            <br />
            <asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Bold="True" Width="300px"></asp:Label>
            <br />

           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
             <asp:TemplateField HeaderText="">
             <ItemTemplate>
             <asp:CheckBox ID="CheckBox1" runat="server"></asp:CheckBox>
             </ItemTemplate> 
             <ItemStyle ForeColor="Blue" Width="30px" />
             </asp:TemplateField>
                <asp:BoundField DataField="SO_NO" HeaderText="So. No." />
                <asp:BoundField DataField="INV_NO" HeaderText="Inv No" />
                <asp:BoundField DataField="INV_DATE" HeaderText="Inv Date " />
                <asp:BoundField DataField="TRANS_WO" HeaderText="Transporter W.O" />
                <asp:BoundField DataField="TRANS_SLNO" HeaderText="Transporter W. SlNo" />
                <asp:BoundField DataField="TRANS_NAME" HeaderText="Transporter Name" />
                <asp:BoundField DataField="TRUCK_NO" HeaderText="Truck No" />
                <asp:BoundField DataField="TOTAL_WEIGHT" HeaderText="Material Weight " />
               
            </Columns>
          </asp:GridView>
         </asp:Panel>    
        
        </div> 
        </center> 
</asp:Content>
