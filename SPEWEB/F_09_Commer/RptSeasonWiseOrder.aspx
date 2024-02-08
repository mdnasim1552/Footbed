<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptSeasonWiseOrder.aspx.cs" Inherits="SPEWEB.F_09_Commer.WebForm1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/highchartwithmap.js"></script>

    <script src="../Scripts/highchartexporting.js"></script>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>

    <script type="text/javascript">
        $(document).ready(function () {

            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $('[id*=DdlSeason]').multiselect({
                includeSelectAllOption: true,
                searchable: true,
                enableFiltering: true,
                maxHeight: 200
            })

            $('.chzn-select').chosen({ search_contains: true });

            var gvMasterPOR = $('#<%=this.gvMasterPOR.ClientID %>');
            gvMasterPOR.Scrollable();

        }

        function Search_Gridview(strKey, cellNr, gvName) {

            var tblData;
            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "gvMasterPOR":
                    tblData = document.getElementById("<%=gvMasterPOR.ClientID %>");
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
        .well h4 {
            margin: 0;
            font-size: 15px;
            font-weight: bold;
        }

        .well p {
            margin: 0;
        }

        .multiselect-native-select {
            width: 100% !important;
        }

        .multiselect {
            width: 100% !important;
            border: 1px solid;
            height: 29px;
            border-color: silver
        }

        .multiselect-container {
            overflow: scroll;
            width: 100% !important;
            max-height: 300px !important;
            border-color: silver
        }

        .multiselect-native-select > .btn-group {
            width: 100% !important;
        }

        label {
            margin-bottom: 0rem !important;
        }

        .chzn-container {
            border-radius: 5px;
        }

            .chzn-container .chzn-drop {
                width: 100% !important;
            }

        .chzn-search input {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
        }
    </style>

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

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1">
                            <asp:Label ID="Label5" runat="server" Text="As on Date:" CssClass="col-form-label"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblseason" runat="server" CssClass="col-form-label">Season</asp:Label>
                            <asp:ListBox ID="DdlSeason" runat="server" CssClass="form-control form-control-sm " SelectionMode="Multiple"></asp:ListBox>
                        </div>

                        <div class="col-md-3 form-group" runat="server" id="divddlSupplierName" visible="false">
                            <asp:Label ID="Label8" runat="server" Style="margin-right: 20px" Text="Supplier :" CssClass="col-form-label"></asp:Label>
                            <asp:CheckBox ID="ChckShipper1" runat="server" CssClass="checkbox-balance" Text="As A Shipper" />
                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                            <%--<cc1:ListSearchExtender ID="ddlSupplierName_ListSearchExtender" runat="server" QueryPattern="Contains" TargetControlID="ddlSupplierName">
                                    </cc1:ListSearchExtender>--%>
                        </div>

                        <div class="col-md-2" runat="server" id="divMatGroup">
                            <asp:Label runat="server" ID="lblCodeBook">Mat. Group</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlCodeBook" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeBook_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2" runat="server" id="divSubGroup">
                            <asp:Label runat="server" ID="lblGroup">Sub Group</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlGroup" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1 form-group" style="margin-top: 20px">
                            <asp:LinkButton ID="lnkbtnOk" runat="server" CssClass="btn btn-sm btn-primary ml-3" OnClick="lnkbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-1 form-group">
                            <asp:Label ID="lblPage" runat="server" Text="Page Size" CssClass=""></asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18" AutoPostBack="true">
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                                <asp:ListItem Value="600">600</asp:ListItem>
                                <asp:ListItem Value="900">900</asp:ListItem>
                                <asp:ListItem Value="1500">1500</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1 form-group">
                            <asp:Label ID="lblAggrgt" runat="server" Text="Aggregate" CssClass="label" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlAggrgt" runat="server" CssClass="form-control form-control-sm" Visible="false">
                                <asp:ListItem Value="MIN">Minimum</asp:ListItem>
                                <asp:ListItem Value="MAX">Maximum</asp:ListItem>
                                <asp:ListItem Value="AVG">Average</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1 form-group" style="margin-top: 20px; margin-left: 20px">
                            <asp:CheckBox ID="ChkMaxPrice" class="btn-outline-primary border-dark" Text="Max.Price" runat="server" Visible="false"></asp:CheckBox>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 600px">

                    <asp:MultiView ID="MV1" runat="server">

                        <asp:View ID="SeasonBySeasonView" runat="server">
                            <div class="row">
                                <div class="col-md-9">
                                    <div class="table-responsive">

                                        <asp:GridView ID="gvOrderStatus" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvOrderStatus_PageIndexChanging" ShowFooter="true">
                                            <PagerSettings Position="Top" />
                                            <RowStyle Font-Size="11px" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Sl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo" runat="server" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" Width="35px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Supplier Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="left" Width="170px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs1" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs1" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs2" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs2" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs3" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs3" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs4" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs4" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs5" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs5" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs6" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs6" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs7" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs7" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs8" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs8" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs9" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs9" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvs10" runat="server" Font-Size="11px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lgvs10" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                                    </div>
                                    <br />

                                    <section class="card card-fluid">
                                        <header class="card-header">
                                            <ul class="nav nav-tabs card-header-tabs">
                                                <li class="nav-item">
                                                    <a class="nav-link active show" data-toggle="tab" href="#home">Material Price Variance</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" data-toggle="tab" href="#profile">Material-Specification Price Variance</a>

                                                </li>
                                            </ul>
                                        </header>
                                        <div class="card-body">
                                            <div id="myTabContent" class="tab-content">

                                                <div class="tab-pane fade active show" id="home">
                                                    <div class="row">
                                                        <div class="table-responsive">

                                                            <asp:GridView ID="gvPriceVar" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                                                AutoGenerateColumns="False" ShowFooter="true" OnPageIndexChanging="gvPriceVar_PageIndexChanging"
                                                                OnRowCreated="gvPriceVar_RowCreated">
                                                                <PagerSettings Position="Bottom" />
                                                                <RowStyle Font-Size="11px" />
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Sl.">
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton ID="imgbtnprint1" runat="server" OnClick="imgbtnprint1_Click" CssClass="btn-success btn-sm"><span class="fa fa-print"></span></asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNum1" runat="server" Height="16px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="right" Width="35px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Material Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMatName1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="left" Width="170px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Unit">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblunt1" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvo1" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvo1" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvr1" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvr1" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt1" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvamt1" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvo2" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o2")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvo2" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvr2" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvr2" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt2" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvamt2" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvo3" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o3")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvo3" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvr3" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvr3" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt3" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvamt3" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvo4" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o4")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvo4" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvr4" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvr4" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt4" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvamt4" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvo5" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o5")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvo5" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvr5" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvr5" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvamt5" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgvamt5" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
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
                                                </div>

                                                <div class="tab-pane fade" id="profile">
                                                    <div class="row">
                                                        <div class="table-responsive">

                                                            <asp:GridView ID="gvSpcfPriceVar" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                                                AutoGenerateColumns="False" ShowFooter="true" OnPageIndexChanging="gvSpcfPriceVar_PageIndexChanging"
                                                                OnRowCreated="gvSpcfPriceVar_RowCreated" OnRowDataBound="gvSpcfPriceVar_RowDataBound">
                                                                <PagerSettings Position="Bottom" />
                                                                <RowStyle Font-Size="11px" />
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Sl.">
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton ID="imgbtnprint2" runat="server" OnClick="imgbtnprint2_Click" CssClass="btn-success btn-sm"><span class="fa fa-print"></span>
                                                                            </asp:LinkButton>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSlNum2" runat="server" Height="16px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="right" Width="35px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Material Name">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMatName2" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="left" Width="170px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Specification">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSpcf2" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="left" Width="200px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Unit">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblunt2" runat="server"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvo1" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvo1" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvr1" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvr1" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvamt1" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt1")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvamt1" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvo2" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o2")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvo2" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvr2" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r2")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvr2" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvamt2" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt2")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvamt2" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvo3" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o3")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvo3" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvr3" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r3")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvr3" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvamt3" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt3")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvamt3" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvo4" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o4")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvo4" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvr4" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r4")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvr4" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvamt4" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt4")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvamt4" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvo5" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "o5")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvo5" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvr5" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "r5")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvr5" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrvamt5" runat="server" Font-Size="11px"
                                                                                Style="text-align: right"
                                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt5")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lgrvamt5" runat="server" Font-Size="11px" Height="16px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                        <ItemStyle HorizontalAlign="Right" Width="75px" />
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
                                                </div>

                                            </div>
                                        </div>
                                    </section>

                                </div>
                                <div class="col-md-3">
                                    <div id="seasonSum" style="width: 300px; height: 300px; margin: 0 auto"></div>
                                </div>
                            </div>
                        </asp:View>

                        <asp:View ID="SeasonOverview" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvSeasonOverview" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvSeasonOverview_PageIndexChanging" ShowFooter="true">
                                    <PagerSettings Position="Top" />
                                    <RowStyle Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblSlNo" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" Width="35px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" Width="170px" />
                                        </asp:TemplateField>

                                        <%--                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="gv2LblCode" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Supplier">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblSupp" runat="server" Font-Size="11px"
                                                    CssClass="text-left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "suplier")) %>'></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Customer">
                                                <ItemTemplate>
                                                    <asp:Label ID="gv2LblCustomer" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="75px" />
                                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblColor" runat="server" Font-Size="11px"
                                                    CssClass="text-left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UM">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblUM" runat="server" Font-Size="11px"
                                                    CssClass="text-left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Requisition">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblTtlReq" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req+ % Allow">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblReqAllow" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqandallw")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Stock">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblCurntStck" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Need To Buy">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblNdToBuy" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ndtobuy")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total PO Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblTtlPoQty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Need To Buy PO">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblNdToBuyPO" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ndtobuypo")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipped Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblShipedQty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO-Ship">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblPoShip" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "poship")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Inhoused Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblInhousedQty" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Inhoused">
                                            <ItemTemplate>
                                                <asp:Label ID="gv2LblPoInhousd" runat="server" Font-Size="11px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "poinhouseqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="75px" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View ID="PriceVariance" runat="server">
                            <div class="row justify-content-end">
                                <asp:HyperLink runat="server" ID="lnkbtnExptExcel" CssClass="btn btn-sm btn-primary text-white" Visible="false">
                                    <i class="fa fa-file-excel pr-1"></i>Download Excel</asp:HyperLink>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="gvVariance" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvVariance_PageIndexChanging" ShowFooter="true">
                                    <PagerSettings Position="Top" />
                                    <RowStyle Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvSlno" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" Width="35px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="gvRsirdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" Width="220px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="gvUnit" runat="server" Font-Size="11px"
                                                    CssClass="text-left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rates">
                                            <ItemTemplate>
                                                <asp:Label ID="gvRates" runat="server" Font-Size="11px"
                                                    CssClass="text-left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rates")).Replace(".", "&nbsp;") %>'></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </asp:View>

                        <asp:View ID="MasterPOReport" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMasterPOR" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False" ShowFooter="true" OnPageIndexChanging="gvMasterPOR_PageIndexChanging" Width="1920px">
                                    <PagerSettings Position="Top" />
                                    <RowStyle Font-Size="11px" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblSlNo4" runat="server" Height="16px"
                                                    Style="text-align: right" Width="25px"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkbtnEye" runat="server" CssClass="btn  btn-xs" Target="_blank"
                                                        NavigateUrl='<%# "~/F_09_Commer/RptPoShipmentLog.aspx?" +
                                                            "orderNo="+DataBinder.Eval(Container.DataItem, "orderno").ToString() +
                                                            "&supCod="+DataBinder.Eval(Container.DataItem, "ssircode").ToString() %>'
                                                        ToolTip="View PO Shipment Log"><i class="fa fa-eye" style="color:orangered" aria-hidden="true"></i>
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchPo" Width="100px" BackColor="Transparent" BorderStyle="None" runat="server" placeholder="PO No" onkeyup="Search_Gridview(this,1,'gvMasterPOR')" CssClass="text-center"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypgvLblPo" runat="server" Width="100px" Target="_blank" NavigateUrl='<%# ResolveUrl( "~/F_09_Commer/RptPurchaseStatus?Type=Purchase&Rpt=Purchasetrk&genno="+ Convert.ToString(DataBinder.Eval(Container.DataItem, "pono"))) %>'
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPoDate" runat="server" Width="75px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") == "01-Jan-1900"? "" 
                                                                : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "orderdat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Store Name">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblStrName" runat="server" Width="150px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "storename")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C No">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblLc" runat="server" Width="120px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcname")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblLcDt" runat="server" Width="75px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lcdate")).ToString("dd-MMM-yyyy") == "01-Jan-1900"? "" 
                                                                : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lcdate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C Value">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblLcVal" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcvalue")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlLcVal" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C Payment Temrs">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPmntrms" runat="server" Width="65px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lcpaymentrms")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblSup" runat="server" Width="150px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipper">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblShip" runat="server" Width="150px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shiperdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPoQty" runat="server" Width="60px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlpoqty" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO Value $">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPoVal" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlpoVal" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Exp.Del. Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblExpDel" runat="server" Width="75px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") == "01-Jan-1900"? "" 
                                                                : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdeldat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="PI No">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPo" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="PI Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPo" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="PI value">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPo" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Invoice<br/>(Latest)">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblinv" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lastchln")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Invoice Date<br/>(Latest)">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblinvDt" runat="server" Width="75px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastchlndate")).ToString("dd-MMM-yyyy") == "01-Jan-1900"? "" 
                                                                : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lastchlndate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Invoice Qty<br/>(Cumulative)">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblinvQty" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlshipqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlinvqty" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="B/L, AWB/D/N No">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPo" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="B/L, AWB/D/N Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPo" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="ETA Date">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPo" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <%--<asp:TemplateField HeaderText="Received at Factory">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPo" runat="server" Width="170px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="L/C Payment Date<br/>(Latest)">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblLcPayDt" runat="server" Width="75px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lclastpay")).ToString("dd-MMM-yyyy") == "01-Jan-1900"? "" 
                                                                : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lclastpay")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C Payment Amount<br/>(Cumulative)">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblLcPayAmt" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lcpayamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlLcamt" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="L/C Link<br/>Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLbllnkQty" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lclinkqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlLclink" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Receive<br/>Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblRcvQty" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlrcvQty" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="QC Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblqcQty" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlqcqty" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Costing<br/>Balance">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblcstBal" runat="server" Width="70px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costbalqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="ttlcstbal" Font-Bold="True" Font-Size="10px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Payment Status">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblPS" runat="server" Width="90px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paymntstatus")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delivery Terms">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblDT" runat="server" Width="100px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deltrms")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="gvLblRmks" runat="server" Width="100px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">          
        function ExecuteGraph(seasonsum) {
            var seasonsum = JSON.parse(seasonsum);

            //console.log(monthlysum);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });

            $('#seasonSum').highcharts({
                chart: {
                    styledMode: true
                },

                title: {
                    text: 'Season Wise Amount'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    allowPointSelect: true,
                    keys: ['name', 'y', 'selected', 'sliced'],
                    data: (function () {
                        // generate an array of random data
                        var data = [],
                            time = (new Date()).getTime(),
                            i;

                        for (var key in seasonsum) {
                            if (seasonsum.hasOwnProperty(key)) {
                                data.push([seasonsum[key].seasondesc,
                                seasonsum[key].ordamt, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true
                }]
            });
        }

        function ExecutePrint(pType) {
            window.open('../RDLCViewerWin.aspx?PrintOpt=' + pType, '_blank');
        }

    </script>
</asp:Content>




