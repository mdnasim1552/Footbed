<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ExportMgt.aspx.cs" Inherits="SPEWEB.F_19_EXP.ExportMgt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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

        .radbtn tbody tr td input[type="radio"] {
            margin-right: 0px;
            margin-left: 4px;
        }

        .chkPackPlan input[type=checkbox] {
            margin-right: 5px;
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
                        <div class="col-md-9 col-sm-9 col-lg-9 ">
                            <div class="form-group">
                                <asp:RadioButtonList ID="RadioButtonList1" CssClass="small form-control form-control-sm bg-secondary radbtn" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">Commercial Invoice</asp:ListItem>
                                    <asp:ListItem Value="1">Forwarding Letter</asp:ListItem>
                                    <asp:ListItem Value="2">Packing List</asp:ListItem>
                                    <asp:ListItem Value="3">Bill of Exchange</asp:ListItem>
                                    <asp:ListItem Value="4">GSP Format</asp:ListItem>
                                    <asp:ListItem Value="5">Beneficiary's Declaration</asp:ListItem>
                                    <asp:ListItem Value="6">Beneficiary's Certificate</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <%--<asp:LinkButton ID="imgPreVious" runat="server" CssClass="control-label" OnClick="imgPreVious_Click">Previous List</asp:LinkButton>--%>
                            <%--<asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                </asp:DropDownList>--%>

                            <div class="row">
                                <div class="col-5 bg-secondary pr-0">
                                    <asp:LinkButton ID="imgPreVious" runat="server" CssClass="text-primary font-weight-bold d-block mt-1" OnClick="imgPreVious_Click"> 
                                        <i class="fa fa-search mr-1"></i> Previous List
                                    </asp:LinkButton>
                                </div>
                                <div class="col-7 pl-0">
                                    <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm border-left-0 chzn-select" TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblinvdate" runat="server" CssClass="small">Inv Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="trnsferNo" runat="server" CssClass="small">Inv ID</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" Text="INV00-" ReadOnly="True" CssClass=" smLbl_to small"></asp:Label>
                                <asp:TextBox ID="txtCurNo2" runat="server" ReadOnly="True" CssClass="form-control form-control-sm ">00000</asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label18" runat="server" CssClass="small">Invoice No</asp:Label>
                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="small">Trans. Mode</asp:Label>
                                <asp:DropDownList ID="DDLShipmentType" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" CssClass="small">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="small">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblorqty" runat="server" CssClass="small">Order Qty:</asp:Label>
                                <asp:TextBox ID="txtordqty" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="70px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblreqty" runat="server" CssClass="small">Rem. Qty:</asp:Label>
                                <asp:TextBox ID="txtexpqty" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="70px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 " id="Btnpanel" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="LbtnShowAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="LbtnShowAll_Click">Order Details</asp:LinkButton>
                                <asp:LinkButton ID="RefBtn" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="RefBtn_Click">Refresh</asp:LinkButton>
                            </div>
                        </div>



                    </div>

                    <div class="row" id="SelectionPanel" runat="server" visible="false">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Master L/C</asp:Label>
                                <asp:DropDownList ID="ddlmlccode" runat="server" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label16" runat="server" CssClass="label">Order Type</asp:Label>
                                <asp:DropDownList ID="dllorderType" runat="server" OnSelectedIndexChanged="dllorderType_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label17" runat="server" CssClass="label">Style Name</asp:Label>
                                <div class="d-flex">
                                    <asp:DropDownList ID="ddlprocode" runat="server" OnSelectedIndexChanged="ddlprocode_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    <asp:HyperLink ID="hypbtnMatReq1" CssClass="btn btn-warning btn-sm text-white" ToolTip="LC General Info" Target="_blank" runat="server"><i class="fa fa-info-circle"></i></asp:HyperLink>

                                </div>

                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="Add" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="Add_Click"><span class="fa fa-check"></span></asp:LinkButton>
                                <asp:LinkButton ID="AddAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="AddAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <asp:CheckBox runat="server" AutoPostBack="true" ID="chkPackPlan" CssClass="badge " Style="margin-top: 20px;" Text="From Plan" OnCheckedChanged="chkPackPlan_CheckedChanged" />
                        </div>
                        <div runat="server" id="divPackPln" class="col-md-4 col-sm-4 col-lg-4 pr-6" visible="false">
                            <asp:Label ID="lblPackPlnList" runat="server" CssClass="label">Package Plan</asp:Label>
                            <div class="d-flex">
                                <asp:DropDownList ID="ddlPackPlnList" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                <asp:LinkButton ID="btnAddPck" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="btnAddPck_Click">Add</asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="accordion mb-5" id="accordionExample">

                <div class="card mb-1">
                    <div class="card-header" id="headingOne">
                        <h2 class="mb-0">
                            <button class="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                <i class="fa fa-arrow-circle-right mr-2"></i>Article/Size Information
                            </button>
                        </h2>
                    </div>

                    <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                        <div class="card-body" style="min-height: 350px;">

                            <div class="table-responsive" style="min-height: 100px;">

                                <asp:GridView ID="gvSalCon" runat="server" ShowFooter="true" OnRowDeleting="gvSalCon_RowDeleting" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <Columns>

                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCol" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhead" AutoPostBack="true" OnCheckedChanged="chkheadl_CheckedChanged" runat="server" />
                                            </HeaderTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvMlcdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Font-Size="10px"
                                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Style">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvstyle" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Font-Size="10px"
                                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PO No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvpono" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "po")) %>'
                                                    Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvArtno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrno")) %>'
                                                    Width="90px" placeholder="Limit (12 Digit)"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvColor" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="txtFTotal" runat="server" ForeColor="Black" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvsize" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbAddMore" runat="server"
                                                    OnClick="AddMore_Click" Width="30px" CssClass="text-facebook"><i class="fa fa-plus"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Qty" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFNetTotal" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvordrqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Prod Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="txtPrdTotal" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvprdqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prdqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bal Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="txtbalqtyTotal" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvbalqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="In-Proc Qty">

                                            <ItemTemplate>
                                                <asp:Label ID="txtgvInProcqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inprocqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PRS/CTN">
                                            <%--<FooterTemplate>
                                            <asp:Label ID="txtFpperctnqty" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvpperctnqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pperctnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total <br> PRS">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFtotlprs" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtotlprs" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total <br> CTNS">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFtotlctn" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtotlctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlctn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="G.W/ CTNS">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFgwperctn" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvgwperctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gwperctn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total G.W">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFtotalgw" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvtotalgw" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalgw")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NetW/ <br> CTN">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFnwperctn" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnwperctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nwperctn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total N.W">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFtotalNw" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbltotlnw" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlctn"))*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nwperctn"))).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dimension <br>of CTNS">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtdimenctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dimenctn")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Wearhouse">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvwearhouseno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "wearhouse")) %>'
                                                    Width="90px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CBM">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFcbm" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcbm" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbm")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">

                                            <ItemTemplate>
                                                <asp:TextBox ID="lblRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FC </br>Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="txtFAmt" runat="server" ForeColor="Black"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate"))*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs"))).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <AlternatingRowStyle />
                                    <FooterStyle CssClass="grvFooter" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </div>

                        </div>
                    </div>
                </div>

                <div class="card">
                    <div class="card-header" id="headingTwo">
                        <h2 class="mb-0">
                            <button class="btn btn-link btn-block text-left collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                <i class="fa fa-arrow-circle-right mr-2"></i>Basic Information
                            </button>
                        </h2>
                    </div>
                    <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                        <div class="card-body" style="min-height: 350px;">

                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <div class="row">
                                    <div class="col-md-1 col-sm-1 col-lg-1 ">
                                        <asp:Label ID="Label7" runat="server" CssClass="label">Mode</asp:Label>
                                        <asp:DropDownList ID="ddlDelMode" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-1 col-sm-2 col-lg-1">
                                        <asp:Label ID="Label5" runat="server" CssClass="label">Del. Date</asp:Label>
                                        <asp:TextBox ID="txtMDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtMDate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtMDate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-1 col-sm-2 col-lg-1">
                                        <asp:Label ID="Label13" runat="server" CssClass="label">Ex-Fact Date</asp:Label>
                                        <asp:TextBox ID="txtExFact" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtExFact_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtExFact"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <asp:Label ID="lblBlNo" runat="server" CssClass="label">BL No</asp:Label>
                                        <asp:TextBox ID="txtBlNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    </div>

                                    <div class="col-md-1 col-sm-2 col-lg-1">
                                        <asp:Label ID="lblBlDate" runat="server" CssClass="label">BL Date</asp:Label>
                                        <asp:TextBox ID="txtBlDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtBlDate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtBlDate"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <asp:Label ID="Label11" runat="server" CssClass="label">Exp No</asp:Label>
                                        <asp:TextBox ID="txtExpNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    </div>

                                    <div class="col-md-1 col-sm-2 col-lg-1">
                                        <asp:Label ID="Label14" runat="server" CssClass="label">Exp Date</asp:Label>
                                        <asp:TextBox ID="txtExpDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtExpDate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtExpDate"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label runat="server" ID="lblFrmt" CssClass="col-form-label">Print Format</asp:Label>
                                        <asp:DropDownList ID="ddlFormat" runat="server" CssClass="form-control form-control-sm px-0">
                                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">F - CCC</asp:ListItem>
                                            <asp:ListItem Value="2">F - COMPAR SPA.</asp:ListItem>
                                            <asp:ListItem Value="3">F - EUROPE</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </asp:Panel>

                            <div class="row" style="padding-top: 10px">

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblCountry" runat="server" CssClass="small">Country</asp:Label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="chzn-select form-control form-control-sm px-0" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" CssClass="small">Port of Loading</asp:Label>
                                        <asp:DropDownList ID="ddlPortLoad" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label20" runat="server" CssClass="small">Port of Discharge</asp:Label>
                                        <asp:DropDownList ID="ddlPortDis" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <asp:Label ID="lblExpRefNo" runat="server" CssClass="small">Exporter's Ref. No.</asp:Label>
                                    <asp:TextBox ID="txtExpRefNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <asp:Label ID="lblExpRefDt" runat="server" CssClass="small">Exporter's Ref. Date</asp:Label>
                                    <asp:TextBox ID="txtExpRefDt" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtExpRefDt_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtExpRefDt"></cc1:CalendarExtender>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label21" runat="server" CssClass="small">Less Discount %</asp:Label>
                                        <asp:TextBox ID="txtDisPer" runat="server" CssClass="form-control form-control-sm ">0</asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="small">Total Discount</asp:Label>
                                        <asp:TextBox ID="txtDis" runat="server" Style="text-align: right" CssClass="form-control form-control-sm "></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label22" runat="server" CssClass="small">Grand Total</asp:Label>
                                        <asp:TextBox ID="txtGTotal" runat="server" Style="text-align: right" CssClass="form-control form-control-sm "></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblproforma" runat="server" CssClass="small">Proforma Invoice No</asp:Label>
                                        <asp:TextBox ID="txtProforma" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="small">Description of Goods</asp:Label>
                                        <asp:DropDownList ID="ddlDesGrp" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblCrtnRmrks" runat="server" CssClass="small">Total Carton</asp:Label>
                                        <asp:TextBox ID="txtCrtnNo" runat="server" CssClass="form-control form-control-sm" Rows="3"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblGweight" runat="server" CssClass="small">Gross Weight (KGS)</asp:Label>
                                        <asp:TextBox ID="txtGweight" runat="server" CssClass="form-control form-control-sm" Rows="3"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblNweight" runat="server" CssClass="small">Net Weight (KGS)</asp:Label>
                                        <asp:TextBox ID="txtNweight" runat="server" CssClass="form-control form-control-sm" Rows="3"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblNotify" runat="server" CssClass="small">NOTIFY</asp:Label>
                                        <asp:TextBox ID="txtnotify2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblReqNarr" runat="server" CssClass="small">Narration</asp:Label>
                                        <asp:TextBox ID="txtBillNarr" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>

            </div>

            <div class="col-md-3" style="display: none;">
                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text=" Shipment:"></asp:Label>
                <asp:DropDownList ID="DDLShipment" runat="server" CssClass=" inputTxt chzn-select" Style="width: 180px;"></asp:DropDownList>
            </div>

            <div class="form-group" id="export" runat="server" visible="false">
            </div>



            <div id="myModal" class="modal export" role="dialog">
                <div class="modal-dialog  ">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table mr-2"></span>Style Wise Order Details And Current Stock 
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
                                                    <div class="dropdown-menu">
                                                        <asp:HyperLink CssClass="dropdown-item" runat="server" ID="HypStockCheck">Stock Check</asp:HyperLink>

                                                        <div class="dropdown-divider"></div>
                                                        <a class="dropdown-item" href="#">Separated link</a>
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
