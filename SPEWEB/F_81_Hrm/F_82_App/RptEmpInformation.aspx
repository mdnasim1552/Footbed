<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptEmpInformation.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.RptEmpInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {
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
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblEmpStatus" runat="server">Emp. Status</asp:Label>
                                <asp:DropDownList ID="ddlEmpStatus" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblPageSize" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1100</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divEmp" runat="server" visible="false">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="label" OnClick="ibtnEmpListAllinfo_Click" Font-Underline="false" ToolTip="Search Employee">Employee &nbsp;<i class="fa fa-search"  aria-hidden="true"></i></asp:LinkButton>
                                <asp:DropDownList ID="ddlEmpNameAllInfo" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divFilter" runat="server" visible="false">
                        <div class="col-md-8 col-sm-8 col-lg-8">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnShow_Click">OK</asp:LinkButton>
                                <br />
                                <asp:CheckBox ID="ChckApplic" runat="server" CssClass=" checkbox" Text="Application Form" />
                                <asp:CheckBox ID="ChckPerInf" runat="server" CssClass="checkbox" Text="Personal Information" />
                                <asp:CheckBox ID="ChckAppint" runat="server" AutoPostBack="True" CssClass="checkbox" Text="Appointment Letter" />
                                <asp:CheckBox ID="ChckIdCard" runat="server" CssClass="checkbox" Text="Id Card" />
                                <asp:CheckBox ID="ChckNomeni" runat="server" CssClass="checkbox" Text="Nomine" />
                                <asp:CheckBox ID="chkRef" runat="server" CssClass="checkbox" Text="Reference" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="ViewServices" runat="server">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Date</asp:Label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="label" OnClick="ibtnEmpList_Click">Employee</asp:LinkButton>
                                        <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvempservices" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="678px">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescription" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descrip")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDate" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "date")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvComp" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSection" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Increment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvIncSalary" runat="server" Font-Size="11PX" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incrsal")).ToString("#, ##0;(#, ##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSalary" runat="server" Font-Size="11PX"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tosalary")).ToString("#, ##0;(#, ##0); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRemrks" runat="server" Font-Size="11PX"
                                                    Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
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
                        <asp:View ID="ViewEmpInformation" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvempinfo" runat="server" PageSize="15" OnRowDataBound="gvempinfo_RowDataBound"
                                    ShowFooter="True" Style="margin-right: 0px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmdddCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvgph" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                        <asp:View ID="EmpDynamicInfo" runat="server">
                            <div class="row">
                                <div class="col-md-1 col-sm-1 col-lg-1">
                                    <div class="form-group mb-0">
                                        <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" CssClass=" checkbox" OnCheckedChanged="chkall_CheckedChanged" Text="Check All" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:CheckBoxList ID="cblEmployee" runat="server" AutoPostBack="True"
                                            CellPadding="2" CssClass="rbtnList1 checkbox small"
                                            Width="100%"
                                            ForeColor="#000" Height="12px"
                                            OnSelectedIndexChanged="cblEmployee_SelectedIndexChanged" RepeatColumns="10"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem>aa</asp:ListItem>
                                            <asp:ListItem>bb</asp:ListItem>
                                            <asp:ListItem>bb</asp:ListItem>
                                            <asp:ListItem>bb</asp:ListItem>
                                            <asp:ListItem>cc</asp:ListItem>
                                            <asp:ListItem>dd</asp:ListItem>
                                            <asp:ListItem>ee</asp:ListItem>
                                            <asp:ListItem>ff</asp:ListItem>
                                            <asp:ListItem>gg</asp:ListItem>
                                            <asp:ListItem>hh</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblSearchlist" runat="server" CssClass="label">Search List:</asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlFieldList1" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlFieldList2" runat="server" OnSelectedIndexChanged="ddlFieldList2_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control form-control-sm">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlFieldList3" runat="server" OnSelectedIndexChanged="ddlFieldList3_SelectedIndexChanged" AutoPostBack="true" CssClass="chzn-select form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlSrch1" runat="server" Width="90px" CssClass=" chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSrch2" runat="server" OnSelectedIndexChanged="ddlSrch2_SelectedIndexChanged" Width="90px" CssClass=" chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSrch3" runat="server" Width="90px" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlSrch3_SelectedIndexChanged" CssClass=" chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="like">Like</asp:ListItem>
                                            <asp:ListItem Value="=">Equal</asp:ListItem>
                                            <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                            <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                            <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                            <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSearch1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:TextBox ID="txtSearch2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:TextBox ID="txtSearch3" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:TextBox ID="txttoSearch1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:TextBox ID="txttoSearch2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <asp:TextBox ID="txttoSearch3" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlOperator1" runat="server" CssClass="chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlOperator2" runat="server" CssClass="chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddldesig01" runat="server" CssClass=" chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="and">And</asp:ListItem>
                                            <asp:ListItem Value="or">Or</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblOrderList" runat="server" CssClass="label">Order Field:</asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlOrder1" runat="server" CssClass="chzn-select form-control form-control-sm" Width="150px">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlOrder2" runat="server" CssClass="chzn-select form-control form-control-sm" Width="150px">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlOrder3" runat="server" CssClass="chzn-select form-control form-control-sm" Width="150px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlOrderad1" runat="server"
                                            CssClass="chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="asc">Asc</asp:ListItem>
                                            <asp:ListItem Value="desc">Des</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlOrderad2" runat="server"
                                            CssClass="chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="asc">Asc</asp:ListItem>
                                            <asp:ListItem Value="desc">Des</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlOrderad3" runat="server"
                                            CssClass="chzn-select form-control form-control-sm">
                                            <asp:ListItem Value="asc">Asc</asp:ListItem>
                                            <asp:ListItem Value="desc">Des</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnEmpDyInfo" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnEmpDyInfo_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-2 pading5px asitCol2">
                                    <asp:Label ID="lbland1" runat="server" CssClass="lblTxt lblName" Text="And" Visible="False"
                                        Width="25px"></asp:Label>
                                    <asp:DropDownList ID="ddltodesig1" runat="server" CssClass=" ddlPage62 inputTxt">
                                    </asp:DropDownList>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="ddldesig02" runat="server"
                                        CssClass="ddlPage62 inputTxt">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbland2" runat="server" Text="And" Visible="False"
                                        CssClass="lblTxt lblName"></asp:Label>
                                    <asp:DropDownList ID="ddltodesig2" runat="server" CssClass=" ddlPage62 inputTxt">
                                    </asp:DropDownList>
                                    <asp:Label ID="lbland3" runat="server" Text="And" Visible="False"
                                        CssClass="lblTxt lblName"></asp:Label>
                                    <asp:DropDownList ID="ddltodesig3" runat="server" CssClass=" ddlPage62 inputTxt">
                                    </asp:DropDownList>
                                    <div class="clearfix"></div>
                                    <asp:DropDownList ID="ddldesig03" runat="server" CssClass="ddlPage62 inputTxt">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvempDyInfo" runat="server" PageSize="15" AllowPaging="true" OnPageIndexChanging="gvempDyInfo_PageIndexChanging"
                                    ShowFooter="True" Style="margin-right: 0px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSLNO" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Employee Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEmpType" runat="server" Style="text-align: Left" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "emptype")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBrnCode" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brcode")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBrnName" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSecID" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvSec" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Card">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvCard" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Emp Name">                                           
                                             <HeaderTemplate>
                                                <asp:Label ID="lblgvEmp" runat="server" Font-Bold="true" Text="Emp Name " Width="90px"></asp:Label>
                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export To Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lgvEname" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDesID" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desigid")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDes" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Joining Date">

                                            <ItemTemplate>
                                                <asp:Label ID="lgvjDate" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "joindate1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Birth Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBDate" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "birthdate")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Blood Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBlg" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bloodgroup")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" Width="30" />

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsal" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supervisor")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gross Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsal2" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grspay")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Salary">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsal3" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "salary")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Per. District">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperdistrict" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "perdistrict"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Per. Upazila">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperupazila" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "perupazila"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Per. Post">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvperpost" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "perpost"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Per. Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpervill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pervill"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre. District">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpredistrict" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "predistrict"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre. Upazila">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvpreupazila" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preupazila"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre. Post">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprepost" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prepost"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pre. Village">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprevill" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "previll"))%>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvshiftname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shiftname"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvservice" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "servicel"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Increment Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvincrdate" runat="server"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "incrdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "incrdate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Increment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvincramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incramt")).ToString(" #,##0.00; (#,##0.00); ")%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grade">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvGrade" runat="server" Style="text-align: Left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Grade")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Line">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvempline" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc"))%>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Primary AC No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprimaryacno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "primaryacno"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Secondary AC No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvsecondaryacno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secondaryacno"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Bus Location">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbuslocation" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buslocation"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Father's Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfathername" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fathername"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mother's Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvmothername" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mothername"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spouse Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvspousename" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spousename"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gender">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgender" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gender"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NID">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvnid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nid"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Birth Certificate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvbirthcertif" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "birthcertf"))%>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Religion">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvreligion" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "religion"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcontactno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "contactno"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Educational Qualification">
                                            <ItemTemplate>
                                                <asp:Label ID="lgveduqualification" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "eduqualification"))%>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Promotion Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvprodate" runat="server"
                                                    Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy")=="01-Jan-1900")? "" :
                                                        Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy")%>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerSettings Mode="NumericFirstLast" Position="Top" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                </asp:GridView>
                            </div>
                            <asp:Panel ID="Panel6" runat="server" Visible="false">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                                <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            </div>
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control inputTxt" TabIndex="2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </asp:Panel>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

