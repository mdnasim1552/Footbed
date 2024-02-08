<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="InterfaceLeavApp.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.InterfaceLeavApp" %>

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
        function SetTarget(type) {
            window.open('../../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
        }
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


                                <asp:Label ID="Label18" runat="server" CssClass="label">From Date</asp:Label>
                                <asp:TextBox ID="txFdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txFdate"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">To Date</asp:Label>

                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">

                            <asp:LinkButton ID="lnkInterface" runat="server" Style="margin-top: 20px;" CssClass=" btn btn-sm btn-primary" OnClick="lnkInterface_Click">Interface</asp:LinkButton></li>
                                     
                
                        </div>

                        <div class="col-md-1">
                            <div class=" btn-group" style="margin-top: 20px;" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-sm btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_84_Lea/HREmpLeave?Type=FLeaveApp" CssClass="dropdown-item">Leave Application (Manual)</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_84_Lea/HREmpLeave?Type=LeaveApp" CssClass="dropdown-item">Leave Application (Online)</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_81_Hrm/F_84_Lea/MyLeave?Type=User" CssClass="dropdown-item">Leave Application Online (Ind)</asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>






                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height: 30px;">
                        <asp:Panel ID="pnlInt" runat="server" Visible="false">
                            <div id="slSt" class=" col-md-12 col-sm-12 col-lg-12">
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
                                                    </asp:RadioButtonList>

                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div>

                                        <asp:Panel ID="pnlallReq" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvLvReq" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvLvReq_RowDataBound">
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
                                                            <asp:TemplateField HeaderText="Leave ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblltrnid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblgvempid" runat="server" Visible="False"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")).ToString() %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="130px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="130px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Current Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvCust" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvstatus")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>

                                                                    <asp:HyperLink ID="lnkbtnEdit" runat="server"><span class="fa fa-edit"></span> </asp:HyperLink>
                                                                    <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click" ToolTip="Current Leave Status"><span class="fa fa-print"></span></asp:LinkButton>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Print Leave Form"><span class="fa fa-print"></span></asp:LinkButton>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="gvHeader" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PnlProcess" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvInprocess" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvInprocess_RowDataBound">
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
                                                            <asp:TemplateField HeaderText="Leave ID">
                                                                <ItemTemplate>


                                                                    <asp:Label ID="lblltrnid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblgvempid" runat="server" Visible="False"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="116px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbtnPrint" runat="server" OnClick="lnkbtnPrint_Click" ForeColor="Black" Font-Underline="false" ToolTip="Print Leave Form"><span class="fa fa-print"></span></asp:LinkButton>
                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" ToolTip="Leave Edit"><span class="fa fa-pencil-alt"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" ToolTip="Leave Process"><span class="fa fa-thumbs-up"></span>
                                                                    </asp:HyperLink>

                                                                    <asp:LinkButton ID="lnkbtnDel" runat="server" OnClick="lnkbtnDel_Click" ForeColor="Red" ToolTip="Delete Leave"><span class="fa fa-trash"></span></asp:LinkButton>

                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="120px" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="gvHeader" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PnlApp" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvApproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvApproved_RowDataBound">
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
                                                            <asp:TemplateField HeaderText="Leave ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblltrnid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblgvempid" runat="server" Visible="False"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")).ToString() %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="HylvPrint" runat="server" OnClick="HylvPrint_Click" ForeColor="Black" Font-Underline="false" ToolTip="Print Leave Form"><span class="fa fa-print"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:HyperLink ID="lnkbtnEditIN" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" ToolTip="Leave Edit"><span class="fa fa-edit"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" ToolTip="Leave Approve"><span class="fa fa-thumbs-up"></span>
                                                                    </asp:HyperLink>
                                                                    <asp:LinkButton ID="lnkbtnfDel" runat="server" OnClick="lnkbtnfDel_Click" ToolTip="Delete Leave"><span class="fa fa-trash"></span></asp:LinkButton>


                                                                </ItemTemplate>
                                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="gvHeader" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="PnlConfrm" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                    <asp:GridView ID="gvConfirm" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
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
                                                            <asp:TemplateField HeaderText="Leave ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblltrnid" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblgvempid" runat="server" Visible="False"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")).ToString() %>'
                                                                        Width="49px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Emp Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvempname" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                        Width="150px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ID Card">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgidcard" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Department Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdeptanme" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptanme")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgdesig" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                                        Width="120px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Leave Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lglvtype" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lvtype")) %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Apply Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvaplydat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aplydat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Start Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvstrtdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strtdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvenddat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblduration" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    <%--  <asp:LinkButton ID="lnkbtnPrint" OnClick="lnkbtnPrintRD_Click" runat="server"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>--%>

                                                                    <%-- <asp:LinkButton ID="lnkbtnEdit" runat="server"><span class="glyphicon glyphicon-pencil"></span>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnView" runat="server"><span class="glyphicon glyphicon-eye-open"></span>
                                                        </asp:LinkButton>--%>

                                                                    <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-print"></span>
                                                                    </asp:HyperLink>

                                                                </ItemTemplate>
                                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="gvHeader" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>


                                    </div>
                                </div>

                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlReport" runat="server" Visible="False">
                            <asp:Panel ID="pnlTrade" runat="server" Visible="false">
                                <div class="form-group">

                                    <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                        <ul class="nav colMid " id="SERV">
                                            <li>

                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/SalesStatusGraph.aspx")%> " target="_blank">01. Sales (Graph)</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalesPer")%> " target="_blank">02. Sales Performance</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProIssue")%> " target="_blank">03. Daywise Issue</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalReg")%> " target="_blank">04. Region Wise Lifting</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProDelivery")%> " target="_blank">05. Order Status</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=Prolift")%> " target="_blank">06. Lifting Status</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=ProDel")%> " target="_blank">07. Delivery Status</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch")%> " target="_blank">08. Seg. Wise Tar. vs Achi.</a>

                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch2")%> " target="_blank">09. Team Wise Tar. vs Achi.</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalLedger")%> " target="_blank">10. Customer Ledger</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptDelvIMEIHistory.aspx")%> " target="_blank">11. Delivery IMEI Status</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=PayHis")%> " target="_blank">12. Payment History</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductModelWise.aspx")%> " target="_blank">13. Product Issuance</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport_02.aspx?Type=ProHis")%> " target="_blank">14. Product Wise History</a>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="plnMgf" runat="server" Visible="false">
                                <div class="form-group">

                                    <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                        <ul class="nav colMid " id="SERV">
                                            <li>

                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/SalesStatusGraph.aspx")%> " target="_blank">01. Sales (Graph)</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalesPer")%> " target="_blank">02. Sales Performance</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProIssue")%> " target="_blank">03. Daywise Issue</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalReg")%> " target="_blank">04. Region Wise Lifting</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=ProDelivery")%> " target="_blank">05. Order Status</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=Prolift")%> " target="_blank">06. Lifting Status</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=ProDel")%> " target="_blank">07. Delivery Status</a>
                                            </li>
                                            <%-- <li>
                                            <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch")%> " target="_blank">08. Seg. Wise Tar. vs Achi.</a>

                                        </li>--%>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductDelivery.aspx?Type=TarVSAch2")%> " target="_blank">09. Team Wise Tar. vs Achi.</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=SalLedger")%> " target="_blank">10. Customer Ledger</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport.aspx?Type=PayHis")%> " target="_blank">12. Payment History</a>
                                            </li>

                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptProductModelWise.aspx")%> " target="_blank">13. Product Issuance</a>
                                            </li>
                                            <li>
                                                <a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalesReport_02.aspx?Type=ProHis")%> " target="_blank">14. Product Wise History</a>
                                            </li>

                                        </ul>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>
    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

