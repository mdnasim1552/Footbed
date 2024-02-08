<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="OrderInformation.aspx.cs" Inherits="SPEWEB.F_01_Mer.OrderInformation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            

<%--            var gvDayworder = $('#<%=this.gvDayworder.ClientID %>');
            gvDayworder.gridviewScroll({
                width: 1050,
                height: 400,

            });

            var grvShipment = $('#<%=this.grvShipment.ClientID %>');
            grvShipment.gridviewScroll({
                width: 1050,
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

    <%-- striped--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>

                        <%--  <div class="loader"></div> --%>
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
                                            <a class="nav-link show" data-toggle="tab" href="#dayorder">Day Wise Order</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayship">Day Wise Shipment</a>
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
                                                <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY ORDER & SHIPMENT</asp:Label>
                                                <asp:GridView ID="grvYearlySales" runat="server" AutoGenerateColumns="False"
                                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                    CssClass="table-condensed table-hover table-bordered grvContentarea " OnRowDataBound="grvYearlySales_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Order">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Size="10px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Shipment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="812px">D. WEEKLY  ORDER & SHIPMENT</asp:Label>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grvWeekSales" runat="server" AutoGenerateColumns="False"
                                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                        CssClass="table-condensed table-hover table-bordered grvContentarea" OnRowDataBound="grvWeekSales_RowDataBound">
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

                                                            <asp:TemplateField HeaderText="Order">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Shipment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Order">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Shipment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Order">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Shipment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Order">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                                            <asp:TemplateField HeaderText="Shipment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wsamt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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

                                            </div>



                                        </div>



                                    </div>
                                </div>
                                <div class="tab-pane fade" id="mon">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">
                                            <div class="col-sm-12 col-md-12 col-lg-12">
                                                <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY ORDER & SHIPMENT</asp:Label>

                                                <asp:GridView ID="grvMonthlySales" runat="server" AutoGenerateColumns="False"
                                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                    CssClass="table-condensed table-hover table-bordered grvContentarea" OnRowDataBound="grvMonthlySales_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Order">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle Font-Size="12px" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Shipment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-9">

                                            <asp:Label ID="lblGrp" runat="server" class="GrpHeader" Visible="false" Width="267px">E. GRAPH</asp:Label>

                                            <asp:Chart ID="Chart1" runat="server" Width="800px">
                                                <Series>
                                                    <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="#2fd1f9"
                                                        MarkerColor="black" MarkerStyle="None" Name="Series1" MarkerSize="4">
                                                    </asp:Series>
                                                    <asp:Series ChartArea="ChartArea1" ChartType="Column" Color="Pink"
                                                        MarkerColor="black" MarkerStyle="None" Name="Series2" MarkerSize="4">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1">
                                                        <AxisX MaximumAutoSize="100" Interval="1">
                                                        </AxisX>
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <Legends>
                                                    <asp:Legend></asp:Legend>
                                                </Legends>
                                            </asp:Chart>



                                        </div>
                                    </div>

                                </div>
                                <div class="tab-pane fade" id="dayorder">

                                    <div class="row">
                                        <div class="col-md-12 table-responsive" >

                                            <asp:Label ID="lblDetails" runat="server" class="GrpHeader" Visible="false" Width="267px">C. DAY WISE ORDER DETAILS</asp:Label>


                                            <asp:GridView ID="gvDayworder" runat="server" AutoGenerateColumns="False"
                                                Font-Size="10px" HorizontalAlign="Left" ShowFooter="True" Width="772px"
                                                CssClass="table-condensed table-hover table-bordered grvContentarea"
                                                OnRowDataBound="gvDayworder_RowDataBound">
                                           
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdayid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Master LC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmlcdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="220px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvorderdate" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orddate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="70px" Style="text-align: left"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Style Desc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstyledesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvunit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <%--<asp:TemplateField HeaderText="Currency">

                                            <ItemTemplate>
                                                <asp:Label ID="lgUnit" runat="server"
                                                    Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc"))
                                                                         %>'
                                                    Width="35px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Order </Br>Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvordrqty" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="65px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFordrqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="65px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvrate" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="65px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order </Br>Amount(FC)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvfcamt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFfcamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Order </Br>Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvDTAmt" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFDTAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="Black" Style="text-align: right" Width="75px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <%--<asp:TemplateField HeaderText="Expired Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvexdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="65px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>
                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>


                                        </div>
                                    </div>


                                </div>
                                <div class="tab-pane fade" id="dayship">
                                    <div class="row">
                                          <div class="col-md-12">
                                            <asp:Label ID="lblColl" runat="server" class="GrpHeader" Visible="false" Width="267px">C. DAY WISE SHIPMENT DETAILS</asp:Label>

                                            <asp:GridView ID="grvShipment" runat="server" AutoGenerateColumns="False"
                                                Font-Size="10px" HorizontalAlign="Left" ShowFooter="True" Width="772px"
                                                CssClass="table-condensed table-hover table-bordered grvContentarea"
                                                OnRowDataBound="grvShipment_RowDataBound">
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
                                                    <asp:TemplateField HeaderText="Invoice </Br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmemodat" runat="server" Width="60px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "memodat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice #">
                                                        <ItemTemplate>


                                                            <asp:HyperLink ID="hypInvoNum" runat="server"
                                                                Font-Size="12px" Font-Underline="False" ForeColor="Black" Target="_blank"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono1")) %>'
                                                                Width="90px"></asp:HyperLink>

                                                            <asp:Label ID="lblcenterid" runat="server" Width="90px" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrid")) %>'></asp:Label>

                                                            <asp:Label ID="lblmemo" runat="server" Width="90px" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono")) %>'></asp:Label>
                                                            <asp:Label ID="lblcustid" runat="server" Width="90px" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custid")) %>'></asp:Label>




                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Desc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcentrdesc" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrdesc")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcustdesc" runat="server" Width="200px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <asp:Label runat="server" ID="lblyTto">Total</asp:Label>

                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice </Br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitmamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFitmamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vat">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvat" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vat")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFvat"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvdis" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invdis")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFinvdis"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net </Br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnetamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFnetamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Realization">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcollamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblFcollamt"></asp:Label>
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
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


