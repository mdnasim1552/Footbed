<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProductionPlan.aspx.cs" Inherits="SPEWEB.F_05_ProShip.ProductionPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            //$("#HypPlanDetails").click(function () {
            //    alert("The paragraph was clicked.");
            //});
        });

        //function pageLoaded() {
        //    $("input, select").bind("keydown", function (event) {
        //        var k1 = new KeyPress();
        //        k1.textBoxHandler(event);
        //    });
        //}

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }

        function GenerateModal(prddate, linecode) {

            $("#Lbltardate").val(prddate);

            $("#Lbllinecode").val(linecode);
            // alert(prddate);
        }
        function CLoseMOdal() {
            $('#exampleModal').modal('hide');

        }
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
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">

                                <%--<asp:Label ID="lblProjectList0" runat="server" CssClass="lblTxt lblName">Order No</asp:Label>--%>
                                <asp:TextBox ID="txtOrderSearch" Visible="false" runat="server" CssClass="form-control-sm hidden" TabIndex="1"></asp:TextBox>
                                <asp:LinkButton ID="ImgbtnFindOrder" CssClass="label" runat="server" OnClick="ImgbtnFindOrder_Click" TabIndex="2" Text="Select Order No"></asp:LinkButton>
                                <asp:DropDownList ID="ddlOrderList" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                                <asp:Label ID="lblddlOrder" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>

                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="LblDStyle" runat="server" class="label">Style-Color-BOM No</asp:Label>
                                <asp:DropDownList ID="ddlStyle" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="4"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                                <a href="#" class="btn btn-sm btn-secondary" data-toggle="modal" data-target="#exampleModalDrawerRight">Calendar  <i class="fa fa-calendar-check"></i></a>

                                <asp:CheckBox ID="CbLineTrnsfr" runat="server" Text="Need Line Transfer?" Style="margin-left: 25px;" AutoPostBack="true" OnCheckedChanged="CbLineTrnsfr_CheckedChanged" />
                            </div>
                        </div>

                        <div class="col-md-3">

                            <a href="#" class="tile  tile-lg" style="background-color: MediumVioletRed; color: white;" title="Weekend">WD</a>
                            <a href="#" class="tile  tile-lg" style="background-color: YellowGreen; color: white;" title="Approve">AP</a>
                            <a href="#" class="tile  tile-lg" style="background-color: lightcoral; color: white;" title="Current">CU</a>

                        </div>

                    </div>
                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label" Text="Planing No:"></asp:Label>

                                <asp:Label ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm" Text="WEN00-"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="">&nbsp;</asp:Label>

                                <asp:Label ID="lblCurNo2" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Date:"></asp:Label>

                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="Ref No:"></asp:Label>
                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevList" runat="server" CssClass="label" OnClick="lbtnPrevList_Click" TabIndex="8"> Prev. List: </asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm" TabIndex="18"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">

                                <asp:Label ID="LblChangeLine" runat="server" CssClass="label" Text="Select Line to Change" Visible="false"></asp:Label>
                                <div class="input-group input-group-sm input-group-alt">
                                    <asp:DropDownList ID="DdlChangeLine" runat="server" CssClass="form-control form-control-sm chzn-select" Visible="false" AutoPostBack="true"></asp:DropDownList>
                                    <div class="input-group-append">
                                        <asp:LinkButton runat="server" ID="LnkbtnConfrm" class="btn btn-primary btn-sm" OnClientClick="return confirm('Do you want to confirm Line Change?');" Visible="false" OnClick="LnkbtnConfrm_Click">Confirm</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">
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

            </div>


            <section class="card card-fluid" style="min-height: 4900px;">
                <!-- .card-header -->
                <header class="card-header">
                    <!-- .nav-tabs -->
                    <ul class="nav nav-tabs card-header-tabs">
                        <li class="nav-item">
                            <a class="nav-link active show" data-toggle="tab" href="#home">Daily Line Wise Work PLan</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link " data-toggle="tab" href="#profile">Plan Assortment Summary</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link " data-toggle="tab" href="#OrderDetails">Order Details</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link " data-toggle="tab" href="#Requisition">Production Requisition List</a>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link" data-toggle="dropdown" href="#" role="button" aria-expanded="false">Other Action
                            <span class="caret"></span>
                            </a>
                            <div class="dropdown-arrow"></div>
                            <div class="dropdown-menu" x-placement="bottom-start" style="position: absolute; will-change: top, left; top: 57px; left: 0px;">
                                <a class="dropdown-item" target="_blank" href="<%= ResolveUrl("~/F_05_ProShip/LCPlanInformation") %>">Order Master Plan</a>
                                <a class="dropdown-item" target="_blank" href="<%= ResolveUrl("~/F_05_ProShip/ExportPlanVsAchiv?Type=Entry&actcode=&sircode=") %>">Production Plan Top Sheet</a>
                                <a class="dropdown-item" target="_blank" href="<%= ResolveUrl("~/F_05_ProShip/MasterCalendarSetup?Type=plancalendar&sircode=&date=&dayid=") %>">Production Planning Calendar Setup</a>
                                <div class="dropdown-divider"></div>
                                <a class="dropdown-item" target="_blank" href="<%= ResolveUrl("~/F_15_Pro/RptProtarVsAchieve?Type=Protvach") %>">Daily Line Wise  Production Report</a>
                                <a class="dropdown-item" target="_blank" href="<%= ResolveUrl("~/F_05_ProShip/MasterCalendarSetup?Type=mstrcalendar&sircode=&date=&dayid=") %>">Master Calendar Setup</a>

                            </div>
                        </li>

                    </ul>
                    <!-- /.nav-tabs -->
                </header>
                <!-- /.card-header -->
                <!-- .card-body -->
                <div class="card-body">
                    <!-- .tab-content -->
                    <div id="myTabContent" class="tab-content">
                        <div class="tab-pane fade active show" id="home">
                            <asp:GridView ID="gvsizes" OnRowDeleting="gvsizes_RowDeleting" OnRowCreated="gvsizes_RowCreated" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15" OnRowDataBound="gvsizes_RowDataBound"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">
                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

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
                                    <asp:TemplateField HeaderText="Customer Order">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvCustomerOrder" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custorder")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trial Order">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTrialOrder" Font-Bold="true" runat="server" Style="font-size: 10px;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trialordr")) %>'
                                                Width="100px"></asp:Label>

                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnAddNew" OnClick="lnkbtnAddNew_Click" runat="server" Text="Add New Row" CssClass="btn btn-primary btn-xs " TabIndex="4"></asp:LinkButton>

                                        </FooterTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">

                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtStrdate" runat="server" Style="text-transform: capitalize" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prddate")).ToString("dd-MMM-yyyy") %>'
                                                Width="91px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="TxtStrdate_CalendarExtender" runat="server" PopupPosition="TopRight"
                                                Format="dd-MMM-yyyy" TargetControlID="TxtStrdate"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Day">

                                        <ItemTemplate>
                                            <asp:Label ID="LblDay" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "daysl")) %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Line">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlline" Width="200px" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                            <asp:Label ID="LblReason" runat="server" Style="color: #ffffff; font-weight: bold;" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ACPD">
                                        <ItemTemplate>
                                            <asp:Label ID="LblCapacity" Style="text-align: right" ToolTip="Article Capacity Day Wise"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "artcapacity")).ToString("###0;") %>'
                                                runat="server" Width="70px" CssClass=" text-twitter"></asp:Label><br />
                                            <asp:Label ID="Label1" Font-Italic="true" Style="text-align: right" ToolTip="Diffirence Between Capacity and Plan"
                                                Text='<%# "DIFF: "+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "capabal")).ToString("###0;-###0;") %>'
                                                runat="server" Width="70px" CssClass=" text-danger"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Allocate">
                                        <ItemTemplate>
                                            <asp:Label ID="LblAlocation" Style="text-align: right" runat="server"></asp:Label><br />
                                            <asp:Label ID="LblLinecapacity" Font-Italic="true" Style="text-align: right" ToolTip="Line Capacity"
                                                runat="server" CssClass=" text-danger"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-01" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-02" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-03" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-04" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-05" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF5" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF6" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS13" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS14" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS15" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF16" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS16" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS17" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS18" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS19" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS20" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS21" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF22" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS22" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-23" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF23" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS23" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-24" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF24" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS24" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-25" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF25" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS25" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-26" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF26" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS26" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-27" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS27" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS28" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS29" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS30" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-31" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF31" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS31" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-32" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF32" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS32" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-33" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF33" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS33" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-34" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF34" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS34" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-35" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF35" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS35" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-36" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF36" runat="server" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS36" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-37" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39" BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS37" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS38" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS39" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFS40" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="40px"
                                                Style="text-align: right"></asp:Label>
                                        </FooterTemplate>
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
                                    <%--  <asp:TemplateField HeaderText="Trial Order QTY">
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
                                    --%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:LinkButton ID="ApproveAll" OnClick="ApproveAll_Click" OnClientClick="return confirm('Do you want to Approve all?');" runat="server"><span class="fa fa-thumbs-up" title="Approve All"></span></asp:LinkButton>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnApprove" OnClick="LbtnApprove_Click" OnClientClick="return confirm('Do you want to Approve for production?');" runat="server" CssClass="btn btn-xs btn-success"><span class="fa fa-thumbs-up" title="Approve for Production"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Copy">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnCopy" OnClick="LbtnCopy_Click" OnClientClick="return confirm('Do you want to Copy this Date?');" runat="server" CssClass="btn btn-xs btn-warning"><span class="fa fa-copy" title="Copy Date"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Plan">
                                        <ItemTemplate>
                                            <asp:LinkButton OnClientClick='<%# "return window.open(\"MasterCalendarSetup?Type=plancalendar&sircode="+Convert.ToString(DataBinder.Eval(Container.DataItem, "linecode"))+"&date="+ Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prddate")).ToString("dd-MMM-yyyy") +"&dayid="+ Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prddate")).ToString("dd-MMM-yyyy") +"\")" %>' ID="HypPlanDetails" ClientIDMode="Static" runat="server" CssClass="btn btn-xs btn-secondary "><span class="fa fa-calendar-alt" title="Plan Details"></span></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Date Change?">
                                        <ItemTemplate>
                                            <button type="button" class="btn btn-sm btn-secondary"
                                                onclick="GenerateModal('<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prddate")).ToString("dd-MMM-yyyy") %>','<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linecode")) %>')" data-toggle="modal" data-target="#exampleModal">
                                                Set</button>
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
                        <div class="tab-pane fade " id="profile">
                            <asp:GridView ID="gvPlanSummary" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15" OnRowDataBound="gvPlanSummary_RowDataBound"
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
                                    <%--  <asp:TemplateField HeaderText="Color">
                                                             
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ColorDesc")) %>'
                                                                        Width="91px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Size-01" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF1" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-02" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF2" runat="server" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-03" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF3" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-04" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF4" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-05" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF5" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF6" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF7" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF8" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF9" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF10" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF11" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-12" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF12" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-13" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF13" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-14" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF14" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-15" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF15" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF16" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF17" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-18" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF18" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-19" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF19" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-20" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF20" runat="server"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="PltxtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
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
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-md-2">
                                Current Plan Selected Color with
                            </div>
                            <div class="col-md-3">
                                <div style="width: 30px; height: 30px; background-color: lightcoral"></div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="tab-pane fade " id="OrderDetails">
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
                                            <asp:Label ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-02">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-03">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-04">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-05">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-06" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-07" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-08" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-09" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-10" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-11" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:Label>
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

                        <div class="tab-pane fade " id="Requisition">
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:GridView ID="gvShiMentInfo2" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" OnRowDataBound="gvShiMentInfo2_RowDataBound">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblSlNo" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblsiReqNo" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblReqDate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pbdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Target Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblTarDate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "tardate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="gvsilftrTtl" runat="server" CssClass="font-weight-bold" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FG Qty.">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblFgQty" runat="server" Height="16px" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rsqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="gvsilftrTtlFgQty" runat="server" CssClass="text-right font-weight-bold" Width="100"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mat. Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblMatQty" runat="server" Height="16px" CssClass="text-right pr-2" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterTemplate>
                                                    <asp:Label ID="gvsilftrTtlMatQty" runat="server" CssClass="text-right font-weight-bold" Width="100"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Req. By">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblReqBy" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "posteduser")) %>'
                                                        Width="125px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Entry Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblPostedDate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "posteddat")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">

                                                <HeaderTemplate>
                                                    <div class="form-inline justify-content-center">
                                                        <div class="mr-3">
                                                            <asp:LinkButton runat="server" ID="LbtnIssueMulti" OnClick="LbtnIssueMulti_Click" ToolTip="Selected Multiple REQ Issue" CssClass="btn btn-sm btn-success pr-1">
                                                        <i class="fa fa-check mr-1"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lnkbtnPrintCombined" CssClass="btn btn-sm btn-primary pr-1" ToolTip="Selected Multiple Req Print" OnClick="lnkbtnPrintCombined_Click">
                                                        <i class="fa fa-print mr-1"></i>
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div>
                                                            <asp:CheckBox ID="chkHead" onclick="javascript:SelectAllCheckboxes('gvShiMentInfo2', this);" ClientIDMode="Static" runat="server" />
                                                        </div>
                                                    </div>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chkPrntCombined" CssClass="ml-2" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Batch Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblBatchCode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batchcode")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvsilblDayId" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "odayid")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                                <div class="col-md-3" style="margin-left:10px">
                                    <asp:GridView ID="gvProcessStat" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings />
                                        <RowStyle />
                                        <Columns>

                                            <asp:TemplateField HeaderText="SL">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvSlNum" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Process">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvlblProdProc" runat="server" Height="16px" Width="110px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prostepdesc")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Received">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvRcvQty" runat="server" Height="16px" CssClass="text-right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty")).ToString("#,##0;(#,##0); ") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Done">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvDoneQty" runat="server" Height="16px" CssClass="text-right pr-2" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="gvBalQty" runat="server" Height="16px" CssClass="text-right pr-2" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
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
                    <!-- /.tab-content -->
                </div>

                <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <!-- .modal-dialog -->
                    <div class="modal-dialog" role="document">
                        <!-- .modal-content -->
                        <div class="modal-content">
                            <!-- .modal-header -->
                            <div class="modal-header">
                                <h5 class="modal-title">Set Date to Change </h5>
                            </div>
                            <!-- /.modal-header -->
                            <!-- .modal-body -->
                            <div class="modal-body">
                                You are changing  
                                <asp:TextBox ID="Lbltardate" Style="text-transform: capitalize" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                    Width="91px" runat="server" ClientIDMode="Static"></asp:TextBox>
                                Plan to
                                 <asp:TextBox ID="TxtChngdate" runat="server" Style="text-transform: capitalize" BackColor="Transparent" BorderColor="#9ed5ef" BorderWidth="1px" BorderStyle="Solid"
                                     Width="91px"></asp:TextBox>
                                <asp:TextBox ID="Lbllinecode" runat="server" Style="display: none;" ClientIDMode="Static"></asp:TextBox>

                                <cc1:CalendarExtender ID="TxtChngdate_CalendarExtender" runat="server" PopupPosition="TopRight" Format="dd-MMM-yyyy" TargetControlID="TxtChngdate"></cc1:CalendarExtender>
                                <span class="text-youtube">Note: Please remember if Production requisition completed against this Plan it will adjust with this date  And Planning Head only can do this job </span>
                            </div>
                            <!-- /.modal-body -->
                            <!-- .modal-footer -->
                            <div class="modal-footer">
                                <asp:LinkButton ID="LbtnUpdateDateChange" runat="server" OnClick="LbtnUpdateDateChange_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update Plan </asp:LinkButton>

                                <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                            </div>
                            <!-- /.modal-footer -->
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
                <!-- /.card-body -->
            </section>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
