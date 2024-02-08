<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptAllDashboard.aspx.cs" Inherits="SPEWEB.F_31_Mis.RptAllDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/highchartwithmap.js"></script>
    <%--<script src="../Scripts/map.js"></script>--%>
    <script src="../Scripts/highchartexporting.js"></script>
    <%--<script src="../Scripts/bd-all.js"></script>--%>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            // $("#OkBtn").trigger("click");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {
            // $("#OkBtn").click()
            //$(document).on("click", "#clear-cash-notes", function () {
            //    $(document).on("click", "#OkBtn", function () {
            //   $(document).on("click", "#OkBtn");
            // $("#OkBtn").trigger("click");
            //  alert("ssssfsf");
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

            <div class="card card-fluid mb-3">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblCurTransDate">Date</asp:Label>
                                <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px">
                            <asp:LinkButton ID="OkBtn" OnClick="OkBtn_Click" ClientIDMode="Static" CssClass="btn btn-sm btn-primary" runat="server">Ok</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            
            <div class="card card-fluid" runat="server" id="divInitialCard">
                <div class="card-body" style="min-height: 250px">
                    <div class="row">
                    </div>
                </div>
            </div>
            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="sales" runat="server">
                    <div class="row" style="display: none;">
                        <div class="col-md-6">
                            <span style="color: #44994a; font-weight: bold; font-size: 12px">Export Performance Today</span>
                            <div class="progress">
                                <div class="progress-bar progress-bar-striped active" role="progressbar"
                                    aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 60%">
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="col-md-6">
                                <button type="button" class="btn btn-danger">New Customer (This Month) <span class="badge custbadge">0</span></button>
                            </div>
                            <div class="col-md-6">
                                <button type="button" class="btn btn-success">New Retailer (This Month) <span class="badge retbadge">0</span></button>
                            </div>
                        </div>
                    </div>


                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="MonthlySales" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="TodaySales" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="weekchart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="piechart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="SalesChart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="CollChart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="Top5Customer" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="Top5Item" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="Top5Team" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="Top5Teamweek" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>
                <asp:View ID="purchase" runat="server">
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="monthpurchase" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="weekpurchase" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="purchart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="Top5Sup" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="top5mat" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="top5supout" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="top5suppay" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="purchasebal" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="Accounts" runat="server">
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="TodayReceive" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="TodayPayment" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="MonthRec" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="MonthPay" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="CurmonAcc" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="CurBal" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="balsheetchart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="accchart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="Production" runat="server">

                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="prodchart" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="Curmonprod" style="width: 100%; height: 250px;"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <div class="col-12">
                            <div class="d-flex justify-content-between">
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="MostItmProd" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>
                                <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                    <div class="card-body">
                                        <div id="Gainloss" style="width: 580px; height: 250px; margin: 0 auto"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="Marchandising" runat="server">

                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 250px">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="Monthlyrpt" style="width: 100%;"></div>
                                </div>
                            </div>
                            <div class="row mt-5">
                                <div class="col-md-12">
                                    <div id="Buyerwise" style="width: 100%;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">


        function ExecuteSalesGraph(salesdata, divwise, monsales, toplist) {
            console.log(JSON.parse(salesdata));
            var sdata = JSON.parse(salesdata);
            console.log(JSON.parse(divwise));
            var dwdata = JSON.parse(divwise);
            var data = JSON.parse(monsales);
            var topdata = JSON.parse(toplist);
            console.log(topdata);
            $(".progress-bar").css('width', 'sdata[9][tsaleamt]%');
            $(".progress-bar").attr({ title: "Sales Performance Today" });
            $(".progress-bar").html(sdata[9]['tsaleamt'] + " %");
            $(".custbadge").html(sdata[10]['totalcust']);
            $(".retbadge").html(sdata[10]['totalret']);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            $('#MonthlySales').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Export Realization',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Taka'
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
                            format: '{point.y:.1f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Monthly Export Realization",
                        "colorByPoint": true,
                        "data": [
                            //{
                            //    "name": "Target Export",
                            //    "y": sdata[0]['tsaleamt'],

                            //},
                            {
                                "name": "Actual Export",
                                "y": sdata[0]['msalttlamt'],

                            },
                            //{
                            //    "name": "Target Realization",
                            //    "y": sdata[0]['tcollamt'],

                            //},
                            {
                                "name": "Actual Realization",
                                "y": sdata[0]['monthcoll'],

                            }
                        ]
                    }
                ]
            });
            $('#TodaySales').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Today Export Realization',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Taka'
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
                            format: '{point.y:.1f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Today Export Realization",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Export",
                                "y": sdata[1]['tsaleamt'],
                                "color": "#f45342",


                            },
                            {
                                "name": "Realization",
                                "y": sdata[1]['tcollamt'],
                                "color": "#448e33",

                            }
                        ]
                    }
                ]
            });
            $('#weekchart').highcharts({


                chart: {
                    // type: 'line'
                    type: 'area'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Last 7 Days ',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        sdata[2]['wdays'],
                        sdata[3]['wdays'],
                        sdata[4]['wdays'],
                        sdata[5]['wdays'],
                        sdata[6]['wdays'],
                        sdata[7]['wdays'],
                        sdata[8]['wdays']

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
                    data: [sdata[2]['tsaleamt'], sdata[3]['tsaleamt'], sdata[4]['tsaleamt'], sdata[5]['tsaleamt'], sdata[6]['tsaleamt'], sdata[7]['tsaleamt'], sdata[8]['tsaleamt']],
                    color: '#1581C1'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [sdata[2]['tcollamt'], sdata[3]['tcollamt'], sdata[4]['tcollamt'], sdata[5]['tcollamt'], sdata[6]['tcollamt'], sdata[7]['tcollamt'], sdata[8]['tcollamt']],
                    color: '#CA6621'
                }]
            });
            $('#SalesChart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''

                },

                subtitle: {
                    text: 'Export TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
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
                    //,
                    //labels: {
                    //    format: '{value} crore'
                    //}

                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
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
                    name: 'Target',
                    data: [data[0]['targtsaleamtcore'], data[1]['targtsaleamtcore'], data[2]['targtsaleamtcore'], data[3]['targtsaleamtcore'], data[4]['targtsaleamtcore'], data[5]['targtsaleamtcore'], data[6]['targtsaleamtcore'], data[7]['targtsaleamtcore'], data[8]['targtsaleamtcore'], data[9]['targtsaleamtcore'], data[10]['targtsaleamtcore'], data[11]['targtsaleamtcore']],
                    color: '#1581C1'

                }, {

                    name: 'Actual',
                    //color:red,
                    data: [data[0]['ttlsalamtcore'], data[1]['ttlsalamtcore'], data[2]['ttlsalamtcore'], data[3]['ttlsalamtcore'], data[4]['ttlsalamtcore'], data[5]['ttlsalamtcore'], data[6]['ttlsalamtcore'], data[7]['ttlsalamtcore'], data[8]['ttlsalamtcore'], data[9]['ttlsalamtcore'], data[10]['ttlsalamtcore'], data[11]['ttlsalamtcore']],
                    color: '#CA6621'
                }]
            });
            $('#CollChart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Realization TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
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
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
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
                    name: 'Target',
                    data: [data[0]['tarcollamtcore'], data[1]['tarcollamtcore'], data[2]['tarcollamtcore'], data[3]['tarcollamtcore'], data[4]['tarcollamtcore'], data[5]['tarcollamtcore'], data[6]['tarcollamtcore'], data[7]['tarcollamtcore'], data[8]['tarcollamtcore'], data[9]['tarcollamtcore'], data[10]['tarcollamtcore'], data[11]['tarcollamtcore']],
                    color: '#42f47a'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [data[0]['collamtcrore'], data[1]['collamtcrore'], data[2]['collamtcrore'], data[3]['collamtcrore'], data[4]['collamtcrore'], data[5]['collamtcrore'], data[6]['collamtcrore'], data[7]['collamtcrore'], data[8]['collamtcrore'], data[9]['collamtcrore'], data[10]['collamtcrore'], data[11]['collamtcrore']],
                    color: '#454289'
                }]
            });
            $('#piechart').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Yearly (Dues)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
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
                        y: data[0]['ttlsalamt'] + data[1]['ttlsalamt'] + data[2]['ttlsalamt'] + data[3]['ttlsalamt'] + data[4]['ttlsalamt'] + data[5]['ttlsalamt'] + data[6]['ttlsalamt'] + data[7]['ttlsalamt'] + data[8]['ttlsalamt'] + data[9]['ttlsalamt'] + data[10]['ttlsalamt'] + data[11]['ttlsalamt'],
                        color: "#2E9ADA"
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: 'Realization',
                        y: data[0]['collamtcrore'] + data[1]['collamt'] + data[2]['collamt'] + data[3]['collamt'] + data[4]['collamt'] + data[5]['collamt'] + data[6]['collamt'] + data[7]['collamt'] + data[8]['collamt'] + data[9]['collamt'] + data[10]['collamt'] + data[11]['collamt'],
                        color: '#E37F3A'
                        //drilldown: null
                    },
                    {
                        name: 'Dues',
                        y: data[0]['bal'] + data[1]['bal'] + data[2]['bal'] + data[3]['bal'] + data[4]['bal'] + data[5]['bal'] + data[6]['bal'] + data[7]['bal'] + data[8]['bal'] + data[9]['bal'] + data[10]['bal'] + data[11]['bal'],
                        color: '#8C8453'
                        //drilldown: null
                    }]
                }],



            });
            $('#Top5Customer').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Top 5 Customers (In Month)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Top Customer",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[0]['sirdesc'],
                                "y": topdata[0]['itmamt'],

                            },
                            {
                                "name": topdata[1]['sirdesc'],
                                "y": topdata[1]['itmamt'],

                            },
                            {
                                "name": topdata[2]['sirdesc'],
                                "y": topdata[2]['itmamt'],

                            },
                            {
                                "name": topdata[3]['sirdesc'],
                                "y": topdata[3]['itmamt'],

                            },
                            {
                                "name": topdata[4]['sirdesc'],
                                "y": topdata[4]['itmamt'],

                            }
                        ]
                    }
                ]
            });
            $('#Top5Item').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Top 5 Products (In Month)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Quantity'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Balance Sheet",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[5]['sirdesc'],
                                "y": topdata[5]['itmqty'],

                            },
                            {
                                "name": topdata[6]['sirdesc'],
                                "y": topdata[6]['itmqty'],

                            },
                            {
                                "name": topdata[7]['sirdesc'],
                                "y": topdata[7]['itmqty'],

                            },
                            {
                                "name": topdata[8]['sirdesc'],
                                "y": topdata[8]['itmqty'],

                            },
                            {
                                "name": topdata[9]['sirdesc'],
                                "y": topdata[9]['itmqty'],

                            }
                        ]
                    }
                ]
            });
            $('#Top5Team').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Top 5 Team (In Month)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Top 5 Team",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[10]['sirdesc'],
                                "y": topdata[10]['itmamt'],

                            },
                            {
                                "name": topdata[11]['sirdesc'],
                                "y": topdata[11]['itmamt'],

                            },
                            {
                                "name": topdata[12]['sirdesc'],
                                "y": topdata[12]['itmamt'],

                            },
                            {
                                "name": topdata[13]['sirdesc'],
                                "y": topdata[13]['itmamt'],

                            },
                            {
                                "name": topdata[14]['sirdesc'],
                                "y": topdata[14]['itmamt'],

                            }
                        ]
                    }
                ]
            });
            $('#Top5Teamweek').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Last Week Team Performance',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Taka'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Last Week Team Performance",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[15]['sirdesc'],
                                "y": topdata[15]['itmamt'],
                                "color": "#4286f4",


                            },
                            {
                                "name": topdata[16]['sirdesc'],
                                "y": topdata[16]['itmamt'],
                                "color": "#f4a641",

                            },
                            {
                                "name": topdata[17]['sirdesc'],
                                "y": topdata[17]['itmamt'],
                                "color": "#f44141",

                            },
                            {
                                "name": topdata[18]['sirdesc'],
                                "y": topdata[18]['itmamt'],
                                "color": "#4faf59",

                            },
                            {
                                "name": topdata[19]['sirdesc'],
                                "y": topdata[19]['itmamt'],
                                "color": "#89a9c1",

                            }
                        ]
                    }
                ]
            });
            //     var data = [
            //[dwdata[0]['srtname'], dwdata[0]['tsaleamt']],
            //[dwdata[1]['srtname'], dwdata[1]['tsaleamt']],
            //[dwdata[2]['srtname'], dwdata[2]['tsaleamt']],
            //[dwdata[3]['srtname'], dwdata[3]['tsaleamt']],
            //[dwdata[4]['srtname'], dwdata[4]['tsaleamt']],
            //[dwdata[5]['srtname'], dwdata[5]['tsaleamt']],
            //[dwdata[6]['srtname'], dwdata[6]['tsaleamt']]
            //     ];
            //Highcharts.Map('MyMap', {
            //    chart: {
            //        map: 'countries/bd/bd-all'
            //    },

            //    title: {
            //        text: 'Division Wise Export'
            //    },

            //    subtitle: {
            //        text: '<a href="../Scripts/bd-all.js"></a>'
            //    },

            //    mapNavigation: {
            //        enabled: true,
            //        buttonOptions: {
            //            verticalAlign: 'bottom'
            //        }
            //    },

            //    colorAxis: {
            //        min: 0
            //    },

            //    series: [{
            //        data: data,
            //        name: 'Random data',
            //        states: {
            //            hover: {
            //                color: '#BADA55'
            //            }
            //        },
            //        dataLabels: {
            //            enabled: true,
            //            format: '{point.name}'
            //        }
            //    }]
            //});
        }
        function ExecutePurchaseGraph(purdata, topdata) {
            console.log(JSON.parse(purdata));
            var purdata = JSON.parse(purdata)
            var topdata = JSON.parse(topdata)
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            $('#purchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Procurement TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
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
                    name: 'Purchase',
                    data: [purdata[0]['ttlsalamtcore'], purdata[1]['ttlsalamtcore'], purdata[2]['ttlsalamtcore'], purdata[3]['ttlsalamtcore'], purdata[4]['ttlsalamtcore'], purdata[5]['ttlsalamtcore'], purdata[6]['ttlsalamtcore'], purdata[7]['ttlsalamtcore'], purdata[8]['ttlsalamtcore'], purdata[9]['ttlsalamtcore'], purdata[10]['ttlsalamtcore'], purdata[11]['ttlsalamtcore']],
                    color: '#4286f4'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [purdata[0]['tpayamtcore'], purdata[1]['tpayamtcore'], purdata[2]['tpayamtcore'], purdata[3]['tpayamtcore'], purdata[4]['tpayamtcore'], purdata[5]['tpayamtcore'], purdata[6]['tpayamtcore'], purdata[7]['tpayamtcore'], purdata[8]['tpayamtcore'], purdata[9]['tpayamtcore'], purdata[10]['tpayamtcore'], purdata[11]['tpayamtcore']],
                    color: '#f44941'
                }]
            });
            $('#monthpurchase').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Purchase Payment (Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Taka'
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
                        "name": "Monthly Purchase Payment",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Purchase",
                                "y": purdata[12]['ttlsalamtcore'],
                                "color": "#f45342",


                            },
                            {
                                "name": "Payment",
                                "y": purdata[12]['tpayamtcore'],
                                "color": "#448e33",

                            }
                        ]
                    }
                ]
            });
            $('#weekpurchase').highcharts({


                chart: {
                    // type: 'line'
                    type: 'area'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Last 7 Days Purchase ',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        purdata[13]['yearmon'],
                        purdata[14]['yearmon'],
                        purdata[15]['yearmon'],
                        purdata[16]['yearmon'],
                        purdata[17]['yearmon'],
                        purdata[18]['yearmon'],
                        purdata[19]['yearmon']

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
                    name: 'Purchase',
                    data: [purdata[13]['ttlsalamt'], purdata[14]['ttlsalamt'], purdata[15]['ttlsalamt'], purdata[16]['ttlsalamt'], purdata[17]['ttlsalamt'], purdata[18]['ttlsalamt'], purdata[19]['ttlsalamt']],
                    color: '#1581C1'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [purdata[13]['tpayamt'], purdata[14]['tpayamt'], purdata[15]['tpayamt'], purdata[16]['tpayamt'], purdata[17]['tpayamt'], purdata[18]['tpayamt'], purdata[19]['tpayamt']],
                    color: '#CA6621'
                }]
            });
            $('#Top5Sup').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Top 5 Supplier (In Month)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.1f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Top Supplier",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[0]['sirdesc'],
                                "y": topdata[0]['itmamt'],

                            },
                            {
                                "name": topdata[1]['sirdesc'],
                                "y": topdata[1]['itmamt'],

                            },
                            {
                                "name": topdata[2]['sirdesc'],
                                "y": topdata[2]['itmamt'],

                            },
                            {
                                "name": topdata[3]['sirdesc'],
                                "y": topdata[3]['itmamt'],

                            },
                            {
                                "name": topdata[4]['sirdesc'],
                                "y": topdata[4]['itmamt'],

                            }
                        ]
                    }
                ]
            });
            $('#top5mat').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Top 5 Materials (In Month)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Quantity'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> <br/>'
                },

                "series": [
                    {
                        "name": "Top Materials",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[5]['sirdesc'],
                                "y": topdata[5]['itmqty'],

                            },
                            {
                                "name": topdata[6]['sirdesc'],
                                "y": topdata[6]['itmqty'],

                            },
                            {
                                "name": topdata[7]['sirdesc'],
                                "y": topdata[7]['itmqty'],

                            },
                            {
                                "name": topdata[8]['sirdesc'],
                                "y": topdata[8]['itmqty'],

                            },
                            {
                                "name": topdata[9]['sirdesc'],
                                "y": topdata[9]['itmqty'],

                            }
                        ]
                    }
                ]
            });
            $('#top5supout').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Top 5 Supplier Outstanding (In Month)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> <br/>'
                },

                "series": [
                    {
                        "name": "Top Supplier Outstanding",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[10]['sirdesc'],
                                "y": topdata[10]['itmamt'],

                            },
                            {
                                "name": topdata[11]['sirdesc'],
                                "y": topdata[11]['itmamt'],

                            },
                            {
                                "name": topdata[12]['sirdesc'],
                                "y": topdata[12]['itmamt'],

                            },
                            {
                                "name": topdata[13]['sirdesc'],
                                "y": topdata[13]['itmamt'],

                            },
                            {
                                "name": topdata[14]['sirdesc'],
                                "y": topdata[14]['itmamt'],

                            }
                        ]
                    }
                ]
            });
            $('#top5suppay').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Top 5 Supplier Payment (In Month)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> <br/>'
                },

                "series": [
                    {
                        "name": "Top Supplier Payment",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": topdata[15]['sirdesc'],
                                "y": topdata[15]['itmamt'],

                            },
                            {
                                "name": topdata[16]['sirdesc'],
                                "y": topdata[16]['itmamt'],

                            },
                            {
                                "name": topdata[17]['sirdesc'],
                                "y": topdata[17]['itmamt'],

                            },
                            {
                                "name": topdata[18]['sirdesc'],
                                "y": topdata[18]['itmamt'],

                            },
                            {
                                "name": topdata[19]['sirdesc'],
                                "y": topdata[19]['itmamt'],

                            }
                        ]
                    }
                ]
            });
            $('#purchasebal').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Yearly Purchase (Balance)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Purchase',
                        'Payment',
                        'Balance'

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
                        name: 'Purchase',
                        y: purdata[0]['ttlsalamtcore'] + purdata[1]['ttlsalamtcore'] + purdata[2]['ttlsalamtcore'] + purdata[3]['ttlsalamtcore'] + purdata[4]['ttlsalamtcore'] + purdata[5]['ttlsalamtcore'] + purdata[6]['ttlsalamtcore'] + purdata[7]['ttlsalamtcore'] + purdata[8]['ttlsalamtcore'] + purdata[9]['ttlsalamtcore'] + purdata[10]['ttlsalamtcore'] + purdata[11]['ttlsalamtcore'],
                        color: "#2E9ADA"
                        //drilldown: 'Microsoft Internet Explorer'
                    }, {
                        name: 'Payment',
                        y: purdata[0]['tpayamtcore'] + purdata[1]['tpayamtcore'] + purdata[2]['tpayamtcore'] + purdata[3]['tpayamtcore'] + purdata[4]['tpayamtcore'] + purdata[5]['tpayamtcore'] + purdata[6]['tpayamtcore'] + purdata[7]['tpayamtcore'] + purdata[8]['tpayamtcore'] + purdata[9]['tpayamtcore'] + purdata[10]['tpayamtcore'] + purdata[11]['tpayamtcore'],
                        color: '#E37F3A'
                        //drilldown: null
                    },
                    {
                        name: 'Balance',
                        y: ((purdata[0]['ttlsalamtcore'] + purdata[1]['ttlsalamtcore'] + purdata[2]['ttlsalamtcore'] + purdata[3]['ttlsalamtcore'] + purdata[4]['ttlsalamtcore'] + purdata[5]['ttlsalamtcore'] + purdata[6]['ttlsalamtcore'] + purdata[7]['ttlsalamtcore'] + purdata[8]['ttlsalamtcore'] + purdata[9]['ttlsalamtcore'] + purdata[10]['ttlsalamtcore'] + purdata[11]['ttlsalamtcore']) - (purdata[0]['tpayamtcore'] + purdata[1]['tpayamtcore'] + purdata[2]['tpayamtcore'] + purdata[3]['tpayamtcore'] + purdata[4]['tpayamtcore'] + purdata[5]['tpayamtcore'] + purdata[6]['tpayamtcore'] + purdata[7]['tpayamtcore'] + purdata[8]['tpayamtcore'] + purdata[9]['tpayamtcore'] + purdata[10]['tpayamtcore'] + purdata[11]['tpayamtcore'])),
                        color: '#8C8453'
                        //drilldown: null
                    }]
                }],



            });
        }

        function ExecuteAccGraph(balshet, monthacc, curbal1, todayrecv, todaypay, monthrec, monthpay) {
            //   console.log(JSON.parse(curbal1));
            var balshet_date = JSON.parse(balshet);
            var monthacc = JSON.parse(monthacc);
            var curbal = JSON.parse(curbal1);
            var todayrecv = JSON.parse(todayrecv);
            var todaypay = JSON.parse(todaypay);
            var monthrec = JSON.parse(monthrec);
            var monthpay = JSON.parse(monthpay);
            //    console.log(monthpay);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });
            $('#balsheetchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Balance Sheet (%)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Parcentage'
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
                            format: '{point.y:.0f}%'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Balance Sheet",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Non-Current Asset",
                                "y": balshet_date[0]['noncuram'],

                            },
                            {
                                "name": "Current Asset",
                                "y": balshet_date[0]['curam'],

                            },
                            {
                                "name": "Equity",
                                "y": balshet_date[0]['equityam'],

                            },
                            {
                                "name": "Non-Current Liabilities",
                                "y": balshet_date[0]['noncurlia'],

                            },
                            {
                                "name": "Current Liabilities",
                                "y": balshet_date[0]['curlia'],

                            }
                        ]
                    }
                ]
            });
            $('#accchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Accounts TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
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
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
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
                    name: 'Recipt',
                    data: [monthacc[0]['cramcore'], monthacc[1]['cramcore'], monthacc[2]['cramcore'], monthacc[3]['cramcore'], monthacc[4]['cramcore'], monthacc[5]['cramcore'], monthacc[6]['cramcore'], monthacc[7]['cramcore'], monthacc[8]['cramcore'], monthacc[9]['cramcore'], monthacc[10]['cramcore'], monthacc[11]['cramcore']],
                    color: '#138225'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [monthacc[0]['dramcore'], monthacc[1]['dramcore'], monthacc[2]['dramcore'], monthacc[3]['dramcore'], monthacc[4]['dramcore'], monthacc[5]['dramcore'], monthacc[6]['dramcore'], monthacc[7]['dramcore'], monthacc[8]['dramcore'], monthacc[9]['dramcore'], monthacc[10]['dramcore'], monthacc[11]['dramcore']],
                    color: '#aa1811'
                }]
            });

            $('#CurmonAcc').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month Account',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Tk (Lakh)'
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
                        "name": "Current Month Account",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Receipt",
                                "y": monthacc[12]['cramcore'],
                                "color": "#ba8639",


                            },
                            {
                                "name": "Payment",
                                "y": monthacc[12]['dramcore'],
                                "color": "#72bc60",

                            }
                        ]
                    }
                ]
            });

            $('#CurBal').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Overall Balance',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Overall Balance",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": curbal[0]['cactdesc'],
                                "y": curbal[0]['trnam'],

                            },
                            {
                                "name": curbal[1]['cactdesc'],
                                "y": curbal[1]['trnam'],

                            },
                            {
                                "name": curbal[2]['cactdesc'],
                                "y": curbal[2]['trnam'],

                            },
                            {
                                "name": curbal[3]['cactdesc'],
                                "y": curbal[3]['trnam'],

                            },
                            {
                                "name": curbal[4]['cactdesc'],
                                "y": curbal[4]['trnam'],

                            }
                        ]
                    }
                ]
            });

            $('#TodayReceive').highcharts({
                chart: {
                    styledMode: true
                },

                title: {
                    text: 'Today Received'
                },

                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: (function () {
                        // generate an array of random data
                        var data = [],
                            time = (new Date()).getTime(),
                            i;

                        for (var key in todayrecv) {
                            if (todayrecv.hasOwnProperty(key)) {
                                data.push([todayrecv[key].cactdesc,
                                todayrecv[key].trnam, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true
                }]
            });
            $('#TodayPayment').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    text: 'Today <br>Payment<br>',
                    align: 'center',
                    verticalAlign: 'middle',
                    y: 40
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}</b>'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'white'
                            }
                        },
                        startAngle: -90,
                        endAngle: 90,
                        center: ['50%', '75%'],
                        size: '160%'
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Today Payment',
                    innerSize: '50%',
                    data: (function () {
                        // generate an array of random data
                        var data = [];

                        for (var key in todaypay) {
                            if (todaypay.hasOwnProperty(key)) {
                                data.push([todaypay[key].cactdesc,
                                todaypay[key].trnam
                                ]);
                            }
                        }
                        return data;
                    }()),
                }]
            });
            $('#MonthRec').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Top Receive Head',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Monthly Top Receive Head",
                        "colorByPoint": true,
                        "data": (function () {
                            // generate an array of random data
                            var data = [];

                            for (var key in monthrec) {
                                if (monthrec.hasOwnProperty(key)) {
                                    data.push({
                                        "name": monthrec[key].cactdesc,
                                        "y": monthrec[key].trnam,
                                    });
                                }
                            }
                            return data;
                        }())
                    }
                ]
            });
            $('#MonthPay').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Top Payment Head',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In Amount'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Monthly Top Payment Head",
                        "colorByPoint": true,
                        "data": (function () {
                            // generate an array of random data
                            var data = [];

                            for (var key in monthpay) {
                                if (monthpay.hasOwnProperty(key)) {
                                    data.push({
                                        "name": monthpay[key].cactdesc,
                                        "y": monthpay[key].trnam,
                                    });
                                }
                            }
                            return data;

                        }())

                    }
                ]

            });
        }

        function ExecuteProductionGrpah(yearprod, mostitm_json, gainloss) {
            //   console.log(JSON.parse(balshet));
            var yearprod = JSON.parse(yearprod);
            var mostitm = JSON.parse(mostitm_json);
            var gainloss = JSON.parse(gainloss);
            console.log(mostitm);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            $('#prodchart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Production TK(Lakh)',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
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
                        '<td style="padding:0"><b>{point.y:.2f}</b></td></tr>',
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
                    name: 'Target',
                    data: [yearprod[0]['bgdamt'], yearprod[1]['bgdamt'], yearprod[2]['bgdamt'], yearprod[3]['bgdamt'], yearprod[4]['bgdamt'], yearprod[5]['bgdamt'], yearprod[6]['bgdamt'], yearprod[7]['bgdamt'], yearprod[8]['bgdamt'], yearprod[9]['bgdamt'], yearprod[10]['bgdamt'], yearprod[11]['bgdamt']],
                    color: '#96780a'

                }, {

                    name: 'Excution',
                    //color:red,
                        data: [yearprod[0]['proamt'], yearprod[1]['proamt'], yearprod[2]['proamt'], yearprod[3]['proamt'], yearprod[4]['proamt'], yearprod[5]['proamt'], yearprod[6]['proamt'], yearprod[7]['proamt'], yearprod[8]['proamt'], yearprod[9]['proamt'], yearprod[10]['proamt'], yearprod[11]['proamt']],
                    color: '#990c4b'
                }]
            });
            $('#Curmonprod').highcharts({


                chart: {
                    type: 'bar'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Current Month Production',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'Amount in Tk (Lakh)'
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
                        "name": "Current Month Production",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Target",
                                "y": yearprod[12]['bgdamt'],
                                "color": "#515791",


                            },
                            {
                                "name": "Execution",
                                "y": yearprod[12]['proamt'],
                                "color": "#ef5677",

                            }
                        ]
                    }
                ]
            });
            $('#MostItmProd').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Most Production Item',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In QTY'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Most Production Item",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": mostitm[0]['itmdesc'],
                                "y": mostitm[0]['proqty'],


                            },
                            {
                                "name": mostitm[1]['itmdesc'],
                                "y": mostitm[1]['proqty'],

                            },
                            {
                                "name": mostitm[2]['itmdesc'],
                                "y": mostitm[2]['proqty'],

                            },
                            {
                                "name": mostitm[3]['itmdesc'],
                                "y": mostitm[3]['proqty'],

                            },
                            {
                                "name": mostitm[4]['itmdesc'],
                                "y": mostitm[4]['proqty'],

                            }
                        ]
                    }
                ]
            });
            $('#Gainloss').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Production Gain Loss',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    type: 'category'
                },
                yAxis: {
                    title: {
                        text: 'In QTY'
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
                            format: '{point.y:.0f}'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Production Gain Loss",
                        "colorByPoint": true,
                        "data": [
                            {
                                "name": "Total Production",
                                "y": gainloss[0]['proqty'],


                            },
                            {
                                "name": "Total QC",
                                "y": gainloss[0]['ttlqcqty'],

                            },
                            {
                                "name": "Total Gain",
                                "y": gainloss[0]['qcqty'],

                            },
                            {
                                "name": "Total Loss",
                                "y": gainloss[0]['rejqty'],

                            }
                            //{
                            //    "name": "Gain (%)",
                            //    "y": (gainloss[0]['qcqty'] * 100) / gainloss[0]['ttlqcqty'],

                            //}
                        ]
                    }
                ]
            });
        }
        function ExecuteMerchandGraph(monthwise) {
            console.log(monthwise);
            var monthwise = JSON.parse(monthwise);
            var monthseries = [];
            var monsmple = [];
            var monordr = [];
            var monreordr = [];
            $.each(monthwise, function (i, item) {
                monthseries.push(item.mondays);
                monsmple.push(item.smple);
                monordr.push(item.orderqty);
                monreordr.push(item.reordrqty);
            }),
                Highcharts.setOptions({
                    lang: {
                        decimalPoint: '.',
                        thousandsSep: ' '
                    }
                });

            $('#Monthlyrpt').highcharts({


                chart: {
                    // type: 'line'
                    type: 'line'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Monthly Merchandising Statistics',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories:
                        monthseries


                    ,
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Count'
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
                    name: 'Inquery',
                    data: monsmple,
                    color: '#1581C1'

                }, {

                    name: 'Order',
                    //color:red,
                    data: monordr,
                    color: '#CA6621'
                },
                {

                    name: 'Re-Order',
                    //color:red,
                    data: monreordr,
                    color: '#111111'
                }]
            });


        }
       // ExecuteMerchandGraph('[{"comcod":"5305","mondays":"01-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"02-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"03-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"04-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"05-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"06-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"07-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"08-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"09-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"10-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"11-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"12-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"13-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"14-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"15-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"16-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"17-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"18-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"19-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"20-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"21-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"22-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"23-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"24-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"25-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"26-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"27-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"28-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"29-Apr-2023","smple":0,"orderqty":0,"reordrqty":0},{"comcod":"5305","mondays":"30-Apr-2023","smple":0,"orderqty":0,"reordrqty":0}]')
        //function Test() {
        //    console.log("I m In");
        //}

    </script>


</asp:Content>
