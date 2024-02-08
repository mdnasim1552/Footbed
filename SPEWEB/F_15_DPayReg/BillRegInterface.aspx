<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="BillRegInterface.aspx.cs" Inherits="SPEWEB.F_15_DPayReg.BillRegInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <style type="text/css">
        .grvHeader th {
            text-align: center;
        }

        .modal-dialog {
            margin: 44px auto;
            width: 100%;
        }

        .InBox {
            color: red !important;
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
                height: 50px;
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
            background: #199698;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            background: #fff none repeat scroll 0 0;
            border: 1px solid #000;
            color: #fff;
            cursor: pointer;
            float: left;
            height: 63px;
            list-style: outside none none;
            margin: 0 5px 0 0;
            padding: 0;
            position: relative;
            text-align: center;
            width: 150px;
        }

            .tbMenuWrp table tr td:nth-child(1) {
                background: #3BA8E0;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(3) {
                background: #EFAD4D;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #CEFEB4;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #90FDD4;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
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
            height: 100%;
            margin: 1px 0;
            padding: 2px;
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
            top: 5px;
        }

        span.lbldata2 {
            /*background: #e5dcdd !important;*/
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
            background: #12A5A6;
            color: red;
        }

        .lblactive label tr td {
            background: #5c5c5c !important;
            color: #fff !important;
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }



        .fan {
            /*border: 2px solid #f3b728;*/
            border-radius: 50%;
            display: inline-block;
            float: left;
            font-size: 16px;
            padding: 5px;
        }

            .fan:nth-child(1) {
                background-color: #ffffff !important;
                color: #E48F23 !important;
            }

            .fan:nth-child(2) {
                color: #52B641 !important;
                background-color: #56B740 !important;
            }

            .fan:nth-child(3) {
                color: #085407;
                background: #085407 !important;
            }

            .fan:nth-child(4) {
                color: #fff;
                background: #DA3F40 !important;
            }

            .fan:nth-child(5) {
                color: #fff;
                background: #009BFF !important;
            }

            .fan:nth-child(6) {
                color: #E4DDDD;
                background: #539250 !important;
            }

            .fan:nth-child(7) {
                color: #E4DDDD;
                background: #E79956 !important;
            }

        /*.modalPopup{
            top:185px !important;
        }*/
    </style>

    <script type="text/javascript">
        //function SetTarget(type) {

        //    window.open('../RDLCViewerWin?PrintOpt=' + type, '_blank');
        //}
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

        function FunCheckedBill(url) {
            window.open('' + url + '', '_blank');
        }

        function FunForwordBill(url) {
            window.open('' + url + '', '_blank');
        }

        function FunApprovedBill(url) {
            window.open('' + url + '', '_blank');
        }

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvBillInfo":
                    tblData = document.getElementById("<%=gvBillInfo.ClientID %>");
                    break;
                case "grvRecm":
                    tblData = document.getElementById("<%=grvRecm.ClientID %>");
                    break;
                case "gvforward":
                    tblData = document.getElementById("<%=gvforward.ClientID %>");
                    break;
                case "grvApproved":
                    tblData = document.getElementById("<%=grvApproved.ClientID %>");
                    break;
                case "grvIssued":
                    tblData = document.getElementById("<%=grvIssued.ClientID %>");
                    break;
                case "gvChequeSign":
                    tblData = document.getElementById("<%=gvChequeSign.ClientID %>");
                    break;
                case "grvComp":
                    tblData = document.getElementById("<%=grvComp.ClientID %>");
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

    </script>


    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%--   <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000">
            </asp:Timer>--%>

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
                                <label class="control-label" for="FromDate">Date</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">Total Bill</label>
                                <asp:Label ID="lblbill" runat="server" CssClass=" form-control "></asp:Label>


                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">Pending Tk</label>
                                <asp:Label ID="lblpanding" runat="server" CssClass="form-control"></asp:Label>

                            </div>
                        </div>

                        <%--<div class="col-md-3">
                            <asp:LinkButton ID="btnSetup" runat="server" CssClass="margin-top30px btn btn-success" OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                                 <asp:LinkButton ID="lnkInteface" runat="server" CssClass="margin-top30px btn btn-secondary " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="lnkReports" runat="server" CssClass="margin-top30px btn btn-warning" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                             
                        </div>--%>
                        <div class="col-md-1">
                            <div class="margin-top30px btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Show Bills</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" CssClass="dropdown-item">Proposal</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" CssClass="dropdown-item">Opening Bills</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" CssClass="dropdown-item" NavigateUrl="~/F_11_Pro/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk">Purchase Tracking</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

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
                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <div>
                                <div class="col-md-12 pading5px asitCol5">
                                </div>

                                <asp:Panel ID="pnlBillInfo" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvBillInfo" runat="server" OnRowDataBound="gvBillInfo_RowDataBound" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True">
                                                <RowStyle />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
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
                                                            <asp:Label ID="lblgvreqno1paym" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="110px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this,2, 'gvBillInfo')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypBill" Visible="false" runat="server" Target="_blank" ForeColor="Blue" Font-Size="10px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>' Width="110px"></span>
                                                            </asp:HyperLink>
                                                            <%# Eval("billno2") %>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hypRq5" runat="server" Target="_blank" ForeColor="Blue" Font-Size="11px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="100px"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1bil" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcHOA" BackColor="Transparent" Width="450px" BorderStyle="None" runat="server" placeholder="Head of Accounts" onkeyup="Search_Gridview(this,3, 'gvBillInfo')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="450px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountrec" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Amt.">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="85px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" Visible="false" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" />
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
                                </asp:Panel>

                                <asp:Panel ID="PnlRecm" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvRecm" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvRecm_RowDataBound" OnRowDeleting="grvRecm_RowDeleting">
                                                <RowStyle />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
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

                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1payma" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="105px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this, 3, 'grvRecm')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("billno2") %>

                                                            <asp:Label ID="lbgvbillno" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Width="105px"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcRefBillNo" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Ref. Bill No" onkeyup="Search_Gridview(this, 4, 'grvRecm')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvrefbillno" runat="server" Width="100px"
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custombillno")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyInprReqno11" runat="server" Target="_blank" ForeColor="Blue" Font-Size="11px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="100px"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcHOA" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Head of Accounts" onkeyup="Search_Gridview(this,5, 'grvRecm')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) 
                                                                        %>'
                                                                Width="400px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource </br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountrecm" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Bill Amtount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" Visible="false" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />


                                                    </asp:TemplateField>
                                                    <asp:TemplateField>

                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllCheckid" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllCheckid_CheckedChanged" Width="20px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCheckid" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>

                                                            <asp:LinkButton ID="lnkbtnChekedId" runat="server" OnClientClick="return confirm('Are you sure you want to Cheked this  Item?');"
                                                                OnClick="lnkbtnChekedId_Click" ToolTip="Cheked"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                                            <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
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

                                <asp:Panel ID="pnlforward" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvforward" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvforward_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNofr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="actcode#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvactcodefr" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode"))%>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqnoBillfr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1fr" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="105px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this, 2, 'gvforward')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("billno2") %>
                                                            <asp:Label ID="lbgvbillnofr" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="105px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcRefBillNo2" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Ref. Bill No" onkeyup="Search_Gridview(this, 3, 'gvforward')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvrefbillno2" Width="100px" runat="server"
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custombillno")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbgvreqnofr" runat="server" Target="_blank" ForeColor="Blue" Font-Size="11px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="100px"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldatefr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcHOA" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Head of Accounts" onkeyup="Search_Gridview(this,4, 'gvforward')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdescfr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) 
                                                                        %>'
                                                                Width="400px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountrecmfr" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Amtount">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotalfr" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamtfr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdatefr" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apppaydate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrintfr" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntryfr" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditINfr" CssClass="btn btn-xs btn-default" Visible="false" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>

                                                            <asp:LinkButton ID="btnDelOrderfr" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="90px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllforwordid" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAllforwordid_CheckedChanged" Width="20px" />

                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkforwordid" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>

                                                            <asp:LinkButton ID="lnkbtnForword" runat="server" OnClientClick="return confirm('Are you sure you want to Forword this Item?');"
                                                                OnClick="lnkbtnForword_Click" ToolTip="Forword"> <span class=" fa fa-check "></span> </asp:LinkButton>

                                                            <%--<asp:LinkButton ID="lnkbtnMerge"runat="server" OnClick="lnkbtnMerge_Click"><span style="color:red" class="glyphicon  glyphicon-plus-sign"></span>  </asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
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

                                <asp:Panel ID="PanelApproved" runat="server" Visible="false">
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
                                                            <asp:Label ID="lblpayidapp" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this, 2, 'grvApproved')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("billno2") %>
                                                            <asp:Label ID="lbgvbillno" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="11px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcRefBillNo3" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Ref. Bill No" onkeyup="Search_Gridview(this, 3, 'grvApproved')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvrefbillno3" Width="100px" runat="server"
                                                                 Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custombillno")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lbgvreqno" runat="server" Target="_blank" ForeColor="Blue" Font-Size="11px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="100px"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="11px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreBillid2" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcHOA" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Head of Accounts" onkeyup="Search_Gridview(this,5, 'grvApproved')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="400px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountapp" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Amt.">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "apdate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" Visible="false" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="90px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAllpayid" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAllpayid_CheckedChanged" Width="20px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkpayid" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lnkbtnPayId" runat="server" OnClientClick="return confirm('Are you sure you want to Approved this Item?');"
                                                                OnClick="lnkbtnPayId_Click" ToolTip="Approved"> <span class=" fa fa-check "></span> </asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
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

                                <asp:Panel ID="PnlIssued" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="grvIssued" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvIssued_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
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

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lnkbtnSplit" runat="server" OnClientClick="return confirm('Are you sure you want to Split this Item?');"
                                                                OnClick="lnkbtnSplit_Click" ToolTip="Split" CssClass="btn  btn-default btn-sm"> <i class="fas fa-minus-square"></i> </span> </asp:LinkButton>


                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno12" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkmerge" runat="server"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmerge"))=="True" %>'
                                                                Width="20px" />
                                                        </ItemTemplate>

                                                        <FooterTemplate>

                                                            <asp:LinkButton ID="lnkbtnMerge" runat="server" OnClientClick="return confirm('Are you sure you want to Merge this Item?');"
                                                                OnClick="lnkbtnMerge_Click" ToolTip="Merge" CssClass="btn  btn-default btn-sm"> <i class="fas fa-plus-square"></i> </span> </asp:LinkButton>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="hypreno2" runat="server" Target="_blank" ForeColor="Blue" Font-Size="11px" Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>' Width="100px"></span>
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="11px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="110px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this, 5, 'grvIssued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Eval("billno2") %>
                                                            <asp:Label ID="lbgvbillno" Visible="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="110px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBillidissu" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Value Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                                                Width="70px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcHOA" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Head of Accounts" onkeyup="Search_Gridview(this,7, 'grvIssued')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="400px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Resource</br> Count" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrescountissued" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Amtount ">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issuedamt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payment Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvappisedate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "appisedate")).ToString("dd-MMM-yyyy")  %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
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

                                <asp:Panel ID="pnlChequeSign" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:GridView ID="gvChequeSign" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno13" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno13" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion" Visible="false">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this, 2, 'gvChequeSign')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvreqno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                        <ItemStyle Font-Size="10px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Voucher No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrVoNo" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>' Width="80px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcHOA" BackColor="Transparent" Width="100px" BorderStyle="None" runat="server" placeholder="Head of Accounts" onkeyup="Search_Gridview(this, 4, 'gvChequeSign')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Party Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvresdesccsign" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))    %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>





                                                    <asp:TemplateField HeaderText="Approved Amtount ">
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblFTotal" runat="server" ForeColor="White"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cheque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvissnochq" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqno"))  %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBnkname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankinf"))  %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cheque <br> Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqdt")) %>'
                                                                Width="80px"></asp:Label>


                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prepared By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preparedid")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Recommendation By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recomid")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "appovedid")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ID #" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesig" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sessionid")) %>'
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>

                                                            <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>

                                                            <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                            </asp:HyperLink>


                                                            <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" />
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
                                </asp:Panel>

                                <asp:Panel ID="PnlComp" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12">

                                            <asp:Label ID="lblmsg" runat="server" Font-Size="12px" Style="font-size: 12px" CssClass="pull-right" Width="100px"></asp:Label>

                                            <asp:GridView ID="grvComp" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="grvComp_RowDataBound">
                                                <RowStyle />
                                                <Columns>


                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvDSlNocpay" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comcod" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvComcod" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>' Width="15px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Payment Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1cpayrID" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))%>' Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requistion" Visible="false">
                                                        <ItemTemplate>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="120px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this, 2, 'grvComp')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvreqnocpayw" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent; word-wrap: normal; word-break: break-all;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Id" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1cpayq" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1"))%>' Width="60px"></asp:Label>

                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Voucher No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvreqno1cpayrVon" runat="server" Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>' Width="95px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Head of Accounts">
                                                        <HeaderTemplate>
                                                            <asp:TextBox ID="txtSrcHOA" BackColor="Transparent" Width="210px" BorderStyle="None" runat="server" placeholder="Head of Accounts" onkeyup="Search_Gridview(this,4, 'grvComp')"></asp:TextBox><br />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvactdesccpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))    %>'
                                                                Width="210px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Party Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgresdesccpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%#   Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc"))    %>'
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="Approved Amt.">
                                                        <%-- <FooterTemplate>
                                                                        <asp:Label ID="lblFTotalcpay" runat="server" ForeColor="White"></asp:Label>
                                                                    </FooterTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvbillamtcpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cheque No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvissnochqcpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqno"))  %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvBnknamecpay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankinf"))  %>'
                                                                Width="160px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Cheque Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lnkgvvaldatecpay" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "checqdt")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Prepared By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesigcpbypay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preparedid")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Recommendation By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesigcrbypay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recomid")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved By" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbgvusrdesigcabypay" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "appovedid")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Size="12px" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAll_CheckedChanged" Text=" ALL" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="true"
                                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "CQPAYTPPARTY"))=="True" %>' />

                                                            <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" Visible="false" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtnUpdate11" runat="server" CssClass="btn btn-xs btn-default" OnClick="lbtnUpdate11_Click">Update</asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="50px" HorizontalAlign="center" />
                                                        <HeaderStyle HorizontalAlign="center" Width="50px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HypChkPrint" runat="server" Target="_blank" CssClass="btn btn-xs btn-default"><span class="fa fa-print"></span>
                                                            </asp:HyperLink>
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
                </div>
            </div>


            <%--            <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
            <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>

            <script src="../Scripts/jquery.counterup.min.js"></script>
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
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>
</asp:Content>


