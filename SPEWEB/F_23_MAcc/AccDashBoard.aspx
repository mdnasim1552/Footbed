<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccDashBoard.aspx.cs" Inherits="SPEWEB.F_23_MAcc.AccDashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highcharts.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>



    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            <%--$("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            var gvcashbook = $('#<%=this.gvcashbook.ClientID%>');
            gvcashbook.Scrollable();
            var gvcashbookp = $('#<%=this.gvcashbookp.ClientID%>');
            gvcashbookp.Scrollable();--%>



            funYearlyGraph();

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

                                <div class="col-md-8">

                                    <ul class="nav nav-tabs card-header-tabs" style="margin-top: 20px;">
                                        <li class="nav-item">
                                            <a class="nav-link active" data-toggle="tab" href="#yrwek">Yearly & Weekly</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show " data-toggle="tab" href="#mon">Monthly</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise1">Day Wise Receipts</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise2">Day Wise Payment</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise3">Cash & Bank Balance</a>
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
                                                <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="267px">A. YEARLY RECEIPT & PAYMENT</asp:Label>
                                                <asp:GridView ID="grvYearlySales" runat="server" AutoGenerateColumns="False"
                                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                    CssClass="table-condensed table-hover table-bordered grvContentarea" OnRowDataBound="grvYearlySales_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Receipt">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                                            </div>
                                            <div class="col-sm-12 col-md-12 col-lg-12">




                                                <div id="contyearlyprev" style="width: 125px !important; height: 150px; margin: 0 auto; float: left;"></div>
                                                <div id="contyearlycur" style="width: 130px !important; height: 150px; margin: 0 auto; float: left;"></div>








                                            </div>
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-9">


                                            <div class="row">



                                                <div class="col-xs-3 col-md-3 col-lg-3">
                                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="798px">C. WEEKLY RECEIPT & PAYMENT</asp:Label>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="grvWeekSales" runat="server" AutoGenerateColumns="False"
                                                        Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                        CssClass="table-condensed table-hover table-bordered grvContentarea gvTopHeader" OnRowDataBound="grvWeekSales_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl" Visible="false">
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label1">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt1" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt1">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt1T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label2">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label3">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label4">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt2" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt2">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt2T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label15">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label5">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" />
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label7">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt3" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt3">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt3T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label8">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label9">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Font-Size="10px" />
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="Label10">Bank</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Receipt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt4" runat="server" Width="55px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wramt4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt4">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFCollamt4T">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblFbRec">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment">
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
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblFbPay">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <HeaderStyle Font-Size="10px" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>




                                                        </Columns>
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-xs-5 col-md-5 col-lg-5">
                                                </div>
                                                <div class="col-xs-1 col-md-1 col-lg-1">
                                                    <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=20")%>' target="_blank" style="margin: 10px 0 0 5px; line-height: 18px;">NEXT</a>

                                                </div>
                                            </div>



                                        </div>



                                    </div>
                                </div>

                                <div class="tab-pane fade" id="mon">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">

                                            <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="267px">B. MONTHLY RECEIPT & PAYMENT</asp:Label>

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
                                                    <asp:TemplateField HeaderText="Receipt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyCollamt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFCollamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyAmt" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
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

                                          

                                            <div id="container" style="width: 830px; height: 282px; margin: 0 auto"></div>







                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="dayWise1">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">
                                            <asp:Label ID="lblReceiptCash" runat="server" Font-Bold="True" class="GrpHeader"
                                                Text="DAY WISE RECEIPTS" Width="1155px" Visible="False"></asp:Label>
                                            <div class=" clearfix"></div>
                                            <asp:GridView ID="gvcashbook" runat="server" Font-Size="10px" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="931px" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Cash">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCashAmt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvCashAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvBankAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBankAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Narration">
                                                        <HeaderTemplate>
                                                            <table style="width: 47%;">
                                                                <tr>
                                                                    <td class="style58">
                                                                        <asp:Label ID="Label13" runat="server" Font-Bold="True"
                                                                            Text="Narration" Width="130px"></asp:Label>
                                                                    </td>
                                                                    <td class="style60">&nbsp;</td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtnCBdataExel" runat="server" BackColor="#000066"
                                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                            ForeColor="White" Style="text-align: center" Width="70px">Export Exel</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNarrationR" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                                Width="190px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pay To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPaytoRec" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
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

                                            <asp:Label ID="lblPaymentCash" runat="server" class="GrpHeader"
                                                Text="DAY WISE PAYMENT" Width="1155px" Visible="False"></asp:Label>


                                            <asp:GridView ID="gvcashbookp" runat="server" Font-Size="10px" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="931px" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDatepay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat1")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvnumpay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cash/Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/Ref #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvCActDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Party/Suppliers/Receivers Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRDesc0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Cash">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvCashAmtpay" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "casham")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvCashAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bank">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvBankAmt0" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFBankAmt1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Narration">
                                                        <HeaderTemplate>
                                                            <table style="width: 47%;">
                                                                <tr>
                                                                    <td class="style58">
                                                                        <asp:Label ID="Label14" runat="server" Font-Bold="True"
                                                                            Text="Narration" Width="130px"></asp:Label>
                                                                    </td>
                                                                    <td class="style60">&nbsp;</td>
                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtnCBPdataExel" runat="server" BackColor="#000066"
                                                                            BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                            ForeColor="White" Style="text-align: center" Width="70px">Export Exel</asp:HyperLink>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvNarrationp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounar")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pay To">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpayto" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
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
                                <div class="tab-pane fade" id="dayWise3">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">

                                            <asp:Label ID="lblDetailsCash" runat="server" Font-Bold="True" class="GrpHeader"
                                                Text="DETAILS OF CASH &amp; BANK BALANCE" Width="1005px"
                                                Visible="False"></asp:Label>


                                            <asp:GridView ID="gvcashbookDB" runat="server" Font-Size="10px" AutoGenerateColumns="False" ShowFooter="True"
                                                Width="973px" CssClass="table-condensed table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Accounts Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvActDesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                Width="500px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Opening">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOpening" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFOpening" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receipt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrecam" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFrecam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpayam" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="100px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFpayam" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvClAmt" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                Height="18px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsam")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFClAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                                Style="text-align: right" Width="100px"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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

            <div>
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

        //$(function () {

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
                    text: 'Receipt'
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
                //series: [{
                //    name: 'Sales',
                //    data: [ys1],
                //    color: '#1581C1'

                //},
                //{

                //    name: 'Collection',
                //    //color:red,
                //    data: [yc1],
                //    color: '#CA6621'
                //    }
                //]


                series: [{
                    name: "Payment",
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
        //});


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
                    name: 'Receipt',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }]
            });
        });
    </script>
</asp:Content>

