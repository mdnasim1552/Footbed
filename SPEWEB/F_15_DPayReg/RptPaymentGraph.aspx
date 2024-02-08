<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptPaymentGraph.aspx.cs" Inherits="SPEWEB.F_15_DPayReg.RptPaymentGraph" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/highchart2.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>
    <script src="../Scripts/HighChartExportData.js"></script>

    <script language="javascript" type="text/javascript">





        $(document).ready(function () {

        });



        function funcFundRequirement(ddaily, dweekly, dmonthly, lstcatwise, lstacthead, lstpaywithpro) {
            console.log(dmonthly);
            var ddaily = JSON.parse(ddaily);
            var dweekly = JSON.parse(dweekly);
            var dmonthly = JSON.parse(dmonthly);
            var lstcatwise = JSON.parse(lstcatwise);
            var lstacthead = JSON.parse(lstacthead);
            var lstpaywithpro = JSON.parse(lstpaywithpro);
            var aracthead = [];
            var arrmonth = [];
            var arpmonth = [];
            var artmonth = [];

            var arrweek = [];
            var arpweek = [];
            var artweek = [];

            var arrday = [];
            var arpday = [];
            var artday = [];


            for (var key in lstacthead) {

                aracthead.push({ name: lstacthead[key].actdesc, y: lstacthead[key].amt });

            }



            // Monthly 
            for (var key in dmonthly) {
                if (dmonthly[key].grp == "A") {
                    arrmonth.push(dmonthly[key].month1);
                    arrmonth.push(dmonthly[key].month2);
                    arrmonth.push(dmonthly[key].month3);
                    arrmonth.push(dmonthly[key].monthab3);
                }

                else if (dmonthly[key].grp == "B") {
                    arpmonth.push(dmonthly[key].month1);
                    arpmonth.push(dmonthly[key].month2);
                    arpmonth.push(dmonthly[key].month3);
                    arpmonth.push(dmonthly[key].monthab3);
                }

                else {

                    artmonth.push(dmonthly[key].month1);
                    artmonth.push(dmonthly[key].month2);
                    artmonth.push(dmonthly[key].month3);
                    artmonth.push(dmonthly[key].monthab3);
                }


            }



            // Weekly 
            for (var key in dweekly) {

                if (dweekly[key].grp == "A") {
                    arrweek.push(dweekly[key].week1);
                    arrweek.push(dweekly[key].week2);
                    arrweek.push(dweekly[key].week3);
                    arrweek.push(dweekly[key].week4);
                }

                else if (dweekly[key].grp == "B") {
                    arpweek.push(dweekly[key].week1);
                    arpweek.push(dweekly[key].week2);
                    arpweek.push(dweekly[key].week3);
                    arpweek.push(dweekly[key].week4);
                }

                else {

                    artweek.push(dweekly[key].week1);
                    artweek.push(dweekly[key].week2);
                    artweek.push(dweekly[key].week3);
                    artweek.push(dweekly[key].week4);
                }


            }


            // Daily 
            for (var key in ddaily) {

                if (ddaily[key].grp == "A") {
                    arrday.push(ddaily[key].day1);
                    arrday.push(ddaily[key].day2);
                    arrday.push(ddaily[key].day3);
                    arrday.push(ddaily[key].day4);
                    arrday.push(ddaily[key].day5);
                    arrday.push(ddaily[key].day6);
                    arrday.push(ddaily[key].day7);

                }

                else if (dweekly[key].grp == "B") {
                    arpday.push(ddaily[key].day1);
                    arpday.push(ddaily[key].day2);
                    arpday.push(ddaily[key].day3);
                    arpday.push(ddaily[key].day4);
                    arpday.push(ddaily[key].day5);
                    arpday.push(ddaily[key].day6);
                    arpday.push(ddaily[key].day7);
                }

                else {

                    artday.push(ddaily[key].day1);
                    artday.push(ddaily[key].day2);
                    artday.push(ddaily[key].day3);
                    artday.push(ddaily[key].day4);
                    artday.push(ddaily[key].day5);
                    artday.push(ddaily[key].day6);
                    artday.push(ddaily[key].day7);
                }


            }




            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });



            $('#chartpaywpro').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'PDC,Bills & Process Status',
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
                        text: 'Amount in Lac'
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
                            format: '{point.y:.1f}Tk'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "PDC,Bills & Process Status",
                        "colorByPoint": true,
                        "data": [
                           {
                               "name": "PDC",
                               "y": lstpaywithpro[0]["pdcam"],

                           },
                           {
                               "name": "Bills",
                               "y": lstpaywithpro[0]["billam"],

                           },
                           {
                               "name": "PDC & Bills",
                               "y": lstpaywithpro[0]["topayam"],

                           },
                           {
                               "name": "Inprocess",
                               "y": lstpaywithpro[0]["inproam"],

                           },
                            {
                                "name": "PDC,Bills & Inprocess",
                                "y": lstpaywithpro[0]["topayamwpro"],

                            }



                        ]
                    }
                ]
            });



            $('#chartmontyly').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },

                subtitle: {
                    text: 'Payment Due - Monthly',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },

                xAxis: {
                    categories: ['Month 1', 'Month 2', 'Month 3', 'Month above 3']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Taka in Lac'
                    },
                    stackLabels: {
                        enabled: false,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'center',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false,
                    enabled: true

                },


                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                },
                series: [
                {
                    name: 'Total',
                    data: artmonth
                },

                 {
                     name: 'PDC',
                     data: arpmonth
                 },
                 {
                     name: 'Regular',
                     data: arrmonth
                 }]
            });


            $('#chartweekly').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },


                subtitle: {
                    text: 'Payment Due - Weekly',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },
                xAxis: {
                    categories: ['Week 1', 'Week 2', 'Week 3', 'Week 4']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Taka in Lac'
                    },
                    stackLabels: {
                        enabled: false,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false,
                    enabled: false

                },


                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                },
                series: [
                    {
                        name: 'Total',
                        data: artweek
                    },

                    {
                        name: 'PDC',
                        data: arpweek
                    },

                    {
                        name: 'Regular',
                        data: arrweek
                    }


                ]
            });




            $('#chartdayily').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },


                subtitle: {
                    text: 'Payment Due - Daily',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }

                },
                xAxis: {
                    categories: ['Day1', 'Day2', 'Day3', 'Day4', 'Day5', 'Day6', 'Day7']
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Taka in Lac'
                    },
                    stackLabels: {
                        enabled: false,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false,
                    enabled: false

                },


                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                },
                series: [

                     {
                         name: 'Total',
                         data: artday
                     },

                     {
                         name: 'PDC',
                         data: arpday
                     },
                    {
                        name: 'Regular',
                        data: arrday
                    }]
            });








            $('#charbillcat').highcharts({


                chart: {
                    type: 'pie'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Bills (Catagrory Wise) ',
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
                        text: 'Amount in Lac'
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
                            format: '{point.y:.1f}Tk'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                "series": [
                    {
                        "name": "Bills (Catagrory Wise)",
                        "colorByPoint": true,
                        "data": [
                           {
                               "name": "Supplier",
                               "y": lstcatwise[0]["supam"],

                           },
                           {
                               "name": "Sub-Contractor",
                               "y": lstcatwise[0]["conam"],

                           },
                           {
                               "name": "General",
                               "y": lstcatwise[0]["genam"],

                           }



                        ]
                    }
                ]
            });




            $('#chartacchead').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Bills (Group Wise)',
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
                        text: 'Amount in Lac'
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
                            format: '{point.y:.2f}Tk'
                        }
                    }
                },

                tooltip: {
                    headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                    pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                },

                series: [{
                    name: 'Budget',
                    color: '#b2e8a0',
                    //data: [{
                    //    name: 'Republican',
                    //    y: 5,
                    //    drilldown: 'republican-2010'
                    //}, {
                    //    name: 'Democrats',
                    //    y: 2,
                    //    drilldown: 'democrats-2010'
                    //}, {
                    //    name: 'Other',
                    //    y: 4,
                    //    drilldown: 'other-2010'
                    //}]
                    data: aracthead,
                }

                ],


            });












        }




    </script>
    <style>
      
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
                                        <asp:Label ID="lbldate" runat="server" CssClass="control-label" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" AutoCompleteType="Disabled" CssClass="form-control"
                                            ClientIDMode="Static"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate" Enabled="true"></cc1:CalendarExtender>

                                    </div>

                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkbtnok" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label" for="FromDate">Bank Balance</label>
                                        <asp:HyperLink ID="Hyplnkbal" runat="server" ToolTip="Click Details bank position" Style=" margin-top: -08px;" CssClass=" form-control " NavigateUrl="~/F_21_GAcc/AccTrialBalance.aspx?Type=BankPosition" Target="_blank"></asp:HyperLink>
                                        <%--  <asp:Label ID="" runat="server" CssClass=" form-control "></asp:Label>--%>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class=" btn-group" role="group" aria-label="Button group with nested dropdown" Style=" margin-top: 20px;">
                                        <button type="button" class="btn btn-danger">Operations</button>
                                        <div class="btn-group" role="group">
                                            <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                            <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                <div class="dropdown-arrow"></div>
                                                <asp:HyperLink ID="hlnkpostdatedcheque" runat="server" NavigateUrl="~/F_21_GAcc/AccPayUpdate.aspx?Type=AccIsu" Target="_blank" CssClass="dropdown-item">Post Dated Cheque</asp:HyperLink>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Show Bills</asp:HyperLink>
                                                <asp:HyperLink ID="hlnkPendingbill" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccPurNotUpdated.aspx" CssClass="dropdown-item">Bill(In Process)</asp:HyperLink>


                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3 margin-top30px" >
                                    <asp:Label ID="lbltk" runat="server" CssClass="lblTxt lblName pull-right " Style="font-size: 16px;">Taka in Lac </asp:Label>

                                </div>


                            </div>

                            <div class="card card-fluid" style="min-height: 350px;">
                                <div class="row">



                                    <div class="col-md-6" style="border: 1px solid #D8D8D8; margin-left: 0px;">


                                        <div id="chartpaywpro" style="width: 550px; height: 250px;"></div>

                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="chartdayily" style="width: 550px; height: 250px;"></div>



                                    </div>




                                </div>

                                <div class="row">

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">

                                        <div id="chartweekly" style="width: 550px; height: 250px;"></div>
                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                                        <div id="chartmontyly" style="width: 550px; height: 250px;"></div>

                                    </div>

                                </div>

                                <div class="row">

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8; margin-left: 0px;">


                                        <div id="charbillcat" style="width: 550px; height: 250px;"></div>

                                    </div>

                                    <div class="col-md-6" style="border: 1px solid #D8D8D8">


                                        <div id="chartacchead" style="width: 550px; height: 250px;"></div>
                                    </div>

                                </div>

                            </div>






                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>



</asp:Content>

