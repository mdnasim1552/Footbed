<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptProcessBasePlan.aspx.cs" Inherits="SPEWEB.F_05_ProShip.RptProcessBasePlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function SynchButton_Click() {
            tblData = document.getElementById("<%=gvsizes.ClientID %>");


            var rowData;
            var rowData1;
            for (var i = 1; i < tblData.rows[0].cells.length; i++) {
                //rowData1 = tblData.rows[1].cells[i].innerHTML;

                var fotdata = document.getElementById("ContentPlaceHolder1_gvsizes_GvS" + i).innerHTML;
                $("#ContentPlaceHolder1_gvsizes_txtgvF" + i + "_0").val(fotdata);

                rowData = tblData.rows[2].cells[i].childNodes[0].innerHTML;
                //  var styleDisplay = 'none';

                //for (var j = 0; j < strData.length; j++) {
                //    if (rowData.toLowerCase().indexOf(strData[j]) >= 0) {
                //        styleDisplay = '';
                //    }
                //    else {
                //        styleDisplay = 'none';
                //        break;
                //    }
                //}
                //console.log(rowData1);
                console.log(fotdata);
                // tblData.rows[i].style.display = styleDisplay;
            }
        }
        function OpenModal() {
            $('#SizeModal').modal('show');
        }
        function CLoseMOdal() {
            $('#SizeModal').modal('hide');
        }

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

        /*Search function start*/
        function Search_Gridview(strKey, cellNr, gvName) {

            var tblData;
            console.log(gvName);

            var strData = strKey.value.toLowerCase().split(" ");
            console.log(strData);
            switch (gvName.toLowerCase()) {
                case "gvplan":
                    tblData = document.getElementById("<%=gvPlan.ClientID %>");
                    break;
            }


            var rowData;
            for (var i = 2; i < tblData.rows.length; i++) {
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
        /*Search function end*/
    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .modal-dialog:not(.modal-dialog-centered) {
            margin-top: 0;
            width: 1000px;
        }

        .progress-bar {
            background-color: #f73535;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid mb-1">
                <div class="card-body">

                    <div class="row">

                        <div runat="server" id="FieldSeason" class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblSeason" runat="server" CssClass="label">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LblProcess" runat="server" CssClass="label">Process</asp:Label>
                                <asp:DropDownList ID="DdlProcess" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="DdlProcess_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div runat="server" id="FieldDate" class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="LblDate" runat="server" CssClass="label">Date</asp:Label>

                                <asp:TextBox ID="txtDatefrom" Style="width: 45%" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>

                            </div>
                        </div>


                        <div class="col-md-1" style="margin-left: -100px">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LblLines" runat="server" CssClass="label">Line</asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlLines" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lnkbtnSearch" class="input-group-text text-success" OnClick="lnkbtnSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkbtnCross" class="input-group-text text-danger" OnClick="lnkbtnCross_Click" Visible="false"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div runat="server" id="FieldCalendar" class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label small">Year Calendar</asp:Label>
                                <div>
                                    <a href="#" class="btn btn-sm btn-success" data-toggle="modal" data-target="#exampleModalDrawerRight">
                                        <i class="fa fa-calendar-check mr-2"></i>Calendar
                                    </a>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px">
                    <asp:MultiView runat="server" ID="MVProcBasePlan">

                        <asp:View runat="server" ID="ViewDayWise">

                            <div class="row">

                                <div class="table-responsive">
                                    <asp:GridView ID="gvPlan" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                        ShowFooter="True" OnRowCreated="gvPlan_RowCreated" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Style ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                        Width="51px"></asp:Label>
                                                    <asp:Label ID="lblSlnum" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                        Width="51px"></asp:Label>
                                                    <asp:Label ID="lblDayid" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid")) %>'
                                                        Width="51px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvColorID" Visible="false" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                        Width="51px"></asp:Label>
                                                    <asp:Label ID="lblmlccod" Visible="false" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                        Width="51px"></asp:Label>
                                                    <asp:Label ID="lblGvLinedsc" runat="server"
                                                        Style="text-transform: capitalize; font-size: 10px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc"))%>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" ">
                                                <HeaderTemplate>
                                                    <asp:TextBox runat="server" BackColor="Transparent" BorderStyle="None" Width="110px" placeholder="Article Name" onkeyup="Search_Gridview(this, 1, 'gvPlan')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStyleDesc0" runat="server" Font-Size="9px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <HeaderTemplate>
                                                    <asp:TextBox runat="server" BackColor="Transparent" BorderStyle="None" Width="110px" placeholder="Order No" onkeyup="Search_Gridview(this, 2, 'gvPlan')"></asp:TextBox><br />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOrderNo" Font-Size="9px" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnSizes" Width="60" OnClick="LbtnSizes_Click" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblOrdQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("###0;(###0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblOrdQtyF" Style="text-align: right !important;" CssClass="text-dark text-right font-weight-bold" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblTPlanQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("###0;(###0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="LblTPlanQtyF" Style="text-align: right !important;" CssClass="text-dark text-right font-weight-bold" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Plan Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblPlanQty" runat="server" BackColor="Transparent"  Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("###0;(###0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="LblPlanQtyF" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                            <%-- <asp:TemplateField HeaderText="Production Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblProdQty" runat="server" BackColor="Transparent" 
                                                         Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prdqty")).ToString("###0;(###0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="LblProdQtyF" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>
                                            <%-- <asp:TemplateField HeaderText="Balance Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblBalQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("###0;(###0); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="LblBalQtyF" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Start Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStartDate" runat="server" Font-Size="10px" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "strdate")).ToString("dd-MMM") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="W.<br>Hour">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblHours" CssClass="bg-teal text-white" BorderStyle="Solid" BorderWidth="1" BorderColor="LightSeaGreen" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whours")).ToString("###0;(###0); ") %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="End Date">
                                                <HeaderTemplate>
                                                    <asp:Label runat="server" ID="lblhtEndDate" Text="End Date"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEndate" runat="server" Font-Size="10px" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "endate")).ToString("dd-MMM") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-01">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF1" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d1")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay1" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="false" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-02">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF2" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d2")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay2" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-03">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF3" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d3")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay3" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-04">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF4" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d4")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay4" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-05">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF5" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d5")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay5" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-06">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF6" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d6")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay6" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-07">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF7" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d7")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay7" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-08">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF8" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d8")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay8" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-09">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF9" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d9")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay9" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-10">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF10" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d10")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay10" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-11">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF11" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d11")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay11" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-12">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF12" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d12")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay12" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-13">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF13" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d13")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay13" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-14">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF14" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d14")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay14" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day-15">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF15" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d15")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay15" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day-16">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF16" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d16")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay16" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day-17">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF17" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d17")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay17" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day-18">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF18" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d18")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay18" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day-19">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF19" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d19")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay19" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day-20">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblgvF20" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d20")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="GvDay20" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
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




                            <div id="SizeModal" class="modal animated slideInLeft" role="dialog">
                                <div class="modal-dialog UpdateMOdel modal-lg">
                                    <div class="modal-content ">
                                        <div class="modal-header">
                                            <h4 class="modal-title"><span class="fa fa-table"></span>
                                                <asp:Label ID="ModalHead" runat="server"></asp:Label>
                                                <asp:Label ID="LblCodes" Style="display: none;" runat="server"></asp:Label>
                                                <asp:Label ID="lblTodDate" Style="display: none;" runat="server"></asp:Label>

                                            </h4>
                                        </div>
                                        <div class="modal-body form-horizontal table-responsive">

                                            <header class="card-header">
                                                <ul class="nav nav-tabs card-header-tabs" id="tabContent">
                                                    <li class="nav-item d-none"><a class="nav-link" href="#details" data-toggle="tab">Size Brakdown</a></li>
                                                    <li class="nav-item active"><a class="nav-link" href="#networking" data-toggle="tab">Planning History</a></li>
                                                    <li class="nav-item"><a class="nav-link" href="#access-security" data-toggle="tab">Order Details</a></li>

                                                </ul>
                                            </header>


                                            <div class="tab-content">
                                                <div class="tab-pane d-none" id="details">

                                                    <asp:GridView ID="gvsizes" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Style ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                    <asp:Label ID="mlblgvSlnum" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                                        Width="30px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Color ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sizes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgv" runat="server" Style="text-transform: capitalize" Text='Plan Qty'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblBalance" runat="server" Text="Balance"></asp:Label>
                                                                    <%--<a href="javascript:void()" onclick="SynchButton_Click();"  style="width:80px;" class="btn btn-sm btn-danger"><span class="fa fa-sync-alt"></span> Balance</a>--%>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="TOD Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSipmentdate" Font-Bold="true" runat="server" Style="font-size: 11px;"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shimentdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="100px"></asp:Label>

                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="LblBalnce" runat="server" Style="text-align: right" Text='Plan Balance'></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Blue" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Unit" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-01" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS1" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-02" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS2" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-03" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS3" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-04" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS4" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-05" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS5" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS6" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-07" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS7" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-08" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS8" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-09" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS9" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-10" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS10" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-11" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS11" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-12" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS12" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-13" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS13" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-14" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS14" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-15" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS15" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Size-16" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS16" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-17" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS17" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-18" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS18" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-19" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS19" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size-20" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="GvS20" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-21" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-22" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-23" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-24" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-25" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-26" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-27" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-28" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-29" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size-30" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-31" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-32" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-33" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-34" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-35" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-36" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-37" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-38" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-39" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-40" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>





                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="40px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Trial Order QTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColTotal1" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colqty")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="FLblgvColTotal" Font-Bold="true" runat="server"></asp:Label>
                                                                </FooterTemplate>
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
                                                <div class="tab-pane active" id="networking">
                                                    <asp:GridView ID="gvPlanSummary" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Start Date ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Pllblgvstartdate" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="End Date ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PllblgvEnddate" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="70px"></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CODE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PllblgvSl" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Lot No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PllblgvLotDesc" Font-Bold="true" runat="server" Style="font-size: 11px;"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotdesc")) %>'
                                                                        Width="100px"></asp:Label>

                                                                </ItemTemplate>

                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Size-01" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF1" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS1" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-02" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF2" runat="server" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS2" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-03" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF3" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS3" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-04" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF4" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS4" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-05" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF5" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS5" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF6" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS6" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-07" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF7" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS7" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-08" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF8" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS8" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-09" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF9" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS9" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-10" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF10" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS10" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-11" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF11" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS11" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-12" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF12" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS12" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-13" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF13" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS13" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-14" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF14" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS14" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-15" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF15" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS15" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Size-16" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF16" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS16" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-17" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF17" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS17" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-18" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF18" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS18" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-19" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF19" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS19" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size-20" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF20" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS20" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-21" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS21" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-22" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="PlanGvS22" runat="server" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-23" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-24" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-25" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-26" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-27" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-28" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-29" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size-30" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-31" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-32" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-33" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="PltxtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-34" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-35" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-36" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-37" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-38" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-39" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-40" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PltxtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>





                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PllblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="40px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                                                </FooterTemplate>
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

                                                <div class="tab-pane" id="access-security">
                                                    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Style ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Color ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category and <br> Article Number">

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleDesc0" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "description")) %>'
                                                                        Width="160px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Justify" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Color">
                                                                <%-- <FooterTemplate>
                                                                            <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                                OnClick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                                        </FooterTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ColorDesc")) %>'
                                                                        Width="91px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-01">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-02">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-03">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-04">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-05">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-07" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-08" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-09" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-10" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-11" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-12" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-13" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-14" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-15" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>



                                                            <asp:TemplateField HeaderText="Size-16" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-17" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-18" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-19" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size-20" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-21" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-22" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-23" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-24" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-25" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-26" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-27" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-28" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-29" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size-30" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-31" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-32" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-33" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-34" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-35" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-36" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-37" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-38" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-39" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-40" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                                        BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>







                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="40px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                                                </FooterTemplate>
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



                                            </div>






                                        </div>
                                        <div class="modal-footer ">

                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal modal-drawer fade has-shown" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
                                <!-- .modal-dialog -->
                                <div class="modal-dialog modal-drawer-right" role="document" style="max-width: 1000px !important;">
                                    <!-- .modal-content -->
                                    <div class="modal-content">
                                        <!-- .modal-header -->
                                        <div class="modal-header modal-body-scrolled">
                                            <h5 id="exampleModalDrawerRightLabel" class="modal-title">Full Year Calender Details</h5>
                                        </div>
                                        <!-- /.modal-header -->
                                        <!-- .modal-body -->
                                        <div class="modal-body">
                                            <embed type="text/html" src="yearcalendar.html" style="width: 100%; height: 100%; overflow: hidden !important; border: none;">
                                        </div>
                                        <!-- /.modal-body -->
                                        <!-- .modal-footer -->
                                        <div class="modal-footer modal-body-scrolled">
                                            <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                                        </div>
                                        <!-- /.modal-footer -->
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>

                        </asp:View>

                        <asp:View runat="server" ID="ViewArtclLrnCurv">
                            <div class="">
                                <asp:HyperLink runat="server" ID="lnkbtnExcl" CssClass="btn btn-sm btn-success">
                                    <i class="fa fa-file-excel mr-1"></i> Download Excel
                                </asp:HyperLink>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvArtclLrnCurv" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                    ShowFooter="True" Width="" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                    <Columns>
                                        <asp:TemplateField HeaderText="BOM No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcBomNo" runat="server" Width="51px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomno")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                            <HeaderStyle Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcPO" runat="server" Width="51px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" />
                                            <HeaderStyle Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Article Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvStyleDesc0" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcBuyer" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyerdesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Upper Leather">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcUprLthr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "upperleather")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcOrdQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%--<asp:Label ID="LblOrdQtyF" Style="text-align: right !important;" CssClass="text-dark text-right font-weight-bold" runat="server"></asp:Label>--%>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Plan Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcTtlQty" Width="70" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%--<asp:Label ID="LblOrdQtyF" Style="text-align: right !important;" CssClass="text-dark text-right font-weight-bold" runat="server"></asp:Label>--%>
                                            </FooterTemplate>
                                            <ItemStyle Width="70px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Man Power">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcManPowr" CssClass="text-right" Width="60px" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "manpower")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="W. Hours">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcHours" CssClass="text-right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whours")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Upper Pcs/pr">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvalcUperPcsPr" CssClass="text-right text-white" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whours")).ToString("#,##0;(#,##0); ") %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Up. Cut SMV">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvalcsMV" CssClass="text-right text-white" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whours")).ToString("#,##0;(#,##0); ") %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Average PPH">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcAvgpPH" Width="70" runat="server" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "agvpph")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Day-1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF1" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d1")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD1" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF2" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d2")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD2" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-3">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcD3" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d3")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD3" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-4">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcD4" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d4")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD4" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-5">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF5" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d5")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD5" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-6">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcD6" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d6")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD6" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-7">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcD7" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d7")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD7" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-8">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcD8" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d8")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD8" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-9">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF9" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d9")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD9" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF10" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d10")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD10" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Day-11">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF11" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d11")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD11" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-12">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF12" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d12")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD12" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-13">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF13" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d13")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD13" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-14">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF14" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d14")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtr14" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-15">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF15" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d15")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtr15" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-16">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF16" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d16")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtr16" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-17">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF17" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d17")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtrD17" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-18">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF18" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d18")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtr18" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-19">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF19" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d19")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtr19" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center"  />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Day-20">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvalcF20" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "d20")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvalcFtr20" runat="server" CssClass="text-dark text-right  font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right"  />
                                            <HeaderStyle HorizontalAlign="Center"  />
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

