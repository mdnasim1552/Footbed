<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RawMattInterface.aspx.cs" Inherits="SPEWEB.F_11_RawInv.RawMattInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="Progressbar">
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
                <div class="card-body pb-2">
                    <div class="row">

                        <asp:Panel ID="plncop" runat="server" Visible="false">
                            <div class="pading5px" style="width: 200px">
                                <asp:Label ID="lblCompany" runat="server" CssClass="smLbl_to" Text="Unit:"></asp:Label>

                                <asp:DropDownList ID="ddlCompany" CssClass="chzn-select fromddl" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </asp:Panel>

                        <div class="col-md-2">
                            <div class="form-group mb-0">
                                <label class="control-label" for="FromDate">From</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm flatpickr-input pr-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFDate_CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group mb-0">
                                <label class="control-label" for="FromDate">To</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm flatpickr-input pr-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="btnSetup" runat="server" CssClass="margin-top30px btn-sm btn btn-success" OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                                 <asp:LinkButton ID="lnkInteface" runat="server" CssClass="margin-top30px btn-sm btn btn-secondary " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="lnkReports" runat="server" CssClass="margin-top30px btn-sm btn btn-warning" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                             
                        </div>
                        <div class="col-md-1">
                            <div class="margin-top30px btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-sm btn-danger">Operations</button>
                                <div class="btn-group btn-group-sm" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_11_RawInv/StoreMtTrnsReqEntry?Type=Entry&genno=">Create Requsition</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_11_RawInv/StoreMtTrnsReqEntry?Type=LoanEntry&genno=">Create Loan Requsition</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_11_RawInv/StoreMtTrnsReqEntry?Type=RetEntry&genno=">Create Return Requsition</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" runat="server" CssClass="dropdown-item" Target="_blank" NavigateUrl="~/F_11_RawInv/StoreMtTrnsReqEntry?Type=JobTrans&genno=">Job Work Materials Transfer</asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>





                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px">
                    <div class="row">
                        <asp:Panel ID="pnlInterf" runat="server">
                            <div id="slSt" class=" col-md-12 ServProdInfo">
                                <div class="panel with-nav-tabs panel-primary">
                                    <fieldset class="tabMenu">
                                        <div class="form-horizontal">
                                            <div class="form-group">

                                                <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0"></asp:ListItem>
                                                        <asp:ListItem Value="1"></asp:ListItem>
                                                        <asp:ListItem Value="2"></asp:ListItem>
                                                        <asp:ListItem Value="3"></asp:ListItem>
                                                        <asp:ListItem Value="4"></asp:ListItem>

                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div>
                                        <asp:Panel ID="pnlReqInfo" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvReqInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqInfo_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Width="80px" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Matt.  Ref">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Trans. From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Trans. To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc"))%>'></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Note">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvMRRFno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrnar"))%>'>
                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Req. Qty" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmocunt")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Matt. Qty" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode2" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Posted By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedusr")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="center" />
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

                                        <asp:Panel ID="pnlreqreadyfap" runat="server" Visible="false">
                                            <div class="row">
                                                <div class=" table-responsive col-lg-12">
                                                    <asp:GridView ID="gvReqrfap" runat="server" AutoGenerateColumns="False" CssClass="table-bordered table-striped grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqrfap_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Width="80px" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Matt. Ref">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Trans. From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Trans. To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc"))%>'></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Note">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvMRRFno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrnar"))%>'>
                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Items" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmocunt")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Qty" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode2" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrsirdesc" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Posted By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedusr")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkbtnEdit" CssClass="mr-2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnEntry" CssClass="mr-2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnPrint" CssClass="mr-2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="lnkbtndelete" CssClass="mr-2" OnClientClick="return confirm('Do You want Delete This Item?');" OnClick="lnkbtndelete_Click" runat="server"><span style="color:red" class="fa fa-trash"></span> </asp:LinkButton>

                                                                </ItemTemplate>
                                                                <ItemStyle Width="130px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="130px" VerticalAlign="Top" />
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


                                        <asp:Panel ID="PnlReqGPass" Visible="false" runat="server">

                                            <div class="row">
                                                <div class=" table-responsive col-lg-12">
                                                    <asp:GridView ID="gvReqGatePass" runat="server" AutoGenerateColumns="False" CssClass="table-bordered table-striped grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqGatePass_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" Width="80px" runat="server" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Matt. Ref">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Trans. From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Trans. To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc"))%>'></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Note">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvMRRFno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrnar"))%>'>
                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Item Count" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmocunt")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Qty" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvReqQty" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrReqAmt" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gate Pass<br> Bal">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGatepbal" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gatpbal")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Posted By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedusr")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>


                                                                    <asp:HyperLink ID="lnkbtnEntry" CssClass="mr-2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check" ></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnPrint" CssClass="mr-2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="lnkbtnstrdelete" CssClass="mr-2" OnClientClick="return confirm('Do You want Delete This Item?');" OnClick="lnkbtnstrdelete_Click" runat="server"><span style="color:red" class="fa fa-trash"></span> </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="130px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="130px" VerticalAlign="Top" />
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


                                        <asp:Panel ID="PnlReqGPassrfap" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12">
                                                    <asp:GridView ID="gvReqGatePassrfap" runat="server" AutoGenerateColumns="False" CssClass="table-bordered table-striped grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvReqGatePassrfap_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Get Pass. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno12" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" Width="80px" runat="server" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Matt. Ref">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Trans. From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Trans. To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc"))%>'></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Note">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvMRRFno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrnar"))%>'>
                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Item Count" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmocunt")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Qty" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvReqQty" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Amount">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrReqAmt" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gate Pass <br> Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrGatePqty" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gatpqty")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Transfer Bal">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrTransferBal" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnbal")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Posted By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedusr")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="lnkbtnEntry" CssClass="mr-2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check" ></span>
                                                                    </asp:HyperLink>
                                                                    
                                                                    <asp:HyperLink ID="hlnkGatPsPrnt" CssClass="mr-2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-print"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="lnkbtngpdelete" CssClass="mr-2" OnClientClick="return confirm('Do You want Delete This Item?');" OnClick="lnkbtngpdelete_Click" runat="server"><span style="color:red" class="fa fa-trash"></span> </asp:LinkButton>


                                                                </ItemTemplate>
                                                                <ItemStyle />
                                                                <HeaderStyle HorizontalAlign="Center" Width="130px" VerticalAlign="Top" />
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

                                        <asp:Panel ID="pnlProTrnas" Visible="false" runat="server">

                                            <div class="row">
                                                <div class=" table-responsive col-lg-12">
                                                    <asp:GridView ID="gvProTransfer" runat="server" AutoGenerateColumns="False" CssClass="table-bordered table-striped grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvProTransfer_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>


                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDSlNo" runat="server" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Req. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtreqno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GPN. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvGatepNo" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "getpno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TRN. No#" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno13" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc" runat="server" Width="80px" Text='<%#Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mtrdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Matt. Ref">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpatcdesc2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Trans. From">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfpactdesc")) %>'></asp:Label>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Trans. To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttpactdesc"))%>'></asp:Label>

                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Note">
                                                                <ItemTemplate>

                                                                    <asp:HyperLink ID="hlnkgvgvMRRFno" runat="server" BorderStyle="none"
                                                                        Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                        Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrnar"))%>'>
                                      
                                                                    </asp:HyperLink>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Items" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvpactcode" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmocunt")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Qty" Visible="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvReqQty" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req. Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReqAmt" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Transfer qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblrTransferQty" runat="server"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00);") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Posted By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedusr")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink ID="lnkbtnPrintIN" Visible="false" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="btnDelOrder" Visible="false" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="130px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="130px" VerticalAlign="Top" />
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

                        <asp:Panel ID="PnlWarehouseSetting" runat="server" CssClass="row" Visible="false">
                            <div class="col-md-12">
                                <ul class="list-unstyled">
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_11_RawInv/PurInterComMatTransfer")%> " target="_blank">1. Inter Company Material Transfer</a>
                                    </li>
                                </ul>
                            </div>
                        </asp:Panel>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
