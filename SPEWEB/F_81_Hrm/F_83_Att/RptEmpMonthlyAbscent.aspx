<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptEmpMonthlyAbscent.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptEmpMonthlyAbscent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .multiselect {
            width: 270px !important;
            text-wrap: initial !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }

        .caret {
            display: none !important;
        }

        .multiselect-text {
            width: 100px !important;
        }

        .multiselect-container {
            width: 300px !important;
            overflow-y: scroll !important;
        }

        span.multiselect-selected-text {
            width: 200px !important;
        }

        .input-group .form-control:not(:first-child):not(:last-child) {
            height: 32px !important;
        }

        .input-group-btn:last-child > .btn {
            height: 32px !important;
        }

        table tr th {
            text-align: center;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });


            $('[id*=ddlEmpNameAllInfo]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 200,
                enableCaseInsensitiveFiltering: true,

            });
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                                <asp:Label ID="Label5" runat="server" CssClass="control-label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="control-label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="control-label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-contro form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="control-label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblLine" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblLocation" runat="server" CssClass="label">Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="label" Width="68">From</asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <asp:Label ID="lbltodate" runat="server" CssClass="label">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass=" form-control form-control-sm" AutoCompleteType="Disabled"></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <label class="control-label" for="lblreportlevel">Page Size</label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2" id="divEmpStatus" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblEmpStatus" runat="server" CssClass="label">Emp. Status</asp:Label>
                                <asp:DropDownList ID="ddlEmpStatus" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
                                    <asp:ListItem Value="2">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblEmployee" runat="server" CssClass="label">Employee                                    
                            <asp:LinkButton ID="ibtnEmpListAllinfo" runat="server" CssClass="label" OnClick="ibtnEmpListAllinfo_Click" ToolTip="Get Employee"><i class="fas fa-search"></i></asp:LinkButton>
                                </asp:Label>
                                <asp:ListBox ID="ddlEmpNameAllInfo" runat="server" CssClass="form-control form-control-sm" Style="min-width: 200px !important;" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-left: 90px; margin-top: -8px;">
                            <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 29px;" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>



                        <div class="form-group" style="display: none;">
                            <label class="control-label" for="lblreportlevel">Company</label>
                            <span style="display: none;">
                                <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="form-control"></asp:TextBox>
                            </span>
                            <%--<asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>
                            <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control" Font-Size="12px" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2" style="display: none;">
                            <div class="form-group">
                                <label class="control-label" for="lblreportlevel">Department</label>
                                <span style="display: none;">
                                    <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>

                                    <asp:LinkButton ID="imgbtnProSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnProSrch_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </span>

                                <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control" TabIndex="2">
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>
                    <div class="col-md-1" style="display: none;">
                        <div class="form-group">
                            <label class="control-label lblmargin-top9px" for="lblreportlevel">Section</label>
                        </div>
                    </div>
                    <div class="col-md-2" style="display: none;">
                        <div class="form-group">
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="233" CssClass="form-control" TabIndex="2">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <span style="display: none;">
                            <label id="chkbod" runat="server" class="switch">
                                <asp:CheckBox ID="chkzerobal" runat="server" AutoPostBack="true" />
                                <span class="btn btn-xs slider round"></span>
                            </label>
                            <asp:Label runat="server" Text="Without Zero" ID="lblnetbalance" CssClass="control-label"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height:450px;">
                    <div class="table-responsive">
                        <asp:GridView ID="gvMonthWiseRpt" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="15"
                            OnPageIndexChanging="gvMonthWiseRpt_PageIndexChanging" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">

                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>                                   
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvidcard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">

                                    <HeaderTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="style58" style="width: auto">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Employee Name"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:HyperLink ID="hlbtnbnkpdataExel" CssClass="btn btn-xs btn-success" runat="server" ToolTip="Export to Excel">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>

                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                    <%--<FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnFUpLeave" runat="server"  CssClass="btn  btn-danger btn-xs" OnClick="lnkbtnFUpLeave_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>--%>

                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesig" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Absent Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvworkingday" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "workingday")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
