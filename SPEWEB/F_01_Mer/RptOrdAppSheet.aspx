<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptOrdAppSheet.aspx.cs" Inherits="SPEWEB.F_01_Mer.RptOrdAppSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../Scripts/highchartwithmap.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function OpenModal(bomSummary) {
            //if (stockqty) {
            //    BindCurrentStockQty(stockqty);
            //}
            var bomSummary = JSON.parse(bomSummary)
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ' '
                }
            });

            $('#SummaryBar').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: ''
                },
                subtitle: {
                    text: 'BOM vs Ex-Factory',
                    style: {
                        color: '#44994a',
                        fontWeight: 'bold'
                    }
                },
                xAxis: {
                    categories: [
                        'Jan',
                        'Feb',
                        'Mar',
                        'Apr',
                        'May',
                        'Jun',
                        'Jul',
                        'Aug',
                        'Sep',
                        'Oct',
                        'Nov',
                        'Dec'
                    ],
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Quantity'
                    }
                },


                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0"><b>{point.y:0f}</b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true,


                },
                plotOptions: {
                    column: {
                        pointPadding: 0.1,
                        borderWidth: 0


                    }
                },
                series: [{
                    name: 'Placed BOM Qty',
                    data: [bomSummary[0]['ordqty'], bomSummary[1]['ordqty'], bomSummary[2]['ordqty'], bomSummary[3]['ordqty'], bomSummary[4]['ordqty'], bomSummary[5]['ordqty'], bomSummary[6]['ordqty'], bomSummary[7]['ordqty'], bomSummary[8]['ordqty'], bomSummary[9]['ordqty'], bomSummary[10]['ordqty'], bomSummary[11]['ordqty']],
                    color: '#4286f4'

                }, {

                    name: 'Ex-Factory Qty',
                    //color:red,
                    data: [bomSummary[0]['expqty'], bomSummary[1]['expqty'], bomSummary[2]['expqty'], bomSummary[3]['expqty'], bomSummary[4]['expqty'], bomSummary[5]['expqty'], bomSummary[6]['expqty'], bomSummary[7]['expqty'], bomSummary[8]['expqty'], bomSummary[9]['expqty'], bomSummary[10]['expqty'], bomSummary[11]['expqty']],
                    color: '#db6c1d'
                }]
            });

            $('#ModalBomSummery').modal('show');
        }

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }
        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {

                case "gvBomAppList":
                    tblData = document.getElementById("<%=gvBomAppList.ClientID %>");
                    break;

                case "gvPendBOM":
                    tblData = document.getElementById("<%=gvPendBOM.ClientID %>");
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
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
                    
        }

        .allmaterial .modal-dialog {
            max-width: 80% !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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


                        <div class="col-md-1 col-sm-1 col-lg-1 " runat="server" id="plnDateF">
                            <div class="form-group">
                                <asp:Label ID="lblDate1" runat="server" CssClass="label">From</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdateto_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" Text="Season" CssClass="control-label"></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblbuyer" runat="server" CssClass="label">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnSerOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="log-divider" id="lblHeadCost" runat="server" visible="False">
                            <span>
                                <i class="fa fa-fw fa-dollar-sign"></i>Cost</span>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnSummery" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSummery_Click">Summary</asp:LinkButton>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="BOM" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvBomAppList" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvBomAppList_RowDataBound">
                                    <RowStyle />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmlccod" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblgvmlcdesc" runat="server" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbomdate" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomdate")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ex-Factory <br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvexfactorydate" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "etd"))).Year.ToString() == "1900" ? "" : (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "etd"))).ToString("dd-MMM-yyyy") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lead Time">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvleadtime" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "leadtime")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--5--%>

                                        <asp:TemplateField HeaderText="Buyer">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Buyer" onkeyup="Search_Gridview(this,5, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBuyername" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvSdupplier" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='Total'></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Style">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchStyle" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,6, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblstyleid" runat="server" BackColor="Transparent" Visible="false"
                                                    BorderStyle="None" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")).ToString() %>'></asp:Label>

                                                <asp:Label ID="lgvgvstyledesc" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")).ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcolorid" runat="server" BackColor="Transparent" Visible="false"
                                                    BorderStyle="None" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")).ToString() %>'></asp:Label>

                                                <asp:Label ID="lgvgvcolordesc" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")).ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchEdArticle" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,8, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--9--%>

                                        <asp:TemplateField HeaderText="Article">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchArticle" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Article" onkeyup="Search_Gridview(this,9, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IMAGE">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchOrder" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Order No" onkeyup="Search_Gridview(this,11, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdrno" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--12--%>

                                        <asp:TemplateField HeaderText="Catagory">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "catedesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM ID">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrchBom" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="BOM ID" onkeyup="Search_Gridview(this, 13, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbomid" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFOrdqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BOM">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypbtnMatReq" Target="_blank" ToolTip="Material Requirement" runat="server"><span class="fa fa-list"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Print</br>(Import)">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyFOrderPrint" ToolTip="Import BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <%--17--%>

                                        <asp:TemplateField HeaderText="Print</br>(Local)">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyLOrderPrint" ToolTip="Local BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Plan Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPlanqqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proplanqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFPlanqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bal Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBalqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balplnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Plan">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HypExpPlan" Target="_blank" ToolTip="Export Plan" runat="server"><span class="fa fa-plug"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipping</br>Mark" Visible="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hypShipMark" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" CssClass="btn-sm btn-outline-info"><span class="fa fa-print"></span></asp:HyperLink>
                                                <asp:HyperLink ID="hypShipMarkV2" runat="server" Target="_blank" ForeColor="Red" Font-Underline="false" CssClass="btn-sm btn-outline-success"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>
                                        <%--22--%>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                                <br/>
                            </div>
                        </asp:View>

                        <asp:View ID="CostDiff" runat="server">
                            <div class="row">
                                <asp:GridView ID="GvCostDiff" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BUYER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBuyername" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ORDER NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrderno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ARTICLE NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvArticleName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IMAGE">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                </asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle Width="67" HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="COLOR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColorName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SIZE RANGE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSizeRnge" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ORDER QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFOrdqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="TOTAL <br>PRE <br> COSTING">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalPreCost" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprecost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotalPreCost" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRE <br> COSTING <br>MATERIAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalPreCostmat" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "precostmat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotalPreCostmat" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROFIT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpreprofit" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preprofit")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFpreprofit" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OVERHEAD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpreoverhead" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preoverhead")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFpreoverhead" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MATERIALS%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpreMaterials" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prematprcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFpreMaterials" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="TOTAL <br>POST <br> COSTING">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalPostCost" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlpostcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotalPostCost" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="POST <br> COSTING <br>MATERIAL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalPostCostmat" runat="server"
                                                    Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "postcostmat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFTotalpostCostmat" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROFIT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpostprofit" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "postprofit")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFpostprofit" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OVERHEAD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpostoverhead" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "postoverhead")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFpostoverhead" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MATERIALS%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpostMaterials" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "postmatprcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFpostMaterials" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DIFFERENCE%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdiff" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diff")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
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
                        <asp:View ID="ViewPendingBOM" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvPendBOM" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvPendBOM_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmlccod" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblgvmlcdesc" runat="server" Height="16px" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                    Width="200px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbomdate" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomdate")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buyer">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchBuyer" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Buyer" onkeyup="Search_Gridview(this,3, 'gvPendBOM')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBuyername" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvSdupplier" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='Total'></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchStyle" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Style" onkeyup="Search_Gridview(this,4, 'gvPendBOM')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblstyleid" runat="server" BackColor="Transparent" Visible="false"
                                                    BorderStyle="None" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")).ToString() %>'></asp:Label>
                                                <asp:Label ID="lbldayid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")).ToString() %>'></asp:Label>

                                                <asp:Label ID="lgvgvstyledesc" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")).ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcolorid" runat="server" BackColor="Transparent" Visible="false"
                                                    BorderStyle="None" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")).ToString() %>'></asp:Label>

                                                <asp:Label ID="lgvgvcolordesc" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Style="text-align: left" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")).ToString() %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchEdArticle" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder='<%# this.GetArticle() %>' onkeyup="Search_Gridview(this,6, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvautoartcle" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Article" Visible="false">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchArticle" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Aticle" onkeyup="Search_Gridview(this,7, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IMAGE" Visible="false">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                </asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No" Visible="false">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchOrder" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order No" onkeyup="Search_Gridview(this,9, 'gvBomAppList')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdrno" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Catagory">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcatedesc" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "category")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOrdqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFOrdqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                            </FooterTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BOM ID">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="gvbpTxtSrchBOM" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="BOM ID" onkeyup="Search_Gridview(this, 8, 'gvPendBOM')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbomid" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Print</br>(Import)">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyFOrderPrint" ToolTip="Import BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span></asp:HyperLink>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print</br>(Local)">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyLOrderPrint" ToolTip="Local BOM " runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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

            <div class="modal fade allmaterial bd-example-modal-lg" id="ModalBomSummery" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 id="exampleModalLabelreq" class="modal-title font-weight-light">
                                <span class="fa fa-info-circle mr-2"></span>Yearly Order vs Ex-Factory Qty
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <%----%>
                                    <asp:GridView ID="gvBomSummery" runat="server" AutoGenerateColumns="False" PageSize="15"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNosumm" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQBomID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthnme"))%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="90px" />
                                                <ItemStyle Width="90px" />
                                                <ItemStyle HorizontalAlign="center" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Placeed BOM Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQREqID" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Font-Size="11px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Right" />

                                                <ItemStyle Width="120px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ex-Factory Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvREQReqdate" runat="server" BackColor="Transparent"
                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle Width="120px" />
                                                <ItemStyle Width="120px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Blue" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>

                                <div class="col-md-8" style="border: 1px solid #D8D8D8;">
                                    <div id="SummaryBar" style="width: 700px; height: 500px; margin: 0 auto"></div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

