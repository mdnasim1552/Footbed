<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SPE.Master" CodeBehind="ProductionInterfaceSemi.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProductionInterfaceSemi" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .InBox {
            color: red !important;
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
            px display: inline-block;
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

        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 117px;
            font-size: 11px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 15px;
            height: 39px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
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
            text-transform: uppercase;
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
    </style>

    <%--<style type="text/css">
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
                height: 35px;
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
            background: #472AC6 !important;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            height: 35px;
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

            .tbMenuWrp table tr td:nth-child(1) {
                background: #4BCF9E;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #92D14F;
            }

            .tbMenuWrp table tr td:nth-child(3) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #92D14F;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #00AF50;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #E6A549;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #71A3E4;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #ffffff;
            }

        /*.tbMenuWrp table tr td:nth-child(7) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                width: 115px;
                padding: 0 3px;
            }*/


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
            px display: inline-block;
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
    </style>--%>

    <script type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


        };

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer2" runat="server" OnTick="Timer1_Tick" Interval="10000">
            </asp:Timer>

            <triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </triggers>
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
                        <div class="form-group col-md-1">
                            <asp:Label ID="Label1" runat="server" CssClass="col-form-label text-dark">From</asp:Label>
                            <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFDate_CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                        </div>

                        <div class="form-group col-md-1">
                            <asp:Label ID="lbldate" runat="server" CssClass=" col-form-label"> To</asp:Label>
                            <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                        </div>

                        <div class="form-group col-md-1">
                            <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-top: 20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                        <div class="form-group col-md-3">
                            <asp:LinkButton ID="LbtnSetting" runat="server" OnClick="LbtnSetting_Click" CssClass="btn btn-success btn-sm" Style="margin-top: 20px;">Settings</asp:LinkButton>
                            <asp:LinkButton ID="LbtnInt" runat="server" OnClick="LbtnInt_Click" CssClass="btn btn-secondary btn-sm" Style="margin-top: 20px;">Interface</asp:LinkButton>
                            <asp:LinkButton ID="LbtnRep" runat="server" OnClick="LbtnRep_Click" CssClass="btn btn-warning btn-sm" Style="margin-top: 20px;">All Reports</asp:LinkButton>
                        </div>
                        <div class="form-group col-md-2">
                            <asp:HyperLink ID="HyperLink3" NavigateUrl="~/F_03_CostABgd/ProdBudget?Type=EntrySemi" runat="server" Target="_blank" CssClass="btn btn-success btn-sm" Style="margin-top: 20px;">Production Budget</asp:HyperLink>
                        </div>

                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <asp:Panel ID="PnlInt" runat="server" CssClass="card-body" Visible="false">
                    <div id="slSt" class=" col-md-12">
                        <div class="panel with-nav-tabs panel-primary">
                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="tbMenuWrp nav nav-tabs">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0"></asp:ListItem>
                                                <asp:ListItem Value="1"></asp:ListItem>
                                                <asp:ListItem Value="2"></asp:ListItem>
                                                <asp:ListItem Value="3" style="display: none"></asp:ListItem>
                                                <asp:ListItem Value="4"></asp:ListItem>
                                                <asp:ListItem Value="5"></asp:ListItem>
                                                <asp:ListItem Value="6"></asp:ListItem>
                                                <asp:ListItem Value="7"></asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div>

                                <asp:Panel ID="pnlallProd" runat="server" Visible="false">
                                    <div class="row mt-3">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="gvProdInfo" runat="server" OnRowDataBound="gvProdInfo_RowDataBound" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Budget </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno1")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bgddat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right" Width="120px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Budgeted</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted</br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="150px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                           

                                                            <asp:LinkButton ID="LbtnRem" runat="server" OnClientClick="return Confirm();" OnClick="LbtnRem_Click" CssClass="mr-2" Style="color: red;"><i class="fa fa-trash"></i>
                                                            </asp:LinkButton>

                                                            <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank"><i class="fa fa-print"></i>
                                                            </asp:HyperLink>


                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="pnlReq" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12">
                                            <asp:GridView ID="grvProReq" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvProReq_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Budget </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bgddat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Budgeted</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted</br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budget</br> Balance">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdbal" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdbal")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQbgdbal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Current Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpbmststus" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmststus")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>
                                                         
                                                             <asp:LinkButton ID="LbtnRem" runat="server" Visible="false" OnClientClick="return Confirm();"  CssClass="mr-2" Style="color: red;"><i class="fa fa-trash"></i>
                                                            </asp:LinkButton>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </asp:Panel>

                                <asp:Panel ID="PanelIssue" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvProIssue" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvProIssue_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budget </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req. </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpreqno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvtitemcount" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trescount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="LbtnRem" runat="server" Visible="false" OnClientClick="return Confirm();" Style="padding: 0px" CssClass="btn btn-xs btn-danger"><span class="glyphicon glyphicon-remove"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="pnlProProcs" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                        </div>
                                    </div>


                                </asp:Panel>

                                <asp:Panel ID="PnlProduction" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="grvProdtion" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvProdtion_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Budget </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                 
                                                    <asp:TemplateField HeaderText="Batch COde" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchcode" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchcode")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Actual</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvacqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Production</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbalqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Production</br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Current Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpbmststus" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proatatus")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="LbtnRem" runat="server" Visible="false" OnClientClick="return Confirm();" Style="padding: 0px" ><span class="fa fa-recycle"></span>
                                                            </asp:LinkButton>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton OnClick="lbtnProdCOmplete_Click" ID="lbtnProdCOmplete" OnClientClick="return confirm('Are you sure you want to Complete this Production?');" runat="server" CssClass="btn btn-xs btn-success"><span class="glyphicon glyphicon-saved"></span>Complete</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <table style="width: 20px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkall_CheckedChanged" /></td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkack" runat="server" Width="20px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlQC" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="grvQCEntry" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvQCEntry_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Budget </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Production </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgprodid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodid")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Batch Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Production</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbalqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Production</br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpStatus" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pstatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="lnkbtnRemove" OnClientClick="return confirm();" OnClick="lnkbtnRemove_Click" runat="server" Target="" ForeColor="Red" Font-Underline="false"><span class="fa fa-recycle"></span>
                                                            </asp:LinkButton>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnlStrec" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="gvstorec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvstorec_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwipid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "production")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Receive </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorproid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodid")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receive Date ">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorprodat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QC </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorgrrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grrno")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Batch Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstrbatchdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstritemcount" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblstrAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Receive</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstrproqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblstrTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receive</br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstramt" Width="120px" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrcvtype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvtype")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton OnClientClick="return confirm('Are you sure you want to delete this item?');" ID="lnkbtnRemoveStr" OnClick="lnkbtnRemoveStr_Click" runat="server" Target="" ForeColor="Red" Font-Underline="false"><span class="fa fa-recycle"></span>
                                                            </asp:LinkButton>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnlComp" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="grvComp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Budget </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "bgddat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Name" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                                Width="350px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Budgeted</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Budgeted</br> Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Current Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpbmststus" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pbmststus")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="LbtnRem" runat="server" Visible="false" OnClientClick="return Confirm();" Style="padding: 0px" CssClass="btn btn-xs btn-danger"><span class="glyphicon glyphicon-remove"></span>
                                                            </asp:LinkButton>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>


                            </div>
                        </div>

                    </div>
                </asp:Panel>
                <asp:Panel ID="PnlSet" runat="server" CssClass="card-body" Visible="false">
                    <asp:Panel ID="PnlSetFG" runat="server" CssClass="row" Visible="false">

                        <div class="row">

                            <div class="col-md-6">

                                <ul class="list-unstyled" id="SERV">

                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/MiniStockInput")%> " target="_blank">01. Stock Label Input-Materials</a>
                                    </li>
                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/MiniProStockInput")%> " target="_blank">02. Stock Label Input-Products</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_03_StdCost/StdCostSheet?InputType=CostAnnaSemi&actcode=")%> " target="_blank">02. Analysis sheet</a>
                                    </li>

                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_03_StdCost/MatAvailability?Type=FG")%> " target="_blank">04. Materials Availability</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_03_StdCost/ProdBudget?Type=Entry")%> " target="_blank">05. Production Budget </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_11_Pro/ProdReq?Type=Entry&genno=")%> " target="_blank">06. Production Requisition</a>
                                    </li>

                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/PBMatIssueSingle?Type=Entry&actcode=&genno=")%> " target="_blank">07. Material Issue </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionPlan?Type=Entry&actcode=")%> " target="_blank">08. Production Entry </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_19_FGInv/ProQCEntry?Type=Entry&genno=")%> " target="_blank">09. QC Check </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_19_FGInv/ProStoreReceive?Type=Entry&genno=")%> " target="_blank">10. Store Received </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_19_FGInv/ProStoreReceive?Type=EntryRej&genno=")%> " target="_blank">11. Store Received (Rejection)</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/ReProductionEntry?Type=Entry&genno=")%> " target="_blank">12. Re-Production Request</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/MatReIssue?Type=EntryFG")%> " target="_blank">13. Materials Re-issue</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/DateWiseProdEstimat?Type=FG")%> " target="_blank">14. Date Wise Production Estimat</a>
                                    </li>
                                </ul>

                            </div>
                          
                        </div>


                    </asp:Panel>
                    <asp:Panel ID="PnlSetSemiFg" runat="server" CssClass="col-md-12"  Visible="false">

                        <div class="row">

                            <div class="col-md-6">

                                <ul class="list-unstyled" id="SERV">


                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_03_CostABgd/StdCostSheet?InputType=CostAnnaSemi&actcode=")%> " target="_blank">02. Analysis sheet</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Pro/MatAvailability?Type=SemiFG")%> " target="_blank">03. Materials Availability-Semi FG</a>
                                    </li>

                                    
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Pro/ProStoreReceive?Type=EntrySemiRej&genno=")%> " target="_blank">12. Store Received (Rejection)</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Pro/ReProductionEntry?Type=EntrySemi&genno=")%> " target="_blank">13. Re-Production Request</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/MatReIssue?Type=EntrySemiFG")%> " target="_blank">14. Materials Re-issue</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Pro/MatRetInWIP?Type=Entry")%> " target="_blank">15. Materials Return in WIP</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Pro/MatRetInWIP?Type=Approved")%> " target="_blank">16. Materials Return Approved in WIP</a>
                                    </li>
                                </ul>

                            </div>
                           
                        </div>


                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="PnlRep" runat="server" Visible="false">
                    <asp:Panel ID="PnlRepFg" runat="server" Visible="false">
                        <div class="row">

                             <div class="col-md-6">

                                <ul class="list-unstyled" id="SERV">
                                    <li class="menuheading">
                                        <span class="">A.Raw Materials</span>
                                    </li>
                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptCentralStore?InputType=General")%> " target="_blank">01. Inventory Report-General</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptCentralStore?InputType=QuantityB")%> " target="_blank">02.  Inventory Report-Quantity Basis</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_23_SaM/RptIndProStock?Type=MatHis")%> " target="_blank">03.   Materials History</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptDaywiseMatIssue2?Type=FG")%> " target="_blank">04.  Day Wise Materials Issue </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptDaywiseMatIssue2?Type=FGQty")%> " target="_blank">05.  Day Wise Materials Issue(Qty) </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_03_StdCost/MatAvailability?Type=FG")%> " target="_blank">06.  Materials Availability </a>
                                    </li>
                                    <li class="menuheading">
                                        <span>B.Production</span>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProdLoss?Type=FG")%> " target="_blank">01.  Production Loss Report </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProdLoss?Type=FGqty")%> " target="_blank">02.  Production Loss (Qty) </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/ProductionReport")%> " target="_blank">03.  Day wise production report </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptDaywiseHourlyProd?Type=DayProd")%> " target="_blank">04. Day Wise Hourly Production </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptDaywiseHourlyProd?Type=DayQcRej")%> " target="_blank">05. Day Wise Hourly QC </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_19_FGInv/RptStockReport02?Type=ValueMFG")%> " target="_blank">06. Daily Stock Summary Qty</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptDaywiseFinishUnFinised")%> " target="_blank">07. Date Wise Finished UnFinished Report</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProductionUnitSummary?Type=ProdSummary")%> " target="_blank">108. Product Unit Time Summary</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptDaywiseHourlyProd?Type=DayProd")%> " target="_blank">09. Date Wise Hourly Production</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptDaywiseHourlyProd?Type=DayQcRej")%> " target="_blank">10. Date Wise Hourly QC</a>
                                    </li>

                                </ul>

                            </div>
                            
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="PnlRepSemiFg" runat="server" CssClass="col-md-12" Visible="false">
                       <div class="row">

                            <div class="col-md-6">

                                <ul class="list-unstyled" id="SERV">
                                    <li class="menuheading">
                                        <span class="">A.Raw Materials</span>
                                    </li>
                                    <li>

                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptCentralStore?InputType=General")%> " target="_blank">01. Inventory Report-General</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptCentralStore?InputType=QuantityB")%> " target="_blank">02.  Inventory Report-Quantity Basis</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_23_SaM/RptIndProStock?Type=MatHis")%> " target="_blank">03.   Materials History</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptDaywiseMatIssue2?Type=SemiFG")%> " target="_blank">04.  Day Wise Materials Issue </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/RptDaywiseMatIssue2?Type=SemiFGQty")%> " target="_blank">05.  Day Wise Materials Issue(Qty) </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_03_StdCost/MatAvailability?Type=SemiFG")%> " target="_blank">06.  Materials Availability </a>
                                    </li>
                                    <li class="menuheading">
                                        <span>B.Production</span>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProdLoss?Type=SemFG")%> " target="_blank">01.  Production Loss Report </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProdLoss?Type=SemFGqty")%> " target="_blank">02.  Production Loss- (Qty) </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_07_Inv/ProductionReport")%> " target="_blank">03.  Day wise production report </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProductionUnitSummary?Type=ProdWeight")%> " target="_blank">04. Product Unit Weight </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptDaywiseHourlyProd?Type=DWiseShiftSemi")%> " target="_blank">05. Date Wise Production Efficiency(Machine Wise) </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProPendingInWip?Type=SemiFG")%> " target="_blank">06. Pending Production List (WIP) Semi FG </a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptDaywiseHourlyProd?Type=MatUtilMachWise")%> " target="_blank">07. Material Utillization Report(Machine Wise) </a>
                                    </li>
                                </ul>

                            </div>
                           
                        </div>

                    </asp:Panel>
                </asp:Panel>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <%--    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
    <script src="../Scripts/jquery.counterup.min.js"></script>--%>

    <script>
        function Confirm() {
            if (confirm("Do you want to Delete This item?")) {
                return true;

            } else {
                return false;
            }
        }
        jQuery(document).ready(function ($) {

            $('.counter').counterUp({
                delay: 10,
                time: 1000
            });

        });
    </script>

    <%-- <Triggers>
<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>
</Triggers>--%>

    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>


