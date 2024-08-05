<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="mat_issue_return.aspx.vb" Inherits="PLANT_DETAILS.mat_issue_return" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
     <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif;">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>--%>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <center>
        <div runat ="server" style ="min-height :600px; font-family: 'Times New Roman', Times, serif;">
            <asp:Panel ID="Panel2" runat="server" BorderColor="Red" BorderStyle="Groove" BorderWidth="5px" Font-Names="Times New Roman" Font-Size="X-Small" Height="354px" style="text-align: left" Width="705px">
                <div id="fastdivision1" aria-orientation="vertical" aria-selected="undefined" style="width: 683px; height: 355px;">
                    
                    <div aria-autocomplete="none" aria-checked="undefined" aria-expanded="undefined" aria-grabbed="undefined" aria-orientation="vertical" style="float: right; font-family: 'Times New Roman', Times, serif; font-size: small; height: 346px; text-align: left;">
                <br />
                        
                        
                        
                 <br />
                 <br />
                    </div>
                </div>
            </asp:Panel>
            </div> 
         </center>
</asp:Content>--%>

<%--===================================================--%>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id$=TextBox171]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/STORE_ISSUE_RET")%>',
                              data: "{ 'prefix': '" + request.term + "'}",
                              dataType: "json",
                              type: "POST",
                              contentType: "application/json; charset=utf-8",
                              success: function (data) {
                                  response($.map(data.d, function (item) {
                                      return {
                                          label: item.split('^')[0],
                                          val: item.split('^')[1],
                                          au: item.split('^')[2],
                                          mat_stock: item.split('^')[3],
                                          mat_avg: item.split('^')[4],
                                          mat_loca: item.split('^')[5],
                                          dept: item.split('^')[6],
                                          cost_cent: item.split('^')[7],
                                          purpose: item.split('^')[8],
                                          r_qty: item.split('^')[9],
                                          RQD_BY: item.split('^')[10],
                                          RQD_DATE: item.split('^')[11],
                                          AUTH_BY: item.split('^')[12],
                                          AUTH_DATE: item.split('^')[13],
                                          ISSUE_BY: item.split('^')[14],
                                          ISSUE_DATE: item.split('^')[15],
                                          line_no: item.split('^')[16]
                                          


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
                             $("[id$=DropDownList3]").val(i.item.val);
                             $("[id$=TextBox168]").val(i.item.au);
                             $("[id$=TextBox167]").val(i.item.mat_stock);
                             $("[id$=TextBox166]").val(i.item.mat_avg);
                             $("[id$=TextBox170]").val(i.item.mat_loca);
                             $("[id$=TextBox169]").val(i.item.line_no);
                             $("[id$=TextBox163]").val(i.item.r_qty);
                             $("[id$=TextBox172]").val(i.item.dept);
                             $("[id$=TextBox173]").val(i.item.cost_cent);
                             $("[id$=TextBox2]").val(i.item.purpose);
                             $("[id$=TextBox8]").val(i.item.RQD_BY);
                             $("[id$=TextBox9]").val(i.item.RQD_DATE);
                             $("[id$=TextBox10]").val(i.item.AUTH_BY);
                             $("[id$=TextBox11]").val(i.item.AUTH_DATE);
                             $("[id$=TextBox1]").val(i.item.ISSUE_BY);
                             $("[id$=TextBox4]").val(i.item.ISSUE_DATE);
                         },
                         minLength: 1
                     });
                      });
    </script>

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Store Issue Return" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10" style="border: 5px groove #FF0066; float: left; text-align: left;">
                <div class="row">
                    <div class="col-8" style="border-right: 5px groove #FF0066;">
                        <div class="row align-items-center mt-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Vaucher No"></asp:Label>

                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox171" runat="server" BorderStyle="Solid"></asp:TextBox>

                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Code"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="DropDownList3" runat="server" Font-Names="Times New Roman"   BackColor="White" BorderStyle="Solid" ForeColor="Red"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label426" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issued Qty." ></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox163" runat="server" Font-Names="Times New Roman" BorderStyle="Solid"  ForeColor="Red"></asp:TextBox>

                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                &nbsp;&nbsp;<asp:Label ID="Label451" runat="server" Font-Bold="True" ForeColor="Blue" Text="Return Qty" ></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" BorderStyle="Solid" Font-Names="Times New Roman" ></asp:TextBox>
                            </div>
                        </div>




                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label425" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Type" ></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox172" runat="server" BackColor="White" BorderStyle="Solid"  ForeColor="Red" ></asp:TextBox>
                            </div>
                        </div>



                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label24" runat="server" Font-Bold="True" ForeColor="Blue" Text="Cost Center" ></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox173" runat="server" BackColor="White" BorderStyle="Solid"  ForeColor="Red" ></asp:TextBox>
                            </div>
                        </div>


                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Blue" Text="Requisition By"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" class="form-control border-black"  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Blue" Text="Requisition Date"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" class="form-control border-black"  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Blue" Text="Authorization By"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" class="form-control border-black"  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Blue" Text="Authorization Date"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" class="form-control border-black"  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issued By"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" class="form-control border-black"  BackColor="White"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row align-items-center">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Blue" Text="Issue Date"></asp:Label>
                            </div>
                            <div class="col-8 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" class="form-control border-black"  BackColor="White"></asp:TextBox>
                            </div>
                        </div>


                        <div class="row align-items-center mb-2">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Blue" Text="Purpose" ></asp:Label>
                            </div>
                            <div class="col-8 text-start">

                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" Font-Names="Times New Roman" class="form-control border-black" TextMode="MultiLine"  BackColor="White"></asp:TextBox>

                            </div>
                        </div>
                    </div>

                    <div class="col-4 align-top">

                        <div class="row mt-2  align-items-center">
                            <div class="col-5 text-end">
                                <asp:Label ID="Label429" runat="server" Font-Bold="True" ForeColor="Blue">A/C Unit</asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox168" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>

                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label431" runat="server" Font-Bold="True" ForeColor="Blue" Text="Availble Stock"></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox167" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label430" runat="server" Font-Bold="True" ForeColor="Blue" Text="Unit Value"></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox166" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label432" runat="server" Font-Bold="True" ForeColor="Blue" Text="Line No"></asp:Label>
                            </div>
                            <div class="col-7 text-start">
                                <asp:TextBox CssClass="form-control" ID="TextBox169" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                            <div class="col-5 text-end">
                                <asp:Label ID="Label450" runat="server" Font-Bold="True" ForeColor="Blue" Text="Location"></asp:Label>
                            </div>
                            <div class="col-7 text-start">

                                <asp:TextBox CssClass="form-control" ID="TextBox170" class="form-control fw-bold" runat="server" BackColor="#559fe0" ForeColor="White" ></asp:TextBox>
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col-5 text-end">
                            </div>
                            <div class="col-7 text-start mt-2">
                                <asp:Button ID="Button45" runat="server" class="btn btn-primary fw-bold" Text="Save" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                                <asp:Button ID="Button46" runat="server" class="btn btn-danger fw-bold" Text="Cancel" />
                            </div>

                        </div>

                        <div class="row align-items-center">
                            <div class="col text-start">
                                <asp:Label ID="ISSUE_ERR_LABEL" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </div>

                        </div>
                    </div>



                </div>

            </div>

        </div>
    </div>

</asp:Content>



<%--===================================================--%>
