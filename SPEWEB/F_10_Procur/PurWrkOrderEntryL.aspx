<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurWrkOrderEntryL.aspx.cs" Inherits="SPEWEB.F_10_Procur.PurWrkOrderEntryL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function SpcfChangModal() {
            $('#SpecificationModal').modal('toggle');
        }
        function CLoseMOdal() {
            $('#SpecificationModal').modal('hide');
        }
        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chkbox input {
            margin-left: 5px !important;
            margin-right: 5px !important;
            margin-top: 2px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Order Date: </asp:Label>
                                <asp:TextBox ID="txtCurOrderDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                <cc1:CalendarExtender ID="txtCurOrderDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurOrderDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label10" runat="server" CssClass="label"> Order No</asp:Label>
                            <div class="form-inline">
                                <asp:Label ID="lblCurOrderNo1" Width="40%" runat="server" CssClass="form-control form-control-sm">POR00- </asp:Label>
                                <asp:TextBox ID="txtCurOrderNo2" Width="60%" runat="server" CssClass="form-control form-control-sm ">00000</asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label15" runat="server" CssClass="label"> Ref. No</asp:Label>

                                <asp:TextBox ID="txtOrderRefNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                <asp:Label ID="lblSyspo" Style="display: none" runat="server" CssClass="label"></asp:Label>
                                <asp:Label ID="lblSyspocustom" Style="display: none" runat="server" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevOrderList" runat="server" CssClass="label" OnClick="lbtnPrevOrderList_Click"
                                    TabIndex="3">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevOrderList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="Label26" runat="server" CssClass="">Print Type</asp:Label>
                            <asp:DropDownList ID="ddlReportLevel" runat="server" AutoPostBack="true" CssClass="chzn-select form-control form-control-sm">
                                <asp:ListItem Selected="True" Value="0">None</asp:ListItem>
                                <asp:ListItem Value="1">Job Work</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                </div>

            </div>


            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="PurApp" runat="server">

                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 350px">
                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" CssClass="label">Supplier</asp:Label>
                                        <asp:TextBox ID="txtsrchSupplier" Visible="false" runat="server" CssClass=""></asp:TextBox>
                                        <asp:LinkButton ID="imgSearchOrderno" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchOrderno_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlSuplierList" runat="server" CssClass="form-control chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlSuplierList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-12">

                                    <asp:GridView ID="gvAprovInfo" runat="server"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="482px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkitem" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="1" %>'
                                                        Width="20px" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPrjCod11" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reqno" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqNo2" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCod2" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Spcfcod Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvspfcod02" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supl Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSupCod" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSuplDesc" runat="server"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "ssirdesc1").ToString() %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Store Name" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProjDesc0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projdesc1")) %>'
                                                        Width="220px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Materials">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResDesc0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                        Width="220px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnSelectedOrdr" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectedOrdr_Click">Selected Order</asp:LinkButton>
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpfDesc0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BOM No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBomNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRF">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrf" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PAP. NO" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPAPNo1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno1")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqNo3" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ref No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRefno" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvApprQty" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="55px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvNewApprovRate" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New Order Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvNewOrderAmt" runat="server" Font-Size="11px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSpcfCod0" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPayType0" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AppNo" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPAPNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno")) %>'
                                                        Width="50px"></asp:Label>
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

                </asp:View>
                <asp:View ID="WorkOrdr" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Supplier:"></asp:Label>

                                        <asp:TextBox ID="txtSupName" runat="server" class="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass=" label" Text="Store Name:"></asp:Label>

                                        <asp:Label ID="lblStoreName" runat="server" class="form-control form-control-sm" ReadOnly="true"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="Subject:"></asp:Label>
                                        <asp:TextBox ID="txtSubject" runat="server" class="form-control form-control-sm">Work Order For Materials</asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1 px-1">
                                    <div class="form-group">
                                        <asp:CheckBox runat="server" ID="chkSummary" Style="margin-top: 20px" CssClass="chkbox form-control form-control-sm px-0" Text="Summary" />
                                    </div>
                                </div>

                                <div class="col-md-2 ">
                                    <div class="form-group" style="margin-top: 20px">
                                        <asp:CheckBox ID="chkCharging" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkCharging_CheckedChanged" Text="Charging/Discount" CssClass="btn btn-sm checkBox" />
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Dear Sir,"></asp:Label>

                                        <asp:TextBox ID="txtLETDES" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" Rows="1" Style="width: 100%;" class=" ">Refer to your offer with specification dated on 15/02/2009 and subsequent discussion our management is pleased to issue work order for the following terms &amp; conditions</asp:TextBox>

                                    </div>
                                </div>
                                <asp:Panel ID="PnlCharging" runat="server" Visible="False" Width="600px">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" CssClass="label">Charge</asp:Label>
                                                <asp:TextBox ID="txtSrchCharge" runat="server" CssClass=" inputTxt inputName hidden inpPixedWidth"></asp:TextBox>



                                                <div class="colMdbtn hidden">
                                                    <asp:LinkButton ID="imgSearchCharge" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchCharge_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>
                                                <asp:DropDownList ID="ddlCharge" runat="server" Width="300" CssClass="form-control form-control-sm chzn-select">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group" style="margin-top: 20px">
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelect_Click">Select</asp:LinkButton>

                                            </div>

                                        </div>
                                        <div class="col-md-3">


                                            <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>


                                        </div>

                                        <div class="form-group hidden">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Store Name</asp:Label>
                                                <asp:TextBox ID="txtSrchProjectName" runat="server" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>


                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="imgSearchProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgSearchProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                </div>

                                            </div>

                                            <div class="col-md-4 pading5px ">
                                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control inputTxt chzn-select">
                                                </asp:DropDownList>

                                            </div>


                                        </div>

                                    </div>





                                </asp:Panel>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvOrderInfo" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                        CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvOrderInfo_RowDataBound" OnPageIndexChanging="gvOrderInfo_PageIndexChanging">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Project Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPrjCod" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reqno" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aprovno" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAprovNo" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCod" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="LblgvSpcfcod" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier Name" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSupDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Store Name" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProjDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "projdesc1")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>






                                            <asp:TemplateField HeaderText="Materials">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                        Width="220px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-sm " OnClick="lbtnDelete_Click">Delete</asp:LinkButton>

                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblgvSpfDesc10" runat="server" OnClick="lblgvSpfDesc10_Click"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="BOM No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBom" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRF">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMrf1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                        Width="90"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Req. No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvReqNo1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ref No." Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrefno1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Aprov.No.">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotal" runat="server"  CssClass="btn btn-sm btn-warning" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAprovNo1" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprovno1")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved Qty">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnUpdatePurOrder" Visible="false" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lbtnUpdatePurOrder_Click">Final Update</asp:LinkButton>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAprovQty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Qty.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvOrderQty" runat="server" BorderColor="#99CCFF"
                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                        Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvApprovRate" runat="server" BorderStyle="None" CssClass="bg-twitter"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aprovrate")).ToString("#,##0.000000;-#,##0.000000; ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvOrderAmt" runat="server" BackColor="Transparent" BorderStyle="none"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="60px" Font-Size="11px" Style="text-align: right"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFooterTOrderAmt" runat="server" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pay.Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPayType" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Selection">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtSelection" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "selection")) %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Crust/Finished">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DdlCstFinished" CssClass="form-control form-control-sm " runat="server">
                                                        <asp:ListItem Value="Crust">Crust</asp:ListItem>
                                                        <asp:ListItem Value="Finished">Finished</asp:ListItem>
                                                        <asp:ListItem Value="">None</asp:ListItem>
                                                    </asp:DropDownList>
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
                                <div class="col-md-1 ">
                                    <div class="form-group" style="margin-top: 20px">
                                        <asp:LinkButton ID="btnSendmail" CssClass="btn btn-success btn-sm" runat="server" OnClick="btnSendmail_Click">Send Email</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblSupMail" runat="server" CssClass="smLbl_to"></asp:Label>
                                        <asp:Label ID="lblFrmEmial" runat="server" CssClass="label"> Email From: </asp:Label>
                                        <asp:TextBox ID="txtSenderMail" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="label">Pass</asp:Label>
                                        <asp:TextBox ID="txtPass" runat="server" class="form-control form-control-sm"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Advanced"></asp:Label>
                                        <asp:TextBox ID="txtadvAmt" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" CssClass="label" Text="Deliver To:"></asp:Label>
                                        <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label25" runat="server" CssClass="label"> Shiping Terms:</asp:Label>
                                        <asp:DropDownList ID="DDLIncoTerms" runat="server" CssClass="form-control form-control-sm chzn-select chzn-single">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label35" runat="server" CssClass="label">Mode of Payment:</asp:Label>
                                        <asp:DropDownList ID="DdlModeOfPayment" runat="server" CssClass="chzn-single form-control form-control-sm chzn-select"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label36" runat="server" CssClass="label">Shipping Mode</asp:Label>
                                        <asp:DropDownList ID="ddlShipMode" runat="server" CssClass="chzn-single form-control form-control-sm chzn-select"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label16" runat="server" CssClass="" Text="Partial Payment:"></asp:Label>
                                        <asp:DropDownList ID="parpayType" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm" AutoPostBack="true" TabIndex="2">
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" CssClass="" Text="Partial Delivery:"></asp:Label>
                                        <asp:DropDownList ID="ddlpardelivery" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm" AutoPostBack="true" TabIndex="2">
                                            <asp:ListItem Value="Allowed">Allowed</asp:ListItem>
                                            <asp:ListItem Value="Not Allowed">Not Allowed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label18" runat="server" CssClass="" Text="Delivery Date:"></asp:Label>
                                        <asp:TextBox ID="TxtDateDelivery" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtDateDelivery"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblSlctReq" runat="server" CssClass="" Text="Select Requisition"></asp:Label>
                                        <div class="row">
                                            <div class="col-9">
                                                <asp:DropDownList ID="ddlSlctReq" runat="server" CssClass="chzn-select chzn-single form-control form-control-sm"></asp:DropDownList>
                                            </div>
                                            <div class="col-3">
                                                <asp:LinkButton ID="btnMerge" CssClass="btn btn-sm btn-success" runat="server" OnClientClick="return confirm('Do you want to push item in this Purchase Order')" Text="Push Requistion" OnClick="btnMerge_Click" TabIndex="1"><span class="fa fa-link"></span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:Panel ID="pnlsch" runat="server">
                                <div class="form-horizontal">

                                    <asp:Panel ID="pnlschgenerate" runat="server" Visible="False">
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px ">
                                                <div class="input-group">
                                                    <span class="input-group-addon glypingraddon">
                                                        <asp:Label ID="Label14" runat="server" CssClass="lblTxt lblName" Text="Total Installement:"></asp:Label>
                                                    </span>
                                                    <asp:TextBox ID="txtTInstall" runat="server" class="form-control inputTxt"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>

                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-group" style="display: none;">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lPays1" runat="server" CssClass="lblTxt lblName" Text="Payment Shedule"></asp:Label>
                                                </span>
                                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True"
                                                    CssClass="style22" Font-Bold="True" ForeColor="Black"
                                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment" />
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </asp:Panel>

                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                OnRowDeleting="gvPayment_RowDeleting" ShowFooter="True" Width="223px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" ForeColor="Black"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvschcode" runat="server" ForeColor="Black" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inscode")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                    <asp:TemplateField HeaderText="Description of Item">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvschdesc" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "insdesc")) %>'
                                                Width="120px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDate" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Height="20px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "insdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd-MMM-yyyy" TargetControlID="txtgvDate"></cc1:CalendarExtender>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lTotalPayment" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lTotalPayment_Click">Total Payment</asp:LinkButton>


                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "insamt")).ToString("#,##0;-#,##0; ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvfschAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="White" Style="text-align: right" Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay Time">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvschrmrks" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvschrmrks02" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks02")) %>'
                                                Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                            <div class="clearfix"></div>
                            <div class="col-md-12">
                                <div class="log-divider" id="lblcharging" runat="server">
                                    <span>
                                        <i class="fa fa-fw fa-dollar-sign"></i>Terms and Condition Information</span>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:Panel ID="PanelOther" runat="server">

                                    <asp:Label ID="lssircode" runat="server" Visible="False"></asp:Label></td>

                                        <div class="row">
                                            <div class="col-md-8">


                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvOrderTerms" runat="server" AllowPaging="True"
                                                        AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                                        CssClass=" table-striped table-hover table-bordered grvContentarea">
                                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                            Mode="NumericFirstLast" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvTermsID" runat="server" Height="16px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termsid")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvSubject" runat="server" BorderColor="#99CCFF"
                                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                        Style="text-align: left; background-color: Transparent"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>'
                                                                        Width="150px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColon" runat="server" Font-Bold="true" Font-Size="16px"
                                                                        Text=" : "></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvDesc" runat="server" BorderColor="#99CCFF"
                                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                        Style="text-align: left; background-color: Transparent"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>'
                                                                        Width="630px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                                                        BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                        Style="text-align: left; background-color: Transparent"
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>'
                                                                        Width="100px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#F5F5F5" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                    </asp:GridView>

                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="label" Text="Special Note / Narration:"></asp:Label>

                                                    <asp:TextBox ID="txtOrderNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="lblRemarks" runat="server" CssClass="label" Text="Remarks:"></asp:Label>

                                                    <asp:TextBox ID="txtRemarks2" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    <div class="form-group" style="display: none;">
                                        <div class="col-md-2 pading5px asitCol2 ">
                                            <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By: "></asp:Label>
                                            <asp:TextBox ID="txtPreparedBy" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        </div>
                                        <div class="col-md-6 pading5px">

                                            <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:"></asp:Label>
                                            <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                            <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approval Date: "></asp:Label>
                                            <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="inputtextbox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtApprovalDateCalendarExtender2" runat="server"
                                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtApprovalDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    </div>
                </asp:View>
            </asp:MultiView>


            <div id="SpecificationModal" class="modal animated slideInLeft" role="dialog" aria-labelledby="ModalHead">
                <div class="modal-dialog">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <asp:Label ID="ModalHead" runat="server" class="modal-title h5 font-weight-bold">Material List</asp:Label>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body form-horizontal">

                            <asp:Label ID="lblHelper" runat="server" Visible="false"></asp:Label>
                            <div class="form-group">
                                <label class="col-form-label">Specifications</label>
                                <div class="row justify-content-between pr-2">
                                    <div class="col-9">
                                        <asp:DropDownList ID="ddlSpecification" CssClass="form-control form-control-sm" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1">
                                        <a class="btn btn-success btn-xs" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
                                            <i class="fa fa-plus"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="collapse" id="collapseExample">
                                    <div class="card card-body">
                                        <div class="row">
                                            <div class="col-md-6 form-group">
                                                <label class="col-form-label">Thikness/Size</label>
                                                <asp:TextBox ID="TxtThikness" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label class="col-form-label">Width/Length</label>
                                                <asp:TextBox ID="txtlength" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row mt-n3">
                                            <div class="col-md-6 form-group">
                                                <label class="col-form-label">Color</label>
                                                <asp:DropDownList ID="ddlModalColor" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-6 form-group">
                                                <label class="col-form-label">Brand</label>
                                                <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row mt-n3">
                                            <div class="col-md-6 form-group">
                                                <label class="col-form-label">Remarks</label>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>



                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="LbtnChngSpcf" runat="server" OnClick="LbtnChngSpcf_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
