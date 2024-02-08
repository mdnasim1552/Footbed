<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptPurInterfaceLocal.aspx.cs" Inherits="SPEWEB.F_10_Procur.RptPurInterfaceLocal" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Scripts/datepicker/bootstrap-datepicker/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="../Scripts/datepicker/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>

    <style type="text/css">
        .modal-dialog {
            margin: 44px auto;
            width: 100%;
        }

        .InBox {
            color: red !important;
        }

        .ServProdInfo .panel-body {
            padding: 0 5px 2px;
        }

        .ServProdInfo label {
            margin-bottom: 0;
        }


        .ServProdInfo .panel {
            margin-bottom: 5px;
        }

        .ServProdInfo .panel-heading {
            padding: 1px 15px;
            font-weight: bold;
            font-size: 16px;
        }

        .menuheading {
            font-size: 16px;
            color: darkcyan;
            padding-left: 10px;
            font-weight: bold;
        }


        .modal-title {
            font-weight: bold;
            color: #000;
        }

            .modal-title span {
                color: red;
            }


        .wrntLbl {
            float: right;
            width: 60%;
            background: #DFF0D8;
            border: 1px solid #DFF0D8;
        }

        .contentPart .ServProdInfo .form-group {
            overflow: hidden;
        }


        .OverAll {
            /*animation-name: example;
            animation-duration: 4s;
            animation-iteration-count: 5;*/
            /*font-size: 18px;*/
            color: black;
            font-size: 14px;
            text-align: center !important;
            margin-top: 0px;
        }


        ul.sidebarMenu {
            margin: 0;
            padding: 0;
            width: 115%;
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                border-bottom: 0;
            }

                ul.sidebarMenu li:last-child {
                    border-bottom: 1px solid #DFF0D8;
                }

                ul.sidebarMenu li a {
                    text-align: left;
                    display: block;
                    line-height: 30px;
                    font-size: 14px;
                    font-family: Calibri;
                }

                ul.sidebarMenu li h4 {
                    line-height: 50px;
                    text-align: center;
                    display: block;
                }

                ul.sidebarMenu li a:hover {
                    background: #D7E6D1;
                    color: black;
                }

        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                width: 155px;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #472AC6 !important;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            /*border: 2px solid #D1D735;*/
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }






        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 35px;
            margin: 1px 0;
            /*padding: 2px;*/
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                /*background: #12A5A6;*/
                /*color: #fff;*/
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background: #fff;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }

        .rptPurInt span.lbldata2 {
            background: #e5dcdd none repeat scroll 0 0;
            border: 1px solid #3ba8e0;
            display: block;
            font-size: 12px;
            line-height: 22px;
            margin: 14px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            background: #667DE8;
            color: #000000;
        }

        .lblactive label tr td {
            background: #667DE8 !important;
            color: #000 !important;
        }

        .blink_me {
            animation: blinker 5s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }


        .fan:nth-child(1) {
            background-color: #e6b0e1;
            color: #fff;
            height: 100%;
            line-height: 32px;
        }


        .fan {
            border-radius: 0;
            display: inline-block;
            float: left;
            font-size: 18px;
            padding: 8px;
        }

            .fan:nth-child(1) {
                background-color: #817E24;
                border-bottom: 2px solid red;
                /* border-top: 2px solid red; */
                /* border-left: 3px solid #4800ff; */
                color: #fff;
                height: 35px;
                line-height: 14px;
            }

            .fan:nth-child(2) {
            }

            .fan:nth-child(3) {
            }

            .fan:nth-child(4) {
            }

            .fan:nth-child(5) {
            }

            .fan:nth-child(6) {
            }

            .fan:nth-child(7) {
            }
        /* for interface*/

        .mix {
            background: #114c70;
        }


        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 86px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 15px;
            height: 36px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 36px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 18px;
            border-radius: 0px 15px;
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform: capitalize;
            font-size: 11px;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #CF4435;
        }

        .circle-tile-heading.purple:hover {
            background-color: #7F3D9B;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }

        .dark-blue {
            background-color: #34495E;
        }

        .dark-yal {
            background-color: #cc5959;
        }

        .green {
            background-color: #16A085;
        }

        .blue {
            background-color: #2980B9;
        }

        .orange {
            background-color: #F39C12;
        }

        .red {
            background-color: #E74C3C;
        }

        .purple {
            background-color: #8E44AD;
        }

        .dark-gray {
            background-color: #7F8C8D;
        }

        .gray {
            background-color: #95A5A6;
        }

        .light-gray {
            background-color: #BDC3C7;
        }

        .yellow {
            background-color: #F1C40F;
        }

        .text-dark-blue {
            color: #34495E;
        }

        .text-green {
            color: #16A085;
        }

        .text-blue {
            color: #2980B9;
        }

        .text-orange {
            color: #F39C12;
        }

        .text-red {
            color: #E74C3C;
        }

        .text-purple {
            color: #8E44AD;
        }

        .text-faded {
            color: rgba(255, 255, 255, 0.7);
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <%--<style type="text/css">
        .InBox {
            color: red !important;
        }



        .modal-title {
            font-weight: bold;
            color: #000;
        }

            .modal-title span {
                color: red;
            }

        .wrntLbl {
            float: right;
            width: 60%;
            background: #DFF0D8;
            border: 1px solid #DFF0D8;
        }

        .contentPart .ServProdInfo .form-group {
            overflow: hidden;
        }

        .OverAll {
            /*animation-name: example;
            animation-duration: 4s;
            animation-iteration-count: 5;*/
            /*font-size: 18px;*/
            color: black;
            font-size: 14px;
            text-align: center !important;
            margin-top: 0px;
        }


        ul.sidebarMenu {
            margin: 0;
            padding: 0;
            width: 115%;
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                border-bottom: 0;
            }

                ul.sidebarMenu li:last-child {
                    border-bottom: 1px solid #DFF0D8;
                }

                ul.sidebarMenu li a {
                    text-align: left;
                    display: block;
                    line-height: 30px;
                    font-size: 14px;
                    font-family: Calibri;
                }

                ul.sidebarMenu li h4 {
                    line-height: 50px;
                    text-align: center;
                    display: block;
                }

                ul.sidebarMenu li a:hover {
                    background: #D7E6D1;
                    color: black;
                }

        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                height: 50px;
                width: 90px;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    height: 50px;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #199698;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            height: 65px;
            width: 88px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 1px;
            color: #fff;
            text-align: center;
            border: 0px solid #D1D735;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }

            .tbMenuWrp table tr td:nth-child(1) {
                background: #3BA8E0;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(3) {
                background: #EFAD4D;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #769BF4;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #00CBF3;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
                display: none;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                background: #EFAD4D;
            }

            .tbMenuWrp table tr td:nth-child(10) {
                background: #769BF4;
            }

            .tbMenuWrp table tr td:nth-child(11) {
                background: #114c70;
            }

            .tbMenuWrp table tr td:nth-child(12) {
                background: #114c70;
            }

            .tbMenuWrp table tr td:nth-child(13) {
                background: #4BCF9E;
            }

        


        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 100%;
            margin: 1px 0;
            padding: 2px;
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                /*background: #12A5A6;*/
                /*color: #fff;*/
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background-color: #00ff21;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }

        .rptPurInt span.lbldata2 {
            background: #e5dcdd none repeat scroll 0 0;
            border: 1px solid #3ba8e0;
            display: block;
            font-size: 10px;
            line-height: 22px;
            margin: 14px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            /*background: #12A5A6;
            color: #fff;*/
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }

        .tbMenuWrp table tr td label {
            /*background:#4BCF9E;*/
        }

        .fan {
            border: 2px solid #f3b728;
            border-radius: 50%;
            display: inline-block;
            float: left;
            font-size: 18px;
            margin-top: 4px;
            padding: 2px;
        }

            .fan:nth-child(1) {
                background: #FF9C40 !important;
                color: #fff;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(2) {
                color: #E49015;
                background-color: #5EB75A !important;
            }

            .fan:nth-child(3) {
                color: #fff;
                background: #085407 !important;
            }

            .fan:nth-child(4) {
                color: #fff;
                background: #DA3F40 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(5) {
                color: #fff;
                background: #009BFF !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(6) {
                color: #E4DDDD;
                background: #539250 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(7) {
                color: #E4DDDD;
                background: #539250 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(10) {
                color: #E4DDDD;
                background: #000 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(11) {
                color: #E4DDDD;
                background: #000 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(12) {
                color: #E4DDDD;
                background: #539250 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #E4DDDD;
                background: #E79956 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #fff;
                background: #459A42 !important;
                border: 2px solid #E4DDDD;
            }





        .modalPopup {
            top: 25px !important;
            min-height: 400px;
            overflow: scroll;
        }

        .xmin-height {
            min-height: 200px;
        }

        .xDropdn {
            z-index: 5000 !important;
            position: absolute;
        }

        .btncn {
            height: 12px;
            width: 12px;
        }
    </style>--%>



    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            function loadModal() {
                $('#exampleModal3').modal('show');
            }

        });
        function openModalSuppl() {
            //    $('#myModal').modal('show');
            $('#modalSulierList').modal('toggle');
        }

        function CloseModal() {
            $('#modalSulierList').modal('hide');
        }
        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvReqInfo":
                    tblData = document.getElementById("<%=gvReqInfo.ClientID %>");
                    break;
                case "gvReqCheck":
                    tblData = document.getElementById("<%=gvReqCheck.ClientID %>");
                    break;
                case "gvReqAuth":
                    tblData = document.getElementById("<%=gvReqAuth.ClientID %>");
                    break;
                case "gvReqApproval":
                    tblData = document.getElementById("<%=gvReqApproval.ClientID %>");
                    break;
                case "gvCSCreate":
                    tblData = document.getElementById("<%=gvCSCreate.ClientID %>");
                    break;
                case "PnlCSChk":
                    tblData = document.getElementById("<%=PnlCSChk.ClientID %>");
                    break;
                case "gvRatePro":
                    tblData = document.getElementById("<%=gvRatePro.ClientID %>");
                    break;
                case "gvRateApp":
                    tblData = document.getElementById("<%=gvRateApp.ClientID %>");
                    break;
                case "gvOrdeProc":
                    tblData = document.getElementById("<%=gvOrdeProc.ClientID %>");
                    break;
                case "gvWrkOrd":
                    tblData = document.getElementById("<%=gvWrkOrd.ClientID %>");
                    break;
                case "grvMRec":
                    tblData = document.getElementById("<%=grvMRec.ClientID %>");
                    break;
                case "grvQC":
                    tblData = document.getElementById("<%=grvQC.ClientID %>");
                    break;
                case "gvStorRcv":
                    tblData = document.getElementById("<%=gvStorRcv.ClientID %>");
                    break;
                case "gvPurBill":
                    tblData = document.getElementById("<%=gvPurBill.ClientID %>");
                    break;
                case "grvComp":
                    tblData = document.getElementById("<%=grvComp.ClientID %>");
                    break;
                case "gvStorRcv":
                    tblData = document.getElementById("<%=gvStorRcv.ClientID %>");
                    break;
                    
            }


            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });



        };

        function LoadRdlcVIewer(comcod, orderno) {
            window.open(`../F_10_Procur/PuchasePrint?Type=OrderSavePdf&comcod=${comcod}&orderno=${orderno}`, '_blank');
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
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtfrmdate" runat="server" CssClass="form-control form-control-sm px-0" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm px-0" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="DdlSeason">Season</label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="DdlSupplier">Supplier</label>
                                <asp:DropDownList ID="DdlSupplier" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">BOM</label>
                                <asp:HyperLink ID="HyPBom" runat="server" CssClass=" form-control form-control-sm bg-twitter" Target="_blank"></asp:HyperLink>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <asp:LinkButton ID="btnSetup" runat="server" CssClass="margin-top30px btn btn-sm btn-success" OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                                 <asp:LinkButton ID="lnkInteface" runat="server" CssClass="margin-top30px btn-sm btn btn-secondary " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="lnkReports" runat="server" CssClass="margin-top30px btn-sm btn btn-warning" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                            <asp:HyperLink ID="lnkGoToLocal" runat="server" CssClass="margin-top30px btn btn-sm btn-primary" Target="_blank" NavigateUrl="~/F_09_Commer/RptLCInterface">Procurement (LC)</asp:HyperLink>

                        </div>
                        <div class="col-md-1">
                            <div class="margin-top30px btn-group btn-group-sm" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-sm btn-danger">Operations</button>
                                <div class="btn-group btn-group-sm" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HypStoreReq" runat="server" Target="_blank" NavigateUrl="~/F_13_CWare/PurReqEntry02?InputType=FxtAstEntry&actcode=&genno=" CssClass="dropdown-item">Create Requsition</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Pro/PurInformation" CssClass="dropdown-item">Dashboard</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_13_CWare/PurReqEntry02?InputType=MoldReqEntry&actcode=&genno=" CssClass="dropdown-item">Mold Requisition</asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">
                    <asp:Panel ID="pnlInterf" runat="server">
                        <div id="slSt" class=" col-md-12 ServProdInfo">
                            <div class="panel with-nav-tabs panel-primary xmin-height">
                                <fieldset class="tabMenu">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="tbMenuWrp nav nav-tabs">
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="0"></asp:ListItem>
                                                    <asp:ListItem Value="1"></asp:ListItem>
                                                    <asp:ListItem Value="2"></asp:ListItem>

                                                    <asp:ListItem Value="3"></asp:ListItem>
                                                    <asp:ListItem Value="4"></asp:ListItem>
                                                    <asp:ListItem Value="5"></asp:ListItem>

                                                    <asp:ListItem Value="6"></asp:ListItem>
                                                    <asp:ListItem Value="7"></asp:ListItem>
                                                    <asp:ListItem Value="8"></asp:ListItem>
                                                    <asp:ListItem Value="9"></asp:ListItem>
                                                    <asp:ListItem Value="10"></asp:ListItem>
                                                    <asp:ListItem Value="11"></asp:ListItem>
                                                    <asp:ListItem Value="12"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <div>
                                    <asp:Panel ID="pnlReqInfo" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvReqInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvReqInfo_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="170px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvdptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReq" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req No" onkeyup="Search_Gridview(this,4, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="hlnkgvgvmrfno" runat="server" BorderStyle="none"
                                                                    Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                                </asp:HyperLink>


                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrsirdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ")%>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchApMat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Amount" onkeyup="Search_Gridview(this,8, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Curent Status">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCUSt" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Curent Status" onkeyup="Search_Gridview(this,9, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchusername" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="User Name" onkeyup="Search_Gridview(this,10, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,11, 'gvReqInfo')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>


                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>




                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlReqCheck" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 450px">

                                                <asp:GridView ID="gvReqCheck" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvReqCheck_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvappatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="220px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvReqCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdvdptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvReqCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvapreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="100px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req No" onkeyup="Search_Gridview(this,4, 'gvReqCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMrf" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvReqCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="hlnkgvapmrfno" runat="server" BorderStyle="none"
                                                                    Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px">
                                      
                                                                </asp:HyperLink>


                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvappactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvaprqrsirdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvlblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ")%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchApAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Amount" onkeyup="Search_Gridview(this,8, 'gvReqCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvapFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvReqCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HypApproval" runat="server" Target="_blank" ForeColor="Green"><span class="fa fa-check "></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="gvHyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnUpdate" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black"><span class="fa fa-edit"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="gvreqcheckDelete" OnClientClick="return confirm('ALERT!! this Requistion Delete Permanently from ERP. Do you want to delete?');" runat="server" OnClick="gvreqcheckDelete_Click"><span style="color:red;"  class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlReqAuth" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvReqAuth" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvReqAuth_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapreqno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvappatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvReqAuth')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvdsdptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvReqAuth')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvapreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="100px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req No" onkeyup="Search_Gridview(this,4, 'gvReqAuth')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapreqno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMrf" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvReqAuth')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="hlnkgvapmrfno" runat="server" BorderStyle="none"
                                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px">
                                      
                                                                </asp:HyperLink>


                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvappactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvaprqrsirdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvlblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);")%>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchApAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Amount" onkeyup="Search_Gridview(this,8, 'gvReqAuth')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvapFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvReqAuth')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HypApproval" runat="server" Target="_blank"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <%--  <ItemStyle Width="120px" />--%>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="gvHyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="gvaprbtnDelReqChk" runat="server" OnClick="gvaprbtnDelReqChk_Click"><span style="color:red;"  class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="PnlReqApproval" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvReqApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvReqApproval_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvappatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvReqApproval')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdadptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Req Date" onkeyup="Search_Gridview(this,3, 'gvReqApproval')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvapreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="100px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req No" onkeyup="Search_Gridview(this,4, 'gvReqApproval')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvReqApproval')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="hlnkgvapmrfno" runat="server" BorderStyle="none"
                                                                    Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                                </asp:HyperLink>


                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvappactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvaprqrsirdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="gvlblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0) ;")%>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchTAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Amount" onkeyup="Search_Gridview(this,8, 'gvReqApproval')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvapApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvapFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvReqApproval')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HypApproval" runat="server" Target="_blank"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="gvHyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelReqRev" runat="server" OnClick="btnDelReqRev_Click"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PnlCSCreate" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">
                                                <asp:GridView ID="gvCSCreate" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvCSCreate_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvCSCreate')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldgdadptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvCSCreate')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReqno" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req. No" onkeyup="Search_Gridview(this,4, 'gvCSCreate')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvCSCreate')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ")%>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchtAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Amount" onkeyup="Search_Gridview(this,8, 'gvCSCreate')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <%--apamt--%>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvCSCreate')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelReqApp" runat="server" OnClick="btnDelReqApp_Click"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                    <asp:Panel ID="PnlCSChk" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">
                                                <asp:GridView ID="gvCsCheck" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvCsCheck_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvCsCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="dlblgdadptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvCsCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText=" Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReqno" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req. No" onkeyup="Search_Gridview(this,4, 'gvCsCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvCsCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ")%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchtAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Amount" onkeyup="Search_Gridview(this,8, 'gvCsCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvCsCheck')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>

                                                                <%-- <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>--%>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />


                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnCSRev" runat="server" OnClick="btnCSRev_Click"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="pnlRatePro" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">
                                                <asp:GridView ID="gvRatePro" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvRatePro_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvRatePro')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdadptdescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvRatePro')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req. No" onkeyup="Search_Gridview(this,4, 'gvRatePro')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvRatePro')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <%--  <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ")%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchtAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Amount" onkeyup="Search_Gridview(this,8, 'gvRatePro')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvRatePro')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>

                                                                <%--   <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelCSNext" runat="server" OnClick="btnDelCSNext_Click"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>




                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="pnlRateApp" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvRateApp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvRateApp_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbdlgdadptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req <br>  Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText=" Req. No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="450px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total</br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpreqty" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0); ")%>' Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvRateApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>

                                                                <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>--%>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnCSchk" OnClick="btnCSchk_Click" runat="server"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>




                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>


                                    </asp:Panel>

                                    <asp:Panel ID="PanelPurchaseType" Visible="false" runat="server">

                                        <div class="table-responsive">

                                            <asp:GridView ID="gvOrdeProc" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvOrdeProc_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvorderno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Store Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvOrdeProc')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsuppliermrr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order <br>  Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvorderdat" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcustompon" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon"))%>' Width="100px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvorderno1" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req. No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchReq" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req. No" onkeyup="Search_Gridview(this,6, 'gvOrdeProc')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Mrf No.">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchMrf" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Mrf No." onkeyup="Search_Gridview(this,7, 'gvOrdeProc')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactcode" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Purchase Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgpurdesc" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc")) %>'
                                                                Width="50px"></asp:Label>

                                                            <asp:Label ID="lblpurtype" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purtype")) %>'
                                                                Width="50px"></asp:Label>

                                                            <asp:LinkButton ID="btnSupplier" runat="server" Enabled="false" OnClick="btnSupplier_Click" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc")) %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcount" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvorderqtsy" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvWoamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFWoamt" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Season" onkeyup="Search_Gridview(this,13, 'gvOrdeProc')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PO">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" runat="server" ToolTip="PO Print" Target="_blank"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInReqPrint" runat="server" ToolTip="Req Print" Target="_blank"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CS">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyCSPrint" runat="server" CssClass="text-primary" ToolTip="CS Print" Target="_blank"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelPoApp" runat="server" OnClick="btnDelPoApp_Click" ToolTip="Delete"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ToolTip="PO Edit"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ToolTip="Approved"><span class="fa fa-check-circle"></span> 
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtngvOrdeProcSendMail" runat="server" OnClick="lnkBtngvOrdeProcSendMail_Click" ToolTip="Send Mail"><span class="fa fa-mail-bulk"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dpt. Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,13, 'gvOrdeProc')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgdadptdescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                            </asp:GridView>

                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="PaneWorder" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvWrkOrd" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvWrkOrd_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvPoreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                                <asp:Label ID="lgvpoPactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                                <asp:Label ID="lgvbblccode" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblccode"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPOgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvWrkOrd')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvadptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Supplier" onkeyup="Search_Gridview(this,3, 'gvWrkOrd')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPOsupplier" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BBLC Description" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPObblcdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order <br>  Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvaprovdat1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchRq" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req. No" Style="text-align: center" onkeyup="Search_Gridview(this,5, 'gvWrkOrd')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPOreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No." Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPOmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPOResCount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblwogvApamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "woamt")).ToString("#,##0;(#,##0);") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFOrProAmt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Purchase Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpurtype" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purtype"))%>'></asp:Label>

                                                                <asp:Label ID="lblgvpurdesc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc"))%>' Width="40px"></asp:Label>


                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Season" Style="text-align: center" onkeyup="Search_Gridview(this,9, 'gvWrkOrd')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HypLnkPrintReq" runat="server" CssClass="text-primary" Target="_blank" ToolTip="Req Print"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CS">
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ToolTip="CS Print"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>

                                                                <%--  <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                    </asp:HyperLink>--%>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ToolTip="PO Entry"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelOrProc" OnClick="btnDelOrProc_Click" runat="server" ToolTip="Delete"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PanelRecv" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="grvMRec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="grvMRec_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'grvMRec')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvsuppliermrr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order <br>  Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvorderdat" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderdat1")) %>'
                                                                    Width="90px"></asp:Label>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcustompon" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon"))%>' Width="100px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchReq" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req. No" onkeyup="Search_Gridview(this,5, 'grvMRec')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMrf" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Mrf No." onkeyup="Search_Gridview(this,6, 'grvMRec')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchase Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgpurdesc" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc")) %>'
                                                                    Width="50px"></asp:Label>

                                                                <asp:Label ID="lblpurtype" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purtype")) %>'
                                                                    Width="50px"></asp:Label>


                                                                <asp:LinkButton ID="btnSupplier" runat="server" Enabled="false" OnClick="btnSupplier_Click" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc")) %>'></asp:LinkButton>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemcount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderqtsy" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bal.Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderqty" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvWoamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFWoamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,13, 'grvMRec')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <div class="btn-group" role="group" aria-label="Button group with nested dropdown">

                                                                    <div class="btn-group" role="group">
                                                                        <button id="btnGroupDrop4" type="button" class=" dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                                            <div class="dropdown-arrow"></div>
                                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" CssClass="dropdown-item"><span class="fa fa-print"></span> Print
                                                                            </asp:HyperLink>

                                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="dropdown-item"><span class="fa fa-check"></span>Rec Entry
                                                                            </asp:HyperLink>
                                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" CssClass="dropdown-item"><span class="fa fa-edit"></span>PO Edit
                                                                            </asp:HyperLink>
                                                                            <asp:LinkButton ID="btnDelRec" OnClick="btnDelRec_Click" runat="server" CssClass="dropdown-item"><span style="color:red" class="fa fa-trash-alt"></span>Delete </asp:LinkButton>
                                                                            <asp:LinkButton OnClick="lnkCheck_Click" OnClientClick="return confirm('Do You Agree to Arrival?')" ID="lnkCheck" CssClass="dropdown-item" runat="server" ToolTip="PO Arrival"><span class="fa fa-check"></span></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>




                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,13, 'grvMRec')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdadptdescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PanelQc" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-refgsponsive col-lg-12" style="min-height: 450px">

                                                <asp:GridView ID="grvQC" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="grvQC_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                            <ItemTemplate>
                                                                lblgvreqno
                                                                     <asp:Label ID="lblQCgvreqno" runat="server" Style="text-align: right"
                                                                         Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>

                                                                <asp:Label ID="lblqcgvorderno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'grvQC')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdadptdesdc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supplier">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Supplier" onkeyup="Search_Gridview(this,3, 'grvQC')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvsuppliermrr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Order <br>  Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvorderdat" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receved No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="80px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText=" Req. No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchMrf" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Mrf No." onkeyup="Search_Gridview(this,7, 'grvQC')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="MR Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvqcmrqty" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MR Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvqcmrqamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvQCFWoamtdda" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upto Recived">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpurqcqtyupto" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready QC Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvchlnqtyqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqcqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="Label3" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ready QC Amount" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvWoamtacamtQc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqcamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvQCFWoamtda" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchase <br> Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblqctypecode" runat="server" Visible="false" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purtype")) %>'></asp:Label>
                                                                <asp:Label ID="lblgvQctypepur" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,15, 'grvQC')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>


                                                                <div class="btn-group" role="group" aria-label="Button group with nested dropdown">

                                                                    <div class="btn-group" role="group">
                                                                        <button id="btnGroupDrop4" type="button" class="dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                                        <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                                            <div class="dropdown-arrow"></div>
                                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" CssClass="dropdown-item"><span class="fa fa-print"></span> Print
                                                                            </asp:HyperLink>

                                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" CssClass="dropdown-item"><span class="fa fa-check"></span> QC Entry
                                                                            </asp:HyperLink>
                                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" CssClass="dropdown-item"><span class="fa fa-edit"></span> REC Edit
                                                                            </asp:HyperLink>
                                                                            <%--   <asp:LinkButton ID="btnDelRec" OnClick="btnDelRec_Click" runat="server" CssClass="dropdown-item" ><span style="color:red" class="fa fa-trash-alt"></span>Delete </asp:LinkButton>--%>
                                                                            <asp:LinkButton ID="btnDelexitsRecv" OnClick="btnDelexitsRecv_Click" runat="server" CssClass="dropdown-item"><span style="color:red" class="fa fa-trash-alt"></span> Delete</asp:LinkButton>

                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </ItemTemplate>
                                                            <%-- <ItemStyle Width="50px" />--%>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>

                                                <div class="clearfix"></div>

                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PanelStoreRcv" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvStorRcv" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvStorRcv_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="SL.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqnoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>

                                                                <asp:Label ID="lblgbPrqcno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcno"))%>' Width="15px"></asp:Label>

                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblgvmrrnoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvordernoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="160px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdadptdescq" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="110px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Supplier" onkeyup="Search_Gridview(this, 3, 'gvStorRcv')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvsupplierbillqc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="140px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Recevied <br>  Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvmrrdat" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QC No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrrno1qc" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcno1"))%>' Width="90px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1qc" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfnoqc" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="Challan">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvChlln" runat="server" Style="text-align: Center"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>--%>


                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcodeqc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemqccount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpurqcty" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvqcamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFmrramt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchase <br> Type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblqctyspecode" runat="server" Visible="false" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purtype")) %>'></asp:Label>
                                                                <asp:Label ID="lblgvdQctypepur" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" Style="text-align: Center" runat="server" Width="80px" placeholder="Season" onkeyup="Search_Gridview(this,15, 'gvStorRcv')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>


                                                                <%--  <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                    </asp:HyperLink>--%>

                                                                <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank"><span class="fa fa-edit"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="btnDelQC" OnClick="btnDelQC_Click" OnClientClick="return confirm('Do you want to delete this item?');" runat="server"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PanelBill" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvPurBill" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvPurBill_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqnoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>

                                                                <asp:Label ID="lblgbPrqcno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcno"))%>' Width="15px"></asp:Label>

                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblgvmrrnoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvordernoqc" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="140px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdadptdesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="110px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Supplier">
                                                             <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSupp" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Supplier" onkeyup="Search_Gridview(this,3, 'gvPurBill')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvsupplierbillqc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Recevied <br>  Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvmrrdat" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcdat1")) %>'
                                                                    Width="90px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QC No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrrno1qc" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcno1"))%>' Width="80px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Req. No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1qc" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="100px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Challan No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearChallno" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Challan No" onkeyup="Search_Gridview(this,8, 'gvPurBill')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvchlnno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfnoqc" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <%-- <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcodeqc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemqccount" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpurqcty" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvqcamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcamt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFmrramt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchase <br> Type" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblqctyspecode" runat="server" Visible="false" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purtype")) %>'></asp:Label>
                                                                <asp:Label ID="lblgvdQctypepur" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purdesc")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,17, 'gvPurBill')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <%--<asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>--%>

                                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank"><span class="fa fa-edit"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelSRcv" OnClick="btnDelSRcv_Click" OnClientClick="return confirm('Do you want to delete this item?');" runat="server"><span style="color:red" class="fa fa-trash-alt"></span> </asp:LinkButton>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PanelComp" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="grvComp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="grvComp_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>


                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="mrrno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrrno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderno" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Dpt. Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgdadptdesc4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Bill <br>  Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkgvmrrdat" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdat1")) %>'
                                                                    Width="70px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRR No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrrno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="80px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvorderno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1"))%>' Width="80px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText=" Req. No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Mrf No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="90px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <%--     <asp:TemplateField HeaderText="Resource Description" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server" Font-Bold="True"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc"))%>' Width="430px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>



                                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrsirdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcount"))%>' Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbillamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0);") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFmrramt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Curent Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                </asp:HyperLink>





                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btndel" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                    <FooterStyle CssClass="grvFooter" />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                    <div class="clearfix"></div>
                                </div>
                            </div>

                        </div>

                    </asp:Panel>

                    <asp:Panel ID="PnlSalesSetup" runat="server" Visible="false">
                        <div class="form-group">

                            <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                <ul class="list-icons mb-3 colMid " id="SERV2">
                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccResourceCode?Type=Matcode")%> " target="_blank">01. Material Opening Code</a>
                                    </li>
                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_34_Mgt/SalesCodeBook?Type=All")%> " target="_blank">02. General Code Book</a>
                                    </li>
                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_09_Commer/PurMktSurvey?Type=SurveyLink")%> " target="_blank">03. Survey Link</a>
                                    </li>

                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint")%> " target="_blank">04. Details Code</a>
                                    </li>

                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_09_Commer/PurSupplierinfo")%> " target="_blank">05. Supplier Information</a>
                                    </li>

                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccSubCodeBook?InputType=SupplierCode")%> " target="_blank">06. Supplier Code</a>
                                    </li>

                                </ul>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlPurchase" runat="server" CssClass="row" Visible="false">

                        <div class="col-md-4">
                            <ul class="list-unstyled" id="SERV">
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur")%> " target="_blank">01. Day Wise Purchase</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=PurSum")%> " target="_blank">02. Purchase Summary</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_10_Procur/RptMatPurHistory")%> " target="_blank">03. Purchase History-Materials Wise</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=IndSup")%> " target="_blank">04. Purchase History-Supplier Wise</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_11_Pro/RptWorkOrderVsSupply?Type=OrdVsSup")%> " target="_blank">05. Work Order-Supplier Wise</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk")%> " target="_blank">06. Purchase Tracking</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_11_Pro/RptWorkOrderVsSupply?Type=OrderTk")%> " target="_blank">07. Order Tracking</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=PurBilltk")%> " target="_blank">08. Bill Tracking</a>

                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_11_Pro/RptDateWiseReq?Type=PendingStatus")%> " target="_blank">09. Pending Status</a>
                                </li>
                            </ul>
                        </div>

                        <div class="col-md-4">
                            <ul class="list-unstyled">
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_15_Acc/RptAccSpLedger?Type=ASPayment")%> " target="_blank">10. Supplier Overall Position</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%> " target="_blank">11. BOM Approved List</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptOrderVsReceive?Type=OrderVsRec")%> " target="_blank">12. BOM VS Received</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_05_ProShip/RptOrderStatus?Type=OrdStatus")%> " target="_blank">13. Order Status Report</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptWorkOrderVsSupply?Type=SeasonSummary")%> " target="_blank">14. Season Wise Supply Summary</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptSeasonWiseOrder?Type=SeasonBySeason")%> " target="_blank">15. Season By Season Suppliers' Summary</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptWorkOrderVsSupply?Type=LeadTime")%> " target="_blank">16. Raw Materials Supply Lead Time </a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptSeasonWiseOrder?Type=SeasonOverview")%> " target="_blank">17. Season Overview Of Materials </a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccResourceCode?Type=MatPriceSumm")%> " target="_blank">18. Material Price Summary</a>
                                </li>
                            </ul>
                        </div>

                        <div class="col-md-4">
                            <ul class="list-unstyled">
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptSeasonWiseOrder?Type=PriceVariance")%> " target="_blank">19. Material Price Variance Report</a>
                                </li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_05_ProShip/RptOrderStatus?Type=OrdStatus")%> " target="_blank">20. Order Status Report</a>
                                </li>

                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptOrderVsReceive?Type=BomMatSummary")%> " target="_blank">21. BOM Wise Materials Summary</a>
                                </li>
                            </ul>
                        </div>

                    </asp:Panel>


                    <div id="modalSulierList" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-md">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">

                                    <h4 class="modal-title">Select Supplier</h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="lblpurcode" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblReqno" runat="server" Visible="false"></asp:Label>

                                        <asp:DropDownList ID="ddlSup" runat="server" CssClass="chzn-select form-control" Width="450px" TabIndex="3"></asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="" ControlToValidate="ddlSup" ErrorMessage="Supplier required" ForeColor="Red" Display="Dynamic" CssClass="ValidationError"
                                            Text="*Supplier required"
                                            ValidationGroup="FormValiCheck"> 
                                        </asp:RequiredFieldValidator>


                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="modal-footer">


                                    <asp:HyperLink ID="hypSuplcode" runat="server" class="btn btn-default" NavigateUrl="~/F_21_GAcc/AccSubCodeBook?InputType=SupplierCode" Target="_blank">New Supplier Add</asp:HyperLink>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    <asp:LinkButton ID="lnkSupllerupdate" runat="server" OnClick="lnkSupllerupdate_Click" Class="btn btn-primary" ValidationGroup="FormValiCheck">Save</asp:LinkButton>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>


            <script>
                jQuery(document).ready(function ($) {
                    $('.counter').counterUp({
                        delay: 10,
                        time: 1000
                    });
                });
            </script>
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

