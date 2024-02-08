<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpPro.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.EmpPro" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../../Scripts/ScrollableGridPlugin.js"></script>
    <script type="text/javascript" language="javascript" src="../../Scripts/KeyPress.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

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
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-1-half col-md-1-half col-lg-1-half">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Promotion No</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm "></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server" CssClass=" form-control form-control-sm "
                                        TabIndex="2"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click"
                                    TabIndex="3">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Panel ID="pnlExcel" Visible="false" runat="server">
                                    <asp:Label ID="lblExel" runat="server" CssClass="label">Upload Excel</asp:Label><br />
                                    <div class="btn btn-secondary btn-sm fileinput-button">
                                        <i class="fa fa-plus fa-fw"></i>
                                        <span>Add files...</span>
                                        <asp:FileUpload ID="fileuploadExcel" runat="server" Height="26px" onchange="submitform();" />
                                    </div>
                                    <asp:LinkButton ID="btnExcelDataUpgrade" runat="server" CssClass="btn btn-sm btn-success" Text="ADJUST" OnClick="btnExcelDataUpgrade_Click"></asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevProList" runat="server" OnClick="lbtnPrevProList_Click" CssClass="label" Font-Underline="false" ToolTip="Get Previous Promotion List">Prev. Pro List</asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevProList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>


                    </div>
                    <asp:Panel ID="pnlprj" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" CssClass="label">Employee Type</asp:Label>
                                    <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <asp:Label ID="Label9" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                    <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblResList" runat="server" CssClass="label">Employee List</asp:Label>
                                    <div class="input-group input-group-sm input-group-alt">
                                        <div class="input-group-prepend">
                                            <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="input-group-text" OnClick="ibtnEmpList_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                        </div>
                                        <asp:DropDownList ID="ddlEmpList" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged" AutoPostBack="True" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblDesig" runat="server" CssClass="label" ForeColor="Blue" Visible="false"></asp:Label>
                                    <asp:LinkButton ID="Label10" OnClick="Label10_Click" runat="server" CssClass="label" Font-Underline="false" ToolTip="Get Designation">Present Desig. </asp:LinkButton>
                                    <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control form-control-sm chzn-select " OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" CssClass="label">Revised Desig.</asp:Label>
                                    <asp:TextBox ID="txtsrchDesg" runat="server" CssClass="form-control form-control-sm " Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="ibtnDesg" runat="server" CssClass="btn btn-primary  hidden srearchBtn" Visible="false" OnClick="ibtnDesg_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    <asp:DropDownList ID="ddlDesig" runat="server" Width="200" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblsign" runat="server" CssClass="smLbl_to">Signatory</asp:Label>
                                    <asp:DropDownList ID="ddlsign" runat="server" CssClass="form-control form-control-sm chzn-select "></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkselect_Click">Select</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px">
                    <div class="table-responsive">
                        <asp:GridView ID="gvremppro" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="818px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnDelete" OnClick="lnkbtnDelete_Click" CssClass="text-red" ToolTip="Delete Promotion" runat="server"><span class="fa fa-trash"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtprjdesc" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvidcardno" runat="server" Font-Size="11PX"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pdesig")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revised Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrevdesig" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rdesig")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Signatory">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsempname" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sempname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="row mt-2">
                        <asp:Panel ID="PnlProRemarks" runat="server" Visible="False">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass="label">Remarks</asp:Label>
                                    <asp:TextBox ID="txtRemarks" runat="server"
                                        TextMode="MultiLine" CssClass="form-control" Columns="50" TabIndex="17"></asp:TextBox>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


