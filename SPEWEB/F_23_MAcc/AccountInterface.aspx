<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccountInterface.aspx.cs" Inherits="SPEWEB.F_23_MAcc.AccountInterface" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .InBox {
            color: red !important;
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
        /* Chrome, Safari, Opera */
        @-webkit-keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
        }

        /* Standard syntax */
        @keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
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
            /*height: 50px;*/
            height: 36px;
            width: 110px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0px 0px;
            color: #fff;
            text-align: center;
            border: 2px solid #9752A2;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            font-size:10px;
            font-family:Cambria !important;
        }


        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            cursor: pointer;
            /*width: 130px;*/
            /*background: whitesmoke;*/
            border-radius: 25px;
            color: #000;
            font-weight: bold;
            padding: 0 0 0 4px;
            line-height: 30px;
            margin: 1px 2px;
            display: block;
            text-align: left;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                background: #12A5A6;
                color: #fff;
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
        }

        .tbMenuWrp table tr td label span.lbldata {
            background: #F3B728;
            border: 1px solid #F3B728;
            border-radius: 50%;
            /*display: block;*/
            height: 30px;
            font-size: 10px;
            line-height: 18px;
            margin: 0 5px 0 0;
            padding: 4px 1px;
            width: 28px;
            float: left;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            background: #12A5A6;
            color: #fff;
        }

        .grvContentarea tr td:last-child {
            width: 120px;
        }
    </style>


    <script type="text/javascript">
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

        function Search_Gridview(strKey, cellNr, gvName) {
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvPurchase":
                    tblData = document.getElementById("<%=gvPurchase.ClientID %>");
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
        };

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="2000000000">
            </asp:Timer>

            <triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>




            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">Date</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                                <asp:DropDownList ID="ddlCompany" Visible="false" CssClass="chzn-select fromddl " Width="200px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>





                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="btnSetup" runat="server" CssClass="margin-top30px btn btn-success" OnClick="btnSetup_Click">Setting</asp:LinkButton></li>
                                 <asp:LinkButton ID="lnkInteface" runat="server" CssClass="margin-top30px btn btn-secondary " OnClick="lnkInteface_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="lnkReports" runat="server" CssClass="margin-top30px btn btn-warning" OnClick="lnkRept_Click">ALL Reports</asp:LinkButton></li>
                             
                        </div>
                        <div class="col-md-1">
                            <div class="margin-top30px btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>


                                        <asp:HyperLink ID="LinkButton1" runat="server" Target="_blank" NavigateUrl="~/F_15_Acc/GeneralAccounts?tcode=99&tname=Payment%20Voucher&Mod=Accounts" CssClass="dropdown-item">Payment Voucher</asp:HyperLink>
                                        <asp:HyperLink ID="LinkButton2" runat="server" Target="_blank" NavigateUrl="~/F_15_Acc/GeneralAccounts?tcode=99&tname=Deposit%20Voucher&Mod=Accounts" CssClass="dropdown-item">Deposit Voucher</asp:HyperLink>
                                        <asp:HyperLink ID="LinkButton3" runat="server" Target="_blank" NavigateUrl="~/F_15_Acc/GeneralAccounts?tcode=99&tname=Journal%20Voucher&Mod=Accounts" CssClass="dropdown-item">Journal Voucher</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_15_Acc/SuplierPayment?tcode=99&tname=Payment Voucher&Mod=Accounts" CssClass="dropdown-item">Supplier Payment</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_15_Acc/TransectionPrint?Type=AccVoucher&Mod=Accounts" CssClass="dropdown-item">Voucher Print</asp:HyperLink>

                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Acc/AccDashBoard" CssClass="dropdown-item">Dashboard</asp:HyperLink>


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
                    <asp:Panel ID="pnlInterAcc" runat="server">
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
                                                <asp:ListItem Value="9"></asp:ListItem>



                                            </asp:RadioButtonList>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>



                           

                                    
                                    <asp:Panel ID="pnlUpSales" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvSalesUpdate" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvSalesUpdate_RowDataBound">
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
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcentrid" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                    Width="49px"></asp:Label>
                                                                <asp:Label ID="lgcorderno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcentrdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Ref No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvinvno1" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcInvRefno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invrefno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Size="10px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtgvDate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcustdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice </Br> Qty">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitmamtordrqty" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblINAmtTotalordrqty" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Invoice </Br> Rate" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbratelitmamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Invoice </Br> Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitmamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblINAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"  Target="_blank"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="pnlUpColl" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">
                                                <asp:GridView ID="gvCollUpdate" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvCollUpdate_RowDataBound"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvINSlNo0" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvINcentrid" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MR Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgINcorderno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                                    Width="100px"></asp:Label>
                                                                <asp:Label ID="lgINcorderno" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtINgvDate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtINgvRemarks" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "udesc")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcactdesc" runat="server" BackColor="Transparent" Font-Size="9px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relz Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvCheqNumb" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relz Amount </br>(FC)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblINgvfcamount" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamount")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblINgvFfcamount" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bank Charge</br> (FC)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvfcbnkcharge" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcbnkcharge")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFfcbnkcharge" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Currency">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcurdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Relz Amount </br> BDT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblINgvitmamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblINAmtTotal" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bank Charge  </br> BDT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpaidamtt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblINPTotal" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Gain/ Loss</br> BDT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvcglamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cglamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFcglamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="PanlUpLc" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12">

                                                <asp:GridView ID="gvLcUpdate" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvLcUpdate_RowDataBound"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvAPPSlNo0" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvAPPcentrid" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Store Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAPPgvteamdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GRN Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgAPPcorderno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grrno")) %>'
                                                                    Width="110px"></asp:Label>
                                                                <asp:Label ID="lgAPPcorderno" runat="server" Visible="false"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                    Width="110px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAPPgvDate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="L/C Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtAPPgvRemarks" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcno")) %>'
                                                                    Width="130px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="L/C Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvlcamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFlcamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="pnlPurchase" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvPurchase" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvPurchase_RowDataBound"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoRD" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="billno" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbillno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ssircode" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvssircode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgbillno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Req. Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgvreqno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Challan No">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgvchlnno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvbilldat" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supplier Name">
                                                            <HeaderTemplate>
                                                                <asp:TextBox ID="txtSrchSuppl" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Supplier Name" Style="text-align: center;" onkeyup="Search_Gridview(this,5, 'gvPurchase')"></asp:TextBox><br />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPssirdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                    Width="230px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPurbillamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0);") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFPurbillamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <FooterStyle HorizontalAlign="Right" Width="50px"  />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="PanelIssue" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvIssue" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvIssue_RowDataBound"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoPCon" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="preqno" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvpreqno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Batch Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="250px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issue Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgpreqno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtissdat" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "issdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Issue Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvissueamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFissueamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="PanelProduction" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvProd" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvProd_RowDataBound"
                                                    ShowFooter="True">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDisp" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvprodid" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodid")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="250px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Production Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgProtextfield" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodid")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtproddate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "proddate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Buyer Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgBatdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchdesc")) %>'
                                                                    Width="250px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Production Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblprodamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prodamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFprodamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="PnlBRec" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvBankRec" runat="server" AllowPaging="True"
                                                    CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                    AutoGenerateColumns="False"
                                                    ShowFooter="True" Width="104px" PageSize="20">
                                                    <RowStyle />


                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="serialnoid" runat="server"
                                                                    Style="text-align: right; font-size: 11px;"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle  Font-Size="14px" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSUBCODE" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                                    Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="VOUNUM" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVOUNUM" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                                    Width="87px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Recon.Date (dd.mm.yyyy)">
                                                            <EditItemTemplate>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <%--    <asp:Label ID="lblRECNDT" runat="server" Font-Size="11px" 
                                                Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")) %>' 
                                                Width="80px"></asp:Label>--%>


                                                                <asp:TextBox ID="txtgvRECNDT" runat="server" Font-Size="11px"
                                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd.MM.yyyy")) %>'
                                                                    Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>

                                                                <cc1:CalendarExtender ID="txtgvRECNDT_CalendarExtender" runat="server"
                                                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtgvRECNDT"></cc1:CalendarExtender>

                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cheq./ Ref. No.">
                                                            <%-- <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                                                </FooterTemplate>--%>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblREFNO" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vou.Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVOUDAT" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="66px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vou.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVOUNUM1" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deposit">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPayment" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Payment">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDeposit" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Accounts Head">

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTRANSDES" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="160px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Details Head">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvDetailsHead" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc1")) %>'
                                                                    Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Narration">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvVarnar" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                                                    Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rpcode" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvRpCode" runat="server" Font-Size="11px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                                                    Width="100px"></asp:Label>
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
                                    <asp:Panel ID="PnlPDC" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                                <asp:GridView ID="dgPdc" runat="server" AutoGenerateColumns="False"
                                                    OnRowDataBound="dgPdc_RowDataBound" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="serialnoid" runat="server"
                                                                    Style="text-align: right; font-size: 11px;"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle  Font-Size="14px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AC.Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvAccCod" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cat.Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgcatCod" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ResCode" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcUcode" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank Name">
                                                            <HeaderTemplate>
                                                                <table style="width: 180px;">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server"  Text="Bank Name"
                                                                                Width="100px"></asp:Label>
                                                                        </td>
                                                                        <td class="style60">
                                                                            <asp:HyperLink ID="hlbtnbtbCdataExel" runat="server" BackColor="#000066"
                                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                                                ForeColor="White" Style="text-align: center" Width="90px" Target="_blank">Export Exel</asp:HyperLink>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvbankname" runat="server" 
                                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "cactdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")).Trim(): "")  %>'
                                                                    Width="180px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Voucher #" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvPVnum" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issue #">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvvounum1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvPVDate" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acc. Description">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgactdesc" runat="server"
                                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")  %>'
                                                                    Width="200px"></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdateAllVou" runat="server" 
                                                Font-Size="13px" ForeColor="White" OnClick="lbtnUpdateAllVou_Click"
                                                Style="text-align: Center; height: 15px;" Width="80px">Update All</asp:LinkButton>
                                        </FooterTemplate>--%>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issue Ref">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvisunum" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cheque No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvchnono" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cheque Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvchdat" runat="server" Style="text-align: left"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequedat")) %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvcramt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cramt")).ToString("#,##0;(#,##0); ") %>'
                                                                    Width="70px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lgvFCrAmt" runat="server"  Font-Size="12px"
                                                                    ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                            </FooterTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Dr. Amt" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvdramt" runat="server"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reconcilaition Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvReconDat" runat="server" BackColor="Transparent"
                                                                    BorderStyle="None" Style="text-align: left; font-size: 11px;"
                                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?""
                                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd-MMM-yyyy")%>'
                                                                    Width="70px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkvmrno" runat="server"
                                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                                    Width="20px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbok" runat="server" CommandArgument="lbok"
                                                                    Width="30px">OK</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Voucher Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvNewVoNum" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "newvocnum")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvParName" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payto")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvBill" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                                    Width="100px"></asp:Label>
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

                                    <asp:Panel ID="PanelChallan" Visible="false" runat="server">

                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvChallan" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvChallan_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDeliv" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Center Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgpactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Challan Number">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvPrchlnno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chlnno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtchlndat" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chlndat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Challan </br> Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblchlamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "chlamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFchlamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder"  CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                    <asp:Panel ID="PnlMTras" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvMatTrasfer" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvMatTrasfer_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDeliv" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvtrnno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Order Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgpactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                   ></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txttrnsdate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "trnsdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Transfer Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltrsnamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trsnamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFchlamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="PnlSRet" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                                <asp:GridView ID="gvSRet" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvSRet_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDeliv" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvretmemo" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Center Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Return </Br>Number">
                                                            <ItemTemplate>

                                                                <%-- <asp:Label ID="lgretmemo1" runat="server"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "retmemo1")) %>'
                                                                        Width="90px"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgssirdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                                    Width="160px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <%--  <asp:Label ID="txtretdat" runat="server" BackColor="Transparent"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "retdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Return </Br Amount">
                                                            <ItemTemplate>
                                                                <%-- <asp:Label ID="lblretamt" runat="server" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFretamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                    <asp:Panel ID="PnlSer" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvService" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvService_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDeliv" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvrecvno" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recvno")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Center Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Received </Br Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgrecvno1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "recvno1")) %>'
                                                                    Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtexecdate" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "execdate")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgcustname" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custname")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Address">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgcustadd" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custadd")) %>'
                                                                    Width="150px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Phone">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgcustmob" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custmob")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Service </Br Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblseramt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "seramt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFseramt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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
                                    <asp:Panel ID="PnlSAdj" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                                <asp:GridView ID="gvSalAdj" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    ShowFooter="True" OnRowDataBound="gvSalAdj_RowDataBound">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNoDeliv" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvmemono" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono")) %>'
                                                                    Width="49px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Center Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgactdesc" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                                    Width="120px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Adjustment </Br> Number">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lgmemono1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono1")) %>'
                                                                    Width="100px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date">

                                                            <ItemTemplate>
                                                                <asp:Label ID="txtmrdat" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdat")).ToString("dd-MMM-yyyy") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Adjustment </Br>  Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsaladjamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "saladjamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblFsaladjamt" runat="server" Style="text-align: right"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-print"></span></asp:HyperLink>
                                                                <asp:HyperLink ID="lnkbtnApp" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                                </asp:HyperLink>
                                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" OnClientClick="return confirm('Do You want to delete this item?');" OnClick="btnDelCSChk" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
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

                                    <asp:Panel ID="pnlUnPost" Visible="false" runat="server">
                                        <div class="row">
                                            <div class="table-responsive col-lg-12" style="min-height: 350px;">
                                                <asp:GridView ID="gvAccUnPosted" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAccUnPosted_RowDataBound"
                                                    ShowFooter="True" Width="550px">
                                                    <RowStyle />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvINSlNo0" runat="server" 
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvvoudat" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "voudat")) %>'
                                                                    Width="80px"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterStyle  HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Voucher">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblvounum" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Reference">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtINgvteamdesc" runat="server" BackColor="Transparent"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="Voucher Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvvouamt" runat="server" Style="text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblgvFvouamt" runat="server" Style="text-align: right" Width="90px"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right"  />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkvmrno" runat="server" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'
                                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv"))=="True" %>'
                                                                    Width="20px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                            
                                                                <asp:LinkButton ID="lnkbtnPrintIN" CssClass="btn btn-xs btn-default" runat="server" OnClick="lnkbtnPrintRD_Click"><span class="fa fa-print"></span></asp:LinkButton>
                                                                <asp:HyperLink ID="hlnkVoucherEdit" CssClass="btn btn-xs btn-default" ToolTip="Edit" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-edit"></span>
                                                                </asp:HyperLink>


                                                                <asp:LinkButton ID="lbtnVoucherApp" CssClass="btn btn-xs btn-default" ToolTip="Approved" runat="server" OnClick="lbtnVoucherApp_Click" Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkmv")) == "True" ? false : true %>'><span class=" fa fa-check"></span> </asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnDeleteVoucher" CssClass="btn btn-xs btn-default" ToolTip="Delete" runat="server" OnClick="lbtnDeleteVoucher_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="300px" />
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
                      
                    </asp:Panel>
                </div>

                    <asp:Panel ID="pnlAcc" runat="server" Visible="false">


                        <div class="form-group">

                            <div class="col-md-4 col-md-offset-4  padingLeft5px lbl2SubMenu ">

                                <ul class="nav colMid" id="SERV">
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccFinalReports?RepType=BS")%> " target="_blank">01. Statement Of Financial Position</a>
                                    </li>


                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccFinalReports?RepType=IS")%> " target="_blank">02. Statement Of Comprehensive Income</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccFinalReports?RepType=SHEQUITY")%> " target="_blank">03. Statement Of Share Holder's Equity</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/RptBankCheque?Type=CashFlow")%> " target="_blank">04. Statement Of Cash Flow</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccTrialBalance?Type=Details")%> " target="_blank">05. Notes: Financial Postion</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccTrialBalance?Type=INDetails")%> " target="_blank">06. Notes: Comprehensive Income</a>
                                    </li>

                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/RptAccDTransaction?Type=Accounts&TrMod=RecPay")%> " target="_blank">07. Receipts & Payment</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccTrialBalance?Type=Mains")%> " target="_blank">08. Trial Balance</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/RptAccDayTransData")%> " target="_blank">09. Daily transaction- All</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/RptAccDTransaction?Type=Accounts&TrMod=ProTrans")%> " target="_blank">10. Daily Transaction- Individual</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccLedger?Type=Ledger&RType=GLedger")%> " target="_blank">11. Ledger</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccControlSchedule?Type=Type01")%> " target="_blank">12. Accounts Control Schedule</a>
                                    </li>
                                    <li>
                                        <a href="<%=this.ResolveUrl("~/F_15_Acc/AccDetailsSchedule")%> " target="_blank">13. Accounts Details Schedule</a>
                                    </li>


                                </ul>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </asp:Panel>

                </div>

            </div>
           <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
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

