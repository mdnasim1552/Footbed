<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptAdminInterface.aspx.cs" Inherits="SPEWEB.F_34_Mgt.RptAdminInterface" %>

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
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });



        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");


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

            //   $('.chzn-select').chosen({ search_contains: true });
        };

    </script>


    <%--<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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

                <div class="col-md-2" style="display: none;">
                    <div class="form-group">
                        <label class="control-label" for="FromDate">Date</label>
                        <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>


                    </div>
                </div>
                <div class="col-md-2" style="display: none;">
                    <div class="form-group">
                        <label class="control-label" for="ToDate">To Date</label>
                        <asp:TextBox ID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                    </div>

                </div>




                <div class="col-md-1">
                    <div class="form-group">
                        <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                    </div>
                </div>

                <div class="col-md-3">
                    <asp:LinkButton ID="btnSetup" runat="server" CssClass="margin-top30px btn btn-success" OnClick="btnSetup_Click">Setting</asp:LinkButton>
                                 <asp:LinkButton ID="lnkInteface" runat="server" CssClass="margin-top30px btn btn-secondary " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                    <asp:LinkButton ID="lnkReports" runat="server" CssClass="margin-top30px btn btn-warning" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton>
                             
                </div>
                <div class="col-md-1" style="display: none;">
                    <div class="margin-top30px btn-group" role="group" aria-label="Button group with nested dropdown">
                        <button type="button" class="btn btn-danger">Operations</button>
                        <div class="btn-group" role="group">
                            <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
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
        <div class="card-body">
            <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>

            <%-- <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000"></asp:Timer>
                            <triggers> <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" /></triggers>--%>

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
                                              <%--  <asp:ListItem Value="7" style="display: none;"></asp:ListItem>
                                                <asp:ListItem Value="8" style="display: none;"></asp:ListItem>--%>


                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </fieldset>
                            <div>
                                <asp:Panel ID="PanlOrdAcRej" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12">
                                            <asp:GridView ID="gvOrdAcRej" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvOrdAcRej_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" 
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' ></asp:Label>
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
                                                            <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Order No" onkeyup="Search_Gridview(this,3, 'gvOrdAcRej')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Edison Article" onkeyup="Search_Gridview(this,4, 'gvOrdAcRej')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Sample Size" onkeyup="Search_Gridview(this,7, 'gvOrdAcRej')"></asp:TextBox><br />
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
                                                            <asp:LinkButton ID="lbtnLink" OnClick="lbtnLink_Click" ToolTip="Accept/Reject" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-link"></span></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Planning">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyProdPlan" Target="_blank" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-file-prescription "></span></asp:HyperLink>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
                                                            <asp:HyperLink ID="HyCommPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" ToolTip="Common Materials" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelPreCost" CssClass="btn btn-xs btn-default" OnClick="btnDelPreCost_Click" ToolTip="Reverse" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                <asp:Panel ID="PanOrdDetApp" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                            <asp:GridView ID="gvOrdDetailsApp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvOrdDetailsApp_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" 
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' ></asp:Label>
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
                                                            <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Order No" onkeyup="Search_Gridview(this,3, 'gvOrdDetailsApp')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Edison Article" onkeyup="Search_Gridview(this,4, 'gvOrdDetailsApp')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Possible Size Range" onkeyup="Search_Gridview(this,6, 'gvOrdDetailsApp')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Sample Size" onkeyup="Search_Gridview(this,7, 'gvOrdDetailsApp')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Consumption Size" onkeyup="Search_Gridview(this,8, 'gvOrdDetailsApp')"></asp:TextBox><br />
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
                                                            <asp:Label ID="Label4" runat="server" Height="16px" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordusrid")) %>'
                                                                Width="80px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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


                                                    <%-- <asp:TemplateField HeaderText="Attachment </br>with Information">
                                                                        <ItemTemplate>
                                                                            <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                                        </ItemTemplate>

                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                        <ItemStyle HorizontalAlign="Right" />

                                                                    </asp:TemplateField>--%>
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
                                                            <asp:HyperLink ID="hypbtnorder" Target="_blank" ToolTip="Consumption" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-edit"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <div class="dropdown">
                                                                <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                    Action
                                                                              <span class="caret"></span>
                                                                </button>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:HyperLink ID="HyPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyCommPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyOrderPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>

                                                                </ul>
                                                            </div>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkCheck" Target="_blank" ToolTip="Consumption Approval" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-check"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelOrder" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                <asp:Panel ID="PnlBomApp" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="gvBOMApp" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvBOMApp_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" 
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' ></asp:Label>
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
                                                            <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order No" onkeyup="Search_Gridview(this,4, 'gvBOMApp')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Edison Article" onkeyup="Search_Gridview(this,5, 'gvBOMApp')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="80px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,7, 'gvBOMApp')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="80px" BorderStyle="None" runat="server" placeholder="Sample Size" onkeyup="Search_Gridview(this,8, 'gvBOMApp')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="80px" BorderStyle="None" runat="server" placeholder="Cons. Size" onkeyup="Search_Gridview(this,9, 'gvBOMApp')"></asp:TextBox><br />
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


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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


                                                    <asp:TemplateField HeaderText="Date">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="Date" onkeyup="Search_Gridview(this,15, 'gvBOMApp')"></asp:TextBox><br />
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
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypbtnMatReqEntry" Target="_blank" ToolTip="Material Requirement Edit" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-edit"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approve">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypbtnMatReq" Target="_blank" ToolTip="Material Requirement Approve" runat="server" CssClass="btn btn-xs btn-default"><span class="fa fa-check"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <div class="dropdown">
                                                                <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                    Action
                                                                              <span class="caret"></span>
                                                                </button>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:HyperLink ID="HyPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyCommPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="OrderPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyFOrderPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink>
                                                                    </li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyLOrderPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnDelOrder" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                <asp:Panel ID="PnlReqApproval" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 450px;">
                                            <asp:GridView ID="gvReqApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvReqApproval_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvapDSlNo" runat="server"  Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvapreqno" runat="server"  Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Store Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvappatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="220px" ></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Dpt. Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvReqApproval')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgdadptdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                Width="120px" ></asp:Label>
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
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req. No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req No" onkeyup="Search_Gridview(this,4, 'gvReqApproval')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvapreqno1" runat="server"  Style="text-align: left"
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
                                                                Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
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
                                                    <asp:TemplateField HeaderText="Resource</br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvaprqrsirdesc" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmcount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblpreqty" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);")%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchTAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Total Amount" onkeyup="Search_Gridview(this,8, 'gvReqApproval')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvapApamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preamt")).ToString("#,##0;(#,##0);") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvapFApamt" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right"  Width="80px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle  HorizontalAlign="Left" />
                                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HypApproval" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="gvHyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelReqRev" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelReqRev_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="120px" />
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

                                <asp:Panel ID="PnlCsAproval" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12">
                                            <asp:GridView ID="gvRatePro" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvRatePro_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server"  Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno" runat="server"  Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Store Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="220px" ></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dpt. Name">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDept" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Dpt. Name" onkeyup="Search_Gridview(this,2, 'gvRatePro')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgdadptdescd" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dptdesc")) %>'
                                                                Width="120px" ></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req <br>  Date">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Date" onkeyup="Search_Gridview(this,3, 'gvRatePro')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvreqrat1" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" Req. No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchReqNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Req. No" onkeyup="Search_Gridview(this,4, 'gvRatePro')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1" runat="server"  Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Mrf No.">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchMRF" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="MRF" onkeyup="Search_Gridview(this,5, 'gvRatePro')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvmrfno" runat="server"  Style="text-align: left"
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
                                                    <asp:TemplateField HeaderText="Resource</br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcount" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmcount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total</br> Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpreqty" runat="server" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0;(#,##0);")%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchtAmt" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Total Amount" onkeyup="Search_Gridview(this,8, 'gvRatePro')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0);") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Width="80px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcurrentStcsap" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle  HorizontalAlign="Left" />
                                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                          

                                                            <asp:LinkButton ID="btnDelCSNext" runat="server" CssClass="btn btn-xs btn-default" OnClick="btnDelCSNext_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="120px" />
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
                                <asp:Panel ID="PanGbillAproval" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12">
                                            <asp:GridView ID="gvPenApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvPenApproval_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNopapr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="reqno#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqnopapr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Requistion <br>Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvreqrat1papr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                                Width="90px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion <br> No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1papr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="90px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MRF No.">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="hlnkgvgvmrfnopapr" runat="server" BorderStyle="none"
                                                                Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                                Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                            </asp:HyperLink>


                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="pactcode" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpactcodepapr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource</br> Count">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountap" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion <br>Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqamtpapr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0);") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                      
                                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvApamtpapr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <FooterStyle HorizontalAlign="Right" Width="60px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CS Attached file" Visible="false">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="hlkQutation" runat="server" Text="Attachded Qutation" Style="width: 100px;" Target="_blank"></asp:HyperLink>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrintfapproved" CssClass="btn btn-xs btn-default" runat="server" Visible="false" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false">
                                             
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelOrderfapproved" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelOrderfapproved_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>



                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requisition Entry">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbleuser" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
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


                                <asp:Panel ID="PanlPayBillAprv" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12">
                                            <asp:GridView ID="grvApproved" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvApproved_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="actcode#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvactcode" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno15" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="70px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <%--    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbgvreqno" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="95"></span>
                                                            </asp:HyperLink>

                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>--%>


                                                    <%--      <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreBillid2" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="70px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>



                                                    <%--      <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Approved By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "posteduser")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="400px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <%-- <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountapp" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Approved Amt.">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "aprvdat")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <%--<asp:TemplateField HeaderText="Curent Status">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle   HorizontalAlign="Left" />
                                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" Visible="false" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
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

                                <%--<asp:Panel ID="pnlBOMGen" Visible="false" runat="server">

                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                            <asp:GridView ID="gvBOMGen" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvBOMGen_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" 
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

                                                        <FooterStyle HorizontalAlign="right" />
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

                                                        <FooterStyle  HorizontalAlign="right" />
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

                                                        <FooterStyle  HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchArt" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Article" onkeyup="Search_Gridview(this,3, 'gvBOMGen')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Edison Article" onkeyup="Search_Gridview(this,4, 'gvBOMGen')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Size Range" onkeyup="Search_Gridview(this,6, 'gvBOMGen')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Sample Size" onkeyup="Search_Gridview(this,7, 'gvBOMGen')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Consumption Size" onkeyup="Search_Gridview(this,8, 'gvBOMGen')"></asp:TextBox><br />
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
                                                    <asp:TemplateField HeaderText="Attachment </br>with Information">
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
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BOM">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypbtnMatReq" Target="_blank" ToolTip="Material Requirement" runat="server" CssClass="btn btn-xs btn-info"><span class="fa fa-openid"></span></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <div class="dropdown">
                                                                <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                    Action
                                                                              <span class="caret"></span>
                                                                </button>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:HyperLink ID="HypOrderEdit" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" Font-Underline="false"><span class="glyphicon glyphicon-edit"></span>Edit Order</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyCommPreCostPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="OrderPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:LinkButton ID="ReplaceThumbnail" CssClass="btn btn-xs btn-default" OnClick="ReplaceThumbnail_Click" runat="server"><span class="glyphicon glyphicon-picture"></span>Replace Thumb</asp:LinkButton>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelOrde_App" OnClick="btnDelOrde_App_Click" ToolTip="Reverse" runat="server" OnClientClick="return confirm('Do You want Delete This Item?');"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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


                                </asp:Panel>--%>


                                <%--<asp:Panel ID="PnlProComp" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

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
                                                            <asp:TextBox ID="txtSearchAutoartcle" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="EDISON ARTICLE" onkeyup="Search_Gridview(this,1, 'gvProCom')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchCat" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="STYLE" onkeyup="Search_Gridview(this,3, 'gvProCom')"></asp:TextBox><br />
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

                                                    <asp:TemplateField HeaderText="Possible Size Range" Visible="false">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchSizeRng" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Possible Size Range" onkeyup="Search_Gridview(this,7, 'gvProCom')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchSam" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Sample Size" onkeyup="Search_Gridview(this,8, 'gvProCom')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtSearchCon" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Consumption Size" onkeyup="Search_Gridview(this,9, 'gvProCom')"></asp:TextBox><br />
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

                                                    <asp:TemplateField HeaderText="ORDER QTY">
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
                                                    <asp:TemplateField HeaderText="REAL PRICE">
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
                                                    <asp:TemplateField HeaderText="Attachment </br>with Information">
                                                        <ItemTemplate>
                                                            <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'>View Doc <span class="fa fa-eye"></span></a>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ORDER <br> RECEIVE DATE">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSearchDate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="ORDER RECEIVE DATE" onkeyup="Search_Gridview(this,11, 'gvProCom')"></asp:TextBox><br />
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
                                                            <asp:TextBox ID="txtShipdate" BackColor="Transparent" BorderStyle="None" Width="80px" runat="server" placeholder="SHIPMENT DATE" onkeyup="Search_Gridview(this,12, 'gvProCom')"></asp:TextBox><br />
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
                                                    <asp:TemplateField HeaderText="Print">
                                                        <ItemTemplate>
                                                            <div class="dropdown">
                                                                <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                    Action
                                                                              <span class="caret"></span>
                                                                </button>
                                                                <ul class="dropdown-menu">
                                                                    <li>
                                                                        <asp:HyperLink ID="HyOrderPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order Print</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyFOrderPrint" CssClass="btn btn-xs btn-default" ToolTip="Import BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink></li>
                                                                    <li>
                                                                        <asp:HyperLink ID="HyLOrderPrint" CssClass="btn btn-xs btn-default" ToolTip="Locl BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink>
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
                                </asp:Panel>--%>



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

                                <a href="<%=this.ResolveUrl("~/F_01_Mer/ConsMatStoring?Type=Chemical")%> " target="_blank">04. Chemical Department Material Analysis</a>
                            </li>
                            <li>

                                <a href="<%=this.ResolveUrl("~/F_01_Mer/ConsMatStoring?Type=Packing")%> " target="_blank">05. Packing Department Material Analysis</a>
                            </li>
                            <li>

                                <a href="<%=this.ResolveUrl("~/F_01_Mer/SampleInquiryLIst")%> " target="_blank">06. Sample Inquiry List</a>
                            </li>



                        </ul>

                    </asp:Panel>
                </asp:Panel>


                <div id="myModal" class="modal animated slideInLeft" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content  ">
                            <div class="modal-header">
                                <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>
                                    <asp:Label ID="lblmodalhead" runat="server"></asp:Label></h4>
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
                                        <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                            OnClientUploadComplete="uploadComplete" runat="server"
                                            ID="AsyncFileUpload1" UploaderStyle="Modern"
                                            CompleteBackColor="White"
                                            UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                            OnUploadedComplete="FileUploadComplete" />
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

            <%-- </ContentTemplate>

                    </asp:UpdatePanel>--%>
        </div>
    </div>


    <%--        </ContentTemplate>



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

