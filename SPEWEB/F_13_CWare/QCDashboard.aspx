<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="QCDashboard.aspx.cs" Inherits="SPEWEB.F_13_CWare.QCDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/highchartwithmap.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>
    <script language="javascript" type="text/javascript">

        function PieChartIQC(QcP1, QcP2, QcFail, Pending) {
            console.log(QcP1, QcP2, QcFail, Pending);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });
            $('#PieChartIQC').highcharts({
                chart: {
                    styledMode: true
                },

                title: {
                    text: ''
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '{point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: [{
                        name: 'QC Passed (1st Time)',
                        y: parseInt(QcP1)
                    }, {
                        name: 'QC Passed (2nd Time)',
                        y: parseInt(QcP2)
                    }, {
                        name: 'QC Failed',
                        y: parseInt(QcFail)
                    }, {
                        name: 'Pending',
                        y: parseInt(Pending)
                    }],
                    showInLegend: true
                }],
            });
        }

        function BarBestSupplier(bestSupplier) {
            var bestSupplier = JSON.parse(bestSupplier);
            console.log(bestSupplier);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });

            $('#BarBestSupplier').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: '',
                },
                xAxis: {
                    type: 'category',
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Quantity'
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
                    name: "Top Best Suppliers",
                    colorByPoint: true,
                    data: (function () {
                        var data = [];
                        for (var key in bestSupplier) {
                            if (bestSupplier.hasOwnProperty(key)) {
                                data.push([bestSupplier[key].ssirdesc,
                                bestSupplier[key].passqty, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true

                }],
            });
        }

        function BarWorstSupplier(worstSupplier) {
            var worstSupplier = JSON.parse(worstSupplier);
            console.log(worstSupplier);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });

            $('#BarWorstSupplier').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: '',
                },
                xAxis: {
                    type: 'category',
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Quantity'
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
                    name: "Top Failure Suppliers",
                    colorByPoint: true,
                    data: (function () {
                        var data = [];
                        for (var key in worstSupplier) {
                            if (worstSupplier.hasOwnProperty(key)) {
                                data.push([worstSupplier[key].ssirdesc,
                                    worstSupplier[key].passqty, false
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

            <div class="card card-fluid mb-1 mt-0">
                <div class="card-body pt-1 pb-0">
                    <div class="row">

                        <div class="col-1">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server">From</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server">To</asp:Label>
                                <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateto_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click" ClientIDMode="Static">Ok</asp:LinkButton>
                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="margin-top20px btn-group btn-group-sm" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger btn-sm">Shortcut</button>
                                <div class="btn-group btn-group-sm" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger btn-sm dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Defect Pareto Chart</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" CssClass="dropdown-item">Order-Defect Reject/Repair Report</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" CssClass="dropdown-item">IQC Inspection Report</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" CssClass="dropdown-item">Finish Goods Inspection Data Entry</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 550px;">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6 form-group" runat="server" id="BestSupplier" style="border: 1px solid #D8D8D8" visible="false">
                            <div id="BarBestSupplier" style="width: 700px; height: 400px; margin: 0 auto"></div>
                        </div>
                        <div class="col-md-6 form-group" runat="server" id="WorstSupplier" style="border: 1px solid #D8D8D8" visible="false">
                            <div id="BarWorstSupplier" style="width: 700px; height: 400px; margin: 0 auto"></div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6 form-group" runat="server" id="PnIQCStatus" style="border: 1px solid #D8D8D8" visible="false">
                            <div id="IQCStat" style="width: 400px; height: 300px; margin: 0 auto">
                                <br />
                                <h5>Quality Dashboard Counter</h5><br />
                                <table>
                                    <tr>
                                        <th>Total Received</th>
                                        <td><asp:Label ID="lblTotalRcv" runat="server" style="margin-left:20px" CssClass="btn btn-success text-right" Width="120px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>QC Passed (1st Time)</th>
                                        <td><asp:Label ID="lblQpassed1" runat="server" style="margin-left:20px" CssClass="btn btn-primary text-right" Width="120px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>QC Passed (2nd Time)</th>
                                        <td><asp:Label ID="lblQpassed2" runat="server" style="margin-left:20px" CssClass="btn btn-info text-right" Width="120px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>QC Failed</th>
                                        <td><asp:Label ID="lblFailed" runat="server" style="margin-left:20px" CssClass="btn btn-danger text-right" Width="120px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>Pending</th>
                                        <td><asp:Label ID="lblPending" runat="server" style="margin-left:20px" CssClass="btn btn-warning text-right" Width="120px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <th>Lead Time</th>
                                        <td><asp:Label ID="lblLdTime" runat="server" style="margin-left:20px" CssClass="btn btn-dark text-right" Width="120px"></asp:Label></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-6 form-group" runat="server" id="PieIQCStat" style="border: 1px solid #D8D8D8" visible="false">
                            <div id="PieChartIQC" style="width: 700px; height: 400px; margin: 0 auto"></div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
