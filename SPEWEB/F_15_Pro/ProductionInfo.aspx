<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProductionInfo.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProductionInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

           <%-- $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            var GvDayWise = $('#<%=this.GvDayWise.ClientID %>');
            GvDayWise.gridviewScroll({
                width: 850,
                height: 400,

            });

            var gvDayWiseExe = $('#<%=this.gvDayWiseExe.ClientID %>');
            gvDayWiseExe.gridviewScroll({
                width: 850,
                height: 400,

            });--%>
        }
    </script>
    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
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

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="card card-fluid">
                        <div class="card-body">

                            <div class="row">

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lbldatefrm" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>

                                    </div>

                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
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
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise1">Day Wise Target</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise2">Day Wise Execution</a>
                                        </li>


                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="card card-fluid" style="min-height: 350px;">
                        <div class="card-body">
                            <div id="myTabContent" class="tab-content">
                                <div class="tab-pane fade active show" id="yrwek">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div class="col-sm-12 col-md-12 col-lg-12">
                                                <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY TARGET Vs EXECUTION</asp:Label>
                                                <asp:GridView ID="grvYearlyProd" runat="server" AutoGenerateColumns="False"
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
                                                        <asp:TemplateField HeaderText="Target">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Size="10px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Execution">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 col-md-12 col-lg-12">

                                                <asp:Chart ID="Chart2" runat="server" Width="250px" Height="150px">
                                                    <Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="#2fd1f9"
                                                            MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4">
                                                        </asp:Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Pink"
                                                            MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4">
                                                        </asp:Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Green"
                                                            MarkerColor="black" MarkerStyle="None" Name="Series3" MarkerSize="4">
                                                        </asp:Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Red"
                                                            MarkerColor="black" MarkerStyle="None" Name="Series4" MarkerSize="4">
                                                        </asp:Series>
                                                        <asp:Series ChartArea="ChartArea1" ChartType="StackedColumn" Color="Blue"
                                                            MarkerColor="black" MarkerStyle="None" Name="Series5" MarkerSize="4">
                                                        </asp:Series>
                                                    </Series>



                                                    <ChartAreas>

                                                        <asp:ChartArea Name="ChartArea1">
                                                            <AxisX MaximumAutoSize="100" Interval="1">
                                                            </AxisX>
                                                            <AxisY LineColor="YellowGreen" Title="Taka in Crore"></AxisY>
                                                        </asp:ChartArea>
                                                    </ChartAreas>



                                                    <Titles>
                                                        <asp:Title Name="Amount">
                                                        </asp:Title>
                                                    </Titles>



                                                </asp:Chart>


                                            </div>
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-9">


                                            <div class="row">

                                                <div class="col-xs-3 col-md-3 col-lg-3">
                                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="812px">C. WEEKLY TARGET Vs EXECUTION</asp:Label>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grvWeekProd" runat="server" AutoGenerateColumns="False"
                                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                        CssClass="table-condensed table-hover table-bordered grvContentarea gvTopHeader" OnRowDataBound="grvWeekProd_RowDataBound">
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
                                                            <asp:TemplateField HeaderText="Days">
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

                                                            <asp:TemplateField HeaderText="Target">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Execution">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt1">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt1T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Days">
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

                                                            <asp:TemplateField HeaderText="Target">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Execution">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt2">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt2T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Days">
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

                                                            <asp:TemplateField HeaderText="Target">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Execution">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt3">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt3T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Days">
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

                                                            <asp:TemplateField HeaderText="Target">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Execution">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wcamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt4">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt4T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
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
                                                <%--<div class="col-xs-5 col-md-5 col-lg-5">
                                                </div>
                                                <div class="col-xs-1 col-md-1 col-lg-1">
                                                    <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=08")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                                                </div>
                                                <div class="col-xs-1 col-md-1 col-lg-1">
                                                    <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/F_13_ProdMon/EntryDailyProduction.aspx?Type=Entry")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">Hourly Production</a>

                                                </div>--%>
                                            </div>



                                        </div>



                                    </div>
                                </div>
                                <div class="tab-pane fade" id="mon">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">

                                            <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="280px">B. MONTHLY TARGET Vs EXECUTION</asp:Label>

                                            <asp:GridView ID="grvMonthlyMonth" runat="server" AutoGenerateColumns="False"
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
                                                            <asp:Label ID="lblprodNmId" runat="server" Width="50px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Execution">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFCollamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
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




                                            <script src="../Scripts/highcharts.js"></script>
                                            <script src="../Scripts/highchartexporting.js"></script>
                                            <%--<script src="https://code.highcharts.com/highcharts.js"></script>
                                <script src="https://code.highcharts.com/modules/exporting.js"></script>--%>

                                            <div id="container" style="width: 830px; height: 282px; margin: 0 auto"></div>







                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="dayWise1">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">
                                            <asp:Label ID="lblDetails" runat="server" class="GrpHeader" Visible="false" Width="400px">D. DAY WISE PRODUCTION TARGET DETAILS</asp:Label>
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
                                                    <asp:TemplateField HeaderText="Production </Br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmemodat" runat="server" Width="60px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbdate")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budget #" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpbno" runat="server" Width="70px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbno")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpreqno" runat="server" Width="70px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lot #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbatchdesc" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrsirdesc" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Production </Br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrsqty" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpreqamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFpreqamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbgdrat" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
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
                                <div class="tab-pane fade" id="dayWise2">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">
                                            <asp:Label ID="lblColl" runat="server" class="GrpHeader" Visible="false" Width="400px">E. DAY WISE EXECUTION DETAILS</asp:Label>
                                            <asp:GridView ID="gvDayWiseExe" runat="server" AutoGenerateColumns="False"
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
                                                    <asp:TemplateField HeaderText="Execution Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblprodate" runat="server" Width="60px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodate")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lot #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbatchdesc" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvounum2" runat="server" Width="70px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Store Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcentrdesc1" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrdesc")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrsirdesc" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <asp:Label runat="server" ID="lblFcustdesc1">Total</asp:Label>

                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Execution </Br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproqty" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFamount"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Execution </Br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFproamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrate" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                                        </ItemTemplate>

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

        <%--var yc1 = parseFloat($('#<%=this.yc1.ClientID%>').val());
        var yc2 = parseFloat($('#<%=this.yc2.ClientID%>').val());
        var yc3 = parseFloat($('#<%=this.yc3.ClientID%>').val());
        var ys1 = parseFloat($('#<%=this.ys1.ClientID%>').val());
        var ys2 = parseFloat($('#<%=this.ys2.ClientID%>').val());
        var ys3 = parseFloat($('#<%=this.ys3.ClientID%>').val());
        var xaxis0 = parseFloat($('#<%=this.xaxis0.ClientID%>').val());
        var xaxis1 = parseFloat($('#<%=this.xaxis1.ClientID%>').val());
        var xaxis2 = parseFloat($('#<%=this.xaxis2.ClientID%>').val());--%>


        /////////------------------------Yearly Graph---------------------

        ////$(function () {

        ////    Highcharts.setOptions({
        ////        lang: {
        ////            decimalPoint: ',',
        ////            thousandsSep: ' '
        ////        }
        ////    });




        ////    $('#contyearly').highcharts({


        ////        chart: {
        ////            type: 'column'
        ////        },
        ////        title: {
        ////            text: ''
        ////        },
        ////        //subtitle: {
        ////        //    text: 'Source: '
        ////        //},
        ////        xAxis: {
        ////            categories: [
        ////                xaxis0,
        ////                xaxis1,
        ////                xaxis2
        ////            ],
        ////            crosshair: true
        ////        },
        ////        yAxis: {
        ////            min: 0,
        ////            title: {
        ////                text: 'Amount'
        ////            }
        ////        },


        ////        tooltip: {
        ////            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
        ////            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
        ////                '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
        ////            footerFormat: '</table>',
        ////            shared: true,
        ////            useHTML: true,

        ////            //pointFormat: "{point.y:, .5f} Lac <br>"
        ////            //pointFormat: '{point.percentage:.0f}%'



        ////            //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


        ////            //<b>{point.y:.1f} mm</b>


        ////        },
        ////        plotOptions: {
        ////            column: {
        ////                pointPadding: 0.1,
        ////                borderWidth: 0


        ////            }
        ////        },
        ////        series: [{
        ////            name: 'Sales',
        ////            data: [ys1, ys2, ys3],
        ////            color: '#1581C1'

        ////        }, {

        ////            name: 'Collection',
        ////            //color:red,
        ////            data: [yc1, yc2, yc3],
        ////            color: '#CA6621'
        ////        }]
        ////    });
        ////});


        /////--------------------------Month Graph-------------------------

        $(function () {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });




            $('#container').highcharts({


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

                    //pointFormat: "{point.y:, .5f} Lac <br>"
                    //pointFormat: '{point.percentage:.0f}%'



                    //pointFormat: '{series.name}: <b>{point.percentage}%</b>',  pointFormat: '{series.name}: <b>{point.y}%</b>',


                    //<b>{point.y:.1f} mm</b>


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Production Target',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Actual Production',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }]
            });
        });
    </script>
</asp:Content>


