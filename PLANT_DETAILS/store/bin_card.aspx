﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" CodeBehind="bin_card.aspx.vb" Inherits="PLANT_DETAILS.bin_card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper" style="font-family: 'Times New Roman', Times, serif">
            <asp:Label ID="Label1" runat="server" Text="SAIL REFRACTORY UNIT BHILAI" Font-Bold="True" Font-Size="XX-Large" ForeColor="#800040" Style="text-align: center" Width="100%"></asp:Label>
        </div>
    </section>
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $("[id$=TextBox115]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Service.asmx/material")%>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('^')[0],
                                    val: item.split('^')[1],
                                    MAT_AU: item.split('^')[2]
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
    <%--<link href="../Content/Site.css" rel="stylesheet" type="text/css" />--%>

    <div class="row text-white" style="background: #296DA9">
        <div class="col text-center">
            <asp:Label ID="Label7" runat="server" Text="Store Material Master" Font-Bold="True" Font-Size="Larger"></asp:Label>
        </div>
    </div>


    <div class="container-fluid">
        <div class="row mt-3 justify-content-center text-center">
            <div class="col-10" style="border: 3px groove #33CC33; float: left; text-align: left;">
                <div class="row justify-content-center">
                    <div class="col-12">
                        <div class="row align-items-center m-1">
                            <div class="col-4 text-end">
                                <asp:Label ID="Label40" runat="server" Font-Bold="True" ForeColor="Blue" Text="Mat Name"></asp:Label>

                            </div>
                            <div class="col-5 text-start">
                                <asp:TextBox ID="TextBox115" runat="server" class="form-control border-black"></asp:TextBox>

                            </div>
                            <div class="col-2 text-start">
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary fw-bold" Text="VIEW" UseSubmitBehavior="false" OnClientClick="this.disabled='true'; this.value='Please Wait...'" />
                            </div>
                        </div>

                        <div class="row align-items-center m-2">
                            <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" Font-Names="Times New Roman" Style="text-align: left">

                                <asp:FormView ID="imp_FormView1" runat="server" CellPadding="4" CellSpacing="2" Font-Names="Times New Roman" ForeColor="Black" GridLines="Both" Height="56%" Width="100%" BackColor="#D7D6D7" Font-Size="Medium">
                                    <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                    <ItemTemplate>
                                        <div class="row align-items-center">
                                            <div class="col-2">
                                                <h6><asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Mat. Code - "></asp:Label></h6>

                                            </div>
                                            <div class="col-5 text-start">
                                                <h6 style="font-family: 'Times New Roman', Times, serif; color: #0000FF;"><%# Eval("MAT_CODE")%></h6>

                                            </div>
                                        </div>
                                        
                                        <div class="row align-items-center">
                                            <div class="col-2">
                                                <h6><asp:Label ID="Label15" runat="server" Font-Bold="True" Text="Mat. Name - "></asp:Label></h6>

                                            </div>
                                            <div class="col-10 text-start">
                                                <h6 style="font-family: 'Times New Roman', Times, serif; color: #0000FF;"><%# Eval("MAT_NAME")%></h6>

                                            </div>
                                        </div>

                                        <div class="row align-items-center">
                                            <div class="col-2">
                                                <h6><asp:Label ID="Label16" runat="server" Font-Bold="True" Text="Mat. Drawing - "></asp:Label></h6>

                                            </div>
                                            <div class="col-5 text-start">
                                                <h6 style="font-family: 'Times New Roman', Times, serif; color: #0000FF;"><%# Eval("MAT_DRAW")%></h6>

                                            </div>
                                        </div>


                                        <div class="row align-items-center mt-3">
                                            <div class="col-2">
                                                <asp:Label ID="Label507" runat="server" Font-Bold="True" Text="Mat. Stock :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label509" runat="server" Text='<%# Eval("MAT_STOCK")%>'></asp:Label>
                                            </div>

                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Accounting Unit :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label17" runat="server" Text='<%# Eval("MAT_AU")%>'></asp:Label>
                                            </div>
                                        </div>
                                        
                                        

                                        <div class="row align-items-center mt-2">
                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label18" runat="server" Font-Bold="True" Text="Mat. Location :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label19" runat="server" Text='<%# Eval("MAT_LOCATION")%>'></asp:Label>
                                            </div>

                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Text="Last Transaction :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label21" runat="server" Text='<%# Eval("LAST_TRANS_DATE")%>'></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col-2">
                                                <asp:Label ID="Label22" runat="server" Font-Bold="True" Text="Last Purchase :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label23" runat="server" Text='<%# Eval("MAT_LASTPUR_DATE")%>'></asp:Label>
                                            </div>

                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label24" runat="server" Font-Bold="True" Text="Last Issue :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label25" runat="server" Text='<%# Eval("LAST_ISSUE_DATE")%>'></asp:Label>
                                            </div>
                                        </div>

                                        <div class="row align-items-center mt-2">
                                            <div class="col-2">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="True" Text="Average Price :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label27" runat="server" Text='<%# Eval("MAT_AVG")%>'></asp:Label>
                                            </div>

                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label28" runat="server" Font-Bold="True" Text="Last Pur. Price :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label29" runat="server" Text='<%# Eval("MAT_LAST_RATE")%>'></asp:Label>
                                            </div>
                                        </div>


                                        <div class="row align-items-center mt-2">
                                            <div class="col-2">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Purpose :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("PURPOSE")%>'></asp:Label>
                                            </div>

                                            <div class="col-2 text-start">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Remarks :"></asp:Label>

                                            </div>
                                            <div class="col-4 text-start">
                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("REMARKS")%>'></asp:Label>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                    <RowStyle BackColor="White" />
                                </asp:FormView>
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>