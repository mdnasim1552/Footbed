<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMonAttenSummary.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.RptMonAttenSummary" %>

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
                                <asp:Label ID="Label2" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2" id="divLine" runat="server">
                            <div class="form-group">
                                <asp:Label ID="lblline" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="label" Width="68">Date</asp:Label>
                                <div class="form-inline">
                                    <asp:TextBox ID="txtfromdate" runat="server" CssClass=" form-control form-control-sm small" Width="45%" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>

                                    <asp:Label ID="lbltodate" runat="server" CssClass=" label">To</asp:Label>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm small" Width="45%" AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-half col-sm-half col-lg-half ml-2">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label ">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"
                                    Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">
                    <div class="row">
                        <asp:MultiView ID="MultiView1" runat="server">
                            <asp:View ID="ViewMonAttnSumDayWise" runat="server">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvMonAttnCountSum" runat="server" AllowPaging="True" OnPageIndexChanging="gvMonAttnCountSum_PageIndexChanging"
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAlNos2IndOvrSum" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <table style="width: 150px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <label runat="server" backcolor="#000066"
                                                                    bordercolor="White" borderstyle="Solid" borderwidth="1px"
                                                                    forecolor="White" style="text-align: center" width="120px">
                                                                    Section</label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" ToolTip="Export To Excel" Width="30px"
                                                                    CssClass="btn btn-sm btn-success"><i  class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAection" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvLine" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Attn. Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPrsnt" runat="server"
                                                        Text='Present'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvAbsnt" runat="server"
                                                        Text='Absent'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvLeave" runat="server"
                                                        Text='Leave'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP1" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p1"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA1" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a1")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL1" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l1")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP2" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p2"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA2" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a2")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL2" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l2")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP3" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p3"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA3" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a3")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL3" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l3")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP4" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p4"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA4" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a4")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL4" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l4")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP5" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p5"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA5" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a5")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL5" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l5")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP6" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p6"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA6" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a6")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL6" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l6")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP7" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p7"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA7" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a7")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL7" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l7")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 8">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP8" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p8"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA8" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a8")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL8" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l8")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 9">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p9"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a9")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l9")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP10" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p10"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA10" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a10")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL10" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l10")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 11">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP11" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p11"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA11" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a11")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL11" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l11")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 12">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP12" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p12"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA12" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a12")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL12" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l12")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 13">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP13" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p13"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA13" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a13")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL13" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l13")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 14">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP14" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p14"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA14" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a14")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL14" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l14")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 15">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP15" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p15"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA15" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a15")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL15" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l15")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 16">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP16" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p16"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA16" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a16")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL16" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l16")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 17">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP17" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p17"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA17" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a17")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL17" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l17")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 18">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP18" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p18"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA18" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a18")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL18" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l18")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 19">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP19" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p19"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA19" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a19")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL19" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l19")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 20">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP20" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p20"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA20" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a20")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL20" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l20")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 21">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP21" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p21"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA21" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a21")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL21" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l21")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 22">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP22" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p22"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA22" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a22")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL22" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l22")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 23">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP23" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p23"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA23" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a23")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL23" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l23")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 24">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP24" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p24"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA24" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a24")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL24" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l24")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 25">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP25" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p25"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA25" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a25")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL25" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l25")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 26">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP26" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p26"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA26" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a26")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL26" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l26")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 27">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP27" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p27"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA27" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a27")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL27" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l27")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 28">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP28" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p28"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA28" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a28")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL28" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l28")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Day 29">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP29" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p29"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a29")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL29" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l29")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p30"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a30")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l30")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day 31">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP31" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p31"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA31" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a31")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL31" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l31")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTotalPresent" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalprsnt"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvTotalAbsent" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalabsnt")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvTotalLeave" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalleave")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvTotalFPresent" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvTotalFAbsent" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvTotalFLeave" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                        </EmptyDataTemplate>
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                            <asp:View ID="ViewMonAttnSumLineWise" runat="server">
                                <div class="table-responsive">

                                    <asp:GridView ID="gvMonAttnSumLineWise" runat="server" AllowPaging="True" OnPageIndexChanging="gvMonAttnSumLineWise_PageIndexChanging"
                                        CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False"
                                        ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAlNos2IndOvrSum" runat="server" Font-Bold="True"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Section">
                                                <HeaderTemplate>
                                                    <table style="width: 150px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <label runat="server" backcolor="#000066"
                                                                    bordercolor="White" borderstyle="Solid" borderwidth="1px"
                                                                    forecolor="White" style="text-align: center" width="120px">
                                                                    Section</label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" ToolTip="Export To Excel" Width="30px" 
                                                                    CssClass="btn btn-sm btn-success"><i  class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAection" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line">                                              
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvLine" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                 <FooterTemplate>
                                                    <asp:Label ID="lgvotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Attn. Status">
                                                <HeaderTemplate>
                                                    <table style="width: 150px;">
                                                        <tr>
                                                            <td class="style58">
                                                                <label runat="server" backcolor="#000066"
                                                                    bordercolor="White" borderstyle="Solid" borderwidth="1px"
                                                                    forecolor="White" style="text-align: center" width="120px">
                                                                    Attn. Status</label>
                                                            </td>
                                                            <td class="style60">&nbsp;</td>
                                                            <td>
                                                                <asp:HyperLink ID="hlbtnCBdataExel" runat="server" ToolTip="Export To Excel" Width="30px"
                                                                    CssClass="btn btn-sm btn-success"><i  class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPrsnt" runat="server"
                                                        Text='Present'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvAbsnt" runat="server"
                                                        Text='Absent'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvLeave" runat="server"
                                                        Text='Leave'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvotal" runat="server" Font-Bold="True" Font-Size="12px" Text="Total"
                                                        ForeColor="#000" Style="text-align: right"></asp:Label><br />
                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 1">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP1" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p1"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA1" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a1")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL1" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l1")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL1" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 2">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP2" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p2"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA2" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a2")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL2" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l2")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL2" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 3">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP3" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p3"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA3" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a3")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL3" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l3")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL3" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 4">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP4" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p4"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA4" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a4")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL4" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l4")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL4" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP5" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p5"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA5" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a5")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL5" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l5")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL5" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 6">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP6" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p6"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA6" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a6")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL6" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l6")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL6" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 7">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP7" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p7"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA7" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a7")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL7" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l7")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL7" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 8">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP8" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p8"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA8" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a8")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL8" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l8")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL8" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 9">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p9"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a9")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l9")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL9" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP10" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p10"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA10" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a10")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL10" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l10")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 11">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP11" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p11"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA11" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a11")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL11" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l11")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 12">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP12" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p12"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA12" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a12")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL12" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l12")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 13">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP13" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p13"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA13" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a13")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL13" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l13")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 14">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP14" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p14"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA14" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a14")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL14" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l14")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 15">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP15" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p15"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA15" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a15")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL15" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l15")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 16">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP16" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p16"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA16" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a16")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL16" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l16")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 17">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP17" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p17"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA17" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a17")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL17" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l17")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 18">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP18" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p18"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA18" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a18")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL18" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l18")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 19">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP19" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p19"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA19" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a19")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL19" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l19")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 20">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP20" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p20"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA20" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a20")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL20" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l20")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 21">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP21" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p21"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA21" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a21")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL21" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l21")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 22">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP22" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p22"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA22" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a22")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL22" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l22")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 23">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP23" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p23"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA23" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a23")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL23" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l23")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 24">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP24" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p24"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA24" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a24")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL24" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l24")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 25">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP25" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p25"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA25" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a25")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL25" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l25")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL25" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 26">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP26" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p26"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA26" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a26")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL26" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l26")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL26" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 27">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP27" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p27"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA27" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a27")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL27" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l27")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL27" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 28">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP28" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p28"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA28" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a28")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL28" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l28")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL28" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 29">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP29" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p29"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a29")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL29" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l29")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL29" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 30">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p30"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a30")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l30")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL30" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 31">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP31" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p31"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA31" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a31")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL31" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l31")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL31" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 32">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP32" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p32"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA32" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a32")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL32" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l32")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP32" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA32" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL32" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 33">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP33" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p33"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA33" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a33")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL33" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l33")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP33" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA33" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL33" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 34">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP34" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p34"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA34" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a34")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL34" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l34")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP34" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA34" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL34" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 35">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP35" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p35"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA35" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a35")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL35" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l35")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP35" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA35" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL35" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 36">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP36" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p36"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA36" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a36")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL36" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l36")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP36" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA36" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL36" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 37">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP37" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p37"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA37" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a37")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL37" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l37")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP37" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA37" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL37" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 38">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP38" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p38"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA38" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a38")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL38" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l38")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP38" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA38" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL38" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 39">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP39" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p39"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a39")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL39" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l39")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP39" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA39" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL39" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 40">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP40" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p40"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA40" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a40")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL30" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l40")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP40" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA40" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL40" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 41">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP41" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p41"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA41" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a41")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL41" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l41")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP41" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA41" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL41" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 42">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP42" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p42"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA42" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a42")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL42" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l42")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP42" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA42" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL42" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 43">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP43" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p43"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA43" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a43")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL43" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l43")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP43" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA43" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL43" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 44">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP44" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p44"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA44" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a44")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL44" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l44")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP44" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA44" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL44" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 45">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP45" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p45"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA45" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a45")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL45" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l45")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP45" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA45" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL45" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 46">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP46" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p46"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA46" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a46")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL46" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l46")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP46" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA46" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL46" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 47">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP47" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p47"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA47" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a47")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL47" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l47")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP47" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA47" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL47" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 48">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP48" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p48"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA48" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a48")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL48" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l48")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP48" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA48" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL48" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 49">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP49" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p49"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a49")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL49" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l49")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP49" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA49" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL49" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP50" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p50"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA50" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a50")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL50" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l50")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP50" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA50" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL50" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 51">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP51" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p51"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA51" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a51")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL51" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l51")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP51" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA51" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL51" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 52">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP52" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p52"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA52" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a52")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL52" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l52")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP52" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA52" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL52" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 23">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP53" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p53"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA53" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a53")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL53" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l53")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP53" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA53" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL53" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 24">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP54" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p54"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA54" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a54")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL54" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l54")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP54" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA54" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL54" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 25">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP55" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p55"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA55" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a55")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL55" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l55")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP55" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA55" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL55" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 26">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP56" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p56"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA56" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a56")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL56" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l56")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP56" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA56" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL56" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 27">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP57" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p57"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA57" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a57")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL57" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l57")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP57" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA57" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL57" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 28">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP58" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p58"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA58" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a58")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL58" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l58")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP58" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA58" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL58" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line 29">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP59" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p59"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA9" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a59")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL59" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l59")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP59" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA59" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL59" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 60">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP60" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p60"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA60" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a60")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL60" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l60")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP60" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA60" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL60" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 61">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP61" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p61"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA61" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a61")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL61" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l61")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP61" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA61" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL61" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Line 62">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvP62" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "p62"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvA62" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "a62")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvL62" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "l62")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFP62" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFA62" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvFL62" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lgvTotalPresent" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalprsnt"))%>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvTotalAbsent" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalabsnt")) %>'
                                                        Width="40px"></asp:Label>
                                                    <asp:Label ID="lgvTotalLeave" runat="server"
                                                        Text='<%# Convert.ToInt32(DataBinder.Eval(Container.DataItem, "totalleave")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvTotalFPresent" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvTotalFAbsent" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                    <asp:Label ID="lgvTotalFLeave" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div style="color: red; text-align: center !important; font-style: italic; font-size: 15px;">No records to display.</div>
                                        </EmptyDataTemplate>
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
