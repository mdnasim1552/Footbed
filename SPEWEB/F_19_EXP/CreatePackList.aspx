<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="CreatePackList.aspx.cs" Inherits="SPEWEB.F_19_EXP.CreatePackList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function openModal() {
            $('#myModal').modal({ backdrop: "static" });
        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');

        }
        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <style>
        .export .modal-dialog {
            max-width: 100% !important;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
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

            <div class="card card-fluid mb-1">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblinvdate" runat="server" CssClass="small">Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm small px-1"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="trnsferNo" runat="server" CssClass="small">Pack Plan</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" runat="server" Text="EPP00-" ReadOnly="True" CssClass="col-6 form-control form-control-sm small" Style="padding: 4px !important"></asp:Label>
                                    <asp:TextBox ID="txtCurNo2" runat="server" ReadOnly="True" CssClass="col-6 form-control form-control-sm small">00000</asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 " runat="server" id="FieldSeason">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" CssClass="small">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="small">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="small">Article No</asp:Label>
                                <asp:DropDownList ID="ddlmlccode" runat="server" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="Label16" runat="server" CssClass="small">Order/Style/Color</asp:Label>
                                <div class="d-flex">
                                    <asp:DropDownList ID="dllorderType" runat="server" OnSelectedIndexChanged="dllorderType_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    <asp:HyperLink ID="hypbtnMatReq1" CssClass="btn btn-warning btn-sm text-white" Target="_blank" runat="server"><i class="fa fa-info-circle"></i> </asp:HyperLink>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="imgPreVious" runat="server" CssClass="small text-primary" OnClick="imgPreVious_Click"> <i class="fa fa-search mr-1"></i> Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>


                    </div>

                    <div class="row" id="SelectionPanel" runat="server" visible="false">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="small">Customer Order</asp:Label>
                                <asp:DropDownList ID="DdlCustorder" AutoPostBack="true" OnSelectedIndexChanged="DdlCustorder_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 px-0">
                            <div class="form-inline" style="margin-top: 20px;">
                                <asp:LinkButton ID="Add" runat="server" CssClass="btn btn-primary btn-sm" OnClick="Add_Click">Add</asp:LinkButton>
                                <asp:HyperLink ID="HypPi" runat="server" Target="_blank" CssClass="btn btn-success btn-sm text-white ml-1" title="Proforma Invoice"><span class="fa fa-file-pdf"></span> PI</asp:HyperLink>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 px-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="small">Contract No/LC No/TT</asp:Label>
                                <asp:Label ID="txtlccontact" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 px-1">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="small" Font-Size="Smaller">L/C Expiry Date</asp:Label>
                                <asp:TextBox ID="txtexdat" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 px-1">
                            <div class="form-group">
                                <asp:Label ID="lblorqty" runat="server" CssClass="small">Main Order Qty</asp:Label>
                                <asp:TextBox ID="txtordqty" runat="server" CssClass="form-control form-control-sm bg-twitter"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="small">PO. Qty:</asp:Label>
                                <asp:TextBox ID="txtPoQty" runat="server" CssClass="form-control form-control-sm bg-apple"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblreqty" runat="server" CssClass="small">Rem. Qty:</asp:Label>
                                <asp:TextBox ID="txtexpqty" runat="server" CssClass="form-control form-control-sm bg-pinterest"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 px-0" id="Btnpanel" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="LbtnShowAll" runat="server" CssClass="btn btn-secondary btn-sm px-0" OnClick="LbtnShowAll_Click"><span class="fa fa-th-list"></span> Order</asp:LinkButton>
                                <asp:LinkButton ID="RefBtn" runat="server" CssClass="btn btn-danger btn-sm" OnClick="RefBtn_Click"><span class="fa fa-sync-alt"></span></asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblCstmRefNo" runat="server" Font-Size="Smaller">Custom Ref. No.</asp:Label>
                                <asp:TextBox ID="txtCstmRefNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <div class="accordion mb-5" id="accordionExample">

                <div class="card mb-1">
                    <div class="card-header" id="headingOne">
                        <h2 class="mb-0">
                            <div class="row">
                                <div class="col-md-3">
                                    <button class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                        <i class="fa fa-arrow-circle-right mr-2"></i>Packing Details Information                                
                                    </button>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="LblBookingDate" CssClass="form-control form-control-sm bg-twitter" runat="server" Text="Ex. Factory Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2">

                                    <asp:TextBox ID="TxtBookingDate" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="TxtBookingDate"></cc1:CalendarExtender>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="Label6" CssClass="form-control form-control-sm bg-twitter" runat="server" Text="Act. Ex.Factory Date"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="TxtExFactoryDate" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                        Format="dd-MMM-yyyy" TargetControlID="TxtExFactoryDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                        </h2>
                    </div>

                    <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                        <div class="card-body">

                            <div class="table-responsive" style="min-height: 100px;">

                                <asp:GridView ID="gv1pack" runat="server" OnRowDataBound="gv1pack_RowDataBound" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                    ShowFooter="True" Width="253px" OnRowDeleting="gv1pack_RowDeleting" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                    <Columns>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red " ItemStyle-CssClass="DeleteBtn" DeleteText="<span class='fa fa-trash'></span>" />
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbAddMore" runat="server"
                                                    OnClick="AddMore_Click" Width="30px" CssClass="text-facebook"><i class="fa fa-plus"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sl." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvSlnum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Style ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                    Width="51px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                    Width="51px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Article">

                                            <ItemTemplate>
                                                <asp:HyperLink ToolTip="Click For BOM Details" ID="PlblgvStyleDesc0" NavigateUrl='<%# "/F_01_Mer/MerChanPrint?Type=BOMPrint&mlccod="+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod"))+
                                                        "&Ptype=import&dayid="+Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid"))+"&sircode="+
                                                        Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid"))+Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid"))+"&format=PDF&Dept=Planning"
                                                        %>'
                                                    runat="server" Style="text-transform: capitalize; text-align: center;" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="120px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <%--<asp:LinkButton CssClass="btn btn-sm btn-info" ID="LbtnCalculate" runat="server" OnClick="LbtnCalculate_Click">Total</asp:LinkButton>

                                            <asp:LinkButton CssClass="btn btn-sm btn-danger" ID="pLbtnCLose" runat="server" OnClick="pLbtnCLose_Click">Collapse</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Forma Name">

                                            <ItemTemplate>
                                                <asp:Label ID="gv1LblFormaName" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "formadesc")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <FooterTemplate>
                                                <%-- <asp:LinkButton ID="lbtnPush" runat="server" Font-Bold="True" Font-Size="12px"
                                                OnClick="lbtnPush_Click" CssClass="btn btn-success btn-sm">Push</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ColorDesc")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Customer Ref./Style">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtRefno" runat="server" CssClass="form-control form-control-sm bg-secondary text-capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custrefno")) %>'
                                                    Width="80px" Style="text-transform: capitalize" ></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bar / EAN /<br/>Supplier Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBarCdRefNo" runat="server" CssClass="form-control form-control-sm bg-secondary text-capitalize text-indigo"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "barcodrefno")) %>' Width="120px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                            <ItemStyle HorizontalAlign="Left" Width="120px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Customer Order/PO <span class='text-red'>*</span>">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustOrder" Style="display: none" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custordno")) %>'></asp:Label>

                                                <asp:TextBox ID="TxtCustOrder" runat="server" CssClass="form-control form-control-sm bg-secondary text-capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custordno")) %>'
                                                    Width="80px" Style="text-transform: capitalize" ></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FB Order<br/>No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFbOrdrNo" runat="server" Style="text-transform: capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HS Code">
                                            <ItemTemplate>
                                                <asp:Label ID="gv1LblHsCode" runat="server"
                                                    CssClass="text-left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hscode")) %>' Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Label Type<br/>/ Department">

                                            <ItemTemplate>
                                                <asp:TextBox ID="gv1TxtTypeOfLbl" runat="server" CssClass="text-left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typeoflebel")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Justify" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Carton No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCrtnNo" runat="server" CssClass="form-control form-control-sm bg-secondary text-capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "crtnno")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Packing">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DdlPacklist" Width="100px" CssClass="form-control form-control-sm" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Num of. CTN" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Ptxtcarton" runat="server" CssClass="bg-twitter" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cartoon")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <br />
                                                <br />
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-01">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF1" Visible="false" Style="text-align: right !important;" CssClass="text-danger" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF1" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-02">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF2" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF2" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-03">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF3" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF3" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-04">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF4" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF4" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-05">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF5" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF5" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-06" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF6" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF6" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-07" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF7" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF7" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-08" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF8" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF8" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />

                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-09" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF9" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF9" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-10" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF10" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("###0;(###0);") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF10" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />

                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-11" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF11" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF11" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-12" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF12" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF12" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-13" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF13" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p13")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF13" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-14" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF14" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p14")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF14" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-15" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF15" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p15")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF15" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-16" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF16" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p16")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF16" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-17" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF17" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p17")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF17" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-18" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF18" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p18")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF18" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-19" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF19" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p19")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF19" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-20" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF20" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p20")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="flblgvF20" Style="text-align: right !important;" CssClass="text-danger" runat="server"></asp:Label>

                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-21" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                                <asp:Label ID="PlblgvF21" Visible="false" Style="text-align: right;" runat="server" CssClass="text-danger" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p21")).ToString("###0;(###0);") %>'></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-22" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-23" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-24" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-25" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-26" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-27" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-28" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-29" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-30" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-31" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-32" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-33" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-34" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-35" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-36" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-37" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-38" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-39" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Size-40" Visible="False">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PtxtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                    BorderStyle="Solid" BorderWidth="1px"
                                                    Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pairs/Carton">
                                            <ItemTemplate>
                                                <asp:Label ID="txtPrsPerCrtn" runat="server" CssClass="text-right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pairspercrtn")).ToString("#,##0;(#,##0); ") %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total<br>Cartons">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTtlCrtn" runat="server" CssClass="form-control form-control-sm text-right bg-green"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlcrtns")).ToString("#,##0;(#,##0); ") %>' Width="50px"></asp:TextBox>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlCrtns" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="50px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total<br>Pairs">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvTotal1" runat="server" CssClass="text-right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlpairs")).ToString("#,##0;(#,##0); ") %>' Width="70px"></asp:Label>
                                                <%--<asp:Label ID="Label4" Visible="false" runat="server" Style="font-size: 11px; text-align: right" CssClass="text-danger"
                                                    Text=""
                                                    Width="60px"></asp:Label>--%>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label ID="PFLblgvTotalPair" CssClass="text-right font-weight-bold" runat="server"></asp:Label>
                                                <asp:Label ID="PFLblgvTotal" runat="server" Visible="false"></asp:Label><br />
                                            </FooterTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Carton Measurement" ItemStyle-Width="210px">
                                            <ItemTemplate>
                                                <div class="row justify-content-center" style="width: 210px">
                                                    <label class="form-control form-control-sm px-1 bg-primary text-white font-weight-bold" style="width: 20px;">L</label>
                                                    <asp:TextBox ID="lblBoxLength" runat="server" CssClass="form-control form-control-sm bg-secondary"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "boxlength")).ToString("#,##0.00;(#,##0.00); ") %>' Width="40px" placeholder="L"></asp:TextBox>

                                                    <label class="form-control form-control-sm px-1 bg-primary text-white font-weight-bold ml-1" style="width: 20px;">W</label>
                                                    <asp:TextBox ID="lblBoxWidth" runat="server" CssClass="form-control form-control-sm bg-secondary"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "boxwidth")).ToString("#,##0.00;(#,##0.00); ") %>' Width="40px" placeholder="W"></asp:TextBox>

                                                    <label class="form-control form-control-sm px-1 bg-primary text-white font-weight-bold ml-1" style="width: 20px;">H</label>
                                                    <asp:TextBox ID="lblBoxHeight" runat="server" CssClass="form-control form-control-sm bg-secondary"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "boxheight")).ToString("#,##0.00;(#,##0.00); ") %>' Width="40px" placeholder="H"></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="210px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="210px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CBM (cm)">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblCBM" runat="server" CssClass="text-right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbm")).ToString("#,##0.00000;(#,##0.00000); ") %>' Width="70px"></asp:Label>

                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlCbm" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="70px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="G. Wgt/Carton">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGrsWgt" runat="server" CssClass="form-control form-control-sm text-right bg-green"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grswgtpercrtn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total G. Wgt (KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTtlGrsWgt" runat="server" ReadOnly="true" CssClass="text-right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlgrswgt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlGrsWgt" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="N. Wgt/Carton">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNetWgt" runat="server" CssClass="form-control form-control-sm text-right bg-green"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netwgtpercrtn")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total N. Wgt (KG)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTtlNetWgt" runat="server" ReadOnly="true" CssClass="text-right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlnetwgt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlNetWgt" CssClass="text-right font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color QTY" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="PlblgvColTotal1" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="PFLblgvColTotal" Font-Bold="true" runat="server"></asp:Label>
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
                                <br />
                                <asp:GridView ID="gvcollapseTwo" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea text-dark"
                                AutoGenerateColumns="False" ShowFooter="true">
                                <PagerSettings Position="Bottom" />
                                <RowStyle Font-Size="11px" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSno" runat="server" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" Width="35px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcd" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMatName9" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="left" Width="450px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Carton">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcarton" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "carton")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="center" Width="55px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Measurement">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmeasurmnt" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc1")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="center" Width="100px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrmrks" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                                <br />

                            </div>

                        </div>
                    </div>
                </div>

                <div class="card">
                    <div class="card-header" id="headingTwo">
                        <h2 class="mb-0">
                            <button class="btn btn-link btn-block text-left collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <i class="fa fa-arrow-circle-right mr-2"></i>Packing Measurement Information
                            </button>
                        </h2>
                    </div>
                    <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                        <div class="card-body">

                        </div>
                    </div>
                </div>

            </div>


            <div id="myModal" class="modal export" role="dialog">
                <div class="modal-dialog  ">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table mr-2"></span>
                                Style Wise Order Details And Current Stock
                            </h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">

                                    <section class="card card-fluid">
                                        <!-- .card-header -->
                                        <header class="card-header">
                                            <!-- .nav-tabs -->
                                            <ul class="nav nav-tabs card-header-tabs">
                                                <li class="nav-item">
                                                    <a class="nav-link active show" data-toggle="tab" href="#home">Stock Details</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" data-toggle="tab" href="#profile">Order Details</a>
                                                </li>
                                                <li class="nav-item dropdown">
                                                    <a class="nav-link" data-toggle="dropdown" href="#" role="button">Important Links 
                                                                <span class="caret"></span>
                                                    </a>
                                                    <div class="dropdown-arrow"></div>

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
                                                    <asp:GridView ID="gvStockDetails" runat="server" AutoGenerateColumns="False" Height="1px" OnRowDataBound="gvStockDetails_RowDataBound"
                                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="DlblgvSlNo0" runat="server"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkColItem" runat="server" />
                                                                </ItemTemplate>

                                                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order No ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblOrderno" runat="server"
                                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrno")) %>'
                                                                        Width="250px"></asp:Label>
                                                                    <asp:Label ID="LblRescode" Visible="false" runat="server"
                                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                                        Width="250px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblResDesc" runat="server"
                                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                        Width="400px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Location ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblLocation" runat="server"
                                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locdesc")) %>'
                                                                        Width="50px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="DlgvqtyStock" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Style="text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterTemplate>
                                                                    <asp:Label ID="DlgvproFqty" runat="server"
                                                                        ForeColor="#000" Style="text-align: right"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Stock">
                                                                <ItemTemplate>
                                                                    <asp:Label ForeColor="#ff0000" ID="DlgvTotalStock" runat="server"
                                                                        BorderStyle="None" Style="text-align: right" Text="test"
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>


                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="In Process">
                                                                <ItemTemplate>
                                                                    <asp:Label ForeColor="#ff33cc" ID="DlgvInProcess" runat="server"
                                                                        BorderStyle="None" Style="text-align: center"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inprqty")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>


                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Availabe ">
                                                                <ItemTemplate>
                                                                    <asp:Label ForeColor="#ff0000" Text="test" ID="DlgvAvailabe" runat="server"
                                                                        BorderStyle="None" Style="text-align: right"
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>


                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="tab-pane fade" id="profile">
                                                    <asp:GridView ID="gvsizes" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Style">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Color">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                                        Width="60px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Size-01" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF1" runat="server" BackColor="Transparent"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-02" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF2" runat="server" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-03" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF3" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-04" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF4" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-05" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF5" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvF6" runat="server"
                                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                                        Width="40px"></asp:Label>
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
                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <!-- /.tab-content -->
                                        </div>
                                        <!-- /.card-body -->
                                    </section>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="LbtnPushGrid" OnClientClick="CLoseMOdal()" CssClass="btn btn-sm btn-subtle-info" OnClick="LbtnPushGrid_Click" runat="server">Add Selected Item</asp:LinkButton>
                            <button type="button" class="btn btn-sm btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
