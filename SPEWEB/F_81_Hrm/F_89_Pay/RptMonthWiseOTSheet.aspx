<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptMonthWiseOTSheet.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_89_Pay.RptMonthWiseOTSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            var gvMonWiseOTSheet = $('#<%=this.gvMonWiseOTSheet.ClientID %>');
            gvMonWiseOTSheet.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>

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
                        <asp:Label ID="lblEmpType" runat="server" CssClass="label">Employee Type</asp:Label>
                        <asp:DropDownList ID="ddlWstation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 col-sm-2 col-lg-2" id="divFrmDate" runat="server">
                    <div class="form-group">
                        <asp:Label ID="lblFrmDate" runat="server" CssClass="label">From</asp:Label>
                        <asp:TextBox ID="txtFrmDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFrmDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtFrmDate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-2 col-sm-2 col-lg-2" id="divToDate" runat="server">
                    <div class="form-group">
                        <asp:Label ID="lblToDate" runat="server" CssClass="label">To</asp:Label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtToDate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-1 col-sm-1 col-lg-1 ">
                    <div class="form-group">
                        <asp:Label ID="lblPageSize" runat="server" CssClass="label">Page Size</asp:Label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
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
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-1 col-sm-1 col-lg-1 ">
                    <div class="form-group" style="margin-top: 20px;">
                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card card-fluid">
        <div class="card-body">
            <div class="row" style="min-height: 350px;" runat="server">
                <asp:GridView ID="gvMonWiseOTSheet" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                    AllowPaging="True" OnPageIndexChanging="gvMonWiseOTSheet_PageIndexChanging">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date">
                            <HeaderTemplate>
                                <asp:Label ID="lblgvHDate" runat="server" Font-Bold="true" Text="Date" Width="70px"></asp:Label>
                                <asp:HyperLink ID="hlbtnexportexcel" runat="server" CssClass="btn btn-success btn-xs" ToolTip="Export To Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "adate")).ToString("dd-MMM-yyyy") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS1" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS1" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS2" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS2" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS3" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS3" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS4" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS4" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS5" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS5" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS6" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS6" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS7" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS7" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS8" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS8" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS9" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS9" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:Label ID="lblgvS10" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFS10" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total OT">
                            <ItemTemplate>
                                <asp:Label ID="lblgvTotalOT" runat="server" BackColor="Transparent" BorderStyle="None"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalot")).ToString("#,##0.00;(#,##0.00); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgvFTotOT" runat="server" Font-Bold="True"
                                    ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" Font-Bold="true" />
                            <FooterStyle HorizontalAlign="Right" />
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
</asp:Content>

