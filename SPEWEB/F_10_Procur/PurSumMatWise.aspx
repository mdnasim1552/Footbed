﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurSumMatWise.aspx.cs" Inherits="SPEWEB.F_10_Procur.PurSumMatWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highchart2.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            <%--//var comcod = <%=this.GetComeCode()%>;--%>
            $("#hlnkprjgrpcode").attr("href", "RptPurchaseStatus.aspx?Type=Purchase&Rpt=PurSum");
            //$("#hlnkdaypur").attr("href", "F_14_Pro/RptPurchaseStatus.aspx?Type=Purchase&comcod='"+comcod+"'&Rpt=DaywPur");
            $("#hlnkdaypur").attr("href", "F_09_Commer/RptPurchaseStatus.aspx?Type=Purchase&Rpt=DaywPur");
        }

        function ExecuteGraph(bgd, alldata, mainhead) {


            var alldata = bgd;
            console.log(alldata);
            var bgddata = JSON.parse(bgd);
            var mainhead = JSON.parse(mainhead);


            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            var armainhead = [];
            for (var i = 0; i < mainhead.length; i++) {
                armainhead[i] = mainhead[i]["rptdesc"];
            }

            $('#pursummary').highcharts({
                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Purchase Summary (MaterialWise)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },


                xAxis: {
                    type: 'category',
                    labels: {
                        formatter: function () {
                            if ($.inArray(this.value, armainhead) !== -1) {
                                return '<span style="fill: maroon;">' + this.value + '</span>';
                            } else {
                                return this.value;
                            }
                        },
                        style: {
                            color: '#000',

                        }
                    }
                },


                yAxis: {
                    title: {
                        text: 'Taka In Lac'
                    }

                },
                legend: {
                    enabled: false
                },
                plotOptions: {
                    series: {
                        borderWidth: 0,
                        dataLabels: {
                            enabled: true,
                            format: '{point.y:.2f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "",
                        "colorByPoint": true,
                        "data":
                            (function () {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in bgddata) {
                                    if (bgddata.hasOwnProperty(key)) {
                                        data.push([bgddata[key].rptdesc,
                                        bgddata[key].amt, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                    }
                ]
            });

        }

    </script>





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
                            <asp:Label runat="server" class="" for="FromDate">From</asp:Label>
                            <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender_txtFDate" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:Label runat="server" class="" for="ToDate">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1_txttodate" runat="server" Enabled="True"
                                Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-1">
                            <div class="btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-sm btn-danger" Style="margin-top: 20px;">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" Style="margin-top: 20px;" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="hlnkprjgrpcode" runat="server" Target="_blank" ClientIDMode="Static" CssClass="dropdown-item">Purchase Summary (Project Wise)</asp:HyperLink>
                                        <asp:HyperLink ID="hlnkdaypur" runat="server" Target="_blank" CssClass="dropdown-item" ClientIDMode="Static">Day Wise Purchase</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-6">
                            <div class="col-md-12">
                                <asp:GridView ID="gvPurmatwise" runat="server" CssClass=" table-condensed table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="475px"
                                    OnRowDataBound="gvPurmatwise_OnRowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1gp" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkgvWDescgp" runat="server" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptdesc")) %>' Width="165px" OnClick="lnkgvWDescgp_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <FooterTemplate>
                                                <asp:Label ID="lgvtotal" runat="server" Style="text-align: right" Text='Total'></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvunit" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rptunit"))%>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBudgetAmtgp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotal" runat="server" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Percent(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpercnt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "peramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtotalper" runat="server" Style="text-align: right" Text="100%"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />

                                </asp:GridView>
                            </div>
                            <div runat="server" id="abppanel" class="col-md-12" style="margin-top: 50px; display: none;" visible="False">
                                <asp:HyperLink CssClass="col-md-4 col-md-offset-4 btn btn-sm btn-success" Style="float: left" Font-Size="13" NavigateUrl="~/F_32_Mis/PrjDirectCost.aspx" Target="_blank" runat="server">ABP Amount</asp:HyperLink>
                                <asp:Label runat="server" ID="abpamt" Style="color: maroon; margin-left: 10px; margin-top: 10px; font-weight: bold; float: left" Font-Size="13"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div id="pursummary" style="width: 500px; height: 500px; margin: 0 auto"></div>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

