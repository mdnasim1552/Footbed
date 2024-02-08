<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptProduction.aspx.cs" Inherits="SPEWEB.F_15_Pro.RptProduction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../Scripts/highchartwithmap.js"></script>
    <script src="../Scripts/highchartexporting.js"></script>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('toggle');
        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

        function Search_Gridview(strKey, cellNr, gvName) {
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvOrderDefect":
                    tblData = document.getElementById("<%=gvOrderDefect.ClientID %>");
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


        function DefParGraph(rejection) {
            var rejection = JSON.parse(rejection);
            console.log(rejection);
            Highcharts.setOptions({
                lang: {
                    decimalPoint: '.',
                    thousandsSep: ''
                }
            });

            $('#ParetoChart').highcharts({
                chart: {
                    type: 'column'
                },
                title: {
                    text: '',
                },
                xAxis: {
                    type: 'category',
                    crosshair: true
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Defect Cumulative %'
                    }
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0,
                    }
                },

                series: [{
                    allowPointSelect: true,
                    name: "Pareto Chart",
                    colorByPoint: true,
                    data: (function () {
                        var data = [];
                        for (var key in rejection) {
                            if (rejection.hasOwnProperty(key)) {
                                data.push([rejection[key].defectname,
                                rejection[key].qty, false
                                ]);
                            }
                        }
                        return data;
                    }()),
                    showInLegend: true

                }],
            });
        }

    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1" runat="server" id="FieldDate">
                            <div class="form-group">
                                <asp:Label ID="lbldate" Text="Date" runat="server" />
                                <asp:Label ID="lblFromDate" Text="From" runat="server" Visible="false" />
                                <asp:TextBox ID="txtDate" runat="server" class="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="FieldtoDate" visible="false">
                            <div class="form-group">
                                <asp:Label Text="To" runat="server" />
                                <asp:TextBox ID="txttoDate" runat="server" class="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate1" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1" runat="server" id="FieldSeason">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="label" for="ToDate">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2" runat="server" id="FieldMasterLC">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Master LC</asp:Label>
                                <asp:DropDownList ID="ddlmlccod" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlmlccod_SelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3" runat="server" id="FieldStyle">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Style"></asp:Label>
                                <asp:DropDownList ID="ddlStyle" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="divProcess">
                            <asp:Label ID="Label14" runat="server" CssClass="label">Process</asp:Label>
                            <asp:DropDownList ID="ddlFromProcess" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1" runat="server" id="pagesize" visible="false">
                            <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Text="Page Size"></asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>

                        
                        <div class="col-md-2 col-sm-2 col-lg-2" runat="server" id="RbPanel1" visible="false" style="margin-top: 8px; margin-bottom: 8px">
                            <asp:RadioButtonList ID="rbtnList1" runat="server" CssClass="rbtnList1 form-control" RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Value="REJ" Selected="True">Rejected</asp:ListItem>
                                <asp:ListItem Value="REP" style="margin-left: 10px">Repaired</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        
                        <div class="col-md-2" id="OrderDeatilslink" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:HyperLink ID="LbtnOrderDetails" runat="server" Target="_blank" CssClass="btn btn-sm btn-warning text-white"><span class="fa fa-file-invoice"></span> Order Details</asp:HyperLink>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px">
                    <asp:MultiView runat="server" ID="MVRptProduction">

                        <asp:View runat="server" ID="ViewBalSheet">

                            <asp:GridView ID="gvBalSheet" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>

                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtnPush" runat="server" Font-Size="12px" Width="80px"
                                                CssClass="" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "qcdate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtnPush" runat="server" Font-Size="12px" Width="100px"
                                                CssClass="" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dtype")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-01">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF1" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-02">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF2" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-03">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF3" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-04">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF4" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-05">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF5" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF6" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF7" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF8" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b8")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />

                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF9" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b9")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF10" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b10")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF11" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b11")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-12" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF12" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b12")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-13" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF13" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b13")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-14" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF14" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b14")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-15" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF15" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b15")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF16" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF17" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-18" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF18" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b18")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-19" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF19" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b19")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-20" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF20" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b20")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>


                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF21" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF22" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-23" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF23" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b23")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-24" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF24" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b24")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-25" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF25" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b25")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-26" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF26" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b26")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-27" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF27" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b27")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-28" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF28" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b28")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-29" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF29" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b29")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-30" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF30" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b30")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-31" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF31" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b31")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-32" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF32" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b32")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-33" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF33" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b33")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-34" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF34" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b34")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-35" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF35" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b35")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-36" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF36" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b36")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-37" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF37" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b37")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-38" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF38" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b38")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-39" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF39" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b39")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-40" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PtxtgvF40" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b40")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="TxtgvTotal1" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rework">
                                        <ItemTemplate>
                                            <asp:Label ID="gvbsTxtRework" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rejection">
                                        <ItemTemplate>
                                            <asp:Label ID="gvbsTxtRepair" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejqty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                        </asp:View>

                        <asp:View runat="server" ID="ViewSizeBalSheet">
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:GridView ID="gvSizeBalSheet" runat="server" AutoGenerateColumns="False" PageSize="15"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Process">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProcess" runat="server" Font-Size="12px"
                                                        CssClass="" Text='<%# DataBinder.Eval(Container.DataItem, "fprostepdesc").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSize" runat="server" Font-Size="12px"
                                                        CssClass="" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dtype")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-01">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF1s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b1")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-02">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF2s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b2")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-03">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF3s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b3")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-04">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF4s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b4")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-05">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF5s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b5")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF6s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b6")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-07" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF7s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b7")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-08" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF8s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b8")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />

                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-09" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF9s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b9")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-10" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF10s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b10")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-11" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF11s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b11")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>


                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-12" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF12s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b12")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>


                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-13" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF13s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b13")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>


                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-14" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF14s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b14")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>


                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-15" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF15s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b15")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-16" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF16s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b16")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-17" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF17s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b17")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-18" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF18s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b18")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-19" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF19s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b19")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>


                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-20" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF20s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b20")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-21" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF21s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b21")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-22" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF22s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b22")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-23" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF23s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b23")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-24" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF24s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b24")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-25" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF25s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b25")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-26" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF26s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b26")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-27" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF27s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b27")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-28" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF28s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b28")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-29" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF29s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b29")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size-30" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF30s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b30")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-31" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF31s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b31")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-32" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF32s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b32")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-33" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF33s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b33")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-34" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF34s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b34")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-35" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF35s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b35")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-36" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF36s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b36")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-37" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF37s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b37")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-38" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF38s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b38")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-39" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF39s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b39")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-40" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="PtxtgvF40s" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "b40")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="TxtgvTotal1s" runat="server"
                                                        Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ")%>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-md-4">

                                    <section class="card card-figure">
                                        <!-- .card-figure -->
                                        <figure class="figure">
                                            <!-- .figure-img -->
                                            <div class="figure-img">
                                                <asp:Image ID="SmpleIMg" runat="server" CssClass="img-fluid" />

                                                <%--<img class="img-fluid" src="assets/images/dummy/img-5.jpg" alt="Card image cap">--%>
                                                <%-- <div class="figure-action">

                                                        <a data-toggle="modal" class="btn btn-sm btn-success" href="#myModal">Click for Replace Image</a>
                                                    </div>--%>
                                            </div>
                                            <!-- /.figure-img -->
                                            <!-- .figure-caption -->
                                            <figcaption class="figure-caption">
                                                <h6 class="figure-title">
                                                    <span class="fa fa-image"></span><a target="_blank" href="#">Article Image</a>
                                                </h6>
                                                <p class="text-muted mb-0">Note: You can change this image </p>
                                            </figcaption>
                                            <!-- /.figure-caption -->
                                        </figure>
                                        <!-- /.card-figure -->
                                    </section>

                                    <asp:Table ID="tbl" runat="server" BorderStyle="Solid" CssClass="table table-bordered grvContentarea">
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="bg-instagram"><span class="fa fa-hand-point-right"></span> Buyer</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="BuyerName" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="bg-twitter"><span class="fa fa-hand-point-right"></span> BRAND</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblbrand" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell><span class="fa fa-hand-point-right"></span> Color</asp:TableCell>
                                            <asp:TableCell CssClass="bg-instagram">
                                                <asp:Label ID="lblcolor" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell><span class="fa fa-hand-point-right"></span> Article</asp:TableCell>
                                            <asp:TableCell CssClass="bg-twitter">
                                                <asp:Label ID="lblarticle" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell CssClass="bg-twitter"><span class="fa fa-hand-point-right"></span> Size Range</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="lblsizernge" runat="server"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell CssClass="bg-instagram"><span class="fa fa-hand-point-right"></span> Total Order</asp:TableCell>
                                            <asp:TableCell>
                                                <asp:Label ID="TotalOrder" runat="server"></asp:Label>
                                            </asp:TableCell>

                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell><span class="fa fa-hand-point-right"></span> Order Currency</asp:TableCell>
                                            <asp:TableCell CssClass="bg-twitter">
                                                <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                                <asp:Label ID="lblCurcode" Visible="false" runat="server"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell><span class="fa fa-hand-point-right"></span> Order No</asp:TableCell>
                                            <asp:TableCell CssClass="bg-instagram">
                                                <asp:Label ID="lblOrderNo" runat="server"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>
                            </div>
                        </asp:View>

                        <asp:View runat="server" ID="ViewQltyNdProd">
                            <asp:GridView ID="gvQltyNdProd" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>

                                    <asp:TemplateField HeaderText="M.P">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblMp" runat="server" Font-Size="12px" Width="60px"
                                                CssClass="" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "manpower")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="gvLblTtlMpQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                        </FooterTemplate>
                                        <%--<FooterStyle HorizontalAlign="Right" />--%>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="M.C">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblMc" runat="server" Font-Size="12px" Width="60px"
                                                CssClass="" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machine")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Hour">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblHour" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whours")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Buyer">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblBuyer" runat="server"
                                                Style="font-size: 11px; text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblOrder" runat="server" Font-Size="12px" Width="60px"
                                                CssClass="" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Article">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblArticle" runat="server" Font-Size="12px" Width="100px"
                                                CssClass="" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Color">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblColor" runat="server" Font-Size="12px" Width="100px"
                                                CssClass="" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order Qty.">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblOrderQty" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QC Pass">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblQcPass" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="gvLblTtlQcQty" Width="100px" CssClass="text-right font-weight-bold"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ord. B/L">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblQcPass" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrbal")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rework">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblRework" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rejection">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblRepair" runat="server"
                                                Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectionqty")).ToString("#,##0;(#,##0); ")%>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cause Of Rejection">
                                        <ItemTemplate>
                                            <asp:Label ID="gvqnpLblRejCause" runat="server"
                                                Style="font-size: 11px; text-align: left"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "causeofrej")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>


                        </asp:View>

                        <asp:View ID="ViewProductionReport" runat="server">

                            <asp:GridView ID="gvProductionReport" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprSection" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "section") %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Manpower">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprManpower" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "manpower")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Machine">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprMachine" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "macqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Working<br>Hour">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprMachine" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "wrkhours")).ToString("#,##0;(#,##0); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Production">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprProduction" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Rejection<br>(PRS)">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprRejection" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectionqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Rework">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprRework" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Productivity<br>/Hour">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprProductivity" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "perhourprod")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Productivity<br>/machine/day">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprProdMachine" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permanprod")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rework %">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprReworkPer" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rewrkprcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rejection %">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprRejectionPer" runat="server"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejprcnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Major Problems">
                                        <ItemTemplate>
                                            <asp:Label ID="gvprRejectionPer" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "majorproblem") %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:View>

                        <asp:View ID="ViewDefectPareto" runat="server">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:GridView ID="gvDefPareto" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"  AllowSorting="true"
                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Defect Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDefectnm" runat="server" Width="190px"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "defectname") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Defect Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDefectQty" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" ID="gvLblTtlDef" Font-Bold="True" Style="text-align: Center"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cumulative<br/>Qty ⥮">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCumQty" runat="server" Width="80px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cumqty")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Defect<br/>Cumulative %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDefCumPrcnt" runat="server" Width="100px"
                                                        Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cumpercnt")).ToString("#,##0.00;(#,##0.00); "))+"  %" %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
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

                                <div class="col-md-7" style="border: 1px solid #D8D8D8;">
                                    <div id="ParetoChart" style="width: 830px; height: 550px; margin: 0 auto"></div>
                                </div>

                            </div>


                        </asp:View>

                        <asp:View ID="ViewOrderDefect" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvOrderDefect" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcOrdNo" BackColor="Transparent" Width="120px" BorderStyle="None" CssClass="text-center" runat="server" placeholder="Order No" onkeyup="Search_Gridview(this, 1, 'gvOrderDefect')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrdno" runat="server" Width="120px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "orderno") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcArtNo" BackColor="Transparent" Width="200px" BorderStyle="None" CssClass="text-center" runat="server" placeholder="Article No" onkeyup="Search_Gridview(this, 2, 'gvOrderDefect')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblArtcno" runat="server" Width="200px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "styledesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Process">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcProc" BackColor="Transparent" Width="110px" BorderStyle="None" CssClass="text-center" runat="server" placeholder="Process" onkeyup="Search_Gridview(this, 3, 'gvOrderDefect')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblprocess" runat="server" Width="110px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "processdesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Line No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLinno" runat="server" Width="100px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "linedesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcOmpTp" BackColor="Transparent" Width="100px" BorderStyle="None" CssClass="text-center" runat="server" placeholder="Component Type" onkeyup="Search_Gridview(this, 5, 'gvOrderDefect')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblComptp" runat="server" Width="100px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "compdesc") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Defect Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDefecNm" runat="server" Width="110px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "defectname") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSizee" runat="server" Width="60px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyy" runat="server" Width="50px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0;(#,##0.0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlQty" Width="50px" Font-Bold="True" Style="text-align: Center"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Applied Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppDt" runat="server" Width="75px"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "qcapplieddate")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemks" runat="server" Width="130px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
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

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
