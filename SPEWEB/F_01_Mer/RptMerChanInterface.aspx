<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMerChanInterface.aspx.cs" Inherits="SPEWEB.F_01_Mer.RptMerChanInterface" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
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

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvSmpleinqlist":
                    tblData = document.getElementById("<%=gvSmpleinqlist.ClientID %>");
                    break;
                case "gvConSheet":
                    tblData = document.getElementById("<%=gvConSheet.ClientID %>");
                    break;
                case "gvPreCost":
                    tblData = document.getElementById("<%=gvPreCost.ClientID %>");
                    break;
                case "gvOrdAcRej":
                    tblData = document.getElementById("<%=gvOrdAcRej.ClientID %>");
                    break;
                case "gvOrdDetails":
                    tblData = document.getElementById("<%=gvOrdDetails.ClientID %>");
                    break;
                case "gvOrdDetailsApp":
                    tblData = document.getElementById("<%=gvOrdDetailsApp.ClientID %>");
                    break;
                case "gvBOMGen":
                    tblData = document.getElementById("<%=gvBOMGen.ClientID %>");
                    break;
                case "gvBOMApp":
                    tblData = document.getElementById("<%=gvBOMApp.ClientID %>");
                    break;
                case "gvProCom":
                    tblData = document.getElementById("<%=gvProCom.ClientID %>");
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
            var comcod = <%=this.GetCompCode()%>;
            //console.log(comcod);
            switch (comcod) {
                case 5305:   // FB  
                case 5306:   // Footbed 

                    $(".tbMenuWrp table tr td:nth-child(1)").hide();//sample entry
                    $(".tbMenuWrp table tr td:nth-child(2)").hide();//sample entry
                    $(".tbMenuWrp table tr td:nth-child(3)").hide();//sample entry
                    break;
            }

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });

            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>


    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

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
                        <asp:Label runat="server" ID="LblFromDate" class="label" for="FromDate">From Date</asp:Label>
                        <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" ID="LblTodate" class="label" for="ToDate">To Date</asp:Label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                    </div>
                </div>

                <div class="col-md-1">
                    <div class="form-group">
                        <asp:Label runat="server" ID="LblSEason" class="label" for="ToDate">Season</asp:Label>
                        <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-1">
                    <div class="form-group" style="margin-top: 20px">
                        <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="btn-sm btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                    </div>
                </div>

                <div class="col-md-3">
                    <div class="form-group" style="margin-top: 20px">
                        <asp:LinkButton ID="btnSetup" runat="server" CssClass=" btn btn-success btn-sm" OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                                 <asp:LinkButton ID="lnkInteface" runat="server" CssClass=" btn btn-secondary btn-sm " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                        <asp:LinkButton ID="lnkReports" runat="server" CssClass=" btn btn-warning btn-sm" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class=" btn-group btn-group-sm" style="margin-top: 20px" role="group" aria-label="Button group with nested dropdown">
                        <button type="button" class="btn btn-danger btn-sm">Operations</button>
                        <div class="btn-group btn-group-sm" role="group">
                            <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                            <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                <div class="dropdown-arrow"></div>
                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Sample Inquiry</asp:HyperLink>
                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" CssClass="dropdown-item">Re-Order</asp:HyperLink>
                                <%--<asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Pro/PurInformation.aspx" CssClass="dropdown-item">Dashboard</asp:HyperLink>--%>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </div>


    <div class="card card-fluid">
        <div class="card-body" style="min-height:250px">

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
                                <div class="clearfix"></div>
                            </fieldset>
                            <div>

                                <asp:Panel ID="pnlallInqList" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvSmpleinqlist" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvSmpleinqlist_RowDataBound">
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

                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="CLIENT NAME" onkeyup="Search_Gridview(this,1, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="110px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SEASON">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchseason" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="SEASON" onkeyup="Search_Gridview(this,2, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseason" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "seasondesc")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Brand Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBRAND" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="BRAND NAME" onkeyup="Search_Gridview(this,3, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblBrnd" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brandesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="IMAGE">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="STYLE" onkeyup="Search_Gridview(this,5, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="ARTICLE" onkeyup="Search_Gridview(this,6, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchOrdqty" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="EST. QTY" onkeyup="Search_Gridview(this,7, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvordrqty" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="lblgvautoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,8, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="70px" BorderStyle="None" runat="server" placeholder="SIZE RANGE" onkeyup="Search_Gridview(this,9, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="60px" BorderStyle="None" runat="server" placeholder="SAM SIZE" onkeyup="Search_Gridview(this,10, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="60px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="CON SIZE">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="60px" BorderStyle="None" runat="server" placeholder="CON SIZE" onkeyup="Search_Gridview(this,11, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="60px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ATTACH </br>WITH INFO.">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="65px" runat="server" placeholder="DATE" onkeyup="Search_Gridview(this,17, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="65px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkEdit" Target="_blank" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <div class="dropdown">
                                                                    <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                        Action
                                                                             
                                                                    </button>
                                                                    <ul class="dropdown-menu dropdown-menu-right">
                                                                        <li>
                                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"> Inquiry</span> </asp:HyperLink>
                                                                        </li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HypCondir" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Consumption Direct</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HypConcom" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Consumption Common</asp:HyperLink></li>

                                                                        <li>
                                                                            <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyCommPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                        <%--<li><asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>--%>

                                                                        <%--<li><asp:HyperLink ID="HyFOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink></li>--%>
                                                                        <%--<li><asp:HyperLink ID="HyLOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink></li>--%>
                                                                    </ul>
                                                                </div>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkCheck" Target="_blank" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnSamDelInq" runat="server" OnClick="btnSamDelInq_Click" ToolTip="Reverse" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>
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

                                <asp:Panel ID="pnlConShet" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvConSheet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvConSheet_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Client Name" onkeyup="Search_Gridview(this,1, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,2, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,3, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtautoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,4, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size Range">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,6, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Samp Size" onkeyup="Search_Gridview(this,7, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Consumption Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,8, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attach </br>with Info.">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,13, 'gvConSheet')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lbtnCons" Target="_blank" ToolTip="Consumption" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyConsPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                                <asp:HyperLink ID="HyCommConsPrint" runat="server" ToolTip="Common Materials" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkCheck" Target="_blank" ToolTip="Consumption Approval" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelInq" runat="server" OnClick="btnDelInq_Click" ToolTip="Reverse" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>
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
                                <asp:Panel ID="PanPreCost" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvPreCost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvPreCost_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Client Name" onkeyup="Search_Gridview(this,1, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,2, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,3, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtautoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,4, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size Range">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,6, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Samp. Size" onkeyup="Search_Gridview(this,7, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Consumption Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,8, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attach. </br>with Info">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,14, 'gvPreCost')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lbtnCost" Target="_blank" ToolTip="Consumption" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                                <asp:HyperLink ID="HyCommPreCostPrint" runat="server" ToolTip="Common Materials" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkCheck" Target="_blank" ToolTip="Pre-Costing Approval" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelCons" runat="server" OnClick="btnDelCons_Click" ToolTip="Reverse" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                <asp:Panel ID="PanlOrdAcRej" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvOrdAcRej" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvOrdAcRej_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Client Name" onkeyup="Search_Gridview(this,1, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,2, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,3, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,4, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size Range">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,6, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Samp. Size" onkeyup="Search_Gridview(this,7, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Consumption Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,8, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attach </br>with Info.">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,14, 'gvOrdAcRej')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Accept <br>Reject">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnLink" OnClick="lbtnLink_Click" ToolTip="Accept/Reject" runat="server"><span class="fa fa-link"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Planning">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyProdPlan" Target="_blank" runat="server"><span class="fa fa-file-o"></span></asp:HyperLink>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                                <asp:HyperLink ID="HyCommPreCostPrint" runat="server" ToolTip="Common Materials" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelPreCost" OnClick="btnDelPreCost_Click" ToolTip="Reverse" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                <asp:Panel ID="PanOrdDet" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive ">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvOrdDetails" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvOrdDetails_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Client Name" onkeyup="Search_Gridview(this,1, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,2, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,3, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,4, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size Range">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,6, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Samp. Size" onkeyup="Search_Gridview(this,7, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Consumption Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,8, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attach </br>with Info.">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,14, 'gvOrdDetails')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hypbtnorder" Target="_blank" ToolTip="Order Generate" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                                <asp:HyperLink ID="HyCommPreCostPrint" runat="server" ToolTip="Common Materials" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelAc_Rej" OnClick="btnDelAc_Rej_Click" ToolTip="Reverse" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HypLcInformation" NavigateUrl='<%# "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))+"&dayid=00000000" %>' Target="_blank" ToolTip="General Information" runat="server"><span class="fa fa-th"></span></asp:HyperLink>
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
                                <asp:Panel ID="PanOrdDetApp" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvOrdDetailsApp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvOrdDetailsApp_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Client Name" onkeyup="Search_Gridview(this,1, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,2, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,3, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,4, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Possible Size Range">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,6, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Samp. Size" onkeyup="Search_Gridview(this,7, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Consumption Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,8, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attach </br>with Infor">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,14, 'gvOrdDetailsApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hypbtnorder" Target="_blank" ToolTip="Consumption" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <div class="dropdown">
                                                                    <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                        Action
                                                                              
                                                                    </button>
                                                                    <ul class="dropdown-menu">
                                                                        <li>
                                                                            <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyCommPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>

                                                                    </ul>
                                                                </div>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HypLcInformation" NavigateUrl='<%# "~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) + "&dayid=00000000" %>' Target="_blank" ToolTip="General Information" runat="server"><span class="fa fa-th"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkCheck" Target="_blank" ToolTip="Order Approval" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%-- <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelOrder" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
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
                                    </div>

                                </asp:Panel>
                                <asp:Panel ID="pnlBOMGen" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvBOMGen" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvBOMGen_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Client Name" onkeyup="Search_Gridview(this,1, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,2, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Color">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchcolor" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Color" onkeyup="Search_Gridview(this,3, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcolordesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,4, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,5, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size Range">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="80px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,7, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="80px" BorderStyle="None" runat="server" placeholder="Samp. Size" onkeyup="Search_Gridview(this,8, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Consumption Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="80px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,9, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblcolorid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lbldayid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblmlccod" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attach </br>with Info">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,14, 'gvBOMGen')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrcvdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BOM">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hypbtnMatReq" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Material Requirement" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hypbtnMatReq1" CssClass="btn btn-xs btn-default" Target="_blank" ToolTip="Order Information" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelOrde_App" CssClass="btn btn-xs btn-default" OnClick="btnDelOrde_App_Click" ToolTip="Reverse" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <div class="dropdown">
                                                                    <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                        Action                                                                             

                                                                    </button>
                                                                    <ul class="dropdown-menu dropdown-menu-right">
                                                                        <li>
                                                                            <asp:HyperLink ID="HypOrderEdit" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-edit"></span>Edit Order</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyCommPreCostPrint" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="OrderPrint" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:LinkButton ID="ReplaceThumbnail" OnClick="ReplaceThumbnail_Click" runat="server"><span class="fa fa-trash"></span>Replace Thumb</asp:LinkButton>
                                                                        </li>
                                                                    </ul>
                                                                </div>
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

                                <asp:Panel ID="PnlBomApp" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvBOMApp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvBOMApp_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Client Name" onkeyup="Search_Gridview(this,1, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,2, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Color">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchcolor" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Color" onkeyup="Search_Gridview(this,3, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcolordesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,4, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,5, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Image">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Possible Size Range">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,7, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sample Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="80px" BorderStyle="None" runat="server" placeholder="Samp. Size" onkeyup="Search_Gridview(this,8, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Consumption Size">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Cons, Size" onkeyup="Search_Gridview(this,9, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="No of Style" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvitmqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmcount")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inquiry Qty" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attac </br>with Info.">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,15, 'gvBOMApp')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvItemdescc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrcvdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hypbtnMatReqEntry" Target="_blank" ToolTip="Material Requirement Edit" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Approve">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hypbtnMatReq" Target="_blank" ToolTip="Material Requirement Approve" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <div class="dropdown">
                                                                    <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                        Action                                                                            

                                                                    </button>
                                                                    <ul class="dropdown-menu dropdown-menu-right">
                                                                        <li>
                                                                            <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyCommPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="OrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyFOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink>
                                                                        </li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyLOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink>
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelOrder" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
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
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnlProComp" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-11">
                                            <div class="row" style="max-height: 360px">
                                                <asp:GridView ID="gvProCom" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvProCom_RowDataBound">
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

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,1, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Client Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="CLIENT NAME" onkeyup="Search_Gridview(this,2, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvSupplier" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Style">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="CATEGORY" onkeyup="Search_Gridview(this,3, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="COLOR">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchColor" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="COLOR" onkeyup="Search_Gridview(this,4, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcolordesc" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="ARTICLE" onkeyup="Search_Gridview(this,5, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                    Width="60px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="IMAGE">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                </asp:HyperLink>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Size Range" Visible="false">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,7, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sample Size" Visible="false">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Samp. Size" onkeyup="Search_Gridview(this,8, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Consumption Size" Visible="false">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,9, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblMlccod" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="lblcolorid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>' Width="80px"></asp:Label>
                                                                <asp:Label ID="lbldayid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'
                                                                    Width="80px"></asp:Label>

                                                                <asp:Label ID="lblstyleid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                    Width="80px"></asp:Label>
                                                                <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                    Width="80px"></asp:Label>


                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Order No">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchOrderno" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="ORDER NO" onkeyup="Search_Gridview(this,7, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvorderno" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Order Type">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtOrderType" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="TYPE" onkeyup="Search_Gridview(this,8, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordrtype" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrtype")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>


                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ORDER <br>QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvordqty" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="REAL <br>PRICE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvPrice" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Merchantdiser" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvusername" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Attach </br>with Info.">
                                                            <ItemTemplate>
                                                                <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                            </ItemTemplate>

                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ORDER <br> RECEIVE DATE">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="RECEIVE DATE" onkeyup="Search_Gridview(this,12, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvOrderrcvdat" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrcvdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SHIPMENT DATE">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtShipdate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="SHIPMENT DATE" onkeyup="Search_Gridview(this,13, 'gvProCom')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvShipmntdat" runat="server" AutoCompleteType="Disabled"
                                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shipmntdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BOM No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblBomNo" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <div class="dropdown">
                                                                    <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                        Action
                                                                             
                                                                    </button>
                                                                    <ul class="dropdown-menu dropdown-menu-right">
                                                                        <li>
                                                                            <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order Print</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyFOrderPrint" ToolTip="Import BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink></li>
                                                                        <li>
                                                                            <asp:HyperLink ID="HyLOrderPrint" ToolTip="Locl BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink>
                                                                        </li>
                                                                        <li>
                                                                            <asp:LinkButton ID="ReplaceThumbnail" OnClick="ReplaceThumbnail_Click2" runat="server"><span class="fa fa-trash"></span>Replace Thumb</asp:LinkButton>
                                                                        </li>
                                                                    </ul>
                                                                </div>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--  <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelOrder" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
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
                                    </div>
                                </asp:Panel>



                            </div>
                        </div>

                    </div>
                </asp:Panel>


                <asp:Panel ID="pnlReprots" runat="server">

                    <asp:Panel ID="plnMgf" runat="server" Visible="false">

                        <ul class="list-unstyled">
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%> " target="_blank">01. BOM Approved List</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_03_CostABgd/RptLCStuatus?Type=PeriodicOrderSt")%> " target="_blank">02. Periodic Order Status</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_05_ProShip/RptOrderStatus?Type=OrdStatus")%> " target="_blank">03. Order Status Report</a>
                            </li>
                              <li>
                                <a href="<%=this.ResolveUrl("~/F_01_Mer/RptPfiInvList?Type=PfiRpt")%> " target="_blank">04. Proforma Invoice List</a>
                            </li>
                              <li>
                                <a href="<%=this.ResolveUrl("~/F_01_Mer/SampleInquiryLIst?Type=Sample")%> " target="_blank">06. Sample Inquiry List</a>
                            </li>
                        </ul>
                    </asp:Panel>

                    <asp:Panel ID="PnlSalesSetup" runat="server" Visible="false">

                        <ul class="list-unstyled">
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/AccProjectCode")%> " target="_blank">01. Order Code Book</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccResourceCode?Type=Matcode")%> " target="_blank">02. Material Opening Code</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/SalesCodeBook?Type=All")%> " target="_blank">03. General Code Book</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_01_Mer/ConsMatStoring?Type=All")%> " target="_blank">04. Department Wise Material Analysis</a>
                            </li>                          
                          
                             <li>
                                <a href="<%=this.ResolveUrl("~/F_03_CostABgd/SalesContact?Type=Entry&genno=&actcode=&dayid=&sircode=")%> " target="_blank">06. Create Proforma Invoice</a>
                            </li>
                        </ul>

                    </asp:Panel>
                </asp:Panel>


                <div id="myModal" class="modal animated slideInLeft" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content  ">
                            <div class="modal-header">

                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>
                                    <asp:Label ID="lblmodalhead" runat="server"></asp:Label></h4>
                                <button type="button" class="close btn btn-xs" data-dismiss="modal"><i class="fa fa-times" aria-hidden="true"></i></button>
                            </div>
                            <asp:MultiView ID="ModalMultiView" runat="server">
                                <asp:View ID="OrdrVIew" runat="server">
                                    <div class="modal-body">
                                        <div class="row-fluid">
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" CssClass="col-md-2">Buyer Name: </asp:Label>
                                                <asp:Label ID="buyername" runat="server" CssClass="col-md-10"></asp:Label>

                                            </div>
                                            <br />
                                            <asp:GridView ID="gvstylemodal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvstylemodal_RowDataBound">
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

                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                                Width="80px"></asp:Label>
                                                            <asp:Label ID="lblinqno" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'></asp:Label>
                                                            <asp:Label ID="lblstyleid" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Article Num.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Image">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%#(Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                            </asp:HyperLink>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Master LC" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlmlccod" CssClass="form-control inputTxt chzn-select" runat="server"></asp:DropDownList>
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
                                    <div class="modal-footer ">

                                        <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="glyphicon glyphicon-save-file"></span> Update </asp:LinkButton>

                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                                    </div>
                                </asp:View>
                                <asp:View ID="ThumbChnge" runat="server">
                                    <asp:Label ID="mlccod" runat="server" Visible="false"></asp:Label>
                                    <div class="modal-body">
                                        <div class="card-body">
                                            <div id="dropzone" class="fileinput-dropzone">
                                                <span>Drop files or click to upload.</span>
                                                <!-- The file input field used as target for the file upload widget -->
                                                <asp:FileUpload ID="FileUpLoad1" onchange="submitform();" runat="server" />

                                            </div>
                                            <div id="progress" class="progress progress-xs rounded-0 fade">

                                                <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>


                                        </div>

                                    </div>
                                    <div class="modal-footer ">

                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>

                </div>

            </div>






            <%--    <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>
            <script src="../Scripts/jquery.counterup.min.js"></script>
            <script>
                jQuery(document).ready(function ($) {
                    $('.counter').counterUp({
                        delay: 10,
                        time: 1000
                    });
                });
            </script>--%>

            <%--                     </ContentTemplate>
                    </asp:UpdatePanel>--%>
        </div>
    </div>



    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>



    <script>
        function uploadComplete(sender) {
            $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "green";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "red";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File upload failed.";
        }

        function openModal() {

            $('#myModal').modal('toggle');

        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }
    </script>

    <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>

    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

