<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="CompanyOverAllReport.aspx.cs" Inherits="SPEWEB.CompanyOverAllReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        ul.footerMenu li {
            display: block;
            list-style: none;
            padding: 0;
            /* border-bottom: 0; */
        }

            ul.footerMenu li a {
                text-align: left;
                display: block;
                cursor: pointer;
                /* background: #32CD32; */
                background: #046971;
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

                ul.footerMenu li a:hover {
                    background: red;
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

        #demo1 {
            margin-top: 30px;
            position: absolute;
            z-index: 200;
            margin-left: 1050px;
        }
    </style>
    <style type="text/css">
        .footer {
            background-color: #2e3639;
            /*position: relative;*/
            z-index: 1;
        }

            .footer .splitter {
                background-color: #ac0;
                background-image: linear-gradient(45deg, rgba(255, 255, 255, 0.2) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.2) 50%, rgba(255, 255, 255, 0.2) 75%, transparent 75%, transparent);
                background-size: 50px 50px;
                box-shadow: 1px 1px 8px gray;
                height: 10px;
            }




            .footer .bar {
                background-color: #1e2629;
                padding: 11px 0 0;
            }

        .quickLink h4 {
            color: #ffffff;
        }

        ul.Menulinks {
            margin: 0;
            padding: 0;
        }

            ul.Menulinks li {
                list-style: none;
            }

                ul.Menulinks li a {
                    display: block;
                    color: #ffffff;
                    padding: 2px 5px 2px 0;
                }

        .Menulinks li a:hover {
            color: #ed4e6e;
            text-decoration: none;
        }

        .Menulinks .glyphicon {
            padding-right: 3px;
        }

        .quickLink p {
            margin: 0;
        }

        .quickLink a:hover {
            color: #0989c6;
        }

        .clTestimonial img {
            margin: 0 auto;
        }

        .clTestimonialTxt {
            text-align: right;
            color: #b3b9bf;
        }

        .clTestimonial h5 {
            color: #0989c6;
            font-size: 14px;
            font-weight: bold;
        }

        .clTestimonial h6 {
            font-size: 18px;
            color: #fff;
        }

        .clTestimonial a {
            color: #ffffff;
        }

        .quickLink {
            color: #fff;
        }

        .MainMenu a {
            background: #f9f9f9;
            color: #000;
        }

            .MainMenu a:hover {
                color: #fff;
                /*//background: #336dbb;*/
            }

        .quickLink fieldset {
            color: #fff;
            font-size: 14px;
            line-height: 18px;
        }

        .copyright p {
            color: #ffffff;
        }

        .nAsitModel p {
            font-size: 18px;
            line-height: 22px;
            color: #000;
        }

        .nAsitModel a span.serialNumb {
            border-right: 1px solid #ccc;
            float: left;
            margin-right: 5px;
            padding: 0 5px;
            text-align: left;
        }

        .tblh {
            background: #DFF0D8;
            height: 30px;
            text-align: center;
        }

        .th1 {
            width: 200px;
            text-align: center;
        }

        .th2 {
            width: 100px;
            text-align: center;
        }

        .th3 {
            width: 100px;
            text-align: center;
        }
    </style>

    <script src="Scripts/highcharts.js"></script>
    <script src="Scripts/highchartexporting.js"></script>

    <script language="javascript" type="text/javascript">

        var comcod, Date1, Date2;
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
          
            GetData();
            //GetData();
        }



        function GetData() {
            try {

                comcod = <%=this.GetCompCode()%>;
                
                NavigateUrl="~/F_99_Allinterface/SalesInterface"
                if(comcod.toString().substring(0,1)=="1")
                {
                   
                    $("#lblstock").attr("href","F_16_Bill/RptBilligSummary"); 
                    $("#lbldues").attr("href","F_41_GAcc/RptProBillStatus?Type=Billstatus&prjcode="); 
                }
                else 

                {
                   
                    $("#lblstock").attr("href","F_19_FGInv/InventoryInformation"); 
                    $("#lbldues").attr("href","F_99_Allinterface/SalesInterface"); 
                 
                
                }

              
          


               
                $.ajax({
                    type: "POST",
                   // url: "<%= ResolveUrl("~/CompanyOverAllReport/GetAllData") %>",
                  url: "CompanyOverAllReport.aspx/GetAllData",
                    data: '{date1: "' + $('#<%=this.txtDateFrom.ClientID%>').val() + '" , date2: "' + $('#<%=this.txtDateto.ClientID%>').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log(JSON.parse(response.d));
                        var data = response.d;
                        
                        //console.log(data['account']);
                        ExecuteGraph(data);
                    },
                    failure: function (response) {
                        //  alert(response);
                        alert("f");
                    }
                });
            }
            catch(e) {
                alert(e);
            }
           
        }

        function ExecuteGraph(bgd) {
            try {

                Highcharts.setOptions({
                    lang: {
                        decimalPoint: '.',
                        thousandsSep: ' '
                    }
                });

                var bgddata = JSON.parse(bgd);
                console.log(bgddata);

                //Acc Legend
                var accdata = bgddata['account'];
              
                var armainhead = [];
                for (var i = 0; i < accdata.length; i++) {
                    armainhead[i] = accdata[i]["head"];
                }

                console.log(accdata);
                var ar1 = '';
                var ar2 = '';
                var row = '';
                comcod = <%=this.GetCompCode()%>;
                Date1 = $('#txtDateFrom').val();
                Date2 = $('#txtDateto').val();

                $.each(accdata,
                    function(i, item) {
                        ar1 = (item.grp == "A")? '<a target=_blank href=' + encodeURI('F_21_GAcc/LinkRptReciptPayment?Type=receipt&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2+'&paycode=') + '>'
                            : item.grp == "B"? '<a target=_blank href=' + encodeURI('F_21_GAcc/LinkRptReciptPayment?Type=payment&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2+'&paycode=') + '>'
                            : '';
                        ar2 = (item.grp == "A") || (item.grp == "B") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + ar1 + item.head + ar2 + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#AccTable tbody").html(row);
                    });


                //Sales Legend
                var salesdata = bgddata['sales'];
                
                var saleshead = [];
                for (var i = 0; i < salesdata.length; i++) {
                    saleshead[i] = salesdata[i]["head"];
                }

                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(salesdata,
                    function(i, item) {
                        ar1 = (item.grp == "A")
                            ? '<a target=_blank href=' + encodeURI('F_01_Mer/RptOrdSum?Type=Shipment&Date1='+Date1+'&Date2='+Date2) + '>'
                            : item.grp == "B"
                            ? '<a target=_blank href=' + encodeURI('F_01_Mer/RptOrdSum?Type=Relz&Date1='+Date1+'&Date2='+Date2) + '>'
                            : '';
                        ar2 = (item.grp == "A") || (item.gcod == "B") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + ar1 + item.head + ar2 + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#sales tbody").html(row);
                    });

                //Purchase Legend
                var purchasedata = bgddata['purchase'];
                var purchasehead = [];
                for (var i = 0; i < purchasedata.length; i++) {
                    purchasehead[i] = purchasedata[i]["head"];
                }

                var paycode='2600000000';
                var ar1 = '';
                var ar2 = '';
                var row = '';
                $.each(purchasedata,
                    function(i, item) {
                        ar1 = (item.grp == "A")
                            ? '<a target=_blank href=' + encodeURI('F_10_Procur/PurSumMatWise?Date1='+Date1+'&Date2='+Date2) + '>'
                            : item.grp == "B"? '<a target=_blank href=' + encodeURI('F_21_GAcc/LinkRptReciptPayment?Type=payment&comcod=' + comcod + '&Date1=' +Date1 + '&Date2=' + Date2+'&paycode=' + paycode) + '>'
                            : '';
                        ar2 = (item.grp == "A") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + ar1 + item.head + ar2 + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#purchase tbody").html(row);
                    });
               

                //Cons Legend
                var consdata = bgddata['tarvsact'];
                var conshead = [];
                for (var i = 0; i < consdata.length; i++) {
                    conshead[i] = consdata[i]["head"];
                }
                var ar1 = '';
                var ar2 = '';
                var row = '';
               
                $.each(consdata,
                    function(i, item) {
                        ar1 = (item.gcod == "A")
                            ? '<a target=_blank href=' + encodeURI('F_32_Mis/RptConstruProgressSum') + '>'
                            : item.gcod == "B"
                            ? '<a target=_blank href=' +
                            encodeURI('F_09_PImp/RptImpExeStatus?Type=DayWiseExecution&prjcode=&Date1='+Date1+'&Date2='+Date2) +
                            '>'
                            : '';
                        ar2 = (item.gcod == "A") || (item.gcod == "B") ? '</a>' : '';
                        row += "<tr>";
                        row += "<td>" + ar1 + item.head + ar2 + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#construction tbody").html(row);
                    });


                //Bank Legend
                var bankdata = bgddata['bankbalance'];
                var bankhead = [];
                for (var i = 0; i < bankdata.length; i++) {
                    bankhead[i] = bankdata[i]["head"];
                }
                var row = '';
                $.each(bankdata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#cashbankbal tbody").html(row);
                    });
                //Stock Legend
                var stockdata = bgddata['stock'];
                var stockhead = [];
                for (var i = 0; i < stockdata.length; i++) {
                    stockhead[i] = stockdata[i]["head"];
                }

                var row = '';
                $.each(stockdata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#stocktable tbody").html(row);
                    });



               

                //Pending Bill Legend
                var penbildata = bgddata['penbil'];
                var penbilhead = [];
                for (var i = 0; i < penbildata.length; i++) {
                    penbilhead[i] = penbildata[i]["head"];
                }
                var row = '';
                $.each(penbildata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#penbillpart tbody").html(row);
                    });


                
                //Future Fund
                var ffunddata = bgddata['ffund'];
              
                var ffundhead = [];
                for (var i = 0; i < ffunddata.length; i++) {
                    ffundhead[i] = ffunddata[i]["head"];
                }

                

                var row = '';
                $.each(ffunddata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#futuretable tbody").html(row);
                    });

                

                //Future Cost 
                var fcostdata = bgddata['fcost'];
                
                console.log(fcostdata);
                var fcosthead = [];
                for (var i = 0; i < fcostdata.length; i++) {
                    fcosthead[i] = fcostdata[i]["head"];
                }
                var row = '';
                $.each(fcostdata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#costtable tbody").html(row);
                    });
                
                //Future Fund Vs Cost 
                var fvscostdata = bgddata['fundcost'];
                var fvscosthead = [];
                for (var i = 0; i < fvscostdata.length; i++) {
                    fvscosthead[i] = fvscostdata[i]["head"];
                }
                var row = '';
                $.each(fvscostdata,
                    function(i, item) {
                        
                        row += "<tr>";
                        row += "<td>"+item.head+ "</td>";
                        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                        row += "</tr>";
                        $("#funvscosttable tbody").html(row);
                    });
                //Merchandising  
                var merchdata = bgddata['merch'];
                var merchhead = [];
                for (var i = 0; i < merchdata.length; i++) {
                    merchhead[i] = merchdata[i]["head"];
                }
                var ar1 = '';
                var ar2 = '';
                var row = '';
                //console.log(merchdata);
                $.each(merchdata,
                    function(i, item) {
                        ar1 = (item.grp == "A")? '<a target=_blank href=' + encodeURI('F_01_Mer/RptOrdSum?Type=Ord&Date1='+Date1+'&Date2='+Date2) + '>'
                           : item.grp == "B"? '<a target=_blank href=' + encodeURI('F_01_Mer/RptOrdSum?Type=Shipment&Date1='+Date1+'&Date2='+Date2) + '>'
                           : '';
                        ar2 = (item.grp == "A") || (item.grp == "B") ? '</a>' : '';

                        row += "<tr>";
                        row += "<td>" + ar1 + item.head + ar2 + "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.amount == 0)
                                ? ''
                                : (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +
                            "</td>";
                        row += "<td style=text-align:right;>" +
                            ((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +
                            "</td>";
                        row += "</tr>";
                        $("#Merchandtable tbody").html(row);
                    });

                //var row = '';
                //$.each(merchdata,
                //    function(i, item) {
                        
                //        row += "<tr>";
                //        row += "<td>"+item.head+ "</td>";
                //        row += "<td style=text-align:right;>" +((item.amount == 0)? '': (item.amount.toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 })) +"</td>";
                //        row += "<td style=text-align:right;>" +((item.peramt == 0) ? '' : (item.peramt.toFixed(2)).toLocaleString('en-US')) +"</td>";
                //        row += "</tr>";
                //        $("#Merchandtable tbody").html(row);
                //    });
                
                //Accounts
                Highcharts.chart('Accountsdt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in accdata) {
                                    if (accdata.hasOwnProperty(key)) {
                                        data.push([
                                            accdata[key].head,
                                            accdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                //sales
                Highcharts.chart('Salesdt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, saleshead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in salesdata) {
                                    if (salesdata.hasOwnProperty(key)) {
                                        data.push([
                                            salesdata[key].head,
                                            salesdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });
                //Purchase
                Highcharts.chart('purchasedt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, purchasehead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in purchasedata) {
                                    if (purchasedata.hasOwnProperty(key)) {
                                        data.push([
                                            purchasedata[key].head,
                                            purchasedata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                //Construction
                Highcharts.chart('Consdt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, conshead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in consdata) {
                                    if (consdata.hasOwnProperty(key)) {
                                        data.push([
                                            consdata[key].head,
                                            consdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                //Bank balance
                Highcharts.chart('bbalancedt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, bankhead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in bankdata) {
                                    if (bankdata.hasOwnProperty(key)) {
                                        data.push([
                                            bankdata[key].head,
                                            bankdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                //Stock
                Highcharts.chart('stockdt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, stockhead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in stockdata) {
                                    if (stockdata.hasOwnProperty(key)) {
                                        data.push([
                                            stockdata[key].head,
                                            stockdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

              
                //Pending Bill
                Highcharts.chart('billdt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, penbilhead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in penbildata) {
                                    if (penbildata.hasOwnProperty(key)) {
                                        data.push([
                                            penbildata[key].head,
                                            penbildata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                //Man Power
                Highcharts.chart('ffunddt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, ffundhead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in ffunddata) {
                                    if (ffunddata.hasOwnProperty(key)) {
                                        data.push([
                                            ffunddata[key].head,
                                            ffunddata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

   
                ///Future Cost
                Highcharts.chart('fcostdt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, fcosthead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in fcostdata) {
                                    if (fcostdata.hasOwnProperty(key)) {
                                        data.push([
                                            fcostdata[key].head,
                                            fcostdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

                //Fund Vs Cost
                Highcharts.chart('fundcstdt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, fvscosthead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in fvscostdata) {
                                    if (fvscostdata.hasOwnProperty(key)) {
                                        data.push([
                                            fvscostdata[key].head,
                                            fvscostdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });
                //Merchandising 
                Highcharts.chart('Merchanddt',
                {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: ''
                    },
                    subtitle: {
                        text: '',
                        style: {
                            color: '#44994a',
                            fontWeight: 'bold'
                        }
                    },


                    xAxis: {
                        type: 'category',
                        labels:
                        {
                            formatter: function() {
                                if ($.inArray(this.value, merchhead) !== -1) {
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
                            text: ''
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
                        pointFormat:
                            '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> of total<br/>'
                    },

                    "series": [
                        {
                            "name": "",
                            "colorByPoint": true,
                            "data":
                            (function() {
                                // generate an array of random data
                                var data = [],

                                    i;

                                for (var key in merchdata) {
                                    if (merchdata.hasOwnProperty(key)) {
                                        data.push([
                                            merchdata[key].head,
                                            merchdata[key].amount, false
                                        ]);
                                    }
                                }
                                return data;
                            }())
                        }
                    ]
                });

            } catch (e) {

                alert(e);
            }


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


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:Label ID="lblDate" runat="server" CssClass="control-label" Text="From"></asp:Label>
                                <asp:TextBox ID="txtDateFrom" runat="server" AutoCompleteType="Disabled" CssClass="form-control"
                                    ClientIDMode="Static"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDateFrom" Enabled="true"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lbltoDate" runat="server" CssClass="control-label" Text="To"></asp:Label>
                                <asp:TextBox ID="txtDateto" ClientIDMode="Static" runat="server" AutoCompleteType="Disabled" CssClass="form-control" ToolTip="(dd-MM-yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDateto_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDateto" Enabled="true"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="btnok" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-primary" OnClientClick="GetData();">OK</asp:LinkButton>
                            </div>
                        </div>

                        
                        <div class="col-md-2 margin-top30px">
                            <asp:Label runat="server" CssClass="control-label " Style="font-size: 16px; font-family: sans-serif; ">Taka In Lac</asp:Label>

                        </div>

                        <div class="col-md-1">
                            <a style="margin-top: 20px;" href="<%=this.ResolveUrl ("~/AllGraph")%>" target="_blank" class="btn btn-block btn-success">Go <span class="glyphicon glyphicon-dashboard"></span></a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-header">
                    <!-- .nav-tabs -->
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item">
                            <a class="nav-link active" data-toggle="tab" href="#Merchand">Merchandising</a>
                        </li>
                        
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#profile">Purchase</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#home2">Production</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show " data-toggle="tab" href="#home">Export</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#Accounts">Accounts</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#cashbalance">Cash & Bank</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#PendingPay">Pending Pay.</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#Stock">Stock</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#FutureFund">Future Fund</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#FutureCst">Future Cost</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link show" data-toggle="tab" href="#FuturefundvsCst">Fund VS Cost</a>
                        </li>


                    </ul>
                    <!-- /.nav-tabs -->
                </div>
                <!-- /.card-header -->

                <div class="card-body">
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane fade active show" id="Merchand">
                            <div class="row">

                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" NavigateUrl="~/F_01_Mer/OrderInformation" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblMerch">Merchandising</asp:HyperLink>

                                    <table id="Merchandtable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_01_Mer/RptMerChanInterface?Type=Merchan")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=01")%>" class="btn btn-primary">Module</a>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div id="Merchanddt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="home">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Visible="False" NavigateUrl="~/F_19_EXP/SalesInformation" Target="_blank" ID="lblsales" Style="font-size: 16px; color: #70737c; font-weight: bold;">Export</asp:HyperLink>

                                    <table id="sales" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_19_EXP/RptExportInterface")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_31_Mis/RptAllDashboard?Type=ExRelz")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=19")%>" class="btn btn-primary">Module</a>
                                    </div>

                                    <%--<div class="col-md-12" style="margin-top: 6px; margin-bottom: 6px;">
                                        <asp:HyperLink runat="server" NavigateUrl="~/F_16_Bill/RptBilligSummary" CssClass="btn btn-success btn-xs" target="_blank"  id="hlnkDetails" >Details</asp:HyperLink>
                                    </div>--%>
                                </div>
                                <div class="col-md-7">
                                    <div id="Salesdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="profile">
                            <div class="row">


                                <div class="col-md-5">
                                    <asp:HyperLink runat="server" NavigateUrl="~/F_10_Procur/PurInformation" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblpurchase">Purchase</asp:HyperLink>

                                    <table id="purchase" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_10_Procur/RptPurInterfaceLocal")%>" class="btn btn-primary">Local Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_09_Commer/RptLCInterface")%>" class="btn btn-danger">Import Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_31_Mis/RptAllDashboard?Type=Purchase")%>" class="btn btn-success">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=10")%>" class="btn btn-primary">Module</a>
                                    </div>
                                </div>

                                <div class="col-md-7">
                                    <div id="purchasedt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="home2">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:HyperLink runat="server" NavigateUrl="~/F_15_Pro/ProductionInfo" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblcons">Production</asp:HyperLink>

                                    <table id="construction" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_15_Pro/ProductionInterface?Type=FG")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_31_Mis/RptAllDashboard?Type=ProductionRMG")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=15")%>" class="btn btn-primary">Module</a>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div id="Consdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Accounts">
                            <div class="row">

                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Visible="False" NavigateUrl="~/F_23_MAcc/AccDashBoard" Target="_blank" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblaccount">Accounts</asp:HyperLink>

                                    <table id="AccTable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_23_MAcc/AccountInterface")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_31_Mis/RptAllDashboard?Type=Accounts")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=21")%>" class="btn btn-primary">Module</a>
                                        <div class=" btn-group" role="group" aria-label="Button group with nested dropdown">
                                            <button type="button" class="btn btn-danger">Ratio Analysis</button>
                                            <div class="btn-group" role="group">
                                                <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                    <div class="dropdown-arrow"></div>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="F_31_Mis/RatioAnaWithGraph?grp=01&comcod=" Target="_blank" CssClass="dropdown-item">Liquidety Ratio</asp:HyperLink>
                                                   <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="F_31_Mis/RatioAnaWithGraph?grp=02&comcod=" Target="_blank" CssClass="dropdown-item">Financial Leveragde</asp:HyperLink>
                                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="F_31_Mis/RatioAnaWithGraph?grp=03&comcod=" Target="_blank" CssClass="dropdown-item">Profitability Ratio</asp:HyperLink>
                                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="F_31_Mis/RatioAnaWithGraph?grp=04&comcod=" Target="_blank" CssClass="dropdown-item">Dividend Policey</asp:HyperLink>
                                                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="F_31_Mis/RatioAnaWithGraph?grp=05&comcod=" Target="_blank" CssClass="dropdown-item">Turn Over Ratio</asp:HyperLink>



                                                    <%--<asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Pro/PurInformation" CssClass="dropdown-item">Dashboard</asp:HyperLink>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-7">
                                    <div id="Accountsdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="cashbalance">
                            <div class="row">

                                <div class="col-md-5">


                                    <asp:HyperLink runat="server" NavigateUrl="~/F_21_GAcc/AccTrialBalance?Type=BankPosition" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblbbalance">Cash & Bank Balance</asp:HyperLink>


                                    <table id="cashbankbal" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_31_Mis/RptAllDashboard?Type=Accounts")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=21")%>" class="btn btn-primary">Module</a>
                                    </div>

                                </div>
                                <div class="col-md-7">
                                    <div id="bbalancedt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="PendingPay">
                            <div class="row">

                                <div class="col-md-6">
                                    <asp:HyperLink runat="server" NavigateUrl="~/F_15_DPayReg/RptPaymentGraph" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblbill">Pending Payments</asp:HyperLink>


                                    <table id="penbillpart" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>

                                    <div class="btn-group">
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface")%>" class="btn btn-primary">Interfaces</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/F_31_Mis/RptAllDashboard?Type=Accounts")%>" class="btn btn-danger">Analysis Graph</a>
                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=21")%>" class="btn btn-primary">Module</a>
                                    </div>

                                </div>
                                <div class="col-md-6">
                                    <div id="billdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>



                            </div>
                        </div>
                        <div class="tab-pane fade" id="Stock">
                            <div class="row">
                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblstock" ClientIDMode="Static">Stock</asp:HyperLink>


                                    <table id="stocktable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                    <br />
                                    <div class="btn-group">

                                        <a target="_blank" href="<%=this.ResolveUrl("~/StepofOperationNew?Module=17")%>" class="btn btn-primary">Module</a>
                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div id="stockdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="FutureFund">
                            <div class="row">

                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" NavigateUrl="~/" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblmanpower">Future Fund</asp:HyperLink>


                                    <table id="futuretable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div class="col-md-7">
                                    <div id="ffunddt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>


                            </div>
                        </div>
                        <div class="tab-pane fade" id="FutureCst">
                            <div class="row">


                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblfcost">Future Cost</asp:HyperLink>

                                    <table id="costtable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div class="col-md-7">
                                    <div id="fcostdt" style="width: 295px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>
                        <div class="tab-pane fade" id="FuturefundvsCst">
                            <div class="row">

                                <div class="col-md-5">

                                    <asp:HyperLink runat="server" Target="_blank" Visible="False" Style="font-size: 16px; color: #70737c; font-weight: bold;" ID="lblfunvscost">Future Fund VS Cost</asp:HyperLink>

                                    <table id="funvscosttable" class="table-striped table-hover table-bordered">
                                        <thead>
                                            <tr class="tblh">
                                                <th class="th1">Head</th>
                                                <th class="th2">Amount</th>
                                                <th class="th3">%</th>
                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                                <div class="col-md-7">
                                    <div id="fundcstdt" style="width: 480px; height: 220px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>



                        <div class="footer row">
                            <div class="splitter">
                            </div>
                            <div class="container-fluid">
                                <div class="row">
                                    <%--   <div class="col-sm-1 col-md-1 col-lg-1"></div>--%>
                                    <div class="col-sm-3 col-md-2 col-lg-3">
                                        <div style="margin-left: 25px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Accounts</h4>
                                            <ul class="Menulinks">
                                                <%--  <li> <a href="<%=this.ResolveUrl("~/F_99_Allinterface/AccountInterface")%>" target="_blank">Account Interface</a></li>--%>


                                                <li><a href="<%=this.ResolveUrl("~/F_21_GAcc/AccLedgerAll")%>" target="_blank">General Ledger</a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_21_GAcc/AccTrialBalance?Type=Mains")%>" target="_blank">Trial Balance</a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_21_GAcc/AllVoucherTopSheet")%>" target="_blank">Voucher 360</a></li>
                                                <li><a href="<%=this.ResolveUrl("~/ F_21_GAcc/RptAccDayTransData")%>" target="_blank">Daily Transection</a></li>

                                            </ul>

                                        </div>

                                    </div>
                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div style="margin-left: -30px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Purchase</h4>
                                            <ul class="Menulinks">
                                                <%--       <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/RptPurInterface")%>" target="_blank">Purchase Interface </a></li>--%>
                                                <li><a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur")%>" target="_blank">Day Wise Purchase </a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=PurSum")%>" target="_blank">Purchae Summary</a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_11_RawInv/RptIndProStock?Type=MatHis")%>" target="_blank">Purchase History -Mat Wise </a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=IndSup")%>" target="_blank">Purchase History -Sup Wise</a></li>

                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3">
                                        <div style="margin-left: 15px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Export </h4>
                                            <ul class="Menulinks">
                                                <%--<li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SalesInterface")%>" target="_blank">Sales Interface </a></li>--%>
                                                <li><a href="<%=this.ResolveUrl("~/F_03_CostABgd/RptOProVsShip?Type=OrdProVsShip&Module=Com")%>" target="_blank">Export, Production, Shipment </a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_19_EXP/DayWiseSalesEntry?Type=SalRep")%>" target="_blank">Day wise Sales Report </a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_19_EXP/SalesRealCertificate")%>" target="_blank">Sales Realization Certificate </a></li>


                                            </ul>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div style="margin-left: -30px" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Production</h4>
                                            <ul class="Menulinks">
                                                <%-- <li><a href="<%=this.ResolveUrl("~/F_99_Allinterface/SubContractorBillInterface")%>" target="_blank">Construction Interface</a></li>--%>

                                                <li><a href="<%=this.ResolveUrl("~/F_15_Pro/RptOrderProShipAll")%>" target="_blank">ORDER, PRODUCTION & SHIPMENT - ALL ORDERS</a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_03_CostABgd/RptLCStuatus?Type=PeriodicProdSt")%>" target="_blank">Periodic Production Status</a></li>

                                            </ul>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 col-md-2 col-lg-2">
                                        <div style="margin-left: -30px;" class="footerCol quickLink">
                                            <h4 style="color: #67D19A">Marchandising</h4>
                                            <ul class="Menulinks">
                                                <li><a href="<%=this.ResolveUrl("~/F_01_Mer/SampleInquiryLIst")%>" target="_blank">Sample Inquiry List</a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%>" target="_blank">BOM Approval List</a></li>
                                                <li><a href="<%=this.ResolveUrl("~/F_03_CostABgd/RptLCStuatus?Type=PeriodicOrderSt")%>" target="_blank">Periodic Order Status</a></li>


                                            </ul>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>

                <!-- End of Container-->
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

