<%--<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Payslip.aspx.vb" Inherits="PLANT_DETAILS.WebForm4" %>--%>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Payslip.aspx.vb" Inherits="PLANT_DETAILS.WebForm4" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/red.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />

    <center>
        <div runat ="server" style ="min-height :600px;">
           
             <asp:Panel ID="Panel9" runat="server" Font-Names="Times New Roman" Width="100%" style="text-align: left">
             <asp:Label ID="Label48" runat="server" BackColor="#4686F0" Font-Bold="True" Font-Size="XX-Large" ForeColor="White" style="text-align: center;line-height :30px;" Text="REPORT" Height ="30px" Width="100%"></asp:Label>
          <br />
                    <br />
                    <asp:Label ID="Label4" runat="server" ForeColor="Blue" Text="Search For"></asp:Label>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="125px" AutoPostBack="True">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>Payments</asp:ListItem>
                        <asp:ListItem>Recoveries</asp:ListItem>
                        <asp:ListItem>Attendance</asp:ListItem>
                        <asp:ListItem>Leave Balance</asp:ListItem>
                        <asp:ListItem>Cumulatives</asp:ListItem>
                        <asp:ListItem>Savings</asp:ListItem>
                        <asp:ListItem>Generate Document</asp:ListItem>
                        <asp:ListItem>Split Document</asp:ListItem>
                        <asp:ListItem>Email Document</asp:ListItem>
                        
                    </asp:DropDownList>

             <asp:Panel ID="Panel10" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" Visible="False">
                 <div runat ="server" style="text-align: center">
                 <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="PAYMENTS" Width="20%"></asp:Label>
                 </div>     
                <br />
                <div runat ="server" style="text-align: center">
               <br />
                 <asp:Label ID="Label53" runat="server" Text="Month"></asp:Label>

                 <asp:DropDownList ID="DropDownList13" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="Label54" runat="server" Text="Year"></asp:Label>

                <asp:DropDownList ID="DropDownList14" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label1" runat="server" Text="SRU Pay Bill"></asp:Label>

                <asp:FileUpload ID="FileUpload1" runat="server" />
                <br />
                <br />
                <asp:Button ID="Button2" runat="server" Text="Upload" />
                <br />
                    </div>
             </asp:Panel>

             <asp:Panel ID="Panel2" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left" Visible="False">
                <div runat ="server" style="text-align: center">
                <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="Recoveries" Width="20%"></asp:Label>
                </div>     
                <br />
                 <div runat ="server" style="text-align: center">
               <br />
                <asp:Label ID="Label51" runat="server" Text="Month"></asp:Label>

                <asp:DropDownList ID="DropDownList11" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="Label52" runat="server" Text="Year"></asp:Label>

                <asp:DropDownList ID="DropDownList12" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label2" runat="server" Text="SRU Pay Recoveries"></asp:Label>

                <asp:FileUpload ID="FileUpload2" runat="server" />
                <br />
                <br />
                <asp:Button ID="Button3" runat="server" Text="Upload" />
                <br />
                </div>
             </asp:Panel>

             <asp:Panel ID="Panel6" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left" Visible="False">
                <div runat ="server" style="text-align: center">
                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="ATTENDANCE" Width="23%"></asp:Label>
                </div>     
                     
                <br />
               <div runat ="server" style="text-align: center">
               <br />
                   <asp:Label ID="Label26" runat="server" Text="Attendance Month"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;    
               <br />
                <asp:Label ID="Label5" runat="server" Text="Payslip Month"></asp:Label>

                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="Label6" runat="server" Text="Year"></asp:Label>

                <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label7" runat="server" Text="SRU Attendance"></asp:Label>

                <asp:FileUpload ID="FileUpload3" runat="server" />
                <br />
                <br />
                <asp:Button ID="Button41" runat="server" Text="Upload" />
                <br /> 
                </div>

             </asp:Panel>


           <asp:Panel ID="Panel1" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: center" Visible="False">
               <div runat ="server" style="text-align: center">
                     <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="LEAVE BALANCE" Width="25%"></asp:Label>
                </div>     
                <br />
               <asp:Label ID="Label27" runat="server" Text="Leave Balance Month"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;    
               
                <br />
                <asp:Label ID="Label9" runat="server" Text="Payslip Month"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
    
                <asp:Label ID="Label11" runat="server" Text="Year"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                </asp:DropDownList>
                <br />
              
                <asp:Label ID="Label12" runat="server" Text="Leave Balance"></asp:Label>
                <asp:FileUpload ID="FileUpload4" runat="server" />
                <br />
                <br />
    
                <asp:Button ID="Button8" runat="server" Text="Upload" />
    
                <br />
           </asp:Panel>


            </asp:Panel>


            <br />

            <asp:Panel ID="Panel3" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: center" Font-Names="Times New Roman" Visible="False">
                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="Large" Text="CUMULATIVES/SAVINGS" Width="100%"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label13" runat="server" Text="Month"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
    
                <asp:Label ID="Label14" runat="server" Text="Year"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                </asp:DropDownList>
                <br />
              
                <asp:Label ID="Label15" runat="server" Text="Cummulatives"></asp:Label>
                <asp:FileUpload ID="FileUpload5" runat="server" />
                <br />
                <br />
    
                <asp:Button ID="Button9" runat="server" Text="Upload" />
    
                <br />
            </asp:Panel>

            <br />

            <asp:Panel ID="Panel4" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: center" Visible="False" Font-Names="Times New Roman">
            <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Size="Large" Text="ADVANCES" Width="100%" style="text-align: center"></asp:Label>
                <br />
                <br />
                <asp:Label ID="Label17" runat="server" Text="Month"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
    
                <asp:Label ID="Label18" runat="server" Text="Year"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="DropDownList19" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>2020</asp:ListItem>
                    <asp:ListItem>2019</asp:ListItem>
                    <asp:ListItem>2018</asp:ListItem>
                </asp:DropDownList>
                <br />
              
                <asp:Label ID="Label25" runat="server" Text="Advances"></asp:Label>
                <asp:FileUpload ID="FileUpload6" runat="server" />
                <br />
                <br />
    
                <asp:Button ID="Button10" runat="server" Text="Upload" />
    
                <br />
            </asp:Panel>








           <asp:Panel ID="Panel11" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" Visible="False">
               <div runat ="server" style="text-align: center">
                     <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="GENERATE DOCUMENT" Width="30%"></asp:Label>
                </div>     
                <br />
               <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label29" runat="server" Text="Document"></asp:Label>

                    <asp:DropDownList ID="DropDownList21" runat="server" AutoPostBack="True" Width="125px">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>PAYSLIP</asp:ListItem>
                        <asp:ListItem>SESBF</asp:ListItem>  
                    </asp:DropDownList>
                   
               </div>
               <br />
               <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <div runat ="server" style="text-align: center">
                            <asp:Label ID="Label49" runat="server" Text="Month"></asp:Label>
                            <asp:DropDownList ID="DropDownList9" runat="server" AutoPostBack="True" Width="125px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>JANUARY</asp:ListItem>
                            <asp:ListItem>FEBRUARY</asp:ListItem>
                            <asp:ListItem>MARCH</asp:ListItem>
                            <asp:ListItem>APRIL</asp:ListItem>
                            <asp:ListItem>MAY</asp:ListItem>
                            <asp:ListItem>JUNE</asp:ListItem>
                            <asp:ListItem>JULY</asp:ListItem>
                            <asp:ListItem>AUGUST</asp:ListItem>
                            <asp:ListItem>SEPTEMBER</asp:ListItem>
                            <asp:ListItem>OCTOBER</asp:ListItem>
                            <asp:ListItem>NOVEMBER</asp:ListItem>
                            <asp:ListItem>DECEMBER</asp:ListItem>
                            </asp:DropDownList>

                            <asp:Label ID="Label50" runat="server" Text="Year"></asp:Label>

                            <asp:DropDownList ID="DropDownList10" runat="server" AutoPostBack="True" Width="125px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>2020</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="Generate payslip" />
                            <br />
                            <br /> 
                        </div>
                               
                            
                       </asp:View>
                       <asp:View ID="View2" runat="server">
                           <div runat ="server" style="text-align: center">
                            <asp:Label ID="Label28" runat="server" Text="Fiscal Year"></asp:Label>
                            <asp:DropDownList ID="DropDownList20" runat="server" AutoPostBack="True" Width="125px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>1920</asp:ListItem>
                            <asp:ListItem>1819</asp:ListItem>
                            
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Button ID="Button7" runat="server" Text="Generate SESBF Slip" />
                            <br />
                            <br /> 
                        </div>
                          
                       </asp:View>
                </asp:MultiView>
               
                  
           </asp:Panel>



            <asp:Panel ID="Panel5" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: center" Visible="False">
                <div runat ="server" style="text-align: center">
                     <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="SPLIT DOCUMENT" Width="20%"></asp:Label>
                </div> 
                <br />
                <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label30" runat="server" Text="Document"></asp:Label>

                    <asp:DropDownList ID="DropDownList22" runat="server" AutoPostBack="True" Width="125px">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>PAYSLIP</asp:ListItem>
                        <asp:ListItem>SESBF</asp:ListItem>  
                        <asp:ListItem>PF SLIP</asp:ListItem>
                    </asp:DropDownList>
                   
               </div>    
                <br />
                <asp:MultiView ID="MultiView2" runat="server">
                    <asp:View ID="View3" runat="server">
                        <div runat ="server" style="text-align: center">
                            <asp:Label ID="Label55" runat="server" Text="Month"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList15" runat="server" AutoPostBack="True" Width="125px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>JANUARY</asp:ListItem>
                                <asp:ListItem>FEBRUARY</asp:ListItem>
                                <asp:ListItem>MARCH</asp:ListItem>
                                <asp:ListItem>APRIL</asp:ListItem>
                                <asp:ListItem>MAY</asp:ListItem>
                                <asp:ListItem>JUNE</asp:ListItem>
                                <asp:ListItem>JULY</asp:ListItem>
                                <asp:ListItem>AUGUST</asp:ListItem>
                                <asp:ListItem>SEPTEMBER</asp:ListItem>
                                <asp:ListItem>OCTOBER</asp:ListItem>
                                <asp:ListItem>NOVEMBER</asp:ListItem>
                                <asp:ListItem>DECEMBER</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
    
                            <asp:Label ID="Label58" runat="server" Text="Year"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList18" runat="server" AutoPostBack="True" Width="125px">
                            <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>2020</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                            </asp:DropDownList>
                            
    
                            <br />
                        </div>
                               
                            
                       </asp:View>
                       <asp:View ID="View4" runat="server">
                           <div runat ="server" style="text-align: center">
                            <asp:Label ID="Label33" runat="server" Text="Fiscal Year"></asp:Label>
                            <asp:DropDownList ID="DropDownList24" runat="server" AutoPostBack="True" Width="125px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>2223</asp:ListItem>
                                <asp:ListItem>2122</asp:ListItem>
                                <asp:ListItem>2021</asp:ListItem>
                                <asp:ListItem>1920</asp:ListItem>
                                <asp:ListItem>1819</asp:ListItem>
                            </asp:DropDownList>
                            
                            <br />
                            <br /> 
                        </div>
                          
                       </asp:View>
                </asp:MultiView>
                <br />
                <br />
                <asp:Button ID="Button4" runat="server" Text="Split PDF" />
                
            </asp:Panel>


            

                 <asp:Panel ID="Panel8" runat="server" BackColor="#AAEEFF" BorderColor="#AAEEFF" BorderStyle="Groove" style="text-align: left" Visible="False" Font-Bold="True">
                    <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="Large" Text="EMAIL DOCUMENT" Width="20%"></asp:Label>
                    </div>
                    <br />
                    <br />
               <div runat ="server" style="text-align: center">
                    <asp:Label ID="Label31" runat="server" Text="Document"></asp:Label>

                    <asp:DropDownList ID="DropDownList23" runat="server" AutoPostBack="True" Width="125px">
                        <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>PAYSLIP</asp:ListItem>
                        <asp:ListItem>SESBF</asp:ListItem>
                        <asp:ListItem>FORM 16</asp:ListItem>
                        <asp:ListItem>PF SLIP</asp:ListItem>  
                    </asp:DropDownList>
                   
               </div>
               <br />
               <asp:MultiView ID="MultiView3" runat="server">
                    <asp:View ID="View5" runat="server">
                        <div runat ="server" style="text-align: center">
                            <asp:Label ID="Label8" runat="server" Text="Personal No"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
    
                            <asp:Label ID="Label56" runat="server" Text="Month"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="DropDownList16" runat="server" AutoPostBack="True" Width="125px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>JANUARY</asp:ListItem>
                                <asp:ListItem>FEBRUARY</asp:ListItem>
                                <asp:ListItem>MARCH</asp:ListItem>
                                <asp:ListItem>APRIL</asp:ListItem>
                                <asp:ListItem>MAY</asp:ListItem>
                                <asp:ListItem>JUNE</asp:ListItem>
                        <asp:ListItem>JULY</asp:ListItem>
                        <asp:ListItem>AUGUST</asp:ListItem>
                        <asp:ListItem>SEPTEMBER</asp:ListItem>
                        <asp:ListItem>OCTOBER</asp:ListItem>
                        <asp:ListItem>NOVEMBER</asp:ListItem>
                        <asp:ListItem>DECEMBER</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
    
                    <asp:Label ID="Label57" runat="server" Text="Year"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="True" Width="125px">
                    <asp:ListItem>Select</asp:ListItem>
                        <asp:ListItem>2020</asp:ListItem>
                        <asp:ListItem>2019</asp:ListItem>
                        <asp:ListItem>2018</asp:ListItem>
                        
                    </asp:DropDownList>
                    <br />
                    <br />
                        </div>
                               
                            
                       </asp:View>
                       <asp:View ID="View6" runat="server">
                           <div runat ="server" style="text-align: center">
                            <asp:Label ID="Label34" runat="server" Text="Personal No"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label36" runat="server" Text="Fiscal Year"></asp:Label>
                            <asp:DropDownList ID="DropDownList27" runat="server" AutoPostBack="True" Width="125px">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>2324</asp:ListItem>
                                <asp:ListItem>2223</asp:ListItem>
                                <asp:ListItem>2122</asp:ListItem>
                                <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>1920</asp:ListItem>
                            <asp:ListItem>1819</asp:ListItem>
                            
                            </asp:DropDownList>
                            <br />
                            <br />
                            
                        </div>
                          
                       </asp:View>
                </asp:MultiView>
                    
                <div runat ="server" style="text-align: center">
                    <asp:Button ID="Button6" runat="server" Text="Send Individual PDF" />
    
                    &nbsp;&nbsp;&nbsp;
    
                    <asp:Button ID="Button5" runat="server" Text="Send Bulk PDF" />
                </div>

                 </asp:Panel>

            </div>
        </center>
</asp:Content>

