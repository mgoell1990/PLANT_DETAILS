<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LDM.aspx.vb" Inherits="PLANT_DETAILS.LDM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   <link href="../Content/red.css" rel="stylesheet" type="text/css" />   
   <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function CountCharacters(textBox,id)
    {
        var old_value;
        if (id == "MainContent_IPT_BUDGET") {
            old_value = '<%= VAR_IPT_BUDGET_UPTO %>';
            getSumGrossSales('BUDGET');
            getSumTotalIncome('BUDGET');

        } else if (id == "MainContent_IPT_ACTUAL") {
            old_value = '<%= VAR_IPT_ACTUAL_UPTO %>';
            getSumGrossSales('ACTUAL');
            getSumTotalIncome('ACTUAL');

        } else if (id == "MainContent_BONUS_PENALTY_BUDGET") {
            old_value = '<%= VAR_BONUS_PENALTY_BUDGET_UPTO %>';
            getSumGrossSales('BUDGET');
            getSumTotalIncome('BUDGET');

        } else if (id == "MainContent_BONUS_PENALTY_ACTUAL") {
            old_value = '<%= VAR_BONUS_PENALTY_ACTUAL_UPTO %>';
            getSumGrossSales('ACTUAL');
            getSumTotalIncome('ACTUAL');

        } else if (id == "MainContent_IC_BUDGET") {
            old_value = '<%= VAR_IC_BUDGET_UPTO %>';
            getSumGrossSales('BUDGET');
            getSumTotalIncome('BUDGET');

        } else if (id == "MainContent_IC_ACTUAL") {
            old_value = '<%= VAR_IC_ACTUAL_UPTO %>';
            getSumGrossSales('ACTUAL');
            getSumTotalIncome('ACTUAL');

        } else if (id == "MainContent_OR_BUDGET") {
            old_value = '<%= VAR_OR_BUDGET_UPTO %>';
            getSumTotalIncome('BUDGET');
        } else if (id == "MainContent_OR_ACTUAL") {
            old_value = '<%= VAR_OR_ACTUAL_UPTO %>';
            getSumTotalIncome('ACTUAL');
        } else if (id == "MainContent_PWB_BUDGET") {
            old_value = '<%= VAR_PWB_BUDGET_UPTO %>';
            getSumTotalIncome('BUDGET');
        } else if (id == "MainContent_PWB_ACTUAL") {
            old_value = '<%= VAR_PWB_ACTUAL_UPTO %>';
            getSumTotalIncome('ACTUAL');
        }
        var text_id = id;
        //alert(textBox.value);
        //alert(id);
        
        var new_id = id + '_UPTO';
        if (isNaN(document.getElementById(new_id).value)) {
            document.getElementById(new_id).value = 0;
        }
        
        document.getElementById(new_id).value = parseFloat(old_value) + parseFloat(textBox.value);
    }

    function getSumGrossSales(col_type)
    {
        //if (col_type == "BUDGET") {
        //    document.getElementById('MainContent_GS_BUDGET').value = parseFloat(document.getElementById('MainContent_IPT_BUDGET').value) + parseFloat(document.getElementById('MainContent_BONUS_PENALTY_BUDGET').value) + parseFloat(document.getElementById('MainContent_IC_BUDGET').value);
        //} else if (col_type == "ACTUAL") {
        //    document.getElementById('MainContent_GS_ACTUAL').value = parseFloat(document.getElementById('MainContent_IPT_ACTUAL').value) + parseFloat(document.getElementById('MainContent_BONUS_PENALTY_ACTUAL').value) + parseFloat(document.getElementById('MainContent_IC_ACTUAL').value);
        //}
        document.getElementById('MainContent_GS_BUDGET').value = parseFloat(document.getElementById('MainContent_IPT_BUDGET').value) + parseFloat(document.getElementById('MainContent_BONUS_PENALTY_BUDGET').value) + parseFloat(document.getElementById('MainContent_IC_BUDGET').value);
        document.getElementById('MainContent_GS_ACTUAL').value = parseFloat(document.getElementById('MainContent_IPT_ACTUAL').value) + parseFloat(document.getElementById('MainContent_BONUS_PENALTY_ACTUAL').value) + parseFloat(document.getElementById('MainContent_IC_ACTUAL').value);

        document.getElementById('MainContent_GS_BUDGET_UPTO').value = parseFloat(document.getElementById('MainContent_IPT_BUDGET_UPTO').value) + parseFloat(document.getElementById('MainContent_BONUS_PENALTY_BUDGET_UPTO').value) + parseFloat(document.getElementById('MainContent_IC_BUDGET_UPTO').value);
        document.getElementById('MainContent_GS_ACTUAL_UPTO').value = parseFloat(document.getElementById('MainContent_IPT_ACTUAL_UPTO').value) + parseFloat(document.getElementById('MainContent_BONUS_PENALTY_ACTUAL_UPTO').value) + parseFloat(document.getElementById('MainContent_IC_ACTUAL_UPTO').value);
        
    }

    function getSumTotalIncome(col_type)
    {
        //if (col_type == "BUDGET") {
        //    document.getElementById('MainContent_TOTAL_INCOME_BUDGET').value = parseFloat(document.getElementById('MainContent_GS_BUDGET').value) + parseFloat(document.getElementById('MainContent_OR_BUDGET').value) + parseFloat(document.getElementById('MainContent_PWB_BUDGET').value);
        //} else if (col_type == "ACTUAL") {
        //    document.getElementById('MainContent_TOTAL_INCOME_ACTUAL').value = parseFloat(document.getElementById('MainContent_GS_ACTUAL').value) + parseFloat(document.getElementById('MainContent_OR_ACTUAL').value) + parseFloat(document.getElementById('MainContent_PWB_ACTUAL').value);
        //}

        document.getElementById('MainContent_TOTAL_INCOME_BUDGET').value = parseFloat(document.getElementById('MainContent_GS_BUDGET').value) + parseFloat(document.getElementById('MainContent_OR_BUDGET').value) + parseFloat(document.getElementById('MainContent_PWB_BUDGET').value);
        document.getElementById('MainContent_TOTAL_INCOME_ACTUAL').value = parseFloat(document.getElementById('MainContent_GS_ACTUAL').value) + parseFloat(document.getElementById('MainContent_OR_ACTUAL').value) + parseFloat(document.getElementById('MainContent_PWB_ACTUAL').value);

        document.getElementById('MainContent_TOTAL_INCOME_BUDGET_UPTO').value = parseFloat(document.getElementById('MainContent_GS_BUDGET_UPTO').value) + parseFloat(document.getElementById('MainContent_OR_BUDGET_UPTO').value) + parseFloat(document.getElementById('MainContent_PWB_BUDGET_UPTO').value);
        document.getElementById('MainContent_TOTAL_INCOME_ACTUAL_UPTO').value = parseFloat(document.getElementById('MainContent_GS_ACTUAL_UPTO').value) + parseFloat(document.getElementById('MainContent_OR_ACTUAL_UPTO').value) + parseFloat(document.getElementById('MainContent_PWB_ACTUAL_UPTO').value);
    }
</script>

    <div class="row text-white mt-0" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label270" runat="server" Text="LDM" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <center>
        <asp:Panel ID="Panel3" runat="server" BorderColor="Lime" BorderStyle="Double" style="text-align: left" Width="99%" CssClass="brds" Font-Names="Times New Roman">
               <asp:Label ID="Label37" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center; line-height :30px;" Text="Last Day Of The Month" Width="100%" Height="30px" CssClass="brds"></asp:Label>
       
        <center>
            <br />
        <table border="0" class="auto-style3">
            <tr>
                <td class="auto-style13">
                    <h3><strong><span>Month:</span></strong>
                        <asp:DropDownList class="form-select" ID="DropDownList2" runat="server" Width="180px">
                            <asp:ListItem Value="-1">Select Month</asp:ListItem>
                            <asp:ListItem Value="10">January</asp:ListItem>
                            <asp:ListItem Value="11">February</asp:ListItem>
                            <asp:ListItem Value="12">March</asp:ListItem>
                            <asp:ListItem Value="1">April</asp:ListItem>
                            <asp:ListItem Value="2">May</asp:ListItem>
                            <asp:ListItem Value="3">June</asp:ListItem>
                            <asp:ListItem Value="4">July</asp:ListItem>
                            <asp:ListItem Value="5">August</asp:ListItem>
                            <asp:ListItem Value="6">September</asp:ListItem>
                            <asp:ListItem Value="7">October</asp:ListItem>
                            <asp:ListItem Value="8">November</asp:ListItem>
                            <asp:ListItem Value="9">December</asp:ListItem>
                        </asp:DropDownList>
                    </h3>
                </td>

                <td class="auto-style13">
                    <h3><strong><span>Year:</span></strong>
                        <asp:DropDownList class="form-select" ID="DropDownList1" runat="server" Width="180px">
                            <asp:ListItem>Select Year</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                        </asp:DropDownList>
                    </h3>
                </td>
                
                <td class="auto-style5">
                    <%--<asp:Button ID="Button1" runat="server" BackColor="#99FF66" BorderColor="Black" BorderStyle="Double" BorderWidth="1px" Font-Bold="True" Font-Size="X-Large" ForeColor="White" Height="50px" Text="Show" Width="180px" />--%>
                    <asp:Button ID="Button1" runat="server" Font-Bold="True" Text="Show" Width="85px" CssClass="bottomstyle"/>
                </td>
            </tr>
        </table>
        </center>
               <asp:Label ID="Label2" runat="server"></asp:Label>
        
            <br />
            
        
            <table border="1" class="auto-style10">
                <tr>
                    <td class="auto-style21" colspan="7">
                        <br />
                        <strong>NET SALE</strong><br /><br /></td>
                </tr>
                <tr>
                    <td colspan="1"></td>
                    <td class="auto-style21" colspan="3"><strong>For the Month</strong></td>
                    <td class="auto-style20" colspan="3"><strong>Upto the Month</strong></td>
                </tr>
                <tr>
                    <td class="auto-style19"></td>
                    <td class="auto-style24">Budget</td>
                    <td class="auto-style24">Actual</td>
                    <td class="auto-style24">CPLY</td>
                    <td class="auto-style24">Budget</td>
                    <td class="auto-style24">Actual</td>
                    <td class="auto-style24">CPLY</td>
                </tr>
                <tr>
                    <td class="auto-style31">Inter-Plant<br /> Transfers</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IPT_BUDGET" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IPT_ACTUAL" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IPT_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IPT_BUDGET_UPTO" runat="server" ReadOnly="True">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IPT_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IPT_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Bounus /
                        <br />
                        Penalty</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="BONUS_PENALTY_BUDGET" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="BONUS_PENALTY_ACTUAL" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="BONUS_PENALTY_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="BONUS_PENALTY_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="BONUS_PENALTY_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="BONUS_PENALTY_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Internal<br /> Consumption</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IC_BUDGET" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IC_ACTUAL" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IC_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IC_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IC_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IC_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31"><strong>Gross<br /> Sales</strong></td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GS_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GS_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GS_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GS_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GS_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GS_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Other<br /> Revenues</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OR_BUDGET" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OR_ACTUAL" runat="server" onblur="CountCharacters(this,this.id);"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OR_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OR_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OR_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OR_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Prov. Written<br /> Back</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="PWB_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="PWB_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="PWB_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="PWB_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="PWB_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="PWB_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31"><strong>Total<br /> Income</strong></td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="TOTAL_INCOME_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="TOTAL_INCOME_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="TOTAL_INCOME_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="TOTAL_INCOME_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="TOTAL_INCOME_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="TOTAL_INCOME_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table border="1" class="auto-style10">
                <tr>
                    <td class="auto-style1">
                        <br />
                        <strong><span class="auto-style2">EXPENDITURE<br /> </span></strong>
                        <br />
                    </td>
                </tr>
            </table>
            <table border="1" class="auto-style10">
                <tr>
                    <td class="auto-style31"><strong>Accretion(-)/<br /> Deplection to<br /> Stock</strong></td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="ACR_DEP_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="ACR_DEP_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="ACR_DEP_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="ACR_DEP_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="ACR_DEP_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="ACR_DEP_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Raw
                        <br />
                        Materials<br /> Others</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="RAW_MATERIAL_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="RAW_MATERIAL_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="RAW_MATERIAL_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="RAW_MATERIAL_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="RAW_MATERIAL_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="RAW_MATERIAL_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Salary<br /> &amp;<br /> Wages</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="SALARY_WAGES_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="SALARY_WAGES_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="SALARY_WAGES_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="SALARY_WAGES_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="SALARY_WAGES_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="SALARY_WAGES_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Stores<br /> &amp;<br /> Spares</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="STORES_SPARES_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="STORES_SPARES_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="STORES_SPARES_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="STORES_SPARES_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="STORES_SPARES_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="STORES_SPARES_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Power<br /> &amp;<br /> Fuel</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="POWER_FUEL_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="POWER_FUEL_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="POWER_FUEL_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="POWER_FUEL_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="POWER_FUEL_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="POWER_FUEL_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Repair<br /> &amp;<br /> Maintenance</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="REPAIR_MAINTENANCE_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="REPAIR_MAINTENANCE_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="REPAIR_MAINTENANCE_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="REPAIR_MAINTENANCE_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="REPAIR_MAINTENANCE_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="REPAIR_MAINTENANCE_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">
                        <br />
                        Freight Outwards<br />
                        <br />
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="FREIGHT_OUT_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="FREIGHT_OUT_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="FREIGHT_OUT_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="FREIGHT_OUT_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="FREIGHT_OUT_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="FREIGHT_OUT_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Selling/Admn.<br /> /Overhead<br /> Expences</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OVERHEAD_EXP_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OVERHEAD_EXP_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OVERHEAD_EXP_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OVERHEAD_EXP_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OVERHEAD_EXP_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="OVERHEAD_EXP_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Share of<br /> CMO/HO<br /> Expences</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="HO_EXP_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="HO_EXP_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="HO_EXP_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="HO_EXP_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="HO_EXP_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="HO_EXP_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">GST<br /> on<br /> IPT</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IGST_IPT_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IGST_IPT_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IGST_IPT_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IGST_IPT_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IGST_IPT_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="IGST_IPT_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31"><strong>
                        <br />
                        Gross Expenditure<br />
                        <br />
                        </strong></td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_EXP_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_EXP_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_EXP_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_EXP_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_EXP_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_EXP_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Less Transfer<br /> to<br /> Inter A/C adj.</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="LESS_TRANS_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="LESS_TRANS_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="LESS_TRANS_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="LESS_TRANS_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="LESS_TRANS_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="LESS_TRANS_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31"><strong>
                        <br />
                        Net Expenditure<br />
                        <br />
                        </strong></td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_EXP_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_EXP_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_EXP_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_EXP_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_EXP_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_EXP_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31"><strong>
                        <br />
                        Gross Margin<br />
                        <br />
                        </strong></td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_MARGIN_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_MARGIN_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_MARGIN_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_MARGIN_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_MARGIN_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="GROSS_MARGIN_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">
                        <br />
                        Depreciation<br />
                        <br />
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="DEPRECIATION_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="DEPRECIATION_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="DEPRECIATION_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="DEPRECIATION_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="DEPRECIATION_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="DEPRECIATION_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31">Interest<br /> on
                        <br />
                        Cash Credit</td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="INTEREST_CASH_CREDIT_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="INTEREST_CASH_CREDIT_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="INTEREST_CASH_CREDIT_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="INTEREST_CASH_CREDIT_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="INTEREST_CASH_CREDIT_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="INTEREST_CASH_CREDIT_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style31"><strong>
                        <br />
                        Net Profit<br />
                        <br />
                        </strong></td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_PROFIT_BUDGET" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_PROFIT_ACTUAL" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_PROFIT_CPLY" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_PROFIT_BUDGET_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_PROFIT_ACTUAL_UPTO" runat="server">0</asp:TextBox>
                    </td>
                    <td class="auto-style24">
                        <asp:TextBox class="form-control" ID="NET_PROFIT_CPLY_UPTO" runat="server">0</asp:TextBox>
                    </td>
                </tr>
            </table>
            <p class="auto-style1">
                &nbsp;</p>
            <p class="auto-style1">
                <asp:Button ID="Button3" runat="server" Font-Bold="True" ForeColor="Blue" Text="SAVE" Width="80px" CssClass="bottomstyle" OnClientClick="this.disabled='true'; this.value='Please Wait...'" UseSubmitBehavior="false" />
                <asp:Button ID="Button4" runat="server" Font-Bold="True" ForeColor="Blue" Text="PRINT" Width="80px" CssClass="bottomstyle" OnClientClick="this.disabled='true'; this.value='Please Wait...'" UseSubmitBehavior="false" />
            </p>
            
               
        
            </asp:Panel>
           </center> 
</asp:Content>






