<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptLCInterface.aspx.cs" Inherits="SPEWEB.F_09_Commer.RptLCInterface" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function Search_Gridview(strKey, cellNr, gvName) {

            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName.toLowerCase()) {
                case "gvprobrec":
                    tblData = document.getElementById("<%=gvprobrec.ClientID %>");
                    break;
                case "gvreqcheck":
                    tblData = document.getElementById("<%=gvReqCheck.ClientID %>");
                    break;
                case "gvreqauth":
                    tblData = document.getElementById("<%=gvReqAuth.ClientID %>");
                    break;
                case "gvprobapp":
                    tblData = document.getElementById("<%=gvprobapp.ClientID %>");
                    break;
                case "gvcscrte":
                    tblData = document.getElementById("<%=gvcscrte.ClientID %>");
                    break;
                case "gvcsaprv":
                    tblData = document.getElementById("<%=gvcsaprv.ClientID %>");
                    break;
                case "gvprobass":
                    tblData = document.getElementById("<%=gvprobass.ClientID %>");
                    break;
                case "gvprobassapp":
                    tblData = document.getElementById("<%=gvprobassapp.ClientID %>");
                    break;
                case "gvprobbill":
                    tblData = document.getElementById("<%=gvprobbill.ClientID %>");
                    break;
                case "gvcosting":
                    tblData = document.getElementById("<%=gvCosting.ClientID %>");
                    break;
                case "gvimportapp":
                    tblData = document.getElementById("<%=gvImportApp.ClientID %>");
                    break;
                case "gvpoin":
                    tblData = document.getElementById("<%=gvPoIn.ClientID %>");
                    break;
                case "gvcscheck":
                    tblData = document.getElementById("<%=gvCsCheck.ClientID %>");
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

           <%-- var gv = $('#<%=this.gvprobrec.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvReqCheck.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvReqAuth.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvprobapp.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvcscrte.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvcsaprv.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvprobass.ClientID %>');
            gv.Scrollable();


            var gv = $('#<%=this.gvprobassapp.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvprobbill.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvCosting.ClientID %>');
            gv.Scrollable();--%>
           <%-- var gv = $('#<%=this.gvImportApp.ClientID %>');
            gv.Scrollable();--%>

        };

        function showMvToLclModal() {
            $('#modalMoveToLocal').modal("show");
        }

        function closeMvToLclModal() {
            $('#modalMoveToLocal').modal("hide");
        }

        function LoadRdlcVIewer(comcod, orderno, supcode, msrno, reqno) {
            window.open(`../F_10_Procur/PuchasePrint?Type=OrderSavePdf&comcod=${comcod}&orderno=${orderno}&supcode=${supcode}&msrno=${msrno}&reqno=${reqno}&dayid=${orderno}`, '_blank');
        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="30">
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
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm px-0" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To Date</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm px-0" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="DdlSeason">Season</label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">BOM</label>
                                <asp:HyperLink ID="HyPBom" runat="server" CssClass="form-control form-control-sm" Target="_blank"></asp:HyperLink>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <asp:LinkButton ID="btnSetup" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-success" OnClick="btnSetup_Click">Setting</asp:LinkButton>
                            <asp:LinkButton ID="lnkInteface" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-secondary " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="lnkReports" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-warning" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton>
                            <asp:HyperLink ID="lnkGoToLocal" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-primary" Target="_blank" NavigateUrl="~/F_10_Procur/RptPurInterfaceLocal">Local Purchase</asp:HyperLink>
                        </div>

                        <div class="col-md-1">
                            <div class="btn-group" style="margin-top: 28px;" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-sm btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Requisition</asp:HyperLink>
                                        <%--<asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" CssClass="dropdown-item">Re-Order</asp:HyperLink>--%>
                                        <%--<asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Pro/PurInformation.aspx" CssClass="dropdown-item">Dashboard</asp:HyperLink>--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <%--                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000"></asp:Timer>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                    </triggers--%>


                            <asp:Panel ID="PnlInt" runat="server" Visible="false">
                                <div class="row">
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
                                                                <%--Req Auth--%>
                                                                <asp:ListItem Value="3"></asp:ListItem>
                                                                <asp:ListItem Value="4"></asp:ListItem>
                                                                <asp:ListItem Value="5"></asp:ListItem>
                                                                <%--CS Check--%>
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
                                                <div class="clearfix"></div>
                                            </fieldset>
                                            <div>

                                                <asp:Panel ID="pnlallRec" runat="server" Visible="false">
                                                    <div class="table-responsive col-11">
                                                        <div class="row" style="max-height: 360px">
                                                            <asp:GridView ID="gvprobrec" OnRowDataBound="gvprobrec_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True" Width="1150px">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Store Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcenter" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText=" ">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchlgpbmno1" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Req. Number" onkeyup="Search_Gridview(this,2, 'gvprobrec')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvprobrec')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="MRF No" onkeyup="Search_Gridview(this,4, 'gvprobrec')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:HyperLink ID="hlnkgvgvmrfno" runat="server" BorderStyle="none"
                                                                                Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="150px">
                                      
                                                                            </asp:HyperLink>

                                                                            <%--<asp:Label ID="lblgvprodesc" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                Width="80px"></asp:Label>--%>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Item </br> Count">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Requistion</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Requistion</br> Amount">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchReqAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="Requistion Amount" onkeyup="Search_Gridview(this,7, 'gvprobrec')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Curent Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblcurrentSt" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstatus")) %>'
                                                                                Width="100px"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvprobrec')"></asp:TextBox><br />
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
                                                                        <HeaderTemplate>
                                                                            <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:HyperLink ID="HyImportPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-print"></span>
                                                                            </asp:HyperLink>
                                                                            <asp:LinkButton ID="btnDelRec" Visible="false" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Req. By">
                                                                        <HeaderTemplate>

                                                                            <asp:TextBox ID="txtSearchUser" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this,11, 'gvprobrec')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                Width="80px"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                                <PagerStyle CssClass="gvPagination" />
                                                                <HeaderStyle CssClass="grvHeader" />
                                                                <FooterStyle CssClass="grvFooter" />

                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="PnlReqCheck" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvReqCheck" OnRowDataBound="gvReqCheck_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True" Width="1150px">
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
                                                                                <asp:Label ID="lblgvprobno" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcomcod" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="MRF No" onkeyup="Search_Gridview(this,1, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmno1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="MRF No">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="MRF No" onkeyup="Search_Gridview(this,4, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprodesc" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req <br/> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requistion</br> Amount">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,8, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="right" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req. By">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtgvRqCkUsername" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this, 9, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>

                                                                                <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>

                                                                                <asp:HyperLink ID="lnkbtnUpdate" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black"><span class="fa fa-edit"></span>
                                                                                </asp:HyperLink>

                                                                                <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Green"><span class="fa fa-check"></span>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>

                                                                            <ItemStyle Width="110px"/>
                                                                            <HeaderStyle HorizontalAlign="Center" Width="11px" />
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
                                                                    <RowStyle CssClass="grvRows" />
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:Panel>

                                                <asp:Panel ID="PnlReqAuth" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvReqAuth" OnRowDataBound="gvReqAuth_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprobno" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcomcod" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="MRF No" onkeyup="Search_Gridview(this,1, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmno1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="260px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MRF No">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="MRF No" onkeyup="Search_Gridview(this,4, 'gvReqCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprodesc" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Req</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Requistion</br> Amount">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this, 8, 'gvReqAuth')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="right" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req. By">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtgvRqAthUsername" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this, 9, 'gvReqAuth')"></asp:TextBox><br />
                                                                            </HeaderTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>

                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>


                                                                                <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>
                                                                                <asp:HyperLink ID="lnkbtnEdit" CssClass="btn btn-xs btn-e" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                                                </asp:HyperLink>

                                                                                <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                                </asp:HyperLink>

                                                                                <asp:LinkButton ID="gvaprbtnDelReqChk" CssClass="btn btn-xs btn-default" runat="server" OnClick="gvaprbtnDelReqChk_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="110px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="110px" VerticalAlign="Top" />
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

                                                </asp:Panel>

                                                <asp:Panel ID="pnlRecApp" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvprobapp" OnRowDataBound="gvprobapp_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprobno" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcomcod" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Req No" onkeyup="Search_Gridview(this,1, 'gvprobapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmno1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvprobapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <%--  <asp:TemplateField HeaderText="Center Name ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcenter" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centerdesc")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvStorname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="260px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MRF No">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="MRF No" onkeyup="Search_Gridview(this,4, 'gvprobapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprodesc" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Req</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Requistion</br> Amount">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,8, 'gvprobapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="right" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req. By">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtgvRqAppUsername" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this, 9, 'gvprobapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>


                                                                                <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>

                                                                                <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                                </asp:HyperLink>
                                                                                <asp:LinkButton ID="btnDelReqRev" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelReqRev_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>


                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
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

                                                </asp:Panel>

                                                <asp:Panel ID="PanCSCrete" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvcscrte" OnRowDataBound="gvcscrte_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcentrid" runat="server" Visible="false"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>

                                                                                <asp:Label ID="lblReqCkapp" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checked")) %>'
                                                                                    Width="49px"></asp:Label>

                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Req No" onkeyup="Search_Gridview(this,1, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmno1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Req Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approval Date">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvappdate" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "arpvdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MRF No ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvMrfNo" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Req</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requistion</br> Amount">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="right" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Req. By">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtgvCsCrtUsername" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this, 10, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">

                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="HyInprPrint" runat="server" CssClass="btn btn-xs btn-default" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">

                                                                            <ItemTemplate>

                                                                                <asp:HyperLink Target="_blank" CssClass="btn btn-xs btn-default" ID="lnkbtnEdit" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDelReqApp" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelReqApp_Click" OnClientClick="return confirm('Do You Want to Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
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

                                                </asp:Panel>

                                                <asp:Panel ID="PnlCSChk" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvCsCheck" OnRowDataBound="gvCsCheck_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcentrid" runat="server" Visible="false"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>

                                                                                <asp:Label ID="lblReqCkapp" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checked")) %>'
                                                                                    Width="49px"></asp:Label>

                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Req No" onkeyup="Search_Gridview(this,1, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmno1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Req Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approval Date">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvappdate" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "arpvdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="260px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MRF No ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvcscrte')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvMrfNo" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Req</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Requistion</br> Amount">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this, 9, 'gvCsCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="right" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Req. By">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtgvCsChkUsername" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this, 10, 'gvCsCheck')"></asp:TextBox><br />
                                                                            </HeaderTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>

                                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span> </asp:HyperLink>
                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnEdit" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>
                                                                                <asp:LinkButton ID="btnCSRev" OnClick="btnCSRev_Click" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
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

                                                </asp:Panel>
                                                <asp:Panel ID="PanCsAprv" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvcsaprv" OnRowDataBound="gvcsaprv_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcentrid" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Req No" onkeyup="Search_Gridview(this,1, 'gvcsaprv')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmno1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvcsaprv')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <%--  <asp:TemplateField HeaderText="Approval Date">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvappdate" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chkdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>--%>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="260px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MRF No ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="MRF No" onkeyup="Search_Gridview(this,4, 'gvcsaprv')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvMrfNo" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Req</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requistion</br> Amount">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,8, 'gvcsaprv')"></asp:TextBox><br />
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
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>
                                                                                <asp:HyperLink ID="HyPrintCS" runat="server" Target="_blank" ForeColor="Blue" ToolTip="CS Print" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req. By">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtgvCsAprvUsername" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this, 10, 'gvcsaprv')"></asp:TextBox><br />
                                                                            </HeaderTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">

                                                                            <ItemTemplate>

                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnEdit" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-edit"></span> </asp:HyperLink>
                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnimport" CssClass="btn btn-xs btn-default" runat="server" ToolTip="Entry"><span class="fa fa-check"></span> </asp:HyperLink>

                                                                                <asp:HyperLink Target="_blank" ID="lnkImporAppr" CssClass="btn btn-xs btn-default" runat="server" ToolTip="Approved"><span class="fa fa-check"></span> </asp:HyperLink>

                                                                                <asp:LinkButton ID="btnDelCSNext" runat="server" CssClass="btn btn-xs btn-default" OnClick="btnDelCSNext_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>


                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
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

                                                </asp:Panel>
                                                <asp:Panel ID="PanelAssorted" Visible="false" runat="server">
                                                    <%--LC Opening--%>
                                                    <div class="row">
                                                        <div class="table-responsive col-md-9">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvprobass" OnRowDataBound="gvprobass_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqno" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PO </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchsyspon" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="PO No" onkeyup="Search_Gridview(this,1, 'gvprobass')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpsyspon" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'
                                                                                    Width="100px"></asp:Label>
                                                                                <asp:Label ID="lblsyspon" runat="server" Visible="false"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "syspon")) %>'
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PO Date">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvappdate" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "podat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req No" onkeyup="Search_Gridview(this,3, 'gvprobass')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmno1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Supplier Name ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" Width="240px" placeholder="Supplier Name" onkeyup="Search_Gridview(this,4, 'gvprobass')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvsupname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supname")) %>'
                                                                                    Width="240px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="MRF No ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRf" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvprobass')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvMrfNo" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Vendor Name ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchvennamep" BackColor="Transparent" BorderStyle="None" runat="server" Width="240px" placeholder="Vendor Name" onkeyup="Search_Gridview(this,6, 'gvprobass')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvvenname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venname")) %>'
                                                                                    Width="240px"></asp:Label>
                                                                                <asp:Label ID="lblvencode" runat="server" Visible="false"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vencode")) %>'
                                                                                    Width="240px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="LbtnLcOpeningALL" runat="server" OnClick="LbtnLcOpening_Click">Multi PO</asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="gvCheckbx" runat="server" />
                                                                            </ItemTemplate>

                                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <%-- <FooterTemplate>
                                                                            <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>--%>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Pass Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvPassIteam" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "passitm")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Requistion</br> Amount">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblFreqamt" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this, 6, 'gvprobass')"></asp:TextBox><br />
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
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnImApp" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-edit"></span> </asp:HyperLink>

                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnEdit" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>



                                                                            </ItemTemplate>

                                                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="L/C No ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchactdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="L/C No" onkeyup="Search_Gridview(this,14, 'gvprobass')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvactdesc" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="HyImportApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>
                                                                                <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>
                                                                                <asp:HyperLink ID="HyPrintCS" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" ToolTip="CS Print" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
                                                                        </asp:TemplateField>




                                                                        <asp:TemplateField HeaderText="Req. By">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtgvProdAppUsername" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req By" onkeyup="Search_Gridview(this, 16, 'gvprobass')"></asp:TextBox><br />
                                                                            </HeaderTemplate>


                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
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

                                                </asp:Panel>
                                                <asp:Panel ID="pnlAssApp" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvprobassapp" OnRowDataBound="gvprobassapp_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgReq" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="actcode" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcentrid" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req </br> Number">

                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req No" onkeyup="Search_Gridview(this,1, 'gvprobassapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvReqno" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Req Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvprobassapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvrecddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Approve Date">

                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvasrdat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "arpvdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                                                    Width="260px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Lc Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprodesc" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Order</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvordqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Received</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvrcvqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQCTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,9, 'gvprobassapp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle HorizontalAlign="right" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Req. By">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                Width="80px"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>



                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnEdit" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>

                                                                                <asp:HyperLink Target="_blank" ID="HypEditLc" CssClass="btn btn-xs btn-default" runat="server" ToolTip="Edit Lc Information"><span class="fa fa-edit"></span> </asp:HyperLink>

                                                                                <asp:LinkButton ID="btnDelRcv" OnClick="btnDelRcv_Click" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
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


                                                </asp:Panel>

                                                <asp:Panel ID="Pnlbill" Visible="false" runat="server">
                                                    <div class="row">
                                                        <div class="table-responsive col-md-11">
                                                            <div class="row" style="max-height: 360px">

                                                                <asp:GridView ID="gvprobbill" OnRowDataBound="gvprobbill_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcentrid" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="lc code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvlccode" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Lc Rec </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchLcNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Lc Rec No" onkeyup="Search_Gridview(this,1, 'gvprobbill')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvrcvNo" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rcvno")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvprobbill')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                                                    Width="160px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Lc Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprodesc" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="Receive</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvRcvqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="QC</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvqcqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQCTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>

                                                                        <%--  <asp:TemplateField HeaderText="Req. By">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                Width="80px"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>--%>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>

                                                                                <asp:HyperLink Target="_blank" ID="HypPreEdit" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-edit"></span> </asp:HyperLink>
                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnEdit" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>
                                                                                <asp:HyperLink Target="_blank" ID="LinkPrint" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span> </asp:HyperLink>

                                                                                <asp:LinkButton ID="btnDelQC" OnClick="btnDelQC_Click" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="180px" VerticalAlign="Top" />
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
                                                </asp:Panel>
                                                <asp:Panel ID="PnlCosting" Visible="false" runat="server">
                                                    <div class="row">
                                                        <div class="table-responsive col-md-10">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvCosting" OnRowDataBound="gvCosting_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                                                    Style="text-align: right"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcentrid" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grrno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="lc code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvlccode" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="GRR </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchGRNNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Lc Rec No" onkeyup="Search_Gridview(this,1, 'gvCosting')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgvgrrno" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grrno")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="QC Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvCosting')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "qcdate")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustname" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stordesc")) %>'
                                                                                    Width="230px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Lc Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvprodesc" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <%-- <asp:TemplateField HeaderText="Req No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvReqno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>--%>


                                                                        <asp:TemplateField HeaderText="Item </br> Count">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvitemcount" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" />
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="QC</br> Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0;(#,##0);") %>'
                                                                                    Width="50px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterTemplate>
                                                                                <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                            </FooterTemplate>
                                                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                        </asp:TemplateField>



                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="fa fa-print"></span></asp:LinkButton>--%>
                                                                                <asp:HyperLink ID="HypbtnEdit" Width="30" runat="server" CssClass="btn btn-xs btn-default" Target="_blank"><span class="fa fa-edit"></span>
                                                                                </asp:HyperLink>
                                                                                <asp:HyperLink Target="_blank" Width="30" ID="lnkbtnForward" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span> </asp:HyperLink>
                                                                                <asp:HyperLink Target="_blank" Width="30" ID="HypbtnPrint" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span> </asp:HyperLink>

                                                                                <asp:LinkButton ID="btnDelRec" Width="30" OnClick="btnDelRec_Click" OnClientClick="return confirm('Do You Want to Delete This Item?');" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>

                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="180px" VerticalAlign="Top" />
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
                                                </asp:Panel>
                                                <asp:Panel ID="pnlPOin" Visible="false" runat="server">

                                                    <div class="row">
                                                        <div class="table-responsive col-md-12">
                                                            <div class="row" style="max-height: 360px">
                                                                <asp:GridView ID="gvPoIn" OnRowDataBound="gvPoIn_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                    ShowFooter="True">
                                                                    <RowStyle />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sl">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvSlNo0im" runat="server"
                                                                                    Style="text-align: right" Width="30px"
                                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvcentridim" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                    Width="49px"></asp:Label>
                                                                                
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req No" onkeyup="Search_Gridview(this,1, 'gvImportApp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lgpbmnoim1" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                    Width="120px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Date" onkeyup="Search_Gridview(this,2, 'gvImportApp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvbgddatim" runat="server" BackColor="Transparent"
                                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Store Name ">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvCustnameim" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                    Width="200px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="MRF No ">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="MRF" onkeyup="Search_Gridview(this,4, 'gvImportApp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvMrfNoimrfn" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                    Width="150px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Supplier Name">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Supplier Name" onkeyup="Search_Gridview(this,5, 'gvImportApp')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblgvMrfNoimSup" runat="server"
                                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supname")) %>'
                                                                                    Width="180px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this, 6, 'gvPoIn')"></asp:TextBox><br />
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
                                                                            <HeaderTemplate>
                                                                                <asp:TextBox ID="txtGvPoInSearchUser" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req. By" onkeyup="Search_Gridview(this, 7, 'gvPoIn')"></asp:TextBox><br />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                    Width="80px"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Req">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false" ToolTip="Req Print"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CS">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="HyPrintCS" runat="server" Target="_blank" ForeColor="Blue" ToolTip="CS Print" Font-Underline="false"><span class="fa fa-print"></span>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDelPoIn" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelPoIn_Click" OnClientClick="return confirm('Do You Want to Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="">
                                                                            <HeaderTemplate>
                                                                                <asp:HyperLink ID="hlbtntbCdataExelim" CssClass="btn btn-xs btn-danger d-none" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink Target="_blank" ID="lnkbtnimportim" runat="server" CssClass="text-primary" ToolTip="PO Entry"><span class="fa fa-check"></span> </asp:HyperLink>

                                                                                <%--<asp:HyperLink Target="_blank" ID="lnkImporApprim" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Approved"><span class="fa  fa-check-circle"></span> </asp:HyperLink>--%>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="LbtnForwardtoLocal" OnClick="LbtnForwardtoLocal_Click" runat="server">Move to Local</asp:LinkButton>
                                                                            </ItemTemplate>
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

                                                </asp:Panel>
                                                <asp:Panel ID="pnlImportAppr" Visible="false" runat="server">
                                                    <div class="table-responsive">
                                                        <div class="row" style="max-height: 360px">
                                                            <asp:GridView ID="gvImportApp" OnRowDataBound="gvImportApp_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0im" runat="server"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentridim" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                                Width="49px"></asp:Label>
                                                                              <asp:Label ID="lblsyspondel" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "syspon")) %>'
                                                                                Width="120px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="PO </br> Number">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchsyspon" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="PO No" onkeyup="Search_Gridview(this,3, 'gvImportApp')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpsyspon" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'
                                                                                Width="120px"></asp:Label>
                                                                           
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Requisition </br> Number">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req No" onkeyup="Search_Gridview(this,3, 'gvImportApp')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgpbmnoim1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Date" onkeyup="Search_Gridview(this,4, 'gvImportApp')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbgddatim" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Store Name ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCustnameim" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="MRF No">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvImportApp')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvMrfNoimrfn" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchSup" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Supplier Name" onkeyup="Search_Gridview(this,6, 'gvImportApp')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvImportAppSupCode" runat="server" Visible="false"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supcode")) %>'></asp:Label>
                                                                            <asp:Label ID="lblgvImportAppMsrno" runat="server" Visible="false"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'></asp:Label>
                                                                            <asp:Label ID="lblgvMrfNoimSup" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supname")) %>'
                                                                                Width="180px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,7, 'gvImportApp')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle HorizontalAlign="right" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="Req. By">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchUser" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="req by" onkeyup="Search_Gridview(this,8, 'gvImportApp')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUsersName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedbyname")) %>'
                                                                                Width="80px"></asp:Label>

                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="PO">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HyInprPrintPO" runat="server" CssClass="text-primary" Target="_blank" Font-Underline="false" ToolTip="PO Print"><span class="fa fa-print"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Req">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HyInprPrint" runat="server" CssClass="text-primary" Target="_blank" Font-Underline="false" ToolTip="Req Print"><span class="fa fa-print"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="CS">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HyPrintCS" runat="server" CssClass="text-primary" Target="_blank" ToolTip="CS Print" Font-Underline="false"><span class="fa fa-print"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelPoApp" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelPoApp_Click" OnClientClick="return confirm('Do You Want to Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15px" HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="">
                                                                        <HeaderTemplate>
                                                                            <%--<asp:HyperLink ID="hlbtntbCdataExelim" CssClass="btn btn-xs btn-danger d-none" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>--%>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lnkbtnimportim" runat="server" CssClass="text-primary mx-1" Target="_blank" ToolTip="PO / Import Edit"><span class="fa fa-check"></span> </asp:HyperLink>
                                                                            <asp:HyperLink ID="lnkImporApprim" runat="server" CssClass="text-primary mx-1" Target="_blank" ToolTip="PO / Import Approval"><span class="fa fa-check-circle"></span> </asp:HyperLink>
                                                                            <asp:LinkButton ID="lnkSendMail" runat="server" CssClass="text-primary mx-1" ToolTip="Send Mail" OnClick="lnkSendMail_Click"><span class="fa fa-mail-bulk"></span> </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="90px" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="90px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <%--   <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LbtnForwardtoLocal" OnClick="LbtnForwardtoLocal_Click" OnClientClick="return confirm('Do you want to move it Local Purchase?')" runat="server">Move to Local</asp:LinkButton>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                </Columns>
                                                                <PagerStyle CssClass="gvPagination" />
                                                                <HeaderStyle CssClass="grvHeader" />
                                                                <FooterStyle CssClass="grvFooter" />
                                                                <RowStyle CssClass="grvRows" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </asp:Panel>

                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </asp:Panel>



                            <asp:Panel ID="pnlReprots" runat="server">

                                <asp:Panel ID="plnMgf" runat="server" CssClass="row" Visible="false">
                                    <div class="col-md-4">
                                        <ul class="list-unstyled">
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptLCStatus?Type=LCCosting")%> " target="_blank">01. LC COSTING REPORT</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptLCPosition?Type=LCPosition")%> " target="_blank">02. LC STATUS REPORT</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptSalSummery?Type=LcCost")%> " target="_blank">03. LC OVERALL COSTING</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptLCStatus?Type=LCVari")%> " target="_blank">04. LC VARIANCE REPORTS</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptSalSummery?Type=LcReceive")%> " target="_blank">05. LC RECEIVED REPORTS</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptLCStatus?Type=LCRecvCon")%> " target="_blank">06. LC RECEIVED CONSIGNMENT WISE</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%> " target="_blank">07. BOM APPROVED LIST</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptOrderVsReceive?Type=OrderVsRec")%> " target="_blank">08. BOM VS PO</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptWorkOrderVsSupply?Type=OrdVsSup")%> " target="_blank">09. Work Order-Supplier Wise</a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="list-unstyled">

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=IndSup")%> " target="_blank">10.  Purchase History-Supplier Wise</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=DaywPur")%> " target="_blank">11.   Day Wise Purchase</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptDateWiseReq?Type=PeriodPurchase")%> " target="_blank">12.  Periodic Purchase Tracking </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=PurSum")%>" target="_blank">13. Purchase Summary</a>
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
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptMataWisePO?Type=MatWisePO")%> " target="_blank">22. Materials Wise PO Report</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptMataWisePO?Type=WeeklyPlanWiseMat")%> " target="_blank">23. Weekly Plan Wise Material Report</a>
                                            </li>
                                              <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk")%> " target="_blank">24. Purchase Tracking</a>
                                            </li>
                                           
                                        </ul>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlSalesSetup" runat="server" CssClass="row" Visible="false">
                                    <div class="col-md-12">
                                        <ul class="list-unstyled">
                                            <%--  <li>
                                        <a href="<%=this.ResolveUrl("~/F_23_SaM/GeneralCodeBook.aspx?Type=All")%> " target="_blank">01. Create Return/Replacement List</a>
                                    </li>--%>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_13_CWare/PurReqEntry02?InputType=LCEntry&comcod=&actcode=&genno=")%> " target="_blank">01.L/C REQUISITION INFORMATION</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/LCAllInfo?Type=All")%> " target="_blank">02.LC OPENING List</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/LcReceive?Type=Entry&comcod=&actcode=&centrid=&genno=")%> " target="_blank">03. IMPORT MATERIAL RECIVED</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/LCCostingDetails?Type=Entry")%> " target="_blank">04. IMPORT MATERIAL COSTING</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/StandardMatCost")%> " target="_blank">05. STANDARD IMPORTED COSTING</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/PurMktSurvey?Type=SurveyLink")%> " target="_blank">06. Survey Link</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccResourceCode?Type=Matcode")%> " target="_blank">07. Material Opening Code</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/SalesCodeBook?Type=All")%> " target="_blank">08. General Code Book</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint")%> " target="_blank">09. Details Code</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/PurSupplierinfo")%> " target="_blank">10. Supplier Information</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccSubCodeBook?InputType=SupplierCode")%> " target="_blank">11. Supplier Code</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/LCOpening?Type=Open&genno=&actcode=")%> " target="_blank">12. Lc Details Information Edit</a>
                                            </li>

                                        </ul>
                                    </div>
                                </asp:Panel>

                            </asp:Panel>



                            <%--                    <script src="../Scripts/jquery.counterup.min.js"></script>
                    <script>
                        jQuery(document).ready(function ($) {
                            $('.counter').counterUp({
                                delay: 10,
                                time: 1000
                            });
                        });
                    </script>--%>
                        </ContentTemplate>

                    </asp:UpdatePanel>


                </div>
            </div>



            <div class="modal fade bd-example-modal-lg" id="modalMoveToLocal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Material List</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Select Store"></asp:Label>
                                    <asp:DropDownList ID="DDlStore" runat="server" CssClass=" form-control form-control-sm" TabIndex="3"></asp:DropDownList>
                                </div>
                            </div>
                            <asp:GridView ID="GV_MatList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkMat" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0im" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmatname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matname")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblspcfdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreqqty" runat="server"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblresrate" runat="server"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalprice" runat="server"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalprice")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                            <asp:Button ID="btnMvToLocal" runat="server" type="button" class="btn btn-primary" Text="Move To Local" OnClick="btnMvToLocal_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>


            <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

