<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DailyAttenSummary.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.DailyAttenSummary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            try {
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {

                alert(e);
            }

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
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label26" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label27" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label28" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label30" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpLine" runat="server" CssClass="label">Line</asp:Label>
                                <asp:DropDownList ID="ddlEmpLine" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                            <asp:TextBox ID="txtDate" runat="server" CssClass=" form-control form-control-sm "></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" style="margin-top: 20px">
                            <asp:LinkButton ID="lnkbtn" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lnkbtn_Click">Ok</asp:LinkButton>

                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <div class="row">
                        <div class="table-responsive">
                            <asp:MultiView ID="MultiView" runat="server">
                                <asp:View ID="AttenSum1" runat="server">
                                    <asp:GridView ID="gvDailyAttnSummary" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvDailyAttnSummary_RowDataBound"
                                        AutoGenerateColumns="False" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Section" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsectionid" runat="server"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">

                                                <%--  <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primarygrdBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsection" runat="server"
                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lindesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDesginat" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "degdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvrcmrks" runat="server" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "newemp")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" BackColor="transparent" BorderStyle="none"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Required <br/> Manpower">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequired" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqmpower")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Held <br/> Manpower">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHeld" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "heldmpower")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="80px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Male">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMale" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "male")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Female">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFemale" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "female")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Present">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPresent" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "psent")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Absent">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAbsent" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshort" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "short1")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Over">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOver" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "overmpower")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="50px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Absent %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAbsend" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abspersnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Style="text-align: right"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtdgvrcmrks" runat="server" Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                        Width="120px" BackColor="transparent" BorderStyle="none"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle CssClass="" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />
                                    </asp:GridView>
                                </asp:View>
                                <asp:View ID="AttenSum2" runat="server">
                                    <asp:GridView ID="gvDailyAttnSummary2" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" OnRowCreated="gvDailyAttnSummary2_RowCreated" ShowFooter="True">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL #">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emp. Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvEmpType" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "emptype")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Line">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvLine" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "linedesc")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsectionid" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label Text="Total :" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supervisor" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBSupervisorid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "budsup")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvBSupervisorF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lineman" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBLinemanid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "budline")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvBLinemanF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opreator" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBOpreatorid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "budop")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvBOpreatoridF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBTotalid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "btotal")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvBTotalidF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supervisor" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPSuperid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "psup")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvPSuperF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lineman" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPLinemanid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pline")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvPLinemanF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op. W1" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPOw1id" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pw1")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvPOw1F" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op. W2" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPOw2id" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pw2")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvPOw2F" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op. W3" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPOw3id" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pw3")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvPOw3F" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPTotal" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ptotal")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvPTotalF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Super" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvASuperid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "asup")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvASuperF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lineman" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvALinemanid" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aline")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvALinemanF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op. W1" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAOw1id" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aw1")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvAOw1F" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op. W2" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAOw2id" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aw2")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvAOw2F" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Op. W3" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvAOw3id" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aw3")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvAOw3F" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvATotal" runat="server" ForeColor="Red"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "atotal")).ToString("#,##;(#,##);") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvATotalF" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>


                                        <FooterStyle CssClass="" HorizontalAlign="left" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="gvHeader" />

                                    </asp:GridView>
                                </asp:View>
                                <div class="table-responsive">
                                    <asp:View ID="AttenCatSum" runat="server">
                                        <asp:GridView ID="gvAttnCatSummary" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvAttnCatSummary_RowDataBound"
                                            AutoGenerateColumns="False" ShowFooter="True">
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL #">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCatSlNo" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Section" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCatsectionid" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Section">

                                                    <%--  <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primarygrdBtn" OnClick="lbtnUpdate_Click">Update</asp:LinkButton>
                                            </FooterTemplate>--%>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCatsection" runat="server"
                                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "secdesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Line">

                                                    <HeaderTemplate>
                                                        <table style="width: 100px;">
                                                            <tr>
                                                                <td class="style58">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Line" Width="60px"></asp:Label>
                                                                </td>
                                                                <td class="style60">&nbsp;</td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-danger btn-xs fa fa-file-excel" ToolTip="Export To Excel"></asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>


                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvlinecsum" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lindesc")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-success  btn-sm"
                                                            OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <%--                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCatDesginat" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "degdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkbtnUpdate" runat="server" CssClass="btn btn-primary  primarygrdBtn"
                                                         OnClick="lnkbtnUpdate_Click">Update</asp:LinkButton>
                                                </FooterTemplate>

                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Manpower">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatheldmpower" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "heldmpower")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mat. Leave">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatmatleave" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "matleave")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="On Leave">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatleav" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leav")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Absent">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatAbsent" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "absnt")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Holiday">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvholiday" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hday")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Soft Present">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatPresent" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "psent")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lnkTotal" runat="server" CssClass="btn btn-primary  primarygrdBtn btn-sm"
                                                            OnClick="lnkTotal_Click">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gate Pass">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgatepass" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gatepass")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="40px" Style="text-align: right"></asp:Label>

                                                        <asp:TextBox ID="txtgatePass" runat="server" ReadOnly="true" type="number" min="0"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gatepass")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="60px" Style="text-align: right"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftgatepass" runat="server" Text='' Width="100px" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="center" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual <br/> Presents">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatAcpresent" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "acpresent")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftacpresent" runat="server" Text=''
                                                            Width="80px" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>

                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Actual <br/> Manpower">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatActMan" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actman")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="80px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Label ID="lblftactman" runat="server" Text=''
                                                            Width="80px" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Absent %" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCatAbsend" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "abspersnt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px" Style="text-align: right"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtdgvCatrcmrks" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                            Width="120px" BackColor="transparent" BorderStyle="none"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Depcode" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCatdepcode" runat="server" Font-Size="11px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "depcode")) %>'
                                                            Width="60px" BackColor="transparent" BorderStyle="none"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="true" HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>

                                            </Columns>
                                            <FooterStyle CssClass="" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="gvHeader" />
                                        </asp:GridView>
                                    </asp:View>
                                </div>
                            </asp:MultiView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

