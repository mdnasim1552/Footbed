<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SamplingInterface.aspx.cs" Inherits="SPEWEB.F_04_Sampling.SamplingInterface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
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
        function CLoseMOdal() {
            $('#exampleModalSm').modal('hide');

        }
        function Rerunmodal(inqno) {

            $("#TxtSdino").val(inqno);
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
                case "gvSmpleinqlist":
                    tblData = document.getElementById("<%=gvSmpleinqlist.ClientID %>");
                    break;

                case "gvConSheet":
                    tblData = document.getElementById("<%=gvConSheet.ClientID %>");
                    break;

                case "gvPreCost":
                    tblData = document.getElementById("<%=gvPreCost.ClientID %>");
                    break;

                case "gvProCom":
                    tblData = document.getElementById("<%=gvProCom.ClientID %>");
                    break;
                case "gvpdguide":
                    tblData = document.getElementById("<%=gvpdguide.ClientID %>");
                    break;
                case "gvissued":
                    tblData = document.getElementById("<%=gvissued.ClientID %>");
                    break;
                case "gvpdguidapp":
                    tblData = document.getElementById("<%=gvpdguidapp.ClientID %>");
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

        <%--var gv = $('#<%=this.gvSmpleinqlist.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvConSheet.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvpdguide.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvissued.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvpdguidapp.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvPreCost.ClientID %>');
            gv.Scrollable();
            var gv = $('#<%=this.gvProCom.ClientID %>');
            gv.Scrollable();--%>

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
                        <label for="FromDate">From Date</label>
                         <span class="text-youtube" style="font-size:small">(*Max 31 days)</span>
                        <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="control-label" for="ToDate">To Date</label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                    </div>

                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="label" for="ToDate">Season</label>
                        <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="label">Sample Type</label>
                        <asp:DropDownList ID="ddlsampletype" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <label class="label">Buyer</label>
                        <asp:DropDownList ID="ddlbuyer" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-md-1 ">
                    <div class="form-group">
                        <label class="label">Agent</label>
                        <div class="input-group input-group-sm input-group-alt">
                            <asp:DropDownList ID="DdlAgent" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>

                        </div>
                    </div>
                </div>
                <div class="col-md-1">
                    <div class="form-group">
                        <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                    </div>
                </div>

                <div class="col-md-3">
                    <asp:LinkButton ID="btnSetup" runat="server" CssClass="margin-top30px btn btn-success btn-sm" OnClick="btnSetup_Click">Setting</asp:LinkButton>
                    <asp:LinkButton ID="lnkInteface" runat="server" CssClass="margin-top30px btn btn-secondary btn-sm" OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                    <asp:LinkButton ID="lnkReports" runat="server" CssClass="margin-top30px btn btn-warning btn-sm" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton>

                </div>
                <div class="col-md-1">
                    <div class="margin-top30px btn-group btn-group-sm" role="group" aria-label="Button group with nested dropdown">
                        <button type="button" class="btn btn-danger btn-sm">Operations</button>
                        <div class="btn-group btn-group-sm" role="group">
                            <button id="btnGroupDrop4" type="button" class="btn btn-danger btn-sm dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                            <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                <div class="dropdown-arrow"></div>
                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Sample Development Inquiry</asp:HyperLink>

                                <%--<asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Pro/PurInformation.aspx" CssClass="dropdown-item">Dashboard</asp:HyperLink>--%>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </div>


    <div class="card card-fluid">
        <div class="card-body">

            <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>

            <%-- <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000"></asp:Timer>
                            <triggers> <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" /></triggers>--%>

            <div class="row">
                <asp:Panel ID="PnlInt" runat="server" Visible="false">
                    <div id="slSt" class=" col-md-12">
                        <div class="panel with-nav-tabs panel-primary">
                            <div class="tbMenuWrp nav nav-tabs">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0"></asp:ListItem>
                                    <asp:ListItem Value="1"></asp:ListItem>
                                    <asp:ListItem Value="2"></asp:ListItem>
                                    <asp:ListItem Value="3"></asp:ListItem>
                                    <asp:ListItem Value="4"></asp:ListItem>
                                    <asp:ListItem Value="5"></asp:ListItem>
                                    <asp:ListItem Value="6"></asp:ListItem>

                                    <%-- <asp:ListItem Value="5"></asp:ListItem>
                                                <asp:ListItem Value="6"></asp:ListItem>
                                                <asp:ListItem Value="7"></asp:ListItem>
                                                <asp:ListItem Value="8"></asp:ListItem>--%>
                                </asp:RadioButtonList>

                            </div>



                            <asp:Panel ID="pnlallInqList" runat="server" Visible="false">
                                <div class="row">
                                    <div class="table-responsive col-10">
                                        <div class="row" style="max-height: 360px"">
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
                                                    
                                                    <asp:TemplateField HeaderText="Inquery No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqno1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                Width="60px"></asp:Label>
                                                            <asp:Label ID="LblSdino" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                Width="60px"></asp:Label>
                                                            <asp:Label ID="LblbUyer" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyer")) %>'
                                                                Width="60px"></asp:Label>
                                                            <asp:Label ID="LblSampType" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptype")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqdate" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqdat")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Sample Type">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvlSampTypeName" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Sample Type  " onkeyup="Search_Gridview(this,3, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvlSampType" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptypdesc")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Last(Forma) Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvlformaname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder=" Last(Forma)" onkeyup="Search_Gridview(this,4, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvlformaname" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Article No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvarticleno" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Article No" onkeyup="Search_Gridview(this,5, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvarticleno" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Color">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvColor" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Version">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvVersion" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sversion")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Catagory">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvcatagorydesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Catagory" onkeyup="Search_Gridview(this,8, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcatagorydesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Buyer Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvBuyerDesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Buyer Name" onkeyup="Search_Gridview(this, 9, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBuyerDesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Brand Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvBrandDesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Brand Name" onkeyup="Search_Gridview(this, 10, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBrandDesc" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brandesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Shoe Type">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvshoetype" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetdesc")) %>'
                                                                Width="70px"></asp:Label>
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
                                                    

                                                    <%--12--%>
                                                    <asp:TemplateField HeaderText="Agent">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvagent" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Agent" onkeyup="Search_Gridview(this, 13, 'gvSmpleinqlist')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvagent" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Sample Size">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsamsize" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Com. Size">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcomsize" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Size Range">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsizerange" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsamqty" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="10px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvremarks" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   


                                                    <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqno" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Current Stage">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCurrStage" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodstage")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkEdit" Target="_blank" runat="server" ToolTip="Edit"><i class="fa fa-edit"  aria-hidden="true"></i></asp:HyperLink>
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
                                                                        <asp:HyperLink ID="HyInprPrint" runat="server" Target="_blank" Font-Underline="false"> <span class="fa fa-print"> Inquiry</span> </asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton OnClick="LbtnDuplication_Click" OnClientClick="return confirm('Do you agree to duplicate this Inquery?')" ID="LbtnDuplication" runat="server"> <span class="fa fa-copy"></span> Duplicate</asp:LinkButton>
                                                                    </li>
                                                                    <li>
                                                                        <asp:LinkButton OnClick="LbtnRetrace_Click" OnClientClick="return confirm('Do you agree to Retrace this Inquery?')" ID="LbtnRetrace" runat="server"> <span class="fa fa-copy"></span> Re-Trace</asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkCheck" Target="_blank" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblStatus" Target="_blank" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgv1UsrName" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="User Name" onkeyup="Search_Gridview(this, 24, 'gvSmpleinqlist')"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblUserName" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "posteduser")) %>'
                                                                Width="50px"></asp:Label>
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
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlConShet" Visible="false" runat="server">
                                <div class="row">
                                    <div class="table-responsive col-10">
                                        <div class="row" style="max-height: 360px">
                                            <asp:GridView ID="gvConSheet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvConSheet_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNocst" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Inquery No">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqno1cst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqdatecst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqdat")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sample Type">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvConSheetTypeName" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Sample Type  " onkeyup="Search_Gridview(this,3, 'gvConSheet')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvlSampType" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptypdesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Last(Forma)">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvConSheetlformaname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Last(Forma)" onkeyup="Search_Gridview(this,4, 'gvConSheet')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvlformanamecst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Article No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvConSheetvarticleno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Article No" onkeyup="Search_Gridview(this,5, 'gvConSheet')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvarticlenocst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Color">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvColor" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Version">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvVersion1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sversion")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Buyer Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvBuyerDesc1" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Buyer Name" onkeyup="Search_Gridview(this,8, 'gvConSheet')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBuyerDesc1" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Catagory">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvcatagorydesccst" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Catagory" onkeyup="Search_Gridview(this,9, 'gvConSheet')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcatagorydesccst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Shoe Type">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvshoetypecst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetdesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IMAGE">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hyprrrcst" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                <asp:Image ID="lblImageUrlcst" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Agent">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvagentcst" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Agent" onkeyup="Search_Gridview(this,12, 'gvConSheet')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvagentcst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Sample Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsamsizecst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize"))%>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Com. Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcomsizecst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Size Range">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsizerangecst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsamqtycst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Remarks">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvremarkscst" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqnocst" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
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
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkCheck" Target="_blank" ToolTip="Consumption Approval" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelInq" runat="server" OnClick="btnDelInq_Click" ToolTip="Reverse" OnClientClick="return confirm('Do You want Reverse This Item?');"><span style="color:red" class=" fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="User Name ">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgv2UsrName" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="User Name" onkeyup="Search_Gridview(this, 21, 'gvConSheet')"></asp:TextBox><br />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:Label ID="LblUserName" runat="server" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "posteduser")) %>'
                                                                Width="50px"></asp:Label>
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
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="Panelpdguide" Visible="false" runat="server">

                                <div class="row">
                                     <div class="table-responsive col-10">
                                        <div class="row" style="max-height: 360px"">
                                        <asp:GridView ID="gvpdguide" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDataBound="gvpdguide_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNopg" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inquery No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqno1pg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqdatepg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqdat")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sample Type">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvpdguideTypeName" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Sample Type " onkeyup="Search_Gridview(this,3, 'gvpdguide')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlSampType" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptypdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                


                                                <asp:TemplateField HeaderText="Last(Forma) Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvlformaname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Last(Forma)" onkeyup="Search_Gridview(this,4, 'gvpdguide')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlformanamepg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                             
                                                
                                                <asp:TemplateField HeaderText="Article No">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvarticleno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Article No" onkeyup="Search_Gridview(this,5, 'gvpdguide')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvarticlenopg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                              
                                                   <asp:TemplateField HeaderText="Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvColor" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Version">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvVersion2" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sversion")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Buyer Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvBuyerDesc2" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Buyer Name" onkeyup="Search_Gridview(this,8, 'gvpdguide')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBuyerDesc2" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                
                                                

                                                <asp:TemplateField HeaderText="Catagory">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvcatagorydescpg" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Catagory" onkeyup="Search_Gridview(this,9, 'gvpdguide')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcatagorydescpg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shoe Type">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvshoetypepg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IMAGE">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyprrrpg" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                            <asp:Image ID="lblImageUrlpg" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvagentpg" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Agent" onkeyup="Search_Gridview(this,12, 'gvpdguide')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvagentpg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Sample Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamsizepg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Com. Size">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcomsizepg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Size Range">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsizerangepg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Quantity">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamqtypg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvremarkspg" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqnopg" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                            Width="80px"></asp:Label>
                                                        <asp:Label ID="LblSampleId" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sampleid")) %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lbtnconpg" Target="_blank" ToolTip="PD Guide(Entry)" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HypgPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkCheckpg" Target="_blank" ToolTip="PD Guide(Approval)" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                
                                                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelConspg" runat="server" OnClick="btnDelConspg_Click" ToolTip="Reverse" OnClientClick="return confirm('Do You want Return This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="PD Book">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbtnPDBook" ToolTip="PD Book" OnClick="LbtnPDBook_Click" runat="server"><span class="fa fa-info-circle"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="User Name ">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgv3UsrName" BackColor="Transparent" BorderStyle="None" runat="server" Width="50px" placeholder="User Name" onkeyup="Search_Gridview(this, 24, 'gvpdguide')"></asp:TextBox><br />
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="LblPdUserName" runat="server" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conusrname")) %>'
                                                            Width="50px"></asp:Label>
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
                                </div>

                            </asp:Panel>


                            <asp:Panel ID="PnlIssued" Visible="false" runat="server">

                                <div class="row">
                                     <div class="table-responsive col-10">
                                        <div class="row" style="max-height: 360px"">
                                            <asp:GridView ID="gvissued" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvissued_RowDataBound">
                                                <RowStyle />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNoiss" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Inquery No">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqno1iss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqdateiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqdat")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sample Type">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvissuedTypeName" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Sample Type  " onkeyup="Search_Gridview(this,3, 'gvissued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvlSampType" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptypdesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Last(Forma) Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvissuedlformaname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Last(Forma)" onkeyup="Search_Gridview(this,4, 'gvissued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvlformanameiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                
                                                   
                                                
                                                    <asp:TemplateField HeaderText="Article No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvissuedarticleno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Article No" onkeyup="Search_Gridview(this,5, 'gvissued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvarticlenoiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Color">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvColor" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Version">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvVersion3" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sversion")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Buyer Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvBuyerDesc3" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Buyer Name" onkeyup="Search_Gridview(this,8, 'gvissued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBuyerDesc3" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Catagory">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvcatagorydesciss" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Catagory" onkeyup="Search_Gridview(this,9, 'gvissued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcatagorydesciss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Shoe Type">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvshoetypeiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetdesc")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="IMAGE">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hyprrrpg" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                <asp:Image ID="lblImageUrliss" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                            </asp:HyperLink>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Agent">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtgvagentiss" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Agent" onkeyup="Search_Gridview(this,12, 'gvissued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvagentiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sample Size">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsamsizeiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Com. Size">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcomsizeiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Size Range">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsizerangeiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Qty">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvsamqtyiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="10px" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>







                                                    <asp:TemplateField HeaderText="Remarks">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvremarksiss" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvinqnoiss" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                                Width="80px"></asp:Label>



                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Current Stage">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvProdstep" runat="server" Font-Size="10px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodstage")) %>'
                                                                Width="80px"></asp:Label>



                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbtnIssued" Target="_blank" ToolTip="Issued" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Print">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyissPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                                <asp:HyperLink ID="HyCommissPrint" runat="server" ToolTip="Common Materials" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelConsiss" runat="server" OnClick="btnDelConsiss_Click" Visible="false" ToolTip="Reverse" OnClientClick="return confirm('Do You want Return This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LbtnStaus" runat="server" OnClick="LbtnStaus_Click" ToolTip="Status"><span style="color:purple" class="fa fa-th"></span> </asp:LinkButton>
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


                            <asp:Panel ID="PnlFinpdguideapp" Visible="false" runat="server">

                                <div class="row">
                                     <div class="table-responsive col-10">
                                        <div class="row" style="max-height: 360px"">
                                            <%--Work--%>
                                        <asp:GridView ID="gvpdguidapp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDataBound="gvpdguidapp_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNopdapp" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Inquery No">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqno1pdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqdatepdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqdat")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sample Type">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvpdguidappTypeName" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Sample Type  " onkeyup="Search_Gridview(this,3, 'gvpdguidapp')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlSampType" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptypdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last(Forma) Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvpdguidapplformaname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Last(Forma)" onkeyup="Search_Gridview(this,4, 'gvpdguidapp')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlformanamepdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                             
                                                <asp:TemplateField HeaderText="Article No">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvpdguidapparticleno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Article No" onkeyup="Search_Gridview(this,5, 'gvpdguidapp')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvarticlenopdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvColor" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Version">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvVersion4" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sversion")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Buyer Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvBuyerDesc5" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Buyer Name" onkeyup="Search_Gridview(this,8, 'gvpdguidapp')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBuyerDesc5" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Catagory">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvcatagorydescpdapp" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Catagory" onkeyup="Search_Gridview(this,9, 'gvpdguidapp')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcatagorydescpdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shoe Type">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvshoetypepdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="IMAGE">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyprrrpg" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                            <asp:Image ID="lblImageUrlpdapp" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Agent">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvagentpdapp" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Agent" onkeyup="Search_Gridview(this,12, 'gvpdguidapp')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvagentpdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Sample Size">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamsizepdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize"))%>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Com. Size">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcomsizepdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize"))%>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Size Range">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsizerangepdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Quantity">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamqtypdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>







                                                <asp:TemplateField HeaderText="Remarks">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvremarkspdapp" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqnopdapp" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                            Width="80px"></asp:Label>



                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HypdappPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PD Book">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="modPdGuideBtnPDBook" ToolTip="Modified PD Book" OnClick="modPdGuideBtnPDBook_Click" runat="server"><span class="fa fa-info-circle"></span></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkCheckpdapp" Target="_blank" ToolTip="Modification/Approve" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelConspdapp" runat="server" OnClick="btnDelConsiss_Click" Visible="false" ToolTip="Reverse" OnClientClick="return confirm('Do You want Return This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                            <asp:Panel ID="PnlConCost" Visible="false" runat="server">

                                <div class="row">
                                    <div class="table-responsive col-10">
                                        <div class="row" style="max-height: 360px"">
                                        <asp:GridView ID="gvPreCost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True" OnRowDataBound="gvPreCost_RowDataBound">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNocst" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Inquery No">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqno1cst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqdatecst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqdat")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Last(Forma) Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvPreCostlformaname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Last(Forma)" onkeyup="Search_Gridview(this,3, 'gvPreCost')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlformanamecst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Article No">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvPreCostarticleno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Article No" onkeyup="Search_Gridview(this,4, 'gvPreCost')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvarticlenocst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvColor" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Version">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvVersion5" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sversion")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Buyer Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvBuyerDesc5" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Buyer Name" onkeyup="Search_Gridview(this,7, 'gvPreCost')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBuyerDesc5" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Catagory">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvcatagorydesccst" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Catagory" onkeyup="Search_Gridview(this,8, 'gvPreCost')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcatagorydesccst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shoe Type">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvshoetypecst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="IMAGE">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyprrrcst" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                            <asp:Image ID="lblImageUrlcst" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Agent">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvagentcst" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Agent" onkeyup="Search_Gridview(this,11, 'gvPreCost')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvagentcst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Sample Size">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamsizecst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Com. Size">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcomsizecst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Size Range">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsizerangecst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Quantity">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamqtycst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Amount">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconamtcst" runat="server" AutoCompleteType="Disabled" Font-Bold="true"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remarks">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvremarkscst" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Created by">
                                                      <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvcbdusr" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="User Nane" onkeyup="Search_Gridview(this,18, 'gvPreCost')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcbdusr" runat="server" AutoCompleteType="Disabled" 
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pcbdusrdesc")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>

                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqnocst" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                            Width="80px"></asp:Label>
                                                        <asp:Label ID="LblSampleId" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sampleid")) %>'
                                                            Width="80px"></asp:Label>


                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lbtnCost" Target="_blank" ToolTip="Consumption" runat="server"><span class="fa fa-edit"></span></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PD Print">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HycbdpdappPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="PD Book">
                                                     <ItemTemplate>
                                                         <asp:LinkButton ID="gvPreCostPDBook" ToolTip="Modified PD Book" OnClick="gvPreCostPDBook_Click" runat="server" 
                                                             CommandArgument='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>' ><span class="fa fa-info-circle"></span></asp:LinkButton>
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Center" />
                                                 </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkCheck" Target="_blank" ToolTip="Pre-Costing Approval" runat="server"><span class="fa fa-check"></span></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton Visible="false" ID="btnDelCons" runat="server" OnClick="btnDelCons_Click" ToolTip="Reverse" OnClientClick="return confirm('Do You want Return This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelPreCost" runat="server" OnClick="btnDelPreCost_Click" ToolTip="Reverse" OnClientClick="return confirm('Do You want Return This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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


                            <asp:Panel ID="PnlProComp" Visible="false" runat="server">
                                <div class="row">
                                    <div class="table-responsive col-10">
                                        <div class="row" style="max-height: 360px"">
                                        <asp:GridView ID="gvProCom" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvProCom_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                            ShowFooter="True">
                                            <RowStyle />
                                            <Columns>


                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNocom" runat="server" Font-Bold="True"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Inquery No">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqno1com" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno1")) %>'
                                                            Width="70px"></asp:Label>
                                                        <asp:Label ID="lblSdino" runat="server" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'></asp:Label>
                                                        <asp:Label ID="gvpcLblSampType" runat="server" Visible="false"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptype")) %>'
                                                                Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Date">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqdatecom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqdat")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sample Type">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvProComTypeName" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Sample Type  " onkeyup="Search_Gridview(this,3, 'gvProCom')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlSampType" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samptypdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last(Forma) Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvProComlformaname" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Last(Forma)" onkeyup="Search_Gridview(this,4, 'gvProCom')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlformanamecom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lformadesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                              
                                                <asp:TemplateField HeaderText="Article No">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvProComarticleno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Article No" onkeyup="Search_Gridview(this,5, 'gvProCom')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvarticlenocom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvColor" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Version">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvVersion5" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="9px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sversion")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Buyer Name">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvBuyerDesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Buyer Name" onkeyup="Search_Gridview(this,8, 'gvProCom')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBuyerDesc" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Catagory">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvcatagorydescom" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Catagory" onkeyup="Search_Gridview(this,9, 'gvProCom')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcatagorydescom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catagorydesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Shoe Type">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvshoetypecom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shoetdesc")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="IMAGE">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyprrrcom" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                            <asp:Image ID="lblImageUrlcom" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Agent">
                                                    <HeaderTemplate>
                                                        <asp:TextBox ID="txtgvagentcom" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Agent" onkeyup="Search_Gridview(this,12, 'gvProCom')"></asp:TextBox><br />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvagentcom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agent")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Sample Size">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamsizecom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Com. Size">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcomsizecom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Size Range">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsizerangecom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: center"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Quantity">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsamqtycom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconamtcom" runat="server" AutoCompleteType="Disabled" Font-Bold="true"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Remarks">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvremarkscom" runat="server" AutoCompleteType="Disabled"
                                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Inquiry Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvinqnocom" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>'
                                                            Width="80px"></asp:Label>



                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                
                                                 <asp:TemplateField HeaderText="PD Book">
                                                     <ItemTemplate>
                                                         <asp:LinkButton ID="gvProComPDBook" ToolTip="Modified PD Book" OnClick="gvPreCostPDBook_Click" runat="server" 
                                                             CommandArgument='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>' ><span class="fa fa-info-circle"></span></asp:LinkButton>
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Center" />
                                                 </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Print">
                                                    <ItemTemplate>
                                                        <div class="dropdown">
                                                            <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                Action                                                                             
                                                            </button>
                                                            <ul class="dropdown-menu dropdown-menu-right">
                                                                <li>
                                                                    <asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>CBD Sheet</asp:HyperLink>

                                                                </li>
                                                                <li>
                                                                    <a data-toggle="modal" id="Retunbtn" data-inqno='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")) %>' class="link text-primary" onclick="Rerunmodal('<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno"))%>')" data-target="#exampleModalSm"><span class="fa fa-reply"></span>Re-Run</a>
                                                                </li>
                                                                <li>
                                                                    <asp:LinkButton ID="LbtnFinal" OnClick="LbtnFinal_Click" runat="server"><span class="fa fa-check-circle"></span>Final Sample</asp:LinkButton>
                                                                </li>
                                                                  <li>
                                                                    <asp:LinkButton ID="LbtnForward" OnClick="LbtnForward_Click" OnClientClick="return confirm('Do You want Forward This Item?');" runat="server"><span class="fa fa-forward"></span>Forward To Concern Company</asp:LinkButton>
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
                                                 <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbltransstatus" Target="_blank" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "transstatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Lbliscomplite" Target="_blank" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "iscomplite")) %>'></asp:Label>
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
                                <a href="<%=this.ResolveUrl("~/F_04_Sampling/RptPdBook?Type=PdBook")%> " target="_blank">04. PD Book Report</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_04_Sampling/RptPdBook?Type=SamReport")%> " target="_blank">05. Date Wise Sample Report</a>
                            </li>
                            <li>
                                <a href="<%=this.ResolveUrl("~/F_04_Sampling/SamTagPrint")%> " target="_blank">06. Sample Tag Print</a>
                            </li>
                        </ul>
                    </asp:Panel>

                    <asp:Panel ID="PnlSalesSetup" runat="server" Visible="false">


                        <ul class="list-unstyled">

                            <li>
                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/AccProjectCode")%> " target="_blank">01. Order Code Book</a>
                            </li>

                            <li>
                                <a href="<%=this.ResolveUrl("~/F_34_Mgt/SalesCodeBook?Type=All")%> " target="_blank">02. General Code Book</a>
                            </li>

                            <li>
                                <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccResourceCode?Type=Matcode")%> " target="_blank">03. Material Opening Code</a>
                            </li>

                            <li>
                                <a href="<%=this.ResolveUrl("~/F_01_Mer/ConsMatStoring?Type=All")%> " target="_blank">04. Department Wise Material Analysis</a>
                            </li>

                            <li>
                                <a href="<%=this.ResolveUrl("~/F_04_Sampling/RptPdBook?Type=PdBookEntry")%> " target="_blank">05. PD Book Information Entry</a>
                            </li>
                            

                        </ul>

                    </asp:Panel>
                </asp:Panel>


                <div id="myModal" class="modal animated slideInLeft" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">

                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span> Sample Final Submit
                                </h4>
                            </div>

                            <div class="modal-body">
                                <asp:Label ID="ModalSdino" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="LblAlrtMsg" runat="server"></asp:Label><br />
                                <div class="log-divider" runat="server" id="Divider" visible="false">
                                    OR                                   
                                
                                </div>
                                <asp:CheckBox ID="MakeNew" runat="server" Checked="true" Text="Make a New Inquiry without merge?" />
                                <div class="alert alert-primary has-icon" role="alert">
                                    <div class="alert-icon">
                                        <span class="fa fa-info"></span>
                                    </div>
                                    This is inform you that after final submit it will appear on Order Accept/Reject Segment in Merchandising Interfaces. Thank you
                                </div>
                            </div>
                            
                            <div class="modal-footer ">
                                <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="fa fa-save"></span>Final Update </asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>

                </div>

                <div id="exampleModalSm" class="modal fade" tabindex="-1" aria-labelledby="mySmallModalLabel" >
                    <!-- .modal-dialog -->
                    <div class="modal-dialog modal-sm">
                        <!-- .modal-content -->
                        <div class="modal-content">
                            <!-- .modal-header -->
                            <div class="modal-header">
                                <h5 class="modal-title" id="mySmallModalLabel">Sample Re Run </h5>
                            </div>
                            <!-- /.modal-header -->
                            <!-- .modal-body -->
                            <div class="modal-body">
                                <div class="form-group">
                                    <asp:TextBox ID="TxtSdino" Style="display: none" Visible="true" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:Label ID="LblCustomer" runat="server" Text="Customer Name" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="DdlCustomer" CssClass="form-control from-control-sm" runat="server"></asp:DropDownList>

                                </div>
                                <div class="form-group">
                                    <label class="label">Select Sample type</label>
                                    <asp:DropDownList ID="DdlSamType" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <!-- /.modal-body -->
                            <!-- .modal-footer -->
                            <div class="modal-footer">
                                <asp:LinkButton ID="LbtnReRunUpdate" runat="server" CssClass="btn btn-primary" OnClick="LbtnReRunUpdate_Click">Update</asp:LinkButton>
                                <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                            </div>
                            <!-- /.modal-footer -->
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>


                <div id="ProductionModal" class="modal fade " tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                    <!-- .modal-dialog -->
                    <div class="modal-dialog" role="document">
                        <!-- .modal-content -->
                        <div class="modal-content">
                            <!-- .modal-header -->
                            <div class="modal-header">
                                <h5 class="modal-title">Sample Production Information </h5>
                            </div>
                            <!-- /.modal-header -->
                            <!-- .modal-body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LblSdino" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" CssClass="label">From Process</asp:Label>
                                            <asp:TextBox ID="TxtFromProces" runat="server" CssClass=" form-control form-control-sm" Enabled="false" TabIndex="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" runat="server" CssClass="label">To Process</asp:Label>
                                            <asp:DropDownList ID="ddlToProcess" runat="server" CssClass=" form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">

                                            <asp:Label ID="Label1" runat="server" CssClass="label">Notes</asp:Label>
                                            <asp:TextBox ID="TxtNotes" TextMode="MultiLine" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvProductionlog" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        Width="449px">
                                        <Columns>
                                            <asp:TemplateField
                                                HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("")+"." %>'
                                                        Width="10px" Style="text-align: left"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="From">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvfromProcess" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fprodesc")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="9px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="9px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="To">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtoProcess" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprodesc")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="9px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="9px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProdDate" runat="server" Style="text-align: left" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "postedat")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="9px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="9px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProdUser" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "posteduser")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Notes">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvNotes" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "notes")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="9px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="9px" />
                                                <FooterStyle HorizontalAlign="right" />
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
                            <!-- /.modal-body -->
                            <!-- .modal-footer -->
                            <div class="modal-footer">
                                <asp:LinkButton ID="LbtnUpdateProduction" OnClick="LbtnUpdateProduction_Click" OnClientClick="CLoseMOdal();" runat="server" CssClass="btn btn-primary">Update Production</asp:LinkButton>
                                <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                            </div>
                            <!-- /.modal-footer -->
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>

                <div id="PDBookModal" class="modal fade " tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                    <!-- .modal-dialog -->
                    <div class="modal-dialog" role="document">
                        <!-- .modal-content -->
                        <div class="modal-content">
                            <!-- .modal-header -->
                            <div class="modal-header">
                                <h5 class="modal-title">PD Book Information </h5>
                                <p runat="server" visible="false"></p>
                            </div>
                            <!-- /.modal-header -->
                            <!-- .modal-body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LblSdinoPdBook" runat="server" Visible="false"></asp:Label>
                                            <asp:Label ID="Label4" runat="server" CssClass="label">Pattern Designer</asp:Label>
                                            <div class="input-group input-group-sm input-group-alt">
                                                <asp:DropDownList ID="DdlPaterDesigner" CssClass="form-control from-control-sm" runat="server"></asp:DropDownList>

                                                <div class="input-group-append">
                                                    <a data-toggle="modal" href="#" onclick="OpenGenCode(36,'Patter Designer')" class="input-group-text text-success" data-target="#exampleModalCode"><span class="fa fa-plus-circle"></span></a>



                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" CssClass="label">Pattern Location</asp:Label>
                                            <asp:TextBox ID="TxtPatLoc" runat="server" CssClass=" form-control form-control-sm" TabIndex="2"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="Label6" runat="server" CssClass="label">Pattern Grading</asp:Label>
                                            <asp:DropDownList ID="DdlPatGrading" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="1">Complete</asp:ListItem>
                                                <asp:ListItem Value="0">In-Complete</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="Label7" runat="server" CssClass="label">Upper Knife </asp:Label>
                                            <asp:DropDownList ID="DdlUppKnif" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="Label8" runat="server" CssClass="label">Lining Knife  </asp:Label>
                                            <asp:DropDownList ID="DdlLinKnif" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="Label9" runat="server" CssClass="label">Bottom Knife </asp:Label>
                                            <asp:DropDownList ID="DdlBotmKniff" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="Label10" runat="server" CssClass="label">Outsole </asp:Label>
                                            <asp:DropDownList ID="DdlOutsole" CssClass="form-control form-control-sm" runat="server">
                                                <asp:ListItem Value="Own">Own</asp:ListItem>
                                                <asp:ListItem Value="Local">Local</asp:ListItem>
                                                <asp:ListItem Value="Import">Import</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="Label11" runat="server" CssClass="label">Remarks</asp:Label>
                                            <asp:TextBox ID="TxtRemarksPdBook" TextMode="MultiLine" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">

                                            <asp:Label ID="lblPPTDate" runat="server" CssClass="label">PPT Date</asp:Label>
                                            <asp:TextBox ID="TxtPPTDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                               Format="dd-MMM-yyyy" TargetControlID="TxtPPTDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <!-- /.modal-body -->
                            <!-- .modal-footer -->
                            <div class="modal-footer">
                                <asp:LinkButton ID="LbtnPdBookUpdate" OnClientClick="CLoseMOdal();" OnClick="LbtnPdBookUpdate_Click" runat="server" CssClass="btn btn-primary">Update PD Book</asp:LinkButton>
                                <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                            </div>
                            <!-- /.modal-footer -->
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>

                <div id="exampleModalCode" class="modal fade " tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                    <!-- .modal-dialog -->
                    <div class="modal-dialog modal-sm" role="document">
                        <!-- .modal-content -->
                        <div class="modal-content">
                            <!-- .modal-header -->
                            <div class="modal-header">
                                <h5 class="modal-title"></h5>
                            </div>
                            <!-- /.modal-header -->
                            <!-- .modal-body -->
                            <div class="modal-body">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCodeType" Style="display: none" Visible="true" runat="server" ClientIDMode="Static"></asp:TextBox>

                                    <label class="label">Write New</label>
                                    <asp:TextBox ID="TxtNewGenCode" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>
                            <!-- /.modal-body -->
                            <!-- .modal-footer -->
                            <div class="modal-footer">
                                <asp:LinkButton ID="LinkButton2" OnClientClick="CLoseMOdal()" OnClick="LbtnNewCodeUpdate_Click" runat="server" CssClass="btn btn-primary">Update</asp:LinkButton>
                                <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                            </div>
                            <!-- /.modal-footer -->
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
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
        function OpenGenCode(CodeType, name) {
            // alert(name)
            $("#exampleModalCode h5").html("<span>New " + name + "</span>");
            $("#txtCodeType").val(CodeType);
        }
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
            //$('#myModal').modal('hide');
            //$('#ProductionModal').modal('hide');
            //$('#PDBookModal').modal('hide');
            //$('#exampleModalCode').modal('hide');

            $('.modal').hide();
            $(".modal-backdrop").hide();
            //$(".modal-backdrop").remove();
        }
        function openProdModal() {

            $('#ProductionModal').modal('toggle');

        }
        function PDBookModal() {

            $('#PDBookModal').modal('toggle');

        }
    </script>

    <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>

    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>

