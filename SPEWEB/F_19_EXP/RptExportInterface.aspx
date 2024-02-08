<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptExportInterface.aspx.cs" Inherits="SPEWEB.F_19_EXP.RptExportInterface" %>

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

        .wrntLbl {
            float: right;
            width: 60%;
            background: #DFF0D8;
            border: 1px solid #DFF0D8;
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

        function OpenArvmodal() {

            $('#Arvmodal').modal('show');
        }
        function CLoseMOdal() {

            $('#Arvmodal').modal('hide');
        }


        function pageLoaded() {
           

         
        };

        $(document).ready(function () {

            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });



        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvFGDeliv":
                    tblData = document.getElementById("<%=gvFGDeliv.ClientID %>");
                    break;
                case "gvFGChln":
                    tblData = document.getElementById("<%=gvFGChln.ClientID %>");
                    break;
                case "gvCollectionAll":
                    tblData = document.getElementById("<%=gvCollectionAll.ClientID %>");
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
            var comcod = <%=this.GetCompCode()%>;
            //console.log(comcod);
            switch (comcod) {
                case 5305:   // FB  
                case 5306:   // Footbed 

                    break;
                default:

                    $(".tbMenuWrp table tr td:nth-child(1)").hide();//sample entry
                 
                    break;
            }
            //$('.chzn-select').chosen({ search_contains: true });
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
                                <label class="control-label" for="FromDate">From Date</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm px-0" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">To Date</label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm px-0" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txttodate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="btnSetup" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-success" OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                            <asp:LinkButton ID="lnkInteface" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-secondary " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="lnkReports" runat="server" Style="margin-top: 28px;" CssClass="btn btn-sm btn-warning" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>                             
                        </div>

                        <div class="col-md-2">
                            <div class="btn-group" style="margin-top: 28px;" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-sm btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Create Invoice</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" CssClass="dropdown-item">Shipment Plan</asp:HyperLink>
                                        <asp:HyperLink ID="HypSampleExport" NavigateUrl="~/F_19_EXP/ExportSample?Type=Entry&actcode=&genno=" runat="server" Target="_blank" CssClass="dropdown-item">Sample Export</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" CssClass="dropdown-item">Collection Entry</asp:HyperLink>

                                        <%--<asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Pro/PurInformation.aspx" CssClass="dropdown-item">Dashboard</asp:HyperLink>--%>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">Recn. Date</label>
                                <asp:TextBox ID="txtRecDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtRecDateCalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtRecDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 150px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000"></asp:Timer>
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            </triggers>

                            <div class="row">
                                <asp:Panel ID="PnlInt" runat="server" Visible="false">
                                    <div id="slSt" class=" col-md-12">
                                        <div class="panel with-nav-tabs panel-primary">
                                            <fieldset class="tabMenu">
                                                <div class="form-horizontal">
                                                    <div class="col-md-12">
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
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                            </fieldset>

                                            <div>

                                                <asp:Panel ID="pnlMpaking" runat="server" Visible="false">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 450px;">

                                                            <asp:GridView ID="gvShipPlanDetails" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea fa-pull-right"
                                                                ShowFooter="True"><%-- OnPageIndexChanging="gvShipPlanDetails_PageIndexChanging"--%>
                                                                <PagerSettings Position="Top" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                                                Width="20px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Pack Plan">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvPackPlan" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "packpln1").ToString() %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Buyer">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvBuyer" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "buyerdesc").ToString() %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Order No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvOrderNo3" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "orderno").ToString() %>' Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Cust Order No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvOrderNo4" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "custordno").ToString() %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Pack Plan Ref">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvPackPlanRef1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "packplanref").ToString() %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Pack Plan Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvPackPlanDate1" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "packplandate")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Article Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvmlcdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mlcdesc").ToString() %>'
                                                                                Width="200px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="Total" runat="server" Text="Total"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <FooterStyle Font-Bold="true" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Quantity">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvQty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgvTotalQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000" Width="75px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Book/Invoce Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvQty1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>

                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bal. Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvQty2" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>

                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Right" />

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cartoon No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvCartNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cartno").ToString() %>'
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText=" ">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="hlkbtnEdit" runat="server" Target="_blank"
                                                                                NavigateUrl='<%# "~/F_19_EXP/CreatePackList?Type=Entry&actcode=" + DataBinder.Eval(Container.DataItem, "mlccod").ToString()+DataBinder.Eval(Container.DataItem, "styleid").ToString() +DataBinder.Eval(Container.DataItem, "colorid").ToString() + DataBinder.Eval(Container.DataItem, "dayid").ToString() + "&genno=" + DataBinder.Eval(Container.DataItem, "packpln").ToString() + "&sircod=" + DataBinder.Eval(Container.DataItem, "buyerid")+ "&date=" + Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "packplandate")).ToString("dd-MMM-yyyy") %>'>
                                                            <i class='fas fa-edit'></i></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>

                                                                 <%--   <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="lbtnDelete" OnClick="lbtnDelete_Click" OnClientClick="return confirm('Are you sure you want delete');" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "packpln").ToString() %>'><i class="fa fa-trash" style="color:red;"></i></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>

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
                                                
                                                <asp:Panel ID="pnlFGDeliv" runat="server" Visible="false">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 450px;">
                                                            <asp:GridView ID="gvFGDeliv" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True" OnRowDataBound="gvFGDeliv_RowDataBound">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                        </ItemTemplate>
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
                                                                            <asp:TextBox ID="txtSearchInvRefno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Ref No" onkeyup="Search_Gridview(this,1, 'gvFGDeliv')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgcInvRefno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invrefno")) %>'
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>

                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        <FooterStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Invoice No">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchreqno" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Invoice No" onkeyup="Search_Gridview(this,2, 'gvFGDeliv')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvreqno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno1")) %>'
                                                                                Width="90px"></asp:Label>
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
                                                                            <asp:TextBox ID="txtSearchorproid" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Order  Name" onkeyup="Search_Gridview(this,4, 'gvFGDeliv')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvstorproid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                                Width="220px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Customer Name">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchcustdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Customer Name" onkeyup="Search_Gridview(this,5, 'gvFGDeliv')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcustdesc" runat="server" BackColor="Transparent"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custdesc")) %>'
                                                                                Width="130px"></asp:Label>
                                                                        </ItemTemplate>
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



                                                                    <asp:TemplateField HeaderText="Invoice</br> Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvordrqty" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0);") %>'
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblstrTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Invoice </Br> Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitmamt" runat="server" Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblINAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Print">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblgvFgDelvPrntType" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printformat")) %>'></asp:Label>
                                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Invoice">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Invoice App.">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Challan">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HypDelivery" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-check"></span>
                                                                            </asp:HyperLink>


                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Export">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HypExprotUp" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-check"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Check">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HypStockCheck" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-search-location"></span>
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Top" />

                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Mode">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvdeltrmdesc" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deltrmdesc")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Ex. Factory </br>Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvexfacdt1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "exfacdt1")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvdelvdate1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "delvdate1")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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

                                                <asp:Panel ID="PnlFGChln" runat="server" Visible="false">

                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 450px;">
                                                            <asp:GridView ID="gvFGChln" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                ShowFooter="True" OnRowDataBound="gvFGChln_RowDataBound">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                        </ItemTemplate>
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
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvdchno1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dchno1")) %>'
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Challan Ref">
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
                                                                    <asp:TemplateField HeaderText="Order Name">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchorproid" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Order Name" onkeyup="Search_Gridview(this,3, 'gvFGChln')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvstorproid" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                                Width="220px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Buyer">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchcustdesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Buyer Name" onkeyup="Search_Gridview(this,4, 'gvFGChln')"></asp:TextBox><br />
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
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
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
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Despatch">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvdespatchtype" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "despatchtype")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Vehicle No">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvvehclno" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vehclno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Transit <br>Voucher">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink Target="_blank" NavigateUrl='<%# "~/F_21_GAcc/Print?Type=accVou&vounum="+Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>' ID="HypLInkVoucher" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                                                Width="80px"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                            </asp:HyperLink>

                                                                            <asp:HyperLink ID="lnkbtnEdit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                                            </asp:HyperLink>

                                                                            <asp:HyperLink ID="lnkbtnEntry" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
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


                                                <asp:Panel ID="PnlColl" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="table-responsive col-lg-12" style="min-height: 450px;">

                                                            <asp:GridView ID="gvCollectionAll" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvCollectionAll_RowDataBound"
                                                                ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                                <RowStyle />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sl">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Voucher#" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvvoucher" runat="server" Font-Bold="True" Style="text-align: right"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>' Width="30px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="A/C No" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcentrid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrid")) %>'
                                                                                Width="90px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cust Id" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcustid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custid")) %>'
                                                                                Width="90px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ref #" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvMemono" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkgvdate" runat="server"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Order Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcactdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "centrdesc") %>'
                                                                                Width="280px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Ref #">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchRef" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Ref #" onkeyup="Search_Gridview(this,7, 'gvCollectionAll')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono1")) %>'
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name">
                                                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchName" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Cust Name" onkeyup="Search_Gridview(this,8, 'gvCollectionAll')"></asp:TextBox><br />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvresdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                                                Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="FC </br>Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvfcamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFfcamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                Style="text-align: right" Width="75px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="BDT </br>Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvtrnamount" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                Width="75px" Style="text-align: right"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFtrnamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                Style="text-align: right" Width="75px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="FC </br>Bank </br>Charge">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvfcbnkcharge" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcbnkcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                Width="55px" Style="text-align: right"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFfcbnkcharge" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                Style="text-align: right" Width="55px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="BDT </br>Bank </br>Charge">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvvatamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                                Width="60px" Style="text-align: right"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFvatamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="BDT </br>Gain/Loss">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvcglamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cglamt")).ToString("#,##0;(#,##0); ") %>'
                                                                                Width="60px" Style="text-align: right"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFcglamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Advance </br>Adjustment">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lgvadjamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjamt")).ToString("#,##0;(#,##0); ") %>'
                                                                                Width="60px" Style="text-align: right"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvFadjamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Pay Type" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvpaytype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                                                                Width="80px" Font-Bold="true"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Cheque No" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvchequeno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                                                Width="80px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Bank Name" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbnknam" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bnknam")) %>'
                                                                                Width="150px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Branch Name" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvbbranch" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Pay Date" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lnkgvpaydat" runat="server"
                                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydat")).ToString("dd-MMM-yyyy") %>'
                                                                                Width="70px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="User" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvuser" Font-Size="8px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                                                Width="50px"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="lbtnEdit" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink ID="LbtnPrint" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>

                                                                            <asp:LinkButton OnClick="lnkCheck_Click" OnClientClick="return confirm('Do You Agree to Approve?')" ID="lnkCheck" runat="server"><span class="fa fa-check"></span></asp:LinkButton>

                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Remarks">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvremarks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                                Width="120px"></asp:Label>
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
                                            </div>
                                        </div>

                                    </div>
                                </asp:Panel>


                                <asp:Panel ID="pnlReprots" runat="server">

                                    <asp:Panel ID="plnMgf" runat="server" Visible="false">




                                        <ul class="list-unstyled ">

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_19_EXP/DayWiseSalesEntry?Type=SalRep")%> " target="_blank">01. Day Wise Sales Report</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_19_EXP/AllCollection?Type=All")%> " target="_blank">02. Day Wise Collection Report</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_03_CostABgd/MLCInfoEntry?Type=Entry&actcode=&dayid=")%> " target="_blank">02. L/C General Information</a>
                                            </li>
                                             <li>
                                                <a href="<%=this.ResolveUrl("~/F_01_Mer/RptOrdAppSheet?Type=BomApp")%> " target="_blank">03. BOM Approved List</a>
                                            </li>


                                        </ul>


                                    </asp:Panel>

                                    <asp:Panel ID="PnlSalesSetup" runat="server" Visible="false">


                                        <ul class="list-unstyled">

                                            <li>


                                                <a href="<%=this.ResolveUrl("~/F_19_EXP/DayWiseSalesEntry?Type=SalEntry")%> " target="_blank">02. Day Wise Sales Entry</a>



                                            </li>


                                        </ul>

                                    </asp:Panel>
                                </asp:Panel>




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

    <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>

    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

