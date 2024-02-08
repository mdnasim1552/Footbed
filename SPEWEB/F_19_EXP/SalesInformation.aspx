<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SalesInformation.aspx.cs" Inherits="SPEWEB.F_19_EXP.SalesInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/highchartwithmap.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {



            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            var s1 = parseFloat($('#<%=this.s1.ClientID%>').val());
            var s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12;

            var c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12;

            var b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12;

            var yc1, yc2, yc3, ys1, ys2, ys3, xaxis0, xaxis1, xaxis2;




        });
        function pageLoaded() {

          

            <%--var GvDayWise = $('#<%=this.GvDayWise.ClientID %>');
            GvDayWise.gridviewScroll({
                width: 1050,
                height: 400,

            });

            var gvDayWiseColl = $('#<%=this.gvDayWiseColl.ClientID %>');
            gvDayWiseColl.gridviewScroll({
                width: 800,
                height: 400,

            });--%>

            //funYearlyGraph();
            //funMonthlyGraph();
            //funMonthlyLineChart();
            //funMonthlyPieChart();
            //funMonthlyGraphArea();

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

        .innertab .panel.with-nav-tabs .panel-heading {
            padding: 5px 5px 0 5px;
        }

         .innertab .panel.with-nav-tabs .nav-tabs {
            border-bottom: none;
        }

      .innertab .panel.with-nav-tabs .nav-justified {
            margin-bottom: -1px;
        }
        /*** PANEL PRIMARY ***/
       .innertab .with-nav-tabs.panel-primary .nav-tabs > li > a,
      .innertab .with-nav-tabs.panel-primary .nav-tabs > li > a:hover,
      .innertab .with-nav-tabs.panel-primary .nav-tabs > li > a:focus {
            color: #fff;
        }

        .innertab .with-nav-tabs.panel-primary .nav-tabs > .open > a,
          .innertab .with-nav-tabs.panel-primary .nav-tabs > .open > a:hover,
          .innertab .with-nav-tabs.panel-primary .nav-tabs > .open > a:focus,
          .innertab .with-nav-tabs.panel-primary .nav-tabs > li > a:hover,
          .innertab .with-nav-tabs.panel-primary .nav-tabs > li > a:focus {
                color: #fff;
                background-color: #3071a9;
                border-color: transparent;
            }

       .innertab .with-nav-tabs.panel-primary .nav-tabs > li.active > a,
       .innertab .with-nav-tabs.panel-primary .nav-tabs > li.active > a:hover,
     .innertab .with-nav-tabs.panel-primary .nav-tabs > li.active > a:focus {
            color: #428bca;
            background-color: #fff;
            border-color: #428bca;
            border-bottom-color: transparent;
        }

       .innertab .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu {
            background-color: #428bca;
            border-color: #3071a9;
        }

        .innertab .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a {
                color: #fff;
            }

             .innertab .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a:hover,
              .innertab .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > li > a:focus {
                    background-color: #3071a9;
                }

           .innertab .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a,
           .innertab .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a:hover,
           .innertab .with-nav-tabs.panel-primary .nav-tabs > li.dropdown .dropdown-menu > .active > a:focus {
                background-color: #4a9fe9;
            }

        /*#MyMap {
    height: 500px; 
    min-width: 310px; 
    max-width: 800px; 
    margin: 0 auto; 
}*/
        .loading {
            margin-top: 10em;
            text-align: center;
            color: gray;
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
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise1">Day Wise Export</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link show" data-toggle="tab" href="#dayWise2">Day Wise Realization</a>
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

                                            <asp:Label ID="lblYear" runat="server" class="GrpHeader" Visible="false" Width="100%">A. YEARLY EXPORT & REALIZATION</asp:Label>
                                            <asp:GridView ID="grvYearlySales" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" PageSize="5" ShowFooter="True"
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
                                                            <asp:Label ID="lblprodNmId" runat="server" Width="30px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearid")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="10px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Export">
                                                        <HeaderTemplate>
                                                            <a href="#" data-toggle="modal" data-target="#myModal">EXPORT</a>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>


                                                            <asp:Label ID="lblyAmt" runat="server" Width="70px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="10px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Realization">
                                                        <HeaderTemplate>


                                                            <a href="#" data-toggle="modal" data-target="#myModal2">Realization</a>


                                                        </HeaderTemplate>


                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyCollamt" runat="server" Width="70px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Balance">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBal" runat="server" Width="51px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balance")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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


                                            <div id="contyearlyprev" style="width: 125px !important; height: 150px; margin: 0 auto; float: left;"></div>
                                            <div id="contyearlycur" style="width: 130px !important; height: 150px; margin: 0 auto; float: left;"></div>




                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-9">


                                            <div class="row">
                                                <div class="col-xs-3 col-md-3 col-lg-3">
                                                    <asp:Label ID="lblWeek" runat="server" class="GrpHeader btn-block" Visible="false" Width="905px">D. WEEKLY EXPORT & REALIZATION</asp:Label>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-sm-12 table-responsive">
                                                    <asp:GridView ID="grvWeekSales" runat="server" AutoGenerateColumns="False" Width="95%"
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
                                                                    <asp:Label ID="lblprodNmId1" runat="server" Width="35px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode1")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl1">Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Export">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt1" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Realization">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt1" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblywbal1" runat="server" Width="45px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wbal1")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal1">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal1T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId2" runat="server" Width="35px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode2")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl2">Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT2">S-Total:</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Export">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt2" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Realization">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt2" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblywbal2" runat="server" Width="45px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wbal2")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal2">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal2T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId3" runat="server" Width="35px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode3")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl3">Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT3">S-Total:</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Export">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt3" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Realization">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt3" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblywbal3" runat="server" Width="45px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wbal3")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal3">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal3T">-</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="10px" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Days">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprodNmId4" runat="server" Width="35px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wcode4")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lbl4">Total</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFT4">Gr Total:</asp:Label>
                                                                    </p>

                                                                </FooterTemplate>
                                                                <HeaderStyle Font-Size="10px" ForeColor="Green" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Size="8px" Font-Bold="true" ForeColor="Green" />
                                                                <ItemStyle ForeColor="Green" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Export">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyAmt4" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Realization">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblyCollamt4" runat="server" Width="45px"
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

                                                            <asp:TemplateField HeaderText="Balance">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblywbal4" runat="server" Width="45px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wbal4")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal4">-</asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        <asp:Label runat="server" ID="lblyFbal4T">-</asp:Label>
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

                                                <%-- <div class="col-xs-12 col-md-12 col-lg-12  linkItem">
                                                    <div class="pull-right">
                                                        <a class="" href='<%=this.ResolveUrl("~/F_23_SaM/SalesStatusGraph.aspx")%>' target="_blank">Graph</a>


                                                        <a class="" href='<%=this.ResolveUrl("~/F_23_SaM/SalesInterface.aspx")%>' target="_blank">Interface</a>
                                                        <a class="" href='<%=this.ResolveUrl("~/F_23_SaM/RptSalescCollOutStanding.aspx")%>' target="_blank">Analysis</a>
                                                        <a class="" href='<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalesPer")%>' target="_blank">Export Performance</a>

                                                        <a class="" href='<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=Prolift")%>' target="_blank">Client Performance</a>
                                                        <a class="" href='<%=this.ResolveUrl("~/F_15_Acc/RptCustPosition.aspx?Type=CustPos")%>' target="_blank">Dues</a>



                                                        <a class="" href='<%=this.ResolveUrl("~/GenPage.aspx?Type=14")%>' target="_blank">All Reports</a>
                                                    </div>
                                                </div>--%>
                                            </div>



                                        </div>


                                    </div>
                                </div>
                                <div class="tab-pane fade" id="mon">
                                    <div class="row">
                                        <div class="col-sm-3 col-md-3 col-lg-3">

                                            <asp:Label ID="lblMon" runat="server" class="GrpHeader" Visible="false" Width="296px">B. MONTHLY EXPORT & REALIZATION</asp:Label>

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
                                                    <asp:TemplateField HeaderText="Export">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyAmt" runat="server" Width="60px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlsalamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFAmt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Realization">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblyCollamt" runat="server" Width="60px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblyFCollamt"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Balance">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmbal" runat="server" Width="60px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bal")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblmFbal"></asp:Label>
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
                                        <div class="col-sm-9 col-md-9 col-lg-9 innertab">

                                            <div class="panel  panel-primary">
                                                <div class="panel-heading">
                                                    <ul class="nav nav-tabs">
                                                        <li class=""><a href="#tab0primary" class="btn-sm btn-primary active" data-toggle="tab" style="font-size: 18px"><span class="glyphicon glyphicon-eye-open"></span>At a Glance</a></li>
                                                        <li><a href="#tab1primary" class="btn-sm btn-success" data-toggle="tab" style="font-size: 18px"><span class="glyphicon glyphicon-stats"></span>Bar Chart</a></li>

                                                        <li><a href="#tab2primary" class="btn-sm btn-danger" data-toggle="tab" style="font-size: 18px"><span class="glyphicon glyphicon-random"></span>Line Chart</a></li>
                                                        <li><a href="#tab3primary" class="btn-sm btn-warning" data-toggle="tab" style="font-size: 18px"><span class="glyphicon glyphicon-cd"></span>Pie Chart </a></li>
                                                        <li><a href="#tab4primary" class="btn-sm btn-info" data-toggle="tab" style="font-size: 18px"><span class="glyphicon glyphicon-cloud"></span>Area Chart </a></li>
                                                        <%-- <li><a href="#tab5primary" data-toggle="tab" style="font-size: 18px"><span class="glyphicon glyphicon-cloud"></span> Map Chart </a></li>--%>
                                                    </ul>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="tab-content">
                                                        <div class="tab-pane fade in active show" id="tab0primary">
                                                            <div class="container content">

                                                                <div id="carouselExampleSlidesOnly" class="carousel slide" data-ride="carousel">
                                                                    <div class="carousel-inner">
                                                                        <div class="carousel-item active">
                                                                            <div id="barchart1" style="width: 830px; height: 282px; margin: 0 auto"></div>


                                                                        </div>
                                                                        <div class="carousel-item">
                                                                            <div id="linechart1" style="width: 830px; height: 282px; margin: 0 auto"></div>

                                                                        </div>
                                                                        <div class="carousel-item">
                                                                            <div id="piechart1" style="width: 830px; height: 282px; margin: 0 auto"></div>

                                                                        </div>
                                                                        <div class="carousel-item">
                                                                            <div id="archart1" style="width: 830px; height: 282px; margin: 0 auto"></div>


                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        <div class="tab-pane fade" id="tab1primary">
                                                            <div id="barchart" style="width: 830px; height: 282px; margin: 0 auto"></div>
                                                        </div>
                                                        <div class="tab-pane fade " id="tab2primary">
                                                            <div id="linechart" style="width: 830px; height: 282px; margin: 0 auto"></div>

                                                        </div>
                                                        <div class="tab-pane fade " id="tab3primary">
                                                            <div id="piechart" style="width: 830px; height: 282px; margin: 0 auto"></div>

                                                        </div>
                                                        <div class="tab-pane fade " id="tab4primary">
                                                            <div id="archart" style="width: 830px; height: 282px; margin: 0 auto"></div>

                                                        </div>
                                                        <%-- <div class="tab-pane fade " id="tab5primary">
                                         <div id="MyMap"></div>

                                    </div>--%>
                                                    </div>
                                                </div>
                                            </div>









                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="dayWise1">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">
                                            <asp:Label ID="lblDetails" runat="server" class="GrpHeader" Visible="false" Width="267px">C. DAY WISE EXPORT DETAILS</asp:Label>
                                            <asp:GridView ID="GvDayWise" runat="server" AutoGenerateColumns="False"
                                                Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                                CssClass="table-condensed table-hover table-bordered grvContentarea" OnRowDataBound="GvDayWise_RowDataBound">
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
                                <div class="tab-pane fade" id="dayWise2">
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12 col-lg-12">
                                            <asp:Label ID="lblColl" runat="server" class="GrpHeader" Visible="false" Width="267px">C. DAY WISE REALIZATION DETAILS</asp:Label>
                                            <asp:GridView ID="gvDayWiseColl" runat="server" AutoGenerateColumns="False"
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
                                                    <asp:TemplateField HeaderText="MR Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmrdat" runat="server" Width="60px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MR #">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmrslno1" runat="server" Width="70px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrslno1")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Desc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcentrdesc1" runat="server" Width="150px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrdesc")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcustdesc1" runat="server" Width="200px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>

                                                            <asp:Label runat="server" ID="lblFcustdesc1">Total</asp:Label>

                                                        </FooterTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Realization </Br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblamount" runat="server" Width="80px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
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
                    </div>


                </div>
            </div>

            <div>
                <asp:TextBox ID="b1" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b2" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b3" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b4" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b5" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b6" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b7" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b8" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b9" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b10" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b11" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="b12" runat="server" Style="display: none;"></asp:TextBox>

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




            </div>

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h2>
                                <asp:Label ID="lbltitel" runat="server"></asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="pnlCompareSales" runat="server">

                                <asp:GridView ID="grvCompareYear" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNocs" runat="server" Font-Bold="True"
                                                    Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                    CssClass="gridtext"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" Font-Size="12" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypNmIdcs" runat="server" Width="80px" Target="_blank" ForeColor="Blue" Style="font-size: 14px;"
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
                                                <asp:Label ID="lblMonAmtcs" runat="server" Width="80px" Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFPrevAmtcs"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtcs" runat="server" Width="80px" Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cursal")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtcs"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Growth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtdif" runat="server" Width="100px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryDiff"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="14px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Growth %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtper" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtcsps"></asp:Label>
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

                            <a href="#" data-toggle="modal" data-target="#myModalSaDet">Customer Wise Export</a>
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
                                <asp:Label ID="lbltitel2" runat="server"></asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="pnlCollection" runat="server">

                                <asp:GridView ID="grvCompareYearColl" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNocoll" runat="server" Font-Bold="True"
                                                    Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                    CssClass="gridtext"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" Font-Size="12" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypNmIdcoll" runat="server" Width="80px" Target="_blank" ForeColor="Blue" Style="font-size: 14px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "yearmon")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lblyTtocs">Total</asp:Label>

                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonAmtcoll" runat="server" Width="80px" Style="text-align: right; font-size: 12px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFPrevAmtcoll"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtcoll" runat="server" Width="80px" Style="text-align: right; font-size: 12px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cursal")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtcoll"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Growth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtdifcoll" runat="server" Width="100px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryDiffcoll"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Growth %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtpercoll" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtPrccoll"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" Width="100" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
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
                            <a href="#" data-toggle="modal" data-target="#myModalColDet">Customer Wise Realization</a>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>

                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModalSaDet" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h2>
                                <asp:Label ID="lblModSalDay" runat="server"></asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="Panel1" runat="server">

                                <asp:GridView ID="gvCompDayWise" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNocs" runat="server" Font-Bold="True"
                                                    Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                    CssClass="gridtext"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" Font-Size="13" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutomer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblresdesc" runat="server" Width="200px" Style="font-size: 11px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lblyTtocs">Total</asp:Label>

                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonAmtcs" runat="server" Width="80px" Style="text-align: right; font-size: 12px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFPrevAmtcs"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtcs" runat="server" Width="80px" Style="text-align: right; font-size: 12px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cursal")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtcs"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Growth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtdif" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryDiff"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Growth %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtper" runat="server" Width="40px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtcsps"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="11px" HorizontalAlign="Center" Width="40" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
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
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>

                    </div>
                </div>
            </div>

            <div class="modal fade" id="myModalColDet" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h2>
                                <asp:Label ID="lblModDayCol" runat="server"></asp:Label></h2>
                        </div>
                        <div class="modal-body">
                            <asp:Panel ID="Panel2" runat="server">

                                <asp:GridView ID="gvCompDayCol" runat="server" AutoGenerateColumns="False"
                                    Font-Size="10px" HorizontalAlign="Left" PageSize="5" ShowFooter="True"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNocs" runat="server" Font-Bold="True"
                                                    Style="text-align: right; font-size: 14px;"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"
                                                    CssClass="gridtext"></asp:Label>
                                            </ItemTemplate>
                                            <ControlStyle Width="20px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="20px" Font-Size="13" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutomer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblresdesc" runat="server" Width="200px" Style="font-size: 11px;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>

                                                <asp:Label runat="server" ID="lblyTtocs">Total</asp:Label>

                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Previous Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonAmtcs" runat="server" Width="80px" Style="text-align: right; font-size: 12px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "presamt")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblFPrevAmtcs"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtcs" runat="server" Width="80px" Style="text-align: right; font-size: 12px;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cursal")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtcs"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Growth">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtdif" runat="server" Width="80px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0;-#,##0; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryDiff"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="12px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="12px" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Growth %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMonCollamtper" runat="server" Width="40px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblypryCollamtcsps"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Size="11px" HorizontalAlign="Center" Width="40" />
                                            <ItemStyle HorizontalAlign="Right" Font-Size="11px" />
                                            <FooterStyle HorizontalAlign="Right" Font-Size="11px" Font-Bold="true" />
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
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(document).ready(function () {
           
        
        });
        function ExecuteMyGraph() {

            s1 = parseFloat($('#<%=this.s1.ClientID%>').val());
            s2 = parseFloat($('#<%=this.s2.ClientID%>').val());
            s3 = parseFloat($('#<%=this.s3.ClientID%>').val());
            s4 = parseFloat($('#<%=this.s4.ClientID%>').val());
            s5 = parseFloat($('#<%=this.s5.ClientID%>').val());
            s6 = parseFloat($('#<%=this.s6.ClientID%>').val());
            s7 = parseFloat($('#<%=this.s7.ClientID%>').val());
            s8 = parseFloat($('#<%=this.s8.ClientID%>').val());
            s9 = parseFloat($('#<%=this.s9.ClientID%>').val());
            s10 = parseFloat($('#<%=this.s10.ClientID%>').val());
            s11 = parseFloat($('#<%=this.s11.ClientID%>').val());
            s12 = parseFloat($('#<%=this.s12.ClientID%>').val());




            c1 = parseFloat($('#<%=this.c1.ClientID%>').val());
            c2 = parseFloat($('#<%=this.c2.ClientID%>').val());
            c3 = parseFloat($('#<%=this.c3.ClientID%>').val());
            c4 = parseFloat($('#<%=this.c4.ClientID%>').val());
            c5 = parseFloat($('#<%=this.c5.ClientID%>').val());
            c6 = parseFloat($('#<%=this.c6.ClientID%>').val());
            c7 = parseFloat($('#<%=this.c7.ClientID%>').val());
            c8 = parseFloat($('#<%=this.c8.ClientID%>').val());
            c9 = parseFloat($('#<%=this.c9.ClientID%>').val());
            c10 = parseFloat($('#<%=this.c10.ClientID%>').val());
            c11 = parseFloat($('#<%=this.c11.ClientID%>').val());
            c12 = parseFloat($('#<%=this.c12.ClientID%>').val());


            b1 = parseFloat($('#<%=this.b1.ClientID%>').val());
            b2 = parseFloat($('#<%=this.b2.ClientID%>').val());
            b3 = parseFloat($('#<%=this.b3.ClientID%>').val());
            b4 = parseFloat($('#<%=this.b4.ClientID%>').val());
            b5 = parseFloat($('#<%=this.b5.ClientID%>').val());
            b6 = parseFloat($('#<%=this.b6.ClientID%>').val());
            b7 = parseFloat($('#<%=this.b7.ClientID%>').val());
            b8 = parseFloat($('#<%=this.b8.ClientID%>').val());
            b9 = parseFloat($('#<%=this.b9.ClientID%>').val());
            b10 = parseFloat($('#<%=this.b10.ClientID%>').val());
            b11 = parseFloat($('#<%=this.b11.ClientID%>').val());
            b12 = parseFloat($('#<%=this.b12.ClientID%>').val());


            yc1 = parseFloat($('#<%=this.yc1.ClientID%>').val());
            yc2 = parseFloat($('#<%=this.yc2.ClientID%>').val());
            yc3 = parseFloat($('#<%=this.yc3.ClientID%>').val());

            ys1 = parseFloat($('#<%=this.ys1.ClientID%>').val());


            ys2 = parseFloat($('#<%=this.ys2.ClientID%>').val());
            ys3 = parseFloat($('#<%=this.ys3.ClientID%>').val());
            xaxis0 = parseFloat($('#<%=this.xaxis0.ClientID%>').val());
            xaxis1 = parseFloat($('#<%=this.xaxis1.ClientID%>').val());
            xaxis2 = parseFloat($('#<%=this.xaxis2.ClientID%>').val());

            funYearlyGraph();
            funMonthlyGraph();
            funMonthlyLineChart();
            funMonthlyPieChart();
            funMonthlyGraphArea();
            //mymapchart();

            //    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        }







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
                    text: 'Export'
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

                //    name: 'Realization',
                //    //color:red,
                //    data: [yc1],
                //    color: '#CA6621'
                //    }
                //]


                series: [{
                    name: "Export",
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
                    text: 'Realization'
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
                    name: "Realization",
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

        function funMonthlyGraph() {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });




            $('#barchart').highcharts({


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
                    name: 'Export',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }, {

                    name: 'Dues',
                    //color:red,
                    data: [b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12],
                    color: '#736B3A'
                }]
            });
            $('#barchart1').highcharts({


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
                    name: 'Export',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }, {

                    name: 'Dues',
                    //color:red,
                    data: [b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12],
                    color: '#736B3A'
                }]
            });
        }
        function funMonthlyGraphArea() {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });




            $('#archart').highcharts({


                chart: {
                    type: 'area'
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
                    name: 'Export',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }, {

                    name: 'Dues',
                    //color:red,
                    data: [b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12],
                    color: '#736B3A'
                }]
            });
            $('#archart1').highcharts({


                chart: {
                    type: 'area'
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
                    name: 'Export',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }, {

                    name: 'Dues',
                    //color:red,
                    data: [b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12],
                    color: '#736B3A'
                }]
            });
        }

        ///// monthly graph for LIne chart
        function funMonthlyLineChart() {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });




            $('#linechart').highcharts({


                chart: {
                    type: 'line'
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
                    name: 'Export',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }, {

                    name: 'Dues',
                    //color:red,
                    data: [b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12],
                    color: '#736B3A'
                }]
            });
            $('#linechart1').highcharts({


                chart: {
                    type: 'line'
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
                    name: 'Export',
                    data: [s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12],
                    color: '#1581C1'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12],
                    color: '#CA6621'
                }, {

                    name: 'Dues',
                    //color:red,
                    data: [b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12],
                    color: '#736B3A'
                }]
            });
        }
        function funMonthlyPieChart() {

            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });


            $('#piechart').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [
                       'Export',
                       'Realization',
                       'Dues'

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
                //    name: 'Export',
                //    data: [ys1],
                //    color: '#1581C1'

                //},
                //{

                //    name: 'Realization',
                //    //color:red,
                //    data: [yc1],
                //    color: '#CA6621'
                //    }
                //]


                series: [{
                    name: "Amount",
                    colorByPoint: true,
                    data: [{
                        name: 'Export',
                        y: s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12,
                        color: "#2E9ADA"
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: 'Realization',
                        y: c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + c10 + c11 + c12,
                        color: '#E37F3A'
                        //drilldown: null
                    },
                     {
                         name: 'Dues',
                         y: b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9 + b10 + b11 + b12,
                         color: '#8C8453'
                         //drilldown: null
                     }]
                }],



            });
            $('#piechart1').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: ''
                },
                xAxis: {
                    categories: [
                       'Export',
                       'Realization',
                       'Dues'

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
                //    name: 'Export',
                //    data: [ys1],
                //    color: '#1581C1'

                //},
                //{

                //    name: 'Realization',
                //    //color:red,
                //    data: [yc1],
                //    color: '#CA6621'
                //    }
                //]


                series: [{
                    name: "Amount",
                    colorByPoint: true,
                    data: [{
                        name: 'Export',
                        y: s1 + s2 + s3 + s4 + s5 + s6 + s7 + s8 + s9 + s10 + s11 + s12,
                        color: "#2E9ADA"
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: 'Realization',
                        y: c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9 + c10 + c11 + c12,
                        color: '#E37F3A'
                        //drilldown: null
                    },
                     {
                         name: 'Dues',
                         y: b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9 + b10 + b11 + b12,
                         color: '#8C8453'
                         //drilldown: null
                     }]
                }],



            });

        }


        //    function mymapchart() {
        //        var data = [
        //['bd-da', 4000],
        //['bd-kh', 5000],
        //['bd-ba', 3000],
        //['bd-cg', 2500],
        //['bd-sy', 1100],
        //['bd-rj', 1800],
        //['bd-rp', 4500]
        //        ];


        //        // Create the chart

        //            Highcharts.Map('MyMap', {
        //            chart: {
        //                map: 'countries/bd/bd-all'
        //            },

        //            title: {
        //                text: 'Division Wise Export'
        //            },

        //            subtitle: {
        //                text: '<a href="../Scripts/bd-all.js"></a>'
        //            },

        //            mapNavigation: {
        //                enabled: true,
        //                buttonOptions: {
        //                    verticalAlign: 'bottom'
        //                }
        //            },

        //            colorAxis: {
        //                min: 0
        //            },

        //            series: [{
        //                data: data,
        //                name: 'Random data',
        //                states: {
        //                    hover: {
        //                        color: '#BADA55'
        //                    }
        //                },
        //                dataLabels: {
        //                    enabled: true,
        //                    format: '{point.name}'
        //                }
        //            }]
        //        });
        //    }
    </script>
</asp:Content>

