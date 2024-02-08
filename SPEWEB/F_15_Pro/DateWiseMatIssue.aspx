<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DateWiseMatIssue.aspx.cs" Inherits="SPEWEB.F_15_Pro.DateWiseMatIssue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="~/Scripts/ScrollableGridPluginNew.js"></script>
    <script type="text/javascript" src="~/Scripts/ScrollableTablePlugin.js"></script>
    <script type="text/javascript" src="~/Scripts/gridviewScrollHaVer.js"></script>
    <link href="~/Content/GridViewScrooling.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            let options = { ScrollHeight: 400 };
            let gv1 = $('#<%=this.gvDateMatIssuDetail.ClientID %>');
            gv1.Scrollable(options);

            $('.chzn-select').chosen({ search_contains: true });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });


        };

        function openModal() {
            $('#myModal').modal('toggle');
        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');

        }

        function SetTarget(type) {
            window.open('../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
        }

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvDateMatIssuDetail":
                    tblData = document.getElementById("<%=gvDateMatIssuDetail.ClientID %>");
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

    <style>
        .chzn-container-single .chzn-single {
            height: 30px !important;
            line-height: 30px !important;
            border-radius: 5px !important;
            /*margin-top: 20px !important;*/
        }
    </style>


    <%--Menu Start--%>
    <div class="card card-fluid">
        <div class="card-body">
            <div class="row">

                <div class="col-md-1 form-group">
                    <asp:Label Text="FromDate" runat="server" />
                    <asp:TextBox ID="txtFDate" runat="server" class="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                        Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                </div>

                <div class="col-md-1 form-group">
                    <asp:Label Text="ToDate" runat="server" />
                    <asp:TextBox ID="txtdate" runat="server" class="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                        Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                </div>

                <div class="col-md-1 form-group">
                    <asp:Label ID="LblSeason" runat="server" class="label" for="ToDate">Season</asp:Label>
                    <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-3 form-group">
                    <asp:Label ID="Label3" runat="server" CssClass="label">Master LC</asp:Label>
                    <asp:DropDownList ID="ddlmlccod" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlmlccod_SelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-4 form-group">
                    <asp:Label runat="server">Article List</asp:Label>
                    <asp:DropDownList ID="ddlOrderList" runat="server" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>

                </div>

                <div class="col-md-1 form-group">
                    <asp:Label ID="lblPage" runat="server" class="control-label" for="ddlpagesize">Page Size</asp:Label>
                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm chzn-select"
                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                        Width="85px">
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="30">30</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        <asp:ListItem Value="150">150</asp:ListItem>
                        <asp:ListItem Value="200">200</asp:ListItem>
                        <asp:ListItem Value="300">300</asp:ListItem>
                        <asp:ListItem Value="500">500</asp:ListItem>
                        <asp:ListItem Value="1000">1000</asp:ListItem>
                        <asp:ListItem Value="10000">10000</asp:ListItem>
                    </asp:DropDownList>
                </div>


                <div class="col-md-2" runat="server">
                    <asp:Label runat="server" ID="lblGroup">Mat. Group</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlGroup" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-md-2" runat="server" id="divSubGroup">
                    <asp:Label runat="server" ID="lblSubGroup">Sub Group</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlSubGroup" CssClass="form-control form-control-sm chzn-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-1 form-group">
                    <asp:LinkButton ID="lnkbtnmeok" runat="server" Style="margin-top: 20px; margin-left: 10px" CssClass="btn btn-primary btn-sm" OnClick="lbtnmeOk_Click">Ok</asp:LinkButton>
                </div>

            </div>
        </div>
    </div>
    <!-- Menu End -->

    <!-- Modal -->
    <div class="card card-fluid">

        <header class="card-header">
            <!-- .nav-tabs -->
            <ul class="nav nav-tabs card-header-tabs">
                <li class="nav-item">
                    <a class="nav-link active show" data-toggle="tab" href="#Details">Details</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link " data-toggle="tab" href="#Summary">Summary</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" data-toggle="tab" href="#ItemSummary">Item Summary</a>
                </li>

            </ul>
            <!-- /.nav-tabs -->
        </header>

        <div class="card-body" style="min-height: 450px">
            <%--tab-context begin--%>
            <div class="tab-content">
                <div class="tab-pane fade active show" id="Details">
                    <div class="row">
                        <asp:GridView runat="server" ID="gvDateMatIssuDetail" AutoGenerateColumns="false" CssClass=" table-striped table-hover table-bordered grvContentarea" Width="1000px"
                            ShowFooter="true" ShowHeader="true" OnRowDataBound="gvDateMatIssuDetail_RowDataBound" OnPageIndexChanging="gvDateMatIssuDetail_PageIndexChanging" AllowPaging="true">
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcReqNo" Width="80px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Req NO" Style="text-align: center;" onkeyup="Search_Gridview(this,1, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="80px" Font-Size="10px" ID="lblReqno" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcIssueNo" Width="75px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Issue No" Style="text-align: center;" onkeyup="Search_Gridview(this,2, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="75px" Font-Size="10px" ID="lblIssueNo" Text='<%# DataBinder.Eval(Container.DataItem, "isueno1") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="75px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcIssueDate" Width="55px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Issue Date" Style="text-align: center;" onkeyup="Search_Gridview(this,3, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="55px" Font-Size="10px" ID="lblIssueDate" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuedate","{0:dd-MMM-yyyy}")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="55px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcArticleNo" Width="100px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Article No" Style="text-align: center;" onkeyup="Search_Gridview(this,4, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="100px" Font-Size="10px" ID="lblArticleNo" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcOrderNo" Width="75px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="FB Order No" Style="text-align: center;" onkeyup="Search_Gridview(this,5, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="75px" Font-Size="10px" ID="lblFBOrderNo" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno1")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="75px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcMatDesc" Width="130px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Materials Description" Style="text-align: center;" onkeyup="Search_Gridview(this,6, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="130px" Font-Size="10px" ID="lblMatDes" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemdesc")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="130px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcSpec" Width="200px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Specification" Style="text-align: center;" onkeyup="Search_Gridview(this,7, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="200px" Font-Size="10px" ID="lblSpecification" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcColor" Width="70px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Color" Style="text-align: center;" onkeyup="Search_Gridview(this,8, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="70px" Font-Size="10px" ID="lblColor" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrcSize" Width="30px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="Size" Style="text-align: center;" onkeyup="Search_Gridview(this,3, 'gvDateMatIssuDetail')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="30px" Font-Size="10px" ID="lblSize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizes")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Req Qty">
                                    <ItemTemplate>
                                        <asp:Label Width="60px" Font-Size="10px" ID="lblReqQty" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" CssClass="text-center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue Qty">
                                    <ItemTemplate>
                                        <asp:Label Width="60px" Font-Size="10px" ID="lblIssueQty" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" CssClass="text-center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rem. Issue Qty">
                                    <ItemTemplate>
                                        <asp:Label Width="60px" Font-Size="10px" ID="lblRemIssueQty" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remissuqty")).ToString("#,##0.00;(#,##0.00); ") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" CssClass="text-center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Conv. Qty">
                                    <ItemTemplate>
                                        <asp:Label Width="60px" Font-Size="10px" ID="lblConvQty" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.00;(#,##0.00); ") %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" CssClass="text-center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="60px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Con. Unit">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" Text="Con. Unit"></asp:Label>
                                        <asp:LinkButton runat="server" ID="btnPrintDetails" OnClick="btnPrintDetails_Click"><span class="fa fa-print"></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Width="50px" Font-Size="10px" ID="lblConUnit" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conuntdesc")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" CssClass="text-center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="50px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                        </asp:GridView>
                    </div>
                </div>
                <!--Date Wise Metarial issue Details Procedure-->
                <div class="tab-pane fade " id="Summary">
                    <div class="row">
                        <asp:GridView ID="gvDateMatlist" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnRowDataBound="gvDateMatlist_RowDataBound" Width="100%" AllowPaging="true" OnPageIndexChanging="gvDateMatlist_PageIndexChanging" PageSize="10">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="Sl" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1) %>' Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ISSUE NO">

                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblIsueNO" runat="server"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuName")) %>'
                                                                        ></asp:Label>--%>
                                        <asp:LinkButton Width="70px" Font-Size="10px" ID="lnkOrderDetails" OnClick="lnkOrderDetails_Click" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isueno1")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" CssClass="text-center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="70px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ORDER NAME" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>

                                        <asp:Label ID="lblOrdrNO" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "OrderName")) %>'></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label Text="Total :" runat="server" />
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                    <ItemStyle Width="280px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req NO">

                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblIsueNO" runat="server"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "issuName")) %>'
                                                                        ></asp:Label>--%>
                                        <asp:Label Width="70px" Font-Size="10px" ID="lblReqno" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>' runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="top" CssClass="text-center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <ItemStyle Width="70px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ISSUE DATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsueDate" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ISUEDATE","{0:dd-MMM-yyyy}")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="center" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ITEM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIitmCount" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ItemCount")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="QTY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIitmQty" runat="server" Style="text-align: right"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblQtyTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TOTAL AMT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTtalAmt" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalAmount")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblAmtTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                    <ItemStyle Width="90px" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />

                        </asp:GridView>

                    </div>
                </div>
                <!--Date Wise Metarial issue Details Procedure End-->

                <!--Date Wise Metarial issue Summary Procedure-->
                <div class="tab-pane fade" id="ItemSummary">
                    <div class="row">
                        <asp:GridView ID="gvDateMatlistSummary" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" OnRowDataBound="gvDateMatlistSummary_RowDataBound" Style="width: 100%;" AllowPaging="true" OnPageIndexChanging="gvDateMatlistSummary_PageIndexChanging" PageSize="20">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Details">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsueDate" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Width="200px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmname")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblSummmaryTotal" Text="Total :" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Thickness">
                                    <ItemTemplate>
                                        <asp:Label ID="lblThickness" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcolor" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Width="100px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSize" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Width="100px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="QTY">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsueNO" runat="server"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px" Width="100px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblQTYTotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                        </asp:GridView>
                    </div>
                </div>
                <!--Date Wise Metarial issue Summary Procedure end-->

                <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">

                            <div class="modal-header">
                                <h5 class="modal-title" id="staticBackdropLabel">Date Wise Material Issue Details</h5>

                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-8 bg-apple">
                                        <asp:Label runat="server" ID="lblordernomodal"></asp:Label>
                                    </div>
                                    <div class="col-md-4 bg-green">
                                        <asp:Label runat="server" ID="lblIssno"></asp:Label>
                                    </div>                
                                </div>

                                <div class="row">
                                    <div class="col-md-8 bg-apple">
                                        <asp:Label runat="server" ID="lblordernamemodal"></asp:Label>
                                    </div>
                                    <div class="col-md-4 bg-green">
                                        <asp:Label runat="server" ID="lblissuedate"></asp:Label>
                                    </div>
                                </div>

                                <asp:GridView ID="gvIssueDetails" AutoGenerateColumns="False" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea" Style="width: 100%;">
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MATERIAL NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblImatname" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SPECIFICATIONS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblspcname" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UNIT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIspcname" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIqty" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="text-center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton ID="DetailsPrint" CssClass="btn btn-sm btn-success pull-left" runat="server" OnClick="DetailsPrint_Click"><span class="fa fa-print"></span> Print</asp:LinkButton>
                                <button type="button" class="btn btn-primary btn-sm " data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <%--tab-context end--%>
        </div>
    </div>


</asp:Content>


