<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpTransfer.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_87_Tra.HREmpTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .log-divider {
            margin: 0.25rem 0;
        }
    </style>
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Transfer Date</asp:Label>
                                <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurTransDate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtCurTransDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="trnsferNo" runat="server" CssClass="label">Transfer. No.</asp:Label>
                                <asp:Label ID="lblCurTransNo1" runat="server" CssClass=" smLbl_to"></asp:Label>
                                <asp:TextBox ID="txtCurTransNo2" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblConTrolCode" runat="server" CssClass="control-label">Search By</asp:Label>
                                <div class="input-group input-group-alt">
                                    <asp:TextBox runat="server" ID="TxtSearch" CssClass="form-control form-control-sm" placeholder="Card/Name">
                                    </asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="ImgbtnFindComp" CssClass="input-group-text" runat="server" OnClick="ImgbtnFindComp_Click" ToolTip="Search Employee" Font-Underline="false">
                                            <span class="fa fa-search"> </span></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divPrevList" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblPrevTrnsList" runat="server">Previous List                                    
                                <asp:LinkButton ID="lbtnPrevTransList" runat="server" CssClass="control-label" OnClick="lbtnPrevTransList_Click1" ToolTip="Get Previous Transfer List"><span class="fa fa-search"></span></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlPrevISSList" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnlCompany" runat="server" CssClass="well padingRight5px" Visible="False">
                        <div class="log-divider" id="lblFCom" runat="server">
                            <span>
                                <i class="fa fa-fw fa-dollar-sign"></i>From Company</span>
                        </div>
                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass="label">Employee Type</asp:Label>
                                    <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="label">Division</asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" CssClass="label">Department</asp:Label>
                                    <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" CssClass="label">Section</asp:Label>
                                    <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblEmpList" runat="server">Employee List                                    
                                    <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="label" OnClick="ibtnEmpList_Click" ToolTip="Get Employee"><span class="fa fa-search"></span></asp:LinkButton>
                                    </asp:Label>
                                    <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="lbldeg" runat="server" CssClass="label">Designation</asp:Label>
                                    <asp:TextBox ID="txtEmpDesignation" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlToCompany" runat="server" CssClass="well padingLeft5px" Visible="False">
                        <div class="log-divider" id="lblTCom" runat="server">
                            <span>
                                <i class="fa fa-fw fa-dollar-sign"></i>To Company</span>
                        </div>

                        <div class="row">
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="label">Employee Type</asp:Label>
                                    <asp:DropDownList ID="ddlWstation1" runat="server" OnSelectedIndexChanged="ddlWstation1_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" CssClass="label">Division</asp:Label>
                                    <asp:DropDownList ID="ddlDivision1" runat="server" OnSelectedIndexChanged="ddlDivision1_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" CssClass="label">Department</asp:Label>
                                    <asp:DropDownList ID="ddlDept1" runat="server" OnSelectedIndexChanged="ddlDept1_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" CssClass="label">Section</asp:Label>
                                    <asp:DropDownList ID="ddlSection1" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblsign" runat="server" CssClass="smLbl_to">Signatory</asp:Label>
                                    <asp:DropDownList ID="ddlsign" runat="server" CssClass="form-control form-control-sm chzn-select "></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblPrsntDate" runat="server" CssClass="label">Present Date</asp:Label>
                                    <asp:TextBox ID="txtpatplacedate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtpatplacedate_CalendarExtender" runat="server" Format="dd.MM.yyyy" TargetControlID="txtpatplacedate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblDesig" runat="server" CssClass="label">Designation</asp:Label>
                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                    <asp:Label ID="lblLine" runat="server" CssClass="label">Line</asp:Label>
                                    <asp:DropDownList ID="ddlEmpLine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkselect" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkselect_Click">Select</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 300px;">
                    <div class="table-responsive">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="910px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL#">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnDelete" OnClick="lnkbtnDelete_Click" CssClass="text-red" runat="server" ToolTip="Delete Transfer Data"><span class="fa fa-trash"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvidcardno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="50px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="145px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>                                    
                                </asp:TemplateField>                                

                                <asp:TemplateField HeaderText="From Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfcomdesc")) %>'
                                            Width="130px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="To Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtCompany" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttcomdesc")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="From Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfprjdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tfsecdesc")) %>'
                                            Width="140px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                              

                                <asp:TemplateField HeaderText="To Section">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttprjdesc" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttsecdesc")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="From Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesig" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="100px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttdesig" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttdesig")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="From Line">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvtfline" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tflinedesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="To Line">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvttline" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ttlinedesc")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present At Place">
                                    <ItemTemplate>
                                        <asp:Label ID="txtpatplace" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "pplacedate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" Font-Size="11PX"></asp:Label>
                                    </ItemTemplate>

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
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmrks")) %>'
                                            Width="100px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:GridView>
                    </div>
                    <asp:Panel ID="pnlremarks" runat="server" Visible="False">
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label">Information of Finalcial Matters</asp:Label>
                                    <asp:TextBox ID="txtfmaters" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="label">Special Note</asp:Label>
                                    <asp:TextBox ID="txtspnote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

