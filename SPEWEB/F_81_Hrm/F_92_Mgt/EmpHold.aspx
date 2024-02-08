<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpHold.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.EmpHold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });

        };
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
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lbldate" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <asp:Panel ID="PnlSub" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label">From</asp:Label>
                                    <asp:TextBox ID="txtfrmDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfrmDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="label">To</asp:Label>
                                    <asp:TextBox ID="txttoDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttoDate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttoDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="lblEmpList" runat="server">Employee List                                        
                                    <asp:LinkButton ID="imgbtnEmployee" runat="server" CssClass="label" OnClick="imgbtnEmployee_Click" ToolTip="Get Employee"><i class="fa fa-search"></i></asp:LinkButton>
                                    </asp:Label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkbtnAdd" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnAdd_Click">Add</asp:LinkButton>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>

                    <div class="row">
                        <asp:GridView ID="gvemphold" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="464px" OnRowDeleting="gvemphold_RowDeleting">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #">
                                    <ItemTemplate>
                                        <asp:Label ID="SLNO" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvidcardno" runat="server" Font-Size="11PX"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempname" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpredesig" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="From">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvfrmdate" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "frmdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="lblgvfrmdate_CalendarExtender" runat="server"
                                            Enabled="True" TargetControlID="lblgvfrmdate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="To">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvtodate" runat="server" Font-Size="11PX"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "todate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="lblgvtodate_CalendarExtender" runat="server"
                                            Enabled="True" TargetControlID="lblgvtodate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

