<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProductionInterface.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProductionInterface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .InBox {
            color: red !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
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

        .UpdateMOdel {
            position: fixed;
            margin: 0;
            width: 100%;
            height: 100%;
            padding: 0;
        }

        .allmaterial .modal-dialog {
            max-width: 80% !important;
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

    <script type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.LabelHandler(event);
            });
        };

        function AlertModal() {
            window.alert('this is an alert');
        }

        function ShowWindow(url) {
            window.open(url, '_blank');
        }

        function openModal() {
            $('#myModal').modal('toggle');
        }

        function CloseModal() {
            $('#myModal').modal('hide');
        }


        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "grvReqAprvl":
                    tblData = document.getElementById("<%=grvReqAprvl.ClientID %>");
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


        function SelectAllCheckboxes(gridName, chk) {

            switch (gridName) {

                case "grvProIssue":

                    $('#<%=grvProIssue.ClientID %>').find("input:checkbox").each(function () {

                        if ($(this).closest('tr').attr('class') == "grvRows") {

                            if ($(this).closest('tr').css('display') != "none") {

                                if ((this).disabled == false) {
                                    if (this != chk) {
                                        this.checked = chk.checked;
                                    }
                                }
                            }
                        }
                    });

                    break;

                case "grvReqAprvl":

                    $('#<%=grvReqAprvl.ClientID %>').find("input:checkbox").each(function () {

                        if ($(this).closest('tr').attr('class') == "grvRows") {

                            if ($(this).closest('tr').css('display') != "none") {

                                if ((this).disabled == false) {
                                    if (this != chk) {
                                        this.checked = chk.checked;
                                    }
                                }
                            }
                        }
                    });

                    break;

            }

        }

        function deleteRequisiton(e) {
            var preqno = e.target.parentNode.parentNode.parentNode.childNodes[3].childNodes[1].innerHTML;
            var itmno = e.target.parentNode.parentNode.parentNode.childNodes[3].childNodes[3].innerHTML;
            var spcfno = e.target.parentNode.parentNode.parentNode.childNodes[3].childNodes[5].innerHTML;
            var procode = e.target.parentNode.parentNode.parentNode.childNodes[3].childNodes[7].innerHTML;
            var reqdate = e.target.parentNode.parentNode.parentNode.childNodes[3].childNodes[9].innerHTML;
            var reqtype = document.getElementById("lblreqtype").innerHTML;

            console.log(2222, itmno)

            var row = e.target.parentNode.parentNode.parentNode;

            if (window.confirm('Are you sure want to delete this item?')) {
                $.ajax({
                    type: "POST",
                    url: "ProductionInterface.aspx/DeleteRequisition",
                    data: "{preqno: '" + preqno + "', itmno: '" + itmno + "', spcfno: '" + spcfno + "', procode: '" + procode + "', reqdate: '" + reqdate + "', reqtype: '" + reqtype + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == true) {
                            row.parentNode.removeChild(row);
                        }
                        else {
                            alert("You are unable to delete this item!!")
                            return;
                        }
                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response) {
                        alert(response.d);
                    }
                });
            }
        }

        function updateRequisiton(e) {

            var preqno = e.target.parentNode.parentNode.childNodes[3].childNodes[1].innerHTML;
            var itmno = e.target.parentNode.parentNode.childNodes[3].childNodes[3].innerHTML;
            var spcfno = e.target.parentNode.parentNode.childNodes[3].childNodes[5].innerHTML;
            var procode = e.target.parentNode.parentNode.childNodes[3].childNodes[7].innerHTML;
            var reqdate = e.target.parentNode.parentNode.childNodes[3].childNodes[9].innerHTML;
            var reqtype = document.getElementById("lblreqtype").innerHTML;
            var reqty = e.target.parentNode.parentNode.childNodes[13].childNodes[1].value;

            //console.log(1, preqno);
            //console.log(2, itmno);
            //console.log(3, spcfno);
            //console.log(4, procode);
            //console.log(5, reqdate);
            //console.log(6, reqtype);
            //console.log(6, reqty);

            $.ajax({
                type: "POST",
                url: "ProductionInterface.aspx/UpdateRequisition",
                data: "{preqno: '" + preqno + "', itmno: '" + itmno + "', spcfno: '" + spcfno + "', procode: '" + procode + "', reqdate: '" + reqdate + "', reqtype: '" + reqtype + "', reqty: '" + reqty + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == true) {
                        alert("Item quantity updated successfully.");
                        e.target.parentNode.parentNode.childNodes[13].childNodes[1].value = (Math.round(reqty * 100) / 100).toFixed(4);
                    }
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }

    </script>


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
                        <label class="control-label" for="FromDate">Date</label>
                        <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="control-label" for="ToDate">To Date</label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="control-label">Planning</label>
                        <asp:HyperLink ID="HyPPlan" runat="server" CssClass="form-control form-control-sm" Target="_blank"></asp:HyperLink>
                    </div>
                </div>
                <div class="col-md-3">
                    <asp:LinkButton ID="LbtnSetting" runat="server" CssClass="margin-top30px btn btn-sm btn-success" OnClick="LbtnSetting_Click">Setting</asp:LinkButton>
                    <asp:LinkButton ID="LbtnInt" runat="server" CssClass="margin-top30px btn btn-sm btn-secondary " OnClick="LbtnInt_Click">Interface</asp:LinkButton>
                    <asp:LinkButton ID="LbtnRep" runat="server" CssClass="margin-top30px btn btn-sm btn-warning" OnClick="LbtnRep_Click">ALL Reports</asp:LinkButton>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="control-label">Season</label>
                        <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <label class="control-label">Article No</label>
                    <asp:DropDownList ID="ddlLCName" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>

                    <asp:Label ID="lblLCdesc" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                </div>
                <div class="col-md-1">
                    <div class="margin-top30px btn-group" role="group" aria-label="Button group with nested dropdown">
                        <button type="button" class="btn btn-sm btn-danger">Operations</button>
                        <div class="btn-group" role="group">
                            <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                            <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                <div class="dropdown-arrow"></div>
                                <%--<asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" CssClass="dropdown-item">Production Plan</asp:HyperLink>--%>
                                <asp:HyperLink ID="HypLink4" runat="server" NavigateUrl="~/F_01_Mer/RptOrdAppSheet?Type=OrdPlan" Target="_blank" CssClass="dropdown-item">Order Plan Top Sheet</asp:HyperLink>
                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Export Planing</asp:HyperLink>


                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="card card-fluid">
        <div class="card-body">


            <asp:Timer ID="Timer1" Enabled="false" runat="server" OnTick="Timer1_Tick" Interval="60000"></asp:Timer>

            <%-- <triggers> <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" /></triggers>--%>

            <div class="row">
                <asp:Panel ID="PnlInt" runat="server" Visible="false">
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
                                                <asp:ListItem Value="3"></asp:ListItem>
                                                <asp:ListItem Value="4"></asp:ListItem>
                                                <asp:ListItem Value="5"></asp:ListItem>
                                                <asp:ListItem Value="6"></asp:ListItem>
                                                <asp:ListItem Value="7"></asp:ListItem>
                                                <asp:ListItem Value="8"></asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div>
                                <%--<Panel class="tab-content">--%>
                                <div class="col-md-12 pading5px asitCol5">
                                </div>
                                <asp:Panel ID="pnlallProd" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="gvProdInfo" runat="server" OnRowDataBound="gvProdInfo_RowDataBound" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tardate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plan No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPlan" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prono")) %>'
                                                                Width="85px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="220px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstyledesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Floor Order">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblgvTrilOrdr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trialordr")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="85px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article  No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvArticle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Target</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>


                                                            <asp:HyperLink ID="HyOrderPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
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
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plan No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvPlan" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prono")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno3" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Maste Lc" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvmlccod" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tardate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="170px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Floor Order">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblgvTrilOrdr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trialordr")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article  No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvArticle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Target</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req  Qty">
                                                        <ItemTemplate>
                                                            <asp:Label Width="60px" ID="lblgvbgdamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Current </br>Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpbmststus" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


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

                                </asp:Panel>

                                <asp:Panel ID="PanelIssue" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvProIssue" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvProIssue_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
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

                                                    <asp:TemplateField HeaderText="Req. </br> Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpreqno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req. Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpreqDate" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchdesc3" runat="server" Width="280px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordertype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Material </br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrsqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isueqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bal</br> qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" Width="80px" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvReqtype" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">

                                                        <HeaderTemplate>
                                                            <div class="form-inline justify-content-center">
                                                                <div class="mr-3">
                                                                    <asp:LinkButton runat="server" ID="lnkbtnPrintCombined" CssClass="btn btn-sm btn-primary pr-1" ToolTip="Selected Multiple Req Print" OnClick="lnkbtnPrintCombined_Click">
                                                                        <i class="fa fa-print mr-1"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div>
                                                                    <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes('grvProIssue', this);" ClientIDMode="Static" runat="server" />
                                                                </div>
                                                            </div>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:CheckBox runat="server" ID="chkPrintCombined" CssClass="mx-2" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Day ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvDayId" runat="server" Style="text-align: right"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "odayid") %>'
                                                                Width="70px"></asp:Label>
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
                                    </div>

                                </asp:Panel>

                                <asp:Panel ID="pnlProProcs" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                            <asp:GridView ID="gvprocess" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvprocess_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="49px"></asp:Label>
                                                            <asp:Label ID="lblpreq" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="49px"></asp:Label>
                                                            <asp:Label ID="lblFlags" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flag")) %>'
                                                                Width="49px"></asp:Label>
                                                            <asp:Label ID="LblOdayid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Preq No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Master LC ">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lblgvprodesc" runat="server" Target="_blank"
                                                                NavigateUrl='<%# ResolveUrl("~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))
                                                                     +"&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid"))) %>'
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="190px"></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Style Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchdesc4" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>' Font-Size="11px" Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="110px"></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Floor Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTrialordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trialorder")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article  No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvArticle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="On Process">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblgvProcess" runat="server" OnClick="lblgvProcess_Click"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprostepdesc")) %>'
                                                                Width="170px"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="In </br>Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvInqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Out </br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvOutQty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="In-Process </br>Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbalqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rej. </br> Qty" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectionqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Repair</br> qty" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrepairqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotalrepair" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Manpower" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmanpqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "manpower")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblmanpower" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="dropdown">
                                                                <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                    Action
                                                                             
                                                                </button>
                                                                <ul class="dropdown-menu dropdown-menu-right">
                                                                    <li>
                                                                        <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span> Print Process
                                                                        </asp:HyperLink>
                                                                    </li>
                                                                    <li>

                                                                        <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span> Production Entry
                                                                        </asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span> Edit Production
                                                                        </asp:HyperLink>

                                                                    </li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HypLinkAdditionReq" Visible="false" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-file-excel"></span> Additional Requistion
                                                                        </asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HypCommonMatreq" Visible="false" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-file-invoice"></span> Common Mat. Requisition
                                                                        </asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HypRecutMatReq" Visible="false" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-file-invoice"></span> Recutting Mat. Requisition
                                                                        </asp:HyperLink></li>
                                                                </ul>
                                                            </div>

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


                                </asp:Panel>

                                <asp:Panel ID="PnlFlorIssue" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvFlissue" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvFlissue_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="55px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Req. Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpreqDate" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchdesc1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="280px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WH Issue </br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrsqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isueqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Flor Issue</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "flisuqty")).ToString("#,##0.00;(#,##0.00);") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bal</br> qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdamt" Width="80px" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "florbalqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>
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
                                </asp:Panel>

                                <asp:Panel ID="PnlStrec" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="gvstorec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvstorec_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwipid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="49px"></asp:Label>
                                                            <asp:Label ID="lblprqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Preq No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date ">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorprodat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order </br> Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorproid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="220px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Style Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstyle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article  No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvArticleno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Production</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstrproqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblstrTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receive</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrcvqtt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblrcvTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


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
                                </asp:Panel>

                                <asp:Panel ID="PnlReqApp" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="grvReqAprvl" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvReqAprvl_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plan No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvplanno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "plnno")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno2" runat="server" Width="180px" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'></asp:Label>

                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Maste Lc" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFlag" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "flag")) %>'
                                                                Width="70px"></asp:Label>
                                                            <asp:Label ID="lgvmlccod" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchcode")) %>'
                                                                Width="180px"></asp:Label>
                                                            <asp:Label ID="lblProc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req. No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvReqDate1" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lgrvDept" runat="server" OnClick="lgrvDept_Click"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procdesc")) %>'
                                                                Width="180px"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <%--                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Order No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvraOrdrNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order No" onkeyup="Search_Gridview(this, 6, 'grvReqAprvl')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvReqApOrdrNo" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="110" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Article No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvReqApArtcl" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="110" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvItmCount" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Qty.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvResQty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvReqtype" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Demand<br/>Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvdmndDate" runat="server" Style="text-align: center"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "demanddate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">

                                                        <HeaderTemplate>
                                                            <div class="form-inline justify-content-center">
                                                                <div class="mr-3">
                                                                    <asp:LinkButton runat="server" ID="graLnkbtnPrintCombined" CssClass="btn btn-sm btn-primary pr-1" ToolTip="Selected Multiple Req Print" OnClick="graLnkbtnPrintCombined_Click">
                                                                        <i class="fa fa-print mr-1"></i>
                                                                    </asp:LinkButton>
                                                                </div>
                                                                <div>
                                                                    <asp:CheckBox ID="graChkHead" onclick="javascript:SelectAllCheckboxes('grvReqAprvl', this);" ClientIDMode="Static" runat="server" />
                                                                </div>
                                                            </div>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink Visible="false" CssClass="btn btn-xs btn-default" ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="LnkBtnApprove" OnClick="LnkBtnApprove_Click" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-check"></span>
                                                            </asp:LinkButton>

                                                            <asp:CheckBox runat="server" ID="graChkPrntCombined" CssClass="ml-2" />

                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Day ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gralblDayId" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid")) %>'
                                                                Width="150px"></asp:Label>
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
                                </asp:Panel>

                                <asp:Panel ID="PnlQC" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="grvQCEntry" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvQCEntry_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Master Order Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpbmno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ordrer No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvOrderNo" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbatchdesc2" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="On Process">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProcess" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprostepdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Production</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbalqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


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
                                </asp:Panel>

                                <asp:Panel ID="PnlComp" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="grvComp" OnRowDataBound="grvComp_RowDataBound" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <%--   <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbgddat" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Preq No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprodesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Style Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstyledesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article  No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvArticle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Receive</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbgdwqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>


                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
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
                </asp:Panel>
                <asp:Panel ID="PnlSet" runat="server" Visible="false">
                    <asp:Panel ID="PnlSetFG" runat="server" Visible="false">

                        <ul class="list-unstyled">
                            <li class="menuheading">
                                <span class="">A.Production Menu</span>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/MatAvailabilityFG?Type=FG&actcode=&sircode=&genno=")%> " target="_blank">04. Materials Availability- FG</a>

                            </li>
                            <li>

                                <a href="<%=this.ResolveUrl("~/F_15_Pro/ProductionProcess?Type=ProStart&actcode=")%> " target="_blank">01. Production Process- Start</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/ProductionProcess?Type=ProTransfer&actcode=")%> " target="_blank">02. Production Process</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/ProdReq?Type=Entry&actcode=&genno=")%> " target="_blank">03.  Production Requisition</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/EntryProTarget")%> " target="_blank">04.  Production Target </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/EntryProduction")%> " target="_blank">05.  Production Entry </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/ProdEntry?Type=Entry&actcode=")%> " target="_blank">06.  Goods Received Entry </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/ProdPlanTopSheet")%>" target="_blank">07.  Production Plan Top Sheet</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/AddProdReq?Type=addreq&actcode=&genno=&dayid=")%>" target="_blank">08.  Production Additional Requisition</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/ProductionManually?Type=Entry&genno=")%>" target="_blank">09.  Production Manually Entry</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/ProdPlanTopSheet?Type=ManProd")%>" target="_blank">10.  Manual Production Topsheet</a>
                            </li>


                        </ul>


                    </asp:Panel>
                    <asp:Panel ID="PnlSetSemiFg" runat="server" Visible="false">Settings Semi FG</asp:Panel>
                </asp:Panel>
                <asp:Panel ID="PnlRep" runat="server" Visible="false">
                    <asp:Panel ID="PnlRepFg" runat="server" Visible="false">
                        <ul class="list-unstyled">
                            <li class="menuheading">
                                <span class="">A.Raw Materials</span>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/InvReport?InputType=General")%> " target="_blank">01. Inventory Report-General</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/InvReport?InputType=AmountB")%> " target="_blank">02.  Inventory Report-Quantity Basis</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/RptIndProStock?Type=MatHis")%> " target="_blank">03.   Materials History</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_15_Pro/MatAvailability?Type=FG")%> " target="_blank">06.  Materials Availability-FG </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%>" target="_blank">07. BOM Approved List</a>
                            </li>
                        </ul>



                    </asp:Panel>
                    <asp:Panel ID="PnlRepSemiFg" runat="server" Visible="false">
                        <ul class="list-unstyled">
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
                                <a href="<%=this.ResolveUrl("~/F_07_Inv/RptDaywiseMatIssue2?Type=SemiFG")%> " target="_blank">04.  Day Wise Materials Issue 2 (Semi FG) </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_07_Inv/RptDaywiseMatIssue2?Type=SemiFGQty")%> " target="_blank">05.  Day Wise Materials Issue 2 (Semi FG Qty) </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_03_StdCost/MatAvailability?Type=SemiFG")%> " target="_blank">06.  Materials Availability-Semi FG </a>
                            </li>
                            <li class="menuheading">
                                <span>B.Production</span>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProdLoss?Type=SemFG")%> " target="_blank">01.  Production Loss Report- (Semi FG) </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_13_ProdMon/RptProdLoss?Type=SemFGqty")%> " target="_blank">02.  Production Loss Qty- (Semi FG) </a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_07_Inv/ProductionReport")%> " target="_blank">03.  Day wise production report </a>
                            </li>
                        </ul>



                    </asp:Panel>
                </asp:Panel>

                <div id="myModal" class="modal animated slideInLeft allmaterial" role="dialog" data-backdrop="static">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content  ">
                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>Department wise Requisition Approve </h4>
                                <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-arrow-left"></span></button>

                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:Image ID="SmpleIMg" Style="border: 1px solid #4779e5;" runat="server" CssClass="img img-thumbnail" />
                                    </div>
                                    <div class="col-md-9">
                                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" CssClass="table table-bordered grvContentarea">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="bg-twitter">CLIENT</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="BuyerName" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">BRAND</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblbrand" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">Size Range</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblsizernge" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">Total Order</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="TotalOrder" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="bg-twitter">Color</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblcolor" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">Article</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblarticle" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">
                                                    <asp:Label ID="lblOrdrNo" runat="server" Text="Trial Order No"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblTrialOrderNo" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">
                                                    <asp:Label runat="server" ID="lblordrqty"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="ordqty" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                        </asp:Table>
                                        <div class="row mb-3">
                                            <div class="col-md-12 table-responsive">
                                                <asp:GridView ID="gvsizes" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                    ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Style">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Color">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvColorID" runat="server" Font-Size="10px" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size-01" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF1" runat="server" BackColor="Transparent"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-02" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF2" runat="server" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-03" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF3" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-04" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF4" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-05" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF5" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF6" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-07" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF7" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-08" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF8" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-09" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF9" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-10" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF10" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-11" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF11" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-12" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF12" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-13" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF13" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-14" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF14" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-15" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF15" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size-16" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF16" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-17" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF17" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-18" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF18" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-19" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF19" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size-20" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF20" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-21" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF21" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-22" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF22" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-23" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF23" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-24" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF24" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-25" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF25" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-26" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF26" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-27" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF27" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-28" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF28" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-29" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF29" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size-30" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF30" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-31" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF31" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-32" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF32" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-33" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF33" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-34" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF34" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-35" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF35" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-36" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF36" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-37" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF37" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-38" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF38" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-39" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF39" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size-40" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvF40" runat="server"
                                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                                    Width="40px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Total">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="40px"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
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

                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvReqitem" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <a href="#" class="text-red" onclick="deleteRequisiton(event)"><span class="fa fa-trash"></span></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Code" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" ControlStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPreqno" runat="server" Style="display: none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                            Width="49px"></asp:Label>
                                                        <asp:Label ID="lblgvRsircode" runat="server" Style="display: none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmno")) %>'
                                                            Width="49px"></asp:Label>
                                                        <asp:Label ID="lblgvSpcfcod" runat="server" Style="display: none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                            Width="49px"></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Style="display: none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                            Width="49px"></asp:Label>
                                                        <%--<asp:Label ID="Label2" runat="server" style="display:none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                            Width="49px"></asp:Label>--%>
                                                        <%--<asp:Label ID="Label3" runat="server" style="display:none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                            Width="49px"></asp:Label>--%>
                                                        <%--<asp:Label ID="Label4" runat="server" style="display:none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                            Width="49px"></asp:Label>--%>
                                                        <%--<asp:Label ID="Label5" runat="server" style="display:none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqty")) %>'
                                                            Width="49px"></asp:Label>--%>
                                                        <asp:Label ID="Label6" runat="server" Style="display: none"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdate")) %>'
                                                            Width="49px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Material Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvprodesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                            Width="250"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvstyledesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="150"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvUnit" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cons Pair">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvConsPair" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "consppair")).ToString("#,##0.0000;(#,##0.0000); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <%--<FooterTemplate>
                                                        <asp:LinkButton ID="lbtnReclt" runat="server" CssClass="btn btn-sm btn-outline-primary" >Recalculate</asp:LinkButton>
                                                    </FooterTemplate>--%>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Budget with<br/>Allowance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbgdallnc3" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.0000;(#,##0.0000);") %>'
                                                            Width="90"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total<br/>additional">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbgttladdtion" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttladdtion")).ToString("#,##0.0000;(#,##0.0000);") %>'
                                                            Width="80"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Additional<br/> percentage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbgaddprcnt" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "addprcnt")).ToString("#,##0.0000;(#,##0.0000);") + " %" %>'
                                                            Width="80"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ructting<br/>Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvbgdallnc1" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlrecut")).ToString("#,##0.0000;(#,##0.0000);") %>'
                                                            Width="80"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Recutting %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReqQty2" runat="server" CssClass="text-red" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recutprcnt")).ToString("#,##0.0;(#,##0.0);") %>'
                                                            Width="80"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Req Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lblReqQty1" runat="server" Style="text-align: right" BackColor="Transparent" BorderStyle="None"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0.0000;(#,##0.0000);") %>'
                                                            Width="90"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80" />
                                                    <FooterStyle HorizontalAlign="Right" Width="80" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <a href="#" class="text-blue" onclick="updateRequisiton(event)">&nbsp; Update</a>
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
                            <div class="modal-footer ">
                                <asp:Label ID="lblPreqno" ClientIDMode="Static" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblmlccod" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lbldept" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblreqdate" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblreqtype" ClientIDMode="Static" runat="server" Style="display: none"></asp:Label>

                                <asp:CheckBox ID="ChkAllPendingDprApprove" runat="server" />Check to Approve this Department for All Associate DPR
                                <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClick="lblbtnSave_Click" OnClientClick="CloseModal()">Approve</asp:LinkButton>
                                <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal" style="margin-left: 10px">Close</button>

                            </div>
                        </div>
                    </div>
                </div>
                <!--------end modal----------------------------->
            </div>
            <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
            <script src="../Scripts/jquery.counterup.min.js"></script>
            <script>
                jQuery(document).ready(function ($) {
                    $('.counter').counterUp({
                        delay: 10,
                        time: 1000
                    });
                });
            </script>
        </div>
    </div>

    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>


