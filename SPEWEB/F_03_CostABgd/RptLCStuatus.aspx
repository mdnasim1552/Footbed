<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptLCStuatus.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.RptLCStuatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highchartwithmap.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".multiselect ").addClass("btn-sm");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function openModal() {
            $('#myModal').modal('toggle');
        }

        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }

        function pageLoaded() {
            $(function () {
                $('[id*=ddlBuyer]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true
                })
            });

            var gv1 = $('#<%=this.gvOrderStatus.ClientID %>');
            gv1.Scrollable();

            var gv2 = $('#<%=this.gvProdStatus.ClientID %>');
            gv2.Scrollable();

            var gv3 = $('#<%=this.gvShipVal.ClientID %>');
            gv3.Scrollable();

            var gv4 = $('#<%=this.gvCatQuantity.ClientID %>');
            gv4.Scrollable();

            var gv5 = $('#<%=this.gvOrderStSummary.ClientID %>');
            gv5.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });

        }

        function OrderSummaryGraph(BuyerSum) {
            var BuyerSum = JSON.parse(BuyerSum);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });

            $('#PieOrdSummary').highcharts({
                chart: {
                    styledMode: true
                },
                title: {
                    text: 'Buyer wise Order Ratio %'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: (function () {
                        var data = [],
                            time = (new Date()).getTime(),
                            i;

                        for (var key in BuyerSum) {
                            if (BuyerSum.hasOwnProperty(key)) {
                                data.push([BuyerSum[key].buyername,
                                BuyerSum[key].ratio, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true
                }]
            });
        }

        function CategoryQtyGraph(Gents, Ladies, Kids) {
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });
            $('#PieCatQuantity').highcharts({
                chart: {
                    styledMode: true
                },

                title: {
                    text: 'Category Wise Order Quantity'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: [{
                        name: 'Gents',
                        y: parseInt(Gents)
                    }, {
                        name: 'Ladies',
                        y: parseInt(Ladies)
                    }, {
                        name: 'Kids',
                        y: parseInt(Kids)
                    }],
                    showInLegend: true
                }],
            });
        }

        function ShipmentValGraph(MonthWise) {
            var MonthWise = JSON.parse(MonthWise);
            console.log(MonthWise);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });
            $('#BarShipVal').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Ex-Factory Month Wise Shipment Value in $ USD',
                },
                xAxis: {
                    type: 'category',
                    crosshair: true,
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Price in $ USD'
                    }
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0,
                    }
                },

                series: [{
                    name: "Ex-Factory Date",
                    allowPointSelect: true,
                    colorByPoint: true,
                    data: (function () {
                        var data = [];
                        for (var key in MonthWise) {
                            if (MonthWise.hasOwnProperty(key)) {
                                data.push([MonthWise[key].exfactordate,
                                MonthWise[key].fcamtusd, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true
                }],
            });
        }

        function ExFactOrderGraph(MonthWise) {
            var MonthWise = JSON.parse(MonthWise);
            console.log(MonthWise);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });

            $('#BarExOrdQty').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Order Quantity As Per Ex-Factory Date',
                },
                xAxis: {
                    type: 'category',
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Order Quantity'
                    }
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0,
                    }
                },

                series: [{
                    allowPointSelect: true,
                    name: "Ex-Factory Date",
                    colorByPoint: true,
                    data: (function () {
                        var data = [];
                        for (var key in MonthWise) {
                            if (MonthWise.hasOwnProperty(key)) {
                                data.push([MonthWise[key].exfactordate,
                                MonthWise[key].totalqty, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true

                }],
            });
        }

    </script>

    <style type="text/css">
        b.caret {
            display: none !important;
        }

        ul.dropdown-menu {
            min-width: 15rem;
        }

        .multiselect-container {
            position: absolute;
            list-style-type: none;
            margin: 0;
            padding: 0;
            height: 300px;
            overflow-y: scroll;
            overflow-x: hidden;
        }

        .chkbox input {
            margin-right: 5px;
            margin-top: 4px;
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


            <div class="card card-fluid mb-1">
                <div class="card-body pt-2 pb-3">

                    <div class="row">

                        <div class="col-md-1">
                            <asp:Label runat="server" for="FromDate">From</asp:Label>
                            <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFrmDate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFrmDate"></asp:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:Label runat="server" for="ToDate">To</asp:Label>
                            <asp:TextBox ID="txtToDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtToDate"></asp:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="small" for="ToDate">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkbtnok" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-2 ">
                            <asp:Label runat="server" for="ToBuyername">Buyer Name</asp:Label>
                            <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px; height: 30px;">
                                <asp:ListBox ID="ddlBuyer" SelectionMode="Multiple" CssClass="form-control form-control-sm " runat="server"></asp:ListBox>
                            </div>
                        </div>

                        <div runat="server" id="FieldIsDetails" class="col-md-2" visible="true">
                            <asp:CheckBox runat="server" ID="chkDtls" Style="margin-top: 20px;" CssClass="form-control form-control-sm border-primary bg-light chkbox" Text="Print with Details" />
                        </div>


                        <div class="col-md-2">
                            <div class="form-group" id="RptType" runat="server">
                                <asp:Label runat="server" CssClass="label">Type:</asp:Label>
                                <asp:DropDownList ID="DdlRptType" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="DdlRptType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">Details Order Status</asp:ListItem>
                                    <asp:ListItem Value="1">Agent & Customer wise Order %</asp:ListItem>
                                    <asp:ListItem Value="2">Month wise Shipment Value</asp:ListItem>
                                    <asp:ListItem Value="3">Category wise Quantity</asp:ListItem>
                                    <asp:ListItem Value="4">Ex-Factory Order Quantity</asp:ListItem>
                                    <asp:ListItem Value="5">Quantity Per ETD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px">

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewPeriodicOrderStatus" runat="server">
                            <div>

                                <asp:Panel ID="PnlDetailsStat" runat="server" Visible="false">
                                    <div class="row">
                                        <asp:GridView ID="gvOrderStatus" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" Width="1175px" OnRowDataBound="gvOrderStatus_RowDataBound" HeaderStyle-VerticalAlign="Middle"
                                            OnSelectedIndexChanged="gvOrderStatus_SelectedIndexChanged">

                                            <Columns>

                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Buyer Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbuyername" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order Name">
                                                    <ItemTemplate>

                                                        <asp:HyperLink ID="lblgvlcOrder" Target="_blank" runat="server" AutoCompleteType="Disabled" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrdesc")) %>'
                                                            BackColor="Transparent" BorderStyle="None" Width="180px" ></asp:HyperLink>

                                                        <asp:Label ID="LblgvOrdNo" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrcod")) %>'></asp:Label>
                                                        <asp:Label ID="LblgvDayid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'></asp:Label>
                                                        <asp:Label ID="LblgvStyle" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'></asp:Label>
                                                        <asp:Label ID="Lblgvarchive" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "archive")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Style Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvStyledesc" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvorderdat" runat="server"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Order Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvorderType" runat="server"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrtype")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Text="Total"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvUnit" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvqtyftr" runat="server"
                                                            CssClass="text-right font-weight-bold"
                                                            Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rate(FC)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvfcrate" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount(FC)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvamountfc" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmtfc" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rate" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvrate" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvAmt" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Prod. Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvProdqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prodqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Challan Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvChallanqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chlnqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Packing<br/>Plan Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvpackqty" runat="server" BackColor="Transparent"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "packqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvpackSum" runat="server" Font-Bold="True" Font-Size="12px"
                                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Archive">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbtnArchive" runat="server" OnClick="LbtnArchive_Click" OnClientClick="return confirm('Do you want to Change Status?')" CssClass="btn btn-sm"><span class="fa fa-lock"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                            <FooterStyle CssClass="grvFooter" />
                                            <RowStyle CssClass="grvRows" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlOrdSummary" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-6">

                                            <asp:GridView ID="gvOrderStSummary" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" HeaderStyle-VerticalAlign="Middle" AllowSorting="true" OnSorting="gvOrderStSummary_Sorting">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSlNo" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Agent" SortExpression="agentdesc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAgent" runat="server" AutoCompleteType="Disabled" BackColor="Transparent" BorderStyle="None"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agentdesc")) %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Buyer Name" SortExpression="buyername">
                                                        <FooterTemplate>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Text="Total"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbuyer" runat="server" AutoCompleteType="Disabled" BackColor="Transparent" BorderStyle="None"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Qty (Pairs)" SortExpression="ordrqty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOrderqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvOrdersum" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="% Ratio" SortExpression="ratio">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRatio" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ratio")).ToString("#,##0.00;(#,##0.00); ")+" %" %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvRatioSum" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>

                                        </div>

                                        <div class="col-md-5" style="border: 1px solid #D8D8D8">
                                            <div id="PieOrdSummary" style="width: 600px; height: 450px; margin: 0 auto"></div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlCatQuantity" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-6">

                                            <asp:GridView ID="gvCatQuantity" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" HeaderStyle-VerticalAlign="Middle">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSlNo2" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Agent">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvAgent2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agentdesc")) %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Buyer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbuyer2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Gents">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvGentsqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gents")).ToString("#,##0;(#,##0); ") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvsum2" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ladies">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvLadiesqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ladies")).ToString("#,##0;(#,##0); ") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvsum3" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Kids">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvKidsqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "kids")).ToString("#,##0;(#,##0); ") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvsum4" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Order">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOrderqty2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvsum1" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>



                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>

                                        </div>

                                        <div class="col-md-5" style="border: 1px solid #D8D8D8">
                                            <div id="PieCatQuantity" style="width: 600px; height: 450px; margin: 0 auto"></div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlShipValue" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-6">

                                            <asp:GridView ID="gvShipVal" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" HeaderStyle-VerticalAlign="Middle">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSlNo3" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ex-Factory Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvexFact" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "exfactordate")) %>' Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Buyer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbuyer3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOrderqty3" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvsum6" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Price $ (USD)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvPriceUSD" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamtusd")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvsum7" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Price € (EUR)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvPriceEUR" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamteuro")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvsum8" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>



                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>

                                        </div>

                                        <div class="col-md-5" style="border: 1px solid #D8D8D8;">
                                            <div id="BarShipVal" style="width: 600px; height: 450px; margin: 0 auto"></div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlAgentOrder" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:GridView ID="gvAgentOrder" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" HeaderStyle-VerticalAlign="Middle">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSlNo4" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ex-Factory Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvexFact2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "exfactordate")) %>' Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvTtl0" runat="server" Text="Total" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <%--Agents Start--%>
                                                    <asp:TemplateField HeaderText="S01" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs1" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls1" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S02" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs2" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls2" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S03" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs3" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls3" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S04" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs4" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls4" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S05" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs5" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls5" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S06" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs6" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls6" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S07" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs7" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls7" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S08" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs8" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls8" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S09" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs9" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls9" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S10" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs10" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls10" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S11" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs11" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls11" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S12" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs12" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls12" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S13" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs13" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls13" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S14" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs14" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls14" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S15" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs15" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls15" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S16" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs16" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls16" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S17" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs17" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls17" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S18" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs18" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls18" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S19" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs19" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls19" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S20" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvs20" runat="server"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>' Width="50px">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbTtls20" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <%--Agents End--%>

                                                    <asp:TemplateField HeaderText="Total Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvTtlqty7" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvTtlQty1" runat="server" CssClass="text-right font-weight-bold"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>

                                        <div class="col-md-5" style="border: 1px solid #D8D8D8;">
                                            <div id="BarExOrdQty" style="width: 600px; height: 450px; margin: 0 auto"></div>
                                        </div>
                                    </div>
                                </asp:Panel>

                            </div>

                        </asp:View>

                        <asp:View ID="ViewPeriodicProductin" runat="server">
                            <%--<div class="row">--%>
                            <%--<div class="form-group">
                                    <div class="form-inline">--%>
                            <%--<div class="col-md-12">--%>
                            <div class="table-responsive ">

                                <asp:GridView ID="gvProdStatus" runat="server" AutoGenerateColumns="False" HeaderStyle-VerticalAlign="Middle" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product No">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="LbtngvProdNo" runat="server" OnClick="LbtngvProdNo_Click"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prdno")) %>'
                                                    Width="90px"></asp:LinkButton>

                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requistion No">
                                            <ItemTemplate>
                                                <asp:Label ID="LblReqno" runat="server"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Store Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStore" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "storename")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBuyer" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Master LC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcOrderPd" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Style Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyledescPd" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Text='<%#(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prddate")).Year==1900 ? "": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prddate")).ToString("dd-MMM-yyyy")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Label5" runat="server" ForeColor="#000" Text="Total" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqtyPd" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lgvproFqty" runat="server" Font-Bold="true"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate (FC)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfcratePd" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount (FC)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvamountfcPd" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFAmtfcPd" runat="server" Font-Bold="true"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyProdEdit" NavigateUrl='<%# this.ResolveUrl("~/F_15_Pro/ProdEntry?Type=Edit&actcode=" + Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrcod")) + "&genno="+Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) +"&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "prdno"))) %>' CssClass="btn btn-xs btn-default" runat="server"
                                                    Target="_blank" ForeColor="Black" Font-Underline="false">
                                                                        <span class="fa fa-edit"></span>
                                                </asp:HyperLink>

                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                            </div>
                            <%--</div>--%>
                            <%--<div class="col-md-1" style="margin-top: -350px">
                                        <asp:HyperLink runat="server" ID="lnkbtnExclDnld" Visible="false" CssClass="btn btn-sm btn-success">
                                        <i class="fa fa-file-excel"></i> Excel
                                        </asp:HyperLink>
                                    </div>--%>
                            <%--</div>
                                </div>--%>
                            <%--</div>--%>
                        </asp:View>

                    </asp:MultiView>

                </div>
            </div>


            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Production Wise Size Details Information </h4>
                            <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-arrow-left"></span></button>

                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12 table-responsive">

                                    <asp:GridView ID="gvProdDetails" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                        ShowFooter="True" HeaderStyle-VerticalAlign="Middle" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="DlblgvSlNo0" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Product No">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="DLbtngvProdNo" runat="server" OnClick="LbtngvProdNo_Click"
                                                        BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prdno")) %>'
                                                        Width="100px"></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Style Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="DlblgvStyle" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:Label ID="DlblgvlcOrderPd" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="DlblgvSize" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. No ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDprno" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="DLabel5" runat="server" ForeColor="#000" Text="Total" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="DlgvqtyPd" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="DlgvproFqty" runat="server" ForeColor="#000" Font-Bold="True"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Location ">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblLocation" runat="server" AutoCompleteType="Disabled"
                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locdesc")) %>'
                                                        Width="100px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" />
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
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>