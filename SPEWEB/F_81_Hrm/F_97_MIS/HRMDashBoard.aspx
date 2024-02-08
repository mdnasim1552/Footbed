<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HRMDashBoard.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_97_MIS.HRMDashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../../Scripts/highcharts.js"></script>
    <script src="../../Scripts/highchartexporting.js"></script>
    <script src="../../Scripts/GoogleChartNew.js"></script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            document.getElementById('<%= lbtnOk.ClientID %>').click();
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            //funEmpStatusGraph();

            google.charts.load("current", { packages: ["corechart"] });
            GetAttnPieChart();
        }
        function GetAttnPieChart() {
            google.charts.setOnLoadCallback(attnPieChart());
        }
        function attnPieChart() {
            var yprsnt = parseFloat($('#<%=this.yprsnt.ClientID%>').val());
            var yabsnt = parseFloat($('#<%=this.yabsnt.ClientID%>').val());
            var yleave = parseFloat($('#<%=this.yleave.ClientID%>').val());
            var yhlday = parseFloat($('#<%=this.yhlday.ClientID%>').val());
            var data = google.visualization.arrayToDataTable([
                ['Task', 'Today Attendance Status'],
                ['Present', yprsnt],
                ['Absent', yabsnt],
                ['Leave', yleave],
                ['Holiday', yhlday]

            ]);

            var options = {
                title: "Today Attendance Status (Effect After Process)",
                is3D: true,
            };

            var chart = new google.visualization.PieChart(document.getElementById('attnstatus'));
            chart.draw(data, options);
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
            <div class="card card-fluid mb-1 mt-0">
                <div class="card-body pt-1 pb-0">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group mb-0">
                                <asp:Label ID="lbldatefrm" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                                <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"
                                    ClientIDMode="Static"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px;" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid mb-5 pb-2" style="min-height: 450px;">
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="row">
                                <asp:Label ID="lblEmpStatus" runat="server" class="label col-12 mb-2 bg-box bg-info text-center">Employee Status</asp:Label>
                                <asp:GridView ID="gvEmpStatus" runat="server" AutoGenerateColumns="False" Width="432px"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpStatus" runat="server" Width="120px" Style="display: initial;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "statusdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle ForeColor="Green" />
                                            <ItemStyle Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNoOfEmp" runat="server" Width="30px" Style="display: initial;"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "noofemp"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                            <div class="row mt-4">
                                <asp:Label ID="lblYestAttn" runat="server" class="label col-12 mb-2 bg-box bg-info text-center">Yesterday Attendance Status</asp:Label>
                                <asp:GridView ID="gvYestAttn" runat="server" AutoGenerateColumns="False" Width="432px"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpStatus" runat="server" Width="120px" Style="display: initial;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "statusdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle ForeColor="Green" />
                                            <ItemStyle Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNoOfEmp" runat="server" Width="30px" Style="display: initial;"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "noofemp"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                            <div class="row mt-4">
                                <asp:Label ID="lblTodayAttn" runat="server" class="label col-12 mb-2 bg-box bg-info text-center">Today's Attendance Status</asp:Label>
                                <asp:GridView ID="gvTodayAttn" runat="server" AutoGenerateColumns="False" Width="432px"
                                    CssClass="table-condensed table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpStatus" runat="server" Width="120px" Style="display: initial;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "statusdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle ForeColor="Green" />
                                            <ItemStyle Font-Bold="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNoOfEmp" runat="server" Width="30px" Style="display: initial;"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "noofemp"))%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-sm-8 col-md-8 col-lg-8">
                            <div class="col-sm-12 col-md-12 col-lg-12 ml-2">
                                <div id="attnstatus" style="width: 100%; height: 250px;"></div>
                            </div>
                            <div class="col-sm-12 col-md-12 col-lg-12">
                                <div id="monpresent" style="width: 100%; height: 300px; margin-top: 10px;"></div>
                            </div>
                            <div class="col-sm-12 col-md-12 col-lg-12">
                                <div id="salcuryear" style="width: 100%; height: 300px; margin-top: 10px;"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--Today Attendance Status--%>
            <div>
                <asp:TextBox ID="yprsnt" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="yabsnt" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="yleave" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="yhlday" runat="server" Style="display: none;"></asp:TextBox>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        //Today Attendance Data//
        var yprsnt = parseFloat($('#<%=this.yprsnt.ClientID%>').val());
        var yabsnt = parseFloat($('#<%=this.yabsnt.ClientID%>').val());
        var yleave = parseFloat($('#<%=this.yleave.ClientID%>').val());
        var yhlday = parseFloat($('#<%=this.yhlday.ClientID%>').val());

        /////////------------------------Employee Status Graph---------------------//////

        function funEmpStatusGraph() {
            Highcharts.setOptions({
                lang: {
                    decimalPoint: ',',
                    thousandsSep: ' '
                }
            });

            $('#attnstatus').highcharts({

                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Today Attendance Status (Effect After Process)'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y}</b>'
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0
                    }
                },

                series: [{
                    name: 'Today Attendance Status',
                    colorByPoint: true,
                    data: [{
                        name: 'Present',
                        y: yprsnt,
                        sliced: true,
                        selected: true,
                        color: '#008000'
                    }, {
                        name: 'Absent',
                        y: yabsnt,
                        color: '#ca2121'
                    }, {
                        name: 'Leave',
                        y: yleave,
                        color: '#0000FF'
                    }, {
                        name: 'Holiday',
                        y: yhlday,
                        color: '#FFEC00'
                    }]
                }],
            });

        };

        function ExecuteGraph(attnstatus, curyrsal) {

            try {

                var attnstatus = JSON.parse(attnstatus);
                var curyrsal = JSON.parse(curyrsal);


                /// Last 10 Days
                var dayseries = [];
                var dayprsnt = [];
                var dayabs = [];
                var dayleav = [];
                $.each(attnstatus, function (i, item) {
                    dayseries.push(item.ymonddesc);
                    dayprsnt.push(item.present);
                    dayabs.push(item.absent);
                    dayleav.push(item.leave);
                });

                /// Salary Current Year
                var saldayseries = [];
                var salary = [];
                $.each(curyrsal, function (i, item) {
                    saldayseries.push(item.ymonddesc);
                    salary.push(item.salamt);
                });

                var monprsntstatus = Highcharts.chart('monpresent', {

                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Attendance Status for the Current Month'
                    },
                    subtitle: {
                        text: '',
                    },
                    xAxis: {
                        categories:
                            dayseries,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Last 10 Days '
                        }
                    },


                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y}</b></td></tr>',
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

                        name: 'Present',
                        data: dayprsnt,
                        color: '#008000'

                    },
                    {

                        name: 'Absent',
                        data: dayabs,
                        color: '#ca2121'
                    }
                        ,
                    {

                        name: 'Leave',
                        data: dayleav,
                        color: '#0000FF'
                    }]
                });
                var salcuryrstatus = Highcharts.chart('salcuryear', {

                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Current Year Salary Status'
                    },
                    subtitle: {
                        text: '',
                    },
                    xAxis: {
                        categories:
                            saldayseries,
                        crosshair: true
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Amount Core TK '
                        }
                    },


                    tooltip: {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            '<td style="padding:0"><b>{point.y}</b></td></tr>',
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

                        name: 'Salary',
                        data: salary,
                        color: '#00bfff'

                    }]
                });
            }
            catch (e) {
                alert(e.message);
            }

        };
    </script>
</asp:Content>
