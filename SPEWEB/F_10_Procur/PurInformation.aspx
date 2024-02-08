<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurInformation.aspx.cs" Inherits="SPEWEB.F_10_Procur.PurInformation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highcharts.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {



            <%--var GvDayWise = $('#<%=this.GvDayWise.ClientID %>');
            GvDayWise.gridviewScroll({
                width: 850,
                height: 400,

            });

            var gvDayWisePay = $('#<%=this.gvDayWisePay.ClientID %>');
            gvDayWisePay.gridviewScroll({
                width: 850,
                height: 400,

            });--%>


            funYearlyGraph();
            funMonthlyGraph();



        }
    </script>
    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .linkItem {
            padding-right: 60px;
        }

            .linkItem a {
                margin: 4px 10px 0;
                font-size: 14px;
                line-height: 18px;
            }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>


                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>

                                    </div>

                                </div>

                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <ul class="nav nav-tabs card-header-tabs" style="margin-top: 20px;">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#yrwek">Yearly & Weekly</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show " data-toggle="tab" href="#mon">Monthly</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise1">Day Wise Purchase</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise2">Day Wise Payment</a>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                        </div>
                    </div>


                    <div class="card card-fluid" style="min-height: 350px;">
                        <div class="card-body">
                            <div class="tab-content">

                                <div class="tab-pane fade active show" id="yrwek">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">

                                            <%--First GirdView--%>
                                            <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY PURCHASE & PAYMENT</asp:Label>
                                            <asp:GridView ID="grvYearlyPur" runat="server" AutoGenerateColumns="False"
                                                Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                                CssClass="gridtext"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="20px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblprodNmId" runat="server" Width="50px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearid")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="10px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purchase">
                                                        <HeaderTemplate>
                                                            <a href="#" data-toggle="modal" data-target="#myModal2">Purchase</a>
                                                        </HeaderTemplate>


                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="10px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">

                                                        <HeaderTemplate>
                                                            <a href="#" data-toggle="modal" data-target="#myModal1">Payment</a>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyPayAmt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purpay")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="10px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>



                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>



                                            <div class="col-sm-12 col-md-12 col-lg-12">
                                                <div id="contyearlyprev" style="width: 125px !important; height: 150px; margin: 0 auto; float: left;"></div>
                                                <div id="contyearlycur" style="width: 130px !important; height: 150px; margin: 0 auto; float: left;"></div>
                                            </div>

                                        </div>

                                        <div class="col-sm-9 col-md-9 col-lg-9">


                                            <div class="row">



                                                <div class="col-xs-3 col-md-3 col-lg-3">
                                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="798px">C. WEEKLY PURCHASE & PAYMENT</asp:Label>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">

                                                    <%--Second GriView--%>

                                                    <asp:GridView ID="grvWeekPur" runat="server" AutoGenerateColumns="False"
                                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True" Style="font-size: 10px; width: 95%; border-collapse: collapse;"
                                                        CssClass="table-condensed table-hover table-bordered grvContentarea gvTopHeader" OnRowDataBound="grvWeekPur_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSwlNo1" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                                        CssClass="gridtext"></asp:Label>
                                                                </ItemTemplate>
                                                                <ControlStyle Width="20px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DAYS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode1")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl1">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Purchase">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt1">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt1T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyPayAmt1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt1">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt1T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="DAYS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode2")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl2">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT2">Sub-Total:</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Purchase">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt2">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt2T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyPayAmt2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt2">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt2T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DAYS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode3")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl3">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT3">Sub-Total:</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Purchase">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt3">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt3T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyPayAmt3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt3">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt3T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DAYS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode4")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl4">Week Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT4">Gr Total:</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Purchase">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt4">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFAmt4T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Payment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyPayAmt4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wpayamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt4">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFPatAmt4T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>


                                                <div class="col-xs-2 col-md-2 col-lg-2">
                                                </div>
                                                <div class="col-xs-10 col-md-10 col-lg-10  linkItem" runat="server" visible="false">
                                                    <div class="pull-right">
                                                        <a href='<%=this.ResolveUrl("~/GenPage.aspx?Type=06")%>' target="_blank">FOREIGN</a>
                                                        <a href='<%=this.ResolveUrl("~/GenPage.aspx?Type=07")%>' target="_blank">LOCAL</a>

                                                    </div>
                                                </div>


                                            </div>



                                        </div>

                                    </div>
                                </div>

                                <div class="tab-pane fade" id="mon">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">

                                            <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY PURCHASE & PAYMENT</asp:Label>

                                            <asp:GridView ID="grvMonthlyPur" runat="server" AutoGenerateColumns="False"
                                                Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                                CssClass="gridtext"></asp:Label>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="20px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblprodNmId" runat="server" Width="60px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purchase">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlsalamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblytpayamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tpayamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFtpayamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>




                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-9">

                                            <%--<script src="https://code.highcharts.com/highcharts.js"></script>
                                <script src="https://code.highcharts.com/modules/exporting.js"></script>--%>

                                            <div id="containerPur" style="width: 830px; height: 282px; margin: 0 auto"></div>

                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane fade" id="dayWise1">
                                    <div class="row">

                                        <asp:Label ID="lblDetails" runat="server" class="GrpHeader" Visible="false" Width="823px">D. DAY WISE PURCHASE DETAILS</asp:Label>

                                        <asp:GridView ID="GvDayWise" runat="server" AutoGenerateColumns="False"
                                            Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                            CssClass="table-condensed table-hover table-bordered grvContentarea">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                            CssClass="gridtext"></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="20px" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bill </Br> Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmemodat" runat="server" Width="70px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdate1")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbillno1" runat="server" Width="75px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Voucher #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvounum1" runat="server" Width="75px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accounts Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpactdesc" runat="server" Width="200px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Materials Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrsirdesc" runat="server" Width="300px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supplier Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblssirdesc" runat="server" Width="200px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                        <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bill </Br> Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbillamt" runat="server" Width="80px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblFitmamt"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>





                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <FooterStyle CssClass="grvFooter" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>

                                    </div>
                                </div>

                                <div class="tab-pane fade" id="dayWise2">
                                    <div class="row">

                                        <asp:Label ID="lblPayDet" runat="server" class="GrpHeader" Visible="false" Width="823px">E. DAY WISE PAYMENT DETAILS</asp:Label>
                                        <asp:GridView ID="gvDayWisePay" runat="server" AutoGenerateColumns="False"
                                            Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                            CssClass="table-condensed table-hover table-bordered grvContentarea">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                            CssClass="gridtext"></asp:Label>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="20px" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="VOUCHER</BR> Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvoudat" runat="server" Width="70px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VOUCHER #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblvounum1" runat="server" Width="75px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BILL #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbillno1" runat="server" Width="70px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accounts Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpactdesc" runat="server" Width="150px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supplier Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblssirdesc" runat="server" Width="200px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcactdesc" runat="server" Width="200px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>

                                                        <asp:Label runat="server" ID="lblFcustdesc1">Total</asp:Label>

                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pay </Br> Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblamount" runat="server" Width="80px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblFamount"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Size="12px" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                </asp:TemplateField>


                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <FooterStyle CssClass="grvFooter" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




            <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h2>
                                <asp:Label ID="Label2" runat="server">Payment Growth</asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="Panel1" runat="server">

                                <asp:GridView ID="gvPaymDet" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoPayDet" runat="server" Font-Bold="True"
                                                    Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                    CssClass="gridtext"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" Font-Size="13" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypNmIdpur" runat="server" Width="80px" Target="_blank" ForeColor="Blue" Style="font-size: 14px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lblyTtocs">Total</asp:Label>

                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonAmtPayDet" runat="server" Width="80px" Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prvpay")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblpayamtprev"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtPayDet" runat="server" Width="80px" Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpay")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblpayamtcur"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Growth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtdifPayDet" runat="server" Width="100px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffpay")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryDiffPayDet"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="14px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Growth %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtperPayDet" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntpay")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtPrcPayDet"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" Width="100" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="14px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <%-- <a href="#" data-toggle="modal" data-target="#myModalColDet">Day Wise Collection</a>--%>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>

                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h2>
                                <asp:Label ID="Label3" runat="server">Purchase Growth</asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="Panel2" runat="server">

                                <asp:GridView ID="gvPurDet" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNoPurdet" runat="server" Font-Bold="True"
                                                    Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                    CssClass="gridtext"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypNmIdPurdet" runat="server" Width="80px" Target="_blank" ForeColor="Blue" Style="font-size: 14px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lblyTtopur">Total</asp:Label>

                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonAmtPurdet" runat="server" Width="80px" Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prvpur")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFPrevAmtPurdet"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtPurdet" runat="server" Width="80px" Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curpur")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtPurdet"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Growth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtdifPurdet" runat="server" Width="100px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffpur")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryDiffPurdet"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="14px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Growth %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtperPurdet" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percntpur")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtPrcPurdet"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" Width="100" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="14px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                                <div class="clearfix"></div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <%-- <a href="#" data-toggle="modal" data-target="#myModalColDet">Day Wise Collection</a>--%>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>

                    </div>
                </div>
            </div>





            <div>
                <asp:TextBox ID="c1" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c2" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c3" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c4" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c5" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c6" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c7" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c8" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c9" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c10" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c11" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="c12" runat="server" Style="display: none;"></asp:TextBox>

                <asp:TextBox ID="s1" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s2" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s3" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s4" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s5" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s6" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s7" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s8" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s9" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s10" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s11" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="s12" runat="server" Style="display: none;"></asp:TextBox>



                <asp:TextBox ID="yc1" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="yc2" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="yc3" runat="server" Style="display: none;"></asp:TextBox>

                <asp:TextBox ID="ys1" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="ys2" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="ys3" runat="server" Style="display: none;"></asp:TextBox>

                <asp:TextBox ID="xaxis0" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="xaxis1" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="xaxis2" runat="server" Style="display: none;"></asp:TextBox>


            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        var s1 = parseFloat($('#<%=this.s1.ClientID%>').val());
        var s2 = parseFloat($('#<%=this.s2.ClientID%>').val());
        var s3 = parseFloat($('#<%=this.s3.ClientID%>').val());
        var s4 = parseFloat($('#<%=this.s4.ClientID%>').val());
        var s5 = parseFloat($('#<%=this.s5.ClientID%>').val());
        var s6 = parseFloat($('#<%=this.s6.ClientID%>').val());
        var s7 = parseFloat($('#<%=this.s7.ClientID%>').val());
        var s8 = parseFloat($('#<%=this.s8.ClientID%>').val());
        var s9 = parseFloat($('#<%=this.s9.ClientID%>').val());
        var s10 = parseFloat($('#<%=this.s10.ClientID%>').val());
        var s11 = parseFloat($('#<%=this.s11.ClientID%>').val());
        var s12 = parseFloat($('#<%=this.s12.ClientID%>').val());




        var c1 = parseFloat($('#<%=this.c1.ClientID%>').val());
        var c2 = parseFloat($('#<%=this.c2.ClientID%>').val());
        var c3 = parseFloat($('#<%=this.c3.ClientID%>').val());
        var c4 = parseFloat($('#<%=this.c4.ClientID%>').val());
        var c5 = parseFloat($('#<%=this.c5.ClientID%>').val());
        var c6 = parseFloat($('#<%=this.c6.ClientID%>').val());
        var c7 = parseFloat($('#<%=this.c7.ClientID%>').val());
        var c8 = parseFloat($('#<%=this.c8.ClientID%>').val());
        var c9 = parseFloat($('#<%=this.c9.ClientID%>').val());
        var c10 = parseFloat($('#<%=this.c10.ClientID%>').val());
        var c11 = parseFloat($('#<%=this.c11.ClientID%>').val());
        var c12 = parseFloat($('#<%=this.c12.ClientID%>').val());

        var yc1 = parseFloat($('#<%=this.yc1.ClientID%>').val());
        var yc2 = parseFloat($('#<%=this.yc2.ClientID%>').val());
        var yc3 = parseFloat($('#<%=this.yc3.ClientID%>').val());
        var ys1 = parseFloat($('#<%=this.ys1.ClientID%>').val());
        var ys2 = parseFloat($('#<%=this.ys2.ClientID%>').val());
        var ys3 = parseFloat($('#<%=this.ys3.ClientID%>').val());
        var xaxis0 = parseFloat($('#<%=this.xaxis0.ClientID%>').val());
        var xaxis1 = parseFloat($('#<%=this.xaxis1.ClientID%>').val());
        var xaxis2 = parseFloat($('#<%=this.xaxis2.ClientID%>').val());


        /////////------------------------Yearly Graph---------------------

        function funYearlyGraph() {
            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });




            $('#contyearlyprev').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Purchase'
                },
                xAxis: {
                    categories: [
                        xaxis0,
                        // xaxis1

                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },


                series: [{
                    name: "Purchase",
                    colorByPoint: true,
                    data: [{
                        name: xaxis0,
                        y: ys1,
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: xaxis1,
                        y: ys2,
                        color: '#1581C1'
                        //drilldown: null
                    }]
                }],



            });

            $('#contyearlycur').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Payment'
                },
                xAxis: {
                    categories: [
                        //xaxis0,
                        // xaxis1

                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: "Payment",
                    colorByPoint: true,
                    data: [{
                        name: xaxis0,
                        y: yc1,
                        color: '#CA6621'
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: xaxis1,
                        y: yc2,

                        color: '#A33F07'
                        //drilldown: null
                    }]
                }],



            });

        }


        /////--------------------------Month Graph-------------------------

        function funMonthlyGraph() {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });




            $('#containerPur').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                //subtitle: {
                //    text: 'Source: '
                //},
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Amount'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,

                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Purchase',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }]
            });
        }
    </script>


</asp:Content>

