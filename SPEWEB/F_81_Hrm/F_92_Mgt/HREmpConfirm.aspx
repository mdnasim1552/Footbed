<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpConfirm.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.HREmpConfirm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function FnDanger() {
            $.toaster('Sorry No Data Found of this Section', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');

        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
            $('[id*=ddlEmpName]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 300,
                enableCaseInsensitiveFiltering: true,

            });
        }
        function SetTarget(type) {
            window.open('../../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
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
                            <asp:Label ID="Label2" runat="server" CssClass="label">Employee Type</asp:Label>
                            <asp:DropDownList ID="ddlWstation" runat="server" Width="100%" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server" CssClass="label">Division</asp:Label>
                            <asp:DropDownList ID="ddlDivision" runat="server" Width="100%" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                            <asp:DropDownList ID="ddlDept" runat="server" Width="100%" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label8" runat="server" CssClass="label">Section</asp:Label>
                            <asp:DropDownList ID="ddlSection" runat="server" Width="100%" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="label">Date</asp:Label>
                            <asp:TextBox ID="txtfrmdate" runat="server" Width="100%" CssClass="form-control form-control-sm "></asp:TextBox>
                            <cc1:CalendarExtender ID="txtfrmdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbltodate" runat="server" CssClass="label">To</asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" Width="100%" CssClass="form-control form-control-sm "></asp:TextBox>
                            <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1" style="margin-top: 20px">
                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblEmpName" runat="server" CssClass="label">Employee  Name                                    
                                <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="label" OnClick="imgbtnEmployee_Click" ToolTip="Get Employee"><i class="fas fa-search"></i></asp:LinkButton>
                            </asp:Label>
                            <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px;">
                                <asp:ListBox ID="ddlEmpName" runat="server" CssClass="form-control form-control-sm multiselect-search" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <%--  <div class="col-md-2 col-sm-2 col-lg-2" style="margin-top: 30px">
                            <asp:CheckBox ID="chkPomOnly" Text="Only Confirmation Letter" runat="server" />
                        </div>--%>
                        <div class="col-md-2" style="margin-top: 20px">
                            <asp:LinkButton ID="btnPrintAll" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btnPrintAll_Click" ToolTip="Print Confirmation Letter">Confirmation Letter</asp:LinkButton>
                        </div>
                        <div class="col-sm-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 450px">
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:GridView ID="dgvEmpCon" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AllowPaging="true" OnPageIndexChanging="dgvEmpCon_PageIndexChanging"
                            ShowFooter="True">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvIDCard" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cardno")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEmID" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name & Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvEmpNam" runat="server" Font-Size="12px"
                                            Text='<%# "<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+
                                                                        "<br>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining Date">

                                    <ItemTemplate>
                                        <asp:Label ID="lgvJoindat" runat="server" Font-Size="12px" Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Confirm Date">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Font-Size="12px" Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "condat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRem" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="12px" Style="text-align: Left; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerSettings Mode="NumericFirstLast" Position="Top" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />

                            <EmptyDataTemplate>
                                <div style="color: red; width: 1278px; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                            </EmptyDataTemplate>

                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

