<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProcessBasePlan.aspx.cs" Inherits="SPEWEB.F_05_ProShip.ProcessBasePlan" %>

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
            $('#copyPlanModal').modal('hide');
        }

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }
        function SelectAllCheckboxes(chk) {
            $('#<%=gvPlan.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false) {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
            });
        }


        function Search_Gridview(strKey, cellNr) {
            //alert(cellNr);
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById("<%=gvPlan.ClientID %>");
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

        $('.MyCustomClass').click(function () {
            alert('The paragraph was clicked.');
        });

    </script>
    <style>
        .ProcessandLotBance label{
            margin-bottom:0px !important;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .modal-dialog:not(.modal-dialog-centered) {
            margin-top: 0;
            width: 1000px;
        }

        .progress-bar {
            background-color: #11d63f;
            color:#000000;
            font-weight:normal;
        }

        .font-size-11 {
            font-size: 11px !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                               
                                <asp:Label ID="LblSeason" runat="server" class="small" for="ToDate">Season</asp:Label>     
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group ProcessandLotBance">
                                <asp:Label ID="Label1" runat="server" CssClass="small">Process</asp:Label>
                                     <asp:CheckBox ID="ChkLotBalance" Checked="true" runat="server" Text="Only Lot Balance" />
                                <asp:DropDownList ID="DdlProcess" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="DdlProcess_SelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="small">From</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblToDate" runat="server" CssClass="small">To</asp:Label>
                                <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                                <a href="#" class="btn btn-sm btn-danger" id="Copybtn" runat="server" visible="false" data-toggle="modal" data-target="#copyPlanModal">
                                    <i class="fa fa-copy"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="small">Buyer Name</asp:Label>
                                <asp:TextBox ID="BuyerName" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="small">Order Qty</asp:Label>
                                <asp:TextBox ID="ordqty" runat="server" CssClass="form-control form-control-sm bg-twitter"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 px-0">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="small">Rem. Plan Qty</asp:Label>
                                <asp:TextBox ID="lblDueTod" runat="server" CssClass="form-control form-control-sm bg-amazon"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="small"> Year</asp:Label>

                                <a href="#" class="btn btn-sm btn-success" data-toggle="modal" data-target="#exampleModalDrawerRight">Calendar  <i class="fa fa-calendar-check"></i>

                                </a>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class=" btn-group btn-group-sm" style="margin-top: 20px" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger btn-sm">Shortcut</button>
                                <div class="btn-group btn-group-sm" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_05_ProShip/LCPlanInformation?Type=ArtCapacity" CssClass="dropdown-item">Article Capacity Plan</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_05_ProShip/ArticleWiseLot?Type=Entry&genno=&actcode=&dayid=" CssClass="dropdown-item">Lot Assortment</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="~/F_05_ProShip/RptProcessBasePlan?Type=Daywise" CssClass="dropdown-item">Day Wise Plan Report</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_05_ProShip/RptOrderStatus?Type=MatMaster" CssClass="dropdown-item">MM Report</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink5" runat="server" Target="_blank" NavigateUrl="~/F_05_ProShip/RptOrderStatus?Type=OrdStatus" CssClass="dropdown-item">Order Status</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink6" runat="server" Target="_blank" NavigateUrl="~/F_01_Mer/RptOrdAppSheet?Type=OrdPlan" CssClass="dropdown-item">Order Plan Report</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink7" runat="server" Target="_blank" NavigateUrl="~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode=&sircode=" CssClass="dropdown-item">Production And Shipment Plan</asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 pr-1">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="small">Line<span class="text-twitter font-size-11"> *Last Book: <span id="LastBookdate" class="text-pink" runat="server"></span>, WH:<span id="DaysetupWkHours" class="text-pink" runat="server"></span></span></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlLines" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="DdlLines_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="lnkbtnSearch" class="input-group-text text-success" OnClick="lnkbtnSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkbtnCross" class="input-group-text text-danger" OnClick="lnkbtnCross_Click" Visible="false"><i class="fa fa-times"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnOrder" runat="server" CssClass="small" OnClick="lnkbtnOrder_Click"> <i class="fa fa-sync mr-1"></i> Order </asp:LinkButton>
                                <span class="text-red small">* Only Pipeline BOM</span>
                                <asp:DropDownList ID="ddlmlccod" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlmlccod_SelectedIndexChanged" AutoPostBack="True" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="row justify-content-between px-2 mb-1">
                                    <asp:Label ID="Label2" runat="server" CssClass="small" Text="Style/Color/BOM"></asp:Label>
                                    <asp:HyperLink ID="lblCrtLotLnk" runat="server" CssClass="small text-primary text-right font-weight-bold" Target="_blank">
                                        <i class="fa fa-arrow-right"></i> Create Link Lot
                                    </asp:HyperLink>
                                </div>
                                <asp:DropDownList ID="ddlStyle" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="small" Text="Lot No#"></asp:Label>
                                <asp:DropDownList ID="DdlLotNo" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="DdlLotNo_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="small" Text="Qty"></asp:Label>
                                <asp:TextBox ID="TxtQty" runat="server" CssClass=" form-control form-control-sm "></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="LblSelect" runat="server" Text="Select" OnClick="LblSelect_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="small">Lot Plan Qty</asp:Label>
                                <asp:Label ID="LblLotQty" runat="server" CssClass="form-control form-control-sm bg-flickr"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label12" runat="server" CssClass="small">Lot Plan Rem.</asp:Label>
                                <asp:Label ID="LblLotPlanRem" runat="server" CssClass="form-control form-control-sm bg-youtube"></asp:Label>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">

                    <div class="row">
                        <div class="table-responsive">
                            <asp:GridView ID="gvPlan" OnRowDataBound="gvPlan_RowDataBound" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" OnRowDeleting="gvPlan_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">
                                <Columns>

                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>">
                                        <ControlStyle CssClass="MyCustomClass" />
                                    </asp:CommandField>


                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Style ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                            <asp:Label ID="lblSlnum" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                Width="51px"></asp:Label>
                                            <asp:Label ID="lblLotno" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotno")) %>'
                                                Width="51px"></asp:Label>
                                            <asp:Label ID="lblDayid" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Line">
                                        <HeaderTemplate>
                                            <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Line" onkeyup="Search_Gridview(this, 2)"></asp:TextBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorID" Visible="false" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                Width="51px"></asp:Label>
                                            <asp:Label ID="lblmlccod" Visible="false" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'
                                                Width="51px"></asp:Label>
                                            <asp:Label ID="lblGvLinedsc" runat="server" ForeColor="Red"
                                                Style="text-transform: capitalize; font-size: 10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc"))%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Article Name">
                                        <HeaderTemplate>
                                            <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="100px" placeholder="Article Name" onkeyup="Search_Gridview(this, 3)"></asp:TextBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink Target="_blank"
                                                NavigateUrl='<%# ResolveUrl("~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))+
                                    "&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid"))) %>'
                                                ID="lblgvStyleDesc0" Font-Size="10px" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                Width="200px"></asp:HyperLink>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Order No">
                                        <HeaderTemplate>
                                            <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order No" onkeyup="Search_Gridview(this, 4)"></asp:TextBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOrderNo" Font-Size="10px" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Color">
                                        <HeaderTemplate>
                                            <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Color" onkeyup="Search_Gridview(this, 5)"></asp:TextBox>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnSizes" Width="70" OnClick="LbtnSizes_Click" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lot#">
                                        <ItemTemplate>
                                            <asp:Label ID="LblLot" Width="50" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Shipment">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvShipment" runat="server" Font-Size="9px" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shipmntdat")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                            <asp:Label ID="lblgvExplndate" Visible="false" runat="server" Font-Size="9px" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "explndate")).ToString("dd-MMM-yyyy") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="LblOrdQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("###0;(###0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="LblOrdQtyF" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Plan">
                                        <ItemTemplate>
                                            <asp:Label ID="LblTPlanQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlplnqty")).ToString("###0;(###0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="LblTPlanQtyF" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plan Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="LblPlanQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("###0;(###0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="LblPlanQtyF" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Target Efficiency">
                                        <ItemTemplate>
                                            <asp:TextBox ID="LblTargEfcincy" CssClass="bg-warning" BorderStyle="Solid" BorderWidth="1" runat="server" BackColor="Transparent" BorderColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trgteffcncy")) %>'
                                                Width="50px">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prod. Qty">
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
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Balance Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="LblBalQty" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("###0;(###0); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="LblBalQtyF" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Progress">
                                        <ItemTemplate>
                                            <asp:Label ID="LblProgress" runat="server" Width="80px">
                                                    <div class="progress">
                                                        <div class="progress-bar" role="progressbar" style='<%# "width:"+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "progress")).ToString("###0;(###0); ")+"%" %>' aria-valuenow="25" aria-valuemin="0" aria-valuemax="100">
                                                            <span><%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "progress")).ToString("###0;(###0); ") %>%</span>
                                                        </div>
                                                    </div>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hour">
                                        <ItemTemplate>
                                            <asp:TextBox ID="LblHours" CssClass="bg-teal text-white" BorderStyle="Solid" BorderWidth="1" BorderColor="LightSeaGreen" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whours")).ToString("###0;(###0); ") %>'
                                                Width="40px">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Capacity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="LblCapacity" CssClass="bg-twitter" BorderStyle="Solid" BorderWidth="1" BorderColor="LightSeaGreen" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capacity")).ToString("###0;(###0); ") %>'
                                                Width="50px">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Final Capacity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="LblFinCapacity" ReadOnly="true" CssClass="bg-twitter" BorderStyle="Solid" BorderWidth="1" BorderColor="LightSeaGreen" runat="server" BackColor="Transparent" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fincapacity")) %>'
                                                Width="50px">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Start date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtgvStrdate" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="85px">
                                            </asp:TextBox>
                                            <cc1:CalendarExtender ID="TxtStrdate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="TxtgvStrdate"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Days Req">
                                        <ItemTemplate>
                                            <asp:Label ID="LblDaysReq" runat="server" CssClass="badge badge-success" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "daysreq")).ToString("###0.0;(###0.0);") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtgvEnddate" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy") %>'
                                                Width="85px">
                                            </asp:TextBox>
                                            <cc1:CalendarExtender ID="TxtEnddate_CalendarExtender" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="TxtgvEnddate"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <div class=" btn-group btn-group-sm" style="margin-top: 20px" role="group" aria-label="Button group with nested dropdown">

                                                <div class="btn-group btn-group-sm" role="group">
                                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                                        <div class="dropdown-arrow"></div>
                                                        <li class="dropdown-item"><span class="badge badge-primary">Approved</span></li>
                                                        <li class="dropdown-item"><span class="badge badge-success">Day Saved</span></li>
                                                        <li class="dropdown-item"><span class="badge badge-warning">Day Not Saved</span></li>

                                                    </div>
                                                </div>
                                            </div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HypDetails" runat="server" Target="_blank"
                                                NavigateUrl='<%# ResolveUrl("~/F_05_ProShip/ProductionPlan?Type=Entry&actcode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))
                                                +"&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid"))+
                                                "&date="+Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "explndate")).ToString("dd-MMM-yyyy")+"&genno="+Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum"))+"&centrid="+Convert.ToString(DataBinder.Eval(Container.DataItem, "linecode"))) %>'
                                                CssClass="btn btn-xs btn-warning text-white">
                                                    <i class="fa fa-hand-point-right"></i>
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton OnClientClick='<%# "return window.open(\"MasterCalendarSetup?Type=plancalendar&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "linecode"))+"&date="+ Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "startdate")).ToString("dd-MMM-yyyy") +"&dayid="+ Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "enddate")).ToString("dd-MMM-yyyy") +"\")" %>' ID="HypPlanDetails" ClientIDMode="Static" runat="server" CssClass="btn btn-xs btn-secondary "><span class="fa fa-calendar-alt" title="Plan Calendar Details"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ChkAll" ClientIDMode="Static" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkack" ClientIDMode="Static" runat="server" CssClass="chkack"
                                                Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "mandayselect"))==true ? true : false %>'
                                                Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbAddMore" runat="server" Visible="false" CommandArgument="lbAddMore" OnClick="lbAddMore_Click"
                                                Width="30px" CssClass="text-info">
                                                    <i class="fa fa-plus"></i>
                                            </asp:LinkButton>

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

                    </div>


                    <div id="copyPlanModal" class="modal" role="dialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content ">
                                <div class="modal-header">
                                    <h4 class="modal-title"><span class="fa fa-table"></span>
                                        Process Planing Copy 
                                    </h4>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="LblInformation" runat="server" CssClass="alert alert-warning"></asp:Label>
                                            <div class="clearfix">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblCopyToProcess" CssClass="label" runat="server">Process (Copy to)</asp:Label>
                                                <asp:DropDownList ID="DdlCopytoProcess" runat="server" CssClass="form-control form-control-sm "></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label13" CssClass="label" runat="server">Start Day</asp:Label>
                                                <asp:DropDownList ID="DdlStarDay" runat="server" CssClass="form-control form-control-sm ">
                                                    <asp:ListItem Value="-7">-7</asp:ListItem>
                                                    <asp:ListItem Value="-6">-6</asp:ListItem>
                                                    <asp:ListItem Value="-5">-5</asp:ListItem>
                                                    <asp:ListItem Value="-4">-4</asp:ListItem>
                                                    <asp:ListItem Value="-3">-3</asp:ListItem>
                                                    <asp:ListItem Value="-2">-2</asp:ListItem>
                                                    <asp:ListItem Value="-1">-1</asp:ListItem>
                                                    <asp:ListItem Value="+1">+1</asp:ListItem>
                                                    <asp:ListItem Value="+2">+2</asp:ListItem>
                                                    <asp:ListItem Value="+3">+3</asp:ListItem>
                                                    <asp:ListItem Value="+4">+4</asp:ListItem>
                                                    <asp:ListItem Value="+5">+5</asp:ListItem>
                                                    <asp:ListItem Value="+6">+6</asp:ListItem>
                                                    <asp:ListItem Value="+7">+7</asp:ListItem>
                                                </asp:DropDownList>
                                                <%--   <asp:TextBox ID="TxtCopyStartDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="TxtCopyStartDate"></cc1:CalendarExtender>--%>
                                            </div>
                                        </div>
                                    </div>



                                </div>
                                <div class="modal-footer ">
                                    <asp:LinkButton ID="LbtnProcessCopy" OnClick="LbtnProcessCopy_Click" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update Process Plan </asp:LinkButton>

                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
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
                                            <li class="nav-item active"><a class="nav-link" href="#details" data-toggle="tab">Size Brakdown</a></li>
                                            <li class="nav-item"><a class="nav-link" href="#networking" data-toggle="tab">Planning History</a></li>
                                            <li class="nav-item"><a class="nav-link" href="#access-security" data-toggle="tab">Order Details</a></li>

                                        </ul>
                                    </header>


                                    <div class="tab-content">
                                        <div class="tab-pane active" id="details">

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
                                                            <a href="javascript:void()" onclick="SynchButton_Click();" style="width: 80px;" class="btn btn-sm btn-danger"><span class="fa fa-sync-alt"></span>Balance</a>
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
                                        <div class="tab-pane" id="networking">
                                            <asp:GridView ID="gvPlanSummary" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Trial Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PllblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trialordrnum")) %>'
                                                                Width="70px"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CODE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PllblgvSl" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                                Width="51px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TOD Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="PllblgvSipmentdate" Font-Bold="true" runat="server" Style="font-size: 11px;"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "todate")).ToString("dd-MMM-yyyy") %>'
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
                                                        <FooterStyle HorizontalAlign="Right" Font-Bold="false" />
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
                                    <asp:LinkButton ID="LbtnSizesUpdate" runat="server" OnClick="LbtnSizesUpdate_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update Size </asp:LinkButton>

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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

