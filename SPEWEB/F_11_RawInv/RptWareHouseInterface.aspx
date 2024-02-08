<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptWareHouseInterface.aspx.cs" Inherits="SPEWEB.F_11_RawInv.RptWareHouseInterface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .InBox {
            color: red !important;
        }

        .menuheading {
            font-size: 16px;
            color: darkcyan;
            padding-left: 10px;
            font-weight: bold;
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
                width: 135px;
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


        .tbMenuWrp table tr td input[type="checkbox"], .tbMenuWrp input[type="radio"] {
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
            width: 100px;
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

        function ShowWindow(url) {

            window.open(url, '_blank');
        }

        function OpenArvmodal() {

            $('#Arvmodal').modal('show');
        }

        function CLoseMOdal() {

            $('#Arvmodal').modal('hide');
        }
        function OpenStoreModal() {

            $('#StoreModal').modal('show');
        }
        function CLoseStoreModal() {

            $('#StoreModal').modal('hide');
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

                case "gvgstorec":

                    $('#<%=gvgstorec.ClientID %>').find("input:checkbox").each(function () {

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

                case "gvArvDetails":

                    $('#<%=gvArvDetails.ClientID %>').find("input:checkbox").each(function () {
                        if ((this).disabled == false) {
                            if (this != chk) {
                                this.checked = chk.checked;
                            }
                        }
                    });

                    break;
            }


        }

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });



        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {

                case "gvAchList":
                    tblData = document.getElementById("<%=gvAchList.ClientID %>");
                    break;

                case "gvArrivList":
                    tblData = document.getElementById("<%=gvArrivList.ClientID %>");
                    break;

                case "gvIncomList":
                    tblData = document.getElementById("<%=gvIncomList.ClientID %>");
                    break;

                case "grvQC":
                    tblData = document.getElementById("<%=grvQC.ClientID %>");
                    break;

                case "gvStorRcv":
                    tblData = document.getElementById("<%=gvStorRcv.ClientID %>");
                    break;

                case "grvProIssue":
                    tblData = document.getElementById("<%=grvProIssue.ClientID %>");
                    break;

                case "gvPromData":
                    tblData = document.getElementById("<%=gvPromData.ClientID %>");
                    break;

                case "gvFGDeliv":
                    tblData = document.getElementById("<%=gvFGDeliv.ClientID %>");
                    break;

                case "PnlFGChln":
                    tblData = document.getElementById("<%=PnlFGChln.ClientID %>");
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

            let options = { ScrollHeight: 300 };
            let gvQC = $('#<%=this.grvQC.ClientID %>');
            gvQC.Scrollable(options);

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>


    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
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
                                <label class="control-label" for="FromDate">Date</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To</label>
                                <asp:TextBox ID="TxtToDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_TxtToDate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="TxtToDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <label>Season </label>
                            <div class="form-group">
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" Style="height: 32px;" runat="server" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">

                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">Article No</asp:ListItem>
                                <asp:ListItem Style="margin-left:15px" Value="1">L/C No</asp:ListItem>
                                <asp:ListItem Style="margin-left:15px" Value="2">Supplier</asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:DropDownList ID="ddlLCName" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                            <asp:Label ID="lblLCdesc" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="btnSetup" runat="server" CssClass="margin-top30px btn btn-success btn-sm" OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                                 <asp:LinkButton ID="lnkInteface" runat="server" CssClass="margin-top30px btn btn-secondary btn-sm" OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="lnkReports" runat="server" CssClass="margin-top30px btn btn-warning btn-sm" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                        </div>

                        <div class="col-md-1">
                            <div class="margin-top30px btn-group btn-group-sm" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger btn-sm">Operations</button>
                                <div class="btn-group btn-group-sm" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <%--<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Sample Inquiry</asp:HyperLink>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" CssClass="dropdown-item">Re-Order</asp:HyperLink>--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 300px;">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <%--                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000"></asp:Timer>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            </triggers>
                            --%>

                            <asp:Panel ID="PnlInt" runat="server" Visible="false">


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
                                                </asp:RadioButtonList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </fieldset>

                                <div class="">

                                    <asp:Panel ID="pnlAchList" runat="server" Visible="false">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAchList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvAchList_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchpactdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Store Name" onkeyup="Search_Gridview(this,1, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactdesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchssircdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblssircdesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchReqno" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req. No" onkeyup="Search_Gridview(this,3, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lblreqno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchorderno" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="P.O / L.C" onkeyup="Search_Gridview(this,4, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lblorderno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtCustompono" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Custom PO" onkeyup="Search_Gridview(this,5, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCustomPono" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="65px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,17, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvorderdat" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchOrdqty" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Order Qty" onkeyup="Search_Gridview(this,7, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrqty" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,8, 'gvAchList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactcode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblorderno" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblreqno" runat="server" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblReqType" runat="server" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                Width="80px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <div class="dropdown">
                                                                <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                    Action
                                                                              
                                                                </button>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:HyperLink ID="HyReqPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"> Requsition</span>                                                                           </asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HypPOPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>P.O</asp:HyperLink></li>

                                                                </ul>
                                                            </div>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton OnClick="lnkCheckAll_Click" ToolTip="Click to Approval All Pending Arrival" OnClientClick="return confirm('Do You Agree to All Pending Arrival?')" ID="lnkCheckAll" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-check-double"></span></asp:LinkButton>

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton OnClick="lnkCheck_Click" OnClientClick="return confirm('Do You Agree to Arrival?')" ID="lnkCheck" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-check"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />

                                            </asp:GridView>
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="pnlArvList" runat="server" Visible="false">

                                        <div class="table-responsive col-lg-12" style="min-height: 450px;">
                                            <asp:GridView ID="gvArrivList" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: center"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchpactdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Store Name" onkeyup="Search_Gridview(this,1, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactdesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchssircdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblssircdesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="galLblReqNo" runat="server" AutoCompleteType="Disabled"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchReqno" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Req. No" onkeyup="Search_Gridview(this,3, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreqno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchCustPo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Custom PO" onkeyup="Search_Gridview(this,4, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LbloCustPon" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="galLblPo" runat="server" AutoCompleteType="Disabled"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchorderno" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="P.O / L.C" onkeyup="Search_Gridview(this,5, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lblorderno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="65px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,6, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvorderdat" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchOrdqty" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Order Qty" onkeyup="Search_Gridview(this,7, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrqty" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this,8, 'gvArrivList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactcode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblorderno" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblreqno" runat="server" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblReqType" runat="server" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <div class="dropdown">
                                                                <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                    Action
                                                                </button>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:HyperLink ID="HyReqPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false">
                                                                            <span class="fa fa-print mr-1"></span> Requsition
                                                                        </asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HypPOPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false">
                                                                            <span class="fa fa-print mr-1"></span> P.O.
                                                                        </asp:HyperLink>
                                                                    </li>

                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Arival">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LbtnArivUpdate" OnClick="LbtnArivUpdate_Click" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                </Columns>

                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />

                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PnlIncom" runat="server" Visible="false">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvIncomList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvIncomList_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchpactdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="140px" placeholder="Store Name" onkeyup="Search_Gridview(this,1, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblpactcode" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'></asp:Label>
                                                            <asp:Label ID="lblgvpactdesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="170px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchssircdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblssircdesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchReqno" BackColor="Transparent" BorderStyle="None" runat="server" Width="200px" placeholder="Req. No" onkeyup="Search_Gridview(this,3, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreqno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                Width="220px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchorderno" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="P.O / L.C" onkeyup="Search_Gridview(this,4, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lnkbtngviclpoModal" Font-Size="X-Small" Width="130px" OnClick="lnkbtngviclpoModal_Click"
                                                                CommandArgument='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'></asp:LinkButton>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchCustPo" BackColor="Transparent" BorderStyle="None" runat="server" Width="180px" placeholder="Custom PO" onkeyup="Search_Gridview(this,5, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LbloCustPon" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon")) %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Date">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="65px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this, 6, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvorderdat" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchOrdqty" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="Order Qty" onkeyup="Search_Gridview(this,7, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrqty" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "orderqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchmrrqty" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Incoming Qty" onkeyup="Search_Gridview(this,8, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmrrqty" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchbalqty" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Bal. Qty" onkeyup="Search_Gridview(this, 9, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbalqty" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="X-Small" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this, 10, 'gvIncomList')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>' Font-Size="X-Small"
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactcode" runat="server" Visible="false" Height="16px" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblorderno" runat="server" Visible="false" Height="16px" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblreqno" runat="server" Height="16px" Font-Size="X-Small"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                                Width="80px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvitemcount" runat="server" BackColor="Transparent"
                                                                BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="X-Small"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itemcount")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>


                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkCheck" Target="_blank" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-check"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />

                                            </asp:GridView>
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="PanelQc" Visible="false" runat="server">

                                        <div class="table-responsive">

                                            <asp:GridView ID="grvQC" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvQC_RowDataBound">
                                                <RowStyle />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="orderno#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCgvreqno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode"))%>' Width="15px"></asp:Label>
                                                            <asp:Label ID="lblqcgvmrrno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno"))%>' Width="15px"></asp:Label>
                                                            <asp:Label ID="lblgvorderno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Store Name">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtStrNm" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Store" onkeyup="Search_Gridview(this, 1, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Supplier">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtSupp" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Supplier" onkeyup="Search_Gridview(this, 2, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>


                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsuppliermrr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Received <br>  Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvorderdat" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Receved No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtRcvNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Receive No" onkeyup="Search_Gridview(this, 4, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvorderno1" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1"))%>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req. No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Requisition No" onkeyup="Search_Gridview(this, 5, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="P.O / L.C">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtPoLc" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="P.O / L.C" onkeyup="Search_Gridview(this, 6, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="Lblorderno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Custom PO">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtCustPo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Custom PO" onkeyup="Search_Gridview(this, 7, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="LbloCustPon2" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Mrf No.">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtMrfNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Mrf No." onkeyup="Search_Gridview(this, 7, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmrfno" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                        <HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <FooterStyle Width="80px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Challan No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvqctxtChlnNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Challan No" onkeyup="Search_Gridview(this, 8, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvchlnno" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                        <HeaderStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Top" />
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
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MR Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqcmrqamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0);") %>'
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
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ready QC Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvchlnqtyqc" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqcqty")).ToString("#,##0;(#,##0);") %>'
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
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqcamt")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvQCFWoamtda" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this, 14, 'grvQC')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvseason" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "season")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="right" Width="80px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="80px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <div class="btn-group">

                                                                <button type="button" class="btn btn-default btn-xs   dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                    Action  
                                                                </button>

                                                                <ul class="dropdown-menu xDropdn">
                                                                    <li>
                                                                        <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span> Print
                                                                        </asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span> QC Entry
                                                                        </asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span> REC Edit
                                                                        </asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton ID="btnDelexitsRecv" CssClass="btn btn-xs btn-default" OnClick="btnDelexitsRecv_Click" runat="server"><span style="color:red" class="fa fa-recycle"></span> Delete</asp:LinkButton>

                                                                    </li>

                                                                </ul>

                                                            </div>



                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="PanelStoreRcv" Visible="false" runat="server">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvStorRcv" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvStorRcv_RowDataBound">
                                                <RowStyle />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNoqc" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
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

                                                    <asp:TemplateField HeaderText="Store Name">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrrcvtxtStrNm" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Store Name" onkeyup="Search_Gridview(this, 1, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Supplier">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrrcvtxtSupplier" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Supplier" onkeyup="Search_Gridview(this, 2, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsupplierbillqc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Recevied <br>  Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvmrrdat" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcdat1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="P.O / L.C">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrrcvtxtPoLc" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="P.O / L.C" onkeyup="Search_Gridview(this, 4, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="Lblorderno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Custom PO">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrtxtCustPo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Custom PO" onkeyup="Search_Gridview(this, 5, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="LbstrCustPon2" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custompon")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Challan No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrrcvtxtChln" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Challan No" onkeyup="Search_Gridview(this, 6, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvchlnno" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno"))%>' Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QC No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrrcvtxtQcNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="QC No" onkeyup="Search_Gridview(this, 7, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmrrno1qc" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purqcno1"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Req. No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrrcvtxtReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req. No" onkeyup="Search_Gridview(this, 8, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1qc" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Mrf No." Visible="false">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvstrrcvtxtMrfNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Mrf No" onkeyup="Search_Gridview(this, 9, 'gvStorRcv')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmrfnoqc" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactcodeqc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Resource</br> Count">
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
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqcamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "purqcamt")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvFmrramt" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="season" onkeyup="Search_Gridview(this, 12, 'gvStorRcv')"></asp:TextBox><br />
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
                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelQC" CssClass="btn btn-xs btn-default" OnClick="btnDelQC_Click" OnClientClick="return confirm('Do you want to delete this item?');" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlMatIssue" runat="server" Visible="false">
                                        <div class="table-responsive">

                                            <asp:GridView ID="grvProIssue" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvProIssue_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
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

                                                    <asp:TemplateField HeaderText="Req. Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpreqdate" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req. </br> Number">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvproisutxtReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Req. Number" onkeyup="Search_Gridview(this, 2, 'grvProIssue')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lgpreqno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Name">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvproisutxtOrdrNm" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order Name" onkeyup="Search_Gridview(this, 3, 'grvProIssue')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmlcdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Article No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvproisutxtArtNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Article No" onkeyup="Search_Gridview(this, 4, 'grvProIssue')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvartno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="140px" />
                                                        <ItemStyle Width="140px" />
                                                        <FooterStyle Width="140px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvfbordno" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="FB Order No" onkeyup="Search_Gridview(this, 5, 'grvProIssue')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvfbordno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fbordno")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="140px" />
                                                        <ItemStyle Width="120px" />
                                                        <FooterStyle Width="120px" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Order Type">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvfbordType" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order Type" onkeyup="Search_Gridview(this, 6, 'grvProIssue')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvfbordType" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordertype")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="140px" />
                                                        <ItemStyle Width="80px" />
                                                        <FooterStyle Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FG Qty." ItemStyle-HorizontalAlign="right">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblgvFgQty" Width="70px" CssClass="pr-2" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fgqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Trial Order No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTrialorderno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trialorder")) %>'
                                                                Width="100px"></asp:Label>
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
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Issue</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvisueqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isueqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblQTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bal</br> qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvbalqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:Label ID="lblPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvtxtType" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Type" onkeyup="Search_Gridview(this, 11, 'grvProIssue')"></asp:TextBox><br />
                                                        </HeaderTemplate>
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
                                                             <asp:LinkButton runat="server" ID="LbtnIssueMulti" OnClick="LbtnIssueMulti_Click" ToolTip="Selected Multiple REQ Issue" CssClass="btn btn-sm btn-success small mr-2">
                                                                <i class="fa fa-check"></i>
                                                            </asp:LinkButton>

                                                            <asp:LinkButton runat="server" ID="lnkbtnPrintCombined" CssClass="btn btn-sm btn-primary small mr-2" ToolTip="Selected Multiple Req Print" OnClick="lnkbtnPrintCombined_Click">
                                                                <i class="fa fa-print"></i>
                                                            </asp:LinkButton>

                                                            <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes('grvProIssue', this);" ClientIDMode="Static" runat="server" />
                                                           </div>
                                                            
                                                        </HeaderTemplate>

                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" Visible="false" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:CheckBox runat="server" ID="chkPrintCombined" />

                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvreqdate" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Pro code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvProCode" runat="server" Style="text-align: right"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "procode") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Day ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgrvDayId" runat="server" Style="text-align: right"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "dayid") %>'
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

                                    </asp:Panel>

                                    <asp:Panel ID="pnlIndentIssue" runat="server">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvPromData" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="gvPromData_RowDataBound"
                                                CssClass="table-striped  table-hover table-bordered grvContentarea" AllowPaging="false">
                                                <RowStyle />

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="15px" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldate" runat="server"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "issuedat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Challan No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvprmdtChlnNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Challan No" onkeyup="Search_Gridview(this, 2, 'gvPromData')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblissueno" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issueno")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Card No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvprmdtCardNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Card No" onkeyup="Search_Gridview(this, 3, 'gvPromData')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvTeamCard" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <table style="width: 250px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="tbhead" runat="server">Name of Person</asp:Label>
                                                                    </td>

                                                                    <td>
                                                                        <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>


                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrsirdesc" runat="server" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Department">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvprmdtDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Department" onkeyup="Search_Gridview(this, 5, 'gvPromData')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDept" runat="server" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptname")) %>'
                                                                Width="130px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Qty" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblissueqty" runat="server"
                                                                Font-Size="10px"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFissueqty" runat="server" Font-Size="10px"
                                                                ForeColor="#000" Width="60px"></asp:Label>
                                                        </FooterTemplate>

                                                        <ItemStyle HorizontalAlign="right" />
                                                        <HeaderStyle VerticalAlign="Top" />
                                                        <HeaderStyle HorizontalAlign="right" />
                                                        <FooterStyle HorizontalAlign="right" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ToolTip="Edit And Approve" ID="BtnEdit" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            <asp:HyperLink ID="HypRDDoPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="gvNodataFound" style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">
                                                        <h4 class="display-1"><i class="fa  fa-spin fa-cog fa-3x"></i>
                                                            <br />
                                                            No records to display. </h4>
                                                    </div>
                                                </EmptyDataTemplate>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />

                                            </asp:GridView>
                                        </div>

                                    </asp:Panel>

                                    <asp:Panel ID="PnlgStrec" Visible="false" runat="server">

                                        <div class="table-responsive">
                                            <asp:GridView ID="gvgstorec" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvgstorec_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwipid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="49px"></asp:Label>
                                                            <asp:Label ID="lblprqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                Width="49px"></asp:Label>
                                                            <asp:Label ID="lgrvoDayId" runat="server" Style="text-align: right"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "odayid") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Preq No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgrcvPreqNo" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Preq No" onkeyup="Search_Gridview(this, 1, 'gvgstorec')"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                                Width="100px"></asp:Label>
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

                                                    <asp:TemplateField HeaderText="Order Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgrcvOrdrNm" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Order Name" onkeyup="Search_Gridview(this, 3, 'gvgstorec')"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorproid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="220px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Style Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgrcvStyleNm" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Style Name" onkeyup="Search_Gridview(this, 4, 'gvgstorec')"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstyle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Article  No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvArticleno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="80px"></asp:Label>
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
                                                        <HeaderTemplate>
                                                            <div class="form-inline justify-content-center">
                                                                <asp:LinkButton runat="server" ID="LbtnFGMulti" OnClick="LbtnFGMulti_Click" ToolTip="Selected Multiple FG" CssClass="btn btn-sm btn-success small mr-2">
                                                                <i class="fa fa-check"></i>
                                                                </asp:LinkButton>
                                                                <asp:CheckBox ID="chkhead1" onclick="javascript:SelectAllCheckboxes('gvgstorec', this);" ClientIDMode="Static" runat="server" />
                                                            </div>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:CheckBox runat="server" ID="chkPrintCombinedFG" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="130px" HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                        <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>

                                    </asp:Panel>
                                    
                                    <asp:Panel ID="pnlFGDeliv" runat="server" Visible="false">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvFGDeliv" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvFGDeliv_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwipid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="49px"></asp:Label>
                                                            <asp:Label ID="lblprqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ref No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvfgdelRef" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Ref. No" onkeyup="Search_Gridview(this, 1, 'gvFGDeliv')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRefno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invrefno")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Inv No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvfgdelInv" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Inv. No" onkeyup="Search_Gridview(this, 2, 'gvFGDeliv')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno1")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date ">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorprodat" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order Name">

                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="gvfgdelOrdrNm" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" Width="100%"
                                                                placeholder="Order Name" onkeyup="Search_Gridview(this, 4, 'gvFGDeliv')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorproid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="220px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Style Name">

                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgdelStyleNm" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Style Name" onkeyup="Search_Gridview(this, 5, 'gvFGDeliv')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstyle" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article  No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvArticleno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Export</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvordrqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0);") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblstrTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Challan">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HypDelivery" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />

                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="PnlFGChln" runat="server" Visible="false">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvFGChln" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvFGChln_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvwipid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                Width="49px"></asp:Label>
                                                            <%-- <asp:Label ID="lblprqno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                                                                Width="49px"></asp:Label>--%>
                                                            <asp:Label ID="lbldchno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dchno")) %>'
                                                                Width="49px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Challan No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgchlnChlnNo" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Challan No" onkeyup="Search_Gridview(this, 1, 'PnlFGChln')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdchno1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dchno1")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Challan Ref">

                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgchlnChlnNo" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Challan No" onkeyup="Search_Gridview(this, 2, 'PnlFGChln')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvchlrefn" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                Width="160px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date ">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdeldate" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deldate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Order  Name">

                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgchlnOrdrNm" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Order Name" onkeyup="Search_Gridview(this, 4, 'PnlFGChln')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstorproid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="220px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Buyer">

                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgchlnBuyer" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Buyer" onkeyup="Search_Gridview(this, 5, 'PnlFGChln')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcustdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Challan</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvtotlprs" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0);") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblstrTotal" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Challan</br> CTN">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvtotlctn" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlctn")).ToString("#,##0;(#,##0);") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFtotlctn" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Despatch">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdespatchtype" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "despatchtype")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vehicle No">

                                                        <HeaderTemplate>
                                                            <asp:TextBox runat="server" ID="gvfgchlnViclNo" BackColor="Transparent" BorderStyle="None" CssClass="text-center" Width="100%"
                                                                placeholder="Vehicle No" onkeyup="Search_Gridview(this, 9, 'PnlFGChln')"></asp:TextBox>
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvvehclno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vehclno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>




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
                                    </asp:Panel>

                                </div>


                            </asp:Panel>


                            <asp:Panel ID="pnlReprots" runat="server">

                                <asp:Panel ID="plnMgf" runat="server" CssClass="row" Visible="false">
                                    <div class="col-md-4">
                                        <ul class="list-unstyled">

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

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptLCPosition?Type=LCPosition")%> " target="_blank">10. LC Status Report</a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="list-unstyled">

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptLCStatus?Type=LCVari")%> " target="_blank">11. LC Variance Reports</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptSalSummery?Type=LcReceive")%> " target="_blank">12. LC Received Reports</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%> " target="_blank">13. BOM Approved List</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/InvReport?InputType=QuantityB")%> " target="_blank">14. Inventory Report</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/RptIndProStock?Type=MatHis")%> " target="_blank">15. Materials History </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_15_Pro/DateWiseMatIssue?Type=DateWise")%> " target="_blank">16. Date Wise Material Issue </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/InvReport?InputType=QuantityB")%> " target="_blank">17. Inventory Report - Quantity Basis </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/InvReport?InputType=MatUnused")%> " target="_blank">18. Periodic Material Unused Report </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/InvReport?InputType=OrdwiseStk")%> " target="_blank">19. Article Wise Material Stock </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_04_Sampling/RptPdBook?Type=SamReport")%> " target="_blank">20. Day Wise Sample Report </a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-md-4">
                                        <ul class="list-unstyled">

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_05_ProShip/RptOrderStatus?Type=MatMaster")%> " target="_blank">21. Material Master Report</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptOrderVsReceive?Type=OrderVsRec")%> " target="_blank">22. BOM Vs. Receive </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptOrderVsReceive?Type=BomMatSummary")%> " target="_blank">23. BOM Wise Material Summary </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_09_Commer/RptSeasonWiseOrder?Type=MasterPOR")%> " target="_blank">24. Master PO Report</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_11_RawInv/RptMaterialTrans?Type=MatTransfer")%> " target="_blank">25. Material Transfer Report </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_10_Procur/RptSupplierDueStatus?Type=PromMatHis")%> " target="_blank">26. Indent Materials Distribution </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_15_Pro/DateWiseMatIssue?Type=DateWise")%> " target="_blank">27. Date Wise Material Issue </a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_15_Pro/RptProductionConsumption?Type=Daywise")%> " target="_blank">28. Day Wise Material Consumption Report </a>
                                            </li>
                                        </ul>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PnlSalesSetup" runat="server" Visible="false">
                                    <ul class="list-unstyled">
                                        <li>
                                            <a href="<%=this.ResolveUrl("~/F_11_RawInv/StockAdjstmnt?Type=Entry&centrid=&batchcode=&date=")%> " target="_blank">01. Material Stock Adjustment Entry</a>
                                        </li>

                                    </ul>

                                </asp:Panel>
                            </asp:Panel>

                            <div id="Arvmodal" class="modal" role="dialog">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content modal-lg">
                                        <div class="modal-header">

                                            <h4 class="modal-title"><span class="fa fa-table"></span>Details Information </h4>
                                            <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-arrow-left"></span></button>
                                        </div>
                                        <div class="modal-body form-horizontal">

                                            <asp:GridView ID="gvArvDetails" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                                <PagerSettings Visible="False" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Description of Materials">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmlcdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSpfDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Order Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvRecvqty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Free Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvFreeqty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="ChkAll" ClientIDMode="Static" runat="server" onclick="javascript:SelectAllCheckboxes('gvArvDetails', this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkack" ClientIDMode="Static" runat="server" CssClass="chkack"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? true : false %>'
                                                                Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? false : true%>'
                                                                Width="20px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <FooterStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvorderno" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblrsircode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblspcfcod" runat="server" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblgp" runat="server" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gp")) %>'
                                                                Width="80px"></asp:Label>


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
                                        <div class="modal-footer ">
                                            <asp:LinkButton ID="ModalUpdateBtn" OnClick="ModalUpdateBtn_Click" OnClientClick="CLoseMOdal();"
                                                runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div id="StoreModal" class="modal" role="dialog">
                                <div class="modal-dialog">
                                    <div class="modal-content ">
                                        <div class="modal-header">

                                            <h4 class="modal-title"><span class="fa fa-table"></span>Change Store</h4>
                                        </div>
                                        <div class="modal-body form-horizontal">
                                            <asp:Label runat="server" ID="lblHelper" Visible="false"></asp:Label>
                                            <p class="text-red" style="font-size: 11px">Warning : Changing this store name will change store name for all relevant requisition</p>
                                            <div class="form-group col-10">
                                                <asp:Label ID="lblstorid" runat="server" CssClass="label">Store Id:</asp:Label>

                                                <asp:DropDownList ID="ddlStorid" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="modal-footer ">
                                            <asp:LinkButton ID="lnkbtnUpdateStore" OnClick="lnkbtnUpdateStore_Click" OnClientClick="CLoseStoreModal();"
                                                runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
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

                        </ContentTemplate>

                    </asp:UpdatePanel>

                </div>
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>


    <script>

        function openModal() {

            $('#myModal').modal('toggle');
        }

    </script>

    <%--<triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>
    </triggers>--%>

    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

