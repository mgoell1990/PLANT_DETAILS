<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="rm_inspection1.aspx.vb" Inherits="PLANT_DETAILS.rm_inspection1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <center>
       <div runat ="server" style ="min-height :600px;">
           <br />
           <asp:Panel ID="Panel3" runat="server" BorderColor="Lime" BorderStyle="Double" style="text-align: left" Width="1000px" CssClass="brds" Font-Names="Times New Roman">
               <asp:Label ID="Label37" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :30px;" Text="INSPECTION" Width="100%" Height="30px" CssClass="brds"></asp:Label>
         <br />
         <br />
         <br />
               
               <div runat ="server" style="width:80%; align-content:unset">
               
                   <asp:Label ID="Label3" runat="server" Width="150px" Text="CRR" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Width="180px"></asp:DropDownList>
                   
              <br />
                   <asp:Label ID="Label4" runat="server" Width="150px" Text="Challan No. :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox287" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
                   &emsp;&emsp;&emsp;&emsp;&emsp;
                   <asp:Label ID="Label8" runat="server" Width="150px" Text="PO No. :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox282" runat="server" Width="180px" BackColor="Red" ReadOnly="True" ForeColor="White"></asp:TextBox>
              <br />
                   <asp:Label ID="Label5" runat="server" Width="150px" Text="Material Sl No. :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox4" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
                   &emsp;&emsp;&emsp;&emsp;&emsp;
                   <asp:Label ID="Label9" runat="server" Width="150px" Text="Supplier :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox283" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
              <br />
                   <asp:Label ID="Label6" runat="server" Width="150px" Text="Material Name :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox278" runat="server" Width="180px" BackColor="Red" ReadOnly="True" ForeColor="White"></asp:TextBox>
                   &emsp;&emsp;&emsp;&emsp;&emsp;
                   <asp:Label ID="Label10" runat="server" Width="150px" Text="A/U :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox284" runat="server" Width="180px" ReadOnly="True"></asp:TextBox>
              <br />
                   <asp:Label ID="Label7" runat="server" Width="150px" Text="Recieved Quantity :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox280" runat="server" Width="180px" BackColor="Red" ReadOnly="True" ForeColor="White"></asp:TextBox>
                   &emsp;&emsp;&emsp;&emsp;&emsp;
                   <asp:Label ID="Label11" runat="server" Width="150px" Text="Rejected Quantity :" Font-Bold="True" ForeColor="Blue"></asp:Label>
                   <asp:TextBox ID="TextBox285" runat="server" Width="180px"></asp:TextBox>
              <br />
              </div>
              

                
               <br />    
               <br />
            <span style="font-size:15px; color:brown; font-weight:bold">Chemical Composition of the Material is as Follows :</span>
                <br />
            <br />
               <div runat ="server" style="width:80%; align-content:unset">
                   <asp:Label ID="Label12" runat="server" Width="150px" Text="Label">MEMO No. :</asp:Label>
                   <asp:TextBox ID="TextBox5" runat="server" Width="180px"></asp:TextBox>
                   &emsp;&emsp;&emsp;&emsp;&emsp;
                   <asp:Label ID="Label13" runat="server" Width="150px" Text="Label">LAB SI No. :</asp:Label>
                   <asp:TextBox ID="TextBox6" runat="server" Width="180px"></asp:TextBox>
               </div>
            <br />
            <table border="1" style="width:100%;">
                
            <tr style="font-size:13px; font-weight:bold; text-align:center">
                
                <td>Characteristics</td>
                <td>Unit</td>
                <td>Gauranteed specification as per PO</td>
                <td>Acceptable limit as per PO rebate clause</td>
                <td>Test Result obtained at SRU lab</td>
            </tr>
                <tr style="font-size:13px; font-weight:bold; text-align:center">
                <td>             
               <asp:DropDownList ID="properties_DropDownList" runat="server" AutoPostBack="True" Width="142px" Font-Names="Times New Roman">
                   <asp:ListItem>Select</asp:ListItem>
                   <asp:ListItem>%Cr2o3</asp:ListItem>
                   <asp:ListItem>Viscosity at 25°c</asp:ListItem>
                   <asp:ListItem>Non volatile Matter</asp:ListItem>
                   <asp:ListItem>MgO</asp:ListItem>
                   <asp:ListItem>CaO</asp:ListItem>
                   <asp:ListItem>Ca(NO3)2+H2O</asp:ListItem>
                   <asp:ListItem>SiO2</asp:ListItem>
                   <asp:ListItem>(K2O+Na2O)</asp:ListItem>
                   <asp:ListItem>%C</asp:ListItem>
                   <asp:ListItem>Fe2O3</asp:ListItem>
                   <asp:ListItem>LOI</asp:ListItem>
                   <asp:ListItem>%AI2O3</asp:ListItem>
                   <asp:ListItem>PCE °C</asp:ListItem>
                   <asp:ListItem>PLC</asp:ListItem>
                   <asp:ListItem>%ASH</asp:ListItem>
                   <asp:ListItem>%MQI</asp:ListItem>
                   <asp:ListItem>SiC%</asp:ListItem>
                   <asp:ListItem>%AI</asp:ListItem>
                   <asp:ListItem>%VM</asp:ListItem>
                   <asp:ListItem>%FC</asp:ListItem>
                   <asp:ListItem>%P2O5</asp:ListItem>
                   <asp:ListItem>%Si</asp:ListItem>
                   <asp:ListItem>Gel Index</asp:ListItem>
                   <asp:ListItem>Moisture</asp:ListItem>
                   <asp:ListItem>Density</asp:ListItem>
                   <asp:ListItem>%Reducing Matter</asp:ListItem>
                   <asp:ListItem>%Sugar</asp:ListItem>
                   <asp:ListItem>True Specific Gravity</asp:ListItem>
                   <asp:ListItem>Water Miscibility</asp:ListItem>
                   <asp:ListItem>(+)2mm</asp:ListItem>
                   <asp:ListItem>(-)mm</asp:ListItem>
                   <asp:ListItem>(+)mm</asp:ListItem>
                   <asp:ListItem>(-)mm</asp:ListItem>
                   <asp:ListItem>+Mesh</asp:ListItem>
                   <asp:ListItem>-Mesh</asp:ListItem>
             </asp:DropDownList>
             
                </td>
                <td>%</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="auto-style63" Width="140px">0</asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" style="margin-left: 26px" Width="140px">0</asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" style="margin-left: 26px" Width="140px">0</asp:TextBox>
                </td>
                <td style="border:hidden">
                    <div runat ="server" style ="float :right;" >
                     <asp:Button ID="Button50" runat="server" Text="ADD" CssClass="bottomstyle" Width="100px" />
                </div>
                </td>
                
            </tr>
                </table>&nbsp;
               
        
        <br />
               <asp:Panel ID="Panel11" runat="server" ScrollBars="Auto" BackColor="White" BorderColor="#3366CC" BorderStyle="Groove" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid" Font-Names="Times New Roman">
            <asp:GridView ID="GridView1" style="font-size:13px; font-weight:bold; text-align:center" runat="server" Font-Size="Small" ShowHeaderWhenEmpty="True" Width="100%" CssClass="mGrid" Font-Names="Times New Roman">
                      <RowStyle Height="25px" />
                
             </asp:GridView>
                </asp:Panel>
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
         <br />
               <asp:Panel ID="Panel1" runat="server" style="text-align: left" Width="1000px" CssClass="brds" Font-Names="Times New Roman">
               <asp:FileUpload ID="FileUpload1" runat="server" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessage" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="UPLOAD" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" CssClass="auto-style62" Text="SAVE" Width="111px" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" Text="CANCEL" />
                   <br />
                   <br />
                   <asp:Button ID="Button51" runat="server" Text="Show Image" />
                   <br />
                   <asp:GridView ID="gvImages" style="text-align:center; width:100%" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound">
    <Columns>
        
        <asp:BoundField DataField="CRR_NO" HeaderText="CRR NO" />
        <asp:TemplateField HeaderText="TEST REPORT">
            <ItemTemplate>
                <%--<asp:Image ID="Image1" class="imgthumb" runat="server" />--%>
                <asp:ImageButton ID="Image1" runat="server" 
                Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
                   <br />
                   <br />
                   
               </asp:Panel>
               <div id="ladiv" style="align-self:center"></div>
           </asp:Panel>

           </div>
           </center>
     
    

    <%--==================================================================--%>
    <div id="divBackground" class="modal">
</div>
<div id="divImage">
<table style="height: 100%; width: 100%">
    <tr>
        <td valign="middle" align="center">
            <img id="imgLoader" alt="" src="images/loader.gif" />
            <img id="imgFull" alt="" src="" style="display: none; height: 600px; width: 590px" />
        </td>
    </tr>
    <tr>
        <td align="center" valign="bottom">
            <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
        </td>
    </tr>
</table>
</div>
    <style type="text/css">
body
{
    margin: 0;
    padding: 0;
    height: 100%;
}
.modal
{
    display: none;
    position: absolute;
    top: 0px;
    left: 0px;
    background-color: black;
    z-index: 100;
    opacity: 0.8;
    filter: alpha(opacity=60);
    -moz-opacity: 0.8;
    min-height: 100%;
}
#divImage
{
    display: none;
    z-index: 1000;
    position: fixed;
    top: 0;
    left: 0;
    background-color: White;
    height: 650px;
    width: 600px;
    padding: 3px;
    border: solid 1px black;
}
</style>

    <script type="text/javascript">
    function LoadDiv(url) {
    var img = new Image();
    var bcgDiv = document.getElementById("divBackground");
    var imgDiv = document.getElementById("divImage");
    var imgFull = document.getElementById("imgFull");
    var imgLoader = document.getElementById("imgLoader");
    imgLoader.style.display = "block";
    img.onload = function () {
        imgFull.src = img.src;
        imgFull.style.display = "block";
        imgLoader.style.display = "none";
   };
    img.src = url;
    var width = document.body.clientWidth;
    if (document.body.clientHeight > document.body.scrollHeight) {
        bcgDiv.style.height = document.body.clientHeight + "px";
    }
    else {
        bcgDiv.style.height = document.body.scrollHeight + "px";
    }
    imgDiv.style.left = (width - 650) / 2 + "px";
    imgDiv.style.top = "20px";
    bcgDiv.style.width = "100%";
 
    bcgDiv.style.display = "block";
    imgDiv.style.display = "block";
    return false;
}
function HideDiv() {
    var bcgDiv = document.getElementById("divBackground");
    var imgDiv = document.getElementById("divImage");
    var imgFull = document.getElementById("imgFull");
    if (bcgDiv != null) {
        bcgDiv.style.display = "none";
        imgDiv.style.display = "none";
        imgFull.style.display = "none";
    }
}
</script>

    <%--==================================================================--%>
</asp:Content>

