<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AllGraph.aspx.cs" Inherits="SPEWEB.AllGraph" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highcharts.js"></script>

    <script src="../Scripts/highchartexporting.js"></script>

    <script language="javascript" type="text/javascript">
        function GetData() {

            $.ajax({
                type: "POST",
                url: "AllGraph.aspx/GetAllData",
                data: '{dates: "' + $('#<%=this.txtDate.ClientID%>').val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(JSON.parse(response.d));
                    var data = JSON.parse(response.d);
                    // alert(data['sales'][0]['collamt']);
                    funMonthlyGraph(data)
                },
                failure: function (response) {
                    //  alert(response);
                    alert("f");
                }
            });
        }
        $(document).ready(function () {
            GetData();

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);



        });
        function pageLoaded() {



        }


        function addplug(plug) {
            // alert(plug);


            $('#<%=this.txtflag.ClientID %>').val(plug);

            switch (plug) {

                case "Procurement":
                case "Construction":
                case "Accounts":
                    $("#OpDate").show();
                    break;
                default:
                    $("#OpDate").hide();
                    break;
            }

        }

        function activetab() {

            var plug = $('#<%=this.txtflag.ClientID %>').val();
            // alert(plug);
            switch (plug) {
                case "Procurement":
                    $('.nav-tabs a[href="#tab1primary"]').tab('show');



                    break;
                case "Sales":



                    $('.nav-tabs a[href="#tab0primary"]').tab('show');
                    break;
                case "Construction":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab2primary"]').tab('show');
                    break;
                case "Accounts":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab3primary"]').tab('show');
                    break;
                case "Collection":
                    $("#OpDate").hide();
                    $('.nav-tabs a[href="#tab4primary"]').tab('show');
                    break;
            }



        }

        function myFunction(x) {
            x.classList.toggle("change");
        }
    </script>
    <style>
        .flowMenu ul {
            margin: 0;
        }

            .flowMenu ul li {
                list-style: none;
                padding: 5px 0;
                /*border-bottom: 1px solid #e9e9e9;*/
            }

                .flowMenu ul li a {
                    padding-bottom: 8px;
                    color: #000;
                    font-size: 14px;
                    font-weight: normal;
                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                    font-family: 'Times New Roman';
                }

        .flowMenu h3 {
            background: #046971;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            color: #fff;
            font-family: AR CENA;
            font-size: 18px;
            /*font-weight: bold;*/
            line-height: 40px;
            margin: 5px 0 0;
            padding: 0 0;
            text-decoration: none;
            text-align: center;
        }



        ul.sidebarMenu li {
            display: block;
            list-style: none;
            border: 1px solid #00444C;
            padding: 0;
            /* border-bottom: 0; */
        }

            ul.sidebarMenu li a {
                text-align: left;
                display: block;
                cursor: pointer;
                /* background: #32CD32; */
                background: #046971;
                border-radius: 5;
                color: #fff;
                text-align: left;
                padding: 0 5px;
                border-bottom: 1px;
                line-height: 30px;
                color: #fff;
                font-size: 13px;
                font-weight: normal;
                text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
            }

                ul.sidebarMenu li a:hover {
                    background: #43b643;
                    color: #fff;
                }

        .AllGraph .nav-tabs {
            border-bottom: 0;
        }

        .sidebarMenu li h5 {
            background: #43b643;
            color: #fff;
            font-size: 15px;
            margin: 0;
            padding: 0;
            line-height: 35px;
            text-align: center;
        }






        #demo {
            margin-top: 30px;
            position: absolute;
            z-index: 200;
            margin-left: 10px;
        }

        .container {
            display: inline-block;
            cursor: pointer;
        }

        .bar1, .bar2, .bar3 {
            width: 25px;
            height: 2.5px;
            background-color: #333;
            margin: 3px 0;
            transition: 0.4s;
        }

        .change .bar1 {
            transform: translate(0, 6px) rotate(-45deg);
        }

        .change .bar2 {
            opacity: 0;
        }

        .change .bar3 {
            transform: translate(0, -5px) rotate(45deg);
        }

        #ddlAnalysis {
            border: none;
            border-color: none;
            background-color: transparent;
        }

        button:focus {
            outline: 0 !important;
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
            <asp:Panel runat="server" ID="plnGrph">


                <div class="card card-fluid">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-1" style="margin-top: 20px;">
                                <div class="dropdown">
                                    <button class="btn btn-secondary btn-sm dropdown-toggle" type="button" id="dropdownMenu2" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                        Analysis
                                    </button>
                                    <div class="dropdown-menu" aria-labelledby="dropdownMenu2">
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_01_Mer/RptMerLCAnalysis.aspx?Type=Report&pactcode=")%>" target="_blank">Order</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_01_Mer/OrderInformation.aspx")%>" target="_blank">Budget</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_15_Pro/PurInformation.aspx")%>" target="_blank">BBLC</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_15_Pro/ProductionInfo.aspx")%>" target="_blank">Production</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_23_MAcc/AccDashBoard.aspx")%>" target="_blank">Accounts</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/GenPage.aspx?Type=05")%>" target="_blank">Inventory</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_31_Mis/RptProjectStatus.aspx?Type=LCStatus")%>" target="_blank">L/C Status</a>
                                        <%--<a class="dropdown-item" href="<%=this.ResolveUrl("~/F_32_Mis/RptManProjectSum.aspx")%>" target="_blank">Project Report 2</a>--%>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_35_GrAcc/RptIndvRealGraph.aspx")%>" target="_blank">Overall</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_21_GAcc/AccFincStatmnt.aspx")%>" target="_blank">Financial Statement</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_21_GAcc/RptLCVariance.aspx")%>" target="_blank">Income Statement L/C</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("#")%>" target="_blank">L/C Resource</a>
                                        <a class="dropdown-item" href="<%=this.ResolveUrl("~/F_62_Mis/RptRatioAnalisiys.aspx")%>" target="_blank">Ratio Analysis</a>

                                    </div>
                                </div>
                            </div>

                            <%--  <div class="col-md-1 text-center" style="margin-top: 23px;">
                                <button type="button" id="ddlAnalysis" data-toggle="collapse" data-target="#demo">
                                    <div class="container" onclick="myFunction(this)">
                                        <div class="bar1"></div>
                                        <div class="bar2"></div>
                                        <div class="bar3"></div>
                                    </div>
                                </button>
                            </div>

                            <div id="demo" class="collapse" style="margin-top:10px;">
                                <div class="flowMenu">
                                    <ul class="dashCir block sidebarMenu">


                                        <li>
                                            <h5>Analysis</h5>
                                        </li>

                                        <li><a href="<%=this.ResolveUrl("~/F_01_Mer/RptMerLCAnalysis.aspx?Type=Report&pactcode=")%>" target="_blank">Order</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_01_Mer/OrderInformation.aspx")%>" target="_blank">Budget</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Pro/PurInformation.aspx")%>" target="_blank">BBLC</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Pro/ProductionInfo.aspx")%>" target="_blank">Production</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_23_MAcc/AccDashBoard.aspx")%>" target="_blank">Accounts</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=05")%>" target="_blank">Inventory</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_31_Mis/RptProjectStatus.aspx?Type=LCStatus")%>" target="_blank">L/C Status</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptIndvRealGraph.aspx")%>" target="_blank">Overall</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_21_GAcc/AccFincStatmnt.aspx")%>" target="_blank">Financial Statement</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_21_GAcc/RptLCVariance.aspx")%>" target="_blank">Income Statement L/C</a></li>
                                        <li><a href="<%=this.ResolveUrl("#")%>" target="_blank">L/C Resource</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_62_Mis/RptRatioAnalisiys.aspx")%>" target="_blank">Ratio Analysis</a></li>



                                    </ul>
                                </div>
                            </div>--%>

                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Label ID="lbldatefrm" runat="server">Date</asp:Label>
                                    <asp:TextBox ID="txtDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server"
                                        Format="dd-MMM-yyyy" TargetControlID="txtDate" Enabled="true"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-1 text-left" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" OnClientClick="GetData()" CssClass="btn btn-sm btn-primary">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <asp:TextBox ID="txtflag" Style="display: none;" runat="server"></asp:TextBox>


                        <%--                        <div class="clearfix"></div>



                        <div class="clearfix"></div>

                        <div class="col-md-10">
                            <div class="row">
                                <div class="col-md-12 text-right">
                                </div>
                            </div>

                        </div>--%>
                    </div>
                </div>






                <!------main panel----------->

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
                                    <div id="purchart" style="width: 100%; height: 250px;"></div>
                                </div>
                            </div>
                            <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                <div class="card-body">
                                    <div id="prodchart" style="width: 100%; height: 250px;"></div>
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
                                    <div id="accchart" style="width: 100%; height: 250px;"></div>
                                </div>
                            </div>
                            <div class="card card-fluid" style="width: 48.7%; margin: 0 auto">
                                <div class="card-body">
                                    <div id="balsheetchart" style="width: 100%; height: 250px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--<div class="row-fluid">
                   <div class="col-md-6" style="border: 1px solid #D8D8D8">
                        <div id="SalesChart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                    </div>
                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                        <div id="CollChart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                    </div>
                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                        <div id="purchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                    </div>
                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                        <div id="prodchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                    </div>
                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                        <div id="accchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                    </div>
                    <div class="col-md-6" style="border: 1px solid #D8D8D8">
                        <div id="balsheetchart" style="width: 580px; height: 250px; margin: 0 auto"></div>
                    </div>

                </div>--%>


            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">

        /////--------------------------Month Graph-------------------------

        function funMonthlyGraph(data) {

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
                    data: [data['pur'][0]['ttlsalamt'], data['pur'][1]['ttlsalamt'], data['pur'][2]['ttlsalamt'], data['pur'][3]['ttlsalamt'], data['pur'][4]['ttlsalamt'], data['pur'][5]['ttlsalamt'], data['pur'][6]['ttlsalamt'], data['pur'][7]['ttlsalamt'], data['pur'][8]['ttlsalamt'], data['pur'][9]['ttlsalamt'], data['pur'][10]['ttlsalamt'], data['pur'][11]['ttlsalamt']],
                    color: '#f4429e'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['pur'][0]['tpayamt'], data['pur'][1]['tpayamt'], data['pur'][2]['tpayamt'], data['pur'][3]['tpayamt'], data['pur'][4]['tpayamt'], data['pur'][5]['tpayamt'], data['pur'][6]['tpayamt'], data['pur'][7]['tpayamt'], data['pur'][8]['tpayamt'], data['pur'][9]['tpayamt'], data['pur'][10]['tpayamt'], data['pur'][11]['tpayamt']],
                    color: '#b24942'
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
                    text: 'Order Vs Shipment FC(K)',
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
                    name: 'Order',
                    data: [data['sales'][0]['ordramt'], data['sales'][1]['ordramt'], data['sales'][2]['ordramt'], data['sales'][3]['ordramt'], data['sales'][4]['ordramt'], data['sales'][5]['ordramt'], data['sales'][6]['ordramt'], data['sales'][7]['ordramt'], data['sales'][8]['ordramt'], data['sales'][9]['ordramt'], data['sales'][10]['ordramt'], data['sales'][11]['ordramt']],
                    color: '#1581C1'

                }, {

                    name: 'Shipment',
                    //color:red,
                    data: [data['sales'][0]['shipamt'], data['sales'][1]['shipamt'], data['sales'][2]['shipamt'], data['sales'][3]['shipamt'], data['sales'][4]['shipamt'], data['sales'][5]['shipamt'], data['sales'][6]['shipamt'], data['sales'][7]['shipamt'], data['sales'][8]['shipamt'], data['sales'][9]['shipamt'], data['sales'][10]['shipamt'], data['sales'][11]['shipamt']],
                    color: '#CA6621'
                }]
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
                    data: [data['prod'][0]['bgdamt'], data['prod'][1]['bgdamt'], data['prod'][2]['bgdamt'], data['prod'][3]['bgdamt'], data['prod'][4]['bgdamt'], data['prod'][5]['bgdamt'], data['prod'][6]['bgdamt'], data['prod'][7]['bgdamt'], data['prod'][8]['bgdamt'], data['prod'][9]['bgdamt'], data['prod'][10]['bgdamt'], data['prod'][11]['bgdamt']],
                    color: '#96780a'

                }, {

                    name: 'Excution',
                    //color:red,
                    data: [data['prod'][0]['proamt'], data['prod'][1]['proamt'], data['prod'][2]['proamt'], data['prod'][3]['proamt'], data['prod'][4]['proamt'], data['prod'][5]['proamt'], data['prod'][6]['proamt'], data['prod'][7]['proamt'], data['prod'][8]['proamt'], data['prod'][9]['examtcore'], data['prod'][10]['proamt'], data['prod'][11]['proamt']],
                    color: '#990c4b'
                }]
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
                    data: [data['acc'][0]['cram'], data['acc'][1]['cram'], data['acc'][2]['cram'], data['acc'][3]['cram'], data['acc'][4]['cram'], data['acc'][5]['cram'], data['acc'][6]['cram'], data['acc'][7]['cram'], data['acc'][8]['cram'], data['acc'][9]['cram'], data['acc'][10]['cram'], data['acc'][11]['cram']],
                    color: '#138225'

                }, {

                    name: 'Payment',
                    //color:red,
                    data: [data['acc'][0]['dram'], data['acc'][1]['dram'], data['acc'][2]['dram'], data['acc'][3]['dram'], data['acc'][4]['dram'], data['acc'][5]['dram'], data['acc'][6]['dram'], data['acc'][7]['dram'], data['acc'][8]['dram'], data['acc'][9]['dram'], data['acc'][10]['dram'], data['acc'][11]['dram']],
                    color: '#aa1811'
                }]
            });

            ////Collection Bar chart 
            $('#CollChart').highcharts({


                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'Shipment Vs Realization(k)',
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


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Shipment',
                    data: [data['sales'][0]['shipamt'], data['sales'][1]['shipamt'], data['sales'][2]['shipamt'], data['sales'][3]['shipamt'], data['sales'][4]['shipamt'], data['sales'][5]['shipamt'], data['sales'][6]['shipamt'], data['sales'][7]['shipamt'], data['sales'][8]['shipamt'], data['sales'][9]['shipamt'], data['sales'][10]['shipamt'], data['sales'][11]['shipamt']],
                    color: '#42f47a'

                }, {

                    name: 'Realization',
                    //color:red,
                    data: [data['sales'][0]['collamt'], data['sales'][1]['collamt'], data['sales'][2]['collamt'], data['sales'][3]['collamt'], data['sales'][4]['collamt'], data['sales'][5]['collamt'], data['sales'][6]['collamt'], data['sales'][7]['collamt'], data['sales'][8]['collamt'], data['sales'][9]['collamt'], data['sales'][10]['collamt'], data['sales'][11]['collamt']],
                    color: '#454289'
                }]
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
                            format: '{point.y:.1f}%'
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
                                "y": data['balshet'][0]['noncuram'],

                            },
                            {
                                "name": "Current Asset",
                                "y": data['balshet'][0]['curam'],

                            },
                            {
                                "name": "Equity",
                                "y": data['balshet'][0]['equityam'],

                            },
                            {
                                "name": "Non-Current Liabilities",
                                "y": data['balshet'][0]['noncurlia'],

                            },
                            {
                                "name": "Current Liabilities",
                                "y": data['balshet'][0]['curlia'],

                            }
                        ]
                    }
                ]
            });

        }



    </script>
</asp:Content>


